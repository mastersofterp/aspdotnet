using System;
using System.Data;
using System.Web;



namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLayer.BusinessEntities
        {
            public class Party
            {
                #region Private Members
                private bool _isBudgetHead = true;
                private int _party_no = 0;
                private string _party_name = string.Empty;
                private string _party_address = string.Empty;
                private int _credit = 0;
                private int _debit = 0;
                private int _payment_type_no = 0;
                private double _balance = 0.0f;
                private string _status = string.Empty;
                private string _remark = string.Empty;
                private double _opbalance = 0.0f;
                private int _mgrp_no = 0;
                private int _rp_no = 0;
                private int _pgrp_no = 0;
                private string _page_no = string.Empty;
                private string _acc_code = string.Empty;
                private bool _freeze = false;
                private bool _stopob = false;
                private string _college_code = string.Empty;
                private string _Party_Contact = string.Empty;
                private DateTime _transaction_date = DateTime.MinValue;
                private string _Bank_Account_No = string.Empty;
                private int _SetDefault = 0;
                private string _TINNO = string.Empty;
                private string _PANNO = string.Empty;
                private string _Work_Nature = string.Empty;

                #endregion

                #region Public
                public bool IsBudgetHead
                {
                    get { return _isBudgetHead; }
                    set { _isBudgetHead = value; }
                }
                public int Party_No
                {
                    get { return _party_no; }
                    set { _party_no = value; }
                }
                public string Party_Name
                {
                    get { return _party_name; }
                    set { _party_name = value; }
                }
                public string Party_Address
                {
                    get { return _party_address; }
                    set { _party_address = value; }
                }
                public int Credit
                {
                    get { return _credit; }
                    set { _credit = value; }
                }
                public int Debit
                {
                    get { return _debit; }
                    set { _debit = value; }
                }
                public int Payment_Type_No
                {
                    get { return _payment_type_no; }
                    set { _payment_type_no = value; }
                }
                public double Balance
                {
                    get { return _balance; }
                    set { _balance = value; }
                }
                public string Status
                {
                    get { return _status; }
                    set { _status = value; }
                }
                public string Remark
                {
                    get { return _remark; }
                    set { _remark = value; }
                }
                public double OpeningBalance
                {
                    get { return _opbalance; }
                    set { _opbalance = value; }
                }
                public int Mgrp_No
                {
                    get { return _mgrp_no; }
                    set { _mgrp_no = value; }
                }
                public int RP_No
                {
                    get { return _rp_no; }
                    set { _rp_no = value; }
                }
                public int PGrp_No
                {
                    get { return _pgrp_no; }
                    set { _pgrp_no = value; }
                }
                public string PageNo
                {
                    get { return _page_no; }
                    set { _page_no = value; }
                }
                public bool Freeze
                {
                    get { return _freeze; }
                    set { _freeze = value; }
                }
                public bool StopOB
                {
                    get { return _stopob; }
                    set { _stopob = value; }
                }
                public string Account_Code
                {
                    get { return _acc_code; }
                    set { _acc_code = value; }
                }
                public string College_Code
                {
                    get { return _college_code; }
                    set { _college_code = value; }
                }
                public string Party_Contact
                {
                    get { return _Party_Contact; }
                    set { _Party_Contact = value; }
                }
                public DateTime Transaction_date
                {
                    get { return _transaction_date; }
                    set { _transaction_date = value; }
                }
                public string Bank_Account_No
                {
                    get { return _Bank_Account_No; }
                    set { _Bank_Account_No = value; }
                }
                public int SetDefault
                {
                    get { return _SetDefault; }
                    set { _SetDefault = value; }
                }
                public string PANNO
                {
                    get { return _PANNO; }
                    set { _PANNO = value; }
                }
                public string TINNO
                {
                    get { return _TINNO; }
                    set { _TINNO = value; }
                }
                public string Work_Nature
                {
                    get { return _Work_Nature; }
                    set { _Work_Nature = value; }
                }
                #endregion
            }

        }//END: BusinessLayer.BusinessEntities

    }//END: UAIMS  

}//END: IITMS