using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class Config
    {
        private double _Fees_GEN = 0.00;
        private double _Fees_SCST = 0.00;
        private DataTable _dtOnlineAdmConfi = null;
        //Added by Nikhil L. on 27/12/2021 to add age for admission configuration.
        private int _age = 0;
        public double Fees_GEN
        {
            get { return _Fees_GEN; }
            set { _Fees_GEN = value; }
        }
        public double Fees_SCST
        {
            get { return _Fees_SCST; }
            set { _Fees_SCST = value; }
        }

        #region Private Member
        //Added By Rishabh on 10/12/2021
        private string _LabelName = string.Empty;
        private string _LabelId = string.Empty;
        private int _ColgId = 0;
        private int _RecId = 0;
        //end
        private int _configNo = 0;
        private string _eventName = string.Empty;
        private int _eventNo = 0;
        private string _status = string.Empty;
        private string _collegeCode = string.Empty;
        private int _ConfigID = 0;
        private int _SessionNo = 0;
        private int _degree_no = 0;
        private int _branchNo = 0;
        private int _Intake = 0;
        private int _SC = 0;
        private int _ST = 0;
        private int _GEN = 0;
        private int _OBC = 0;
        private int _Cnfno = 0;
        private string _Details = string.Empty;
        private DateTime _Config_sdate = DateTime.Now;
        private DateTime _Config_stime = DateTime.Now;
        private DateTime _Config_edate = DateTime.Now;
        private DateTime _Config_etime = DateTime.Now;
        private double _Fees = 0;
        private int _Admbatch = 0;
        private int _College_Id = 0;
        private int _admtype = 0;
        private int _cdbno = 0;
        private int _deptno = 0;
        //Added by Sunita 20-06-2019
        private string _DepartNos = string.Empty;
        private string _DegreeNoS = string.Empty;

        #endregion

        #region Private Property Fields

        //Added by Nikhil L. on 27/12/2021 to add age for admission configuration.
        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                _age = value;
            }
        }
        //Added By Rishabh on 10/12/2021
        public int RecId
        {
            get
            {
                return _RecId;
            }
            set
            {
                _RecId = value;
            }
        }

        public int ColgId
        {
            get
            {
                return _ColgId;
            }
            set
            {
                _ColgId = value;
            }
        }

        public string LabelId
        {
            get
            {
                return _LabelId;
            }
            set
            {
                _LabelId = value;
            }
        }

        public string LabelName
        {
            get
            {
                return _LabelName;
            }
            set
            {
                _LabelName = value;
            }
        }
        //end
        public int AdmType
        {
            get { return _admtype; }
            set { _admtype = value; }
        }
        public int Cdbno
        {
            get { return _cdbno; }
            set { _cdbno = value; }
        }
        public int Deptno
        {
            get { return _deptno; }
            set { _deptno = value; }
        }
        public int ConfigNo
        {
            get { return _configNo; }
            set { _configNo = value; }
        }
        public string EventName
        {
            get { return _eventName; }
            set { _eventName = value; }
        }
        public int EventNo
        {
            get { return _eventNo; }
            set { _eventNo = value; }
        }
        public string CollegeCode
        {
            get { return _collegeCode; }
            set { _collegeCode = value; }
        }
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public int ConfigID
        {
            get { return _ConfigID; }
            set { _ConfigID = value; }
        }
        public int SessionNo
        {
            get { return _SessionNo; }
            set { _SessionNo = value; }
        }
        public int Degree_No
        {
            get { return _degree_no; }
            set { _degree_no = value; }
        }
        public int BranchNo
        {
            get { return _branchNo; }
            set { _branchNo = value; }
        }
        public int Intake
        {
            get { return _Intake; }
            set { _Intake = value; }
        }
        public int SCQuota
        {
            get { return _SC; }
            set { _SC = value; }
        }
        public int STQuota
        {
            get { return _ST; }
            set { _ST = value; }
        }
        public int GENQuota
        {
            get { return _GEN; }
            set { _GEN = value; }
        }
        public int OBCQuota
        {
            get { return _OBC; }
            set { _OBC = value; }
        }
        public int Cnfno
        {
            get { return _Cnfno; }
            set { _Cnfno = value; }
        }


        public string Details
        {
            get { return _Details; }
            set { _Details = value; }
        }
        public DateTime Config_SDate
        {
            get { return _Config_sdate; }
            set { _Config_sdate = value; }
        }
        public DateTime Config_STime
        {
            get { return _Config_stime; }
            set { _Config_stime = value; }
        }
        public DateTime Config_EDate
        {
            get { return _Config_edate; }
            set { _Config_edate = value; }
        }
        public DateTime Config_ETime
        {
            get { return _Config_etime; }
            set { _Config_etime = value; }
        }
        public double Fees
        {
            get { return _Fees; }
            set { _Fees = value; }
        }
        public int Admbatch
        {
            get { return _Admbatch; }
            set { _Admbatch = value; }
        }
        public int College_Id
        {
            get { return _College_Id; }
            set { _College_Id = value; }
        }
        public DataTable dtOnlineAdmConfi
        {
            get { return _dtOnlineAdmConfi; }
            set { _dtOnlineAdmConfi = value; }
        }

        //Added by Sunita 20-06-2019
        public string DepartNos
        {
            get { return DepartNos; }
            set { DepartNos = value; }
        }
        public string DegreeNoS
        {
            get { return _DegreeNoS; }
            set { _DegreeNoS = value; }
        }


        #endregion
    }
}
