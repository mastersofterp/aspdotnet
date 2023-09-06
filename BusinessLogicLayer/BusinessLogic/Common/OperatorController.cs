//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : COMMON
// PAGE NAME     : OPERATOR CONTROLLER
// CREATION DATE : 18-NOV-2009
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
    public class OperatorController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddOperator(Operator opr)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_OPERATOR_NAME", opr.OperatorName),
                    new SqlParameter("@P_USER_NO", opr.UserNo),
                    new SqlParameter("@P_SESSION_NO", opr.SessionNo),
                    new SqlParameter("@P_ACTIVATION_DATE", opr.ActivationDate),
                    new SqlParameter("@P_ACTIVE", opr.IsActive),
                    new SqlParameter("@P_PERM_MENU_LEVEL1", opr.MenuLevel1_Perms),
                    new SqlParameter("@P_PERM_MENU_LEVEL2", opr.MenuLevel2_Perms),
                    new SqlParameter("@P_PERM_MENU_LEVEL3", opr.MenuLevel3_Perms),
                    new SqlParameter("@P_COLLEGE_CODE", opr.CollegeCode),
                    new SqlParameter("@P_OPERATOR_NO", status)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_WIN_OPERATOR_INSERT", sqlParams, true);

                if (obj != null && obj.ToString() != "-99")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OperatorController.AddOperator() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public int UpdateOperator(Operator opr)
        {
            int status;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_OPERATOR_NAME", opr.OperatorName),
                    new SqlParameter("@P_USER_NO", opr.UserNo),
                    new SqlParameter("@P_SESSION_NO", opr.SessionNo),
                    new SqlParameter("@P_ACTIVE", opr.IsActive),
                    new SqlParameter("@P_PERM_MENU_LEVEL1", opr.MenuLevel1_Perms),
                    new SqlParameter("@P_PERM_MENU_LEVEL2", opr.MenuLevel2_Perms),
                    new SqlParameter("@P_PERM_MENU_LEVEL3", opr.MenuLevel3_Perms),
                    new SqlParameter("@P_OPERATOR_NO", opr.OperatorNo)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_WIN_OPERATOR_UPDATE", sqlParams, true);

                if (obj != null && obj.ToString() != "-99")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OperatorController.UpdateAsset() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetAllOperators()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_WIN_OPERATOR_GET_ALL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OperatorController.GetAllOperators() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetOperatorByNo(int operatorNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_OPERATOR_NO", operatorNo) };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_WIN_OPERATOR_GET_BY_NO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OperatorController.GetOperatorByNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public object ValidateOperator(string operatorName, int sessionNo)
        {
            object count = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_OPERATOR_NAME", operatorName),
                    new SqlParameter("@P_SESSIONNO", sessionNo) 
                };
                count = objSQLHelper.ExecuteScalarSP("PKG_WIN_OPERATOR_VALIDATE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OperatorController.GetOperatorByNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return count;
        }
    }
}