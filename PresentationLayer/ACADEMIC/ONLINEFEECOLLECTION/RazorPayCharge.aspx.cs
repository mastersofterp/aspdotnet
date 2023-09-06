using BusinessLogicLayer.BusinessEntities.Academic;
using BusinessLogicLayer.BusinessEntities.RazorPay;
using Newtonsoft.Json;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ACADEMIC_ONLINEFEECOLLECTION_RazorPayCharge : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                string paymentId = Request.Form["hdnPaymentId"];
                string paymentId1 = Request.Form["razorpay_payment_id"];
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
                    //ObjPR.IPAddress = Session["IPADDR"].ToString();
                    //ObjPR.MACAddress = "";
                    //int Result = 0;
                    //Result = ObjRPC.InsertRazorPayDetails(ObjPR);

                    //lblmessage.Visible = true;
                    ////lblerror.Visible = true;
                    ////lblerror.Text = ErrorMessage[1].ToString();
                    ////lblmessage.Text = "Hello ! " + Session["customerName"] + ", We have processed Payment of  Rs." + objpg.Amount + " successfully. <br/> <br/> Transaction ID : " + objpg.ResponceTransactionId.ToString() + ".<br/> Thank You";

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
                    ////ShowReport("Online Payment Report", "rptOnlinePaymentSlip.rpt", ObjPR.CreatedBy, ObjPR.OrderId, ObjPR.TransactionId);
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
                    ////btnReport.Visible = false;
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
                    //Result = ObjRPC.InsertRazorPayDetails(ObjPR);

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
            //objCommon.ShowError(Page, "Unable To Process Transaction");

            //throw;
        }
    }
}