using System;
using System.Collections;
using System.Text;


namespace IITMS.UAIMS.BusinessLogicLayer.BusinessEntities
{
    public class Grade
    {
        #region Private Member
        private int _gradeType = 0;
        private string _gradeName = string.Empty;
        private string _collegeCode = string.Empty;
        #endregion 

        #region Public Member
        public string CollegeCode
        {
            get { return _collegeCode; }
            set { _collegeCode = value; }
        }
        public string GradeName
        {
            get { return _gradeName; }
            set { _gradeName = value; }
        }
        public int GradeType
        {
            get { return _gradeType; }
            set { _gradeType = value; }
        }
        #endregion

        public CustomStatus AddGrade(Grade objGrade)
        {
            throw new NotImplementedException();
        }
    }
}
