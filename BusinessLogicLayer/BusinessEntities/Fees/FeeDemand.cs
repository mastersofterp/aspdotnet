//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : FEE DEMAND CLASS
// CREATION DATE : 05-JUNE-2009
// CREATED BY    : AMIT YADAV
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class FeeDemand
    {
        #region Private Fields
        int _demandId = 0;
        int _studentId = 0;        
        string _studentName = string.Empty;
        string _enrollmentNo = string.Empty;
        int _branchNo = 0;
        int _sessionNo = 0;
        int _degreeNo = 0;
        int _semesterNo = 0;
        int _admBatchNo = 0;
        string _receiptTypeCode = string.Empty;
        int _paymentTypeNo = 0;
        FeeHeadAmounts _feeHeads = new FeeHeadAmounts();
        double _totalFeeAmount = 0.00;
        int _counterNo = 0;
        int _feeCatNo = 0;
        DateTime _demandDate = DateTime.MinValue;
        bool _isCancelled = false;
        bool _isDeleted = false;
        int _userNo = 0;
        string _remark = string.Empty;
        string _collegeCode = string.Empty;
        int _examType = 0;
        double _excessAmout = 0.00;
        int _uano = 0;
        string _ipaddress = string.Empty;
        int _college_id = 0;
        double _Backlogfees = 0.00;
        double _Regularfees = 0.00;

        #endregion

        #region Public Properties

        public int UANO
        {
            get { return _uano; }
            set { _uano = value; }
        }
        public String IpAddress
        {
            get { return _ipaddress; }
            set { _ipaddress = value; }
        }

        public int DemandId
        {
            get { return _demandId; }
            set { _demandId = value; }
        }

        public int StudentId
        {
            get { return _studentId; }
            set { _studentId = value; }
        }

        public string StudentName
        {
            get { return _studentName; }
            set { _studentName = value; }
        }

        public string EnrollmentNo
        {
            get { return _enrollmentNo; }
            set { _enrollmentNo = value; }
        }

        public int BranchNo
        {
            get { return _branchNo; }
            set { _branchNo = value; }
        }

        public int SessionNo
        {
            get { return _sessionNo; }
            set { _sessionNo = value; }
        }

        public int DegreeNo
        {
            get { return _degreeNo; }
            set { _degreeNo = value; }
        }

        public int SemesterNo
        {
            get { return _semesterNo; }
            set { _semesterNo = value; }
        }

        public int AdmBatchNo
        {
            get { return _admBatchNo; }
            set { _admBatchNo = value; }
        }

        public string ReceiptTypeCode
        {
            get { return _receiptTypeCode; }
            set { _receiptTypeCode = value; }
        }

        public int PaymentTypeNo
        {
            get { return _paymentTypeNo; }
            set { _paymentTypeNo = value; }
        }

        public FeeHeadAmounts FeeHeads
        {
            get { return _feeHeads; }
            set { _feeHeads = value; }
        }

        public double TotalFeeAmount
        {
            get { return _totalFeeAmount; }
            set { _totalFeeAmount = value; }
        }

        public int CounterNo
        {
            get { return _counterNo; }
            set { _counterNo = value; }
        }

        public int FeeCatNo
        {
            get { return _feeCatNo; }
            set { _feeCatNo = value; }
        }

        public DateTime DemandDate
        {
            get { return _demandDate; }
            set { _demandDate = value; }
        }

        public bool IsCancelled
        {
            get { return _isCancelled; }
            set { _isCancelled = value; }
        }

        public bool IsDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; }
        }

        public int UserNo
        {
            get { return _userNo; }
            set { _userNo = value; }
        }

        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        public string CollegeCode
        {
            get { return _collegeCode; }
            set { _collegeCode = value; }
        }
        public int ExamType
        {
            get { return _examType; }
            set { _examType = value; }
        }
        public double ExcessAmount
        {
            get { return _excessAmout; }
            set { _excessAmout = value; }
        }
        public int College_ID
        {
            get { return _college_id; }
            set { _college_id = value; }
        }

        public double BacklogFees
        {
            get { return _Backlogfees; }
            set { _Backlogfees = value; }
        }
        public double RegularFees
        {
            get { return _Regularfees; }
            set { _Regularfees = value; }
        }
        #endregion
    }
}