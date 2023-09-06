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


namespace IITMS.UAIMS.BusinessLogicLayer.BusinessLogic
{
 
    public class Monthlybilllock_controller
    {
        public string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        //public int Monthly_billLock(string pDate, DateTime dueDate, DateTime lastDate, string pStatus)//,string pWaterSatus)
        //{
        //    int status = 0;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[]
        //        {
        //            new SqlParameter("@P_DATE",pDate),
        //            new SqlParameter("@P_STATUS",pStatus),                   
        //            new SqlParameter("@P_DUE_DATE", dueDate),
        //            new SqlParameter("@P_LAST_DATE",lastDate),
        //            //new SqlParameter("@P_WATERSTATUS",pWaterSatus),
        //            new SqlParameter("@P_OUTPUT","1")                 
        //        };

        //        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
        //        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ESTATE_MONTHLY_BILLLOCK", sqlParams, true);
                
        //        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
        //            status = Convert.ToInt32(CustomStatus.RecordSaved);
        //        else
        //            status = Convert.ToInt32(CustomStatus.Error);
        //    }
        //    catch (Exception ex)
        //    {

        //        status = Convert.ToInt32(CustomStatus.Error);
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Monthly_billLock.Monthly_billLock() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return status;
        //}

        public int Monthly_billLock(DateTime SelectDate, DateTime dueDate, DateTime lastDate, string pStatus)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_DATE", SelectDate);
                objParams[1] = new SqlParameter("@P_STATUS", pStatus); 
                if (dueDate == DateTime.MinValue)
                {
                    objParams[2] = new SqlParameter("@P_DUE_DATE", DBNull.Value);
                }
                else
                {
                    objParams[2] = new SqlParameter("@P_DUE_DATE", dueDate);
                }
                if (lastDate == DateTime.MinValue)
                {
                    objParams[3] = new SqlParameter("@P_LAST_DATE", DBNull.Value);
                }
                else
                {
                    objParams[3] = new SqlParameter("@P_LAST_DATE", lastDate);
                }
                objParams[4] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[4].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ESTATE_MONTHLY_BILLLOCK", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    retStatus = Convert.ToInt32(CustomStatus.Error);

                //object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_IO_TRAN_INWARD_INSERT", objParams, true);

                //if (obj != null && obj.ToString().Equals("-99"))
                //{
                //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //}
                //else
                //{
                //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //}
            }
            catch (Exception ex)
            {
                retStatus = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranController.AddInwardEntry-> " + ex.ToString());
            }
            return retStatus;
        }      


        public int ElectricityBillProcess(string proDate, string NameId )
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_DATE", proDate);
                objParams[1] = new SqlParameter("@P_NAMEID", NameId);
                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ESTATE_MONTHLY_NEW", objParams, true);
                if (obj != null && obj.ToString().Equals("-99"))
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
            }
            catch (Exception ex)
            {
                retStatus = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IOTranCCController.AddInwardCCEntry-> " + ex.ToString());
            }
            return retStatus;
        }


    }

}
