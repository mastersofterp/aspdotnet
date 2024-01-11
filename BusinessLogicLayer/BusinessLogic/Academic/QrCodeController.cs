//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : BATCH MASTER CONTROLLER                                              
// CREATION DATE : 02-SEPT-2009                                                         
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

    public class QrCodeController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet GetStudentDataForGradeCard(int admbatch, int sessionNO, int collegeid, int degreeNo, int branchNo, int semesterNo, string declareDate, string dateOfIssue, int studentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_ADMBATCHNO", admbatch),
                    new SqlParameter("@P_SESSIONNO", sessionNO),
                    new SqlParameter("@P_COLLEGEID", collegeid),
                    new SqlParameter("@P_DEGREENO", degreeNo),
                    new SqlParameter("@P_BRANCHNO", branchNo),
                    new SqlParameter("@P_SEMESTERNO", semesterNo),
                    new SqlParameter("@P_RESULTDECLAREDDATE", declareDate),
                    new SqlParameter("@P_DATE_OF_ISSUE", dateOfIssue),
                    new SqlParameter("@P_IDNO", studentId)
                };

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GRADE_CARD_REPORT_UG", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.QrCodeController.GetStudentResultData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet GetStudentDataForGradeCardPG(int admbatch, int sessionNO, int collegeid, int degreeNo, int branchNo, int semesterNo, string declaredDate, string dateOfIssue, int studentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_ADMBATCHNO", admbatch),
                    new SqlParameter("@P_SESSIONNO", sessionNO),
                    new SqlParameter("@P_COLLEGEID", collegeid),
                    new SqlParameter("@P_DEGREENO", degreeNo),
                    new SqlParameter("@P_BRANCHNO", branchNo),
                    new SqlParameter("@P_SEMESTERNO", semesterNo),
                    new SqlParameter("@P_RESULTDECLAREDDATE",declaredDate),
                    new SqlParameter("@P_DATE_OF_ISSUE", dateOfIssue),
                    new SqlParameter("@P_IDNO", studentId)
                };

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GRADE_CARD_REPORT_PG", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.QrCodeController.GetStudentResultData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet GetStudentDataForGradeCardPC(int admbatch, int sessionNO, int collegeid, int degreeNo, int branchNo, int semesterNo, string declaredDate, string dateOfIssue, int studentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_ADMBATCHNO", admbatch),
                    new SqlParameter("@P_SESSIONNO", sessionNO),
                    new SqlParameter("@P_COLLEGEID", collegeid),
                    new SqlParameter("@P_DEGREENO", degreeNo),
                    new SqlParameter("@P_BRANCHNO", branchNo),
                    new SqlParameter("@P_SEMESTERNO", semesterNo),
                    new SqlParameter("@P_RESULTDECLAREDDATE",declaredDate),
                    new SqlParameter("@P_DATE_OF_ISSUE", dateOfIssue),
                    new SqlParameter("@P_IDNO", studentId)
                };

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GRADE_CARD_REPORT_PC", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.QrCodeController.GetStudentResultData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet GetStudentDataForConvocation(int studentId, int DegreeNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                   
                    new SqlParameter("@P_IDNO", studentId),
                    new SqlParameter("@P_DEGREENO", DegreeNo),
                    new SqlParameter("@P_CERTNO", 5)
                    
                };

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACAD_CONVOCATION_CERTIFICATE_REPORT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.QrCodeController.GetStudentResultData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetStudentResultData(int studentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                   
                    new SqlParameter("@P_IDNO", studentId)
                    
                };

                ds = objDataAccess.ExecuteDataSetSP("PKG_TRANSCRIPT_SUB", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.QrCodeController.GetStudentResultData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int AddUpdateQrCode(int idnos, byte[] QR_IMAGE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNOS", idnos);
                objParams[1] = new SqlParameter("@P_QR_IMAGE", QR_IMAGE);
                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_STUDENT_QRCODE", objParams, true);
                retStatus = Convert.ToInt32(ret);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.QrCodeController.AddUpdateQrCode -> " + ex.ToString());
            }
            return retStatus;
        }



        public DataSet GetDetailsForAdmitCard(int sessionno, int semesterNo, int branchNo, int DEGREENO, int prev_status, int studentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_SESSIONNO", sessionno),
                    new SqlParameter("@P_SEMESTERNO ", semesterNo),
                    new SqlParameter("@P_BRANCHNO", branchNo),
                    new SqlParameter("@P_DEGREENO", DEGREENO),
                    new SqlParameter("@P_PREV_STATUS", prev_status),
                    new SqlParameter("@P_IDNO", studentId),
                    //new SqlParameter("@p_DATEOFISSUE", dateOfIssue) -- Hemant [27-10-2017]
                };

                ds = objDataAccess.ExecuteDataSetSP("PKG_REGIST_SP_REPORT_BULK_EXAM_REGISTSLIP", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.QrCodeController.GetStudentResultData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetStudentDataForGradeCardPG_Details(int sessionNO, int collegeid, int degreeNo, int branchNo, int semesterNo, string declaredDate, string dateOfIssue, string studentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    //new SqlParameter("@P_ADMBATCHNO", admbatch),
                    new SqlParameter("@P_COLLEGEID", collegeid),
                    new SqlParameter("@P_SESSIONNO", sessionNO),
                    new SqlParameter("@P_DEGREENO", degreeNo),
                    new SqlParameter("@P_BRANCHNO", branchNo),
                    new SqlParameter("@P_SEMESTERNO", semesterNo),
                    new SqlParameter("@P_RESULTDECLAREDDATE",declaredDate),
                    new SqlParameter("@P_DATE_OF_ISSUE", dateOfIssue),
                    new SqlParameter("@P_IDNO", studentId)
                };

                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GRADE_CARD_REPORT_PG_DETAILS", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.QrCodeController.GetStudentResultData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Added By S.Patil - 19july2019 
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="DegreeNo"></param>
        /// <returns></returns>
        public DataSet GetStudentDataForDisplayInExcel(int sessionNO, int collegeid, int degreeNo, int branchNo, int semesterNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {   
                    new SqlParameter("@P_SESSIONNO", sessionNO),
                    new SqlParameter("@P_COLL", collegeid),
                    new SqlParameter("@P_DEGREENO", degreeNo),
                    new SqlParameter("@P_BRANCHO", branchNo),
                    new SqlParameter("@P_SEMESTERNO", semesterNo)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_GET_RESULT_DATA_FOR_EXCEL", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.QrCodeController.GetStudentResultData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //Added by Deepali on 27/08/2020
        public DataSet GetStudentDataForQRCode(int sessionNO, int collegeid, int degreeNo, int branchNo, int semesterNo, string batchNo, string declaredDate, string dateOfIssue, string studentId, string UserName)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_COLLEGEID", collegeid),
                    new SqlParameter("@P_SESSIONNO", sessionNO),
                    new SqlParameter("@P_DEGREENO", degreeNo),
                    new SqlParameter("@P_BRANCHNO", branchNo),
                    new SqlParameter("@P_SEMESTERNO", semesterNo),
                    new SqlParameter("@P_ADMBATCHNO", batchNo),
                    new SqlParameter("@P_RESULTDECLAREDDATE",declaredDate),
                    new SqlParameter("@P_DATE_OF_ISSUE", dateOfIssue),
                    new SqlParameter("@P_IDNO", studentId),
                    new SqlParameter("@P_USER_FULL_NAME", UserName)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ACD_GRADE_CARD_REPORT_PG", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.QrCodeController.GetStudentResultData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Added By Dileep Kare
        /// on 31032020
        /// </summary>
        /// <param name="IDNO"></param>
        /// <param name="degreeno"></param>
        /// <param name="branchno"></param>
        /// <returns></returns>
        public int GenerationofTranscriptnumber(int IDNO, int degreeno, int branchno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                if (objSQLHelper.ExecuteNonQuerySP("PKG_GENERATE_TRANSCRIPT_NUMBER", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpDateStudent-> " + ex.ToString());
            }
            return retStatus;
        }

        /// <summary>
        /// Added By Deepali 
        /// on 19/12/2020
        /// </summary>
        /// <param name="IDNO"></param>
        /// <param name="degreeno"></param>
        /// <param name="branchno"></param>
        /// <returns></returns>
        public int GenerateCertficateSerialNumber(int IDNO, int degreeno, int branchno, int certNO, int semesterno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                objParams[3] = new SqlParameter("@P_CERTNO", certNO);
                objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                if (objSQLHelper.ExecuteNonQuerySP("PKG_CERTIFICATE_SRNO_GENERATION", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpDateStudent-> " + ex.ToString());
            }
            return retStatus;
        }



        public DataSet GetStudentDataForQRCodeCert(int studentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                   
                    new SqlParameter("@P_IDNO", studentId)
                    
                };

                ds = objDataAccess.ExecuteDataSetSP("PKG_GET_STUD_DATA_FOR_QRCODE", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.QrCodeController.GetStudentResultData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //added by deepali on 18/01/2020
        public DataSet GetBacklogStudentDataForDisplayInExcel()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[0];

                ds = objDataAccess.ExecuteDataSetSP("PKG_EXAM_STUDENT_FAILED_COURSEWISE_REPORT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.QrCodeController.GetStudentResultData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //New Method Added by Dileep kare on 24.04.2021

        public DataSet GetTrReportStudentDetails(int Sessionno, int College_id, int Degreeno, int Branchno, int stud_type, int SemesterNo)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objDataAccess = new SQLHelper(connectionString);

                SqlParameter[] sqlParams = new SqlParameter[7];
                sqlParams[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                sqlParams[1] = new SqlParameter("@P_COLLEGE_ID", College_id);
                sqlParams[2] = new SqlParameter("@P_DEGREENO", Degreeno);
                sqlParams[3] = new SqlParameter("@P_BRANCHNO", Branchno);
                sqlParams[4] = new SqlParameter("@P_STUD_TYPE", stud_type);
                sqlParams[5] = new SqlParameter("@P_SEMESTERNO", SemesterNo);
                sqlParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                sqlParams[6].Direction = ParameterDirection.Output;

                ds = objDataAccess.ExecuteDataSetSP("PKG_EXAM_MARKS_DETAILS", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.QrCodeController.GetTrReportStudentDetails() --> " + ex.Message + " " + ex.StackTrace);
            }

            return ds;
        }

        /// <summary>
        /// Added By Dileep on 05.06.2021
        /// </summary>
        /// <param name="Sessionno"></param>
        /// <returns></returns>
        public DataSet GetResultStatistics(int sessionno, int prev_status)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_SESSIONNO", sessionno),
                    new SqlParameter("@P_PREV_STATUS", prev_status),
                    //new SqlParameter("@p_DATEOFISSUE", dateOfIssue) -- Hemant [27-10-2017]
                };

                ds = objDataAccess.ExecuteDataSetSP("PKG_EXAM_GET_RESULT STATISTICS", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.QrCodeController.GetResultStatistics() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int AddQrCodeForStudentIDCard(int idnos, byte[] QR_IMAGE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNOS", idnos);
                objParams[1] = new SqlParameter("@P_QR_IMAGE", QR_IMAGE);
                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_UPDATE_STUDENT_QRCODE_FOR_IDCARD", objParams, true);
                retStatus = Convert.ToInt32(ret);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.QrCodeController.AddQrCodeForStudentIDCard -> " + ex.ToString());
            }
            return retStatus;
        }

        // ADDED by SHUBHAM on 22/12/23 for RCPIPER QR CODE ON REPORT
        public int AddUpdateQrCodeHallTicket(int idno, byte[] QR_IMAGE, int sem, int schemeno, int sessionno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_QR_IMAGE", QR_IMAGE);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", sem);
                objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[4] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_STUDENT_QRCODE_HALLTICKET", objParams, true);
                retStatus = Convert.ToInt32(ret);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.QrCodeController.AddUpdateQrCode -> " + ex.ToString());
            }
            return retStatus;
        }
    }
}


