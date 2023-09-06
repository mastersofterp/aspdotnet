using BusinessLogicLayer.BusinessEntities.Academic.PoastAdmission;
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
    public class ADMPGenerateCallLetter
    {
         string _UAIMS_constr = string.Empty;
         public ADMPGenerateCallLetter()
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

       public DataSet GetStudentsForCallLetter(CallLetterGenration EntGenCll)
       {
           DataSet ds = null;
           SQLHelper objDataAccessLayer = new SQLHelper(_UAIMS_constr);

           try
           {
               SqlParameter[] objParams = new SqlParameter[]
                        {
                           new SqlParameter("@P_ADMBATCH",EntGenCll.ADMBATCH),
                           new SqlParameter("@P_PROGRAMME_TYPE",EntGenCll.ProgramType),
                           new SqlParameter("@P_DEGREENO",EntGenCll.DegreeNo),
                           new SqlParameter("@P_BRANCHNO",EntGenCll.Branchno)                         
                        };

               ds = objDataAccessLayer.ExecuteDataSetSP("[dbo].[PKG_GET_STUDENT_LIST_FOR_CALLLETTER_GENERATION]", objParams);
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

           return ds;
       }

       public int INSERT_UPDATE_CALLLETTER(CallLetterGenration CallLTR, string IpAddress, int CreatedBy)
       {

           int retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.Others);
           try
           {
               SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
               SqlParameter[] objParams = null;
               objParams = new SqlParameter[9];
               {
                   objParams[0] = new SqlParameter("@P_USERNO", CallLTR.USerNo);
                   objParams[1] = new SqlParameter("@P_ADMBATCH", CallLTR.ADMBATCH);
                   objParams[2] = new SqlParameter("@P_DEGREENO", CallLTR.DegreeNo);
                   objParams[3] = new SqlParameter("@P_BRANCHNO", CallLTR.Branchno);
                   objParams[4] = new SqlParameter("@P_CALLDATE", CallLTR.CallDate);
                   objParams[5] = new SqlParameter("@P_CALLTIME", CallLTR.Calltime);
                   objParams[6] = new SqlParameter("@P_ipAddress", IpAddress);
                   objParams[7] = new SqlParameter("@P_CreatedBy", CreatedBy);
                   objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                   objParams[8].Direction = ParameterDirection.Output;
               };
               object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERTUPDATE_CALLLETTER_GENERATION", objParams, false);

               if (Convert.ToInt32(ret) == -99)
                   retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.TransactionFailed);
               else
                   retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.RecordSaved);
           }
           catch (Exception ex)
           {
               throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.ADMPGenerateCallLetter.PKG_ACD_INSERTUPDATE_CALLLETTER_GENERATION-> " + ex.ToString());
           }
           return retStatus;

       }


    }
}
