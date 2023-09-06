//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HEALTH                         
// CREATION DATE : 13-FEB-2016                                                        
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
            public class Health
            {
                #region Private Members

                #region Doctor Master

                private int _DRID;

                private string _DRNAME;

                private string _DEGREE;

                private string _DESIG;

                private string _ADDRESS;

                private string _PHONE;

                private string _HOSPITALNAME;

                private string _HADDRESS;

                private string _HPHONE;

                private System.Nullable<char> _STATUS;

                private string _IP_ADDRESS;

                private string _MAC_ADDRESS;

                private System.Nullable<System.DateTime> _AUDIT_DATE;

                private string _COLLEGE_CODE;

                private string _EMP_CODE = string.Empty;

                private int _EMP_IDNO = 0;

                #endregion

                #region Items

                private int _INO;

                private string _INAME;

                private string _UNIT;

                private string _IDETAILS;

                private System.Nullable<int> _SGID;

                private System.Nullable<int> _REORDERQTY;

                private System.Nullable<int> _OBQTY;

                private System.Nullable<int> _OBVALUE;

                private System.Nullable<char> _REFERRED;

                private System.Nullable<int> _MEDTYPENO;

                private System.Nullable<decimal> _PRICE;

                #endregion

                #region Patient Master

                private int _PID;

                private System.Nullable<int> _RPID;

                private string _RELATION;

                private string _CARDNO;

                private System.Nullable<int> _TYPENO;

                private string _REGNO;

                private string _NAME;

                private System.Nullable<char> _SEX;

                private string _DESIGNATION;

                private string _DEPTNO;

                private System.DateTime _DOB;

                private System.Nullable<int> _MARRIED;

                private string _CITY;

                private string _STATE;

                private string _HOSTEL;

                private string _HBLOCK;

                private string _HROOM;

                private string _EMAIL;

                private System.Nullable<int> _BLOODGRNO;

                private string _HEIGHT;

                private string _WEIGHT;

                private string _HABITS;

                private string _FAMILYHIS;

                private System.Nullable<int> _AGE;

                private System.Nullable<int> _AGEAPRX;

                private System.Nullable<int> _IDNO;

                #endregion

                #region SUBGROUP

                private string _SGNAME;

                private int _MGID;

                #endregion

                #region Certificate
                private int _CI_ID = 0;
                private DateTime _CI_DATE ;
                private string _REMARK = string.Empty;
                private int _CERTI_NAME_ID = 0;
                private char _P_CODE;


                public int CI_ID
                {
                    get { return _CI_ID; }
                    set { _CI_ID = value; }
                }

                 public char P_CODE
                {
                    get { return _P_CODE; }
                    set { _P_CODE = value; }
                }

                public DateTime CI_DATE
                {
                    get { return _CI_DATE; }
                    set { _CI_DATE = value; }
                }

                public string REMARK
                {
                    get { return _REMARK; }
                    set { _REMARK = value; }
                }

                public int CERTI_NAME_ID
                {
                    get { return _CERTI_NAME_ID; }
                    set { _CERTI_NAME_ID = value; }
                }

                #endregion

                #region Generate Certificate


                private int _CER_ID = 0;
                private int _CER_NO = 0;
                private string _SignGovt = string.Empty;
                private string _PatientName = string.Empty;
                private string _DrName = string.Empty;
                private string _SufferingFrom = string.Empty;
                private string _AbsenceDays = string.Empty;
                private DateTime _FromDate = DateTime.MinValue ;
                private DateTime _IssuedDate = DateTime.MinValue ;
                private string _AuthorizedMed = string.Empty;
                private string _Department = string.Empty;
                private string _PostOf = string.Empty;
                private string _Age = string.Empty;
                private string _AgeAppreance = string.Empty;
                private string _ReferTo = string.Empty;
                private string _Expenditure = string.Empty;
                private string _BLOODGP_NAME = string.Empty;


                public int CER_ID
                {
                    get { return _CER_ID; }
                    set { _CER_ID = value; }
                }
                public int CER_NO
                {
                    get { return _CER_NO; }
                    set { _CER_NO = value; }
                }
                public string SignGovt
                {
                    get { return _SignGovt; }
                    set { _SignGovt = value; }
                }
                public string PatientName
                {
                    get { return _PatientName; }
                    set { _PatientName = value; }
                }
                public string DrName
                {
                    get { return _DrName; }
                    set { _DrName = value; }
                }
                public string SufferingFrom
                {
                    get { return _SufferingFrom; }
                    set { _SufferingFrom = value; }
                }
                public string AbsenceDays
                {
                    get { return _AbsenceDays; }
                    set { _AbsenceDays = value; }
                }
                public DateTime FromDate
                {
                    get { return _FromDate; }
                    set { _FromDate = value; }
                }
                public DateTime IssuedDate
                {
                    get { return _IssuedDate; }
                    set { _IssuedDate  = value; }
                }
                public string AuthorizedMed
                {
                    get { return _AuthorizedMed; }
                    set { _AuthorizedMed = value; }
                }
                public string Department
                {
                    get { return _Department; }
                    set { _Department = value; }
                }
                public string PostOf
                {
                    get { return _PostOf; }
                    set { _PostOf = value; }
                }
                public string Age
                {
                    get { return _Age; }
                    set { _Age = value; }
                }
                public string AgeAppreance
                {
                    get { return _AgeAppreance; }
                    set { _AgeAppreance = value; }
                }
                public string ReferTo
                {
                    get { return _ReferTo; }
                    set { _ReferTo = value; }
                }
                public string Expenditure
                {
                    get { return _Expenditure; }
                    set { _Expenditure = value; }
                }

                public string BLOODGP_NAME
                {
                    get { return _BLOODGP_NAME; }
                    set { _BLOODGP_NAME = value; }
                }


                #endregion

                #region DosageMaster

                private int _DNO = 0;
                private string _DOSAGENAME = string.Empty;
                private string _DOSAGEQUANTITY = string.Empty;
                #endregion

                #endregion

                #region Public Properties

                #region Doctor Master

                public int DRID
                {
                    get { return _DRID; }
                    set { _DRID = value; }
                }

                public int EMP_IDNO
                {
                    get { return _EMP_IDNO; }
                    set { _EMP_IDNO = value; }
                }

                public string DRNAME
                {
                    get { return _DRNAME; }
                    set { _DRNAME = value; }
                }

                public string DEGREE     
                {
                    get { return _DEGREE; }
                    set { _DEGREE = value; }
                }
                public string EMP_CODE     
                {
                    get { return _EMP_CODE; }
                    set { _EMP_CODE = value; }
                }

                public string DESIG
                {
                    get { return _DESIG; }
                    set { _DESIG = value; }
                }

                public string ADDRESS
                {
                    get { return _ADDRESS; }
                    set { _ADDRESS = value; }
                }

                public string PHONE
                {
                    get { return _PHONE; }
                    set { _PHONE = value; }
                }

                public string HOSPITALNAME
                {
                    get { return _HOSPITALNAME; }
                    set { _HOSPITALNAME = value; }
                }

                public string HADDRESS
                {
                    get { return _HADDRESS; }
                    set { _HADDRESS = value; }
                }

                public string HPHONE
                {
                    get { return _HPHONE; }
                    set { _HPHONE = value; }
                }

                public System.Nullable<char> STATUS
                {
                    get { return _STATUS; }
                    set { _STATUS = value; }
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

                #region Items

                public int INO
                {
                    get { return _INO; }
                    set { _INO = value; }
                }

                public string INAME
                {
                    get { return _INAME; }
                    set { _INAME = value; }
                }

                public string UNIT
                {
                    get { return _UNIT; }
                    set { _UNIT = value; }
                }

                public string IDETAILS
                {
                    get { return _IDETAILS; }
                    set { _IDETAILS = value; }
                }

                public System.Nullable<int> SGID
                {
                    get { return _SGID; }
                    set { _SGID = value; }
                }

                public System.Nullable<int> REORDERQTY
                {
                    get { return _REORDERQTY; }
                    set { _REORDERQTY = value; }
                }

                public System.Nullable<int> OBQTY
                {
                    get { return _OBQTY; }
                    set { _OBQTY = value; }
                }

                public System.Nullable<int> OBVALUE
                {
                    get { return _OBVALUE; }
                    set { _OBVALUE = value; }
                }

                public System.Nullable<char> REFERRED
                {
                    get { return _REFERRED; }
                    set { _REFERRED = value; }
                }

                public System.Nullable<int> MEDTYPENO
                {
                    get { return _MEDTYPENO; }
                    set { _MEDTYPENO = value; }
                }

                public System.Nullable<decimal> PRICE
                {
                    get { return _PRICE; }
                    set { _PRICE = value; }
                }

                #endregion

                #region Patient Master

                public int PID
                {
                    get { return _PID; }
                    set { _PID = value; }
                }

                public System.Nullable<int> RPID
                {
                    get { return _RPID; }
                    set { _RPID = value; }
                }

                public string RELATION
                {
                    get { return _RELATION; }
                    set { _RELATION = value; }
                }

                public string CARDNO
                {
                    get { return _CARDNO; }
                    set { _CARDNO = value; }
                }

                public System.Nullable<int> TYPENO
                {
                    get { return _TYPENO; }
                    set { _TYPENO = value; }
                }

                public string REGNO
                {
                    get { return _REGNO; }
                    set { _REGNO = value; }
                }

                public string NAME
                {
                    get { return _NAME; }
                    set { _NAME = value; }
                }

                public System.Nullable<char> SEX
                {
                    get { return _SEX; }
                    set { _SEX = value; }
                }

                public string DESIGNATION
                {
                    get { return _DESIGNATION; }
                    set { _DESIGNATION = value; }
                }

                public string DEPTNO
                {
                    get { return _DEPTNO; }
                    set { _DEPTNO = value; }
                }

                public System.DateTime DOB
                {
                    get { return _DOB; }
                    set { _DOB = value; }
                }

                public System.Nullable<int> MARRIED
                {
                    get { return _MARRIED; }
                    set { _MARRIED = value; }
                }

                public string CITY
                {
                    get { return _CITY; }
                    set { _CITY = value; }
                }

                public string STATE
                {
                    get { return _STATE; }
                    set { _STATE = value; }
                }

                public string HOSTEL
                {
                    get { return _HOSTEL; }
                    set { _HOSTEL = value; }
                }

                public string HBLOCK
                {
                    get { return _HBLOCK; }
                    set { _HBLOCK = value; }
                }

                public string HROOM
                {
                    get { return _HROOM; }
                    set { _HROOM = value; }
                }

                public string EMAIL
                {
                    get { return _EMAIL; }
                    set { _EMAIL = value; }
                }

                public System.Nullable<int> BLOODGRNO
                {
                    get { return _BLOODGRNO; }
                    set { _BLOODGRNO = value; }
                }

                public string HEIGHT
                {
                    get { return _HEIGHT; }
                    set { _HEIGHT = value; }
                }

                public string WEIGHT
                {
                    get { return _WEIGHT; }
                    set { _WEIGHT = value; }
                }

                public string HABITS
                {
                    get { return _HABITS; }
                    set { _HABITS = value; }
                }

                public string FAMILYHIS
                {
                    get { return _FAMILYHIS; }
                    set { _FAMILYHIS = value; }
                }

                public System.Nullable<int> AGE
                {
                    get { return _AGE; }
                    set { _AGE = value; }
                }

                public System.Nullable<int> AGEAPRX
                {
                    get { return _AGEAPRX; }
                    set { _AGEAPRX = value; }
                }

                public System.Nullable<int> IDNO
                {
                    get { return _IDNO; }
                    set { _IDNO = value; }
                }

                #endregion

                #region SUBGROUP

                public string SGNAME
                {
                    get { return _SGNAME; }
                    set { _SGNAME = value; }
                }

                public int MGID
                {
                    get { return _MGID; }
                    set { _MGID = value; }
                }

                #endregion

                #region DosageMaster

                public int DNO
                {
                    get { return _DNO; }
                    set { _DNO = value; }
                }

                public string DOSAGENAME
                {
                    get { return _DOSAGENAME; }
                    set { _DOSAGENAME = value; }
                }

                public string DOSAGEQUANTITY
                {
                    get { return _DOSAGEQUANTITY; }
                    set { _DOSAGEQUANTITY = value; }
                }

                #endregion

                #endregion


            }
        }
    }
}
