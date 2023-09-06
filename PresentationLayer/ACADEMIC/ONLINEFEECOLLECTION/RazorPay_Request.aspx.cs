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

public partial class RazorPay_Request : System.Web.UI.Page
{
    Common objCommon = new Common();
    public string orderId;
    public string MerchantId = "0";
    public string ApplicationId = "0";
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
            Amount = Convert.ToDouble(Session["studAmt"]);

            hdnKey.Value = Convert.ToString(Session["RAZORPAYKEY"]);
            Session["RazKey"] = Convert.ToString(Session["RAZORPAYKEY"]);
            Session["Secrets"] = Convert.ToString(Session["RAZORPAYSECRET"]);
            hdnCurrency.Value = "INR";
            hdnStudentEmail.Value = Convert.ToString(Session["studEmail"]);
            hdnMobileNumber.Value = Convert.ToString(Session["studPhone"]);
            Session["MOBILENO"] = Convert.ToString(Session["studPhone"]);
            hdnStudentName.Value = Convert.ToString(Session["payStudName"]);
            Session["STUDENT_NAME"] = Convert.ToString(Session["payStudName"]);
            //Session["PGTranId"] = TransactionId.ToString();
            hdnAmount.Value = Session["studAmt"].ToString();
            //MerchantId = Convert.ToString(TransactionId);
            ApplicationId = Convert.ToString("admin");
            hdnMerchantName.Value = "ATLAS";  //

            Session["TranAmt"] = Convert.ToDecimal(Session["studAmt"]).ToString();

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

            //************string key = "<Enter your Api Key here>";*************
            //************string secret = "<Enter your Api Secret here>";********
            string key = Convert.ToString(Session["RAZORPAYKEY"]);
            string secret = Convert.ToString(Session["RAZORPAYSECRET"]);
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;//added on date 24/07/2018 as per discussed with Prashant Gawali sir..
            RazorpayClient client = new RazorpayClient(key, secret);//added on date 24/07/2018 as per discussed with Prashant Gawali sir..

            Razorpay.Api.Order order = client.Order.Create(input);

            orderId = order["id"].ToString();
            hdnRazorId.Value = order["id"].ToString();

            //
            
            //Razorpay.Api.Customer customer = client.Customer.Create(input);
            //Razorpay.Api.Payment payment = client.Payment.Capture(input);

            //

            string s1 = order["id"].ToString();

        //}
    }

    protected void hdnPaymentId_ValueChanged(object sender, EventArgs e)
    {
        string payid = hdnPaymentId.Value;
        Session["PaymentId"] = hdnPaymentId.Value;
        Response.Redirect("RazorPayOnlinePaymentRequest.aspx");
    }

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