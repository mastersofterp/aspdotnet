using IITMS;
using IITMS.SQLServer.SQLDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessLogic.PostAdmission
{
    public class ADMPFeesController
    {
        string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet RetrieveStudentDetailsNew(string search, string category)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_SEARCHSTRING", search);
                objParams[1] = new SqlParameter("@P_SEARCH", category);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMP_SEARCHSTUDENT_FESS_REFUND", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetStudentInfoByIdRefund(int studentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_USERNO", studentId),          
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ADMP_FEECOLLECT_GET_STUDENT_INFO_REFUND", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ADMPFeesController.GetStudentInfoByIdRefund() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetAllCollections(int studentId, int dcrNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_USERNO", studentId),
                    new SqlParameter("@P_DCR_NO", dcrNo)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ADMP_REFUND_GET_ALL_COLLECTION", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ADMPFeesController.GetAllCollections() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetAmountsToRefund(int dcr_no)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_DCR_NO", dcr_no)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_REFUND_FEE_ITEMS_AMOUNT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ADMPFeesController.GetAmountsToRefund() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

    }
}
