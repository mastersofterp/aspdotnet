//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                        
// PAGE NAME     : STUDENT SEARCH CLASS
// CREATION DATE : 30-JUN-2009                                                        
// CREATED BY    : AMIT YADAV
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class StudentSearch
    {
        string _searchText = string.Empty;
        string _searchField = string.Empty;
        string _orderByField = string.Empty;

        string _studentName = string.Empty;
        string _enrollmentNo = string.Empty;
        int _degreeNo = 0;
        int _branchNo = 0;
        int _yearNo = 0;
        int _semesterNo = 0;
        string _idNo = string.Empty;
        string _srno = string.Empty;
        //Added by Nikhil Vind Lambe on 20022020
        string _fatherName = string.Empty;
        string _motherName = string.Empty;
        string _mobileNo = string.Empty;

        //Added by Saurabh Lonare 19/02/2022 for Hostel
        string _regNo = string.Empty;
        string _divNo = string.Empty;
        string _appid = string.Empty;
        //----------End by Saurabh-----------

        //Added by SHUBHAM BARKE 01/06/2022 for Hostel
        int _OrganizationId = 0;

        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; }
        }

        public string SearchField
        {
            get { return _searchField; }
            set { _searchField = value; }
        }

        public string OrderByField
        {
            get { return _orderByField; }
            set { _orderByField = value; }
        }

        public string StudentName
        {
            get { return _studentName; }
            set { _studentName = value; }
        }

        public string EnrollmentNo
        {
            get { return _enrollmentNo; }
            set { _enrollmentNo = value; }
        }

        public int DegreeNo
        {
            get { return _degreeNo; }
            set { _degreeNo = value; }
        }

        public int BranchNo
        {
            get { return _branchNo; }
            set { _branchNo = value; }
        }

        public int YearNo
        {
            get { return _yearNo; }
            set { _yearNo = value; }
        }

        public int SemesterNo
        {
            get { return _semesterNo; }
            set { _semesterNo = value; }
        }
        public string IdNo
        {
            get { return _idNo; }
            set { _idNo = value; }
        }
        public string Srno
        {
            get { return _srno; }
            set { _srno = value; }
        }

        //Added by Nikhil Vinod Lambe on 20022020
        public string FatherName
        {
            get { return _fatherName; }
            set { _fatherName = value; }
        }
        public string MotherName
        {
            get { return _motherName; }
            set { _motherName = value; }
        }
        public string MobileNo
        {
            get { return _mobileNo; }
            set { _mobileNo = value; }
        }


        public string RegNo
        {
            get { return _regNo; }
            set { _regNo = value; }
        }
        public string DivisionNo
        {
            get { return _divNo; }
            set { _divNo = value; }
        }
        public string ApplicationID
        {
            get { return _appid; }
            set { _appid = value; }
        }

        public int OrganizationId
        {
            get { return _OrganizationId; }
            set { _OrganizationId = value; }
        }

    }
}