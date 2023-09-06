using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class Pay_Supplimentary_Bill
            {
               
                private System.Nullable<int> _SUPLTRXID;

                private System.Nullable<int> _SUPLHNO;

                private string _SUPLHEAD;

                private System.Nullable<char> _SUPLSTATUS;

                private string _MONYEAR;

                private System.Nullable<int> _IDNO;

                private string _ORDNO;

                private System.Nullable<System.DateTime> _SBDATE;

                private System.Nullable<bool> _ADDIT;

                private System.Nullable<bool> _ADDOTHINCOME;

                private System.Nullable<char> _PSTATUS;

                private System.Nullable<int> _APPOINTNO;

                private System.Nullable<int> _SCALENO;

                private System.Nullable<int> _STAFFNO;

                private System.Nullable<decimal> _SUPLIPAY;

                private System.Nullable<decimal> _I1;

                private System.Nullable<decimal> _I2;

                private System.Nullable<decimal> _I3;

                private System.Nullable<decimal> _I4;

                private System.Nullable<decimal> _I5;

                private System.Nullable<decimal> _I6;

                private System.Nullable<decimal> _I7;

                private System.Nullable<decimal> _I8;

                private System.Nullable<decimal> _I9;

                private System.Nullable<decimal> _I10;

                private System.Nullable<decimal> _I11;

                private System.Nullable<decimal> _I12;

                private System.Nullable<decimal> _I13;

                private System.Nullable<decimal> _I14;

                private System.Nullable<decimal> _I15;

                private System.Nullable<decimal> _GS;

                private System.Nullable<decimal> _GS1;

                private System.Nullable<decimal> _D1;

                private System.Nullable<decimal> _D2;

                private System.Nullable<decimal> _D3;

                private System.Nullable<decimal> _D4;

                private System.Nullable<decimal> _D5;

                private System.Nullable<decimal> _D6;

                private System.Nullable<decimal> _D7;

                private System.Nullable<decimal> _D8;

                private System.Nullable<decimal> _D9;

                private System.Nullable<decimal> _D10;

                private System.Nullable<decimal> _D11;

                private System.Nullable<decimal> _D12;

                private System.Nullable<decimal> _D13;

                private System.Nullable<decimal> _D14;

                private System.Nullable<decimal> _D15;

                private System.Nullable<decimal> _D16;

                private System.Nullable<decimal> _D17;

                private System.Nullable<decimal> _D18;

                private System.Nullable<decimal> _D19;

                private System.Nullable<decimal> _D20;

                private System.Nullable<decimal> _D21;

                private System.Nullable<decimal> _D22;

                private System.Nullable<decimal> _D23;

                private System.Nullable<decimal> _D24;

                private System.Nullable<decimal> _D25;

                private System.Nullable<decimal> _D26;

                private System.Nullable<decimal> _D27;

                private System.Nullable<decimal> _D28;

                private System.Nullable<decimal> _D29;

                private System.Nullable<decimal> _D30;

                private System.Nullable<decimal> _TOT_DED;

                private System.Nullable<decimal> _NET_PAY;

                private System.Nullable<bool> _HP;

                private System.Nullable<decimal> _PAYDAYS;

                private string _REMARK;

                private System.Nullable<decimal> _PAY;

                private string _TITLE;

                private System.Nullable<decimal> _EXPAMT;

                private System.Nullable<decimal> _DPAMT;

                private System.Nullable<decimal> _GRADEPAY;

                private string _COLLEGE_CODE;


                //[Column(Storage = "_SUPLTRXID", DbType = "Int")]
                public System.Nullable<int> SUPLTRXID
                {
                    get
                    {
                        return this._SUPLTRXID;
                    }
                    set
                    {
                        if ((this._SUPLTRXID != value))
                        {
                            this._SUPLTRXID = value;
                        }
                    }
                }

                //[Column(Storage = "_SUPLHNO", DbType = "Int")]
                public System.Nullable<int> SUPLHNO
                {
                    get
                    {
                        return this._SUPLHNO;
                    }
                    set
                    {
                        if ((this._SUPLHNO != value))
                        {
                            this._SUPLHNO = value;
                        }
                    }
                }

                //[Column(Storage = "_SUPLHEAD", DbType = "NVarChar(50)")]
                public string SUPLHEAD
                {
                    get
                    {
                        return this._SUPLHEAD;
                    }
                    set
                    {
                        if ((this._SUPLHEAD != value))
                        {
                            this._SUPLHEAD = value;
                        }
                    }
                }

                //[Column(Storage = "_SUPLSTATUS", DbType = "NChar(1)")]
                public System.Nullable<char> SUPLSTATUS
                {
                    get
                    {
                        return this._SUPLSTATUS;
                    }
                    set
                    {
                        if ((this._SUPLSTATUS != value))
                        {
                            this._SUPLSTATUS = value;
                        }
                    }
                }

                //[Column(Storage = "_MONYEAR", DbType = "NVarChar(10)")]
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

                //[Column(Storage = "_ORDNO", DbType = "NVarChar(50)")]
                public string ORDNO
                {
                    get
                    {
                        return this._ORDNO;
                    }
                    set
                    {
                        if ((this._ORDNO != value))
                        {
                            this._ORDNO = value;
                        }
                    }
                }

                //[Column(Storage = "_SBDATE", DbType = "DateTime")]
                public System.Nullable<System.DateTime> SBDATE
                {
                    get
                    {
                        return this._SBDATE;
                    }
                    set
                    {
                        if ((this._SBDATE != value))
                        {
                            this._SBDATE = value;
                        }
                    }
                }

               // [Column(Storage = "_ADDIT", DbType = "Bit")]
                public System.Nullable<bool> ADDIT
                {
                    get
                    {
                        return this._ADDIT;
                    }
                    set
                    {
                        if ((this._ADDIT != value))
                        {
                            this._ADDIT = value;
                        }
                    }
                }

                // [Column(Storage = "_ADDOTHINCOME", DbType = "Bit")]
                public System.Nullable<bool> ADDOTHINCOME
                {
                    get
                    {
                        return this._ADDOTHINCOME;
                    }
                    set
                    {
                        if ((this._ADDOTHINCOME != value))
                        {
                            this._ADDOTHINCOME = value;
                        }
                    }
                }

               // [Column(Storage = "_PSTATUS", DbType = "NChar(1)")]
                public System.Nullable<char> PSTATUS
                {
                    get
                    {
                        return this._PSTATUS;
                    }
                    set
                    {
                        if ((this._PSTATUS != value))
                        {
                            this._PSTATUS = value;
                        }
                    }
                }

                //[Column(Storage = "_APPOINTNO", DbType = "Int")]
                public System.Nullable<int> APPOINTNO
                {
                    get
                    {
                        return this._APPOINTNO;
                    }
                    set
                    {
                        if ((this._APPOINTNO != value))
                        {
                            this._APPOINTNO = value;
                        }
                    }
                }

               //[Column(Storage = "_SCALENO", DbType = "Int")]
                public System.Nullable<int> SCALENO
                {
                    get
                    {
                        return this._SCALENO;
                    }
                    set
                    {
                        if ((this._SCALENO != value))
                        {
                            this._SCALENO = value;
                        }
                    }
                }

                //[Column(Storage = "_STAFFNO", DbType = "Int")]
                public System.Nullable<int> STAFFNO
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

                //[Column(Storage = "_SUPLIPAY", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> SUPLIPAY
                {
                    get
                    {
                        return this._SUPLIPAY;
                    }
                    set
                    {
                        if ((this._SUPLIPAY != value))
                        {
                            this._SUPLIPAY = value;
                        }
                    }
                }

                //[Column(Storage = "_I1", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> I1
                {
                    get
                    {
                        return this._I1;
                    }
                    set
                    {
                        if ((this._I1 != value))
                        {
                            this._I1 = value;
                        }
                    }
                }

                //[Column(Storage = "_I2", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> I2
                {
                    get
                    {
                        return this._I2;
                    }
                    set
                    {
                        if ((this._I2 != value))
                        {
                            this._I2 = value;
                        }
                    }
                }

               //[Column(Storage = "_I3", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> I3
                {
                    get
                    {
                        return this._I3;
                    }
                    set
                    {
                        if ((this._I3 != value))
                        {
                            this._I3 = value;
                        }
                    }
                }

               //[Column(Storage = "_I4", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> I4
                {
                    get
                    {
                        return this._I4;
                    }
                    set
                    {
                        if ((this._I4 != value))
                        {
                            this._I4 = value;
                        }
                    }
                }

                //[Column(Storage = "_I5", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> I5
                {
                    get
                    {
                        return this._I5;
                    }
                    set
                    {
                        if ((this._I5 != value))
                        {
                            this._I5 = value;
                        }
                    }
                }

                //[Column(Storage = "_I6", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> I6
                {
                    get
                    {
                        return this._I6;
                    }
                    set
                    {
                        if ((this._I6 != value))
                        {
                            this._I6 = value;
                        }
                    }
                }

                //[Column(Storage = "_I7", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> I7
                {
                    get
                    {
                        return this._I7;
                    }
                    set
                    {
                        if ((this._I7 != value))
                        {
                            this._I7 = value;
                        }
                    }
                }

                //[Column(Storage = "_I8", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> I8
                {
                    get
                    {
                        return this._I8;
                    }
                    set
                    {
                        if ((this._I8 != value))
                        {
                            this._I8 = value;
                        }
                    }
                }

                //[Column(Storage = "_I9", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> I9
                {
                    get
                    {
                        return this._I9;
                    }
                    set
                    {
                        if ((this._I9 != value))
                        {
                            this._I9 = value;
                        }
                    }
                }

                //[Column(Storage = "_I10", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> I10
                {
                    get
                    {
                        return this._I10;
                    }
                    set
                    {
                        if ((this._I10 != value))
                        {
                            this._I10 = value;
                        }
                    }
                }

                //[Column(Storage = "_I11", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> I11
                {
                    get
                    {
                        return this._I11;
                    }
                    set
                    {
                        if ((this._I11 != value))
                        {
                            this._I11 = value;
                        }
                    }
                }

                //[Column(Storage = "_I12", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> I12
                {
                    get
                    {
                        return this._I12;
                    }
                    set
                    {
                        if ((this._I12 != value))
                        {
                            this._I12 = value;
                        }
                    }
                }

                //[Column(Storage = "_I13", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> I13
                {
                    get
                    {
                        return this._I13;
                    }
                    set
                    {
                        if ((this._I13 != value))
                        {
                            this._I13 = value;
                        }
                    }
                }

                //[Column(Storage = "_I14", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> I14
                {
                    get
                    {
                        return this._I14;
                    }
                    set
                    {
                        if ((this._I14 != value))
                        {
                            this._I14 = value;
                        }
                    }
                }

                //[Column(Storage = "_I15", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> I15
                {
                    get
                    {
                        return this._I15;
                    }
                    set
                    {
                        if ((this._I15 != value))
                        {
                            this._I15 = value;
                        }
                    }
                }

                //[Column(Storage = "_GS", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> GS
                {
                    get
                    {
                        return this._GS;
                    }
                    set
                    {
                        if ((this._GS != value))
                        {
                            this._GS = value;
                        }
                    }
                }

                //[Column(Storage = "_GS1", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> GS1
                {
                    get
                    {
                        return this._GS1;
                    }
                    set
                    {
                        if ((this._GS1 != value))
                        {
                            this._GS1 = value;
                        }
                    }
                }

                //[Column(Storage = "_D1", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> D1
                {
                    get
                    {
                        return this._D1;
                    }
                    set
                    {
                        if ((this._D1 != value))
                        {
                            this._D1 = value;
                        }
                    }
                }

                //[Column(Storage = "_D2", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> D2
                {
                    get
                    {
                        return this._D2;
                    }
                    set
                    {
                        if ((this._D2 != value))
                        {
                            this._D2 = value;
                        }
                    }
                }

                //[Column(Storage = "_D3", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> D3
                {
                    get
                    {
                        return this._D3;
                    }
                    set
                    {
                        if ((this._D3 != value))
                        {
                            this._D3 = value;
                        }
                    }
                }

                //[Column(Storage = "_D4", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> D4
                {
                    get
                    {
                        return this._D4;
                    }
                    set
                    {
                        if ((this._D4 != value))
                        {
                            this._D4 = value;
                        }
                    }
                }

                //[Column(Storage = "_D5", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> D5
                {
                    get
                    {
                        return this._D5;
                    }
                    set
                    {
                        if ((this._D5 != value))
                        {
                            this._D5 = value;
                        }
                    }
                }

                //[Column(Storage = "_D6", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> D6
                {
                    get
                    {
                        return this._D6;
                    }
                    set
                    {
                        if ((this._D6 != value))
                        {
                            this._D6 = value;
                        }
                    }
                }

                //[Column(Storage = "_D7", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> D7
                {
                    get
                    {
                        return this._D7;
                    }
                    set
                    {
                        if ((this._D7 != value))
                        {
                            this._D7 = value;
                        }
                    }
                }

                //[Column(Storage = "_D8", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> D8
                {
                    get
                    {
                        return this._D8;
                    }
                    set
                    {
                        if ((this._D8 != value))
                        {
                            this._D8 = value;
                        }
                    }
                }

                //[Column(Storage = "_D9", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> D9
                {
                    get
                    {
                        return this._D9;
                    }
                    set
                    {
                        if ((this._D9 != value))
                        {
                            this._D9 = value;
                        }
                    }
                }

                //[Column(Storage = "_D10", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> D10
                {
                    get
                    {
                        return this._D10;
                    }
                    set
                    {
                        if ((this._D10 != value))
                        {
                            this._D10 = value;
                        }
                    }
                }

                //[Column(Storage = "_D11", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> D11
                {
                    get
                    {
                        return this._D11;
                    }
                    set
                    {
                        if ((this._D11 != value))
                        {
                            this._D11 = value;
                        }
                    }
                }

                //[Column(Storage = "_D12", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> D12
                {
                    get
                    {
                        return this._D12;
                    }
                    set
                    {
                        if ((this._D12 != value))
                        {
                            this._D12 = value;
                        }
                    }
                }

                //[Column(Storage = "_D13", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> D13
                {
                    get
                    {
                        return this._D13;
                    }
                    set
                    {
                        if ((this._D13 != value))
                        {
                            this._D13 = value;
                        }
                    }
                }

                //[Column(Storage = "_D14", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> D14
                {
                    get
                    {
                        return this._D14;
                    }
                    set
                    {
                        if ((this._D14 != value))
                        {
                            this._D14 = value;
                        }
                    }
                }

                //[Column(Storage = "_D15", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> D15
                {
                    get
                    {
                        return this._D15;
                    }
                    set
                    {
                        if ((this._D15 != value))
                        {
                            this._D15 = value;
                        }
                    }
                }

                //[Column(Storage = "_D16", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> D16
                {
                    get
                    {
                        return this._D16;
                    }
                    set
                    {
                        if ((this._D16 != value))
                        {
                            this._D16 = value;
                        }
                    }
                }

                //[Column(Storage = "_D17", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> D17
                {
                    get
                    {
                        return this._D17;
                    }
                    set
                    {
                        if ((this._D17 != value))
                        {
                            this._D17 = value;
                        }
                    }
                }

                //[Column(Storage = "_D18", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> D18
                {
                    get
                    {
                        return this._D18;
                    }
                    set
                    {
                        if ((this._D18 != value))
                        {
                            this._D18 = value;
                        }
                    }
                }

                //[Column(Storage = "_D19", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> D19
                {
                    get
                    {
                        return this._D19;
                    }
                    set
                    {
                        if ((this._D19 != value))
                        {
                            this._D19 = value;
                        }
                    }
                }

                //[Column(Storage = "_D20", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> D20
                {
                    get
                    {
                        return this._D20;
                    }
                    set
                    {
                        if ((this._D20 != value))
                        {
                            this._D20 = value;
                        }
                    }
                }

                //[Column(Storage = "_D21", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> D21
                {
                    get
                    {
                        return this._D21;
                    }
                    set
                    {
                        if ((this._D21 != value))
                        {
                            this._D21 = value;
                        }
                    }
                }

                //[Column(Storage = "_D22", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> D22
                {
                    get
                    {
                        return this._D22;
                    }
                    set
                    {
                        if ((this._D22 != value))
                        {
                            this._D22 = value;
                        }
                    }
                }

                //[Column(Storage = "_D23", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> D23
                {
                    get
                    {
                        return this._D23;
                    }
                    set
                    {
                        if ((this._D23 != value))
                        {
                            this._D23 = value;
                        }
                    }
                }

                //[Column(Storage = "_D24", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> D24
                {
                    get
                    {
                        return this._D24;
                    }
                    set
                    {
                        if ((this._D24 != value))
                        {
                            this._D24 = value;
                        }
                    }
                }

                //[Column(Storage = "_D25", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> D25
                {
                    get
                    {
                        return this._D25;
                    }
                    set
                    {
                        if ((this._D25 != value))
                        {
                            this._D25 = value;
                        }
                    }
                }

                //[Column(Storage = "_D26", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> D26
                {
                    get
                    {
                        return this._D26;
                    }
                    set
                    {
                        if ((this._D26 != value))
                        {
                            this._D26 = value;
                        }
                    }
                }

                //[Column(Storage = "_D27", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> D27
                {
                    get
                    {
                        return this._D27;
                    }
                    set
                    {
                        if ((this._D27 != value))
                        {
                            this._D27 = value;
                        }
                    }
                }

                //[Column(Storage = "_D28", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> D28
                {
                    get
                    {
                        return this._D28;
                    }
                    set
                    {
                        if ((this._D28 != value))
                        {
                            this._D28 = value;
                        }
                    }
                }

                //[Column(Storage = "_D29", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> D29
                {
                    get
                    {
                        return this._D29;
                    }
                    set
                    {
                        if ((this._D29 != value))
                        {
                            this._D29 = value;
                        }
                    }
                }

                //[Column(Storage = "_D30", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> D30
                {
                    get
                    {
                        return this._D30;
                    }
                    set
                    {
                        if ((this._D30 != value))
                        {
                            this._D30 = value;
                        }
                    }
                }

                //[Column(Storage = "_TOT_DED", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> TOT_DED
                {
                    get
                    {
                        return this._TOT_DED;
                    }
                    set
                    {
                        if ((this._TOT_DED != value))
                        {
                            this._TOT_DED = value;
                        }
                    }
                }

                //[Column(Storage = "_NET_PAY", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> NET_PAY
                {
                    get
                    {
                        return this._NET_PAY;
                    }
                    set
                    {
                        if ((this._NET_PAY != value))
                        {
                            this._NET_PAY = value;
                        }
                    }
                }

                //[Column(Storage = "_HP", DbType = "Bit")]
                public System.Nullable<bool> HP
                {
                    get
                    {
                        return this._HP;
                    }
                    set
                    {
                        if ((this._HP != value))
                        {
                            this._HP = value;
                        }
                    }
                }

                //[Column(Storage = "_PAYDAYS", DbType = "Decimal(4,1)")]
                public System.Nullable<decimal> PAYDAYS
                {
                    get
                    {
                        return this._PAYDAYS;
                    }
                    set
                    {
                        if ((this._PAYDAYS != value))
                        {
                            this._PAYDAYS = value;
                        }
                    }
                }

                //[Column(Storage = "_REMARK", DbType = "NVarChar(250)")]
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

                //[Column(Storage = "_PAY", DbType = "Decimal(12,2)")]
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

                //[Column(Storage = "_TITLE", DbType = "NVarChar(50)")]
                public string TITLE
                {
                    get
                    {
                        return this._TITLE;
                    }
                    set
                    {
                        if ((this._TITLE != value))
                        {
                            this._TITLE = value;
                        }
                    }
                }

                //[Column(Storage = "_EXPAMT", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> EXPAMT
                {
                    get
                    {
                        return this._EXPAMT;
                    }
                    set
                    {
                        if ((this._EXPAMT != value))
                        {
                            this._EXPAMT = value;
                        }
                    }
                }

                //[Column(Storage = "_DPAMT", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> DPAMT
                {
                    get
                    {
                        return this._DPAMT;
                    }
                    set
                    {
                        if ((this._DPAMT != value))
                        {
                            this._DPAMT = value;
                        }
                    }
                }

                //[Column(Storage = "_GRADEPAY", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> GRADEPAY
                {
                    get
                    {
                        return this._GRADEPAY;
                    }
                    set
                    {
                        if ((this._GRADEPAY != value))
                        {
                            this._GRADEPAY = value;
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

            }
        
        }//END: BusinessLayer.BusinessEntities

    }//END: UAIMS 

}// END: IITMS
