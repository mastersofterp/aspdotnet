using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

public partial class ACADEMIC_MASTERS_RoomMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    SeatingArrangementController objExamController = new SeatingArrangementController();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["masterpage"] != null)
        {
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        }
        else
        {
            objCommon.SetMasterPage(Page, "");
        }
    }
    #region pageload

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //This checks the authorization of the user.
              //  CheckPageAuthorization();

                Page.Title = Session["coll_name"].ToString();

                if (Request.QueryString["pageno"] != null)
                {
                   // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                }

                // Set form mode equals to -1(New Mode).
                ViewState["exdtno"] = "0";

                this.PopulateDropDown();
                divMsg.InnerHtml = string.Empty;
                ViewState["roomname"] = string.Empty;
            }
            //int collegecode = Convert.ToInt16(objCommon.LookUp("ACD_DEPARTMENT", "DISTINCT COLLEGE_CODE", "DEPTNO = " + Convert.ToInt32(ddlDept.SelectedValue)));
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -
            //objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));//Header
        }

      
    }

    private void PopulateDropDown()
    {
        try
        {

            //Degree Name
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "COLLEGE_NAME AS COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_NAME ASC");

           //For filling the Number of Floors
           //objCommon.FillDropDownList(ddlFloorNo, "ACD_FLOOR", "FLOORNO", "FLOORNAME", "FLOORNO > 0", "FLOORNO DESC");
            ddlCollege.Focus();
           
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_ExamDate.PopulateDropDown --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            //if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString()) == false)
            //{
            //    Response.Redirect("~/notauthorized.aspx?page=SessionCreate.aspx");
            //}
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=SessionCreate.aspx");
        }
    }

    #endregion

    #region Submit
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddlCollege.SelectedIndex == 0)
        {

            objCommon.DisplayMessage(this.updRoom, "Please Select College/School!", this.Page);
        }
        else if (ddlDept.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(this.updRoom, "Please Select Department!", this.Page);

        }
        else if (ddlFloorNo.SelectedIndex==0)
        {
            objCommon.DisplayMessage(this.updRoom, "Please Select Floor!", this.Page);

        }
        else if(ddlBlockNo.SelectedIndex==0)
        {
            objCommon.DisplayMessage(this.updRoom, "Please Select Block!", this.Page);
        }
        else if (txtRoomName.Text == "")
        {
            objCommon.DisplayMessage(this.updRoom, "Please Enter RoomName!", this.Page);
        }
        else if (txtRoomCapacity.Text == "")
        {
            objCommon.DisplayMessage(this.updRoom, "Please Enter RoomCapacity!", this.Page);
        }
        else
        {
            try
            {
                //Set all properties

                Exam objexam = new Exam();
                objexam.department = Convert.ToInt32(ddlDept.SelectedValue);
                objexam.collegeid = Convert.ToInt32(ddlCollege.SelectedValue);
                objexam.Roomname = txtRoomName.Text.Trim();
                objexam.RoomCapacity = Convert.ToInt32(txtRoomCapacity.Text);
                //   int Floornu = Convert.ToInt32(ddlFloorNo.SelectedValue);
                objexam.FloorNo = Convert.ToInt32(ddlFloorNo.SelectedValue);
                objexam.Blockno = Convert.ToInt32(ddlBlockNo.SelectedValue);
                objexam.collegecode = Convert.ToInt32(ViewState["CollegeCode"]);
                if (chkStatus.Checked)
                {
                    objexam.ActiveStatus = Convert.ToBoolean(1);
                }
                else
                {
                    objexam.ActiveStatus = Convert.ToBoolean(0);
                }
                //objexam.Sequence = Convert.ToInt32(txtSequence.Text.Trim());
                //int seq = Convert.ToInt32(txtSequence.Text.Trim());
                //int Sequen = Convert.ToInt32(objCommon.LookUp("ACD_ROOM", "SEQUENCENO", "SEQUENCENO =" + txtSequence.Text));

                //if (seq == Sequen)
                //{
                //    objCommon.DisplayMessage(this.updRoom, "Room Sequence already Exist!!", this.Page);
                //    //txtSequence.Text = "";
                //    return;
                //}
                //else { }


                if (this.CheckDuplicateRoom() == true)
                {
                    objCommon.DisplayMessage(this.updRoom, "Room Entry with the name [" + txtRoomName.Text.Trim() + "] already exists!!", this.Page);
                    return;
                }
                //commented on 01052022
                //if (this.CheckDuplicateSequence() == true)
                //{
                //    objCommon.DisplayMessage(this.updRoom, "Room Sequence [" + txtSequence.Text.Trim() + "] already exists!!", this.Page);
                //    return;
                //}


                //if (this.CheckDuplicateSequence() == true)
                //{
                //    objCommon.DisplayMessage(this.updRoom, "Room Sequence [" + txtSequence.Text.Trim() + "] already exists!!", this.Page);
                //    return;
                //}

                //Check for add or edit
                if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
                {
                    SeatingArrangementController objSC = new SeatingArrangementController();

                    //Edit 
                    objexam.Roomno = Convert.ToInt32(ViewState["roomno"]);
                    CustomStatus cs = (CustomStatus)objSC.UpdateRoom(objexam);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                       
                       
                        objCommon.DisplayMessage(this.updRoom, "Room Updated Successfully!", this.Page);
                        btnSubmit.Text = "Submit";
                        BindRooms();
                        ClearControls();
                    }
                }
                else
                {
                    CustomStatus cs;
                    SeatingArrangementController objSC = new SeatingArrangementController();

                    //Add New
                    cs = (CustomStatus)objSC.AddRoom(objexam);

                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                       
                        objCommon.DisplayMessage(this.updRoom, "Room Saved Successfully!", this.Page);
                        BindRooms();
                        ClearControls();
                    }
                    else if (cs.Equals(CustomStatus.TransactionFailed))
                    {
                        objCommon.DisplayMessage(this.updRoom, "Transaction Failed!", this.Page);
                        return;
                    }
                }

                this.BindRooms();
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "ACADEMIC_MASTERS_RoomMaster.btnSubmit_Click-> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.DisplayMessage("Server UnAvailable", this.Page);
            }


        }
     
    }
    #endregion

    #region Duplicate Entry Check
    private bool CheckDuplicateRoom()
    {
        //Check Room Name is duplicate entry or not
        if (ViewState["roomname"].ToString() == txtRoomName.Text)
        {
            //Room name not changed while editing
            return false;
        }

        int cnt = Convert.ToInt16(objCommon.LookUp("ACD_ROOM", "COUNT(*)", "ROOMNAME LIKE '" + txtRoomName.Text.Trim() + "'"));
        if (cnt > 0)
            return true;
        else
            return false;
    }
    private bool CheckDuplicateSequence()
    {

        SeatingArrangementController objSC = new SeatingArrangementController();
        //SqlDataReader dr = objSC.GetSingleRoom(ROOMNO);
        //this.ShowDetails(ROOMNO);
        //int sequence = Convert.ToInt16(objCommon.LookUp("ACD_ROOM", "Sequenceno", "Sequenceno =" + txtSequence.Text.Trim()));
      
        ////Check Room Name is duplicate entry or not
        //if (ViewState["SequenceNo"].ToString() == txtSequence.Text)
        //{
        //    //Room name not changed while editing
        //    return false;
        //}


        //COMMENTED ON 01052022
        //int cnt = Convert.ToInt16(objCommon.LookUp("ACD_ROOM", "COUNT(*)", "Sequenceno =" + txtSequence.Text.Trim()));
        //if (cnt > 0)
        //    return true;
        //else
            return false;
    }
    #endregion

    #region BindRoom Show Deatils

    private void BindRooms()
    {
        try
        {
            SeatingArrangementController objSC = new SeatingArrangementController();
            DataSet ds = objSC.GetAllRoom(Convert.ToInt32(ddlCollege.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlCollege.SelectedValue));
            lvRoomMaster.DataSource = ds;
            lvRoomMaster.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AcademicCalenderMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.DisplayMessage(updRoom, "Server UnAvailable", this.Page);
        }
    }

    private void ShowDetails(int ROOMNO)
    {
        try
        {
            SeatingArrangementController objSC = new SeatingArrangementController();
            SqlDataReader dr = objSC.GetSingleRoom(ROOMNO);
            if (dr != null)
            {
                if (dr.Read())
                {
                    txtRoomName.Text = dr["ROOMNAME"] == null ? string.Empty : dr["ROOMNAME"].ToString();
                    ViewState["roomname"] = txtRoomName.Text.Trim();
                    ddlFloorNo.SelectedValue = dr["FLOORNO"].ToString();
                 //   int collegecode = Convert.ToInt32(objCommon.LookUp("ACD_DEPARTMENT", "DISTINCT COLLEGE_CODE", "DEPTNO = " + Convert.ToInt32(ddlDept.SelectedValue)));
                    objCommon.FillDropDownList(ddlBlockNo, "ACD_BLOCK ", "BLOCKNO", "BLOCKNAME", "BLOCKNO > 0 AND ACTIVESTATUS=1" ,"BLOCKNAME ASC");
                    ddlBlockNo.SelectedValue = dr["BLOCKNO"].ToString();
                    ddlCollege.SelectedValue = dr["COLLEGE_ID"].ToString();
                
                   // objCommon.FillDropDownList( ddlDept, "ACD_BRANCH B INNER JOIN ACD_DEGREE D ON B.DEGREENO = D.DEGREENO", "B.BRANCHNO", "B.LONGNAME", "D.DEGREENO = " + dr["DEGREENO"].ToString(), "B.BRANCHNO" );
                    //aayushi
                    objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT D, ACD_COLLEGE_DEPT C ", "D.DEPTNO", "D.DEPTNAME", "D.DEPTNO=C.DEPTNO AND C.DEPTNO >0 AND C.COLLEGE_ID=" + ddlCollege.SelectedValue + "", "DEPTNAME ASC");
                    ddlDept.SelectedValue = dr["DEPTNO"].ToString();
                    txtRoomName.Text = dr["ROOMNAME"].ToString();
                    txtRoomCapacity.Text = dr["ROOMCAPACITY"].ToString();
                    int Active = Convert.ToInt32(dr["ACTIVESTATUS"].ToString());
                    if (Active == 1)
                    {
                        chkStatus.Checked = true;
                    }
                    else
                    {
                        chkStatus.Checked = false;
                    }
                    //txtSequence.Text = dr["SequenceNo"].ToString();
                     int seq   = Convert.ToInt32(dr["SequenceNo"].ToString());
                     seq = Convert.ToInt32(ViewState["SequenceNo"]);  
                } 
            }
            if (dr != null) dr.Close();
        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_RoomMaster.ShowDetails_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
    #endregion

    #region Degree
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    #endregion

    #region Delete_click

    //Function For Deleting the Selected Academic Calendar Session
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            SeatingArrangementController objSC = new SeatingArrangementController();
            ImageButton btnDelete = sender as ImageButton;

            //Delete 
            CustomStatus cs = (CustomStatus)objSC.DeleteRoom(Convert.ToInt32(btnDelete.ToolTip));
            
           // objCommon.DisplayMessage("Room Deleted Succesfully !!", this.Page);
            objCommon.DisplayMessage(this, "Room Deleted Succesfully !!", this);
            //aayushi
            BindRooms();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_RoomMaster.btnDelete_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }

    }
    #endregion

    #region edit

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int ROOMNO = int.Parse(btnEdit.CommandArgument);
            ViewState["roomno"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            btnSubmit.Text = "Update";
            this.ShowDetails(ROOMNO);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_MASTERS_RoomMaster.btnEdit_Click-> " + ex.Message + "" + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }
    #endregion

    #region clear

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void ClearControls()
    {
        ddlCollege.SelectedIndex = 0;
        ddlDept.SelectedIndex = 0;
        ddlFloorNo.SelectedIndex = 0;
        ddlBlockNo.SelectedIndex = 0;
        chkStatus.Checked = false;
        txtRoomCapacity.Text = string.Empty;
        txtRoomName.Text = string.Empty;
        ViewState["action"] = null;
        ViewState["roomno"] = null;
        ViewState["roomname"] = string.Empty;
       //txtSequence.Text = string.Empty;
    }
    #endregion

    #region Report
    protected void btnreport_Click(object sender, EventArgs e)
    {
        ShowAbsentReport("Available_Room_Report", "rptRoomMasterDescription.rpt");
    }

    private void ShowAbsentReport(string reportTitle, string rptFileName)
    {
        try
        {
            
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/Commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue)  + "";
            //divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            //divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //divMsg.InnerHtml += " </script>";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updRoom, this.updRoom.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ACADEMIC_EXAMINATION_AnsPaperRecord.ShowDocketReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    #endregion

    #region Dept

    protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        int collegecode = Convert.ToInt32(objCommon.LookUp("ACD_DEPARTMENT", "DISTINCT COLLEGE_CODE", "DEPTNO = " + Convert.ToInt32(ddlDept.SelectedValue)));
        ViewState["CollegeCode"] = collegecode;
        objCommon.FillDropDownList(ddlFloorNo, "ACD_FLOOR ", "FLOORNO", "FLOORNAME", "FLOORNO > 0 AND ACTIVESTATUS=1 ","FLOORNAME ASC");
       // objCommon.FillDropDownList(ddlBlockNo, "ACD_BLOCK", "BLOCKNO", "BLOCKNAME", "BLOCKNO > 0 AND COLLEGE_CODE=" + collegecode, "BLOCKNAME ASC");
        this.BindRooms();
        ddlFloorNo.Focus();
       // objCommon.FillDropDownList(ddlRoom, "ACD_ROOM R INNER JOIN ACD_BLOCK B ON B.BLOCKNO=R.BLOCKNO INNER JOIN ACD_DEPARTMENT D ON R.DEPTNO=D.DEPTNO", "R.ROOMNO", "CONCAT(ROOMNAME,'-',DEPTCODE)", "R.ROOMNO > 0 AND  R.BLOCKNO=" + ddlBlockNo.SelectedValue + "", "ROOMNAME ASC");

    }
    #endregion

    protected void ddlBlock_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void ddlFloorNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        int collegecode = Convert.ToInt16(objCommon.LookUp("ACD_DEPARTMENT", "DISTINCT COLLEGE_CODE", "DEPTNO = " + Convert.ToInt32(ddlDept.SelectedValue)));
        objCommon.FillDropDownList(ddlBlockNo, "ACD_BLOCK ", "BLOCKNO", "BLOCKNAME", "BLOCKNO > 0 AND ACTIVESTATUS=1","BLOCKNAME ASC");
       // this.BindRooms();
        ddlBlockNo.Focus();
    }
    protected void lvRoomMaster_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
         
        
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Branch Name
        objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT D, ACD_COLLEGE_DEPT C ", "D.DEPTNO", "D.DEPTNAME", "D.DEPTNO=C.DEPTNO AND C.DEPTNO >0 AND C.COLLEGE_ID=" + ddlCollege.SelectedValue + "", "DEPTNAME");
        //  this.BindRooms();
        ddlDept.Focus();
    }
}
