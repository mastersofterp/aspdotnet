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
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;

namespace IITMS.UAIMS.BusinessLogicLayer.BusinessLogic
{
    public class MeterTypeChargesMasterController
    {
        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
       
        
        
        public int AddMeterTypeCharges(MeterTypeCharges_Master objMat)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);         	
                SqlParameter[] sqlParams = new SqlParameter[]                      	
                {                                                                  	
                    new SqlParameter("@P_MTYPE_NO",objMat.MtypeNo),           
                    new SqlParameter("@P_FIX_CHRG",objMat.FixedCharges),                		
                    new SqlParameter("@P_FCA_CHRG",objMat.FcaCharges),   		
                    new SqlParameter("@P_ELECT_DUTY",objMat.ElictricDuty),                       				
                    new SqlParameter("@P_AVG_UN",objMat.MeterAvgUnit),           
                    new SqlParameter("@P_LOCK_UN",objMat.MeterLockUnit),
                    new SqlParameter("@P_ACC_CHRG",objMat.MeterAccCharges),           
                    new SqlParameter("@P_M_RENT",objMat.MeterRent), 
                    new SqlParameter("@P_OUTPUT",objMat.MCharges )  
                
                                            		
                };                                                                  
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_EST_INS_CHRG", sqlParams, true);

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
