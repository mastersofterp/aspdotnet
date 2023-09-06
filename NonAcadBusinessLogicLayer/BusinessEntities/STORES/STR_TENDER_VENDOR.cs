using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class STR_TENDER_VENDOR
    {
        private int _TVNO;
        private int _TENDERNO;
        private string _NAME ;
        private string _CONTACT ;
        private string _ADDRESS ;
        private string _REMARK ;
        private string _COLLEGE_CODE ;
        private string _EMAIL ;
        private string _VENDORCODE ;
        private string  _CST ;
        private string _BST;
        private char _STATUS;
        private string _TECHSPEC;
        private string _OTHERTECH;

       

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

        public int TENDERNO
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

        public string NAME
        {
            get
            {
                return _NAME;
            }
            set
            {
                if (_NAME != value)
                {
                    _NAME = value;
                }
            }
        }

        public string CONTACT
        {
            get
            {
                return _CONTACT;
            }
            set
            {
                if (_CONTACT != value)
                {
                    _CONTACT = value;
                }
            }
        }

        public string ADDRESS
        {
            get
            {
                return _ADDRESS;
            }
            set
            {
                if (_ADDRESS != value)
                {
                    _ADDRESS = value;
                }
            }
        }

        public string REMARK
        {
            get
            {
                return _REMARK;
            }
            set
            {
                if (_REMARK != value)
                {
                    _REMARK = value;
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

        public string EMAIL
        {
            get
            {
                return _EMAIL;
            }
            set
            {
                if (_EMAIL != value)
                {
                    _EMAIL = value;
                }
            }
        }

        public string VENDORCODE
        {
            get
            {
                return _VENDORCODE;
            }
            set
            {
                if (_VENDORCODE != value)
                {
                    _VENDORCODE = value;
                }
            }
        }

        public string  CST
        {
            get
            {
                return _CST;
            }
            set
            {
                if (_CST != value)
                {
                    _CST = value;
                }
            }
        }

        public string BST
        {
            get
            {
                return _BST;
            }
            set
            {
                if (_BST != value)
                {
                    _BST = value;
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

        public string TECHSPEC
        {
            get
            {
                return _TECHSPEC;
            }
            set
            {
                if (_TECHSPEC != value)
                {
                    _TECHSPEC = value;
                }
            }
        }

        public string OTHERTECH
        {
            get
            {
                return _OTHERTECH;
            }
            set
            {
                if (_OTHERTECH != value)
                {
                    _OTHERTECH = value;
                }
            }
        }
    }
}
