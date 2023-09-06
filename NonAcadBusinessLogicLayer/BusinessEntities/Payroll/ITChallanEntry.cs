using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class ITChallanEntry
            {
                #region PrivateMembers
                private System.Nullable<int> _CHIDNO;
                private string _MONYEAR;
                private System.Nullable<System.DateTime> _CHDATE = DateTime.MinValue;
                private string _SUPORDERNO;
                private string _STAFF;
                private string _CHNO;
                private string _CHQDDNO;
                private System.Nullable<decimal> _TAXDEPO;
                private System.Nullable<decimal> _SURCHARGE;
                private System.Nullable<decimal> _EDUCESS;
                private System.Nullable<decimal> _INTEREST;
                private System.Nullable<decimal> _OTHERS;
                private string _BSRCODE;
                private System.Nullable<System.DateTime> _DEDUDATE = DateTime.MinValue;
                private System.Nullable<System.DateTime> _DEPODATE = DateTime.MinValue;
                private string _COLLEGECODE;
                private int _COLLEGENO;
                #endregion

                #region PublicMethods
                public System.Nullable<int> CHIDNO
                {
                    get
                    {
                        return this._CHIDNO;
                    }
                    set
                    {
                        if ((this._CHIDNO != value))
                        {
                            this._CHIDNO = value;
                        }
                    }
                }
                public string MONYEAR
                {
                    get
                    {
                        return this._MONYEAR;
                    }
                    set
                    {
                        if ((this._MONYEAR != value))
                        {
                            this._MONYEAR = value;
                        }
                    }
                }
                public System.Nullable<System.DateTime> CHDATE
                {
                    get
                    {
                        return this._CHDATE;
                    }
                    set
                    {
                        if ((this._CHDATE != value))
                        {
                            this._CHDATE = value;
                        }
                    }

                }
                public string SUPORDERNO
                {
                    get
                    {
                        return this._SUPORDERNO;
                    }
                    set
                    {
                        if ((this._SUPORDERNO != value))
                        {
                            this._SUPORDERNO = value;
                        }
                    }
                }
                public string STAFF
                {
                    get
                    {
                        return this._STAFF;
                    }
                    set
                    {
                        if ((this._STAFF != value))
                        {
                            this._STAFF = value;
                        }
                    }
                }
                public string CHNO
                {
                    get
                    {
                        return this._CHNO;
                    }
                    set
                    {
                        if ((this._CHNO != value))
                        {
                            this._CHNO = value;
                        }
                    }
                }
                public string CHQDDNO
                {
                    get
                    {
                        return this._CHQDDNO;
                    }
                    set
                    {
                        if ((this._CHQDDNO != value))
                        {
                            this._CHQDDNO = value;
                        }
                    }
                }
                public System.Nullable<decimal> TAXDEPO
                {
                    get
                    {
                        return this._TAXDEPO;
                    }
                    set
                    {
                        if ((this._TAXDEPO != value))
                        {
                            this._TAXDEPO = value;
                        }
                    }
                }
                public System.Nullable<decimal> SURCHARGE
                {
                    get
                    {
                        return this._SURCHARGE;
                    }
                    set
                    {
                        if ((this._SURCHARGE != value))
                        {
                            this._SURCHARGE = value;
                        }
                    }
                }
                public System.Nullable<decimal> EDUCESS
                {
                    get
                    {
                        return this._EDUCESS;
                    }
                    set
                    {
                        if ((this._EDUCESS != value))
                        {
                            this._EDUCESS = value;
                        }
                    }
                }
                public System.Nullable<decimal> INTEREST
                {
                    get
                    {
                        return this._INTEREST;
                    }
                    set
                    {
                        if ((this._INTEREST != value))
                        {
                            this._INTEREST = value;
                        }
                    }
                }
                public System.Nullable<decimal> OTHERS
                {
                    get
                    {
                        return this._OTHERS;
                    }
                    set
                    {
                        if ((this._OTHERS != value))
                        {
                            this._OTHERS = value;
                        }
                    }
                }
                public string BSRCODE
                {
                    get
                    {
                        return this._BSRCODE;
                    }
                    set
                    {
                        if ((this._BSRCODE != value))
                        {
                            this._BSRCODE = value;
                        }
                    }
                }
                public System.Nullable<System.DateTime> DEDUDATE
                {
                    get
                    {
                        return this._DEDUDATE;
                    }
                    set
                    {
                        if ((this._DEDUDATE != value))
                        {
                            this._DEDUDATE = value;
                        }
                    }

                }
                public System.Nullable<System.DateTime> DEPODATE
                {
                    get
                    {
                        return this._DEPODATE;
                    }
                    set
                    {
                        if ((this._DEPODATE != value))
                        {
                            this._DEPODATE = value;
                        }
                    }

                }
                public string COLLEGECODE
                {
                    get
                    {
                        return this._COLLEGECODE;
                    }
                    set
                    {
                        if ((this._COLLEGECODE != value))
                        {
                            this._COLLEGECODE = value;
                        }
                    }
                }
                public int COLLEGENO
                {
                    get
                    {
                        return this._COLLEGENO;
                    }
                    set
                    {
                        if ((this._COLLEGENO != value))
                        {
                            this._COLLEGENO = value;
                        }
                    }
                }
                #endregion

            }
        }
    }
}
