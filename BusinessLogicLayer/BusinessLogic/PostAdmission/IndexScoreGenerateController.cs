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
    public class IndexScoreGenerateController
    {
        private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;     

        /*

        CREATE PROC PKG_INDEX_NUMBER_GENERATION_RAJAGIRI  
        (  
         @P_DEGREENO INT,  
         @P_BRANCHNO INT,  
         @P_ADMBATCHNO INT,  
         @P_OUT INT OUTPUT  
        ) 
         
        */
        public int GenerateIndexScore(int DegreeNo, int Branchno, int AdmBatchno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[]{
                    new SqlParameter("@P_DEGREENO", DegreeNo),
                    new SqlParameter("@P_BRANCHNO", Branchno),
                    new SqlParameter("@P_ADMBATCHNO", AdmBatchno),
                     new SqlParameter("@P_OUT", SqlDbType.Int)  
                };
                objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INDEX_NUMBER_GENERATION_RAJAGIRI", objParams, true);
                return Convert.ToInt32(ret);

            }
            catch (Exception ex)
            {
                return retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("BusinessLayer.BusinessLogic.IndexScoreGenerateController.GenerateIndexScore-> " + ex.ToString());
            }
            //return retStatus;
        }

        public DataSet GetAllIndexScoreApplicantList(string search, string category)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                //SqlParameter[] objParams = null;
                //ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GETALL_EXAMBOARD", objParams);

                ds = objSQLHelper.ExecuteDataSet("PKG_ACD_ADMP_GETALL_GENERATE_INDEX_SCORE");

            }
            catch (Exception ex)
            {
                throw new IITMSException("BusinessLayer.BusinessLogic.IndexScoreConfigController.GetAllSubjectwiseMarksList-> " + ex.ToString());
            }
            return ds;
        }


        public DataSet GetAllIndexScoreMarksList(string DegreeNo, string BranchNo, string AdmBatchNo, string UserNO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                //SqlParameter[] objParams = null;
                //ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GETALL_EXAMBOARD", objParams);

                /*

                    @P_DEGREENO INT,  
                    @P_BRANCHNO INT,  
                    @P_ADMBATCHNO INT,  
                    @P_USERNO INT,  
                    @P_OUT  
                */
                SqlParameter[] param = new SqlParameter[]
                        {                                                     
                            new SqlParameter("@P_DEGREENO", DegreeNo),  
                            new SqlParameter("@P_BRANCHNO", BranchNo),
                            new SqlParameter("@P_ADMBATCHNO", AdmBatchNo),
                            new SqlParameter("@P_USERNO", UserNO),
                            new SqlParameter("@P_OUT", SqlDbType.Int)  
                        };
                param[param.Length - 1].Direction = ParameterDirection.Output;


                ds = objSQLHelper.ExecuteDataSetSP("PKG_INDEX_REPORT_RAJAGIRI", param);

            }
            catch (Exception ex)
            {
                throw new IITMSException("BusinessLayer.BusinessLogic.IndexScoreConfigController.GetAllIndexScoreMarksList-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetAllIndexScoreQualificationMarksList(string UserNO, string QualifyNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                //SqlParameter[] objParams = null;
                //ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GETALL_EXAMBOARD", objParams);

                /*

                   @P_USERNO      INT,  
                    @P_QUALIFYNO   INT 
                */
                SqlParameter[] param = new SqlParameter[]
                        {                         
                            new SqlParameter("@P_USERNO", UserNO),
                            new SqlParameter("@P_QUALIFYNO", QualifyNo)      
                        };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_GENERATE_INDEX_SCORE_MARKSHEETLIST", param);

            }
            catch (Exception ex)
            {
                throw new IITMSException("BusinessLayer.BusinessLogic.IndexScoreConfigController.GetAllIndexScoreMarksList-> " + ex.ToString());
            }
            return ds;
        }
    }
}
