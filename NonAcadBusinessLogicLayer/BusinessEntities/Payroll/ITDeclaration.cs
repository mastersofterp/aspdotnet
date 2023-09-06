using System;
using System.Data;
using System.Web;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {

            public class ITDeclaration
            {
                #region Private Members
                private int _ITNO;
                private int _IDNO;
                private System.Nullable<System.DateTime> _FROMDATE = DateTime.MinValue;
                private System.Nullable<System.DateTime> _TODATE = DateTime.MinValue;
                private string _VIA1;
                private string _VIA2;
                private string _VIA3;
                private string _VIA4;
                private string _VIA5;
                private string _VIA6;
                private string _VIA7;
                private string _VIA8;
                private string _VIA9;
                private string _VIA10;
                private string _VIA11;
                private string _VIA12;
                private string _VIA13;

                //------
                private string _VIB1;
                private string _VIB2;
                private string _VIB3;
                private string _VIB4;
                private string _VIB5;
                private string _VIB6;
                private string _VIB7;
                private string _VIB8;
                private string _VIB9;
                private string _VIB10;
                private string _VIB11;
                private string _VIB12;
                private string _VIB13;

                //------
                private decimal _VIAMT1;
                private decimal _VIAMT2;
                private decimal _VIAMT3;
                private decimal _VIAMT4;
                private decimal _VIAMT5;
                private decimal _VIAMT6;
                private decimal _VIAMT7;
                private decimal _VIAMT8;
                private decimal _VIAMT9;
                private decimal _VIAMT10;
                private decimal _VIAMT11;
                private decimal _VIAMT12;
                private decimal _VIAMT13;

                //-------
                private string _REBP1;
                private string _REBP2;
                private string _REBP3;
                private string _REBP4;
                private string _REBP5;
                private string _REBP6;
                private string _REBP7;
                private string _REBP8;
                private string _REBP9;
                private string _REBP10;
                private string _REBP11;
                private string _REBP12;
                private string _REBP13;
                private string _REBP14;
                private string _REBP15;
                private string _REBP16;
                private string _REBP17;
                private string _REBP18;
                private string _REBP19;
                private string _REBP20;

                //-------
                private decimal _REBM1;
                private decimal _REBM2;
                private decimal _REBM3;
                private decimal _REBM4;
                private decimal _REBM5;
                private decimal _REBM6;
                private decimal _REBM7;
                private decimal _REBM8;
                private decimal _REBM9;
                private decimal _REBM10;
                private decimal _REBM11;
                private decimal _REBM12;
                private decimal _REBM13;
                private decimal _REBM14;
                private decimal _REBM15;
                private decimal _REBM16;
                private decimal _REBM17;
                private decimal _REBM18;
                private decimal _REBM19;
                private decimal _REBM20;

                //-------
                private decimal _REBT1;
                private decimal _REBT2;
                private decimal _REBT3;
                private decimal _REBT4;
                private decimal _REBT5;
                private decimal _REBT6;
                private decimal _REBT7;
                private decimal _REBT8;
                private decimal _REBT9;
                private decimal _REBT10;
                private decimal _REBT11;
                private decimal _REBT12;
                private decimal _REBT13;
                private decimal _REBT14;
                private decimal _REBT15;
                private decimal _REBT16;
                private decimal _REBT17;
                private decimal _REBT18;
                private decimal _REBT19;
                private decimal _REBT20;

                //-------
                private decimal _REBD1;
                private decimal _REBD2;
                private decimal _REBD3;
                private decimal _REBD4;
                private decimal _REBD5;
                private decimal _REBD6;
                private decimal _REBD7;
                private decimal _REBD8;
                private decimal _REBD9;
                private decimal _REBD10;
                private decimal _REBD11;
                private decimal _REBD12;
                private decimal _REBD13;
                private decimal _REBD14;
                private decimal _REBD15;
                private decimal _REBD16;
                private decimal _REBD17;
                private decimal _REBD18;
                private decimal _REBD19;
                private decimal _REBD20;
                //-------
                private decimal _BALAMT;

                private string _INCP1;
                private string _INCP2;
                private string _INCP3;
                private string _INCP4;
                private string _INCP5;

                //------
                private decimal _INCAMT1;
                private decimal _INCAMT2;
                private decimal _INCAMT3;
                private decimal _INCAMT4;
                private decimal _INCAMT5;

                private System.Nullable<System.DateTime> _INCDATE = DateTime.MinValue;

                private string _REMARK;

                private string _COLLEGE_CODE;

                private decimal _OTHERINCTDS;

         
                private DataTable _VIA_HEADS_Table = null;
                private string _FINYEAR;
                private DataTable _REBATE_HEADS_Table = null;

                private bool _IsLock = false;
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
                public DataTable REBATE_HEADS_Table
                {
                    get
                    {
                        return this._REBATE_HEADS_Table;
                    }
                    set
                    {
                        if ((this._REBATE_HEADS_Table != value))
                        {
                            this._REBATE_HEADS_Table = value;
                        }
                    }
                }
                public DataTable VIA_HEADS_Table
                {
                    get
                    {
                        return this._VIA_HEADS_Table;
                    }
                    set
                    {
                        if ((this._VIA_HEADS_Table != value))
                        {
                            this._VIA_HEADS_Table = value;
                        }
                    }
                }
                public bool IsLock
                {
                    get { return _IsLock; }
                    set { _IsLock = value; }
                }

                private bool _IsFinalSubmit = false;
               
                public bool IsFinalSubmit
                {
                    get { return _IsFinalSubmit; }
                    set { _IsFinalSubmit = value; }
                }

                private bool _IsMailSend = false;

                public bool IsMailSend
                {
                    get { return _IsMailSend; }
                    set { _IsMailSend = value; }
                }

                //PERQUISITES
                private decimal _VALUE3;

                public decimal VALUE3
                {
                    get { return _VALUE3; }
                    set { _VALUE3 = value; }
                }
                private decimal _VALUE4;

                public decimal VALUE4
                {
                    get { return _VALUE4; }
                    set { _VALUE4 = value; }
                }
                private decimal _VALUE5;

                public decimal VALUE5
                {
                    get { return _VALUE5; }
                    set { _VALUE5 = value; }
                }


                private string _PERQUISITE;

                public string PERQUISITE
                {
                    get { return _PERQUISITE; }
                    set { _PERQUISITE = value; }
                }

                private int _COLLEGENO;

                public int COLLEGENO
                {
                    get { return _COLLEGENO; }
                    set { _COLLEGENO = value; }
                }

                private int _PERQUISITEID;

                public int PERQUISITEID
                {
                    get { return _PERQUISITEID; }
                    set { _PERQUISITEID = value; }
                }

                private decimal _SUMPERQUISITE;

                public decimal SUMPERQUISITE
                {
                    get { return _SUMPERQUISITE; }
                    set { _SUMPERQUISITE = value; }
                }
                //PERQUISITES END

                private decimal _80CCDNPS;

                public decimal CCDNPS
                {
                    get { return _80CCDNPS; }
                    set { _80CCDNPS = value; }
                }

                private decimal _RGESS80CCG;

                public decimal RGESS80CCG
                {
                    get { return _RGESS80CCG; }
                    set { _RGESS80CCG = value; }
                }


                //Sachin


                private int _CHAPVI_ID;

                public int CHAPVI_ID
                {
                    get { return _CHAPVI_ID; }
                    set { _CHAPVI_ID = value; }
                }



                private int _CNO;

                public int CNO
                {
                    get { return _CNO; }
                    set { _CNO = value; }
                }


                private string _HouseOwnerName;

                public string HouseOwnerName
                {
                    get { return _HouseOwnerName; }
                    set { _HouseOwnerName = value; }
                }



                private string _PanNumber;

                public string PanNumber
                {
                    get { return _PanNumber; }
                    set { _PanNumber = value; }
                }


                private DateTime _DeclarationDate;

                public DateTime DeclarationDate
                {
                    get { return _DeclarationDate; }
                    set { _DeclarationDate = value; }
                }

                private double _Amount;

                public double Amount
                {
                    get { return _Amount; }
                    set { _Amount = value; }
                }

                private string _FinYear;

                public string FinYear
                {
                    get { return _FinYear; }
                    set { _FinYear = value; }
                }

                private int _RebateHeadId;
                public int RebateHeadId
                {
                    get { return _RebateHeadId; }
                    set { _RebateHeadId = value; }
                }


                private int _FNO;
                public int FNO
                {
                    get { return _FNO; }
                    set { _FNO = value; }
                }

                private string _PAYHEAD;

                public string PAYHEAD
                {
                    get { return _PAYHEAD; }
                    set { _PAYHEAD = value; }
                }

                private string _DEDHEAD;

                public string DEDHEAD
                {
                    get { return _DEDHEAD; }
                    set { _DEDHEAD = value; }
                }

                private string _ST;

                public string ST
                {
                    get { return _ST; }
                    set { _ST = value; }
                }



                private string _EmpDocumentName;

                public string EmpDocumentName
                {
                    get { return _EmpDocumentName; }
                    set { _EmpDocumentName = value; }
                }



                private string _EmpDocumentUrl;

                public string EmpDocumentUrl
                {
                    get { return _EmpDocumentUrl; }
                    set { _EmpDocumentUrl = value; }
                }


                private string _DocumentType;

                public string DocumentType
                {
                    get { return _DocumentType; }
                    set { _DocumentType = value; }
                }

                private string _CollegeNo;

                public string CollegeNo
                {
                    get { return _CollegeNo; }
                    set { _CollegeNo = value; }
                }



                #endregion

                #region Public Properties

                public int ITNO
                {
                    get
                    {
                        return this._ITNO;
                    }
                    set
                    {
                        if ((this._ITNO != value))
                        {
                            this._ITNO = value;
                        }
                    }
                }
                private int _IT_RULE_ID;
                public int IT_RULE_ID
                {
                    get
                    {
                        return this._IT_RULE_ID;
                    }
                    set
                    {
                        if ((this._IT_RULE_ID != value))
                        {
                            this._IT_RULE_ID = value;
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

                public string VIA1
                {
                    get
                    {
                        return this._VIA1;
                    }
                    set
                    {
                        this._VIA1 = value;

                    }
                }

                public string VIA2
                {
                    get
                    {
                        return this._VIA2;
                    }
                    set
                    {
                        this._VIA2 = value;

                    }
                }

                public string VIA3
                {
                    get
                    {
                        return this._VIA3;
                    }
                    set
                    {
                        this._VIA3 = value;

                    }
                }

                public string VIA4
                {
                    get
                    {
                        return this._VIA4;
                    }
                    set
                    {
                        this._VIA4 = value;

                    }
                }

                public string VIA5
                {
                    get
                    {
                        return this._VIA5;
                    }
                    set
                    {
                        this._VIA5 = value;

                    }
                }

                public string VIA6
                {
                    get
                    {
                        return this._VIA6;
                    }
                    set
                    {
                        this._VIA6 = value;

                    }
                }
                public string VIA7
                {
                    get
                    {
                        return this._VIA7;
                    }
                    set
                    {
                        this._VIA7 = value;

                    }
                }
                public string VIA8
                {
                    get
                    {
                        return this._VIA8;
                    }
                    set
                    {
                        this._VIA8 = value;

                    }
                }
                public string VIA9
                {
                    get
                    {
                        return this._VIA9;
                    }
                    set
                    {
                        this._VIA9 = value;

                    }
                }
                public string VIA10
                {
                    get
                    {
                        return this._VIA10;
                    }
                    set
                    {
                        this._VIA10 = value;

                    }
                }

                public string VIA11
                {
                    get
                    {
                        return this._VIA11;
                    }
                    set
                    {
                        this._VIA11 = value;

                    }
                }
                public string VIA12
                {
                    get
                    {
                        return this._VIA12;
                    }
                    set
                    {
                        this._VIA12 = value;

                    }
                }
                public string VIA13
                {
                    get
                    {
                        return this._VIA13;
                    }
                    set
                    {
                        this._VIA13 = value;

                    }
                }
                //--------


                public string VIB1
                {
                    get
                    {
                        return this._VIB1;
                    }
                    set
                    {
                        this._VIB1 = value;

                    }
                }

                public string VIB2
                {
                    get
                    {
                        return this._VIB2;
                    }
                    set
                    {
                        this._VIB2 = value;

                    }
                }

                public string VIB3
                {
                    get
                    {
                        return this._VIB3;
                    }
                    set
                    {
                        this._VIB3 = value;

                    }
                }

                public string VIB4
                {
                    get
                    {
                        return this._VIB4;
                    }
                    set
                    {
                        this._VIB4 = value;

                    }
                }

                public string VIB5
                {
                    get
                    {
                        return this._VIB5;
                    }
                    set
                    {
                        this._VIB5 = value;

                    }
                }

                public string VIB6
                {
                    get
                    {
                        return this._VIB6;
                    }
                    set
                    {
                        this._VIB6 = value;

                    }
                }
                public string VIB7
                {
                    get
                    {
                        return this._VIB7;
                    }
                    set
                    {
                        this._VIB7 = value;

                    }
                }
                public string VIB8
                {
                    get
                    {
                        return this._VIB8;
                    }
                    set
                    {
                        this._VIB8 = value;

                    }
                }
                public string VIB9
                {
                    get
                    {
                        return this._VIB9;
                    }
                    set
                    {
                        this._VIB9 = value;

                    }
                }
                public string VIB10
                {
                    get
                    {
                        return this._VIB10;
                    }
                    set
                    {
                        this._VIB10 = value;

                    }
                }
                public string VIB11
                {
                    get
                    {
                        return this._VIB11;
                    }
                    set
                    {
                        this._VIB11 = value;

                    }
                }
                public string VIB12
                {
                    get
                    {
                        return this._VIB12;
                    }
                    set
                    {
                        this._VIB12 = value;

                    }
                }
                public string VIB13
                {
                    get
                    {
                        return this._VIB13;
                    }
                    set
                    {
                        this._VIB13 = value;

                    }
                }
                //---------

                public decimal VIAMT1
                {
                    get
                    {
                        return this._VIAMT1;
                    }
                    set
                    {
                        this._VIAMT1 = value;

                    }
                }

                public decimal VIAMT2
                {
                    get
                    {
                        return this._VIAMT2;
                    }
                    set
                    {
                        this._VIAMT2 = value;

                    }
                }

                public decimal VIAMT3
                {
                    get
                    {
                        return this._VIAMT3;
                    }
                    set
                    {
                        this._VIAMT3 = value;

                    }
                }

                public decimal VIAMT4
                {
                    get
                    {
                        return this._VIAMT4;
                    }
                    set
                    {
                        this._VIAMT4 = value;

                    }
                }

                public decimal VIAMT5
                {
                    get
                    {
                        return this._VIAMT5;
                    }
                    set
                    {
                        this._VIAMT5 = value;

                    }
                }

                public decimal VIAMT6
                {
                    get
                    {
                        return this._VIAMT6;
                    }
                    set
                    {
                        this._VIAMT6 = value;

                    }
                }
                public decimal VIAMT7
                {
                    get
                    {
                        return this._VIAMT7;
                    }
                    set
                    {
                        this._VIAMT7 = value;

                    }
                }
                public decimal VIAMT8
                {
                    get
                    {
                        return this._VIAMT8;
                    }
                    set
                    {
                        this._VIAMT8 = value;

                    }
                }
                public decimal VIAMT9
                {
                    get
                    {
                        return this._VIAMT9;
                    }
                    set
                    {
                        this._VIAMT9 = value;

                    }
                }
                public decimal VIAMT10
                {
                    get
                    {
                        return this._VIAMT10;
                    }
                    set
                    {
                        this._VIAMT10 = value;

                    }
                }
                public decimal VIAMT11
                {
                    get
                    {
                        return this._VIAMT11;
                    }
                    set
                    {
                        this._VIAMT11 = value;

                    }
                }
                public decimal VIAMT12
                {
                    get
                    {
                        return this._VIAMT12;
                    }
                    set
                    {
                        this._VIAMT12 = value;

                    }
                }
                public decimal VIAMT13
                {
                    get
                    {
                        return this._VIAMT13;
                    }
                    set
                    {
                        this._VIAMT13 = value;

                    }
                }
                //--------

                public string REBP1
                {
                    get
                    {
                        return this._REBP1;
                    }
                    set
                    {
                        this._REBP1 = value;

                    }
                }

                public string REBP2
                {
                    get
                    {
                        return this._REBP2;
                    }
                    set
                    {
                        this._REBP2 = value;

                    }
                }

                public string REBP3
                {
                    get
                    {
                        return this._REBP3;
                    }
                    set
                    {
                        this._REBP3 = value;

                    }
                }

                public string REBP4
                {
                    get
                    {
                        return this._REBP4;
                    }
                    set
                    {
                        this._REBP4 = value;

                    }
                }

                public string REBP5
                {
                    get
                    {
                        return this._REBP5;
                    }
                    set
                    {
                        this._REBP5 = value;

                    }
                }

                public string REBP6
                {
                    get
                    {
                        return this._REBP6;
                    }
                    set
                    {
                        this._REBP6 = value;

                    }
                }

                public string REBP7
                {
                    get
                    {
                        return this._REBP7;
                    }
                    set
                    {
                        this._REBP7 = value;

                    }
                }

                public string REBP8
                {
                    get
                    {
                        return this._REBP8;
                    }
                    set
                    {
                        this._REBP8 = value;

                    }
                }

                public string REBP9
                {
                    get
                    {
                        return this._REBP9;
                    }
                    set
                    {
                        this._REBP9 = value;

                    }
                }

                public string REBP10
                {
                    get
                    {
                        return this._REBP10;
                    }
                    set
                    {
                        this._REBP10 = value;

                    }
                }

                public string REBP11
                {
                    get
                    {
                        return this._REBP11;
                    }
                    set
                    {
                        this._REBP11 = value;

                    }
                }

                public string REBP12
                {
                    get
                    {
                        return this._REBP12;
                    }
                    set
                    {
                        this._REBP12 = value;

                    }
                }

                public string REBP13
                {
                    get
                    {
                        return this._REBP13;
                    }
                    set
                    {
                        this._REBP13 = value;

                    }
                }

                public string REBP14
                {
                    get
                    {
                        return this._REBP14;
                    }
                    set
                    {
                        this._REBP14 = value;

                    }
                }

                public string REBP15
                {
                    get
                    {
                        return this._REBP15;
                    }
                    set
                    {
                        this._REBP15 = value;

                    }
                }

                public string REBP16
                {
                    get
                    {
                        return this._REBP16;
                    }
                    set
                    {
                        this._REBP16 = value;

                    }
                }

                public string REBP17
                {
                    get
                    {
                        return this._REBP17;
                    }
                    set
                    {
                        this._REBP17 = value;

                    }
                }

                public string REBP18
                {
                    get
                    {
                        return this._REBP18;
                    }
                    set
                    {
                        this._REBP18 = value;

                    }
                }

                public string REBP19
                {
                    get
                    {
                        return this._REBP19;
                    }
                    set
                    {
                        this._REBP19 = value;

                    }
                }

                public string REBP20
                {
                    get
                    {
                        return this._REBP20;
                    }
                    set
                    {
                        this._REBP20 = value;

                    }
                }
                //-------

                public decimal REBM1
                {
                    get
                    {
                        return this._REBM1;
                    }
                    set
                    {
                        this._REBM1 = value;

                    }
                }

                public decimal REBM2
                {
                    get
                    {
                        return this._REBM2;
                    }
                    set
                    {
                        this._REBM2 = value;

                    }
                }

                public decimal REBM3
                {
                    get
                    {
                        return this._REBM3;
                    }
                    set
                    {
                        this._REBM3 = value;

                    }
                }

                public decimal REBM4
                {
                    get
                    {
                        return this._REBM4;
                    }
                    set
                    {
                        this._REBM4 = value;

                    }
                }

                public decimal REBM5
                {
                    get
                    {
                        return this._REBM5;
                    }
                    set
                    {
                        this._REBM5 = value;

                    }
                }

                public decimal REBM6
                {
                    get
                    {
                        return this._REBM6;
                    }
                    set
                    {
                        this._REBM6 = value;

                    }
                }

                public decimal REBM7
                {
                    get
                    {
                        return this._REBM7;
                    }
                    set
                    {
                        this._REBM7 = value;

                    }
                }

                public decimal REBM8
                {
                    get
                    {
                        return this._REBM8;
                    }
                    set
                    {
                        this._REBM8 = value;

                    }
                }

                public decimal REBM9
                {
                    get
                    {
                        return this._REBM9;
                    }
                    set
                    {
                        this._REBM9 = value;

                    }
                }

                public decimal REBM10
                {
                    get
                    {
                        return this._REBM10;
                    }
                    set
                    {
                        this._REBM10 = value;

                    }
                }

                public decimal REBM11
                {
                    get
                    {
                        return this._REBM11;
                    }
                    set
                    {
                        this._REBM11 = value;

                    }
                }

                public decimal REBM12
                {
                    get
                    {
                        return this._REBM12;
                    }
                    set
                    {
                        this._REBM12 = value;

                    }
                }

                public decimal REBM13
                {
                    get
                    {
                        return this._REBM13;
                    }
                    set
                    {
                        this._REBM13 = value;

                    }
                }

                public decimal REBM14
                {
                    get
                    {
                        return this._REBM14;
                    }
                    set
                    {
                        this._REBM14 = value;

                    }
                }

                public decimal REBM15
                {
                    get
                    {
                        return this._REBM15;
                    }
                    set
                    {
                        this._REBM15 = value;

                    }
                }

                public decimal REBM16
                {
                    get
                    {
                        return this._REBM16;
                    }
                    set
                    {
                        this._REBM16 = value;

                    }
                }

                public decimal REBM17
                {
                    get
                    {
                        return this._REBM17;
                    }
                    set
                    {
                        this._REBM17 = value;

                    }
                }

                public decimal REBM18
                {
                    get
                    {
                        return this._REBM18;
                    }
                    set
                    {
                        this._REBM18 = value;

                    }
                }

                public decimal REBM19
                {
                    get
                    {
                        return this._REBM19;
                    }
                    set
                    {
                        this._REBM19 = value;

                    }
                }

                public decimal REBM20
                {
                    get
                    {
                        return this._REBM20;
                    }
                    set
                    {
                        this._REBM20 = value;

                    }
                }

                //--------
                public decimal REBT1
                {
                    get
                    {
                        return this._REBT1;
                    }
                    set
                    {
                        this._REBT1 = value;

                    }
                }

                public decimal REBT2
                {
                    get
                    {
                        return this._REBT2;
                    }
                    set
                    {
                        this._REBT2 = value;

                    }
                }

                public decimal REBT3
                {
                    get
                    {
                        return this._REBT3;
                    }
                    set
                    {
                        this._REBT3 = value;

                    }
                }

                public decimal REBT4
                {
                    get
                    {
                        return this._REBT4;
                    }
                    set
                    {
                        this._REBT4 = value;

                    }
                }

                public decimal REBT5
                {
                    get
                    {
                        return this._REBT5;
                    }
                    set
                    {
                        this._REBT5 = value;

                    }
                }

                public decimal REBT6
                {
                    get
                    {
                        return this._REBT6;
                    }
                    set
                    {
                        this._REBT6 = value;

                    }
                }

                public decimal REBT7
                {
                    get
                    {
                        return this._REBT7;
                    }
                    set
                    {
                        this._REBT7 = value;

                    }
                }

                public decimal REBT8
                {
                    get
                    {
                        return this._REBT8;
                    }
                    set
                    {
                        this._REBT8 = value;

                    }
                }

                public decimal REBT9
                {
                    get
                    {
                        return this._REBT9;
                    }
                    set
                    {
                        this._REBT9 = value;

                    }
                }

                public decimal REBT10
                {
                    get
                    {
                        return this._REBT10;
                    }
                    set
                    {
                        this._REBT10 = value;

                    }
                }

                public decimal REBT11
                {
                    get
                    {
                        return this._REBT11;
                    }
                    set
                    {
                        this._REBT11 = value;

                    }
                }

                public decimal REBT12
                {
                    get
                    {
                        return this._REBT12;
                    }
                    set
                    {
                        this._REBT12 = value;

                    }
                }

                public decimal REBT13
                {
                    get
                    {
                        return this._REBT13;
                    }
                    set
                    {
                        this._REBT13 = value;

                    }
                }

                public decimal REBT14
                {
                    get
                    {
                        return this._REBT14;
                    }
                    set
                    {
                        this._REBT14 = value;

                    }
                }

                public decimal REBT15
                {
                    get
                    {
                        return this._REBT15;
                    }
                    set
                    {
                        this._REBT15 = value;

                    }
                }

                public decimal REBT16
                {
                    get
                    {
                        return this._REBT16;
                    }
                    set
                    {
                        this._REBT16 = value;

                    }
                }

                public decimal REBT17
                {
                    get
                    {
                        return this._REBT17;
                    }
                    set
                    {
                        this._REBT17 = value;

                    }
                }

                public decimal REBT18
                {
                    get
                    {
                        return this._REBT18;
                    }
                    set
                    {
                        this._REBT18 = value;

                    }
                }

                public decimal REBT19
                {
                    get
                    {
                        return this._REBT19;
                    }
                    set
                    {
                        this._REBT19 = value;

                    }
                }

                public decimal REBT20
                {
                    get
                    {
                        return this._REBT20;
                    }
                    set
                    {
                        this._REBT20 = value;

                    }
                }
                //---------

                public decimal REBD1
                {
                    get
                    {
                        return this._REBD1;
                    }
                    set
                    {
                        this._REBD1 = value;

                    }
                }

                public decimal REBD2
                {
                    get
                    {
                        return this._REBD2;
                    }
                    set
                    {
                        this._REBD2 = value;

                    }
                }

                public decimal REBD3
                {
                    get
                    {
                        return this._REBD3;
                    }
                    set
                    {
                        this._REBD3 = value;

                    }
                }

                public decimal REBD4
                {
                    get
                    {
                        return this._REBD4;
                    }
                    set
                    {
                        this._REBD4 = value;

                    }
                }

                public decimal REBD5
                {
                    get
                    {
                        return this._REBD5;
                    }
                    set
                    {
                        this._REBD5 = value;

                    }
                }

                public decimal REBD6
                {
                    get
                    {
                        return this._REBD6;
                    }
                    set
                    {
                        this._REBD6 = value;

                    }
                }

                public decimal REBD7
                {
                    get
                    {
                        return this._REBD7;
                    }
                    set
                    {
                        this._REBD7 = value;

                    }
                }

                public decimal REBD8
                {
                    get
                    {
                        return this._REBD8;
                    }
                    set
                    {
                        this._REBD8 = value;

                    }
                }

                public decimal REBD9
                {
                    get
                    {
                        return this._REBD9;
                    }
                    set
                    {
                        this._REBD9 = value;

                    }
                }

                public decimal REBD10
                {
                    get
                    {
                        return this._REBD10;
                    }
                    set
                    {
                        this._REBD10 = value;

                    }
                }

                public decimal REBD11
                {
                    get
                    {
                        return this._REBD11;
                    }
                    set
                    {
                        this._REBD11 = value;

                    }
                }

                public decimal REBD12
                {
                    get
                    {
                        return this._REBD12;
                    }
                    set
                    {
                        this._REBD12 = value;

                    }
                }

                public decimal REBD13
                {
                    get
                    {
                        return this._REBD13;
                    }
                    set
                    {
                        this._REBD13 = value;

                    }
                }

                public decimal REBD14
                {
                    get
                    {
                        return this._REBD14;
                    }
                    set
                    {
                        this._REBD14 = value;

                    }
                }

                public decimal REBD15
                {
                    get
                    {
                        return this._REBD15;
                    }
                    set
                    {
                        this._REBD15 = value;

                    }
                }

                public decimal REBD16
                {
                    get
                    {
                        return this._REBD16;
                    }
                    set
                    {
                        this._REBD16 = value;

                    }
                }

                public decimal REBD17
                {
                    get
                    {
                        return this._REBD17;
                    }
                    set
                    {
                        this._REBD17 = value;

                    }
                }

                public decimal REBD18
                {
                    get
                    {
                        return this._REBD18;
                    }
                    set
                    {
                        this._REBD18 = value;

                    }
                }

                public decimal REBD19
                {
                    get
                    {
                        return this._REBD19;
                    }
                    set
                    {
                        this._REBD19 = value;

                    }
                }

                public decimal REBD20
                {
                    get
                    {
                        return this._REBD20;
                    }
                    set
                    {
                        this._REBD20 = value;

                    }
                }
                //-------

                public decimal BALAMT
                {
                    get
                    {
                        return this._BALAMT;
                    }
                    set
                    {
                        this._BALAMT = value;

                    }
                }

                //-----
                public string INCP1
                {
                    get
                    {
                        return this._INCP1;
                    }
                    set
                    {
                        this._INCP1 = value;

                    }
                }

                public string INCP2
                {
                    get
                    {
                        return this._INCP2;
                    }
                    set
                    {
                        this._INCP2 = value;

                    }
                }

                public string INCP3
                {
                    get
                    {
                        return this._INCP3;
                    }
                    set
                    {
                        this._INCP3 = value;

                    }
                }

                public string INCP4
                {
                    get
                    {
                        return this._INCP4;
                    }
                    set
                    {
                        this._INCP4 = value;

                    }
                }

                public string INCP5
                {
                    get
                    {
                        return this._INCP5;
                    }
                    set
                    {
                        this._INCP5 = value;

                    }
                }
                //-----

                public decimal INCAMT1
                {
                    get
                    {
                        return this._INCAMT1;
                    }
                    set
                    {
                        this._INCAMT1 = value;

                    }
                }

                public decimal INCAMT2
                {
                    get
                    {
                        return this._INCAMT2;
                    }
                    set
                    {
                        this._INCAMT2 = value;

                    }
                }

                public decimal INCAMT3
                {
                    get
                    {
                        return this._INCAMT3;
                    }
                    set
                    {
                        this._INCAMT3 = value;

                    }
                }

                public decimal INCAMT4
                {
                    get
                    {
                        return this._INCAMT4;
                    }
                    set
                    {
                        this._INCAMT4 = value;

                    }
                }

                public decimal INCAMT5
                {
                    get
                    {
                        return this._INCAMT5;
                    }
                    set
                    {
                        this._INCAMT5 = value;

                    }
                }
                //-----

                public System.Nullable<System.DateTime> INCDATE
                {
                    get
                    {
                        return this._INCDATE;
                    }
                    set
                    {
                        if ((this._INCDATE != value))
                        {
                            this._INCDATE = value;
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
                        this._REMARK = value;

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
                        this._COLLEGE_CODE = value;

                    }
                }
                public decimal OTHERINCTDS
                {
                    get
                    {
                        return this._OTHERINCTDS;
                    }
                    set
                    {
                        this._OTHERINCTDS = value;

                    }
                }














                #endregion
            }
        }
    }
}
