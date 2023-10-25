using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class Leaves
            {

                #region Private members

                //=========EL ENCASHMENT
                private System.Nullable<bool> _IsActive;
                private double _BALANCE;
                private int _CREATEDBY;
                private int _MODIFIEDBY;
                private string _IPADDRESS;
                private double _TDAY;

                //=========EL ENCASHMENT APPROVE
                private int _APPROVEDBY;




                private System.Nullable<bool> _IsAllowBeforeApplication;
                private string _SHIFT_INTIME;
                private System.Nullable<bool> _ISFULLDAYLEAVE;
                private string _PERIOD_NAME;
                private int _PERIOD_FROM;
                private int _PERIOD_TO;

                private int _COLLEGE_NO;
                private int _SESSION_SRNO;

                //EngagedFaculty

                private int _PROGRAM_NO;
                private int _TRANNO;
                private int _PERIODNO;
                private string _YEARSEM;
                private string _SUBJECT;
                private int _THEORY;
                private string _FACULTY_NAME;
                private int _FACULTYNO;
                private DateTime _ENGAGED_DATE;
                ////////////
                //[Table(Name="PAY_LEAVE")]	
                private int _LNO;
                private System.Nullable<int> _STNO;
                private System.Nullable<int> _APPOINT_NO;
                private string _LEAVENAME;
                private string _LEAVE;
                private double _MAX;
                private System.Nullable<bool> _CARRY;
                private System.Nullable<int> _PERIOD;
                private System.Nullable<char> _SEX;
                private double _CAL;
                private string _COLLEGE_CODE;

                //[TABLE(namespace="PAY_HOLIDAYS"]
                private int _HNO;
                private string _HOLIDAYNAME;
                private DateTime _HDT;
                private int _YEAR;
                //private int _HPERIOD;

                //[TABLE NAME="PAY_LEAVE_PASSING_AUTHORITY"]
                private int _PANO;
                private string _PANAME;
                private int _UANO;

                //[TABLE NAME="PAY_LEAVE_PASSING_AUTHORITY_PATH"]
                private int _PAPNO;
                private int _PAN01;
                private int _PAN02;
                private int _PAN03;
                private int _PAN04;
                private int _PAN05;
                private string _PAPATH;
                private int _DEPTNO;

                //[TABLE NAME="PAY_LEAVE_APP_ENTRY"]
                private int _LETRNO;
                private int _EMPNO;
                private DateTime _APPDT;
                private DateTime _FROMDT;
                private DateTime _TODT;
                private string _LeaveTime;
                private double _NO_DAYS;
                private DateTime _JOINDT;
                private System.Nullable<bool> _FIT;
                private System.Nullable<bool> _UNFIT;
                private System.Nullable<bool> _FNAN;
                private string _REASON;
                private string _STATUS;
                //[TABLE NAME="PAY_LEAVE_APP_PASS_ENTRY"]
                private int _LAPENO;
                private string _APP_REMARKS;
                private string _APP_ADDRESS;
                private int _PREFIX;
                private int _SUFFIX;
                private string _CHARGEHANDED;
                private string _ENTRY_STATUS;
                private int _DAYS;
                private int _HTNO;
                private string _HOLIDAYTYPE;

                private int _WTNO;
                private string _WORKTYPE;
                private int _LEAVENO;
                private string _LEAVENAMESHRT;
                private string _SHORTNAME;
                private double _MAXDAYS;
                private System.Nullable<int> _YEARLYP;
                private string _HALFYEARLYP;
                private int _CALHOLIDAY;

                private string _EMPNAME;
                private string _INTIME;
                private string _OUTTIME;
                private DateTime _DATE;
                private string _HOURS;
                private string _LATEBY;
                private int _LTANO;
                private string _SHIFTOUTTIME;
                private string _LEAVETYPE;
                private double _LEAVENO1;
                private int _RESNO;

                private int _LSNO;
                private int _LEAVE01;
                private int _LEAVE02;
                private int _LEAVE03;
                private int _LEAVE04;
                private int _LEAVE05;
                private string _LEAVESEQ;
                private string _LEAVESTATUS;
                private int _ENO;
                private System.Nullable<bool> _LEAVEFNAN;
                private string _LVCANCELRMARK;
                private int _MLHPL;
                private string _ATTEMONTHNAME;
                private int _ST;

                private string _PROGRAM_TYPE;
                // PAYROLL_DETENTION_EL
                private int _SUBDESIGNO;
                private double _ELNOOFDAYS;
                private double _ELTOCREDIT;
                private string _ELYEAR;

                //Service Based Leave
                private int _SESSION_SERVICE_SRNO;
                private System.Nullable<bool> _SERVICE_PERIOD_STATUS;
                private int _SERVICE_LIMIT;
                private int _SERVICE_COMPLETE_LIMIT;
                private int _MAXPREDATED;
                private int _MAXPOSTDATED;
                private int _PDMAXDAYS;
                private int _PDLMAXDAYS;

                private int _MAXOCCASIONPD;
                private int _MAXOCCASIONPDL;
                private string _OCCPERIODPD;
                private string _OCCPERIODPDL;


                private string _ANREASON;
                private System.Nullable<bool> _IsPayLeave;
                private System.Nullable<bool> _IsCertificate;
                private System.Nullable<bool> _IsWithCertificate;

                private string _FileName = string.Empty;
                private string _FilePath = string.Empty;
                private decimal _FileSize = 0;

                private string _LType = string.Empty;

                private System.Nullable<bool> __IsCreditSlotWise;
                private int _SlotOFDays;
                private double _LeaveCreditAfterSlot;

                private System.Nullable<DateTime> _ALLOTEDDATE;
                private string _CREDITDT = string.Empty;
                private DateTime _CreatedDate;
                private int _CreatedBy = 0;
                private int _ModifiedBy = 0;

                //leave added by Sonal Banode
                private System.Nullable<bool> _IsClassArrangeRequired;
                private System.Nullable<bool> _IsClassArrangeAcceptanceRequired;
                private int _CHIDNO;
                private string _TIME;
                private string _CLASS_ARRAN_STATUS;
                private int _SRNO;
                private decimal _LeaveCreditedAfterSlot;
                private System.Nullable<bool> _isSMS;
                private System.Nullable<bool> _isEmail;
                private int _ODDAYS = 0;
                private int _odApp = 0;
                private int _ComoffvalidMonth = 0;
                private string _FULLDAYCOMOFF = string.Empty;
                private string _HALFDAYCOMOFF = string.Empty;
                private System.Nullable<bool> _IsLeaveWisePath;
                private string _DIRECTORMAILID = string.Empty;
                private int _LEAVEAPPROVALVALIDDAYS = 0;
                private int _NOTIFICATIONDAYS = 0;
                private System.Nullable<bool> _IsAutoApprove;
                private System.Nullable<bool> _IsApprovalOnDirectLeave;
                private System.Nullable<bool> _IsValidatedays;
                //LOCATION MASTER==================================
                private string _LOCATION = string.Empty;
                private string _LATITUDE = string.Empty;
                private string _LONGITUDE = string.Empty;
                private string _RADIUS = string.Empty;
                private string _LOCATION_ADDRESS = string.Empty;
                private int _LOCNO = 0;
                private int _IDNO = 0;
                //===============================================
                //Added on 20-09-2022 by Sonal Banode
                private System.Nullable<bool> _IsDOJ;
                private int _SPFID;
                private System.Nullable<bool> _IsCompOff;
                private int _Daynumber = 0;
                private System.Nullable<bool> _ISEMPWISESAT;
                private int _SERIALNO = 0;
                private string _IRREGULARITYSTATUS = string.Empty;
                private System.Nullable<bool> _ISWEEKWISE;
                private int _SHIFTNO = 0;
                private double _CARRYDAYS;
                //Added On 12/12/2022 by Shrikant Bharne
                private System.Nullable<bool> _IsCopypath;
                private System.Nullable<bool> _IsEmployeewiseSaturday;
                private System.Nullable<bool> _IsRequiredDocumentforOD;
                private string _AllowLate;
                private string _AllowEarly;

                private int _PERTNO;
                private DateTime _APRDT;

                private int _Dayselection;
                private int _PERMISSIONINMONTH;

                //
                //Added on 29-03-2023 by Sonal Banode
                private System.Nullable<bool> _ISREQUIREDLOAD;
                private System.Nullable<decimal> _LEAVEVALIDMONTH;
                private string _CLASS = string.Empty;
                private string _COURSENAME = string.Empty;
                private string _SLOTNAME = string.Empty;
                private int _FACNO = 0;
                private string _LOAD_STATUS = string.Empty;

                private System.Nullable<bool> _IsLeaveValid;
                private System.Nullable<bool> _ISLWPNOTALLOW;
                private System.Nullable<bool> _IsShowLWP;
                private System.Nullable<bool> _IsChargeMail;
                private System.Nullable<bool> _IsValidatedLeaveComb;

                //Added on 20-09-2023 by Piyush Thakre
                private System.Nullable<bool> _IsNotAllowLeaveinCont;
                //
                # endregion

                #region public members
                //=========EL ENCASHMENT

                public System.Nullable<bool> IsActive
                {
                    get
                    {
                        return this._IsActive;
                    }
                    set
                    {
                        if ((this._IsActive != value))
                        {
                            this._IsActive = value;
                        }
                    }
                }

                public double BALANCE
                {
                    get
                    {
                        return this._BALANCE;
                    }
                    set
                    {
                        if ((this._BALANCE != value))
                        {
                            this._BALANCE = value;
                        }
                    }
                }

                public double TDAY
                {
                    get
                    {
                        return this._TDAY;
                    }
                    set
                    {
                        if ((this._TDAY != value))
                        {
                            this._TDAY = value;
                        }
                    }
                }

                public int CREATEDBY
                {
                    get
                    {
                        return this._CREATEDBY;
                    }
                    set
                    {
                        if ((this._CREATEDBY != value))
                        {
                            this._CREATEDBY = value;
                        }
                    }
                }
                public int MODIFIEDBY
                {
                    get
                    {
                        return this._MODIFIEDBY;
                    }
                    set
                    {
                        if ((this._MODIFIEDBY != value))
                        {
                            this._MODIFIEDBY = value;
                        }
                    }
                }
                public string IPADDRESS
                {
                    get
                    {
                        return this._IPADDRESS;
                    }
                    set
                    {
                        if ((this._IPADDRESS != value))
                        {
                            this._IPADDRESS = value;
                        }
                    }
                }

                //=========EL ENCASHMENT APPROVE

                public int APPROVEDBY
                {
                    get
                    {
                        return this._APPROVEDBY;
                    }
                    set
                    {
                        if ((this._APPROVEDBY != value))
                        {
                            this._APPROVEDBY = value;
                        }
                    }
                }


                public string FileName
                {
                    get { return _FileName; }
                    set { _FileName = value; }
                }

                public string FilePath
                {
                    get { return _FilePath; }
                    set { _FilePath = value; }
                }

                public decimal FileSize
                {
                    get { return _FileSize; }
                    set { _FileSize = value; }
                }
                public System.Nullable<bool> IsWithCertificate
                {
                    get
                    {
                        return this._IsWithCertificate;
                    }
                    set
                    {
                        if ((this._IsWithCertificate != value))
                        {
                            this._IsWithCertificate = value;
                        }
                    }
                }

                public System.Nullable<bool> IsPayLeave
                {
                    get
                    {
                        return this._IsPayLeave;
                    }
                    set
                    {
                        if ((this._IsPayLeave != value))
                        {
                            this._IsPayLeave = value;
                        }
                    }
                }
                public System.Nullable<bool> IsCertificate
                {
                    get
                    {
                        return this._IsCertificate;
                    }
                    set
                    {
                        if ((this._IsCertificate != value))
                        {
                            this._IsCertificate = value;
                        }
                    }
                }
                public System.Nullable<bool> IsAllowBeforeApplication
                {
                    get
                    {
                        return this._IsAllowBeforeApplication;
                    }
                    set
                    {
                        if ((this._IsAllowBeforeApplication != value))
                        {
                            this._IsAllowBeforeApplication = value;
                        }
                    }
                }
                public string SHIFT_INTIME
                {
                    get
                    {
                        return this._SHIFT_INTIME;

                    }
                    set
                    {
                        if ((this._SHIFT_INTIME != value))
                        {
                            this._SHIFT_INTIME = value;
                        }

                    }

                }
                public System.Nullable<bool> ISFULLDAYLEAVE
                {
                    get
                    {
                        return this._ISFULLDAYLEAVE;
                    }
                    set
                    {
                        if ((this._ISFULLDAYLEAVE != value))
                        {
                            this._ISFULLDAYLEAVE = value;
                        }
                    }
                }
                public int PERIOD_FROM
                {
                    get
                    {
                        return this._PERIOD_FROM;
                    }
                    set
                    {
                        if ((this._PERIOD_FROM != value))
                        {
                            this._PERIOD_FROM = value;
                        }
                    }

                }
                public int PERIOD_TO
                {
                    get
                    {
                        return this._PERIOD_TO;
                    }
                    set
                    {
                        if ((this._PERIOD_TO != value))
                        {
                            this._PERIOD_TO = value;
                        }
                    }

                }


                public string PERIOD_NAME
                {
                    get
                    {
                        return this._PERIOD_NAME;

                    }
                    set
                    {
                        if ((this._PERIOD_NAME != value))
                        {
                            this._PERIOD_NAME = value;
                        }

                    }

                }
                public int SESSION_SRNO
                {
                    get
                    {
                        return this._SESSION_SRNO;

                    }
                    set
                    {
                        if ((this._SESSION_SRNO != value))
                        {
                            this._SESSION_SRNO = value;
                        }

                    }

                }
                public int COLLEGE_NO
                {
                    get
                    {
                        return this._COLLEGE_NO;

                    }
                    set
                    {
                        if ((this._COLLEGE_NO != value))
                        {
                            this._COLLEGE_NO = value;
                        }

                    }

                }
                public int TRANNO
                {
                    get
                    {
                        return this._TRANNO;

                    }
                    set
                    {
                        if ((this._TRANNO != value))
                        {
                            this._TRANNO = value;
                        }

                    }

                }

                public int PROGRAM_NO
                {
                    get
                    {
                        return this._PROGRAM_NO;

                    }
                    set
                    {
                        if ((this._PROGRAM_NO != value))
                        {
                            this._PROGRAM_NO = value;
                        }

                    }

                }
                public int FACULTYNO
                {
                    get
                    {
                        return this._FACULTYNO;

                    }
                    set
                    {
                        if ((this._FACULTYNO != value))
                        {
                            this._FACULTYNO = value;
                        }

                    }

                }
                public int PERIODNO
                {
                    get
                    {
                        return this._PERIODNO;

                    }
                    set
                    {
                        if ((this._PERIODNO != value))
                        {
                            this._PERIODNO = value;
                        }

                    }

                }
                public string YEARSEM
                {
                    get
                    {
                        return this._YEARSEM;
                    }
                    set
                    {
                        if ((this._YEARSEM != value))
                        {
                            this._YEARSEM = value;
                        }
                    }
                }

                public string PROGRAM_TYPE
                {
                    get
                    {
                        return this._PROGRAM_TYPE;
                    }
                    set
                    {
                        if ((this._PROGRAM_TYPE != value))
                        {
                            this._PROGRAM_TYPE = value;
                        }
                    }
                }
                public string SUBJECT
                {
                    get
                    {
                        return this._SUBJECT;
                    }
                    set
                    {
                        if ((this._SUBJECT != value))
                        {
                            this._SUBJECT = value;
                        }
                    }
                }


                public string FACULTY_NAME
                {
                    get
                    {
                        return this._FACULTY_NAME;
                    }
                    set
                    {
                        if ((this._FACULTY_NAME != value))
                        {
                            this._FACULTY_NAME = value;
                        }
                    }
                }
                public int THEORY
                {
                    get
                    {
                        return this._THEORY;

                    }
                    set
                    {
                        if ((this._THEORY != value))
                        {
                            this._THEORY = value;
                        }

                    }

                }
                public DateTime ENGAGED_DATE
                {
                    get
                    {
                        return this._ENGAGED_DATE;
                    }
                    set
                    {
                        if ((this._ENGAGED_DATE != value))
                        {
                            this._ENGAGED_DATE = value;
                        }
                    }
                }
                //[Table(Name="PAY_LEAVE")]	
                public int LNO
                {
                    get
                    {
                        return this._LNO;
                    }
                    set
                    {
                        if ((this._LNO != value))
                        {
                            this._LNO = value;
                        }
                    }
                }
                public System.Nullable<int> STNO
                {
                    get
                    {
                        return this._STNO;
                    }
                    set
                    {
                        if ((this._STNO != value))
                        {
                            this._STNO = value;
                        }
                    }
                }
                public string LEAVENAME
                {
                    get
                    {
                        return this._LEAVENAME;
                    }
                    set
                    {
                        if ((this._LEAVENAME != value))
                        {
                            this._LEAVENAME = value;
                        }
                    }
                }
                public string LEAVE
                {
                    get
                    {
                        return this._LEAVE;
                    }
                    set
                    {
                        if ((this._LEAVE != value))
                        {
                            this._LEAVE = value;
                        }
                    }
                }
                public double MAX
                {
                    get
                    {
                        return this._MAX;
                    }
                    set
                    {
                        if ((this._MAX != value))
                        {
                            this._MAX = value;
                        }
                    }
                }


                public System.Nullable<bool> CARRY
                {
                    get
                    {
                        return this._CARRY;
                    }
                    set
                    {
                        if ((this._CARRY != value))
                        {
                            this._CARRY = value;
                        }
                    }
                }
                public System.Nullable<int> PERIOD
                {
                    get
                    {
                        return this._PERIOD;
                    }
                    set
                    {
                        if ((this._PERIOD != value))
                        {
                            this._PERIOD = value;
                        }

                    }
                }
                public System.Nullable<char> SEX
                {
                    get
                    {
                        return this._SEX;
                    }
                    set
                    {
                        if ((this._SEX != value))
                        {
                            this._SEX = value;
                        }
                    }
                }
                public double CAL
                {
                    get
                    {
                        return this._CAL;
                    }
                    set
                    {
                        if ((this._CAL != value))
                        {
                            this._CAL = value;
                        }
                    }
                }
                public string COLLEGE_CODE
                {
                    get
                    {
                        return this._COLLEGE_CODE;
                    }
                    set
                    {
                        if ((this._COLLEGE_CODE != value))
                        {
                            this._COLLEGE_CODE = value;
                        }
                    }
                }
                //[TABLE(namespace="PAY_HOLIDAYS"]
                public int HNO
                {
                    get
                    {
                        return this._HNO;
                    }
                    set
                    {
                        if ((this._HNO != value))
                        {
                            this._HNO = value;
                        }
                    }
                }
                public string HOLIDAYNAME
                {
                    get
                    {
                        return this._HOLIDAYNAME;
                    }
                    set
                    {
                        if ((this._HOLIDAYNAME != value))
                        {
                            this._HOLIDAYNAME = value;
                        }

                    }
                }
                public DateTime HDT
                {
                    get
                    {
                        return this._HDT;
                    }
                    set
                    {
                        if ((this._HDT != value))
                        {
                            this._HDT = value;
                        }

                    }
                }
                public int YEAR
                {
                    get
                    {
                        return this._YEAR;
                    }
                    set
                    {
                        if ((this._YEAR != value))
                        {
                            this._YEAR = value;
                        }
                    }
                }

                public int PANO
                {
                    get
                    {
                        return this._PANO;
                    }
                    set
                    {
                        if ((this._PANO != value))
                        {
                            this._PANO = value;
                        }
                    }
                }
                public string PANAME
                {
                    get
                    {
                        return this._PANAME;
                    }
                    set
                    {
                        if ((this._PANAME != value))
                        {
                            this._PANAME = value;
                        }
                    }

                }

                public int UANO
                {
                    get
                    {
                        return this._UANO;
                    }
                    set
                    {
                        if ((this._UANO != value))
                        {
                            this._UANO = value;
                        }
                    }
                }

                public int PAPNO
                {
                    get
                    {
                        return this._PAPNO;
                    }
                    set
                    {
                        if ((this._PAPNO != value))
                        {
                            this._PAPNO = value;
                        }
                    }
                }

                public int PAN01
                {
                    get
                    {
                        return this._PAN01;
                    }
                    set
                    {
                        if ((this._PAN01 != value))
                        {
                            this._PAN01 = value;
                        }
                    }
                }
                public int PAN02
                {
                    get
                    {
                        return this._PAN02;
                    }
                    set
                    {
                        if ((this._PAN02 != value))
                        {
                            this._PAN02 = value;
                        }
                    }
                }
                public int PAN03
                {
                    get
                    {
                        return this._PAN03;
                    }
                    set
                    {
                        if ((this._PAN03 != value))
                        {
                            this._PAN03 = value;
                        }
                    }
                }

                public int PAN04
                {
                    get
                    {
                        return this._PAN04;
                    }
                    set
                    {
                        if ((this._PAN04 != value))
                        {
                            this._PAN04 = value;
                        }
                    }
                }

                public int PAN05
                {
                    get
                    {
                        return this._PAN05;
                    }
                    set
                    {
                        if ((this._PAN05 != value))
                        {
                            this._PAN05 = value;
                        }
                    }
                }
                public string PAPATH
                {
                    get
                    {
                        return this._PAPATH;
                    }
                    set
                    {
                        if ((this._PAPATH != value))
                        {
                            this._PAPATH = value;
                        }

                    }
                }

                public int DEPTNO
                {
                    get
                    {
                        return this._DEPTNO;
                    }
                    set
                    {
                        if ((this._DEPTNO != value))
                        {
                            this._DEPTNO = value;
                        }
                    }
                }

                public int LETRNO
                {
                    get
                    {
                        return this._LETRNO;
                    }
                    set
                    {
                        if ((this._LETRNO != value))
                        {
                            this._LETRNO = value;
                        }
                    }
                }
                public int EMPNO
                {
                    get
                    {
                        return this._EMPNO;
                    }
                    set
                    {
                        if ((this._EMPNO != value))
                        {
                            this._EMPNO = value;
                        }
                    }

                }
                public DateTime APPDT
                {
                    get
                    {
                        return this._APPDT;
                    }
                    set
                    {
                        if ((this._APPDT != value))
                        {
                            this._APPDT = value;
                        }
                    }
                }
                public DateTime FROMDT
                {
                    get
                    {
                        return this._FROMDT;
                    }
                    set
                    {
                        if ((this._FROMDT != value))
                        {
                            this._FROMDT = value;
                        }
                    }
                }
                public DateTime TODT
                {
                    get
                    {
                        return this._TODT;
                    }
                    set
                    {
                        if ((this._TODT != value))
                        {
                            this._TODT = value;
                        }
                    }
                }
                public string LeaveTime
                {
                    get
                    {
                        return this._LeaveTime;
                    }
                    set
                    {
                        if ((this._LeaveTime != value))
                        {
                            this._LeaveTime = value;
                        }
                    }
                }


                public double NO_DAYS
                {
                    get
                    {
                        return this._NO_DAYS;
                    }
                    set
                    {
                        if ((this._NO_DAYS != value))
                        {
                            this._NO_DAYS = value;
                        }
                    }

                }
                public DateTime JOINDT
                {
                    get
                    {
                        return this._JOINDT;
                    }
                    set
                    {
                        if ((this._JOINDT != value))
                        {
                            this._JOINDT = value;
                        }
                    }
                }


                public System.Nullable<bool> FIT
                {
                    get
                    {
                        return this._FIT;
                    }
                    set
                    {
                        if ((this._FIT != value))
                        {
                            this._FIT = value;
                        }
                    }
                }
                public System.Nullable<bool> UNFIT
                {
                    get
                    {
                        return this._UNFIT;
                    }
                    set
                    {
                        if ((this._UNFIT != value))
                        {
                            this._UNFIT = value;
                        }
                    }
                }
                public System.Nullable<bool> FNAN
                {
                    get
                    {
                        return this._FNAN;
                    }
                    set
                    {
                        if ((this._FNAN != value))
                        {
                            this._FNAN = value;
                        }
                    }
                }

                public string REASON
                {
                    get
                    {
                        return this._REASON;
                    }
                    set
                    {
                        if ((this._REASON != value))
                        {
                            this._REASON = value;
                        }
                    }
                }
                public string ADDRESS
                {
                    get
                    {
                        return this._APP_ADDRESS;
                    }
                    set
                    {
                        if ((this._APP_ADDRESS != value))
                        {
                            this._APP_ADDRESS = value;
                        }
                    }
                }
                public string STATUS
                {
                    get
                    {
                        return this._STATUS;
                    }
                    set
                    {
                        if ((this._STATUS != value))
                        {
                            this._STATUS = value;
                        }
                    }
                }
                public int LAPENO
                {
                    get
                    {
                        return this._LAPENO;
                    }
                    set
                    {
                        if ((this._LAPENO != value))
                        {
                            this._LAPENO = value;
                        }
                    }
                }
                public String APP_REMARKS
                {
                    get
                    {
                        return this._APP_REMARKS;
                    }
                    set
                    {
                        if ((this._APP_REMARKS != value))
                        {
                            this._APP_REMARKS = value;
                        }
                    }
                }
                public int PREFIX
                {
                    get
                    {
                        return this._PREFIX;
                    }
                    set
                    {
                        if ((this._PREFIX != value))
                        {
                            this._PREFIX = value;
                        }
                    }
                }
                public int SUFFIX
                {
                    get
                    {
                        return this._SUFFIX;
                    }
                    set
                    {
                        if ((this._SUFFIX != value))
                        {
                            this._SUFFIX = value;
                        }
                    }
                }

                public String CHARGE_HANDED
                {
                    get
                    {
                        return this._CHARGEHANDED;
                    }
                    set
                    {
                        if ((this._CHARGEHANDED != value))
                        {
                            this._CHARGEHANDED = value;
                        }
                    }
                }

                public String ENTRY_STATUS
                {
                    get
                    {
                        return this._ENTRY_STATUS;
                    }
                    set
                    {
                        if ((this._ENTRY_STATUS != value))
                        {
                            this._ENTRY_STATUS = value;
                        }
                    }
                }

                public int DAYS
                {
                    get
                    {
                        return this._DAYS;
                    }
                    set
                    {
                        if ((this._DAYS != value))
                        {
                            this._DAYS = value;
                        }
                    }
                }

                public int HTNO
                {
                    get
                    {
                        return this._HTNO;
                    }
                    set
                    {
                        if ((this._HTNO != value))
                        {
                            this._HTNO = value;
                        }
                    }
                }

                public string HOLIDAYTYPE
                {
                    get
                    {
                        return this._HOLIDAYTYPE;
                    }
                    set
                    {
                        if ((this._HOLIDAYTYPE != value))
                        {
                            this._HOLIDAYTYPE = value;
                        }
                    }
                }

                public int WTNO
                {
                    get
                    {
                        return this._WTNO;
                    }
                    set
                    {
                        if ((this._WTNO != value))
                        {
                            this._WTNO = value;
                        }
                    }
                }

                public string WORKTYPE
                {
                    get
                    {
                        return this._WORKTYPE;
                    }
                    set
                    {
                        if ((this._WORKTYPE != value))
                        {
                            this._WORKTYPE = value;
                        }
                    }
                }
                //LEAVE..................

                public int LEAVENO
                {
                    get
                    {
                        return this._LEAVENO;
                    }
                    set
                    {
                        if ((this._LEAVENO != value))
                        {
                            this._LEAVENO = value;
                        }
                    }
                }

                public string LEAVENAMESHRT
                {
                    get
                    {
                        return this._LEAVENAMESHRT;
                    }
                    set
                    {
                        if ((this._LEAVENAMESHRT != value))
                        {
                            this._LEAVENAMESHRT = value;
                        }
                    }
                }


                public string SHORTNAME
                {
                    get
                    {
                        return this._SHORTNAME;
                    }
                    set
                    {
                        if ((this._SHORTNAME != value))
                        {
                            this._SHORTNAME = value;
                        }
                    }
                }

                public double MAXDAYS
                {
                    get
                    {
                        return this._MAXDAYS;
                    }
                    set
                    {
                        if ((this._MAXDAYS != value))
                        {
                            this._MAXDAYS = value;
                        }
                    }
                }
                public System.Nullable<int> YEARLYPRD
                {
                    get
                    {
                        return this._YEARLYP;
                    }
                    set
                    {
                        if ((this._YEARLYP != value))
                        {
                            this._YEARLYP = value;
                        }
                    }

                }
                public string HALFYEARLYPD
                {
                    get
                    {
                        return this._HALFYEARLYP;

                    }
                    set
                    {
                        if ((this._HALFYEARLYP != value))
                        {
                            this._HALFYEARLYP = value;
                        }

                    }

                }

                public int CALHOLIDAY
                {
                    get
                    {
                        return this._CALHOLIDAY;
                    }
                    set
                    {
                        if ((this._CALHOLIDAY != value))
                        {
                            this._CALHOLIDAY = value;
                        }
                    }
                }

                //LEAVE END..............


                public string EMPNAME
                {
                    get
                    {
                        return this._EMPNAME;

                    }
                    set
                    {
                        if ((this._EMPNAME != value))
                        {
                            this._EMPNAME = value;
                        }

                    }

                }

                public string INTIME
                {
                    get
                    {
                        return this._INTIME;

                    }
                    set
                    {
                        if ((this._INTIME != value))
                        {
                            this._INTIME = value;
                        }

                    }

                }

                public string OUTTIME
                {
                    get
                    {
                        return this._OUTTIME;

                    }
                    set
                    {
                        if ((this._OUTTIME != value))
                        {
                            this._OUTTIME = value;
                        }

                    }

                }
                public DateTime DATE
                {
                    get
                    {
                        return this._DATE;
                    }
                    set
                    {
                        if ((this._DATE != value))
                        {
                            this._DATE = value;
                        }

                    }
                }


                public string HOURS
                {
                    get
                    {
                        return this._HOURS;

                    }
                    set
                    {
                        if ((this._HOURS != value))
                        {
                            this._HOURS = value;
                        }

                    }

                }

                public string LATEBY
                {
                    get
                    {
                        return this._LATEBY;

                    }
                    set
                    {
                        if ((this._LATEBY != value))
                        {
                            this._LATEBY = value;
                        }

                    }

                }

                public int LTANO
                {
                    get
                    {
                        return this._LTANO;

                    }
                    set
                    {
                        if ((this._LTANO != value))
                        {
                            this._LTANO = value;
                        }

                    }

                }

                public string SHIFTOUTTIME
                {
                    get
                    {
                        return this._SHIFTOUTTIME;

                    }
                    set
                    {
                        if ((this._SHIFTOUTTIME != value))
                        {
                            this._SHIFTOUTTIME = value;
                        }

                    }

                }

                public string LEAVETYPE
                {
                    get
                    {
                        return this._LEAVETYPE;

                    }
                    set
                    {
                        if ((this._LEAVETYPE != value))
                        {
                            this._LEAVETYPE = value;
                        }

                    }

                }

                public int RESNO
                {
                    get
                    {
                        return this._RESNO;

                    }
                    set
                    {
                        if ((this._RESNO != value))
                        {
                            this._RESNO = value;
                        }

                    }

                }

                public double LEAVENO1
                {
                    get
                    {
                        return this._LEAVENO1;

                    }
                    set
                    {
                        if ((this._LEAVENO1 != value))
                        {
                            this._LEAVENO1 = value;
                        }

                    }

                }

                public int LSNO
                {
                    get
                    {
                        return this._LSNO;

                    }
                    set
                    {
                        if ((this._LSNO != value))
                        {
                            this._LSNO = value;
                        }

                    }

                }


                public int LEAVE01
                {
                    get
                    {
                        return this._LEAVE01;

                    }
                    set
                    {
                        if ((this._LEAVE01 != value))
                        {
                            this._LEAVE01 = value;
                        }

                    }

                }

                public int LEAVE02
                {
                    get
                    {
                        return this._LEAVE02;

                    }
                    set
                    {
                        if ((this._LEAVE02 != value))
                        {
                            this._LEAVE02 = value;
                        }

                    }

                }

                public int LEAVE03
                {
                    get
                    {
                        return this._LEAVE03;

                    }
                    set
                    {
                        if ((this._LEAVE03 != value))
                        {
                            this._LEAVE03 = value;
                        }

                    }

                }

                public int LEAVE04
                {
                    get
                    {
                        return this._LEAVE04;

                    }
                    set
                    {
                        if ((this._LEAVE04 != value))
                        {
                            this._LEAVE04 = value;
                        }

                    }

                }

                public int LEAVE05
                {
                    get
                    {
                        return this._LEAVE05;

                    }
                    set
                    {
                        if ((this._LEAVE05 != value))
                        {
                            this._LEAVE05 = value;
                        }

                    }

                }

                public string LEAVESEQ
                {
                    get
                    {
                        return this._LEAVESEQ;

                    }
                    set
                    {
                        if ((this._LEAVESEQ != value))
                        {
                            this._LEAVESEQ = value;
                        }

                    }

                }
                public string LEAVESTATUS
                {
                    get
                    {
                        return this._LEAVESTATUS;
                    }
                    set
                    {
                        if (this._LEAVESTATUS != value)
                        {
                            this._LEAVESTATUS = value;
                        }
                    }
                }
                public int ENO
                {
                    get
                    {
                        return this._ENO;
                    }
                    set
                    {
                        if ((this._ENO != value))
                        {
                            this._ENO = value;
                        }
                    }
                }
                public System.Nullable<bool> LEAVEFNAN
                {
                    get
                    {
                        return this._LEAVEFNAN;
                    }
                    set
                    {
                        if ((this._LEAVEFNAN != value))
                        {
                            this._LEAVEFNAN = value;
                        }
                    }
                }
                public System.Nullable<int> APPOINT_NO
                {
                    get
                    {
                        return this._APPOINT_NO;
                    }
                    set
                    {
                        if ((this._APPOINT_NO != value))
                        {
                            this._APPOINT_NO = value;
                        }
                    }
                }
                //_LVCANCELRMARK
                public string LVCANCELRMARK
                {
                    get
                    {
                        return this._LVCANCELRMARK;
                    }
                    set
                    {
                        if (this._LVCANCELRMARK != value)
                        {
                            this._LVCANCELRMARK = value;
                        }
                    }
                }
                public int MLHPL
                {
                    get
                    {
                        return this._MLHPL;
                    }
                    set
                    {
                        if ((this._MLHPL != value))
                        {
                            this._MLHPL = value;
                        }
                    }
                }
                //_ATTEMONTHNAME
                public string ATTEMONTHNAME
                {
                    get
                    {
                        return this._ATTEMONTHNAME;
                    }
                    set
                    {
                        if (this._ATTEMONTHNAME != value)
                        {
                            this._ATTEMONTHNAME = value;
                        }
                    }
                }

                public int ST
                {
                    get
                    {
                        return this._ST;
                    }
                    set
                    {
                        if ((this._ST != value))
                        {
                            this._ST = value;
                        }
                    }
                }
                //PAYROLL_DETENTION_EL
                public int SUBDESIGNO
                {
                    get
                    {
                        return this._SUBDESIGNO;

                    }
                    set
                    {
                        if ((this._SUBDESIGNO != value))
                        {
                            this._SUBDESIGNO = value;
                        }

                    }

                }

                public double ELNOOFDAYS
                {
                    get
                    {
                        return this._ELNOOFDAYS;

                    }
                    set
                    {
                        if ((this._ELNOOFDAYS != value))
                        {
                            this._ELNOOFDAYS = value;
                        }

                    }

                }


                public double ELTOCREDIT
                {
                    get
                    {
                        return this._ELTOCREDIT;

                    }
                    set
                    {
                        if ((this._ELTOCREDIT != value))
                        {
                            this._ELTOCREDIT = value;
                        }

                    }

                }

                public string ELYEAR
                {
                    get
                    {
                        return this._ELYEAR;

                    }
                    set
                    {
                        if ((this._ELYEAR != value))
                        {
                            this._ELYEAR = value;
                        }

                    }

                }


                public string ANREASON
                {
                    get
                    {
                        return this._ANREASON;
                    }
                    set
                    {
                        if ((this._ANREASON != value))
                        {
                            this._ANREASON = value;
                        }
                    }
                }

                public int SESSION_SERVICE_SRNO
                {
                    get
                    {
                        return this._SESSION_SERVICE_SRNO;
                    }
                    set
                    {
                        if ((this._SESSION_SERVICE_SRNO != value))
                        {
                            this._SESSION_SERVICE_SRNO = value;
                        }
                    }

                }

                public System.Nullable<bool> SERVICE_PERIOD_STATUS
                {
                    get
                    {
                        return this._SERVICE_PERIOD_STATUS;
                    }
                    set
                    {
                        if ((this._SERVICE_PERIOD_STATUS != value))
                        {
                            this._SERVICE_PERIOD_STATUS = value;
                        }
                    }
                }


                public int SERVICE_LIMIT
                {
                    get
                    {
                        return this._SERVICE_LIMIT;
                    }
                    set
                    {
                        if ((this._SERVICE_LIMIT != value))
                        {
                            this._SERVICE_LIMIT = value;
                        }
                    }
                }
                public int SERVICE_COMPLETE_LIMIT
                {
                    get
                    {
                        return this._SERVICE_COMPLETE_LIMIT;
                    }
                    set
                    {
                        if ((this._SERVICE_COMPLETE_LIMIT != value))
                        {
                            this._SERVICE_COMPLETE_LIMIT = value;
                        }
                    }
                }
                public int MAXPREDATED
                {
                    get
                    {
                        return this._MAXPREDATED;
                    }
                    set
                    {
                        if ((this._MAXPREDATED != value))
                        {
                            this._MAXPREDATED = value;
                        }
                    }
                }
                public int MAXPOSTDATED
                {
                    get
                    {
                        return this._MAXPOSTDATED;
                    }
                    set
                    {
                        if ((this._MAXPOSTDATED != value))
                        {
                            this._MAXPOSTDATED = value;
                        }
                    }
                }
                public int PDMAXDAYS
                {
                    get
                    {
                        return this._PDMAXDAYS;
                    }
                    set
                    {
                        if ((this._PDMAXDAYS != value))
                        {
                            this._PDMAXDAYS = value;
                        }
                    }
                }
                public int PDLMAXDAYS
                {
                    get
                    {
                        return this._PDLMAXDAYS;
                    }
                    set
                    {
                        if ((this._PDLMAXDAYS != value))
                        {
                            this._PDLMAXDAYS = value;
                        }
                    }
                }
                public int MAXOCCASIONPD
                {
                    get
                    {
                        return this._MAXOCCASIONPD;
                    }
                    set
                    {
                        if ((this._MAXOCCASIONPD != value))
                        {
                            this._MAXOCCASIONPD = value;
                        }
                    }
                }
                public int MAXOCCASIONPDL
                {
                    get
                    {
                        return this._MAXOCCASIONPDL;
                    }
                    set
                    {
                        if ((this._MAXOCCASIONPDL != value))
                        {
                            this._MAXOCCASIONPDL = value;
                        }
                    }
                }
                public string OCCPERIODPD
                {
                    get
                    {
                        return this._OCCPERIODPD;
                    }
                    set
                    {
                        if ((this._OCCPERIODPD != value))
                        {
                            this._OCCPERIODPD = value;
                        }
                    }
                }
                public string OCCPERIODPDL
                {
                    get
                    {
                        return this._OCCPERIODPDL;
                    }
                    set
                    {
                        if ((this._OCCPERIODPDL != value))
                        {
                            this._OCCPERIODPDL = value;
                        }
                    }
                }

                public string LType
                {
                    get
                    {
                        return this._LType;
                    }
                    set
                    {
                        if ((this._LType != value))
                        {
                            this._LType = value;
                        }
                    }
                }

                public System.Nullable<bool> IsCreditSlotWise
                {
                    get
                    {
                        return this.__IsCreditSlotWise;
                    }
                    set
                    {
                        if ((this.__IsCreditSlotWise != value))
                        {
                            this.__IsCreditSlotWise = value;
                        }
                    }
                }
                public int SlotOFDays
                {
                    get
                    {
                        return this._SlotOFDays;
                    }
                    set
                    {
                        if ((this._SlotOFDays != value))
                        {
                            this._SlotOFDays = value;
                        }
                    }
                }
                public double LeaveCreditAfterSlot
                {
                    get
                    {
                        return this._LeaveCreditAfterSlot;
                    }
                    set
                    {
                        if ((this._LeaveCreditAfterSlot != value))
                        {
                            this._LeaveCreditAfterSlot = value;
                        }
                    }
                }

                public System.Nullable<DateTime> ALLOTEDDATE
                {
                    get
                    {
                        return this._ALLOTEDDATE;
                    }
                    set
                    {
                        if ((this._ALLOTEDDATE != value))
                        {
                            this._ALLOTEDDATE = value;
                        }
                    }
                }
                public string CREDITDT
                {
                    get
                    {
                        return this._CREDITDT;
                    }
                    set
                    {
                        if ((this._CREDITDT != value))
                        {
                            this._CREDITDT = value;
                        }
                    }
                }

                public DateTime CreatedDate
                {
                    get
                    {
                        return this._CreatedDate;
                    }
                    set
                    {
                        if ((this._CreatedDate != value))
                        {
                            this._CreatedDate = value;
                        }
                    }
                }
                public int CreatedBy
                {
                    get { return _CreatedBy; }
                    set { _CreatedBy = value; }
                }

                public int ModifiedBy
                {
                    get { return _ModifiedBy; }
                    set { _ModifiedBy = value; }
                }

                public System.Nullable<bool> IsClassArrangeRequired
                {
                    get
                    {
                        return this._IsClassArrangeRequired;
                    }
                    set
                    {
                        if ((this._IsClassArrangeRequired != value))
                        {
                            this._IsClassArrangeRequired = value;
                        }
                    }
                }
                public System.Nullable<bool> IsClassArrangeAcceptanceRequired
                {
                    get
                    {
                        return this._IsClassArrangeAcceptanceRequired;
                    }
                    set
                    {
                        if ((this._IsClassArrangeAcceptanceRequired != value))
                        {
                            this._IsClassArrangeAcceptanceRequired = value;
                        }
                    }
                }

                public int CHIDNO
                {
                    get
                    {
                        return this._CHIDNO;
                    }
                    set
                    {
                        if ((this._CHIDNO != value))
                        {
                            this._CHIDNO = value;
                        }
                    }
                }

                public string TIME
                {
                    get
                    {
                        return this._TIME;
                    }
                    set
                    {
                        if ((this._TIME != value))
                        {
                            this._TIME = value;
                        }
                    }
                }

                public string CLASS_ARRAN_STATUS
                {
                    get
                    {
                        return this._CLASS_ARRAN_STATUS;
                    }
                    set
                    {
                        if ((this._CLASS_ARRAN_STATUS != value))
                        {
                            this._CLASS_ARRAN_STATUS = value;
                        }
                    }
                }

                public int SRNO
                {
                    get
                    {
                        return this._SRNO;
                    }
                    set
                    {
                        if ((this._SRNO != value))
                        {
                            this._SRNO = value;
                        }
                    }
                }

                public decimal LeaveCreditedAfterSlot
                {
                    get
                    {
                        return this._LeaveCreditedAfterSlot;
                    }
                    set
                    {
                        if ((this._LeaveCreditedAfterSlot != value))
                        {
                            this._LeaveCreditedAfterSlot = value;
                        }
                    }
                }

                public System.Nullable<bool> isSMS
                {
                    get
                    {
                        return this._isSMS;
                    }
                    set
                    {
                        if ((this._isSMS != value))
                        {
                            this._isSMS = value;
                        }
                    }
                }

                public System.Nullable<bool> isEmail
                {
                    get
                    {
                        return this._isEmail;
                    }
                    set
                    {
                        if ((this._isEmail != value))
                        {
                            this._isEmail = value;
                        }
                    }
                }

                public int ODDAYS
                {
                    get
                    {
                        return this._ODDAYS;
                    }
                    set
                    {
                        if ((this._ODDAYS != value))
                        {
                            this._ODDAYS = value;
                        }
                    }
                }

                public int odApp
                {
                    get
                    {
                        return this._odApp;
                    }
                    set
                    {
                        if ((this._odApp != value))
                        {
                            this._odApp = value;
                        }
                    }
                }

                public int ComoffvalidMonth
                {
                    get
                    {
                        return this._ComoffvalidMonth;
                    }
                    set
                    {
                        if ((this._ComoffvalidMonth != value))
                        {
                            this._ComoffvalidMonth = value;
                        }
                    }
                }

                public string FULLDAYCOMOFF
                {
                    get
                    {
                        return this._FULLDAYCOMOFF;
                    }
                    set
                    {
                        if ((this._FULLDAYCOMOFF != value))
                        {
                            this._FULLDAYCOMOFF = value;
                        }
                    }
                }

                public string HALFDAYCOMOFF
                {
                    get
                    {
                        return this._HALFDAYCOMOFF;
                    }
                    set
                    {
                        if ((this._HALFDAYCOMOFF != value))
                        {
                            this._HALFDAYCOMOFF = value;
                        }
                    }
                }

                public System.Nullable<bool> IsLeaveWisePath
                {
                    get
                    {
                        return this._IsLeaveWisePath;
                    }
                    set
                    {
                        if ((this._IsLeaveWisePath != value))
                        {
                            this._IsLeaveWisePath = value;
                        }
                    }
                }

                public string DIRECTORMAILID
                {
                    get
                    {
                        return this._DIRECTORMAILID;
                    }
                    set
                    {
                        if ((this._DIRECTORMAILID != value))
                        {
                            this._DIRECTORMAILID = value;
                        }
                    }
                }

                public int LEAVEAPPROVALVALIDDAYS
                {
                    get
                    {
                        return this._LEAVEAPPROVALVALIDDAYS;
                    }
                    set
                    {
                        if ((this._LEAVEAPPROVALVALIDDAYS != value))
                        {
                            this._LEAVEAPPROVALVALIDDAYS = value;
                        }
                    }
                }



                public int NOTIFICATIONDAYS
                {
                    get
                    {
                        return this._NOTIFICATIONDAYS;
                    }
                    set
                    {
                        if ((this._NOTIFICATIONDAYS != value))
                        {
                            this._NOTIFICATIONDAYS = value;
                        }
                    }
                }

                public System.Nullable<bool> IsAutoApprove
                {
                    get
                    {
                        return this._IsAutoApprove;
                    }
                    set
                    {
                        if ((this._IsAutoApprove != value))
                        {
                            this._IsAutoApprove = value;
                        }
                    }
                }

                public string LOCATION
                {
                    get
                    {
                        return this._LOCATION;
                    }
                    set
                    {
                        if ((this._LOCATION != value))
                        {
                            this._LOCATION = value;
                        }
                    }
                }

                public string LATITUDE
                {
                    get
                    {
                        return this._LATITUDE;
                    }
                    set
                    {
                        if ((this._LATITUDE != value))
                        {
                            this._LATITUDE = value;
                        }
                    }
                }

                public string LONGITUDE
                {
                    get
                    {
                        return this._LONGITUDE;
                    }
                    set
                    {
                        if ((this._LONGITUDE != value))
                        {
                            this._LONGITUDE = value;
                        }
                    }
                }

                public string RADIUS
                {
                    get
                    {
                        return this._RADIUS;
                    }
                    set
                    {
                        if ((this._RADIUS != value))
                        {
                            this._RADIUS = value;
                        }
                    }
                }

                public string LOCATION_ADDRESS
                {
                    get
                    {
                        return this._LOCATION_ADDRESS;
                    }
                    set
                    {
                        if ((this._LOCATION_ADDRESS != value))
                        {
                            this._LOCATION_ADDRESS = value;
                        }
                    }
                }

                public int LOCNO
                {
                    get
                    {
                        return this._LOCNO;
                    }
                    set
                    {
                        if ((this._LOCNO != value))
                        {
                            this._LOCNO = value;
                        }
                    }
                }

                public int IDNO
                {
                    get
                    {
                        return this._IDNO;
                    }
                    set
                    {
                        if ((this._IDNO != value))
                        {
                            this._IDNO = value;
                        }
                    }
                }

                public System.Nullable<bool> IsApprovalOnDirectLeave
                {
                    get
                    {
                        return this._IsApprovalOnDirectLeave;
                    }
                    set
                    {
                        if ((this._IsApprovalOnDirectLeave != value))
                        {
                            this._IsApprovalOnDirectLeave = value;
                        }
                    }
                }

                public System.Nullable<bool> IsValidatedays
                {
                    get
                    {
                        return this._IsValidatedays;
                    }
                    set
                    {
                        if ((this._IsValidatedays != value))
                        {
                            this._IsValidatedays = value;
                        }
                    }
                }

                public System.Nullable<bool> IsDOJ
                {
                    get
                    {
                        return this._IsDOJ;
                    }
                    set
                    {
                        if ((this._IsDOJ != value))
                        {
                            this._IsDOJ = value;
                        }
                    }
                }

                public int SPFID
                {
                    get
                    {
                        return this._SPFID;
                    }
                    set
                    {
                        if ((this._SPFID != value))
                        {
                            this._SPFID = value;
                        }
                    }
                }

                public System.Nullable<bool> IsCompOff
                {
                    get
                    {
                        return this._IsCompOff;
                    }
                    set
                    {
                        if ((this._IsCompOff != value))
                        {
                            this._IsCompOff = value;
                        }
                    }
                }

                public int Daynumber
                {
                    get
                    {
                        return this._Daynumber;
                    }
                    set
                    {
                        if ((this._Daynumber != value))
                        {
                            this._Daynumber = value;
                        }
                    }
                }

                public System.Nullable<bool> ISEMPWISESAT
                {
                    get
                    {
                        return this._ISEMPWISESAT;
                    }
                    set
                    {
                        if ((this._ISEMPWISESAT != value))
                        {
                            this._ISEMPWISESAT = value;
                        }
                    }
                }

                public int SERIALNO
                {
                    get
                    {
                        return this._SERIALNO;
                    }
                    set
                    {
                        if ((this._SERIALNO != value))
                        {
                            this._SERIALNO = value;
                        }
                    }
                }

                public string IRREGULARITYSTATUS
                {
                    get
                    {
                        return this._IRREGULARITYSTATUS;
                    }
                    set
                    {
                        if ((this._IRREGULARITYSTATUS != value))
                        {
                            this._IRREGULARITYSTATUS = value;
                        }
                    }
                }

                public System.Nullable<bool> ISWEEKWISE
                {
                    get
                    {
                        return this._ISWEEKWISE;
                    }
                    set
                    {
                        if ((this._ISWEEKWISE != value))
                        {
                            this._ISWEEKWISE = value;
                        }
                    }
                }

                public int SHIFTNO
                {
                    get
                    {
                        return this._SHIFTNO;
                    }
                    set
                    {
                        if ((this._SHIFTNO != value))
                        {
                            this._SHIFTNO = value;
                        }
                    }
                }
                public System.Nullable<bool> IsCopypath
                {
                    get
                    {
                        return this._IsCopypath;
                    }
                    set
                    {
                        if ((this._IsCopypath != value))
                        {
                            this._IsCopypath = value;
                        }
                    }
                }

                public System.Nullable<bool> IsEmployeewiseSaturday
                {
                    get
                    {
                        return this._IsEmployeewiseSaturday;
                    }
                    set
                    {
                        if ((this._IsEmployeewiseSaturday != value))
                        {
                            this._IsEmployeewiseSaturday = value;
                        }
                    }
                }

                public double CARRYDAYS
                {
                    get
                    {
                        return this._CARRYDAYS;
                    }
                    set
                    {
                        if ((this._CARRYDAYS != value))
                        {
                            this._CARRYDAYS = value;
                        }
                    }
                }

                public System.Nullable<bool> IsRequiredDocumentforOD
                {
                    get
                    {
                        return this._IsRequiredDocumentforOD;
                    }
                    set
                    {
                        if ((this._IsRequiredDocumentforOD != value))
                        {
                            this._IsRequiredDocumentforOD = value;
                        }
                    }
                }

                public string AllowLate
                {
                    get
                    {
                        return this._AllowLate;
                    }
                    set
                    {
                        if ((this._AllowLate != value))
                        {
                            this._AllowLate = value;
                        }
                    }
                }
                public string AllowEarly
                {
                    get
                    {
                        return this._AllowEarly;
                    }
                    set
                    {
                        if ((this._AllowEarly != value))
                        {
                            this._AllowEarly = value;
                        }
                    }
                }

                public int PERTNO
                {
                    get
                    {
                        return this._PERTNO;
                    }
                    set
                    {
                        if ((this._PERTNO != value))
                        {
                            this._PERTNO = value;
                        }
                    }
                }

                public DateTime APRDT
                {
                    get
                    {
                        return this._APRDT;
                    }
                    set
                    {
                        if ((this._APRDT != value))
                        {
                            this._APRDT = value;
                        }
                    }
                }

                public int Dayselection
                {
                    get
                    {
                        return this._Dayselection;
                    }
                    set
                    {
                        if ((this._Dayselection != value))
                        {
                            this._Dayselection = value;
                        }
                    }
                }
                public int PERMISSIONINMONTH
                {
                    get
                    {
                        return this._PERMISSIONINMONTH;
                    }
                    set
                    {
                        if ((this._PERMISSIONINMONTH != value))
                        {
                            this._PERMISSIONINMONTH = value;
                        }
                    }
                }

                public System.Nullable<bool> ISREQUIREDLOAD
                {
                    get
                    {
                        return this._ISREQUIREDLOAD;
                    }
                    set
                    {
                        if ((this._ISREQUIREDLOAD != value))
                        {
                            this._ISREQUIREDLOAD = value;
                        }
                    }
                }

                public System.Nullable<decimal> LEAVEVALIDMONTH
                {
                    get
                    {
                        return this._LEAVEVALIDMONTH;
                    }
                    set
                    {
                        if ((this._LEAVEVALIDMONTH != value))
                        {
                            this._LEAVEVALIDMONTH = value;
                        }
                    }
                }

                public string CLASS
                {
                    get
                    {
                        return this._CLASS;
                    }
                    set
                    {
                        if ((this._CLASS != value))
                        {
                            this._CLASS = value;
                        }
                    }
                }

                public string COURSENAME
                {
                    get
                    {
                        return this._COURSENAME;
                    }
                    set
                    {
                        if ((this._COURSENAME != value))
                        {
                            this._COURSENAME = value;
                        }
                    }
                }

                public string SLOTNAME
                {
                    get
                    {
                        return this._SLOTNAME;
                    }
                    set
                    {
                        if ((this._SLOTNAME != value))
                        {
                            this._SLOTNAME = value;
                        }
                    }
                }

                public int FACNO
                {
                    get
                    {
                        return this._FACNO;
                    }
                    set
                    {
                        if ((this._FACNO != value))
                        {
                            this._FACNO = value;
                        }
                    }
                }

                public string LOAD_STATUS
                {
                    get
                    {
                        return this._LOAD_STATUS;
                    }
                    set
                    {
                        if ((this._LOAD_STATUS != value))
                        {
                            this._LOAD_STATUS = value;
                        }
                    }
                }


                public System.Nullable<bool> IsLeaveValid
                {
                    get
                    {
                        return this._IsLeaveValid;
                    }
                    set
                    {
                        if ((this._IsLeaveValid != value))
                        {
                            this._IsLeaveValid = value;
                        }
                    }
                }
                public System.Nullable<bool> ISLWPNOTALLOW
                {
                    get
                    {
                        return this._ISLWPNOTALLOW;
                    }
                    set
                    {
                        if ((this._ISLWPNOTALLOW != value))
                        {
                            this._ISLWPNOTALLOW = value;

                        }
                    }
                }

                public System.Nullable<bool> IsShowLWP
                {
                    get
                    {
                        return this._IsShowLWP;
                    }
                    set
                    {
                        if ((this._IsShowLWP != value))
                        {
                            this._IsShowLWP = value;

                        }
                    }
                }
                public System.Nullable<bool> IsChargeMail
                {
                    get
                    {
                        return this._IsChargeMail;
                    }
                    set
                    {
                        if ((this._IsChargeMail != value))
                        {
                            this._IsChargeMail = value;

                        }
                    }
                }

                public System.Nullable<bool> IsValidatedLeaveComb
                {
                    get
                    {
                        return this._IsValidatedLeaveComb;
                    }
                    set
                    {
                        if ((this._IsValidatedLeaveComb != value))
                        {
                            this._IsValidatedLeaveComb = value;

                        }
                    }
                }

                public System.Nullable<bool> IsNotAllowLeaveinCont
                {
                    get
                    {
                        return this._IsNotAllowLeaveinCont;
                    }
                    set
                    {
                        if ((this._IsNotAllowLeaveinCont != value))
                        {
                            this._IsNotAllowLeaveinCont = value;

                        }
                    }
                }

                #endregion


            } //end class Leaves
        }//end namespace  BusinessLogicLayer.BusinessEntities 
    }//end namespace NITPRM
}//end namespace IITMS
