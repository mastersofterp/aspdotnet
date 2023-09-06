using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
           public class AdvancePassingAuthority
            {
                //[TABLE NAME="PAYROLL_ADVANCE_PASSING_AUTHORITY"]
                private int _PNO;
                private string _PANAME;
                private int _UA_NO;
                private int _COLLEGE_NO;
                private int _DEPTNO;

                private int _PAN01;
                private int _PAN02;
                private int _PAN03;
                private int _PAN04;
                private int _PAN05;
                private string _PAPATH;
         
               
                private int _COLLEGE_CODE;


                private int _PAPNO;

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

                public string PANAME
                {
                    get
                    {
                        return this._PANAME;
                    }
                    set
                    {
                        if ((this._PANAME != value))
                        {
                            this._PANAME = value;
                        }
                    }

                }

                public int UA_NO
                {
                    get
                    {
                        return this._UA_NO;
                    }
                    set
                    {
                        if ((this._UA_NO != value))
                        {
                            this._UA_NO = value;
                        }
                    }
                }

                public int PANO
                {
                    get
                    {
                        return this._PNO;
                    }
                    set
                    {
                        if ((this._PNO != value))
                        {
                            this._PNO = value;
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
                        if ((this._DEPTNO != value))
                        {
                            this._DEPTNO = value;
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

                public int COLLEGE_CODE
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

                private int _EMPNO;

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

                private string _REASON;
                public string REASON
                {
                    get
                    {
                        return this._REASON;
                    }
                    set
                    {
                        if ((this._REASON != value))
                        {
                            this._REASON = value;
                        }
                    }
                }

                private double _ADVANCEAMOUNT;
                public double ADVANCEAMOUNT
                {
                    get
                    {
                        return this._ADVANCEAMOUNT;
                    }
                    set
                    {
                        if ((this._ADVANCEAMOUNT != value))
                        {
                            this._ADVANCEAMOUNT = value;
                        }
                    }

                }

                private DateTime _ADVCANEDATE;
                public DateTime ADVCANEDATE
                {
                    get
                    {
                        return this._ADVCANEDATE;
                    }
                    set
                    {
                        if ((this._ADVCANEDATE != value))
                        {
                            this._ADVCANEDATE = value;
                        }
                    }

                }


                private DateTime _APPLYDATE;
                public DateTime APPLYDATE
                {
                    get
                    {
                        return this._APPLYDATE;
                    }
                    set
                    {
                        if ((this._APPLYDATE != value))
                        {
                            this._APPLYDATE = value;
                        }
                    }

                }

                private int _STAFFNO;
                public int STAFFNO
                {
                    get
                    {
                        return this._STAFFNO;
                    }
                    set
                    {
                        if ((this._STAFFNO != value))
                        {
                            this._STAFFNO = value;
                        }
                    }

                }

                private int _ANO;
                public int ANO
                {
                    get
                    {
                        return this._ANO;
                    }
                    set
                    {
                        if ((this._ANO != value))
                        {
                            this._ANO = value;
                        }
                    }

                }

                private DateTime _APPROVEREJECTDATE;
                public DateTime APPROVEREJECTDATE
                {
                    get
                    {
                        return this._APPROVEREJECTDATE;
                    }
                    set
                    {
                        if ((this._APPROVEREJECTDATE != value))
                        {
                            this._APPROVEREJECTDATE = value;
                        }
                    }
                }   

                private DateTime _ADVCAHEDATE;
                public DateTime ADVCAHEDATE
                {
                    get
                    {
                        return this._ADVCAHEDATE;
                    }
                    set
                    {
                        if ((this._ADVCAHEDATE != value))
                        {
                            this._ADVCAHEDATE = value;
                        }
                    }
                }

                private string _ADVCANSTATUS;
                public string ADVCANSTATUS
                {
                    get
                    {
                        return this._ADVCANSTATUS;
                    }
                    set
                    {
                        if ((this._ADVCANSTATUS != value))
                        {
                            this._ADVCANSTATUS = value;
                        }
                    }
                }

                private int _IDNO;
                public int IDNO
                {
                    get
                    {
                        return this._IDNO;
                    }
                    set
                    {
                        if ((this._IDNO != value))
                        {
                            this._IDNO = value;
                        }
                    }

                }

                private string _STATUS;
                public string STATUS
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

                private string _APPROVEREJECTREMARK;
                public string APPROVEREJECTREMARK
               {
                   get
                   {
                       return this._APPROVEREJECTREMARK;
                   }
                   set
                   {
                       if ((this._APPROVEREJECTREMARK != value))
                       {
                           this._APPROVEREJECTREMARK = value;
                       }
                   }
               }
            }
        }
    }
}
