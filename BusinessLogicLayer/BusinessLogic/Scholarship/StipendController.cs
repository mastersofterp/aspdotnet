//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : SCHOLARSHIP                                                           
// PAGE NAME     : SCHOLAERSHIP CONTROLLER                                              
// CREATION DATE : 14-07-2015                                                         
// CREATED BY    : DIGEHS PATEL                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================



using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

namespace BusinessLogicLayer.BusinessLogic.SCHOLARSHIP
{
    public class StipendController
    {
        private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        #region  StipendMaster
        public int Add_STIPEND_MASTER_ENTRY(Stipend_Master objModifyEntry)
        {
            {
                int retStatus = 0;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                    SqlParameter[] objParams = null;
                    {
                        objParams = new SqlParameter[22];
                        objParams[0] = new SqlParameter("@P_IDNOS", objModifyEntry.IDNOSTR);
                        if (objModifyEntry.STFROMDT == null)
                        {
                            objParams[1] = new SqlParameter("@P_STFROMDT", "");
                        }
                        else
                        {
                            objParams[1] = new SqlParameter("@P_STFROMDT", objModifyEntry.STFROMDT);
                        }
                        if (objModifyEntry.STTODT == null)
                        {
                            objParams[2] = new SqlParameter("@P_STTODT", "");
                        }
                        else
                        {
                            objParams[2] = new SqlParameter("@P_STTODT", objModifyEntry.STTODT);
                        }
                        objParams[3] = new SqlParameter("@P_STIPEND_MONTH ", objModifyEntry.STIPEND_MONTH);
                        objParams[4] = new SqlParameter("@P_MON_STIPEND_AMT", objModifyEntry.MON_STIPEND_AMT);

                        objParams[5] = new SqlParameter("@P_OPBAL", objModifyEntry.OPBAL);
                        objParams[6] = new SqlParameter("@P_SPSTATUS", objModifyEntry.SPSTATUS);
                        if (objModifyEntry.STFROMDT1 == null)
                        {
                            objParams[7] = new SqlParameter("@P_STFROMDT_1", "");
                        }
                        else
                        {
                            objParams[7] = new SqlParameter("@P_STFROMDT_1", objModifyEntry.STFROMDT1);
                        }

                        if (objModifyEntry.STTODT1 == null)
                        {
                            objParams[8] = new SqlParameter("@P_STTODT_1", "");

                        }
                        else
                        {
                            objParams[8] = new SqlParameter("@P_STTODT_1", objModifyEntry.STTODT1);
                        }



                        objParams[9] = new SqlParameter("@P_STIPEND_MONTH_1", objModifyEntry.STIPEND_MONTH1);
                        objParams[10] = new SqlParameter("@P_MON_STIPEND_AMT_1", objModifyEntry.MON_STIPEND_AMT1);


                        if (objModifyEntry.STFROMDT2 == null)
                        {
                            objParams[11] = new SqlParameter("@P_STFROMDT_2", "");
                        }
                        else
                        {
                            objParams[11] = new SqlParameter("@P_STFROMDT_2", objModifyEntry.STFROMDT2);
                        }

                        if (objModifyEntry.STTODT2 == null)
                        {
                            objParams[12] = new SqlParameter("@P_STTODT_2", "");

                        }
                        else
                        {
                            objParams[12] = new SqlParameter("@P_STTODT_2", objModifyEntry.STTODT2);
                        }
                        objParams[13] = new SqlParameter("@P_STIPEND_MONTH_2", objModifyEntry.STIPEND_MONTH2);
                        objParams[14] = new SqlParameter("@P_MON_STIPEND_AMT_2", objModifyEntry.MON_STIPEND_AMT2);

                        if (objModifyEntry.REVISEDDATE == null)
                        {
                            objParams[15] = new SqlParameter("@P_REVISEDDATE", "");
                        }
                        else
                        {
                            objParams[15] = new SqlParameter("@P_REVISEDDATE", objModifyEntry.REVISEDDATE);
                        }

                        objParams[16] = new SqlParameter("@P_REVISEDAMT", objModifyEntry.REVISEDAMT);

                        if (objModifyEntry.EFFECTIVEREVISEDDATE == null)
                        {
                            objParams[17] = new SqlParameter("@P_EFFECTIVEREVISEDDATE", "");

                        }
                        else
                        {
                            objParams[17] = new SqlParameter("@P_EFFECTIVEREVISEDDATE", objModifyEntry.EFFECTIVEREVISEDDATE);
                        }
                        if (objModifyEntry.ACCLOSEDATE == null)
                        {
                            objParams[18] = new SqlParameter("@P_ACCLOSEDATE", "");
                        }
                        else
                        {
                            objParams[18] = new SqlParameter("@P_ACCLOSEDATE", objModifyEntry.ACCLOSEDATE);
                        }
                        objParams[19] = new SqlParameter("@P_ACCLOSERMK", objModifyEntry.ACCLOSERMK);
                        objParams[20] = new SqlParameter("@P_ADDITIONALAMT", objModifyEntry.ADDITIONALAMT);

                        objParams[21] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[21].Direction = ParameterDirection.Output;
                    };
                    if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STIPEND_MASTER_INS", objParams, false) != null)
                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                catch (Exception ex)
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ScholarshipController.AddScholrshipNotice-> " + ex.ToString());
                }
                return retStatus;
            }
        }

