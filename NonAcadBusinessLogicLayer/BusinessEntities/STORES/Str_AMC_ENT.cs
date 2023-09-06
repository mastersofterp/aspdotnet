using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;

namespace IITMS.UAIMS.BusinessLogicLayer.BusinessEntities
{
    public class Str_AMC_ENT
    {
        #region AMC PROPOSAL

        #region Private Methods
        private int _AMCNO = 0; 
        private string _REF_NUMBER = string.Empty;
        private int _BHNO = 0;
        private int _MDNO = 0;
        private int _SUBDEPTNO = 0;
        private DateTime _DATE;
        private DateTime _AMC_FROM_DATE;
        private DateTime _AMC_TO_DATE;
        private int _AMC_CATEGORY = 0;
        private string _JUSTIFICATION = string.Empty;
        private double _AMC_COST = 0.0;
        private string _A3_CODE = string.Empty;
        private string _ITEM_DESCRIPTION = string.Empty;
        private double _AMC_WITH_GST = 0.0;
        private double _AMC_WITHOUT_GST = 0.0;
        private string _BUDGET_PROVISION = string.Empty;
        private string _AMC_DETAILS = string.Empty;
        private int _VENDOR = 0;
        private int _ITEM_NO = 0;
        private int _SRNO = 0;
        private int _AMC_STATUS = 0;
        private int _AMC_PAYMENT_MODE = 0;
        private double _PAID_AMOUNT = 0.0;
        private DateTime _AMC_DATE_OF_PAYMENT ;
        private int _TRANSACTION_ID = 0;
       
        #endregion


        #region Public Method
        public int TRANSACTION_ID
        {
            get { return _TRANSACTION_ID; }
            set { _TRANSACTION_ID = value; }
        }

        public int AMC_PAYMENT_MODE
        {
            get { return _AMC_PAYMENT_MODE; }
            set { _AMC_PAYMENT_MODE = value; }
        }

        public double PAID_AMOUNT
        {
            get { return _PAID_AMOUNT; }
            set { _PAID_AMOUNT = value; }
        }

        public DateTime AMC_DATE_OF_PAYMENT
        {
            get { return _AMC_DATE_OF_PAYMENT; }
            set { _AMC_DATE_OF_PAYMENT = value; }
        }

        public string REF_NUMBER
        {
            get { return _REF_NUMBER; }
            set { _REF_NUMBER = value; }
        }
        public string AMC_DETAILS
        {
            get { return _AMC_DETAILS; }
            set { _AMC_DETAILS = value; }
        }
        public string ITEM_DESCRIPTION
        {
            get { return _ITEM_DESCRIPTION; }
            set { _ITEM_DESCRIPTION = value; }
        }
        public string A3_CODE
        {
            get { return _A3_CODE; }
            set { _A3_CODE = value; }
        }
        public string JUSTIFICATION
        {
            get { return _JUSTIFICATION; }
            set { _JUSTIFICATION = value; }
        }

        public double AMC_WITHOUT_GST
        {
            get { return _AMC_WITHOUT_GST; }
            set { _AMC_WITHOUT_GST = value; }
        }
        public double AMC_WITH_GST
        {
            get { return _AMC_WITH_GST; }
            set { _AMC_WITH_GST = value; }
        }
        public double AMC_COST
        {
            get { return _AMC_COST; }
            set { _AMC_COST = value; }
        }
        public DateTime DATE
        {
            get { return _DATE; }
            set { _DATE = value; }
        }

        public DateTime AMC_FROM_DATE
        {
            get { return _AMC_FROM_DATE; }
            set { _AMC_FROM_DATE = value; }
        }
        public DateTime AMC_TO_DATE
        {
            get { return _AMC_TO_DATE; }
            set { _AMC_TO_DATE = value; }
        }
        public string  BUDGET_PROVISION
        {
            get { return _BUDGET_PROVISION; }
            set { _BUDGET_PROVISION = value; }
        }
        public int VENDOR
        {
            get { return _VENDOR; }
            set { _VENDOR = value; }
        }
        public int ITEM_NO
        {
            get { return _ITEM_NO; }
            set { _ITEM_NO = value; }
        }
        public int SRNO
        {
            get { return _SRNO; }
            set { _SRNO = value; }
        }
        public int AMC_STATUS
        {
            get { return _AMC_STATUS; }
            set { _AMC_STATUS = value; }
        }
        

