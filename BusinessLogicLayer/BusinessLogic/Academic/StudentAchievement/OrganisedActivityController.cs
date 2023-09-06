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
  public  class OrganisedActivityController
    {
      private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

      public int InsertOrganisedActivityData(OrganisedActivityEntity objOAE, string _ua_no, string coordinator, string member)
      {
          int retStatus = Convert.ToInt32(CustomStatus.Others);
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            
                          
                            new SqlParameter("@P_ACADMIC_YEAR_ID ", objOAE.acadamic_year_id),
                            new SqlParameter("@P_TITLE_OF_ACITIVITY", objOAE.activity_title),
                            new SqlParameter("@P_TECHNICALACTIVITY_TYPE_ID", objOAE.technical_type_id),
                            new SqlParameter("@P_ORGANIZE_BY", objOAE.organize_by),
                            new SqlParameter("@P_CONDUCT_BY", objOAE.conduct_by),
                            new SqlParameter("@P_EVENT_LEVEL_ID", objOAE.event_level_id),
                            new SqlParameter("@P_STDATE", objOAE.SDate),
                            new SqlParameter("@P_ENDDATE", objOAE.EDate),
                            new SqlParameter("@P_VENUE", objOAE.venue),
                            new SqlParameter("@P_EVENT_MODE", objOAE.event_mode),
                            new SqlParameter("@P_DURATION_ID", objOAE.duration_id),
                            new SqlParameter("@P_STUDENTS_PARTICIPANTS_NO", objOAE.student_participants_no),
                            new SqlParameter("@P_TEACHERS_STAFF_PARTICIPATIONS_NO", objOAE.teachers_staff_participants_no),
                            new SqlParameter("@P_FUNDED_BY", objOAE.funded_by),
                            new SqlParameter("@P_SANCTIONED_AMOUNT", objOAE.sanctioned_amount),
                            new SqlParameter("@P_CONVERNER", _ua_no),
                            new SqlParameter("@P_CO_ORDINATOR ", coordinator),
                            new SqlParameter("@P_MEMBERS",member),
                            new SqlParameter("@P_OrganizationId",objOAE.OrganizationId),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int),
                              };
              sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;


              object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_ACHIEVEMENT_ACTIVITY_ORGANISED", sqlParams, true);
              if (Convert.ToInt32(ret) == -99)
                  retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
              else
                  retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

          }
          catch (Exception ex)
          {
              retStatus = Convert.ToInt32(CustomStatus.Error);
              throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.InsertOrganisedActivityData-> " + ex.ToString());
          }
          return retStatus;
      }

      public DataSet EditOrganisedActivityData(int ACTIVITY_ORGANISED_ID)
      {
          DataSet ds = null;
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] sqlParams = new SqlParameter[1];
              sqlParams[0] = new SqlParameter("@P_ACTIVITY_ORGANISED_ID", ACTIVITY_ORGANISED_ID);
              ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EDIT_ACHIEVEMENT_ACTIVITY_ORGANISED", sqlParams);
          }
          catch (Exception ex)
          {
              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EditOrganisedActivityData-> " + ex.ToString());
          }
          return ds;
      }

      public DataSet ActivityOrganisedListView()
      {
          DataSet ds = null;
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] sqlParams = new SqlParameter[0];

              ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SELECT_ACHIEVEMENT_ACTIVITY_ORGANISED", sqlParams);
          }
          catch (Exception ex)
          {

              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ActivityOrganisedListView-> " + ex.ToString());
          }
          return ds;
      }

      public int UpdateOrganisedActivity(OrganisedActivityEntity objOAE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                             new SqlParameter("@P_ACTIVITY_ORGANISED_ID",objOAE.organised_activity_id),
                            new SqlParameter("@P_ACADMIC_YEAR_ID",objOAE.acadamic_year_id),
                            new SqlParameter("@P_TITLE_OF_ACITIVITY", objOAE.activity_title),
                            new SqlParameter("@P_TECHNICALACTIVITY_TYPE_ID", objOAE.technical_type_id),
                            new SqlParameter("@P_ORGANIZE_BY", objOAE.organize_by),
                            new SqlParameter("@P_CONDUCT_BY", objOAE.conduct_by),
                            new SqlParameter("@P_EVENT_LEVEL_ID", objOAE.event_level_id),
                            new SqlParameter("@P_STDATE", objOAE.SDate),
                            new SqlParameter("@P_ENDDATE", objOAE.EDate),
                            new SqlParameter("@P_VENUE", objOAE.venue),
                            new SqlParameter("@P_EVENT_MODE", objOAE.event_mode),
                            new SqlParameter("@P_DURATION_ID", objOAE.duration_id),
                            new SqlParameter("@P_STUDENTS_PARTICIPANTS_NO", objOAE.student_participants_no),
                            new SqlParameter("@P_TEACHERS_STAFF_PARTICIPATIONS_NO", objOAE.teachers_staff_participants_no),
                            new SqlParameter("@P_FUNDED_BY", objOAE.funded_by),
                            new SqlParameter("@P_SANCTIONED_AMOUNT", objOAE.sanctioned_amount),
                            new SqlParameter("@P_CONVERNER", objOAE.converner),
                            new SqlParameter("@P_CO_ORDINATOR", objOAE.co_ordinator),
                             new SqlParameter("@P_MEMBERS", objOAE.members),
                          };


                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_ACHIEVEMENT_ACTIVITY_ORGANISED", sqlParams, false);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);



            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.UpdateOrganisedActivity-> " + ex.ToString());
            }
            return retStatus;
        }

      /// <summary>
      /// CREATE BY Diksha Nandurkar
      /// </summary>
      /// <returns></returns>
      public DataSet ActivityOrganisedReport()
      {
          DataSet ds = null;
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] sqlParams = new SqlParameter[0];

              ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SELECT_ACHIEVEMENT_ACTIVITY_ORGANISED_REPORT", sqlParams);
          }
          catch (Exception ex)
          {

              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ActivityOrganisedListView-> " + ex.ToString());
          }
          return ds;
      }
    }
    }

