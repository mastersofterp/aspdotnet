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
            public class ClubController
            {
                string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public int AddClubData(string typeactivity,int Incharge, string email, string regno, int Active, int CreatedBy, string ipAddress, int orgid)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_CLUB_ACTIVITY_TYPE", typeactivity);
                        objParams[1] = new SqlParameter("@P_INCHARGE_NO", Incharge);
                        objParams[2] = new SqlParameter("@P_EMAIL", email);
                        objParams[3] = new SqlParameter("@P_TOTAL_REGNO_LIMIT", regno);
                        objParams[4] = new SqlParameter("@P_ACTIVESTATUS", Active);
                        objParams[5] = new SqlParameter("@P_CREATED_BY", CreatedBy);
                        objParams[6] = new SqlParameter("@P_IPADDRESS", ipAddress);
                        objParams[7] = new SqlParameter("@P_ORGANIZATIONID", orgid);
                        objParams[8] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_INS_CLUB_MASTER", objParams, true);

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                        if (obj.Equals(2627))
                        {
                            status = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddClubData --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public int UpdateClubData(int club_no, string typeactivity,int Incharge,string email, string regno, int Active, int CreatedBy, string ipAddress, int orgid)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_CLUB_ACTIVITY_NO", club_no);
                        objParams[1] = new SqlParameter("@P_CLUB_ACTIVITY_TYPE", typeactivity);
                        objParams[2] = new SqlParameter("@P_INCHARGE_NO", Incharge);
                        objParams[3] = new SqlParameter("@P_EMAIL", email);
                        objParams[4] = new SqlParameter("@P_TOTAL_REGNO_LIMIT", regno);
                        objParams[5] = new SqlParameter("@P_ACTIVESTATUS", Active);
                        objParams[6] = new SqlParameter("@P_CREATED_BY", CreatedBy);
                        objParams[7] = new SqlParameter("@P_IPADDRESS", ipAddress);
                        objParams[8] = new SqlParameter("@P_ORGANIZATIONID", orgid);
                        objParams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_UPDATE_CLUB_MASTER", objParams, true);

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                        //if (obj.Equals(2627))
                        //{
                        //    status = Convert.ToInt32(CustomStatus.RecordExist);
                        //}
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateClubData --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public DataSet GetAllclubdata()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        ds = objSQLHelper.ExecuteDataSet("PKG_SP_GET_CLUB_MASTER");
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetAllclubdata-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetClubActivityByNo(int clubno)
                {

                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_CLUB_NO", clubno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_BY_NO_CLUB_MASTER", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentController.GetClubActivityByNo->" + ex.ToString());
                    }
                    return ds;

                }
                //Club Registration Form
                public int AddClubRegistrationDetails(int idno,string club)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_CLUB_ACTIVITY_NO", club);
                       
                        objParams[2] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_INS_ACD_CLUB_REGISTRATION_DETAILS", objParams, true);

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                        if (obj.Equals(2627))
                        {
                            status = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddClubData --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public DataSet GetShowClubRegistration(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_GET_ALL_ACD_CLUB_REGISTRATION", objParams);
                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BasicDetaisls-> " + ex.ToString());
                    }
                    return ds;
                }

                //club activity 
                public DataSet Getshowstudent(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUDENT_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BasicDetaisls-> " + ex.ToString());
                    }
                    return ds;
                }
                public int AddClubRegistration(int idno, int sessionno, int Subuser, string Title, string Venue, string Fromdate, string Todate, string Duration, string Description, string File)
                {
                    int status = 0;
                    try
                    {

                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_CLUBACTIVITY_TYPE", Subuser);
                        objParams[3] = new SqlParameter("@P_TITLE_OF_EVENT", Title);
                        objParams[4] = new SqlParameter("@P_VENUE", Venue);
                        objParams[5] = new SqlParameter("@P_FROMDATE", Fromdate);
                        objParams[6] = new SqlParameter("@P_TODATE", Todate);
                        objParams[7] = new SqlParameter("@P_DURATION", Duration);
                        objParams[8] = new SqlParameter("@P_DESCRIPTION_OF_EVENT", Description);
                        objParams[9] = new SqlParameter("@P_UPLOAD_DOCUMENT", File);
                        //objParams[10] = new SqlParameter("@P_COLLEGEID", collegeid);
                        objParams[10] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_INS_ACD_CLUB_ACTIVITY_REGISTRATION", objParams, true);

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                        if (obj.Equals(2627))
                        {
                            status = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddClubData --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }


                public int UpdateClubRegistration(int club_no, int Subuser, string Title, string Venue, string Fromdate, string Todate, string Duration, string Description,string File)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_CLUB_NO", club_no);
                        //objParams[1] = new SqlParameter("@P_IDNO", idno);
                        //objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_CLUBACTIVITY_TYPE", Subuser);
                        objParams[2] = new SqlParameter("@P_TITLE_OF_EVENT", Title);
                        objParams[3] = new SqlParameter("@P_VENUE", Venue);
                        objParams[4] = new SqlParameter("@P_FROMDATE", Fromdate);
                        objParams[5] = new SqlParameter("@P_TODATE", Todate);
                        objParams[6] = new SqlParameter("@P_DURATION", Duration);
                        objParams[7] = new SqlParameter("@P_DESCRIPTION_OF_EVENT", Description);
                        objParams[8] = new SqlParameter("@P_UPLOAD_DOCUMENT", File);
                        objParams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_UPD_ACD_CLUB_ACTIVITY_REGISTRATION", objParams, true);

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                        if (obj.Equals(2627))
                        {
                            status = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateClubData --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public DataSet GetClubActivityRegistrationDetails(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_GETDATA_ACD_CLUB_ACTIVITY", objParams);
                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetAllclubdata-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetClubRegistrationDetails(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSet("PKG_SP_GETDATA_ACD_CLUB_ACTIVITY");
                    }
                    catch (Exception ex)
                    {
                       
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetAllclubdata-> " + ex.ToString());
                    }
                    return ds;
                }



                public DataSet GetClubActivityRegByNo(string clubno)
                {

                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_CLUB_NO", clubno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_GETDATA_ACD_CLUB_ACTIVITY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentController.GetClubActivityByNo->" + ex.ToString());
                    }
                    return ds;

                }
                public DataSet GetClubRegistrationDetailsReport(int club, int collegeid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_CLUB_ACTIVITY_NO", club);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", collegeid);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_ACD_CLUB_ACTIVITY_REGISTRATION_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetailsSemesteradmission-> " + ex.ToString());
                    }
                    return ds;
                }
                //public int Club_Registration_approve_students(int idno)
                //{
                //    int retstatus = 0;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objparams = null;
                //        objparams = new SqlParameter[2];

                //        objparams[0] = new SqlParameter("@P_IDNO", idno);
                //        objparams[1] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                //        objparams[1].Direction = ParameterDirection.Output;
                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SP_CLUB_REGISTRATION_APPROVE_STUDENTS", objparams, true);

                //        if (ret.ToString() == "1")
                //            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //        else if (ret.ToString() == "0")
                //            retstatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                //        else
                //            retstatus = Convert.ToInt32(CustomStatus.Error);


                //    }
                //    catch (Exception ex)
                //    {
                //        retstatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ClubController.Club_Registration_approve_students->" + ex.ToString());
                //    }
                //    return retstatus;
                //}

                public int Club_Registration_approve_students(int idno, int UANO, int userType, int stat, int type, int club, int club_activity_no)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[8];
                        objparams[0] = new SqlParameter("@P_UANO", UANO);
                        objparams[1] = new SqlParameter("@P_UA_TYPE", userType);
                        objparams[2] = new SqlParameter("@P_IDNO", idno);
                        objparams[3] = new SqlParameter("@P_APPROVED", stat);
                        objparams[4] = new SqlParameter("@P_CLUBACTIVITY_TYPE", type);
                        objparams[5] = new SqlParameter("@P_CLUB_NO", club);
                        objparams[6] = new SqlParameter("@P_CLUB_ACTIVITY_NO", club_activity_no);
                        objparams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[7].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SP_CLUB_REGISTRATION_APPROVE_STUDENTS", objparams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        if (Convert.ToInt32(ret) == 1)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);


                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ClubController.Club_Registration_approve_students->" + ex.ToString());
                    }
                    return retstatus;
                }


                //public DataSet ClubRegistrationDetailsStudents(int collegeid)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;
                //        objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("@P_COLLEGE_ID", collegeid);

                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_ACD_CLUB_ACTIVITY_REGISTRATION_APPROVE_STUDENTS_DETAILS", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetailsSemesteradmission-> " + ex.ToString());
                //    }
                //    return ds;
                //}

                public DataSet clubCollegesession(int mode, string collegeid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_MODE", mode);
                        objParams[1] = new SqlParameter("@P_COLLEGE_IDNOS", collegeid);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_CONCAT_COLLEGE_SESSION_NEW", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetailsSemesteradmission-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetClubRegistrationApprovalStudentReport(int club,int collegeid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_CLUB_ACTIVITY_TYPE", club);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", collegeid);
                        
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_ACD_CLUB_ACTIVITY_REGISTRATION_APPROVAL_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetailsSemesteradmission-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetClubActivityRegeShowDetails(int clubno)
                {

                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_CLUB_NO", clubno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_GETDATA_UPDATE_ACD_CLUB_ACTIVITY_REGISTRATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentController.GetClubActivityByNo->" + ex.ToString());
                    }
                    return ds;

                }

                //Check Master Table ID reference and If refered then Prevent Master Data Inactive 
                public string CheckReferMasterTable(int MST_TBL_CODE, string MST_FORM_NAME, int COL_ID_VALUE)
                {
                    string retStatus = string.Empty;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {                            
                            new SqlParameter("@P_MST_TBL_CODE", MST_TBL_CODE),
                            new SqlParameter("@P_MST_FORM_NAME", MST_FORM_NAME),
                            new SqlParameter("@P_COL_ID_VALUE", COL_ID_VALUE),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int),
                        };

                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_VALIDATE_ACHIEVEMENT_MASTER", sqlParams, true);

                        retStatus = ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = "-1,\"Exception\"";
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CheckReferMasterTable-> " + ex.ToString());
                    }
                    return retStatus;
                }
              
            }
        }
    }
}
