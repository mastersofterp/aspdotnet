using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Linq;
using System.Text;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            
         public  class ITLE_RequestAllowrestest
         {

             private  int        _sessionno     = 0;
             private int _ua_no = 0;

             private  string     _stud_idno      = string.Empty;
             private  string     _stud_name      = string.Empty;
             private  string     _roll_no        = string.Empty;
             private  string     _branchno       = string.Empty;
             private  string     _courseno       = string.Empty;
             private  string     _courseName     = string.Empty;
             private  string     _subid          = string.Empty ;
             private  string      _credits       = string.Empty;
             private  string     _testno         = string.Empty;
             private  string     _lecture        = string.Empty ;
             private  string     _theory         = string.Empty;
             private  string     _practicle      = string.Empty ;
             private  string     _batchname      = string.Empty;

             private string _studName = string.Empty;





             public int Ua_no
             {
                 get { return _ua_no; }
                 set { _ua_no = value; }
             }

             public string StudName
             {
                 get { 
                     return _studName;
                     }
                 set { _studName = value; 
                     }
             }
             public string BatchName
             {
                 get
                 {
                     return _batchname;
                 }
                 set
                 {
                     _batchname = value;
                 }
             }



             public string Lecture
             {
                 get { return _lecture; }
                 set { _lecture = value; }
             }

             public string  Theory
             {
                 get { return _theory; }
                 set { _theory = value; }
             }

             public string  Practicle
             {
                 get { return _practicle; }
                 set { _practicle = value; }
             }

             public string Testno
             {
                 get { return _testno; }
                 set { _testno = value; }
             }
             public string Subid
             {
                 get
                 {
                     return _subid;
                 }
                 set
                 {
                     _subid = value;
                 }
             }
             public string Credits
             {
                 get
                 {
                     return _credits;
                 }
                 set
                 {
                     _credits = value;
                 }
             }

             public int   Sessionno
             {
                 get { return _sessionno; }
                 set { _sessionno = value; }
             }

             public string  Stud_idno
             {
                 get { return _stud_idno; }
                 set { _stud_idno = value; }
             }

             public string Roll_no
             {
                 get { return _roll_no; }
                 set { _roll_no = value; }
             }

             public string  Branchno
             {
                 get { return _branchno; }
                 set { _branchno = value; }
             }

             public string Courseno
             {
                 get { return _courseno; }
                 set { _courseno = value; }
             }

             public string CourseName
             {
                 get { return _courseName; }
                 set {_courseName=value; }
             }



         }

        }
    }
}
