//======================================================================================
// PROJECT NAME  : UAIMS (GPM)                                                               
// MODULE NAME   : Hostel Attendance Controller                                  
// CREATION DATE : 28-DEC-2022                                                       
// CREATED BY    : SONALI BHOR                                     
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================  

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;


namespace HostelBusinessLogicLayer.BusinessLogic.Hostel
{
    public class HostelAttendanceController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddUpdateHostelAttendanceIncharge(int inchargeno,int hostelno,int blockno, string floornos, int inchargeuano, string ipaddress, int userno, int collegecode, int orgid)
        {
            int status = 0;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_INCHARGE_ID" ,inchargeno),
                    new SqlParameter("@P_HOSTEL_NO", hostelno),
                    new SqlParameter("@P_BLOCK_NO", blockno),
                    new SqlParameter("@P_FLOOR_NOS", floornos),
                    new SqlParameter("@P_INCHARGE_UANO",inchargeuano),
                    new SqlParameter("@P_IPADDRESS",ipaddress),
                    new SqlParameter("@P_USERNO",userno),
                    new SqlParameter("@P_COLLEGE_CODE",collegecode),
                    new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),
                    new SqlParameter("@P_OUTPUT", status)                    
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_INSERT_UPDATE_ATTENDANCE_INCHARGE", sqlParams, true);

                if (Convert.ToInt32(obj) == -99)
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HostelSessionController.AddHostelSession() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }


        public DataSet GetAllIncharge(int inchargeid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_INCHARGE_ID",inchargeid),
                    new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]))
                };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_GET_ATTENDANCE_INCHARGE_DATA", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HostelSessionController.GetAllHostelSession() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;

        }

        //Added by Saurabh L on 07/01/2023 Purpose: To get Attendance submitted data by incharge to Admin
        public DataSet AllInchargeSubmittedAttnDatewise(int hostelSession, int hostelNo, DateTime attDate)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_HOSTEL_SESSIONNO",hostelSession),
                    new SqlParameter("@P_HOSTELNO",hostelNo),
                    new SqlParameter("@P_ATT_DATE",attDate),
                    new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]))
                };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_GET_ATTENDANCE_SUBMITTED_INCHARGE_DATA_DATEWISE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("HostelBusinessLogicLayer.BusinessLogic.Hostel.HostelAttendanceController.AllInchargeSubmittedAttnDatewise() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
    
    }
}
