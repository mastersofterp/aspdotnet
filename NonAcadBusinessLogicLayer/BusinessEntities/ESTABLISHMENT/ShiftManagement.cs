using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS
{
    namespace NITPRM
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class ShiftManagement
            {
                #region Private members
                private int _STNO;
                private int _SHIFTNO;
                private string _SHIFTNAME = string.Empty;
                private string _INTIME = string.Empty;
                private string _OUTTIME = string.Empty;

                private string _INTIME_MID = string.Empty;
                private string _OUTTIME_MID = string.Empty;
                private System.Nullable<bool> _IsNightShift;
                private System.Nullable<bool> _IsAllowCompOffLeave;
                private System.Nullable<bool> _IsDoubleDuty;
                private int _INCHARGEEMPLOYEEIDNO;
                private int _EMPLOYEEIDNO;
                private int _COLLEGE_NO;

                private System.Nullable<System.DateTime> _FROMDATE = DateTime.MinValue;
                private System.Nullable<System.DateTime> _TODATE = DateTime.MinValue;
                private System.Nullable<System.DateTime> _SHIFTDATE = DateTime.MinValue;


                #endregion


                #region Public Region

                public int STNO
                {
                    get { return _STNO; }
                    set { _STNO = value; }
                }
                public int SHIFTNO
                {
                    get { return _SHIFTNO; }
                    set { _SHIFTNO = value; }
                }

                public string SHIFTNAME
                {
                    get { return _SHIFTNAME; }
                    set { _SHIFTNAME = value; }
                }

                public string INTIME
                {
                    get { return _INTIME; }
                    set { _INTIME = value; }
                }

                public string OUTTIME
                {
                    get { return _OUTTIME; }
                    set { _OUTTIME = value; }
                }
                public string INTIME_MID
                {
                    get { return _INTIME_MID; }
                    set { _INTIME_MID = value; }
                }

                public string OUTTIME_MID
                {
                    get { return _OUTTIME_MID; }
                    set { _OUTTIME_MID = value; }
                }
                public System.Nullable<bool> IsNightShift
                {
                    get
                    {
                        return this._IsNightShift;
                    }
                    set
                    {
                        if ((this._IsNightShift != value))
                        {
                            this._IsNightShift = value;
                        }
                    }
                }
                //_IsDoubleDuty
                public System.Nullable<bool> IsDoubleDuty
                {
                    get
                    {
                        return this._IsDoubleDuty;
                    }
                    set
                    {
                        if ((this._IsDoubleDuty != value))
                        {
                            this._IsDoubleDuty = value;
                        }
                    }
                }
                public System.Nullable<bool> IsAllowCompOffLeave
                {
                    get
                    {
                        return this._IsAllowCompOffLeave;
                    }
                    set
                    {
                        if ((this._IsAllowCompOffLeave != value))
                        {
                            this._IsAllowCompOffLeave = value;
                        }
                    }
                }
                //_IsAllowCompOffLeave
                public int INCHARGEEMPLOYEEIDNO
                {
                    get { return _INCHARGEEMPLOYEEIDNO; }
                    set { _INCHARGEEMPLOYEEIDNO = value; }
                }

                public int EMPLOYEEIDNO
                {
                    get { return _EMPLOYEEIDNO; }
                    set { _EMPLOYEEIDNO = value; }
                }

                public int COLLEGE_NO
                {
                    get { return _COLLEGE_NO; }
                    set { _COLLEGE_NO = value; }
                }

                public System.Nullable<System.DateTime> FROMDATE
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

                public System.Nullable<System.DateTime> TODATE
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

                public System.Nullable<System.DateTime> SHIFTDATE
                {
                    get
                    {
                        return this._SHIFTDATE;
                    }
                    set
                    {
                        if ((this._SHIFTDATE != value))
                        {
                            this._SHIFTDATE = value;
                        }
                    }
                }

                #endregion
            }
        }
    }
}
