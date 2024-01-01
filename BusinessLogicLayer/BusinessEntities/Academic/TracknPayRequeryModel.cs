using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraknPayRequery_Sheduler.Model
{
   public class TracknPayRequeryModel
    {
    }
    public class VMTracknPay
    {
        public string RequestUrl { get; set; }
        public int PaymentGateway { get; set; }
        public string Hash { get; set; }
        public string merchantKey { get; set; }
        public string trackId { get; set; }
        public string salt { get; set; }
        public string address_line_1 { get; set; }
        public string address_line_2 { get; set; }
        public string request_url { get; set; }
        public string amount { get; set; }
        public string api_key { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string currency { get; set; }
        public string description { get; set; }
        public string email { get; set; }
        public string mode { get; set; }
        public string name { get; set; }
        public string order_id { get; set; }
        public string phone { get; set; }
        public string return_url { get; set; }
        public string state { get; set; }
        public string udf1 { get; set; }
        public string udf2 { get; set; }
        public string udf3 { get; set; }
        public string udf4 { get; set; }
        public string udf5 { get; set; }
        public string zip_code { get; set; }
        public string hash { get; set; }
        public DateTime date { get; set; }
        public string result { get; set; }
        public string error_desc { get; set; }
        public string transactionId { get; set; }
    }

    public class VMTrackNPayRequeryPost
    {
        public string api_key { get; set; }
        public string order_id { get; set; }
        public string hash { get; set; }
        public string transaction_id { get; set; }
    }

    public class VMTrackNPayPaymentStatus
    {
        public List<VMTrackNPayPaymentStatusResponse> data { get; set; }
        public string hash { get; set; }
    }
    public class VMTrackNPayPaymentStatusResponse
    {
        public string transaction_id { get; set; }
        public string bank_code { get; set; }
        public string payment_mode { get; set; }
        public string payment_channel { get; set; }
        public string payment_datetime { get; set; }
        public string response_code { get; set; }
        public string response_message { get; set; }
        public string authorization_staus { get; set; }
        public string order_id { get; set; }
        public string amount { get; set; }
        public string amount_orig { get; set; }
        public string tdr_amount { get; set; }
        public string tax_on_tdr_amount { get; set; }
        public string description { get; set; }
        public string error_desc { get; set; }
        public string customer_phone { get; set; }
        public string customer_name { get; set; }
        public string customer_email { get; set; }
    }
}
