using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            public class CourseRegistrationApprovebyAdvisorController
            {
                string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public SqlDataReader GetCourseRegistrationStudentData(CourseRegistrationApproveByAdvisorEntity obj)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_SESSION_NO", obj.Sessionno),
                            new SqlParameter("@P_COLLEGE_ID", obj.CollegeId),
                            new SqlParameter("@P_DEGREE_NO", obj.Degreeno),
                            new SqlParameter("@P_BRANCH_NO", obj.Branchno),
                            new SqlParameter("@P_SEMESTER_NO", obj.Semesterno),
                            new SqlParameter("@P_UA_NO", obj.UA_NO)
                        };

                        dr = objDataAccess.ExecuteReaderSP("[DBO].[PKG_STUDENT_COURSE_REGISTRATION_BY_ADVISOR]", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseRegistrationApprovebyAdvisorController.GetCourseRegistrationStudentData() --> " + ex.Message + " " + ex.StackTrace);
                    }

                    return dr;
                }

                public SqlDataReader GetSingleStudentCourseRegistrationDetail(CourseRegistrationApproveByAdvisorEntity obj)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_connectionString);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_SESSION_NO", obj.Sessionno),
                            new SqlParameter("@P_COLLEGE_ID", obj.CollegeId),
                            new SqlParameter("@P_DEGREE_NO", obj.Degreeno),
                            new SqlParameter("@P_BRANCH_NO", obj.Branchno),
                            new SqlParameter("@P_SEMESTER_NO", obj.Semesterno),
                            new SqlParameter("@P_UA_NO", obj.UA_NO),
                            new SqlParameter("@P_IDNO", obj.IDNO)
                        };

                        dr = objDataAccess.ExecuteReaderSP("[DBO].[PKG_SINGLE_STUDENT_COURSE_REGISTRATION_DETAIL]", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseRegistrationApprovebyAdvisorController.GetSingleStudentCourseRegistrationDetail() --> " + ex.Message + " " + ex.StackTrace);
                    }

                    return dr;
                }

                public int InsertUpdate_ApprovalStatus(CourseRegistrationApproveByAdvisorEntity obj)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[10];

                        objParams[0] = new SqlParameter("@P_SESSION_NO", obj.Sessionno);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", obj.CollegeId);
                        objParams[2] = new SqlParameter("@P_DEGREE_NO", obj.Degreeno);
                        objParams[3] = new SqlParameter("@P_BRANCH_NO", obj.Branchno);
                        objParams[4] = new SqlParameter("@P_SEMESTER_NO", obj.Semesterno);
                        objParams[5] = new SqlParameter("@P_UA_NO", obj.UA_NO);
                        objParams[6] = new SqlParameter("@P_IPADDRESS", obj.IPAddress);
                        objParams[7] = new SqlParameter("@BULK_DATA", obj.dtStudCourseReg);
                        objParams[8] = new SqlParameter("@ENTRY_TYPE", obj.EntryType);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SP_INSERT_UPDATE_COURSE_REGISTRATION_BY_ADVISOR_APPROVAL", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseRegistrationApprovebyAdvisorController.InsertUpdate_ApprovalStatus-> " + ex.ToString());
                    }

                    return retStatus;

                }
            }
        }
    }
}