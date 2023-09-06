using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data;
using System.Data.SqlClient;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;


namespace IITMS.UAIMS.BusinessLogicLayer.BusinessLogic
{
    public class MeterNumberMasterController
    {
        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddMeterNumber(MeterNumber_Master objMat)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_MTYPE_NO",objMat.MeterTypeNo),      
                    new SqlParameter("@P_METER_NO",objMat.MeterNo),             
                    new SqlParameter("@P_EMULTI_METER",objMat.EmeterMultiple),  
                    new SqlParameter("@P_RENT",objMat.Rent),                    		
                    new SqlParameter("@P_OUTPUT",objMat.MType)                               
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ESTATE_ADDMETERNO_MASTER", sqlParams, true);

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
