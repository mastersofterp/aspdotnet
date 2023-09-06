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
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class Str_AMC_Con
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetTablesForAMC()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STORE_GET_TABLES_FOR_AMC", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DepartmentController.GetAllDepartMent-> " + ex.ToString());
                    }
                    return ds;
                }
                public int InsUpdAmcProposal(Str_AMC_ENT ObjEnt)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[20];
                        objParams[0] = new SqlParameter("@P_AMCNO", ObjEnt.AMCNO);             
                        objParams[1] = new SqlParameter("@P_REF_NUMBER",ObjEnt.REF_NUMBER);		
                        objParams[2] = new SqlParameter("@P_BHNO",ObjEnt.BHNO);				
                        objParams[3] = new SqlParameter("@P_MDNO",ObjEnt.MDNO);			
                        objParams[4] = new SqlParameter("@P_AMC_DATE",ObjEnt.DATE);			
                        objParams[5] = new SqlParameter("@P_AMC_FROM_DATE",ObjEnt.AMC_FROM_DATE);		
                        objParams[6] = new SqlParameter("@P_AMC_TO_DATE",ObjEnt.AMC_TO_DATE);		
                        objParams[7] = new SqlParameter("@P_AMC_STAUS",ObjEnt.STATUS);		
                        objParams[8] = new SqlParameter("@P_BUDGET_PROVISION",ObjEnt.BUDGET_PROVISION);	
                        objParams[9] = new SqlParameter("@P_AMC_DETAILS",ObjEnt.AMC_DETAILS);			
                        objParams[10] = new SqlParameter("@P_AMC_CATEGORY",ObjEnt.AMC_CATEGORY);	
                        objParams[11] = new SqlParameter("@P_VENDOR",ObjEnt.VENDOR);				
                        objParams[12] = new SqlParameter("@P_COST_AMC",ObjEnt.AMC_COST);
                        objParams[13] = new SqlParameter("@P_AMC_TABLE", ObjEnt.AMC_PROPOSAL);
                        objParams[14] = new SqlParameter("@P_TRANSACTION_ID", ObjEnt.TRANSATION_ID);
                        objParams[15] = new SqlParameter("@P_ITEM_NO", ObjEnt.ITEM_NO);
                        objParams[16] = new SqlParameter("@P_MODE_OF_PAYMENT", ObjEnt.AMC_PAYMENT_MODE);
                        objParams[17] = new SqlParameter("@P_PAID_AMOUNT", ObjEnt.PAID_AMOUNT);
                        objParams[18] = new SqlParameter("@P_DATE_OF_PAYMENT", ObjEnt.AMC_DATE_OF_PAYMENT);
                        objParams[19] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[19].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STR_INSERT_UPDATE_AMC_PROPOSAL", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        return retStatus;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DepartmentController.GetAllDepartMent-> " + ex.ToString());
                    }
                    return retStatus;
                }



                public int InsUpdAmcApproval(Str_AMC_ENT ObjEnt)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_SRNO", ObjEnt.SRNO);
                        objParams[1] = new SqlParameter("@P_NUMBER_OF_QUOTAIONS", ObjEnt.NUMBER_OF_QUOTAIONS);
                        objParams[2] = new SqlParameter("@P_COMPANY_NAME", ObjEnt.COMPANY_NAME);
                        objParams[3] = new SqlParameter("@P_COMMENTS_ON_COMPARATIVE_STATEMENT", ObjEnt.COMMENTS_ON_COMPARATIVE_STATEMENT);
                        objParams[4] = new SqlParameter("@P_PAYMENT_DETAILS", ObjEnt.PAYMENT_DETAILS);
                        objParams[5] = new SqlParameter("@P_BUDGET_PROVISION", ObjEnt.BUDGET_PROVISION);
                        objParams[6] = new SqlParameter("@P_APPROVAL", ObjEnt.APPROVAL);
                        objParams[7] = new SqlParameter("@P_AMOUNT_LEFT", ObjEnt.AMOUNT_LEFT);
                        objParams[8] = new SqlParameter("@P_AMC_APPROVAL", ObjEnt.AMC_PROPOSAL);
                        objParams[9] = new SqlParameter("@P_SUBDEPTNO", ObjEnt.SUBDEPTNO);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STORE_INS_UPD_AMC_APPROVAL", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        return retStatus;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DepartmentController.GetAllDepartMent-> " + ex.ToString());
                    }
                    return retStatus;
                }


                #region Asset Transfering

                #region ASSETS TRANSFER

                public DataSet GetTablesForAssetTransfer()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("STR_GET_TABLES_OF_ASSET_TRANSFER", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DepartmentController.GetAllDepartMent-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsUpdAssetTransfer(Str_AMC_ENT ObjEnt)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_ATE_NO", ObjEnt.ATE_NO);
                        objParams[1] = new SqlParameter("@P_STOCK_REGISTER_NO", ObjEnt.STOCK_REGISTER_NO);
                        objParams[2] = new SqlParameter("@P_TRANSFER_NO", ObjEnt.TRANSFER_NO);
                        objParams[3] = new SqlParameter("@P_DATE_OF_TRANSFER", ObjEnt.DATE_OF_TRANSFER);
                        objParams[4] = new SqlParameter("@P_ITEM_NO", ObjEnt.ITEM_NO);
                        objParams[5] = new SqlParameter("@P_HANDED_OVER_BY_NAME", ObjEnt.HANDED_OVER_BY_NAME);
                        objParams[6] = new SqlParameter("@P_TRANSFER_CATEGORY", ObjEnt.TRANSFER_CATEGORY);
                        objParams[7] = new SqlParameter("@P_TRANSFERRING_DEPARTMENT_NAME", ObjEnt.TRANSFERRING_DEPARTMENT_NAME);
                        objParams[8] = new SqlParameter("@P_TAKEN_OVER_BY_NAME_HOD", ObjEnt.TAKEN_OVER_BY_NAME_HOD);
                        objParams[9] = new SqlParameter("@P_RECEIVERS_DEPARTMENT", ObjEnt.RECEIVERS_DEPARTMENT);
                        objParams[10] = new SqlParameter("@P_REMARKS", ObjEnt.REMARKS);
                        objParams[11] = new SqlParameter("@P_APPROVING_AUTHORITY", ObjEnt.APPROVING_AUTHORITY);
                        objParams[12] = new SqlParameter("@P_ASSET_TRANSFER_TABLE", ObjEnt.ASSETTRANSFERTABLE);
                        objParams[13] = new SqlParameter("@P_TRANSATION_ID", ObjEnt.TRANSATION_ID);
                        objParams[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[14].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STR_STORE_ASSET_TRANSFER_ENTRY_IU", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        return retStatus;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DepartmentController.GetAllDepartMent-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int updAmcApproval(Str_AMC_ENT ObjEnt)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_TRANSACTION_ID", ObjEnt.TRANSATION_ID);
                        objParams[1] = new SqlParameter("@P_STATUS", ObjEnt.AMC_STATUS);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_AMC_APPROVAL_IU", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        return retStatus;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DepartmentController.GetAllDepartMent-> " + ex.ToString());
                    }
                    return retStatus;
                }


                #endregion

                #region APPROVAL OF ASSET TRANSFER(BY PRINCIPAL)

                public DataSet GetTablesForApprovalAsset()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_APPROVAL_ASSET_TABLES", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DepartmentController.GetAllDepartMent-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAssettransferDetails(Str_AMC_ENT ObjEnt)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_TRANSATION_ID", ObjEnt.TRANSATION_ID);
                        objParams[1] = new SqlParameter("@P_ITEM_NO", ObjEnt.ITEM_NO);
                        objParams[2] = new SqlParameter("@P_STATUS", ObjEnt.STATUS);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_ASSET_TANSFER_ENTRY_DETAILS", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DepartmentController.GetAllDepartMent-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsUpdApprovalAsset(Str_AMC_ENT ObjEnt)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_APTPNO", ObjEnt.APTPNO);
                        objParams[1] = new SqlParameter("@P_TRANSFER_NO", ObjEnt.TRANSFER_NO);
                        objParams[2] = new SqlParameter("@P_STATUS", ObjEnt.STATUS);
                        objParams[3] = new SqlParameter("@P_REMARK", ObjEnt.REMARKS);
                        objParams[4] = new SqlParameter("@P_APPROVAL_ASSET_TBL", ObjEnt.ASSETTRANSFERTABLE);
                        objParams[5] = new SqlParameter("@P_ITEM_NO", ObjEnt.ITEM_NO);
                        objParams[6] = new SqlParameter("@P_DATE_OF_TRANSFER", ObjEnt.DATE_OF_ACCEPTANCE);
                        objParams[7] = new SqlParameter("@P_TRANSATION_ID", ObjEnt.TRANSATION_ID);
                        objParams[8] = new SqlParameter("@P_UA_NO", ObjEnt.UA_NO);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STR_ASSET_TRANSFER_APPROVAL_IU", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        return retStatus;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DepartmentController.GetAllDepartMent-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAssetItemList()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_ASSET_ITEM_LIST", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DepartmentController.GetAllDepartMent-> " + ex.ToString());
                    }
                    return ds;

                }
                #endregion

                #region  ACCEPTANCE
                public DataSet GetAcceptanceTables()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("STR_GET_ACCEPTANCE_TABLE", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DepartmentController.GetAllDepartMent-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetApprovalItemList()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_APPROVAL_ITEM_LIST", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DepartmentController.GetAllDepartMent-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsUpdAcceptlAsset(Str_AMC_ENT ObjEnt)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_ASTNO", ObjEnt.ASTNO);
                        objParams[1] = new SqlParameter("@P_RECEIVER_STOCK_REGISTER_NO", ObjEnt.RECEIVER_STOCK_REGISTER_NO);
                        objParams[2] = new SqlParameter("@P_TRANSFER_NO", ObjEnt.TRANSFER_NO);
                        objParams[3] = new SqlParameter("@P_DATE_OF_ACCEPTANCE", ObjEnt.DATE_OF_ACCEPTANCE);
                        objParams[4] = new SqlParameter("@P_ITEM_NO", ObjEnt.ITEM_NO);
                        objParams[5] = new SqlParameter("@P_ITEM_NAME", ObjEnt.ITEM_NAME);
                        objParams[6] = new SqlParameter("@P_TRANSFER_DEPARTMENT_NAME", ObjEnt.TRANSFER_DEPARTMENT_NAME);
                        objParams[7] = new SqlParameter("@P_REMARK", ObjEnt.REMARKS);
                        objParams[8] = new SqlParameter("@P_TRANSACTION_ID", ObjEnt.TRANSATION_ID);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STR_ACCEPTANCE_OF_ASSET_TRANSFERS_IU", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        return retStatus;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DepartmentController.GetAllDepartMent-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion

                #region AMC Bill Payment

                public DataSet GetApproveList(int Deptno, string StoreUser)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_DEPTNO", Deptno);
                        objParams[1] = new SqlParameter("@P_STORE_USER", StoreUser);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STORE_GET_APPROVED_AMC_LIST", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_AMC_Con.GetApproveList-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAMCList(int Deptno, string StoreUser)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;                       
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_DEPTNO", Deptno);
                        objParams[1] = new SqlParameter("@P_STORE_USER", StoreUser);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STORE_GET_AMC_LIST", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_AMC_Con.GetApproveList-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddUpdAMCBillPayment(Str_AMC_ENT ObjEnt)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_BILL_PAYMENT_ID", ObjEnt.BILL_PAYMENT_ID);
                        objParams[1] = new SqlParameter("@P_TRANSACTION_ID", ObjEnt.TRANSACTION_ID);                       
                        objParams[2] = new SqlParameter("@P_AMC_BILL_PAYMENT_TABLE", ObjEnt.BILL_PAYMENT_DT);
                        
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STR_AMC_BILL_PAYMENT_INS_UPD", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        return retStatus;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DepartmentController.GetAllDepartMent-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetRefNumber(String Deptno, string StoreUser)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_DEPTNO", Deptno);
                        objParams[1] = new SqlParameter("@P_STORE_USER", StoreUser);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STORE_GET_REFF_NUMBER", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_AMC_Con.GetApproveList-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #endregion
            }
        }
    }
}
