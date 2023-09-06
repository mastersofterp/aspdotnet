//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS LOGIC FILE [PF]                                  
// CREATION DATE : 07-DEC-2009                                                        
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
            public class PFCONTROLLER
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                #region PF_LOAN

                public int AddPfLoan(PF objpf)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {   
                                 new SqlParameter("@P_IDNO",objpf.IDNO),
                                 new SqlParameter("@P_FDATE",objpf.FDATE),
                                 new SqlParameter("@P_TDATE",objpf.TDATE),
                                 new SqlParameter("@P_ADVAMT",objpf.ADVAMT),
                                 new SqlParameter("@P_ADVDT",objpf.ADVDT),
                                 new SqlParameter("@P_PFLOANTYPENO",objpf.PFLOANTYPENO),
                                 new SqlParameter("@P_INSTALMENT",objpf.INSTALMENT),
                                 new SqlParameter("@P_INSTAMT",objpf.INSTAMT),
                                 new SqlParameter("@P_SANCTION",objpf.SANCTION),
                                 new SqlParameter("@P_SANDT",objpf.SANDT),
                                 new SqlParameter("@P_SANNO",objpf.SANNO),
                                 new SqlParameter("@P_SANAMT",objpf.SANAMT),
                                 new SqlParameter("@P_PER",objpf.PER),
                                 new SqlParameter("@P_PREBAL",objpf.PREBAL),
                                 new SqlParameter("@P_WDT",objpf.WDT),
                                 new SqlParameter("@P_CURSANAMT",objpf.CURSANAMT),
                                 new SqlParameter("@P_COLLEGE_CODE",objpf.COLLEGE_CODE),
                                  new SqlParameter("@P_REMARK",objpf.REMARK),
                                  new SqlParameter("@P_COLLEGE_NO",objpf.COLLEGE_NO)
                            };
                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_INSERT_PF_LOAN", sqlParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PFCONTROLLER.AddPfLoan()-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdatePfLoan(PF objpf)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {    
                                 new SqlParameter("@P_PFLTNO",objpf.PFLTNO),
                                 new SqlParameter("@P_IDNO",objpf.IDNO),
                                 new SqlParameter("@P_FDATE",objpf.FDATE),
                                 new SqlParameter("@P_TDATE",objpf.TDATE),
                                 new SqlParameter("@P_SANCTION",objpf.SANCTION),
                                 new SqlParameter("@P_SANDT",objpf.SANDT),
                                 new SqlParameter("@P_SANNO",objpf.SANNO),
                                 new SqlParameter("@P_SANAMT",objpf.SANAMT),
                                 new SqlParameter("@P_PFLOANTYPENO",objpf.PFLOANTYPENO), 
                                 new SqlParameter("@P_CURSANAMT",objpf.CURSANAMT),
                                 new SqlParameter("@P_COLLEGE_CODE",objpf.COLLEGE_CODE),
                                 new SqlParameter("@P_COLLEGE_NO",objpf.COLLEGE_NO)
                            };
                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_UPDATE_PF_LOAN", sqlParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PFCONTROLLER.UpdatePfLoan()-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeletePfLoan(int pfLTNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {    
                                 new SqlParameter("@P_PFLTNO",pfLTNo)                                
                            };
                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_DELETE_PF_LOAN", sqlParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PFCONTROLLER.DeletePfLoan()-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetPfLoanByPFLNO(int pfLTNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlparam = new SqlParameter[]
                            { 
                              new SqlParameter("@P_PFLTNO",pfLTNo)  
                            };
                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_GET_PF_LOANBYPFLNO", sqlparam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.PFCONTROLLER.GetPfLoanByPFLNO() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetPfLoanByIdNo(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlparam = new SqlParameter[]
                        { 
                            new SqlParameter("@P_IDNO",idNo)
                        };
                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_GETALL_PF_LOANBYIDNO", sqlparam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.PFCONTROLLER.GetPfLoanByIdNo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
                public int AddPFLoanInstallMent(Payroll objinst)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[27];
                        objParams[0] = new SqlParameter("@P_IDNO", objinst.IDNO);
                        objParams[1] = new SqlParameter("@P_PAYHEAD", objinst.PAYHEAD);
                        objParams[2] = new SqlParameter("@P_CODE", objinst.CODE);
                        objParams[3] = new SqlParameter("@P_INSTALNO", objinst.INSTALNO);
                        objParams[4] = new SqlParameter("@P_MONAMT", objinst.MONAMT);
                        objParams[5] = new SqlParameter("@P_TOTAMT", objinst.TOTAMT);
                        objParams[6] = new SqlParameter("@P_BAL_AMT", objinst.BAL_AMT);
                        objParams[7] = new SqlParameter("@P_STOP", objinst.STOP);

                        if (!(objinst.START_DT == null))
                            objParams[8] = new SqlParameter("@P_START_DT", objinst.START_DT);
                        else
                            objParams[8] = new SqlParameter("@P_START_DT", DBNull.Value);

                        objParams[9] = new SqlParameter("@P_EXPDT", objinst.EXPDT);
                        objParams[10] = new SqlParameter("@P_PAIDNO", objinst.PAIDNO);
                        objParams[11] = new SqlParameter("@P_MON", DBNull.Value);
                        objParams[12] = new SqlParameter("@P_NEW", objinst.NEW);
                        objParams[13] = new SqlParameter("@P_ACCNO", objinst.ACCNO);
                        objParams[14] = new SqlParameter("@P_REF_NO", DBNull.Value);
                        objParams[15] = new SqlParameter("@P_DESP_NO", DBNull.Value);
                        objParams[16] = new SqlParameter("@P_DESP_DT", DBNull.Value);
                        objParams[17] = new SqlParameter("@P_DEFA_AMT", objinst.DEFA_AMT);
                        objParams[18] = new SqlParameter("@P_PRO_AMT", DBNull.Value);

                        if (!(objinst.SUBHEADNO == null))
                            objParams[19] = new SqlParameter("@P_SUBHEADNO", objinst.SUBHEADNO);
                        else
                            objParams[19] = new SqlParameter("@P_SUBHEADNO", DBNull.Value);

                        objParams[20] = new SqlParameter("@P_STOP1", DBNull.Value);
                        objParams[21] = new SqlParameter("@P_REGULAR", objinst.REGULAR);
                        objParams[22] = new SqlParameter("@P_LTNO", DBNull.Value);
                        objParams[23] = new SqlParameter("@P_REMARK", objinst.REMARK);
                        objParams[24] = new SqlParameter("@P_COLLEGE_CODE", objinst.COLLEGE_CODE);
                        objParams[25] = new SqlParameter("@P_COLLEGE_NO", objinst.COLLEGENO);                        
                        objParams[26] = new SqlParameter("@P_INO", SqlDbType.Int);
                        objParams[26].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_INS_PFLOANINSTALLMENT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PayController.AddPFLoanInstallMent()-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllScanctionLoans()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        ds = objSQLHelper.ExecuteDataSet("PAYROLL_GETALL_SANCTIONLOANS");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.PFCONTROLLER.GetPfLoanByPFLNO() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
                #endregion

                #region PFMASTER

                public int AddPFMaster(PF objpf)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {   
                                 new SqlParameter("@P_SHORTNAME",objpf.SHORTNAME),
                                 new SqlParameter("@P_FULLNAME",objpf.FULLNAME),
                                 new SqlParameter("@P_H1",objpf.H1),
                                 new SqlParameter("@P_H2",objpf.H2),
                                 new SqlParameter("@P_H3",objpf.H3),
                                 new SqlParameter("@P_COLLEGE_CODE",objpf.COLLEGE_CODE)
                            };
                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_INSERT_PFMAST", sqlParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PFCONTROLLER.AddPFMaster()-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdatePFMast(PF objpf)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {    
                                 new SqlParameter("@P_PFNO",objpf.PFNO),
                                 new SqlParameter("@P_SHORTNAME",objpf.SHORTNAME),
                                 new SqlParameter("@P_FULLNAME",objpf.FULLNAME),
                                 new SqlParameter("@P_H1",objpf.H1),
                                 new SqlParameter("@P_H2",objpf.H2),
                                 new SqlParameter("@P_H3",objpf.H3),
                                 new SqlParameter("@P_COLLEGE_CODE",objpf.COLLEGE_CODE)
                            };
                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_UPDATE_PFMAST", sqlParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PFCONTROLLER.UpdatePFMast()-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetPFMastByPFNO(int PFNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlparam = new SqlParameter[]
                            { 
                              new SqlParameter("@P_PFNo",PFNo)  
                            };
                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_GET_PFMASTBYPFNO", sqlparam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.PFCONTROLLER.GetPfLoanByPFLNO() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetAllPFMast()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        ds = objSQLHelper.ExecuteDataSet("PAYROLL_GETALL_PFMAST");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.PFCONTROLLER.GetPfLoanByIdNo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                #endregion

                #region PFLAONTYPE
                public DataSet GETPFFINANCEYAER()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        ds = objSQLHelper.ExecuteDataSet("GET_PF_FIN_YEAR");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.PFCONTROLLER.GETPFFINANCEYAER() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public int AddPFLoanType(PF objpf)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {   
                                 new SqlParameter("@P_NAME",objpf.NAME),
                                 new SqlParameter("@P_SHORTNAME",objpf.SHORTNAME),
                                 new SqlParameter("@P_PFNO",objpf.PFNO),
                                 new SqlParameter("@P_DEDUCTED",objpf.DEDUCTED),
                                 new SqlParameter("@P_AMT",objpf.AMT),
                                 new SqlParameter("@P_APP_FOR",objpf.APP_FOR),
                                 new SqlParameter("@P_COLLEGE_CODE",objpf.COLLEGE_CODE)
                            };
                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_INSERT_PFLOANTYPE", sqlParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PFCONTROLLER.AddPFLoanType()-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdatePFLoanType(PF objpf)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {    
                                 new SqlParameter("@P_PFLOANTYPENO",objpf.PFLOANTYPENO),
                                 new SqlParameter("@P_NAME",objpf.NAME),
                                 new SqlParameter("@P_SHORTNAME",objpf.SHORTNAME),
                                 new SqlParameter("@P_PFNO",objpf.PFNO),
                                 new SqlParameter("@P_DEDUCTED",objpf.DEDUCTED),
                                 new SqlParameter("@P_AMT",objpf.AMT),
                                 new SqlParameter("@P_APP_FOR",objpf.APP_FOR),
                                 new SqlParameter("@P_COLLEGE_CODE",objpf.COLLEGE_CODE)
                            };
                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_UPDATE_PFLOANTYPE", sqlParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PFCONTROLLER.UpdatePFLoanType()-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetPFLoanTypeByPfLaonTypeNo(int pfLoanTypeNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlparam = new SqlParameter[]
                            { 
                              new SqlParameter("@P_PFLOANTYPENO",pfLoanTypeNo)  
                            };
                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_GET_PFLOANTYPEBYPFLOANTYPENO", sqlparam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.PFCONTROLLER.GetPFMastByPFNO() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetAllPFLoanType()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        ds = objSQLHelper.ExecuteDataSet("PAYROLL_GETALL_PFLOANTYPE");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.PFCONTROLLER.GetAllPFLoanType() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                #endregion

                #region PF_INTREST_CALCULATION

                public int PFIntrestCalculation(int idNo, int staffNo, int percentage, string fSdate, string fEdate, string carray, DataTable Dt)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        //AddDaysal
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {   
                                 new SqlParameter("@P_IDNO",idNo),
                                 new SqlParameter("@P_STAFFNO",staffNo),
                                 new SqlParameter("@P_PERCENTAGE",percentage),                               
                                 new SqlParameter("@P_FSDATE",fSdate),
                                 new SqlParameter("@P_FEDATE",fEdate),
                                 new SqlParameter("@P_CARRAYFORWARD_OB",carray),
                                  new SqlParameter("@DatesTbl",Dt)
                                 
                                
                            };
                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_PF_INTREST_CALCULATION", sqlParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PFCONTROLLER.PFIntrestCalculation()-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public DataSet GetIntrestgrid(DateTime fromDate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlparam = new SqlParameter[]
                            { 
                              new SqlParameter("@FDate",fromDate)  
                            };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_QUARTERDATA", sqlparam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.PFCONTROLLER.GetPfLoanByPFLNO() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
                #endregion

                #region GET_PrograsiveBalance_LoanBalance

                public string GetPrograsiveBalanceLoanBalance(int idNo,string fsDate,string feDate)
                {
                    string Prog_Lon_Bal = string.Empty;
                    string P_PROGBAL_LOANBAL = string.Empty;
                     try
                     {  
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                           new SqlParameter("@P_IDNO",idNo),
                           new SqlParameter("@P_FSDATE",fsDate),
                           new SqlParameter("@P_FEDATE",feDate),
                           new SqlParameter("@P_PROGBAL_LOANBAL",SqlDbType.NVarChar,100),                           
                        };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                        Prog_Lon_Bal = Convert.ToString(objSQLHelper.ExecuteNonQuerySP("PAYROLL_GET_PROGRASIVEBALANCE_LOANBALANCE", sqlParams, true));

                     }
                     catch (Exception ex)
                     {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.GetPrograsiveBalanceLoanBalance() --> " + ex.Message + " " + ex.StackTrace);
                     }

                     return Prog_Lon_Bal;
                }

                #endregion

            }
        }
    }
}
