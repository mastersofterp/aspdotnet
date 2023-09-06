//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS ENTITIES FILE //[GPF]                                  
// CREATION DATE : 25-NOV-2009                                                        
// CREATED BY    : KIRAN GVS                                       
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================  
using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class GPF
            {
                #region PF_TRAN
               
                private int _PFTRXNO;

                private int _IDNO;

                private int _STAFFNO;
                
                private string _MONYEAR;

                private System.Nullable<decimal> _H1;

                private System.Nullable<decimal> _H2;

                private System.Nullable<decimal> _H3;

                private System.Nullable<decimal> _H4;

                private System.Nullable<decimal> _OB;

                private System.Nullable<decimal> _LOANBAL;

                private System.Nullable<decimal> _PROGRESSIVEBAL;

                private System.Nullable<System.DateTime> _MONTHDATE;

                private System.Nullable<System.DateTime> _FSDATE;

                private System.Nullable<System.DateTime> _FEDATE;

                private int _PFNO;

                private string _STATUS;

                private string _COLLEGE_CODE;
                private int _COLLEGE_NO;

                private decimal _TRANSFERAMOUNT = 0;
                public int COLLEGE_NO
                {
                    get
                    {
                        return _COLLEGE_NO;
                    }
                    set
                    {
                        _COLLEGE_NO = value;
                    }
                }

                public decimal TRANSFERAMOUNT
                {
                    get { return _TRANSFERAMOUNT; }
                    set { _TRANSFERAMOUNT = value; }
                }
                private DateTime _TRANSFERDATE;

                public DateTime TRANSFERDATE
                {
                    get { return _TRANSFERDATE; }
                    set { _TRANSFERDATE = value; }
                }

                
                //[Column(Storage = "_PFTRXNO", DbType = "Int NOT NULL")]
                public int PFTRXNO
                {
                    get
                    {
                        return this._PFTRXNO;
                    }
                    set
                    {
                        if ((this._PFTRXNO != value))
                        {
                            this._PFTRXNO = value;
                        }
                    }
                }

                //[Column(Storage = "_IDNO", DbType = "Int NOT NULL")]
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


                //[Column(Storage = "_MONYEAR", DbType = "NVarChar(20)")]
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

                //[Column(Storage = "_H1", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> H1
                {
                    get
                    {
                        return this._H1;
                    }
                    set
                    {
                        if ((this._H1 != value))
                        {
                            this._H1 = value;
                        }
                    }
                }

                //[Column(Storage = "_H2", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> H2
                {
                    get
                    {
                        return this._H2;
                    }
                    set
                    {
                        if ((this._H2 != value))
                        {
                            this._H2 = value;
                        }
                    }
                }

                //[Column(Storage = "_H3", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> H3
                {
                    get
                    {
                        return this._H3;
                    }
                    set
                    {
                        if ((this._H3 != value))
                        {
                            this._H3 = value;
                        }
                    }
                }

                //[Column(Storage = "_H4", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> H4
                {
                    get
                    {
                        return this._H4;
                    }
                    set
                    {
                        if ((this._H4 != value))
                        {
                            this._H4 = value;
                        }
                    }
                }

                //[Column(Storage = "_OB", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> OB
                {
                    get
                    {
                        return this._OB;
                    }
                    set
                    {
                        if ((this._OB != value))
                        {
                            this._OB = value;
                        }
                    }
                }

                //[Column(Storage = "_OB", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> LOANBAL
                {
                    get
                    {
                        return this._LOANBAL;
                    }
                    set
                    {
                        if ((this._LOANBAL != value))
                        {
                            this._LOANBAL = value;
                        }
                    }
                }

                //[Column(Storage = "_PROGRESSIVEBAL", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> PROGRESSIVEBAL
                {
                    get
                    {
                        return this._PROGRESSIVEBAL;
                    }
                    set
                    {
                        if ((this._PROGRESSIVEBAL != value))
                        {
                            this._PROGRESSIVEBAL = value;
                        }
                    }
                }


                //[Column(Storage = "_FSDATE", DbType = "DateTime")]
                public System.Nullable<System.DateTime> MONTHDATE
                {
                    get
                    {
                        return this._MONTHDATE;
                    }
                    set
                    {
                        if ((this._MONTHDATE != value))
                        {
                            this._MONTHDATE = value;
                        }
                    }
                }



                //[Column(Storage = "_FSDATE", DbType = "DateTime")]
                public System.Nullable<System.DateTime> FSDATE
                {
                    get
                    {
                        return this._FSDATE;
                    }
                    set
                    {
                        if ((this._FSDATE != value))
                        {
                            this._FSDATE = value;
                        }
                    }
                }

                //[Column(Storage = "_FEDATE", DbType = "DateTime")]
                public System.Nullable<System.DateTime> FEDATE
                {
                    get
                    {
                        return this._FEDATE;
                    }
                    set
                    {
                        if ((this._FEDATE != value))
                        {
                            this._FEDATE = value;
                        }
                    }
                }

                //[Column(Storage = "_GPFCPF", DbType = "NVarChar(10) NOT NULL", CanBeNull = false)]
                public int PFNO
                {
                    get
                    {
                        return this._PFNO;
                    }
                    set
                    {
                        if ((this._PFNO != value))
                        {
                            this._PFNO = value;
                        }
                    }
                }

                //[Column(Storage = "_STATUS", DbType = "NChar(1) NOT NULL")]
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

                //[Column(Storage = "_COLLEGE_CODE", DbType = "NVarChar(15)")]
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
        
        }//END: BusinessLayer.BusinessEntities

    }//END: UAIMS 

 }// END: IITMS
