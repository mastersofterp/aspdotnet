//======================================================================================
// PROJECT NAME  : RFCAMPUS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : PHOTOCOPY AND REVAULTION REGISTRATION RESPONSE BY STUDENT AND ADMIN END                                   
// CREATION DATE : 20-08-2022
// ADDED BY      : SACHIN A
// ADDED DATE    : 
// MODIFIED BY   : 
// MODIFIED DESC :                                                    
//======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Linq;
using System.Net;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;
using System.Net.NetworkInformation;

public partial class PhotoReval_Response : System.Web.UI.Page
{
    #region Variable Declaration

    string
          strPG_TxnStatus = string.Empty,
          strPG_TPSLTxnBankCode = string.Empty,
          strPG_TxnDateTime = string.Empty,
          strPG_TxnDate = string.Empty,
          strPG_TxnTime = string.Empty,
          strPG_TxnType = string.Empty;
    //string strPGResponse;
    //string[] strSplitDecryptedResponse;
    //string[] strArrPG_TxnDateTime;
    //string strPG_MerchantCode;
    #endregion

    Common objCommon = new Common();
    FeeCollectionController feeController = new FeeCollectionController();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                //btnReports.Visible = true;
                //btnRegistrationSlip.Visible = true;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                con.Open();
                string res = "PHOTORES";
                SqlCommand cmd = new SqlCommand("Insert into Test (Name,Response) values ('" + Convert.ToString(HttpContext.Current.Request["msg"]) + "','" + res + "')", con);
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                con.Close();

                string[] responseArrary = Convert.ToString(HttpContext.Current.Request["msg"]).Split('|');

                 //string[] responseArrary = Convert.ToString("SVCE2|273857134|SHMP8655201768|218097|1.00|HMP|510372|03|INR|MDDIRECT|02-NA|NA|00000000.00|26-03-2020 12:25:03|0300|NA|ADITHYA  R|2738|CF|CF170501003,57,5,9486246104|1|NA|57|NA|PGS10001-Success|CE80567379DBFBED8442ECF5FD7B86D07381D49023A0D0927883DCA879638D69").Split('|'); 
                //string[] responseArrary = Convert.ToString("SVCE2|27385790953|SPMP8654983858|NA|1.00|PMP|607093|03|INR|RDDIRECT|NA|NA|0.00|26-03-2020 11:23:01|0399|NA|ADITHYA  R|2738|CF|CF170501003,57,5,9486246104|1|NA|57|NA|PME10014-Connection timedout|ACBD05C74D7E8E6EB4F8D905C8AC549E4B85165F80F8DA7966FAEF2EFD1B62D3
                //string[] responseArrary = Convert.ToString("SVCE2|172057403|SHMP8634441559|993142|1.00|HMP|510372|03|INR|MDDIRECT|02-NA|NA|00000000.00|19-03-2020 10:46:48|0300|NA|BALACHANDAR M|1662|AEF|AEF180301010,58,3,9444208734|1|NA|58|NA|PGS10001-Success|gpOT4FkIAejC").Split('|');
            
