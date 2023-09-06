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
            public class QuotaMaster
            {

                #region Private members
              
                private int _QUOTA_NO;
                private int _POST_NO;         
                private string _QUOTA_RULE;
                private string _COLLEGE_CODE;
             
                # endregion

                #region public members
              
                public int QUOTA_NO
                {
                    get
                    {
                        return this._QUOTA_NO;
                    }
                    set
                    {
                        if ((this._QUOTA_NO != value))
                        {
                            this._QUOTA_NO = value;
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
             
                public string QUOTA_RULE
                {
                    get
                    {
                        return this._QUOTA_RULE;
                    }
                    set
                    {
                        if ((this._QUOTA_RULE != value))
                        {
                            this._QUOTA_RULE = value;
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

