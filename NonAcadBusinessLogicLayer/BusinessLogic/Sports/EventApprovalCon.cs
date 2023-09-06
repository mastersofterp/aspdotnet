//================================================================================================
//PROJECT NAME  : UAIMS
//MODULE NAME   : SPORTS
//CREATED BY    : MRUNAL SINGH
//CREATION DATE : 01-MAY-2017   
//MODIFY BY     : 
//MODIFIED DATE :    
//================================================================================================   
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
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class EventApprovalCon
            {
                /// <SUMMARY>
                /// ConnectionStrings
                /// </SUMMARY>
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;



                //Get list of Events to approve as per approval authority path.
                public DataSet GetEventsPendingList(int UA_No)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_No);
                        ds = objSQLHelper.ExecuteDataSetSP("PR_SPRT_GET_PENDINGLIST_FOR_APPROVAL", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EventApprovalCon.GetEventsPendingList->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }


                //This method is used to get event details.
                public DataSet GetEventDetail(int psid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PSID", psid);
                        ds = objSQLHelper.ExecuteDataSetSP("PR_SPRT_GET_EVENT_DETAILS_BY_ID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EventApprovalCon.GetLeaveApplDetail->" + ex.ToString());

                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }


                //
                public int UpdateApprovalAuthority(EventApprovalEnt objEA)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[5];
                        objparams[0] = new SqlParameter("@P_PSID", objEA.PSID);
                        objparams[1] = new SqlParameter("@P_UA_NO", objEA.UA_NO);
                        objparams[2] = new SqlParameter("@P_STATUS", objEA.Status);
                        objparams[3] = new SqlParameter("@P_REMARKS", objEA.Remarks);                      
                        objparams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[4].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PR_SPRT_APPROVE_PLAN_SCHEDULE_BY_AUTHORITY", objparams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EventApprovalCon.UpdateApprovalAuthority->" + ex.ToString());
                    }
                    return retStatus;
                }



                //

                public int UpdatePlanSchedule(EventApprovalEnt objEA)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[4];
                        objparams[0] = new SqlParameter("@P_PSID", objEA.PSID);
                        objparams[1] = new SqlParameter("@P_FROMDT", objEA.FROMDT);
                        objparams[2] = new SqlParameter("@P_TODT", objEA.TODT);                     
                        objparams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PR_SPRT_APPROVE_PLAN_SCHEDULE", objparams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EventApprovalCon.UpdateLvAplAprStatus->" + ex.ToString());
                    }
                    return retStatus;
                }


                public int AddUpdateEventTeamAllotment(EventApprovalEnt objEA)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[7];
                        objparams[0] = new SqlParameter("@P_ETALLOTID", objEA.ETALLOTID);
                        objparams[1] = new SqlParameter("@P_ETID", objEA.ETID);
                        objparams[2] = new SqlParameter("@P_EVENTID", objEA.EVENTID);
                        objparams[3] = new SqlParameter("@P_TEAMID", objEA.TEAMID);
                        objparams[4] = new SqlParameter("@P_VENUEID", objEA.VENUEID);
                        objparams[5] = new SqlParameter("@P_USERID", objEA.USERID);
                        objparams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[6].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PR_SPRT_EVENT_TEAM_ALLOTMENT_IU", objparams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EventApprovalCon.UpdateLvAplAprStatus->" + ex.ToString());
                    }
                    return retStatus;
                }
            }
        }
    }
}
