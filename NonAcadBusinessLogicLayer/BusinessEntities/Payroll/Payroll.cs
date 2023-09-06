//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS ENTITIES FILE //[PAY ROLL]                                  
// CREATION DATE : 07-MAY-2009                                                        
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
            public class Payroll
            {
                #region Private Members
                //Payroll_Scale
                private int _scaleno = 0;
                private int _b1 = 0;
                private int _i1 = 0;
                private int _b2 = 0;
                private int _i2 = 0;
                private int _b3 = 0;
                private int _i3 = 0;
                private int _b4 = 0;
                private int _i4 = 0;
                private int _b5 = 0;
                private int _i5 = 0;
                private int _b6 = 0;
                private int _i6 = 0;
                private int _b7 = 0;
                private int _i7 = 0;
                private int _b8 = 0;
                private int _i8 = 0;
                private int _b9 = 0;
                private int _i9 = 0;
                private int _b10 = 0;
                private int _i10 = 0;
                private int _b11 = 0;

                private string _scale = string.Empty;
                private double _gradepay = 0;
                private int _ruleno = 0;
                private double _scalerange = 0;
                private string _collegecode = string.Empty;

                //Payroll_PAYHEAD
                private int _srno = 0;
                private string _payshort = string.Empty;
                private string _payfull = string.Empty;
                private string _type = string.Empty;
                private string _calon = string.Empty;
                private string _formula = string.Empty;

                //20-02-2023
                private System.Nullable<bool> _Isclearamount;

                //Payroll_clapay
                private int _calno = 0;
                private string _payhead = string.Empty;
                private int _typecalpay = 0;
                private double _bmin = 0;
                private double _bmax = 0;
                private double _per = 0;
                private double _min = 0;
                private double _max = 0;
                private double _fix = 0;
                private string _payrule = string.Empty;
                private DateTime _fdt;
                private DateTime _tdt;

                ////[Table(Name = "pay.PAY_LIC_INSTALMENT")]
                private System.Nullable<int> _INO;
                private System.Nullable<int> _IDNO;
                private string _PAYHEAD;
                private string _CODE;
                private System.Nullable<int> _BANKNO;
                private System.Nullable<int> _BANKCITYNO;

                private System.Nullable<int> _policyno;
                private System.Nullable<int> _INSTALNO;
                private System.Nullable<decimal> _MONAMT;
                private System.Nullable<decimal> _TOTAMT;
                private System.Nullable<decimal> _BAL_AMT;
                private System.Nullable<decimal> _EMP_CONAMT;
                private System.Nullable<bool> _STOP;
                private System.Nullable<System.DateTime> _DRAWN_DATE;
                private System.Nullable<System.DateTime> _START_DT;
                private System.Nullable<System.DateTime> _EXPDT;
                private System.Nullable<int> _PAIDNO;
                private string _MON;
                private System.Nullable<bool> _NEW;
                private string _ACCNO;
                private string _REF_NO;
                private string _DESP_NO;
                private System.Nullable<System.DateTime> _DESP_DT;
                private System.Nullable<decimal> _DEFA_AMT;
                private System.Nullable<decimal> _PRO_AMT;
                private System.Nullable<int> _SUBHEADNO;
                private System.Nullable<bool> _STOP1;
                private System.Nullable<bool> _REGULAR;
                private System.Nullable<int> _LTNO;
                private string _REMARK;
                private string _COLLEGE_CODE;


                private int _STATUSNO;
                private string _STATUS;

                private int _COLLEGENO;
                private int _STAFFNO;




                private int _BankAccMappingId;
                private int _SCHEMENO;
                private int _COLLEGE_ID;
                //private int _STAFFNO;
                //private int _BANKNO;
                private string _BANK_ACCNO;
                private string _PFILENO = string.Empty;


                private string _PAYSHORT_KANNADA;
                private string _PAYSHORTNAME;

                #endregion

                #region Public Property Fields

                //[Column(Storage = "_MONAMT", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> EMP_CONAMT
                {
                    get
                    {
                        return this._EMP_CONAMT;
                    }
                    set
                    {
                        if ((this._EMP_CONAMT != value))
                        {
                            this._EMP_CONAMT = value;
                        }
                    }
                }

                //[Column(Storage = "_INO", DbType = "Int")]
                public System.Nullable<int> INO
                {
                    get
                    {
                        return this._INO;
                    }
                    set
                    {
                        if ((this._INO != value))
                        {
                            this._INO = value;
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

                //[Column(Storage = "_PAYHEAD", DbType = "NVarChar(3)")]
                public string PAYHEAD
                {
                    get
                    {
                        return this._PAYHEAD;
                    }
                    set
                    {
                        if ((this._PAYHEAD != value))
                        {
                            this._PAYHEAD = value;
                        }
                    }
                }

                public string PFILENO
                {
                    get
                    {
                        return this._PFILENO;
                    }
                    set
                    {
                        if ((this._PFILENO != value))
                        {
                            this._PFILENO = value;
                        }
                    }
                }
                //[Column(Storage = "_CODE", DbType = "NVarChar(20)")]
                public string CODE
                {
                    get
                    {
                        return this._CODE;
                    }
                    set
                    {
                        if ((this._CODE != value))
                        {
                            this._CODE = value;
                        }
                    }
                }

                //[Column(Storage = "_policyno", DbType = "Int")]
                public System.Nullable<int> policyno
                {
                    get
                    {
                        return this._policyno;
                    }
                    set
                    {
                        if ((this._policyno != value))
                        {
                            this._policyno = value;
                        }
                    }
                }

                //[Column(Storage = "_BANKNO", DbType = "Int")]
                public System.Nullable<int> BANKNO
                {
                    get
                    {
                        return this._BANKNO;
                    }
                    set
                    {
                        if ((this._BANKNO != value))
                        {
                            this._BANKNO = value;
                        }
                    }
                }


                public System.Nullable<int> BANKCITYNO
                {
                    get
                    {
                        return this._BANKCITYNO;
                    }
                    set
                    {
                        if ((this._BANKCITYNO != value))
                        {
                            this._BANKCITYNO = value;
                        }
                    }
                }


                //[Column(Storage = "_INSTALNO", DbType = "Int")]
                public System.Nullable<int> INSTALNO
                {
                    get
                    {
                        return this._INSTALNO;
                    }
                    set
                    {
                        if ((this._INSTALNO != value))
                        {
                            this._INSTALNO = value;
                        }
                    }
                }


                //[Column(Storage = "_MONAMT", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> MONAMT
                {
                    get
                    {
                        return this._MONAMT;
                    }
                    set
                    {
                        if ((this._MONAMT != value))
                        {
                            this._MONAMT = value;
                        }
                    }
                }

                //[Column(Storage = "_TOTAMT", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> TOTAMT
                {
                    get
                    {
                        return this._TOTAMT;
                    }
                    set
                    {
                        if ((this._TOTAMT != value))
                        {
                            this._TOTAMT = value;
                        }
                    }
                }

                //[Column(Storage = "_BAL_AMT", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> BAL_AMT
                {
                    get
                    {
                        return this._BAL_AMT;
                    }
                    set
                    {
                        if ((this._BAL_AMT != value))
                        {
                            this._BAL_AMT = value;
                        }
                    }
                }

                //[Column(Storage = "_STOP", DbType = "Bit")]
                public System.Nullable<bool> STOP
                {
                    get
                    {
                        return this._STOP;
                    }
                    set
                    {
                        if ((this._STOP != value))
                        {
                            this._STOP = value;
                        }
                    }
                }



                //[Column(Storage = "_START_DT", DbType = "DateTime")]
                public System.Nullable<System.DateTime> DRAWN_DATE
                {
                    get
                    {
                        return this._DRAWN_DATE;
                    }
                    set
                    {
                        if ((this._DRAWN_DATE != value))
                        {
                            this._DRAWN_DATE = value;
                        }
                    }
                }

                //[Column(Storage = "_START_DT", DbType = "DateTime")]
                public System.Nullable<System.DateTime> START_DT
                {
                    get
                    {
                        return this._START_DT;
                    }
                    set
                    {
                        if ((this._START_DT != value))
                        {
                            this._START_DT = value;
                        }
                    }
                }

                //[Column(Storage = "_EXPDT", DbType = "DateTime")]
                public System.Nullable<System.DateTime> EXPDT
                {
                    get
                    {
                        return this._EXPDT;
                    }
                    set
                    {
                        if ((this._EXPDT != value))
                        {
                            this._EXPDT = value;
                        }
                    }
                }

                //[Column(Storage = "_PAIDNO", DbType = "Int")]
                public System.Nullable<int> PAIDNO
                {
                    get
                    {
                        return this._PAIDNO;
                    }
                    set
                    {
                        if ((this._PAIDNO != value))
                        {
                            this._PAIDNO = value;
                        }
                    }
                }

                //[Column(Storage = "_MON", DbType = "NVarChar(10)")]
                public string MON
                {
                    get
                    {
                        return this._MON;
                    }
                    set
                    {
                        if ((this._MON != value))
                        {
                            this._MON = value;
                        }
                    }
                }

                //[Column(Storage = "_NEW", DbType = "Bit")]
                public System.Nullable<bool> NEW
                {
                    get
                    {
                        return this._NEW;
                    }
                    set
                    {
                        if ((this._NEW != value))
                        {
                            this._NEW = value;
                        }
                    }
                }

                //[Column(Storage = "_ACCNO", DbType = "NVarChar(30)")]
                public string ACCNO
                {
                    get
                    {
                        return this._ACCNO;
                    }
                    set
                    {
                        if ((this._ACCNO != value))
                        {
                            this._ACCNO = value;
                        }
                    }
                }

                //[Column(Storage = "_REF_NO", DbType = "NVarChar(35)")]
                public string REF_NO
                {
                    get
                    {
                        return this._REF_NO;
                    }
                    set
                    {
                        if ((this._REF_NO != value))
                        {
                            this._REF_NO = value;
                        }
                    }
                }

                //[Column(Storage = "_DESP_NO", DbType = "NVarChar(35)")]
                public string DESP_NO
                {
                    get
                    {
                        return this._DESP_NO;
                    }
                    set
                    {
                        if ((this._DESP_NO != value))
                        {
                            this._DESP_NO = value;
                        }
                    }
                }

                //[Column(Storage = "_DESP_DT", DbType = "DateTime")]
                public System.Nullable<System.DateTime> DESP_DT
                {
                    get
                    {
                        return this._DESP_DT;
                    }
                    set
                    {
                        if ((this._DESP_DT != value))
                        {
                            this._DESP_DT = value;
                        }
                    }
                }

                //[Column(Storage = "_DEFA_AMT", DbType = "Decimal(10,2)")]
                public System.Nullable<decimal> DEFA_AMT
                {
                    get
                    {
                        return this._DEFA_AMT;
                    }
                    set
                    {
                        if ((this._DEFA_AMT != value))
                        {
                            this._DEFA_AMT = value;
                        }
                    }
                }

                //[Column(Storage = "_PRO_AMT", DbType = "Decimal(10,2)")]
                public System.Nullable<decimal> PRO_AMT
                {
                    get
                    {
                        return this._PRO_AMT;
                    }
                    set
                    {
                        if ((this._PRO_AMT != value))
                        {
                            this._PRO_AMT = value;
                        }
                    }
                }

                //[Column(Storage = "_IBNO", DbType = "Decimal(5,0)")]
                public System.Nullable<int> SUBHEADNO
                {
                    get
                    {
                        return this._SUBHEADNO;
                    }
                    set
                    {
                        if ((this._SUBHEADNO != value))
                        {
                            this._SUBHEADNO = value;
                        }
                    }
                }

                //[Column(Storage = "_STOP1", DbType = "Bit")]
                public System.Nullable<bool> STOP1
                {
                    get
                    {
                        return this._STOP1;
                    }
                    set
                    {
                        if ((this._STOP1 != value))
                        {
                            this._STOP1 = value;
                        }
                    }
                }

                //[Column(Storage = "_REGULAR", DbType = "Bit")]
                public System.Nullable<bool> REGULAR
                {
                    get
                    {
                        return this._REGULAR;
                    }
                    set
                    {
                        if ((this._REGULAR != value))
                        {
                            this._REGULAR = value;
                        }
                    }
                }

                //[Column(Storage = "_LTNO", DbType = "Bit")]
                public System.Nullable<int> LTNO
                {
                    get
                    {
                        return this._LTNO;
                    }
                    set
                    {
                        if ((this._LTNO != value))
                        {
                            this._LTNO = value;
                        }
                    }
                }

                //[Column(Storage = "_REMARK", DbType = "NVarChar(200)")]
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




                //Payroll_Scale
                public int ScaleNo
                {
                    get { return _scaleno; }
                    set { _scaleno = value; }
                }

                public int B1
                {
                    get { return _b1; }
                    set { _b1 = value; }
                }

                public int I1
                {
                    get { return _i1; }
                    set { _i1 = value; }
                }

                public int B2
                {
                    get { return _b2; }
                    set { _b2 = value; }
                }

                public int I2
                {
                    get { return _i2; }
                    set { _i2 = value; }
                }

                public int B3
                {
                    get { return _b3; }
                    set { _b3 = value; }
                }

                public int I3
                {
                    get { return _i3; }
                    set { _i3 = value; }
                }

                public int B4
                {
                    get { return _b4; }
                    set { _b4 = value; }
                }

                public int I4
                {
                    get { return _i4; }
                    set { _i4 = value; }

                }

                public int B5
                {
                    get { return _b5; }
                    set { _b5 = value; }
                }

                public int I5
                {
                    get { return _i5; }
                    set { _i5 = value; }
                }

                public int B6
                {
                    get { return _b6; }
                    set { _b6 = value; }
                }

                public int I6
                {
                    get { return _i6; }
                    set { _i6 = value; }
                }

                public int B7
                {
                    get { return _b7; }
                    set { _b7 = value; }
                }

                public int I7
                {
                    get { return _i7; }
                    set { _i7 = value; }
                }

                public int B8
                {
                    get { return _b8; }
                    set { _b8 = value; }
                }

                public int I8
                {
                    get { return _i8; }
                    set { _i8= value; }
                }

                public int B9
                {
                    get { return _b9; }
                    set { _b9 = value; }
                }


                public int I9
                {
                    get { return _i9; }
                    set { _i9 = value; }
                }


                public int B10
                {
                    get { return _b10; }
                    set { _b10 = value; }
                }

                public int I10
                {
                    get { return _i10; }
                    set { _i10 = value; }
                }

                public int B11
                {
                    get { return _b11; }
                    set { _b11 = value; }
                }


                public string Scale
                {
                    get { return _scale; }
                    set { _scale = value; }
                }

                public double GradePay
                {
                    get { return _gradepay; }
                    set { _gradepay = value; }
                }

                public int RuleNo
                {
                    get { return _ruleno; }
                    set { _ruleno = value; }
                }

                public double ScaleRange
                {
                    get { return _scalerange; }
                    set { _scalerange = value; }
                }

                public string CollegeCode
                {
                    get { return _collegecode; }
                    set { _collegecode = value; }
                }


                //Payroll_PAYHEAD
                public int Srno
                {
                    get { return _srno; }
                    set { _srno = value; }
                }

                public string PayShort
                {
                    get { return _payshort; }
                    set { _payshort = value; }
                }

                public string PayFull
                {
                    get { return _payfull; }
                    set { _payfull = value; }
                }

                public string Type
                {
                    get { return _type; }
                    set { _type = value; }
                }

                public string CalOn
                {
                    get { return _calon; }
                    set { _calon = value; }
                }

                public string Formula
                {
                    get { return _formula; }
                    set { _formula = value; }
                }


                // 20-02-2023 
                public System.Nullable<bool> Isclearamount
                {
                    get
                    {
                        return this._Isclearamount;
                    }
                    set
                    {
                        if ((this._Isclearamount != value))
                        {
                            this._Isclearamount = value;
                        }
                    }
                }



                //Payroll_calpay

                public int Calno
                {
                    get { return _calno; }
                    set { _calno = value; }
                }

                public string Payhead
                {
                    get { return _payhead; }
                    set { _payhead = value; }
                }

                public int TypePayCal
                {
                    get { return _typecalpay; }
                    set { _typecalpay = value; }
                }

                public double Bmin
                {
                    get { return _bmin; }
                    set { _bmin = value; }
                }

                public double Bmax
                {
                    get { return _bmax; }
                    set { _bmax = value; }
                }

                public double Per
                {
                    get { return _per; }
                    set { _per = value; }
                }

                public double Min
                {
                    get { return _min; }
                    set { _min = value; }
                }

                public double Max
                {
                    get { return _max; }
                    set { _max = value; }
                }

                public double Fix
                {
                    get { return _fix; }
                    set { _fix = value; }
                }

                public string Payrule
                {
                    get { return _payrule; }
                    set { _payrule = value; }
                }

                public DateTime Fdt
                {
                    get { return _fdt; }
                    set { _fdt = value; }
                }

                public DateTime Tdt
                {
                    get { return _tdt; }
                    set { _tdt = value; }
                }
                public int STATUSNO
                {
                    get
                    {
                        return this._STATUSNO;
                    }
                    set
                    {
                        if ((this._STATUSNO != value))
                        {
                            this._STATUSNO = value;
                        }
                    }
                }

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




                public int BankAccMappingId
                {
                    get
                    {
                        return this._BankAccMappingId;
                    }
                    set
                    {
                        if ((this._BankAccMappingId != value))
                        {
                            this._BankAccMappingId = value;
                        }
                    }
                }


                public int SCHEMENO
                {
                    get
                    {
                        return this._SCHEMENO;
                    }
                    set
                    {
                        if ((this._SCHEMENO != value))
                        {
                            this._SCHEMENO = value;
                        }
                    }
                }

                public int COLLEGE_ID
                {
                    get
                    {
                        return this._COLLEGE_ID;
                    }
                    set
                    {
                        if ((this._COLLEGE_ID != value))
                        {
                            this._COLLEGE_ID = value;
                        }
                    }
                }


                public string BANK_ACCNO
                {
                    get
                    {
                        return this._BANK_ACCNO;
                    }
                    set
                    {
                        if ((this._BANK_ACCNO != value))
                        {
                            this._BANK_ACCNO = value;
                        }
                    }
                }


                public string PAYSHORT_KANNADA
                {
                    get
                    {
                        return this._PAYSHORT_KANNADA;
                    }
                    set
                    {
                        if ((this._PAYSHORT_KANNADA != value))
                        {
                            this._PAYSHORT_KANNADA = value;
                        }
                    }
                }

                #endregion


                private int _ARNO = 0;
                private DateTime _AFRM;
                private DateTime _ATO;
                private int _SR_NO = 0;
                private string _GOVORDNO;
                private DateTime _GOVORDDT;
                private string _OFFORDNO;
                private DateTime _OFFORDDT;
                //Pay Rule

                private string _RULENAME;
                private System.Nullable<bool> _IsR7;

                public int SR_NO
                {
                    get { return _SR_NO; }
                    set { _SR_NO = value; }
                }
                public int ARNO
                {
                    get { return _ARNO; }
                    set { _ARNO = value; }
                }

                public DateTime AFRM
                {
                    get { return _AFRM; }
                    set { _AFRM = value; }
                }

                public DateTime ATO
                {
                    get { return _ATO; }
                    set { _ATO = value; }
                }
               

                public string GOVORDNO
                {
                    get { return _GOVORDNO; }
                    set { _GOVORDNO = value; }
                }

                public DateTime GOVORDDT
                {
                    get { return _GOVORDDT; }
                    set { _GOVORDDT = value; }
                }

                public string OFFORDNO
                {
                    get { return _OFFORDNO; }
                    set { _OFFORDNO = value; }
                }

                public DateTime OFFORDDT
                {
                    get { return _OFFORDDT; }
                    set { _OFFORDDT = value; }
                }


                public string PAYSHORTNAME
                {
                    get { return _PAYSHORTNAME; }
                    set { _PAYSHORTNAME = value; }
                }

                public System.Nullable<bool> IsR7
                {
                    get
                    {
                        return this._IsR7;
                    }
                    set
                    {
                        if ((this._IsR7 != value))
                        {
                            this._IsR7 = value;
                        }
                    }
                }



                public string RULENAME
                {
                    get { return this._RULENAME; }
                    set
                    {
                        if ((this._RULENAME != value))
                        {
                            this._RULENAME = value;
                        }
                    }
                }

                #region SLAB MASTER
                //Added By Vidisha On 05-01-2020
                private int _PTSLABID = 0;
                private string _SLAB_NAME = string.Empty;
                private double _FROM_SLAB = 0;
                private double _TO_SLAB = 0;
                private double _AMOUNT = 0;
                //end
                #endregion

                #region SLAB MASTER
                //Added By Vidisha On 05-01-2020                
                public string SLAB_NAME
                {
                    get { return _SLAB_NAME; }
                    set { _SLAB_NAME = value; }
                }

                public int PTSLABID
                {
                    get { return _PTSLABID; }
                    set { _PTSLABID = value; }
                }

                public double FROM_SLAB
                {
                    get { return _FROM_SLAB; }
                    set { _FROM_SLAB = value; }
                }

                public double TO_SLAB
                {
                    get { return _TO_SLAB; }
                    set { _TO_SLAB = value; }
                }

                public double AMOUNT
                {
                    get { return _AMOUNT; }
                    set { _AMOUNT = value; }
                }
                //end
                #endregion
            }//end class payroll

        }//end namespace  BusinessLogicLayer.BusinessEntities 

    }//end namespace UAIMS

}//end namespace IITMS
