using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class Shifts
            {
                #region PrivateMembers
                private int _COLLEGE_NO;
                private string _SHIFTNAME;
                private System.Nullable<int>[] _STATUS=new System.Nullable<int>[7];
                private string _INTIME1;
                private string _OUTTIME1;
                private string[] _INTIME = new string[7];
                private string[] _OUTTIME = new string[7];
                private int _ASHNO;
                private int _IDNO;
                private string _NAME;
                private DateTime _DATE;
                private int _SHIFTNO;
                private string _COLLEGE_CODE;
                private int _HTNO;
                //add
                private DateTime _FROMDATE;
                private DateTime _TODATE;
                private int _RFIDNO;
                private int _UA_NO;
              
                #endregion
                
                #region PublicProperties
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
                public string SHIFTNAME
                {
                    get
                    {
                        return this._SHIFTNAME;
                    }
                    set
                    {
                        if ((this._SHIFTNAME != value))
                        {
                            this._SHIFTNAME = value;
                        }
                    }
                }

                public System.Nullable<int>[] STATUS
                {   get
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

                public string INTIME1
                {
                    get
                    {
                        return this._INTIME1;
                    }
                    set
                    {
                        if ((this._INTIME1 != value))
                        {
                            this._INTIME1 = value;
                        }
                    }
                }

                public string OUTTIME1
                {
                    get
                    {
                        return this._OUTTIME1;
                    }
                    set
                    {
                        if ((this._OUTTIME1 != value))
                        {
                            this._OUTTIME1 = value;
                        }
                    }
                }
                public string[] INTIME
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

                public string[] OUTTIME
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
                public int ASHNO
                {
                    get
                    {
                        return this._ASHNO;
                    }
                    set
                    {
                        if ((this._ASHNO != value))
                        {
                            this._ASHNO = value;
                        }
                    }
                }

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

                public string NAME
                {
                    get
                    {
                        return this._NAME;
                    }
                    set
                    {
                        if ((this._NAME != value))
                        {
                            this._NAME = value;
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

                public int SHIFTNO
                {
                    get
                    {
                        return this._SHIFTNO;
                    }
                    set
                    {
                        if ((this._SHIFTNO != value))
                        {
                            this._SHIFTNO = value;
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


                public int HTNO
                {
                    get
                    {
                        return this._HTNO;
                    }
                    set
                    {
                        if ((this._HTNO != value))
                        {
                            this._HTNO = value;
                        }
                    }
                }

                public DateTime FROMDATE
                {
                    get
                    {
                        return this._FROMDATE;
                    }
                    set
                    {
                        if ((this._FROMDATE != value))
                        {
                            this._FROMDATE = value;
                        }
                    }
                }
                public DateTime TODATE
                {
                    get
                    {
                        return this._TODATE;
                    }
                    set
                    {
                        if ((this._TODATE != value))
                        {
                            this._TODATE = value;
                        }
                    }
                }

                public int RFIDNO
                {
                    get
                    {
                        return this._RFIDNO;
                    }
                    set
                    {
                        if ((this._RFIDNO != value))
                        {
                            this._RFIDNO = value;
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

                #endregion
            }
        }
    }
}
