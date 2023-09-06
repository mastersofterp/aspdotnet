using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using BusinessLogicLayer.BusinessEntities.Academic.StudentAchievement;

namespace BusinessLogicLayer.BusinessLogic.Academic.StudentAchievement
{
    public class EventParticipationController
    {
        private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;



        public int InsertEventParticipationData(EventParticipationEntity objEPE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            
                          
                            new SqlParameter("@P_ACADMIC_YEAR_ID ", objEPE.acadamic_year_id),
                            new SqlParameter("@P_EVENT_CATEGORY_ID", objEPE.event_category_id),
                            new SqlParameter("@P_ACTIVITY_CATEGORY_ID", objEPE.activity_category_id),
                            new SqlParameter("@P_CREATE_EVENT_ID", objEPE.create_event_id),
                            new SqlParameter("@P_PARTICIPATION_TYPE_ID", objEPE.participation_type_id),
                            new SqlParameter("@P_FA_STATUS", objEPE.fc_status),
                            new SqlParameter("@P_AMOUNT", objEPE.amount),
                            new SqlParameter("@P_FILE_NAME", objEPE.file_name),
                            new SqlParameter("@P_IDNO", objEPE.idno),
                            new SqlParameter("@P_IP_ADDRESS", objEPE.IPADDRESS),
                            new SqlParameter("@P_UA_NO", objEPE.uno),
                            new SqlParameter("@P_C_DATE", objEPE.Current_Date),
                            new SqlParameter("@P_OrganizationId",objEPE.OrganizationId),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int),
                              };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;


                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_ACHIEVEMENT_EVENT_PARTICIPATION", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.InsertEventParticipationData-> " + ex.ToString());
            }
            return retStatus;
        }



        public DataSet EventParticipationListView()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SELECT_ACHIEVEMENT_EVENT_PARTICIPATION", sqlParams);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BasicDetaisls-> " + ex.ToString());
            }
            return ds;
        }

        public int UpdatEeventParticipation(EventParticipationEntity objEPE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_EVENT_PARTICIPATION_ID",objEPE.event_participation_id),
                            new SqlParameter("@P_ACADMIC_YEAR_ID", objEPE.acadamic_year_id),
                            new SqlParameter("@P_EVENT_CATEGORY_ID", objEPE.event_category_id),
                            new SqlParameter("@P_ACTIVITY_CATEGORY_ID", objEPE.activity_category_id),
                            new SqlParameter("@P_CREATE_EVENT_ID", objEPE.create_event_id),
                            new SqlParameter("@P_PARTICIPATION_TYPE_ID", objEPE.participation_type_id),
                            new SqlParameter("@P_FA_STATUS", objEPE.fc_status),
                            new SqlParameter("@P_AMOUNT", objEPE.amount),
                            new SqlParameter("@P_FILE_NAME", objEPE.file_name),
                               
                          };


                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_ACHIEVEMENT_EVENT_PARTICIPATION", sqlParams, false);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);



            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.UpdateEventNatureData-> " + ex.ToString());
            }
            return retStatus;
        }
        public DataSet EditEeventParticipation(int EVENT_PARTICIPATION_ID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@P_EVENT_PARTICIPATION_ID", EVENT_PARTICIPATION_ID);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EDIT_ACHIEVEMENT_EVENT_PARTICIPATION", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EditEventNatureData-> " + ex.ToString());
            }
            return ds;
        }

        /// <summary>
        /// Create By Diksha Nandurkar
        /// </summary>
        /// <returns></returns>
        public DataSet StudentAchievementReport(int EVENT_PARTICIPATION_ID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@P_EVENT_PARTICIPATION_ID", EVENT_PARTICIPATION_ID);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SELECT_ACHIEVEMENT_STUDENT_ACHIEVEMENT_REPORT", sqlParams);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAchievementReport-> " + ex.ToString());
            }
            return ds;
        }

    }

}