        public int UPDATE_STIPEND_MASTER_ENTRY(Stipend_Master objModifyEntry)
        {
            {
                int retStatus = 0;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                    SqlParameter[] objParams = null;
                    {
                        objParams = new SqlParameter[25];
                        objParams[0] = new SqlParameter("@P_IDNO", objModifyEntry.Id_No);
                        if (objModifyEntry.STFROMDT == null)
                        {
                            objParams[1] = new SqlParameter("@P_STFROMDT", "");
                        }
                        else
                        {
                            objParams[1] = new SqlParameter("@P_STFROMDT", objModifyEntry.STFROMDT);
                        }
                        if (objModifyEntry.STTODT == null)
                        {
                            objParams[2] = new SqlParameter("@P_STTODT", "");
                        }
                        else
                        {
                            objParams[2] = new SqlParameter("@P_STTODT", objModifyEntry.STTODT);
                        }
                        objParams[3] = new SqlParameter("@P_STIPEND_MONTH ", objModifyEntry.STIPEND_MONTH);
                        objParams[4] = new SqlParameter("@P_MON_STIPEND_AMT", objModifyEntry.MON_STIPEND_AMT);

                        objParams[5] = new SqlParameter("@P_OPBAL", objModifyEntry.OPBAL);
                        objParams[6] = new SqlParameter("@P_SPSTATUS", objModifyEntry.SPSTATUS);
                        if (objModifyEntry.STFROMDT1 == null)
                        {
                            objParams[7] = new SqlParameter("@P_STFROMDT_1", "");
                        }
                        else
                        {
                            objParams[7] = new SqlParameter("@P_STFROMDT_1", objModifyEntry.STFROMDT1);
                        }

                        if (objModifyEntry.STTODT1 == null)
                        {
                            objParams[8] = new SqlParameter("@P_STTODT_1", "");

                        }
                        else
                        {
                            objParams[8] = new SqlParameter("@P_STTODT_1", objModifyEntry.STTODT1);
                        }



                        objParams[9] = new SqlParameter("@P_STIPEND_MONTH_1", objModifyEntry.STIPEND_MONTH1);
                        objParams[10] = new SqlParameter("@P_MON_STIPEND_AMT_1", objModifyEntry.MON_STIPEND_AMT1);


                        if (objModifyEntry.STFROMDT2 == null)
                        {
                            objParams[11] = new SqlParameter("@P_STFROMDT_2", "");
                        }
                        else
                        {
                            objParams[11] = new SqlParameter("@P_STFROMDT_2", objModifyEntry.STFROMDT2);
                        }

                        if (objModifyEntry.STTODT2 == null)
                        {
                            objParams[12] = new SqlParameter("@P_STTODT_2", "");

                        }
                        else
                        {
                            objParams[12] = new SqlParameter("@P_STTODT_2", objModifyEntry.STTODT2);
                        }
                        objParams[13] = new SqlParameter("@P_STIPEND_MONTH_2", objModifyEntry.STIPEND_MONTH2);
                        objParams[14] = new SqlParameter("@P_MON_STIPEND_AMT_2", objModifyEntry.MON_STIPEND_AMT2);

                        if (objModifyEntry.REVISEDDATE == null)
                        {
                            objParams[15] = new SqlParameter("@P_REVISEDDATE", "");
                        }
                        else
                        {
                            objParams[15] = new SqlParameter("@P_REVISEDDATE", objModifyEntry.REVISEDDATE);
                        }

                        objParams[16] = new SqlParameter("@P_REVISEDAMT", objModifyEntry.REVISEDAMT);

                        if (objModifyEntry.EFFECTIVEREVISEDDATE == null)
                        {
                            objParams[17] = new SqlParameter("@P_EFFECTIVEREVISEDDATE", "");

                        }
                        else
                        {
                            objParams[17] = new SqlParameter("@P_EFFECTIVEREVISEDDATE", objModifyEntry.EFFECTIVEREVISEDDATE);
                        }
                        if (objModifyEntry.ACCLOSEDATE == null)
                        {
                            objParams[18] = new SqlParameter("@P_ACCLOSEDATE", "");
                        }
                        else
                        {
                            objParams[18] = new SqlParameter("@P_ACCLOSEDATE", objModifyEntry.ACCLOSEDATE);
                        }
                        objParams[19] = new SqlParameter("@P_ACCLOSERMK", objModifyEntry.ACCLOSERMK);
                        objParams[20] = new SqlParameter("@P_SRNO", objModifyEntry.Sr_No);
                        objParams[21] = new SqlParameter("@P_ADDITIONALAMT", objModifyEntry.ADDITIONALAMT);
                        objParams[22] = new SqlParameter("@P_STOPTRAN", objModifyEntry.StopTransaction);
                        objParams[23] = new SqlParameter("@P_STOPTRAN_RMK", objModifyEntry.Remarks);
                        objParams[24] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[24].Direction = ParameterDirection.Output;
                    };
                    if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STIPEND_MASTER_UPDATE", objParams, false) != null)
                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                catch (Exception ex)
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ScholarshipController.AddScholrshipNotice-> " + ex.ToString());
                }
                return retStatus;
            }
        }

        public int Add_STIPEND_DETAIL_ENTRY(Stipend_Master objModifyEntry)
        {
            {
                int retStatus = 0;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                    SqlParameter[] objParams = null;
                    {
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_IDNO", objModifyEntry.IDNOSTR);
                        objParams[1] = new SqlParameter("@P_OPBAL_AMT", objModifyEntry.OPBAL);
                        objParams[2] = new SqlParameter("@_REVISED_AMT", objModifyEntry.REVISEDAMT);
                        if (objModifyEntry.REVISEDDATE == null)
                        {
                            objParams[3] = new SqlParameter("@P_REVISED_DATE", "");
                        }
                        else
                        {
                            objParams[3] = new SqlParameter("@P_REVISED_DATE", objModifyEntry.REVISEDDATE);
                        }
                        if (objModifyEntry.EFFECTIVEREVISEDDATE == null)
                        {
                            objParams[4] = new SqlParameter("@P_EFFECTIVE_REVISEDDATE", "");
                        }
                        else
                        {
                            objParams[4] = new SqlParameter("@P_EFFECTIVE_REVISEDDATE", objModifyEntry.EFFECTIVEREVISEDDATE);
                        }
                        objParams[5] = new SqlParameter("@P_TOTALBAL_AMT", objModifyEntry.OPBAL + objModifyEntry.REVISEDAMT);
                        objParams[6] = new SqlParameter("@P_ADDITIONALAMT", objModifyEntry.ADDITIONALAMT);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                    };
                    if (objSQLHelper.ExecuteNonQuerySP("ACD_STIPEND_DETAIL_INSERT", objParams, false) != null)
                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                catch (Exception ex)
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ScholarshipController.AddScholrshipNotice-> " + ex.ToString());
                }
                return retStatus;
            }
        }
        public int Add_STIPEND_DETAIL_ADDTIONALAMT(Stipend_Master objModifyEntry)
        {
            {
                int retStatus = 0;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                    SqlParameter[] objParams = null;
                    {
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_IDNO", objModifyEntry.IDNOSTR);
                        objParams[1] = new SqlParameter("@P_OPBAL_AMT", objModifyEntry.OPBAL);
                        objParams[2] = new SqlParameter("@_REVISED_AMT",objModifyEntry.REVISEDAMT);
                        if (objModifyEntry.REVISEDDATE == null)
                        {
                            objParams[3] = new SqlParameter("@P_REVISED_DATE", "");
                        }
                        else
                        {
                            objParams[3] = new SqlParameter("@P_REVISED_DATE", objModifyEntry.REVISEDDATE);
                        }
                        if (objModifyEntry.EFFECTIVEREVISEDDATE == null)
                        {
                            objParams[4] = new SqlParameter("@P_EFFECTIVE_REVISEDDATE", "");
                        }
                        else
                        {
                            objParams[4] = new SqlParameter("@P_EFFECTIVE_REVISEDDATE", objModifyEntry.EFFECTIVEREVISEDDATE);
                        }
                        objParams[5] = new SqlParameter("@P_TOTALBAL_AMT", objModifyEntry.OPBAL + objModifyEntry.ADDITIONALAMT);
                        objParams[6] = new SqlParameter("@P_ADDITIONALAMT", objModifyEntry.ADDITIONALAMT);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                    };
                    if (objSQLHelper.ExecuteNonQuerySP("ACD_STIPEND_DETAIL_INSERT", objParams, false) != null)
                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                catch (Exception ex)
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ScholarshipController.AddScholrshipNotice-> " + ex.ToString());
                }
                return retStatus;
            }
        }

        public int DELETE_STIPEND_DETAIL_ENTRY(Stipend_Master objModifyEntry)
        {
            {
                int retStatus = 0;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                    SqlParameter[] objParams = null;
                    {
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ID", objModifyEntry.Id_No);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                    };
                    if (objSQLHelper.ExecuteNonQuerySP("ACD_STIPEND_DETAIL_DELETE", objParams, false) != null)
                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                catch (Exception ex)
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ScholarshipController.AddScholrshipNotice-> " + ex.ToString());
                }
                return retStatus;
            }
        }
     
        public int DELETE_STIPEND_MASTER_ENTRY(Stipend_Master objModifyEntry)
        {
            {
                int retStatus = 0;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                    SqlParameter[] objParams = null;
                    {
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SRNOS", objModifyEntry.IDNOSTR);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                    };
                    if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STIPEND_MASTER_DELETE", objParams, false) != null)
                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                catch (Exception ex)
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ScholarshipController.AddScholrshipNotice-> " + ex.ToString());
                }
                return retStatus;
            }
        }
      
        public DataSet GetRevisedAmt(int IDNO, string FROMDT, string REVISEDDT, string TODT,int DEGREENO, int BRANCHNO, int MONTHS1, decimal MONTHS1AMT)
        {
            {
                int retStatus = 0;
                DataSet ds = null;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                    SqlParameter[] objParams = null;
                    {

                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_FROMDT", FROMDT);
                        objParams[2] = new SqlParameter("@P_REVISEDDT", REVISEDDT);
                        objParams[3] = new SqlParameter("@P_TODT", TODT);
                        objParams[4] = new SqlParameter("@P_DEGREENO", DEGREENO);
                        objParams[5] = new SqlParameter("@P_BRANCHNO", BRANCHNO);
                        objParams[6] = new SqlParameter("@P_MONTHS1", MONTHS1);
                        objParams[7] = new SqlParameter("@P_MONTHS1AMT", MONTHS1AMT);
                        objParams[8] = new SqlParameter("@P_RTNOPBAL", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;
                    };
                    ds = objSQLHelper.ExecuteDataSetSP("PKG_STIPEND_REVISEDAMOUNT", objParams);
                    return ds;
                }
                catch (Exception ex)
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ScholarshipController.AddScholrshipNotice-> " + ex.ToString());
                }
            }
        }
    
        public DataSet GetPHDRevisedAmt(int IDNO, string FROMDT, string REVISEDDT, string TODT, int DEGREENO, int BRANCHNO, int MONTHS1, decimal MONTHS1AMT,int MONTHS2, decimal MONTHS2AMT)
        {
            {
                int retStatus = 0;
                DataSet ds = null;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                    SqlParameter[] objParams = null;
                    {

                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_FROMDT", FROMDT);
                        objParams[2] = new SqlParameter("@P_REVISEDDT", REVISEDDT);
                        objParams[3] = new SqlParameter("@P_TODT", TODT);
                        objParams[4] = new SqlParameter("@P_DEGREENO", DEGREENO);
                        objParams[5] = new SqlParameter("@P_BRANCHNO", BRANCHNO);
                        objParams[6] = new SqlParameter("@P_MONTHS1", MONTHS1);
                        objParams[7] = new SqlParameter("@P_MONTHS1AMT", MONTHS1AMT);
                        objParams[8] = new SqlParameter("@P_MONTHS2", MONTHS1);
                        objParams[9] = new SqlParameter("@P_MONTHS2AMT", MONTHS1AMT);
                        objParams[10] = new SqlParameter("@P_RTNOPBAL", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;
                    };
                    ds = objSQLHelper.ExecuteDataSetSP("PKG_STIPEND_PHDREVISEDAMOUNT", objParams);
                    return ds;
                }
                catch (Exception ex)
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ScholarshipController.AddScholrshipNotice-> " + ex.ToString());
                }
            }
        }
        public DataSet GetStipendMasterAmt()
        {
            {
                int retStatus = 0;
                DataSet ds = null;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                    SqlParameter[] objParams = null;
                    {

                        objParams = new SqlParameter[0];
                    };
                    ds = objSQLHelper.ExecuteDataSetSP("PKG_GETSTIPEND_MASTERAMOUNT", objParams);
                    return ds;
                }
                catch (Exception ex)
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ScholarshipController.AddScholrshipNotice-> " + ex.ToString());
                }
            }
        }
        # endregion
    }
}
