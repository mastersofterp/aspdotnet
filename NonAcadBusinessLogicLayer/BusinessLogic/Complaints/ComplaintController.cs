using System;
using System.Data;
using System.Web;
using IITMS.UAIMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
//using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Data.SqlClient;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class ComplaintController
            {
                /// <summary>
                /// ConnectionString
                /// </summary>
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetComplaintNo(int DeptId, int TYPEID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_DEPTID", DeptId);
                        objParams[1] = new SqlParameter("@P_TYPEID", TYPEID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_REPAIR_MAINTAINANCE_SP_RET_COMPLAINTNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.GetComplaintNo()-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentInfo(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_REPAIR_MAINTAINANCE_GET_STUDENT_INFO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.GetComplaintNo()-> " + ex.ToString());
                    }

                    return ds;
                }

                // Get Scheme, Semester, Photo by user idno
                public SqlDataReader GetShemeSemesterByUser(int ua_idno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_IDNO", ua_idno);
                        dr = objSQLHelper.ExecuteReaderSP("PKG_GET_SCHEME_SEMESTER_BY_USERID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GetShemeSemester-> " + ex.ToString());
                    }
                    return dr;
                }

                public DataSet GetFeedBackQuestionForMaster(Complaint objCEnt)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_CTID", objCEnt.CTID);
                        objParams[1] = new SqlParameter("@P_SUBID", objCEnt.SubId);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_GET_FEEDBACK_QUESTION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.GetFeedBackQuestion-> " + ex.ToString());
                    }
                    return ds;
                }

                //For fet the complaint feedback complaints
                public SqlDataReader GetCourseSelected(int sessionno, int idno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);
                        dr = objSQLHelper.ExecuteReaderSP("PKG_STUDENT_FEEDBACK_COURSE_SELECT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.GetFeedBackQuestion-> " + ex.ToString());
                    }
                    return dr;
                }


                public DataSet GetCOURSESforfeedbacklist(int session, int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_SP_GET_COURSES_FORFEEDBACK_list", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.CourseChangeController.GetCOURSESToBeCHANGED-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }





              //  By sheru for complaint Feedback

                public DataSet GetFeedbackInfo(int userno, string complaint)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_UA_NO", userno);
                        objParams[1] = new SqlParameter("@P_COMPLAINT_NO", complaint);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_REPAIR_MAINTAINANCE_GET_STUDENT_INFO_FEEDBACK", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.GetComplaintNo()-> " + ex.ToString());
                    }

                    return ds;
                }

                //Added by Sheru kumar gaur for cast discrimination form
                public DataSet SearchStud(string rollno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_REGNO", rollno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SEARCH_REPORT", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ComplaintController.SearchStud()-> " + ex.ToString());
                    }
                    return ds;
                }

                public int StudComplaintSubmit(int idno, string Cmobile, string Aname, int Vidno, string Vmobile, DateTime date, string Cdetail, byte[] f1, byte[] f2, string email, string filename1, string filename2)
                {
                    int Status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_CMOBILE", Cmobile);
                        objParams[2] = new SqlParameter("@P_ACCUSED_NAME", Aname);
                        objParams[3] = new SqlParameter("@P_VICTIM_IDNO", Vidno);
                        objParams[4] = new SqlParameter("@P_VICTIM_MOBILE", Vmobile);
                        objParams[5] = new SqlParameter("@P_INCIDENT_DATE", date);
                        objParams[6] = new SqlParameter("@P_COMPLAINT_DETAILS", Cdetail);

                        objParams[7] = new SqlParameter("@P_FILE1", f1);
                        objParams[8] = new SqlParameter("@P_FILE2", f2);
                        objParams[9] = new SqlParameter("@P_FILENAME1", filename1);
                        objParams[10] = new SqlParameter("@P_FILENAME2", filename2);

                        objParams[11] = new SqlParameter("@P_EMAIL", email);

                        objParams[12] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);

                        objParams[12].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_CASTE_DISCRIMINATION_INSERT", objParams, false);
                        Status = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        Status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ComplaintController.StudComplaintSubmit() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return Status;


                }
                public DataSet ShowStud(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_CASTE_DISCRIMINATION_SHOW", objParams);


                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ComplaintController.ShowStud()-> " + ex.ToString());
                    }
                    return ds;
                }
                public int SENDMSG_PASS(string MSG, string MOBILENO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        int ret = 0;
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_MSG", MSG);
                        objParams[1] = new SqlParameter("@P_MOBILENO", MOBILENO);
                        objSQLHelper.ExecuteNonQuerySP("PKG_REPAIR_MAINTAINANCE_SEND_BULKSMS", objParams, true);
                        return ret = 1;

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Con_StudSecurityCheck.SENDMSG_PASS-> " + ex.ToString());
                    }
                }

                // Bu Sheru Gaur for Complaint_FEEDBACK_UPDATE
                public int ComplaintFeedbackUpdate(int uano, string complaint_no, string complaint_deatils, string dept_inch_name, string complaintee_name, string remark, string remark_text)
                {
                    int Status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_UA_NO", uano);
                        objParams[1] = new SqlParameter("@P_COMPLAINT_NO", complaint_no);
                        objParams[2] = new SqlParameter("@P_COMPLAINT_DETAILS", complaint_deatils);
                        objParams[3] = new SqlParameter("@P_DEPT_INCH_NAME",dept_inch_name );
                        objParams[4] = new SqlParameter("@P_COMPLAINTEE_NAME",complaintee_name );
                        objParams[5] = new SqlParameter("@P_REMARK",remark );
                        objParams[6] = new SqlParameter("@P_REMARK_TEXT",remark_text );

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COMPLAINT_FEEDBACK", objParams, false);
                        Status = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        Status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ComplaintController.ComplaintFeedbackUpdate() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return Status;

                }

                //For Complaint Feedback Get Courses

                public DataSet GetCOMPLAINTforfeedback(int session, int uano)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_UA_NO", uano);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_SP_GET_COMPLAINT_FORFEEDBACK", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.CourseChangeController.GetCOURSESToBeCHANGED-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }


                //public int GetComplaintNo(int DeptId)
                //{
                //    int id = 0;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = new SqlParameter[0];                           
                //         new SqlParameter("@P_DEPTID",DeptId);

                //        object ret = objSQLHelper.ExecuteScalarSP("PKG_REPAIR_MAINTAINANCE_SP_RET_COMPLAINTNO", objParams);

                //        if (ret != null)
                //            id = Convert.ToInt32(ret);
                //    }
                //    catch (Exception ex)
                //    {
                //        return id;
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.GetComplaintNo-> " + ex.ToString());
                //    }
                //    return id;
                //}

                public DataSet GetCommitteeUsers()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_REPAIR_MAINTAINANCE_GET_COMMITTEE_USER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.GetCommitteeUsers-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddUpdateUser(Complaint objCT)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New Complaint Item
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_USERNO", objCT.USERNO);
                        objParams[1] = new SqlParameter("@P_EMPDEPTNO", objCT.EMP_DEPT);// emp dept no
                        objParams[2] = new SqlParameter("@P_IDNO", objCT.IDNO);    // emp idno
                        objParams[3] = new SqlParameter("@P_UA_NO", objCT.Ua_No);  // creater ua_no        
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REPAIR_MAINTAINANCE_INSERT_UPDATE_COMMITTEE_USER", objParams, true);
                       
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.AddCI-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public DataSet GetAllComplaints(int userno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_USERNO", userno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_REPAIR_MAINTAINANCE_SP_ALL_COMPLAINTS", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.GetAllComplaints-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetComplaintsCell(int userno, int statusId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_USERNO", userno);
                        objParams[1] = new SqlParameter("@P_STATUSID", statusId);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_REPAIR_MAINTAINANCE_ALL_COMPLAINTS_IN_CELL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.GetAllComplaints-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAllComplaintsUser(int userno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_USERNO", userno);
                        
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_REPAIR_MAINTAINANCE_SP_ALL_COMPLAINTS_USER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.GetAllComplaints-> " + ex.ToString());
                    }
                    return ds;
                }
                 
                public int AddComplaint(Complaint objCT)                   
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try  
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New File
                        objParams = new SqlParameter[20];
                        objParams[0] = new SqlParameter("@P_COMPLAINTNO", objCT.ComplaintNo);
                        objParams[1] = new SqlParameter("@P_COMPLAINTDATE", objCT.ComplaintDate);
                        objParams[2] = new SqlParameter("@P_COMPLAINT", objCT.complaint);
                        objParams[3] = new SqlParameter("@P_COMPLAINTEE_NAME", objCT.Complaintee_Name);
                        objParams[4] = new SqlParameter("@P_COMPLAINTEE_ADDRESS", objCT.Complaintee_Address);
                        objParams[5] = new SqlParameter("@P_COMPLAINTEE_PHONENO", objCT.Complaintee_PhoneNo);
                        objParams[6] = new SqlParameter("@P_COMPLAINTSTATUS", objCT.ComplaintStatus);
                        objParams[7] = new SqlParameter("@P_DEPTID", objCT.Deptid);
                        objParams[8] = new SqlParameter("@P_ALLOTMENTSTATUS", objCT.AllotmentStatus);
                        objParams[9] = new SqlParameter("@P_UA_NO", objCT.Ua_No);
                        objParams[10] = new SqlParameter("@P_Admin_UA_NO",objCT.Admin_UA_no);
                        objParams[11] = new SqlParameter("@P_AREAID", objCT.AreaId);
                        objParams[12] = new SqlParameter("@P_OTHERPHONENO", objCT.Complaintee_OtherPhoneNo);
                        
                        if (objCT.PreferableDate == DateTime.MinValue)
                        {
                            objParams[13] = new SqlParameter("@P_PDATE", DBNull.Value);
                        }
                        else
                        {
                            objParams[13] = new SqlParameter("@P_PDATE", objCT.PreferableDate);
                        }

                        if (objCT.PreferableDate == DateTime.MinValue)
                        {
                            objParams[14] = new SqlParameter("@P_PTIME", DBNull.Value);
                        }
                        else
                        {
                            objParams[14] = new SqlParameter("@P_PTIME", objCT.PreferableTime);
                        }
                        if (objCT.PreferableDate == DateTime.MinValue)
                        {
                            objParams[15] = new SqlParameter("@P_PTIMETO", DBNull.Value);
                        }
                        else
                        {
                            objParams[15] = new SqlParameter("@P_PTIMETO", objCT.PreferableTimeTo);
                        }
                    

                        objParams[16] = new SqlParameter("@P_PWID", objCT.PWID);
                        objParams[17] = new SqlParameter("@P_TYPEID", objCT.TypeId);
                        objParams[18] = new SqlParameter("@P_REMARK", objCT.OPENREMARK);
                        objParams[19] = new SqlParameter("@P_COMPLAINTID",SqlDbType.Int);

                        objParams[19].Direction = ParameterDirection.Output;

                       // if (objSQLHelper.ExecuteNonQuerySP("PKG_REPAIR_MAINTAINANCE_SP_INS_COMPLAINT", objParams, false) != null)
                           // retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                     
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REPAIR_MAINTAINANCE_SP_INS_COMPLAINT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == 2627)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else
                        {
                            objCT.ComplaintId = Convert.ToInt32(ret);
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.AddComplaint-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public int UpdateComplaint(Complaint objCT)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New File
                        objParams = new SqlParameter[16];
                        objParams[0] = new SqlParameter("@P_COMPLAINTID", objCT.ComplaintId);                       
                        objParams[1] = new SqlParameter("@P_COMPLAINT", objCT.complaint);
                        objParams[2] = new SqlParameter("@P_COMPLAINTEE_NAME", objCT.Complaintee_Name);
                        objParams[3] = new SqlParameter("@P_COMPLAINTEE_ADDRESS", objCT.Complaintee_Address);
                        objParams[4] = new SqlParameter("@P_COMPLAINTEE_PHONENO", objCT.Complaintee_PhoneNo);
                        objParams[5] = new SqlParameter("@P_COMPLAINTSTATUS", objCT.ComplaintStatus);
                        objParams[6] = new SqlParameter("@P_DEPTID", objCT.Deptid);
                        objParams[7] = new SqlParameter("@P_ALLOTMENTSTATUS", objCT.AllotmentStatus);
                        objParams[8] = new SqlParameter("@P_UA_NO", objCT.Ua_No);
                        objParams[9] = new SqlParameter("@P_Admin_UA_NO", objCT.Admin_UA_no);
                        objParams[10] = new SqlParameter("@P_AREAID", objCT.AreaId);
                        objParams[11] = new SqlParameter("@P_OTHERPHONENO", objCT.Complaintee_OtherPhoneNo);
                        objParams[12] = new SqlParameter("@P_PWID", objCT.PWID);
                        objParams[13] = new SqlParameter("@P_TYPEID", objCT.TypeId);
                        objParams[14] = new SqlParameter("@P_REMARK", objCT.OPENREMARK);
                        objParams[15] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[15].Direction = ParameterDirection.Output;

                        // if (objSQLHelper.ExecuteNonQuerySP("PKG_REPAIR_MAINTAINANCE_SP_INS_COMPLAINT", objParams, false) != null)
                        // retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REPAIR_MAINTAINANCE_SP_UPDATE_COMPLAINT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if(Convert.ToInt32(ret) == 2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                        {
                            objCT.ComplaintId = Convert.ToInt32(ret);
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.AddComplaint-> " + ex.ToString());
                    }
                    return retStatus;
                }


                // This method is used to get login credentails for sending mail.
                public DataSet GetUserData(int ComplaintId, int UserType)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_COMPLAINTID", ComplaintId);
                        objParams[1] = new SqlParameter("@P_USER_TYPE", UserType);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_REPAIR_MAINTAINANCE_GET_CREDENTIALS_FOR_EMAIL_SERVICE_PROVIDER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ComplaintController.GetUserData-> " + ex.ToString());
                    }
                    return ds;
                }

                public void SendComplaintSMS(string MSG, string MOBILENO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_MSG", MSG);
                        objParams[1] = new SqlParameter("@P_MOBILENO", MOBILENO);
                        objSQLHelper.ExecuteNonQuerySP("PKG_REPAIR_MAINTAINANCE_SEND_BULKSMS", objParams, true);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ComplaintController.SendComplaintSMS-> " + ex.ToString());
                    }
                }


                public DataSet GetAllottedUserData(int ComplaintId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COMPLAINTID", ComplaintId);                       
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_REPAIR_MAINTAINANCE_GET_ALLOTTED_USER_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ComplaintController.GetAllottedUserData-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetCompletedComplaints(int ComplaintId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COMPLAINTID", ComplaintId);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_REPAIR_MAINTAINANCE_SP_WORKEDOUT_COMPLAINTS", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.GetCompletedComplaints-> " + ex.ToString());
                    }
                    return ds;
                }
              

                public int AddComplaintAllotmentByCell(Complaint objCT)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New File  
                        objParams = new SqlParameter[16];
                        objParams[0] = new SqlParameter("@P_CELLALLOTMENTID", objCT.CELLALLOTMENTID);
                        objParams[1] = new SqlParameter("@P_COMPLAINTID", objCT.ComplaintId);
                        objParams[2] = new SqlParameter("@P_COMPLAINTALLOTDATE", objCT.ComplaintDate);  
                        objParams[3] = new SqlParameter("@P_DEPTID", objCT.Deptid);
                        objParams[4] = new SqlParameter("@P_EMPID", objCT.Ua_No);
                        objParams[5] = new SqlParameter("@P_USERNO", objCT.Admin_UA_no);
                        objParams[6] = new SqlParameter("@P_REMARK", objCT.remark);
                        objParams[7] = new SqlParameter("@P_USER_CATEGORY", objCT.USER_CATEGORY);
                        objParams[8] = new SqlParameter("@P_ALLOT_TO_NAME", objCT.ALLOT_TO_NAME);
                        objParams[9] = new SqlParameter("@P_PWID", objCT.PWID);

                        if (objCT.PreferableDate == DateTime.MinValue)
                        {
                            objParams[10] = new SqlParameter("@P_PDATE", DBNull.Value);
                        }
                        else
                        {
                            objParams[10] = new SqlParameter("@P_PDATE", objCT.PreferableDate);
                        }

                        if (objCT.PreferableTime == DateTime.MinValue)
                        {
                            objParams[11] = new SqlParameter("@P_PTIME", DBNull.Value);
                        }
                        else
                        {
                            objParams[11] = new SqlParameter("@P_PTIME", objCT.PreferableTime);
                        }
                        if (objCT.PreferableTimeTo == DateTime.MinValue)
                        {
                            objParams[12] = new SqlParameter("@P_PTIMETO", DBNull.Value);
                        }
                        else
                        {
                            objParams[12] = new SqlParameter("@P_PTIMETO", objCT.PreferableTimeTo);
                        }                      
                        objParams[13] = new SqlParameter("@P_COMPLAINTSTATUS", objCT.ComplaintStatus);
                        objParams[14] = new SqlParameter("@P_SUGGESTION", objCT.SUGGESTION);
                        objParams[15] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[15].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_REPAIR_MAINTAINANCE_SP_ALLOTMENT_BY_COMPLAINT_CELL", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.AddComplaint-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetAllComplaintType(int deptid)
                {
                    DataSet dsCT = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DEPTID", deptid);

                        dsCT = objSQLHelper.ExecuteDataSetSP("PKG_REPAIR_MAINTAINANCE_SP_ALL_COMPLAINT_TYPE", objParams);

                    }
                    catch (Exception ex)
                    {
                        return dsCT;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.GetAllComplaintType-> " + ex.ToString());
                    }

                    return dsCT;
                }

                /// <summary>
                /// This method adds a new record in the Complaint_Type Table
                /// </summary>
                /// <param name="objComplaintType">objComplaintType is the object of Complaint_Type class.</param>
                /// <returns>Integer CustomStatus - RecordSaved or Error</returns>
                public int AddComplaintType(Complaint objComplaintType)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New Complaint Type
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_TYPENAME", objComplaintType.Type_Name);
                        objParams[1] = new SqlParameter("@P_DEPTID", objComplaintType.Deptid);
                        objParams[2] = new SqlParameter("@P_TYPE_CODE", objComplaintType.Type_Code);
                        objParams[3] = new SqlParameter("@P_TYPEID", objComplaintType.TypeId);
                        objParams[3].Direction = ParameterDirection.InputOutput;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_REPAIR_MAINTAINANCE_SP_INS_COMPLAINT_TYPE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.AddCT-> " + ex.ToString());
                    }

                    return retStatus;
                }

                /// <summary>
                /// This method updates a record in complaint_type Table
                /// </summary>
                /// <param name="objcomplaint_type">objcomplaint_type is the object of complaint_type class.</param>
                /// <returns>Integer CustomStatus - RecordUpdated or Error</returns>
                public int UpdateComplaintType(Complaint objComplaintType)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Update complaint type
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_TYPENAME", objComplaintType.Type_Name);
                        objParams[1] = new SqlParameter("@P_DEPTID", objComplaintType.Deptid);
                        objParams[2] = new SqlParameter("@P_TYPEID", objComplaintType.TypeId);
                        objParams[3] = new SqlParameter("@P_TYPE_CODE", objComplaintType.Type_Code);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_REPAIR_MAINTAINANCE_SP_UPD_COMPLAINT_TYPE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.UpdateCT-> " + ex.ToString());
                    }

                    return retStatus;
                }

                /// <summary>
                /// This method is used to retrieve single complaint from complaint_type table.
                /// </summary>
                /// <param name="newsid">Retrieve single complaint as per this deptid.</param>
                /// <returns>SqlDataReader</returns>
                public SqlDataReader GetSingleComplaintType(int typeid)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TYPEID", typeid);
                        
                        dr = objSQLHelper.ExecuteReaderSP("PKG_REPAIR_MAINTAINANCE_SP_RET_COMPLAINT_TYPE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.GetSingleComplaintType-> " + ex.ToString());
                    }
                    return dr;
                }
                
                /// <summary>
                /// This method gets all records from Table
                /// Table Use : complaint_master
                /// </summary>
                /// <returns>Dataset</returns>
                public DataSet GetAllComplaintItem(int deptid)
                {
                    DataSet dsCI = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DEPTID", deptid);

                        dsCI = objSQLHelper.ExecuteDataSetSP("PKG_REPAIR_MAINTAINANCE_SP_ALL_COMPLAINT_ITEMMASTER", objParams);

                    }
                    catch (Exception ex)
                    {
                        return dsCI;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.GetAllComplaintItem-> " + ex.ToString());
                    }

                    return dsCI;
                }

                /// <summary>
                /// This method adds a new record in the Complaint_item Table
                /// </summary>
                /// <param name="objComplaintItem">objComplaintItem is the object of Complaint class.</param>
                /// <returns>Integer CustomStatus - RecordSaved or Error</returns>
                public int AddItemMaster(Complaint objComplaintItem)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New Complaint Item
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_ITEMCODE", objComplaintItem.ItemCode);
                        objParams[1] = new SqlParameter("@P_ITEMNAME", objComplaintItem.ItemName);
                        objParams[2] = new SqlParameter("@P_ITEMUNIT", objComplaintItem.ItemUnit);
                        objParams[3] = new SqlParameter("@P_MAXSTOCK", objComplaintItem.MaxStock);
                        objParams[4] = new SqlParameter("@P_MINSTOCK", objComplaintItem.MinStock);
                        objParams[5] = new SqlParameter("@P_CURRSTOCK", objComplaintItem.CurrStock);
                        objParams[6] = new SqlParameter("@P_DEPTID", objComplaintItem.Deptid);
                        objParams[7] = new SqlParameter("@P_ITEMTYPEID", objComplaintItem.TypeId);
                        objParams[8] = new SqlParameter("@P_ITEMID", objComplaintItem.ItemId);
                        objParams[8].Direction = ParameterDirection.InputOutput;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_REPAIR_MAINTAINANCE_SP_INS_COMPLAINT_ITEMMASTER", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.AddItemMaster-> " + ex.ToString());
                    }

                    return retStatus;
                }

                /// <summary>
                /// This method updates a record in complaint_itemmaster Table
                /// </summary>
                /// <param name="objcomplaint_item">objcomplaint_item is the object of complaint class.</param>
                /// <returns>Integer CustomStatus - RecordUpdated or Error</returns>
                public int UpdateItemMaster(Complaint objComplaintItem)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Update complaint item master
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_ITEMCODE", objComplaintItem.ItemCode);
                        objParams[1] = new SqlParameter("@P_ITEMNAME", objComplaintItem.ItemName);
                        objParams[2] = new SqlParameter("@P_ITEMUNIT", objComplaintItem.ItemUnit);
                        objParams[3] = new SqlParameter("@P_MAXSTOCK", objComplaintItem.MaxStock);
                        objParams[4] = new SqlParameter("@P_MINSTOCK", objComplaintItem.MinStock);
                        objParams[5] = new SqlParameter("@P_CURRSTOCK", objComplaintItem.CurrStock);
                        objParams[6] = new SqlParameter("@P_DEPTID", objComplaintItem.Deptid);
                        objParams[7] = new SqlParameter("@P_ITEMTYPEID", objComplaintItem.TypeId);
                        objParams[8] = new SqlParameter("@P_ITEMID", objComplaintItem.ItemId);
                        
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_REPAIR_MAINTAINANCE_SP_UPD_COMPLAINT_ITEMMASTER", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.UpdateItemMaster-> " + ex.ToString());
                    }

                    return retStatus;
                }


                 // complaint itemorder
              
                /// <summary>
                /// This method gets all records from Table
                /// Table Use : complaint_itemorder
                /// </summary>
                /// <returns>Dataset</returns>
                public DataSet GetAllItemOrder(int deptid)
                {
                    DataSet dsItem = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DEPTID", deptid);

                        dsItem = objSQLHelper.ExecuteDataSetSP("PKG_REPAIR_MAINTAINANCE_SP_ALL_COMPLAINT_ITEMORDER", objParams);

                    }
                    catch (Exception ex)
                    {
                        return dsItem;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.GetAllComplaintItem-> " + ex.ToString());
                    }

                    return dsItem;
                }

                /// <summary>
                /// This method adds a new record in the Complaint_item Table
                /// </summary>
                /// <param name="objComplaintItem">objComplaintItem is the object of Complaint class.</param>
                /// <returns>Integer CustomStatus - RecordSaved or Error</returns>
                public int AddItemOrder(Complaint objItemOrder)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New Complaint Itemorder
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_ORDERDATE", objItemOrder.OrderDate);
                        objParams[1] = new SqlParameter("@P_ITEMID", objItemOrder.ItemId);
                        objParams[2] = new SqlParameter("@P_ORDERQTY", objItemOrder.QtyOrder);
                        objParams[3] = new SqlParameter("@P_DEPTID", objItemOrder.Deptid);
                        objParams[4] = new SqlParameter("@P_ORDERID", objItemOrder.OrderId);
                        objParams[4].Direction = ParameterDirection.InputOutput;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_REPAIR_MAINTAINANCE_SP_INS_COMPLAINT_ITEMORDER", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.AddItemOrder-> " + ex.ToString());
                    }

                    return retStatus;
                }

                /// <summary>
                /// This method updates a record in complaint_itemmaster Table
                /// </summary>
                /// <param name="objcomplaint_item">objcomplaint_item is the object of complaint class.</param>
                /// <returns>Integer CustomStatus - RecordUpdated or Error</returns>
                public int UpdateItemOrder(Complaint objItemOrder)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Update complaint item master
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_ORDERDATE", objItemOrder.OrderDate);
                        objParams[1] = new SqlParameter("@P_ITEMID", objItemOrder.ItemId);
                        objParams[2] = new SqlParameter("@P_ORDERQTY", objItemOrder.QtyOrder);
                        objParams[3] = new SqlParameter("@P_DEPTID", objItemOrder.Deptid);
                        objParams[4] = new SqlParameter("@P_ORDERID", objItemOrder.OrderId);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_REPAIR_MAINTAINANCE_SP_UPD_COMPLAINT_ITEMORDER", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.UpdateItemOrder-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public SqlDataReader GetSingleItemOrder(int itemid)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ITEMID", itemid);
                        
                        dr = objSQLHelper.ExecuteReaderSP("PKG_REPAIR_MAINTAINANCE_SP_RET_COMPLAINT_ITEMMASTER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.GetSingleItemOrder-> " + ex.ToString());
                    }
                    return dr;
                }

                public SqlDataReader GetSingleComplaintItemOrder(int orderid)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ORDERID", orderid);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_REPAIR_MAINTAINANCE_SP_RET_COMPLAINT_ITEMORDER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.GetSingleComplaintItemOrder-> " + ex.ToString());
                    }
                    return dr;
                }


                /// <summary>
                /// This method is used to retrieve single complaint from complaint_itemmaster table.
                /// </summary>
                /// <param name="itemid">Retrieve single complaint as per this itemid.</param>
                /// <returns>SqlDataReader</returns>
                public SqlDataReader GetSingleComplaintItem(int itemid)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ITEMID", itemid);
                        
                        dr = objSQLHelper.ExecuteReaderSP("PKG_REPAIR_MAINTAINANCE_SP_RET_COMPLAINT_ITEMMASTER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.GetSingleComplaintItem-> " + ex.ToString());
                    }
                    return dr;
                }

                public DataSet GetAllCreateUsers()
                {
                    DataSet ds = null;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_REPAIR_MAINTAINANCE_SP_ALL_COMPLAINT_USER", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.GetAllCreateUsers-> " + ex.ToString());
                    }
                    return ds;
                }

                public SqlDataReader GetSingleCreateUsers(int cno)
                {

                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_C_NO ", cno);
                        
                        dr = objSQLHelper.ExecuteReaderSP("PKG_REPAIR_MAINTAINANCE_SP_RET_COMPLAINT_USER", objParams);

                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.GetSingleCreateUsers-> " + ex.ToString());
                    }
                    return dr;

                }

                public int AddComplaintUser(Complaint ComplaintUser)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New Complaint Item
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_C_DEPTNO", ComplaintUser.Deptid);
                        objParams[1] = new SqlParameter("@P_C_UANO", ComplaintUser.Ua_No);
                        objParams[2] = new SqlParameter("@P_C_STATUS", ComplaintUser.C_Status);
                        objParams[3] = new SqlParameter("@P_C_EMPNO ", ComplaintUser.EmpId);
                        objParams[4] = new SqlParameter("@P_C_ACTIVE_STATUS", ComplaintUser.C_ACTIVE_STATUS);
                        objParams[5] = new SqlParameter("@P_C_NO", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_REPAIR_MAINTAINANCE_SP_INS_COMPLAINT_USER", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REPAIR_MAINTAINANCE_SP_INS_COMPLAINT_USER", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.AddCI-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateComplaintUser(Complaint ComplaintUser)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Update complaint item master
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_C_DEPTNO", ComplaintUser.Deptid);
                        objParams[1] = new SqlParameter("@P_C_UANO", ComplaintUser.Ua_No);
                        objParams[2] = new SqlParameter("@P_C_STATUS", ComplaintUser.C_Status);
                        objParams[3] = new SqlParameter("@P_C_EMPNO ", ComplaintUser.EmpId);
                        objParams[4] = new SqlParameter("@P_C_NO", ComplaintUser.C_No);
                        objParams[5] = new SqlParameter("@P_C_ACTIVE_STATUS", ComplaintUser.C_ACTIVE_STATUS);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_REPAIR_MAINTAINANCE_SP_UPD_COMPLAINT_USER", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REPAIR_MAINTAINANCE_SP_UPD_COMPLAINT_USER", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == 2627)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.UpdateItemMaster-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetComplaintNature(int userno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objsqlhelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_USERNO", userno);
                        
                        ds = objsqlhelper.ExecuteDataSetSP("PKG_REPAIR_MAINTAINANCE_SP_DDW_COMPLAINT_TYPE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.ComplaintController.GetComplaintNature-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAllCompaintAllotmentDetails(int userno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objsqlhelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_USERNO", userno);
                        
                        ds = objsqlhelper.ExecuteDataSetSP("PKG_REPAIR_MAINTAINANCE_SP_ALL_COMP_ALLOTMENT_USER", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.ComplaintController.GetAllCompaintAllotmentDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetEmployeeCreateusersCuno(Int32 Deptid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objsqlhelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DEPTID",  Deptid);
                        
                        ds = objsqlhelper.ExecuteDataSetSP("PKG_REPAIR_MAINTAINANCE_SP_DDW_CREATEUSERS_CUANO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.ComplaintController.GetEmployeeCreateusersCuno-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetComplaintTo(Int32 Deptid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objsqlhelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DEPTID", Deptid);
                        
                        ds = objsqlhelper.ExecuteDataSetSP("PKG_REPAIR_MAINTAINANCE_SP_DDW_COMPLAINT_TO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.ComplaintController.GetEmployee-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetEmployeeComplaintUserEno(Int32 userno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objsqlhelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_USERNO", userno);
                        
                        ds = objsqlhelper.ExecuteDataSetSP("PKG_REPAIR_MAINTAINANCE_SP_DDW_CREATEUSER_CEMPNO_USER", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.ComplaintController.GetEmployeeComplaintUser-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddComplaintAllotment(Complaint objComplaintAllotment)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New Complaint Item
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_COMPLAINTID", objComplaintAllotment.ComplaintNo);
                        objParams[1] = new SqlParameter("@P_CTYPEID", objComplaintAllotment.TypeId);
                        objParams[2] = new SqlParameter("@P_EMPID",  objComplaintAllotment.EmpId);
                        objParams[3] = new SqlParameter("@P_DEPTID", objComplaintAllotment.Deptid);
                        objParams[4] = new SqlParameter("@P_COMPALLOTMENTID", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_REPAIR_MAINTAINANCE_SP_INS_COMPLAINT_ALLOTMENT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.AddComplaintAllotment-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public SqlDataReader GetDeptIdName(int userno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_USERNO", userno);
                        
                        dr = objSQLHelper.ExecuteReaderSP("PKG_REPAIR_MAINTAINANCE_SP_RET_DEPTID_DEPTNAME", objParams);

                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.GetSingleCreateUsers-> " + ex.ToString());
                    }
                    return dr;

                }

                public DataSet GetItemNameByComplaintType(Int32 itemid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objsqlhelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ITEMID", itemid);
                        
                        ds = objsqlhelper.ExecuteDataSetSP("PKG_REPAIR_MAINTAINANCE_SP_DDW_ITEMNAME_BYCOMPLAINT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.ComplaintController.GetItemNameByComplaintType-> " + ex.ToString());
                    }
                    return ds;
                }

                public SqlDataReader GetItemUnits(Int32 itemid)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objsqlhelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ITEMID",  itemid);
                        
                        dr = objsqlhelper.ExecuteReaderSP("PKG_REPAIR_MAINTAINANCE_SP_RET_ITEMUNIT_BYITEMID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.ComplaintController.GetItemUnits-> " + ex.ToString());
                    }
                    return dr;
                }

                public int InsertComplaintWorkOut(Complaint objComplaintWorkout)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New Complaint Item
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_COMPLAINTID", objComplaintWorkout.ComplaintId);
                        objParams[1] = new SqlParameter("@P_WORKDATE", objComplaintWorkout.WorkDate);
                        objParams[2] = new SqlParameter("@P_WORKOUT", objComplaintWorkout.WorkOut);
                        objParams[3] = new SqlParameter("@P_EMPID", objComplaintWorkout.EmpId);
                        objParams[4] = new SqlParameter("@P_DEPTID", objComplaintWorkout.Deptid);                        
                        objParams[5] = new SqlParameter("@P_COMPLAINTSTATUS", objComplaintWorkout.C_Status);
                        objParams[6] = new SqlParameter("@P_COMPNATURE_ID", objComplaintWorkout.CompNatureId);
                        objParams[7] = new SqlParameter("@P_ALLOTED_TO", objComplaintWorkout.AllotedTo);
                        objParams[8] = new SqlParameter("@P_ITEMLIST_DT", objComplaintWorkout.ITEMLIST_DT);
                        objParams[9] = new SqlParameter("@P_REWORK", objComplaintWorkout.REWORK);
                        objParams[10] = new SqlParameter("@P_CWID", objComplaintWorkout.CwId);
                        objParams[10].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_REPAIR_MAINTAINANCE_SP_INS_COMPLAINT_WORKOUT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.AddComplaintWorkOut-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int AddComplaintWorkOut(Complaint objComplaintWorkout)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New Complaint Item
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_COMPLAINTID", objComplaintWorkout.ComplaintId);
                        objParams[1] = new SqlParameter("@P_WORKDATE", objComplaintWorkout.WorkDate);
                        objParams[2] = new SqlParameter("@P_WORKOUT", objComplaintWorkout.WorkOut);
                        objParams[3] = new SqlParameter("@P_EMPID", objComplaintWorkout.EmpId);
                        objParams[4] = new SqlParameter("@P_DEPTID", objComplaintWorkout.Deptid);
                        objParams[5] = new SqlParameter("@P_ITEMID", objComplaintWorkout.ItemId);
                        objParams[6] = new SqlParameter("@P_ITEMNAME", objComplaintWorkout.ItemName);
                        objParams[7] = new SqlParameter("@P_ITEMUNIT", objComplaintWorkout.ItemUnit);
                        objParams[8] = new SqlParameter("@P_QTYISSUED", objComplaintWorkout.QtyIssued);
                        objParams[9] = new SqlParameter("@P_COMPLAINTSTATUS", objComplaintWorkout.C_Status);
                        objParams[10] = new SqlParameter("@P_TYPEID", objComplaintWorkout.TypeId);
                        objParams[11] = new SqlParameter("@P_TYPENAME", objComplaintWorkout.Type_Name);
                        objParams[12] = new SqlParameter("@P_CWID", objComplaintWorkout.CwId);
                        objParams[12].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_REPAIR_MAINTAINANCE_SP_INS_COMPLAINT_WORKOUTDTL", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.AddComplaintWorkOut-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetAllWorkoutDetails(Int32 userno, Int32 compno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objsqlhelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_EMPID", userno);
                        objParams[1] = new SqlParameter("@P_COMPLAINTID", compno);
                        
                        ds = objsqlhelper.ExecuteDataSetSP("PKG_REPAIR_MAINTAINANCE_SP_ALL_COMPLAINT_WORKOUTDTL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.ComplaintController.GetAllWorkoutDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public SqlDataReader Getworkout(int compid)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COMPID", compid);
                        
                        dr = objSQLHelper.ExecuteReaderSP("PKG_REPAIR_MAINTAINANCE_SP_RET_COMPLAINT_WORKOUT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.GetSingleItemOrder-> " + ex.ToString());
                    }
                    return dr;
                }
                
                public int GetDeptName(int userno)
                {
                    int deptid = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_USERNO", userno);
                        
                        deptid = Convert.ToInt32(objSQLHelper.ExecuteScalarSP("PKG_REPAIR_MAINTAINANCE_SP_RET_DEPTID_DEPTNAME", objParams));

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.GetSingleCreateUsers-> " + ex.ToString());
                    }
                    return deptid;
                }

                public DataSet GetComplaintType(int deptid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objsqlhelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DEPTID", deptid);

                        ds = objsqlhelper.ExecuteDataSetSP("PKG_REPAIR_MAINTAINANCE_SP_RET_COMPLAINT_TYPE_DEPTID", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.ComplaintController.GetEmployee-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddDepartmentTypeName(Complaint objMat)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                         new SqlParameter("@P_DEPARTMENT_TYPE_NAME",objMat.Department_name),
                         new SqlParameter("@P_DEPARTMENT_CODE",objMat.Department_code),
                           new SqlParameter("@P_FlAG",objMat.Flag),
                         new SqlParameter("@P_OUTPUT", objMat.Dept_Id)
                         
                        };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_REPAIR_MAINTAINANCE_SP_ADD_COMPLAINT_TYPE_DEPTNAME", sqlParams, true);

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DepartmentNameController.AddDepartmentName() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public int DeleteComplaint(int complaintID, int IdNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COMPLAINT_ID", complaintID);

                        objSQLHelper.ExecuteNonQuerySP("PKG_REPAIR_MAINTAINANCE_DELETE_COMPLAINT", objParams, false);
                        retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ComplaintController.DeleteComplaint-> " + ex.ToString());
                    }
                    return Convert.ToInt32(retStatus);
                }


                public int DeleteDepartment(int deptID)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DEPT_ID", deptID);

                        objSQLHelper.ExecuteNonQuerySP("PKG_REPAIR_MAINTAINANCE_DELETE_DEPARTMENT", objParams, false);
                        retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ComplaintController.DeleteDepartment-> " + ex.ToString());
                    }
                    return Convert.ToInt32(retStatus);
                }


                public DataSet GetComplaintToEmployee(Int32 Deptid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objsqlhelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DEPTID", Deptid);

                        ds = objsqlhelper.ExecuteDataSetSP("PKG_REPAIR_MAINTAINANCE_SP_COMPLAINT_TO_EMPLOYEE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.ComplaintController.GetEmployee-> " + ex.ToString());
                    }
                    return ds;
                }


                public int UpdateDeclinedRemark(Complaint objComplaintType)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                       
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_COMPLAINTID", objComplaintType.ComplaintId);
                        objParams[1] = new SqlParameter("@P_REMARK", objComplaintType.remark);  

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_REPAIR_MAINTAINANCE_SP_UPD_DECLINED_REMARK", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.UpdateCT-> " + ex.ToString());
                    }

                    return retStatus;
                }

                #region Priority Work


                public int AddUpdatePriorityWork(Complaint objCEnt)
                {
                    int retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_PWID", objCEnt.PWID);
                        objParams[1] = new SqlParameter("@P_PWNAME", objCEnt.PWNAME);                       
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REPAIR_MAINTAINANCE_PRIORITY_WORK_IU", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ComplaintController.AddUpdatePriorityWork->" + ex.ToString());
                    }
                    return retstatus;
                }

            

                #endregion

                #region Staff

                public int AddUpdateStaff(Complaint objCEnt)
                {
                    int retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_STAFFID", objCEnt.STAFFID);
                        objParams[1] = new SqlParameter("@P_DEPTNO", objCEnt.DEPTNO);
                        objParams[2] = new SqlParameter("@P_COMPLAINT_NATURE_ID", objCEnt.COMPLAINT_NATURE_ID);
                        objParams[3] = new SqlParameter("@P_STAFF_NAME", objCEnt.STAFF_NAME);
                        objParams[4] = new SqlParameter("@P_MOBILENO", objCEnt.MOBILENO);
                        objParams[5] = new SqlParameter("@P_EMAIL_ID", objCEnt.EMAIL_ID);
                        objParams[6] = new SqlParameter("@P_USERNO", objCEnt.USERNO);                      
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REPAIR_MAINTAINANCE_STAFF_IU", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ComplaintController.AddUpdateStaff->" + ex.ToString());
                    }
                    return retstatus;
                }


                #endregion


                #region Area Name


                public int AddUpdateAreaName(Complaint objCEnt)
                {
                    int retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_AREAID", objCEnt.AREAID);
                        objParams[1] = new SqlParameter("@P_AREANAME", objCEnt.AREANAME);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REPAIR_MAINTAINANCE_AREA_NAME_IU", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ComplaintController.AddUpdateAreaName->" + ex.ToString());
                    }
                    return retstatus;
                }



                #endregion



                public int DeleteComplaintWorkoutDetails(int CWID,int Status)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_CWID", CWID);                        

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_REPAIR_MAINTAINANCE_SP_DELETE_WORKOUTDETAIL_BY_COMPLAINT_ID", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ComplaintController.UpdateCT-> " + ex.ToString());
                    }

                    return retStatus;
                }



            }
        }
    }
 }
