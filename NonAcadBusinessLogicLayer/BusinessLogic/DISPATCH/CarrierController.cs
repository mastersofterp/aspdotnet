using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlTypes;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Dispatch;



namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLogicLayer.BusinessLogic
        {
            public class CarrierController
    {
        private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddUpdateCarrier(CarrierMaster objCM)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_CARRIERNO", objCM.carrierNo);
                objParams[1] = new SqlParameter("@P_CARRIERNAME", objCM.carrierName);
                objParams[2] = new SqlParameter("@P_COLLEGE_CODE", objCM.college_code);
                objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_IO_CARRIER_INSERT_UPDATE", objParams, true);

                if (obj != null && obj.ToString().Equals("-99"))
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else if (obj.ToString().Equals("2627"))
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
            }
            catch (Exception ex)
            {
                retStatus = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.WeightController.AddWeight-> " + ex.ToString());

            }
            return retStatus;
        }

        /// <summary>
        /// DeleteCarrier method is used to delete existing  carrier.
        /// </summary>          

        public int DeleteCarrier(int carrierNo)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_CARRIERNO", carrierNo);
                objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_IO_DEL_CARRIER", objParams, false);
                retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CarrierController.DeleteCarrier-> " + ex.ToString());
            }
            return Convert.ToInt32(retStatus);
        }
    }
        }
    }
}
