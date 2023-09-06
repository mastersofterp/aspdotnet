using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class IForumMaster
            {
                #region Private Members
                private int _FORUM_NO;
                private int _THREAD_NO;
                private int _MESSAGE_NO;
                private int _SESSIONNO;
                private int _UA_NO;
                private int _COURSENO;
                private string _FORUM;
                private string _THREAD;
                private string _MESSAGE;
                private string _DESCRIPTION;
                private System.Nullable<System.DateTime> _CREATEDDATE;
                #endregion

                #region Public Properties

                public int FORUM_NO
                {
                    get
                    {
                        return this._FORUM_NO;
                    }
                    set
                    {
                        if ((this._FORUM_NO != value))
                        {
                            this._FORUM_NO = value;
                        }
                    }
                }
                public int THREAD_NO
                {
                    get
                    {
                        return this._THREAD_NO;  
                    }
                    set
                    {
                        if ((this._THREAD_NO != value))
                        {
                            this._THREAD_NO = value;  
                        } 
                    }

                }
                public int MESSAGE_NO
                {

                    get
                    {
                        return this._MESSAGE_NO;                        
                    }
                    set
                    {
                        if ((this._MESSAGE_NO != value))
                        {
                            this._MESSAGE_NO = value;
                        }
                    }
                }


                public int SESSIONNO
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
                public string FORUM
                {
                    get
                    {
                        return this._FORUM;
                    }
                    set
                    {
                        if ((this._FORUM != value))
                        {
                            this._FORUM = value;
                        }
                    }
                }
                public string THREAD
                {
                    get
                    {
                        return this._THREAD;
                    }
                    set
                    {
                        if ((this._THREAD != value))
                        {
                            this._THREAD = value;
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
                public System.Nullable<System.DateTime> CREATEDDATE
                {
                    get
                    {
                        return this._CREATEDDATE;
                    }
                    set
                    {
                        if ((this._CREATEDDATE != value))
                        {
                            this._CREATEDDATE = value;
                        }
                    }
                }

                #endregion

            }
        }
    }
}