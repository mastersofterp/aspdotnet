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
using paytm;
using Paytm;
using Newtonsoft.Json;
using System.IO;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG;

public partial class PaytmOnlinePaymentResponse : System.Web.UI.Page
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

    //new
    string order_id = string.Empty;
    string amount = string.Empty;
    string firstname = string.Empty;
    string emailId = string.Empty;
    string Idno = string.Empty;
    string recipt = string.Empty;
    string saltkey = string.Empty;
    int payid = 0;
    int orgid = 0;
 
    string Regno = string.Empty;
   
    string transactionId = string.Empty;
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
    string timeStamp = string.Empty;
    string studIdno = string.Empty;
    string studName = string.Empty;
    string extra = string.Empty;
    int payactivityno = 0;
    int installmentno = 0;




    #endregion

    FeeCollectionController feeController = new FeeCollectionController();
    SendEmailCommon objSendEmail = new SendEmailCommon(); //Object Creation
    OrganizationController objOrg = new OrganizationController();

    string UserFirstPaymentStatus = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {

                DataSet Orgds = null;
                int Ord_Id = Convert.ToInt32(Session["OrgId"]);
                Orgds = objOrg.GetOrganizationById(Ord_Id);
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


                //SqlDataReader dr = objCommon.GetCommonDetails();
                //if (dr != null)
                //{
                //    if (dr.Read())
                //    {
                //        //lblCollege.Text = dr["COLLEGENAME"].ToString();
                //        //lblAddress.Text = dr["College_Address"].ToString();
                //        Session["OrgId"] = dr["OrganizationId"].ToString();
                //        //imgCollegeLogo.ImageUrl = "~/showimage.aspx?id=0&type=college";
                //    }
                //}

                // get fetch paytm response
                FetchPaytmResponse();

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }

    #region Paytm Response
    public void FetchPaytmResponse()
    {
        try
        {
           // Replace the with the Merchant Key provided by Paytm at the time of registration.
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string Order_ID = "";
            string paytmChecksum = "";
            foreach (string key in Request.Form.Keys)
            {
                parameters.Add(key.Trim(), Request.Form[key].Trim());
            }

            if (parameters.ContainsKey("CHECKSUMHASH"))
            {
                Order_ID = parameters["ORDERID"];
                paytmChecksum = parameters["CHECKSUMHASH"];
                parameters.Remove("CHECKSUMHASH");
            }

            if (parameters.ContainsKey("ORDERID"))
            {
                Order_ID = parameters["ORDERID"];
            }

            string merchantKey = objCommon.LookUp("ACD_DCR_TEMP", "MID", "ORDER_ID='" + Order_ID + "'  ");

            if (CheckSum.verifyCheckSum(merchantKey, parameters, paytmChecksum))
            {
                lblOrderid.Text = parameters["ORDERID"].ToString();
                lblAmount.Text = parameters["TXNAMOUNT"].ToString();
               // lblPStatus.Text = parameters["STATUS"].ToString();
                //lblTxntype.Text = "Online";
                //lblGateway.Text = parameters["GATEWAYNAME"].ToString();
                //lblResCode.Text = parameters["RESPCODE"].ToString();
                //lblRespmsg.Text = parameters["STATUS"].ToString();
                //lblBankid.Text = parameters["BANKTXNID"].ToString();
                //lblPayMode.Text = parameters["PAYMENTMODE"].ToString();
                lblTransactionDate.Text = parameters["TXNDATE"].ToString();
                lblTrasactionId.Text = parameters["TXNID"].ToString();

                timeStamp = parameters["TXNDATE"].ToString();
                txnStatus = parameters["STATUS"].ToString();
                order_id = parameters["ORDERID"].ToString();
                totalAmount = parameters["TXNAMOUNT"].ToString();
                var TxnStatus = parameters["RESPCODE"].ToString();
               
                ViewState["order_id"] = parameters["ORDERID"].ToString();
                var Idno = objCommon.LookUp("ACD_DCR_TEMP", "IDNO", "ORDER_ID='" + Order_ID + "'");
                 
                ViewState["IDNO"] = Idno;
                Session["order_id"] = order_id.ToString();
                Session["idno"] = Idno;

                string Regno = objCommon.LookUp("ACD_STUDENT", "REGNO", "IDNO=" + ViewState["IDNO"].ToString());
                string BRANCHNAME = objCommon.LookUp("ACD_STUDENT A INNER JOIN ACD_BRANCH B ON (A.BRANCHNO=B.BRANCHNO)", "B.LONGNAME", "A.IDNO=" + ViewState["IDNO"].ToString());
                string semesterNo = objCommon.LookUp("ACD_STUDENT", "SEMESTERNO", "IDNO=" + ViewState["IDNO"].ToString());
                string Name = objCommon.LookUp("ACD_STUDENT", "STUDNAME", "IDNO=" + ViewState["IDNO"].ToString());
                lblRegNo.Text = Regno;
                lblstudentname.Text = Name;
                lblBranch.Text = BRANCHNAME;
                lblSemester.Text = semesterNo;
                transactionId =   parameters["TXNID"].ToString();
                trackID = parameters["TXNID"].ToString();

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

                if (TxnStatus == "01")     //Payment response in - (SUCCESS, FAILURE, AWAITED)
                {
                    divSuccess.Visible = true;
                    divFailure.Visible = false;

                    string UA_IDNO = objCommon.LookUp("USER_ACC", "UA_IDNO", "UA_No = '" + Session["userno"] + "'");
                    string UA_NAME = objCommon.LookUp("USER_ACC", "UA_NAME", "UA_IDNO = '" + Convert.ToInt32(UA_IDNO) + "'");

                    int result = 0;
                    string PaymentFor = string.Empty, txnMessage = string.Empty, BankReferenceNo = string.Empty;
                    string rec_code = objCommon.LookUp("ACD_DCR_TEMP", "RECIEPT_CODE", "ORDER_ID = '" + order_id + "'");
                    feeType = rec_code;
                    objsem.IdNo = Convert.ToInt32(Session["idno"]);
                    objsem.SESSIONNO = Convert.ToInt32(Session["currentsession"].ToString());
                    objsem.SemesterNO = Convert.ToInt32(semesterNo);  //
                    objsem.paymentMode = 1;
                    objsem.OfflineMode = 0;
                    objsem.Total_Amt = Convert.ToDecimal(totalAmount);   // lblamount.Text

                    objsem.IPADDRESS = Request.ServerVariables["REMOTE_HOST"];
                    objsem.Date_of_Payment = DateTime.Now.ToString("dd/MM/yyyy");
                    int output = 0;

                    if (Convert.ToInt32(installmentno) > 0)
                    {
                        output = objFees.InsertInstallmentOnlinePayment_DCR(Idno, rec_code, order_id, transactionId, "O", "1", amount, "Success", Convert.ToInt32(installmentno), "-");
                    }
                    else
                    {
                        output = objFees.InsertOnlinePayment_DCR(Idno, rec_code, order_id, transactionId, "O", "1", amount, "Success", Regno, "-");
                    }

                    output = objFees.UpdatePAYTMOnlinePaymentlog(Convert.ToInt32(Idno), order_id, tokenID, trackID, feeType, txnStatus, timeStamp);     //trackID =  transactionId

                    btnPrint.Visible = true;
                }
                if (TxnStatus != "01")   // FAILURE
                {
                    divSuccess.Visible = false;
                    divFailure.Visible = true;
                    int result = 0;
                    order_id = Session["order_id"].ToString();

                    string PaymentFor = string.Empty, txnMessage = string.Empty, BankReferenceNo = string.Empty;
                    txnMessage = "";
                    string rec_code = objCommon.LookUp("ACD_DCR_TEMP", "RECIEPT_CODE", "ORDER_ID = '" + order_id + "'");
                    feeType = rec_code;
                    objFees.InsertOnlinePaymentlog(Idno, rec_code, "O", amount, "Payment Fail", order_id);

                    //result = objFees.OnlineInstallmentFeesPayment(mihpayid, order_id, amount, "0000", "", PaymentFor, txnMessage, BankReferenceNo, PaymentFor, rec_code);
                    result = objFees.UpdatePAYTMOnlinePaymentlog(Convert.ToInt32(Idno), order_id, tokenID, transactionId, feeType, txnStatus, timeStamp);
                    btnPrint.Visible = false;
                }
                else
                {
                    objFees.UpdatePAYTMOnlinePaymentlog(Convert.ToInt32(Idno), order_id, tokenID, transactionId, feeType, txnStatus, timeStamp);
                }

            }
        }
        catch(Exception ex)
        {
        }
    }
    #endregion


    #region Reciept & Print code methods
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
            ShowReport("OnlineFeePayment", "rptOnlineReceipt.rpt");
        }
    }

    private void ShowReport(string reportTitle, string rptFileName)
    {
        try
        {
            int IDNO = Convert.ToInt32(Session["idno"]);

            string DcrNo = objCommon.LookUp("ACD_DCR", "DCR_NO", "IDNO='" + Session["idno"].ToString() + "' AND ORDER_ID ='" + Convert.ToString(Session["order_id"]) + "'");

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            url += "&param=@P_COLLEGE_CODE=" + Convert.ToInt32(Session["colcode"]) + ",@P_IDNO=" + IDNO + ",@P_DCRNO=" + Convert.ToInt32(DcrNo);

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
        //string returnpageurl = Convert.ToString(Session["ReturnpageUrl"]);
        //Response.Redirect(returnpageurl);
        Response.Redirect("~/default.aspx");
    }
    #endregion


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


    #region Check Transaction Status
    protected void TransactionStatus() 
    {
        Dictionary<string, string> body = new Dictionary<string, string>();
        Dictionary<string, string> head = new Dictionary<string, string>();
        Dictionary<string, Dictionary<string, string>> requestBody = new Dictionary<string, Dictionary<string, string>>();

        body.Add("mid", "YOUR_MID_HERE");
        body.Add("orderId", "ORDERID_98765");

        /*
        * Generate checksum by parameters we have in body
        * Find your Merchant Key in your Paytm Dashboard at https://dashboard.paytm.com/next/apikeys 
        */
        string paytmChecksum = Checksum.generateSignature(JsonConvert.SerializeObject(body), "YOUR_KEY_HERE");

        head.Add("signature", paytmChecksum);

        requestBody.Add("body", body);
        requestBody.Add("head", head);

        string post_data = JsonConvert.SerializeObject(requestBody);

        //For  Staging
        string url = "https://securegw-stage.paytm.in/v3/order/status";

        //For  Production 
        //string  url  =  "https://securegw.paytm.in/v3/order/status";

        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

        webRequest.Method = "POST";
        webRequest.ContentType = "application/json";
        webRequest.ContentLength = post_data.Length;

        using (StreamWriter requestWriter = new StreamWriter(webRequest.GetRequestStream()))
        {
            requestWriter.Write(post_data);
        }

        string responseData = string.Empty;

        using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
        {
            responseData = responseReader.ReadToEnd();
            Console.WriteLine(responseData);
        }

    }
    #endregion

}