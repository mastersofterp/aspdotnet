using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS.UAIMS.BusinessLogicLayer.BusinessEntities
{
    public class Quarter_Occupant
    {
        #region Private Member
        private int _mNO = 0;
        private int _occupantName = 0;
        private DateTime _officeOrder_Dt = DateTime.MinValue;
        private DateTime _allotment_Dt   = DateTime.MinValue;
        private int _qrtType_Id = 0;
        private int _qrt_No = 0;
        private string _material = string.Empty;
        private string _quantity = string.Empty;
        private int _empID = 0;
        private int _QA_ID = 0;
 
        #endregion

        #region Public Member

        public int QA_ID
        {
            get { return _QA_ID; }
            set { _QA_ID = value; }
        }
        public int EmpID
        {
            get { return _empID;  }
            set { _empID = value; }
        }

        public int MNO
        {
            get { return _mNO; }
            set { _mNO = value; }
        }
        public int OccupantName
        {
            get { return _occupantName; }
            set { _occupantName = value; }
        }

        public DateTime OfficeOrder_Dt
        {
            get { return _officeOrder_Dt; }
            set { _officeOrder_Dt = value; }
        }

        public DateTime Allotment_Dt
        {
            get { return _allotment_Dt; }
            set { _allotment_Dt = value; }
        }

        public int QrtType_Id
        {
            get { return _qrtType_Id; }
            set { _qrtType_Id = value; }
        }

        public int Qrt_No
        {
            get { return _qrt_No; }
            set { _qrt_No = value; }
        }

        public string  Material
        {
            get { return _material; }
            set { _material = value; }
        }

        public string Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }
      

        #endregion

    }
}
