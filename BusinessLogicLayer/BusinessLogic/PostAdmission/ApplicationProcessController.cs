using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;


namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class ApplicationProcessController
    {
        string uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet GetApplicationProcess(ApplicationProcess objAP)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                SqlParameter[] objParams = new SqlParameter[] 
                      { 
                         new SqlParameter("@P_BATCHNO",objAP.BatchNo),     
                         new SqlParameter("@P_UGPGOT",objAP.UGPGOT), 
                         new SqlParameter("@P_DEGREENO",objAP.DegreeNo), 
                         new SqlParameter("@P_BRANCHNO",objAP.BranchNo),
                         new SqlParameter("@P_APPLICATION_STAGE",objAP.ApplicationStage),                        
                         new SqlParameter("@P_UA_TYPE",objAP.UaType),
                         new SqlParameter("@P_UA_NO",objAP.UaNo),
                         new SqlParameter("@P_FACULTY_UANO",objAP.FacultyUaNo),
                         new SqlParameter("@P_LOGINURL",objAP.LoginUrl)
                         
                      };
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ADM_GET_APPLICATION_PROCESS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ApplicationProcessController.GetApplicationProcess() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public int InsAppProcessExamSchedule(ApplicationProcess objAP)
        {
            int retstatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[11];
                objParams[0] = new SqlParameter("@P_ADMBATCH", objAP.BatchNo);
                objParams[1] = new SqlParameter("@P_DEGREENO", objAP.DegreeNo);
                objParams[2] = new SqlParameter("@P_BRACNCHNO", objAP.BranchNo);
                objParams[3] = new SqlParameter("@P_USERNO", objAP.UserNo);
                objParams[4] = new SqlParameter("@P_EXAMSCHEDULE", objAP.ExamSchedule);
                objParams[5] = new SqlParameter("@P_SCHEDULE_NO", objAP.ScheduleNo);
                objParams[6] = new SqlParameter("@P_SCHEDULE_NOS", objAP.ScheduleNos);
                objParams[7] = new SqlParameter("@P_APPLICATION_STAGE", objAP.ApplicationStage);
                objParams[8] = new SqlParameter("@P_UA_NO", objAP.UaNo);
                objParams[9] = new SqlParameter("@P_FACULTY_UANO", objAP.FacultyUaNo);
                objParams[10] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[10].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ADMP_INS_APP_PROCESS", objParams, true);

                if (Convert.ToInt32(ret) == -99)
                    retstatus = Convert.ToInt32(ret);  //Convert.ToInt32(CustomStatus.Error);                  
                else
                    retstatus = Convert.ToInt32(ret);  //Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ApplicationProcessController.InsAppProcessExamSchedule() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retstatus;
        }
        public int InsUpdEmailSmsSendLog(ApplicationProcess objAP)
        {
            int retstatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_USERNO", objAP.UserNo);
                objParams[1] = new SqlParameter("@P_APPLICATIONSTAGEID", objAP.ApplicationStage);
                objParams[2] = new SqlParameter("@P_UA_NO", objAP.UaNo);
                objParams[3] = new SqlParameter("@P_PROCESSTYPE", objAP.ProcessType);
                objParams[4] = new SqlParameter("@P_DESCRIPTION", objAP.Description);
                objParams[5] = new SqlParameter("@P_ESUBJECT", objAP.ESubject);
                objParams[6] = new SqlParameter("@P_EWBODY", objAP.EWBody);
                objParams[7] = new SqlParameter("@P_TEMPLATEID", objAP.TemplateId);
                objParams[8] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ADMP_APP_PROCESS_INSUPD_EMAILSMS_SENDLOG", objParams, true);

                if (Convert.ToInt32(ret) == 1)
                    retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    retstatus = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ApplicationProcessController.UpdApplicationProcess() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retstatus;
        }
        public DataSet GetScheduleForSMSEmail(ApplicationProcess objAP)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                SqlParameter[] objParams = new SqlParameter[] 
                      { 
                         new SqlParameter("@P_ADMBATCH",objAP.BatchNo),          
                      };
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ADMP_GET_SCHEDULE_EMAILSMS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ApplicationProcessController.GetScheduleForSMSEmail() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet GetScheduleStudent(ApplicationProcess objAP)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                SqlParameter[] objParams = new SqlParameter[] 
                      { 
                         new SqlParameter("@P_ADMBATCH",objAP.BatchNo),     
                         new SqlParameter("@P_DEGREENO",objAP.DegreeNo), 
                         new SqlParameter("@P_BRACNCHNO",objAP.BranchNo),   
                         new SqlParameter("@P_SCHEDULE_NO",objAP.ScheduleNos),   
                      };
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ADMP_GET_SCHEDULE_STUDENT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ApplicationProcessController.GetScheduleStudent() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet GetVerifyDocumentList(ApplicationProcess objAP)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                SqlParameter[] objParams = new SqlParameter[] 
                      { 
                         new SqlParameter("@P_USERNO",objAP.Userno),                         
                      };
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ADMP_GET_VERIFYDOCUMENTLIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ApplicationProcessController.GetVerifyDocumentList() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public int InsUpdVerifyDocument(ApplicationProcess objAP,string IsVerify)
        {
            int retstatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_XML", IsVerify);       
                objParams[1] = new SqlParameter("@P_UA_NO", objAP.UaNo);
                objParams[2] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ADMP_INSUPD_VERIFYDOCUMENTLIST", objParams, true);

                if (Convert.ToInt32(ret) == 2)
                    retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                else
                    retstatus = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ApplicationProcessController.InsUpdVerifyDocument() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retstatus;
        }
        public DataSet GetMarksheetList(ApplicationProcess objAP)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                SqlParameter[] objParams = new SqlParameter[] 
                      { 
                         new SqlParameter("@P_USERNO", objAP.Userno),     
                         new SqlParameter("@P_QUALIFYNO", objAP.QualifyNo)   
                      };
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ADMP_GET_MARKSHEETLIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ApplicationProcessController.GetMarksheetList() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet GetSMSTemplate(int PageNo, string TempName)
        {

            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_PAGENO", PageNo);
                objParams[1] = new SqlParameter("@P_TEMPLATE_NAME", TempName);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_SMS_TEMPLATE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUsersByUserType-> " + ex.ToString());
            }
            return ds;

        }      
        #region Preview Form Methods
        public DataSet GetAllBranchDetails(int UserNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_USERNO", UserNo);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_BRANCHPREFERENCE_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ApplicationProcessController.GetAllBranchDetails-> " + ex.ToString());
            }
            return ds;
        }
        public DataSet GetRecordForPersonalDetails(int userno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_USERNO", userno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_PREVIEW_USERDATA_DAIICT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ApplicationProcessController.GetRecordForPersonalDetails() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet GetEducationalDetails(int userno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_USERNO", userno);
                //PKG_GET_EDU_DETAILS
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ADMP_GET_EDU_DETAILS_BY_USERNO_RAJAGIRI", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ApplicationProcessController.GetEducationalDetails() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet GetReservationDetails(int UserNO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(uaims_constr);

                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_USERNO", UserNO);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ADMP_GET_RESERV_DETAILS_BY_USERNO_RAJAGIRI", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("RAJAGIRI.BusinessLayer.BusinessLogic.StudentReservationDetailsController.GetAll_StudReservDetails-> " + ex.ToString());
            }
            return ds;
        }

        #endregion

    }

}
