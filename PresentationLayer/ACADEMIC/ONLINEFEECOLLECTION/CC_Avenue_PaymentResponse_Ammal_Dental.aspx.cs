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
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System.IO;
//using System.Net.ServicePointManager;

public partial class CC_Avenue_PaymentResponse : System.Web.UI.Page
{
    #region class
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController objFees = new FeeCollectionController();
    StudentController objStu=new StudentController ();
    SemesterRegistration objsem = new SemesterRegistration();
    string hash_seq = string.Empty;
    int degreeno = 0;
    int college_id = 0;
    #endregion
    string Idno = string.Empty;
    string userno = string.Empty;
    string Regno = string.Empty;
    string order_id = string.Empty;
    string ResJson = string.Empty;
    string msg = string.Empty;
    string accessCode1 = string.Empty;
    string workingKey = string.Empty;
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

            string OrganizationID = objCommon.LookUp("REFF", "ORGANIZATIONID", "");


            if (OrganizationID.ToString() == "16")
                {
                
                int activityno = Convert.ToInt32(objCommon.LookUp("ACD_Payment_ACTIVITY_MASTER", "ACTIVITYNO", "ACTIVITYNAME ='Online Payment'"));
                Session["payactivityno"] = activityno;
                }
      
            DataSet dscon = objFees.GetOnlinePaymentConfigurationDetails_WithDegree(Convert.ToInt32(OrganizationID), 0, Convert.ToInt32(Session["payactivityno"]), degreeno, 3);
            if (dscon.Tables[0] != null && dscon.Tables[0].Rows.Count > 0)
                {
                if (dscon.Tables[0].Rows.Count > 1)
                    {

                    }
                else
                    {
                   // Session["paymentId"] = ds1.Tables[0].Rows[0]["PAY_ID"].ToString();
                   // string RequestUrl = ds1.Tables[0].Rows[0]["PGPAGE_URL"].ToString();
                    accessCode1 = dscon.Tables[0].Rows[0]["ACCESS_CODE"].ToString();
                    workingKey = dscon.Tables[0].Rows[0]["CHECKSUM_KEY"].ToString();
                   // Response.Redirect(RequestUrl);
                    }
                }


                //System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;
                //string workingKey = ConfigurationManager.AppSettings["workingKey"];
                //string accessCode1 = ConfigurationManager.AppSettings["AccessCode"]; //"AVED47KD55AM13DEMA";
                // string workingKey = "EEC68E21693137DE538CC710CEEBF139";//put in the 32bit alpha numeric key in the quotes provided here
                CCACrypto ccaCrypto = new CCACrypto();
                string encResponse = ccaCrypto.Decrypt(Request.Form["encResp"], workingKey);

                #region  NOT IN USE

                //https://apitest.ccavenue.com/apis/servlet/DoWebTrans
               // string workingKey = ConfigurationManager.AppSettings["workingKey"];
               //// string workingKey = "EEC68E21693137DE538CC710CEEBF139";//put in the 32bit alpha numeric key in the quotes provided here
               // CCACrypto ccaCrypto = new CCACrypto();
               // string encResponse = ccaCrypto.Decrypt(Request.Form["encResp"], workingKey);
               //
               // string orderStatusQuery = encResponse; // Ex.= CCAvenue Reference No.|Order No.|
               // string encQuery = "";
               //
               // string queryUrl = "https://testlogin.ccavenue.com/apis/servlet/DoWebTrans";
               //
               // encQuery = ccaCrypto.Encrypt(orderStatusQuery, workingKey);
               // string  accessCode="AVED47KD55AM13DEMA";
               //
               // // make query for the status of the order to ccAvenues change the command param as per your need
               // string authQueryUrlParam = "enc_request=" + encQuery + "&access_code=" + accessCode + "&command=orderStatusTracker&request_type=STRING&response_type=STRING";
               // String message = postPaymentRequestToGateway(queryUrl, authQueryUrlParam);


