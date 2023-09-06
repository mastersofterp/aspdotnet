using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;

namespace IITMS.UAIMS.BusinessLogicLayer.BusinessLogic
{
    public class ConsumerTypeMasterCont
    {
        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddConsumerType(ConsumerTypeMaster objMat)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_CONSUMER_TYPE_NAME",objMat.ConsumerType),
                    new SqlParameter("@P_OUTPUT", objMat.ConsumerTypeNo)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ESTATE_ADDCONSUMER_TYPE_MASTER", sqlParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConsumerTypeController.AddConsumerType() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }


    }
}

