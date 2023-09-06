//================================================================================================
//PROJECT NAME  : UAIMS
//MODULE NAME   : SPORTS
//CREATED BY    : MRUNAL SINGH
//CREATION DATE : 24-APR-2017   
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
            public class PlanScheduleCon
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                /// <summary>
                /// This function is used to create plan and schedule for an Event.
                /// </summary>
                /// <param name="objSport"></param>
                /// <returns></returns>
                public int AddUpdatePlanSchedule(PlanSchedule objPS)
                {
                    int retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_PSID", objPS.PSID);
                        objParams[1] = new SqlParameter("@P_ETID", objPS.ETID);
                        objParams[2] = new SqlParameter("@P_EVENTID", objPS.EVENTID);
                        objParams[3] = new SqlParameter("@P_FROMDATE", objPS.FROMDATE);
                        objParams[4] = new SqlParameter("@P_TODATE", objPS.TODATE);
                        objParams[5] = new SqlParameter("@P_TEAMID", objPS.TEAMID);
                        objParams[6] = new SqlParameter("@P_USERID", objPS.USERID);
                        objParams[7] = new SqlParameter("@P_COLLEGE_NO", objPS.COLLEGE_NO);
                        objParams[8] = new SqlParameter("@P_PAPNO", objPS.PAPNO);
                        objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objPS.COLLEGE_CODE);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PR_SPRT_PLAN_SCHEDULE_EVENT_IU", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == 2627)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PlanScheduleCon.AddUpdatePlanSchedule->" + ex.ToString());
                    }
                    return retstatus;
                }

            }
        }
    }
}
