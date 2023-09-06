using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessEntities.SCHOLARSHIP
{
    public class StudentScholarshipEntry
    {
        #region private member
        private int _scholIdNo = 0;
        private int _idNo = 0;
        private string  _regNo = string.Empty ;
        private int _degreeNo = 0;
        private int _branchNo = 0;
        private int _admbatchNo = 0;
        private int _year = 0;
        private int _concessionNo = 0;
        private decimal _sch_Amt = 0.0m;
        private int _bill_No = 0;
        private int _ua_No = 0;
        private DateTime _entry_Date = DateTime.Today;
        private int _collge_code = 0 ;
        private decimal _refund_Amt = 0.0m;
        private int _shiftNO = 0;
        private int _categoryNo = 0;
        private DateTime _billdate = DateTime.Today;
        private int _sch_type = 0;
        private DateTime _paydate = DateTime.Today;
        private decimal _payamt = 0.0m;
        private int _sanctioneorderno = 0;
        private int _sanctioneyear = 0;
        private string _particular = string.Empty;
        private decimal _sanction_amt = 0.0m;
        private int _class=0;
        private int _semesterno= 0;
        #endregion

        #region Public Property Fields

        public int ScholId_No
        {
            get { return _scholIdNo; }
            set { _scholIdNo = value; }
        }

        public int Id_No
        {
            get { return _idNo; }
            set { _idNo = value; }
        }
        public string Reg_No
        {
            get { return _regNo; }
            set { _regNo = value; }
        }

        public int Degree_No
        {
            get { return _degreeNo; }
            set { _degreeNo = value; }
        }
        public int Branch_No
        {
            get { return _branchNo; }
            set { _branchNo = value; }
        }
        public int ADMBatch_No
        {
            get { return _admbatchNo; }
            set { _admbatchNo = value; }
        }
        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }
        public int Concession_No
        {
            get { return _concessionNo; }
            set { _concessionNo = value; }
        }
        public decimal Sch_Amount
        {
            get { return _sch_Amt; }
            set { _sch_Amt = value; }
        }
        public int Bill_No
        {
            get { return _bill_No; }
            set { _bill_No = value; }
        }
        public DateTime BillDate
        {
            get { return _entry_Date; }
            set { _entry_Date = value; }
        }
        public int Ua_No
        {
            get { return _ua_No; }
            set { _ua_No = value; }
        }
        public int  College_Code
        {
            get { return _collge_code; }
            set { _collge_code = value; }
        }
        public decimal Refund_Amt
        {
            get { return _refund_Amt; }
            set { _refund_Amt = value; }
        }
        public int Shift_No
        {
            get { return _shiftNO; }
            set { _shiftNO = value; }
        }
        public int Category_No
        {
            get { return _categoryNo; }
            set { _categoryNo = value; }
        }
        public DateTime Bill_Date
        {
            get { return _billdate; }
            set { _billdate = value; }
        }
        public int Sch_Type
        {
            get { return _sch_type; }
            set { _sch_type = value; }
        }
        public DateTime Pay_Date
        {
            get { return _paydate; }
            set { _paydate = value; }
        }
        public decimal PayAmt
        {
            get { return _payamt; }
            set { _payamt = value; }
        }
        public int SantionOrderno
        {
            get { return _sanctioneorderno; }
            set { _sanctioneorderno = value; }
        }
        public int SanctionYear
        {
            get { return _sanctioneyear; }
            set { _sanctioneyear = value; }
        }
        public string Paricular
        {
            get { return _particular; }
            set { _particular = value; }
        }
        public  decimal SanctionAmt
        {
            get { return _sanction_amt; }
            set { _sanction_amt = value; }
        }
        public int Class
        {
            get { return _class; }
            set { _class = value; }
        }
        public int Semesterno
        {
            get { return _semesterno; }
            set { _semesterno = value; }
        }
       
        #endregion
    }
}
