using System;
using System.Collections.Generic;
using System.Text;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class LeavingCertificate
            {
                #region Private members
               
                private string _collegeCode;
                #endregion

                #region Public Property Fields
               
                public string CollegeCode
                {
                    get { return _collegeCode; }
                    set { _collegeCode = value; }
                }

                #endregion
            }
        }
    }
}
