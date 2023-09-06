using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class STR_ISSUE_ITEMS
            {
                #region Private Members

                private int ISSUENO;
                private int REQTRNO;
                private string ISSUE_SLIPNO;
                private DateTime ISSUE_DATE;
                private int QUANTITY;
                private char ISSUE_TYPE;
                private string STATUS;
                private string REMARK;
                private string IREMARK;
                private double RATE;
                private string COLLEGE_CODE;
                private int MDNO;
                private int ITEMNO;
                private int ISSUE_TNO;
                private int INVOICENO;
                private char STORE_USER_TYPE;
                private DataTable _DSRItemIssueTbl;
                private int _UA_NO;
                private DataTable _ItemTbl;
                private int COLLEGE_NO;
                private int ISSUE_TO_SDNO;
                private int ISSUE_TO_IDNO;
                
                
                #endregion

                #region Public Properties
                public int _ISSUE_TO_SDNO
                {
                    get { return ISSUE_TO_SDNO; }
                    set { ISSUE_TO_SDNO = value; }
                }
                public int _ISSUE_TO_IDNO
                {
                    get { return ISSUE_TO_IDNO; }
                    set { ISSUE_TO_IDNO = value; }
                }
                public int _COLLEGE_NO
                {
                    get { return COLLEGE_NO; }
                    set { COLLEGE_NO = value; }
                }
                public int _ISSUENO
                {
                    get { return ISSUENO; }
                    set { ISSUENO = value; }
                }
                public int _REQTRNO
                {
                    get { return REQTRNO; }
                    set { REQTRNO = value; }
                }
                public string _ISSUE_SLIPNO
                {
                    get { return ISSUE_SLIPNO; }
                    set { ISSUE_SLIPNO = value; }
                }
                public DateTime _ISSUE_DATE
                {
                    get { return ISSUE_DATE; }
                    set { ISSUE_DATE = value; }
                }
                public int _QUANTITY
                {
                    get { return QUANTITY; }
                    set { QUANTITY = value; }
                }
                public char _ISSUE_TYPE
                {
                    get { return ISSUE_TYPE; }
                    set { ISSUE_TYPE = value; }
                }
                public string _STATUS
                {
                    get { return STATUS; }
                    set { STATUS = value; }
                }
                public string _REMARK
                {
                    get { return REMARK; }
                    set { REMARK = value; }
                }
                public string _IREMARK
                {
                    get { return IREMARK; }
                    set { IREMARK = value; }
                }
                public double _RATE
                {
                    get { return RATE; }
                    set { RATE = value; }
                }
                public string _COLLEGE_CODE
                {
                    get { return COLLEGE_CODE; }
                    set { COLLEGE_CODE = value; }
                }
                public int _MDNO
                {
                    get { return MDNO; }
                    set { MDNO = value; }
                }
                public int _ITEMNO
                {
                    get { return ITEMNO; }
                    set { ITEMNO = value; }
                }
                public int _ISSUE_TNO
                {
                    get { return ISSUE_TNO; }
                    set { ISSUE_TNO = value; }
                }
                public int _INVOICENO
                {
                    get { return INVOICENO; }
                    set { INVOICENO = value; }
                }

                public char _STORE_USER_TYPE
                {

                    get { return _STORE_USER_TYPE; }
                    set { _STORE_USER_TYPE = value; }                    
                }

                public DataTable DSRItemIssueTbl
                {
                    get { return _DSRItemIssueTbl; }
                    set { _DSRItemIssueTbl = value; }
                }

                public int UA_NO
                {
                    get { return _UA_NO; }
                    set { _UA_NO = value; }
                }

                public DataTable ItemTbl
                {
                    get { return _ItemTbl; }
                    set { _ItemTbl = value; }
                }
               

                #endregion

            }
        }
    }
}
