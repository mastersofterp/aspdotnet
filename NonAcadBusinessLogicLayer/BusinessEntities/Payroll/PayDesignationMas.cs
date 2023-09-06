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
            public class PayDesignationMas
            {

                #region Private Members

                private int _DESIGNO = 0;

                private string _DESIGNATION = string.Empty;

                private int _STAFFNO = 0;

                private string _DESIGSHORT = string.Empty;

                private int _SEQNO = 0;

                private string _COLLEGECODE = string.Empty;

                #endregion
                
                #region Public Members

                public string COLLEGECODE
                {
                    get { return _COLLEGECODE; }
                    set { _COLLEGECODE = value; }
                }

                public int SEQNO
                {
                    get { return _SEQNO; }
                    set { _SEQNO = value; }
                }


                public string DESIGSHORT
                {
                    get { return _DESIGSHORT; }
                    set { _DESIGSHORT = value; }
                }

                public int STAFFNO
                {
                    get { return _STAFFNO; }
                    set { _STAFFNO = value; }
                }

                public string DESIGNATION
                {
                    get { return _DESIGNATION; }
                    set { _DESIGNATION = value; }
                }

                public int DESIGNO
                {
                    get { return _DESIGNO; }
                    set { _DESIGNO = value; }
                }

                

                #endregion
            }
        }
    }
}
