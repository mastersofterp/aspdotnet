using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class StudentSelectFieldController
    {
        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet GetSingleSelectedFieldTable(int SFTRXNO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlparam = new SqlParameter[]
                            { 
                              new SqlParameter("@P_SFTRXNO",SFTRXNO)                              
                            };
                ds = objSQLHelper.ExecuteDataSetSP("GETSINGLE_SELECTED_FIELD_TABLE", sqlparam);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PFCONTROLLER.GetSingleFieldTable() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet GetSelectedSftrxno(string SFTRXNO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlparam = new SqlParameter[]
                            { 
                              new SqlParameter("@P_SFTRXNO",SFTRXNO)                              
                            };
                ds = objSQLHelper.ExecuteDataSetSP("GETSELECTED_SELECTED_FIELD_TABLE", sqlparam);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PFCONTROLLER.GetSingleFieldTable() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet EmployeeSelectedFieldReport(string TABLENAMES, string COLUMNNAMES, string WHERECONDITION, string ORDERBY)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlparam = new SqlParameter[]
                            { 
                              new SqlParameter("P_TABLENAMES",TABLENAMES),
                              new SqlParameter("@P_COLUMNNAMES",COLUMNNAMES), 
                              new SqlParameter("@P_WHERECONDITION",WHERECONDITION), 
                              new SqlParameter("@P_ORDERBY",(ORDERBY ==null) ?  DBNull.Value as object:ORDERBY)  
                            };
                ds = objSQLHelper.ExecuteDataSetSP("PKG_SELECTED_FIELD_REPORT", sqlparam);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PFCONTROLLER.EmployeeSelectedFieldReport() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
    }
}
