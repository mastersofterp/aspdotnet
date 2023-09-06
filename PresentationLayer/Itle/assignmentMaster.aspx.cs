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
using System.Collections.Generic;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Net.Mail;
using IITMS.NITPRM;


public partial class Itle_assignmentMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();

    AssignmentController objAM = new AssignmentController();
    Assignment objAssign = new Assignment();
    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
    string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"];

    static decimal File_size;
    string PageId;

    #region Page Load

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

        if (!Page.IsPostBack)
        {
            //Check page refresh
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());

            //Check Session
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            else
            {
                //Check CourseNo in Session variable,if null then redirect to SelectCourse page. 
                if (Session["ICourseNo"] == null)
                {
                    Response.Redirect("~/Itle/selectCourse.aspx?pageno=1445");
                }

                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title


                Page.Title = Session["coll_name"].ToString();
                lblSession.Text = Session["SESSION_NAME"].ToString();
                lblSession.ToolTip = Session["SessionNo"].ToString();
                lblCourseName.Text = Session["ICourseName"].ToString();
                lblCurrdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                lblCurrdate.ForeColor = System.Drawing.Color.Green;

                lblSession.ForeColor = System.Drawing.Color.Green;
                lblCourseName.ForeColor = System.Drawing.Color.Green;

                if (ViewState["action"] == null)
                    ViewState["action"] = "add";

                // PageId = Request.QueryString["pageno"];


            }
            BindListView();
            BindMonitorInfo();
            GetAttachmentSize();
            Session["Attachments"] = null;
        }
        // Used to get maximum size of file attachment

    }

    #endregion

    #region Private Method

    private void CheckPageRefresh()
    {
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {

            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
            //objCommon.DisplayMessage(UpdAssignment, "Enter valid assignment submission date", this.Page);
        }
        else
        {
            Response.Redirect("assignmentMaster.aspx");
        }

    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=assignmentMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=assignmentMaster.aspx");
        }
    }

    private void BindListView()
    {
        try
        {
            AssignmentController objAM = new AssignmentController();
            DataSet ds = objAM.GetAllAssignmentListByUaNo(Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["ICourseNo"]), Convert.ToInt32(Session["userno"]), Convert.ToInt32(Session["OrgId"]));
            lvAssignment.DataSource = ds;
            lvAssignment.DataBind();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void BindMonitorInfo()
    {
        //DataSet ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_STUDENT_RESULT R ON (R.IDNO=S.IDNO)", "DISTINCT S.STUDNAME,S.STUDENTMOBILE", "S.IDNO","R.COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + "", "STUDNAME");
        DataSet ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_STUDENT_RESULT R ON (R.IDNO=S.IDNO) INNER JOIN ACD_SECTION SEC ON (S.SECTIONNO = SEC.SECTIONNO)", "DISTINCT S.STUDNAME,SEC.SECTIONNAME,(CASE WHEN S.STUDENTMOBILE IS NULL THEN 'N/A' ELSE S.STUDENTMOBILE END ) AS STUDENTMOBILE,(CASE WHEN S.EMAILID IS NULL THEN 'N/A' ELSE S.EMAILID END ) AS  EMAILID,S.ROLLNO", "S.IDNO", "R.COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + " AND R.SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + "", " S.STUDNAME ASC");


        lvStudent.DataSource = ds;
        lvStudent.DataBind();
    }

    private DataTable GetAttachmentDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("ATTACH_ID", typeof(int)));
        dt.Columns.Add(new DataColumn("FILE_NAME", typeof(string)));
        dt.Columns.Add(new DataColumn("FILE_PATH", typeof(string)));
        dt.Columns.Add(new DataColumn("SIZE", typeof(int)));
        return dt;
    }

    private DataRow GetDeletableDataRow(DataTable dt, string value)
    {
        DataRow dataRow = null;
        try
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ATTACH_ID"].ToString() == value)
                {
                    dataRow = dr;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_FeeCollection.GetEditableDataRow() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        return dataRow;
    }

    private void BindListView_Attachments(DataTable dt)
    {
        try
        {
            divAttch.Style["display"] = "block";
            lvCompAttach.DataSource = dt;
            lvCompAttach.DataBind();


        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_FeeCollection.BindListView_DemandDraftDetails() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void GetAttachmentSize()
    {

        try
        {

            PageId = Request.QueryString["pageno"];

            if (Convert.ToInt32(Session["usertype"]) == 1)
            {

                File_size = Convert.ToDecimal(objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "FILE_SIZE_ADMIN", "PAGE_ID=" + PageId));
                //lblFileSize.Text = objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "dbo.udf_FormatBytes(FILE_SIZE_ADMIN,'Bytes')AS FILE_SIZE_ADMIN", "PAGE_ID=" + PageId);
            }
            else

                if (Convert.ToInt32(Session["usertype"]) == 2)
                {
                    File_size = Convert.ToDecimal(objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "FILE_SIZE_STUDENT", "PAGE_ID=" + PageId));
                    // lblFileSize.Text = objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "dbo.udf_FormatBytes(FILE_SIZE_STUDENT,'Bytes')AS FILE_SIZE_STUDENT", "PAGE_ID=" + PageId);
                }

                else if (Convert.ToInt32(Session["usertype"]) == 3)
                {
                    File_size = Convert.ToDecimal(objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "FILE_SIZE_FACULTY", "PAGE_ID=" + PageId));

                    lblFileSize.Text = objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "dbo.udf_FormatBytes(FILE_SIZE_FACULTY,'Bytes')AS FILE_SIZE_FACULTY", "PAGE_ID=" + PageId);

                }



        }
        catch (Exception ex)
        {

        }

    }

    private void ClearControls()
    {
        try
        {
            txtTopic.Text = string.Empty;
            txtDescription.Text = "&nbsp;";
            lblStatus.Text = string.Empty;
            txtSubmitDate.Text = string.Empty;
            lblCurrdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblSession.Text = Session["SESSION_NAME"].ToString();
            txtAMarks.Text = string.Empty;
            txtLastTimeOfSubmission.Text = string.Empty;
            ViewState["action"] = null;
            //chkSendSMS.Checked  = false;        
            lblSession.ToolTip = Session["SessionNo"].ToString();
            Session["Attachments"] = null;


            // for Deleting all rows from datatable at runtime

            DataTable dt = null;
            lvCompAttach.DataSource = string.Empty;
            lvCompAttach.DataBind();

            //lvStudent.DataSource = null;
            //lvStudent.DataBind();


            //dt = ((DataTable)Session["Attachments"]);
            //dt.Rows.Clear();
            //dt.Clear();

            BindListView_Attachments(dt);
            //foreach (DataRow row in dt.Rows)
            //{
            //    row.Delete();
            //}
            // TableAdapter.Update(dt);
            // End
        }
        catch (Exception ex)
        {
        }

    }

    protected void Uncheck()
    {
        foreach (ListViewDataItem lsvdata in lvStudent.Items)
        {
            CheckBox chkitem = lsvdata.FindControl("chkStud") as CheckBox;
            chkitem.Checked = false;
            cbHead.Checked = false;
        }
    }

    private void ShowDetail(int assignno, int courseno, int sessionno, int ua_no)
    {
        try
        {
            AssignmentController objAM = new AssignmentController();
            ViewState["asno"] = assignno;
            //used to access attachments
            DataSet ds = objAM.GetAllAtachmentByAsNo(Convert.ToInt32(ViewState["asno"]), courseno, sessionno, ua_no, Convert.ToInt32(Session["OrgId"]));
            DataTable dt = new DataTable();
            //int totFiles = ds.Tables[0].Rows.Count;
            // if (ds.Tables[0].Rows.Count > 0)                                 
            //{
            dt = this.GetAttachmentDataTable();
            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
            {
                //dt = this.GetAttachmentDataTable();
                DataRow dr = dt.NewRow();
                dr["ATTACH_ID"] = ds.Tables[0].Rows[j]["ATTACHMENT_ID"];
                //string fileName = ds.Tables[0].Rows[j]["FILE_NAME"].ToString();
                dr["FILE_NAME"] = ds.Tables[0].Rows[j]["FILE_NAME"].ToString();
                dr["FILE_PATH"] = ds.Tables[0].Rows[j]["FILE_PATH"].ToString();
                dr["SIZE"] = ds.Tables[0].Rows[j]["SIZE"];
                dt.Rows.Add(dr);
                Session["Attachments"] = dt;
                this.BindListView_Attachments(dt);
            }


            divAttch.Style["display"] = "block";
            lvCompAttach.DataSource = dt;
            lvCompAttach.DataBind();


            DataTableReader dtr = objAM.GetSingleAssignment(Convert.ToInt32(ViewState["asno"]), courseno, sessionno, ua_no,Convert.ToInt32(Session["OrgId"]));

            //Show Assignment Details
            if (dtr != null)
            {
                if (dtr.Read())
                {
                    //ViewState["assignno"] = int.Parse(dtr["AS_NO"].ToString());
                    txtTopic.Text = dtr["SUBJECT"] == null ? "" : dtr["SUBJECT"].ToString();
                    txtDescription.Text = dtr["DESCRIPTION"] == null ? "" : dtr["DESCRIPTION"].ToString();
                    txtSubmitDate.Text = dtr["SUBMITDATE"] == null ? "" : Convert.ToDateTime(dtr["SUBMITDATE"].ToString()).ToString("dd/MM/yyyy");
                    //hdnFile.Value = dtr["ATTACHMENT"] == null ? "" : dtr["ATTACHMENT"].ToString();
                    lblCurrdate.Text = dtr["ASSIGNDATE"] == null ? "" : Convert.ToDateTime(dtr["ASSIGNDATE"].ToString()).ToString("dd-MMM-yyyy");
                    txtAMarks.Text = dtr["ASSIGNMENT_MARKS"] == null ? "" : dtr["ASSIGNMENT_MARKS"].ToString();
                    txtLastTimeOfSubmission.Text = Convert.ToDateTime(dtr["SUBMITDATE"].ToString().Trim()).ToString("HH:mm:ss");


                }
                if (dtr != null) dtr.Close();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.ShowDetail -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("itle")));
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Itle")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ITLE," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + ",@P_COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + ",@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]);
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);

            //COURSENAME=" + Session["ICourseName"].ToString() + ",username=" + Session["username"].ToString() + ",SESSIONNAME=" + Session["SESSION_NAME"].ToString() + ",
        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(Page, "assignmentMaster.ShowReport->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

    #endregion

    #region Page Events

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["CheckRefresh"] = Session["CheckRefresh"];
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Uncheck();
        //  Response.Redirect("assignmentMaster.aspx");
        ClearControls();
        Session["Attachments"] = null;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //CheckPageRefresh();

        if (Convert.ToDateTime(txtSubmitDate.Text) < DateTime.Now)
        {
            objCommon.DisplayMessage(UpdAssignment, "Assignment submission date should be greater than create date", this.Page);
            return;
        }

        try
        {
            if (lvStudent != null)
            {
                string idno = string.Empty;
                string mailTo = string.Empty;
                string mailSubject = string.Empty;


                //Get the id's of the student to whom assignment is given
                foreach (ListViewDataItem dti in lvStudent.Items)
                {
                    CheckBox chkSel = dti.FindControl("chkStud") as CheckBox;
                    Label lblmailTo = dti.FindControl("lblMailTo") as Label;

                    if (chkSel.Checked)
                    {

                        if (idno.Equals(string.Empty))
                        {
                            idno = chkSel.ToolTip;
                        }
                        else
                        {
                            idno = idno + "," + chkSel.ToolTip;
                        }

                        // By Zubair. Dated: 06-02-2015
                        // Get studet Email Id for sending Email
                        if (mailTo.Equals(string.Empty))
                        {
                            mailTo = lblmailTo.Text.Trim();
                        }
                        else
                        {
                            mailTo = mailTo + "," + lblmailTo.Text.Trim();
                        }
                    }
                }
                if (idno.Equals(string.Empty))
                {
                    objCommon.DisplayMessage("Please Select Atleast one Student", this.Page);
                    return;
                }
                txtSubmitDate.Text = Request.Form[txtSubmitDate.UniqueID];
                //txtLastTimeOfSubmission.Text=Request.Form[txtLastTimeOfSubmission.UniqueID];

                if (Convert.ToDateTime(txtSubmitDate.Text.Trim()) <= DateTime.Today)
                {
                    if (Convert.ToDateTime(txtSubmitDate.Text.Trim()) == DateTime.Today)
                    {
                        if (Convert.ToDateTime(txtLastTimeOfSubmission.Text.Trim()).TimeOfDay <= DateTime.Now.TimeOfDay)
                        {

                            //lblStatus.Text = "Assignment Submission Date and Time Must Be Greater than Current Date and Time";
                            objCommon.DisplayMessage("Assignment Submission Time Must Be Greater than Current Time", this.Page);
                            return;
                        }
                    }
                    else
                    {
                        objCommon.DisplayMessage("Assignment Submission Date and Time Must Be Greater than Current Date and Time", this.Page);
                        return;
                    }
                }


                string filename = string.Empty;

                //if (Request.QueryString["cno"] != null)
                //    Session["CourseNo"] = Request.QueryString["cno"].ToString();

                List<AssignmentAttachment> attachments = new List<AssignmentAttachment>();

                objAssign.UA_NO = Convert.ToInt32(Session["userno"].ToString());
                objAssign.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
                objAssign.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
                objAssign.SUBJECT = txtTopic.Text;
                objAssign.DESCRIPTION = txtDescription.Text.Trim();
                objAssign.ASSIGNMENT_TOALMARKS = Convert.ToInt32(txtAMarks.Text);
                objAssign.ASSIGNDATE = Convert.ToDateTime(lblCurrdate.Text.Trim());

                //by Zubair
                //objAssign.SUBMITDATE = Convert.ToDateTime(txtSubmitDate.Text.Trim());
                objAssign.SUBMITDATE = Convert.ToDateTime(Convert.ToDateTime(txtSubmitDate.Text).ToString("dd/MM/yyyy") + " " + txtLastTimeOfSubmission.Text.Trim());
                objAssign.STATUS = '1';
                //objAssign.ATTACHMENT = fuAssign.FileName;
                objAssign.COLLEGE_CODE = Session["colcode"].ToString();

                if (Session["Attachments"] != null && ((DataTable)Session["Attachments"]) != null)
                {
                    DataTable dt = ((DataTable)Session["Attachments"]);
                    foreach (DataRow dr in dt.Rows)
                    {
                        AssignmentAttachment attach = new AssignmentAttachment();
                        attach.AttachmentId = Convert.ToInt32(dr["ATTACH_ID"]);
                        attach.FileName = dr["FILE_NAME"].ToString();
                        attach.FilePath = dr["FILE_PATH"].ToString();
                        attach.Size = Convert.ToInt32(dr["SIZE"]);
                        attachments.Add(attach);
                    }
                }
                objAssign.Attachments = attachments;

                if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
                {
                    //Edit Assignment
                    if (ViewState["asno"] != null)
                    {

                        //Addtional property 
                        objAssign.AS_NO = Convert.ToInt32(ViewState["asno"]);
                        objAssign.UA_NO = Convert.ToInt32(Session["userno"].ToString());
                        objAssign.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
                        objAssign.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
                        objAssign.SUBJECT = txtTopic.Text;
                        objAssign.DESCRIPTION = txtDescription.Text.Trim();
                        objAssign.ASSIGNDATE = Convert.ToDateTime(lblCurrdate.Text.Trim());
                        //objAssign.SUBMITDATE = Convert.ToDateTime(txtSubmitDate.Text.Trim());
                        objAssign.SUBMITDATE = Convert.ToDateTime(Convert.ToDateTime(txtSubmitDate.Text).ToString("dd/MM/yyyy") + " " + txtLastTimeOfSubmission.Text.Trim());
                        objAssign.STATUS = '1';

                        AssignmentAttachment attach = new AssignmentAttachment();
                        attach.Assignment_NO = Convert.ToInt32(ViewState["asno"]);

                        //objAssign.OLDFILENAME = hdnFile.Value;

                        //    if (hdnFile.Value !="" && hdnFile.Value !=null && fuAssign.HasFile==false)
                        //    {
                        //        objAssign.ATTACHMENT = hdnFile.Value;
                        //    }

                        CustomStatus cs = (CustomStatus)objAM.UpdateAssignment(objAssign, idno, Convert.ToInt32(ViewState["asno"]), Convert.ToInt32(Session["OrgId"]));
                        if (cs.Equals(CustomStatus.RecordUpdated))
                        {
                            //mailSubject = "Assignment has been modified. Please verify.";
                            //sendmail(mailSubject, mailTo, Session["ICourseName"].ToString(), Session["userfullname"].ToString(), Convert.ToDateTime(objAssign.SUBMITDATE));


                            //lblStatus.Text = "Assignment Modified";
                            //Response.Redirect(Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&action")));
                            //lvCompAttach.DataSource = null;
                            //lvCompAttach.DataBind();                           
                            objCommon.DisplayMessage(UpdAssignment, "Assignment Modified Successfully", this.Page);
                            BindListView();
                            ClearControls();
                        }

                        else if (cs.Equals(CustomStatus.DuplicateRecord))
                        {
                            objCommon.DisplayMessage(UpdAssignment, "Assignment Already Exist", this.Page);
                            //lvCompAttach.DataSource = null;
                            //lvCompAttach.DataBind();
                            BindListView();
                            ClearControls();
                        }
                        else
                            if (cs.Equals(CustomStatus.FileExists))
                            {
                                lblStatus.Text = "File already exists. Please upload another file or rename and upload.";
                            }
                    }
                    ViewState["action"] = "add";
                }
                else
                {  //Add Assignment 

                    CustomStatus cs = (CustomStatus)objAM.AddAssignment(objAssign, idno, Convert.ToInt32(Session["OrgId"]));
                    if (cs.Equals(CustomStatus.RecordSaved))
                    {
                        //mailSubject = "New assignment has been created for you. Please verify.";
                        //sendmail(mailSubject, mailTo, Session["ICourseName"].ToString(), Session["userfullname"].ToString(), Convert.ToDateTime(objAssign.SUBMITDATE));

                        objCommon.DisplayMessage(UpdAssignment, "Assignment Added Successfully", this.Page);
                        // Response.Redirect(Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&action")));
                        BindListView();
                        ClearControls();
                    }

                    else if (cs.Equals(CustomStatus.DuplicateRecord))
                    {
                        objCommon.DisplayMessage(UpdAssignment, "Assignment Already Exist", this.Page);
                        BindListView();
                        ClearControls();
                    }
                    else
                    {
                        if (cs.Equals(CustomStatus.FileExists))
                            lblStatus.Text = "File already exists. Please upload another file or rename and upload.";
                    }
                }
                //THIS CODE IS USED FOR SMS SENDING TO STUDENTS
                //if (chkSendSMS.Checked == true)
                //{
                //    objAssign.AS_NO = Convert.ToInt32(objCommon.LookUp("ACD_IASSIGNMASTER", "MAX(AS_NO)", ""));
                //    CustomStatus cs = (CustomStatus)objAM.SendSMSofAssignment(objAssign, idno);
                //    ClearControls();

                //}

                BindListView();
                ClearControls();
                Uncheck();

                //Response.Redirect("assignmentMaster.aspx");
            }
            BindListView();
            Uncheck();
            ClearControls();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }


    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        DataSet ds = null;
        try
        {
            Uncheck();
            ImageButton btnEdit = sender as ImageButton;
            int assignno = int.Parse(btnEdit.CommandArgument);
            ViewState["assignno"] = assignno;
            ds = objCommon.FillDropDown("ACD_IASSIGNMASTER_STUDENTS", "IDNO, isnull(SMS_STATUS,0)as SMS_STATUS ", "null", "AS_NO=" + Convert.ToString(assignno), "IDNO");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    foreach (ListViewDataItem lsvdata in lvStudent.Items)
                    {

                        CheckBox chkitem = lsvdata.FindControl("chkStud") as CheckBox;

                        //LinkButton lnkResendSms = lsvdata.FindControl("lnkResendSms") as LinkButton;

                        if (chkitem.ToolTip.Equals(ds.Tables[0].Rows[i]["IDNO"].ToString()))
                        {

                            chkitem.Checked = true;
                            //lnkResendSms.Text = Convert.ToInt32(ds.Tables[0].Rows[i]["SMS_STATUS"]) == 1 ? "" : "Resend SMS".ToString();

                        }


                    }
                }
            }
            ShowDetail(assignno, Convert.ToInt32(Session["ICourseNo"]), Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["userno"].ToString()));

            ViewState["action"] = "edit";

            //chkSendSMS.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
        finally
        {
            ds.Dispose();
        }
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            CheckPageRefresh();
            ImageButton btnDel = sender as ImageButton;
            int assignno = int.Parse(btnDel.CommandArgument);

            AssignmentController objAM = new AssignmentController();
            Assignment objAssign = new Assignment();

            objAssign.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
            objAssign.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
            objAssign.UA_NO = Convert.ToInt32(Session["userno"]);
            objAssign.AS_NO = assignno;

            if (Convert.ToInt16(objAM.DeleteAssignment(objAssign)) == Convert.ToInt16(CustomStatus.RecordDeleted))
            {
                objCommon.DisplayMessage(UpdAssignment, "Record Deleted Successfully", this.Page);
                //lblStatus.Text = "Assignment Deleted Successfully...";
                BindListView();
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_assignmentMaster.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnViewAssignment_Click(object sender, EventArgs e)
    {
        try
        {

            ShowReport("Itle_Assignment_Report", "Itle_Assignment_Report.rpt");

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "assignmentMaster.btnViewTeachingPlan_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    protected void btnAttachFile_Click(object sender, EventArgs e)
    {
        try
        {



            if (fileUploader.HasFile)
            {
                string DOCFOLDER = file_path + "ITLE\\upload_files\\Assignment";

                if (!System.IO.Directory.Exists(DOCFOLDER))
                {
                    System.IO.Directory.CreateDirectory(DOCFOLDER);

                }

                string fileName = System.Guid.NewGuid().ToString() + fileUploader.FileName.Substring(fileUploader.FileName.IndexOf('.'));
                string fileExtention = System.IO.Path.GetExtension(fileName);

                int count = Convert.ToInt32(objCommon.LookUp("ACD_IATTACHMENT_FILE_EXTENTIONS", "COUNT(EXTENTION)", "EXTENTION='" + fileExtention.ToString() + "'"));

                if (count != 0)
                {
                    string filePath = file_path + "ITLE\\upload_files\\Assignment\\" + fileName;

                    if (fileUploader.PostedFile.ContentLength < File_size)
                    {
                        fileUploader.SaveAs(filePath);

                    }
                    else
                    {
                        objCommon.DisplayMessage("Unable to upload file. Size of uploaded file is greater than maximum upload size allowed.", this);
                        return;
                    }

                    DataTable dt;
                    //int totFile = dt.Rows.Count;
                    if (Session["Attachments"] != null && ((DataTable)Session["Attachments"]) != null)
                    {
                        dt = ((DataTable)Session["Attachments"]);
                        DataRow dr = dt.NewRow();

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                dr["ATTACH_ID"] = dt.Rows.Count + 1;
                                dr["FILE_NAME"] = fileUploader.FileName;
                                dr["FILE_PATH"] = filePath;
                                dr["SIZE"] = (fileUploader.PostedFile.ContentLength);
                                dt.Rows.Add(dr);
                                Session["Attachments"] = dt;
                                this.BindListView_Attachments(dt);
                            }
                        }
                        else
                        {
                            dt = this.GetAttachmentDataTable();
                            dr = dt.NewRow();
                            dr["ATTACH_ID"] = dt.Rows.Count + 1;
                            dr["FILE_NAME"] = fileUploader.FileName;
                            dr["FILE_PATH"] = filePath;
                            dr["SIZE"] = (fileUploader.PostedFile.ContentLength);
                            dt.Rows.Add(dr);
                            Session.Add("Attachments", dt);
                            this.BindListView_Attachments(dt);
                        }
                    }
                    else
                    {
                        dt = this.GetAttachmentDataTable();
                        DataRow dr = dt.NewRow();
                        dr["ATTACH_ID"] = dt.Rows.Count + 1;
                        dr["FILE_NAME"] = fileUploader.FileName;
                        dr["FILE_PATH"] = filePath;
                        dr["SIZE"] = (fileUploader.PostedFile.ContentLength);
                        dt.Rows.Add(dr);
                        Session.Add("Attachments", dt);
                        this.BindListView_Attachments(dt);
                    }
                }
                else
                {
                    objCommon.DisplayMessage("File Format not supported.", this);
                }
            }
            else
            {
                objCommon.DisplayMessage("Please select a file to attach.", this);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "assignmentMaster.btnAttachFile_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }

    }

    protected void lnkRemoveAttach_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnRemove = sender as LinkButton;

            int fileId = Convert.ToInt32(btnRemove.CommandArgument);

            DataTable dt;
            if (Session["Attachments"] != null && ((DataTable)Session["Attachments"]) != null)
            {
                dt = ((DataTable)Session["Attachments"]);
                dt.Rows.Remove(this.GetDeletableDataRow(dt, Convert.ToString(fileId)));
                Session["Attachments"] = dt;
                this.BindListView_Attachments(dt);
            }

            //to permanently delete from database in case of Edit
            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {
                string count = objCommon.LookUp("ACD_IASSIGNMENT_ATTACHMENTS", "ATTACHMENT_ID", "AS_NO =" + Convert.ToInt32(ViewState["assignno"]) + "AND FACULTY_NO=" + Session["userno"] + "AND ATTACHMENT_ID=" + fileId);
                if (count != "")
                {
                    int cs = objCommon.DeleteClientTableRow("ACD_IASSIGNMENT_ATTACHMENTS", "AS_NO =" + Convert.ToInt32(ViewState["assignno"]) + "AND FACULTY_NO=" + Session["userno"] + "AND ATTACHMENT_ID=" + fileId);
                }
            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_FeeCollection.btnDeleteDDInfo_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void lnkDownload_Click(object sender, EventArgs e)
    {

        LinkButton lnkbtn = sender as LinkButton;

        int an_no = int.Parse(lnkbtn.CommandArgument);
        string fileName = lnkbtn.ToolTip.ToString();
        string filePath = file_path + "Itle/upload_files/Assignment/" + "Assignment_" + Convert.ToInt32(an_no);

        Response.ContentType = "Text/Doc";
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(filePath) + System.IO.Path.GetExtension(fileName));
        Response.WriteFile(filePath + System.IO.Path.GetExtension(fileName));
        Response.End();

    }

    public string GetFileName(object filename, object assingmentno)
    {
        if (filename != null && filename.ToString() != "")
            //return filename.ToString();
            //  return "assignment_" + Convert.ToInt32(assingmentno) + System.IO.Path.GetExtension(filename.ToString());
            return "~/ITLE/upload_files/assignment/assignment_" + Convert.ToInt32(assingmentno) + System.IO.Path.GetExtension(filename.ToString());
        else
            return "None";
    }

    public string GetFileNamePath(object filename)
    {
        if (filename != null && filename.ToString() != "")
            return "~/ITLE/upload_files/assignment/" + filename.ToString();
        else
            return "";
    }

    public string GetStatus(object status)
    {
        DateTime DT = Convert.ToDateTime(status);
        if (Convert.ToDateTime(Convert.ToDateTime(DT).ToString("dd-MMM-yyyy")) <= DateTime.Today)
        {
            if (Convert.ToDateTime(Convert.ToDateTime(DT).ToString("dd-MMM-yyyy")) == DateTime.Today)
            {
                if (Convert.ToDateTime(DT).TimeOfDay < DateTime.Now.TimeOfDay)
                {
                    return "<span style='color:Red'>Expired</span>";
                }
                else
                {
                    return "<span style='color:Green'>Active</span>";
                }
            }
            else
            {
                return "<span style='color:Red'>Expired</span>";
            }
        }
        else
        {
            return "<span style='color:Green'>Active</span>";
        }

    }

    public Boolean checkeEnable(object count)
    {
        if (Convert.ToInt32(count) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion

    #region Send Email

    public void sendmail(string subject, string toEmailId, string course_name, string Faculty_name, DateTime lastDtOfSubmition)
    {
        try
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = subject;
            mailMessage.Body += "Course Name : <b>" + course_name + "</b><br/>";
            mailMessage.Body += "Faculty Name : <b>" + Faculty_name + "</b><br/>";
            mailMessage.Body += "Last Date of submission : <b>" + lastDtOfSubmition.ToString("d") + "</b> Time :<b> " + lastDtOfSubmition.ToString("hh:mm:ss tt") + "</b><br/>";
            mailMessage.Body += "<a  href='http://www.itle.dalmialionscollege.ac.in'>Click here to login E-Learning</a>";



            SmtpClient smtpClient = new SmtpClient();
            smtpClient.EnableSsl = true;


            string[] multi = toEmailId.Split(',');
            foreach (string mutipleemialid in multi)
            {
                mailMessage.To.Add(new MailAddress(mutipleemialid));
            }



            smtpClient.Send(mailMessage);


        }

        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "default.sendmail -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");

        }


    }

    // For Resending SMS to Single Student
    //protected void lnkResendSms_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        LinkButton lnkbutton = lvStudent.FindControl("lnkResendSms") as LinkButton;
    //        string idno = lnkbutton.ToolTip;
    //        objAssign.AS_NO = Convert.ToInt32(ViewState["assignno"]);
    //        objAssign.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
    //        objAssign.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);

    //        CustomStatus cs = (CustomStatus)objAM.SendSMSofAssignment(objAssign, idno);



    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUCommon.ShowError(Page, "Itle_assignmentMaster.lnkResendSms_Click-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //    finally
    //    {

    //    }

    //}

    #endregion
    protected void txtSubmitDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime date = Convert.ToDateTime(txtSubmitDate.Text.ToString());
            DateTime today = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            if (date < today)
            {
                MessageBox("Please select Valid Date");
                txtSubmitDate.Text = "";
                return;

            }
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    public void MessageBox(string msg)
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MSG", "alert('" + msg + "');", true);
    }
}





