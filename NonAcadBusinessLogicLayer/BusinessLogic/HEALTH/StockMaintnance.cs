//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HEALTH                         
// CREATION DATE : 26-FEB-2016                                                        
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
           public class StockMaintnance
            {
                StoreMasterController objMaster = new StoreMasterController();
                Str_Purchase_Order_Controller objPO = new Str_Purchase_Order_Controller();
                Common objComman = new Common();

                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                #region Party_Category

                public int AddPartyCategory(string categoryName, string collegeCode, string userid, string shortname)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New ITEM_MASTER
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_PCNAME", categoryName);
                        objParams[4] = new SqlParameter("@P_PCSHORTNAME", shortname);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[2] = new SqlParameter("@P_USER_ID", userid);
                        objParams[3] = new SqlParameter("@P_PCNO", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_PARTY_CATEGORY_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StockMaintnance.AddParyCategory-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdatePartyCategory(string categoryName, int categoryNo, string collegeCode, string userid, string shortname)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New ITEM_MASTER
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_PCNO", categoryNo);
                        objParams[1] = new SqlParameter("@P_PCNAME", categoryName);
                        objParams[4] = new SqlParameter("@P_PCSHORTNAME", shortname);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[3] = new SqlParameter("@P_USER_ID", userid);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_PARTY_CATEGORY_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StockMaintnance.UpdateParyCategory-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllParyCategory()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_PARTY_CATEGORY_GET_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StockMaintnance.GetAllParyCategory-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSingleRecordParyCategory(int categoryNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PARTY_CATEGORY_NO", categoryNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_PARTY_CATEGORY_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StockMaintnance.GetSingleRecordParyCategory-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #region Party

                public int AddParty(StockMaster objStock, string userid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New ITEM_MASTER
                        objParams = new SqlParameter[14];
                        objParams[0] = new SqlParameter("@P_PCODE", objStock.PCODE);
                        objParams[1] = new SqlParameter("@P_PNAME", objStock.PNAME);
                        objParams[2] = new SqlParameter("@P_PCNO", objStock.PCNO);
                        objParams[3] = new SqlParameter("@P_ADDRESS", objStock.ADDRESS);
                        objParams[4] = new SqlParameter("@P_CITYNO", objStock.CITYNO);
                        objParams[5] = new SqlParameter("@P_STATENO", objStock.STATENO);
                        objParams[6] = new SqlParameter("@P_PHONE", objStock.PHONE);
                        objParams[7] = new SqlParameter("@P_EMAIL", objStock.EMAIL);
                        objParams[8] = new SqlParameter("@P_CST", objStock.CST);
                        objParams[9] = new SqlParameter("@P_BST", objStock.BST);
                        objParams[10] = new SqlParameter("@P_USER_ID", userid);
                        objParams[11] = new SqlParameter("@P_REMARK", objStock.REMARK);
                        objParams[12] = new SqlParameter("@P_COLLEGE_CODE", objStock.COLLEGE_CODE);
                        objParams[13] = new SqlParameter("@P_PNO", SqlDbType.Int);
                        objParams[13].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_PARTY_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StockMaintnance.AddParty-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateParty(StockMaster objStock, string userid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Update Party                       
                        objParams = new SqlParameter[14];
                        objParams[0] = new SqlParameter("@P_PNO", objStock.PNO);
                        objParams[1] = new SqlParameter("@P_PCODE", objStock.PCODE);
                        objParams[2] = new SqlParameter("@P_PNAME", objStock.PNAME);
                        objParams[3] = new SqlParameter("@P_PCNO", objStock.PCNO);
                        objParams[4] = new SqlParameter("@P_ADDRESS", objStock.ADDRESS);
                        objParams[5] = new SqlParameter("@P_CITYNO", objStock.CITYNO);
                        objParams[6] = new SqlParameter("@P_STATENO", objStock.STATENO);
                        objParams[7] = new SqlParameter("@P_PHONE", objStock.PHONE);
                        objParams[8] = new SqlParameter("@P_EMAIL", objStock.EMAIL);
                        objParams[9] = new SqlParameter("@P_CST", objStock.CST);
                        objParams[10] = new SqlParameter("@P_BST", objStock.BST);
                        objParams[11] = new SqlParameter("@P_USER_ID", userid);
                        objParams[12] = new SqlParameter("@P_REMARK", objStock.REMARK);
                        objParams[13] = new SqlParameter("@P_COLLEGE_CODE", objStock.COLLEGE_CODE);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_PARTY_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StockMaintnance.UpdateParty-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllParty(string colcode)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COLLEGE_CODE", colcode);                      

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_PARTY_GET_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StockMaintnance.GetAllParty-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSingleRecordParty(int pNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PNO", pNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_PARTY_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StockMaintnance.GetSingleRecordParty-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GenratePartyCode(int MDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PCNO", MDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_GENERATE_VENDORCODE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StockMaintnance_GeneratevendorCode-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #region HEALTH_MAIN_ITEM_GROUP

                public int AddMainItemGroup(string mainItemGrpName, string Sname, string collegeCode, string userid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New ITEM_MASTER
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_MIGNAME", mainItemGrpName);
                        objParams[1] = new SqlParameter("@P_SNAME", Sname);

                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[3] = new SqlParameter("@P_USER_ID", userid);
                        objParams[4] = new SqlParameter("@P_MIGNO", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_MAIN_ITEM_GROUP_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }


                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.RecordExist);

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StockMaintnance.AddItemMaster-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateMainItemGroup(string mainItemGrpName, string Sname, string collegeCode, int mainItemGprNo, string userid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New ITEM_MASTER
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_MIGNAME", mainItemGrpName);
                        objParams[1] = new SqlParameter("@P_SNAME", Sname);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[3] = new SqlParameter("@P_MIGNO", mainItemGprNo);
                        objParams[4] = new SqlParameter("@P_USER_ID", userid);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_MAIN_ITEM_GROUP_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StockMaintnance.UpdateMainItemGroup-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //public DataSet GetAllMainItemGroup()
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = new SqlParameter[0];

                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_MAIN_ITEM_GROUP_GET_ALL", objParams);

                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.GetAllMainItemGroup-> " + ex.ToString());
                //    }
                //    return ds;
                //}

                //public DataSet GetSingleRecordMainItemGroup(int mainItemGprNo)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null; ;
                //        objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("@P_MIGNO", mainItemGprNo);
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_MAIN_ITEM_GROUP_GET_NO", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.GetSingleRecordMainItemGroup-> " + ex.ToString());
                //    }
                //    return ds;
                //}

                #endregion

                #region HEALTH_MAIN_ITEM_SUBGROUP

                public int AddMainSubItemGroup(string mainSubItemGrpName, string Sname, int mainItemGprNo, string collegeCode, string userid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New ITEM_MASTER
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_MISGNAME", mainSubItemGrpName);
                        objParams[1] = new SqlParameter("@P_SNAME", Sname);
                        objParams[2] = new SqlParameter("@P_MIGNO", mainItemGprNo);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[4] = new SqlParameter("@P_USER_ID", userid);
                        objParams[5] = new SqlParameter("@P_MISGNO", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_MAIN_SUB_ITEM_GROUP_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StockMaintnance.AddMainSubItemGroup-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateMainSubItemGroup(string mainSubItemGrpName, string Sname, int mainItemGprNo, string collegeCode, int mainSubItemGprNo, string userid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New ITEM_MASTER
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_MISGNAME", mainSubItemGrpName);
                        objParams[1] = new SqlParameter("@P_SNAME", Sname);
                        objParams[2] = new SqlParameter("@P_MIGNO", mainItemGprNo);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[4] = new SqlParameter("@P_MISGNO", mainSubItemGprNo);
                        objParams[5] = new SqlParameter("@P_USER_ID", userid);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_MAIN_SUB_ITEM_GROUP_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StockMaintnance.UpdateMainSubItemGroup-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //public DataSet GetAllMainSubItemGroup()
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = new SqlParameter[0];

                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_MAIN_ITEM_SUB_GROUP_GET_ALL", objParams);

                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.GetAllMainSubItemGroup-> " + ex.ToString());
                //    }
                //    return ds;
                //}

                //public DataSet GetSingleRecordMainSubItemGroup(int mainSubItemGprNo)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null; ;
                //        objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("@P_MISGNO", mainSubItemGprNo);
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_MAIN_ITEM_SUB_GROUP_GET_BY_NO", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.GetSingleRecordMainSubItemGroup-> " + ex.ToString());
                //    }
                //    return ds;
                //}
                #endregion

                #region HEALTH_ITEM_MASTER

                public int AddItemMaster(StockMaster objStock, string userid, int itpno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New ITEM_MASTER
                        objParams = new SqlParameter[16];
                        objParams[0] = new SqlParameter("@P_ITEM_CODE", objStock.ITEM_CODE);
                        objParams[1] = new SqlParameter("@P_ITEM_NAME", objStock.ITEM_NAME);
                        objParams[2] = new SqlParameter("@P_ITEM_DETAILS", objStock.ITEM_DETAILS);
                        objParams[3] = new SqlParameter("@P_MIGNO", objStock.MIGNO);
                        objParams[4] = new SqlParameter("@P_MISGNO", objStock.MISGNO);
                        objParams[5] = new SqlParameter("@P_ITEM_UNIT", objStock.ITEM_UNIT);
                        objParams[6] = new SqlParameter("@P_ITEM_REORDER_QTY", objStock.ITEM_REORDER_QTY); 
                        objParams[7] = new SqlParameter("@P_ITEM_MIN_QTY", objStock.ITEM_MIN_QTY);
                        objParams[8] = new SqlParameter("@P_ITEM_MAX_QTY", objStock.ITEM_MAX_QTY);                     
                        objParams[9] = new SqlParameter("@P_ITEM_CUR_QTY", objStock.ITEM_CUR_QTY);
                        objParams[10] = new SqlParameter("@P_ITEM_OB_QTY", objStock.ITEM_OB_QTY);
                        objParams[11] = new SqlParameter("@P_ITEM_OB_VALUE", objStock.ITEM_OB_VALUE);       
                        objParams[12] = new SqlParameter("@P_COLLEGE_CODE", objStock.COLLEGE_CODE);
                        objParams[13] = new SqlParameter("@P_USER_ID", userid);
                        objParams[14] = new SqlParameter("@P_ITPNO", itpno);                     
                        objParams[15] = new SqlParameter("@P_ITEM_NO", SqlDbType.Int);
                        objParams[15].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_ITEM_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StockMaintnance.AddItemMaster-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateItemMaster(StockMaster objStock, string userid, int itpno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Update New Item_Master
                        objParams = new SqlParameter[18];
                        objParams[0] = new SqlParameter("@P_ITEM_NO", objStock.ITEM_NO);
                        objParams[1] = new SqlParameter("@P_ITEM_CODE", objStock.ITEM_CODE);
                        objParams[2] = new SqlParameter("@P_ITEM_NAME", objStock.ITEM_NAME);
                        objParams[3] = new SqlParameter("@P_ITEM_DETAILS", objStock.ITEM_DETAILS);
                        objParams[4] = new SqlParameter("@P_MIGNO", objStock.MIGNO);
                        objParams[5] = new SqlParameter("@P_MISGNO", objStock.MISGNO);
                        objParams[6] = new SqlParameter("@P_ITEM_UNIT", objStock.ITEM_UNIT);
                        objParams[7] = new SqlParameter("@P_ITEM_REORDER_QTY", objStock.ITEM_REORDER_QTY);
                        objParams[8] = new SqlParameter("@P_ITEM_MIN_QTY", objStock.ITEM_MIN_QTY);
                        objParams[9] = new SqlParameter("@P_ITEM_MAX_QTY", objStock.ITEM_MAX_QTY);
                        objParams[10] = new SqlParameter("@P_ITEM_BUD_QTY", objStock.ITEM_BUD_QTY);
                        objParams[11] = new SqlParameter("@P_ITEM_CUR_QTY", objStock.ITEM_CUR_QTY);
                        objParams[12] = new SqlParameter("@P_ITEM_OB_QTY", objStock.ITEM_OB_QTY);
                        objParams[13] = new SqlParameter("@P_ITEM_OB_VALUE", objStock.ITEM_OB_VALUE);
                        objParams[14] = new SqlParameter("@P_COLLEGE_CODE", objStock.COLLEGE_CODE);
                        objParams[15] = new SqlParameter("@P_USER_ID", userid);
                        objParams[16] = new SqlParameter("@P_ITPNO", itpno);
                        objParams[17] = new SqlParameter("@P_APPROVAL", objStock.ITEM_APPROVAL);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_ITEM_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StockMaintnance.UpdateItemMaster-> " + ex.ToString());
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

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_ITEM_GET_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StockMaintnance.GetAllItemMaster-> " + ex.ToString());
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
                        objParams[0] = new SqlParameter("@P_ITEM_NO", itemNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_ITEM_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StockMaintnance.GetSingleRecordItemMaster-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #region Invoice Entry
                public DataSet GenrateInvoiceNo(int MDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_MDNO", MDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_GENERATE_INVOICENO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StockMaintnance.GenerateInvoiceNo-> " + ex.ToString());
                    }
                    return ds;
                }

                //public DataSet GetAllTaxes()
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        ds = objMaster.GetAllTax();
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StockMaintnance_GetAllTaxes-> " + ex.ToString());
                //    }
                //    return ds;
                //}
                public DataSet GetAllTax()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_TAX_GET_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetAllTax-> " + ex.ToString());
                    }
                    return ds;

                }

                public DataSet GetAllItemsByInvoice(int ITEM_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; 
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_INVTRNO", ITEM_NO);
                        //objParams[1] = new SqlParameter("@P_ITEMNO", ITEM_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_INVOICE_ITEM_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StockMaintnance_GetAllItemsByInvoice-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAllItemList()
                {

                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_STOCK_SUMMARY_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StockMaintnance.GetAllItemMaster-> " + ex.ToString());
                    }
                    return ds;
                }

                #region issuemedicnie
                public DataSet GetPatientDetails(int serchtype, string serchtext)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SERCHTEXT", serchtext);
                        objParams[1] = new SqlParameter("@P_SERCHTYPE", serchtype);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_PRESCRIPTION_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StockMaintnance.GetPatientDetails-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion
               

                public int SaveInovoiceEntry(StockMaster objStock, string colcode, string accstatus)
                { 
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[32];
                        objParams[0] = new SqlParameter("@P_INVOICE_NO", objStock.INVOICE_NO);
                        objParams[1] = new SqlParameter("@P_INVOICE_DATE", objStock.INVOICE_DATE);
                        objParams[2] = new SqlParameter("@P_D_M_NO ", objStock.D_M_NO);
                        objParams[3] = new SqlParameter("@P_D_M_DATE", objStock.D_M_DATE);
                        objParams[4] = new SqlParameter("@P_VENDOR_NO", objStock.VENDOR_NO);
                        objParams[5] = new SqlParameter("@P_RECEIVE_DATE", objStock.RECEIVE_DATE);
                        objParams[6] = new SqlParameter("@P_PURCHASE_ORDER_NO", objStock.PURCHASE_ORDER_NO);
                        objParams[7] = new SqlParameter("@P_PURCHASE_ORDER_DATE", objStock.PURCHASE_ORDER_DATE);
                        objParams[8] = new SqlParameter("@P_ITEM_TOT_QTY", objStock.ITEMTOTQTY);
                        objParams[9] = new SqlParameter("@P_GRAMT", objStock.GRAMT);
                        objParams[10] = new SqlParameter("@P_FLAG1", objStock.FLAG1);
                        objParams[11] = new SqlParameter("@P_FLAG2", objStock.FLAG2);
                        objParams[12] = new SqlParameter("@P_FLAG3", objStock.FLAG3);
                        objParams[13] = new SqlParameter("@P_FLAG4", objStock.FLAG4);
                        objParams[14] = new SqlParameter("@P_ECHG1", objStock.ECHG1);
                        objParams[15] = new SqlParameter("@P_ECHG2", objStock.ECHG2);
                        objParams[16] = new SqlParameter("@P_ECHG3", objStock.ECHG3);
                        objParams[17] = new SqlParameter("@P_ECHG4", objStock.ECHG4);
                        objParams[18] = new SqlParameter("@P_EP1", objStock.EP1);
                        objParams[19] = new SqlParameter("@P_EP2", objStock.EP2);
                        objParams[20] = new SqlParameter("@P_EP3", objStock.EP3);
                        objParams[21] = new SqlParameter("@P_EP4", objStock.EP4);
                        objParams[22] = new SqlParameter("@P_EAMT1", objStock.EAMT1);
                        objParams[23] = new SqlParameter("@P_EAMT2", objStock.EAMT2);
                        objParams[24] = new SqlParameter("@P_EAMT3", objStock.EAMT3);
                        objParams[25] = new SqlParameter("@P_EAMT4", objStock.EAMT4);
                        objParams[26] = new SqlParameter("@P_COLLEGE_CODE", colcode);
                        objParams[27] = new SqlParameter("@P_NETAMT", objStock.NETAMT);
                        objParams[28] = new SqlParameter("@P_REMARK", objStock.REMARK);
                        objParams[29] = new SqlParameter("@P_ITEM_LIST_TBL", objStock.ITEMLIST);
                        objParams[30] = new SqlParameter("@P_INVTRNO", objStock.INVTRNO);                         
                        objParams[31] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[31].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_INVOICE_INSERT_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == 2627)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else if (Convert.ToInt32(ret) == 2)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else 
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.HEALTH.BusinessLayer.BusinessLogic.LabController.AddUpdateTestContent->" + ex.ToString());
                    }
                    return retstatus;
                   // return (int)CustomStatus.RecordUpdated;

                }
                public int UpdateInovoiceEntry(StockMaster objStock, List<Health_Invoice_Item> ListInvItem, string colcode)
                {
                    //try
                    //{
                        DAC.Commands.Clear();
                        //Delete All Item And Taxes
                        DAC dcItemDel = new DAC();
                        dcItemDel.StoredProcedure = "PKG_HEALTH_INVOICE_ITEM_DELETE";
                        dcItemDel.Params.Add("@P_INVTRNO", SqlDbType.Int, objStock.INVTRNO);
                        DAC.Commands.Add(dcItemDel);


                        //Save Invoice.
                        DAC dc = new DAC();
                        dc.StoredProcedure = "PKG_HEALTH_INVOICE_UPDATE";
                        //dc.Params.Add("@P_INVOICE_NO ", SqlDbType.NVarChar, objStock.INVOICE_NO);
                        dc.Params.Add("@P_INVOICE_DATE", SqlDbType.DateTime, objStock.INVOICE_DATE);
                        dc.Params.Add("@P_D_M_NO", SqlDbType.NVarChar, objStock.D_M_NO);
                        dc.Params.Add("@P_D_M_DATE", SqlDbType.DateTime, objStock.D_M_DATE);
                        dc.Params.Add("@P_VENDOR_NO", SqlDbType.Int, objStock.VENDOR_NO);
                        dc.Params.Add("@P_RECEIVE_DATE", SqlDbType.DateTime, objStock.RECEIVE_DATE);
                        dc.Params.Add("@P_PURCHASE_ORDER_NO", SqlDbType.Int, objStock.PURCHASE_ORDER_NO);
                        dc.Params.Add("@P_PURCHASE_ORDER_DATE", SqlDbType.DateTime, objStock.PURCHASE_ORDER_DATE);

                        dc.Params.Add("@P_GRAMT", SqlDbType.Money, objStock.GRAMT);
                        dc.Params.Add("@P_ITEM_TOT_QTY", SqlDbType.Int, objStock.ITEMTOTQTY);

                        dc.Params.Add("@P_FLAG1", SqlDbType.Bit, objStock.FLAG1);
                        dc.Params.Add("@P_FLAG2", SqlDbType.Bit, objStock.FLAG2);
                        dc.Params.Add("@P_FLAG3", SqlDbType.Bit, objStock.FLAG3);
                        dc.Params.Add("@P_FLAG4", SqlDbType.Bit, objStock.FLAG4);
                        dc.Params.Add("@P_ECHG1", SqlDbType.NVarChar, objStock.ECHG1);
                        dc.Params.Add("@P_ECHG2", SqlDbType.NVarChar, objStock.ECHG2);
                        dc.Params.Add("@P_ECHG3", SqlDbType.NVarChar, objStock.ECHG3);
                        dc.Params.Add("@P_ECHG4", SqlDbType.NVarChar, objStock.ECHG4);
                        dc.Params.Add("@P_EP1", SqlDbType.Decimal, objStock.EP1);
                        dc.Params.Add("@P_EP2", SqlDbType.Decimal, objStock.EP2);
                        dc.Params.Add("@P_EP3", SqlDbType.Decimal, objStock.EP3);
                        dc.Params.Add("@P_EP4", SqlDbType.Decimal, objStock.EP4);
                        dc.Params.Add("@P_EAMT1", SqlDbType.Money, objStock.EAMT1);
                        dc.Params.Add("@P_EAMT2", SqlDbType.Money, objStock.EAMT2);
                        dc.Params.Add("@P_EAMT3", SqlDbType.Money, objStock.EAMT3);
                        dc.Params.Add("@P_EAMT4", SqlDbType.Money, objStock.EAMT4);

                        dc.Params.Add("@P_COLLEGE_CODE", SqlDbType.NVarChar, colcode);
                        dc.Params.Add("@P_NETAMT", SqlDbType.Money, objStock.NETAMT);
                        dc.Params.Add("@P_REMARK", SqlDbType.NVarChar, objStock.REMARK);

                        dc.Params.Add("@P_INVTRNO", SqlDbType.Int, objStock.INVTRNO);


                        DAC.Commands.Add(dc);
                        //Save Invoice Item.
                        foreach (Health_Invoice_Item Item in ListInvItem)
                        {
                            DAC dcItem = new DAC();
                            dcItem.StoredProcedure = "PKG_HEALTH_INVOICE_ITEM_INSERT";
                            dcItem.Params.Add("@P_INVINO", SqlDbType.Int, 0);
                            dcItem.Params.Add("@P_INVTRNO", SqlDbType.Int, Item.INVTRNO);
                            dcItem.Params.Add("@P_ITEM_NO", SqlDbType.Int, Item.ITEM_NO);
                            dcItem.Params.Add("@P_UNIT", SqlDbType.Decimal, Item.UNIT);
                            dcItem.Params.Add("@P_QTY", SqlDbType.Decimal, Item.QTY);
                            dcItem.Params.Add("@P_RATE", SqlDbType.Decimal, Item.RATE);
                            dcItem.Params.Add("@P_AMT", SqlDbType.Decimal, Item.AMT);
                            dcItem.Params.Add("@P_COLLEGE_CODE", SqlDbType.Decimal, colcode);
                            dcItem.Params.Add("@P_ACCSTATUS  ", SqlDbType.NVarChar, 'N');
                            dcItem.Params.Add("@P_TOTQTY", SqlDbType.Decimal, Item.TOTQTY);
                            dcItem.Params.Add("@P_BATCH_NO", SqlDbType.Text, Item.BATCH_NO);
                            dcItem.Params.Add("@P_EXPIRY_DATE", SqlDbType.Text, Item.EXPIRY_DATE);
                            dcItem.Params.Add("@P_MFG_DATE", SqlDbType.DateTime, Item.MFG_DATE);

                            DAC.Commands.Add(dcItem);
                        }
                         DAC.ExecuteBatch();
                        DAC.Commands.Clear();
                        return (int)CustomStatus.RecordUpdated;
                    //}
                    //catch(Exception ex)
                    //{
                    //    ex.Message.ToString();
                    //}

                }
                public int GetInVoiceTRNO()
                {
                    SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                    // string MAXNO= objSQLHelper.ExecuteScalar("SELECT ISNULL((SELECT MAX(INVTRNO) FROM STORE_INVOICE),0)").ToString();
                    string MAXNO = objComman.LookUp("HEALTH_INVOICE", "COUNT(1)", "");
                    if (MAXNO.Trim() == "")
                    {
                        return 0;
                    }
                    else
                    {
                        int MAX = Convert.ToInt32(MAXNO) + 1;
                        return Convert.ToInt32(MAX);
                    }
                }
                public DataSet GetInvoiceByNo(int INVTRNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_INVTRNO", INVTRNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_INVOICE_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StockMaintnance_GetInvoiceByNo-> " + ex.ToString());
                    }
                    return ds;
                }

              

                public DataSet GetAllTaxeByInvoice(int INVTRNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_INVTRNO", INVTRNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_INVOICE_TAX_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StockMaintnance_GetInvoiceByNo-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetAllItemByPO(int PORDNO)
                {
                    DataSet ds = null;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PORDNO", PORDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_ITEM_GET_BY_PO_FOR_INVOICE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StockMaintnance_GetAllItemByPO-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetAllTaxByPO(int PORDNO)
                {
                    DataSet ds = null;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PORDNO", PORDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_FIELD_GET_BY_PO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StockMaintnance_GetAllTaxByPO-> " + ex.ToString());
                    }
                    return ds;
                }

                //public int SaveInvoice_Item_Entry(StockMaster objStock,string colcode)
                //{
                //    DAC.Commands.Clear();
                //    DAC dcItem = new DAC();
                //    dcItem.StoredProcedure = "PKG_HEALTH_INVOICE_ITEM_INSERT";

                //    dcItem.Params.Add("@P_INVINO", SqlDbType.Int, 0);
                //    dcItem.Params.Add("@P_INVTRNO", SqlDbType.Int, objStock.INVOICE_NO);
                //    dcItem.Params.Add("@P_ITEM_NO", SqlDbType.Int, objStock.ITEM_NO);
                //    dcItem.Params.Add("@P_UNIT", SqlDbType.Decimal, objStock.UNIT);
                //    dcItem.Params.Add("@P_QTY", SqlDbType.Decimal, objStock.ITEM_CUR_QTY);
                //    dcItem.Params.Add("@P_RATE", SqlDbType.Decimal, objStock.ITEM_RATE);
                //    dcItem.Params.Add("@P_AMT", SqlDbType.Decimal, objStock.ITEM_AMT);

                //    dcItem.Params.Add("@P_TOTQTY", SqlDbType.Decimal, objStock.ITEM_MAX_QTY);
                //    dcItem.Params.Add("@P_BATCH_NO", SqlDbType.NVarChar, objStock.ITEM_BATCHNO);
                //    dcItem.Params.Add("@P_EXPIRY_DATE", SqlDbType.DateTime, objStock.EXP_DATE);
                //    dcItem.Params.Add("@P_MFG_DATE", SqlDbType.DateTime, objStock.MFG_DATE );
                //    dcItem.Params.Add("@P_COLLEGE_CODE", SqlDbType.NVarChar, colcode);
                //    DAC.Commands.Add(dcItem);
                //    DAC.ExecuteBatch();
                //    DAC.Commands.Clear();
                //    return (int)CustomStatus.RecordSaved;
                //}
               
               //public int SaveInvoice_Item_Entry(StockMaster objStock, string colcode)
               // {
               //     int retStatus = Convert.ToInt32(CustomStatus.Others);

               //     try
               //     {
               //         SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
               //         SqlParameter[] objParams = null;

               //         objParams = new SqlParameter[12];
               //         objParams[0] = new SqlParameter("@P_INVTRNO", objStock.INVOICE_NO);
               //         objParams[1] = new SqlParameter("@P_ITEM_NO", objStock.ITEM_NO);
               //         objParams[2] = new SqlParameter("@P_UNIT", objStock.UNIT);
               //         objParams[3] = new SqlParameter("@P_QTY", objStock.ITEM_CUR_QTY);
               //         objParams[4] = new SqlParameter("@P_RATE", objStock.ITEM_RATE);
               //         objParams[5] = new SqlParameter("@P_AMT", objStock.ITEM_AMT);
               //         objParams[6] = new SqlParameter("@P_TOTQTY", objStock.ITEM_MAX_QTY);
               //         objParams[7] = new SqlParameter("@P_BATCH_NO",objStock.ITEM_BATCHNO);
               //         objParams[8] = new SqlParameter("@P_EXPIRY_DATE", objStock.EXP_DATE);
               //         objParams[9] = new SqlParameter("@P_MFG_DATE", objStock.MFG_DATE);
               //         objParams[10] = new SqlParameter("@P_COLLEGE_CODE", colcode);
               //         objParams[11] = new SqlParameter("@P_INVINO", SqlDbType.Int, 0);
               //         objParams[11].Direction = ParameterDirection.Output;

               //         retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_INVOICE_ITEM_INSERT", objParams, true));
               //     }
               //     catch (Exception ex)
               //     {
               //         retStatus = Convert.ToInt32(CustomStatus.Error);
               //         throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.AddAbsentStudentsRecord-> " + ex.ToString());
               //     }

               //     return retStatus;
               // }
               //public DataSet GetItemEntry_ByInvoice(int inv_no)
               //{
               //    DataSet ds = null;
               //    try
               //    {
               //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
               //        SqlParameter[] objParams = null; ;
               //        objParams = new SqlParameter[1];
               //        objParams[0] = new SqlParameter("@P_INVOICE_NO", inv_no);
                     
               //        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_INVOICE_ITEM_ENTRY", objParams);
               //    }
               //    catch (Exception ex)
               //    {
               //        return ds;
               //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StockMaintnance.GenerateInvoiceNo-> " + ex.ToString());
               //    }
               //    return ds;
               //}
                public DataSet GetAllInvoice()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_GET_ALL_INVOICE", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetAllTax-> " + ex.ToString());
                    }
                    return ds;

                }

                #endregion

                #region Manufacturer
                public int AddManufacturer(StockMaster obj)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_MCODE", obj.MCODE);
                        objParams[1] = new SqlParameter("@P_MNAME", obj.MNAME);
                        objParams[2] = new SqlParameter("@P_ADDRESS", obj.ADDRESS);                       
                        objParams[3] = new SqlParameter("@P_CONT_PERSON", obj.CONT_PERSON);
                        objParams[4] = new SqlParameter("@P_PHONE", obj.PHONE);
                        objParams[5] = new SqlParameter("@P_EMAIL", obj.EMAIL);
                        objParams[6] = new SqlParameter("@P_REMARK", obj.REMARK);
                        objParams[7] = new SqlParameter("@P_USER_ID", obj.USERID);
                        objParams[8] = new SqlParameter("@P_MNO", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_MANUFACTURER_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {

                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.HEALTH.BusinessLayer.BusinessLogic.StockMaintnance.AddManufacturer->" + ex.ToString());
                    }
                    return retstatus;
                }

                public int UpdateManufacturer(StockMaster obj)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_MCODE", obj.MCODE);
                        objParams[1] = new SqlParameter("@P_MNAME", obj.MNAME);
                        objParams[2] = new SqlParameter("@P_ADDRESS", obj.ADDRESS);                       
                        objParams[3] = new SqlParameter("@P_CONT_PERSON", obj.CONT_PERSON);
                        objParams[4] = new SqlParameter("@P_PHONE", obj.PHONE);
                        objParams[5] = new SqlParameter("@P_EMAIL", obj.EMAIL);
                        objParams[6] = new SqlParameter("@P_REMARK", obj.REMARK);
                        objParams[7] = new SqlParameter("@P_USER_ID", obj.USERID);
                        objParams[8] = new SqlParameter("@P_MNO", obj.MNO);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_MANUFACTURER_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.HEALTH.BusinessLayer.BusinessLogic.StockMaintnance.UpdateManufacturer->" + ex.ToString());
                    }
                    return retstatus;
                }

                #endregion

                

                #region Issue Medicine
                //public DataSet GetPatientDetails(int serchtype, string serchtext)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null; ;
                //        objParams = new SqlParameter[2];
                //        objParams[0] = new SqlParameter("@P_SERCHTEXT", serchtext);
                //        objParams[1] = new SqlParameter("@P_SERCHTYPE", serchtype);
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_PRESCRIPTION_DETAILS", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StockMaintnance.GetPatientDetails-> " + ex.ToString());
                //    }
                //    return ds;
                //}
                #endregion



            }
        }
    }
}