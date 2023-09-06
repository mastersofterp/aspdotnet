using System;
using System.Collections.Generic;
using System.Text;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class GEC_Student
    {
        //general info 
        #region Private Member
        private int _idNo = 0;
        private string _regNo = string.Empty;
        private string _rollNo = string.Empty;
        private int _sectionNo = 0;
        private string _studName = string.Empty;
        private string _studNameHindi = string.Empty;
        private string _fatherName = string.Empty;
        private string _motherName = string.Empty;
        private DateTime _dob = DateTime.Today;
        private char _sex = ' ';
        private int _religionNo = 0;
        private char _married = ' ';
        private int _nationalityNo = 0;
        private int _categoryNo = 0;
        // address info
        private string _pAddress = string.Empty;
        private string _pCity = string.Empty;


        private int _pState = 0;
        private string _pPinCode = string.Empty;
        private string _pMobile = string.Empty;
        // admission details
        private DateTime _admDate = DateTime.Today;
        private bool _hosteler = false;
        private int _degreeNo = 0;
        private int _batchNo = 0;
        private int _branchNo = 0;
        private int _classYear = 0;
        private int _semesterNo = 0;
        private int _pType = 0;
        private int _examPtype = 0;  //examPtype -> field use in GEC only 


        private int _stateNo = 0;
        private string _collegeCode = string.Empty;
        private string _birthPlace = string.Empty;
        private int _mToungeNo = 0;
        private string _otherLanguage = string.Empty;
        private decimal _height = 0.0m;
        private decimal _weight = 0;

        private string _identyMark = string.Empty;
        private int _bloodGroupNo = 0;
        private string _caste = string.Empty;
        private string _countryDomicile = string.Empty;
        private bool _irregular = false;
        private bool _urban = false;
        private int _year = 0;
        private bool _pro = false;
        private int _schemeNo = 0;
        private string _lastRollNo = string.Empty;
        private int _roll2 = 0;
        private string _accNo = string.Empty;
        private string _visano = string.Empty;
        private string _passportNo = string.Empty;
        private string _emailID = string.Empty;

        //private bool _idType = false;
        private int _idType = 0;
        private bool _admCancel = false;
        private int _admBatch = 0;
        private DateTime _leaveDate = DateTime.Today;
        private bool _can = false;
        private DateTime _canDate = DateTime.Today;
        private string _remark = string.Empty;
        private int _facAdvisor = 0;
        private string _prjname = string.Empty;
        private string _qualifyexamname = string.Empty;
        private decimal _percentage = 0.0m;
        private decimal _percentile = 0.0m;
        private string _yearOfExam = string.Empty;
        private string _qexmRollNo = string.Empty;
        private int uano = 0;
        GEC_QualifiedExam[] _lastQualExams = null;
        private string _studId = string.Empty;

        // 16 extra field requirement of GPM
        private int _scholarshipNo = 0;
        private int _generalMeritNo = 0;
        private int _categoryMeritNo = 0;
        private string _registeredNo = string.Empty;
        private int _admissionRoundNo = 0;
        private char _SSCTechnical = 'N';
        private char _defenceQuota = 'N';
        private char _phisycalHandicap = 'N';
        private string _typeOfPhisycalHandicap = string.Empty;
        private char _personWithDisability = 'N';
        private string _typeOfPhyDisability = string.Empty;
        private char _fatherMotherDomicileMaha = 'N';
        private char _fatherMotherCentralGovEmp = 'N';
        private char _HSSCMcvc = 'N';
        private decimal _annualIncome = 0.0m;
        private char _certAtachAnnualIncome = 'N';
        private char _IntermediateDrwingExm = 'N';
        private int _admQuotaNo = 0;
        private int _Others1 = 0;
        private int _Others2 = 0;
        private int _OthersMax1 = 0;
        private int _OthersMax2 = 0;
        private int _CompScienceObt = 0;
        private int _CompScienceMax = 0;
        private int _NATAScore = 0;
        private decimal _PercentileHsc = 0.0m;



        //parameters for GCET 
        private int _GCETRollNo = 0;
        private int _GCETRank = 0;
        private string _GCETRYearOfExam = string.Empty;
        private int _GCETCategory = 0;
        private int _GCETPhyObtMarks = 0;
        private int _GCETPhysMaxMarks = 0;
        private int _GCETChemObtMarks = 0;
        private int _GCETChemMaxMarks = 0;
        private int _GCETMathsObtMarks = 0;
        private int _GCETMathsMaxMarks = 0;
        private int _GCETBioObtMarks = 0;
        private int _GCETBioMaxMarks = 0;
        private int _GCETCompScObtMarks = 0;
        private int _GCETCompScMaxMarks = 0;
        private int _GCETNATAScore = 0;

        // dd info

        private string _ddNo = string.Empty;
        private DateTime _dddate = DateTime.MinValue;
        private string _ddAmount = string.Empty;
        private int _bankNo = 0;
        private int _cityNo = 0;

        #endregion

        #region Public Property Fields


        public int IdType
        {
            get { return _idType; }
            set { _idType = value; }
        }
        public string QexmRollNo
        {
            get { return _qexmRollNo; }
            set { _qexmRollNo = value; }
        }

        public string YearOfExam
        {
            get { return _yearOfExam; }
            set { _yearOfExam = value; }
        }
        public decimal Percentile
        {
            get { return _percentile; }
            set { _percentile = value; }
        }
        public decimal Percentage
        {
            get { return _percentage; }
            set { _percentage = value; }
        }
        public string Qualifyexamname
        {
            get { return _qualifyexamname; }
            set { _qualifyexamname = value; }
        }
        public string EmailID
        {
            get { return _emailID; }
            set { _emailID = value; }
        }
        public string RegNo
        {
            get { return _regNo; }
            set { _regNo = value; }
        }
        public string OtherLanguage
        {
            get { return _otherLanguage; }
            set { _otherLanguage = value; }
        }

        public int MToungeNo
        {
            get { return _mToungeNo; }
            set { _mToungeNo = value; }
        }

        public decimal Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }
        public string BirthPlace
        {
            get { return _birthPlace; }
            set { _birthPlace = value; }
        }
        public decimal Height
        {
            get { return _height; }
            set { _height = value; }
        }
        public int IdNo
        {
            get { return _idNo; }
            set { _idNo = value; }
        }
        public string PCity
        {
            get { return _pCity; }
            set { _pCity = value; }
        }

        public string RollNo
        {
            get { return _rollNo; }
            set { _rollNo = value; }
        }
        public int SectionNo
        {
            get { return _sectionNo; }
            set { _sectionNo = value; }
        }
        public string StudName
        {
            get { return _studName; }
            set { _studName = value; }
        }

        public string StudNameHindi
        {
            get { return _studNameHindi; }
            set { _studNameHindi = value; }
        }
        public string FatherName
        {
            get { return _fatherName; }
            set { _fatherName = value; }
        }
        public string MotherName
        {
            get { return _motherName; }
            set { _motherName = value; }
        }
        public DateTime Dob
        {
            get { return _dob; }
            set { _dob = value; }
        }

        public char Sex
        {
            get { return _sex; }
            set { _sex = value; }
        }
        public int ReligionNo
        {
            get { return _religionNo; }
            set { _religionNo = value; }
        }

        public char Married
        {
            get { return _married; }
            set { _married = value; }
        }
        public int NationalityNo
        {
            get { return _nationalityNo; }
            set { _nationalityNo = value; }
        }
        public int CategoryNo
        {
            get { return _categoryNo; }
            set { _categoryNo = value; }
        }
        // address info
        public string PAddress
        {
            get { return _pAddress; }
            set { _pAddress = value; }
        }

        public int PState
        {
            get { return _pState; }
            set { _pState = value; }
        }
        public string PPinCode
        {
            get { return _pPinCode; }
            set { _pPinCode = value; }
        }
        public string PMobile
        {
            get { return _pMobile; }
            set { _pMobile = value; }
        }
        // admission details
        public DateTime AdmDate
        {
            get { return _admDate; }
            set { _admDate = value; }
        }
        public bool Hosteler
        {
            get { return _hosteler; }
            set { _hosteler = value; }
        }
        public int DegreeNo
        {
            get { return _degreeNo; }
            set { _degreeNo = value; }
        }
        public int BatchNo
        {
            get { return _batchNo; }
            set { _batchNo = value; }
        }
        public int BranchNo
        {
            get { return _branchNo; }
            set { _branchNo = value; }
        }
        public int ClassYear
        {
            get { return _classYear; }
            set { _classYear = value; }
        }
        public int SemesterNo
        {
            get { return _semesterNo; }
            set { _semesterNo = value; }
        }
        public int PType
        {
            get { return _pType; }
            set { _pType = value; }
        }

        public int ExamPtype
        {
            get { return _examPtype; }
            set { _examPtype = value; }
        }


        public int StateNo
        {
            get { return _stateNo; }
            set { _stateNo = value; }
        }
        public string CollegeCode
        {
            get { return _collegeCode; }
            set { _collegeCode = value; }
        }
        public string IdentyMark
        {
            get { return _identyMark; }
            set { _identyMark = value; }
        }


        public int BloodGroupNo
        {
            get { return _bloodGroupNo; }
            set { _bloodGroupNo = value; }
        }
        public string Caste
        {
            get { return _caste; }
            set { _caste = value; }
        }
        public string CountryDomicile
        {
            get { return _countryDomicile; }
            set { _countryDomicile = value; }
        }
        public bool Irregular
        {
            get { return _irregular; }
            set { _irregular = value; }
        }
        public bool Urban
        {
            get { return _urban; }
            set { _urban = value; }
        }
        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }
        public bool Pro
        {
            get { return _pro; }
            set { _pro = value; }
        }
        public int SchemeNo
        {
            get { return _schemeNo; }
            set { _schemeNo = value; }
        }
        public string LastRollNo
        {
            get { return _lastRollNo; }
            set { _lastRollNo = value; }
        }
        public int Roll2
        {
            get { return _roll2; }
            set { _roll2 = value; }
        }
        public string AccNo
        {
            get { return _accNo; }
            set { _accNo = value; }
        }
        public string Visano
        {
            get { return _visano; }
            set { _visano = value; }
        }
        public string PassportNo
        {
            get { return _passportNo; }
            set { _passportNo = value; }
        }
        //public string EmailID
        //{
        //    get { return _emailID; }
        //    set { _emailID = value; }
        //}
        //public bool IdType
        //{
        //    get { return _idType; }
        //    set { _idType = value; }
        //}
        public bool AdmCancel
        {
            get { return _admCancel; }
            set { _admCancel = value; }
        }
        public int AdmBatch
        {
            get { return _admBatch; }
            set { _admBatch = value; }
        }
        public DateTime LeaveDate
        {
            get { return _leaveDate; }
            set { _leaveDate = value; }
        }
        public bool Can
        {
            get { return _can; }
            set { _can = value; }
        }
        public DateTime CanDate
        {
            get { return _canDate; }
            set { _canDate = value; }
        }
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        public int FacAdvisor
        {
            get { return _facAdvisor; }
            set { _facAdvisor = value; }
        }
        public string Prjname
        {
            get { return _prjname; }
            set { _prjname = value; }
        }
        public int Uano
        {
            get { return uano; }
            set { uano = value; }
        }
        public GEC_QualifiedExam[] LastQualifiedExams
        {
            get { return _lastQualExams; }
            set { _lastQualExams = value; }
        }
        public string StudId
        {
            get { return _studId; }
            set { _studId = value; }
        }
        public int ScholarshipNo
        {
            get { return _scholarshipNo; }
            set { _scholarshipNo = value; }
        }
        public int GeneralMeritNo
        {
            get { return _generalMeritNo; }
            set { _generalMeritNo = value; }
        }
        public int CategoryMeritNo
        {
            get { return _categoryMeritNo; }
            set { _categoryMeritNo = value; }
        }
        public string RegisteredNo
        {
            get { return _registeredNo; }
            set { _registeredNo = value; }
        }
        public int AdmissionRoundNo
        {
            get { return _admissionRoundNo; }
            set { _admissionRoundNo = value; }
        }
        public char SSCTechnical
        {
            get { return _SSCTechnical; }
            set { _SSCTechnical = value; }
        }
        public char DefenceQuota
        {
            get { return _defenceQuota; }
            set { _defenceQuota = value; }
        }
        public char PhisycalHandicap
        {
            get { return _phisycalHandicap; }
            set { _phisycalHandicap = value; }
        }
        public string TypeOfPhisycalHandicap
        {
            get { return _typeOfPhisycalHandicap; }
            set { _typeOfPhisycalHandicap = value; }
        }
        public char PersonWithDisability
        {
            get { return _personWithDisability; }
            set { _personWithDisability = value; }
        }
        public string TypeOfPhyDisability
        {
            get { return _typeOfPhyDisability; }
            set { _typeOfPhyDisability = value; }
        }
        public char FatherMotherDomicileMaha
        {
            get { return _fatherMotherDomicileMaha; }
            set { _fatherMotherDomicileMaha = value; }
        }
        public char FatherMotherCentralGovEmp
        {
            get { return _fatherMotherCentralGovEmp; }
            set { _fatherMotherCentralGovEmp = value; }
        }
        public char HSSCMcvc
        {
            get { return _HSSCMcvc; }
            set { _HSSCMcvc = value; }
        }
        public decimal AnnualIncome
        {
            get { return _annualIncome; }
            set { _annualIncome = value; }
        }
        public char CertAtachAnnualIncome
        {
            get { return _certAtachAnnualIncome; }
            set { _certAtachAnnualIncome = value; }
        }
        public char IntermediateDrwingExm
        {
            get { return _IntermediateDrwingExm; }
            set { _IntermediateDrwingExm = value; }
        }
        public int AdmQuotaNo
        {
            get { return _admQuotaNo; }
            set { _admQuotaNo = value; }
        }
        public int Others1
        {
            get { return _Others1; }
            set { _Others1 = value; }
        }
        public int Others2
        {
            get { return _Others2; }
            set { _Others2 = value; }
        }
        public int OthersMax1
        {
            get { return _OthersMax1; }
            set { _OthersMax1 = value; }
        }
        public int OthersMax2
        {
            get { return _OthersMax2; }
            set { _OthersMax2 = value; }
        }
        public int CompScienceObt
        {
            get { return _CompScienceObt; }
            set { _CompScienceObt = value; }
        }
        public int CompScienceMax
        {
            get { return _CompScienceMax; }
            set { _CompScienceMax = value; }
        }
        public int NATAScore
        {
            get { return _NATAScore; }
            set { _NATAScore = value; }
        }


        //properties for GCET 
        public int GECTROLLNO
        {
            get { return _GCETRollNo; }
            set { _GCETRollNo = value; }
        }
        public int GCETRANK
        {
            get { return _GCETRank; }
            set { _GCETRank = value; }
        }
        public string GCETYEAROFEXAM
        {
            get { return _GCETRYearOfExam; }
            set { _GCETRYearOfExam = value; }
        }
        public int GCETCATEGORY
        {
            get { return _GCETCategory; }
            set { _GCETCategory = value; }
        }
        public int GCETPhyObtMarks
        {
            get { return _GCETPhyObtMarks; }
            set { _GCETPhyObtMarks = value; }
        }
        public int GCETPhysMaxMarks
        {
            get { return _GCETPhysMaxMarks; }
            set { _GCETPhysMaxMarks = value; }
        }
        public int GCETChemObtMarks
        {
            get { return _GCETChemObtMarks; }
            set { _GCETChemObtMarks = value; }
        }
        public int GCETChemMaxMarks
        {
            get { return _GCETChemMaxMarks; }
            set { _GCETChemMaxMarks = value; }
        }
        public int GCETMathsObtMarks
        {
            get { return _GCETMathsObtMarks; }
            set { _GCETMathsObtMarks = value; }
        }
        public int GCETMathsMaxMarks
        {
            get { return _GCETMathsMaxMarks; }
            set { _GCETMathsMaxMarks = value; }
        }
        public int GCETBioObtMarks
        {
            get { return _GCETBioObtMarks; }
            set { _GCETBioObtMarks = value; }
        }
        public int GCETBioMaxMarks
        {
            get { return _GCETBioMaxMarks; }
            set { _GCETBioMaxMarks = value; }
        }
        public int GCETCompScObtMarks
        {
            get { return _GCETCompScObtMarks; }
            set { _GCETCompScObtMarks = value; }
        }
        public int GCETCompScMaxMarks
        {
            get { return _GCETCompScMaxMarks; }
            set { _GCETCompScMaxMarks = value; }
        }
        public int GCETNATAScore
        {
            get { return _GCETNATAScore; }
            set { _GCETNATAScore = value; }
        }
        public decimal PercentileHsc
        {
            get { return _PercentileHsc; }
            set { _PercentileHsc = value; }
        }

        // dd info

        public string DdNo
        {
            get { return _ddNo; }
            set { _ddNo = value; }
        }

        public DateTime Dddate
        {
            get { return _dddate; }
            set { _dddate = value; }
        }

        public string DdAmount
        {
            get { return _ddAmount; }
            set { _ddAmount = value; }
        }

        public int BankNo
        {
            get { return _bankNo; }
            set { _bankNo = value; }
        }


        public int cityNo
        {
            get { return _cityNo; }
            set { _cityNo = value; }
        }

        private int _receiptNo = 0;
        private string _receiptAmount = string.Empty;


        public int ReceiptNo
        {
            get { return _receiptNo; }
            set { _receiptNo = value; }
        }

        public string ReceiptAmount
        {
            get { return _receiptAmount; }
            set { _receiptAmount = value; }
        }

        public DateTime ReceiptDate
        {
            get { return _receiptDate; }
            set { _receiptDate = value; }
        }private DateTime _receiptDate = DateTime.MinValue;
        #endregion
    }
}