//======================================================================================
// PROJECT NAME  : CCMS                                                                
// MODULE NAME   : BUSINESS LOGIC FILE
// CREATION DATE : 04-April-2011                                                        
// CREATED BY    : ANKIT AGRAWAL
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================  

using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Collections;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLogicLayer;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLogicLayer.BusinessLogic
        {
            public class PayConfigurationController
            {
                private string _uaims_reg_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                public string _client_constr = string.Empty;

                public PayConfigurationController()
                {
                    _client_constr = _uaims_reg_constr;
                }

                public PayConfigurationController(string DbUserName, string DbPassword, String DataBase)
                {
                    _client_constr = "Password=" + DbPassword + ";User ID=" + DbUserName + "; SERVER=" + HttpContext.Current.Session["Server"].ToString().Trim() + ";DataBase=" + DataBase + ";";

                }

                public DataSet GetConfiguration()
                {
                    DataSet dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_client_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PAY_CONFIGURATION", objParams).Tables[0].DataSet;

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayConfigurationController.GetCurrentConfiguration->" + ex.ToString());
                    }
                    return dtr;

                }
                public int UpdateRefTable(PayConfiguration objConfig)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_client_constr);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[76];


                        objParams[0] = new SqlParameter("@P_DA", objConfig.DA);
                        objParams[1] = new SqlParameter("@P_CLA", objConfig.CLA);
                        objParams[2] = new SqlParameter("@P_TA", objConfig.TA);
                        objParams[3] = new SqlParameter("@P_DA_ON_TA", objConfig.DAONTA);
                        objParams[4] = new SqlParameter("@P_RECOVERY", objConfig.RECOVERY);
                        objParams[5] = new SqlParameter("@P_GPF_ADD", objConfig.GPFADD);
                        objParams[6] = new SqlParameter("@P_GPF_ADV", objConfig.GPF_ADV);
                        objParams[7] = new SqlParameter("@P_IT", objConfig.IT);

                        objParams[8] = new SqlParameter("@P_PT", objConfig.PT);

                        objParams[9] = new SqlParameter("@P_HRA", objConfig.HRA);
                        objParams[10] = new SqlParameter("@P_LIC", objConfig.LIC);
                        //objParams[11] = new SqlParameter("@P_GIS", objConfig.GIS);
                        //objParams[12] = new SqlParameter("@P_LISEFEE", objConfig.LISEFEE);
                        //objParams[13] = new SqlParameter("@P_SOCIETY", objConfig.SOCIETY);
                        objParams[11] = new SqlParameter("@P_LWP", objConfig.LWP);
                        objParams[12] = new SqlParameter("@P_DE", objConfig.DE);
                        objParams[13] = new SqlParameter("@P_G80", objConfig.G80);
                        objParams[14] = new SqlParameter("@P_CPF", objConfig.CPF);
                        objParams[15] = new SqlParameter("@P_CPF_LOAN", objConfig.CPFLOAN);
                        objParams[16] = new SqlParameter("@P_PHONE", objConfig.PHONE);
                        objParams[17] = new SqlParameter("@P_MEDICAL", objConfig.MEDICAL);
                        objParams[18] = new SqlParameter("@P_RD", objConfig.RD);
                        objParams[19] = new SqlParameter("@P_HONO", objConfig.HONO);
                        // 
                        objParams[20] = new SqlParameter("@P_GPF", objConfig.GPF);
                        objParams[21] = new SqlParameter("@P_PRINCIPAL", objConfig.PRINCIPAL);





                        //objParams[26] = new SqlParameter("@P_BANK", objConfig.BANK);
                        //objParams[27] = new SqlParameter("@P_SECTIONXI", objConfig.SECTIONXI);
                        if (!objConfig.FROMDATE.Equals(DateTime.MinValue))
                            objParams[22] = new SqlParameter("@P_FROMDATE", objConfig.FROMDATE);
                        else
                            objParams[22] = new SqlParameter("@P_FROMDATE", DBNull.Value);
                        objParams[23] = new SqlParameter("@P_CPFPER", objConfig.CPFPER);
                        objParams[24] = new SqlParameter("@P_LICBRANCH", objConfig.LICBRANCH);
                        objParams[25] = new SqlParameter("@P_TANNO", objConfig.TANNO);
                        if (!objConfig.PFFSDATE.Equals(DateTime.MinValue))
                            objParams[26] = new SqlParameter("@P_PFFSDATE", objConfig.PFFSDATE);
                        else
                            objParams[26] = new SqlParameter("@P_PFFSDATE", DBNull.Value);
                        if (!objConfig.CPFFROMDATE.Equals(DateTime.MinValue))
                            objParams[27] = new SqlParameter("@P_CPFFROMDATE", objConfig.CPFFROMDATE);
                        else
                            objParams[27] = new SqlParameter("@P_CPFFROMDATE", DBNull.Value);
                        //objParams[34] = new SqlParameter("@P_COLLEGECODE", objConfig.COLLEGECODE);
                        //objParams[35] = new SqlParameter("@P_BANKLOCATION", objConfig.BANKLOCATION);
                        //objParams[36] = new SqlParameter("@P_SECTIONXII", objConfig.SECTIONXII);








                        objParams[28] = new SqlParameter("@P_DAPER", objConfig.DAPER);
                        if (!objConfig.TODATE.Equals(DateTime.MinValue))
                            objParams[29] = new SqlParameter("@P_TODATE", objConfig.TODATE);
                        else
                            objParams[29] = new SqlParameter("@P_TODATE", DBNull.Value);




                        //objParams[39] = new SqlParameter("@P_ACCOUNT", objConfig.ACCOUNT);


                        objParams[30] = new SqlParameter("@P_PACNO", objConfig.PACNO);
                        objParams[31] = new SqlParameter("@P_PANNO", objConfig.PANNO);



                        if (!objConfig.PFFEDATE.Equals(DateTime.MinValue))
                            objParams[32] = new SqlParameter("@P_PFFEDATE", objConfig.PFFEDATE);
                        else
                            objParams[32] = new SqlParameter("@P_PFFEDATE", DBNull.Value);
                        if (!objConfig.CPFTODATE.Equals(DateTime.MinValue))
                            objParams[33] = new SqlParameter("@P_CPFTODATE", objConfig.CPFTODATE);
                        else
                            objParams[33] = new SqlParameter("@P_CPFTODATE", DBNull.Value);




                        //objParams[39] = new SqlParameter("@P_LINKACC", objConfig.LINKACC);








                        //objParams[45] = new SqlParameter("@P_ITREGNO", objConfig.ITREGNO);
                        //objParams[46] = new SqlParameter("@P_PTREGNO", objConfig.PTREGNO);
                        // objParams[47] = new SqlParameter("@P_BANKLOAN", objConfig.BANKLOAN);
                        // objParams[48] = new SqlParameter("@P_PUJAFUND", objConfig.PUJAFUND);
                        //objParams[49] = new SqlParameter("@P_NTFUND", objConfig.NTFUND);
                        //objParams[50] = new SqlParameter("@P_CRFUND", objConfig.CRFUND);
                        //objParams[51] = new SqlParameter("@P_OTHERDED", objConfig.OTHERDED);
                        //objParams[52] = new SqlParameter("@P_PAYUNIT_NO", objConfig.PAYUNITNO);
                        // objParams[53] = new SqlParameter("@P_CODENO", objConfig.CODENO);
                        // objParams[54] = new SqlParameter("@P_ZONE", objConfig.ZONE);
                        //  objParams[55] = new SqlParameter("@P_WARD", objConfig.WARD);

                        // objParams[57] = new SqlParameter("@P_REGISTRAR", objConfig.REGISTRAR);

                        objParams[34] = new SqlParameter("@P_COLNAME", objConfig.COLNAME);
                        objParams[35] = new SqlParameter("@P_SOCNAME", objConfig.SOCNAME);
                        objParams[36] = new SqlParameter("@P_COMPANY", objConfig.COMPANY);
                        objParams[37] = new SqlParameter("@P_ACCPW", objConfig.ACCPW);
                        objParams[38] = new SqlParameter("@P_SINGLE_INV", objConfig.SINGLE_INV);
                        objParams[39] = new SqlParameter("@P_RWIDTH", objConfig.RWIDTH);
                        objParams[40] = new SqlParameter("@P_RHEIGHT", objConfig.RHEIGHT);
                        objParams[41] = new SqlParameter("@P_SALPASSWORD", objConfig.SALPASSWORD);
                        objParams[42] = new SqlParameter("@P_GPFPER", objConfig.GPFPER);
                        objParams[43] = new SqlParameter("@P_ENAMESIZE", objConfig.ENAMESIZE);
                        objParams[44] = new SqlParameter("@P_ANOSIZE", objConfig.ANOSIZE);
                        objParams[45] = new SqlParameter("@P_AMTSIZE", objConfig.AMTSIZE);
                        objParams[46] = new SqlParameter("@P_LWP_CAL", objConfig.LWP_CAL);
                        objParams[47] = new SqlParameter("@P_DOSFONT", objConfig.DOSFONT);
                        objParams[48] = new SqlParameter("@P_ZERO", objConfig.ZERO);
                        objParams[49] = new SqlParameter("@P_RD_PER", objConfig.RD_PER);
                        objParams[50] = new SqlParameter("@P_LWP_PDAY", objConfig.LWP_PDAY);
                        objParams[51] = new SqlParameter("@P_LICEFEE_FIELD", objConfig.LICEFEE_FIELD);
                        objParams[52] = new SqlParameter("@P_PROPOSED_SAL", objConfig.PROPOSED_SAL);
                        objParams[53] = new SqlParameter("@P_MODULE_NO", objConfig.MODULE_NO);
                        objParams[54] = new SqlParameter("@P_LINK_NO", objConfig.LINK_NO);
                        //objParams[57] = new SqlParameter("@P_LINK_NO", objConfig.LINK_NO);
                        objParams[55] = new SqlParameter("@P_LEAVE_APPROVAL", objConfig.LEAVE_APPROVAL);
                        //objParams[58] = new SqlParameter("@P_PH_AMT", objConfig.PH_AMT);
                        //objParams[59] = new SqlParameter("@P_STAFFNO", objConfig.STAFFNO);
                        objParams[56] = new SqlParameter("@P_GradePay", objConfig.GP_Status);
                        objParams[57] = new SqlParameter("@P_DP", objConfig.DP_Status);
                        objParams[58] = new SqlParameter("@P_EPFAMOUNT",objConfig.EPFAMOUNT);
                        objParams[59] = new SqlParameter("@P_STAFF_APPLICABLE", objConfig.STAFF_APPLICABLE);

                        objParams[60] = new SqlParameter("@P_OVERTIMEHEAD", objConfig.OverTimeHead);
                         
                        //Amol sawarkar 04-03-2022


                        if (!objConfig.EmpPayslipShowFromDate.Equals(DateTime.MinValue))
                            objParams[61] = new SqlParameter("@P_EmpPayslipShowFromDate", objConfig.EmpPayslipShowFromDate);
                        else
                            objParams[61] = new SqlParameter("@P_EmpPayslipShowFromDate", DBNull.Value);

                      //  objParams[61] = new SqlParameter("@P_EmpPayslipShowFromDate",objConfig.EmpPayslipShowFromDate);
                        objParams[62] = new SqlParameter("@P_ESICNO", objConfig.ESICNO);
                        objParams[63] = new SqlParameter("@P_ESI_LIMIT_HP", objConfig.ESI_LIMIT_HP);
                        objParams[64] = new SqlParameter("@P_ESI_PER_HP", objConfig.ESI_PER_HP);
                        objParams[65] = new SqlParameter("@P_EmployerContribution", objConfig.EmployerContribution);
                        objParams[66] = new SqlParameter("@P_ESIC_LIMIT", objConfig.ESIC_LIMIT);

                   

                        if (!objConfig.ESIFIRSTFROMDATE.Equals(DateTime.MinValue))
                            objParams[67] = new SqlParameter("@P_ESIFIRSTFROMDATE", objConfig.ESIFIRSTFROMDATE);
                        else
                            objParams[67] = new SqlParameter("@P_ESIFIRSTFROMDATE", DBNull.Value);

                       // objParams[67] = new SqlParameter("@P_ESIFIRSTFROMDATE", objConfig.ESIFIRSTFROMDATE);


                        if (!objConfig.ESIFIRSTTODATE.Equals(DateTime.MinValue))
                            objParams[68] = new SqlParameter("@P_ESIFIRSTTODATE", objConfig.ESIFIRSTTODATE);
                        else
                            objParams[68] = new SqlParameter("@P_ESIFIRSTTODATE", DBNull.Value);

                       // objParams[68] = new SqlParameter("@P_ESIFIRSTTODATE", objConfig.ESIFIRSTFROMDATE);


                        if (!objConfig.ESISECONDFROMDATE.Equals(DateTime.MinValue))
                            objParams[69] = new SqlParameter("@P_ESISECONDFROMDATE", objConfig.ESISECONDFROMDATE);
                        else
                            objParams[69] = new SqlParameter("@P_ESISECONDFROMDATE", DBNull.Value);


                     //   objParams[69] = new SqlParameter("@P_ESISECONDFROMDATE", objConfig.ESISECONDFROMDATE);

                        if (!objConfig.ESISECONDTODATE.Equals(DateTime.MinValue))
                            objParams[70] = new SqlParameter("@P_ESISECONDTODATE", objConfig.ESISECONDTODATE);
                        else
                            objParams[70] = new SqlParameter("@P_ESISECONDTODATE", DBNull.Value);

                       // objParams[70] = new SqlParameter("@P_ESISECONDTODATE", objConfig.ESISECONDTODATE);

                        //objParams[62] = new SqlParameter("@P_GPFLOAN", objConfig.GPFLOAN);

                        objParams[71] = new SqlParameter("@P_USERLOGINTYPE", objConfig.USERLOGINTYPE);
                        objParams[72] = new SqlParameter("@P_USERPASSWORD", objConfig.USERPASSWORD);

                        //add 14-09-2022
                        if (objConfig.PhotoSign == null)
                            objParams[73] = new SqlParameter("@P_PHOTOSign", DBNull.Value);
                        else
                            objParams[73] = new SqlParameter("@P_PHOTOSign", objConfig.PhotoSign);

                        objParams[73].SqlDbType = SqlDbType.Image;
                        objParams[74] = new SqlParameter("@P_AutoUserCreated", objConfig.IsAutoUserCreated);


                        objParams[75] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[75].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_CONFIGURATION", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayConfigurationController.UpdateRefTable -> " + ex.ToString());
                    }
                    return retStatus;

                }

                public DataSet GetAllPayHead()
                {
                    DataSet ds = null;
                    try

                    {
                        SQLHelper objSQLHelper = new SQLHelper(_client_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_ALL_PAYHEAD_FOR_CONFIG", objParams);
                    }
                    catch (Exception ex)

                    {
                        return ds;
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayController.GetAllPayHead-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAllStaff()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_client_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_STAFF", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.CCMS.BusinessLayer.BusinessLogic.PayHeadPrivilegesController.GetAllPayHead-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet EditStaff()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_client_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_STAFF_PAY_REF", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayHeadPrivilegesController.EditPayHeadUser-> " + ex.ToString());
                    }
                    finally
                    {


                        ds.Dispose();

                    }
                    return ds;

                }
            }
        }
    }
}
