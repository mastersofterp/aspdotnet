using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
   public class STR_TENDER_ITEM_ENTRY
    {
        private int _TINO;
        private int _QTY;
        private int _TVNO;
        private int _UNIT;
        private string _TENDERNO;
        private int _ITEM_NO;
        private decimal _PRICE;
        private string _MANUFACTURE;
        private string _MODELNO;
        private string _TECHSPEC;
        private string _CURRENCY;
        private string _ITEMDETAIL;
        private string _COLLEGE_CODE;
        private string _OTHERSPEC;
        private int _TECHNO;
        private decimal _TAXPER;
        private decimal _TOTALAMT;
        private decimal _TAXAMT;

        private decimal _TAXABLE_AMT;
        private string _ITEM_REMARK;
        private string _QUALITY_QTY_SPEC;
        private string _FLAG;
        private decimal _DISCOUNT;
        private DateTime _EDATE;
        private int _MDNO;

        

        public int MDNO
        {
            get
            {
                return _MDNO;
            }
            set
            {
                if (_MDNO != value)
                {
                    _MDNO = value;
                }
            }
        }
        public string FLAG
        {
            get
            {
                return _FLAG;
            }
            set
            {
                if (_FLAG != value)
                {
                    _FLAG = value;
                }
            }
        }
        public DateTime EDATE
        {
            get
            {
                return _EDATE;
            }
            set
            {
                if (_EDATE != value)
                {
                    _EDATE = value;
                }
            }
        }
        public decimal DISCOUNT
        {
            get { return _DISCOUNT; }
            set { _DISCOUNT = value; }
        }
        private decimal _DISCOUNTAMOUNT;

        public decimal DISCOUNTAMOUNT
        {
            get { return _DISCOUNTAMOUNT; }
            set { _DISCOUNTAMOUNT = value; }
        }

        public string QUALITY_QTY_SPEC
        {
            get
            {
                return _QUALITY_QTY_SPEC;
            }
            set
            {
                _QUALITY_QTY_SPEC = value;
            }
        }
        public string ITEM_REMARK
        {
            get
            {
                return _ITEM_REMARK;
            }
            set
            {
                _ITEM_REMARK = value;
            }
        }
        public decimal TAXABLE_AMT
        {
            get
            {
                return _TAXABLE_AMT;
            }
            set
            {
                _TAXABLE_AMT = value;
            }
        }

        public decimal TAXPER
        {
            get { 
                return _TAXPER; 
            }
            set {
                _TAXPER = value;
            }
        }
       

        public decimal TAXAMT
        {
            get { 
                return _TAXAMT; 
            }
            set {
                _TAXAMT = value;
            }
        }
       

        public decimal TOTALAMT
        {
            get {
                return _TOTALAMT;
            }
            set {
                _TOTALAMT = value;
            }
        }

        public int TINO
        {
            get
            {
                return _TINO;
            }
            set
            {
                if (_TINO!= value)
                {
                    _TINO = value;
                }
            }
        }
        public int UNIT
        {
            get
            {
                return _UNIT;
            }
            set
            {
                if (_UNIT != value)
                {
                    _UNIT = value;
                }
            }
        }
 public int QTY 
        {
            get
            {
                return _QTY;
            }
            set
            {
                if (_QTY!= value)
                {
                    _QTY= value;
                }
            }
        }
 public int TVNO
        {
            get
            {
                return _TVNO;
            }
            set
            {
                if (_TVNO!= value)
                {
                    _TVNO = value;
                }
            }
        }
 public string TENDERNO
        {
            get
            {
                return _TENDERNO;
            }
            set
            {
                if (_TENDERNO!= value)
                {
                    _TENDERNO = value;
                }
            }
        }
 public int ITEM_NO
        {
            get
            {
                return _ITEM_NO;
            }
            set
            {
                if (_ITEM_NO!= value)
                {
                    _ITEM_NO = value;
                }
            }
        }
 public decimal PRICE
        {
            get
            {
                return _PRICE;
            }
            set
            {
                if (_PRICE!= value)
                {
                    _PRICE = value;
                }
            }
        }
 public string MANUFACTURE
        {
            get
            {
                return _MANUFACTURE;
            }
            set
            {
                if (_MANUFACTURE!= value)
                {
                    _MANUFACTURE = value;
                }
            }
        }
 public string MODELNO
        {
            get
            {
                return _MODELNO;
            }
            set
            {
                if (_MODELNO!= value)
                {
                    _MODELNO = value;
                }
            }
        }
 public string TECHSPEC 
        {
            get
            {
                return _TECHSPEC;
            }
            set
            {
                if (_TECHSPEC!= value)
                {
                    _TECHSPEC = value;
                }
            }
        }
 public string CURRENCY
        {
            get
            {
                return _CURRENCY;
            }
            set
            {
                if (_CURRENCY!= value)
                {
                    _CURRENCY = value;
                }
            }
        }
 public string ITEMDETAIL
        {
            get
            {
                return _ITEMDETAIL;
            }
            set
            {
                if (_ITEMDETAIL!= value)
                {
                    _ITEMDETAIL = value;
                }
            }
        }
 public string COLLEGE_CODE
        {
            get
            {
                return _COLLEGE_CODE;
            }
            set
            {
                if (_COLLEGE_CODE != value)
                {
                    _COLLEGE_CODE = value;
                }
            }
        }
 public int TECHNO
 {
     get
     {
         return _TECHNO;
     }
     set
     {
         if (_TECHNO != value)
         {
             _TECHNO = value;
         }
     }
 }
 public string OTHERSPEC
 {
     get
     {
         return _OTHERSPEC;
     }
     set
     {
         if (_OTHERSPEC != value)
         {
             _OTHERSPEC = value;
         }
     }
 }
        

    }
}
