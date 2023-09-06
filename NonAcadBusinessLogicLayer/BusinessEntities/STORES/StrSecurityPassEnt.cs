using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class StrSecurityPassEnt
    {
        public int SPID { get; set; }
        public string SP_NUMBER { get; set; }
        public DateTime SPDATE { get; set; }
        public string TIME { get; set; }       
       
        public DateTime DMDATE { get; set; }
        public string DMNO { get; set; }
        public int PNO { get; set; }
        public int MDNO { get; set; }
        public string GATE_KEEPER { get; set; }
        public string INCHARGE { get; set; }
        public string PORDNO { get; set; }
        public string REMARK { get; set; }
        public string VEHICLE_NO { get; set; }
        public int CREATED_BY { get; set; }
        public int MODIFIED_BY { get; set; }

        public DataTable SEC_PASS_ITEM_TBL { get; set; }

        #region Security Pass Outward
        public int SP_OW_ID { get; set; }
        public int GP_NUMBER { get; set; }
        public string REG_SLIP_NO { get; set; }
        public int IR_ID { get; set; }
        public DateTime OUT_DATE { get; set; }
        public string OUT_TIME { get; set; }
        public DateTime RECEIVED_DATE { get; set; }
        public string RECEIVED_TIME { get; set; }
        public string IRTRANID { get; set; }
        public int TRAN_TYPE { get; set; }
        #endregion
    }
}
