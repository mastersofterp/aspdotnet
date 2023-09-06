using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IITMS;
using System.Data.SqlClient;
using System.Data;
using IITMS.UAIMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;

namespace IITMS.UAIMS.BusinessLogicLayer.BusinessLogic
{
    public class MeterTeriffController
    {
        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddMeterTariff(MeterTarrif objMat)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_METERTYPE",    objMat.Metertype),
                    new SqlParameter("@P_METERUNITFROM",objMat.MeterunitFrom),
                    new SqlParameter("@P_METERUNITTO",  objMat.MeterunitTO),
                    new SqlParameter("@P_METERRATE",    objMat.MeterRate),
                    new SqlParameter("@P_OUTPUT",       objMat.MID)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ESTATE_ADDMETERTARRIF_MASTER", sqlParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MaterialMasterController.AddMaterialType() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }


    }
}
