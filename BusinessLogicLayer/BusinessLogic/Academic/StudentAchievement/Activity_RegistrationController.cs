//=================================================================================
// PROJECT NAME  : UAIMS                                                           
// MODULE NAME   : ACADEMIC
// PAGE NAME     : Activity Registraton Controller                                                 
// CREATION DATE : 20-SEP-2022
// CREATED BY    : NIKHIL SHENDE                          
// MODIFIED DATE : 
// MODIFIED BY   : 
// MODIFIED DESC : 
//=================================================================================
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

    public class Activity_RegistrationController
    {
        private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet ActivityRegistrationListView(int idno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@IDNO", idno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ACTIVELIST", sqlParams);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BasicDetaisls-> " + ex.ToString());
            }
            return ds;
        }

        public int InsertActiveActivityData(int Idno, int ClubNo, int Create_Event_Id, DateTime Registration_Date, int Created_By_Ua_No, int Flag)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            
                            new SqlParameter("@P_IDNO", Idno),
                            new SqlParameter("@P_CLUBNO",ClubNo),
                            new SqlParameter("@P_CREATE_EVENT_ID", Create_Event_Id),
                            new SqlParameter("@P_REGISTRATION_DATE",Registration_Date),
                            new SqlParameter("@P_CREATED_BY",Created_By_Ua_No),
                            new SqlParameter("@P_Flag",Flag),
                            //new SqlParameter("@P_CREATED_ON", Created_On_Date),
                            //new SqlParameter("@P_MODIFIED_BY", Modified_By_Ua_No),
                            //new SqlParameter("@P_MODIFIED_ON", Modified_on_Date),
                            new SqlParameter("@P_OUT", SqlDbType.Int),
                              };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_ACTIVE_STUDENT_ACTIVITY", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)

                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                }
                else if (Convert.ToInt32(ret) == -1001)
                {
                    retStatus = Convert.ToInt32(ret);
                }
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

        public int DeleteActivityRegistration(int idno, int act_id)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_IDNO", idno),
                            new SqlParameter("@P_ACT_ID",act_id),
                            new SqlParameter("@P_OUT",SqlDbType.Int),
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_DELETE_REGISTERED_EVENT", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)

                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.InsertCreateEventData-> " + ex.ToString());
            }
            return retStatus;
        }
    }
}
