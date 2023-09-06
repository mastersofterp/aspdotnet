//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HEALTH (Direct Medicine Issue)                        
// CREATION DATE : 21-FEB-2017                                                        
// CREATED BY    : MRUNAL SINGH 
//====================================================================================== 

using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLogicLayer.BusinessEntities.Health
        {
            public class DirectMIssue
            {
                #region Private Members

                #region PRECREPTION

                private int _DRID;

                private int _INO;

                private string _HEIGHT;

                private string _WEIGHT;

                private int _PID;

                private System.Nullable<int> _RPID;

                private int _PRESCNO;

                private System.Nullable<int> _DOPDNO;

                private System.Nullable<int> _ADMNO;

                private string _ITEMNAME;

                private string _QTY;

                private System.Nullable<System.DateTime> _PRESCDT;

                private System.Nullable<System.DateTime> _PRESCTIME;

                private string _DOSES;

                private string _SPINST;

                private System.Nullable<int> _NOOFDAYS;

                private string _IP_ADDRESS;

                private string _MAC_ADDRESS;

                private System.Nullable<System.DateTime> _AUDIT_DATE;

                private string _COLLEGE_CODE;

                private DataTable _MEDICINE_PRES = null;

                #endregion

                #region OPD TRANSACTION

                private int _OPDID;

                private System.Nullable<System.DateTime> _OPDDATE;

                private System.Nullable<System.DateTime> _OPDTIME;

                private string _COMPLAINT;

                private string _FINDING;

                private string _DIAGNOSIS;

                private string _INSTRUCTION;

                private string _REMARK;

                private System.Nullable<System.DateTime> _NEXTDT;

                private System.Nullable<System.DateTime> _NEXTTIME;

                private string _BP;

                private System.Nullable<decimal> _TEMP;

                private string _PULSE;

                private string _RESP;

                private char _PATIENT_CODE;
                private int _DEPENDENTID = 0;
                private string _REFERENCE_BY = string.Empty;
                private string _PATIENT_NAME = string.Empty;
                private char _SEX;
               // private int _AGE = 0;
                private string _AGE = string.Empty;
                private int _TEST_GIVEN = 0;
                private int _ISSUE_CERTIFICATES = 0;
                private int _BLOOD_GROUP = 0;

                private DataTable _TEST_TITLE = null;
                private DataTable _TITLE_CONTENTS = null;

                private DataTable _MEDICINE_ISSUE = null;

                private int _PRESCRIPTION_STATUS = 0;

                public int PRESCRIPTION_STATUS
                {
                    get { return _PRESCRIPTION_STATUS; }
                    set { _PRESCRIPTION_STATUS = value; }
                }

                public DataTable MEDICINE_ISSUE
                {
                    get { return _MEDICINE_ISSUE; }
                    set { _MEDICINE_ISSUE = value; }
                }

                #endregion

                #endregion

                #region Public Properties

                #region PRECREPTION

                public DataTable TEST_TITLE
                {
                    get { return _TEST_TITLE; }
                    set { _TEST_TITLE = value; }
                }

                public DataTable TITLE_CONTENTS
                {
                    get { return _TITLE_CONTENTS; }
                    set { _TITLE_CONTENTS = value; }
                }

                public int ISSUE_CERTIFICATES
                {
                    get { return _ISSUE_CERTIFICATES; }
                    set { _ISSUE_CERTIFICATES = value; }
                }
                //public int AGE
                //{
                //    get { return _AGE; }
                //    set { _AGE = value; }
                //}

                public string AGE
                {
                    get { return _AGE; }
                    set { _AGE = value; }
                }

                public int BLOOD_GROUP
                {
                    get { return _BLOOD_GROUP; }
                    set { _BLOOD_GROUP = value; }
                }
                public char SEX
                {
                    get { return _SEX; }
                    set { _SEX = value; }
                }

                public System.Nullable<int> RPID
                {
                    get { return _RPID; }
                    set { _RPID = value; }
                }
                public string REFERENCE_BY
                {
                    get { return _REFERENCE_BY; }
                    set { _REFERENCE_BY = value; }
                }
                public string PATIENT_NAME
                {
                    get { return _PATIENT_NAME; }
                    set { _PATIENT_NAME = value; }
                }
                public int PID
                {
                    get { return _PID; }
                    set { _PID = value; }
                }
                public int DEPENDENTID
                {
                    get { return _DEPENDENTID; }
                    set { _DEPENDENTID = value; }
                }

                public char PATIENT_CODE
                {
                    get { return _PATIENT_CODE; }
                    set { _PATIENT_CODE = value; }
                }


                public string HEIGHT
                {
                    get { return _HEIGHT; }
                    set { _HEIGHT = value; }
                }

                public int INO
                {
                    get { return _INO; }
                    set { _INO = value; }
                }
                public int TEST_GIVEN
                {
                    get { return _TEST_GIVEN; }
                    set { _TEST_GIVEN = value; }
                }

                public string WEIGHT
                {
                    get { return _WEIGHT; }
                    set { _WEIGHT = value; }
                }

                public int DRID
                {
                    get { return _DRID; }
                    set { _DRID = value; }
                }

                public int PRESCNO
                {
                    get { return _PRESCNO; }
                    set { _PRESCNO = value; }
                }

                public System.Nullable<int> DOPDNO
                {
                    get { return _DOPDNO; }
                    set { _DOPDNO = value; }
                }

                public System.Nullable<int> ADMNO
                {
                    get { return _ADMNO; }
                    set { _ADMNO = value; }
                }

                public string ITEMNAME
                {
                    get { return _ITEMNAME; }
                    set { _ITEMNAME = value; }
                }

                public string QTY
                {
                    get { return _QTY; }
                    set { _QTY = value; }
                }

                public System.Nullable<System.DateTime> PRESCDT
                {
                    get { return _PRESCDT; }
                    set { _PRESCDT = value; }
                }

                public System.Nullable<System.DateTime> PRESCTIME
                {
                    get { return _PRESCTIME; }
                    set { _PRESCTIME = value; }
                }

                public string DOSES
                {
                    get { return _DOSES; }
                    set { _DOSES = value; }
                }

                public string SPINST
                {
                    get { return _SPINST; }
                    set { _SPINST = value; }
                }

                public System.Nullable<int> NOOFDAYS
                {
                    get { return _NOOFDAYS; }
                    set { _NOOFDAYS = value; }
                }

                public DataTable MEDICINE_PRES
                {
                    get { return _MEDICINE_PRES; }
                    set { _MEDICINE_PRES = value; }
                }

                public string IP_ADDRESS
                {
                    get { return _IP_ADDRESS; }
                    set { _IP_ADDRESS = value; }
                }

                public string MAC_ADDRESS
                {
                    get { return _MAC_ADDRESS; }
                    set { _MAC_ADDRESS = value; }
                }

                public System.Nullable<System.DateTime> AUDIT_DATE
                {
                    get { return _AUDIT_DATE; }
                    set { _AUDIT_DATE = value; }
                }

                public string COLLEGE_CODE
                {
                    get { return _COLLEGE_CODE; }
                    set { _COLLEGE_CODE = value; }
                }

                #endregion

                #region OPD TRANSACTION

                public int OPDID
                {
                    get { return _OPDID; }
                    set { _OPDID = value; }
                }

                public System.Nullable<System.DateTime> OPDDATE
                {
                    get { return _OPDDATE; }
                    set { _OPDDATE = value; }
                }

                public System.Nullable<System.DateTime> OPDTIME
                {
                    get { return _OPDTIME; }
                    set { _OPDTIME = value; }
                }

                public string COMPLAINT
                {
                    get { return _COMPLAINT; }
                    set { _COMPLAINT = value; }
                }

                public string FINDING
                {
                    get { return _FINDING; }
                    set { _FINDING = value; }
                }

                public string DIAGNOSIS
                {
                    get { return _DIAGNOSIS; }
                    set { _DIAGNOSIS = value; }
                }

                public string INSTRUCTION
                {
                    get { return _INSTRUCTION; }
                    set { _INSTRUCTION = value; }
                }

                public string REMARK
                {
                    get { return _REMARK; }
                    set { _REMARK = value; }
                }

                public System.Nullable<System.DateTime> NEXTDT
                {
                    get { return _NEXTDT; }
                    set { _NEXTDT = value; }
                }

                public System.Nullable<System.DateTime> NEXTTIME
                {
                    get { return _NEXTTIME; }
                    set { _NEXTTIME = value; }
                }

                public string BP
                {
                    get { return _BP; }
                    set { _BP = value; }
                }

                public System.Nullable<decimal> TEMP
                {
                    get { return _TEMP; }
                    set { _TEMP = value; }
                }

                public string PULSE
                {
                    get { return _PULSE; }
                    set { _PULSE = value; }
                }

                public string RESP
                {
                    get { return _RESP; }
                    set { _RESP = value; }
                }

                #endregion

                #endregion

            }
        }
    }
}
