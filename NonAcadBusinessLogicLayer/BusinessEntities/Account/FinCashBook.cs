using System;
using System.Data;
using System.Web;



namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLayer.BusinessEntities
        {
            public class FinCashBook
            {
                #region Private Members
                private int _company_no = 0;
                private string _company_code = string.Empty;
                private string _company_name = string.Empty;
                private DateTime _company_finddate_from = DateTime.MinValue;
                private DateTime _company_finddate_to = DateTime.MinValue;
                private DateTime _bookwritedate = DateTime.MinValue;
                private string _password = string.Empty;
                private string _year = string.Empty;
                
                private string _college_code = string.Empty;
                private int _IsCompanyLock = 0;

                //CASHBOOK REPORT 
                private int _vno = 0;
                private int _tr_no = 0;
                
                private int _subtr_no = 0;
                private int _mgrp_no = 0;
                private int _tentry = 0;
                private double _amt = 0;
                private string _tr_type = string.Empty;
                private string _particular = string.Empty;
                private string _parti_name = string.Empty;
                private DateTime _Date = DateTime.MinValue;
                private string _Parent_Comp_Code = string.Empty;

                
                #endregion


                #region Public 
                public int Company_No
                {
                    get { return _company_no; }
                    set { _company_no = value; }
                }
                public string Company_Code
                {
                    get { return _company_code; }
                    set { _company_code = value; }
                }
                public string Company_Name
                {
                    get { return _company_name; }
                    set { _company_name = value; }
                }
                public DateTime Company_FindDate_From
                {
                    get { return _company_finddate_from; }
                    set { _company_finddate_from = value; }
                }
                public DateTime Company_FindDate_To
                {
                    get { return _company_finddate_to; }
                    set { _company_finddate_to = value; }
                }
                public DateTime BookWriteDate
                {
                    get { return _bookwritedate; }
                    set { _bookwritedate = value; }
                }
                public string Password
                {
                    get { return _password; }
                    set { _password = value; }
                }
                public string Year
                {
                    get { return _year; }
                    set { _year = value; }
                }
                public string College_Code
                {
                    get { return _college_code; }
                    set { _college_code = value; }
                }
                public int IsCompanyLock
                {
                    get { return _IsCompanyLock; }
                    set { _IsCompanyLock = value; }
                }

                //CASHBOOK REPORT
                public int VNo
                {
                    get { return _vno; }
                    set { _vno = value; }
                }
                public int TrNo
                {
                    get { return _tr_no; }
                    set { _tr_no = value; }
                }
                public string  TrType
                {
                    get { return _tr_type; }
                    set { _tr_type = value; }
                }
                public int SubTrNo
                {
                    get { return _subtr_no; }
                    set { _subtr_no = value; }
                }
                public int MgrpNo
                {
                    get { return _mgrp_no; }
                    set { _mgrp_no = value; }
                }
                public int TEntry
                {
                    get { return _tentry ; }
                    set { _tentry = value; }
                }
                public double Amt
                {
                    get { return _amt; }
                    set { _amt = value; }
                }
                public string Particular
                {
                    get { return _particular ; }
                    set { _particular  = value; }
                }
                public string PartyName
                {
                    get { return _parti_name; }
                    set { _parti_name = value; }
                }
                public DateTime Date
                {
                    get { return _Date; }
                    set { _Date = value; }
                }
                public string Parent_Comp_Code
                {
                    get { return _Parent_Comp_Code; }
                    set { _Parent_Comp_Code = value; }
                }
                #endregion
            }

        }//END: BusinessLayer.BusinessEntities

    }//END: UAIMS  

}//END: IITMS