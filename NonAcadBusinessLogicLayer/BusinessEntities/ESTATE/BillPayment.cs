using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using IITMS;
using System.Data;


namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
   public class BillPayment
    {
       #region Private Member
        private int _PID = 0;
        private DateTime _DATE ;
       
        private int _EMPID = 0;     
        private string _RECEIPT_NO = string.Empty;   
        private DateTime _RECEIPT_DATE;
        private double _PAID_AMOUNT;
        private double _BALANCE_AMOUNT;
        private double _TOTAL_AMOUNT;

        private string _BPROCESS_ID;
        private string _ARREAR;
        private string _ARREAR_INTEREST;

        private DataTable _ARREAR_TABLE = null;
       


        #endregion

        #region Public Members 
        public DataTable ARREAR_TABLE
        {
            get { return _ARREAR_TABLE; }
            set { _ARREAR_TABLE = value; }
        }

        public int PID
        {
            get { return _PID; }
            set { _PID = value; }
        }
        public DateTime DATE
        {
            get { return _DATE; }
            set { _DATE = value; }
        }      
       
        public int EMPID
        {
            get { return _EMPID; }
            set { _EMPID = value; }
        }

        public string RECEIPT_NO
        {
            get { return _RECEIPT_NO; }
            set { _RECEIPT_NO = value; }
        }

        public DateTime RECEIPT_DATE
        {
            get { return _RECEIPT_DATE; }
            set { _RECEIPT_DATE = value; }
        }
        public double PAID_AMOUNT
        {
            get { return _PAID_AMOUNT; }
            set { _PAID_AMOUNT = value; }
        }
        public double BALANCE_AMOUNT
        {
            get { return _BALANCE_AMOUNT; }
            set { _BALANCE_AMOUNT = value; }
        }
        public double TOTAL_AMOUNT
        {
            get { return _TOTAL_AMOUNT; }
            set { _TOTAL_AMOUNT = value; }
        }

        public string BPROCESS_ID
        {
            get { return _BPROCESS_ID; }
            set { _BPROCESS_ID = value; }
        }
        public string ARREAR
        {
            get { return _ARREAR; }
            set { _ARREAR = value; }
        }
        public string ARREAR_INTEREST
        {
            get { return _ARREAR_INTEREST; }
            set { _ARREAR_INTEREST = value; }
        }

       
       
       
        #endregion 
    }
}
