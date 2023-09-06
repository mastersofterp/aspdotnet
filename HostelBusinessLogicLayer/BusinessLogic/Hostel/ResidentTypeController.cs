//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : HOSTEL
// PAGE NAME     : RESIDENT TYPE CONTROLLER
// CREATION DATE : 18-AUG-2009
// CREATED BY    : AMIT YADAV
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Data;
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class ResidentTypeController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddResidentType(string residentType, bool isStudent, string collegeCode)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_RESIDENT_TYPE_NAME", residentType),
                    new SqlParameter("@P_IS_STUDENT", isStudent),
                    new SqlParameter("@P_COLLEGE_CODE", collegeCode),
                    new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),
                    new SqlParameter("@P_RESIDENT_TYPE_NO", status)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_RESIDENT_TYPE_INSERT", sqlParams, true);

                if (Convert.ToInt32(obj) == -99)
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ResidentTypeController.AddResidentType() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public int UpdateResidentType(int residentTypeNo, string residentType, bool isStudent)
        {
            int status;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_RESIDENT_TYPE_NAME", residentType),
                    new SqlParameter("@P_IS_STUDENT", isStudent),
                    new SqlParameter("@P_RESIDENT_TYPE_NO", residentTypeNo)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_RESIDENT_TYPE_UPDATE", sqlParams, true);

                if (Convert.ToInt32(obj) == -99)
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ResidentTypeController.UpdateResidentType() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetAllResidentTypes()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]))
                };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_RESIDENT_TYPE_GET_ALL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ResidentTypeController.GetAllResidentTypes() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetResidentTypeByNo(int residentTypeNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] 
                { new SqlParameter("@P_RESIDENT_TYPE_NO", residentTypeNo) };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_RESIDENT_TYPE_GET_BY_NO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ResidentTypeController.GetResidentTypeByNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
    }
}