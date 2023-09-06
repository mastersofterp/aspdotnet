

//====================================================
//CREATED BY: SWATI GHATE
//CREATED DATE: 26-08-2015
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
            public class SchoolMaster
            {

                #region Private members
              
                private int _SCHOOL_NO;
                private int _DEPT_NO;
                private int _SCHOOL_DEPT_NO;
                private string _SCHOOL_NAME;

                private int _APPOINT_NO;
                private DateTime _APP_DATE = DateTime.MinValue;
                private DateTime _DURATION_DATE = DateTime.MinValue;
                private string _HOD_NAME;
                private string _DEAN_NAME;
                private char _STATUS;
                private string _COLLEGE_CODE;
                private int _USER = 0;
                private DateTime _DATE = DateTime.MinValue;
                private int _APPOINT_BY_NO=0;
                private string _REMARK = string.Empty;
                private int _HOD_NO; private int _DEAN_NO;
                # endregion

                #region public members
                public char STATUS
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
                public int HOD_NO
                {
                    get
                    {
                        return this._HOD_NO;
                    }
                    set
                    {
                        if ((this._HOD_NO != value))
                        {
                            this._HOD_NO = value;
                        }
                    }
                }
                public int DEAN_NO
                {
                    get
                    {
                        return this._DEAN_NO;
                    }
                    set
                    {
                        if ((this._DEAN_NO != value))
                        {
                            this._DEAN_NO = value;
                        }
                    }
                }
                public int APPOINT_NO
                {
                    get
                    {
                        return this._APPOINT_NO;
                    }
                    set
                    {
                        if ((this._APPOINT_NO != value))
                        {
                            this._SCHOOL_NO = value;
                        }
                    }
                }
                //[Table(Name="PAY_LEAVE")]	
                public int SCHOOL_NO
                {
                    get
                    {
                        return this._SCHOOL_NO;
                    }
                    set
                    {
                        if ((this._SCHOOL_NO != value))
                        {
                            this._SCHOOL_NO = value;
                        }
                    }
                }
                public int DEPT_NO
                {
                    get
                    {
                        return this._DEPT_NO;
                    }
                    set
                    {
                        if ((this._DEPT_NO != value))
                        {
                            this._DEPT_NO = value;
                        }
                    }
                }

                public int SCHOOL_DEPT_NO
                {
                    get
                    {
                        return this._SCHOOL_DEPT_NO;
                    }
                    set
                    {
                        if ((this._SCHOOL_DEPT_NO != value))
                        {
                            this._SCHOOL_DEPT_NO = value;
                        }
                    }
                }
                public string SCHOOL_NAME
                {
                    get
                    {
                        return this._SCHOOL_NAME;
                    }
                    set
                    {
                        if ((this._SCHOOL_NAME != value))
                        {
                            this._SCHOOL_NAME = value;
                        }
                    }
                }
                //private string ;
                public string HOD_NAME
                {
                    get
                    {
                        return this._HOD_NAME;
                    }
                    set
                    {
                        if ((this._HOD_NAME != value))
                        {
                            this._HOD_NAME = value;
                        }
                    }
                }
                //private string ;
                public string DEAN_NAME
                {
                    get
                    {
                        return this._DEAN_NAME;
                    }
                    set
                    {
                        if ((this._DEAN_NAME != value))
                        {
                            this._DEAN_NAME = value;
                        }
                    }
                }
                public int USER
                {
                    get { return _USER; }
                    set { _USER = value; }
                }

                public int APPOINT_BY_NO
                {
                    get { return _APPOINT_BY_NO; }
                    set { _APPOINT_BY_NO = value; }
                }

                public string REMARK
                {
                    get { return _REMARK; }
                    set { _REMARK = value; }
                }

                public DateTime DATE
                {
                    get { return _DATE; }
                    set { _DATE = value; }
                }
                  //_APP_DATE,_DURATION_DATE
                public DateTime APP_DATE
                {
                    get { return _APP_DATE; }
                    set { _APP_DATE = value; }
                }
                public DateTime DURATION_DATE
                {
                    get { return _DURATION_DATE; }
                    set { _DURATION_DATE = value; }
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

