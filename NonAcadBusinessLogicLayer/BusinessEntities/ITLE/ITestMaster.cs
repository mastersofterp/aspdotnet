using System;
using System.Data;
using System.Web;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class ITestMaster
            {
                #region Private Members

                private int _TESTNO;

                private int _SDSRNO;

                private string _TESTNAME;

                private int _COURSENO;

                private int _SESSIONNO;

                private DateTime _STARTDATE;

                private DateTime _ENDDATE;

                private decimal _MARKSRIGHTANS;

                private decimal _MARKSWRONGANS;

                private char _SHOWRANDOM;

                private DateTime _TESTDURATION;

                private string _COLLEGE_CODE;

                private DateTime _CREATEDDATE;
                private int _TEST_NO;


                private string _QUESTIONNO;

                private int _UA_NO;

                private int _IDNO;

                private decimal _TOTAL;

                private string _CORR_ANS;

                private int _CORRECTMARKS;

                private int _WRONG_ANS;
                private bool _STATUS;

                public bool STATUS
                {
                    get { return _STATUS; }
                    set { _STATUS = value; }
                }



                private char _SHOWRESULT;

                private char _SHOW_ANSWER_KEY;

                private int _TOTQUESTION = 0;

                private string _SELECTED_TOPICS = string.Empty;

                private string _QUESTIONRATIO = string.Empty;

                private int _ATTEMPTS = 0;

                private int _MalFunctionCount = 0;

                private string _STUDNAME = string.Empty;

                private string _TEST_TYPE = string.Empty;

                private string _QUESTION_MARKS = string.Empty;

                private int _STUDATTEMPTS;

                private string _studidno = string.Empty;
                private string _count = string.Empty;
                private string _reqno = string.Empty;

                private string _uano = string.Empty;

                private char _FULL_RANDOME_TEST;


                private string _IsAllowPassword = string.Empty;

                private string _Password = string.Empty;

                #endregion

                #region Public Memebers

                public string Uano
                {
                    get { return _uano; }
                    set { _uano = value; }
                }
                public string Studidno
                {
                    get { return _studidno; }
                    set { _studidno = value; }
                }

                public string Count
                {
                    get { return _count; }
                    set { _count = value; }
                }
                public string Reqno
                {
                    get { return _reqno; }
                    set { _reqno = value; }
                }

                public int WRONG_ANS
                {
                    get { return _WRONG_ANS; }
                    set { _WRONG_ANS = value; }
                }

                public int IDNO
                {
                    get { return _IDNO; }
                    set { _IDNO = value; }
                }

                public int SDSRNO
                {
                    get { return _SDSRNO; }
                    set { _SDSRNO = value; }
                }

                public string CORR_ANS
                {
                    get { return _CORR_ANS; }
                    set { _CORR_ANS = value; }
                }
                public int CORRECTMARKS
                {
                    get { return _CORRECTMARKS; }
                    set { _CORRECTMARKS = value; }
                }
                public int TESTNO
                {
                    get { return _TESTNO; }
                    set { _TESTNO = value; }
                }
                public decimal TOTAL
                {
                    get { return _TOTAL; }
                    set { _TOTAL = value; }
                }
                public string TESTNAME
                {
                    get
                    {
                        return _TESTNAME;
                    }
                    set
                    {
                        _TESTNAME = value;
                    }
                }

                public int COURSENO
                {
                    get
                    {
                        return _COURSENO;
                    }
                    set
                    {
                        _COURSENO = value;
                    }
                }
                public DateTime TESTDURATION
                {
                    get { return _TESTDURATION; }
                    set { _TESTDURATION = value; }
                }
                public int SESSIONNO
                {
                    get
                    {
                        return _SESSIONNO;
                    }
                    set
                    {
                        _SESSIONNO = value;
                    }
                }

                public DateTime STARTDATE
                {
                    get
                    {
                        return _STARTDATE;
                    }
                    set
                    {
                        _STARTDATE = value;
                    }
                }

                public DateTime ENDDATE
                {
                    get
                    {
                        return _ENDDATE;
                    }
                    set
                    {
                        _ENDDATE = value;
                    }
                }

                public decimal MARKSRIGHTANS
                {
                    get
                    {
                        return _MARKSRIGHTANS;
                    }
                    set
                    {
                        _MARKSRIGHTANS = value;
                    }
                }

                public decimal MARKSWRONGANS
                {
                    get
                    {
                        return _MARKSWRONGANS;
                    }
                    set
                    {
                        _MARKSWRONGANS = value;
                    }
                }

                public char SHOWRANDOM
                {
                    get
                    {
                        return _SHOWRANDOM;
                    }
                    set
                    {
                        _SHOWRANDOM = value;
                    }
                }

                public int TEST_NO
                {
                    get { return _TEST_NO; }
                    set { _TEST_NO = value; }
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

                public DateTime CREATEDDATE
                {
                    get
                    {
                        return _CREATEDDATE;
                    }
                    set
                    {
                        _CREATEDDATE = value;
                    }
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

                public int UA_NO
                {
                    get
                    {
                        return _UA_NO;
                    }
                    set
                    {
                        _UA_NO = value;
                    }
                }


                public char SHOWRESULT
                {
                    get
                    {
                        return _SHOWRESULT;
                    }
                    set
                    {
                        _SHOWRESULT = value;
                    }
                }


                public char SHOW_ANSWER_KEY
                {
                    get
                    {
                        return _SHOW_ANSWER_KEY;
                    }
                    set
                    {
                        _SHOW_ANSWER_KEY = value;
                    }
                }

                public int TOTQUESTION
                {
                    get { return _TOTQUESTION; }
                    set { _TOTQUESTION = value; }
                }


                public string SELECTED_TOPICS
                {
                    get { return _SELECTED_TOPICS; }
                    set { _SELECTED_TOPICS = value; }
                }

                public string QUESTIONRATIO
                {
                    get { return _QUESTIONRATIO; }
                    set { _QUESTIONRATIO = value; }
                }

                public int ATTEMPTS
                {
                    get { return _ATTEMPTS; }
                    set { _ATTEMPTS = value; }
                }

                public int MalFunctionCount
                {
                    get { return _MalFunctionCount; }
                    set { _MalFunctionCount = value; }
                }

                public string STUDNAME
                {
                    get { return _STUDNAME; }
                    set { _STUDNAME = value; }
                }


                public string TEST_TYPE
                {
                    get { return _TEST_TYPE; }
                    set { _TEST_TYPE = value; }
                }

                public string QUESTION_MARKS
                {
                    get { return _QUESTION_MARKS; }
                    set { _QUESTION_MARKS = value; }
                }
                public int STUDATTEMPTS
                {
                    get { return _STUDATTEMPTS; }
                    set { _STUDATTEMPTS = value; }
                }

                public char FULL_RANDOME_TEST
                {
                    get { return _FULL_RANDOME_TEST; }
                    set { _FULL_RANDOME_TEST = value; }
                }

                public string IsAllowPassword
                {
                    get { return _IsAllowPassword; }
                    set { _IsAllowPassword = value; }
                }
                public string Password
                {
                    get { return _Password; }
                    set { _Password = value; }
                }

                #endregion
            }
        }
    }
}
