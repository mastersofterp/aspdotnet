using System;
using System.Collections.Generic;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class StudentScholarship
    {
        #region private member
        private int _scholIdNo = 0;
       
        private int _idNo=0;
        private string _regNo = string.Empty;
        private int _degreeNo=0;
        private int _branchNo=0;
        private int _admbatchNo = 0;
        private int _year=0;
        private int _shift = 0;
        private int _categoryNo = 0;
        private int _admcategoryNo = 0;
        private int _concessionNo = 0;
        private string  _concession_reason=string.Empty ;
        private decimal  _Amaount=0;
        private int _billno=0;
        private DateTime _bill_dt = DateTime.Now;
        private int _ua_no=0;
        private string _ip_address=string.Empty ;
        private DateTime _entrydate=DateTime.Today  ;
        private int  _collgecode = 0 ;
        private decimal _refundamt=0;
        private decimal _adjustmentamt=0 ;
        private int _sch_type = 0;
        private string _forwardedto = string.Empty;

        #endregion
    
        #region public property field

        public int  Admcategory_No
        {
            get { return _admcategoryNo; }
            set { _admcategoryNo = value; }
        }

        public int Category_No
        {
            get { return _categoryNo; }
            set { _categoryNo = value; }
        }
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
        public string  Reg_No
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
        public string Concession_Reason
        {
            get { return _concession_reason ; }
            set { _concession_reason  = value; }
        }
        public decimal Amount
        {
            get { return _Amaount  ;}
            set {_Amaount =value  ;}

        }
        public int Bill_No
        {
            get { return _billno; }
            set { _billno =value ;}
        }
        public DateTime Bill_Date
        {
           get { return  _bill_dt; }
            set{ _bill_dt = value ;}
        }
        public  int Ua_No
        {
            get { return _ua_no  ;}
            set { _ua_no = value; }
        }
        public string IP_Address
        {
            get { return _ip_address; }
            set { IP_Address = value; }
        }
        public DateTime Entry_Date
        {
            get { return Entry_Date; }
            set { Entry_Date = value; }
        }
        public int College_Code
        {
            get { return _collgecode; }
            set { _collgecode = value; }
        }
        public decimal Refund_Amount
        {
            get { return _refundamt; }
            set { _refundamt = value; }
        }
        public decimal Adjustment_Amount
        {
            get { return _adjustmentamt; }
            set { _adjustmentamt = value; }
        }
        public int Sch_Type
        {
            get { return _sch_type; }
            set {_sch_type  = value; }
        }
        public string Forwarede_To
        {
            get { return _forwardedto; }
            set { _forwardedto = value; }
        }
        public int Shift_No
        {
            get { return _shift; }
            set { _shift = value; }
        }
     #endregion
    }
}
