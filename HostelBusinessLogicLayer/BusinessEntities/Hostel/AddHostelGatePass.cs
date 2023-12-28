using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class AddHostelGatePass
            {
                #region Private Members
                private int _passNo = 0;
                #endregion

                #region Public Property Fields
                public int PassNo
                {
                    get { return _passNo; }
                    set { _passNo = value; }
                }
                #endregion
            }
        }
    }
}
