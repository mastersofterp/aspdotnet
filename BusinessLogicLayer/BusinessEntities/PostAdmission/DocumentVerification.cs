using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessEntities.Academic.PoastAdmission
{
    public class DocumentVerification
    {

        private int _ADMBATCH = 0;
        private int _ProgramType = 0;
        private int _DegreeNo = 0;
        private int _UsereNo = 0;
        private int _CreatedBy = 0;
        private int _DocStatus = 0;
        private DateTime _CallDate;
        private string _Calltime = string.Empty;
        private string _Branchno = string.Empty;
        private string _IpAdreess = string.Empty;


        public int ADMBATCH
        {
            get
            {
                return _ADMBATCH;
            }
            set
            {
                _ADMBATCH = value;
            }
        }

        public int ProgramType
        {
            get
            {
                return _ProgramType;
            }
            set
            {
                _ProgramType = value;
            }
        }
        public int DegreeNo
        {
            get
            {
                return _DegreeNo;
            }
            set
            {
                _DegreeNo = value;
            }
        }
        public string Branchno
        {
            get
            {
                return _Branchno;
            }
            set
            {
                _Branchno = value;
            }
        }
        public int USerNo
        {
            get
            {
                return _UsereNo;
            }
            set
            {
                _UsereNo = value;
            }
        }

        public DateTime CallDate
        {
            get
            {
                return _CallDate;
            }
            set
            {
                _CallDate = value;
            }
        }

        public string Calltime
        {
            get
            {
                return _Calltime;
            }
            set
            {
                _Calltime = value;
            }
        }

        public string IpAdreess
        {
            get
            {
                return _IpAdreess;
            }
            set
            {
                _IpAdreess = value;
            }
        }

        public int CreatedBy
        {
            get
            {
                return _CreatedBy;
            }
            set
            {
                _CreatedBy = value;
            }
        }
        public int DocStatus
        {
            get
            {
                return _DocStatus;
            }
            set
            {
                _DocStatus = value;
            }
        }
    }
}
