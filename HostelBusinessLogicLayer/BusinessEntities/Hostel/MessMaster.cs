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
            public class MessMaster
            {
                #region Private
                private int _MESS_NO = 0;
                private string _MESS_NAME = string.Empty;
                private string _COLLEGE_CODE = string.Empty;
                private string _USERID = string.Empty;
                private string _IPADDRESS = string.Empty;
                private string _MACADDRESS = string.Empty;
                private DateTime _AUDITDATE = DateTime.MinValue;
                #endregion

                #region Public

                public int MESSNO
                {
                    get { return _MESS_NO; }
                    set { _MESS_NO = value; }
                }

                public string MESSNAME
                {
                    get { return _MESS_NAME; }
                    set { _MESS_NAME = value; }
                }

                public string COLLLEGECODE
                {
                    get { return _COLLEGE_CODE; }
                    set { _COLLEGE_CODE = value; }
                }

                public string USERID
                {
                    get { return _USERID; }
                    set { _USERID = value; }
                }

                public string IPADDRESS
                {
                    get { return _IPADDRESS; }
                    set { _IPADDRESS = value; }
                }

                public string MACADDRESS
                {
                    get { return _MACADDRESS; }
                    set { _MACADDRESS = value; }
                }

                public DateTime AUDITDATE
                {
                    get { return _AUDITDATE; }
                    set { _AUDITDATE = value; }
                }

                #endregion 
            }
        }
    }
}
