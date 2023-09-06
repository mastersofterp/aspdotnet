using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessLogic.PostAdmission
{
    public class IndexScoreConfigController
    {
        private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int InsertSubjectwiseMarks(int AdmBatchno, int DegreeNo, int Branchno, string ConfigurationType, string xml, string AdvantageType, float MaxMarks)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                //Add
                objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_ADMBATCHNO", AdmBatchno);
                objParams[1] = new SqlParameter("@P_DEGREENO", DegreeNo);
                objParams[2] = new SqlParameter("@P_BRANCHNO", Branchno);
                objParams[3] = new SqlParameter("@P_CONFIGURATION_TYPE", ConfigurationType);
                objParams[4] = new SqlParameter("@P_XMLDATA", xml);
                objParams[5] = new SqlParameter("@P_MAX_MARKS", MaxMarks);
                objParams[6] = new SqlParameter("@P_ADVANTAGE_TYPE", AdvantageType);
                objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_INDEX_SCORE_CONFIG", objParams, true);
                return Convert.ToInt32(ret);
                
            }
            catch (Exception ex)
            {
                return retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("BusinessLayer.BusinessLogic.IndexScoreConfigController.InsertSubjectwiseMarks-> " + ex.ToString());
            }
            //return retStatus;
        }
        public int UpdateSubjectwiseMarksSubjects(int AdmBatchno, int DegreeNo, int Branchno, string ConfigurationType, string xml, string AdvantageType, float MaxMarks)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                //Add
                objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_ADMBATCHNO", AdmBatchno);
                objParams[1] = new SqlParameter("@P_DEGREENO", DegreeNo);
                objParams[2] = new SqlParameter("@P_BRANCHNO", Branchno);
                objParams[3] = new SqlParameter("@P_CONFIGURATION_TYPE", ConfigurationType);
                objParams[4] = new SqlParameter("@P_XMLDATA", xml);
                objParams[5] = new SqlParameter("@P_MAX_MARKS", MaxMarks);
                objParams[6] = new SqlParameter("@P_ADVANTAGE_TYPE", AdvantageType);
                objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPD_INDEX_SCORE_CONFIG", objParams, true);

                return Convert.ToInt32(ret);
                //if (ret != null && ret.ToString() != "-99" && ret.ToString() != "-1001")
                //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //else
                //    retStatus = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                return retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("BusinessLayer.BusinessLogic.IndexScoreConfigController.UpdateSubjectwiseMarksSubjects-> " + ex.ToString());
            }
            //return retStatus;
        }
        public DataSet GetAllSubjectwiseMarksList(string search, string category)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                //SqlParameter[] objParams = null;
                //ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GETALL_EXAMBOARD", objParams);

                ds = objSQLHelper.ExecuteDataSet("PKG_ACD_GETALL_INDEX_SCORE_CONFIG");

            }
            catch (Exception ex)
            {
                throw new IITMSException("BusinessLayer.BusinessLogic.IndexScoreConfigController.GetAllSubjectwiseMarksList-> " + ex.ToString());
            }
            return ds;
        }
        public DataSet GetSingleSubjectwiseMarksInformation(int AdmBatchno, int DegreeNo, int Branchno, string ConfigurationType, string AdvantageType)
        {
            
            DataSet ds = null;
            try
            {
               
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[5];
                //objParams[0] = new SqlParameter("@P_ORGANIZATIONID", OrganizationID);
                objParams[0] = new SqlParameter("@P_ADMBATCHNO", AdmBatchno);
                objParams[1] = new SqlParameter("@P_DEGREENO", DegreeNo);
                objParams[2] = new SqlParameter("@P_BRANCHNO", Branchno);
                objParams[3] = new SqlParameter("@P_CONFIGURATION_TYPE", ConfigurationType);
                objParams[4] = new SqlParameter("@P_ADVANTAGE_TYPE", AdvantageType);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_SUBJECTMARKS_INDEX_SCORE_CONFIG", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("BusinessLayer.BusinessLogic.IndexScoreConfigController.GetSingleSubjectwiseMaxMarksInformation->" + ex.ToString());
            }
            return ds;
        }
        
    }
}
