using System;
using System.Collections;

namespace IITMS.NITPRM.BusinessLayer.BusinessEntities
{
    public class Str_DeptRequest
    {
        
            // [Table(Name = "UAIMSSTORE.STR_REQ_MAIN")]
            #region STR_REQ_MAIN

            private System.Nullable<int> _REQTRNO;

            private string _REQ_NO;

            private System.Nullable<System.DateTime> _REQ_DATE;

            private string _REQ_SLIP_NO;

            private System.Nullable<System.DateTime> _REQ_SLIP_DATE;

            private System.Nullable<int> _DCODE;

            private string _NAME;

            private System.Nullable<int> _BUDNO;

            private System.Nullable<int> _MDCODE;

            private System.Nullable<System.DateTime> _DT;

            private System.Nullable<int> _DPV_NO;

            private System.Nullable<bool> _FINAL;

            private System.Nullable<bool> _TEQIP;

            private System.Nullable<char> _STATUS;

            private string _REMARK;

            private string _COLLEGE_CODE;

            #endregion 

            // [Table(Name = "UAIMSSTORE.STR_REQ_TRAN")]

            #region STR_REQ_TRAN
            private System.Nullable<int> _REQ_TNO;     

            private System.Nullable<int> _INO;

            private System.Nullable<int> _REQ_QTY;

            private System.Nullable<decimal> _RATE;

            private System.Nullable<int> _ISSUE_QTY;

            private System.Nullable<int> _INVTRNO;

            private System.Nullable<char> _FLAG;

            private System.Nullable<bool> _END;
          
            private string _TECHSPEC;

            private string _REMARKTRN;


            #endregion

            //[Column(Storage = "_REQTRNO", DbType = "Int")]
            public System.Nullable<int> REQTRNO
            {
            get
            {
                return this._REQTRNO;
            }
            set
            {
                if ((this._REQTRNO != value))
                {
                    this._REQTRNO = value;
                }
            }
            }

            //[Column(Storage = "_REQ_NO", DbType = "NVarChar(50)")]
            public string REQ_NO
            {
            get
            {
                return this._REQ_NO;
            }
            set
            {
                if ((this._REQ_NO != value))
                {
                    this._REQ_NO = value;
                }
            }
            }

            //[Column(Storage = "_REQ_DATE", DbType = "DateTime")]
            public System.Nullable<System.DateTime> REQ_DATE
            {
            get
            {
                return this._REQ_DATE;
            }
            set
            {
                if ((this._REQ_DATE != value))
                {
                    this._REQ_DATE = value;
                }
            }
            }

            //[Column(Storage = "_REQ_SLIP_NO", DbType = "NVarChar(50)")]
            public string REQ_SLIP_NO
            {
            get
            {
                return this._REQ_SLIP_NO;
            }
            set
            {
                if ((this._REQ_SLIP_NO != value))
                {
                    this._REQ_SLIP_NO = value;
                }
            }
            }

            //[Column(Storage = "_REQ_SLIP_DATE", DbType = "DateTime")]
            public System.Nullable<System.DateTime> REQ_SLIP_DATE
            {
            get
            {
                return this._REQ_SLIP_DATE;
            }
            set
            {
                if ((this._REQ_SLIP_DATE != value))
                {
                    this._REQ_SLIP_DATE = value;
                }
            }
            }

            //[Column(Storage = "_DCODE", DbType = "Int")]
            public System.Nullable<int> DCODE
            {
            get
            {
                return this._DCODE;
            }
            set
            {
                if ((this._DCODE != value))
                {
                    this._DCODE = value;
                }
            }
            }

            //[Column(Storage = "_NAME", DbType = "NVarChar(100)")]
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

            //[Column(Storage = "_BUDNO", DbType = "Int")]
            public System.Nullable<int> BUDNO
            {
            get
            {
                return this._BUDNO;
            }
            set
            {
                if ((this._BUDNO != value))
                {
                    this._BUDNO = value;
                }
            }
            }

            //[Column(Storage = "_MDCODE", DbType = "Int")]
            public System.Nullable<int> MDCODE
            {
            get
            {
                return this._MDCODE;
            }
            set
            {
                if ((this._MDCODE != value))
                {
                    this._MDCODE = value;
                }
            }
            }

