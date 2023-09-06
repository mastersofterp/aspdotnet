using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace NITPRM
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class AccountMaster
            {
                #region Private Members
                private int _TRNO = 0;
                private int _BNO = 0;
                private string _ACCNO = string.Empty;
                private string _ACCNAME = string.Empty;
                private int _SRNO = 0;
                private int _CFRNO = 0;
                private int _CTONO = 0;
                private int _CCURNO = 0;
                private DateTime _CISSUEDT = DateTime.MinValue;
                private int _STATUS = 0;
                #endregion


                #region Public
                public int TRNO
                {
                    get { return _TRNO; }
                    set { _TRNO = value; }
                }
                public int BNO
                {
                    get { return _BNO; }
                    set { _BNO = value; }
                }
                public string ACCNO
                {
                    get { return _ACCNO; }
                    set { _ACCNO = value; }
                }
                public string ACCNAME
                {
                    get { return _ACCNAME; }
                    set { _ACCNAME = value; }
                }
                public int SRNO
                {
                    get { return _SRNO; }
                    set { _SRNO = value; }
                }
                public int CFRNO
                {
                    get { return _CFRNO; }
                    set { _CFRNO = value; }
                }
                public int CTONO
                {
                    get { return _CTONO; }
                    set { _CTONO = value; }
                }
                public int CCURNO
                {
                    get { return _CCURNO; }
                    set { _CCURNO = value; }
                }
                public DateTime CISSUEDT
                {
                    get { return _CISSUEDT; }
                    set { _CISSUEDT = value; }
                }
                public int STATUS
                {
                    get { return _STATUS; }
                    set { _STATUS = value; }
                }
                #endregion
            }

        }//END: BusinessLayer.BusinessEntities

    }//END: NITPRM  

}//END: IITMS