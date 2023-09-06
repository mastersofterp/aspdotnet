using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace IITMS.NITPRM.BusinessLayer.BusinessEntities
{
    public class Ent_Acd_TallyConfig
            {
                private int _TallyConfigId = 0;
                private string _ServerName = string.Empty;
                private int _PortNumber = 0;
                private int _TallyVoucherTypeId = 0;
                private string _TallyVoucherType = string.Empty;
                private string _CashTallyVoucherName = string.Empty;
                private string _BankTallyVoucherName = string.Empty;
                private Boolean _IsActive = false;
                private int _CollegeId = 0;
                private int _CreatedBy = 0;
                private int _ModifiedBy = 0;
                private DateTime _ModifiedDate = DateTime.MinValue;
                private string _IPAddress = string.Empty;
                private string _MACAddress = string.Empty;
                private DateTime _StartTime = DateTime.MinValue;
                private DateTime _EndTime = DateTime.MinValue;
                private string _CommandType = string.Empty;


                public int TallyConfigId { get { return _TallyConfigId; } set { _TallyConfigId = value; } }
                public string ServerName { get { return _ServerName; } set { _ServerName = value; } }
                public int PortNumber { get { return _PortNumber; } set { _PortNumber = value; } }
                public Boolean IsActive { get { return _IsActive; } set { _IsActive = value; } }
                public int CollegeId { get { return _CollegeId; } set { _CollegeId = value; } }
                public int CreatedBy { get { return _CreatedBy; } set { _CreatedBy = value; } }
                public int ModifiedBy { get { return _ModifiedBy; } set { _ModifiedBy = value; } }
                public DateTime ModifiedDate { get { return _ModifiedDate; } set { _ModifiedDate = value; } }
                public string IPAddress { get { return _IPAddress; } set { _IPAddress = value; } }
                public string MACAddress { get { return _MACAddress; } set { _MACAddress = value; } }
                public DateTime StartTime { get { return _StartTime; } set { _StartTime = value; } }
                public DateTime EndTime { get { return _EndTime; } set { _EndTime = value; } }
                public string CommandType { get { return _CommandType; } set { _CommandType = value; } }

                public int TallyVoucherTypeId
                {
                    get { return _TallyVoucherTypeId; }
                    set { _TallyVoucherTypeId = value; }
                }

                public string TallyVoucherType
                {
                    get { return _TallyVoucherType; }
                    set { _TallyVoucherType = value; }
                }
                public string CashTallyVoucherName
                {
                    get { return _CashTallyVoucherName; }
                    set { _CashTallyVoucherName = value; }
                }
                public string BankTallyVoucherName
                {
                    get { return _BankTallyVoucherName; }
                    set { _BankTallyVoucherName = value; }
                }








            }
     
}
