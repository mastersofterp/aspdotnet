//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS LOGIC FILE [RulesCreationController]                              
// CREATION DATE : 05-MAY-2018
// CREATED BY    : M. REHBAR SHEIKH                                                   
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
            public class RulesCreationController
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                //*****UPDATED BY: M. REHBAR SHEIKH ON 04-08-2018****
                public int InsertRules(RulesCreation objRules, int RuleID)
                {
                    //int retStatus = Convert.ToInt32(CustomStatus.Others);
                    int status = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add
                        objParams = new SqlParameter[19];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", objRules.college_Id);
                        objParams[1] = new SqlParameter("@P_DEGREENO", objRules.degreeNo);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", objRules.BranchNo);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", objRules.SchemeNo);
                        objParams[4] = new SqlParameter("@P_ACADEMIC_PATTERN", objRules.academicPattern);
                        objParams[5] = new SqlParameter("@P_DURATION_REGULAR", objRules.durationRegular);
                        objParams[6] = new SqlParameter("@P_SPAN_PERIOD", objRules.spanPeriod);
                        objParams[7] = new SqlParameter("@P_BACKLOG_SEM1_SEM2", objRules.Backlog_Sem1_Sem2);
                        objParams[8] = new SqlParameter("@P_BACKLOG_SEM2_SEM3", objRules.Backlog_Sem2_Sem3);
                        objParams[9] = new SqlParameter("@P_BACKLOG_SEM3_SEM4", objRules.Backlog_Sem3_Sem4);
                        objParams[10] = new SqlParameter("@P_BACKLOG_SEM4_SEM5", objRules.Backlog_Sem4_Sem5);
                        objParams[11] = new SqlParameter("@P_BACKLOG_SEM5_SEM6", objRules.Backlog_Sem5_Sem6);
                        objParams[12] = new SqlParameter("@P_BACKLOG_SEM6_SEM7", objRules.Backlog_Sem6_Sem7);
                        objParams[13] = new SqlParameter("@P_BACKLOG_SEM7_SEM8", objRules.Backlog_Sem7_Sem8);
                        objParams[14] = new SqlParameter("@P_REMARKS", objRules.remark);
                        objParams[15] = new SqlParameter("@P_RULE_ID", RuleID);
                        objParams[16] = new SqlParameter("@P_IPADD", objRules.IPaddress);
                        objParams[17] = new SqlParameter("@P_UA_NO", objRules.UA_NO);
                        objParams[18] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[18].Direction = ParameterDirection.Output;

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_INS_SESSIONMASTER", objParams, true) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_SEM_PROMOTION_RULES_CREATION_INSERT", objParams, true);

                        if (obj.ToString() == "1")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (obj.ToString() == "2")
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RulesCreationController.InsertRules-> " + ex.ToString());
                    }
                    return status;
                }

                /// <summary>
                /// Added By S.Patil - 28112018
                /// </summary>
                public SqlDataReader GetRuleDetailsToEdit(int RuleID)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_RuleID", RuleID);
                        dr = objSQLHelper.ExecuteReaderSP("PKG_SP_GET_RULESDATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetSingleSession-> " + ex.ToString());
                    }
                    return dr;
                }


                //*****ADDED BY: M. REHBAR SHEIKH ON 04-09-2018****
                public int InsertYearRules(RulesCreation objRules, int ruleno)
                {
                    //int retStatus = Convert.ToInt32(CustomStatus.Others);
                    int status = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add
                        objParams = new SqlParameter[16];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", objRules.college_Id);
                        objParams[1] = new SqlParameter("@P_DEGREENO", objRules.degreeNo);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", objRules.BranchNo);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", objRules.SchemeNo);
                        objParams[4] = new SqlParameter("@P_ACADEMIC_PATTERN", objRules.academicPattern);
                        objParams[5] = new SqlParameter("@P_DURATION_REGULAR", objRules.durationRegular);
                        objParams[6] = new SqlParameter("@P_SPAN_PERIOD", objRules.spanPeriod);
                        objParams[7] = new SqlParameter("@P_BACKLOG_YEAR1_YEAR2", objRules.Backlog_Year1_Year2);
                        objParams[8] = new SqlParameter("@P_BACKLOG_YEAR2_YEAR3", objRules.Backlog_Year2_Year3);
                        objParams[9] = new SqlParameter("@P_BACKLOG_YEAR3_YEAR4", objRules.Backlog_Year3_Year4);
                        objParams[10] = new SqlParameter("@P_BACKLOG_YEAR4_YEAR5", objRules.Backlog_Year4_Year5);
                        objParams[11] = new SqlParameter("@P_REMARKS", objRules.remark);
                        objParams[12] = new SqlParameter("@P_RULENO", ruleno);
                        objParams[13] = new SqlParameter("@P_IPADD", objRules.IPaddress);
                        objParams[14] = new SqlParameter("@P_UA_NO", objRules.UA_NO);
                        objParams[15] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[15].Direction = ParameterDirection.Output;

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_INS_SESSIONMASTER", objParams, true) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_YEAR_PROMOTION_RULES_CREATION_INSERT", objParams, true);

                        if (obj.ToString() == "1")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (obj.ToString() == "2")
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RulesCreationController.InsertYearRules-> " + ex.ToString());
                    }
                    return status;
                }

                //*****ADDED BY: M. REHBAR SHEIKH ON 04-08-2018****
                public DataSet GetAllSemPromotionRules()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_ALL_SEM_PROMOTION_RULES", objParams);

                    }
                    catch (Exception ex)
                    {
                        //return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RulesCreationController.GetAllSemPromotionRules-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }//END

                //*****ADDED BY: M. REHBAR SHEIKH ON 04-09-2018****
                public DataSet GetAllYearPromotionRules()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_ALL_YEAR_PROMOTION_RULES", objParams);

                    }
                    catch (Exception ex)
                    {
                        //return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RulesCreationController.GetAllSemPromotionRules-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }//END

                //*****ADDED BY: M. REHBAR SHEIKH ON 04-08-2018****
                public DataSet GetAllSemPromotionRulesByFilters(int college_id, int degreeno, int branchno, int schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_SEM_PROMOTION_RULES_BY_COLLEGE", objParams);
                        //ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_SEM_PROMOTION_RULES", objParams);
                    }
                    catch (Exception ex)
                    {
                        //return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RulesCreationController.GetAllSemPromotionRulesByFilters-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }//END

                //*****ADDED BY: M. REHBAR SHEIKH ON 04-09-2018****
                public DataSet GetAllYearPromotionRulesByFilters(int college_id, int degreeno, int branchno, int schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_YEAR_PROMOTION_RULES", objParams);

                    }
                    catch (Exception ex)
                    {
                        //return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RulesCreationController.GetAllSemPromotionRulesByFilters-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }//END

                //*****ADDED BY: M. REHBAR SHEIKH ON 16-08-2018****
                public DataSet GetAllSemPromotionRulesByDegree(int college_id, int degreeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_SEM_PROMOTION_RULES_BY_DEGREE", objParams);

                    }
                    catch (Exception ex)
                    {
                        //return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RulesCreationController.GetAllSemPromotionRulesByFilters-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }//END

                //*****ADDED BY: M. REHBAR SHEIKH ON 04-09-2018****
                public DataSet GetAllYearPromotionRulesByDegree(int college_id, int degreeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_YEAR_PROMOTION_RULES_BY_DEGREE", objParams);

                    }
                    catch (Exception ex)
                    {
                        //return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RulesCreationController.GetAllSemPromotionRulesByFilters-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }//END


                //added on 06-05-2020 by Vaishali to get sem promotion rules details by Rule ID
                public SqlDataReader GetDetailsForSemPromotionRulesByRuleID(int ruleid)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_RULE_ID", ruleid);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_GET_DETAILS_SEM_PROMOTION_RULES_BY_RULE_ID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RulesCreationController.GetDetailsForSemPromotionRulesByRuleID-> " + ex.ToString());
                    }
                    return dr;
                }

//added on 06-05-2020 by Vaishali to get sem promotion rules details
                public DataSet GetDetailsForSemPromotionRules(int schemeno, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_DETAILS_SEM_PROMOTION_RULES", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RulesCreationController.GetDetailsForSemPromotionRules-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

 //added on 06-05-2020 by Vaishali to update into sem promotion rules
                public int UpdSemPromotionRules(int ruleid, int schemeno, int semesterno, int min_earned_credits_per, int prev_sem_course_cleared)
                {
                    int retstatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[6];
                        object obj;
                        objParams[0] = new SqlParameter("@P_RULE_ID", ruleid);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_MIN_EARNED_CREDIT_PER", min_earned_credits_per);
                        objParams[4] = new SqlParameter("@P_PREV_SEM_COURSE_CLEARED", prev_sem_course_cleared);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        obj = objSQLHelper.ExecuteNonQuerySP("PKG_UPD_SEM_PROMOTION_RULES", objParams, true);

                        if (obj != null)
                        {
                            if (Convert.ToInt32(obj) == 2)
                            {
                                retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            }
                            else if (Convert.ToInt32(obj) == -99)
                            {
                                retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            }
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RulesCreationController.UpdSemPromotionRules-> " + ex.ToString());
                    }

                    return retstatus;
                }

  //added on 06-05-2020 by Vaishali to insert into sem promotion rules
                public int InsSemPromotionRules(int schemeno, int semesterno, int min_earned_credits_per, int prev_sem_course_cleared)
                {
                    int retstatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        object obj;
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[2] = new SqlParameter("@P_MIN_EARNED_CREDIT_PER", min_earned_credits_per);
                        objParams[3] = new SqlParameter("@P_PREV_SEM_COURSE_CLEARED", prev_sem_course_cleared);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        obj = objSQLHelper.ExecuteNonQuerySP("PKG_INS_SEM_PROMOTION_RULES", objParams, true);

                        if (obj != null)
                        {
                            if (Convert.ToInt32(obj) == 1)
                            {
                                retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            }
                            else if (Convert.ToInt32(obj) == -99)
                            {
                                retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            }
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RulesCreationController.InsSemPromotionRules-> " + ex.ToString());
                    }

                    return retstatus;
                }

            }
        }

    }
}
