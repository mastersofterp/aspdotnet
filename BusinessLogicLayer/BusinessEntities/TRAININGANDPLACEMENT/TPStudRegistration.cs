using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class TPStudRegistration
            {
                #region Private Members 
                    private DateTime _regdate = DateTime.MinValue;
                    private char _regstatus = ' ';
                    private char _Studenttype = ' ';                
                    private String _otherqualification = string.Empty;
                    private string _college_code = string.Empty;
                    private int _gradeclass = 0;

                    // ADDED BY SUMIT-- 25092019 //
                    private string _mobileno = string.Empty;
                    private string _email = string.Empty;
                    private string _address = string.Empty;
                    private string _FileName;
                    private string _ConsentFormFileName;

                    //ADDED BY DILEEP KARE ON 13.07.2021//
                    private string _certname = string.Empty;
                    private string _Institute = string.Empty;
                    private int _Duration = 0;
                    private string _ucfilename = string.Empty;
                    private string _ucfilepath = string.Empty;
                    private string _jobprofile = string.Empty;
                    private string _Company = string.Empty;
                    private string _location = string.Empty;
                    private int _work_duration = 0;
                    private string _jobdescription = string.Empty;
                    private int _major = 0;
                    private int _minor = 0;
                    private int _age = 0;
                                                
                #endregion

                #region Public Properties
                    public DateTime RegDate
                    {
                        get { return _regdate; }
                        set { _regdate = value; }
                    }
                    public char RegStatus
                    {
                        get { return _regstatus; }
                        set { _regstatus = value; }
                    }
                    public char StudentType
                    {
                        get { return _Studenttype; }
                        set { _Studenttype = value; }
                    }
                    public String OtherQualification
                    {
                        get { return _otherqualification; }
                        set { _otherqualification = value; }
                    }
                    public string College_Code
                    {
                        get { return _college_code; }
                        set { _college_code = value; }
                    }
                    public int Gradeclass
                    {
                        get { return _gradeclass; }
                        set { _gradeclass = value; }
                    }
                    //ADDED BY SUMIT--25092019 //
                    public string Mobile
                    {
                        get { return _mobileno; }
                        set { _mobileno = value; }
                    }

                    public string Email
                    {
                        get { return _email; }
                        set { _email = value; }
                    }

                    public string Address
                    {
                        get { return _address; }
                        set { _address = value; }
                    }

                    public string FileName
                    {
                        get { return _FileName; }
                        set { _FileName = value; }
                    }

                    public string ConsentFormFileName
                    {
                        get { return _ConsentFormFileName; }
                        set { _ConsentFormFileName = value; }
                    }


                    //ADDED BY DILEEP KARE ON 13.07.2021//
                    public string Certificate_Name
                    {
                        get { return _certname; }
                        set { _certname = value; }
                    }
                    public string Institute
                    {
                        get { return _Institute; }
                        set { _Institute = value; }
                    }
                    public int Duration
                    {
                        get { return _Duration; }
                        set { _Duration = value; }
                    }
                    public string UCFilename
                    {
                        get { return _ucfilename; }
                        set { _ucfilename = value; }
                    }
                    public string Uc_FilePath
                    {
                        get { return _ucfilepath; }
                        set { _ucfilepath = value; }
                    }
                    public string Job_Profile
                    {
                        get { return _jobprofile; }
                        set { _jobprofile = value; }
                    }

                    public string Company
                    {
                        get { return _Company; }
                        set { _Company = value; }
                    }
                    public string Location
                    {
                        get { return _location; }
                        set { _location = value; }
                    }

                    public int Work_Duration
                    {
                        get { return _work_duration; }
                        set { _work_duration = value; }
                    }
                    public string Job_Description
                    {
                        get { return _jobdescription; }
                        set { _jobdescription = value; }
                    }
                    public int Major
                    {
                        get { return _major; }
                        set { _major = value; }
                    }
                    public int Minor
                    {
                        get { return _minor; }
                        set { _minor = value; }
                    }
                    public int Age
                    {
                        get { return _age; }
                        set { _age = value; }
                    }
                #endregion
            }
        }
    }
}
