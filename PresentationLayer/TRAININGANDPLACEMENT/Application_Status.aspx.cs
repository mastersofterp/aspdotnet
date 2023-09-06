using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using BusinessLogicLayer.BusinessLogic;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Collections.Generic;
using System.Net.Mail;
using System.Collections.Specialized;
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
using System.IO;
using System.Web.Services;
using System.Web.Script.Services;
using System.Text;
using SendGrid;
using SendGrid.Helpers;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Generic;
using System.Net.Mail;
using DynamicAL_v2;
//using _NVP;
using System.Collections.Specialized;
using EASendMail;


public partial class EXAMINATION_Projects_Application_Status : System.Web.UI.Page
{
    Common objCommon = new Common();
    UAIMS_Common objUCommon = new UAIMS_Common();
    TPController objCompany = new TPController();
    TrainingPlacement objTP = new TrainingPlacement();
    //UserController objUC = new UserController();
    SendEmailCommon objSendEmail = new SendEmailCommon(); //Object Creation
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
                    //this.CheckPageAuthorization();

                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();

                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                        // lblHelp.Text = objCommon.GetPageHelp(int.Parse(Request.QueryString["pageno"].ToString()));
                    }
                    ViewState["action"] = "add";
                    objCommon.FillDropDownList(ddlJobAnnouncement, "ACD_TP_REGISTER A INNER JOIN ACD_TP_COMPSCHEDULE B ON (A.SCHEDULENO=B.SCHEDULENO) INNER JOIN ACD_TP_COMPANY C ON (C.COMPID=B.COMPID)", "DISTINCT B.SCHEDULENO", "(CONCAT(C.COMPNAME,'-',INTERVIEWFROM,'-',INTERVIEWTO)) as COMPNAME", "STUDCONFIRM>0", " COMPNAME");
                    BindListViewJobProfile();
                    Session["EmailFileAttachemnt"] = null;
                    ViewState["folderPath"] = null;
                }
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.Page_Load --> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private void BindListViewJobProfile()
    {
        try
        {
            if (ddlstatus.SelectedValue == "1")
            {
                int Scheduleno = Convert.ToInt32(ddlJobAnnouncement.SelectedValue);
                int status = 1;
                DataSet ds = objCompany.GetStudentOnApplStatusLV(status, Scheduleno);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["IDNO"] = ds.Tables[0].Rows[0]["IDNO"].ToString();
                    lvApplicationStatus.DataSource = ds;
                    lvApplicationStatus.DataBind();
                    hfcount.Value = ds.Tables[0].Rows.Count.ToString();
                    string s1 =ds.Tables[0].Rows[0]["STATUS"].ToString();
                    //if (s1.Equals("Confirmed"))
                    //{


                    //    tablelv.Attributes.Add("class", "hom_but_a");
                    //    myDiv.Attributes.Add("class", "top_rounded");
                    //}
                }
                else {
                    objCommon.DisplayMessage(Page, "No Records Available for this Selection", Page);
                    lvApplicationStatus.Visible = false;
                
                }
            }
            else if (ddlstatus.SelectedValue == "2")
            {
                int Scheduleno = Convert.ToInt32(ddlJobAnnouncement.SelectedValue);
                int Status = 2;

                DataSet ds = objCompany.GetStudentOnApplStatusLV(Status, Scheduleno);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lvApplicationStatus.DataSource = ds;
                    lvApplicationStatus.DataBind();
                    hfcount.Value = ds.Tables[0].Rows.Count.ToString();
                    lvApplicationStatus.Visible = true;
                }
                else { 
                
                    objCommon.DisplayMessage(Page,"No Record Found For This Selection",Page);
                }

            }

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUCommon.ShowError(Page, "TRAININGANDPLACEMENT_Masters_TPJobLoc.BindListViewJobLoc -> " + ex.Message + " " + ex.StackTrace);
            else
                objUCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    protected void btnApply_Click(object sender, EventArgs e)
    {
        BindListViewJobProfile();
    }
    protected void lnkdetails_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Pop", "Show();", true);

        foreach (ListViewItem item in lvApplicationStatus.Items)
        {
            LinkButton app = (item.FindControl("lnkdetails") as LinkButton);
         
            string FILENAME = app.ToolTip;
            string filePath = Server.MapPath("~/ACADEMIC/RESUME/" + FILENAME);


            string filee = Server.MapPath("~/Transactions/TP_PDF_Reader.aspx");
            FileInfo file = new FileInfo(filePath);

            if (file.Exists)
            {
                Session["sb"] = filePath.ToString();
                //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Alert", "window.open('"+filee+"'); alert('Your message here' );", true);

                //Response.Redirect("~/TRAININGANDPLACEMENT/Transactions/TP_PDF_Reader.aspx");

                string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("TRAININGANDPLACEMENT")));

                url +=  "ACADEMIC/RESUME/" + FILENAME;
                //string url = filePath;
              

                divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
                divMsg.InnerHtml += " window.open('" + url + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
                divMsg.InnerHtml += " </script>";
               
            }  

           
          

        }


    

    }

    public void fileDownload(string FILE_NAME, string filepath)
    {
        ResponseFile(Page.Request, Page.Response, FILE_NAME, filepath, 1024000);
    }

    public static bool ResponseFile(HttpRequest _Request, HttpResponse _Response, string _FILE_NAME, string _fullPath, long _speed)
    {
        try
        {
            FileStream myFile = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryReader br = new BinaryReader(myFile);
            try
            {
                _Response.AddHeader("Accept-Ranges", "bytes");
                _Response.Buffer = false;
                long fileLength = myFile.Length;
                long startBytes = 0;

                int pack = 10240; //10K bytes
                int sleep = (int)Math.Floor((double)(1000 * pack / _speed)) + 1;
                if (_Request.Headers["Range"] != null)
                {
                    _Response.StatusCode = 206;
                    string[] range = _Request.Headers["Range"].Split(new char[] { '=', '-' });
                    startBytes = Convert.ToInt64(range[1]);
                }
                _Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                if (startBytes != 0)
                {
                    _Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1, fileLength));
                }
                _Response.AddHeader("Connection", "Keep-Alive");
                _Response.ContentType = "application/octet-stream";
                _Response.AddHeader("Content-Disposition", "attachment;filename=" + _FILE_NAME);

                br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                int maxCount = (int)Math.Floor((double)((fileLength - startBytes) / pack)) + 1;

                for (int i = 0; i < maxCount; i++)
                {
                    if (_Response.IsClientConnected)
                    {
                        _Response.BinaryWrite(br.ReadBytes(pack));
                    }
                    else
                    {
                        i = maxCount;
                    }
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                br.Close();
                myFile.Close();
            }
        }
        catch
        {
            return false;
        }
        return true;
    }
    protected void lnkdownload_Click(object sender, EventArgs e)
    {
        foreach (ListViewItem item in lvApplicationStatus.Items)
        {
            LinkButton app = (item.FindControl("lnkdownload") as LinkButton);
            //string FILE_NAME = ViewState["FILENAME"].ToString();
           // string FILE_NAME = ViewState["FILENAME"].ToString();
            string FILENAME = app.ToolTip;
            string filePath = Server.MapPath("~/ACADEMIC/RESUME/" + FILENAME);
            FileInfo file = new FileInfo(filePath);

            if (file.Exists)
            {
                DownloadFile(Server.MapPath("~/ACADEMIC/Resume/"), FILENAME);
            }
            else
            {
                objCommon.DisplayMessage("Requested file is not available to download", this.Page);

                return;
            }

            //if (file.Exists)
            //{
            //    Response.Clear();
            //    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
            //    Response.AddHeader("Content-Length", file.Length.ToString());
            //    Response.ContentType = "application/octet-stream";
            //    Response.Flush();
            //    Response.TransmitFile(file.FullName);
            //    Response.End();
            //}
            //else
            //{
            //    objCommon.DisplayMessage("Requested file is not available to download", this.Page);

            //    //return;
            //}
        }
    }
    protected void btnCanceltab_Click(object sender, EventArgs e)
    {
        ddlJobAnnouncement.SelectedIndex = 0;
        ddlstatus.SelectedIndex = 0;
        lvApplicationStatus.Visible = false;
    }
    protected void BtnSendEmail_Click(object sender, EventArgs e)
    {
        try
        {
            string studentIds = string.Empty; string filename = ""; int count = 0;

            string folderPath = Server.MapPath("~/EmailUploadFile/");


            DataSet dsUserContactnew = null;
            TPController objTC = new TPController();
            //dsUserContactnew = objTC.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(90000));

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            Session["EmailFileAttachemnt"] = fuemailattach.FileName;
            if (Convert.ToString(Session["EmailFileAttachemnt"]) != string.Empty || Convert.ToString(Session["EmailFileAttachemnt"]) != "")
            {
                fuemailattach.PostedFile.SaveAs(folderPath + Path.GetFileName(Convert.ToString(Session["EmailFileAttachemnt"])));
                ViewState["folderPath"] = folderPath + Path.GetFileName(Convert.ToString(Session["EmailFileAttachemnt"]));
            }

            DataSet dsUserContact = null;
            //if (rbtodayselect.SelectedValue == "1")
                //dsUserContact = objUC.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(Request.QueryString["pageno"]));
            //else
                //dsUserContact = objUC.GetEmailTamplateandUserDetails("", "", "ERP", Convert.ToInt32(2830));
            dsUserContact = objCompany.EmailTemplate();

            if (dsUserContact.Tables[0] != null && dsUserContact.Tables[0].Rows.Count > 0)
            {
                foreach (ListViewDataItem item in lvApplicationStatus.Items)
                {                  
                    CheckBox chkround = (item.FindControl("chkRow") as CheckBox);
                    HiddenField hdIdno = (item.FindControl("hdIdno") as HiddenField); 
                    string emailid = chkround.ToolTip;
                    string idno = null;
                    idno = hdIdno.Value;
                    if (chkround.Checked == true)
                    {
                        count++;
                        string message = "";
                    
                        DataSet dsconfig1 = objCommon.FillDropDown("ACD_STUDENT A inner join ACD_TP_REGISTER B on (A.IDNO=B.IDNO)", "A.EMAILID", "B.IDNO", "B.IDNO = '" +Convert.ToInt32( idno) + "' and A.EMAILID<> ''", "");
                        string ToEmailId = dsconfig1.Tables[0].Rows[0]["EMAILID"].ToString();
                      //  DataSet dsconfig = objCommon.FillDropDown("REFF", "SENDGRID_APIKEY", "COMPANY_EMAILSVCID,EMAILSVCPWD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", "");

                       // string API = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
                       // string FromMailId = dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString();
                       // string FromMailPass = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();
                       //// reff = objCompany.reffemaildetails();
                       // Executes(txtDescription.Text, ToEmailId, txtSubject.Text, FromMailId, API, "Training And Placement");

                        //------------11-08-2023

                        DataSet dsconfig = objCommon.FillDropDown("REFF", "SENDGRID_STATUS", "", "OrganizationId=" + Convert.ToInt32(Session["OrgId"]), string.Empty);
                        string SENDGRID_STATUS = dsconfig.Tables[0].Rows[0]["SENDGRID_STATUS"].ToString();

                        string path = Server.MapPath("~/ACADEMIC/OfferLetter/");

                        int status1 = 0;
                        string email_type = string.Empty;
                        string Link = string.Empty;
                        int sendmail = 0;

                        message = txtDescription.Text;
                        // string filename = string.Empty;
                        filename = Convert.ToString(Session["EmailFileAttachemnt"]);
                        string Imgfile = string.Empty;
                        Byte[] Imgbytes = null;
                        if (filename != string.Empty)
                        {
                            path = path + filename;
                            string LogoPath = path;
                            Imgbytes = File.ReadAllBytes(LogoPath);
                            Imgfile = Convert.ToBase64String(Imgbytes);
                        }
                        status1 = objSendEmail.SendEmail(ToEmailId, message, txtSubject.Text, "", "", null, filename, Imgbytes, "image/png/pdf");

                        if (status1 == 1)
                        {
                            objCommon.DisplayUserMessage(this.Page, "Mail Sent Successfully.", this.Page);
                        }
                        else
                        {
                            objCommon.DisplayMessage(this.Page, "Failed To send email", this.Page);
                        }


                        //---------11-08-2023

                    }
                        else
                        {
                            objCommon.DisplayMessage("Please Check Student Status to Whom You want to send Email.", this.Page);
                        }
                   // }
                }
            }
            else
            {
                objCommon.DisplayMessage( "Email Tamplate Not Found!!!", this.Page);
            }
            if (count == 0)
            {
                objCommon.DisplayMessage( "Please select atleast one student for email send !!!", this.Page);
                
                Session["EmailFileAttachemnt"] = null;
                return;
            }
            else
            {
                string path = Server.MapPath("~/EmailUploadFile/" + fuemailattach.FileName);
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not  
                {
                    file.Delete();
                }
                //objCommon.DisplayMessage( "Email send Successfully !!!", this.Page);
                
                Session["EmailFileAttachemnt"] = null;
                ViewState["folderPath"] = null;
            }
            txtSubject.Text = string.Empty;
            txtDescription.Text = string.Empty;
        }
        catch (Exception ex)
        {

        }       
    }

   
    //protected void Execute(string message, string toSendAddress, string Subject, string ManualMesage, string firstname, string username, string filename, string ReffEmail, string ReffPassword)
    //{
    //    try
    //    {
    //        SmtpMail oMail = new SmtpMail("TryIt");

    //        oMail.From = ReffEmail;

    //        oMail.To = toSendAddress;

    //        oMail.Subject = Subject;

    //        oMail.HtmlBody = message;
    //        //if (rbtodayselect.SelectedValue == "1")
    //        //{
    //        oMail.HtmlBody = oMail.HtmlBody.Replace("[USERFIRSTNAME]", firstname.ToString());
    //        oMail.HtmlBody = oMail.HtmlBody.Replace("[MESSAGE]", ManualMesage.ToString());
    //        //}
    //        //else
    //        {
    //            oMail.HtmlBody = oMail.HtmlBody.Replace("[UA_FULLNAME]", firstname.ToString());
    //        }
    //        if (filename != string.Empty)
    //        {
    //            oMail.AddAttachment(System.Web.HttpContext.Current.Server.MapPath("~/EmailUploadFile/" + filename + ""));
    //        }
    //        // SmtpServer oServer = new SmtpServer("smtp.live.com");
    //        SmtpServer oServer = new SmtpServer("smtp.office365.com"); // modify on 29-01-2022

    //        oServer.User = ReffEmail;
    //        oServer.Password = ReffPassword;

    //        oServer.Port = 587;

    //        oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

    //        EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();


    //        oSmtp.SendMail(oServer, oMail);
    //        //Common objCommon = new Common();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}





    //---start

    private void Executes(string Message, string toEmailId, string Subject, string FromMailId, string APIKey, string TomailSub)
    {
        try
        {
            var fromAddress = new System.Net.Mail.MailAddress(FromMailId, TomailSub);
            var toAddress = new System.Net.Mail.MailAddress(toEmailId, "");
            var apiKey = APIKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(FromMailId, TomailSub);
            var subject = Subject.ToString();
            var to = new EmailAddress(toEmailId, "");
            var plainTextContent = "";
            var htmlContent = Message;


            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            //if (CC_Mail != "")
            //{
            //    string[] CCId = CC_Mail.Split(',');
            //    foreach (string CCEmail in CCId)
            //    {
            //        msg.AddCcs(CCEmail); //mm.CC.Add(new MailAddress(CCEmail)); //Adding Multiple CC email Id  
            //    }
            //}
            var bytes = File.ReadAllBytes(ViewState["folderPath"].ToString());
            var file = Convert.ToBase64String(bytes);
            //byte[] byteData = Encoding.ASCII.GetBytes(File.ReadAllText(lblAttach.Text));
            msg.Attachments = new List<SendGrid.Helpers.Mail.Attachment>
                {

                    new SendGrid.Helpers.Mail.Attachment
                    {
                        Content =file,//lblAttach.Text,//Convert.ToBase64String( byteData),
                        Filename = Session["EmailFileAttachemnt"].ToString(),
                        Type = "application/pdf",
                        Disposition = "attachment"

                    }
                };

            var response = client.SendEmailAsync(msg).Result;

        }
        catch (Exception ex)
        {

        }
    }

    //-----end

    static async Task Execute(string Message, string useremail, string subject, string ReffEmail, string ReffPassword, string filename, string FirstName)
        {

        try
            {
            SmtpMail oMail = new SmtpMail("TryIt");

            oMail.From = ReffEmail;

            oMail.To = useremail;

            oMail.Subject = subject;

            oMail.HtmlBody = Message;

            oMail.HtmlBody = oMail.HtmlBody.Replace("[USERFIRSTNAME]", FirstName.ToString());
            oMail.HtmlBody = oMail.HtmlBody.Replace("[EMAILID]", useremail.ToString());
            //oMail.HtmlBody = oMail.HtmlBody.Replace("[OTP]", otp.ToString());

            SmtpServer oServer = new SmtpServer("smtp.office365.com");
            oServer.User = ReffEmail;
            oServer.Password = ReffPassword;

            oServer.Port = 587;

            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

            EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();



            oSmtp.SendMail(oServer, oMail);

            }
        catch (Exception ex)
            {

            }

        }
    protected void btnSubmit_Click(object sender, System.EventArgs e)
    {
        int count = 0;
        foreach (ListViewDataItem item in lvApplicationStatus.Items)
        {
            
            TextBox txtremark = (item.FindControl("txtRemark") as TextBox);
            string Remark = txtremark.Text;

            CheckBox chkround = (item.FindControl("chkRow") as CheckBox);
            HiddenField hdidno = (item.FindControl("hdIdno") as HiddenField);
            if (chkround.Checked == false && txtremark.Text == "")
            {

                objCommon.DisplayMessage(" Please Select atleast One Student and Enter Remark.", this.Page);
                return;
            }
            else if (chkround.Checked == true && txtremark.Text != "")
            {
                count++;
                //int scheduleno = Convert.ToInt32(ddlJobAnnouncement.SelectedValue);
                //int idno = Convert.ToInt32(ViewState["IDNO"]);
                int scheduleno = Convert.ToInt32(ddlJobAnnouncement.SelectedValue);
                int idno = Convert.ToInt32(hdidno.Value);

                //TextBox txtremark = (item.FindControl("txtRemark") as TextBox);
                //string Remark = txtremark.Text;

                CustomStatus CS = (CustomStatus)objCompany.UpdateRemarkApplStatus(Remark, scheduleno, idno);
                if (CS.Equals(CustomStatus.RecordUpdated))
                {
                    objCommon.DisplayMessage("Record Saved Successfully.", this.Page);
                    //objCommon.DisplayMessage(upnlroundselection, "Offer Letter Send Successfully.", this.Page);
                    ViewState["action"] = null;
                    //clear();
                    ddlJobAnnouncement.SelectedIndex = 0;
                    ddlstatus.SelectedIndex = 0;
                    //lvApplicationStatus.Visible = false;
                    lvApplicationStatus.DataSource = null;
                    lvApplicationStatus.DataBind();
                }
            }
            //else if (chkround.Checked == true && txtremark.Text != "")
            //{
            //    objCommon.DisplayMessage(" Please Enter Remark.", this.Page); 
            //    return;
            //}
            else if (chkround.Checked == true && txtremark.Text == "")
            {
                objCommon.DisplayMessage(" Please Enter Remark.", this.Page);
                return;
            }
            else if (chkround.Checked == false && txtremark.Text != "")
            {
                objCommon.DisplayMessage(" Please Select atleast One Student .", this.Page);
                return;
            }

        }


        //if (count == 0)
        //{

        //    objCommon.DisplayMessage(Page, " Please Select Atleast One Student to Update Records.", Page);
        //}
        }

    protected void btnCancelEmail_Click(object sender, System.EventArgs e)
    {
        txtSubject.Text = string.Empty;
        txtDescription.Text = string.Empty;
    }



    public void DownloadFile(string path, string fileName)
    {
        try
        {
            FileStream sourceFile = new FileStream((path + "\\" + fileName), FileMode.Open);
            long fileSize = sourceFile.Length;
            byte[] getContent = new byte[(int)fileSize];
            sourceFile.Read(getContent, 0, (int)sourceFile.Length);
            sourceFile.Close();
            sourceFile.Dispose();

            Response.ClearContent();
            Response.Clear();
            Response.BinaryWrite(getContent);
            Response.ContentType = GetResponseType(fileName.Substring(fileName.IndexOf('.')));
            Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");

            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            Response.Clear();
            Response.ContentType = "text/html";
            Response.Write("Unable to download the attachment.");
        }
    }
    private string GetResponseType(string fileExtension)
    {
        switch (fileExtension.ToLower())
        {
            case ".doc":
                return "application/vnd.ms-word";
                break;

            case ".docx":
                return "application/vnd.ms-word";
                break;

            case ".xls":
                return "application/ms-excel";
                break;

            case ".xlsx":
                return "application/ms-excel";
                break;

            case ".pdf":
                return "application/pdf";
                break;

            case ".ppt":
                return "application/vnd.ms-powerpoint";
                break;

            case ".txt":
                return "text/plain";
                break;

            case "":
                return "";
                break;

            default:
                return "";
                break;
        }
    }
    protected void clsepop_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        txtSubject.Text = string.Empty;
        txtDescription.Text = string.Empty;
    }
}

