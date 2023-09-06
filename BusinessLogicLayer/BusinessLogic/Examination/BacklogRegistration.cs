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
            public class BacklogRegistration
            {
                string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


                //public int DeleteCourse(int SessionNo, int SchemeNo, int semesterno, int CourseNo, int idno)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;

                //        //Edit Course
                //        objParams = new SqlParameter[6];
                //        objParams[0] = new SqlParameter("@P_SESSIONNO",SessionNo);
                //        objParams[1] = new SqlParameter("@P_SCHEMENO",SchemeNo);
                //        objParams[2] = new SqlParameter("@P_IDNO", idno);
                //        objParams[3] = new SqlParameter("@P_COURSENO", CourseNo);
                //        objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                //        objParams[5] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                //        objParams[5].Direction = ParameterDirection.Output;
                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSE_SP_DEL_BACKLOGCOURSE", objParams, true);

                //        if (Convert.ToInt32(ret) == -99)
                //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //        else
                //            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.DeleteCourseAllot -> " + ex.ToString());
                //    }

                //    return retStatus;
                //}

                //public int AddCourses(int SESSIONNO, int SCHEMENO, int IDNO, int SEMESTERNO, int COURSENO, string IPADDRESS, int UA_NO)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;

                //        //Add New Registered Subject Details
                //        objParams = new SqlParameter[8];

                //        objParams[0] = new SqlParameter("@P_SESSIONNO", SESSIONNO);
                //        objParams[1] = new SqlParameter("@P_SCHEMENO", SCHEMENO);
                //        objParams[2] = new SqlParameter("@P_IDNO", IDNO);
                //        objParams[3] = new SqlParameter("@P_SEMESTERNO", SEMESTERNO);
                //        objParams[4] = new SqlParameter("@P_COURSENO", COURSENO);
                //        objParams[5] = new SqlParameter("@P_IPADDRESS", IPADDRESS);
                //        objParams[6] = new SqlParameter("@P_CREGIDNO", UA_NO);
                //        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //        objParams[7].Direction = ParameterDirection.Output;

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_BACKLOGCOURSE", objParams, true);
                //        if (Convert.ToInt32(ret) == -99)
                //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //        else
                //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddRegisteredSubjects-> " + ex.ToString());
                //    }

                //    return retStatus;

                //}

                public int AddCourses(int SESSIONNO, int SCHEMENO, int IDNO, int SEMESTERNO, int COURSENO, string IPADDRESS, int UA_NO, int status)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New Registered Subject Details
                        objParams = new SqlParameter[9];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", SESSIONNO);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", SCHEMENO);
                        objParams[2] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", SEMESTERNO);
                        objParams[4] = new SqlParameter("@P_COURSENO", COURSENO);
                        objParams[5] = new SqlParameter("@P_IPADDRESS", IPADDRESS);
                        objParams[6] = new SqlParameter("@P_STATUS", status);
                        objParams[7] = new SqlParameter("@P_CREGIDNO", UA_NO);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PREREGIST_SP_INS_BACKLOGCOURSE", objParams, true);
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
                public int DeleteCourse(int SessionNo, int SchemeNo, int semesterno, int CourseNo, int idno, string IPADDRESS, int UA_NO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Edit Course
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", SchemeNo);
                        objParams[2] = new SqlParameter("@P_IDNO", idno);
                        objParams[3] = new SqlParameter("@P_COURSENO", CourseNo);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[5] = new SqlParameter("@P_CREGIDNO", UA_NO);
                        objParams[6] = new SqlParameter("@P_IPADDRESS", IPADDRESS);
                        objParams[7] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSE_SP_DEL_BACKLOGCOURSE", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.DeleteCourseAllot -> " + ex.ToString());
                    }

                    return retStatus;
                }
              
                /// <summary>
                /// Added By Sachin For Revaluation Reg dt on 04052023
                /// </summary>
                /// <param name="objSR"></param>
                /// <param name="App_Type"></param>
                /// <param name="Total_Exter_Marks"></param>
                /// <param name="User_Type"></param>
                /// <returns></returns>
                public int AddPhotoCopyRegisteration_Rcpiper(StudentRegist objSR, string App_Type, string Total_Exter_Marks, int User_Type)    //,string orderid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New eXAM Registered Subject Details
                        objParams = new SqlParameter[14];
                        objParams[0] = new SqlParameter("@P_IDNO", objSR.IDNO);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO);
                        objParams[3] = new SqlParameter("@P_COURSENOS", objSR.COURSENOS);
                        objParams[4] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                        objParams[5] = new SqlParameter("@P_SEMESTERNOS", objSR.SEMESTERNOS);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_UA_NO", objSR.UA_NO);
                        objParams[8] = new SqlParameter("@P_GRADES", objSR.EXTERMARKS);
                        objParams[9] = new SqlParameter("@P_CCODE", objSR.CCODES);
                        objParams[10] = new SqlParameter("@P_APP_TYPE", App_Type);
                        objParams[11] = new SqlParameter("@P_EXTER_MARKS", Total_Exter_Marks);
                        objParams[12] = new SqlParameter("@P_USER_TYPE", User_Type);
                        //objParams[13] = new SqlParameter("@P_ORDERID", orderid);

                        objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[13].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REGIST_SP_INS_EXAM_COURSE_FOR_PHOTOCOPY_RCPIPER", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(ret);
                        else
                            retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AddPhotoCopyRegisteration-> " + ex.ToString());
                    }
                    return retStatus;
                }
            
            }
        }
    }
}
