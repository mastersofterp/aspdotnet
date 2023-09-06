using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

namespace BusinessLogicLayer.BusinessLogic
{
   public class ReportController
    {
        string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        // Added by Yograj on 21-03-2023
        public DataSet GetPersonalDetails(int USERNO)
        {
            

            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_USERNO", USERNO);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ONLINEADMP_FORM_REPORT_DAIICT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMS.IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PDPReportController.getStudentCertificate-> " + ex.ToString());
            }
            return ds;
        }

        //Added By Kajal Jaiswal on 04-04-2023 getAddressDetails
        public DataSet GetAddressDetails(int userno)
        {
           
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_USERNO", userno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_APPLICANT_ADDRESS_DETAILS_BY_USERNO_RPT_DAIICT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMS.IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PDPReportController.getStudentCertificate-> " + ex.ToString());
            }
            return ds;
        }


        //Added By Saurabh Kumare on 04-04-2023
        public DataSet GetEducationalDetails(int userno)
        {
           
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_USERNO", userno);                      
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ADMP_GET_EDU_DETAILS_BY_USERNO_DAIICT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMS.IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PDPReportController.getStudentCertificate-> " + ex.ToString());
            }
            return ds;
        }


        //Added By Kajal Jaiswal on 04-04-2023
        public DataSet GetApplyProgramDetails(int userno, int CHECK)
        {

            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_USERNO", userno);
                objParams[1] = new SqlParameter("@P_CHECK", CHECK);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_APPLICANT_PROGRAMME_BY_USERNO_RPT_DAIICT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMS.IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PDPReportController.getStudentCertificate-> " + ex.ToString());
            }
            return ds;
        }


        //Added By Kajal Jaiswal on 05-04-2023
        public DataSet GetTestScoreDetails(int userno)
        {
            
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_USERNO", userno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ADMP_GET_TEST_SCORE_DETAILS_BY_USERNO_RPT_DAIICT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMS.IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PDPReportController.getStudentCertificate-> " + ex.ToString());
            }
            return ds;
        }

        //Added By Kajal Jaiswal on 05-04-2023
        public DataSet GetDocumentUploadDetails(int userno)
        {
            
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_USERNO", userno);          
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ADMP_GET_UPLOAD_DOCS_USERNO_RPT_DAIICT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMS.IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PDPReportController.getStudentCertificate-> " + ex.ToString());
            }
            return ds;
        }

        //Added By Kajal Jaiswal on 25-04-2023
        public DataSet GetFinalSubmission(int userno)
        {

            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_USERNO", userno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ADMP_GET_FINAL_SUBMISSION_RPT_DAIICT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMS.IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PDPReportController.getStudentCertificate-> " + ex.ToString());
            }
            return ds;
        }

    }
}
