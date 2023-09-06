using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class STR_INDENT
    {
        private int _INDTRNO;
        private string _INDNO;
        private DateTime _INDSLIP_DATE;
        private string _INDSLIP_NO;
        private string _NAME;
        private string _REMARK;
        private int _GPNO;
        private string _GPREFNO;
        private int _MISDNO;
        private string _STATUS;
        private string _ITEMNO;
        private string _ISREPAIR;
        private string _RECEIVERNAME;
        private string _RECEIVERADDRESS;
        private string _VehicleNo;
        private DateTime _GATEPASSDATE;
        private string _ReqQty;
        private string _TransferQty;
        private int _REQTRNO;

        public int REQTRNO
        {
            get { return _REQTRNO; }
            set { _REQTRNO = value; }
        }

        public string TransferQty
        {
            get { return _TransferQty; }
            set { _TransferQty = value; }
        }

        public string ReqQty
        {
            get { return _ReqQty; }
            set { _ReqQty = value; }
        }
        public DateTime GATEPASSDATE
        {
            get { return _GATEPASSDATE; }
            set { _GATEPASSDATE = value; }
        }
        public int GPNO
        {
            get { return _GPNO; }
            set { _GPNO = value; }
        }
        public string GPREFNO
        {
            get { return _GPREFNO; }
            set { _GPREFNO = value; }
        }
        public string VehicleNo
        {
            get { return _VehicleNo; }
            set { _VehicleNo = value; }
        }

        public string RECEIVERADDRESS
        {
            get { return _RECEIVERADDRESS; }
            set { _RECEIVERADDRESS = value; }
        }

        public string RECEIVERNAME
        {
            get { return _RECEIVERNAME; }
            set { _RECEIVERNAME = value; }
        }
        public string ISREPAIR
        {
            get { return _ISREPAIR; }
            set { _ISREPAIR = value; }
        }

        public string ITEMNO
        {
            get { return _ITEMNO; }
            set { _ITEMNO = value; }
        }

        public int MISDNO
        {
            get { return _MISDNO; }
            set { _MISDNO = value; }
        }
        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

        public int INDTRNO
        {
            get
            {
                return _INDTRNO;
            }
            set
            {
                if (_INDTRNO != value)
                {
                    _INDTRNO = value;
                }
            }
        }
        public string INDNO
        {
            get
            {
                return _INDNO;
            }
            set
            {
                if (_INDNO != value)
                {
                    _INDNO = value;
                }
            }
        }
        public DateTime INDSLIP_DATE
        {
            get
            {
                return _INDSLIP_DATE;
            }
            set
            {
                if (_INDSLIP_DATE != value)
                {
                    _INDSLIP_DATE = value;
                }
            }
        }
        public string INDSLIP_NO
        {
            get
            {
                return _INDSLIP_NO;
            }
            set
            {
                if (_INDSLIP_NO != value)
                {
                    _INDSLIP_NO = value;
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
    }
}
