using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.NITPRM;

public partial class Itle_AnnouncementMaster : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"].ToString();

    decimal File_size;
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
                   // Response.Redirect("~/Itle/selectCourse.aspx");
                    Response.Redirect("~/Itle/selectCourse.aspx?pageno=1445");

                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                Page.Title = Session["coll_name"].ToString();
               
                if (ViewState["action"] == null)
                    ViewState["action"] = "add";

                // temprary provision for current session using session variable [by defaullt value set 1 in db]
                lblSession.Text = Session["SESSION_NAME"].ToString();
                lblSession.ToolTip = Session["SessionNo"].ToString();
                lblSession.ForeColor = System.Drawing.Color.Green;
                lblCorseName.ForeColor = System.Drawing.Color.Green;
                lblCorseName.Text = Session["ICourseName"].ToString();
                lblCurrdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                lblCurrdate.ForeColor = System.Drawing.Color.Green;
                

                BindListView();
                txtSubject.Focus();
                //GetStatusOnPageLoad();
            }
        }

        // Used to get maximum size of file attachment
        GetAttachmentSize();
        //Check page Refresh
        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //Session["post"] = Convert.ToInt32(Session["post"]) + 1;
        //string[] ctl = Request.Form.GetValues("__EVENTTARGET");

        //if (Convert.ToInt32(Session["post"]) > 1 & ctl == null)
        //    Response.Redirect("AnnouncementMaster.aspx");
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ViewState["CheckRefresh"] = Session["CheckRefresh"];
    }

    #endregion

    #region To Clear FTB

    protected void clearFTB()
    {
        ftbDescription.Text = string.Empty;

    }

    #endregion

    #region Private Method

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(),int.Parse(Session["loginid"].ToString()),0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=AnnouncementMaster.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=AnnouncementMaster.aspx");
        }
    }

    private void CheckPageRefresh()
    {
        if (Session["CheckRefresh"].ToString() == ViewState["CheckRefresh"].ToString())
        {
            //Label1.Text = "Hello";
            Session["CheckRefresh"] = Server.UrlDecode(System.DateTime.Now.ToString());
        }
        else
        {
            Response.Redirect("AnnouncementMaster.aspx");
        }

    }

    private void ClearControls()
    {
        ViewState["action"] = null;
        ftbDescription.Text = "&nbsp;";
        txtExpDate.Text = string.Empty;
        txtSubject.Text = string.Empty;
        //lblStatus.Text = string.Empty;
        lblCurrdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
        lblSession.Text = Session["SESSION_NAME"].ToString();
        lblSession.ToolTip = Session["SessionNo"].ToString();
        lblPreAttach.Text = string.Empty;
        lblStatus.Text = string.Empty;
    }

    private void BindListView()
    {
        try
        {
            IAnnouncementController objAC = new IAnnouncementController();
            DataSet ds = objAC.GetAllAnnouncementtListByUaNo(Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["ICourseNo"]), Convert.ToInt32(Session["userno"]));

            //if (ds.Tables[0].Rows.Count > 0)
            //{
                lvAnnounce.DataSource = ds;
                lvAnnounce.DataBind();
           // }
            //else
            //{
            //    lvAnnounce.DataSource = null;
            //    lvAnnounce.DataBind();
            //}
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_AnnouncementMaster.BindListView-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowDetail(int an_no, int courseno, int sessionno, int ua_no)
    {
        try
        {
            IAnnouncementController objAC = new IAnnouncementController();
            ViewState["anno"] = an_no;
            DataTableReader dtr = objAC.GetSingleAnnouncement(Convert.ToInt32(ViewState["anno"]), courseno, sessionno, ua_no);

            //Show Announcement Details
            if (dtr != null)
            {
                if (dtr.Read())
                {
                    txtSubject.Text = dtr["SUBJECT"] == null ? "" : dtr["SUBJECT"].ToString();
                    ftbDescription.Text = dtr["DESCRIPTION"] == null ? "" : dtr["DESCRIPTION"].ToString();
                    txtExpDate.Text = dtr["EXPDATE"] == null ? "" : Convert.ToDateTime(dtr["EXPDATE"].ToString()).ToString("dd-MMM-yyyy");
                    hdnFile.Value = dtr["ATTACHMENT"] == null ? "" : dtr["ATTACHMENT"].ToString();
                    lblCurrdate.Text = dtr["STARTDATE"] == null ? "" : Convert.ToDateTime(dtr["STARTDATE"].ToString()).ToString("dd-MMM-yyyy");

                    lblPreAttach.Visible = false;
                    lblPreAttach.Text = "";
                    if (dtr["ATTACHMENT"] != null)
                    {
                        lblPreAttach.Visible = true;
                        lblPreAttach.Text = dtr["ATTACHMENT"].ToString();
                    }

                }
            }
            if (dtr != null) dtr.Close();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_AnnouncementMaster.ShowDetail -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            //string Script = string.Empty;
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Itle")));
            //url += "Reports/CommonReport.aspx?";
            //url += "pagetitle=" + reportTitle;
            //url += "&path=~,Reports,ITLE," + rptFileName;
            //url += "&param=@P_SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + ",SESSION_NAME=" + Session["SESSION_NAME"] + ",@P_COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + ",@P_COLLEGE_CODE=" + Session["colcode"]; //",COURSE_NAME=" + Session["ICourseName"] +
            //Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);


            string Script = string.Empty;
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("itle")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,ITLE," + rptFileName;
            url += "&param=@P_SESSIONNO=" + Convert.ToInt32(Session["SessionNo"]) + ",@P_COURSENO=" + Convert.ToInt32(Session["ICourseNo"]) + ",@P_COLLEGE_CODE=" + Session["colcode"]; //",COURSE_NAME=" + Session["ICourseName"] +
            Script += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Report", Script, true);


        }
        catch (Exception ex)
        {
            objCommon.DisplayUserMessage(Page, "AnnouncementMaster.ShowReport->  " + ex.Message + ex.StackTrace, this.Page);
        }
    }

    #endregion

    #region Attachments

    public string GetFileNamePath(object filename)
    {
        if (filename != null && filename.ToString() != "")
            return "ITLE/upload_files/announcement/" + filename.ToString();
        else
            return "";
    }

    public string GetStatus(object status)
    {
        DateTime DT = Convert.ToDateTime(status);
        if (DT < DateTime.Today)
            return "<span style='color:Red'>Expired</span>";
        else
            return "<span style='color:Green'>Active</span>";
    }

    //Get Page Id for getting File size from configuration
    private void GetAttachmentSize()
    {


        try
        {

            PageId = Request.QueryString["pageno"];

            if (Convert.ToInt32(Session["usertype"]) == 1)
            {

                File_size = Convert.ToDecimal(objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "FILE_SIZE_ADMIN", "PAGE_ID=" + PageId));
                lblFileSize.Text = objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "dbo.udf_FormatBytes(FILE_SIZE_ADMIN,'Bytes')AS FILE_SIZE_ADMIN", "PAGE_ID=" + PageId);
            }
            else

                if (Convert.ToInt32(Session["usertype"]) == 2)
                {
                    File_size = Convert.ToDecimal(objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "FILE_SIZE_STUDENT", "PAGE_ID=" + PageId));

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

    //private void GetStatusOnPageLoad()
    //{
    //    IAnnouncementController objAC = new IAnnouncementController();
    //    try
    //    {
    //        DateTime CurrentDate = DateTime.Today;
    //        objAC.GetStatus(CurrentDate);

    //    }
    //    catch (Exception ex)
    //    { 

    //    }
    //}


    //public string GetFileName(object filename, object announcemetnno)
    //{
    //    if (filename != null && filename.ToString() != "")
    //        return "file:///" + file_path.Replace("\\","/") + "ITLE/upload_files/announcement/announcement_" + Convert.ToInt32(announcemetnno) + System.IO.Path.GetExtension(filename.ToString());
    //    else
    //        return "None";
    //}

    #endregion

    #region Page Events

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControls();
        lblStatus.Text = string.Empty;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btnEdit = sender as ImageButton;
            int an_no = int.Parse(btnEdit.CommandArgument);
            ViewState["an_no"] = an_no;

            ShowDetail(an_no, Convert.ToInt32(Session["ICourseNo"]), Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["userno"].ToString()));

            ViewState["action"] = "edit";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_AnnouncementMaster.btnEdit_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            CheckPageRefresh();
            ImageButton btnDel = sender as ImageButton;
            int an_no = int.Parse(btnDel.CommandArgument);

            IAnnouncementController objAC = new IAnnouncementController();

            CustomStatus cs = (CustomStatus)objAC.DeleteAnnouncement(Convert.ToInt32(Session["userno"].ToString()), Convert.ToInt32(lblSession.ToolTip), Convert.ToInt32(Session["ICourseNo"]), an_no);
            if (cs.Equals(CustomStatus.RecordDeleted))
                objCommon.DisplayMessage(UpdAnnounce, "Announcement Deleted Successfully...", this.Page);
            //lblStatus.Text = "Announcement Deleted Successfully...";
            BindListView();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Itle_AnnouncementMaster.btnDelete_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        CheckPageRefresh();
        //txtExpDate.Text = Request.Form(txtExpDate.UniqueId);
        txtExpDate.Text = Request.Form[txtExpDate.UniqueID];
        if (Convert.ToDateTime(txtExpDate.Text.Trim()) <= DateTime.Today)
        {
            //lblStatus.BackColor = System.Drawing.Color.Yellow;
            //lblStatus.Text = "Announcement Expiry Date Must Be Greater Then Current Date";
            objCommon.DisplayMessage(UpdAnnounce, "Announcement Expiry Date Must Be Greater Then Current Date", this.Page);

            return;
        }
        IAnnouncement objAnnounce = new IAnnouncement();
        IAnnouncementController objAC = new IAnnouncementController();
        try
        {


            string filename = string.Empty;
            objAnnounce.UA_NO = Convert.ToInt32(Session["userno"].ToString());
            objAnnounce.SESSIONNO = Convert.ToInt32(lblSession.ToolTip);
            objAnnounce.COURSENO = Convert.ToInt32(Session["ICourseNo"]);
            objAnnounce.SUBJECT = txtSubject.Text.Trim();
            objAnnounce.DESCRIPTION = ftbDescription.Text.Trim();
            //objAnnounce.ATTACHMENT = fuAnnounce.FileName;
            objAnnounce.STARTDATE = Convert.ToDateTime(lblCurrdate.Text.ToString());
            objAnnounce.EXPDATE = (txtExpDate.Text.ToString() != "" ? Convert.ToDateTime(txtExpDate.Text.ToString()) : DateTime.MinValue);
            objAnnounce.STATUS = '1';
            objAnnounce.COLLEGE_CODE = Session["colcode"].ToString();

            //if (fuAnnounce.PostedFile.ContentLength < File_size || fuAnnounce.HasFile.ToString() == "")
            //{

            //    objAnnounce.ATTACHMENT = fuAnnounce.FileName;


            //    //fileUploader.SaveAs(Server.MapPath("") + "\\UPLOAD_FILES\\" + fileName);
            //}
            //else
            //{
            //    objCommon.DisplayMessage("Unable to upload file. Size of uploaded file is greater than maximum upload size allowed.", this);

            //    return;

            //}


            if (ViewState["action"] != null && ViewState["action"].ToString().Equals("edit"))
            {
                objAnnounce.AN_NO = Convert.ToInt32(ViewState["an_no"]);
                objAnnounce.OLDFILENAME = hdnFile.Value;

                //if (hdnFile.Value != "" && hdnFile.Value != null && fuAnnounce.HasFile == false)
                //{
                //    objAnnounce.ATTACHMENT = hdnFile.Value;
                //}

                //set status
                DateTime dt = Convert.ToDateTime(txtExpDate.Text);
                if (dt < DateTime.Now)
                    objAnnounce.STATUS = '0';
                else
                    objAnnounce.STATUS = '1';

                CustomStatus cs = (CustomStatus)objAC.UpdateAnnouncement(objAnnounce, fuAnnounce);
                if (cs.Equals(CustomStatus.RecordUpdated))
                    objCommon.DisplayMessage(UpdAnnounce, "Record Modified Successfully", this.Page);
                //lblStatus.Text = "Record Modified...... ";
                //Response.Redirect(Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&action")));
                else
                    if (cs.Equals(CustomStatus.FileExists))
                        lblStatus.Text = "File already exists. Please upload another file or rename and upload.";

                ClearControls();

            }
            else
            {
                //string Subject = objCommon.LookUp("ACD_IANNOUNCEMASTER", "SUBJECT", "SUBJECT='" + txtSubject.Text.Trim() + "'");
                //if (Subject == txtSubject.Text)
                //{
                //    objCommon.DisplayMessage(UpdAnnounce, "Announcement exist with this Title", this.Page);
                //    return;
                //}

                CustomStatus cs = (CustomStatus)objAC.AddAnnouncement(objAnnounce, fuAnnounce);
                if (cs.Equals(CustomStatus.RecordSaved))
                {
                    objCommon.DisplayMessage(UpdAnnounce, "Record Saved Successfully", this.Page);


                }
                else if (cs.Equals(CustomStatus.DuplicateRecord))
                {
                    objCommon.DisplayUserMessage(UpdAnnounce, "Announcement Allready Exist.....", this.Page);
                }                
                //lblStatus.Text = "....Record Saved !";
                //Response.Redirect(Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&action")));
                else
                    if (cs.Equals(CustomStatus.FileExists))
                        lblStatus.Text = "File already exists. Please upload another file or rename and upload.";
            }

            ClearControls();
            BindListView();
            //Response.Redirect("AnnouncementMaster.aspx");
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Itle_AnnouncementMaster.btnSubmit_Click -> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    protected void btnViewAnnouncement_Click(object sender, EventArgs e)
    {
        try
        {

            ShowReport("Itle_Announcement_Report", "Itle_Announcement_Report.rpt");

        }
        catch (Exception ex)
        {

            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "AnnouncementMaster.btnViewTeachingPlan_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");

        }
    }

    //To Download File
    protected void lnkDownload_Click(object sender, EventArgs e)
    {

        LinkButton lnkbtn = sender as LinkButton;

        int an_no = int.Parse(lnkbtn.CommandArgument);
        string fileName = lnkbtn.ToolTip.ToString();
        string filePath = file_path + "Itle/upload_files/announcement/" + "announcement_" + Convert.ToInt32(an_no);

        HttpContext.Current.Response.ContentType = "Text/Doc";
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(filePath) + System.IO.Path.GetExtension(fileName));
        HttpContext.Current.Response.WriteFile(filePath + System.IO.Path.GetExtension(fileName));
        HttpContext.Current.Response.End();
        //HttpContext.Current.Response.ContentType = "application/octet-stream";

    }

    protected void lvAnnounce_SelectedIndexChanged(object sender, EventArgs e)
    {



    }

    #endregion  
}
