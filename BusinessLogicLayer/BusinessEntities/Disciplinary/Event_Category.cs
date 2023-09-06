

using System;
using System.Collections;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
            public class Event_Category
            {
                #region private
                private int _ECATID = 0;
                private string _ECATNAME = string.Empty;
                private string  _CCODE = string.Empty;
                
                #endregion

                #region public
                public int ECATID
                {
                    get { return _ECATID; }
                    set { _ECATID = value; }
                }
                public string ECATNAME
                {
                    get { return _ECATNAME; }
                    set { _ECATNAME = value; }
                }

                public string CCODE
                {
                    get { return _CCODE; }
                    set { _CCODE = value; }
                }
              
                #endregion
            }
        }
    
