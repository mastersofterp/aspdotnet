using System;
using System.Data;
using System.Web;

namespace BusinessLogicLayer.BusinessEntities.ITLE
{
    public  class Request_AllowingRetest
    {
        #region private
        private string _session = string.Empty;
        private string _stud_name = string.Empty;      
        private string _rolno = string.Empty;               
        private string _dept = string.Empty;        
        private string _year = string.Empty;        
        private int _courseno = 0;        
        private string _coursename = string.Empty;        
        private string _subtype = string.Empty;        
        private decimal _ltp = 0;        
        private decimal _credits = 0;        
        private string _testname = string.Empty;        
        private DateTime _appdate;
        #endregion 

        #region public
        public string Session
        {
            get { return _session; }
            set { _session = value; }
        }
        public string Stud_name
        {
            get { return _stud_name; }
            set { _stud_name = value; }
        }
        public string Rolno
        {
            get { return _rolno; }
            set { _rolno = value; }
        }
        public string Dept
        {
            get { return _dept; }
            set { _dept = value; }
        }
        public string Year
        {
            get { return _year; }
            set { _year = value; }
        }
        public int Courseno
        {
            get { return _courseno; }
            set { _courseno = value; }
        }
        public string Coursename
        {
            get { return _coursename; }
            set { _coursename = value; }
        }
        public string Subtype
        {
            get { return _subtype; }
            set { _subtype = value; }
        }
        public decimal Ltp
        {
            get { return _ltp; }
            set { _ltp = value; }
        }
        public decimal Credits
        {
            get { return _credits; }
            set { _credits = value; }
        }
        public string Testname
        {
            get { return _testname; }
            set { _testname = value; }
        }
        public DateTime Appdate
        {
            get { return _appdate; }
            set { _appdate = value; }
        }
        #endregion
    }
}



