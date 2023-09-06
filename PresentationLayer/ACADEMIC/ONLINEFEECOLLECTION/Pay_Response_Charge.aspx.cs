using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.BusinessEntities.RazorPay;
using IITMS.UAIMS;
using Newtonsoft.Json;
using Razorpay.Api;

public partial class Pay_Response_Charge : System.Web.UI.Page
{
    Common objCommon = new Common();
    string SuccessMessage = string.Empty;
    string ErrorMessage = string.Empty;
    Ent_Pay_Response ObjPR = new Ent_Pay_Response();
    //RazorPayController ObjRPC = new RazorPayController();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string paymentId = Request.Form["hdnPaymentId"];
                string paymentId1 = Request.Form["razorpay_payment_id"];
                 
                string razorpay_signature = Request.Form["razorpay_signature"];
                string order_id = Session["Order_ID"].ToString();
                //string secret = "IfqHQ4g8NywTLCTFEaMxMRjc";

                Dictionary<string, object> input = new Dictionary<string, object>();
                double tranAmt = Convert.ToDouble(Request.Form["hdnAmount"]);
                input.Add("amount", Convert.ToInt32(Math.Round(tranAmt * 100))); // this amount should be same as transaction amount

                string key = Convert.ToString(Session["RazKey"]);
                string secret = Convert.ToString(Session["Secrets"]);

                RazorpayClient client = new RazorpayClient(key, secret);
                var json = "";
                if (paymentId != null)
                {
                    Payment payment = client.Payment.Fetch(paymentId);
                    json = payment.Attributes.ToString(Formatting.None);

                }
                Ent_Payment objPay = JsonConvert.DeserializeObject<Ent_Payment>(json);
                long unixDate = Convert.ToInt64(objPay.created_at);
                DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                DateTime sTime = start.AddSeconds(unixDate).ToLocalTime();

