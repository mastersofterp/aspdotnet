using System;
using System.Data;
using System.Web;



namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLayer.BusinessEntities
        {
            public class Trans
            {
                #region Private Members
                private int _transaction_no = 0;
                private int _subtr_no = 0;
                private DateTime _transaction_date = DateTime.MinValue;
                private string _transaction_type = string.Empty;
                private string _tran = string.Empty;
                private int _party_no = 0;

                private string _college_code = string.Empty;
                #endregion


                #region Public 
                public int Transaction_No
                {
                    get { return _transaction_no; }
                    set { _transaction_no = value; }
                }
                public int Subtr_No
                {
                    get { return _subtr_no; }
                    set { _subtr_no = value; }
                }
                public DateTime Transaction_Date
                {
                    get { return _transaction_date; }
                    set { _transaction_date = value; }
                }
                public string Transaction_Type
                {
                    get { return _transaction_type; }
                    set { _transaction_type = value; }
                }
                public string Tran
                {
                    get { return _tran; }
                    set { _tran = value; }
                }
                public int Party_No
                {
                    get { return _party_no; }
                    set { _party_no = value; }
                }
                public string College_Code
                {
                    get { return _college_code; }
                    set { _college_code = value; }
                }
                #endregion
            }

        }//END: BusinessLayer.BusinessEntities

    }//END: UAIMS  

}//END: IITMS