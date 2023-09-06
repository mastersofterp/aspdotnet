using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLayer.BusinessEntities
        {
           public class TestResult
            {


                #region Private Members

                private int _TESTNO;

                private int _IDNO;

                private int _CORRECTMARKS;

                private decimal _TOTALMARKS;

                private int _COURSENO;

                private int _STUDATTEMPTS;

                private string _COLLEGE_CODE;

                private int _STUDENT_ID;

                private string _STUDENT_NAME;
                private string _ATTEMPT_STATUS;

                private string _REMARKS = string.Empty;

                private decimal _MARKS_OBTAINED;

                private int _SESSIONNO;

                
                private System.Nullable<char> _CHECKED;

                private string _QUESTIONNO;

                //private DateTime _CREATEDDATE;



                #endregion

                #region Public Memebers

                public int TESTNO
                {
                    get { return _TESTNO; }
                    set { _TESTNO = value; }
                }

                public int IDNO
                {
                    get { return _IDNO; }
                    set { _IDNO = value; }
                }
                public int CORRECTMARKS
                {
                    get { return _CORRECTMARKS; }
                    set { _CORRECTMARKS = value; }
                }
                public decimal TOTALMARKS
                {
                    get { return _TOTALMARKS; }
                    set { _TOTALMARKS = value; }
                }
                public int COURSENO
                {
                    get { return _COURSENO; }
                    set { _COURSENO = value; }
                }

                public int STUDATTEMPTS
                {
                    get { return _STUDATTEMPTS; }
                    set { _STUDATTEMPTS = value; }
                }



                public string COLLEGE_CODE
                {
                    get
                    {
                        return _COLLEGE_CODE;
                    }
                    set
                    {
                        _COLLEGE_CODE = value;
                    }
                }

                public int STUDENT_ID
                {
                    get { return _STUDENT_ID; }
                    set { _STUDENT_ID = value; }
                }

                public string STUDENT_NAME
                {
                    get { return _STUDENT_NAME; }
                    set { _STUDENT_NAME = value; }
                }

                public string ATTEMPT_STATUS
                {
                    get { return _ATTEMPT_STATUS; }
                    set { _ATTEMPT_STATUS = value; }
                }

                public decimal MARKS_OBTAINED
                {
                    get { return _MARKS_OBTAINED; }
                    set { _MARKS_OBTAINED = value; }
                }

                public System.Nullable<char> CHECKED
                {
                    get
                    {
                        return this._CHECKED;
                    }
                    set
                    {
                        if ((this._CHECKED != value))
                        {
                            this._CHECKED = value;
                        }
                    }
                }

                public string REMARKS
                {
                    get { return _REMARKS; }
                    set { _REMARKS = value; }
                }

                public string QUESTIONNO
                {
                    get
                    {
                        return _QUESTIONNO;
                    }
                    set
                    {
                        _QUESTIONNO = value;
                    }
                }

                public int SESSIONNO
                {
                    get { return _SESSIONNO; }
                    set { _SESSIONNO = value; }
                }
                //public DateTime CREATEDDATE
                //{
                //    get 
                //    { 
                //        return _CREATEDDATE;
                //    }
                //    set 
                //    { 
                //        _CREATEDDATE = value; 
                //    }
                //}


                #endregion
            }
        }
    }
}