                if (objPay.status == "captured")
                {
                    //***********"Transaction proceed successfully."************
                    //pnlSuccess.Visible = true;
                    //lblPaySeccess.Visible = true;
                    //pnlfailed.Visible = false;
                    //ObjPR.TransactionId = objPay.notes.merchant_order_id.ToString();
                    //ObjPR.Status = "Ok";
                    //ObjPR.Message = "captured";
                    //ObjPR.ErrorMessage = "NA";
                    //ObjPR.ResponceTransactionId = objPay.id;
                    //ObjPR.Amount = Convert.ToDouble(objPay.amount) / 100;
                    //ObjPR.TransactionTime = sTime;
                    //ObjPR.OrderId = objPay.order_id;
                    //ObjPR.CreatedBy = Convert.ToInt32(Session["USERNO"].ToString());
                    //ObjPR.UserNo = Session["USERNO"].ToString();
                    ////ObjPR.IPAddress = Session["IPADDR"].ToString();
                    //ObjPR.IPAddress = Session["ipAddress"].ToString();
                    //ObjPR.OrderId = Session["Order_ID"].ToString();                     
                    //ObjPR.MACAddress = "";
                    //int Result = 0;

                    //Ent_Payment_notes ss = new Ent_Payment_notes();
                    //ss = objPay.notes;
                    //string comment_date = objPay.created_at;
                    ////Result = ObjRPC.InsertRazorPayDetails(ObjPR);
                    //Result = ObjRPC.InsertRazorPayDetailsReponse(ObjPR,objPay);
                    //lblmessage.Visible = true;

                    ////*************DISPLAY MSG ON PAGE*************
                    //lblmessage.Text = "Hello ! <p>" + Session["STUDENT_NAME"] + ", We have processed Payment of  Rs." + ObjPR.Amount + " successfully.</p><p><strong>Transaction ID :</strong> " + ObjPR.ResponceTransactionId.ToString() + ".</p><p>Thank You!</p>";

                    ////***********MSG FOR SMS********************
                    //SuccessMessage = "Hello ! " + Session["STUDENT_NAME"] + ", We have processed Payment of  Rs." + ObjPR.Amount + " successfully. Transaction ID : " + ObjPR.ResponceTransactionId.ToString() + ". Thank You! JH ADMISSION";


                    //ViewState["CREATEDBY"] = null;
                    //ViewState["ORDERID"] = null;
                    //ViewState["TRANSACTIONID"] = null;

                    //ViewState["CREATEDBY"] = ObjPR.CreatedBy;
                    //ViewState["ORDERID"] = ObjPR.OrderId;
                    //ViewState["TRANSACTIONID"] = ObjPR.TransactionId;
                     
                    //lblmessage.Visible = true;

                    //if (Session["STUDENT_NAME"] != null && Session["MOBILENO"] != null)
                    //{
                    //    SendSMS(Session["MOBILENO"].ToString() + "", SuccessMessage);
                    //}
                }
                else if (objPay.status == "failed")
                {
                    //***********"Transaction failed"************
                    //pnlSuccess.Visible = false;
                    //pnlfailed.Visible = true;
                    //lblPayFailed.Visible = true;                     
                    //ObjPR.TransactionId = objPay.notes.merchant_order_id.ToString();
                    //ObjPR.Status = "failed";
                    //ObjPR.Message = objPay.error_code; //"FAILURE";
                    //ObjPR.ErrorMessage = objPay.error_description;
                    //ObjPR.ResponceTransactionId = objPay.id;
                    //ObjPR.Amount = Convert.ToDouble(objPay.amount) / 100;
                    //ObjPR.TransactionTime = sTime;
                    //ObjPR.OrderId = objPay.order_id;
                    //ObjPR.CreatedBy = Convert.ToInt32(Session["USERNO"].ToString());
                    //ObjPR.UserNo = Session["USERNO"].ToString();
                    //ObjPR.IPAddress = Session["IPADDR"].ToString();
                    //ObjPR.MACAddress = "";
                    //int Result = 0;                    
                    //Result = ObjRPC.InsertRazorPayDetailsReponse(ObjPR, objPay);
                    //lblerrorE.Visible = true;
                    //lblerrorE.Text = "Hello ! <p> " + Session["STUDENT_NAME"] + ", We are Unable To Process Payment of  Rs." + ObjPR.Amount + ".<br/> <br/>  Due to Reason : " + ObjPR.ErrorMessage + ".  <br/> <br/> </p><p><strong>Transaction ID :</strong> " + ObjPR.ResponceTransactionId.ToString() + ".<br/> Thank You";
                    //ErrorMessage = "Hello ! " + Session["STUDENT_NAME"] + ", We are Unable To Process Payment of  Rs." + ObjPR.Amount + ". Due to Reason : " + ObjPR.ErrorMessage + ".  Transaction ID : " + ObjPR.ResponceTransactionId.ToString() + ". Thank You";
                    //ViewState["CREATEDBY"] = null;
                    //ViewState["ORDERID"] = null;
                    //ViewState["TRANSACTIONID"] = null;
                    //ViewState["CREATEDBY"] = ObjPR.CreatedBy;
                    //ViewState["ORDERID"] = ObjPR.OrderId;
                    //ViewState["TRANSACTIONID"] = ObjPR.TransactionId;
                    //if (Session["STUDENT_NAME"] != null && Session["MOBILENO"] != null)
                    //{
                    //    SendSMS(Session["MOBILENO"].ToString() + "", ErrorMessage);
                    //}
                }
            }
        }
        catch (Exception ex)
        {
            objCommon.ShowError(Page, "Unable To Process Transaction");
        }
    }

    public void SendSMS(string Mobile, string text)
    {
        string status = "";
        try
        {
            string Message = string.Empty;
            DataSet ds = objCommon.FillDropDown("Reff", "SMSProvider", "SMSSVCID,SMSSVCPWD", "", "");
            if (ds.Tables[0].Rows.Count > 0)
            {
                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("http://" + ds.Tables[0].Rows[0]["SMSProvider"].ToString() + "?"));
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("http://" + "www.SMSnMMS.co.in/sms.aspx" + "?"));
                request.ContentType = "text/xml; charset=utf-8";
                request.Method = "POST";

                string postDate = "ID=" + ds.Tables[0].Rows[0]["SMSSVCID"].ToString();
                postDate += "&";
                postDate += "Pwd=" + ds.Tables[0].Rows[0]["SMSSVCPWD"].ToString();
                postDate += "&";
                postDate += "PhNo=91" + Mobile;
                postDate += "&";
                postDate += "Text=" + text;

                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postDate);
                request.ContentType = "application/x-www-form-urlencoded";

                request.ContentLength = byteArray.Length;
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse _webresponse = request.GetResponse();
                dataStream = _webresponse.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                status = reader.ReadToEnd();
            }
            else
            {
                status = "0";
            }

        }
        catch
        {

        }
    }

    private void ShowReport(string reportTitle, string rptFileName, int userno)
    {
        try
        {
            string url = Request.Url.ToString().Substring(0, (Request.Url.ToString().ToLower().IndexOf("")));
            url += "Reports/CommonReport.aspx?";
            url += "pagetitle=" + reportTitle;
            url += "&path=~,Reports,Academic," + rptFileName;
            url += "&param=@P_USERNO=" + Convert.ToInt32(userno);// +",@P_ORDER_ID='1'" + ",@P_TRANSACTION_ID=''" + ",@P_COLLEGE_CODE='33'";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string features = "addressbar=no,menubar=no,scrollbars=1,statusbar=no,resizable=yes";
            sb.Append(@"window.open('" + url + "','','" + features + "');");
            ScriptManager.RegisterClientScriptBlock(this.updResponse, this.updResponse.GetType(), "controlJSScript", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            if (Convert.ToBoolean(Session["error"]) == true)
                objCommon.ShowError(Page, "Pay_Response_Charge.ShowReport() --> " + ex.Message + " " + ex.StackTrace);
            else
                objCommon.ShowError(Page, "Server Unavailable.");
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        ShowReport("Online_Payment_Report", "rptOnlinePaymentSlipNew.rpt", Convert.ToInt32(ViewState["CREATEDBY"]));
    }


    //My Code
    //generated_signature = CreateSHA256Signature(order_id + "|" + paymentId1);
    private string CreateSHA256Signature(string RawData)
    {
        /*
         <summary>Creates a SHA256 Signature</summary>
         <param name="RawData">The string used to create the SHA256 signautre.</param>
         <returns>A string containing the SHA256 signature.</returns>
         */

        System.Security.Cryptography.SHA256 hasher = System.Security.Cryptography.SHA256Managed.Create();
        byte[] HashValue = hasher.ComputeHash(Encoding.ASCII.GetBytes(RawData));

        string strHex = "";
        foreach (byte b in HashValue)
        {
            strHex += b.ToString("x2");
        }
        return strHex.ToUpper();
    }
    //my code end

}