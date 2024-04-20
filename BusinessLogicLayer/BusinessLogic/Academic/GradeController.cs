
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
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;


namespace BusinessLogicLayer.BusinessLogic.Academic
{
   public class GradeController
    {
       string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

       public int AddGrade(Grade objGrade)
       {
           int status = 0;
           try
           {
               SQLHelper objSQLHelper = new SQLHelper(connectionString);
               SqlParameter[] sqlParams = new SqlParameter[]
                {
                    //new SqlParameter("@P_GRADE_TYPE",objGrade.GradeType),
                    new SqlParameter("@P_GRADE_NAME", objGrade.GradeName),
                    new SqlParameter("@P_COLLEGE_CODE", objGrade.CollegeCode),
                    new SqlParameter("@P_OUTPUT", status)
                };
               sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

               object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GRADE_TYPE_INSERT", sqlParams, true);

               if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                   status = Convert.ToInt32(CustomStatus.RecordSaved);
               else
                   status = Convert.ToInt32(CustomStatus.Error);
           }
           catch (Exception ex)
           {
               status = Convert.ToInt32(CustomStatus.Error);
               throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.AddBatch() --> " + ex.Message + " " + ex.StackTrace);
           }
           return status;
       }

       public int UpdateGrade(Grade objGrade)
       {
           int status;
           try
           {
               SQLHelper objSQLHelper = new SQLHelper(connectionString);
               SqlParameter[] sqlParams = new SqlParameter[]
                {                    
                    //new SqlParameter("@P_BATCHNAME",objBatch.BatchName),
                    new SqlParameter("@P_GRADE_TYPE_NAME", objGrade.GradeName),
                    new SqlParameter("@P_COLLEGE_CODE", objGrade.CollegeCode),
                    new SqlParameter("@P_OUTPUT",objGrade.GradeType)
                };
               sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

               object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GRADE_TYPE_UPDATE", sqlParams, true);

               if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                   status = Convert.ToInt32(CustomStatus.RecordUpdated);
               else
                   status = Convert.ToInt32(CustomStatus.Error);
           }
           catch (Exception ex)
           {
               status = Convert.ToInt32(CustomStatus.Error);
               throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.UpdateBatch() --> " + ex.Message + " " + ex.StackTrace);
           }
           return status;
       }
       public DataSet GetAllGrade()
       {
           DataSet ds = null;
           try
           {
               SQLHelper objSQLHelper = new SQLHelper(connectionString);
               SqlParameter[] objParams = new SqlParameter[0];

               ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GRADE_GET_ALL", objParams);
           }
           catch (Exception ex)
           {
               throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetAllBatch() --> " + ex.Message + " " + ex.StackTrace);
           }
           return ds;
       }
       public SqlDataReader GetGradeTypeNo(int gradeNo)
       {
           SqlDataReader dr = null;
           try
           {
               SQLHelper objSQLHelper = new SQLHelper(connectionString);
               SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_OUTPUT", gradeNo) };

               dr = objSQLHelper.ExecuteReaderSP("PKG_ACD_GRADE_GET_BY_TYPE_NO", objParams);

           }
           catch (Exception ex)
           {
               throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetBatchByNo() --> " + ex.Message + " " + ex.StackTrace);
           }
           return dr;
       }

       //Added by Nikhil Lambe on 23/04/2021 to get students list for I grade
       public DataSet GetStudentsList_For_I_Grade(int SessionNo, int SchemeNo, int SemesterNo, int Subject)
       {
           DataSet ds = null;
           try
           {
               SQLHelper objSqlHelper = new SQLHelper(connectionString);
               SqlParameter[] objParams = null;

               objParams = new SqlParameter[4];
               objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
               objParams[1] = new SqlParameter("@P_SCHEMENO", SchemeNo);
               objParams[2] = new SqlParameter("@P_SEMESTERNO", SemesterNo);
               objParams[3] = new SqlParameter("@P_SUBJECT", Subject);
               ds = objSqlHelper.ExecuteDataSetSP("PKG_SP_GET_STUDENTS_LIST_FOR_I_GRADE", objParams);
           }
           catch (Exception ex)
           {
               throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GradeController.GetStudentsList_For_I_Grade --> " + ex.Message + " " + ex.StackTrace);
           }
           return ds;
       }

