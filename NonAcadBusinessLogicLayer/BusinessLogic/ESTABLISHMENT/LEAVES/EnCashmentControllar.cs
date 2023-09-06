
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLogicLayer.BusinessLogic.ESTABLISHMENT
        {
            public class EnCashmentControllar
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                // Get All Data for BindListView of ELEnCashment_Apply
                public DataSet GetAllEncashmentData(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParam = new SqlParameter[1];
                        objParam[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_GET_EL_ENCASHMENT", objParam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EnCashmentControllar.GetAllEncashmentData-> " + ex.ToString());
                    }

                    return ds;

                }

                //Add Or Update The Data of ELEnCashment_Apply
                public int AddUpdateElEnCashment(Leaves objLeaveMaster)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        bool flag = true;
                        if (flag == true)
                        {
                            objParams = new SqlParameter[13];
                            objParams[0] = new SqlParameter("@P_IDNO", objLeaveMaster.EMPNO);
                            objParams[1] = new SqlParameter("@P_APPDT", objLeaveMaster.APPDT);
                            objParams[2] = new SqlParameter("@P_TOTAL_DAYS", objLeaveMaster.NO_DAYS);
                            objParams[3] = new SqlParameter("@P_SRNO", objLeaveMaster.TRANNO);
                            objParams[4] = new SqlParameter("@P_LNO", objLeaveMaster.LNO);
                            objParams[5] = new SqlParameter("@P_LEAVENO", objLeaveMaster.LEAVENO);
                            objParams[6] = new SqlParameter("@P_PERIOD", objLeaveMaster.PERIOD);
                            objParams[7] = new SqlParameter("@P_SESSION_SRNO", objLeaveMaster.SESSION_SRNO);
                            objParams[8] = new SqlParameter("@P_ISACTIVE", objLeaveMaster.IsActive);
                            
                            objParams[9] = new SqlParameter("@P_CERATEDBY", objLeaveMaster.CREATEDBY);
                            objParams[10] = new SqlParameter("@P_MODIFIEDBY", objLeaveMaster.MODIFIEDBY);
                            objParams[11] = new SqlParameter("@P_IPADDRESS", objLeaveMaster.IPADDRESS);
                            objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                            objParams[12].Direction = ParameterDirection.Output;

                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_INS_UPD_EL_ENCASHMENT", objParams, true);
                            if (Convert.ToInt32(ret) == -99)
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (Convert.ToInt32(ret) == -1001)
                                retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EnCashmentControllar.ELEnCashment_Apply-> " + ex.ToString());
                    }
                    return retStatus;

                }

                //Get single record of ELEnCashment_Apply
                public DataSet GetSingleEncashmentData(int SRNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParam = new SqlParameter[1];
                        objParam[0] = new SqlParameter("@P_SRNO", SRNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_GETSINGLE_EL_ENCASHMENT", objParam);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EnCashmentControllar.ELEnCashment_Apply-> " + ex.ToString());
                    }
                    return ds;
                }

                //Delete the data of ELEnCashment_Apply
                public int DeleteEncashData(Leaves objLeaveMaster)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SRNO", objLeaveMaster.TRANNO);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_DEL_EL_ENCASHMENT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EnCashmentControllar.ELEnCashment_Apply-> " + ex.ToString());
                    }
                    return retStatus;

                }

                //Get All Data for BinListView of ELEnCashment_Approve
                public DataSet GetAllEncashApplyData(int collegeno, Leaves objLeaveMaster)
                   {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParam = new SqlParameter[3];
                       objParam[0] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                       if (!objLeaveMaster.FROMDT.Equals(DateTime.MinValue))
                           objParam[1] = new SqlParameter("@P_FROMDT", objLeaveMaster.FROMDT);
                       else
                           objParam[1] = new SqlParameter("@P_FROMDT", DBNull.Value);
                       if (!objLeaveMaster.TODT.Equals(DateTime.MinValue))
                           objParam[2] = new SqlParameter("@P_TODT", objLeaveMaster.TODT);
                       else
                           objParam[2] = new SqlParameter("@P_TODT", DBNull.Value);

                       ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_GET_EL_ENCASHMENT_APPROVE", objParam);
                   }
                   catch (Exception ex)
                   {
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EnCashmentControllar.GetAllEncashApplyData-> " + ex.ToString());
                   }
                  return ds;
              }

                //Approve The Record Of ELEnCashment_Approve
                public int ApproveRecord(Leaves objLeaveMaster, DataTable dtApprove)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                       // objParams[0] = new SqlParameter("@P_SRNO", objLeaveMaster.TRANNO);
                        objParams[0] = new SqlParameter("@ESTB_EL_ENCASH_LIST", dtApprove);
                        objParams[1] = new SqlParameter("@P_APPROVEDBY", objLeaveMaster.APPROVEDBY);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_INS_UPD_EL_ENCASH_APPROVE_RECORD", objParams, false);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EnCashmentControllar.ApproveRecord->" + ex.ToString());
                    }

                    return retStatus;
                }
            }
        }
    }
}





