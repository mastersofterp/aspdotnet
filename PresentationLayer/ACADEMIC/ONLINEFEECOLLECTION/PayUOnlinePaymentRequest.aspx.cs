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

public partial class PayUOnlinePaymentRequest : System.Web.UI.Page
{
    #region class
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController objFees = new FeeCollectionController();

    string hash_seq = string.Empty;
    #endregion
    string Idno = string.Empty;
    string userno = string.Empty;
    string Regno = string.Empty;
    public string txnid1 = string.Empty;
    public string action1 = string.Empty;
    public string hash1 = string.Empty;
    int degreeno = 0;
    int college_id = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                SqlDataReader dr = objCommon.GetCommonDetails();

                if (dr != null)
                {
                    if (dr.Read())
                    {
                        lblCollege.Text = dr["COLLEGENAME"].ToString();
                        lblAddress.Text = dr["College_Address"].ToString();
                        imgCollegeLogo.ImageUrl = "~/showimage.aspx?id=0&type=college";
                    }
                }
                //Added by Nikhil L. on 23-08-2022 for getting response and request url as per degreeno for RCPIPER.

                if (Session["OrgId"].ToString() == "6")
                {
                    degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
                }
                if (Session["OrgId"].ToString() == "8")
                {
                    college_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
                }

                //**********************************End by Nikhil L.********************************************//


                lblRegNo.Text = Session["regno"].ToString();
                lblstudentname.Text = Convert.ToString(Session["payStudName"]);
                lblBranch.Text = Convert.ToString(Session["Branchname"]);
                firstname.Text = Convert.ToString(Session["payStudName"]);

                lblSemester.Text = Convert.ToString(Session["paysemester"]);
                email.Text = Convert.ToString(Session["studEmail"]);
                phone.Text = Convert.ToString(Session["studPhone"]);
                lblamount.Text = Convert.ToString(Session["studAmt"]);
                int payId = Convert.ToInt32(Session["paymentId"]);
                lblYear.Text = Session["YEARNO"].ToString();
                DataSet ds1 = objFees.GetOnlinePaymentConfigurationDetails_WithDegree(Convert.ToInt32(Session["OrgId"]), payId, Convert.ToInt32(Session["payactivityno"]), Convert.ToInt32(degreeno), Convert.ToInt32(college_id));
                if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
                {
                    string ResponseUrl = ds1.Tables[0].Rows[0]["RESPONSE_URL"].ToString();
                    string RequestUrl = ds1.Tables[0].Rows[0]["REQUEST_URL"].ToString();
                    string merchentkey = ds1.Tables[0].Rows[0]["MERCHANT_ID"].ToString();
                    string hashsequence = ds1.Tables[0].Rows[0]["HASH_SEQUENCE"].ToString();
                    string saltkey = ds1.Tables[0].Rows[0]["CHECKSUM_KEY"].ToString();
                    string accesscode = ds1.Tables[0].Rows[0]["ACCESS_CODE"].ToString();
                    lblActivityName.Text = ds1.Tables[0].Rows[0]["ACTIVITY_NAME"].ToString();
                    ViewState["ResponseUrl"] = ResponseUrl;
                    ViewState["RequestUrl"] = RequestUrl;
                    ViewState["merchentkey"] = merchentkey;
                    ViewState["saltkey"] = saltkey;
                    ViewState["accesscode"] = accesscode;
                    ViewState["hashsequence"] = hashsequence;
                    key.Value = merchentkey;
                    //hash.Value = hashsequence;
                    surl.Text = ResponseUrl;
                    furl.Text = ResponseUrl;
                    productinfo.Text = Convert.ToString(Session["idno"]);
                    udf1.Text = Convert.ToString(Session["OrgId"]);
                    udf2.Text = payId.ToString();
                    udf3.Text = Convert.ToString(Session["payactivityno"]);
                    //udf4.Text = Convert.ToString(Session["Installmentno"]);
                    udf4.Text = Convert.ToString(Session["Installmentno"]);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }

    #region Method
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

