using System;
using System.Data;
using System.Web;



namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLayer.BusinessEntities
        {

            public class FeeLedgerHeadMapingClass
            {
                #region Private Members
                private string _reciept_type = string.Empty;
                private int _degreeNo = 0;
                private string _feeheadNo = string.Empty;
                private int _college = 0;
                private int _ledgerNo = 0;
                private int _cashNo = 0;
                private int _bankNo = 0;
                private string _createrName = string.Empty;
                private DateTime _createDate = DateTime.MinValue;
                private string _LastModiFier = string.Empty;
                private DateTime _LastModifierDate = DateTime.MinValue;
                private string _cName = string.Empty;
                private string _fee_ledger_name = string.Empty;
                private string _naration = string.Empty;
                private string _cPass = string.Empty;
                private int _mBank = 0;
                private int _Mfeeled = 0;
                private int _SequenceId = 0;


                private int _BatchNo = 0;
                private int _SemesterNo = 0;
              
                #endregion



                #region Public
                public string RECIEPT_TYPE
                {
                    get { return _reciept_type; }
                    set { _reciept_type = value; }
                }
                public int DegreeNo
                {
                    get { return _degreeNo; }
                    set { _degreeNo = value; }
                }
                public DateTime CREATE_DATE
                {
                    get { return _createDate; }
                    set { _createDate = value; }
                }
                public string FEE_HEAD_NO
                {
                    get { return _feeheadNo; }
                    set { _feeheadNo = value; }
                }
                public int COLLEGE
                {
                    get { return _college; }
                    set { _college = value; }
                }
                public int LEDGER_NO
                {
                    get { return _ledgerNo; }
                    set { _ledgerNo = value; }
                }
                public int CASH_NO
                {
                    get { return _cashNo; }
                    set { _cashNo = value; }
                }
                public int BANK_NO
                {
                    get { return _bankNo; }
                    set { _bankNo = value; }
                }
                public string CREATER_NAME
                {
                    get { return _createrName; }
                    set { _createrName = value; }
                }
                public string LASTMODIFIER
                {
                    get { return _LastModiFier; }
                    set { _LastModiFier = value; }
                }


                public DateTime LASTMODIFIER_DATE
                {
                    get { return _LastModifierDate; }
                    set { _LastModifierDate = value; }
                }

                public string CName
                {
                    get { return _cName; }
                    set { _cName = value; }
                }
                public string CPass
                {
                    get { return _cPass; }
                    set { _cPass = value; }
                }
                public int MBank
                {
                    get { return _mBank; }
                    set { _mBank = value; }
                }
                public int Mfeeled
                {
                    get { return _Mfeeled; }
                    set { _Mfeeled = value; }
                }
                public string FeeLedger_NAme
                {
                    get { return _fee_ledger_name; }
                    set { _fee_ledger_name = value; }
                }
                public string Naration
                {
                    get { return _naration; }
                    set { _naration = value; }
                }
                public int SequenceId
                {
                    get { return _SequenceId; }
                    set { _SequenceId = value; }
                }

                public int BatchNo
                {
                    get { return _BatchNo; }
                    set { _BatchNo = value; }
                }
                public int SemesterNo
                {
                    get { return _SemesterNo; }
                    set { _SemesterNo = value; }
                }

                #endregion




            }
             }//END: BusinessLayer.BusinessEntities

    }//END: UAIMS  

}//END: IITMS

