using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IITMS;

namespace IITMS.UAIMS.BusinessLogicLayer.BusinessEntities
{
    public class College
    {
        #region private members


        private int _University;

        private int _InstituteType;

        private int _State;

        private byte[] _uploadLogo = null;
        //========================================================



        private int _COLLEGE_ID;

        private string _collegeCode;


        private string _name;


        private string _address;


        private string _contactPersonName;


        private int _personDesignation;


        private string _personContactNo;

        private string _collegeType;


        private string _degreeOffered;

        private string _departmentOffered;
        private string _branchOffered;
        private string _ShortName;
        private string _Location;



        // added by abhishek 27052019 for add fitnessfile 

        private int docno;
        public string enroll;
        public string roll;
        private int idno;
        private int documentno;
        private string doctype;
        private string docname;
        private string docpath;

        //House Allotment
        private int _haId;
        private int _houseId;
        private int _eitherVal;
        private int _orVal;
        private int _activeStatus;
        private string _houseName;

        private string _houseCode;
        private string _colour;
        private byte[] _uploadSign = null;


        private string collegelogo = string.Empty;

       
       
        #endregion
        #region public members

        public byte[] UploadSign
        {
            get
            {
                return _uploadSign;
            }
            set
            {
                _uploadSign = value;
            }
        }
        public byte[] UploadLogo
        {
            get
            {
                return _uploadLogo;
            }
            set
            {
                _uploadLogo = value;
            }
        }
        public int University
        {
            get
            {
                return _University;
            }
            set
            {
                _University = value;
            }
        }

        public int State
        {
            get
            {
                return _State;
            }
            set
            {
                _State = value;
            }
        }
        public int InstituteType
        {
            get
            {
                return _InstituteType;
            }
            set
            {
                _InstituteType = value;
            }
        }

        public int COLLEGE_ID
        {
            get
            {
                return _COLLEGE_ID;
            }
            set
            {
                _COLLEGE_ID = value;
            }
        }
        public string CollegeCode
        {
            get
            {
                return _collegeCode;
            }
            set
            {
                _collegeCode = value;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
            }
        }
        public string ContactPersonName
        {
            get
            {
                return _contactPersonName;
            }
            set
            {
                _contactPersonName = value;
            }
        }
        public int PersonDesignation
        {
            get
            {
                return _personDesignation;
            }
            set
            {
                _personDesignation = value;
            }
        }
        public string PersonContactNo
        {
            get
            {
                return _personContactNo;
            }
            set
            {
                _personContactNo = value;
            }
        }
        public string Collegetype
        {
            get
            {
                return _collegeType;
            }
            set
            {
                _collegeType = value;
            }
        }
        public string DegreeOffered
        {
            get
            {
                return _degreeOffered;
            }
            set
            {
                _degreeOffered = value;
            }
        }
        public string DepartmentOffered
        {
            get
            {
                return _departmentOffered;
            }
            set
            {
                _departmentOffered = value;
            }
        }
        public string BranchOffered
        {
            get
            {
                return _branchOffered;
            }
            set
            {
                _branchOffered = value;
            }
        }

        public string Short_Name
        {
            get
            {
                return _ShortName;
            }
            set
            {
                _ShortName = value;
            }
        }

        public string Location
        {
            get
            {
                return _Location;
            }
            set
            {
                _Location = value;
            }
        }

        // for add fitnessfile
        //abhishek

        public int DOCNO
        {
            get
            {
                return docno;
            }
            set
            {
                docno = value;
            }
        }

        public int IDNO
        {
            get
            {
                return idno;
            }
            set
            {
                idno = value;
            }
        }

        public int DOCUMENTNO
        {
            get
            {
                return docno;
            }
            set
            {
                docno = value;
            }
        }

        public string DOCTYPE
        {
            get
            {
                return doctype;
            }
            set
            {
                doctype = value;
            }
        }

        public string DOCNAME
        {
            get
            {
                return docname;
            }
            set
            {
                docname = value;
            }
        }

        public string DOCPATH
        {
            get
            {
                return docpath;
            }
            set
            {
                docpath = value;
            }
        }

        public string ENROLL
        {
            get
            {
                return enroll;
            }
            set
            {
                enroll = value;
            }
        }

        public string ROLL
        {
            get
            {
                return roll;
            }
            set
            {
                roll = value;
            }
        }

        public bool ActiveStatus
        {
            get;
            set;
        }

        public int OrgId
        {
            get;
            set;
        }
        public string Colour
        {
            get { return _colour; }
            set { _colour = value; }
        }

        public string HouseCode
        {
            get { return _houseCode; }
            set { _houseCode = value; }
        }

        public string HouseName
        {
            get { return _houseName; }
            set { _houseName = value; }
        }

        public int Active_Status
        {
            get { return _activeStatus; }
            set { _activeStatus = value; }
        }

        public int Or_Val
        {
            get { return _orVal; }
            set { _orVal = value; }
        }

        public int Either_Val
        {
            get { return _eitherVal; }
            set { _eitherVal = value; }
        }

        public int House_Id
        {
            get { return _houseId; }
            set { _houseId = value; }
        }

        public int Ha_Id
        {
            get { return _haId; }
            set { _haId = value; }
        }


        public string CollegeLogo
            {
            get
                {
                return collegelogo;
                }
            set
                {
                collegelogo = value;
                }
            }


        #endregion
    }
}