        byte[] message = Encoding.UTF8.GetBytes(text);

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
    #endregion




    protected void btnPay_Click(object sender, EventArgs e)
    {
        string[] hashVarsSeq;
        string hash_string = string.Empty;
        //txnid1 = Convert.ToString(Session["txnid1"]);

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
        string txnid1 = (i + "" + j + "" + k + "" + l + "" + m).ToString() + "-" + UserId;
        string str1 = objCommon.LookUp("ACD_DCR", "ORDER_ID", "ORDER_ID='" + txnid1 + "'");

        if (str1 != "" || str1 != string.Empty)
        {
            goto Reprocess;
        }
        //if (string.IsNullOrEmpty(Request.Form["txnid"])) // generating txnid
        //{


        //}
        //else
        //{
        //    txnid1 = Convert.ToString(Session["txnid1"]);
        //}
        //objFees.InsertPendingAmountInDCR(Convert.ToInt32(Session["IDNO"]), Convert.ToInt32(Session["lblSemester"]), Convert.ToString(Session["receipt_type"]), Convert.ToDouble(Session["Amount"]),
        //          "O", Convert.ToInt32(txnid1), Convert.ToInt32(Session["SESSIONNO"].ToString()), Convert.ToString(Session["receipt_type"]), Convert.ToInt32(Session["InstallmentNo"]));
        //int result = 0;

        int result = 0;
        objFees.InsertOnlinePaymentlog(Convert.ToString(Session["idno"]), Session["ReceiptType"].ToString(), Convert.ToString(Session["PaymentMode"]), Convert.ToString(Session["studAmt"]), "Not Continued", txnid1);

        if (Convert.ToInt32(Session["Installmentno"]) > 0)
        {
            result = objFees.InsertInstallmentOnlinePayment_TempDCR(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["demandno"]), Convert.ToInt32(Session["paysemester"]), txnid1, Convert.ToDouble(Session["studAmt"]), Convert.ToString(Session["ReceiptType"]), Convert.ToInt32(Session["userno"]), "-");
        }
        else if (Session["ReceiptType"].ToString() == "PRF" || Session["ReceiptType"].ToString() == "RF" || Session["ReceiptType"].ToString() == "SEF")
        {
            result = objFees.InsertPayment_Log_TempDCR(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["paysession"]), Session["semesternos"].ToString(), txnid1, 1, Convert.ToString(Session["ReceiptType"]), "-");
        }
        else
        {
            result = objFees.InsertOnlinePayment_TempDCR(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["paysession"]), Convert.ToInt32(Session["paysemester"]), txnid1, 1, Convert.ToString(Session["ReceiptType"]), "-");
        }
        //result = objFees.SubmitFeesofStudent(objStudentFees, 1, 2, lblReceiptCode.Text, Convert.ToInt32(demand_semester));
        //int orderid = Convert.ToInt32(objStudentFees.OrderID);
        string orderid = objCommon.LookUp("ACD_DCR_TEMP", "ORDER_ID", "IDNO = " + Convert.ToInt32(Session["idno"]) + " AND ORDER_ID='" + txnid1 + "'");
        if (orderid != "" || orderid != string.Empty || orderid == txnid1)
        {

            if (string.IsNullOrEmpty(Request.Form["hash"])) // generating hash value
            {

                if (
                    string.IsNullOrEmpty(ViewState["merchentkey"].ToString()) ||
                    string.IsNullOrEmpty(txnid1) ||
                    string.IsNullOrEmpty(Convert.ToString(Session["studAmt"])) ||
                    string.IsNullOrEmpty(Convert.ToString(Session["payStudName"])) ||
                    string.IsNullOrEmpty(Convert.ToString(Session["studEmail"])) ||
                    string.IsNullOrEmpty(Convert.ToString(Session["studPhone"])) ||
                    string.IsNullOrEmpty(Convert.ToString(Session["idno"])) ||
                    string.IsNullOrEmpty(ViewState["ResponseUrl"].ToString()) ||
                    string.IsNullOrEmpty(ViewState["ResponseUrl"].ToString()) ||
                    string.IsNullOrEmpty("payu_biz")

                    )
                {
                    //error
                    //frmError.Visible = true;
                    return;
                }
                else
                {
                    //frmError.Visible = false;
                    hashVarsSeq = ViewState["hashsequence"].ToString().Split('|'); // spliting hash sequence from config
                    hash_string = "";
                    foreach (string hash_var in hashVarsSeq)
                    {
                        if (hash_var == "key")
                        {
                            hash_string = hash_string + ViewState["merchentkey"].ToString();
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "txnid")
                        {
                            hash_string = hash_string + txnid1;
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "amount")
                        {
                            hash_string = hash_string + Convert.ToDecimal(Session["studAmt"]).ToString("g29");
                            hash_string = hash_string + '|';
                        }
                        else
                        {

                            hash_string = hash_string + (Request.Form[hash_var] != null ? Request.Form[hash_var] : "");// isset if else
                            hash_string = hash_string + '|';
                        }
                    }

                    hash_string += ViewState["saltkey"].ToString();// appending SALT

                    hash1 = Generatehash512(hash_string).ToLower();         //generating hash
                    action1 = ViewState["RequestUrl"].ToString() + "/_payment";// setting URL
                }
            }
            else if (!string.IsNullOrEmpty(Request.Form["hash"]))
            {
                hash1 = Request.Form["hash"];
                action1 = ViewState["RequestUrl"].ToString() + "/_payment";
            }
            if (!string.IsNullOrEmpty(hash1))
            {
                hash.Value = hash1;
                txnid.Value = txnid1;

                System.Collections.Hashtable data = new System.Collections.Hashtable(); // adding values in gash table for data post
                data.Add("hash", hash.Value);
                data.Add("txnid", txnid.Value);
                data.Add("key", key.Value);
                string amount = Convert.ToString(Session["studAmt"]);
                string AmountForm = Convert.ToDecimal(amount.Trim()).ToString("g29");// eliminating trailing zeros
                //amount.Text = AmountForm;
                data.Add("amount", AmountForm);
                data.Add("firstname", Convert.ToString(Session["payStudName"]));
                data.Add("email", Convert.ToString(Session["studEmail"]));
                data.Add("phone", Convert.ToString(Session["studPhone"]));
                data.Add("productinfo", Convert.ToString(Session["idno"]));
                //data.Add("productinfo", productinfo.Text.Trim());
                data.Add("surl", ViewState["ResponseUrl"].ToString());
                data.Add("furl", ViewState["ResponseUrl"].ToString());
                data.Add("service_provider", "payu_biz");
                //if (Request.QueryString["ReceiptType"] != null)
                //string ReceiptType = Request.QueryString["ReceiptType"];
                data.Add("udf1", udf1.Text.Trim());
                data.Add("udf2", udf2.Text.Trim());
                data.Add("udf3", udf3.Text.Trim());
                data.Add("udf4", udf4.Text.Trim());
                //data.Add("udf5", udf5.Text.Trim());
                //data.Add("udf6", udf6.Text.Trim());

                string strForm = PreparePOSTForm(action1, data);
                Page.Controls.Add(new LiteralControl(strForm));
            }

            else
            {
                //no hash
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        string returnpageurl = Convert.ToString(Session["ReturnpageUrl"]);
        Response.Redirect(returnpageurl);
    }

    private string PreparePOSTForm(string url, System.Collections.Hashtable data)      // post form
    {
        //Set a name for the form
        string formID = "PostForm";
        //Build the form using the specified data to be posted.
        StringBuilder strForm = new StringBuilder();
        strForm.Append("<form id=\"" + formID + "\" name=\"" +
                       formID + "\" action=\"" + url +
                       "\" method=\"POST\">");

        foreach (System.Collections.DictionaryEntry key in data)
        {

            strForm.Append("<input type=\"hidden\" name=\"" + key.Key +
                           "\" value=\"" + key.Value + "\">");
        }


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