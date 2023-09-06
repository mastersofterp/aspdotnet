using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessLogic.Academic
{
    public class ClubStudentMappingController
    {
        private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public string InsertClubStudentMapping(int IdNo, int ClubActivityNo)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;


                objParams = new SqlParameter[3];

                objParams[0] = new SqlParameter("@P_IDNO", IdNo);
                objParams[1] = new SqlParameter("@P_CLUB_ACTIVITY_NO", ClubActivityNo);


                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_CLUB_STUDENT_MAPPING", objParams, true);
                return ret.ToString();

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("BusinessLogicLayer.BusinessLogic.Academic.ClubStudentMappingController.InsertClubStudentMapping-> " + ex.ToString());
            }
        }

        public int DeleteClubStudentMapping(int IdNo, int ClubActivityNO)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_IDNO", IdNo),
                            new SqlParameter("@P_CLUB_ACTIVITY_NO",ClubActivityNO),
                            new SqlParameter("@P_OUT",SqlDbType.Int),
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_DELETE_CLUB_STUDENT_MAPPING", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)

                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.InsertCreateEventData-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetAllClubStudentMappingData(int CollegeId, string DegreeNo, string BranchNo, int ClubActivityNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@P_COLLEGE_ID", CollegeId);
                sqlParams[1] = new SqlParameter("@P_DEGREENO", DegreeNo);
                sqlParams[2] = new SqlParameter("@P_BRANCHNO", BranchNo);
                sqlParams[3] = new SqlParameter("@P_CLUB_ACTIVITY_NO", ClubActivityNo);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ALL_CLUB_STUDENT_MAPPING", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GetAllClubStudentMappingData-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet ClubStudentMappingReport()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ALL_CLUB_STUDENT_MAPPING_EXCEL_REPORT", sqlParams);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ClubStudentMappingReport-> " + ex.ToString());
            }
            return ds;
        }
    }
}
