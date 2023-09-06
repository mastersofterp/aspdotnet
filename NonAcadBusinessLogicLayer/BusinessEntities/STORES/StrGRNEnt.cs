using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class StrGRNEnt
    {
        public int SPID { get; set; }
        public int GRNID { get; set; }
        public string GRN_NUMBER { get; set; }
        public DateTime SPDATE { get; set; }
        public DateTime GRNDATE { get; set; } 
        public int PNO { get; set; }
        public int MDNO { get; set; }
        public string PORDNO { get; set; }
        public string REMARK { get; set; }      
        public int CREATED_BY { get; set; }
        public DataTable GRN_ITEM_TBL { get; set; }
        public DataTable GRN_TAX_TBL { get; set; }
        public string DMNO { get; set; }
        public DateTime DMDATE { get; set; }
        public int MODIFIED_BY { get; set; }
    }
}
