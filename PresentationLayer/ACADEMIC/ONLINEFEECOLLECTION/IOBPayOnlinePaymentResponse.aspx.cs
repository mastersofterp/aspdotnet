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
using System.Security.Cryptography.X509Certificates;
using mastersofterp_MAKAUAT;
using System.Net.Security;
using System.Data.SqlClient;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using EASendMail;
using System.Net;
using System.Net.Mail;
using BusinessLogicLayer.BusinessLogic;
using Newtonsoft.Json;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG;


public partial class IOBPayOnlinePaymentResponse : System.Web.UI.Page
{
    #region class
    Common objCommon = new Common();
    UAIMS_Common objUaimsCommon = new UAIMS_Common();
    FeeCollectionController objFees = new FeeCollectionController();
    StudentController objStu=new StudentController ();
    SemesterRegistration objsem = new SemesterRegistration();
    OrganizationController objOrg = new OrganizationController();
    string hash_seq = string.Empty;
    int degreeno = 0;
    int college_id = 0;
    #endregion

    FeeCollectionController feeController = new FeeCollectionController();
    SendEmailCommon objSendEmail = new SendEmailCommon(); //Object Creation

    string UserFirstPaymentStatus = string.Empty;
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
                //        Session["OrgId"] = dr["OrganizationId"].ToString();
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


                string order_id = string.Empty;
                string amount = string.Empty;
                string firstname = string.Empty;
                string emailId = string.Empty;
                string Idno = string.Empty;
                string recipt = string.Empty;
                string saltkey = string.Empty;
                int payid = 0;
                int orgid = 0;
                string hash_seq = string.Empty;
                string mihpayid = string.Empty;
                string Regno = string.Empty;
                string encryptionkey = string.Empty;
                string encryptioniv = string.Empty;

                string transactionDate = string.Empty;
                string totalAmount = string.Empty;
                string tokenID = string.Empty;
                string merchantID = string.Empty;
                string statustimeStamp = string.Empty;
                string trackID = string.Empty;
                string txnStatus = string.Empty;
                string actionInfo = string.Empty;
                string feeType = string.Empty;
                string merchanttxnID = string.Empty;
                string merchantSubID = string.Empty;
                string timeStamp = string.Empty;
                string studIdno = string.Empty;
                string studName = string.Empty;
                string extra = string.Empty;
                int payactivityno = 0;
                int installmentno = 0;


                #region IOBPay response

                string jsonData = (Request.Form["resjson"]);
                ResponseData deserializedData = JsonConvert.DeserializeObject<ResponseData>(jsonData);

                string data = deserializedData.Data;
                string Merchant_Id = deserializedData.MerchantId;
                string request_Id = deserializedData.RequestId;
                string Hmac = deserializedData.Hmac;
                string action = deserializedData.Action;
                string Merchant_SubId = deserializedData.MerchantSubId;

                DataSet pg_ds = objCommon.FillDropDown("ACD_PG_CONFIGURATION", "ACCESS_CODE", "HASH_SEQUENCE", "MERCHANT_ID= '" + Merchant_Id + "' ", "CONFIG_ID DESC");
                if (pg_ds != null && pg_ds.Tables[0].Rows.Count > 0)
                {
                    encryptionkey = pg_ds.Tables[0].Rows[0]["ACCESS_CODE"].ToString();      //ADD ENCRYPTION KEY HERE
                    encryptioniv = pg_ds.Tables[0].Rows[0]["HASH_SEQUENCE"].ToString();     //ADD ENCRYPTION IV HERE
                }


                EncryptHelper encrypthelper = new EncryptHelper();
                String decrypteddata = encrypthelper.Decrypt(data, encryptionkey, encryptioniv);

                TransactionData transactionData = JsonConvert.DeserializeObject<TransactionData>(decrypteddata);

