using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using IITMS.SQLServer.SQLDAL;
using System.Net;
using System.Web.Configuration;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Net.Security;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
//using mastersofterp;


public partial class ACADEMIC_SendBulkEmailSms : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    ImportDataController IDC = new ImportDataController();
    FetchDataController objFet = new FetchDataController();
    //CONNECTIONSTRING
    string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

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
            //Check Session
            if (Session["userno"] == null && Session["username"] == null &&
                Session["usertype"] == null && Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }

            //Page Authorization
            //CheckPageAuthorization();

            //Set the Page Title
            Page.Title = Session["coll_name"].ToString();

            //Load Page Help
            if (Request.QueryString["pageno"] != null)
            {
                //lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
            }

            PopulateDropDown();
            objCommon.SetLabelData("0", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]), Convert.ToInt32(Session["userno"]));//Set label -  
            objCommon.SetHeaderLabelData(Convert.ToString(Request.QueryString["pageno"]));  // Set Page Header  - 
        }

        divMsg.InnerHtml = string.Empty;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }

    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=BulkStudentLogin.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=BulkStudentLogin.aspx");
        }
    }

    private void PopulateDropDown()
    {
        try
        {
            objCommon.FillDropDownList(ddlAdmBatch, "ACD_ADMBATCH", "BATCHNO", "BATCHNAME", "BATCHNO > 0", "BATCHNAME DESC");
            objCommon.FillDropDownList(ddlColg, "ACD_COLLEGE_MASTER", "COLLEGE_ID", "ISNULL(COLLEGE_NAME,'')+(CASE WHEN LOCATION IS NULL THEN '' ELSE ' - 'END) +ISNULL(LOCATION,'') COLLEGE_NAME", "COLLEGE_ID IN(" + Session["college_nos"] + ") AND COLLEGE_ID > 0 AND OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "COLLEGE_ID");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private void ShowMessage(string message)
    {
        if (message != string.Empty)
        {
            divMsg.InnerHtml += "<script type='text/javascript' language='javascript'> alert('" + message + "'); </script>";
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {

        string branch = GetBranch();
        branch = branch.Replace('$', ',');

        ViewState["DegreeNo"] = branch;
        string[] Branchno = branch.Split(',');
        try
        {

            if (branch == "0")
            {

                objCommon.DisplayUserMessage(upduser, "Please select at least one branch!", Page);
            }
            else
            {

                DataSet ds = new DataSet();
                //ds = objFetch.GetStudentListForSendMail(Convert.ToInt32(ddlCollege.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), Convert.ToInt32(ddlBranch.SelectedValue), Convert.ToInt32(ddlSem.SelectedValue));
                // DataSet ds = objCommon.FillDropDown("ACD_ONLINE_USER_UPLOAD U LEFT JOIN USER_ACC US ON (US.UA_NAME=U.USERNAME)", "CASE WHEN U.USERNAME IS NULL THEN '-' ELSE U.USERNAME END REGNO, 'HSNCU@Stud_2020' as DOBNEW, US.UA_MOBILE, US.UA_EMAIL, U.STUDNAME", "ISNULL(US.UA_NO,0) AS CREATED", "U.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND U.ADMBATCH=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + "AND U.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue), "REGNO");
                // DataSet ds = objCommon.FillDropDown("USER_ACC US LEFT JOIN ACD_USER_REGISTRATION UR ON (US.UA_NAME=UR.USERNAME) CROSS APPLY(SELECT TOP 1 DEGREENO, COLLEGE_ID FROM ACD_USER_BRANCH_PREF BP WHERE UR.USERNO=BP.USERNO) BP", "DISTINCT US.UA_NAME AS REGNO, US.UA_MOBILE", "US.UA_EMAIL, US.UA_FULLNAME AS STUDNAME", "UR.ADMBATCH=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + " AND US.UA_TYPE=2 AND BP.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND BP.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue), "REGNO");

                //DataSet ds = objCommon.FillDropDown("USER_ACC US LEFT JOIN ACD_USER_REGISTRATION UR ON (US.UA_NAME=UR.USERNAME) CROSS APPLY(SELECT TOP 1 DEGREENO, COLLEGE_ID FROM ACD_USER_BRANCH_PREF BP WHERE UR.USERNO=BP.USERNO) BP  INNER JOIN ACD_STUDENT ST ON (ST.IDNO=US.UA_IDNO AND ST.USERNO=UR.USERNAME)", "DISTINCT US.UA_NAME AS REGNO, US.UA_MOBILE", "US.UA_EMAIL, US.UA_FULLNAME AS STUDNAME", "UR.ADMBATCH=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + " AND US.UA_TYPE=2 AND ST.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND  ISNULL(ST.ADMCAN,0)=0   AND ISNULL(ST.CAN,0)=0  AND ST.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue), "REGNO");

                //Added for According to branch finding Data on dated 09/10/2020 by Swapnil Thakare

                //DataSet ds = objCommon.FillDropDown("USER_ACC US LEFT JOIN ACD_USER_REGISTRATION UR ON (US.UA_NAME=UR.USERNAME COLLATE Latin1_General_CI_AS) CROSS APPLY(SELECT TOP 1 DEGREENO, COLLEGE_ID FROM ACD_USER_BRANCH_PREF BP WHERE UR.USERNO=BP.USERNO) BP  INNER JOIN ACD_STUDENT ST ON (ST.IDNO=US.UA_IDNO AND ST.USERNO=UR.USERNAME)", "DISTINCT US.UA_NAME AS REGNO, US.UA_MOBILE", "US.UA_EMAIL, US.UA_FULLNAME AS STUDNAME", "UR.ADMBATCH=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + " AND US.UA_TYPE=2 AND ST.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " AND  ISNULL(ST.ADMCAN,0)=0   AND ISNULL(ST.CAN,0)=0 AND ST.BRANCHNO IN (" + branch + ") AND ST.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue), "REGNO");
                //ds = objCommon.FillDropDown("ACD_STUDENT S INNER JOIN ACD_STUDENT_RESULT SR ON S.IDNO = SR.IDNO INNER JOIN ACD_COLLEGE_DEGREE_BRANCH CDB ON (S.DEGREENO=CDB.DEGREENO AND S.BRANCHNO=CDB.BRANCHNO)", "DISTINCT SR.IDNO", "SR.REGNO,UPPER(S.STUDNAME)AS STUDNAME,S.EMAILID", "S.IDNO = SR.IDNO AND S.DEGREENO=CDB.DEGREENO AND S.BRANCHNO=CDB.BRANCHNO AND S.CAN=0 AND S.ADMCAN=0 AND SR.REGISTERED = 1 AND SR.EXAM_REGISTERED=1 AND (CANCEL IS NULL OR CANCEL = 0)  AND S.BRANCHNO=" + Convert.ToInt32(ddlBranch.SelectedValue) + " S.DEGREENO=" + Convert.ToInt32(ddlDegree.SelectedValue) + " S.ADMBATCH=" + Convert.ToInt32(ddlAdmBatch.SelectedValue) + " S.COLLEGE_ID=" + Convert.ToInt32(ddlColg.SelectedValue), "REGNO");
                ds = objFet.GetStudentListForSendMailAndMsg(Convert.ToInt32(ddlColg.SelectedValue), Convert.ToInt32(ddlDegree.SelectedValue), branch.ToString(), Convert.ToInt32(ddlAdmBatch.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvStudents.DataSource = ds.Tables[0];
                    lvStudents.DataBind();
                    lvStudents.Visible = true;
                    btnSendSMS.Enabled = true;
                }
                else
                {
                    objCommon.DisplayUserMessage(upduser, "No Record Found!", Page);
                    lvStudents.DataSource = null;
                    lvStudents.DataBind();
                    lvStudents.Visible = false;
                    btnSendSMS.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void ddlColg_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBranch.Items.Clear();
        objCommon.FillDropDownList(ddlDegree, "ACD_DEGREE D , ACD_COLLEGE_DEGREE C", "D.DEGREENO", "D.DEGREENAME", "D.DEGREENO=C.DEGREENO AND C.DEGREENO>0 AND C.COLLEGE_ID=" + ddlColg.SelectedValue + " AND C.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "DEGREENO");
    }

    protected void btnSendSMS_Click(object sender, EventArgs e)
    {

        string folderPath = Server.MapPath("~/TempDocument/");
        //string folderPath = @"E:\Images\"; // Your path Where you want to save other than Server.MapPath
        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists. Create it.
            Directory.CreateDirectory(folderPath);
        }

        //Save the File to the Directory (Folder).
        Session["FileName"] = fuAttachment.FileName;
        if (Session["FileName"] != string.Empty || Session["FileName"] != "")  // ADDED FOR CHECKING IF FILE EXISTS OR NOT ON 09-10-2020 BY SWAPNIL T
        {
            fuAttachment.SaveAs(folderPath + Path.GetFileName(fuAttachment.FileName));
        }

        Session["result"] = "0";
        int i = 0;
        int msgtype = 0;

        if (rbEmail.Checked == true || rbBoth.Checked == true)
        {
            if (txtSubject.Text == string.Empty)
            {
                objCommon.DisplayMessage(upduser, "Please Enter the Subject!", this.Page);
                return;
            }
            else if (txtMatter.Text == string.Empty)
            {
                objCommon.DisplayMessage(upduser, "Please Enter the Message!", this.Page);
                return;
            }
        }
        else if (rbSMS.Checked == true)
        {
            if (txtMatter.Text == string.Empty)
            {
                objCommon.DisplayMessage(upduser, "Please Enter the Message!", this.Page);
                return;
            }
        }

        foreach (ListViewDataItem item in lvStudents.Items)
        {
            CheckBox chk = item.FindControl("chkRow") as CheckBox;

            if (chk.Checked == false)
            {
                i++;
            }
        }

        if (i == lvStudents.Items.Count)
        {
            objCommon.DisplayMessage(upduser, "Please Select Students from the Student List!", this.Page);
            return;
        }
        int response = 0;
        foreach (ListViewDataItem item in lvStudents.Items)
        {
            CheckBox chk = item.FindControl("chkRow") as CheckBox;
            Label lblreg = item.FindControl("lblreg") as Label;
            Label lblStudName = item.FindControl("lblstud") as Label;
            Label lblEmailId = item.FindControl("lblEmailId") as Label;
            Label lblMobileNo = item.FindControl("lblMobileNo") as Label;
            
            if (chk.Checked == true)
            {
                string useremail = lblEmailId.Text == string.Empty ? objCommon.LookUp("acd_student a inner join user_acc b on (a.idno=b.UA_IDNO)", "b.UA_EMAIL", "UA_NAME='" + lblreg.Text.Replace("'", "`").Trim() + "' and UA_NAME IS NOT NULL") : lblEmailId.Text;
                string message = "Dear Student,<br />" + txtMatter.Text;
                string subject = txtSubject.Text;

                if (rbBoth.Checked == true)
                {
                    msgtype = 3;
                    
                    string path = Server.MapPath("~/TempDocument/");
                    Task<int> task = Execute(message, useremail, subject, Convert.ToString(Session["FileName"]), path);
                    response = task.Result;
                    // sendmail(useremail, subject, message);
                    //sendEmail(message, useremail, subject);
                    //SendMailBYSendgrid(message, useremail, subject);

                    string Mobileno = lblMobileNo.Text == string.Empty ? objCommon.LookUp("acd_student a inner join user_acc b on (a.idno=b.UA_IDNO)", "b.UA_MOBILE", "UA_NAME='" + lblreg.Text.Replace("'", "`").Trim() + "' and UA_NAME IS NOT NULL") : lblMobileNo.Text;
                    if (Mobileno != "")
                    {
                        string msg = "DEAR STUDENT,\n" + txtMatter.Text + "\nMAKAUT, WB";
                        SendSMS(Mobileno, msg, "1007501635681027335");
                    }
                }
                else if (rbEmail.Checked == true)
                {
                    msgtype = 1;

                    // sendmail(useremail, subject, message);
                    //sendEmail(message, useremail, subject);
                    //SendMailBYSendgrid(message, useremail, subject);
                    string path = Server.MapPath("~/TempDocument/");
                    Task<int> task = Execute(message, useremail, subject, Convert.ToString(Session["FileName"]), path);
                    response = task.Result;

                }
                else if (rbSMS.Checked == true)
                {
                    msgtype = 2;
                    string msg = "DEAR STUDENT,\n" + txtMatter.Text + "\nMAKAUT, WB";
                    string Mobileno = lblMobileNo.Text == string.Empty ? objCommon.LookUp("acd_student a inner join user_acc b on (a.idno=b.UA_IDNO)", "b.UA_MOBILE", "UA_NAME='" + lblreg.Text.Replace("'", "`").Trim() + "' and UA_NAME IS NOT NULL") : lblMobileNo.Text;
                    if (Mobileno != "")
                    {
                        SendSMS(Mobileno, msg, "1007501635681027335");
                    }
                }

                int status = IDC.InsertBulkMessageLog(lblreg.Text, msgtype, txtSubject.Text, txtMatter.Text, Convert.ToInt32(Session["userno"].ToString()), Session["ipAddress"].ToString(), lblEmailId.Text, lblMobileNo.Text);

                if (status != 1)
                {
                    objCommon.DisplayMessage(upduser, "Something went wrong!", this.Page);
                }
            }
        }

        //  File.Delete(Server.MapPath(Server.MapPath("~/TempDocument/") + "\\" + Session["FileName"].ToString())); //Delete Sending file after send done.

        if (Session["result"].ToString() == "1")
        {
            objCommon.DisplayMessage(upduser, "Message Sent Successfully!!", this.Page);
        }
        else
        {
            objCommon.DisplayMessage(upduser, "Sorry, Your Application not configured with mail server, Please contact Admin Department !!", this.Page);
        }
    }

    public void SendSMS(string mobno, string message, string TemplateID = "")
    {
        try
        {
            string url=string.Empty;
            string uid = string.Empty;
            string pass = string.Empty;
            DataSet ds = objCommon.FillDropDown("Reff", "SMSProvider", "SMSSVCID,SMSSVCPWD", "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                url = string.Format("http://" + ds.Tables[0].Rows[0]["SMSProvider"].ToString() + "?");
                uid = ds.Tables[0].Rows[0]["SMSSVCID"].ToString();
                pass = ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
                WebRequest request = HttpWebRequest.Create("" + url + "ID=" + uid + "&PWD=" + pass + "&PHNO=" + mobno + "&TEXT=" + message + "&TemplateID=" + TemplateID + "");
                WebResponse response = request.GetResponse();
                System.IO.StreamReader reader = new StreamReader(response.GetResponseStream());
                string urlText = reader.ReadToEnd(); // it takes the response from your url. now you can use as your need      
                //return urlText;  
                Session["result"] = 1;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void SendSMS1(string Mobile, string text)
    {
        string status = "";
        try
        {
            string Message = string.Empty;
            DataSet ds = objCommon.FillDropDown("Reff", "SMSProvider", "SMSSVCID,SMSSVCPWD", "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("http://" + ds.Tables[0].Rows[0]["SMSProvider"].ToString() + "?"));
                request.ContentType = "text/xml; charset=utf-8";
                request.Method = "POST";

                string postDate = "ID=" + ds.Tables[0].Rows[0]["SMSSVCID"].ToString();
                postDate += "&";
                postDate += "Pwd=" + ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
                postDate += "&";
                postDate += "PhNo=91" + Mobile;
                postDate += "&";
                postDate += "Text=" + text;

                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postDate);
                request.ContentType = "application/x-www-form-urlencoded";

                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse _webresponse = request.GetResponse();
                dataStream = _webresponse.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                status = reader.ReadToEnd();
                Session["result"] = 1;
            }
            else
            {
                status = "0";
            }
        }
        catch
        {
            throw;
        }
    }

    #region Backup 22092020 SMTP.GMAIL
    //public void sendmail(string toEmailId, string Sub, string body)
    //{
    //    try
    //    {
    //        DataSet dsconfig = null;
    //        MailMessage mail = new MailMessage();
    //       // string message = string.Empty;            
    //       // var message = (dynamic)null;
    //        dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,USER_PROFILE_SUBJECT,CollegeName", "EMAILSVCPWD,USER_PROFILE_SENDERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
    //        var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString(), "");
    //        var toAddress = new MailAddress(toEmailId, "");
    //        // string fromPassword = clsTripleLvlEncyrpt.ThreeLevelDecrypt(Session["EMAILSVCPWD"].ToString());
    //        string fromPassword = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();
    //        var smtp = new SmtpClient
    //        {
    //            Host = "smtp.gmail.com",
    //            Port = 587,
    //            EnableSsl = true,
    //            DeliveryMethod = SmtpDeliveryMethod.Network,
    //            UseDefaultCredentials = false,
    //            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
    //        };
    //        using (var message = new MailMessage(fromAddress, toAddress)
    //        {
    //            Subject = Sub,
    //            Body = body,
    //            BodyEncoding = System.Text.Encoding.UTF8,
    //            SubjectEncoding = System.Text.Encoding.Default,
    //            IsBodyHtml = true

    //        })
    //            if (fuAttachment.HasFile)
    //            {
    //                mail.Attachments.Add(new Attachment(fuAttachment.PostedFile.InputStream, fuAttachment.FileName));
    //            }
    //        mail.IsBodyHtml = true;
    //      //  mail.Body = message;
    //        {
    //            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
    //            smtp.Send(mail);

    //            if (DeliveryNotificationOptions.OnSuccess == DeliveryNotificationOptions.OnSuccess)
    //            {
    //                Session["result"] = "1";
    //                //Storing the details of sent email
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}
    #endregion

    //public int SendMailBYSendgrid(string message, string emailid, string subject)
    //{
    //    int ret = 0;
    //    try
    //    {
    //        DataSet dsconfig = null;
    //        dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD", "COMPANY_EMAILSVCID <> '' AND SENDGRID_USERNAME<> ''", string.Empty);
    //        string fromAddress = dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString();
    //        string user = dsconfig.Tables[0].Rows[0]["SENDGRID_USERNAME"].ToString();
    //        string pwd = dsconfig.Tables[0].Rows[0]["SENDGRID_PWD"].ToString();
    //        string decrFromPwd = Common.DecryptPassword(pwd);
    //        //==============================================================
    //        var myMessage = new SendGridMessage();
    //        string file = Session["FileName"].ToString();
    //        //If want to send attachment in email
    //        if (file != string.Empty || file != "")  // ADDED FOR CHECKING IF FILE EXISTS OR NOT ON 09-10-2020 BY SWAPNIL T
    //        {
    //            if (fuAttachment.HasFile)
    //            {
    //                //MemoryStream stream = new MemoryStream(data.attachment);
    //                myMessage.AddAttachment(Server.MapPath("~/TempDocument/") + "\\" + Session["FileName"].ToString());
    //            }
    //        }
    //        myMessage.From = new MailAddress(fromAddress);
    //        myMessage.AddTo(emailid);
    //        myMessage.Subject = subject;
    //        myMessage.Html = message;


    //        var credentials = new NetworkCredential(user, decrFromPwd);
    //        var transportWeb = new Web(credentials);
    //        transportWeb.Deliver(myMessage);
    //        ret = 1;
    //        Session["result"] = 1;
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //    //return transportWeb.DeliverAsync(myMessage);
    //    return ret;
    //}


    //public void sendEmail1(string Message, string toEmailId, string sub)
    //{
    //     try
    //    {
    //        //FileStream fStream;
    //        //DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/TempDocument/"));
    //        MailMessage mail = new MailMessage();
    //        //if (Session["FileName"] != string.Empty || Session["FileName"] != "")
    //        //{
    //        //   Attachment attachFile = new Attachment(Server.MapPath("~/TempDocument/") + "\\" + Session["FileName"].ToString());
    //        //   mail.Attachments.Add(attachFile);
    //        //}           

    //        DataSet dsconfig = null;            
    //        // string message = string.Empty;            
    //        var message = (dynamic)null;
    //        //dsconfig = objCommon.FillDropDown("REFF", "EMAILSVCID,USER_PROFILE_SUBJECT,CollegeName", "EMAILSVCPWD,USER_PROFILE_SENDERNAME,MASTERSOFT_GRID_MAILID,MASTERSOFT_GRID_PASSWORD,MASTERSOFT_GRID_USERNAME", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);
    //        dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD", "COMPANY_EMAILSVCID <> '' AND SENDGRID_USERNAME<> ''", string.Empty);
    //        var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "HSNCU");
    //        var toAddress = new MailAddress(toEmailId, "");
    //        // string fromPassword = clsTripleLvlEncyrpt.ThreeLevelDecrypt(Session["EMAILSVCPWD"].ToString());
    //        string fromPassword = dsconfig.Tables[0].Rows[0]["SENDGRID_PWD"].ToString();
    //        string SendgridUserId = dsconfig.Tables[0].Rows[0]["SENDGRID_USERNAME"].ToString();

           
    //        var smtp = new SmtpClient
    //        {
    //            //Host = "smtp.gmail.com",
    //            Host = "smtp.sendgrid.net",
    //            Port = 587,
               
    //            EnableSsl = true,
    //            DeliveryMethod = SmtpDeliveryMethod.Network,
    //            Timeout = 10000,
    //            UseDefaultCredentials = false,
    //            Credentials = new NetworkCredential(SendgridUserId, fromPassword)
    //        };
    //        using (message = new MailMessage(fromAddress, toAddress)
    //        {
    //            Subject = sub,
    //            Body = Message,
    //            BodyEncoding = System.Text.Encoding.UTF8,
    //            SubjectEncoding = System.Text.Encoding.Default,
    //            IsBodyHtml = true
    //        })
    //            if (fuAttachment.HasFile)
    //            {
    //                for (int i = 0; i < Request.Files.Count; i++)
    //                {
    //                    HttpPostedFile fu = Request.Files[i];
    //                    mail.Attachments.Add(new Attachment(Server.MapPath("~/TempDocument/") + "\\" + Session["FileName"].ToString())); //fuAttachment.PostedFile.InputStream, fu.FileName
    //                }
    //            }

    //        // Attachment attachFile = new Attachment(Server.MapPath("~/TempDocument/") + "\\" + Session["FileName"].ToString());

              

    //          //mail.Attachments.Add(new Attachment(Path.GetFileName( + )));
    //          message.IsBodyHtml = true;
    //          mail.Body = Message;
    //          mail.From = fromAddress;
    //          mail.To.Clear();
    //          mail.To.Add(toAddress);
    //        {
    //            //ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
    //            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
    //            smtp.Send(mail);
    //            Session["result"] = 1;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}


    public int sendEmail(string message, string mailId, string Subject)
    {
        int status = 1;
        try
        {


            string EMAILID = mailId.Trim();
            var fromAddress = objCommon.LookUp("REFF", "LTRIM(RTRIM(EMAILSVCID))", ""); //"incharge_mis_ghrcemp@raisoni.net";   

            // any address where the email will be sending
            var toAddress = EMAILID.Trim();
            //Password of your gmail address


            var fromPassword = objCommon.LookUp("REFF", "LTRIM(RTRIM(EMAILSVCPWD))", "");   // const string fromPassword = "thebestofall";  
            // Passing the values and make a email formate to display

            MailMessage msg = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            msg.From = new MailAddress(fromAddress, "SBU");
            msg.To.Add(new MailAddress(toAddress));
            msg.Subject = Subject;

            msg.IsBodyHtml = true;
            msg.Body = message;
            System.Net.Mail.Attachment attachment;
            string file = Session["FileName"].ToString();
            //If want to send attachment in email
            if (file != string.Empty || file != "")  // ADDED FOR CHECKING IF FILE EXISTS OR NOT ON 09-10-2020 BY SWAPNIL T
            {
                if (fuAttachment.HasFile)
                {
                    attachment = new System.Net.Mail.Attachment(Server.MapPath("~/TempDocument/") + "\\" + Session["FileName"].ToString());
                    msg.Attachments.Add(attachment);
                }
            }
            //smtp.enableSsl = "true";
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential(fromAddress.Trim(), fromPassword.Trim());
            ServicePointManager.ServerCertificateValidationCallback =
                delegate(object s, X509Certificate certificate,
                X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };
            smtp.Send(msg);

        }

        catch (Exception ex)
        {
            throw;
        }
        return status;
    }
       

    protected void rbEmail_CheckedChanged(object sender, EventArgs e)
    {
        if (rbEmail.Checked == true)
        {
            btnSendSMS.Text = "Send Email";
            divSubject.Visible = true;
        }
        else
        {
            btnSendSMS.Text = "Send Email/SMS";
            divSubject.Visible = false;
        }
    }

    protected void rbSMS_CheckedChanged(object sender, EventArgs e)
    {
        if (rbSMS.Checked == true)
        {
            btnSendSMS.Text = "Send SMS";
            divSubject.Visible = false;
        }
        else
        {
            btnSendSMS.Text = "Send Email/SMS";
            divSubject.Visible = false;
        }
    }

    protected void rbBoth_CheckedChanged(object sender, EventArgs e)
    {
        if (rbBoth.Checked == true)
        {
            btnSendSMS.Text = "Send Email & SMS";
            divSubject.Visible = true;
        }
        else
        {
            btnSendSMS.Text = "Send Email/SMS";
            divSubject.Visible = false;
        }
    }


    private void test()
    {
    }

    private string GetBranch()
    {
        string branchNo = "";
        string branchno = string.Empty;
        int X = 0;
        // pnlStudent.Update();
        foreach (ListItem item in ddlBranch.Items)
        {
            if (item.Selected == true)
            {
                branchNo += item.Value + '$';
                X = 1;
            }
        }

        if (X == 0)
        {
            branchNo = "0";
        }

        if (branchNo != "0")
        {
            branchno = branchNo.Substring(0, branchNo.Length - 1);
        }
        else
        {
            branchno = branchNo;
        }
        if (branchno != "")
        {
            string[] bValue = branchno.Split('$');
        }
        // degreeno = degreeno.Substring(0, degreeno.Length - 1);
        //}
        return branchno;
    }

    protected void ddlDegree_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlBranch.Items.Clear();
            if (ddlDegree.SelectedIndex > 0)
            {
                // objCommon.FillDropDownList(ddlBranch, "ACD_BRANCH B , ACD_COLLEGE_DEGREE_BRANCH AD", "B.BRANCHNO", "B.SHORTNAME", "B.BRANCHNO = AD.BRANCHNO AND AD.COLLEGE_ID=" + ddlColg.SelectedValue + " AND AD.DEGREENO = " + ddlDegree.SelectedValue + " ", "BRANCHNO");
                DataSet ds = objCommon.FillDropDown("ACD_BRANCH B INNER JOIN ACD_COLLEGE_DEGREE_BRANCH AD ON ( B.BRANCHNO = AD.BRANCHNO )", "DISTINCT(B.BRANCHNO)", "B.LONGNAME", "B.BRANCHNO > 0 AND AD.DEGREENO = " + Convert.ToInt32(ddlDegree.SelectedValue) + " AND AD.OrganizationId=" + Convert.ToInt32(Session["OrgId"]), "B.BRANCHNO");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ddlBranch.Items.Add(new ListItem(Convert.ToString(ds.Tables[0].Rows[i][1]), Convert.ToString(ds.Tables[0].Rows[i][0])));
                }
            }
            else
            {
                ShowMessage("Please select college/school");
                ddlDegree.Focus();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        lvStudents.Visible = false;
    }
   
    public string MessageBody(string Name, string msg)
    {
        const string EmailTemplate = "<html><body>" +
                              "<div align=\"center\">" +
                              "<table style=\"width:602px;border:#1F75E2 3px solid\" cellspacing=\"0\" cellpadding=\"0\">" +
                               "<tr>" +
                               "<td>" + "</tr>" +
                               "<tr>" +
                              "<td width=\"100%\" style=\"vertical-align:top;text-align:left;padding:20px 15px 20px 15px;height:200px;FONT-FAMILY: Verdana;FONT-SIZE: 12px\">#content</td>" +
                              "</tr>" +
                              "</table>" +
                              "</div>" +
                              "</body></html>";
        StringBuilder mailBody = new StringBuilder();
        mailBody.AppendFormat("<h1>Greetings !!</h1>");
        mailBody.AppendFormat("Dear" + " " + "<b>" + Name + "," + "</b>");   //b
        mailBody.AppendFormat("<br />");
        mailBody.AppendFormat("<br />");
        mailBody.AppendFormat("<b>" + msg + "</b>" + "<br/><br/>");       //b
        mailBody.AppendFormat("This is an auto generated response to your email. Please do not reply to this mail.");
        mailBody.AppendFormat("<br /><br /><br /><br />Regards,<br />");   //bb
        mailBody.AppendFormat("Sarala Birla University, Ranchi<br /><br />");   //bb

        string Mailbody = mailBody.ToString();
        string nMailbody = EmailTemplate.Replace("#content", Mailbody);

        //string CCemail = CC_Email;

        //sendEmail(nMailbody, Email, "One-Time Password to Lock Marks", CCemail);
        return nMailbody;
    }

    static async Task<int> Execute(string Message, string toEmailId, string sub, string filename, string path)
    {
        int ret = 0;

        try
        {
            
            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);
            var fromAddress = new MailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "SBU");
            var toAddress = new MailAddress(toEmailId, "");

            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), "MAKAUT");
            var subject = sub;
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;
            
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            
            ////If want to send attachment in email
            //if (filename != string.Empty || filename != "")  // ADDED FOR CHECKING IF FILE EXISTS OR NOT ON 09-10-2020 BY SWAPNIL T
            //{
               
            //        //MemoryStream stream = new MemoryStream(data.attachment);
               
            //    msg.AddAttachment(Server.MapPath("~/TempDocument/") + "\\" + filename.ToString());
                
            //}
            var response = await client.SendEmailAsync(msg);
            string res = Convert.ToString(response.StatusCode);
            if (res == "Accepted")
            {
                ret = 1;
            }
            else
            {
                ret = 0;
            }


        }
        catch (Exception ex)
        {
            throw;
        }
        return ret;
    }
}