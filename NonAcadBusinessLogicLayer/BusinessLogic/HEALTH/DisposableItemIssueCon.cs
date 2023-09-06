//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HEALTH (Disposable Item Issue)                        
// CREATION DATE : 30-AUG-2017                                                            
// CREATED BY    : MRUNAL SINGH 
//====================================================================================== 

using System;
using System.Data;
using System.Web;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Health;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.DAC;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLogicLayer.BusinessLogic
        {
            public class DisposableItemIssueCon
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public int DisposableItemIssueIU(DisposableItemIssue_Ent objDisp)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_DINO", objDisp.DINO);
                        objParams[1] = new SqlParameter("@P_ITEM_NO", objDisp.ITEM_NO);
                        objParams[2] = new SqlParameter("@P_AVAILABLE_QTY", objDisp.AVAILABLE_QTY);
                        objParams[3] = new SqlParameter("@P_ISSUE_QTY", objDisp.ISSUE_QTY);
                        objParams[4] = new SqlParameter("@P_ISSUE_DATE", objDisp.ISSUE_DATE);
                        objParams[5] = new SqlParameter("@P_REMARK", objDisp.REMARK);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objDisp.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_USERID", objDisp.USER_ID);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_DISPOSABLE_ITEM_ISSUE_IU", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DisposableItemIssueCon.DisposableItemIssueIU-> " + ex.ToString());
                    }
                    return retStatus;
                }

                // to get items available qty
                public DataSet GetDispItemAvailableQty(int ITEM_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            { 
                              new SqlParameter("@P_ITEM_NO", ITEM_NO),                                
                            };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_GET_DISPOSABLE_ITEM_AVAILABLE_QTY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.DisposableItemIssueCon.GetDispItemAvailableQty() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                // to get disposable items issue list 
                public DataSet GetDisposableItemIssueList()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_GET_DISPOSABLE_ITEM_ISSUE_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DisposableItemIssueCon.GetDisposableItemIssueList()-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// This method is used to get stock availability of selected item for prescription.
                /// </summary>
                public DataSet GetInsufficientStockDetails(int ITEM_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            { 
                              new SqlParameter("@P_ITEM_NO",ITEM_NO),   
                            };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_GET_DISPOSABLE_INSUFFICIENT_STOCK_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.DisposableItemIssueCon.GetInsufficientStockDetails() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

            }
        }
    }
}
