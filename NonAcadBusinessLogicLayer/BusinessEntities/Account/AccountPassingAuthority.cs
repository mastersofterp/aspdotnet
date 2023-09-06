using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class AccountPassingAuthority
            {
                #region Private Members

                //Passing Authority
                private int _PANO;
                private string _PANAME;
                private int _UANO;
                private int _College_No;
                private int _College_Code;

                //Passing Authority Path
                private int _PAPNO;
                private int _PAN01;
                private int _PAN02;
                private int _PAN03;
                private int _PAN04;
                private int _PAN05;
                private string _PAPATH;
                private int _DEPTNO;

                //Extraa
                private int _LNO;
                private int _EMPNO;

                #endregion

                #region Public Members

                //Passing Auhtority
                public int PANO
                {
                    get
                    {
                        return this._PANO;
                    }
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
                public int College_No
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

                //Passing Authority Path
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
                public int College_Code
                {
                    get
                    {
                        return this._College_Code;
                    }
                    set
                    {
                        if ((this._College_Code != value))
                        {
                            this._College_Code = value;
                        }
                    }
                }

                //EXTRAA
                public int LNO
                {
                    get
                    {
                        return this._LNO;
                    }
                    set
                    {
                        if ((this._LNO != value))
                        {
                            this._LNO = value;
                        }
                    }
                }
                public int EMPNO
                {
                    get
                    {
                        return this._EMPNO;
                    }
                    set
                    {
                        if ((this._EMPNO != value))
                        {
                            this._EMPNO = value;
                        }
                    }
                }

                #endregion
            }
        }
    }
}
