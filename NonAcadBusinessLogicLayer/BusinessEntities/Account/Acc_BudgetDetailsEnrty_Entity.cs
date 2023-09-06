//=================================================================================
// PROJECT NAME  : UAIMS - RFC-SVCEC                                                          
// MODULE NAME   :BUDGET_DETAILS_ENTRY                                                    
// CREATION DATE : 20-OCT-2019                                               
// CREATED BY    : ANDOJU VIJAY                                                
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================


using System;
using System.Data;
using System.Web;



namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLayer.BusinessEntities
        {
            public class Acc_BudgetDetailsEnrty_Entity
            {
                #region Private Members
                private int     _BUDGET_ALLOCDETAIL_ID  =0;
                private int     _BUDGETALLOCTION_ID  =0;
                private DateTime  _FROM_DATE  ;
                private DateTime  _TO_DATE ;
                private double  _DEPT_AMOUNT = 0;
                private double  _APPROVED_AMOUNT = 0;
                private int     _CREATED_BY =0;
                //private string  _CREATED_DATE = string.Empty;
                private int     _COLLEGE_ID = 0;
                //private string  _MODIFIED_DATE =string.Empty;
                private int     _MODIFIED_BY =0;
                private char    _APPROVED_SATUS ='P';
                private int     _IS_APPROVE = 0;
                private int    _Dept_id = 0;
                private int   _Parent_id = 0;
                private DataTable _Budgettable;
                private DataTable _BudgetApproveTable;
                #endregion

                #region Public Members
                public DataTable BudgetApproveTable
                {
                    get { return _BudgetApproveTable; }
                    set { _BudgetApproveTable = value; }
                }

                public DataTable Budgettable
                {
                    get { return _Budgettable;}
                    set {_Budgettable=value;}
                }
                public int BUDGET_ALLOCDETAIL_ID
                {
                    get { return _BUDGET_ALLOCDETAIL_ID; }
                    set { _BUDGET_ALLOCDETAIL_ID = value; }
                }
                public int Dept_id
                {
                    get { return _Dept_id; }
                    set { _Dept_id = value; }
                }
                public int BUDGETALLOCTION_ID
                {
                    get { return _BUDGETALLOCTION_ID; }
                    set { _BUDGETALLOCTION_ID = value; }
                }
                public int CREATED_BY
                {
                    get { return _CREATED_BY; }
                    set { _CREATED_BY = value; }
                }
                public int COLLEGE_ID
                {
                    get { return _COLLEGE_ID; }
                    set { _COLLEGE_ID = value; }
                }
                public int MODIFIED_BY
                {
                    get { return _MODIFIED_BY; }
                    set { _MODIFIED_BY = value; }
                }
                public int IS_APPROVE
                {
                    get { return _IS_APPROVE; }
                    set { _IS_APPROVE = value; }
                }
                public DateTime FROM_DATE
                {
                    get { return _FROM_DATE; }
                    set { _FROM_DATE = value; }
                }
                public DateTime TO_DATE
                {
                    get { return _TO_DATE; }
                    set { _TO_DATE = value; }
                }
                public double DEPT_AMOUNT
                {
                    get { return _DEPT_AMOUNT; }
                    set { _DEPT_AMOUNT = value; }
                }
                public double APPROVED_AMOUNT
                {
                    get { return _APPROVED_AMOUNT; }
                    set { _APPROVED_AMOUNT = value; }
                }
                public int Parent_id
                {
                    get { return _Parent_id ; }
                    set { _Parent_id = value; }
                }
                #endregion
            } 
         }     
     }         
 }             
                 