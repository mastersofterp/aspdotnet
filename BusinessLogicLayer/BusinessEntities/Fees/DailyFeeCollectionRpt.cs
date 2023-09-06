//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                        
// PAGE NAME     : DAILY FEE COLLECTION REPORT
// CREATION DATE : 11-JUN-2009                                                        
// CREATED BY    : AMIT YADAV
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class DailyFeeCollectionRpt
    {
        #region Private Variables
        DateTime _fromDate = DateTime.MinValue;
        DateTime _toDate = DateTime.MinValue;
        string _fdate = string.Empty; //sunita

        int _counterNo = 0;
        string _paymentMode = string.Empty;
        string _receiptTypes = string.Empty;
        int _degreeNo = 0;
        int _branchNo = 0;
        int _yearNo = 0;
        int _semesterNo = 0;
        int _paytypeno = 0;
        string _showbalanceno = string.Empty; 
        bool _paidAmount = false;
        string _YearNos = string.Empty;


        //Added By Rishabh - 02062022
        string _CollegeIds = string.Empty;
        string _DegreeNos = string.Empty;
        string _BranchNos = string.Empty;
        string _SemesterNos = string.Empty;

        #endregion

        #region Public Properties
        //Sunita
        public string FDate
        {
            get { return _fdate; }
            set { _fdate = value; }
        }
        //end
        public DateTime FromDate
        {
            get { return _fromDate; }
            set { _fromDate = value; }
        }

        public DateTime ToDate
        {
            get { return _toDate; }
            set { _toDate = value; }
        }

        public int CounterNo
        {
            get { return _counterNo; }
            set { _counterNo = value; }
        }

        public string PaymentMode
        {
            get { return _paymentMode; }
            set { _paymentMode = value; }
        }

        public string ReceiptTypes
        {
            get { return _receiptTypes; }
            set { _receiptTypes = value; }
        }

        public int DegreeNo
        {
            get { return _degreeNo; }
            set { _degreeNo = value; }
        }

        public int BranchNo
        {
            get { return _branchNo; }
            set { _branchNo = value; }
        }

        public int YearNo
        {
            get { return _yearNo; }
            set { _yearNo = value; }
        }

        public int SemesterNo
        {
            get { return _semesterNo; }
            set { _semesterNo = value; }
        }

        public int PayTypeNo
        {
            get { return _paytypeno; }
            set { _paytypeno = value; }
        }
        public string ShowBalance
        {
            get { return _showbalanceno; }
            set { _showbalanceno = value; }
        }
        public bool PaidAmount
        {
            get { return _paidAmount; }
            set { _paidAmount = value; }
        }

        public string YearNos
        {
            get { return _YearNos; }
            set { _YearNos = value; }
        }

        public string CollegeNos
        {
            get { return _CollegeIds; }
            set { _CollegeIds = value; }
        }

        public string DegreeNos
        {
            get { return _DegreeNos; }
            set { _DegreeNos = value; }
        }

        public string BranchNos
        {
            get { return _BranchNos; }
            set { _BranchNos = value; }
        }

        public string SemesterNos
        {
            get { return _SemesterNos; }
            set { _SemesterNos = value; }
        }
        #endregion
    }
}