using System;
using System.Data;
using System.Web;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Academic;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            namespace BusinessLogicLayer.BusinessLogic.Academic
            {
                public class AuthorityApprovalcontroller
                {
                    private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


                    public int AddAAPath(AuthorityApproval OBJAAP, int collegeNo, int dept)
                    {
                        int retstatus = 0;
                        try
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                            SqlParameter[] objparams = null;
                            objparams = new SqlParameter[13];
                            objparams[0] = new SqlParameter("@P_AUTHORITY", OBJAAP.AUTHORITY);
                            objparams[1] = new SqlParameter("@P_AUTHORITY_NAME", OBJAAP.AUTHORITY_NAME);
                            objparams[2] = new SqlParameter("@P_AANO1", OBJAAP.APPROVAL_1);
                            objparams[3] = new SqlParameter("@P_AANO2", OBJAAP.APPROVAL_2);
                            objparams[4] = new SqlParameter("@P_AANO3", OBJAAP.APPROVAL_3);
                            objparams[5] = new SqlParameter("@P_AANO4", OBJAAP.APPROVAL_4);
                            objparams[6] = new SqlParameter("@P_AANO5", OBJAAP.APPROVAL_5);
                            objparams[7] = new SqlParameter("@P_AAPATH", OBJAAP.AAPATH);
                            objparams[8] = new SqlParameter("@P_CREATED_BY", OBJAAP.CREATED_BY);
                            objparams[9] = new SqlParameter("@P_IP_ADDRESS", OBJAAP.IP_ADDRESS);
                            objparams[10] = new SqlParameter("@P_DEPT_NO", dept);
                            objparams[11] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                            objparams[12] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                            objparams[12].Direction = ParameterDirection.Output;
                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_AUTHORITY_APPROVAL_MASTER_INS", objparams, true);

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
                            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AuthorityApprovalcontroller.AddAAPath->" + ex.ToString());
                        }
                        return retstatus;
                    }


                    public int UpdateAAPath(AuthorityApproval OBJAAP, int collegeNo, int dept)
                    {
                        int retstatus = 0;
                        try
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                            SqlParameter[] objparams = null;
                            objparams = new SqlParameter[12];
                            objparams[0] = new SqlParameter("@P_APP_NO", OBJAAP.APP_NO);
                            objparams[1] = new SqlParameter("@P_AUTHORITY", OBJAAP.AUTHORITY);
                            objparams[2] = new SqlParameter("@P_AANO1", OBJAAP.APPROVAL_1);
                            objparams[3] = new SqlParameter("@P_AANO2", OBJAAP.APPROVAL_2);
                            objparams[4] = new SqlParameter("@P_AANO3", OBJAAP.APPROVAL_3);
                            objparams[5] = new SqlParameter("@P_AANO4", OBJAAP.APPROVAL_4);
                            objparams[6] = new SqlParameter("@P_AANO5", OBJAAP.APPROVAL_5);
                            objparams[7] = new SqlParameter("@P_AAPATH", OBJAAP.AAPATH);
                            objparams[8] = new SqlParameter("@P_CREATED_BY", OBJAAP.CREATED_BY);
                            objparams[9] = new SqlParameter("@P_DEPT_NO", dept);
                            objparams[10] = new SqlParameter("@P_IP_ADDRESS", OBJAAP.IP_ADDRESS);
                            objparams[11] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                            if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_AUTHORITY_APPROVAL_MASTER_UPDATE", objparams, true) != null)
                                retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        catch (Exception ex)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.Error);
                            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AuthorityApprovalcontroller.UpdateAAPath->" + ex.ToString());
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
                            //   objparams[0] = new SqlParameter("@P_APP_NO", APP_NO);
                            ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_AUTHORITY_APPROVAL_MASTER_GET_BYALL", objparams);
                        }
                        catch (Exception ex)
                        {
                            return ds;
                            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AuthorityApprovalcontroller.GetAllAAMaster->" + ex.ToString());
                        }
                        finally
                        {
                            ds.Dispose();
                        }
                        return ds;
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
                            ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_AUTHORITY_APPROVAL_MASTER_GET_BY_NO", objparams);
                        }
                        catch (Exception ex)
                        {
                            return ds;
                            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AuthorityApprovalcontroller.GetSingleAAPath->" + ex.ToString());
                        }
                        finally
                        {
                            ds.Dispose();
                        }
                        return ds;
                    }
                    //added by pooja
                    //For club authority approval Master

                    public int clubAddAAPath(AuthorityApproval OBJAAP, int collegeNo)
                    {
                        int retstatus = 0;
                        try
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                            SqlParameter[] objparams = null;
                            objparams = new SqlParameter[12];

                            objparams[0] = new SqlParameter("@P_CLUB_ACTIVITY_NO", OBJAAP.CLUB_ACTIVITY_NO);
                            objparams[1] = new SqlParameter("@P_CLUB_ACTIVITY_TYPE", OBJAAP.CLUB_ACTIVITY_TYPE);
                            objparams[2] = new SqlParameter("@P_AANO1", OBJAAP.APPROVAL_1);
                            objparams[3] = new SqlParameter("@P_AANO2", OBJAAP.APPROVAL_2);
                            objparams[4] = new SqlParameter("@P_AANO3", OBJAAP.APPROVAL_3);
                            objparams[5] = new SqlParameter("@P_AANO4", OBJAAP.APPROVAL_4);
                            objparams[6] = new SqlParameter("@P_AANO5", OBJAAP.APPROVAL_5);
                            objparams[7] = new SqlParameter("@P_AAPATH", OBJAAP.AAPATH);
                            objparams[8] = new SqlParameter("@P_CREATED_BY", OBJAAP.CREATED_BY);
                            objparams[9] = new SqlParameter("@P_IP_ADDRESS", OBJAAP.IP_ADDRESS);
                            //objparams[10] = new SqlParameter("@P_DEPT_NO", deptNo);
                            objparams[10] = new SqlParameter("@P_COLLEGE_NO", collegeNo);

                            objparams[11] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                            objparams[11].Direction = ParameterDirection.Output;
                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_CLUB_AUTHORITY_APPROVAL_MASTER_INS", objparams, true);
                            if (Convert.ToInt32(ret) == 1)

                                //if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                                retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.Equals(0))
                            {
                                retstatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                            }
                            else
                                retstatus = Convert.ToInt32(CustomStatus.Error);



                        }
                        catch (Exception ex)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.Error);
                            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AuthorityApprovalcontroller.AddAAPath->" + ex.ToString());
                        }
                        return retstatus;
                    }


                    public int clubUpdateAAPath(AuthorityApproval OBJAAP, int collegeNo)
                    {
                        int retstatus = 0;
                        try
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                            SqlParameter[] objparams = null;
                            objparams = new SqlParameter[13];

                            objparams[0] = new SqlParameter("@P_APP_NO", OBJAAP.APP_NO);
                            objparams[1] = new SqlParameter("@P_CLUB_ACTIVITY_NO", OBJAAP.CLUB_ACTIVITY_NO);
                            objparams[2] = new SqlParameter("@P_CLUB_ACTIVITY_TYPE", OBJAAP.CLUB_ACTIVITY_TYPE);
                            objparams[3] = new SqlParameter("@P_AANO1", OBJAAP.APPROVAL_1);
                            objparams[4] = new SqlParameter("@P_AANO2", OBJAAP.APPROVAL_2);
                            objparams[5] = new SqlParameter("@P_AANO3", OBJAAP.APPROVAL_3);
                            objparams[6] = new SqlParameter("@P_AANO4", OBJAAP.APPROVAL_4);
                            objparams[7] = new SqlParameter("@P_AANO5", OBJAAP.APPROVAL_5);
                            objparams[8] = new SqlParameter("@P_AAPATH", OBJAAP.AAPATH);
                            objparams[9] = new SqlParameter("@P_CREATED_BY", OBJAAP.CREATED_BY);
                            //objparams[8] = new SqlParameter("CREATED_DATE", OBJAAP.CREATED_DATE);
                            objparams[10] = new SqlParameter("@P_IP_ADDRESS", OBJAAP.IP_ADDRESS);
                            //objparams[10] = new SqlParameter("@P_DEPT_NO", deptNo);
                            objparams[11] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                            objparams[12] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                            objparams[12].Direction = ParameterDirection.Output;
                            //   objparams[11] = new SqlParameter("@P_ESTB_EMP_RECORD", dtEmpRecord);
                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_CLUB_AUTHORITY_APPROVAL_MASTER_UPDATE", objparams, true);
                            //if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_CLUB_AUTHORITY_APPROVAL_MASTER_UPDATE", objparams, true) != null)
                            if (Convert.ToInt32(ret) == 1)
                                retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            else if (ret.Equals(0))
                                retstatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                            else
                                retstatus = Convert.ToInt32(CustomStatus.Error);

                            //retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        catch (Exception ex)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.Error);
                            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AuthorityApprovalcontroller.UpdateAAPath->" + ex.ToString());
                        }
                        return retstatus;
                    }


                    public DataSet clubGetAllAAMaster()
                    {
                        DataSet ds = null;
                        try
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                            SqlParameter[] objparams = new SqlParameter[0];
                            //   objparams[0] = new SqlParameter("@P_APP_NO", APP_NO);
                            ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_CLUB_AUTHORITY_APPROVAL_MASTER_GET_BYALL", objparams);
                        }
                        catch (Exception ex)
                        {
                            return ds;
                            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AuthorityApprovalcontroller.GetAllAAMaster->" + ex.ToString());
                        }
                        finally
                        {
                            ds.Dispose();
                        }
                        return ds;
                    }
                    public DataSet clubGetSingleAAPath(int APP_NO)
                    {
                        DataSet ds = null;
                        try
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                            SqlParameter[] objparams = null;
                            objparams = new SqlParameter[1];
                            objparams[0] = new SqlParameter("@P_APP_NO", APP_NO);
                            ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_CLUB_AUTHORITY_APPROVAL_MASTER_GET_BY_NO", objparams);
                        }
                        catch (Exception ex)
                        {
                            return ds;
                            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AuthorityApprovalcontroller.GetSingleAAPath->" + ex.ToString());
                        }
                        finally
                        {
                            ds.Dispose();
                        }
                        return ds;
                    }

                    //Added by pooja for nodues on date 21-07-2023
                    public int AddAuthApprovalPath(AuthorityApproval OBJAAP, int AUTHORITY, string AUTHORITY_NAME, string APPROVAL_1, string APPROVAL_2, string APPROVAL_3, string APPROVAL_4, string APPROVAL_5)
                    {
                        int retstatus = 0;
                        try
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                            SqlParameter[] objparams = null;
                            objparams = new SqlParameter[10];
                            objparams[0] = new SqlParameter("@P_AUTHORITY", AUTHORITY);
                            objparams[1] = new SqlParameter("@P_AUTHORITY_NAME", AUTHORITY_NAME);
                            objparams[2] = new SqlParameter("@P_APPROVAL1", APPROVAL_1);
                            objparams[3] = new SqlParameter("@P_APPROVAL2", APPROVAL_2);
                            objparams[4] = new SqlParameter("@P_APPROVAL3", APPROVAL_3);
                            objparams[5] = new SqlParameter("@P_APPROVAL4", APPROVAL_4);
                            objparams[6] = new SqlParameter("@P_APPROVAL5", APPROVAL_5);
                            objparams[7] = new SqlParameter("@P_CREATED_BY", OBJAAP.CREATED_BY);
                            objparams[8] = new SqlParameter("@P_IP_ADDRESS", OBJAAP.IP_ADDRESS);

                            objparams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                            objparams[9].Direction = ParameterDirection.Output;
                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_Add_APPROVAL_MASTER_INS", objparams, true);

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
                            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AuthorityApprovalcontroller.AddAuthApprovalPath->" + ex.ToString());
                        }
                        return retstatus;
                    }

                    public int UpdateAuthApprovalPath(AuthorityApproval OBJAAP, int AUTHORITY, string AUTHORITY_NAME, string APPROVAL_1, string APPROVAL_2, string APPROVAL_3, string APPROVAL_4, string APPROVAL_5)
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
                            objparams[3] = new SqlParameter("@P_APPROVAL1", APPROVAL_1);
                            objparams[4] = new SqlParameter("@P_APPROVAL2", APPROVAL_2);
                            objparams[5] = new SqlParameter("@P_APPROVAL3", APPROVAL_3);
                            objparams[6] = new SqlParameter("@P_APPROVAL4", APPROVAL_4);
                            objparams[7] = new SqlParameter("@P_APPROVAL5", APPROVAL_5);
                            objparams[8] = new SqlParameter("@P_CREATED_BY", OBJAAP.CREATED_BY);
                            objparams[9] = new SqlParameter("@P_IP_ADDRESS", OBJAAP.IP_ADDRESS);
                            objparams[10] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                            objparams[10].Direction = ParameterDirection.Output;
                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_Add_APPROVAL_MASTER_UPDATE", objparams, true);
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
                            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AuthorityApprovalcontroller.UpdateAuthApprovalPath->" + ex.ToString());
                        }
                        return retstatus;
                    }


                    public DataSet GetAllAuthApprovalPathMaster()
                    {
                        DataSet ds = null;
                        try
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                            SqlParameter[] objparams = new SqlParameter[0];
                            ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_Add_APPROVAL_MASTER_GET_BYALL", objparams);
                        }
                        catch (Exception ex)
                        {
                            return ds;
                            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AuthorityApprovalcontroller.GetAllAuthApprovalPathMaster->" + ex.ToString());
                        }
                        finally
                        {
                            ds.Dispose();
                        }
                        return ds;
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
                            ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_Add_APPROVAL_MASTER_GET_BY_NO", objparams);
                        }
                        catch (Exception ex)
                        {
                            return ds;
                            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AuthorityApprovalcontroller.GetSingleAuthApprovalPath->" + ex.ToString());
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
}