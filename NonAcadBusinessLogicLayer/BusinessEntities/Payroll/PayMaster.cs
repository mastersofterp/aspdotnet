using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class PayMaster
            {
                # region Private Members
                private System.Nullable<int> _IDNO;

                private System.Nullable<char> _PSTATUS;

                private System.Nullable<int> _SUBDEPTNO;

                private System.Nullable<char> _STATUS;

                private System.Nullable<int> _PAYRULE;
               // private string _PAYRULE = string.Empty;

                private System.Nullable<int> _APPOINTNO;

                private System.Nullable<int> _BANKNO;

                private System.Nullable<int> _SCALENO;

                private System.Nullable<int> _SUPPLINO;

                private System.Nullable<int> _SUBDESIGNO;

                private System.Nullable<int> _SEQ_NO;

                private System.Nullable<int> _DESIGNATURENO;

                private System.Nullable<System.DateTime> _ODOI;

                private System.Nullable<System.DateTime> _DOJ;

                private System.Nullable<int> _OBASIC = 0;

                private System.Nullable<int> _BASIC = 0;

                private System.Nullable<int> _PAY = 0;

                private System.Nullable<decimal> _I1 = 0.0m;

                private System.Nullable<decimal> _I2 = 0.0m;

                private System.Nullable<decimal> _I3 = 0.0m;

                private System.Nullable<decimal> _I4 = 0.0m;

                private System.Nullable<decimal> _I5 = 0.0m;

                private System.Nullable<decimal> _I6 = 0.0m;

                private System.Nullable<decimal> _I7 = 0.0m;

                private System.Nullable<decimal> _I8 = 0.0m;

                private System.Nullable<decimal> _I9 = 0.0m;

                private System.Nullable<decimal> _I10 = 0.0m;

                private System.Nullable<decimal> _I11 = 0.0m;

                private System.Nullable<decimal> _I12 = 0.0m;

                private System.Nullable<decimal> _I13 = 0.0m;

                private System.Nullable<decimal> _I14 = 0.0m;

                private System.Nullable<decimal> _I15 = 0.0m;

                private System.Nullable<decimal> _GS = 0.0m;

                private System.Nullable<decimal> _GS1 = 0.0m;

                private System.Nullable<decimal> _D1 = 0.0m;

                private System.Nullable<decimal> _D2 = 0.0m;

                private System.Nullable<decimal> _D3 = 0.0m;

                private System.Nullable<decimal> _D4 = 0.0m;

                private System.Nullable<decimal> _D5 = 0.0m;

                private System.Nullable<decimal> _D6 = 0.0m;

                private System.Nullable<decimal> _D7 = 0.0m;

                private System.Nullable<decimal> _D8 = 0.0m;

                private System.Nullable<decimal> _D9 = 0.0m;

                private System.Nullable<decimal> _D10 = 0.0m;

                private System.Nullable<decimal> _D11 = 0.0m;

                private System.Nullable<decimal> _D12 = 0.0m;

                private System.Nullable<decimal> _D13 = 0.0m;

                private System.Nullable<decimal> _D14 = 0.0m;

                private System.Nullable<decimal> _D15 = 0.0m;

                private System.Nullable<decimal> _D16 = 0.0m;

                private System.Nullable<decimal> _D17 = 0.0m;

                private System.Nullable<decimal> _D18 = 0.0m;

                private System.Nullable<decimal> _D19 = 0.0m;

                private System.Nullable<decimal> _D20 = 0.0m;

                private System.Nullable<decimal> _D21 = 0.0m;

                private System.Nullable<decimal> _D22 = 0.0m;

                private System.Nullable<decimal> _D23 = 0.0m;

                private System.Nullable<decimal> _D24 = 0.0m;

                private System.Nullable<decimal> _D25 = 0.0m;

                private System.Nullable<decimal> _D26 = 0.0m;

                private System.Nullable<decimal> _D27 = 0.0m;

                private System.Nullable<decimal> _D28 = 0.0m;

                private System.Nullable<decimal> _D29 = 0.0m;

                private System.Nullable<decimal> _D30 = 0.0m;

                private System.Nullable<decimal> _TOT_DED = 0.0m;

                private System.Nullable<decimal> _NET_PAY = 0.0m;

                private System.Nullable<bool> _HP;

                private System.Nullable<bool> _WP;

                private System.Nullable<System.DateTime> _SBDATE;

                private System.Nullable<char> _TA;

                private System.Nullable<char> _INCYN;

                private System.Nullable<char> _LOG;

                private System.Nullable<int> _DPCAL;

                private System.Nullable<decimal> _DAAMT = 0.0m;

                private System.Nullable<bool> _IT;

                private System.Nullable<decimal> _GRADEPAY = 0.0m;

                private string _REMARK = string.Empty;

                private string _COMMREM = string.Empty;

                private string _MONREM = string.Empty;

                private string _COLLEGE_CODE;
                private string _MAINDEPTCODE = string.Empty;
                private string _MAINDEPTNAME = string.Empty;
                private System.Nullable<int> _MAINDEPTNO;


                private int _DA_HEAD_ID;
                private string _DA_HEAD_DESCRIPTION = string.Empty;
                private int _ORGANIZATIONID;


                //  Amol sawarkar add 
                private System.Nullable<int> _DIVIDNO;
                private string _DIVNAME;
                private string _DIVCODE;

                // add amol sawarkar 

                private System.Nullable<int> _DIVISIONMASTER;
                

                // 09-03-2023
                private System.Nullable<int> _STAFFNO;		
                private string _STAFFTYPE;
                private string _ACTIVESTATUS;
                private System.Nullable<bool> _IsTeaching;

                // 13-03-2023

                private System.Nullable<int> _RETIREMENTAGE;		

                #region Unicode

                private string _DEPARTMENT;

                private string _DEPARTMENT_KANNADA;

                private string _DEPTSHORTNAME;

                private string _DESIGNATION;

                private string _DESIGNATION_KANNADA;

                private string _DESIGSHORT;

                private System.Nullable<decimal> _DESIGNATION_SEQNO = 0.0m;

                #endregion

                # endregion

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

                public System.Nullable<int> SUBDEPTNO
                {
                    get
                    {
                        return this._SUBDEPTNO;
                    }
                    set
                    {
                        if ((this._SUBDEPTNO != value))
                        {
                            this._SUBDEPTNO = value;
                        }
                    }
                }

                public System.Nullable<char> STATUS
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

                public System.Nullable<int> PAYRULE
                {
                    get
                    {
                        return this._PAYRULE;
                    }
                    set
                    {
                        if ((this._PAYRULE != value))
                        {
                            this._PAYRULE = value;
                        }
                    }
                }

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

                public System.Nullable<int> SUPPLINO
                {
                    get
                    {
                        return this._SUPPLINO;
                    }
                    set
                    {
                        if ((this._SUPPLINO != value))
                        {
                            this._SUPPLINO = value;
                        }
                    }
                }

                public System.Nullable<int> SUBDESIGNO
                {
                    get
                    {
                        return this._SUBDESIGNO;
                    }
                    set
                    {
                        if ((this._SUBDESIGNO != value))
                        {
                            this._SUBDESIGNO = value;
                        }
                    }
                }

                public System.Nullable<int> SEQ_NO
                {
                    get
                    {
                        return this._SEQ_NO;
                    }
                    set
                    {
                        if ((this._SEQ_NO != value))
                        {
                            this._SEQ_NO = value;
                        }
                    }
                }

                public System.Nullable<int> DESIGNATURENO
                {
                    get
                    {
                        return this._DESIGNATURENO;
                    }
                    set
                    {
                        if ((this._DESIGNATURENO != value))
                        {
                            this._DESIGNATURENO = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> ODOI
                {
                    get
                    {
                        return this._ODOI;
                    }
                    set
                    {
                        if ((this._ODOI != value))
                        {
                            this._ODOI = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> DOJ
                {
                    get
                    {
                        return this._DOJ;
                    }
                    set
                    {
                        if ((this._DOJ != value))
                        {
                            this._DOJ = value;
                        }
                    }
                }

                public System.Nullable<int> OBASIC
                {
                    get
                    {
                        return this._OBASIC;
                    }
                    set
                    {
                        if ((this._OBASIC != value))
                        {
                            this._OBASIC = value;
                        }
                    }
                }

                public System.Nullable<int> BASIC
                {
                    get
                    {
                        return this._BASIC;
                    }
                    set
                    {
                        if ((this._BASIC != value))
                        {
                            this._BASIC = value;
                        }
                    }
                }

                public System.Nullable<int> PAY
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

                public System.Nullable<bool> WP
                {
                    get
                    {
                        return this._WP;
                    }
                    set
                    {
                        if ((this._WP != value))
                        {
                            this._WP = value;
                        }
                    }
                }

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

                public System.Nullable<char> TA
                {
                    get
                    {
                        return this._TA;
                    }
                    set
                    {
                        if ((this._TA != value))
                        {
                            this._TA = value;
                        }
                    }
                }

                public System.Nullable<char> INCYN
                {
                    get
                    {
                        return this._INCYN;
                    }
                    set
                    {
                        if ((this._INCYN != value))
                        {
                            this._INCYN = value;
                        }
                    }
                }

                public System.Nullable<char> LOG
                {
                    get
                    {
                        return this._LOG;
                    }
                    set
                    {
                        if ((this._LOG != value))
                        {
                            this._LOG = value;
                        }
                    }
                }

                public System.Nullable<int> DPCAL
                {
                    get
                    {
                        return this._DPCAL;
                    }
                    set
                    {
                        if ((this._DPCAL != value))
                        {
                            this._DPCAL = value;
                        }
                    }
                }

                public System.Nullable<decimal> DAAMT
                {
                    get
                    {
                        return this._DAAMT;
                    }
                    set
                    {
                        if ((this._DAAMT != value))
                        {
                            this._DAAMT = value;
                        }
                    }
                }

                public System.Nullable<bool> IT
                {
                    get
                    {
                        return this._IT;
                    }
                    set
                    {
                        if ((this._IT != value))
                        {
                            this._IT = value;
                        }
                    }
                }

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

                public string COMMREM
                {
                    get
                    {
                        return this._COMMREM;
                    }
                    set
                    {
                        if ((this._COMMREM != value))
                        {
                            this._COMMREM = value;
                        }
                    }
                }

                public string MONREM
                {
                    get
                    {
                        return this._MONREM;
                    }
                    set
                    {
                        if ((this._MONREM != value))
                        {
                            this._MONREM = value;
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

                public string MAINDEPTCODE
                {
                    get
                    {
                        return this._MAINDEPTCODE;
                    }
                    set
                    {
                        if ((this._MAINDEPTCODE != value))
                        {
                            this._MAINDEPTCODE = value;
                        }
                    }
                }

                public string MAINDEPTNAME
                {
                    get
                    {
                        return this._MAINDEPTNAME;
                    }
                    set
                    {
                        if ((this._MAINDEPTNAME != value))
                        {
                            this._MAINDEPTNAME = value;
                        }
                    }
                }

                public System.Nullable<int> MAINDEPTNO
                {
                    get
                    {
                        return this._MAINDEPTNO;
                    }
                    set
                    {
                       if ((this._MAINDEPTNO != value))
                        {
                            this._MAINDEPTNO = value;
                        }
                    }
                }
                
             

                #region Unicode

                public string DEPARTMENT
                {
                    get
                    {
                        return this._DEPARTMENT;
                    }
                    set
                    {
                        if ((this._DEPARTMENT != value))
                        {
                            this._DEPARTMENT = value;
                        }
                    }
                }

                public string DEPARTMENT_KANNADA
                {
                    get
                    {
                        return this._DEPARTMENT_KANNADA;
                    }
                    set
                    {
                        if ((this._DEPARTMENT_KANNADA != value))
                        {
                            this._DEPARTMENT_KANNADA = value;
                        }
                    }
                }

                public string DEPTSHORTNAME
                {
                    get
                    {
                        return this._DEPTSHORTNAME;
                    }
                    set
                    {
                        if ((this._DEPTSHORTNAME != value))
                        {
                            this._DEPTSHORTNAME = value;
                        }
                    }
                }

                public string DESIGNATION
                {
                    get
                    {
                        return this._DESIGNATION;
                    }
                    set
                    {
                        if ((this._DESIGNATION != value))
                        {
                            this._DESIGNATION = value;
                        }
                    }
                }

                public string DESIGNATION_KANNADA
                {
                    get
                    {
                        return this._DESIGNATION_KANNADA;
                    }
                    set
                    {
                        if ((this._DESIGNATION_KANNADA != value))
                        {
                            this._DESIGNATION_KANNADA = value;
                        }
                    }
                }

                public string DESIGSHORT
                {
                    get
                    {
                        return this._DESIGSHORT;
                    }
                    set
                    {
                        if ((this._DESIGSHORT != value))
                        {
                            this._DESIGSHORT = value;
                        }
                    }
                }

                public System.Nullable<decimal> DESIGNATION_SEQNO
                {
                    get
                    {
                        return this._DESIGNATION_SEQNO;
                    }
                    set
                    {
                        if ((this._DESIGNATION_SEQNO != value))
                        {
                            this._DESIGNATION_SEQNO = value;
                        }
                    }
                }

                #endregion


                # endregion


                public int DAHEADID
                {
                    set
                    {
                        _DA_HEAD_ID = value;
                    }
                    get
                    {
                        return _DA_HEAD_ID;
                    }

                }
                public string DAHEADESCIPTION
                {
                    set
                    {
                        _DA_HEAD_DESCRIPTION = value;
                    }
                    get
                    {
                        return _DA_HEAD_DESCRIPTION;
                    }
                }

                // Amol sawarkar

                public System.Nullable<int> DIVIDNO
                {
                    get
                    {
                        return this._DIVIDNO;
                    }
                    set
                    {
                        if ((this._DIVIDNO != value))
                        {
                            this._DIVIDNO = value;
                        }
                    }
                }
                public string DIVNAME
                {
                    set
                    {
                        _DIVNAME = value;
                    }
                    get
                    {
                        return _DIVNAME;
                    }
                }
                public string DIVCODE
                {
                    set
                    {
                        _DIVCODE = value;
                    }
                    get
                    {
                        return _DIVCODE;
                    }
                }

                //ADD AMOL
                public System.Nullable<int> DIVISIONMASTER
                {
                    get
                    {
                        return this._DIVISIONMASTER;
                    }
                    set
                    {
                        if ((this._DIVISIONMASTER != value))
                        {
                            this._DIVISIONMASTER = value;
                        }
                    }
                }

                // 09-03-2023
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

                public string STAFFTYPE
                {
                    set
                    {
                        _STAFFTYPE = value;
                    }
                    get
                    {
                        return _STAFFTYPE;
                    }
                }
                public string ACTIVESTATUS
                {
                    set
                    {
                        _ACTIVESTATUS = value;
                    }
                    get
                    {
                        return _ACTIVESTATUS;
                    }
                }

                public System.Nullable<bool> IsTeaching
                {
                    get
                    {
                        return this._IsTeaching;
                    }
                    set
                    {
                        if ((this._IsTeaching != value))
                        {
                            this._IsTeaching = value;
                        }
                    }
                }


                //13-03-2023

                public System.Nullable<int> RETIREMENTAGE
                {
                    get
                    {
                        return this._RETIREMENTAGE;
                    }
                    set
                    {
                        if ((this._RETIREMENTAGE != value))
                        {
                            this._RETIREMENTAGE = value;
                        }
                    }
                }

                //-------Shaikkh Juned (05-11-2022)--Start
                private DataTable _EmployeeDataImport_TBL = null;
                private string _regNo = string.Empty;
                private char _gender = ' ';
                //-------Shaikkh Juned (05-11-2022)--end


                //-------Shaikkh Juned (05-11-2022)--Start
                public DataTable EmployeeDataImport_TBL
                {
                    get
                    {
                        return this._EmployeeDataImport_TBL;
                    }
                    set
                    {
                        if ((this._EmployeeDataImport_TBL != value))
                        {
                            this._EmployeeDataImport_TBL = value;
                        }
                    }
                }



                public string RegNo
                {
                    get { return _regNo; }
                    set { _regNo = value; }
                }

                public char gender
                {
                    get { return _gender; }
                    set { _gender = value; }
                }

                //-------Shaikkh Juned (05-11-2022)--end
            }



        } //END: BusinessLayer.BusinessEntities
    } //END: UAIMS  
} //END: IITMS
