using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessLogic.PostAdmission
{
    public class ACDAnalyticsDashboardConfigController
    {
        private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public string InsertAnalyticsDashbord(string UaNo, int UaType, string Dashboard, int UserNo)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;


                objParams = new SqlParameter[5];
                
                objParams[0] = new SqlParameter("@P_UA_NO", UaNo);
                objParams[1] = new SqlParameter("@P_UA_TYPE", UaType);
                objParams[2] = new SqlParameter("@P_DASHBOARDNO", Dashboard);
                objParams[3] = new SqlParameter("@P_CREATEDBY", UserNo);

                objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[4].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_ANALYTICS_DASHBOARD_USER_CONFIG", objParams, true);
                return ret.ToString();

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("Crescent.BusinessLayer.BusinessLogic.ACDAnalyticsDashboardConfigController.InsertAnalyticsDashbord-> " + ex.ToString());
            }
        }
        public string UpdateAnalyticsDashbord(string UaNo, int UaType, string Dashboard, int UserNo)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;


                objParams = new SqlParameter[5];

                objParams[0] = new SqlParameter("@P_UA_NO", UaNo);
                objParams[1] = new SqlParameter("@P_UA_TYPE", UaType);
                objParams[2] = new SqlParameter("@P_DASHBOARDNO", Dashboard);
                objParams[3] = new SqlParameter("@P_CREATEDBY", UserNo);

                objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[4].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_ANALYTICS_DASHBOARD_USER_CONFIG", objParams, true);
                return ret.ToString();

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("Crescent.BusinessLayer.BusinessLogic.ACDAnalyticsDashboardConfigController.InsertAnalyticsDashbord-> " + ex.ToString());
            }
        }
        public DataSet GetUserList(int UserType)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_UA_TYPE", UserType);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ALL_ANALYTICS_DASHBOARD_USER_CONFIG", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("Crescent.BusinessLayer.BusinessLogic.ACDAnalyticsDashboardConfigController.GetUserList->" + ex.ToString());
            }
            return ds;
        }
        public DataSet GetSingleUserInformation(int UaNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_UA_NO", UaNo);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_ANALYTICS_DASHBOARD_USER_CONFIG", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("BusinessLayer.BusinessLogic.StudentOnlineAdmissionMasterController.GetSingleSubjectTypeInformation->" + ex.ToString());
            }
            return ds;

        }
    }
}
