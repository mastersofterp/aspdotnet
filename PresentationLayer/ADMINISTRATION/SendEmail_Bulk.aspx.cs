using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using SendGrid;
using System.Text;
using Org.BouncyCastle.X509;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using EASendMail;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.Collections.Generic;
using BusinessLogicLayer.BusinessLogic;

public partial class ADMINISTRATION_SendEmail_Bulk : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    FetchDataController objFetch = new FetchDataController();
    AcdAttendanceController objAttC = new AcdAttendanceController();
    SendEmailCommon objSendEmail = new SendEmailCommon(); //Object Creation


    #region PAGE_LOAD
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
                    //  CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();


                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    PopulateDropDownList();
                    //btnSndSms.Enabled = false;
                    ViewState["FileName"] = null;
                    fuAttachment.FileContent.Flush();
                }
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AttendenceReportByFaculty.Page_Load()-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=studentinfoentry.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=studentinfoentry.aspx");
        }
    }
    protected void PopulateDropDownList()
    {
        try
        {
            objCommon.FillDropDownList(ddlCollege, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0", "COLLEGE_ID");

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "Academic_AttendanceReportByFaculty.PopulateDropDownList-> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion

    #region Cancel
    private void ClearControls()
    {
        ddlBranch.Items.Clear();
        ddlBranch.Items.Add(new ListItem("Please Select", "0"));
        ddlSem.Items.Clear();
        ddlSem.Items.Add(new ListItem("Please Select", "0"));

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    #endregion

    #region DDL
    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDegree.SelectedIndex > 0)
        {
            objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH A INNER JOIN ACD_COLLEGE_DEGREE_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "DISTINCT(A.BRANCHNO)", "A.LONGNAME", "A.BRANCHNO > 0 AND B.DEGREENO = " + ddlDegree.SelectedValue, "A.LONGNAME");
            ddlBranch.Focus();

            //objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH", "BRANCHNO", "LONGNAME", "BRANCHNO NOT IN(0) AND BRANCHNO > 0", "BRANCHNO");
            //ddlBranch.Focus();
            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
            ddlBranch.Focus();
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            lvStudents.Visible = false;
            PnlStudentmeeting.Visible = false;
        }
        else
        {
            ClearControls();
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            lvStudents.Visible = false;
            PnlStudentmeeting.Visible = false;
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBranch.SelectedIndex > 0)
        {
            ddlSem.Items.Clear();
            objCommon.FillDropDownList(ddlSem, "ACD_SEMESTER A INNER JOIN ACD_STUDENT B ON (A.SEMESTERNO=B.SEMESTERNO)", "DISTINCT A.SEMESTERNO", "A.SEMESTERNAME", "A.SEMESTERNO>0 AND DEGREENO=" + ddlDegree.SelectedValue, "A.SEMESTERNO");
            ddlSem.Focus();
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            lvStudents.Visible = false;

        }
        else
        {
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            lvStudents.Visible = false;

            ddlSem.Items.Clear();
            ddlSem.Items.Add(new ListItem("Please Select", "0"));
        }
    }
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        lvStudents.Visible = false;
        PnlStudentmeeting.Visible = false;
    }
    protected void ddlCollege_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCollege.SelectedIndex > 0)
            {
                ddlDegree.Items.Clear();
                objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE A INNER JOIN ACD_COLLEGE_DEGREE B ON (A.DEGREENO=B.DEGREENO)", "DISTINCT(A.DEGREENO)", "A.DEGREENAME", "A.DEGREENO > 0 AND B.COLLEGE_ID = " + Convert.ToInt32(ddlCollege.SelectedValue), "A.DEGREENAME");
                ddlDegree.Focus();
            }
            else
            {
                ddlDegree.Items.Clear();
                ddlDegree.Items.Add(new ListItem("Please Select", "0"));
            }
            lvStudents.DataSource = null;
            lvStudents.DataBind();
            lvStudents.Visible = false;
            PnlStudentmeeting.Visible = false;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "ExamDate.ddlCollege_SelectedIndexChanged -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void rdbRegistered_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudents.DataSource = null;
        lvStudents.DataBind();
        lvStudents.Visible = false;

    }
    #endregion DDL

    #region Send Mail
    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        int count = 0;
        string IPaddress = Session["ipAddress"].ToString();
        if (fuAttachment.HasFile)
        {
            string contentType = contentType = fuAttachment.PostedFile.ContentType;
            string ext = System.IO.Path.GetExtension(fuAttachment.PostedFile.FileName);
            int fileSize = fuAttachment.PostedFile.ContentLength;
            int KB = fileSize / 1024;
            if (ext == ".pdf")
            {
                if (KB <= 500)
                {
                    string folderPath = Server.MapPath("~/TempDocument/");
                    //Check whether Directory (Folder) exists.
                    if (!Directory.Exists(folderPath))
                    {
                        //If Directory (Folder) does not exists. Create it.
                        Directory.CreateDirectory(folderPath);
                    }

                    //Save the File to the Directory (Folder).
                    ViewState["FileName"] = fuAttachment.FileName;
                    if (ViewState["FileName"] != string.Empty || ViewState["FileName"] != "")
                    {
                        string x = folderPath + Path.GetFileName(fuAttachment.FileName);
                        if (!File.Exists(x))
                        {
                            fuAttachment.SaveAs(folderPath + Path.GetFileName(fuAttachment.FileName));
                        }
                        else
                        {
                        }
                    }

                }
                else
                {
                    objCommon.DisplayMessage(this.Page, "Please Upload file Below or Equal to 500 kb only !", this.Page);
                    return;
                }

            }
            else
            {
                objCommon.DisplayMessage(this.Page, "Please Upload file with .pdf format only.", this.Page);
                return;
            }
        }
        foreach (ListViewDataItem dataitem in lvStudents.Items)
        {
            CheckBox cbRow = dataitem.FindControl("cbRow") as CheckBox;
            if (cbRow.Checked == true)
                count++;
        }
        if (count <= 0)
        {
            objCommon.DisplayMessage(this.updpnl, "Please Select atleast one Student For Send Email", this);
        }
        else
        {
            foreach (ListViewDataItem item in lvStudents.Items)
            {
                CheckBox chek = item.FindControl("cbRow") as CheckBox;
                Label lblParMobile = item.FindControl("lblParMobile") as Label;
                HiddenField hdnidno = item.FindControl("hdnidno") as HiddenField;
                Label lblParEmail = item.FindControl("lblParEmail") as Label;
                string useremail = lblParEmail.Text;
                string subject = txtSubject.Text;
                string message = txtMessage.Text;
                string path = Server.MapPath("~/TempDocument/");
                int cs = 0;
                if (chek.Checked)
                {
                    if (useremail != string.Empty)
                    {
                        try
                        {
                            int status = 0;
                            string email_type = string.Empty;
                            string Link = string.Empty;
                            int sendmail = 0;
                            string filename = Convert.ToString(ViewState["FileName"]);
                            string Imgfile = string.Empty;
                            Byte[] Imgbytes = null;
                            if (filename != string.Empty)
                            {
                                path = path + filename;
                                string LogoPath = path;
                                Imgbytes = File.ReadAllBytes(LogoPath);
                                Imgfile = Convert.ToBase64String(Imgbytes);
                            }
                            if (filename == string.Empty)
                            {
                                status = objSendEmail.SendEmail(useremail, message, subject); 
                            }
                            else
                            {
                                status = objSendEmail.SendEmail(useremail, message, subject, "", "", null, filename, Imgbytes, "image/png/pdf");
                            }

                            if (status == 1)
                            {
                                objCommon.DisplayMessage(this.updpnl, "Email Send Successfully", this.Page);

                            }
                            else
                            {
                                objCommon.DisplayMessage(this.updpnl, "Failed To send email", this.Page);

                            }
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                }
            }
            foreach (ListViewDataItem item in lvStudents.Items)
            {
                CheckBox chek = item.FindControl("cbRow") as CheckBox;
                // CheckBox chek1 = item.FindControl("cbHead") as CheckBox;
                chek.Checked = false;
                // chek1.Checked = false;
            }
        }

    }

    #endregion Send Mail

    #region Comment 
    //private DataSet getModuleConfig()
    //{
    //    DataSet ds = objCommon.GetModuleConfig(Convert.ToInt32(Convert.ToInt32(Session["OrgId"])));
    //    return ds;
    //}
    //static async Task<int> Execute(string Message, string toEmailId, string sub, string filename, string path, int OrgId)
    //{
    //    int ret = 0;
    //    try
    //    {
    //        string Imgfile = string.Empty;
    //        if (filename != string.Empty)
    //        {
    //            path = path + filename;
    //            string LogoPath = path;
    //            Byte[] Imgbytes = File.ReadAllBytes(LogoPath);
    //            Imgfile = Convert.ToBase64String(Imgbytes);

    //            Common objCommon = new Common();
    //            DataSet dsconfig = null;
    //            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY,SUBJECT_OTP", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);
    //            var fromAddress = new System.Net.Mail.MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString());
    //            var toAddress = new System.Net.Mail.MailAddress(toEmailId, "");
    //            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
    //            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
    //            var client = new SendGridClient(apiKey);
    //            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString());
    //            var subject = sub;
    //            var to = new EmailAddress(toEmailId, "");
    //            var plainTextContent = "";
    //            var htmlContent = Message;
    //            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

    //            var attachments = new List<SendGrid.Helpers.Mail.Attachment>();
    //            var attachment = new SendGrid.Helpers.Mail.Attachment()
    //            {
    //                Content = Imgfile,
    //                Type = "image/png/pdf",
    //                Filename = filename,
    //                Disposition = "inline",
    //                ContentId = "Logo"
    //            };
    //            attachments.Add(attachment);
    //            msg.AddAttachments(attachments);
    //            //var response = client.SendEmailAsync(msg);
    //            //string res = Convert.ToString(response.IsCompleted);
    //            //if (res == "Accepted")
    //            //{
    //            //    ret = 1;
    //            //}
    //            //else
    //            //{
    //            //    ret = 0;
    //            //}
    //            //attachments.Dispose();
    //            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
    //            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
    //            string res = Convert.ToString(response.StatusCode);
    //            if (res == "Accepted")
    //            {
    //                ret = 1;
    //            }
    //            else
    //            {
    //                ret = 0;
    //            }
    //        }
    //        else
    //        {

    //            Common objCommon = new Common();
    //            DataSet dsconfig = null;
    //            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY,SUBJECT_OTP", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);
    //            var fromAddress = new System.Net.Mail.MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString());
    //            var toAddress = new System.Net.Mail.MailAddress(toEmailId, "");
    //            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
    //            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
    //            var client = new SendGridClient(apiKey);
    //            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["SUBJECT_OTP"].ToString());
    //            var subject = sub;
    //            var to = new EmailAddress(toEmailId, "");
    //            var plainTextContent = "";
    //            var htmlContent = Message;
    //            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
    //            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
    //            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
    //            string res = Convert.ToString(response.StatusCode);
    //            if (res == "Accepted")
    //            {
    //                ret = 1;
    //            }
    //            else
    //            {
    //                ret = 0;
    //            }


    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //    return ret;

    //}
    //private int OutLook_Email(string Message, string toEmailId, string sub)
    //{

    //    int ret = 0;
    //    try
    //    {
    //        Common objCommon = new Common();
    //        DataSet dsconfig = null;
    //        //dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,USER_PROFILE_SUBJECT,CollegeName", "EMAILSVCPWD,USER_PROFILE_SENDERNAME,COMPANY_EMAILSVCID AS MASTERSOFT_GRID_MAILID,SENDGRID_PWD AS MASTERSOFT_GRID_PASSWORD,SENDGRID_USERNAME AS MASTERSOFT_GRID_USERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
    //        //dsconfig = objCommon.FillDropDown("REFF", "SLIIT_EMAIL,USER_PROFILE_SUBJECT,CollegeName", "SLIIT_EMAIL_PWD,USER_PROFILE_SENDERNAME,COMPANY_EMAILSVCID AS MASTERSOFT_GRID_MAILID,SENDGRID_PWD AS MASTERSOFT_GRID_PASSWORD,SENDGRID_USERNAME AS MASTERSOFT_GRID_USERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);

    //        dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,CollegeName", "EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
    //        SmtpMail oMail = new SmtpMail("TryIt");
    //        oMail.From = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
    //        oMail.To = toEmailId;
    //        oMail.Subject = sub;
    //        oMail.HtmlBody = Message;
    //        // SmtpServer oServer = new SmtpServer("smtp.live.com");
    //        SmtpServer oServer = new SmtpServer("smtp.office365.com"); // modify on 29-01-2022
    //        oServer.User = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
    //        oServer.Password = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();
    //        oServer.Port = 587;
    //        oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
    //        Console.WriteLine("start to send email over TLS...");
    //        EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
    //        oSmtp.SendMail(oServer, oMail);
    //        Console.WriteLine("email sent successfully!");
    //        ret = 1;
    //    }
    //    catch (Exception ep)
    //    {
    //        Console.WriteLine("failed to send email with the following error:");
    //        Console.WriteLine(ep.Message);
    //        ret = 0;
    //    }
    //    return ret;
    //}
    //public int sendEmail(string Message, string mailId, string Subject)
    //{
    //    int status = 1;
    //    try
    //    {
    //        DataSet ds;
    //        ds = objCommon.FillDropDown("REFF", "SUBJECT_OTP", "", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]), string.Empty);
    //        //string Org =Convert.ToString(objCommon.FillDropDown("REFF", "SUBJECT_OTP", "", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]), string.Empty));
    //        string Org = (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) ? ds.Tables[0].Rows[0]["SUBJECT_OTP"].ToString() : string.Empty;
    //        string EMAILID = mailId.Trim();
    //        var fromAddress = objCommon.LookUp("REFF", "EMAILSVCID", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]));
    //        // any address where the email will be sending
    //        var toAddress = EMAILID.Trim();
    //        //Password of your gmail address

    //        var fromPassword = objCommon.LookUp("REFF", "(EMAILSVCPWD)", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]));
    //        // Passing the values and make a email formate to display
    //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
    //        MailMessage msg = new MailMessage();
    //        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();

    //        msg.From = new System.Net.Mail.MailAddress(fromAddress, Org);
    //        msg.To.Add(new System.Net.Mail.MailAddress(toAddress));
    //        msg.Subject = Subject;

    //        msg.IsBodyHtml = true;
    //        msg.Body = Message;
    //        System.Net.Mail.Attachment attachment;
    //        string file = ViewState["FileName"].ToString();
    //        //If want to send attachment in email
    //        if (file != string.Empty || file != "") //Added By Rishabh B. on 11012022
    //        {
    //            if (fuAttachment.HasFile)
    //            {
    //                attachment = new System.Net.Mail.Attachment(Server.MapPath("~/TempDocument/") + "\\" + ViewState["FileName"].ToString());
    //                msg.Attachments.Add(attachment);
    //            }
    //        }
    //        //smtp.enableSsl = "true";
    //        smtp.Host = "smtp.gmail.com";
    //        smtp.Port = 587;
    //        smtp.UseDefaultCredentials = false;
    //        smtp.EnableSsl = true;
    //        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
    //        smtp.Credentials = new System.Net.NetworkCredential(fromAddress.Trim(), fromPassword.Trim());
    //        //ServicePointManager.ServerCertificateValidationCallback =
    //        //    delegate(object s, X509Certificate certificate,
    //        //    X509Chain chain, SslPolicyErrors sslPolicyErrors)
    //        //    { return true; };
    //        //smtp.Send(msg);

    //        ServicePointManager.ServerCertificateValidationCallback =
    //          delegate(object s, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
    //          X509Chain chain, SslPolicyErrors sslPolicyErrors)
    //          { return true; };
    //        smtp.Send(msg);
    //        return status = 1;
    //    }

    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //    //return status;
    //}
    #endregion Comment

    #region Bind Listview
    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            ds = objFetch.GetStudentListForSendMail(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                PnlStudentmeeting.Visible = true;
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                lvStudents.Visible = true;
            }
            else
            {
                PnlStudentmeeting.Visible = false;
                objCommon.DisplayUserMessage(updpnl, "No record found.", this.Page);
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_Bulk EmailSending-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    #endregion Bind Listview

}