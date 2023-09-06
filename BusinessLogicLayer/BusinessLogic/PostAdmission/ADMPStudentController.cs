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
   public class ADMPStudentController
    {
       string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

       public DataSet RetrieveStudentDetailsAdmCancel(string search, string category)
       {
           DataSet ds = null;
           try
           {
               SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
               SqlParameter[] objParams = null;
               objParams = new SqlParameter[2];
               objParams[0] = new SqlParameter("@P_SEARCHSTRING", search);
               objParams[1] = new SqlParameter("@P_SEARCH", category);

               ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMP_STUDENT_SP_SEARCH_STUDENT_NEW_DESIGN_ADMCANCEL", objParams);
           }
           catch (Exception ex)
           {
               throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ADMPStudentController.RetrieveStudentDetailsAdmCancel-> " + ex.ToString());
           }
           return ds;
       }

       public DataTableReader GetStudentDetailsForBranchChnage(int userno)
       {
           DataTableReader dtr = null;
           try
           {
               SQLHelper objSH = new SQLHelper(_UAIMS_constr);
               SqlParameter[] objParams = null;
               objParams = new SqlParameter[1];
               objParams[0] = new SqlParameter("@P_USERNO", userno);
               dtr = objSH.ExecuteDataSetSP("PKG_ADMP_SP_GET_STUDDETAILS_FOR_BRANCHCHANGE", objParams).Tables[0].CreateDataReader();
           }
           catch (Exception ex)
           {
               throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ADMPStudentController.GetStudentDetails-> " + ex.ToString());
           }
           return dtr;
       }

       public int ChangeBranch(int UserNo, int AdmBatch, int DegreeNo, int BranchNo, int NewBranchNo, string StudName, int CreatedBy)
       {
           int retStatus = Convert.ToInt32(CustomStatus.Others);
           try
           {
               SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
               SqlParameter[] objParams = null;

               //Add Branch change data
               objParams = new SqlParameter[8];
               objParams[0] = new SqlParameter("@P_USERNO", UserNo);
               objParams[1] = new SqlParameter("@P_ADMBATCH", AdmBatch);
               objParams[2] = new SqlParameter("@P_DEGREENO", DegreeNo);
               objParams[3] = new SqlParameter("@P_BRANCHNO", BranchNo);
               objParams[4] = new SqlParameter("@P_NEW_BRANCHNO", NewBranchNo);
               objParams[5] = new SqlParameter("@P_STUDNAME", StudName);
               objParams[6] = new SqlParameter("@P_CreatedBy", CreatedBy);
               objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
               objParams[7].Direction = ParameterDirection.Output;

               object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMP_POSTADMISSION_BRANCH_CHANGE", objParams, true);
               if (Convert.ToInt32(ret) == -99)
                   retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
               else if (Convert.ToInt32(ret) == 2627)
                   retStatus = Convert.ToInt32(CustomStatus.RecordExist);
               else
                   retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
           }
           catch (Exception ex)
           {
               retStatus = Convert.ToInt32(CustomStatus.Error);
               throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ADMPStudentController.ChangeBranch-> " + ex.ToString());
           }

           return retStatus;
       }

       public string CancelDemandForSelectedStudent_ByBranchChange(int UserNo, int DegreeNo, int BranchNo, int AdmBatch, int PaymentType, string StudeName, int CreatedBy)
       {
           string strOutput = "0";
           try
           {
               SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
               SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_USERNO", UserNo),
                    new SqlParameter("@P_DEGREENO", DegreeNo),
                    new SqlParameter("@P_BRANCHNO", BranchNo),
                    new SqlParameter("@P_ADMBATCHNO", AdmBatch),    
                    new SqlParameter("@P_PAYMENTTYPE", PaymentType),
                    new SqlParameter("@P_STUDNAME", StudeName),  
                    new SqlParameter("@P_CreatedBy", CreatedBy),  
                    new SqlParameter("@P_OUTPUT", strOutput)
                };
               sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
               sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
               object output = objDataAccess.ExecuteNonQuerySP("PKG_ADMP_CANCEL_DEMAND_BY_BRANCHCHANGE_WITHOUTFEES", sqlParams, true);

               if (output != null && output.ToString() == "-99")
                   return "-99";
               else if (output.ToString() == "2")
                   return "2";
               else
                   strOutput = output.ToString();
           }
           catch (Exception ex)
           {
               throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ADMPStudentController.CancelDemandForSelectedStudent_ByBranchChange() --> " + ex.Message + " " + ex.StackTrace);
           }
           return strOutput;
       }
    }
}
