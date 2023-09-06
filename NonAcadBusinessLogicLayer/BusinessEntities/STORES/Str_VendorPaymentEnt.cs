using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IITMS.UAIMS.BusinessLogicLayer.BusinessEntities
{
    public class Str_VendorPaymentEnt
    {
        public int VPID { get; set; }
        public string VP_NUMBER { get; set; }
        public DateTime VPDATE { get; set; }
        public int PNO { get; set; }
        public int PAYMENT_TYPE { get; set; }
        public string PORDNO { get; set; }
        public string GRNID { get; set; }
        public string INVTRNO { get; set; }
        public double PAYMENT_AMOUNT { get; set; }
        public int MODE_OF_PAY { get; set; }
        public int BANK_ID { get; set; }
        public int BRANCH_ID { get; set; }
        public int IFSC_ID { get; set; }
        public int BANKACCNO_ID { get; set; }
        public string PAYEE_NAME { get; set; }
        public string REMARK { get; set; }
        public int CREATED_BY { get; set; }
        public int MODIFIED_BY { get; set; }
        public DataTable VENDOR_PAYMENT_TABLE{ get; set; }
    }
}
