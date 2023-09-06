using System;
using System.Data;
using System.Web;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class ITRule
            {

                #region Private Members
                private System.Nullable<int> _ITNO;

                private System.Nullable<decimal> _MINLIMIT;

                private System.Nullable<decimal> _MAXLIMIT;

                private System.Nullable<decimal> _FIXAMT;

                private System.Nullable<decimal> _PERCENTAGE;
                private string _ITRULENAME = string.Empty;
                private string _COLLEGE_CODE = string.Empty;
                private int _STATUS;
                private System.Nullable<int> _ITRULEID;
                private int _COLLEGENO;

                private bool _IsActive;

                public bool IsActive
                {
                    get { return _IsActive; }
                    set { _IsActive = value; }
                }

                private int _SchemeType;
                public int SchemeType
                {
                    get
                    {
                        return _SchemeType;
                    }
                    set
                    {
                        _SchemeType = value;
                    }
                }

                #endregion

                #region Public Properties
                public string COLLEGECODE
                {
                    set
                    {
                        _COLLEGE_CODE = value;
                    }
                    get
                    {
                        return _COLLEGE_CODE;
                    }
                }
                public System.Nullable<int> ITNO
                {
                    get
                    {
                        return this._ITNO;
                    }
                    set
                    {
                        if ((this._ITNO != value))
                        {
                            this._ITNO = value;
                        }
                    }
                }

                public System.Nullable<decimal> MINLIMIT
                {
                    get
                    {
                        return this._MINLIMIT;
                    }
                    set
                    {
                        if ((this._MINLIMIT != value))
                        {
                            this._MINLIMIT = value;
                        }
                    }
                }
                public System.Nullable<decimal> MAXLIMIT
                {
                    get
                    {
                        return this._MAXLIMIT;
                    }
                    set
                    {
                        if ((this._MAXLIMIT != value))
                        {
                            this._MAXLIMIT = value;
                        }
                    }
                }
                public System.Nullable<decimal> FIXAMT
                {
                    get
                    {
                        return this._FIXAMT;
                    }
                    set
                    {
                        if ((this._FIXAMT != value))
                        {
                            this._FIXAMT = value;
                        }
                    }
                }
                public System.Nullable<decimal> PERCENTAGE
                {
                    get
                    {
                        return this._PERCENTAGE;
                    }
                    set
                    {
                        if ((this._PERCENTAGE != value))
                        {
                            this._PERCENTAGE = value;
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
                public int COLLEGENO
                {
                    set
                    {
                        _COLLEGENO = value;
                    }
                    get
                    {
                        return _COLLEGENO;
                    }
                }
                public int STATUS
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

                public string ITRULENAME
                {
                    get
                    {
                        return _ITRULENAME;
                    }
                    set
                    {
                        _ITRULENAME = value;
                    }
                }
                public System.Nullable<int> ITRULEID
                {
                    get
                    {
                        return this._ITRULEID;
                    }
                    set
                    {
                        if ((this._ITRULEID != value))
                        {
                            this._ITRULEID = value;
                        }
                    }
                }
                #endregion
            }
        }
    }
}
