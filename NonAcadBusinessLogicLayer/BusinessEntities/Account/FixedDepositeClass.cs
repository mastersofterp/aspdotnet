using System;
using System.Data;
using System.Web;



namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLayer.BusinessEntities
        {
            public class FixedDepositeClass
            {
                #region Private Members

                private int _FDID = 0;
                private string _PARTY_NO ="0";
                private string _scheme = "";
                private string _fdr_no = "";
                private double _rateOfIntrest = 0.0;
                private string _deposite_date = "";
                private string _maturity_date = "";
                private double _amount = 0.0;
                private int _Comp_No = 0;
                private string _Comp_code = "";
                private string _Serial_No = "0";
                private int _Bank_Id = 0;
                private string _Customer_Id = "";
                private string _Investment_Date = "";
                private double _Invested_Amt = 0.0;
                private double _Maturity_Amt = 0.0;
                private string _PAN_No = "";
                private string _Period_From_Date = "";
                private string _Period_To_Date = "";
                private string _Scheme = "";
                private string _Account_Holder = "";
                private string _Address = "";
                private string _Nomination_For = "";
                private string _Remark = "";
                private int _IsClosed = 0;

                private double _AccumulatedInterest = 0.0;
                private string _BankAddress = "";
                private double _FdWithdrawnAmount = 0.0;
                private string _Fd_Duration = "";
                private string _Reference = "";
                private string _RegisterBookNo = "";
                private string _FdAdviseAttachment = "";

                #endregion

                #region Public

                public string FdAdviseAttachment
                {
                    get { return _FdAdviseAttachment; }
                    set { _FdAdviseAttachment = value; }
                }

                public string Reference
                {
                    get { return _Reference; }
                    set { _Reference = value; }
                }
                public string RegisterBookNo
                {
                    get { return _RegisterBookNo; }
                    set { _RegisterBookNo = value; }
                }

                public string BankAddress
                {
                    get { return _BankAddress; }
                    set { _BankAddress = value; }
                }

                public string Fd_Duration
                {
                    get { return _Fd_Duration; }
                    set { _Fd_Duration = value; }
                }

                public double AccumulatedInterest
                {
                    get { return _AccumulatedInterest; }
                    set { _AccumulatedInterest = value; }
                }
                public double FdWithdrawnAmount
                {
                    get { return _FdWithdrawnAmount; }
                    set { _FdWithdrawnAmount = value; }
                }

                public int FDID
                {
                    get { return _FDID; }
                    set { _FDID = value; }
                }

                public string PARTY_NO
                {
                    get { return _PARTY_NO; }
                    set { _PARTY_NO = value; }
                }

                public string SCHEME
                {
                    get { return _scheme; }
                    set { _scheme = value; }
                }

                public string FDR_NO
                {
                    get { return _fdr_no; }
                    set { _fdr_no = value; }
                }

                public double RateOfIntrest
                {
                    get { return _rateOfIntrest; }
                    set { _rateOfIntrest = value; }
                }

                public string Deposite_Date
                {
                    get { return _deposite_date; }
                    set { _deposite_date = value; }
                }

                public string  Maturity_Date
                {
                    get { return _maturity_date; }
                    set { _maturity_date = value; }
                }


                public double Amount
                {
                    get { return _amount; }
                    set { _amount = value; }
                }

                public int Comp_No
                    {
                        get { return _Comp_No; }
                        set { _Comp_No = value; }
                }
                public string Comp_code
                {
                    get { return _Comp_code; }
                    set { _Comp_code = value; }
                }
                public string Serial_No
                {
                    get { return _Serial_No; }
                    set { _Serial_No = value; }
                }
                public int Bank_Id
                {
                    get { return _Bank_Id; }
                    set { _Bank_Id = value; }
                }
                public string Customer_Id
                {
                    get { return _Customer_Id; }
                    set { _Customer_Id = value; }
                }
                public string Investment_Date
                {
                    get { return _Investment_Date; }
                    set { _Investment_Date = value; }
                }
                public double Invested_Amt
                {
                    get { return _Invested_Amt; }
                    set { _Invested_Amt = value; }
                }
                public double Maturity_Amt
                {
                    get { return _Maturity_Amt; }
                    set { _Maturity_Amt = value; }
                }
                public string PAN_No
                {
                    get { return _PAN_No; }
                    set { _PAN_No = value; }
                }
                public string Period_From_Date
                {
                    get { return _Period_From_Date; }
                    set { _Period_From_Date = value; }
                }
                public string Period_To_Date
                {
                    get { return _Period_To_Date; }
                    set { _Period_To_Date = value; }
                }
                public string Scheme
                {
                    get { return _Scheme; }
                    set { _Scheme = value; }
                }
                public string Account_Holder
                {
                    get { return _Account_Holder; }
                    set { _Account_Holder = value; }
                }
                public string Address
                {
                    get { return _Address; }
                    set { _Address = value; }
                }
                public string Nomination_For
                {
                    get { return _Nomination_For; }
                    set { _Nomination_For = value; }
                }
                public string Remark
                {
                    get { return _Remark; }
                    set { _Remark = value; }
                }

                public int IsClosed
                {
                    get { return _IsClosed; }
                    set { _IsClosed = value; }
                }

                //public DateTime CREATED_MODIFIED_DATE
                //{
                //    get { return _CREATED_MODIFIED_DATE; }
                //    set { _CREATED_MODIFIED_DATE = value; }
                //}
                //public string COMPANY_CODE
                //{
                //    get { return _COMPANY_CODE; }
                //    set { _COMPANY_CODE = value; }
                //}
                //public string P_YEAR
                //{
                //    get { return _P_YEAR; }
                //    set { _P_YEAR = value; }
                //}
                //public int TRANSACTION_NO
                //{
                //    get { return _TRANSACTION_NO; }
                //    set { _TRANSACTION_NO = value; }
                //}
                //public string STR_VOUCHER_NO
                //{
                //    get { return _STR_VOUCHER_NO; }
                //    set { _STR_VOUCHER_NO = value; }
                //}

                #endregion
            }

        }//END: BusinessLayer.BusinessEntities

    }//END: UAIMS  

}//END: IITMS

