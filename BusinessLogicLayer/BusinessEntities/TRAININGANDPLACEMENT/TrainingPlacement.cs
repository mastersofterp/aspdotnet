using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class TrainingPlacement
            {
                #region Private Members
                //Table Name = ACAD_TP_COMPANY
                private int _COMPID;
                private int _COMPCATNO;
                private string _COMPCODE;
                private string _COMPNAME;
                private string _COMPADD;
                private string _COMPDIRECTOR;
                private int _CITYNO;
                private string _CITY;
                private string _PINCODE;
                private string _PHONENO;
                private string _FAXNO;
                private string _EMAILID;
                private string _WEBSITE;
                private string _SALRANGE;
                private string _CONTPERSON;
                private string _CONTDESIGNATION;
                private string _CONTADDRESS; 
                private string _CONTPHONE;
                private string _CONTMAILID;
                private string _OTHERINFO;
                private string _REMARK;
                private string _COLLEGE_CODE;
                private string _COMPPWD;
                private System.Nullable<char> _COMPSTATUS;

                private int _SCHEDULENO;
                private DateTime _SCHEDULEDATE;
                private DateTime _SCHEDULETIME;                
                private DateTime _INTERVIEWFROM;
                private DateTime _INTERVIEWTO;
                private string _REQUIREMENT;
                private string _UGPG;
                private string _BRANCH;
                private string _CRITERIA;
                //private System.Nullable<Boolean>  _BOND;
                private bool  _BOND;

               
                private string _BONDDETAILS;
                private string _SELECTNO;
                private double _UGSTIPEND;
                private double _UGSALARY;
                private double _PGSTIPEND;
                private double _PGSALARY;
                private string _SCHEDULESTATUS;
                private int _JOBTYPE;
                //private System.Nullable<Boolean> _JOBANNOUNCEMENT;
                private bool  _JOBANNOUNCEMENT;

                
                private System.Nullable<DateTime> _LASTDATE;
                private string _venue = string.Empty;
                private string _note = string.Empty;

               
                private string _VISITORNAME;
                private string _VISITORDESIGNATION;
                private string _VISITORDEPARTMENT;
                private char _INPLANT;
                
                private string _IPCONTPERSON;
                private string _IPCONTDESIGNATION;
                private string _IPCONTADDRESS;                
                private string _IPCONTPHONE;
                private string _IPCONTMAILID;
                private string _PLACEMENTCONTNO;
                private int _BRANCHNO;
                private int _DreamJob;
                private string _FileName;
                
                private string _Name;
                private string _OrganisedBy;
                private int _WorkNo;
                private bool  _IsDtFixed;
                private int _CurrencySalary;
                private int _CurrencyStipend;
                private decimal _SSCPER = 0.0m;
                private decimal _HSCPER = 0.0m;
                private decimal _DIPLOMAPER = 0.0m;
                private decimal _UGPER = 0.0m;
                private decimal _PGPER = 0.0m;
                private int _Backlog = 0;

                // ADDED BY SUMIT-- 15092019

                private int _DEGREE;


                //ADDED By Rohit on 21-01-2022
                private string _CompRegNo = string.Empty;
                private int _JobSector = 0;
                private int _CarrerAreas = 0;
                private int _AssociationFor = 0;
                private string _LocationName = string.Empty;
                private byte[] _Logo = null;

                //added by Rohit on 01-02-2022 for Job Announcement
                private int _JobRole = 0;
                private int _PlacementMode = 0;
                private string _JobDiscription = string.Empty;
                private double _Amount = 0;
                private double _MinAmount = 0;
                private double _MaxAmount = 0;
                private int _Interval = 0;
                private int _Currency = 0;
                private int _RoundNo = 0;
                private string _RoundDiscription = string.Empty;
                private string _SalDetails = string.Empty;
                private int _anywhereinsrilanka = 0;
                private string _Semester =string.Empty;

                private int _Faculty=0;
                private string _StudyLevel=string.Empty;
                private string _program=string.Empty;

                //added on 07-02-2022

                //added by juned 23-11-2022
                private int _Country = 0;
                private int _State = 0;

                private DataTable _TP_ROUND_TBL = null;  //sHAIKH jUNED 07-12-2022
                private DataTable _TP_ANNOUNCE_FOR_TBL = null;  //sHAIKH jUNED 11-01-2023
                private int _ACOMSCHNO = 0;
                #endregion 

                #region Public Members
                //Company Master [Table=ACAD_TP_COMPANY]
                public int COMPID
                {
                    get
                    {
                        return this._COMPID;
                    }
                    set
                    {
                        if ((this._COMPID != value))
                        {
                            this._COMPID = value;
                        }
                    }
                }
                public bool BOND
                {
                    get { return _BOND; }
                    set { _BOND = value; }
                }
                public int COMPCATNO
                {
                    get
                    {
                        return this._COMPCATNO;
                    }
                    set
                    {
                        if ((this._COMPCATNO != value))
                        {
                            this._COMPCATNO = value;
                        }
                    }
                }
                public bool JOBANNOUNCEMENT
                {
                    get { return _JOBANNOUNCEMENT; }
                    set { _JOBANNOUNCEMENT = value; }
                }
                
                public string COMPCODE
                {
                    get
                    {
                        return this._COMPCODE;
                    }
                    set
                    {
                        if ((this._COMPCODE != value))
                        {
                            this._COMPCODE = value;
                        }
                    }
                }
                public string COMPNAME
                {
                    get
                    {
                        return this._COMPNAME;
                    }
                    set
                    {
                        if ((this._COMPNAME != value))
                        {
                            this._COMPNAME = value;
                        }
                    }
                }
                public string COMPADD
                {
                    get
                    {
                        return this._COMPADD;
                    }
                    set
                    {
                        if ((this._COMPADD != value))
                        {
                            this._COMPADD = value;
                        }

                    }
                }
                public string COMPDIRECTOR
                {
                    get
                    {
                        return this._COMPDIRECTOR;
                    }
                    set
                    {
                        if ((this._COMPDIRECTOR != value))
                        {
                            this._COMPDIRECTOR = value;
                        }
                    }
                }
                public int CITYNO
                {
                    get
                    {
                        return this._CITYNO;
                    }
                    set
                    {
                        if ((this._CITYNO != value))
                        {
                            this._CITYNO = value;
                        }
                    }
                }
                public string CITY
                {
                    get
                    {
                        return this._CITY;
                    }
                    set
                    {
                        if ((this._CITY != value))
                        {
                            this._CITY = value;
                        }
                    }
                }
                public string PINCODE
                {
                    get
                    {
                        return this._PINCODE;
                    }
                    set
                    {
                        if ((this._PINCODE != value))
                        {
                            this._PINCODE = value;
                        }
                    }
                
                }
                public string PHONENO
                {
                    get
                    {
                        return this._PHONENO;
                    }
                    set{
                        if ((this._PHONENO != value))
                        {
                            this._PHONENO=value;
                        }
                    }

                }
                public string FAXNO
                {
                    get
                    {
                        return this._FAXNO;
                    }
                    set
                    {
                        if ((this._FAXNO != value))
                        {
                            this._FAXNO = value;
                        }
                    }
                }
                public string EMAILID
                {
                    get
                    {
                        return this._EMAILID;
                    }
                    set
                    {
                        if ((this._EMAILID != value))
                        {
                            this._EMAILID = value;
                        }
                    }
                }
                public string WEBSITE
                {
                    get
                    {
                        return this._WEBSITE;
                    }
                    set
                    {
                        if ((this._WEBSITE != value))
                        {
                            this._WEBSITE = value;
                        }
                    }
                }
                public string SALRANGE
                {
                    get
                    {
                        return this._SALRANGE;
                    }
                    set
                    {
                        if ((this._SALRANGE != value))
                        {
                            this._SALRANGE = value;
                        }
                    }
                }
                public string CONTPERSON
                {
                    get
                    {
                        return this._CONTPERSON;
                    }
                    set
                    {
                        if((this._CONTPERSON != value))
                        {
                            this._CONTPERSON=value;
                        }
                    }
                }
                public string CONTDESIGNATION
                {
                    get
                    {
                        return this._CONTDESIGNATION;
                    }
                    set
                    {
                        if ((this._CONTDESIGNATION != value))
                        {
                            this._CONTDESIGNATION = value;
                        }
                    }
                }


                public string CONTADDRESS
                {
                    get
                    {
                        return this._CONTADDRESS;
                    }
                    set
                    {
                        if((this._CONTADDRESS != value))
                        {
                            this._CONTADDRESS = value;
                        }
                    }
                }
                public string CONTPHONE
                {
                    get
                    {
                        return this._CONTPHONE;
                    }
                    set
                    {
                        if ((this._CONTPHONE != value))
                        {
                            this._CONTPHONE = value;
                        }
                    }
                }
                public string CONTMAILID
                {
                    get
                    {
                        return this._CONTMAILID;
                    }
                    set
                    {
                        if ((this._CONTMAILID != value))
                        {
                            this._CONTMAILID = value;
                        }
                    }
            
                }
                public string OTHERINFO
                {
                    get
                    {
                        return this._OTHERINFO;
                    }
                    set
                    {
                        if ((this._OTHERINFO != value))
                        {
                            this._OTHERINFO = value;
                        }
                    }

                }
                public string REMARK
                {
                    get
                    {
                        return this._REMARK;
                    }
                    set
                    {
                        if ((this._REMARK != value))
                        {
                            this._REMARK = value;
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
                public string COMPPWD
                {
                    get
                    {
                        return this._COMPPWD;
                    }
                    set
                    {
                        if ((this._COMPPWD != value))
                        {
                            this._COMPPWD = value;
                        }
                    }
                }
                public System.Nullable<char> COMPSTATUS
                {
                    get
                    {
                        return this._COMPSTATUS;
                    }
                    set
                    {
                        if ((this._COMPSTATUS != value))
                        {
                            this._COMPSTATUS = value;
                        }
                    }
                }
                //Company Schedule [Table=ACAD_TP_COMPSCHEDULE]
                public int SCHEDULENO
                {
                    get
                    {
                        return this._SCHEDULENO;
                    }
                    set
                    {
                        if((this._SCHEDULENO !=value ))
                        {
                            this._SCHEDULENO = value;
                        }
                    }
                }
                public DateTime SCHEDULEDATE
                {
                    get
                    {
                        return this._SCHEDULEDATE;
                    }
                    set
                    {
                        if ((this._SCHEDULEDATE != value))
                        {
                            this._SCHEDULEDATE=value;
                        }
                    }
                }
                public DateTime SCHEDULETIME
                {
                    get { return _SCHEDULETIME; }
                    set { _SCHEDULETIME = value; }
                }
                public DateTime INTERVIEWFROM
                {
                    get
                    {
                        return this._INTERVIEWFROM;
                    }
                    set
                    {
                        if ((this._INTERVIEWFROM != value))
                        {
                            this._INTERVIEWFROM = value;
                        }
                    }
                }
                public DateTime INTERVIEWTO
                {
                    get
                    {
                        return this._INTERVIEWTO;
                    }
                    set
                    {
                        if ((this._INTERVIEWTO != value))
                        {
                            this._INTERVIEWTO = value;
                        }
                    }
                }
                public string REQUIREMENT
                {
                    get
                    {
                        return this._REQUIREMENT;
                    }
                    set
                    {
                        if ((this._REQUIREMENT != value))
                        {
                            this._REQUIREMENT = value;
                        }
                    }
                }
                public string UGPG
                {
                    get
                    { 
                        return this._UGPG; 
                    }
                    set
                    {
                        if ((this._UGPG != value))
                        {
                            this._UGPG = value;
                        }
                    }
                }
                public string BRANCH
                {
                    get
                    {
                        return this._BRANCH;
                    }
                    set
                    {
                        if((this._BRANCH !=value))
                        {
                            this._BRANCH=value;
                        }                    }
                }
                public string CRITERIA
                {
                    get
                    {
                        return this._CRITERIA;
                    }
                    set
                    {
                        if ((this._CRITERIA!=value))
                        {
                            this._CRITERIA=value;
                        }
                    }
                }
                //public System.Nullable<Boolean> BOND
                //{
                //    get
                //    {
                //        return this._BOND;
                //    }
                //    set
                //    {
                //        if ((this._BOND != value))
                //        {
                //            this._BOND = value;
                //        }
                //    }
                //}
                public string BONDDETAILS
                {
                    get
                    {
                         return this._BONDDETAILS;
                    }
                    set
                    {
                        if ((this._BONDDETAILS != value))
                        {
                            this._BONDDETAILS = value;
                        }
                    }
                }
                public string SELECTNO
                {
                    get
                    {
                        return this._SELECTNO;
                    }
                    set
                    {
                        if ((this._SELECTNO != value))
                        {
                            this._SELECTNO = value;
                        }
                    }
                }
                public double UGSTIPEND
                {
                    get
                    {
                        return this._UGSTIPEND;
                    }
                    set
                    {
                        if ((this._UGSTIPEND != value))
                        {
                            this._UGSTIPEND = value;
                        }
                    }
                }
                public double UGSALARY
                {
                    get
                    {
                        return this._UGSALARY;
                    }
                    set
                    {
                        if((this._UGSALARY != value))
                        {
                          this._UGSALARY=value;
                        }
                    }
                }
                public double PGSTIPEND
                {
                    get
                    {
                        return this._PGSTIPEND;
                    }
                    set
                    {
                        if ((this._PGSTIPEND != value))
                        {
                            this._PGSTIPEND = value;
                        }
                    }
                }
                public double PGSALARY
                {
                    get
                    {
                        return this._PGSALARY;
                    }
                    set
                    {
                        if ((this._PGSALARY != value))
                        {
                            this._PGSALARY = value;
                        }
                    }
                }
                public string SCHEDULESTATUS
                {
                    get
                    {
                        return this._SCHEDULESTATUS;
                    }
                    set
                    {
                        if ((this._SCHEDULESTATUS != value))
                        {
                            this._SCHEDULESTATUS = value;
                        }
                    }
                }
                public int JOBTYPE
                {
                    get
                    {
                        return this._JOBTYPE;
                    }
                    set
                    {
                        if ((this._JOBTYPE != value))
                        {
                            this._JOBTYPE = value;
                        }
                    }
                }

                //public System.Nullable<Boolean> JOBANNOUNCEMENT
                //{
                //    get 
                //    { 
                //        return  this._JOBANNOUNCEMENT;
                //    }
                //    set 
                //    {
                //        if ((this._JOBANNOUNCEMENT != value))
                //        {
                //            this._JOBANNOUNCEMENT = value;
                //        }
                        
                //    }
                //}

                public System.Nullable<DateTime> LASTDATE
                {
                    get { return this._LASTDATE; }
                    set
                    {
                        if ((this._LASTDATE != value))
                        {
                            this._LASTDATE = value;
                        }
                    }
                }
                public string Venue
                {
                    get { return _venue; }
                    set { _venue = value; }
                }
                public string NOTE
                {
                    get { return _note; }
                    set { _note = value; }
                }
                //Company Schedule Deatils
                public string VISITORNAME
                {
                    get
                    {
                        return this._VISITORNAME;
                    }
                    set
                    {
                        if ((this._VISITORNAME != value))
                        {
                            this._VISITORNAME = value;
                        }
                    }
                }
                public string VISITORDESIGNATION
                {
                    get
                    {
                        return this._VISITORDESIGNATION;
                    }
                    set
                    {
                        if ((this._VISITORDESIGNATION !=value))
                        {
                            this._VISITORDESIGNATION = value;
                        }

                    }
                }
                public string VISITORDEPARTMENT
                {
                    get
                    {
                        return this._VISITORDEPARTMENT;
                    }
                    set
                    {
                        if ((this._VISITORDEPARTMENT != value))
                        {
                            this._VISITORDEPARTMENT = value;
                        }
                    }
                }
                public string IPCONTPERSON
                {
                    get { return _IPCONTPERSON; }
                    set { _IPCONTPERSON = value; }
                }
                public string IPCONTDESIGNATION
                {
                    get { return _IPCONTDESIGNATION; }
                    set { _IPCONTDESIGNATION = value; }
                }
                public string IPCONTADDRESS
                {
                    get { return _IPCONTADDRESS; }
                    set { _IPCONTADDRESS = value; }
                }
                public string IPCONTPHONE
                {
                    get { return _IPCONTPHONE; }
                    set { _IPCONTPHONE = value; }
                }
                public string IPCONTMAILID
                {
                    get { return _IPCONTMAILID; }
                    set { _IPCONTMAILID = value; }
                }
                public char INPLANT
                {
                    get { return _INPLANT; }
                    set { _INPLANT = value; }
                }
                public string PLACEMENTCONTNO
                {
                    get { return _PLACEMENTCONTNO; }
                    set { _PLACEMENTCONTNO = value; }
                }
                public int BRANCHNO
                {
                    get { return _BRANCHNO; }
                    set { _BRANCHNO = value; }
                }
                public int DreamJob
                {
                    get { return _DreamJob; }
                    set { _DreamJob = value; }
                }
                public string FileName
                {
                    get { return _FileName; }
                    set { _FileName = value; }
                }
                public string Name
                {
                    get { return _Name; }
                    set { _Name = value; }
                }
                public string ORGANISEDBY
                {
                    get { return _OrganisedBy ; }
                    set { _OrganisedBy  = value; }
                }
                public int WORKNO
                {
                    get { return _WorkNo ; }
                    set { _WorkNo  = value; }
                }
                public bool IsDtFixed
                {
                    get { return _IsDtFixed ; }
                    set { _IsDtFixed = value; }
                }

              
                public int CurrencySalary
                {
                    get { return _CurrencySalary; }
                    set { _CurrencySalary = value; }
                }

                public int CurrencyStipend
                {
                    get { return _CurrencyStipend; }
                    set { _CurrencyStipend = value; }
                }

                public decimal SSCPER
                {
                    get { return _SSCPER; }
                    set { _SSCPER = value; }
                }

                public decimal HSCPER
                {
                    get { return _HSCPER; }
                    set { _HSCPER = value; }
                }

                public decimal DIPLOMAPER
                {
                    get { return _DIPLOMAPER; }
                    set { _DIPLOMAPER = value; }
                }
                

                
                public decimal UGPER
                {
                    get { return _UGPER; }
                    set { _UGPER = value; }
                }

                public decimal PGPER
                {
                    get { return _PGPER; }
                    set { _PGPER = value; }
                }

                public int Backlog
                {
                    get { return _Backlog; }
                    set { _Backlog = value; }
                }
                /// <summary>
                /// added by sumit
                /// </summary>
                public int DEGREE
                {
                    get
                    {
                        return this._DEGREE;
                    }
                    set
                    {
                        if ((this._DEGREE != value))
                        {
                            this._DEGREE = value;
                        }
                    }
                }

                //Added By Rohit 21-01-2022
                public string CompRegNo
                {
                    get { return _CompRegNo; }
                    set { _CompRegNo = value; }
                }
                public int JobSector
                {
                    get { return _JobSector; }
                    set { _JobSector = value; }
                }
                public int CareerAreas
                {
                    get { return _CarrerAreas; }
                    set { _CarrerAreas = value; }
                }
                public int AssociationFor
                {
                    get { return _AssociationFor; }
                    set { _AssociationFor = value; }
                }

                public string LocationName
                {
                    get { return _LocationName; }
                    set { _LocationName = value; }
                }
                public byte[] Logo
                {
                    get { return _Logo; }
                    set { _Logo = value; }
                }

                //added by Rohit on 01-02-2022 for job announcement
                public int JobRole
                {
                    get { return _JobRole; }
                    set { _JobRole = value; }
                }

                public int PlacementMode
                {
                    get { return _PlacementMode; }
                    set { _PlacementMode = value; }
                }
                public string JobDiscription
                {
                    get { return _JobDiscription; }
                    set { _JobDiscription = value; }
                
                }

                public double Amount
                {
                    get { return _Amount; }
                    set { _Amount = value; }

                }
                public double MinAmount
                {
                    get { return _MinAmount; }
                    set { _MinAmount = value; }

                }
                public double MaxAmount
                {
                    get { return _MaxAmount; }
                    set { _MaxAmount = value; }

                }

                public int Interval
                {
                    get { return _Interval; }
                    set { _Interval = value; }

                }

                public int Currency
                {
                    get { return _Currency; }
                    set { _Currency = value; }

                }

                public int RoundNo
                {
                    get { return _RoundNo; }
                    set { _RoundNo = value; }

                }

                public string RoundDiscription
                {
                    get { return _RoundDiscription; }
                    set { _RoundDiscription = value; }
                }

                public string SalDetails
                {

                    get { return _SalDetails; }
                    set { _SalDetails = value; }
                }

                public int anywhereinsrilanka
                {
                    get { return _anywhereinsrilanka; }
                    set { _anywhereinsrilanka =value; }
                }

                public string Semester
                {

                    get { return _Semester; }
                    set { _Semester = value; }
                }

                public int Faculty
                {

                    get { return _Faculty; }
                    set { _Faculty = value; }
                }
                public string StudyLevel
                {

                    get { return _StudyLevel; }
                    set { _StudyLevel = value; }
                }

                 public string Program
                {
                    get { return _program; }
                    set { _program = value; }
                }
                
                //added By juned 23-11-2022

                 public int Country
                 {

                     get { return _Country; }
                     set { _Country = value; }
                 }

                 public int State
                 {

                     get { return _State; }
                     set { _State = value; }
                 }


                #endregion

                 public DataTable TP_ROUND_TBL
                 {
                     get
                     {
                         return this._TP_ROUND_TBL;
                     }
                     set
                     {
                         if ((this._TP_ROUND_TBL != value))
                         {
                             this._TP_ROUND_TBL = value;
                         }
                     }
                 }

                 public DataTable TP_ANNOUNCE_FOR_TBL
                 {
                     get
                     {
                         return this._TP_ANNOUNCE_FOR_TBL;
                     }
                     set
                     {
                         if ((this._TP_ANNOUNCE_FOR_TBL != value))
                         {
                             this._TP_ANNOUNCE_FOR_TBL = value;
                         }
                     }
                 }

                 public int ACOMSCHNO
                 {
                     get
                     {
                         return this._ACOMSCHNO;
                     }
                     set
                     {
                         if ((this._ACOMSCHNO != value))
                         {
                             this._ACOMSCHNO = value;
                         }
                     }
                 }

 
            }//end class TrainingPlacement
        }//end namespace  BusinessLogicLayer.BusinessEntities
    }//end namespace NITPRM
}//end namespace IITMS

