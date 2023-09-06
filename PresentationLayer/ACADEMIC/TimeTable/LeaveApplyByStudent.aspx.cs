//======================================================================================
// PROJECT NAME  : UAIMS                                                        
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : Event And Holidays Entry Form.                                      
// CREATION DATE :                                                        
// CREATED BY    :                                                
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

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
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System.Activities.Statements;
using System.IO;


public partial class ACADEMIC_TIMETABLE_LeaveApplyByStudent : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    AcdAttendanceController objAttC = new AcdAttendanceController();
    AcdAttendanceModel objAttModel = new AcdAttendanceModel();
    
    #region Page Methods
    protected void Page_PreInit(object sender, EventArgs e)
    {
        //To Set the MasterPage
        if (Session["masterpage"] != null)
            objCommon.SetMasterPage(Page, Session["masterpage"].ToString());
        else
            objCommon.SetMasterPage(Page, "");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                //Check Session
                if (Session["userno"] == null || Session["username"] == null ||
                    Session["usertype"] == null || Session["userfullname"] == null)
                {
                    Response.Redirect("~/default.aspx");
                }
                else
                {
                    //Page Authorization
                    this.CheckPageAuthorization();
                    Blob_Storage();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    if (Session["usertype"].ToString() == "2")
                    {
                        int idno = 0;
                        if (Session["userno"] != null)
                            idno = Convert.ToInt32(objCommon.LookUp("user_acc", "UA_IDNO", "UA_NO=" + Convert.ToInt32(Session["userno"])));
                        ViewState["IDNO"] = idno;
                        this.ShowStudDetails(idno);
                    }
                    else
                    {
                        objCommon.DisplayMessage(updHoliday, "Sorry you are not Authorized to view this Page !", this.Page);
                        // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Sorry, your are not Authorized to view this Page !');window.location ='';", true);
                        Response.Redirect("~/notauthorized.aspx?page=LeaveApplyByStudent.aspx");
                        return;
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Sorry, your are not Authorized to view this Page !');window.location ='../../home.aspx';", true);
                    }

                    this.FillDropdown();


                    ViewState["ipAddress"] = Request.ServerVariables["REMOTE_ADDR"];
                }
            }

            this.BindListView();
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AbsentStudentEntry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AbsentStudentEntry.aspx");
        }
    }
    private void Blob_Storage()
    {

        string blob_ContainerName = "";
        string blob_ConStr = "";
        if (System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"] != null)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_OD"] != null)
            {
                blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_OD"].ToString();
                blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
            }
            else
            {
                objCommon.DisplayUserMessage(this.updHoliday, "Something went wrong, Blob Storage container related details not found.", this.Page);
                return;
            }
        }
        else
        {
            objCommon.DisplayUserMessage(this.updHoliday, "Something went wrong, Blob Storage container related details not found.", this.Page);
            return;
        }
    }
    private void FillDropdown()
    {
        try
        {
            if (ViewState["IDNO"] != null)
            {
                //string activeSession = objCommon.LookUp("ACD_ATTENDANCE_CONFIG", "MAX(ISNULL(SESSIONNO,0))", "");
                string activeSession = objCommon.LookUp("ACD_STUDENT_RESULT R INNER JOIN ACD_STUDENT S ON S.IDNO=R.IDNO AND S.SEMESTERNO=R.SEMESTERNO", "MAX(ISNULL(SESSIONNO,0))", "S.IDNO= " + ViewState["IDNO"]);

                objCommon.FillDropDownList(ddlSession, "ACD_SESSION_MASTER", "SESSIONNO", "SESSION_PNAME", "SESSIONNO>0 AND ISNULL(IS_ACTIVE,0)=1", "SESSIONNO DESC");
                ddlSession.SelectedValue = activeSession;
                ddlSession.Enabled = false;
                objCommon.FillDropDownList(ddlLeaveName, "acd_specialleavetype", "specialleavetypeno", "specialleavetype", "specialleavetypeno>0 AND ISNULL(ACTIVESTATUS , 0) = 1", "specialleavetypeno");
                objCommon.FillDropDownList(ddlOdType, "ACD_ODTYPE", "ODID", "OD_NAME", "ODID>0 AND ISNULL(ACTIVESTATUS , 0) = 1", "ODID");
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void BindListView()
    {
        try
        {
            SessionController objsessionn = new SessionController();
            DataSet ds = objAttC.GetAllLeaveByStudent(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(ViewState["IDNO"]));
            lvExamday.DataSource = ds;
            lvExamday.DataBind();
            objCommon.SetListViewLabel("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]), lvExamday);//Set label - 
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (ListViewDataItem item in lvExamday.Items)
                {
                    ImageButton btnEdit = item.FindControl("btnEdit") as ImageButton;
                    Label lblStatus = item.FindControl("lblAStatus") as Label;

                    if (lblStatus.Text == "PENDING")
                    {
                        btnEdit.Enabled = true;
                        btnEdit.ImageUrl = "~/Images/edit.png";
                    }
                    else
                    {
                        btnEdit.Enabled = false;
                        if (lblStatus.Text == "APPROVED")
                        {
                            btnEdit.ImageUrl = "~/images/check1.jpg";
                            //btnEdit.ImageUrl.wi="50px";
                            btnEdit.Width = Unit.Pixel(15);
                        }
                        if (lblStatus.Text == "REJECTED")
                        {
                            btnEdit.ImageUrl = "~/Images/delete.png";
                        }
                        if (lblStatus.Text == "CANCELLED")
                        {
                            btnEdit.ImageUrl = "~/Images/delete.png";
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #endregion

    #region Usetr defied Methods

    private bool CheckDuplicateEntry()
    {
        bool flag = false;
        try
        {
            int Academic_Session = 0;
            //if (ChkDate.Checked)
            //    Academic_Session = objCommon.LookUp("ACD_ACADEMIC_LEAVE_MASTER", "HOLIDAY_NO", "CONVERT(nvarchar,ACADEMIC_HOLIDAY_STDATE,103)= dbo.DateOnly('" + Convert.ToDateTime(txtFromDate.Text.Trim()) + "')  AND CONVERT(nvarchar,ACADEMIC_HOLIDAY_ENDDATE,103) = dbo.DateOnly('" + Convert.ToDateTime(txtToDate.Text.Trim()) + "') ");
            //else
            //   
            Academic_Session = Convert.ToInt32(objCommon.LookUp("ACD_ACADEMIC_LEAVE_MASTER", "COUNT(*)", "isnull(cancel,0)=0 and idno=" + Convert.ToInt32(lblName.ToolTip) + " AND ISNULL(APPROVAL_STATUS,0)<>2 AND ( ( CONVERT(DATE,'" + Convert.ToDateTime(txtFromDate.Text.Trim()).ToString("dd/MM/yyyy") + "',103) between CONVERT(DATE,ACADEMIC_HOLIDAY_STDATE,103) and CONVERT(DATE,ACADEMIC_HOLIDAY_ENDDATE,103) ) OR  (CONVERT(DATE,ACADEMIC_HOLIDAY_STDATE,103) between CONVERT(DATE,'" + Convert.ToDateTime(txtFromDate.Text.Trim()).ToString("dd/MM/yyyy") + "',103)  and CONVERT(DATE,'" + Convert.ToDateTime(txtToDate.Text.Trim()).ToString("dd/MM/yyyy") + "',103) ) OR  (CONVERT(DATE,ACADEMIC_HOLIDAY_ENDDATE,103) between CONVERT(DATE,'" + Convert.ToDateTime(txtFromDate.Text.Trim()).ToString("dd/MM/yyyy") + "',103)  and CONVERT(DATE,'" + Convert.ToDateTime(txtToDate.Text.Trim()).ToString("dd/MM/yyyy") + "',103) )  )"));

            if (Academic_Session > 0)
            {
                flag = true;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return flag;
    }
    private void ShowDetails(int Holidayno)
    {
        try
        {
            string _slotNos = string.Empty;
            SqlDataReader dr = objAttC.GetSingleAcademicLeave(Holidayno);
            if (dr != null)
            {
                if (dr.Read())
                {

                    txtEventDetail.Text = dr["ACADEMIC_HOLIDAY_DETAIL"] == null ? string.Empty : dr["ACADEMIC_HOLIDAY_DETAIL"].ToString();
                    ddlLeaveName.SelectedValue = dr["ACADEMIC_LEAVE_NO"] == null ? string.Empty : dr["ACADEMIC_LEAVE_NO"].ToString();

                    if (dr["ODTYPE"] == null | dr["ODTYPE"].ToString().Equals(""))
                        ddlOdType.SelectedIndex = 0;
                    else
                        ddlOdType.SelectedValue = dr["ODTYPE"].ToString();

                    txtFromDate.Text = dr["ACADEMIC_HOLIDAY_STDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["ACADEMIC_HOLIDAY_STDATE"].ToString()).ToString("dd/MM/yyyy");
                    txtToDate.Text = dr["ACADEMIC_HOLIDAY_ENDDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["ACADEMIC_HOLIDAY_ENDDATE"].ToString()).ToString("dd/MM/yyyy");
                    if(dr["FILENAME"]==null)
                    { 
                        lblFileName.Visible=false;
                    }
                    else
                    {
                        lblFileName.Visible=true;
                        lblFileName.Text = dr["FILENAME"].ToString();
                    }
                   
                    if (!string.IsNullOrEmpty(txtFromDate.Text))
                    {
                        this.txtFromDate_TextChanged(new object(), new EventArgs());
                    }
                    if (dr["SESSIONNO"] == null | dr["SESSIONNO"].ToString().Equals(""))
                        ddlSession.SelectedIndex = 0;
                    else
                        ddlSession.SelectedValue = dr["SESSIONNO"].ToString();



                    if (chkSlots.Items.Count > 0)
                    {
                        _slotNos = dr["SLOTNO"].ToString();
                        string[] values = _slotNos.Split(',');
                        foreach (string a in values)
                        {
                            for (int i = 0; i < chkSlots.Items.Count; i++)
                            {
                                if (chkSlots.Items[i].Value == a)
                                    chkSlots.Items[i].Selected = true;
                            }
                        }
                    }

                    #region commented
                    //  txtToDate.Text = dr["ACADEMIC_HOLIDAY_ENDDATE"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["ACADEMIC_HOLIDAY_ENDDATE"].ToString()).ToString("dd/MM/yyyy");
                    //if (dr["ACADEMIC_HOLIDAY_ENDDATE"] == null | dr["ACADEMIC_HOLIDAY_ENDDATE"].ToString().Equals(""))
                    //{
                    //    //  tdToDate.Visible = false;                      
                    //    ChkDate.Checked = false;
                    //}
                    //else
                    //{
                    //    ChkDate.Checked = true;
                    //    //  tdToDate.Visible = true;                                               
                    //}
                    #endregion commented
                }
            }

            if (dr != null) dr.Close();

            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private void ClearControls()
    {

        txtEventDetail.Text = string.Empty;
        ddlLeaveName.SelectedIndex = 0;
        txtFromDate.Text = string.Empty;
        txtToDate.Text = string.Empty;
        ViewState["action"] = null;
        chkSlots.Items.Clear();
        chkCheckAll.Enabled = false;
        ddlOdType.SelectedIndex = 0;

    }
    private void ShowStudDetails(int idno)
    {
        try
        {

            DataSet dsStudent = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_BRANCH B ON (S.BRANCHNO = B.BRANCHNO) INNER JOIN ACD_SEMESTER SM ON (S.SEMESTERNO = SM.SEMESTERNO) INNER JOIN ACD_SCHEME SC ON (S.SCHEMENO = SC.SCHEMENO) LEFT OUTER JOIN ACD_ADMBATCH AM ON (S.ADMBATCH = AM.BATCHNO) INNER JOIN ACD_DEGREE DG ON (S.DEGREENO = DG.DEGREENO)", "S.IDNO,DG.DEGREENAME", "S.STUDNAME,S.FATHERNAME,S.MOTHERNAME,S.REGNO,S.ROLLNO,S.SEMESTERNO,S.SCHEMENO,SM.SEMESTERNAME,B.BRANCHNO,B.LONGNAME,SC.SCHEMENAME,S.PTYPE,S.ADMBATCH,AM.BATCHNAME,S.DEGREENO,S.PHYSICALLY_HANDICAPPED AS PH", "S.IDNO = " + idno, string.Empty);
            if (dsStudent != null && dsStudent.Tables.Count > 0)
            {
                if (dsStudent.Tables[0].Rows.Count > 0)
                {
                    imgPhoto.ImageUrl = "~/showimage.aspx?id=" + dsStudent.Tables[0].Rows[0]["IDNO"].ToString() + "&type=STUDENT";

                    //Show Student Details..
                    lblName.Text = dsStudent.Tables[0].Rows[0]["STUDNAME"].ToString();
                    lblName.ToolTip = dsStudent.Tables[0].Rows[0]["IDNO"].ToString();

                    lblFatherName.Text = dsStudent.Tables[0].Rows[0]["FATHERNAME"].ToString();
                    lblMotherName.Text = dsStudent.Tables[0].Rows[0]["MOTHERNAME"].ToString();

                    lblEnrollNo.Text = dsStudent.Tables[0].Rows[0]["REGNO"].ToString();
                    lblBranch.Text = dsStudent.Tables[0].Rows[0]["DEGREENAME"].ToString() + " / " + dsStudent.Tables[0].Rows[0]["LONGNAME"].ToString();
                    lblBranch.ToolTip = dsStudent.Tables[0].Rows[0]["BRANCHNO"].ToString();
                    lblScheme.Text = dsStudent.Tables[0].Rows[0]["SCHEMENAME"].ToString();
                    lblScheme.ToolTip = dsStudent.Tables[0].Rows[0]["SCHEMENO"].ToString();
                    lblSemester.Text = dsStudent.Tables[0].Rows[0]["SEMESTERNAME"].ToString();
                    lblSemester.ToolTip = dsStudent.Tables[0].Rows[0]["SEMESTERNO"].ToString();

                    lblAdmBatch.Text = dsStudent.Tables[0].Rows[0]["BATCHNAME"].ToString();
                    lblAdmBatch.ToolTip = dsStudent.Tables[0].Rows[0]["ADMBATCH"].ToString();

                    //physically hadicapped
                    lblPH.Text = dsStudent.Tables[0].Rows[0]["PH"].ToString() == "False" ? "No" : "Yes";

                    tblInfo.Visible = true;

                }

            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }


    protected void SelectDate(object sender, DayRenderEventArgs e)
    {
        if (e.Day.IsWeekend)
        {
            e.Day.IsSelectable = false;
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //Shows report
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_SESSIONNO=" + ddlSession.SelectedValue + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString();
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";


            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");

            ScriptManager.RegisterClientScriptBlock(this.updHoliday, this.updHoliday.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #endregion

    #region Event Methods


    protected void btnSubmit_Click(object sender, EventArgs e)
     {
        //Single student leave apply
        try
        {
            string blob_ContainerName = "";
            string blob_ConStr = "";
            if (System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"] != null)
            {
                if (System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_OD"] != null)
                {
                    blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_OD"].ToString();
                    blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
                }
                else
                {
                    objCommon.DisplayUserMessage(this.updHoliday, "Something went wrong, Blob Storage container related details not found.", this.Page);
                    return;
                }
            }
            else
            {
                objCommon.DisplayUserMessage(this.updHoliday, "Something went wrong, Blob Storage container related details not found.", this.Page);
                return;
            }
            string slotno = string.Empty, idno = string.Empty, regno = string.Empty;
            int odType = 1;

            if (txtFromDate.Text != string.Empty)// | txtToDate.Text != string.Empty)
            {
                hiddenfieldfromDt.Value = txtFromDate.Text;

                #region commented
                // hiddenFieldToDt.Value = txtToDate.Text;
                //if (ChkDate.Checked)
                //{
                //    //if ((Convert.ToDateTime(txtFromDate.Text) < Convert.ToDateTime(hiddenfieldfromDt.Value)) | (Convert.ToDateTime(txtToDate.Text) < Convert.ToDateTime(hiddenfieldfromDt.Value) | (Convert.ToDateTime(txtToDate.Text) > Convert.ToDateTime(hiddenFieldToDt.Value)) | (Convert.ToDateTime(txtToDate.Text) < Convert.ToDateTime(hiddenfieldfromDt.Value))))
                //    //{
                //    //    objCommon.DisplayMessage(updHoliday, "Select Date in Proper Range", this.Page);
                //    //    return;
                //    //}
                //    //if (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(txtToDate.Text))
                //    //{
                //    //    objCommon.DisplayMessage(updHoliday, "From Date should be Lesser than To Date", this.Page);
                //    //    return;
                //    //}
                //}
                //else
                //{
                #endregion commented

                if ((Convert.ToDateTime(txtFromDate.Text) < Convert.ToDateTime(hiddenfieldfromDt.Value)) | (Convert.ToDateTime(txtFromDate.Text) > Convert.ToDateTime(hiddenfieldfromDt.Value)))
                {
                    
                    objCommon.DisplayMessage(updHoliday, "Select Date In Proper Range", this.Page);
                    return;
                }
                // }
                if((Convert.ToDateTime(txtFromDate.Text))>(Convert.ToDateTime(txtToDate.Text)))
                {
                     objCommon.DisplayMessage(updHoliday, "To date should be greater than OD date", this.Page);
                     txtToDate.Text = "";
                    return;
                }


                objAttModel.LeaveStartDate = Convert.ToDateTime(txtFromDate.Text);
                if (ddlOdType.SelectedValue == "2")
                    objAttModel.LeaveEndDate = Convert.ToDateTime(txtToDate.Text);
                else
                    objAttModel.LeaveEndDate = Convert.ToDateTime(txtFromDate.Text);
                objAttModel.College_code = Session["colcode"].ToString();//
                objAttModel.Event_Detail = txtEventDetail.Text;
                objAttModel.LEAVENO = Convert.ToInt32(ddlLeaveName.SelectedValue);
                idno = lblName.ToolTip;
                regno = lblEnrollNo.Text.Trim();
                objAttModel.UA_NO_TRAN = Convert.ToInt32(Session["userno"]);

                for (int i = 0; i < chkSlots.Items.Count; i++)
                {
                    if (chkSlots.Items[i].Selected == true)
                        slotno += chkSlots.Items[i].Value + ',';
                }

                if (!string.IsNullOrEmpty(slotno))
                    slotno = slotno.Substring(0, slotno.Length - 1);

                if (ddlOdType.SelectedValue == "1")//if NORMAL OD then slots selection mandatory. 
                {
                    if (slotno == string.Empty)
                    {
                        objCommon.DisplayMessage(updHoliday, "Please Select Atleast One Slot!", this.Page);
                        return;
                    }
                }
                odType = Convert.ToInt32(ddlOdType.SelectedValue);
                if (fuDoc.HasFile)
                {
                    string contentType = contentType = fuDoc.PostedFile.ContentType;
                    string ext = System.IO.Path.GetExtension(fuDoc.PostedFile.FileName);
                    string userno = Session["userno"].ToString();
                   // string Leavtype = ddlLeaveName.SelectedItem.Text;
                    string OTP = GenerateOTP();
                    if (ext == ".pdf")
                    {
                        HttpPostedFile file = fuDoc.PostedFile;
                        string filename = userno + "_ODLEAVE_" + OTP ;
                        ViewState["filename"] = filename + ext;
                        int fileSize = fuDoc.PostedFile.ContentLength;
                        int KB = fileSize / 1024;
                        if (KB <= 150)
                        {
                            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
                            {
                                int retval = Blob_Upload(blob_ConStr, blob_ContainerName, userno + "_ODLEAVE_" + OTP  , fuDoc);
                                if (retval == 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                    return;
                                }
                            }
                            else
                            {
                                if (CheckDuplicateEntry() == true)
                                {
                                    objCommon.DisplayMessage(updHoliday, "Entry For This Date Already Done!", this.Page);
                                    return;
                                }
                                int retval = Blob_Upload(blob_ConStr, blob_ContainerName, userno + "_ODLEAVE_" + OTP   , fuDoc);
                                if (retval == 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Unable to upload...Please try again...');", true);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.Page, "Please Upload file Below or Equal to 150 kb only !", this.Page);
                            return;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage(this.Page, "Please Upload file with .pdf format only.", this.Page);
                        return;
                    }
                }
                //Check for add or edit
                if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
                {

                    objAttModel.Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                    objAttModel.Holiday_No = Convert.ToInt32(ViewState["holidayno"]);//Holiday no.
                    string filename = string.Empty;
                    if (ViewState["filename"] == null)
                    {
                        if (lblFileName.Text == "")
                        {
                            filename = string.Empty;
                        }
                        else
                        {
                            filename = lblFileName.Text;
                        }
                    }
                    else
                    {
                        filename = ViewState["filename"].ToString();
                    }
                    CustomStatus cs = (CustomStatus)objAttC.UpdateLeaveDetails(objAttModel, idno, regno, slotno, odType, filename);
                    if (cs.Equals(CustomStatus.RecordUpdated))
                    {
                        BindListView();
                        ClearControls();
                        objCommon.DisplayMessage(updHoliday, "Record Updated Successfully!", this.Page);
                    }
                    else
                    {
                        BindListView();
                        ClearControls();
                        objCommon.DisplayMessage(updHoliday, "Record Already Exists!", this.Page);
                    }
                }
                else
                {
                    if (CheckDuplicateEntry() == true)
                    {
                        objCommon.DisplayMessage(updHoliday, "Entry For This Date Already Done!", this.Page);
                        return;
                    }

                    objAttModel.Sessionno = Convert.ToInt32(ddlSession.SelectedValue);
                    string filename = string.Empty;
                    if (ViewState["filename"] == null)
                    {
                        filename = string.Empty;
                    }
                    else
                    {
                        filename = ViewState["filename"].ToString();
                    }
                    //Add New
                    CustomStatus cs = (CustomStatus)objAttC.AddLeaveDetails(objAttModel, idno, regno, slotno, odType,filename);
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        BindListView();
                        ClearControls();
                        objCommon.DisplayMessage(updHoliday, "Record Saved Successfully!", this.Page);
                    }
                    else if (cs.Equals(CustomStatus.TransactionFailed))
                    {
                        objCommon.DisplayMessage(updHoliday, "Transaction Failed", this.Page);
                    }
                }
            }
            else
            {
                objCommon.DisplayMessage(updHoliday, "Please Enter Date", this.Page);
                return;
            }
            this.BindListView();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private string GenerateOTP()
    {
        string allowedChars = "";

        allowedChars += "1,2,3,4,5,6,7,8,9,0"; //,!,@,#,$,%,&,?
        //--------------------------------------
        char[] sep = { ',' };

        string[] arr = allowedChars.Split(sep);

        string otpString = "";

        string temp = "";

        Random rand = new Random();

        for (int i = 0; i < 6; i++)
        {
            temp = arr[rand.Next(0, arr.Length)];
            otpString += temp;
        }
        return otpString;
    }
    public int Blob_Upload(string ConStr, string ContainerName, string DocName, FileUpload FU)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        int retval = 1;
        string Ext = System.IO.Path.GetExtension(FU.FileName);
        string FileName = DocName + Ext;
        try
        {
            DeleteIFExits(FileName);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });

            CloudBlockBlob cblob = container.GetBlockBlobReference(FileName);
            cblob.UploadFromStream(FU.PostedFile.InputStream);
        }
        catch
        {
            retval = 0;
            return retval;
        }
        return retval;
    }
    private CloudBlobContainer Blob_Connection(string ConStr, string ContainerName)
    {
        CloudStorageAccount account = CloudStorageAccount.Parse(ConStr);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(ContainerName);
        return container;
    }
    public void DeleteIFExits(string FileName)
    {
        string blob_ContainerName = "";
        string blob_ConStr = "";
        if (System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"] != null)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_OD"] != null)
            {
                blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_OD"].ToString();
                blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
            }
            else
            {
                objCommon.DisplayUserMessage(this.updHoliday, "Something went wrong, Blob Storage container related details not found.", this.Page);
                return;
            }
        }
        else
        {
            objCommon.DisplayUserMessage(this.updHoliday, "Something went wrong, Blob Storage container related details not found.", this.Page);
            return;
        }
        CloudBlobContainer container = Blob_Connection(blob_ConStr, blob_ContainerName);
        string FN = System.IO.Path.GetFileNameWithoutExtension(FileName);
        try
        {
            System.Threading.Tasks.Parallel.ForEach(container.ListBlobs(FN,true),y=>
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                ((CloudBlockBlob)y).DeleteIfExists();
            });
        }
        catch (Exception) { }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int holidayno = int.Parse(btnEdit.CommandArgument);
            ViewState["holidayno"] = int.Parse(btnEdit.CommandArgument);
            ViewState["action"] = "edit";
            this.ShowDetails(holidayno);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            SessionController objsessionn = new SessionController();
            ImageButton btnDelete = sender as ImageButton;
            int Sessionno = int.Parse(btnDelete.CommandArgument);
            objCommon.DisplayMessage(updHoliday, "Holiday Entry Deleted Successfully !!", this.Page);
            this.BindListView();
            this.ClearControls();
            return;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void ddlSession_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.BindListView();
    }
    protected void dpPager_PreRender(object sender, EventArgs e)
    {
        this.BindListView();
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("rptSpecialLeave", "rptSpecialLeave.rpt");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnDeptwise_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("rptSpecialLeave", "rptSpecialLeaveDeptWise.rpt");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    protected void lvStudents_ItemDataBound(object sender, ListViewItemEventArgs e)
    {

    }

    #endregion


    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        DataSet ds = null;
        //int idno = lblName.ToolTip != string.Empty ? Convert.ToInt32(lblName.ToolTip) : 0;
        ds = objAttC.GetSelectedDateSlots(Convert.ToInt32(ddlSession.SelectedValue), Convert.ToInt32(lblName.ToolTip), Convert.ToDateTime(txtFromDate.Text));

        if (ddlOdType.SelectedValue == "1")//Normal OD
        {
            txtToDate.Text = txtFromDate.Text;
            txtToDate.Enabled = false;
            if (ds.Tables[0].Rows.Count > 0)
            {
                chkSlots.DataSource = ds;
                chkSlots.DataTextField = "SLOTTIME";
                chkSlots.DataValueField = "SLOTNO";
                chkSlots.DataBind();
                chkSlots.Visible = true;
                chkCheckAll.Visible = true;
                chkCheckAll.Enabled = true;
            }
            else
            {
                objCommon.DisplayMessage(updHoliday, "No Slots Available For Selected Date.", this);
                chkSlots.DataSource = null;
                chkSlots.DataBind();
                chkSlots.Visible = false;
                chkCheckAll.Visible = false;
                chkCheckAll.Enabled = false;
            }
        }
        else //Special  OD
        {
            txtToDate.Enabled = true;
            // txtToDate.Text = string.Empty;
            chkCheckAll.Enabled = false;
        }
    }
    protected void chkCheckAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCheckAll.Checked == true)
        {
            for (int i = 0; i < chkSlots.Items.Count; i++)
            {
                chkSlots.Items[i].Selected = true;
            }
        }
        else
        {
            for (int i = 0; i < chkSlots.Items.Count; i++)
            {
                chkSlots.Items[i].Selected = false;
            }
        }
    }
    protected void ddlOdType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlOdType.SelectedValue == "1")//Normal OD
        {
            txtToDate.Enabled = false;
            txtToDate.Text = string.Empty;
            txtFromDate.Text = string.Empty;
            chkSlots.Items.Clear();
        }
        else //Special  OD
        {
            txtToDate.Enabled = true;
            txtToDate.Text = string.Empty;
            txtFromDate.Text = string.Empty;
            chkSlots.Items.Clear();
        }
    }
    protected void lvExamday_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            ListViewDataItem item = (ListViewDataItem)e.Item;
            string status = (string)DataBinder.Eval(item.DataItem, "APPROVAL_STATUS");

            if (status == "PENDING")
            {
                (e.Item.FindControl("lblAStatus") as Label).ForeColor = System.Drawing.Color.SandyBrown;
            }
            else if (status == "APPROVED")
            {
                (e.Item.FindControl("lblAStatus") as Label).ForeColor = System.Drawing.Color.Green;
            }
            else if (status == "REJECTED")
            {
                (e.Item.FindControl("lblAStatus") as Label).ForeColor = System.Drawing.Color.Red;
            }
        }
    }
    public DataTable Blob_GetById(string ConStr, string ContainerName, string Id)
    {
        CloudBlobContainer container = Blob_Connection(ConStr, ContainerName);
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        var permission = container.GetPermissions();
        permission.PublicAccess = BlobContainerPublicAccessType.Container;
        container.SetPermissions(permission);

        DataTable dt = new DataTable();
        dt.TableName = "FilteredBolb";
        dt.Columns.Add("Name");
        dt.Columns.Add("Uri");

        //var blobList = container.ListBlobs(useFlatBlobListing: true);
        var blobList = container.ListBlobs(Id, true);
        foreach (var blob in blobList)
        {
            string x = (blob.Uri.ToString().Split('/')[blob.Uri.ToString().Split('/').Length - 1]);
            string y = x.Split('_')[0];
            dt.Rows.Add(x, blob.Uri);
        }
        return dt;
    }

    protected void imgbtnpfPrevDoc_Click(object sender, ImageClickEventArgs e)
    {
        string Url = string.Empty;
        string directoryPath = string.Empty;

        string blob_ContainerName = "";
        string blob_ConStr = "";
        if (System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"] != null)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_OD"] != null)
            {
                blob_ContainerName = System.Configuration.ConfigurationManager.AppSettings["Blob_ContainerName_OD"].ToString();
                blob_ConStr = System.Configuration.ConfigurationManager.AppSettings["Blob_ConnectionString"].ToString();
            }
            else
            {
                objCommon.DisplayUserMessage(this.updHoliday, "Something went wrong, Blob Storage container related details not found.", this.Page);
                return;
            }
        }
        else
        {
            objCommon.DisplayUserMessage(this.updHoliday, "Something went wrong, Blob Storage container related details not found.", this.Page);
            return;
        }

        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blob_ConStr);
        CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
        string FileName = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
        string directoryName = "~/ODAPPLYDOCUMENT" + "/";
        directoryPath = Server.MapPath(directoryName);

        if (!Directory.Exists(directoryPath.ToString()))
        {

            Directory.CreateDirectory(directoryPath.ToString());
        }
        CloudBlobContainer blobContainer = cloudBlobClient.GetContainerReference(blob_ContainerName);
        string doc = ((System.Web.UI.WebControls.ImageButton)(sender)).ToolTip.ToString();
        var Document = doc;
        string extension = Path.GetExtension(doc.ToString());
        if (doc == null || doc == "")
        {
            objCommon.DisplayMessage(this.Page, "Please Upload file Below or Equal to 150 kb only !", this.Page);
            return;
        }
        else
        {
            if (extension == ".pdf")
            {
                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, doc);
                var Newblob = blobContainer.GetBlockBlobReference(Document);
                string filePath = directoryPath + "\\" + Document;
                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }
                Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                Response.Clear();
                Response.ClearHeaders();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                Response.TransmitFile(filePath);
                Response.Flush();
                Response.End();
            }
            else
            {
                DataTable dtBlobPic = Blob_GetById(blob_ConStr, blob_ContainerName, doc);
                var Newblob = blobContainer.GetBlockBlobReference(Document);
                string filePath = directoryPath + "\\" + Document;
                if ((System.IO.File.Exists(filePath)))
                {
                    System.IO.File.Delete(filePath);
                }
                Newblob.DownloadToFile(filePath, System.IO.FileMode.CreateNew);
                Response.Clear();
                Response.ClearHeaders();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                Response.TransmitFile(filePath);
                //Response.Flush();
                Response.End();
            }
        }
    }
}