       //Added by Nikhil Lambe on 23/04/2021 to update grade of students
       public int UpdateStudents_For_I_Grade(int SessionNo, int SchemeNo, int SemesterNo, int Subject, int Idno)
       {
           int status = 0;
           try
           {
               SQLHelper objSqlHelper = new SQLHelper(connectionString);
               SqlParameter[] objParams = null;
               objParams = new SqlParameter[6];
               objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
               objParams[1] = new SqlParameter("@P_SCHEMENO", SchemeNo);
               objParams[2] = new SqlParameter("@P_SEMESTERNO", SemesterNo);
               objParams[3] = new SqlParameter("@P_SUBJECT", Subject);
               objParams[4] = new SqlParameter("@P_IDNO", Idno);
               objParams[5] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
               objParams[5].Direction = ParameterDirection.Output;

               object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_UPDATE_STUDENTS_FOR_I_GRADE", objParams, true);

               if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                   status = Convert.ToInt32(CustomStatus.RecordUpdated);
               else
                   status = Convert.ToInt32(CustomStatus.Error);
           }
           catch (Exception ex)
           {
               status = Convert.ToInt32(CustomStatus.Error);
               throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GradeController.UpdateStudents_For_I_Grade --> " + ex.Message + " " + ex.StackTrace);
           }
           return status;
       }

       //Added by Nikhil Lambe on 23/04/2021 to get students list for I grade by idno
       public DataSet GetSubjectsList_For_I_Grade_ByIdno(int SessionNo, int SemesterNo, int Idno)
       {
           DataSet ds = null;
           try
           {
               SQLHelper objSqlHelper = new SQLHelper(connectionString);
               SqlParameter[] objParams = null;

               objParams = new SqlParameter[3];
               objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
               objParams[1] = new SqlParameter("@P_SEMESTERNO", SemesterNo);
               objParams[2] = new SqlParameter("@P_IDNO", Idno);
               ds = objSqlHelper.ExecuteDataSetSP("PKG_SP_GET_SUBJECTS_LIST_I_GRADE_BY_IDNO", objParams);
           }
           catch (Exception ex)
           {
               throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GradeController.GetSubjectsList_For_I_Grade_ByIdno --> " + ex.Message + " " + ex.StackTrace);
           }
           return ds;
       }

       //Added by Nikhil Lambe on 24/04/2021 to update students by sub and idno.
       public int UpdateGradeBy_Id(int SessionNo, int SemesterNo, int Subject, int Idno)
       {
           int status = 0;
           try
           {
               SQLHelper objSqlHelper = new SQLHelper(connectionString);
               SqlParameter[] objParams = null;
               objParams = new SqlParameter[5];
               objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
               objParams[1] = new SqlParameter("@P_SEMESTERNO", SemesterNo);
               objParams[2] = new SqlParameter("@P_SUBJECT", Subject);
               objParams[3] = new SqlParameter("@P_IDNO", Idno);
               objParams[4] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
               objParams[4].Direction = ParameterDirection.Output;

               object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_UPDATE_STUDENTS_FOR_I_GRADE_BY_SUBJECT_ID", objParams, true);

               if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                   status = Convert.ToInt32(CustomStatus.RecordUpdated);
               else
                   status = Convert.ToInt32(CustomStatus.Error);
           }
           catch (Exception ex)
           {
               status = Convert.ToInt32(CustomStatus.Error);
               throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GradeController.UpdateGradeBy_Id --> " + ex.Message + " " + ex.StackTrace);
           }
           return status;
       }

