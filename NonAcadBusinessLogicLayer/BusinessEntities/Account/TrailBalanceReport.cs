using System;
using System.Data;
using System.Web;



namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLayer.BusinessEntities
        {
            public class TrialBalanceReport
            {
                #region Private Members
                private string _PartyName = string.Empty;
                private int _MGRPNO = 0;
                private int _PRNO = 0;
                private int _PARTYNO = 0;
                private double _OPBALANCE = 0;
                private double _CLBALANCE = 0;
                private int _ISPARTY = 0;
                private int _FANO = 0;
                private double _DEBIT = 0;
                private double _CREDIT = 0;
                private int _LEDGERINDEX = 0;
                private int _POSITION = 0;

                
                #endregion

                #region Public
                public string PartyName
                {
                    get { return _PartyName; }
                    set { _PartyName = value; }
                }
                public int MGRPNO
                {
                    get { return _MGRPNO; }
                    set { _MGRPNO = value; }
                }
                public int PRNO
                {
                    get { return _PRNO; }
                    set { _PRNO = value; }
                }
                public int PARTYNO
                {
                    get { return _PARTYNO; }
                    set { _PARTYNO = value; }
                }
                public double OPBALANCE
                {
                    get { return _OPBALANCE; }
                    set { _OPBALANCE = value; }
                }
                public double CLBALANCE
                {
                    get { return _CLBALANCE; }
                    set { _CLBALANCE = value; }
                }
                public int ISPARTY
                {
                    get { return _ISPARTY; }
                    set { _ISPARTY = value; }
                }
                public int FANO
                {
                    get { return _FANO; }
                    set { _FANO = value; }
                }
                public double DEBIT
                {
                    get { return _DEBIT; }
                    set { _DEBIT = value; }
                }
                public double CREDIT
                {
                    get { return _CREDIT; }
                    set { _CREDIT = value; }
                }
                public int LEDGERINDEX
                {
                    get { return _LEDGERINDEX; }
                    set { _LEDGERINDEX = value; }
                }
                #endregion
                public int POSITION
                {
                    get { return _POSITION; }
                    set { _POSITION = value; }
                }
            }

        }//END: BusinessLayer.BusinessEntities

    }//END: UAIMS  

}//END: IITMS