//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : ATTENDANCE CONTROLLER                                                
// CREATION DATE : 10-OCT-2009                                                          
// CREATED BY    : SANJAY RATNAPARKHI                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Data;
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;


namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class StudentAttendanceController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        //CHANGED BY MANISH ON 02/02/2017
        //public int AddAttendance(StudentAttendance objAttendance, int batch)
        //{
        //    int retStatus = Convert.ToInt32(CustomStatus.Others);
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
        //        SqlParameter[] objParams = null;
        //        objParams = new SqlParameter[25];
        //        objParams[0] = new SqlParameter("@P_SESSIONNO", objAttendance.SessionNo);
        //        objParams[1] = new SqlParameter("@P_UA_NO", objAttendance.UaNo);
        //        objParams[2] = new SqlParameter("@P_SCHEMENO", objAttendance.SchemeNo);
        //        objParams[3] = new SqlParameter("@P_SEMESTERNO", objAttendance.SemesterNo);
        //        objParams[4] = new SqlParameter("@P_COURSENO", objAttendance.CourseNo);
        //        objParams[5] = new SqlParameter("@P_CCODE", objAttendance.CCode);
        //        objParams[6] = new SqlParameter("@P_BATCHNO", batch);
        //        objParams[7] = new SqlParameter("@P_SUBID", objAttendance.SubId);
        //        //objParams[8] = new SqlParameter("@P_BATCHNOS", batches);
        //        objParams[8] = new SqlParameter("@P_STUDIDS", objAttendance.StudIds);
        //        objParams[9] = new SqlParameter("@P_ATT_STATUS", objAttendance.AttStatus);
        //        objParams[10] = new SqlParameter("@P_ATT_DATE", objAttendance.AttDate);
        //        objParams[11] = new SqlParameter("@P_PERIOD", objAttendance.Period);
        //        objParams[12] = new SqlParameter("@P_HOURS", objAttendance.Hours);
        //        objParams[13] = new SqlParameter("@P_CLASS_TYPE", objAttendance.ClassType);
        //        objParams[14] = new SqlParameter("@P_CURRENT_DATE", objAttendance.CurDate);
        //        objParams[15] = new SqlParameter("@P_COLLEGE_CODE", objAttendance.CollegeCode);
        //        objParams[16] = new SqlParameter("@P_SECTIONNO", objAttendance.Sectionno);
        //        objParams[17] = new SqlParameter("@P_THPR", objAttendance.Th_Pr);
        //        objParams[18] = new SqlParameter("@P_TOPIC_COVERED", objAttendance.Topic);
        //        objParams[19] = new SqlParameter("@P_AGUA_NO", objAttendance.AguaNo);
        //        objParams[20] = new SqlParameter("@P_AGSECTIONNO", objAttendance.AgsectionNo);
        //        objParams[21] = new SqlParameter("@P_AGCOURSENO", objAttendance.AgCourseNo);
        //        objParams[22] = new SqlParameter("@P_SWAPSTATUS", objAttendance.Swap_status);
        //        objParams[23] = new SqlParameter("@P_ADDITIONAL_SLOT", objAttendance.Additional_Slot);
        //        objParams[24] = new SqlParameter("@P_ATT_NO", objAttendance.AttNo);
        //        objParams[24].Direction = ParameterDirection.InputOutput;
        //        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_STUDENT_ATTENDANCE_INSERT", objParams, true);
        //        if (obj != null && obj.ToString().Equals("-1001"))
        //        {
        //            retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
        //        }
        //        else
        //        {
        //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        retStatus = Convert.ToInt32(CustomStatus.Error);
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.AddAttendance-> " + ex.ToString());
        //    }

        //    return retStatus;
        //}

        /// <summary>
        /// Added by Pritish 0n date 31-05-2019
        /// </summary>
        /// <param name="objAttendance"></param>
        /// <param name="batch"></param>
        /// <returns></returns>
        //public int AddAttendance(StudentAttendance objAttendance, int batch,int tpno)
        //{
        //    int retStatus = Convert.ToInt32(CustomStatus.Others);
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
        //        SqlParameter[] objParams = null;
        //        objParams = new SqlParameter[27];
        //        objParams[0] = new SqlParameter("@P_SESSIONNO", objAttendance.SessionNo);
        //        objParams[1] = new SqlParameter("@P_UA_NO", objAttendance.UaNo);
        //        objParams[2] = new SqlParameter("@P_SCHEMENO", objAttendance.SchemeNo);
        //        objParams[3] = new SqlParameter("@P_SEMESTERNO", objAttendance.SemesterNo);
        //        objParams[4] = new SqlParameter("@P_COURSENO", objAttendance.CourseNo);
        //        objParams[5] = new SqlParameter("@P_CCODE", objAttendance.CCode);
        //        objParams[6] = new SqlParameter("@P_BATCHNO", batch);
        //        objParams[7] = new SqlParameter("@P_SUBID", objAttendance.SubId);
        //        //objParams[8] = new SqlParameter("@P_BATCHNOS", batches);
        //        objParams[8] = new SqlParameter("@P_STUDIDS", objAttendance.StudIds);
        //        objParams[9] = new SqlParameter("@P_ATT_STATUS", objAttendance.AttStatus);
        //        objParams[10] = new SqlParameter("@P_ATT_DATE", objAttendance.AttDate);
        //        objParams[11] = new SqlParameter("@P_PERIOD", objAttendance.Period);
        //        objParams[12] = new SqlParameter("@P_HOURS", objAttendance.Hours);
        //        objParams[13] = new SqlParameter("@P_CLASS_TYPE", objAttendance.ClassType);
        //        objParams[14] = new SqlParameter("@P_CURRENT_DATE", objAttendance.CurDate);
        //        objParams[15] = new SqlParameter("@P_COLLEGE_CODE", objAttendance.CollegeCode);
        //        objParams[16] = new SqlParameter("@P_SECTIONNO", objAttendance.Sectionno);
        //        objParams[17] = new SqlParameter("@P_THPR", objAttendance.Th_Pr);
        //        objParams[18] = new SqlParameter("@P_TOPIC_COVERED", objAttendance.Topic);
        //        objParams[19] = new SqlParameter("@P_AGUA_NO", objAttendance.AguaNo);
        //        objParams[20] = new SqlParameter("@P_AGSECTIONNO", objAttendance.AgsectionNo);
        //        objParams[21] = new SqlParameter("@P_AGCOURSENO", objAttendance.AgCourseNo);
        //        objParams[22] = new SqlParameter("@P_SWAPSTATUS", objAttendance.Swap_status);
        //        objParams[23] = new SqlParameter("@P_ADDITIONAL_SLOT", objAttendance.Additional_Slot);
        //        objParams[24] = new SqlParameter("@P_EXTRA_CUR_STATUS", objAttendance.Extra_Curr_Status);
        //        objParams[25] = new SqlParameter("@P_TP_NO", tpno);
        //        objParams[26] = new SqlParameter("@P_ATT_NO", objAttendance.AttNo);
        //        objParams[26].Direction = ParameterDirection.Output;

        //        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_STUDENT_ATTENDANCE_INSERT", objParams, true);
        //        if (obj != null && obj.ToString().Equals("-1001"))
        //        {
        //            retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
        //        }
        //        else
        //        {
        //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        retStatus = Convert.ToInt32(CustomStatus.Error);
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.AddAttendance-> " + ex.ToString());
        //    }
        //    return retStatus;
        //}


        public int AddAttendance(StudentAttendance objAttendance, int batch, int tpno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[28];
                objParams[0] = new SqlParameter("@P_SESSIONNO", objAttendance.SessionNo);
                objParams[1] = new SqlParameter("@P_UA_NO", objAttendance.UaNo);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objAttendance.SchemeNo);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", objAttendance.SemesterNo);
                objParams[4] = new SqlParameter("@P_COURSENO", objAttendance.CourseNo);
                objParams[5] = new SqlParameter("@P_CCODE", objAttendance.CCode);
                objParams[6] = new SqlParameter("@P_BATCHNO", batch);
                objParams[7] = new SqlParameter("@P_SUBID", objAttendance.SubId);
                //objParams[8] = new SqlParameter("@P_BATCHNOS", batches);
                objParams[8] = new SqlParameter("@P_STUDIDS", objAttendance.StudIds);
                objParams[9] = new SqlParameter("@P_ATT_STATUS", objAttendance.AttStatus);
                objParams[10] = new SqlParameter("@P_ATT_DATE", objAttendance.AttDate);
                objParams[11] = new SqlParameter("@P_PERIOD", objAttendance.Period);
                objParams[12] = new SqlParameter("@P_HOURS", objAttendance.Hours);
                objParams[13] = new SqlParameter("@P_CLASS_TYPE", objAttendance.ClassType);
                objParams[14] = new SqlParameter("@P_CURRENT_DATE", objAttendance.CurDate);
                objParams[15] = new SqlParameter("@P_COLLEGE_CODE", objAttendance.CollegeCode);
                objParams[16] = new SqlParameter("@P_SECTIONNO", objAttendance.Sectionno);
                objParams[17] = new SqlParameter("@P_THPR", objAttendance.Th_Pr);
                objParams[18] = new SqlParameter("@P_TOPIC_COVERED", objAttendance.Topic);
                objParams[19] = new SqlParameter("@P_AGUA_NO", objAttendance.AguaNo);
                objParams[20] = new SqlParameter("@P_AGSECTIONNO", objAttendance.AgsectionNo);
                objParams[21] = new SqlParameter("@P_AGCOURSENO", objAttendance.AgCourseNo);
                objParams[22] = new SqlParameter("@P_SWAPSTATUS", objAttendance.Swap_status);
                objParams[23] = new SqlParameter("@P_ADDITIONAL_SLOT", objAttendance.Additional_Slot);
                objParams[24] = new SqlParameter("@P_EXTRA_CUR_STATUS", objAttendance.Extra_Curr_Status);
                objParams[25] = new SqlParameter("@P_TP_NO", tpno);
                objParams[26] = new SqlParameter("@P_REMARK", objAttendance.Attendance_Remark);
                objParams[27] = new SqlParameter("@P_ATT_NO", objAttendance.AttNo);
                objParams[27].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_STUDENT_ATTENDANCE_INSERT", objParams, true);
                if (obj != null && obj.ToString().Equals("-1001"))
                {
                    retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.AddAttendance-> " + ex.ToString());
            }
            return retStatus;
        }


        public void SENDMSG(string MSG, string MOBILENO)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_MSG", MSG);
                objParams[1] = new SqlParameter("@P_MOBILENO", MOBILENO);
                objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SENDSMS_OF_ATTENDENCEANDMARKS", objParams, true);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.AddAttendance-> " + ex.ToString());
            }
        }

        /// <summary>
        /// Added by Pritish on date 31-05-2019
        /// </summary>
        /// 

        // FOR TO CHECK THE SAME COURSE PARAMTER RECODE IS PRESENT OR NOT , IF NOT THEN CONTINUE ELSE STOP
        public DataSet GetAttendenceByDate_Edit(int sessionNo, int uaNo, int courseNo, int sectionNo, int subId, int thpr, string attDate, int classtype, int period, int batchno, string ccode)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[11];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_UA_NO", uaNo);
                objParams[2] = new SqlParameter("@P_COURSENO", courseNo);
                objParams[3] = new SqlParameter("@P_SECTIONNO", sectionNo);
                objParams[4] = new SqlParameter("@P_SUBID", subId);
                objParams[5] = new SqlParameter("@P_THPR", thpr);
                objParams[6] = new SqlParameter("@P_ATTDATE", attDate);
                objParams[7] = new SqlParameter("@P_CLASSTYPE", classtype);
                objParams[8] = new SqlParameter("@P_PERIOD", period);
                objParams[9] = new SqlParameter("@P_BATCH", batchno);
                objParams[10] = new SqlParameter("@P_CCODE", ccode);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STUDENT_GET_DUPLICATE_ATTENDANCE_EDIT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetStudentAttendence-> " + ex.ToString());
            }

            return ds;
        }


        public DataSet GetAttendenceByDate_Report(int sessionNo, int uaNo, int subId, int thpr, string courseNo, int batchNo, string CCODE)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_UANO", uaNo);
                objParams[2] = new SqlParameter("@P_SUBID", subId);
                objParams[3] = new SqlParameter("@P_TH_PR", thpr);
                objParams[4] = new SqlParameter("@P_COURSENO", courseNo);
                objParams[5] = new SqlParameter("@P_BATCHNO", batchNo);
                objParams[6] = new SqlParameter("@P_CCODE", CCODE);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ATTENDANCE_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetStudentAttendence-> " + ex.ToString());
            }

            return ds;
        }


        //public DataSet GetStudentAttendence(int sessionNo, int courseNo, string ccode, int schemeno, int uaNo, int subId, int sectionNo, int batchNo)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(connectionString);

        //        SqlParameter[] objParams = new SqlParameter[8];
        //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
        //        objParams[1] = new SqlParameter("@P_COURSENO", courseNo);
        //        objParams[2] = new SqlParameter("@P_CCODE", ccode);
        //        objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
        //        objParams[4] = new SqlParameter("@P_UA_NO", uaNo);
        //        objParams[5] = new SqlParameter("@P_SUBID", subId);
        //        objParams[6] = new SqlParameter("@P_SECTIONNO", sectionNo);
        //        objParams[7] = new SqlParameter("@P_BATCHNO", batchNo);

        //        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_ATTENDANCE_BY_FACULTY", objParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetStudentAttendence-> " + ex.ToString());
        //    }

        //    return ds;
        //}

        /// Modified by Sunita on date 28-09-2019
        /// </summary>
        /// <param name="sessionNo"></param>
        /// <param name="courseNo"></param>
        /// <param name="uaNo"></param>
        /// <param name="subId"></param>
        /// <param name="thpr"></param>
        /// <param name="sectionNo"></param>
        /// <param name="batchNos"></param>
        /// <param name="CCODE"></param>
        /// <returns></returns>
        public DataSet GetStudentAttendence(int sessionNo, int courseNo, int uaNo, int subId, int thpr, int sectionNo, int batchNos, string CCODE, string date) // Changed by Manish on 14/01/2017
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_COURSENO", courseNo);
                objParams[2] = new SqlParameter("@P_UA_NO", uaNo);
                objParams[3] = new SqlParameter("@P_SUBID", subId);
                objParams[4] = new SqlParameter("@P_THPR", thpr);
                objParams[5] = new SqlParameter("@P_SECTIONNO", sectionNo);
                objParams[6] = new SqlParameter("@P_BATCHNOS", batchNos);
                objParams[7] = new SqlParameter("@P_CCODE", CCODE);
                objParams[8] = new SqlParameter("@P_DATE", date);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_ATTENDANCE_BY_FACULTY", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetStudentAttendence-> " + ex.ToString());
            }

            return ds;
        }
        //public DataSet GetAttendenceByDate(int sessionNo, int uaNo, string attDate, int subId, string courseNo, int classType, int period, int batchNo)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(connectionString);

        //        SqlParameter[] objParams = new SqlParameter[8];
        //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
        //        objParams[1] = new SqlParameter("@P_UA_NO", uaNo);
        //        objParams[2] = new SqlParameter("@P_ATTDATE", attDate);
        //        objParams[3] = new SqlParameter("@P_SUBID", subId);
        //        objParams[4] = new SqlParameter("@P_COURSENO", courseNo);
        //        objParams[5] = new SqlParameter("@P_CLASSTYPE", classType);
        //        objParams[6] = new SqlParameter("@P_PERIOD", period);
        //        objParams[7] = new SqlParameter("@P_BATCHNO", batchNo);

        //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STUDENT_ATTENDANCE_GET_DATE", objParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetStudentAttendence-> " + ex.ToString());
        //    }

        //    return ds;
        //}

        /// <summary>
        /// Added by Prithish 31-05-2019
        /// </summary>

        public DataSet GetAttendenceByDate(int sessionNo, int uaNo, string attDate, int subId, int thpr, string courseNo, int classType, int period, int batchNo, int sectionno, string ccode)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[11];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_UA_NO", uaNo);
                objParams[2] = new SqlParameter("@P_ATTDATE", attDate);
                objParams[3] = new SqlParameter("@P_SUBID", subId);
                objParams[4] = new SqlParameter("@P_THPR", thpr);
                objParams[5] = new SqlParameter("@P_COURSENO", courseNo);
                objParams[6] = new SqlParameter("@P_CLASSTYPE", classType);
                objParams[7] = new SqlParameter("@P_PERIOD", period);
                objParams[8] = new SqlParameter("@P_BATCHNO", batchNo);
                objParams[9] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[10] = new SqlParameter("@P_CCODE", ccode);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STUDENT_ATTENDANCE_GET_DATE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetStudentAttendence-> " + ex.ToString());
            }

            return ds;
        }

        //public DataSet GetAttendenceByDate(int sessionNo, int uaNo, string attDate, int subId, int courseNo, int classType, int period)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(connectionString);

        //        SqlParameter[] objParams = new SqlParameter[7];
        //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
        //        objParams[1] = new SqlParameter("@P_UA_NO", uaNo);
        //        objParams[2] = new SqlParameter("@P_ATTDATE", attDate);
        //        objParams[3] = new SqlParameter("@P_SUBID", subId);
        //        objParams[4] = new SqlParameter("@P_COURSENO", courseNo);
        //        objParams[5] = new SqlParameter("@P_CLASSTYPE", classType);
        //        //objParams[6] = new SqlParameter("@P_BATCHNO", batchNo);
        //        objParams[6] = new SqlParameter("@P_PERIOD", period);

        //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STUDENT_ATTENDANCE_GET_DATE", objParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetStudentAttendence-> " + ex.ToString());
        //    }

        //    return ds;
        //}

        public int UpdateAttendance(StudentAttendance objAttendance)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_STUDIDS", objAttendance.StudIds);
                objParams[1] = new SqlParameter("@P_ATT_STATUS", objAttendance.AttStatus);
                objParams[2] = new SqlParameter("@P_ATT_NO", objAttendance.AttNo);
                // objParams[2].Direction = ParameterDirection.InputOutput;

                if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_STUDENT_ATTENDANCE_UPDATE", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.UpdateAttendance-> " + ex.ToString());
            }

            return retStatus;
        }

        //public DataSet GetDuplicateAttendence(int sessionNo, int uaNo, int courseNo, int sectionNo, int subId, string attDate, int classtype, int period)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(connectionString);

        //        SqlParameter[] objParams = new SqlParameter[8];
        //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
        //        objParams[1] = new SqlParameter("@P_UA_NO", uaNo);
        //        objParams[2] = new SqlParameter("@P_COURSENO", courseNo);
        //        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionNo);
        //        objParams[4] = new SqlParameter("@P_SUBID", subId);
        //        objParams[5] = new SqlParameter("@P_ATTDATE", attDate);
        //        objParams[6] = new SqlParameter("@P_CLASSTYPE", classtype);
        //        objParams[7] = new SqlParameter("@P_PERIOD", period);

        //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STUDENT_GET_DUPLICATE_ATTENDANCE", objParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetDuplicateAttendence-> " + ex.ToString());
        //    }

        //    return ds;
        //}
        /// <summary>
        /// Added by Pritish
        /// </summary>

        public DataSet GetDuplicateAttendence(int sessionNo, int uaNo, int courseNo, int sectionNo, int subId, int thpr, string attDate, int classtype, int period, int batchno, string ccode)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[11];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_UA_NO", uaNo);
                objParams[2] = new SqlParameter("@P_COURSENO", courseNo);
                objParams[3] = new SqlParameter("@P_SECTIONNO", sectionNo);
                objParams[4] = new SqlParameter("@P_SUBID", subId);
                objParams[5] = new SqlParameter("@P_THPR", thpr);
                objParams[6] = new SqlParameter("@P_ATTDATE", attDate);
                objParams[7] = new SqlParameter("@P_CLASSTYPE", classtype);
                objParams[8] = new SqlParameter("@P_PERIOD", period);
                objParams[9] = new SqlParameter("@P_BATCH", batchno);
                objParams[10] = new SqlParameter("@P_CCODE", ccode);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STUDENT_GET_DUPLICATE_ATTENDANCE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetDuplicateAttendence-> " + ex.ToString());
            }

            return ds;
        }

        public DataSet GetSelectedCautionMoneyFeeItem(DailyFeeCollectionRpt dcrReport, string[] feeHead)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSelectedFeeItem = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
            {
                new SqlParameter("@P_RECIEPT_CODE",dcrReport.ReceiptTypes),
                new SqlParameter("@P_FROM_DATE",(dcrReport.FromDate != DateTime.MinValue ? dcrReport.FromDate as object : DBNull.Value as object)),
                new SqlParameter("@P_TO_DATE",(dcrReport.ToDate != DateTime.MinValue ? dcrReport.ToDate as object : DBNull.Value as object)),
                new SqlParameter("@P_SEMESTERNO",dcrReport.SemesterNo),
                new SqlParameter("@P_BRANCHNO",dcrReport.BranchNo),
                new SqlParameter("@P_DEGREENO",dcrReport.DegreeNo),
                new SqlParameter("@P_YEAR",dcrReport.YearNo),
                new SqlParameter("@P_FEEHEAD1",feeHead[0]),
                //new SqlParameter("@P_FEEHEAD2",feeHead[1]),
                //new SqlParameter("@P_FEEHEAD3",feeHead[2]),
                //new SqlParameter("@P_FEEHEAD4",feeHead[3]),
                //new SqlParameter("@P_FEEHEAD5",feeHead[4])
            };
                ds = objSelectedFeeItem.ExecuteDataSetSP("PKG_FEECOLLECT_REPORT_CAUTION_MONEY_DEPOSIT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetSelectedCautionMoneyFeeItem-> " + ex.ToString());
            }
            return ds;
        }

        //public DataSet GetDayWiseData(int sessionNo, int schemeNo, int courseNo, int uaNo, int subId, int sectionno, string fromdate, string todate)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(connectionString);

        //        SqlParameter[] objParams = new SqlParameter[8];
        //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
        //        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeNo);
        //        objParams[2] = new SqlParameter("@P_COURSENO", courseNo);
        //        objParams[3] = new SqlParameter("@P_UA_NO", uaNo);
        //        objParams[4] = new SqlParameter("@P_SUBID", subId);
        //        objParams[5] = new SqlParameter("@P_SECTIONNO", sectionno);
        //        objParams[6] = new SqlParameter("@P_FROMDATE", fromdate);
        //        objParams[7] = new SqlParameter("@P_TODATE", todate);

        //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_REPORT_STU_ATTENDANCE_DAILY", objParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetStudentAttendence-> " + ex.ToString());
        //    }

        //    return ds;
        //}

        /// <summary>
        /// Added by Pritish on date 31-05-2019
        /// </summary>

        public DataSet GetDayWiseData(int sessionNo, int schemeNo, int courseNo, int uaNo, int subId, int sectionno, DateTime fromdate, DateTime todate, int Coursetype, int batchno, string ccode)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[11];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeNo);
                objParams[2] = new SqlParameter("@P_COURSENO", courseNo);
                objParams[3] = new SqlParameter("@P_UA_NO", uaNo);
                objParams[4] = new SqlParameter("@P_SUBID", subId);
                objParams[5] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[6] = new SqlParameter("@P_FROMDATE", fromdate);
                objParams[7] = new SqlParameter("@P_TODATE", todate);
                objParams[8] = new SqlParameter("@P_TH_PR", Coursetype);
                objParams[9] = new SqlParameter("@P_BATCHNO", batchno);
                objParams[10] = new SqlParameter("@P_CCODE", ccode);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_REPORT_STU_ATTENDANCE_DAILY17112011NEW", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetStudentAttendence-> " + ex.ToString());
            }

            return ds;
        }

        //MODIFIED ON 5_10_16
        //public DataSet GetPeriod(int sessionNo, int courseNo, int uaNo, int subId, int thpr, int slot, int section, int Scheme,string CCode)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(connectionString);

        //        SqlParameter[] objParams = new SqlParameter[9];
        //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
        //        objParams[1] = new SqlParameter("@P_COURSENO", courseNo);
        //        objParams[2] = new SqlParameter("@P_UA_NO", uaNo);
        //        objParams[3] = new SqlParameter("@P_SUBID", subId);
        //        objParams[4] = new SqlParameter("@P_THPR", thpr);
        //        objParams[5] = new SqlParameter("@P_SLOT", slot);
        //        objParams[6] = new SqlParameter("@P_SECTIONNO", section);
        //        objParams[7] = new SqlParameter("@P_SCHEMENO", Scheme);
        //        objParams[8] = new SqlParameter("@P_CCODE", CCode);
        //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ATTENDANCE_GET_PEROID_BY_DAY", objParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetStudentAttendence-> " + ex.ToString());
        //    }

        //    return ds;
        //}

        /// <summary>
        /// Addded by Pritish 31-05-2019
        /// </summary>
        /// <param name="sessionNo"></param>
        /// <param name="courseNo"></param>
        /// <param name="uaNo"></param>
        /// <param name="subId"></param>
        /// <param name="thpr"></param>
        /// <param name="slot"></param>
        /// <param name="section"></param>
        /// <param name="Scheme"></param>
        /// <param name="CCode"></param>
        /// <returns></returns>
        public DataSet GetPeriod(int sessionNo, int courseNo, int uaNo, int subId, int thpr, int slot, int section, int Scheme, string CCode)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_COURSENO", courseNo);
                objParams[2] = new SqlParameter("@P_UA_NO", uaNo);
                objParams[3] = new SqlParameter("@P_SUBID", subId);
                objParams[4] = new SqlParameter("@P_THPR", thpr);
                objParams[5] = new SqlParameter("@P_SLOT", slot);
                objParams[6] = new SqlParameter("@P_SECTIONNO", section);
                objParams[7] = new SqlParameter("@P_SCHEMENO", Scheme);
                objParams[8] = new SqlParameter("@P_CCODE", CCode);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ATTENDANCE_GET_PEROID_BY_DAY", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetStudentAttendence-> " + ex.ToString());
            }

            return ds;
        }



        //added by reena on 5_8_16
        //public DataSet GetPeriodExtra(int sessionNo, int courseNo, int uaNo, int subId, int section, int Schemeno)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(connectionString);

        //        SqlParameter[] objParams = new SqlParameter[6];
        //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
        //        objParams[1] = new SqlParameter("@P_COURSENO", courseNo);
        //        objParams[2] = new SqlParameter("@P_UA_NO", uaNo);
        //        objParams[3] = new SqlParameter("@P_SUBID", subId);
        //        // objParams[4] = new SqlParameter("@P_SLOT", slot);
        //        objParams[4] = new SqlParameter("@P_SECTIONNO", section);
        //        objParams[5] = new SqlParameter("@P_SCHEMENO ", Schemeno);
        //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ATTENDANCE_GET_PEROID_EXTRA_ALTERNATE", objParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetStudentAttendence-> " + ex.ToString());
        //    }

        //    return ds;
        //}

        /// <summary>
        /// Added by Pritish on date 31-05-2019
        /// </summary>
        /// <param name="sessionNo"></param>
        /// <param name="courseNo"></param>
        /// <param name="uaNo"></param>
        /// <param name="subId"></param>
        /// <param name="section"></param>
        /// <param name="Schemeno"></param>
        /// <returns></returns>
        public DataSet GetPeriodExtra(int sessionNo, int courseNo, int uaNo, int subId, int section, int Schemeno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_COURSENO", courseNo);
                objParams[2] = new SqlParameter("@P_UA_NO", uaNo);
                objParams[3] = new SqlParameter("@P_SUBID", subId);
                // objParams[4] = new SqlParameter("@P_SLOT", slot);
                objParams[4] = new SqlParameter("@P_SECTIONNO", section);
                objParams[5] = new SqlParameter("@P_SCHEMENO ", Schemeno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ATTENDANCE_GET_PEROID_EXTRA_ALTERNATE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetStudentAttendence-> " + ex.ToString());
            }

            return ds;
        }


        // added by manish 20/02/17
        //public DataSet GetFacultiesBySlot(int Sessionno, string Date, int Slot, int uano,int Sectionno,int subid,int thpr)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(connectionString);

        //        SqlParameter[] objParams = new SqlParameter[7];
        //        objParams[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
        //        objParams[1] = new SqlParameter("@P_DATE", Date);
        //        objParams[2] = new SqlParameter("@P_SLOT", Slot);
        //        objParams[3] = new SqlParameter("@P_UA_NO", uano);
        //        objParams[4] = new SqlParameter("@P_SUBID", subid);
        //        objParams[5] = new SqlParameter("@P_TH_PR", thpr);
        //        objParams[6] = new SqlParameter("@P_SECTIONNO", Sectionno);

        //        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_FACULTY_BY_SLOT", objParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetStudentAttendence-> " + ex.ToString());
        //    }
        //    return ds;
        //}

        /// <summary>
        /// Added by Pritish on date 31-05-2019
        /// </summary>

        public DataSet GetFacultiesBySlot(int Sessionno, string Date, int Slot, int uano, int Sectionno, int subid, int thpr)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                objParams[1] = new SqlParameter("@P_DATE", Date);
                objParams[2] = new SqlParameter("@P_SLOT", Slot);
                objParams[3] = new SqlParameter("@P_UA_NO", uano);
                objParams[4] = new SqlParameter("@P_SUBID", subid);
                objParams[5] = new SqlParameter("@P_TH_PR", thpr);
                objParams[6] = new SqlParameter("@P_SECTIONNO", Sectionno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_FACULTY_BY_SLOT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetStudentAttendence-> " + ex.ToString());
            }
            return ds;
        }

        /// <summary>
        /// Added by Pritish on date 31-05-2019
        /// </summary>
        /// <param name="attno"></param>
        /// <param name="userno"></param>
        /// <returns></returns>
        public int deleteAttendanceRecord(int attno, int userno)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_ATT_NO", attno);
                objParams[1] = new SqlParameter("@P_USER_NO", userno);
                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_DELETE_ATTENDANCE_ENTRY", objParams, true);
                if (Convert.ToInt32(obj) == -99)
                    retStatus = Convert.ToInt32(obj);
                if (Convert.ToInt32(obj) == 1)
                {
                    retStatus = Convert.ToInt32(obj);
                }

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.deleteAttendanceRecord-> " + ex.ToString());
            }
            return retStatus;
        }

        /// <summary>
        /// Added by Pritish On date 31-05-2019
        /// </summary>
        public DataSet GetCourseWiseAttendanceDetails(int Sessionno, int courseNo, int uano, int batchNo, int subid, int thpr)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                objParams[1] = new SqlParameter("@P_COURSENO", courseNo);
                objParams[2] = new SqlParameter("@P_UA_NO", uano);
                objParams[3] = new SqlParameter("@P_SUBID", subid);
                objParams[4] = new SqlParameter("@P_THPR", thpr);
                objParams[5] = new SqlParameter("@P_BATCHNO", batchNo);

                ds = objSQLHelper.ExecuteDataSetSP("ACD_PKG_GET_COURSEWISE_ATTENDANCE_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetStudentAttendence-> " + ex.ToString());
            }
            return ds;
        }

        /// <summary>
        /// Added by Pritish On date 31-05-2019
        /// </summary>
        /// <param name="objAtt"></param>
        /// <returns></returns>

        public int GetAttendanceRepeat(StudentAttendance objAtt)
        {
            int count = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_SESSIONNO", objAtt.SessionNo);
                objParams[1] = new SqlParameter("@P_FACULTYNO", objAtt.FacultyNo);
                objParams[2] = new SqlParameter("@P_ATT_DATE", objAtt.AttDate);
                objParams[3] = new SqlParameter("@P_COURSENO", objAtt.CourseNo);
                objParams[4] = new SqlParameter("@P_BATCHNO", objAtt.BatchNo);
                objParams[5] = new SqlParameter("@P_PERIOD", objAtt.Period);
                objParams[6] = new SqlParameter("@P_CLASSTYPE", objAtt.ClassType);
                objParams[7] = new SqlParameter("@P_SECTIONNO", objAtt.Sectionno);

                count = (int)objSQLHelper.ExecuteScalarSP("PKG_ACAD_GET_REPEAT_ATTENDANCE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GetCourseAllotment-> " + ex.ToString());
            }
            return count;
        }

        /// <summary>
        /// Added by Rita.M on date 08/07/2019
        /// </summary>
        /// <param name="sessionNo"></param>
        /// <param name="schemeno"></param>
        /// <param name="courseNo"></param>
        /// <param name="uaNo"></param>
        /// <param name="subId"></param>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public DataSet GetDateWiseData(int sessionNo, int schemeno, int courseNo, int uaNo, int subId, string fromdate, string todate, int batchNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_COURSENO", courseNo);
                objParams[3] = new SqlParameter("@P_UA_NO", uaNo);
                objParams[4] = new SqlParameter("@P_SUBID", subId);
                //objParams[5] = new SqlParameter("@P_DIVISIONNO", divisionno);
                objParams[5] = new SqlParameter("@P_FROMDATE", fromdate);
                objParams[6] = new SqlParameter("@P_TODATE", todate);
                objParams[7] = new SqlParameter("@P_BATCHNO", batchNo);
                //objParams[9] = new SqlParameter("@P_PERIODNO",perionNo);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_REPORT_STU_ATTENDANCE_DAILY", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentAttendanceController.GetDateWiseData-> " + ex.ToString());
            }

            return ds;
        }
        /// <summary>
        /// Added by S.Patil = 29082019
        /// </summary>
        /// <param name="session"></param>
        /// <param name="uaNo"></param>
        /// <param name="fdate"></param>
        /// <param name="todate"></param>
        /// <returns></returns>
        public DataSet GetStudAttDetails(int session, int uaNo, string fdate, string todate)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                objParams[1] = new SqlParameter("@P_UA_NO", uaNo);
                objParams[2] = new SqlParameter("@P_FROMDATE", fdate);
                objParams[3] = new SqlParameter("@P_TODATE", todate);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_REPORT_STU_ATTENDANCE_FACULTY_SUBJECTWISE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentAttendanceController.GetStudAttDetails-> " + ex.ToString());
            }

            return ds;
        }

        /// <summary>
        /// Added by S.Patil - 29082019
        /// </summary>
        /// <param name="sessionNo"></param>
        /// <param name="schemeno"></param>
        /// <param name="semno"></param>
        /// <param name="courseNo"></param>
        /// <param name="div"></param>
        /// <param name="ua_no"></param>
        /// <param name="fdate"></param>
        /// <param name="todate"></param>
        /// <param name="subid"></param>
        /// <param name="batchno"></param>
        /// <returns></returns>
        public DataSet StudAttUpToDateReport(int sessionNo, int schemeno, int semno, int courseNo, int div, int ua_no, string fdate, string todate, int subid, int batchno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semno);
                objParams[3] = new SqlParameter("@P_COURSENO", courseNo);
                objParams[4] = new SqlParameter("@P_DIVISIONNO", div);
                objParams[5] = new SqlParameter("@P_UA_NO", ua_no);
                objParams[6] = new SqlParameter("@P_FROMDATE", fdate);
                objParams[7] = new SqlParameter("@P_TODATE", todate);
                objParams[8] = new SqlParameter("@P_SUBID", subid);
                objParams[9] = new SqlParameter("@P_BATCHNO", batchno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_REPORT_STU_ATTENDANCE_UPTO_DATE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.StudAttUpToDateReport-> " + ex.ToString());
            }

            return ds;
        }

        /// <summary>
        /// Added by S.Patil - 29082019
        /// </summary>
        /// <param name="sessionNo"></param>
        /// <param name="degreeno"></param>
        /// <param name="branchno"></param>
        /// <param name="sem"></param>
        /// <param name="fdate"></param>
        /// <param name="todate"></param>
        /// <param name="per"></param>
        /// <param name="cond"></param>
        /// <param name="condval"></param>
        /// <param name="courseno"></param>
        /// <returns></returns>
        public DataSet GetExtraCurriAttendanceStud(int sessionNo, int degreeno, int branchno, int sem, string fdate, string todate, int per, string cond, int condval, string courseno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", sem);
                objParams[4] = new SqlParameter("@P_FROMDATE", fdate);
                objParams[5] = new SqlParameter("@P_TODATE", todate);
                objParams[6] = new SqlParameter("@P_PERCENTAGE", per);
                objParams[7] = new SqlParameter("@P_CONDITION", cond);
                objParams[8] = new SqlParameter("@P_CONDVALUE", condval);
                objParams[9] = new SqlParameter("@P_COURSENO", courseno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_EXTRACURRI_ATTENDANCE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetExtraCurriAttendanceStud-> " + ex.ToString());
            }
            return ds;
        }
        /// <summary>
        /// Added By Rita M on date 07/07/2019
        /// </summary>
        /// <param name="schemeNo"></param>
        /// <param name="courseNo"></param>
        /// <param name="uaNo"></param>
        /// <param name="subId"></param>
        /// <param name="mmyy"></param>
        /// <returns></returns>
        public DataSet GetDayWiseAttendanceData(int schemeNo, int courseNo, int uaNo, int subId, string mmyy)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_SCHEMENO", schemeNo);
                objParams[1] = new SqlParameter("@P_COURSENO", courseNo);
                objParams[2] = new SqlParameter("@P_UA_NO", uaNo);
                objParams[3] = new SqlParameter("@P_SUBID", subId);
                objParams[4] = new SqlParameter("@P_MONTHYEAR", mmyy);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_REPORT_STU_ATTENDANCE_DAILY", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentAttendanceController.GetStudentAttendence-> " + ex.ToString());
            }

            return ds;
        }

        /// <summary>
        /// Added by Nidhi Gour on 31/10/2019
        /// </summary>
        /// <param name="sessionNo"></param>
        /// <param name="semesterno"></param>
        /// <param name="schemeno"></param>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <param name="sectionno"></param>
        /// <param name="conditions"></param>
        /// <param name="percentage"></param>
        /// <param name="subid"></param>
        /// <returns></returns>


        public DataSet GetAttendanceDeailsForSms(int sessionNo, int semesterno, int schemeno, DateTime fromdate, DateTime todate, string sectionno, string conditions, decimal percentage, int subid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[3] = new SqlParameter("@P_FROMDATE", fromdate);
                objParams[4] = new SqlParameter("@P_TODATE", todate);
                objParams[5] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[6] = new SqlParameter("@P_CONDITIONS", conditions);
                objParams[7] = new SqlParameter("@P_PERCENTAGE", percentage);
                objParams[8] = new SqlParameter("@P_SUBID", subid);
                // objParams[9] = new SqlParameter("@P_UANO", UA_NO);


                // ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_TIMETABLE_SUBJECTWISE_PERCENTAGE_PE", objParams);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_TIMETABLE_SUBJECTWISE_PERCENTAGE_PE_PA", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentAttendanceController.GetAttendanceDataForSms-> " + ex.ToString());
            }

            return ds;
        }
        /// <summary>
        /// Added by Nidhi Gour on 31/10/2019
        /// </summary>
        /// <param name="sessionNo"></param>
        /// <param name="semesterno"></param>
        /// <param name="schemeno"></param>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <param name="sectionno"></param>
        /// <param name="conditions"></param>
        /// <param name="percentage"></param>
        /// <param name="subid"></param>
        /// <returns></returns>

        public DataSet AttendenceWiseGetEmailAndMobileForCommunication(string idnos)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_IDNO", idnos);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_EAMIL_MOBILE_FOR_BULK_SENDING", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentAttendanceController.AttendenceWiseGetEmailAndMobileForCommunication-> " + ex.ToString());
            }
            return ds;
        }

        //Added by Nikhil Vinod Lambe on 21022020 
        //Updated BY Sakshi M on 09012024
        public DataSet GetAttendenceDetails(IITMS.UAIMS.BusinessLayer.BusinessEntities.Attendance objAtt, int selector, string FromDate, string ToDate)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[12];
                objParams[0] = new SqlParameter("@P_SESSIONNO ", objAtt.SessionNo);
                objParams[1] = new SqlParameter("@P_SEMESTERNO ", objAtt.SemesterNo);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objAtt.SchemeNo);
                objParams[3] = new SqlParameter("@P_FROMDATE", FromDate);
                objParams[4] = new SqlParameter("@P_TODATE", ToDate);
                //objParams[5] = new SqlParameter("@P_SECTIONNO",objAtt.SectionNo );
                objParams[5] = new SqlParameter("@P_PERCENTAGEFROM", objAtt.PercentageFrom);
                objParams[6] = new SqlParameter("@P_PERCENTAGETO", objAtt.PercentageTo);
                objParams[7] = new SqlParameter("@P_SECTIONNO", objAtt.SectionNo);
                objParams[8] = new SqlParameter("@P_SUBID", objAtt.SubId);
                objParams[9] = new SqlParameter("@P_CONDITIONS", objAtt.Conditions);
                objParams[10] = new SqlParameter("@P_PERCENTAGE", objAtt.Percentage);
                objParams[11] = new SqlParameter("@P_SELECTOR", selector);
                //objParams[9] = new SqlParameter("@P_TH_PR",objAtt.Th_Pr );
                //objParams[10] = new SqlParameter("@P_BATCHNO",objAtt.BatchNo) ;
                //objParams[11] = new SqlParameter("@P_UANO",objAtt.UaNo);
                //objParams[12] = new SqlParameter("@P_DEGREENO",objAtt.DegreeNo );
                //objParams[13] = new SqlParameter("@P_BRANCHNO",objAtt.BranchNo );

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_TIMETABLE_SUBJECTWISE_PERCENTAGE_BETWEEN", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetAttendenceDetails-> " + ex.ToString());
            }
            return ds;

        }

        //Update BY Sakshi M on 09012024
        public DataSet GetAttendanceByPercentage(IITMS.UAIMS.BusinessLayer.BusinessEntities.Attendance objAtt, int selector, string FromDate, string ToDate)
        {
            DataSet dsPer = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[12];
                objParams[0] = new SqlParameter("@P_SESSIONNO", objAtt.SessionNo);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", objAtt.SemesterNo);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objAtt.SchemeNo);
                objParams[3] = new SqlParameter("@P_FROMDATE", FromDate);
                objParams[4] = new SqlParameter("@P_TODATE", ToDate);
                objParams[5] = new SqlParameter("@P_PERCENTAGEFROM", objAtt.PercentageFrom);
                objParams[6] = new SqlParameter("@P_PERCENTAGETO", objAtt.PercentageTo);
                objParams[7] = new SqlParameter("@P_SECTIONNO", objAtt.SectionNo);
                objParams[8] = new SqlParameter("@P_SUBID", objAtt.SubId);
                objParams[9] = new SqlParameter("@P_CONDITIONS", objAtt.Conditions);
                objParams[10] = new SqlParameter("@P_PERCENTAGE", objAtt.Percentage);
                objParams[11] = new SqlParameter("@P_SELECTOR", selector);
                dsPer = objSQLHelper.ExecuteDataSetSP("PKG_ACD_TIMETABLE_SUBJECTWISE_PERCENTAGE_BETWEEN", objParams);
            }
            catch (Exception ex)
            {
                return dsPer;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetAttendanceByPercentage-> " + ex.ToString());
            }
            return dsPer;
        }

        public DataSet GetAttendanceForAll(IITMS.UAIMS.BusinessLayer.BusinessEntities.Attendance objAtt, int selector)
        {
            DataSet dsAll = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[12];
                objParams[0] = new SqlParameter("@P_SESSIONNO", objAtt.SessionNo);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", objAtt.SemesterNo);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objAtt.SchemeNo);
                objParams[3] = new SqlParameter("@P_FROMDATE", objAtt.FromDate);
                objParams[4] = new SqlParameter("@P_TODATE", objAtt.ToDate);
                objParams[5] = new SqlParameter("@P_PERCENTAGEFROM", objAtt.PercentageFrom);
                objParams[6] = new SqlParameter("@P_PERCENTAGETO", objAtt.PercentageTo);
                objParams[7] = new SqlParameter("@P_SECTIONNO", objAtt.SectionNo);
                objParams[8] = new SqlParameter("@P_SUBID", objAtt.SubId);
                objParams[9] = new SqlParameter("@P_CONDITIONS", objAtt.Conditions);
                objParams[10] = new SqlParameter("@P_PERCENTAGE", objAtt.Percentage);
                objParams[11] = new SqlParameter("@P_SELECTOR", selector);
                dsAll = objSQLHelper.ExecuteDataSetSP("PKG_ACD_TIMETABLE_SUBJECTWISE_PERCENTAGE_BETWEEN", objParams);
            }
            catch (Exception ex)
            {
                return dsAll;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetAttendanceByPercentage-> " + ex.ToString());
            }
            return dsAll;
        }

        /// //Updated BY Sakshi M ON 09012024
        public DataSet GetAttendanceSelectorWise(IITMS.UAIMS.BusinessLayer.BusinessEntities.Attendance objAtt, int selector, string FromDate, string ToDate)
        {
            DataSet dsAll = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[12];
                objParams[0] = new SqlParameter("@P_SESSIONNO", objAtt.SessionNo);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", objAtt.SemesterNo);
                objParams[2] = new SqlParameter("@P_SCHEMENO", objAtt.SchemeNo);
                objParams[3] = new SqlParameter("@P_FROMDATE", FromDate);
                objParams[4] = new SqlParameter("@P_TODATE", ToDate);
                objParams[5] = new SqlParameter("@P_PERCENTAGEFROM", objAtt.PercentageFrom);
                objParams[6] = new SqlParameter("@P_PERCENTAGETO", objAtt.PercentageTo);
                objParams[7] = new SqlParameter("@P_SECTIONNO", objAtt.SectionNo);
                objParams[8] = new SqlParameter("@P_SUBID", objAtt.SubId);
                objParams[9] = new SqlParameter("@P_CONDITIONS", objAtt.Conditions);
                objParams[10] = new SqlParameter("@P_PERCENTAGE", objAtt.Percentage);
                objParams[11] = new SqlParameter("@P_SELECTOR", selector);
                dsAll = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ATTENDANCE_SHOW_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                return dsAll;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.GetAttendanceByPercentage-> " + ex.ToString());
            }
            return dsAll;
        }





        /// <summary>
        /// added by Jay T on 21_02_2023
        /// </summary>
        /// <param name="sessionNo"></param>
        /// <param name="semesterno"></param>
        /// <param name="schemeno"></param>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <param name="sectionno"></param>
        /// <param name="conditions"></param>
        /// <param name="percentage"></param>
        /// <param name="subid"></param>
        /// <returns></returns>
        public DataSet GetAttendanceDeailsForSms1(int sessionNo, int semesterno, int schemeno, DateTime fromdate, DateTime todate, int sectionno, string conditions, decimal percentage, int subid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[3] = new SqlParameter("@P_FROMDATE", fromdate);
                objParams[4] = new SqlParameter("@P_TODATE", todate);
                objParams[5] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[6] = new SqlParameter("@P_CONDITIONS", conditions);
                objParams[7] = new SqlParameter("@P_PERCENTAGE", percentage);
                objParams[8] = new SqlParameter("@P_SUBID", subid);
                // objParams[9] = new SqlParameter("@P_UANO", UA_NO);


                // ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_TIMETABLE_SUBJECTWISE_PERCENTAGE_PE", objParams);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_TIMETABLE_SUBJECTWISE_PERCENTAGE_PE_PA", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentAttendanceController.GetAttendanceDataForSms-> " + ex.ToString());
            }

            return ds;
        }
        //added by nehal on dated 02052023
        public DataSet GetAttendanceDeailsForSmsFaculty(int sessionNo, int semesterno, int schemeno, DateTime fromdate, DateTime todate, string sectionno, string conditions, decimal percentage, int subid, int uano)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[3] = new SqlParameter("@P_FROMDATE", fromdate);
                objParams[4] = new SqlParameter("@P_TODATE", todate);
                objParams[5] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[6] = new SqlParameter("@P_CONDITIONS", conditions);
                objParams[7] = new SqlParameter("@P_PERCENTAGE", percentage);
                objParams[8] = new SqlParameter("@P_SUBID", subid);
                objParams[9] = new SqlParameter("@P_FAC_ADVISOR", uano);


                // ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_TIMETABLE_SUBJECTWISE_PERCENTAGE_PE", objParams);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_TIMETABLE_SUBJECTWISE_PERCENTAGE_PE_PA_FACULTY", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentAttendanceController.GetAttendanceDeailsForSmsFaculty-> " + ex.ToString());
            }

            return ds;
        }

        public DataSet GetDetailsFromReportMaster(int reportid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_REPORT_ID", reportid);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_REPORT_DETAILS_REPORT_MASTER", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentAttendanceController.AttendenceWiseGetEmailAndMobileForCommunication-> " + ex.ToString());
            }
            return ds;
        }

    }
}
