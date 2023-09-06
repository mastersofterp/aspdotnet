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
            public class Grid_Entity
            {
                #region Private Member
                string _CompCode = string.Empty;

                
                string _Ledger = string.Empty;

                
                int _Party_No = 0;

                
                DateTime _FromDate;

                
                DateTime _ToDate;

                
                #endregion

                #region
                public string CompCode
                {
                    get { return _CompCode; }
                    set { _CompCode = value; }
                }

                public string Ledger
                {
                    get { return _Ledger; }
                    set { _Ledger = value; }
                }

                public int Party_No
                {
                    get { return _Party_No; }
                    set { _Party_No = value; }
                }

                public DateTime FromDate
                {
                    get { return _FromDate; }
                    set { _FromDate = value; }
                }

                public DateTime ToDate
                {
                    get { return _ToDate; }
                    set { _ToDate = value; }
                }
                #endregion
            }
        }
    }
}
