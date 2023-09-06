//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HEALTH (Laboratory Test)                        
// CREATION DATE : 13-APR-2016                                                        
// CREATED BY    : MRUNAL SINGH 
//====================================================================================== 

using System;
using System.Data;
using System.Web;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Health;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.DAC;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLogicLayer.BusinessLogic
        {
            public class DirectMIssueController
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetPatientDetailsForDirectIssue(int serchtext)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_OPDNO", serchtext);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_DIRECT_PRESCRIPTION_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StockMaintnance.GetPatientDetailsForDirectIssue-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// This method is used to get patients history for direct medicine issue.
                /// </summary>
                //public DataSet GetDirectIssueHistory(int PID, string PatientCat)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = new SqlParameter[]
                //            { 
                //              new SqlParameter("@P_PID",PID),  
                //              new SqlParameter("@P_PATIENTCAT",PatientCat )
                //            };
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_DIRECT_MEDICINE_ISSUE_HISTORY", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.DirectMIssueController.GetDirectIssueHistory() --> " + ex.Message + " " + ex.StackTrace);
                //    }
                //    return ds;
                //}

                // TO GET LIST OF PRESCRIPTION FOR SECOND TIME IN DIRECT ISSUE.
                public DataSet GetPreviousPrescriptionForIssue(int OPDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_OPDNO", OPDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_PREVIOUS_PRESCRIPTION_FOR_ISSUE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DirectMIssueController.GetPreviousPrescriptionForIssue-> " + ex.ToString());
                    }
                    return ds;
                }



                /// <summary>
                /// This method is used to get idno of Patient by Employee Code or Student RegNo.
                /// </summary>
                /// parameter SearchText is either Employee Code or Student RegNo.
                /// 
                public DataSet GetPatientInDirectIssue(string SearchText, int PatientCat)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            { 
                              new SqlParameter("@P_SEARCH_TEXT",SearchText),
                              new SqlParameter("@P_PATIENTCAT",PatientCat )
                            };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_GET_IDNO_BY_EMPLOYEECODE_REGNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.HealthTransactionController.GetPatientIDByCodeRegNo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }



                /// <summary>
                /// This method is used to insert patient details when doing direct issue.
                /// <summary>
                ///   
                public int AddDirectMedicineIssue(DirectMIssue objD)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    int DOPDID = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            {                                 
                                
                                 new SqlParameter("@P_PID",objD.PID),
                                 new SqlParameter("@P_DRID",objD.DRID),
                                 new SqlParameter("@P_OPDDATE", System.DateTime.Now),
                                 new SqlParameter("@P_OPDTIME", System.DateTime.Now),
                                 new SqlParameter("@P_COMPLAINT",objD.COMPLAINT),
                                 new SqlParameter("@P_FINDING",objD.FINDING),
                                 new SqlParameter("@P_WEIGHT",objD.WEIGHT),
                                 new SqlParameter("@P_BP",objD.BP),
                                 new SqlParameter("@P_TEMP",objD.TEMP),
                                 new SqlParameter("@P_PULSE",objD.PULSE),
                                 new SqlParameter("@P_RESP",objD.RESP),
                                 new SqlParameter("@P_IP_ADDRESS",objD.IP_ADDRESS),
                                 new SqlParameter("@P_MAC_ADDRESS",objD.MAC_ADDRESS),
                                 new SqlParameter("@P_COLLEGE_CODE",objD.COLLEGE_CODE),
                                 new SqlParameter("@P_DEPENDENTID",objD.DEPENDENTID),
                                 new SqlParameter("@P_PATIENT_CODE",objD.PATIENT_CODE),
                                 new SqlParameter("@P_PATIENT_NAME",objD.PATIENT_NAME),
                                 new SqlParameter("@P_REFERENCE_BY",objD.REFERENCE_BY),
                                 new SqlParameter("@P_AGE",objD.AGE),
                                 new SqlParameter("@P_SEX",objD.SEX),                               
                                 new SqlParameter("@P_DOPDID",DOPDID),
                            };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_DIRECT_MEDICINE_ISSUE_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.HealthTransactionController.AddPreviousDirectIssue()-> " + ex.ToString());
                    }
                    return retStatus;
                }


                /// <summary>
                /// This method is used to insert Doctor details in the Hel_DoctorMaster Table
                /// <summary>
                ///   
                public int AddHelPrescriptionInsert(DirectMIssue objD)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    int PRESCNO = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            {                                  
                                 new SqlParameter("@P_DOPDNO",objD.DOPDNO),
                                 new SqlParameter("@P_ADMNO",objD.ADMNO),
                                 new SqlParameter("@P_ITEMNAME",objD.ITEMNAME),
                                 new SqlParameter("@P_INO",objD.INO),
                                 new SqlParameter("@P_QTY",objD.QTY),                              
                                 new SqlParameter("@P_DOSES",objD.DOSES),
                                 new SqlParameter("@P_SPINST",objD.SPINST),
                                 new SqlParameter("@P_PRESCRIPTION_STATUS",objD.PRESCRIPTION_STATUS),
                                 new SqlParameter("@P_PID",objD.PID),
                                 new SqlParameter("@P_NOOFDAYS",objD.NOOFDAYS),
                                 new SqlParameter("@P_IP_ADDRESS",objD.IP_ADDRESS),
                                 new SqlParameter("@P_MAC_ADDRESS",objD.MAC_ADDRESS),
                                 new SqlParameter("@P_COLLEGE_CODE",objD.COLLEGE_CODE),
                                 new SqlParameter("@P_PRESCNO",PRESCNO),
                            };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_PRECREPTION_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.HealthTransactionController.AddHelPrescriptionInsert()-> " + ex.ToString());
                    }
                    return retStatus;
                }


                /// <summary>
                /// This method is used to insert Doctor details in the Hel_DoctorMaster Table
                /// <summary>
                ///   
                public int AddDirectPrescription(DirectMIssue objD)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    int PRESCNO = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            {                                  
                                 new SqlParameter("@P_DOPDNO",objD.DOPDNO),
                                 new SqlParameter("@P_MEDICINE_PRES",objD.MEDICINE_PRES),
                                 new SqlParameter("@P_PID",objD.PID),                                 
                                 new SqlParameter("@P_IP_ADDRESS",objD.IP_ADDRESS),
                                 new SqlParameter("@P_MAC_ADDRESS",objD.MAC_ADDRESS),
                                 new SqlParameter("@P_COLLEGE_CODE",objD.COLLEGE_CODE),
                                 new SqlParameter("@P_PRESCNO",PRESCNO),
                            };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_DIRECT_ISSUE_PRECREPTION_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.HealthTransactionController.AddHelPrescriptionInsert()-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// This method is used to get total of available quantity of item
                /// <summary>
                /// 
                public DataSet GetTotal(int itemno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            { 
                              new SqlParameter("@P_ITEMNO",itemno)  
                            };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_DIRECT_TOTAL_BY_ITEMNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.DirectMIssueController.GetTotal() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }



             

            }
        }
    }
}
