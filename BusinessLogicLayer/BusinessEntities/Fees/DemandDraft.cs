/// ======================================================================================
/// PROJECT NAME  : UAIMS
/// MODULE NAME   : ACADEMIC
/// PAGE NAME     : DAILY COLLECTION REGISTER
/// CREATION DATE : 15-MAY-2009
/// CREATED BY    : AMIT YADAV
/// MODIFIED DATE :
/// MODIFIED DESC :
/// ======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class DemandDrafts
    {
        #region Private Fileds
        int _DDTransactionId = 0;
        int dailyCollectionRegisterId = 0;
        int studentId = 0;
        string _DDNo = string.Empty;
        DateTime _DateOfDD = DateTime.Today;
        string _DDCity = string.Empty;
        int _bankNo = 0;
        string _DDBank = string.Empty;
        double _DDAmount = 0.00;
        string collegeCode = string.Empty;
        #endregion

        #region Public Properties

        public int DemandDraftTransactionId
        {
            get { return _DDTransactionId; }
            set { _DDTransactionId = value; }
        }

        public int DailyCollectionRegisterId
        {
            get { return dailyCollectionRegisterId; }
            set { dailyCollectionRegisterId = value; }
        }

        public int StudentId
        {
            get { return studentId; }
            set { studentId = value; }
        }

        public string DemandDraftNo
        {
            get { return _DDNo; }
            set { _DDNo = value; }
        }

        public DateTime DemandDraftDate
        {
            get { return _DateOfDD; }
            set { _DateOfDD = value; }
        }

        public string DemandDraftCity
        {
            get { return _DDCity; }
            set { _DDCity = value; }
        }

        public int BankNo
        {
            get { return _bankNo; }
            set { _bankNo = value; }
        }

        public string DemandDraftBank
        {
            get { return _DDBank; }
            set { _DDBank = value; }
        }

        public double DemandDraftAmount
        {
            get { return _DDAmount; }
            set { _DDAmount = value; }
        }

        public string CollegeCode
        {
            get { return collegeCode; }
            set { collegeCode = value; }
        }
        #endregion
    }
}