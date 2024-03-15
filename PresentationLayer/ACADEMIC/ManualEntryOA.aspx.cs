/*
 Created By         : Nikhil Lambe
 Created Date     : 09-02-2023
 Description        : To get student for manual fee entry for admission portal.
*/
//-----------------------------------------------------------------------------------------------------------------------------
//--Version   Modified    On Modified         By Purpose
//-----------------------------------------------------------------------------------------------------------------------------
//--1.0.1    16-02-2024     Rutuja         Changes for the PHD
//--------------------------------------------- -------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Data;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using CCA.Util;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data.SqlClient;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.IO;
using System.Threading.Tasks;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;



public partial class ACADEMIC_ManualEntryOA : System.Web.UI.Page
{
    Common objCommon = new Common();

    UAIMS_Common objUCommon = new UAIMS_Common();

    OnlineAdmissionController Admcontroller = new OnlineAdmissionController();

    string spName = string.Empty; string spParameters = string.Empty; string spValue = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                    CheckPageAuthorization();
                    //Set the Page Title
                    Page.Title = Session["coll_name"].ToString();
                    //Load Page Help
                    if (Request.QueryString["pageno"] != null)
                    {
                    }
                }

            }
            else
            {
                ViewState["DONE"] = 0;
            }
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
                Response.Redirect("~/notauthorized.aspx?page=ManualEntryOA.aspx");
            }
        }
        else
        {
            //Even if PageNo is Null then, don't show the page
            Response.Redirect("~/notauthorized.aspx?page=ManualEntryOA.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
          
            lblName.Text = string.Empty;
            lblEmail.Text = string.Empty;
            lblMobile.Text = string.Empty;
            lblDegree.Text = string.Empty;
            lblFees.Text = string.Empty;
            lblPayStatus.Text = string.Empty;
            if (!txtAppId.Text.ToString().Equals(string.Empty))
            {
                string appId = string.Empty;
                appId = txtAppId.Text.ToString().TrimEnd();
                spName = "PKG_ACD_OA_GET_SEARCH_USER_FOR_MANUAL_ENTRY";
                spParameters = "@P_APPID";
                spValue = "" + appId + "";
                DataSet dsGet = null;
                dsGet = objCommon.DynamicSPCall_Select(spName, spParameters, spValue);
                DataSet ds_degreenamePHD = objCommon.FillDropDown("ACD_college_degree_branch AD INNER JOIN ACD_PHD_REGISTRATION AUR ON (AD.BRANCHNO=AUR.DEPARTMENT_NO)", "AD.DEGREENO", "USERNO", "USERNO=" + Convert.ToInt32(ViewState["USERNO"]) + "AND USERNAME =" + appId + " AND UGPGOT=3", "");
                if (dsGet != null && dsGet.Tables.Count > 0 && dsGet.Tables[0].Rows.Count > 0)
                {
                    divDetails.Visible = true;
                    lblName.Text = dsGet.Tables[0].Rows[0]["FIRSTNAME"].ToString().Equals(string.Empty) ? "-" : dsGet.Tables[0].Rows[0]["FIRSTNAME"].ToString();
                    lblEmail.Text = dsGet.Tables[0].Rows[0]["EMAILID"].ToString().Equals(string.Empty) ? "-" : dsGet.Tables[0].Rows[0]["EMAILID"].ToString();
                    lblMobile.Text = dsGet.Tables[0].Rows[0]["MOBILENO"].ToString().Equals(string.Empty) ? "-" : dsGet.Tables[0].Rows[0]["MOBILENO"].ToString();
                    lblDegree.Text = dsGet.Tables[0].Rows[0]["DEGREE"].ToString().Equals(string.Empty) ? "-" : dsGet.Tables[0].Rows[0]["DEGREE"].ToString();
                    lblFees.Text = dsGet.Tables[0].Rows[0]["FEES"].ToString().Equals(string.Empty) ? "-" : dsGet.Tables[0].Rows[0]["FEES"].ToString();
                    lblPayStatus.Text = dsGet.Tables[0].Rows[0]["PAY_STATUS"].ToString().Equals(string.Empty) ? "-" : dsGet.Tables[0].Rows[0]["PAY_STATUS"].ToString();
                    ViewState["AMOUNT"] = dsGet.Tables[0].Rows[0]["FEES"].ToString();
                    ViewState["USERNO"] = dsGet.Tables[0].Rows[0]["USERNO"].ToString();
                    if (lblPayStatus.Text.ToString().Equals("Done"))
                    {
                        if (ViewState["DONE"].ToString() == "0")
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`Payment Already Done.`)", true);
                        lblPayStatus.ForeColor = System.Drawing.Color.Green;
                        btnSubmit.Visible = false;


                        if (lblDegree.Text != "Doctor of Philosophy")
                        {
                            btnreceipt.Visible = true;
                        }
                    }
                    else
                    {
                        lblPayStatus.ForeColor = System.Drawing.Color.Red;
                        btnSubmit.Visible = true;
                        btnreceipt.Visible = false;
                    }
                    btnCancel.Visible = true;
                }
            else
            {
                divDetails.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`No record found.`)", true);
                btnCancel.Visible = false;
                btnreceipt.Visible = false;
                btnSubmit.Visible = false;
                return;
            }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int userNo = Convert.ToInt32(ViewState["USERNO"].ToString());          
            //decimal amount = Convert.ToDecimal(ViewState["AMOUNT"].ToString());
           //string amount ;
            decimal amount = Convert.ToDecimal(ViewState["AMOUNT"].ToString());
            //string amount = ViewState["AMOUNT"].ToString();
        
            //int amount = Convert.ToInt32(ViewState["AMOUNT"].ToString());
            string appId = string.Empty;
            appId = txtAppId.Text.ToString().TrimEnd();
            spName = "PKG_ACD_OA_UPDATE_MANUAL_ENTRY";
            spParameters = "@P_USERNO,@P_AMOUNT,@P_APPID";
            spValue = "" + userNo + "," + amount + "," + appId;
            string isNRI = objCommon.LookUp("ACD_USER_REGISTRATION", "ISNULL(NRI_STATUS,0)", "USERNO=" + Convert.ToInt32(ViewState["USERNO"]));
            string isUGPGOT = objCommon.LookUp("ACD_USER_REGISTRATION", "ISNULL(UGPGOT,0)", "USERNO=" + Convert.ToInt32(ViewState["USERNO"]));
            DataSet ds_degreenameMCA = objCommon.FillDropDown("ACD_DEGREE AD INNER JOIN ACD_USER_REGISTRATION AUR ON (AD.DEGREENO=AUR.DEGREENO)", "AD.CODE", "USERNO", " USERNO=" + userNo + "", "");
            string DegreeName = string.Empty;
            if (ds_degreenameMCA.Tables.Count > 0 && ds_degreenameMCA.Tables[0].Rows.Count > 0)
            {
                // Check if the "CODE" column value is not DBNull
                if (ds_degreenameMCA.Tables[0].Rows[0]["CODE"] != DBNull.Value)
                {
                    DegreeName = ds_degreenameMCA.Tables[0].Rows[0]["CODE"].ToString();
                }
            }
            DataSet ds_degreenamePHD = objCommon.FillDropDown("ACD_college_degree_branch AD INNER JOIN ACD_PHD_REGISTRATION AUR ON (AD.BRANCHNO=AUR.DEPARTMENT_NO)", "AD.DEGREENO", "USERNO", "USERNO=" + userNo + "AND USERNAME =" + appId + " AND UGPGOT=3", "");
            string PHDDegree = string.Empty; 
            if (ds_degreenamePHD.Tables.Count > 0 && ds_degreenamePHD.Tables[0].Rows.Count > 0)
            {
                // Check if the "CODE" column value is not DBNull
                if (ds_degreenamePHD.Tables[0].Rows[0]["DEGREENO"] != DBNull.Value)
                {
                    PHDDegree = ds_degreenamePHD.Tables[0].Rows[0]["DEGREENO"].ToString();
                }
            }

    

            string lookupResult = objCommon.LookUp("ACD_DEGREE AD INNER JOIN ACD_USER_REGISTRATION AUR ON (AD.DEGREENO=AUR.DEGREENO)", "AD.DEGREENO", "USERNO=" + userNo + " AND USERNAME ='" + appId + "'");
            DataSet dsPay = null;
            dsPay = objCommon.DynamicSPCall_Select(spName, spParameters, spValue);
            if (dsPay != null && dsPay.Tables.Count > 0 && dsPay.Tables[0].Rows.Count > 0)
            {
                //<1.0.1> 
                if (dsPay.Tables[0].Rows[0]["OUTPUT"].ToString().Equals("1"))
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert(`Manual payment done successfully.`)", true);
                    dsPay = null;
                    userNo =0;
                    amount = 0;
                    spName = "";
                    spParameters = "";
                    spValue = "";
                    ViewState["DONE"] = 1;
                    btnSearch_Click(sender, e);
                    if (PHDDegree != "24")
                    {
                        if (isNRI.ToString().Equals("1"))
                        {
                            Task<int> x = SendMailForAppId_NRI();
                        }
                        else if (DegreeName.ToString().Equals("B.Tech."))
                        {
                            Task<int> x = SendMailForAppId();
                        }
                        else if (isUGPGOT.ToString().Equals("1"))
                        {
                            Task<int> x = SendMailForAppId_UG();
                        }
                        else if (DegreeName.ToString().Equals("MCA"))
                        {
                            Task<int> x = SendMailForAppId_MCA();

                        }
                        else if (isUGPGOT.ToString().Equals("2"))
                        {
                            Task<int> x = SendMailForAppId_PG();
                        }
                      //  ShowReport("OnlineFeePayment", "rptOnlineReceipt_Online_Adm.rpt");

                        btnreceipt.Visible = true;
                    }
                   //</1.0.1>
                    return;
              }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            lblName.Text = string.Empty;
            lblEmail.Text = string.Empty;
            lblMobile.Text = string.Empty;
            lblDegree.Text = string.Empty;
            lblFees.Text = string.Empty;
            lblPayStatus.Text = string.Empty;
            txtAppId.Text = string.Empty;
            btnSubmit.Visible = false;
            divDetails.Visible = false;
            btnCancel.Visible = false;
            btnreceipt.Visible = false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
     //<1.0.1> 
    protected async Task<int> SendMailForAppId_PG()
    {
        int ret = 0;
        // int userno = Convert.ToInt32(((UserDetails)(Session["user"])).UserNo);

        try
        {
            int userno = Convert.ToInt32(ViewState["USERNO"].ToString());      
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            string MyHtmlString = "";
            Common objCommon = new Common();
            DataSet dsconfig = null;
            DataSet ds_degreename = objCommon.FillDropDown("ACD_DEGREE AD INNER JOIN ACD_USER_REGISTRATION AUR ON (AD.DEGREENO=AUR.DEGREENO)", "AD.CODE", "USERNO", " USERNO=" + userno + "", "");
            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY,SUBJECT_OTP", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);

            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            var client = new SendGridClient(apiKey);

            var to = new SendGrid.Helpers.Mail.EmailAddress();

            #region comment
            //var to = Emailid;
            //foreach (var i in emails)
            //{
            //    to.Add(new EmailAddress(i));
            //}
            //var cc = new List<EmailAddress>();
            //foreach (var i in ccemails)
            //{
            //    cc.Add(new EmailAddress(i));
            //}
            #endregion


            string email = objCommon.LookUp("ACD_USER_REGISTRATION", "EMAILID", "USERNO=" + userno);
            string Name = objCommon.LookUp("ACD_USER_REGISTRATION", "FIRSTNAME", "USERNO=" + userno);
            ViewState["firstName"] = Name;
            string UserName = objCommon.LookUp("ACD_USER_REGISTRATION", "USERNAME", "USERNO=" + userno);
            string emailid = email.ToString().Trim().Replace("'", "");
            string DegreeName = ds_degreename.Tables[0].Rows[0]["CODE"].ToString();
            Session["DegreeName"] = DegreeName;
            //string emailid = ViewState["emailApp"].ToString().Trim().Replace("'", "");
            //string subject = "CIEAT Application ID";
            string subject = "Application Number";
            var toAddress = emailid.ToString().Trim().ToString();
            const string EmailTemplate = "<html><body>" +
                             "<div align=\"center\">" +
                             "<table style=\"width:602px;border:#1F75E2 3px solid\" cellspacing=\"0\" cellpadding=\"0\">" +
                              "<tr>" +
                              "<td>" + "</tr>" +
                              "<tr>" +
                             "<td width=\"100%\" style=\"vertical-align:top;text-align:left;padding:20px 15px 20px 15px;height:200px;FONT-FAMILY: Verdana;FONT-SIZE: 15px\">#content</td>" +
                             "</tr>" +
                             "<tr>" +
                //  "<td width=\"100%\" style=\"vertical-align:middle;text-align:left;padding:20px 15px 20px 15px;height:100px;FONT-FAMILY: Verdana;FONT-SIZE: 15px\"><b>Note : For further assistance you may send an email to <span style="color: blue;">admissionoffice@crescent.education</span> or Contact us at +91 9543277888 (9 AM to 4 PM, Mon to Friday).<br/> </td>" +
                           "<td width=\"100%\" style=\"vertical-align:middle;text-align:left;padding:20px 15px 20px 15px;height:100px;FONT-FAMILY: Verdana;FONT-SIZE: 15px\"><b>Note : For further assistance you may send an email to <span style=\"color: blue;\"><u>admissionoffice@crescent.education<u></span> or Contact us at +91 9543277888 (9 AM to 4 PM, Monday to Friday).<br/> </td>" +
                             "</tr>" +
                             "</table>" +
                             "</div>" +
                             "</body></html>";

            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendFormat("Dear <b>{0}</b>,", Name.Trim());
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("Congratulations!");
            mailBody.AppendFormat("<br /><br />");
            mailBody.AppendFormat("You have successfully submitted your online application form for " + DegreeName + " . Programme");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("Email ID :" + emailid);
            mailBody.AppendFormat("<br />");
            // mailBody.AppendFormat("CIEAT Application  : " + UserName.Trim());
            mailBody.AppendFormat("Application Number  : " + UserName.Trim());
            mailBody.AppendFormat("<br /><br/>");
            mailBody.AppendFormat("<b><u>Please read the below information and follow the same:<br/><br/></b></u>");
            mailBody.AppendFormat("1.All information related to CSBAT 2024 (Crescent School of Business Admission Test) / further admission process will be sent to this email id only");
            mailBody.AppendFormat("<br /><br/>");
            mailBody.AppendFormat("2.You are required to log in to the application portal using your user name and password and enter your Entrance Test (CAT, XAT, CMAT, ATMA, MAT, GMAT, TANCET) & Under Graduation Examination Marks immediately after the declaration of the results.");
            mailBody.AppendFormat("<br /><br/>");
            mailBody.AppendFormat("3.The application form submitted by you is attached in this email for your reference. Please quote the application number in all your future communications.");

            //mailBody.AppendFormat("<p>You can use this Email ID and Password to modify details you filled with the application.</p>");
            string Mailbody = mailBody.ToString();
            string nMailbody = EmailTemplate.Replace("#content", Mailbody);
            //int status = sendEmail(nMailbody, emailid, subject);
            int status = 0;
            //Task<int> task = Execute(nMailbody, emailid, subject);
            //status = task.Result;
            var msg = new SendGrid.Helpers.Mail.SendGridMessage()
            {
                From = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), ""),
                // Subject = "CIEAT Application ID",
                Subject = "Crescent University - " + DegreeName + ". Application Submission",
                HtmlContent = nMailbody.ToString(),


            };
            // int userno = Convert.ToInt32(Session["userno"].ToString());
            MemoryStream oAttachment1 = ShowGeneralExportReportForMailForApplication("Reports,Academic,rptPreviewForm_PG.rpt", "@P_USERNO=" + userno + ",@P_COLLEGE_CODE=" + 56, DegreeName);

            var bytesRpt = oAttachment1.ToArray();
            var fileRpt = Convert.ToBase64String(bytesRpt);
            byte[] test = (byte[])bytesRpt;
            string FileName = UserName.Trim() + "_APPLICATION";

            //var msg1 = MailHelper.CreateSingleEmail(msg.From, to, msg.Subject, "", msg.HtmlContent);
            //msg1.Attachments = new List<SendGrid.Helpers.Mail.Attachment>
            //    {
            //        new SendGrid.Helpers.Mail.Attachment
            //        {
            //            Content = Convert.ToBase64String(test),
            //            Filename = FileName,
            //            Type = "application/pdf",
            //            Disposition = "attachment"
            //        }
            //    };

            #region  comment
            //byte[] fileBytes = ConvertDataSetToExcel(ds1);
            //var file = Convert.ToBase64String(fileBytes);
            //msg1.AddAttachment("AbsentStudentReportWeekly.xlsx", file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            //msg1.AddAttachment(attachment);
            // msg1.AddAttachment(MailAttachmentFromBlob("Attachment.xlsx"));
            //if (cc.Count > 0)
            //{
            //    msg1.AddCcs(cc);
            //}
            #endregion

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
            //Console.WriteLine(msg1.Serialize());
            //var response = await client.SendEmailAsync(msg1).ConfigureAwait(false);
            ////var response = await client.SendEmailAsync(msg1); ;
            //string res = Convert.ToString(response.StatusCode);
            //ret = (res == "Accepted") ? 1 : 0;
            //objCommon.DisplayMessage(this.Page, ret.ToString(), this.Page);
            //SendEmailCommon objSendEmailCommon=new SendEmailCommon();
            DataSet ds = getModuleConfig(0);
            string email_type = string.Empty;
            string Link = string.Empty;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                email_type = ds.Tables[0].Rows[0]["EMAIL_TYPE"].ToString();
                Link = ds.Tables[0].Rows[0]["LINK"].ToString();

            }
            //if (ret == 0)
            //    ret = Amazon1(emailid, nMailbody.ToString(), "CIEAT Application ID", FileName, test);
            //else
            //    ret = await SendGrid(nMailbody.ToString(), nMailbody.ToString(), "CIEAT Application ID", userno, UserName.Trim());
            if (email_type == "2" && email_type != "")
            {
                // ret = await SendGrid(nMailbody.ToString(), emailid, "Crescent University - B.Tech. Application Submission", userno, UserName.Trim(), test);
                Task<int> ret1 = SendGrid(nMailbody.ToString(), emailid, "Crescent University - " + DegreeName + ". Application Submission", userno, UserName.Trim(), test);

            }
            else if (email_type == "4" && email_type != "")
            {
                ret = Amazon1(emailid, nMailbody.ToString(), "Crescent University - " + DegreeName + ". Application Submission", FileName, test);
            }
            if (ret == 1)
            {
                objCommon.DisplayMessage(this.Page, "Dear " + ViewState["firstName"].ToString().Trim() + " Congratulations! You have submitted the online application successfully. Your Application No. is " + UserName.Trim() + " For assistance, please call 9543277888.", this.Page);
                int userNO = Convert.ToInt32(ViewState["USERNO"].ToString());
                
            }
        }
        catch (Exception ex)
        {
            SendGrid.Helpers.Errors.Model.SendGridErrorResponse errorResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<SendGrid.Helpers.Errors.Model.SendGridErrorResponse>(ex.Message);
            Console.WriteLine(ex.Message);
        }
        return ret;
    }

    protected async Task<int> SendMailForAppId_UG()
    {
        int ret = 0;
        // int userno = Convert.ToInt32(((UserDetails)(Session["user"])).UserNo);

        try
        {
            int userno = Convert.ToInt32(ViewState["USERNO"].ToString());
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            string MyHtmlString = "";
            Common objCommon = new Common();
            DataSet dsconfig = null;
            DataSet ds_degreenameUG = objCommon.FillDropDown("ACD_DEGREE AD INNER JOIN ACD_USER_REGISTRATION AUR ON (AD.DEGREENO=AUR.DEGREENO)", "AD.CODE", "USERNO", " USERNO=" + userno + "", "");

            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY,SUBJECT_OTP", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);

            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            var client = new SendGridClient(apiKey);


            var to = new SendGrid.Helpers.Mail.EmailAddress();

            #region comment
            //var to = Emailid;
            //foreach (var i in emails)
            //{
            //    to.Add(new EmailAddress(i));
            //}
            //var cc = new List<EmailAddress>();
            //foreach (var i in ccemails)
            //{
            //    cc.Add(new EmailAddress(i));
            //}
            #endregion
       
            string email =  objCommon.LookUp("ACD_USER_REGISTRATION", "EMAILID", "USERNO=" + userno);
            string Name = objCommon.LookUp("ACD_USER_REGISTRATION", "FIRSTNAME", "USERNO=" + userno);
            ViewState["firstName"] = Name;
            string UserName = objCommon.LookUp("ACD_USER_REGISTRATION", "USERNAME", "USERNO=" + userno);
            string emailid = email.ToString().Trim().Replace("'", "");
            string DegreeName = ds_degreenameUG.Tables[0].Rows[0]["CODE"].ToString();
            string isNRI = objCommon.LookUp("ACD_USER_REGISTRATION", "ISNULL(NRI_STATUS,0)", "USERNO=" + Convert.ToInt32(ViewState["USERNO"]));
            //string emailid = ViewState["emailApp"].ToString().Trim().Replace("'", "");
            //string subject = "CIEAT Application ID";
            string subject = "Application No";
            var toAddress = emailid.ToString().Trim().ToString();
            const string EmailTemplate = "<html><body>" +
                             "<div align=\"center\">" +
                             "<table style=\"width:602px;border:#1F75E2 3px solid\" cellspacing=\"0\" cellpadding=\"0\">" +
                              "<tr>" +
                              "<td>" + "</tr>" +
                              "<tr>" +
                             "<td width=\"100%\" style=\"vertical-align:top;text-align:left;padding:20px 15px 20px 15px;height:200px;FONT-FAMILY: Verdana;FONT-SIZE: 15px\">#content</td>" +
                             "</tr>" +
                             "<tr>" +
                //  "<td width=\"100%\" style=\"vertical-align:middle;text-align:left;padding:20px 15px 20px 15px;height:100px;FONT-FAMILY: Verdana;FONT-SIZE: 15px\"><b>Note : For further assistance you may send an email to <span style="color: blue;">admissionoffice@crescent.education</span> or Contact us at +91 9543277888 (9 AM to 4 PM, Mon to Friday).<br/> </td>" +
                           "<td width=\"100%\" style=\"vertical-align:middle;text-align:left;padding:20px 15px 20px 15px;height:100px;FONT-FAMILY: Verdana;FONT-SIZE: 15px\"><b>Note : For further assistance you may send an email to <span style=\"color: blue;\"><u>admissionoffice@crescent.education<u></span> or Contact us at +91 9543277888 (9 AM to 4 PM, Mon to Friday).<br/> </td>" +
                             "</tr>" +
                             "</table>" +
                             "</div>" +
                             "</body></html>";



            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendFormat("Dear <b>{0}</b>,", Name.Trim());
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("Congratulations!");
            mailBody.AppendFormat("<br /><br />");
            mailBody.AppendFormat("You have successfully submitted your online application form for " + DegreeName + " . Programme");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("Email ID :" + emailid);
            mailBody.AppendFormat("<br />");
            // mailBody.AppendFormat("CIEAT Application  : " + UserName.Trim());
            mailBody.AppendFormat("Application No  : " + UserName.Trim());
            mailBody.AppendFormat("<br /><br/>");
            mailBody.AppendFormat("<b><u>Please read the below information and follow the same:<br/><br/></b></u>");
            mailBody.AppendFormat("1.All the information related to and further admission process will be sent to this email id only");
            mailBody.AppendFormat("<br /><br/>");
            mailBody.AppendFormat("2.The application form submitted by you is attached in this email for your reference. Please quote the application number in all your future communications.");

            //mailBody.AppendFormat("<p>You can use this Email ID and Password to modify details you filled with the application.</p>");
            string Mailbody = mailBody.ToString();
            string nMailbody = EmailTemplate.Replace("#content", Mailbody);
            //int status = sendEmail(nMailbody, emailid, subject);
            int status = 0;
            //Task<int> task = Execute(nMailbody, emailid, subject);
            //status = task.Result;
            var msg = new SendGrid.Helpers.Mail.SendGridMessage()
            {
                From = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), ""),
                // Subject = "CIEAT Application ID",
                Subject = "Crescent University - " + DegreeName + ". Application Submission",
                HtmlContent = nMailbody.ToString(),

            };
            // int userno = Convert.ToInt32(Session["userno"].ToString());
            MemoryStream oAttachment1 = ShowGeneralExportReportForMailForApplication_NRI("Reports,Academic,rptPreviewForm_Others.rpt", "@P_USERNO=" + userno + ",@P_COLLEGE_CODE=" + 56, isNRI);

            var bytesRpt = oAttachment1.ToArray();
            var fileRpt = Convert.ToBase64String(bytesRpt);
            byte[] test = (byte[])bytesRpt;
            string FileName = UserName.Trim() + "_APPLICATION";

            //var msg1 = MailHelper.CreateSingleEmail(msg.From, to, msg.Subject, "", msg.HtmlContent);
            //msg1.Attachments = new List<SendGrid.Helpers.Mail.Attachment>
            //    {
            //        new SendGrid.Helpers.Mail.Attachment
            //        {
            //            Content = Convert.ToBase64String(test),
            //            Filename = FileName,
            //            Type = "application/pdf",
            //            Disposition = "attachment"
            //        }
            //    };

            #region  comment
            //byte[] fileBytes = ConvertDataSetToExcel(ds1);
            //var file = Convert.ToBase64String(fileBytes);
            //msg1.AddAttachment("AbsentStudentReportWeekly.xlsx", file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            //msg1.AddAttachment(attachment);
            // msg1.AddAttachment(MailAttachmentFromBlob("Attachment.xlsx"));
            //if (cc.Count > 0)
            //{
            //    msg1.AddCcs(cc);
            //}
            #endregion

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
            //Console.WriteLine(msg1.Serialize());
            //var response = await client.SendEmailAsync(msg1).ConfigureAwait(false);
            ////var response = await client.SendEmailAsync(msg1); ;
            //string res = Convert.ToString(response.StatusCode);
            //ret = (res == "Accepted") ? 1 : 0;
            //objCommon.DisplayMessage(this.Page, ret.ToString(), this.Page);
            //SendEmailCommon objSendEmailCommon=new SendEmailCommon();
            DataSet ds = getModuleConfig(0);
            string email_type = string.Empty;
            string Link = string.Empty;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                email_type = ds.Tables[0].Rows[0]["EMAIL_TYPE"].ToString();
                Link = ds.Tables[0].Rows[0]["LINK"].ToString();

            }
            //if (ret == 0)
            //    ret = Amazon1(emailid, nMailbody.ToString(), "CIEAT Application ID", FileName, test);
            //else
            //    ret = await SendGrid(nMailbody.ToString(), nMailbody.ToString(), "CIEAT Application ID", userno, UserName.Trim());
            if (email_type == "2" && email_type != "")
            {
                // ret = await SendGrid(nMailbody.ToString(), emailid, "Crescent University - B.Tech. Application Submission", userno, UserName.Trim(), test);
                //Task<int> ret1 = SendGrid(nMailbody.ToString(), emailid, "Crescent University - B.Tech. Application Submission", userno, UserName.Trim(), test);
                Task<int> ret1 = SendGrid(nMailbody.ToString(), emailid, "Crescent University - " + DegreeName + ". Application Submission", userno, UserName.Trim(), test);


            }
            else if (email_type == "4" && email_type != "")
            {
                //ret = Amazon1(emailid, nMailbody.ToString(), "Crescent University - B.Tech. Application Submission", FileName, test);
                ret = Amazon1(emailid, nMailbody.ToString(), "Crescent University - " + DegreeName + ". Application Submission", FileName, test);


            }
            if (ret == 1)
            {
                objCommon.DisplayMessage(this.Page, "Dear " + ViewState["firstName"].ToString().Trim() + " Congratulations! You have submitted the online application successfully. Your Application No. is " + UserName.Trim() + " For assistance, please call 9543277888.", this.Page);
                int userNO = Convert.ToInt32(ViewState["USERNO"].ToString());
               
            }
        }
        catch (Exception ex)
        {
            SendGrid.Helpers.Errors.Model.SendGridErrorResponse errorResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<SendGrid.Helpers.Errors.Model.SendGridErrorResponse>(ex.Message);
            Console.WriteLine(ex.Message);
        }
        return ret;
    }

    protected async Task<int> SendMailForAppId_MCA()
    {
        int ret = 0;
        // int userno = Convert.ToInt32(((UserDetails)(Session["user"])).UserNo);

        try
        {
            int userno = Convert.ToInt32(ViewState["USERNO"].ToString());
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            string MyHtmlString = "";
            Common objCommon = new Common();
            DataSet dsconfig = null;
            DataSet ds_degreename = objCommon.FillDropDown("ACD_DEGREE AD INNER JOIN ACD_USER_REGISTRATION AUR ON (AD.DEGREENO=AUR.DEGREENO)", "AD.CODE", "USERNO", " USERNO=" + userno + "", "");
            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY,SUBJECT_OTP", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);

            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            var client = new SendGridClient(apiKey);

            var to = new SendGrid.Helpers.Mail.EmailAddress();

            #region comment
            //var to = Emailid;
            //foreach (var i in emails)
            //{
            //    to.Add(new EmailAddress(i));
            //}
            //var cc = new List<EmailAddress>();
            //foreach (var i in ccemails)
            //{
            //    cc.Add(new EmailAddress(i));
            //}
            #endregion


            string email = objCommon.LookUp("ACD_USER_REGISTRATION", "EMAILID", "USERNO=" + userno);
            string Name = objCommon.LookUp("ACD_USER_REGISTRATION", "FIRSTNAME", "USERNO=" + userno);
            ViewState["firstName"] = Name;
            string UserName = objCommon.LookUp("ACD_USER_REGISTRATION", "USERNAME", "USERNO=" + userno);
            string emailid = email.ToString().Trim().Replace("'", "");
            string DegreeName = ds_degreename.Tables[0].Rows[0]["CODE"].ToString();

            //string emailid = ViewState["emailApp"].ToString().Trim().Replace("'", "");
            //string subject = "CIEAT Application ID";
            string subject = "Application Number";
            var toAddress = emailid.ToString().Trim().ToString();
            const string EmailTemplate = "<html><body>" +
                             "<div align=\"center\">" +
                             "<table style=\"width:602px;border:#1F75E2 3px solid\" cellspacing=\"0\" cellpadding=\"0\">" +
                              "<tr>" +
                              "<td>" + "</tr>" +
                              "<tr>" +
                             "<td width=\"100%\" style=\"vertical-align:top;text-align:left;padding:20px 15px 20px 15px;height:200px;FONT-FAMILY: Verdana;FONT-SIZE: 15px\">#content</td>" +
                             "</tr>" +
                             "<tr>" +
                //  "<td width=\"100%\" style=\"vertical-align:middle;text-align:left;padding:20px 15px 20px 15px;height:100px;FONT-FAMILY: Verdana;FONT-SIZE: 15px\"><b>Note : For further assistance you may send an email to <span style="color: blue;">admissionoffice@crescent.education</span> or Contact us at +91 9543277888 (9 AM to 4 PM, Mon to Friday).<br/> </td>" +
                           "<td width=\"100%\" style=\"vertical-align:middle;text-align:left;padding:20px 15px 20px 15px;height:100px;FONT-FAMILY: Verdana;FONT-SIZE: 15px\"><b>Note : For further assistance you may send an email to <span style=\"color: blue;\"><u>admissionoffice@crescent.education<u></span> or Contact us at +91 9543277888 (9 AM to 4 PM, Monday to Friday).<br/> </td>" +
                             "</tr>" +
                             "</table>" +
                             "</div>" +
                             "</body></html>";

            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendFormat("Dear <b>{0}</b>,", Name.Trim());
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("Congratulations!");
            mailBody.AppendFormat("<br /><br />");
            mailBody.AppendFormat("You have successfully submitted your online application form for " + DegreeName + " . Programme");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("Email ID :" + emailid);
            mailBody.AppendFormat("<br />");
            // mailBody.AppendFormat("CIEAT Application  : " + UserName.Trim());
            mailBody.AppendFormat("Application Number  : " + UserName.Trim());
            mailBody.AppendFormat("<br /><br/>");
            mailBody.AppendFormat("<b><u>Please read the below information and follow the same:<br/><br/></b></u>");
            mailBody.AppendFormat("1.All information related to Crescent PG Admission 2024 /further admission process will be sent to this email id only");
            mailBody.AppendFormat("<br /><br/>");
            mailBody.AppendFormat("2.You are required to log in to the application portal using your user name and password and enter your Entrance Under Graduation Examination Marks immediately after the declaration of the results.");
            mailBody.AppendFormat("<br /><br/>");
            mailBody.AppendFormat("3.The application form submitted by you is attached in this email for your reference. Please quote the application number in all your future communications.");

            //mailBody.AppendFormat("<p>You can use this Email ID and Password to modify details you filled with the application.</p>");
            string Mailbody = mailBody.ToString();
            string nMailbody = EmailTemplate.Replace("#content", Mailbody);
            //int status = sendEmail(nMailbody, emailid, subject);
            int status = 0;
            //Task<int> task = Execute(nMailbody, emailid, subject);
            //status = task.Result;
            var msg = new SendGrid.Helpers.Mail.SendGridMessage()
            {
                From = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), ""),
                // Subject = "CIEAT Application ID",
                Subject = "Crescent University - " + DegreeName + ". Application Submission",
                HtmlContent = nMailbody.ToString(),


            };
            // int userno = Convert.ToInt32(Session["userno"].ToString());
            MemoryStream oAttachment1 = ShowGeneralExportReportForMailForApplication("Reports,Academic,rptPreviewForm_PG.rpt", "@P_USERNO=" + userno + ",@P_COLLEGE_CODE=" + 56, DegreeName);

            var bytesRpt = oAttachment1.ToArray();
            var fileRpt = Convert.ToBase64String(bytesRpt);
            byte[] test = (byte[])bytesRpt;
            string FileName = UserName.Trim() + "_APPLICATION";

            //var msg1 = MailHelper.CreateSingleEmail(msg.From, to, msg.Subject, "", msg.HtmlContent);
            //msg1.Attachments = new List<SendGrid.Helpers.Mail.Attachment>
            //    {
            //        new SendGrid.Helpers.Mail.Attachment
            //        {
            //            Content = Convert.ToBase64String(test),
            //            Filename = FileName,
            //            Type = "application/pdf",
            //            Disposition = "attachment"
            //        }
            //    };

            #region  comment
            //byte[] fileBytes = ConvertDataSetToExcel(ds1);
            //var file = Convert.ToBase64String(fileBytes);
            //msg1.AddAttachment("AbsentStudentReportWeekly.xlsx", file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            //msg1.AddAttachment(attachment);
            // msg1.AddAttachment(MailAttachmentFromBlob("Attachment.xlsx"));
            //if (cc.Count > 0)
            //{
            //    msg1.AddCcs(cc);
            //}
            #endregion

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
            //Console.WriteLine(msg1.Serialize());
            //var response = await client.SendEmailAsync(msg1).ConfigureAwait(false);
            ////var response = await client.SendEmailAsync(msg1); ;
            //string res = Convert.ToString(response.StatusCode);
            //ret = (res == "Accepted") ? 1 : 0;
            //objCommon.DisplayMessage(this.Page, ret.ToString(), this.Page);
            //SendEmailCommon objSendEmailCommon=new SendEmailCommon();
            DataSet ds = getModuleConfig(0);
            string email_type = string.Empty;
            string Link = string.Empty;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                email_type = ds.Tables[0].Rows[0]["EMAIL_TYPE"].ToString();
                Link = ds.Tables[0].Rows[0]["LINK"].ToString();

            }
            //if (ret == 0)
            //    ret = Amazon1(emailid, nMailbody.ToString(), "CIEAT Application ID", FileName, test);
            //else
            //    ret = await SendGrid(nMailbody.ToString(), nMailbody.ToString(), "CIEAT Application ID", userno, UserName.Trim());
            if (email_type == "2" && email_type != "")
            {
                // ret = await SendGrid(nMailbody.ToString(), emailid, "Crescent University - B.Tech. Application Submission", userno, UserName.Trim(), test);
                Task<int> ret1 = SendGrid(nMailbody.ToString(), emailid, "Crescent University - " + DegreeName + ". Application Submission", userno, UserName.Trim(), test);

            }
            else if (email_type == "4" && email_type != "")
            {
                ret = Amazon1(emailid, nMailbody.ToString(), "Crescent University - " + DegreeName + ". Application Submission", FileName, test);
            }
            if (ret == 1)
            {
                objCommon.DisplayMessage(this.Page, "Dear " + ViewState["firstName"].ToString().Trim() + " Congratulations! You have submitted the online application successfully. Your Application No. is " + UserName.Trim() + " For assistance, please call 9543277888.", this.Page);
                int userNO = Convert.ToInt32(ViewState["USERNO"].ToString());
             
            }
        }
        catch (Exception ex)
        {
            SendGrid.Helpers.Errors.Model.SendGridErrorResponse errorResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<SendGrid.Helpers.Errors.Model.SendGridErrorResponse>(ex.Message);
            Console.WriteLine(ex.Message);
        }
        return ret;
    }

    protected async Task<int> SendMailForAppId()
    {
        int ret = 0;
        // int userno = Convert.ToInt32(((UserDetails)(Session["user"])).UserNo);

        try
        {

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            string MyHtmlString = "";
            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY,SUBJECT_OTP", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);

            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            var client = new SendGridClient(apiKey);

            var to = new SendGrid.Helpers.Mail.EmailAddress();

            #region comment
            //var to = Emailid;
            //foreach (var i in emails)
            //{
            //    to.Add(new EmailAddress(i));
            //}
            //var cc = new List<EmailAddress>();
            //foreach (var i in ccemails)
            //{
            //    cc.Add(new EmailAddress(i));
            //}
            #endregion

            int userno = Convert.ToInt32(ViewState["USERNO"].ToString());
            string email = objCommon.LookUp("ACD_USER_REGISTRATION", "EMAILID", "USERNO=" + userno);
            string Name = objCommon.LookUp("ACD_USER_REGISTRATION", "FIRSTNAME", "USERNO=" + userno);
            ViewState["firstName"] = Name;
            string UserName = objCommon.LookUp("ACD_USER_REGISTRATION", "USERNAME", "USERNO=" + userno);
            string emailid = email.ToString().Trim().Replace("'", "");
            DataSet ds_degreenameUG = objCommon.FillDropDown("ACD_DEGREE AD INNER JOIN ACD_USER_REGISTRATION AUR ON (AD.DEGREENO=AUR.DEGREENO)", "AD.CODE", "USERNO", " USERNO=" + userno + "", "");
            string DegreeName = ds_degreenameUG.Tables[0].Rows[0]["CODE"].ToString();

            //string emailid = ViewState["emailApp"].ToString().Trim().Replace("'", "");
            //string subject = "CIEAT Application ID";
            string subject = "Application No";
            var toAddress = emailid.ToString().Trim().ToString();
            const string EmailTemplate = "<html><body>" +
                             "<div align=\"center\">" +
                             "<table style=\"width:602px;border:#1F75E2 3px solid\" cellspacing=\"0\" cellpadding=\"0\">" +
                              "<tr>" +
                              "<td>" + "</tr>" +
                              "<tr>" +
                             "<td width=\"100%\" style=\"vertical-align:top;text-align:left;padding:20px 15px 20px 15px;height:200px;FONT-FAMILY: Verdana;FONT-SIZE: 15px\">#content</td>" +
                             "</tr>" +
                             "<tr>" +
                //  "<td width=\"100%\" style=\"vertical-align:middle;text-align:left;padding:20px 15px 20px 15px;height:100px;FONT-FAMILY: Verdana;FONT-SIZE: 15px\"><b>Note : For further assistance you may send an email to <span style="color: blue;">admissionoffice@crescent.education</span> or Contact us at +91 9543277888 (9 AM to 4 PM, Mon to Friday).<br/> </td>" +
                           "<td width=\"100%\" style=\"vertical-align:middle;text-align:left;padding:20px 15px 20px 15px;height:100px;FONT-FAMILY: Verdana;FONT-SIZE: 15px\"><b>Note : For further assistance you may send an email to <span style=\"color: blue;\"><u>admissionoffice@crescent.education<u></span> or Contact us at +91 9543277888 (9 AM to 4 PM, Mon to Friday).<br/> </td>" +
                             "</tr>" +
                             "</table>" +
                             "</div>" +
                             "</body></html>";

            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendFormat("Dear <b>{0}</b>,", Name.Trim());
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("Congratulations!");
            mailBody.AppendFormat("<br /><br />");
            mailBody.AppendFormat("You have successfully submitted your online application form for B.Tech. Programme");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("Email ID :" + emailid);
            mailBody.AppendFormat("<br />");
            // mailBody.AppendFormat("CIEAT Application  : " + UserName.Trim());
            mailBody.AppendFormat("Application No  : " + UserName.Trim());
            mailBody.AppendFormat("<br /><br/>");
            mailBody.AppendFormat("<b><u>Please read the below information and follow the same:<br/><br/></b></u>");
            mailBody.AppendFormat("1.All the information related to CIEAT 2024 (Crescent Institute Engineering Admission Test) and further admission process will be sent to this email id only");
            mailBody.AppendFormat("<br /><br/>");
            mailBody.AppendFormat("2.You are required to log on to the application portal using your user name and password and enter your 12th Std. Board Examination Marks immediately after the declaration of the results.");
            mailBody.AppendFormat("<br /><br/>");
            mailBody.AppendFormat("3.The application form submitted by you is attached in this email for your reference. Please quote the application number in all your future communications.");

            //mailBody.AppendFormat("<p>You can use this Email ID and Password to modify details you filled with the application.</p>");
            string Mailbody = mailBody.ToString();
            string nMailbody = EmailTemplate.Replace("#content", Mailbody);
            //int status = sendEmail(nMailbody, emailid, subject);
            int status = 0;
            //Task<int> task = Execute(nMailbody, emailid, subject);
            //status = task.Result;
            var msg = new SendGrid.Helpers.Mail.SendGridMessage()
            {
                From = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), ""),
                // Subject = "CIEAT Application ID",
                Subject = "Crescent University - B.Tech. Application Submission",
                HtmlContent = nMailbody.ToString(),

            };
            // int userno = Convert.ToInt32(Session["userno"].ToString());
            MemoryStream oAttachment1 = ShowGeneralExportReportForMailForApplication("Reports,Academic,rptPreviewForm_Crescent.rpt", "@P_USERNO=" + userno + ",@P_COLLEGE_CODE=" + 56, DegreeName);

            var bytesRpt = oAttachment1.ToArray();
            var fileRpt = Convert.ToBase64String(bytesRpt);
            byte[] test = (byte[])bytesRpt;
            string FileName = UserName.Trim() + "_APPLICATION";

            //var msg1 = MailHelper.CreateSingleEmail(msg.From, to, msg.Subject, "", msg.HtmlContent);
            //msg1.Attachments = new List<SendGrid.Helpers.Mail.Attachment>
            //    {
            //        new SendGrid.Helpers.Mail.Attachment
            //        {
            //            Content = Convert.ToBase64String(test),
            //            Filename = FileName,
            //            Type = "application/pdf",
            //            Disposition = "attachment"
            //        }
            //    };

            #region  comment
            //byte[] fileBytes = ConvertDataSetToExcel(ds1);
            //var file = Convert.ToBase64String(fileBytes);
            //msg1.AddAttachment("AbsentStudentReportWeekly.xlsx", file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            //msg1.AddAttachment(attachment);
            // msg1.AddAttachment(MailAttachmentFromBlob("Attachment.xlsx"));
            //if (cc.Count > 0)
            //{
            //    msg1.AddCcs(cc);
            //}
            #endregion

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
            //Console.WriteLine(msg1.Serialize());
            //var response = await client.SendEmailAsync(msg1).ConfigureAwait(false);
            ////var response = await client.SendEmailAsync(msg1); ;
            //string res = Convert.ToString(response.StatusCode);
            //ret = (res == "Accepted") ? 1 : 0;
            //objCommon.DisplayMessage(this.Page, ret.ToString(), this.Page);
            //SendEmailCommon objSendEmailCommon=new SendEmailCommon();
            DataSet ds = getModuleConfig(0);
            string email_type = string.Empty;
            string Link = string.Empty;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                email_type = ds.Tables[0].Rows[0]["EMAIL_TYPE"].ToString();
                Link = ds.Tables[0].Rows[0]["LINK"].ToString();

            }
            //if (ret == 0)
            //    ret = Amazon1(emailid, nMailbody.ToString(), "CIEAT Application ID", FileName, test);
            //else
            //    ret = await SendGrid(nMailbody.ToString(), nMailbody.ToString(), "CIEAT Application ID", userno, UserName.Trim());
            if (email_type == "2" && email_type != "")
            {
                // ret = await SendGrid(nMailbody.ToString(), emailid, "Crescent University - B.Tech. Application Submission", userno, UserName.Trim(), test);
                Task<int> ret1 = SendGrid(nMailbody.ToString(), emailid, "Crescent University - B.Tech. Application Submission", userno, UserName.Trim(), test);

            }
            else if (email_type == "4" && email_type != "")
            {
                ret = Amazon1(emailid, nMailbody.ToString(), "Crescent University - B.Tech. Application Submission", FileName, test);
            }
            if (ret == 1)
            {
                objCommon.DisplayMessage(this.Page, "Dear " + ViewState["firstName"].ToString().Trim() + " Congratulations! You have submitted the online application successfully. Your Application No. is " + UserName.Trim() + " For assistance, please call 9543277888.", this.Page);
                int userNO = Convert.ToInt32(ViewState["USERNO"].ToString());
               
            }
        }
        catch (Exception ex)
        {
            SendGrid.Helpers.Errors.Model.SendGridErrorResponse errorResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<SendGrid.Helpers.Errors.Model.SendGridErrorResponse>(ex.Message);
            Console.WriteLine(ex.Message);
        }
        return ret;
    }

    protected async Task<int> SendMailForAppId_NRI()
    {
        int ret = 0;
        // int userno = Convert.ToInt32(((UserDetails)(Session["user"])).UserNo);

        try
        {

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            string MyHtmlString = "";
            Common objCommon = new Common();
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY,SUBJECT_OTP", "COMPANY_EMAILSVCID <> '' and SENDGRID_PWD<> ''", string.Empty);

            var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
            var client = new SendGridClient(apiKey);

            var to = new SendGrid.Helpers.Mail.EmailAddress();

            #region comment
            //var to = Emailid;
            //foreach (var i in emails)
            //{
            //    to.Add(new EmailAddress(i));
            //}
            //var cc = new List<EmailAddress>();
            //foreach (var i in ccemails)
            //{
            //    cc.Add(new EmailAddress(i));
            //}
            #endregion

            int userno = Convert.ToInt32(ViewState["USERNO"].ToString()); 
            string email = objCommon.LookUp("ACD_USER_REGISTRATION", "EMAILID", "USERNO=" + userno);
            string Name = objCommon.LookUp("ACD_USER_REGISTRATION", "FIRSTNAME", "USERNO=" + userno);
            ViewState["firstName"] = Name;
            string UserName = objCommon.LookUp("ACD_USER_REGISTRATION", "USERNAME", "USERNO=" + userno);
            string emailid = email.ToString().Trim().Replace("'", "");
            string isNRI = objCommon.LookUp("ACD_USER_REGISTRATION", "ISNULL(NRI_STATUS,0)", "USERNO=" + Convert.ToInt32(ViewState["USERNO"]));
            //string emailid = ViewState["emailApp"].ToString().Trim().Replace("'", "");
            //string subject = "CIEAT Application ID";
            string subject = "Application No";
            var toAddress = emailid.ToString().Trim().ToString();
            const string EmailTemplate = "<html><body>" +
                             "<div align=\"center\">" +
                             "<table style=\"width:602px;border:#1F75E2 3px solid\" cellspacing=\"0\" cellpadding=\"0\">" +
                              "<tr>" +
                              "<td>" + "</tr>" +
                              "<tr>" +
                             "<td width=\"100%\" style=\"vertical-align:top;text-align:left;padding:20px 15px 20px 15px;height:200px;FONT-FAMILY: Verdana;FONT-SIZE: 15px\">#content</td>" +
                             "</tr>" +
                             "<tr>" +
                //  "<td width=\"100%\" style=\"vertical-align:middle;text-align:left;padding:20px 15px 20px 15px;height:100px;FONT-FAMILY: Verdana;FONT-SIZE: 15px\"><b>Note : For further assistance you may send an email to <span style="color: blue;">admissionoffice@crescent.education</span> or Contact us at +91 9543277888 (9 AM to 4 PM, Mon to Friday).<br/> </td>" +
                           "<td width=\"100%\" style=\"vertical-align:middle;text-align:left;padding:20px 15px 20px 15px;height:100px;FONT-FAMILY: Verdana;FONT-SIZE: 15px\"><b>Note : For further assistance you may send an email to <span style=\"color: blue;\"><u>admissionoffice@crescent.education<u></span> or Contact us at +91 9543277888 (9 AM to 4 PM, Mon to Friday).<br/> </td>" +
                             "</tr>" +
                             "</table>" +
                             "</div>" +
                             "</body></html>";

            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendFormat("Dear <b>{0}</b>,", Name.Trim());
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("Congratulations!");
            mailBody.AppendFormat("<br /><br />");
            mailBody.AppendFormat("You have successfully submitted your online application form for B.Tech. Programme");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("Email ID :" + emailid);
            mailBody.AppendFormat("<br />");
            // mailBody.AppendFormat("CIEAT Application  : " + UserName.Trim());
            mailBody.AppendFormat("Application No  : " + UserName.Trim());
            mailBody.AppendFormat("<br /><br/>");
            mailBody.AppendFormat("<b><u>Please read the below information and follow the same:<br/><br/></b></u>");
            mailBody.AppendFormat("1.All the information related to and further admission process will be sent to this email id only");
            mailBody.AppendFormat("<br /><br/>");
            mailBody.AppendFormat("2.The application form submitted by you is attached in this email for your reference. Please quote the application number in all your future communications.");

            //mailBody.AppendFormat("<p>You can use this Email ID and Password to modify details you filled with the application.</p>");
            string Mailbody = mailBody.ToString();
            string nMailbody = EmailTemplate.Replace("#content", Mailbody);
            //int status = sendEmail(nMailbody, emailid, subject);
            int status = 0;
            //Task<int> task = Execute(nMailbody, emailid, subject);
            //status = task.Result;
            var msg = new SendGrid.Helpers.Mail.SendGridMessage()
            {
                From = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), ""),
                // Subject = "CIEAT Application ID",
                Subject = "Crescent University - B.Tech. Application Submission",
                HtmlContent = nMailbody.ToString(),

            };
            // int userno = Convert.ToInt32(Session["userno"].ToString());

            MemoryStream oAttachment1 = ShowGeneralExportReportForMailForApplication_NRI("Reports,Academic,rptPreviewForm_PG_NRI.rpt", "@P_USERNO=" + userno + ",@P_COLLEGE_CODE=" + 56, isNRI);

            var bytesRpt = oAttachment1.ToArray();
            var fileRpt = Convert.ToBase64String(bytesRpt);
            byte[] test = (byte[])bytesRpt;
            string FileName = UserName.Trim() + "_APPLICATION";

            //var msg1 = MailHelper.CreateSingleEmail(msg.From, to, msg.Subject, "", msg.HtmlContent);
            //msg1.Attachments = new List<SendGrid.Helpers.Mail.Attachment>
            //    {
            //        new SendGrid.Helpers.Mail.Attachment
            //        {
            //            Content = Convert.ToBase64String(test),
            //            Filename = FileName,
            //            Type = "application/pdf",
            //            Disposition = "attachment"
            //        }
            //    };

            #region  comment
            //byte[] fileBytes = ConvertDataSetToExcel(ds1);
            //var file = Convert.ToBase64String(fileBytes);
            //msg1.AddAttachment("AbsentStudentReportWeekly.xlsx", file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            //msg1.AddAttachment(attachment);
            // msg1.AddAttachment(MailAttachmentFromBlob("Attachment.xlsx"));
            //if (cc.Count > 0)
            //{
            //    msg1.AddCcs(cc);
            //}
            #endregion

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
            //Console.WriteLine(msg1.Serialize());
            //var response = await client.SendEmailAsync(msg1).ConfigureAwait(false);
            ////var response = await client.SendEmailAsync(msg1); ;
            //string res = Convert.ToString(response.StatusCode);
            //ret = (res == "Accepted") ? 1 : 0;
            //objCommon.DisplayMessage(this.Page, ret.ToString(), this.Page);
            //SendEmailCommon objSendEmailCommon=new SendEmailCommon();
            DataSet ds = getModuleConfig(0);
            string email_type = string.Empty;
            string Link = string.Empty;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                email_type = ds.Tables[0].Rows[0]["EMAIL_TYPE"].ToString();
                Link = ds.Tables[0].Rows[0]["LINK"].ToString();

            }
            //if (ret == 0)
            //    ret = Amazon1(emailid, nMailbody.ToString(), "CIEAT Application ID", FileName, test);
            //else
            //    ret = await SendGrid(nMailbody.ToString(), nMailbody.ToString(), "CIEAT Application ID", userno, UserName.Trim());
            if (email_type == "2" && email_type != "")
            {
                // ret = await SendGrid(nMailbody.ToString(), emailid, "Crescent University - B.Tech. Application Submission", userno, UserName.Trim(), test);
                Task<int> ret1 = SendGrid(nMailbody.ToString(), emailid, "Crescent University - B.Tech. Application Submission", userno, UserName.Trim(), test);

            }
            else if (email_type == "4" && email_type != "")
            {
                ret = Amazon1(emailid, nMailbody.ToString(), "Crescent University - B.Tech. Application Submission", FileName, test);
            }
            if (ret == 1)
            {
                objCommon.DisplayMessage(this.Page, "Dear " + ViewState["firstName"].ToString().Trim() + " Congratulations! You have submitted the online application successfully. Your Application No. is " + UserName.Trim() + " For assistance, please call 9543277888.", this.Page);
                int userNo = Convert.ToInt32(ViewState["USERNO"].ToString());          
              
            }
        }
        catch (Exception ex)
        {
            SendGrid.Helpers.Errors.Model.SendGridErrorResponse errorResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<SendGrid.Helpers.Errors.Model.SendGridErrorResponse>(ex.Message);
            Console.WriteLine(ex.Message);
        }
        return ret;
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
          
            string DcrNo = string.Empty;
            string IDNO = string.Empty;
            DataSet dsDCR_IDNO = objCommon.FillDropDown("ACD_DCR_ONLINE", "DCR_NO", "IDNO,NAME", " IDNO=" + Convert.ToInt32(ViewState["USERNO"]) + "", "");
            //DataSet dsDCR_IDNO = objCommon.FillDropDown("ACD_DCR_ONLINE ", "ORDER_ID,DCR_NO ", "IDNO", "IDNO " + Convert.ToInt32(ViewState["USERNO"])+ "" , string.Empty);    //objNew.Get_Dcr_Idno_From_OrderId_ForReport(lblOrderId.Text.ToString());
            if (dsDCR_IDNO.Tables[0].Rows.Count > 0)
            {
                DcrNo = dsDCR_IDNO.Tables[0].Rows[0]["DCR_NO"].ToString();
                IDNO = dsDCR_IDNO.Tables[0].Rows[0]["IDNO"].ToString();
                string IDNO1 = dsDCR_IDNO.Tables[0].Rows[0]["NAME"].ToString();
            }
            /// string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("")));
         //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ManualEntryOA")));

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));

            url += "Reports/commonreport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            //url += "&param=@P_COLLEGE_CODE=56,@P_IDNO=" + Convert.ToInt32(IDNO) + ",@P_DCRNO=" + Convert.ToInt32(DcrNo);
            url += "&param=@P_IDNO=" + Convert.ToInt32(IDNO) + ",@P_DCR_NO=" + Convert.ToInt32(DcrNo);

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Oops! Something Went Wrong!", this.Page);
            return;
        }
    }

    static public MemoryStream ShowGeneralExportReportForMailForApplication(string path, string paramString,string DegreeName)
    {
        MemoryStream oStream;
        ReportDocument customReport;
        customReport = new ReportDocument();
          string reportPath = "";
         if (DegreeName.ToString().Equals("B.Tech."))
          {
              reportPath = System.Web.HttpContext.Current.Server.MapPath("~/Reports/Academic/rptPreviewForm_Crescent.rpt");
          }
         if (DegreeName.ToString().Equals("MCA"))
         {
             reportPath = System.Web.HttpContext.Current.Server.MapPath("~/Reports/Academic/rptPreviewForm_PG.rpt");
         }
         else
          {
             reportPath = System.Web.HttpContext.Current.Server.MapPath("~/Reports/Academic/rptPreviewForm_PG.rpt");
          }
            customReport.Load(reportPath);
        char ch = ',';
        string[] val = paramString.Split(ch);
        if (customReport.ParameterFields.Count > 0)
        {
            for (int i = 0; i < val.Length; i++)
            {

                int indexOfEql = val[i].IndexOf('=');
                int indexOfStar = val[i].IndexOf('*');
                string paramName = string.Empty;
                string value = string.Empty;
                string reportName = "MainRpt";
                paramName = val[i].Substring(0, indexOfEql);

                if (indexOfStar > 0)
                {
                    value = val[i].Substring(indexOfEql + 1, ((indexOfStar - 1) - indexOfEql));
                    reportName = val[i].Substring(indexOfStar + 1);
                }
                else
                {
                    value = val[i].Substring(indexOfEql + 1);
                }

                if (reportName == "MainRpt")
                {
                    if (value == "null")
                    {
                        customReport.SetParameterValue(paramName, null);
                    }
                    else
                        customReport.SetParameterValue(paramName, value);
                }
                else
                    customReport.SetParameterValue(paramName, value, reportName);
            }
        }

        ConfigureCrystalReports(customReport);
        for (int i = 0; i < customReport.Subreports.Count; i++)
        {
            ConfigureCrystalReports(customReport.Subreports[i]);
        }

        //oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //return oStream;

        // Assuming customReport.ExportToStream returns a compatible stream type
        Stream exportStream = customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

        // Create a MemoryStream to hold the exported data
        MemoryStream memoryStream = new MemoryStream();

        // Copy the contents of the exportStream to the memoryStream
        exportStream.CopyTo(memoryStream);

        // Close the exportStream
        exportStream.Close();

        // Reset the position of the memoryStream to the beginning
        memoryStream.Seek(0, SeekOrigin.Begin);

        // Now you have the exported data in a MemoryStream
        return memoryStream;
    }

    static public MemoryStream ShowGeneralExportReportForMailForApplication_NRI(string path, string paramString, string isNRI)
    {
        MemoryStream oStream;
        ReportDocument customReport;
        customReport = new ReportDocument();
        string reportPath = "";
        if (isNRI.ToString().Equals("1"))
        {
            reportPath = System.Web.HttpContext.Current.Server.MapPath("~/Reports/Academic/rptPreviewForm_PG_NRI.rpt");
        }
        else
        {
            reportPath = System.Web.HttpContext.Current.Server.MapPath("~/Reports/Academic/rptPreviewForm_Others.rpt");
        }
        customReport.Load(reportPath);
        char ch = ',';
        string[] val = paramString.Split(ch);
        if (customReport.ParameterFields.Count > 0)
        {
            for (int i = 0; i < val.Length; i++)
            {

                int indexOfEql = val[i].IndexOf('=');
                int indexOfStar = val[i].IndexOf('*');
                string paramName = string.Empty;
                string value = string.Empty;
                string reportName = "MainRpt";
                paramName = val[i].Substring(0, indexOfEql);

                if (indexOfStar > 0)
                {
                    value = val[i].Substring(indexOfEql + 1, ((indexOfStar - 1) - indexOfEql));
                    reportName = val[i].Substring(indexOfStar + 1);
                }
                else
                {
                    value = val[i].Substring(indexOfEql + 1);
                }

                if (reportName == "MainRpt")
                {
                    if (value == "null")
                    {
                        customReport.SetParameterValue(paramName, null);
                    }
                    else
                        customReport.SetParameterValue(paramName, value);
                }
                else
                    customReport.SetParameterValue(paramName, value, reportName);
            }
        }

        ConfigureCrystalReports(customReport);
        for (int i = 0; i < customReport.Subreports.Count; i++)
        {
            ConfigureCrystalReports(customReport.Subreports[i]);
        }

        //oStream = (MemoryStream)customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //return oStream;

        // Assuming customReport.ExportToStream returns a compatible stream type
        Stream exportStream = customReport.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

        // Create a MemoryStream to hold the exported data
        MemoryStream memoryStream = new MemoryStream();

        // Copy the contents of the exportStream to the memoryStream
        exportStream.CopyTo(memoryStream);

        // Close the exportStream
        exportStream.Close();

        // Reset the position of the memoryStream to the beginning
        memoryStream.Seek(0, SeekOrigin.Begin);

        // Now you have the exported data in a MemoryStream
        return memoryStream;
    }

    private int Amazon1(string useremail, string message, string subject, string attachmentfilename, byte[] bytefile)
    {
        int ret = 0;
        try
        {
            DataSet dsconfig = null;
            dsconfig = objCommon.FillDropDown("reff", "EMAILSVCID", "EMAILSVCPWD,CODE_STANDARD", "EMAILSVCID <> '' and EMAILSVCPWD<> ''", string.Empty);

            if (dsconfig != null)
            {
                string shortcode = dsconfig.Tables[0].Rows[0]["CODE_STANDARD"].ToString();
                var smtpClient = new System.Net.Mail.SmtpClient("email-smtp.ap-south-1.amazonaws.com", 587)
                {
                    Credentials = new NetworkCredential("AKIAUVZ5FSTMFA3CG74W", "BLYE5zzrcQkbKZEqICN3S+lhS3EdwBLl9Sl8n3EUbHEU"),
                    EnableSsl = true
                };

                var messageNew = new MailMessage
                {
                    From = new System.Net.Mail.MailAddress("no-reply@iitms.co.in", shortcode),
                    Subject = subject,//"Test Email",
                    Body = message,//"This is the body of the email."
                    IsBodyHtml = true,
                };

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
                MemoryStream stream = new MemoryStream(bytefile);
                System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(stream, attachmentfilename, "application/pdf");
                messageNew.Attachments.Add(attachment);

                //messageNew.To.Add("yograj.chaple@mastersofterp.co.in");
                messageNew.To.Add(useremail);

                smtpClient.Send(messageNew);

                if (System.Net.Mail.DeliveryNotificationOptions.OnSuccess == System.Net.Mail.DeliveryNotificationOptions.OnSuccess)
                {
                    return ret = 1;
                    //Storing the details of sent email
                }
                else
                {
                    return ret = 0;
                }
            }
        }
        catch (Exception ex)
        {

            ret = 0;
        }
        return ret;
    }

    protected async Task<int> SendGrid(string Message, string toEmailId, string sub, int userno, string APPLICATIONID, byte[] test)
    {
        int ret = 0;

        Common objCommon = new Common();
        DataSet dsconfig = null;
        dsconfig = objCommon.FillDropDown("REFF", "COMPANY_EMAILSVCID", "SENDGRID_USERNAME,SENDGRID_PWD,SENDGRID_APIKEY, CODE_STANDARD", "COMPANY_EMAILSVCID <> ''", string.Empty);

        var apiKey = dsconfig.Tables[0].Rows[0]["SENDGRID_APIKEY"].ToString();
        var client = new SendGridClient(apiKey);
        var from = new SendGrid.Helpers.Mail.EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), dsconfig.Tables[0].Rows[0]["CODE_STANDARD"].ToString());
        var subject = sub;
        var to = new SendGrid.Helpers.Mail.EmailAddress(toEmailId, "");
        var plainTextContent = "";
        //var htmlContent = Message;
        var msg = new SendGrid.Helpers.Mail.SendGridMessage()
        {
            From = new EmailAddress(dsconfig.Tables[0].Rows[0]["COMPANY_EMAILSVCID"].ToString(), ""),
            Subject = sub,
            HtmlContent = Message,
        };

        //MemoryStream oAttachment1 = ShowGeneralExportReportForMailForApplication("Reports,Academic,rptPreviewForm_Crescent.rpt", "@P_USERNO=" + userno + ",@P_COLLEGE_CODE=" + 56);

        //var bytesRpt = oAttachment1.ToArray();
        //var fileRpt = Convert.ToBase64String(bytesRpt);
        //byte[] test = (byte[])bytesRpt;


        string FileName = APPLICATIONID + "_APPLICATION";

        var msg1 = MailHelper.CreateSingleEmail(msg.From, to, msg.Subject, "", msg.HtmlContent);
        msg1.Attachments = new List<SendGrid.Helpers.Mail.Attachment>
                {
                    new SendGrid.Helpers.Mail.Attachment
                    {
                        Content = Convert.ToBase64String(test),
                        Filename = FileName,
                        Type = "application/pdf",
                        Disposition = "attachment"
                    }
                };


        #region  comment
        //byte[] fileBytes = ConvertDataSetToExcel(ds1);
        //var file = Convert.ToBase64String(fileBytes);
        //msg1.AddAttachment("AbsentStudentReportWeekly.xlsx", file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        //msg1.AddAttachment(attachment);
        // msg1.AddAttachment(MailAttachmentFromBlob("Attachment.xlsx"));
        //if (cc.Count > 0)
        //{
        //    msg1.AddCcs(cc);
        //}
        #endregion

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11;
        Console.WriteLine(msg1.Serialize());
        var response = await client.SendEmailAsync(msg1).ConfigureAwait(false);
        // var response = await client.SendEmailAsync(msg1); ;
        string res = Convert.ToString(response.StatusCode);
        if (res == "Accepted")
        {
            ret = 1;
        }
        else
        {
            ret = 0;
        }
        return ret;
    }

    private DataSet getModuleConfig(int OrganizationId)
    {
        DataSet ds = objCommon.GetModuleConfig(OrganizationId);
        return ds;
    }

    static private void ConfigureCrystalReports(ReportDocument customReport)
    {
        ConnectionInfo connectionInfo = Common.GetCrystalConnection();
        Common.SetDBLogonForReport(connectionInfo, customReport);
    }

    protected void btnreceipt_Click(object sender, EventArgs e)
    {
        try
        {
            ShowReport("OnlineFeePayment", "rptOnlineReceipt_Online_Adm.rpt");
        }
        catch (Exception ex)
        {
            objCommon.DisplayMessage(this.Page, "Oops! Something Went Wrong!", this.Page);
            return;
        }
 //</1.0.1> 
    }
}