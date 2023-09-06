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

public partial class CC_Avenue_PaymentRequest : System.Web.UI.Page
    {

    CCACrypto ccaCrypto = new CCACrypto();
    // string workingKey = "EEC68E21693137DE538CC710CEEBF139";//put in the 32bit alpha numeric key in the quotes provided here
    string ccaRequest = "";
    public string strEncRequest = "";
    //public string strAccessCode="";
    public string strAccessCode;// put the access key in the quotes provided here.
    public string bckbutton;

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
    int installno = 0;

    protected void Page_Load(object sender, EventArgs e)
        {
        strAccessCode = Session["AccessCode"].ToString();

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

                degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));
                college_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));

                installno = Convert.ToInt32(objCommon.LookUp("ACD_FEES_INSTALLMENT", "COUNT(INSTALMENT_NO)", "IDNO=" + Convert.ToInt32(Session["idno"].ToString())));

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
                DataSet ds1 = objFees.GetOnlinePaymentConfigurationDetails_WithDegree(Convert.ToInt32(Session["OrgId"]), payId, Convert.ToInt32(Session["payactivityno"]),0, 0);
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
                    }


                else
                    {


                    }
                }
            catch (Exception ex)
                {
                Response.Write(ex.Message);
                }
            }
        CCAvenue();
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


    protected void btnPay_Click(object sender, EventArgs e)//Pay()
        {


        }
    protected void btnBack_Click1(object sender, EventArgs e)
        {
        string returnpageurl = Convert.ToString(Session["ReturnpageUrl"]);
        Response.Redirect(returnpageurl);
        }

    protected void CCAvenue()
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

        int result = 0;
        objFees.InsertOnlinePaymentlog(Convert.ToString(Session["idno"]), Session["ReceiptType"].ToString(), Convert.ToString(Session["PaymentMode"]), Convert.ToString(Session["studAmt"]), "Not Continued", txnid1);
        if (Convert.ToInt32(Session["Installmentno"]) > 0)
            {
            result = objFees.InsertInstallmentOnlinePayment_TempDCR(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["demandno"]), Convert.ToInt32(Session["paysemester"]), txnid1, Convert.ToDouble(Session["studAmt"]), Convert.ToString(Session["ReceiptType"]), Convert.ToInt32(Session["userno"]), "-");
            }
        else
            {
            result = objFees.InsertOnlinePayment_TempDCR(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["paysession"]), Convert.ToInt32(Session["paysemester"]), txnid1, 1, Convert.ToString(Session["ReceiptType"]), "-");
            }

        //result = objFees.InsertOnlinePayment_TempDCR(Convert.ToInt32(Session["idno"]), Convert.ToInt32(Session["paysession"]), Convert.ToInt32(Session["paysemester"]), txnid1, 1, Convert.ToString(Session["ReceiptType"]), "-");

        //result = objFees.SubmitFeesofStudent(objStudentFees, 1, 2, lblReceiptCode.Text, Convert.ToInt32(demand_semester));
        //int orderid = Convert.ToInt32(objStudentFees.OrderID);


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
                //action1 = ViewState["RequestUrl"].ToString() + "/_payment";// setting URL
                action1 = ViewState["RequestUrl"].ToString();
                }
            }
        else if (!string.IsNullOrEmpty(Request.Form["hash"]))
            {
            hash1 = Request.Form["hash"];
            //action1 = ViewState["RequestUrl"].ToString() + "/_payment";
            action1 = ViewState["RequestUrl"].ToString();
            }
        if (!string.IsNullOrEmpty(hash1))
            {
            hash.Value = hash1;
            txnid.Value = txnid1;


            //##################################
            string amount = Session["studAmt"].ToString();   //session amount
            string studName = Session["studName"].ToString(); //student Name
            string studPhone = Session["studPhone"].ToString(); //student Phone
            string studEmail = Session["studEmail"].ToString(); //student Phone
            string refno = txnid1.ToString();  // unique number for every transaction i.e order id
            string homelink = Session["homelink"].ToString();     //Url for payment indentification       
            int Participant = Convert.ToInt32(Session["Participant"]);
            int TitleId = Convert.ToInt32(Session["TitleId"]);

            string reciept_code = Session["ReceiptType"].ToString();
            System.Collections.Hashtable data = new System.Collections.Hashtable();

            string MID = ViewState["merchentkey"].ToString();
            string workingKey = ViewState["saltkey"].ToString();
            // Compulsory information
            data.Add("tid", "");
            data.Add("merchant_id", MID);  //account id
            data.Add("order_id", refno);
            data.Add("amount", amount);
            data.Add("currency", "INR");

            //  data.Add("redirect_url", "https://test.ccavenue.com/transaction/transaction.do?command=initiateTransaction");   //Responsne Url Live
            //  data.Add("cancel_url", "http://localhost:61905/PresentationLayer/ACADEMIC/ONLINEFEECOLLECTION/CC_Avenue_PaymentResponce.aspx");	   //Responsne Url Live
            data.Add("redirect_url", ViewState["ResponseUrl"]);
            data.Add("cancel_url", ViewState["ResponseUrl"]);
            //Billing information(optional):
            data.Add("billing_name", studName);
            data.Add("billing_address", reciept_code);//Added by gaurav for RECIEPT CODE
            //data.Add("billing_city", "Ranchi");
            //data.Add("billing_state", "Jharkhand");
            //data.Add("billing_zip", "835103");
            //data.Add("billing_country", "India");
            data.Add("billing_tel", studPhone);
            data.Add("billing_email", studEmail);

            //Shipping information(optional):
            data.Add("delivery_name", studName);
            data.Add("delivery_address", Session["ReturnpageUrl"].ToString());
            //data.Add("delivery_city", "Ranchi");
            //data.Add("delivery_state", "Jharkhand");
            //data.Add("delivery_zip", "835103");
            //data.Add("delivery_country", "India");
            data.Add("delivery_tel", studPhone);


            //additional Info.
            data.Add("merchant_param1", Session["userno"]);    //payment for which course
            data.Add("merchant_param2", Session["Branchname"]);	//paramets for payment indentification
            data.Add("merchant_param3", "FEES COLLECTION");
            data.Add("merchant_param4", studPhone);
            data.Add("merchant_param5", Session["idno"]);
            data.Add("merchant_param6", installno);
            data.Add("merchant_param7", txnid1);
            //data.Add("promo_code", "");
            //data.Add("customer_identifier", userno);
            data.Add("offer_type", TitleId);
            data.Add("offer_code", TitleId);
            data.Add("eci_value", Session["Branchname"]);
            data.Add("billing_notes", Session["userno"]);
            //added by gaurav 

            //<input type="hidden" id="encRequest" name="encRequest" value="<%=strEncRequest%>"/>

            form1.Action = ViewState["RequestUrl"].ToString();          // "https://test.ccavenue.com/transaction/transaction.do?command=initiateTransaction";
            form1.Method = "post";


            foreach (System.Collections.DictionaryEntry key in data)
                {
                string name = key.Key.ToString();

                if (key.Key != null)
                    {
                    if (!name.StartsWith("_"))
                        {
                        ccaRequest = ccaRequest + key.Key + "=" + key.Value + "&";

                        }
                    }

                }

            strEncRequest = ccaCrypto.Encrypt(ccaRequest, workingKey);
            }

        else
            {
            Response.Write("Session is NULL");
            }
        }
    }