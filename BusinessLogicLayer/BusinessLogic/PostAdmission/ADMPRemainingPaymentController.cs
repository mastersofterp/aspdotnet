using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessLogic.PostAdmission
{
   public class ADMPRemainingPaymentController
    
    {
         string _UAIMS_constr = string.Empty;
         public ADMPRemainingPaymentController()
                {
                    _UAIMS_constr=System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                }


        public DataSet GetBranch(int Degree, int UGPGOT)
       {
           DataSet ds = null;
           SQLHelper objDataAccessLayer = new SQLHelper(_UAIMS_constr);

           try
           {
               SqlParameter[] objParams = new SqlParameter[]
                        {
                           new SqlParameter("@Degree",Degree),
                            new SqlParameter("@UPPGOT",UGPGOT)
                        };

               ds = objDataAccessLayer.ExecuteDataSetSP("[dbo].[PKG_ADMP_GET_GETDEGREENAME_WITH_PAYMENT]", objParams);
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

           return ds;
       }

       public DataSet GetStudentList(int ADMBATCH, int ProgramType, int DegreeNo, string branchno)
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

               ds = objDataAccessLayer.ExecuteDataSetSP("[dbo].[PKG_GET_ADMP_REMAININGPAYMENT_STUDENT_LIST]", objParams);
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

           return ds;
       }


       public int InsertRemaingPayment(int admbatch,  int DegreeNo, int BranchNo, int userNo,  decimal Demand, decimal OutStanding, string ipaddress, int ua_no, int ApplicationId, int CreatedBy)
       {
           //DataSet ds = null;
           int retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.Others);
           try
           {
               SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
               SqlParameter[] objParams = null;
               objParams = new SqlParameter[10];
               {
                   objParams[0] = new SqlParameter("@P_USERNO", userNo);
                   objParams[1] = new SqlParameter("@P_ADMBATCH", admbatch);
                   objParams[2] = new SqlParameter("@P_DEGREENO", DegreeNo);
                   objParams[3] = new SqlParameter("@P_BRANCHNO", BranchNo);
                   objParams[4] = new SqlParameter("@P_DEMAND", Demand);
                   objParams[5] = new SqlParameter("@P_OUTSTANDINGAMOUNT", OutStanding);
                   objParams[6] = new SqlParameter("@P_APPICATIONID", ApplicationId);
                   objParams[7] = new SqlParameter("@P_IPADDRESS", ipaddress);
                   objParams[8] = new SqlParameter("@P_CREATEDBY", CreatedBy);
                   objParams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                   objParams[9].Direction = ParameterDirection.Output;
               };
               object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMP_INSERT_REMAINGPAYMENT", objParams, false);

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
