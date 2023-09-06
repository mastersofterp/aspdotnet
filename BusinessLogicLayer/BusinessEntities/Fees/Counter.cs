using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class Counter
    {
        #region Private Fields

        int _counterNo = 0;
        string _counterName = string.Empty;
        string _printName = string.Empty;
        int _counterUserId = 0;
        string _receiptPermission = string.Empty;
        string _collegeCode = string.Empty;

        #endregion

        #region Public Properties

        public int CounterNo
        {
            get { return _counterNo; }
            set { _counterNo = value; }
        }

        public string CounterName
        {
            get { return _counterName; }
            set { _counterName = value; }
        }

        public string PrintName
        {
            get { return _printName; }
            set { _printName = value; }
        }

        public int CounterUserId
        {
            get { return _counterUserId; }
            set { _counterUserId = value; }
        }

        public string ReceiptPermission
        {
            get { return _receiptPermission; }
            set { _receiptPermission = value; }
        }

        public string CollegeCode
        {
            get { return _collegeCode; }
            set { _collegeCode = value; }
        }

        #endregion
    }
}