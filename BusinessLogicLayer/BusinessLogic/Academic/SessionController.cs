//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS LOGIC FILE [SESSIONCONTROLLER]                              
// CREATION DATE : 18-MAY-2009                                                          
// CREATED BY    : SANJAY RATNAPARKHI                                                   
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
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class SessionController
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public int AddSession(IITMS.UAIMS.BusinessLayer.BusinessEntities.Session objSession)
                {
                    //int retStatus = Convert.ToInt32(CustomStatus.Others);
                    int status = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add
                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_SESSION_PNAME", objSession.Session_PName);
                        objParams[1] = new SqlParameter("@P_SESSION_STDATE", objSession.Session_SDate);
                        objParams[2] = new SqlParameter("@P_SESSION_ENDDATE", objSession.Session_EDate);
                        objParams[3] = new SqlParameter("@P_SESSION_NAME", objSession.Session_Name);
                        objParams[4] = new SqlParameter("@P_ODD_EVEN", objSession.Odd_Even);
                        objParams[5] = new SqlParameter("@P_SESSNAME_HINDI", objSession.Sessname_hindi);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objSession.College_code);
                        objParams[7] = new SqlParameter("@P_FLOCK", objSession.Flock);
                        objParams[8] = new SqlParameter("@P_EXAMTYPE", objSession.ExamType);
                        objParams[9] = new SqlParameter("@P_ACADEMIC_YEAR", objSession.academic_year);
                        objParams[10] = new SqlParameter("@P_IS_ACTIVE", objSession.IsActive);
                        objParams[11] = new SqlParameter("@P_PROVISIONAL_CERTIFICATE_SESSION_NAME", objSession.PROVISIONAL_CERTIFICATE_SESSION_NAME);
                        objParams[12] = new SqlParameter("@P_COLLEGE_ID", objSession.College_Id_str); //modify by maithili [19-08-2022]
                        objParams[13] = new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])); //Added By Rishabh on 25/01/2022
                        objParams[14] = new SqlParameter("@P_SESSIONNO", SqlDbType.Int);
                        objParams[14].Direction = ParameterDirection.Output;

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_INS_SESSIONMASTER", objParams, true) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_INS_SESSIONMASTER", objParams, true);

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001" && obj.ToString() != "-2")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (obj.ToString().Equals("-2"))
                        {
                            status = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.AddSession-> " + ex.ToString());
                    }
                    return status;
                }


                public int UpdateSession(IITMS.UAIMS.BusinessLayer.BusinessEntities.Session objSession)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //update
                        objParams = new SqlParameter[14];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objSession.SessionNo);
                        objParams[1] = new SqlParameter("@P_SESSION_PNAME", objSession.Session_PName);
                        objParams[2] = new SqlParameter("@P_SESSION_STDATE", objSession.Session_SDate);
                        objParams[3] = new SqlParameter("@P_SESSION_ENDDATE", objSession.Session_EDate);
                        objParams[4] = new SqlParameter("@P_SESSION_NAME", objSession.Session_Name);
                        objParams[5] = new SqlParameter("@P_ODD_EVEN", objSession.Odd_Even);
                        objParams[6] = new SqlParameter("@P_SESSNAME_HINDI", objSession.Sessname_hindi);
                        objParams[7] = new SqlParameter("@P_EXAMTYPE", objSession.ExamType);
                        objParams[8] = new SqlParameter("@P_ACADEMIC_YEAR", objSession.academic_year);
                        objParams[9] = new SqlParameter("@P_FLOCK", objSession.Flock);
                        objParams[10] = new SqlParameter("@P_IS_ACTIVE", objSession.IsActive);
                        objParams[11] = new SqlParameter("@P_PROVISIONAL_CERTIFICATE_SESSION_NAME", objSession.PROVISIONAL_CERTIFICATE_SESSION_NAME);
                        objParams[12] = new SqlParameter("@P_COLLEGE_ID", objSession.College_Id_str); //modify by maithili [19-08-2022]
                        objParams[13] = new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])); //Added By Rishabh on 25/01/2022

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_UPD_SESSIONMASTER", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.UpdateCT-> " + ex.ToString());
                    }

                    return retStatus;
                }
                public DataSet GetCurrentSession()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_ALL_BRANCH", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetCurrentSession-> " + ex.ToString());
                    }
                    return ds;
                }
               
                public DataSet GetAllSession()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SESSION_SP_ALL_SESSIONMASTER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetAllSession-> " + ex.ToString());
                    }
                    return ds;
                }
               
                public SqlDataReader GetSingleSession(int sessionno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("P_SESSIONNO", sessionno);
                        dr = objSQLHelper.ExecuteReaderSP("PKG_SESSION_SP_RET_SESSIONMASTER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetSingleSession-> " + ex.ToString());
                    }
                    return dr;
                }

                /// <summary>
                /// Added by Nehal 20-02-2023
                /// </summary>
                /// <param name="ATLAS"></param>
                /// <returns></returns>
                /// 

                public DataSet GetAllSession_Modified()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SESSION_SP_ALL_SESSION_MODIFIED", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetAllSession_Modified-> " + ex.ToString());
                    }
                    return ds;
                }

                public SqlDataReader GetSingleSession_Modified(int sessionno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("P_SESSIONID", sessionno);
                        dr = objSQLHelper.ExecuteReaderSP("PKG_SESSION_SP_RET_SESSION_MODIFIED", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetSingleSession_Modified-> " + ex.ToString());
                    }
                    return dr;
                }
                // modify bY VINAY MISHRA ON DATED 21/06/2023
                public int UpdateSession_Modified(IITMS.UAIMS.BusinessLayer.BusinessEntities.Session objSession)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //update
                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_SESSIONID", objSession.SessionNo);
                        objParams[1] = new SqlParameter("@P_SESSION_PNAME", objSession.Session_PName);
                        objParams[2] = new SqlParameter("@P_SESSION_STDATE", objSession.Session_SDate);
                        objParams[3] = new SqlParameter("@P_SESSION_ENDDATE", objSession.Session_EDate);
                        objParams[4] = new SqlParameter("@P_SESSION_NAME", objSession.Session_Name);
                        objParams[5] = new SqlParameter("@P_ODD_EVEN", objSession.Odd_Even);
                        objParams[6] = new SqlParameter("@P_SESSNAME_HINDI", objSession.Sessname_hindi);
                        objParams[7] = new SqlParameter("@P_EXAMTYPE", objSession.ExamType);
                        objParams[8] = new SqlParameter("@P_ACADEMIC_YEAR", objSession.academic_year);
                        objParams[9] = new SqlParameter("@P_FLOCK", objSession.Flock);
                        objParams[10] = new SqlParameter("@P_IS_ACTIVE", objSession.IsActive);
                        objParams[11] = new SqlParameter("@P_SEQUENCE_NO", objSession.sequence_no); //Modified by Vinay Mishra on 16/06/2023
                        //objParams[11] = new SqlParameter("@P_PROVISIONAL_CERTIFICATE_SESSION_NAME", objSession.PROVISIONAL_CERTIFICATE_SESSION_NAME);
                        //objParams[12] = new SqlParameter("@P_COLLEGE_ID", objSession.College_Id_str); //modify by maithili [19-08-2022]

                        objParams[12] = new SqlParameter("@P_ACADEMIC_YEAR_ID", objSession.AcademicYearID); // Added by Shailendra K on dated 13.03.2024 as per T-55537 & 55470
                        objParams[13] = new SqlParameter("@P_STUDY_PATTERN_ID", objSession.StudyPatternNo); // Added by Shailendra K on dated 13.03.2024 as per T-55537 & 55470
                        objParams[14] = new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])); //Added By Rishabh on 25/01/2022

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_UPD_SESSION_MODIFIED", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);



                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.UpdateSession_Modified-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int AddSession_Modified(IITMS.UAIMS.BusinessLayer.BusinessEntities.Session objSession)
                {
                    //int retStatus = Convert.ToInt32(CustomStatus.Others);
                    int status = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add
                        objParams = new SqlParameter[16];
                        objParams[0] = new SqlParameter("@P_SESSION_PNAME", objSession.Session_PName);
                        objParams[1] = new SqlParameter("@P_SESSION_STDATE", objSession.Session_SDate);
                        objParams[2] = new SqlParameter("@P_SESSION_ENDDATE", objSession.Session_EDate);
                        objParams[3] = new SqlParameter("@P_SESSION_NAME", objSession.Session_Name);
                        objParams[4] = new SqlParameter("@P_ODD_EVEN", objSession.Odd_Even);
                        objParams[5] = new SqlParameter("@P_SESSNAME_HINDI", objSession.Sessname_hindi);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objSession.College_code);
                        objParams[7] = new SqlParameter("@P_FLOCK", objSession.Flock);
                        objParams[8] = new SqlParameter("@P_EXAMTYPE", objSession.ExamType);
                        objParams[9] = new SqlParameter("@P_ACADEMIC_YEAR", objSession.academic_year);
                        objParams[10] = new SqlParameter("@P_IS_ACTIVE", objSession.IsActive);
                        objParams[11] = new SqlParameter("@P_SEQUENCE_NO", objSession.sequence_no); //Modified by Vinay Mishra on 16/06/2023
                        //objParams[11] = new SqlParameter("@P_PROVISIONAL_CERTIFICATE_SESSION_NAME", objSession.PROVISIONAL_CERTIFICATE_SESSION_NAME);
                        //objParams[12] = new SqlParameter("@P_COLLEGE_ID", objSession.College_Id_str); //modify by maithili [19-08-2022]
                        objParams[12] = new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])); //Added By Rishabh on 25/01/2022

                        objParams[13] = new SqlParameter("@P_ACADEMIC_YEAR_ID", objSession.AcademicYearID); // Added by Shailendra K on dated 13.03.2024 as per T-55537 & 55470
                        objParams[14] = new SqlParameter("@P_STUDY_PATTERN_ID", objSession.StudyPatternNo); // Added by Shailendra K on dated 13.03.2024 as per T-55537 & 55470

                        objParams[15] = new SqlParameter("@P_SESSIONID", SqlDbType.Int);
                        objParams[15].Direction = ParameterDirection.Output;

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_INS_SESSIONMASTER", objParams, true) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_INS_SESSION_MODIFIED", objParams, true);

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001" && obj.ToString() != "-2")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (obj.ToString().Equals("-2"))
                        {
                            status = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.AddSession_Modified-> " + ex.ToString());
                    }
                    return status;
                }

                public int AddSessionMaster_Modified(IITMS.UAIMS.BusinessLayer.BusinessEntities.Session objSession, int Sessionid)
                {
                    int status = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONID", Sessionid);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", objSession.College_Id_str);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_INS_SESSIONMASTER_MODIFIED", objParams, true);

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001" && obj.ToString() != "-2")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (obj.ToString().Equals("-2"))
                        {
                            status = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.AddSessionMaster_Modified-> " + ex.ToString());
                    }
                    return status;
                }

                public int ActiveDeactiveSessionConfiguration(int Sessionno, int uano, string ipAddress, int activestatus)
                {
                    int status = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", uano);
                        objParams[2] = new SqlParameter("@P_IPADDRESS", ipAddress);
                        objParams[3] = new SqlParameter("@P_ACTIVESTATUS", activestatus);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_UPD_SESSION_MASTER_ACTIVE_DEACTIVE", objParams, true);

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001" && obj.ToString() != "-2")
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.ActiveDeactiveSessionConfiguration-> " + ex.ToString());
                    }
                    return status;
                }
            }
        }
    }
}
