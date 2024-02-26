using System;
using System.Data;
using System.Web;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
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
            /// <summary>
            /// This CourseController is used to control Course table.
            /// </summary>
           
            public partial class CourseControlleritle 
            {
                string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                public DataSet GetCourseByUaNo(int session, int uano, int utype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_UANO", uano);
                        objParams[2] = new SqlParameter("@P_USERTYPE", utype);
                       

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_COURSES_BYUANO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GetCourseByUaNo-> " + ex.ToString());
                    }
                    return ds;
                }

                //USED FOR DISPLAYING LIST OF CREATED TEST TO THE STUDENT
                public DataSet GetTestAll(int COURSENO, string test_type, int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_COURSENO",COURSENO );
                        objParams[1] = new SqlParameter("@P_TEST_TYPE", test_type);
                        objParams[2] = new SqlParameter("@P_IDNO", idno);
                        //objParams = new SqlParameter[3];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_GET_TEST_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.CourseCntroller.GetCourseByUaNo-> " + ex.ToString());
                    }
                    return ds;
                }


                //USED FOR DISPLAYING NOTIFICATIONS OF CREATED TEST TO THE STUDENT
                public DataSet GetTestNotifications(int IDNO, int ua_type)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_USER_TYPE", ua_type);
                        //objParams = new SqlParameter[3];
                        
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_GET_TEST_NOTIFICATIONS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.CourseCntroller.GetCourseByUaNo-> " + ex.ToString());
                    }
                    return ds;
                }

                #region For Test

                public int Update_User_Attempts(int id, int courseno, int testno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Update Student Attempt Info
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", id);
                        objParams[1] = new SqlParameter("@P_TESTNO", testno);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseno);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_UPDATE_STUD_ATTEMPTS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.User_AccController.Update_ITLE_User -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetTestInfo(int userno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_USERNO", userno);
                        //objParams = new SqlParameter[3];


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_FETCH_STUD_INFO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.CourseCntroller.GetTestInfo-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion
            
            
                //FOR IP ADDRESS ENTRY

                public int IAaddressEntry(ILibrary objlib)
                {
                    int status = 0;
                    try
                    {
                        
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_COMPUTERNAME", objlib.Computername);
                        objParams[1] = new SqlParameter("@P_IPADDRESS", objlib.Ipaddress);
                        objParams[2] = new SqlParameter("@P_ID", objlib.Id);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_IPADDRESS_INSERT", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                status = 0;
                            else
                                status = Convert.ToInt32(ret.ToString());
                        }
                        else
                            status = 0;

                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.Lib_ConfigControl.UpdateConfig-> " + ee.ToString());
                    }
                    return status;

                }


                public int deleteipaddressentry(int ip_no)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_ID", ip_no);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_IPADDRESS_DELETE", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                status = 0;
                            else
                                status = Convert.ToInt32(ret.ToString());
                        }
                        else
                            status = 0;
                        
                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.Lib_ConfigControl.UpdateConfig-> " + ee.ToString());
                    }
                    return status;
                    
                }


                public DataTableReader GetSingleIP(int ip_no)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IP_NO", ip_no);


                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_SINGLE_IP_ADDRESS", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseControlleritle.GetSingleIP -> " + ex.ToString());
                    }
                    return dtr;
                }


                //STUDENT LOG HISTORY MAINTAINED 24/03/2014

                public int AddLogHistory(int idno,int pageid, int courseno)
                {
                    int flag = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        
                     
                        {
                            //Add New Assignment
                            objParams = new SqlParameter[3];
                            objParams[0] = new SqlParameter("@P_ID_NO", idno);
                          objParams[1] = new SqlParameter("@P_PAGEID", pageid);
                            objParams[2] = new SqlParameter("@P_COURSENO", courseno);

                            //objParams[12] = new SqlParameter("@P_AS_NO", SqlDbType.Int);
                            //objParams[12].Direction = ParameterDirection.Output;

                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_SP_INS_LOG_HISTORY", objParams, true);
                            if (Convert.ToInt32(ret) == -99)
                                flag = 0;
                            else flag = 1;

                        }
                    }
                    catch (Exception ex)
                    {
                        
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AssignmentController.AddAssignment-> " + ex.ToString());
                    }

                    return flag;
                }                       

              // END LOG HISTORY


                //USED FOR USER PROFILE

                public DataTableReader GetStudInfo(string idno,int sessionno)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ID_NO", idno);
                       
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);


                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_STUDINFO_BY_IDNO", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IAnnouncementController.GetSingleAnnouncement-> " + ex.ToString());
                    }
                    return dtr;
                }



                public DataSet getDataAllowforRetest(int  SESSIONNO ,int IDNO,int USERTYPE  )
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SESSIONNO);
                        objParams[1] = new SqlParameter("@P_UANO", IDNO);
                        objParams[2] = new SqlParameter("@P_USERTYPE", USERTYPE);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_GETDATAT_FOR_ALLOWRETEST", objParams);
                    }                                       
                    catch (Exception ex)                    
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.CourseCntroller.GetTestInfo-> " + ex.ToString());
                    }

                    return ds;
                }

                //ADDED  BY TANU 18_07_2022
                //UPDATE THE SWITCH COUNT OF DESCRIPTIVE TYPE TEST
                public int UpdateMalfunctionSwitchCount(int testNo, int idno, int count, int COURSENO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_TESTNO", testNo);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);
                        objParams[2] = new SqlParameter("@P_MalFunctionCount", count);
                        objParams[3] = new SqlParameter("@P_COURSENO", COURSENO);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_UPDATE_DESC_MALFUNCTIONCOUNT", objParams, true);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IQuestionbankController.UpdateIQuestionBank -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetStudentByCourse(int SESSIONNO, string UA_NO,int COURSENO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SESSIONNO);
                        objParams[1] = new SqlParameter("@P_UANO", UA_NO);
                        objParams[2] = new SqlParameter("@P_COURSENO", COURSENO);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_GET_GetStudentByCourse", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.CourseCntroller.GetTestInfo-> " + ex.ToString());
                    }

                    return ds;
                }

                  //[Start Block] [Parag.O][16-02-2024][53141] JECRC || RFC || Need Excel Report of Assignment Result
                public DataSet GetCourseWiseAssignment(int COURSENO,int UANO)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_COURSENO", COURSENO);
                        objParams[1] = new SqlParameter("@P_UA_NO", UANO);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_GET_COURSEWISE_ASSIGN_REPORT", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    return ds;
                }

                public DataSet GetAssignmentReportForAdmin(int COURSENO, int SESSIONNO,int UANO)
                {
                    DataSet ds;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_COURSE_NO", COURSENO);
                        objParams[1] = new SqlParameter("@P_SESSION_NO", SESSIONNO);
                        objParams[2] = new SqlParameter("@P_UANO", UANO);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_GET_ASSIGNMENT_RESULT_REPORT_ADMIN", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                    return ds;
                }

                //[End Block] [Parag.O][16-02-2024][53141] JECRC || RFC || Need Excel Report of Assignment Result

            }

           
        }//END: BusinessLayer.BusinessLogic

    }//END: NITPRM  

}//END: IITMS
