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
   public class ExamScheduleController
    {
       string _UAIMS_constr = string.Empty;
       public ExamScheduleController()
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

       public int Add_Update_EntranceSchedule(int ScheduleNo, int admBatch, DateTime date, string time, int DegreeId, string DegreeName, string Branch, int activeStatus, string ipAddress, int createdBy, string Place, string CenterAddress, int Mode, int ORGID)
       {
           int status = 0;
           try
           {
               SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
               SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                             new SqlParameter("@ScheduleNo",ScheduleNo),
                             new SqlParameter("@ADMBATCH",admBatch),                            
                             new SqlParameter("@SCHD_DATE",date),
                             new SqlParameter("@SCHD_TIME",time),
                             new SqlParameter("@DEGREEID",DegreeId),
                             new SqlParameter("@DEGREENAME",DegreeName),
                             new SqlParameter("@BRANCHNAME",Branch),   
                             new SqlParameter("@Place",Place),   
                             new SqlParameter("@Address",CenterAddress),   
                             new SqlParameter("@MODE",Mode),  
                             new SqlParameter("@ORGID",ORGID),  
                             new SqlParameter("@P_CREATED_BY",createdBy),
                             new SqlParameter("@P_IPADDRESS",ipAddress),
                             //new SqlParameter("@P_FLAG",flag),
                             new SqlParameter("@ACTIVE_STATUS",activeStatus),             
                             new SqlParameter("@P_OUTPUT",SqlDbType.Int)
                    };
               sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
               object obj = objSQLHelper.ExecuteNonQuerySP("[dbo].[PKG_SP_INSERT_ENTRANCE_SCHEDULE]", sqlParams, true);
               //object obj = 1;
               if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001" && obj.ToString().Equals("1"))
                   status = Convert.ToInt32(CustomStatus.RecordSaved);
               else if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001" && obj.ToString().Equals("2"))
                   status = Convert.ToInt32(CustomStatus.RecordUpdated);
               else if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001" && obj.ToString().Equals("2627"))
                   status = Convert.ToInt32(CustomStatus.RecordExist);
               else
                   status = Convert.ToInt32(CustomStatus.Error);
           }
           catch (Exception ex)
           {
               status = Convert.ToInt32(CustomStatus.Error);
               throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.EntranceTestDetail.Add_Update_EntranceSchedule-> " + ex.ToString());
           }
           return status;
       }

       public DataSet BindExamSchedule(int ORGID)
       {
           DataSet ds = null;
           SQLHelper objDataAccessLayer = new SQLHelper(_UAIMS_constr);

           try
           {
               SqlParameter[] sqlParams = new SqlParameter[] 
                        {
                             new SqlParameter("@ORGID",ORGID),                         
                    };
               ds = objDataAccessLayer.ExecuteDataSetSP("[dbo].[PKG_ACAD_GET_GETEXAMSCHEDULEINFO]", sqlParams);
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

           return ds;
       }
    }
}
