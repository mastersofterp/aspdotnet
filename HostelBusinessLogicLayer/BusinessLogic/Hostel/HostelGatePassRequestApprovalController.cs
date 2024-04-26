using HostelBusinessLogicLayer.BusinessEntities.Hostel;
using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelBusinessLogicLayer.BusinessLogic.Hostel
{
    public class HostelGatePassRequestApprovalController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet GetAllRequests(int uano, string Applydate, int Purpose, string Todate, string Fromdate, string Status)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_UA_NO", uano);

                //Added By Himanshu Tamrakar 02042024
                objParams[1] = new SqlParameter("@P_TODATE", Todate); 
                objParams[2] = new SqlParameter("@P_FROMDATE", Fromdate);
                objParams[3] = new SqlParameter("@P_PURPOSE", Purpose);
                objParams[4] = new SqlParameter("@P_APPLYDATE", Applydate);
                objParams[5] = new SqlParameter("@P_STATUS", Status);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_GATEPASS_GET_APPROVAL_REQUEST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HostelGatePassRequestApprovalController.GetAllRequests() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet ShowStudentRequestDetails(int hgpid,int uano,int orgid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_HGPID", hgpid);
                objParams[1] = new SqlParameter("@P_UANO", uano);
                objParams[2] = new SqlParameter("@P_ORGID", orgid);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_GATEPASS_SHOW_REQUEST_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HostelGatePassRequestApprovalController.ShowStudentRequestDetails() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int ApproveGatepass(int Idno, string AttachmentPath, string AttachmentName, int HgpId, string Status, int orgid,int uano)
        {
            int status = 0;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[8];
                   sqlParams[0]= new SqlParameter("@P_IDNO", Idno);
                   sqlParams[1]= new SqlParameter("@P_ATTACHMENT", AttachmentPath);
                   sqlParams[2]= new SqlParameter("@P_ATTACHMENT_NAME", AttachmentName);
                   sqlParams[3] = new SqlParameter("@P_STATUS", Status);
                   sqlParams[4] = new SqlParameter("@P_ORGID", orgid);
                   sqlParams[5] = new SqlParameter("@P_UANO", uano); 
                   sqlParams[6] = new SqlParameter("@P_HGPID", HgpId);
                   sqlParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                   sqlParams[7].Direction = ParameterDirection.Output;


                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_GATEPASS_PARENT_APPROVAL", sqlParams, true);

                if (Convert.ToInt32(obj) == -99)
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic..HostelGatePassRequestApprovalController.AddParentApproval() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
    }
}
