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
            public class ITHRACALCULATE
            {
                #region Private Members
                private System.Nullable<int> _IDNO;
                private System.Nullable<decimal> _PAY;
                private System.Nullable<decimal> _DA;
                private System.Nullable<decimal> _ACTUALHRA;
                private System.Nullable<decimal> _ROPHRA;
                private System.Nullable<decimal> _HRARECEIVED;
                private System.Nullable<decimal> _PAIDRENT;
                private System.Nullable<decimal> _SAL10PER;
                private System.Nullable<decimal> _RENTPAIDACCESS10PER;
                private System.Nullable<decimal> _SAL40PER;
                private System.Nullable<decimal> _LESSHRA;
                private System.Nullable<decimal> _HRACALCULATE;

                //CHAPTER VA LIMIT
                private System.Nullable<int> _VANO;
                private System.Nullable<decimal> _ACTUALAMT1;
                private System.Nullable<decimal> _ACTUALAMT2;
                private System.Nullable<decimal> _ACTUALAMT3;
                private System.Nullable<decimal> _ACTUALAMT4;
                private System.Nullable<decimal> _ACTUALAMT5;
                private System.Nullable<decimal> _ACTUALAMT6;
                private System.Nullable<decimal> _ACTUALAMT7;
                private System.Nullable<decimal> _ACTUALAMT8;
                private System.Nullable<decimal> _ACTUALAMT9;
                private System.Nullable<decimal> _ACTUALAMT10;
                private System.Nullable<decimal> _ACTUALAMT11;
                private System.Nullable<decimal> _ACTUALAMT12;
                private System.Nullable<decimal> _ACTUALAMT13;
                private System.Nullable<decimal> _LIMIT1;
                private System.Nullable<decimal> _LIMIT2;
                private System.Nullable<decimal> _LIMIT3;
                private System.Nullable<decimal> _LIMIT4;
                private System.Nullable<decimal> _LIMIT5;
                private System.Nullable<decimal> _LIMIT6;
                private System.Nullable<decimal> _LIMIT7;
                private System.Nullable<decimal> _LIMIT8;
                private System.Nullable<decimal> _LIMIT9;
                private System.Nullable<decimal> _LIMIT10;
                private System.Nullable<decimal> _LIMIT11;
                private System.Nullable<decimal> _LIMIT12;
                private System.Nullable<decimal> _LIMIT13;
                private System.Nullable<decimal> _AMOUNT1;
                private System.Nullable<decimal> _AMOUNT2;
                private System.Nullable<decimal> _AMOUNT3;
                private System.Nullable<decimal> _AMOUNT4;
                private System.Nullable<decimal> _AMOUNT5;
                private System.Nullable<decimal> _AMOUNT6;
                private System.Nullable<decimal> _AMOUNT7;
                private System.Nullable<decimal> _AMOUNT8;
                private System.Nullable<decimal> _AMOUNT9;
                private System.Nullable<decimal> _AMOUNT10;
                private System.Nullable<decimal> _AMOUNT11;
                private System.Nullable<decimal> _AMOUNT12;
                private System.Nullable<decimal> _AMOUNT13;
                private string _FINYEAR;

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

                public System.Nullable<decimal> PAY
                {
                    get
                    {
                        return this._PAY;
                    }
                    set
                    {
                        if ((this._PAY != value))
                        {
                            this._PAY = value;
                        }
                    }
                }
                public System.Nullable<decimal> DA
                {
                    get
                    {
                        return this._DA;
                    }
                    set
                    {
                        if ((this._DA != value))
                        {
                            this._DA = value;
                        }
                    }
                }
                public System.Nullable<decimal> ACTUALHRA
                {
                    get
                    {
                        return this._ACTUALHRA;
                    }
                    set
                    {
                        if ((this._ACTUALHRA != value))
                        {
                            this._ACTUALHRA = value;
                        }
                    }
                }
                public System.Nullable<decimal> ROPHRA
                {
                    get
                    {
                        return this._ROPHRA;
                    }
                    set
                    {
                        if ((this._ROPHRA != value))
                        {
                            this._ROPHRA = value;
                        }
                    }
                }
                public System.Nullable<decimal> HRARECEIVED
                {
                    get
                    {
                        return this._HRARECEIVED;
                    }
                    set
                    {
                        if ((this._HRARECEIVED != value))
                        {
                            this._HRARECEIVED = value;
                        }
                    }
                }
                public System.Nullable<decimal> PAIDRENT
                {
                    get
                    {
                        return this._PAIDRENT;
                    }
                    set
                    {
                        if ((this._PAIDRENT != value))
                        {
                            this._PAIDRENT = value;
                        }
                    }
                }
                public System.Nullable<decimal> SAL10PER
                {
                    get
                    {
                        return this._SAL10PER;
                    }
                    set
                    {
                        if ((this._SAL10PER != value))
                        {
                            this._SAL10PER = value;
                        }
                    }
                }
                public System.Nullable<decimal> RENTPAIDACCESS10PER
                {
                    get
                    {
                        return this._RENTPAIDACCESS10PER;
                    }
                    set
                    {
                        if ((this._RENTPAIDACCESS10PER != value))
                        {
                            this._RENTPAIDACCESS10PER = value;
                        }
                    }
                }
                public System.Nullable<decimal> SAL40PER
                {
                    get
                    {
                        return this._SAL40PER;
                    }
                    set
                    {
                        if ((this._SAL40PER != value))
                        {
                            this._SAL40PER = value;
                        }
                    }
                }
                public System.Nullable<decimal> LESSHRA
                {
                    get
                    {
                        return this._LESSHRA;
                    }
                    set
                    {
                        if ((this._LESSHRA != value))
                        {
                            this._LESSHRA = value;
                        }
                    }
                }
                public System.Nullable<decimal> HRACALCULATE
                {
                    get
                    {
                        return this._HRACALCULATE;
                    }
                    set
                    {
                        if ((this._HRACALCULATE != value))
                        {
                            this._HRACALCULATE = value;
                        }
                    }
                }

                public System.Nullable<int> VANO
                {
                    get
                    {
                        return this._VANO;
                    }
                    set
                    {
                        if ((this._VANO != value))
                        {
                            this._VANO = value;
                        }
                    }
                }
                public System.Nullable<decimal> ACTUALAMT1
                {
                    get
                    {
                        return this._ACTUALAMT1;
                    }
                    set
                    {
                        if ((this._ACTUALAMT1 != value))
                        {
                            this._ACTUALAMT1 = value;
                        }
                    }
                }
                public System.Nullable<decimal> ACTUALAMT2
                {
                    get
                    {
                        return this._ACTUALAMT2;
                    }
                    set
                    {
                        if ((this._ACTUALAMT2 != value))
                        {
                            this._ACTUALAMT2 = value;
                        }
                    }
                }
                public System.Nullable<decimal> ACTUALAMT3
                {
                    get
                    {
                        return this._ACTUALAMT3;
                    }
                    set
                    {
                        if ((this._ACTUALAMT3 != value))
                        {
                            this._ACTUALAMT3 = value;
                        }
                    }
                }
                public System.Nullable<decimal> ACTUALAMT4
                {
                    get
                    {
                        return this._ACTUALAMT4;
                    }
                    set
                    {
                        if ((this._ACTUALAMT4 != value))
                        {
                            this._ACTUALAMT4 = value;
                        }
                    }
                }
                public System.Nullable<decimal> ACTUALAMT5
                {
                    get
                    {
                        return this._ACTUALAMT5;
                    }
                    set
                    {
                        if ((this._ACTUALAMT5 != value))
                        {
                            this._ACTUALAMT5 = value;
                        }
                    }
                }
                public System.Nullable<decimal> ACTUALAMT6
                {
                    get
                    {
                        return this._ACTUALAMT6;
                    }
                    set
                    {
                        if ((this._ACTUALAMT6 != value))
                        {
                            this._ACTUALAMT6 = value;
                        }
                    }
                }
                public System.Nullable<decimal> ACTUALAMT7
                {
                    get
                    {
                        return this._ACTUALAMT7;
                    }
                    set
                    {
                        if ((this._ACTUALAMT7 != value))
                        {
                            this._ACTUALAMT7 = value;
                        }
                    }
                }
                public System.Nullable<decimal> ACTUALAMT8
                {
                    get
                    {
                        return this._ACTUALAMT8;
                    }
                    set
                    {
                        if ((this._ACTUALAMT8 != value))
                        {
                            this._ACTUALAMT8 = value;
                        }
                    }
                }
                public System.Nullable<decimal> ACTUALAMT9
                {
                    get
                    {
                        return this._ACTUALAMT9;
                    }
                    set
                    {
                        if ((this._ACTUALAMT9 != value))
                        {
                            this._ACTUALAMT9 = value;
                        }
                    }
                }
                public System.Nullable<decimal> ACTUALAMT10
                {
                    get
                    {
                        return this._ACTUALAMT10;
                    }
                    set
                    {
                        if ((this._ACTUALAMT10 != value))
                        {
                            this._ACTUALAMT10 = value;
                        }
                    }
                }
                public System.Nullable<decimal> ACTUALAMT11
                {
                    get
                    {
                        return this._ACTUALAMT11;
                    }
                    set
                    {
                        if ((this._ACTUALAMT11 != value))
                        {
                            this._ACTUALAMT11 = value;
                        }
                    }
                }
                public System.Nullable<decimal> ACTUALAMT12
                {
                    get
                    {
                        return this._ACTUALAMT12;
                    }
                    set
                    {
                        if ((this._ACTUALAMT12 != value))
                        {
                            this._ACTUALAMT12 = value;
                        }
                    }
                }
                public System.Nullable<decimal> ACTUALAMT13
                {
                    get
                    {
                        return this._ACTUALAMT13;
                    }
                    set
                    {
                        if ((this._ACTUALAMT13 != value))
                        {
                            this._ACTUALAMT13 = value;
                        }
                    }
                }

                public System.Nullable<decimal> LIMIT1
                {
                    get
                    {
                        return this._LIMIT1;
                    }
                    set
                    {
                        if ((this._LIMIT1 != value))
                        {
                            this._LIMIT1 = value;
                        }
                    }
                }
                public System.Nullable<decimal> LIMIT2
                {
                    get
                    {
                        return this._LIMIT2;
                    }
                    set
                    {
                        if ((this._LIMIT2 != value))
                        {
                            this._LIMIT2 = value;
                        }
                    }
                }
                public System.Nullable<decimal> LIMIT3
                {
                    get
                    {
                        return this._LIMIT3;
                    }
                    set
                    {
                        if ((this._LIMIT3 != value))
                        {
                            this._LIMIT3 = value;
                        }
                    }
                }
                public System.Nullable<decimal> LIMIT4
                {
                    get
                    {
                        return this._LIMIT4;
                    }
                    set
                    {
                        if ((this._LIMIT4 != value))
                        {
                            this._LIMIT4 = value;
                        }
                    }
                }
                public System.Nullable<decimal> LIMIT5
                {
                    get
                    {
                        return this._LIMIT5;
                    }
                    set
                    {
                        if ((this._LIMIT5 != value))
                        {
                            this._LIMIT5 = value;
                        }
                    }
                }
                public System.Nullable<decimal> LIMIT6
                {
                    get
                    {
                        return this._LIMIT6;
                    }
                    set
                    {
                        if ((this._LIMIT6 != value))
                        {
                            this._LIMIT6 = value;
                        }
                    }
                }
                public System.Nullable<decimal> LIMIT7
                {
                    get
                    {
                        return this._LIMIT7;
                    }
                    set
                    {
                        if ((this._LIMIT7 != value))
                        {
                            this._LIMIT7 = value;
                        }
                    }
                }
                public System.Nullable<decimal> LIMIT8
                {
                    get
                    {
                        return this._LIMIT8;
                    }
                    set
                    {
                        if ((this._LIMIT8 != value))
                        {
                            this._LIMIT8 = value;
                        }
                    }
                }
                public System.Nullable<decimal> LIMIT9
                {
                    get
                    {
                        return this._LIMIT9;
                    }
                    set
                    {
                        if ((this._LIMIT9 != value))
                        {
                            this._LIMIT9 = value;
                        }
                    }
                }
                public System.Nullable<decimal> LIMIT10
                {
                    get
                    {
                        return this._LIMIT10;
                    }
                    set
                    {
                        if ((this._LIMIT10 != value))
                        {
                            this._LIMIT10 = value;
                        }
                    }
                }
                public System.Nullable<decimal> LIMIT11
                {
                    get
                    {
                        return this._LIMIT11;
                    }
                    set
                    {
                        if ((this._LIMIT11 != value))
                        {
                            this._LIMIT11 = value;
                        }
                    }
                }
                public System.Nullable<decimal> LIMIT12
                {
                    get
                    {
                        return this._LIMIT12;
                    }
                    set
                    {
                        if ((this._LIMIT12 != value))
                        {
                            this._LIMIT12 = value;
                        }
                    }
                }
                public System.Nullable<decimal> LIMIT13
                {
                    get
                    {
                        return this._LIMIT13;
                    }
                    set
                    {
                        if ((this._LIMIT13 != value))
                        {
                            this._LIMIT13 = value;
                        }
                    }
                }

                public System.Nullable<decimal> AMOUNT1
                {
                    get
                    {
                        return this._AMOUNT1;
                    }
                    set
                    {
                        if ((this._AMOUNT1 != value))
                        {
                            this._AMOUNT1 = value;
                        }
                    }
                }
                public System.Nullable<decimal> AMOUNT2
                {
                    get
                    {
                        return this._AMOUNT2;
                    }
                    set
                    {
                        if ((this._AMOUNT2 != value))
                        {
                            this._AMOUNT2 = value;
                        }
                    }
                }
                public System.Nullable<decimal> AMOUNT3
                {
                    get
                    {
                        return this._AMOUNT3;
                    }
                    set
                    {
                        if ((this._AMOUNT3 != value))
                        {
                            this._AMOUNT3 = value;
                        }
                    }
                }
                public System.Nullable<decimal> AMOUNT4
                {
                    get
                    {
                        return this._AMOUNT4;
                    }
                    set
                    {
                        if ((this._AMOUNT4 != value))
                        {
                            this._AMOUNT4 = value;
                        }
                    }
                }
                public System.Nullable<decimal> AMOUNT5
                {
                    get
                    {
                        return this._AMOUNT5;
                    }
                    set
                    {
                        if ((this._AMOUNT5 != value))
                        {
                            this._AMOUNT5 = value;
                        }
                    }
                }
                public System.Nullable<decimal> AMOUNT6
                {
                    get
                    {
                        return this._AMOUNT6;
                    }
                    set
                    {
                        if ((this._AMOUNT6 != value))
                        {
                            this._AMOUNT6 = value;
                        }
                    }
                }
                public System.Nullable<decimal> AMOUNT7
                {
                    get
                    {
                        return this._AMOUNT7;
                    }
                    set
                    {
                        if ((this._AMOUNT7 != value))
                        {
                            this._AMOUNT7 = value;
                        }
                    }
                }
                public System.Nullable<decimal> AMOUNT8
                {
                    get
                    {
                        return this._AMOUNT8;
                    }
                    set
                    {
                        if ((this._AMOUNT8 != value))
                        {
                            this._AMOUNT8 = value;
                        }
                    }
                }
                public System.Nullable<decimal> AMOUNT9
                {
                    get
                    {
                        return this._AMOUNT9;
                    }
                    set
                    {
                        if ((this._AMOUNT9 != value))
                        {
                            this._AMOUNT9 = value;
                        }
                    }
                }
                public System.Nullable<decimal> AMOUNT10
                {
                    get
                    {
                        return this._AMOUNT10;
                    }
                    set
                    {
                        if ((this._AMOUNT10 != value))
                        {
                            this._AMOUNT10 = value;
                        }
                    }
                }
                public System.Nullable<decimal> AMOUNT11
                {
                    get
                    {
                        return this._AMOUNT11;
                    }
                    set
                    {
                        if ((this._AMOUNT11 != value))
                        {
                            this._AMOUNT11 = value;
                        }
                    }
                }
                public System.Nullable<decimal> AMOUNT12
                {
                    get
                    {
                        return this._AMOUNT12;
                    }
                    set
                    {
                        if ((this._AMOUNT12 != value))
                        {
                            this._AMOUNT12 = value;
                        }
                    }
                }
                public System.Nullable<decimal> AMOUNT13
                {
                    get
                    {
                        return this._AMOUNT13;
                    }
                    set
                    {
                        if ((this._AMOUNT13 != value))
                        {
                            this._AMOUNT13 = value;
                        }
                    }
                }

                public string FINYEAR
                {
                    get
                    {
                        return this._FINYEAR;
                    }
                    set
                    {
                        if ((this._FINYEAR != value))
                        {
                            this._FINYEAR = value;
                        }
                    }
                }


                #endregion
            }
        }
    }
}
