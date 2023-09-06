using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class Str_SerialNumber
    {
        private int _INVDINO;

        private string _serialNumber;
        private string _TECH_SPEC;
        private string _QUALITY_QTY_SPEC;
        private string _ITEM_REMARK;

        private System.Nullable<DateTime> _DES_Date;

        //------Shaikh Juned 27-10-2022 Add for Dead Stock Entry form --start
        private DataTable _DEAD_STOCK_ITEM_TBL = null;

        private DateTime __ISSUE_DATE;

        private string _REMARK;
        //------Shaikh Juned 27-10-2022 Add for Dead Stock Entry form --end


        public int INVDINO
        {
            get
            {
                return _INVDINO;
            }
            set
            {
                if (_INVDINO != value)
                {
                    _INVDINO = value;
                }
            }
        }

        public string serialNumber
        {
            get
            {
                return _serialNumber;
            }
            set
            {
                if (_serialNumber != value)
                {
                    _serialNumber = value;
                }
            }
        }

        public string TECH_SPEC
        {
            get
            {
                return _TECH_SPEC;
            }
            set
            {
                if (_TECH_SPEC != value)
                {
                    _TECH_SPEC = value;
                }
            }
        }

        public string QUALITY_QTY_SPEC
        {
            get
            {
                return _QUALITY_QTY_SPEC;
            }
            set
            {
                if (_QUALITY_QTY_SPEC != value)
                {
                    _QUALITY_QTY_SPEC = value;
                }
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
                if (_ITEM_REMARK != value)
                {
                    _ITEM_REMARK = value;
                }
            }
        }

        public System.Nullable<DateTime> DES_Date
        {
            get
            {
                return this._DES_Date;
            }
            set
            {
                if ((this._DES_Date != value))
                {
                    this._DES_Date = value;
                }
            }
        }


       
        //------Shaikh Juned 27-10-2022 Add for Dead Stock Entry form --start
        public DataTable DEAD_STOCK_ITEM_TBL
        {
            get
            {
                return this._DEAD_STOCK_ITEM_TBL;
            }
            set
            {
                if ((this._DEAD_STOCK_ITEM_TBL != value))
                {
                    this._DEAD_STOCK_ITEM_TBL = value;
                }
            }
        }

        public DateTime ISSUE_DATE
        {
            get
            {
                return __ISSUE_DATE;
            }
            set
            {
                if (__ISSUE_DATE != value)
                {
                    __ISSUE_DATE = value;
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

        //----Shaikh Juned 27-10-2022 Add for Dead Stock Entry form --end

    }
}
