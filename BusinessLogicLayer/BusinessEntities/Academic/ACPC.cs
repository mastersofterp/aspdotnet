using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class ACPC
    {
        #region
        int     _sessionNo   = 0;
        string  _IdNo        = string.Empty;
        int     _semesterN0  = 0;
        int     _degreeNo    = 0;
        string  _recieptCode = string.Empty;
        string  _demandNo        = string.Empty;
        bool    _isCancelled = true;
        bool    _isReconciled = false;
        string _Branch = string.Empty;
        string _remark = string.Empty;
        DateTime challanDate = DateTime.MinValue;
        DateTime printDate = DateTime.MinValue;
        string _amount = string.Empty;
        #endregion

        #region
        public int SessionN0
        {
            get { return _sessionNo; }
            set { _sessionNo = value; }
        }
        public string Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        public string IdNo
        {
            get { return _IdNo; }
            set { _IdNo = value; }
        }

        public int SemesterNo
        {
            get { return _semesterN0; }
            set { _semesterN0 = value; }
        }

        public int DegreeNo
        {
            get { return _degreeNo; }
            set { _degreeNo = value; }
        }

        public string RecieptCode
        {
            get { return _recieptCode; }
            set { _recieptCode = value; }
        }

        public string DemandNo
        {
            get { return _demandNo; }
            set { _demandNo = value; }
        }

        public bool IsCanelled
        {
            get { return _isCancelled; }
            set { _isCancelled = value; }
        }

        public bool IsReconciled
        {
            get { return _isReconciled; }
            set { _isReconciled = value; }
        }

        public DateTime ChallanDate
        {
            get { return challanDate; }
            set { challanDate = value; }
        }

        public DateTime PrintDate
        {
            get { return printDate; }
            set { printDate = value; }
        }

        public string Branch
        {
            get { return _Branch; }
            set { _Branch = value; }
        }

        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        #endregion
    }
}
