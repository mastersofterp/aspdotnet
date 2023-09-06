using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
  public class ExamQuestionPaperModel
   {

       
            public int QuestionPaperId { get; set; }
            public string QuestionPaperName { get; set; }
            public int SchemeSubjectId { get; set; }
            public decimal TotalMaxMarks { get; set; }
            public int SessionId { get; set; }
            public int ExamPatternMappingId { get; set; }
            public string Description { get; set; }
            public string IsLock { get; set; }
            public string ActiveStatus { get; set; }
            public int SubjectId { get; set; }
            public string SubjectName { get; set; }
            public int QuestionPatternId { get; set; }
            public string GroupIds { get; set; }
            public string ExamName { get; set; }
            public string SchemeMappingName { get; set; }
            public string Status { get; set; }
            public int srno { get; set; }
            public string FilePath { get; set; }
            public string SectionName { get; set; }
            public int SectionId { get; set; }
            public int QuestionRow { get; set; }
            public int GroupId { get; set; }
            public decimal QuePatMaxMarks { get; set; }
            public string MaxMarks { get; set; }
            public int SubCOId { get; set; }
            public string SubCOName { get; set; }
            public int bloomId { get; set; }
            public string BloomCategoryName { get; set; }
            public int PaperQuestionsId { get; set; }
            public string QuestionNo { get; set; }
            public int BloomCategoryId { get; set; }
            public int LevelId { get; set; }
            public string IsActive { get; set; }
            public int QuestionsCOId { get; set; }
            public int SequenceNo { get; set; }
            public int MarksEnteredCount { get; set; }
            public string QuestionPatternName { get; set; }
        }
        public class TotalQuestion
        {
            public int TotalNoQuestions { get; set; }
            public int SolveNoQuestions { get; set; }
        }
        
        public class ExamPaperQuestionsDataModel
        {
            public int tempqueNo { get; set; }
            public int tempGroupNo { get; set; }
            public string QuestionNo { get; set; }
        }

        public class QuestionsCOMappingDatatable
        {
            public int tempqueNo { get; set; }
            public int tempGroupNo { get; set; }
            public int Weightage { get; set; }

        }

        
}
