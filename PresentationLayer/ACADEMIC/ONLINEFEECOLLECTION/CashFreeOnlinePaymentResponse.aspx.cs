using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CCA.Util;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data.SqlClient;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG;

public partial class CashFreeOnlinePaymentResponse : System.Web.UI.Page
{
    #region class
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController objFees = new FeeCollectionController();
    StudentController objSC = new StudentController();
    OrganizationController objOrg = new OrganizationController();

    string hash_seq = string.Empty;
    #endregion
    string Idno = string.Empty;
    string userno = string.Empty;
    string Regno = string.Empty;
    string installmentno = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                //SqlDataReader dr = objCommon.GetCommonDetails();

                //if (dr != null)
                //{
                //    if (dr.Read())
                //    {
                //        lblCollege.Text = dr["COLLEGENAME"].ToString();
                //        lblAddress.Text = dr["College_Address"].ToString();
                //        imgCollegeLogo.ImageUrl = "~/showimage.aspx?id=0&type=college";
                //    }
                //}

                DataSet Orgds = null;
                var OrgId = objCommon.LookUp("REFF", "OrganizationId", "");
                Orgds = objOrg.GetOrganizationById(Convert.ToInt32(OrgId));
                byte[] imgData = null;
                if (Orgds.Tables != null)
                {
                    if (Orgds.Tables[0].Rows.Count > 0)
                    {

                        if (Orgds.Tables[0].Rows[0]["Logo"] != DBNull.Value)
                        {
                            imgData = Orgds.Tables[0].Rows[0]["Logo"] as byte[];
                            imgCollegeLogo.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(imgData);
                        }
                        else
                        {
                            // hdnLogoOrg.Value = "0";
                        }

                    }
                }

                string paymentmode = string.Empty;
                
                string RecieptType = string.Empty;
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                foreach (string key in Request.Form.Keys)
                {
                    parameters.Add(key.Trim(), Request.Form[key].Trim());
                }
                lblOrderId.Text = parameters["orderId"];
                lblamount.Text = parameters["orderAmount"];
                paymentmode = parameters["paymentMode"];
                string Message = parameters["txMsg"];
                lblResponsecode.Text = parameters["txStatus"];
                string Time = parameters["txTime"];
                lblTrasactionId.Text = parameters["referenceId"];
                lblTransactionDate.Text = Time;
                
                DataSet dsstud = objFees.GetStudentDetailsFromOrderId(lblOrderId.Text);
                if (dsstud.Tables[0] != null && dsstud.Tables[0].Rows.Count > 0)
                {
                    Session["username"] = dsstud.Tables[0].Rows[0]["UA_NAME"].ToString();
                    Session["usertype"] = dsstud.Tables[0].Rows[0]["UA_TYPE"].ToString();
                    Session["userfullname"] = dsstud.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
                    Session["idno"] = dsstud.Tables[0].Rows[0]["UA_IDNO"].ToString();
                    Session["firstlog"] = dsstud.Tables[0].Rows[0]["UA_FIRSTLOG"].ToString();
                    lblstudentname.Text = dsstud.Tables[0].Rows[0]["STUDNAME"].ToString();
                    lblRegNo.Text = dsstud.Tables[0].Rows[0]["REGNO"].ToString();
                    installmentno = dsstud.Tables[0].Rows[0]["INSTALLMENTNO"].ToString();
                    RecieptType = dsstud.Tables[0].Rows[0]["RECIEPT_CODE"].ToString();
                    Idno = dsstud.Tables[0].Rows[0]["IDNO"].ToString();
                }
                Session["coll_name"] = objCommon.LookUp("REFF", "CollegeName", "");
                Session["colcode"] = objCommon.LookUp("REFF", "COLLEGE_CODE", "");
                Session["currentsession"] = objCommon.LookUp("ACD_SESSION_MASTER", "MAX(SESSIONNO)", "SESSIONNO>0");
                Session["sessionname"] = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", "SESSIONNO=(SELECT MAX(SESSIONNO) FROM ACD_SESSION_MASTER WHERE SESSIONNO>0)");

                if (lblResponsecode.Text == "SUCCESS")
                {
                    divSuccess.Visible = true;
                    int output = 0;
                    if (Convert.ToInt32(installmentno) > 0)
                    {
                        output = objFees.InsertInstallmentOnlinePayment_DCRCashfree(Idno, RecieptType, lblOrderId.Text, lblTrasactionId.Text, "O", "1", lblamount.Text, "Success", Convert.ToInt32(installmentno), Message);
                    }
                    else
                    {
                        output = objFees.InsertOnlinePayment_DCRCashfree(Idno, RecieptType, lblOrderId.Text, lblTrasactionId.Text, "O", "1", lblamount.Text, "Success", Message);
                    }

                    #region Retest Exam
                    // Addeded by gaurav 23-09-2022 for retest 
                    if (RecieptType == "REF")
                    {

                        int session = Convert.ToInt32(objCommon.LookUp("ACD_ABSENT_STUD_EXAM_REG_LOG", "TOP 1 (SESSIONNO)", "IDNO=" + Idno));
                        DataSet dsstudent = null;
                        dsstudent = objSC.GetRetestStudentDetailsExam(Convert.ToInt32(Idno), session);

                    }
                    #endregion
                    if (output == -99)
                    {
                        divSuccess.Visible = false;
                        divFailure.Visible = true;

                        objFees.InsertOnlinePaymentlog(Idno, RecieptType, "O", lblamount.Text, "Payment Fail", lblOrderId.Text);
                    }
                    else
                    {
                        ViewState["out"] = output;
                        btnReciept.Visible = true;
                    }
                }
                else
                {
                    divFailure.Visible = true;
                    divSuccess.Visible = false;
                    btnReciept.Visible = false;
                    objFees.InsertOnlinePaymentlog(Idno, RecieptType, "O", lblamount.Text, "Payment Fail", lblOrderId.Text);
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }

