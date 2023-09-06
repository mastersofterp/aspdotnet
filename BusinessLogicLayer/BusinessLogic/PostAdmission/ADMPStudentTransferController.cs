using IITMS;
using IITMS.SQLServer.SQLDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessLogic.PostAdmission
{
    public class ADMPStudentTransferController
    {
        string _UAIMS_constr = string.Empty;
        public ADMPStudentTransferController()
        {
            _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        }


        public DataSet GetBranch(int Degree)
        {
            DataSet ds = null;
            SQLHelper objDataAccessLayer = new SQLHelper(_UAIMS_constr);

            try
            {
                SqlParameter[] objParams = new SqlParameter[]
                        {
                           new SqlParameter("@Degree",Degree),
                        };

                ds = objDataAccessLayer.ExecuteDataSetSP("[dbo].[PKG_ACAD_GET_GETDEGREENAME]", objParams);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return ds;
        }


        public DataSet GetStudentList(int ADMBATCH,int UGPG,int Degree, string Branch)
        {
            DataSet ds = null;
            SQLHelper objDataAccessLayer = new SQLHelper(_UAIMS_constr);

            try
            {
                SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_ADMBATCH",ADMBATCH),
                           new SqlParameter("@P_PROGRAMME_TYPE",UGPG),
                           new SqlParameter("@P_DEGREENO",Degree),
                           new SqlParameter("@P_BRANCHNO",Branch)
                        };

                ds = objDataAccessLayer.ExecuteDataSetSP("[dbo].[PKG_GET_LIST_FOR_STUDENT_TRANSFER]", objParams);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return ds;
        }

        public int INSERT_StudentTransfer(int UserNo, int PType, int DegreeNo, int BranchNo)
        {
            int retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[7];
                {
                    objParams[0] = new SqlParameter("@P_USERNO", UserNo);
                    objParams[1] = new SqlParameter("@P_PTYPE", PType);
                    objParams[2] = new SqlParameter("@P_DEGREENO", DegreeNo);
                    objParams[3] = new SqlParameter("@P_BRANCHNO", BranchNo);
                    objParams[4] = new SqlParameter("@P_REMARK", "Post Admisssion Transfere");
                    objParams[5] = new SqlParameter("@P_ADMDATE", DateTime.Now);
                    objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                    objParams[6].Direction = ParameterDirection.Output;
                };
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMP_TRANSFER_STUD_DATA", objParams, false);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ADMPStudentTransferController.INSERT_StudentTransfer()-->" + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        
        }

    }
}
