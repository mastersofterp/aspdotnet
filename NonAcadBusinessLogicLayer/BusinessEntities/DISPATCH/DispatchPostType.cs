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
        namespace BusinessLayer.BusinessEntities
        {
            public  class DispatchPostType
            {
              #region Private Members

                private int _posttypeno = 0;            
                private string _posttype = string.Empty;
              

                #endregion

                #region Public Menber

            
                public int PostTypeNo
                {
                    get { return _posttypeno; }
                    set { _posttypeno = value; }
                }
                public string PostType
                {
                    get { return _posttype; }
                    set { _posttype = value; }
                }
                
                #endregion
            }

        }
    }
}

