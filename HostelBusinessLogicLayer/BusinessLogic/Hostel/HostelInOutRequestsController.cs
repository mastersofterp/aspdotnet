using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.Services;
namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class HostelInOutRequestsController
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                public DataSet GetAllRequestsBySearch(HostelInOutReq ObjHReq,string applydate,string todate,string fromdate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_APPLYDATE", applydate);
                        objParams[1] = new SqlParameter("@P_PURPOSE", ObjHReq.Purpose);
                        objParams[2] = new SqlParameter("@P_GATEPASSCODE", ObjHReq.Gatepassno);
                        objParams[3] = new SqlParameter("@P_TODATE", todate);
                        objParams[4] = new SqlParameter("@P_FROMDATE", fromdate);
                        objParams[5] = new SqlParameter("@P_STATUS", ObjHReq.Status);
                        
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_GATEPASS_GET_REQUESTS_BY_SEARCH", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HostelInOutRequestsController.GetAllRequestsBySearch() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
                public SqlDataReader GetMoreDetails(int Idno, int Hgpid)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", Idno);
                        objParams[1] = new SqlParameter("@P_HGP_ID",Hgpid);


                        dr = objSQLHelper.ExecuteReaderSP("PKG_HOSTEL_GATEPASS_GET_MORE_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HostelInOutRequestsController.GetMoreDetails() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return dr;
                }

                public SqlDataReader GetInMateDetails(int Idno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", Idno);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_HOSTEL_GATEPASS_GET_INMATE_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Hostel.HostelInOutRequestsController.GetInMateDetails() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return dr;
                }

                public int InsertAttachedDocuments(HostelInOutReq ObjHReq)
                {
                     int retStatus1 = 0;
                     int retStatus = Convert.ToInt32(CustomStatus.Others);
                     try
                     {
                         SQLHelper objSqlhelper = new SQLHelper(_UAIMS_constr);
                         SqlParameter[] sqlParams = null;
                         {
                             sqlParams = new SqlParameter[5];
                             sqlParams[0]= new SqlParameter("@P_HGP_ID", ObjHReq.Hgpid);
                             sqlParams[1]=new SqlParameter("@P_IDNO", ObjHReq.Idno);
                             sqlParams[2]=new SqlParameter("@P_DOCUMENT", ObjHReq.UploadedFile);
                             sqlParams[3]= new SqlParameter("@P_DOCUMENT_NAME",ObjHReq.UploadedfileName);
                             sqlParams[4]= new SqlParameter("@P_OUT",SqlDbType.Int);
                             sqlParams[4].Direction = ParameterDirection.Output;
                         };

                         object Status = objSqlhelper.ExecuteNonQuerySP("PKG_HOSTEL_GATEPASS_INSERT_UPLOAD_DOCUMENT_DETAILS", sqlParams, true);

                         retStatus1 = Convert.ToInt32(Status);
                         return retStatus1;
                     }
                     catch (Exception ex)
                     {
                         throw new IITMSException("IITMS.UAIMS.HostelBusinessLogicLayer.HostelInOutRequestsController.InsertAttachedDocuments() --> " + ex.Message + " " + ex.StackTrace);
                     }
                 }
                public int ChangeParentApprovalStatus(int hgpid,char pa_status)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSqlhelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = null;
                        {
                            sqlParams = new SqlParameter[2];
                            sqlParams[0] = new SqlParameter("@P_HGPID", hgpid);
                            sqlParams[1] = new SqlParameter("@P_PA_STATUS", pa_status);
                        };
                        if (objSqlhelper.ExecuteNonQuerySP("PKG_HOSTEL_GATEPASS_CHANGE_PARENT_APPROVAL_STATUS", sqlParams, true) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.HostelBusinessLogicLayer.HostelInOutRequestsController.ChangeParentApprovalStatus() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return retStatus;
                }

             }
        }
    }
}
