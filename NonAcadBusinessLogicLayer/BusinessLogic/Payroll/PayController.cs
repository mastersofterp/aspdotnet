//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS LOGIC FILE [PAY ROLL]                                  
// CREATION DATE : 01-MAY-2009                                                        
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
            public class PayController
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                #region AppointMent
                /// <summary>
                /// This method adds a new record in the Appoint Table
                /// </summary>
                /// <param name="appoint,columnvalues,college_code"></param>
                /// <returns>Integer CustomStatus - RecordSaved or Error</returns>
                public int AddAppointment(string appoint, string columnvalues, string college_code)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_APPOINT", appoint);
                        objParams[1] = new SqlParameter("@P_COLUMNVALUES", columnvalues);
                        objParams[2] = new SqlParameter("@P_COLLEGECODE", college_code);
                        objParams[3] = new SqlParameter("@P_ID", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SP_INS_APPOINT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.AddAppointment-> " + ex.ToString());
                    }
                    return retStatus;
                }
                /// <summary>
                /// This method update a record in the Appoint Table
                /// </summary>
                /// <param name="appointno,columnames"></param>
                /// <returns>Integer CustomStatus - RecordSaved or Error</returns>
                public int UpdateAppoint(int appointno, string columnnames)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Update complaint type
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ID", appointno);
                        objParams[1] = new SqlParameter("@P_COLUMNNAMES", columnnames);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SP_UPD_APPOINT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.UpdateAppoint-> " + ex.ToString());
                    }

                    return retStatus;
                }
                /// <summary>
                /// This method returns the row depends up on appointno
                /// </summary>
                /// <param name="appointno"></param>
                /// <returns>returns the row depends up on appointno into datareader </returns>
                public SqlDataReader GetAppointno(int appointno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_APPOINTNO", appointno);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_PAY_SP_RET_APPOINT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.GetAppointno-> " + ex.ToString());
                    }
                    return dr;
                }
                /// <summary>
                /// This method returns the records of the appoint table  
                /// </summary>                
                /// <returns>returns the records of the appoint table into dataset</returns>                                
                public DataSet GetAllAppoint()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_SP_ALL_APPOINT", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.GetAllAppoint-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #region PayHead

                /// <summary>
                /// This method returns all records of payhead
                /// </summary>                
                /// <returns>returns all records of payhead into dataset</returns>
                /// 
                public DataSet GetAllPayHead()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_SP_ALL_PAYHEAD", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.GetAllPayHead-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetIdHead(int idHead)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDHEAD", idHead);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_ALL_IDHEADS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.GetIdHead-> " + ex.ToString());
                    }
                    finally
                    {


                        ds.Dispose();

                    }
                    return ds;

                }

                public DataSet GetRetPayHead(int srNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SRNO", srNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_RET_PAYHEADS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.GetIdHead-> " + ex.ToString());
                    }
                    finally
                    {


                        ds.Dispose();

                    }
                    return ds;
                }

                public int UpdatePayHead(Payroll objPayHead)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SRNO", objPayHead.Srno);
                        objParams[1] = new SqlParameter("@P_PAYSHORT", objPayHead.PayShort);
                        objParams[2] = new SqlParameter("@P_PAYFULL", objPayHead.PayFull);
                        objParams[3] = new SqlParameter("@P_TYPE", objPayHead.Type);
                        objParams[4] = new SqlParameter("@P_CAL_ON", objPayHead.CalOn);
                        objParams[5] = new SqlParameter("@P_FORMULA", objPayHead.Formula);
                        objParams[6] = new SqlParameter("@P_Isclearamount",objPayHead.Isclearamount);
                        // objParams[6] = new SqlParameter("@P_PAYSHORT_KANNADA", objPayHead.PAYSHORT_KANNADA);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_PAYHEAD", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PayController.ComplaintController.UpdatePayHead-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion

                #region Scale
                /// <summary>
                /// This method returns all records of Payroll_Scale
                /// </summary>                
                /// <returns>returns all records of Payroll_Scale into dataset</returns>
                public DataSet GetAllScale(string payrule)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PAYRULE", payrule);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_ALL_SCALE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.GetAllScale-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetScale(int scaleNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCALENO", scaleNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_RET_SCALE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.GetScale-> " + ex.ToString());
                    }
                    finally
                    {


                        ds.Dispose();

                    }
                    return ds;

                }

                public int AddScale(Payroll objScale)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[28];
                        objParams[0] = new SqlParameter("@P_B1", objScale.B1);
                        objParams[1] = new SqlParameter("@P_I1", objScale.I1);
                        objParams[2] = new SqlParameter("@P_B2", objScale.B2);
                        objParams[3] = new SqlParameter("@P_I2", objScale.I2);
                        objParams[4] = new SqlParameter("@P_B3", objScale.B3);
                        objParams[5] = new SqlParameter("@P_I3", objScale.I3);
                        objParams[6] = new SqlParameter("@P_B4", objScale.B4);
                        objParams[7] = new SqlParameter("@P_I4", objScale.I4);
                        objParams[8] = new SqlParameter("@P_B5", objScale.B5);
                        objParams[9] = new SqlParameter("@P_I5", objScale.I5);
                        objParams[10] = new SqlParameter("@P_B6", objScale.B6);
                        objParams[11] = new SqlParameter("@P_I6", objScale.I6);
                        objParams[12] = new SqlParameter("@P_B7", objScale.B7);
                        objParams[13] = new SqlParameter("@P_I7", objScale.I7);
                        objParams[14] = new SqlParameter("@P_B8", objScale.B8);
                        objParams[15] = new SqlParameter("@P_SCALE", objScale.Scale);
                        objParams[16] = new SqlParameter("@P_SCALERANGE", objScale.ScaleRange);
                        objParams[17] = new SqlParameter("@P_GRADE", objScale.GradePay);
                        objParams[18] = new SqlParameter("@P_RULENO", objScale.RuleNo);
                        objParams[19] = new SqlParameter("@P_COLLEGE_CODE", objScale.CollegeCode);
                        objParams[20] = new SqlParameter("@P_ShortScaleName", objScale.PAYSHORTNAME);
                        objParams[21] = new SqlParameter("@P_I8", objScale.I8);
                        objParams[22] = new SqlParameter("@P_B9", objScale.B9);
                        objParams[23] = new SqlParameter("@P_I9", objScale.I9);
                        objParams[24] = new SqlParameter("@P_B10", objScale.B10);
                        objParams[25] = new SqlParameter("@P_I10", objScale.I10);
                        objParams[26] = new SqlParameter("@P_B11", objScale.B11);
                        objParams[27] = new SqlParameter("@P_SCALENO ", SqlDbType.Int);
                        objParams[27].Direction = ParameterDirection.Output;
                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SCALE", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SCALE_BK", objParams, true);
                        if (ret != null)
                        {
                            if (Convert.ToInt32(ret.ToString()) == 1)
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            }
                            if (ret.ToString().Equals("-99"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            }
                            if (Convert.ToInt32(ret.ToString()) == 2627)
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                            }
                            if (Convert.ToInt32(ret.ToString()) == 2)
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PayController.ComplaintController.AddScale-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateScale(Payroll objScale)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[29];
                        objParams[0] = new SqlParameter("@P_B1", objScale.B1);
                        objParams[1] = new SqlParameter("@P_I1", objScale.I1);
                        objParams[2] = new SqlParameter("@P_B2", objScale.B2);
                        objParams[3] = new SqlParameter("@P_I2", objScale.I2);
                        objParams[4] = new SqlParameter("@P_B3", objScale.B3);
                        objParams[5] = new SqlParameter("@P_I3", objScale.I3);
                        objParams[6] = new SqlParameter("@P_B4", objScale.B4);
                        objParams[7] = new SqlParameter("@P_I4", objScale.I4);
                        objParams[8] = new SqlParameter("@P_B5", objScale.B5);
                        objParams[9] = new SqlParameter("@P_I5", objScale.I5);
                        objParams[10] = new SqlParameter("@P_B6", objScale.B6);
                        objParams[11] = new SqlParameter("@P_I6", objScale.I6);
                        objParams[12] = new SqlParameter("@P_B7", objScale.B7);
                        objParams[13] = new SqlParameter("@P_I7", objScale.I7);
                        objParams[14] = new SqlParameter("@P_B8", objScale.B8);
                        objParams[15] = new SqlParameter("@P_SCALERANGE", objScale.ScaleRange);
                        objParams[16] = new SqlParameter("@P_GRADE", objScale.GradePay);
                        objParams[17] = new SqlParameter("@P_RULENO", objScale.RuleNo);
                        objParams[18] = new SqlParameter("@P_COLLEGE_CODE", objScale.CollegeCode);
                        objParams[19] = new SqlParameter("@P_SCALENO ", objScale.ScaleNo);
                        objParams[20] = new SqlParameter("@P_SCALE", objScale.Scale);
                        objParams[21] = new SqlParameter("@P_ShortScaleName", objScale.PAYSHORTNAME);
                        objParams[22] = new SqlParameter("@P_I8", objScale.I8);
                        objParams[23] = new SqlParameter("@P_B9", objScale.B9);
                        objParams[24] = new SqlParameter("@P_I9", objScale.I9);
                        objParams[25] = new SqlParameter("@P_B10", objScale.B10);
                        objParams[26] = new SqlParameter("@P_I10", objScale.I10);
                        objParams[27] = new SqlParameter("@P_B11", objScale.B11);
                        objParams[28] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[28].Direction = ParameterDirection.Output;
                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SCALE", objParams, false) != null)
                        //   retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                       // object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SCALE_BK", objParams, true);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SCALE", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                            }
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PayController.ComplaintController.UpdateScale-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion

                #region AUTHORIT MASTER

                public DataSet AllAuthorityName()
                {

                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_MASTERS_SP_PAYROLL_NODUES_AUTHORITY_NAME", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Masters.AllQuarters-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddAuthorityName(string Auhtoname, int Ua_no)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New Quarter
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_AUTHORITY_NAME", Auhtoname);
                        objParams[1] = new SqlParameter("@P_AUTHO_UA_NO", Ua_no);

                        objParams[2] = new SqlParameter("@P_AUTHO_ID", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_MASTERS_SP_INS_PAYROLL_NODUES_AUTHORITY_NAME", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Masters.AddQuarter-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateAuthorityName(int AuthoID, string Auhtoname, int Ua_No)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Update Quarter
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_AUTHO_ID", AuthoID);
                        objParams[1] = new SqlParameter("@P_AUTHORITY_NAME", Auhtoname);
                        objParams[2] = new SqlParameter("@P_AUTHO_UA_NO", Ua_No);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_MASTERS_SP_UPD_PAYROLL_NODUES_AUTHORITY_NAME", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Masters.UpdateQuarter-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion


                #region AUTHORIT TYPE

                public DataSet AllAuthorityType()
                {

                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_MASTERS_SP_PAYROLL_NODUES_AUTHORITY_TYPE", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Masters.AllQuarters-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddAuthorityType(string Auhtoname)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New Quarter
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_AUTHORITY_TYP_NAME", Auhtoname);

                        objParams[1] = new SqlParameter("@P_AUTHO_TYP_ID", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_MASTERS_SP_INS_PAYROLL_NODUES_AUTHORITY_TYPE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Masters.AddQuarter-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateAuthorityType(int AuthoID, string Auhtoname)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Update Quarter
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_AUTHO_TYP_ID", AuthoID);
                        objParams[1] = new SqlParameter("@P_AUTHORITY_TYP_NAME", Auhtoname);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_MASTERS_SP_UPD_PAYROLL_NODUES_AUTHORITY_TYPE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Masters.UpdateQuarter-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion


                #region Pay Rule
                //Pay Rule
                public int AddPayRule(Payroll objRule)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];

                        objParams[0] = new SqlParameter("@P_PAYRULE", objRule.Payrule);
                        objParams[1] = new SqlParameter("@P_RULENAME", objRule.RULENAME);
                        objParams[2] = new SqlParameter("@P_IsR7", objRule.IsR7);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", objRule.COLLEGE_CODE);
                        objParams[4] = new SqlParameter("@ACTIVESTATUS", objRule.STATUS);
                        
                        objParams[5] = new SqlParameter("@P_RULENO", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_RULE_INSERT", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PayController.AddPayRule->" + ex.ToString());
                    }
                    return retstatus;
                }

                //To modify existing Status
                public int UpdatePayRule(Payroll objRule)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_RULENO", objRule.RuleNo);
                        objParams[1] = new SqlParameter("@P_PAYRULE", objRule.Payrule);
                        objParams[2] = new SqlParameter("@P_RULENAME", objRule.RULENAME);
                        objParams[3] = new SqlParameter("@P_IsR7", objRule.IsR7);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", objRule.COLLEGE_CODE);
                        objParams[5] = new SqlParameter("@ACTIVESTATUS", objRule.STATUS);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_RULE_UPDATE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateHolidayType->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetPayRule(int Ruleno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_RULENO", Ruleno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_SP_GET_RULE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PayController.GetPayRule-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAllPayRule()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_RULL_GET_BYALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PayController.GetPayRule->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                #endregion

                public DataSet GetAllCalPay(string payrule, int collegeNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_PAYRULE", payrule);
                        objParams[1] = new SqlParameter("@P_COLLEGENO", collegeNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_ALL_CALPAY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.GetAllCalPay-> " + ex.ToString());
                    }
                    finally
                    {


                        ds.Dispose();

                    }
                    return ds;
                }

                public int AddCalPay(Payroll objCalPay)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_PAYHEAD", objCalPay.Payhead);
                        objParams[1] = new SqlParameter("@P_PAYRULE", objCalPay.Payrule);
                        objParams[2] = new SqlParameter("@P_BMIN", objCalPay.Bmin);
                        objParams[3] = new SqlParameter("@P_BMAX ", objCalPay.Bmax);
                        objParams[4] = new SqlParameter("@P_PER ", objCalPay.Per);
                        objParams[5] = new SqlParameter("@P_MIN", objCalPay.Min);
                        objParams[6] = new SqlParameter("@P_MAX", objCalPay.Max);
                        objParams[7] = new SqlParameter("@P_FIX", objCalPay.Fix);
                        objParams[8] = new SqlParameter("@P_FDT", objCalPay.Fdt);
                        objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objCalPay.CollegeCode);
                        objParams[10] = new SqlParameter("@P_COLLEGENO", objCalPay.COLLEGENO);
                        objParams[11] = new SqlParameter("@P_CALNO ", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        object retval = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_CALPAY_BK", objParams, true);
                        if (Convert.ToInt32(retval) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(retval) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        ////else
                        ////    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        ////if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_CALPAY", objParams, false) != null)
                        ////    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_CALPAY_BK", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PayController.ComplaintController.AddCalPay-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateCalPay(Payroll objCalPay)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_PAYHEAD", objCalPay.Payhead);
                        objParams[1] = new SqlParameter("@P_PAYRULE", objCalPay.Payrule);
                        objParams[2] = new SqlParameter("@P_BMIN", objCalPay.Bmin);
                        objParams[3] = new SqlParameter("@P_BMAX ", objCalPay.Bmax);
                        objParams[4] = new SqlParameter("@P_PER ", objCalPay.Per);
                        objParams[5] = new SqlParameter("@P_MIN", objCalPay.Min);
                        objParams[6] = new SqlParameter("@P_MAX", objCalPay.Max);
                        objParams[7] = new SqlParameter("@P_FIX", objCalPay.Fix);
                        objParams[8] = new SqlParameter("@P_FDT", objCalPay.Fdt);
                        objParams[9] = new SqlParameter("@P_CALNO ", objCalPay.Calno);
                        objParams[10] = new SqlParameter("@P_COLLEGENO ", objCalPay.COLLEGENO);
                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_CALPAY", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_CALPAY_BK", objParams, false);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("2"))  // updated on 20-06-2022
                            {
                                Convert.ToInt32(CustomStatus.RecordExist);
                            }
                            else if(ret.ToString().Equals("1"))
                            {
                                Convert.ToInt32(CustomStatus.RecordSaved);
                            }
                        }
                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_CALPAY_BK", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PayController.ComplaintController.UpdateCalPay-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeleteCalPay(int calNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_CALNO ", calNo);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DEL_CALPAY", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PayController.ComplaintController.DeleteCalPay-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetCalPay(int calNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_CALNO", calNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_RET_CALPAY", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.GetCalPay-> " + ex.ToString());
                    }
                    finally
                    {


                        ds.Dispose();

                    }
                    return ds;

                }

                public int UpdateEmpmasPaymasSeqNo(int sqNo, int idNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Update complaint type
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SEQNO", sqNo);
                        objParams[1] = new SqlParameter("@P_IDNO", idNo);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SEQNO_EMPMAS_PAYMAS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.UpdateEmpmasPaymasSeqNo-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetEmpByStaffno(int staffNo, string orderby, int dept, int collegeno, int EmpTypeNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_STAFFNO", staffNo);
                        objParams[1] = new SqlParameter("@P_ORDERBY", orderby);
                        objParams[2] = new SqlParameter("@P_DEPTNO", dept);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        objParams[4] = new SqlParameter("@P_EMPTYPE_NO", EmpTypeNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_SP_ALL_EMP_BY_STAFFNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.GetEmpByStaffno-> " + ex.ToString());
                    }
                    finally
                    {


                        ds.Dispose();

                    }
                    return ds;
                }


                public DataSet GetIncrementEmployees(int staffNo, int incMonth, int collegeNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_STAFFNO ", staffNo);
                        objParams[1] = new SqlParameter("@P_MONTH", incMonth);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_RET_INCREMENT_EMPLOYEES", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.GetIncrementEmployees-> " + ex.ToString());
                    }

                    return ds;
                }

                public int UpdateIncerment(int idNo, int oldBasic, int newBasic, char incYesNo, DateTime doi)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Update complaint type
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        objParams[1] = new SqlParameter("@P_OBASIC ", oldBasic);
                        objParams[2] = new SqlParameter("@P_BASIC ", newBasic);
                        objParams[3] = new SqlParameter("@P_INCYN  ", incYesNo);
                        objParams[4] = new SqlParameter("@P_DOI   ", doi);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_INCREMENT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.UpdateIncerment-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetAllSubPayHead()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_ALL_SUBPAYHEAD", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.AllSubpayHead-> " + ex.ToString());
                    }
                    finally
                    {


                        ds.Dispose();

                    }
                    return ds;
                }

                public int AddSubPayHead(string payHead, string shortName, string fullName, int bookEntry, string collegeCode, bool mainHead)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_PAYHEAD", payHead);
                        objParams[1] = new SqlParameter("@P_SHORTNAME", shortName);
                        objParams[2] = new SqlParameter("@P_FULLNAME", fullName);
                        objParams[3] = new SqlParameter("@P_BOOKADJ ", bookEntry);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE ", collegeCode);
                        objParams[5] = new SqlParameter("@P_MAINHEAD ", mainHead);
                        objParams[6] = new SqlParameter("@P_SUBHEADNO", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_SUBPAYHEAD", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PayController.AddSupayHead-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateSubPayHead(int SubHeadNo, string payHead, string shortName, string fullName, int bookEntry, bool mainHead)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_PAYHEAD", payHead);
                        objParams[1] = new SqlParameter("@P_SHORTNAME", shortName);
                        objParams[2] = new SqlParameter("@P_FULLNAME", fullName);
                        objParams[3] = new SqlParameter("@P_BOOKADJ ", bookEntry);
                        objParams[4] = new SqlParameter("@P_MAINHEAD ", mainHead);
                        objParams[5] = new SqlParameter("@P_SUBHEADNO ", SubHeadNo);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SUBPAYHEAD", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PayController.UpdateSubPayHead-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddInstallMent(Payroll objscale)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[31];
                        objParams[0] = new SqlParameter("@P_IDNO", objscale.IDNO);
                        objParams[1] = new SqlParameter("@P_PAYHEAD", objscale.PAYHEAD);
                        objParams[2] = new SqlParameter("@P_CODE", objscale.CODE);
                        objParams[3] = new SqlParameter("@P_INSTALNO", objscale.INSTALNO);
                        objParams[4] = new SqlParameter("@P_MONAMT", objscale.MONAMT);
                        objParams[5] = new SqlParameter("@P_TOTAMT", objscale.TOTAMT);
                        objParams[6] = new SqlParameter("@P_BAL_AMT", objscale.BAL_AMT);
                        objParams[7] = new SqlParameter("@P_STOP", objscale.STOP);

                        if (!(objscale.START_DT == null))
                            objParams[8] = new SqlParameter("@P_START_DT", objscale.START_DT);
                        else
                            objParams[8] = new SqlParameter("@P_START_DT", DBNull.Value);

                        objParams[9] = new SqlParameter("@P_EXPDT", objscale.EXPDT);
                        objParams[10] = new SqlParameter("@P_PAIDNO", objscale.PAIDNO);
                        objParams[11] = new SqlParameter("@P_MON", DBNull.Value);
                        objParams[12] = new SqlParameter("@P_NEW", objscale.NEW);
                        objParams[13] = new SqlParameter("@P_ACCNO", objscale.ACCNO);
                        objParams[14] = new SqlParameter("@P_REF_NO", DBNull.Value);
                        objParams[15] = new SqlParameter("@P_DESP_NO", DBNull.Value);
                        objParams[16] = new SqlParameter("@P_DESP_DT", DBNull.Value);
                        objParams[17] = new SqlParameter("@P_DEFA_AMT", objscale.DEFA_AMT);
                        objParams[18] = new SqlParameter("@P_PRO_AMT", DBNull.Value);

                        if (!(objscale.SUBHEADNO == null))
                            objParams[19] = new SqlParameter("@P_SUBHEADNO", objscale.SUBHEADNO);
                        else
                            objParams[19] = new SqlParameter("@P_SUBHEADNO", DBNull.Value);

                        objParams[20] = new SqlParameter("@P_STOP1", DBNull.Value);
                        objParams[21] = new SqlParameter("@P_REGULAR", objscale.REGULAR);
                        objParams[22] = new SqlParameter("@P_LTNO", DBNull.Value);
                        objParams[23] = new SqlParameter("@P_REMARK", objscale.REMARK);
                        objParams[24] = new SqlParameter("@P_COLLEGE_CODE", objscale.COLLEGE_CODE);
                        objParams[25] = new SqlParameter("@P_BANKNO", objscale.BANKNO);
                        objParams[26] = new SqlParameter("@P_BANKCITYNO", objscale.BANKCITYNO);
                        objParams[27] = new SqlParameter("@P_DRAWN_DATE", objscale.DRAWN_DATE);
                        objParams[28] = new SqlParameter("@P_COLLEGENO", objscale.COLLEGENO);
                        objParams[29] = new SqlParameter("@P_STAFFNO", objscale.STAFFNO);

                        objParams[30] = new SqlParameter("@P_INO", SqlDbType.Int);
                        objParams[30].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_MONTHLY_INSTALLMENT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PayController.AddInstallMent-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public int AddMedicalInstallMent(Payroll objscale)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[32];
                        objParams[0] = new SqlParameter("@P_IDNO", objscale.IDNO);
                        objParams[1] = new SqlParameter("@P_PAYHEAD", objscale.PAYHEAD);
                        objParams[2] = new SqlParameter("@P_CODE", objscale.CODE);
                        objParams[3] = new SqlParameter("@P_INSTALNO", objscale.INSTALNO);
                        objParams[4] = new SqlParameter("@P_MONAMT", objscale.MONAMT);
                        objParams[5] = new SqlParameter("@P_TOTAMT", objscale.TOTAMT);
                        objParams[6] = new SqlParameter("@P_BAL_AMT", objscale.BAL_AMT);
                        objParams[7] = new SqlParameter("@P_STOP", objscale.STOP);

                        if (!(objscale.START_DT == null))
                            objParams[8] = new SqlParameter("@P_START_DT", objscale.START_DT);
                        else
                            objParams[8] = new SqlParameter("@P_START_DT", DBNull.Value);

                        objParams[9] = new SqlParameter("@P_EXPDT", objscale.EXPDT);
                        objParams[10] = new SqlParameter("@P_PAIDNO", objscale.PAIDNO);
                        objParams[11] = new SqlParameter("@P_MON", DBNull.Value);
                        objParams[12] = new SqlParameter("@P_NEW", objscale.NEW);
                        objParams[13] = new SqlParameter("@P_ACCNO", objscale.ACCNO);
                        objParams[14] = new SqlParameter("@P_REF_NO", DBNull.Value);
                        objParams[15] = new SqlParameter("@P_DESP_NO", DBNull.Value);
                        objParams[16] = new SqlParameter("@P_DESP_DT", DBNull.Value);
                        objParams[17] = new SqlParameter("@P_DEFA_AMT", objscale.DEFA_AMT);
                        objParams[18] = new SqlParameter("@P_PRO_AMT", DBNull.Value);

                        if (!(objscale.SUBHEADNO == null))
                            objParams[19] = new SqlParameter("@P_SUBHEADNO", objscale.SUBHEADNO);
                        else
                            objParams[19] = new SqlParameter("@P_SUBHEADNO", DBNull.Value);

                        objParams[20] = new SqlParameter("@P_STOP1", DBNull.Value);
                        objParams[21] = new SqlParameter("@P_REGULAR", objscale.REGULAR);
                        objParams[22] = new SqlParameter("@P_LTNO", DBNull.Value);
                        objParams[23] = new SqlParameter("@P_REMARK", objscale.REMARK);
                        objParams[24] = new SqlParameter("@P_COLLEGE_CODE", objscale.COLLEGE_CODE);
                        objParams[25] = new SqlParameter("@P_BANKNO", objscale.BANKNO);
                        objParams[26] = new SqlParameter("@P_BANKCITYNO", objscale.BANKCITYNO);
                        objParams[27] = new SqlParameter("@P_DRAWN_DATE", objscale.DRAWN_DATE);
                        objParams[28] = new SqlParameter("@P_COLLEGENO", objscale.COLLEGENO);
                        objParams[29] = new SqlParameter("@P_STAFFNO", objscale.STAFFNO);
                        objParams[30] = new SqlParameter("@P_EMPR_CONT", objscale.EMP_CONAMT);
                        objParams[31] = new SqlParameter("@P_INO", SqlDbType.Int);
                        objParams[31].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_MONTHLY_INSTALLMENT_MEDICAL", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PayController.AddInstallMent-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public int UpdateInstallMent(Payroll objscale)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[28];
                        objParams[0] = new SqlParameter("@P_IDNO", objscale.IDNO);
                        objParams[1] = new SqlParameter("@P_PAYHEAD", objscale.PAYHEAD);
                        objParams[2] = new SqlParameter("@P_CODE", objscale.CODE);
                        objParams[3] = new SqlParameter("@P_INSTALNO", objscale.INSTALNO);
                        objParams[4] = new SqlParameter("@P_MONAMT", objscale.MONAMT);
                        objParams[5] = new SqlParameter("@P_TOTAMT", objscale.TOTAMT);
                        objParams[6] = new SqlParameter("@P_BAL_AMT", objscale.BAL_AMT);
                        objParams[7] = new SqlParameter("@P_STOP", objscale.STOP);
                        objParams[8] = new SqlParameter("@P_START_DT", objscale.START_DT);
                        objParams[9] = new SqlParameter("@P_EXPDT", objscale.EXPDT);
                        objParams[10] = new SqlParameter("@P_PAIDNO", objscale.PAIDNO);
                        objParams[11] = new SqlParameter("@P_MON", objscale.MON);
                        objParams[12] = new SqlParameter("@ P_NEW ", objscale.NEW);
                        objParams[13] = new SqlParameter("@ P_ACCNO", objscale.ACCNO);
                        objParams[14] = new SqlParameter("@P_REF_NO", objscale.REF_NO);
                        objParams[15] = new SqlParameter("@P_DESP_NO", objscale.DESP_NO);
                        objParams[16] = new SqlParameter("@P_DESP_DT", objscale.DESP_DT);
                        objParams[17] = new SqlParameter("@P_DEFA_AMT", objscale.DEFA_AMT);
                        objParams[18] = new SqlParameter("@P_PRO_AMT", objscale.PRO_AMT);
                        objParams[19] = new SqlParameter("@P_SUBHEADNO", objscale.SUBHEADNO);
                        objParams[20] = new SqlParameter("@P_STOP1", objscale.STOP1);
                        objParams[21] = new SqlParameter("@ P_REGULAR", objscale.REGULAR);
                        objParams[22] = new SqlParameter("@P_LTNO", objscale.LTNO);
                        objParams[23] = new SqlParameter("@P_REMARK", objscale.REMARK);
                        objParams[25] = new SqlParameter("@P_BANKNO", objscale.BANKNO);
                        objParams[26] = new SqlParameter("@P_DRAWN_DATE", objscale.DRAWN_DATE);
                        objParams[27] = new SqlParameter("@P_INO", objscale.INO);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_MONTHLY_INSTALLMENT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PayController.UpdateInstallMent-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeleteInstallMent(int iNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_INO", iNo);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DTL_MONTHLY_INSTALLMENT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PayController.DeleteInstallMent-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetInstallMent(int ino)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_INO", ino);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_RET_MONTHLY_INSTALLMENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.GetInstallMent-> " + ex.ToString());
                    }
                    finally
                    {

                        ds.Dispose();

                    }
                    return ds;
                }

                public DataSet GetStopInstallMent(int idno, int stop, string instType)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SHOW_STOP_INSTALLMENT", stop);
                        objParams[2] = new SqlParameter("@P_INST_TYPE", instType);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_SHOW_STOP_INSTALLMENT_EMP", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.GetStopInstallMent-> " + ex.ToString());
                    }
                    finally
                    {

                        ds.Dispose();

                    }
                    return ds;
                }

                public DataSet GetAllInstallMent(int idno, int stop, int CollNo, int Satffno, DateTime FromDate, DateTime ToDate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[6];

                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SHOW_STOP_INSTALLMENT", stop);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", CollNo);
                        objParams[3] = new SqlParameter("@P_STAFFNO", Satffno);
                        objParams[4] = new SqlParameter("@P_FROM_DATE", FromDate);
                        objParams[5] = new SqlParameter("@P_TO_DATE", ToDate);

            


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_MEDICAL_SHOW_INSTALLMENT_EMP", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.GetStopInstallMent-> " + ex.ToString());
                    }
                    finally
                    {

                        ds.Dispose();

                    }
                    return ds;
                }

                public DataSet GetIncreEmp(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_INST_EMPINFO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.GetInstallMent-> " + ex.ToString());
                    }
                    finally
                    {

                        ds.Dispose();

                    }
                    return ds;
                }

                #region Payroll_Daysal

                public int AddDaysal(int staffNo, DateTime monDate, string monYear, decimal days, string payHead, string collegeCode, int collegeNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        //AddDaysal
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {    
                                 new SqlParameter("@P_STAFFNO",staffNo),
                                 new SqlParameter("@P_MONDATE",monDate),
                                 new SqlParameter("@P_MONYEAR",monYear),
                                 new SqlParameter("@P_DAYS",days),
                                 new SqlParameter("@P_PAYHEAD",payHead),
                                 new SqlParameter("@P_COLLEGE_CODE",collegeCode),
                                 new SqlParameter("@P_COLLEGE_NO",collegeNo)
                            };

                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_INSERT_DAYSAL", sqlParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PayController.AddDaysal-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateDaysal(int dsNo, int staffNo, DateTime monDate, string monYear, decimal days, string payHead, string collegeCode, int collegeNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        //UpdateDaysal
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {    
                                 new SqlParameter("@P_DSNO",dsNo),
                                 new SqlParameter("@P_STAFFNO",staffNo),
                                 new SqlParameter("@P_MONDATE",monDate),  
                                 new SqlParameter("@P_MONYEAR",monYear),
                                 new SqlParameter("@P_DAYS",days),
                                 new SqlParameter("@P_PAYHEAD",payHead),
                                 new SqlParameter("@P_COLLEGE_CODE",collegeCode),
                                 new SqlParameter("@P_COLLEGE_NO",collegeNo)
                            };

                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_UPDATE_DAYSAL", sqlParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PayController.UpdateDaysal-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeleteDaySal(int dsNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        //DeleteDaysal
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {    
                                 new SqlParameter("@P_DSNO",dsNo)
                            };

                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_DELETE_DAYSAL", sqlParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PayController.DeleteDaySal-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllDaysal()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        ds = objSQLHelper.ExecuteDataSet("PAYROLL_GETALL_DAYSAL");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.PayController.GetAllDaysal() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetSingleDaySal(int dsNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlparam = new SqlParameter[]
                            { 
                              new SqlParameter("@P_DSNO",dsNo)
                            };

                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_GETSINGLE_DAYSAL", sqlparam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.PayController.GetSingleDaySal() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                #endregion

                #region Salary Remark

                public int UpdateSalaryRemark(int staffNo, int idNo, string commRemark, string remark, string monYear)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        //UpdateDaysal
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {    
                             new SqlParameter("@P_STAFFNO",staffNo),
                             new SqlParameter("@P_IDNO",idNo),  
                             new SqlParameter("@P_COMMREMARK",commRemark),
                             new SqlParameter("@P_REMARK",remark), 
                             new SqlParameter("@P_MONYEAR",monYear)                            
                        };
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAYROLL_MONTHLY_REMARK", sqlParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PayController.UpdateSalaryRemark()-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateMonthRemark(string monRemark, string monYear, int collegeNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        //UpdateDaysal
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {    
                             new SqlParameter("@P_MONYEAR",monYear),                            
                             new SqlParameter("@P_MONTHREMARK",monRemark),
                             new SqlParameter("@P_COLLEGENO",collegeNo)
                          
                        };
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAYROLL_MONTH_REMARK", sqlParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PayController.UpdateMonthRemark()-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetEmployeesForRemark(int staffNo, string monYear, int collegeNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlparam = new SqlParameter[]
                        { 
                          new SqlParameter("@P_STAFFNO",staffNo),
                          new SqlParameter("@P_MONYEAR",monYear),
                          new SqlParameter("@P_COLLEGE_NO",collegeNo)
                        };

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYROLL_GETEMP_FOR_REMARK", sqlparam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.PayController.GetEmployeesForRemark() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                #endregion

                #region LwpEntry

                public int AddLwpEntry(int idNo, string monYear, DateTime lwpDate, string collegeCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        //AddDaysal
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {    
                                 new SqlParameter("@P_IDNO",idNo),
                                 new SqlParameter("@P_LWPMONYEAR",monYear),
                                 new SqlParameter("@P_LWPDATE",lwpDate),
                                 new SqlParameter("@P_COLLEGE_CODE",collegeCode)
                            };

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAYROLL_INST_LWPENTRY", sqlParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PayController.AddLwpEntry-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateLwpEntry(int lwpNo, int idNo, string monYear, DateTime lwpDate, string collegeCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        //UpdateLwpEntry
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {    
                                 new SqlParameter("@P_LWPNO",lwpNo),
                                 new SqlParameter("@P_IDNO",idNo),
                                 new SqlParameter("@P_LWPMONYEAR",monYear),  
                                 new SqlParameter("@P_LWPDATE",lwpDate),
                                 new SqlParameter("@P_COLLEGE_CODE",collegeCode)
                            };

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAYROLL_UPD_LWPENTRY", sqlParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PayController.UpdateLwpEntry-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeleteLwpEntry(int lwpNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        //DeleteDaysal
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {    
                                 new SqlParameter("@P_LWPNO",lwpNo)
                            };

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAYROLL_DEL_LWPENTRY", sqlParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PayController.DeleteLwpEntry-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllLwpEntry(int idNo, string lwpMonYear)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {    
                                  new SqlParameter("@P_IDNO",idNo),
                                  new SqlParameter("@P_LWPMONYEAR",lwpMonYear)
                            };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYROLL_GETALL_LWPENTRY", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.PayController.GetAllLwpEntry() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetSingleLwpEntry(int lwpNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlparam = new SqlParameter[]
                            { 
                              new SqlParameter("@P_LWPNO",lwpNo)
                            };

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYROLL_GETBYLWPNO_LWPENTRY", sqlparam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.PayController.GetSingleLwpEntry() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                #endregion

                #region EMPLOYEE SELECTED FIELD REPORT

                public DataSet GetAllSelectedFieldTable()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        ds = objSQLHelper.ExecuteDataSet("GETALL_SELECTED_FIELD_TABLE");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.PayController.GetAllSelectedFieldTable() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet EmployeeSelectedFieldReport(string TABLENAMES, string COLUMNNAMES, string WHERECONDITION, string ORDERBY)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlparam = new SqlParameter[]
                            { 
                              new SqlParameter("P_TABLENAMES",TABLENAMES),
                              new SqlParameter("@P_COLUMNNAMES",COLUMNNAMES), 
                              new SqlParameter("@P_WHERECONDITION",WHERECONDITION), 
                              new SqlParameter("@P_ORDERBY",(ORDERBY ==null) ?  DBNull.Value as object:ORDERBY)  
                            };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_SELECTED_FIELD_REPORT", sqlparam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.PFCONTROLLER.EmployeeSelectedFieldReport() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetSingleSelectedFieldTable(int SFTRXNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlparam = new SqlParameter[]
                            { 
                              new SqlParameter("@P_SFTRXNO",SFTRXNO)                              
                            };
                        ds = objSQLHelper.ExecuteDataSetSP("GETSINGLE_SELECTED_FIELD_TABLE", sqlparam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.PFCONTROLLER.GetSingleFieldTable() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetSelectedSftrxno(string SFTRXNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlparam = new SqlParameter[]
                            { 
                              new SqlParameter("@P_SFTRXNO",SFTRXNO)                              
                            };
                        ds = objSQLHelper.ExecuteDataSetSP("GETSELECTED_SELECTED_FIELD_TABLE", sqlparam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.PFCONTROLLER.GetSingleFieldTable() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                #endregion

                #region Employee User Creation

                public DataSet GetEmployeeForUserCreation(int userTypeId, int collegeno, int staffno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlparam = new SqlParameter[]
                            { 
                              new SqlParameter("@P_USERTYPEID",userTypeId) , 
                               new SqlParameter("@P_COLLEGE_NO",collegeno),
                              new SqlParameter("@P_STAFFNO",staffno)
                            };
                        ds = objSQLHelper.ExecuteDataSetSP("GET_EMPLOYEE_FOR_USER_CREATION", sqlparam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.PFCONTROLLER.GetEmployeeForUserCreation() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                #endregion

                public DataSet GetEmployeeInstallmentSchedule(string monthYear, int staffNo, string payHead)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                        SqlParameter[] sqlparam = new SqlParameter[]
                            { 
                              new SqlParameter("@P_MON_YEAR",monthYear),
                              new SqlParameter("@P_STAFF_NO",staffNo),
                              new SqlParameter("@P_PAYHEAD",payHead)
                              
                            };

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYROLL_MOTHLY_SCHEDULE_INSTALMENT", sqlparam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.PayController.GetAllDaysal() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetEmployeesForDisplayReport(string MonthYear)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_MONTHYEAR", MonthYear);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYBILL_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForDisplayReport-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetEmployeesForHouseRent(string MonthYear, int staffNo, string LICENSE_FEE_FIELD)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_MON_YEAR", MonthYear);
                        objParams[1] = new SqlParameter("@P_STAFF_NO", staffNo);
                        objParams[2] = new SqlParameter("@P_PAYHEAD", LICENSE_FEE_FIELD);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYROLL_GETEMPLOYEE_FOR_HOUSERENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForDisplayReport-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetEmployeesForDisplayReport(string MonthYear, int staffNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_MONTHYEAR", MonthYear);
                        objParams[1] = new SqlParameter("@P_STAFF", staffNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYBILL_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForDisplayReport-> " + ex.ToString());
                    }
                    return ds;
                }

                #region TransferData

                public int TransferData(int idNo, DateTime presentDate, decimal loginTime, int machineNo, string user, string collegecode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {   
                                 new SqlParameter("@P_IDNO",idNo),
                                 new SqlParameter("@P_PRESENTDATE",presentDate),
                                 new SqlParameter("@P_LOGINTIME",loginTime),
                                 new SqlParameter("@P_MACHINE_NUMBER",machineNo),
                                 new SqlParameter("@P_UAIMS_USER",user),                                
                                 new SqlParameter("@P_COLLEGE_CODE",collegecode)
                            };
                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_TRANSFERDATA", sqlParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PFCONTROLLER.AddPfLoan()-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeletetTransferData(DateTime presentDate)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {    
                                 new SqlParameter("@P_PRESENTDATE",presentDate)                               
                            };
                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_DELETE_EXISTING_ROWS_TRANSFERDATA", sqlParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PayController.DeleteInstallMent-> " + ex.ToString());
                    }

                    return retStatus;
                }

                #endregion

                #region Status
                // To Fetch all existing Status
                public DataSet GetAllStatus()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_STATUS_GET_BYALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetAllWorkType->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                // To insert New Staus
                public int AddStatus(Payroll objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_STATUS", objLeave.STATUS);

                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", objLeave.COLLEGE_CODE);
                        objParams[2] = new SqlParameter("@P_STATUSNO", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_STATUS_INSERT", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.AddWorkType->" + ex.ToString());
                    }
                    return retstatus;
                }

                //To modify existing Status
                public int UpdateStatus(Payroll objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_STATUSNO", objLeave.STATUSNO);
                        objParams[1] = new SqlParameter("@P_STATUS", objLeave.STATUS);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", objLeave.COLLEGE_CODE);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_STATUS_UPDATE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.UpdateHolidayType->" + ex.ToString());
                    }
                    return retstatus;
                }

                // To fetch existing status details 
                public DataSet GetSingleStatus(int STATUSNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null; ;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_STATUSNO", STATUSNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_STATUS_GET_BY_NO", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetSingleStatus->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                //To Delete existing Status
                public int DeleteStatus(int STATUSNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_STATUSNO", STATUSNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_STATUS_DELETE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.DeleteStatus->" + ex.ToString());
                    }
                    return retstatus;
                }
                #endregion


                // ADD IN PAYCONTROLLER




                public DataSet GetEmployeeForUpdateBulkPhoto(int staffno, int deptNo, int CollegeNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_STAFFNO", staffno);
                        objParams[1] = new SqlParameter("@P_DEPTNO", deptNo);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", CollegeNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SHOW_EMPLOYEE_PHOTO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.GetEmployeeForUpdateBulkPhoto-> " + ex.ToString());
                    }

                    return ds;
                }

                public int Update_Bulk_Photo(int idno, byte[] photo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        if (photo != null)
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                            SqlParameter[] objParams = new SqlParameter[2];
                            objParams[0] = new SqlParameter("@P_IDNO", idno);

                            objParams[1] = new SqlParameter("@P_PHOTO", photo);
                            // objParams[2] = new SqlParameter("@P_SIGN", img_signature);

                            if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPLOAD_BULK_EMPLOYEE_PHOTO", objParams, false) != null)
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentPhoto->" + ex.ToString());
                    }

                    return retStatus;
                }

                public int Update_Bulk_Signature(int idno, byte[] img_signature)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        if (img_signature != null)
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                            SqlParameter[] objParams = new SqlParameter[2];
                            objParams[0] = new SqlParameter("@P_IDNO", idno);


                            objParams[1] = new SqlParameter("@P_SIGN", img_signature);

                            if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPLOAD_BULK_EMP_SIGNATURE", objParams, false) != null)
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentPhoto->" + ex.ToString());
                    }

                    return retStatus;
                }

                #region bank statement
                //TO EXPORT PF AND BANK STATEMENT INTO EXCEL

                public DataSet BankStatementExcel(string MonthYear, int collegeNo, int staffNo, int bankNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_TABNAME", MonthYear);
                        objParams[1] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                        objParams[2] = new SqlParameter("@P_STAFF_NO", staffNo);
                        objParams[3] = new SqlParameter("@P_BANKNO", bankNo);
                        //objParams[4] = new SqlParameter("@P_IDNOS", idnos);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_BANK_STATEMENT_REPORT_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForDisplayReport-> " + ex.ToString());
                    }
                    return ds;
                }

                //method for listbox 26/05/21
                public DataSet BankStatementExcelforListBox(string MonthYear, int collegeNo, int staffNo, string bankNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_TABNAME", MonthYear);
                        objParams[1] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                        objParams[2] = new SqlParameter("@P_STAFF_NO", staffNo);
                        objParams[3] = new SqlParameter("@P_BANKNO", bankNo);
                        //objParams[4] = new SqlParameter("@P_IDNOS", idnos);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_BANK_STATEMENT_REPORT_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForDisplayReport-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet NEFTStatementExcel(string MonthYear, int collegeNo, int staffNo, int bankNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_TABNAME", MonthYear);
                        objParams[1] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                        objParams[2] = new SqlParameter("@P_STAFF_NO", staffNo);
                        objParams[3] = new SqlParameter("@P_BANKNO", bankNo);
                        //objParams[4] = new SqlParameter("@P_IDNOS", idnos);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_NEFT_STATEMENT_REPORT_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForDisplayReport-> " + ex.ToString());
                    }
                    return ds;
                }



                public DataSet GetEmployeeForBankStatement(int staffNo, int orderByIdno, int collegeNo, string monthYear, int bankNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_STAFFNO", staffNo);
                        objParams[1] = new SqlParameter("@P_ORDERBY_IDNO", orderByIdno);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                        objParams[3] = new SqlParameter("@P_MONYEAR", monthYear);
                        objParams[4] = new SqlParameter("@P_BANKNO", bankNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_RET_PAY_EMP_BANK_STATEMENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AttendanceController.GetAttendanceOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet PFStatementExcel(string MonthYear, int collegeNo, string staffNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_TABNAME", MonthYear);
                        objParams[1] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                        objParams[2] = new SqlParameter("@P_STAFF_NO", staffNo);
                        //objParams[2] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_PF_STATEMENT_REPORT_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForDisplayReport-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet BankStatementText(string MonthYear, int collegeNo, int staffNo, int bankNo, int EmployeeType)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_MONYEAR", MonthYear);
                        objParams[1] = new SqlParameter("@P_COLLEGENO", collegeNo);
                        objParams[2] = new SqlParameter("@P_STAFFNO", staffNo);
                        objParams[3] = new SqlParameter("@P_BANKNO", bankNo);
                        objParams[4] = new SqlParameter("@P_EMPLOYEETYPE", EmployeeType);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_BANK_STATEMENT_TEXT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.BankStatementText-> " + ex.ToString());
                    }
                    return ds;
                }

                //method for  bankstatement for listbox 26/05/21
                public DataSet BankStatementTextListbox(string MonthYear, int collegeNo, string staffNo, string bankNo, int EmployeeType)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_MONYEAR", MonthYear);
                        objParams[1] = new SqlParameter("@P_COLLEGENO", collegeNo);
                        objParams[2] = new SqlParameter("@P_STAFFNO", staffNo);
                        objParams[3] = new SqlParameter("@P_BANKNO", bankNo);
                        objParams[4] = new SqlParameter("@P_EMPLOYEETYPE", EmployeeType);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_BANK_STATEMENT_TEXT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.BankStatementText-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                #region Salary Deposit
                public DataSet GetSalOBOfEmployee(int staffNo, int orderByIdno, int collegeNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_STAFFNO", staffNo);
                        objParams[1] = new SqlParameter("@P_ORDERBY_IDNO", orderByIdno);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_RET_PAY_SALARY_DEPOSIT_OB", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AttendanceController.GetAttendanceOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int UpdateSALOB(decimal obamt, DateTime obdate, int idNo)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_OBAMT", obamt);
                        objParams[1] = new SqlParameter("@P_OBDATE", obdate);
                        objParams[2] = new SqlParameter("@P_IDNO", idNo);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_SAL_OB", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.AttendanceController.UpdateAbsentDays-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion


                #region Bank Account Mapping

                public int AddUpdateBankAccMapping(Payroll objCalPay)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_BankAccMappingId", objCalPay.BankAccMappingId);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", objCalPay.SCHEMENO);
                        objParams[2] = new SqlParameter("@P_STAFFNO", objCalPay.STAFFNO);
                        objParams[3] = new SqlParameter("@P_BANKNO ", objCalPay.BANKNO);
                        objParams[4] = new SqlParameter("@P_BANK_ACCNO ", objCalPay.BANK_ACCNO);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objCalPay.COLLEGE_CODE);
                        objParams[6] = new SqlParameter("@P_COLLEGE_ID", objCalPay.COLLEGE_ID);
                        objParams[7] = new SqlParameter("@P_OUT ", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INS_UPD_BANK_MAPPING", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PayController.ComplaintController.AddCalPay-> " + ex.ToString());
                    }
                    return retStatus;
                }



                public DataSet GetAllMappingBank(Payroll objCalPay)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_BankAccMappingId", objCalPay.BankAccMappingId);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_ALL_MAPPED_BANK", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.GetAllCalPay-> " + ex.ToString());
                    }
                    finally
                    {


                        ds.Dispose();

                    }
                    return ds;
                }

                public DataSet GetAll_BYID_MappingBank(Payroll objCalPay)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", objCalPay.COLLEGE_ID);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_ALL_BY_ID_MAPPED_BANK", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.GetAllCalPay-> " + ex.ToString());
                    }
                    finally
                    {


                        ds.Dispose();

                    }
                    return ds;
                }

                public DataSet GetCheckMappingBankExist(Payroll objCalPay)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", objCalPay.SCHEMENO);
                        objParams[1] = new SqlParameter("@P_STAFFNO", objCalPay.STAFFNO);
                        objParams[2] = new SqlParameter("@P_BANKNO ", objCalPay.BANKNO);
                        objParams[3] = new SqlParameter("@P_BANK_ACCNO ", objCalPay.BANK_ACCNO);
                        objParams[4] = new SqlParameter("@P_COLLEGE_ID ", objCalPay.COLLEGE_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_CHECK_MAPPED_BANK_EXIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.GetAllCalPay-> " + ex.ToString());
                    }
                    finally
                    {


                        ds.Dispose();

                    }
                    return ds;
                }

                #endregion



                public int UpdateRegularDed(int INO, string MONAMT, bool stop)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Update complaint type
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_INO", INO);
                        objParams[1] = new SqlParameter("@P_STOP ", stop);
                        objParams[2] = new SqlParameter("@P_MONAMT ", MONAMT);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_REGULAR_DED", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PayController.UpdateIncerment-> " + ex.ToString());
                    }

                    return retStatus;
                }

                #region Daily Wages
                //DAILY WAGES 
                public DataSet GetDailyWagesEmployees(int collegeNo, int staffNo, int month)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_COLLEGENO ", collegeNo);
                        objParams[1] = new SqlParameter("@P_STAFFNO ", staffNo);
                        objParams[2] = new SqlParameter("@P_MONTH", month);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_RET_DAILY_WAGES_EMPLOYEES", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.GetIncrementEmployees-> " + ex.ToString());
                    }

                    return ds;
                }

                public int UpdateDailyWagesBasic(int collegeNo, double idNo, double Basic, double dailyamt)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Update complaint type
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_COLLEGENO", collegeNo);
                        objParams[1] = new SqlParameter("@P_IDNO", idNo);
                        objParams[2] = new SqlParameter("@P_BASIC ", Basic);
                        objParams[3] = new SqlParameter("@P_DAILYAMT ", dailyamt);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_DAILY_WAGES_BASIC", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.UpdateIncerment-> " + ex.ToString());
                    }

                    return retStatus;
                }

                //END DAILY WAGES
                #endregion

                public DataSet GetPensionEmployeesForRemark(int staffNo, string monYear)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlparam = new SqlParameter[]
                        { 
                          new SqlParameter("@P_STAFFNO",staffNo),
                          new SqlParameter("@P_MONYEAR",monYear)
                        };

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PENSION_PAYROLL_GETEMP_FOR_REMARK", sqlparam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.PayController.GetEmployeesForRemark() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                #region Arrears

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

                public DataSet GetPensionArrearsInfo(int arno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ARNO", arno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PENSION_GET_ARREARS", objParams);
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

                public DataSet GetAllMonth(string fromDate, string todate, int collegeno)
                {

                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_FROM_DATE", fromDate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", todate);
                        objParams[2] = new SqlParameter("@P_COLLEGENO", collegeno);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_REPORT_DROPDOWN_GET_MONTH1", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayController.GetAllPayHead-> " + ex.ToString());
                    }
                    return ds;
                }



                public DataSet GetPensionAllMonth(string fromDate, string todate, int collegeno)
                {

                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_FROM_DATE", fromDate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", todate);
                        objParams[2] = new SqlParameter("@P_COLLEGENO", collegeno);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PENSION_REPORT_DROPDOWN_GET_MONTH1", objParams);
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

                public DataSet GetPensionArrearsDiff(int arno, int idno, string mon)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ARNO", arno);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);
                        objParams[2] = new SqlParameter("@P_MON", mon);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PENSION_GET_ARREARS_DIFF", objParams);
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

                public DataSet GetPensionArrearsAmount(int arno, int idno, string mon, string payhead)
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


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PENSION_GET_ARREARS_AMT", objParams);
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

                public DataSet GetPensionActualAmount(int idno, string mon, string payhead)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_MON", mon);
                        objParams[2] = new SqlParameter("@P_PAYHEAD", payhead);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PENSION_GET_PAYHEAD_AMT", objParams);
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

                public int TransferToPensionSuplBill(int suplarrearno, string suplordno, DateTime supldate, int suplbillHeadno, string suplheadname, string suplmon)
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

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PENSION_TRANSFER_TO_SUPLIMENTRY_BILL", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.PayController.UpdateSalaryGrant-> " + ex.ToString());
                    }
                    return retStatus;
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

                public int DeletePensionArrearsRecord(int arno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ARNO", arno);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PENSION_DELETE_ARREARS_RECORDS", objParams, false) != null)
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

                public int AddPayArrears(Payroll objPayroll)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[14];
                        objParams[0] = new SqlParameter("@P_COLLEGENO", objPayroll.STAFFNO);
                        objParams[1] = new SqlParameter("@P_PAYHEAD", objPayroll.PAYHEAD);
                        objParams[2] = new SqlParameter("@P_AFRM", objPayroll.AFRM);
                        objParams[3] = new SqlParameter("@P_ATO ", objPayroll.ATO);
                        objParams[4] = new SqlParameter("@P_PER ", objPayroll.Per);
                        objParams[5] = new SqlParameter("@P_RULENO", objPayroll.Payrule);
                        objParams[6] = new SqlParameter("@P_GOVORDNO", objPayroll.GOVORDNO);
                        objParams[7] = new SqlParameter("@P_GOVORDDT", objPayroll.GOVORDDT);
                        objParams[8] = new SqlParameter("@P_OFFORDNO", objPayroll.OFFORDNO);
                        objParams[9] = new SqlParameter("@P_OFFORDDT", objPayroll.OFFORDDT);
                        objParams[10] = new SqlParameter("@P_REMARK", objPayroll.REMARK);
                        objParams[11] = new SqlParameter("@P_COLLEGE_CODE", objPayroll.COLLEGE_CODE);
                        objParams[12] = new SqlParameter("@P_SR_NO", objPayroll.SR_NO);
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

                public int AddPensionArrears(Payroll objPayroll)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_COLLEGENO", objPayroll.STAFFNO);
                        objParams[1] = new SqlParameter("@P_PAYHEAD", objPayroll.PAYHEAD);
                        objParams[2] = new SqlParameter("@P_AFRM", objPayroll.AFRM);
                        objParams[3] = new SqlParameter("@P_ATO ", objPayroll.ATO);
                        objParams[4] = new SqlParameter("@P_PER ", objPayroll.Per);
                        objParams[5] = new SqlParameter("@P_RULENO", objPayroll.Payrule);
                        objParams[6] = new SqlParameter("@P_GOVORDNO", objPayroll.GOVORDNO);
                        objParams[7] = new SqlParameter("@P_GOVORDDT", objPayroll.GOVORDDT);
                        objParams[8] = new SqlParameter("@P_OFFORDNO", objPayroll.OFFORDNO);
                        objParams[9] = new SqlParameter("@P_OFFORDDT", objPayroll.OFFORDDT);
                        objParams[10] = new SqlParameter("@P_REMARK", objPayroll.REMARK);
                        objParams[11] = new SqlParameter("@P_COLLEGE_CODE", objPayroll.COLLEGE_CODE);
                        objParams[12] = new SqlParameter("@P_ARNO ", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PENSION_INS_ARREARS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.PayController.AddPayArrears-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int CalculateArrears(int collegeNo, string payhead, string fromdate, string todate, string idno, double per, int arno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    //DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                        objParams[1] = new SqlParameter("@P_FROM_DATE", fromdate);
                        objParams[2] = new SqlParameter("@P_TO_DATE", todate);
                        objParams[3] = new SqlParameter("@P_HEAD", payhead);
                        objParams[4] = new SqlParameter("@P_PER", per);
                        objParams[5] = new SqlParameter("@P_IDNO", idno);
                        objParams[6] = new SqlParameter("@P_ARNO", arno);

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

                public int CalculatePensionArrears(int collegeNo, string payhead, string fromdate, string todate, string idno, double per, int arno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    //DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                        objParams[1] = new SqlParameter("@P_FROM_DATE", fromdate);
                        objParams[2] = new SqlParameter("@P_TO_DATE", todate);
                        objParams[3] = new SqlParameter("@P_HEAD", payhead);
                        objParams[4] = new SqlParameter("@P_PER", per);
                        objParams[5] = new SqlParameter("@P_IDNO", idno);
                        objParams[6] = new SqlParameter("@P_ARNO", arno);

                        // ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_ARREARS_CALCULATE2", objParams);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PENSION_ARREARS_CALCULATE2", objParams, false) != null)
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

                public int DeletePensionSupplBillTransferRecord(int arno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ARNO", arno);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PENSION_SUPPLBILL_TRANSFER_DELETE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.PayController.UpdateSalaryGrant-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion

                #region Salary Remark



                public int UpdatePensionSalaryRemark(int staffNo, int idNo, string commRemark, string remark, string monYear)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        //UpdateDaysal
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {    
                             new SqlParameter("@P_STAFFNO",staffNo),
                             new SqlParameter("@P_IDNO",idNo),  
                             new SqlParameter("@P_COMMREMARK",commRemark),
                             new SqlParameter("@P_REMARK",remark), 
                             new SqlParameter("@P_MONYEAR",monYear)                            
                        };
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PENSION_PAYROLL_MONTHLY_REMARK", sqlParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PayController.UpdateSalaryRemark()-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateMonthRemark(string monRemark, string monYear)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        //UpdateDaysal
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {    
                             new SqlParameter("@P_MONYEAR",monYear),                            
                             new SqlParameter("@P_MONTHREMARK",monRemark)
                          
                        };
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAYROLL_MONTH_REMARK", sqlParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PayController.UpdateMonthRemark()-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdatePensionMonthRemark(string monRemark, string monYear)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        //UpdateDaysal
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {    
                             new SqlParameter("@P_MONYEAR",monYear),                            
                             new SqlParameter("@P_MONTHREMARK",monRemark)
                          
                        };
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PENSION_PAYROLL_MONTH_REMARK", sqlParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PayController.UpdateMonthRemark()-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetEmployeesForRemark(int staffNo, string monYear)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlparam = new SqlParameter[]
                        { 
                          new SqlParameter("@P_STAFFNO",staffNo),
                          new SqlParameter("@P_MONYEAR",monYear)
                        };

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYROLL_GETEMP_FOR_REMARK", sqlparam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.PayController.GetEmployeesForRemark() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }



                #endregion

                #region LIC Excel Report
                //TO LIC Excel Report

                public DataSet LICExportExcel(string MonthYear, int collegeNo, int staffNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_TABNAME", MonthYear);
                        objParams[1] = new SqlParameter("@P_COLLEGNO", collegeNo);
                        objParams[2] = new SqlParameter("@P_STAFF_NO", staffNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_LIC_EXPORT_EXCEL_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.LICExportExcel-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet EPFStatementExcel(string MonthYear, int collegeNo, int staffNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_TABNAME", MonthYear);
                        objParams[1] = new SqlParameter("@P_COLLEGNO", collegeNo);
                        objParams[2] = new SqlParameter("@P_STAFF_NO", staffNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_EPF_DEDUCTION_EXCEL_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForDisplayReport-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet EPFStatementExcelNew(string MonthYear, int collegeNo, int staffNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_TABNAME", MonthYear);
                        objParams[1] = new SqlParameter("@P_COLLEGNO", collegeNo);
                        objParams[2] = new SqlParameter("@P_STAFF_NO", staffNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_EPF_DEDUCTION_EXCEL_NEW_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForDisplayReport-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet EPFStatementWord(string MonthYear, int collegeNo, int staffNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_TABNAME", MonthYear);
                        objParams[1] = new SqlParameter("@P_COLLEGNO", collegeNo);
                        objParams[2] = new SqlParameter("@P_STAFF_NO", staffNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_EPF_DEDUCTION_WORD_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForDisplayReport-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #region SLAB MASTER
                public int AddUpdateSlabCalculation(Payroll objPay)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SLAB_NAME", objPay.SLAB_NAME);
                        objParams[1] = new SqlParameter("@P_FROM_SLAB", objPay.FROM_SLAB);
                        objParams[2] = new SqlParameter("@P_TO_SLAB", objPay.TO_SLAB);
                        objParams[3] = new SqlParameter("@P_AMOUNT", objPay.AMOUNT);
                        objParams[4] = new SqlParameter("@P_PTSLABID", objPay.PTSLABID);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INSERT_UPDATE_SLABMASTER", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITMascontroller.AddEmployeeIT -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetSlabCalculation()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_SLAB_CALCULATION", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayConfigurationController.GetITConfiguration->" + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSlabCalculationById(int PTSLABID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PTSLABID", PTSLABID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_SLAB_CALCULATION_BYID", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.PayConfigurationController.GetITConfiguration->" + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                public DataSet GetIncrementEmployeesper(int staffNo, int incMonth, int collegeNo, int subdeptno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_STAFFNO ", staffNo);
                        objParams[1] = new SqlParameter("@P_MONTH", incMonth);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                        objParams[3] = new SqlParameter("@P_SUBDEPTNO", subdeptno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_RET_INCREMENT_EMPLOYEES_PER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.GetIncrementEmployees-> " + ex.ToString());
                    }

                    return ds;
                }

                public int UpdateIncermentper(int idNo, int oldBasic, int newBasic, char incYesNo, DateTime doi, decimal Incper)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Update complaint type
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        objParams[1] = new SqlParameter("@P_OBASIC ", oldBasic);
                        objParams[2] = new SqlParameter("@P_BASIC ", newBasic);
                        objParams[3] = new SqlParameter("@P_INCYN  ", incYesNo);
                        objParams[4] = new SqlParameter("@P_DOI   ", doi);
                        objParams[5] = new SqlParameter("@P_INCPER   ", Incper);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_UPD_INCREMENT_PER", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.UpdateIncerment-> " + ex.ToString());
                    }

                    return retStatus;
                }
                //Amol sawarkar 16-02-2022

                //public DataSet YearlyHeadwiseExportExcelReport(string fromdate, string todate, string payhead, int collegeNo, int staffNo)
                public DataSet YearlyHeadwiseExportExcelReport(string Fdate, string Todate, string payhead, int collegeNo, string staffNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        //objParams[0] = new SqlParameter("@P_TABNAME", MonthYear);
                        //objParams[0] = new SqlParameter("@P_FROM_DATE", fromdate);
                        //objParams[1] = new SqlParameter("@P_TO_DATE", todate);
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", Todate);
                        objParams[2] = new SqlParameter("@P_HEAD", payhead);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                        objParams[4] = new SqlParameter("@P_STAFF_NO", staffNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PAY_YEARLY_PAYHEAD_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.YearlyHeadwiseExportExcelReport-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet YearlyGrossExportExcelReport(string Fdate, string Todate, int collegeNo, string staffNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        //objParams[0] = new SqlParameter("@P_TABNAME", MonthYear);
                        //objParams[0] = new SqlParameter("@P_FROM_DATE", fromdate);
                        //objParams[1] = new SqlParameter("@P_TO_DATE", todate);
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", Todate);
                        // objParams[2] = new SqlParameter("@P_HEAD", payhead);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                        objParams[3] = new SqlParameter("@P_STAFF_NO", staffNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PAY_YEARLY_GROSS_SALARY_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.YearlyHeadwiseExportExcelReport-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet DepatmentWiseYearlyGrossSalaryReport(string Fdate, string Todate, int collegeNo, string staffNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        //objParams[0] = new SqlParameter("@P_TABNAME", MonthYear);
                        //objParams[0] = new SqlParameter("@P_FROM_DATE", fromdate);
                        //objParams[1] = new SqlParameter("@P_TO_DATE", todate);
                        objParams[0] = new SqlParameter("@P_FROM_DATE", Fdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", Todate);
                        // objParams[2] = new SqlParameter("@P_HEAD", payhead);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                        objParams[3] = new SqlParameter("@P_STAFF_NO", staffNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PAY_DEPATMENT_WISE_YEARLY_GROSS_SALARY_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.DepatmentWiseYearlyGrossSalaryReport-> " + ex.ToString());
                    }
                    return ds;
                }
                public int AddUpdateMapDept(int Acd_DeptNo, string Acd_DeptName, int Pay_DeptNo, string Pay_DeptName, int Dept_no, string Com_Code)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@_DEPT_NO", Dept_no);
                        objParams[1] = new SqlParameter("@_ACD_DEPTNO", Acd_DeptNo);
                        objParams[2] = new SqlParameter("@_ACD_DEPTNAME", Acd_DeptName);
                        objParams[3] = new SqlParameter("@_PAY_DEPTNO", Pay_DeptNo);
                        objParams[4] = new SqlParameter("@_PAY_DEPTNAME", Pay_DeptName);
                        objParams[5] = new SqlParameter("@_COMP_COD", Com_Code);
                        objParams[6] = new SqlParameter("@_P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;
                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACC_INSERT_UPDATE_DEPT_MAPPING", objParams, true));
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddFeeAccountTransfer-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //28-09-2022

                public DataSet GetEmployeeForUserEmailSend(int userTypeId, int collegeno, int staffno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlparam = new SqlParameter[]
                            { 
                              new SqlParameter("@P_USERTYPEID",userTypeId) , 
                               new SqlParameter("@P_COLLEGE_NO",collegeno),
                              new SqlParameter("@P_STAFFNO",staffno)
                            };
                        ds = objSQLHelper.ExecuteDataSetSP("GET_EMPLOYEE_FOR_USER_CREATION_FOR_SEND_MAIL", sqlparam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.PFCONTROLLER.GetEmployeeForUserCreation() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }


                // prashant sir 
                public DataSet GetEmployeeForUserCreationCommon(int userTypeId, int collegeno, int staffno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlparam = new SqlParameter[]
                            { 
                              new SqlParameter("@P_USERTYPEID",userTypeId) , 
                               new SqlParameter("@P_COLLEGE_NO",collegeno),
                              new SqlParameter("@P_STAFFNO",staffno)
                            };
                        ds = objSQLHelper.ExecuteDataSetSP("GET_EMPLOYEE_FOR_USER_CREATION_COMMON", sqlparam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.PFCONTROLLER.GetEmployeeForUserCreation() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public int SaveEarningDeduction(int Calno, int Collegeno, string Rule)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_CALPAY_NO", Calno);

                        objParams[1] = new SqlParameter("@P_COLLEGE_NO", Collegeno);

                        objParams[2] = new SqlParameter("@P_RULE", Rule);
                        objParams[3] = new SqlParameter("@P_ID", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_SP_INS_CALPAY", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.AddAppointment-> " + ex.ToString());
                    }
                    return retStatus;
                }


                // Added on 13-01-2023

                #region Entity Master
                // Added by Purva RAUT
                public int AddEntityMaster(EmpMaster empmas)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_EnityNo", empmas.EnityNo);
                        objParams[1] = new SqlParameter("@P_EntityName", empmas.EntityName);
                        objParams[2] = new SqlParameter("@P_CollegeCode", empmas.COLLEGE_CODE);
                        objParams[3] = new SqlParameter("@P_OrganizationId", empmas.OrganizationId);
                        objParams[4] = new SqlParameter("@P_ActiveStaus", empmas.IsActive);
                        objParams[5] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INSERT_UPDATE_ENTITY_MASTER", objParams, true);
                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        if(Convert.ToInt32(ret) == 2)
                             retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITMascontroller.AddEmployeeIT -> " + ex.ToString());
                    }
                    return retStatus;
                }


                public DataSet GetEntityMaster(EmpMaster empmas)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_EnityNo", empmas.EnityNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ENTITYMASTER", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayConfigurationController.GetITConfiguration->" + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #region Attrition Type Master
                public int AddAttritionTypeMaster(EmpMaster empmas)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_AttritionTypeNo", empmas.AttritionTypeNo);
                        objParams[1] = new SqlParameter("@P_AttritionName", empmas.AttritionName);
                        objParams[2] = new SqlParameter("@P_CollegeCode", empmas.COLLEGE_CODE);
                        objParams[3] = new SqlParameter("@P_OrganizationId", empmas.OrganizationId);
                        objParams[4] = new SqlParameter("@P_ActiveStaus", empmas.IsActive);
                        objParams[5] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_INSERT_UPDATE_ATTRITION_TYPE_MASTER", objParams, true);
                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITMascontroller.AddEmployeeIT -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAttritionTypeMaster(EmpMaster empmas)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_AttritionTypeNo", empmas.EnityNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_GET_ATTRITIONTYPEMASTER", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayConfigurationController.GetITConfiguration->" + ex.ToString());
                    }
                    return ds;
                }
                #endregion

            }
        }
    }
}
