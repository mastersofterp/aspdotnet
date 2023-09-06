//================================================================
//CREATED BY    : MRUNAL SINGH
//PROJECT NAME  : UAIMS
//MODULE NAME   : SPORTS & EVENT
//CREATION DATE : 26-APR-2017      
//================================================================  

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class PassignAuthorityEnt
            {
                #region Approval Authority

                private int _PANO;
                private string _PANAME;
                private int _UANO;
                private int _COLLEGE_NO;
                private string _COLLEGE_CODE;         

             
                public int PANO
                {
                    get
                    { return this._PANO; }
                    set
                    {
                        if ((this._PANO != value))
                        {
                            this._PANO = value;
                        }
                    }
                }

                public string PANAME
                {
                    get
                    {
                        return this._PANAME;
                    }
                    set
                    {
                        if ((this._PANAME != value))
                        {
                            this._PANAME = value;
                        }
                    }

                }

                public int UANO
                {
                    get
                    {
                        return this._UANO;
                    }
                    set
                    {
                        if ((this._UANO != value))
                        {
                            this._UANO = value;
                        }
                    }
                }


                public int COLLEGE_NO
                {
                    get
                    { return this._COLLEGE_NO; }
                    set
                    {
                        if ((this._COLLEGE_NO != value))
                        {
                            this._COLLEGE_NO = value;
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



                #region Approval Passing Path

              
                private int _PAPNO;
                private int _PAN01;
                private int _PAN02;
                private int _PAN03;
                private int _PAN04;
                private int _PAN05;
                private string _PAPATH;
                private int _DEPTNO;
                private int _SUBDESIGNO;

                private int _EVENTNO;
                public int EVENTNO
                {
                    get
                    {
                        return this._EVENTNO;
                    }
                    set
                    {
                        if ((this._EVENTNO != value))
                        {
                            this._EVENTNO = value;
                        }
                    }
                }


                public int PAPNO
                {
                    get
                    {
                        return this._PAPNO;
                    }
                    set
                    {
                        if ((this._PAPNO != value))
                        {
                            this._PAPNO = value;
                        }
                    }
                }

                public int PAN01
                {
                    get
                    {
                        return this._PAN01;
                    }
                    set
                    {
                        if ((this._PAN01 != value))
                        {
                            this._PAN01 = value;
                        }
                    }
                }
                public int PAN02
                {
                    get
                    {
                        return this._PAN02;
                    }
                    set
                    {
                        if ((this._PAN02 != value))
                        {
                            this._PAN02 = value;
                        }
                    }
                }
                public int PAN03
                {
                    get
                    {
                        return this._PAN03;
                    }
                    set
                    {
                        if ((this._PAN03 != value))
                        {
                            this._PAN03 = value;
                        }
                    }
                }

                public int PAN04
                {
                    get
                    {
                        return this._PAN04;
                    }
                    set
                    {
                        if ((this._PAN04 != value))
                        {
                            this._PAN04 = value;
                        }
                    }
                }

                public int PAN05
                {
                    get
                    {
                        return this._PAN05;
                    }
                    set
                    {
                        if ((this._PAN05 != value))
                        {
                            this._PAN05 = value;
                        }
                    }
                }
                public string PAPATH
                {
                    get
                    {
                        return this._PAPATH;
                    }
                    set
                    {
                        if ((this._PAPATH != value))
                        {
                            this._PAPATH = value;
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
                #endregion

            }
        }
    }
}

