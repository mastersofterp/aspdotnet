using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessLogic.Academic.Admission
{
    public class ADMPInterviewTestConfigController
    {
        private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int INSUPDInterviewTestData(int PanelForId, int BatchNo, int DegreeNo, int BranchNo, int MaxMarks, int OprCode)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_PANELFORID",PanelForId),
                            new SqlParameter("@P_BATCHNO",BatchNo),
                            new SqlParameter("@P_DEGREENO",DegreeNo),
                            new SqlParameter("@P_BRANCHNO",BranchNo),
                            new SqlParameter("@P_MAXMARKS", MaxMarks),
                            new SqlParameter("@P_OPR_CODE", OprCode),
                            new SqlParameter("@P_OUT", SqlDbType.Int),
                        };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ADMP_INS_UPD_INTERVIEW_TEST_CONFIG", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.INSUPDInterviewTestData-> " + ex.ToString());
            }
            return retStatus;
        }
        public DataSet GetAllInterviewTestList(int BatchNo, int DegreeNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@P_BATCHNO", BatchNo);
                sqlParams[1] = new SqlParameter("@P_DEGREENO", DegreeNo);


                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ADMP_ALL_INTERVIEW_TEST_CONFIG", sqlParams);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GetAllInterviewTestList-> " + ex.ToString());
            }
            return ds;
        }
    }
}
