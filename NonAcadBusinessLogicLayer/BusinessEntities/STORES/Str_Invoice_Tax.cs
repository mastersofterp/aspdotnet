using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class Str_Invoice_Tax
    {
        private int _INVTNO;
        private int _INVTRNO;
        private int _TAXNO;
        private double _AMT;
        private double _PER;
        private int _ITEM_NO;
        private string _FNAME;
        public double _DISCOUNT_AMT;

        //====================
        private string _ITEM_NAME;
        private string _COLLEGE_CODE;
        private double _TAXAMT;



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
        public string ITEM_NAME
        {
            get
            {
                return _ITEM_NAME;
            }
            set
            {
                if (_ITEM_NAME != value)
                {
                    _ITEM_NAME = value;
                }
            }
        }
        public double TAXAMT
        {
            get
            {
                return _TAXAMT;
            }
            set
            {
                if (_TAXAMT != value)
                {
                    _TAXAMT = value;
                }
            }
        }
        //========================
       

        public double DISCOUNT_AMT
        {
            get
            {
                return _DISCOUNT_AMT;
            }
            set
            {
                if (_DISCOUNT_AMT != value)
                {
                    _DISCOUNT_AMT = value;
                }
            }
        }
        public int INVTNO
        {
            get
            {
                return _INVTNO;
            }
            set
            {
                if (_INVTNO != value)
                {
                    _INVTNO = value;
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

        public int TAXNO
        {
            get
            {
                return _TAXNO;
            }
            set
            {
                if (_TAXNO != value)
                {
                    _TAXNO = value;
                }
            }
        }

        public double AMT
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

        public double PERCENTAGE
        {
            get
            {
                return _PER;
            }
            set
            {
                if (_PER != value)
                {
                    _PER = value;
                }
            }
        }
        public string FNAME
        {
            get
            {
                return _FNAME;
            }
            set
            {
                if (_FNAME != value)
                {
                    _FNAME = value;
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
                if (_ITEM_NO != value)
                {
                    _ITEM_NO = value;
                }
            }
        }

    }
}
