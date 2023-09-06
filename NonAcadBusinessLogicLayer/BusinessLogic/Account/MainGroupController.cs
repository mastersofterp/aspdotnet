using System;
using System.Data;
using System.Web;
using IITMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using IITMS.NITPRM;
namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class MainGroupController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>
               // private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                public string _client_constr = string.Empty;
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public MainGroupController()
                {
                }
                public MainGroupController( string DbPassword,string DbUserName,String DataBase)
                {
                    _client_constr = "Password=" + DbPassword + ";User ID=" + DbUserName + "; SERVER=" + HttpContext.Current.Session["Server"].ToString().Trim() + ";DataBase=" + DataBase + ";";
                }
                
                public int AddMainGroup(MainGroup objMG,string code_year)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_MGRP_NAME", objMG.Mgrp_Name);
                        objParams[2] = new SqlParameter("@P_PR_NO", objMG.Pr_No);
                        objParams[3] = new SqlParameter("@P_FA_NO", objMG.Fa_No);
                        objParams[4] = new SqlParameter("@P_ACC_CODE", objMG.Acc_Code);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objMG.College_Code);
                        objParams[6] = new SqlParameter("@P_PAYMENT_TYPE", objMG.Payment_type);
                        objParams[7] = new SqlParameter("@P_MGRP_NO", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_INS_MAIN_GROUP", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MainGroupController.AddMainGroup-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateMainGroup(MainGroup objMG, string code_year)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Update MainGroup Group
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_MGRP_NO", objMG.Mgrp_No);
                        objParams[2] = new SqlParameter("@P_MGRP_NAME", objMG.Mgrp_Name);
                        objParams[3] = new SqlParameter("@P_PR_NO", objMG.Pr_No);
                        objParams[4] = new SqlParameter("@P_FA_NO", objMG.Fa_No);
                        objParams[5] = new SqlParameter("@P_ACC_CODE", objMG.Acc_Code);
                        objParams[6] = new SqlParameter("@P_PAYMENT_TYPE", objMG.Payment_type);
                        objParams[7] = new SqlParameter("@P_OP", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SP_UPD_MAIN_GROUP", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MainGroupController.UpdateMainGroup-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataTableReader GetMainGroup(int mgrp_no,object code_year)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_MGRP_NO", mgrp_no);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_RET_MAIN_GROUP", objParams).Tables[0].CreateDataReader();

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MainGroupController.GetMainGroup-> " + ex.ToString());
                    }
                    return dtr;
                }
            }

        }//END: BusinessLayer.BusinessLogic

    }//END: UAIMS  

}//END: IITMS