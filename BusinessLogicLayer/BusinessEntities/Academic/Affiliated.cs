using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Academic
{
    public class Affiliated
    {
        #region privatemember

        private string _collegecode = string.Empty;
        private int _uano = 0;
        private string _address = string.Empty;
        private string _pincode = string.Empty;
        private int _district = 0;
        private string _website = string.Empty;
        private string _phoneno = string.Empty;
        private string _altphoneno = string.Empty;
        private string _mobileno = string.Empty;
        private string _altmobileno = string.Empty;
        private string _faxno = string.Empty;
        private string _emailid = string.Empty;
        private string _altemailid = string.Empty;
        private string _authority = string.Empty;
        private string _authorityName = string.Empty;
        private string _contactPersonName = string.Empty;
        private string _contactPersonMob = string.Empty;
        public string _contactPersonEmail = string.Empty;
        private DateTime? _deadline = null;
        // Added by Nikhil V.Lambe on 26/05/2021 for Define Intake
        private string _collegeId = string.Empty;
        private string _branchType = string.Empty;
        private string _level = string.Empty;
        private string _degree = string.Empty;
        private string _branch = string.Empty;
        private string _intake = string.Empty;
        private string _duration = string.Empty;
        private int _degreeExc = 0;
        private int _branchExc = 0;
        private int _branchTypeExc = 0;
        private int _levelExc = 0;
        private int _collIDExc = 0;
        //---------------------------------------------------------------------
        private string _actualIntake = string.Empty;
        private string _exmIntake = string.Empty;
        #endregion
        #region publicmember
        public string CollegeCode
        {
            get { return _collegecode; }
            set { _collegecode = value; }
        }
        public int UaNo
        {
            get { return _uano; }
            set { _uano = value; }
        }
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        public string PinCode
        {
            get { return _pincode; }
            set { _pincode = value; }
        }
        public int District
        {
            get { return _district; }
            set { _district = value; }
        }
        public string Website
        {
            get { return _website; }
            set { _website = value; }
        }
        public string PhoneNo
        {
            get { return _phoneno; }
            set { _phoneno = value; }

        }
        public string Alt_PhoneNo
        {
            get { return _altphoneno; }
            set { _altphoneno = value; }

        }

        public string MobileNo
        {
            get { return _mobileno; }
            set { _mobileno = value; }

        }

        public string Alt_MobileNo
        {
            get { return _altmobileno; }
            set { _altmobileno = value; }

        }
        public string FaxNo
        {
            get { return _faxno; }
            set { _faxno = value; }

        }
        public string EmailId
        {
            get { return _emailid; }
            set { _emailid = value; }

        }
        public string Alt_EmailId
        {
            get { return _altemailid; }
            set { _altemailid = value; }

        }
        public string Authority
        {
            get { return _authority; }
            set { _authority = value; }

        }
        public string Authority_Name
        {
            get { return _authorityName; }
            set { _authorityName = value; }
        }
        public string ContactPersonName
        {
            get { return _contactPersonName; }
            set { _contactPersonName = value; }

        }
        public string ContactPersonMob
        {
            get { return _contactPersonMob; }
            set { _contactPersonMob = value; }

        }

        public string ContactPersonEmail
        {
            get { return _contactPersonEmail; }
            set { _contactPersonEmail = value; }

        }
        public DateTime? Deadline_date
        {
            get { return _deadline; }
            set { _deadline = value; }
        }
        // Added by Nikhil V.Lambe on 26/05/2021 for Define Intake
        public string CollegeId
        {
            get { return _collegeId; }
            set { _collegeId = value; }

        }
        public string BranchType
        {
            get { return _branchType; }
            set { _branchType = value; }

        }
        public string Level
        {
            get { return _level; }
            set { _level = value; }
        }
        public string Degree
        {
            get { return _degree; }
            set { _degree = value; }
        }
        public string Branch
        {
            get { return _branch; }
            set { _branch = value; }
        }
        public string Intake
        {
            get { return _intake; }
            set { _intake = value; }
        }
        public string Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }
        public int DegreeExc
        {
            get { return _degreeExc; }
            set { _degreeExc = value; }
        }
        public int BranchExc
        {
            get { return _branchExc; }
            set { _branchExc = value; }
        }
        public int BranchTypeExc
        {
            get { return _branchTypeExc; }
            set { _branchTypeExc = value; }
        }
        public int LevelExc
        {
            get { return _levelExc; }
            set { _levelExc = value; }
        }
        public int CollIDExc
        {
            get { return _collIDExc; }
            set { _collIDExc = value; }
        }
        //-----------------------------------------------
        public string ActualIntake
        {
            get { return _actualIntake; }
            set { _actualIntake = value; }
        }
        public string ExmIntake
        {
            get { return _exmIntake; }
            set { _exmIntake = value; }
        }
        #endregion
    }
}
