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

public partial class ACADEMIC_ONLINEFEECOLLECTION_OmniwareOnlinePaymentRequest : System.Web.UI.Page
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

    int degreeno = 0;
    int college_id = 0;

    public string action = string.Empty;
    public string hash = string.Empty;

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
                if (Session["OrgId"].ToString() == "16")
                {
                    degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
                }
                if (Session["OrgId"].ToString() == "16")
                {
                    college_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
                }

                lblRegNo.Text = Session["regno"].ToString();
                lblstudentname.Text = Convert.ToString(Session["payStudName"]);
                lblBranch.Text = Convert.ToString(Session["Branchname"]);
                firstname.Text = lblstudentname.Text;

                lblSemester.Text = Convert.ToString(Session["paysemester"]);
                email.Text = Convert.ToString(Session["studEmail"]);
                phone.Text = Convert.ToString(Session["studPhone"]);
                lblamount.Text = Convert.ToString(Session["studAmt"]);
                int payId = Convert.ToInt32(Session["paymentId"]);
                lblYear.Text = Session["YEARNO"].ToString();

                DataSet ds1 = objFees.GetOnlinePaymentConfigurationDetails_WithDegree(Convert.ToInt32(Session["OrgId"]), payId, 1, Convert.ToInt32(degreeno), Convert.ToInt32(college_id));
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
                    surl.Text = ResponseUrl;
                    furl.Text = ResponseUrl;
                    productinfo.Text = Convert.ToString(Session["idno"]);
                    udf1.Text = Convert.ToString(Session["OrgId"]);
                    udf2.Text = payId.ToString();
                    udf3.Text = Convert.ToString(Session["payactivityno"]);
                    udf4.Text = Convert.ToString(Session["Installmentno"]);
                    if (ds1.Tables[0].Rows[0]["INSTANCE"].ToString() == "1")
                    {
                        lblmode.Text = "TEST";
                    }
                    else
                    {
                        lblmode.Text = "LIVE";
                    }
                }
                lblcity.Text = objCommon.LookUp("ACD_STU_ADDRESS A INNER JOIN ACD_CITY B ON (A.PCITY = B.CITYNO)", "B.CITY", "IDNO=" + Convert.ToInt32(Session["idno"].ToString()));
                if (lblcity.Text == string.Empty)
                {
                    lblcity.Text = "-";
                }

                lblcountry.Text = "IND";
                lblcurrency.Text = "INR";
                lbldescription.Text = Convert.ToString(Session["idno"]);
                

                lblstate.Text = objCommon.LookUp("ACD_STU_ADDRESS A INNER JOIN ACD_STATE B ON (A.PSTATE = B.STATENO)", "B.STATENAME", "IDNO=" + Convert.ToInt32(Session["idno"].ToString()));
                lblzipcode.Text = objCommon.LookUp("ACD_STU_ADDRESS", "PPINCODE", "IDNO=" + Convert.ToInt32(Session["idno"].ToString()));
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

            msg.Subject = "Your transaction with SIMS";

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
    Reprocess:
        string UserId = Convert.ToString(Session["userno"]);
        string country = "IND";
        string currency = "INR";
        string return_url = ViewState["ResponseUrl"].ToString();
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

        int result = 0;
        //Session["ReceiptType"] = "O";
        //Session["PaymentMode"] = "O";
        //Session["studAmt"] = lblamount.Text;
        //Session["demandno"] = "1";
        //Session["paysemester"] = "1";
        //Session["paysession"] = "1";

        objFees.InsertOnlinePaymentlog(Convert.ToString(Session["idno"]), Session["ReceiptType"].ToString(), Convert.ToString(Session["PaymentMode"]), Convert.ToString(Session["studAmt"]), "Not Continued", txnid1);

        if (Convert.ToInt32(Session["Installmentno"]) > 0)
        {
            result = objFees.InsertInstallmentOnlinePayment_TempDCR(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["demandno"]), Convert.ToInt32(Session["paysemester"]), txnid1, Convert.ToDouble(Session["studAmt"]), Convert.ToString(Session["ReceiptType"]), Convert.ToInt32(Session["userno"]), "-");
        }
        else
        {
            result = objFees.InsertOnlinePayment_TempDCR(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["paysession"]), Convert.ToInt32(Session["paysemester"]), txnid1, 1, Convert.ToString(Session["ReceiptType"]), "-");
        }

        lblorder_id.Text = txnid1;
        try
        {
            string[] hashVarsSeq;
            string hash_string = string.Empty;

            string orderid = objCommon.LookUp("ACD_DCR_TEMP", "ORDER_ID", "IDNO = " + Convert.ToInt32(Session["idno"]) + " AND ORDER_ID='" + txnid1 + "'");
            

            if (string.IsNullOrEmpty(Request.Form["hash"])) // generating hash value
            {
                hashVarsSeq = ViewState["hashsequence"].ToString().Split('|'); // spliting hash sequence from config
                hash_string = "";
                foreach (string hash_var in hashVarsSeq)
                {
                    if (hash_var == "api_key")
                    {
                        hash_string = hash_string + ViewState["merchentkey"].ToString().Trim();
                        hash_string = hash_string + '|';
                    }
                    else if (hash_var == "amount")
                    {
                        hash_string = hash_string + Convert.ToDecimal(lblamount.Text.Trim()).ToString("g29");
                        hash_string = hash_string + '|';
                    }
                    else if (hash_var == "SALT")
                    {
                        hash_string = hash_string + ViewState["saltkey"].ToString().Trim();
                        hash_string = hash_string + '|';
                    }
                    else if (hash_var == "mode")
                    {
                        hash_string = hash_string + lblmode.Text.Trim();
                        hash_string = hash_string + '|';
                    }
                    else if (hash_var == "country")
                    {
                        hash_string = hash_string + country;
                        hash_string = hash_string + '|';
                    }
                    else if (hash_var == "currency")
                    {
                        hash_string = hash_string + currency;
                        hash_string = hash_string + '|';
                    }
                    else if (hash_var == "return_url")
                    {
                        hash_string = hash_string + return_url;
                        hash_string = hash_string + '|';
                    }
                    else if (hash_var == "address_line_1")
                    {
                        hash_string = hash_string + lblCollege.Text.Trim();
                        hash_string = hash_string + '|';
                    }
                    else if (hash_var == "address_line_2")
                    {
                        hash_string = hash_string + lblAddress.Text.Trim();
                        hash_string = hash_string + '|';
                    }
                    else if (hash_var == "city")
                    {
                        hash_string = hash_string + lblcity.Text.Trim();
                        hash_string = hash_string + '|';
                    }
                    else if (hash_var == "description")
                    {
                        hash_string = hash_string + lbldescription.Text.Trim();
                        hash_string = hash_string + '|';
                    }
                    else if (hash_var == "email")
                    {
                        hash_string = hash_string + email.Text.Trim();
                        hash_string = hash_string + '|';
                    }
                    else if (hash_var == "name")
                    {
                        hash_string = hash_string + lblstudentname.Text.Trim();
                        hash_string = hash_string + '|';
                    }
                    else if (hash_var == "order_id")
                    {
                        hash_string = hash_string + lblorder_id.Text.Trim();
                        hash_string = hash_string + '|';
                    }
                    else if (hash_var == "phone")
                    {
                        hash_string = hash_string + phone.Text.Trim();
                        hash_string = hash_string + '|';
                    }
                    else if (hash_var == "state")
                    {
                        hash_string = hash_string + lblstate.Text.Trim();
                        hash_string = hash_string + '|';
                    }
                    else if (hash_var == "zip_code")
                    {
                        hash_string = hash_string + lblzipcode.Text.Trim();
                        hash_string = hash_string + '|';
                    }
                    else
                    {
                        if (Request.Form[hash_var] != "")
                        {
                            hash_string = hash_string + Request.Form[hash_var];
                            hash_string = hash_string + '|';
                        }
                    }
                }

                hash_string = hash_string.Substring(0, hash_string.Length - 1);
                hash = Generatehash512(hash_string).ToUpper();         //generating hash
                ViewState["hash"] = hash;
                action = ViewState["RequestUrl"].ToString();// setting URL
            }
            else if (!string.IsNullOrEmpty(Request.Form["hash"]))
            {
                hash = Request.Form["hash"];
                action = ViewState["RequestUrl"].ToString() + "/_payment";
            }
            if (!string.IsNullOrEmpty(hash))
            {


                System.Collections.Hashtable data = new System.Collections.Hashtable(); // adding values in gash table for data post
                data.Add("address_line_1", lblCollege.Text.Trim());
                data.Add("address_line_2", lblAddress.Text.Trim());
                string AmountForm = Convert.ToDecimal(lblamount.Text.Trim()).ToString("g29");// eliminating trailing zeros
                data.Add("amount", AmountForm);
                data.Add("api_key", ViewState["merchentkey"].ToString().Trim());
                data.Add("city", lblcity.Text.Trim());
                data.Add("country", lblcountry.Text.Trim());
                data.Add("currency", lblcurrency.Text.Trim());
                data.Add("description", lbldescription.Text.Trim());
                data.Add("email", email.Text.Trim());
                data.Add("mode", lblmode.Text.Trim());
                data.Add("name", lblstudentname.Text.Trim());
                data.Add("order_id", lblorder_id.Text.Trim());
                data.Add("phone", phone.Text.Trim());
                data.Add("return_url", return_url);
                data.Add("state", lblstate.Text.Trim());
                data.Add("udf1", udf1.Text.Trim());
                data.Add("udf2", udf2.Text.Trim());
                data.Add("udf3", udf3.Text.Trim());
                data.Add("udf4", udf4.Text.Trim());
                data.Add("udf5", udf5.Text.Trim());
                data.Add("zip_code", lblzipcode.Text.Trim());
                data.Add("hash", hash);

                string strForm = PreparePOSTForm(action, data);
                Page.Controls.Add(new LiteralControl(strForm));

            }

        }

        catch (Exception ex)
        {
            Response.Write("<span style='color:red'>" + ex.Message + "</span>");

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