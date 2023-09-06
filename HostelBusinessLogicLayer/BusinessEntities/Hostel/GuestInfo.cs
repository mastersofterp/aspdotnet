//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HOSTEL                                                               
// PAGE NAME     : GUEST INFO                                                           
// CREATION DATE : 18-AUG-2009                                                          
// CREATED BY    : SANJAY RATNAPARKHI                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Collections;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class GuestInfo
    {
        #region Private Members
        private int _guestNo = 0;
        private int _residentTypeNo = 0;
        private string _GuestName = string.Empty;
        private string _guestAddress = string.Empty;
        private string _contactNo = string.Empty;
        private string _purpose = string.Empty;
        private string _companyName = string.Empty;
        private string _companyAddress = string.Empty;
        private string _companyContactNo = string.Empty;
        private DateTime _fromDate = DateTime.MinValue;
        private DateTime _toDate = DateTime.MinValue;
        private string _collegeCode = string.Empty;
        private int _sessionno = 0;
        private int _hostelno=0;
        private int _OrganizationId = 0;
        private string _emailid = string.Empty;
        private int _ua_no = 0;

        #endregion

        #region Public Fields
        public int OrganizationId
        {
            get { return _OrganizationId; }
            set { _OrganizationId = value; }
        }
        public int GuestNo
        {
            get { return _guestNo; }
            set { _guestNo = value; }
        }
        public int ResidentTypeNo
        {
            get { return _residentTypeNo; }
            set { _residentTypeNo = value; }
        }
        public string GuestName
        {
            get { return _GuestName; }
            set { _GuestName = value; }
        }
        public string GuestAddress
        {
            get { return _guestAddress; }
            set { _guestAddress = value; }
        }
        public string ContactNo
        {
            get { return _contactNo; }
            set { _contactNo = value; }
        }
        public string Purpose
        {
            get { return _purpose; }
            set { _purpose = value; }
        }
        public string CompanyName
        {
            get { return _companyName; }
            set { _companyName = value; }
        }
        public string CompanyAddress
        {
            get { return _companyAddress; }
            set { _companyAddress = value; }
        }
        public string CompanyContactNo
        {
            get { return _companyContactNo; }
            set { _companyContactNo = value; }
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
        public string CollegeCode
        {
            get { return _collegeCode; }
            set { _collegeCode = value; }
        }

        public int Sessionno
        {
            get { return _sessionno; }
            set { _sessionno = value; }
        }
        public int Hostelno
        {
            get { return _hostelno; }
            set { _hostelno = value; }
        }
        public string EmailId
        {
            get { return _emailid; }
            set { _emailid = value; }
        }
        public int Ua_no
        { 
            get { return _ua_no; }
            set { _ua_no = value; }
        }
        #endregion

    }
}
