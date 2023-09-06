//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS LOGIC FILE //[PAY ARREARS CONTROLLER]                                  
// CREATION DATE : 07-JUNE-2016                                                        
// CREATED BY    : SURAJ CHOUDHARI                                       
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
            public class PayArrearsController
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
      
                public DataSet GetArrearsInfo(int arno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ARNO", arno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ARREARS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayController.GetArrearsInfo-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetAllMonth(string fromDate, string todate, int staffno, int collegeNo)
                {

                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_FROM_DATE", fromDate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", todate);
                        objParams[2] = new SqlParameter("@P_STAFFNO", staffno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", collegeNo);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_REPORT_DROPDOWN_GET_MONTH1", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayController.GetAllPayHead-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetArrearsDiff(int arno, int idno, string mon)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ARNO", arno);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);
                        objParams[2] = new SqlParameter("@P_MON", mon);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ARREARS_DIFF", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayController.GetArrearsDiff-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetArrearsAmount(int arno, int idno, string mon, string payhead)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_ARNO", arno);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);
                        objParams[2] = new SqlParameter("@P_MON", mon);
                        objParams[3] = new SqlParameter("@P_PAYHEAD", payhead);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ARREARS_AMT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayController.GetArrearsAmount-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetActualAmount(int idno, string mon, string payhead)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_MON", mon);
                        objParams[2] = new SqlParameter("@P_PAYHEAD", payhead);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_PAYHEAD_AMT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayController.GetActualAmount-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int TransferToSuplBill(int suplarrearno, string suplordno, DateTime supldate, int suplbillHeadno, string suplheadname, string suplmon)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_ARNO", suplarrearno);
                        objParams[1] = new SqlParameter("@P_SUPL_ORDERNO", suplordno);
                        objParams[2] = new SqlParameter("@P_SUPL_DATE", supldate);
                        objParams[3] = new SqlParameter("@P_SUPLHEAD_NO", suplbillHeadno);
                        objParams[4] = new SqlParameter("@P_SUPLHEAD_NAME", suplheadname);
                        objParams[5] = new SqlParameter("@P_SUPL_MON", suplmon);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_TRANSFER_TO_SUPLIMENTRY_BILL", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.PayController.UpdateSalaryGrant-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeleteArrearsRecord(int arno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ARNO", arno);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DELETE_ARREARS_RECORDS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.PayController.UpdateSalaryGrant-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //To add Arrears Calculation 

                public int AddPayArrears(PayArrearsEntity objPAE)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[14];
                        objParams[0] = new SqlParameter("@P_STAFFNO", objPAE.STAFFNO);
                        objParams[1] = new SqlParameter("@P_PAYHEAD", objPAE.PAYHEAD);
                        objParams[2] = new SqlParameter("@P_AFRM", objPAE.AFRM);
                        objParams[3] = new SqlParameter("@P_ATO ", objPAE.ATO);
                        objParams[4] = new SqlParameter("@P_PER ", objPAE.Per);
                        objParams[5] = new SqlParameter("@P_RULENO", objPAE.Payrule);
                        objParams[6] = new SqlParameter("@P_GOVORDNO", objPAE.GOVORDNO);
                        objParams[7] = new SqlParameter("@P_GOVORDDT", objPAE.GOVORDDT);
                        objParams[8] = new SqlParameter("@P_OFFORDNO", objPAE.OFFORDNO);
                        objParams[9] = new SqlParameter("@P_OFFORDDT", objPAE.OFFORDDT);
                        objParams[10] = new SqlParameter("@P_REMARK", objPAE.REMARK);
                        objParams[11] = new SqlParameter("@P_COLLEGE_CODE", objPAE.COLLEGE_CODE);
                        objParams[12] = new SqlParameter("@P_COLLEGE_NO", objPAE.COLLEGENO);
                        objParams[13] = new SqlParameter("@P_ARNO ", SqlDbType.Int);
                        objParams[13].Direction = ParameterDirection.Output;
                        
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_ARREARS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.PayController.AddPayArrears-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int CalculateArrears(int staffno,int collegeno, string payhead, string fromdate, string todate, string idno, double per, int arno, int payRule)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    //DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_STAFFNO", staffno);
                        objParams[1] = new SqlParameter("@P_COLLEGENO", collegeno);
                        objParams[2] = new SqlParameter("@P_FROM_DATE", fromdate);
                        objParams[3] = new SqlParameter("@P_TO_DATE", todate);
                        objParams[4] = new SqlParameter("@P_HEAD", payhead);
                        objParams[5] = new SqlParameter("@P_PER", per);
                        objParams[6] = new SqlParameter("@P_IDNO", idno);
                        objParams[7] = new SqlParameter("@P_ARNO", arno);
                        objParams[8] = new SqlParameter("@P_PAYRULE", payRule);

                        // ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_ARREARS_CALCULATE2", objParams);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_ARREARS_CALCULATE2", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        //ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_ARREARS_CALCULATE2", objParams);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        // return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayController.GetEmpByStaffno-> " + ex.ToString());
                    }

                    //return ds;
                    return retStatus;
                }
                public int DeleteArrearsDiffRecord(int arno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ARNO", arno);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_ARREARS_DELETE_ARREAR_DIFF", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.PayController.UpdateSalaryGrant-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeleteSupplBillTransferRecord(int arno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ARNO", arno);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SUPPLBILL_TRANSFER_DELETE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.PayController.UpdateSalaryGrant-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //Increment Arrears Calculation
                public int IncrementCalculateArrears(string fromdate, string todate, int staffno, int collegeno, int arno, string idno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", fromdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", todate);
                        objParams[2] = new SqlParameter("@P_STAFFNO", staffno);
                        objParams[3] = new SqlParameter("@P_COLLEGENO", collegeno);
                        objParams[4] = new SqlParameter("@P_ARNO", arno);
                        objParams[5] = new SqlParameter("@P_IDNO", idno);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INCREMENT_ARREARS_CALCULATION", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayController.GetEmpByStaffno-> " + ex.ToString());
                    }

                    return retStatus;
                }
            }
        }
    }
}
