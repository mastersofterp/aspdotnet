using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace IITMS.NITPRM.BusinessLayer.BusinessEntities
{
    public class Ent_Acd_TallyFeeHeads
            {
                private int _CollegeId = 0;
                private int _CashBookId = 0;
                private string _CommandType = string.Empty;
                private DataTable _FeeHeadTally = null;
                private int _ModifiedBy = 0;
                private DateTime _ModifiedDate = DateTime.MinValue;
                private string _IPAddress = string.Empty;
                private string _MACAddress = string.Empty;
                private string _CashBookName = string.Empty;


                public int CollegeId { get { return _CollegeId; } set { _CollegeId = value; } }
                public int CashBookId { get { return _CashBookId; } set { _CashBookId = value; } }
                public string CommandType { get { return _CommandType; } set { _CommandType = value; } }
                public DataTable FeeHeadTally { get { return _FeeHeadTally; } set { _FeeHeadTally = value; } }


                public int ModifiedBy { get { return _ModifiedBy; } set { _ModifiedBy = value; } }
                public DateTime ModifiedDate { get { return _ModifiedDate; } set { _ModifiedDate = value; } }
                public string IPAddress { get { return _IPAddress; } set { _IPAddress = value; } }
                public string MACAddress { get { return _MACAddress; } set { _MACAddress = value; } }
                public string CashBookName { get { return _CashBookName; } set { _CashBookName = value; } }






            }
    
}
