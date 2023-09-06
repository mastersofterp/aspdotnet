//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC
// PAGE NAME     : CLUB ACTIVITY MASTER CONTROLLER                                            
// CREATION DATE : 02-09-2022                                                      
// CREATED BY    : NIKHIL SHENDE                                                  
// MODIFIED DATE :
// MODIFIED DESC :
//=======================================================================================
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
   public class Club_ActivityController
    {
       private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
     
        public int InsertClubActivity(string Name, string Description)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 

                            //new SqlParameter("@ACTIVITYID ", obja.ActivityId),
                            new SqlParameter("@P_ACTIVITY_NAME", Name),
                            new SqlParameter("@P_ACTIVITY_DESCRIPTION",Description),
            
                            new SqlParameter("@P_OUT", SqlDbType.Int),
                              };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;


                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_ACTIVITY_CLUB", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Club_ActivityController.InsertClubActivity-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet EditClubActivityData(int _Activity_Id)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@P_ACTIVITY_ID", _Activity_Id);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EDIT_CLUB_ACTIVITY", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EditBasicDetailsData-> " + ex.ToString());
            }
            return ds;
        }

        public int UpdateClubActivity( int Activity_Id, string Name, string Description)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 

                            new SqlParameter("@P_ACTIVITYID ", Activity_Id),
                            new SqlParameter("@P_ACTIVITY_NAME", Name),
                            new SqlParameter("@P_ACTIVITY_DESCRIPTION",Description),
            
                            new SqlParameter("@P_OUT", SqlDbType.Int),
                              };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;


                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_ACTIVITY_CLUB", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Club_ActivityController.InsertClubActivity-> " + ex.ToString());
            }
            return retStatus;
        }

    }
}

