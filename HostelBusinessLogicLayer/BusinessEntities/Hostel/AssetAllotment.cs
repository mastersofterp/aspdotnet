//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : HOSTEL
// PAGE NAME     : ASSET ALLOTMENT CLASS
// CREATION DATE : 14-AUG-2009
// CREATED BY    : SANJAY RATNAPARKHI 
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Collections;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class AssetAllotment
    {
        #region Private Method
            private int _assetAllotmentNo = 0;
            private int _roomNo = 0;
            private int _assetNo = 0;
            private int _quantity = 0;
            private DateTime _allotmentDate = DateTime.MinValue;
            private string _allotmentCode = string.Empty;
            private string collegeCode = string.Empty;

            private int _organizationid = 0;
        #endregion
        
        #region Public Property
            public int AssetAllotmentNo
        {
            get { return _assetAllotmentNo; }
            set { _assetAllotmentNo = value; }
        }
            public int RoomNo
        {
            get { return _roomNo; }
            set { _roomNo = value; }
        }
            public int AssetNo
        {
            get { return _assetNo; }
            set { _assetNo = value; }
        }
            public int Quantity
            {
                get { return _quantity; }
                set { _quantity = value; }
            }
            public DateTime AllotmentDate
        {
            get { return _allotmentDate; }
            set { _allotmentDate = value; }
        }
            public string AllotmentCode
        {
            get { return _allotmentCode; }
            set { _allotmentCode = value; }
        }
            public string CollegeCode
        {
            get { return collegeCode; }
            set { collegeCode = value; }
        }

            public int organizationid
            {
                get { return _organizationid; }
                set { _organizationid = value; }
            }
        #endregion
    }
}
