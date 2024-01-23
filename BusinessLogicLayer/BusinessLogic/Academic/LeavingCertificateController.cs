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

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class LeavingCertificateController
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


                public DataSet GetLeavingDetail(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_RET_LEAVING_CERTIFICATE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavingCertificateController.GetLeavingDetail-> " + ex.ToString());
                    }
                    return ds;
                }



                /// <summary>
                /// Added by Nehal 16-03-2023
                /// </summary>
                /// <param name="RCPIT"></param>
                /// <returns></returns>
                /// 

                public DataSet GetAllTCReason(int id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ID", id);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_TC_REASON_MASTER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavingCertificateController.GetAllTCReason-> " + ex.ToString());
                    }
                    return ds;
                }
                public int Add_TCReasonMaster(int id, string reason, bool IsActive)
                {
                    int status = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_ID", id);
                        objParams[1] = new SqlParameter("@P_REASON", reason);
                        objParams[2] = new SqlParameter("@P_ACTIVE_STATUS", IsActive);
                        objParams[3] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG__TC_REASON_MASTER_INS_UPD", objParams, true);

                        if (obj.ToString().Equals("-2"))
                        {
                            status = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else if (obj.ToString().Equals("2"))
                        {
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else if (obj.ToString().Equals("1"))
                        {
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavingCertificateController.Add_TCReasonMaster-> " + ex.ToString());
                    }
                    return status;
                }

                // Added By Vipul Tichakule on Dated 08-01-2024
                public DataSet GetAllTCRemarks(int id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ID", id);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_TC_REMARKS_MASTER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavingCertificateController.GetAllTCRemarks-> " + ex.ToString());
                    }
                    return ds;
                }

                // Added By Vipul Tichakule on Dated 08-01-2024
                public int Add_TCRemarksMaster(int id, string remarks, bool IsActive)
                {
                    int status = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_ID", id);
                        objParams[1] = new SqlParameter("@P_REMARKS", remarks);
                        objParams[2] = new SqlParameter("@P_ACTIVE_STATUS", IsActive);
                        objParams[3] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG__TC_REMARKS_MASTER_INS_UPD", objParams, true);

                        if (obj.ToString().Equals("-2"))
                        {
                            status = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else if (obj.ToString().Equals("2"))
                        {
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else if (obj.ToString().Equals("1"))
                        {
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavingCertificateController.Add_TCReasonMaster-> " + ex.ToString());
                    }
                    return status;
                }

            }
        }
    }
}
