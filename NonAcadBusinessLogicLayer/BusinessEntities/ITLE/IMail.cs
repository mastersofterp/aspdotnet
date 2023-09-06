using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class IMail
            {
                #region Private Members

                private int _MAIL_NO;

                private System.Nullable<int> _SESSIONNO;

                private int _FROMUA_NO;

                private int _TOUA_NO;

                private string _SUBJECT;

                private string _MESSAGE;

                private string _ATTACHMENT1 = string.Empty;

                private string _ATTACHMENT2 = string.Empty;

                private string _ATTACHMENT3 = string.Empty;

                private string _FILENAME = string.Empty;

                private string _OLDFILENAME = string.Empty;

                private System.Nullable<System.DateTime> _MAILDATE;

                private System.Nullable<char> _STATUS;

                private string _COLLEGE_CODE;

                #endregion

                #region Public Properties

                public int MAIL_NO
                {
                    get
                    {
                        return this._MAIL_NO;
                    }
                    set
                    {
                        if ((this._MAIL_NO != value))
                        {
                            this._MAIL_NO = value;
                        }
                    }
                }

                public System.Nullable<int> SESSIONNO
                {
                    get
                    {
                        return this._SESSIONNO;
                    }
                    set
                    {
                        if ((this._SESSIONNO != value))
                        {
                            this._SESSIONNO = value;
                        }
                    }
                }

                public int FROMUA_NO
                {
                    get
                    {
                        return this._FROMUA_NO;
                    }
                    set
                    {
                        if ((this._FROMUA_NO != value))
                        {
                            this._FROMUA_NO = value;
                        }
                    }
                }

                public int TOUA_NO
                {
                    get
                    {
                        return this._TOUA_NO;
                    }
                    set
                    {
                        if ((this._TOUA_NO != value))
                        {
                            this._TOUA_NO = value;
                        }
                    }
                }

                public string SUBJECT
                {
                    get
                    {
                        return this._SUBJECT;
                    }
                    set
                    {
                        if ((this._SUBJECT != value))
                        {
                            this._SUBJECT = value;
                        }
                    }
                }

                public string MESSAGE
                {
                    get
                    {
                        return this._MESSAGE;
                    }
                    set
                    {
                        if ((this._MESSAGE != value))
                        {
                            this._MESSAGE = value;
                        }
                    }
                }

                public string ATTACHMENT1
                {
                    get
                    {
                        return this._ATTACHMENT1;
                    }
                    set
                    {
                        if ((this._ATTACHMENT1 != value))
                        {
                            this._ATTACHMENT1 = value;
                        }
                    }
                }

                public string ATTACHMENT2
                {
                    get
                    {
                        return this._ATTACHMENT2;
                    }
                    set
                    {
                        if ((this._ATTACHMENT2 != value))
                        {
                            this._ATTACHMENT2 = value;
                        }
                    }
                }

                public string ATTACHMENT3
                {
                    get
                    {
                        return this._ATTACHMENT3;
                    }
                    set
                    {
                        if ((this._ATTACHMENT3 != value))
                        {
                            this._ATTACHMENT3 = value;
                        }
                    }
                }

                public string FILENAME
                {
                    get
                    {
                        return this._FILENAME;
                    }
                    set
                    {
                        this._FILENAME = value;
                    }
                }

                public string OLDFILENAME
                {
                    get
                    {
                        return this._OLDFILENAME;
                    }
                    set
                    {
                        this._OLDFILENAME = value;
                    }
                }

                public System.Nullable<System.DateTime> MAILDATE
                {
                    get
                    {
                        return this._MAILDATE;
                    }
                    set
                    {
                        if ((this._MAILDATE != value))
                        {
                            this._MAILDATE = value;
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
    

