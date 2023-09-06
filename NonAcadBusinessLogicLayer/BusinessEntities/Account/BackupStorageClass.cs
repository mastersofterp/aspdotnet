using System;
using System.Data;
using System.Web;



namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLayer.BusinessEntities
        {
            public class BackupStorageClass
            {
                #region Private Members
                private int _TranId = 0;
                private string _Particulars = string.Empty;
                private double _Balance = 0;
                private string _Narration = string.Empty;
                private double _Amount = 0;
                private double _Debit = 0;
                private double _Credit = 0;
                private string _Mode = string.Empty;
                private int _PartyNo = 0;
                private string _ChequeNo = string.Empty;
                private string _ChequeDate = string.Empty;
                private string _OPartyNo = string.Empty;
                private string _TranMode = string.Empty;
                private string _IpAddress = string.Empty;
                #endregion

                #region Public
                   public int TranId
                   {
                       get { return _TranId; }
                       set { _TranId = value; }
                   }
                   public string Particulars
                   {
                       get { return _Particulars; }
                       set { _Particulars = value; }
                   }
                   public double Balance
                   {
                       get { return _Balance; }
                       set { _Balance = value; }
                   }
                   public string Narration
                   {
                       get { return _Narration; }
                       set { _Narration = value; }
                   }
                   public double Amount
                   {
                       get { return _Amount; }
                       set { _Amount = value; }
                   }
                   public double Debit
                   {
                       get { return _Debit; }
                       set { _Debit = value; }
                   }
                   public double Credit
                   {
                       get { return _Credit; }
                       set { _Credit = value; }
                   }
                   public string Mode
                   {
                       get { return _Mode; }
                       set { _Mode = value; }
                   }
                   public int PartyNo
                   {
                       get { return _PartyNo; }
                       set { _PartyNo = value; }
                   }
                   public string ChequeNo
                   {
                       get { return _ChequeNo; }
                       set { _ChequeNo = value; }
                   }
                   public string ChequeDate
                   {
                       get { return _ChequeDate; }
                       set { _ChequeDate = value; }
                   }
                   public string OPartyNo
                   {
                       get { return _OPartyNo; }
                       set { _OPartyNo = value; }
                   }

                   public string TranMode
                   {
                       get { return _TranMode; }
                       set { _TranMode = value; }
                   }
                   public string IpAddress
                   {
                       get { return _IpAddress; }
                       set { _IpAddress = value; }
                   }
                #endregion
            }

        }//END: BusinessLayer.BusinessEntities

    }//END: UAIMS  

}//END: IITMS