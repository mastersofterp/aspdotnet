//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS ENTITIES [EXAM CREATION & EXAM CONFIG]                     
// CREATION DATE : 22-MAY-2009                                                          
// CREATED BY    : SANJAY RATNAPARKHI                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class Exam
            {
                #region Private Members for EXAM_NAME TABLE
                private string _examNo = string.Empty;
                private int session_Plan;
                private int _examid = 0;
                private int _degreeno = 0;
                private int _branchno = 0;
                private int _schemeno = 0;
                public int _subExamNo = 0;
                public int _PATTERNNO = 0;
                private int _CourseType = 0;
                public decimal _MAXMARKS;


                private string _examinationName = string.Empty;
                private string _fldName = string.Empty;
                private string _formula = string.Empty;
                private string _collegeCode = string.Empty;

                private int _collegeno = 0;
                private int _department;
                private int _college_id;


                #endregion

                #region Public Property Fields for EXAM_NAME TABLE


                public int Collegeno
                {
                    get { return _collegeno; }
                    set { _collegeno = value; }
                }
                public string ExamNo
                {
                    get { return _examNo; }
                    set { _examNo = value; }
                }

                public string ExaminationName
                {
                    get { return _examinationName; }
                    set { _examinationName = value; }
                }


                public string FldName
                {
                    get { return _fldName; }
                    set { _fldName = value; }
                }

                public string Formula
                {
                    get { return _formula; }
                    set { _formula = value; }
                }

                public string CollegeCode
                {
                    get { return _collegeCode; }
                    set { _collegeCode = value; }
                }
                public int BranchNo
                {
                    get { return _branchno; }
                    set { _branchno = value; }
                }

                public int SchemeNo
                {
                    get { return _schemeno; }
                    set { _schemeno = value; }
                }
                public int DegreeNo
                {
                    get { return _degreeno; }
                    set { _degreeno = value; }
                }
                public int ExamID
                {
                    get { return _examid; }
                    set { _examid = value; }
                }
                #endregion

                //EXAM CONFIG TABLE FIELDS SET
                #region Private Members for EXAM CONFIG TABLE
                private int _econId = 0;
                private string _configExamName = string.Empty;
                private DateTime _fromDate = DateTime.Now;
                private DateTime _toDate = DateTime.Now;
                private int _sessionNo = 0;

                #endregion

                #region Public Property Fields for EXAM CONFIG TABLE
                public int EconId
                {
                    get { return _econId; }
                    set { _econId = value; }
                }

                public string ConfigExamName
                {
                    get { return _configExamName; }
                    set { _configExamName = value; }
                }

                public DateTime FromDate
                {
                    get { return _fromDate; }
                    set { _fromDate = value; }
                }

                public DateTime ToDate
                {
                    get { return _toDate; }
                    set { _toDate = value; }
                }

                public int SessionNo
                {
                    get { return _sessionNo; }
                    set { _sessionNo = value; }
                }

                #endregion

                #region private Memebers for  Exam Day
                private int _exdtno = 0;
                private int _dayno = 0;
                private int _exam_tt_type = 0;
                private DateTime _examdate = DateTime.Now;
                private int _slot = 0;
                private int _semesterno = 0;
                private string _dayname = string.Empty;
                #endregion

                #region Public Memebers for  Exam Day
                public int Exdtno
                {
                    get { return _exdtno; }
                    set { _exdtno = value; }
                }
                public int Dayno
                {
                    get { return _dayno; }
                    set { _dayno = value; }
                }
                public int Exam_TT_Type
                {
                    get { return _exam_tt_type; }
                    set { _exam_tt_type = value; }
                }
                public DateTime Examdate
                {
                    get { return _examdate; }
                    set { _examdate = value; }
                }
                public int Slot
                {
                    get { return _slot; }
                    set { _slot = value; }
                }
                public string DayName
                {
                    get { return _dayname; }
                    set { _dayname = value; }
                }
                public int SemesterNo
                {
                    get { return _semesterno; }
                    set { _semesterno = value; }
                }
                #endregion

                #region  Private Members of Exam Invigilator
                private int _shiftno = 0;
                private int _invigilator = 0;
                private int _exminvno = 0;

                #endregion
                #region Public members of Exam Invigilator
                public int Shiftno
                {
                    get { return _shiftno; }
                    set { _shiftno = value; }
                }
                public int Invigilator
                {
                    get { return _invigilator; }
                    set { _invigilator = value; }
                }
                public int EXMINVNO
                {
                    get { return _exminvno; }
                    set { _exminvno = value; }
                }

                #endregion

                #region Private members for Answere paper Records

                private int _sessionno = 0;

                public int Sessionno
                {
                    get { return _sessionno; }
                    set { _sessionno = value; }
                }
                private int _dayno1 = 0;

                public int Dayno1
                {
                    get { return _dayno1; }
                    set { _dayno1 = value; }
                }
                private int _slotno = 0;

                public int Slotno
                {
                    get { return _slotno; }
                    set { _slotno = value; }
                }
                private int _roomno = 0;

                public int Roomno
                {
                    get { return _roomno; }
                    set { _roomno = value; }
                }
                private string _is_ans_paper_from = string.Empty;

                public string Is_ans_paper_from
                {
                    get { return _is_ans_paper_from; }
                    set { _is_ans_paper_from = value; }
                }
                private string _is_ans_paper_to = string.Empty;

                public string Is_ans_paper_to
                {
                    get { return _is_ans_paper_to; }
                    set { _is_ans_paper_to = value; }
                }
                private int _totIssued = 0;

                public int TotIssued
                {
                    get { return _totIssued; }
                    set { _totIssued = value; }
                }
                private string _ret_ans_paper_from = string.Empty;

                public string Ret_ans_paper_from
                {
                    get { return _ret_ans_paper_from; }
                    set { _ret_ans_paper_from = value; }
                }
                private string _ret_ans_paper_to = string.Empty;

                public string Ret_ans_paper_to
                {
                    get { return _ret_ans_paper_to; }
                    set { _ret_ans_paper_to = value; }
                }
                private int _totReturned = 0;

                public int TotReturned
                {
                    get { return _totReturned; }
                    set { _totReturned = value; }
                }

                private int _courseno = 0;

                public int Courseno
                {
                    get { return _courseno; }
                    set { _courseno = value; }
                }


                #endregion
                private int _Status = 0;
                //private int _blockno = 0;

                //public int Blockno
                //{
                //    get { return _blockno; }
                //    set { _blockno = value; }
                //}

                public int Status
                {
                    get { return _Status; }
                    set { _Status = value; }
                }

                public int PATTERNNO
                {
                    get { return _PATTERNNO; }
                    set { _PATTERNNO = value; }
                }

                public int CourseType
                {
                    get { return _CourseType; }
                    set { _CourseType = value; }
                }

                public decimal MAXMARKS
                {
                    get { return _MAXMARKS; }
                    set { _MAXMARKS = value; }
                }

                public int SubExamNo
                {
                    get { return _subExamNo; }
                    set { _subExamNo = value; }
                }
                public string SubExamname { get; set; }
                public string FieldName { get; set; }
                private string _roomname = string.Empty;
                private int _blockno = 0;

                public string Roomname
                {
                    get { return _roomname; }
                    set { _roomname = value; }
                }

                private int _room_capacity;

                public int RoomCapacity
                {
                    get { return _room_capacity; }
                    set { _room_capacity = value; }
                }

                private int _floorno;

                public int FloorNo
                {
                    get { return _floorno; }
                    set { _floorno = value; }
                }

                private int _invReq;

                public int InvReq
                {
                    get { return _invReq; }
                    set { _invReq = value; }
                }

                public int Blockno
                {
                    get { return _blockno; }
                    set { _blockno = value; }
                }

                public int department
                {
                    get { return _department; }
                    set { _department = value; }
                }

                public int collegeid
                {
                    get { return _college_id; }
                    set { _college_id = value; }
                }
                //ADDED BY SNEHA G.
                public bool ActiveStatus
                {
                    get;
                    set;
                }
                public int collegecode
                {
                    get;
                    set;
                }

                public int OrgId
                {
                    get;
                    set;
                }
                public int Fixed
                {
                    get;
                    set;
                }
                public int sessionPlan
                {
                    get { return session_Plan; }
                    set { session_Plan = value; }
                }
                //Add by Pritish 18052019...
                #region private Memebers for  TeachingPlan Master

                private int _tp_no = 0;
                private int _ua_no = 0;
                private DateTime _date = DateTime.Now;
                private int _sectionno = 0;
                private int _batchno = 0;
                private string _topic_covered = string.Empty;
                private int _unit_no = 0;
                private int _lecture_no = 0;
                private string _remark = string.Empty;
                private string _slotTeaching = string.Empty;

                #endregion

                #region Public Memebers for  TeachingPlan Master

                public int TP_NO
                {
                    get { return _tp_no; }
                    set { _tp_no = value; }
                }

                public string SlotTeaching
                {
                    get { return _slotTeaching; }
                    set { _slotTeaching = value; }
                }
                public int Ua_No
                {
                    get { return _ua_no; }
                    set { _ua_no = value; }
                }
                public DateTime Date
                {
                    get { return _date; }
                    set { _date = value; }
                }
                public int Sectionno
                {
                    get { return _sectionno; }
                    set { _sectionno = value; }
                }
                public int BatchNo
                {
                    get { return _batchno; }
                    set { _batchno = value; }
                }
                public string Topic_Covered
                {
                    get { return _topic_covered; }
                    set { _topic_covered = value; }
                }
                public int UnitNo
                {
                    get { return _unit_no; }
                    set { _unit_no = value; }
                }
                public int Lecture_No
                {
                    get { return _lecture_no; }
                    set { _lecture_no = value; }
                }
                public string Remark
                {
                    get { return _remark; }
                    set { _remark = value; }
                }

                #endregion

                //ADDED BY SNEHA G ON 18/01/2022
                #region Exam Transaction Details

                public int Idno
                {
                    get;
                    set;
                }
                public string Transaction_no
                {
                    get;
                    set;
                }
                public DateTime Transaction_date
                {
                    get;
                    set;
                }
                public string file_name
                {
                    get;
                    set;
                }
                public string file_path
                {
                    get;
                    set;
                }
                public decimal trans_amt
                {
                    get;
                    set;
                }
                public int Approvedby
                {
                    get;
                    set;
                }
                public int Approvedstatus
                {
                    get;
                    set;
                }
                public int Subid
                {
                    get;
                    set;
                }
                public int Sequence
                {
                    get;
                    set;
                }
                //public string _Transaction_no = string.Empty;
                //public DateTime _transaction_date = DateTime.Now;
                //public decimal _trans_amt;
                //public string _file_name = string.Empty;
                //public string _file_path = string.Empty;
                //public int _Approvedby = 0;
                //public int _Approvedstatus = 0;
                #endregion


                //Added By Atharva 		
                private int _ActivityNo;
                private string _Grade;
                private bool _IsActiveStatus;
                private int _CollegeCode;
                private int _ActivityCode;

                public int ActivityCode
                {
                    get { return _ActivityCode; }
                    set { _ActivityCode = value; }
                }

                public int ActivityNo
                {
                    get { return _ActivityNo; }
                    set { _ActivityNo = value; }
                }

                public string Grade
                {
                    get { return _Grade; }
                    set { _Grade = value; }
                }

                public bool IsActive
                {
                    get { return _IsActiveStatus; }
                    set { _IsActiveStatus = value; }
                }
            }
            public class EndSemMark
            {
                //ADDED BY SHAHBAZ AHMAD ON 31-12-2022
                public int IDNO { get; set; }
                public decimal? EXTERMARKS { get; set; }
            }


        }
    }
}
