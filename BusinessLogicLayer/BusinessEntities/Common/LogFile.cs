using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class LogFile
            {
                #region Private Members
                private int _id = 0;
                private string _ua_name = string.Empty;
                private DateTime _logintime = DateTime.Now;
                private DateTime _logouttime = DateTime.Now;
                #endregion

                #region Public Property Fields
                public int ID
                {
                    get { return _id; }
                    set { _id = value; }
                }

                public string Ua_Name
                {
                    get { return _ua_name; }
                    set { _ua_name = value; }
                }

                public DateTime LoginTime
                {
                    get { return _logintime; }
                    set { _logintime = value; }
                }

                public DateTime LogoutTime
                {
                    get { return _logouttime; }
                    set { _logouttime = value; }
                }
                #endregion
            }

        }//END: BusinessLayer.BusinessEntities

    }//END: UAIMS  

}//END: IITMS