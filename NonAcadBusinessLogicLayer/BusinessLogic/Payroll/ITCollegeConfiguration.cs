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
            public class ITCollegeConfiguration
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                /// <summary>
                /// Get Current Configuration in page Pay_ITConfiguration.aspx on page load.
                /// </summary>
                /// <returns></returns>
                public DataTableReader GetITCollegeConfiguration()
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_IT_COLLEGE_CONFIGURATION", objParams).Tables[0].CreateDataReader();

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITCollegeConfiguration.GetITCollegeConfiguration->" + ex.ToString());
                    }
                    return dtr;

                }


                /// <summary>
                /// Add Employer Information from IT Configuration page.
                /// </summary>
                /// <param name="objIT"> objIT is the Object of ITConfiguration Class</param>
                /// <returns>Integer CustomStatus Record Added or Error</returns>
                public int AddUpdateITCollegeConfiguration(ITConfiguration objIT)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_COLLEG_NO", objIT.COLLEGENO);
                        objParams[1] = new SqlParameter("@P_PANNO", objIT.PANNO);
                        objParams[2] = new SqlParameter("@P_TANNO", objIT.TANNO);
                        objParams[3] = new SqlParameter("@P_SIGNNAME", objIT.SIGNNAME);
                        objParams[4] = new SqlParameter("@P_SONOF", objIT.SONOF);
                        objParams[5] = new SqlParameter("@P_DESIGNATION", objIT.DESIGNATION);
                        
                        //objParams[6] = new SqlParameter("@P_FDATE", objIT.FROMDATE);
                        if (!objIT.FROMDATE.Equals(DateTime.MinValue))
                            objParams[6] = new SqlParameter("@P_FDATE", objIT.FROMDATE);
                        else
                            objParams[6] = new SqlParameter("@P_FDATE", DBNull.Value);

                        //objParams[7] = new SqlParameter("@P_TDATE", objIT.TODATE);
                        if (!objIT.TODATE.Equals(DateTime.MinValue))
                            objParams[7] = new SqlParameter("@P_TDATE", objIT.TODATE);
                        else
                            objParams[7] = new SqlParameter("@P_TDATE", DBNull.Value);

                        objParams[8] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_ADDUPD_IT_COLLEGE_CONFIGURATION", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITMascontroller.AddITConfiguration -> " + ex.ToString());
                    }
                    return retStatus;
                }
                /// <summary>
                /// Get Current Configuration in page Pay_ITConfiguration.aspx on page load.
                /// </summary>
                /// <returns></returns>
                public DataTableReader GetITCollegeConfiguration(ITConfiguration objIT)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_COLLEGE_NO", objIT.COLLEGENO);
                        objParams[1] = new SqlParameter("@P_FDATE", objIT.FROMDATE);
                        objParams[2] = new SqlParameter("@P_TDATE", objIT.TODATE);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_IT_COLLEGE_CONFIGURATION", objParams).Tables[0].CreateDataReader();

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayConfigurationController.GetITConfiguration->" + ex.ToString());
                    }
                    return dtr;

                }

                //24Q excel report
                public DataSet IT24QExcelReport(string fromdate, string todate, int empno, int staffno, int collegno, string options, string orderby, int supzeroamt, int supneg)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_FROMDATE", fromdate);
                        objParams[1] = new SqlParameter("@P_TODATE", todate);
                        objParams[2] = new SqlParameter("@P_EMPNO", empno);
                        objParams[3] = new SqlParameter("@P_STAFFNO", staffno);
                        objParams[4] = new SqlParameter("@P_COLLEGENO", collegno);
                        objParams[5] = new SqlParameter("@P_OPTIONS", options);
                        objParams[6] = new SqlParameter("@P_OREDERBY", orderby);
                        objParams[7] = new SqlParameter("@P_SUPZEROAMT", supzeroamt);
                        objParams[8] = new SqlParameter("@P_SUPNEG", supneg);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_IT_24Q_REPORT_DMIMS_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForDisplayReport-> " + ex.ToString());
                    }
                    return ds;
                }
            }
        }
    }
}
