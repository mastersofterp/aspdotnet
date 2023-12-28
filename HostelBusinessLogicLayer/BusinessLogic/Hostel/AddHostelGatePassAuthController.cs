using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Data;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class AddHostelGatePassAuthController
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public int UpdateAuthApprovalPath(AddHostelGatePassAuthApproval OBJAAP, int STUDTYPE, int DAYS, string APPROVAL_1, string APPROVAL_2, string APPROVAL_3, string APPROVAL_4, string APPROVAL_5)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[11];
                        objparams[0] = new SqlParameter("@P_APP_NO", OBJAAP.APP_NO);
                        objparams[1] = new SqlParameter("@P_STUDTYPE", STUDTYPE);
                        objparams[2] = new SqlParameter("@P_DAYS", DAYS);
                        objparams[3] = new SqlParameter("@P_APPROVAL1", APPROVAL_1);
                        objparams[4] = new SqlParameter("@P_APPROVAL2", APPROVAL_2);
                        objparams[5] = new SqlParameter("@P_APPROVAL3", APPROVAL_3);
                        objparams[6] = new SqlParameter("@P_APPROVAL4", APPROVAL_4);
                        objparams[7] = new SqlParameter("@P_APPROVAL5", APPROVAL_5);
                        objparams[8] = new SqlParameter("@P_CREATED_BY", OBJAAP.CREATED_BY);
                        objparams[9] = new SqlParameter("@P_IP_ADDRESS", OBJAAP.IP_ADDRESS);
                        objparams[10] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objparams[10].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_Add_APPROVAL_MASTER_UPDATE", objparams, true);
                        if (Convert.ToInt32(ret) == 1)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else if (ret.Equals(0))
                            retstatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AddHostelGatePassAuthController.UpdateAuthApprovalPath->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetSingleAAPath(int APP_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_APP_NO", APP_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSETL_AUTHORITY_APPROVAL_MASTER_GET_BY_NO", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AddHostelGatePassAuthController.GetSingleAAPath->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetAllAuthApprovalPathMaster()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objparams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_Add_APPROVAL_MASTER_GET_BYALL", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AddHostelGatePassAuthController.GetAllAuthApprovalPathMaster->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public int AddAuthApprovalPath(AddHostelGatePassAuthApproval OBJAAP, int STUDTYPE, int DAYS, string APPROVAL_1, string APPROVAL_2, string APPROVAL_3, string APPROVAL_4, string APPROVAL_5)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[11];
                        objparams[0] = new SqlParameter("@P_APPNO", OBJAAP.APP_NO);
                        objparams[1] = new SqlParameter("@P_STUDTYPE", STUDTYPE);
                        objparams[2] = new SqlParameter("@P_DAYS", DAYS);
                        objparams[3] = new SqlParameter("@P_APPROVAL1", APPROVAL_1);
                        objparams[4] = new SqlParameter("@P_APPROVAL2", APPROVAL_2);
                        objparams[5] = new SqlParameter("@P_APPROVAL3", APPROVAL_3);
                        objparams[6] = new SqlParameter("@P_APPROVAL4", APPROVAL_4);
                        objparams[7] = new SqlParameter("@P_APPROVAL5", APPROVAL_5);
                        objparams[8] = new SqlParameter("@P_CREATED_BY", OBJAAP.CREATED_BY);
                        objparams[9] = new SqlParameter("@P_IP_ADDRESS", OBJAAP.IP_ADDRESS);
                        objparams[10] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objparams[10].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_Add_APPROVAL_MASTER_INS", objparams, true);

                        if (Convert.ToInt32(ret) == 2627)
                            retstatus = (Convert.ToInt32(ret));
                        else if (Convert.ToInt32(ret) == 1027)
                            retstatus = (Convert.ToInt32(ret));
                        else if (ret != null && ret.ToString() != "-99" && ret.ToString() != "-1001")
                            retstatus = (Convert.ToInt32(ret));
                        else
                            retstatus = (Convert.ToInt32(ret));
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AddHostelGatePassAuthController.AddAuthApprovalPath->" + ex.ToString());
                    }
                    return retstatus;
                }

                //-------------------

                public int UpdateAAPath(AddHostelGatePassAuthApproval OBJAAP, int hostel, int dept)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[14];
                        objparams[0] = new SqlParameter("@P_APP_NO", OBJAAP.APP_NO);
                        objparams[1] = new SqlParameter("@P_AUTHORITY", OBJAAP.AUTHORITY);
                        objparams[2] = new SqlParameter("@P_AANO1", OBJAAP.APPROVAL_1);
                        objparams[3] = new SqlParameter("@P_AANO2", OBJAAP.APPROVAL_2);
                        objparams[4] = new SqlParameter("@P_AANO3", OBJAAP.APPROVAL_3);
                        objparams[5] = new SqlParameter("@P_AANO4", OBJAAP.APPROVAL_4);
                        objparams[6] = new SqlParameter("@P_AANO5", OBJAAP.APPROVAL_5);
                        objparams[7] = new SqlParameter("@P_STUDTYPE", OBJAAP.STUDTYPE);
                        objparams[8] = new SqlParameter("@P_DAYS", OBJAAP.DAYS);
                        objparams[9] = new SqlParameter("@P_AAPATH", OBJAAP.AAPATH);
                        objparams[10] = new SqlParameter("@P_CREATED_BY", OBJAAP.CREATED_BY);
                        objparams[11] = new SqlParameter("@P_DEPT_NO", dept);
                        objparams[12] = new SqlParameter("@P_IP_ADDRESS", OBJAAP.IP_ADDRESS);
                        objparams[13] = new SqlParameter("@P_HOSTEL", hostel);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_AUTHORITY_APPROVAL_MASTER_UPDATE", objparams, true) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AddHostelGatePassAuthController.UpdateAAPath->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetAllAAMaster()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objparams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_AUTHORITY_APPROVAL_MASTER_GET_BYALL", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AddHostelGatePassAuthController.GetAllAAMaster->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetLabel(AddHostelGatePassAuthApproval OBJAAP)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objparams = new SqlParameter[2];
                        objparams[0] = new SqlParameter("@P_STUTYPE", OBJAAP.STUDTYPE);
                        objparams[1] = new SqlParameter("@P_DAYS", OBJAAP.DAYS);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_AUTHORITY_APPROVAL_LABEL_MASTER", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AddHostelGatePassAuthController.GetAllAAMaster->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int AddAAPath(AddHostelGatePassAuthApproval OBJAAP, int hostel, int dept)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[17];
                        objparams[0] = new SqlParameter("@P_APPNO", OBJAAP.APP_NO);
                        objparams[1] = new SqlParameter("@P_AUTHORITY", OBJAAP.AUTHORITY);
                        objparams[2] = new SqlParameter("@P_AUTHORITY_NAME", OBJAAP.AUTHORITY_NAME);
                        objparams[3] = new SqlParameter("@P_AANO1", OBJAAP.APPROVAL_1);
                        objparams[4] = new SqlParameter("@P_AANO2", OBJAAP.APPROVAL_2);
                        objparams[5] = new SqlParameter("@P_AANO3", OBJAAP.APPROVAL_3);
                        objparams[6] = new SqlParameter("@P_AANO4", OBJAAP.APPROVAL_4);
                        objparams[7] = new SqlParameter("@P_AANO5", OBJAAP.APPROVAL_5);
                        objparams[8] = new SqlParameter("@P_STUDTYPE", OBJAAP.STUDTYPE);
                        objparams[9] = new SqlParameter("@P_DAYS", OBJAAP.DAYS);
                        objparams[10] = new SqlParameter("@P_AAPATH", OBJAAP.AAPATH);
                        objparams[11] = new SqlParameter("@P_CREATED_BY", OBJAAP.CREATED_BY);
                        objparams[12] = new SqlParameter("@P_IP_ADDRESS", OBJAAP.IP_ADDRESS);
                        objparams[13] = new SqlParameter("@P_DEPT_NO", dept);
                        objparams[14] = new SqlParameter("@P_HOSTEL", hostel);
                        objparams[15] = new SqlParameter("@P_OrganizationId", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objparams[16] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objparams[16].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_AUTHORITY_APPROVAL_MASTER_INS", objparams, true);

                        if (Convert.ToInt32(ret) == 2627)
                            retstatus = (Convert.ToInt32(ret));
                        else if (Convert.ToInt32(ret) == 2323)
                            retstatus = (Convert.ToInt32(ret));
                        else if (ret != null && ret.ToString() != "-99" && ret.ToString() != "-1001")
                            retstatus = (Convert.ToInt32(ret));
                        else
                            retstatus = (Convert.ToInt32(ret));
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AddHostelGatePassAuthController.AddAAPath->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetSingleAuthApprovalPath(int APP_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_APP_NO", APP_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_Add_APPROVAL_MASTER_GET_BY_NO", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AddHostelGatePassAuthController.GetSingleAuthApprovalPath->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
            }

        }
    }
}
