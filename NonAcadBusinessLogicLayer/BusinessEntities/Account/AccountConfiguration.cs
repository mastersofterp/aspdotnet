using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLayer.BusinessEntities
        {
            public class AccountConfiguration
            {
                #region Private Members
                private int _ConfigId = 0;
                private string _ConfiguraionDesc = string.Empty;
                private string _ConfigValue = string.Empty;
                private string _Signature1 = string.Empty;

                public string Signature1
                {
                    get { return _Signature1; }
                    set { _Signature1 = value; }
                }
                private string _Signature2 = string.Empty;

                public string Signature2
                {
                    get { return _Signature2; }
                    set { _Signature2 = value; }
                }
                private string _Signature3 = string.Empty;

                public string Signature3
                {
                    get { return _Signature3; }
                    set { _Signature3 = value; }
                }
                private string _Signature4 = string.Empty;

                public string Signature4
                {
                    get { return _Signature4; }
                    set { _Signature4 = value; }
                }
                private string _Signature5 = string.Empty;

                public string Signature5
                {
                    get { return _Signature5; }
                    set { _Signature5 = value; }
                }
                #endregion

                #region Public
                public int ConfigId
                {
                    get { return _ConfigId; }
                    set { _ConfigId = value; }
                }
                public string ConfiguraionDesc
                {
                    get { return _ConfiguraionDesc; }
                    set { _ConfiguraionDesc = value; }
                }
                public string ConfigValue
                {
                    get { return _ConfigValue; }
                    set { _ConfigValue = value; }
                }
                
                #endregion
            }

        }//END: BusinessLayer.BusinessEntities

    }//END: UAIMS  

}//END: IITMS