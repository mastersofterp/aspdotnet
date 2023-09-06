using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;


namespace IITMS
{
    namespace NITPRM
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class ItemMasterController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>

                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


                #region MAIN_ITEM_GROUP

                public int AddMainItemGroup(string mainItemGrpName,string collegeCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New ITEM_MASTER
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_MAIN_ITEM_GRP_NAME",mainItemGrpName);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE",collegeCode);
                        objParams[2] = new SqlParameter("@P_MAIN_ITEM_GRP_NO",SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_MAIN_ITEM_GROUP_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.AddItemMaster-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateMainItemGroup(string mainItemGrpName, string collegeCode,int mainItemGprNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New ITEM_MASTER
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_MAIN_ITEM_GRP_NAME", mainItemGrpName);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[2] = new SqlParameter("@P_MAIN_ITEM_GRP_NO", mainItemGprNo);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_MAIN_ITEM_GROUP_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.UpdateMainItemGroup-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllMainItemGroup()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_MAIN_ITEM_GROUP_GET_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.GetAllMainItemGroup-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSingleRecordMainItemGroup(int mainItemGprNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_MAIN_ITEM_GRP_NO", mainItemGprNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_MAIN_ITEM_GROUP_GET_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.GetSingleRecordMainItemGroup-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion


                #region MAIN_SUB_ITEM_GROUP

                public int AddMainSubItemGroup(string mainSubItemGrpName,int mainItemGprNo,string collegeCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New ITEM_MASTER
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_MAIN_SUB_ITEM_GRP_NAME", mainSubItemGrpName);
                        objParams[1] = new SqlParameter("@P_MAIN_ITEM_GRP_NO", mainItemGprNo);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE",collegeCode);
                        objParams[3] = new SqlParameter("@P_MAIN_SUB_ITEM_GRP_NO",SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_MAIN_SUB_ITEM_GROUP_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.AddMainSubItemGroup-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateMainSubItemGroup(string mainSubItemGrpName, int mainItemGprNo, string collegeCode, int mainSubItemGprNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New ITEM_MASTER
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_MAIN_SUB_ITEM_GRP_NAME", mainSubItemGrpName);
                        objParams[1] = new SqlParameter("@P_MAIN_ITEM_GRP_NO", mainItemGprNo);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[3] = new SqlParameter("@P_MAIN_SUB_ITEM_GRP_NO", mainSubItemGprNo);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_MAIN_SUB_ITEM_GROUP_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.UpdateMainSubItemGroup-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllMainSubItemGroup()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_MAIN_SUB_ITEM_GROUP_GET_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.GetAllMainSubItemGroup-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSingleRecordMainSubItemGroup(int mainSubItemGprNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_MAIN_SUB_ITEM_GRP_NO", mainSubItemGprNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_MAIN_SUB_ITEM_GROUP_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.GetSingleRecordMainSubItemGroup-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                #region Item_master

                public int AddItemMaster(ItemMaster objItemMaster)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New ITEM_MASTER
                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_ITEM_CODE", objItemMaster.ITEM_CODE);
                        objParams[1] = new SqlParameter("@P_ITEM_NAME", objItemMaster.ITEM_NAME);
                        objParams[2] = new SqlParameter("@P_ITEM_DETAILS",objItemMaster.ITEM_DETAILS);
                        objParams[3] = new SqlParameter("@P_MAIN_ITEM_GRP_NO", objItemMaster.MAIN_ITEM_GRP_NO);
                        objParams[4] = new SqlParameter("@P_MAIN_SUB_ITEM_GRP_NO", objItemMaster.MAIN_SUB_ITEM_GRP_NO);
                        objParams[5] = new SqlParameter("@P_UNIT", objItemMaster.UNIT);
                        objParams[6] = new SqlParameter("@P_RECORDED_QTY", objItemMaster.RECORDED_QTY);
                        objParams[7] = new SqlParameter("@P_MIN_QTY", objItemMaster.MIN_QTY);
                        objParams[8] = new SqlParameter("@P_MAX_QTY", objItemMaster.MAX_QTY);
                        objParams[9] = new SqlParameter("@P_BUD_QTY", objItemMaster.BUD_QTY);
                        objParams[10] = new SqlParameter("@P_CUR_QTY", objItemMaster.CUR_QTY);
                        objParams[11] = new SqlParameter("@P_OB_QTY", objItemMaster.OB_QTY);
                        objParams[12] = new SqlParameter("@P_OB_VALUE", objItemMaster.OB_VALUE);
                        objParams[13] = new SqlParameter("@P_COLLEGE_CODE", objItemMaster.COLLEGE_CODE);
                        objParams[14] = new SqlParameter("@P_ITEM_NO", SqlDbType.Int);
                        objParams[14].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_ITEM_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.AddItemMaster-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateItemMaster(ItemMaster objItemMaster)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Update New Item_Master
                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_ITEM_NO", objItemMaster.ITEM_NO);
                        objParams[1] = new SqlParameter("@P_ITEM_CODE", objItemMaster.ITEM_CODE);
                        objParams[2] = new SqlParameter("@P_ITEM_NAME", objItemMaster.ITEM_NAME);
                        objParams[3] = new SqlParameter("@P_ITEM_DETAILS", objItemMaster.ITEM_DETAILS);
                        objParams[4] = new SqlParameter("@P_MAIN_ITEM_GRP_NO", objItemMaster.MAIN_ITEM_GRP_NO);
                        objParams[5] = new SqlParameter("@P_MAIN_SUB_ITEM_GRP_NO", objItemMaster.MAIN_SUB_ITEM_GRP_NO);
                        objParams[6] = new SqlParameter("@P_UNIT", objItemMaster.UNIT);
                        objParams[7] = new SqlParameter("@P_RECORDED_QTY", objItemMaster.RECORDED_QTY);
                        objParams[8] = new SqlParameter("@P_MIN_QTY", objItemMaster.MIN_QTY);
                        objParams[9] = new SqlParameter("@P_MAX_QTY", objItemMaster.MAX_QTY);
                        objParams[10] = new SqlParameter("@P_BUD_QTY", objItemMaster.BUD_QTY);
                        objParams[11] = new SqlParameter("@P_CUR_QTY", objItemMaster.CUR_QTY);
                        objParams[12] = new SqlParameter("@P_OB_QTY", objItemMaster.OB_QTY);
                        objParams[13] = new SqlParameter("@P_OB_VALUE", objItemMaster.OB_VALUE);
                        objParams[14] = new SqlParameter("@P_COLLEGE_CODE", objItemMaster.COLLEGE_CODE);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_ITEM_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.UpdateItemMaster-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllItemMaster()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_ITEM_GET_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.GetAllItemMaster-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSingleRecordItemMaster(int itemNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ITEM_NO",itemNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_ITEM_GET_NO",objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.GetSingleRecordItemMaster-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

            }
        }
    }
}
