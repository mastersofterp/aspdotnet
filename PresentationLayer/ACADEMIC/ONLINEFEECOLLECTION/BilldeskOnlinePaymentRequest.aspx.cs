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

public partial class BilldeskOnlinePaymentRequest : System.Web.UI.Page
{
    #region class
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController objFees = new FeeCollectionController();
    StudentFees objStudentFees = new StudentFees();
    OrganizationController objOrg = new OrganizationController();

    string hash_seq = string.Empty;
    #endregion
    string Idno = string.Empty;
    string userno = string.Empty;
    string Regno = string.Empty;
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
                //        Session["orgid"] = objCommon.LookUp("REFF", "OrganizationId", "");
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

                lblRegNo.Text = Session["regno"].ToString();
                lblstudentname.Text = Convert.ToString(Session["studName"]);
                lblamount.Text = Convert.ToString(Session["studAmt"]);
                int payId = Convert.ToInt32(Session["paymentId"]);
                DataSet ds1 = objFees.GetOnlinePaymentConfigurationDetails(Convert.ToInt32(Session["OrgId"]), payId, Convert.ToInt32(Session["payactivityno"]));
                if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
                {

                    string ResponseUrl = ds1.Tables[0].Rows[0]["RESPONSE_URL"].ToString();
                    string RequestUrl = ds1.Tables[0].Rows[0]["REQUEST_URL"].ToString();
                    string merchentId = ds1.Tables[0].Rows[0]["MERCHANT_ID"].ToString();
                    string hashsequence = ds1.Tables[0].Rows[0]["HASH_SEQUENCE"].ToString();
                    string ChecksumKey = ds1.Tables[0].Rows[0]["CHECKSUM_KEY"].ToString();
                    string SecurityId = ds1.Tables[0].Rows[0]["ACCESS_CODE"].ToString();
                    lblActivityName.Text = ds1.Tables[0].Rows[0]["ACTIVITY_NAME"].ToString();
                    ViewState["ResponseUrl"] = ResponseUrl;
                    ViewState["RequestUrl"] = RequestUrl;
                    ViewState["merchentId"] = merchentId;
                    ViewState["ChecksumKey"] = ChecksumKey;
                    ViewState["SecurityId"] = SecurityId;


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





    protected void btnPay_Click(object sender, EventArgs e)
    {
        int status1 = 0;
        int Currency = 1;
        string amount = string.Empty;
        //amount = Convert.ToDouble(lblAmount.Text);

        string UserId = Convert.ToString(Session["userno"]);
        if (Session["userno"] == null)
        {
            Response.Redirect("~/default.aspx");
        }
    Reprocess:
        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        Random ram = new Random();
        int i = ram.Next(1, 9);
        int j = ram.Next(21, 51);
        int k = ram.Next(471, 999);
        int l = System.DateTime.Today.Day;
        int m = System.DateTime.Today.Month;
        string refno = string.Empty;
        if (Session["ReceiptType"].ToString() == "EF")
        {
            refno = Session["Order_id"].ToString();
        }
        else if (Session["ReceiptType"].ToString() == "PRF" || Session["ReceiptType"].ToString() == "RF")           //Added on 26082022
        {
            refno = Session["Order_id"].ToString();
        }
        else
        {
            refno = (i + "" + j + "" + k + "" + l + "" + m).ToString() + UserId;
        }

        string str1 = objCommon.LookUp("ACD_DCR", "ORDER_ID", "ORDER_ID='" + refno + "'");
        if ((Session["ReceiptType"].ToString() != "PRF") && (Session["ReceiptType"].ToString() != "RF"))                 //Added on 24082022  
        {
            if (str1 != "" || str1 != string.Empty)
            {
                goto Reprocess;
            }
        }

        Session["studrefno"] = refno;
        string datetm = indianTime.ToString("dd-MMM-yyyy");
        string status = "Not Continued";
        amount = Convert.ToString(Session["studAmt"]);

        //string custtype = "INR";
        #region  ADDED BY GAURAV 30_11_2023 FOR SUBSTITUTE EXAM REG CRESCENT
        String ExamNo = Convert.ToString(Session["EXAMNO"]) == "" ? "NA" : Convert.ToString(Session["EXAMNO"]);
        Session["ExamNo"] = ExamNo;
        #endregion
        String data = string.Empty;
        if (Session["ReceiptType"] == "REF")
        {
            data = Convert.ToString(ViewState["merchentId"]) + "|" + refno + "|NA|" + amount + "|NA|NA|NA|INR|NA|R|" + Convert.ToString(ViewState["SecurityId"]) + "|NA|NA|F|" + Convert.ToString(Session["payStudName"]) + "|" + Convert.ToString(Session["paymobileno"]) + "|" + Convert.ToString(Session["ReceiptType"]) + "|" + Convert.ToString(Session["regno"]) + "-" + Convert.ToInt32(Session["ExamNo"]) + "|" + Convert.ToString(Session["idno"]) + "|" + Convert.ToString(Session["userno"]) + "|" + Convert.ToString(Session["Installmentno"]) + "|" + Convert.ToString(ViewState["ResponseUrl"]);
        }
        {

             data = Convert.ToString(ViewState["merchentId"]) + "|" + refno + "|NA|" + amount + "|NA|NA|NA|INR|NA|R|" + Convert.ToString(ViewState["SecurityId"]) + "|NA|NA|F|" + Convert.ToString(Session["payStudName"]) + "|" + Convert.ToString(Session["paymobileno"]) + "|" + Convert.ToString(Session["ReceiptType"]) + "|" + Convert.ToString(Session["regno"]) + "|" + Convert.ToString(Session["idno"]) + "|" + Convert.ToString(Session["userno"]) + "|" + Convert.ToString(Session["Installmentno"]) + "|" + Convert.ToString(ViewState["ResponseUrl"]);
        }

        String commonkey = Convert.ToString(ViewState["ChecksumKey"]);

        String hash = String.Empty;

        hash = GetHMACSHA256(data, commonkey);

        data = data + "|" + hash.ToUpper();
        // requestparams.Value = data;

        int count = Convert.ToInt32(objCommon.LookUp("ACD_ADMISSION_STATUS_LOG", "count(*)", "IDNO =" + Convert.ToInt32(Session["idno"]) + " AND SEMESTERNO =" + Convert.ToInt32(Session["paysemester"]) + " AND STATUS = 1"));
        if (count > 0)
        {
            Session["admission"] = Convert.ToInt32(1);
        }
        else
        {
            Session["admission"] = Convert.ToInt32(0);
        }

        int result = 0;
        objFees.InsertOnlinePaymentlog(Convert.ToString(Session["idno"]), Session["ReceiptType"].ToString(), Convert.ToString(Session["PaymentMode"]), Convert.ToString(Session["studAmt"]), status, refno);

        #region SupplyExam Feeslog
        if (Session["ReceiptType"].ToString() == "SEF")
        {
            objStudentFees.UserNo = Convert.ToInt32(Session["idno"].ToString());
            objStudentFees.Amount = Convert.ToDouble(Session["studAmt"].ToString());
            objStudentFees.SessionNo = (Session["paysession"].ToString());
            objStudentFees.OrderID = Session["studrefno"].ToString();// lblOrderID.Text;
            result = objFees.AddPhotoRevalFeeLog(objStudentFees, 1, 1, "SEF", 1);    //AddPhotoRevalFeeLogSupply
        }
        #endregion

        if (Session["ReceiptType"].ToString() == "EF")
        {

            objStudentFees.UserNo = Convert.ToInt32(Session["idno"].ToString());
            objStudentFees.Amount = Convert.ToDouble(Session["studAmt"].ToString());
            objStudentFees.SessionNo = (Session["paysession"].ToString());
            objStudentFees.OrderID = Session["studrefno"].ToString();// lblOrderID.Text;
            //insert in acd_fees_log
            result = objFees.AddExamDetailsFeeLog(objStudentFees, 1, 1, "EF", 3); //3 for arrear exam fee

        }



        if (Convert.ToInt32(Session["Installmentno"]) > 0)
        {
            result = objFees.InsertInstallmentOnlinePayment_TempDCR(Convert.ToInt32(Idno), Convert.ToInt32(Session["demandno"]), Convert.ToInt32(Session["paysemester"]), refno, Convert.ToDouble(amount), Convert.ToString(Session["ReceiptType"]), Convert.ToInt32(Session["userno"]), data);
        }
        else if (Session["ReceiptType"].ToString() == "PRF" || Session["ReceiptType"].ToString() == "RF" || Session["ReceiptType"].ToString() == "SEF")
        {
            result = objFees.InsertPayment_Log_TempDCR(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["paysession"]), Session["semesternos"].ToString(), refno, 1, Convert.ToString(Session["ReceiptType"]), data);
        }
        else if (Session["ReceiptType"].ToString() == "REF")     //Added by gaurav 23_09_2022 START --for retestexam
        {
            objStudentFees.UserNo = Convert.ToInt32(Session["idno"].ToString());
            objStudentFees.Amount = Convert.ToDouble(Session["studAmt"].ToString());
            objStudentFees.SessionNo = (Session["paysession"].ToString());
            objStudentFees.OrderID = Session["studrefno"].ToString();
            result = objFees.InsertOnlinePayment_TempDCR(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["paysession"]), Convert.ToInt32(Session["paysemester"]), refno, 1, Convert.ToString(Session["ReceiptType"]), data);
        }
        else
        {
            result = objFees.InsertOnlinePayment_TempDCR(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["paysession"]), Convert.ToInt32(Session["paysemester"]), refno, 1, Convert.ToString(Session["ReceiptType"]), data);
        }
        if (result > 0)
        {
            string orderid = objCommon.LookUp("ACD_DCR_TEMP", "ORDER_ID", "IDNO = " + Convert.ToInt32(Session["idno"]) + " AND ORDER_ID='" + refno + "'");
            if (orderid != "" || orderid != string.Empty || orderid == refno)
            {
                string strForm = PreparePOSTForm(Convert.ToString(ViewState["RequestUrl"]), data);
                Page.Controls.Add(new LiteralControl(strForm));
            }
        }
        else
        {
            objCommon.DisplayMessage("Online Payment Not Done, Please Try Again..!!", this.Page);
            return;
        }

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        string returnpageurl = Convert.ToString(Session["ReturnpageUrl"]);
        Response.Redirect(returnpageurl);
    }

    private string PreparePOSTForm(string url, string data)      // post form
    {
        //Set a name for the form
        string formID = "PostForm";
        //Build the form using the specified data to be posted.
        StringBuilder strForm = new StringBuilder();
        strForm.Append("<form id=\"" + formID + "\" name=\"" +
                       formID + "\" action=\"" + url +
                       "\" method=\"POST\">");

        strForm.Append("<input type=\"hidden\" name=\"msg" +
                       "\" value=\"" + data + "\">");

        strForm.Append("</form>");
        //Build the JavaScript which will do the Posting operation.
        StringBuilder strScript = new StringBuilder();
        strScript.Append("<script language='javascript'>");
        strScript.Append("var v" + formID + " = document." +
                         formID + ";");
        strScript.Append("v" + formID + ".submit();");
        strScript.Append("</script>");
        //Return the form and the script concatenated.
        //(The order is important, Form then JavaScript)
        return strForm.ToString() + strScript.ToString();
    }

    public string GetHMACSHA256(string text, string key)
    {
        UTF8Encoding encoder = new UTF8Encoding();

        byte[] hashValue;
        byte[] keybyt = encoder.GetBytes(key);
        byte[] message = encoder.GetBytes(text);

        HMACSHA256 hashString = new HMACSHA256(keybyt);
        string hex = "";

        hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;
    }

}