        public int AMCNO 
        {
            get { return _AMCNO; }
            set { _AMCNO = value; }
        }
        public int BHNO
        {
            get { return _BHNO; }
            set { _BHNO = value; }
        }
        public int MDNO
        {
            get { return _MDNO; }
            set { _MDNO = value; }
        }
        public int SUBDEPTNO
        {
            get { return _SUBDEPTNO; }
            set { _SUBDEPTNO = value; }
        }
        public int AMC_CATEGORY
        {
            get { return _AMC_CATEGORY; }
            set { _AMC_CATEGORY = value; }
        }


        #endregion

        #endregion

        #region AMC APPROVAL

        #region Private
        private int _NUMBER_OF_QUOTAIONS = 0;
        private string _COMPANY_NAME = string.Empty;
        private string _COMMENTS_ON_COMPARATIVE_STATEMENT = string.Empty;
        private string _PAYMENT_DETAILS = string.Empty;
        private int _APPROVAL = 0;
        private double _AMOUNT_LEFT = 0.0;
        private DataTable _AMC_PROPOSAL;
        private DataTable _BILL_PAYMENT_DT;
        private int _CREATED_BY;
        private int _MODIFIED_BY;
        private int _BILL_PAYMENT_ID;


        #endregion

        #region Public
        public int BILL_PAYMENT_ID
        {
            get { return _BILL_PAYMENT_ID; }
            set { _BILL_PAYMENT_ID = value; }
        }
        public int CREATED_BY {
            get { return _CREATED_BY; } 
            set { _CREATED_BY = value; } 
        }
        public int MODIFIED_BY {
            get { return _MODIFIED_BY; } 
            set { _MODIFIED_BY = value; } 
        }

        public DataTable AMC_PROPOSAL
        {
            get { return _AMC_PROPOSAL; }
            set { _AMC_PROPOSAL = value; }
        }
        public DataTable BILL_PAYMENT_DT
        {
            get { return _BILL_PAYMENT_DT; }
            set { _BILL_PAYMENT_DT = value; }
        }
        public string PAYMENT_DETAILS
        {
            get { return _PAYMENT_DETAILS; }
            set { _PAYMENT_DETAILS = value; }
        }
        public string COMMENTS_ON_COMPARATIVE_STATEMENT
        {
            get { return _COMMENTS_ON_COMPARATIVE_STATEMENT; }
            set { _COMMENTS_ON_COMPARATIVE_STATEMENT = value; }
        }
        public string COMPANY_NAME
        {
            get { return _COMPANY_NAME; }
            set { _COMPANY_NAME = value; }
        }
        public int NUMBER_OF_QUOTAIONS
        {
            get { return _NUMBER_OF_QUOTAIONS; }
            set { _NUMBER_OF_QUOTAIONS = value; }
        }
        public int APPROVAL
        {
            get { return _APPROVAL; }
            set { _APPROVAL = value; }
        }
        public double AMOUNT_LEFT
        {
            get { return _AMOUNT_LEFT; }
            set { _AMOUNT_LEFT = value; }
        }
        #endregion



        #endregion

        #region ASSET TRANSFER ENTRY

        #region Private

        private int _ATE_NO = 0;
        private int _STOCK_REGISTER_NO = 0;
        private string _TRANSFER_NO = string.Empty;
        private DateTime _DATE_OF_TRANSFER;
        //  private int _ITEM_NO = 0;
        private int _HANDED_OVER_BY_NAME = 0;
        private int _TRANSFER_CATEGORY = 0;
        private int _TRANSFERRING_DEPARTMENT_NAME = 0;
        private int _TAKEN_OVER_BY_NAME_HOD = 0;
        private int _RECEIVERS_DEPARTMENT = 0;
        private string _REMARKS = string.Empty;
        private int _APPROVING_AUTHORITY = 0;
        private DataTable _ASSETTRANSFERTABLE;

        #endregion


        #region Public

