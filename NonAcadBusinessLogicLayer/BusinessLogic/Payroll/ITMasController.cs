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
            public class ITMasController
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


                /// <summary>
                /// AddEmployee method is used to add new Employee.
                /// </summary>
                /// <param name="objIT">objIT is the object of ITMaster class</param>
                /// <returns>Integer CustomStatus Record Added or Error</returns>
                public int AddEmployeeIT(ITMaster objIT)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New Employee
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", objIT.IDNO);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", objIT.COLLEGE_CODE);
                        //objParams[2] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        //objParams[2].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySPTrans("PKG_PAY_INS_IT", objParams, true, 3) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITMascontroller.AddEmployeeIT -> " + ex.ToString());
                    }
                    return retStatus;
                }


                /// <summary>
                /// Add Employer Information from IT Configuration page.
                /// </summary>
                /// <param name="objIT"> objIT is the Object of ITConfiguration Class</param>
                /// <returns>Integer CustomStatus Record Added or Error</returns>
                public int AddITConfiguration(ITConfiguration objIT)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[56];
                        objParams[0] = new SqlParameter("@P_COLLEGENAME", objIT.COLLEGENAME);
                        objParams[1] = new SqlParameter("@P_PANNO", objIT.PANNO);
                        objParams[2] = new SqlParameter("@P_TANNO", objIT.TANNO);
                        objParams[3] = new SqlParameter("@P_SIGNNAME", objIT.SIGNNAME);
                        objParams[4] = new SqlParameter("@P_SECTION", objIT.SECTION);
                        objParams[5] = new SqlParameter("@P_SONOF", objIT.SONOF);
                        objParams[5] = new SqlParameter("@P_DESIGNATION", objIT.DESIGNATION);
                        objParams[6] = new SqlParameter("@P_EMPBLOCKNO", objIT.EMPBLOCKNO);
                        objParams[7] = new SqlParameter("@P_EMPBUILDINGNAME", objIT.EMPBUILDINGNAME);
                        objParams[8] = new SqlParameter("@P_EMPROAD", objIT.EMPROAD);
                        objParams[9] = new SqlParameter("@P_EMPAREA", objIT.EMPAREA);
                        objParams[10] = new SqlParameter("@P_EMPCITY", objIT.EMPCITY);
                        objParams[11] = new SqlParameter("@P_EMPSTATE", objIT.EMPSTATE);
                        objParams[12] = new SqlParameter("@P_EMPPINCODE", objIT.EMPPINCODE);
                        objParams[13] = new SqlParameter("@P_EMPPHONENO", objIT.EMPPHONENO);
                        objParams[14] = new SqlParameter("@P_EMPEMAIL", objIT.EMPEMAIL);
                        objParams[15] = new SqlParameter("@P_PERBLOCKNO", objIT.PERSONBLOCKNO);
                        objParams[16] = new SqlParameter("@P_PERBUILDINGNAME", objIT.PERSONBUILDINGNAME);
                        objParams[17] = new SqlParameter("@P_PERROAD", objIT.PERSONROAD);
                        objParams[18] = new SqlParameter("@P_PERAREA", objIT.PERSONAREA);
                        objParams[19] = new SqlParameter("@P_PERCITY", objIT.PERSONCITY);
                        objParams[20] = new SqlParameter("@P_PERSTATE", objIT.PERSONSTATE);
                        objParams[21] = new SqlParameter("@P_PERPINCODE", objIT.PERSONPINCODE);
                        objParams[22] = new SqlParameter("@P_PERPHONENO", objIT.PERSONPHONENO);
                        objParams[23] = new SqlParameter("@P_PEREMAIL", objIT.PERSONEMAIL);
                        objParams[24] = new SqlParameter("@P_BANKNAME", objIT.BANKNAME);
                        objParams[25] = new SqlParameter("@P_BRANCHNAME", objIT.BRANCHNAME);
                        objParams[26] = new SqlParameter("@P_BANKPLACE", objIT.BANKPLACE);


                        if (!objIT.PRINDATE.Equals(DateTime.MinValue))
                            objParams[27] = new SqlParameter("@P_PRINDATE", objIT.PRINDATE);
                        else
                            objParams[27] = new SqlParameter("@P_PRINDATE", DBNull.Value);

                        objParams[28] = new SqlParameter("@P_RELIEFLIMIT", objIT.LIMIT);
                        objParams[29] = new SqlParameter("@P_FEMALELIMIT", objIT.FEMALELIMIT);
                        objParams[30] = new SqlParameter("@P_BONDLIMIT", objIT.BONDLIMIT);
                        objParams[31] = new SqlParameter("@P_STD_DED_MALE", objIT.STDDEDMALE);
                        objParams[32] = new SqlParameter("@P_STD_DED_FEMALE", objIT.STDDEDFEMALE);
                        objParams[33] = new SqlParameter("@P_SURCHARGE", objIT.SURCHARGE);
                        objParams[34] = new SqlParameter("@P_MORETHAN", objIT.MORETHAN);
                        objParams[35] = new SqlParameter("@P_MEDICAL_EXEMPTION", objIT.MEDICALEXEMPTION);
                        objParams[36] = new SqlParameter("@P_TAX_INC_LIMIT", objIT.TAXINC_LIMIT);
                        objParams[37] = new SqlParameter("@P_TAX_INC_UPLIMIT", objIT.TAXINCUP_LIMIT);
                        objParams[38] = new SqlParameter("@P_EDUCESS", objIT.EDUCESS);
                        objParams[39] = new SqlParameter("@P_PT", objIT.ADDITIONALPT);
                        objParams[40] = new SqlParameter("@P_BSRCODE", objIT.BSRCODE);

                        if (!objIT.FROMDATE.Equals(DateTime.MinValue))
                            objParams[41] = new SqlParameter("@P_FROMDATE", objIT.FROMDATE);
                        else
                            objParams[41] = new SqlParameter("@P_FROMDATE", DBNull.Value);

                        if (!objIT.TODATE.Equals(DateTime.MinValue))
                            objParams[42] = new SqlParameter("@P_TODATE", objIT.TODATE);
                        else
                            objParams[42] = new SqlParameter("@P_TODATE", DBNull.Value);

                        objParams[43] = new SqlParameter("@P_SHOWFORMNO16", objIT.SHOWFORMNO16);
                        objParams[44] = new SqlParameter("@P_ADD_PROPOSED_SALARY", objIT.PROPOSEDSALARY);
                        objParams[45] = new SqlParameter("@P_PREVIOUS_MONTH_SALARY", objIT.PREVIOUSSALARY);
                        objParams[46] = new SqlParameter("@P_COLLEGE_CODE", objIT.COLLEGE_CODE);
                        objParams[47] = new SqlParameter("@P_SIGNFNAME", objIT.SIGNFNAME);
                        objParams[48] = new SqlParameter("@P_HIGH_EDUCESS", objIT.HIGHEDUCESS);
                        objParams[49] = new SqlParameter("@P_ADDNPSGSFORIT", objIT.ADDNPSGROSSFORIT);
                        objParams[50] = new SqlParameter("@P_TALIMIT", objIT.TALIMIT);
                        objParams[51] = new SqlParameter("@P_TAX_REBATE", objIT.TAXREBATE);
                        objParams[52] = new SqlParameter("@P_HOUSEAMTLIMIT", objIT.HOUSEAMTLIMIT);
                        objParams[53] = new SqlParameter("@P_80CCDNPSLIMIT", objIT.CCDNPS80);
                        objParams[54] = new SqlParameter("@P_RGESSLIMIT", objIT.RGESS_CCG);

                        objParams[55] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[55].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_ITCONFIG", objParams, true);
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
                public DataTableReader GetITConfiguration()
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ITCONFIGURATION", objParams).Tables[0].CreateDataReader();

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayConfigurationController.GetITConfiguration->" + ex.ToString());
                    }
                    return dtr;

                }

                /// <summary>
                /// Procedure To Get Present Income Tax Rules
                /// Method Added By Ankit Agrawal on 13-Apr-2011
                /// </summary>
                /// <returns>Dataset of all the Defined Rules</returns>
                public DataSet GetITRules()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_IT_RULES", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayConfigurationController.GetITRules->" + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Procedure To Define A New Rule or Update An Existing Rule for new Rule ITNO is Passed as Zero
                /// and to update a particular rule its original ITNO is passed 
                /// Method Written By Ankit Agrawal on 13-Apr-2011
                /// </summary>
                /// <param name="objITRule"></param>
                /// <returns>Status as Integer</returns>
                public int AddUpdateITRule(ITRule objITRule)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_ITNO", objITRule.ITNO);
                        objParams[1] = new SqlParameter("@P_MINLIMIT", objITRule.MINLIMIT);
                        objParams[2] = new SqlParameter("@P_MAXLIMIT", objITRule.MAXLIMIT);
                        objParams[3] = new SqlParameter("@P_FIXAMT", objITRule.FIXAMT);
                        objParams[4] = new SqlParameter("@P_PER", objITRule.PERCENTAGE);
                        objParams[5] = new SqlParameter("@P_COLLEGECODE", objITRule.COLLEGE_CODE);
                        objParams[6] = new SqlParameter("@P_ITSTATUS", objITRule.STATUS);
                        objParams[7] = new SqlParameter("@P_IT_RULE_ID", objITRule.ITRULEID);
                        objParams[8] = new SqlParameter("@P_SCHEMETYPE", objITRule.SchemeType);
                        objParams[9] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_ADD_UPDATE_IT_RULE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                        {
                            if (objITRule.ITNO == 0)
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITMascontroller.AddUpdateITRule -> " + ex.ToString());
                    }
                    return retStatus;
                }


                /// <summary>
                /// Procedure To Get Deduction Heads For Income Tax
                /// </summary>
                /// <returns>Dataset</returns>

                public DataSet GetITDeductionHeads()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_IT_DEDUCTION_HEADS", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayConfigurationController.GetITConfiguration->" + ex.ToString());
                    }
                    return ds;
                }


                /// <summary>
                /// Procedure To Update Deduction Head Name and Field Name For Income Tax Deduction
                /// </summary>
                /// <param name="FNo">ID</param>
                /// <param name="PayHead">HeadName</param>
                /// <param name="DeductionHead">Field Name</param>
                /// <returns></returns>
                public int UpdateITDedutionHead(int FNo, string PayHead, string DeductionHead)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_FNO", FNo);
                        objParams[1] = new SqlParameter("@P_PAYHEAD", PayHead);
                        objParams[2] = new SqlParameter("@P_DEDHEAD", DeductionHead);
                        objParams[3] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPDATE_F16DEDHEAD", objParams, true);
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

                public int InsertITDeductionHead(int FNo, string PayHead, string DeductionHead,string ST,string CollegeCode,int Userno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_FNO", FNo);
                        objParams[1] = new SqlParameter("@P_PAYHEAD", PayHead);
                        objParams[2] = new SqlParameter("@P_DEDHEAD", DeductionHead);
                        objParams[3] = new SqlParameter("@P_ST", ST);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", CollegeCode);
                        objParams[5] = new SqlParameter("@P_USERNO", Userno);
                        objParams[6] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INSERT_F16DEDHEAD", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if(Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITMascontroller.InsertITDeductionHead -> " + ex.ToString());
                    }
                    return retStatus;
                }


                /// <summary>
                /// To get Heads for Income TAx
                /// Added by Mrunal Bansod
                /// </summary>
                /// <returns></returns>
                public DataSet GetITHeads()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_IT_HEADS", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayConfigurationController.GetITHeads->" + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// To update the Records in the table Pay_CHAPVI_HEAD
                /// </summary>
                /// <param name="CNO">ID</param>
                /// <param name="HEAD">Head Name</param>
                /// <param name="SECTION">Section</param>
                /// <param name="PAYHEAD">Payhead</param>
                /// <returns></returns>

                public int UpdateITHead(int CNO, string Head, string Section, string PayHead, string headfullname, decimal limit, bool isPercentage)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_CNO", CNO);
                        objParams[1] = new SqlParameter("@P_HEADNAME", Head);
                        objParams[2] = new SqlParameter("@P_SECTION", Section);
                        objParams[3] = new SqlParameter("@P_PAYHEAD", PayHead);
                        objParams[4] = new SqlParameter("@P_HEADNAMEFULL", headfullname);
                        objParams[5] = new SqlParameter("@P_LIMIT", limit);
                        objParams[6] = new SqlParameter("@P_ISPERCENTAGE", isPercentage);
                        objParams[7] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPDATE_PAY_CHAPVI_HEAD", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITMascontroller.UpdateITHead -> " + ex.ToString());
                    }
                    return retStatus;
                }
                //INSERT IT HEADS

                public int AddITHeadCHV(int CNO, string Head, string Section, int college_code, string PayHead, string PayHeadFullName, decimal Limit, bool IsPercentage)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];
                        objParams[0]= new SqlParameter("@P_CNO",CNO);
                        objParams[1] = new SqlParameter("@P_PAYHEAD", PayHead);
                        objParams[2] = new SqlParameter("@P_HEADNAME", Head);
                        objParams[3] = new SqlParameter("@P_SECTION", Section);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", college_code);
                        objParams[5] = new SqlParameter("@P_HEADNAMEFULL",PayHeadFullName);
                        objParams[6] = new SqlParameter("@P_LIMIT",Limit);
                        objParams[7] = new SqlParameter("@P_ISPERCENTAGE",IsPercentage);
                        objParams[8] = new SqlParameter("@P_STATUS",SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INSERT_PAY_CHAPVI_HEAD", objParams, true);
                        if(Convert.ToInt32(ret) == -99)
                        {
                           retStatus=Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if(Convert.ToInt32(ret) == 1)
                        {
                            retStatus=Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                       else  if(Convert.ToInt32(ret) == 2627)
                        {
                           retStatus=Convert.ToInt32(CustomStatus.RecordExist);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITMascontroller.AddUpdateITRule -> " + ex.ToString());
                    }
                    return retStatus;
                }

                //INSERT IT HEADS END 


                /// <summary>
                /// To Insert IT Heads In Table.
                /// </summary>
                /// <param name="Headname"></param>
                /// <param name="Section"></param>
                /// <param name="Payhead"></param>
                /// <returns></returns>
                public int AddITHeads(string Headname, string Section, string Payhead)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_HEADNAME", Headname);
                        objParams[1] = new SqlParameter("@P_SECTION", Section);
                        objParams[2] = new SqlParameter("@@P_PAYHEAD", Payhead);
                        objParams[3] = new SqlParameter("@P_CNO", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_CHAPVI_HEAD", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Masters.AddINSTALLMENTBANK-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// TO RETRIVE OTHER INCOME HEADS FROM TABLE FOR DECLARATION FORM
                /// </summary>
                /// <returns></returns>
                public DataSet GetOtherIncomeHead()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_IT_OTHERINCOME_HEAD", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayConfigurationController.GetOtherIncomeHead->" + ex.ToString());
                    }
                    return ds;
                }



                /// <summary>
                /// PROCEDURE TO ADD OR UPDATE CHALLAN ENTRY
                /// WRITTEN BY ANKIT AGRAWAL ON 14-APR-2011
                /// </summary>
                /// <param name="objChallan"></param>
                /// <returns>STATUS</returns>
                public int AddUpdateChallanEntry(ITChallanEntry objChallan)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[18];
                        objParams[0] = new SqlParameter("@P_CHIDNO", objChallan.CHIDNO);
                        objParams[1] = new SqlParameter("@P_MONYEAR", objChallan.MONYEAR);
                        objParams[2] = new SqlParameter("@P_CHDATE", objChallan.CHDATE);
                        objParams[3] = new SqlParameter("@P_ORDERNO", objChallan.SUPORDERNO);
                        objParams[4] = new SqlParameter("@P_STAFF", objChallan.STAFF);
                        objParams[5] = new SqlParameter("@P_CHNO", objChallan.CHNO);
                        objParams[6] = new SqlParameter("@P_CHQDD", objChallan.CHQDDNO);
                        objParams[7] = new SqlParameter("@P_TAXDEPO", objChallan.TAXDEPO);
                        objParams[8] = new SqlParameter("@P_SURCHARGE", objChallan.SURCHARGE);
                        objParams[9] = new SqlParameter("@P_EDUCESS", objChallan.EDUCESS);
                        objParams[10] = new SqlParameter("@P_INTEREST", objChallan.INTEREST);
                        objParams[11] = new SqlParameter("@P_OTHERS", objChallan.OTHERS);
                        objParams[12] = new SqlParameter("@P_BSRCODE", objChallan.BSRCODE);
                        objParams[13] = new SqlParameter("@P_DEDUDATE", objChallan.DEDUDATE);
                        objParams[14] = new SqlParameter("@P_DEPODATE", objChallan.DEPODATE);
                        objParams[15] = new SqlParameter("@P_COLLEGECODE", objChallan.COLLEGECODE);
                        objParams[16] = new SqlParameter("@P_COLLEGE_NO", objChallan.COLLEGENO);
                        objParams[17] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[17].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_ADD_UPDATE_IT_CHALLANENTRY", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                        {
                            if (objChallan.CHIDNO == 0)
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITMascontroller.AddUpdateChallanEntry -> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// PROCEDURE TO GET ALL THE CHALLAN ENTRIES IN THE DATABASE
                /// WRITTEN BY ANKIT AGRAWAL ON 14-APR-2011
                /// </summary>
                /// <returns>DATASET</returns>
                public DataSet GetChallanEntry(int ChIdNo, string MonYear)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_CHID", ChIdNo);
                        objParams[1] = new SqlParameter("@P_MONYEAR", MonYear);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ALL_IT_CHALLAN_ENTRY", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayConfigurationController.GetChallanEntry->" + ex.ToString());
                    }
                    return ds;
                }

                public int DeleteChalanEntry(int ChIdNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_CHIDNO", ChIdNo);
                        objParams[1] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DELETE_IT_CHALLANENTRY", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITMascontroller.DeleteChalanEntry -> " + ex.ToString());
                    }
                    return retStatus;
                }


                /// <summary>
                /// To get all Details of Chalan Entry for Chalan Transaction Entry Form.
                /// </summary>
                /// <param name="chalanNo"></param>
                /// <returns></returns>
                public DataTableReader GetChalanEntryDetail(int chalanNo)
                {
                    DataTableReader dtr = null;
                    //SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_CHALANNO", chalanNo);
                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_IT_CHALLAN_ENTRY_BY_CHALANNO", objParams).Tables[0].CreateDataReader();


                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.EmpCreateController.GetChalanEntryDetail->" + ex.ToString());
                    }
                    return dtr;
                }

                /// <summary>
                /// To get all records from PAY_ITCHALAN TABLE
                /// </summary>
                /// <param name="ChIdNo"></param>
                /// <returns></returns>
                public DataSet GetChallanTranEntry(int ChIdNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_CHID", ChIdNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_IT_CHALLANTRAN_ENTRY", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayConfigurationController.GetChallanTranEntry->" + ex.ToString());
                    }
                    return ds;
                }


                /// <summary>
                /// To get Emp Information for Edit
                /// </summary>
                /// <param name="idno"></param>
                /// <returns></returns>
                public DataTableReader GetEmpInfo(int schIdno)
                {
                    DataTableReader dtr = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCHIDNO", schIdno);
                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_IT_CHALLANTRAN_ENTRY_BY_IDNO", objParams).Tables[0].CreateDataReader();


                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.EmpCreateController.GetEmpInfo->" + ex.ToString());
                    }
                    return dtr;
                }

                /// <summary>
                /// Delete Particular Record from PAY_ITCHALANTRAN TABLE.
                /// </summary>
                /// <param name="SchIdno"></param>
                /// <returns></returns>

                public int DeleteChalanTranEntry(int SCHIDNO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SCHIDNO", SCHIDNO);
                        objParams[1] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DELETE_IT_CHALLAN_TRAN_ENTRY", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITMascontroller.DeleteChalanTranEntry -> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// Add Chalan Tran Entry into table.
                /// </summary>
                /// <param name="objITChalanTran"></param>
                /// <returns></returns>
                public int AddITChalanTranEntry(ITChalanTranEntry objITChalanTran)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_CHIDNO", objITChalanTran.CHIDNO);
                        objParams[1] = new SqlParameter("@P_IDNO", objITChalanTran.IDNO);
                        objParams[2] = new SqlParameter("@P_PAYDATE", objITChalanTran.PAYDATE);
                        objParams[3] = new SqlParameter("@P_GSAMT", objITChalanTran.GSAMT);
                        objParams[4] = new SqlParameter("@P_CHAMT", objITChalanTran.CHAMT);
                        objParams[5] = new SqlParameter("@P_CHSCHARGE", objITChalanTran.CHSCHARGE);
                        objParams[6] = new SqlParameter("@P_CHEDUCESS", objITChalanTran.CHEDUCESS);
                        objParams[7] = new SqlParameter("@P_COLLEGECODE", objITChalanTran.COLLEGECODE);
                        objParams[8] = new SqlParameter("@P_SCHIDNO", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_CHALAN_TRAN", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Masters.AddITChalanTranEntry-> " + ex.ToString());
                    }
                    return retStatus;
                }
                /// <summary>
                /// To Update the Record in the table Pay_ITChalanTran through Pay_ITChalanTranEntry Form
                /// </summary>
                /// <param name="objITChalanTran"></param>
                /// <returns></returns>
                public int UpdateChalanTranEntry(ITChalanTranEntry objITChalanTran)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        //objParams[0] = new SqlParameter("@P_CHIDNO", objITChalanTran.CHIDNO);
                        objParams[0] = new SqlParameter("@P_SCHIDNO", objITChalanTran.SCHIDNO);
                        objParams[1] = new SqlParameter("@P_IDNO", objITChalanTran.IDNO);
                        objParams[2] = new SqlParameter("@P_GSAMT", objITChalanTran.GSAMT);
                        objParams[3] = new SqlParameter("@P_CHAMT", objITChalanTran.CHAMT);
                        objParams[4] = new SqlParameter("@P_CHSCHARGE", objITChalanTran.CHSCHARGE);
                        objParams[5] = new SqlParameter("@P_CHEDUCESS", objITChalanTran.CHEDUCESS);
                        objParams[6] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPDATE_PAY_ITCHALAN_TRAN_ENTRY", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITMascontroller.UpdateChalanTranEntry -> " + ex.ToString());
                    }
                    return retStatus;
                }
                /// <summary>
                /// To get Chalan No.on Page Pay_ITChalanTranEntry
                /// </summary>
                /// <returns></returns>
                public DataSet GetChallaNo()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_SP_GET_CHALANNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayController.GetChallaNo-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// To Get all MonthYear fro ITChalan in between dates.
                /// </summary>
                /// <param name="fromDate"></param>
                /// <param name="todate"></param>
                /// <returns></returns>
                public DataSet GetAllMonthYear(string fromDate, string todate)
                {

                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_FROM_DATE", fromDate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", todate);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_ITCHALANTRAN_GET_MONTH", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayController.GetAllPayHead-> " + ex.ToString());
                    }
                    return ds;
                }



                /// <summary>
                /// To get All Months on Page Pay_ITChalanTranEntry
                /// </summary>
                /// <returns></returns>
                public DataSet GetMonth()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_SP_GET_MON", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayController.GetMonth-> " + ex.ToString());
                    }
                    return ds;
                }


                public string GetChalanNoForReport(string ChalanNo, int index)
                {
                    string temp = string.Empty;
                    if (ChalanNo != "0")
                    {
                        temp = ChalanNo + "$";
                    }
                    else
                    {
                        temp = ChalanNo + "$";
                    }
                    return temp;
                }

                public string GetMonthForReport(string Month, int index)
                {
                    string temp = string.Empty;
                    if (Month != "0")
                    {
                        temp = Month + "$";
                    }
                    else
                    {
                        temp = Month + "$";
                    }
                    return temp;
                }

                /// <summary>
                /// TO UPDATE IT ACKNOWLEDGE
                /// </summary>
                /// <param name="objIT"></param>
                /// <returns></returns>
                public int UpdateITAcknowledge(ITConfiguration objIT)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_Q1", objIT.Q1);
                        objParams[1] = new SqlParameter("@P_Q2", objIT.Q2);
                        objParams[2] = new SqlParameter("@P_Q3", objIT.Q3);
                        objParams[3] = new SqlParameter("@P_Q4", objIT.Q4);
                        objParams[4] = new SqlParameter("@P_Q5", objIT.Q5);
                        objParams[5] = new SqlParameter("@P_ACK1", objIT.ACK1);
                        objParams[6] = new SqlParameter("@P_ACK2", objIT.ACK2);
                        objParams[7] = new SqlParameter("@P_ACK3", objIT.ACK3);
                        objParams[8] = new SqlParameter("@P_ACK4", objIT.ACK4);
                        objParams[9] = new SqlParameter("@P_ACK5", objIT.ACK5);
                        objParams[10] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_ITACKNOWLEDGE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITMascontroller.UpdateITAcknowledge -> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// TO GET DATA FROM REFIT TABLE FOR PAY_ACKNOWLEDGE.ASPX FORM
                /// </summary>
                /// <returns></returns>
                public DataTableReader GetITAcknowledge()
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ITACKNOWLEDGE", objParams).Tables[0].CreateDataReader();

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayConfigurationController.GetITAcknowledge->" + ex.ToString());
                    }
                    return dtr;

                }
                /// <summary>
                /// to retrive tax deposited of employees for particular month
                /// </summary>
                /// <param name="ChIdNo"></param>
                /// <param name="MonYear"></param>
                /// <returns></returns>
                public DataSet GetTaxDeposited(string payhead, string MonYear, int stno, int collegeNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_PAYHEAD", payhead);
                        objParams[1] = new SqlParameter("@P_MONYEAR", MonYear);
                        objParams[2] = new SqlParameter("@P_STNO", stno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_TAX_DEPOSITED_IT_CHALLAN_ENTRY", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITMASCONTROOLER.GetTaxDeposited->" + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// To get all Details of Chalan Entry for Chalan Transaction Entry Form.
                /// </summary>
                /// <param name="chalanNo"></param>
                /// <returns></returns>
                public DataSet GetEmployeeDetailsForChalanEntries(int chalanNo)
                {
                    DataSet ds = null;
                    //SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_CHALANNO", chalanNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_IT_CHALLAN_ENTRIES", objParams);


                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.EmpCreateController.GetChalanEntryDetail->" + ex.ToString());
                    }
                    return ds;
                }


                #region IT RULE MASTER
                public DataSet GetITRulesName()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_IT_RULES_NAMES", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITMasController.GetITRulesName->" + ex.ToString());
                    }
                    return ds;
                }

                public int InsertITRuleName(ITRule objITRule)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@IT_RULE_ID", objITRule.ITRULEID);
                        objParams[1] = new SqlParameter("@IT_RULE_NAME", objITRule.ITRULENAME);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", objITRule.COLLEGECODE);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", objITRule.COLLEGENO);
                        objParams[4] = new SqlParameter("@P_IsActive", objITRule.IsActive);
                        objParams[5] = new SqlParameter("@P_SchemeType", objITRule.SchemeType);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PAYROLL_INSERT_IT_RULE_NAME", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else if (Convert.ToInt32(ret) == 2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else if (Convert.ToInt32(ret) == 2627)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITMascontroller.InsertITRuleName -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateITRuleName(ITRule objITRule)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@IT_RULE_ID", objITRule.ITRULEID);
                        objParams[1] = new SqlParameter("@IT_RULE_NAME", objITRule.ITRULENAME);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", objITRule.COLLEGECODE);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", objITRule.COLLEGENO);
                        objParams[4] = new SqlParameter("@P_IsActive", objITRule.IsActive);
                        objParams[5] = new SqlParameter("@P_SchemeType", objITRule.SchemeType);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PAYROLL_UPDATE_IT_RULE_NAME", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else if (Convert.ToInt32(ret) == 2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else if (Convert.ToInt32(ret) == 2627)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITMascontroller.UpdateITRuleName -> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion





            }
        }
    }
}
