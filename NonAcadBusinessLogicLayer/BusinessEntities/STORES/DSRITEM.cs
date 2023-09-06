using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class DSRITEM
    {
        private int _DSRINO;
        private int _DSRTRNO;
        private int _ITEM_NO;
        private double _QTY;
        private double _RATE;
        private double _AMT;
        private int _StoreSubDepartmentId;
        private int _DSTKNO;
        private DateTime _ISSUE_DATE;
        private string _ISSUE_REMARK = string.Empty;
        private DataTable _DSRIssueItemsTbl = null;

        private string _COLLEGE_CODE;
        private int _MDNO ;
        private char _STATUS ;
        private int _ISSUENO =0;
        private char _STORE_USER_TYPE;

        public char STORE_USER_TYPE
        {
            get
            {
                return _STORE_USER_TYPE;
            }
            set
            {
                if (_STORE_USER_TYPE != value)
                {
                    _STORE_USER_TYPE = value;
                }
            }
        }

        public int ISSUENO
        {
            get
            {
                return _ISSUENO;
            }
            set
            {
                if (_ISSUENO != value)
                {
                    _ISSUENO = value;
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


        public DataTable DSRIssueItemsTbl
        {
            get
            {
                return _DSRIssueItemsTbl;
            }
            set
            {
                if (_DSRIssueItemsTbl != value)
                {
                    _DSRIssueItemsTbl = value;
                }
            }
        }
        
        
        public string ISSUE_REMARK
        {
            get
            {
                return _ISSUE_REMARK;
            }
            set
            {
                if (_ISSUE_REMARK != value)
                {
                    _ISSUE_REMARK = value;
                }
            }
        }


        public DateTime ISSUE_DATE
        {
            get
            {
                return _ISSUE_DATE;
            }
            set
            {
                if (_ISSUE_DATE != value)
                {
                    _ISSUE_DATE = value;
                }
            }
        }


        public int DSTKNO
        {
            get
            {
                return _DSTKNO;
            }
            set
            {
                if (_DSTKNO != value)
                {
                    _DSTKNO = value;
                }
            }
        }


        public int StoreSubDepartmentId
        {
            get
            {
                return _StoreSubDepartmentId;
            }
            set
            {
                if (_StoreSubDepartmentId != value)
                {
                    _StoreSubDepartmentId = value;
                }
            }
        }

        public int DSRINO 
        {
            get 
            {
                return _DSRINO;
            }
            set 
            {
                if (_DSRINO != value)
                {
                    _DSRINO = value; 
                }
            }
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
        public double QTY
        { 
            get 
            {
                return _QTY;
            }
            set 
            {
                if (_QTY != value)
                {
                    _QTY = value;
                }
            }
        }
        public double RATE 
        {
            get 
            {
                return _RATE;
            }
            set 
            {
                if (_RATE != value)
                {
                    _RATE = value;
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
    }
}
