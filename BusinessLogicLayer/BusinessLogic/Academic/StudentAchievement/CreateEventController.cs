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
   
   public class CreateEventController
    {
       private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

       public int InsertCreateEventData(CreateEventEntity objCEE, string clubtype, int college, string degree, string branch, string houses, string _ua_no)
       {
           int retStatus = Convert.ToInt32(CustomStatus.Others);
           try
           {
               SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
               SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            
                            new SqlParameter("@P_ACADMIC_YEAR_ID", objCEE.acadamic_year_id),
                            new SqlParameter("@P_EVENT_CATEGORY_ID", objCEE.event_category_id),
                            new SqlParameter("@P_EVENT_TITLE", objCEE.event_titlte),
                            new SqlParameter("@P_ORGANIZE_BY", objCEE.organize_by),
                            new SqlParameter("@P_CONDUCT_BY", objCEE.conduct_by),
                            new SqlParameter("@P_EVENT_LEVEL_ID", objCEE.event_level_id),
                            //new SqlParameter("@P_UA_NO", _ua_no), // 
                            new SqlParameter("@P_STDATE", objCEE.SDate),
                            new SqlParameter("@P_ENDDATE", objCEE.EDate),
                            new SqlParameter("@P_VENUE", objCEE.venue),
                            new SqlParameter("@P_EVENT_MODE", objCEE.event_mode),
                            new SqlParameter("@P_DURATION_ID", objCEE.duration_id),
                            new SqlParameter("@P_PRIZES", objCEE.prizes),
                            new SqlParameter("@P_WINNER", objCEE.winner),
                            new SqlParameter("@P_RUNNER_UP", objCEE.runner_up),
                            new SqlParameter("@P_THIRD_PLACE", objCEE.third_place),
                            new SqlParameter("@P_FUNDED_BY", objCEE.funded_by),
                            new SqlParameter("@P_STATUS", objCEE.IsActive),
                            new SqlParameter("@P_FILE_NAME", objCEE.file_name),
                            new SqlParameter("@P_OrganizationId",objCEE.OrganizationId),
                            new SqlParameter("@P_ACTIVITY_TYPE",objCEE.Activity_id),  //Added by Nikhil Shende Dt:-09/09/2022
                            new SqlParameter("@P_CLUB_TYPE",clubtype),

                            new SqlParameter("@P_COLLEGE_NO",college),
                            new SqlParameter("@P_DEGREENO",degree),
                            new SqlParameter("@P_BRANCHNO",branch),
                            new SqlParameter("@P_HOUSES",houses),

                            new SqlParameter("@P_ACTIVITY_TIME",objCEE.Time),
                            new SqlParameter("@P_FACULTY_CORDINATOR", _ua_no),
                            new SqlParameter("@P_REGISTRATION_DATE", objCEE.RegDate),
                            new SqlParameter("@P_REGISTRATION_CAPACITY", objCEE.RegCapacity),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int),
                              };

               sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

               object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_CREATE_EVENT", sqlParams, true);
               if (Convert.ToInt32(ret) == -99)
                   retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
               else
                   retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

           }
           catch (Exception ex)
           {
               retStatus = Convert.ToInt32(CustomStatus.Error);
               throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.InsertCreateEventData-> " + ex.ToString());
           }
           return retStatus;
       }


       public DataSet CreateEventListView()
       {
           DataSet ds = null;
           try
           {
               SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
               SqlParameter[] sqlParams = new SqlParameter[0];

               ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SELECT_ACHIEVEMENT_CREATE_EVENT", sqlParams);
           }
           catch (Exception ex)
           {

               throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BasicDetaisls-> " + ex.ToString());
           }
           return ds;
       }

      
       public DataSet EditCreateEventData(int CREATE_EVENT_ID)
       {
           DataSet ds = null;
           try
           {
               SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
               SqlParameter[] sqlParams = new SqlParameter[1];
               sqlParams[0] = new SqlParameter("@P_CREATE_EVENT_ID", CREATE_EVENT_ID);
               ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EDIT_ACHIEVEMENT_CREATE_EVENT", sqlParams);
           }
           catch (Exception ex)
           {
               throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EditBasicDetailsData-> " + ex.ToString());
           }
           return ds;
       }


       public int UpdateCreateEventData(CreateEventEntity objCEE, string clubtype, int college, string degree, string branch, string houses, string _ua_no)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_CREATE_EVENT_ID", objCEE.create_event_id),
                            new SqlParameter("@P_ACADMIC_YEAR_ID", objCEE.acadamic_year_id),
                            new SqlParameter("@P_EVENT_CATEGORY_ID", objCEE.event_category_id),
                            new SqlParameter("@P_EVENT_TITLE", objCEE.event_titlte),
                            new SqlParameter("@P_ORGANIZE_BY", objCEE.organize_by),
                            new SqlParameter("@P_CONDUCT_BY", objCEE.conduct_by),
                            new SqlParameter("@P_EVENT_LEVEL_ID", objCEE.event_level_id),
                            //new SqlParameter("@P_UA_NO", _ua_no),
                            new SqlParameter("@P_STDATE", objCEE.SDate),
                            new SqlParameter("@P_ENDDATE", objCEE.EDate),
                            new SqlParameter("@P_VENUE", objCEE.venue),
                            new SqlParameter("@P_EVENT_MODE", objCEE.event_mode),
                            new SqlParameter("@P_DURATION_ID", objCEE.duration_id),
                            new SqlParameter("@P_PRIZES", objCEE.prizes),
                            new SqlParameter("@P_WINNER", objCEE.winner),
                            new SqlParameter("@P_RUNNER_UP", objCEE.runner_up),
                            new SqlParameter("@P_THIRD_PLACE", objCEE.third_place),
                            new SqlParameter("@P_FUNDED_BY", objCEE.funded_by),
                            new SqlParameter("@P_STATUS", objCEE.IsActive),
                            new SqlParameter("@P_FILE_NAME", objCEE.file_name),
                            new SqlParameter("@P_ACTIVITY_TYPE",objCEE.Activity_id), //Added by Nikhil Shende Dt:-09/09/2022
                            new SqlParameter("@P_CLUB_TYPE",clubtype),
                            new SqlParameter("@P_ACTIVITY_TIME",objCEE.Time),

                            new SqlParameter("@P_COLLEGE_NO",college),
                            new SqlParameter("@P_DEGREENO",degree),
                            new SqlParameter("@P_BRANCHNO",branch),
                            new SqlParameter("@P_HOUSES",houses),

                            new SqlParameter("@P_FACULTY_CORDINATOR",_ua_no), 
                            new SqlParameter("@P_REGISTRATION_DATE", objCEE.RegDate), 
                            new SqlParameter("@P_REGISTRATION_CAPACITY", objCEE.RegCapacity),
                        };
               

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_ACHIEVEMENT_CREATE_EVENT", sqlParams, false);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);



            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.UpdateCreateEventData-> " + ex.ToString());
            }
            return retStatus;
        }
    
       /// <summary>
       /// Create By Diksha Nandurkar
       /// </summary>
       /// <returns></returns>
      public DataSet CreateEventReport()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SELECT_ACHIEVEMENT_CREATE_EVENT_REPORT", sqlParams);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CreateEventReport-> " + ex.ToString());
            }
            return ds;
        }

   
    }



    }