                CheckResponseFromBillDesk(responseArrary);
            }
        }
        catch { }
    }

    // ----  BILl Desk Payment Response 
    public void CheckResponseFromBillDesk(string[] BDResponse)
    {
        #region Declaration for Response processing
        //string urlPath = ConfigurationManager.AppSettings["PaymentPath"];
        string ErrorMessage = string.Empty;
        #endregion

        #region BillDesk Response Data
        string MerchantID = BDResponse[0].Replace('+', ' ');
        string UniTranNo = BDResponse[1].Replace('+', ' ');
        string TxnReferenceNo = BDResponse[2].Replace('+', ' ');
        string BankReferenceNo = BDResponse[3].Replace('+', ' ');
        string txn_amount = BDResponse[4].Replace('+', ' ');
        string BankID = BDResponse[5].Replace('+', ' ');
        string BankMerchantID = BDResponse[6].Replace('+', ' ');
        string TxnType = BDResponse[7].Replace('+', ' ');
        string CurrencyType = BDResponse[8].Replace('+', ' ');
        string ItemCode = BDResponse[9].Replace('+', ' ');
        string SecurityType = BDResponse[10].Replace('+', ' ');
        string SecurityID = BDResponse[11].Replace('+', ' ');
        string SecurityPasswod = BDResponse[12].Replace('+', ' ');
        string TxnDate = BDResponse[13].Replace('+', ' ');
        string AuthStatus = BDResponse[14].Replace('+', ' ');
        string SettlementType = BDResponse[15].Replace('+', ' ');
        string additional_info1 = BDResponse[16].Replace('+', ' ');
        string additional_info2 = BDResponse[17].Replace('+', ' ');
        string additional_info3 = BDResponse[18].Replace('+', ' ');



        string additional_info4 = BDResponse[19].Replace('+', ' ');
        string additional_info5 = BDResponse[20].Replace('+', ' ');
        string additional_info6 = BDResponse[21].Replace('+', ' ');
        string additional_info7 = BDResponse[22].Replace('+', ' ');
        string ErrorStatus = BDResponse[23].Replace('+', ' ');
        string errorDescription = BDResponse[24].Replace('+', ' ');
        String Checksum = BDResponse[25].Replace('+', ' ');
        //string cc="94667B7E9D0A4F0329B5BAA119D9F4E0126635AC4725C58B5671C0FE4E03925D";
        #endregion

        #region Generate Bill Desk Check Sum
        StringBuilder billRequest = new StringBuilder();
        billRequest.Append(MerchantID).Append("|");
        billRequest.Append(UniTranNo).Append("|");
        billRequest.Append(TxnReferenceNo).Append("|");
        billRequest.Append(BankReferenceNo).Append("|");
        billRequest.Append(txn_amount).Append("|");
        billRequest.Append(BankID).Append("|");
        billRequest.Append(BankMerchantID).Append("|");
        billRequest.Append(TxnType).Append("|");
        billRequest.Append(CurrencyType).Append("|");
        billRequest.Append(ItemCode).Append("|");
        billRequest.Append(SecurityType).Append("|");
        billRequest.Append(SecurityID).Append("|");
        billRequest.Append(SecurityPasswod).Append("|");
        billRequest.Append(TxnDate).Append("|");
        billRequest.Append(AuthStatus).Append("|");
        billRequest.Append(SettlementType).Append("|");
        billRequest.Append(additional_info1).Append("|");
        billRequest.Append(additional_info2).Append("|");
        billRequest.Append(additional_info3).Append("|");
        billRequest.Append(additional_info4).Append("|");
        billRequest.Append(additional_info5).Append("|");
        billRequest.Append(additional_info6).Append("|");
        billRequest.Append(additional_info7).Append("|");
        billRequest.Append(ErrorStatus).Append("|");
        billRequest.Append(errorDescription);

        string data = billRequest.ToString();
        lblmessage.Text = data;
        ViewState["Orderid"] = UniTranNo;
        ViewState["IDNO"] = additional_info5;           //additional_info2;
       
        string ChecksumKey = "sNPAFD72PKSWXYICHM2SyYgaQ7pF1Xf4";                      // Session["CHECKSUM_KEY"].ToString();               //"gpOT4FkIAejC";        //Added checksum key on 21082022
        String hash = String.Empty;
        hash = GetHMACSHA256(data, ChecksumKey);
        hash = hash.ToUpper();

        #endregion

        #region Payment Transaction Update

        string txnMessage = string.Empty;
        string txnStatus = string.Empty;
        string txnMode = string.Empty;


        //SqlConnection con4 = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
        //if (con4.State == ConnectionState.Open)
        //{
        //    con4.Close();
        //}
        //con4.Open();
        //string res4 = "OHash";
        //SqlCommand cmd4 = new SqlCommand("Insert into Test (Name,Response) values ('" + Convert.ToString(hash) + "','" + res4 + "')", con4);
        //cmd4.Connection = con4;
        //cmd4.ExecuteNonQuery();
        //con4.Close();


        //SqlConnection con5 = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
        //if (con5.State == ConnectionState.Open)
        //{
        //    con5.Close();
        //}
        //con5.Open();
        //string res5 = "OChecksum";
        //SqlCommand cmd5 = new SqlCommand("Insert into Test (Name,Response) values ('" + Convert.ToString(Checksum) + "','" + res5 + "')", con5);
        //cmd5.Connection = con5;
        //cmd5.ExecuteNonQuery();
        //con5.Close();


        //SqlConnection con6 = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
        //if (con6.State == ConnectionState.Open)
        //{
        //    con6.Close();
        //}
        //con6.Open();
        //string res6 = "OLabelValue";
        //SqlCommand cmd6 = new SqlCommand("Insert into Test (Name,Response) values ('" + Convert.ToString(lblmessage.Text) + "','" + res6 + "')", con6);
        //cmd6.Connection = con6;
        //cmd6.ExecuteNonQuery();
        //con6.Close();


        if (hash == Checksum)//if (hash == cc)
        {
            #region Get Transaction Details
            if (AuthStatus == "0300")
            {
                txnMessage = "Successful Transaction";
                txnStatus = "Success";
            }
            else if (AuthStatus == "0399")
            {
                txnMessage = "Cancel Transaction";
                txnStatus = "Invalid Authentication at Bank";
            }
            else if (AuthStatus == "NA")
            {
                txnMessage = "Cancel Transaction";
                txnStatus = "Invalid Input in the Request Message";
            }
            else if (AuthStatus == "0002")
            {
                txnMessage = "Cancel Transaction";
                txnStatus = "BillDesk is waiting for Response from Bank";
            }
            else if (AuthStatus == "0001")
            {
                txnMessage = "Cancel Transaction";
                txnStatus = "Error at BillDesk";
            }
            else
            {
                txnMessage = "Something went wrong. Try Again!.";
                txnStatus = "Payment Faild";
            }
            #endregion

            #region Transaction Type
            if (TxnType == "01")
                txnMode = "Netbanking";
            else if (TxnType == "02")
                txnMode = "Credit Card";
            else if (TxnType == "03")
                txnMode = "Debit Card";
            else if (TxnType == "04")
                txnMode = "Cash Card";
            else if (TxnType == "05")
                txnMode = "Mobile Wallet";
            else if (TxnType == "06")
                txnMode = "IMPS";
            else if (TxnType == "07")
                txnMode = "Reward Points";
            else if (TxnType == "08")
                txnMode = "Rupay";
            #endregion

            #region Assign Values to objEntity
            string TxRefNo = TxnReferenceNo;
            string PgTxnNo = BankReferenceNo;
            decimal TxnAmount = Convert.ToDecimal(txn_amount);
            string TxStatus = txnStatus;
            string TxMssg = txnMessage;
            string TransactionType = "Online";
            string PaymentFor = TxnType;
            #endregion


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            string res = "R";
            SqlCommand cmd = new SqlCommand("Insert into Test (Name,Response) values ('" + lblmessage.Text.ToString() + "','" + res + "')", con);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();

            if (txnStatus == "Success")
            {
                Session["ResponseMsg"] = "Payment success.";

                if (ViewState["Orderid"] != null)
                {
                    int result = 0;
                   string rec_code = objCommon.LookUp("ACD_DCR_temp", "RECIEPT_CODE", "ORDER_ID = '" + ViewState["Orderid"] +"'");
                   // string rec_code = objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "ORDER_ID = '" + Session["Order_Id"] + "'");

                    if (rec_code == "PRF")
                    { 
                        result = feeController.OnlinePhotoRevalPayment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 1); //1 for photo copy
                    }
                    else if (rec_code == "RF")//"RF"
                    {
                        result = feeController.OnlinePhotoRevalPayment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 2); //2 for reval
                    }
                    else if (rec_code == "AEF")//"AEF"
                    {
                        result = feeController.OnlinePhotoRevalPayment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 3); //3 for arrear exam
                    }
                    else if (rec_code == "CF")//"CF"
                    {
                        result = feeController.OnlinePhotoRevalPayment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 4); //4 for Condonation
                    }
                    else if (rec_code == "REF")//"REF"
                    {
                        result = feeController.OnlinePhotoRevalPayment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 5); //5 for review on 06052022
                    }


                    btnReports.Visible = true;
                    btnRegistrationSlip.Visible = true;

                    lblmessage.Visible = true;
                    lblmessage.ForeColor = System.Drawing.Color.Green; lblmessage.Font.Bold = true;
                    lblmessage.Text = "Hello " + additional_info1 + " ! <br /> We have processed Payment of  Rs." + txn_amount + " successfully. <br/> <br/> Transaction ID : " + BankReferenceNo + ".<br/> Thank You";
                    // SuccessMessage = "Hello " + udf1 + " ! , We have processed Payment of  Rs." + Amount + " successfully. Transaction ID : " + bank_txn + ". Thank You";
                    lbtnGoBack.Visible = true;
                }
               
            }
            else
            {
                Session["ResponseMsg"] = "Please try again.";
                if (ViewState["Orderid"] != null)
                {
                    int result = 0;
                    string rec_code = objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "ORDER_ID = '" + ViewState["Orderid"] + "'");

                    if (rec_code == "PRF")
                    {
                        result = feeController.OnlinePhotoRevalPayment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 1);  //1 for photo copy
                    }
                    else if (rec_code == "RF")//"RF"
                    {
                        result = feeController.OnlinePhotoRevalPayment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 2);  //2 for reval
                    }
                    else if (rec_code == "AEF")//"AEF"
                    {
                        result = feeController.OnlinePhotoRevalPayment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 3);  //3 for arrear
                    }
                    else if (rec_code == "CF")//"CF"
                    {
                        result = feeController.OnlinePhotoRevalPayment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 4);  //4 for Condonation
                    }
                    else if (rec_code == "REF")//"REF"
                    {
                        result = feeController.OnlinePhotoRevalPayment(TxnReferenceNo, UniTranNo, txn_amount, AuthStatus, txnStatus, PaymentFor, txnMessage, BankReferenceNo, PaymentFor, 5);  //5 for REVIEW  on 06052022
                    }

                    //btnReports.Visible = true;
                    //btnRegistrationSlip.Visible = true;
                    btnReports.Visible = false;
                    btnRegistrationSlip.Visible = false;

                    lblmessage.Visible = true;
                    lblmessage.ForeColor = System.Drawing.Color.Red; lblmessage.Font.Bold = true;
                    lblmessage.Text = "Hello " + additional_info1 + " ! <br /> We are Unable To Process Payment of  Rs." + txn_amount + ".<br/> <br/>  Due to Reason : ' NA'.  <br/> <br/> Transaction ID : " + BankReferenceNo + ".<br/> Thank You";
                    lbtnGoBack.Visible = true;
                    //try
                    //{
                    //    if (AuthStatus == "0002")
                    //    {
                    //        doubleVerification();
                    //    }
                    //    else if (txnMessage == "Cancel Transaction")
                    //    {
                    //        doubleVerification();
                    //    }
                    //}
                    //catch { }
                }
            }
        }
        else
        {
            //txnMessage = "Something went wrong. Try Again!.";
            //txnStatus = "Payment Faild";
            //Session["ResponseMsg"] = "Something went wrong. Please try again.";
            lblmessage.Visible = true;
            lblmessage.ForeColor = System.Drawing.Color.Red; lblmessage.Font.Bold = true;
            lblmessage.Text = "Something went wrong. Try Again!.";
        }

        #endregion
    }
    
    public void doubleVerification()
    {
        try
        {
            String data = "0122|SVCE2" + "|" + ViewState["Orderid"] + "|" + DateTime.Now.ToString("yyyyMMddhhmmss");
            //string data = "0122|SVCE2|" + ViewState["Orderid"] + "|SHMP8634441559|993142|1.00|HMP|510372|03|INR|MDDIRECT|02-NA|NA|00000000.00|19-03-2020 10:46:48|0300|NA|BALACHANDAR M|1662|AEF|AEF180301010,58,3,9444208734|1|NA|58|NA|PGS10001-Success|gpOT4FkIAejC";
            string ChecksumKey = "gpOT4FkIAejC";
            String hash = String.Empty;
            hash = GetHMACSHA256(data, ChecksumKey);
            hash = hash.ToUpper();
            string msg = data + "|" + hash;

            //TO ENABLE SECURE CONNECTION SSL/TLS
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; 

            var request = (HttpWebRequest)WebRequest.Create("https://www.billdesk.com/pgidsk/PGIQueryController?msg=" + msg);
            var response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string strResponse = reader.ReadToEnd();
            //lblNote1.Text = strResponse;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["UAIMS"].ToString());
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            string res = "RD";
            SqlCommand cmd = new SqlCommand("Insert into Test (Name,Response) values ('" + strResponse.ToString() + "','" + res + "')", con);
            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
            //lblNote1.Text = string.Empty;
            string[] repoarray;
            repoarray = strResponse.Split('|');

            string authstatus = repoarray[15].ToString();
            string txnid1 = repoarray[2].ToString();
            string amount1 = repoarray[5].ToString();
            string apitransid = repoarray[3].ToString();
            string BankReferenceNo = repoarray[4].ToString();
            string TxnType = repoarray[8].ToString();
            string receipt = repoarray[19].ToString();
            //string status_msg = repoarray[25].ToString();

            string rec_code = objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "ORDER_ID = '" + ViewState["Orderid"]+"'");
            //string status = repoarray[15].ToString();
            //string status_msg = repoarray[25].ToString();
            //string msgR = repoarray[32].ToString();
            string txnMessage = string.Empty;
            string txnStatus = string.Empty;
            #region Get Transaction Details
            if (authstatus == "0300")
            {
                txnMessage = "Successful Transaction";
                txnStatus = "Success";
            }
            else if (authstatus == "0399")
            {
                txnMessage = "Cancel Transaction";
                txnStatus = "Invalid Authentication at Bank";
            }
            else if (authstatus == "NA")
            {
                txnMessage = "Cancel Transaction";
                txnStatus = "Invalid Input in the Request Message";
            }
            else if (authstatus == "0002")
            {
                txnMessage = "Cancel Transaction";
                txnStatus = "BillDesk is waiting for Response from Bank";
            }
            else if (authstatus == "0001")
            {
                txnMessage = "Cancel Transaction";
                txnStatus = "Error at BillDesk";
            }
            else
            {
                txnMessage = "Something went wrong. Try Again!.";
                txnStatus = "Payment Faild";
            }
            #endregion

            FeeCollectionController objFeesCnt = new FeeCollectionController();
            if (authstatus == "0300")
            {
                int retval = 0;
                if (rec_code == "PRF")
                {
                    retval = objFeesCnt.OnlinePaymentUpdation(apitransid, txnid1, amount1, authstatus, txnStatus, TxnType, txnMessage, BankReferenceNo, 1);

                }
                else if (rec_code == "RF")
                {
                    retval = objFeesCnt.OnlinePaymentUpdation(apitransid, txnid1, amount1, authstatus, txnStatus, TxnType, txnMessage, BankReferenceNo, 2);

                }
                else if (rec_code == "AEF")
                {
                    retval = objFeesCnt.OnlinePaymentUpdation(apitransid, txnid1, amount1, authstatus, txnStatus, TxnType, txnMessage, BankReferenceNo, 3);

                }
                else if (rec_code == "CF")
                {
                    retval = objFeesCnt.OnlinePaymentUpdation(apitransid, txnid1, amount1, authstatus, txnStatus, TxnType, txnMessage, BankReferenceNo, 4);

                }
                else if (rec_code == "REF")//ADDED BY PRAFULL MUKE ON DATE 07052022
                {
                    retval = objFeesCnt.OnlinePaymentUpdation(apitransid, txnid1, amount1, authstatus, txnStatus, TxnType, txnMessage, BankReferenceNo, 6);
                }

                //if (retval == -99)
                //{
                //    objCommon.DisplayMessage(this, "Error occured", this.Page);
                //}
            }
        }
        catch { }
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

    protected void lbtnGoBack_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Orderid"] != null)
            {
                InitializeSession();
                string rec_code = objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "ORDER_ID = '" + ViewState["Orderid"] + "'");
                if (rec_code == "PRF")
                {
                    int pageno = Convert.ToInt32(objCommon.LookUp("ACCESS_LINK", "AL_NO", "AL_URL = 'ACADEMIC/PhotoCopyRegistration.aspx'"));
                    Response.Redirect("PhotoCopyRegistration.aspx?pageno=" + pageno + "");//2480 in test
                }
                else if (rec_code == "RF")
                {
                    int pageno = Convert.ToInt32(objCommon.LookUp("ACCESS_LINK", "AL_NO", "AL_URL = 'ACADEMIC/RevaluationRegistration.aspx' AND ACTIVE_STATUS=1"));
                    Response.Redirect("RevaluationRegistration.aspx?pageno=" + pageno + "");//1776 in test
                }
                else if (rec_code == "AEF")
                {
                    int pageno = Convert.ToInt32(objCommon.LookUp("ACCESS_LINK", "AL_NO", "AL_URL = 'ACADEMIC/EXAMINATION/BacklogExamregEndSem.aspx' AND ACTIVE_STATUS=1"));
                    Response.Redirect("Examination/BacklogExamregEndSem.aspx?pageno=" + pageno + "");//2174 in test
                }
                else if (rec_code == "CF")
                {
                    int pageno = Convert.ToInt32(objCommon.LookUp("ACCESS_LINK", "AL_NO", "AL_URL = 'ACADEMIC/EXAMINATION/StudentCondonationFees.aspx' AND ACTIVE_STATUS=1"));
                    Response.Redirect("Examination/StudentCondonationFees.aspx?pageno=" + pageno + "");//2541 in test
                }
                else if (rec_code == "REF")
                {
                    int pageno = Convert.ToInt32(objCommon.LookUp("ACCESS_LINK", "AL_NO", "AL_URL = 'ACADEMIC/ReviewRegistration.aspx' AND ACTIVE_STATUS=1"));
                    Response.Redirect("ReviewRegistration.aspx?pageno=" + pageno + "");//added for review fee on 06052022
                }
            }
        }
        catch { }
    }

    public void Showmessage(string message)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + message + "');", true);
    }

    public string GetMACAddress()
    {
        String st = String.Empty;
        foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
        {
            OperationalStatus ot = nic.OperationalStatus;
            if (nic.OperationalStatus == OperationalStatus.Up)
            {
                st = nic.GetPhysicalAddress().ToString();
                break;
            }
        }
        return st;
    }

    public void InitializeSession()
   {
        try
        {
            //int IDNO = Convert.ToInt32(Request.QueryString["id"].ToString());
            int IDNO = Convert.ToInt32(ViewState["IDNO"]);
            //int IDNO = Convert.ToInt32(ViewState["IDNO"].ToString());
            //Session["colcode"] = 50;
            int userno = Convert.ToInt32(objCommon.LookUp("USER_ACC", "UA_NO", "UA_IDNO = " + IDNO + ""));

            //Get Common Details
            SqlDataReader dr = objCommon.GetCommonDetails();
            if (dr != null)
            {
                if (dr.Read())
                {
                    Session["coll_name"] = dr["CollegeName"].ToString();
                }
            }

            User_AccController objUC = new User_AccController();
            UserAcc objUA = objUC.GetSingleRecordByUANo(userno);

            DataSet ds = objCommon.FillDropDown("ACD_ACCESS_MASTER A INNER JOIN ACD_MACHINE_TYPE_MASTER B ON (B.MACTYPENO=A.MACTYPENO AND B.COLLEGE_CODE=A.COLLEGE_CODE)", "A.MACADD", "B.MACTYPE_STATUS", "A.UA_NO=" + objUA.UA_No + "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt32(ds.Tables[0].Rows[0][1]) == 0)
                {
                    Session["USER_MAC"] = Convert.ToString(ds.Tables[0].Rows[0][0]);
                }
                else
                {
                    Session["USER_MAC"] = "0";
                }
            }

            Session["userno"] = objUA.UA_No.ToString();
            Session["idno"] = objUA.UA_IDNo.ToString();
            Session["username"] = objUA.UA_Name;
            Session["usertype"] = objUA.UA_Type;
            Session["userfullname"] = objUA.UA_FullName;
            Session["dec"] = objUA.UA_Dec.ToString();
            Session["userdeptno"] = objUA.UA_DeptNo.ToString();
            DataSet dsReff = objCommon.FillDropDown("REFF", "*", "", string.Empty, string.Empty);
            Session["colcode"] = dsReff.Tables[0].Rows[0]["COLLEGE_CODE"].ToString(); //Added by Irfan Shaikh on 20190424
            Session["currentsession"] = objCommon.LookUp("ACD_SESSION_MASTER", "MAX(SESSIONNO)", "SESSIONNO>0 AND FLOCK=1");
            Session["firstlog"] = objUA.UA_FirstLogin;
            Session["ua_status"] = objUA.UA_Status;
            Session["ua_section"] = objUA.UA_section.ToString();
            Session["UA_DESIG"] = objUA.UA_Desig.ToString();
            string ipAddress = Request.ServerVariables["REMOTE_HOST"];
            Session["ipAddress"] = ipAddress;
            string macAddress = GetMACAddress();
            Session["macAddress"] = macAddress;

            int retLogID = LogTableController.AddtoLog(Session["username"].ToString(), Session["ipAddress"].ToString(), Session["macAddress"].ToString(), DateTime.Now);
            Session["logid"] = retLogID + 1;
            Session["loginid"] = retLogID.ToString();

            string lastlogout = string.Empty;
            string lastloginid = objCommon.LookUp("LOGFILE", "MAX(ID)", "UA_NAME='" + Session["username"].ToString() + "' AND UA_NAME IS NOT NULL");
            Session["lastloginid"] = lastloginid.ToString();
            if (Session["lastloginid"].ToString() != string.Empty)
            {
                lastlogout = objCommon.LookUp("LOGFILE", "LOGOUTTIME", "ID=" + Convert.ToInt32(Session["lastloginid"].ToString()));
            }

            Session["sessionname"] = objCommon.LookUp("ACD_SESSION_MASTER", "SESSION_NAME", "FLOCK=1");
            Session["hostel_session"] = objCommon.LookUp("ACD_HOSTEL_SESSION", "MAX(HOSTEL_SESSION_NO)", "FLOCK=1");
            Session["WorkingDate"] = DateTime.Now.ToString();
            Session["college_nos"] = objUA.COLLEGE_CODE;
            Session["Session"] = Session["sessionname"].ToString();


        }
        catch { }
    }

    //GENERATE REPORT AFTER ONLINE PAYMENT DONE SUCCESSFULLY. 
    private void ShowReport_NEW(string reportTitle, string rptFileName,int reval_type)
    {
        try
        {
            InitializeSession();
            //string col = Session["colcode"].ToString();
            //string userno = Session["userno"].ToString();
            
            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().IndexOf("Academic")));

            //string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToUpper().IndexOf("ADMISSION")));
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            //url += "Reports/CommonReport.aspx?";

            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //@P_REVAL_TYPE = 1 for Photo copy AND 2 for reval
            url += "&param=@P_ORDER_ID=" + ViewState["Orderid"] + ",@P_REVAL_TYPE=" + reval_type + ",@P_COLLEGE_CODE=" + Session["colcode"].ToString() + "";
            //url += "&param=@P_ORDER_ID=" + ViewState["Orderid"] + ",@P_REVAL_TYPE=1,@P_COLLEGE_CODE=50";
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PhotoReval_Response.ShowReport_NEW() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnReports_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Orderid"] != null)
            {
                string rec_code = objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "ORDER_ID = '" + ViewState["Orderid"]+"'");
                if (rec_code == "PRF")
                {
                    this.ShowReport_NEW("Photo Reval Payment_Details", "PhotoRevalPaymentReceipt.rpt", 1);//1 for photo copy
                }
                else if (rec_code == "RF")
                {
                    this.ShowReport_NEW("Revaluation Payment_Details", "PhotoRevalPaymentReceipt.rpt", 2);//2 for reval
                }
                else if (rec_code == "AEF")
                {
                    this.ShowReport_NEW("Arrear Payment_Details", "PhotoRevalPaymentReceipt.rpt", 3);//3 for Arrear
                }
                else if (rec_code == "CF")
                {
                    this.ShowReport_NEW("Condonation Payment_Details", "PhotoRevalPaymentReceipt.rpt", 4);//4 for Condonation
                }
                else if (rec_code == "REF")
                {
                    this.ShowReport_NEW("Review_Fee_Payment_Details", "PhotoRevalPaymentReceipt.rpt", 5);//5 for REVIEW//
                }

            }
        }
        catch { }
    }

    public string getRemoteAddr()
    {
        string UserIPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (UserIPAddress == null)
        {
            UserIPAddress = Request.ServerVariables["REMOTE_ADDR"];
        }
        return UserIPAddress;
    }
    
    private void ShowReport(string reportTitle, string rptFileName,string rec_code,int reval_type)
    {
        try
        {
            InitializeSession();

            int SessionNo = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "SESSIONNO", "ORDER_ID = " + ViewState["Orderid"] + " AND ISNULL(CAN,0)=0 AND RECIEPT_CODE='" + rec_code + "'"));
            int idno = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "IDNO", "ORDER_ID = " + ViewState["Orderid"] + " AND ISNULL(CAN,0)=0 AND RECIEPT_CODE='" + rec_code + "'"));

            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;

            //@P_REVAL_TYPE = 1 for Photo copy AND 2 for reval
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + Convert.ToInt32(idno) + ",@P_SESSIONNO=" + Convert.ToInt32(SessionNo) + ",@P_REVAL_TYPE=" + reval_type + "";

            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "PhotoReval_Response.ShowReport_NEW() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    private void ShowAEFReport(string reportTitle, string rptFileName, string rec_code)
    {

        InitializeSession();
        int SessionNo = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "SESSIONNO", "ORDER_ID = " + ViewState["Orderid"] + " AND ISNULL(CAN,0)=0 AND RECIEPT_CODE='" + rec_code + "'"));
        int idno = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "IDNO", "ORDER_ID = " + ViewState["Orderid"] + " AND ISNULL(CAN,0)=0 AND RECIEPT_CODE='" + rec_code + "'"));
        //int Semesterno = Convert.ToInt32(objCommon.LookUp("ACD_DCR", "SEMESTERNO", "ORDER_ID = " + ViewState["Orderid"] + " AND ISNULL(CAN,0)=0 AND RECIEPT_CODE='" + rec_code + "'"));
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("academic")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            //if (ViewState["usertype"].ToString() == "2")
            //{
            //    url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + Convert.ToInt32(SessionNo) + ",@P_SEMESTERNO=" + Semesterno;
            //}
            //else if (ViewState["usertype"].ToString() == "1" || ViewState["usertype"].ToString() == "3")
            //{
            url += "&param=@P_COLLEGE_CODE=" + Session["colcode"].ToString() + ",@P_IDNO=" + idno + ",@P_SESSIONNO=" + Convert.ToInt32(SessionNo) ;
            //}
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            //sb.Append(@"window.open('" + url + "','','" + features + "');");

            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "controlJSScript", sb.ToString(), true);
            divMsg.InnerHtml = " <script type='text/javascript' language='javascript'>";
            divMsg.InnerHtml += " window.open('" + url + "','" + reportTitle + "','addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes');";
            divMsg.InnerHtml += " </script>";
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Academic_ReceiptTypeDefinition.ShowAEFReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnRegistrationSlip_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["Orderid"] != null)
            {
                string rec_code = objCommon.LookUp("ACD_DCR", "RECIEPT_CODE", "ORDER_ID = '" + ViewState["Orderid"]+"'");
                if (rec_code == "PRF")
                {
                    ShowReport("Photo Copy Registration Slip", "rptPhotoRevaluation.rpt", rec_code, 1); //1 for photo copy
                }
                else if (rec_code == "RF")
                {
                    ShowReport("Revaluation Registration Slip", "rptPhotoRevaluation.rpt", rec_code, 2); //2 for reval
                }
                else if (rec_code == "AEF")
                {
                    ShowAEFReport("ExamRegistrationSlip", "PaymentReceipt_Exam_Registered_Courses.rpt", rec_code);
                }
                else if (rec_code == "CF")
                {
                    ShowReport("CondonationSlip", "StudCondonation.rpt", rec_code, 4);
                }
                else if (rec_code == "REF")
                {
                    ShowReport("Review fee Detail", "rptPhotoRevaluation.rpt", rec_code, 3); //3 for review  on 06052022
                }
            }
        }
        catch { }
    }       
}
