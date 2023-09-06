//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HEALTH (Disposable Item Issue)                        
// CREATION DATE : 30-AUG-2017                                                        
// CREATED BY    : MRUNAL SINGH 
//====================================================================================== 

using System;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLogicLayer.BusinessEntities.Health
        {
            public class DisposableItemIssue_Ent
            {

                #region Disposable Item Issue

                private int _DINO = 0;
                private int _ITEM_NO = 0;
                private int _AVAILABLE_QTY = 0;
                private int _ISSUE_QTY = 0;
                private int _BALANCE_QTY = 0;
                private DateTime _ISSUE_DATE;
                private string _REMARK = string.Empty;
                private string _COLLEGE_CODE = string.Empty;
                private DateTime _AUDIT_DATE;
                private int _USER_ID = 0;



                public int DINO
                {
                    get { return _DINO; }
                    set { _DINO = value; }
                }

                public int ITEM_NO
                {
                    get { return _ITEM_NO; }
                    set { _ITEM_NO = value; }
                }

                public int AVAILABLE_QTY
                {
                    get { return _AVAILABLE_QTY; }
                    set { _AVAILABLE_QTY = value; }
                }

                public int ISSUE_QTY
                {
                    get { return _ISSUE_QTY; }
                    set { _ISSUE_QTY = value; }
                }

                public int BALANCE_QTY
                {
                    get { return _BALANCE_QTY; }
                    set { _BALANCE_QTY = value; }
                }

                public DateTime ISSUE_DATE
                {
                    get { return _ISSUE_DATE; }
                    set { _ISSUE_DATE = value; }
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

                public DateTime AUDIT_DATE
                {
                    get { return _AUDIT_DATE; }
                    set { _AUDIT_DATE = value; }
                }

                public int USER_ID
                {
                    get { return _USER_ID; }
                    set { _USER_ID = value; }
                }   
                #endregion


            }
        }
    }
}
