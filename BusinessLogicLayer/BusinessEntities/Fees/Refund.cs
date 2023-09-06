//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                        
// PAGE NAME     : REFUND 
// CREATION DATE : 31-JUL-2009                                                        
// CREATED BY    : AMIT YADAV
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class Refund
    {
        #region Private Fields
        private int _Organizationid = 0; //added by dileep kare on 31.12.2021
        private decimal _Cancellation_Charges = 0;//added by dileep kare on 31.12.2021

        private int _REFUND_NO;

        private int _DCR_NO;

        private string _REC_NO;

        private int _IDNO;

        private string _VOUCHER_NO;

        private DateTime _VOUCHER_DATE;

        FeeHeadAmounts feeItemsAmount = new FeeHeadAmounts(); // fee items

        DemandDrafts[] _paidChequesDemandDrafts = null;        

        private double _TOTAL_AMT;

        private double _CHQ_DD_AMT;

        private double _CASH_AMT;

        private string _PAY_TYPE;

        private bool _CAN;

        private DateTime _PRINTDATE;

        private string _REMARK;

        private int _COUNTER_NO;

        private int _UA_NO;

        private string _excessamount = string.Empty;


        private string _COLLEGE_CODE; 
        #endregion

        public Refund()
        {
        }

        #region Public Properties

        public int Organizationid //added by dileep kare on 31.12.2021
        {
            get { return _Organizationid; }
            set { _Organizationid = value; }
        }

        public decimal Cancellation_Charges//added by dileep kare on 31.12.2021
        {
            get { return _Cancellation_Charges; }
            set { _Cancellation_Charges = value; }
        }
        public string excessamount
        {
            get
            {
                return _excessamount;
            }
            set
            {

                _excessamount = value;

            }
        }
        public int RefundNo
        {
            get
            {
                return this._REFUND_NO;
            }
            set
            {
                if ((this._REFUND_NO != value))
                {
                    this._REFUND_NO = value;
                }
            }
        }

        public int DCR_NO
        {
            get
            {
                return this._DCR_NO;
            }
            set
            {
                if ((this._DCR_NO != value))
                {
                    this._DCR_NO = value;
                }
            }
        }

        public string ReceiptNo
        {
            get
            {
                return this._REC_NO;
            }
            set
            {
                if ((this._REC_NO != value))
                {
                    this._REC_NO = value;
                }
            }
        }

        public int IDNO
        {
            get
            {
                return this._IDNO;
            }
            set
            {
                if ((this._IDNO != value))
                {
                    this._IDNO = value;
                }
            }
        }

        public string VoucherNo
        {
            get
            {
                return this._VOUCHER_NO;
            }
            set
            {
                if ((this._VOUCHER_NO != value))
                {
                    this._VOUCHER_NO = value;
                }
            }
        }

        public DateTime VoucherDate
        {
            get
            {
                return this._VOUCHER_DATE;
            }
            set
            {
                if ((this._VOUCHER_DATE != value))
                {
                    this._VOUCHER_DATE = value;
                }
            }
        }

        public FeeHeadAmounts FeeItemsAmount
        {
            get { return feeItemsAmount; }
            set { feeItemsAmount = value; }
        }

        public DemandDrafts[] PaidChequesDemandDrafts
        {
            get { return _paidChequesDemandDrafts; }
            set { _paidChequesDemandDrafts = value; }
        }

        public double TotalAmount
        {
            get
            {
                return this._TOTAL_AMT;
            }
            set
            {
                if ((this._TOTAL_AMT != value))
                {
                    this._TOTAL_AMT = value;
                }
            }
        }

        public double ChequeDD_Amount
        {
            get
            {
                return this._CHQ_DD_AMT;
            }
            set
            {
                if ((this._CHQ_DD_AMT != value))
                {
                    this._CHQ_DD_AMT = value;
                }
            }
        }

        public double CashAmount
        {
            get
            {
                return this._CASH_AMT;
            }
            set
            {
                if ((this._CASH_AMT != value))
                {
                    this._CASH_AMT = value;
                }
            }
        }

        public string PayType
        {
            get
            {
                return this._PAY_TYPE;
            }
            set
            {
                if ((this._PAY_TYPE != value))
                {
                    this._PAY_TYPE = value;
                }
            }
        }

        public bool IsCanceled
        {
            get
            {
                return this._CAN;
            }
            set
            {
                if ((this._CAN != value))
                {
                    this._CAN = value;
                }
            }
        }

        public DateTime PrintDate
        {
            get
            {
                return this._PRINTDATE;
            }
            set
            {
                if ((this._PRINTDATE != value))
                {
                    this._PRINTDATE = value;
                }
            }
        }

        public string Remark
        {
            get
            {
                return this._REMARK;
            }
            set
            {
                if ((this._REMARK != value))
                {
                    this._REMARK = value;
                }
            }
        }

        public int CounterNo
        {
            get
            {
                return this._COUNTER_NO;
            }
            set
            {
                if ((this._COUNTER_NO != value))
                {
                    this._COUNTER_NO = value;
                }
            }
        }

        public int UserNo
        {
            get
            {
                return this._UA_NO;
            }
            set
            {
                if ((this._UA_NO != value))
                {
                    this._UA_NO = value;
                }
            }
        }

        public string CollegeCode
        {
            get
            {
                return this._COLLEGE_CODE;
            }
            set
            {
                if ((this._COLLEGE_CODE != value))
                {
                    this._COLLEGE_CODE = value;
                }
            }
        } 
        #endregion
    }
}