using System;
using System.Data;
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS;
namespace BusinessLogicLayer.BusinessLogic.Academic.MentorMentee
{
    public class StudentControllerDetails_MM
    {
        string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


      public DataTableReader GetStudentCompleteDetails(int idno)
      {
          DataTableReader dtr = null;

          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] objParams = null;
              objParams = new SqlParameter[1];
              objParams[0] = new SqlParameter("@P_IDNO", idno);

              dtr = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUDENT_COMPLETEDETAILS_BY_IDNO", objParams).Tables[0].CreateDataReader();

          }
          catch (Exception ex)
          {
              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentCompleteDetails->" + ex.ToString());
          }
          return dtr;
      }

      public DataSet RetrieveStudentCurrentRegDetails(int idno)
      {
          DataSet ds = null;
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] objParams = null;
              objParams = new SqlParameter[1];
              objParams[0] = new SqlParameter("@P_IDNO", idno);

              ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_REGISTRATION_DETAILS_BY_ID", objParams);

          }
          catch (Exception ex)
          {
              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
          }
          return ds;
      }

      public DataSet RetrieveStudentAttendanceDetails(int sessionno, int schemeno, int semesterno, int idno)
      {
          DataSet ds = null;
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] objParams = null;
              objParams = new SqlParameter[4];
              objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
              objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
              objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
              objParams[3] = new SqlParameter("@P_IDNO", idno);

              ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_REPORT_STUDENT_ATTENDANCE", objParams);

          }
          catch (Exception ex)
          {
              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
          }
          return ds;
      }

      public DataSet RetrieveStudentFeesDetails(int idno,int uano)
      {
          DataSet ds = null;
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] objParams = null;
              objParams = new SqlParameter[2];
              objParams[0] = new SqlParameter("@P_IDNO", idno);
              objParams[1] = new SqlParameter("@P_UANO", uano);

              ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_FEES_DETAILS_BY_ID", objParams);

          }
          catch (Exception ex)
          {
              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentFeesDetails-> " + ex.ToString());
          }
          return ds;
      }


      public DataSet RetrieveStudentYearWiseFeesDetails(int idno)
      {
          DataSet ds = null;
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] objParams = null;
              objParams = new SqlParameter[1];
              objParams[0] = new SqlParameter("@P_IDNO", idno);

              ds = objSQLHelper.ExecuteDataSetSP("PKG_YEAR_WISE_STUDENT_FEES_DETAILS", objParams);

          }
          catch (Exception ex)
          {
              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentYearWiseFeesDetails-> " + ex.ToString());
          }
          return ds;
      }


      public DataSet RetrieveOtherFeeDetails(int idno)
      {
          DataSet ds = null;
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] objParams = null;
              objParams = new SqlParameter[1];
              objParams[0] = new SqlParameter("@P_IDNO", idno);

              ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_MISC_FEES_DETAILS", objParams);

          }
          catch (Exception ex)
          {
              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveOtherFeeDetails-> " + ex.ToString());
          }
          return ds;
      }


      public DataSet RetrieveStudentCertificateDetails(int idno)
      {
          DataSet ds = null;
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] objParams = null;
              objParams = new SqlParameter[1];
              objParams[0] = new SqlParameter("@P_IDNO", idno);

              ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_CERT_ISUUED_DETAILS_BY_ID", objParams);

          }
          catch (Exception ex)
          {
              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCertificateDetails-> " + ex.ToString());
          }
          return ds;
      }

      public DataSet GetAllRemarkDetails(int idno)
      {
          DataSet ds = null;

          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] objParams = new SqlParameter[1];
              objParams[0] = new SqlParameter("@P_IDNO", idno);

              ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_GET_REMARK", objParams);

          }
          catch (Exception ex)
          {
              return ds;
              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentcontroller.GetAllQualifyExamDetails-> " + ex.ToString());
          }

          return ds;
      }

      public DataSet GetStudentRefunds(int idno)
      {
          DataSet ds = null;
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] objParams = null;
              objParams = new SqlParameter[1];
              objParams[0] = new SqlParameter("@P_IDNO", idno);

              ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STUDENT_REFUND_AMOUNT", objParams);

          }
          catch (Exception ex)
          {
              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentRefunds-> " + ex.ToString());
          }
          return ds;
      }

      //public DataSet RetrieveRegDetailsByIdnoAndSession(int idno, int sessionno)
      //{
      //    DataSet ds = null;
      //    try
      //    {
      //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
      //        SqlParameter[] objParams = null;
      //        objParams = new SqlParameter[2];
      //        objParams[0] = new SqlParameter("@P_IDNO", idno);
      //        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
      //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_REGISTRATION_DETAILS_BY_ID_AND_SESSION", objParams);

      //    }
      //    catch (Exception ex)
      //    {
      //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveRegDetailsByIdnoAndSession-> " + ex.ToString());
      //    }
      //    return ds;
      //}

      public DataSet GetDetailsOfInternalMarks(int idno, int sessionno)
      {
          DataSet ds = null;
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] objParams = null;
              objParams = new SqlParameter[2];
              objParams[0] = new SqlParameter("@P_IDNO", idno);
              objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
              ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_GET_INTERNAL_MARKS_DETAILS", objParams);

          }
          catch (Exception ex)
          {
              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetDetailsOfInternalMarks-> " + ex.ToString());
          }
          return ds;
      }


      public DataSet GetDetailsOfAttendanceByIdno(int idno, int sessionno)
      {
          DataSet ds = null;
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] objParams = null;
              objParams = new SqlParameter[2];
              objParams[0] = new SqlParameter("@P_IDNO", idno);
              objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
              ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_GET_ATTENDANCE_DETAILS_BY_IDNO", objParams);

          }
          catch (Exception ex)
          {
              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetDetailsOfAttendanceByIdno-> " + ex.ToString());
          }
          return ds;
      }

      public DataSet Getpromotionstatus(int idno)
      {
          DataSet ds = null;
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] objParams = new SqlParameter[1];
              objParams[0] = new SqlParameter("@P_ID", idno);
              ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_PROMOTION_STATUS_BYID", objParams);
          }
          catch (Exception ex)
          {
              throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetSemesterHistoryDetails() --> " + ex.Message + " " + ex.StackTrace);
          }
          return ds;
      }


      public DataSet GetDetailsOfGradeRealease(int idno, int sessionno)
      {
          DataSet ds = null;
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] objParams = null;
              objParams = new SqlParameter[2];
              objParams[0] = new SqlParameter("@P_IDNO", idno);
              objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
              ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_INTERMEDIATE_GRADE_DETAILS_BY_ID", objParams);

          }
          catch (Exception ex)
          {
              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetDetailsOfInternalMarks-> " + ex.ToString());
          }
          return ds;
      }

      public DataSet RetrieveRegDetailsByIdnoAndSession(int idno, int sessionno)
      {
          DataSet ds = null;
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] objParams = null;
              objParams = new SqlParameter[2];
              objParams[0] = new SqlParameter("@P_IDNO", idno);
              objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
              ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_REGISTRATION_DETAILS_BY_ID_AND_SESSION", objParams);

          }
          catch (Exception ex)
          {
              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveRegDetailsByIdnoAndSession-> " + ex.ToString());
          }
          return ds;
      }

      public DataSet GetSemsesterwiseMarkDetails(int idno, int sessionno, int semesterno)
      {
          DataSet ds = null;
          try
          {
              SQLHelper objsqlhelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] objparams = new SqlParameter[3];
              objparams[0] = new SqlParameter("@P_IDNO", idno);
              objparams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
              objparams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);

              ds = objsqlhelper.ExecuteDataSetSP("PKG_ACD_GET_SEMESTERWISE_STUD_DETAILS", objparams);

          }
          catch (Exception ex)
          {
              throw new IITMSException("IITMS.NITPRM.BUSINESSLAYER.BUSINESSLOGIC.STUDENTCONTROLLER.GETSESSIONWISEMARKDETAILS -->" + ex.Message + " " + ex.StackTrace);
          }
          return ds;
      }

      public DataSet GetSemesterHistoryDetails(int idno, int Semesterno)
      {
          DataSet ds = null;
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] objParams = new SqlParameter[2];
              objParams[0] = new SqlParameter("@P_IDNO", idno);
              objParams[1] = new SqlParameter("@P_SEMESTERNO", Semesterno);


              ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GETALL_SEMESTERWISE_STUDDETAILS", objParams);
          }
          catch (Exception ex)
          {
              throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetSemesterHistoryDetails() --> " + ex.Message + " " + ex.StackTrace);
          }
          return ds;
      }

      public DataSet GetSemesterHistoryDetailsForRevalResult(int idno, int sessionno, int semesterno)
      {
          DataSet ds = null;
          try
          {
              SQLHelper objsqlhelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] objparams = new SqlParameter[3];
              objparams[0] = new SqlParameter("@P_IDNO", idno);
              objparams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
              objparams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);

              ds = objsqlhelper.ExecuteDataSetSP("PKG_ACD_GET_SEMESTERWISE_STUD_DETAILS_REVAL", objparams);

          }
          catch (Exception ex)
          {
              throw new IITMSException("IITMS.NITPRM.BUSINESSLAYER.BUSINESSLOGIC.STUDENTCONTROLLER.GETSESSIONWISEMARKDETAILS -->" + ex.Message + " " + ex.StackTrace);
          }
          return ds;
      }

    

    

      public DataSet GetReceiptInfoCompleteDetails(int IDNO, string Receipt_Code, int Semesterno)
      {
          DataSet ds = null;
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] objParams = null;
              objParams = new SqlParameter[3];
              objParams[0] = new SqlParameter("@P_IDNO", IDNO);
              objParams[1] = new SqlParameter("@P_RECEIPT_CODE", Receipt_Code);
              objParams[2] = new SqlParameter("@P_SEMESTERNO", Semesterno);
              ds = objSQLHelper.ExecuteDataSetSP("ACD_SHOW_RECEIPT_INFO_ON_POPUP_COMPLETE_DETAILS", objParams);

          }
          catch (Exception ex)
          {
              throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FeeCollectionController.GetReceiptInfoCompleteDetails-> " + ex.ToString());
          }
          return ds;

      }


      public DataSet GetDetailsOfInternalMarksHeader(int idno, int sessionno)
      {
          DataSet ds = null;
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] objParams = null;
              objParams = new SqlParameter[2];
              objParams[0] = new SqlParameter("@P_IDNO", idno);
              objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
              ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_COURSEWISE_GRADE_HEAD_NEW", objParams);

          }
          catch (Exception ex)
          {
              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetDetailsOfInternalMarks-> " + ex.ToString());
          }
          return ds;
      }


      public DataSet GetDetailsOfInternalMarks1(int idno, int sessionno)
      {
          DataSet ds = null;
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] objParams = null;
              objParams = new SqlParameter[2];
              objParams[0] = new SqlParameter("@P_IDNO", idno);
              objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
              ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SUBEXAMNAME_BY_PARTICULATUR_IDNO_Subexam", objParams);

          }
          catch (Exception ex)
          {
              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetDetailsOfInternalMarks-> " + ex.ToString());
          }
          return ds;
      }

      public DataSet GetSearchDropdownDetails(string ddlname)
      {
          try
          {
              DataSet ds = null;
              SQLHelper objDataAccessLayer = new SQLHelper(_UAIMS_constr);
              SqlParameter[] objParams = null;
              objParams = new SqlParameter[1];
              objParams[0] = new SqlParameter("@P_DDLNAME", ddlname);

              ds = objDataAccessLayer.ExecuteDataSetSP("PKG_GET_SEARCH_DROPDOWN_DETAILS", objParams);

              return ds;
          }
          catch (Exception Ex)
          {
              throw new Exception(Ex.Message);
          }
      }

      public DataSet RetrieveStudentDetailsAdmCancel(string search, string category, int uano)
      {
          DataSet ds = null;
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] objParams = null;
              objParams = new SqlParameter[3];
              objParams[0] = new SqlParameter("@P_SEARCHSTRING", search);
              objParams[1] = new SqlParameter("@P_SEARCH", category);
              objParams[2] = new SqlParameter("@P_UA_NO", uano);           //Added by sachin Lohakare 25-04-2023


              ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_SEARCH_STUDENT_NEW_DESIGN_ADMCANCEL_MM", objParams);
          }
          catch (Exception ex)
          {
              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentDetailsAdmCancel-> " + ex.ToString());
          }
          return ds;
      }

      public DataSet RetrieveStudentSemesterNumberResult(int idno)
      {
          DataSet ds = null;
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] objParams = null;
              objParams = new SqlParameter[1];
              objParams[0] = new SqlParameter("@P_IDNO", idno);

              ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUD_CURRENT_SEMESTER__RESULT", objParams);

          }
          catch (Exception ex)
          {
              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
          }
          return ds;
      }

      public DataSet AdmfessDues(int idno, int Semesterno)
      {
          DataSet ds = null;

          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] objParams = null;
              objParams = new SqlParameter[2];
              objParams[0] = new SqlParameter("@P_IDNO", idno);
              objParams[1] = new SqlParameter("@P_SEMESTERNO", Semesterno);
              ds = objSQLHelper.ExecuteDataSetSP("PKG_IS_ADMFEE_DUES", objParams);
          }
          catch (Exception ex)
          {
              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetBulkSemesterPromotionData->" + ex.ToString());
          }
          return ds;
      }
    }
}