            //[Column(Storage = "_DT", DbType = "DateTime")]
            public System.Nullable<System.DateTime> DT
            {
            get
            {
                return this._DT;
            }
            set
            {
                if ((this._DT != value))
                {
                    this._DT = value;
                }
            }
            }

            //[Column(Storage = "_DPV_NO", DbType = "Int")]
            public System.Nullable<int> DPV_NO
            {
            get
            {
                return this._DPV_NO;
            }
            set
            {
                if ((this._DPV_NO != value))
                {
                    this._DPV_NO = value;
                }
            }
            }

            //[Column(Storage = "_FINAL", DbType = "Bit")]
            public System.Nullable<bool> FINAL
            {
            get
            {
                return this._FINAL;
            }
            set
            {
                if ((this._FINAL != value))
                {
                    this._FINAL = value;
                }
            }
            }

            //[Column(Storage = "_TEQIP", DbType = "Bit")]
            public System.Nullable<bool> TEQIP
            {
            get
            {
                return this._TEQIP;
            }
            set
            {
                if ((this._TEQIP != value))
                {
                    this._TEQIP = value;
                }
            }
            }

            //[Column(Storage = "_STATUS", DbType = "NChar(1)")]
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


            //[Column(Storage = "_REQ_TNO", DbType = "Int")]
            public System.Nullable<int> REQ_TNO
            {
                get
                {
                    return this._REQ_TNO;
                }
                set
                {
                    if ((this._REQ_TNO != value))
                    {
                        this._REQ_TNO = value;
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

            //[Column(Storage = "_REQ_QTY", DbType = "Int")]
            public System.Nullable<int> REQ_QTY
            {
                get
                {
                    return this._REQ_QTY;
                }
                set
                {
                    if ((this._REQ_QTY != value))
                    {
                        this._REQ_QTY = value;
                    }
                }
            }

            // [Column(Storage = "_RATE", DbType = "Decimal(10,4)")]
            public System.Nullable<decimal> RATE
            {
                get
                {
                    return this._RATE;
                }
                set
                {
                    if ((this._RATE != value))
                    {
                        this._RATE = value;
                    }
                }
            }

            //[Column(Storage = "_ISSUE_QTY", DbType = "Int")]
            public System.Nullable<int> ISSUE_QTY
            {
                get
                {
                    return this._ISSUE_QTY;
                }
                set
                {
                    if ((this._ISSUE_QTY != value))
                    {
                        this._ISSUE_QTY = value;
                    }
                }
            }

            //[Column(Storage = "_INVTRNO", DbType = "Int")]
            public System.Nullable<int> INVTRNO
            {
                get
                {
                    return this._INVTRNO;
                }
                set
                {
                    if ((this._INVTRNO != value))
                    {
                        this._INVTRNO = value;
                    }
                }
            }

            //[Column(Storage = "_FLAG", DbType = "NVarChar(1)")]
            public System.Nullable<char> FLAG
            {
                get
                {
                    return this._FLAG;
                }
                set
                {
                    if ((this._FLAG != value))
                    {
                        this._FLAG = value;
                    }
                }
            }

            //[Column(Name = "[END]", Storage = "_END", DbType = "Bit")]
            public System.Nullable<bool> END
            {
                get
                {
                    return this._END;
                }
                set
                {
                    if ((this._END != value))
                    {
                        this._END = value;
                    }
                }
            }

            //[Column(Storage = "_TECHSPEC", DbType = "NVarChar(MAX)")]
            public string TECHSPEC
            {
                get
                {
                    return this._TECHSPEC;
                }
                set
                {
                    if ((this._TECHSPEC != value))
                    {
                        this._TECHSPEC = value;
                    }
                }
            }

            //[Column(Storage = "_REMARK", DbType = "NVarChar(200)")]
            public string REMARKTRN
            {
                get
                {
                    return this._REMARKTRN;
                }
                set
                {
                    if ((this._REMARKTRN != value))
                    {
                        this._REMARKTRN = value;
                    }
                }
            }
               
        }

    }



