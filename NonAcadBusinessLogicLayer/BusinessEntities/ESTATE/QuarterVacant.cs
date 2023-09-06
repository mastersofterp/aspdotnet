using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS.UAIMS.BusinessLogicLayer.BusinessEntities
{
    public class QuarterVacant
    {

        #region Private Members
        private int _mNO = 0;
        private int _nameOfVacator = 0;
        private int _quarterType = 0;
        private int _quarterNo = 0;
        private string _vacationOrderNo = string.Empty;
        private DateTime _vacationDT = DateTime.MinValue;
        private DateTime _offceOrdetDt = DateTime.MinValue;
        private string _materialId = string.Empty;
        private string _alloterQTY = string.Empty;
        private string _shortQTY = string.Empty;
        private string _fine = string.Empty;
        private string _fine_Remark = string.Empty;
        private int _empID = 0;

        #endregion

        #region Public Members

        public int MNO
        {
            get { return _mNO; }
            set { _mNO = value; }
        }

        public int NameOfVacator
        {
            get { return _nameOfVacator; }
            set { _nameOfVacator = value; }
        }


        public int QuarterType
        {
            get { return _quarterType; }
            set { _quarterType = value; }
        }


        public int QuarterNo
        {
            get { return _quarterNo; }
            set { _quarterNo = value; }
        }


        public string VacationOrderNo
        {
            get { return _vacationOrderNo; }
            set { _vacationOrderNo = value; }
        }


        public DateTime VacationDT
        {
            get { return _vacationDT; }
            set { _vacationDT = value; }
        }


        public DateTime OffceOrdetDt
        {
            get { return _offceOrdetDt; }
            set { _offceOrdetDt = value; }
        }


        public string MaterialId
        {
            get { return _materialId; }
            set { _materialId = value; }
        }


        public string AlloterQTY
        {
            get { return _alloterQTY; }
            set { _alloterQTY = value; }
        }


        public string ShortQTY
        {
            get { return _shortQTY; }
            set { _shortQTY = value; }
        }


        public string Fine
        {
            get { return _fine; }
            set { _fine = value; }
        }


        public string Fine_Remark
        {
            get { return _fine_Remark; }
            set { _fine_Remark = value; }
        }

        public int EmpID
        {
            get { return _empID; }
            set { _empID = value; }
        }

        #endregion

    }
}