          //  NameValueCollection param = getResponseMap(message);
          //  String status = "";
          //  String encRes = "";
          //  if (param != null && param.Count == 2)
          //  {
          //      for (int i = 0; i < param.Count; i++)
          //      {
          //          if ("status".Equals(param.Keys[i]))
          //          {
          //              status = param[i];
          //          }
          //          if ("enc_response".Equals(param.Keys[i]))
          //          {
          //              encRes = param[i];
          //              //Response.Write(encResXML);
          //          }
          //      }
          //      if (!"".Equals(status) && status.Equals("0"))
          //      {
          //          String ResString = ccaCrypto.Decrypt(encRes, workingKey);
          //          Response.Write(ResString);
          //      }
          //      else if (!"".Equals(status) && status.Equals("1"))
          //      {
          //          Console.WriteLine("failure response from ccAvenues: " + encRes);
          //      }
          //
          //  }
                //

                #endregion

                NameValueCollection Params = new NameValueCollection();
                string[] segments = encResponse.Split('&');
                //string UserNo = Session["userno"].ToString();
                foreach (string seg in segments)
                {
                    string[] parts = seg.Split('=');
                    if (parts.Length > 0)
                    {
                        string Key = parts[0].Trim();
                        string Value = parts[1].Trim();
                        Params.Add(Key, Value);
                    }
                }

                // ##############JSON RESPONCE
                   #region JSON_RESPONCE
                string orderStatusQueryJson = "{ \"reference_no\":\"" + Params["tracking_id"].ToString() + "\", \"order_no\":\"" + Params["order_id"].ToString() + "\" }";
                string encJson = "";//Params["order_id"]
                string queryUrl = "https://logintest.ccavenue.com/apis/servlet/DoWebTrans";
                // "https://apitest.ccavenue.com/apis/servlet/DoWebTrans";
                //https://login.ccavenue.com/apis/servlet/DoWebTrans
                //"https://logintest.ccavenue.com/apis/servlet/DoWebTrans?enc_request=&access_code=&request_type=JSON&response_type=JSON&command=orderStatusTracker&version=1.2"
                CCACrypto ccaCrypto1 = new CCACrypto();
                encJson = ccaCrypto1.Encrypt(orderStatusQueryJson, workingKey);
                // make query for the status of the order to ccAvenues change the command param as per your need
                string authQueryUrlParam = "enc_request=" + encJson + "&access_code=" + accessCode1 + "&command=orderStatusTracker&request_type=JSON&response_type=JSON";
                // Url Connection
                String message = postPaymentRequestToGateway(queryUrl, authQueryUrlParam);
                //Response.Write(message);
                String status = "";
                String encResJson = "";
                NameValueCollection param = getResponseMap(message);
               
                if (param != null && param.Count == 2)
                {
                    for (int i = 0; i < param.Count; i++)
                    {
                        if ("status".Equals(param.Keys[i]))
                        {
                            status = param[i];
                        }
                        if ("enc_response".Equals(param.Keys[i]))
                        {
                            encResJson = param[i];
                            //Response.Write(encResXML);
                        }
                    }
                    if (!"".Equals(status) && status.Equals("0"))
                    {
                        
                            ResJson = ccaCrypto.Decrypt(encResJson, workingKey);
                          //  msg = ResJson;
                       // Response.Write(ResJson);
                    }
                    else if (!"".Equals(status) && status.Equals("1"))
                    {
                       // Console.WriteLine("failure response from ccAvenues: " + encResJson);
                    }

                }

