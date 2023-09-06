using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace NITPRM
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class Vendor
            {
                #region Private Members

                private System.Nullable<int> _STR_PARTY_NO;

                private string _PARTY_CODE;

                private string _PARTY_NAME;

                private System.Nullable<int> _PARTY_CATEGORY_NO;

                private string _ADDRESS;

                private System.Nullable<int> _CITYNO;

                private System.Nullable<int> _STATENO;

                private string _PHONE;

                private string _EMAIL;

                private string _CST;

                private string _BST;

                private System.Nullable<int> _TNO;

                private System.Nullable<Decimal> _OB;

                private string _REMARK;

                private string _COLLEGE_CODE;

                #endregion

                #region Public Properties

                //[Column(Storage = "_STR_PARTY_NO", DbType = "Int")]
                public System.Nullable<int> STR_PARTY_NO
                {
                    get
                    {
                        return this._STR_PARTY_NO;
                    }
                    set
                    {
                        if ((this._STR_PARTY_NO != value))
                        {
                            this._STR_PARTY_NO = value;
                        }
                    }
                }

                //[Column(Storage = "_PARTY_CODE", DbType = "NVarChar(20)")]
                public string PARTY_CODE
                {
                    get
                    {
                        return this._PARTY_CODE;
                    }
                    set
                    {
                        if ((this._PARTY_CODE != value))
                        {
                            this._PARTY_CODE = value;
                        }
                    }
                }

                //[Column(Storage = "_PARTY_NAME", DbType = "NVarChar(100)")]
                public string PARTY_NAME
                {
                    get
                    {
                        return this._PARTY_NAME;
                    }
                    set
                    {
                        if ((this._PARTY_NAME != value))
                        {
                            this._PARTY_NAME = value;
                        }
                    }
                }

                //[Column(Storage = "_PARTY_CATEGORY_NO", DbType = "Int")]
                public System.Nullable<int> PARTY_CATEGORY_NO
                {
                    get
                    {
                        return this._PARTY_CATEGORY_NO;
                    }
                    set
                    {
                        if ((this._PARTY_CATEGORY_NO != value))
                        {
                            this._PARTY_CATEGORY_NO = value;
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

                //[Column(Storage = "_STATENO", DbType = "Int")]
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

                //[Column(Storage = "_TNO", DbType = "Int")]
                public System.Nullable<int> TNO
                {
                    get
                    {
                        return this._TNO;
                    }
                    set
                    {
                        if ((this._TNO != value))
                        {
                            this._TNO = value;
                        }
                    }
                }

                //[Column(Storage = "_OB", DbType = "int")]
                public System.Nullable<Decimal> OB
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
                #endregion

            }
        }
    }
}
