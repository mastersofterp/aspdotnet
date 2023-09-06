//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STORES
// PAGE NAME     : StoreMaster.cs                                                    
// CREATION DATE : 05-May-2009                                                        
// CREATED BY    : G.V.S. KIRAN                                                         
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================

using System;
using System.Data;
using System.Collections.Generic;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class StoreMaster
    {
        #region STORE_BudgetHead


        private int _BHALNO;
        private int _BHNO;
        private decimal _BAMT;

        private System.DateTime _BUDFSDATE;

        private System.DateTime _BUDFEDATE;

        private string _SCHEME;

        private string _BNATURE;

        private string _BCOORDINATOR;

        private string _COLLEGE_CODE;
        //[Column(Storage = "_BHALNO", DbType = "INT NOT NULL" )]
        public int BHALNO
        {
            get
            {
                return this._BHALNO;
            }
            set
            {
                if ((this._BHALNO != value))
                {
                    this._BHALNO = value;

                }
            }
        }
        //[Column(Storage = "_BHNO", DbType = "Int NOT NULL",CanBeNull = false)]
        public int BHNO
        {
            get
            {
                return this._BHNO;
            }
            set
            {
                if ((this._BHNO != value))
                {
                    this._BHNO = value;

                }
            }
        }



        //[Column(Storage = "_BAMT", DbType = "Decimal(10,2) NOT NULL")]
        public decimal BAMT
        {
            get
            {
                return this._BAMT;
            }
            set
            {
                if ((this._BAMT != value))
                {
                    this._BAMT = value;

                }
            }
        }

        //[Column(Storage = "_BUDFSDATE", DbType = "DateTime NOT NULL")]
        public System.DateTime BUDFSDATE
        {
            get
            {
                return this._BUDFSDATE;
            }
            set
            {
                if ((this._BUDFSDATE != value))
                {

                    this._BUDFSDATE = value;

                }
            }
        }

        //[Column(Storage = "_BUDFEDATE", DbType = "DateTime NOT NULL")]
        public System.DateTime BUDFEDATE
        {
            get
            {
                return this._BUDFEDATE;
            }
            set
            {
                if ((this._BUDFEDATE != value))
                {

                    this._BUDFEDATE = value;

                }
            }
        }



        //[Column(Storage = "_BNATURE", DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]
        public string SCHEME
        {
            get
            {
                return this._SCHEME;
            }
            set
            {
                if ((this._SCHEME != value))
                {

                    this._SCHEME = value;

                }
            }
        }
        //[Column(Storage = "_BNATURE", DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]
        public string BNATURE
        {
            get
            {
                return this._BNATURE;
            }
            set
            {
                if ((this._BNATURE != value))
                {

                    this._BNATURE = value;

                }
            }
        }

        //[Column(Storage = "_BCOORDINATOR", DbType = "NVarChar(50) NOT NULL", CanBeNull = false)]
        public string BCOORDINATOR
        {
            get
            {
                return this._BCOORDINATOR;
            }
            set
            {
                if ((this._BCOORDINATOR != value))
                {

                    this._BCOORDINATOR = value;

                }
            }
        }

        //[Column(Storage = "_COLLEGE_CODE", DbType = "NVarChar(15) NOT NULL", CanBeNull = false)]
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

        #region STORE_SUBDEPT_BUDGET
        private int _SD_BUDNO;

        private int _SDNO;

        private decimal _SD_BUDAMT;

        private System.DateTime _SD_BUDSDATE;

        private System.DateTime _SD_BUDEDATE;

        private decimal _SD_BUDBALAMT;


        //[Column(Storage = "_SD_BUD_NO", DbType = "Int NOT NULL", IsPrimaryKey = true)]
        public int SD_BUDNO
        {
            get
            {
                return this._SD_BUDNO;
            }
            set
            {
                if ((this._SD_BUDNO != value))
                {

                    this._SD_BUDNO = value;

                }
            }
        }



        //[Column(Storage = "_SDNO", DbType = "Int NOT NULL")]
        public int SDNO
        {
            get
            {
                return this._SDNO;
            }
            set
            {
                if ((this._SDNO != value))
                {
                    this._SDNO = value;
                }
            }
        }

        //[Column(Storage = "_SD_BUD_AMT", DbType = "Decimal(10,2) NOT NULL")]
        public decimal SD_BUDAMT
        {
            get
            {
                return this._SD_BUDAMT;
            }
            set
            {
                if ((this._SD_BUDAMT != value))
                {

                    this._SD_BUDAMT = value;

                }
            }
        }

        //[Column(Storage = "_SD_BUD_SDATE", DbType = "DateTime NOT NULL")]
        public System.DateTime SD_BUDSDATE
        {
            get
            {
                return this._SD_BUDSDATE;
            }
            set
            {
                if ((this._SD_BUDSDATE != value))
                {
                    this._SD_BUDSDATE = value;

                }
            }
        }

        //[Column(Storage = "_SD_BUD_EDATE", DbType = "DateTime NOT NULL")]
        public System.DateTime SD_BUDEDATE
        {
            get
            {
                return this._SD_BUDEDATE;
            }
            set
            {
                if ((this._SD_BUDEDATE != value))
                {

                    this._SD_BUDEDATE = value;

                }
            }
        }

        //[Column(Storage = "_SD_BUD_BALAMT", DbType = "Decimal(10,2) NOT NULL")]
        public decimal SD_BUDBALAMT
        {
            get
            {
                return this._SD_BUDBALAMT;
            }
            set
            {
                if ((this._SD_BUDBALAMT != value))
                {

                    this._SD_BUDBALAMT = value;

                }
            }
        }



        #endregion

        #region STORE_FIELDMASTER

        private System.Nullable<int> _FNO;

        private string _FNAME;

        private System.Nullable<char> _FTYPE;

        private System.Nullable<int> _FSRNO;

        private System.Nullable<char> _IND_FOR;

        private System.Nullable<bool> _ADDED_IN_BASIC;
        private System.Nullable<bool> _DEDUCT_IN_BASIC;

        //[Column(Storage="_FIELD_NO", DbType="Int")]
        public System.Nullable<int> FNO
        {
            get
            {
                return this._FNO;
            }
            set
            {
                if ((this._FNO != value))
                {
                    this._FNO = value;
                }
            }
        }

        //[Column(Storage="_FIELD_NAME", DbType="NVarChar(50)")]
        public string FNAME
        {
            get
            {
                return this._FNAME;
            }
            set
            {
                if ((this._FNAME != value))
                {
                    this._FNAME = value;
                }
            }
        }

        //[Column(Storage="_FIELD_TYPE", DbType="NVarChar(1)")]
        public System.Nullable<char> FTYPE
        {
            get
            {
                return this._FTYPE;
            }
            set
            {
                if ((this._FTYPE != value))
                {
                    this._FTYPE = value;
                }
            }
        }

        //[Column(Storage="_SERIAL_NO", DbType="Int")]
        public System.Nullable<int> FSRNO
        {
            get
            {
                return this._FSRNO;
            }
            set
            {
                if ((this._FSRNO != value))
                {
                    this._FSRNO = value;
                }
            }
        }

        //[Column(Storage="_IND_FOR", DbType="NVarChar(1)")]
        public System.Nullable<char> IND_FOR
        {
            get
            {
                return this._IND_FOR;
            }
            set
            {
                if ((this._IND_FOR != value))
                {
                    this._IND_FOR = value;
                }
            }
        }

        //[Column(Storage="_CALC_ON_BASICAMT_YN", DbType="Bit")]
        public System.Nullable<bool> ADDED_IN_BASIC
        {
            get
            {
                return this._ADDED_IN_BASIC;
            }
            set
            {
                if ((this._ADDED_IN_BASIC != value))
                {
                    this._ADDED_IN_BASIC = value;
                }
            }
        }
        public System.Nullable<bool> DEDUCT_IN_BASIC
        {
            get
            {
                return this._DEDUCT_IN_BASIC;
            }
            set
            {
                if ((this._DEDUCT_IN_BASIC != value))
                {
                    this._DEDUCT_IN_BASIC = value;
                }
            }
        }


        #endregion

        #region STORE_PARTY

        private System.Nullable<int> _PNO;

        private string _PCODE;

        private string _PNAME;

        private System.Nullable<int> _PCNO;

        private string _ADDRESS;

        private System.Nullable<int> _CITYNO;

        private System.Nullable<int> _STATENO;

        private string _PHONE;

        private string _EMAIL;

        private string _CST;

        private string _BST;

        private DataTable _PARTY_BANK_DETAIL_TBL;

        public DataTable PARTY_BANK_DETAIL_TBL
        {
            get
            {
                return this._PARTY_BANK_DETAIL_TBL;
            }
            set
            {
                if ((this._PARTY_BANK_DETAIL_TBL != value))
                {
                    this._PARTY_BANK_DETAIL_TBL = value;
                }
            }
        }


        private string _REMARK;


        //[Column(Storage = "_PNO", DbType = "Int")]
        public System.Nullable<int> PNO
        {
            get
            {
                return this._PNO;
            }
            set
            {
                if ((this._PNO != value))
                {
                    this._PNO = value;
                }
            }
        }

        //[Column(Storage = "_PCODE", DbType = "NVarChar(20)")]
        public string PCODE
        {
            get
            {
                return this._PCODE;
            }
            set
            {
                if ((this._PCODE != value))
                {
                    this._PCODE = value;
                }
            }
        }

        //[Column(Storage = "_PNAME", DbType = "NVarChar(100)")]
        public string PNAME
        {
            get
            {
                return this._PNAME;
            }
            set
            {
                if ((this._PNAME != value))
                {
                    this._PNAME = value;
                }
            }
        }

        //[Column(Storage = "_PCNO", DbType = "Int")]
        public System.Nullable<int> PCNO
        {
            get
            {
                return this._PCNO;
            }
            set
            {
                if ((this._PCNO != value))
                {
                    this._PCNO = value;
                }
            }
        }

        //[Column(Storage = "_ADDRESS", DbType = "NVarChar(200)")]
        public string ADDRESS
        {
            get
            {
                return this._ADDRESS;
            }
            set
            {
                if ((this._ADDRESS != value))
                {
                    this._ADDRESS = value;
                }
            }
        }

        //[Column(Storage = "_CITYNO", DbType = "Int")]
        public System.Nullable<int> CITYNO
        {
            get
            {
                return this._CITYNO;
            }
            set
            {
                if ((this._CITYNO != value))
                {
                    this._CITYNO = value;
                }
            }
        }

        // [Column(Storage = "_STATENO", DbType = "Int")]
        public System.Nullable<int> STATENO
        {
            get
            {
                return this._STATENO;
            }
            set
            {
                if ((this._STATENO != value))
                {
                    this._STATENO = value;
                }
            }
        }

        //[Column(Storage = "_PHONE", DbType = "NVarChar(10)")]
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

        //[Column(Storage = "_EMAIL", DbType = "NVarChar(100)")]
        public string EMAIL
        {
            get
            {
                return this._EMAIL;
            }
            set
            {
                if ((this._EMAIL != value))
                {
                    this._EMAIL = value;
                }
            }
        }

        //[Column(Storage = "_CST", DbType = "NVarChar(10)")]
        public string CST
        {
            get
            {
                return this._CST;
            }
            set
            {
                if ((this._CST != value))
                {
                    this._CST = value;
                }
            }
        }

        //[Column(Storage = "_BST", DbType = "NVarChar(10)")]
        public string BST
        {
            get
            {
                return this._BST;
            }
            set
            {
                if ((this._BST != value))
                {
                    this._BST = value;
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


        #endregion

        #region STORE_PARTY_CATEGORY

        private string _PCNAME;

        //[Column(Storage = "_PCNAME", DbType = "NVarChar(100) NOT NULL", CanBeNull = false)]
        public string PCNAME
        {
            get
            {
                return this._PCNAME;
            }
            set
            {
                if ((this._PCNAME != value))
                {

                    this._PCNAME = value;

                }
            }
        }


        #endregion

        #region STORE_ITEM_MASTER
        private System.Nullable<int> _ITEM_NO;
        private string _ITEM_CODE;
        private string _ITEM_NAME;
        private string _ITEM_DETAILS;
        private System.Nullable<int> _MIGNO;
        private System.Nullable<int> _MISGNO;
        private string _ITEM_UNIT;
        private System.Nullable<int> _ITEM_REORDER_QTY;
        private System.Nullable<int> _ITEM_MIN_QTY;
        private System.Nullable<int> _ITEM_MAX_QTY;
        private System.Nullable<int> _ITEM_BUD_QTY;
        private System.Nullable<int> _ITEM_CUR_QTY;
        private System.Nullable<int> _ITEM_OB_QTY;
        private System.Nullable<int> _ITEM_OB_VALUE;
        private string _ITEM_APPROVAL;
        private System.Nullable<int> _TAXID;

        public System.Nullable<int> TAXID
        {
            get
            {
                return this._TAXID;
            }
            set
            {
                if ((this._TAXID != value))
                {
                    this._TAXID = value;
                }
            }
        }

        public System.Nullable<int> ITEM_NO
        {
            get
            {
                return this._ITEM_NO;
            }
            set
            {
                if ((this._ITEM_NO != value))
                {
                    this._ITEM_NO = value;
                }
            }
        }


        public string ITEM_CODE
        {
            get
            {
                return this._ITEM_CODE;
            }
            set
            {
                if ((this._ITEM_CODE != value))
                {
                    this._ITEM_CODE = value;
                }
            }
        }


        public string ITEM_NAME
        {
            get
            {
                return this._ITEM_NAME;
            }
            set
            {
                if ((this._ITEM_NAME != value))
                {
                    this._ITEM_NAME = value;
                }
            }
        }


        public string ITEM_DETAILS
        {
            get
            {
                return this._ITEM_DETAILS;
            }
            set
            {
                if ((this._ITEM_DETAILS != value))
                {
                    this._ITEM_DETAILS = value;
                }
            }
        }


        public System.Nullable<int> MIGNO
        {
            get
            {
                return this._MIGNO;
            }
            set
            {
                if ((this._MIGNO != value))
                {
                    this._MIGNO = value;
                }
            }
        }


        public System.Nullable<int> MISGNO
        {
            get
            {
                return this._MISGNO;
            }
            set
            {
                if ((this._MISGNO != value))
                {
                    this._MISGNO = value;
                }
            }
        }


        public string ITEM_UNIT
        {
            get
            {
                return this._ITEM_UNIT;
            }
            set
            {
                if ((this._ITEM_UNIT != value))
                {
                    this._ITEM_UNIT = value;
                }
            }
        }


        public System.Nullable<int> ITEM_REORDER_QTY
        {
            get
            {
                return this._ITEM_REORDER_QTY;
            }
            set
            {
                if ((this._ITEM_REORDER_QTY != value))
                {
                    this._ITEM_REORDER_QTY = value;
                }
            }
        }


        public System.Nullable<int> ITEM_MIN_QTY
        {
            get
            {
                return this._ITEM_MIN_QTY;
            }
            set
            {
                if ((this._ITEM_MIN_QTY != value))
                {
                    this._ITEM_MIN_QTY = value;
                }
            }
        }


        public System.Nullable<int> ITEM_MAX_QTY
        {
            get
            {
                return this._ITEM_MAX_QTY;
            }
            set
            {
                if ((this._ITEM_MAX_QTY != value))
                {
                    this._ITEM_MAX_QTY = value;
                }
            }
        }


        public System.Nullable<int> ITEM_BUD_QTY
        {
            get
            {
                return this._ITEM_BUD_QTY;
            }
            set
            {
                if ((this._ITEM_BUD_QTY != value))
                {
                    this._ITEM_BUD_QTY = value;
                }
            }
        }


        public System.Nullable<int> ITEM_CUR_QTY
        {
            get
            {
                return this._ITEM_CUR_QTY;
            }
            set
            {
                if ((this._ITEM_CUR_QTY != value))
                {
                    this._ITEM_CUR_QTY = value;
                }
            }
        }


        public System.Nullable<int> ITEM_OB_QTY
        {
            get
            {
                return this._ITEM_OB_QTY;
            }
            set
            {
                if ((this._ITEM_OB_QTY != value))
                {
                    this._ITEM_OB_QTY = value;
                }
            }
        }

        public System.Nullable<int> ITEM_OB_VALUE
        {
            get
            {
                return this._ITEM_OB_VALUE;
            }
            set
            {
                if ((this._ITEM_OB_VALUE != value))
                {
                    this._ITEM_OB_VALUE = value;
                }
            }
        }
        public string ITEM_APPROVAL
        {
            get
            {
                return this._ITEM_APPROVAL;
            }
            set
            {
                if ((this._ITEM_APPROVAL != value))
                {
                    this._ITEM_APPROVAL = value;
                }
            }
        }


        #endregion

        #region STORE_TAX_MASTER
        private double _TaxNo;
        private string _TaxName;
        private int _TaxPer;

        public double TAXNO
        {
            get
            {
                return this._TaxNo;
            }
            set
            {
                if (this._TaxNo != value)
                {
                    _TaxNo = value;
                }
            }
        }
        public string TAXNAME
        {
            get
            {
                return this._TaxName;
            }
            set
            {
                if (this._TaxName != value)
                {
                    this._TaxName = value;
                }
            }
        }
        public int TAXPER
        {
            get
            {
                return this._TaxPer;
            }
            set
            {
                if (this._TaxPer != value)
                {
                    this._TaxPer = value;
                }

            }

        }
        #endregion

        #region STORE_GRAND_MASTER
        private int _GRANDNO;
        private string _GRAND_CODE;
        private string _GRAND_NAME;
        private string _GRAND_DETAILS;
        public int GRANDNO
        {
            get
            {
                return _GRANDNO;
            }
            set
            {
                if (_GRANDNO != value)
                {
                    _GRANDNO = value;
                }
            }
        }
        public string GRAND_CODE
        {
            get
            {
                return _GRAND_CODE;
            }
            set
            {
                if (_GRAND_CODE != value)
                {
                    _GRAND_CODE = value;
                }
            }
        }
        public string GRAND_NAME
        {
            get
            {
                return _GRAND_NAME;
            }
            set
            {
                if (_GRAND_NAME != value)
                {
                    _GRAND_NAME = value;
                }
            }
        }
        public string GRAND_DETAILS
        {
            get
            {
                return _GRAND_DETAILS;
            }
            set
            {
                if (_GRAND_DETAILS != value)
                {
                    _GRAND_DETAILS = value;
                }
            }
        }
        #endregion

        #region STORE_DSR_MASTER
        private int _DSTKNO;
        private int _MDNO;
        private int _DSR_GRANDNO;

        private string _DSTK_NAME;
        private string _DSTK_SHORT_NAME;
        private string _DSTK_YEAR;
        private string _DRNO;

        public int DSTKNO
        {
            get { return _DSTKNO; }
            set
            {
                if (_DSTKNO != value)
                {
                    _DSTKNO = value;
                }
            }
        }
        public int DSR_MDNO
        {
            get { return _MDNO; }
            set
            {
                if (_MDNO != value)
                {
                    _MDNO = value;
                }
            }
        }
        public int DSR_GRANDNO
        {
            get { return _DSR_GRANDNO; }
            set
            {
                if (_DSR_GRANDNO != value)
                {
                    _DSR_GRANDNO = value;
                }
            }
        }
        public string DSTK_NAME
        {
            get
            {
                return _DSTK_NAME;
            }
            set
            {
                if (_DSTK_NAME != value)
                {
                    _DSTK_NAME = value;
                }
            }
        }
        public string DSTK_SHORT_NAME
        {
            get
            {
                return _DSTK_SHORT_NAME;
            }
            set
            {
                if (_DSTK_SHORT_NAME != value)
                {
                    _DSTK_SHORT_NAME = value;
                }
            }
        }
        public string DRNO
        {
            get
            {
                return _DRNO;
            }
            set
            {
                if (_DRNO != value)
                {
                    _DRNO = value;
                }
            }
        }


        #endregion

        #region PASSING AUTHORITY

        private int _PANO;
        private string _PANAME;
        private int _UANO;
        private int _PAPNO;
        private double _AMOUNT_FROM;
        private double _AMOUNT_TO;
        private int _IS_SPECIAL;



        public int PANO
        {
            get
            {
                return this._PANO;
            }
            set
            {
                if ((this._PANO != value))
                {
                    this._PANO = value;
                }
            }
        }

        public string PANAME
        {
            get
            {
                return this._PANAME;
            }
            set
            {
                if ((this._PANAME != value))
                {
                    this._PANAME = value;
                }
            }

        }

        public int UANO
        {
            get
            {
                return this._UANO;
            }
            set
            {
                if ((this._UANO != value))
                {
                    this._UANO = value;
                }
            }
        }

        public int PAPNO
        {
            get
            {
                return this._PAPNO;
            }
            set
            {
                if ((this._PAPNO != value))
                {
                    this._PAPNO = value;
                }
            }
        }

        public double AMOUNT_FROM
        {
            get
            {
                return this._AMOUNT_FROM;
            }
            set
            {
                if ((this._AMOUNT_FROM != value))
                {
                    this._AMOUNT_FROM = value;
                }
            }
        }
        public double AMOUNT_TO
        {
            get
            {
                return this._AMOUNT_TO;
            }
            set
            {
                if ((this._AMOUNT_TO != value))
                {
                    this._AMOUNT_TO = value;
                }
            }
        }

        public int IS_SPECIAL
        {
            get
            {
                return this._IS_SPECIAL;
            }
            set
            {
                if ((this._IS_SPECIAL != value))
                {
                    this._IS_SPECIAL = value;
                }
            }
        }

        #endregion

        #region PASSING AUTHORITY PATH
        private int _LNO;
        private int _DEPTNO;
        //private int _PAPNO;
        private int _PAN01;
        private int _PAN02;
        private int _PAN03;
        private int _PAN04;
        private int _PAN05;
        private string _PAPATH;
        private char _PATH_FOR;
        private int _EMPNO;


        public int LNO
        {
            get
            {
                return this._LNO;
            }
            set
            {
                if ((this._LNO != value))
                {
                    this._LNO = value;
                }
            }
        }

        public int DEPTNO
        {
            get
            {
                return this._DEPTNO;
            }
            set
            {
                if ((this._DEPTNO != value))
                {
                    this._DEPTNO = value;
                }
            }
        }


        public int PAN01
        {
            get
            {
                return this._PAN01;
            }
            set
            {
                if ((this._PAN01 != value))
                {
                    this._PAN01 = value;
                }
            }
        }
        public int PAN02
        {
            get
            {
                return this._PAN02;
            }
            set
            {
                if ((this._PAN02 != value))
                {
                    this._PAN02 = value;
                }
            }
        }
        public int PAN03
        {
            get
            {
                return this._PAN03;
            }
            set
            {
                if ((this._PAN03 != value))
                {
                    this._PAN03 = value;
                }
            }
        }

        public int PAN04
        {
            get
            {
                return this._PAN04;
            }
            set
            {
                if ((this._PAN04 != value))
                {
                    this._PAN04 = value;
                }
            }
        }

        public int PAN05
        {
            get
            {
                return this._PAN05;
            }
            set
            {
                if ((this._PAN05 != value))
                {
                    this._PAN05 = value;
                }
            }
        }
        public string PAPATH
        {
            get
            {
                return this._PAPATH;
            }
            set
            {
                if ((this._PAPATH != value))
                {
                    this._PAPATH = value;
                }

            }
        }
        public char PATH_FOR
        {
            get
            {
                return this._PATH_FOR;
            }
            set
            {
                if ((this._PATH_FOR != value))
                {
                    this._PATH_FOR = value;
                }
            }
        }

        public int EMPNO
        {
            get
            {
                return this._EMPNO;
            }
            set
            {
                if ((this._EMPNO != value))
                {
                    this._EMPNO = value;
                }
            }

        }

        #endregion

        private DataTable _TaxFieldsTbl_TRAN = null;
        private int _CREATED_BY;
        private int _MODIFIED_BY;

        public DataTable TaxFieldsTbl_TRAN
        {
            get { return _TaxFieldsTbl_TRAN; }
            set { _TaxFieldsTbl_TRAN = value; }
        }

        public int CREATED_BY
        {
            get { return _CREATED_BY; }
            set { _CREATED_BY = value; }
        }
        public int MODIFIED_BY
        {
            get { return _MODIFIED_BY; }
            set { _MODIFIED_BY = value; }
        }



        #region TAX MASTER
        private int _Tax_Id;
        private string _Tax_Name;
        private string _Tax_Code;
        private int _Is_Per;
        private decimal _Tax_Per;
        private int _Is_State_Tax;
        private System.Nullable<bool> _Cal_Basic_Ammount;
        private int _Tax_SerialNo;
        private string _College_Code;

        public int Tax_Id
        {
            get
            {
                return this._Tax_Id;
            }
            set
            {
                if (this._Tax_Id != value)
                {
                    this._Tax_Id = value;
                }

            }
        }
        public string Tax_Name
        {
            get
            {
                return this._Tax_Name;
            }
            set
            {
                if (this._Tax_Name != value)
                {
                    this._Tax_Name = value;
                }

            }
        }
        public string Tax_Code
        {
            get
            {
                return this._Tax_Code;
            }
            set
            {
                if (this._Tax_Code != value)
                {
                    this._Tax_Code = value;
                }

            }
        }
        public int Is_Per
        {
            get
            {
                return this._Is_Per;
            }
            set
            {
                if (this._Is_Per != value)
                {
                    this._Is_Per = value;
                }

            }
        }
        public decimal Tax_Per
        {
            get
            {
                return this._Tax_Per;
            }
            set
            {
                if (this._Tax_Per != value)
                {
                    this._Tax_Per = value;
                }

            }
        }
        public int Is_State_Tax
        {
            get
            {
                return this._Is_State_Tax;
            }
            set
            {
                if (this._Is_State_Tax != value)
                {
                    this._Is_State_Tax = value;
                }

            }
        }
        public System.Nullable<bool> Cal_Basic_Ammount
        {
            get
            {
                return this._Cal_Basic_Ammount;
            }
            set
            {
                if (this._Cal_Basic_Ammount != value)
                {
                    this._Cal_Basic_Ammount = value;
                }

            }
        }
        public int Tax_SerialNo
        {
            get
            {
                return this._Tax_SerialNo;
            }
            set
            {
                if (this._Tax_SerialNo != value)
                {
                    this._Tax_SerialNo = value;
                }

            }
        }
        public string College_Code
        {
            get
            {
                return this._College_Code;
            }
            set
            {
                if (this._College_Code != value)
                {
                    this._College_Code = value;
                }

            }
        }

        #endregion
    }

}
