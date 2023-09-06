//===============================================================
//PROJECT NAME  : UAIMS
//MODULE NAME   : SPORTS
//CREATED BY    : MRUNAL SINGH
//CREATION DATE : 18-MAY-2017   
//MODIFY BY     : 
//MODIFIED DATE :    
//================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.DAC;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class KitController
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;                
                
                Common objComman = new Common();

                #region Party_Category

                public int AddPartyCategory(string categoryName, string collegeCode, string userid, string shortname)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_PCNAME", categoryName);
                        objParams[4] = new SqlParameter("@P_PCSHORTNAME", shortname);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[2] = new SqlParameter("@P_USER_ID", userid);
                        objParams[3] = new SqlParameter("@P_PCNO", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PR_SPRT_PARTY_CATEGORY_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.KitController.AddParyCategory-> " + ex.ToString());
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
                     
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_PCNO", categoryNo);
                        objParams[1] = new SqlParameter("@P_PCNAME", categoryName);
                        objParams[4] = new SqlParameter("@P_PCSHORTNAME", shortname);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[3] = new SqlParameter("@P_USER_ID", userid);
                        if (objSQLHelper.ExecuteNonQuerySP("PR_SPRT_PARTY_CATEGORY_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.KitController.UpdateParyCategory-> " + ex.ToString());
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

                        ds = objSQLHelper.ExecuteDataSetSP("PR_SPRT_PARTY_CATEGORY_GET_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.KitController.GetAllParyCategory-> " + ex.ToString());
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
                        ds = objSQLHelper.ExecuteDataSetSP("PR_SPRT_PARTY_CATEGORY_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.KitController.GetSingleRecordParyCategory-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #region Party

                public int AddParty(EventKitEnt objEK, string userid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                     
                        objParams = new SqlParameter[16];
                        objParams[0] = new SqlParameter("@P_PCODE", objEK.PCODE);
                        objParams[1] = new SqlParameter("@P_PNAME", objEK.PNAME);
                        objParams[2] = new SqlParameter("@P_PCNO", objEK.PCNO);
                        objParams[3] = new SqlParameter("@P_ADDRESS", objEK.ADDRESS);
                        objParams[4] = new SqlParameter("@P_CITYNO", objEK.CITYNO);
                        objParams[5] = new SqlParameter("@P_STATENO", objEK.STATENO);
                        objParams[6] = new SqlParameter("@P_PHONE", objEK.PHONE);
                        objParams[7] = new SqlParameter("@P_EMAIL", objEK.EMAIL);
                        objParams[8] = new SqlParameter("@P_CST", objEK.CST);
                        objParams[9] = new SqlParameter("@P_BST", objEK.BST);
                        objParams[10] = new SqlParameter("@P_USER_ID", userid);
                        objParams[11] = new SqlParameter("@P_REMARK", objEK.REMARK);
                        objParams[12] = new SqlParameter("@P_COLLEGE_CODE", objEK.COLLEGE_CODE);
                        objParams[13] = new SqlParameter("@P_GST", objEK.GST);
                        objParams[14] = new SqlParameter("@P_PANNO", objEK.PANNO);
                        objParams[15] = new SqlParameter("@P_PNO", SqlDbType.Int);
                        objParams[15].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PR_SPRT_PARTY_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.KitController.AddParty-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateParty(EventKitEnt objEK, string userid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                                       
                        objParams = new SqlParameter[16];
                        objParams[0] = new SqlParameter("@P_PNO", objEK.PNO);
                        objParams[1] = new SqlParameter("@P_PCODE", objEK.PCODE);
                        objParams[2] = new SqlParameter("@P_PNAME", objEK.PNAME);
                        objParams[3] = new SqlParameter("@P_PCNO", objEK.PCNO);
                        objParams[4] = new SqlParameter("@P_ADDRESS", objEK.ADDRESS);
                        objParams[5] = new SqlParameter("@P_CITYNO", objEK.CITYNO);
                        objParams[6] = new SqlParameter("@P_STATENO", objEK.STATENO);
                        objParams[7] = new SqlParameter("@P_PHONE", objEK.PHONE);
                        objParams[8] = new SqlParameter("@P_EMAIL", objEK.EMAIL);
                        objParams[9] = new SqlParameter("@P_CST", objEK.CST);
                        objParams[10] = new SqlParameter("@P_BST", objEK.BST);
                        objParams[11] = new SqlParameter("@P_USER_ID", userid);
                        objParams[12] = new SqlParameter("@P_REMARK", objEK.REMARK);
                        objParams[13] = new SqlParameter("@P_COLLEGE_CODE", objEK.COLLEGE_CODE);
                        objParams[14] = new SqlParameter("@P_GST", objEK.GST);
                        objParams[15] = new SqlParameter("@P_PANNO", objEK.PANNO);

                        if (objSQLHelper.ExecuteNonQuerySP("PR_SPRT_PARTY_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.KitController.UpdateParty-> " + ex.ToString());
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

                        ds = objSQLHelper.ExecuteDataSetSP("PR_SPRT_PARTY_GET_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.KitController.GetAllParty-> " + ex.ToString());
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
                        ds = objSQLHelper.ExecuteDataSetSP("PR_SPRT_PARTY_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.KitController.GetSingleRecordParty-> " + ex.ToString());
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
                        ds = objSQLHelper.ExecuteDataSetSP("PR_SPRT_GENERATE_VENDORCODE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.KitController.GeneratevendorCode-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion


                #region Item Group

                public int AddMainItemGroup(string mainItemGrpName, string Sname, string collegeCode, string userid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                       
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_MIGNAME", mainItemGrpName);
                        objParams[1] = new SqlParameter("@P_SNAME", Sname);

                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[3] = new SqlParameter("@P_USER_ID", userid);
                        objParams[4] = new SqlParameter("@P_MIGNO", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PR_SPRT_MAIN_ITEM_GROUP_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }


                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.RecordExist);

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.KitController.AddItemMaster-> " + ex.ToString());
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
                      
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_MIGNAME", mainItemGrpName);
                        objParams[1] = new SqlParameter("@P_SNAME", Sname);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[3] = new SqlParameter("@P_MIGNO", mainItemGprNo);
                        objParams[4] = new SqlParameter("@P_USER_ID", userid);
                        if (objSQLHelper.ExecuteNonQuerySP("PR_SPRT_MAIN_ITEM_GROUP_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.KitController.UpdateMainItemGroup-> " + ex.ToString());
                    }
                    return retStatus;
                }           

                #endregion


                #region Item Sub Group

                public int AddMainSubItemGroup(string mainSubItemGrpName, string Sname, int mainItemGprNo, string collegeCode, string userid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_MISGNAME", mainSubItemGrpName);
                        objParams[1] = new SqlParameter("@P_SNAME", Sname);
                        objParams[2] = new SqlParameter("@P_MIGNO", mainItemGprNo);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[4] = new SqlParameter("@P_USER_ID", userid);
                        objParams[5] = new SqlParameter("@P_MISGNO", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PR_SPRT_MAIN_SUB_ITEM_GROUP_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.KitController.AddMainSubItemGroup-> " + ex.ToString());
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
                        
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_MISGNAME", mainSubItemGrpName);
                        objParams[1] = new SqlParameter("@P_SNAME", Sname);
                        objParams[2] = new SqlParameter("@P_MIGNO", mainItemGprNo);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[4] = new SqlParameter("@P_MISGNO", mainSubItemGprNo);
                        objParams[5] = new SqlParameter("@P_USER_ID", userid);
                        if (objSQLHelper.ExecuteNonQuerySP("PR_SPRT_MAIN_SUB_ITEM_GROUP_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.KitController.UpdateMainSubItemGroup-> " + ex.ToString());
                    }
                    return retStatus;
                }             
                #endregion


                #region Item Master

                public int AddItemMaster(EventKitEnt objEK, string userid, int itpno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                      
                        objParams = new SqlParameter[16];
                        objParams[0] = new SqlParameter("@P_ITEM_CODE", objEK.ITEM_CODE);
                        objParams[1] = new SqlParameter("@P_ITEM_NAME", objEK.ITEM_NAME);
                        objParams[2] = new SqlParameter("@P_ITEM_DETAILS", objEK.ITEM_DETAILS);
                        objParams[3] = new SqlParameter("@P_MIGNO", objEK.MIGNO);
                        objParams[4] = new SqlParameter("@P_MISGNO", objEK.MISGNO);
                        objParams[5] = new SqlParameter("@P_ITEM_UNIT", objEK.ITEM_UNIT);
                        objParams[6] = new SqlParameter("@P_ITEM_REORDER_QTY", objEK.ITEM_REORDER_QTY);
                        objParams[7] = new SqlParameter("@P_ITEM_MIN_QTY", objEK.ITEM_MIN_QTY);
                        objParams[8] = new SqlParameter("@P_ITEM_MAX_QTY", objEK.ITEM_MAX_QTY);
                        objParams[9] = new SqlParameter("@P_ITEM_CUR_QTY", objEK.ITEM_CUR_QTY);
                        objParams[10] = new SqlParameter("@P_ITEM_OB_QTY", objEK.ITEM_OB_QTY);
                        objParams[11] = new SqlParameter("@P_ITEM_OB_VALUE", objEK.ITEM_OB_VALUE);
                        objParams[12] = new SqlParameter("@P_COLLEGE_CODE", objEK.COLLEGE_CODE);
                        objParams[13] = new SqlParameter("@P_USER_ID", userid);
                        objParams[14] = new SqlParameter("@P_ITPNO", itpno);
                        objParams[15] = new SqlParameter("@P_ITEM_NO", SqlDbType.Int);
                        objParams[15].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PR_SPRT_ITEM_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.KitController.AddItemMaster-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateItemMaster(EventKitEnt objEK, string userid, int itpno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                      
                        objParams = new SqlParameter[18];
                        objParams[0] = new SqlParameter("@P_ITEM_NO", objEK.ITEM_NO);
                        objParams[1] = new SqlParameter("@P_ITEM_CODE", objEK.ITEM_CODE);
                        objParams[2] = new SqlParameter("@P_ITEM_NAME", objEK.ITEM_NAME);
                        objParams[3] = new SqlParameter("@P_ITEM_DETAILS", objEK.ITEM_DETAILS);
                        objParams[4] = new SqlParameter("@P_MIGNO", objEK.MIGNO);
                        objParams[5] = new SqlParameter("@P_MISGNO", objEK.MISGNO);
                        objParams[6] = new SqlParameter("@P_ITEM_UNIT", objEK.ITEM_UNIT);
                        objParams[7] = new SqlParameter("@P_ITEM_REORDER_QTY", objEK.ITEM_REORDER_QTY);
                        objParams[8] = new SqlParameter("@P_ITEM_MIN_QTY", objEK.ITEM_MIN_QTY);
                        objParams[9] = new SqlParameter("@P_ITEM_MAX_QTY", objEK.ITEM_MAX_QTY);
                        objParams[10] = new SqlParameter("@P_ITEM_BUD_QTY", objEK.ITEM_BUD_QTY);
                        objParams[11] = new SqlParameter("@P_ITEM_CUR_QTY", objEK.ITEM_CUR_QTY);
                        objParams[12] = new SqlParameter("@P_ITEM_OB_QTY", objEK.ITEM_OB_QTY);
                        objParams[13] = new SqlParameter("@P_ITEM_OB_VALUE", objEK.ITEM_OB_VALUE);
                        objParams[14] = new SqlParameter("@P_COLLEGE_CODE", objEK.COLLEGE_CODE);
                        objParams[15] = new SqlParameter("@P_USER_ID", userid);
                        objParams[16] = new SqlParameter("@P_ITPNO", itpno);
                        objParams[17] = new SqlParameter("@P_APPROVAL", objEK.ITEM_APPROVAL);
                        if (objSQLHelper.ExecuteNonQuerySP("PR_SPRT_ITEM_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.KitController.UpdateItemMaster-> " + ex.ToString());
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

                        ds = objSQLHelper.ExecuteDataSetSP("PR_SPRT_ITEM_GET_ALL_ITEMS", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.KitController.GetAllItemMaster-> " + ex.ToString());
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
                        ds = objSQLHelper.ExecuteDataSetSP("PR_SPRT_ITEM_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.KitController.GetSingleRecordItemMaster-> " + ex.ToString());
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
                        ds = objSQLHelper.ExecuteDataSetSP("PR_SPRT_GENERATE_INVOICENO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.KitController.GenerateInvoiceNo-> " + ex.ToString());
                    }
                    return ds;
                }

                
                public DataSet GetAllTax()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PR_SPRT_TAX_GET_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.KitController.GetAllTax-> " + ex.ToString());
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
                        ds = objSQLHelper.ExecuteDataSetSP("PR_SPRT_INVOICE_ITEM_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.KitController.GetAllItemsByInvoice-> " + ex.ToString());
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
                        ds = objSQLHelper.ExecuteDataSetSP("PR_SPRT_STOCK_SUMMARY_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.KitController.GetAllItemMaster-> " + ex.ToString());
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
                        ds = objSQLHelper.ExecuteDataSetSP("PR_SPRT_PRESCRIPTION_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.KitController.GetPatientDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion


                public int SaveInovoiceEntry(EventKitEnt objEK, string colcode, string accstatus)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[32];
                        objParams[0] = new SqlParameter("@P_INVOICE_NO", objEK.INVOICE_NO);
                        objParams[1] = new SqlParameter("@P_INVOICE_DATE", objEK.INVOICE_DATE);
                        objParams[2] = new SqlParameter("@P_D_M_NO ", objEK.D_M_NO);
                        objParams[3] = new SqlParameter("@P_D_M_DATE", objEK.D_M_DATE);
                        objParams[4] = new SqlParameter("@P_VENDOR_NO", objEK.VENDOR_NO);
                        objParams[5] = new SqlParameter("@P_RECEIVE_DATE", objEK.RECEIVE_DATE);
                        objParams[6] = new SqlParameter("@P_PURCHASE_ORDER_NO", objEK.PURCHASE_ORDER_NO);
                        objParams[7] = new SqlParameter("@P_PURCHASE_ORDER_DATE", objEK.PURCHASE_ORDER_DATE);
                        objParams[8] = new SqlParameter("@P_ITEM_TOT_QTY", objEK.ITEMTOTQTY);
                        objParams[9] = new SqlParameter("@P_GRAMT", objEK.GRAMT);
                        objParams[10] = new SqlParameter("@P_FLAG1", objEK.FLAG1);
                        objParams[11] = new SqlParameter("@P_FLAG2", objEK.FLAG2);
                        objParams[12] = new SqlParameter("@P_FLAG3", objEK.FLAG3);
                        objParams[13] = new SqlParameter("@P_FLAG4", objEK.FLAG4);
                        objParams[14] = new SqlParameter("@P_ECHG1", objEK.ECHG1);
                        objParams[15] = new SqlParameter("@P_ECHG2", objEK.ECHG2);
                        objParams[16] = new SqlParameter("@P_ECHG3", objEK.ECHG3);
                        objParams[17] = new SqlParameter("@P_ECHG4", objEK.ECHG4);
                        objParams[18] = new SqlParameter("@P_EP1", objEK.EP1);
                        objParams[19] = new SqlParameter("@P_EP2", objEK.EP2);
                        objParams[20] = new SqlParameter("@P_EP3", objEK.EP3);
                        objParams[21] = new SqlParameter("@P_EP4", objEK.EP4);
                        objParams[22] = new SqlParameter("@P_EAMT1", objEK.EAMT1);
                        objParams[23] = new SqlParameter("@P_EAMT2", objEK.EAMT2);
                        objParams[24] = new SqlParameter("@P_EAMT3", objEK.EAMT3);
                        objParams[25] = new SqlParameter("@P_EAMT4", objEK.EAMT4);
                        objParams[26] = new SqlParameter("@P_COLLEGE_CODE", colcode);
                        objParams[27] = new SqlParameter("@P_NETAMT", objEK.NETAMT);
                        objParams[28] = new SqlParameter("@P_REMARK", objEK.REMARK);
                        objParams[29] = new SqlParameter("@P_ITEM_LIST_TBL", objEK.ITEMLIST);
                        objParams[30] = new SqlParameter("@P_INVTRNO", objEK.INVTRNO);
                        objParams[31] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[31].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PR_SPRT_INVOICE_INSERT_UPDATE", objParams, true);
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
                        throw new IITMSException("IITMS.Sports.BusinessLayer.BusinessLogic.KitController.SaveInovoiceEntry->" + ex.ToString());
                    }
                    return retstatus;
                    // return (int)CustomStatus.RecordUpdated;

                }

                public int UpdateInovoiceEntry(EventKitEnt objEK, List<Sports_Invoice_Item> ListInvItem, string colcode)
                {
                    //try
                    //{
                    DAC.Commands.Clear();
                    //Delete All Item And Taxes
                    DAC dcItemDel = new DAC();
                    dcItemDel.StoredProcedure = "PR_SPRT_INVOICE_ITEM_DELETE";
                    dcItemDel.Params.Add("@P_INVTRNO", SqlDbType.Int, objEK.INVTRNO);
                    DAC.Commands.Add(dcItemDel);


                    //Save Invoice.
                    DAC dc = new DAC();
                    dc.StoredProcedure = "PR_SPRT_INVOICE_UPDATE";
                    //dc.Params.Add("@P_INVOICE_NO ", SqlDbType.NVarChar, objStock.INVOICE_NO);
                    dc.Params.Add("@P_INVOICE_DATE", SqlDbType.DateTime, objEK.INVOICE_DATE);
                    dc.Params.Add("@P_D_M_NO", SqlDbType.NVarChar, objEK.D_M_NO);
                    dc.Params.Add("@P_D_M_DATE", SqlDbType.DateTime, objEK.D_M_DATE);
                    dc.Params.Add("@P_VENDOR_NO", SqlDbType.Int, objEK.VENDOR_NO);
                    dc.Params.Add("@P_RECEIVE_DATE", SqlDbType.DateTime, objEK.RECEIVE_DATE);
                    dc.Params.Add("@P_PURCHASE_ORDER_NO", SqlDbType.Int, objEK.PURCHASE_ORDER_NO);
                    dc.Params.Add("@P_PURCHASE_ORDER_DATE", SqlDbType.DateTime, objEK.PURCHASE_ORDER_DATE);

                    dc.Params.Add("@P_GRAMT", SqlDbType.Money, objEK.GRAMT);
                    dc.Params.Add("@P_ITEM_TOT_QTY", SqlDbType.Int, objEK.ITEMTOTQTY);

                    dc.Params.Add("@P_FLAG1", SqlDbType.Bit, objEK.FLAG1);
                    dc.Params.Add("@P_FLAG2", SqlDbType.Bit, objEK.FLAG2);
                    dc.Params.Add("@P_FLAG3", SqlDbType.Bit, objEK.FLAG3);
                    dc.Params.Add("@P_FLAG4", SqlDbType.Bit, objEK.FLAG4);
                    dc.Params.Add("@P_ECHG1", SqlDbType.NVarChar, objEK.ECHG1);
                    dc.Params.Add("@P_ECHG2", SqlDbType.NVarChar, objEK.ECHG2);
                    dc.Params.Add("@P_ECHG3", SqlDbType.NVarChar, objEK.ECHG3);
                    dc.Params.Add("@P_ECHG4", SqlDbType.NVarChar, objEK.ECHG4);
                    dc.Params.Add("@P_EP1", SqlDbType.Decimal, objEK.EP1);
                    dc.Params.Add("@P_EP2", SqlDbType.Decimal, objEK.EP2);
                    dc.Params.Add("@P_EP3", SqlDbType.Decimal, objEK.EP3);
                    dc.Params.Add("@P_EP4", SqlDbType.Decimal, objEK.EP4);
                    dc.Params.Add("@P_EAMT1", SqlDbType.Money, objEK.EAMT1);
                    dc.Params.Add("@P_EAMT2", SqlDbType.Money, objEK.EAMT2);
                    dc.Params.Add("@P_EAMT3", SqlDbType.Money, objEK.EAMT3);
                    dc.Params.Add("@P_EAMT4", SqlDbType.Money, objEK.EAMT4);

                    dc.Params.Add("@P_COLLEGE_CODE", SqlDbType.NVarChar, colcode);
                    dc.Params.Add("@P_NETAMT", SqlDbType.Money, objEK.NETAMT);
                    dc.Params.Add("@P_REMARK", SqlDbType.NVarChar, objEK.REMARK);

                    dc.Params.Add("@P_INVTRNO", SqlDbType.Int, objEK.INVTRNO);


                    DAC.Commands.Add(dc);
                    //Save Invoice Item.
                    foreach (Sports_Invoice_Item Item in ListInvItem)
                    {
                        DAC dcItem = new DAC();
                        dcItem.StoredProcedure = "PR_SPRT_INVOICE_ITEM_INSERT";
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
                }
                public int GetInVoiceTRNO()
                {
                    SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);                   
                    string MAXNO = objComman.LookUp("SPRT_INVOICE", "COUNT(1)", "");
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
                        ds = objSQLHelper.ExecuteDataSetSP("PR_SPRT_INVOICE_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.KitController.GetInvoiceByNo-> " + ex.ToString());
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
                        ds = objSQLHelper.ExecuteDataSetSP("PR_SPRT_INVOICE_TAX_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.KitController.GetInvoiceByNo-> " + ex.ToString());
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
                        ds = objSQLHelper.ExecuteDataSetSP("PR_SPRT_ITEM_GET_BY_PO_FOR_INVOICE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.KitController.GetAllItemByPO-> " + ex.ToString());
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
                        ds = objSQLHelper.ExecuteDataSetSP("PR_SPRT_FIELD_GET_BY_PO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.KitController.GetAllTaxByPO-> " + ex.ToString());
                    }
                    return ds;
                }
            
                public DataSet GetAllInvoice()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PR_SPRT_GET_ALL_INVOICE", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.KitController.GetAllTax-> " + ex.ToString());
                    }
                    return ds;

                }

                #endregion



                #region Issue Item  & Issue Return

                public int ItemIssueInsertUpdate(EventKitEnt objEK)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_ISSUE_ID", objEK.ISSUE_ID);
                        objParams[1] = new SqlParameter("@P_TEAMID", objEK.TEAMID);
                        objParams[2] = new SqlParameter("@P_CLUBID", objEK.CLUBID);
                        objParams[3] = new SqlParameter("@P_ISSUE_TYPE", objEK.ISSUE_TYPE);
                        objParams[4] = new SqlParameter("@P_ISSUE_DATE", objEK.ISSUE_DATE);
                        objParams[5] = new SqlParameter("@P_REMARK", objEK.REMARK);
                        objParams[6] = new SqlParameter("@P_ISSUE_ITEM_LIST", objEK.ISSUE_ITEM_LIST);
                        objParams[7] = new SqlParameter("@P_USERID", objEK.USERID);                       
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PR_SPRT_ISSUE_ITEM_INSERT_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.KitController.ItemIssueInsertUpdate-> " + ex.ToString());
                    }
                    return retStatus;
                }




                public DataSet GetIssueDetails(char cat)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ISSUE_TYPE", cat);
                        ds = objSQLHelper.ExecuteDataSetSP("PR_SPRT_GET_ALL_ISSUE_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.KitController.GetIssueDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetIssueItemDetailsById(int IssueId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ISSUE_ID", IssueId);
                        ds = objSQLHelper.ExecuteDataSetSP("PR_SPRT_GET_ITEM_ISSUE_DETAILS_BY_ID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.KitController.GetIssueItemDetailsById-> " + ex.ToString());
                    }
                    return ds;
                }


                // This method is used to get available quantity of selected Item
                public DataSet GetItemsAvailableQuantity(int ItemId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ITEM_NO", ItemId);
                        ds = objSQLHelper.ExecuteDataSetSP("PR_SPRT_GET_SELECTED_ITEMS_AVAILABLE_QTY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.KitController.GetItemsAvailableQuantity-> " + ex.ToString());
                    }
                    return ds;
                }

                // This method is used to get list of Issued Item to a particular team
                public DataSet GetItemIssueDetailsOnIssueDate(int IssueId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ISSUE_ID", IssueId);
                        ds = objSQLHelper.ExecuteDataSetSP("PR_SPRT_GET_ITEMS_ISSUE_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.KitController.GetItemsAvailableQuantity-> " + ex.ToString());
                    }
                    return ds;
                }

           
                public int IssueReturnInsertUpdate(EventKitEnt objEK)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_ISSUE_ID", objEK.ISSUE_ID);
                        objParams[1] = new SqlParameter("@P_USERID", objEK.USERID);
                        objParams[2] = new SqlParameter("@P_FINAL_REMARK", objEK.REMARK);
                        objParams[3] = new SqlParameter("@P_ISSUE_RETURN_TBL", objEK.ISSUE_RETURN_TBL);                       
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PR_SPRT_ISSUE_RETURN_INSERT_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.KitController.IssueReturnInsertUpdate-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion

            }
        }
    }
}
