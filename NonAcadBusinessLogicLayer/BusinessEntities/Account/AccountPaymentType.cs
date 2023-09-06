//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : PAYMENT GROUP ENTRY                                                    
// CREATION DATE : 16-August-2015                                               
// CREATED BY    : NAKUL CHAWRE                                                
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================
using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class AccountPaymentType
            {
                #region Private Declaration

                private int _PGROUP_NO = 0;
                
                private string _GROUPNAME = string.Empty;
                
                private string _PAYTYPENO = string.Empty;
                
                private int _USERID = 0;
                
                private int _COLLEGE_ID = 0;

                #endregion Private Declaration

                #region Public Methods

                public int PGROUP_NO
                {
                    get { return _PGROUP_NO; }
                    set { _PGROUP_NO = value; }
                }

                public string GROUPNAME
                {
                    get { return _GROUPNAME; }
                    set { _GROUPNAME = value; }
                }

                public string PAYTYPENO
                {
                    get { return _PAYTYPENO; }
                    set { _PAYTYPENO = value; }
                }

                public int USERID
                {
                    get { return _USERID; }
                    set { _USERID = value; }
                }

                public int COLLEGE_ID
                {
                    get { return _COLLEGE_ID; }
                    set { _COLLEGE_ID = value; }
                }

                #endregion Public Methods
            }
        }
    }
}