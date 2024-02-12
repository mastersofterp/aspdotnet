using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class Requisition
            {
                #region Private members
                private System.Nullable<bool> _IsActive;
                private int _REQUNO;
                private int _DEPTNO;
                private int _USERNO;
                private int _POSTCATNO;
                private int _POST_NO;
                private string _POSTNO;
                private int _COLLEGE_NO;
                private string _POST;
                private int _CREATEDBY;
                private int _MODIFIEDBY;
                private string _COLLEGE_CODE;

                private int _REQ_ID;
                private string _REQUISITION_NO;
                private int _NO_OF_POSITION;
                private string _DESCRIPTION;
                private DateTime _REQUEST_DATE;
                private DateTime _APPROVED_DATE;
                private string _APPROVAL_STATUS;
                private int _REQ_PANO;
                private int _PANO1;
                private int _PANO2;
                private int _PANO3;
                private int _PANO4;
                private int _PANO5;
                private string _PATH;


                #endregion


                #region public members
                public System.Nullable<bool> IsActive
                {
                    get
                    {
                        return this._IsActive;
                    }
                    set
                    {
                        if ((this._IsActive != value))
                        {
                            this._IsActive = value;
                        }
                    }
                }
                public int REQUNO
                {
                    get
                    {
                        return this._REQUNO;
                    }
                    set
                    {
                        if ((this._REQUNO != value))
                        {
                            this._REQUNO = value;
                        }
                    }
                }
                public int DEPTNO
                {
                    get
                    {
                        return this._DEPTNO;
                    }
                    set
                    {
                        if ((this._DEPTNO != value))
                        {
                            this._DEPTNO = value;
                        }
                    }
                }
                public int USERNO
                {
                    get
                    {
                        return this._USERNO;

                    }
                    set
                    {
                        if ((this._USERNO != value))
                        {
                            this._USERNO = value;
                        }

                    }

                }
                public int POSTCATNO
                {
                    get
                    {
                        return this._POSTCATNO;

                    }
                    set
                    {
                        if ((this._POSTCATNO != value))
                        {
                            this._POSTCATNO = value;
                        }

                    }

                }
                public string POSTNO
                {
                    get
                    {
                        return this._POSTNO;

                    }
                    set
                    {
                        if ((this._POSTNO != value))
                        {
                            this._POSTNO = value;
                        }

                    }

                }

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
                public string POST
                {
                    get
                    {
                        return this._POST;
                    }
                    set
                    {
                        if ((this._POST != value))
                        {
                            this._POST = value;
                        }
                    }
                }
                public int CREATEDBY
                {
                    get
                    {
                        return this._CREATEDBY;
                    }
                    set
                    {
                        if ((this._CREATEDBY != value))
                        {
                            this._CREATEDBY = value;
                        }
                    }
                }
                public int MODIFIEDBY
                {
                    get
                    {
                        return this._MODIFIEDBY;
                    }
                    set
                    {
                        if ((this._MODIFIEDBY != value))
                        {
                            this._MODIFIEDBY = value;
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
                public string REQUISITION_NO
                {
                    get
                    {
                        return this._REQUISITION_NO;
                    }
                    set
                    {
                        if ((this._REQUISITION_NO != value))
                        {
                            this._REQUISITION_NO = value;
                        }
                    }
                }
                public int NO_OF_POSITION
                {
                    get
                    {
                        return this._NO_OF_POSITION;
                    }
                    set
                    {
                        if ((this._NO_OF_POSITION != value))
                        {
                            this._NO_OF_POSITION = value;
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
                public DateTime REQUEST_DATE
                {
                    get
                    {
                        return this._REQUEST_DATE;
                    }
                    set
                    {
                        if ((this._REQUEST_DATE != value))
                        {
                            this._REQUEST_DATE = value;
                        }
                    }
                }
                public DateTime APPROVED_DATE
                {
                    get
                    {
                        return this._APPROVED_DATE;
                    }
                    set
                    {
                        if ((this._APPROVED_DATE != value))
                        {
                            this._APPROVED_DATE = value;
                        }
                    }
                }
                public string APPROVAL_STATUS
                {
                    get
                    {
                        return this._APPROVAL_STATUS;
                    }
                    set
                    {
                        if ((this._APPROVAL_STATUS != value))
                        {
                            this._APPROVAL_STATUS = value;
                        }
                    }
                }
                public int REQ_PANO
                {
                    get
                    {
                        return this._REQ_PANO;

                    }
                    set
                    {
                        if ((this._REQ_PANO != value))
                        {
                            this._REQ_PANO = value;
                        }

                    }

                }
                public int REQ_ID
                {
                    get
                    {
                        return this._REQ_ID;

                    }
                    set
                    {
                        if ((this._REQ_ID != value))
                        {
                            this._REQ_ID = value;
                        }

                    }

                }
                public int POST_NO
                {
                    get
                    {
                        return this._POST_NO;

                    }
                    set
                    {
                        if ((this._POST_NO != value))
                        {
                            this._POST_NO = value;
                        }

                    }

                }
                public int PANO1
                {
                    get
                    {
                        return this._PANO1;

                    }
                    set
                    {
                        if ((this._PANO1 != value))
                        {
                            this._PANO1 = value;
                        }

                    }

                }
                public int PANO2
                {
                    get
                    {
                        return this._PANO2;

                    }
                    set
                    {
                        if ((this._PANO2 != value))
                        {
                            this._PANO2 = value;
                        }

                    }

                }
                public int PANO3
                {
                    get
                    {
                        return this._PANO3;

                    }
                    set
                    {
                        if ((this._PANO3 != value))
                        {
                            this._PANO3 = value;
                        }

                    }

                }
                public int PANO4
                {
                    get
                    {
                        return this._PANO4;

                    }
                    set
                    {
                        if ((this._PANO4 != value))
                        {
                            this._PANO4 = value;
                        }

                    }

                }
                public int PANO5
                {
                    get
                    {
                        return this._PANO5;

                    }
                    set
                    {
                        if ((this._PANO5 != value))
                        {
                            this._PANO5 = value;
                        }

                    }

                }
                public string PATH
                {
                    get
                    {
                        return this._PATH;
                    }
                    set
                    {
                        if ((this._PATH != value))
                        {
                            this._PATH = value;
                        }
                    }
                }
                #endregion
            }
        }
    }
}
