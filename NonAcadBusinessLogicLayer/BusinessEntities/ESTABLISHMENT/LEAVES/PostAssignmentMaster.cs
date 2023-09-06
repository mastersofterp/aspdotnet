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
            public class PostAssignmentMaster
            {

                #region Private members

                private int _POST_ASSIGN_NO;  
                private int _STAFFNO;               
                private int _POST_NO;
                private int _POST_CODE_NO;
                private char _POST_STATUS;
                private int _IDNO;
                private int _MODE_NO;
                private int _CATEGORYNO;
                private DateTime _APP_DATE = DateTime.MinValue;
                private DateTime _VAC_DATE = DateTime.MinValue;              
                private string _STATUS_RECRUITMENT = string.Empty;
                private string _REMARK = string.Empty;           
                private string _COLLEGE_CODE;

                # endregion

                #region public members
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

                public int POST_ASSIGN_NO
                {
                    get
                    {
                        return this._POST_ASSIGN_NO;
                    }
                    set
                    {
                        if ((this._POST_ASSIGN_NO != value))
                        {
                            this._POST_ASSIGN_NO = value;
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
                public Char POST_STATUS
                {
                    get { return _POST_STATUS; }
                    set { _POST_STATUS = value; }
                }
             
                public int IDNO
                {
                    get
                    {
                        return this._IDNO;
                    }
                    set
                    {
                        if ((this._IDNO != value))
                        {
                            this._IDNO = value;
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

                public int CATEGORYNO
                {
                    get
                    {
                        return this._CATEGORYNO;
                    }
                    set
                    {
                        if ((this._CATEGORYNO != value))
                        {
                            this._CATEGORYNO = value;
                        }
                    }
                }
                                
                public DateTime APP_DATE
                {
                    get { return _APP_DATE; }
                    set { _APP_DATE = value; }
                }
                public DateTime VAC_DATE
                {
                    get { return _VAC_DATE; }
                    set { _VAC_DATE = value; }
                }  
                public string STATUS_RECRUITMENT
                {
                    get
                    {
                        return this._STATUS_RECRUITMENT;
                    }
                    set
                    {
                        if ((this._STATUS_RECRUITMENT != value))
                        {
                            this._STATUS_RECRUITMENT = value;
                        }
                    }
                }

                public string REMARK
                {
                    get
                    {
                        return this._REMARK;
                    }
                    set
                    {
                        if ((this._REMARK != value))
                        {
                            this._REMARK = value;
                        }
                    }
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

