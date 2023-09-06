using System;
using System.Data;
using System.Web;



namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLayer.BusinessEntities
        {
            public class CombinedCashBankBook
            {
                #region Private Members
                private double  _amount = 0.0;
                private double _opBal = 0.0;
                
                private double _clBal = 0.0;
                private double _opBalb = 0.0;
                private double _clBalb = 0.0;
                private string _opBal_no = string.Empty;
                private string _parti = string.Empty;
                private string _receipt = string.Empty;
                private DateTime _tr_date= DateTime.MinValue;
                private double _trb = 0.0;
                private double _tr = 0.0;
                private string  _bank_nm = string .Empty ;

                
                #endregion

                #region Public
                public double Amount
                {
                    get { return _amount; }
                    set { _amount = value; }
                }
                public double OpBal
                {
                    get { return _opBal; }
                    set { _opBal = value; }
                }
                
                public double ClBal
                {
                    get { return _clBal; }
                    set { _clBal = value; }
                }
                public double OpBalb
                {
                    get { return _opBalb; }
                    set { _opBalb = value; }
                }
                public double ClBalb
                {
                    get { return _clBalb; }
                    set { _clBalb = value; }
                }
                public string OpBalNo
                {
                    get { return _opBal_no; }
                    set { _opBal_no = value; }
                }
                public string Particular
                {
                    get { return _parti; }
                    set { _parti = value; }
                }
                public string Receipt
                {
                    get { return _receipt; }
                    set { _receipt = value; }
                }

                public DateTime  Tr_Date
                {
                    get { return _tr_date; }
                    set { _tr_date = value; }
                }

                public double Trb
                {
                    get { return _trb; }
                    set { _trb = value; }
                }
                public double Tr
                {
                    get { return _tr; }
                    set { _tr = value; }
                }
                public string BankNm
                {
                    get { return _bank_nm; }
                    set { _bank_nm = value; }
                }
                
                #endregion
            }

        }//END: BusinessLayer.BusinessEntities

    }//END: UAIMS  

}//END: IITMS