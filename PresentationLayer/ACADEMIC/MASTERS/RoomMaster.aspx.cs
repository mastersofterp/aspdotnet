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

using System.Collections.Generic;

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
                SetInitialRow();
                this.PopulateDropDown();
                divMsg.InnerHtml = string.Empty;
                ViewState["roomname"] = string.Empty;
                this.BindRooms();

            }
            //int collegecode = Convert.ToInt16(objCommon.LookUp("ACD_DEPARTMENT", "DISTINCT COLLEGE_CODE", "DEPTNO = " + Convert.ToInt32(ddlDept.SelectedValue)));
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -
            //objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));//Header
        }


    }
    private void SetInitialRow()
    {
        //new 
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("txtRoomName", typeof(string)));
        dt.Columns.Add(new DataColumn("txtRoomCapacity", typeof(string)));
        dt.Columns.Add(new DataColumn("chkStatus", typeof(string)));


        dr = dt.NewRow();
        dr["txtRoomName"] = string.Empty;
        dr["txtRoomCapacity"] = string.Empty;
        dr["chkStatus"] = string.Empty;


        dt.Rows.Add(dr);
        ViewState["CurrentTable"] = dt;

        lvAssessment.DataSource = dt;
        lvAssessment.DataBind();
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

    public Boolean checklistview()
    {

        foreach (ListViewDataItem item in lvAssessment.Items)
        {
            HiddenField hfdValue = item.FindControl("hfdValue") as HiddenField;
            TextBox txtGradeReleaseName = item.FindControl("txtRoomName") as TextBox;
            TextBox txtOutOfMarks = item.FindControl("txtRoomCapacity") as TextBox;
            CheckBox chkactive = item.FindControl("chkStatus") as CheckBox;
            if (txtGradeReleaseName.Text == "")
            {
                objCommon.DisplayMessage(this.updRoom, "Please Enter the Room Name !", this.Page);
                BindRooms();
                return false;

            }
            else if (txtOutOfMarks.Text == "")
            {
                objCommon.DisplayMessage(this.updRoom, "Please Enter the Room Capacity !", this.Page);
                BindRooms();
                return false;

            }
            else if (chkactive.Checked == false)
            {

                objCommon.DisplayMessage(this.updRoom, "Please select the Active status!", this.Page);
                BindRooms();
                return false;
            }
        }
        return true ;
    }

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
        else if (ddlFloorNo.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(this.updRoom, "Please Select Floor!", this.Page);

        }
        else if (ddlBlockNo.SelectedIndex == 0)
        {
            objCommon.DisplayMessage(this.updRoom, "Please Select Block!", this.Page);
        }
        else
        {
            try
            {


                Exam objexam = new Exam();
                objexam.department = Convert.ToInt32(ddlDept.SelectedValue);
                objexam.collegeid = Convert.ToInt32(ddlCollege.SelectedValue);

                objexam.FloorNo = Convert.ToInt32(ddlFloorNo.SelectedValue);
                objexam.Blockno = Convert.ToInt32(ddlBlockNo.SelectedValue);
                objexam.collegecode = Convert.ToInt32(ViewState["CollegeCode"]);
                foreach (ListViewDataItem item in lvAssessment.Items)
                {
                    HiddenField hfdValue = item.FindControl("hfdValue") as HiddenField;
                    TextBox txtGradeReleaseName = item.FindControl("txtRoomName") as TextBox;
                    TextBox txtRoomCapacity = item.FindControl("txtRoomCapacity") as TextBox;
                    
                    TextBox txtOutOfMarks = item.FindControl("txtRoomCapacity") as TextBox;
                    CheckBox chkactive = item.FindControl("chkStatus") as CheckBox;
                    //if (txtGradeReleaseName.Text == "")
                    //{
                    //    objCommon.DisplayMessage(this.updRoom, "Please Enter the Room Name !", this.Page);
                    //    BindRooms();
                    //    return;

                    //}
                    //else if (txtOutOfMarks.Text == "")
                    //{
                    //    objCommon.DisplayMessage(this.updRoom, "Please Enter the Room Capacity !", this.Page);
                    //    BindRooms();
                    //    return;


                    //}
                    if (checklistview() == true)
                    {
                        if (chkactive.Checked)
                        {
                            objexam.ActiveStatus = Convert.ToBoolean(1);
                        }
                        else if (cs.Equals(CustomStatus.TransactionFailed))
                        {
                            objexam.ActiveStatus = Convert.ToBoolean(0);
                        }
                        if (btnSubmit.Text == "Update")
                        {
                            int cnt = Convert.ToInt16(objCommon.LookUp("ACD_ROOM", "COUNT(*)", "ROOMCAPACITY='" + txtRoomCapacity.Text + "' and ROOMNAME LIKE '" + txtGradeReleaseName.Text.Trim() + "'"));
                            if (cnt > 0)
                            {
                                objCommon.DisplayMessage(this.updRoom, "Room Entry with the name [" + txtGradeReleaseName.Text.Trim() + "] already exists!!", this.Page);
                                return;
                            }
                            else
                            {
                                objexam.Roomname = txtGradeReleaseName.Text;
                                objexam.RoomCapacity = Convert.ToInt32(txtOutOfMarks.Text);
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
                                        btnadd.Visible = true;

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


                                    }
                                    else if (cs.Equals(CustomStatus.TransactionFailed))
                                    {
                                        objCommon.DisplayMessage(this.updRoom, "Transaction Failed!", this.Page);
                                        return;
                                    }
                                }


                            }



                        }
                        else
                        {
                            int cnt = Convert.ToInt16(objCommon.LookUp("ACD_ROOM", "COUNT(*)", "ROOMNAME LIKE '" + txtGradeReleaseName.Text.Trim() + "'"));
                            if (cnt > 0)
                            {
                                objCommon.DisplayMessage(this.updRoom, "Room Entry with the name [" + txtGradeReleaseName.Text.Trim() + "] already exists!!", this.Page);
                                return;
                            }
                            else
                            {
                                objexam.Roomname = txtGradeReleaseName.Text;
                                objexam.RoomCapacity = Convert.ToInt32(txtOutOfMarks.Text);
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
                                        this.BindRooms();
                                        ClearControls();

                                    }
                                    else if (cs.Equals(CustomStatus.TransactionFailed))
                                    {
                                        objCommon.DisplayMessage(this.updRoom, "Transaction Failed!", this.Page);
                                        return;
                                    }
                                }


                            }
                        }
                        //Added by lalit
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.updRoom, "Please Filled the listview All Column", this.Page);
                        return;

                    }

                }
                this.BindRooms();
                ClearControls();
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


    #region BindRoom Show Deatils

    private void BindRooms()
    {
        try
        {
            SeatingArrangementController objSC = new SeatingArrangementController();
            DataSet ds = objSC.GetAllRoom(Convert.ToInt32(ddlCollege.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlDept.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlDept.SelectedValue), Convert.ToInt32(ddlFloorNo.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlFloorNo.SelectedValue), Convert.ToInt32(ddlBlockNo.SelectedValue) == 0 ? 0 : Convert.ToInt32(ddlFloorNo.SelectedValue));
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
                    foreach (ListViewDataItem item in lvAssessment.Items)
                    {
                        HiddenField hfdValue = item.FindControl("hfdValue") as HiddenField;
                        TextBox txtGradeReleaseName = item.FindControl("txtRoomName") as TextBox;
                        TextBox txtOutOfMarks = item.FindControl("txtRoomCapacity") as TextBox;
                        CheckBox chkactive = item.FindControl("chkStatus") as CheckBox;
                        txtGradeReleaseName.Text = dr["ROOMNAME"] == null ? string.Empty : dr["ROOMNAME"].ToString();
                        ViewState["roomname"] = txtGradeReleaseName.Text.Trim();
                       objCommon.FillDropDownList(ddlFloorNo, "ACD_FLOOR ", "FLOORNO", "FLOORNAME", "FLOORNO > 0 AND ACTIVESTATUS=1 ", "FLOORNAME ASC");
                        ddlFloorNo.SelectedValue = dr["FLOORNO"].ToString();
                        //   int collegecode = Convert.ToInt32(objCommon.LookUp("ACD_DEPARTMENT", "DISTINCT COLLEGE_CODE", "DEPTNO = " + Convert.ToInt32(ddlDept.SelectedValue)));
                        objCommon.FillDropDownList(ddlBlockNo, "ACD_BLOCK ", "BLOCKNO", "BLOCKNAME", "BLOCKNO > 0 AND ACTIVESTATUS=1", "BLOCKNAME ASC");
                        ddlBlockNo.SelectedValue = dr["BLOCKNO"].ToString();
                        ddlCollege.SelectedValue = dr["COLLEGE_ID"].ToString();


                        objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT D, ACD_COLLEGE_DEPT C ", "D.DEPTNO", "D.DEPTNAME", "D.DEPTNO=C.DEPTNO AND C.DEPTNO >0 AND C.COLLEGE_ID=" + ddlCollege.SelectedValue + "", "DEPTNAME ASC");
                        ddlDept.SelectedValue = dr["DEPTNO"].ToString();
                        txtGradeReleaseName.Text = dr["ROOMNAME"].ToString();
                        txtOutOfMarks.Text = dr["ROOMCAPACITY"].ToString();
                        int Active = Convert.ToInt32(dr["ACTIVESTATUS"].ToString());
                        if (Active == 1)
                        {
                            chkactive.Checked = true;
                        }
                        else
                        {
                            chkactive.Checked = false;
                        }
                        // txtSequence.Text = dr["SequenceNo"].ToString();
                        //int seq = Convert.ToInt32(dr["SequenceNo"].ToString());
                        //seq = Convert.ToInt32(ViewState["SequenceNo"]);
                    }
                }
                if (dr != null) dr.Close();
            }
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
            int ROOMNAME = int.Parse(btnEdit.CommandArgument);

            bool recordExists = false;
            foreach (ListViewDataItem item in lvAssessment.Items)
            {
                HiddenField hfdValue = item.FindControl("hfdValue") as HiddenField;
                int existingROOMNAME = int.Parse(hfdValue.Value);

                if (existingROOMNAME == ROOMNAME)
                 {
                    recordExists = true;
                    break;
                }
            }
            if (!recordExists)
            {
                // Proceed with updating the record
                ViewState["roomno"] = ROOMNAME;
                ViewState["action"] = "edit";
                btnSubmit.Text = "Update";
                //this.ShowDetails(ROOMNAME);
                btnadd.Visible = false;
            }
            else
            {
                // The record is already in the ListView, you may display a message or take other actions.
                // For example:
                objCommon.DisplayMessage(this.updRoom, "The selected room is already being edited.", this.Page);
            }

            ViewState["roomno"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            btnSubmit.Text = "Update";
            this.ShowDetails(ROOMNAME);
            btnadd.Visible = false;
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


        foreach (ListViewDataItem item in lvAssessment.Items)
        {
            HiddenField hfdValue = item.FindControl("hfdValue") as HiddenField;
            TextBox txtGradeReleaseName = item.FindControl("txtRoomName") as TextBox;
            TextBox txtOutOfMarks = item.FindControl("txtRoomCapacity") as TextBox;
            CheckBox chkactive = item.FindControl("chkStatus") as CheckBox;
            txtGradeReleaseName.Text = "";
            txtOutOfMarks.Text = "";
            chkactive.Checked = false;
            ViewState["action"] = null;
            ViewState["roomno"] = null;
            ViewState["roomname"] = string.Empty;
        }
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
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_COLLEGE_ID=" + Convert.ToInt32(ddlCollege.SelectedValue) + "";
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
        objCommon.FillDropDownList(ddlFloorNo, "ACD_FLOOR ", "FLOORNO", "FLOORNAME", "FLOORNO > 0 AND ACTIVESTATUS=1 ", "FLOORNAME ASC");
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
        objCommon.FillDropDownList(ddlBlockNo, "ACD_BLOCK ", "BLOCKNO", "BLOCKNAME", "BLOCKNO > 0 AND ACTIVESTATUS=1", "BLOCKNAME ASC");
        this.BindRooms();
        ddlBlockNo.Focus();
    }
    protected void lvRoomMaster_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        BindRooms();

        
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Branch Name
        objCommon.FillDropDownList(ddlDept, "ACD_DEPARTMENT D, ACD_COLLEGE_DEPT C ", "D.DEPTNO", "D.DEPTNAME", "D.DEPTNO=C.DEPTNO AND C.DEPTNO >0 AND C.COLLEGE_ID=" + ddlCollege.SelectedValue + "", "DEPTNAME");
        this.BindRooms();
        ddlDept.Focus();
    }
    protected void ddlBlockNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.BindRooms();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        try
        {
            int rowIndex = 0; int count = 0;
            if (ViewState["CurrentTable"] != null)
            {

                if (ddlCollege.SelectedIndex == 0 && ddlDept.SelectedIndex == 0 && ddlFloorNo.SelectedIndex == 0 && ddlBlockNo.SelectedIndex == 0)
                {
                    objCommon.DisplayMessage(updRoom, "Please Select  College, Department and Floor and Block", this.Page);
                }
                else
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                    DataRow drCurrentRow = null;
                    if (lvAssessment.Items.Count > 0)
                    {

                        DataTable dtNewTable = new DataTable();
                        dtNewTable.Columns.Add(new DataColumn("txtRoomName", typeof(string)));
                        dtNewTable.Columns.Add(new DataColumn("txtRoomCapacity", typeof(string)));
                        dtNewTable.Columns.Add(new DataColumn("chkStatus", typeof(string)));
                        dtNewTable.Columns.Add(new DataColumn("ID", typeof(string)));
                        drCurrentRow = dtNewTable.NewRow();
                        drCurrentRow["txtRoomName"] = string.Empty;
                        drCurrentRow["txtRoomCapacity"] = string.Empty;
                        drCurrentRow["chkStatus"] = string.Empty;
                        drCurrentRow["ID"] = 0;
                        //dtNewTable.Rows.Add(drCurrentRow);
                        for (int i = 0; i < lvAssessment.Items.Count; i++)
                        {

                            HiddenField hdfid = (HiddenField)lvAssessment.Items[rowIndex].FindControl("hfdValue");
                            TextBox box1 = (TextBox)lvAssessment.Items[rowIndex].FindControl("txtRoomName");
                            TextBox box2 = (TextBox)lvAssessment.Items[rowIndex].FindControl("txtRoomCapacity");
                            CheckBox box3 = (CheckBox)lvAssessment.Items[rowIndex].FindControl("chkStatus");

                            int cnt = Convert.ToInt16(objCommon.LookUp("ACD_ROOM", "COUNT(*)", "ROOMNAME LIKE '" + box1.Text.Trim() + "'"));
                            if (box1.Text.Trim() == string.Empty)
                            {
                                objCommon.DisplayMessage(updRoom, "Please Enter Room Name", this.Page);
                                return;
                            }
                            else if (box2.Text.Trim() == string.Empty)
                            {
                                objCommon.DisplayMessage(updRoom, "Please Enter Room Capacity", this.Page);
                                return;
                            }
                            else if (cnt > 0)
                            {
                                objCommon.DisplayMessage(this.updRoom, "Room Entry with the name [" + box1.Text.Trim() + "] already exists!!", this.Page);
                                return;
                            }
                            else
                            {

                                drCurrentRow = dtNewTable.NewRow();
                                drCurrentRow["txtRoomName"] = box1.Text;

                                drCurrentRow["txtRoomCapacity"] = box2.Text;
                                drCurrentRow["chkStatus"] = box3.Checked;

                                //drCurrentRow = dtNewTable.NewRow();
                                drCurrentRow["ID"] = hdfid.Value;


                                rowIndex++;

                                dtNewTable.Rows.Add(drCurrentRow);

                            }



                        }


                        drCurrentRow = dtNewTable.NewRow();
                        drCurrentRow["txtRoomName"] = string.Empty;
                        drCurrentRow["txtRoomCapacity"] = string.Empty;
                        drCurrentRow["chkStatus"] = string.Empty;
                        drCurrentRow["ID"] = 0;
                        dtNewTable.Rows.Add(drCurrentRow);

                        ViewState["CurrentTable"] = dtNewTable;
                        lvAssessment.DataSource = dtNewTable;
                        lvAssessment.DataBind();
                        ///  int index1 = 0;

                    }
                    else
                    {

                        objCommon.DisplayMessage(updRoom, "Maximum Options Limit Reached", this.Page);

                    }

                }


            }
            else
            {
                objCommon.DisplayMessage(updRoom, "Error!!!", this.Page);
            }


        }
        catch (Exception ex)
        {

        }
        SetPreviousDataGrades();
    }
    private void SetPreviousDataGrades()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {



                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HiddenField hdfid = (HiddenField)lvAssessment.Items[rowIndex].FindControl("hfdValue");
                    TextBox box1 = (TextBox)lvAssessment.Items[rowIndex].FindControl("txtRoomName");
                    TextBox box2 = (TextBox)lvAssessment.Items[rowIndex].FindControl("txtRoomCapacity");
                    CheckBox box3 = (CheckBox)lvAssessment.Items[rowIndex].FindControl("chkStatus");


                    string status = Convert.ToString(dt.Rows[i]["chkStatus"].ToString());
                    if (status == "True")
                    {
                        box3.Checked = true;
                    }
                    else
                    {
                        box3.Checked = false;
                    }

                    box1.Text = dt.Rows[i]["txtRoomName"].ToString();
                    box2.Text = dt.Rows[i]["txtRoomCapacity"].ToString();
                    //box3.Checked = Convert.ToBoolean(dt.Rows[i]["chkStatus"].ToString());


                    rowIndex++;


                }

            }

        }

        else
        {
            SetInitialRow();
        }
    }
    private void SetPreviousData()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++) // Change i=1 to i=0
                {
                    // Find the ListViewItem at the specified index
                    ListViewDataItem lvItem = lvAssessment.Items[i];

                    // Find controls inside the ListViewItem
                    TextBox box1 = (TextBox)lvItem.FindControl("txtRoomName");
                    TextBox box2 = (TextBox)lvItem.FindControl("txtRoomCapacity");
                    CheckBox box3 = (CheckBox)lvAssessment.Items[i].FindControl("chkStatus");

                    // Set values from DataTable to TextBoxes
                    box1.Text = dt.Rows[i]["txtRoomName"].ToString();
                    box2.Text = dt.Rows[i]["txtRoomCapacity"].ToString();

                    //Have to change this for delete operations
                    if (dt.Rows[i]["chkStatus"] == null)
                        box3.Checked = false;
                    else
                        box3.Checked = Convert.ToBoolean(dt.Rows[i]["chkStatus"]);

                    rowIndex++;
                }
            }
        }
    }
    protected void lnkRemove_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lb = (ImageButton)sender;
        ListViewDataItem lvItem = (ListViewDataItem)lb.NamingContainer;
        int rowID = lvItem.DisplayIndex;



        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];

            if (dt.Rows.Count > 1 && rowID < dt.Rows.Count)
            {
                // Remove the Selected Row data
                dt.Rows.RemoveAt(rowID);
                // Rebind the ListView for the updated data 
                lvAssessment.DataSource = dt;
                lvAssessment.DataBind();

                // Store the current data in ViewState for future reference
                ViewState["CurrentTable"] = dt;
            }
            
        }

        // Set Previous Data on Postbacks
        SetPreviousData();
    }


    //protected void lvAssessment_RowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        DataTable dt = (DataTable)ViewState["CurrentTable"];
    //        // LinkButton lb = (LinkButton)e.Row.FindControl("lnkRemove");
    //        ImageButton lb = (ImageButton)e.Row.FindControl("lnkRemove");


    //        if (lb != null)
    //        {
    //            if (dt.Rows.Count > 1)
    //            {
    //                if (e.Row.RowIndex == dt.Rows.Count - 1)
    //                {
    //                    lb.Visible = false;
    //                }
    //            }
    //            else
    //            {
    //                lb.Visible = false;
    //            }
    //        }
    //    }
    //}














    //private void SetPreviousData()
    //{
    //    int rowIndex = 0;

    //    if (ViewState["CurrentTable"] != null)
    //    {
    //        DataTable dt = (DataTable)ViewState["CurrentTable"];
    //        if (dt.Rows.Count > 0)
    //        {
    //            for (int i = 1; i < dt.Rows.Count; i++)
    //            {
    //                // Find the ListViewItem at the specified index
    //                ListViewDataItem lvItem = lvAssessment.Items[i];

    //                // Find controls inside the ListViewItem
    //                TextBox box1 = (TextBox)lvItem.FindControl("txtRoomName");
    //                TextBox box2 = (TextBox)lvItem.FindControl("txtRoomCapacity");
    //                CheckBox box3 = (CheckBox)lvAssessment.Items[rowIndex].FindControl("chkStatus");


    //                // Set values from DataTable to TextBoxes
    //                box1.Text = dt.Rows[i]["Column1"].ToString();
    //                box2.Text = dt.Rows[i]["Column2"].ToString();
    //                box3.Checked = Convert.ToBoolean(dt.Rows[i]["Column3"]);

    //                //box3.Checked = dt.RowDeleted[i]["Column3"].ToString();
    //                //box3.Checked["chkStatus"] = box3.Checked;




    //                rowIndex++;



    //            }
    //        }
    //    }
    //}

    //protected void lnkRemove_Click(object sender, ImageClickEventArgs e)
    //{

    //    //LinkButton lb = (LinkButton)sender;
    //    ImageButton lb = (ImageButton)sender;
    //    //ListView lvRow = (ListView)lb.NamingContainer;
    //    ListViewDataItem lvItem = (ListViewDataItem)lb.NamingContainer;
    //    int rowID = lvItem.DisplayIndex+1;


    //    //int rowID = lvRow.RowIndex + 1;

    //    //int rowID = lvItem.DisplayIndex;

    //    if (ViewState["CurrentTable"] != null)
    //    {
    //        DataTable dt = (DataTable)ViewState["CurrentTable"];

    //        if (dt.Rows.Count > 0 && rowID < dt.Rows.Count)
    //        {
    //            // Remove the Selected Row data
    //            dt.Rows.RemoveAt(rowID);
    //        }
    //        if (dt.Rows.Count > 1)
    //        {
    //            //if (gvRow.RowIndex < dt.Rows.Count )
    //            //if (gvRow.RowIndex < dt.Rows.Count - 1)
    //            if (rowID < dt.Rows.Count)
    //            {
    //                //Remove the Selected Row data
    //                dt.Rows.Remove(dt.Rows[rowID]);
    //            }
    //        }
    //        //Store the current data in ViewState for future reference
    //        ViewState["CurrentTable"] = dt;
    //        //Re bind the ListView for the updated data 
    //        lvAssessment.DataSource = dt;
    //        lvAssessment.DataBind();
    //     }

    //    //Set Previous Data on Postbacks
    //    SetPreviousData();
    //}

    protected void lvAssessment_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];

            ImageButton lb = (ImageButton)e.Item.FindControl("lnkRemove");
            CheckBox box3 = (CheckBox)e.Item.FindControl("chkStatus");

            if (lb != null && box3 != null)
            {
                if (dt.Rows.Count > 1 && box3.Checked)
                {
                    lb.Visible = false;
                }
                else
                {
                    lb.Visible = true;
                }
            }
        }
    }

    //protected void lvAssessment_ItemDataBound(object sender, ListViewItemEventArgs e)
    //{
    //    if (e.Item.ItemType == ListViewItemType.DataItem)
    //    {
    //        DataTable dt = (DataTable)ViewState["CurrentTable"];

    //        ImageButton lb = (ImageButton)e.Item.FindControl("lnkRemove");

    //        if (lb != null)
    //        {
    //            if (dt.Rows.Count > 1)
    //            {
    //                if (e.Item.DisplayIndex == dt.Rows.Count - 1)
    //                {
    //                    lb.Visible = false;
    //                }
    //            }
    //            else
    //            {
    //                lb.Visible = false;
    //            }
    //        }
    //    }
    //}








    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        DataTable dt = (DataTable)ViewState["CurrentTable"];
    //        // LinkButton lb = (LinkButton)e.Row.FindControl("lnkRemove");
    //        ImageButton lb = (ImageButton)e.Row.FindControl("lnkRemove");


    //        if (lb != null)
    //        {
    //            if (dt.Rows.Count > 1)
    //            {
    //                if (e.Row.RowIndex == dt.Rows.Count - 1)
    //                {
    //                    lb.Visible = false;
    //                }
    //            }
    //            else
    //            {
    //                lb.Visible = false;
    //            }
    //        }
    //    }
    //}
}
