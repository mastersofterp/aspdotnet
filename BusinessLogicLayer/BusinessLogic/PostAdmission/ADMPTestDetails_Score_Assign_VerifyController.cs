using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
   public class ADMPTestDetails_Score_Assign_VerifyController
    {
        private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet GetTestScoreDataList(int ScoreId, int BatchNo, int ProgramType, int userno, int ua_no, int Payment_status)
        {
            DataSet ds = null;
            try
            {
                string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_SCOREID", ScoreId);
                objParams[1] = new SqlParameter("@P_BATCHNO", BatchNo);
                objParams[2] = new SqlParameter("@P_PROGRAM_TYPE", ProgramType);
                objParams[3] = new SqlParameter("@P_USERNO", userno);
                objParams[4] = new SqlParameter("@P_UA_NO", ua_no);
                objParams[5] = new SqlParameter("@P_PAYMENT_STATUS", Payment_status);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ADMP_RET_ALL_TESTSCORE_DATA", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("BusinessLayer.BusinessLogic.ACDAdmissionDemoController.GetTestScoreDataList->" + ex.ToString());
            }
            return ds;
        }

        public string SaveTestScore(int UserNo, string XmlData, int isverify)
        {
            string retStatus = string.Empty;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                
                objParams[0] = new SqlParameter("@P_UANO", UserNo);
                objParams[1] = new SqlParameter("@P_XML", XmlData);
                objParams[2] = new SqlParameter("@P_ISVERIFY", isverify);
                objParams[3] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ADMP_TESTSCORE_VERIFY_DAIICT", objParams, true);
                retStatus = ret.ToString();

                return retStatus;
            }
            catch (Exception ex)
            {
                throw new IITMSException("BusinessLayer.BusinessLogic.ACDAdmissionDemoController.SaveTestScore->" + ex.ToString());
            }
        }

        public DataSet GetTestScoreDataListByFaculty(int ScoreId, int BatchNo, int ProgramType, int Ua_No, int PaymentStatus)
        {
            DataSet ds = null;

            string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_SCOREID", ScoreId);
                objParams[1] = new SqlParameter("@P_BATCHNO", BatchNo);
                objParams[2] = new SqlParameter("@P_PROGRAM_TYPE", ProgramType);
                objParams[3] = new SqlParameter("@P_UA_NO", Ua_No);
                objParams[4] = new SqlParameter("@P_STATUS", PaymentStatus);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ADMP_RET_ALL_TESTSCORE_DATA_FOR_FACULTY_DAIICT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("ADMPTestDetail_Assign_Faculty.GetTestScoreDataListByFaculty() ->" + ex.ToString());
            }
            return ds;
        }

        public string AssignFacultyTestScore(int Ua_No, string XmlData)
        {
            string retStatus = string.Empty;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];

                objParams[0] = new SqlParameter("@P_UA_NO", Ua_No);
                objParams[1] = new SqlParameter("@P_XML", XmlData);
                objParams[2] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ADMP_TESTSCORE_FACULTY_ASSIGN_DAIICT", objParams, true);
                retStatus = ret.ToString();

                return retStatus;
            }
            catch (Exception ex)
            {
                throw new IITMSException("BusinessLayer.BusinessLogic.ADMPTestDetails_Score_Assign_VerifyController.AssignFacultyTestScore->" + ex.ToString());
            }
        }
    }
}
