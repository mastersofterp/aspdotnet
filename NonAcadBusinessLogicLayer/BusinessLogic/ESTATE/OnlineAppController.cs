using System;
using System.Collections.Generic;
using System.Linq;

using System.Data.SqlClient;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;

namespace IITMS.UAIMS.BusinessLogicLayer.BusinessLogic
{
    public class OnlineAppController
    {
        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        #region Online Application

        public long AddUpdateOnlineApplication(OnlineApp objOApp)
        {
            long retstatus = 0;
            try
            {

                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_APP_ID", objOApp.APPID);
                objParams[1] = new SqlParameter("@P_EMP_ID", objOApp.EMPID);
                objParams[2] = new SqlParameter("@P_TOTAL_MEMBER", objOApp.TOTAL_MEMBERS);
                objParams[3] = new SqlParameter("@P_REMARK", objOApp.REMARK);
                objParams[4] = new SqlParameter("@P_STATUS", objOApp.STATUS);
                objParams[5] = new SqlParameter("@P_QRT_TYPE", objOApp.QRT_TYPE);
                objParams[6] = new SqlParameter("@P_APPLICATION_DATE", objOApp.APPLICATION_DATE);      
                objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTATE_ONLINE_APPLICATION_INSERT_UPDATE", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else if (Convert.ToInt32(ret) == 2627)
                    retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                else
                    retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retstatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OnlineAppController.AddUpdateOnlineApplication->" + ex.ToString());
            }
            return retstatus;
        }


        #endregion

    }
}
