using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class Pay_Supplimentary_Bill_Guest
            {
                private System.Nullable<int> _SUPLGUESTID;
                private int _FINYEARID;
                private string _EMPLOYEENAME;
                private string _SUPLHEAD;
                private int _SUPLHNO;
                private int _SUBDESIGNO;
                private int _DEPARTMENTNO;
                private string _CODE;
                private string _ORDNO;
                private System.Nullable<System.DateTime> _SBDATE;
                private string _PANNO;
                private int _BANKNO;
                private string _EMAILID;
                private string _MOBILENO;
                private string _ACCNO;
                private string _IFSCCODE;
                private string _REMARK;
                private System.Nullable<decimal> _TOTAL_AMOUNT;
                private System.Nullable<decimal> _TDS_PER;
                private System.Nullable<decimal> _TDS_AMOUNT;
                private System.Nullable<decimal> _NET_AMOUNT;
                private System.Nullable<decimal> _TDS_NETAMOUNT;
                private string _COLLEGE_CODE;
                private int _SECTIONID;
                

                //[Column(Storage = "_SUPLGUESTID", DbType = "Int")]
                public System.Nullable<int> SUPLGUESTID
                {
                    get
                    {
                        return this._SUPLGUESTID;
                    }
                    set
                    {
                        if ((this._SUPLGUESTID != value))
                        {
                            this._SUPLGUESTID = value;
                        }
                    }
                }

                //[Column(Storage = "_FINYEARID", DbType = "Int")]
                public int FINYEARID
                {
                    get
                    {
                        return this._FINYEARID;
                    }
                    set
                    {
                        if ((this._FINYEARID != value))
                        {
                            this._FINYEARID = value;
                        }
                    }
                }
                //[Column(Storage = "_EMPLOYEENAME", DbType = "NVarChar(50)")]
                public string EMPLOYEENAME
                {
                    get
                    {
                        return this._EMPLOYEENAME;
                    }
                    set
                    {
                        if ((this._EMPLOYEENAME != value))
                        {
                            this._EMPLOYEENAME = value;
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


                
                //[Column(Storage = "_REMARK", DbType = "NVarChar(50)")]
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

                //[Column(Storage = "_SUPLHNO", DbType = "Int")]
                public int SUPLHNO
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

                //[Column(Storage = "_SUBDESIGNO", DbType = "Int")]
                public int SUBDESIGNO
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


                //[Column(Storage = "_DEPARTMENTNO", DbType = "Int")]
                public int DEPARTMENTNO
                {
                    get
                    {
                        return this._DEPARTMENTNO;
                    }
                    set
                    {
                        if ((this._DEPARTMENTNO != value))
                        {
                            this._DEPARTMENTNO = value;
                        }
                    }
                }
                //[Column(Storage = "_CODE", DbType = "NVarChar(50)")]
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

                //[Column(Storage = "SBDATE", DbType = "Int")]
                public System.Nullable<DateTime> SBDATE
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

                //[Column(Storage = "_PANNO", DbType = "NVarChar(50)")]
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

                //[Column(Storage = "_BANKNO", DbType = "Int")]
                public int BANKNO
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

                //[Column(Storage = "_EMAILID", DbType = "NVarChar(50)")]
                public string EMAILID
                {
                    get
                    {
                        return this._EMAILID;
                    }
                    set
                    {
                        if ((this._EMAILID != value))
                        {
                            this._EMAILID = value;
                        }
                    }
                }

                //[Column(Storage = "_MOBILENO", DbType = "NVarChar(50)")]
                public string MOBILENO
                {
                    get
                    {
                        return this._MOBILENO;
                    }
                    set
                    {
                        if ((this._MOBILENO != value))
                        {
                            this._MOBILENO = value;
                        }
                    }
                }

                //[Column(Storage = "_ACCNO", DbType = "NVarChar(50)")]
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

                //[Column(Storage = "_ACCNO", DbType = "NVarChar(50)")]
                public string IFSCCODE
                {
                    get
                    {
                        return this._IFSCCODE;
                    }
                    set
                    {
                        if ((this._IFSCCODE != value))
                        {
                            this._IFSCCODE = value;
                        }
                    }
                }

                //[Column(Storage = "_TOTAL_AMOUNT", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> TOTAL_AMOUNT
                {
                    get
                    {
                        return this._TOTAL_AMOUNT;
                    }
                    set
                    {
                        if ((this._TOTAL_AMOUNT != value))
                        {
                            this._TOTAL_AMOUNT = value;
                        }
                    }
                }

                //[Column(Storage = "_TDS_PER", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> TDS_PER
                {
                    get
                    {
                        return this._TDS_PER;
                    }
                    set
                    {
                        if ((this._TDS_PER != value))
                        {
                            this._TDS_PER = value;
                        }
                    }
                }

                //[Column(Storage = "_TDS_AMOUNT", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> TDS_AMOUNT
                {
                    get
                    {
                        return this._TDS_AMOUNT;
                    }
                    set
                    {
                        if ((this._TDS_AMOUNT != value))
                        {
                            this._TDS_AMOUNT = value;
                        }
                    }
                }

                //[Column(Storage = "_NET_AMOUNT", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> NET_AMOUNT
                {
                    get
                    {
                        return this._NET_AMOUNT;
                    }
                    set
                    {
                        if ((this._NET_AMOUNT != value))
                        {
                            this._NET_AMOUNT = value;
                        }
                    }
                }

                //[Column(Storage = "_COLLEGE_CODE", DbType = "NVarChar(50)")]
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


                //[Column(Storage = "_TDS_NETAMOUNT", DbType = "Decimal(12,2)")]
                public System.Nullable<decimal> TDS_NETAMOUNT
                {
                    get
                    {
                        return this._TDS_NETAMOUNT;
                    }
                    set
                    {
                        if ((this._TDS_NETAMOUNT != value))
                        {
                            this._TDS_NETAMOUNT = value;
                        }
                    }
                }

                //[Column(Storage = "_SECTIONID", DbType = "Int")]
                public int SECTIONID
                {
                    get
                    {
                        return this._SECTIONID;
                    }
                    set
                    {
                        if ((this._SECTIONID != value))
                        {
                            this._SECTIONID = value;
                        }
                    }
                }
               
            }
        }
    }
}
