using BusinessLogicLayer.BusinessEntities.Academic;
using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessLogic
{
    public class ScoreMappingController
    {
        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddDegreeScoreMapping(EntranceScoreMapping ScrMappng, DataTable MappingData)
        {
            int status = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_tvp_MappingData", MappingData),
                    //new SqlParameter("@P_EXAMNO", ScrMappng.ExamNo),
                    //new SqlParameter("@P_CATEGORY", ScrMappng.CategoryNo),
                    //new SqlParameter("@P_SCORE", ScrMappng.Score),   
                    //new SqlParameter("@P_ENT_NO", SqlDbType.Int),
                    //new SqlParameter("@P_PREREQ_ACT_NO", ScrMappng.Prereq_Act_No),
                    new SqlParameter("@P_ENT_NO", SqlDbType.Int),
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                status = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_Academic_INS_EntranceDegreeScoreMapping", sqlParams, true);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.ScoreMappingController.AddDegreeScoreMapping() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public int UpdateDegreeScoreMapping(EntranceScoreMapping ScrMappng)
        {
            int status = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_DegreeNo", ScrMappng.DegreeNo),
                    new SqlParameter("@P_EXAMNO", ScrMappng.ExamNo),
                    new SqlParameter("@P_CATEGORY", ScrMappng.CategoryNo),
                    new SqlParameter("@P_SCORE", ScrMappng.Score),   
                    new SqlParameter("@P_ENT_NO", ScrMappng.Ent_NO),
                    new SqlParameter("@P_ENT_NO_Ret", SqlDbType.Int),                    
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                status = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_Academic_UPD_EntranceDegreeScoreMapping", sqlParams, true);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.ScoreMappingController.UpdateDegreeScoreMapping() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetAllMappedData()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_Degree_Mapped_Score_GET_ALL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ScoreMappingController.GetAllMappedData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public SqlDataReader GetSingleDegreeMappedRecord(int ENT_NO)
        {
            SqlDataReader dr = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_ENT_NO", ENT_NO);
                dr = objSQLHelper.ExecuteReaderSP("PKG_ACD_SP_GET_Degree_Mapped", objParams);
            }
            catch (Exception ex)
            {
                return dr;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ScoreMappingController.GetSingleDegreeMappedRecord-> " + ex.ToString());
            }
            return dr;
        }
    }
}
