//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : HOSTEL
// PAGE NAME     : ROOM ALLOMENT CLASS
// CREATION DATE : 17-AUG-2009
// CREATED BY    : AMIT YADAV
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Collections;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class RoomAllotment
    {
        #region Private Member

        private int _roomAllotmentNo = 0;
        private int _hostelSessionNo = 0;
        private int _roomNo = 0;
        private int _residentTypeNo = 0;
        private int _residentNo = 0;
        private DateTime _allotmentDate = DateTime.MinValue;
        private bool _isCanceled = false;
        private string _remark = string.Empty;
        private int _userNo = 0;
        private int _counterNo = 0;
        private string _collegeCode = string.Empty;
        private string _feeCatNo = string.Empty;

        #endregion

        #region Public Properties

        public int RoomAllotmentNo
        {
            get { return _roomAllotmentNo; }
            set { _roomAllotmentNo = value; }
        }

        public int HostelSessionNo
        {
            get { return _hostelSessionNo; }
            set { _hostelSessionNo = value; }
        }

        public int RoomNo
        {
            get { return _roomNo; }
            set { _roomNo = value; }
        }

        public int ResidentTypeNo
        {
            get { return _residentTypeNo; }
            set { _residentTypeNo = value; }
        }

        public int ResidentNo
        {
            get { return _residentNo; }
            set { _residentNo = value; }
        }

        public DateTime AllotmentDate
        {
            get { return _allotmentDate; }
            set { _allotmentDate = value; }
        }

        public bool IsCanceled
        {
            get { return _isCanceled; }
            set { _isCanceled = value; }
        }

        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        public int UserNo
        {
            get { return _userNo; }
            set { _userNo = value; }
        }

        public int CounterNo
        {
            get { return _counterNo; }
            set { _counterNo = value; }
        }

        public string CollegeCode
        {
            get { return _collegeCode; }
            set { _collegeCode = value; }
        }

        public string FeeCatNo
        {
            get { return _feeCatNo; }
            set { _feeCatNo = value; }
        }
        #endregion

        //ROOM CANCEL 
        private string _ipaddress = string.Empty;
        private decimal _percentage = 0.0m;
        private decimal _hostel_Charge = 0.0m;
        private decimal _perdayCharge = 0.0m;
        private decimal _messCharge = 0.0m;
        private decimal _messMonthCharge = 0.0m;
        private decimal _otherCharge = 0.0m;
        private decimal _total_Amt = 0.0m;
        private decimal _refund_Amt = 0.0m;
        private int _days = 0;
        private string _chequeNo = string.Empty;
        private DateTime _chequeDate = DateTime.MinValue;

        public string IP_Address
        {
            get { return _ipaddress; }
            set { _ipaddress = value; }
        }
        public decimal Percentage
        {
            get { return _percentage; }
            set { _percentage = value; }
        }
        public decimal Hostel_Charge
        {
            get { return _hostel_Charge; }
            set { _hostel_Charge = value; }
        }
        public decimal PerDayCharge
        {
            get { return _perdayCharge; }
            set { _perdayCharge = value; }
        }
        public decimal MessCharge
        {
            get { return _messCharge; }
            set { _messCharge = value; }
        }
        public decimal MessMonthCharge
        {
            get { return _messMonthCharge; }
            set { _messMonthCharge = value; }
        }
        public decimal OtherCharge
        {
            get { return _otherCharge; }
            set { _otherCharge = value; }
        }
        public decimal TotalAmt
        {
            get { return _total_Amt; }
            set { _total_Amt = value; }
        }
        public decimal RefundAmt
        {
            get { return _refund_Amt; }
            set { _refund_Amt = value; }
        }
        public int Days
        {
            get { return _days; }
            set { _days = value; }
        }
        public string ChequeNo
        {
            get { return _chequeNo; }
            set { _chequeNo = value; }
        }
        public DateTime ChequeDate
        {
            get { return _chequeDate; }
            set { _chequeDate = value; }
        }
    }
}