    public void TransferToEmail1(string ToID, string userMsg, string userMsg1, string userMsg2, string messBody3, string messBody4, string messBody5)
    {
        try
        {
            //string path = Server.MapPath(@"/Css/images/Index.Jpeg");
            //LinkedResource Img = new LinkedResource(path, MediaTypeNames.Image.Jpeg);
            //Img.ContentId = "MyImage";   

            ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
            //string fromPassword = Common.DecryptPassword(objCommon.LookUp("REFF", "EMAILSVCPWD", string.Empty));
            //string fromAddress = objCommon.LookUp("REFF", "EMAILSVCID", string.Empty);
            string fromPassword = Common.DecryptPassword(objCommon.LookUp("Email_Configuration", "EMAILSVCPWD1", string.Empty));
            string fromAddress = objCommon.LookUp("Email_Configuration", "EMAILSVCID1", string.Empty);

            MailMessage msg = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            msg.From = new MailAddress(fromAddress, "NIT GOA");
            msg.To.Add(new MailAddress(ToID));

            msg.Subject = "Your transaction with MAKAUT";

            const string EmailTemplate = "<html><body>" +
                                     "<div align=\"left\">" +
                                     "<table style=\"width:602px;border:#FFFFFF 3px solid\" cellspacing=\"0\" cellpadding=\"0\">" +
                                      "<tr>" +
                                      "<td>" + "</tr>" +
                                      "<tr>" +
                                     "<td width=\"100%\" style=\"vertical-align:top;text-align:left;padding:20px 15px 20px 15px;height:200px;FONT-FAMILY: Trebuchet MS;FONT-SIZE: 14px\">#content</td>" +
                                     "</tr>" +
                                     "<tr>" +
                                     "<td width=\"100%\" style=\"vertical-align:middle;text-align:left;padding:20px 15px 20px 15px;height:100px;FONT-FAMILY: Trebuchet MS;FONT-SIZE: 14px\"><img src=\"\"  id=\"../../Css/images/Index.png\" height=\"10\" width=\"10\"><br/><b>National Institute of Technology Goa </td>" +
                                     "</tr>" +
                                     "</table>" +
                                     "</div>" +
                                     "</body></html>";
            StringBuilder mailBody = new StringBuilder();
            //mailBody.AppendFormat("<h1>Greating !!</h1>");
            mailBody.AppendFormat("Dear <b>{0}</b> ,", messBody3);
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat(userMsg);
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat(messBody5);
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat(userMsg1);
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat(userMsg2);
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat(messBody4);
            mailBody.AppendFormat("<br />");
            string Mailbody = mailBody.ToString();
            string nMailbody = EmailTemplate.Replace("#content", Mailbody);
            msg.IsBodyHtml = true;
            msg.Body = nMailbody;

            smtp.Host = "smtp.gmail.com";

            smtp.Port = 587;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);
            smtp.EnableSsl = true;
            smtp.Send(msg);

        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "DISPATCH_Transactions_IO_InwardDispatch.TransferToEmail-> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server UnAvailable");
        }
    }

    public string Generatehash512(string text)
    {

        byte[] message = System.Text.Encoding.UTF8.GetBytes(text);

        UnicodeEncoding UE = new UnicodeEncoding();
        byte[] hashValue;
        SHA512Managed hashString = new SHA512Managed();
        string hex = "";
        hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;

    }




    //private void ShowReport(string reportTitle, string rptFileName)
    //{
    //    try
    //    {

    //        int DcrNo = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "DCR_NO", "ORDER_ID='" + Convert.ToString(lblOrderId.Text) + "'"));
    //        int IDNO = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "IDNO", "ORDER_ID='" + Convert.ToString(lblOrderId.Text) + "'"));

    //        string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("ACADEMIC")));
    //        url += "Reports/CommonReport.aspx?";
    //        url += "pagetitle=" + reportTitle;
    //        url += "&path=~,Reports,Academic," + rptFileName;

    //        url += "&param=@P_COLLEGE_CODE=35,@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo);

    //        divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
    //        divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
    //        divMsg.InnerHtml += " </script>";

    //        ////To open new window from Updatepanel
    //        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //        //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
    //        //sb.Append(@"window.open('" + url + "','','" + features + "');");

    //        //ScriptManager.RegisterClientScriptBlock(this.updFee, this.updFee.GetType(), "controlJSScript", sb.ToString(), true);
    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objUaimsCommon.ShowError(Page, "CourseWise_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objUaimsCommon.ShowError(Page, "Server Unavailable.");
    //    }
    //}


    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            string DcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "ORDER_ID='" + Convert.ToString(lblOrderId.Text) + "'");
            int IDNO = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "IDNO", "ORDER_ID='" + Convert.ToString(lblOrderId.Text) + "'"));
            int college_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(IDNO)));
            Session["UAFULLNAME"] = Session["userfullname"].ToString();  //objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NO=" + Convert.ToInt32(Session["userno"]));

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo);
            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(college_id) + ",@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo) + ",@P_UA_NAME=" + Session["UAFULLNAME"];

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            //To open new window from Updatepanel
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this.updFee, this.updFee.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "CourseWise_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/default.aspx");
    }
    protected void btnReciept_Click(object sender, EventArgs e)
    {
        //ShowReport("OnlineFeePayment", "rptOnlineReceipt.rpt");
        ShowReport("OnlineFeePayment", "rptOnlineReceipt_New.rpt");
    }
}