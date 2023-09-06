using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class EMP_ATTANDANCE
            {

                #region Private members
                //[Table(Name="PAY_LEAVE")]	
                private int _LOGINDEX;
                private System.Nullable<int> _NODEINDEX;
                private DateTime _LOGTIME;
                private int _USERID;
                private int _NODEID;
                private int _AUTHTYPE;
                private int _AUTHRESULT;
                private int _OPENRESULT;
                private int _FUNCTIONNO;
                private DateTime _SLOGTIME;
                private int _CHECKED;
                private string _RESERVE;

                //Added on 10/04/2023
                private int _COLLEGE_NO;
                private System.Nullable<int> _STNO;
                private int _PROCESSFROM;
                private int _PROCESSTO;

                # endregion

                #region public members
                //[Table(Name="ESTB_ATTANDANCE_LOG")]	
                public int LOGINDEX
                {
                    get
                    {
                        return this._LOGINDEX;
                    }
                    set
                    {
                        if ((this._LOGINDEX != value))
                        {
                            this._LOGINDEX = value;
                        }
                    }
                }
                public System.Nullable<int> NODEINDEX
                {
                    get
                    {
                        return this._NODEINDEX;
                    }
                    set
                    {
                        if ((this._NODEINDEX != value))
                        {
                            this._NODEINDEX = value;
                        }
                    }
                }
                public DateTime LOGTIME
                {
                    get
                    {
                        return this._LOGTIME;
                    }
                    set
                    {
                        if ((this._LOGTIME != value))
                        {
                            this._LOGTIME = value;
                        }
                    }
                }
                public int USERID
                {
                    get
                    {
                        return this._USERID;
                    }
                    set
                    {
                        if ((this._USERID != value))
                        {
                            this._USERID = value;
                        }
                    }
                }
                public int NODEID
                {
                    get
                    {
                        return this._NODEID;
                    }
                    set
                    {
                        if ((this._NODEID != value))
                        {
                            this._NODEID = value;
                        }
                    }
                }
                public int AUTHTYPE
                {
                    get
                    {
                        return this._AUTHTYPE;
                    }
                    set
                    {
                        if ((this._AUTHTYPE != value))
                        {
                            this._AUTHTYPE = value;
                        }
                    }
                }
                public int AUTHRESULT
                {
                    get
                    {
                        return this._AUTHRESULT;
                    }
                    set
                    {
                        if ((this._AUTHRESULT != value))
                        {
                            this._AUTHRESULT = value;
                        }

                    }
                }
                public int OPENRESULT
                {
                    get
                    {
                        return this._OPENRESULT;
                    }
                    set
                    {
                        if ((this._OPENRESULT != value))
                        {
                            this._OPENRESULT = value;
                        }
                    }
                }
                public int FUNCTIONNO
                {
                    get
                    {
                        return this._FUNCTIONNO;
                    }
                    set
                    {
                        if ((this._FUNCTIONNO != value))
                        {
                            this._FUNCTIONNO = value;
                        }
                    }
                }
                public DateTime SLOGTIME
                {
                    get
                    {
                        return this._SLOGTIME;
                    }
                    set
                    {
                        if ((this._SLOGTIME != value))
                        {
                            this._SLOGTIME = value;
                        }
                    }
                }
                public int CHECKED
                {
                    get
                    {
                        return this._CHECKED;
                    }
                    set
                    {
                        if ((this._CHECKED != value))
                        {
                            this._CHECKED = value;
                        }
                    }
                }
                public string RESERVE
                {
                    get
                    {
                        return this._RESERVE;
                    }
                    set
                    {
                        if ((this._RESERVE != value))
                        {
                            this._RESERVE = value;
                        }
                    }
                }

                //added on 10/04/2023
                public int COLLEGE_NO
                {
                    get
                    {
                        return this._COLLEGE_NO;

                    }
                    set
                    {
                        if ((this._COLLEGE_NO != value))
                        {
                            this._COLLEGE_NO = value;
                        }

                    }

                }
                public System.Nullable<int> STNO
                {
                    get
                    {
                        return this._STNO;
                    }
                    set
                    {
                        if ((this._STNO != value))
                        {
                            this._STNO = value;
                        }
                    }
                }
                public int PROCESS_FROM
                {
                    get
                    {
                        return this._PROCESSFROM;
                    }
                    set
                    {
                        if ((this._PROCESSFROM != value))
                        {
                            this._PROCESSFROM = value;
                        }
                    }
                }
                public int PROCESS_TO
                {
                    get
                    {
                        return this._PROCESSTO;
                    }
                    set
                    {
                        if ((this._PROCESSTO != value))
                        {
                            this._PROCESSTO = value;
                        }
                    }
                }

                #endregion


            } //end class Leaves
        }//end namespace  BusinessLogicLayer.BusinessEntities 
    }//end namespace NITPRM
}//end namespace IITMS
