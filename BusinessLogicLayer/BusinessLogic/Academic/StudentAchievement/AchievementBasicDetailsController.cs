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
    public class AchievementBasicDetailsController
    {
        private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


        public int InsertBasicDetailsData(AchievementBasicDetailsEntity objBDE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            
                            new SqlParameter("@P_PARTICIPATION_LEVEL_NAME", objBDE.participation_level_name),
                            new SqlParameter("@P_ACTIVE_STATUS", objBDE.IsActive),
                            new SqlParameter("@P_OrganizationId",objBDE.OrganizationId),
                              new SqlParameter("@P_OUTPUT", SqlDbType.Int),
                              };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_ACHIEVEMENT_BASIC_DETAISLS", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.InsertBasicDetailsData-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetListview()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SELECT_ACHIEVEMENT_BASIC_DETAISLS", sqlParams);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GetListview-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet EditBasicDetailsData(int PARTICIPATION_LEVEL_ID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@P_PARTICIPATION_LEVEL_ID", PARTICIPATION_LEVEL_ID);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EDIT_ACHIEVEMENT_BASIC_DETAISLS", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EditBasicDetailsData-> " + ex.ToString());
            }
            return ds;
        }




        public int UpdateBasicDetailsData(AchievementBasicDetailsEntity objBDE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_PARTICIPATION_LEVEL_ID",objBDE.participation_level_id),
                            new SqlParameter("@P_PARTICIPATION_LEVEL_NAME", objBDE.participation_level_name),
                            new SqlParameter("@P_ACTIVE_STATUS", objBDE.IsActive),
                          };


                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_ACHIEVEMENT_BASIC_DETAISLS", sqlParams, false);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);



            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.UpdateBasicDetailsData-> " + ex.ToString());
            }
            return retStatus;
        }




        public int InsertAchievementEventNature(AchievementBasicDetailsEntity objBDE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            
                            new SqlParameter("@P_EVENT_NATURE", objBDE.event_nature_name),
                            new SqlParameter("@P_ACTIVE_STATUS", objBDE.IsActive),
                            new SqlParameter("@P_OrganizationId",objBDE.OrganizationId),
                              new SqlParameter("@P_OUTPUT", SqlDbType.Int),
                              };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_ACHIEVEMENT_EVENT_NATURE", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.InsertAchievementEventNature-> " + ex.ToString());
            }
            return retStatus;
        }



        public DataSet GetEventNatureList()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SELECT_ACHIEVEMENT_EVENT_NATURE", sqlParams);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BasicDetaisls-> " + ex.ToString());
            }
            return ds;
        }




        public DataSet EditEventNatureData(int EVENT_NATURE_ID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@P_EVENT_NATURE_ID", EVENT_NATURE_ID);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EDIT_ACHIEVEMENT_EVENT_NATURE", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EditEventNatureData-> " + ex.ToString());
            }
            return ds;
        }



        public int UpdateEventNatureData(AchievementBasicDetailsEntity objBDE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_EVENT_NATURE_ID",objBDE.event_nature_id),
                            new SqlParameter("@P_EVENT_NATURE", objBDE.event_nature_name),
                            new SqlParameter("@P_ACTIVE_STATUS", objBDE.IsActive),
                          };


                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_ACHIEVEMENT_EVENT_NATURE", sqlParams, false);
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

        public int InsertEventCategoryData(AchievementBasicDetailsEntity objBDE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            
                            new SqlParameter("@P_EVENT_CATEGORY_NAME", objBDE.event_category_name),
                            new SqlParameter("@P_EVENT_NATURE_ID", objBDE.event_nature_id),
                            new SqlParameter("@P_ACTIVE_STATUS", objBDE.IsActive),
                            new SqlParameter("@P_OrganizationId",objBDE.OrganizationId),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int),
                              };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_ACHIEVEMENT_EVENT_CATEGORY", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.InsertEventCategoryData-> " + ex.ToString());
            }
            return retStatus;
        }


        public DataSet GetCategoryList()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SELECT_ACHIEVEMENT_EVENT_CATEGORY", sqlParams);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GetCategoryList-> " + ex.ToString());
            }
            return ds;
        }




        public DataSet EditCategoryData(int EVENT_CATEGORY_ID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@P_EVENT_CATEGORY_ID", EVENT_CATEGORY_ID);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EDIT_ACHIEVEMENT_EVENT_CATEGORY", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EditCategoryData-> " + ex.ToString());
            }
            return ds;
        }



        public int UpdateCategoryData(AchievementBasicDetailsEntity objBDE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_EVENT_CATEGORY_ID ",objBDE.event_category_id),
                            new SqlParameter("@P_EVENT_CATEGORY_NAME", objBDE.event_category_name),
                            new SqlParameter("@P_EVENT_NATURE_ID ", objBDE.event_nature_id),
                            new SqlParameter("@P_ACTIVE_STATUS", objBDE.IsActive),
                          };


                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_ACHIEVEMENT_EVENT_CATEGORY", sqlParams, false);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);



            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.UpdateCategoryData-> " + ex.ToString());
            }
            return retStatus;
        }



        public int InsertActivityCategory(AchievementBasicDetailsEntity objBDE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                           
                            new SqlParameter("@P_ACTIVITY_CATEGORY_NAME", objBDE.activity_category_name),
                            new SqlParameter("@P_EVENT_CATEGORY_ID", objBDE.event_category_id),
                            new SqlParameter("@P_ACTIVE_STATUS", objBDE.IsActive),
                            new SqlParameter("@P_OrganizationId",objBDE.OrganizationId),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int),
                              };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_ACHIEVEMENT_ACTIVITY_CATEGORY", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.InsertActivityCategory-> " + ex.ToString());
            }
            return retStatus;
        }




        public DataSet GetActivityCategoryList()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SELECT_ACHIEVEMENT_ACTIVITY_CATEGORY", sqlParams);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GetActivityCategoryList-> " + ex.ToString());
            }
            return ds;
        }



        public DataSet EditActivityCategoryData(int ACTIVITY_CATEGORY_ID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@P_ACTIVITY_CATEGORY_ID", ACTIVITY_CATEGORY_ID);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EDIT_ACHIEVEMENT_ACTIVITY_CATEGORY", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EditCategoryData-> " + ex.ToString());
            }
            return ds;
        }

        public int UpdateActivityData(AchievementBasicDetailsEntity objBDE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_ACTIVITY_CATEGORY_ID",objBDE.activity_category_id),
                            new SqlParameter("@P_ACTIVITY_CATEGORY_NAME", objBDE.activity_category_name),
                            new SqlParameter("@P_EVENT_CATEGORY_ID", objBDE.event_category_id),
                            new SqlParameter("@P_ACTIVE_STATUS", objBDE.IsActive),
                          };


                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_ACHIEVEMENT_ACTIVITY_CATEGORY", sqlParams, false);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);



            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.UpdateActivityData-> " + ex.ToString());
            }
            return retStatus;
        }




        public int InsertEventLevel(AchievementBasicDetailsEntity objBDE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            
                            new SqlParameter("@P_EVENT_LEVEL", objBDE.event_level_name),
                            new SqlParameter("@P_ACTIVE_STATUS", objBDE.IsActive),
                            new SqlParameter("@P_OrganizationId",objBDE.OrganizationId),
                               new SqlParameter("@P_OUTPUT", SqlDbType.Int),
                              };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;



                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_ACHIEVEMENT_EVENT_LEVEL", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.BasicDetaisls-> " + ex.ToString());
            }
            return retStatus;
        }



        public DataSet EventLevelListview()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SELECT_ACHIEVEMENT_EVENT_LEVEL", sqlParams);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BasicDetaisls-> " + ex.ToString());
            }
            return ds;
        }
        public DataSet EditEventLevel(int EVENT_LEVEL_ID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@P_EVENT_LEVEL_ID", EVENT_LEVEL_ID);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EDIT_ACHIEVEMENT_EVENT_LEVEL", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EditBasicDetailsData-> " + ex.ToString());
            }
            return ds;
        }




        public int UpdateEventLevelData(AchievementBasicDetailsEntity objBDE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_EVENT_LEVEL_ID",objBDE.event_level_id),
                            new SqlParameter("@P_EVENT_LEVEL", objBDE.event_level_name),
                            new SqlParameter("@P_ACTIVE_STATUS", objBDE.IsActive),
                          };


                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_ACHIEVEMENT_EVENT_LEVEL", sqlParams, false);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);



            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.UpdateBasicDetailsData-> " + ex.ToString());
            }
            return retStatus;
        }



        public int InsertTechnicalActivityType(AchievementBasicDetailsEntity objBDE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            
                            new SqlParameter("@P_TECHNICALACTIVITY_TYPE", objBDE.technical_type),
                            new SqlParameter("@P_ACTIVE_STATUS", objBDE.IsActive),
                            new SqlParameter("@P_OrganizationId",objBDE.OrganizationId),
                             new SqlParameter("@P_OUTPUT", SqlDbType.Int),
                              };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_ACHIEVEMENT_TECHNICALACTIVITY_TYPE", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.BasicDetaisls-> " + ex.ToString());
            }
            return retStatus;
        }


        public DataSet TecnicalActivityListView()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SELECT_ACHIEVEMENT_TECHINICAL_ACTIVITY_TYPE", sqlParams);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BasicDetaisls-> " + ex.ToString());
            }
            return ds;
        }


        public DataSet EditTechnicalActivity(int TECHNICALACTIVITY_TYPE_ID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@P_TECHNICALACTIVITY_TYPE_ID", TECHNICALACTIVITY_TYPE_ID);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EDIT_ACHIEVEMENT_TECHNICAL_ACTIVITY_TYPE", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EditBasicDetailsData-> " + ex.ToString());
            }
            return ds;
        }


        public int UpdateTechnicalActivityData(AchievementBasicDetailsEntity objBDE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_TECHNICALACTIVITY_TYPE_ID",objBDE.technical_type_id),
                            new SqlParameter("@P_TECHNICALACTIVITY_TYPE", objBDE.technical_type),
                            new SqlParameter("@P_ACTIVE_STATUS", objBDE.IsActive),
                          };


                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_ACHIEVEMENT_TECHNICAL_ACTIVITY_TYPE", sqlParams, false);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);



            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.UpdateBasicDetailsData-> " + ex.ToString());
            }
            return retStatus;
        }

        public int InsertParticipationType(AchievementBasicDetailsEntity objBDE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            
                            new SqlParameter("@P_PARTICIPATION_TYPE", objBDE.participation_type),
                            new SqlParameter("@P_ACTIVE_STATUS", objBDE.IsActive),
                            new SqlParameter("@P_OrganizationId",objBDE.OrganizationId),
                             new SqlParameter("@P_OUTPUT", SqlDbType.Int),
                              };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_ACHIEVEMENT_PARTICIPATION_TYPE", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.BasicDetaisls-> " + ex.ToString());
            }
            return retStatus;
        }




        public DataSet ParticipationTypeListview()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SELECT_ACHIEVEMENT_PARTICIPATION_TYPE", sqlParams);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BasicDetaisls-> " + ex.ToString());
            }
            return ds;
        }
        public DataSet EditParticipationType(int PARTICIPATION_TYPE_ID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@P_PARTICIPATION_TYPE_ID", PARTICIPATION_TYPE_ID);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EDIT_ACHIEVEMENT_PARTICIPATION_TYPE", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EditBasicDetailsData-> " + ex.ToString());
            }
            return ds;
        }


        public int UpdateParticipationType(AchievementBasicDetailsEntity objBDE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_PARTICIPATION_TYPE_ID",objBDE.participation_type_id),
                            new SqlParameter("@P_PARTICIPATION_TYPE", objBDE.participation_type),
                            new SqlParameter("@P_ACTIVE_STATUS", objBDE.IsActive),
                          };


                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_ACHIEVEMENT_PARTICIPATION_TYPE", sqlParams, false);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);



            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.UpdateBasicDetailsData-> " + ex.ToString());
            }
            return retStatus;
        }

        public int InsertMoocsPlatformData(AchievementBasicDetailsEntity objBDE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            
                            new SqlParameter("@P_MOOCS_PLATFORM", objBDE.mooc_platform),
                            new SqlParameter("@P_ACTIVE_STATUS", objBDE.IsActive),
                            new SqlParameter("@P_OrganizationId",objBDE.OrganizationId),
                              new SqlParameter("@P_OUTPUT", SqlDbType.Int),
                              };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_ACHIEVEMENT_MOOCS_PLATFORM", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.BasicDetaisls-> " + ex.ToString());
            }
            return retStatus;
        }


        public DataSet MoocsPlatformListview()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SELECT_ACHIEVEMENT_MOOCS_PLATFORM", sqlParams);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BasicDetaisls-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet EditMoocsPlatform(int MOOCS_PLATFORM_ID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@P_MOOCS_PLATFORM_ID", MOOCS_PLATFORM_ID);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EDIT_ACHIEVEMENT_MOOCS_PLATFORM", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EditBasicDetailsData-> " + ex.ToString());
            }
            return ds;
        }

        /// <summary>
        /// Create By Diksha Nandurkar
        /// </summary>
        /// <param name="objBDE"></param>
        /// <returns></returns>

        public int UpdateAcadmic(AchievementBasicDetailsEntity objBDE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_ACADMIC_YEAR_ID",objBDE.acadamic_year_id),
                            new SqlParameter("@P_ACADMIC_YEAR_NAME", objBDE.acadamic_year_name),
                            new SqlParameter("@P_ACTIVE_STATUS", objBDE.IsActive),
                            new SqlParameter("@P_STDATE", objBDE.SDate),
                            new SqlParameter("@P_STATUS", objBDE.status),
                            new SqlParameter("@P_ENDDATE", objBDE.EDate),
                            
                          };


                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_ACHIEVEMENT_ACADMIC_YEAR", sqlParams, false);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);



            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.UpdateBasicDetailsData-> " + ex.ToString());
            }
            return retStatus;
        }


        public int UpdateMoocsPlatform(AchievementBasicDetailsEntity objBDE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_MOOCS_PLATFORM_ID",objBDE.moocs_platform_id),
                            new SqlParameter("@P_MOOCS_PLATFORM", objBDE.mooc_platform),
                            new SqlParameter("@P_ACTIVE_STATUS", objBDE.IsActive),
                          };


                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_ACHIEVEMENT_MOOCS_PLATFORM", sqlParams, false);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);



            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.UpdateBasicDetailsData-> " + ex.ToString());
            }
            return retStatus;
        }
        public int InsertDuration(AchievementBasicDetailsEntity objBDE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            
                            new SqlParameter("@P_DURATION", objBDE.duration),
                            new SqlParameter("@P_ACTIVE_STATUS", objBDE.IsActive),
                            new SqlParameter("@P_OrganizationId",objBDE.OrganizationId),
                             new SqlParameter("@P_OUTPUT", SqlDbType.Int),
                              };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_ACHIEVEMENT_ACHIEVEMENT_DURATION", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.BasicDetaisls-> " + ex.ToString());
            }
            return retStatus;
        }



        public DataSet DurationBindListview()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SELECT_ACHIEVEMENT_DURATION", sqlParams);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BasicDetaisls-> " + ex.ToString());
            }
            return ds;
        }


        public DataSet EditDuration(int DURATION_ID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@P_DURATION_ID", DURATION_ID);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EDIT_ACHIEVEMENT_DURATION", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EditBasicDetailsData-> " + ex.ToString());
            }
            return ds;
        }


        public int UpdateDurationData(AchievementBasicDetailsEntity objBDE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_DURATION_ID",objBDE.duration_id),
                            new SqlParameter("@P_DURATION", objBDE.duration),
                            new SqlParameter("@P_ACTIVE_STATUS", objBDE.IsActive),
                          };


                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_ACHIEVEMENT_DURATION", sqlParams, false);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);



            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.UpdateBasicDetailsData-> " + ex.ToString());
            }
            return retStatus;
        }
        /// <summary>
        /// Create by Diksha Nandurkar
        /// </summary>
        /// <param name="objBDE"></param>
        /// <returns></returns>
        public int InsertAcadmicYear(AchievementBasicDetailsEntity objBDE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                           
                            new SqlParameter("@P_ACADMIC_YEAR_NAME", objBDE.acadamic_year_name),
                            new SqlParameter("@P_STDATE", objBDE.SDate),
                            new SqlParameter("@P_ENDDATE", objBDE.EDate),
                            new SqlParameter("@P_ACTIVE_STATUS", objBDE.IsActive),
                            new SqlParameter("@P_STATUS", objBDE.status),
                            new SqlParameter("@P_OrganizationId",objBDE.OrganizationId),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int),
                              };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_ACHIEVEMENT_ACADMIC_YEAR", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.InsertAcadmicYear-> " + ex.ToString());
            }
            return retStatus;
        }
        public DataSet AcadmicYearListview()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SELECT_ACHIEVEMENT_ACADMIC_YEAR", sqlParams);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AcadmicYearListview-> " + ex.ToString());
            }
            return ds;
        }

        /// <summary>
        /// Create By Diksha Nandurkar
        /// </summary>
        /// <param name="ACADMIC_YEAR_ID"></param>
        /// <returns></returns>
        public DataSet EditAcadmic(int ACADMIC_YEAR_ID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@P_ACADMIC_YEAR_ID", ACADMIC_YEAR_ID);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EDIT_ACHIEVEMENT_ACADMIC_YEAR", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EditAcadmic-> " + ex.ToString());
            }
            return ds;
        }

        public int UpdateDuration(AchievementBasicDetailsEntity objBDE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_DURATION_ID",objBDE.duration_id),
                            new SqlParameter("@P_DURATION", objBDE.duration),
                            new SqlParameter("@P_ACTIVE_STATUS", objBDE.IsActive),
                          };


                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_ACHIEVEMENT_DURATION", sqlParams, false);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);



            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.UpdateBasicDetailsData-> " + ex.ToString());
            }
            return retStatus;
        }

        //Check Master Table ID reference and If refered then Prevent Master Data Inactive 
        public string CheckReferMasterTable(int MST_TBL_CODE, string MST_FORM_NAME, int COL_ID_VALUE)
        {
            string retStatus = string.Empty;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        {                            
                            new SqlParameter("@P_MST_TBL_CODE", MST_TBL_CODE),
                            new SqlParameter("@P_MST_FORM_NAME", MST_FORM_NAME),
                            new SqlParameter("@P_COL_ID_VALUE", COL_ID_VALUE),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int),
                        };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_VALIDATE_ACHIEVEMENT_MASTER", sqlParams, true);

                retStatus = ret.ToString();
            }
            catch (Exception ex)
            {
                retStatus = "-1,\"Exception\"";
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CheckReferMasterTable-> " + ex.ToString());
            }
            return retStatus;
        }

    }
}