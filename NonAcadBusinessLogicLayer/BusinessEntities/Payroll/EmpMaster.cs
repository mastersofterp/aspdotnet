using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class EmpMaster
            {
                #region Private Members
                private decimal _IDNO;

                private System.Nullable<decimal> _SEQ_NO;

                private string _TITLE;

                private string _IFSC_CODE;

                private string _FNAME;

                private string _MNAME;

                private string _LNAME;

                private string _PFILENO;

                private string _NUNIQUEID;

                private System.Nullable<char> _SEX;

                private string _FATHERNAME;

                private System.Nullable<System.DateTime> _DOB = DateTime.MinValue;

                private System.Nullable<System.DateTime> _DOJ = DateTime.MinValue;

                private System.Nullable<System.DateTime> _DOI = DateTime.MinValue;

                private System.Nullable<System.DateTime> _RDT = DateTime.MinValue;

                //private System.Nullable<System.DateTime> _DOB = DateTime.Today;

                //private System.Nullable<System.DateTime> _DOJ = DateTime.Today;

                //private System.Nullable<System.DateTime> _DOI = DateTime.Today;

                //private System.Nullable<System.DateTime> _RDT = DateTime.Today;


                private string _HEIGHT = "0";

                private string _IDMARK1 = string.Empty;

                private string _IDMARK2 = string.Empty;

                private string _RESADD1;

                private string _TOWNADD1;

                private string _PHONENO;
                private string _ALTERNATEPHONENO;

                private int _DAHEADID;

                private string _EMAILID;

                private string _ALTERNATEEMAILID;

                private System.Nullable<decimal> _CASTENO;

                private System.Nullable<decimal> _CATEGORYNO;

                private string _SUBCASTE;

                private string _COMMUNITY;

                private System.Nullable<decimal> _RELIGIONNO;

                private System.Nullable<decimal> _NATIONALITYNO;

                private System.Nullable<bool> _HP;
                private System.Nullable<bool> _IsNEFT;

                private System.Nullable<bool> _IS_SHIFT_MANAGMENT;


                private System.Nullable<bool> _SENIOR_CITIZEN;

                private System.Nullable<decimal> _SUBDEPTNO;

                private System.Nullable<decimal> _SUBDESIGNO;

                private System.Nullable<decimal> _STNO;

                private System.Nullable<decimal> _STAFFNO;

                private string _BANKACC_NO;

                private string _GPF_NO;

                private string _PPF_NO;

                private string _PAN_NO;

                private System.Nullable<decimal> _BANKNO;

                private System.Nullable<bool> _QUARTER;

                private System.Nullable<bool> _IsBusFac;

               

                private System.Nullable<int> _QTRNO;

                private System.Nullable<int> _CLNO;

                private System.Nullable<int> _BANKCITYNO;

                private System.Nullable<decimal> _ETRNO;

                private string _REMARK;

                private string _COMMREM;

                private string _MONREM;

                private string _AUTHENREF;

                private System.Nullable<decimal> _ACATNO;

                private string _ANFN;

                private string _STATUS;

                private System.Nullable<bool> _QRENT_YN;

                private string _COLLEGE_CODE;

                private string _photoPath = string.Empty;

                private int _photoSize = 0;

                private int _PFNO = 0;
               
                private byte[] _photo = null;
                private byte[] _PhotoSign = null;

                private int _SHIFTNO = 0;

                private string _SACTIVE;

                private int _NUDESIGNO = 0;

                private string _MAIDENNAME = string.Empty;

                private string _HUSBANDNAME = string.Empty;

                private int _STATUSNO = 0;

                private System.Nullable<bool> _EPF_EXTRA;

                private System.Nullable<System.DateTime> _STDATE = DateTime.MinValue;

                private System.Nullable<System.DateTime> _RELIEVINGDATE = DateTime.MinValue;
                private System.Nullable<System.DateTime> _EXPDATEOFEXT = DateTime.MinValue;

                private System.Nullable<bool> _EMPLOYEE_LOCK;

                private int _COLLEGE_NO = 0;
                private int _UA_TYPE = 0;

                private int _EMPTYPENO = 0;
                private int _PGDEPTNO = 0;

                //unicode Added By Rohit Maske 26-09-2018
                private string _FNAME_UNICODE;

                private string _MNAME_UNICODE;

                private string _LNAME_UNICODE;

                private string _FATHERNAME_UNICODE;

                private int _UA_NO = 0;
                private int _USER_UATYPE = 0;

                private int _BLOODGRPNO = 0;

                private int _ChildMale = 0;
                private int _ChildFemale = 0;
                private System.Nullable<bool> _IsPhysicallyChallenged;
                private System.Nullable<bool> _MaritalStatus;
                private int _HandicapTypeID = 0;

                private string _CollegeRoomNo;
                private string _CollegeIntercomNo;
                private string _CasteCode;
                private string _QualForDisplay;
                private string _Employment;
                private string _Community;
                private System.Nullable<System.DateTime> _QuartersAllotmentDate = DateTime.MinValue;

                private string UAN = string.Empty;

                private int _MAINDEPTNO = 0;

                private int _EmployeeNo;

                //Added by Sonal Banode
                private string _ADHAR;
                private string _PASSPORT;
                private int _TITLENO;
                private string _CITY;
                private string _TALUKA = string.Empty;
                private string _DISTRICT = string.Empty;
                private string _PINCODE = string.Empty;
                private string _STATE = string.Empty;
                private string _COUNTRY = string.Empty;
                private int _COUNTRYNO;
                private int _STATENO;

                public int MAINDEPTNO
                {
                    set
                    {
                        _MAINDEPTNO = value;
                    }
                    get
                    {
                        return _MAINDEPTNO;
                    }
                }

                public int PEmployeeNo
                {
                    set
                    {
                        _EmployeeNo = value;
                    }
                    get
                    {
                        return _EmployeeNo;
                    }
                }

                public string UAN1
                {
                    get { return UAN; }
                    set { UAN = value; }
                }
                private string _EmployeeId = string.Empty;


                public string EmployeeId
                {
                    get { return _EmployeeId; }
                    set { _EmployeeId = value; }
                }


                public string IFSC_CODE
                {
                    get { return _IFSC_CODE; }
                    set { _IFSC_CODE = value; }
                }

                public int BLOODGRPNO
                {
                    get { return _BLOODGRPNO; }
                    set { _BLOODGRPNO = value; }
                }
                public System.Nullable<bool> MaritalStatus
                {
                    get { return _MaritalStatus; }
                    set { _MaritalStatus = value; }
                }
                public int ChildMale
                {
                    get { return _ChildMale; }
                    set { _ChildMale = value; }
                }
                public int ChildFemale
                {
                    get { return _ChildFemale; }
                    set { _ChildFemale = value; }
                }
                public System.Nullable<bool> IsPhysicallyChallenged
                {
                    get { return _IsPhysicallyChallenged; }
                    set { _IsPhysicallyChallenged = value; }
                }
                public System.Nullable<bool> IsNEFT
                {
                    get { return _IsNEFT; }
                    set { _IsNEFT = value; }
                }

                
                public int HandicapTypeID
                {
                    get { return _HandicapTypeID; }
                    set { _HandicapTypeID = value; }
                }

                public string CollegeRoomNo
                {
                    get { return _CollegeRoomNo; }
                    set { _CollegeRoomNo = value; }
                }
                public string CollegeIntercomNo
                {
                    get { return _CollegeIntercomNo; }
                    set { _CollegeIntercomNo = value; }
                }
                public string CasteCode
                {
                    get { return _CasteCode; }
                    set { _CasteCode = value; }
                }
                public string QualForDisplay
                {
                    get { return _QualForDisplay; }
                    set { _QualForDisplay = value; }
                }
                public string Employment
                {
                    get { return _Employment; }
                    set { _Employment = value; }
                }
                public string Community
                {
                    get { return _Community; }
                    set { _Community = value; }
                }
                public System.Nullable<System.DateTime> QuartersAllotmentDate
                {
                    get
                    {
                        return this._QuartersAllotmentDate;
                    }
                    set
                    {
                        if ((this._QuartersAllotmentDate != value))
                        {
                            this._QuartersAllotmentDate = value;
                        }
                    }
                }

                private int _Age = 0;

                public int Age
                {
                    get { return _Age; }
                    set { _Age = value; }
                }

                private Boolean _IsCabfac = false;

                public Boolean IsCabfac
                {
                    get { return _IsCabfac; }
                    set { _IsCabfac = value; }
                }
                private Boolean _isTelguMin = false;

                public Boolean IsTelguMin
                {
                    get { return _isTelguMin; }
                    set { _isTelguMin = value; }
                }
                private Boolean _isDrugAlrg = false;
                private Boolean _UserStatus = false;
                public Boolean UserStatus
                {
                    get
                    {
                        return _UserStatus;
                    }
                    set
                    {
                        _UserStatus = value;
                    }
                }

                public Boolean IsDrugAlrg
                {
                    get { return _isDrugAlrg; }
                    set { _isDrugAlrg = value; }
                }

                private string _ESICNO;

                # endregion



                // Entity Name  Added on 13-01-2023

                private string _EntityName;
                private string _AttritionType;
                private string _AttritionName;
                private int _OrganizationId;
                private int _EnityNo;
                private int _AttritionTypeNo;


                // Employee information   added  16-01-2023 by Purva Raut
                private bool _isNoticePeriod;

                public bool IsNoticePeriod
                {
                    get { return _isNoticePeriod; }
                    set { _isNoticePeriod = value; }
                }

                private System.Nullable<System.DateTime> _EXITDATE = DateTime.MinValue;

                public System.Nullable<System.DateTime> EXITDATE
                {
                    get { return _EXITDATE; }
                    set { _EXITDATE = value; }
                }

                private System.Nullable<System.DateTime> _RESIGNATIONTDATE = DateTime.MinValue;

                public System.Nullable<System.DateTime> RESIGNATIONTDATE
                {
                    get { return _RESIGNATIONTDATE; }
                    set { _RESIGNATIONTDATE = value; }
                }

                private string _RESIGNATIONRESASON;

                public string RESIGNATIONRESASON
                {
                    get { return _RESIGNATIONRESASON; }
                    set { _RESIGNATIONRESASON = value; }
                }
                private System.Nullable<System.DateTime> _GROUPOFDOJ = DateTime.MinValue;

                public System.Nullable<System.DateTime> GROUPOFDOJ
                {
                    get { return _GROUPOFDOJ; }
                    set { _GROUPOFDOJ = value; }
                }
                # region Public Properties


                public System.Nullable<bool> IsBusFac
                {
                    get { return _IsBusFac; }
                    set { _IsBusFac = value; }
                }

                public string HUSBANDNAME
                {
                    get { return _HUSBANDNAME; }
                    set { _HUSBANDNAME = value; }
                }

                public string MAIDENNAME
                {
                    get { return _MAIDENNAME; }
                    set { _MAIDENNAME = value; }
                }

                public int NUDESIGNO
                {
                    get { return _NUDESIGNO; }
                    set { _NUDESIGNO = value; }
                }

                public int PFNO
                {
                    get
                    {
                        return this._PFNO;
                    }
                    set
                    {
                        if ((this._PFNO != value))
                        {
                            this._PFNO = value;
                        }
                    }
                }


                public decimal IDNO
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

                public System.Nullable<decimal> SEQ_NO
                {
                    get
                    {
                        return this._SEQ_NO;
                    }
                    set
                    {
                        if ((this._SEQ_NO != value))
                        {
                            this._SEQ_NO = value;
                        }
                    }
                }


                public string TITLE
                {
                    get
                    {
                        return this._TITLE;
                    }
                    set
                    {
                        if ((this._TITLE != value))
                        {
                            this._TITLE = value;
                        }
                    }
                }


                public string PFILENO
                {
                    get
                    {
                        return this._PFILENO;
                    }
                    set
                    {
                        if ((this._PFILENO != value))
                        {
                            this._PFILENO = value;
                        }
                    }
                }



                public string NUNIQUEID
                {
                    get
                    {
                        return this._NUNIQUEID;
                    }
                    set
                    {
                        if ((this._NUNIQUEID != value))
                        {
                            this._NUNIQUEID = value;
                        }
                    }
                }

                public string FNAME
                {
                    get
                    {
                        return this._FNAME;
                    }
                    set
                    {
                        if ((this._FNAME != value))
                        {
                            this._FNAME = value;
                        }
                    }
                }

                public string MNAME
                {
                    get
                    {
                        return this._MNAME;
                    }
                    set
                    {
                        if ((this._MNAME != value))
                        {
                            this._MNAME = value;
                        }
                    }
                }

                public string LNAME
                {
                    get
                    {
                        return this._LNAME;
                    }
                    set
                    {
                        if ((this._LNAME != value))
                        {
                            this._LNAME = value;
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

                public string FATHERNAME
                {
                    get
                    {
                        return this._FATHERNAME;
                    }
                    set
                    {
                        if ((this._FATHERNAME != value))
                        {
                            this._FATHERNAME = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> DOB
                {
                    get
                    {
                        return this._DOB;
                    }
                    set
                    {
                        if ((this._DOB != value))
                        {
                            this._DOB = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> DOJ
                {
                    get
                    {
                        return this._DOJ;
                    }
                    set
                    {
                        if ((this._DOJ != value))
                        {
                            this._DOJ = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> DOI
                {
                    get
                    {
                        return this._DOI;
                    }
                    set
                    {
                        if ((this._DOI != value))
                        {
                            this._DOI = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> RDT
                {
                    get
                    {
                        return this._RDT;
                    }
                    set
                    {
                        if ((this._RDT != value))
                        {
                            this._RDT = value;
                        }
                    }
                }

                public string HEIGHT
                {
                    get
                    {
                        return this._HEIGHT;
                    }
                    set
                    {
                        if ((this._HEIGHT != value))
                        {
                            this._HEIGHT = value;
                        }
                    }
                }

                public string IDMARK1
                {
                    get
                    {
                        return this._IDMARK1;
                    }
                    set
                    {
                        if ((this._IDMARK1 != value))
                        {
                            this._IDMARK1 = value;
                        }
                    }
                }

                public string IDMARK2
                {
                    get
                    {
                        return this._IDMARK2;
                    }
                    set
                    {
                        if ((this._IDMARK2 != value))
                        {
                            this._IDMARK2 = value;
                        }
                    }
                }

                public string RESADD1
                {
                    get
                    {
                        return this._RESADD1;
                    }
                    set
                    {
                        if ((this._RESADD1 != value))
                        {
                            this._RESADD1 = value;
                        }
                    }
                }

                public string TOWNADD1
                {
                    get
                    {
                        return this._TOWNADD1;
                    }
                    set
                    {
                        if ((this._TOWNADD1 != value))
                        {
                            this._TOWNADD1 = value;
                        }
                    }
                }

                public string PHONENO
                {
                    get
                    {
                        return this._PHONENO;
                    }
                    set
                    {
                        if ((this._PHONENO != value))
                        {
                            this._PHONENO = value;
                        }
                    }
                }


                public string ALTERNATEPHONENO
                {
                    get
                    {
                        return this._ALTERNATEPHONENO;
                    }
                    set
                    {
                        if ((this._ALTERNATEPHONENO != value))
                        {
                            this._ALTERNATEPHONENO = value;
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

                public string ALTERNATEEMAILID
                {
                    get
                    {
                        return this._ALTERNATEEMAILID;
                    }
                    set
                    {
                        if ((this._ALTERNATEEMAILID != value))
                        {
                            this._ALTERNATEEMAILID = value;
                        }
                    }
                }

                public System.Nullable<decimal> CASTENO
                {
                    get
                    {
                        return this._CASTENO;
                    }
                    set
                    {
                        if ((this._CASTENO != value))
                        {
                            this._CASTENO = value;
                        }
                    }
                }

                public System.Nullable<decimal> CATEGORYNO
                {
                    get
                    {
                        return this._CATEGORYNO;
                    }
                    set
                    {
                        if ((this._CATEGORYNO != value))
                        {
                            this._CATEGORYNO = value;
                        }
                    }
                }

                public string SUBCASTE
                {
                    get
                    {
                        return this._SUBCASTE;
                    }
                    set
                    {
                        if ((this._SUBCASTE != value))
                        {
                            this._SUBCASTE = value;
                        }
                    }
                }

                public string COMMUNITY
                {
                    get
                    {
                        return this._COMMUNITY;
                    }
                    set
                    {
                        if ((this._COMMUNITY != value))
                        {
                            this._COMMUNITY = value;
                        }
                    }
                }

                public System.Nullable<decimal> RELIGIONNO
                {
                    get
                    {
                        return this._RELIGIONNO;
                    }
                    set
                    {
                        if ((this._RELIGIONNO != value))
                        {
                            this._RELIGIONNO = value;
                        }
                    }
                }

                public System.Nullable<decimal> NATIONALITYNO
                {
                    get
                    {
                        return this._NATIONALITYNO;
                    }
                    set
                    {
                        if ((this._NATIONALITYNO != value))
                        {
                            this._NATIONALITYNO = value;
                        }
                    }
                }

                public System.Nullable<bool> HP
                {
                    get
                    {
                        return this._HP;
                    }
                    set
                    {
                        if ((this._HP != value))
                        {
                            this._HP = value;
                        }
                    }
                }


                public System.Nullable<bool> IS_SHIFT_MANAGMENT
                {
                    get
                    {
                        return this._IS_SHIFT_MANAGMENT;
                    }
                    set
                    {
                        if ((this._IS_SHIFT_MANAGMENT != value))
                        {
                            this._IS_SHIFT_MANAGMENT = value;
                        }
                    }
                }
                
                public System.Nullable<bool> SENIOR_CIIZEN
                {
                    get
                    {
                        return this._SENIOR_CITIZEN;
                    }
                    set
                    {
                        if ((this._SENIOR_CITIZEN != value))
                        {
                            this._SENIOR_CITIZEN = value;
                        }
                    }
                }

                public System.Nullable<decimal> SUBDEPTNO
                {
                    get
                    {
                        return this._SUBDEPTNO;
                    }
                    set
                    {
                        if ((this._SUBDEPTNO != value))
                        {
                            this._SUBDEPTNO = value;
                        }
                    }
                }

                public System.Nullable<decimal> SUBDESIGNO
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

                public System.Nullable<decimal> STNO
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

                public System.Nullable<decimal> STAFFNO
                {
                    get
                    {
                        return this._STAFFNO;
                    }
                    set
                    {
                        if ((this._STAFFNO != value))
                        {
                            this._STAFFNO = value;
                        }
                    }
                }

                public string BANKACC_NO
                {
                    get
                    {
                        return this._BANKACC_NO;
                    }
                    set
                    {
                        if ((this._BANKACC_NO != value))
                        {
                            this._BANKACC_NO = value;
                        }
                    }
                }

                public string GPF_NO
                {
                    get
                    {
                        return this._GPF_NO;
                    }
                    set
                    {
                        if ((this._GPF_NO != value))
                        {
                            this._GPF_NO = value;
                        }
                    }
                }

                public string PPF_NO
                {
                    get
                    {
                        return this._PPF_NO;
                    }
                    set
                    {
                        if ((this._PPF_NO != value))
                        {
                            this._PPF_NO = value;
                        }
                    }
                }

                public string PAN_NO
                {
                    get
                    {
                        return this._PAN_NO;
                    }
                    set
                    {
                        if ((this._PAN_NO != value))
                        {
                            this._PAN_NO = value;
                        }
                    }
                }

                public System.Nullable<decimal> BANKNO
                {
                    get
                    {
                        return this._BANKNO;
                    }
                    set
                    {
                        if ((this._BANKNO != value))
                        {
                            this._BANKNO = value;
                        }
                    }
                }

                public System.Nullable<bool> QUARTER
                {
                    get
                    {
                        return this._QUARTER;
                    }
                    set
                    {
                        if ((this._QUARTER != value))
                        {
                            this._QUARTER = value;
                        }
                    }
                }

                public System.Nullable<int> QTRNO
                {
                    get
                    {
                        return this._QTRNO;
                    }
                    set
                    {
                        if ((this._QTRNO != value))
                        {
                            this._QTRNO = value;
                        }
                    }
                }

                public System.Nullable<int> CLNO
                {
                    get
                    {
                        return this._CLNO;
                    }
                    set
                    {
                        if ((this._CLNO != value))
                        {
                            this._CLNO = value;
                        }
                    }
                }

                public System.Nullable<int> BANKCITYNO
                {
                    get
                    {
                        return this._BANKCITYNO;
                    }
                    set
                    {
                        if ((this._BANKCITYNO != value))
                        {
                            this._BANKCITYNO = value;
                        }
                    }
                }

                public System.Nullable<decimal> ETRNO
                {
                    get
                    {
                        return this._ETRNO;
                    }
                    set
                    {
                        if ((this._ETRNO != value))
                        {
                            this._ETRNO = value;
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

                public string COMMREM
                {
                    get
                    {
                        return this._COMMREM;
                    }
                    set
                    {
                        if ((this._COMMREM != value))
                        {
                            this._COMMREM = value;
                        }
                    }
                }

                public string MONREM
                {
                    get
                    {
                        return this._MONREM;
                    }
                    set
                    {
                        if ((this._MONREM != value))
                        {
                            this._MONREM = value;
                        }
                    }
                }

                public string AUTHENREF
                {
                    get
                    {
                        return this._AUTHENREF;
                    }
                    set
                    {
                        if ((this._AUTHENREF != value))
                        {
                            this._AUTHENREF = value;
                        }
                    }
                }

                public System.Nullable<decimal> ACATNO
                {
                    get
                    {
                        return this._ACATNO;
                    }
                    set
                    {
                        if ((this._ACATNO != value))
                        {
                            this._ACATNO = value;
                        }
                    }
                }

                public string ANFN
                {
                    get
                    {
                        return this._ANFN;
                    }
                    set
                    {
                        if ((this._ANFN != value))
                        {
                            this._ANFN = value;
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

                public System.Nullable<bool> QRENT_YN
                {
                    get
                    {
                        return this._QRENT_YN;
                    }
                    set
                    {
                        if ((this._QRENT_YN != value))
                        {
                            this._QRENT_YN = value;
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

                public string PhotoPath
                {
                    get { return _photoPath; }
                    set { _photoPath = value; }
                }

                public int PhotoSize
                {
                    get { return _photoSize; }
                    set { _photoSize = value; }
                }

                public byte[] Photo
                {
                    get { return _photo; }
                    set { _photo = value; }
                }

                public byte[] PhotoSign
                {
                    get { return _PhotoSign; }
                    set { _PhotoSign = value; }
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

                public string SACTIVE
                {
                    get
                    {
                        return this._SACTIVE;
                    }
                    set
                    {
                        if ((this._SACTIVE != value))
                        {
                            this._SACTIVE = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> STDATE
                {
                    get
                    {
                        return this._STDATE;
                    }
                    set
                    {
                        if ((this._STDATE != value))
                        {
                            this._STDATE = value;
                        }
                    }
                }

                public int STATUSNO
                {
                    get
                    {
                        return this._STATUSNO;
                    }
                    set
                    {
                        if ((this._STATUSNO != value))
                        {
                            this._STATUSNO = value;
                        }
                    }
                }
                public System.Nullable<bool> EPF_EXTRA
                {
                    get
                    {
                        return this._EPF_EXTRA;
                    }
                    set
                    {
                        if ((this._EPF_EXTRA != value))
                        {
                            this._EPF_EXTRA = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> RELIEVINGDATE
                {
                    get
                    {
                        return this._RELIEVINGDATE;
                    }
                    set
                    {
                        if ((this._RELIEVINGDATE != value))
                        {
                            this._RELIEVINGDATE = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> EXPDATEOFEXT
                {
                    get
                    {
                        return this._EXPDATEOFEXT;
                    }
                    set
                    {
                        if ((this._EXPDATEOFEXT != value))
                        {
                            this._EXPDATEOFEXT = value;
                        }
                    }
                }

                public int COLLEGE_NO
                {
                    get { return _COLLEGE_NO; }
                    set { _COLLEGE_NO = value; }
                }

                public System.Nullable<bool> EMPLOYEE_LOCK
                {
                    get
                    {
                        return this._EMPLOYEE_LOCK;
                    }
                    set
                    {
                        if ((this._EMPLOYEE_LOCK != value))
                        {
                            this._EMPLOYEE_LOCK = value;
                        }
                    }
                }

                public int UA_TYPE
                {
                    get
                    {
                        return this._UA_TYPE;
                    }
                    set
                    {
                        if ((this._UA_TYPE != value))
                        {
                            this._UA_TYPE = value;
                        }
                    }
                }

                // private int _EMPTYPENO = 0;
               // private int _PGDEPTNO = 0;

                public int EMPTYPENO
                {
                    get
                    {
                        return this._EMPTYPENO;
                    }
                    set
                    {
                        if ((this._EMPTYPENO != value))
                        {
                            this._EMPTYPENO = value;
                        }
                    }
                }

                public int PGDEPTNO
                {
                    get
                    {
                        return this._PGDEPTNO;
                    }
                    set
                    {
                        if ((this._PGDEPTNO != value))
                        {
                            this._PGDEPTNO = value;
                        }
                    }
                }


                //UNICODE ADD By Rohit Maske 26-09-2018

                public string FNAME_UNICODE
                {
                    get
                    {
                        return this._FNAME_UNICODE;
                    }
                    set
                    {
                        if ((this._FNAME_UNICODE != value))
                        {
                            this._FNAME_UNICODE = value;
                        }
                    }
                }
                public string MNAME_UNICODE
                {
                    get
                    {
                        return this._MNAME_UNICODE;
                    }
                    set
                    {
                        if ((this._MNAME_UNICODE != value))
                        {
                            this._MNAME_UNICODE = value;
                        }
                    }
                }
                public string LNAME_UNICODE
                {
                    get
                    {
                        return this._LNAME_UNICODE;
                    }
                    set
                    {
                        if ((this._LNAME_UNICODE != value))
                        {
                            this._LNAME_UNICODE = value;
                        }
                    }
                }

                public string FATHERNAME_UNICODE
                {
                    get
                    {
                        return this._FATHERNAME_UNICODE;
                    }
                    set
                    {
                        if ((this._FATHERNAME_UNICODE != value))
                        {
                            this._FATHERNAME_UNICODE = value;
                        }
                    }
                }              

                public int UA_NO
                {
                    get
                    {
                        return this._UA_NO;
                    }
                    set
                    {
                        if ((this._UA_NO != value))
                        {
                            this._UA_NO = value;
                        }
                    }
                }


                public int USER_UATYPE
                {
                    get
                    {
                        return this._USER_UATYPE;
                    }
                    set
                    {
                        if ((this._USER_UATYPE != value))
                        {
                            this._USER_UATYPE = value;
                        }
                    }
                }


                public string ESICNO
                {
                    get { return this._ESICNO; }
                     
                    set
                    {
                        if ((this._ESICNO != value))
                        {
                            this._ESICNO = value;
                        }
                    }
                }

                public string ADHAR
                {
                    get
                    {
                        return this._ADHAR;
                    }
                    set
                    {
                        if (( this._ADHAR !=value))
                        {
                            this._ADHAR = value;
                        }
                    }
                }

                public string PASSPORT
                {
                    get
                    {
                        return this._PASSPORT;
                    }
                    set
                    {
                        if ((this._PASSPORT != value))
                        {
                            this._PASSPORT = value;
                        }
                    }
                }

                public int TITLENO
                {
                    get
                    {
                        return this._TITLENO;
                    }
                    set
                    {
                        if ((this._TITLENO != value))
                        {
                            this._TITLENO = value;
                        }
                    }
                }

                public string STATE
                {
                    get
                    {
                        return this._STATE;
                    }
                    set
                    {
                        if ((this._STATE != value))
                        {
                            this._STATE = value;
                        }
                    }
                }

                public string COUNTRY
                {
                    get
                    {
                        return this._COUNTRY;
                    }
                    set
                    {
                        if ((this._COUNTRY != value))
                        {
                            this._COUNTRY = value;
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

                public string TALUKA
                {
                    get
                    {
                        return this._TALUKA;
                    }
                    set
                    {
                        if ((this._TALUKA != value))
                        {
                            this._TALUKA = value;
                        }
                    }
                }

                public string DISTRICT
                {
                    get
                    {
                        return this._DISTRICT;
                    }
                    set
                    {
                        if ((this._DISTRICT != value))
                        {
                            this._DISTRICT = value;
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

                # endregion


                // added by vidisha on 11-05-2021 for pension payroll
                private System.Nullable<System.DateTime> _LDT = DateTime.MinValue;
                private string _ANFN_LEAVE;//30-10-2014
                private System.Nullable<bool> _SRCITIZEN;
                private System.Nullable<System.DateTime> _PREVISED_DATE = DateTime.MinValue;
                private string _JNTU;
                private string _ACITE;             
                private string _CELLNO;
                private System.Nullable<int> _BLOOD_GP;
                private string _BANK_BRANCH;
                private string _PRAN_NO = string.Empty;
                private decimal _ActualBasic = 0;

                //added on 26-04-2022
                private string _AICTE_NO;


                public System.Nullable<System.DateTime> LDT
                {
                    get
                    {
                        return this._LDT;
                    }
                    set
                    {
                        if ((this._LDT != value))
                        {
                            this._LDT = value;
                        }
                    }
                }
                public string ANFN_LEAVE//30-10-2014==SWATI
                {
                    get
                    {
                        return this._ANFN_LEAVE;
                    }
                    set
                    {
                        if ((this._ANFN_LEAVE != value))
                        {
                            this._ANFN_LEAVE = value;
                        }
                    }
                }
                public System.Nullable<bool> SRCITIZEN
                {
                    get
                    {
                        return this._SRCITIZEN;
                    }
                    set
                    {
                        if ((this._SRCITIZEN != value))
                        {
                            this._SRCITIZEN = value;
                        }
                    }
                }
                public System.Nullable<System.DateTime> PREVISED_DATE
                {
                    get
                    {
                        return this._PREVISED_DATE;
                    }
                    set
                    {
                        if ((this._PREVISED_DATE != value))
                        {
                            this._PREVISED_DATE = value;
                        }
                    }
                }
                public string JNTU
                {
                    get
                    {
                        return this._JNTU;
                    }
                    set
                    {
                        if (this._JNTU != value)
                        {
                            this._JNTU = value;
                        }
                    }
                }
                public string ACITE
                {
                    get
                    {
                        return this._ACITE;
                    }
                    set
                    {
                        if (this._ACITE != value)
                        {
                            this._ACITE = value;
                        }
                    }
                }
                public System.Nullable<int> BLOOD_GP
                {
                    get
                    {
                        return this._BLOOD_GP;
                    }
                    set
                    {
                        if (this._BLOOD_GP != value)
                        {
                            this._BLOOD_GP = value;
                        }
                    }
                }
                public string CELLNO
                {
                    get
                    {
                        return this._CELLNO;
                    }
                    set
                    {
                        if (this._CELLNO != value)
                        {
                            this._CELLNO = value;
                        }
                    }
                }
                public string BANK_BRANCH
                {
                    get
                    {
                        return this._BANK_BRANCH;
                    }
                    set
                    {
                        if (this._BANK_BRANCH != value)
                        {
                            this._BANK_BRANCH = value;
                        }
                    }
                }
                public string PRAN_NO
                {
                    get { return _PRAN_NO; }
                    set { _PRAN_NO = value; }
                }
                public decimal ActualBasic
                {
                    get { return _ActualBasic; }
                    set { _ActualBasic = value; }
                }


                public int DAHEADID
                {
                    get
                    {
                        return _DAHEADID;
                    }
                    set
                    {
                        _DAHEADID = value;
                    }
                }

                private Boolean _ExServicemen = false;

                public Boolean ExServicemen
                {
                    get { return _ExServicemen; }
                    set { _ExServicemen = value; }
                }

                public string AICTE_NO
                {
                    get
                    {
                        return this._AICTE_NO;
                    }
                    set
                    {
                        if ((this._AICTE_NO != value))
                        {
                            this._AICTE_NO = value;
                        }
                    }
                }

                private System.Nullable<System.DateTime> _RESIGNATIONDATE = DateTime.MinValue;

                public System.Nullable<System.DateTime> RESIGNATIONDATE
                {
                    get
                    {
                        return this._RESIGNATIONDATE;
                    }
                    set
                    {
                        if ((this._RESIGNATIONDATE != value))
                        {
                            this._RESIGNATIONDATE = value;
                        } 
                    }
                }
                private Boolean _IsActive = false;

                public Boolean IsActive
                {
                    get { return _IsActive; }
                    set { _IsActive = value; }
                }

                private int _REGPASSID = 0;



                private string _PANAME = string.Empty;
                private int _PASSTYPE = 0;

                public int REGPASSID
                {
                    get
                    {
                        return _REGPASSID;
                    }
                    set
                    {
                        _REGPASSID = value;
                    }
                }



                public string PANAME
                {
                    get
                    {
                        return _PANAME;
                    }
                    set
                    {
                        _PANAME = value;

                    }
                }

                public int PASSTYPE
                {
                    get
                    {
                        return _PASSTYPE;
                    }
                    set
                    {
                        _PASSTYPE = value;
                    }
                }
                private int _Nodues_No = 0;
                private int _Nodues_Status = 0;

                private int _IdNo = 0;
                public int _Created_By = 0;
                private string _IPADDRESS = string.Empty;
                private string _Remark = string.Empty;

                private int _AUTHO_ID = 0;
                private int _AUTHO_TYPE_ID = 0;


                public int Nodues_No { get { return this._Nodues_No; } set { if ((this._Nodues_No != value)) { this._Nodues_No = value; } } }
                public int Nodues_Status { get { return this._Nodues_Status; } set { if ((this._Nodues_Status != value)) { this._Nodues_Status = value; } } }
                public int IdNo { get { return this._IdNo; } set { if ((this._IdNo != value)) { this._IdNo = value; } } }
                public int Created_By { get { return this._Created_By; } set { if ((this._Created_By != value)) { this._Created_By = value; } } }
                public string IPADDRESS { get { return this._IPADDRESS; } set { if ((this._IPADDRESS != value)) { this._IPADDRESS = value; } } }
                public string Remark { get { return this._Remark; } set { if ((this._Remark != value)) { this._Remark = value; } } }

                public int AUTHO_ID { get { return this._AUTHO_ID; } set { if ((this._AUTHO_ID != value)) { this._AUTHO_ID = value; } } }
                public int AUTHO_TYPE_ID { get { return this._AUTHO_TYPE_ID; } set { if ((this._AUTHO_TYPE_ID != value)) { this._AUTHO_TYPE_ID = value; } } }
                public int PaylevelId { get; set; }
                public int CellNumber { get; set; }
                //for ASSET ALLOTMENNT 
                private int _ASSETID = 0;
                private string _ASSETREMARK = string.Empty;
                private bool _ISAPPROVED = false;
                private string _ASSETNAME = string.Empty;
                public string ASSETNAME
                {
                    get { return _ASSETNAME; }
                    set { _ASSETNAME = value; }
                }

                public int ASSETID
                {
                    get { return _ASSETID; }
                    set { _ASSETID = value; }
                }

                public string ASSETREMARK
                {
                    get { return _ASSETREMARK; }
                    set { _ASSETREMARK = value; }
                }
                public bool ISAPPROVED
                {
                    get { return _ISAPPROVED; }
                    set { _ISAPPROVED = value; }
                }

                private int _ASSETALLOTID;
                public int ASSETALLOTID
                {
                    get { return _ASSETALLOTID; }
                    set { _ASSETALLOTID = value; }
                }

                private string _RESIGNATIONREMARK;

                public string RESIGNATIONREMARK
                {
                    get { return _RESIGNATIONREMARK; }
                    set { _RESIGNATIONREMARK = value; }
                }

                private int _RESIGNATIONEMPID;
                public int RESIGNATIONEMPID
                {
                    get { return _RESIGNATIONEMPID; }
                    set { _RESIGNATIONEMPID = value; }
                }

                private int _EXITTYPEID;
                public int EXITTYPEID
                {
                    get { return _EXITTYPEID; }
                    set { _EXITTYPEID = value; }
                }

                private bool _REGSTATUS;
                public bool REGSTATUS
                {
                    get { return _REGSTATUS; }
                    set { _REGSTATUS = value; }
                }

                private string _COMMANDTYPE;
                public string COMMANDTYPE
                {
                    set
                    {
                        _COMMANDTYPE = value;
                    }
                    get
                    {
                        return _COMMANDTYPE;
                    }
                }


                private string _HR_RESIGNATION_REMARK;
                public string PHRRESIGNATIONREMARK
                {
                    set
                    {
                        _HR_RESIGNATION_REMARK = value;
                    }
                    get
                    {
                        return _HR_RESIGNATION_REMARK;
                    }
                }

                private int _REG_NOTICE_PERIOD;
                public int PNOTICEPERIOD
                {
                    set
                    {
                        _REG_NOTICE_PERIOD = value;
                    }
                    get
                    {
                        return _REG_NOTICE_PERIOD;
                    }
                }

                private bool _ISNODUES;
                public bool ISNODUES
                {
                    set
                    {
                        _ISNODUES = value;
                    }
                    get
                    {
                        return _ISNODUES;
                    }
                }

                //private DateTime _REG_RELEVING_DATE;
                private System.Nullable<System.DateTime> _REGRELEVINGDATE = DateTime.MinValue;
                public System.Nullable<System.DateTime> REGRELEVINGDATE
                {
                    get
                    {
                        return this._REGRELEVINGDATE;
                    }
                    set
                    {
                        if ((this._REGRELEVINGDATE != value))
                        {
                            this._REGRELEVINGDATE = value;
                        }
                    }
                }


                private string _TYPENAME;
                public string TYPENAME
                {
                    set
                    {
                        _TYPENAME = value;
                    }
                    get
                    {
                        return _TYPENAME;
                    }
                }


                private int _SEQUENCENO;
                public int SEQUENCENO
                {
                    set
                    {
                        _SEQUENCENO = value;
                    }
                    get
                    {
                        return _SEQUENCENO;
                    }
                }

                private int _TYPEID;
                public int TYPEID
                {
                    set
                    {
                        _TYPEID = value;
                    }
                    get
                    {
                        return _TYPEID;
                    }
                }

                private string _PASSWORD;
                private int _NOTIFICATIONDAYS;
                public string PASSWORD
                {
                    set
                    {
                        _PASSWORD = value;
                    }
                    get
                    {
                        return _PASSWORD;
                    }
                }

                public int NOTIFICATIONDAYS
                {
                    set
                    {
                        _NOTIFICATIONDAYS = value;
                    }
                    get
                    {
                        return _NOTIFICATIONDAYS;
                    }
                }
                private int _COLLEGEID;
                public int COLLEGEID
                {
                    set
                    {
                        _COLLEGEID = value;
                    }
                    get
                    {
                        return _COLLEGEID;
                    }
                }

                private string _EMPNAME;
                public string EMPNAME
                {
                    set
                    {
                        _EMPNAME = value;
                    }
                    get
                    {
                        return _EMPNAME;
                    }
                }
                private int _EMPAUTHMAILID;
                public int EMPAUTHMAILID
                {
                    set
                    {
                        _EMPAUTHMAILID = value;
                    }
                    get
                    {
                        return _EMPAUTHMAILID;
                    }
                }
                private System.Nullable<decimal> _FINALAMOUNT;
                public System.Nullable<decimal> FINALAMOUNT
                {
                    get
                    {
                        return this._FINALAMOUNT;
                    }
                    set
                    {
                        if ((this._FINALAMOUNT != value))
                        {
                            this._FINALAMOUNT = value;
                        }
                    }

                }

                public string EntityName
                {
                    set
                    {
                        _EntityName = value;
                    }
                    get
                    {
                        return _EntityName;
                    }
                }

                public  string  AttritionType
                {
                    set
                    {
                        _AttritionType = value;
                    }
                    get
                    {
                        return _AttritionType;
                    }
                }

                public string AttritionName
                {
                    set
                    {
                        _AttritionName = value;
                    }
                    get
                    {
                        return _AttritionName;
                    }
                }
                public int OrganizationId
                {
                    get { return _OrganizationId; }
                    set { _OrganizationId = value; }
                }

                public int EnityNo
                {
                    set
                    {
                        _EnityNo = value;
                    }
                    get
                    {
                       return _EnityNo;
                    }
                }
                public int AttritionTypeNo
                {
                    get { return _AttritionTypeNo; }
                    set { _AttritionTypeNo = value; }
                }


                // add 23-03-2023
                private Boolean _IsBioAuthorityPerson = false;

                public Boolean IsBioAuthorityPerson
                {
                    get { return _IsBioAuthorityPerson; }
                    set { _IsBioAuthorityPerson = value; }
                }

                private int _HRA_HEADID;

                public int HRA_HEADID
                {
                    set
                    {
                        _HRA_HEADID = value;
                    }
                    get
                    {
                        return _HRA_HEADID;
                    }
                }

                // Added on 03-02-2024 Employee Transfer
                private int _EMPLOYEETRANSFERID;
                public int EMPLOYEETRANSFERID
                {
                    set
                    {
                        _EMPLOYEETRANSFERID = value;
                    }
                    get
                    {
                        return _EMPLOYEETRANSFERID;
                    }
                }

                private System.Nullable<System.DateTime> _EMPTRANSFERDATE = DateTime.MinValue;

                public System.Nullable<System.DateTime> EMPTRANSFERDATE
                {
                    get { return _EMPTRANSFERDATE; }
                    set { _EMPTRANSFERDATE = value; }
                }

                private string _EMPTRANSFERRESASON;

                public string EMPTRANSFERRESASON
                {
                    get { return _EMPTRANSFERRESASON; }
                    set { _EMPTRANSFERRESASON = value; }
                }

                private int _OLDEMPCOLLEGENO;
                private int _NEWEMPCOLLEGENO;

                public int OLDEMPCOLLEGENO
                {
                    set
                    {
                        _OLDEMPCOLLEGENO = value;
                    }
                    get
                    {
                        return _OLDEMPCOLLEGENO;
                    }

                }

                public int NEWEMPCOLLEGENO
                {
                    set
                    {
                        _NEWEMPCOLLEGENO = value;
                    }
                    get
                    {
                        return _NEWEMPCOLLEGENO;
                    }
                }

                private int _EMPTRANSFERIDNO;
                public int EMPTRANSFERIDNO
                {

                    set
                    {
                        _EMPTRANSFERIDNO = value;
                    }
                    get
                    {
                        return _EMPTRANSFERIDNO;
                    }
                }

            }



        }//END: BusinessLayer.BusinessEntities

    }//END: UAIMS 

}// END: IITMS  _EXPDATEOFEXT