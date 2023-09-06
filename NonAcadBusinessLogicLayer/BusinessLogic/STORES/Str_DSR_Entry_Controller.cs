using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using IITMS.NITPRM;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class Str_DSR_Entry_Controller
            {
                Str_DSR objDsr = new Str_DSR();
                StoreMasterController objmaster = new StoreMasterController();
                Common objcommon = new Common();
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetAllVendor()
                {
                    DataSet ds = null;
                    try
                    {
                        ds = objmaster.GetAllParty();
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_DSR_Entry_Controller_GetAllVendor-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAllItem()
                {
                    DataSet ds = null;
                    try
                    {
                        ds = objmaster.GetAllItemMaster();
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_DSR_Entry_Controller_GetAllItemMaster-> " + ex.ToString());
                    }
                    return ds;

                }

                public DataSet GetAllDSR()
                {
                    DataSet ds = null;
                    try
                    {
                        ds = objmaster.GetAllDSR();
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_DSR_Entry_Controller_GetAllDSR-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAllDept()
                {
                    DataSet ds = null;
                    try
                    {
                        //ds = objmaster.GetAllDepartMent();
                        ds = objcommon.FillDropDown("STORE_DEPARTMENT", "MDNAME,MDSNAME,COLLEGE_CODE", "MDNO", "", "");
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_DSR_Entry_Controller_GetAllDept-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetAllFinancialYear()
                {
                    DataSet ds = null;
                    try
                    {
                        ds = objmaster.GetAllFinancial_Year();
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_DSR_Entry_Controller_GetAllFinancialYear-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetSingleRecordDSRBYMDNO(int MDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_MDNO", MDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_DSR_GET_BY_MDNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetSingleRecordDSR-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetAllDSRNumber(int DSRTRNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DSTKNO", DSRTRNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_DSR_GET_BY_DSRTRNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetSingleRecordDSR-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAllDSRNumberNew(int DSRTRNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DSTKNO", DSRTRNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_DSR_GET_BY_DSRTRNO_DIRECT_DSR", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetSingleRecordDSR-> " + ex.ToString());
                    }
                    return ds;
                }


                public int SaveDSR(Str_DSR objDsr)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New DEADSTOCK
                        objParams = new SqlParameter[18];
                        objParams[0] = new SqlParameter("@P_DSRTRNO ", SqlDbType.Int);
                        objParams[0].Direction = ParameterDirection.Output;
                        objParams[1] = new SqlParameter("@P_DSTKNO", objDsr.DSTKNO);
                        objParams[2] = new SqlParameter("@P_DSRNO", objDsr.DSRNO);
                        objParams[3] = new SqlParameter("@P_DSRDATE", objDsr.DSRDATE);
                        objParams[4] = new SqlParameter("@P_AOPURCHASE", objDsr.AOPURCHASE);
                        objParams[5] = new SqlParameter("@P_PNO", objDsr.PNO);
                        objParams[6] = new SqlParameter("@P_INVOICENO", objDsr.INVOICENO);
                        objParams[7] = new SqlParameter("@P_AFINAL", objDsr.AFINAL);
                        objParams[8] = new SqlParameter("@P_DOCREDIT", objDsr.DOCREDIT);
                        objParams[9] = new SqlParameter("@P_VOUCHERNO", objDsr.VOUCHERNO);
                        objParams[10] = new SqlParameter("@P_AMOUNTREALISED", objDsr.AMOUNTREALISED);
                        objParams[11] = new SqlParameter("@P_AMOUNTWRITTEN", objDsr.AMOUNTWRITTEN);
                        objParams[12] = new SqlParameter("@P_BALANCE", objDsr.BALANCE);
                        objParams[13] = new SqlParameter("@P_REMARKS", objDsr.REMARKS);
                        objParams[14] = new SqlParameter("@P_INVTRNO", objDsr.INVTRNO);
                        objParams[15] = new SqlParameter("@P_MDNO", objDsr.MDNO);
                        objParams[16] = new SqlParameter("@P_STATUS", objDsr.STATUS);
                        objParams[17] = new SqlParameter("@P_FLAG", objDsr.FLAG);
                        if (objSQLHelper.ExecuteNonQuerySP("STR_DSR_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_DSR_Entry_Controller.SaveDSR-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int UpdateDSR(Str_DSR objDsr)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[18];
                        objParams[0] = new SqlParameter("@P_DSRTRNO ", objDsr.DSRTRNO);
                        objParams[1] = new SqlParameter("@P_DSTKNO", objDsr.DSTKNO);
                        objParams[2] = new SqlParameter("@P_DSRNO", objDsr.DSRNO);
                        objParams[3] = new SqlParameter("@P_DSRDATE", objDsr.DSRDATE);
                        objParams[4] = new SqlParameter("@P_AOPURCHASE", objDsr.AOPURCHASE);
                        objParams[5] = new SqlParameter("@P_PNO", objDsr.PNO);
                        objParams[6] = new SqlParameter("@P_INVOICENO", objDsr.INVOICENO);
                        objParams[7] = new SqlParameter("@P_AFINAL", objDsr.AFINAL);
                        objParams[8] = new SqlParameter("@P_DOCREDIT", objDsr.DOCREDIT);
                        objParams[9] = new SqlParameter("@P_VOUCHERNO", objDsr.VOUCHERNO);
                        objParams[10] = new SqlParameter("@P_AMOUNTREALISED", objDsr.AMOUNTREALISED);
                        objParams[11] = new SqlParameter("@P_AMOUNTWRITTEN", objDsr.AMOUNTWRITTEN);
                        objParams[12] = new SqlParameter("@P_BALANCE", objDsr.BALANCE);
                        objParams[13] = new SqlParameter("@P_REMARKS", objDsr.REMARKS);
                        objParams[14] = new SqlParameter("@P_INVTRNO", objDsr.INVTRNO);
                        objParams[15] = new SqlParameter("@P_MDNO", objDsr.MDNO);
                        objParams[16] = new SqlParameter("@P_STATUS", objDsr.STATUS);
                        objParams[17] = new SqlParameter("@P_FLAG", objDsr.FLAG);

                        if (objSQLHelper.ExecuteNonQuerySP("STR_DSR_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_DSR_Entry_Controller.UpdateDSR-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public int AddUpdateDSR(Str_DSR objDsr)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New DEADSTOCK
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_DSRTRNO ", objDsr.DSRTRNO);                       
                        objParams[1] = new SqlParameter("@P_MDNO", objDsr.MDNO);
                        objParams[2] = new SqlParameter("@P_DSTKNO", objDsr.DSTKNO);
                        objParams[3] = new SqlParameter("@P_FNO", objDsr.FNO);
                        objParams[4] = new SqlParameter("@P_DSRNO", objDsr.DSRNUMBER);   
                        objParams[5] = new SqlParameter("@P_DOP", objDsr.DOP);
                        objParams[6] = new SqlParameter("@P_DSRDATE", objDsr.DSRDATE);
                        objParams[7] = new SqlParameter("@P_PNO", objDsr.PNO);
                        objParams[8] = new SqlParameter("@P_REMARKNEW", objDsr.REMARKNEW);
                        objParams[9] = new SqlParameter("@P_ITEM_DETAILS_TBL", objDsr.ITEM_DETAILS);
                        objParams[10] = new SqlParameter("@P_USERID", objDsr.USERID);
                        objParams[11] = new SqlParameter("@P_COLLEGE_CODE", objDsr.COLLEGE_CODE);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);        
                        objParams[12].Direction = ParameterDirection.Output; 
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STR_DEAD_STOCK_REGISTER_IU", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else if (Convert.ToInt32(ret) == 2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_DSR_Entry_Controller.SaveDSR-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public DataSet GetDSRDetailsById(int DSRTRNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DSRTRNO", DSRTRNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_DSR_BY_DSRTRNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetDSRDetailsById-> " + ex.ToString());
                    }
                    return ds;
                }



                /// Purpose : Get DSR Data by DSRTRNO 
                /// FUNCTION NAME: GetDSRDataByInvoiceId
                /// </summary>               
                /// <returns></returns>
                public DataSet GetItemSerialNoByDsrTrNo(int DSRTRNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objsqlhelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DSRTRNO", DSRTRNO);
                        ds = objsqlhelper.ExecuteDataSetSP("PKG_STR_GET_DSR_NUMBER_BY_DSRTRNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Invoice_Entry_Controller_ GetDSRDataByDsrTrId-> " + ex.ToString());
                    }
                    return ds;
                }









                public DataSet GetDsrEntryByDSRTRNO(int DSRTRNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DSRTRNO", DSRTRNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_DSR_DSRTRNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StoreMasterController.GetSingleRecordDSR-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet genrateDSRCount(int DSRTKNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_DSRTKNO", DSRTKNO);
                        objParams[1] = new SqlParameter("@P_COUNT", 0);
                        objParams[1].Direction = ParameterDirection.Output;
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_DSR_GENERATE_DSRNO", objParams);
                          
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_DSR_Entry_Controller.GetSingleRecordDSR-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateSerialNumber(string DsrItemSrNo, int InvoiceId)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                       
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DSRTRNO ", DsrItemSrNo);
                        objParams[1] = new SqlParameter("@P_INVDINO", InvoiceId);                                              
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STR_UPDATE_DSR_ITEM_SRNO", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                       

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_DSR_Entry_Controller.UpdateSerialNumber-> " + ex.ToString());
                    }
                    return retStatus;
                }



                #region DSR Issue Item
                /// Purpose : Auto-complete Method for Item. 
                /// FUNCTION NAME: fillItem
                /// </summary>
                /// <returns></returns>
                public DataSet fillItem(string ItemName)
                {
                    DataSet ds = null;                    
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];                                  
                        objParams[0] = new SqlParameter("@P_ITEM_NAME", ItemName);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_DSR_ITEM_BY_ITEMNAME", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_DSR_Entry_Controller.fillItem-> " + ex.ToString());
                    }
                    return ds;
                }


                // It is used to get all DSR Items to issue.
                public DataSet GetAllDSRItemsToIssue(DSRITEM objDSRItem)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objsqlhelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];                       
                        objParams[0] = new SqlParameter("@P_ITEM_NO", objDSRItem.ITEM_NO);                        
                        ds = objsqlhelper.ExecuteDataSetSP("PKG_STR_GET_DSR_ITEM_TO_ISSUE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_DSR_Entry_Controller.GetAllDSRItemsToIssue-> " + ex.ToString());
                    }
                    return ds;
                }
                


                // It is used to Issue DSR Item details.
                public int AddDSRItemIssueDetails(DSRITEM objDsrItem)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;                       
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_ISSUENO ", objDsrItem.ISSUENO);
                        objParams[1] = new SqlParameter("@P_MDNO", objDsrItem.MDNO);
                        objParams[2] = new SqlParameter("@P_SDNO", objDsrItem.MDNO);
                        objParams[3] = new SqlParameter("@P_ITEM_NO", objDsrItem.ITEM_NO);
                        objParams[4] = new SqlParameter("@P_ISSUE_DATE", objDsrItem.DSTKNO);
                        objParams[5] = new SqlParameter("@P_REMARK", objDsrItem.ISSUE_REMARK);
                        objParams[6] = new SqlParameter("@P_STATUS", objDsrItem.STATUS);
                        objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objDsrItem.COLLEGE_CODE);
                        objParams[8] = new SqlParameter("@P_DSRISSUE_ITEM_TBL", objDsrItem.DSRIssueItemsTbl);
                        objParams[9] = new SqlParameter("@P_STORE_USER_TYPE", objDsrItem.STORE_USER_TYPE);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STR_ISSUE_DSR_ITEM_IU", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else 
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }  
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_DSR_Entry_Controller.AddDSRItemIssueDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }



                #endregion



            }
        }
    }
}
