using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelBusinessLogicLayer.BusinessEntities.Hostel
{
    public class BulkRoomAllotment
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

        private int _hostelno = 0;

        private int _blockNo = 0;
        private int _floorNo = 0;
        private int _messNo = 0;
        private long _regno = 0;
        private int _orgid = 0;

        
        private int _sem = 0;

        
        #endregion

        #region Public Properties
        public int Sem
        {
            get { return _sem; }
            set { _sem = value; }
        }
        public int OrgID
        {
            get { return _orgid; }
            set { _orgid = value; }
        }
       
        public long Regno
        {
            get { return _regno; }
            set { _regno = value; }
        }
        public int MessNo
        {
            get { return _messNo; }
            set { _messNo = value; }
        }

        public int HostelNo
        {
            get { return _hostelno; }
            set { _hostelno = value; }
        }
        public int BlockNo
        {
            get { return _blockNo; }
            set { _blockNo = value; }
        }
        public int FloorNo
        {
            get { return _floorNo; }
            set { _floorNo = value; }
        }
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

        public Nullable<System.DateTime> Cancel_date { get; set; }
        #endregion
    }
}
