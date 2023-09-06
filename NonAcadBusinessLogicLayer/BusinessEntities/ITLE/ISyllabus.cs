using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class ISyllabus
            {
                #region Private Members

                private int _SUB_NO=0;

                private System.Nullable<int> _SESSIONNO=0;

                private System.Nullable<int> _COURSENO=0;

                private System.Nullable<int> _UA_NO=0;

                private string _SYLLABUS_NAME = string.Empty;

                private string _UNIT_NAME = string.Empty;

                private string _TOPIC_NAME = string.Empty;

                private string _DESCRIPTION = string.Empty;

                private System.Nullable<System.DateTime> _CREATED_DATE = DateTime.MinValue;

                private string _COLLEGE_CODE = string.Empty;

                private string _ATTACHMENT;

                private string _FILENAME = string.Empty;

                private string _OLDFILENAME = string.Empty;

                private int _IsBlob;
                private string _FILE_PATH;
                #endregion

                #region Public Properties

                public string FILE_PATH
                {
                    get
                    {
                        return this._FILE_PATH;
                    }
                    set
                    {
                        if ((this._FILE_PATH != value))
                        {
                            this._FILE_PATH = value;
                        }
                    }
                }
                public int IsBlob
                {
                    get
                    {
                        return this._IsBlob;
                    }
                    set
                    {
                        if ((this._IsBlob != value))
                        {
                            this._IsBlob = value;
                        }
                    }
                }
                public int SUB_NO
                {
                    get
                    {
                        return this._SUB_NO;
                    }
                    set
                    {
                        if ((this._SUB_NO != value))
                        {
                            this._SUB_NO = value;
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

                public System.Nullable<int> COURSENO
                {
                    get
                    {
                        return this._COURSENO;
                    }
                    set
                    {
                        if ((this._COURSENO != value))
                        {
                            this._COURSENO = value;
                        }
                    }
                }

                public System.Nullable<int> UA_NO
                {
                    get
                    {
                        return this._UA_NO;
                    }
                    set
                    {
                        if ((this._UA_NO != value))
                        {
                            this._UA_NO = value;
                        }
                    }
                }

                public string SYLLABUS_NAME
                {
                    get
                    {
                        return this._SYLLABUS_NAME;
                    }
                    set
                    {
                        if ((this._SYLLABUS_NAME != value))
                        {
                            this._SYLLABUS_NAME = value;
                        }
                    }
                }

                public string UNIT_NAME
                {
                    get
                    {
                        return this._UNIT_NAME;
                    }
                    set
                    {
                        if ((this._UNIT_NAME != value))
                        {
                            this._UNIT_NAME = value;
                        }
                    }
                }

                public string TOPIC_NAME
                {
                    get
                    {
                        return this._TOPIC_NAME;
                    }
                    set
                    {
                        if ((this._TOPIC_NAME != value))
                        {
                            this._TOPIC_NAME = value;
                        }
                    }
                }

                public string DESCRIPTION
                {
                    get
                    {
                        return this._DESCRIPTION;
                    }
                    set
                    {
                        if ((this._DESCRIPTION != value))
                        {
                            this._DESCRIPTION = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> CREATED_DATE
                {
                    get
                    {
                        return this._CREATED_DATE;
                    }
                    set
                    {
                        if ((this._CREATED_DATE != value))
                        {
                            this._CREATED_DATE = value;
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

                public string ATTACHMENT
                {
                    get
                    {
                        return this._ATTACHMENT;
                    }
                    set
                    {
                        if ((this._ATTACHMENT != value))
                        {
                            this._ATTACHMENT = value;
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

                #endregion
            }
        }
    }
}
