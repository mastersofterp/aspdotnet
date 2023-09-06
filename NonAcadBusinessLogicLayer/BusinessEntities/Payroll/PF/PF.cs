//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS ENTITIES FILE //[PF]                                  
// CREATION DATE : 07-DEC-2009                                                        
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
            public class PF
            {
                #region PFLOAN
                
                #region PRIVATE
                private int _PFLTNO;
		
                private System.Nullable<int> _IDNO;

                private System.Nullable<System.DateTime> _FDATE;

                private System.Nullable<System.DateTime> _TDATE;

                private System.Nullable<decimal> _ADVAMT;

                private System.Nullable<System.DateTime> _ADVDT;

                private System.Nullable<int> _PFLOANTYPENO;

                private System.Nullable<int> _INSTALMENT;

                private System.Nullable<decimal> _INSTAMT;

                private string _SANCTION;

                private System.Nullable<System.DateTime> _SANDT;

                private string _SANNO;

                private System.Nullable<decimal> _SANAMT;

                private System.Nullable<decimal> _PER;

                private System.Nullable<decimal> _PREBAL;

                private System.Nullable<System.DateTime> _WDT;

                private System.Nullable<decimal> _CURSANAMT;

                private string _COLLEGE_CODE;

                private string _REMARK;
                private int _COLLEGE_NO;

                #endregion

                #region PUBLIC

                //[Column(Storage = "_PFLTNO", DbType = "Int NOT NULL")]
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
                public int PFLTNO
                {
                    get
                    {
                        return this._PFLTNO;
                    }
                    set
                    {
                        if ((this._PFLTNO != value))
                        {
                            this._PFLTNO = value;
                        }
                    }
                }

                //[Column(Storage = "_IDNO", DbType = "Int")]
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

                //[Column(Storage = "_FDATE", DbType = "DateTime")]
                public System.Nullable<System.DateTime> FDATE
                {
                    get
                    {
                        return this._FDATE;
                    }
                    set
                    {
                        if ((this._FDATE != value))
                        {
                            this._FDATE = value;
                        }
                    }
                }

                //[Column(Storage = "_TDATE", DbType = "DateTime")]
                public System.Nullable<System.DateTime> TDATE
                {
                    get
                    {
                        return this._TDATE;
                    }
                    set
                    {
                        if ((this._TDATE != value))
                        {
                            this._TDATE = value;
                        }
                    }
                }

                //[Column(Storage = "_ADVAMT", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> ADVAMT
                {
                    get
                    {
                        return this._ADVAMT;
                    }
                    set
                    {
                        if ((this._ADVAMT != value))
                        {
                            this._ADVAMT = value;
                        }
                    }
                }

                //[Column(Storage = "_ADVDT", DbType = "DateTime")]
                public System.Nullable<System.DateTime> ADVDT
                {
                    get
                    {
                        return this._ADVDT;
                    }
                    set
                    {
                        if ((this._ADVDT != value))
                        {
                            this._ADVDT = value;
                        }
                    }
                }

                //[Column(Storage = "_PFLOANTYPENO", DbType = "Int")]
                public System.Nullable<int> PFLOANTYPENO
                {
                    get
                    {
                        return this._PFLOANTYPENO;
                    }
                    set
                    {
                        if ((this._PFLOANTYPENO != value))
                        {
                            this._PFLOANTYPENO = value;
                        }
                    }
                }

                //[Column(Storage = "_INSTALMENT", DbType = "Int")]
                public System.Nullable<int> INSTALMENT
                {
                    get
                    {
                        return this._INSTALMENT;
                    }
                    set
                    {
                        if ((this._INSTALMENT != value))
                        {
                            this._INSTALMENT = value;
                        }
                    }
                }

                //[Column(Storage = "_INSTAMT", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> INSTAMT
                {
                    get
                    {
                        return this._INSTAMT;
                    }
                    set
                    {
                        if ((this._INSTAMT != value))
                        {
                            this._INSTAMT = value;
                        }
                    }
                }

                //[Column(Storage = "_SANCTION", DbType = "Bit")]
                public string SANCTION
                {
                    get
                    {
                        return this._SANCTION;
                    }
                    set
                    {
                        if ((this._SANCTION != value))
                        {
                            this._SANCTION = value;
                        }
                    }
                }

                public string REMARK
                {
                    get
                    {
                        return this._REMARK;
                    }
                    set
                    {
                        if ((this._REMARK != value))
                        {
                            this._REMARK = value;
                        }
                    }
                }
                //[Column(Storage = "_SANDT", DbType = "DateTime")]
                public System.Nullable<System.DateTime> SANDT
                {
                    get
                    {
                        return this._SANDT;
                    }
                    set
                    {
                        if ((this._SANDT != value))
                        {
                            this._SANDT = value;
                        }
                    }
                }

                //[Column(Storage = "_SANNO", DbType = "NVarChar(50)")]
                public string SANNO
                {
                    get
                    {
                        return this._SANNO;
                    }
                    set
                    {
                        if ((this._SANNO != value))
                        {
                            this._SANNO = value;
                        }
                    }
                }

                //[Column(Storage = "_SANAMT", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> SANAMT
                {
                    get
                    {
                        return this._SANAMT;
                    }
                    set
                    {
                        if ((this._SANAMT != value))
                        {
                            this._SANAMT = value;
                        }
                    }
                }

                //[Column(Storage = "_PER", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> PER
                {
                    get
                    {
                        return this._PER;
                    }
                    set
                    {
                        if ((this._PER != value))
                        {
                            this._PER = value;
                        }
                    }
                }

                //[Column(Storage = "_PREBAL", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> PREBAL
                {
                    get
                    {
                        return this._PREBAL;
                    }
                    set
                    {
                        if ((this._PREBAL != value))
                        {
                            this._PREBAL = value;
                        }
                    }
                }

                //[Column(Storage = "_WDT", DbType = "DateTime")]
                public System.Nullable<System.DateTime> WDT
                {
                    get
                    {
                        return this._WDT;
                    }
                    set
                    {
                        if ((this._WDT != value))
                        {
                            this._WDT = value;
                        }
                    }
                }

                //[Column(Storage = "_CURSANAMT", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> CURSANAMT
                {
                    get
                    {
                        return this._CURSANAMT;
                    }
                    set
                    {
                        if ((this._CURSANAMT != value))
                        {
                            this._CURSANAMT = value;
                        }
                    }
                }

               // [Column(Storage = "_COLLEGE_CODE", DbType = "NVarChar(15)")]
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

                #endregion

                #region PFMASTER

                #region PRIVATE
                
                    private int _PFNO;

                    private string _SHORTNAME;

                    private string _FULLNAME;

                    private string _H1;

                    private string _H2;

                    private string _H3;

                #endregion

                #region PUBLIC
                    //[Column(Storage = "_PFNO", DbType = "Int NOT NULL")]
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

                    //[Column(Storage = "_SHORTNAME", DbType = "NVarChar(20) NOT NULL", CanBeNull = false)]
                    public string SHORTNAME
                    {
                        get
                        {
                            return this._SHORTNAME;
                        }
                        set
                        {
                            if ((this._SHORTNAME != value))
                            {
                                this._SHORTNAME = value;
                            }
                        }
                    }

                    //[Column(Storage = "_FULLNAME", DbType = "NVarChar(100)")]
                    public string FULLNAME
                    {
                        get
                        {
                            return this._FULLNAME;
                        }
                        set
                        {
                            if ((this._FULLNAME != value))
                            {
                                this._FULLNAME = value;
                            }
                        }
                    }

                    //[Column(Storage = "_H1", DbType = "NVarChar(4)")]
                    public string H1
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

                    //[Column(Storage = "_H2", DbType = "NVarChar(4)")]
                    public string H2
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

                    //[Column(Storage = "_H3", DbType = "NVarChar(4)")]
                    public string H3
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
                #endregion

                #endregion

                #region PFLOANTYPE

                #region PRIVATE
    
                    private string _NAME;

                    private System.Nullable<bool> _DEDUCTED;

                    private System.Nullable<decimal> _AMT;

                    private System.Nullable<char> _APP_FOR;

                #endregion

                #region PUBLIC
                   
                    //[Column(Storage = "_NAME", DbType = "NVarChar(50)")]
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

                    //[Column(Storage = "_DEDUCTED", DbType = "Decimal(1,0)")]
                    public System.Nullable<bool> DEDUCTED
                    {
                        get
                        {
                            return this._DEDUCTED;
                        }
                        set
                        {
                            if ((this._DEDUCTED != value))
                            {
                                this._DEDUCTED = value;
                            }
                        }
                    }

                    //[Column(Storage = "_AMT", DbType = "Decimal(12,2)")]
                    public System.Nullable<decimal> AMT
                    {
                        get
                        {
                            return this._AMT;
                        }
                        set
                        {
                            if ((this._AMT != value))
                            {
                                this._AMT = value;
                            }
                        }
                    }

                    //[Column(Storage = "_APP_FOR", DbType = "NChar(1)")]
                    public System.Nullable<char> APP_FOR
                    {
                        get
                        {
                            return this._APP_FOR;
                        }
                        set
                        {
                            if ((this._APP_FOR != value))
                            {
                                this._APP_FOR = value;
                            }
                        }
                    }
                #endregion

                #endregion
            }
        }
    }
}
