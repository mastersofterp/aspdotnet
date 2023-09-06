using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IITMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;
using System.Data;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Academic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

namespace IITMS.UAIMS.BusinessLogicLayer.BusinessLogic
{
   public class OnlineAdmissionGroupwiseController
   {
       string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

       public int AddOnline(Config objConfig, int form_category, string STime, string ETime, int UgPg, int orgId, int activeStatus, string DegreeBranchno, int groupno, int is_nri, double nrifees)
       {
           //int status = Convert.ToInt32(CustomStatus.Others);
           int status = 0;
           try
           {
               SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
               SqlParameter[] objParams = null;

               //Add New Adm
               objParams = new SqlParameter[24];
               objParams[0] = new SqlParameter("@P_ADMBATCH", objConfig.Admbatch);
               objParams[1] = new SqlParameter("@P_DEGREENO", 0);

               objParams[2] = new SqlParameter("@P_BRANCHNO", 0);     //Added By Yogesh K. on Date: 25-01-2023
              
               objParams[3] = new SqlParameter("@P_ADMSTRDATE", objConfig.Config_SDate);
               objParams[4] = new SqlParameter("@P_STARTTIME", STime);
               objParams[5] = new SqlParameter("@P_ADMENDDATE", objConfig.Config_EDate);
               objParams[6] = new SqlParameter("@P_ENDTIME", ETime);
               objParams[7] = new SqlParameter("@P_DETAILS", objConfig.Details);
               objParams[8] = new SqlParameter("@P_FEES", objConfig.Fees);
               objParams[9] = new SqlParameter("@P_FORM_CATEGORY", form_category);
               objParams[10] = new SqlParameter("@P_COLLEGEID", objConfig.College_Id);
               objParams[11] = new SqlParameter("@P_UGPG", UgPg);
               objParams[12] = new SqlParameter("@P_ADMTYPE", objConfig.AdmType);

               objParams[13] = new SqlParameter("@P_CDBNO", objConfig.Cdbno);
               objParams[14] = new SqlParameter("@P_DEPTNO", objConfig.Deptno);

               objParams[15] = new SqlParameter("@P_AGE", objConfig.Age);          
               objParams[16] = new SqlParameter("@P_ORGANIZATION_ID", orgId);
               objParams[17] = new SqlParameter("@P_ACTIVE_STATUS", activeStatus);
               objParams[18] = new SqlParameter("@P_DEGREE_BRANCHNO", DegreeBranchno);
               objParams[19] = new SqlParameter("@P_GROUPNO", groupno);
               objParams[20] = new SqlParameter("@P_CONFIGID", 1111);
               objParams[21] = new SqlParameter("@P_ISNRI", is_nri);
               objParams[22] = new SqlParameter("@P_NRI_FEES", nrifees);
               objParams[23] = new SqlParameter("@P_OUT", SqlDbType.Int);
               objParams[23].Direction = ParameterDirection.Output;

               object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ADD_ADMISSION_CONFIG_GROUPWISE", objParams, true);


               //object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ADD_ADMISSION_CONFIG_GROUPWISE", objParams, true);

               if (ret != null && ret.ToString() == "2")
                   status = Convert.ToInt32(CustomStatus.RecordSaved);
               else if (ret != null && ret.ToString() == "-99")
                   status = Convert.ToInt32(CustomStatus.Error);
               else if (ret != null && ret.ToString() == "1")
                   status = Convert.ToInt32(CustomStatus.DuplicateRecord);
               
           }
           catch (Exception ex)
           {
               status = Convert.ToInt32(CustomStatus.Error);
               throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.AddSession-> " + ex.ToString());
           }
           return status;
       }

