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
            public class budgetHead
            {
                #region Private Members

                private int _BUDG_NO = 0;
                private string _BUDG_CODE = string.Empty;
                private string _BUDG_NAME = string.Empty;
                private int _BUDG_PRNO = 0;
                private int _BUDG_LNO = 0;
                private string _COLLEGE_CODE = string.Empty;

                #endregion

                #region Public

                public int BUDG_NO
                {
                    get { return _BUDG_NO; }
                    set { _BUDG_NO = value; }
                }

                public string BUDG_CODE
                {
                    get { return _BUDG_CODE; }
                    set { _BUDG_CODE = value; }
                }

                public string BUDG_NAME
                {
                    get { return _BUDG_NAME; }
                    set { _BUDG_NAME = value; }
                }

                public int BUDG_PRNO
                {
                    get { return _BUDG_PRNO; }
                    set { _BUDG_PRNO = value; }
                }

                public int BUDG_LNO
                {
                    get { return _BUDG_LNO; }
                    set { _BUDG_LNO = value; }
                }

                public string COLLEGE_CODE
                {
                    get { return _COLLEGE_CODE; }
                    set { _COLLEGE_CODE = value; }
                }
                #endregion
            }
        }
    }
}