using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

namespace  IITMS.UAIMS.BusinessLogicLayer.BusinessLogic
{
     public class MeterTypeNamecontroller
    {
         string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

         public int AddMeterType(MeterTypeName objMat)
         {
             int status = 0;
             try
             {
                 SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                 SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_MATERTYPENAME",objMat.MeterTypeName1),
                    new SqlParameter("@P_METERTYPENO",objMat.MeterTypeNo),
                    new SqlParameter("@P_OUTPUT", objMat.Mtype)
                };
                 sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                 object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ESTATE_ADDMETERTYPENAME_MASTER", sqlParams, true);

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
