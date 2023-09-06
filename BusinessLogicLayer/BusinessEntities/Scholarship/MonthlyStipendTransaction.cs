using System;
using System.Data;
using System.Web;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class MonthlyStipendTransaction
            {
                #region Private Members
                private System.Nullable<int> _tranno=0;
                private string _trannostr = string.Empty;
                private System.Nullable<int> _idno=0;
                private System.Nullable<System.DateTime> _tranmonthfrom=DateTime.Now ;
                private System.Nullable<decimal> _tranamt=0;
                private System.Nullable<decimal> _arrearamt=0;
                private System.Nullable<System.DateTime> _trandate=DateTime.Now ;
                private System.Nullable<System.DateTime> _tranmonthto=DateTime.Now ;
                private System.Nullable<System.DateTime> _arreardate=DateTime.Now ;
                private System.Nullable<System.DateTime> _arreardate_to=DateTime.Now ;
                private System.Nullable<System.DateTime> _arrearfromdate = DateTime.Now;
                private System.Nullable<System.DateTime> _arreartodate = DateTime.Now;
                private string _arrear_remark=string.Empty ;
                private string _monname=string.Empty ;
                private string _transt=string.Empty ;
                private string _remark=string.Empty ;
                private System.Nullable<int> _querytype = 0;
               

                #endregion

                #region publicproperties
                public System.Nullable<int> TRANNO
                {
                    get
                    {
                        return this._tranno;
                    }
                    set
                    {
                        if ((this._tranno != value))
                        {
                            this._tranno = value;
                        }
                    }
                }
                public string TRANNOSTR
                {
                    get
                    {
                        return this._trannostr;
                    }
                    set
                    {
                        this._trannostr = value;
                    }
                }
                public System.Nullable<int>IDNO
                {
                    get
                    {
                        return this._idno; 
                    }
                    set
                    {
                        if (this._idno!= value)
                        {
                            this._idno = value;
                        }
                    }
                }
                
                public System.Nullable<System.DateTime> TranmonthFrom
                {
                    get
                    {
                        return this._tranmonthfrom;
                    }
                    set
                    {
                        if(this._tranmonthfrom !=value )
                        {
                            this._tranmonthfrom =value ;
                        }
                    }
                }
                public System.Nullable<System.DateTime> TranmonthTo
                {
                    get
                    {
                        return this._tranmonthto;
                    }
                    set
                    {
                        if (this._tranmonthto != value)
                        {
                            this._tranmonthto = value;
                        }
                    }
                }
                public string  MonthName
                {
                    get
                    {
                        return this._monname;
                    }
                    set
                    {
                            this._monname = value;
                    }
                }
                public System.Nullable<System.Decimal> TranAmt
                {
                    get
                    {
                        return this._tranamt;
                    }
                    set
                    {
                        if (this._tranamt != value)
                        {
                            this._tranamt = value;
                        }
                    }
                }
                public System.Nullable<System.Decimal> ArrearAmt
                {
                    get
                    {
                        return this._arrearamt;
                    }
                    set
                    {
                        if (this._arrearamt != value)
                        {
                            this._arrearamt = value;
                        }
                    }
                }
                public string  TranST
                {
                    get
                    {
                        return this._transt;
                    }
                    set
                    {
                        this._transt = value;
                    }
                }
                public string Remark
                {
                    get
                    {
                        return this._remark;
                    }
                    set
                    {
                        this._remark = value;
                    }
                }

                public System.Nullable<System.DateTime> ArrearDate
                {
                    get
                    {
                        return this._arreardate;
                    }
                    set
                    {
                        if (this._arreardate != value)
                        {
                            this._arreardate = value;
                        }
                    }
                }
                public System.Nullable<System.DateTime> ArrearFromDate
                {
                    get
                    {
                        return this._arrearfromdate;
                    }
                    set
                    {
                        if (this._arrearfromdate != value)
                        {
                            this._arrearfromdate = value;
                        }
                    }
                }
                public System.Nullable<System.DateTime> ArrearToDate
                {
                    get
                    {
                        return this._arreartodate;
                    }
                    set
                    {
                        if (this._arreartodate != value)
                        {
                            this._arreartodate = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> TranDate
                {
                    get
                    {
                        return this._trandate;
                    }
                    set
                    {
                        if (this._trandate != value)
                        {
                            this._trandate = value;
                        }
                    }
                }
                public string Arrear_Remark
                {
                    get
                    {
                        return this._arrear_remark;
                    }
                    set
                    {
                        this._arrear_remark = value;
                    }
                }
                public System.Nullable<int> QueryType
                {
                    get
                    {
                        return this._querytype;
                    }
                    set
                    {
                        if (this._querytype != value)
                        {
                            this._querytype = value;
                        }
                    }
                }
                #endregion
            }
        }
    }
}
