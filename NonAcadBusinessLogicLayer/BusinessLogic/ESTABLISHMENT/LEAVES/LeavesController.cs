
//======================================================================================
// PROJECT NAME  : NITPRM                                                                
// MODULE NAME   : BUSINESS LOGIC FILE //[LEAVE CONTROLLER]                                  
// CREATION DATE : 07-OCT-2009                                                        
// CREATED BY    : JAYANT DHOMNE                                   
// MODIFIED DATE :
// MODIFIED DESC :
//====================================================================================== 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
//using IITMS;
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
            public class LeavesController
            {
                /// <SUMMARY>
                /// ConnectionStrings
                /// </SUMMARY>
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                #region COmp-off
                //COMP OFF ADD
                public DataSet GetCompOffValidate(Leaves objLM)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", objLM.EMPNO);
                        objParams[1] = new SqlParameter("@P_WORKDATE", objLM.DATE);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_COMPOFF_NO_OF_DAYS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetCompOffValidate->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                //public int AddCompOff(int Idno, DateTime Working_Date, DateTime NextMonth, string Reason, DateTime App_Date, string InTime, string OutTime, int Hour, char Status)
                //{

                //    int retstatus = 0;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objparams = null;
                //        objparams = new SqlParameter[9];
                //        objparams[0] = new SqlParameter("@P_IDNO", Idno);
                //        if (!Working_Date.Equals(DateTime.MinValue))
                //            objparams[1] = new SqlParameter("@P_WORKING_DATE", Working_Date);
                //        else
                //            objparams[1] = new SqlParameter("@P_WORKING_DATE", DBNull.Value);

                //        if (!Working_Date.Equals(DateTime.MinValue))
                //            objparams[2] = new SqlParameter("@P_EXPIRY_DATE", NextMonth);
                //        else
                //            objparams[2] = new SqlParameter("@P_EXPIRY_DATE", DBNull.Value);

                //        objparams[3] = new SqlParameter("@P_REASON", Reason);
                //        if (!App_Date.Equals(DateTime.MinValue))
                //            objparams[4] = new SqlParameter("@P_APPLY_DATE", App_Date);
                //        else
                //            objparams[4] = new SqlParameter("@P_APPLY_DATE", DBNull.Value);

                //        objparams[5] = new SqlParameter("@P_INTIME", InTime);
                //        objparams[6] = new SqlParameter("@P_OUTTIME", OutTime);
                //        objparams[7] = new SqlParameter("@P_WORKING_HOUR", Hour);
                //        objparams[8] = new SqlParameter("@P_STATUS", Status);
                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_COMP_OFF_LEAVE_INSERT", objparams, false) != null)
                //            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //    }
                //    catch (Exception ex)
                //    {
                //        retstatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddCompOff->" + ex.ToString());
                //    }
                //    return retstatus;
                //}
                public int AddCompOff(int Idno, DateTime Working_Date, DateTime NextMonth, string Reason, DateTime App_Date, string InTime, string OutTime, string Hour, char Status, double no_of_days)
                {

                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[10];
                        objparams[0] = new SqlParameter("@P_IDNO", Idno);
                        if (!Working_Date.Equals(DateTime.MinValue))
                            objparams[1] = new SqlParameter("@P_WORKING_DATE", Working_Date);
                        else
                            objparams[1] = new SqlParameter("@P_WORKING_DATE", DBNull.Value);

                        if (!Working_Date.Equals(DateTime.MinValue))
                            objparams[2] = new SqlParameter("@P_EXPIRY_DATE", NextMonth);
                        else
                            objparams[2] = new SqlParameter("@P_EXPIRY_DATE", DBNull.Value);

                        objparams[3] = new SqlParameter("@P_REASON", Reason);
                        if (!App_Date.Equals(DateTime.MinValue))
                            objparams[4] = new SqlParameter("@P_APPLY_DATE", App_Date);
                        else
                            objparams[4] = new SqlParameter("@P_APPLY_DATE", DBNull.Value);

                        objparams[5] = new SqlParameter("@P_INTIME", InTime);
                        objparams[6] = new SqlParameter("@P_OUTTIME", OutTime);
                        objparams[7] = new SqlParameter("@P_WORKING_HOUR", Hour);
                        objparams[8] = new SqlParameter("@P_STATUS", Status);
                        objparams[9] = new SqlParameter("@P_NO_OF_DAYS", no_of_days);
                        //@P_NO_OF_DAYS
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_COMP_OFF_LEAVE_INSERT", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddCompOff->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet RetrieveAllCompOff(int Idno)
                {
                    DataSet ds = null;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", Idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_COMPOFF_LEAVE_GETALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.RetrieveAllCompOff->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                //------------------------------------

                public DataSet GetPendListforCompOffApproval(int UA_No)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_No);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_COMP_OFF_LEAVE_GET_PENDINGLIST", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetPendListforLeaveApproval->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                //----------------------------------

                public DataSet GetCompOffLeaveDetail(int RNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_RNO", RNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_COMP_OFF_LEAVE_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLeaveApplDetail->" + ex.ToString());

                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                //----------------------------------

                public int UpdateComp_offRequestEntry(int RNO, int UA_NO, string STATUS, string REMARKS, DateTime APRDT, int p)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[6];
                        objparams[0] = new SqlParameter("@P_RNO", RNO);
                        objparams[1] = new SqlParameter("@P_UA_NO", UA_NO);
                        objparams[2] = new SqlParameter("@P_STATUS", STATUS);
                        objparams[3] = new SqlParameter("@P_REMARKS", REMARKS);
                        objparams[4] = new SqlParameter("@P_APRDT", APRDT);
                        objparams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[5].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_COMP_OFF_LEAVE_PASS_ENTRY_UPDATE", objparams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_APP_PASS_ENTRY_UPDATE1", objparams, false) != null)
                        //    retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateLvAplAprStatus->" + ex.ToString());
                    }
                    return retStatus;

                }

                //---------------------------------------

                public DataSet GetCompoffEmployeeList()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];
                        //objParams[0] = new SqlParameter("@P_DEPT", Dept);
                        // objParams[1] = new SqlParameter("@P_STAFFTYPE", StaffType);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_COMP_OFF_LEAVE_GET_BY_EMPLOYEES", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetLeavesSType->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                //----------------------------------

                public int UpdateCompOffLeaves(string rnos, int Year)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_RNO", rnos);
                        objParams[1] = new SqlParameter("@P_YEAR", Year);
                        objParams[2] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_COMP_OFF_LEAVE_EMPLOYEES_STATUS_UPFDATE", objParams, true);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.CreditLeaves->" + ex.ToString());
                    }
                    return retStatus;

                }

                #endregion
                #region LeavePeriod
                public int AddUpdatePeriod(Leaves objLM)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New Title
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_PERIOD", objLM.PERIOD);
                        objParams[1] = new SqlParameter("@P_PERIOD_NAME", objLM.PERIOD_NAME);

                        objParams[2] = new SqlParameter("@P_PERIOD_FROM", objLM.PERIOD_FROM);
                        objParams[3] = new SqlParameter("@P_PERIOD_TO", objLM.PERIOD_TO);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_INS_UPD_LEAVE_PERIOD", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeaveController.AddUpdatePeriod-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion
                #region CL_45_Appointment
                public int UpdateAppointment(int collegeno, DataTable dtAppRecord)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_ESTB_CL45_APPOINT_RECORD", dtAppRecord);
                        objParams[1] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        objParams[2] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_UPDATE_CL_45_APOINTMENT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateAppointment->" + ex.ToString());
                    }
                    return retStatus;

                }
                #endregion
                #region CL45Day
                public DataSet GetEmpListFor45DayCLCredit(Leaves objLeaves)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DEPTNO", objLeaves.DEPTNO);
                        objParams[1] = new SqlParameter("@P_STNO", objLeaves.STNO);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", objLeaves.COLLEGE_NO);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTAB_EMPLOYESS_FOR_45DAYS_CL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetLeavesSType->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int Add_CL45DAYS_LEVES(Leaves objLeaves)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_IDNO", objLeaves.EMPNO);
                        objParams[1] = new SqlParameter("@P_ALLOTDT", objLeaves.APPDT);
                        objParams[2] = new SqlParameter("@P_FROM_DATE", objLeaves.FROMDT);
                        objParams[3] = new SqlParameter("@P_TO_DATE", objLeaves.TODT);
                        objParams[4] = new SqlParameter("@P_LEAVES", objLeaves.NO_DAYS);
                        objParams[5] = new SqlParameter("@P_PERIOD", objLeaves.PERIOD);
                        objParams[6] = new SqlParameter("@P_YEAR", objLeaves.YEAR);
                        objParams[7] = new SqlParameter("@P_LEAVENO", objLeaves.LEAVENO);
                        objParams[8] = new SqlParameter("@P_LNO", objLeaves.LNO);
                        objParams[9] = new SqlParameter("@P_COLLEGE_NO", objLeaves.COLLEGE_NO);
                        objParams[10] = new SqlParameter("@P_SESSION_SRNO", objLeaves.SESSION_SRNO);
                        objParams[11] = new SqlParameter("@P_COLLEGE_CODE", objLeaves.COLLEGE_CODE);


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTAB_LEAVE_CL45DAYS_UPDATE", objParams, true);

                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamNameController.Add_Update_AdmissionDate -> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// Modified by: swati ghate
                /// Description: To check no. of 45 days slot & credit leave with updated alloted date
                /// date: 13-03-2015
                /// </summary>
                /// <param name="objLeaves"></param>
                /// <returns></returns>
                public int UPDATE_CL45DAYS_LEAVES_ONLOAD(Leaves objLeaves)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_ENO", objLeaves.ENO);
                        objParams[1] = new SqlParameter("@P_IDNO", objLeaves.EMPNO);
                        objParams[2] = new SqlParameter("@P_INCREMENT_DAY", objLeaves.NO_DAYS);
                        objParams[3] = new SqlParameter("@P_INCREMENT_LEAVE", objLeaves.LEAVE);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_ALLOT_CL_LEAVES_AFTER_45DAYS", objParams, true);
                        // retStatus = Convert.ToInt32(ret);
                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UPDATE_CL45DAYS_LEAVES_ONLOAD -> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion
                #region leave
                // To insert New Leave Type
                public int AddLeave(Leaves objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[40];
                        objParams[0] = new SqlParameter("@P_STNO", objLeave.STNO);
                        objParams[1] = new SqlParameter("@P_LEAVE", objLeave.LEAVE);
                        objParams[2] = new SqlParameter("@P_LEAVENAME", objLeave.LEAVENAME);
                        objParams[3] = new SqlParameter("@P_MAX", objLeave.MAX);
                        objParams[4] = new SqlParameter("@P_CARRY", objLeave.CARRY);
                        objParams[5] = new SqlParameter("@P_PERIOD", objLeave.PERIOD);
                        objParams[6] = new SqlParameter("@P_SEX", objLeave.SEX);
                        objParams[7] = new SqlParameter("@P_CAL", objLeave.CAL);
                        objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objLeave.COLLEGE_CODE);
                        objParams[9] = new SqlParameter("@P_ALLOWEDDAYS", objLeave.DAYS);
                        objParams[10] = new SqlParameter("@P_CALHOLIDAY", objLeave.CALHOLIDAY);
                        objParams[11] = new SqlParameter("@P_LEAVENO", objLeave.LEAVENO);
                        objParams[12] = new SqlParameter("@P_MAXDAYS", objLeave.MAXDAYS);
                        objParams[13] = new SqlParameter("@P_NO_DAYS", objLeave.NO_DAYS);
                        objParams[14] = new SqlParameter("@P_SERVICE_LIMIT", objLeave.SERVICE_LIMIT);
                        objParams[15] = new SqlParameter("@P_SERVICE_COMPLETE_LIMIT", objLeave.SERVICE_COMPLETE_LIMIT);
                        objParams[16] = new SqlParameter("@P_IS_ALLOW_BEFORE_APPLICATION", objLeave.IsAllowBeforeApplication);
                        objParams[17] = new SqlParameter("@P_LEAVETIME", objLeave.LeaveTime);
                        objParams[18] = new SqlParameter("@P_MAXPREDATED", objLeave.MAXPREDATED);
                        objParams[19] = new SqlParameter("@P_MAXPOSTDATED", objLeave.MAXPOSTDATED);
                        objParams[20] = new SqlParameter("@P_PDMAXDAYS", objLeave.PDMAXDAYS);
                        objParams[21] = new SqlParameter("@P_PDLMAXDAYS", objLeave.PDLMAXDAYS);
                        objParams[22] = new SqlParameter("@P_MAXOCCASIONPD", objLeave.MAXOCCASIONPD);
                        objParams[23] = new SqlParameter("@P_MAXOCCASIONPDL", objLeave.MAXOCCASIONPDL);
                        objParams[24] = new SqlParameter("@P_OCCPERIODPD", objLeave.OCCPERIODPD);
                        objParams[25] = new SqlParameter("@P_OCCPERIODPDL", objLeave.OCCPERIODPDL);
                        objParams[26] = new SqlParameter("@P_IsCreditslotwise", objLeave.IsCreditSlotWise);
                        objParams[27] = new SqlParameter("@P_SlotOFDays", objLeave.SlotOFDays);
                        //objParams[28] = new SqlParameter("@P_LeaveCreditAfterSlot", objLeave.LeaveCreditAfterSlot);
                        objParams[28] = new SqlParameter("@P_LeaveCreditAfterSlot", objLeave.LeaveCreditedAfterSlot);

                        if (!objLeave.ALLOTEDDATE.Equals(DateTime.MinValue))
                            objParams[29] = new SqlParameter("@P_ALLOTEDDATE", objLeave.ALLOTEDDATE);
                        else
                            objParams[29] = new SqlParameter("@P_ALLOTEDDATE", DBNull.Value);

                        //objParams[29] = new SqlParameter("@P_ALLOTEDDATE", objLeave.ALLOTEDDATE);
                        objParams[30] = new SqlParameter("@P_CREDITDT", objLeave.CREDITDT);
                        objParams[31] = new SqlParameter("@P_IsClassArrangeRequired", objLeave.IsClassArrangeRequired);
                        objParams[32] = new SqlParameter("@P_IsClassArrangeAcceptanceRequired", objLeave.IsClassArrangeAcceptanceRequired);
                        objParams[33] = new SqlParameter("@P_ISDOJ", objLeave.IsDOJ);
                        objParams[34] = new SqlParameter("@P_MAXDAYSCARRY", objLeave.CARRYDAYS);
                        objParams[35] = new SqlParameter("@P_IsRequiredLoad", objLeave.ISREQUIREDLOAD);
                        objParams[36] = new SqlParameter("@P_LEAVEVALIDMONTH", objLeave.LEAVEVALIDMONTH);
                        objParams[37] = new SqlParameter("@P_CREATEDBY", objLeave.CREATEDBY);
                        objParams[38] = new SqlParameter("@P_IsLeaveValid", objLeave.IsLeaveValid);
                        objParams[39] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[39].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            //objLeave.LNO = Convert.ToInt32(ret);
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_INSERT", objParams, false) != null)
                        //retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.AddLeave-> " + ex.ToString());
                    }
                    return retstatus;
                }

                // To update Existing Leave Type
                public int UpdateLeave(Leaves objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[41];
                        objParams[0] = new SqlParameter("@P_STNO", objLeave.STNO);
                        objParams[1] = new SqlParameter("@P_LEAVE", objLeave.LEAVE);
                        objParams[2] = new SqlParameter("@P_LEAVENAME", objLeave.LEAVENAME);
                        objParams[3] = new SqlParameter("@P_MAX", objLeave.MAX);
                        objParams[4] = new SqlParameter("@P_CARRY", objLeave.CARRY);
                        objParams[5] = new SqlParameter("@P_PERIOD", objLeave.PERIOD);
                        objParams[6] = new SqlParameter("@P_SEX", objLeave.SEX);
                        objParams[7] = new SqlParameter("@P_CAL", objLeave.CAL);
                        objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objLeave.COLLEGE_CODE);
                        objParams[9] = new SqlParameter("@P_LEAVENO", objLeave.LEAVENO);
                        objParams[10] = new SqlParameter("@P_ALLOWEDDAYS", objLeave.DAYS);
                        objParams[11] = new SqlParameter("@P_CALHOLIDAY", objLeave.CALHOLIDAY);
                        objParams[12] = new SqlParameter("@P_LNO", objLeave.LNO);
                        objParams[13] = new SqlParameter("@P_MAXDAYS", objLeave.MAXDAYS);
                        objParams[14] = new SqlParameter("@P_NO_DAYS", objLeave.NO_DAYS);
                        objParams[15] = new SqlParameter("@P_SERVICE_LIMIT", objLeave.SERVICE_LIMIT);
                        objParams[16] = new SqlParameter("@P_SERVICE_COMPLETE_LIMIT", objLeave.SERVICE_COMPLETE_LIMIT);
                        objParams[17] = new SqlParameter("@P_IS_ALLOW_BEFORE_APPLICATION", objLeave.IsAllowBeforeApplication);
                        objParams[18] = new SqlParameter("@P_LEAVETIME", objLeave.LeaveTime);
                        objParams[19] = new SqlParameter("@P_MAXPREDATED", objLeave.MAXPREDATED);
                        objParams[20] = new SqlParameter("@P_MAXPOSTDATED", objLeave.MAXPOSTDATED);
                        objParams[21] = new SqlParameter("@P_PDMAXDAYS", objLeave.PDMAXDAYS);
                        objParams[22] = new SqlParameter("@P_PDLMAXDAYS", objLeave.PDLMAXDAYS);
                        objParams[23] = new SqlParameter("@P_MAXOCCASIONPD", objLeave.MAXOCCASIONPD);
                        objParams[24] = new SqlParameter("@P_MAXOCCASIONPDL", objLeave.MAXOCCASIONPDL);
                        objParams[25] = new SqlParameter("@P_OCCPERIODPD", objLeave.OCCPERIODPD);
                        objParams[26] = new SqlParameter("@P_OCCPERIODPDL", objLeave.OCCPERIODPDL);
                        objParams[27] = new SqlParameter("@P_IsCreditslotwise", objLeave.IsCreditSlotWise);
                        objParams[28] = new SqlParameter("@P_SlotOFDays", objLeave.SlotOFDays);
                        //objParams[29] = new SqlParameter("@P_LeaveCreditAfterSlot", objLeave.LeaveCreditAfterSlot);
                        objParams[29] = new SqlParameter("@P_LeaveCreditAfterSlot", objLeave.LeaveCreditedAfterSlot);
                        if (!objLeave.ALLOTEDDATE.Equals(DateTime.MinValue))
                            objParams[30] = new SqlParameter("@P_ALLOTEDDATE", objLeave.ALLOTEDDATE);
                        else
                            objParams[30] = new SqlParameter("@P_ALLOTEDDATE", DBNull.Value);
                        // objParams[30] = new SqlParameter("@P_ALLOTEDDATE", objLeave.ALLOTEDDATE);
                        objParams[31] = new SqlParameter("@P_CREDITDT", objLeave.CREDITDT);
                        objParams[32] = new SqlParameter("@P_IsClassArrangeRequired", objLeave.IsClassArrangeRequired);
                        objParams[33] = new SqlParameter("@P_IsClassArrangeAcceptanceRequired", objLeave.IsClassArrangeAcceptanceRequired);
                        objParams[34] = new SqlParameter("@P_ISDOJ", objLeave.IsDOJ);
                        objParams[35] = new SqlParameter("@P_MAXDAYSCARRY", objLeave.CARRYDAYS);
                        objParams[36] = new SqlParameter("@P_IsRequiredLoad", objLeave.ISREQUIREDLOAD);
                        objParams[37] = new SqlParameter("@P_LEAVEVALIDMONTH", objLeave.LEAVEVALIDMONTH);
                        objParams[38] = new SqlParameter("@P_MODIFIEDBY", objLeave.MODIFIEDBY);
                        objParams[39] = new SqlParameter("@P_IsLeaveValid", objLeave.IsLeaveValid);
                        objParams[40] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[40].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            //objLeave.LNO = Convert.ToInt32(ret);
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                        // if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_UPDATE", objParams, false) != null)
                        //retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.UpdateLeave-> " + ex.ToString());
                    }
                    return retstatus;
                }

                // To Delete Leave Type
                public int DeleteLeave(int LNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_LNO", LNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_DELETE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.DeleteLeave-> " + ex.ToString());
                    }
                    return retstatus;
                }

                // To Fetch all Leave Types 
                public DataSet GetAllLeave()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_GETALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetAllLeave-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }

                    return ds;
                }

                // To Fetch single Leave Types detail by passing Leave No
                public DataSet GetSingleLeave(int LEAVENO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_LNO", LEAVENO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_GETSINGLE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetSingleLeave->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion // End Leave Type Master Region

                // Holidays Region 
                #region Holidays Master
                // To insert New Holiday
                public int AddHoliday(Leaves objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[10];
                        objparams[0] = new SqlParameter("@P_HOLIDAYNAME", objLeave.HOLIDAYNAME);

                        if (!objLeave.HDT.Equals(DateTime.MinValue))
                            objparams[1] = new SqlParameter("@P_DT", objLeave.HDT);
                        else
                            objparams[1] = new SqlParameter("@P_DT", DBNull.Value);

                        //objparams[1]=new SqlParameter("@P_DT",objLeave.HDT);

                        if (!objLeave.TODT.Equals(DateTime.MinValue))
                            objparams[2] = new SqlParameter("@P_TODT", objLeave.TODT);
                        else
                            objparams[2] = new SqlParameter("@P_TODT", DBNull.Value);

                        objparams[3] = new SqlParameter("@P_YEAR", objLeave.YEAR);
                        objparams[4] = new SqlParameter("@P_PERIOD", objLeave.PERIOD);
                        objparams[5] = new SqlParameter("@P_COLLEGE_CODE", objLeave.COLLEGE_CODE);
                        objparams[6] = new SqlParameter("@P_HTNO", objLeave.HTNO);
                        objparams[7] = new SqlParameter("@P_STNO", objLeave.STNO);
                        objparams[8] = new SqlParameter("@P_RESTRICT_STATUS", objLeave.STATUS);
                        objparams[9] = new SqlParameter("@P_COLLEGE_NO", objLeave.COLLEGE_NO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_HOLIDAYS_INSERT", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddHoliday->" + ex.ToString());
                    }
                    return retstatus;
                }

                // To update  existing holiday details
                public int UpdateHoliday(Leaves objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_HNO", objLeave.HNO);
                        objParams[1] = new SqlParameter("@P_HOLIDAYNAME", objLeave.HOLIDAYNAME);
                        if (!objLeave.HDT.Equals(DateTime.MinValue))
                            objParams[2] = new SqlParameter("@P_DT", objLeave.HDT);
                        else
                            objParams[2] = new SqlParameter("@P_DT", DBNull.Value);

                        if (!objLeave.TODT.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_TODT", objLeave.TODT);
                        else
                            objParams[3] = new SqlParameter("@P_TODT", DBNull.Value);

                        //objParams[2] = new SqlParameter("@P_DT", objLeave.HDT);
                        objParams[4] = new SqlParameter("@P_YEAR", objLeave.YEAR);
                        objParams[5] = new SqlParameter("@P_PERIOD", objLeave.PERIOD);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objLeave.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_HTNO", objLeave.HTNO);
                        objParams[8] = new SqlParameter("@P_STNO", objLeave.STNO);
                        objParams[9] = new SqlParameter("@P_RESTRICT_STATUS", objLeave.STATUS);
                        objParams[10] = new SqlParameter("@P_COLLEGE_NO", objLeave.COLLEGE_NO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_HOLIDAYS_UPDATE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateHoliday->" + ex.ToString());
                    }
                    return retstatus;

                }

                // To Delete  existing holiday details
                public int DeleteHoliday(int HNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_HNO", HNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_HOLIDAYS_DELETE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.DeleteHoloday->" + ex.ToString());

                    }
                    return retstatus;
                }

                // To Fetch all existing holiday details
                public DataSet RetrieveAllHoliday(int collegeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_COLLEGE_NO", collegeno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_HOLIDAYS_GETALL", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.RetrieveAllHolodays->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                // To fetch existing holiday details by passing Holiday No.
                public DataSet RetrieveSingleHoliday(int HNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_HNO", HNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_HOLIDAYS_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.RetrieveSingleHoliday->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;


                }

                // To check existing  date is holiday by passing Date
                public DataSet RetrieveSingleHolydate(DateTime HDT)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DT", HDT);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_HOLIDAYS_GET_BY_DT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.RetrieveSingleHolydate->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;


                }


                // To insert New HOLIDAY TYPE
                public int AddHolidayType(Leaves objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_HOLIDAYTYPE", objLeave.HOLIDAYTYPE);

                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", objLeave.COLLEGE_CODE);
                        objParams[2] = new SqlParameter("@P_HTNO", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_HOLIDAY_TYPE_INSERT", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddHolidayType->" + ex.ToString());
                    }
                    return retstatus;
                }

                //To modify existing HOLIDAY TYPE
                public int UpdateHolidayType(Leaves objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_HTNO", objLeave.HTNO);
                        objParams[1] = new SqlParameter("@P_HOLIDAYTYPE", objLeave.HOLIDAYTYPE);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", objLeave.COLLEGE_CODE);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_HOLIDAY_TYPE_UPDATE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateHolidayType->" + ex.ToString());
                    }
                    return retstatus;
                }

                // To Fetch all existing Holiday type
                public DataSet GetAllHolidayType()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_HOLIDAYTYPE_GET_BYALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetAllHolidayType->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                // To fetch existing Holiday type details by passing htno
                public DataSet GetSingHolidayType(int htno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null; ;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_HTNO", htno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_HOLIDAY_TYPE_GET_BY_NO", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetSingHolidayType->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                //To Delete existing Holiday type
                public int DeleteHolidaytype(int HTNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_HTNO", HTNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_HOLIDAY_TYPE_DELETE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.DeleteHolidaytype->" + ex.ToString());
                    }
                    return retstatus;
                }



                ///WORK TYPE MASTER/////


                // To Fetch all existing Work type
                public DataSet GetAllWorkType()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_WORKTYPE_GET_BYALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetAllWorkType->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                // To insert New Work TYPE
                public int AddWorkType(Leaves objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_WORKTYPE", objLeave.WORKTYPE);

                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", objLeave.COLLEGE_CODE);
                        objParams[2] = new SqlParameter("@P_WTNO", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_WORK_TYPE_INSERT", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddWorkType->" + ex.ToString());
                    }
                    return retstatus;
                }

                //To modify existing Work TYPE
                public int UpdateWorkType(Leaves objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        //objParams[0] = new SqlParameter("@P_WTNO", objLeave.HTNO);
                        //objParams[1] = new SqlParameter("@P_WORKTYPE", objLeave.HOLIDAYTYPE);
                        //objParams[2] = new SqlParameter("@P_COLLEGE_CODE", objLeave.COLLEGE_CODE);
                        objParams[0] = new SqlParameter("@P_WTNO", objLeave.WTNO);
                        objParams[1] = new SqlParameter("@P_WORKTYPE", objLeave.WORKTYPE);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", objLeave.COLLEGE_CODE);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_WORK_TYPE_UPDATE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateHolidayType->" + ex.ToString());
                    }
                    return retstatus;
                }

                // To fetch existing Work type details by passing htno
                public DataSet GetSingWorkType(int Wtno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null; ;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_WTNO", Wtno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_WORK_TYPE_GET_BY_NO", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetSingWorkType->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                //To Delete existing Work type
                public int DeleteWorkType(int WTNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_WTNO", WTNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_WORK_TYPE_DELETE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.DeleteHolidaytype->" + ex.ToString());
                    }
                    return retstatus;
                }

                #endregion

                #region RestrictLeave
                // To Fetch all existing RestrictLeave
                public DataSet GetAllRestrictLeave()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_RESTRICT_LEAVE_GET_BYALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetAllRestrictLeave->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                // To insert New RestrictLeave
                public int AddRestrictLeave(Leaves objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_STNO", objLeave.STNO);
                        objParams[1] = new SqlParameter("@P_LEAVENO", objLeave.LEAVENO1);
                        objParams[2] = new SqlParameter("@P_DAYS", objLeave.NO_DAYS);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", objLeave.COLLEGE_CODE);
                        objParams[4] = new SqlParameter("@P_LNO", objLeave.LNO);
                        objParams[5] = new SqlParameter("@P_APPOINTNO", objLeave.APPOINT_NO);
                        objParams[6] = new SqlParameter("@P_RESNO", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_RESTRICT_LEAVE_INSERT", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddRestrictLeave->" + ex.ToString());
                    }
                    return retstatus;
                }


                //To modify existing RestrictLeave

                public int UpdateRestrictLeave(Leaves objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_RESNO", objLeave.RESNO);
                        objParams[1] = new SqlParameter("@P_STNO", objLeave.STNO);
                        objParams[2] = new SqlParameter("@P_LEAVENO", objLeave.LEAVENO1);
                        objParams[3] = new SqlParameter("@P_DAYS", objLeave.NO_DAYS);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", objLeave.COLLEGE_CODE);
                        objParams[5] = new SqlParameter("@P_LNO", objLeave.LNO);
                        objParams[6] = new SqlParameter("@P_APPOINTNO", objLeave.APPOINT_NO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_RESTRICT_LEAVE_UPDATE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateRestrictLeave->" + ex.ToString());
                    }
                    return retstatus;
                }



                // To fetch existing RestrictLeave details by passing htno
                public DataSet GetSingleRestrictLeave(int resno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null; ;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_RESNO", resno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_RESTRICT_LEAVE_GET_BY_NO", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetSingleRestrictLeave->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                //To Delete existing RestrictLeave
                public int DeleteRestrictLeave(int RESNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_RESNO", RESNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_RESTRICT_LEAVE_DELETE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.DeleteRestrictLeave->" + ex.ToString());
                    }
                    return retstatus;
                }


                #endregion

                # region PASSING_AUTHORITY
                // To insert New PASSING_AUTHORITY
                public int AddPassAuthority(Leaves objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_PANAME", objLeave.PANAME);
                        objParams[1] = new SqlParameter("@P_UA_NO", objLeave.UANO);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", objLeave.COLLEGE_NO);
                        objParams[3] = new SqlParameter("@P_PANO", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_PASSING_AUTHORITY_INSERT", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddPassAuthority->" + ex.ToString());
                    }
                    return retstatus;
                }
                //To modify existing Passing Authority
                public int UpdatePassAuthority(Leaves objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_PANO", objLeave.PANO);
                        objParams[1] = new SqlParameter("@P_PANAME", objLeave.PANAME);
                        objParams[2] = new SqlParameter("@P_UA_NO", objLeave.UANO);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", objLeave.COLLEGE_NO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_PASSING_AUTHORITY_UPDATE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdatePassAuthority->" + ex.ToString());
                    }
                    return retstatus;
                }
                //To Delete existing Passing Authority
                public int DeletePassAuthority(int PANo)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_PANO", PANo);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_PASSING_AUTHORITY_DELETE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.DeletePassAuthority->" + ex.ToString());
                    }
                    return retstatus;
                }

                // To Fetch all existing passing authority details
                public DataSet GetAllPassAuthority(int collegeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_PASSING_AUTHORITY_GET_BYALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.RetAllPassAuthority->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                // To fetch existing Passing Authority details by passing Passing Authority No.
                public DataSet GetSingPassAuthority(int PANo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null; ;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_PANO", PANo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_PASSING_AUTHORITY_GET_BY_NO", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.RetSingPassAuthority->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                #endregion

                #region LEAVE PASSING_AUTHORITY_PATH
                // To insert New PASSING_AUTHORITY_PATH

                public int AddPAPath(Leaves objLeave, DataTable dtEmpRecord)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[13];
                        objparams[0] = new SqlParameter("@P_PAN01", objLeave.PAN01);
                        objparams[1] = new SqlParameter("@P_PAN02", objLeave.PAN02);
                        objparams[2] = new SqlParameter("@P_PAN03", objLeave.PAN03);
                        objparams[3] = new SqlParameter("@P_PAN04", objLeave.PAN04);
                        objparams[4] = new SqlParameter("@P_PAN05", objLeave.PAN05);
                        objparams[5] = new SqlParameter("@P_PAPATH", objLeave.PAPATH);
                        objparams[6] = new SqlParameter("@P_DEPTNO", objLeave.DEPTNO);
                        objparams[7] = new SqlParameter("@P_LNO", objLeave.LNO);
                        objparams[8] = new SqlParameter("@P_COLLEGE_NO", objLeave.COLLEGE_NO);
                        objparams[9] = new SqlParameter("@P_COLLEGE_CODE", objLeave.COLLEGE_CODE);
                        objparams[10] = new SqlParameter("@P_IsCopypath", objLeave.IsCopypath);
                        objparams[11] = new SqlParameter("@P_ESTB_EMP_RECORD", dtEmpRecord);
                        objparams[12] = new SqlParameter("@P_PAPNO", SqlDbType.Int);
                        objparams[12].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_PASSING_AUTHORITY_PATH_INSERT", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddPAPath->" + ex.ToString());
                    }
                    return retstatus;
                }

                public int UpdatePAPath(Leaves objLeave, DataTable dtEmpRecord)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[12];
                        objparams[0] = new SqlParameter("@P_PAPNO", objLeave.PAPNO);
                        objparams[1] = new SqlParameter("@P_PAN01", objLeave.PAN01);
                        objparams[2] = new SqlParameter("@P_PAN02", objLeave.PAN02);
                        objparams[3] = new SqlParameter("@P_PAN03", objLeave.PAN03);
                        objparams[4] = new SqlParameter("@P_PAN04", objLeave.PAN04);
                        objparams[5] = new SqlParameter("@P_PAN05", objLeave.PAN05);
                        objparams[6] = new SqlParameter("@P_PAPATH", objLeave.PAPATH);
                        objparams[7] = new SqlParameter("@P_DEPTNO", objLeave.DEPTNO);
                        objparams[8] = new SqlParameter("@P_LNO", objLeave.LNO);
                        objparams[9] = new SqlParameter("@P_COLLEGE_NO", objLeave.COLLEGE_NO);
                        objparams[10] = new SqlParameter("@P_COLLEGE_CODE", objLeave.COLLEGE_CODE);
                        objparams[11] = new SqlParameter("@P_ESTB_EMP_RECORD", dtEmpRecord);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_PASSING_AUTHORITY_PATH_UPDATE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdatePAPath->" + ex.ToString());
                    }
                    return retstatus;
                }
                public int DeletePAPath(int PAPNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@PAPNO", PAPNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_PASSING_AUTHORITY_PATH_DELETE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.DeletePAPath->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetAllPAPath(int collegeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_PASSING_AUTHORITY_PATH_GET_BYALL", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetAllPAPath->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public DataSet GetSinglePAPath(int PAPNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_PAPNO", PAPNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_PASSING_AUTHORITY_PATH_GET_BY_NO", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetSinglePAPath->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion
                #region ODPassing Authority Path

                public DataSet GetAllODPAPath(int collegeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_OD_PAY_PASSING_AUTHORITY_PATH_GET_BYALL", objparams);

                        //ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_PASSING_AUTHORITY_PATH_GET_BYALL", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetAllPAPath->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int AddODPAPathOLD(OD objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[9];
                        objparams[0] = new SqlParameter("@P_PAN01", objLeave.PAN01);
                        objparams[1] = new SqlParameter("@P_PAN02", objLeave.PAN02);
                        objparams[2] = new SqlParameter("@P_PAN03", objLeave.PAN03);
                        objparams[3] = new SqlParameter("@P_PAN04", objLeave.PAN04);
                        objparams[4] = new SqlParameter("@P_PAN05", objLeave.PAN05);
                        objparams[5] = new SqlParameter("@P_PAPATH", objLeave.PAPATH);
                        objparams[6] = new SqlParameter("@P_DEPTNO", objLeave.DEPTNO);
                        objparams[7] = new SqlParameter("@P_COLLEGE_NO", objLeave.COLLEGE_NO);
                        objparams[8] = new SqlParameter("@P_PAPNO", SqlDbType.Int);
                        objparams[8].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_OD_PAY_PASSING_AUTHORITY_PATH_INSERT", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddPAPath->" + ex.ToString());
                    }
                    return retstatus;
                }

                public int UpdateODPAPathOLD(OD objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[9];
                        objparams[0] = new SqlParameter("@P_PAPNO", objLeave.PAPNO);
                        objparams[1] = new SqlParameter("@P_PAN01", objLeave.PAN01);
                        objparams[2] = new SqlParameter("@P_PAN02", objLeave.PAN02);
                        objparams[3] = new SqlParameter("@P_PAN03", objLeave.PAN03);
                        objparams[4] = new SqlParameter("@P_PAN04", objLeave.PAN04);
                        objparams[5] = new SqlParameter("@P_PAN05", objLeave.PAN05);
                        objparams[6] = new SqlParameter("@P_PAPATH", objLeave.PAPATH);
                        objparams[7] = new SqlParameter("@P_DEPTNO", objLeave.DEPTNO);
                        objparams[8] = new SqlParameter("@P_COLLEGE_NO", objLeave.COLLEGE_NO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_OD_PAY_PASSING_AUTHORITY_PATH_UPDATE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdatePAPath->" + ex.ToString());
                    }
                    return retstatus;
                }
                public int AddODPAPath(Leaves objLeave, DataTable dtEmpRecord)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[11];
                        objparams[0] = new SqlParameter("@P_PAN01", objLeave.PAN01);
                        objparams[1] = new SqlParameter("@P_PAN02", objLeave.PAN02);
                        objparams[2] = new SqlParameter("@P_PAN03", objLeave.PAN03);
                        objparams[3] = new SqlParameter("@P_PAN04", objLeave.PAN04);
                        objparams[4] = new SqlParameter("@P_PAN05", objLeave.PAN05);
                        objparams[5] = new SqlParameter("@P_PAPATH", objLeave.PAPATH);
                        objparams[6] = new SqlParameter("@P_DEPTNO", objLeave.DEPTNO);
                        objparams[7] = new SqlParameter("@P_COLLEGE_NO", objLeave.COLLEGE_NO);
                        objparams[8] = new SqlParameter("@P_COLLEGE_CODE", objLeave.COLLEGE_CODE);
                        objparams[9] = new SqlParameter("@P_ESTB_EMP_RECORD", dtEmpRecord);
                        objparams[10] = new SqlParameter("@P_PAPNO", SqlDbType.Int);
                        objparams[10].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_OD_PAY_PASSING_AUTHORITY_PATH_INSERT", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddPAPath->" + ex.ToString());
                    }
                    return retstatus;
                }

                public int UpdateODPAPath(Leaves objLeave, DataTable dtEmpRecord)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[11];
                        objparams[0] = new SqlParameter("@P_PAPNO", objLeave.PAPNO);
                        objparams[1] = new SqlParameter("@P_PAN01", objLeave.PAN01);
                        objparams[2] = new SqlParameter("@P_PAN02", objLeave.PAN02);
                        objparams[3] = new SqlParameter("@P_PAN03", objLeave.PAN03);
                        objparams[4] = new SqlParameter("@P_PAN04", objLeave.PAN04);
                        objparams[5] = new SqlParameter("@P_PAN05", objLeave.PAN05);
                        objparams[6] = new SqlParameter("@P_PAPATH", objLeave.PAPATH);
                        objparams[7] = new SqlParameter("@P_DEPTNO", objLeave.DEPTNO);
                        objparams[8] = new SqlParameter("@P_COLLEGE_NO", objLeave.COLLEGE_NO);
                        objparams[9] = new SqlParameter("@P_COLLEGE_CODE", objLeave.COLLEGE_CODE);
                        objparams[10] = new SqlParameter("@P_ESTB_EMP_RECORD", dtEmpRecord);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_OD_PAY_PASSING_AUTHORITY_PATH_UPDATE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdatePAPath->" + ex.ToString());
                    }
                    return retstatus;
                }
                public int DeleteODPAPath(int PAPNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@PAPNO", PAPNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_OD_PAY_PASSING_AUTHORITY_PATH_DELETE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.DeletePAPath->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetSingleODPAPath(int PAPNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_PAPNO", PAPNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_OD_PAY_PASSING_AUTHORITY_PATH_GET_BY_NO", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetSinglePAPath->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion
                #region Leave Application
                /// <summary>
                /// Purpose:To Get SMS detail to send to Authority
                /// </summary>
                /// <param name="objLM"></param>
                /// <returns></returns>
                public DataSet GetSMSInformation(Leaves objLM)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_LETRNO", objLM.LETRNO);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_GET_SMS_INFORMATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetSMSInformation->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public DataSet CheckLeaveExists(Leaves objLM)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_FRDATE", objLM.FROMDT);
                        objParams[1] = new SqlParameter("@P_TODATE", objLM.TODT);
                        objParams[2] = new SqlParameter("@P_LETRNO", objLM.LETRNO);
                        objParams[3] = new SqlParameter("@P_EMPNO", objLM.EMPNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_CHECK_LEAVE_EXISTS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetNoofdays->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public DataSet GetAllEngagedLecture(Leaves objLM)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_TRANNO", objLM.TRANNO);
                        objParams[1] = new SqlParameter("@P_IDNO", objLM.EMPNO);
                        objParams[2] = new SqlParameter("@P_LNO", objLM.LNO);

                        objParams[3] = new SqlParameter("@P_LETRNO", objLM.LETRNO);
                        if (!objLM.FROMDT.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_FROM_DATE", objLM.FROMDT);
                        else
                            objParams[4] = new SqlParameter("@P_FROM_DATE", DBNull.Value);

                        if (!objLM.TODT.Equals(DateTime.MinValue))
                            objParams[5] = new SqlParameter("@P_TO_DATE", objLM.TODT);
                        else
                            objParams[5] = new SqlParameter("@P_TO_DATE", DBNull.Value);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_GETALL_ENGAGED_LEACTURE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetAllEngagedLecture->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public int DeleteEngagedLecture(Leaves objLM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_LNO", objLM.LNO);
                        objParams[1] = new SqlParameter("@P_TRANNO", objLM.TRANNO);
                        objParams[2] = new SqlParameter("@P_IDNO", objLM.EMPNO);
                        objParams[3] = new SqlParameter("@P_LETRNO", objLM.LETRNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_DELETE_ENGAGED_LEACTURE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.DeleteLeave-> " + ex.ToString());
                    }
                    return retstatus;
                }
                public int AddEngagedLecture(Leaves objLM)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_TRANNO", objLM.TRANNO);
                        objParams[1] = new SqlParameter("@P_PERIODNO", objLM.PERIODNO);

                        if (!objLM.ENGAGED_DATE.Equals(DateTime.MinValue))
                            objParams[2] = new SqlParameter("@P_ENGAGED_DATE", objLM.ENGAGED_DATE);
                        else
                            objParams[2] = new SqlParameter("@P_ENGAGED_DATE", DBNull.Value);
                        objParams[3] = new SqlParameter("@P_YEAR_SEM", objLM.YEARSEM);
                        objParams[4] = new SqlParameter("@P_SUBJECT", objLM.SUBJECT);
                        objParams[5] = new SqlParameter("@P_FACULTY", objLM.FACULTYNO);
                        objParams[6] = new SqlParameter("@P_FACULTY_NAME", objLM.FACULTY_NAME);
                        objParams[7] = new SqlParameter("@P_THEORY", objLM.THEORY);
                        objParams[8] = new SqlParameter("@P_IDNO", objLM.EMPNO);
                        objParams[9] = new SqlParameter("@P_LNO", objLM.LNO);
                        objParams[10] = new SqlParameter("@P_LETRNO", objLM.LETRNO);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_INSERT_ENGAGED_LEACTURE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ODController.AddAPP_ENTRY->" + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddAPP_ENTRY(Leaves objLeaves, string pre, string suf, int Rno, DataTable dteng, DataTable dtLoad)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[41];
                        objParams[0] = new SqlParameter("@P_EMPNO", objLeaves.EMPNO);
                        //objParams[1] = new SqlParameter("@P_DEPTNO", objLeaves.DEPTNO);
                        objParams[1] = new SqlParameter("@P_LNO", objLeaves.LNO);
                        if (!objLeaves.APPDT.Equals(DateTime.MinValue))
                            objParams[2] = new SqlParameter("@P_APPDT", objLeaves.APPDT);
                        else
                            objParams[2] = new SqlParameter("@P_APPDT", DBNull.Value);

                        if (!objLeaves.FROMDT.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_FROM_DATE", objLeaves.FROMDT);
                        else
                            objParams[3] = new SqlParameter("@P_FROM_DATE", DBNull.Value);

                        if (!objLeaves.TODT.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_TO_DATE", objLeaves.TODT);
                        else
                            objParams[4] = new SqlParameter("@P_TO_DATE", DBNull.Value);

                        objParams[5] = new SqlParameter("@P_NO_OF_DAYS", objLeaves.NO_DAYS);
                        if (!objLeaves.JOINDT.Equals(DateTime.MinValue))
                            objParams[6] = new SqlParameter("@P_JOINDT", objLeaves.JOINDT);
                        else
                            objParams[6] = new SqlParameter("@P_JOINDT", DBNull.Value);


                        objParams[7] = new SqlParameter("@P_FIT", objLeaves.FIT);
                        objParams[8] = new SqlParameter("@P_UNFIT", objLeaves.UNFIT);
                        objParams[9] = new SqlParameter("@P_PERIOD", objLeaves.PERIOD);
                        objParams[10] = new SqlParameter("@P_FNAN", objLeaves.FNAN);

                        objParams[11] = new SqlParameter("@P_REASON", objLeaves.REASON);
                        objParams[12] = new SqlParameter("@P_ADDRESS", objLeaves.ADDRESS);
                        objParams[13] = new SqlParameter("@P_STATUS", objLeaves.STATUS);
                        objParams[14] = new SqlParameter("@P_COLLEGE_CODE", objLeaves.COLLEGE_CODE);
                        objParams[15] = new SqlParameter("@P_PREFIX", objLeaves.PREFIX);
                        objParams[16] = new SqlParameter("@P_SUFFIX", objLeaves.SUFFIX);
                        objParams[17] = new SqlParameter("@P_PAPNO", objLeaves.PAPNO);
                        objParams[18] = new SqlParameter("@P_PRE", pre);
                        objParams[19] = new SqlParameter("@P_POST", suf);
                        objParams[20] = new SqlParameter("@P_CHARGE", objLeaves.CHARGE_HANDED);
                        objParams[21] = new SqlParameter("@P_ENTRY_STATUS", objLeaves.ENTRY_STATUS);
                        objParams[22] = new SqlParameter("@P_LEAVEFNAN", objLeaves.LEAVEFNAN);
                        objParams[23] = new SqlParameter("@P_RNO", Rno);
                        objParams[24] = new SqlParameter("@P_MLHPL", objLeaves.MLHPL);

                        objParams[25] = new SqlParameter("@P_LEAVENO", objLeaves.LEAVENO);
                        objParams[26] = new SqlParameter("@P_SESSION_SRNO", objLeaves.SESSION_SRNO);
                        objParams[27] = new SqlParameter("@P_ISFULLDAYLEAVE", objLeaves.ISFULLDAYLEAVE);
                        objParams[28] = new SqlParameter("@P_LEAVE_SESSION_SERVICE_SRNO", objLeaves.SESSION_SERVICE_SRNO);
                        objParams[29] = new SqlParameter("@IsWithCertificate", objLeaves.IsWithCertificate);
                        objParams[30] = new SqlParameter("@P_FileName", objLeaves.FileName);
                        objParams[31] = new SqlParameter("@P_FilePath", objLeaves.FilePath);
                        objParams[32] = new SqlParameter("@P_FileSize", objLeaves.FileSize);
                        objParams[33] = new SqlParameter("@P_Type", objLeaves.LType);
                        objParams[34] = new SqlParameter("@P_CREATEDBY", objLeaves.CREATEDBY);
                        objParams[35] = new SqlParameter("@P_CHIDNO", objLeaves.CHIDNO);
                        objParams[36] = new SqlParameter("@P_ESTB_LEAVE_EMP_LECTURE_RECORD", dteng);
                        objParams[37] = new SqlParameter("@P_CLASS_ARRAN_STATUS", objLeaves.CLASS_ARRAN_STATUS);
                        objParams[38] = new SqlParameter("@P_ESTB_LEAVE_EMP_LOAD_RECORD", dtLoad);
                        objParams[39] = new SqlParameter("@P_LOAD_STATUS", objLeaves.LOAD_STATUS);
                        objParams[40] = new SqlParameter("@P_LETRNO", SqlDbType.Int);
                        objParams[40].Direction = ParameterDirection.Output;

                        int ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_APP_ENTRY_INSERT1", objParams, true));
                        //if (Convert.ToInt32(ret) == -99)
                        //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        //else
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = -99;
                        else
                            retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddAPP_ENTRY->" + ex.ToString());
                    }
                    return retStatus;
                }


                public int AddAPP_ENTRY_auth(Leaves objLeaves, string pre, string suf, int userno, DateTime aprdt, int rno)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[30];
                        objParams[0] = new SqlParameter("@P_EMPNO", objLeaves.EMPNO);
                        //objParams[1] = new SqlParameter("@P_DEPTNO", objLeaves.DEPTNO);
                        objParams[1] = new SqlParameter("@P_LNO", objLeaves.LNO);
                        if (!objLeaves.APPDT.Equals(DateTime.MinValue))
                            objParams[2] = new SqlParameter("@P_APPDT", objLeaves.APPDT);
                        else
                            objParams[2] = new SqlParameter("@P_APPDT", DBNull.Value);

                        if (!objLeaves.FROMDT.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_FROM_DATE", objLeaves.FROMDT);
                        else
                            objParams[3] = new SqlParameter("@P_FROM_DATE", DBNull.Value);

                        if (!objLeaves.TODT.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_TO_DATE", objLeaves.TODT);
                        else
                            objParams[4] = new SqlParameter("@P_TO_DATE", DBNull.Value);

                        objParams[5] = new SqlParameter("@P_NO_OF_DAYS", objLeaves.NO_DAYS);
                        if (!objLeaves.JOINDT.Equals(DateTime.MinValue))
                            objParams[6] = new SqlParameter("@P_JOINDT", objLeaves.JOINDT);
                        else
                            objParams[6] = new SqlParameter("@P_JOINDT", DBNull.Value);


                        objParams[7] = new SqlParameter("@P_FIT", objLeaves.FIT);
                        objParams[8] = new SqlParameter("@P_UNFIT", objLeaves.UNFIT);
                        objParams[9] = new SqlParameter("@P_PERIOD", objLeaves.PERIOD);
                        objParams[10] = new SqlParameter("@P_FNAN", objLeaves.FNAN);

                        objParams[11] = new SqlParameter("@P_REASON", objLeaves.REASON);
                        objParams[12] = new SqlParameter("@P_ADDRESS", objLeaves.ADDRESS);
                        objParams[13] = new SqlParameter("@P_STATUS", objLeaves.STATUS);
                        objParams[14] = new SqlParameter("@P_COLLEGE_CODE", objLeaves.COLLEGE_CODE);
                        objParams[15] = new SqlParameter("@P_PREFIX", objLeaves.PREFIX);
                        objParams[16] = new SqlParameter("@P_SUFFIX", objLeaves.SUFFIX);
                        objParams[17] = new SqlParameter("@P_PAPNO", objLeaves.PAPNO);
                        objParams[18] = new SqlParameter("@P_PRE", pre);
                        objParams[19] = new SqlParameter("@P_POST", suf);
                        objParams[20] = new SqlParameter("@P_UA_NO", userno);
                        objParams[21] = new SqlParameter("@P_APRDT", aprdt);
                        objParams[22] = new SqlParameter("@P_CHARGE", objLeaves.CHARGE_HANDED);
                        objParams[23] = new SqlParameter("@P_ENTRY_STATUS", objLeaves.ENTRY_STATUS);
                        objParams[24] = new SqlParameter("@P_MLHPL", objLeaves.MLHPL);
                        objParams[25] = new SqlParameter("@P_RNO", rno);
                        objParams[26] = new SqlParameter("@P_LEAVENO", objLeaves.LEAVENO);
                        objParams[27] = new SqlParameter("@P_SESSION_SRNO", objLeaves.SESSION_SRNO);
                        objParams[28] = new SqlParameter("@P_ISFULLDAYLEAVE", objLeaves.ISFULLDAYLEAVE);

                        objParams[29] = new SqlParameter("@P_LETRNO", SqlDbType.Int);
                        objParams[29].Direction = ParameterDirection.Output;

                        int ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_APP_ENTRY_INSERT2", objParams, true));
                        if (Convert.ToInt32(ret) == -99)
                            // retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            retStatus = -99;
                        else
                            retStatus = ret;
                        //retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddAPP_ENTRY_auth->" + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// created by Mrunal Bansod on dated 4 april 2012 to leave entry for late coming  
                /// </summary>
                /// <param name="objLeaves"></param>
                /// <param name="pre"></param>
                /// <param name="suf"></param>
                /// <returns></returns>
                public int AddAPP_ENTRYByLateComing(Leaves objLeaves, string pre, string suf)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[23];
                        objParams[0] = new SqlParameter("@P_EMPNO", objLeaves.EMPNO);
                        //objParams[1] = new SqlParameter("@P_DEPTNO", objLeaves.DEPTNO);
                        objParams[1] = new SqlParameter("@P_LNO", objLeaves.LNO);
                        if (!objLeaves.APPDT.Equals(DateTime.MinValue))
                            objParams[2] = new SqlParameter("@P_APPDT", objLeaves.APPDT);
                        else
                            objParams[2] = new SqlParameter("@P_APPDT", DBNull.Value);

                        if (!objLeaves.FROMDT.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_FROM_DATE", objLeaves.FROMDT);
                        else
                            objParams[3] = new SqlParameter("@P_FROM_DATE", DBNull.Value);

                        if (!objLeaves.TODT.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_TO_DATE", objLeaves.TODT);
                        else
                            objParams[4] = new SqlParameter("@P_TO_DATE", DBNull.Value);

                        objParams[5] = new SqlParameter("@P_NO_OF_DAYS", objLeaves.NO_DAYS);
                        if (!objLeaves.JOINDT.Equals(DateTime.MinValue))
                            objParams[6] = new SqlParameter("@P_JOINDT", objLeaves.JOINDT);
                        else
                            objParams[6] = new SqlParameter("@P_JOINDT", DBNull.Value);


                        objParams[7] = new SqlParameter("@P_FIT", objLeaves.FIT);
                        objParams[8] = new SqlParameter("@P_UNFIT", objLeaves.UNFIT);
                        objParams[9] = new SqlParameter("@P_PERIOD", objLeaves.PERIOD);
                        objParams[10] = new SqlParameter("@P_FNAN", objLeaves.FNAN);

                        objParams[11] = new SqlParameter("@P_REASON", objLeaves.REASON);
                        objParams[12] = new SqlParameter("@P_ADDRESS", objLeaves.ADDRESS);
                        objParams[13] = new SqlParameter("@P_STATUS", objLeaves.STATUS);
                        objParams[14] = new SqlParameter("@P_COLLEGE_CODE", objLeaves.COLLEGE_CODE);
                        objParams[15] = new SqlParameter("@P_PREFIX", objLeaves.PREFIX);
                        objParams[16] = new SqlParameter("@P_SUFFIX", objLeaves.SUFFIX);
                        objParams[17] = new SqlParameter("@P_PAPNO", objLeaves.PAPNO);
                        objParams[18] = new SqlParameter("@P_PRE", pre);
                        objParams[19] = new SqlParameter("@P_POST", suf);
                        objParams[20] = new SqlParameter("@P_CHARGE", objLeaves.CHARGE_HANDED);
                        objParams[21] = new SqlParameter("@P_ENTRY_STATUS", objLeaves.ENTRY_STATUS);
                        objParams[22] = new SqlParameter("@P_LETRNO", SqlDbType.Int);
                        objParams[22].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_APP_ENTRY_BYLATECOMING", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddAPP_ENTRYByLateComing->" + ex.ToString());
                    }
                    return retStatus;
                }


                public DataSet GetLeavesStatus(int IDNO, int Year, int LNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[3];
                        objparams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objparams[1] = new SqlParameter("@P_YEAR ", Year);
                        objparams[2] = new SqlParameter("@P_LNO", LNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_BAL_LEAVESTATUS", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLeavesStatus->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetLeavesBalStatus(int IDNO, int Year, int LNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[3];
                        objparams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objparams[1] = new SqlParameter("@P_YEAR ", Year);
                        objparams[2] = new SqlParameter("@P_LNO", LNO);
                        //PKG_ESTB_LEAVE_PAY_BAL_LEAVESTATUS
                        //ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_BAL_STATUS", objparams);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_BAL_LEAVESTATUS", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLeavesStatus->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }


                public DataSet GetLeaveApplStatus(int EmpNo, int stno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[2];
                        objparams[0] = new SqlParameter("@P_EMPNO", EmpNo);
                        objparams[1] = new SqlParameter("@P_STNO", stno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_APP_ENTRY_GET_STATUS", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLeaveApplStatus->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;

                }

                public DataSet GetLeaveApplStatusForRestrictedholidays(int EmpNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_EMPNO", EmpNo);
                        //objparams[1] = new SqlParameter("@P_STNO", stno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_APP_ENTRY_GET_STATUS_FOR_RESTRICT", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLeaveApplStatus->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;

                }

                public DataSet GetSuffixDays(string Tdt, int Cnt)
                {
                    DataSet cnt = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_TDT", Tdt);
                        objParams[1] = new SqlParameter("@P_CNT", Cnt);
                        objParams[1].Direction = ParameterDirection.Output;
                        cnt = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_APP_ENTRY_GET_SUFFIX", objParams);

                    }
                    catch (Exception ex)
                    {
                        return cnt;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetSuffixDays->" + ex.ToString());
                    }
                    return cnt;

                }
                public DataSet GetPrefixDays(string Fdt, int Cnt, int holcnt)
                {
                    DataSet dt = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_PDT", Fdt);
                        objParams[1] = new SqlParameter("@P_CNT", Cnt);
                        objParams[2] = new SqlParameter("@P_HOL", holcnt);
                        dt = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_APP_ENTRY_GET_PREFIX", objParams);

                    }
                    catch (Exception ex)
                    {
                        return dt;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetPrefixDays->" + ex.ToString());
                    }
                    return dt;
                }
                public DataSet GetPAPath_EmpNO(int EMPNO, int LNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_EMPNO", EMPNO);
                        objParams[1] = new SqlParameter("@P_LNO", LNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_PASSING_AUTHORITY_PATH_GET_BY_EMPNO", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetPAPath_EmpNO->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }

                    return ds;
                }

                public int DeleteLeaveAppEntry(int LetrNo, int idno)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_LETRNO", LetrNo);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);
                        objParams[2] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        Object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_APP_ENTRY_DELETE", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.DeleteLeaveAppEntry->" + ex.ToString());
                    }
                    return retStatus;
                }


                public int UpdateAppEntry(Leaves objLeaves, DataTable dteng, DataTable dtLoad)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[25];
                        objParams[0] = new SqlParameter("@P_LETRNO", objLeaves.LETRNO);
                        //objParams[1] = new SqlParameter("@P_EMPNO", objLeaves.EMPNO);
                        objParams[1] = new SqlParameter("@P_LNO", objLeaves.LNO);

                        if (!objLeaves.FROMDT.Equals(DateTime.MinValue))
                            objParams[2] = new SqlParameter("@P_FROM_DATE", objLeaves.FROMDT);
                        else
                            objParams[2] = new SqlParameter("@P_FROM_DATE", DBNull.Value);

                        if (!objLeaves.TODT.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_TO_DATE", objLeaves.TODT);
                        else
                            objParams[3] = new SqlParameter("@P_TO_DATE", DBNull.Value);

                        objParams[4] = new SqlParameter("@P_NO_OF_DAYS", objLeaves.NO_DAYS);
                        if (!objLeaves.JOINDT.Equals(DateTime.MinValue))
                            objParams[5] = new SqlParameter("@P_JOINDT", objLeaves.JOINDT);
                        else
                            objParams[5] = new SqlParameter("@P_JOINDT", DBNull.Value);

                        objParams[6] = new SqlParameter("@P_FIT", objLeaves.FIT);
                        objParams[7] = new SqlParameter("@P_UNFIT", objLeaves.UNFIT);
                        objParams[8] = new SqlParameter("@P_FNAN", objLeaves.FNAN);
                        objParams[9] = new SqlParameter("@P_REASON", objLeaves.REASON);
                        objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objLeaves.COLLEGE_CODE);
                        objParams[11] = new SqlParameter("@P_PREFIX", objLeaves.PREFIX);
                        objParams[12] = new SqlParameter("@P_SUFFIX", objLeaves.SUFFIX);
                        objParams[13] = new SqlParameter("@P_CHARGE", objLeaves.CHARGE_HANDED);
                        objParams[14] = new SqlParameter("@P_ADDRESS", objLeaves.ADDRESS);
                        objParams[15] = new SqlParameter("@P_ISFULLDAYLEAVE", objLeaves.ISFULLDAYLEAVE);
                        objParams[16] = new SqlParameter("@IsWithCertificate", objLeaves.IsWithCertificate);
                        objParams[17] = new SqlParameter("@P_FileName", objLeaves.FileName);
                        objParams[18] = new SqlParameter("@P_Type", objLeaves.LType);
                        objParams[19] = new SqlParameter("@P_MODIFYBY", objLeaves.MODIFIEDBY);
                        objParams[20] = new SqlParameter("@P_CHIDNO", objLeaves.CHIDNO);
                        objParams[21] = new SqlParameter("@P_ESTB_LEAVE_EMP_LECTURE_RECORD", dteng);
                        objParams[22] = new SqlParameter("@P_ESTB_LEAVE_EMP_LOAD_RECORD", dtLoad);
                        objParams[23] = new SqlParameter("@P_LOAD_STATUS", objLeaves.LOAD_STATUS);
                        objParams[24] = new SqlParameter("@P_LEAVEFNAN", objLeaves.LEAVEFNAN);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_APP_ENTRY_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateAppEntry->" + ex.ToString());
                    }
                    return retStatus;
                }

                public int DeleteDeductedLeaveApp(int LetrNo)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_LETRNO", LetrNo);
                        Object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_DELETE_DEDUCTED_LEAVE", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.DeleteLeaveAppEntry->" + ex.ToString());
                    }
                    return retStatus;
                }


                #endregion

                #region Leave Approval
                //Fetch Pending List of Leave application for approval to passing Authority
                public DataSet GetPendListforLeaveApproval(int UA_No)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_No);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_APP_GET_PENDINGLIST", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetPendListforLeaveApproval->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetPendListforLeaveApprovalAuth(int UA_No, int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_No);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_APP_GET_PENDINGLIST_AUTHRITY", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetPendListforLeaveApproval->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetLeaveApplDetail(int LETRNO)
                //fetch  Leave Application details of Particular Leave aaplication by Passing LETRNO & its approval status
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_LETRNO", LETRNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_LEAVEAPLDTL_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLeaveApplDetail->" + ex.ToString());

                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public int UpdateAppPassEntry(int LETRNO, int UA_NO, string STATUS, string REMARKS, DateTime APRDT, int p)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[6];
                        objparams[0] = new SqlParameter("@P_LETRNO", LETRNO);
                        objparams[1] = new SqlParameter("@P_UA_NO", UA_NO);
                        objparams[2] = new SqlParameter("@P_STATUS", STATUS);
                        objparams[3] = new SqlParameter("@P_REMARKS", REMARKS);
                        objparams[4] = new SqlParameter("@P_APRDT", APRDT);
                        objparams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[5].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_APP_PASS_ENTRY_UPDATE1", objparams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_APP_PASS_ENTRY_UPDATE1", objparams, false) != null)
                        //    retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateLvAplAprStatus->" + ex.ToString());
                    }
                    return retStatus;

                }

                public DataSet GetLvApplApprovedList()
                //fetch records Leave application which is approved  for Establishment Section For transfer to Service Book.
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_APP_GET_APPROVEDLIST", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLvApplApprovedList->" + ex.ToString());

                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetLeaveAppEntryByNO(int LETRNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_LETRNO", LETRNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_APP_ENTRY_GET_BY_NO", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLeaveAppEntryByNO->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetLeaveAppEntryByNOForEdit(int IDNO, int LETRNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_LETRNO", LETRNO);
                        objParams[1] = new SqlParameter("@P_ENO", IDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_APP_ENTRY_GET_BY_NO_TOEDIT", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLeaveAppEntryByNO->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet FillLeaveName(int EMPID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_IDNO", EMPID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_FILL", objparams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.FillLeaveName->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                /// <summary>
                /// Created By: Swati Ghate
                /// Date: 10-03-2015
                /// Description: To get allowed capping days after joining date to fill leave application
                /// </summary>
                /// <param name="objLeaves"></param>
                /// <returns></returns>
                public int GetAllowDays(Leaves objLeaves)
                {
                    int ret = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];

                        if (!objLeaves.JOINDT.Equals(DateTime.MinValue))
                            objParams[0] = new SqlParameter("@P_JOINDT", objLeaves.JOINDT);
                        else
                            objParams[0] = new SqlParameter("@P_JOINDT", DBNull.Value);

                        if (!objLeaves.FROMDT.Equals(DateTime.MinValue))
                            objParams[1] = new SqlParameter("@P_FROM_DATE", objLeaves.FROMDT);
                        else
                            objParams[1] = new SqlParameter("@P_FROM_DATE", DBNull.Value);

                        objParams[2] = new SqlParameter("@P_ALLOW_DAYS", objLeaves.NO_DAYS);
                        objParams[3] = new SqlParameter("@P_STNO", objLeaves.STNO);
                        objParams[4] = new SqlParameter("@P_COLLEGENO", objLeaves.COLLEGE_NO);
                        objParams[5] = new SqlParameter("@P_IS_ALLOW_BEFORE_APPLICATION", objLeaves.IsAllowBeforeApplication);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;
                        ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_NO_OF_ALLOWDAYS", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        ret = -99;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetAllowDays->" + ex.ToString());
                    }
                    return ret;
                }
                public int GetAllowDaysNew(string joindt, string todt, int no_days, int stno)
                {
                    int ret = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_JOINDT", joindt);
                        objParams[1] = new SqlParameter("@P_TODAYDATE", todt);
                        objParams[2] = new SqlParameter("@P_ALLOW_DAYS", no_days);
                        objParams[3] = new SqlParameter("@P_STNO", stno);

                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_NO_OF_ALLOWDAYS", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        ret = -99;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetAllowDays->" + ex.ToString());
                    }
                    return ret;
                }
                // public DataSet GetNoofdays(DateTime Frdate, DateTime Todate, string leavetype, int stno, int calholy,int collegeno)
                public DataSet GetNoofdays(DateTime Frdate, DateTime Todate, string leavetype, int stno, int calholy, int college_no, int FNAN)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_FRDATE", Frdate);
                        objParams[1] = new SqlParameter("@P_TODATE", Todate);
                        objParams[2] = new SqlParameter("@P_LEAVETYPE", leavetype);
                        objParams[3] = new SqlParameter("@P_STNO", stno);
                        objParams[4] = new SqlParameter("@P_CAL_HOLIDAY", calholy);
                        objParams[5] = new SqlParameter("@P_COLLEGE_NO", college_no);
                        objParams[6] = new SqlParameter("@P_FNAN", FNAN);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_NO_OF_DAYS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetNoofdays->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetNoofdays(DateTime Frdate, DateTime Todate, string leavetype, int stno, int calholy, int college_no, int FNAN, int empno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_FRDATE", Frdate);
                        objParams[1] = new SqlParameter("@P_TODATE", Todate);
                        objParams[2] = new SqlParameter("@P_LEAVETYPE", leavetype);
                        objParams[3] = new SqlParameter("@P_STNO", stno);
                        objParams[4] = new SqlParameter("@P_CAL_HOLIDAY", calholy);
                        objParams[5] = new SqlParameter("@P_COLLEGE_NO", college_no);
                        objParams[6] = new SqlParameter("@P_FNAN", FNAN);
                        objParams[7] = new SqlParameter("@P_EMPNO", empno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_NO_OF_DAY_NEW", objParams);//
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetNoofdays->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetNoofdaysOD(DateTime Frdate, DateTime Todate, int stno, int collegeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_FRDATE", Frdate);
                        objParams[1] = new SqlParameter("@P_TODATE", Todate);
                        objParams[2] = new SqlParameter("@P_STNO", stno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", collegeno); 
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_OD_NO_OF_DAYS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetNoofdays->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetEmpName(int EMPID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_EMPID", EMPID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_EMPNAME_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetEmpName->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                //public DataTable GetLvApplAprStatus(int LETRNO)
                //    //fetch Leave Application Approval Status of particular leave By passing LETRNO
                //{
                //   DataTable dt = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null;
                //        objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("@P_LETRNO", LETRNO);

                //        dt = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_APL_APPROVALESTATUS", objParams);

                //    }
                //    catch (Exception ex)
                //    {
                //        return dt;
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLvApplAprStatus->" + ex.ToString());
                //    }
                //    finally
                //    {

                //        dt.Dispose();
                //    }
                //    return dt;
                //}

                //public int AddAPP_PASS_ENTRY(Leaves objLeaves)
                //{
                //    int retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //    try
                //    {
                //        SQLHelper objSQLHelper=new SQLHelper(_nitprm_constr);
                //        SqlParameter [] objParams=null;
                //        objParams =new SqlParameter[7];
                //        objParams[0]=new SqlParameter("@P_LETRNO",objLeaves.LETRNO);
                //        objParams[1]=new SqlParameter("@P_PANO",objLeaves.PANO);
                //        objParams[2]=new SqlParameter("@P_STATUS",objLeaves.STATUS);
                //        objParams[3]=new SqlParameter("@P_APP_DATE",objLeaves.APPDT);
                //        objParams[4]=new SqlParameter("@P_APP_REMARKS",objLeaves.APP_REMARKS);
                //        objParams[5]=new SqlParameter("@P_COLLEGE_CODE",objLeaves.COLLEGE_CODE);
                //        objParams[6]=new SqlParameter("@P_LAPENO",SqlDbType.Int);
                //        objParams[6].Direction=ParameterDirection.Output;

                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_APP_PASS_ENTRY_INSERT", objParams, false) != null)
                //            retstatus =Convert.ToInt32(CustomStatus.RecordSaved);

                //    }
                //    catch (Exception ex)
                //    {
                //        retstatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddAPP_PASS_ENTRY->"+ ex.ToString());
                //    }
                //    return retstatus;
                //}


                //Direct Leave Approval

                public DataSet GetPendListforLeaveDirectApproval(int deptno, int STNO, int lno, string dt, int collegeno, int sortno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[1] = new SqlParameter("@P_STNO", STNO);
                        objParams[2] = new SqlParameter("@P_LNO", lno);
                        objParams[3] = new SqlParameter("@P_LVDATE", dt);
                        objParams[4] = new SqlParameter("@P_COLLEGENO", collegeno);
                        objParams[5] = new SqlParameter("@P_SORTNO", sortno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_GET_PENDINGLIST_DIRECT_APPROVAL", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetPendListforLeaveDirectApproval->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetLeaveApplDetailDirectApproval(int LETRNO)
                //fetch  Leave Application details of Particular Leave aaplication by Passing LETRNO & its approval status
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_LETRNO", LETRNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_LEAVEAPLDTL_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLeaveApplDetail->" + ex.ToString());

                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int UpdateAppPassEntryDirectApprvl(int LETRNO, int UA_NO, string STATUS, string REMARKS, DateTime APRDT, int p)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[6];
                        objparams[0] = new SqlParameter("@P_LETRNO", LETRNO);
                        objparams[1] = new SqlParameter("@P_UA_NO", UA_NO);
                        objparams[2] = new SqlParameter("@P_STATUS", STATUS);
                        objparams[3] = new SqlParameter("@P_REMARKS", REMARKS);
                        objparams[4] = new SqlParameter("@P_APRDT", APRDT);
                        objparams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[5].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_APP_PASS_ENTRY_UPD_DIRECT_APPRVL", objparams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_APP_PASS_ENTRY_UPDATE1", objparams, false) != null)
                        //    retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateLvAplAprStatus->" + ex.ToString());
                    }
                    return retStatus;

                }

                /// <summary>
                /// created by Mrunal Bansod 11 oct 2012 
                /// </summary>
                /// <param name="UA_No"></param>
                /// <returns></returns>
                public DataSet GetPendListforLVApprovalStatusALL(int UA_No)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_No);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_APP_GET_PENDINGLIST_STATUS_ALL", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetPendListforLVApprovalStatusALL->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetPendListforLVApprovalStatusParticular(int UA_No, string frmdt, string todt)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_No);
                        objParams[1] = new SqlParameter("@P_FROM_DATE", frmdt);
                        objParams[2] = new SqlParameter("@P_TODATE", todt);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_APP_GET_PENDINGLIST_STATUS", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetPendListforLVApprovalStatusParticular->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public DataSet GetMedicalLeaveValidateCount(int empno, int year)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_EMPNO", empno);
                        objParams[1] = new SqlParameter("@P_YEAR", year);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_MEDICAL_LEAVE_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLeaveApplDetail->" + ex.ToString());

                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion
                #region LeaveTran

                public int AddLeaveTran(string sTrNo, string OrdNo, DateTime OrdDt)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_LETRNOS", sTrNo);
                        objParams[1] = new SqlParameter("@P_ORDNO", OrdNo);
                        objParams[2] = new SqlParameter("@P_ORDDT", OrdDt);
                        objParams[3] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_LEAVETRAN_INSERT", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddLeaveTran->" + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion

                #region Leave Allotment
                public int AddGetLeaveSessionDetails(Leaves objLM)
                {

                    int ret = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];

                        objParams[0] = new SqlParameter("@P_PERIOD", objLM.PERIOD);
                        objParams[1] = new SqlParameter("@P_YEAR", objLM.YEAR);
                        objParams[2] = new SqlParameter("@P_LEAVENO", objLM.LEAVENO);

                        if (!objLM.FROMDT.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_FDT", objLM.FROMDT);
                        else
                            objParams[3] = new SqlParameter("@P_FDT", DBNull.Value);
                        if (!objLM.TODT.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_TDT", objLM.TODT);
                        else
                            objParams[4] = new SqlParameter("@P_TDT", DBNull.Value);

                        objParams[5] = new SqlParameter("@P_COLLEGE_NO", objLM.COLLEGE_NO);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        //ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_INSERT_FINANCIAL_YEAR", objParams);
                        //return ds;
                        ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_INSERT_LEAVE_SESSION", objParams, true));

                    }
                    catch (Exception ex)
                    {

                        ret = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.AddGetFinancialYearDetails->" + ex.ToString());

                    }
                    return ret;
                }
                public DataSet GetLeavesSType(int StaffType, int period, char gender)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_STAFFTYPE", StaffType);
                        objParams[1] = new SqlParameter("@P_PERIOD", period);
                        objParams[2] = new SqlParameter("@P_GENDER", gender);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_LEAVES_GET_BY_STAFFTYPE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLeavesSType->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetLnoThroughWord(string word, int StaffType, int prd)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_WORD", word);
                        objParams[1] = new SqlParameter("@P_STNO", StaffType);
                        objParams[2] = new SqlParameter("@P_PERIOD", prd);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_LEAVES_GET_LNO_THROUGH_WORD", objParams);
                        //ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_GET_LNO_WORD", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLnoThroughWord->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetLnoWord(string word, int StaffType, int prd)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_WORD", word);
                        objParams[1] = new SqlParameter("@P_STNO", StaffType);
                        objParams[2] = new SqlParameter("@P_PERIOD", prd);
                        //ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_LEAVES_GET_LNO_THROUGH_WORD", objParams);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_GET_LNO_WORD", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLnoWord->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet ChkRecordExits(int Period, int Year, int Stno, int Lno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_PERIOD", Period);
                        objParams[1] = new SqlParameter("@P_YEAR", Year);
                        objParams[2] = new SqlParameter("@P_STNO", Stno);
                        objParams[3] = new SqlParameter("@P_LNO", Lno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_LEAVETRAN_EXISTS", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.AddLeave-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public DataSet GetEmployeeList_Allotment(Leaves objLM)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_DEPT", objLM.DEPTNO);
                        objParams[1] = new SqlParameter("@P_STAFFTYPE", objLM.STNO);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", objLM.COLLEGE_NO);
                        objParams[3] = new SqlParameter("@P_GENDER", objLM.SEX);
                        objParams[4] = new SqlParameter("@P_PERIOD", objLM.PERIOD);
                        objParams[5] = new SqlParameter("@P_SESSION_SRNO", objLM.SESSION_SRNO);
                        objParams[6] = new SqlParameter("@P_LNO", objLM.LNO);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_LEAVES_GET_BY_EMPLOYEES_ALLOTMENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetEmployeeList->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                // public int CreditLeaves(int Period, int Year, int StNo, int Lno, DateTime adt, DateTime fdt, DateTime tdt, string Collcode, int EmpId, int prvlno, int deptno, int leaveno,int staffno,int leave_session_srno)

                public int CreditLeaves_Old(int Period, int Year, int StNo, int Lno, DateTime adt, DateTime fdt, DateTime tdt, string Collcode, int EmpId, int prvlno, int deptno, int leaveno, int college_no, int staffno, int leave_session_srno)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[16];
                        objParams[0] = new SqlParameter("@P_PERIOD", Period);
                        objParams[1] = new SqlParameter("@P_YEAR", Year);
                        objParams[2] = new SqlParameter("@P_STNO", StNo);
                        objParams[3] = new SqlParameter("@P_LNO", Lno);
                        objParams[4] = new SqlParameter("@P_PRVLNO", prvlno);
                        objParams[5] = new SqlParameter("@P_APPDT", adt);
                        objParams[6] = new SqlParameter("@P_FRDT", fdt);
                        objParams[7] = new SqlParameter("@P_TODT", tdt);
                        objParams[8] = new SqlParameter("@P_COLLEGE_CODE", Collcode);
                        objParams[9] = new SqlParameter("@P_EMPID", EmpId);
                        objParams[10] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[11] = new SqlParameter("@P_LEAVENO", leaveno);
                        objParams[12] = new SqlParameter("@P_COLLEGE_NO", college_no);
                        objParams[13] = new SqlParameter("@P_STAFFNO", staffno);
                        objParams[14] = new SqlParameter("@P_LEAVE_SESSION_SRNO", leave_session_srno);
                        objParams[15] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[15].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_LEAVETRAN_CREDITLEAVES", objParams, true);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.CreditLeaves->" + ex.ToString());
                    }
                    return retStatus;

                }
                public int CreditLeaves(Leaves objLM, DataTable dtAppRecord)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_PERIOD", objLM.PERIOD);
                        objParams[1] = new SqlParameter("@P_YEAR", objLM.YEAR);
                        objParams[2] = new SqlParameter("@P_STNO", objLM.STNO);
                        objParams[3] = new SqlParameter("@P_APPDT", objLM.APPDT);
                        objParams[4] = new SqlParameter("@P_FRDT", objLM.FROMDT);
                        objParams[5] = new SqlParameter("@P_TODT", objLM.TODT);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objLM.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_COLLEGE_NO", objLM.COLLEGE_NO);
                        objParams[8] = new SqlParameter("@P_IS_SERVICE_LEAVE", objLM.SERVICE_PERIOD_STATUS);
                        objParams[9] = new SqlParameter("@P_ESTB_LEAVE_CREDIT_RECORD", dtAppRecord);
                        objParams[10] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;
                        //  prvlno,deptno,,staffno,
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_LEAVETRAN_CREDITLEAVES_TEST_NEW_CHECKING_SLOT", objParams, true);
                        //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_LEAVETRAN_CREDITLEAVES", objParams, true);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.CreditLeaves->" + ex.ToString());
                    }
                    return retStatus;

                }
                public int DirectCreditLeaves(int Period, int Year, int StNo, int Lno, DateTime adt, DateTime fdt, DateTime tdt, string Collcode, int EmpId, double noofdays, string creditBy, string remark)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_PERIOD", Period);
                        objParams[1] = new SqlParameter("@P_YEAR", Year);
                        objParams[2] = new SqlParameter("@P_STNO", StNo);
                        objParams[3] = new SqlParameter("@P_LNO", Lno);
                        objParams[4] = new SqlParameter("@P_NOOFDAYS", noofdays);
                        objParams[5] = new SqlParameter("@P_APPDT", adt);
                        objParams[6] = new SqlParameter("@P_FRDT", fdt);
                        objParams[7] = new SqlParameter("@P_TODT", tdt);
                        objParams[8] = new SqlParameter("@P_COLLEGE_CODE", Collcode);
                        objParams[9] = new SqlParameter("@P_EMPID", EmpId);
                        objParams[10] = new SqlParameter("@P_CREDIT_BY", creditBy);
                        objParams[11] = new SqlParameter("@P_REMARK", remark);
                        objParams[12] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_DIRECT_CREDIT_LEAVE_INSERT", objParams, true);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.DirectCreditLeaves->" + ex.ToString());
                    }
                    return retStatus;

                }

                public int DirectCreditUpdate(int eno, double noofdays, string remark)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ENO", eno);
                        objParams[1] = new SqlParameter("@P_NOOFDAYS", noofdays);
                        objParams[2] = new SqlParameter("@P_REMARK", remark);



                        //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_DIRECT_CREDIT_LEAVE_UPDATE", objParams, true);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_DIRECT_CREDIT_LEAVE_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);


                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.DirectCreditUpdate->" + ex.ToString());
                    }
                    return retStatus;

                }


                public int GetStaffType(int EmpID)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_EMPID", EmpID);
                        objParams[1] = new SqlParameter("@P_STNO", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_EMP_STAFFTYPE_BYEMPID", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetStaffType->" + ex.ToString());
                    }
                    return retStatus;

                }

                #endregion

                #region order

                public int ORDER_ENTRY(string ordno, DateTime orddate, int ordtype, string user, int leavetype)
                {
                    //        @P_ORDTRNO,
                    //@P_ORDNO ,
                    //@P_ORDDATE ,
                    //@ORDTYPE ,
                    //@USER_ID ,
                    //@FLAG 	
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_ORDNO", ordno);
                        if (!orddate.Equals(DateTime.MinValue))
                            objParams[1] = new SqlParameter("@P_ORDDATE", orddate);
                        else
                            objParams[1] = new SqlParameter("@P_ORDDATE", DBNull.Value);
                        objParams[2] = new SqlParameter("@ORDTYPE", ordtype);
                        objParams[3] = new SqlParameter("@USER_ID", user);
                        objParams[4] = new SqlParameter("@FLAG", 'P');
                        objParams[5] = new SqlParameter("@P_LEAVETYPE", leavetype);
                        objParams[6] = new SqlParameter("@P_ORDTRNO", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_ORDER_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.order_ENTRY->" + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateAppEntry_order(int ordtrno, int letrno, DateTime orddate)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_LETRNO", letrno);
                        objParams[1] = new SqlParameter("@P_ORDTRNO", ordtrno);
                        objParams[2] = new SqlParameter("@P_ORDDT", orddate);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_APP_ENTRY_ORDERNO_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateAppEntry->" + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateODEntry_order(int ordtrno, int letrno)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ODTRNO", letrno);
                        objParams[1] = new SqlParameter("@P_ORDTRNO", ordtrno);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_OD_PAY_APP_ENTRY_ORDERNO_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateODEntry_order->" + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetLvApplApprovedListforOrder(int college_no, int stno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_COLLEGE_NO", college_no);
                        objParams[1] = new SqlParameter("@P_STNO", stno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_APP_GET_APPROVEDLIST_ORDER", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLvApplApprovedList->" + ex.ToString());

                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }


                public DataSet GetODApplApprovedListforOrder(int college_no, int stno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_COLLEGE_NO", college_no);
                        objParams[1] = new SqlParameter("@P_STNO", stno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_OD_PAY_APP_GET_APPROVEDLIST_ORDER", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetODApplApprovedListforOrder->" + ex.ToString());

                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetOrderNO(int type)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TYPE", type);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_GENERATE_ORDERNO", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLvApplApprovedList->" + ex.ToString());

                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }


                #endregion

                #region BulkUpdate


                public int Add_Update_LEAVE(Leaves objLM, DataTable dtAppRecord)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[10];

                        objParams[0] = new SqlParameter("@P_ESTB_LEAVE_OPENING_RECORD", dtAppRecord);

                        objParams[1] = new SqlParameter("@P_LNO", objLM.LNO);
                        objParams[2] = new SqlParameter("@P_LEAVENO", objLM.LEAVENO);
                        objParams[3] = new SqlParameter("@P_YEAR", objLM.YEAR);

                        objParams[4] = new SqlParameter("@P_PERIOD", objLM.PERIOD);

                        objParams[5] = new SqlParameter("@P_FROMDATE", objLM.FROMDT);
                        objParams[6] = new SqlParameter("@P_TODATE", objLM.TODT);
                        objParams[7] = new SqlParameter("@P_LEAVE_SESSION_SRNO", objLM.SESSION_SRNO);
                        objParams[8] = new SqlParameter("@P_COLLEGE_NO", objLM.COLLEGE_NO);
                        objParams[9] = new SqlParameter("@P_TRANNO", objLM.TRANNO);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAYROLL_LEAVE_INSERT_OPENINGBALANCE", objParams, true);

                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.Add_Update_LEAVE -> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int Add_Update_LEAVE_OLD(Leaves objLM)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_IDNOS", objLM.EMPNO);
                        objParams[1] = new SqlParameter("@P_LEAVES", objLM.NO_DAYS);
                        objParams[2] = new SqlParameter("@P_LNO", objLM.LNO);
                        objParams[3] = new SqlParameter("@P_LEAVENO", objLM.LEAVENO);
                        objParams[4] = new SqlParameter("@P_YEAR", objLM.YEAR);

                        objParams[5] = new SqlParameter("@P_PERIOD", objLM.PERIOD);

                        objParams[6] = new SqlParameter("@P_FROMDATE", objLM.FROMDT);
                        objParams[7] = new SqlParameter("@P_TODATE", objLM.TODT);
                        objParams[8] = new SqlParameter("@P_LEAVE_SESSION_SRNO", objLM.SESSION_SRNO);
                        objParams[9] = new SqlParameter("@P_COLLEGE_NO", objLM.COLLEGE_NO);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAYROLL_LEAVE_INSERT_OPENINGBALANCE", objParams, true);

                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.Add_Update_LEAVE -> " + ex.ToString());
                    }
                    return retStatus;
                }
                //  DataSet ds = objleave.GetEmployeesForBulkOpeningBalance(Convert.ToInt32(ddlStaffType.SelectedValue), Convert.ToInt32(ddlLeave.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue), Convert.ToInt32(ddlState.SelectedValue), Convert.ToInt32(ddlCollege.SelectedValue));               
                //public DataSet GetEmployeesForBulkOpeningBalance(int EmpType, int LeaveType,int Year, int State,int scalestatus)
                // public DataSet GetEmployeesForBulkOpeningBalance(int StaffType,int LeaveType, int Year, int State,int college_no,int session_srno,int deptno)
                public DataSet GetEmployeesForBulkOpeningBalance(Leaves objLM)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_STNO", objLM.STNO);
                        objParams[1] = new SqlParameter("@P_LNO", objLM.LNO);
                        objParams[2] = new SqlParameter("@P_YEAR", objLM.YEAR);
                        objParams[3] = new SqlParameter("@P_STATE", objLM.TRANNO);
                        objParams[4] = new SqlParameter("@P_COLLEGE_NO", objLM.COLLEGE_NO);
                        objParams[5] = new SqlParameter("@P_SESSION_SRNO", objLM.SESSION_SRNO);
                        objParams[6] = new SqlParameter("@P_DEPTNO", objLM.DEPTNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_LEAVE_GET_EMPLOYEES_FOR_BULK_OPENING_BALANCE", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetEmployeesForBulkOpeningBalance->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }

                    return ds;
                }

                #endregion

                #region OnDuty Apply

                public int AddAPP_ENTRY(OD objOD)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[26];
                        objParams[0] = new SqlParameter("@P_EMPNO", objOD.EMPNO);
                        objParams[1] = new SqlParameter("@P_APRDATE", objOD.APRDT);
                        objParams[2] = new SqlParameter("@P_DATE", objOD.DATE);
                        objParams[3] = new SqlParameter("@P_PLACE", objOD.PLACE);
                        //objParams[4] = new SqlParameter("@P_PURPOSE",objOD.PURPOSE);	
                        objParams[4] = new SqlParameter("@P_PURPOSENO", objOD.PURPOSENO);
                        objParams[5] = new SqlParameter("@P_INSTRUCTED_BY", objOD.INSTRBY);
                        objParams[6] = new SqlParameter("@P_OUT_TIME", objOD.OUT_TIME);
                        objParams[7] = new SqlParameter("@P_IN_TIME", objOD.IN_TIME);
                        objParams[8] = new SqlParameter("@P_OUTTIME", objOD.OUTTIME);
                        objParams[9] = new SqlParameter("@P_INTIME", objOD.INTIME);
                        objParams[10] = new SqlParameter("@P_STATUS", objOD.STATUS);
                        objParams[11] = new SqlParameter("@P_COLLEGE_CODE", objOD.COLLEGE_CODE);
                        objParams[12] = new SqlParameter("@P_PAPNO", objOD.PAPNO);
                        if (!objOD.FROMDT.Equals(DateTime.MinValue))
                            objParams[13] = new SqlParameter("@P_FROM_DATE", objOD.FROMDT);
                        else
                            objParams[13] = new SqlParameter("@P_FROM_DATE", DBNull.Value);

                        if (!objOD.TODT.Equals(DateTime.MinValue))
                            objParams[14] = new SqlParameter("@P_TO_DATE", objOD.TODT);
                        else
                            objParams[14] = new SqlParameter("@P_TO_DATE", DBNull.Value);

                        objParams[15] = new SqlParameter("@P_NO_OF_DAYS", objOD.NO_DAYS);
                        if (!objOD.JOINDT.Equals(DateTime.MinValue))
                            objParams[16] = new SqlParameter("@P_JOINDT", objOD.JOINDT);
                        else
                            objParams[16] = new SqlParameter("@P_JOINDT", DBNull.Value);
                        objParams[17] = new SqlParameter("@P_REG_AMT", objOD.REG_AMT);
                        objParams[18] = new SqlParameter("@P_TADA_AMT", objOD.TADA_AMT);
                        //objParams[19] = new SqlParameter("@P_PURPOSENO", objOD.PURPOSENO);
                        objParams[19] = new SqlParameter("@P_PROGRAMNO", objOD.EVENT);
                        objParams[20] = new SqlParameter("@P_TOPIC", objOD.TOPIC);
                        objParams[21] = new SqlParameter("@P_ORGANISEDBY", objOD.ORGANISED_BY);
                        objParams[22] = new SqlParameter("@P_ODTYPE", objOD.ODTYPE);


                        if (!objOD.EVENT_FROMDT.Equals(DateTime.MinValue))
                            objParams[23] = new SqlParameter("@P_EVENT_FRMDT", objOD.EVENT_FROMDT);
                        else
                            objParams[23] = new SqlParameter("@P_EVENT_FRMDT", DBNull.Value);

                        if (!objOD.EVENT_TODT.Equals(DateTime.MinValue))
                            objParams[24] = new SqlParameter("@P_EVENT_TODT", objOD.EVENT_TODT);
                        else
                            objParams[24] = new SqlParameter("@P_EVENT_TODT", DBNull.Value);

                        objParams[25] = new SqlParameter("@P_ODTRNO", SqlDbType.Int);
                        objParams[25].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_OD_PAY_APP_ENTRY_INSERT1", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ODController.AddAPP_ENTRY->" + ex.ToString());
                    }
                    return retStatus;
                }
                public int AddAPP_ENTRY_OD(OD objOD)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[27];
                        objParams[0] = new SqlParameter("@P_EMPNO", objOD.EMPNO);
                        objParams[1] = new SqlParameter("@P_APRDATE", objOD.APRDT);
                        objParams[2] = new SqlParameter("@P_DATE", objOD.DATE);
                        objParams[3] = new SqlParameter("@P_PLACE", objOD.PLACE);
                        //objParams[4] = new SqlParameter("@P_PURPOSE",objOD.PURPOSE);///////////	
                        objParams[4] = new SqlParameter("@P_PURPOSENO", objOD.PURPOSENO);
                        objParams[5] = new SqlParameter("@P_INSTRUCTED_BY", objOD.INSTRBY);
                        objParams[6] = new SqlParameter("@P_OUT_TIME", objOD.OUT_TIME);
                        objParams[7] = new SqlParameter("@P_IN_TIME", objOD.IN_TIME);
                        objParams[8] = new SqlParameter("@P_OUTTIME", objOD.OUTTIME);
                        objParams[9] = new SqlParameter("@P_INTIME", objOD.INTIME);
                        objParams[10] = new SqlParameter("@P_STATUS", objOD.STATUS);
                        objParams[11] = new SqlParameter("@P_COLLEGE_CODE", objOD.COLLEGE_CODE);
                        objParams[12] = new SqlParameter("@P_PAPNO", objOD.PAPNO);
                        if (!objOD.FROMDT.Equals(DateTime.MinValue))
                            objParams[13] = new SqlParameter("@P_FROM_DATE", objOD.FROMDT);
                        else
                            objParams[13] = new SqlParameter("@P_FROM_DATE", DBNull.Value);

                        if (!objOD.TODT.Equals(DateTime.MinValue))
                            objParams[14] = new SqlParameter("@P_TO_DATE", objOD.TODT);
                        else
                            objParams[14] = new SqlParameter("@P_TO_DATE", DBNull.Value);

                        objParams[15] = new SqlParameter("@P_NO_OF_DAYS", objOD.NO_DAYS);
                        if (!objOD.JOINDT.Equals(DateTime.MinValue))
                            objParams[16] = new SqlParameter("@P_JOINDT", objOD.JOINDT);
                        else
                            objParams[16] = new SqlParameter("@P_JOINDT", DBNull.Value);
                        objParams[17] = new SqlParameter("@P_REG_AMT", objOD.REG_AMT);
                        objParams[18] = new SqlParameter("@P_TADA_AMT", objOD.TADA_AMT);
                        //objParams[19] = new SqlParameter("@P_PURPOSENO", objOD.PURPOSENO);
                        //objParams[19] = new SqlParameter("@P_EVENTTYPE", objOD.EVENTTYPE);
                        objParams[19] = new SqlParameter("@P_EVENTTYPE", objOD.EVENT);
                        objParams[20] = new SqlParameter("@P_TOPIC", objOD.TOPIC);
                        objParams[21] = new SqlParameter("@P_ORGANISEDBY", objOD.ORGANISED_BY);
                        objParams[22] = new SqlParameter("@P_ODTYPE", objOD.ODTYPE);


                        if (!objOD.EVENT_FROMDT.Equals(DateTime.MinValue))
                            objParams[23] = new SqlParameter("@P_EVENT_FRMDT", objOD.EVENT_FROMDT);
                        else
                            objParams[23] = new SqlParameter("@P_EVENT_FRMDT", DBNull.Value);

                        if (!objOD.EVENT_TODT.Equals(DateTime.MinValue))
                            objParams[24] = new SqlParameter("@P_EVENT_TODT", objOD.EVENT_TODT);
                        else
                            objParams[24] = new SqlParameter("@P_EVENT_TODT", DBNull.Value);
                        objParams[25] = new SqlParameter("@P_Filename", objOD.FileName);

                        objParams[26] = new SqlParameter("@P_ODTRNO", SqlDbType.Int);
                        objParams[26].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_OD_PAY_APP_ENTRY_INSERT1", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ODController.AddAPP_ENTRY->" + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// Created by Mrunal Bansod 
                /// </summary>
                /// <param name="objOD"></param>
                /// <param name="userno"></param>
                /// <param name="aprdt"></param>
                /// <returns></returns>
                public int AddAPP_ENTRY_ODauth(OD objOD, int userno, DateTime aprdt)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[28];
                        objParams[0] = new SqlParameter("@P_EMPNO", objOD.EMPNO);
                        objParams[1] = new SqlParameter("@P_APRDATE", objOD.APRDT);
                        objParams[2] = new SqlParameter("@P_DATE", objOD.DATE);
                        objParams[3] = new SqlParameter("@P_PLACE", objOD.PLACE);
                        //objParams[4] = new SqlParameter("@P_PURPOSE", objOD.PURPOSE);
                        objParams[4] = new SqlParameter("@P_PURPOSENO", objOD.PURPOSENO);
                        objParams[5] = new SqlParameter("@P_INSTRUCTED_BY", objOD.INSTRBY);
                        objParams[6] = new SqlParameter("@P_OUT_TIME", objOD.OUT_TIME);
                        objParams[7] = new SqlParameter("@P_IN_TIME", objOD.IN_TIME);
                        objParams[8] = new SqlParameter("@P_OUTTIME", objOD.OUTTIME);
                        objParams[9] = new SqlParameter("@P_INTIME", objOD.INTIME);
                        objParams[10] = new SqlParameter("@P_STATUS", objOD.STATUS);
                        objParams[11] = new SqlParameter("@P_COLLEGE_CODE", objOD.COLLEGE_CODE);
                        objParams[12] = new SqlParameter("@P_PAPNO", objOD.PAPNO);
                        if (!objOD.FROMDT.Equals(DateTime.MinValue))
                            objParams[13] = new SqlParameter("@P_FROM_DATE", objOD.FROMDT);
                        else
                            objParams[13] = new SqlParameter("@P_FROM_DATE", DBNull.Value);

                        if (!objOD.TODT.Equals(DateTime.MinValue))
                            objParams[14] = new SqlParameter("@P_TO_DATE", objOD.TODT);
                        else
                            objParams[14] = new SqlParameter("@P_TO_DATE", DBNull.Value);

                        objParams[15] = new SqlParameter("@P_NO_OF_DAYS", objOD.NO_DAYS);
                        if (!objOD.JOINDT.Equals(DateTime.MinValue))
                            objParams[16] = new SqlParameter("@P_JOINDT", objOD.JOINDT);
                        else
                            objParams[16] = new SqlParameter("@P_JOINDT", DBNull.Value);

                        objParams[17] = new SqlParameter("@P_UA_NO", userno);
                        objParams[18] = new SqlParameter("@P_APRDT", aprdt);

                        objParams[19] = new SqlParameter("@P_REG_AMT", objOD.REG_AMT);
                        objParams[20] = new SqlParameter("@P_TADA_AMT", objOD.TADA_AMT);
                        //objParams[21] = new SqlParameter("@P_PURPOSENO", objOD.PURPOSENO);
                        objParams[21] = new SqlParameter("@P_PROGRAMNO", objOD.EVENT);
                        objParams[22] = new SqlParameter("@P_TOPIC", objOD.TOPIC);
                        objParams[23] = new SqlParameter("@P_ORGANISEDBY", objOD.ORGANISED_BY);
                        objParams[24] = new SqlParameter("@P_ODTYPE", objOD.ODTYPE);


                        if (!objOD.EVENT_FROMDT.Equals(DateTime.MinValue))
                            objParams[25] = new SqlParameter("@P_EVENT_FRMDT", objOD.EVENT_FROMDT);
                        else
                            objParams[25] = new SqlParameter("@P_EVENT_FRMDT", DBNull.Value);

                        if (!objOD.EVENT_TODT.Equals(DateTime.MinValue))
                            objParams[26] = new SqlParameter("@P_EVENT_TODT", objOD.EVENT_TODT);
                        else
                            objParams[26] = new SqlParameter("@P_EVENT_TODT", DBNull.Value);

                        objParams[27] = new SqlParameter("@P_ODTRNO", SqlDbType.Int);
                        objParams[27].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_OD_PAY_APP_ENTRY_INSERT2", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ODController.AddAPP_ENTRY_ODauth->" + ex.ToString());
                    }
                    return retStatus;
                }




                public DataSet GetODStatus(int IDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_IDNO", IDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_OD_PAY_APP_APPROVED_GET_BY_NO", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ODController.GetODStatus->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetODApplStatus(int EmpNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_EMPNO", EmpNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_OD_PAY_APP_ENTRY_GET_STATUS", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ODController.GetODApplStatus->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;

                }

                public DataSet GetPAPath_EmpNO(int EMPNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_EMPNO", EMPNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_OD_PAY_PASSING_AUTHORITY_PATH_GET_BY_EMPNO", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ODController.GetPAPath_EmpNO->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }

                    return ds;
                }

                public int DeleteODAppEntry(int odtrNo)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ODTRNO", odtrNo);
                        objParams[1] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        Object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_OD_PAY_APP_ENTRY_DELETE", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ODController.DeleteODAppEntry->" + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateAppEntry(OD objOD)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[26];
                        objParams[0] = new SqlParameter("@P_EMPNO", objOD.EMPNO);
                        objParams[1] = new SqlParameter("@P_APRDATE", objOD.APRDT);
                        objParams[2] = new SqlParameter("@P_DATE", objOD.DATE);
                        objParams[3] = new SqlParameter("@P_PLACE", objOD.PLACE);
                        //objParams[4] = new SqlParameter("@P_PURPOSE", objOD.PURPOSE);
                        objParams[4] = new SqlParameter("@P_PURPOSENO", objOD.PURPOSENO);
                        objParams[5] = new SqlParameter("@P_INSTRUCTED_BY", objOD.INSTRBY);
                        objParams[6] = new SqlParameter("@P_OUT_TIME", objOD.OUT_TIME);
                        objParams[7] = new SqlParameter("@P_IN_TIME", objOD.IN_TIME);
                        objParams[8] = new SqlParameter("@P_OUTTIME", objOD.OUTTIME);
                        objParams[9] = new SqlParameter("@P_INTIME", objOD.INTIME);
                        objParams[10] = new SqlParameter("@P_STATUS", objOD.STATUS);
                        objParams[11] = new SqlParameter("@P_COLLEGE_CODE", objOD.COLLEGE_CODE);
                        //objParams[12] = new SqlParameter("@P_PAPNO", objOD.PAPNO);
                        objParams[12] = new SqlParameter("@P_ODTRNO", objOD.ODTRNO);
                        if (!objOD.FROMDT.Equals(DateTime.MinValue))
                            objParams[13] = new SqlParameter("@P_FROM_DATE", objOD.FROMDT);
                        else
                            objParams[13] = new SqlParameter("@P_FROM_DATE", DBNull.Value);

                        if (!objOD.TODT.Equals(DateTime.MinValue))
                            objParams[14] = new SqlParameter("@P_TO_DATE", objOD.TODT);
                        else
                            objParams[14] = new SqlParameter("@P_TO_DATE", DBNull.Value);

                        objParams[15] = new SqlParameter("@P_NO_OF_DAYS", objOD.NO_DAYS);
                        if (!objOD.JOINDT.Equals(DateTime.MinValue))
                            objParams[16] = new SqlParameter("@P_JOINDT", objOD.JOINDT);
                        else
                            objParams[16] = new SqlParameter("@P_JOINDT", DBNull.Value);

                        objParams[17] = new SqlParameter("@P_REG_AMT", objOD.REG_AMT);
                        objParams[18] = new SqlParameter("@P_TADA_AMT", objOD.TADA_AMT);
                        //objParams[19] = new SqlParameter("@P_PURPOSENO", objOD.PURPOSENO);
                        objParams[19] = new SqlParameter("@P_PROGRAMNO", objOD.EVENT);
                        objParams[20] = new SqlParameter("@P_TOPIC", objOD.TOPIC);
                        objParams[21] = new SqlParameter("@P_ORGANISEDBY", objOD.ORGANISED_BY);
                        objParams[22] = new SqlParameter("@P_ODTYPE", objOD.ODTYPE);

                        if (!objOD.EVENT_FROMDT.Equals(DateTime.MinValue))
                            objParams[23] = new SqlParameter("@P_EVENT_FRMDT", objOD.EVENT_FROMDT);
                        else
                            objParams[23] = new SqlParameter("@P_EVENT_FRMDT", DBNull.Value);

                        if (!objOD.EVENT_TODT.Equals(DateTime.MinValue))
                            objParams[24] = new SqlParameter("@P_EVENT_TODT", objOD.EVENT_TODT);
                        else
                            objParams[24] = new SqlParameter("@P_EVENT_TODT", DBNull.Value);

                        objParams[25] = new SqlParameter("@P_Filename", objOD.FileName);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_OD_APP_ENTRY_INSERT_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ODController.UpdateAppEntry->" + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetODAppEntryByNOForEdit(int ODTRNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ODTRNO", ODTRNO);
                        //objParams[1] = new SqlParameter("@P_ENO", IDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_OD_PAY_APP_ENTRY_GET_BY_NO_TOEDIT", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetODAppEntryByNOForEdit->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion



                #region LeaveSequenceForDeduction
                public DataSet GetAllLeaveSeq(Leaves objLM)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_COLLEGE_NO", objLM.COLLEGE_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_SEQUENCE_GET_BYALL", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetAllLeaveSeq->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int AddLeaveSeq(Leaves objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[12];
                        objparams[0] = new SqlParameter("@P_LEAVE01", objLeave.LEAVE01);
                        objparams[1] = new SqlParameter("@P_LEAVE02", objLeave.LEAVE02);
                        objparams[2] = new SqlParameter("@P_LEAVE03", objLeave.LEAVE03);
                        objparams[3] = new SqlParameter("@P_LEAVE04", objLeave.LEAVE04);
                        objparams[4] = new SqlParameter("@P_LEAVE05", objLeave.LEAVE05);
                        objparams[5] = new SqlParameter("@P_LEAVESEQ", objLeave.LEAVESEQ);
                        objparams[6] = new SqlParameter("@P_STNO", objLeave.STNO);
                        objparams[7] = new SqlParameter("@P_COLLEGE_CODE", objLeave.COLLEGE_CODE);
                        objparams[8] = new SqlParameter("@P_PERIOD", objLeave.PERIOD);
                        objparams[9] = new SqlParameter("@P_APPOINT", objLeave.APPOINT_NO);
                        objparams[10] = new SqlParameter("@P_COLLEGE_NO", objLeave.COLLEGE_NO);
                        objparams[11] = new SqlParameter("@P_LSNO", SqlDbType.Int);
                        objparams[11].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_SEQUENCE_INSERT", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddLeaveSeq->" + ex.ToString());
                    }
                    return retstatus;
                }

                public int UpdateLeaveSeq(Leaves objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[12];
                        objparams[0] = new SqlParameter("@P_LSNO", objLeave.LSNO);
                        objparams[1] = new SqlParameter("@P_LEAVE01", objLeave.LEAVE01);
                        objparams[2] = new SqlParameter("@P_LEAVE02", objLeave.LEAVE02);
                        objparams[3] = new SqlParameter("@P_LEAVE03", objLeave.LEAVE03);
                        objparams[4] = new SqlParameter("@P_LEAVE04", objLeave.LEAVE04);
                        objparams[5] = new SqlParameter("@P_LEAVE05", objLeave.LEAVE05);
                        objparams[6] = new SqlParameter("@P_LEAVESEQ", objLeave.LEAVESEQ);
                        objparams[7] = new SqlParameter("@P_STNO", objLeave.STNO);
                        objparams[8] = new SqlParameter("@P_COLLEGE_CODE", objLeave.COLLEGE_CODE);
                        objparams[9] = new SqlParameter("@P_PERIOD", objLeave.PERIOD);
                        objparams[10] = new SqlParameter("@P_APPOINT", objLeave.APPOINT_NO);
                        objparams[11] = new SqlParameter("@P_COLLEGE_NO", objLeave.COLLEGE_NO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_SEQ_UPDATE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateLeaveSeq->" + ex.ToString());
                    }
                    return retstatus;
                }

                public int DeleteLeaveSeq(int LSNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_LSNO", LSNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_SEQ_DELETE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.DeleteLeaveSeq->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetSingleLeaveSeq(int LSNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_LSNO", LSNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_SEQ_GET_BY_NO", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetSingleLeaveSeq->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                #endregion

                #region ODAPPROVAL
                public DataSet GetPendListforODApproval(int UA_No)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_No);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_OD_PAY_APP_GET_PENDINGLIST", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetPendListforLeaveApproval->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int UpdateODAppPassEntry(int ODTRNO, int UA_NO, string STATUS, string REMARKS, DateTime APRDT, int p)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[6];
                        objparams[0] = new SqlParameter("@P_ODTRNO", ODTRNO);
                        objparams[1] = new SqlParameter("@P_UA_NO", UA_NO);
                        objparams[2] = new SqlParameter("@P_STATUS", STATUS);
                        objparams[3] = new SqlParameter("@P_REMARKS", REMARKS);
                        objparams[4] = new SqlParameter("@P_APRDT", APRDT);
                        objparams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[5].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_OD_PAY_APP_PASS_ENTRY_UPDATE1", objparams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_APP_PASS_ENTRY_UPDATE1", objparams, false) != null)
                        //    retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateLvAplAprStatus->" + ex.ToString());
                    }
                    return retStatus;

                }
                /// <summary>
                /// Mrunal dated 20 june 2012
                /// </summary>
                /// <param name="ODTRNO"></param>
                /// <param name="tadaAmount"></param>
                /// <returns></returns>
                public int UpdateODAppEntry(int ODTRNO, double tadaAmount, double modRegAmt)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[4];
                        objparams[0] = new SqlParameter("@P_ODTRNO", ODTRNO);
                        objparams[1] = new SqlParameter("@P_TADA", tadaAmount);
                        objparams[2] = new SqlParameter("@P_MODI_REG_AMT", modRegAmt);
                        objparams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_OD_PAY_APP_ENTRY_UPDATE", objparams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_APP_PASS_ENTRY_UPDATE1", objparams, false) != null)
                        //    retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateODAppEntry->" + ex.ToString());
                    }
                    return retStatus;

                }
                /// <summary>
                /// Mrunal
                /// </summary>
                /// <param name="ODTRNO"></param>
                /// <param name="STATUS"></param>
                /// <param name="REMARKS"></param>
                /// <param name="APRDT"></param>
                /// <param name="pano"></param>
                /// <param name="pano3"></param>
                /// <returns></returns>
                public int UpdateODAppPassEntryForAccountant(int ODTRNO, string STATUS, string REMARKS, DateTime APRDT, int pano, int pano3)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[7];
                        objparams[0] = new SqlParameter("@P_ODTRNO", ODTRNO);
                        objparams[1] = new SqlParameter("@P_STATUS", STATUS);
                        objparams[2] = new SqlParameter("@P_REMARKS", REMARKS);
                        objparams[3] = new SqlParameter("@P_APRDT", APRDT);
                        objparams[4] = new SqlParameter("@P_PANO", pano);
                        objparams[5] = new SqlParameter("@P_PANO3", pano3);
                        objparams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[6].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_OD_PAY_APP_PASS_ENTRY_UPDATE_FOR_ACC", objparams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_APP_PASS_ENTRY_UPDATE1", objparams, false) != null)
                        //    retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateLvAplAprStatus->" + ex.ToString());
                    }
                    return retStatus;

                }
                /// <summary>
                /// Mrunal
                /// </summary>
                /// <param name="deptno"></param>
                /// <param name="budgallot"></param>
                /// <param name="budgutil"></param>
                /// <param name="budgbal"></param>
                /// <returns></returns>
                public int UpdateODBudget(int deptno, double budgallot, double budgutil, double budgbal)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[5];
                        objparams[0] = new SqlParameter("@P_DEPTNO", deptno);
                        objparams[1] = new SqlParameter("@P_BUDG_ALLOT", budgallot);
                        objparams[2] = new SqlParameter("@P_BUDG_UTIL", budgutil);
                        objparams[3] = new SqlParameter("@P_BUDG_BAL", budgbal);
                        objparams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[4].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_OD_PAY_UPD_OD BUDGET", objparams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);



                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateODBudget->" + ex.ToString());
                    }
                    return retStatus;

                }


                public DataSet GetODApplDetail(int ODTRNO)
                //fetch  Leave Application details of Particular Leave aaplication by Passing LETRNO & its approval status
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ODTRNO", ODTRNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_OD_PAY_ODAPLDTL_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLeaveApplDetail->" + ex.ToString());

                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                /// <summary>
                /// mRUNAL
                /// </summary>
                /// <param name="deptno"></param>
                /// <param name="budgallot"></param>
                /// <param name="budgutil"></param>
                /// <param name="budgbal"></param>
                /// <param name="user"></param>
                /// <param name="dt"></param>
                /// <param name="college_code"></param>
                /// <returns></returns>
                public int AddODBudget(int deptno, double budgallot, double budgutil, double budgbal, string user, DateTime dt, string college_code)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New Title
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[1] = new SqlParameter("@P_BUDGALLOT", budgallot);
                        objParams[2] = new SqlParameter("@P_BUDGUTIL", budgutil);
                        objParams[3] = new SqlParameter("@P_BUDGBAL", budgbal);
                        objParams[4] = new SqlParameter("@P_USER", user);
                        objParams[5] = new SqlParameter("@P_DATE", dt);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", college_code);
                        objParams[7] = new SqlParameter("@P_BUDGNO", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_INS_OD_BUDGET", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.AddODBudget-> " + ex.ToString());
                    }
                    return retStatus;
                }
                /// <summary>
                /// MRUNAL
                /// </summary>
                /// <param name="deptno"></param>
                /// <returns></returns>
                public DataSet GetBudget(int deptno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DEPTNO", deptno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_OD_GET_BUDGET", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetBudget->" + ex.ToString());

                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                /// <summary>
                /// created by Mrunal Bansod 11 oct 2012
                /// </summary>
                /// <param name="UA_No"></param>
                /// <returns></returns>
                public DataSet GetPendListforODApprovalStatusALL(int UA_No)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_No);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_OD_PAY_APP_GET_PENDINGLIST_STATUS_ALL", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetPendListforODApprovalStatusALL->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }


                public DataSet GetPendListforODApprovalStatusParticular(int UA_No, string frmdt, string todt)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_No);
                        objParams[1] = new SqlParameter("@P_FROM_DATE", frmdt);
                        objParams[2] = new SqlParameter("@P_TODATE", todt);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_OD_PAY_APP_GET_PENDINGLIST_STATUS", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetPendListforODApprovalStatusParticular->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion

                #region AttendanceProcess
                public string LeaveAttendanceProcess(string idNos, int userIdno, int month, int year, int deptno, string CollegeCode, int college_no, int stno)
                {
                    string retStatus = string.Empty;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_IDNOS", idNos);
                        objParams[1] = new SqlParameter("@P_USER_IDNO", userIdno);
                        objParams[2] = new SqlParameter("@P_Month", month);
                        objParams[3] = new SqlParameter("@P_Year", year);
                        objParams[4] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[5] = new SqlParameter("@P_COLLEGECODE", CollegeCode);
                        objParams[6] = new SqlParameter("@P_COLLEGE_NO", college_no);
                        objParams[7] = new SqlParameter("@P_STNO", stno);
                        objParams[8] = new SqlParameter("@P_MESSAGE", SqlDbType.NVarChar, 1000);
                        objParams[8].Direction = ParameterDirection.Output;
                        retStatus = Convert.ToString(objSQLHelper.ExecuteNonQuerySP("PKG_PAY_LEAVE_ATTENDANCE_PROCESS", objParams, true));
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToString(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.LeaveAttendanceProcess-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion

                #region LeaveCancel
                //Created By: SWATI GHATE
                //Create Date: 02-08-2014
                //Reason: to Cancel the approved leave of employee
                public DataSet GetAllLeavesByEmp(Leaves objleave)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_EMPNO", objleave.EMPNO);
                        objParams[1] = new SqlParameter("@P_DEPTNO", objleave.DEPTNO);

                        if (!objleave.FROMDT.Equals(DateTime.MinValue))
                            objParams[2] = new SqlParameter("@P_FROM_DATE", objleave.FROMDT);
                        else
                            objParams[2] = new SqlParameter("@P_FROM_DATE", DBNull.Value);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_GET_ALL_LEAVES_BY_EMP", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLnoWord->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }




                public int LeaveCancel(Leaves objleave)
                {
                    int retStatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_EMPNO", objleave.ENO);
                        objParams[1] = new SqlParameter("@P_LNO", objleave.LNO);
                        objParams[2] = new SqlParameter("@P_LETRNO", objleave.LETRNO);
                        objParams[3] = new SqlParameter("@P_FROM_DATE", objleave.FROMDT);
                        objParams[4] = new SqlParameter("@P_TODATE", objleave.TODT);
                        objParams[5] = new SqlParameter("@P_LVCANCELRMARK", objleave.LVCANCELRMARK);
                        objParams[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_APP_CANCEL", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);


                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.DirectCreditUpdate->" + ex.ToString());
                    }
                    return retStatus;

                }

                #endregion


                #region CarryForwordLeave
                public int AddCarryForwordLeave(Leaves objLM, int exist_period, int exist_year, int PendingCarry)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[12];

                        objParams[0] = new SqlParameter("@P_PERIOD", objLM.PERIOD);
                        objParams[1] = new SqlParameter("@P_YEAR", objLM.YEAR);
                        objParams[2] = new SqlParameter("@P_FDT", objLM.FROMDT);
                        objParams[3] = new SqlParameter("@P_TDT", objLM.TODT);
                        objParams[4] = new SqlParameter("@P_EXIST_PERIOD", exist_period);
                        objParams[5] = new SqlParameter("@P_EXIST_YEAR", exist_year);
                        objParams[6] = new SqlParameter("@P_STNO", objLM.STNO);
                        objParams[7] = new SqlParameter("@P_LEAVENO", objLM.LEAVENO);
                        objParams[8] = new SqlParameter("@P_COLLEGE_NO", objLM.COLLEGE_NO);
                        objParams[9] = new SqlParameter("@P_TRANNO", objLM.TRANNO);
                        objParams[10] = new SqlParameter("@P_TYPETRANNO", PendingCarry);
                        objParams[11] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_CARRY_FORWARD_TO_NEW_SESSION", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeaveController.AddCarryForwordLeave-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetCarryForwordLeave(Leaves objLM, int exist_period, int exist_year, int PendingCarry)
                {

                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_PERIOD", objLM.PERIOD);
                        objParams[1] = new SqlParameter("@P_YEAR", objLM.YEAR);
                        objParams[2] = new SqlParameter("@P_FDT", objLM.FROMDT);
                        objParams[3] = new SqlParameter("@P_TDT", objLM.TODT);
                        objParams[4] = new SqlParameter("@P_EXIST_PERIOD", exist_period);
                        objParams[5] = new SqlParameter("@P_EXIST_YEAR", exist_year);
                        objParams[6] = new SqlParameter("@P_STNO", objLM.STNO);
                        objParams[7] = new SqlParameter("@P_LEAVENO", objLM.LEAVENO);
                        objParams[8] = new SqlParameter("@P_COLLEGE_NO", objLM.COLLEGE_NO);
                        objParams[9] = new SqlParameter("@P_TRANNO", objLM.TRANNO);
                        objParams[10] = new SqlParameter("@P_TYPETRANNO", PendingCarry);
                        objParams[11] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_CARRY_FORWARD_TO_NEW_SESSION", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetCarryForwordLeave-> " + ex.ToString());
                    }
                    return ds;
                }


                #endregion

                #region MonthlyAttendance
                /// <summary>
                /// Createwd By :swati ghate
                /// Date: 17-04-2015
                /// TO return Attenda Month name To avoid Error of table name of corresponding month
                /// </summary>
                /// <param name="ToDt"></param>
                /// <returns></returns>
                public int GetAttendMonyear(DateTime ToDt)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_TODATE", ToDt);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_PAY_GET_MONYEAR", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetAttendMonyear->" + ex.ToString());
                    }
                    return retStatus;

                }

                public int CheckAttendanceProcess(Leaves objLM)
                {
                    int ret = 99;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_FSDATE", objLM.FROMDT);
                        objParams[1] = new SqlParameter("@P_FEDATE", objLM.TODT);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", objLM.COLLEGE_NO);
                        objParams[3] = new SqlParameter("@P_STNO", objLM.STNO);
                        objParams[4] = new SqlParameter("@P_DEPTNO", objLM.DEPTNO);

                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;
                        // ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_ESTB_CHECK_ATTENDANCE", objParams);
                        ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PAYROLL_ESTB_CHECK_ATTENDANCE_PROCESS", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        return ret;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.CheckAttendance->" + ex.ToString());
                    }

                    return ret;
                }
                #endregion
                #region AttendanceTransfer
                /// <summary>
                /// Created By: Swati Ghate
                /// Dtae: 21-04-2015
                /// To display All LWP days after Attendace process 
                /// </summary>
                /// <param name="frmdate"></param>
                /// <param name="todate"></param>
                /// <returns></returns>
                //public DataSet GetAttendanceRecord(int deptno, DateTime frmdate, DateTime todate, int college_no, string report_type)
                public DataSet GetAttendanceRecord(int stno, string frmdate, string todate, int college_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        //@P_DEPT_NO                        
                        objParams[0] = new SqlParameter("@P_COLLEGE_NO", college_no);
                        objParams[1] = new SqlParameter("@P_STNO", stno);
                        objParams[2] = new SqlParameter("@P_FSDATE", frmdate);
                        objParams[3] = new SqlParameter("@P_FEDATE", todate);

                        //ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_PAY_FINAL_SALARY_REPORT", objParams);
                        ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_PAY_FINAL_TRANSFER_ATTENDANCE_SALARY_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetAttendanceRecord->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                //[PAYROLL_ESTB_CHECK_ATTENDANCE]
                /// <summary>
                /// Created By: Swati Ghate
                /// Dtae: 21-04-2015
                /// To check Attendace process done or not for selected month
                /// </summary>
                /// <param name="frmdate"></param>
                /// <param name="todate"></param>
                /// <returns></returns>
                public int CheckAttendance(DateTime frmdate, DateTime todate)
                {
                    int ret = 99;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_FSDATE", frmdate);
                        objParams[1] = new SqlParameter("@P_FEDATE", todate);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        // ds = objSQLHelper.ExecuteDataSetSP("PAYROLL_ESTB_CHECK_ATTENDANCE", objParams);
                        ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PAYROLL_ESTB_CHECK_ATTENDANCE", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        return ret;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.CheckAttendance->" + ex.ToString());
                    }

                    return ret;
                }
                public int UpdateAttendance(int idno, double leaves, DateTime fdt, DateTime tdt, int colllege_no)
                {
                    int retstatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_LEAVES", leaves);
                        objParams[2] = new SqlParameter("@P_FDT", fdt);
                        objParams[3] = new SqlParameter("@P_TDT", tdt);
                        objParams[4] = new SqlParameter("@P_COLLEGE_NO", colllege_no);
                        objParams[5] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));

                        // ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PAYROLL_ESTB_UPDATE_ABSENT_DAYS", objParams, true));
                        if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_ESTB_UPDATE_ABSENT_DAYS", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        return retstatus;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.CheckAttendance->" + ex.ToString());
                    }

                    return retstatus;
                }
                #endregion
                public DataSet GetEmployeebtn4to6(string mon, int stno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_TABLE", mon);
                        objParams[1] = new SqlParameter("@P_STNO", stno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_TOTWKHR_HOLI_SUNDAYS_BET_4TO6", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetODAppEntryByNOForEdit->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetEmployeegrt6(string mon, int stno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_TABLE", mon);
                        objParams[1] = new SqlParameter("@P_STNO", stno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_TOTWKHR_HOLI_SUNDAYS_GREAT6", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetEmployeegrt6->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetEmployeeLateBy10min(string mon, int stno, int deptno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_TABLE", mon);
                        objParams[1] = new SqlParameter("@P_STNO", stno);
                        objParams[2] = new SqlParameter("@P_DEPTNO", deptno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_LEAVE_GET_EMP_LATEBY_10MIN", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetEmployeeLateBy10min->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetLateEmpForModify(string mon)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TABLE", mon);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_LEAVE_GET_EMP_LATEBY_10MIN", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetEmployeeLateBy10min->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetEmployeeLateByHalfHr(string mon, int stno, int deptno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_TABLE", mon);
                        objParams[1] = new SqlParameter("@P_STNO", stno);
                        objParams[2] = new SqlParameter("@P_DEPTNO", deptno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_LEAVE_GET_EMP_LATEBY_HALFHOUR", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetEmployeegrt6->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int UpdateCreditDeductLeave(Leaves objLeaves)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[14];
                        objParams[0] = new SqlParameter("@P_LETRNO", objLeaves.LETRNO);


                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_APP_ENTRY_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateAppEntry->" + ex.ToString());
                    }
                    return retStatus;
                }

                public int CreditLeavesForWork(int Period, int Year, int StNo, int Lno, DateTime adt, DateTime fdt, DateTime tdt, string Collcode, int EmpId, double leaves, string creditby)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_PERIOD", Period);
                        objParams[1] = new SqlParameter("@P_YEAR", Year);
                        objParams[2] = new SqlParameter("@P_STNO", StNo);
                        objParams[3] = new SqlParameter("@P_LNO", Lno);
                        objParams[4] = new SqlParameter("@P_APPDT", adt);
                        objParams[5] = new SqlParameter("@P_FRDT", fdt);
                        objParams[6] = new SqlParameter("@P_TODT", tdt);
                        objParams[7] = new SqlParameter("@P_COLLEGE_CODE", Collcode);
                        objParams[8] = new SqlParameter("@P_EMPID", EmpId);
                        objParams[9] = new SqlParameter("@P_LEAVES", leaves);
                        objParams[10] = new SqlParameter("@P_CREDIT_BY", creditby);

                        objParams[11] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_LEAVETRAN_CREDITLEAVES_FORWORK", objParams, true);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.CreditLeaves->" + ex.ToString());
                    }
                    return retStatus;

                }


                public DataSet GetEarlyGoingforMody(int mon, int stno, int deptno, string entry)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_MONTH", mon);
                        objParams[1] = new SqlParameter("@P_STNO", stno);
                        objParams[2] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[3] = new SqlParameter("@P_STATUS", entry);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_GET_EARLY_GOING_MODI", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetEarlyGoingforMody->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }


                public DataSet GetLateComingforMody(int mon, int stno, int deptno, string entry)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_MONTH", mon);
                        objParams[1] = new SqlParameter("@P_STNO", stno);
                        objParams[2] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[3] = new SqlParameter("@P_STATUS", entry);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_GET_LATE_COMING_MODI", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLateComingforMody->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int AddODPurpose(string purpose, string college_code)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New Title
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_PURPOSE", purpose);


                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", college_code);
                        objParams[2] = new SqlParameter("@P_PURPOSENO", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_INS_OD_PURPOSE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.AddODPurpose-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateODPurpose(int purposeNO, string purpose)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_PURPOSENO", purposeNO);
                        objParams[1] = new SqlParameter("@P_PURPOSE", purpose);


                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_UPD_OD_PURPOSE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.UpdateODPurpose-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet AllODPurpose()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_GET_OD_PURPOSE", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AllODPurpose->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public int DeleteODPurpose(int PNo)  //Added by Saket Singh on 14-Dec-2016
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_PURPOSENO", PNo);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_DELETE_OD_PURPOSE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.DeleteODPurpose->" + ex.ToString());
                    }
                    return retstatus;
                }

                #region Program Type
                // To insert New Leave Type

                // Modified By: Swati Ghate
                //Reason: To pass P_LEAVENO parameter
                //Date:11/11/2014
                public int AddProgramType(Leaves objPType)
                {
                    int retstatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@PROGRAM_TYPE", objPType.PROGRAM_TYPE);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", objPType.COLLEGE_CODE);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_PROGRAMTYPE_INSERT", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeaveController.AddProgramType-> " + ex.ToString());
                    }
                    return retstatus;
                }

                // To update Existing Leave Type
                public int UpdateProgramType(Leaves objPType)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@PROGRAM_TYPE", objPType.PROGRAM_TYPE);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", objPType.COLLEGE_CODE);
                        objParams[2] = new SqlParameter("@PROGRAM_NO", objPType.PROGRAM_NO);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_PROGRAMTYPE_UPDATE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeaveController.UpdateProgramType-> " + ex.ToString());
                    }
                    return retstatus;
                }

                // To Delete Leave Type
                public int DeleteProgramType(int LNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@PROGRAM_NO", LNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_PROGRAMTYPE_DELETE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeaveController.DeleteProgramType-> " + ex.ToString());
                    }
                    return retstatus;
                }

                // To Fetch all Leave Types 
                public DataSet GetAllProgramType()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_PROGRAMTYPE_GETALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetAllProgramType-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }

                    return ds;
                }

                // To Fetch single Leave Types detail by passing Leave No
                public DataSet GetSingleProgramType(int PNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@PROGRAM_NO", PNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_PROGRAMTYPE_GETSINGLE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetSingleProgramType->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion //end region Program type
                #region AssignShift

                //Added by Abhishek madndal
                // To Fetch all the week days
                public DataSet RetrieveWeekDays()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = new SqlParameter[0];
                        //objparams[0] = new SqlParameter("@P_SRNO", srno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_CHANGE_SHIFT_GETALL_WEEKDAYS", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.RetrieveAllEmployee->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }


                // To Fetch all existing employee details college wise
                public DataSet RetrieveAllEmployee(int collegeno, int deptno, int stno, int tranno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = new SqlParameter[4];
                        objparams[0] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        objparams[1] = new SqlParameter("@P_DEPTNO", deptno);
                        objparams[2] = new SqlParameter("@P_STNO", stno);
                        objparams[3] = new SqlParameter("@P_TRANNO", tranno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_EMPLOYEE_GETALL_SHIFT", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.RetrieveAllEmployee->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }


                // To assign shift to employee
                public int AddAssignShift(Shifts objshifts, DataTable dtEmpRecord)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[6];
                        objparams[0] = new SqlParameter("@P_ESTB_EMP_RECORD", dtEmpRecord);

                        if (!objshifts.FROMDATE.Equals(DateTime.MinValue))
                            objparams[1] = new SqlParameter("@P_FROMDATE", objshifts.FROMDATE);
                        else
                            objparams[1] = new SqlParameter("@P_DT", DBNull.Value);

                        if (!objshifts.TODATE.Equals(DateTime.MinValue))
                            objparams[2] = new SqlParameter("@P_TODATE", objshifts.TODATE);
                        else
                            objparams[2] = new SqlParameter("@P_DT", DBNull.Value);

                        objparams[3] = new SqlParameter("@P_SHIFTNO", objshifts.SHIFTNO);
                        objparams[4] = new SqlParameter("@P_SHIFTNAME", objshifts.SHIFTNAME);
                        objparams[5] = new SqlParameter("@P_COLLEGE_CODE", objshifts.COLLEGE_CODE);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_ASSIGNSHIFT_INSERT", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddAssignShift->" + ex.ToString());
                    }
                    return retstatus;
                }
                public int AddAssignShift_continue(Shifts objshifts, char status)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[6];
                        objparams[0] = new SqlParameter("@P_IDNO", objshifts.IDNO);

                        if (!objshifts.DATE.Equals(DateTime.MinValue))
                            objparams[1] = new SqlParameter("@P_DT", objshifts.DATE);
                        else
                            objparams[1] = new SqlParameter("@P_DT", DBNull.Value);

                        objparams[2] = new SqlParameter("@P_SHIFTNO", objshifts.SHIFTNO);
                        objparams[3] = new SqlParameter("@P_SHIFTNAME", objshifts.SHIFTNAME);
                        objparams[4] = new SqlParameter("@P_COLLEGE_CODE", objshifts.COLLEGE_CODE);
                        objparams[5] = new SqlParameter("@P_CONTINUE_STATUS", status);


                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_ASSIGNSHIFT_INSERT_STATUS", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddAssignShift->" + ex.ToString());
                    }
                    return retstatus;
                }



                //To change shift time to employee
                //public int AddChangeShiftTime(Shifts objshifts, DataTable dtDays, DataTable dtEmpRecord)
                //{
                //    int retstatus = 0;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objparams = null;
                //        objparams = new SqlParameter[6];
                //        objparams[0] = new SqlParameter("@P_INTIME", objshifts.INTIME1);
                //        objparams[1] = new SqlParameter("@P_OUTTIME", objshifts.OUTTIME1);
                //        objparams[2] = new SqlParameter("@P_FROM_DATE", objshifts.FROMDATE);
                //        objparams[3] = new SqlParameter("@P_TO_DATE", objshifts.TODATE);
                //        objparams[4] = new SqlParameter("@P_ESTB_DAY_RECORD", dtDays);
                //        objparams[5] = new SqlParameter("@P_ESTB_EMP_RECORD", dtEmpRecord);
                //        //objparams[6] = new SqlParameter("@P_COLLEGE_CODE", objshifts.COLLEGE_CODE);
                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_INS_CHANGE_SHIFT_TIME_BYDAYS", objparams, false) != null)
                //            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //    }
                //    catch (Exception ex)
                //    {
                //        retstatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddChangeShiftTime->" + ex.ToString());
                //    }
                //    return retstatus;
                //}

                public int AddChangeShiftTime(Shifts objshifts, DataTable dt)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[6];
                        objparams[0] = new SqlParameter("@P_ESTB_EMP_RECORD", dt);

                        if (!objshifts.FROMDATE.Equals(DateTime.MinValue))
                            objparams[1] = new SqlParameter("@P_FROMDATE", objshifts.FROMDATE);
                        else
                            objparams[1] = new SqlParameter("@P_FROMDATE", DBNull.Value);

                        if (!objshifts.TODATE.Equals(DateTime.MinValue))
                            objparams[2] = new SqlParameter("@P_TODATE", objshifts.TODATE);
                        else
                            objparams[2] = new SqlParameter("@P_TODATE", DBNull.Value);

                        objparams[3] = new SqlParameter("@P_INTIME", objshifts.INTIME1);
                        objparams[4] = new SqlParameter("@P_OUTTIME", objshifts.OUTTIME1);
                        objparams[5] = new SqlParameter("@P_COLLEGE_CODE", objshifts.COLLEGE_CODE);


                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_INS_CHANGE_SHIFT_TIME", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddChangeShiftTime->" + ex.ToString());
                    }
                    return retstatus;
                }







                //To change shift time to employee
                public int AddChangeShiftTime_backup(Shifts objshifts, DataTable dtDays)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[6];
                        objparams[0] = new SqlParameter("@P_IDNO", objshifts.IDNO);

                        if (!objshifts.DATE.Equals(DateTime.MinValue))
                            objparams[1] = new SqlParameter("@P_DT", objshifts.DATE);
                        else
                            objparams[1] = new SqlParameter("@P_DT", DBNull.Value);

                        objparams[2] = new SqlParameter("@P_INTIME", objshifts.INTIME1);
                        objparams[3] = new SqlParameter("@P_OUTTIME", objshifts.OUTTIME1);
                        objparams[4] = new SqlParameter("@P_ESTB_DAY_RECORD", dtDays);
                        objparams[5] = new SqlParameter("@P_COLLEGE_CODE", objshifts.COLLEGE_CODE);


                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_INS_CHANGE_SHIFT_TIME", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddChangeShiftTime->" + ex.ToString());
                    }
                    return retstatus;
                }

                //To entry in detention table
                public int AddDetentionEntry(Shifts objshifts)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[4];
                        objparams[0] = new SqlParameter("@P_IDNO", objshifts.IDNO);

                        if (!objshifts.DATE.Equals(DateTime.MinValue))
                            objparams[1] = new SqlParameter("@P_DT", objshifts.DATE);
                        else
                            objparams[1] = new SqlParameter("@P_DT", DBNull.Value);

                        objparams[2] = new SqlParameter("@P_COLLEGE_CODE", objshifts.COLLEGE_CODE);
                        objparams[3] = new SqlParameter("@P_HTNO", objshifts.HTNO);


                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_INS_DETENTION_ENTRY", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddDetentionEntry->" + ex.ToString());
                    }
                    return retstatus;
                }

                public int UpdateDefaultShift(Shifts objshifts)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", objshifts.IDNO);
                        objParams[1] = new SqlParameter("@P_SHIFTNO", objshifts.SHIFTNO);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_UPD_DEFAULT_SHIFT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.UpdateODPurpose-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion

                public int AddLeaveShortName(Leaves objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_LEAVENAME", objLeave.LEAVENAMESHRT);
                        objParams[1] = new SqlParameter("@P_LEAVESHORTNAME", objLeave.SHORTNAME);
                        objParams[2] = new SqlParameter("@P_MAXDAYS", objLeave.MAXDAYS);
                        objParams[3] = new SqlParameter("@P_YEARLY", objLeave.YEARLYPRD);
                        objParams[4] = new SqlParameter("@P_IsCertificate", objLeave.IsCertificate);
                        objParams[5] = new SqlParameter("@P_IsPayLeave", objLeave.IsPayLeave);
                        objParams[6] = new SqlParameter("@P_IsValidatedays", objLeave.IsValidatedays);
                        objParams[7] = new SqlParameter("@P_ISCOMPOFF", objLeave.IsCompOff);
                        objParams[8] = new SqlParameter("@P_No_ofdayscertificate", objLeave.NO_DAYS);
                        objParams[9] = new SqlParameter("@P_LEAVENO", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_LEAVE_SHORT_NAME", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddHolidayType->" + ex.ToString());
                    }
                    return retstatus;
                }

                public int UpdateLeaveShortName(Leaves objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_LEAVENO", objLeave.LEAVENO);
                        objParams[1] = new SqlParameter("@P_LEAVENAME", objLeave.LEAVENAMESHRT);
                        objParams[2] = new SqlParameter("@P_LEAVESHORTNAME", objLeave.SHORTNAME);
                        objParams[3] = new SqlParameter("@P_MAXDAYS", objLeave.MAXDAYS);
                        objParams[4] = new SqlParameter("@P_YEARS", objLeave.YEARLYPRD);
                        objParams[5] = new SqlParameter("@P_IsCertificate", objLeave.IsCertificate);
                        objParams[6] = new SqlParameter("@P_IsPayLeave", objLeave.IsPayLeave);
                        objParams[7] = new SqlParameter("@P_IsValidatedays", objLeave.IsValidatedays);
                        objParams[8] = new SqlParameter("@P_ISCOMPOFF", objLeave.IsCompOff);
                        objParams[9] = new SqlParameter("@P_No_ofdayscertificate", objLeave.NO_DAYS);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_LEAVE_SHORT_NAME_UPDATE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateHolidayType->" + ex.ToString());
                    }
                    return retstatus;
                }
                public DataSet GetSingLeaveShortName(int levno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null; ;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_LEAVENO", levno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_LEAVE_SHORT_NAME_GET_BY_NO", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetSingHolidayType->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                // To Fetch all existing LEAVE NAME
                public DataSet GetAllLeaveShortName()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_LEAVE_SHORT_NAME_GET_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetAllHolidayType->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                //To Delete existing Holiday type
                public int DeleteLeaveShortName(int lvno)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_LEAVENO", lvno);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_LEAVE_SHORT_NAME_DELETE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.DeleteHolidaytype->" + ex.ToString());
                    }
                    return retstatus;
                }
                public DataSet GetLeaveListDetails()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_LEAVE_GET_LEAVE_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetAllHolidayType->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                // To Fetch all Leave Types 
                public DataSet GetAllLeaveName()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_LEAVE_GET_LEAVE_DETAILS", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetAllLeave-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }

                    return ds;
                }

                //Add Leave Name short

                public int AddLeaveNameYearly(Leaves objLeave, int selectval)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_STNO", objLeave.STNO);

                        objParams[1] = new SqlParameter("@P_LVNO", selectval);
                        objParams[2] = new SqlParameter("@P_MAXDAY", objLeave.MAX);
                        objParams[3] = new SqlParameter("@P_STATUS", objLeave.PERIOD);
                        objParams[4] = new SqlParameter("@P_LNNAME", objLeave.LEAVENAME);

                        objParams[5] = new SqlParameter("@P_LNNO", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_LEAVE_NAME_INSERT_LEAVE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddHolidayType->" + ex.ToString());
                    }
                    return retstatus;
                }

                // ----------------------------Delete--------------------------------------------
                public int DeleteInsertedLeave(Leaves objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_LNO", objLeave.LNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_DELETE_UNCHECKED_LEAVE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.UpdateLeave-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public int UpdateInsertedLeave(Leaves objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_LNO", objLeave.LNO);
                        objParams[1] = new SqlParameter("@P_STNO", objLeave.STNO);
                        objParams[2] = new SqlParameter("@P_LEAVE", objLeave.LEAVE);
                        objParams[3] = new SqlParameter("@P_LEAVENAME", objLeave.LEAVENAME);
                        objParams[4] = new SqlParameter("@P_MAX", objLeave.MAX);
                        objParams[5] = new SqlParameter("@P_CARRY", objLeave.CARRY);
                        objParams[6] = new SqlParameter("@P_PERIOD", objLeave.PERIOD);
                        objParams[7] = new SqlParameter("@P_SEX", objLeave.SEX);
                        objParams[8] = new SqlParameter("@P_CAL", objLeave.CAL);
                        objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objLeave.COLLEGE_CODE);
                        objParams[10] = new SqlParameter("@P_ALLOWEDDAYS", objLeave.DAYS);
                        objParams[11] = new SqlParameter("@P_CALHOLIDAY", objLeave.CALHOLIDAY);
                        objParams[12] = new SqlParameter("@P_STATUS", objLeave.STATUS);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_UPDATE_INSERT", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.UpdateLeave-> " + ex.ToString());
                    }
                    return retstatus;
                }
                //UPDATE LEAVE 
                public int UpdateLeaveNameYearly(Leaves objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_STNO", objLeave.STNO);

                        objParams[1] = new SqlParameter("@P_LVNO", objLeave.LNO);
                        objParams[2] = new SqlParameter("@P_MAXDAYS", objLeave.MAX);
                        objParams[3] = new SqlParameter("@P_STATUS", objLeave.PERIOD);
                        objParams[4] = new SqlParameter("@P_LVNAME", objLeave.LEAVENAME);

                        //objParams[5] = new SqlParameter("@P_STLVN0", SqlDbType.Int);
                        //objParams[5].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_LEAVE_NAME_UPDATE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddHolidayType->" + ex.ToString());
                    }
                    return retstatus;
                }



                //SQLDATA READER LEAVE NAME

                public SqlDataReader GetLeaveNo(int sno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SNO", sno);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_PAY_GET_LEAVE_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.PayController.GetAppointno-> " + ex.ToString());
                    }
                    return dr;
                }


                //TO DELETE LEAVE NAME
                public int DeleteLeavePayName(int stno)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_STNO", stno);
                        //objparams[1] = new SqlParameter("@P_LVNO", objLeave.LNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_LEAVE_DELETE_STAFF", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.DeleteHolidaytype->" + ex.ToString());
                    }
                    return retstatus;
                }
                public int DeleteLeaveFromPayLeave(int stno)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_STNO", stno);
                        //objparams[1] = new SqlParameter("@P_LNO", objLeave.LNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_LEAVE_DELETE_FROM_PAY_LEAVE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.DeleteHolidaytype->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetHolidaydt(DateTime Frdate, DateTime Todate, int stno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_FROMDT", Frdate);
                        objParams[1] = new SqlParameter("@P_TODT", Todate);
                        objParams[2] = new SqlParameter("@P_STNO", stno);
                        //objParams[2] = new SqlParameter("@P_LEAVETYPE", leavetype);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_GET_HOLIDAY_BYDATE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetNoofdays->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }




                public DataSet GetStatusEmpList(string frmdt, string todt, int statusno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_FROM_DATE", frmdt);
                        objParams[1] = new SqlParameter("@P_TODATE", todt);
                        objParams[2] = new SqlParameter("@P_STATUSNO", statusno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_EMP_STATUS", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetStatusEmpList->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                #region ThumbProblem

                /// <summary>
                /// Added by Mrunal Bansod on dated 16/10/2012 
                /// used to get employee list whi forgot to punch
                /// </summary>
                /// <param name="frmdt"></param>
                /// <param name="todt"></param>
                /// <param name="deptno"></param>
                /// <param name="designo"></param>
                /// <returns></returns>
                public DataSet GetThumbPrblmList(string frmdt, string todt, int deptno, int collegeno, int stno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_FROM_DATE", frmdt);
                        objParams[1] = new SqlParameter("@P_TO_DATE", todt);
                        objParams[2] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        objParams[4] = new SqlParameter("@P_STNO", stno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_THUMB_PROBLEM_LEAVE", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetThumbPrblmList->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }


                /// <summary>
                /// Added by Mrunal Bansod on dated 5 Nov 2012
                /// </summary>
                /// <param name="frmdt"></param>
                /// <param name="todt"></param>
                /// <param name="deptno"></param>
                /// <returns></returns>
                public DataSet GetThumbPrblemAllowedEmpList(string frmdt, string todt, int deptno, int collegeno, int stno,int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_FROMDATE", frmdt);
                        objParams[1] = new SqlParameter("@P_TODATE", todt);
                        objParams[2] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        objParams[4] = new SqlParameter("@P_STNO", stno);
                        objParams[5] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_ALLOWED_THUMB_PROBLEM", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetThumbPrblemAllowedEmpList->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                /// <summary>
                /// Added by Mrunal Bansod on dated 17/10/2012 
                /// to save thumb prlblm allow data in table
                /// </summary>
                /// <param name="objLeave"></param>
                /// <returns></returns>
                public int AddThumbPrblm(Leaves objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[11];
                        objparams[0] = new SqlParameter("@P_IDNO", objLeave.EMPNO);

                        if (!objLeave.DATE.Equals(DateTime.MinValue))
                            objparams[1] = new SqlParameter("@P_DT", objLeave.DATE);
                        else
                            objparams[1] = new SqlParameter("@P_DT", DBNull.Value);

                        objparams[2] = new SqlParameter("@P_EMPNAME", objLeave.EMPNAME);
                        objparams[3] = new SqlParameter("@P_INTIME", objLeave.INTIME);
                        objparams[4] = new SqlParameter("@P_OUTTIME", objLeave.OUTTIME);
                        objparams[5] = new SqlParameter("@P_WTNO", objLeave.WTNO);
                        objparams[6] = new SqlParameter("@P_STATUS", objLeave.STATUS);
                        objparams[7] = new SqlParameter("@P_COLLEGE_CODE", objLeave.COLLEGE_CODE);
                        objparams[8] = new SqlParameter("@P_REASON_NO", objLeave.RESNO);
                        objparams[9] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objparams[10] = new SqlParameter("@P_CREATEDBY", objLeave.UANO);
                        //@P_REASON_NO

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_THUMBPRBLM_ALLOW_INSERT", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddThumbPrblm->" + ex.ToString());
                    }
                    return retstatus;
                }

                #endregion

                #region NR_Rercord
                /// <summary>
                /// To Display IN & OUT both not found record
                /// </summary>
                /// <param name="frmdt"></param>
                /// <param name="todt"></param>
                /// <param name="deptno"></param>
                /// <param name="collegeno"></param>
                /// <param name="stno"></param>
                /// <returns></returns>
                public DataSet GetThumbPrblmList_NonRegistered(string frmdt, string todt, int deptno, int collegeno, int stno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_FROM_DATE", frmdt);
                        objParams[1] = new SqlParameter("@P_TO_DATE", todt);
                        objParams[2] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        objParams[4] = new SqlParameter("@P_STNO", stno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_THUMB_PROBLEM_LEAVE_NR", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetThumbPrblmList->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }


                /// <summary>
                /// PURPOSE:TO DISPLAY ALL NON REGISTERD RECROD FOR MODIFICATION
                /// ADDED BY:SWATI GHATE
                /// DATE:2-7-2016
                /// </summary>
                /// <param name="frmdt"></param>
                /// <param name="todt"></param>
                /// <param name="deptno"></param>
                /// <param name="collegeno"></param>
                /// <returns></returns>
                public DataSet GetThumbPrblemAllowedEmpList_NonRegistered(string frmdt, string todt, int deptno, int collegeno, int stno,int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_FROMDATE", frmdt);
                        objParams[1] = new SqlParameter("@P_TODATE", todt);
                        objParams[2] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        objParams[4] = new SqlParameter("@P_STNO", stno);
                        objParams[5] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_ALLOWED_THUMB_PROBLEM_NR", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetThumbPrblemAllowedEmpList->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                /// <summary>
                /// Added by: SWATI GHATE
                /// DATE:2-7-2016
                /// PURPOSE: TO SAVE ALL NR RECORDS
                /// </summary>
                /// <param name="objLeave"></param>
                /// <returns></returns>
                public int AddThumbPrblm_NonRegistered(Leaves objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[11];
                        objparams[0] = new SqlParameter("@P_IDNO", objLeave.EMPNO);

                        if (!objLeave.DATE.Equals(DateTime.MinValue))
                            objparams[1] = new SqlParameter("@P_DT", objLeave.DATE);
                        else
                            objparams[1] = new SqlParameter("@P_DT", DBNull.Value);

                        objparams[2] = new SqlParameter("@P_EMPNAME", objLeave.EMPNAME);
                        objparams[3] = new SqlParameter("@P_INTIME", objLeave.INTIME);
                        objparams[4] = new SqlParameter("@P_OUTTIME", objLeave.OUTTIME);
                        objparams[5] = new SqlParameter("@P_WTNO", objLeave.WTNO);
                        objparams[6] = new SqlParameter("@P_STATUS", objLeave.STATUS);
                        objparams[7] = new SqlParameter("@P_COLLEGE_CODE", objLeave.COLLEGE_CODE);
                        objparams[8] = new SqlParameter("@P_REASON_NO", objLeave.RESNO);
                        objparams[9] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objparams[10] = new SqlParameter("@P_CREATEDBY", objLeave.UANO);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_THUMBPRBLM_ALLOW_INSERT_NR", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddThumbPrblm->" + ex.ToString());
                    }
                    return retstatus;
                }

                #endregion


                #region LateComers
                /// <summary>
                /// Added by Mrunal Bansod on dated 17/10/2012
                /// used in aprroval of late comers
                /// Method used to retrive employee's login details.
                /// </summary>
                /// <param name="frmdate"></param>
                /// <param name="todate"></param>
                /// <returns></returns>
                public DataSet GetLoginInfoByDate(DateTime frmdate, DateTime todate, int deptno, int collegeno, int stno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", frmdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", todate);
                        objParams[2] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        objParams[4] = new SqlParameter("@P_STNO", stno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_PAY_EMP_DAILY_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLoginInfoByDate->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                /// <summary>
                /// Added by Mrunal Bansod.
                /// Method to get late comeres allowed list.
                /// </summary>
                /// <param name="frmdt"></param>
                /// <param name="todt"></param>
                /// <param name="deptno"></param>
                /// <returns></returns>
                public DataSet GetLateComingAllowedEmpList(string frmdt, string todt, int deptno, int collegeno, int stno, int idno )
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_FROMDATE", frmdt);
                        objParams[1] = new SqlParameter("@P_TODATE", todt);
                        objParams[2] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        objParams[4] = new SqlParameter("@P_STNO", stno);
                        objParams[5] = new SqlParameter("@P_IDNO", idno);     
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_ALLOWED_LATE_COMERS", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetEarlyGoingAllowedEmpList->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                /// <summary>
                /// Added by Mrunal Bansod on dated 17/10/2012 
                /// to save late comers allow data in table
                /// </summary>
                /// <param name="objLeave"></param>
                /// <returns></returns>
                public int AddLateComersAllow(Leaves objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[14];
                        objparams[0] = new SqlParameter("@P_IDNO", objLeave.EMPNO);

                        if (!objLeave.DATE.Equals(DateTime.MinValue))
                            objparams[1] = new SqlParameter("@P_DT", objLeave.DATE);
                        else
                            objparams[1] = new SqlParameter("@P_DT", DBNull.Value);

                        objparams[2] = new SqlParameter("@P_EMPNAME", objLeave.EMPNAME);
                        objparams[3] = new SqlParameter("@P_INTIME", objLeave.INTIME);
                        objparams[4] = new SqlParameter("@P_OUTTIME", objLeave.OUTTIME);
                        objparams[5] = new SqlParameter("@P_HOURS", objLeave.HOURS);
                        objparams[6] = new SqlParameter("@P_LATEBY", objLeave.LATEBY);
                        objparams[7] = new SqlParameter("@P_WTNO", objLeave.WTNO);
                        objparams[8] = new SqlParameter("@P_STATUS", objLeave.STATUS);
                        objparams[9] = new SqlParameter("@P_COLLEGE_CODE", objLeave.COLLEGE_CODE);
                        objparams[10] = new SqlParameter("@P_REASON_NO", objLeave.RESNO);
                        objparams[11] = new SqlParameter("@P_SHIFT_INTIME", objLeave.SHIFT_INTIME);
                        objparams[12] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objparams[13] = new SqlParameter("@P_CREATEDBY", objLeave.UANO);
                        //@P_REASON_NO


                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_LATE_COMERS_ALLOW_INSERT", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddThumbPrblm->" + ex.ToString());
                    }
                    return retstatus;
                }

                ///<summary>
                ///create by: swati ghate 
                ///date: 03-02-2015
                ///reason: to delete the data from table before transfering the data 
                ///</summary>

                public int DeleteDataTableForLatecomers()
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_DELETE_DATATABLE_FOR_LATECOMERS", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.DeleteLeave-> " + ex.ToString());
                    }
                    return retstatus;
                }
                ///<summary>
                ///create by: swati ghate 
                ///date: 03-02-2015
                ///reason: To transfering the data from Datatable in to Table 
                ///</summary>
                public bool BulkInsertDataTable(string tableName, DataTable dataTable)
                {
                    bool isSuccuss;
                    try
                    {
                        // SqlConnection SqlConnectionObj = GetSQLConnection();
                        SqlConnection SqlConnectionObj = new SqlConnection(_nitprm_constr);
                        SqlConnectionObj.Open();
                        SqlBulkCopy bulkCopy = new SqlBulkCopy(SqlConnectionObj, SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.UseInternalTransaction, null);
                        bulkCopy.DestinationTableName = tableName;
                        bulkCopy.WriteToServer(dataTable);
                        SqlConnectionObj.Close();
                        isSuccuss = true;
                    }
                    catch (Exception ex)
                    {


                        isSuccuss = false;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.BulkInsertDataTable->" + ex.ToString());

                    }
                    return isSuccuss;
                }

                #endregion

                #region EarlyGoing
                /// <summary>
                /// Added by Mrunal Bansod 
                /// to retrived early going employee's list
                /// </summary>
                /// <param name="frmdt"></param>
                /// <param name="todt"></param>
                /// <param name="idno"></param>
                /// <param name="deptno"></param>
                /// <returns></returns>
                public DataSet GetEarlyGoingEmpList(string frmdt, string todt, int collegeno, int deptno, int stno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_FROMDATE", frmdt);
                        objParams[1] = new SqlParameter("@P_TODATE", todt);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        objParams[3] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[4] = new SqlParameter("@P_STNO", stno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_EMPLOYEE_EARLY_GOING", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetEarlyGoingEmpList->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }




                /// <summary>
                /// Added by Mrunal Bansod 
                /// Method to insert approval of early going data
                /// </summary>
                /// <param name="objLeave"></param>
                /// <returns></returns>
                public int AddEarlyGoingAllow(Leaves objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[13];
                        objparams[0] = new SqlParameter("@P_IDNO", objLeave.EMPNO);

                        if (!objLeave.DATE.Equals(DateTime.MinValue))
                            objparams[1] = new SqlParameter("@P_DT", objLeave.DATE);
                        else
                            objparams[1] = new SqlParameter("@P_DT", DBNull.Value);

                        objparams[2] = new SqlParameter("@P_EMPNAME", objLeave.EMPNAME);
                        objparams[3] = new SqlParameter("@P_INTIME", objLeave.INTIME);
                        objparams[4] = new SqlParameter("@P_OUTTIME", objLeave.OUTTIME);
                        objparams[5] = new SqlParameter("@P_SHIFTOUTTIME", objLeave.SHIFTOUTTIME);
                        objparams[6] = new SqlParameter("@P_LEAVETYPE", objLeave.LEAVETYPE);
                        objparams[7] = new SqlParameter("@P_WTNO", objLeave.WTNO);
                        objparams[8] = new SqlParameter("@P_STATUS", objLeave.STATUS);
                        objparams[9] = new SqlParameter("@P_COLLEGE_CODE", objLeave.COLLEGE_CODE);
                        objparams[10] = new SqlParameter("@P_REASON_NO", objLeave.RESNO);
                        objparams[11] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objparams[12] = new SqlParameter("@P_CREATEDBY", objLeave.UANO);


                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_EARLY_GOING_ALLOW_INSERT", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddEarlyGoingAllow->" + ex.ToString());
                    }
                    return retstatus;
                }


                /// <summary>
                /// Added by Mrunal Bansod
                /// 
                /// </summary>
                /// <param name="frmdt"></param>
                /// <param name="todt"></param>
                /// <param name="idno"></param>
                /// <param name="deptno"></param>
                /// <returns></returns>
                public DataSet GetEarlyGoingAllowedEmpList(string frmdt, string todt, int deptno, int collegeno, int stno , int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_FROMDATE", frmdt);
                        objParams[1] = new SqlParameter("@P_TODATE", todt);
                        objParams[2] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        objParams[4] = new SqlParameter("@P_STNO", stno);
                        objParams[5] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_ALLOWED_EARLY_GOING", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetEarlyGoingAllowedEmpList->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }


                #endregion

                public DataSet GetEmpOfDirectCredit(int stno, int lno, int deptno, int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_STNO", stno);
                        objParams[1] = new SqlParameter("@P_LNO", lno);
                        objParams[2] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[3] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_PAY_GET_DIRECT_CREDIT_LEAVES", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetEmpOfDirectCredit->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                // To fetch existing Direct Credit Leave details by passing Holiday No.
                public DataSet RetrieveSingleDirectCreditLeave(int ENO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ENO", ENO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_DIRECT_CREDIT_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.RetrieveSingleDirectCreditLeave->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;


                }


                //TO DELETE DIRECT CREDITED LEAVE
                public int DeleteDirectCreditedLeave(int IDNO, int LNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_LNO", LNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_DELETE_DIRECT_CREDIT_LEAVE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.DeleteLeave-> " + ex.ToString());
                    }
                    return retstatus;
                }

                //END OF TO DELETE DIRECT CREDITED LEAVE


                //TO UPDATE PAYROLL_LEAVE_APP_ENTRY TABLE 14dec2012
                public int UpdateLeaveAppEntry_old(int LETRNO, DateTime frmdate, DateTime todate, double no_of_day, DateTime joindate)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[6];
                        objparams[0] = new SqlParameter("@P_LETRNO", LETRNO);
                        objparams[1] = new SqlParameter("@P_FROMDATE", frmdate);
                        objparams[2] = new SqlParameter("@P_TODATE", todate);
                        objparams[3] = new SqlParameter("@P_NO_OF_DAYS", no_of_day);
                        objparams[4] = new SqlParameter("@P_JOIN_DATE", joindate);
                        objparams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[5].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_APPROVAL_UPDATE", objparams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_APP_PASS_ENTRY_UPDATE1", objparams, false) != null)
                        //    retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateLvAplAprStatus->" + ex.ToString());
                    }
                    return retStatus;

                }
                public int UpdateLeaveAppEntry(Leaves objLM)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[8];
                        objparams[0] = new SqlParameter("@P_LETRNO", objLM.LETRNO);
                        objparams[1] = new SqlParameter("@P_FROMDATE", objLM.FROMDT);
                        objparams[2] = new SqlParameter("@P_TODATE", objLM.TODT);
                        objparams[3] = new SqlParameter("@P_NO_OF_DAYS", objLM.NO_DAYS);
                        objparams[4] = new SqlParameter("@P_JOIN_DATE", objLM.JOINDT);
                        objparams[5] = new SqlParameter("@P_STATUS", objLM.STATUS);
                        objparams[6] = new SqlParameter("@P_LEAVE_FNAN", objLM.LEAVEFNAN);

                        objparams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_APPROVAL_UPDATE", objparams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_APP_PASS_ENTRY_UPDATE1", objparams, false) != null)
                        //    retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateLvAplAprStatus->" + ex.ToString());
                    }
                    return retStatus;

                }
                //TO INSERT LEAVE WITHOUT PAY LEAVE ENTRY
                public int InsertLWPEntry(Leaves objLeaves, string pre, string suf)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[23];
                        objParams[0] = new SqlParameter("@P_EMPNO", objLeaves.EMPNO);

                        objParams[1] = new SqlParameter("@P_LNO", objLeaves.LNO);
                        objParams[2] = new SqlParameter("@P_APPDT", objLeaves.APPDT);

                        objParams[3] = new SqlParameter("@P_FROM_DATE", objLeaves.FROMDT);

                        objParams[4] = new SqlParameter("@P_TO_DATE", objLeaves.TODT);
                        objParams[5] = new SqlParameter("@P_NO_OF_DAYS", objLeaves.NO_DAYS);
                        objParams[6] = new SqlParameter("@P_JOINDT", objLeaves.JOINDT);
                        objParams[7] = new SqlParameter("@P_FIT", objLeaves.FIT);
                        objParams[8] = new SqlParameter("@P_UNFIT", objLeaves.UNFIT);
                        objParams[9] = new SqlParameter("@P_PERIOD", objLeaves.PERIOD);
                        objParams[10] = new SqlParameter("@P_FNAN", objLeaves.FNAN);

                        objParams[11] = new SqlParameter("@P_REASON", objLeaves.REASON);
                        objParams[12] = new SqlParameter("@P_ADDRESS", objLeaves.ADDRESS);
                        objParams[13] = new SqlParameter("@P_STATUS", objLeaves.STATUS);
                        objParams[14] = new SqlParameter("@P_COLLEGE_CODE", objLeaves.COLLEGE_CODE);
                        objParams[15] = new SqlParameter("@P_PREFIX", objLeaves.PREFIX);
                        objParams[16] = new SqlParameter("@P_SUFFIX", objLeaves.SUFFIX);
                        objParams[17] = new SqlParameter("@P_PAPNO", objLeaves.PAPNO);
                        objParams[18] = new SqlParameter("@P_PRE", pre);
                        objParams[19] = new SqlParameter("@P_POST", suf);
                        objParams[20] = new SqlParameter("@P_CHARGE", objLeaves.CHARGE_HANDED);
                        objParams[21] = new SqlParameter("@P_ENTRY_STATUS", objLeaves.ENTRY_STATUS);
                        objParams[22] = new SqlParameter("@P_LETRNO", SqlDbType.Int);
                        objParams[22].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_APP_ENTRY_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddAPP_ENTRY->" + ex.ToString());
                    }
                    return retStatus;
                }

                //To get Employees who forgot to punch 27Dec2012
                public DataSet GetEmployeeForgotToPunch(int mon, int stno, int deptno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_MON", mon);
                        objParams[1] = new SqlParameter("@P_STNO", stno);
                        objParams[2] = new SqlParameter("@P_DEPTNO", deptno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_FORGOT_TO_PUNCH_LIST", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetEmployeegrt6->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetEmployee(int deptno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DEPTNO", deptno);
                        //ds = objSQLHelper.ExecuteDataSet("EMP_USERINFO_USERNAME");
                        ds = objSQLHelper.ExecuteDataSetSP("EMP_USERINFO_USERNAME", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetSingleLeave->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public DataSet GetEmployeeByDeptStaff(int deptno, int staffno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[1] = new SqlParameter("@P_STAFFNO", staffno);

                        //ds = objSQLHelper.ExecuteDataSet("EMP_USERINFO_USERNAME");
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_EMPNAME_BY_DEPT_STAFF", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetSingleLeave->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                //To update Leave Status D
                public int UpdateLeaveStatus(Leaves objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_LNO", objLeave.LNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_UPDATE_STATUS", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.UpdateLeave-> " + ex.ToString());
                    }
                    return retstatus;
                }
                //End of To update leave status 

                //To update Leave Status from D To A
                public int UpdateLeaveStatusDToA(Leaves objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_LNO", objLeave.LNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_UPDATE_STATUS_DTOA", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.UpdateLeave-> " + ex.ToString());
                    }
                    return retstatus;
                }

                //TO FETCH EMPLOYEE LEAVE TAKEN
                // To Fetch all existing holiday details
                public DataSet EmployeeLeaveTakenDetails(int empno, DateTime fdate, DateTime tdate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = new SqlParameter[3];
                        objparams[0] = new SqlParameter("@P_EMPNO", empno);
                        objparams[1] = new SqlParameter("@P_FDATE", fdate);
                        objparams[2] = new SqlParameter("@P_TDATE", tdate);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_ESTB_EMPLOYEE_LEAVETAKEN_DETAILS", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.RetrieveAllEmployee->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                //FOR RMPLOYEE LEAVE CALCELATION 
                public DataSet GetApprovedLeavesToCancel(int IDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_IDNO", IDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_APPROVED_LEAVE_LIST_BYIDNO", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLeavesStatus->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                //UPDATE LEAVETRAN TABLE AFTER LEAVE CALCELATION
                public int UpdateleaveCancelLeavetran(Leaves objLeaves)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_ENO", objLeaves.ENO);
                        objParams[1] = new SqlParameter("@P_FDATE", objLeaves.FROMDT);
                        objParams[2] = new SqlParameter("@P_TDATE", objLeaves.TODT);
                        objParams[3] = new SqlParameter("@P_LEAVES", objLeaves.LEAVE);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVECANCEL_UPDATE_LEAVETRAN_TABLE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateAppEntry->" + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetEmployeeList(int Dept, int StaffType, int college_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DEPT", Dept);
                        objParams[1] = new SqlParameter("@P_STAFFTYPE", StaffType);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", college_no);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_LEAVES_GET_BY_EMPLOYEES", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetLeavesSType->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                //---------------------------------


                //COMP OFF END

                #region LeaveDeduction
                public DataSet GetLeaveToDeduct(Leaves objLeaves)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_ATTENMONTH", objLeaves.ATTEMONTHNAME);
                        objParams[1] = new SqlParameter("@P_DEPTNO", objLeaves.DEPTNO);
                        objParams[2] = new SqlParameter("@P_STAFFNO", objLeaves.STNO);
                        objParams[3] = new SqlParameter("@P_APPOINTNO", objLeaves.APPOINT_NO);
                        // objParams[1] = new SqlParameter("@P_STAFFTYPE", StaffType);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_EMPLOYEE_LIST_TO_LEAVE_DEDUCT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetLeavesSType->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int AddLeaveToDeduct(int idno, decimal leaves, int year, int st, int period, int college_code, int leaveno, string attenmon)
                {

                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[8];
                        objparams[0] = new SqlParameter("@P_IDNO", idno);
                        objparams[1] = new SqlParameter("@P_LEAVES", leaves);
                        objparams[2] = new SqlParameter("@P_YEAR", year);
                        objparams[3] = new SqlParameter("@P_ST", st);
                        objparams[4] = new SqlParameter("@P_PERIOD", period);
                        objparams[5] = new SqlParameter("@P_COLLEGE_CODE", college_code);
                        objparams[6] = new SqlParameter("@P_LEAVENO", leaveno);
                        objparams[7] = new SqlParameter("@P_MONTH", attenmon);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_LEAVE_INSERT_FOR_DEDUCTION", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddCompOff->" + ex.ToString());
                    }
                    return retstatus;
                }

                public int DeleteLeaveDeduct(int idno, string attenmon)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_ATTENMON", attenmon);
                        Object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DELETE_EXIST_LEAVE_DEDUCT", objParams, true);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.DeleteLeaveAppEntry->" + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion


                //--------------------------------Lwp Entry Instean of Leave-----------------
                public int UpdateLeaveAppEntryForLwp(int LETRNO, DateTime frmdate, DateTime todate, double no_of_day, DateTime joindate, int lno)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[7];
                        objparams[0] = new SqlParameter("@P_LETRNO", LETRNO);
                        objparams[1] = new SqlParameter("@P_FROMDATE", frmdate);
                        objparams[2] = new SqlParameter("@P_TODATE", todate);
                        objparams[3] = new SqlParameter("@P_NO_OF_DAYS", no_of_day);
                        objparams[4] = new SqlParameter("@P_JOIN_DATE", joindate);
                        objparams[5] = new SqlParameter("@P_LNO", lno);
                        objparams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[6].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_APPROVAL_UPDATE_LWP_ENTRY", objparams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_APP_PASS_ENTRY_UPDATE1", objparams, false) != null)
                        //    retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateLvAplAprStatus->" + ex.ToString());
                    }
                    return retStatus;

                }
                //--------------------------------



                #region vacationentry
                public DataSet RetrieveAllEmployeeVacation(int collegeno, int deptno, int stno, int ua_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = new SqlParameter[4];
                        objparams[0] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        objparams[1] = new SqlParameter("@P_DEPTNO", deptno);
                        objparams[2] = new SqlParameter("@P_STNO", stno);
                        objparams[3] = new SqlParameter("@P_UA_NO", ua_no);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_EMPLOYEE_GETALL_VACATION_DATA", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.RetrieveAllEmployeeVacation->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int AddVacation(DataTable dtEmpRecord, Shifts objshifts)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[2];
                        objparams[0] = new SqlParameter("@P_ESTB_VACATION_RECORD", dtEmpRecord);

                        objparams[1] = new SqlParameter("@P_COLLEGE_NO", objshifts.COLLEGE_NO);


                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_VACATION_ENTRY", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddAssignShift->" + ex.ToString());
                    }
                    return retstatus;
                }


                //public int AddVacation(Leaves objLM, DataTable dtVacation)
                //{
                //    int retstatus = 0;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objparams = null;
                //        objparams = new SqlParameter[4];

                //        objparams[0] = new SqlParameter("@P_ESTB_EMP_RECORD", dtVacation);

                //        if (!objLM.FROMDT.Equals(DateTime.MinValue))
                //            objparams[1] = new SqlParameter("@P_FROMDT", objLM.FROMDT);
                //        else
                //            objparams[1] = new SqlParameter("@P_FROMDT", DBNull.Value);

                //        if (!objLM.TODT.Equals(DateTime.MinValue))
                //            objparams[2] = new SqlParameter("@P_TODT", objLM.TODT);
                //        else
                //            objparams[2] = new SqlParameter("@P_TODT", DBNull.Value);

                //        objparams[3] = new SqlParameter("@P_COLLEGE_NO", objLM.COLLEGE_NO);

                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_VACATION_ENTRY", objparams, false) != null)
                //            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //    }
                //    catch (Exception ex)
                //    {
                //        retstatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddVacation->" + ex.ToString());
                //    }
                //    return retstatus;
                //}

                public DataSet GetVacationInfo(int collegeNo, int stno, int SUBDEPTNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_COLLEGE_NO", collegeNo);
                        objParams[1] = new SqlParameter("@P_STNO", stno);
                        objParams[2] = new SqlParameter("@P_SUBDEPTNO", SUBDEPTNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_GET_VACATION_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetEmailSendInformation->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public int DeleteVacationEntry(int IDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_VACATION_ENTRY_DELETE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.DeleteLeave-> " + ex.ToString());
                    }
                    return retstatus;
                }
                public DataSet GetVacationExcelReport(Leaves objLeaves)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_COLLEGE_NO", objLeaves.COLLEGE_NO);
                        objParams[1] = new SqlParameter("@P_STNO", objLeaves.STNO);
                        objParams[2] = new SqlParameter("@P_DEPTNO", objLeaves.DEPTNO);
                        objParams[3] = new SqlParameter("@P_FROMDT", objLeaves.FROMDT);
                        objParams[4] = new SqlParameter("@P_TODT", objLeaves.TODT);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_VACATION_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetEmailSendInformation->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;

                }
                #endregion


                #region Detention_For_EL
                //DETENTION FOR EL CALCULATION
                public DataSet GetEmployeesForDetentionEL(Leaves objLeaves)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_STAFFNO", objLeaves.STNO);
                        objParams[1] = new SqlParameter("@P_DEPTNO", objLeaves.DEPTNO);
                        //  objParams[0] = new SqlParameter("@P_STAFFNO", Year);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_DETENTION_ENTRIES_FOR_EL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForAmountDeduction-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddDetentionEntryForEL(Leaves objLeaves)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_IDNO", objLeaves.EMPNO);

                        objParams[1] = new SqlParameter("@P_ELNOOFDAYS", objLeaves.ELNOOFDAYS);


                        objParams[2] = new SqlParameter("@P_ELYEAR", objLeaves.ELYEAR);
                        objParams[3] = new SqlParameter("@P_EL_TO_CREDIT", objLeaves.ELTOCREDIT);
                        objParams[4] = new SqlParameter("@P_DESIGNO", objLeaves.SUBDESIGNO);
                        objParams[5] = new SqlParameter("@P_DEPTNO", objLeaves.DEPTNO);

                        objParams[6] = new SqlParameter("@P_STAFFNO", objLeaves.STNO);
                        objParams[7] = new SqlParameter("@P_YEAR", objLeaves.YEAR);
                        objParams[8] = new SqlParameter("@P_REASON", objLeaves.REASON);
                        objParams[9] = new SqlParameter("@P_PERIOD", objLeaves.PERIOD);

                        //@P_YEAR
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_ESTAB_INSERT_DETENTION_FOR_EL", objParams, true);
                        // retStatus = Convert.ToInt32(ret);
                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.Add_Update_AdmissionDate -> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetEmployeesForDetentionELUpdate(Leaves objLeaves)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_STAFFNO", objLeaves.STNO);
                        objParams[1] = new SqlParameter("@P_YEAR", objLeaves.YEAR);
                        objParams[2] = new SqlParameter("@P_DEPTNO", objLeaves.DEPTNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_DETENTION_ENTRIES_FOR_UPDATE_EL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ChangeInMasterFileController.GetEmployeesForAmountDeduction-> " + ex.ToString());
                    }
                    return ds;
                }
                public int DeleteLeaveDetentionEntryForEL(Leaves objLeaves)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_STAFFNO", objLeaves.STNO);
                        objParams[1] = new SqlParameter("@P_YEAR", objLeaves.YEAR);
                        objParams[2] = new SqlParameter("@P_DEPTNO", objLeaves.DEPTNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_DELETE_dETENTION_ENTRY_FOR_EL", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeaveController.DeleteLeave-> " + ex.ToString());
                    }
                    return retstatus;
                }
                #endregion


                #region Dashboard

                public DataSet GetLeavecountForDashboard(int userno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_IDNO", userno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_AUTHORITY_DASHBOARD", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.RetrieveAllEmployee->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet RetrieveEmpDeatilsallLeaveApproveDashboardnew(int UANO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", UANO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_APP_GET_APPROVEDANDPENDING_COUNT_FOR_PRINCIPAL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet RetrieveEmpDeatilsallLeaveApprovecountDashboardnew(int UANO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", UANO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_APP_GET_WEB_METHOD_APPROVED_COUNT_FOR_PRINCIPAL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet RetrieveEmpDeatilsallLeavePendingcountDashboardnew(int UANO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", UANO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_APP_GET_WEB_METHOD_PENDING_COUNT_FOR_PRINCIPAL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentCurrentRegDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion


                #region Directcomoff
                //ADDED BY SHRIKANT BHARNE
                public int DirectAddCompOff(int Idno, DateTime Working_Date, string Reason, DateTime App_Date, char Status, int DPTNO, double no_of_days)
                {

                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[9];
                        objparams[0] = new SqlParameter("@P_IDNO", Idno);
                        if (!Working_Date.Equals(DateTime.MinValue))
                            objparams[1] = new SqlParameter("@P_WORKING_DATE", Working_Date);
                        else
                            objparams[1] = new SqlParameter("@P_WORKING_DATE", DBNull.Value);

                        //if (!Working_Date.Equals(DateTime.MinValue))
                        //    objparams[2] = new SqlParameter("@P_EXPIRY_DATE", NextMonth);
                        //else
                        //    objparams[2] = new SqlParameter("@P_EXPIRY_DATE", DBNull.Value);

                        objparams[2] = new SqlParameter("@P_REASON", Reason);
                        if (!App_Date.Equals(DateTime.MinValue))
                            objparams[3] = new SqlParameter("@P_APPLY_DATE", App_Date);
                        else
                            objparams[3] = new SqlParameter("@P_APPLY_DATE", DBNull.Value);
                        objparams[4] = new SqlParameter("@P_STATUS", Status);
                        objparams[5] = new SqlParameter("@P_DEPTNO", DPTNO);
                        objparams[6] = new SqlParameter("@P_NO_OF_DAYS", no_of_days);
                        objparams[7] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objparams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[8].Direction = ParameterDirection.Output;

                        //@P_NO_OF_DAYS
                        // if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_COMP_OFF_LEAVE_INSERT", objparams, false) != null)
                        //     retstatus = Convert.ToInt32(CustomStatus.RecordSaved);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_COMP_OFF_LEAVE_INSERT_DIRECT_ENTRY", objparams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            //objLeave.LNO = Convert.ToInt32(ret);
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddCompOff->" + ex.ToString());
                    }
                    return retstatus;
                }


                #endregion

                #region Working Saturday

                public int AddUpdateWorkingHolidayDetails(Leaves objLeave, string Remark, bool IsWorking, int swdid, DataTable dt)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_SWDID", swdid);
                        objParams[1] = new SqlParameter("@P_STNO", objLeave.STNO);
                        objParams[2] = new SqlParameter("@P_WORKINGDATE", objLeave.DATE);
                        objParams[3] = new SqlParameter("@P_REMARK", Remark);
                        objParams[4] = new SqlParameter("@P_CREATEDBY", objLeave.CreatedBy);
                        objParams[5] = new SqlParameter("@P_MODIFYBY", objLeave.ModifiedBy);
                        objParams[6] = new SqlParameter("@P_HOLIDAY_WORKING_DEPT_RECORD", dt);
                        objParams[7] = new SqlParameter("@P_ISEMPWISESAT", objLeave.ISEMPWISESAT);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;
                        // object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SATWORK_ADD_UPDATE_WORKINGDAY_DETAILS", objParams, true);
                        //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SATWORK_ADD_UPDATE_WORKINGDAY_DETAILS_TEST", objParams, true);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_SATWORK_ADD_UPDATE_WORKINGDAY_DETAILS", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == -1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.Add_Update_AdmissionDate -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddUpdateWorkingHolidayDetails(Leaves objLeave, string Remark, bool IsWorking, int swdid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SWDID", swdid);
                        objParams[1] = new SqlParameter("@P_STNO", objLeave.STNO);
                        objParams[2] = new SqlParameter("@P_WORKINGDATE", objLeave.DATE);
                        objParams[3] = new SqlParameter("@P_REMARK", Remark);
                        objParams[4] = new SqlParameter("@P_CREATEDBY", objLeave.CreatedBy);
                        objParams[5] = new SqlParameter("@P_MODIFYBY", objLeave.ModifiedBy);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SATWORK_ADD_UPDATE_WORKINGDAY_DETAILS", objParams, true);
                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.Add_Update_AdmissionDate -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetSaturdayWorkingDate(DateTime fromdt, DateTime todt, int stno, int empno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_FROMDT", fromdt);
                        objParams[1] = new SqlParameter("@P_TODT", todt);
                        objParams[2] = new SqlParameter("@P_STNO", stno);
                        objParams[3] = new SqlParameter("@P_EMPNO", empno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_WORKING_AS_SATURDAY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeaveController.GetSaturdayWorkingDate-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSaturdayWorkingDate(DateTime fromdt, DateTime todt, int stno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_FROMDT", fromdt);
                        objParams[1] = new SqlParameter("@P_TODT", todt);
                        objParams[2] = new SqlParameter("@P_STNO", stno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_WORKING_AS_SATURDAY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeaveController.GetSaturdayWorkingDate-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSaturdayWorkingDetailsById(int SWDID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_SWDID", SWDID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SATWORK_GET_SATURDAY_WORKING_DETAILS_BYID", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetSaturdayWorkingDetailsById->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetSaturdayWorkingDetails(int UANO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_UA_NO", UANO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SATWORK_GET_ADD_WORKING_DETAILS", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetSaturdayWorkingDetails->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                //Added by Sonal Banode on 07-11-2022 for employee wise saturday

                public DataSet GetEmployeeListForSaturday(int STNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_STNO", STNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_GET_EMPLOYEE_LIST_FOR_SATURDAY", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetEmployeeListForSaturday->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int AddUpdateWorkingHolidayDetailsByID(Leaves objLeave, string Remark, bool IsWorking, int swdid, DataTable dt)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SWDID", swdid);
                        objParams[1] = new SqlParameter("@P_STNO", objLeave.STNO);
                        objParams[2] = new SqlParameter("@P_WORKINGDATE", objLeave.DATE);
                        objParams[3] = new SqlParameter("@P_REMARK", Remark);
                        objParams[4] = new SqlParameter("@P_CREATEDBY", objLeave.CreatedBy);
                        objParams[5] = new SqlParameter("@P_MODIFYBY", objLeave.ModifiedBy);
                        objParams[6] = new SqlParameter("@P_HOLIDAY_WORKING_RECORD_IDNO", dt);
                        objParams[7] = new SqlParameter("@P_OrganizationId", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[8] = new SqlParameter("@P_ISEMPWISESAT", objLeave.ISEMPWISESAT);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SATWORK_ADD_UPDATE_WORKINGDAY_BY_IDNO", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == -1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.Add_Update_AdmissionDate -> " + ex.ToString());
                    }
                    return retStatus;
                }

                //
                #endregion

                #region
                public int AddAPP_ENTRYDIRECT(Leaves objLeaves, string pre, string suf, int Rno)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[36];
                        objParams[0] = new SqlParameter("@P_EMPNO", objLeaves.EMPNO);
                        //objParams[1] = new SqlParameter("@P_DEPTNO", objLeaves.DEPTNO);
                        objParams[1] = new SqlParameter("@P_LNO", objLeaves.LNO);
                        if (!objLeaves.APPDT.Equals(DateTime.MinValue))
                            objParams[2] = new SqlParameter("@P_APPDT", objLeaves.APPDT);
                        else
                            objParams[2] = new SqlParameter("@P_APPDT", DBNull.Value);

                        if (!objLeaves.FROMDT.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_FROM_DATE", objLeaves.FROMDT);
                        else
                            objParams[3] = new SqlParameter("@P_FROM_DATE", DBNull.Value);

                        if (!objLeaves.TODT.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_TO_DATE", objLeaves.TODT);
                        else
                            objParams[4] = new SqlParameter("@P_TO_DATE", DBNull.Value);

                        objParams[5] = new SqlParameter("@P_NO_OF_DAYS", objLeaves.NO_DAYS);
                        if (!objLeaves.JOINDT.Equals(DateTime.MinValue))
                            objParams[6] = new SqlParameter("@P_JOINDT", objLeaves.JOINDT);
                        else
                            objParams[6] = new SqlParameter("@P_JOINDT", DBNull.Value);


                        objParams[7] = new SqlParameter("@P_FIT", objLeaves.FIT);
                        objParams[8] = new SqlParameter("@P_UNFIT", objLeaves.UNFIT);
                        objParams[9] = new SqlParameter("@P_PERIOD", objLeaves.PERIOD);
                        objParams[10] = new SqlParameter("@P_FNAN", objLeaves.FNAN);

                        objParams[11] = new SqlParameter("@P_REASON", objLeaves.REASON);
                        objParams[12] = new SqlParameter("@P_ADDRESS", objLeaves.ADDRESS);
                        objParams[13] = new SqlParameter("@P_STATUS", objLeaves.STATUS);
                        objParams[14] = new SqlParameter("@P_COLLEGE_CODE", objLeaves.COLLEGE_CODE);
                        objParams[15] = new SqlParameter("@P_PREFIX", objLeaves.PREFIX);
                        objParams[16] = new SqlParameter("@P_SUFFIX", objLeaves.SUFFIX);
                        objParams[17] = new SqlParameter("@P_PAPNO", objLeaves.PAPNO);
                        objParams[18] = new SqlParameter("@P_PRE", pre);
                        objParams[19] = new SqlParameter("@P_POST", suf);
                        objParams[20] = new SqlParameter("@P_CHARGE", objLeaves.CHARGE_HANDED);
                        objParams[21] = new SqlParameter("@P_ENTRY_STATUS", objLeaves.ENTRY_STATUS);
                        objParams[22] = new SqlParameter("@P_LEAVEFNAN", objLeaves.LEAVEFNAN);
                        objParams[23] = new SqlParameter("@P_RNO", Rno);
                        objParams[24] = new SqlParameter("@P_MLHPL", objLeaves.MLHPL);

                        objParams[25] = new SqlParameter("@P_LEAVENO", objLeaves.LEAVENO);
                        objParams[26] = new SqlParameter("@P_SESSION_SRNO", objLeaves.SESSION_SRNO);
                        objParams[27] = new SqlParameter("@P_ISFULLDAYLEAVE", objLeaves.ISFULLDAYLEAVE);
                        objParams[28] = new SqlParameter("@P_LEAVE_SESSION_SERVICE_SRNO", objLeaves.SESSION_SERVICE_SRNO);
                        objParams[29] = new SqlParameter("@IsWithCertificate", objLeaves.IsWithCertificate);
                        objParams[30] = new SqlParameter("@P_FileName", objLeaves.FileName);
                        objParams[31] = new SqlParameter("@P_FilePath", objLeaves.FilePath);
                        objParams[32] = new SqlParameter("@P_FileSize", objLeaves.FileSize);
                        objParams[33] = new SqlParameter("@P_Type", objLeaves.LType);
                        objParams[34] = new SqlParameter("@P_CREATEDBY", objLeaves.CREATEDBY);
                        objParams[35] = new SqlParameter("@P_LETRNO", SqlDbType.Int);
                        objParams[35].Direction = ParameterDirection.Output;

                        int ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_APP_ENTRY_INSERT1_DIRECTLEAVE", objParams, true));
                        //if (Convert.ToInt32(ret) == -99)
                        //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        //else
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = -99;
                        else
                            retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddAPP_ENTRY->" + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetleaveApplicationDataForExport(int collegeno, int stno, int deptno, DateTime FDATE, DateTime TDATE, int Type, int leaveno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[7];
                        objparams[0] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        objparams[1] = new SqlParameter("@P_STNO", stno);
                        objparams[2] = new SqlParameter("@P_DEPTNO", deptno);
                        objparams[3] = new SqlParameter("@P_FDATE", FDATE);
                        objparams[4] = new SqlParameter("@P_TDATE", TDATE);
                        objparams[5] = new SqlParameter("@P_TYPE", Type);
                        objparams[6] = new SqlParameter("@P_LEAVENO", leaveno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_ALL_LEAVE_APPLICATION_EXPORT_REPORT", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetLeavesStatus->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetleaveAllotmentDataForExport(int collegeno, int PERIOD, int YEAR, int LEAVENO, int DEPTNO, int STNO , int IDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[7];
                        objparams[0] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        objparams[1] = new SqlParameter("@P_PERIOD", PERIOD);
                        objparams[2] = new SqlParameter("@P_YEAR", YEAR);
                        objparams[3] = new SqlParameter("@P_LEAVENO", LEAVENO);
                        objparams[4] = new SqlParameter("@P_DEPTNO", DEPTNO);
                        objparams[5] = new SqlParameter("@P_STNO", STNO);
                        objparams[6] = new SqlParameter("@P_IDNO", IDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_BAL_LEAVESTATUS_EXPORT_TO_EXCEL", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetLeavesStatus->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetComOffCreditDataForExport(int collegeno, int stno, int deptno, DateTime FDATE, DateTime TDATE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[5];
                        objparams[0] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        objparams[1] = new SqlParameter("@P_STNO", stno);
                        objparams[2] = new SqlParameter("@P_DEPTNO", deptno);
                        objparams[3] = new SqlParameter("@P_FDATE", FDATE);
                        objparams[4] = new SqlParameter("@P_TDATE", TDATE);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_COM_OFF_CREDITED_EXPORT_EXCEL_REPORT", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetLeavesStatus->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion

                #region Class Arrangment
                public DataSet GetPendListforClassApproval(int IdNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IdNo", IdNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_GET_CLASS_ARRANGMENT_APPROVAL", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetPendListforLeaveApproval->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public DataSet GetAppectListforClassArrangementApproval(int IdNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", IdNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_GET_CLASS_ARRANGMENT_ACCEPT_APPROVAL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetPendListforChargeAcceptance->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public DataSet GetClassHandoverDetailsBySrno(int SRNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SRNO", SRNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_GET_CLASS_ARRANGMENT_ACCEPT_REJECT_BY_SRNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetPendListforChargeAcceptance->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public int AddUpdateclassChargeAcceptance(Leaves objLM)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SRNO", objLM.SRNO);
                        objParams[1] = new SqlParameter("@P_STATUS", objLM.STATUS);
                        objParams[2] = new SqlParameter("@P_APP_REMARKS", objLM.APP_REMARKS);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_CLASS_APP_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeaveController.AddUpdateChargeAcceptance-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion

                #region attendanceprocessnew
                public string LeaveAttendanceProcessnew(string idNos, int userIdno, string frmdt, string todt, int deptno, string CollegeCode, int college_no, int stno)
                {
                    string retStatus = string.Empty;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_IDNOS", idNos);
                        objParams[1] = new SqlParameter("@P_USER_IDNO", userIdno);
                        objParams[2] = new SqlParameter("@P_FROMDATE", frmdt);
                        objParams[3] = new SqlParameter("@P_TODATE", todt);
                        // objParams[2] = new SqlParameter("@P_Month", month);
                        // objParams[3] = new SqlParameter("@P_Year", year);
                        objParams[4] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[5] = new SqlParameter("@P_COLLEGECODE", CollegeCode);
                        objParams[6] = new SqlParameter("@P_COLLEGE_NO", college_no);
                        objParams[7] = new SqlParameter("@P_STNO", stno);
                        objParams[8] = new SqlParameter("@P_MESSAGE", SqlDbType.NVarChar, 1000);
                        objParams[8].Direction = ParameterDirection.Output;
                        //retStatus = Convert.ToString(objSQLHelper.ExecuteNonQuerySP("PKG_PAY_LEAVE_ATTENDANCE_PROCESS_NEW_SVCE", objParams, true));
                        retStatus = Convert.ToString(objSQLHelper.ExecuteNonQuerySP("PKG_PAY_LEAVE_ATTENDANCE_PROCESS_NEW", objParams, true));
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToString(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.LeaveAttendanceProcess-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion

                public int UpdateAppEntryDirectLeaveApplication(Leaves objLeaves)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[21];
                        objParams[0] = new SqlParameter("@P_LETRNO", objLeaves.LETRNO);
                        //objParams[1] = new SqlParameter("@P_EMPNO", objLeaves.EMPNO);
                        objParams[1] = new SqlParameter("@P_LNO", objLeaves.LNO);

                        if (!objLeaves.FROMDT.Equals(DateTime.MinValue))
                            objParams[2] = new SqlParameter("@P_FROM_DATE", objLeaves.FROMDT);
                        else
                            objParams[2] = new SqlParameter("@P_FROM_DATE", DBNull.Value);

                        if (!objLeaves.TODT.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_TO_DATE", objLeaves.TODT);
                        else
                            objParams[3] = new SqlParameter("@P_TO_DATE", DBNull.Value);

                        objParams[4] = new SqlParameter("@P_NO_OF_DAYS", objLeaves.NO_DAYS);
                        if (!objLeaves.JOINDT.Equals(DateTime.MinValue))
                            objParams[5] = new SqlParameter("@P_JOINDT", objLeaves.JOINDT);
                        else
                            objParams[5] = new SqlParameter("@P_JOINDT", DBNull.Value);

                        objParams[6] = new SqlParameter("@P_FIT", objLeaves.FIT);
                        objParams[7] = new SqlParameter("@P_UNFIT", objLeaves.UNFIT);
                        objParams[8] = new SqlParameter("@P_FNAN", objLeaves.FNAN);
                        objParams[9] = new SqlParameter("@P_REASON", objLeaves.REASON);
                        objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objLeaves.COLLEGE_CODE);
                        objParams[11] = new SqlParameter("@P_PREFIX", objLeaves.PREFIX);
                        objParams[12] = new SqlParameter("@P_SUFFIX", objLeaves.SUFFIX);
                        objParams[13] = new SqlParameter("@P_CHARGE", objLeaves.CHARGE_HANDED);
                        objParams[14] = new SqlParameter("@P_ADDRESS", objLeaves.ADDRESS);
                        objParams[15] = new SqlParameter("@P_ISFULLDAYLEAVE", objLeaves.ISFULLDAYLEAVE);
                        objParams[16] = new SqlParameter("@IsWithCertificate", objLeaves.IsWithCertificate);
                        objParams[17] = new SqlParameter("@P_FileName", objLeaves.FileName);
                        objParams[18] = new SqlParameter("@P_Type", objLeaves.LType);
                        objParams[19] = new SqlParameter("@P_MODIFYBY", objLeaves.MODIFIEDBY);
                        objParams[20] = new SqlParameter("@P_CHIDNO", objLeaves.CHIDNO);


                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_DIRECT_APP_ENTRY_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateAppEntry->" + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddORDERAPP_ENTRY(Leaves objLeaves, string pre, string suf, int Rno)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[34];
                        objParams[0] = new SqlParameter("@P_EMPNO", objLeaves.EMPNO);
                        //objParams[1] = new SqlParameter("@P_DEPTNO", objLeaves.DEPTNO);
                        objParams[1] = new SqlParameter("@P_LNO", objLeaves.LNO);
                        if (!objLeaves.APPDT.Equals(DateTime.MinValue))
                            objParams[2] = new SqlParameter("@P_APPDT", objLeaves.APPDT);
                        else
                            objParams[2] = new SqlParameter("@P_APPDT", DBNull.Value);

                        if (!objLeaves.FROMDT.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_FROM_DATE", objLeaves.FROMDT);
                        else
                            objParams[3] = new SqlParameter("@P_FROM_DATE", DBNull.Value);

                        if (!objLeaves.TODT.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_TO_DATE", objLeaves.TODT);
                        else
                            objParams[4] = new SqlParameter("@P_TO_DATE", DBNull.Value);

                        objParams[5] = new SqlParameter("@P_NO_OF_DAYS", objLeaves.NO_DAYS);
                        if (!objLeaves.JOINDT.Equals(DateTime.MinValue))
                            objParams[6] = new SqlParameter("@P_JOINDT", objLeaves.JOINDT);
                        else
                            objParams[6] = new SqlParameter("@P_JOINDT", DBNull.Value);


                        objParams[7] = new SqlParameter("@P_FIT", objLeaves.FIT);
                        objParams[8] = new SqlParameter("@P_UNFIT", objLeaves.UNFIT);
                        objParams[9] = new SqlParameter("@P_PERIOD", objLeaves.PERIOD);
                        objParams[10] = new SqlParameter("@P_FNAN", objLeaves.FNAN);

                        objParams[11] = new SqlParameter("@P_REASON", objLeaves.REASON);
                        objParams[12] = new SqlParameter("@P_ADDRESS", objLeaves.ADDRESS);
                        objParams[13] = new SqlParameter("@P_STATUS", objLeaves.STATUS);
                        objParams[14] = new SqlParameter("@P_COLLEGE_CODE", objLeaves.COLLEGE_CODE);
                        objParams[15] = new SqlParameter("@P_PREFIX", objLeaves.PREFIX);
                        objParams[16] = new SqlParameter("@P_SUFFIX", objLeaves.SUFFIX);
                        objParams[17] = new SqlParameter("@P_PAPNO", objLeaves.PAPNO);
                        objParams[18] = new SqlParameter("@P_PRE", pre);
                        objParams[19] = new SqlParameter("@P_POST", suf);
                        objParams[20] = new SqlParameter("@P_CHARGE", objLeaves.CHARGE_HANDED);
                        objParams[21] = new SqlParameter("@P_ENTRY_STATUS", objLeaves.ENTRY_STATUS);
                        objParams[22] = new SqlParameter("@P_LEAVEFNAN", objLeaves.LEAVEFNAN);
                        objParams[23] = new SqlParameter("@P_RNO", Rno);
                        objParams[24] = new SqlParameter("@P_MLHPL", objLeaves.MLHPL);

                        objParams[25] = new SqlParameter("@P_LEAVENO", objLeaves.LEAVENO);
                        objParams[26] = new SqlParameter("@P_SESSION_SRNO", objLeaves.SESSION_SRNO);
                        objParams[27] = new SqlParameter("@P_ISFULLDAYLEAVE", objLeaves.ISFULLDAYLEAVE);
                        objParams[28] = new SqlParameter("@P_LEAVE_SESSION_SERVICE_SRNO", objLeaves.SESSION_SERVICE_SRNO);
                        objParams[29] = new SqlParameter("@IsWithCertificate", objLeaves.IsWithCertificate);
                        objParams[30] = new SqlParameter("@P_FileName", objLeaves.FileName);
                        objParams[31] = new SqlParameter("@P_FilePath", objLeaves.FilePath);
                        objParams[32] = new SqlParameter("@P_FileSize", objLeaves.FileSize);
                        objParams[33] = new SqlParameter("@P_LETRNO", SqlDbType.Int);
                        objParams[33].Direction = ParameterDirection.Output;

                        int ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_ORDER_APP_ENTRY_INSERT", objParams, true));
                        //if (Convert.ToInt32(ret) == -99)
                        //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        //else
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = -99;
                        else
                            retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddAPP_ENTRY->" + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateOrderAppEntry(Leaves objLeaves)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[18];
                        objParams[0] = new SqlParameter("@P_LETRNO", objLeaves.LETRNO);
                        //objParams[1] = new SqlParameter("@P_EMPNO", objLeaves.EMPNO);
                        objParams[1] = new SqlParameter("@P_LNO", objLeaves.LNO);

                        if (!objLeaves.FROMDT.Equals(DateTime.MinValue))
                            objParams[2] = new SqlParameter("@P_FROM_DATE", objLeaves.FROMDT);
                        else
                            objParams[2] = new SqlParameter("@P_FROM_DATE", DBNull.Value);

                        if (!objLeaves.TODT.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_TO_DATE", objLeaves.TODT);
                        else
                            objParams[3] = new SqlParameter("@P_TO_DATE", DBNull.Value);

                        objParams[4] = new SqlParameter("@P_NO_OF_DAYS", objLeaves.NO_DAYS);
                        if (!objLeaves.JOINDT.Equals(DateTime.MinValue))
                            objParams[5] = new SqlParameter("@P_JOINDT", objLeaves.JOINDT);
                        else
                            objParams[5] = new SqlParameter("@P_JOINDT", DBNull.Value);

                        objParams[6] = new SqlParameter("@P_FIT", objLeaves.FIT);
                        objParams[7] = new SqlParameter("@P_UNFIT", objLeaves.UNFIT);
                        objParams[8] = new SqlParameter("@P_FNAN", objLeaves.FNAN);
                        objParams[9] = new SqlParameter("@P_REASON", objLeaves.REASON);
                        objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objLeaves.COLLEGE_CODE);
                        objParams[11] = new SqlParameter("@P_PREFIX", objLeaves.PREFIX);
                        objParams[12] = new SqlParameter("@P_SUFFIX", objLeaves.SUFFIX);
                        objParams[13] = new SqlParameter("@P_CHARGE", objLeaves.CHARGE_HANDED);
                        objParams[14] = new SqlParameter("@P_ADDRESS", objLeaves.ADDRESS);
                        objParams[15] = new SqlParameter("@P_ISFULLDAYLEAVE", objLeaves.ISFULLDAYLEAVE);
                        objParams[16] = new SqlParameter("@IsWithCertificate", objLeaves.IsWithCertificate);
                        objParams[17] = new SqlParameter("@P_FileName", objLeaves.FileName);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_ORDER_APP_ENTRY_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateAppEntry->" + ex.ToString());
                    }
                    return retStatus;
                }

                #region comp-off cancel
                public DataSet GetCreditedCompOff()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_COM_OFF_CREDITED", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLeaveApplStatus->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;

                }

                public int CompOffCancel(int ENO, int IDNO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ENO", ENO);
                        objParams[1] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_UPD_COMP_OFF_CANCEL", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            //objLeave.LNO = Convert.ToInt32(ret);
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.AddLeave-> " + ex.ToString());
                    }
                    return retstatus;
                }

                # endregion

                #region LeaveConfiguration
                public int AddUpdateLeaveConfiguration(Leaves objLeaves,string Leavevalue)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[25];
                        objParams[0] = new SqlParameter("@P_isSMS", objLeaves.isSMS);
                        objParams[1] = new SqlParameter("@P_isEmail", objLeaves.isEmail);
                        objParams[2] = new SqlParameter("@P_ODDAYS", objLeaves.ODDAYS);
                        objParams[3] = new SqlParameter("@P_odApp", objLeaves.odApp);
                        objParams[4] = new SqlParameter("@P_ComoffvalidMonth", objLeaves.ComoffvalidMonth);
                        objParams[5] = new SqlParameter("@P_FULLDAYCOMOFF", objLeaves.FULLDAYCOMOFF);
                        objParams[6] = new SqlParameter("@P_IsLeaveWisePath", objLeaves.IsLeaveWisePath);
                        objParams[7] = new SqlParameter("@P_DIRECTORMAILID", objLeaves.DIRECTORMAILID);
                        objParams[8] = new SqlParameter("@P_NOTIFICATION_DAYS_FOR_LEAVE_APPROVAL_VALIDDATE", objLeaves.NOTIFICATIONDAYS);
                        objParams[9] = new SqlParameter("@P_DAYS_FOR_AUTO_LEAVE_APPROVAL", objLeaves.LEAVEAPPROVALVALIDDAYS);
                        objParams[10] = new SqlParameter("@P_IsAutoApproved", objLeaves.IsAutoApprove);
                        objParams[11] = new SqlParameter("@P_IsApprovalOnDirectLeave", objLeaves.IsApprovalOnDirectLeave);
                        objParams[12] = new SqlParameter("@P_Daynumber", objLeaves.Daynumber);
                        objParams[13] = new SqlParameter("@P_IsEmployeewiseSaturday", objLeaves.IsEmployeewiseSaturday);
                        objParams[14] = new SqlParameter("@P_IsRequiredDocumentforOD", objLeaves.IsRequiredDocumentforOD);
                        objParams[15] = new SqlParameter("@P_AllowLate", objLeaves.AllowLate);
                        objParams[16] = new SqlParameter("@P_AllowEarly", objLeaves.AllowEarly);
                        objParams[17] = new SqlParameter("@P_PERMISSIONINMONTH", objLeaves.PERMISSIONINMONTH);
                        objParams[18] = new SqlParameter("@P_ISLWPNOTALLOW", objLeaves.ISLWPNOTALLOW);
                        objParams[19] = new SqlParameter("@P_LEAVEVALUE", Leavevalue);
                        objParams[20] = new SqlParameter("@P_IsShowLWP", objLeaves.IsShowLWP);
                        objParams[21] = new SqlParameter("@P_IsChargeHandedMail", objLeaves.IsChargeMail);
                        objParams[22] = new SqlParameter("@P_IsValidatedLeaveComb", objLeaves.IsValidatedLeaveComb);
                        objParams[23] = new SqlParameter("@P_IsNotAllowLeaveinCont", objLeaves.IsNotAllowLeaveinCont);
                        objParams[24] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[24].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_CONFIGURATION_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {

                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }


                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeaveController.AddUpdateLeaveConfiguration-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetConfigurationDetail()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null; ;
                        objparams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_GET_CONFIGURATION_DETAIL", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetSingHolidayType->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                # endregion

                #region LeaveWisePath
                public int AddLeaveWisePAPath(Leaves objLeave, DataTable dtEmpRecordLeaveNew)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[12];
                        objparams[0] = new SqlParameter("@P_PAN01", objLeave.PAN01);
                        objparams[1] = new SqlParameter("@P_PAN02", objLeave.PAN02);
                        objparams[2] = new SqlParameter("@P_PAN03", objLeave.PAN03);
                        objparams[3] = new SqlParameter("@P_PAN04", objLeave.PAN04);
                        objparams[4] = new SqlParameter("@P_PAN05", objLeave.PAN05);
                        objparams[5] = new SqlParameter("@P_PAPATH", objLeave.PAPATH);
                        objparams[6] = new SqlParameter("@P_DEPTNO", objLeave.DEPTNO);
                        objparams[7] = new SqlParameter("@P_LNO", objLeave.LNO);
                        objparams[8] = new SqlParameter("@P_COLLEGE_NO", objLeave.COLLEGE_NO);
                        objparams[9] = new SqlParameter("@P_COLLEGE_CODE", objLeave.COLLEGE_CODE);
                        objparams[10] = new SqlParameter("@P_ESTB_EMP_RECORD_NEW", dtEmpRecordLeaveNew);
                        objparams[11] = new SqlParameter("@P_PAPNO", SqlDbType.Int);
                        objparams[11].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_LEAVE_WISE_PASSING_AUTHORITY_PATH_INSERT", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddLeaveWisePAPath->" + ex.ToString());
                    }
                    return retstatus;
                }

                public int UpdateLeaveWisePAPath(Leaves objLeave, DataTable dtEmpRecordLeaveNew)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[12];
                        objparams[0] = new SqlParameter("@P_PAPNO", objLeave.PAPNO);
                        objparams[1] = new SqlParameter("@P_PAN01", objLeave.PAN01);
                        objparams[2] = new SqlParameter("@P_PAN02", objLeave.PAN02);
                        objparams[3] = new SqlParameter("@P_PAN03", objLeave.PAN03);
                        objparams[4] = new SqlParameter("@P_PAN04", objLeave.PAN04);
                        objparams[5] = new SqlParameter("@P_PAN05", objLeave.PAN05);
                        objparams[6] = new SqlParameter("@P_PAPATH", objLeave.PAPATH);
                        objparams[7] = new SqlParameter("@P_DEPTNO", objLeave.DEPTNO);
                        objparams[8] = new SqlParameter("@P_LNO", objLeave.LNO);
                        objparams[9] = new SqlParameter("@P_COLLEGE_NO", objLeave.COLLEGE_NO);
                        objparams[10] = new SqlParameter("@P_COLLEGE_CODE", objLeave.COLLEGE_CODE);
                        objparams[11] = new SqlParameter("@P_ESTB_EMP_RECORD_NEW", dtEmpRecordLeaveNew);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_LEAVE_WISE_PASSING_AUTHORITY_PATH_UPDATE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateLeaveWisePAPath->" + ex.ToString());
                    }
                    return retstatus;
                }

                #endregion

                #region Location Master

                public int AddLocation(Leaves objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_LOCATION_NAME", objLeave.LOCATION);
                        objParams[1] = new SqlParameter("@P_LATITUDE", objLeave.LATITUDE);
                        objParams[2] = new SqlParameter("@P_LONGITUDE", objLeave.LONGITUDE);
                        objParams[3] = new SqlParameter("@P_RADIUS", objLeave.RADIUS);
                        objParams[4] = new SqlParameter("@P_LOCATION_ADDRESS", objLeave.LOCATION_ADDRESS);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objLeave.COLLEGE_CODE);
                        objParams[6] = new SqlParameter("@P_CREATEDBY", objLeave.CREATEDBY);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_LEAVE_LOCATION_INSERT", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddLocation->" + ex.ToString());
                    }
                    return retstatus;
                }

                public int UpdateLocation(Leaves objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_LOCNO", objLeave.LOCNO);
                        objParams[1] = new SqlParameter("@P_LOCATION_NAME", objLeave.LOCATION);
                        objParams[2] = new SqlParameter("@P_LATITUDE", objLeave.LATITUDE);
                        objParams[3] = new SqlParameter("@P_LONGITUDE", objLeave.LONGITUDE);
                        objParams[4] = new SqlParameter("@P_RADIUS", objLeave.RADIUS);
                        objParams[5] = new SqlParameter("@P_LOCATION_ADDRESS", objLeave.LOCATION_ADDRESS);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objLeave.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_MODIFYBY", objLeave.MODIFIEDBY);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_LEAVE_LOCATION_UPDATE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateHolidayType->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetSingleLocation(int LOCNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null; ;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_LOCNO", LOCNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_LEAVE_GET_SINGLE_LOCATION_BY_ID", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetSingHolidayType->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetAllLocationName()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_LEAVE_GET_ALL_LOCATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetAllHolidayType->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                //To Delete existing Holiday type
                //public int DeleteLeaveShortName(int lvno)
                //{
                //    int retstatus = 0;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objparams = null;
                //        objparams = new SqlParameter[1];
                //        objparams[0] = new SqlParameter("@P_LEAVENO", lvno);
                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_PAY_LEAVE_SHORT_NAME_DELETE", objparams, false) != null)
                //            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                //    }
                //    catch (Exception ex)
                //    {
                //        retstatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.DeleteHolidaytype->" + ex.ToString());
                //    }
                //    return retstatus;
                //}

                #endregion

                # region Employee Location Mapping

                public DataSet GetEmployeeListLocationMapping(int idno, int collegeno, int deptid, int stno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        objParams[2] = new SqlParameter("@P_SUBDEPTNO", deptid);
                        objParams[3] = new SqlParameter("@P_STNO", stno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_LEAVE_GET_EMPLOYEE_LOCATION_MAPPING_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetEmployeeListLocationMapping->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetLocationWiseListLocationMapping(int locno, int collegeno, int deptid, int stno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_LOCNO", locno);
                        objParams[1] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        objParams[2] = new SqlParameter("@P_SUBDEPTNO", deptid);
                        objParams[3] = new SqlParameter("@P_STNO", stno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_LEAVE_GET_LOCATION_WISE_MAPPING_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetLocationWiseListLocationMapping->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetUnMappedEmployee(int locno, int collegeno, int deptid, int stno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_LOCNO", locno);
                        objParams[1] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        objParams[2] = new SqlParameter("@P_SUBDEPTNO", deptid);
                        objParams[3] = new SqlParameter("@P_STNO", stno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_LEAVE_GET_UNMAPPED_EMPLOYEE_FOR_LOCATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetUnMappedEmployee->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetUnMappedLocation(int idno, int collegeno, int deptid, int stno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        objParams[2] = new SqlParameter("@P_SUBDEPTNO", deptid);
                        objParams[3] = new SqlParameter("@P_STNO", stno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_LEAVE_GET_UNMAPPED_LOCATION_FOR_EMP", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetUnMappedLocation->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int AddEmpMapping(DataTable dtMapRecord, int CREATEDBY, string COLLEGE_CODE)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ESTB_LEAVE_EMP_LOCATION_MAPPING_RECORD", dtMapRecord);
                        objParams[1] = new SqlParameter("@P_CREATEDBY", CREATEDBY);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", COLLEGE_CODE);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_EMP_MAPPING_INSERT", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddLocation->" + ex.ToString());
                    }
                    return retstatus;
                }

                public int AddLocEmpUnMap(DataTable dtMapRecord, int map)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ESTB_LEAVE_EMP_LOCATION_MAPPING_RECORD", dtMapRecord);
                        objParams[1] = new SqlParameter("@P_MAPID", map);


                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_EMP_UNMAP_INSERT", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddLocation->" + ex.ToString());
                    }
                    return retstatus;
                }

                #endregion

                #region Check Valid Days For Leave

                public DataSet GetIsValidLeaveDateDays(DateTime frmdate, int stno, int collegeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_FRMDATE", frmdate);
                        objParams[1] = new SqlParameter("@P_STNO", stno);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", collegeno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_CHECK_VALID_LEAVE_DATE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetIsValidLeaveDateDays->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion

                #region Early going

                public DataSet GetThumbPrblmList(string frmdt, string todt, int deptno, int collegeno, int stno,int idno,int count)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];

                        objParams[0] = new SqlParameter("@P_FROM_DATE", frmdt);
                        objParams[1] = new SqlParameter("@P_TO_DATE", todt);
                        objParams[2] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        objParams[4] = new SqlParameter("@P_STNO", stno);
                        objParams[5] = new SqlParameter("@P_IDNO", idno);
                        objParams[6] = new SqlParameter("@P_COUNT", count);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_THUMB_PROBLEM_LEAVE", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetThumbPrblmList->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetThumbPrblmList_NonRegistered(string frmdt, string todt, int deptno, int collegeno, int stno, int idno, int count)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];

                        objParams[0] = new SqlParameter("@P_FROM_DATE", frmdt);
                        objParams[1] = new SqlParameter("@P_TO_DATE", todt);
                        objParams[2] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        objParams[4] = new SqlParameter("@P_STNO", stno);
                        objParams[5] = new SqlParameter("@P_IDNO", idno);
                        objParams[6] = new SqlParameter("@P_COUNT", count);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_THUMB_PROBLEM_LEAVE_NR", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetThumbPrblmList->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetLoginInfoByDate(DateTime frmdate, DateTime todate, int deptno, int collegeno, int stno,int idno, int count)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", frmdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", todate);
                        objParams[2] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        objParams[4] = new SqlParameter("@P_STNO", stno);
                        objParams[5] = new SqlParameter("@P_IDNO", idno);
                        objParams[6] = new SqlParameter("@P_COUNT", count);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_PAY_EMP_DAILY_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLoginInfoByDate->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetEarlyGoingEmpList(string frmdt, string todt, int collegeno, int deptno, int stno,int idno, int count)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];

                        objParams[0] = new SqlParameter("@P_FROMDATE", frmdt);
                        objParams[1] = new SqlParameter("@P_TODATE", todt);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        objParams[3] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[4] = new SqlParameter("@P_STNO", stno);
                        objParams[5] = new SqlParameter("@P_IDNO", idno);
                        objParams[6] = new SqlParameter("@P_COUNT", count);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_EMPLOYEE_EARLY_GOING", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetEarlyGoingEmpList->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                #endregion

                #region LeaveSuffixprefix
                public int AddLeavePrefixSuffix(Leaves objLeave, string PrefixAllowed, string SufixAllowed)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[7];
                        objparams[0] = new SqlParameter("@P_leaveno", objLeave.LEAVENO);
                        objparams[1] = new SqlParameter("@P_LEAVE", objLeave.LEAVENAME);
                        objparams[2] = new SqlParameter("@P_PrefixAllowed", PrefixAllowed);
                        objparams[3] = new SqlParameter("@P_SufixAllowed", SufixAllowed);
                        objparams[4] = new SqlParameter("@P_CreateDate", objLeave.CreatedDate);
                        objparams[5] = new SqlParameter("@P_CreatedBy", objLeave.CreatedBy);
                        objparams[6] = new SqlParameter("@P_SPFID", SqlDbType.Int);
                        objparams[6].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_ADD_SUFFIXPREFIX", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.AddLeavePrefixSuffix->" + ex.ToString());
                    }
                    return retstatus;
                }

                public int UpdateLeavePrefixSuffix(Leaves objLeave, string PrefixAllowed, string SufixAllowed)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[7];

                        objparams[0] = new SqlParameter("@P_SPFID", objLeave.SPFID);
                        objparams[1] = new SqlParameter("@P_leaveno", objLeave.LEAVENO);
                        objparams[2] = new SqlParameter("@P_LEAVE", objLeave.LEAVENAME);
                        objparams[3] = new SqlParameter("@P_PrefixAllowed", PrefixAllowed);
                        objparams[4] = new SqlParameter("@P_SufixAllowed", SufixAllowed);
                        objparams[5] = new SqlParameter("@P_MODIFIED_BY", objLeave.ModifiedBy);
                        objparams[6] = new SqlParameter("@P_MODIFIED_DATE", objLeave.CreatedDate);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_UPDATE_SUFFIXPREFIX", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.UpdateLeavePrefixSuffix->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet ValidsuffixPrefix(int idno, DateTime Fromdate, DateTime Todate, string prefix, string suffix)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_Fromdate", Fromdate);
                        objParams[2] = new SqlParameter("@P_Todate", Todate);
                        objParams[3] = new SqlParameter("@P_Prefix", prefix);
                        objParams[4] = new SqlParameter("@P_suffix", suffix);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_GET_LEAVE_SUFFIX_PREFIX", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.ValidsuffixPrefix->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                #endregion

                #region FacultyDashboard
                public DataSet RetrieveEmployeeInOut(int userno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = new SqlParameter[6];
                        objparams[0] = new SqlParameter("@P_IDNO", userno);
                        objparams[1] = new SqlParameter("@P_DEPTNO", Convert.ToInt32(0));
                        objparams[2] = new SqlParameter("@P_TIME_FROM", "N");
                        objparams[3] = new SqlParameter("@P_TIME_TO", "N");
                        objparams[4] = new SqlParameter("@P_INOUT", "N");
                        objparams[5] = new SqlParameter("@P_STNO", 0);

                        ds = objSQLHelper.ExecuteDataSetSP("EMP_INOUT_DASHBOARD", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.RetrieveAllEmployee->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet RetrieveEmployeeAttedeance(int userno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = new SqlParameter[6];
                        objparams[0] = new SqlParameter("@P_IDNO", userno);
                        objparams[1] = new SqlParameter("@P_DEPTNO", Convert.ToInt32(0));
                        objparams[2] = new SqlParameter("@P_TIME_FROM", "N");
                        objparams[3] = new SqlParameter("@P_TIME_TO", "N");
                        objparams[4] = new SqlParameter("@P_INOUT", "N");
                        objparams[5] = new SqlParameter("@P_STNO", 0);


                        ds = objSQLHelper.ExecuteDataSetSP("EMP_INOUT_DASHBOARD_PERCENTAGE", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.RetrieveAllEmployee->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet RetrieveHolidays(int userno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_IDNO", userno);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_DASHBOARD_HOLIDAY", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.RetrieveAllEmployee->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet RetrieveLeaveBalance(int userno, int currentYear)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = new SqlParameter[2];
                        objparams[0] = new SqlParameter("@P_IDNO", userno);
                        objparams[1] = new SqlParameter("@P_YEAR", currentYear);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_DASHBOARD_CL_BAL", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.RetrieveAllEmployee->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                #endregion

                #region LateThumbproblemshift
                public DataSet GetThumbPrblmList_Shift(string frmdt, string todt, int deptno, int collegeno, int stno,int idno,  int count)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];

                        objParams[0] = new SqlParameter("@P_FROM_DATE", frmdt);
                        objParams[1] = new SqlParameter("@P_TO_DATE", todt);
                        objParams[2] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        objParams[4] = new SqlParameter("@P_STNO", stno);
                        objParams[5] = new SqlParameter("@P_IDNO", idno);
                        objParams[6] = new SqlParameter("@P_COUNT", count);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_THUMB_PROBLEM_LEAVE_SHIFT", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetThumbPrblmList_Shift->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetLateComersEmpList_Shift(DateTime frmdate, DateTime todate, int deptno, int collegeno, int stno,int idno, int count)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", frmdate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", todate);
                        objParams[2] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        objParams[4] = new SqlParameter("@P_STNO", stno);
                        objParams[5] = new SqlParameter("@P_IDNO", idno);
                        objParams[6] = new SqlParameter("@P_COUNT", count);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_PAY_EMP_DAILY_REPORT_SHIFT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLoginInfoByDate->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetEarlyGoingEmpList_Shift(string frmdt, string todt, int collegeno, int deptno, int stno,int idno, int count)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];

                        objParams[0] = new SqlParameter("@P_FROMDATE", frmdt);
                        objParams[1] = new SqlParameter("@P_TODATE", todt);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        objParams[3] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[4] = new SqlParameter("@P_STNO", stno);
                        objParams[5] = new SqlParameter("@P_IDNO", idno);
                        objParams[6] = new SqlParameter("@P_COUNT", count);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_EMPLOYEE_EARLY_GOING_SHIFT", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetEarlyGoingEmpList->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public DataSet GetThumbPrblmList_NonRegistered_Shift(string frmdt, string todt, int deptno, int collegeno, int stno,int idno,  int count)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];

                        objParams[0] = new SqlParameter("@P_FROM_DATE", frmdt);
                        objParams[1] = new SqlParameter("@P_TO_DATE", todt);
                        objParams[2] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        objParams[4] = new SqlParameter("@P_STNO", stno);
                        objParams[5] = new SqlParameter("@P_IDNO", idno);
                        objParams[6] = new SqlParameter("@P_COUNT", count);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_THUMB_PROBLEM_LEAVE_NR_SHIFT", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetThumbPrblmList->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                #endregion

                #region Irregularity Apply

                public DataSet GetIrregularityApplyList(string frmdt, string todt, int idno, int deptno, int collegeno, int stno, int count)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];

                        objParams[0] = new SqlParameter("@P_FROM_DATE", frmdt);
                        objParams[1] = new SqlParameter("@P_TO_DATE", todt);
                        objParams[2] = new SqlParameter("@P_IDNO", idno);
                        objParams[3] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[4] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        objParams[5] = new SqlParameter("@P_STNO", stno);
                        objParams[6] = new SqlParameter("@P_COUNT", count);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_PAY_EMP_DAILYBWEEKBASIC_IRRIREGULAITY", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetIrregularityApplyList->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetIrregularityApplyBySrno(string frmdt, string todt, int idno, int deptno, int collegeno, int stno, int count, int SRNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];

                        objParams[0] = new SqlParameter("@P_FROM_DATE", frmdt);
                        objParams[1] = new SqlParameter("@P_TO_DATE", todt);
                        objParams[2] = new SqlParameter("@P_IDNO", idno);
                        objParams[3] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[4] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        objParams[5] = new SqlParameter("@P_STNO", stno);
                        objParams[6] = new SqlParameter("@P_COUNT", count);
                        objParams[7] = new SqlParameter("@P_SRNO", SRNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_PAY_EMP_DAILYBWEEKBASIC_IRREGULARITY_APPLY", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetIrregularityApplyBySrno->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int AddIrregularityEntry(Leaves objLeaves)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[18];
                        objParams[0] = new SqlParameter("@P_EMPNO", objLeaves.EMPNO);
                        objParams[1] = new SqlParameter("@P_STNO", objLeaves.STNO);
                        objParams[2] = new SqlParameter("@P_SERIALNO", objLeaves.SERIALNO);
                        if (!objLeaves.APPDT.Equals(DateTime.MinValue))
                            objParams[3] = new SqlParameter("@P_APPDT", objLeaves.APPDT);
                        else
                            objParams[3] = new SqlParameter("@P_APPDT", DBNull.Value);

                        if (!objLeaves.DATE.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_DATE", objLeaves.DATE);
                        else
                            objParams[4] = new SqlParameter("@P_DATE", DBNull.Value);
                        objParams[5] = new SqlParameter("@P_REASON", objLeaves.REASON);
                        objParams[6] = new SqlParameter("@P_STATUS", objLeaves.STATUS);
                        objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objLeaves.COLLEGE_CODE);
                        objParams[8] = new SqlParameter("@P_PAPNO", objLeaves.PAPNO);
                        objParams[9] = new SqlParameter("@P_CREATEDBY", objLeaves.CREATEDBY);
                        objParams[10] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[11] = new SqlParameter("@P_IRREGULARITYSTATUS", objLeaves.IRREGULARITYSTATUS);
                        objParams[12] = new SqlParameter("@P_ISWEEKWISE", objLeaves.ISWEEKWISE);
                        objParams[13] = new SqlParameter("@P_INTIME", objLeaves.INTIME);
                        objParams[14] = new SqlParameter("@P_OUTTIME", objLeaves.OUTTIME);
                        objParams[15] = new SqlParameter("@P_SHIFTNO", objLeaves.SHIFTNO);
                        objParams[16] = new SqlParameter("@P_WORKHOURS", objLeaves.HOURS);
                        objParams[17] = new SqlParameter("@P_IRRTRNO", SqlDbType.Int);
                        objParams[17].Direction = ParameterDirection.Output;

                        int ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_APP_ENTRY_IRREGULARITY", objParams, true));

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = -99;
                        else
                            retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddAPP_ENTRY->" + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetIrregularityApplyListForTeaching(string frmdt, string todt, int idno, int deptno, int collegeno, int stno, int count)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];

                        objParams[0] = new SqlParameter("@P_FROM_DATE", frmdt);
                        objParams[1] = new SqlParameter("@P_TO_DATE", todt);
                        objParams[2] = new SqlParameter("@P_IDNO", idno);
                        objParams[3] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[4] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        objParams[5] = new SqlParameter("@P_STNO", stno);
                        objParams[6] = new SqlParameter("@P_COUNT", count);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_PAY_EMP_WEEKWISE_IRRIREGULAITY", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetIrregularityApplyListForTeaching->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetIrregularityApplyForTeaching(string frmdt, string todt, int idno, int deptno, int collegeno, int stno, int count, int SRNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];

                        objParams[0] = new SqlParameter("@P_FROM_DATE", frmdt);
                        objParams[1] = new SqlParameter("@P_TO_DATE", todt);
                        objParams[2] = new SqlParameter("@P_IDNO", idno);
                        objParams[3] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[4] = new SqlParameter("@P_COLLEGE_NO", collegeno);
                        objParams[5] = new SqlParameter("@P_STNO", stno);
                        objParams[6] = new SqlParameter("@P_COUNT", count);
                        objParams[7] = new SqlParameter("@P_SRNO", SRNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_PAY_EMP_WEEKWISE_IRRIREGULAITY_APPLY_BY_SRNO", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetIrregularityApplyForTeaching->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion

                #region Irregularity Approve

                public DataSet GetPendListforIrregularityApproval(int UA_No)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_No);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_APP_GET_IRREGULARITY_PENDINGLIST", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetPendListforIrregularityApproval->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetIrregularLeaveApproval(int IRRTRNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IRRTRNO", IRRTRNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_IRREGULAR_APL_GETBYNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetIrregularLeaveApproval->" + ex.ToString());

                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int UpdateIrregularityAppPassEntry(int IRRTRNO, int UA_NO, string STATUS, string REMARKS, DateTime APRDT, int p)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[6];
                        objparams[0] = new SqlParameter("@P_IRRTRNO", IRRTRNO);
                        objparams[1] = new SqlParameter("@P_UA_NO", UA_NO);
                        objparams[2] = new SqlParameter("@P_STATUS", STATUS);
                        objparams[3] = new SqlParameter("@P_REMARKS", REMARKS);
                        objparams[4] = new SqlParameter("@P_APRDT", APRDT);
                        objparams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[5].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_IRREGULARITY_PAY_APP_PASS_ENTRY_UPDATE", objparams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateLvAplAprStatus->" + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetALLIrregularityApproval(int UA_No)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_No);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_IRREGULARITY_PAY_APP_GET_PENDINGLIST_STATUS_ALL", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetPendListforIrregularityApproval->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetPendListforIrregularittyStatusParticular(int UA_No, string frmdt, string todt)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_No);
                        objParams[1] = new SqlParameter("@P_FROM_DATE", frmdt);
                        objParams[2] = new SqlParameter("@P_TODATE", todt);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_IRREGULAIRTY_PAY_APP_GET_PENDINGLIST_STATUS", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetPendListforLVApprovalStatusParticular->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                #endregion

                #region Directleavestatus
                public DataSet GetLeavesStatusForDirectLeave(int IDNO, int Year, int LNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[3];
                        objparams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objparams[1] = new SqlParameter("@P_YEAR ", Year);
                        objparams[2] = new SqlParameter("@P_LNO", LNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_BAL_LEAVESTATUS_FOR_DIRECTLEAVE", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLeavesStatus->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                #endregion

                #region EmployeewiseSaturday
                public DataSet GetEmployeeSaturdayWorkingDetails(int UANO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_UA_NO", UANO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_EMPLOYEEWISE_SATWORK_GET_ADD_WORKING_DETAILS", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetSaturdayWorkingDetails->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetEmployeeSaturdayWorkingDetailsById(int SWDID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_SWDID", SWDID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_Employeewise_SATWORK_GET_SATURDAY_WORKING_DETAILS_BYID", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetSaturdayWorkingDetailsById->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int DeleteEmployeeSaturday(int SWID)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_SWID", SWID);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_SATURDAYEMPLOYEE_WISE_DELETE", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.DeleteHoloday->" + ex.ToString());

                    }
                    return retstatus;
                }

                public int AddUpdateWorkingEmployeewiseHolidayDetailsByID(Leaves objLeave, string Remark, bool IsWorking, int swdid, DataTable dt)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SWDID", swdid);
                        objParams[1] = new SqlParameter("@P_STNO", objLeave.STNO);
                        objParams[2] = new SqlParameter("@P_WORKINGDATE", objLeave.DATE);
                        objParams[3] = new SqlParameter("@P_REMARK", Remark);
                        objParams[4] = new SqlParameter("@P_CREATEDBY", objLeave.CreatedBy);
                        objParams[5] = new SqlParameter("@P_MODIFYBY", objLeave.ModifiedBy);
                        objParams[6] = new SqlParameter("@P_HOLIDAY_WORKING_RECORD_IDNO", dt);
                        objParams[7] = new SqlParameter("@P_OrganizationId", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[8] = new SqlParameter("@P_ISEMPWISESAT", objLeave.ISEMPWISESAT);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SATWORK_EMPLOYEEWISE_ADD_UPDATE_WORKINGDAY_BY_IDNO", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == -1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.Add_Update_AdmissionDate -> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion

                #region ServicebookExcel
                public DataSet ServicebookDataExcel(int Collegeno, int DEPT, int STNO, int IDNO, string ServiceCategory, Boolean HighestQual)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[6];
                        objparams[0] = new SqlParameter("@P_COLLEGE_NO", Collegeno);
                        objparams[1] = new SqlParameter("@P_DEPT", DEPT);
                        objparams[2] = new SqlParameter("@P_STNO ", STNO);
                        objparams[3] = new SqlParameter("@P_IDNO", IDNO);
                        objparams[4] = new SqlParameter("@P_ServiceCategory", ServiceCategory);
                        objparams[5] = new SqlParameter("@P_highest", HighestQual);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_SERVICEBOOK_ALL_EXCEL", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.ServicebookDataExcel->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion

                #region Leave Application

                // Added BY Shrikant Bharne.
                public DataSet GetRestricatedHoliday(DateTime fromdt, DateTime todt, int stno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_FROMDT", fromdt);
                        objParams[1] = new SqlParameter("@P_TODT", todt);
                        objParams[2] = new SqlParameter("@P_STNO", stno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_RESTRICATED_HOLIDAY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeaveController.GetSaturdayWorkingDate-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                #region Permission entry
                public DataSet RetrieveSinglepermission(int PERTNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PERTNO", PERTNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PER_GET_SINGLE_PERMISSION", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CPDAController.RetrieveSingleBlockName->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;


                }


                public DataSet GetPermissionDetails(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_GET_PERMISSION_ENTRY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CPDAController.GetCPDADApplicationDetails-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetUserPermissionDetails(int UA_NO, int UA_TYPE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[1] = new SqlParameter("@P_UA_TYPE", UA_TYPE);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_CPDA_GET_SINGLE_PERMISSION_INFO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CPDAController.GetUserCPDADetails-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int AddPermissionEntry(Leaves objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[10];
                        if (!objLeave.DATE.Equals(DateTime.MinValue))
                            objParams[0] = new SqlParameter("@P_DATE", objLeave.DATE);
                        else
                            objParams[0] = new SqlParameter("@P_DATE", DBNull.Value);

                        if (!objLeave.DATE.Equals(DateTime.MinValue))
                            objParams[1] = new SqlParameter("@P_APRDATE", objLeave.APRDT);
                        else
                            objParams[1] = new SqlParameter("@P_APRDATE", DBNull.Value);
                        objParams[2] = new SqlParameter("@P_EMPNO", objLeave.EMPNO);

                        objParams[3] = new SqlParameter("@P_REASON", objLeave.REASON);
                        objParams[4] = new SqlParameter("@P_STATUS", objLeave.STATUS);
                        objParams[5] = new SqlParameter("@P_PAPNO", objLeave.PAPNO);
                        objParams[6] = new SqlParameter("@P_CREATEDBY", objLeave.CreatedBy);
                        objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objLeave.COLLEGE_CODE);
                        objParams[8] = new SqlParameter("@P_Dayselection", objLeave.Dayselection);
                        objParams[9] = new SqlParameter("@P_PERTNO", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_PERMISSION_PAY_APP_ENTRY_INSERT1", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddHolidayType->" + ex.ToString());
                    }
                    return retstatus;
                }

                public int UpdatePermission(Leaves objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[5];


                        objparams[0] = new SqlParameter("@P_PERTNO", objLeave.PERTNO);
                        if (!objLeave.DATE.Equals(DateTime.MinValue))
                            objparams[1] = new SqlParameter("@P_DATE", objLeave.DATE);
                        else
                            objparams[1] = new SqlParameter("@P_DATE", DBNull.Value);
                        objparams[2] = new SqlParameter("@P_REASON", objLeave.REASON);
                        objparams[3] = new SqlParameter("@P_Dayselection", objLeave.Dayselection);
                        objparams[4] = new SqlParameter("@P_ModifiedBy", objLeave.MODIFIEDBY);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_PERM_UPDATE_PERMISSION", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CPDAController.UpdateBlockName->" + ex.ToString());

                    }
                    return retstatus;
                }


                public DataSet GetPermissionCount(int empno, DateTime DATE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_EMPNO", empno);
                        objParams[1] = new SqlParameter("@P_DATE", DATE);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PERM_CHECK_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CPDAController.GetUserCPDADetails-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion

                #region PerApproval
                public DataSet GetPendListforPermissionApproval(int UA_No)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_No);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_PERM_PAY_APP_GET_PENDINGLIST", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetPendListforLeaveApproval->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetPendListforperApprovalStatusALL(int UA_No)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_No);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_PERM_PAY_APP_GET_PENDINGLIST_STATUS_ALL", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetPendListforLVApprovalStatusALL->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetPendListforPermissionApprovalStatusParticular(int UA_No, string frmdt, string todt)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_No);
                        objParams[1] = new SqlParameter("@P_FROM_DATE", frmdt);
                        objParams[2] = new SqlParameter("@P_TODATE", todt);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_PERMISSION_PAY_APP_GET_PENDINGLIST_STATUS", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetPendListforLVApprovalStatusParticular->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetPerApplDetail(int PERTNO)
                //fetch  Leave Application details of Particular Leave aaplication by Passing LETRNO & its approval status
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PERTNO", PERTNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_PERMISSION_PAY_LEAVEAPLDTL_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLeaveApplDetail->" + ex.ToString());

                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int UpdateAppperPassEntry(int PERTNO, int UA_NO, string STATUS, string REMARKS, DateTime APRDT, int p)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[6];
                        objparams[2] = new SqlParameter("@P_STATUS", STATUS);
                        objparams[3] = new SqlParameter("@P_REMARKS", REMARKS);
                        objparams[4] = new SqlParameter("@P_APRDT", APRDT);
                        objparams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[0] = new SqlParameter("@P_PERTNO", PERTNO);
                        objparams[1] = new SqlParameter("@P_UA_NO", UA_NO);
                        objparams[2] = new SqlParameter("@P_STATUS", STATUS);
                        objparams[3] = new SqlParameter("@P_REMARKS", REMARKS);
                        objparams[4] = new SqlParameter("@P_APRDT", APRDT);
                        objparams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[5].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_PERMISSION_PAY_APP_PASS_ENTRY_UPDATE1", objparams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_APP_PASS_ENTRY_UPDATE1", objparams, false) != null)
                        //    retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateLvAplAprStatus->" + ex.ToString());
                    }
                    return retStatus;

                }
                public DataSet GetPendListforPERMISSApprovalStatusParticular(int UA_No, string frmdt, string todt)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_No);
                        objParams[1] = new SqlParameter("@P_FROM_DATE", frmdt);
                        objParams[2] = new SqlParameter("@P_TODATE", todt);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_PERMISSION_PAY_APP_GET_ALL_STATUS", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetPendListforLVApprovalStatusParticular->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                #endregion

                #region DirectApproval
                //--------------------------------------
                public DataSet GetPendListforODDirectApproval(int deptno, int STNO, string dt, int collegeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[1] = new SqlParameter("@P_STNO", STNO);
                        objParams[2] = new SqlParameter("@P_LVDATE", dt);
                        objParams[3] = new SqlParameter("@P_COLLEGENO", collegeno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_OD_GET_PENDINGLIST_DIRECT_APPROVAL", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetPendListforODDirectApproval->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public int UpdateAppPassEntryDirectApprvlOD(int ODTRNO, int UA_NO, string STATUS, string REMARKS, DateTime APRDT, int p)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[6];
                        objparams[0] = new SqlParameter("@P_ODTRNO", ODTRNO);
                        objparams[1] = new SqlParameter("@P_UA_NO", UA_NO);
                        objparams[2] = new SqlParameter("@P_STATUS", STATUS);
                        objparams[3] = new SqlParameter("@P_REMARKS", REMARKS);
                        objparams[4] = new SqlParameter("@P_APRDT", APRDT);
                        objparams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[5].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_APP_PASS_ENTRY_UPD_DIRECT_APPRVL_OD", objparams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_APP_PASS_ENTRY_UPDATE1", objparams, false) != null)
                        //    retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.UpdateLvAplAprStatus->" + ex.ToString());
                    }
                    return retStatus;

                }
                public DataSet GetODApplDetailDirectApproval(int ODTRNO)
                //fetch  Leave Application details of Particular Leave aaplication by Passing LETRNO & its approval status
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ODTRNO", ODTRNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_OD_PAY_ODAPLDTL_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLeaveApplDetail->" + ex.ToString());

                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                #endregion

                #region Leave Load

                public DataSet GetEmployeeScheduleList(int UA_No, DateTime FromDate, DateTime ToDate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_UANO", UA_No);
                        objParams[1] = new SqlParameter("@P_START_DATE", FromDate);
                        objParams[2] = new SqlParameter("@P_END_DATE", ToDate);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_FACULTYWISE_TIMETABLE", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetPendListforIrregularityApproval->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetLeaveValidCount(int empno, DateTime FromDate, int LNO, int letrno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_EMPNO", empno);
                        objParams[1] = new SqlParameter("@P_FROM_DATE", FromDate);
                        objParams[2] = new SqlParameter("@P_LNO", LNO);
                        objParams[3] = new SqlParameter("@P_LETRNO", letrno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_LEAVE_CHECK_VALID_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CPDAController.GetUserCPDADetails-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                #endregion

                public DataSet GetLeavesStatusForLWPApply(int IDNO, int Year, int LNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[3];
                        objparams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objparams[1] = new SqlParameter("@P_YEAR ", Year);
                        objparams[2] = new SqlParameter("@P_LNO", LNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_PAY_BAL_LEAVESTATUS_VALID_FOR_LWP_APPLY", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLeavesStatus->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int AddHolidayTest(Leaves objLeave)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[11];
                        objparams[0] = new SqlParameter("@P_HOLIDAYNAME", objLeave.HOLIDAYNAME);

                        if (!objLeave.HDT.Equals(DateTime.MinValue))
                            objparams[1] = new SqlParameter("@P_DT", objLeave.HDT);
                        else
                            objparams[1] = new SqlParameter("@P_DT", DBNull.Value);

                        //objparams[1]=new SqlParameter("@P_DT",objLeave.HDT);

                        if (!objLeave.TODT.Equals(DateTime.MinValue))
                            objparams[2] = new SqlParameter("@P_TODT", objLeave.TODT);
                        else
                            objparams[2] = new SqlParameter("@P_TODT", DBNull.Value);

                        objparams[3] = new SqlParameter("@P_YEAR", objLeave.YEAR);
                        objparams[4] = new SqlParameter("@P_PERIOD", objLeave.PERIOD);
                        objparams[5] = new SqlParameter("@P_COLLEGE_CODE", objLeave.COLLEGE_CODE);
                        objparams[6] = new SqlParameter("@P_HTNO", objLeave.HTNO);
                        objparams[7] = new SqlParameter("@P_STNO", objLeave.STNO);
                        objparams[8] = new SqlParameter("@P_RESTRICT_STATUS", objLeave.STATUS);
                        objparams[9] = new SqlParameter("@P_COLLEGE_NO", objLeave.COLLEGE_NO);
                        objparams[10] = new SqlParameter("@P_UA_NO", objLeave.UANO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_PAY_HOLIDAYS_INSERT_NEW", objparams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.AddHoliday->" + ex.ToString());
                    }
                    return retstatus;
                }

                #region ChargeMail
                public DataSet GetChargeEmailInformation(Leaves objLM)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_LETRNO", objLM.LETRNO);
                        //ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_GET_MAIL_LOAD_INFORMATION", objParams);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_CHARGE_GET_EMAIL_INFORMATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetEmailInformation->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                #endregion

                #region ContinouesLeaveApplicationValidation
                public DataSet CheckIsLeaveCont(DateTime fromdt, DateTime todt, DateTime joindt, int stno, int empno, int letrno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_FROMDT", fromdt);
                        objParams[1] = new SqlParameter("@P_TODT", todt);
                        objParams[2] = new SqlParameter("@P_JOINDT", joindt);
                        objParams[3] = new SqlParameter("@P_STNO", stno);
                        objParams[4] = new SqlParameter("@P_Empid", empno);
                        objParams[5] = new SqlParameter("@P_LETRNO", letrno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_CHECK_LEAVE_CONTINUE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeaveController.GetSaturdayWorkingDate-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                #region Shift Management

                public DataSet GetNoofdays_Shift(DateTime Frdate, DateTime Todate, string leavetype, int calholy, int FNAN, int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_FRDATE", Frdate);
                        objParams[1] = new SqlParameter("@P_TODATE", Todate);
                        objParams[2] = new SqlParameter("@P_LEAVETYPE", leavetype);
                        objParams[3] = new SqlParameter("@P_CAL_HOLIDAY", calholy);
                        objParams[4] = new SqlParameter("@P_FNAN", FNAN);
                        objParams[5] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_NO_OF_DAYS_SHIFT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetNoofdays_Shift->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int GetAllowDays_Shift(Leaves objLeaves)
                {
                    int ret = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];

                        if (!objLeaves.JOINDT.Equals(DateTime.MinValue))
                            objParams[0] = new SqlParameter("@P_JOINDT", objLeaves.JOINDT);
                        else
                            objParams[0] = new SqlParameter("@P_JOINDT", DBNull.Value);

                        if (!objLeaves.FROMDT.Equals(DateTime.MinValue))
                            objParams[1] = new SqlParameter("@P_FROM_DATE", objLeaves.FROMDT);
                        else
                            objParams[1] = new SqlParameter("@P_FROM_DATE", DBNull.Value);

                        objParams[2] = new SqlParameter("@P_ALLOW_DAYS", objLeaves.NO_DAYS);
                        objParams[3] = new SqlParameter("@P_STNO", objLeaves.STNO);
                        objParams[4] = new SqlParameter("@P_COLLEGENO", objLeaves.COLLEGE_NO);
                        objParams[5] = new SqlParameter("@P_IS_ALLOW_BEFORE_APPLICATION", objLeaves.IsAllowBeforeApplication);

                        objParams[6] = new SqlParameter("@P_IDNO", objLeaves.EMPNO);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                        ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_NO_OF_ALLOWDAYS_SHIFT", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        ret = -99;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetAllowDays->" + ex.ToString());
                    }
                    return ret;
                }

                #endregion

                #region Leave Balance Report

                public DataSet LeaveEmployeeBalanceForExport(int idno, int lvno, int Year, int periodno, int stno, int deptno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[6];
                        objparams[0] = new SqlParameter("@P_IDNO", idno);
                        objparams[1] = new SqlParameter("@P_LVNO ", lvno);
                        objparams[2] = new SqlParameter("@P_YEAR", Year);
                        objparams[3] = new SqlParameter("@P_PERIOD", periodno);
                        objparams[4] = new SqlParameter("@P_STNO", stno);
                        objparams[5] = new SqlParameter("@P_Deptno", deptno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_BALANCE_REPORT_TOEXCEL", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LeavesController.GetLeavesStatus->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion 

                #region No of days for shift

                public DataSet GetNoofdays_Shift(DateTime Frdate, DateTime Todate, string leavetype, int calholy, int FNAN, int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_FRDATE", Frdate);
                        objParams[1] = new SqlParameter("@P_TODATE", Todate);
                        objParams[2] = new SqlParameter("@P_LEAVETYPE", leavetype);
                        objParams[3] = new SqlParameter("@P_CAL_HOLIDAY", calholy);
                        objParams[4] = new SqlParameter("@P_FNAN", FNAN);
                        objParams[5] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTB_LEAVE_NO_OF_DAYS_SHIFT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetNoofdays_Shift->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int GetAllowDays_Shift(Leaves objLeaves)
                {
                    int ret = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];

                        if (!objLeaves.JOINDT.Equals(DateTime.MinValue))
                            objParams[0] = new SqlParameter("@P_JOINDT", objLeaves.JOINDT);
                        else
                            objParams[0] = new SqlParameter("@P_JOINDT", DBNull.Value);

                        if (!objLeaves.FROMDT.Equals(DateTime.MinValue))
                            objParams[1] = new SqlParameter("@P_FROM_DATE", objLeaves.FROMDT);
                        else
                            objParams[1] = new SqlParameter("@P_FROM_DATE", DBNull.Value);

                        objParams[2] = new SqlParameter("@P_ALLOW_DAYS", objLeaves.NO_DAYS);
                        objParams[3] = new SqlParameter("@P_STNO", objLeaves.STNO);
                        objParams[4] = new SqlParameter("@P_COLLEGENO", objLeaves.COLLEGE_NO);
                        objParams[5] = new SqlParameter("@P_IS_ALLOW_BEFORE_APPLICATION", objLeaves.IsAllowBeforeApplication);

                        objParams[6] = new SqlParameter("@P_IDNO", objLeaves.EMPNO);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                        ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ESTB_LEAVE_NO_OF_ALLOWDAYS_SHIFT", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        ret = -99;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetAllowDays->" + ex.ToString());
                    }
                    return ret;
                }
                #endregion
            }
        }
    }
}