       public int UpdateOnline(Config objConfig, int form_category, string STime, string ETime, int UgPg, int activeStatus, string ConfigID, string DegreeBranchno,int groupno, int OrgId, int is_nri, double nrifees)
       {
           int retStatus = Convert.ToInt32(CustomStatus.Others);

           try
           {
               SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
               SqlParameter[] objParams = null;

               //update
               objParams = new SqlParameter[24];
               objParams[0] = new SqlParameter("@P_ADMBATCH", objConfig.Admbatch);
               objParams[1] = new SqlParameter("@P_DEGREENO", 0);

               objParams[2] = new SqlParameter("@P_BRANCHNO", 0);      

               objParams[3] = new SqlParameter("@P_ADMSTRDATE", objConfig.Config_SDate);
               objParams[4] = new SqlParameter("@P_STARTTIME", STime);
               objParams[5] = new SqlParameter("@P_ADMENDDATE", objConfig.Config_EDate);
               objParams[6] = new SqlParameter("@P_ENDTIME", ETime);
               objParams[7] = new SqlParameter("@P_DETAILS", objConfig.Details);
               objParams[8] = new SqlParameter("@P_FEES", objConfig.Fees);
               objParams[9] = new SqlParameter("@P_FORM_CATEGORY", form_category);
               objParams[10] = new SqlParameter("@P_COLLEGEID", objConfig.College_Id);
               objParams[11] = new SqlParameter("@P_UGPG", UgPg);
               objParams[12] = new SqlParameter("@P_CONFIGID", ConfigID);
               objParams[13] = new SqlParameter("@P_ADMTYPE", objConfig.AdmType);

               objParams[14] = new SqlParameter("@P_CDBNO", objConfig.Cdbno);
               objParams[15] = new SqlParameter("@P_DEPTNO", objConfig.Deptno);

               objParams[16] = new SqlParameter("@P_AGE", objConfig.Age);         
               objParams[17] = new SqlParameter("@P_ACTIVE_STATUS", activeStatus);
               objParams[18] = new SqlParameter("@P_DEGREE_BRANCHNO", DegreeBranchno);
               objParams[19] = new SqlParameter("@P_GROUPNO", groupno);
               objParams[20] = new SqlParameter("@P_ORGANIZATION_ID", OrgId);
               objParams[21] = new SqlParameter("@P_ISNRI", is_nri);
               objParams[22] = new SqlParameter("@P_NRI_FEES", nrifees);
               objParams[23] = new SqlParameter("@P_OUT",SqlDbType.Int);
               objParams[23].Direction = ParameterDirection.Output;

               object ret = (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPD_ADMISSION_CONFIG_GROUPWISE", objParams, true));
               if (ret != null && ret.ToString() == "2")
                   retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
               else if (ret != null && ret.ToString() == "-99")
                   retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
               else if (ret != null && ret.ToString() == "1")
                   retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
               else if (ret != null && ret.ToString() == "3")
                   retStatus = Convert.ToInt32(CustomStatus.RecordExist);
           }
           catch (Exception ex)
           {
               retStatus = Convert.ToInt32(CustomStatus.Error);
               throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.UpdateCT-> " + ex.ToString());
           }
           return retStatus;
       }

       public DataSet GetAllConfig()
       {
           DataSet ds = null;
           try
           {
               SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
               SqlParameter[] objParams = new SqlParameter[0];

               ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_CONFIG_GROUPWISE", objParams);

           }
           catch (Exception ex)
           {
               throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.GetAllConfig-> " + ex.ToString());
           }
           return ds;
       }


       public SqlDataReader GetSingleConfig(string ConfigID)
       {
           SqlDataReader dr = null;
           try
           {
               SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
               SqlParameter[] objParams = new SqlParameter[1];
               objParams[0] = new SqlParameter("@P_CONFIGID", ConfigID);
               dr = objSQLHelper.ExecuteReaderSP("PKG_GET_CONFIG_BY_CONFIGID_GROUPWISE", objParams);
           }
           catch (Exception ex)
           {
               return dr;
               throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SessionController.GetSingleConfig-> " + ex.ToString());
           }
           return dr;
       }

    }
}
