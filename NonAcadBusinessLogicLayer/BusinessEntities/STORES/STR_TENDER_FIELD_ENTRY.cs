using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class STR_TENDER_FIELD_ENTRY
    {
        private int _TFNO;
        private string _TENDERNO;
        private int _FNO;
        private string _INFO;
        private decimal _AMT;
        private char _C_CALDEPEND_ON;
        private char _P_CALDEPEND_ON;
        private decimal _PERCENTAGE;
        private int _TVNO;

        private DataTable _VENDOR_TAX_TBL;
        public DataTable VENDOR_TAX_TBL
        {
            get
            {
                return _VENDOR_TAX_TBL;
            }
            set
            {
                _VENDOR_TAX_TBL = value;
            }
        }

        public int TFNO
        {
            get
            {
                return _TFNO;
            }
            set
            {
                if (_TFNO != value)
                {
                    _TFNO = value;
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
                if (_TVNO != value)
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
                if (_TENDERNO != value)
                {
                    _TENDERNO = value;
                }
            }
        }
        public int FNO
        {
            get
            {
                return _FNO;
            }
            set
            {
                if (_FNO != value)
                {
                    _FNO = value;
                }
            }
        }
        public string INFO
        {
            get
            {
                return _INFO;
            }
            set
            {
                if (_INFO != value)
                {
                    _INFO = value;
                }
            }
        }
        public decimal AMT
        {
            get
            {
                return _AMT;
            }
            set
            {
                if (_AMT != value)
                {
                    _AMT = value;
                }
            }
        }
        public char C_CALDEPEND_ON
        {
            get
            {
                return _C_CALDEPEND_ON;
            }
            set
            {
                if (_C_CALDEPEND_ON != value)
                {
                    _C_CALDEPEND_ON = value;
                }
            }
        }
        public char P_CALDEPEND_ON
        {
            get
            {
                return _P_CALDEPEND_ON;
            }
            set
            {
                if (_P_CALDEPEND_ON != value)
                {
                    _P_CALDEPEND_ON = value;
                }
            }
        }
        public decimal PERCENTAGE
        {
            get
            {
                return _PERCENTAGE;
            }
            set
            {
                if (_PERCENTAGE != value)
                {
                    _PERCENTAGE = value;
                }
            }
        }
    }
}
