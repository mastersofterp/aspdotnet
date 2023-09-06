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
            public class ITLETestMaster
            {
                #region Private Members

                #region IETestMaster Part
                private int _TESTNO;

                private string _TESTNAME;

                private int _UA_NO;

                private DateTime _ENDDATE;

                private DateTime _STARTDATE;

                private DateTime _TESTDURATION;

                private string _COLLEGE_CODE;

                private DateTime _CREATEDDATE;

                private int _ATTEMPTS = 0;

                private int _TOTQUESTION = 0;

                private decimal _MARKSRIGHTANS;

                private decimal _MARKSWRONGANS;

                private decimal _TOTMARK;

                private string _IE_COURSENOS = string.Empty;

                private string _QUESTIONRATIO = string.Empty;

                private string _QUESTION_MARKS = string.Empty;

                private string _TEST_TYPE = string.Empty;

                private char _SHOWRESULT;

                private char _SHOWRANDOM;

                private char _FULL_RANDOME_TEST;

                #endregion

                private int _TEST_NO;


                private string _QUESTIONNO;

                private int _IDNO;

                private string _CORR_ANS;

                private int _CORRECTMARKS;

                private int _WRONG_ANS;

                private bool _STATUS;

                public bool STATUS
                {
                    get { return _STATUS; }
                    set { _STATUS = value; }
                }



                private string _SELECTED_TOPICS = string.Empty;

                private int _STUDATTEMPTS;

                private string _studidno = string.Empty;
                private string _count = string.Empty;
                private string _reqno = string.Empty;

                private string _uano = string.Empty;



                #endregion

                #region Public Memebers

                public int TESTNO
                {
                    get { return _TESTNO; }
                    set { _TESTNO = value; }
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

                public DateTime TESTDURATION
                {
                    get { return _TESTDURATION; }
                    set { _TESTDURATION = value; }
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

                public int ATTEMPTS
                {
                    get { return _ATTEMPTS; }
                    set { _ATTEMPTS = value; }
                }

                public int TOTQUESTION
                {
                    get { return _TOTQUESTION; }
                    set { _TOTQUESTION = value; }
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

                public decimal TOTMARK
                {
                    get { return _TOTMARK; }
                    set { _TOTMARK = value; }
                }


                public string IE_COURSENOS
                {
                    get { return _IE_COURSENOS; }
                    set { _IE_COURSENOS = value; }
                }

                public string QUESTIONRATIO
                {
                    get { return _QUESTIONRATIO; }
                    set { _QUESTIONRATIO = value; }
                }

                public string QUESTION_MARKS
                {
                    get { return _QUESTION_MARKS; }
                    set { _QUESTION_MARKS = value; }
                }

                public string TEST_TYPE
                {
                    get { return _TEST_TYPE; }
                    set { _TEST_TYPE = value; }
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

                public char FULL_RANDOME_TEST
                {
                    get { return _FULL_RANDOME_TEST; }
                    set { _FULL_RANDOME_TEST = value; }
                }


                /// <summary>
                /// ///
                /// </summary>
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

                public string SELECTED_TOPICS
                {
                    get { return _SELECTED_TOPICS; }
                    set { _SELECTED_TOPICS = value; }
                }

                public int STUDATTEMPTS
                {
                    get { return _STUDATTEMPTS; }
                    set { _STUDATTEMPTS = value; }
                }

                #endregion
            }
        }
    }
}
