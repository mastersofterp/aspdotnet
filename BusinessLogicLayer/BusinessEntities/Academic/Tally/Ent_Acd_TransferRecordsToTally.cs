using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
namespace IITMS.NITPRM.BusinessLayer.BusinessEntities
{
    public class Ent_Acd_TransferRecordsToTally
            {
                private string _VoucherTypeName = string.Empty;
                private DateTime _VoucherDate = DateTime.MinValue;
                private string _ReceiptNumber = string.Empty;
                private string _NameOnReceipt = string.Empty;
                private string _InstrumentNumber = string.Empty;
                private DateTime _InstrumentDate = DateTime.MinValue;
                private string _CashLedgerName = string.Empty;
                private string _BankLedgerName = string.Empty;
                private double _HeadAmount = 0.00;
                private string _UniqId = string.Empty;
                private string _BankName = string.Empty;
                private string _BankLocation = string.Empty;
                private double _TotalAmount = 0.00;
                private string _Narration = string.Empty;
                private DataTable _HeadsTable = null;
                private string _ReceiptLedgerName = string.Empty;
                private string _PaymentFavoring = string.Empty;
                private string _BankPartyName = string.Empty;

                private int _CashBookId = 0;
                private int _CollegeId = 0;
                private DateTime _FromDate = DateTime.MinValue;
                private DateTime _ToDate = DateTime.MinValue;

                private int _ModifiedBy = 0;
                private DateTime _ModifiedDate = DateTime.MinValue;
                private string _IPAddress = string.Empty;
                private string _MACAddress = string.Empty;

                private string _DcrId = string.Empty;





                public string VoucherTypeName { get { return _VoucherTypeName; } set { _VoucherTypeName = value; } }
                public DateTime VoucherDate { get { return _VoucherDate; } set { _VoucherDate = value; } }
                public string ReceiptNumber { get { return _ReceiptNumber; } set { _ReceiptNumber = value; } }
                public string NameOnReceipt { get { return _NameOnReceipt; } set { _NameOnReceipt = value; } }
                public string InstrumentNumber { get { return _InstrumentNumber; } set { _InstrumentNumber = value; } }
                public DateTime InstrumentDate { get { return _InstrumentDate; } set { _InstrumentDate = value; } }
                public string CashLedgerName { get { return _CashLedgerName; } set { _CashLedgerName = value; } }
                public string BankLedgerName { get { return _BankLedgerName; } set { _BankLedgerName = value; } }
                public double HeadAmount { get { return _HeadAmount; } set { _HeadAmount = value; } }
                public string UniqId { get { return _UniqId; } set { _UniqId = value; } }
                public string BankName { get { return _BankName; } set { _BankName = value; } }
                public string BankLocation { get { return _BankLocation; } set { _BankLocation = value; } }
                public double TotalAmount { get { return _TotalAmount; } set { _TotalAmount = value; } }
                public string Narration { get { return _Narration; } set { _Narration = value; } }
                public DataTable HeadsTable { get { return _HeadsTable; } set { _HeadsTable = value; } }
                public string ReceiptLedgerName { get { return _ReceiptLedgerName; } set { _ReceiptLedgerName = value; } }
                public string PaymentFavoring { get { return _PaymentFavoring; } set { _PaymentFavoring = value; } }
                public string BankPartyName { get { return _BankPartyName; } set { _BankPartyName = value; } }



                public int CashBookId { get { return _CashBookId; } set { _CashBookId = value; } }
                public int CollegeId { get { return _CollegeId; } set { _CollegeId = value; } }
                public DateTime FromDate { get { return _FromDate; } set { _FromDate = value; } }
                public DateTime ToDate { get { return _ToDate; } set { _ToDate = value; } }


                public int ModifiedBy { get { return _ModifiedBy; } set { _ModifiedBy = value; } }
                public DateTime ModifiedDate { get { return _ModifiedDate; } set { _ModifiedDate = value; } }
                public string IPAddress { get { return _IPAddress; } set { _IPAddress = value; } }
                public string MACAddress { get { return _MACAddress; } set { _MACAddress = value; } }

                public string DcrId { get { return _DcrId; } set { _DcrId = value; } }

            }
   
}
