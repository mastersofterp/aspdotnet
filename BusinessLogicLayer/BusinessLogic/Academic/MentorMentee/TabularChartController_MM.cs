using IITMS;
using IITMS.SQLServer.SQLDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessLogic.Academic.MentorMentee
{
    public class TabularChartController_MM
    {
        private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

       
        public DataSet GetBacklogStudentDataForDisplayInExcel(int UANO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = null;

                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_USERNO", UANO);

                ds = objDataAccess.ExecuteDataSetSP("PKG_EXAM_STUDENT_FAILED_COURSEWISE_REPORT_MM", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.QrCodeController.GetStudentResultData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetTrReportStudentDetails(int Sessionno, int College_id, int Degreeno, int Branchno, int stud_type, int SemesterNo, int FactAdvisor)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objDataAccess = new SQLHelper(_nitprm_constr);

                SqlParameter[] sqlParams = new SqlParameter[7];
                sqlParams[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                sqlParams[1] = new SqlParameter("@P_COLLEGE_ID", College_id);
                sqlParams[2] = new SqlParameter("@P_DEGREENO", Degreeno);
                sqlParams[3] = new SqlParameter("@P_BRANCHNO", Branchno);
                sqlParams[4] = new SqlParameter("@P_STUD_TYPE", stud_type);
                sqlParams[5] = new SqlParameter("@P_FACTADVISOR", FactAdvisor);
                //sqlParams[5] = new SqlParameter("@P_SEMESTERNO", SemesterNo);
                sqlParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                sqlParams[6].Direction = ParameterDirection.Output;

                ds = objDataAccess.ExecuteDataSetSP("PKG_EXAM_MARKS_DETAILS_MM", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.QrCodeController.GetTrReportStudentDetails() --> " + ex.Message + " " + ex.StackTrace);
            }

            return ds;
        }
    }
}
