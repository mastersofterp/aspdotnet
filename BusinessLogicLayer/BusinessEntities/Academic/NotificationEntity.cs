using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System;
using System.Collections;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class NotificationEntity
    {
        #region Private Member

        private int _idno = 0;
        private string _collegeCode = string.Empty;
        private string _ipaddress = string.Empty;
        private DateTime _date = DateTime.MinValue;
        private string _remark = string.Empty;
        private int _uano = 0;
        private int _out = 0;
        private string _Title = string.Empty;
        private string _Details = string.Empty;
        private int _Degreeno = 0;
        private int _Branchno = 0;
        private int _Semesterno = 0;
        private int _Deptno = 0;
        private int _UA_Type = 0;
        private int _NotificationID = 0;
        private string _UserNo = string.Empty;
        private DateTime _Expirydate = DateTime.MinValue;
        private string _regid = string.Empty;

        #endregion

        #region Public Properties

        public string UserNo
        {
            get { return _UserNo; }
            set { _UserNo = value; }
        }

        public int NotificationID
        {
            get { return _NotificationID; }
            set { _NotificationID = value; }
        }

        public int Degreeno
        {
            get { return _Degreeno; }
            set { _Degreeno = value; }
        }

        public int Branchno
        {
            get { return _Branchno; }
            set { _Branchno = value; }
        }

        public int Semesterno
        {
            get { return _Semesterno; }
            set { _Semesterno = value; }
        }

        public int Deptno
        {
            get { return _Deptno; }
            set { _Deptno = value; }
        }

        public int UA_Type
        {
            get { return _UA_Type; }
            set { _UA_Type = value; }
        }


        public int Out
        {
            get { return _out; }
            set { _out = value; }
        }

        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        public string Details
        {
            get { return _Details; }
            set { _Details = value; }
        }

        public int Idno
        {
            get { return _idno; }
            set { _idno = value; }
        }

        public string CollegeCode
        {
            get { return _collegeCode; }
            set { _collegeCode = value; }
        }

        public string Ipaddress
        {
            get { return _ipaddress; }
            set { _ipaddress = value; }
        }
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        public int UANO
        {
            get { return _uano; }
            set { _uano = value; }
        }

        public DateTime Expirydate
        {
            get { return _Expirydate; }
            set { _Expirydate = value; }
        }

        public string RegID
        {
            get { return _regid; }
            set { _regid = value; }
        }


        #endregion
    }
}
