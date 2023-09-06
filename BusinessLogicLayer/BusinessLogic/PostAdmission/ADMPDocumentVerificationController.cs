using BusinessLogicLayer.BusinessEntities.Academic.PoastAdmission;
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
   public class ADMPDocumentVerificationController
    {
         string _UAIMS_constr = string.Empty;
         public ADMPDocumentVerificationController()
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

       public DataSet StudentListFocVerification(DocumentVerification EntDocVerification)
       {
           DataSet ds = null;
           SQLHelper objDataAccessLayer = new SQLHelper(_UAIMS_constr);

           try
           {
               SqlParameter[] objParams = new SqlParameter[]
                        {
                           new SqlParameter("@P_ADMBATCH",EntDocVerification.ADMBATCH),
                           new SqlParameter("@P_PROGRAMME_TYPE",EntDocVerification.ProgramType),
                           new SqlParameter("@P_DEGREENO",EntDocVerification.DegreeNo),
                           new SqlParameter("@P_BRANCHNO",EntDocVerification.Branchno)                         
                        };

               ds = objDataAccessLayer.ExecuteDataSetSP("[dbo].[PKG_GET_STUDENT_LIST_FOR_DOCUMENTVERIFICATION]", objParams);
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

           return ds;
       }

       public int INSERT_UPDATE_DOCUMENTVERIFICATION(DocumentVerification EntDocVerification)
       {

           int retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.Others);
           try
           {
               SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
               SqlParameter[] objParams = null;
               objParams = new SqlParameter[8];
               {
                   objParams[0] = new SqlParameter("@P_USERNO", EntDocVerification.USerNo);
                   objParams[1] = new SqlParameter("@P_ADMBATCH", EntDocVerification.ADMBATCH);
                   objParams[2] = new SqlParameter("@P_DEGREENO", EntDocVerification.DegreeNo);
                   objParams[3] = new SqlParameter("@P_BRANCHNO", EntDocVerification.Branchno);
                   objParams[4] = new SqlParameter("@P_DOCSTATUS", EntDocVerification.DocStatus);
                   objParams[5] = new SqlParameter("@P_ipAddress", EntDocVerification.IpAdreess);
                   objParams[6] = new SqlParameter("@P_CreatedBy", EntDocVerification.CreatedBy);
                   objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                   objParams[7].Direction = ParameterDirection.Output;
               };
               object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERTUPDATE_ADMP_DOCUMNET_VERIFICATION", objParams, false);

               if (Convert.ToInt32(ret) == -99)
                   retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.TransactionFailed);
               else
                   retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.RecordSaved);
           }
           catch (Exception ex)
           {
               throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.ADMPDocumentVerificationController.PKG_ACD_INSERTUPDATE_ADMP_DOCUMNETVERIFICATION-> " + ex.ToString());
           }
           return retStatus;
       }
    }
}
