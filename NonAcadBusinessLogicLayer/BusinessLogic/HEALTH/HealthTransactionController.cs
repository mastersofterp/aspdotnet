//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HEALTH                              
// CREATION DATE : 13-FEB-2016                                                        
// CREATED BY    : MRUNAL SINGH                                      
// MODIFIED DATE :
// MODIFIED DESC :
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
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Health;

namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLogicLayer.BusinessLogic//.Health
        {
         public class HealthTransactionController
            {
                
                    private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                    /// <summary>
                    /// This method is used to Get Record(s) from HEL_OPDTRANSACTION table based on OPDID.
                    /// </summary>
                    public DataSet GetPatientDetailsByOPDID(int OPDID, string PatientCat)
                    {
                        DataSet ds = null;
                        try
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                            SqlParameter[] objParams = new SqlParameter[]
                            { 
                              new SqlParameter("@P_OPDID",OPDID),  
                              new SqlParameter("@P_PATIENTCAT",PatientCat )
                            };
                            ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_OPDTRAN_GET_BY_NO", objParams);
                        }
                        catch (Exception ex)
                        {
                            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.HealthTransactionController.GetPatientDetailsByOPDID() --> " + ex.Message + " " + ex.StackTrace);
                        }
                        return ds;
                    }
                    

                    /// <summary>
                    /// This method is used to get idno of Patient by Employee Code or Student RegNo.
                    /// </summary>
                    /// parameter SearchText is either Employee Code or Student RegNo.
                    /// 
                    public DataSet GetPatientIDByCodeRegNo(string SearchText, int PatientCat)
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

                    public DataSet GetTestContentDetails(int TITLENO)
                    {
                        DataSet ds = null;
                        try
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                            SqlParameter[] objParams = new SqlParameter[]
                            { 
                              new SqlParameter("@P_TITLENO", TITLENO)                            
                            };
                            ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_GET_CONTENT_BY_TITLENO", objParams);
                        }
                        catch (Exception ex)
                        {
                            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.HealthTransactionController.GetTestContentDetails() --> " + ex.Message + " " + ex.StackTrace);
                        }
                        return ds;
                    }

                    /// <summary>
                    /// This method is used to get stock availability of selected item for prescription.
                    /// </summary>
                    public DataSet GetInsufficientStockDetails(int ITEM_NO)
                    {
                        DataSet ds = null;
                        try
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                            SqlParameter[] objParams = new SqlParameter[]
                            { 
                              new SqlParameter("@P_ITEM_NO",ITEM_NO),  
                              
                            };
                            ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_GET_INSUFFICIENT_STOCK_DETAILS", objParams);
                        }
                        catch (Exception ex)
                        {
                            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.HealthTransactionController.GetInsufficientStockDetails() --> " + ex.Message + " " + ex.StackTrace);
                        }
                        return ds;
                    }
                    /// <summary>
                    /// This method is used to insert Doctor details in the HEALTH_PATIENT_DETAILS Table
                    /// <summary>
                    ///   
                    public int AddHelOPDINSERT(HealthTransactions objHel)
                    {
                        int retStatus = Convert.ToInt32(CustomStatus.Others);
                        int OPDID = 0;
                        try
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                            SqlParameter[] objParams = new SqlParameter[]
                            {                                  
                                 new SqlParameter("@P_RPID",objHel.RPID),
                                 new SqlParameter("@P_PID",objHel.PID),
                                 new SqlParameter("@P_DRID",objHel.DRID),
                                 new SqlParameter("@P_OPDDATE",objHel.OPDDATE),
                                 new SqlParameter("@P_OPDTIME",objHel.OPDTIME),
                                 new SqlParameter("@P_COMPLAINT",objHel.COMPLAINT),
                                 new SqlParameter("@P_FINDING",objHel.FINDING),
                                 new SqlParameter("@P_DIAGNOSIS",objHel.DIAGNOSIS),
                                 new SqlParameter("@P_INSTRUCTION",objHel.INSTRUCTION),
                                 new SqlParameter("@P_REMARK",objHel.REMARK),
                                 //new SqlParameter("@P_NEXTDT",objHel.NEXTDT),
                                 //new SqlParameter("@P_NEXTTIME",objHel.NEXTTIME),
                                 new SqlParameter("@P_HEIGHT",objHel.HEIGHT),
                                 new SqlParameter("@P_WEIGHT",objHel.WEIGHT),
                                 new SqlParameter("@P_BP",objHel.BP),
                                 new SqlParameter("@P_TEMP",objHel.TEMP),
                                 new SqlParameter("@P_PULSE",objHel.PULSE),
                                 new SqlParameter("@P_RESP",objHel.RESP),
                                 new SqlParameter("@P_IP_ADDRESS",objHel.IP_ADDRESS),
                                 new SqlParameter("@P_MAC_ADDRESS",objHel.MAC_ADDRESS),
                                 new SqlParameter("@P_COLLEGE_CODE",objHel.COLLEGE_CODE),
                                 new SqlParameter("@P_DEPENDENTID",objHel.DEPENDENTID),
                                 new SqlParameter("@P_PATIENT_CODE",objHel.PATIENT_CODE),
                                 new SqlParameter("@P_PATIENT_NAME",objHel.PATIENT_NAME),
                                 new SqlParameter("@P_REFERENCE_BY",objHel.REFERENCE_BY),
                                 new SqlParameter("@P_AGE",objHel.AGE),
                                 new SqlParameter("@P_SEX",objHel.SEX),
                                 new SqlParameter("@P_TEST_GIVEN",objHel.TEST_GIVEN),
                                 new SqlParameter("@P_TEST_TITLE_TBL", objHel.TEST_TITLE),
                                 new SqlParameter("@P_TITLE_CONTENTS_TBL", objHel.TITLE_CONTENTS),
                                 new SqlParameter("@P_ISSUE_CERTIFICATES",objHel.ISSUE_CERTIFICATES),  
                                 new SqlParameter("@P_BLOOD_GROUP",objHel.BLOOD_GROUP),
                                 new SqlParameter("@P_CHIEF_COMPLAINT", objHel.CHIEF_COMPLAINT),                                
                                 new SqlParameter("@P_PAST_HISTORY", objHel.PAST_HISTORY),
                                 new SqlParameter("@P_FAMILY_HISTORY", objHel.FAMILY_HISTORY),
                                 new SqlParameter("@P_CHRONIC_DIESEASE", objHel.CHRONIC_DIESEASE),                                
                                 new SqlParameter("@P_SURGICAL_PROCEDURE", objHel.SURGICAL_PROCEDURE),                                 
                                 new SqlParameter("@P_REFERRED_TO", objHel.REFERRED_TO),
                                 new SqlParameter("@P_ADMITTED_STATUS", objHel.ADMITTED_STATUS),
                                 new SqlParameter("@P_RANDOM_SUGAR_LEVEL", objHel.RANDOM_SUGAR_LEVEL),
                                 new SqlParameter("@P_OPDID",OPDID),
                            };
                            objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;

                            if (objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_OPDTRAN_INSERT", objParams, false) != null)
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        catch (Exception ex)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.HealthTransactionController.AddHelPatientDetails()-> " + ex.ToString());
                        }
                        return retStatus;
                    }

                    /// <summary>
                    /// This method is used to insert Doctor details in the HEALTH_PATIENT_DETAILS Table
                    /// <summary>
                    ///   
                    public int AddHelOPDUPDATE(HealthTransactions objHel)
                    {
                        int retStatus = Convert.ToInt32(CustomStatus.Others);                      
                        try
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                            SqlParameter[] objParams = new SqlParameter[]
                            {                          
                                 new SqlParameter("@P_OPDID",objHel.OPDNO),                                
                                 new SqlParameter("@P_PID",objHel.PID),
                                 new SqlParameter("@P_DRID",objHel.DRID),
                                 new SqlParameter("@P_OPDDATE",objHel.OPDDATE),
                                 new SqlParameter("@P_OPDTIME",objHel.OPDTIME),
                                 new SqlParameter("@P_COMPLAINT",objHel.COMPLAINT),
                                 new SqlParameter("@P_FINDING",objHel.FINDING),
                                 new SqlParameter("@P_DIAGNOSIS",objHel.DIAGNOSIS),
                                 new SqlParameter("@P_INSTRUCTION",objHel.INSTRUCTION),
                                 new SqlParameter("@P_REMARK",objHel.REMARK),                               
                                 new SqlParameter("@P_HEIGHT",objHel.HEIGHT),
                                 new SqlParameter("@P_WEIGHT",objHel.WEIGHT),
                                 new SqlParameter("@P_BP",objHel.BP),
                                 new SqlParameter("@P_TEMP",objHel.TEMP),
                                 new SqlParameter("@P_PULSE",objHel.PULSE),
                                 new SqlParameter("@P_RESP",objHel.RESP),
                                 new SqlParameter("@P_IP_ADDRESS",objHel.IP_ADDRESS),
                                 new SqlParameter("@P_MAC_ADDRESS",objHel.MAC_ADDRESS),
                                 new SqlParameter("@P_COLLEGE_CODE",objHel.COLLEGE_CODE),
                                 new SqlParameter("@P_DEPENDENTID",objHel.DEPENDENTID),
                                 new SqlParameter("@P_PATIENT_CODE",objHel.PATIENT_CODE),
                                 new SqlParameter("@P_PATIENT_NAME",objHel.PATIENT_NAME),
                                 new SqlParameter("@P_REFERENCE_BY",objHel.REFERENCE_BY),
                                 new SqlParameter("@P_AGE",objHel.AGE),
                                 new SqlParameter("@P_SEX",objHel.SEX),
                                 new SqlParameter("@P_TEST_GIVEN",objHel.TEST_GIVEN),
                                 new SqlParameter("@P_TEST_TITLE_TBL", objHel.TEST_TITLE),
                                 new SqlParameter("@P_TITLE_CONTENTS_TBL", objHel.TITLE_CONTENTS),
                                 new SqlParameter("@P_ISSUE_CERTIFICATES",objHel.ISSUE_CERTIFICATES),  
                                 new SqlParameter("@P_BLOOD_GROUP",objHel.BLOOD_GROUP),
                                 new SqlParameter("@P_CHIEF_COMPLAINT", objHel.CHIEF_COMPLAINT),                                
                                 new SqlParameter("@P_PAST_HISTORY", objHel.PAST_HISTORY),
                                 new SqlParameter("@P_FAMILY_HISTORY", objHel.FAMILY_HISTORY),
                                 new SqlParameter("@P_CHRONIC_DIESEASE", objHel.CHRONIC_DIESEASE),                                
                                 new SqlParameter("@P_SURGICAL_PROCEDURE", objHel.SURGICAL_PROCEDURE),                                 
                                 new SqlParameter("@P_REFERRED_TO", objHel.REFERRED_TO),
                                 new SqlParameter("@P_ADMITTED_STATUS", objHel.ADMITTED_STATUS),
                                 new SqlParameter("@P_RANDOM_SUGAR_LEVEL", objHel.RANDOM_SUGAR_LEVEL),
                                 new SqlParameter("@P_OUT",objHel.OPDID),                                 
                            };
                            objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;

                            if (objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_OPDTRAN_UPDATE", objParams, false) != null)
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        catch (Exception ex)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.HealthTransactionController.AddHelOPDUPDATE()-> " + ex.ToString());
                        }
                        return retStatus;
                    }

                   




                    /// <summary>
                    /// This method is used to insert Doctor details in the Hel_DoctorMaster Table
                    /// <summary>
                    ///   
                    public int AddHelPrescriptionInsert(HealthTransactions objHel)
                    {
                        int retStatus = Convert.ToInt32(CustomStatus.Others);
                        int PRESCNO = 0;
                        try
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                            SqlParameter[] objParams = new SqlParameter[]
                            {                                  
                                 new SqlParameter("@P_OPDNO",objHel.OPDNO),
                                 new SqlParameter("@P_MEDICINE_PRES",objHel.MEDICINE_PRES),
                                 new SqlParameter("@P_PID",objHel.PID),                                 
                                 new SqlParameter("@P_IP_ADDRESS",objHel.IP_ADDRESS),
                                 new SqlParameter("@P_MAC_ADDRESS",objHel.MAC_ADDRESS),
                                 new SqlParameter("@P_COLLEGE_CODE",objHel.COLLEGE_CODE),
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
                    /// This method is used to search patient of different category.
                    /// <summary>
                    ///  
                    public DataTable RetrievePatientDetails(string search, string category)//, int PCat)
                    {
                        DataTable dt = null;
                        try
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                            SqlParameter[] objParams = null;
                            objParams = new SqlParameter[2];
                            objParams[0] = new SqlParameter("@P_SEARCH", search);
                            objParams[1] = new SqlParameter("@P_CATEGORY", category);
                           // objParams[2] = new SqlParameter("@P_PATIENT_CATEGORY", PCat);

                            dt = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_SP_SEARCH_PATIENT", objParams).Tables[0];

                        }
                        catch (Exception ex)
                        {
                            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrievePatientDetails-> " + ex.ToString());
                        }
                        return dt;
                    }


                    public int AddHealthMedicineIsuue(HealthTransactions objHelTran)
                    {
                        int retStatus = Convert.ToInt32(CustomStatus.Others);

                        try
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                            SqlParameter[] objParams = null;
                            objParams = new SqlParameter[5];
                            objParams[0] = new SqlParameter("@P_PATIENTNO", objHelTran.PID);
                            objParams[1] = new SqlParameter("@P_DOCNO", objHelTran.DRID);
                            objParams[2] = new SqlParameter("@P_OPDNO", objHelTran.OPDID);
                            objParams[3] = new SqlParameter("@P_MEDICINE_ISSUE_TBL", objHelTran.MEDICINE_ISSUE);                           
                            objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                            objParams[4].Direction = ParameterDirection.Output;

                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_MEDICINE_ISSUE_ENTRY", objParams, true);                                                
                            if (Convert.ToInt32(ret) == -99)
                            {
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            }
                            else if (Convert.ToInt32(ret) == 2627)
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                            }
                            else
                            {
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            }
                        }
                        catch (Exception ex)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.HealthTransactionController.AddHealthMedicineIsuue()-> " + ex.ToString());
                        }
                        return retStatus;
                    }



                    public int DeleteHealthMedicineIsuue(int Itemno, int Opdno)
                    {
                        int retStatus = Convert.ToInt32(CustomStatus.Others);

                        try
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                            SqlParameter[] objParams = null;
                            objParams = new SqlParameter[3];
                            objParams[0] = new SqlParameter("@P_ITEMNO", Itemno);
                            objParams[1] = new SqlParameter("@P_OPDNO", Opdno);
                            objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                            objParams[2].Direction = ParameterDirection.Output;

                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_DEL_MEDICINE", objParams, true);
                            if (Convert.ToInt32(ret) == -99)
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                        }
                        catch (Exception ex)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.HealthTransactionController.AddHelPrescriptionInsert()-> " + ex.ToString());
                        }
                        return retStatus;
                    }

                    public int UpdateHealthMedicineIsuue(int Itemno, int Opdno, int Dosesno, int Med_Qty)
                    {
                        int retStatus = Convert.ToInt32(CustomStatus.Others);

                        try
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                            SqlParameter[] objParams = null;
                            objParams = new SqlParameter[5];
                            objParams[0] = new SqlParameter("@P_ITEMNO", Itemno);
                            objParams[1] = new SqlParameter("@P_OPDNO", Opdno);
                            objParams[2] = new SqlParameter("@P_DOSENO", Dosesno);
                            objParams[3] = new SqlParameter("@P_MED_QTY", Med_Qty);
                            objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                            objParams[4].Direction = ParameterDirection.Output;

                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_UPDATE_MEDICINE", objParams, true);
                            if (Convert.ToInt32(ret) == -99)
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        catch (Exception ex)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.HealthTransactionController.AddHelPrescriptionInsert()-> " + ex.ToString());
                        }
                        return retStatus;
                    }


                    //public int AddHealthMedicineIsuue(int PatientNo, int DoctorNo, int OpdNo, string itemno, string qtygiven, string doses, string qtyissue, string qtyBal, string issuestatus)
                    //{
                    //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    //    try
                    //    {
                    //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                    //        SqlParameter[] objParams = null;
                    //        objParams = new SqlParameter[10];
                    //        objParams[0] = new SqlParameter("@P_PATIENTNO", PatientNo);
                    //        objParams[1] = new SqlParameter("@P_DOCNO", DoctorNo);
                    //        objParams[2] = new SqlParameter("@P_OPDNO", OpdNo);
                    //        objParams[3] = new SqlParameter("@P_ITEMNO", itemno);
                    //        objParams[4] = new SqlParameter("@P_QTYGIVEN", qtygiven);
                    //        objParams[5] = new SqlParameter("@P_QTYDOSES", doses);
                    //        objParams[6] = new SqlParameter("@P_QTYISSUE", qtyissue);
                    //        //objParams[7] =new SqlParameter("",qtyAvail);
                    //        objParams[7] = new SqlParameter("@P_QTYBAL", qtyBal);
                    //        objParams[8] = new SqlParameter("@P_ISSUESTATUS", issuestatus);
                    //        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                    //        objParams[9].Direction = ParameterDirection.Output;

                    //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_MEDICINE_ISSUE_ENTRY", objParams, true);
                    //        if (Convert.ToInt32(ret) == -99)
                    //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    //        else
                    //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        retStatus = Convert.ToInt32(CustomStatus.Error);
                    //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.HealthTransactionController.AddHelPrescriptionInsert()-> " + ex.ToString());
                    //    }
                    //    return retStatus;
                    //}

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
                            ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_GET_TOTAL_QUANTITY_BY_ITEMNO", objParams);
                        }
                        catch (Exception ex)
                        {
                            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.LabController.GetPatientDetailsByPID() --> " + ex.Message + " " + ex.StackTrace);
                        }
                        return ds;
                    }

                    /// <summary>
                    /// This method is used to Get Record(s) from HEL_OPDTRANSACTION table based on OPDID.
                    /// </summary>
                    public DataSet GetAllPrescriptions(int IDNO)
                    {
                        DataSet ds = null;
                        try
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                            SqlParameter[] objParams = new SqlParameter[]
                            { 
                              new SqlParameter("@P_IDNO",IDNO)                              
                            };
                            ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_GET_PRES_DETAILS_BY_ID", objParams);
                        }
                        catch (Exception ex)
                        {
                            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.HealthTransactionController.GetAllPrescriptions() --> " + ex.Message + " " + ex.StackTrace);
                        }
                        return ds;
                    }



                    /// <summary>
                    /// This method is used to insert Doctor details in the HEALTH_PATIENT_DETAILS Table
                    /// <summary>
                    ///   
                    public int AddPrescriptionBasedOPD(HealthTransactions objHel)
                    {
                        int retStatus = Convert.ToInt32(CustomStatus.Others);
                        int OPDID = 0;
                        try
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                            SqlParameter[] objParams = new SqlParameter[]
                            {                                 
                                
                                 new SqlParameter("@P_PID",objHel.PID),
                                 new SqlParameter("@P_DRID",objHel.DRID),                                
                                 new SqlParameter("@P_COMPLAINT",objHel.COMPLAINT),
                                 new SqlParameter("@P_FINDING",objHel.FINDING),                                
                                 new SqlParameter("@P_WEIGHT",objHel.WEIGHT),
                                 new SqlParameter("@P_BP",objHel.BP),
                                 new SqlParameter("@P_TEMP",objHel.TEMP),
                                 new SqlParameter("@P_PULSE",objHel.PULSE),
                                 new SqlParameter("@P_RESP",objHel.RESP),
                                 new SqlParameter("@P_IP_ADDRESS",objHel.IP_ADDRESS),
                                 new SqlParameter("@P_MAC_ADDRESS",objHel.MAC_ADDRESS),
                                 new SqlParameter("@P_COLLEGE_CODE",objHel.COLLEGE_CODE),
                                 new SqlParameter("@P_DEPENDENTID",objHel.DEPENDENTID),
                                 new SqlParameter("@P_PATIENT_CODE",objHel.PATIENT_CODE),
                                 new SqlParameter("@P_PATIENT_NAME",objHel.PATIENT_NAME),
                                 new SqlParameter("@P_REFERENCE_BY",objHel.REFERENCE_BY),
                                 new SqlParameter("@P_AGE",objHel.AGE),
                                 new SqlParameter("@P_SEX",objHel.SEX),                                 
                                 new SqlParameter("@P_OPDID",OPDID),
                            };
                            objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;

                            if (objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_PRESCRIPTION_BASED_OPD_INSERT", objParams, false) != null)
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        catch (Exception ex)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.HealthTransactionController.AddHelPatientDetails()-> " + ex.ToString());
                        }
                        return retStatus;
                    }

                    /// <summary>
                    /// This method is used to insert Doctor details in the Hel_DoctorMaster Table
                    /// <summary>
                    ///   
                    public int AddHelPrescriptionBasedMedicine(HealthTransactions objHel)
                    {
                        int retStatus = Convert.ToInt32(CustomStatus.Others);
                        int PRESCNO = 0;
                        try
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                            SqlParameter[] objParams = new SqlParameter[]
                            {                                  
                                 new SqlParameter("@P_OPDNO",objHel.OPDNO),
                                 new SqlParameter("@P_MEDICINE_PRES",objHel.MEDICINE_PRES),
                                 new SqlParameter("@P_PID",objHel.PID),                                 
                                 new SqlParameter("@P_IP_ADDRESS",objHel.IP_ADDRESS),
                                 new SqlParameter("@P_MAC_ADDRESS",objHel.MAC_ADDRESS),
                                 new SqlParameter("@P_COLLEGE_CODE",objHel.COLLEGE_CODE),
                                 new SqlParameter("@P_PRESCNO",PRESCNO),
                            };
                            objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;

                            if (objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_PRECREPTION_BASED_INSERT", objParams, false) != null)
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        catch (Exception ex)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.HealthTransactionController.AddHelPrescriptionInsert()-> " + ex.ToString());
                        }
                        return retStatus;
                    }
               
              }

            }
        }
    }

