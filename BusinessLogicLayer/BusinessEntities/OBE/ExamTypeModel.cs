using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IITMS.NITPRM.BusinessLayer.BusinessEntities
{
    public class vmQuestionWithStudent
    {
        public List<Question> Question { get; set; }
        public List<Students> Students { get; set; }
        public List<StudentsMult> StudentsMult { get; set; }
        public List<SubMaxMarks> SubMaxMarks { get; set; }
        public List<MarkEntryStatusCodeModel> MarkEntryStatusCode { get; set; }
    }
    public class ExamTypeModel
    {
        public int ExamTypeId { get; set; }
        public string ExamTypeName { get; set; }
        public string IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string IPAddress { get; set; }
        public int OrganizationId { get; set; }
        public string Status { get; set; }
        public string MacAdress { get; set; }
        public string convertSplitInfoToJson { get; set; }
        public int PaperQuestionsId { get; set; }
        public bool IsLOCK { get; set; }
        public bool ActiveStatus { get; set; }
        ///////////////////////  added by sarang ////////////////////////////

        public int SubjectId { get; set; }
        public int SectionId { get; set; }
        public int SessionId { get; set; }
        public int SchemeSubjectId { get; set; }
        public int QuestionPaperId { get; set; }
        public string SubjectName { get; set; }
        public string SectionName { get; set; }
    }
    public class MarkEntryStatusCodeModel
    {
        public int StatusCodeId { get; set; }
        public string StatusCodeName { get; set; }
        public int StatusCode { get; set; }
        public string StatusCodeMeaning { get; set; }
        public string ActiveStatus { get; set; }

    }
    public class MarksEntryModel
    {
        public string SchemeMappingName { get; set; }
        public int SchemeSubjectId { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int QuestionPaperId { get; set; }
        public int SessionId { get; set; }
        public string ObtainedMarks { get; set; }
        public string PaperQuestionsId { get; set; }
        public string CourseRegistrationDetailsId { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; }

    }

    public class CheckLockStatus
    {
        public int PaperQuestionsId { get; set; }
        public bool IsLOCK { get; set; }
        public bool ActiveStatus { get; set; }
    }
    public class Question
    {
        public int PaperQuestionsId { get; set; }
        public string QuestionWithMarks { get; set; }
        public string MaxMarks { get; set; }
    }

    public class Students
    {
        public int StudentId { get; set; }
        public string EnrollmentNo { get; set; }
        public int CourseRegistrationDetailsId { get; set; }
        public string StudentName { get; set; }
        public int ExamPatternMappingId { get; set; }
        public string IsLock { get; set; }
        public string TOTMarks { get; set; }
        public string ActiveStatus { get; set; }

    }

    public class StudentsMult
    {
        public int PaperQuestionsId { get; set; }
        public String StudentId { get; set; }
        public string Stud_Sub_ToT_marks { get; set; }
        public string Que_marks { get; set; }
        public int CourseRegistrationDetailsId { get; set; }

    }
    public class SubMaxMarks
    {
        public string MaxMarks { get; set; }

    }
    public class MarkEntryListDataModel
    {

        public int CourseRegistrationDetailsId { get; set; }
        public int ExamPatternMappingId { get; set; }
        public int PaperQuestionsId { get; set; }
        public string quemarks { get; set; }
        public string IsLock { get; set; }
        public string ActiveStatus { get; set; }


    }
    public class TotMarksListDataModel
    {
        public int CourseRegistrationDetailsId { get; set; }
        public int ExamPatternMappingId { get; set; }
        public string MarksObtained { get; set; }

    }
}
