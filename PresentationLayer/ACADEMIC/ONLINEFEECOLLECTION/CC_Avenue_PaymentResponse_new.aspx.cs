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
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG;
//using System.Net.ServicePointManager;

public partial class CC_Avenue_PaymentResponse : System.Web.UI.Page
{
    #region class
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController objFees = new FeeCollectionController();
    StudentController objStu = new StudentController();
    SemesterRegistration objsem = new SemesterRegistration();
    OrganizationController objOrg = new OrganizationController();

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
        //CCACrypto ccaCrypto = new CCACrypto();
        //string encResponse = string.Empty;
        //DataSet dsworking = objCommon.FillDropDown("ACD_PG_CONFIGURATION", "CHECKSUM_KEY", "COLLEGE_ID", "", "");
        //int Count = 0;
        //for (int i = 1; i <= dsworking.Tables[0].Rows.Count; i++)
        //{
        //    Count++;
        //    workingKey = dsworking.Tables[0].Rows[0]["CHECKSUM_KEY"].ToString();
        //    // encResponse = ccaCrypto.Decrypt(Request.Form["encResp"], workingKey);
        //    try
        //    {
        //        lblwork1.Text = ccaCrypto.Decrypt("e7b6bc6adcdb20e424df5d0b9e8bd94b2251d834971aad8472f4e62e2edd9178e28ace0e1964c530208e132143326bfd7a37ea1a9514e2ada4a5afd7cb8a522db12bfc4ebf560f026731fae530872b4f20ad2edc33f742d9df3f1683d16c41cdc80089ab541b344cd69d85c87dc1e02c95aea38d1d25041adb5cb9af492fba9e12d242e779096434ba9e5df0adc4d30c7307a5ace7011f4d344d42465513b263cc2e41694f75935864c7093c7b07916600b0d0d45a095e07fab3645bda79213647361a528e649f24db6366258efeed88bfcfe6b52ccdb4ef9b0b532dea37d0363461859d6f986a93532e7a97e004764b96e5b00a88c25f275a1991242109a567bdffd4d3299760c6ce450114c03d92fe39ea6396ff5e5eff2eb6bebd4f164cfc2eeaf6e187edf6ab7cd63bb99e1366ad48389254205ec6a3c3a3bf7a15f2e77301998b7b43db86f1c27f89dee99790f403ab61bf9c953d740885c11141207f6d14c887f9018656d5675cbf3e34898052228a81bcafab9baac4055af1fb6913df7f361effd643f8816c1d8e64e247e0369d3d7ef2e3002d1d965922c021669f4f62e9b6ba4bcadb9552a56ad5f5079562927ea0b9662dd991eff34115b19e3bc0f11fb5ec1a9bcf4833ebaf254e206d89299b205932e6d7f156fe8846455d36040a22c2142a74740ed55bb58b4912e7ee8b25d5ba6aa2a8001f2eec0ab4d340eff36a64bdeacc3139395c08264102918229dae21c7f69357c343a75b8d9126dbe9a58282bb6432cc09d17a4878142ddd7289786fa98decebc03d7c092fd751f49091d56d3bbcc86ff27baca58a8cd0a0ee3014d0ae4bf881e40d48612084c1637f7f93bc06901e5c670443dfc7320383c57daa8ed0027f40543c461449608a65f466cec1821f8727cb8595d28f0315a1f0751411c7d4d18ae9e41b62989419ec2743e8a215439d9e798bc68a3da7440d192d9c9591753b26602941d7182a41109fc152d7a3bb27b371cd403275ab1183ac03abca054709acc72e8714aeede51ac9f6e38514d75e26110b5bad3b498badcd15fd5cfe000ac69964d256aa10127295d740564c61fc28c407abd505364dd9bfedce21902dbfdd59f4a46a0fc1a0c66b4997e1b4e2c759bc1ea28de4b766245a0fc4adac5b37b2ad634330ba12618381faa427c1c227eb2dee901e8c21a31270908e488a75e1e19c2fd91c9b90c0f20ffb06e75cbed0dc9b43d45336cf20be85486e55da8f8c06f4f5f8b6f5d50f0fe5d87fcf12c74601fbf1a7d9aafa720dbeca36784b35e5b2545840c781380efa5e8a69712d0fe7c6112f3e9df8e3ca56742a9d0c1c2aabd2f4a7ab3ee26bdb1a76c1efe4e0ae2b903b91caab9cc76fb9ced15dd41af12893f758e0912c5657381", workingKey);
        //        break;
        //    }
        //    catch (Exception Ex)
        //    {
        //        continue;
        //    }
        //}
           
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

