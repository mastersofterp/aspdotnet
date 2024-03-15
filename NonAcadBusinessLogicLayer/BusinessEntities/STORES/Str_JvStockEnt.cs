using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class Str_JvStockEnt
            {
                public int JVTRAN_ID { get; set; }
                public string JVTRAN_SLIP_NO { get; set; }
                public DateTime TRAN_DATE { get; set; }
                public int JVTRAN_TYPE { get; set; }
                public int FROM_COLLEGE { get; set; }
                public int FROM_DEPT { get; set; }
                public int FROM_EMPLOYEE { get; set; }
                public int TO_COLLEGE { get; set; }
                public int TO_DEPT { get; set; }
                public int TO_EMPLOYEE { get; set; }
                public string REMARK { get; set; }
                public string STORE_USER_TYPE { get; set; }
                public int REQTRNO { get; set; }
                public string ISSUE_TYPE { get; set; }
                public int CREATED_BY { get; set; }
                public int MODIFIED_BY { get; set; }
                public int LOCATIONNO { get; set; }              //----31/10/2022
                public int StudentIdno { get; set; }
                

                public string COLLEGE_CODE { get; set; }
                public int ITEMNO { get; set; }
                public DataTable JV_ITEM_TBL { get; set; }
                public string INVIDNO { get; set; }
                public DataTable INV_ITEM_TBL { get; set; }  //18-07-2023 Add by juned  // fro save invoice Qty and Rate


                public int PNO { get; set; }
                public int CIS_ID { get; set; }
                public string COND_SALE_TRNO { get; set; }
                public DateTime SALE_DATE { get; set; }
                public DataTable SALE_SAVE_TABLE { get; set; }



                public DateTime OUT_DATE { get; set; }
                public string OUT_TIME { get; set; }
                public DateTime RECEIVED_DATE { get; set; }
                public string RECEIVED_TIME { get; set; }
                public string VEHICLE_NO { get; set; }

                #region Item Repair
                public int IR_ID { get; set; }
                public string GATEPASS_NO { get; set; }
                public string ITEM_IN { get; set; }
                public int ITEM_NO { get; set; }
                public int COLLEGE_NO { get; set; }  
                public int DEPT_NO { get; set; }
                public DateTime RETURN_DATE { get; set; }   
                public DataTable ITEM_REPAIR_TBL { get; set; }
                public string SENT_TO { get; set; }
                public string CARRY_EMP_NAME { get; set; }
                public string CARRY_EMP_MBL_NO { get; set; }
                public int EMP_FROM { get; set; }
                public int TRAN_TYPE { get; set; }
                
                #endregion
            }
        }
    }
}
