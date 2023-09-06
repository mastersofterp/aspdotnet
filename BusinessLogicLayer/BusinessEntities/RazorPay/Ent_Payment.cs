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
}
