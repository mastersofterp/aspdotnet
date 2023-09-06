using System;
using System.Data;
using System.Web;

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
            public class WithheldWithdrawnController
            {
                string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public int InsertUpdateWithHeldWithDrawn(WithheldWithDrawn objWWE)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New Employee
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_IDNO", objWWE.IDNO);
                        objParams[1] = new SqlParameter("@P_MONTHYEAR", objWWE.MONTHYEAR);
                        objParams[2] = new SqlParameter("@P_WITHHELDSTATUS", objWWE.WITHHELDSTATUS);
                        objParams[3] = new SqlParameter("@P_WITHHELDREMARK", objWWE.WITHHELDREMARK);
                        objParams[4] = new SqlParameter("@P_COLLEGENO", objWWE.COLLEGENO);
                        objParams[5] = new SqlParameter("@P_STAFFNO", objWWE.STAFFNO);
                        //objParams[6] = new SqlParameter("@P_WITHHELDDATE", objWWE.WITHHELDDATE);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INSERT_UPDATE_WITHHELDWITHDRAWN", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.WithheldWithdrawnController.InsertUpdateWithHeldWithDrawn -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateWithHeldWithDrawn(WithheldWithDrawn objWWE)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New Employee
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_IDNOS", objWWE.IDNOS);
                        objParams[1] = new SqlParameter("@P_MONTHYEAR", objWWE.MONTHYEAR);
                        objParams[2] = new SqlParameter("@P_WITHHELDSTATUS", objWWE.WITHHELDSTATUS);
                        objParams[3] = new SqlParameter("@P_WITHHELDREMARK", objWWE.WITHHELDREMARK);
                        objParams[4] = new SqlParameter("@P_COLLEGENO", objWWE.COLLEGENO);
                        objParams[5] = new SqlParameter("@P_STAFFNO", objWWE.STAFFNO);
                        //objParams[6] = new SqlParameter("@P_WITHHELDDATE", objWWE.WITHHELDDATE);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPDATE_WITHHELDWITHDRAWN", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.WithheldWithdrawnController.InsertUpdateWithHeldWithDrawn -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetWithHeldWithDrawnDetails(WithheldWithDrawn objWWE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_MONTHYEAR", objWWE.MONTHYEAR);
                        objParams[1] = new SqlParameter("@P_COLLEGENO", objWWE.COLLEGENO);
                        objParams[2] = new SqlParameter("@P_STAFFNO", objWWE.STAFFNO);
                        objParams[3] = new SqlParameter("@P_ORDERBY", objWWE.ORDERBY);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_WITHHELDWITHDRAWN", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.WithheldWithdrawnController.GetWithHeldWithDrawnDetails-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetWithHeldWithDrawnDetails_PaidUpiad(WithheldWithDrawn objWWE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_MON_YEAR", objWWE.MONTHYEAR);
                        objParams[1] = new SqlParameter("@P_COLLEGE_NO", objWWE.COLLEGENO);
                        objParams[2] = new SqlParameter("@P_STAFF_NO", objWWE.STAFFNO);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_PAID_UNPAID_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.WithheldWithdrawnController.GetWithHeldWithDrawnDetails-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }


                public DataSet GetWithDrawnDetails(WithheldWithDrawn objWWE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_COLLEGENO", objWWE.COLLEGENO);
                        objParams[1] = new SqlParameter("@P_STAFFNO", objWWE.STAFFNO);
                        objParams[2] = new SqlParameter("@P_ORDERBY", objWWE.ORDERBY);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_WITHDRAWN_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.WithheldWithdrawnController.GetWithDrawnDetails-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int UpdateWithDrawnDetails(WithheldWithDrawn objWWE)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New Employee
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_IDNO", objWWE.IDNO);
                        objParams[1] = new SqlParameter("@P_WITHHELDID", objWWE.WITHHELDID);
                        objParams[2] = new SqlParameter("@P_WITHDRAWNSTATUS", objWWE.WITHHELDSTATUS);
                        objParams[3] = new SqlParameter("@P_WITHDRAWNDATE", objWWE.WITHHELDDATE);
                        objParams[4] = new SqlParameter("@P_WITHHELDREMARK", objWWE.WITHHELDREMARK);
                        objParams[5] = new SqlParameter("@P_COLLEGENO", objWWE.COLLEGENO);
                        objParams[6] = new SqlParameter("@P_STAFFNO", objWWE.STAFFNO);
                        //objParams[6] = new SqlParameter("@P_WITHHELDDATE", objWWE.WITHHELDDATE);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPDATE_WITHDRAWN_DETAILS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.WithheldWithdrawnController.UpdateWithDrawnDetails -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateWithDrawnNotIDNO(WithheldWithDrawn objWWE)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New Employee
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_WITHHELDIDNOS", objWWE.WITHHELDIDNOS);
                        objParams[1] = new SqlParameter("@P_WITHDRAWNSTATUS", objWWE.WITHHELDSTATUS);
                        objParams[2] = new SqlParameter("@P_WITHDRAWNREMARK", objWWE.WITHHELDREMARK);
                        // objParams[3] = new SqlParameter("@P_WITHDRAWNDATE", objWWE.WITHHELDDATE);
                        if (!objWWE.WITHHELDDATE.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_WITHDRAWNDATE", objWWE.WITHHELDDATE);
                        else
                            objParams[3] = new SqlParameter("@P_WITHDRAWNDATE", DBNull.Value);
                        objParams[4] = new SqlParameter("@P_COLLEGENO", objWWE.COLLEGENO);
                        objParams[5] = new SqlParameter("@P_STAFFNO", objWWE.STAFFNO);
                        //objParams[6] = new SqlParameter("@P_WITHHELDDATE", objWWE.WITHHELDDATE);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPDATE_NOT_WITHDRAWN_IDNOS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.WithheldWithdrawnController.UpdateWithDrawnNotIDNO -> " + ex.ToString());
                    }
                    return retStatus;
                }
            }
        }
    }
}
