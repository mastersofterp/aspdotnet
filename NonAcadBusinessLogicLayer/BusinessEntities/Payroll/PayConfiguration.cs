using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLogicLayer.BusinessEntities
        {
            public class PayConfiguration
            {
                #region Private Members

                private string _DA;

                private string _HRA;

                private string _CLA;

                private string _TA;

                private string _DA_ON_TA;

                private string _RECOVERY;

                private string _GPF_ADD;

                private string _GPF_ADV;

                private string _IT;

                private string _PT;

                private string _LIC;

                private string _GIS;

                private string _LISEFEE;

                private string _SOCIETY;

                private string _LWP;

                private string _DE;

                private string _G80;

                private string _CPF;

                private string _CPF_LOAN;

                private string _GPFLOAN;



                private string _PHONE;

                private string _MEDICAL;

                private string _RD;

                private string _HONO;





                private string _GPF;

                private string _PRINCIPAL;

                private string _BANK;

                private string _SECTIONXI;

                private System.Nullable<System.DateTime> _FROMDATE = DateTime.MinValue;

                private System.Nullable<decimal> _CPFPER;

                private string _LICBRANCH;

                private string _TANNO;

                private System.Nullable<System.DateTime> _PFFSDATE = DateTime.MinValue;

                private System.Nullable<System.DateTime> _CPFFROMDATE = DateTime.MinValue;

                private string _COLLEGECODE;

                private string _BANKLOCATION;

                private string _SECTIONXII;

                private System.Nullable<decimal> _DAPER;

                private System.Nullable<System.DateTime> _TODATE = DateTime.MinValue;

                private System.Nullable<Int64> _ACCOUNT;

                private string _PACNO;

                private string _PANNO;

                private System.Nullable<System.DateTime> _PFFEDATE = DateTime.MinValue;

                private System.Nullable<System.DateTime> _CPFTODATE = DateTime.MinValue;





                private System.Nullable<Int64> _LINK_WITH_ACC;

                private System.Nullable<Int64> _GP_Status;

                private System.Nullable<Int64> _DP_Status;

                private bool _IsAutoUserCreated;









                private string _ITREGNO;

                private string _PTREGNO;

                private string __BANKLOAN;

                private string _PUJAFUND;

                private string _NTFUND;

                private string _CRFUND;

                private string _OTHERDED;

                private string _PAY_UNIT_NO;

                private string _CODENO;

                private string _ZONE;

                private string _WARD;

                private string _REGISTRAR;



                private string _COLNAME;


                private string _SOCNAME;
                private string _COMPANY;
                private string _ACCPW;
                private int _SINGLE_INV;
                private double _RWIDTH;


                private double _RHEIGHT;
                private string _SALPASSWORD;
                private double _GPFPER;
                private double _ENAMESIZE;
                private int _ANOSIZE;
                private int _AMTSIZE;
                private string _LWP_CAL;
                private Boolean _DOSFONT;



                private Boolean _ZERO;
                private double _RD_PER;
                private int _LWP_PDAY;
                private string _LICEFEE_FIELD;
                private int _PROPOSED_SAL;
                private int _MODULE_NO;
                private int _LINK_NO;
                private string _LEAVE_APPROVAL;
                private int _PH_AMT;


                private string _STAFFNO;



                private System.Nullable<decimal> _EPFAMOUNT;
                private string _STAFF_APPLICABLE;


                //Amol sawarkar 04-03-2022


                private string _ESICNO;
                private System.Nullable<decimal> _ESI_LIMIT_HP;
                private System.Nullable<decimal> _ESI_PER_HP;
                private System.Nullable<decimal> _EmployerContribution;
                private System.Nullable<decimal> _ESIC_LIMIT;

                private System.Nullable<System.DateTime> _ESIFIRSTFROMDATE = DateTime.MinValue;
                private System.Nullable<System.DateTime> _ESIFIRSTTODATE = DateTime.MinValue;



                private System.Nullable<System.DateTime> _ESISECONDFROMDATE = DateTime.MinValue;
                private System.Nullable<System.DateTime> _ESISECONDTODATE = DateTime.MinValue;


                private System.Nullable<System.DateTime> _EmpPayslipShowFromDate = DateTime.MinValue;



                private string _USERLOGINTYPE;
                private string _USERPASSWORD;

                //14-09-2022
                private byte[] _PhotoSign = null;

                # endregion


                # region Public Properties


                public string USERLOGINTYPE
                {
                    get
                    {
                        return this._USERLOGINTYPE;
                    }
                    set
                    {
                        if ((this._USERLOGINTYPE != value))
                        {
                            this._USERLOGINTYPE = value;
                        }
                    }
                }
                public string USERPASSWORD
                {
                    get
                    {
                        return this._USERPASSWORD;
                    }
                    set
                    {
                        if ((this._USERPASSWORD != value))
                        {
                            this._USERPASSWORD = value;
                        }
                    }
                }

                public string DA
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

                public string HRA
                {
                    get
                    {
                        return this._HRA;
                    }
                    set
                    {
                        if ((this._HRA != value))
                        {
                            this._HRA = value;
                        }
                    }
                }

                public string CLA
                {
                    get
                    {
                        return this._CLA;
                    }
                    set
                    {
                        if ((this._CLA != value))
                        {
                            this._CLA = value;
                        }
                    }
                }

                public string TA
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

                public string DAONTA
                {
                    get
                    {
                        return this._DA_ON_TA;
                    }
                    set
                    {
                        if ((this._DA_ON_TA != value))
                        {
                            this._DA_ON_TA = value;
                        }
                    }
                }

                public string RECOVERY
                {
                    get
                    {
                        return this._RECOVERY;
                    }
                    set
                    {
                        if ((this._RECOVERY != value))
                        {
                            this._RECOVERY = value;
                        }
                    }
                }

                public string GPFADD
                {
                    get
                    {
                        return this._GPF_ADD;
                    }
                    set
                    {
                        if ((this._GPF_ADD != value))
                        {
                            this._GPF_ADD = value;
                        }
                    }
                }

                public string GPF_ADV
                {
                    get
                    {
                        return this._GPF_ADV;
                    }
                    set
                    {
                        if ((this._GPF_ADV != value))
                        {
                            this._GPF_ADV = value;
                        }
                    }
                }

                public string IT
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

                public string PT
                {
                    get
                    {
                        return this._PT;
                    }
                    set
                    {
                        if ((this._PT != value))
                        {
                            this._PT = value;
                        }
                    }
                }

                public string LIC
                {
                    get
                    {
                        return this._LIC;
                    }
                    set
                    {
                        if ((this._LIC != value))
                        {
                            this._LIC = value;
                        }
                    }
                }

                public string GIS
                {
                    get
                    {
                        return this._GIS;
                    }
                    set
                    {
                        if ((this._GIS != value))
                        {
                            this._GIS = value;
                        }
                    }
                }

                public string LISEFEE
                {
                    get
                    {
                        return this._LISEFEE;
                    }
                    set
                    {
                        if ((this._LISEFEE != value))
                        {
                            this._LISEFEE = value;
                        }
                    }
                }

                public string SOCIETY
                {
                    get
                    {
                        return this._SOCIETY;
                    }
                    set
                    {
                        if ((this._SOCIETY != value))
                        {
                            this._SOCIETY = value;
                        }
                    }
                }

                public string LWP
                {
                    get
                    {
                        return this._LWP;
                    }
                    set
                    {
                        if ((this._LWP != value))
                        {
                            this._LWP = value;
                        }
                    }
                }

                public string DE
                {
                    get
                    {
                        return this._DE;
                    }
                    set
                    {
                        if ((this._DE != value))
                        {
                            this._DE = value;
                        }
                    }
                }

                public string G80
                {
                    get
                    {
                        return this._G80;
                    }
                    set
                    {
                        if ((this._G80 != value))
                        {
                            this._G80 = value;
                        }
                    }
                }

                public string CPF
                {
                    get
                    {
                        return this._CPF;
                    }
                    set
                    {
                        if ((this._CPF != value))
                        {
                            this._CPF = value;
                        }
                    }
                }

                public string CPFLOAN
                {
                    get
                    {
                        return this._CPF_LOAN;
                    }
                    set
                    {
                        if ((this._CPF_LOAN != value))
                        {
                            this._CPF_LOAN = value;
                        }
                    }
                }



                public string GPFLOAN
                {
                    get
                    {
                        return this._GPFLOAN;
                    }
                    set
                    {
                        if ((this._GPFLOAN != value))
                        {
                            this._GPFLOAN = value;
                        }
                    }
                }


                public string PHONE
                {
                    get
                    {
                        return this._PHONE;
                    }
                    set
                    {
                        if ((this._PHONE != value))
                        {
                            this._PHONE = value;
                        }
                    }
                }

                public string MEDICAL
                {
                    get
                    {
                        return this._MEDICAL;
                    }
                    set
                    {
                        if ((this._MEDICAL != value))
                        {
                            this._MEDICAL = value;
                        }
                    }
                }

                public string RD
                {
                    get
                    {
                        return this._RD;
                    }
                    set
                    {
                        if ((this._RD != value))
                        {
                            this._RD = value;
                        }
                    }
                }

                public string HONO
                {
                    get
                    {
                        return this._HONO;
                    }
                    set
                    {
                        if ((this._HONO != value))
                        {
                            this._HONO = value;
                        }
                    }
                }



                public string GPF
                {
                    get
                    {
                        return this._GPF;
                    }
                    set
                    {
                        if ((this._GPF != value))
                        {
                            this._GPF = value;
                        }
                    }
                }

                public string PRINCIPAL
                {
                    get
                    {
                        return this._PRINCIPAL;
                    }
                    set
                    {
                        if ((this._PRINCIPAL != value))
                        {
                            this._PRINCIPAL = value;
                        }
                    }
                }

                public string BANK
                {
                    get
                    {
                        return this._BANK;
                    }
                    set
                    {
                        if ((this._BANK != value))
                        {
                            this._BANK = value;
                        }
                    }
                }

                public string SECTIONXI
                {
                    get
                    {
                        return this._SECTIONXI;
                    }
                    set
                    {
                        if ((this._SECTIONXI != value))
                        {
                            this._SECTIONXI = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> FROMDATE
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

                public System.Nullable<decimal> CPFPER
                {
                    get
                    {
                        return this._CPFPER;
                    }
                    set
                    {
                        if ((this._CPFPER != value))
                        {
                            this._CPFPER = value;
                        }
                    }
                }

                public string LICBRANCH
                {
                    get
                    {
                        return this._LICBRANCH;
                    }
                    set
                    {
                        if ((this._LICBRANCH != value))
                        {
                            this._LICBRANCH = value;
                        }
                    }
                }

                public string TANNO
                {
                    get
                    {
                        return this._TANNO;
                    }
                    set
                    {
                        if ((this._TANNO != value))
                        {
                            this._TANNO = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> PFFSDATE
                {
                    get
                    {
                        return this._PFFSDATE;
                    }
                    set
                    {
                        if ((this._PFFSDATE != value))
                        {
                            this._PFFSDATE = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> CPFFROMDATE
                {
                    get
                    {
                        return this._CPFFROMDATE;
                    }
                    set
                    {
                        if ((this._CPFFROMDATE != value))
                        {
                            this._CPFFROMDATE = value;
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

                public string BANKLOCATION
                {
                    get
                    {
                        return this._BANKLOCATION;
                    }
                    set
                    {
                        if ((this._BANKLOCATION != value))
                        {
                            this._BANKLOCATION = value;
                        }
                    }
                }

                public string SECTIONXII
                {
                    get
                    {
                        return this._SECTIONXII;
                    }
                    set
                    {
                        if ((this._SECTIONXII != value))
                        {
                            this._SECTIONXII = value;
                        }
                    }
                }

                public System.Nullable<decimal> DAPER
                {
                    get
                    {
                        return this._DAPER;
                    }
                    set
                    {
                        if ((this._DAPER != value))
                        {
                            this._DAPER = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> TODATE
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

                public System.Nullable<Int64> ACCOUNT
                {
                    get
                    {
                        return this._ACCOUNT;
                    }
                    set
                    {
                        if ((this._ACCOUNT != value))
                        {
                            this._ACCOUNT = value;
                        }
                    }
                }

                public string PACNO
                {
                    get
                    {
                        return this._PACNO;
                    }
                    set
                    {
                        if ((this._PACNO != value))
                        {
                            this._PACNO = value;
                        }
                    }
                }

                public string PANNO
                {
                    get
                    {
                        return this._PANNO;
                    }
                    set
                    {
                        if ((this._PANNO != value))
                        {
                            this._PANNO = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> PFFEDATE
                {
                    get
                    {
                        return this._PFFEDATE;
                    }
                    set
                    {
                        if ((this._PFFEDATE != value))
                        {
                            this._PFFEDATE = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> CPFTODATE
                {
                    get
                    {
                        return this._CPFTODATE;
                    }
                    set
                    {
                        if ((this._CPFTODATE != value))
                        {
                            this._CPFTODATE = value;
                        }
                    }
                }
                public System.Nullable<Int64> LINKACC
                {
                    get
                    {
                        return this._LINK_WITH_ACC;
                    }
                    set
                    {
                        if ((this._LINK_WITH_ACC != value))
                        {
                            this._LINK_WITH_ACC = value;
                        }
                    }
                }




                public System.Nullable<Int64> GP_Status
                {
                    get
                    {
                        return this._GP_Status;
                    }
                    set
                    {
                        if ((this._GP_Status != value))
                        {
                            this._GP_Status = value;
                        }
                    }
                }




                public System.Nullable<Int64> DP_Status
                {
                    get
                    {
                        return this._DP_Status;
                    }
                    set
                    {
                        if ((this._DP_Status != value))
                        {
                            this._DP_Status = value;
                        }
                    }
                }

                public bool IsAutoUserCreated
                {
                    get
                    {
                        return this._IsAutoUserCreated;
                    }
                    set
                    {
                        if ((this._IsAutoUserCreated != value))
                        {
                            this._IsAutoUserCreated = value;
                        }
                    }
                }


                public string ITREGNO
                {
                    get
                    {
                        return this._ITREGNO;
                    }
                    set
                    {
                        if ((this._ITREGNO != value))
                        {
                            this._ITREGNO = value;
                        }
                    }
                }


                public string PTREGNO
                {
                    get
                    {
                        return this._PTREGNO;
                    }
                    set
                    {
                        if ((this._PTREGNO != value))
                        {
                            this._PTREGNO = value;
                        }
                    }
                }

                public string BANKLOAN
                {
                    get
                    {
                        return this.__BANKLOAN;
                    }
                    set
                    {
                        if ((this.__BANKLOAN != value))
                        {
                            this.__BANKLOAN = value;
                        }
                    }
                }

                public string PUJAFUND
                {
                    get
                    {
                        return this._PUJAFUND;
                    }
                    set
                    {
                        if ((this._PUJAFUND != value))
                        {
                            this._PUJAFUND = value;
                        }
                    }
                }

                public string NTFUND
                {
                    get
                    {
                        return this._NTFUND;
                    }
                    set
                    {
                        if ((this._NTFUND != value))
                        {
                            this._NTFUND = value;
                        }
                    }
                }

                public string CRFUND
                {
                    get
                    {
                        return this._CRFUND;
                    }
                    set
                    {
                        if ((this._CRFUND != value))
                        {
                            this._CRFUND = value;
                        }
                    }
                }

                public string OTHERDED
                {
                    get
                    {
                        return this._OTHERDED;
                    }
                    set
                    {
                        if ((this._OTHERDED != value))
                        {
                            this._OTHERDED = value;
                        }
                    }
                }

                public string PAYUNITNO
                {
                    get
                    {
                        return this._PAY_UNIT_NO;
                    }
                    set
                    {
                        if ((this._PAY_UNIT_NO != value))
                        {
                            this._PAY_UNIT_NO = value;
                        }
                    }
                }

                public string CODENO
                {
                    get
                    {
                        return this._CODENO;
                    }
                    set
                    {
                        if ((this._CODENO != value))
                        {
                            this._CODENO = value;
                        }
                    }
                }

                public string ZONE
                {
                    get
                    {
                        return this._ZONE;
                    }
                    set
                    {
                        if ((this._ZONE != value))
                        {
                            this._ZONE = value;
                        }
                    }
                }

                public string WARD
                {
                    get
                    {
                        return this._WARD;
                    }
                    set
                    {
                        if ((this._WARD != value))
                        {
                            this._WARD = value;
                        }
                    }
                }
                public string REGISTRAR
                {
                    get
                    {
                        return this._REGISTRAR;
                    }
                    set
                    {
                        if ((this._REGISTRAR != value))
                        {
                            this._REGISTRAR = value;
                        }
                    }
                }














                public string COLNAME
                {
                    get
                    {
                        return this._COLNAME;
                    }
                    set
                    {
                        if ((this._COLNAME != value))
                        {
                            this._COLNAME = value;
                        }
                    }
                }

                public string SOCNAME
                {
                    get
                    {
                        return this._SOCNAME;
                    }
                    set
                    {
                        if ((this._SOCNAME != value))
                        {
                            this._SOCNAME = value;
                        }
                    }
                }


                public string COMPANY
                {
                    get
                    {
                        return this._COMPANY;
                    }
                    set
                    {
                        if ((this._COMPANY != value))
                        {
                            this._COMPANY = value;
                        }
                    }
                }

                public string ACCPW
                {
                    get
                    {
                        return this._ACCPW;
                    }
                    set
                    {
                        if ((this._ACCPW != value))
                        {
                            this._ACCPW = value;
                        }
                    }
                }

                public int SINGLE_INV
                {
                    get { return _SINGLE_INV; }
                    set { _SINGLE_INV = value; }
                }


                public double RWIDTH
                {
                    get { return _RWIDTH; }
                    set { _RWIDTH = value; }
                }

                public double RHEIGHT
                {
                    get { return _RHEIGHT; }
                    set { _RHEIGHT = value; }
                }

                public string SALPASSWORD
                {
                    get
                    {
                        return this._SALPASSWORD;
                    }
                    set
                    {
                        if ((this._SALPASSWORD != value))
                        {
                            this._SALPASSWORD = value;
                        }
                    }
                }

                public double GPFPER
                {
                    get { return _GPFPER; }
                    set { _GPFPER = value; }
                }

                public double ENAMESIZE
                {
                    get { return _ENAMESIZE; }
                    set { _ENAMESIZE = value; }
                }


                public int ANOSIZE
                {
                    get { return _ANOSIZE; }
                    set { _ANOSIZE = value; }
                }

                public int AMTSIZE
                {
                    get { return _AMTSIZE; }
                    set { _AMTSIZE = value; }
                }


                public string LWP_CAL
                {
                    get
                    {
                        return this._LWP_CAL;
                    }
                    set
                    {
                        if ((this._LWP_CAL != value))
                        {
                            this._LWP_CAL = value;
                        }
                    }
                }

                public Boolean DOSFONT
                {
                    get { return _DOSFONT; }
                    set { _DOSFONT = value; }
                }


                public Boolean ZERO
                {
                    get { return _ZERO; }
                    set { _ZERO = value; }
                }


                public double RD_PER
                {
                    get { return _RD_PER; }
                    set { _RD_PER = value; }
                }


                public int LWP_PDAY
                {
                    get { return _LWP_PDAY; }
                    set { _LWP_PDAY = value; }
                }
                public string LICEFEE_FIELD
                {
                    get
                    {
                        return this._LICEFEE_FIELD;
                    }
                    set
                    {
                        if ((this._LICEFEE_FIELD != value))
                        {
                            this._LICEFEE_FIELD = value;
                        }
                    }
                }

                public int PROPOSED_SAL
                {
                    get { return _PROPOSED_SAL; }
                    set { _PROPOSED_SAL = value; }
                }

                public int MODULE_NO
                {
                    get { return _MODULE_NO; }
                    set { _MODULE_NO = value; }
                }

                public int LINK_NO
                {
                    get { return _LINK_NO; }
                    set { _LINK_NO = value; }
                }
                private string _OverTimeHead = string.Empty;

                public string OverTimeHead
                {
                    get { return _OverTimeHead; }
                    set { _OverTimeHead = value; }
                }
                public string LEAVE_APPROVAL
                {
                    get
                    {
                        return this._LEAVE_APPROVAL;
                    }
                    set
                    {
                        if ((this._LEAVE_APPROVAL != value))
                        {
                            this._LEAVE_APPROVAL = value;
                        }
                    }
                }

                public int PH_AMT
                {
                    get { return _PH_AMT; }
                    set { _PH_AMT = value; }
                }

                public string STAFFNO
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

                public System.Nullable<decimal> EPFAMOUNT
                {
                    get
                    {
                        return this._EPFAMOUNT;
                    }
                    set
                    {
                        if ((this._EPFAMOUNT != value))
                        {
                            this._EPFAMOUNT = value;
                        }
                    }
                }

                public string STAFF_APPLICABLE
                {
                    get
                    {
                        return this._STAFF_APPLICABLE;
                    }
                    set
                    {
                        if ((this._STAFF_APPLICABLE != value))
                        {
                            this._STAFF_APPLICABLE = value;
                        }
                    }
                }






                // Amol sawarkar 

                public string ESICNO
                {
                    get
                    {
                        return this._ESICNO;
                    }
                    set
                    {
                        if ((this._ESICNO != value))
                        {
                            this._ESICNO = value;
                        }
                    }
                }

                public System.Nullable<decimal> ESI_LIMIT_HP
                {
                    get
                    {
                        return this._ESI_LIMIT_HP;
                    }
                    set
                    {
                        if ((this._ESI_LIMIT_HP != value))
                        {
                            this._ESI_LIMIT_HP = value;
                        }
                    }
                }

                public System.Nullable<decimal> ESI_PER_HP
                {
                    get
                    {
                        return this._ESI_PER_HP;
                    }
                    set
                    {
                        if ((this._ESI_PER_HP != value))
                        {
                            this._ESI_PER_HP = value;
                        }
                    }
                }

                public System.Nullable<decimal> EmployerContribution
                {
                    get
                    {
                        return this._EmployerContribution;
                    }
                    set
                    {
                        if ((this._EmployerContribution != value))
                        {
                            this._EmployerContribution = value;
                        }
                    }
                }
                public System.Nullable<decimal> ESIC_LIMIT
                {
                    get
                    {
                        return this._ESIC_LIMIT;
                    }
                    set
                    {
                        if ((this._ESIC_LIMIT != value))
                        {
                            this._ESIC_LIMIT = value;
                        }
                    }
                }


                public System.Nullable<System.DateTime> ESIFIRSTFROMDATE
                {
                    get
                    {
                        return this._ESIFIRSTFROMDATE;
                    }
                    set
                    {
                        if ((this._ESIFIRSTFROMDATE != value))
                        {
                            this._ESIFIRSTFROMDATE = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> ESIFIRSTTODATE
                {
                    get
                    {
                        return this._ESIFIRSTTODATE;
                    }
                    set
                    {
                        if ((this._ESIFIRSTTODATE != value))
                        {
                            this._ESIFIRSTTODATE = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> ESISECONDFROMDATE
                {
                    get
                    {
                        return this._ESISECONDFROMDATE;
                    }
                    set
                    {
                        if ((this._ESISECONDFROMDATE != value))
                        {
                            this._ESISECONDFROMDATE = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> ESISECONDTODATE
                {
                    get
                    {
                        return this._ESISECONDTODATE;
                    }
                    set
                    {
                        if ((this._ESISECONDTODATE != value))
                        {
                            this._ESISECONDTODATE = value;
                        }
                    }
                }



                public System.Nullable<System.DateTime> EmpPayslipShowFromDate
                {
                    get
                    {
                        return this._EmpPayslipShowFromDate;
                    }
                    set
                    {
                        if ((this._EmpPayslipShowFromDate != value))
                        {
                            this._EmpPayslipShowFromDate = value;
                        }
                    }
                }


                // 14-09-2022

                public byte[] PhotoSign
                {
                    get { return _PhotoSign; }
                    set { _PhotoSign = value; }
                }


                # endregion

            }
        }
    }
}