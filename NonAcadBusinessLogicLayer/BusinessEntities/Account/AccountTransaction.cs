using System;
using System.Data;
using System.Web;



namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLayer.BusinessEntities
        {
            public class AccountTransaction
            {
                #region Private Members
                private int _SUBTR_NO = 0;
                private DateTime _TRANSACTION_DATE = DateTime.MinValue;
                private string _TRANSACTION_TYPE = string.Empty;
                private string _TRAN = string.Empty;
                private int _PARTY_NO = 0;
                private string _OPARTY_NO = string.Empty;
                private double _AMOUNT = 0.0f;
                private int _DEGREE_NO = 0;
                private int _VOUCHER_NO = 0;
                private string _TRANSFER_ENTRY = string.Empty;
                private string _CBTYPE_STATUS = string.Empty;
                private string _CBTYPE = string.Empty;
                private string _RECIEPT_PAYMENT_FEES = string.Empty;
                private string _REC_NO = string.Empty;
                private string _CHQ_NO = string.Empty;
                private DateTime _CHQ_DATE = DateTime.MinValue;
                private string _CHALLAN = string.Empty;
                private string P_CAN = string.Empty;
                private DateTime _RECON_DATE = DateTime.MinValue;
                private int _DCR_NO = 0;
                private int _IDF_NO = 0;
                private int _CASH_BANK_NO = 0;
                private string _ADVANCE_REFUND_NONE = string.Empty;
                private string _PAGENO = string.Empty;
                private string _PARTICULARS = string.Empty;
                private string _COLLEGE_CODE = string.Empty;
                private string _USER = string.Empty;
                private DateTime _CREATED_MODIFIED_DATE = DateTime.MinValue;
                private string _COMPANY_CODE = string.Empty;
                private string _P_YEAR = string.Empty;
                private int _TRANSACTION_NO = 0;
                private string _STR_VOUCHER_NO = string.Empty;
                private string _STR_CB_VOUCHER_NO = string.Empty;
                private decimal _TaxPer = 0;

                //=========================Added By Akshay Dixit On 26-04-2022====================================
                private int _ISEMPLOYEE = 0;
                private int _IDNO = 0;
                private int _VOUCHER_SQN = 0;
                private int _PARENTID = 0;
                private int _BALANCEAMOUNT = 0;
                private int _CREATEDBY = 0;
                private int _MODIFIEDBY = 0;
                private int _OLD_VOUCHER_SQN = 0;
                private string _FILEPATH = string.Empty;


                private int _BRANCH_NO = 0;
                private int _BATCH_NO = 0;
                private int _SEM_NO = 0;
                //================================================================================================
                private DataTable _TransFieldsTbl_TRAN = null;

                public DataTable TransFieldsTbl_TRAN
                {
                    get { return _TransFieldsTbl_TRAN; }
                    set { _TransFieldsTbl_TRAN = value; }
                }

                //For Abstract Bill
                private string _VOUCHER_TYPE = string.Empty;

                public string VOUCHER_TYPE
                {
                    get { return _VOUCHER_TYPE; }
                    set { _VOUCHER_TYPE = value; }
                }
                private string _HEADACC = string.Empty;

                public string HEADACC
                {
                    get { return _HEADACC; }
                    set { _HEADACC = value; }
                }
                private double _GROSSAMOUNT = 0.00;

                public double GROSSAMOUNT
                {
                    get { return _GROSSAMOUNT; }
                    set { _GROSSAMOUNT = value; }
                }
                private string _NARRATION = string.Empty;

                public string NARRATION
                {
                    get { return _NARRATION; }
                    set { _NARRATION = value; }
                }
                private string _HEADACCOUNTID = string.Empty;

                public string HEADACCOUNTID
                {
                    get { return _HEADACCOUNTID; }
                    set { _HEADACCOUNTID = value; }
                }
                private string _TOTALPAYBLE = string.Empty;

                public string TOTALPAYBLE
                {
                    get { return _TOTALPAYBLE; }
                    set { _TOTALPAYBLE = value; }
                }

                private double _AdvanceAmount;

                public double AdvanceAmount
                {
                    get { return _AdvanceAmount; }
                    set { _AdvanceAmount = value; }
                }
                private double _BillAmount;

                public double BillAmount
                {
                    get { return _BillAmount; }
                    set { _BillAmount = value; }
                }
                private string _DisplayName;

                public string DisplayName
                {
                    get { return _DisplayName; }
                    set { _DisplayName = value; }
                }

                private int _JV_PARTY;

                public int JV_PARTY
                {
                    get { return _JV_PARTY; }
                    set { _JV_PARTY = value; }
                }
                private int _BANK_NO;

                public int BANK_NO
                {
                    get { return _BANK_NO; }
                    set { _BANK_NO = value; }
                }
                private int _BILLNO;

                public int BILLNO
                {
                    get { return _BILLNO; }
                    set { _BILLNO = value; }
                }
                public decimal TaxPer
                {
                    get { return _TaxPer; }
                    set { _TaxPer = value; }
                }

                //=============Added By Akshay Dixit On 26-04-2022==================

                public int ISEMPLOYEE
                {
                    get { return _ISEMPLOYEE; }
                    set { _ISEMPLOYEE = value; }
                }

                public int IDNO
                {
                    get { return _IDNO; }
                    set { _IDNO = value; }
                }

                public int VOUCHER_SQN
                {
                    get { return _VOUCHER_SQN; }
                    set { _VOUCHER_SQN = value; }
                }

                public int PARENTID
                {
                    get { return _PARENTID; }
                    set { _PARENTID = value; }
                }

                public int BALANCEAMOUNT
                {
                    get { return _BALANCEAMOUNT; }
                    set { _BALANCEAMOUNT = value; }
                }

                public int CREATEDBY
                {
                    get { return _CREATEDBY; }
                    set { _CREATEDBY = value; }
                }

                public int MODIFIEDBY
                {
                    get { return _MODIFIEDBY; }
                    set { _MODIFIEDBY = value; }
                }

                public int OLD_VOUCHER_SQN
                {
                    get { return _OLD_VOUCHER_SQN; }
                    set { _OLD_VOUCHER_SQN = value; }
                }

                public string FILEPATH
                {
                    get { return _FILEPATH; }
                    set { _FILEPATH = value; }
                }
                //================================================================
                

                public int BRANCH_NO
                {
                    get { return _BRANCH_NO; }
                    set { _BRANCH_NO = value; }
                }

                public int BATCH_NO
                {
                    get { return _BATCH_NO; }
                    set { _BATCH_NO = value; }
                }
                public int SEM_NO
                {
                    get { return _SEM_NO; }
                    set { _SEM_NO = value; }
                }

               #endregion

                #region Public 
                public int SUBTR_NO
                {
                    get { return _SUBTR_NO; }
                    set { _SUBTR_NO = value; }
                }
                public DateTime TRANSACTION_DATE
                {
                    get { return _TRANSACTION_DATE; }
                    set { _TRANSACTION_DATE = value; }
                }
                public string TRANSACTION_TYPE
                {
                    get { return _TRANSACTION_TYPE; }
                    set { _TRANSACTION_TYPE = value; }
                }
                public string TRAN
                {
                    get { return _TRAN; }
                    set { _TRAN = value; }
                }
                public int PARTY_NO
                {
                    get { return _PARTY_NO; }
                    set { _PARTY_NO = value; }
                }
                public string OPARTY_NO
                {
                    get { return _OPARTY_NO; }
                    set { _OPARTY_NO = value; }
                }
                public double AMOUNT
                {
                    get { return _AMOUNT; }
                    set { _AMOUNT = value; }
                }
                public int DEGREE_NO
                {
                    get { return _DEGREE_NO; }
                    set { _DEGREE_NO = value; }
                }
                public int VOUCHER_NO
                {
                    get { return _VOUCHER_NO; }
                    set { _VOUCHER_NO = value; }
                }
                public string TRANSFER_ENTRY
                {
                    get { return _TRANSFER_ENTRY; }
                    set { _TRANSFER_ENTRY = value; }
                }
                public string CBTYPE_STATUS
                {
                    get { return _CBTYPE_STATUS; }
                    set { _CBTYPE_STATUS = value; }
                }
                public string CBTYPE
                {
                    get { return _CBTYPE; }
                    set { _CBTYPE = value; }
                }
                public string RECIEPT_PAYMENT_FEES
                {
                    get { return _RECIEPT_PAYMENT_FEES; }
                    set { _RECIEPT_PAYMENT_FEES = value; }
                }
                public string REC_NO
                {
                    get { return _REC_NO; }
                    set { _REC_NO = value; }
                }
                public string CHQ_NO
                {
                    get { return _CHQ_NO; }
                    set { _CHQ_NO = value; }
                }
                public DateTime CHQ_DATE
                {
                    get { return _CHQ_DATE; }
                    set { _CHQ_DATE = value; }
                }
                public string CHALLAN
                {
                    get { return _CHALLAN; }
                    set { _CHALLAN = value; }
                }
                public string P_CAN1
                {
                    get { return P_CAN; }
                    set { P_CAN = value; }
                }
                public DateTime RECON_DATE
                {
                    get { return _RECON_DATE; }
                    set { _RECON_DATE = value; }
                }
                public int DCR_NO
                {
                    get { return _DCR_NO; }
                    set { _DCR_NO = value; }
                }
                public int IDF_NO
                {
                    get { return _IDF_NO; }
                    set { _IDF_NO = value; }
                }
                public int CASH_BANK_NO
                {
                    get { return _CASH_BANK_NO; }
                    set { _CASH_BANK_NO = value; }
                }
                public string ADVANCE_REFUND_NONE
                {
                    get { return _ADVANCE_REFUND_NONE; }
                    set { _ADVANCE_REFUND_NONE = value; }
                }
                public string PAGENO
                {
                    get { return _PAGENO; }
                    set { _PAGENO = value; }
                }
                public string PARTICULARS
                {
                    get { return _PARTICULARS; }
                    set { _PARTICULARS = value; }
                }
                public string COLLEGE_CODE
                {
                    get { return _COLLEGE_CODE; }
                    set { _COLLEGE_CODE = value; }
                }
                public string USER
                {
                    get { return _USER; }
                    set { _USER = value; }
                }
                public DateTime CREATED_MODIFIED_DATE
                {
                    get { return _CREATED_MODIFIED_DATE; }
                    set { _CREATED_MODIFIED_DATE = value; }
                }
                public string COMPANY_CODE
                {
                    get { return _COMPANY_CODE; }
                    set { _COMPANY_CODE = value; }
                }
                public string P_YEAR
                {
                    get { return _P_YEAR; }
                    set { _P_YEAR = value; }
                }
                public int TRANSACTION_NO
                {
                    get { return _TRANSACTION_NO; }
                    set { _TRANSACTION_NO = value; }
                }
                public string STR_VOUCHER_NO
                {
                    get { return _STR_VOUCHER_NO; }
                    set { _STR_VOUCHER_NO = value; }
                }
                public string STR_CB_VOUCHER_NO
                {
                    get { return _STR_CB_VOUCHER_NO ; }
                    set { _STR_CB_VOUCHER_NO = value; }
                }
                
            #endregion
            }

        }//END: BusinessLayer.BusinessEntities

    }//END: UAIMS  

}//END: IITMS

