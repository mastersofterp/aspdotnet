using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS.UAIMS;
using Razorpay.Api;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using System.Data.SqlClient;

public partial class RazorPay_Request : System.Web.UI.Page
{
    Common objCommon = new Common();
    public string Raz_orderId;
    public string MerchantId = "0";
    public string ApplicationId = "0";
    
    FeeCollectionController objFees = new FeeCollectionController();
    //RazorPayController ObjRPC = new RazorPayController();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
           SendRequest();
              
        }
    }

    public void SendRequest()
    {
        var College_ShortName = string.Empty;
        SqlDataReader dr = objCommon.GetCommonDetails();

        if (dr != null)
        {
            if (dr.Read())
            {
                College_ShortName = dr["CODE_STANDARD"].ToString();
                Session["CollegeNm"] = College_ShortName;
            }
        }

        int userno = Convert.ToInt32(Session["userno"]);
        DataSet ds = new DataSet();
        //ds = ObjRPC.GetAllDetailsForPayment(userno);
        Session["USERNO"] = null;
        Session["STUDENT_NAME"] = null;
        Session["MOBILENO"] = null;
        Session["USERNO"] = userno;

        string order_ID = Session["Order_ID"].ToString();


        double Amount = 0.00;
        string udf9 = string.Empty;

        //*********ITERATE THROUGH EACH REQUIRED COLUMN VALUE AND SET TO HIDDENT FIELDS AND PROPERTIES**************
        //foreach (DataRow dr in ds.Tables[0].Rows)
        //{

        //hdnCurrency.Value = "INR";
        //hdnStudentEmail.Value = Convert.ToString(Session["studEmail"]);
        //hdnMobileNumber.Value = Convert.ToString(Session["studPhone"]);
        //hdnKey.Value = Convert.ToString(Session["RAZORPAYKEY"]);
        //hdnStudentName.Value = Convert.ToString(Session["payStudName"]);
        //Session["PGTranId"] = TransactionId.ToString();
        //hdnAmount.Value = Session["studAmt"].ToString();
        //hdnMerchantName.Value = College_ShortName;

        Amount = Convert.ToDouble(Session["studAmt"]);
        Session["RazKey"] = Convert.ToString(Session["RAZORPAYKEY"]);
        Session["Secrets"] = Convert.ToString(Session["RAZORPAYSECRET"]);
        Session["MOBILENO"] = Convert.ToString(Session["studPhone"]);
        Session["STUDENT_NAME"] = Convert.ToString(Session["payStudName"]);
        var InstallmentNo = Session["Installmentno"].ToString();
        Session["Desc"] = InstallmentNo;
        MerchantId = Session["regno"].ToString();
        ApplicationId = Convert.ToString("admin");

        Session["studAddr"] = "Nagpur";
        Session["TranAmt"] = Convert.ToDecimal(Session["studAmt"]).ToString();
        Session["Callback_Url"] = Session["ResponseURL"].ToString();
        //"http://localhost:62868/PresentationLayer/ACADEMIC/ONLINEFEECOLLECTION/RazorPayOnlinePaymentResponse.aspx";
        Session["Cancel_url"] = "https://example.com/payment-cancel";

        Dictionary<string, object> input = new Dictionary<string, object>();

        //for sending notes details
        Dictionary<string, object> notes = new Dictionary<string, object>();
        notes.Add("Order_ID", order_ID);
        //

        input.Add("amount", Convert.ToInt32(Math.Round(Amount * 100))); // this amount should be same as transaction amount && AMOUNT SHOULD BE IN PAISA
        input.Add("currency", "INR");
        //input.Add("receipt", Convert.ToString(TransactionId));
        input.Add("payment_capture", 1);
        input.Add("notes", notes);

        //************string key and secret = "<Enter your Api Key and Secret here>";*************
        string key = Convert.ToString(Session["RAZORPAYKEY"]);
        string secret = Convert.ToString(Session["RAZORPAYSECRET"]);
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;  //added on date 24/07/2018 as per discussed with Prashant Gawali sir..
        RazorpayClient client = new RazorpayClient(key, secret);   //added on date 24/07/2018 as per discussed with Prashant Gawali sir..

        Razorpay.Api.Order order = client.Order.Create(input);

        Raz_orderId = order["id"].ToString();
        // hdnRazorId.Value = order["id"].ToString();
        Session["RazOrder_Id"] = order["id"].ToString();

        //
        //Razorpay.Api.Customer customer = client.Customer.Create(input);
        //Razorpay.Api.Payment payment = client.Payment.Capture(input);
        //

        string s1 = order["id"].ToString();

        int SessionNo; int UserNo; string RazorPayId; double dAmount; double Fee; double Tax; string OrderId; string RazorPayOrderId; string IP_Address; double RazorPay_Amount;
        SessionNo = 0;
        UserNo = Convert.ToInt32(Session["USERNO"].ToString());
        RazorPayId = "";
        dAmount = Convert.ToDouble(Session["studAmt"]);
        RazorPay_Amount = Amount;
        Fee = 0;
        Tax = 0;
        OrderId = Session["Order_ID"].ToString();
        RazorPayOrderId = Raz_orderId;
        IP_Address = Request.ServerVariables["REMOTE_HOST"].ToString();

        int Result = 0;
        Result = objFees.InsertRazorPayNotCaptureTransaction(SessionNo, UserNo, RazorPayId, dAmount, RazorPay_Amount, Fee, Tax, OrderId, RazorPayOrderId, IP_Address);


        //}
    }

    //protected void hdnPaymentId_ValueChanged(object sender, EventArgs e)
    //{
    //    string payid = hdnPaymentId.Value;
    //    Session["PaymentId"] = hdnPaymentId.Value;
    //    Response.Redirect("RazorPayOnlinePaymentRequest.aspx");
    //}

    //*********GENERATE UNIQUE TRANSACTION ID***********
    //public string RandomDigits(int length)
    //{
    //    var random = new Random();
    //    string s = string.Empty;
    //    for (int i = 0; i < length; i++)
    //        s = String.Concat(s, random.Next(10).ToString());jhghg
    //    return s;
    //}

    //private string CreateSHA256Signature(string RawData)
    //{
    //    /*
    //     <summary>Creates a SHA256 Signature</summary>
    //     <param name="RawData">The string used to create the SHA256 signautre.</param>
    //     <returns>A string containing the SHA256 signature.</returns>
    //     */

    //    System.Security.Cryptography.SHA256 hasher = System.Security.Cryptography.SHA256Managed.Create();
    //    byte[] HashValue = hasher.ComputeHash(Encoding.ASCII.GetBytes(RawData));

    //    string strHex = "";
    //    foreach (byte b in HashValue)
    //    {
    //        strHex += b.ToString("x2");
    //    }
    //    return strHex.ToUpper();;;;
    //}

}