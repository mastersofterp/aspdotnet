using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IITMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Data;
using IITMS.NITPRM;
using System.Web.UI.WebControls;
namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class CostCenterController
            {
                public string _client_constr = string.Empty;
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                public CostCenterController()
               {

               }

                //public CostCenterController(string DbUserName, string DbPassword, String DataBase)
                //{
                //    _client_constr = "Password=" + DbPassword + ";User ID=" + DbUserName + "; SERVER=" + HttpContext.Current.Session["Server"].ToString().Trim() + ";DataBase=" + DataBase + ";";
                //}

                public int CostCategoryAddUpdate(CostCenter objCostCenter, string Company_Code)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_COMPCODE", Company_Code);
                        objParams[1] = new SqlParameter("@P_CATID", objCostCenter.CATID);
                        objParams[2] = new SqlParameter("@P_CATEGORYNAME", objCostCenter.CATEGORYNAME);


                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_COST_CATEGORY_INSERT_UPDATE", objParams, true);
                        retStatus = 1;

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ex.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int CostCenterAddUpdate(CostCenter objCostCenter, string Company_Code)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_COMPCODE", Company_Code);
                        objParams[1] = new SqlParameter("@P_CC_ID", objCostCenter.CC_ID);
                        objParams[2] = new SqlParameter("@P_Cat_ID", objCostCenter.CATID);
                        objParams[3] = new SqlParameter("@P_CCNAME", objCostCenter.CCNAME);


                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_COST_CENTER_INSERT_UPDATE", objParams, true);
                        retStatus = 1;

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ex.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int CostCenterLedgerDelete(int Party_No, string Company_Code)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        objSQLHelper.ExecuteNonQuery("delete from Acc_" + Company_Code + "_CCLEDGER where Party_No=" + Party_No);
                        retStatus = 1;

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ex.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int CostCenterLedgerAdd(CostCenter objCostCenter, string Company_Code)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_COMPCODE", Company_Code);
                        objParams[1] = new SqlParameter("@P_PARTY_NO", objCostCenter.PARTY_NO);
                        objParams[2] = new SqlParameter("@P_PAYMENTTYPENO", objCostCenter.PAYMENTTYPENO);
                        objParams[3] = new SqlParameter("@P_OPBALANCE", objCostCenter.OPBALANCE);
                        objParams[4] = new SqlParameter("@P_STATUS", objCostCenter.STATUS);
                        objParams[5] = new SqlParameter("@P_APPLICABLE", objCostCenter.APPLICABLE);
                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_CCL_ADD", objParams, true);
                        retStatus = 1;

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ex.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int CostCenterVoucherDelete(int Vch_No,string VchType,int partyno, string Company_Code,string CCVH_DATE)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        objSQLHelper.ExecuteNonQuery("delete from Acc_" + Company_Code + "_CCVCH where VCHNO=" + Vch_No + " and VchType='" + VchType + "' and CCVH_DATE=convert(datetime,'" + Convert.ToDateTime(CCVH_DATE).ToString("dd-MMM-yyyy") + "',112) and PARTY_NO=" + partyno);
                        retStatus = 1;

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ex.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int CostCenterVoucherAdd(CostCenter objCostCenter, string Company_Code)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_COMPCODE", Company_Code);
                        objParams[1] = new SqlParameter("@P_VCHNO", objCostCenter.VCHNO);
                        objParams[2] = new SqlParameter("@P_PARTY_NO", objCostCenter.PARTY_NO);
                        objParams[3] = new SqlParameter("@P_CCID", objCostCenter.CC_ID);
                        objParams[4] = new SqlParameter("@P_CATID", objCostCenter.CATID);
                        objParams[5] = new SqlParameter("@P_VCHTYPE", objCostCenter.VCHTYPE);
                        objParams[6] = new SqlParameter("@P_CCVH_DATE", objCostCenter.CCVHDATE);
                        objParams[7] = new SqlParameter("@P_AMOUNT", objCostCenter.AMOUNT);
                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_CCVCH_ADD", objParams, true);
                        retStatus = 1;

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ex.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int CostCenterReport(string Company_Code,DateTime fromdate,DateTime todate)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_COMPCODE", Company_Code);
                        objParams[1] = new SqlParameter("@P_FROMDATE", fromdate.ToString("dd-MMM-yyyy"));
                        objParams[2] = new SqlParameter("@P_TODATE", todate.ToString("dd-MMM-yyyy"));

                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_COST_CENTER_REPORT", objParams, true);
                        retStatus = 1;

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ex.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ex.ToString());
                    }
                    return retStatus;
                }


                //----------------------------------------------------------Used Section for Isolated CHeque Print-------------------------------------------------------

                public int AddPayee(int IDNO, string PARYTNAME, string Company_Code)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_COMPCODE", Company_Code);
                        objParams[1] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[2] = new SqlParameter("@P_PARTYNAME", PARYTNAME);
                        objParams[3] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACC_PAYEE_INSERT_UPDATE", objParams, true));
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddIsolatedCheque(string CompanyCode, string UserName, DataTable dtCheque)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_INSOLATEDCHQ", dtCheque);
                        objParams[1] = new SqlParameter("@P_USERNAME", UserName);
                        objParams[2] = new SqlParameter("@P_COMPANY_CODE", CompanyCode);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_INSOLATED_CHEQUE_PRINT_INSERT", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else

                                retStatus = Convert.ToInt32(ret);// (CustomSatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddChequeEntryDetails-> " + ex.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddChequeEntryDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public void BindBudgetHead(DropDownList ddlList)
                {
                    try
                    {
                        SQLHelper objsqlhelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[0];
                        DataSet ds = null;
                        ds = objsqlhelper.ExecuteDataSetSP("PKG_ACC_SP_BUDGETHEAD_BIND", objParams);

                        ddlList.Items.Clear();
                        ddlList.Items.Add("Please Select");
                        ddlList.SelectedItem.Value = "0";

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlList.DataSource = ds;
                            ddlList.DataValueField = ds.Tables[0].Columns[0].ToString();
                            ddlList.DataTextField = ds.Tables[0].Columns[1].ToString();
                            ddlList.DataBind();
                            ddlList.SelectedIndex = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.Common.FillDropDown-> " + ex.ToString());
                    }
                }
                //ADDED BY VIJAY ANDOJU ON 20-08-2020 FOR GET BUDGETAPPLIED DETAILS
                public DataSet GETAPPLIEDBUDGET(DateTime FROMDATE, DateTime TODATE,string Isapproved)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", FROMDATE);
                        objParams[1] = new SqlParameter("@P_TO_DATE", TODATE);
                        if (Isapproved == "P")
                        {
                            ds = objSqlHelper.ExecuteDataSetSP("PKG_ACC_APPLIED_BUDGET_EXCEL_PENDING", objParams);
                        }
                        if (Isapproved == "A")
                        {
                            ds = objSqlHelper.ExecuteDataSetSP("PKG_ACC_APPLIED_BUDGET_EXCEL_APPROVED", objParams);
                        }
                       
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.Common.FillDropDown-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// CREATED BY : SHRIKANT AMBONE
                /// CREATED DATE : 03-04-2021
                /// METHOD NAME : CostCenterVoucherDeleteMakaut
                /// USED FOR : TO DELETE THE COST CENTER VOUCHER
                /// PARAMETER : int Vch_No, string VchType, int partyno, string Company_Code, string CCVH_DATE
                /// </summary>
                public int CostCenterVoucherDeleteMakaut(int Vch_No, string VchType, int partyno, string Company_Code, string CCVH_DATE)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        objSQLHelper.ExecuteNonQuery("delete from Acc_ALL_TEMP_CCVCH where VCHNO=" + Vch_No + " and VchType='" + VchType + "' and CCVH_DATE=convert(datetime,'" + Convert.ToDateTime(CCVH_DATE).ToString("dd-MMM-yyyy") + "',112) and PARTY_NO=" + partyno);
                        retStatus = 1;

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ex.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// CREATED BY : SHRIKANT AMBONE
                /// CREATED DATE : 03-04-2021
                /// METHOD NAME : CostCenterVoucherAddMakaut
                /// USED FOR : TO ADD THE COST CENTER VOUCHER
                /// PARAMETER : CostCenter objCostCenter, string Company_Code
                /// </summary>
                public int CostCenterVoucherAddMakaut(CostCenter objCostCenter, string Company_Code)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_COMPCODE", Company_Code);
                        objParams[1] = new SqlParameter("@P_VCHNO", objCostCenter.VCHNO);
                        objParams[2] = new SqlParameter("@P_PARTY_NO", objCostCenter.PARTY_NO);
                        objParams[3] = new SqlParameter("@P_CCID", objCostCenter.CC_ID);
                        objParams[4] = new SqlParameter("@P_CATID", objCostCenter.CATID);
                        objParams[5] = new SqlParameter("@P_VCHTYPE", objCostCenter.VCHTYPE);
                        objParams[6] = new SqlParameter("@P_CCVH_DATE", objCostCenter.CCVHDATE);
                        objParams[7] = new SqlParameter("@P_AMOUNT", objCostCenter.AMOUNT);
                        objSQLHelper.ExecuteNonQuerySP("PKG_ACC_CCVCH_ADD_MAKAUT", objParams, true);
                        retStatus = 1;

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ex.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ex.ToString());
                    }
                    return retStatus;
                }

                // use in accounting voucher page , for get the multipal cost center data 
                public DataSet GetCostCenterdata(int VOUCHERSQN,string CompCode, string istemp)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_VCHNO", VOUCHERSQN);
                        objParams[1] = new SqlParameter("@P_COMPCODE", CompCode);
                        if (istemp == "Y")
                        {
                            ds = objSqlHelper.ExecuteDataSetSP("PKG_ACC_GET_CCVCH_TEMP", objParams);
                        }
                        if (istemp == "N")
                        {
                            ds = objSqlHelper.ExecuteDataSetSP("PKG_ACC_GET_CCVCH_MAIN", objParams);
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.Common.FillDropDown-> " + ex.ToString());
                    }
                    return ds;
                }

                
                /// CREATED DATE : 15-05-2023
                /// METHOD NAME : DeleteCostCenterVoucherDelete
                /// USED FOR : TO DELETE THE COST CENTER VOUCHER
                /// PARAMETER : int Vch_No, string VchType, int partyno, string Company_Code, string CCVH_DATE
                /// </summary>
                public int DeleteMultipalCostCenterVoucher(int Vch_No, string VchType, string Company_Code, string CCVH_DATE, string IsTemp)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        if (IsTemp == "Y")
                        {
                            objSQLHelper.ExecuteNonQuery("delete from Acc_ALL_TEMP_CCVCH where VCHNO=" + Vch_No + " and VchType='" + VchType + "' and CCVH_DATE=convert(datetime,'" + Convert.ToDateTime(CCVH_DATE).ToString("dd-MMM-yyyy") + "',112)");
                            retStatus = 1;
                        }
                        else
                        {
                            objSQLHelper.ExecuteNonQuery("delete from Acc_" + Company_Code + "_CCVCH where VCHNO=" + Vch_No + " and VchType='" + VchType + "' and CCVH_DATE=convert(datetime,'" + Convert.ToDateTime(CCVH_DATE).ToString("dd-MMM-yyyy") + "',112)");
                            retStatus = 1;

                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ex.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ex.ToString());
                    }
                    return retStatus;
                }

            }
        }
    }
}
