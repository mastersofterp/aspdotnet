using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
   public  class QuestionPatternMasterModel
    {

        public int QuestionPatternId { get; set; }
        public string QuestionPatternName { get; set; }
        public int TotalNoQuestions { get; set; }
        public int SolveNoQuestions { get; set; }
        public string ActiveStatus { get; set; }
        public int CollegeDepartmentId { get; set; }
        public int SchemeId { get; set; }
        public int SchemeSubjectId { get; set; }
    }
}
