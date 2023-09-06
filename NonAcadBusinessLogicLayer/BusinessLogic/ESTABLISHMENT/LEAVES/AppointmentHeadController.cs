//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : LEAVE MGT.                               
// CREATION DATE : 24-08-2015                                                        
// CREATED BY    : SWATI GHATE
// MODIFIED DATE :
// MODIFIED DESC :
//====================================================================================== 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class AppointmentHeadController
            {
                /// <SUMMARY>
                /// ConnectionStrings
                /// </SUMMARY>
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                #region AppointmentHeadEntry
                /// <summary>
                /// Created By: Swati Ghate
                /// Created Date: 02-009-2015
                /// Reason: TO display Notification for Duration End for Dean & heads
                /// </summary>
                /// <returns></returns>
                public DataSet GetNotificationForDuration()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_APPOINTMENT_DURATION_END_FOR_HEAD_DEAN", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SchoolController.AppointmentHead-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }

                    return ds;
                }
                
                /// <summary>
                /// Purpose To update Active_status field for appointment head & dean IF appointment is expire
                /// </summary>
                /// <param name="app_no"></param>
                /// <returns></returns>
                public int UpdateStatusForAppointmentHeadDean(int app_no)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_APP_NO", app_no);                      
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_SCHOOL_APPOINTMENT_HEAD_STATUS_UPD", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AppointmentHeadController.UpdateStatusForAppointmentHeadDean->" + ex.ToString());
                    }
                    return retstatus;
                }
                // To insert New Appointment HEad & dean Entry

                public int AddUpdateAppointmentHead(SchoolMaster objSchool,int app_no)
                {
                    int pkid = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_APP_NO", app_no);                      
                        objParams[1] = new SqlParameter("@P_SCHOOLNO", objSchool.SCHOOL_NO);
                        objParams[2] = new SqlParameter("@P_DEPTNO", objSchool.DEPT_NO);                      
                        objParams[3] = new SqlParameter("@P_HOD_NO", objSchool.HOD_NO);
                        objParams[4] = new SqlParameter("@P_DEAN_NO", objSchool.DEAN_NO); 
                        objParams[5] = new SqlParameter("@P_STATUS", objSchool.STATUS);
                        objParams[6] = new SqlParameter("@P_APP_DATE", objSchool.APP_DATE);
                        objParams[7] = new SqlParameter("@P_DURATION_DATE", objSchool.DURATION_DATE);                       
                        objParams[8] = new SqlParameter("@P_DATE", objSchool.DATE);
                        objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objSchool.COLLEGE_CODE);
                        objParams[10] = new SqlParameter("@P_APP_BY", objSchool.APPOINT_BY_NO);
                        objParams[11] = new SqlParameter("@P_REMARK", objSchool.REMARK);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_SCHOOL_APPOINTMENT_HEAD_INS_UPD", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                            {
                                pkid = -99;
                            }
                            else
                                pkid = Convert.ToInt32(ret.ToString());
                        }
                        else
                        {
                            pkid = -99;

                        }

                    }
                    catch (Exception ee)
                    {
                        pkid = -99;
                    }
                    return pkid;
                }
            
                // To Delete AppointmentHead
                public int DeleteAppointmentHead(int APP_NO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_APP_NO", APP_NO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_SCHOOL_APPOINTMENT_HEAD_DELETE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SchoolController.DeleteAppointmentHead-> " + ex.ToString());
                    }
                    return retstatus;
                }

                // To Fetch all AppointmentHead
                public DataSet GetAllAppointmentHead()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_SCHOOL_APPOINTMENT_HEAD_GETALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SchoolController.AppointmentHead-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }

                    return ds;
                }

                // To Fetch single AppointmentHead detail by passing Appointment No
                public DataSet GetSingleAppointmentHead(int APP_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_APP_NO", APP_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_SCHOOL_APPOINTMENT_HEAD_GETSINGLE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SchoolController.GetSingleAppointmentHead->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion // End AppointmentHead Enrty Region
            }
        }
    }
}
