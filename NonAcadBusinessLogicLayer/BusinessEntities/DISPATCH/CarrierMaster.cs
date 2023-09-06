using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLogicLayer.BusinessEntities.Dispatch
        {
            public class CarrierMaster
            {
                 #region Courier Carrier Master

                #region Private Member
                private int _carrierNo = 0;
                private string _carrierName = string.Empty;
                private string _college_code = string.Empty;
                private int _letterCategory = 0;
                #endregion

                #region Public Members
                public int carrierNo
                {
                    get { return _carrierNo; }
                    set { _carrierNo = value; }
                }
                public string carrierName
                {
                    get { return _carrierName; }
                    set { _carrierName = value; }
                }
                public string college_code
                {
                    get { return _college_code;}
                    set { _college_code = value; }
                }
                public int letterCategory
                {
                    get { return _letterCategory; }
                    set { _letterCategory = value; }
                }

                #endregion

                #endregion
            }
        }
    }
}
