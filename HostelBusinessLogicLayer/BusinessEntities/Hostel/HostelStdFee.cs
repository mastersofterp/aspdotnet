using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelBusinessLogicLayer.BusinessEntities.Hostel
{
    public class HostelStdFee
    {
        #region Private Method

        private string _recieptCode = string.Empty;
        private int _degreeNo = 0;
        private int _Session_no = 0;
        private int _Branch_no = 0;
        private int _Hostel_no = 0;
        private int _Hostel_Name = 0;
        private int _bathtype = 0;
        private int _Capacity = 0;
        private int _Room_Type = 0;
        #endregion

        #region Public Property
        public int RoomType
        {
            get { return _Room_Type; }
            set { _Room_Type = value; }
        }

        public int BATH
        {
            get { return _bathtype; }
            set { _bathtype = value; }
        }
        public int CAPACITY
        {
            get { return _Capacity; }
            set { _Capacity = value; }
        }
        public string ReceiptCode
        {
            get { return _recieptCode; }
            set { _recieptCode = value; }
        }

        public int DegreeNo
        {
            get { return _degreeNo; }
            set { _degreeNo = value; }
        }

        public int Session_No
        {
            get { return _Session_no; }
            set { _Session_no = value; }
        }

        public int Branch_No
        {
            get { return _Branch_no; }
            set { _Branch_no = value; }
        }
        public int Hostel_No
        {
            get { return _Hostel_no; }
            set { _Hostel_no = value; }
        }
        public int Hostel_Name
        {
            get { return _Hostel_Name; }
            set { _Hostel_Name = value; }
        }
        #endregion
    }
}
