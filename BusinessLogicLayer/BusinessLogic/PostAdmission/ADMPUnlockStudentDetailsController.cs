using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class ADMPUnlockStudentDetailsController
    {
        string uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        //public DataSet SearchUnlockStudentDetails(ADMPUnlockStudentDetails objUD)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
        //        SqlParameter[] objParams = new SqlParameter[] 
        //              { 
        //                 new SqlParameter("@P_APPLICATION_ID",objUD.ApplicationId),     
        //                 new SqlParameter("@P_FIRSTNAME",objUD.FirstName)         
        //              };
        //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMP_SEARCH_UNLOCK_STUDENT", objParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ADMPUnlockStudentDetailsController.SearchUnlockStudentDetails() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}
        public DataSet GetUnlockStudentDetailsADMP(ADMPUnlockStudentDetails objUD)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                SqlParameter[] objParams = new SqlParameter[] 
                      { 
                         new SqlParameter("@P_BATCHNO",objUD.BatchNo),   
                         new SqlParameter("@P_UGPGOT",objUD.UGPGOT),   
                         new SqlParameter("@P_DEGREENO",objUD.DegreeNo),
                         new SqlParameter("@P_BRANCHNO",objUD.BranchNo),
                         new SqlParameter("@P_COLLEGE_ID",objUD.CollegeId)
                      };
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ADMP_GET_UNLOCK_STUDENT_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ADMPUnlockStudentDetailsController.GetUnlockStudentDetails() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetUnlockStudentDetails(ADMPUnlockStudentDetails objUD)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                SqlParameter[] objParams = new SqlParameter[] 
                      { 
                         new SqlParameter("@P_BATCHNO",objUD.BatchNo),   
                         new SqlParameter("@P_UGPGOT",objUD.UGPGOT),   
                         new SqlParameter("@P_DEGREENO",objUD.DegreeNo),
                         new SqlParameter("@P_BRANCHNO",objUD.BranchNo),
                         new SqlParameter("@P_COLLEGE_ID",objUD.CollegeId)
                      };
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_UNLOCK_STUDENT_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ADMPUnlockStudentDetailsController.GetUnlockStudentDetails() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int InsUpdUnlockStudentDetails(ADMPUnlockStudentDetails objUD)
        {
            int retstatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[12];
                objParams[0] = new SqlParameter("@P_USERNO", objUD.Userno);
                objParams[1] = new SqlParameter("@P_ADMBATCH", objUD.BatchNo);
                objParams[2] = new SqlParameter("@P_DEGREENO", objUD.DegreeNo);
                objParams[3] = new SqlParameter("@P_BRANCHNO", objUD.BranchNo);
                objParams[4] = new SqlParameter("@P_ALLOWPROCESS", objUD.AllowProcess);
                objParams[5] = new SqlParameter("@P_STARTDATE", objUD.StartDate);
                objParams[6] = new SqlParameter("@P_ENDDATE", objUD.EndDate);
                objParams[7] = new SqlParameter("@P_STARTIME", objUD.StartTime);
                objParams[8] = new SqlParameter("@P_ENDTIME", objUD.EndTime);
                objParams[9] = new SqlParameter("@P_UA_NO", objUD.UaNo);
                objParams[10] = new SqlParameter("@P_ALLOWPROCESSFROM", objUD.AllowProcessFrom);
                objParams[11] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[11].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSUPD_UNLOCK_STUDENT", objParams, true);

                if (Convert.ToInt32(ret) == -99)
                    retstatus = Convert.ToInt32(ret);  //Convert.ToInt32(CustomStatus.Error);                  
                else
                    retstatus = Convert.ToInt32(ret);  //Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ApplicationProcessController.InsUpdUnlockStudentDetails() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retstatus;
        }
    }
}
