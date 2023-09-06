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
    public class ADMPExamHallTicketController
    {
         string _UAIMS_constr = string.Empty;
         public ADMPExamHallTicketController()
                {
                    _UAIMS_constr=System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
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

         public DataSet GetSChedule(int Degree, string Branch,int ADMBATCH)
         {
             DataSet ds = null;
             SQLHelper objDataAccessLayer = new SQLHelper(_UAIMS_constr);

             try
             {
                 SqlParameter[] objParams = new SqlParameter[]
                        {
                           new SqlParameter("@DEGREENO",Degree),
                           new SqlParameter("@BRACNCHNO",Branch),
                           new SqlParameter("@P_ADMBATCH",ADMBATCH)
                        };

                 ds = objDataAccessLayer.ExecuteDataSetSP("[dbo].[PKG_ACAD_SP_GETSCHEDULE]", objParams);
             }
             catch (Exception ex)
             {
                 throw new Exception(ex.Message);
             }

             return ds;
         }

         public DataSet GetAdmitCardStudent(int ADMBATCH, int ProgramType, int DegreeNo, string branchno)
         {
             DataSet ds = null;
             SQLHelper objDataAccessLayer = new SQLHelper(_UAIMS_constr);

             try
             {
                 SqlParameter[] objParams = new SqlParameter[]
                        {
                           new SqlParameter("@P_ADMBATCH",ADMBATCH),
                           new SqlParameter("@P_PROGRAMME_TYPE",ProgramType),
                           new SqlParameter("@P_DEGREENO",DegreeNo),
                           new SqlParameter("@P_BRANCHNO",branchno)
                        };

                 ds = objDataAccessLayer.ExecuteDataSetSP("[dbo].[PKG_GET_STUDENT_LIST_FOR_ADMITCARD]", objParams);
             }
             catch (Exception ex)
             {
                 throw new Exception(ex.Message);
             }

             return ds;
         }

         public int UpdateStatusFor_AdmitCard(int admbatch, int userNo, string ip_address, int uano, string ExamSchedule, int ScheduleNO,bool IsReschedule) //int rollNo
         {
             //DataSet ds = null;
             int retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.Others);
             try
             {
                 SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                 SqlParameter[] objParams = null;
                 objParams = new SqlParameter[8];
                 {
                     objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                     objParams[1] = new SqlParameter("@P_USERNO", userNo);
                     objParams[2] = new SqlParameter("@P_IPADDRESS", ip_address);
                     objParams[3] = new SqlParameter("@P_UA_NO", uano);
                     objParams[4] = new SqlParameter("@P_ExamSchedule", ExamSchedule);
                     objParams[5] = new SqlParameter("@P_SCHEDULE_NO", ScheduleNO);
                     objParams[6] = new SqlParameter("@P_IsReshedule", IsReschedule);
                     objParams[7] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                     objParams[7].Direction = ParameterDirection.Output;
                 };
                 object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_ADMITCARD_ENTRY_GENERATION", objParams, false);

                 if (Convert.ToInt32(ret) == -99)
                     retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.TransactionFailed);
                 else
                     retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.RecordSaved);
             }
             catch (Exception ex)
             {
                 throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamHallTicketController.UpdateStatusFor_AdmitCard()-->" + ex.Message + " " + ex.StackTrace);
             }
             return retStatus;
         }
    }
}
