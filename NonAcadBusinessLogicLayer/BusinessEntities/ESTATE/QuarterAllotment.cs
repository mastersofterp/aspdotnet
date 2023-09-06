using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS.UAIMS.BusinessLogicLayer.BusinessEntities
{
    public class QuarterAllotment
    {

        #region Private Members
        private int _mId = 0;
        private int _employeeType = 0;
        private int _name = 0;
        private int _designation = 0;
        private int _department = 0;
        
        private string  _quarterType = string.Empty;
        private string  _quarterNo = string.Empty;
        private string  _waterMeterNo = string.Empty;
        private string  _eneryMetertye = string.Empty;
        private string  _meterNo = string.Empty;
      
        private string _allotOrderNo = string.Empty;
        private DateTime _offceOrderDt  = DateTime.MinValue;
        private DateTime _allotmentDate = DateTime.MinValue;
        private double _quarterRent = 0;
        private int _waterMeterstatus = 0;
        private int _waterMeterRent = 0;
        private int _gasMeter = 0;
        private int _customerNo = 0;
        private string _watermetertype = string.Empty;
        private int _QID = 0;
        private DateTime _occuDate = DateTime.MinValue;
        private int _TOTAL_MEMBERS = 0;
        private int _QA_ID = 0;

        public string _Remark = string.Empty;

       // private int _waterMeterRent = 0;


        #endregion


        #region Public Member

        public int QA_ID
        {
            get { return _QA_ID; }
            set { _QA_ID = value; }

        }
        public int QID
        {
            get { return _QID; }
            set { _QID = value; }

        }
        public string Watermetertype
        {
            get { return _watermetertype; }
            set { _watermetertype = value; }
        }

        public int MId
        {
            get { return _mId; }
            set { _mId = value; }
        }

        public int EmployeeType
        {
            get { return _employeeType; }
            set { _employeeType = value; }
        }


        public int Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Designation
        {
            get { return _designation; }
            set { _designation = value; }
        }


        public int Department
        {
            get { return _department; }
            set { _department = value; }
        }


        public string  QuarterType
        {
            get { return _quarterType; }
            set { _quarterType = value; }
        }


        public string QuarterNo
        {
            get { return _quarterNo; }
            set { _quarterNo = value; }
        }


        public string WaterMeterNo
        {
            get { return _waterMeterNo; }
            set { _waterMeterNo = value; }
        }

        public string EneryMetertye
        {
            get { return _eneryMetertye; }
            set { _eneryMetertye = value; }
        }

        public string MeterNo
        {
            get { return _meterNo; }
            set { _meterNo = value; }
        }


        public string AllotOrderNo
        {
            get { return _allotOrderNo; }
            set { _allotOrderNo = value; }
        }

        public DateTime OffceOrderDt
        {
            get { return _offceOrderDt; }
            set { _offceOrderDt = value; }
        }


        public DateTime AllotmentDate
        {
            get { return _allotmentDate; }
            set { _allotmentDate = value; }
        }


        public double QuarterRent
        {
            get { return _quarterRent; }
            set { _quarterRent = value; }
        }

        public int WaterMeterstatus
        {
            get { return _waterMeterstatus; }
            set { _waterMeterstatus = value; }
        }

        public int WaterMeterRent
        {
            get { return _waterMeterRent; }
            set { _waterMeterRent = value; }
        }

        public int GasMeter
        {
            get { return _gasMeter; }
            set { _gasMeter = value; }
        }

        public int CustomerNo
        {
            get { return _customerNo; }
            set { _customerNo = value; }
        }
        public DateTime occuDate
        {
            get { return _occuDate; }
            set { _occuDate = value; }
        }
        public int TOTAL_MEMBERS
        {
            get { return _TOTAL_MEMBERS; }
            set { _TOTAL_MEMBERS = value; }
        }


        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }

        #endregion

    }
}
