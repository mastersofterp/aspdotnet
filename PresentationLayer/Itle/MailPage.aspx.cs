using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Collections.Generic;
using IITMS.NITPRM;
public partial class Itle_MailPage : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    IMailController myController = new IMailController();
    CourseControlleritle objCourse = new CourseControlleritle();
    string file_path = System.Configuration.ConfigurationManager.AppSettings["DirPath"];

    string PageId;
    decimal File_size;

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
        //Check Session
        if (Session["userno"] == null || Session["username"] == null ||
            Session["usertype"] == null || Session["userfullname"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
        else
        {
            if (!Page.IsPostBack)
            {
                //Page Authorization
                CheckPageAuthorization();
                //Set the Page Title
                ViewState["senderName"] = txtMailTo.Text.ToString();
                Page.Title = Session["coll_name"].ToString();
                PageId = Request.QueryString["pageno"];
               
                object x = Request.Url;
                if (Request.QueryString["mid"] != null && Request.QueryString["mid"] != "")
                {
                    int mailId = Convert.ToInt32(Request.QueryString["mid"]);
                    ShowMailDetails(mailId);
                }
                else
                    ShowInbox();
                // nullify any attachment record in user session
                Session["Attachments"] = null;

                
                // Used to insert id,date and courseno in Log_History table
                int log_history = objCourse.AddLogHistory(Convert.ToInt32(Session["idno"]), Convert.ToInt32(PageId), Convert.ToInt32(Session["ICourseNo"]));

                if (Convert.ToInt32(Session["usertype"]) == 3)
                {
                    lblGroupLink.Visible = true;
                }
            
            }
            else
            {
                divReadMail.Visible = false;
            }
        }

        // Used to get maximum size of file attachment
        GetAttachmentSize();
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
                Response.Redirect("~/notauthorized.aspx?page=Itle_MailPage.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=Itle_MailPage.aspx");
        }
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
            }
            else

                if (Convert.ToInt32(Session["usertype"]) == 2)
                {
                    File_size = Convert.ToDecimal(objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "FILE_SIZE_STUDENT", "PAGE_ID=" + PageId));
                    lblFileSize.Text = objCommon.LookUp("ACD_IATTATCHMENT_CONFIGURATION", "dbo.udf_FormatBytes(FILE_SIZE_STUDENT,'Bytes')AS FILE_SIZE_STUDENT", "PAGE_ID=" + PageId);
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

    #endregion

    #region Inbox

    protected void btnShowInbox_Click(object sender, EventArgs e)
    {
        try
        {
            lvSearchMail.DataSource = null;
            txtSearch.Text = string.Empty;
            lblError.Text = string.Empty;
            Session["OutBoxMail"] = null;
            ShowInbox();

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_MailPage.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }


    #endregion

    #region Outbox

    protected void btnDeleteOnboxMail_Click(object sender, EventArgs e)
    {
        this.SetOutBoxMailStatus(lvOutMails, 'D');
        ShowOutbox();
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        try
        {



            //Check for use input
            if (txtSearch.Text.Trim() != "")
            {
                divInMails.Visible = false;
                divOutMails.Visible = false;
                dvTrash.Visible = false;

                DataSet oDs = new DataSet();
                if (Session["OutBoxMail"] != null)
                {
                    oDs = (DataSet)Session["OutBoxMail"];
                }
                else
                {
                    oDs = (DataSet)Session["MailMessages"];
                }

                string strQuery = string.Empty;
                DataSet oDsResults = oDs.Clone();

                if (oDs != null && oDs.Tables.Count > 0)
                {

                    //FOR SEARCHING OUTBOX MAIL
                    if (Session["OutBoxMail"] != null)
                    {
                        strQuery = "RECEIVER like '%" + txtSearch.Text + "%'";


                    }
                    //FOR SEARCHING INBOX MAIL
                    else
                    {
                        strQuery = "SENDER like '%" + txtSearch.Text + "%'";


                    }

                    DataRow[] drFilterRows = oDs.Tables[0].Select(strQuery);

                    foreach (DataRow dr in drFilterRows)
                    {
                        oDsResults.Tables[0].ImportRow(dr);

                    }

                    if (drFilterRows.Length > 0)
                    {
                        oDsResults.AcceptChanges();
                        if (Session["OutBoxMail"] != null)
                        {
                            //FOR SEARCHING OUTBOX MAIL
                            lvSearchOutBoxMail.DataSource = oDsResults;
                            lvSearchOutBoxMail.DataBind();
                            divSearchOutBoxMail.Visible = true;
                            divSearchMail.Visible = false;
                        }
                        else
                        {
                            //FOR SEARCHING INBOX MAIL
                            lvSearchMail.DataSource = oDsResults;
                            lvSearchMail.DataBind();
                            divSearchMail.Visible = true;
                            divSearchOutBoxMail.Visible = false;
                        }

                        lblError.Visible = true;
                        lblError.Text = drFilterRows.Length + " Record Found.";
                        lblError.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        DataSet ds = null;
                        lvSearchMail.DataSource = ds;
                        lvSearchMail.DataBind();

                        lvSearchOutBoxMail.DataSource = ds;
                        lvSearchOutBoxMail.DataBind();
                        oDsResults = null;
                        lblError.Visible = true;
                        lblError.Text = "No Record Found.";
                        lblError.ForeColor = System.Drawing.Color.Green;

                    }
                }
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "Please fill criteria before search";
                lblError.ForeColor = System.Drawing.Color.Red;
            }



        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "MailPage.btnSearch_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }

    }

    protected void btnShowOutbox_Click(object sender, EventArgs e)
    {
        try
        {
            lvSearchMail.DataSource = null;
            txtSearch.Text = string.Empty;
            lblError.Text = string.Empty;
            ShowOutbox();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_MailPage.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    #endregion

    #region Trash

    protected void btnSearchRestore_Click(object sender, EventArgs e)
    {

        this.SetTrashMailStatus(lvSearchMail, 'P');
        ShowTrash();
    }

    private void ShowTrash()
    {
        try
        {
            #region Tab Setting
            divSearchMail.Visible = false;
            divInMails.Visible = false;
            divOutMails.Visible = false;
            divCompose.Visible = false;
            dvTrash.Visible = true;
            divSearchOutBoxMail.Visible = false;

            spnInbox.Attributes["class"] = "tab";
            spnSentItems.Attributes["class"] = "tab";
            spnCompose.Attributes["class"] = "tab";
            spTrash.Attributes["class"] = "active_tab";
            #endregion

            DataSet ds = myController.GetDeletedMails(Session["userno"] != null ? Convert.ToInt32(Session["userno"]) : 0);
            lvTrash.DataSource = ds;
            lvTrash.DataBind();

            Session["MailMessages"] = ds;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_MailPage.ShowTrash --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnTrash_Click(object sender, EventArgs e)
    {

        try
        {
            lvSearchMail.DataSource = null;
            txtSearch.Text = string.Empty;
            lblError.Text = string.Empty;
            Session["OutBoxMail"] = null;
            ShowTrash();
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_MailPage.btnTrash_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }


    }

    #endregion

    #region Compose Mail

    protected void btnComposeMsg_Click(object sender, EventArgs e)
    {
        try
        {
            #region Tab Setting
            divInMails.Visible = false;
            divOutMails.Visible = false;
            divCompose.Visible = true;
            dvTrash.Visible = false;
            txtSearch.Text = string.Empty;
            spnInbox.Attributes["class"] = "tab";
            spnSentItems.Attributes["class"] = "tab";
            spnCompose.Attributes["class"] = "active_tab";
            spTrash.Attributes["class"] = "tab";
            #endregion
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_MailPage.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnAttachFile_Click(object sender, EventArgs e)
    {
        try
        {
            if (fileUploader.HasFile)
            {
                string DOCFOLDER = file_path + "ITLE\\upload_files\\MailMessage";

                if (!System.IO.Directory.Exists(DOCFOLDER))
                {
                    System.IO.Directory.CreateDirectory(DOCFOLDER);

                }

                string fileName = System.Guid.NewGuid().ToString() + fileUploader.FileName.Substring(fileUploader.FileName.IndexOf('.'));
                string fileExtention = System.IO.Path.GetExtension(fileName);

                int count = Convert.ToInt32(objCommon.LookUp("ACD_IATTACHMENT_FILE_EXTENTIONS", "COUNT(EXTENTION)", "EXTENTION='" + fileExtention.ToString() + "'"));

                if (count != 0)
                {

                    string filePath = file_path + "ITLE\\upload_files\\MailMessage\\" + fileName;
                    // string filePath = "ITLE\\UPLOAD_FILES\\" + fileName;

                    if (fileUploader.PostedFile.ContentLength < File_size)
                    {
                        fileUploader.SaveAs(filePath);
                        //fileUploader.SaveAs(Server.MapPath("") + "\\UPLOAD_FILES\\" + fileName);
                    }
                    else
                    {
                        objCommon.DisplayMessage("Unable to upload file. Size of uploaded file is greater than maximum upload size allowed.", this);
                        return;
                    }

                    DataTable dt;
                    if (Session["Attachments"] != null && ((DataTable)Session["Attachments"]) != null)
                    {
                        dt = ((DataTable)Session["Attachments"]);
                        DataRow dr = dt.NewRow();
                        dr["ATTACH_ID"] = dt.Rows.Count + 1;
                        dr["FILE_NAME"] = fileUploader.FileName;
                        dr["FILE_PATH"] = filePath;
                        dr["SIZE"] = (fileUploader.PostedFile.ContentLength / 1024);
                        dt.Rows.Add(dr);
                        Session["Attachments"] = dt;
                        this.BindListView_Attachments(dt);
                    }
                    else
                    {
                        dt = this.GetAttachmentDataTable();
                        DataRow dr = dt.NewRow();
                        dr["ATTACH_ID"] = dt.Rows.Count + 1;
                        dr["FILE_NAME"] = fileUploader.FileName;
                        dr["FILE_PATH"] = filePath;
                        dr["SIZE"] = fileUploader.PostedFile.ContentLength;
                        dt.Rows.Add(dr);
                        Session.Add("Attachments", dt);
                        this.BindListView_Attachments(dt);
                    }
                }
                else
                {
                    objCommon.DisplayMessage("File Format not supprted.", this);
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
                objUCommon.ShowError(Page, "MailPage.btnAttachFile_Click() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
        txtMailTo.Text = Convert.ToString(hdntxtMailto.Value);
    }

    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        try
        {


            Mail myMail = new Mail();
            myMail.Subject = txtSubject.Text.Trim();
            myMail.SentDate = DateTime.Now;
            myMail.Body = ftbMailBody.Text;
            myMail.SenderId = Convert.ToInt32(Session["userno"]);
            if (ViewState["ParentMailId"] != null && ViewState["ParentMailId"].ToString() != "")
                myMail.ParentId = Convert.ToInt32(ViewState["ParentMailId"]);
            List<MailReceiver> receivers = new List<MailReceiver>();
            List<MailAttachment> attachments = new List<MailAttachment>();

            string[] rec = hidMailTo.Value.Split(',');

            if (hidMailTo.Value == "")
            {
                MailReceiver user = new MailReceiver();
                user.ReceiverId = Convert.ToInt32(hidSenderid.Value);
                user.Status = 'U';
                receivers.Add(user);
            }
            else
            {
                foreach (string userno in rec)
                {
                    MailReceiver user = new MailReceiver();
                    try
                    {
                        user.ReceiverId = Convert.ToInt32(userno);
                    }
                    catch
                    {
                    }
                    user.Status = 'U';
                    receivers.Add(user);
                }
            }
            myMail.Receivers = receivers;

            if (Session["Attachments"] != null && ((DataTable)Session["Attachments"]) != null)
            {
                DataTable dt = ((DataTable)Session["Attachments"]);
                foreach (DataRow dr in dt.Rows)
                {
                    MailAttachment attach = new MailAttachment();
                    attach.FileName = dr["FILE_NAME"].ToString();
                    attach.FilePath = dr["FILE_PATH"].ToString();
                    attach.Size = Convert.ToInt32(dr["SIZE"]);
                    attachments.Add(attach);
                }
            }
            myMail.Attachments = attachments;
            if (myController.SendMail(myMail))
                this.btnDiscard_Click(sender, e);
            else
                objCommon.DisplayMessage("Unable to send message.", this);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_MailPage.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    protected void btnDiscard_Click(object sender, EventArgs e)
    {
        //Response.Redirect(Request.Url.ToString().Contains("&type=") ? Request.Url.ToString().Remove(Request.Url.ToString().IndexOf("&type=")) : Request.Url.ToString());
        string url = "~/ITLE/MailPage.aspx?pageno=" + PageId; //Added by Saket Singh on 06-09-2017
        Response.Redirect(url, false);
    }

    #endregion

    #region Actions on Mails

    protected void btnDeleteInboxMail_Click(object sender, EventArgs e)
    {
        this.SetMailStatus(lvInMails, 'D');
        ShowInbox();
    }

    protected void btnMarkAsRead_Click(object sender, EventArgs e)
    {
        this.SetMailStatus(lvInMails, 'R');
        ShowInbox();
    }

    protected void btnMarkAsUnread_Click(object sender, EventArgs e)
    {
        this.SetMailStatus(lvInMails, 'U');
        ShowInbox();
    }

    protected void btnRestore_Click(object sender, EventArgs e)
    {

        this.SetTrashMailStatus(lvTrash, 'P');
        ShowTrash();
    }

    protected void btnReply_Click(object sender, EventArgs e)
    {
        ViewState["ParentMailId"] = ViewState["MailId"];
        txtMailTo.Text = hidSenderUsername.Value;
        txtSubject.Text = "Re: " + lblSubject.Text;
        ftbMailBody.Text = "<br/><br/>-------------------------------------------------------------------------------------------------";
        ftbMailBody.Text += "<table width='100%'><tr><td width='10%'>From: </td><td width='90%'>" + lblMailFrom.Text + "</td></tr>";
        ftbMailBody.Text += "<tr><td>Date:</td><td>" + lblMailDate.Text + "</td></tr>";
        ftbMailBody.Text += "<tr><td>Subject:</td><td>" + lblSubject.Text + "</td></tr>";
        ftbMailBody.Text += "<tr><td>Sent To:</td><td>" + lblMailTo.Text + "</td></tr>";
        ftbMailBody.Text += "<tr><td colspan='2'><br/>" + divMailBody.InnerHtml + "</td></tr>";

        btnComposeMsg_Click(sender, e);
    }

    protected void btnForward_Click(object sender, EventArgs e)
    {
        ViewState["ParentMailId"] = ViewState["MailId"];
        txtSubject.Text = "Fwd: " + lblSubject.Text;
        ftbMailBody.Text = "<br/><br/>-------------------------------------------------------------------------------------------------";
        ftbMailBody.Text += "<table width='100%'><tr><td width='10%'>From: </td><td width='90%'>" + lblMailFrom.Text + "</td></tr>";
        ftbMailBody.Text += "<tr><td>Date:</td><td>" + lblMailDate.Text + "</td></tr>";
        ftbMailBody.Text += "<tr><td>Subject:</td><td>" + lblSubject.Text + "</td></tr>";
        ftbMailBody.Text += "<tr><td>Sent To:</td><td>" + lblMailTo.Text + "</td></tr>";
        ftbMailBody.Text += "<tr><td colspan='2'><br/>" + divMailBody.InnerHtml + "</td></tr>";

        btnComposeMsg_Click(sender, e);
    }

    protected void btnDeleteMail_Click(object sender, EventArgs e)
    {
        myController.SetMailStatus(Convert.ToInt32(ViewState["MailId"]), Convert.ToInt32(Session["userno"]), 'D');
        ShowInbox();
    }

    #endregion

    #region Private Methods

    private void ShowInbox()
    {
        try
        {
            #region Tab Setting
            divInMails.Visible = true;
            divOutMails.Visible = false;
            divCompose.Visible = false;
            dvTrash.Visible = false;
            divSearchMail.Visible = false;
            divSearchOutBoxMail.Visible = false;
            txtSearch.Text = string.Empty;
            //lblError.Text = string.Empty;
            spnInbox.Attributes["class"] = "active_tab";
            spnSentItems.Attributes["class"] = "tab";
            spnCompose.Attributes["class"] = "tab";
            spTrash.Attributes["class"] = "tab";
            #endregion

            DataSet ds = myController.GetReceivedMails(Session["userno"] != null ? Convert.ToInt32(Session["userno"]) : 0);
            lvInMails.DataSource = ds;
            lvInMails.DataBind();

            Session["MailMessages"] = ds;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_MailPage.ShowInbox --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void ShowOutbox()
    {
        try
        {
            #region Tab Setting
            divInMails.Visible = false;
            divOutMails.Visible = true;
            divCompose.Visible = false;
            dvTrash.Visible = false;
            divSearchMail.Visible = false;
            divSearchOutBoxMail.Visible = false;
            spnInbox.Attributes["class"] = "tab";
            spnSentItems.Attributes["class"] = "active_tab";
            spnCompose.Attributes["class"] = "tab";
            spTrash.Attributes["class"] = "tab";
            #endregion

            DataSet ds = myController.GetSentMails(Session["userno"] != null ? Convert.ToInt32(Session["userno"]) : 0);
            lvOutMails.DataSource = ds;
            lvOutMails.DataBind();
            Session["OutBoxMail"] = ds;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Itle_MailPage.btnSubmit_Click --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable");
        }
    }

    private void ShowMailDetails(int mailId)
    {
        DataSet ds = myController.GetMailById(mailId);
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["MailId"] = mailId;
            hidSenderid.Value = Convert.ToInt32(ds.Tables[0].Rows[0]["SENDER_ID"]).ToString();
            hidSenderUsername.Value = ds.Tables[0].Rows[0]["USERNAME"].ToString();
            lblMailFrom.Text = ds.Tables[0].Rows[0]["SENDER"].ToString();
            lblMailTo.Text = ds.Tables[0].Rows[0]["RECEIVER"].ToString();
            lblMailDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["SENT_DATE"]).ToString("dddd, dd MMMM yyyy hh:mm tt");
            lblSubject.Text = ds.Tables[0].Rows[0]["SUBJECT"].ToString();
            divMailBody.InnerHtml = ds.Tables[0].Rows[0]["BODY"].ToString();
            divReadMail.Visible = true;

            if (Request.QueryString["type"] == "in")
            {
                #region Inbox Tab Setting
                divInMails.Visible = false;
                divOutMails.Visible = false;
                divCompose.Visible = false;
                spnInbox.Attributes["class"] = "active_tab";
                spnSentItems.Attributes["class"] = "tab";
                spnCompose.Attributes["class"] = "tab";
                #endregion
            }
            else
            {
                #region Outbox Tab Setting
                divInMails.Visible = false;
                divOutMails.Visible = false;
                divCompose.Visible = false;
                spnInbox.Attributes["class"] = "tab";
                spnSentItems.Attributes["class"] = "active_tab";
                spnCompose.Attributes["class"] = "tab";
                #endregion
            }
            ds = myController.GetMailAttachments(mailId);
            lvAttachments.DataSource = ds;
            lvAttachments.DataBind();

            myController.SetMailStatus(mailId, Convert.ToInt32(Session["userno"]), 'R');
        }
        else
            ShowInbox();
    }

    private void SetMailStatus(ListView lv, char status)
    {
        foreach (ListViewDataItem item in lv.Items)
        {
            CheckBox chk = item.FindControl("chkSelectMail") as CheckBox;
            if (chk != null && chk.Checked)
            {
                HiddenField hid = item.FindControl("hidMailId") as HiddenField;
                if (hid != null)
                {
                    int mailId = Convert.ToInt32(hid.Value);
                    myController.SetMailStatus(mailId, Convert.ToInt32(Session["userno"]), status);
                }
            }
            else
            {
                objCommon.DisplayUserMessage(this.Page, "Please select Mail.", this.Page);

            }
        }
    }

    private void SetTrashMailStatus(ListView lv, char status)
    {
        foreach (ListViewDataItem item in lv.Items)
        {
            CheckBox chk = item.FindControl("chkSelectMail") as CheckBox;
            if (chk != null && chk.Checked)
            {
                HiddenField hid = item.FindControl("hidMailId") as HiddenField;
                if (hid != null)
                {
                    int mailId = Convert.ToInt32(hid.Value);
                    myController.SetTrashMailStatus(mailId, Convert.ToInt32(Session["userno"]));
                }
            }
            else
            {
                objCommon.DisplayUserMessage(this.Page, "Please select Mail to restore.", this.Page);

            }
        }
    }

    private void SetOutBoxMailStatus(ListView lv, char status)
    {
        foreach (ListViewDataItem item in lv.Items)
        {
            CheckBox chk = item.FindControl("chkSelectMail") as CheckBox;
            if (chk != null && chk.Checked)
            {
                HiddenField hid = item.FindControl("hidMailId") as HiddenField;
                if (hid != null)
                {
                    int mailId = Convert.ToInt32(hid.Value);
                    myController.SetOutBoxMailStatus(mailId, Convert.ToInt32(Session["userno"]), status);
                }
            }
            else
            {
                objCommon.DisplayUserMessage(this.Page, "Please select Mail to delete.", this.Page);

            }
        }
    }

    #endregion

    #region Displaying File Attachment



    protected void lnkRemoveAttach_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton btnRemove = sender as LinkButton;
            DataTable dt;
            if (Session["Attachments"] != null && ((DataTable)Session["Attachments"]) != null)
            {
                dt = ((DataTable)Session["Attachments"]);
                dt.Rows.Remove(this.GetDeletableDataRow(dt, btnRemove.CommandArgument));
                Session["Attachments"] = dt;
                this.BindListView_Attachments(dt);
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

    #endregion

    #region Paging

    protected void dpInMails_OnPreRender(object sender, EventArgs e)
    {
        ShowInbox();
    }

    protected void dpOutMails_OnPreRender(object sender, EventArgs e)
    {
        ShowOutbox();
    }

    protected void dpTrash_OnPreRender(object sender, EventArgs e)
    {
        ShowTrash();
    }



    #endregion

}

