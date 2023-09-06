using System;
using System.Collections.Generic;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class Phd
    {
        //general info 
        #region Private Member
        private int _idNo = 0;
        private string _regNo = string.Empty;
        private string _rollNo = string.Empty;
        private string _studName = string.Empty;
       
        private DateTime _dob = DateTime.Today;
        private char _sex = ' ';
     
        private string _pAddress = string.Empty;
        private string _pCity = string.Empty;
        private int _pState = 0;
        private string _pPinCode = string.Empty;
        private string _pMobile = string.Empty;
        // admission details
        private DateTime _admDate = DateTime.Today;
        private string _yearOfExam = string.Empty;
        private int _degreeNo = 0;
        private int _batchNo = 0;
        private int _branchNo = 0;
        private int _classYear = 0;
        private int _semesterNo = 0;
        private int _pType = 0;
        private int _examPtype = 0;  //examPtype -> field use in GEC only 
        private string _emailID = string.Empty;

        //private bool _idType = false;
        private bool _admCancel = false;
        private bool _can = false;
        private DateTime _canDate = DateTime.Today;
        private string _remark = string.Empty;
        private int uano = 0;
        private string _studId = string.Empty;
        private string _studentMobile = string.Empty;
             //prospectus details
              
        private string _IPADDRESS;
        private DateTime _printDate = DateTime.Today;
                    
        string _enrollNo = string.Empty;

        private int _phdstatus = 0;
        private int _phdsupervisorno = 0;

        private int _phdcosupervisorno1 = 0;
        private int _phdcosupervisorno2 = 0;

        private int _typesupervisorno = 0;
        private int _typecosupervisorno1 = 0;
        private int _typecosupervisorno2 = 0;
        
        private int _supervisorno = 0;
        private int _supervisormemberno = 0;
        private int _supervisorstatus = 0;

        private int _joinsupervisorno = 0;
        private int _joinsupervisormemberno = 0;
        private int _joinsupervisorstatus = 0;

        private int _institutefacultyno = 0;
        private int _institutefacmemberno = 0;
        private int _institutefacultystatus = 0;

        private int _drcno = 0;
        private int _drcmemberno = 0;
        private int _drcstatus = 0;

        private int _drcchairno = 0;
        private int _drcchairmemberno = 0;
        private int _drcchairstatus = 0;

        private int _phdstatuscat = 0;

        private decimal _credits = 0;
        private string _topics = string.Empty;
        private string _workdone = string.Empty;
        private string _phdremark = string.Empty;
           
        private DateTime? _attempt1datewritten;//********Added on 04/12/2015
        private DateTime? _attempt2datewritten;//********Added on 04/12/2015
        private DateTime _attempt1dateoral = DateTime.Today;
        private DateTime? _attempt2dateoral = DateTime.Today;
        private DateTime _approveddate = DateTime.Today;         

        private string _superrole = string.Empty;
        private string _research = string.Empty;
        private string _completecourse = string.Empty;

        private int _special = 0;

        // ADDED FOR EXTRA SUPERVISOR
        private int _secondjoinsupervisorno = 0;
        private int _secondjoinsupervisormemberno = 0;
        private int _secondjoinsupervisorstatus = 0;
       

        // add thesis title --- 26062018 -- 
        private string _ThesisTitle = string.Empty;
        private string _Descipline = string.Empty;
       

        //--- Phd degree completed 

        private string _ThesisTitleHindi = string.Empty;
        private int _PhdPassoutyear = 0;
        private int _PhdConvocationyear = 0;
        private decimal _PhddegreeTotalAmount = 0;
        private string _PhdFeesRef = string.Empty;
        private string _PhdDegreeRemark = string.Empty;
        private DateTime _PhdConvocationDate = DateTime.Today;


        //-----Phd module Synopsis
        private int _PhdExaminer1 = 0;
        private int _PhdExaminer1Status = 0;
        private int _PhdExaminer2 = 0;
        private int _PhdExaminer2Status = 0;
        private int _PhdExaminer3 = 0;
        private int _PhdExaminer3Status = 0;
        private int _PhdExaminer4 = 0;
        private int _PhdExaminer4Status = 0;
        private int _PhdExaminer5 = 0;
        private int _PhdExaminer5Status = 0;
        private int _PhdExaminer6 = 0;
        private int _PhdExaminer6Status = 0;
        private int _PhdExaminer7 = 0;
        private int _PhdExaminer7Status = 0;
        private int _PhdExaminer8 = 0;
        private int _PhdExaminer8Status = 0;
        private int _PhdExaminer9 = 0;
        private int _PhdExaminer9Status = 0;
        private int _PhdExaminer10 = 0;
        private int _PhdExaminer10Status = 0;
        private string _PhdStatusValue = string.Empty;


        #endregion

        #region Public Property Fields
        public string YearOfExam
        {
            get { return _yearOfExam; }
            set { _yearOfExam = value; }
        }
        public string PhdStatusValue
        {
            get { return _PhdStatusValue; }
            set { _PhdStatusValue = value; }
        }

        public int PhdExaminer1
        {
            get { return _PhdExaminer1; }
            set { _PhdExaminer1 = value; }
        }


        public int PhdExaminer2
        {
            get { return _PhdExaminer2; }
            set { _PhdExaminer2 = value; }
        }
        public int PhdExaminer3
        {
            get { return _PhdExaminer3; }
            set { _PhdExaminer3 = value; }
        }
        public int PhdExaminer4
        {
            get { return _PhdExaminer4; }
            set { _PhdExaminer4 = value; }
        }
        public int PhdExaminer5
        {
            get { return _PhdExaminer5; }
            set { _PhdExaminer5 = value; }
        }
        public int PhdExaminer6
        {
            get { return _PhdExaminer6; }
            set { _PhdExaminer6 = value; }
        }
        public int PhdExaminer7
        {
            get { return _PhdExaminer7; }
            set { _PhdExaminer7 = value; }
        }
        public int PhdExaminer8
        {
            get { return _PhdExaminer8; }
            set { _PhdExaminer8 = value; }
        }
        public int PhdExaminer9
        {
            get { return _PhdExaminer9; }
            set { _PhdExaminer9 = value; }
        }
        public int PhdExaminer10
        {
            get { return _PhdExaminer10; }
            set { _PhdExaminer10 = value; }
        }
        //---------------------------------------
        public int PhdExaminer1Status
        {
            get { return _PhdExaminer1Status; }
            set { _PhdExaminer1Status = value; }
        }

        public int PhdExaminer2Status
        {
            get { return _PhdExaminer2Status; }
            set { _PhdExaminer2Status = value; }
        }
        public int PhdExaminer3Status
        {
            get { return _PhdExaminer3Status; }
            set { _PhdExaminer3Status = value; }
        }
        public int PhdExaminer4Status
        {
            get { return _PhdExaminer4Status; }
            set { _PhdExaminer4Status = value; }
        }
        public int PhdExaminer5Status
        {
            get { return _PhdExaminer5Status; }
            set { _PhdExaminer5Status = value; }
        }
        public int PhdExaminer6Status
        {
            get { return _PhdExaminer6Status; }
            set { _PhdExaminer6Status = value; }
        }
        public int PhdExaminer7Status
        {
            get { return _PhdExaminer7Status; }
            set { _PhdExaminer7Status = value; }
        }
        public int PhdExaminer8Status
        {
            get { return _PhdExaminer8Status; }
            set { _PhdExaminer8Status = value; }
        }
        public int PhdExaminer9Status
        {
            get { return _PhdExaminer9Status; }
            set { _PhdExaminer9Status = value; }
        }
        public int PhdExaminer10Status
        {
            get { return _PhdExaminer10Status; }
            set { _PhdExaminer10Status = value; }
        }
        //------------------------------
                
        //-------phd degree completed -- 02082018
        public int PhdPassoutyear
        {
            get { return _PhdPassoutyear; }
            set { _PhdPassoutyear = value; }
        }

        public int PhdConvocationyear
        {
            get { return _PhdConvocationyear; }
            set { _PhdConvocationyear = value; }
        }

        public string ThesisTitleHindi
        {
            get { return _ThesisTitleHindi; }
            set { _ThesisTitleHindi = value; }
        }

        public Decimal PhddegreeTotalAmount
        {
            get { return _PhddegreeTotalAmount; }
            set { _PhddegreeTotalAmount = value; }
        }

        public string PhdFeesRef
        {
            get { return _PhdFeesRef; }
            set { _PhdFeesRef = value; }
        }

        public string PhdDegreeRemark
        {
            get { return _PhdDegreeRemark; }
            set { _PhdDegreeRemark = value; }
        }

        public DateTime PhdConvocationDate
        {
            get { return _PhdConvocationDate; }
            set { _PhdConvocationDate = value; }
        }
        //------------------
               
        // -- for annexure f added by dipali on 26062018
        public string ThesisTitle
        {
            get { return _ThesisTitle; }
            set { _ThesisTitle = value; }
        }

        public string Descipline
        {
            get { return _Descipline; }
            set { _Descipline = value; }
        }

        //==== end  == //
               
        //ADDED BY VAIBHAV M ON DATE 09012017
       public int Secondjoinsupervisorno
        {
            get { return _secondjoinsupervisorno; }
            set { _secondjoinsupervisorno = value; }
        }
        public int Secondjoinsupervisormemberno
        {
            get { return _secondjoinsupervisormemberno; }
            set { _secondjoinsupervisormemberno = value; }
        }
        public int Secondjoinsupervisorstatus
        {
            get { return _secondjoinsupervisorstatus; }
            set { _secondjoinsupervisorstatus = value; }
        }
        public int Special
        {
            get { return _special; }
            set { _special = value; }
        }
        public string Completecourse
        {
            get { return _completecourse; }
            set { _completecourse = value; }
        }
        public string Research
        {
            get { return _research; }
            set { _research = value; }
        }
        public string SuperRole
        {
            get { return _superrole; }
            set { _superrole = value; }
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
        public int DrcChairNo
        {
            get { return _drcchairno; }
            set { _drcchairno = value; }
        }
        public int DrcChairmemberNo
        {
            get { return _drcchairmemberno; }
            set { _drcchairmemberno = value; }
        }
        public int DrcChairstatus
        {
            get { return _drcchairstatus; }
            set { _drcchairstatus = value; }
        }
        public string RollNo
        {
            get { return _rollNo; }
            set { _rollNo = value; }
        }
      
        public string StudName
        {
            get { return _studName; }
            set { _studName = value; }
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

        public int ExamPtype  // only for gec project
        {
            get { return _examPtype; }
            set { _examPtype = value; }
        }

        public bool AdmCancel
        {
            get { return _admCancel; }
            set { _admCancel = value; }
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
        
        public int Uano
        {
            get { return uano; }
            set { uano = value; }
        }
       
        public string StudId
        {
            get { return _studId; }
            set { _studId = value; }
        }


       
        public string StudentMobile
        {
            get { return _studentMobile; }
            set { _studentMobile = value; }
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

        public DateTime PrintDate
        {
            get { return _printDate; }
            set { _printDate = value; }
        }
                
        public string EnrollNo
        {
            get { return _enrollNo; }
            set { _enrollNo = value; }
        }
        public int PhdStatus
        {
            get { return _phdstatus; }
            set { _phdstatus = value; }
        }
        public int PhdSupervisorNo
        {
            get { return _phdsupervisorno; }
            set { _phdsupervisorno = value; }
        }

        public int PhdCoSupervisorNo1
        {
            get { return _phdcosupervisorno1; }
            set { _phdcosupervisorno1 = value; }
        }

        public int PhdCoSupervisorNo2
        {
            get { return _phdcosupervisorno2; }
            set { _phdcosupervisorno2 = value; }
        }

        public int TypeSupervisorNo
        {
            get { return _typesupervisorno; }
            set { _typesupervisorno = value; }
        }

        public int TypeCoSupervisorNo1
        {
            get { return _typecosupervisorno1; }
            set { _typecosupervisorno1 = value; }
        }

        public int TypeCoSupervisorNo2
        {
            get { return _typecosupervisorno2; }
            set { _typecosupervisorno2 = value; }
        }
              
        public int SupervisorNo
        {
            get { return _supervisorno; }
            set { _supervisorno = value; }
        }
        public int SupervisormemberNo
        {
            get { return _supervisormemberno; }
            set { _supervisormemberno = value; }
        }
        public int SupervisorStatus
        {
            get { return _supervisorstatus; }
            set { _supervisorstatus = value; }
        }

        public int JoinsupervisorNo
        {
            get { return _joinsupervisorno; }
            set { _joinsupervisorno = value; }
        }
        public int JoinsupervisormemberNo
        {
            get { return _joinsupervisormemberno; }
            set { _joinsupervisormemberno = value; }
        }
        public int JoinsupervisorStatus
        {
            get { return _joinsupervisorstatus; }
            set { _joinsupervisorstatus = value; }
        }


        public int InstitutefacultyNo
        {
            get { return _institutefacultyno; }
            set { _institutefacultyno = value; }
        }
        public int InstitutefacmemberNo
        {
            get { return _institutefacmemberno; }
            set { _institutefacmemberno = value; }
        }
        public int InstitutefacultyStatus
        {
            get { return _institutefacultystatus; }
            set { _institutefacultystatus = value; }
        }

        public int DrcNo
        {
            get { return _drcno; }
            set { _drcno = value; }
        }
        public int DrcmemberNo
        {
            get { return _drcmemberno; }
            set { _drcmemberno = value; }
        }
        public int Drcstatus
        {
            get { return _drcstatus; }
            set { _drcstatus = value; }
        }
        public int Phdstatuscat
        {
            get { return _phdstatuscat; }
            set { _phdstatuscat = value; }
        }
        public decimal Credits
        {
            get { return _credits; }
            set { _credits = value; }
        }
        public string Topics
        {
            get { return _topics; }
            set { _topics = value; }
        }
        public string Workdone
        {
            get { return _workdone; }
            set { _workdone = value; }
        }
        public string Phdremark
        {
            get { return _phdremark; }
            set { _phdremark = value; }
        }
       
        public DateTime? Attempt1DateWritten
        {
            get { return _attempt1datewritten; }
            set { _attempt1datewritten = value; }
        }

        public DateTime? Attempt2DateWritten
        {
            get { return _attempt2datewritten; }
            set { _attempt2datewritten = value; }
        }

        public DateTime Attempt1DateOral
        {
            get { return _attempt1dateoral; }
            set { _attempt1dateoral = value; }
        }

        public DateTime? Attempt2DateOral
        {
            get { return _attempt2dateoral; }
            set { _attempt2dateoral = value; }
        }

        public DateTime ApprovedDate
        {
            get { return _approveddate; }
            set { _approveddate = value; }
        }
         #endregion


        #region Added By Sneha for MTech

        private int _Sessionno = 0;
        private int _collegecode = 0;
        private string _IDNOS = string.Empty;
        private string _REGNOS = string.Empty;

        private string _Guide1 = string.Empty;
        private string _Guide2 = string.Empty;
        private string _Guide3 = string.Empty;

        private string _Examiner1 = string.Empty;
        private string _Examiner2 = string.Empty;
        private string _Examiner3 = string.Empty;

        private string _Dissertation = string.Empty;


        public int Sessionno
        {
            get { return _Sessionno; }
            set { _Sessionno = value; }
        }
        public int College_Code
        {
            get { return _collegecode; }
            set { _collegecode = value; }
        }
        public string IDNOS
        {
            get { return _IDNOS; }
            set { _IDNOS = value; }
        }
        public string Regnos
        {
            get { return _REGNOS; }
            set { _REGNOS = value; }
        }

        public string Guide1
        {
            get { return _Guide1; }
            set { _Guide1 = value; }
        }
        public string Guide2
        {
            get { return _Guide2; }
            set { _Guide2 = value; }
        }
        public string Guide3
        {
            get { return _Guide3; }
            set { _Guide3 = value; }
        }
        public string Examiner1
        {
            get { return _Examiner1; }
            set { _Examiner1 = value; }
        }
        public string Examiner2
        {
            get { return _Examiner2; }
            set { _Examiner2 = value; }
        }
        public string Examiner3
        {
            get { return _Examiner3; }
            set { _Examiner3 = value; }
        }
        public string Dissertation
        {
            get { return _Dissertation; }
            set { _Dissertation = value; }
        }



        #endregion


        #region Added Phd_Thesis_Approval_Entry (27/02/2023)

        private int _submission_status_no = 0;
        private int _dispatchedtoreviewer_status_no = 0;
        private int _reviewerreportreceived_status_no = 0;
        private int _opendefencevivaschedule_status_no = 0;
        private int _awarded_status_no = 0;
        private int _submission_uano = 0;
        private int _dispatchedtoreviewer_uano = 0;
        private int _reviewerreportreceived_uano = 0;
        private int _opendefencevivaschedule_uano = 0;
        private int _awarded_uano = 0;

        private string _submission_status = string.Empty;
        private string _submission_ip = string.Empty;
        private string _submission_remark = string.Empty;
        private string _dispatchedtoreviewer_status = string.Empty;
        private string _dispatchedtoreviewer_ip = string.Empty;
        private string _dispatchedtoreviewer_remark = string.Empty;
        private string _reviewerreportreceived_status = string.Empty;
        private string _reviewerreportreceived_ip = string.Empty;
        private string _reviewerreportreceived_remark = string.Empty;
        private string _opendefencevivaschedule_status = string.Empty;
        private string _opendefencevivaschedule_ip = string.Empty;
        private string _opendefencevivaschedule_remark = string.Empty;
        private string _awarded_status = string.Empty;
        private string _awarded_ip = string.Empty;
        private string _awarded_remark = string.Empty;

        private int _examiner_type_no = 0;
        private int _flag = 0;

        private string _examiner_type = string.Empty;
        private string _flag_name = string.Empty;
        private string _flag_remark = string.Empty;

        private DateTime? _submission_create_date = null;
        private DateTime? _dispatchedtoreviewer_create_date = null;
        private DateTime? _reviewerreportreceived_create_date = null;
        private DateTime? _opendefencevivaschedule_create_date = null;
        private DateTime? _awarded_create_date = null;

        public int SUBMISSION_STATUS_NO
        {
            get { return _submission_status_no; }
            set { _submission_status_no = value; }
        }
        public int DISPATCHEDTOREVIEWER_STATUS_NO
        {
            get { return _dispatchedtoreviewer_status_no; }
            set { _dispatchedtoreviewer_status_no = value; }
        }
        public int REVIEWERREPORTRECEIVED_STATUS_NO
        {
            get { return _reviewerreportreceived_status_no; }
            set { _reviewerreportreceived_status_no = value; }
        }
        public int OPENDEFENCEVIVASCHEDULE_STATUS_NO
        {
            get { return _opendefencevivaschedule_status_no; }
            set { _opendefencevivaschedule_status_no = value; }
        }
        public int AWARDED_STATUS_NO
        {
            get { return _awarded_status_no; }
            set { _awarded_status_no = value; }
        }
        public int SUBMISSION_UANO
        {
            get { return _submission_uano; }
            set { _submission_uano = value; }
        }
        public int DISPATCHEDTOREVIEWER_UANO
        {
            get { return _dispatchedtoreviewer_uano; }
            set { _dispatchedtoreviewer_uano = value; }
        }
        public int REVIEWERREPORTRECEIVED_UANO
        {
            get { return _reviewerreportreceived_uano; }
            set { _reviewerreportreceived_uano = value; }
        }
        public int OPENDEFENCEVIVASCHEDULE_UANO
        {
            get { return _opendefencevivaschedule_uano; }
            set { _opendefencevivaschedule_uano = value; }
        }
        public int AWARDED_UANO
        {
            get { return _awarded_uano; }
            set { _awarded_uano = value; }
        }
        public int EXAMINER_TYPE_NO
        {
            get { return _examiner_type_no; }
            set { _examiner_type_no = value; }
        }
        public int FLAG
        {
            get { return _flag; }
            set { _flag = value; }
        }

        public string SUBMISSION_STATUS
        {
            get { return _submission_status; }
            set { _submission_status = value; }
        }
        public string SUBMISSION_IP
        {
            get { return _submission_ip; }
            set { _submission_ip = value; }
        }
        public string SUBMISSION_REMARK
        {
            get { return _submission_remark; }
            set { _submission_remark = value; }
        }
        public string DISPATCHEDTOREVIEWER_STATUS
        {
            get { return _dispatchedtoreviewer_status; }
            set { _dispatchedtoreviewer_status = value; }
        }
        public string DISPATCHEDTOREVIEWER_IP
        {
            get { return _dispatchedtoreviewer_ip; }
            set { _dispatchedtoreviewer_ip = value; }
        }
        public string DISPATCHEDTOREVIEWER_REMARK
        {
            get { return _dispatchedtoreviewer_remark; }
            set { _dispatchedtoreviewer_remark = value; }
        }
        public string REVIEWERREPORTRECEIVED_STATUS
        {
            get { return _reviewerreportreceived_status; }
            set { _reviewerreportreceived_status = value; }
        }
        public string REVIEWERREPORTRECEIVED_IP
        {
            get { return _reviewerreportreceived_ip; }
            set { _reviewerreportreceived_ip = value; }
        }
        public string REVIEWERREPORTRECEIVED_REMARK
        {
            get { return _reviewerreportreceived_remark; }
            set { _reviewerreportreceived_remark = value; }
        }
        public string OPENDEFENCEVIVASCHEDULE_STATUS
        {
            get { return _opendefencevivaschedule_status; }
            set { _opendefencevivaschedule_status = value; }
        }
        public string OPENDEFENCEVIVASCHEDULE_IP
        {
            get { return _opendefencevivaschedule_ip; }
            set { _opendefencevivaschedule_ip = value; }
        }
        public string OPENDEFENCEVIVASCHEDULE_REMARK
        {
            get { return _opendefencevivaschedule_remark; }
            set { _opendefencevivaschedule_remark = value; }
        }
        public string AWARDED_STATUS
        {
            get { return _awarded_status; }
            set { _awarded_status = value; }
        }
        public string AWARDED_IP
        {
            get { return _awarded_ip; }
            set { _awarded_ip = value; }
        }
        public string AWARDED_REMARK
        {
            get { return _awarded_remark; }
            set { _awarded_remark = value; }
        }

        public string EXAMINER_TYPE
        {
            get { return _examiner_type; }
            set { _examiner_type = value; }
        }
        public string FLAG_NAME
        {
            get { return _flag_name; }
            set { _flag_name = value; }
        }
        public string FLAG_REMARK
        {
            get { return _flag_remark; }
            set { _flag_remark = value; }
        }

        public DateTime? SUBMISSION_CREATE_DATE
        {
            get { return _submission_create_date; }
            set { _submission_create_date = value; }
        }
        public DateTime? DISPATCHEDTOREVIEWER_CREATE_DATE
        {
            get { return _dispatchedtoreviewer_create_date; }
            set { _dispatchedtoreviewer_create_date = value; }
        }
        public DateTime? REVIEWERREPORTRECEIVED_CREATE_DATE
        {
            get { return _reviewerreportreceived_create_date; }
            set { _reviewerreportreceived_create_date = value; }
        }
        public DateTime? OPENDEFENCEVIVASCHEDULE_CREATE_DATE
        {
            get { return _opendefencevivaschedule_create_date; }
            set { _opendefencevivaschedule_create_date = value; }
        }
        public DateTime? AWARDED_CREATE_DATE
        {
            get { return _awarded_create_date; }
            set { _awarded_create_date = value; }
        }

        #endregion


        #region Added Phd_Pblications_Deatils (17/04/2023)

        private int _AdmBatch = 0;
        private string _PUBLICATION_TYPE;
        private string _TITLE;
        private string _SUBJECT;
        private System.Nullable<System.DateTime> _PUBLICATIONDATE;
        private string _DETAILS;
        private System.Nullable<decimal> _SPONSORED_AMOUNT;
        private string _SPONSOREDBY;
        private string _EISSN;
        private string _PUB_ADD;
        private string _VOLUME_NO;
        private string _ISSUE_NO;
        private string _PUB_STATUS;
        private string _PUBLISHER;
        private string _MONTH;
        private int _IsJournalScopus;
        private int _IS_CONFERENCE;
        private string _IMPACTFACTORS;
        private string _CITATIONINDEX = string.Empty;
        private string _DOIN;
        private int _IndexingFactors;
        private string _IndexingFactorValue;
        private System.Nullable<System.DateTime> _IndexingDATE;
        private string _INDEXING_TYPE = string.Empty;
        private string _CONNAME;
        private string _ORGANISOR;
        private string _PUBLICATION;
        private string _PAGENO;
        private string _ATTACHMENTS;
        private System.Nullable<int> _YEAR;
        private string _ISBN;
        private string _LOCATION;
        private System.Nullable<int> _PUBTRXNO;
        private string _WEBLINK;
        private string _COLLEGE_CODE;

        public int AdmBatch
        {
            get { return _AdmBatch; }
            set { _AdmBatch = value; }
        }
        public string PUBLICATION_TYPE
        {
            get
            {
                return this._PUBLICATION_TYPE;
            }
            set
            {
                if ((this._PUBLICATION_TYPE != value))
                {
                    this._PUBLICATION_TYPE = value;
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
        public System.Nullable<System.DateTime> PUBLICATIONDATE
        {
            get
            {
                return this._PUBLICATIONDATE;
            }
            set
            {
                if ((this._PUBLICATIONDATE != value))
                {
                    this._PUBLICATIONDATE = value;
                }
            }
        }
        public string DETAILS
        {
            get
            {
                return this._DETAILS;
            }
            set
            {
                if ((this._DETAILS != value))
                {
                    this._DETAILS = value;
                }
            }
        }
        public System.Nullable<decimal> SPONSORED_AMOUNT
        {
            get
            {
                return this._SPONSORED_AMOUNT;
            }
            set
            {
                if ((this._SPONSORED_AMOUNT != value))
                {
                    this._SPONSORED_AMOUNT = value;
                }
            }
        }

        public string EISSN
        {
            get
            {
                return this._EISSN;
            }
            set
            {
                if ((this._EISSN != value))
                {
                    this._EISSN = value;
                }
            }
        }

        public string PUB_ADD
        {
            get
            {
                return this._PUB_ADD;
            }
            set
            {
                if ((this._PUB_ADD != value))
                {
                    this._PUB_ADD = value;
                }
            }
        }

        public string VOLUME_NO
        {
            get
            {
                return this._VOLUME_NO;
            }
            set
            {
                if ((this._VOLUME_NO != value))
                {
                    this._VOLUME_NO = value;
                }
            }
        }

        public string ISSUE_NO
        {
            get
            {
                return this._ISSUE_NO;
            }
            set
            {
                if ((this._ISSUE_NO != value))
                {
                    this._ISSUE_NO = value;
                }
            }
        }

        public string PUB_STATUS
        {
            get
            {
                return this._PUB_STATUS;
            }
            set
            {
                if ((this._PUB_STATUS != value))
                {
                    this._PUB_STATUS = value;
                }
            }
        }
        public string PUBLISHER
        {
            get
            {
                return this._PUBLISHER;
            }
            set
            {
                if ((this._PUBLISHER != value))
                {
                    this._PUBLISHER = value;
                }
            }
        }

        public string MONTH
        {
            get
            {
                return this._MONTH;
            }
            set
            {
                if ((this._MONTH != value))
                {
                    this._MONTH = value;
                }
            }
        }

        public int IsJournalScopus
        {
            get
            {
                return this._IsJournalScopus;
            }
            set
            {
                if ((this._IsJournalScopus != value))
                {
                    this._IsJournalScopus = value;
                }
            }
        }

        public int IS_CONFERENCE
        {
            get
            {
                return this._IS_CONFERENCE;
            }
            set
            {
                if ((this._IS_CONFERENCE != value))
                {
                    this._IS_CONFERENCE = value;
                }
            }
        }
        public string IMPACTFACTORS
        {
            get
            {
                return this._IMPACTFACTORS;
            }
            set
            {
                if ((this._IMPACTFACTORS != value))
                {
                    this._IMPACTFACTORS = value;
                }
            }
        }
        public string CITATIONINDEX
        {
            get
            {
                return this._CITATIONINDEX;
            }
            set
            {
                if ((this._CITATIONINDEX != value))
                {
                    this._CITATIONINDEX = value;
                }
            }
        }
        public string DOIN
        {
            get
            {
                return this._DOIN;
            }
            set
            {
                if ((this._DOIN != value))
                {
                    this._DOIN = value;
                }
            }
        }

        public int IndexingFactors
        {
            get
            {
                return this._IndexingFactors;
            }
            set
            {
                if ((this._IndexingFactors != value))
                {
                    this._IndexingFactors = value;
                }
            }
        }
        public string IndexingFactorValue
        {
            get
            {
                return this._IndexingFactorValue;
            }
            set
            {
                if ((this._IndexingFactorValue != value))
                {
                    this._IndexingFactorValue = value;
                }
            }
        }

        public System.Nullable<System.DateTime> IndexingDATE
        {
            get
            {
                return this._IndexingDATE;
            }
            set
            {
                if ((this._IndexingDATE != value))
                {
                    this._IndexingDATE = value;
                }
            }
        }
        public string INDEXING_TYPE
        {
            get { return this._INDEXING_TYPE; }
            set
            {
                if (this._INDEXING_TYPE != value)
                {
                    this._INDEXING_TYPE = value;
                }
            }
        }
        public string CONFERENCE_NAME
        {
            get
            {
                return this._CONNAME;
            }
            set
            {
                if ((this._CONNAME != value))
                {
                    this._CONNAME = value;
                }
            }
        }
        public string ORGANISOR
        {
            get
            {
                return this._ORGANISOR;
            }
            set
            {
                if ((this._ORGANISOR != value))
                {
                    this._ORGANISOR = value;
                }
            }
        }
        public string PUBLICATION
        {
            get
            {
                return this._PUBLICATION;
            }
            set
            {
                if ((this._PUBLICATION != value))
                {
                    this._PUBLICATION = value;
                }
            }
        }
        public string PAGENO
        {
            get
            {
                return this._PAGENO;
            }
            set
            {
                if ((this._PAGENO != value))
                {
                    this._PAGENO = value;
                }
            }
        }
        public string ATTACHMENTS
        {
            get
            {
                return this._ATTACHMENTS;
            }
            set
            {
                if ((this._ATTACHMENTS != value))
                {
                    this._ATTACHMENTS = value;
                }
            }
        }
        public string ISBN
        {
            get
            {
                return this._ISBN;
            }
            set
            {
                if ((this._ISBN != value))
                {
                    this._ISBN = value;
                }
            }
        }
        public System.Nullable<int> YEAR
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
        public System.Nullable<int> PUBTRXNO
        {
            get
            {
                return this._PUBTRXNO;
            }
            set
            {
                if ((this._PUBTRXNO != value))
                {
                    this._PUBTRXNO = value;
                }
            }
        }
        public string WEBLINK
        {
            get
            {
                return this._WEBLINK;
            }
            set
            {
                if ((this._WEBLINK != value))
                {
                    this._WEBLINK = value;
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

        #endregion

    }
}