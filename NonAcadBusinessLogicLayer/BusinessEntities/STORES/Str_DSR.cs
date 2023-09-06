using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class Str_DSR
    {
        private int _DSRTRNO; // Dead Stock Register Transaction ID
       
        private string _DSRNO;
        private DateTime _DSRDATE;
        private string _AOPURCHASE;
      
        private string _INVOICENO;
        private string _AFINAL;
        private DateTime _DOCREDIT;
        private string _VOUCHERNO;
        private double _AMOUNTREALISED;
        private double _AMOUNTWRITTEN;
        private double _BALANCE;
        private string _REMARKS;
        private int _INVTRNO;
        private int _MDNO;
        private char _FLAG;
        private char _STATUS;


        private int _DEPTNO = 0;
        private int _DSTKNO;  // Dead Stock Register Master ID
        private int _FNO = 0; // Financial Year ID
        private string _DSRNUMBER;
        private DateTime _DOP;
        private DateTime _DSR_DATE;
        private int _PNO;
        private string _REMARKNEW;
        private DataTable  _ITEM_DETAILS;
        private int _USERID = 0;
        private string _COLLEGE_CODE;


        public int DEPTNO
        {
            get { return _DEPTNO; }
            set { _DEPTNO = value; }
        }

        public int DSTKNO
        {
            get { return _DSTKNO;}

            set { if (_DSTKNO != value)
                {
                    _DSTKNO = value;
                }
            }
        }

        public int FNO
        {
            get { return _FNO; }
            set { _FNO = value; }
        }

        public string DSRNUMBER
        {
            get { return _DSRNUMBER; }
            set { _DSRNUMBER = value; }
        }

        public DateTime DOP
        {
            get { return _DOP; }
            set { _DOP = value; }
        }

        public DateTime DSR_DATE
        {
            get { return _DSR_DATE; }
            set { _DSR_DATE = value; }
        }

        public string REMARKNEW
        {
            get { return _REMARKNEW; }
            set { _REMARKNEW = value; }
        }

        public DataTable ITEM_DETAILS
        {
            get { return _ITEM_DETAILS; }
            set { _ITEM_DETAILS = value; }
        }

        public int USERID
        {
            get { return _USERID; }
            set { _USERID = value; }
        }

        public string COLLEGE_CODE
        {
            get { return _COLLEGE_CODE; }
            set { _COLLEGE_CODE = value; }
        }

     

        public int DSRTRNO
        {
            get 
            {
                return _DSRTRNO;
            }
            set 
            {
                if (_DSRTRNO != value)
                {
                    _DSRTRNO = value;
                }
            }
        }
       
        public string DSRNO
        {
            get 
            {
                return _DSRNO;
            }
            set 
            {
                if (_DSRNO != value)
                {
                    _DSRNO = value;
                }
            }
        }
        public DateTime DSRDATE
        {
            get 
            {
                return _DSRDATE;
            }
            set
            {
                if (_DSRDATE != value)
                {
                    _DSRDATE = value;
                }
            }
        }
        public string AOPURCHASE
        {
            get 
            {
                return _AOPURCHASE;
            }
            set 
            {
                if (_AOPURCHASE != value)
                {
                    _AOPURCHASE = value;
                }
            }
        }
        public int PNO
        {
            get 
            {
                return _PNO;
            }
            set 
            {
                if (_PNO != value)
                {
                    _PNO = value;
                }
            }
        }
        public string INVOICENO
        {
            get
            {
                return _INVOICENO;
            }
            set
            {
                if (_INVOICENO != value)
                {
                    _INVOICENO = value;
                }
            }
        }
        public string AFINAL
        {
            get 
            {
                return _AFINAL;
            }
            set 
            {
                if (_AFINAL != value)
                {
                    _AFINAL = value; 
                }
            }
        }
        public string VOUCHERNO
        {
            get 
            {
                return _VOUCHERNO;
            }
            set 
            {
                if (_VOUCHERNO != value)
                {
                    _VOUCHERNO = value;
                }
            }
        }
        public double AMOUNTREALISED
        {
            get 
            {
                return _AMOUNTREALISED;
            }
            set 
            {
                if (_AMOUNTREALISED != value)
                {
                    _AMOUNTREALISED = value;
                }
            }
        }
        public double AMOUNTWRITTEN
        {
            get 
            {
                return _AMOUNTWRITTEN;
            }
            set 
            {
                if (_AMOUNTWRITTEN != value)
                {
                    _AMOUNTWRITTEN = value;
                }
            }
        }
        public DateTime DOCREDIT
        {
            get 
            {
                return _DOCREDIT;
            }
            set 
            {
                if (_DOCREDIT != value)
                {
                    _DOCREDIT = value;
                }
            }
        }
        public double BALANCE
        {
            get 
            {
            return _BALANCE;
            }
            set 
            {
            if ( _BALANCE!= value)
                {
                    _BALANCE = value;
                }
            }
        }
        public string REMARKS
               {
            get 
            {
            return _REMARKS;
            }
            set 
            {
            if ( _REMARKS!= value)
                {
                    _REMARKS = value;
                }
            }
        }
        public int INVTRNO
        {
            get
            {
                return _INVTRNO;
            }
            set
            {
                if (_INVTRNO != value)
                {
                    _INVTRNO = value;
                }
            }
        }
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
        public char FLAG
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
        public char STATUS
        {
            get
            {
                return _STATUS;
            }
            set
            {
                if (_STATUS != value)
                {
                    _STATUS = value;
                }
            }
        }

    }
}
