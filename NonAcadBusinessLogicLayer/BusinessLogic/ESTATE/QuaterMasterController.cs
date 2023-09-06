using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;


namespace IITMS.UAIMS.BusinessLogicLayer.BusinessLogic
{
    public class QuaterMasterController
    {

        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddQuarter(Quater_Master objMat)
        {
            int status = 0;
            try
            {

                SQLHelper objSQLHelper   = new SQLHelper(_connectionString);                     		
                SqlParameter[] sqlParams = new SqlParameter[]                                   
                {                                                                                
                    new SqlParameter("@P_QUATER_TYPE",objMat.QrtType),
                    new SqlParameter("@P_QUATER_NO",objMat.QrtNo),
                    new SqlParameter("@P_EMETER_NO",objMat.MeterNumber),
                    new SqlParameter("@P_WMETER_NO",objMat.WaterNumber),
                    new SqlParameter("@P_QRTNAME",objMat.QrtName),
                    new SqlParameter("@P_BLOCK",objMat.block),
                    new SqlParameter("@P_CONNECTED_LOAD",objMat.ConnectedLoad),
                    new SqlParameter("@P_OUTPUT", objMat.QId)                    
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ESTATE_ADDQUATER_MASTER", sqlParams, true);

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

