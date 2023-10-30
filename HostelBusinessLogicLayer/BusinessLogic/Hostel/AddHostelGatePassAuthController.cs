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

                public int UpdateAuthApprovalPath(AddHostelGatePassAuthApproval OBJAAP, int STUDTYPE, int AUTHORITY, string AUTHORITY_NAME, string APPROVAL_1, string APPROVAL_2, string APPROVAL_3, string APPROVAL_4)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[11];
                        objparams[0] = new SqlParameter("@P_APP_NO", OBJAAP.APP_NO);
                        objparams[1] = new SqlParameter("@P_AUTHORITY", AUTHORITY);
                        objparams[2] = new SqlParameter("@P_AUTHORITY_NAME", AUTHORITY_NAME);
                        objparams[3] = new SqlParameter("@P_STUDTYPE", STUDTYPE);
                        objparams[4] = new SqlParameter("@P_APPROVAL1", APPROVAL_1);
                        objparams[5] = new SqlParameter("@P_APPROVAL2", APPROVAL_2);
                        objparams[6] = new SqlParameter("@P_APPROVAL3", APPROVAL_3);
                        objparams[7] = new SqlParameter("@P_APPROVAL4", APPROVAL_4);
                        objparams[8] = new SqlParameter("@P_CREATED_BY", OBJAAP.CREATED_BY);
                        objparams[9] = new SqlParameter("@P_IP_ADDRESS", OBJAAP.IP_ADDRESS);
                        objparams[10] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objparams[11].Direction = ParameterDirection.Output;
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
                public int AddAuthApprovalPath(AddHostelGatePassAuthApproval OBJAAP, int STUDTYPE, int AUTHORITY, string AUTHORITY_NAME, string APPROVAL_1, string APPROVAL_2, string APPROVAL_3, string APPROVAL_4)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[12];
                        objparams[0] = new SqlParameter("@P_APPNO", OBJAAP.APP_NO);
                        objparams[1] = new SqlParameter("@P_AUTHORITY", AUTHORITY);
                        objparams[2] = new SqlParameter("@P_AUTHORITY_NAME", AUTHORITY_NAME);
                        objparams[3] = new SqlParameter("@P_STUDTYPE", STUDTYPE);
                        objparams[4] = new SqlParameter("@P_APPROVAL1", APPROVAL_1);
                        objparams[5] = new SqlParameter("@P_APPROVAL2", APPROVAL_2);
                        objparams[6] = new SqlParameter("@P_APPROVAL3", APPROVAL_3);
                        objparams[7] = new SqlParameter("@P_APPROVAL4", APPROVAL_4);
                        objparams[8] = new SqlParameter("@P_CREATED_BY", OBJAAP.CREATED_BY);
                        objparams[9] = new SqlParameter("@P_IP_ADDRESS", OBJAAP.IP_ADDRESS);
                        objparams[10] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objparams[11].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_Add_APPROVAL_MASTER_INS", objparams, true);

                        if (Convert.ToInt32(ret) == 2627)
                            retstatus = (Convert.ToInt32(ret));
                        if (Convert.ToInt32(ret) == 1027)
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

                public int UpdateAAPath(AddHostelGatePassAuthApproval OBJAAP, int collegeNo, int dept)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[10];
                        objparams[0] = new SqlParameter("@P_APP_NO", OBJAAP.APP_NO);
                        objparams[1] = new SqlParameter("@P_AUTHORITY", OBJAAP.AUTHORITY);
                        objparams[2] = new SqlParameter("@P_AANO1", OBJAAP.APPROVAL_1);
                        objparams[3] = new SqlParameter("@P_AANO2", OBJAAP.APPROVAL_2);
                        objparams[4] = new SqlParameter("@P_AANO3", OBJAAP.APPROVAL_3);
                        objparams[5] = new SqlParameter("@P_AAPATH", OBJAAP.AAPATH);
                        objparams[6] = new SqlParameter("@P_CREATED_BY", OBJAAP.CREATED_BY);
                        objparams[7] = new SqlParameter("@P_DEPT_NO", dept);
                        objparams[8] = new SqlParameter("@P_IP_ADDRESS", OBJAAP.IP_ADDRESS);
                        objparams[9] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
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

                public int AddAAPath(AddHostelGatePassAuthApproval OBJAAP, int collegeNo, int dept)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[12];
                        objparams[0] = new SqlParameter("@P_APPNO", OBJAAP.APP_NO);
                        objparams[1] = new SqlParameter("@P_AUTHORITY", OBJAAP.AUTHORITY);
                        objparams[2] = new SqlParameter("@P_AUTHORITY_NAME", OBJAAP.AUTHORITY_NAME);
                        objparams[3] = new SqlParameter("@P_AANO1", OBJAAP.APPROVAL_1);
                        objparams[4] = new SqlParameter("@P_AANO2", OBJAAP.APPROVAL_2);
                        objparams[5] = new SqlParameter("@P_AANO3", OBJAAP.APPROVAL_3);
                        objparams[6] = new SqlParameter("@P_AAPATH", OBJAAP.AAPATH);
                        objparams[7] = new SqlParameter("@P_CREATED_BY", OBJAAP.CREATED_BY);
                        objparams[8] = new SqlParameter("@P_IP_ADDRESS", OBJAAP.IP_ADDRESS);
                        objparams[9] = new SqlParameter("@P_DEPT_NO", dept);
                        objparams[10] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                        objparams[11] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objparams[12].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_AUTHORITY_APPROVAL_MASTER_INS", objparams, true);

                        if (Convert.ToInt32(ret) == 2627)
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
