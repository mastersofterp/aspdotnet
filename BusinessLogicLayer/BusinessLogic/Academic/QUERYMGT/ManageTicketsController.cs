using IITMS.SQLServer.SQLDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class ManageTicketsController
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;



                public DataSet GetDataBYID(int ID)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_OUTPUT", ID) };

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_QM_ManageTicket_GETDATABYID", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetAchievementNo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }


                public DataSet GETDATA( int UNO, char status)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_UA_NO", UNO), new SqlParameter("@P_Status", status) };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_QM_ManageTicket_GETDATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetAllAchievement() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }


                public int AddManageTicket(ManageTickets objMT)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[12];

                        sqlParams[0] = new SqlParameter("@P_QMRaiseTicketID", objMT.QMRaiseTicketID);
                        sqlParams[1] = new SqlParameter("@P_PendingID", objMT.PendingId);
                        sqlParams[2] = new SqlParameter("@P_Status", objMT.Status);
                        sqlParams[3] = new SqlParameter("@P_Remark", objMT.Remark);
                        sqlParams[4] = new SqlParameter("@P_Filepath", objMT.Filepath);
                        sqlParams[5] = new SqlParameter("@P_Filename", objMT.Filename);
                        sqlParams[6]=  new SqlParameter("@P_UserId",objMT.UserId);
                        sqlParams[7] = new SqlParameter("@P_IsStudentRemark", objMT.IsStudentRemark);
                        sqlParams[8] = new SqlParameter("@P_InfoReq", objMT.RequiredInformation);
                        sqlParams[9] = new SqlParameter("@P_IsDeptChange", objMT.IsDeptChange);
                        sqlParams[10] = new SqlParameter("@P_NewDeptId", objMT.NewDeptId);
                        sqlParams[11] = new SqlParameter("@P_Out", SqlDbType.Int);
                        sqlParams[11].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_QM_ManageTicket_INSERT", sqlParams, true);
                        status = (Int32)obj;
                    }
                    catch (Exception ex)
                    {
                        status = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddAchievementMaster() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;

                }

                // add 24-08-2022 amol
                // show student history 

                public DataSet GetStudentTicketHistory(int ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_STUDENTID", ID) };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_QM_ManageTicket_GETHISTORYDETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetAllAchievement() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                //24-08-2022
                // student history name ,sem, branch,year
                public DataSet GetDetailsById(int ID)
                {
                    DataSet ds1 = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_OUTPUT", ID) };

                        ds1 = objSQLHelper.ExecuteDataSetSP("PKG_QM_ManageTicket_GETDETAILSBYID", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetAchievementNo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds1;
                }
                public int UpdateManageTicketChangeDept(int requestid,int request_type,int category_id,int subcategory_id)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[5];

                        sqlParams[0] = new SqlParameter("@P_QMRaiseTicketID", requestid);
                        sqlParams[1] = new SqlParameter("@P_QMRequestTypeID", request_type);
                        sqlParams[2] = new SqlParameter("@P_QMRequestCategoryID", category_id);
                        sqlParams[3] = new SqlParameter("@P_QMRequestSubCategoryID",subcategory_id);
                        sqlParams[4] = new SqlParameter("@P_Out", SqlDbType.Int);
                        sqlParams[4].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_QM_ManageTicket_ChangeDept", sqlParams, true);
                        status = (Int32)obj;
                    }
                    catch (Exception ex)
                    {
                        status = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddAchievementMaster() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;

                }
            }
        }
    }
}
