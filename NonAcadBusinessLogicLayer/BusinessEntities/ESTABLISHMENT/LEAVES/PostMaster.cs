//====================================================
//CREATED BY: SWATI GHATE
//CREATED DATE: 27-02-2016
//====================================================
using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class PostMaster
            {

                #region Private members

                private int _POST_CODE_NO;
                private int _POST_NO;
                private int _SUBDEPTNO;
                private int _STAFFNO;
                private string _POST_NAME = string.Empty;
                private string _ORDERNO = string.Empty;
                private int _MODE_NO;
                private DateTime _ORDER_DATE = DateTime.MinValue;
                private string _STATUS = string.Empty;
                private string _QUOTA = string.Empty;
                private string _POST_CODE = string.Empty;
                private string _COLLEGE_CODE;
                private int _USER = 0;
                private int _TOTAL_POST;
                private int _PLAN_NO = 0;

                
                # endregion

                #region public members
                public int PLAN_NO
                {
                    get
                    {
                        return this._PLAN_NO;
                    }
                    set
                    {
                        if ((this._PLAN_NO != value))
                        {
                            this._PLAN_NO = value;
                        }
                    }
                }
                public int TOTAL_POST
                {
                    get
                    {
                        return this._TOTAL_POST;
                    }
                    set
                    {
                        if ((this._TOTAL_POST != value))
                        {
                            this._TOTAL_POST = value;
                        }
                    }
                }
                public string ORDERNO
                {
                    get
                    {
                        return this._ORDERNO;
                    }
                    set
                    {
                        if ((this._ORDERNO != value))
                        {
                            this._ORDERNO = value;
                        }
                    }
                }
               
                public int MODE_NO
                {
                    get
                    {
                        return this._MODE_NO;
                    }
                    set
                    {
                        if ((this._MODE_NO != value))
                        {
                            this._MODE_NO = value;
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
                public int POST_CODE_NO
                {
                    get
                    {
                        return this._POST_CODE_NO;
                    }
                    set
                    {
                        if ((this._POST_CODE_NO != value))
                        {
                            this._POST_CODE_NO = value;
                        }
                    }
                }
                //
                public int SUBDEPTNO
                {
                    get
                    {
                        return this._SUBDEPTNO;
                    }
                    set
                    {
                        if ((this._SUBDEPTNO != value))
                        {
                            this._SUBDEPTNO = value;
                        }
                    }
                }

                public int STAFFNO
                {
                    get
                    {
                        return this._STAFFNO;
                    }
                    set
                    {
                        if ((this._STAFFNO != value))
                        {
                            this._STAFFNO = value;
                        }
                    }
                }
                public string POST_NAME
                {
                    get
                    {
                        return this._POST_NAME;
                    }
                    set
                    {
                        if ((this._POST_NAME != value))
                        {
                            this._POST_NAME = value;
                        }
                    }
                }

                public string POST_CODE
                {
                    get
                    {
                        return this._POST_CODE;
                    }
                    set
                    {
                        if ((this._POST_CODE != value))
                        {
                            this._POST_CODE = value;
                        }
                    }
                }
                public string QUOTA
                {
                    get
                    {
                        return this._QUOTA;
                    }
                    set
                    {
                        if ((this._QUOTA != value))
                        {
                            this._QUOTA = value;
                        }
                    }
                }
                public string STATUS
                {
                    get { return _STATUS; }
                    set { _STATUS = value; }
                }
                public int USER
                {
                    get { return _USER; }
                    set { _USER = value; }
                }
              
                public DateTime ORDER_DATE
                {
                    get { return _ORDER_DATE; }
                    set { _ORDER_DATE = value; }
                }               
                public string COLLEGE_CODE
                {
                    get { return _COLLEGE_CODE; }
                    set { _COLLEGE_CODE = value; }
                }
                #endregion


            } //end class Leaves
        }//end namespace  BusinessLogicLayer.BusinessEntities 
    }//end namespace UAIMS
}//end namespace IITMS

