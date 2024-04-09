using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessEntities.RazorPay
{
    public class Ent_Payment
    {
        public string id { get; set; }
        public string entity { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public string status { get; set; }
        public string order_id { get; set; }
        public string invoice_id { get; set; }
        public string international { get; set; }
        public string method { get; set; }
        public string amount_refunded { get; set; }
        public string refund_status { get; set; }
        public string captured { get; set; }
        public string description { get; set; }
        public string card_id { get; set; }
        public string bank { get; set; }
        public string wallet { get; set; }
        public string vpa { get; set; }
        public string email { get; set; }
        public string contact { get; set; }
        public string fee { get; set; }
        public string tax { get; set; }
        public string error_code { get; set; }
        public string error_description { get; set; }
        public string created_at { get; set; }
        public Ent_Payment_notes notes { get; set; }
        public Ent_Payment_Error error { get; set; }
    }

    public class Ent_Payment_notes
    {
        public string merchant_order_id { get; set; }
    }

    public class Ent_Payment_Error
    {
        public string code { get; set; }
        public string description { get; set; }
    }

    public class AcquirerData
    {
        public string rrn { get; set; }
        public string upi_transaction_id { get; set; }
        public object auth_code { get; set; }
    }

    public class Notes
    {
        public string Order_ID { get; set; }
        //[JsonProperty("shipping address")]
        // public string shipping address { get; set; }
    }

    public class Ent_PaymentNew
    {
        public string id { get; set; }
        public string entity { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public string status { get; set; }
        public string order_id { get; set; }
        public object invoice_id { get; set; }
        public bool international { get; set; }
        public string method { get; set; }
        public int amount_refunded { get; set; }
        public object refund_status { get; set; }
        public bool captured { get; set; }
        public string description { get; set; }
        public object card_id { get; set; }
        public object bank { get; set; }
        public object wallet { get; set; }
        public string vpa { get; set; }
        public string email { get; set; }
        public string contact { get; set; }
        public Notes notes { get; set; }
        public int fee { get; set; }
        public int tax { get; set; }
        public object error_code { get; set; }
        public object error_description { get; set; }
        public object error_source { get; set; }
        public object error_step { get; set; }
        public object error_reason { get; set; }
        public AcquirerData acquirer_data { get; set; }
        public int created_at { get; set; }
        public Upi upi { get; set; }
    }

    public class Upi
    {
        public string vpa { get; set; }
    }

    public class Card
    {
        public string id { get; set; }
        public string entity { get; set; }
        public string name { get; set; }
        public string last4 { get; set; }
        public string network { get; set; }
        public string type { get; set; }
        public object issuer { get; set; }
        public bool international { get; set; }
        public bool emi { get; set; }
        public string sub_type { get; set; }
        public object token_iin { get; set; }
    }

    public class Root
    {
        public string id { get; set; }
        public string entity { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public string status { get; set; }
        public string order_id { get; set; }
        public object invoice_id { get; set; }
        public bool international { get; set; }
        public string method { get; set; }
        public int amount_refunded { get; set; }
        public object refund_status { get; set; }
        public bool captured { get; set; }
        public string description { get; set; }
        public string card_id { get; set; }
        public Card card { get; set; }
        public object bank { get; set; }
        public object wallet { get; set; }
        public object vpa { get; set; }
        public string email { get; set; }
        public string contact { get; set; }
        public Notes notes { get; set; }
        public object fee { get; set; }
        public object tax { get; set; }
        public string error_code { get; set; }
        public string error_description { get; set; }
        public string error_source { get; set; }
        public string error_step { get; set; }
        public string error_reason { get; set; }
        public AcquirerData acquirer_data { get; set; }
        public int created_at { get; set; }
    }

}
