using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class Pay_FinancialYear
            {
                private int _FINYEARID;
                private DateTime _FROMDATE;
                private DateTime _TODATE;
                private string _COLLEGE_CODE;
                private string _FINANCIAL_YEAR;
                private string _SHORT_NAME;

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

                public int FINYEARID
                {
                    get
                    {
                        return this._FINYEARID;
                    }
                    set
                    {
                        if ((this._FINYEARID != value))
                        {
                            this._FINYEARID = value;
                        }
                    }
                }

                public string FINANCIAL_YEAR
                {
                    get
                    {
                        return this._FINANCIAL_YEAR;
                    }
                    set
                    {
                        if ((this._FINANCIAL_YEAR != value))
                        {
                            this._FINANCIAL_YEAR = value;
                        }
                    }
                }

                public string SHORT_NAME
                {
                    get
                    {
                        return this._SHORT_NAME;
                    }
                    set
                    {
                        if ((this._SHORT_NAME != value))
                        {
                            this._SHORT_NAME = value;
                        }
                    }
                }

            }
        }
    }
}
