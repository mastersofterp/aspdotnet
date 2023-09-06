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
            public class Str_ItemServiceController
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                public int AddItemService(Str_ItemService Obj)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New NewsPaper
                        objParams = new SqlParameter[14];
                        objParams[0] = new SqlParameter("@P_PAYMENT_MODE", Obj.PAYMENT_MODE);
                        objParams[1] = new SqlParameter("@P_ITEM_NO", Obj.ITEM_NO);
                        objParams[2] = new SqlParameter("@P_VENDOR_NO", Obj.VENDOR_NO);
                        objParams[3] = new SqlParameter("@P_IN_DATE_TIME", Obj.IN_DATE_TIME);
                        objParams[4] = new SqlParameter("@P_OUT_DATE_TIME", Obj.OUT_DATE_TIME);
                        objParams[5] = new SqlParameter("@P_REMARK", Obj.REMARK);
                        objParams[6] = new SqlParameter("@P_NEXT_VISIT_DATE", Obj.NEXT_VISIT_DATE);
                        objParams[7] = new SqlParameter("@P_BILL_NO", Obj.BILL_NO);
                        objParams[8] = new SqlParameter("@P_WORK_ORDER_NO", Obj.WORK_ORDER_NO);
                        objParams[9] = new SqlParameter("@P_PAID_ON_DATE", Obj.PAID_ON_DATE);
                        objParams[10] = new SqlParameter("@P_BILL_DATE", Obj.BILL_DATE);
                        objParams[11] = new SqlParameter("@P_ITEM_SERVICE_TRAN", Obj.Item_Servicing_table);
                        objParams[12] = new SqlParameter("@P_AMOUNT", Obj.AMOUNT);
                        objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[13].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STORE_INS_UPD_ITEM_SERVICE", objParams, true);
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
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.NewsPaperController.AddNewsPaper-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetStoreTables()
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_TABLES", objParams);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.NewsPaperController.AddNewsPaper-> " + ex.ToString());
                    }
                    return ds;

                }

                public DataSet GetTablesforScarpentry()
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_TABLES_FOR_SCARP", objParams);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.NewsPaperController.AddNewsPaper-> " + ex.ToString());
                    }
                    return ds;

                }
                public int AddScarpEntry(Str_ItemService Obj)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New NewsPaper
                        objParams = new SqlParameter[7];

                        objParams[0] = new SqlParameter("@P_ITEM_NO", Obj.ITEM_NO);
                        objParams[1] = new SqlParameter("@P_SCRAP_REFERENCE_NO", Obj.SCRAP_REFERENCE_NO);
                        objParams[2] = new SqlParameter("@P_DISPOSAL_DATE", Obj.DISPOSAL_DATE);
                        objParams[3] = new SqlParameter("@P_RECOMMENDED_BY", Obj.RECOMMENDED_BY);
                        objParams[4] = new SqlParameter("@P_REMARK", Obj.REMARK);
                        objParams[5] = new SqlParameter("@P_STORE_SCRAP_ENTRY_TBL", Obj.Scrap_table);
                        //objParams[6] = new SqlParameter("@P_DSRTRNO", Obj.DSRTRNO);
                        //objParams[7] = new SqlParameter("@P_TRANSACTION_ID", Obj.TRANSACTION_ID);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_SCRAP_ENTRY", objParams, true);
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
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.NewsPaperController.AddNewsPaper-> " + ex.ToString());
                    }
                    return retStatus;
                }
            }
        }
    }
}