       //Added by Nikhil Lambe on 24/04/2021 to get students list for I grade report excel
       public DataSet Get_Students_List_I_Grade_Excel_Rpt(int SessionNo, int College, int Degree, int Branch, int Scheme, int SemesterNo, int Subject)
       {
           DataSet ds = null;
           try
           {
               SQLHelper objSqlHelper = new SQLHelper(connectionString);
               SqlParameter[] objParams = null;

               objParams = new SqlParameter[3];
               objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
               objParams[1] = new SqlParameter("@P_COLLEGE", College);
               objParams[2] = new SqlParameter("@P_DEGREENO", Degree);
               objParams[2] = new SqlParameter("@P_BRANCHNO", Branch);
               objParams[2] = new SqlParameter("@P_SCHEMENO", Scheme);
               objParams[2] = new SqlParameter("@P_SEMESTERNO", SemesterNo);
               objParams[2] = new SqlParameter("@P_SUBJECT", Subject);
               ds = objSqlHelper.ExecuteDataSetSP("PKG_SP_GET_STUDENTS_LIST_I_GRADE_EXCEL", objParams);
           }
           catch (Exception ex)
           {
               throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GradeController.Get_Students_List_I_Grade_Excel_Rpt --> " + ex.Message + " " + ex.StackTrace);
           }
           return ds;
       }
       //public int AddGrade(Grade objGrade, int OrgID, bool Status)
       //{
       //    int status = 0;
       //    try
       //    {
       //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
       //        SqlParameter[] sqlParams = new SqlParameter[]
       //         {
       //             //new SqlParameter("@P_GRADE_TYPE",objGrade.GradeType),
       //             new SqlParameter("@P_GRADE_NAME", objGrade.GradeName),
       //             new SqlParameter("@P_COLLEGE_CODE", objGrade.CollegeCode),
       //             new SqlParameter("@P_ORG_ID", OrgID ),
       //             new SqlParameter("@P_STATUS", Status),
       //             new SqlParameter("@P_OUTPUT", status)
       //         };
       //        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

       //        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GRADE_TYPE_INSERT", sqlParams, true);

       //        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
       //            status = Convert.ToInt32(CustomStatus.RecordSaved);
       //        else
       //            status = Convert.ToInt32(CustomStatus.Error);
       //    }
       //    catch (Exception ex)
       //    {
       //        status = Convert.ToInt32(CustomStatus.Error);
       //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.AddBatch() --> " + ex.Message + " " + ex.StackTrace);
       //    }
       //    return status;
       //}

       //public int UpdateGrade(Grade objGrade, int OrgID, bool Status)
       //{
       //    int status;
       //    try
       //    {
       //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
       //        SqlParameter[] sqlParams = new SqlParameter[]
       //         {                    
       //             //new SqlParameter("@P_BATCHNAME",objBatch.BatchName),
       //             new SqlParameter("@P_GRADE_TYPE_NAME", objGrade.GradeName),
       //             new SqlParameter("@P_COLLEGE_CODE", objGrade.CollegeCode),
       //             new SqlParameter("@P_ORG_ID", OrgID ),
       //             new SqlParameter("@P_STATUS", Status),
       //             new SqlParameter("@P_OUTPUT",objGrade.GradeType)
       //         };
       //        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

       //        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GRADE_TYPE_UPDATE", sqlParams, true);

       //        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
       //            status = Convert.ToInt32(CustomStatus.RecordUpdated);
       //        else
       //            status = Convert.ToInt32(CustomStatus.Error);
       //    }
       //    catch (Exception ex)
       //    {
       //        status = Convert.ToInt32(CustomStatus.Error);
       //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.UpdateBatch() --> " + ex.Message + " " + ex.StackTrace);
       //    }
       //    return status;
       //}


