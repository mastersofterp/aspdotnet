using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class Acc_Section
            {
                #region Private Members
                private int _as_no = 0;
                private string _as_title = string.Empty;
                private decimal _as_srno = 0.0M;
                #endregion

                #region Public Properties
                public int As_No
                {
                    get { return _as_no; }
                    set { _as_no = value; }
                }

                public string As_Title
                {
                    get { return _as_title; }
                    set { _as_title = value; }
                }

                public decimal As_SrNo
                {
                    get { return _as_srno; }
                    set { _as_srno = value; }
                }
                #endregion
            }

        }//END: BusinessLayer.BusinessEntities

    }//END: UAIMS  

}//END: IITMS