                #endregion
                // ##############JSON RESPONCE
                Idno = Params["merchant_param5"];
                ViewState["IDNO"] = Idno;
                DataSet ds = objCommon.FillDropDown("USER_ACC U INNER JOIN ACD_STUDENT S ON(S.IDNO = U.UA_IDNO) INNER JOIN ACD_BRANCH B ON(B.BRANCHNO = S.BRANCHNO)", "UA_NAME", "UA_NO,UA_TYPE,UA_FULLNAME,UA_IDNO,UA_FIRSTLOG,B.LONGNAME", "UA_IDNO=" + Convert.ToInt32(Idno), string.Empty);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    Session["username"] = ds.Tables[0].Rows[0]["UA_NAME"].ToString();
                    Session["usertype"] = ds.Tables[0].Rows[0]["UA_TYPE"].ToString();
                    Session["userfullname"] = ds.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
                    Session["idno"] = ds.Tables[0].Rows[0]["UA_IDNO"].ToString();
                    Session["firstlog"] = ds.Tables[0].Rows[0]["UA_FIRSTLOG"].ToString();
                    Session["userno"] = ds.Tables[0].Rows[0]["UA_NO"].ToString();
                    Session["branchname"] = ds.Tables[0].Rows[0]["LONGNAME"].ToString();
                }               
                order_id = Params["order_id"];//adde by gaurav
                ViewState["Order_id"] = order_id;
                Regno = Convert.ToString(objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + Idno)); //adde by gaurav
                // added by gaurav START
                lblRegNo.Text = Regno;              
                string tranID = Params["tracking_id"];
                string orderno = Params["order_id"];//Params["billing_address"]
                int installmentno = Convert.ToInt32(Params["installno"]);
                lblBranch.Text = Params["merchant_param2"];
                string semester = objCommon.LookUp("ACD_DCR_TEMP", "SEMESTERNO", "IDNO=" + Idno.ToString() + "and RECIEPT_CODE='" + Params["billing_address"] + "'");
                lblSemester.Text = semester;
                lblOrderId.Text = order_id;
                lblTransactionDate.Text = System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                string StatusF = Params["order_status"];
               // string msg = "";
                string msg = ResJson;
                userno = Params["merchant_param1"];
                //lblmessage.Text = Params["billing_notes"];

                Session["userno"] = userno;
                DataSet ds1 = objCommon.FillDropDown("USER_ACC", "UA_NAME", "UA_TYPE,UA_FULLNAME,UA_IDNO,UA_FIRSTLOG", "UA_NO=" + Convert.ToInt32(Session["userno"]), string.Empty);
                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                {

                    Session["username"] = ds1.Tables[0].Rows[0]["UA_NAME"].ToString();
                    Session["usertype"] = ds1.Tables[0].Rows[0]["UA_TYPE"].ToString();
                    Session["userfullname"] = ds1.Tables[0].Rows[0]["UA_FULLNAME"].ToString();
                    Session["idno"] = ds1.Tables[0].Rows[0]["UA_IDNO"].ToString();
                    Session["firstlog"] = ds1.Tables[0].Rows[0]["UA_FIRSTLOG"].ToString();

                }

                Session["coll_name"] = objCommon.LookUp("REFF", "CollegeName", "");
                Session["colcode"] = objCommon.LookUp("REFF", "COLLEGE_CODE", "");
                Session["currentsession"] = objCommon.LookUp("ACD_SESSION_MASTER", "MAX(SESSIONNO)", "SESSIONNO>0");
                Session["sessionname"] = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", "SESSIONNO=(SELECT MAX(SESSIONNO) FROM ACD_SESSION_MASTER WHERE SESSIONNO>0)");

                Session["payment"] = "payment";
                string payId = orderno;
               // hdfOrderId.Value = payId;
                string transid = tranID;
                string recipt = Params["billing_address"];//ADDED FOR RECIEPT CODE
                 status = string.Empty;
                string PaymentMode = "O";
                string CashBook = string.Empty;
                string amount = Params["amount"];
                lblTrasactionId.Text = tranID;
               // string mihpayid = Params["tid"];

                lblamount.Text = amount;
                lblstudentname.Text = Params["delivery_name"];

               // lblidno.Text = Regno;
              // ldlresponint output = 0;ceHandling.Text = payId;
                int output = 0;
                if (StatusF == "Success")
                {
                    divSuccess.Visible = true;

                    if (Convert.ToInt32(installmentno) > 0)
                        {
                        output = objFees.InsertInstallmentOnlinePayment_DCR(Idno, recipt, order_id, tranID, "O", "1", amount, "Success", Convert.ToInt32(installmentno), "-");
                        }
                    else
                        {
                         output = objFees.InsertOnlinePayment_DCR(Idno, recipt, order_id, tranID, "O", "1", amount, "Success", Regno, msg);
                        }

                    if (output == -99)
                    {
                        divSuccess.Visible = false;
                        divFailure.Visible = true;
                        status = "Payment Fail";

                        objFees.InsertOnlinePaymentlog(Idno, recipt, PaymentMode, amount, status, order_id);
                    }
                    else
                    {
                        ViewState["out"] = output;

                    }
                    btnPrint.Visible = true;
                }
                else
                {
                  

                    divSuccess.Visible = false;
                    divFailure.Visible = true;
                    int result = 0;
                    string PaymentFor = string.Empty, txnMessage = string.Empty, BankReferenceNo = string.Empty;
                    string rec_code = objCommon.LookUp("ACD_DCR_TEMP", "RECIEPT_CODE", "ORDER_ID = '" + order_id + "'");
                    objFees.InsertOnlinePaymentlog(Idno, rec_code, "O", amount, "Payment Fail", order_id);

                  //  result = objFees.OnlineInstallmentFeesPayment(tranID, order_id, amount, "0000", "", PaymentFor, txnMessage, BankReferenceNo, PaymentFor, rec_code);
                    btnPrint.Visible = false;

                }


            }
            catch (Exception ex)
            {
                //lblErrorLog.Text = ex.StackTrace.ToString();
               Response.Write(ex.Message);
               
            }
        }
    
    }

    #region Method
    public void TransferToEmail1(string ToID, string userMsg, string userMsg1, string userMsg2, string messBody3, string messBody4, string messBody5)
    {
        try
        {         
            ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;            
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

  
    protected void btnBack_Click(object sender, EventArgs e)
    {

        //data.Add("redirect_url", Session["ReturnpageUrl"]);
        string returnpageurl = Convert.ToString(ViewState["ReturnpageUrl"]);
        //Response.Redirect(returnpageurl);
        Response.Redirect("~/default.aspx");
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

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ShowReport("OnlineFeePayment", "rptOnlineReceipt.rpt");

    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            int IDNO = Convert.ToInt32(ViewState["IDNO"]);

            string DcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO='" + IDNO + "' AND ORDER_ID ='" + ViewState["Order_id"].ToString() + "'");

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo);

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
            //To open new window from Updatepanel
           
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objUaimsCommon.ShowError(Page, "CourseWise_Registration.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objUaimsCommon.ShowError(Page, "Server Unavailable.");
        }
    }
    private string postPaymentRequestToGateway(String queryUrl, String urlParam)
    {

        String message = "";
        try
        {
            StreamWriter myWriter = null;// it will open a http connection with provided url
            WebRequest objRequest = WebRequest.Create(queryUrl);//send data using objxmlhttp object
            objRequest.Method = "POST";
            //objRequest.ContentLength = TranRequest.Length;
            objRequest.ContentType = "application/x-www-form-urlencoded";//to set content type
            myWriter = new System.IO.StreamWriter(objRequest.GetRequestStream());
            myWriter.Write(urlParam);//send data
            myWriter.Close();//closed the myWriter object

            // Getting Response
            System.Net.HttpWebResponse objResponse = (System.Net.HttpWebResponse)objRequest.GetResponse();//receive the responce from objxmlhttp object 
            using (System.IO.StreamReader sr = new System.IO.StreamReader(objResponse.GetResponseStream()))
            {
                message = sr.ReadToEnd();
                //Response.Write(message);
            }
        }
        catch (Exception exception)
        {
            Console.Write("Exception occured while connection." + exception);
        }
        return message;

    }

    private NameValueCollection getResponseMap(String message)
    {
        NameValueCollection Params = new NameValueCollection();
        if (message != null || !"".Equals(message))
        {
            string[] segments = message.Split('&');
            foreach (string seg in segments)
            {
                string[] parts = seg.Split('=');
                if (parts.Length > 0)
                {
                    string Key = parts[0].Trim();
                    string Value = parts[1].Trim();
                    Params.Add(Key, Value);
                }
            }
        }
        return Params;
    }
}