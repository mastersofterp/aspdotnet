using System;
using System.Collections.Generic;
using System.Text;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
        public class TPStudent
        {
            #region Private Members
                private string _regno = string.Empty;
                private int _idno = 0;
                private string _studName = string.Empty;
                private DateTime _dob = DateTime.MinValue;                
                private char _sex = ' ';
                private string _lAddress = string.Empty;
                private int _lcity = 0;
                private string _lPincode = string.Empty;
                private string _contact_no = string.Empty;
                private string _mobile = string.Empty;
                private string _collegeCode = string.Empty;
                private int _bloodGroupNo = 0;
                private decimal _height = 0.0m;
                private decimal _weight = 0;
                private string _emailID = string.Empty;
                private string _visano = string.Empty;
                private string _passportNo = string.Empty;

            //required in alumni form    
            private int _CompId = 0;
                private int _EnrollmentNo = 0;
                private int _aluNo = 0;
                private string _compcontact_no = string.Empty;
                private string _desig = string.Empty;
                private int _BranchNo = 0;
                private int _PassYear = 0;


                // TP Admin Lock Unlock tab
                private int _EXAM_LOCK_UNLOCK = 0;
                private int _WORK_EXP_LOCK_UNLOCK = 0;
                private int _TECH_SKIL_LOCK_UNLOCK = 0;
                private int _PROJECT_LOCK_UNLOCK = 0;
                private int _CERTIFICATION_LOCK_UNLOCK = 0;
                private int _LANGUAGE_LOCK_UNLOCK = 0;
                private int _AWARD_LOCK_UNLOCK = 0;
                private int _COMPETITION_LOCK_UNLOCK = 0;
                private int _TRAINING_LOCK_UNLOCK = 0;
                private int _TEST_SCORE_LOCK_UNLOCK = 0;
                private int _BUILD_RESUME_LOCK_UNLOCK = 0;

            #endregion

            #region Public Properties
                public string RegNo
                {
                    get { return _regno; }
                    set { _regno = value; }
                }
                public int IdNo
                {
                    get { return _idno; }
                    set { _idno = value; }
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
                public string LAddress
                {
                    get { return _lAddress; }
                    set { _lAddress = value; }
                }

                public int LCity
                {
                    get { return _lcity; }
                    set { _lcity = value; }
                }
                public string LPinCode
                {
                    get { return _lPincode; }
                    set { _lPincode = value; }
                }
                public string ContactNo
                {
                    get { return _contact_no; }
                    set { _contact_no = value; }
                }

                public string Mobile
                {
                    get { return _mobile; }
                    set { _mobile = value; }
                }
                public string CollegeCode
                {
                    get { return _collegeCode; }
                    set { _collegeCode = value; }
                }
                public int BloodGroupNo
                {
                    get { return _bloodGroupNo; }
                    set { _bloodGroupNo = value; }
                }

                public decimal Weight
                {
                    get { return _weight; }
                    set { _weight = value; }
                }
                
                public decimal Height
                {
                    get { return _height; }
                    set { _height = value; }
                }
                public string EmailID
                {
                    get { return _emailID; }
                    set { _emailID = value; }
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

                public int CompId
                {
                    get { return _CompId ; }
                    set { _CompId  = value; }
                }
                public int EnrollmentNo
                {
                    get { return _EnrollmentNo ; }
                    set { _EnrollmentNo  = value; }
                }
                public int AluNo
                {
                    get { return _aluNo; }
                    set { _aluNo = value; }
                }
                public string CompContact
                {
                    get { return _compcontact_no; }
                    set { _compcontact_no = value; }
                }
                public string Desig
                {
                    get { return _desig; }
                    set { _desig = value; }
                }
                public int BranchNo
                {
                    get { return _BranchNo; }
                    set { _BranchNo = value; }
                }
                public int PassYear
                {
                    get { return _PassYear; }
                    set { _PassYear = value; }
                }

                //Admin Lock Unlock
                public int EXAM_LOCK_UNLOCK
                {
                    get { return _EXAM_LOCK_UNLOCK; }
                    set { _EXAM_LOCK_UNLOCK = value; }
                }
                public int WORK_EXP_LOCK_UNLOCK
                {
                    get { return _WORK_EXP_LOCK_UNLOCK; }
                    set { _WORK_EXP_LOCK_UNLOCK = value; }
                }
                public int TECH_SKIL_LOCK_UNLOCK
                {
                    get { return _TECH_SKIL_LOCK_UNLOCK; }
                    set { _TECH_SKIL_LOCK_UNLOCK = value; }
                }
                public int PROJECT_LOCK_UNLOCK
                {
                    get { return _PROJECT_LOCK_UNLOCK; }
                    set { _PROJECT_LOCK_UNLOCK = value; }
                }
                public int CERTIFICATION_LOCK_UNLOCK
                {
                    get { return _CERTIFICATION_LOCK_UNLOCK; }
                    set { _CERTIFICATION_LOCK_UNLOCK = value; }
                }
                public int LANGUAGE_LOCK_UNLOCK
                {
                    get { return _LANGUAGE_LOCK_UNLOCK; }
                    set { _LANGUAGE_LOCK_UNLOCK = value; }
                }
                public int AWARD_LOCK_UNLOCK
                {
                    get { return _AWARD_LOCK_UNLOCK; }
                    set { _AWARD_LOCK_UNLOCK = value; }
                }
                public int COMPETITION_LOCK_UNLOCK
                {
                    get { return _COMPETITION_LOCK_UNLOCK; }
                    set { _COMPETITION_LOCK_UNLOCK = value; }
                }
                public int TRAINING_LOCK_UNLOCK
                {
                    get { return _TRAINING_LOCK_UNLOCK; }
                    set { _TRAINING_LOCK_UNLOCK = value; }
                }
                public int TEST_SCORE_LOCK_UNLOCK
                {
                    get { return _TEST_SCORE_LOCK_UNLOCK; }
                    set { _TEST_SCORE_LOCK_UNLOCK = value; }
                }
                public int BUILD_RESUME_LOCK_UNLOCK
                {
                    get { return _BUILD_RESUME_LOCK_UNLOCK; }
                    set { _BUILD_RESUME_LOCK_UNLOCK = value; }
                }
            #endregion
        }

      }
    }
}
