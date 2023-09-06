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
        namespace BusinessLogicLayer.BusinessLogic //.Health
        {
            public class HelMasterController
            {


                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                #region Doctor Master

                /// <summary>
                /// This method is used to insert Doctor details in the Hel_DoctorMaster Table
                /// <summary>
                ///   
                public int AddHelDoctor(Health objHel)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    int DRID = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[14];
                                                            
                                 objParams[0] = new SqlParameter("@P_DRNAME",objHel.DRNAME);
                                 objParams[1] = new SqlParameter("@P_DEGREE",objHel.DEGREE);
                                 objParams[2] = new SqlParameter("@P_DESIG",objHel.DESIG);
                                 objParams[3] = new SqlParameter("@P_ADDRESS",objHel.ADDRESS);
                                 objParams[4] = new SqlParameter("@P_PHONE",objHel.PHONE);
                                 objParams[5] = new SqlParameter("@P_HOSPITALNAME",objHel.HOSPITALNAME);
                                 objParams[6] = new SqlParameter("@P_HADDRESS",objHel.HADDRESS);
                                 objParams[7] = new SqlParameter("@P_HPHONE",objHel.HPHONE);
                                 objParams[8] = new SqlParameter("@P_STATUS",objHel.STATUS);
                                 objParams[9] = new SqlParameter("@P_IP_ADDRESS",objHel.IP_ADDRESS);
                                 objParams[10] = new SqlParameter("@P_MAC_ADDRESS",objHel.MAC_ADDRESS);
                                 objParams[11] = new SqlParameter("@P_COLLEGE_CODE",objHel.COLLEGE_CODE);
                                 objParams[12] = new SqlParameter("@P_EMP_IDNO",objHel.EMP_IDNO);
                                 objParams[13] = new SqlParameter("@P_DRID", SqlDbType.Int);
                                 objParams[13].Direction = ParameterDirection.Output;
                           
                        //objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_DOCTOR_INSERT", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                        int ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_DOCTOR_INSERT", objParams, true));
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = -99;
                        else
                            retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.HelMasterController.AddHelDoctor()-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// This method is used to Modify details of DOCTORS in HEL_DOCTORMASTER table.
                /// </summary>
                /// 
                public int UpdateHelDoctors(Health objHel)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            {    
                                 new SqlParameter("@P_DRID",objHel.DRID),
                                 new SqlParameter("@P_DRNAME",objHel.DRNAME),
                                 new SqlParameter("@P_DEGREE",objHel.DEGREE),
                                 new SqlParameter("@P_DESIG",objHel.DESIG),
                                 new SqlParameter("@P_ADDRESS",objHel.ADDRESS),
                                 new SqlParameter("@P_PHONE",objHel.PHONE),
                                 new SqlParameter("@P_HOSPITALNAME",objHel.HOSPITALNAME),
                                 new SqlParameter("@P_HADDRESS",objHel.HADDRESS),
                                 new SqlParameter("@P_HPHONE",objHel.HPHONE),
                                 new SqlParameter("@P_STATUS",objHel.STATUS),
                                 new SqlParameter("@P_IP_ADDRESS",objHel.IP_ADDRESS),
                                 new SqlParameter("@P_MAC_ADDRESS",objHel.MAC_ADDRESS),
                                 new SqlParameter("@P_COLLEGE_CODE",objHel.COLLEGE_CODE), 
                                 new SqlParameter("@P_EMP_IDNO",objHel.EMP_IDNO),                             
                            };
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_DOCTOR_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.HelMasterController.UpdateHelDoctors()-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// This method is used to Get Record(s) from HEL_DOCTORMASTER table based on DRID.
                /// </summary>
                public DataSet GetDoctorByDRID(int DRID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            { 
                              new SqlParameter("@P_DRID",DRID)  
                            };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_DOCTOR_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.HelMasterController.GetDoctorByDRID() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                #endregion

                #region Item Master

                /// <summary>
                /// This method is used to insert Doctor details in the hel_item Table
                /// <summary>
                ///   
                public int AddHelItem(Health objHel)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    int INO = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            {                                  
                                 new SqlParameter("@P_INAME",objHel.INAME),
                                 new SqlParameter("@P_UNIT",objHel.UNIT),
                                 new SqlParameter("@P_IDETAILS",objHel.IDETAILS),
                                 new SqlParameter("@P_SGID",objHel.SGID),
                                 new SqlParameter("@P_REORDERQTY",objHel.REORDERQTY),
                                 new SqlParameter("@P_OBQTY",objHel.OBQTY),
                                 //new SqlParameter("@P_OBVALUE",objHel.OBVALUE),
                                 new SqlParameter("@P_PRICE",objHel.PRICE),
                                 new SqlParameter("@P_REFERRED",objHel.REFERRED),
                                 new SqlParameter("@P_MEDTYPENO",objHel.MEDTYPENO),
                                 new SqlParameter("@P_IP_ADDRESS",objHel.IP_ADDRESS),
                                 new SqlParameter("@P_MAC_ADDRESS",objHel.MAC_ADDRESS),
                                 new SqlParameter("@P_COLLEGE_CODE",objHel.COLLEGE_CODE),
                                 new SqlParameter("@P_INO",INO),
                            };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_ITEM_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.HelMasterController.AddHelItem()-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// This method is used to Modify details of ITEM in HEL_ITEM table.
                /// </summary>
                /// 
                public int UpdateHelItem(Health objHel)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            {    
                                 new SqlParameter("@P_INO",objHel.INO),
                                 new SqlParameter("@P_INAME",objHel.INAME),
                                 new SqlParameter("@P_UNIT",objHel.UNIT),
                                 new SqlParameter("@P_IDETAILS",objHel.IDETAILS),
                                 new SqlParameter("@P_SGID",objHel.SGID),
                                 new SqlParameter("@P_REORDERQTY",objHel.REORDERQTY),
                                 new SqlParameter("@P_OBQTY",objHel.OBQTY),
                                 //new SqlParameter("@P_OBVALUE",objHel.OBVALUE),
                                 new SqlParameter("@P_PRICE",objHel.PRICE),
                                 new SqlParameter("@P_REFERRED",objHel.REFERRED),
                                 new SqlParameter("@P_MEDTYPENO",objHel.MEDTYPENO),
                                 new SqlParameter("@P_IP_ADDRESS",objHel.IP_ADDRESS),
                                 new SqlParameter("@P_MAC_ADDRESS",objHel.MAC_ADDRESS),
                                 new SqlParameter("@P_COLLEGE_CODE",objHel.COLLEGE_CODE),
                            };
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HEL_ITEM_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.HelMasterController.UpdateHelItem()-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// This method is used to Get Record(s) from HEL_ITEM table based on INO.
                /// </summary>
                public DataSet GetItemByINO(int INO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            { 
                              new SqlParameter("@P_INO",INO)  
                            };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_ITEM_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.HelMasterController.GetItemByINO() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                #endregion

                #region Item SubGroup

                /// <summary>
                /// This method is used to insert Doctor details in the Hel_DoctorMaster Table
                /// <summary>
                ///   
                public int AddHelItemSubGroup(Health objHel)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    int SGID = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            {                                  
                                 new SqlParameter("@P_SGNAME",objHel.SGNAME),
                                 new SqlParameter("@P_MGID",objHel.MGID),
                                 new SqlParameter("@P_IP_ADDRESS",objHel.IP_ADDRESS),
                                 new SqlParameter("@P_MAC_ADDRESS",objHel.MAC_ADDRESS),
                                 new SqlParameter("@P_COLLEGE_CODE",objHel.COLLEGE_CODE),
                                 new SqlParameter("@P_SGID",SGID),
                            };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_SUBGROUP_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.HelMasterController.AddHelItemSubGroup()-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// This method is used to Modify details of DOCTORS in HEL_DOCTORMASTER table.
                /// </summary>
                /// 
                public int UpdateHelItemSubGroup(Health objHel)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            {    
                                 new SqlParameter("@P_SGID",objHel.SGID),
                                 new SqlParameter("@P_SGNAME",objHel.SGNAME),
                                 new SqlParameter("@P_MGID",objHel.MGID),
                                 new SqlParameter("@P_IP_ADDRESS",objHel.IP_ADDRESS),
                                 new SqlParameter("@P_MAC_ADDRESS",objHel.MAC_ADDRESS),
                                 new SqlParameter("@P_COLLEGE_CODE",objHel.COLLEGE_CODE),                                                                                               
                            };
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_SUBGROUP_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.HelMasterController.UpdateHelItemSubGroup()-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// This method is used to Get Record(s) from HEL_DOCTORMASTER table based on DRID.
                /// </summary>
                public DataSet GetItemSubGroupBySGID(int SGID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            { 
                              new SqlParameter("@P_SGID",SGID)  
                            };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_SUBGROUP_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.HelMasterController.GetItemSubGroupBySGID() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                #endregion

                #region Patient Details

                /// <summary>
                /// This method is used to insert Doctor details in the Hel_DoctorMaster Table
                /// <summary>
                ///   
                public int AddHelPatientDetails(Health objHel)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    int PID = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            {                                  
                                 new SqlParameter("@P_RPID",objHel.RPID),
                                 new SqlParameter("@P_RELATION",objHel.RELATION),
                                 new SqlParameter("@P_CARDNO",objHel.CARDNO),
                                 new SqlParameter("@P_TYPENO",objHel.TYPENO),
                                 new SqlParameter("@P_REGNO",objHel.REGNO),
                                 new SqlParameter("@P_NAME",objHel.NAME),
                                 new SqlParameter("@P_SEX",objHel.SEX),
                                 new SqlParameter("@P_DESIGNATION",objHel.DESIGNATION),
                                 new SqlParameter("@P_DEPTNO",objHel.DEPTNO),
                                 new SqlParameter("@P_DOB",objHel.DOB),
                                 new SqlParameter("@P_MARRIED",objHel.MARRIED),
                                 new SqlParameter("@P_ADDRESS",objHel.ADDRESS),
                                 new SqlParameter("@P_CITY",objHel.CITY),
                                 new SqlParameter("@P_STATE",objHel.STATE),
                                 new SqlParameter("@P_HOSTEL",objHel.HOSTEL),
                                 new SqlParameter("@P_HBLOCK",objHel.HBLOCK),
                                 new SqlParameter("@P_HROOM",objHel.HROOM),
                                 new SqlParameter("@P_PHONE",objHel.PHONE),
                                 new SqlParameter("@P_EMAIL",objHel.EMAIL),
                                 new SqlParameter("@P_BLOODGRNO",objHel.BLOODGRNO),
                                 new SqlParameter("@P_HEIGHT",objHel.HEIGHT),
                                 new SqlParameter("@P_WEIGHT",objHel.WEIGHT),
                                 new SqlParameter("@P_HABITS",objHel.HABITS),
                                 new SqlParameter("@P_FAMILYHIS",objHel.FAMILYHIS),
                                 new SqlParameter("@P_AGE",objHel.AGE),
                                 new SqlParameter("@P_AGEAPRX",objHel.AGEAPRX),
                                 new SqlParameter("@P_IDNO",objHel.IDNO),
                                 new SqlParameter("@P_IP_ADDRESS",objHel.IP_ADDRESS),
                                 new SqlParameter("@P_MAC_ADDRESS",objHel.MAC_ADDRESS),
                                 new SqlParameter("@P_COLLEGE_CODE",objHel.COLLEGE_CODE),
                                 new SqlParameter("@P_PID",PID),
                            };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_PATIENT_MASTER_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.HelMasterController.AddHelPatientDetails()-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// This method is used to Modify details of DOCTORS in HEL_DOCTORMASTER table.
                /// </summary>
                /// 
                public int UpdateHelPatientDetails(Health objHel)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            {    
                                 new SqlParameter("@P_PID",objHel.PID),
                                 new SqlParameter("@P_RPID",objHel.RPID),
                                 new SqlParameter("@P_RELATION",objHel.RELATION),
                                 new SqlParameter("@P_CARDNO",objHel.CARDNO),
                                 new SqlParameter("@P_TYPENO",objHel.TYPENO),
                                 new SqlParameter("@P_REGNO",objHel.REGNO),
                                 new SqlParameter("@P_NAME",objHel.NAME),
                                 new SqlParameter("@P_SEX",objHel.SEX),
                                 new SqlParameter("@P_DESIGNATION",objHel.DESIGNATION),
                                 new SqlParameter("@P_DEPTNO",objHel.DEPTNO),
                                 new SqlParameter("@P_DOB",objHel.DOB),
                                 new SqlParameter("@P_MARRIED",objHel.MARRIED),
                                 new SqlParameter("@P_ADDRESS",objHel.ADDRESS),
                                 new SqlParameter("@P_CITY",objHel.CITY),
                                 new SqlParameter("@P_STATE",objHel.STATE),
                                 new SqlParameter("@P_HOSTEL",objHel.HOSTEL),
                                 new SqlParameter("@P_HBLOCK",objHel.HBLOCK),
                                 new SqlParameter("@P_HROOM",objHel.HROOM),
                                 new SqlParameter("@P_PHONE",objHel.PHONE),
                                 new SqlParameter("@P_EMAIL",objHel.EMAIL),
                                 new SqlParameter("@P_BLOODGRNO",objHel.BLOODGRNO),
                                 new SqlParameter("@P_HEIGHT",objHel.HEIGHT),
                                 new SqlParameter("@P_WEIGHT",objHel.WEIGHT),
                                 new SqlParameter("@P_HABITS",objHel.HABITS),
                                 new SqlParameter("@P_FAMILYHIS",objHel.FAMILYHIS),
                                 new SqlParameter("@P_AGE",objHel.AGE),
                                 new SqlParameter("@P_AGEAPRX",objHel.AGEAPRX),
                                 new SqlParameter("@P_IDNO",objHel.IDNO),
                                 new SqlParameter("@P_IP_ADDRESS",objHel.IP_ADDRESS),
                                 new SqlParameter("@P_MAC_ADDRESS",objHel.MAC_ADDRESS),
                                 new SqlParameter("@P_COLLEGE_CODE",objHel.COLLEGE_CODE),                                                               
                            };
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_PATIENT_MASTER_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.HelMasterController.UpdateHelPatientDetails()-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// This method is used to Get Record(s) from HEL_PATIENTMASTER table based on PID.  
                /// </summary>
                public DataSet GetPatientDetailsByPID(int PID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            { 
                              new SqlParameter("@P_PID",PID)  
                            };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_PATIENT_MASTER_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.HelMasterController.GetPatientDetailsByPID() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetStudentInfo(int PID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            { 
                              new SqlParameter("@P_PID",PID)  
                            };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_GET_STUDENT_INFO_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.HelMasterController.GetPatientDetailsByPID() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetOtherPatientInfo(int PID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            { 
                              new SqlParameter("@P_PID",PID)  
                            };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_GET_OTHER_PATIENT_INFO_BY_NAME", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.HelMasterController.GetOtherPatientInfo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }


                #endregion

                /// <summary>
                /// This method is used to insert Doctor details in the Hel_DoctorMaster Table
                /// <summary>
                ///   
                public int AddCertificateIssue(Health objHel, HealthTransactions objHelTran)
                {
                    int retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_CI_ID", objHel.CI_ID);
                        objParams[1] = new SqlParameter("@P_PID", objHel.PID);
                        objParams[2] = new SqlParameter("@P_CERTI_NAME_ID", objHel.CERTI_NAME_ID);
                        objParams[3] = new SqlParameter("@P_REMARK", objHel.REMARK);
                        objParams[4] = new SqlParameter("@P_P_CODE", objHel.P_CODE);
                        objParams[5] = new SqlParameter("@P_OPDID", objHelTran.OPDID);
                        objParams[6] = new SqlParameter("@P_ISSUE_DATE", objHel.AUDIT_DATE);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_CERTIFICATE_ISSUE_INSERT_UPDATE", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == 2627)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.HelMasterController.AddCertificateIssue()-> " + ex.ToString());
                    }
                    return retstatus;
                }

                /// <summary>
                /// This method is used to insert Certificate details in the Hel_CertificateMaster Table
                /// <summary>
                ///  
                public int AddGenerateCertificate(Health objHel, HealthTransactions objHelTran)
                {
                    int retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[17];
                        objParams[0] = new SqlParameter("@P_CER_ID", objHel.CER_ID);
                        objParams[1] = new SqlParameter("@P_CER_NO", objHel.CER_NO);
                        objParams[2] = new SqlParameter("@P_SignGovt", objHel.SignGovt);
                        objParams[3] = new SqlParameter("@P_PatientName", objHel.PatientName);
                        objParams[4] = new SqlParameter("@P_DrName", objHel.DrName);
                        objParams[5] = new SqlParameter("@P_SufferingFrom", objHel.SufferingFrom);
                        objParams[6] = new SqlParameter("@P_AbsenceDays", objHel.AbsenceDays);

                       // objParams[7] = new SqlParameter("@P_FromDate", iff(objHel.FromDate);

                        if (objHel.FromDate == DateTime.MinValue)
                        {
                            objParams[7] = new SqlParameter("@P_FromDate", DBNull.Value);
                        }
                        else
                        {
                            objParams[7] = new SqlParameter("@P_FromDate", objHel.FromDate);                       
                        }

                        objParams[8] = new SqlParameter("@P_IssuedDate", objHel.IssuedDate);
                        objParams[9] = new SqlParameter("@P_AuthorizedMed", objHel.AuthorizedMed);
                        objParams[10] = new SqlParameter("@P_Department", objHel.Department);
                        objParams[11] = new SqlParameter("@P_PostOf", objHel.PostOf);
                        objParams[12] = new SqlParameter("@P_Age", objHel.Age);
                        objParams[13] = new SqlParameter("@P_AgeAppreance", objHel.AgeAppreance);
                        objParams[14] = new SqlParameter("@P_ReferTo", objHel.ReferTo);
                        objParams[15] = new SqlParameter("@P_Expenditure", objHel.Expenditure);
                        objParams[16] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[16].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_GENERATE_CERTIFICATE_INSERT_UPDATE", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == 2627)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.HelMasterController.AddCertificateIssue()-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public int AddUpdateBloodGp(Health objHel)
                {
                    int retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_BLOODGRNO", objHel.BLOODGRNO);
                        objParams[1] = new SqlParameter("@P_BLOODGP_NAME", objHel.BLOODGP_NAME);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", objHel.COLLEGE_CODE);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_BLOOD_GROUP_INSERT_UPDATE", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == 2627)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.HelMasterController.AddCertificateIssue()-> " + ex.ToString());
                    }
                    return retstatus;
                }





                public DataSet GetDataHealthCertificate(int Cer_No)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                        SqlParameter[] objParams = new SqlParameter[]
                            { 
                              new SqlParameter("@P_CERNO",Cer_No)  
                            };
                        ds = objSQLHelper.ExecuteDataSetSP("[dbo].[PKG_HEALTH_GET_ALL_CERTIFICATE_DATA]", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LabController.GetContentByTitle ->" + ex.ToString());
                    }
                    return ds;
                }



                public DataSet GetDataHealthCertificateById(int Cer_Id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                        SqlParameter[] objParams = new SqlParameter[]
                            { 
                              new SqlParameter("@P_CERID",Cer_Id)  
                            };
                        ds = objSQLHelper.ExecuteDataSetSP("[dbo].[UspDataGenCertificateById]", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LabController.GetContentByTitle ->" + ex.ToString());
                    }
                    return ds;
                }





                //public DataSet GetOtherPatientInfo(int PID)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = new SqlParameter[]
                //            { 
                //              new SqlParameter("@P_PID",PID)  
                //            };
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_GET_OTHER_PATIENT_INFO_BY_NAME", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.HelMasterController.GetOtherPatientInfo() --> " + ex.Message + " " + ex.StackTrace);
                //    }
                //    return ds;
                //}

                #region Dosage Master

                public int AddHelDosage(Health objHel)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    int DNO = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];

                        objParams[0] = new SqlParameter("@P_DNAME", objHel.DOSAGENAME);
                        objParams[1] = new SqlParameter("@P_DQTY", objHel.DOSAGEQUANTITY);
                        objParams[2] = new SqlParameter("@P_STATUS", objHel.STATUS);
                        objParams[3] = new SqlParameter("@P_IP_ADDRESS", objHel.IP_ADDRESS);
                        objParams[4] = new SqlParameter("@P_MAC_ADDRESS", objHel.MAC_ADDRESS);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objHel.COLLEGE_CODE);
                        objParams[6] = new SqlParameter("@P_DNO", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_DOSAGEMASTER_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.HelMasterController.AddHelDoctor()-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateHelDosage(Health objHel)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];

                        objParams[0] = new SqlParameter("@P_DNAME", objHel.DOSAGENAME);
                        objParams[1] = new SqlParameter("@P_DQTY", objHel.DOSAGEQUANTITY);
                        objParams[2] = new SqlParameter("@P_STATUS", objHel.STATUS);
                        objParams[3] = new SqlParameter("@P_IP_ADDRESS", objHel.IP_ADDRESS);
                        objParams[4] = new SqlParameter("@P_MAC_ADDRESS", objHel.MAC_ADDRESS);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objHel.COLLEGE_CODE);
                        objParams[6] = new SqlParameter("@P_DNO", objHel.DNO);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_DOSAGEMASTER_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.HelMasterController.AddHelDoctor()-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetDosageMasterDetails()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_GET_ALL_DOSAGEMASTER", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.HelMasterController.GetDosageMasterDetails() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }


                public DataSet GetDosageMasterByNo(int DNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DNO", DNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_DOSAGEMASTER_GET_BY_NO", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.HelMasterController.GetDosageMasterByNo->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion




            }
        }
    }
}
