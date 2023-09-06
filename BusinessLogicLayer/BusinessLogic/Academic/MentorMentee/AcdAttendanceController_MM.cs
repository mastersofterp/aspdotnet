using System;
using System.Data;
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS;


namespace BusinessLogicLayer.BusinessLogic.Academic.MentorMentee
{
    public class AcdAttendanceController_MM
    {
        string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet GetSubjectWiseDetailsExcelReport(int sessionno, int schemeno, int semno, int sectionno, DateTime frmdate, DateTime todate, string CONDITIONS, int PERCENTAGE, int COURSENO, int SUBID, int College_id, int uano)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[12];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semno);
                objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                objParams[4] = new SqlParameter("@P_FROMDATE", frmdate);
                objParams[5] = new SqlParameter("@P_TODATE", todate);
                objParams[6] = new SqlParameter("@P_CONDITIONS", CONDITIONS);
                objParams[7] = new SqlParameter("@P_PERCENTAGE", PERCENTAGE);
                objParams[8] = new SqlParameter("@P_COURSENO", COURSENO);
                objParams[9] = new SqlParameter("@P_SUBID", SUBID);
                objParams[10] = new SqlParameter("@P_COLLEGE_ID", College_id);
                objParams[11] = new SqlParameter("@P_FAC_ADVISOR", uano);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_TIMETABLE_SUBJECTWISE_PERCENTAGE_BY_COURSES_PE_PA_EXCEL_MM", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AcdAttendanceController.GetSubjectWiseDetailsExcelReport-> " + ex.ToString());
            }
            return ds;
        }


        public DataSet GetSemesterDurationwise(int sessionno, int branchno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_BRANCHNO", branchno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
              


                ds = objSQLHelper.ExecuteDataSetSP("ACD_SEMESTER_DURATIONWISE_MM", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TeachingPlanController.GetSingleTeachingPlanEntry -> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetCollegeSchemeMappingDetails(int ColSchemeno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_COLSCHEMENO", ColSchemeno);
              
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_COLLEGE_SCHEME_MAPPING_DETAILS_MM", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.GetCollegeSchemeMappingDetails-> " + ex.ToString());
            }
            return ds;
        }

    }
}
