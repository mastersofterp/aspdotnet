using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class CourseRegistrationApproveByAdvisorEntity
            {
                #region Private Method

                private int _Sessionno;
                private int _CollegeId;
                private int _Degreeno;
                private int _Branchno;
                private int _Semesterno;
                private int _UA_NO;
                private int _IDNO;
                private int _EntryType;
                private string _IPAddress;

                private DataTable _dtStudCourseReg = null;

                #endregion Private Method

                #region Public Method

                public int Sessionno
                {
                    get { return _Sessionno; }
                    set { _Sessionno = value; }
                }

                public int CollegeId
                {
                    get { return _CollegeId; }
                    set { _CollegeId = value; }
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

                public int UA_NO
                {
                    get { return _UA_NO; }
                    set { _UA_NO = value; }
                }

                public int IDNO
                {
                    get { return _IDNO; }
                    set { _IDNO = value; }
                }

                public int EntryType
                {
                    get { return _EntryType; }
                    set { _EntryType = value; }
                }

                public string IPAddress
                {
                    get { return _IPAddress; }
                    set { _IPAddress = value; }
                }

                public DataTable dtStudCourseReg
                {
                    get { return _dtStudCourseReg; }
                    set { _dtStudCourseReg = value; }
                }

                #endregion Public Method
            }
        }
    }
}