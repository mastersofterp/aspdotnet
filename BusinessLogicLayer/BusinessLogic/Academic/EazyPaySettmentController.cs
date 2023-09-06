using IITMS;
using IITMS.SQLServer.SQLDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessLogic.Academic
{
    public class EazyPaySettmentController
    {
        string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int InsertSettelmentDate(string OrderId,DateTime TransactionDate, DateTime SettlementDate) 
        {
            //DataSet ds = null;
            int retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                {
                    objParams[0] = new SqlParameter("@P_ORDERID", OrderId);
                    objParams[1] = new SqlParameter("@P_TRANDATE", TransactionDate);
                    objParams[2] = new SqlParameter("@P_SETTELMENTDATE", SettlementDate);                    
                    objParams[3] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    objParams[3].Direction = ParameterDirection.Output;
                };
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_UPDATE_SETTELMENTDATE", objParams, true);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EazyPaySettmentController.InsertSettelmentDate()-->" + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }


        public DataSet GetSettemlmentReport(DateTime FromDate, DateTime ToDate)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_FromDate", FromDate);
                objParams[1] = new SqlParameter("@P_ToDate", ToDate);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SETTELMENT_REPORT_DATA", objParams);

            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUserLinkDomain-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetOrderId(DateTime FromDate, DateTime ToDate)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_FromDate", FromDate);
                objParams[1] = new SqlParameter("@P_ToDate", ToDate);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ORDERID_FOR_SETTELMENT", objParams);

            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EazyPaySettmentController.GetOrderId-> " + ex.ToString());
            }
            return ds;
        }
    }
}
