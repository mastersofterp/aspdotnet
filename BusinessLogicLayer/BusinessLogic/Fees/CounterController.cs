//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : COUNTER CONTROLLER
// CREATION DATE : 07-OCT-2009
// CREATED BY    : AMIT YADAV
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Data;
using System.Data.SqlClient;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class CounterController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddCounter(Counter counter)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_COUNTER_NAME", counter.CounterName),
                    new SqlParameter("@P_PRINT_NAME", counter.PrintName),
                    new SqlParameter("@P_USER_NO", counter.CounterUserId),
                    new SqlParameter("@P_RECEIPT_PERMISSION", counter.ReceiptPermission),
                    new SqlParameter("@P_COLLEGE_CODE", counter.CollegeCode),
                    new SqlParameter("@P_COUNTER_NO", status)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_FEECOLLECT_COUNTER_INSERT", sqlParams, true);

                if (obj != null && obj.ToString() != "-99")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CounterController.AddCounter() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public int UpdateCounter(Counter counter)
        {
            int status;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_COUNTER_NAME", counter.CounterName),
                    new SqlParameter("@P_PRINT_NAME", counter.PrintName),
                    new SqlParameter("@P_USER_NO", counter.CounterUserId),
                    new SqlParameter("@P_RECEIPT_PERMISSION", counter.ReceiptPermission),
                    new SqlParameter("@P_COUNTER_NO", counter.CounterNo)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_FEECOLLECT_COUNTER_UPDATE", sqlParams, true);

                if (obj != null && obj.ToString() != "-99")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CounterController.UpdateAsset() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetAllCounters()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_FEECOLLECT_COUNTER_GET_ALL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CounterController.GetAllCounters() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetCounterByNo(int counterNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_COUNTER_NO", counterNo) };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_FEECOLLECT_COUNTER_GET_BY_NO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CounterController.GetCounterByNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
    }
}