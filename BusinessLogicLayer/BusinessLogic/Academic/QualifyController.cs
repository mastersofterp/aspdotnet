//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : QUALIFY MASTER CONTROLLER                                              
// CREATION DATE : 21-MARCH-2012                                                       
// CREATED BY    : ASHISH DHAKATE                                                 
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
   public class QualifyController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddQualifyExam(StudentQualExm objQualExm, int Qualilevelno, int programmeType)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_QUALIEXMNAME",objQualExm.QualexmName),
                    new SqlParameter("@P_QEXAMID", objQualExm.QualexmId),
                    new SqlParameter("@P_QEXAMSTATUS",objQualExm.QexmStatus),
                    new SqlParameter("@P_DEGREENO",objQualExm.DegreeNo),
                    new SqlParameter("@P_QUALILEVELNO",Qualilevelno),
                    new SqlParameter("@P_COLLEGE_CODE", objQualExm.CollegeCode),
                    new SqlParameter("@P_PROGRAMME_TYPE", programmeType),           //Added by Nikhil L. on 16-06-2022 to add programme type for Entrance and Qualify.
                    new SqlParameter("@P_QUALIFYNO", status)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_QUALIFYEXAM_INSERT", sqlParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.QualifyController.AddQualifyExam() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public int UpdateQualifyExam(StudentQualExm objQualExm, int Qualilevelno, int programmeType)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {                    
                    new SqlParameter("@P_QUALIEXMNAME",objQualExm.QualexmName),
                    new SqlParameter("@P_QEXAMID", objQualExm.QualexmId),
                    new SqlParameter("@P_QEXAMSTATUS",objQualExm.QexmStatus),
                    new SqlParameter("@P_DEGREENO",objQualExm.DegreeNo),
                    new SqlParameter("@P_QUALILEVELNO",Qualilevelno), 
                    new SqlParameter("@P_COLLEGE_CODE", objQualExm.CollegeCode),
                    new SqlParameter("@P_PROGRAMME_TYPE", programmeType),
                    new SqlParameter("@P_QUALIFYNO", objQualExm.QUALIFYNO)
                   
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_QUALIFYEXAM_UPDATE", sqlParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.QualifyController.UpdateQualifyExam() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetAllQualifyExam()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_QUALIFYEXAM_GET_ALL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetAllQualifyExam() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public SqlDataReader GetQualifyExamNo(int QualifyNo)
        {
            SqlDataReader dr = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_QUALIFYNO", QualifyNo) };

                dr = objSQLHelper.ExecuteReaderSP("PKG_ACAD_QUALIFYEXAM_GET_BY_NO", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetQualifyExamNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return dr;
        }

        public DataSet GetAllEvents()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_EVENT_GET_ALL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMS.IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.GetAllQualifyExam() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

    }
}
