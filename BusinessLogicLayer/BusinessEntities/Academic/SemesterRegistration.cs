using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class SemesterRegistration
            {
                #region Private Member
                private int _idNo = 0;
                private int _sessionNo = 0;
                private int _semesterno = 0;
                private int _schemeno = 0;
                private int _paymentmode = 0;
                private int _offlinemode = 0;
                private string _bankname = string.Empty;
                private string _branchname = string.Empty;
                private int _DDNo = 0;
                private int _DDnumber = 0;
                private int _checknumber = 0;
                //private int total_amt = 0;
                private Decimal total_amt = 0;
                private string _date_of_issue;
                private int _chequeNo = 0;
                private string _transactionid = string.Empty;
                private string _date_of_payment;
                private string filename_slip = string.Empty;
                // private byte[] filename_slip = null;
                private int _CREATED_BY;
                private string _IPADDRESS;
                #endregion
                #region Public Property Fields
                public int IdNo
                {
                    get { return _idNo; }
                    set { _idNo = value; }
                }
                public int SESSIONNO
                {
                    get { return _sessionNo; }
                    set { _sessionNo = value; }
                }
                public int SemesterNO
                {
                    get { return _semesterno; }
                    set { _semesterno = value; }
                }

                public int SchemeNo
                {
                    get { return _schemeno; }
                    set { _schemeno = value; }
                }
                public int paymentMode
                {
                    get { return _paymentmode; }
                    set { _paymentmode = value; }
                }

                public int OfflineMode
                {
                    get { return _offlinemode; }
                    set { _offlinemode = value; }
                }

                public string BankName
                {
                    get { return _bankname; }
                    set { _bankname = value; }
                }
                public string BranchName
                {
                    get { return _branchname; }
                    set { _branchname = value; }
                }
                public int DDNo
                {
                    get { return _DDNo; }
                    set { _DDNo = value; }
                }
                public int DDNumber
                {
                    get { return _DDnumber; }
                    set { _DDnumber = value; }
                }
                public int CheckNumber
                {
                    get { return _checknumber; }
                    set { _checknumber = value; }
                }
                //public int Total_Amt
                //{
                //    get { return total_amt; }
                //    set { total_amt = value; }
                //}
                public Decimal Total_Amt
                {
                    get { return total_amt; }
                    set { total_amt = value; }
                }
                public string Date_of_Issue
                {
                    get { return _date_of_issue; }
                    set { _date_of_issue = value; }
                }
                public int chequeNo
                {
                    get { return _chequeNo; }
                    set { _chequeNo = value; }
                }
                public string Date_of_Payment
                {
                    get { return _date_of_payment; }
                    set { _date_of_payment = value; }
                }
                public string TransactionId
                {
                    get { return _transactionid; }
                    set { _transactionid = value; }
                }
                public string Filename
                {
                    get { return filename_slip; }
                    set { filename_slip = value; }
                }
                //public byte[] Filename
                //{
                //    get { return filename_slip; }
                //    set { filename_slip = value; }
                //}
                public int CREATED_BY
                {
                    get { return _CREATED_BY; }
                    set { _CREATED_BY = value; }
                }
                public string IPADDRESS
                {
                    get
                    {
                        return this._IPADDRESS;
                    }
                    set
                    {
                        if ((this._IPADDRESS != value))
                        {
                            this._IPADDRESS = value;
                        }
                    }
                }
                #endregion
            }
        }
    }
}
