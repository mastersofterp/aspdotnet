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
   public class ADMPProvisionalAdmissionAprovalController
    {
        string _UAIMS_constr = string.Empty;
        public ADMPProvisionalAdmissionAprovalController()
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

       public DataSet GetProvisionalStudent(int ADMBATCH, int ProgramType, int DegreeNo, string branchno,int USERNO)
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
                           new SqlParameter("@USERNO",USERNO)   
                        };

               ds = objDataAccessLayer.ExecuteDataSetSP("[dbo].[PKG_GET_STUDENT_LIST_FOR_PROVISIONALADM]", objParams);
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

           return ds;
       }


       public DataSet GetPayableAmount(int ADMBATCH, int DegreeNo,int branchno, int PAYTYPENO)
       {
           DataSet ds = null;
           SQLHelper objDataAccessLayer = new SQLHelper(_UAIMS_constr);

           try
           {
               SqlParameter[] objParams = new SqlParameter[]
                        {
                           new SqlParameter("@P_ADMBATCH",ADMBATCH),
                           new SqlParameter("@P_DEGREENO",DegreeNo),
                           new SqlParameter("@P_BRANCHNO",branchno),
                           new SqlParameter("@P_PAYTYPENO",PAYTYPENO)   
                        };

               ds = objDataAccessLayer.ExecuteDataSetSP("[dbo].[PKG_GET_PAYABLEAMOUNT]", objParams);
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

           return ds;
       }



       public int Create_ProAdmission_Demand(int USERNO, int ADMBATCH, string APPLICATIONID, int DEGREENO, int BRANCHNO, int PAYMENTTYPE, double AMOUNT, double STANDARDFEE, int CreatedBy, string ipAddress, string STUDNAME)
       {

           int retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.Others);
           try
           {
               SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
               SqlParameter[] objParams = null;
               objParams = new SqlParameter[14];
               {
                   objParams[0] = new SqlParameter("@P_USERNO", USERNO);
                   //objParams[1] = new SqlParameter("@P_IDNO", IDNO);  
                   objParams[1] = new SqlParameter("@P_ADMBATCH",ADMBATCH);                       
                   objParams[2] = new SqlParameter("@P_APPLICATIONID",APPLICATIONID);
                   objParams[3] = new SqlParameter("@P_DEGREENO",DEGREENO);
                   objParams[4] = new SqlParameter("@P_BRANCHNO",BRANCHNO);
                   objParams[5] = new SqlParameter("@P_PAYMENTTYPE",PAYMENTTYPE);
                   objParams[6] = new SqlParameter("@P_AMOUNT",AMOUNT);
                   objParams[7] = new SqlParameter("@P_STANDARDFEE", STANDARDFEE); 
                   objParams[8] = new SqlParameter("@P_ipAddress",ipAddress);
                   objParams[9] = new SqlParameter("@P_CreatedBy", CreatedBy);
                   objParams[10] = new SqlParameter("@P_SEMESTERNO", 1);
                   objParams[11] = new SqlParameter("@P_RECIEPTCODE", "TF");
                   objParams[12] = new SqlParameter("@P_STUDNAME", STUDNAME);
                   objParams[13] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                   objParams[13].Direction = ParameterDirection.Output;
               };
               object ret = objSQLHelper.ExecuteNonQuerySP("PKG_CREATE_PROADM_DEMAND", objParams, false);

               if (Convert.ToInt32(ret) == -99)
                   retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.TransactionFailed);
               else
                   retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.RecordSaved);
           }
           catch (Exception ex)
           {
               throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.ADMPProvisionalAdmissionAprovalController.PKG_CREATE_PROADM_DEMAND-> " + ex.ToString());
           }
           return retStatus;
           
       }

       //public int Create_ProAdmission_Online_Demand(int USERNO, int ADMBATCH, string APPLICATIONID, int DEGREENO, int BRANCHNO, int PAYMENTTYPE, double AMOUNT, double STANDARDFEE, int CREATEDBY, string ipAddress)
       //{
       //    int retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.Others);
       //    try
       //    {
       //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
       //        SqlParameter[] objParams = null;
       //        objParams = new SqlParameter[12];
       //        {
       //            objParams[0] = new SqlParameter("@P_BRANCHNO", BRANCHNO);
       //            objParams[1] = new SqlParameter("@P_DEGREENO", DEGREENO);
       //            objParams[2] = new SqlParameter("@P_FOR_SEMESTERNO", 1);
       //            objParams[3] = new SqlParameter("@P_PAYMENTTYPE", PAYMENTTYPE);
       //            objParams[4] = new SqlParameter("@P_UA_NO", USERNO);
       //            objParams[5] = new SqlParameter("@P_COLLEGE_CODE", 56);
       //            objParams[6] = new SqlParameter("@P_OVERWRITE", 1);
       //            objParams[7] = new SqlParameter("@P_COLLEGE_ID ", 56);
       //            objParams[8] = new SqlParameter("@P_ORGANIZATION_ID", 2);
       //            objParams[9] = new SqlParameter("@V_RECIEPTCODE", "TF");
       //            objParams[10] = new SqlParameter("@P_ADMBATCH", ADMBATCH);
       //            objParams[11] = new SqlParameter("@P_PROGRAMME_TYPE", 2); 
       //            objParams[12] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
       //            objParams[12].Direction = ParameterDirection.Output;
       //        };

       //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_FEECOLLECT_CREATE_DEMAND_BULK_FOR_SELECTED_STUDENTS_ADMP", objParams, false);

       //        if (Convert.ToInt32(ret) == -99)
       //            retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.TransactionFailed);
       //        else
       //            retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.RecordSaved);
       //    }
       //    catch (Exception ex)
       //    {
       //        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.ADMPProvisionalAdmissionAprovalController.PKG_CREATE_PROADM_DEMAND-> " + ex.ToString());
       //    }
       //    return retStatus;
       //}
    }
}
