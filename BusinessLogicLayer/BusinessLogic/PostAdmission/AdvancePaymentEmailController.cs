using IITMS.SQLServer.SQLDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
/*                                                  
---------------------------------------------------------------------------------------------------------------------------                                                          
Created By : Bhagyashree                                                      
Created On : 15-03-2024                             
Purpose    : Create controller for Advance Payment Email                                       
Version    : 1.0.0                                                
---------------------------------------------------------------------------------------------------------------------------                                                            
Version   Modified On   Modified By        Purpose                                                            
---------------------------------------------------------------------------------------------------------------------------                                                            
                                      
------------------------------------------- -------------------------------------------------------------------------------                             
*/ 
namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class AdvancePaymentEmailController
    {
        string uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet GetStudentDetails(int BatchNo,int UgPgOt,int DegreeNo, string BranchNos)
        {

            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_BATCHNO", BatchNo);
                objParams[1] = new SqlParameter("@P_UGPGOT", UgPgOt);
                objParams[2] = new SqlParameter("@P_DEGREENO", DegreeNo);
                objParams[3] = new SqlParameter("@P_BRANCHNOS", BranchNos);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ADMP_GET_ADVANCE_PAYMENT_EMAIL_STUDENT_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AdvancePaymentEmailController.GetUsersByUserType-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetStudentDetailsToSendEmail(string UsernoXml)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_USERNO", UsernoXml);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ADMP_GET_SEND_EMAIL_STUDENT_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AdvancePaymentEmailController.GetStudentDetailsToSendEmail-> " + ex.ToString());
            }
            return ds;
        }
    }
}
