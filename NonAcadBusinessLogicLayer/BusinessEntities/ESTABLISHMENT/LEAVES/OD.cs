using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class OD
            {
                #region Private members

                //[TABLE NAME="PAY_OD_PASSING_AUTHORITY_PATH"]
                private int _COLLEGE_NO;
                private int _PAPNO;
                private int _PAN01;
                private int _PAN02;
                private int _PAN03;
                private int _PAN04;
                private int _PAN05;
                private string _PAPATH;
                private int _DEPTNO;
                //[TABLE NAME="PAY_OD_APP_ENTRY"]

                private DateTime _FROMDT;
                private DateTime _TODT;
                private double _NO_DAYS;
                private DateTime _JOINDT;

                private int _ODTRNO;
                private int _EMPNO;
                private DateTime _APRDT;
                private DateTime _DATE;
                private string _PLACE;
                private string _PURPOSE;
                private int _PURPOSENO;
                private string _INSTRBY;
                private string _OUT_TIME;
                private string _IN_TIME;
                private string _INTIME;
                private string _OUTTIME;
                private char _STATUS;
                private string _COLLEGE_CODE;
                //[TABLE NAME="PAY_OD_APP_PASS_ENTRY"]
                private int _ODPENO;
                private int _PANO;
                private string _APR_REMARKS;
                private double _REG_AMT;
                private int _TADA_AMT;
                private int _EVENT;
                private string _TOPIC;
                private string _ORGANISED_BY;
                private string _ODTYPE;


                private DateTime _EVENT_FROMDT;
                private DateTime _EVENT_TODT;
                private string _EVENTTYPE;
                private string _FileName = string.Empty;
                private string _FilePath = string.Empty;
                private decimal _FileSize = 0;

                # endregion
                #region public members
               
                public int COLLEGE_NO
                {
                    get
                    {
                        return this._COLLEGE_NO;
                    }
                    set
                    {
                        if ((this._COLLEGE_NO != value))
                        {
                            this._COLLEGE_NO = value;
                        }
                    }
                }
                public int PAPNO
                {
                    get
                    {
                        return this._PAPNO;
                    }
                    set
                    {
                        if ((this._PAPNO != value))
                        {
                            this._PAPNO = value;
                        }
                    }
                }
                public int PAN01
                {
                    get
                    {
                        return this._PAN01;
                    }
                    set
                    {
                        if ((this._PAN01 != value))
                        {
                            this._PAN01 = value;
                        }
                    }
                }
                public int PAN02
                {
                    get
                    {
                        return this._PAN02;
                    }
                    set
                    {
                        if ((this._PAN02 != value))
                        {
                            this._PAN02 = value;
                        }
                    }
                }
                public int PAN03
                {
                    get
                    {
                        return this._PAN03;
                    }
                    set
                    {
                        if ((this._PAN03 != value))
                        {
                            this._PAN03 = value;
                        }
                    }
                }
                public int PAN04
                {
                    get
                    {
                        return this._PAN04;
                    }
                    set
                    {
                        if ((this._PAN04 != value))
                        {
                            this._PAN04 = value;
                        }
                    }
                }
                public int PAN05
                {
                    get
                    {
                        return this._PAN05;
                    }
                    set
                    {
                        if ((this._PAN05 != value))
                        {
                            this._PAN05 = value;
                        }
                    }
                }
                public string PAPATH
                {
                    get
                    {
                        return this._PAPATH;
                    }
                    set
                    {
                        if ((this._PAPATH != value))
                        {
                            this._PAPATH = value;
                        }
                    }
                }
                public int DEPTNO
                {
                    get
                    {
                        return this._DEPTNO;
                    }
                    set
                    {
                        if (this._DEPTNO != value)
                        {
                            this._DEPTNO = value;
                        }
                    }
                }

                public DateTime FROMDT
                {
                    get
                    {
                        return this._FROMDT;
                    }
                    set
                    {
                        if ((this._FROMDT != value))
                        {
                            this._FROMDT = value;
                        }
                    }
                }
                public DateTime TODT
                {
                    get
                    {
                        return this._TODT;
                    }
                    set
                    {
                        if ((this._TODT != value))
                        {
                            this._TODT = value;
                        }
                    }
                }


                public double NO_DAYS
                {
                    get
                    {
                        return this._NO_DAYS;
                    }
                    set
                    {
                        if ((this._NO_DAYS != value))
                        {
                            this._NO_DAYS = value;
                        }
                    }

                }
                public DateTime JOINDT
                {
                    get
                    {
                        return this._JOINDT;
                    }
                    set
                    {
                        if ((this._JOINDT != value))
                        {
                            this._JOINDT = value;
                        }
                    }
                }



                public int ODTRNO
                {
                    get
                    {
                        return this._ODTRNO;
                    }
                    set
                    {
                        if ((this._ODTRNO != value))
                        {
                            this._ODTRNO = value;
                        }
                    }
                }
                public int EMPNO
                {
                    get
                    {
                        return this._EMPNO;
                    }
                    set
                    {
                        if ((this._EMPNO != value))
                        {
                            this._EMPNO = value;
                        }
                    }
                }
                public DateTime APRDT
                {
                    get
                    {
                        return this._APRDT;
                    }
                    set
                    {
                        if ((this._APRDT != value))
                        {
                            this._APRDT = value;
                        }
                    }
                }
                public DateTime DATE
                {
                    get
                    {
                        return this._DATE;
                    }
                    set
                    {
                        if ((this._DATE != value))
                        {
                            this._DATE = value;
                        }
                    }
                }
                public string PLACE
                {
                    get
                    {
                        return this._PLACE;
                    }
                    set
                    {
                        if ((this._PLACE != value))
                        {
                            this._PLACE = value;
                        }
                    }
                }
                public string PURPOSE
                {
                    get
                    {
                        return this._PURPOSE;
                    }
                    set
                    {
                        if ((this._PURPOSE != value))
                        {
                            this._PURPOSE = value;
                        }
                    }
                }

                public int PURPOSENO
                {
                    get
                    {
                        return this._PURPOSENO;
                    }
                    set
                    {
                        if ((this._PURPOSENO != value))
                        {
                            this._PURPOSENO = value;
                        }
                    }

                }
                public string INSTRBY
                {
                    get
                    {
                        return this._INSTRBY;
                    }
                    set
                    {
                        if ((this._INSTRBY != value))
                        {
                            this._INSTRBY = value;
                        }
                    }
                }
                public string OUT_TIME
                {
                    get
                    {
                        return this._OUT_TIME;
                    }
                    set
                    {
                        if ((this._OUT_TIME != value))
                        {
                            this._OUT_TIME = value;
                        }
                    }
                }
                public string IN_TIME
                {
                    get
                    {
                        return this._IN_TIME;
                    }
                    set
                    {
                        if ((this._IN_TIME != value))
                        {
                            this._IN_TIME = value;
                        }
                    }
                }
                public string OUTTIME
                {
                    get
                    {
                        return this._OUTTIME;
                    }
                    set
                    {
                        if ((this._OUTTIME != value))
                        {
                            this._OUTTIME = value;
                        }
                    }
                }
                public string INTIME
                {
                    get
                    {
                        return this._INTIME;
                    }
                    set
                    {
                        if ((this._INTIME != value))
                        {
                            this._INTIME = value;
                        }
                    }
                }
                public char STATUS
                {
                    get
                    {
                        return this._STATUS;
                    }
                    set
                    {
                        if ((this._STATUS != value))
                        {
                            this._STATUS = value;
                        }
                    }
                }
                public string COLLEGE_CODE
                {
                    get
                    {
                        return this._COLLEGE_CODE;
                    }
                    set
                    {
                        if ((this._COLLEGE_CODE != value))
                        {
                            this._COLLEGE_CODE = value;
                        }
                    }
                }
                private int ODPENO
                {
                    get
                    {
                        return this._ODPENO;
                    }
                    set
                    {
                        if (this._ODPENO != value)
                        {
                            this._ODPENO = value;
                        }
                    }
                }
                private int PANO
                {
                    get
                    {
                        return this._PANO;
                    }
                    set
                    {
                        if (this._PANO != value)
                        {
                            this._PANO = value;
                        }
                    }
                }
                public string APR_REMARKS
                {
                    get
                    {
                        return this._APR_REMARKS;
                    }
                    set
                    {
                        if ((this._APR_REMARKS != value))
                        {
                            this._APR_REMARKS = value;
                        }
                    }
                }

                public double REG_AMT
                {
                    get
                    {
                        return this._REG_AMT;
                    }
                    set
                    {
                        if ((this._REG_AMT != value))
                        {
                            this._REG_AMT = value;
                        }
                    }

                }

                public int TADA_AMT
                {
                    get
                    {
                        return this._TADA_AMT;
                    }
                    set
                    {
                        if ((this._TADA_AMT != value))
                        {
                            this._TADA_AMT = value;
                        }
                    }

                }

                public int EVENT
                {
                    get
                    {
                        return this._EVENT;
                    }
                    set
                    {
                        if ((this._EVENT != value))
                        {
                            this._EVENT = value;
                        }
                    }
                }

                public string TOPIC
                {
                    get
                    {
                        return this._TOPIC;
                    }
                    set
                    {
                        if ((this._TOPIC != value))
                        {
                            this._TOPIC = value;
                        }
                    }
                }

                public string ORGANISED_BY
                {
                    get
                    {
                        return this._ORGANISED_BY;
                    }
                    set
                    {
                        if ((this._ORGANISED_BY != value))
                        {
                            this._ORGANISED_BY = value;
                        }
                    }
                }


                public string ODTYPE
                {
                    get
                    {
                        return this._ODTYPE;
                    }
                    set
                    {
                        if ((this._ODTYPE != value))
                        {
                            this._ODTYPE = value;
                        }
                    }
                }
                public DateTime EVENT_FROMDT
                {
                    get
                    {
                        return this._EVENT_FROMDT;
                    }
                    set
                    {
                        if ((this._EVENT_FROMDT != value))
                        {
                            this._EVENT_FROMDT = value;
                        }
                    }
                }
                public DateTime EVENT_TODT
                {
                    get
                    {
                        return this._EVENT_TODT;
                    }
                    set
                    {
                        if ((this._EVENT_TODT != value))
                        {
                            this._EVENT_TODT = value;
                        }
                    }
                }

                public string FileName
                {
                    get { return _FileName; }
                    set { _FileName = value; }
                }

                public string FilePath
                {
                    get { return _FilePath; }
                    set { _FilePath = value; }
                }

                public decimal FileSize
                {
                    get { return _FileSize; }
                    set { _FileSize = value; }
                }

                #region new
                public string EVENTTYPE
                {
                    get
                    {
                        return this._EVENTTYPE;
                    }
                    set
                    {
                        if ((this._EVENTTYPE != value))
                        {
                            this._EVENTTYPE = value;
                        }
                    }
                }
                #endregion




                #endregion
            }
        }
    }
}

