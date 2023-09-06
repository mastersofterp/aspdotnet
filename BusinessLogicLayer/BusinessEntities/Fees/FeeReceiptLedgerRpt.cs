//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                        
// PAGE NAME     : FEE RECEIPT LEDGER REPORT
// CREATION DATE : 24-JUN-2009                                                        
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
    public class FeeReceiptLedgerRpt
    {
        #region Private Variables
        bool _filterByReceipt = true;       
        bool _filterBychallan = false;
        DateTime _fromDate = DateTime.MinValue;
        DateTime _toDate = DateTime.MinValue;
        string _paymentMode = string.Empty;
        string _receiptTypes = string.Empty;
        int _degreeNo = 0;
        int _branchNo = 0;
        int _yearNo = 0;
        int _semesterNo = 0;
        #endregion

        #region Public Properties

        public bool FilterByReceipt
        {
            get { return _filterByReceipt; }
            set { _filterByReceipt = value; }
        }

        public bool FilterBychallan
        {
            get { return _filterBychallan; }
            set { _filterBychallan = value; }
        }

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
        #endregion
    }
}