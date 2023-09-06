using System;
using System.Data;
using System.Web;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;



namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class Str_ItemService
            {
                #region ItemService
                private int _SRNO = 0;
                private int _ITEM_NO = 0;
                private int _VENDOR_NO = 0;
                private DateTime _IN_DATE_TIME;
                private DateTime _OUT_DATE_TIME;
                private string _VENDOR_ADDRESS;
                private DateTime _NEXT_VISIT_DATE;
                private int _BILL_NO = 0;
                private int _WORK_ORDER_NO = 0;
                private DateTime _PAID_ON_DATE;
                private DateTime _BILL_DATE;
                private string _PAYMENT_MODE = string.Empty;
                private string _REMARK = string.Empty;
                private DataTable _Item_Servicing_table;
                private double _AMOUNT = 0.0;
                private int _DSRTRNO = 0;
                private int _TRANSACTION_ID = 0;


                public int TRANSACTION_ID
                {
                    get { return _TRANSACTION_ID; }
                    set { _TRANSACTION_ID=value; }
                }

                public int DSRTRNO
                {
                    get {return _DSRTRNO; }
                    set { _DSRTRNO=value; }
                }
                public double AMOUNT
                {
                    get { return _AMOUNT; }
                    set { _AMOUNT = value; }
                }

                public DataTable Item_Servicing_table
                {
                    get { return _Item_Servicing_table; }
                    set { _Item_Servicing_table = value; }
                }
                public string REMARK
                {
                    get { return _REMARK; }
                    set { _REMARK = value; }
                }
                public string VENDOR_ADDRESS
                {
                    get { return _VENDOR_ADDRESS; }
                    set { _VENDOR_ADDRESS = value; }
                }
                public string PAYMENT_MODE
                {
                    get { return _PAYMENT_MODE; }
                    set { _PAYMENT_MODE = value; }
                }

                public int BILL_NO
                {
                    get { return _BILL_NO; }
                    set { _BILL_NO = value; }
                }
                public int WORK_ORDER_NO
                {
                    get { return _WORK_ORDER_NO; }
                    set { _WORK_ORDER_NO = value; }
                }
                public int ITEM_NO
                {
                    get { return _ITEM_NO; }
                    set { _ITEM_NO = value; }
                }
                public int VENDOR_NO
                {
                    get { return _VENDOR_NO; }
                    set { _VENDOR_NO = value; }
                }
                public DateTime IN_DATE_TIME
                {
                    get { return _IN_DATE_TIME; }
                    set { _IN_DATE_TIME = value; }
                }
                public DateTime OUT_DATE_TIME
                {
                    get { return _OUT_DATE_TIME; }
                    set { _OUT_DATE_TIME = value; }
                }
                public DateTime NEXT_VISIT_DATE
                {
                    get { return _NEXT_VISIT_DATE; }
                    set { _NEXT_VISIT_DATE = value; }
                }
                public DateTime PAID_ON_DATE
                {
                    get { return _PAID_ON_DATE; }
                    set { _PAID_ON_DATE = value; }
                }
                public DateTime BILL_DATE
                {
                    get { return _BILL_DATE; }
                    set { _BILL_DATE = value; }
                }

                #endregion


                private string   _SCRAP_REFERENCE_NO = string.Empty;
                private DateTime _DISPOSAL_DATE;
                private string _RECOMMENDED_BY = string.Empty;
                private DataTable _Scrap_table;


                public string SCRAP_REFERENCE_NO
                {
                    get { return _SCRAP_REFERENCE_NO; }
                    set { _SCRAP_REFERENCE_NO = value; }
                }
                public string RECOMMENDED_BY
                {
                    get { return _RECOMMENDED_BY; }
                    set { _RECOMMENDED_BY = value; }
                }
                public DateTime DISPOSAL_DATE
                {
                    get { return _DISPOSAL_DATE; }
                    set { _DISPOSAL_DATE = value; }
                }

                public DataTable Scrap_table
                {
                    get { return _Scrap_table; }
                    set { _Scrap_table = value; }
                }

            }



        }
    }
}