        public DataTable ASSETTRANSFERTABLE
        {
            get { return _ASSETTRANSFERTABLE; }
            set { _ASSETTRANSFERTABLE = value; }
        }

        public int ATE_NO
        {
            get { return _ATE_NO; }
            set { _ATE_NO = value; }
        }
        public int STOCK_REGISTER_NO
        {
            get { return _STOCK_REGISTER_NO; }
            set { _STOCK_REGISTER_NO = value; }
        }
        public string TRANSFER_NO
        {
            get { return _TRANSFER_NO; }
            set { _TRANSFER_NO = value; }
        }
        //public int ITEM_NO
        //{
        //    get { return _ITEM_NO; }
        //    set { _ITEM_NO = value; }
        //}
        public int HANDED_OVER_BY_NAME
        {
            get { return _HANDED_OVER_BY_NAME; }
            set { _HANDED_OVER_BY_NAME = value; }
        }
        public int TRANSFER_CATEGORY
        {
            get { return _TRANSFER_CATEGORY; }
            set { _TRANSFER_CATEGORY = value; }
        }
        public int TRANSFERRING_DEPARTMENT_NAME
        {
            get { return _TRANSFERRING_DEPARTMENT_NAME; }
            set { _TRANSFERRING_DEPARTMENT_NAME = value; }

        }
        public int TAKEN_OVER_BY_NAME_HOD
        {
            get { return _TAKEN_OVER_BY_NAME_HOD; }
            set { _TAKEN_OVER_BY_NAME_HOD = value; }
        }
        public int RECEIVERS_DEPARTMENT
        {
            get { return _RECEIVERS_DEPARTMENT; }
            set { _RECEIVERS_DEPARTMENT = value; }
        }
        public int APPROVING_AUTHORITY
        {
            get { return _APPROVING_AUTHORITY; }
            set { _APPROVING_AUTHORITY = value; }
        }
        public string REMARKS
        {
            get { return _REMARKS; }
            set { _REMARKS = value; }
        }
        public DateTime DATE_OF_TRANSFER
        {
            get { return _DATE_OF_TRANSFER; }
            set { _DATE_OF_TRANSFER = value; }
        }
        #endregion

        #endregion

        #region APPROVAL OF ASSET TRANSFER

        #region Private

       private int _APTPNO=0;	
       private string _STATUS = string.Empty;
       private int _TRANSATION_ID = 0;
      
        #endregion

        #region Public
       public int APTPNO
       {
           get { return _APTPNO; }
           set { _APTPNO = value; }
       }

       public int TRANSATION_ID
       {
           get { return _TRANSATION_ID; }
           set { _TRANSATION_ID = value; }
       }
    
       public string STATUS
       {
           get { return _STATUS; }
           set { _STATUS = value; }
       }
        #endregion

        #endregion

        #region  Accept

       #region Private
       private int _ASTNO = 0;   
       private int _RECEIVER_STOCK_REGISTER_NO=0;
       private int _UA_NO = 0;
       
       private DateTime _DATE_OF_ACCEPTANCE;
       private string _ITEM_NAME=string.Empty;
       private string _TRANSFER_DEPARTMENT_NAME=string.Empty;
       #endregion

        #region Public
       public int ASTNO
       {
           get { return _ASTNO; }
           set { _ASTNO = value; }
       }
       public int UA_NO
       {
           get { return _UA_NO; }
           set { _UA_NO = value; }
       }
       public int RECEIVER_STOCK_REGISTER_NO
       {
           get { return _RECEIVER_STOCK_REGISTER_NO; }
           set { _RECEIVER_STOCK_REGISTER_NO = value; }
       }
       public string ITEM_NAME
       {
           get { return _ITEM_NAME; }
           set { _ITEM_NAME = value; }
       }
       public string TRANSFER_DEPARTMENT_NAME
       {
           get { return _TRANSFER_DEPARTMENT_NAME; }
           set { _TRANSFER_DEPARTMENT_NAME = value; }
       }
       public DateTime DATE_OF_ACCEPTANCE
       {
           get { return _DATE_OF_ACCEPTANCE; }
           set { _DATE_OF_ACCEPTANCE = value; }
       }
      
      


        #endregion


        #endregion


    }
}
