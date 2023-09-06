//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS LOGIC FILE [GPF]                                  
// CREATION DATE : 25-NOV-2009                                                        
// CREATED BY    : KIRAN GVS                                       
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================  
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
        namespace BusinessLayer.BusinessLogic
        {
            public class GPF_CONTROLLER
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                #region PF_TRAN

                public int AddPfTran(GPF objpftran)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {   
                                 new SqlParameter("@P_IDNO",objpftran.IDNO),
                                 new SqlParameter("@P_MONYEAR",objpftran.MONYEAR),
                                 new SqlParameter("@P_H1",objpftran.H1),
                                 new SqlParameter("@P_H2",objpftran.H2),
                                 new SqlParameter("@P_H3",objpftran.H3),
                                 new SqlParameter("@P_H4",objpftran.H4),
                                 new SqlParameter("@P_OB",objpftran.OB),
                                 new SqlParameter("@P_LOANBAL",objpftran.LOANBAL),
                                 new SqlParameter("@P_PROGRESSIVEBAL",objpftran.PROGRESSIVEBAL),
                                 new SqlParameter("@P_MONTHDATE",objpftran.MONTHDATE),
                                 new SqlParameter("@P_FSDATE",objpftran.FSDATE),
                                 new SqlParameter("@P_FEDATE",objpftran.FEDATE),
                                 new SqlParameter("@P_PFNO",objpftran.PFNO),
                                 new SqlParameter("@P_PFLOANTYPENO","1"),
                                 new SqlParameter("@P_STATUS",objpftran.STATUS),
                                 new SqlParameter("@P_COLLEGE_CODE",objpftran.COLLEGE_CODE),
                                 new SqlParameter("@P_COLLEGE_NO",objpftran.COLLEGE_NO)
                            };
                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_INSERT_PF_TRAN", sqlParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.GPF_CONTROLLER.AddPfTran-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetPfTran(GPF objpftran,string processStatus)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlparam = new SqlParameter[]
                            { 
                              new SqlParameter("@P_IDNO",objpftran.IDNO),
                              new SqlParameter("@P_STAFFNO",objpftran.STAFFNO),
                              new SqlParameter("@P_MONYEAR",(objpftran.MONYEAR==null ?DBNull.Value as object : objpftran.MONYEAR as object)), 
                              new SqlParameter("@P_PFNO",(objpftran.PFNO==0? DBNull.Value as object : objpftran.PFNO as object)),
                              new SqlParameter("@P_STATUS",(objpftran.STATUS==null ?DBNull.Value as object : objpftran.STATUS as object)),
                              new SqlParameter("@P_PROCESS",(objpftran==null ?DBNull.Value as Object:processStatus as Object))
                            };
                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_GET_PF_TRAN", sqlparam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.GPF_CONTROLLER.GetPfTran() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public int DeletePfTran(int pfTrxNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {   
                                new SqlParameter("@P_PFTRXNO",pfTrxNo)                                
                            };
                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_DELETE_PF_TRAN", sqlParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.GPF_CONTROLLER.DeletePfTran()-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetPfTranByTrxno(int pfTrxno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlparam = new SqlParameter[]
                            { 
                              new SqlParameter("@P_PFTRXNO",pfTrxno)
                            };

                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_Get_PFTRANBYTRXNO", sqlparam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.GPF_CONTROLLER.GetPfTranByTrxno() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public int ProcessPfTran(GPF objpftran)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        //AddDaysal
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {   
                                 new SqlParameter("@P_IDNO",objpftran.IDNO),
                                 new SqlParameter("@P_STAFFNO",objpftran.STAFFNO),
                                 new SqlParameter("@P_MONDATE",objpftran.MONTHDATE),
                                 new SqlParameter("@P_MONYEAR",objpftran.MONYEAR),
                                 new SqlParameter("@P_STATUS",objpftran.STATUS),
                                 new SqlParameter("@P_FSDATE",objpftran.FSDATE),
                                 new SqlParameter("@P_FEDATE",objpftran.FEDATE),
                                 new SqlParameter("@P_COLLEGE_CODE",objpftran.COLLEGE_CODE),
                                 new SqlParameter("@P_TRANSFERAMOUNT",objpftran.TRANSFERAMOUNT),
                                 new SqlParameter("@P_TRANSFERDATE",objpftran.TRANSFERDATE),
                                 new SqlParameter("@P_COLLEGE_NO",objpftran.COLLEGE_NO)
                            };
                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_PROCESS_PF_TRAN", sqlParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.GPF_CONTROLLER.ProcessPfTran()-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion

            }
        }
    }
}