       public int AddGrade(Grade objGrade, int OrgID, bool Status)
       {
           int status = 0;
           try
           {
               SQLHelper objSQLHelper = new SQLHelper(connectionString);
               SqlParameter[] sqlParams = new SqlParameter[]
                {
                    //new SqlParameter("@P_GRADE_TYPE",objGrade.GradeType),
                    new SqlParameter("@P_GRADE_NAME", objGrade.GradeName),
                    new SqlParameter("@P_COLLEGE_CODE", objGrade.CollegeCode),
                    new SqlParameter("@P_ORG_ID", OrgID ),
                    new SqlParameter("@P_STATUS", Status),
                    new SqlParameter("@P_OUTPUT", status)
                };
               sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

               object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GRADE_TYPE_INSERT", sqlParams, true);

               if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                   status = Convert.ToInt32(CustomStatus.RecordSaved);
               else if (obj.ToString() == "-1001")
                   status = Convert.ToInt32(CustomStatus.DuplicateRecord);
               else
                   status = Convert.ToInt32(CustomStatus.Error);
           }
           catch (Exception ex)
           {
               status = Convert.ToInt32(CustomStatus.Error);
               throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.AddBatch() --> " + ex.Message + " " + ex.StackTrace);
           }
           return status;
       }
       public int UpdateGrade(Grade objGrade, int OrgID, bool Status)
       {
           int status;
           try
           {
               SQLHelper objSQLHelper = new SQLHelper(connectionString);
               SqlParameter[] sqlParams = new SqlParameter[]
                {                    
                    //new SqlParameter("@P_BATCHNAME",objBatch.BatchName),
                    new SqlParameter("@P_GRADE_TYPE_NAME", objGrade.GradeName),
                    new SqlParameter("@P_COLLEGE_CODE", objGrade.CollegeCode),
                    new SqlParameter("@P_ORG_ID", OrgID ),
                    new SqlParameter("@P_STATUS", Status),
                    new SqlParameter("@P_OUTPUT",objGrade.GradeType)
                };
               sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

               object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GRADE_TYPE_UPDATE", sqlParams, true);

               if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                   status = Convert.ToInt32(CustomStatus.RecordUpdated);
               else if (obj.ToString() == "-1001")
                   status = Convert.ToInt32(CustomStatus.DuplicateRecord);
               else
                   status = Convert.ToInt32(CustomStatus.Error);
           }
           catch (Exception ex)
           {
               status = Convert.ToInt32(CustomStatus.Error);
               throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.UpdateBatch() --> " + ex.Message + " " + ex.StackTrace);
           }
           return status;
       }


        public int AddCourseActivityMaster(int activityno, string activityname, int ret)
       {
           int status = 0;
           try
           {
               SQLHelper objSQLHelper = new SQLHelper(connectionString);
               SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_ACTIVITY_TYPE_NO",activityno),
                    new SqlParameter("@P_ACTIVITY_NAME", activityname),
                    new SqlParameter("@P_ISACTIVE",ret),
                    new SqlParameter("@P_OUT", status)
                };
               sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

               object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_COURSE_ACTIVITY", sqlParams, true);

               if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                   status = Convert.ToInt32(CustomStatus.RecordSaved);
               else
                   status = Convert.ToInt32(CustomStatus.Error);
           }
           catch (Exception ex)
           {
               status = Convert.ToInt32(CustomStatus.Error);
               throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.AddBatch() --> " + ex.Message + " " + ex.StackTrace);
           }
           return status;
       }

       public int UpdateCourseActivityMaster(int activityno, string activityname, int ret)
       {
           int status = 0;
           try
           {
               SQLHelper objSQLHelper = new SQLHelper(connectionString);
               SqlParameter[] sqlParams = new SqlParameter[]
                {                    
                     new SqlParameter("@P_ACTIVITY_TYPE_NO",activityno),
                    new SqlParameter("@P_ACTIVITY_NAME", activityname),
                    new SqlParameter("@P_ISACTIVE",ret),
                    new SqlParameter("@P_OUT",status)
                };
               sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

               object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_COURSE_ACTIVITY", sqlParams, true);

               if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                   status = Convert.ToInt32(CustomStatus.RecordUpdated);
               else
                   status = Convert.ToInt32(CustomStatus.Error);
           }
           catch (Exception ex)
           {
               status = Convert.ToInt32(CustomStatus.Error);
               throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.UpdateBatch() --> " + ex.Message + " " + ex.StackTrace);
           }
           return status;
       }
 
    }
    }
    
}
