using System;
using System.Data;
using System.Web;



namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {

            public class ImportDataMaster
            {
                #region Private
                private string _no = string.Empty;
                private int _admbatch = 0;
                #region Other Entry
                private string _name = string.Empty;
                private string _prn = string.Empty;
                private string _gateYear = string.Empty;
                private string _gateReg = string.Empty;
                private string _gateScore = string.Empty;
                private string _gatePaper = string.Empty;
                private DateTime _dob = DateTime.MinValue;
                private string _gender = string.Empty;
                private string _mobile = string.Empty;               
                private string _applicantCategory = string.Empty;
                private string _programme = string.Empty;
                private string _institute = string.Empty;
                private string _allottedCategory = string.Empty;
                private string _group = string.Empty;

                #endregion

                #region BTech
                private string _RollNo = string.Empty;
                private string _AIROverall = string.Empty;
                private string _CandidateName  = string.Empty;
                private string _BranchName = string.Empty;
                private string _CandidateCategory = string.Empty;
                private string _ph = string.Empty;
                private string _HomeState  = string.Empty;
                private string _ReportingDate = string.Empty;
                private string _Quota = string.Empty;
                private string _RoundNo = string.Empty;
                private string _FatherName = string.Empty;
                private string _MotherName = string.Empty;

                #endregion

                private int _degreeno = 0;
                private int _branchno = 0;
                private int _semesterno = 0;
                private int _sessionno = 0;
                private int _collegeId = 0;
                private string _ipAddress = string.Empty;
                private string _Meritno = string.Empty;
                private string _ApplicationId = string.Empty;
                #endregion

                #region Public
                public string Meritno
                {
                    get { return _Meritno; }
                    set { _Meritno = value; }
                }
                public string ApplicationId
                {
                    get { return _ApplicationId; }
                    set { _ApplicationId = value; }
                }
                
                public string NO
                {
                    get { return _no; }
                    set { _no = value; }
                }

                public string NAME
                {
                    get { return _name; }
                    set { _name = value; }
                }
                public string PRN
                {
                    get { return _prn; }
                    set { _prn = value; }
                }
                public string GATEYEAR
                {
                    get { return _gateYear; }
                    set { _gateYear = value; }
                }
                public string GATEREG
                {
                    get { return _gateReg; }
                    set { _gateReg = value; }
                }
                public string GATESCORE
                {
                    get { return _gateScore; }
                    set { _gateScore = value; }
                }
                public string GATEPAPER
                {
                    get { return _gatePaper; }
                    set { _gatePaper = value; }
                }

                public DateTime DOB
                {
                    get { return _dob; }
                    set { _dob = value; }
                }

                public string GENDER
                {
                    get { return _gender; }
                    set { _gender = value; }
                }
                public string MOBILE
                {
                    get { return _mobile; }
                    set { _mobile = value; }
                }
                public string APPLICANTCATEGORY
                {
                    get { return _applicantCategory; }
                    set { _applicantCategory = value; }
                }
                public string PROGRAMME
                {
                    get { return _programme; }
                    set { _programme = value; }
                }
                public string INSTITUTE
                {
                    get { return _institute; }
                    set { _institute = value; }
                }
                public string ALLOTTEDCATEGORY 
                {
                    get { return _allottedCategory; }
                    set { _allottedCategory = value; }
                }
                public string GROUP 
                {
                    get { return _group; }
                    set { _group = value; }
                }
                public string ROLLNO
                {
                    get { return _RollNo ; }
                    set { _RollNo  = value; }
                }
                public string AIROVERALL
                {
                    get { return _AIROverall ; }
                    set { _AIROverall  = value; }
                }
                public string CANDIDATENAME
                {
                    get { return _CandidateName  ; }
                    set { _CandidateName   = value; }
                }
                public string BRANCHNAME
                {
                    get { return _BranchName ; }
                    set { _BranchName  = value; }
                }
                public string PH
                {
                    get { return _ph ; }
                    set { _ph  = value; }
                }
                public string HOMESTATE 
                {
                    get { return _HomeState  ; }
                    set { _HomeState   = value; }
                }
                public string REPORTINGDATE
                {
                    get { return _ReportingDate ; }
                    set { _ReportingDate  = value; }
                }
                public string QUOTA
                {
                    get { return _Quota ; }
                    set { _Quota  = value; }
                }
                public string ROUNDNO
                {
                    get { return _RoundNo ; }
                    set { _RoundNo  = value; }
                }
                public int BATCHNO
                {
                    get { return _admbatch; }
                    set { _admbatch = value; }
                }

                public string FATHERNAME
                {
                    get { return _FatherName; }
                    set { _FatherName = value; }
                }

                public string MOTHERNAME
                {
                    get { return _MotherName; }
                    set { _MotherName = value; }
                }


                public int DEGREENO
                {
                    get { return _degreeno; }
                    set { _degreeno = value; }
                }
                public int BRANCHNO
                {
                    get { return _branchno; }
                    set { _branchno = value; }
                }
                public int COLLEGEID
                {
                    get { return _collegeId; }
                    set { _collegeId = value; }
                }
                public int SESSIONNO
                {
                    get { return _sessionno; }
                    set { _sessionno = value; }
                }
                public int SEMESTERNO
                {
                    get { return _semesterno; }
                    set { _semesterno = value; }
                }
                public string IPADDRESS
                {
                    get { return _ipAddress; }
                    set { _ipAddress = value; }
                }

                #endregion
            }
        }
    }
}

