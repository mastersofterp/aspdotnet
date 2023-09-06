using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
   public  class Str_Invoice
    {

       public int INVTRNO { get; set; }
        public int GRNID { get; set; }
        public string INVNO { get; set; }
        public DateTime INVDATE { get; set; }
        public DateTime GRNDATE { get; set; }
        public int PNO { get; set; }
        public int MDNO { get; set; }
        public string PORDNO { get; set; }
        public string GRN_NUM { get; set; }
        public string REMARK { get; set; }
        public int CREATED_BY { get; set; }
        public DataTable INVOICE_ITEM_TBL { get; set; }
        public DataTable INVOICE_TAX_TBL { get; set; }
        public string DMNO { get; set; }
        public DateTime DMDATE { get; set; }
        public int MODIFIED_BY { get; set; }
        public decimal NETAMOUNT { get; set; }  // Shaikh Juned 11-11-2022

        public int IsBlob { get; set; }  //Shaikh Juned 28-08-2023 for check blob status
        public string INV_FILE_NAME { get; set; }
        public DataTable INVOICE_UPLOAD_FILE_TBL { get; set; }


    }
}
