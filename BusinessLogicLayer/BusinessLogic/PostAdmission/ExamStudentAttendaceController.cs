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
   public class ExamStudentAttendaceController
    {

  
       string _UAIMS_constr = string.Empty;
       public ExamStudentAttendaceController()
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

       public DataSet GetSChedule(int Degree, string Branch, int ADMBATCH)
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

       public DataSet GetAdmitCardStudent(int ADMBATCH, int ProgramType, int DegreeNo, string branchno,string ExamSchedule)
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
                           new SqlParameter("@P_BRANCHNO",branchno),
                           new SqlParameter("@P_Schedule",ExamSchedule)
                        };

               ds = objDataAccessLayer.ExecuteDataSetSP("[dbo].[PKG_GET_STUDENT_LIST_FOR_STUDENTATTENDANCE]", objParams);
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

           return ds;
       }

       public int INSERT_Update_STUDENTATTENDANCE(int admbatch, int ACNO, string ip_address, int userNo, bool IsAttend, int AttendanceNo) //int rollNo
       {
           //DataSet ds = null;
           int retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.Others);
           try
           {
               SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
               SqlParameter[] objParams = null;
               objParams = new SqlParameter[7];
               {
                   objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                   objParams[1] = new SqlParameter("@P_ACNO", ACNO);                  
                   objParams[2] = new SqlParameter("@P_IPADDRESS", ip_address);
                   objParams[3] = new SqlParameter("@P_USERNO", userNo);
                   objParams[4] = new SqlParameter("@P_IsAttend", IsAttend);
                   objParams[5] = new SqlParameter("@P_AttendanceNO", AttendanceNo);  
                   objParams[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                   objParams[6].Direction = ParameterDirection.Output;
               };
               object ret = objSQLHelper.ExecuteNonQuerySP("[dbo].[PKG_INSERT_UPDATE_EXAM_STUDENT_ATTENDANCE]", objParams, false);

               if (Convert.ToInt32(ret) == -99)
                   retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.TransactionFailed);
               else
                   retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.RecordSaved);
           }
           catch (Exception ex)
           {
               throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamStudentAttendaceController.INSERT_Update_STUDENTATTENDANCE()-->" + ex.Message + " " + ex.StackTrace);
           }
           return retStatus;
       }
    }
}
