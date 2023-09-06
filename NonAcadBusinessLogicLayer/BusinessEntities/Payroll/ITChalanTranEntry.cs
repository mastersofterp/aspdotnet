using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class ITChalanTranEntry
            {
                #region PrivateMembers

                private int _SCHIDNO;
                private int _CHIDNO;
                private int _IDNO;
                private System.Nullable<System.DateTime> _PAYDATE = DateTime.MinValue;
                private decimal _GSAMT;
                private decimal _CHAMT;
                private decimal _CHSCHARGE;
                private decimal _CHEDUCESS;
                private string _COLLEGECODE;



                #endregion

                #region PublicProperties

                public int SCHIDNO
                {
                    get
                    {
                        return this._SCHIDNO;
                    }
                    set
                    {
                        if ((this._SCHIDNO != value))
                        {
                            this._SCHIDNO = value;
                        }
                    }
                }

                public int CHIDNO
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

                public System.Nullable<System.DateTime> PAYDATE
                {
                    get
                    {
                        return this._PAYDATE;
                    }
                    set
                    {
                        if ((this._PAYDATE != value))
                        {
                            this._PAYDATE = value;
                        }
                    }

                }

                public decimal GSAMT
                {
                    get
                    {
                        return this._GSAMT;
                    }
                    set
                    {
                        if ((this._GSAMT != value))
                        {
                            this._GSAMT = value;
                        }
                    }
                }

                public decimal CHAMT
                {
                    get
                    {
                        return this._CHAMT;
                    }
                    set
                    {
                        if ((this._CHAMT != value))
                        {
                            this._CHAMT = value;
                        }
                    }
                }

                public decimal CHSCHARGE
                {
                    get
                    {
                        return this._CHSCHARGE;
                    }
                    set
                    {
                        if ((this._CHSCHARGE != value))
                        {
                            this._CHSCHARGE = value;
                        }
                    }
                }

                public decimal CHEDUCESS
                {
                    get
                    {
                        return this._CHEDUCESS;
                    }
                    set
                    {
                        if ((this._CHEDUCESS != value))
                        {
                            this._CHEDUCESS = value;
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




                #endregion
            }
        }
    }
}
