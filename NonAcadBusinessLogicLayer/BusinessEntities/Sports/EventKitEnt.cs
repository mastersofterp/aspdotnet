//================================================================
//CREATED BY    : MRUNAL SINGH
//PROJECT NAME  : UAIMS
//MODULE NAME   : SPORTS & EVENT
//CREATION DATE : 18-MAY-2017      
//================================================================ 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class EventKitEnt
            {
                #region  Kit/Store

                #region Party Category Master

                private string _PCNAME = string.Empty;

                public string PCNAME
                {
                    get { return _PCNAME; }
                    set { _PCNAME = value; }
                }


                #endregion

                #region Party Master

                private int _PNO = 0;
                private string _PCODE = string.Empty;
                private string _PNAME = string.Empty;
                private int _PCNO = 0;
                private string _ADDRESS = string.Empty;
                private int _CITYNO = 0;
                private int _STATENO = 0;
                private string _PHONE = string.Empty;
                private string _EMAIL = string.Empty;
                private string _CST = string.Empty;
                private string _BST = string.Empty;
                private string _REMARK = string.Empty;
                private string _COLLEGE_CODE = string.Empty;
                private string _GST = string.Empty;
                private string _PANNO = string.Empty;



                public int PNO
                {
                    get { return _PNO; }
                    set { _PNO = value; }
                }
                public string PCODE
                {
                    get { return _PCODE; }
                    set { _PCODE = value; }
                }

                public string PNAME
                {
                    get { return _PNAME; }
                    set { _PNAME = value; }
                }

                public int PCNO
                {
                    get { return _PCNO; }
                    set { _PCNO = value; }
                }

                public string ADDRESS
                {
                    get { return _ADDRESS; }
                    set { _ADDRESS = value; }
                }
                public int CITYNO
                {
                    get { return _CITYNO; }
                    set { _CITYNO = value; }
                }
                public int STATENO
                {
                    get { return _STATENO; }
                    set { _STATENO = value; }
                }
                public string PHONE
                {
                    get { return _PHONE; }
                    set { _PHONE = value; }
                }

                public string EMAIL
                {
                    get { return _EMAIL; }
                    set { _EMAIL = value; }
                }
                public string CST
                {
                    get { return _CST; }
                    set { _CST = value; }
                }
                public string BST
                {
                    get { return _BST; }
                    set { _BST = value; }
                }

                public string REMARK
                {
                    get { return _REMARK; }
                    set { _REMARK = value; }
                }
                public string COLLEGE_CODE
                {
                    get { return _COLLEGE_CODE; }
                    set { _COLLEGE_CODE = value; }
                }
                public string GST
                {
                    get { return _GST; }
                    set { _GST = value; }
                }
                public string PANNO
                {
                    get { return _PANNO; }
                    set { _PANNO = value; }
                }

                #endregion

                #region Item Master

                private int _ITEM_NO = 0;
                private string _ITEM_CODE = string.Empty;
                private string _ITEM_NAME = string.Empty;
                private string _ITEM_DETAILS = string.Empty;
                private int _MAIN_ITEM_GRP_NO = 0;
                private int _MAIN_SUB_ITEM_GRP_NO = 0;
                private string _UNIT = string.Empty;
                private int _RECORDED_QTY = 0;
                private int _MIN_QTY = 0;
                private int _MAX_QTY = 0;
                private int _BUD_QTY = 0;
                private int _CUR_QTY = 0;
                private int _OB_QTY = 0;
                private int _OB_VALUE = 0;



                private int _MIGNO;
                private int _MISGNO;
                private string _ITEM_UNIT;
                private int _ITEM_REORDER_QTY;
                private int _ITEM_MIN_QTY;
                private int _ITEM_MAX_QTY;
                private int _ITEM_BUD_QTY;
                private int _ITEM_CUR_QTY;
                private int _ITEM_OB_QTY;
                private int _ITEM_OB_VALUE;
                private string _ITEM_APPROVAL;
                private int _ITEM_RATE;
                private double _ITEM_AMT;


                public double ITEM_AMT
                {
                    get { return _ITEM_AMT; }
                    set { _ITEM_AMT = value; }
                }
                public int ITEM_RATE
                {
                    get { return _ITEM_RATE; }
                    set { _ITEM_RATE = value; }
                }
                public int MIGNO
                {
                    get { return _MIGNO; }
                    set { _MIGNO = value; }
                }

                public int MISGNO
                {
                    get { return _MISGNO; }
                    set { _MISGNO = value; }
                }

                public string ITEM_UNIT
                {
                    get { return _ITEM_UNIT; }
                    set { _ITEM_UNIT = value; }
                }

                public int ITEM_REORDER_QTY
                {
                    get { return _ITEM_REORDER_QTY; }
                    set { _ITEM_REORDER_QTY = value; }
                }

                public int ITEM_MIN_QTY
                {
                    get { return _ITEM_MIN_QTY; }
                    set { _ITEM_MIN_QTY = value; }
                }

                public int ITEM_MAX_QTY
                {
                    get { return _ITEM_MAX_QTY; }
                    set { _ITEM_MAX_QTY = value; }
                }

                public int ITEM_BUD_QTY
                {
                    get { return _ITEM_BUD_QTY; }
                    set { _ITEM_BUD_QTY = value; }
                }

                public int ITEM_CUR_QTY
                {
                    get { return _ITEM_CUR_QTY; }
                    set { _ITEM_CUR_QTY = value; }
                }
                public int ITEM_OB_QTY
                {
                    get { return _ITEM_OB_QTY; }
                    set { _ITEM_OB_QTY = value; }
                }

                public int ITEM_OB_VALUE
                {
                    get { return _ITEM_OB_VALUE; }
                    set { _ITEM_OB_VALUE = value; }
                }
                public string ITEM_APPROVAL
                {
                    get { return _ITEM_APPROVAL; }
                    set { _ITEM_APPROVAL = value; }
                }
                public int ITEM_NO
                {
                    get { return _ITEM_NO; }
                    set { _ITEM_NO = value; }
                }

                public string ITEM_CODE
                {
                    get { return _ITEM_CODE; }
                    set { _ITEM_CODE = value; }
                }

                public string ITEM_NAME
                {
                    get { return _ITEM_NAME; }
                    set { _ITEM_NAME = value; }
                }

                public string ITEM_DETAILS
                {
                    get { return _ITEM_DETAILS; }
                    set { _ITEM_DETAILS = value; }
                }


                public int MAIN_ITEM_GRP_NO
                {
                    get { return _MAIN_ITEM_GRP_NO; }
                    set { _MAIN_ITEM_GRP_NO = value; }
                }
                public int MAIN_SUB_ITEM_GRP_NO
                {
                    get { return _MAIN_SUB_ITEM_GRP_NO; }
                    set { _MAIN_SUB_ITEM_GRP_NO = value; }
                }

                public string UNIT
                {
                    get { return _UNIT; }
                    set { _UNIT = value; }
                }
                public int RECORDED_QTY
                {
                    get { return _RECORDED_QTY; }
                    set { _RECORDED_QTY = value; }
                }
                public int MIN_QTY
                {
                    get { return _MIN_QTY; }
                    set { _MIN_QTY = value; }
                }

                public int MAX_QTY
                {
                    get { return _MAX_QTY; }
                    set { _MAX_QTY = value; }
                }
                public int BUD_QTY
                {
                    get { return _BUD_QTY; }
                    set { _BUD_QTY = value; }
                }

                public int CUR_QTY
                {
                    get { return _CUR_QTY; }
                    set { _CUR_QTY = value; }
                }


                public int OB_QTY
                {
                    get { return _OB_QTY; }
                    set { _OB_QTY = value; }
                }

                public int OB_VALUE
                {
                    get { return _OB_VALUE; }
                    set { _OB_VALUE = value; }
                }
                #endregion

                #region Invoice Entry

                private DataTable _ITEMLIST = null;

                public DataTable ITEMLIST
                {
                    get { return _ITEMLIST; }
                    set { _ITEMLIST = value; }
                }

                public int INVTRNO { get; set; }
                public string INVOICE_NO { get; set; }
                public DateTime INVOICE_DATE { get; set; }
                public string D_M_NO { get; set; }
                public DateTime D_M_DATE { get; set; }
                public int VENDOR_NO { get; set; }
                public DateTime RECEIVE_DATE { get; set; }
                public string PURCHASE_ORDER_NO { get; set; }
                public DateTime PURCHASE_ORDER_DATE { get; set; }

                public string ITEM_BATCHNO { get; set; }
                public DateTime EXP_DATE { get; set; }
                public DateTime MFG_DATE { get; set; }

                public string POREFNO { get; set; }
                public int MDNO { get; set; }
                public double GRAMT { get; set; }
                public double NETAMT { get; set; }
                //  public string REMARK { get; set; }
                public bool FLAG1 { get; set; }
                public bool FLAG2 { get; set; }
                public bool FLAG3 { get; set; }
                public bool FLAG4 { get; set; }
                public string ECHG1 { get; set; }
                public string ECHG2 { get; set; }
                public string ECHG3 { get; set; }
                public string ECHG4 { get; set; }
                public double EP1 { get; set; }
                public double EP2 { get; set; }
                public double EP3 { get; set; }
                public double EP4 { get; set; }
                public double EAMT1 { get; set; }
                public double EAMT2 { get; set; }
                public double EAMT3 { get; set; }
                public double EAMT4 { get; set; }

                public double TAXAMOUNT { get; set; }

                public DateTime ACCDATE { get; set; }
                public int ACCQTY { get; set; }
                public string ACCSTATUS { get; set; }
                public string STATAUS { get; set; }
                public string ACCREMARK { get; set; }
                public int ITEMTOTQTY { get; set; }
                #endregion




                #region   Issue Item & Return Item

                private int _ISSUE_ID = 0;
                private int _TEAMID = 0;
                private int _CLUBID = 0;
                private char _ISSUE_TYPE;
                private DateTime _ISSUE_DATE;
                private int _USERID = 0;
                private int _ISSUE_ITEM_ID = 0;
                private DataTable _ISSUE_ITEM_LIST = null;
                private DataTable _ISSUE_RETURN_TBL = null;


                public DataTable ISSUE_RETURN_TBL
                {
                    get { return _ISSUE_RETURN_TBL; }
                    set { _ISSUE_RETURN_TBL = value; }
                }

                public DataTable ISSUE_ITEM_LIST
                {
                    get { return _ISSUE_ITEM_LIST; }
                    set { _ISSUE_ITEM_LIST = value; }
                }
                public int ISSUE_ID
                {
                    get { return _ISSUE_ID; }
                    set { _ISSUE_ID = value; }
                }
                public int TEAMID
                {
                    get { return _TEAMID; }
                    set { _TEAMID = value; }
                }
                public int CLUBID
                {
                    get { return _CLUBID; }
                    set { _CLUBID = value; }
                }
                public char ISSUE_TYPE
                {
                    get { return _ISSUE_TYPE; }
                    set { _ISSUE_TYPE = value; }
                }
                public DateTime ISSUE_DATE
                {
                    get { return _ISSUE_DATE; }
                    set { _ISSUE_DATE = value; }
                }

                public int USERID
                {
                    get { return _USERID; }
                    set { _USERID = value; }
                }
                public int ISSUE_ITEM_ID
                {
                    get { return _ISSUE_ITEM_ID; }
                    set { _ISSUE_ITEM_ID = value; }
                }

                #endregion


                #endregion

            }
        }
    }
}
