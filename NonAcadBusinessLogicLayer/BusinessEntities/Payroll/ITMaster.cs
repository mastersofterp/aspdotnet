using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class ITMaster
            {
                #region Private Members
                private System.Nullable<int> _IDNO;

                private System.Nullable<decimal> _M1=0.0m;

                private System.Nullable<decimal> _M2 = 0.0m;

                private System.Nullable<decimal> _M3 = 0.0m;

                private System.Nullable<decimal> _M4 = 0.0m;

                private System.Nullable<decimal> _M5 = 0.0m;

                private System.Nullable<decimal> _M6 = 0.0m;

                private System.Nullable<decimal> _M7 = 0.0m;

                private System.Nullable<decimal> _M8 = 0.0m;

                private System.Nullable<decimal> _M9 = 0.0m;

                private System.Nullable<decimal> _M10 = 0.0m;

                private System.Nullable<decimal> _M11 = 0.0m;

                private System.Nullable<decimal> _M12 = 0.0m;

                private string _COLLEGE_CODE;
                #endregion

                #region Public Properties
                public System.Nullable<int> IDNO
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

                public System.Nullable<decimal> M1
                {
                    get
                    {
                        return this._M1;
                    }
                    set
                    {
                        if ((this._M1 != value))
                        {
                            this._M1 = value;
                        }
                    }
                }

                public System.Nullable<decimal> M2
                {
                    get
                    {
                        return this._M2;
                    }
                    set
                    {
                        if ((this._M2 != value))
                        {
                            this._M2 = value;
                        }
                    }
                }

                public System.Nullable<decimal> M3
                {
                    get
                    {
                        return this._M3;
                    }
                    set
                    {
                        if ((this._M3 != value))
                        {
                            this._M3 = value;
                        }
                    }
                }

                public System.Nullable<decimal> M4
                {
                    get
                    {
                        return this._M4;
                    }
                    set
                    {
                        if ((this._M4 != value))
                        {
                            this._M4 = value;
                        }
                    }
                }

                public System.Nullable<decimal> M5
                {
                    get
                    {
                        return this._M5;
                    }
                    set
                    {
                        if ((this._M5 != value))
                        {
                            this._M5 = value;
                        }
                    }
                }

                public System.Nullable<decimal> M6
                {
                    get
                    {
                        return this._M6;
                    }
                    set
                    {
                        if ((this._M6 != value))
                        {
                            this._M6 = value;
                        }
                    }
                }

                public System.Nullable<decimal> M7
                {
                    get
                    {
                        return this._M7;
                    }
                    set
                    {
                        if ((this._M7 != value))
                        {
                            this._M7 = value;
                        }
                    }
                }

                public System.Nullable<decimal> M8
                {
                    get
                    {
                        return this._M8;
                    }
                    set
                    {
                        if ((this._M8 != value))
                        {
                            this._M8 = value;
                        }
                    }
                }

                public System.Nullable<decimal> M9
                {
                    get
                    {
                        return this._M9;
                    }
                    set
                    {
                        if ((this._M9 != value))
                        {
                            this._M9 = value;
                        }
                    }
                }

                public System.Nullable<decimal> M10
                {
                    get
                    {
                        return this._M10;
                    }
                    set
                    {
                        if ((this._M10 != value))
                        {
                            this._M10 = value;
                        }
                    }
                }

                public System.Nullable<decimal> M11
                {
                    get
                    {
                        return this._M11;
                    }
                    set
                    {
                        if ((this._M11 != value))
                        {
                            this._M11 = value;
                        }
                    }
                }

                public System.Nullable<decimal> M12
                {
                    get
                    {
                        return this._M12;
                    }
                    set
                    {
                        if ((this._M12 != value))
                        {
                            this._M12 = value;
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

                #endregion
            }
        } //END: BusinessLayer.BusinessEntities
    } //END: UAIMS  
} //END: IITMS