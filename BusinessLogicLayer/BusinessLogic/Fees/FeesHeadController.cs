//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS LOGIC CONTROLLER [FEES HEAD DEFINATION]                     
// CREATION DATE : 12-MAY-2009                                                          
// CREATED BY    : SANJAY RATNAPARKHI                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class FeesHeadController
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetFeesHeads(string rcpCode)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_RECIEPTCODE", rcpCode);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_FEE_HEADS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesHeadController.GetFeesHeads-> " + ex.ToString());
                    }
                    return ds;
                }


                public int UpdateFeeHead(int FEE_TITLE_NO, string FEE_SHORTNAME, string FEE_LONGNAME, int RECEIPT_SHOW, int ISSCHOLARSHIP, int ISREACTIVATESTUDENT, int ISCAUTION_MONEY)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_FEE_TITLE_NO", FEE_TITLE_NO);
                        objParams[1] = new SqlParameter("@FEE_SHORTNAME ", FEE_SHORTNAME);
                        objParams[2] = new SqlParameter("@FEE_LONGNAME", FEE_LONGNAME);
                        objParams[3] = new SqlParameter("@RECEIPT_SHOW", RECEIPT_SHOW);
                        objParams[4] = new SqlParameter("@ISSCHOLARSHIP", ISSCHOLARSHIP);
                        objParams[5] = new SqlParameter("@ISREACTIVATESTUDENT", ISREACTIVATESTUDENT);
                        objParams[6] = new SqlParameter("@ISCAUTION_MONEY", ISCAUTION_MONEY);

                        //***************

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPD_FEE_TITLE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesHead.UpdateFeeHead-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //currency 
                public int UpdateCurrencyHead(int feeTitleNo, string recCode, int paytypeno, int CURRENCY)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_FEE_TITLE_NO", feeTitleNo);

                        objParams[1] = new SqlParameter("@P_REC_CODE", recCode);

                        objParams[2] = new SqlParameter("@P_PAYTYPENO", paytypeno);
                        objParams[3] = new SqlParameter("@P_CUR_NO", CURRENCY);


                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPD_FEE_TITLE_CURRENCY", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesHead.UpdateFeeHead-> " + ex.ToString());
                    }

                    return retStatus;
                }
                /// <summary>
                /// /GET CURRENCY
                /// </summary>
                /// <param name="rcpCode"></param>
                /// <param name="payTypeNo"></param>
                /// <returns></returns>
                public DataSet GetCurrencyHeads(string rcpCode, int payTypeNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_RECIEPTCODE", rcpCode);
                        objParams[1] = new SqlParameter("@P_PAYTYPENO", payTypeNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_CURRENCY_HEADS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeesHeadController.GetFeesHeads-> " + ex.ToString());
                    }
                    return ds;
                }

                #region BackLogExamFees
                /// <summary>
                ///Created By sumit 25102019
                /// </summary>
                /// <param name="PerPaperFees"></param>
                /// <param name="NumberOfPapers"></param>
                /// <returns></returns>
                public int BackLogExamFees(int PerPaperFees, int NoOfPaper, int MoreFees, string IpAddress, int UA_NO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_PERPAPERFEES", PerPaperFees);
                        objParams[1] = new SqlParameter("@P_NOOFPAPERS", NoOfPaper);
                        objParams[2] = new SqlParameter("@P_MORETHANPAPERFEES", MoreFees);
                        objParams[3] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[4] = new SqlParameter("@P_IPADDRESS", IpAddress);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_BACKLOG_EXAM_FEES", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddWorkArea-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion
            }
        }
    }
}