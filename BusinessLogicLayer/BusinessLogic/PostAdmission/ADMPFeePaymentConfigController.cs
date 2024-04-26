using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IITMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Data;
/*
---------------------------------------------------------------------------------------------------------------------------                                                                      
Created By  :                                                                 
Created On  :                                                    
Purpose     :                                      
Version     :                                                             
---------------------------------------------------------------------------------------------------------------------------                                                                        
Version   Modified On   Modified By        Purpose                                                                        
---------------------------------------------------------------------------------------------------------------------------                                                                        
1.0.1     14-03-2024    Isha Kanojiya      Added Branch and Start/End Payment Date and Provision Admission Date                         
------------------------------------------- -------------------------------------------------------------------------------  
1.0.2     29-03-2024    Isha Kanojiya      Added Admbatch,Programtype and Degreeno
--------------------------------------------------------------------------------------------------------------------------- 
 */
namespace BusinessLogicLayer.BusinessLogic.PostAdmission
{

    public class ADMPFeePaymentConfigController
    {
        private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public string InsertADMPFeePayConfig(ADMPFeePaymentConfigEntity objafpc)
        {
            string retStatus = string.Empty;
            try
            {

                //@P_ACTIVITYFOR  INT,   
                //@P_ADMBATCH   INT,  
                //@P_PROGRAMTYPE  INT,  
                //@P_DEGREENO   INT,  
                //@P_PAYMENTCATEGORY INT,  
                //@P_FEEPAYMENT  NUMERIC(8,2),  
                //@P_STARTDATE  DATETIME,  
                //@P_ENDDATE   DATETIME,  
                //@P_ACTIVITYSTATUS BIT,  
                //@P_OUT    INT OUT  
                //Admquotano     Rcpttypeno

                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[14];

                objParams[0] = new SqlParameter("@P_ACTIVITYFOR", objafpc.Activityfor);
                objParams[1] = new SqlParameter("@P_ADMBATCH", objafpc.Admbatch);
                objParams[2] = new SqlParameter("@P_PROGRAMTYPE", objafpc.Programtype);
                objParams[3] = new SqlParameter("@P_DEGREENO", objafpc.Degreeno);
                //<1.0.1>
                objParams[4] = new SqlParameter("@P_BRANCHNO", objafpc.Branchno);
                //</1.0.1>
                objParams[5] = new SqlParameter("@P_PAYMENTCATEGORY", objafpc.Paymentcategory);
                objParams[6] = new SqlParameter("@P_FEEPAYMENT", objafpc.Feepayment);
                objParams[7] = new SqlParameter("@P_STARTDATE", objafpc.Startdate);
                objParams[8] = new SqlParameter("@P_ENDDATE", objafpc.Enddate);
                //<1.0.1>
                objParams[9] = new SqlParameter("@P_OFFICEVISITSTARTDATE", objafpc.OfficeVisitStartDate);
                objParams[10] = new SqlParameter("@P_OFFICEVISITENDDATE", objafpc.OfficeVisitEndDate);
                objParams[11] = new SqlParameter("@P_PROVISIONALADMISSIONOFFERVALIDDATE",objafpc.ProvisionalAdmissionDate);
                //</1.0.1>
                objParams[12] = new SqlParameter("@P_ACTIVITYSTATUS", objafpc.Activitystatus);         
                objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[13].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_ADMP_FEE_PAYMENT_CONFIG", objParams, true);
                retStatus = ret.ToString();

                return retStatus;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.InsertADMPFeePayConfig-> " + ex.ToString());
            }
        }

        public string UpdateADMPFeePayConfig(ADMPFeePaymentConfigEntity objafpc)
        {
            string retStatus = string.Empty;
            try
            {

                /*@P_CONFIGID   INT,  
                 @P_ACTIVITYFOR  INT,   
                 @P_ADMBATCH   INT,  
                 @P_PROGRAMTYPE  INT,  
                 @P_DEGREENO   INT,  
                 @P_PAYMENTCATEGORY INT,  
                 @P_FEEPAYMENT  NUMERIC(8,2),  
                 @P_STARTDATE  DATETIME,  
                 @P_ENDDATE   DATETIME,  
                 @P_ACTIVITYSTATUS BIT,  
                 @P_OUT*/
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[15];

                objParams[0] = new SqlParameter("@P_CONFIGID", objafpc.ConfigID);
                objParams[1] = new SqlParameter("@P_ACTIVITYFOR", objafpc.Activityfor);
                objParams[2] = new SqlParameter("@P_ADMBATCH", objafpc.Admbatch);
                objParams[3] = new SqlParameter("@P_PROGRAMTYPE", objafpc.Programtype);
                objParams[4] = new SqlParameter("@P_DEGREENO", objafpc.Degreeno);
                //<1.0.1>
                objParams[5] = new SqlParameter("@P_BRANCHNO", objafpc.Branchno);
                //</1.0.1>
                objParams[6] = new SqlParameter("@P_PAYMENTCATEGORY", objafpc.Paymentcategory);
                objParams[7] = new SqlParameter("@P_FEEPAYMENT", objafpc.Feepayment);
                objParams[8] = new SqlParameter("@P_STARTDATE", objafpc.Startdate);
                objParams[9] = new SqlParameter("@P_ENDDATE", objafpc.Enddate);
                //<1.0.1>
                objParams[10] = new SqlParameter("@P_OFFICEVISITSTARTDATE", objafpc.OfficeVisitStartDate);
                objParams[11] = new SqlParameter("@P_OFFICEVISITENDDATE", objafpc.OfficeVisitEndDate);
                objParams[12] = new SqlParameter("@P_PROVISIONALADMISSIONOFFERVALIDDATE", objafpc.ProvisionalAdmissionDate);
                //</1.0.1>
                objParams[13] = new SqlParameter("@P_ACTIVITYSTATUS", objafpc.Activitystatus);
                objParams[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[14].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPD_ADMP_FEE_PAYMENT_CONFIG", objParams, true);
                retStatus = ret.ToString();

                return retStatus;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.UpdateADMPFeePayConfig-> " + ex.ToString());
            }
        }

        public DataSet GetRetADMPFeePayConfigListData(ADMPFeePaymentConfigEntity objafpc, int FeePayConfig_ID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@P_CONFIGID", FeePayConfig_ID);
                //<1.0.2>
                sqlParams[1] = new SqlParameter("@P_ADMBATCH ", objafpc.Admbatch);
                sqlParams[2] = new SqlParameter("@P_PROGRAMTYPE", objafpc.Programtype);
                sqlParams[3] = new SqlParameter("@P_DEGREENO ", objafpc.Degreeno);
                //</1.0.2>
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_RET_ALL_ADMP_FEE_PAYMENT_CONFIG", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GetRetADMPFeePayConfigListData-> " + ex.ToString());
            }
            return ds;
        }
    }
}
