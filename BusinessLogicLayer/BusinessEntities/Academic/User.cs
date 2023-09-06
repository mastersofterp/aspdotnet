using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class User
            {
                #region Private Members
                 
                private int _userno = 0;
                private string _firstname = string.Empty; 
                private bool _reg_status = false;
                private DateTime _changedate = DateTime.Today;
                private byte[] _Photo = null;
                private DateTime _regdate = DateTime.Today;
                private DateTime _dob;
                private int _degreeno = 0;
                private int _branchno = 0;
                private int _jhRCA_ENTRNO = 0;
                
                private int _programType = 0;
                private int _AdmissionType = 0;
                private DateTime _studDob = DateTime.Today;
                private int _gender = 0;
                private int _religion = 0;
                private int _community = 0;
                private int _mother_Tongue = 0;
                private string _aadhar = string.Empty;
                private int _nationality = 0;
                private string _father_Name = string.Empty;
                private int _father_Occupation = 0;
                private string _father_Mobile = string.Empty;
                private string _mother_Name = string.Empty;
                private int _mother_Occupation = 0;
                private string _mother_Mobile = string.Empty;
                private string _address_1 = string.Empty;
                private string _address_2 = string.Empty;
                private string _address_3 = string.Empty;
                private string _city = string.Empty;
                private int _stateNo = 0;
                private string _pinCode = string.Empty;
                private int _edu_Info = 0;
                private string _exm_Reg_12 = string.Empty;
                private string _schoolName = string.Empty;
                private int _month_No = 0;
                private string _month = string.Empty;
                private int _year_No = 0;
                private string _year = string.Empty;
                private int _medium_No = 0;
                private string _medium = string.Empty;
                private string _country_Name = string.Empty;
                private int _sub1 = 0;
                private decimal _marks_Obt_1 = 0;
                private decimal _max_Marks_1 = 0;
                private decimal _per_1 = 0;
                private string _language = string.Empty;
                private decimal _marks_Obt_lang = 0;
                private decimal _max_Marks_lang = 0;
                private decimal _per_lang = 0;

                private int _sub2 = 0;
                private decimal _marks_Obt_2 = 0;
                private decimal _max_Marks_2 = 0;
                private decimal _per_2 = 0;

                private int _sub3 = 0;
                private decimal _marks_Obt_3 = 0;
                private decimal _max_Marks_3 = 0;
                private decimal _per_3 = 0;

                private int _sub4 = 0;
                private decimal _marks_Obt_4 = 0;
                private decimal _max_Marks_4 = 0;
                private decimal _per_4 = 0;

                private int _sub5 = 0;
                private decimal _marks_Obt_5 = 0;
                private decimal _max_Marks_5 = 0;
                private decimal _per_5 = 0;

                private int _sub6 = 0;
                private decimal _marks_Obt_6 = 0;
                private decimal _max_Marks_6 = 0;
                private decimal _per_6 = 0;

                private string _other_Specify = string.Empty;
                private decimal _cut_Off = 0;

                private string _photo_Path = "NA";
                private int _contribute = 0;

                private string _religionOther = string.Empty;
                private string _fOccupation_Other = string.Empty;
                private string _mOccupation_Other = string.Empty;
                private string _edu_Info_Other = string.Empty;
                private string _medium_Other = string.Empty;
                private int _available = 0;
                private int _countryID = 0;
                private int _annualIncome = 0;
                private int _instituteADV = 0;
                private string _phase = string.Empty;
                private string _instituteADV_Other = string.Empty;
                private int _district = 0;

                private int _passChange = 0;
                private int _specialize = 0;
                private decimal _totObt = 0;
                private decimal _totMax = 0;
                private decimal _totPer = 0;
                private decimal _nata = 0;
                #endregion





                //Added for JCERC
                private string _fatherName = string.Empty;
                private string _motherName = string.Empty;
                private string _emailid = string.Empty;
                private int _gender1 = ' ';
                private int _nationalityNo = 0;
                private int _categoryNo = 0;
                private string _aletremailid = string.Empty;
                private int _RELIGION = 0;
                private int _BLOODGRP = 0;
                private string _IDENTIFICATIONMARK = string.Empty;
                private string _ADHAARNO = string.Empty;
                private int _MaritalStatus = 0;
                private int _Differently_Abled = 0;
                private string _Nature_Disability = string.Empty;
                private string _Percentage_Disability = string.Empty;
                private int _State_Domicile = 0;
                private int _Sports_Person = 0;
                private int _Sports_Represented = 0;
                private string _F_TelNumber = string.Empty;
                private string _F_MobileNo = string.Empty;
                private int _F_Occupation = 0;
                private string _F_Designation = string.Empty;
                private string _F_EmailAddress = string.Empty;
                private string _M_TelNumber = string.Empty;
                private string _M_MobileNo = string.Empty;
                private int _M_Occupation = 0;
                private string _M_Designation = string.Empty;
                private string _M_EmailAddress = string.Empty;
                private int _host_Trans = 0;
                private string _host_Trans_Name = string.Empty;
                private int _ParentsAnnualIncome = 0;
                private string _sportsName = string.Empty;
                private string _sportsDoc = string.Empty;
                private string _sportsDocPath = string.Empty;
                private string _F_OccupationOther = string.Empty;
                private string _M_OccupationOther = string.Empty;

                //Added by Nikhil L . on 02/03/2022 for PG.
                private int _UGPGOT = 0;
                private int _qual_Degreeno_PG = 0;
                private int _branch_of_Study_PG = 0;
                private string _branch_of_Study_Others_PG = string.Empty;
                private string _institute_Name_PG = string.Empty;
                private string _university_Name_PG = string.Empty;
                private string _location_PG = string.Empty;
                private int _institute_State_PG = 0;
                private int _per_cgpa_PG = 0;
                private decimal _marks_per_cgpa_PG = 0;
                private int _semesterno_PG = 0;
                private int _year_PG = 0;
                private string _mode_of_study_PG = string.Empty;
                private string _name_of_test_PG = string.Empty;
                private decimal _score_PG = 0;
                private string _qualDegree_Others = string.Empty;

                #region public
                public int PROGRAMTYPE
                {
                    get { return _programType; }
                    set { _programType = value; }
                }
                public int ADMISSIONTYPE
                {
                    get { return _AdmissionType; }
                    set { _AdmissionType = value; }
                }
                public DateTime STUDDOB
                {
                    get { return _studDob; }
                    set { _studDob = value; }
                }
                public int JHRCA_ENTRNO
                {
                    get { return _jhRCA_ENTRNO; }
                    set { _jhRCA_ENTRNO = value; }
                }
                public int USERNO
                {
                    get { return _userno; }
                    set { _userno = value; }
                }
                public string FIRSTNAME
                {
                    get { return _firstname; }
                    set { _firstname = value; }
                }
                public bool REG_STATUS
                {
                    get { return _reg_status; }
                    set { _reg_status = value; }
                }
                public DateTime CHANGEDATE
                {
                    get { return _changedate; }
                    set { _changedate = value; }
                }
                public DateTime REGDATE
                {
                    get { return _regdate; }
                    set { _regdate = value; }
                }
                public byte[] PHOTO
                {
                    get { return _Photo; }
                    set { _Photo = value; }

                }
                public int BRANCHNO
                {
                    get { return _branchno; }
                    set { _branchno = value; }
                }
                public int DEGREENO
                {
                    get { return _degreeno; }
                    set { _degreeno = value; }
                }
                public DateTime DOB
                {
                    get { return _dob; }
                    set { _dob = value; }
                }
                public int Gender
                {
                    get { return _gender; }
                    set { _gender = value; }
                }
                public int Religion
                {
                    get { return _religion; }
                    set { _religion = value; }
                }
                public int Community
                {
                    get { return _community; }
                    set { _community = value; }
                }
                public int Mother_Tongue
                {
                    get { return _mother_Tongue; }
                    set { _mother_Tongue = value; }
                }
                public string Aadhar
                {
                    get { return _aadhar; }
                    set { _aadhar = value; }
                }
                public int Nationality
                {
                    get { return _nationality; }
                    set { _nationality = value; }
                }
                public string Father_Name
                {
                    get { return _father_Name; }
                    set { _father_Name = value; }
                }
                public int Father_Occupation
                {
                    get { return _father_Occupation; }
                    set { _father_Occupation = value; }
                }
                public string Father_Mobile
                {
                    get { return _father_Mobile; }
                    set { _father_Mobile = value; }
                }
                public string Mother_Name
                {
                    get { return _mother_Name; }
                    set { _mother_Name = value; }
                }
                public int Mother_Occupation
                {
                    get { return _mother_Occupation; }
                    set { _mother_Occupation = value; }
                }

                public string Mother_Mobile
                {
                    get { return _mother_Mobile; }
                    set { _mother_Mobile = value; }
                }

                public string Address_1
                {
                    get { return _address_1; }
                    set { _address_1 = value; }
                }
                public string Address_2
                {
                    get { return _address_2; }
                    set { _address_2 = value; }
                }
                public string Address_3
                {
                    get { return _address_3; }
                    set { _address_3 = value; }
                }
                public string City
                {
                    get { return _city; }
                    set { _city = value; }
                }
                public int State_No
                {
                    get { return _stateNo; }
                    set { _stateNo = value; }
                }
                public string PinCode
                {
                    get { return _pinCode; }
                    set { _pinCode = value; }
                }
                public int Edu_Info
                {
                    get { return _edu_Info; }
                    set { _edu_Info = value; }
                }

                public string Exm_Reg_12
                {
                    get { return _exm_Reg_12; }
                    set { _exm_Reg_12 = value; }
                }
                public string School_name
                {
                    get { return _schoolName; }
                    set { _schoolName = value; }
                }
                public int Month_No
                {
                    get { return _month_No; }
                    set { _month_No = value; }
                }
                public string Month
                {
                    get { return _month; }
                    set { _month = value; }
                }
                public int Year_No
                {
                    get { return _year_No; }
                    set { _year_No = value; }
                }
                public string Year
                {
                    get { return _year; }
                    set { _year = value; }
                }
                public int Medium_No
                {
                    get { return _medium_No; }
                    set { _medium_No = value; }
                }
                public string Medium
                {
                    get { return _medium; }
                    set { _medium = value; }
                }
                public string Country_Name
                {
                    get { return _country_Name; }
                    set { _country_Name = value; }
                }
                public int Sub_1
                {
                    get { return _sub1; }
                    set { _sub1 = value; }
                }
                public decimal Marks_Obt_1
                {
                    get { return _marks_Obt_1; }
                    set { _marks_Obt_1 = value; }
                }
                public decimal Max_Marks_1
                {
                    get { return _max_Marks_1; }
                    set { _max_Marks_1 = value; }
                }
                public decimal Per_1
                {
                    get { return _per_1; }
                    set { _per_1 = value; }
                }
                public string Language
                {
                    get { return _language; }
                    set { _language = value; }
                }
                public decimal Marks_Obt_Lang
                {
                    get { return _marks_Obt_lang; }
                    set { _marks_Obt_lang = value; }
                }
                public decimal Max_Marks_Lang
                {
                    get { return _max_Marks_lang; }
                    set { _max_Marks_lang = value; }
                }
                public decimal Per_Lang
                {
                    get { return _per_lang; }
                    set { _per_lang = value; }
                }

                public int Sub_2
                {
                    get { return _sub2; }
                    set { _sub2 = value; }
                }
                public decimal Marks_Obt_2
                {
                    get { return _marks_Obt_2; }
                    set { _marks_Obt_2 = value; }
                }
                public decimal Max_Marks_2
                {
                    get { return _max_Marks_2; }
                    set { _max_Marks_2 = value; }
                }
                public decimal Per_2
                {
                    get { return _per_2; }
                    set { _per_2 = value; }
                }

                public int Sub_3
                {
                    get { return _sub3; }
                    set { _sub3 = value; }
                }
                public decimal Marks_Obt_3
                {
                    get { return _marks_Obt_3; }
                    set { _marks_Obt_3 = value; }
                }
                public decimal Max_Marks_3
                {
                    get { return _max_Marks_3; }
                    set { _max_Marks_3 = value; }
                }
                public decimal Per_3
                {
                    get { return _per_3; }
                    set { _per_3 = value; }
                }


                public int Sub_4
                {
                    get { return _sub4; }
                    set { _sub4 = value; }
                }
                public decimal Marks_Obt_4
                {
                    get { return _marks_Obt_4; }
                    set { _marks_Obt_4 = value; }
                }
                public decimal Max_Marks_4
                {
                    get { return _max_Marks_4; }
                    set { _max_Marks_4 = value; }
                }
                public decimal Per_4
                {
                    get { return _per_4; }
                    set { _per_4 = value; }
                }



                public int Sub_5
                {
                    get { return _sub5; }
                    set { _sub5 = value; }
                }
                public decimal Marks_Obt_5
                {
                    get { return _marks_Obt_5; }
                    set { _marks_Obt_5 = value; }
                }
                public decimal Max_Marks_5
                {
                    get { return _max_Marks_5; }
                    set { _max_Marks_5 = value; }
                }
                public decimal Per_5
                {
                    get { return _per_5; }
                    set { _per_5 = value; }
                }



                public int Sub_6
                {
                    get { return _sub6; }
                    set { _sub6 = value; }
                }
                public decimal Marks_Obt_6
                {
                    get { return _marks_Obt_6; }
                    set { _marks_Obt_6 = value; }
                }
                public decimal Max_Marks_6
                {
                    get { return _max_Marks_6; }
                    set { _max_Marks_6 = value; }
                }
                public decimal Per_6
                {
                    get { return _per_6; }
                    set { _per_6 = value; }
                }


                public string Other_Specify
                {
                    get { return _other_Specify; }
                    set { _other_Specify = value; }
                }
                public decimal Cut_off
                {
                    get { return _cut_Off; }
                    set { _cut_Off = value; }
                }
                public string Photo_Path
                {
                    get { return _photo_Path; }
                    set { _photo_Path = value; }
                }
                public int contribute
                {
                    get { return _contribute; }
                    set { _contribute = value; }
                }
                public string Religion_Other
                {
                    get { return _religionOther; }
                    set { _religionOther = value; }
                }

                public string FOccupation_Other
                {
                    get { return _fOccupation_Other; }
                    set { _fOccupation_Other = value; }
                }
                public string MOccupation_Other
                {
                    get { return _mOccupation_Other; }
                    set { _mOccupation_Other = value; }
                }
                public string Edu_Info_Other
                {
                    get { return _edu_Info_Other; }
                    set { _edu_Info_Other = value; }
                }
                public string Medium_Other
                {
                    get { return _medium_Other; }
                    set { _medium_Other = value; }
                }

                public int Available
                {
                    get { return _available; }
                    set { _available = value; }
                }
                public int Country_Id
                {
                    get { return _countryID; }
                    set { _countryID = value; }
                }
                public int AnnualIncome
                {
                    get { return _annualIncome; }
                    set { _annualIncome = value; }
                }
                public int InstituteADV
                {
                    get { return _instituteADV; }
                    set { _instituteADV = value; }
                }
                public string InstituteADV_Other
                {
                    get { return _instituteADV_Other; }
                    set { _instituteADV_Other = value; }
                }
                public string Phase
                {
                    get { return _phase; }
                    set { _phase = value; }
                }

                public int District
                {
                    get { return _district; }
                    set { _district = value; }
                }
                public int Pass_Change
                {
                    get { return _passChange; }
                    set { _passChange = value; }
                }

                public int Specialize
                {
                    get { return _specialize; }
                    set { _specialize = value; }
                }
                public decimal Tot_Obt
                {
                    get { return _totObt; }
                    set { _totObt = value; }
                }
                public decimal Tot_Max
                {
                    get { return _totMax; }
                    set { _totMax = value; }
                }
                public decimal Tot_Per
                {
                    get { return _totPer; }
                    set { _totPer = value; }
                }
                public decimal Nata
                {
                    get { return _nata; }
                    set { _nata = value; }
                }




                //Added for JECRC
                public int MaritalStatus
                    {
                    get
                        {
                        return _MaritalStatus;
                        }
                    set
                        {
                        _MaritalStatus = value;
                        }
                    }
                public int Differently_Abled
                    {
                    get
                        {
                        return _Differently_Abled;
                        }
                    set
                        {
                        _Differently_Abled = value;
                        }
                    }
                public string Nature_Disability
                    {
                    get
                        {
                        return _Nature_Disability;
                        }
                    set
                        {
                        _Nature_Disability = value;
                        }
                    }
                public string Percentage_Disability
                    {
                    get
                        {
                        return _Percentage_Disability;
                        }
                    set
                        {
                        _Percentage_Disability = value;
                        }
                    }
                public int State_Domicile
                    {
                    get
                        {
                        return _State_Domicile;
                        }
                    set
                        {
                        _State_Domicile = value;
                        }
                    }
                public int Sports_Person
                    {
                    get
                        {
                        return _Sports_Person;
                        }
                    set
                        {
                        _Sports_Person = value;
                        }
                    }
                public int Sports_Represented
                    {
                    get
                        {
                        return _Sports_Represented;
                        }
                    set
                        {
                        _Sports_Represented = value;
                        }
                    }
                public string F_TelNumber
                    {
                    get
                        {
                        return _F_TelNumber;
                        }
                    set
                        {
                        _F_TelNumber = value;
                        }
                    }
                public string F_MobileNo
                    {
                    get
                        {
                        return _F_MobileNo;
                        }
                    set
                        {
                        _F_MobileNo = value;
                        }
                    }
                public int F_Occupation
                    {
                    get
                        {
                        return _F_Occupation;
                        }
                    set
                        {
                        _F_Occupation = value;
                        }
                    }
                public string F_Designation
                    {
                    get
                        {
                        return _F_Designation;
                        }
                    set
                        {
                        _F_Designation = value;
                        }
                    }
                public string F_EmailAddress
                    {
                    get
                        {
                        return _F_EmailAddress;
                        }
                    set
                        {
                        _F_EmailAddress = value;
                        }
                    }
                public string M_TelNumber
                    {
                    get
                        {
                        return _M_TelNumber;
                        }
                    set
                        {
                        _M_TelNumber = value;
                        }
                    }
                public string M_MobileNo
                    {
                    get
                        {
                        return _M_MobileNo;
                        }
                    set
                        {
                        _M_MobileNo = value;
                        }
                    }
                public int M_Occupation
                    {
                    get
                        {
                        return _M_Occupation;
                        }
                    set
                        {
                        _M_Occupation = value;
                        }
                    }
                public string M_Designation
                    {
                    get
                        {
                        return _M_Designation;
                        }
                    set
                        {
                        _M_Designation = value;
                        }
                    }
                public string M_EmailAddress
                    {
                    get
                        {
                        return _M_EmailAddress;
                        }
                    set
                        {
                        _M_EmailAddress = value;
                        }
                    }
                public string EMAILID
                    {
                    get
                        {
                        return _emailid;
                        }
                    set
                        {
                        _emailid = value;
                        }
                    }
                public string FATHERNAME
                    {
                    get
                        {
                        return _fatherName;
                        }
                    set
                        {
                        _fatherName = value;
                        }
                    }
                public string MOTHERNAME
                    {
                    get
                        {
                        return _motherName;
                        }
                    set
                        {
                        _motherName = value;
                        }
                    }
                public int GENDER
                    {
                    get
                        {
                        return _gender1;
                        }
                    set
                        {
                        _gender1 = value;
                        }
                    }
                public int NATIONALITY
                    {
                    get
                        {
                        return _nationalityNo;
                        }
                    set
                        {
                        _nationalityNo = value;
                        }
                    }
                public int CATEGORY
                    {
                    get
                        {
                        return _categoryNo;
                        }
                    set
                        {
                        _categoryNo = value;
                        }
                    }
                public string IDENTIFICATIONMARK
                    {
                    get
                        {
                        return _IDENTIFICATIONMARK;
                        }
                    set
                        {
                        _IDENTIFICATIONMARK = value;
                        }
                    }
                public string ADHAARNO
                    {
                    get
                        {
                        return _ADHAARNO;
                        }
                    set
                        {
                        _ADHAARNO = value;
                        }
                    }
                public int BLOODGRP
                    {
                    get
                        {
                        return _BLOODGRP;
                        }
                    set
                        {
                        _BLOODGRP = value;
                        }
                    }
                public int RELIGION
                    {
                    get
                        {
                        return _RELIGION;
                        }
                    set
                        {
                        _RELIGION = value;
                        }
                    }
                public string ALTERNATEEMAILID
                    {
                    get
                        {
                        return _aletremailid;
                        }
                    set
                        {
                        _aletremailid = value;
                        }
                    }
                public int Host_Trans
                    {
                    get
                        {
                        return _host_Trans;
                        }
                    set
                        {
                        _host_Trans = value;
                        }
                    }
                public string Host_Trans_Name
                    {
                    get
                        {
                        return _host_Trans_Name;
                        }
                    set
                        {
                        _host_Trans_Name = value;
                        }
                    }
                public int ParentsAnnualIncome
                    {
                    get
                        {
                        return _ParentsAnnualIncome;
                        }
                    set
                        {
                        _ParentsAnnualIncome = value;
                        }
                    }
                public string sportsName
                    {
                    get
                        {
                        return _sportsName;
                        }
                    set
                        {
                        _sportsName = value;
                        }
                    }
                public string sportsDoc
                    {
                    get
                        {
                        return _sportsDoc;
                        }
                    set
                        {
                        _sportsDoc = value;
                        }
                    }
                public string sportsDocPath
                    {
                    get
                        {
                        return _sportsDocPath;
                        }
                    set
                        {
                        _sportsDocPath = value;
                        }
                    }
                public string F_OccupationOther
                    {
                    get
                        {
                        return _F_OccupationOther;
                        }
                    set
                        {
                        _F_OccupationOther = value;
                        }
                    }
                public string M_OccupationOther
                    {
                    get
                        {
                        return _M_OccupationOther;
                        }
                    set
                        {
                        _M_OccupationOther = value;
                        }
                    }
                public int Qual_DegreeNO_PG
                {
                    get { return _qual_Degreeno_PG; }
                    set { _qual_Degreeno_PG = value; }
                }
                public int Branch_Of_Study_PG
                {
                    get { return _branch_of_Study_PG; }
                    set { _branch_of_Study_PG = value; }
                }
                public string Branch_Of_Study_Others_PG
                {
                    get { return _branch_of_Study_Others_PG; }
                    set { _branch_of_Study_Others_PG = value; }
                }
                public string Institute_Name_PG
                {
                    get { return _institute_Name_PG; }
                    set { _institute_Name_PG = value; }
                }
                public string University_Name_PG
                {
                    get { return _university_Name_PG; }
                    set { _university_Name_PG = value; }
                }
                public string Location_PG
                {
                    get { return _location_PG; }
                    set { _location_PG = value; }
                }
                public int Institute_State_PG
                {
                    get { return _institute_State_PG; }
                    set { _institute_State_PG = value; }
                }
                public int Per_CGPA_PG
                {
                    get { return _per_cgpa_PG; }
                    set { _per_cgpa_PG = value; }
                }
                public decimal Marks_Per_CGPA_PG
                {
                    get { return _marks_per_cgpa_PG; }
                    set { _marks_per_cgpa_PG = value; }
                }
                public int Semesterno_PG
                {
                    get { return _semesterno_PG; }
                    set { _semesterno_PG = value; }
                }

                public int Year_PG
                {
                    get { return _year_PG; }
                    set { _year_PG = value; }
                }
                public string Mode_Of_Study_PG
                {
                    get { return _mode_of_study_PG; }
                    set { _mode_of_study_PG = value; }
                }
                public string Name_Of_Test_PG
                {
                    get { return _name_of_test_PG; }
                    set { _name_of_test_PG = value; }
                }
                public decimal Score_PG
                {
                    get { return _score_PG; }
                    set { _score_PG = value; }
                }

                public string Qual_Degree_Others
                {
                    get { return _qualDegree_Others; }
                    set { _qualDegree_Others = value; }
                }
                #endregion
            }
        }
    }

}
