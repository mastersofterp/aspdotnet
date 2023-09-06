using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLogicLayer.BusinessEntities
        {
            public class LeadManage
            {
                #region Private Members
                //EnquiryStatus
                    private int _ENQUIRYSTATUSNO=0;
                    private string _ENQUIRYSTATUSNAME=string.Empty;
                    private int _ENQUIRYSTATUS=0;
                //Sourcetype
                    private int _SOURCETYPENO = 0;
                    private string _SOURCETYPENAME = string.Empty;
                    private int _SOURCETYPESTATUS = 0;
                //Enquiry Generation
                    private int _enqgenno = 0;
                    private string _studname = string.Empty;
                    private string _studentmobile = string.Empty;
                    private string _emailid = string.Empty;
                    private string _parentsmobile = string.Empty;
                    private string _school_name = string.Empty;
                    private int _cityno = 0;
                    private string _lead_collected_by = string.Empty;
                    private int _lead_source_no = 0;
                    private string _batchname = string.Empty;

                //Enquiry form
                    private int _enquiryno = 0; 
                    private string _studfirstname = string.Empty;
                    private string _studmiddlename = string.Empty;
                    private string _studlastname = string.Empty;
                    private string _studmobile = string.Empty;
                    private string _studemail = string.Empty;
                    private string _gender = string.Empty;
                    private DateTime _dob = DateTime.Today;
                    private int _degreeno = 0;
                    private int _branchno = 0;
                    private string _parentname = string.Empty;
                    private string _Parentmobile = string.Empty;
                    private string _Parentemail = string.Empty;
                    private string _address = string.Empty;
                    private int _stateno = 0;
                    private int _districtno = 0;
                    private string _pin = string.Empty;
                    private int _sourceno = 0;
                    private int _batchno = 0;
                    private string _Password = string.Empty;
                    private int _ADMBATCH = 0;
                    private int _ua_section = 0;
                    private int _OrganizationId = 0;

                #endregion

                #region Public Properties

                    //EnquiryStatus
                    public int ENQUIRYSTATUSNO
                            {
                                get { return _ENQUIRYSTATUSNO; }
                                set { _ENQUIRYSTATUSNO = value; }
                            }
                    public string ENQUIRYSTATUSNAME
                            {
                                get { return _ENQUIRYSTATUSNAME; }
                                set { _ENQUIRYSTATUSNAME = value; }
                            }
                    public int ENQUIRYSTATUS
                            {
                                get { return _ENQUIRYSTATUS; }
                                set { _ENQUIRYSTATUS = value; }
                            }

                //Sourcetype
                    public int SOURCETYPENO
                    {
                        get { return _SOURCETYPENO; }
                        set { _SOURCETYPENO = value; }
                    }
                    public string SOURCETYPENAME
                    {
                        get { return _SOURCETYPENAME; }
                        set { _SOURCETYPENAME = value; }
                    }
                    public int SOURCETYPESTATUS
                    {
                        get { return _SOURCETYPESTATUS; }
                        set { _SOURCETYPESTATUS = value; }
                    }
                    //Enquiry Generation
                    public int EnqGenNo 
                    {
                        get { return _enqgenno; }
                        set { _enqgenno = value; }
                    }
                    public string StudName
                    {
                        get { return _studname; }
                        set { _studname = value; }
                    }
                    public string studentMobile
                     {
                        get { return _studentmobile; }
                        set { _studentmobile = value; }
                    }
                    public string EmailId
                    {
                     get { return _emailid; }
                     set { _emailid = value; }
                    }
                    public string ParentsMobile
                    {
                     get { return _parentsmobile; }
                     set { _parentsmobile = value; }
                    }
                    public string School_Name
                    {
                     get { return _school_name; }
                     set { _school_name = value; }
                    }
                    public int CityNo
                    {
                     get { return _cityno; }
                     set { _cityno = value; }
                    }
                    public string Lead_Collected_by
                    {
                        get { return _lead_collected_by; }
                        set { _lead_collected_by = value; }
                    }
                    public int Lead_Source_No
                    {
                        get { return _lead_source_no; }
                        set { _lead_source_no = value; }
                    }
                    public string BatchName
                    {
                        get { return _batchname; }
                        set { _batchname = value; }
                    }

                    //Enquiry form

                    public int EnquiryNo
                    {
                        get { return _enquiryno; }
                        set { _enquiryno = value; }
                    }
                    public string StudFirstName
                    {
                        get { return _studfirstname; }
                        set { _studfirstname = value; }
                    }
                    public string StudMiddleName
                    {
                        get { return _studmiddlename; }
                        set { _studmiddlename = value; }
                    }
                    public string StudLastName
                    {
                        get { return _studlastname; }
                        set { _studlastname = value; }
                    }
                    
                    public string StudMobile
                    {
                        get { return _studmobile; }
                        set { _studmobile = value; }
                    }
                    public string StudEmail
                    {
                        get { return _studemail; }
                        set { _studemail = value; }
                    }
                    public string Gender
                    {
                        get { return _gender; }
                        set { _gender = value; }
                    }
                    public DateTime DOB
                    {
                        get { return _dob; }
                        set { _dob = value; }
                    }
                    public int DegreeNo
                    {
                        get { return _degreeno; }
                        set { _degreeno = value; }
                    }
                    public int BranchNo
                    {
                        get { return _branchno; }
                        set { _branchno = value; }
                    }
                    public string ParentName
                    {
                        get { return _parentname; }
                        set { _parentname = value; }
                    }
                    public string ParentMobile
                    {
                        get { return _Parentmobile; }
                        set { _Parentmobile = value; }
                    }
                    public string ParentEmail
                    {
                        get { return _Parentemail; }
                        set { _Parentemail = value; }
                    }
                    public string Address
                    {
                        get { return _address; }
                        set { _address = value; }
                    }
                   
                    public int StateNo
                    {
                        get { return _stateno; }
                        set { _stateno = value; }
                    }
                    public int DistrictNo
                    {
                        get { return _districtno; }
                        set { _districtno = value; }
                    }
                    public string PIN
                    {
                        get { return _pin; }
                        set { _pin = value; }
                    }
                 
                    public int SourceNo
                    {
                        get { return _sourceno; }
                        set { _sourceno = value; }
                    }
                    public int BatchNo
                    {
                        get { return _batchno; }
                        set { _batchno = value; }
                    }
                    public string Password
                    {
                        get { return _Password; }
                        set { _Password = value; }
                    }
                    public int ADMBATCH
                    {
                        get { return _ADMBATCH; }
                        set { _ADMBATCH = value; }
                    }
                    public int UA_SECTION
                    {
                        get { return _ua_section; }
                        set { _ua_section = value; }
                    }

                    public int OrganizationId
                    {
                        get { return _OrganizationId; }
                        set { _OrganizationId = value; }
                    }

                #endregion
            }
        }
    }
}