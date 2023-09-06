//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                        
// PAGE NAME     : DAILY COLLECTION REGISTER 
// CREATION DATE : 15-MAY-2009                                                        
// CREATED BY    : AMIT YADAV
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Collections.Generic;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class DailyCollectionRegister
    {
        #region Private Fields

        int dcrNo = 0;
        int studentId = 0;
        string enrollmentNo = string.Empty;
        string studentName = string.Empty;
        int degreeNo = 0;
        int branchNo = 0;
        string branchName = string.Empty;
        int yearNo = 0;
        int semesterNo = 0;
        int sessionno = 0;
        int counterNo = 0;
        int examtype = 0;
        int currency = 0;
        string receiptNo = string.Empty;
        DateTime receiptDate = DateTime.MinValue;
        string paymentModeCode = string.Empty; // C -> Cash, B -> Bank, A -> ATM, S -> Scholarship.
        string paymentType = string.Empty; // Mode of payment Cash(C) OR Demand Draft(D).
        string receiptTypeCode = string.Empty; // receiptType_code from receipt_type tbl.
        int feeCatNo = 0; // Fee_Cat_No of standard fee definition

        FeeHeadAmounts feeHeadAmt = new FeeHeadAmounts(); // fee items
        string remark = string.Empty; // any comment

        DemandDrafts[] _paidDemandDrafts = null; // multiple dd details        
        double demandDraftAmount = 0.00;
        double cashAmount = 0.00;
        double totalAmount = 0.00;

        DateTime challanDate = DateTime.MinValue;
        DateTime printDate = DateTime.MinValue;

        bool isCancelled = true;
        bool isReconciled = false;
        bool isDeleted = false;

        string companyCode = string.Empty; // A/C related
        string rpEntry = string.Empty; // A/C related

        int userNo = 0;
        string collegeCode = string.Empty;

        //add 10/04/2012
        double excessAmount = 0.00;
        int excessStatus = 0;

        double late_fee = 0.00;//****************************

        //add by Rita....
        string creaditdebitno = string.Empty;
        string transreffno = string.Empty;

        //add by Rita---on date 13062019.....
        int ispaytm = 0;

        int banid = 0;

        int installmentFlag = 0;
        int installmentNo = 0;


        #endregion

        #region Public Properties
        public int DcrNo
        {
            get { return dcrNo; }
            set { dcrNo = value; }
        }

        public int StudentId
        {
            get { return studentId; }
            set { studentId = value; }
        }

        public int SessionNo
        {
            get { return sessionno; }
            set { sessionno = value; }
        }


        public string EnrollmentNo
        {
            get { return enrollmentNo; }
            set { enrollmentNo = value; }
        }

        public string StudentName
        {
            get { return studentName; }
            set { studentName = value; }
        }

        public int DegreeNo
        {
            get { return degreeNo; }
            set { degreeNo = value; }
        }

        public int BranchNo
        {
            get { return branchNo; }
            set { branchNo = value; }
        }

        public string BranchName
        {
            get { return branchName; }
            set { branchName = value; }
        }

        public int YearNo
        {
            get { return yearNo; }
            set { yearNo = value; }
        }

        public int SemesterNo
        {
            get { return semesterNo; }
            set { semesterNo = value; }
        }       

        public int CounterNo
        {
            get { return counterNo; }
            set { counterNo = value; }
        }

        public string ReceiptNo
        {
            get { return receiptNo; }
            set { receiptNo = value; }
        }

        public DateTime ReceiptDate
        {
            get { return receiptDate; }
            set { receiptDate = value; }
        }

        public string PaymentModeCode
        {
            get { return paymentModeCode; }
            set { paymentModeCode = value; }
        }

        public string ReceiptTypeCode
        {
            get { return receiptTypeCode; }
            set { receiptTypeCode = value; }
        }

        public int FeeCatNo
        {
            get { return feeCatNo; }
            set { feeCatNo = value; }
        }

        public string PaymentType
        {
            get { return paymentType; }
            set { paymentType = value; }
        }

        public FeeHeadAmounts FeeHeadAmounts
        {
            get { return feeHeadAmt; }
            set { feeHeadAmt = value; }
        }

        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        public DemandDrafts[] PaidDemandDrafts
        {
            get { return _paidDemandDrafts; }
            set { _paidDemandDrafts = value; }
        }

        public double DemandDraftAmount
        {
            get { return demandDraftAmount; }
            set { demandDraftAmount = value; }
        }

        public double CashAmount
        {
            get { return cashAmount; }
            set { cashAmount = value; }
        }

        public double TotalAmount
        {
            get { return totalAmount; }
            set { totalAmount = value; }
        }

        public DateTime ChallanDate
        {
            get { return challanDate; }
            set { challanDate = value; }
        }

        public DateTime PrintDate
        {
            get { return printDate; }
            set { printDate = value; }
        }

        public bool IsCancelled
        {
            get { return isCancelled; }
            set { isCancelled = value; }
        }

        public bool IsReconciled
        {
            get { return isReconciled; }
            set { isReconciled = value; }
        }

        public bool IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }

        public string CompanyCode
        {
            get { return companyCode; }
            set { companyCode = value; }
        }

        public string RpEntry
        {
            get { return rpEntry; }
            set { rpEntry = value; }
        }

        public int UserNo
        {
            get { return userNo; }
            set { userNo = value; }
        }

        public int ExamType
        {
            get { return examtype; }
            set { examtype = value; }
        }

        public string CollegeCode
        {
            get { return collegeCode; }
            set { collegeCode = value; }
        }

        public double ExcessAmount
        {
            get { return excessAmount; }
            set { excessAmount = value; }
        }
        public int ExcessStatus
        {
            get { return excessStatus; }
            set { excessStatus = value; }
        }
        public int Currency
        {
            get { return currency; }
            set { currency = value; }
        }
        public double Late_fee
        {
            get { return late_fee; }
            set { late_fee = value; }
        }
        //Add by Rita
        public string CreditDebitNo
        {
            get { return creaditdebitno; }
            set { creaditdebitno = value; }
        }
        public string TransReffNo
        {
            get { return transreffno; }
            set { transreffno = value; }
        }
        //add by rita 
        public int IsPaytm
        {
            get { return ispaytm; }
            set { ispaytm = value; }
        }

        public int BankId
        {
            get { return banid; }
            set { banid = value; }
        }

        public int InstallmentFlag
        {
            get { return installmentFlag; }
            set { installmentFlag = value; }
        }

        public int InstallmentNo
        {
            get { return installmentNo; }
            set { installmentNo = value; }
        }
        #endregion
    }
}