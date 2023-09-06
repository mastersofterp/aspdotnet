//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : HOSTEL
// PAGE NAME     : ROOM CLASS
// CREATION DATE : 12-AUG-2009
// CREATED BY    : SANJAY RATNAPARKHI AND AMIT YADAV
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Collections;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class Room
    {
        #region Private Member

        private int _roomNo = 0;
        private int _hostelNo = 0;
        private int _blockNo = 0;
        private int _floorNo = 0;
        private int _branchNo = 0;        
        private int _semesterNo = 0;
        private string _roomName = string.Empty;
        private int _residentTypeNo = 0;
        private int _capacity = 0;
        private int _organizationid = 0;
        private int _Roomtype = 0;
        private string _collegeCode = string.Empty;

        #endregion

        #region Public Properties


        public int Roomtype
        {
            get { return _Roomtype; }
            set { _Roomtype = value; }
        }

        public int RoomNo
        {
            get { return _roomNo; }
            set { _roomNo = value; }
        }
        public int HostelNo
        {
            get { return _hostelNo; }
            set { _hostelNo = value; }
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

        public int BranchNo
        {
            get { return _branchNo; }
            set { _branchNo = value; }
        }

        public int SemesterNo
        {
            get { return _semesterNo; }
            set { _semesterNo = value; }
        }

        public string RoomName
        {
            get { return _roomName; }
            set { _roomName = value; }
        }

        public int ResidentTypeNo
        {
            get { return _residentTypeNo; }
            set { _residentTypeNo = value; }
        }

        public int Capacity
        {
            get { return _capacity; }
            set { _capacity = value; }
        }

        public string CollegeCode
        {
            get { return _collegeCode; }
            set { _collegeCode = value; }
        }

        public int organizationid
        {
            get { return _organizationid; }
            set { _organizationid = value; }
        }

        #endregion
    }
}