                string OrganizationID = objCommon.LookUp("REFF", "ORGANIZATIONID", "");

                if (OrganizationID.ToString() == "16")
                {

                    int activityno = Convert.ToInt32(objCommon.LookUp("ACD_Payment_ACTIVITY_MASTER", "ACTIVITYNO", "ACTIVITYNAME ='Online Payment'"));
                    Session["payactivityno"] = activityno;
                }

                DataSet dscon = objFees.GetOnlinePaymentConfigurationDetails_WithDegree(Convert.ToInt32(OrganizationID), 0, Convert.ToInt32(Session["payactivityno"]), degreeno, 5);
                if (dscon.Tables[0] != null && dscon.Tables[0].Rows.Count > 0)
                {
                    if (dscon.Tables[0].Rows.Count > 1)
                    {

                    }
                    else
                    {
                        //Session["paymentId"] = ds1.Tables[0].Rows[0]["PAY_ID"].ToString();
                        //string RequestUrl = ds1.Tables[0].Rows[0]["PGPAGE_URL"].ToString();
                        accessCode1 = dscon.Tables[0].Rows[0]["ACCESS_CODE"].ToString();
                        workingKey = dscon.Tables[0].Rows[0]["CHECKSUM_KEY"].ToString();
                        // Response.Redirect(RequestUrl);
                    }
                }


                CCACrypto ccaCrypto = new CCACrypto();
                string encResponse = string.Empty;
                DataSet dsworking = objCommon.FillDropDown("ACD_PG_CONFIGURATION", "DISTINCT CHECKSUM_KEY", "COLLEGE_ID", "ISNULL(ACTIVE_STATUS,0)=1", "");
                //int Count = 0;
                for (int i = 1; i <= dsworking.Tables[0].Rows.Count; i++)
                {
                  //  Count++;
                    workingKey = dsworking.Tables[0].Rows[i]["CHECKSUM_KEY"].ToString();
                    // encResponse = ccaCrypto.Decrypt(Request.Form["encResp"], workingKey);
                    try
                    {
                        encResponse = ccaCrypto.Decrypt(Request.Form["encResp"], workingKey);
                        break;
                    }
                    catch (Exception Ex)
                    {
                        continue;
                        //lblResponse.Text = Request.Form["encResp"].ToString();
                    }
                    
                }
              //  lblResponse.Text = Request.Form["encResp"].ToString();

                //try
                //{
                //    encResponse = ccaCrypto.Decrypt(Request.Form["encResp"], workingKey);
                //}
                //catch (Exception Ex)
                //{
                //    lblResponse.Text = Request.Form["encResp"].ToString();
                //    lblErrormsg.Text = encResponse.ToString();
                //    lblmessage.Text = Ex.Message.ToString();
                //}


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
                int installmentno = Convert.ToInt32(Params["merchant_param3"]);
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
                lblErrorLog.Text = ex.StackTrace.ToString();
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
        //ShowReport("OnlineFeePayment", "rptOnlineReceipt.rpt");
        ShowReport("OnlineFeePayment", "rptOnlineReceipt_New.rpt");
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            int IDNO = Convert.ToInt32(ViewState["IDNO"]);

            string DcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO='" + ViewState["IDNO"].ToString() + "' AND ORDER_ID ='" + Convert.ToString(ViewState["order_id"]) + "'");
            int college_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(IDNO)));
            Session["UAFULLNAME"] = objCommon.LookUp("USER_ACC", "UA_FULLNAME", "UA_NO=" + Convert.ToInt32(Session["userno"]));

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