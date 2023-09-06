using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    
            public class StudentRegistration
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                /// <summary>
                /// 
                /// </summary>
                /// <param name="idno"></param>
                /// <param name="utype"></param>
                /// <param name="userDec"></param>
                /// <param name="ua_no"></param>
                /// <returns></returns>
                public DataSet GetStudentList( int idno ,int utype, int userDec ,int ua_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_TYPE", utype);
                        objParams[2] = new SqlParameter("@P_DEC", userDec);
                        objParams[3] = new SqlParameter("@P_UA_NO", ua_no);                        

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PREREGIST_SP_RET_STUDENTS", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentList-> " + ex.ToString());
                    }

                    return ds;
                }

                public bool CheckStudentForFaculty(int idno,int ua_no)
                {
                    bool ret = false;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);

                        object rt = objSQLHelper.ExecuteScalarSP("PKG_STUDENT_SP_CHECK_STUD_FOR_FAC", objParams);
                        if (rt == null || rt == "")
                            ret = false;
                        else
                            ret = true;

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.CheckStudentForFaculty-> " + ex.ToString());
                    }

                    return ret;
                }

                /// <summary>
                /// 
                /// </summary>
                /// <param name="idno"></param>
                /// <param name="sessionno"></param>
                /// <param name="schemeno"></param>
                /// <param name="sectionno"></param>
                /// <param name="flagStatus"></param>
                /// <returns></returns>
                public DataTable GetSubjectDetailsById(int idno,int sessionno,int schemeno,int sectionno,int flagStatus)
                {
                    DataTable dt = null;
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[4] = new SqlParameter("@P_FLAG", flagStatus);

                        dt = objSH.ExecuteDataSetSP("PKG_PREREGIST_SP_RET_SUBJECTDETAILS", objParams).Tables[0];
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetSubjectDetailsById-> " + ex.ToString());
                    }
                    return dt;
                }

                /// <summary>
                /// 
                /// </summary>
                /// <returns></returns>
                public DataTableReader GetStudentDetails(int idno)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        dtr = objSH.ExecuteDataSetSP("PKG_PREREGIST_SP_RET_STUDDETAILS", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetStudentDetails-> " + ex.ToString());
                    }
                    return dtr;
                }


                public int AddRegisteredSubjectsBulk(StudentRegist objSR, int Prev_status)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New Registered Subject Details
                        objParams = new SqlParameter[13];

                        objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                        objParams[1] = new SqlParameter("@P_REGNO", objSR.REGNO);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                        objParams[3] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                        objParams[4] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                        objParams[5] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                        objParams[6] = new SqlParameter("@P_ELECTIVES", objSR.ELECTIVE);
                        objParams[7] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                        objParams[8] = new SqlParameter("@P_ACCEPT", objSR.ACEEPTSUB);
                        objParams[9] = new SqlParameter("@P_PREV_STATUS", Prev_status);
                        objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                        objParams[11] = new SqlParameter("@P_CREGIDNO", objSR.UA_NO);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_REGIST_SUBJECTS_BULK", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddRegisteredSubjects-> " + ex.ToString());
                    }

                    return retStatus;

                }

                public DataSet GetRegistTotalStudents(int sessionno, int schemeno, int rdbtn, int regstatus)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_RD", rdbtn);
                        objParams[3] = new SqlParameter("@P_REGSTATUS", regstatus);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PREREGIST_SP_REPORT_COURSE_STATS", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetRegistTotalStudents-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetCourseWiseStudents(int sessionno, int schemeno, int rdbtn, int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_RD", rdbtn);
                        objParams[3] = new SqlParameter("@P_SUBID", subid);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PREREGIST_SP_REPORT_ROLLLIST", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetCourseWiseStudents-> " + ex.ToString());
                    }
                    return ds;
                }
                

                public DataSet GetFailedSubjects(string regno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@REGNO", regno);

                        ds = objSQL.ExecuteDataSetSP("PKG_SUD_HISTORY_SP_GET_FAILSUBJ", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetNewSchemesForRegistration-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetDetainedSubjects(string regno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@REGNO", regno);

                        ds = objSQL.ExecuteDataSetSP("PKG_SUD_HISTORY_SP_GET_DETSUBJ", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetNewSchemesForRegistration-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSubjectHistory(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@IDNO", idno);

                        ds = objSQL.ExecuteDataSetSP("PKG_SUD_HISTORY_SP_GET_PASSSUBJ", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetNewSchemesForRegistration-> " + ex.ToString());
                    }
                    return ds;
                }
                                

                public bool CheckCourseRegistered(int idno, int sessionno, int schemeno,int courseno)
                {
                    bool ret = false;
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[3] = new SqlParameter("@P_IDNO", idno);

                        object rt = objSH.ExecuteScalarSP("PKG_PREREGIST_SP_CHK_COURSREGISTERD", objParams);
                        if (rt == null || Convert.ToInt32(rt) == 0)
                            ret = false;
                        else
                            ret = true;

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.CheckCourseRegistered-> " + ex.ToString());
                    }
                    return ret;
                }

                public int AddAddlRegisteredSubjects(StudentRegist objSR)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New Registered Subject Details
                        objParams = new SqlParameter[11];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                        objParams[1] = new SqlParameter("@P_IDNO", objSR.IDNO);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);          
                        objParams[3] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                        objParams[4] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                        objParams[5] = new SqlParameter("@P_REGNO", objSR.REGNO);
                        objParams[6] = new SqlParameter("@P_ROLLNO", objSR.ROLLNO);
                        objParams[7] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                        objParams[8] = new SqlParameter("@P_UA_N0", objSR.UA_NO);
                        objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_REGIST_SUBJECTS", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddAddlRegisteredSubjects-> " + ex.ToString());
                    }

                    return retStatus;

                }
                
                public int CountCourseRegistered(int sessionno, int schemeno,int idno)
                {
                    int count = 0;
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_IDNO", idno);

                        object rt = objSH.ExecuteScalarSP("PKG_PREREGIST_SP_COUNT_COURSREGISTERED", objParams);
                        count = Convert.ToInt32(rt);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.CountCourseRegistered-> " + ex.ToString());
                    }
                    return count;
                }

               
                public DataSet GetFailedSubjects(int idno, int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);

                        ds = objSQL.ExecuteDataSetSP("PKG_STUD_SP_RET_FAIL_COURSES", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetNewSchemesForRegistration-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetRegisteredSubjects(int idno, int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);

                        ds = objSQL.ExecuteDataSetSP("PKG_STUD_SP_RET_REGISTERED_COURSES", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetNewSchemesForRegistration-> " + ex.ToString());
                    }
                    return ds;
                }



               public int ExamRegistationRegularSubjects(StudentRegist objSR)
                 {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add Fail Subject Details
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_IDNO",objSR.IDNO);
                        objParams[1] = new SqlParameter("@P_SESSIONNO",objSR.SESSIONNO);
                        objParams[2] = new SqlParameter("@P_SCHEMENO",objSR.SCHEMENO);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO",objSR.SEMESTERNO);
                        objParams[4] = new SqlParameter("@P_COURSENO",objSR.COURSENO);
                        objParams[5] = new SqlParameter("@P_IPADDRESS",objSR.IPADDRESS);
                        objParams[6] = new SqlParameter("@P_EXAM_REGISTERED",objSR.EXAM_REGISTERED);
                        objParams[7] = new SqlParameter("@P_S1IND",objSR.S1IND);
                        objParams[8] = new SqlParameter("@P_S2IND",objSR.S2IND);
                        objParams[9] = new SqlParameter("@P_S3IND",objSR.S3IND);
                        objParams[10] = new SqlParameter("@P_S4IND",objSR.S4IND);
                        objParams[11] = new SqlParameter("@P_OUT",SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_REGISTATION_REGULAR_SUBJECTS", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddFailSubjects-> " + ex.ToString());
                    }

                    return retStatus;

                 }

               #region Pre-Admission
               public int AddRegisteredSubjectsThirdSem(StudentRegist objSR, int Prev_status, int seatno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New Registered Subject Details
                        objParams = new SqlParameter[13];

                        objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                        objParams[1] = new SqlParameter("@P_REGNO", objSR.REGNO);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                        objParams[3] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                        objParams[4] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                        objParams[5] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                        objParams[6] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                        objParams[7] = new SqlParameter("@P_ACCEPT", objSR.ACEEPTSUB);
                        objParams[8] = new SqlParameter("@P_PREV_STATUS", Prev_status);
                        objParams[9] = new SqlParameter("@P_SEATNO", seatno);
                        objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                        objParams[11] = new SqlParameter("@P_GDPOINT", objSR.GDPOINT);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_THIRD_SEM_SUBJECTS", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddRegisteredSubjects-> " + ex.ToString());
                    }

                    return retStatus;

                }

                public string PreAdmissionRegistration(int sessionno, long idno, string studname, string fathername, string mothername,
                   string lastname, string gender, DateTime dob, int mtongueno, int pcity, string ptelephonestd,
                   string ptelephone, string pmobile, string emailid, int stateno, int caste, int categoryno, int nationalityno, int minority,
                   int qualifyno, int ssc_maths, int ssc_maths_max, decimal ssc_maths_per, int ssc_total, int ssc_outofmarks,
                   int mhcet_score, int mhcet_maths_score, int mhcet_physics_score,
                   int hsc_maths, int hsc_maths_max, int hsc_chem, int hsc_chem_max, int hsc_phy, int hsc_phy_max, int hsc_pcm, int hsc_pcm_max,
                   decimal per, int hsc_total, int hsc_outofmarks, int aieee_score, int aieee_rank, int aieee_rollno, string branch_pref, DateTime REG_ENTRY_DATE)
                {
                    //CustomStatus cs = CustomStatus.Error;
                    string retStatus = string.Empty;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_STUDNAME", studname),
                            new SqlParameter("@P_MOTHERNAME", mothername),
                            new SqlParameter("@P_FATHERNAME", fathername),
                            new SqlParameter("@P_LASTNAME", lastname),
                            new SqlParameter("@P_GENDER", gender),
                            new SqlParameter("@P_DOB", dob),
                            new SqlParameter("@P_MTONGUENO", mtongueno),
                            new SqlParameter("@P_PCITY", pcity),
                            new SqlParameter("@P_PTELEPHONESTD", ptelephonestd),
                            new SqlParameter("@P_PTELEPHONE", ptelephone),
                            new SqlParameter("@P_PMOBILE", pmobile),
                            new SqlParameter("@P_EMAILID", emailid),
                            new SqlParameter("@P_STATENO", stateno),
                            new SqlParameter("@P_CASTE", caste),
                            new SqlParameter("@P_CATEGORYNO", categoryno),
                            new SqlParameter("@P_NATIONALITYNO", nationalityno),
                            new SqlParameter("@P_MINORITY", minority),
                            new SqlParameter("@P_QUALIFYNO", qualifyno),
                            new SqlParameter("@P_SSC_MATHS", ssc_maths),
                            new SqlParameter("@P_SSC_MATHS_MAX", ssc_maths_max),
                            new SqlParameter("@P_SSC_MATHS_PER", ssc_maths_per),
                            new SqlParameter("@P_SSC_TOTAL", ssc_total),
                            new SqlParameter("@P_SSC_OUTOF", ssc_outofmarks),
                            new SqlParameter("@P_MHCET_SCORE", mhcet_score),
                            new SqlParameter("@P_MHCET_MATHS_SCORE", mhcet_maths_score),
                            new SqlParameter("@P_MHCET_PHYSICS_SCORE", mhcet_physics_score),
                            new SqlParameter("@P_HSC_MAT", hsc_maths),
                            new SqlParameter("@P_HSC_MAT_MAX", hsc_maths_max),
                            new SqlParameter("@P_HSC_CHE", hsc_chem),
                            new SqlParameter("@P_HSC_CHE_MAX", hsc_chem_max),
                            new SqlParameter("@P_HSC_PHY", hsc_phy),
                            new SqlParameter("@P_HSC_PHY_MAX", hsc_phy_max),
                            new SqlParameter("@P_HSC_PCM", hsc_pcm),
                            new SqlParameter("@P_HSC_PCM_MAX", hsc_pcm_max),
                            new SqlParameter("@P_PER", per),
                            new SqlParameter("@P_HSC_TOTAL", hsc_total),
                            new SqlParameter("@P_HSC_OUTOF", hsc_outofmarks),
                            new SqlParameter("@P_AIEEE_SCORE", aieee_score),
                            new SqlParameter("@P_AIEEE_RANK", aieee_rank),
                            new SqlParameter("@P_AIEEE_ROLLNO", aieee_rollno),
                            new SqlParameter("@P_BRANCH_PREF", branch_pref),
                            //new SqlParameter("@P_REG_ENTRY_DATE", REG_ENTRY_DATE),
                            new SqlParameter("@P_IDNO", idno),
                            
                        };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                        sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.BigInt;
                        object ret = objDataAccess.ExecuteNonQuerySP("PKG_ACAD_INS_REGISTRATION", sqlParams, true);

                        //if (Convert.ToInt32(ret) == -99)
                        //    cs = CustomStatus.TransactionFailed;
                        //else
                        //    cs = CustomStatus.RecordSaved;
                        //cs = ret.ToString();
                        retStatus = ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.StudentRegistration.PreAdmissionRegistration() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return retStatus;
                }

                public DataSet GetMeritList(int sessionno, int aieee_mhcet, int scorefrom, int scoreto, int hsc_pcm, int minority, int combined, string fromdate, string todate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_AIEEE_MHCET", aieee_mhcet);
                        objParams[2] = new SqlParameter("@P_SCOREFROM", scorefrom);
                        objParams[3] = new SqlParameter("@P_SCORETO ", scoreto);
                        objParams[4] = new SqlParameter("@P_HSC_PCM", hsc_pcm);
                        objParams[5] = new SqlParameter("@P_MINORITY", minority);
                        objParams[6] = new SqlParameter("@P_COMBINED", combined);
                        objParams[7] = new SqlParameter("@P_FROMDATE", fromdate);
                        objParams[8] = new SqlParameter("@P_TODATE", todate);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_MERIT_LIST", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetMeritList-> " + ex.ToString());
                    }
                    return ds;
                }

                public CustomStatus GenerateMeritList(int sessionno, int aieee_mhcet, int minority, int combined, string fromdate, string todate)
                {
                    CustomStatus cs = CustomStatus.Others;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_AIEEE_MHCET", aieee_mhcet);
                        objParams[2] = new SqlParameter("@P_MINORITY", minority);
                        objParams[3] = new SqlParameter("@P_COMBINED", combined);
                        objParams[4] = new SqlParameter("@P_FROMDATE", fromdate);
                        objParams[5] = new SqlParameter("@P_TODATE", todate);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_GENERATE_MERIT_LIST", objParams, false);
                        if (ret != null)
                            cs = CustomStatus.RecordSaved;
                        else
                            cs = CustomStatus.Error;

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GenerateMeritList-> " + ex.ToString());
                    }
                    return cs;
                }

                public CustomStatus AllotBranch(int sessionno, long idno, int branchno, int roundno, int batchno, int paytypeno, int idtypeno)
                {
                    CustomStatus cs = CustomStatus.Error;
                    //string retStatus = string.Empty;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_IDNO", idno),
                            new SqlParameter("@P_BRANCHNO", branchno),
                            new SqlParameter("@P_ROUNDNO", roundno),
                            new SqlParameter("@P_ADMBATCH",batchno),
                            new SqlParameter("@P_PTYPE",paytypeno),
                            new SqlParameter("@P_IDTYPE",idtypeno),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                        sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.Int;
                        object ret = objDataAccess.ExecuteNonQuerySP("PKG_ACAD_ALLOT_BRANCH", sqlParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            cs = CustomStatus.TransactionFailed;
                        else
                            cs = CustomStatus.RecordSaved;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.StudentRegistration.UpdateDocumentVerfication() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return cs;
                }

                public DataSet GetBranchPreferences(int sessionno, long idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_REPORT_PROCESS_FORM_BRANCH_PREF", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetBranchPreferences-> " + ex.ToString());
                    }
                    return ds;
                }
               #endregion

                public int UpdatePaymentCategory(string idno, string ptype, string semesterno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_PTYPE", ptype);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);  
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_UPD_PAYMENTTYPE", objParams, false);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddFailSubjects-> " + ex.ToString());
                    }

                    return retStatus;

                }
                public CustomStatus GenereateRegistrationNo(int admbatch,int cont)
                {
                    CustomStatus cs = CustomStatus.Error;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                           
                            new SqlParameter("@P_ADMBATCH", admbatch),
                          
                            new SqlParameter("@P_CONT", cont)
                        };

                        object ret = objDataAccess.ExecuteNonQuerySP("PKG_ACAD_BULK_REGNO_GENERATION", sqlParams, false);

                        cs = CustomStatus.RecordSaved;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.StudentRegistration.GenereateRegistrationNo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return cs;
                }

                public int CheckN4Rule(int idno, int sessionno, int semesterno, int degreeno)
                {
                    int ret = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_DEGREENO", degreeno);


                        ret = Convert.ToInt32(objSQLHelper.ExecuteScalarSP("PKG_ACAD_STUD_SEM_DATA", objParams));

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetPreviousSemesterStud-> " + ex.ToString());
                    }

                    return ret;
                }

                //public int CheckN4Rule(int idno,int sessionno,int semesterno)
                //{
                //    int ret = 0;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                //        SqlParameter[] objParams = new SqlParameter[3];
                //        objParams[0] = new SqlParameter("@P_IDNO", idno);
                //        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                //        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);


                //        ret = Convert.ToInt32(objSQLHelper.ExecuteScalarSP("PKG_ACAD_STUD_SEM_DATA", objParams));

                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.GetPreviousSemesterStud-> " + ex.ToString());
                //    }

                //    return ret;
                //}

                public int GenereateEnrollmentNo(int admbatch, int semesterno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                        objParams[1] = new SqlParameter("@P_SEMESTER", semesterno);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_BULK_ENROLLMENT_GENERATION", objParams, false);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddFailSubjects-> " + ex.ToString());
                    }

                    return retStatus;

                }

                public int UpdateSemesterPromotionNo(string idno, int semesterno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPD_PROMOTION_SEMESTRENO", objParams, false);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddFailSubjects-> " + ex.ToString());
                    }

                    return retStatus;

                }

                public int UpdateSemesterProAddPromotionNo(string idno, int semesterno,int sessionno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                        //objParams[3] = new SqlParameter("@P_YEAR_OLD", oldyear);
                        //objParams[4] = new SqlParameter("@P_SEMESTER_OLD", oldsem);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPD_PROMOTION_SEMESTRENO_PROV", objParams, false);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddFailSubjects-> " + ex.ToString());
                    }

                    return retStatus;

                }

                public int UpdateBranchCategory(string idno, string branchno, string admcategoryno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[2] = new SqlParameter("@P_ADMCATEGORYNO", admcategoryno);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_UPD_BRANCHCAT", objParams, false);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddFailSubjects-> " + ex.ToString());
                    }

                    return retStatus;

                }


                //public int AddExamRegisteredSubjects(StudentRegist objSR)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;

                //        //Add New eXAM Registered Subject Details
                //        objParams = new SqlParameter[9];

                //        objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                //        objParams[1] = new SqlParameter("@P_REGNO", objSR.REGNO);
                //        objParams[2] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                //        objParams[3] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                //        objParams[4] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                //        objParams[5] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                //        //objParams[6] = new SqlParameter("@P_PREV_STATUS", Prev_status);
                //        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                //        objParams[7] = new SqlParameter("@P_EXREGIDNO", objSR.UA_NO);
                //        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //        objParams[8].Direction = ParameterDirection.Output;

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE", objParams, true);
                //        if (Convert.ToInt32(ret) == -99)
                //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //        else
                //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
                //    }

                //    return retStatus;

                //}


                public int AddExamRegisteredSubjects(StudentRegist objSR)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New eXAM Registered Subject Details
                        objParams = new SqlParameter[10];

                        objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                        objParams[1] = new SqlParameter("@P_REGNO", objSR.REGNO);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                        objParams[4] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                        objParams[5] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                        objParams[6] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                        //objParams[6] = new SqlParameter("@P_PREV_STATUS", Prev_status);
                        objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                        objParams[8] = new SqlParameter("@P_EXREGIDNO", objSR.UA_NO);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
                    }

                    return retStatus;

                }

                public int AddRevalautionRegisteredSubjects(StudentRegist objSR)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New eXAM Registered Subject Details
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                        objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                        objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                        objParams[5] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_EXREGIDNO", objSR.UA_NO);
                        objParams[8] = new SqlParameter("@P_EXTERMARK", objSR.EXTERMARKS);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE_FOR_REVALUATION", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
                    }

                    return retStatus;

                }

                public int AddRevalautionMarkEntry(StudentRegist objSR)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add revlauation entry
                        objParams = new SqlParameter[14];
                        objParams[0] = new SqlParameter("@P_IDNOS", objSR.IDNOS);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                        objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENO);
                        objParams[4] = new SqlParameter("@P_IPADDRESS_V1", objSR.IPADDRESS);
                        objParams[5] = new SqlParameter("@P_IPADDRESS_V2", objSR.IPADDRESS);
                        objParams[6] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                        objParams[7] = new SqlParameter("@P_UA_NO_V1", objSR.UA_NO);
                        objParams[8] = new SqlParameter("@P_UA_NO_V2", objSR.UA_NO);
                        //valuer1 marks
                        if (objSR.VALUER1_MKS == null)
                            objParams[9] = new SqlParameter("@P_VALUER1_MKS", DBNull.Value);
                        else
                            objParams[9] = new SqlParameter("@P_VALUER1_MKS", objSR.VALUER1_MKS);
                        //valuer2 marks
                        if (objSR.VALUER2_MKS == null)
                            objParams[10] = new SqlParameter("@P_VALUER2_MKS", DBNull.Value);
                        else
                            objParams[10] = new SqlParameter("@P_VALUER2_MKS", objSR.VALUER2_MKS);
                        ////valuer1 marks
                        //if (objSR.VALUER1_MKS == null)
                        //    objParams[11] = new SqlParameter("@P_MARK_DIFFS", DBNull.Value);
                        //else
                        //    objParams[11] = new SqlParameter("@P_MARK_DIFFS", objSR.VALUER1_MKS);
                        //marks diff
                        if (objSR.MARKDIFFS == null)
                            objParams[11] = new SqlParameter("@P_MARK_DIFFS", DBNull.Value);
                        else
                            objParams[11] = new SqlParameter("@P_MARK_DIFFS", objSR.MARKDIFFS);

                        // new marks
                        if (objSR.NEWMARKS == null)
                            objParams[12] = new SqlParameter("@P_NEW_MARKS", DBNull.Value);
                        else
                            objParams[12] = new SqlParameter("@P_NEW_MARKS", objSR.NEWMARKS);


                        //objParams[9] = new SqlParameter("@P_VALUER1_MKS", objSR.VALUER1_MKS);
                        //objParams[10] = new SqlParameter("@P_VALUER2_MKS", objSR.VALUER2_MKS);
                        //objParams[11] = new SqlParameter("@P_MARK_DIFFS", objSR.MARKDIFFS);
                        //objParams[12] = new SqlParameter("@P_NEW_MARKS", objSR.NEWMARKS);
                        objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[13].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_REVALUATION_MARK_ENTRY", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamRegistration-> " + ex.ToString());
                    }

                    return retStatus;

                }

                public int AddAddlRegisteredSubjectsPhd(StudentRegist objSR)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New Registered Subject Details
                        objParams = new SqlParameter[11];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                        objParams[1] = new SqlParameter("@P_IDNO", objSR.IDNO);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                        objParams[4] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                        objParams[5] = new SqlParameter("@P_REGNO", objSR.REGNO);
                        objParams[6] = new SqlParameter("@P_ROLLNO", objSR.ROLLNO);
                        objParams[7] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                        objParams[8] = new SqlParameter("@P_CREDITS", objSR.CREDITS);
                        objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_REGIST_SUBJECTS_PHD", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddAddlRegisteredSubjects-> " + ex.ToString());
                    }

                    return retStatus;

                }
                //for branch change
                public CustomStatus GenereateRegistrationNoBranch(int degreeno, int branchno, int admbatch, int idno)
                {
                    CustomStatus cs = CustomStatus.Error;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_DEGREENO", degreeno),
                            new SqlParameter("@P_BRANCHNO", branchno),
                            new SqlParameter("@P_ADMBATCH", admbatch),
                            new SqlParameter("@P_IDNO", idno),
                        };

                        object ret = objDataAccess.ExecuteNonQuerySP("PKG_ACAD_BULK_REGNO_GENERATION_BRCHANGE_NEW", sqlParams, false);

                        cs = CustomStatus.RecordSaved;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.StudentRegistration.GenereateRegistrationNo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return cs;
                }


                public int AddAddCoursesForPhd(StudentRegist objSR)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New Registered Subject Details
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                        objParams[1] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PHD_COURSE_SP_INS_COURSE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddAddlRegisteredSubjects-> " + ex.ToString());
                    }

                    return retStatus;

                }

                public int GenereateSingleEnrollmentNo(int admbatch, int semesterno, int idno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                        objParams[1] = new SqlParameter("@P_SEMESTER", semesterno);
                        objParams[2] = new SqlParameter("@P_IDNO", idno);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SINGLE_ENROLLMENT_GENERATION", objParams, false);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddFailSubjects-> " + ex.ToString());
                    }

                    return retStatus;

                }

                public int InsertBranchChange(StudentRegist objStudent)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        
                        {
                         new SqlParameter("@P_IDNO", objStudent.IDNO),
                         new SqlParameter("@P_OLD_BRANCH", objStudent.BRANCHNO),
                         new SqlParameter("@P_BR_PREF", objStudent.BRANCH_REF),
                         new SqlParameter("@P_UA_IDNO", objStudent.UA_NO),
                         new SqlParameter("@P_IPADDRESS", objStudent.IPADDRESS),
                         new SqlParameter("@P_COLLEGE_CODE", objStudent.COLLEGE_CODE),
                         new SqlParameter("@P_ABID", status)
                    };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INS_APPLY_BRANCH_CHANGE", sqlParams, true);

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);

                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.InsertBranchChange-> " + ex.ToString());
                    }

                    return status;
                }

                public int AddUpdElectiveSubject(StudentRegist objSR)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        
                        {
                         new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO),
                         new SqlParameter("@P_IDNO", objSR.IDNO),
                         new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO),
                         new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO),
                         new SqlParameter("@P_COURSENOS", objSR.COURSENO),
                         new SqlParameter("@P_SELECTCOURSENOS", objSR.SELECT_COURSE),
                         new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS),
                         new SqlParameter("@P_CREDITS", objSR.CREDITS),
                         new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE),
                         new SqlParameter("@P_UA_NO", objSR.UA_NO),
                         new SqlParameter("@P_OUT", retStatus)
                    };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_STUDENT_INS_UPD_ELECTIVE", sqlParams, true);

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddUpdElectiveSubject-> " + ex.ToString());
                    }

                    return retStatus;


                    //catch (Exception ex)
                    //{
                    //    retStatus = Convert.ToInt32(CustomStatus.Error);
                    //    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddUpdElectiveSubject-> " + ex.ToString());
                    //}

                    //return retStatus;

                }
                // INSERT CONVOCATION CERTIFICATE//27-FEB-2014//UMESH
                public int AddConvocation(StudentRegist objSR, string studnames, string degree, string branch, string regulation_date, string convocation_date, string ipaddress, string deptname, int certno, int Conv_no)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New Registered Subject Details
                        objParams = new SqlParameter[14];
                        //idnos new memeber add in studentregistration.cs page.(27/01/2012)
                        objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNOS);
                        objParams[1] = new SqlParameter("@P_REGNO", objSR.REGNO);
                        objParams[2] = new SqlParameter("@P_STUDENTNAME", studnames);
                        objParams[3] = new SqlParameter("@P_DEGREE", degree);
                        objParams[4] = new SqlParameter("@P_BRANCH", branch);
                        objParams[5] = new SqlParameter("@P_REGULATION_DATE", regulation_date);
                        objParams[6] = new SqlParameter("@P_CONVOCATION_DATE", convocation_date);
                        objParams[7] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                        objParams[8] = new SqlParameter("@P_IPADDRESS", ipaddress);
                        objParams[9] = new SqlParameter("@P_DEPTNAME", deptname);
                        objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                        objParams[11] = new SqlParameter("@P_CERTNO", certno);
                        objParams[12] = new SqlParameter("@P_CONV_NO", Conv_no);
                        objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[13].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_CONVOCATION_CERTIFICATE_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.AddRegisteredSubjects-> " + ex.ToString());
                    }

                    return retStatus;

                }


            }
        }
  
