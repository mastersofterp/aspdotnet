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
            public class ProvisionalCertificateEntity
            {
                #region Define Variable

                private Int64 _IDNO;
	            private int _SESSIONNO;
                private Int64 _CREATE_BY;
                private string _IPADDRESS;

                #endregion Define Variable

                #region Initialize Get & Set Methode

                public Int64 IDNO
                {
                    get { return _IDNO; }
                    set { _IDNO=value;}
                }

                public int SESSIONNO
                {
                    get { return _SESSIONNO; }
                    set { _SESSIONNO = value; }
                }

                public Int64 CREATE_BY
                {
                    get { return _CREATE_BY; }
                    set { _CREATE_BY = value; }
                }

                public string IPADDRESS
                {
                    get { return _IPADDRESS; }
                    set { _IPADDRESS = value; }
                }

                #endregion Initialize Get & Set Methode
            }
        }
    }
}