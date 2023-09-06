using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class CoursesLockUnlock
            {
                #region Private Members
                private int _ID = 0;
                private int _DEGREENO = 0;
                private int _SCHEMENO = 0;
                private int _SEMESTERNO = 0;
                private int _COURSENO = 0;
                private string _CCODE = string.Empty;
                private string _COURSE_NAME = string.Empty;
                private int _COURSE_TYPE = 0;
                private bool _STATUS;
                private int _CREATED_BY = 0;
                private DateTime _CREATED_DATE;
                private int _MODIFIED_BY = 0;
                private DateTime _MODIFIED_DATE;
                private string _COLLEGE_CODE = string.Empty;
                //private int _OrganizationId = 0;
                //private string _IPADDRESS = string.Empty;

                #endregion

                #region Public Property Fields
                public int ID
                {
                    get { return _ID; }
                    set { _ID = value; }
                }

                public int DEGREENO
                {
                    get { return _DEGREENO; }
                    set { _DEGREENO = value; }
                }
                public int SCHEMENO
                {
                    get { return _SCHEMENO; }
                    set { _SCHEMENO = value; }
                }
                public int SEMESTERNO
                {
                    get { return _SEMESTERNO; }
                    set { _SEMESTERNO = value; }
                }
                public int COURSENO
                {
                    get { return _COURSENO; }
                    set { _COURSENO = value; }
                }
               
                public string CCode
                {
                    get { return _CCODE; }
                    set { _CCODE = value; }
                }
                public string COURSE_NAME
                {
                    get { return _COURSE_NAME; }
                    set { _COURSE_NAME = value; }
                }
                public int COURSE_TYPE
                {
                    get { return _COURSE_TYPE; }
                    set { _COURSE_TYPE = value; }
                }

                public bool STATUS
                {
                    get { return _STATUS; }
                    set { _STATUS = value; }
                }

                public int CREATED_BY
                {
                    get { return _CREATED_BY; }
                    set { _CREATED_BY = value; }
                }

                public DateTime CREATED_DATE
                {
                    get { return _CREATED_DATE; }
                    set { _CREATED_DATE = value; }
                }

                public int MODIFIED_BY
                {
                    get { return _MODIFIED_BY; }
                    set { _MODIFIED_BY = value; }
                }
                public DateTime MODIFIED_DATE
                {
                    get { return _MODIFIED_DATE; }
                    set { _MODIFIED_DATE = value; }
                }
                public string COLLEGE_CODE
                {
                    get { return _COLLEGE_CODE; }
                    set { _COLLEGE_CODE = value; }
                }
                
                //public int OrganizationId
                //{
                //    get { return _OrganizationId; }
                //    set { _OrganizationId = value; }
                //}

                //public string IPADDRESS
                //{
                //    get { return _IPADDRESS; }
                //    set { _IPADDRESS = value; }
                //}
                #endregion
            }

        }//END: BusinessLayer.BusinessEntities

    }//END: UAIMS  

}//END: IITMS