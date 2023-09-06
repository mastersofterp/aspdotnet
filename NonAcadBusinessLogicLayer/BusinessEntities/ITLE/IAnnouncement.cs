using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class IAnnouncement
            {
                #region Private Members

                private int _AN_NO;

                private int _UA_NO;

                private System.Nullable<int> _SESSIONNO;

                private int _COURSENO;

                private System.Nullable<System.DateTime> _STARTDATE = DateTime.Now;

                private System.Nullable<System.DateTime> _EXPDATE = DateTime.Now;

                private string _SUBJECT = string.Empty;

                private string _DESCRIPTION = string.Empty;

                private string _ATTACHMENT = string.Empty;

                private string _FILENAME = string.Empty;

                private string _OLDFILENAME = string.Empty;

                private System.Nullable<char> _STATUS;

                private string _COLLEGE_CODE = string.Empty; private int _AWDID;

                #endregion 

                #region Public Properties

                public int AN_NO
                {
                    get
                    {
                        return this._AN_NO;
                    }
                    set
                    {
                        if ((this._AN_NO != value))
                        {
                            this._AN_NO = value;
                        }
                    }
                }

                public int UA_NO
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

                public int COURSENO
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

                public System.Nullable<System.DateTime> STARTDATE
                {
                    get
                    {
                        return this._STARTDATE;
                    }
                    set
                    {
                        if ((this._STARTDATE != value))
                        {
                            this._STARTDATE = value;
                        }
                    }
                }

                public System.Nullable<System.DateTime> EXPDATE
                {
                    get
                    {
                        return this._EXPDATE;
                    }
                    set
                    {
                        if ((this._EXPDATE != value))
                        {
                            this._EXPDATE = value;
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

                public int AWDID
                {
                    get
                    {
                        return this._AWDID;
                    }
                    set
                    {
                        if ((this._AWDID != value))
                        {
                            this._AWDID = value;
                        }
                    }
                }
                
                #endregion

            }
        }
    }
}       
         
      
  

