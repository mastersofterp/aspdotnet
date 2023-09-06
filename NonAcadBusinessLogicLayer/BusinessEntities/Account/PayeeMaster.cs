using System;
using System.Data;
using System.Web;



namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLayer.BusinessEntities
        {
            public class PayeeMasterClass
            {
                #region Private Members
                private int _IDNO = 0;
                private string _PARTYNAME = string.Empty;
                private string _ADDRESS = string.Empty;
                private string _ACCNO = string.Empty;
                private int _CAN = 0;
                private string _IFSC = string.Empty;
                private string _BRANCH = string.Empty;
                private int _PARTY_NO = 0;
                private int _BANK_NO = 0;
                private int _NATURE_ID = 0;
                private string _PAN_NO = string.Empty;
                private string _EMAIL_ID = string.Empty;
                private string _CONTACT_NO = string.Empty;
                #endregion

                #region Public

                public string IFSC
                {
                    get { return _IFSC; }
                    set { _IFSC = value; }
                }

                public string BRANCH
                {
                    get { return _BRANCH; }
                    set { _BRANCH = value; }
                }

                public int PARTY_NO
                {
                    get { return _PARTY_NO; }
                    set { _PARTY_NO = value; }
                }

                public int BANK_NO
                {
                    get { return _BANK_NO; }
                    set { _BANK_NO = value; }
                }

                public int IDNO
                {
                    get { return _IDNO; }
                    set { _IDNO = value; }
                }
                public string PARTYNAME
                {
                    get { return _PARTYNAME; }
                    set { _PARTYNAME = value; }
                }
                public string ADDRESS
                {
                    get { return _ADDRESS; }
                    set { _ADDRESS = value; }
                }
                public string ACCNO
                {
                    get { return _ACCNO; }
                    set { _ACCNO = value; }
                }
                public int CAN
                {
                    get { return _CAN; }
                    set { _CAN = value; }
                }
                public int NATURE_ID
                {
                    get { return _NATURE_ID; }
                    set { _NATURE_ID = value; }
                }
                public string PAN_NUMBER
                {
                    get { return _PAN_NO; }
                    set { _PAN_NO = value; }
                }

                public string EMAIL_ID
                {
                    get { return _EMAIL_ID; }
                    set { _EMAIL_ID = value; }
                }

                public string CONTACT_NO
                {
                    get { return _CONTACT_NO; }
                    set { _CONTACT_NO = value; }
                }

                #endregion
            }

        }//END: BusinessLayer.BusinessEntities

    }//END: UAIMS  

}//END: IITMS