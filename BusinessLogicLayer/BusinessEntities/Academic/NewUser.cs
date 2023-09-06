using System;
using System.Data;
using System.Web;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class NewUser
            {
                #region Private Member

                private int _idno = 0;
                private int _sessionno = 0;
                private string _firstName = string.Empty;
                private string _middleName = string.Empty;
                private string _lastName = string.Empty;

                private string _fatherName = string.Empty;
                private string _motherName = string.Empty;
                //private int _mothertongue = 0;

                private string _Mobile = string.Empty;
                private string _std = string.Empty;
                private string _phone = string.Empty;
                private string _emailid = string.Empty;
                private DateTime _dob = DateTime.Today;
                private char _gender = ' ';


                private int _nationalityNo = 0;
                private int _categoryNo = 0;
                private string _city = string.Empty;

                private string _stateNo = string.Empty;
                //private int _casteNo = 0;
                private int _allIndiaRank = 0;
                private int _JEEEscore = 0;
                private int _JEEERollNo = 0;

                private int _SSCMath = 0;
                //private int _SSCTotal = 0;//Deepak: 07th Mar,15 changed for grade pt
                private double _SSCTotal = 0;
                private int _SSCOutOF = 0;
                private double _SSCAggr = 0;
                private int _HSCMath = 0;
                private int _HSCMathoutof = 0;
                private double _HSCMathAggr = 0;
                private int _HSCPhysics = 0;
                private int _HSCPhysicsoutof = 0;
                private double _HSCPhysicsAggr = 0;
                private int _HSCChemistry = 0;
                private int _HSCChemistryoutof = 0;
                private double _HSCChemistryAggr = 0;
                private int _HSC_PCMTotal = 0;
                private int _HSC_PCMTotaloutof = 0;
                private double _HSCPCMAggr = 0;
                private int _HSCTotal = 0;
                private int _HSCOutOf = 0;
                private double _HSCAggr = 0;
                private string _BranchPref = string.Empty;
                private byte[] _Photo = null;
                private byte[] _Studsign = null;

                //AS PER REQURIMENT ADDEDD THIS ONE 31-01-2014

                private string _pcity = string.Empty;
                private string _pstateNo = string.Empty;
                private string _cAddress = string.Empty;
                private string _pAddress = string.Empty;
                private string _pdistrictNo = string.Empty;
                private string _cdistrictNo = string.Empty;
                private string _cpin = string.Empty;
                private string _ppin = string.Empty;

                private string _aletremailid = string.Empty;
                private int _admcategory = 0;
                //12/2/2014
                private int _JEEPhysic = 0;
                private int _JEEChemistry = 0;
                private int _JEEMath = 0;
                private int _ADVJEEROLLNO = 0;
                private int _ADVJEESCORE = 0;
                private int _ADVJEERANK = 0;
                //22-2-2014 for NRI
                private string _passportno = string.Empty;
                private string _placeofissue = string.Empty;
                private string _issuecountry = string.Empty;
                private string _satregno = string.Empty;
                private string _location = string.Empty;
                private DateTime _issue = DateTime.Today;
                private DateTime _validupto = DateTime.Today;

                //FOR SPORTS START
                private int _SCIOLY_NATIONAL = 0;
                private int _SCIOLY_INTERNATIONAL = 0;
                private int _SPORT_STATE = 0;
                private int _SPORT_NATIONAL = 0;
                private int _SCHR_ARTIST = 0;
                private int _NCC_STATE = 0;
                private int _NCC_NATIONAL = 0;
                private int _TALENT_SEARCH = 0;
                private string _SCIOLY_DISCIPLINE = string.Empty;
                private string _SPORT_DESCRIPTION = string.Empty;
                private string _SPORT_NATDESCRIPTION = string.Empty;//07032014
                //FOR SPORTS END

                //For Start 12nd Rank & Roll no: Dt:07 Mar,14
                private string _HSC_DIRCT_ROLL = string.Empty;
                private int _HSC_DIRCT_RANK = 0;
                private int _SPORT_RANK = 0;
                private int _SPORT_NATRANK = 0;//07032014
                //End 

                //For SAT Start 10 Mar,14
                private int _SAT_MATH = 0;
                private int _SAT_PHYSICS = 0;
                private int _SAT_CHEMISTRY = 0;
                private int _SAT_TOTAL = 0;
                private string _NRI_LOCATION = string.Empty;
                //SAT END
                private string _COUNTRY = string.Empty;
                private string _P_COUNTRY = string.Empty;

                private int _SSCBOARD = 0;
                private string _SSCBOARDNAME = string.Empty;
                private string _SSCNAME = string.Empty;
                private string _SSCADDRESS = string.Empty;
                private int _MARKTYPE = 0;
                private double _SSCGD_OBT = 0;
                private double _SSCGD_MAX = 0;
                private double _SSCGD_PER = 0;
                private int _SSCCON_NO = 0;
                private int _SSCSTATE_NO = 0;
                private int _SSCDIS_NO = 0;
                private int _SSCCITY_NO = 0;
                private string _SSCCON_NAME = string.Empty;
                private string _SSCSTATE_NAME = string.Empty;
                private string _SSCDIS_NAME = string.Empty;
                private string _SSCCITY_NAME = string.Empty;
                private int _SSCPIN = 0;


                private int _HSCBOARD = 0;
                private string _HSCBOARDNAME = string.Empty;
                private string _HSCNAME = string.Empty;
                private string _HSCADDRESS = string.Empty;

                private int _HSCCON_NO = 0;
                private int _HSCSTATE_NO = 0;
                private int _HSCDIS_NO = 0;
                private int _HSCCITY_NO = 0;
                private string _HSCCON_NAME = string.Empty;
                private string _HSCSTATE_NAME = string.Empty;
                private string _HSCDIS_NAME = string.Empty;
                private string _HSCCITY_NAME = string.Empty;
                private int _HSCPIN = 0;

                private string _GUARDFATHER_NAME = string.Empty;
                private string _GUARDMOTHER_NAME = string.Empty;
                private string _GUARDEMAILID = string.Empty;
                private string _GUARDNAME = string.Empty;
                private string _GUARDRELATION = string.Empty;
                private string _GUARDMOBILENO = string.Empty;
                private int _GUARDSTATUS = 0;

                //Added by Manish on 04/09/2015
                private int _RELIGION = 0;
                private int _BLOODGRP = 0;
                private string _IDENTIFICATIONMARK = string.Empty;
                private string _ADHAARNO = string.Empty;
                private string _OTHERCCITY = string.Empty;
                private string _OTHERPCITY = string.Empty;
                private string _OTHERCSTATE = string.Empty;
                private string _OTHERPSTATE = string.Empty;
                private string _OTHERCDISTRICT = string.Empty;
                private string _OTHERPDISTRICT = string.Empty;
                private string _REGID = string.Empty;

                // For Bank Detalis 04/09/2015
                private string _ACCOUNTNO = string.Empty;
                private int _BANKNO = 0;
                private string _BRANCHNAME = string.Empty;
                private string _IFSCCODE = string.Empty;

                // For Last Qualification Details 04/09/2015
                private int _stlqno = 0;
                private int _uno = 0;
                private int _qualifyno = 0;
                private string _board_univ = string.Empty;
                private string _year_of_passing = string.Empty;
                private double _out_of_marks = 0.00;
                private double _obtained_marks = 0.00;
                private double _percentage = 0.00;
                private string _creator = string.Empty;
                private DateTime _created_date = DateTime.MinValue;
                private string _college_code = string.Empty;
                private string _examno = string.Empty;
                private string _examname = string.Empty;
                private string _board = string.Empty;
                private string _passingyear = string.Empty;
                private string _total = string.Empty;
                private string _getmarks = string.Empty;
                private string _percent = string.Empty;
                private string _Duration = string.Empty;
                private string _Subjects = string.Empty;
                private int _Statusno = 0;
                private string _EntrExam = string.Empty;
                /// <summary>
                /// Added by Nikhil L. on 05/09/2021 to add degree marks. 
                /// </summary>
                private double _degree_out_of_marks = 0.00;
                private double _degree_obtained_marks = 0.00;
                private double _degree_percentage = 0.00;

                // Added by Manish on 05-01-2015 for Other Information
                private string _Distnno = string.Empty;
                private string _Awardname = string.Empty;
                private string _Awardedby = string.Empty;
                private string _Year = string.Empty;
                private string _Remarks = string.Empty;
                private string _Reschno = string.Empty;
                private string _ProjectName = string.Empty;
                private string _ProjectDesc = string.Empty;
                private string _ProjectAgency = string.Empty;
                private string _ProjectResearch = string.Empty;
                private string _ResearchStudy = string.Empty;
                private string _OrderOfResearch = string.Empty;
                private string _Choice = string.Empty;
                private string _FellowName = string.Empty;
                private string _Amount = string.Empty;
                private string _Agency = string.Empty;
                private DateTime? _AppliDate = null;
                private string _Decision = string.Empty;
                private Boolean _Fellowship;
                private Boolean _Employed;
                private string _EmpName = string.Empty;
                private string _EmpOffice = string.Empty;
                private Boolean _Claim;
                private Boolean _BusService;
                private Boolean _HostelAcco;
                private int _EntrExamNo = 0;
                private double _EntrScore = 0;
                private Boolean _Handicapped;
                private int HandicapNo = 0;
                private Boolean _UnivRegistred;
                private string _Regno = string.Empty;
                private int _Expno = 0;
                private string _Orgname = string.Empty;
                private string _desg = string.Empty;
                private DateTime? _StartDate = null;
                private DateTime? _EndDate = null;

                //Added By "Rohit Kumar Tiwari" on 15-12-2015

                private int _MaritalStatus = 0;
                private int _Differently_Abled = 0;
                private string _Nature_Disability = string.Empty;
                private string _Percentage_Disability = string.Empty;
                private int _State_Domicile = 0;
                private int _Sports_Person = 0;

                private int _Sports_Represented = 0;
                private int _JHUEmployeeWard = 0;
                private string _NameEmployee = string.Empty;
                private string _Designation = string.Empty;
                private string _Department = string.Empty;
                private int _InternalCandidateInfo = 0;

                private string _Course = string.Empty;
                private string _EnrollmentNumber = string.Empty;
                //Father Details
                private string _F_FirstName = string.Empty;
                private string _F_MiddleName = string.Empty;
                private string _F_LastName = string.Empty;
                private string _F_TelNumber = string.Empty;
                private string _F_MobileNo = string.Empty;
                private int _F_Occupation = 0;
                private string _F_Designation = string.Empty;
                private string _F_EmailAddress = string.Empty;
                //Mother Details
                private string _M_FirstName = string.Empty;
                private string _M_MiddleName = string.Empty;
                private string _M_LastName = string.Empty;
                private string _M_TelNumber = string.Empty;
                private string _M_MobileNo = string.Empty;
                private int _M_Occupation = 0;
                private string _M_Designation = string.Empty;
                private string _M_EmailAddress = string.Empty;

                private int _SchoolName = 0;
                private string _OtherReligion = string.Empty;

                private string _Employee_id = string.Empty;
                private int _ParentsAnnualIncome = 0;

                //****ADDED BY: MD. REHBAR SHEIKH ON 10-02-2018*******
                private int _countryid = 0;
                private int _stateid = 0;
                private int _cityid = 0;

                private double _cgpa = 0.00;

                private int _pcountryid = 0;
                private int _pstateid = 0;
                private int _pcityid = 0;

                //*****************************************************


                //ADDED ON DATE 04/03/2018//
                private int _QUAL_PATTERN = 0;
                private int _QUAL_STATUS = 0;
                private int _QUAL_DURATION = 0;
                private int _QUAL_YEAR_SEM = 0;

                //ADDED BY:ROHIT KUMAR TIWARI ON 07-03-2018

                private string _Country_Code = string.Empty;

                //Added by Sunita- 12032018
                private string _Nri_State = string.Empty;

                private string _jhRCA_ExamName = string.Empty;
                private int _jhRCA_Year = 0;

                private string _jhRCA_RollNo = string.Empty;
                private string _jhRCA_Result = string.Empty;

                private int _BRANCHNO = 0;

                // private int _ParentsAnnualIncome=0;
                // private decimal _ParentsAnnualIncome = 0; //comment on date

                /*********Added by dileep kare for U32 Educational Details*********/
                private int _U32_Method_Sub = 0;
                private int _12th_subno = 0;
                private string _12th_subtype = string.Empty;
                private string _12th_Sub_name = string.Empty;
                /************Added by Nikhil L. for other university********/
                private int _otherUniv = 0;
                private string _otherUnivName = string.Empty;
                private string _nameofUniversity = string.Empty;

                private string _nameofLastDegree = string.Empty;
                private string _yearLastDegree = string.Empty;

                private int _host_Trans = 0;
                private string _host_Trans_Name = string.Empty;

                public string JHRCA_ExamName
                {
                    get { return _jhRCA_ExamName; }
                    set { _jhRCA_ExamName = value; }
                }
                public int JHRCA_Year
                {
                    get { return _jhRCA_Year; }
                    set { _jhRCA_Year = value; }
                }

                public string JHRCA_RollNo
                {
                    get { return _jhRCA_RollNo; }
                    set { _jhRCA_RollNo = value; }
                }

                public string JHRCA_Result
                {
                    get { return _jhRCA_Result; }
                    set { _jhRCA_Result = value; }
                }
                public int MaritalStatus
                {
                    get { return _MaritalStatus; }
                    set { _MaritalStatus = value; }
                }
                public int Differently_Abled
                {
                    get { return _Differently_Abled; }
                    set { _Differently_Abled = value; }
                }
                public string Nature_Disability
                {
                    get { return _Nature_Disability; }
                    set { _Nature_Disability = value; }
                }
                public string Percentage_Disability
                {
                    get { return _Percentage_Disability; }
                    set { _Percentage_Disability = value; }
                }
                public int State_Domicile
                {
                    get { return _State_Domicile; }
                    set { _State_Domicile = value; }
                }
                public int Sports_Person
                {
                    get { return _Sports_Person; }
                    set { _Sports_Person = value; }
                }
                public int Sports_Represented
                {
                    get { return _Sports_Represented; }
                    set { _Sports_Represented = value; }
                }
                public int JHUEmployeeWard
                {
                    get { return _JHUEmployeeWard; }
                    set { _JHUEmployeeWard = value; }
                }
                public string NameEmployee
                {
                    get { return _NameEmployee; }
                    set { _NameEmployee = value; }
                }
                public string Designation
                {
                    get { return _Designation; }
                    set { _Designation = value; }
                }
                public string Department
                {
                    get { return _Department; }
                    set { _Department = value; }
                }
                public int InternalCandidateInfo
                {
                    get { return _InternalCandidateInfo; }
                    set { _InternalCandidateInfo = value; }
                }

                public string Course
                {
                    get { return _Course; }
                    set { _Course = value; }
                }
                public string EnrollmentNumber
                {
                    get { return _EnrollmentNumber; }
                    set { _EnrollmentNumber = value; }
                }


                //Father Details

                public string F_FirstName
                {
                    get { return _F_FirstName; }
                    set { _F_FirstName = value; }
                }
                public string F_MiddleName
                {
                    get { return _F_MiddleName; }
                    set { _F_MiddleName = value; }
                }
                public string F_LastName
                {
                    get { return _F_LastName; }
                    set { _F_LastName = value; }
                }
                public string F_TelNumber
                {
                    get { return _F_TelNumber; }
                    set { _F_TelNumber = value; }
                }
                public string F_MobileNo
                {
                    get { return _F_MobileNo; }
                    set { _F_MobileNo = value; }
                }
                public int F_Occupation
                {
                    get { return _F_Occupation; }
                    set { _F_Occupation = value; }
                }
                public string F_Designation
                {
                    get { return _F_Designation; }
                    set { _F_Designation = value; }
                }
                public string F_EmailAddress
                {
                    get { return _F_EmailAddress; }
                    set { _F_EmailAddress = value; }
                }

                //Mother Details
                public string M_FirstName
                {
                    get { return _M_FirstName; }
                    set { _M_FirstName = value; }
                }
                public string M_MiddleName
                {
                    get { return _M_MiddleName; }
                    set { _M_MiddleName = value; }
                }
                public string M_LastName
                {
                    get { return _M_LastName; }
                    set { _M_LastName = value; }
                }
                public string M_TelNumber
                {
                    get { return _M_TelNumber; }
                    set { _M_TelNumber = value; }
                }
                public string M_MobileNo
                {
                    get { return _M_MobileNo; }
                    set { _M_MobileNo = value; }
                }
                public int M_Occupation
                {
                    get { return _M_Occupation; }
                    set { _M_Occupation = value; }
                }
                public string M_Designation
                {
                    get { return _M_Designation; }
                    set { _M_Designation = value; }
                }
                public string M_EmailAddress
                {
                    get { return _M_EmailAddress; }
                    set { _M_EmailAddress = value; }
                }

                //public int ParentsAnnualIncome
                //{
                //    get { return _ParentsAnnualIncome; }
                //    set { _ParentsAnnualIncome = value; }
                //}
                public int SchoolName
                {
                    get { return _SchoolName; }
                    set { _SchoolName = value; }
                }


                public string OtherReligion
                {
                    get { return _OtherReligion; }
                    set { _OtherReligion = value; }
                }

                //public decimal ParentsAnnualIncome				
                //{
                //    get { return _ParentsAnnualIncome; }
                //    set { _ParentsAnnualIncome = value; }
                //}
                public string Employee_id
                {
                    get { return _Employee_id; }
                    set { _Employee_id = value; }
                }


                public int ParentsAnnualIncome
                {
                    get { return _ParentsAnnualIncome; }
                    set { _ParentsAnnualIncome = value; }
                }



                //****ADDED BY: MD. REHBAR SHEIKH ON 10-02-2018*******
                public int CountryId
                {
                    get { return _countryid; }
                    set { _countryid = value; }
                }
                public int StateId
                {
                    get { return _stateid; }
                    set { _stateid = value; }
                }
                public int CityId
                {
                    get { return _cityid; }
                    set { _cityid = value; }
                }
                public double Cgpa
                {
                    get { return this._cgpa; }
                    set { this._cgpa = value; }
                }

                //Added By Sunita
                public int PCountryId
                {
                    get { return _pcountryid; }
                    set { _pcountryid = value; }
                }
                public int PStateId
                {
                    get { return _pstateid; }
                    set { _pstateid = value; }
                }
                public int PCityId
                {
                    get { return _pcityid; }
                    set { _pcityid = value; }
                }

                //ADDED ON DATE 04/03/2018
                public int QUAL_PATTERN
                {
                    get { return _QUAL_PATTERN; }
                    set { _QUAL_PATTERN = value; }
                }
                public int QUAL_STATUS
                {
                    get { return _QUAL_STATUS; }
                    set { _QUAL_STATUS = value; }
                }
                public int QUAL_DURATION
                {
                    get { return _QUAL_DURATION; }
                    set { _QUAL_DURATION = value; }
                }
                public int QUAL_YEAR_SEM
                {
                    get { return _QUAL_YEAR_SEM; }
                    set { _QUAL_YEAR_SEM = value; }
                }


                //ADDED BY:ROHIT KUMAR TIWARI ON 07-03-2018

                public string Country_Code
                {
                    get { return _Country_Code; }
                    set { _Country_Code = value; }
                }

                //Added BY Sunita--12032018
                public string NRI_STATE
                {
                    get { return _Nri_State; }
                    set { _Nri_State = value; }
                }

                #endregion

                #region Public Member


                public int IDNO
                {
                    get { return _idno; }
                    set { _idno = value; }
                }
                public int SESSIONNO
                {
                    get { return _sessionno; }
                    set { _sessionno = value; }
                }
                public string MOBILENO
                {
                    get { return _Mobile; }
                    set { _Mobile = value; }
                }
                public string EMAILID
                {
                    get { return _emailid; }
                    set { _emailid = value; }
                }
                public string FIRSTNAME
                {
                    get { return _firstName; }
                    set { _firstName = value; }
                }
                public string MIDDLENAME
                {
                    get { return _middleName; }
                    set { _middleName = value; }
                }
                public string LASTNAME
                {
                    get { return _lastName; }
                    set { _lastName = value; }
                }
                public string FATHERNAME
                {
                    get { return _fatherName; }
                    set { _fatherName = value; }
                }
                public string MOTHERNAME
                {
                    get { return _motherName; }
                    set { _motherName = value; }
                }
                public string STD
                {
                    get { return _std; }
                    set { _std = value; }
                }
                public string PHONE
                {
                    get { return _phone; }
                    set { _phone = value; }
                }
                public DateTime DOB
                {
                    get { return _dob; }
                    set { _dob = value; }
                }
                public char GENDER
                {
                    get { return _gender; }
                    set { _gender = value; }
                }
                //public int MTONGUE
                //{
                //    get { return _mothertongue; }
                //    set { _mothertongue = value; }
                //}


                public int NATIONALITY
                {
                    get { return _nationalityNo; }
                    set { _nationalityNo = value; }
                }
                public int CATEGORY
                {
                    get { return _categoryNo; }
                    set { _categoryNo = value; }
                }
                public string CITY
                {
                    get { return _city; }
                    set { _city = value; }
                }
                public string STATE
                {
                    get { return _stateNo; }
                    set { _stateNo = value; }
                }
                //Added by Manish on 04/09/2015
                public string IDENTIFICATIONMARK
                {
                    get { return _IDENTIFICATIONMARK; }
                    set { _IDENTIFICATIONMARK = value; }
                }
                public string ADHAARNO
                {
                    get { return _ADHAARNO; }
                    set { _ADHAARNO = value; }
                }
                public int BLOODGRP
                {
                    get { return _BLOODGRP; }
                    set { _BLOODGRP = value; }
                }
                public int RELIGION
                {
                    get { return _RELIGION; }
                    set { _RELIGION = value; }
                }
                //public int CASTE
                //{
                //    get { return _casteNo; }
                //    set { _casteNo = value; }
                //}
                public int ALLINDIA_RANK
                {
                    get { return _allIndiaRank; }
                    set { _allIndiaRank = value; }
                }
                public int JEEESCORE
                {
                    get { return _JEEEscore; }
                    set { _JEEEscore = value; }
                }
                public int JEEEROLLNO
                {
                    get { return _JEEERollNo; }
                    set { _JEEERollNo = value; }
                }

                public int SSCMATH
                {
                    get { return _SSCMath; }
                    set { _SSCMath = value; }
                }
                public double SSCTOTAL
                {
                    get { return _SSCTotal; }
                    set { _SSCTotal = value; }
                }
                public int SSCOUTOF
                {
                    get { return _SSCOutOF; }
                    set { _SSCOutOF = value; }
                }
                public double SSCAGGRE
                {
                    get { return _SSCAggr; }
                    set { _SSCAggr = value; }
                }
                public int HSCMATH
                {
                    get { return _HSCMath; }
                    set { _HSCMath = value; }

                }
                public int HSCMATHOUTOF
                {
                    get { return _HSCMathoutof; }
                    set { _HSCMathoutof = value; }

                }
                public int HSCPHYSICS
                {
                    get { return _HSCPhysics; }
                    set { _HSCPhysics = value; }

                }
                public int HSCPHYSICSOUTOF
                {
                    get { return _HSCPhysicsoutof; }
                    set { _HSCPhysicsoutof = value; }

                }
                public int HSCCHEMISTRY
                {
                    get { return _HSCChemistry; }
                    set { _HSCChemistry = value; }

                }
                public int HSCCHEMISTRYOUTOF
                {
                    get { return _HSCChemistryoutof; }
                    set { _HSCChemistryoutof = value; }

                }
                public int HSCPCM_TOTAL
                {
                    get { return _HSC_PCMTotal; }
                    set { _HSC_PCMTotal = value; }

                }
                public int HSCPCM_TOTALOUTOF
                {
                    get { return _HSC_PCMTotaloutof; }
                    set { _HSC_PCMTotaloutof = value; }

                }
                public int HSCTOTAL
                {
                    get { return _HSCTotal; }
                    set { _HSCTotal = value; }

                }
                public int HSCOUTOF
                {
                    get { return _HSCOutOf; }
                    set { _HSCOutOf = value; }

                }
                public double HSCAGGRE
                {
                    get { return _HSCAggr; }
                    set { _HSCAggr = value; }
                }
                public string BRANCHPREF
                {
                    get { return _BranchPref; }
                    set { _BranchPref = value; }
                }
                public byte[] PHOTO
                {
                    get { return _Photo; }
                    set { _Photo = value; }

                }

                //AS PER REQURIMENT ADDEDD THIS ONE 31-01-2014
                public string PCITY
                {
                    get { return _pcity; }
                    set { _pcity = value; }
                }
                public string PSTATE
                {
                    get { return _pstateNo; }
                    set { _pstateNo = value; }
                }
                public string CDISTRICT
                {
                    get { return _cdistrictNo; }
                    set { _cdistrictNo = value; }
                }
                public string PDISTRICT
                {
                    get { return _pdistrictNo; }
                    set { _pdistrictNo = value; }
                }
                public string CADDRESS
                {
                    get { return _cAddress; }
                    set { _cAddress = value; }
                }
                public string PADDRESS
                {
                    get { return _pAddress; }
                    set { _pAddress = value; }
                }

                public string ALTERNATEEMAILID
                {
                    get { return _aletremailid; }
                    set { _aletremailid = value; }
                }
                public string CPINNO
                {
                    get { return _cpin; }
                    set { _cpin = value; }
                }
                public string PPINNO
                {
                    get { return _ppin; }
                    set { _ppin = value; }
                }
                public int ADMCATEGORY
                {
                    get { return _admcategory; }
                    set { _admcategory = value; }
                }
                public double HSCMATHAGGRE
                {
                    get { return _HSCMathAggr; }
                    set { _HSCMathAggr = value; }
                }
                public double HSCPHYSICAGGRE
                {
                    get { return _HSCPhysicsAggr; }
                    set { _HSCPhysicsAggr = value; }
                }
                public double HSCCHEMISAGGRE
                {
                    get { return _HSCChemistryAggr; }
                    set { _HSCChemistryAggr = value; }
                }
                public double HSCPCMAGGRE
                {
                    get { return _HSCPCMAggr; }
                    set { _HSCPCMAggr = value; }
                }

                //12/2/2014

                public int JEEPHYSICS
                {
                    get { return _JEEPhysic; }
                    set { _JEEPhysic = value; }
                }
                public int JEECHEMISTRY
                {
                    get { return _JEEChemistry; }
                    set { _JEEChemistry = value; }
                }
                public int JEEMATH
                {
                    get { return _JEEMath; }
                    set { _JEEMath = value; }
                }
                public int ADVJEEROLLNO
                {
                    get { return _ADVJEEROLLNO; }
                    set { _ADVJEEROLLNO = value; }
                }
                public int ADVJEESCORE
                {
                    get { return _ADVJEESCORE; }
                    set { _ADVJEESCORE = value; }
                }
                public int ADVJEERANK
                {
                    get { return _ADVJEERANK; }
                    set { _ADVJEERANK = value; }
                }
                //22-2-2014 for NRI
                public string PASSPORT
                {
                    get { return _passportno; }
                    set { _passportno = value; }
                }
                public string PLACEOFISSUE
                {
                    get { return _placeofissue; }
                    set { _placeofissue = value; }
                }
                public string ISSUINGCOUNTRY
                {
                    get { return _issuecountry; }
                    set { _issuecountry = value; }
                }
                public string SAT_REGNO
                {
                    get { return _satregno; }
                    set { _satregno = value; }
                }
                public string LOCATION
                {
                    get { return _location; }
                    set { _location = value; }
                }
                public DateTime ISSUEDATE
                {
                    get { return _issue; }
                    set { _issue = value; }
                }
                public DateTime VALIDUPTO
                {
                    get { return _validupto; }
                    set { _validupto = value; }
                }
                //FOR SPORTS START  06MAR14                         

                public int SCIOLY_NATIONAL
                {
                    get { return _SCIOLY_NATIONAL; }
                    set { _SCIOLY_NATIONAL = value; }
                }
                public int SCIOLY_INTERNATIONAL
                {
                    get { return _SCIOLY_INTERNATIONAL; }
                    set { _SCIOLY_INTERNATIONAL = value; }
                }
                public int SPORT_STATE
                {
                    get { return _SPORT_STATE; }
                    set { _SPORT_STATE = value; }
                }
                public int SPORT_NATIONAL
                {
                    get { return _SPORT_NATIONAL; }
                    set { _SPORT_NATIONAL = value; }
                }
                public int SCHR_ARTIST
                {
                    get { return _SCHR_ARTIST; }
                    set { _SCHR_ARTIST = value; }
                }
                public int NCC_STATE
                {
                    get { return _NCC_STATE; }
                    set { _NCC_STATE = value; }
                }
                public int NCC_NATIONAL
                {
                    get { return _NCC_NATIONAL; }
                    set { _NCC_NATIONAL = value; }
                }
                public int TALENT_SEARCH
                {
                    get { return _TALENT_SEARCH; }
                    set { _TALENT_SEARCH = value; }
                }
                public string SCIOLY_DISCIPLINE
                {
                    get { return _SCIOLY_DISCIPLINE; }
                    set { _SCIOLY_DISCIPLINE = value; }
                }
                public string SPORT_DESCRIPTION
                {
                    get { return _SPORT_DESCRIPTION; }
                    set { _SPORT_DESCRIPTION = value; }
                }
                public string SPORT_NATDESCRIPTION
                {
                    get { return _SPORT_NATDESCRIPTION; }
                    set { _SPORT_NATDESCRIPTION = value; }
                }
                public string HSC_DIRCT_ROLL//*
                {
                    get { return _HSC_DIRCT_ROLL; }
                    set { _HSC_DIRCT_ROLL = value; }
                }
                public int HSC_DIRCT_RANK
                {
                    get { return _HSC_DIRCT_RANK; }
                    set { _HSC_DIRCT_RANK = value; }
                }
                public int SPORT_RANK
                {
                    get { return _SPORT_RANK; }
                    set { _SPORT_RANK = value; }
                }
                public int SPORT_NATRANK
                {
                    get { return _SPORT_NATRANK; }
                    set { _SPORT_NATRANK = value; }
                }
                //FOR SPORTS END

                //For SAT Start 10 Mar, 14
                public int SAT_MATH
                {
                    get { return _SAT_MATH; }
                    set { _SAT_MATH = value; }
                }

                public int SAT_PHYSICS
                {
                    get { return _SAT_PHYSICS; }
                    set { _SAT_PHYSICS = value; }
                }

                public int SAT_CHEMISTRY
                {
                    get { return _SAT_CHEMISTRY; }
                    set { _SAT_CHEMISTRY = value; }
                }

                public int SAT_TOTAL
                {
                    get { return _SAT_TOTAL; }
                    set { _SAT_TOTAL = value; }
                }

                public string NRI_LOCATION
                {
                    get { return _NRI_LOCATION; }
                    set { _NRI_LOCATION = value; }
                }
                public string COUNTRY
                {
                    get { return _COUNTRY; }
                    set { _COUNTRY = value; }
                }
                public string P_COUNTRY
                {
                    get { return _P_COUNTRY; }
                    set { _P_COUNTRY = value; }
                }

                public int SSCBOARD
                {
                    get { return _SSCBOARD; }
                    set { _SSCBOARD = value; }
                }
                public string SSCBOARDNAME
                {
                    get { return _SSCBOARDNAME; }
                    set { _SSCBOARDNAME = value; }
                }
                public int MARKTYPE
                {
                    get { return _MARKTYPE; }
                    set { _MARKTYPE = value; }
                }
                public int SSCCON_NO
                {
                    get { return _SSCCON_NO; }
                    set { _SSCCON_NO = value; }
                }
                public int SSCSTATE_NO
                {
                    get { return _SSCSTATE_NO; }
                    set { _SSCSTATE_NO = value; }
                }
                public int SSCDIS_NO
                {
                    get { return _SSCDIS_NO; }
                    set { _SSCDIS_NO = value; }
                }
                public int SSCCITY_NO
                {
                    get { return _SSCCITY_NO; }
                    set { _SSCCITY_NO = value; }
                }
                public int SSCPIN
                {
                    get { return _SSCPIN; }
                    set { _SSCPIN = value; }
                }
                public string SSCNAME
                {
                    get { return _SSCNAME; }
                    set { _SSCNAME = value; }
                }
                public string SSCADDRESS
                {
                    get { return _SSCADDRESS; }
                    set { _SSCADDRESS = value; }
                }
                public string SSCCON_NAME
                {
                    get { return _SSCCON_NAME; }
                    set { _SSCCON_NAME = value; }
                }
                public string SSCSTATE_NAME
                {
                    get { return _SSCSTATE_NAME; }
                    set { _SSCSTATE_NAME = value; }
                }
                public string SSCDIS_NAME
                {
                    get { return _SSCDIS_NAME; }
                    set { _SSCDIS_NAME = value; }
                }
                public string SSCCITY_NAME
                {
                    get { return _SSCCITY_NAME; }
                    set { _SSCCITY_NAME = value; }
                }
                public double SSCGD_OBT
                {
                    get { return _SSCGD_OBT; }
                    set { _SSCGD_OBT = value; }
                }
                public double SSCGD_MAX
                {
                    get { return _SSCGD_MAX; }
                    set { _SSCGD_MAX = value; }
                }
                public double SSCGD_PER
                {
                    get { return _SSCGD_PER; }
                    set { _SSCGD_PER = value; }
                }

                public int HSCBOARD
                {
                    get { return _HSCBOARD; }
                    set { _HSCBOARD = value; }
                }
                public string HSCBOARDNAME
                {
                    get { return _HSCBOARDNAME; }
                    set { _HSCBOARDNAME = value; }
                }
                public int HSCCON_NO
                {
                    get { return _HSCCON_NO; }
                    set { _HSCCON_NO = value; }
                }
                public int HSCSTATE_NO
                {
                    get { return _HSCSTATE_NO; }
                    set { _HSCSTATE_NO = value; }
                }
                public int HSCDIS_NO
                {
                    get { return _HSCDIS_NO; }
                    set { _HSCDIS_NO = value; }
                }
                public int HSCCITY_NO
                {
                    get { return _HSCCITY_NO; }
                    set { _HSCCITY_NO = value; }
                }
                public int HSCPIN
                {
                    get { return _HSCPIN; }
                    set { _HSCPIN = value; }
                }
                public string HSCNAME
                {
                    get { return _HSCNAME; }
                    set { _HSCNAME = value; }
                }
                public string HSCADDRESS
                {
                    get { return _HSCADDRESS; }
                    set { _HSCADDRESS = value; }
                }
                public string HSCCON_NAME
                {
                    get { return _HSCCON_NAME; }
                    set { _HSCCON_NAME = value; }
                }
                public string HSCSTATE_NAME
                {
                    get { return _HSCSTATE_NAME; }
                    set { _HSCSTATE_NAME = value; }
                }
                public string HSCDIS_NAME
                {
                    get { return _HSCDIS_NAME; }
                    set { _HSCDIS_NAME = value; }
                }
                public string HSCCITY_NAME
                {
                    get { return _HSCCITY_NAME; }
                    set { _HSCCITY_NAME = value; }
                }

                public string GUARDFATHER_NAME
                {
                    get { return _GUARDFATHER_NAME; }
                    set { _GUARDFATHER_NAME = value; }
                }
                public string GUARDMOTHER_NAME
                {
                    get { return _GUARDMOTHER_NAME; }
                    set { _GUARDMOTHER_NAME = value; }
                }
                public string GUARDEMAILID
                {
                    get { return _GUARDEMAILID; }
                    set { _GUARDEMAILID = value; }
                }
                public string GUARDNAME
                {
                    get { return _GUARDNAME; }
                    set { _GUARDNAME = value; }
                }
                public string GUARDRELATION
                {
                    get { return _GUARDRELATION; }
                    set { _GUARDRELATION = value; }
                }
                public string GUARDMOBILENO
                {
                    get { return _GUARDMOBILENO; }
                    set { _GUARDMOBILENO = value; }
                }
                public int GUARDSTATUS
                {
                    get { return _GUARDSTATUS; }
                    set { _GUARDSTATUS = value; }
                }
                //SAT End
                //SAT End

                // For Bank Detalis 04/09/2015
                public string IFSCCODE
                {
                    get { return _IFSCCODE; }
                    set { _IFSCCODE = value; }
                }
                public string ACCOUNTNO
                {
                    get { return _ACCOUNTNO; }
                    set { _ACCOUNTNO = value; }
                }
                public int BANKNO
                {
                    get { return _BANKNO; }
                    set { _BANKNO = value; }
                }
                public string BRANCHNAME
                {
                    get { return _BRANCHNAME; }
                    set { _BRANCHNAME = value; }
                }
                // Bank End
                // For Last Qualification Details 04/09/2015
                public int StlQno
                {
                    get { return this._stlqno; }
                    set { this._stlqno = value; }
                }
                public int UNo
                {
                    get { return this._uno; }
                    set { this._uno = value; }
                }
                public int QualifyNo
                {
                    get { return this._qualifyno; }
                    set { this._qualifyno = value; }
                }
                public string BoardUniversity
                {
                    get { return this._board_univ; }
                    set { this._board_univ = value; }
                }
                public string PassingYear
                {
                    get { return this._year_of_passing; }
                    set { this._year_of_passing = value; }
                }
                public double TotalMarks
                {
                    get { return this._out_of_marks; }
                    set { this._out_of_marks = value; }
                }
                public double MarksObtained
                {
                    get { return this._obtained_marks; }
                    set { this._obtained_marks = value; }
                }
                public double Percentage
                {
                    get { return this._percentage; }
                    set { this._percentage = value; }
                }
                public string Creator
                {
                    get { return this._creator; }
                    set { this._creator = value; }
                }
                public DateTime CreatedDate
                {
                    get { return this._created_date; }
                    set { this._created_date = value; }
                }
                public string CollegeCode
                {
                    get { return this._college_code; }
                    set { this._college_code = value; }
                }

                public string ExamNos
                {
                    get { return this._examno; }
                    set { this._examno = value; }
                }
                public string BoardName
                {
                    get { return this._board; }
                    set { this._board = value; }
                }
                public string YearPassing
                {
                    get { return this._passingyear; }
                    set { this._passingyear = value; }
                }
                public string MarksTotal
                {
                    get { return this._total; }
                    set { this._total = value; }
                }
                public string GetMarks
                {
                    get { return this._getmarks; }
                    set { this._getmarks = value; }
                }
                public string Percent
                {
                    get { return this._percent; }
                    set { this._percent = value; }
                }
                public string ExamName
                {
                    get { return _examname; }
                    set { _examname = value; }
                }
                public string Subjects
                {
                    get { return _Subjects; }
                    set { _Subjects = value; }
                }
                public string Duration
                {
                    get { return _Duration; }
                    set { _Duration = value; }
                }
                public int Statusno
                {
                    get { return _Statusno; }
                    set { _Statusno = value; }
                }
                public string OTHERPDISTRICT
                {
                    get { return _OTHERPDISTRICT; }
                    set { _OTHERPDISTRICT = value; }
                }
                public string OTHERCDISTRICT
                {
                    get { return _OTHERCDISTRICT; }
                    set { _OTHERCDISTRICT = value; }
                }
                public string OTHERCCITY
                {
                    get { return _OTHERCCITY; }
                    set { _OTHERCCITY = value; }
                }
                public string OTHERPCITY
                {
                    get { return _OTHERPCITY; }
                    set { _OTHERPCITY = value; }
                }
                public string OTHERCSTATE
                {
                    get { return _OTHERCSTATE; }
                    set { _OTHERCSTATE = value; }
                }
                public string OTHERPSTATE
                {
                    get { return _OTHERPSTATE; }
                    set { _OTHERPSTATE = value; }
                }
                public string REGID
                {
                    get { return _REGID; }
                    set { _REGID = value; }
                }
                public byte[] Studsign
                {
                    get { return _Studsign; }
                    set { _Studsign = value; }
                }
                public string EntrExam
                {
                    get { return _EntrExam; }
                    set { _EntrExam = value; }
                }

                public int Host_Trans
                {
                    get { return _host_Trans; }
                    set { _host_Trans = value; }
                }
                public string Host_Trans_Name
                {
                    get { return _host_Trans_Name; }
                    set { _host_Trans_Name = value; }
                }
                /// <summary>
                /// Added By Nikhil L. on 05/09/2021 to add degree marks.
                /// </summary>
                public double DegreeTotalMarks
                {
                    get { return this._degree_out_of_marks; }
                    set { this._degree_out_of_marks = value; }
                }
                public double DegreeMarksObtained
                {
                    get { return this._degree_obtained_marks; }
                    set { this._degree_obtained_marks = value; }
                }
                public double DegreePercentage
                {
                    get { return this._degree_percentage; }
                    set { this._degree_percentage = value; }
                }
                // Added by Manish on 05-01-2015 for Other Information

                public string Distnno
                {
                    get { return _Distnno; }
                    set { _Distnno = value; }
                }
                public string Awardname
                {
                    get { return _Awardname; }
                    set { _Awardname = value; }
                }
                public string Awardedby
                {
                    get { return _Awardedby; }
                    set { _Awardedby = value; }
                }
                public string Year
                {
                    get { return _Year; }
                    set { _Year = value; }
                }
                public string Remarks
                {
                    get { return _Remarks; }
                    set { _Remarks = value; }
                }
                public string Reschno
                {
                    get { return _Reschno; }
                    set { _Reschno = value; }
                }
                public string ProjectName
                {
                    get { return _ProjectName; }
                    set { _ProjectName = value; }
                }
                public string ProjectDesc
                {
                    get { return _ProjectDesc; }
                    set { _ProjectDesc = value; }
                }
                public string ProjectAgency
                {
                    get { return _ProjectAgency; }
                    set { _ProjectAgency = value; }
                }
                public string ProjectResearch
                {
                    get { return _ProjectResearch; }
                    set { _ProjectResearch = value; }
                }
                public string ResearchStudy
                {
                    get { return _ResearchStudy; }
                    set { _ResearchStudy = value; }
                }
                public string OrderOfResearch
                {
                    get { return _OrderOfResearch; }
                    set { _OrderOfResearch = value; }
                }
                public string Choice
                {
                    get { return _Choice; }
                    set { _Choice = value; }
                }
                public string FellowName
                {
                    get { return _FellowName; }
                    set { _FellowName = value; }
                }
                public string Amount
                {
                    get { return _Amount; }
                    set { _Amount = value; }
                }
                public string Agency
                {
                    get { return _Agency; }
                    set { _Agency = value; }
                }

                public string Decision
                {
                    get { return _Decision; }
                    set { _Decision = value; }
                }
                public Boolean Fellowship
                {
                    get { return _Fellowship; }
                    set { _Fellowship = value; }
                }
                public Boolean Employed
                {
                    get { return _Employed; }
                    set { _Employed = value; }
                }
                public string EmpName
                {
                    get { return _EmpName; }
                    set { _EmpName = value; }
                }
                public string EmpOffice
                {
                    get { return _EmpOffice; }
                    set { _EmpOffice = value; }
                }
                public Boolean Claim
                {
                    get { return _Claim; }
                    set { _Claim = value; }
                }
                public Boolean BusService
                {
                    get { return _BusService; }
                    set { _BusService = value; }
                }
                public Boolean HostelAcco
                {
                    get { return _HostelAcco; }
                    set { _HostelAcco = value; }
                }
                public int EntrExamNo
                {
                    get { return _EntrExamNo; }
                    set { _EntrExamNo = value; }
                }
                public double EntrScore
                {
                    get { return _EntrScore; }
                    set { _EntrScore = value; }
                }
                public DateTime? AppliDate
                {
                    get { return _AppliDate; }
                    set { _AppliDate = value; }
                }
                public Boolean Handicapped
                {
                    get { return _Handicapped; }
                    set { _Handicapped = value; }
                }
                public int Handicapno
                {
                    get { return HandicapNo; }
                    set { HandicapNo = value; }
                }
                public Boolean UnivRegistred
                {
                    get { return _UnivRegistred; }
                    set { _UnivRegistred = value; }
                }
                public string Regno
                {
                    get { return _Regno; }
                    set { _Regno = value; }
                }
                public int Expno
                {
                    get { return _Expno; }
                    set { _Expno = value; }
                }
                public string Orgname
                {
                    get { return _Orgname; }
                    set { _Orgname = value; }
                }
                public string Desg
                {
                    get { return _desg; }
                    set { _desg = value; }
                }
                public DateTime? StartDate
                {
                    get { return _StartDate; }
                    set { _StartDate = value; }
                }
                public DateTime? EndDate
                {
                    get { return _EndDate; }
                    set { _EndDate = value; }
                }

                public int BRANCHNO
                {
                    get { return _BRANCHNO; }
                    set { _BRANCHNO = value; }
                }
                public int XIIth_SubNo
                {
                    get { return _12th_subno; }
                    set { _12th_subno = value; }
                }
                public string XIIth_subtype
                {
                    get { return _12th_subtype; }
                    set { _12th_subtype = value; }
                }

                public string XIIth_Sub_name
                {
                    get { return _12th_Sub_name; }
                    set { _12th_Sub_name = value; }
                }
                public int U32_Method_Sub
                {
                    get { return _U32_Method_Sub; }
                    set { _U32_Method_Sub = value; }
                }

                public int OtherUniv
                {
                    get { return _otherUniv; }
                    set { _otherUniv = value; }
                }
                public string OtherUnivName
                {
                    get { return _otherUnivName; }
                    set { _otherUnivName = value; }
                }
                public string NameofUniveristy
                {
                    get { return _nameofUniversity; }
                    set { _nameofUniversity = value; }
                }
                public string NameofLastDegree
                {
                    get { return _nameofLastDegree; }
                    set { _nameofLastDegree = value; }
                }
                public string YearLastDegree
                {
                    get { return _yearLastDegree; }
                    set { _yearLastDegree = value; }
                }
                #endregion
            }
        }
    }
}
