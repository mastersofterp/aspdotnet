using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class UserAcc
            {
                #region Private Members
                private int _ua_no = 0;
                private string _ua_name = string.Empty;
                private string _ua_pwd = string.Empty;
                private string _ua_oldpwd = string.Empty;
                private string _ua_acc = string.Empty;
                private int _ua_idno = 0;
                private int _ua_type = 0;
                private int _emp_Idno = 0;
                private string _ua_fullname = string.Empty;
                private string _ua_fathername = string.Empty;
                private string _ua_fathermobile = string.Empty;
                private string _ua_desig = string.Empty;
                private string _ua_email = string.Empty;
                private string _ua_deptno = string.Empty;
                private string _ua_qtrno = string.Empty;
                private int _ua_status = 0;
                private string _ua_empst = string.Empty;
                private int _ua_empdeptno = 0;
                private int _ua_dec = 0;
                private bool _ua_firstlog = false;
                private string _ua_section = string.Empty;
                private string _College_No;

                private string _ua_accno = string.Empty;
                private string _ua_ifsc = string.Empty;

                //private string _ua_remark = string.Empty;
                private string _ua_remark = string.Empty;
                private int _ua_userno = 0;
                private int _ua_sourcepageno = 0;



                #endregion


      

                public string UA_section
                {
                    get { return _ua_section; }
                    set { _ua_section = value; }
                }
                private System.Nullable<int> _TRXNO;

                private System.Nullable<int> _UA_IDNO;

                private System.Nullable<System.DateTime> _SIGNED_DATE;

                private System.Nullable<bool> _SIGNED;

                private string _IP_ADDRESS;

                private string _MAC_ADDRESS;

                private System.Nullable<System.DateTime> _AUDIT_DATE;

                private string _USER_ID;

                private string _COLLEGE_CODE;
                private int _parent_uatype = 0;
                private string _MOBILE;
                private string _EMAIL;
               
                
               // [Column(Storage = "_TRXNO", DbType = "Int")]
                public System.Nullable<int> TRXNO
                {
                    get
                    {
                        return this._TRXNO;
                    }
                    set
                    {
                        if ((this._TRXNO != value))
                        {
                            this._TRXNO = value;
                        }
                    }
                }

               // [Column(Storage = "_UA_IDNO", DbType = "Int")]
                public System.Nullable<int> UA_IDNO
                {
                    get
                    {
                        return this._UA_IDNO;
                    }
                    set
                    {
                        if ((this._UA_IDNO != value))
                        {
                            this._UA_IDNO = value;
                        }
                    }
                }

                //[Column(Storage = "_SIGNED_DATE", DbType = "DateTime")]
                public System.Nullable<System.DateTime> SIGNED_DATE
                {
                    get
                    {
                        return this._SIGNED_DATE;
                    }
                    set
                    {
                        if ((this._SIGNED_DATE != value))
                        {
                            this._SIGNED_DATE = value;
                        }
                    }
                }

                //[Column(Storage = "_SIGNED", DbType = "Bit")]
                public System.Nullable<bool> SIGNED
                {
                    get
                    {
                        return this._SIGNED;
                    }
                    set
                    {
                        if ((this._SIGNED != value))
                        {
                            this._SIGNED = value;
                        }
                    }
                }

                //[Column(Storage = "_IP_ADDRESS", DbType = "NVarChar(50)")]
                public string IP_ADDRESS
                {
                    get
                    {
                        return this._IP_ADDRESS;
                    }
                    set
                    {
                        if ((this._IP_ADDRESS != value))
                        {
                            this._IP_ADDRESS = value;
                        }
                    }
                }


                public string MAC_ADDRESS
                {
                    get
                    {
                        return this._MAC_ADDRESS;
                    }
                    set
                    {
                        if ((this._MAC_ADDRESS != value))
                        {
                            this._MAC_ADDRESS = value;
                        }
                    }
                }

                //[Column(Storage = "_AUDIT_DATE", DbType = "DateTime")]
                public System.Nullable<System.DateTime> AUDIT_DATE
                {
                    get
                    {
                        return this._AUDIT_DATE;
                    }
                    set
                    {
                        if ((this._AUDIT_DATE != value))
                        {
                            this._AUDIT_DATE = value;
                        }
                    }
                }

                //[Column(Storage = "_USER_ID", DbType = "NVarChar(20)")]
                public string USER_ID
                {
                    get
                    {
                        return this._USER_ID;
                    }
                    set
                    {
                        if ((this._USER_ID != value))
                        {
                            this._USER_ID = value;
                        }
                    }
                }

               // [Column(Storage = "_COLLEGE_CODE", DbType = "NVarChar(20)")]
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

                public int Parent_UserType
                {
                    get { return _parent_uatype; }
                    set { _parent_uatype = value; }
                }
                public string MOBILE
                {
                    get
                    {
                        return this._MOBILE;
                    }
                    set
                    {
                        if ((this._MOBILE != value))
                        {
                            this._MOBILE = value;
                        }
                    }
                }

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

                public string UA_AccNo
                {
                    get { return _ua_accno; }
                    set { _ua_accno = value; }
                }

                public string UA_IFSC
                {
                    get { return _ua_ifsc; }
                    set { _ua_ifsc = value; }
                }



                                
                #region Public Properties
                public string UA_Acc
                {
                    get { return _ua_acc; }
                    set { _ua_acc = value; }
                }

                public int UA_Dec
                {
                    get { return _ua_dec; }
                    set { _ua_dec = value; }
                }

                public string UA_DeptNo
                {
                    get { return _ua_deptno; }
                    set { _ua_deptno = value; }
                }

                public string UA_Desig
                {
                    get { return _ua_desig; }
                    set { _ua_desig = value; }
                }

                public string UA_Email
                {
                    get { return _ua_email; }
                    set { _ua_email = value; }
                }

                public int UA_EmpDeptNo
                {
                    get { return _ua_empdeptno; }
                    set { _ua_empdeptno = value; }
                }

                public string UA_EmpSt
                {
                    get { return _ua_empst; }
                    set { _ua_empst = value; }
                }
                public int EMP_IDNo
                {
                    get { return _emp_Idno; }
                    set { _emp_Idno = value; }
                }
                public string UA_FullName
                {
                    get { return _ua_fullname; }
                    set { _ua_fullname = value; }
                }

                public string UA_FatherName
                {
                    get { return _ua_fathername; }
                    set { _ua_fathername = value; }
                }
                public string UA_FatherMobile
                {
                    get { return _ua_fathermobile; }
                    set { _ua_fathermobile = value; }
                }

                public int UA_IDNo
                {
                    get { return _ua_idno; }
                    set { _ua_idno = value; }
                }

                public string UA_Name
                {
                    get { return _ua_name; }
                    set { _ua_name = value; }
                }

                public int UA_No
                {
                    get { return _ua_no; }
                    set { _ua_no = value; }
                }

                public string UA_OldPwd
                {
                    get { return _ua_oldpwd; }
                    set { _ua_oldpwd = value; }
                }

                public string UA_Pwd
                {
                    get { return _ua_pwd; }
                    set { _ua_pwd = value; }
                }

                public string UA_QtrNo
                {
                    get { return _ua_qtrno; }
                    set { _ua_qtrno = value; }
                }

                public int UA_Status
                {
                    get { return _ua_status; }
                    set { _ua_status = value; }
                }

                public int UA_Type
                {
                    get { return _ua_type; }
                    set { _ua_type = value; }
                }
                public bool UA_FirstLogin
                {
                    get { return _ua_firstlog; }
                    set { _ua_firstlog= value; }
                }
                public string College_No
                {
                    get
                    {
                        return this._College_No;
                    }
                    set
                    {
                        if ((this._College_No != value))
                        {
                            this._College_No = value;
                        }
                    }
                }

                public string UA_Remark
                {
                    get { return _ua_remark; }
                    set { _ua_remark = value; }
                }

                public int UA_Userno
                {
                    get { return _ua_userno; }
                    set { _ua_userno = value; }
                }

                public int UA_SourcePageNo 
                {
                    get { return _ua_sourcepageno; }
                    set { _ua_sourcepageno = value; }
                }

               
                #endregion

                //Added by S.P as Umesh sir told
                public int OrganizationId
                {
                    get;
                    set;
                }
            }

        }//END: BusinessLayer.BusinessEntities

    }//END: UAIMS  

}//END: IITMS