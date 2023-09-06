//=================================================================================
// PROJECT NAME  : U-AIMS(ONLINE ADMISSION)                                                          
// MODULE NAME   : EDCUTATIONAL DETAILS      
// CREATION DATE : 05-09-2015                                                     
// CREATED BY    : MANISH A CHAWADE                             
// MODIFIED BY   : 
// MODIFICATIONS : 

// MODIFIED DATE :   
//=================================================================================

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
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Net;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;

using System.Net;
using System.Net.Mail;
using System.Text;


public partial class ReceiveApplicationStatus : System.Web.UI.Page
{
    Common objCommon = new Common();
    //NewUser objnu = new NewUser();
    applicationReceivedController objnuc = new applicationReceivedController();



    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["userno"] == null || Session["username"] == null ||
                Session["usertype"] == null || Session["userfullname"] == null)
            {
                Response.Redirect("~/default.aspx");
            }
            if (!Page.IsPostBack)
            {
                //PopulateDropDown();
                //Page Authorization
                this.CheckPageAuthorization();               
                pnlDetails.Visible = false;
                //this.bindList();             
            }
            divMsg.InnerHtml = string.Empty;
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "domain.Page_Load-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }
    private void CheckPageAuthorization()
    {
        if (Request.QueryString["pageno"] != null)
        {
            //Check for Authorization of Page
            if (Common.CheckPage(int.Parse(Session["userno"].ToString()), Request.QueryString["pageno"].ToString(), int.Parse(Session["loginid"].ToString()), 0) == false)
            {
                Response.Redirect("~/notauthorized.aspx?page=ReceiveApplicationStatus.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ReceiveApplicationStatus.aspx");
        }
    }
    private void ShowStudents()
    {
        try
        {
            DataSet ds = objCommon.FillDropDown("ACD_NEWUSER_REGISTRATION S INNER JOIN ACD_USER_REGISTRATION R ON (R.USERNO = S.USERNO) LEFT OUTER JOIN ACD_BRANCH B ON (B.BRANCHNO = R.BRANCHNO)", "R.USERNO", "R.USERNAME,R.FIRSTNAME AS STUDENTNAME,DBO.FN_DESC('DEGREENAME',R.DEGREENO)DEGREE,(CASE WHEN R.BRANCHNO = 0 THEN '-' ELSE B.LONGNAME END) AS BRANCH,(CASE WHEN S.APPLICATION_VERIFIED = 1 THEN 'RECEIVED' ELSE 'NOT RECEIVED' END) AS APPLICATION_STATUS", "S.APPLICATION_VERIFIED = 1 and R.USERNO = " + ViewState["userno"].ToString() + "", string.Empty);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                lvStudents.DataSource = ds;
                lvStudents.DataBind();
                lvStudents.Visible = true;
            }
            else
            {
                lvStudents.DataSource = null;
                lvStudents.DataBind();
                lvStudents.Visible = false;
            }
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ReceiveApplicationStatus.ShowStudents()> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }


    protected void btnShow_Click(object sender, EventArgs e)
    {
        pnlDetails.Visible = false;
        lvStudents.DataSource = null;
        lvStudents.DataBind();
       
        string appid = objCommon.LookUp("ACD_USER_REGISTRATION", "USERNAME", "USERNAME='" + txtApplicationID.Text.Trim() + "'");
        if (String.IsNullOrEmpty(appid))
        {
            objCommon.DisplayMessage(updpnlUser,"Please Enter Correct Application ID.", this.Page);
            return;
        }
        string UserNocheck = objCommon.LookUp("ACD_USER_PROFILE_STATUS US INNER JOIN ACD_USER_REGISTRATION R ON US.USERNO = R.USERNO INNER JOIN ACD_NEWUSER_REGISTRATION NR ON US.USERNO=NR.USERNO", "CONFIRM_STATUS", "R.USERNAME ='" + txtApplicationID.Text.Trim() + "'");
        if (String.IsNullOrEmpty(UserNocheck))
        {
            objCommon.DisplayMessage(updpnlUser,"Application Id Not Found", this.Page);
        }
        else
        {
            try
            {
                int USERNO = Convert.ToInt32(objCommon.LookUp("ACD_USER_REGISTRATION", "USERNO", "USERNAME='" + txtApplicationID.Text.Trim() + "'"));
                ViewState["userno"] = USERNO;
                DataSet ds = null;
                ds = objnuc.GetExstStudentDetailsByApplicationID(appid);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblStudentname.Text = ds.Tables[0].Rows[0]["STUDENTNAME"].ToString();
                    lblDateOfBirth.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
                    lblAddress.Text = ds.Tables[0].Rows[0]["LADDRESS"].ToString();
                    lblEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                    lblMobile.Text = ds.Tables[0].Rows[0]["MOBILENO"].ToString();
                    lblPinCode.Text = ds.Tables[0].Rows[0]["LPINCODE"].ToString();
                    lblProgram.Text = ds.Tables[0].Rows[0]["DEGREE"].ToString();
                    lblBranch.Text = ds.Tables[0].Rows[0]["BRANCH"].ToString();
                    if (ds.Tables[0].Rows[0]["PHOTO"].ToString() != string.Empty)
                    {
                        imgPhoto.ImageUrl = "../showimage.aspx?id=" + ViewState["userno"].ToString() + "&type=ADMISSION";
                    }
                    else
                    {
                        imgPhoto.ImageUrl = "~/images/nophoto.jpg";
                    }
                    pnlDetails.Visible = true;
                    ShowStudents();
                }
                else
                {
                    pnlDetails.Visible = false;
                }
            }
            catch (Exception ex)
            {
                if (Convert.ToBoolean(Session["error"]) == true)
                    objCommon.ShowError(Page, "ReceiveApplicationStatus.btnShow_Click-> " + ex.Message + " " + ex.StackTrace);
                else
                    objCommon.ShowError(Page, "Server UnAvailable");
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    
    protected void btnRecieved_Click(object sender, EventArgs e)
    {
        try
        {
            int userno = Convert.ToInt32(ViewState["userno"]);

            CustomStatus cs = (CustomStatus)objnuc.UpdateStudentApplicationStatus(userno);

            if (cs.Equals(CustomStatus.RecordUpdated))
            {
                objCommon.DisplayMessage("Application Received.", this.Page);
                ShowStudents();
                string emailid = string.Empty;
                emailid = objCommon.LookUp("ACD_USER_REGISTRATION", "EMAILID", "USERNO = " + ViewState["userno"].ToString() + "");
                if (emailid != string.Empty)
                {

                    string subject = "Application Recieved";
                    //string message = "Application recieved Successfully";

                    string EmailTemplate = "<html><body>" +
                                        "<div align=\"center\">" +
                                        "<table style=\"width:602px;border:#DB0F10 3px solid\" cellspacing=\"0\" cellpadding=\"0\">" +
                                         "<tr>" +
                                         "<td>" + "</tr>" +
                                         "<tr>" +
                                        "<td width=\"100%\" style=\"vertical-align:top;text-align:left;padding:20px 15px 20px 15px;height:200px;FONT-FAMILY: Verdana;FONT-SIZE: 12px\">#content</td>" +
                                        "</tr>" +
                                        "<tr>" +
                                        "<td width=\"100%\" style=\"vertical-align:middle;text-align:left;padding:20px 15px 20px 15px;height:100px;FONT-FAMILY: Verdana;FONT-SIZE: 11px\"><b>With Best Wishes<br/>Sarala Birla University, Ranchi</td>" +
                                        "</tr>" +
                                        "</table>" +
                                        "</div>" +
                                        "</body></html>";
                    StringBuilder mailBody = new StringBuilder();
                    mailBody.AppendFormat("<h1>Greetings !!</h1>");
                    mailBody.AppendFormat( "Dear "+objCommon.LookUp("ACD_USER_REGISTRATION","FIRSTNAME","USERNO="+ ViewState["userno"].ToString()));
                    mailBody.AppendFormat("<br />");
                    mailBody.AppendFormat("<br />");
                    mailBody.AppendFormat("<p>THANK YOU FOR SUBMITING ADMISSION FORM AND WE RECIEVED SUCCESSFULLY.</p>");
                    mailBody.AppendFormat("<br />");
                    string Mailbody = mailBody.ToString();
                    string nMailbody = EmailTemplate.Replace("#content", Mailbody);
                   sendEmail(nMailbody, emailid, subject);

                    string mobileno = objCommon.LookUp("ACD_USER_REGISTRATION", "MOBILENO", "USERNO = " + ViewState["userno"].ToString() + "");
                    this.SendSMS(mobileno, "THANK YOU FOR SUBMITING ADMISSION FORM AND WE RECIEVED SUCCESSFULLY");
                }
            }
            else
                objCommon.DisplayMessage("Application not Received.", this.Page);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "ReceiveApplicationStatus.btnRecieved_Click-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public int sendEmail(string message, string emailid, string subject)
    {
        int ret = 0;
        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");


        DataSet dsconfig = null;
        dsconfig = objCommon.FillDropDown("ACD_ADMCONFIGURATION", "EMAILID EMAILSVCID", "PASSWORD EMAILSVCPWD", string.Empty, string.Empty);
        string emailfrom = dsconfig.Tables[0].Rows[0]["EMAILSVCID"].ToString();
        string emailpass = dsconfig.Tables[0].Rows[0]["EMAILSVCPWD"].ToString();
        //int fascility = dsconfig.Tables[0].Rows[0]["FASCILITY"].ToString();

        if (emailfrom != "" && emailpass != "")
        {

            mail.From = new MailAddress(emailfrom);
            string MailFrom = emailfrom;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(emailfrom, emailpass);
            SmtpServer.EnableSsl = true;
            string aa = string.Empty;
            mail.Subject = subject;
            mail.To.Clear();
            mail.To.Add(emailid);

            mail.IsBodyHtml = true;
            mail.Body = message;
            SmtpServer.Send(mail);
            if (DeliveryNotificationOptions.OnSuccess == DeliveryNotificationOptions.OnSuccess)
            {
                return ret = 1;
            }

        }
        return ret = 0;
    }

    public void SendSMS(string Mobile, string text)
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
            }
            else
            {
                status = "0";
            }

        }
        catch
        {

        }

    }

    
}
