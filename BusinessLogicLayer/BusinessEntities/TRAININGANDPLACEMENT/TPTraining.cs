using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class TPTraining
            {
                #region Private Members
                    string _seqnos = string.Empty;
                    string _orgs = string.Empty;                
                    string _periods = string.Empty;
                    string _subjects = string.Empty;
                    string _salaries = string.Empty;

                    string _wseqnos = string.Empty;                    
                    string _workareanos = string.Empty;
                    string _workareanames = string.Empty;

                    string _jlseqnos = string.Empty;                    
                    string _jlnos = string.Empty;                    
                    string _jlnames = string.Empty;

                    string _proj_title = string.Empty;
                    string _proj_duration = string.Empty;
                    string _proj_details = string.Empty;
                    private DataTable _TP_STUDENT_PROJECT_TBL = null;  //sHAIKH jUNED 18-11-2022
                    private DataTable _TP_STUDENT_EDUCATION_TBL = null;  //sHAIKH jUNED 18-11-2022
                    private DataTable _TP_STUDENT_CERTIFICATION_TBL = null;  //sHAIKH jUNED 21-11-2022
                    private DataTable _TP_STUDENT_SKILLS_TBL = null;  //sHAIKH jUNED 21-11-2022
                    
                    
                #endregion

                #region Public Properties
                    public string SeqNos
                    {
                        get { return _seqnos; }
                        set { _seqnos = value; }
                    }
                    public string Orgs
                    {
                        get { return _orgs; }
                        set { _orgs = value; }
                    }
                    public string Periods
                    {
                        get { return _periods; }
                        set { _periods = value; }
                    }
                    public string Subjects
                    {
                        get { return _subjects; }
                        set { _subjects = value; }
                    }
                    public string Salaries
                    {
                        get { return _salaries; }
                        set { _salaries = value; }
                    }

                    public string WSeqNos
                    {
                        get { return _wseqnos; }
                        set { _wseqnos = value; }
                    }
                    public string WorkAreaNos
                    {
                        get { return _workareanos; }
                        set { _workareanos = value; }
                    }
                    public string WorkAreaNames
                    {
                        get { return _workareanames; }
                        set { _workareanames = value; }
                    }

                    public string JLSeqNos
                    {
                        get { return _jlseqnos; }
                        set { _jlseqnos = value; }
                    }
                    public string JLNos
                    {
                        get { return _jlnos; }
                        set { _jlnos = value; }
                    }
                    public string JLNames
                    {
                        get { return _jlnames; }
                        set { _jlnames = value; }
                    }

                    public string ProjTitle
                    {
                        get { return _proj_title ; }
                        set { _proj_title  = value; }
                    }
                    public string ProjDuration
                    {
                        get { return _proj_duration ; }
                        set { _proj_duration  = value; }
                    }
                    public string ProjDetails
                    {
                        get { return _proj_details ; }
                        set { _proj_details  = value; }
                    }

                //--------start
                    public DataTable TP_STUDENT_PROJECT_TBL
                    {
                        get
                        {
                            return this._TP_STUDENT_PROJECT_TBL;
                        }
                        set
                        {
                            if ((this._TP_STUDENT_PROJECT_TBL != value))
                            {
                                this._TP_STUDENT_PROJECT_TBL = value;
                            }
                        }
                    }



                    public DataTable TP_STUDENT_EDUCATION_TBL
                    {
                        get
                        {
                            return this._TP_STUDENT_EDUCATION_TBL;
                        }
                        set
                        {
                            if ((this._TP_STUDENT_EDUCATION_TBL != value))
                            {
                                this._TP_STUDENT_EDUCATION_TBL = value;
                            }
                        }
                    }


                    public DataTable TP_STUDENT_CERTIFICATION_TBL
                    {
                        get
                        {
                            return this._TP_STUDENT_CERTIFICATION_TBL;
                        }
                        set
                        {
                            if ((this._TP_STUDENT_CERTIFICATION_TBL != value))
                            {
                                this._TP_STUDENT_CERTIFICATION_TBL = value;
                            }
                        }
                    }


                    public DataTable TP_STUDENT_SKILLS_TBL
                    {
                        get
                        {
                            return this._TP_STUDENT_SKILLS_TBL;
                        }
                        set
                        {
                            if ((this._TP_STUDENT_SKILLS_TBL != value))
                            {
                                this._TP_STUDENT_SKILLS_TBL = value;
                            }
                        }
                    }
                //--------end

                #endregion



                    #region TP_master

                    #region Tab1
                    int _TP_ID = 0;
                    int _ACADEMIC_YEAR_ID = 0;
                    int _DEPTNO = 0;
                    string _NAME_OF_ORGNIZATION = string.Empty;
                    string _NAME_OF_MOUS = string.Empty;

                    string _BENEFITS_TO_STUDENTS_AND_STAFF = string.Empty;
                    int _COLLABORATION_YEAR = 0;
                    int _COLLABORATION_EXPIRED_YEAR = 0;

                    string _CLAIM = string.Empty;



                    public int TP_ID
                    {
                        get { return _TP_ID; }
                        set { _TP_ID = value; }
                    }
                    public int ACADEMIC_YEAR_ID
                    {
                        get { return _ACADEMIC_YEAR_ID; }
                        set { _ACADEMIC_YEAR_ID = value; }
                    }
                    public int DEPTNO
                    {
                        get { return _DEPTNO; }
                        set { _DEPTNO = value; }
                    }
                    public string NAME_OF_ORGNIZATION
                    {
                        get { return _NAME_OF_ORGNIZATION; }
                        set { _NAME_OF_ORGNIZATION = value; }
                    }
                    public string NAME_OF_MOUS
                    {
                        get { return _NAME_OF_MOUS; }
                        set { _NAME_OF_MOUS = value; }
                    }

                    public string BENEFITS_TO_STUDENTS_AND_STAFF
                    {
                        get { return _BENEFITS_TO_STUDENTS_AND_STAFF; }
                        set { _BENEFITS_TO_STUDENTS_AND_STAFF = value; }
                    }

                    public int COLLABORATION_YEAR
                    {
                        get { return _COLLABORATION_YEAR; }
                        set { _COLLABORATION_YEAR = value; }
                    }
                    public int COLLABORATION_EXPIRED_YEAR
                    {
                        get { return _COLLABORATION_EXPIRED_YEAR; }
                        set { _COLLABORATION_EXPIRED_YEAR = value; }
                    }

                    public string CLAIM
                    {
                        get { return _CLAIM; }
                        set { _CLAIM = value; }
                    }
                    #endregion

                    #region tab2

                    int _Acadyr_ID = 0;
                    int _TAB2_ID = 0;
                    int _Tab2Dept_ID = 0;
                    string _NAME_OF_stud = string.Empty;
                    string _Company = string.Empty;
                    string _Address = string.Empty;
                    string _ContactPerson = string.Empty;
                    string _tEmailID = string.Empty;
                    string _tMobileNo = string.Empty;
                    string _ttxtCompanyWebiste = string.Empty;
                    string _ttxtInternshipDuration = string.Empty;
                    int _tClass = 0;
                    int _tLevel = 0;
                    int _tModeInternship = 0;

                    public int TAB2_ID
                    {
                        get { return _TAB2_ID; }
                        set { _TAB2_ID = value; }
                    }
                    public int tModeInternship
                    {
                        get { return _tModeInternship; }
                        set { _tModeInternship = value; }
                    }
                    public int tLevel
                    {
                        get { return _tLevel; }
                        set { _tLevel = value; }
                    }
                    public int tClass
                    {
                        get { return _tClass; }
                        set { _tClass = value; }
                    }
                    public string ttxtInternshipDuration
                    {
                        get { return _ttxtInternshipDuration; }
                        set { _ttxtInternshipDuration = value; }
                    }


                    public string ttxtCompanyWebiste
                    {
                        get { return _ttxtCompanyWebiste; }
                        set { _ttxtCompanyWebiste = value; }
                    }

                    public int Acadyr_ID
                    {
                        get { return _Acadyr_ID; }
                        set { _Acadyr_ID = value; }
                    }
                    public int Tab2Dept_ID
                    {
                        get { return _Tab2Dept_ID; }
                        set { _Tab2Dept_ID = value; }
                    }

                    public string NAME_OF_stud
                    {
                        get { return _NAME_OF_stud; }
                        set { _NAME_OF_stud = value; }
                    }
                    public string Company
                    {
                        get { return _Company; }
                        set { _Company = value; }
                    }
                    public string Address
                    {
                        get { return _Address; }
                        set { _Address = value; }
                    }
                    public string ContactPerson
                    {
                        get { return _ContactPerson; }
                        set { _ContactPerson = value; }
                    }

                    public string tEmailID
                    {
                        get { return _tEmailID; }
                        set { _tEmailID = value; }
                    }
                    public string tMobileNo
                    {
                        get { return _tMobileNo; }
                        set { _tMobileNo = value; }
                    }
                    #endregion
                    #region tab3

                    int _Acadyr3_ID = 0;
                    int _TAB3_ID = 0;
                    int _Tab3Dept_ID = 0;
                    string _NAME_OF_Advisor = string.Empty;
                    string _Designation_of_Advisor = string.Empty;
                    string _Advisor_Company_Name = string.Empty;
                    string _Location = string.Empty;
                    string _tEmailID3 = string.Empty;
                    string _tMobileNo3 = string.Empty;
                    string _Expertee_Domain = string.Empty;
                    string _Credit_Claim = string.Empty;
                    string _Staff_Coordinator = string.Empty;


                    public int Acadyr3_ID
                    {
                        get { return _Acadyr3_ID; }
                        set { _Acadyr3_ID = value; }
                    }
                    public int TAB3_ID
                    {
                        get { return _TAB3_ID; }
                        set { _TAB3_ID = value; }
                    }
                    public int Tab3Dept_ID
                    {
                        get { return _Tab3Dept_ID; }
                        set { _Tab3Dept_ID = value; }
                    }

                    public string NAME_OF_Advisor
                    {
                        get { return _NAME_OF_Advisor; }
                        set { _NAME_OF_Advisor = value; }
                    }

                    public string Designation_of_Advisor
                    {
                        get { return _Designation_of_Advisor; }
                        set { _Designation_of_Advisor = value; }
                    }


                    public string Advisor_Company_Name
                    {
                        get { return _Advisor_Company_Name; }
                        set { _Advisor_Company_Name = value; }
                    }
                    public string Location
                    {
                        get { return _Location; }
                        set { _Location = value; }
                    }
                    public string tEmailID3
                    {
                        get { return _tEmailID3; }
                        set { _tEmailID3 = value; }
                    }
                    public string tMobileNo3
                    {
                        get { return _tMobileNo3; }
                        set { _tMobileNo3 = value; }
                    }
                    public string Expertee_Domain
                    {
                        get { return _Expertee_Domain; }
                        set { _Expertee_Domain = value; }
                    }
                    public string Credit_Claim
                    {
                        get { return _Credit_Claim; }
                        set { _Credit_Claim = value; }
                    }
                    public string Staff_Coordinator
                    {
                        get { return _Staff_Coordinator; }
                        set { _Staff_Coordinator = value; }
                    }




                    #endregion


                    #region tab4

                    int _Acadyr4_ID = 0;
                    int _TAB4_ID = 0;
                    int _Tab4Dept_ID = 0;
                    string _NameOfExpert = string.Empty;
                    string _DesignationExpert = string.Empty;
                    string _ExpertCompanyName = string.Empty;
                    string _t4email = string.Empty;
                    string _t4mobile = string.Empty;
                    string _TopicInteraction = string.Empty;
                    string _DateInteraction = string.Empty;
                    int _t4Mode = 0;
                    int _t4class = 0;
                    string _StdBenefitted = string.Empty;
                    string _t4StaffCoordinator = string.Empty;

                    public int Acadyr4_ID
                    {
                        get { return _Acadyr4_ID; }
                        set { _Acadyr4_ID = value; }
                    }
                    public int TAB4_ID
                    {
                        get { return _TAB4_ID; }
                        set { _TAB4_ID = value; }
                    }
                    public int Tab4Dept_ID
                    {
                        get { return _Tab4Dept_ID; }
                        set { _Tab4Dept_ID = value; }
                    }
                    public string NameOfExpert
                    {
                        get { return _NameOfExpert; }
                        set { _NameOfExpert = value; }
                    }
                    public string DesignationExpert
                    {
                        get { return _DesignationExpert; }
                        set { _DesignationExpert = value; }
                    }
                    public string ExpertCompanyName
                    {
                        get { return _ExpertCompanyName; }
                        set { _ExpertCompanyName = value; }
                    }
                    public string t4email
                    {
                        get { return _t4email; }
                        set { _t4email = value; }
                    }
                    public string t4mobile
                    {
                        get { return _t4mobile; }
                        set { _t4mobile = value; }
                    }
                    public string TopicInteraction
                    {
                        get { return _TopicInteraction; }
                        set { _TopicInteraction = value; }
                    }
                    public string DateInteraction
                    {
                        get { return _DateInteraction; }
                        set { _DateInteraction = value; }
                    }
                    public int t4Mode
                    {
                        get { return _t4Mode; }
                        set { _t4Mode = value; }
                    }
                    public int t4class
                    {
                        get { return _t4class; }
                        set { _t4class = value; }
                    }

                    public string StdBenefitted
                    {
                        get { return _StdBenefitted; }
                        set { _StdBenefitted = value; }
                    }
                    public string t4StaffCoordinator
                    {
                        get { return _t4StaffCoordinator; }
                        set { _t4StaffCoordinator = value; }
                    }




                    #endregion

                    #region tab5

                    int _Acadyr5_ID = 0;
                    int _TAB5_ID = 0;
                    int _Tab5Dept_ID = 0;
                    string _NameOfAlumni = string.Empty;
                    string _DesignationofAlumni = string.Empty;
                    int _YearofPassout = 0;
                    string _CompanyName5 = string.Empty;
                    string _t5email = string.Empty;
                    string _t5mobile = string.Empty;
                    string _TopicInteraction5 = string.Empty;
                    string _DateInteraction5 = string.Empty;
                    int _t5Mode = 0;
                    int _t5class = 0;
                    string _StdBenefitted5 = string.Empty;
                    string _StaffCoordinator5 = string.Empty;


                    public int Acadyr5_ID
                    {
                        get { return _Acadyr5_ID; }
                        set { _Acadyr5_ID = value; }
                    }
                    public int TAB5_ID
                    {
                        get { return _TAB5_ID; }
                        set { _TAB5_ID = value; }
                    }
                    public int Tab5Dept_ID
                    {
                        get { return _Tab5Dept_ID; }
                        set { _Tab5Dept_ID = value; }
                    }
                    public string NameOfAlumni
                    {
                        get { return _NameOfAlumni; }
                        set { _NameOfAlumni = value; }
                    }
                    public string DesignationofAlumni
                    {
                        get { return _DesignationofAlumni; }
                        set { _DesignationofAlumni = value; }
                    }
                    public int YearofPassout
                    {
                        get { return _YearofPassout; }
                        set { _YearofPassout = value; }
                    }

                    public string CompanyName5
                    {
                        get { return _CompanyName5; }
                        set { _CompanyName5 = value; }
                    }
                    public string t5email
                    {
                        get { return _t5email; }
                        set { _t5email = value; }
                    }
                    public string t5mobile
                    {
                        get { return _t5mobile; }
                        set { _t5mobile = value; }
                    }
                    public string TopicInteraction5
                    {
                        get { return _TopicInteraction5; }
                        set { _TopicInteraction5 = value; }
                    }
                    public string DateInteraction5
                    {
                        get { return _DateInteraction5; }
                        set { _DateInteraction5 = value; }
                    }
                    public int t5Mode
                    {
                        get { return _t5Mode; }
                        set { _t5Mode = value; }
                    }
                    public int t5class
                    {
                        get { return _t5class; }
                        set { _t5class = value; }
                    }

                    public string StdBenefitted5
                    {
                        get { return _StdBenefitted5; }
                        set { _StdBenefitted5 = value; }
                    }
                    public string StaffCoordinator5
                    {
                        get { return _StaffCoordinator5; }
                        set { _StaffCoordinator5 = value; }
                    }




                    #endregion

                    #region tab6

                    int _Acadyr6_ID = 0;
                    int _TAB6_ID = 0;
                    int _Tab6Dept_ID = 0;
                    string _NameOfEvent = string.Empty;
                    int _NumberofStudentsParticipated = 0;
                    string _Achievement = string.Empty;
                    decimal _AwardAmountinRs = 0;
                    int _NumberofStudentPlaced = 0;
                    int _FinancialAssistancefromInstitute = 0;
                    decimal _FinancialAssistancefromInstituteinRs = 0;
                    string _Remark = string.Empty;
                    string _StaffCoordinator6 = string.Empty;



                    public int Acadyr6_ID
                    {
                        get { return _Acadyr6_ID; }
                        set { _Acadyr6_ID = value; }
                    }
                    public int TAB6_ID
                    {
                        get { return _TAB6_ID; }
                        set { _TAB6_ID = value; }
                    }
                    public int Tab6Dept_ID
                    {
                        get { return _Tab6Dept_ID; }
                        set { _Tab6Dept_ID = value; }
                    }
                    public string NameOfEvent
                    {
                        get { return _NameOfEvent; }
                        set { _NameOfEvent = value; }
                    }
                    public int NumberofStudentsParticipated
                    {
                        get { return _NumberofStudentsParticipated; }
                        set { _NumberofStudentsParticipated = value; }
                    }
                    public string Achievement
                    {
                        get { return _Achievement; }
                        set { _Achievement = value; }
                    }
                    public decimal AwardAmountinRs
                    {
                        get { return _AwardAmountinRs; }
                        set { _AwardAmountinRs = value; }
                    }
                    public int NumberofStudentPlaced
                    {
                        get { return _NumberofStudentPlaced; }
                        set { _NumberofStudentPlaced = value; }
                    }
                    public int FinancialAssistancefromInstitute
                    {
                        get { return _FinancialAssistancefromInstitute; }
                        set { _FinancialAssistancefromInstitute = value; }
                    }
                    public decimal FinancialAssistancefromInstituteinRs
                    {
                        get { return _FinancialAssistancefromInstituteinRs; }
                        set { _FinancialAssistancefromInstituteinRs = value; }
                    }

                    public string Remark
                    {
                        get { return _Remark; }
                        set { _Remark = value; }
                    }
                    public string StaffCoordinator6
                    {
                        get { return _StaffCoordinator6; }
                        set { _StaffCoordinator6 = value; }
                    }
                    #endregion


                    #region tab7


                    int _Acadyr7_ID = 0;
                    int _TAB7_ID = 0;
                    int _Tab7Dept_ID = 0;
                    string _NameOfStudent = string.Empty;
                    int _ForeignLanguage = 0;
                    int _Certification = 0;
                    int _Level7 = 0;
                    string _LevelofCertification = string.Empty;
                    string _StaffCoordinator7 = string.Empty;


                    public int Acadyr7_ID
                    {
                        get { return _Acadyr7_ID; }
                        set { _Acadyr7_ID = value; }
                    }
                    public int TAB7_ID
                    {
                        get { return _TAB7_ID; }
                        set { _TAB7_ID = value; }
                    }
                    public int Tab7Dept_ID
                    {
                        get { return _Tab7Dept_ID; }
                        set { _Tab7Dept_ID = value; }
                    }
                    public string NameOfStudent
                    {
                        get { return _NameOfStudent; }
                        set { _NameOfStudent = value; }
                    }
                    public int ForeignLanguage
                    {
                        get { return _ForeignLanguage; }
                        set { _ForeignLanguage = value; }
                    }
                    public int Certification
                    {
                        get { return _Certification; }
                        set { _Certification = value; }
                    }
                    public int Level7
                    {
                        get { return _Level7; }
                        set { _Level7 = value; }
                    }

                    public string LevelofCertification
                    {
                        get { return _LevelofCertification; }
                        set { _LevelofCertification = value; }
                    }
                    public string StaffCoordinator7
                    {
                        get { return _StaffCoordinator7; }
                        set { _StaffCoordinator7 = value; }
                    }

                    #endregion

                    #region tab8

                    int _Acadyr8_ID = 0;
                    int _TAB8_ID = 0;
                    int _Tab8Dept_ID = 0;
                    string _NameOfCompany8 = string.Empty;
                    string _Location8 = string.Empty;
                    string _Address8 = string.Empty;
                    string _EmailID8 = string.Empty;
                    string _MobileNo8 = string.Empty;
                    string _Website8 = string.Empty;
                    string _DateOfVisit8 = string.Empty;
                    int _Class8 = 0;
                    int _NoofStudentVisited8 = 0;
                    string _StaffCoordinator8 = string.Empty;


                    public int Acadyr8_ID
                    {
                        get { return _Acadyr8_ID; }
                        set { _Acadyr8_ID = value; }
                    }
                    public int TAB8_ID
                    {
                        get { return _TAB8_ID; }
                        set { _TAB8_ID = value; }
                    }
                    public int Tab8Dept_ID
                    {
                        get { return _Tab8Dept_ID; }
                        set { _Tab8Dept_ID = value; }
                    }
                    public string NameOfCompany8
                    {
                        get { return _NameOfCompany8; }
                        set { _NameOfCompany8 = value; }
                    }
                    public string Location8
                    {
                        get { return _Location8; }
                        set { _Location8 = value; }
                    }

                    public string Address8
                    {
                        get { return _Address8; }
                        set { _Address8 = value; }
                    }
                    public string EmailID8
                    {
                        get { return _EmailID8; }
                        set { _EmailID8 = value; }
                    }
                    public string MobileNo8
                    {
                        get { return _MobileNo8; }
                        set { _MobileNo8 = value; }
                    }
                    public string Website8
                    {
                        get { return _Website8; }
                        set { _Website8 = value; }
                    }
                    public string DateOfVisit8
                    {
                        get { return _DateOfVisit8; }
                        set { _DateOfVisit8 = value; }
                    }

                    public int Class8
                    {
                        get { return _Class8; }
                        set { _Class8 = value; }
                    }
                    public int NoofStudentVisited8
                    {
                        get { return _NoofStudentVisited8; }
                        set { _NoofStudentVisited8 = value; }
                    }
                    public string StaffCoordinator8
                    {
                        get { return _StaffCoordinator8; }
                        set { _StaffCoordinator8 = value; }
                    }

                    #endregion

                    #region tab9

                    int _Acadyr9_ID = 0;
                    int _TAB9_ID = 0;
                    int _Tab9Dept_ID = 0;
                    string _NameOfCompany9 = string.Empty;
                    string _Location9 = string.Empty;
                    string _DateOfInteraction9 = string.Empty;
                    int _Mode9 = 0;
                    string _NoofStudentBenefitted9 = string.Empty;
                    string _StaffCoordinator9 = string.Empty;
                    string _Claim9 = string.Empty;

                    public int Acadyr9_ID
                    {
                        get { return _Acadyr9_ID; }
                        set { _Acadyr9_ID = value; }
                    }
                    public int TAB9_ID
                    {
                        get { return _TAB9_ID; }
                        set { _TAB9_ID = value; }
                    }
                    public int Tab9Dept_ID
                    {
                        get { return _Tab9Dept_ID; }
                        set { _Tab9Dept_ID = value; }
                    }
                    public string NameOfCompany9
                    {
                        get { return _NameOfCompany9; }
                        set { _NameOfCompany9 = value; }
                    }
                    public string Location9
                    {
                        get { return _Location9; }
                        set { _Location9 = value; }
                    }
                    public string DateOfInteraction9
                    {
                        get { return _DateOfInteraction9; }
                        set { _DateOfInteraction9 = value; }
                    }
                    public int Mode9
                    {
                        get { return _Mode9; }
                        set { _Mode9 = value; }
                    }

                    public string NoofStudentBenefitted9
                    {
                        get { return _NoofStudentBenefitted9; }
                        set { _NoofStudentBenefitted9 = value; }
                    }
                    public string StaffCoordinator9
                    {
                        get { return _StaffCoordinator9; }
                        set { _StaffCoordinator9 = value; }
                    }
                    public string Claim9
                    {
                        get { return _Claim9; }
                        set { _Claim9 = value; }
                    }


                    #endregion

                    #endregion

                    #region Career_Profile
                    #region Tab_3
                    int _SkillName = 0;
                    int _Proficiency = 0;
                    string _ReleventDocument = string.Empty;

                    public int SkillName
                    {
                        get { return _SkillName; }
                        set { _SkillName = value; }
                    }

                    public int Proficiency
                    {
                        get { return _Proficiency; }
                        set { _Proficiency = value; }
                    }

                    public string ReleventDocument
                    {
                        get { return _ReleventDocument; }
                        set { _ReleventDocument = value; }
                    }

                    #endregion

                    #region Tab_4
                    string _ProjectTitle = string.Empty;
                    string _ProjectDomian = string.Empty;
                    string _GuideSupervissorName = string.Empty;
                    DateTime _StartDate = DateTime.MinValue;
                    DateTime _EndDate = DateTime.MinValue;
                    int _CurrentlyWork = 0;
                    string _Descripition = string.Empty;
                    string _ReleventDocument1 = string.Empty;

                    public string ProjectTitle
                    {
                        get { return _ProjectTitle; }
                        set { _ProjectTitle = value; }
                    }
                    public string ProjectDomian
                    {
                        get { return _ProjectDomian; }
                        set { _ProjectDomian = value; }
                    }
                    public string GuideSupervissorName
                    {
                        get { return _GuideSupervissorName; }
                        set { _GuideSupervissorName = value; }
                    }
                    public DateTime StartDate
                    {
                        get { return _StartDate; }
                        set { _StartDate = value; }

                    }
                    public DateTime EndDate
                    {
                        get { return _EndDate; }
                        set { _EndDate = value; }

                    }
                    public int CurrentlyWork
                    {
                        get { return _CurrentlyWork; }
                        set { _CurrentlyWork = value; }
                    }
                    public string Descripition
                    {
                        get { return _Descripition; }
                        set { _Descripition = value; }
                    }
                    public string ReleventDocument1
                    {
                        get { return _ReleventDocument1; }
                        set { _ReleventDocument1 = value; }
                    }

                    #endregion


                    #region Tab_5
                    string _Title = string.Empty;
                    string _CertifiedBy = string.Empty;
                    string _Grade = string.Empty;
                    DateTime _FromDate = DateTime.MinValue;
                    DateTime _ToDate = DateTime.MinValue;
                    int _CurrentlyWork1 = 0;
                    string _ReleventDocument2 = string.Empty;

                    public string Title
                    {
                        get { return _Title; }
                        set { _Title = value; }
                    }
                    public string CertifiedBy
                    {
                        get { return _CertifiedBy; }
                        set { _CertifiedBy = value; }
                    }
                    public string Grade
                    {
                        get { return _Grade; }
                        set { _Grade = value; }
                    }
                    public DateTime FromDate
                    {
                        get { return _FromDate; }
                        set { _FromDate = value; }

                    }
                    public DateTime ToDate
                    {
                        get { return _ToDate; }
                        set { _ToDate = value; }

                    }
                    public int CurrentlyWork1
                    {
                        get { return _CurrentlyWork1; }
                        set { _CurrentlyWork1 = value; }
                    }
                    public string ReleventDocument2
                    {
                        get { return _ReleventDocument2; }
                        set { _ReleventDocument2 = value; }
                    }

                    #endregion

                    #region Language Tab_6

                    int _language = 0;
                    int _ProficiencyLang = 0;
                    string __ReleventDocument3 = string.Empty;

                    public int language
                    {
                        get { return _language; }
                        set { _language = value; }
                    }
                    public int ProficiencyLang
                    {
                        get { return _ProficiencyLang; }
                        set { _ProficiencyLang = value; }
                    }
                    public string ReleventDocument3
                    {
                        get { return __ReleventDocument3; }
                        set { __ReleventDocument3 = value; }
                    }
                    #endregion

                    #region tab_7 Awards and Recognitions
                    string _Award_Title = string.Empty;
                    DateTime _Date_of_Award = DateTime.MinValue;
                    string _Given_By = string.Empty;
                    int _Level = 0;
                    string __ReleventDocument4 = string.Empty;

                    public string Award_Title
                    {
                        get { return _Award_Title; }
                        set { _Award_Title = value; }
                    }
                    public DateTime Date_of_Award
                    {
                        get { return _Date_of_Award; }
                        set { _Date_of_Award = value; }
                    }
                    public string Given_By
                    {
                        get { return _Given_By; }
                        set { _Given_By = value; }
                    }
                    public int Level
                    {
                        get { return _Level; }
                        set { _Level = value; }
                    }
                    public string ReleventDocument4
                    {
                        get { return __ReleventDocument4; }
                        set { __ReleventDocument4 = value; }
                    }
                    #endregion

                    #region tab_8 Competitions


                    string _Competition_Title = string.Empty;
                    string _Organized_By = string.Empty;
                    int _Level1 = 0;
                    DateTime _From_Date = DateTime.MinValue;
                    DateTime _To_Date = DateTime.MinValue;
                    string _Project_Title = string.Empty;
                    int _Participation_Status = 0;
                    string __ReleventDocument5 = string.Empty;

                    public string Competition_Title
                    {
                        get { return _Competition_Title; }
                        set { _Competition_Title = value; }
                    }
                    public string Organized_By
                    {
                        get { return _Organized_By; }
                        set { _Organized_By = value; }
                    }
                    public int Level1
                    {
                        get { return _Level1; }
                        set { _Level1 = value; }
                    }
                    public DateTime From_Date
                    {
                        get { return _From_Date; }
                        set { _From_Date = value; }
                    }
                    public DateTime To_Date
                    {
                        get { return _To_Date; }
                        set { _To_Date = value; }
                    }
                    public string Project_Title
                    {
                        get { return _Project_Title; }
                        set { _Project_Title = value; }
                    }
                    public int Participation_Status
                    {
                        get { return _Participation_Status; }
                        set { _Participation_Status = value; }
                    }
                    public string ReleventDocument5
                    {
                        get { return __ReleventDocument5; }
                        set { __ReleventDocument5 = value; }
                    }

                    #endregion


                    #region tab_9 Training And Workshop


                    string _Training_Title = string.Empty;
                    string _Organized_By1 = string.Empty;
                    int _Category = 0;
                    DateTime _From_Date1 = DateTime.MinValue;
                    DateTime _To_Date1 = DateTime.MinValue;
                    string __ReleventDocument6 = string.Empty;

                    public string Training_Title
                    {
                        get { return _Training_Title; }
                        set { _Training_Title = value; }
                    }
                    public string Organized_By1
                    {
                        get { return _Organized_By1; }
                        set { _Organized_By1 = value; }
                    }
                    public int Category
                    {
                        get { return _Category; }
                        set { _Category = value; }
                    }
                    public DateTime From_Date1
                    {
                        get { return _From_Date1; }
                        set { _From_Date1 = value; }
                    }
                    public DateTime To_Date1
                    {
                        get { return _To_Date1; }
                        set { _To_Date1 = value; }
                    }
                    public string ReleventDocument6
                    {
                        get { return __ReleventDocument6; }
                        set { __ReleventDocument6 = value; }
                    }

                    #endregion


                    #region tab_10 Test Scores


                    int _Exam = 0;
                    int _QualificationStatus = 0;
                    DateTime _Year = DateTime.MinValue;
                    string _TestScore = string.Empty;
                    string __ReleventDocument7 = string.Empty;
                    int _IsBlob = 0;

                    public int Exam
                    {
                        get { return _Exam; }
                        set { _Exam = value; }
                    }
                    public int IsBlob
                    {
                        get { return _IsBlob; }
                        set { _IsBlob = value; }
                    }
                    public int QualificationStatus
                    {
                        get { return _QualificationStatus; }
                        set { _QualificationStatus = value; }
                    }
                    public DateTime Year
                    {
                        get { return _Year; }
                        set { _Year = value; }
                    }
                    public string TestScore
                    {
                        get { return _TestScore; }
                        set { _TestScore = value; }
                    }
                    public string ReleventDocument7
                    {
                        get { return __ReleventDocument7; }
                        set { __ReleventDocument7 = value; }
                    }

                    #endregion

                    #region Exam Details
                    int _GAP = 0;
                    private System.Nullable<bool> _IS_GAP;

                    public int GAP
                    {
                        get { return _GAP; }
                        set { _GAP = value; }
                    }

                    public System.Nullable<bool> IS_GAP
                    {
                        get { return _IS_GAP; }
                        set { _IS_GAP = value; }
                    }
                    #endregion

                    #region
                    string _ReleventDocument8 = string.Empty;
                    int _IsBlob1 = 0;

                    public string ReleventDocument8
                    {
                        get { return _ReleventDocument8; }
                        set { _ReleventDocument8 = value; }
                    }
                    public int IsBlob1
                    {
                        get { return _IsBlob1; }
                        set { _IsBlob1 = value; }
                    }
                    #endregion
                    #endregion


                    #region TP Companies Details Form
                    #region Tab0
                    int _TP_CMPID = 0;
                    int _COMPANY_ID = 0;
                    int _SECTOR_ID = 0;
                    string _INCORPORATION_STATUS = string.Empty;
                    string _ADDRESS = string.Empty;

                    string _WEBSITE = string.Empty;
                    string _MOBILE_NO = string.Empty;
                    string _MANAGER_CONTACT_PERSON_NAME = string.Empty;
                    string _EMAIL_ID = string.Empty;




                    public int TP_CMPID
                    {
                        get { return _TP_CMPID; }
                        set { _TP_CMPID = value; }
                    }
                    public int COMPANY_ID
                    {
                        get { return _COMPANY_ID; }
                        set { _COMPANY_ID = value; }
                    }
                    public int SECTOR_ID
                    {
                        get { return _SECTOR_ID; }
                        set { _SECTOR_ID = value; }
                    }
                    public string INCORPORATION_STATUS
                    {
                        get { return _INCORPORATION_STATUS; }
                        set { _INCORPORATION_STATUS = value; }
                    }
                    public string ADDRESS
                    {
                        get { return _ADDRESS; }
                        set { _ADDRESS = value; }
                    }

                    public string WEBSITE
                    {
                        get { return _WEBSITE; }
                        set { _WEBSITE = value; }
                    }

                    public string MOBILE_NO
                    {
                        get { return _MOBILE_NO; }
                        set { _MOBILE_NO = value; }
                    }
                    public string MANAGER_CONTACT_PERSON_NAME
                    {
                        get { return _MANAGER_CONTACT_PERSON_NAME; }
                        set { _MANAGER_CONTACT_PERSON_NAME = value; }
                    }

                    public string EMAIL_ID
                    {
                        get { return _EMAIL_ID; }
                        set { _EMAIL_ID = value; }
                    }
                    #endregion

                    #region Tab 1
                    private int _DIS_ID;
                    private string _DISCIPLINE;
                    private int _LEVEL;
                    private string _YEAR_OF_INCEPTION;
                    private int _NUMBER_OF_SUBDTREAM;
                    private int _NUMBER_OF_FACULTIES;
                    private int _ELIGIBLE_STUDENT_1;
                    private int _ELIGIBLE_STUDENT_2;

                    public int DIS_ID
                    {
                        get { return _DIS_ID; }
                        set { _DIS_ID = value; }
                    }
                    public string DISCIPLINE
                    {
                        get { return _DISCIPLINE; }
                        set { _DISCIPLINE = value; }
                    }
                    public int LEVEL
                    {
                        get { return _LEVEL; }
                        set { _LEVEL = value; }
                    }
                    public string YEAR_OF_INCEPTION
                    {
                        get { return _YEAR_OF_INCEPTION; }
                        set { _YEAR_OF_INCEPTION = value; }
                    }
                    public int NUMBER_OF_SUBDTREAM
                    {
                        get { return _NUMBER_OF_SUBDTREAM; }
                        set { _NUMBER_OF_SUBDTREAM = value; }
                    }
                    public int NUMBER_OF_FACULTIES
                    {
                        get { return _NUMBER_OF_FACULTIES; }
                        set { _NUMBER_OF_FACULTIES = value; }
                    }
                    public int ELIGIBLE_STUDENT_1
                    {
                        get { return _ELIGIBLE_STUDENT_1; }
                        set { _ELIGIBLE_STUDENT_1 = value; }
                    }
                    public int ELIGIBLE_STUDENT_2
                    {
                        get { return _ELIGIBLE_STUDENT_2; }
                        set { _ELIGIBLE_STUDENT_2 = value; }
                    }
                    #endregion

                    #region Tab 2
                    private int _TP_CUR_ID;
                    private int _COMPANY_NAME;
                    private int _COMPANY_SACTOR;
                    private string _INCORPORATION_SACTOR;
                    private string _ADDRESS_CURR;
                    private string _WEBSITE_CURR;
                    private string _MOBILE_NO_CURR;
                    private string _MANAGER_NAME_CURR;
                    private string _EMAIL_ID_CURR;
                    private int _DISCIPLINE_CURR;
                    private int _LEVEL_CURR;
                    private DateTime _FROM_DATE_CURR;
                    private DateTime _TO_DATE_CURR;
                    private int _NO_OF_STUDENTS;

                    public int TP_CUR_ID
                    {
                        get { return _TP_CUR_ID; }
                        set { _TP_CUR_ID = value; }
                    }
                    public int COMPANY_NAME
                    {
                        get { return _COMPANY_NAME; }
                        set { _COMPANY_NAME = value; }
                    }
                    public int COMPANY_SACTOR
                    {
                        get { return _COMPANY_SACTOR; }
                        set { _COMPANY_SACTOR = value; }
                    }
                    public string INCORPORATION_SACTOR
                    {
                        get { return _INCORPORATION_SACTOR; }
                        set { _INCORPORATION_SACTOR = value; }
                    }
                    public string ADDRESS_CURR
                    {
                        get { return _ADDRESS_CURR; }
                        set { _ADDRESS_CURR = value; }
                    }
                    public string WEBSITE_CURR
                    {
                        get { return _WEBSITE_CURR; }
                        set { _WEBSITE_CURR = value; }
                    }
                    public string MOBILE_NO_CURR
                    {
                        get { return _MOBILE_NO_CURR; }
                        set { _MOBILE_NO_CURR = value; }
                    }
                    public string MANAGER_NAME_CURR
                    {
                        get { return _MANAGER_NAME_CURR; }
                        set { _MANAGER_NAME_CURR = value; }
                    }
                    public string EMAIL_ID_CURR
                    {
                        get { return _EMAIL_ID_CURR; }
                        set { _EMAIL_ID_CURR = value; }
                    }
                    public int DISCIPLINE_CURR
                    {
                        get { return _DISCIPLINE_CURR; }
                        set { _DISCIPLINE_CURR = value; }
                    }
                    public int LEVEL_CURR
                    {
                        get { return _LEVEL_CURR; }
                        set { _LEVEL_CURR = value; }
                    }
                    public DateTime FROM_DATE_CURR
                    {
                        get { return _FROM_DATE_CURR; }
                        set { _FROM_DATE_CURR = value; }
                    }
                    public DateTime TO_DATE_CURR
                    {
                        get { return _TO_DATE_CURR; }
                        set { _TO_DATE_CURR = value; }
                    }
                    public int NO_OF_STUDENTS
                    {
                        get { return _NO_OF_STUDENTS; }
                        set { _NO_OF_STUDENTS = value; }
                    }
                    #endregion

                    #region Tab 3
                    private int _TP_VIS_ID;
                    private int _COMPANY_NAME_VIS;
                    private int _COMPANY_SACTOR_VIS;
                    private string _INCORPORATION_SACTOR_VIS;
                    private string _DESIGNATION;
                    private string _FIRST_NAME;
                    private string _LAST_NAME;
                    private string _ADDRESS_VIS;
                    private string _WEBSITE_VIS;
                    private string _MOBILE_NO_VIS;
                    private string _MANAGER_NAME_VIS;
                    private string _EMAIL_ID_VIS;
                    private int _DISCIPLINE_VIS;
                    private int _LEVEL_VIS;
                    private DateTime _LECTURE_DATE;
                    private int _NO_OF_STUDENTS_VIS;

                    public int TP_VIS_ID
                    {
                        get { return _TP_VIS_ID; }
                        set { _TP_VIS_ID = value; }
                    }
                    public int COMPANY_NAME_VIS
                    {
                        get { return _COMPANY_NAME_VIS; }
                        set { _COMPANY_NAME_VIS = value; }
                    }
                    public int COMPANY_SACTOR_VIS
                    {
                        get { return _COMPANY_SACTOR_VIS; }
                        set { _COMPANY_SACTOR_VIS = value; }
                    }
                    public string INCORPORATION_SACTOR_VIS
                    {
                        get { return _INCORPORATION_SACTOR_VIS; }
                        set { _INCORPORATION_SACTOR_VIS = value; }
                    }
                    public string DESIGNATION
                    {
                        get { return _DESIGNATION; }
                        set { _DESIGNATION = value; }
                    }
                    public string FIRST_NAME
                    {
                        get { return _FIRST_NAME; }
                        set { _FIRST_NAME = value; }
                    }
                    public string LAST_NAME
                    {
                        get { return _LAST_NAME; }
                        set { _LAST_NAME = value; }
                    }
                    public string ADDRESS_VIS
                    {
                        get { return _ADDRESS_VIS; }
                        set { _ADDRESS_VIS = value; }
                    }
                    public string WEBSITE_VIS
                    {
                        get { return _WEBSITE_VIS; }
                        set { _WEBSITE_VIS = value; }
                    }
                    public string MOBILE_NO_VIS
                    {
                        get { return _MOBILE_NO_VIS; }
                        set { _MOBILE_NO_VIS = value; }
                    }
                    public string MANAGER_NAME_VIS
                    {
                        get { return _MANAGER_NAME_VIS; }
                        set { _MANAGER_NAME_VIS = value; }
                    }
                    public string EMAIL_ID_VIS
                    {
                        get { return _EMAIL_ID_VIS; }
                        set { _EMAIL_ID_VIS = value; }
                    }
                    public int DISCIPLINE_VIS
                    {
                        get { return _DISCIPLINE_VIS; }
                        set { _DISCIPLINE_VIS = value; }
                    }
                    public int LEVEL_VIS
                    {
                        get { return _LEVEL_VIS; }
                        set { _LEVEL_VIS = value; }
                    }
                    public DateTime LECTURE_DATE
                    {
                        get { return _LECTURE_DATE; }
                        set { _LECTURE_DATE = value; }
                    }

                    public int NO_OF_STUDENTS_VIS
                    {
                        get { return _NO_OF_STUDENTS_VIS; }
                        set { _NO_OF_STUDENTS_VIS = value; }
                    }
                    #endregion

                    #region Tab 4
                    private int _TP_IV_ID;
                    private int _COMPANY_NAME_IV;
                    private int _COMPANY_SACTOR_IV;
                    private string _INCORPORATION_SACTOR_IV;
                    private string _ADDRESS_IV;
                    private string _WEBSITE_IV;
                    private string _MOBILE_NO_IV;
                    private string _MANAGER_NAME_IV;
                    private string _EMAIL_ID_IV;
                    private int _DISCIPLINE_IV;
                    private int _LEVEL_IV;
                    private DateTime _FROM_DATE_IV;
                    private DateTime _TO_DATE_IV;
                    private int _NO_OF_STUDENTS_IV;

                    public int TP_IV_ID
                    {
                        get { return _TP_IV_ID; }
                        set { _TP_IV_ID = value; }
                    }
                    public int COMPANY_NAME_IV
                    {
                        get { return _COMPANY_NAME_IV; }
                        set { _COMPANY_NAME_IV = value; }
                    }
                    public int COMPANY_SACTOR_IV
                    {
                        get { return _COMPANY_SACTOR_IV; }
                        set { _COMPANY_SACTOR_IV = value; }
                    }
                    public string INCORPORATION_SACTOR_IV
                    {
                        get { return _INCORPORATION_SACTOR_IV; }
                        set { _INCORPORATION_SACTOR_IV = value; }
                    }

                    public string ADDRESS_IV
                    {
                        get { return _ADDRESS_IV; }
                        set { _ADDRESS_IV = value; }
                    }
                    public string WEBSITE_IV
                    {
                        get { return _WEBSITE_IV; }
                        set { _WEBSITE_IV = value; }
                    }
                    public string MOBILE_NO_IV
                    {
                        get { return _MOBILE_NO_IV; }
                        set { _MOBILE_NO_IV = value; }
                    }
                    public string MANAGER_NAME_IV
                    {
                        get { return _MANAGER_NAME_IV; }
                        set { _MANAGER_NAME_IV = value; }
                    }
                    public string EMAIL_ID_IV
                    {
                        get { return _EMAIL_ID_IV; }
                        set { _EMAIL_ID_IV = value; }
                    }
                    public int DISCIPLINE_IV
                    {
                        get { return _DISCIPLINE_IV; }
                        set { _DISCIPLINE_IV = value; }
                    }
                    public int LEVEL_IV
                    {
                        get { return _LEVEL_IV; }
                        set { _LEVEL_IV = value; }
                    }
                    public DateTime FROM_DATE_IV
                    {
                        get { return _FROM_DATE_IV; }
                        set { _FROM_DATE_IV = value; }
                    }
                    public DateTime TO_DATE_IV
                    {
                        get { return _TO_DATE_IV; }
                        set { _TO_DATE_IV = value; }
                    }

                    public int NO_OF_STUDENTS_IV
                    {
                        get { return _NO_OF_STUDENTS_IV; }
                        set { _NO_OF_STUDENTS_IV = value; }
                    }
                    #endregion

                    #region Tab 5
                    private int _TP_GL_ID;
                    private int _COMPANY_NAME_GUL;
                    private int _COMPANY_SACTOR_GUL;
                    private string _INCORPORATION_SACTOR_GUL;
                    private string _DESIGNATION_GUL;
                    private string _FIRST_NAME_GUL;
                    private string _LAST_NAME_GUL;
                    private string _ADDRESS_GUL;
                    private string _WEBSITE_GUL;
                    private string _MOBILE_NO_GUL;
                    private string _MANAGER_NAME_GUL;
                    private string _EMAIL_ID_GUL;
                    private int _DISCIPLINE_GUL;
                    private int _LEVEL_GUL;
                    private DateTime _LECTURE_DATE_GUL;
                    private int _NO_OF_STUDENTS_GUL;

                    public int TP_GL_ID
                    {
                        get { return _TP_GL_ID; }
                        set { _TP_GL_ID = value; }
                    }
                    public int COMPANY_NAME_GUL
                    {
                        get { return _COMPANY_NAME_GUL; }
                        set { _COMPANY_NAME_GUL = value; }
                    }
                    public int COMPANY_SACTOR_GUL
                    {
                        get { return _COMPANY_SACTOR_GUL; }
                        set { _COMPANY_SACTOR_GUL = value; }
                    }
                    public string INCORPORATION_SACTOR_GUL
                    {
                        get { return _INCORPORATION_SACTOR_GUL; }
                        set { _INCORPORATION_SACTOR_GUL = value; }
                    }
                    public string DESIGNATION_GUL
                    {
                        get { return _DESIGNATION_GUL; }
                        set { _DESIGNATION_GUL = value; }
                    }
                    public string FIRST_NAME_GUL
                    {
                        get { return _FIRST_NAME_GUL; }
                        set { _FIRST_NAME_GUL = value; }
                    }
                    public string LAST_NAME_GUL
                    {
                        get { return _LAST_NAME_GUL; }
                        set { _LAST_NAME_GUL = value; }
                    }
                    public string ADDRESS_GUL
                    {
                        get { return _ADDRESS_GUL; }
                        set { _ADDRESS_GUL = value; }
                    }
                    public string WEBSITE_GUL
                    {
                        get { return _WEBSITE_GUL; }
                        set { _WEBSITE_GUL = value; }
                    }
                    public string MOBILE_NO_GUL
                    {
                        get { return _MOBILE_NO_GUL; }
                        set { _MOBILE_NO_GUL = value; }
                    }
                    public string MANAGER_NAME_GUL
                    {
                        get { return _MANAGER_NAME_GUL; }
                        set { _MANAGER_NAME_GUL = value; }
                    }
                    public string EMAIL_ID_GUL
                    {
                        get { return _EMAIL_ID_GUL; }
                        set { _EMAIL_ID_GUL = value; }
                    }
                    public int DISCIPLINE_GUL
                    {
                        get { return _DISCIPLINE_GUL; }
                        set { _DISCIPLINE_GUL = value; }
                    }
                    public int LEVEL_GUL
                    {
                        get { return _LEVEL_GUL; }
                        set { _LEVEL_GUL = value; }
                    }
                    public DateTime LECTURE_DATE_GUL
                    {
                        get { return _LECTURE_DATE_GUL; }
                        set { _LECTURE_DATE_GUL = value; }
                    }

                    public int NO_OF_STUDENTS_GUL
                    {
                        get { return _NO_OF_STUDENTS_GUL; }
                        set { _NO_OF_STUDENTS_GUL = value; }
                    }
                    #endregion

                    #region Tab 6
                    private int _TP_FLI_ID;
                    private string _EXTERNAL_FACULTY_FLI;
                    private int _FACULTY_ID_FLI;
                    private int _COMPANY_NAME_FLI;
                    private string _ADDRESS_FLI;
                    private string _WEBSITE_FLI;
                    private string _MOBILE_NO_FLI;
                    private string _MANAGER_NAME_FLI;
                    private string _EMAIL_ID_FLI;
                    private int _DISCIPLINE_FLI;
                    private int _LEVEL_FLI;

                    public int TP_FLI_ID
                    {
                        get { return _TP_FLI_ID; }
                        set { _TP_FLI_ID = value; }
                    }

                    public string EXTERNAL_FACULTY_FLI
                    {
                        get { return _EXTERNAL_FACULTY_FLI; }
                        set { _EXTERNAL_FACULTY_FLI = value; }
                    }
                    public int FACULTY_ID_FLI
                    {
                        get { return _FACULTY_ID_FLI; }
                        set { _FACULTY_ID_FLI = value; }
                    }
                    public int COMPANY_NAME_FLI
                    {
                        get { return _COMPANY_NAME_FLI; }
                        set { _COMPANY_NAME_FLI = value; }
                    }
                    public string ADDRESS_FLI
                    {
                        get { return _ADDRESS_FLI; }
                        set { _ADDRESS_FLI = value; }
                    }
                    public string WEBSITE_FLI
                    {
                        get { return _WEBSITE_FLI; }
                        set { _WEBSITE_FLI = value; }
                    }
                    public string MOBILE_NO_FLI
                    {
                        get { return _MOBILE_NO_FLI; }
                        set { _MOBILE_NO_FLI = value; }
                    }
                    public string MANAGER_NAME_FLI
                    {
                        get { return _MANAGER_NAME_FLI; }
                        set { _MANAGER_NAME_FLI = value; }
                    }
                    public string EMAIL_ID_FLI
                    {
                        get { return _EMAIL_ID_FLI; }
                        set { _EMAIL_ID_FLI = value; }
                    }
                    public int DISCIPLINE_FLI
                    {
                        get { return _DISCIPLINE_FLI; }
                        set { _DISCIPLINE_FLI = value; }
                    }
                    public int LEVEL_FLI
                    {
                        get { return _LEVEL_FLI; }
                        set { _LEVEL_FLI = value; }
                    }

                    #endregion


                    #region Tab 7
                    private int _TP_FPTI_ID;
                    private int _COMPANY_NAME_FPTI;
                    private int _SECTOR_NAME_FPTI;
                    private string _INCORPORATION_STATUS_FPTI;
                    private int _FACULTY_ID_FPIT;
                    private string _ADDRESS_FPTI;
                    private string _WEBSITE_FPTI;
                    private string _MOBILE_NO_FPTI;
                    private string _MANAGER_NAME_FPTI;
                    private string _EMAIL_ID_FPTI;
                    private int _DISCIPLINE_FPTI;
                    private int _LEVEL_FPTI;
                    private DateTime _DATE_OF_LECTURE_FPTI;

                    public int TP_FPTI_ID
                    {
                        get { return _TP_FPTI_ID; }
                        set { _TP_FPTI_ID = value; }
                    }
                    public int COMPANY_NAME_FPTI
                    {
                        get { return _COMPANY_NAME_FPTI; }
                        set { _COMPANY_NAME_FPTI = value; }
                    }
                    public int SECTOR_NAME_FPTI
                    {
                        get { return _SECTOR_NAME_FPTI; }
                        set { _SECTOR_NAME_FPTI = value; }
                    }
                    public string INCORPORATION_STATUS_FPTI
                    {
                        get { return _INCORPORATION_STATUS_FPTI; }
                        set { _INCORPORATION_STATUS_FPTI = value; }
                    }

                    public int FACULTY_ID_FPIT
                    {
                        get { return _FACULTY_ID_FPIT; }
                        set { _FACULTY_ID_FPIT = value; }
                    }

                    public string ADDRESS_FPTI
                    {
                        get { return _ADDRESS_FPTI; }
                        set { _ADDRESS_FPTI = value; }
                    }
                    public string WEBSITE_FPTI
                    {
                        get { return _WEBSITE_FPTI; }
                        set { _WEBSITE_FPTI = value; }
                    }
                    public string MOBILE_NO_FPTI
                    {
                        get { return _MOBILE_NO_FPTI; }
                        set { _MOBILE_NO_FPTI = value; }
                    }
                    public string MANAGER_NAME_FPTI
                    {
                        get { return _MANAGER_NAME_FPTI; }
                        set { _MANAGER_NAME_FPTI = value; }
                    }
                    public string EMAIL_ID_FPTI
                    {
                        get { return _EMAIL_ID_FPTI; }
                        set { _EMAIL_ID_FPTI = value; }
                    }
                    public int DISCIPLINE_FPTI
                    {
                        get { return _DISCIPLINE_FPTI; }
                        set { _DISCIPLINE_FPTI = value; }
                    }
                    public int LEVEL_FPTI
                    {
                        get { return _LEVEL_FPTI; }
                        set { _LEVEL_FPTI = value; }
                    }
                    public DateTime DATE_OF_LECTURE_FPTI
                    {
                        get { return _DATE_OF_LECTURE_FPTI; }
                        set { _DATE_OF_LECTURE_FPTI = value; }
                    }

                    #endregion

                    #region Tab 8
                    private int _TP_FOOI_ID;
                    private int _COMPANY_NAME_FOOI;
                    private int _SECTOR_NAME_FOOI;
                    private string _INCORPORATION_STATUS_FOOI;
                    private string _TYPE_OF_BOARD_FOOT;
                    private int _FACULTY_ID_FOOT;
                    private string _ADDRESS_FOOI;
                    private string _WEBSITE_FOOI;
                    private string _MOBILE_NO_FOOI;
                    private string _MANAGER_NAME_FOOI;
                    private string _EMAIL_ID_FOOI;
                    private int _DISCIPLINE_FOOI;
                    private int _LEVEL_FOOI;
                    private int _MEMBER_FOOI;

                    public int TP_FOOI_ID
                    {
                        get { return _TP_FOOI_ID; }
                        set { _TP_FOOI_ID = value; }
                    }
                    public int COMPANY_NAME_FOOI
                    {
                        get { return _COMPANY_NAME_FOOI; }
                        set { _COMPANY_NAME_FOOI = value; }
                    }
                    public int SECTOR_NAME_FOOI
                    {
                        get { return _SECTOR_NAME_FOOI; }
                        set { _SECTOR_NAME_FOOI = value; }
                    }
                    public string INCORPORATION_STATUS_FOOI
                    {
                        get { return _INCORPORATION_STATUS_FOOI; }
                        set { _INCORPORATION_STATUS_FOOI = value; }
                    }
                    public string TYPE_OF_BOARD_FOOT
                    {
                        get { return _TYPE_OF_BOARD_FOOT; }
                        set { _TYPE_OF_BOARD_FOOT = value; }
                    }

                    public int FACULTY_ID_FOOT
                    {
                        get { return _FACULTY_ID_FOOT; }
                        set { _FACULTY_ID_FOOT = value; }
                    }

                    public string ADDRESS_FOOI
                    {
                        get { return _ADDRESS_FOOI; }
                        set { _ADDRESS_FOOI = value; }
                    }
                    public string WEBSITE_FOOI
                    {
                        get { return _WEBSITE_FOOI; }
                        set { _WEBSITE_FOOI = value; }
                    }
                    public string MOBILE_NO_FOOI
                    {
                        get { return _MOBILE_NO_FOOI; }
                        set { _MOBILE_NO_FOOI = value; }
                    }
                    public string MANAGER_NAME_FOOI
                    {
                        get { return _MANAGER_NAME_FOOI; }
                        set { _MANAGER_NAME_FOOI = value; }
                    }
                    public string EMAIL_ID_FOOI
                    {
                        get { return _EMAIL_ID_FOOI; }
                        set { _EMAIL_ID_FOOI = value; }
                    }
                    public int DISCIPLINE_FOOI
                    {
                        get { return _DISCIPLINE_FOOI; }
                        set { _DISCIPLINE_FOOI = value; }
                    }
                    public int LEVEL_FOOI
                    {
                        get { return _LEVEL_FOOI; }
                        set { _LEVEL_FOOI = value; }
                    }
                    public int MEMBER_FOOI
                    {
                        get { return _MEMBER_FOOI; }
                        set { _MEMBER_FOOI = value; }
                    }


                    #endregion

                    #region Tab 9
                    private int _TP_EPAI_ID;
                    private int _COMPANY_NAME_EPAI;
                    private int _SECTOR_NAME_EPAI;
                    private string _INCORPORATION_STATUS_EPAI;
                    private int _FACULTY_ID_EPAI;
                    private int _DISCIPLINE_EPAI;
                    private int _LEVEL_EPAI;
                    private string _PROGRAM_NAME_EPAI;
                    private DateTime _FROM_DATE_EPAI;
                    private DateTime _TO_DATE_EPAI;
                    private string _NO_OF_EXECUTIVE_ATTEND_COURSES;

                    public int TP_EPAI_ID
                    {
                        get { return _TP_EPAI_ID; }
                        set { _TP_EPAI_ID = value; }
                    }
                    public int COMPANY_NAME_EPAI
                    {
                        get { return _COMPANY_NAME_EPAI; }
                        set { _COMPANY_NAME_EPAI = value; }
                    }
                    public int SECTOR_NAME_EPAI
                    {
                        get { return _SECTOR_NAME_EPAI; }
                        set { _SECTOR_NAME_EPAI = value; }
                    }
                    public string INCORPORATION_STATUS_EPAI
                    {
                        get { return _INCORPORATION_STATUS_EPAI; }
                        set { _INCORPORATION_STATUS_EPAI = value; }
                    }

                    public int FACULTY_ID_EPAI
                    {
                        get { return _FACULTY_ID_EPAI; }
                        set { _FACULTY_ID_EPAI = value; }
                    }
                    public int DISCIPLINE_EPAI
                    {
                        get { return _DISCIPLINE_EPAI; }
                        set { _DISCIPLINE_EPAI = value; }
                    }
                    public int LEVEL_EPAI
                    {
                        get { return _LEVEL_EPAI; }
                        set { _LEVEL_EPAI = value; }
                    }
                    public string PROGRAM_NAME_EPAI
                    {
                        get { return _PROGRAM_NAME_EPAI; }
                        set { _PROGRAM_NAME_EPAI = value; }
                    }
                    public DateTime FROM_DATE_EPAI
                    {
                        get { return _FROM_DATE_EPAI; }
                        set { _FROM_DATE_EPAI = value; }
                    }
                    public DateTime TO_DATE_EPAI
                    {
                        get { return _TO_DATE_EPAI; }
                        set { _TO_DATE_EPAI = value; }
                    }
                    public string NO_OF_EXECUTIVE_ATTEND_COURSES
                    {
                        get { return _NO_OF_EXECUTIVE_ATTEND_COURSES; }
                        set { _NO_OF_EXECUTIVE_ATTEND_COURSES = value; }
                    }



                    #endregion

                    #region Tab 10
                    private int _TP_FTI_ID;
                    private int _COMPANY_NAME_FTI;
                    private int _SECTOR_NAME_FTI;
                    private string _INCORPORATION_STATUS_FTI;
                    private int _FACULTY_ID_FTI;
                    private int _DISCIPLINE_FTI;
                    private int _LEVEL_FTI;
                    private DateTime _FROM_DATE_FTI;
                    private DateTime _TO_DATE_FTI;

                    public int TP_FTI_ID
                    {
                        get { return _TP_FTI_ID; }
                        set { _TP_FTI_ID = value; }
                    }
                    public int COMPANY_NAME_FTI
                    {
                        get { return _COMPANY_NAME_FTI; }
                        set { _COMPANY_NAME_FTI = value; }
                    }
                    public int SECTOR_NAME_FTI
                    {
                        get { return _SECTOR_NAME_FTI; }
                        set { _SECTOR_NAME_FTI = value; }
                    }
                    public string INCORPORATION_STATUS_FTI
                    {
                        get { return _INCORPORATION_STATUS_FTI; }
                        set { _INCORPORATION_STATUS_FTI = value; }
                    }

                    public int FACULTY_ID_FTI
                    {
                        get { return _FACULTY_ID_FTI; }
                        set { _FACULTY_ID_FTI = value; }
                    }
                    public int DISCIPLINE_FTI
                    {
                        get { return _DISCIPLINE_FTI; }
                        set { _DISCIPLINE_FTI = value; }
                    }
                    public int LEVEL_FTI
                    {
                        get { return _LEVEL_FTI; }
                        set { _LEVEL_FTI = value; }
                    }
                    public DateTime FROM_DATE_FTI
                    {
                        get { return _FROM_DATE_FTI; }
                        set { _FROM_DATE_FTI = value; }
                    }
                    public DateTime TO_DATE_FTI
                    {
                        get { return _TO_DATE_FTI; }
                        set { _TO_DATE_FTI = value; }
                    }




                    #endregion

                    #region Tab 11
                    private int _FPP_ID;
                    private int _COMPANY_NAME_FPP;
                    private int _SECTOR_NAME_FPP;
                    private string _INCORPORATION_STATUS_FPP;
                    private int _FACULTY_ID_FPP;
                    private int _DISCIPLINE_FPP;
                    private int _LEVEL_FPP;
                    private DateTime _PATENT_ADOPTION_DATE_FPP;
                    private int _PATENT_NO_FPP;
                    private string _GRANTED_FPP;
                    private string _PATENT_OWNER_EPP;
                    private string _YEAR_EPP;


                    public int FPP_ID
                    {
                        get { return _FPP_ID; }
                        set { _FPP_ID = value; }
                    }
                    public int COMPANY_NAME_FPP
                    {
                        get { return _COMPANY_NAME_FPP; }
                        set { _COMPANY_NAME_FPP = value; }
                    }
                    public int SECTOR_NAME_FPP
                    {
                        get { return _SECTOR_NAME_FPP; }
                        set { _SECTOR_NAME_FPP = value; }
                    }
                    public string INCORPORATION_STATUS_FPP
                    {
                        get { return _INCORPORATION_STATUS_FPP; }
                        set { _INCORPORATION_STATUS_FPP = value; }
                    }

                    public int FACULTY_ID_FPP
                    {
                        get { return _FACULTY_ID_FPP; }
                        set { _FACULTY_ID_FPP = value; }
                    }
                    public int DISCIPLINE_FPP
                    {
                        get { return _DISCIPLINE_FPP; }
                        set { _DISCIPLINE_FPP = value; }
                    }
                    public int LEVEL_FPP
                    {
                        get { return _LEVEL_FPP; }
                        set { _LEVEL_FPP = value; }
                    }
                    public DateTime PATENT_ADOPTION_DATE_FPP
                    {
                        get { return _PATENT_ADOPTION_DATE_FPP; }
                        set { _PATENT_ADOPTION_DATE_FPP = value; }
                    }
                    public int PATENT_NO_FPP
                    {
                        get { return _PATENT_NO_FPP; }
                        set { _PATENT_NO_FPP = value; }
                    }
                    public string GRANTED_FPP
                    {
                        get { return _GRANTED_FPP; }
                        set { _GRANTED_FPP = value; }
                    }
                    public string PATENT_OWNER_EPP
                    {
                        get { return _PATENT_OWNER_EPP; }
                        set { _PATENT_OWNER_EPP = value; }
                    }
                    public string YEAR_EPP
                    {
                        get { return _YEAR_EPP; }
                        set { _YEAR_EPP = value; }
                    }

                    #endregion

                    #region Tab 12
                    private int _PAIF_ID;
                    private int _COMPANY_NAME_PAIF;
                    private int _SECTOR_NAME_PAIF;
                    private string _INCORPORATION_STATUS_PAIF;
                    private int _FACULTY_ID_PAIF;
                    private int _DISCIPLINE_PAIF;
                    private int _LEVEL_PAIF;
                    private DateTime _PRESENTED_DATE_PAIF;
                    private string _PAPER_TITLE_PAIF;
                    private string _ASSIGNMENT_TYPE_PAIF;


                    public int PAIF_ID
                    {
                        get { return _PAIF_ID; }
                        set { _PAIF_ID = value; }
                    }
                    public int COMPANY_NAME_PAIF
                    {
                        get { return _COMPANY_NAME_PAIF; }
                        set { _COMPANY_NAME_PAIF = value; }
                    }
                    public int SECTOR_NAME_PAIF
                    {
                        get { return _SECTOR_NAME_PAIF; }
                        set { _SECTOR_NAME_PAIF = value; }
                    }
                    public string INCORPORATION_STATUS_PAIF
                    {
                        get { return _INCORPORATION_STATUS_PAIF; }
                        set { _INCORPORATION_STATUS_PAIF = value; }
                    }

                    public int FACULTY_ID_PAIF
                    {
                        get { return _FACULTY_ID_PAIF; }
                        set { _FACULTY_ID_PAIF = value; }
                    }
                    public int DISCIPLINE_PAIF
                    {
                        get { return _DISCIPLINE_PAIF; }
                        set { _DISCIPLINE_PAIF = value; }
                    }
                    public int LEVEL_PAIF
                    {
                        get { return _LEVEL_PAIF; }
                        set { _LEVEL_PAIF = value; }
                    }
                    public DateTime PRESENTED_DATE_PAIF
                    {
                        get { return _PRESENTED_DATE_PAIF; }
                        set { _PRESENTED_DATE_PAIF = value; }
                    }

                    public string PAPER_TITLE_PAIF
                    {
                        get { return _PAPER_TITLE_PAIF; }
                        set { _PAPER_TITLE_PAIF = value; }
                    }
                    public string ASSIGNMENT_TYPE_PAIF
                    {
                        get { return _ASSIGNMENT_TYPE_PAIF; }
                        set { _ASSIGNMENT_TYPE_PAIF = value; }
                    }


                    #endregion

                    #region Tab 13
                    private int _SEV_ID;
                    private int _COMPANY_NAME_SEV;
                    private int _SECTOR_NAME_SEV;
                    private string _INCORPORATION_STATUS_SEV;
                    private string _TYPE_OF_SERVICES_SEV;
                    private string _TITLE_OF_SERVICES_SEV;
                    private string _YEAR_SEV;
                    private int _FACULTY_ID_SEV;
                    private int _DISCIPLINE_SEV;
                    private int _LEVEL_SEV;
                    private DateTime _START_DATE_SEV;
                    private DateTime _FINISH_DATE_SEV;
                    private string _FEE_RECEIVED_FROM_INDUSTRY_SEV;


                    public int SEV_ID
                    {
                        get { return _SEV_ID; }
                        set { _SEV_ID = value; }
                    }
                    public int COMPANY_NAME_SEV
                    {
                        get { return _COMPANY_NAME_SEV; }
                        set { _COMPANY_NAME_SEV = value; }
                    }
                    public int SECTOR_NAME_SEV
                    {
                        get { return _SECTOR_NAME_SEV; }
                        set { _SECTOR_NAME_SEV = value; }
                    }
                    public string INCORPORATION_STATUS_SEV
                    {
                        get { return _INCORPORATION_STATUS_SEV; }
                        set { _INCORPORATION_STATUS_SEV = value; }
                    }

                    public string TYPE_OF_SERVICES_SEV
                    {
                        get { return _TYPE_OF_SERVICES_SEV; }
                        set { _TYPE_OF_SERVICES_SEV = value; }
                    }
                    public string TITLE_OF_SERVICES_SEV
                    {
                        get { return _TITLE_OF_SERVICES_SEV; }
                        set { _TITLE_OF_SERVICES_SEV = value; }
                    }
                    public string YEAR_SEV
                    {
                        get { return _YEAR_SEV; }
                        set { _YEAR_SEV = value; }
                    }

                    public int FACULTY_ID_SEV
                    {
                        get { return _FACULTY_ID_SEV; }
                        set { _FACULTY_ID_SEV = value; }
                    }
                    public int DISCIPLINE_SEV
                    {
                        get { return _DISCIPLINE_SEV; }
                        set { _DISCIPLINE_SEV = value; }
                    }
                    public int LEVEL_SEV
                    {
                        get { return _LEVEL_SEV; }
                        set { _LEVEL_SEV = value; }
                    }
                    public DateTime START_DATE_SEV
                    {
                        get { return _START_DATE_SEV; }
                        set { _START_DATE_SEV = value; }
                    }
                    public DateTime FINISH_DATE_SEV
                    {
                        get { return _FINISH_DATE_SEV; }
                        set { _FINISH_DATE_SEV = value; }
                    }

                    public string FEE_RECEIVED_FROM_INDUSTRY_SEV
                    {
                        get { return _FEE_RECEIVED_FROM_INDUSTRY_SEV; }
                        set { _FEE_RECEIVED_FROM_INDUSTRY_SEV = value; }
                    }



                    #endregion

                    #region Tab 14
                    private int _SSE_ID;
                    private string _STUDENT_FIRST_NAME_SSE;
                    private string _STUDENT_LAST_NAME_SSE;
                    private string _TYPE_OF_SELF_EMPLOYMENT_SSE;
                    private int _DISCIPLINE_SSE;
                    private int _LEVEL_SSE;
                    private string _YEAR_SSE;
                    private int _COMPANY_NO_SSE;
                    private string _ADDRESS_SSE;
                    private string _EMAIL_ID_SSE;
                    private string _MOBILE_NO_SSE;
                    private string _WEBSITE_SSE;


                    public int SSE_ID
                    {
                        get { return _SSE_ID; }
                        set { _SSE_ID = value; }
                    }

                    public string STUDENT_FIRST_NAME_SSE
                    {
                        get { return _STUDENT_FIRST_NAME_SSE; }
                        set { _STUDENT_FIRST_NAME_SSE = value; }
                    }

                    public string STUDENT_LAST_NAME_SSE
                    {
                        get { return _STUDENT_LAST_NAME_SSE; }
                        set { _STUDENT_LAST_NAME_SSE = value; }
                    }
                    public string TYPE_OF_SELF_EMPLOYMENT_SSE
                    {
                        get { return _TYPE_OF_SELF_EMPLOYMENT_SSE; }
                        set { _TYPE_OF_SELF_EMPLOYMENT_SSE = value; }
                    }
                    public int DISCIPLINE_SSE
                    {
                        get { return _DISCIPLINE_SSE; }
                        set { _DISCIPLINE_SSE = value; }
                    }
                    public int LEVEL_SSE
                    {
                        get { return _LEVEL_SSE; }
                        set { _LEVEL_SSE = value; }
                    }
                    public string YEAR_SSE
                    {
                        get { return _YEAR_SSE; }
                        set { _YEAR_SSE = value; }
                    }
                    public int COMPANY_NO_SSE
                    {
                        get { return _COMPANY_NO_SSE; }
                        set { _COMPANY_NO_SSE = value; }
                    }


                    public string ADDRESS_SSE
                    {
                        get { return _ADDRESS_SSE; }
                        set { _ADDRESS_SSE = value; }
                    }
                    public string EMAIL_ID_SSE
                    {
                        get { return _EMAIL_ID_SSE; }
                        set { _EMAIL_ID_SSE = value; }
                    }
                    public string MOBILE_NO_SSE
                    {
                        get { return _MOBILE_NO_SSE; }
                        set { _MOBILE_NO_SSE = value; }
                    }
                    public string WEBSITE_SSE
                    {
                        get { return _WEBSITE_SSE; }
                        set { _WEBSITE_SSE = value; }
                    }



                    #endregion

                    #endregion

                #region  TP Data Collection For Placement Drive

                    private int _Degree;
                    private int _Branch;
                    private int _Semester;
                    private decimal _CGPA;
                    private int _NoOfAttempt;
                    private int _Attempt;
                    private int _ADMBatch;

                    public int Degree
                    {
                        get { return _Degree; }
                        set { _Degree = value; }
                    }

                    public int Branch
                    {
                        get { return _Branch; }
                        set { _Branch = value; }
                    }

                    public int Semester
                    {
                        get { return _Semester; }
                        set { _Semester = value; }
                    }

                    public decimal CGPA
                    {
                        get { return _CGPA; }
                        set { _CGPA = value; }
                    }

                    public int NoOfAttempt
                    {
                        get { return _NoOfAttempt; }
                        set { _NoOfAttempt = value; }
                    }
                    public int Attempt
                    {
                        get { return _Attempt; }
                        set { _Attempt = value; }
                    }

                    public int ADMBatch
                    {
                        get { return _ADMBatch; }
                        set { _ADMBatch = value; }
                    }

                #endregion

                #region  TP Data Collection For Placement Drive
                    private int _CAStatus;
                    private int _CADegree;
                    private int _CASemester;
                    private int _CAUA_NO;
                    private int _CAID;

                    public int CAStatus
                    {
                        get { return _CAStatus; }
                        set { _CAStatus = value; }
                    }
                public int CADegree
                {
                    get{return _CADegree;}
                    set { _CADegree = value; }
                }
                public int CASemester
                {
                    get { return _CASemester; }
                    set { _CASemester = value; }
                }
                public int CAUA_NO
                {
                    get { return _CAUA_NO; }
                    set { _CAUA_NO = value; }
                }
                public int CAID
                {
                    get { return _CAID; }
                    set { _CAID = value; }
                }
                #endregion
            }

        }
    }
}
