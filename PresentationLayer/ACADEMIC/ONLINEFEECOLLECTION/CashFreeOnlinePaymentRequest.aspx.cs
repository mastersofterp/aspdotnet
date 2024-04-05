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

public partial class CashFreeOnlinePaymentRequest : System.Web.UI.Page
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
        if (Session["ReceiptType"].ToString()=="EF")
        {
            refno= Session["Order_id"].ToString();
        }
        else
        {
        refno = (i + "" + j + "" + k + "" + l + "" + m).ToString() + UserId;
        }

        string str1 = objCommon.LookUp("ACD_DCR", "ORDER_ID", "ORDER_ID='" + refno + "'");

        if (str1 != "" || str1 != string.Empty)
        {
            goto Reprocess;
        }

        Session["studrefno"] = refno;
        string datetm = indianTime.ToString("dd-MMM-yyyy");
        string status = "Not Continued";
        amount = Convert.ToString(Session["studAmt"]);

        
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


        if (Session["ReceiptType"].ToString() == "EF")
        {

            objStudentFees.UserNo = Convert.ToInt32(Session["idno"].ToString());
            objStudentFees.Amount = Convert.ToDouble(Session["studAmt"].ToString());
            objStudentFees.SessionNo =  (Session["paysession"].ToString());
            objStudentFees.OrderID = Session["studrefno"].ToString();// lblOrderID.Text;

            //insert in acd_fees_log
            result = objFees.AddExamDetailsFeeLog(objStudentFees, 1, 1, "EF", 3); //3 for arrear exam fee
          
        }

        else if (Convert.ToInt32(Session["Installmentno"]) > 0)
        {
            result = objFees.InsertInstallmentOnlinePayment_TempDCRCashfree(Convert.ToInt32(Idno), Convert.ToInt32(Session["demandno"]), Convert.ToInt32(Session["paysemester"]), refno, Convert.ToDouble(amount), Convert.ToString(Session["ReceiptType"]), Convert.ToInt32(Session["userno"]), "", Convert.ToInt32(Session["Installmentno"]));
        }
        else
        {
            result = objFees.InsertOnlinePayment_TempDCRCashfree(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["paysession"]), Convert.ToInt32(Session["paysemester"]), refno, 1, Convert.ToString(Session["ReceiptType"]), Convert.ToInt32(Session["userno"]), "");
        }
        if (result > 0)
        {
            string orderid = objCommon.LookUp("ACD_DCR_TEMP", "ORDER_ID", "IDNO = " + Convert.ToInt32(Session["idno"]) + " AND ORDER_ID='" + refno + "'");
            if (orderid != "" || orderid != string.Empty || orderid == refno)
            {
                string secret = Convert.ToString(ViewState["ChecksumKey"]);
                string data = "";
                string Merchantkey = Convert.ToString(ViewState["merchentId"]);
                SortedDictionary<string, string> formParams = new SortedDictionary<string, string>();
                formParams.Add("appId", Merchantkey);
                formParams.Add("orderId", refno);
                formParams.Add("orderAmount", amount);
                formParams.Add("customerName", lblstudentname.Text);
                formParams.Add("customerPhone", Convert.ToString(Session["studPhone"]));
                formParams.Add("customerEmail", Convert.ToString(Session["studEmail"]));
                formParams.Add("returnUrl", Convert.ToString(ViewState["ResponseUrl"]));
                foreach (var kvp in formParams)
                {
                    data = data + kvp.Key + kvp.Value;
                }

                string signature = CreateToken(data, secret);
                //Console.Write(signature);
                string outputHTML = "<html>";
                outputHTML += "<head>";
                outputHTML += "<title>Merchant Check Out Page</title>";
                outputHTML += "</head>";
                outputHTML += "<body>";
                outputHTML += "<center>Please do not refresh this page...</center>";  // you can put h1 tag here
                //	outputHTML += "<form id='redirectForm' method='post' action='https://www.gocashfree.com/checkout/post/submit'>";
                outputHTML += "<form id='redirectForm' method='post' action='" + Convert.ToString(ViewState["RequestUrl"]) + "'>";

                outputHTML += "<input type='hidden' name='appId' value='" + Merchantkey + "'/>";
                outputHTML += "<input type='hidden' name='orderId' value='" + refno + "'/>";
                outputHTML += "<input type='hidden' name='orderAmount' value='" + amount + "'/>";
                outputHTML += "<input type='hidden' name='customerName' value='" + lblstudentname.Text + "'/>";
                outputHTML += "<input type='hidden' name='customerEmail' value='" + Convert.ToString(Session["studEmail"]) + "'/>";
                outputHTML += "<input type='hidden' name='customerPhone' value='" + Convert.ToString(Session["studPhone"]) + "'/>";
                //outputHTML += "<input type='hidden' name='regNo' value='" + Convert.ToString(Session["regno"]) + "'/>";
                //outputHTML += "<input type='hidden' name='idNo' value='" + Convert.ToString(Session["userno"]) + "'/>";
                outputHTML += "<input type='hidden' name='returnUrl' value='" + Convert.ToString(ViewState["ResponseUrl"]) + "'/>";
                outputHTML += "<input type='hidden' name='signature' value='" + signature + "'/>";
                outputHTML += "<table border='1'>";
                outputHTML += "<tbody>";
                foreach (string keys in formParams.Keys)
                {
                    outputHTML += "<input type='hidden' name='" + keys + "' value='" + formParams[keys] + "'>";
                }
                outputHTML += "</tbody>";
                outputHTML += "</table>";
                outputHTML += "<script type='text/javascript'>";
                outputHTML += "document.getElementById('redirectForm').submit();";
                outputHTML += "</script>";
                outputHTML += "</form>";
                outputHTML += "</body>";
                outputHTML += "</html>";
                Response.Write(outputHTML);
            }

        }
        else
        {
            objCommon.DisplayMessage("Online Payment Not Done, Please Try Again..!!", this.Page);
            return;
        }

    }

    private string CreateToken(string message, string secret)
    {
        secret = secret ?? "";
        var encoding = new System.Text.ASCIIEncoding();
        byte[] keyByte = encoding.GetBytes(secret);
        byte[] messageBytes = encoding.GetBytes(message);
        using (var hmacsha256 = new HMACSHA256(keyByte))
        {
            byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
            return Convert.ToBase64String(hashmessage);
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        string returnpageurl = Convert.ToString(Session["ReturnpageUrl"]);
        Response.Redirect(returnpageurl);
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