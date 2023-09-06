using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {

            public class UserRights
            {
                #region Private Members
                private int _usertypeid = 0;
                private string _userdesc = string.Empty;
                private string _userrights = string.Empty;
                #endregion

                #region Public Properties
                public int UserTypeID
                {
                    get { return _usertypeid; }
                    set { _usertypeid = value; }
                }

                public string UserDesc
                {
                    get { return _userdesc; }
                    set { _userdesc = value; }
                }

                public string UserRightss
                {
                    get { return _userrights; }
                    set { _userrights = value; }
                }
                #endregion

            }

        }//END: UAIMS  

    }//END: IITMS
}