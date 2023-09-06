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
            public class LabController
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public int AddUpdateTestTitle(LabMaster obj)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_TITLENO", obj.TITLENO);
                        objParams[1] = new SqlParameter("@P_TITLE", obj.TITLE);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", obj.COLLEGE_CODE); 
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_LAB_TEST_TITLE_INSERT_UPDATE", objParams, true);
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
                        throw new IITMSException("IITMS.HEALTH.BusinessLayer.BusinessLogic.LabController.AddUpdateTestTitle->" + ex.ToString());
                    }
                    return retstatus;
                }

                public int AddUpdateTestContent(LabMaster obj)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];                      
                        objParams[0] = new SqlParameter("@P_TITLENO", obj.TITLENO);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", obj.COLLEGE_CODE);
                        objParams[2] = new SqlParameter("@P_HEALTH_TEST_CONTENTTBL", obj.TESTCONTENT);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_LAB_TEST_CONTENT_INSERT_UPDATE", objParams, true);
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
                        throw new IITMSException("IITMS.HEALTH.BusinessLayer.BusinessLogic.LabController.AddUpdateTestContent->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetContentByTitle(int TITLENO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TITLENO", TITLENO);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_LAB_GET_CONTENT_BY_TITLENO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LabController.GetContentByTitle ->" + ex.ToString());
                    }
                    return ds;
                }

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
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_LAB_GET_PATIENT_BY_IDNO", objParams);                      
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.LabController.GetPatientDetailsByPID() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetTestContentToObserve(int TITLENO, int OPDID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_TITLENO", TITLENO);
                        objParams[1] = new SqlParameter("@P_OPDID", OPDID);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_LAB_GET_CONTENT_FOR_OBSERVATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LabController.GetContentByTitle ->" + ex.ToString());
                    }
                    return ds;
                }

                public int AddUpdateObservation(LabMaster obj)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_OBSERNO", obj.OBSERNO);
                        objParams[1] = new SqlParameter("@P_PATIENT_ID", obj.PATIENT_ID);
                        objParams[2] = new SqlParameter("@P_OPDID", obj.OPDID);
                        objParams[3] = new SqlParameter("@P_TITLENO", obj.TITLENO);
                        objParams[4] = new SqlParameter("@P_TEST_SAMPLE_DT", obj.TEST_SAMPLE_DT);
                        objParams[5] = new SqlParameter("@P_TEST_DUE_DT", obj.TEST_DUE_DT);
                        objParams[6] = new SqlParameter("@P_COMMON_REMARK", obj.COMMON_REMARK);                       
                        objParams[7] = new SqlParameter("@P_COLLEGE_CODE", obj.COLLEGE_CODE);
                        objParams[8] = new SqlParameter("@P_OBSERVATION_TRAN_TBL", obj.OBSERVATION_TRAN);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_HEALTH_LAB_OBSERVATION_INSERT_UPDATE", objParams, true);
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
                        throw new IITMSException("IITMS.HEALTH.BusinessLayer.BusinessLogic.LabController.AddUpdateTestContent->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetTestObservation(int OBSERNO)
                   {
                        DataSet ds = null;
                        try
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                            SqlParameter[] objParams = new SqlParameter[1];
                            objParams[0] = new SqlParameter("@P_OBSERNO", OBSERNO);

                            ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_LAB_GET_CONTENT_BY_OBSERNO", objParams);
                        }
                        catch (Exception ex)
                        {
                            return ds;
                            throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LabController.GetTestObservation ->" + ex.ToString());
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
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_LAB_GET_STUDENT_PATIENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.LabController.GetPatientDetailsByPID() --> " + ex.Message + " " + ex.StackTrace);
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
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_HEALTH_LAB_GET_OTHER_PATIENT_INFO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.LabController.GetOtherPatientInfo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
            }
        }
    }
}
