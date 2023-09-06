using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class Help
            {
                #region Private Members
                private int _hal_no = 0;
                private int _helpid = 0;
                private string _helpdesc = string.Empty;
                #endregion

                #region Public Properties
                public int HelpId
                {
                    get { return _helpid; }
                    set { _helpid = value; }
                }

                public int Hal_No
                {
                    get { return _hal_no; }
                    set { _hal_no = value; }
                }

                public string HelpDesc
                {
                    get { return _helpdesc; }
                    set { _helpdesc = value; }
                }
                #endregion
            }

        }//END: BusinessLayer.BusinessEntities

    }//END: UAIMS  

}//END: IITMS