                if (transactionData != null)
                {
                    transactionDate = transactionData.txndt;
                    totalAmount = transactionData.totalamt;
                    tokenID = transactionData.tokenid;
                    merchantID = transactionData.merchantid;
                    statustimeStamp = transactionData.statustimestamp;
                    trackID = transactionData.trackid;
                    txnStatus = transactionData.txnstatus;
                    actionInfo = transactionData.action;
                    feeType = transactionData.feetype;
                    merchanttxnID = transactionData.merchanttxnid;
                    merchantSubID = transactionData.merchantsubid;
                    timeStamp = transactionData.timestamp;
                    studIdno = transactionData.udf1;
                    //studName = "ABC";
                   installmentno=     Convert.ToInt32(transactionData.udf2);
                   // installmentno = Convert.ToInt32(transactionData.udf3);
                        
                    order_id = merchanttxnID;     //tokenID;
                    ViewState["order_id"] = order_id;
                    amount = totalAmount;
                    Idno = studIdno;

                    orgid = 0;                                   //Convert.ToInt32(Request.Form["udf1"]);
                    payid = 0;                                   //Convert.ToInt32(Request.Form["udf2"]);
                    payactivityno = 0;                           //Convert.ToInt32(Request.Form["udf3"]);
                   // installmentno = Convert.ToInt32(Session["Installmentno"]);                           //Convert.ToInt32(Request.Form["udf4"]);
                    //int installmentno = Convert.ToInt32(0);
                    ViewState["IDNO"] = Idno;

                    
                    //Added by Nikhil L. on 23-08-2022 for getting response and request url as per degreeno for RCPIPER.
                    if (Session["OrgId"].ToString() == "6")
                    {
                        degreeno = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "DEGREENO", "IDNO=" + Convert.ToInt32(ViewState["IDNO"].ToString())));
                    }
                    if (Session["OrgId"].ToString() == "8")
                    {
                        college_id = Convert.ToInt32(objCommon.LookUp("ACD_STUDENT", "COLLEGE_ID", "IDNO=" + Convert.ToInt32(ViewState["IDNO"].ToString())));
                    }



                    //**********************************End by Nikhil L.********************************************//

                    Regno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + ViewState["IDNO"].ToString());

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


                    Session["coll_name"] = objCommon.LookUp("REFF", "CollegeName", "");
                    Session["colcode"] = objCommon.LookUp("REFF", "COLLEGE_CODE", "");
                    Session["currentsession"] = objCommon.LookUp("ACD_SESSION_MASTER", "MAX(SESSIONNO)", "SESSIONNO>0");
                    Session["sessionname"] = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", "SESSIONNO=(SELECT MAX(SESSIONNO) FROM ACD_SESSION_MASTER WHERE SESSIONNO>0)");
                    string semester = objCommon.LookUp("ACD_DCR_TEMP", "SEMESTERNO", "IDNO=" + ViewState["IDNO"].ToString());
                    string Name = objCommon.LookUp("ACD_STUDENT", "STUDNAME", "IDNO=" + ViewState["IDNO"].ToString());


                    lblRegNo.Text = Regno;
                    lblstudentname.Text = Name;                                    //firstname;
                    lblOrderId.Text = order_id;
                    lblamount.Text = amount;

                    lblTransactionDate.Text = transactionDate;                          //System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                    mihpayid = trackID;                                                 //Request.Form["mihpayid"].ToString();
                    lblBranch.Text = Convert.ToString(Session["branchname"]);
                    lblSemester.Text = semester;
                    lblTrasactionId.Text = mihpayid;                                    //trackID ==  mihpayid; 


                    if (txnStatus.ToLower().ToString() == "success")     //Payment response in - (SUCCESS, FAILURE, AWAITED)
                    {

                        string UA_IDNO = objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_No = '" + Session["userno"] + "'");
                        string UA_NAME = objCommon.LookUp("USER_ACC", "UA_NAME", "UA_IDNO = '" + Convert.ToInt32(UA_IDNO) + "'");

                        divSuccess.Visible = true;
                        divFailure.Visible = false;
                        int result = 0;
                        string PaymentFor = string.Empty, txnMessage = string.Empty, BankReferenceNo = string.Empty;
                        string rec_code = objCommon.LookUp("ACD_DCR_TEMP", "RECIEPT_CODE", "ORDER_ID = '" + order_id + "'");
                        objsem.IdNo = Convert.ToInt32(Session["idno"]);
                        objsem.SESSIONNO = Convert.ToInt32(Session["currentsession"].ToString());
                        objsem.SemesterNO = Convert.ToInt32(lblSemester.Text);
                        objsem.paymentMode = 1;
                        objsem.OfflineMode = 0;
                        objsem.Total_Amt = Convert.ToDecimal(lblamount.Text);

                        objsem.IPADDRESS = Request.ServerVariables["REMOTE_HOST"];
                        objsem.Date_of_Payment = DateTime.Now.ToString("dd/MM/yyyy");
                        int output = 0;



                        if (Session["OrgId"].ToString() == "18")
                        {
                            UA_IDNO = objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_No = '" + Session["userno"] + "'");
                            UA_NAME = objCommon.LookUp("USER_ACC", "UA_NAME", "UA_IDNO = '" + Convert.ToInt32(UA_IDNO) + "'");
                            
                            if (UA_IDNO == UA_NAME)
                            {
                                UserFirstPaymentStatus = "5151";
                                ViewState["First_PaymentStatus"] = "5151";

                            }
                            else
                            {
                                ViewState["First_PaymentStatus"] = "0";
                            }
                        }
                        else
                        {
                            ViewState["First_PaymentStatus"] = "0";
                        }

                        if (Convert.ToInt32(installmentno) > 0)
                        {
                            output = objFees.InsertInstallmentOnlinePayment_DCR(Idno, rec_code, order_id, mihpayid, "O", "1", amount, "Success", Convert.ToInt32(installmentno), "-");

                            if (ViewState["First_PaymentStatus"] == "5151")
                            {
                                 UA_IDNO = objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_No = '" + Session["userno"] + "'");

                                UPDATE_USER(UA_IDNO, 1);

                                Sendmail();
                            }
                            else
                            {

                            }
                            }
                        else
                        {
                            output = objFees.InsertOnlinePayment_DCR(Idno, rec_code, order_id, mihpayid, "O", "1", amount, "Success", Regno, "-");

                            if (Session["OrgId"].ToString() == "18")
                            {
                                if (ViewState["First_PaymentStatus"].Equals("5151"))
                                {
                                     UA_IDNO = objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_No = '" + Session["userno"] + "'");

                                    UPDATE_USER(UA_IDNO, 1);

                                    Sendmail();
                                }
                                else
                                {

                                }
                            }
                            }


                        output = objFees.UpdateIOBPayOnlinePaymentlog(Convert.ToInt32(Idno), order_id, tokenID, trackID, feeType, txnStatus, timeStamp);
                      
                        btnPrint.Visible = true;

                    }

                    if (txnStatus.ToLower().ToString() == "failure")   // FAILURE
                    {
                        divSuccess.Visible = false;
                        divFailure.Visible = true;
                        int result = 0;
                        order_id = Session["OrderId"].ToString();

                        string PaymentFor = string.Empty, txnMessage = string.Empty, BankReferenceNo = string.Empty;
                        txnMessage = "";
                        string rec_code = objCommon.LookUp("ACD_DCR_TEMP", "RECIEPT_CODE", "ORDER_ID = '" + order_id + "'");
                        objFees.InsertOnlinePaymentlog(Idno, rec_code, "O", amount, "Payment Fail", order_id);

                        //result = objFees.OnlineInstallmentFeesPayment(mihpayid, order_id, amount, "0000", "", PaymentFor, txnMessage, BankReferenceNo, PaymentFor, rec_code);
                        result = objFees.UpdateIOBPayOnlinePaymentlog(Convert.ToInt32(Idno), order_id, tokenID, trackID, feeType, txnStatus, timeStamp);
                        btnPrint.Visible = false;
                    }


                    if (txnStatus.ToLower().ToString() == "awaited")    // AWAITED
                    {
                        divSuccess.Visible = false;
                        divFailure.Visible = true;
                        int result = 0;
                        order_id = Session["OrderId"].ToString();

                        string PaymentFor = string.Empty, txnMessage = string.Empty, BankReferenceNo = string.Empty;
                        txnMessage = "";
                        string rec_code = objCommon.LookUp("ACD_DCR_TEMP", "RECIEPT_CODE", "ORDER_ID = '" + order_id + "'");
                        objFees.InsertOnlinePaymentlog(Idno, rec_code, "O", amount, "Payment Fail", order_id);

                        //result = objFees.OnlineInstallmentFeesPayment(mihpayid, order_id, amount, "0000", "", PaymentFor, txnMessage, BankReferenceNo, PaymentFor, rec_code);
                        result = objFees.UpdateIOBPayOnlinePaymentlog(Convert.ToInt32(Idno), order_id, tokenID, trackID, feeType, txnStatus, timeStamp);
                        btnPrint.Visible = false;
                    }

                }
                #endregion

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }

    #region Method
    //public void TransferToEmail1(string ToID, string userMsg, string userMsg1, string userMsg2, string messBody3, string messBody4, string messBody5)
    //{
    //    try
    //    {
    //        //string path = Server.MapPath(@"/Css/images/Index.Jpeg");
    //        //LinkedResource Img = new LinkedResource(path, MediaTypeNames.Image.Jpeg);
    //        //Img.ContentId = "MyImage";   

    //        ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
    //        //string fromPassword = Common.DecryptPassword(objCommon.LookUp("REFF", "EMAILSVCPWD", string.Empty));
    //        //string fromAddress = objCommon.LookUp("REFF", "EMAILSVCID", string.Empty);
    //        string fromPassword = Common.DecryptPassword(objCommon.LookUp("Email_Configuration", "EMAILSVCPWD1", string.Empty));
    //        string fromAddress = objCommon.LookUp("Email_Configuration", "EMAILSVCID1", string.Empty);

    //        MailMessage msg = new MailMessage();
    //        SmtpClient smtp = new SmtpClient();

    //        msg.From = new MailAddress(fromAddress, "NIT GOA");
    //        msg.To.Add(new MailAddress(ToID));

    //        msg.Subject = "Your transaction with MAKAUT";

    //        const string EmailTemplate = "<html><body>" +
    //                                 "<div align=\"left\">" +
    //                                 "<table style=\"width:602px;border:#FFFFFF 3px solid\" cellspacing=\"0\" cellpadding=\"0\">" +
    //                                  "<tr>" +
    //                                  "<td>" + "</tr>" +
    //                                  "<tr>" +
    //                                 "<td width=\"100%\" style=\"vertical-align:top;text-align:left;padding:20px 15px 20px 15px;height:200px;FONT-FAMILY: Trebuchet MS;FONT-SIZE: 14px\">#content</td>" +
    //                                 "</tr>" +
    //                                 "<tr>" +
    //                                 "<td width=\"100%\" style=\"vertical-align:middle;text-align:left;padding:20px 15px 20px 15px;height:100px;FONT-FAMILY: Trebuchet MS;FONT-SIZE: 14px\"><img src=\"\"  id=\"../../Css/images/Index.png\" height=\"10\" width=\"10\"><br/><b>National Institute of Technology Goa </td>" +
    //                                 "</tr>" +
    //                                 "</table>" +
    //                                 "</div>" +
    //                                 "</body></html>";
    //        StringBuilder mailBody = new StringBuilder();
    //        //mailBody.AppendFormat("<h1>Greating !!</h1>");
    //        mailBody.AppendFormat("Dear <b>{0}</b> ,", messBody3);
    //        mailBody.AppendFormat("<br />");
    //        mailBody.AppendFormat("<br />");
    //        mailBody.AppendFormat(userMsg);
    //        mailBody.AppendFormat("<br />");
    //        mailBody.AppendFormat(messBody5);
    //        mailBody.AppendFormat("<br />");
    //        mailBody.AppendFormat(userMsg1);
    //        mailBody.AppendFormat("<br />");
    //        mailBody.AppendFormat(userMsg2);
    //        mailBody.AppendFormat("<br />");
    //        mailBody.AppendFormat(messBody4);
    //        mailBody.AppendFormat("<br />");
    //        string Mailbody = mailBody.ToString();
    //        string nMailbody = EmailTemplate.Replace("#content", Mailbody);
    //        msg.IsBodyHtml = true;
    //        msg.Body = nMailbody;

    //        smtp.Host = "smtp.gmail.com";

    //        smtp.Port = 587;
    //        smtp.UseDefaultCredentials = true;
    //        smtp.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);
    //        smtp.EnableSsl = true;
    //        smtp.Send(msg);

    //    }
    //    catch (Exception ex)
    //    {
    //        if (Convert.ToBoolean(Session["error"]) == true)
    //            objCommon.ShowError(Page, "DISPATCH_Transactions_IO_InwardDispatch.TransferToEmail-> " + ex.Message + " " + ex.StackTrace);
    //        else
    //            objCommon.ShowError(Page, "Server UnAvailable");
    //    }
    //}

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
        //string returnpageurl = Convert.ToString(Session["ReturnpageUrl"]);
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
        string ptype = (objCommon.LookUp("ACD_STUDENT A INNER JOIN ACD_PAYMENTTYPE P ON (A.PTYPE=P.PAYTYPENO) ", "PAYTYPENAME", "IDNO=" + Session["idno"].ToString()));
        if (ptype == "Provisional" && Session["OrgId"].ToString() == "5")
        {
            //ShowReport("InstallmentOnlineFeePayment", "rptOnlineReceiptforprovisionaladm.rpt", Convert.ToInt32(DcrNo), Convert.ToInt32(Session["stuinfoidno"]));

            ShowReport("OnlineFeePayment", "rptOnlineReceiptforprovisionaladm.rpt");
            return;
        }
        else
        {
            //ShowReport("OnlineFeePayment", "rptOnlineReceipt.rpt");
            ShowReport("OnlineFeePayment", "rptOnlineReceipt_New.rpt");
        }
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

    protected void UPDATE_USER(string UA_NO,int FirstTimePay)
        {
        try
            {
            string UA_PWD = string.Empty;
            string password = string.Empty;
            //string student_Name = "ROHIT";
            int IDNO = 0;
            string REGNO = string.Empty;
            string Email = string.Empty;
            string UA_ACC = string.Empty;
            if (Convert.ToInt32(Session["OrgId"].ToString()) == 18)
                {
                    IDNO = Convert.ToInt32(ViewState["IDNO"]);
                    REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO = '" + ViewState["IDNO"] + "'");
                    UA_PWD = clsTripleLvlEncyrpt.ThreeLevelEncrypt(REGNO.ToString());
               
                }
            else
                {
                  IDNO = Convert.ToInt32(ViewState["IDNO"]);
                  UA_PWD = clsTripleLvlEncyrpt.ThreeLevelEncrypt(IDNO.ToString());
                  REGNO = IDNO.ToString();
                }

            CustomStatus CS = (CustomStatus)feeController.UpdateUser(REGNO, UA_PWD, IDNO, FirstTimePay);

            }
        catch (Exception ex)
            {
            throw;
            }
        }
     private DataSet getModuleConfig()
        {
        DataSet ds = objCommon.GetModuleConfig(Convert.ToInt32(Session["OrgId"]));
        return ds;
        }
    public void Sendmail()
        {
        string email_type = string.Empty;
        string Link = string.Empty;
        int sendmail = 0;
        string subject = string.Empty;
        string srnno = string.Empty;
        string pwd = string.Empty;
        int status = 0;
        string IDNO = Session["IDNO"].ToString();
        string MISLink = objCommon.LookUp("ACD_MODULE_CONFIG", "ONLINE_ADM_LINK", "OrganizationId=" + Session["OrgId"]);
        string Username = string.Empty;
        string Password = string.Empty;
        string Name = objCommon.LookUp("ACD_STUDENT", "STUDNAME", "IDNO=" + Convert.ToInt32(ViewState["IDNO"]));
        string Branchname = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_DEGREE D ON (S.DEGREENO=D.DEGREENO) INNER JOIN ACD_BRANCH B ON (B.BRANCHNO=S.BRANCHNO)", "CONCAT(D.DEGREENAME, ' in ',B.LONGNAME)", "IDNO=" + ViewState["IDNO"].ToString());
        string REGNO = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + Convert.ToInt32(ViewState["IDNO"]));
        string COLLEGE_CODE = objCommon.LookUp("REFF", "CODE_STANDARD", "");
        string EmailID = objCommon.LookUp("ACD_STUDENT", "EMAILID", "IDNO=" + Convert.ToInt32(ViewState["IDNO"]));
        string college = objCommon.LookUp("ACD_STUDENT S INNER JOIN ACD_COLLEGE_MASTER M ON(S.COLLEGE_ID=M.COLLEGE_ID)", "M.COLLEGE_NAME", "IDNO=" + Convert.ToInt32(ViewState["IDNO"]));
        Username = REGNO;
        Password = REGNO;

        DataSet ds = getModuleConfig();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
            email_type = ds.Tables[0].Rows[0]["EMAIL_TYPE"].ToString();
            Link = ds.Tables[0].Rows[0]["LINK"].ToString();
            sendmail = Convert.ToInt32(ds.Tables[0].Rows[0]["THIRDPARTY_PAYLINK_MAIL_SEND"].ToString());

            if (sendmail == 1)
                {
                subject = "New MIS Login Credentials";           
                string message = "";
                message += "<p>Dear :<b>" + Name + "</b> </p>";
                message += "<p><b>" + Branchname + "</b></p>";
                message += "<p>Your fees have been submitted successfully and you have been registered for the program mentioned above.Your new Login credentials are as follows</p><p>" + MISLink + " </p><p>Username   : " + Username + " <br/>Password   : " + Password + "</p>";
                message +="<p>Note for Provisional Registration only:</p>";
                message += "<p>All the documents must be uploaded on URL: <b>" + MISLink + "</b>";
                message += "<p>Process of fee payment: Login using above credentials in <b>" + MISLink + "</b> Academic Menu-->>Student Related-->>Online Payment.: ";
                message += "<p>The fee payment should be made within 7 days of receiving this mail/letter, after which your claim for admission may be requested.</p>";
                message += "<p style=font-weight:bold;>Thanks<br>Team Admissions<br>" + COLLEGE_CODE + " University<br></p>";

                status = objSendEmail.SendEmail(EmailID, message, subject); //Calling Method
                }
            }

        if (status == 1)
            {
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "functionConfirm", "confirmmsg();", true);
            }
        else
            {
            objCommon.DisplayMessage(this.Page, "Failed to send mail.", this.Page);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "functionConfirm", "confirmmsg();", true);
            } 

        }

    
    protected void btnredirect_Click(object sender, EventArgs e)
        {
        //Response.Redirect("FeeCollectionOptions.aspx");
        }


    //New Code IOBPAY  - 23/08/2023
    public class EncryptHelper
    {
        public RijndaelManaged GetRijndaelManaged(byte[] secretKey, String iv)
        {
            //  var keyBytes = new byte[32];
            //   var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
            //   Array.Copy(secretKeyBytes, keyBytes, Math.Min(keyBytes.Length, secretKeyBytes.Length));
            return new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = 256,
                BlockSize = 128,
                Key = secretKey,
                IV = Encoding.UTF8.GetBytes(iv)
            };
        }

        public byte[] Encrypt(byte[] plainBytes, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateEncryptor()
                .TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        }

        public byte[] Decrypt(byte[] encryptedData, RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateDecryptor()
                .TransformFinalBlock(encryptedData, 0, encryptedData.Length);
        }

        public String Encrypt(String plainText, String key, String iv)
        {
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var keybytes = getHashSha256(key);
            return Convert.ToBase64String(Encrypt(plainBytes, GetRijndaelManaged(keybytes, iv)));
        }

        public String Decrypt(String encryptedText, String key, String iv)
        {
            var encryptedBytes = Convert.FromBase64String(encryptedText);
            var keybytes = getHashSha256(key);
            return Encoding.UTF8.GetString(Decrypt(encryptedBytes, GetRijndaelManaged(keybytes, iv)));
        }

        public byte[] getHashSha256(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            return hash;
        }

        public byte[] HashHMAC(byte[] key, byte[] message)
        {
            var hash = new HMACSHA256(key);
            return hash.ComputeHash(message);

        }
    }

    //end




}