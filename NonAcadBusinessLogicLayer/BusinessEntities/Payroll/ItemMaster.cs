using System;
using System.Data;
using System.Web;


namespace IITMS
{
    namespace NITPRM
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class ItemMaster
            {
                #region Private Members
                    private System.Nullable<int> _ITEM_NO;
                    private string _ITEM_CODE;
                    private string _ITEM_NAME;
                    private string _ITEM_DETAILS;
                    private System.Nullable<int> _MAIN_ITEM_GRP_NO;
                    private System.Nullable<int> _MAIN_SUB_ITEM_GRP_NO;
                    private string _UNIT;
                    private System.Nullable<int> _RECORDED_QTY;
                    private System.Nullable<int> _MIN_QTY;
                    private System.Nullable<int> _MAX_QTY;
                    private System.Nullable<int> _BUD_QTY;
                    private System.Nullable<int> _CUR_QTY;
                    private System.Nullable<int> _OB_QTY;
                    private System.Nullable<int> _OB_VALUE;
                    private string _COLLEGE_CODE;
                #endregion

                #region Public Properties
                    //[Column(Storage = "_ITEM_NO", DbType = "Int")]
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

                    //[Column(Storage = "_ITEM_CODE", DbType = "NVarChar(10)")]
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

                    //[Column(Storage = "_ITEM_NAME", DbType = "NVarChar(100)")]
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

                   // [Column(Storage = "_ITEM_DETAILS", DbType = "NVarChar(2000)")]
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

                    //[Column(Storage = "_MAIN_ITEM_GRP_NO", DbType = "Int")]
                    public System.Nullable<int> MAIN_ITEM_GRP_NO
                    {
                        get
                        {
                            return this._MAIN_ITEM_GRP_NO;
                        }
                        set
                        {
                            if ((this._MAIN_ITEM_GRP_NO != value))
                            {
                                this._MAIN_ITEM_GRP_NO = value;
                            }
                        }
                    }

                    //[Column(Storage = "_MAIN_SUB_ITEM_GRP_NO", DbType = "Int")]
                    public System.Nullable<int> MAIN_SUB_ITEM_GRP_NO
                    {
                        get
                        {
                            return this._MAIN_SUB_ITEM_GRP_NO;
                        }
                        set
                        {
                            if ((this._MAIN_SUB_ITEM_GRP_NO != value))
                            {
                                this._MAIN_SUB_ITEM_GRP_NO = value;
                            }
                        }
                    }

                    //[Column(Storage = "_UNIT", DbType = "NVarChar(100)")]
                    public string UNIT
                    {
                        get
                        {
                            return this._UNIT;
                        }
                        set
                        {
                            if ((this._UNIT != value))
                            {
                                this._UNIT = value;
                            }
                        }
                    }

                    //[Column(Storage = "_RECORDED_QTY", DbType = "Int")]
                    public System.Nullable<int> RECORDED_QTY
                    {
                        get
                        {
                            return this._RECORDED_QTY;
                        }
                        set
                        {
                            if ((this._RECORDED_QTY != value))
                            {
                                this._RECORDED_QTY = value;
                            }
                        }
                    }

                    //[Column(Storage = "_MIN_QTY", DbType = "Int")]
                    public System.Nullable<int> MIN_QTY
                    {
                        get
                        {
                            return this._MIN_QTY;
                        }
                        set
                        {
                            if ((this._MIN_QTY != value))
                            {
                                this._MIN_QTY = value;
                            }
                        }
                    }

                    //[Column(Storage = "_MAX_QTY", DbType = "Int")]
                    public System.Nullable<int> MAX_QTY
                    {
                        get
                        {
                            return this._MAX_QTY;
                        }
                        set
                        {
                            if ((this._MAX_QTY != value))
                            {
                                this._MAX_QTY = value;
                            }
                        }
                    }

                    //[Column(Storage = "_BUD_QTY", DbType = "Int")]
                    public System.Nullable<int> BUD_QTY
                    {
                        get
                        {
                            return this._BUD_QTY;
                        }
                        set
                        {
                            if ((this._BUD_QTY != value))
                            {
                                this._BUD_QTY = value;
                            }
                        }
                    }

                    //[Column(Storage = "_CUR_QTY", DbType = "Int")]
                    public System.Nullable<int> CUR_QTY
                    {
                        get
                        {
                            return this._CUR_QTY;
                        }
                        set
                        {
                            if ((this._CUR_QTY != value))
                            {
                                this._CUR_QTY = value;
                            }
                        }
                    }

                    //[Column(Storage = "_OB_QTY", DbType = "Int")]
                    public System.Nullable<int> OB_QTY
                    {
                        get
                        {
                            return this._OB_QTY;
                        }
                        set
                        {
                            if ((this._OB_QTY != value))
                            {
                                this._OB_QTY = value;
                            }
                        }
                    }

                    //[Column(Storage = "_OB_VALUE", DbType = "Int")]
                    public System.Nullable<int> OB_VALUE
                    {
                        get
                        {
                            return this._OB_VALUE;
                        }
                        set
                        {
                            if ((this._OB_VALUE != value))
                            {
                                this._OB_VALUE = value;
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

        }
    }
}