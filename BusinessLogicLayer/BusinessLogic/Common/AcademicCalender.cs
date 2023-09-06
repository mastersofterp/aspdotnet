//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : ACADEMIC CALENDAR
// CREATION DATE : 06-SEP-2017
// CREATED BY    : ROHIT KUMAR TIWARI
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;

using IITMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class AcademicCalender
            {
                private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                /// Added By "ROHIT KUMAR TIWARI" on 06-SEP-2017
                ///  Method for Adding Acdamic Calendar Schedule
                /// </summary>
                /// <param name="unitno"></param>
                /// <param name="uano"></param>
                /// <param name="lectureno"></param>
                /// <param name="subid"></param>
                /// <param name="degreeno"></param>
                /// <param name="branchno"></param>
                /// <param name="topiccovered"></param>
                /// <param name="reference"></param>
                /// <param name="colcode"></param>
                /// <returns></returns>
                public int Add_AcademicCalendar(int batchnono, int degreeno, string activitytype, string activity, string generalschedule, DateTime fromdate, DateTime todate, string colcode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_BATCHNO", batchnono);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_ACTIVITY_TYPE", activitytype);
                        objParams[3] = new SqlParameter("@P_ACTIVITY", activity);
                        objParams[4] = new SqlParameter("@P_GENERAL_SCHEDULE", generalschedule);
                        objParams[5] = new SqlParameter("@P_FROMDATE", fromdate);
                        objParams[6] = new SqlParameter("@P_TODATE", todate);
                        objParams[7] = new SqlParameter("@P_COLLEGE_CODE", colcode);
                        objParams[8] = new SqlParameter("@P_SRNO", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_CALENDAR_SCHEDULE_INSERT", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AcademicCalendar.Add_AcademicCalendar -> " + ex.ToString());
                    }
                    return retStatus;
                }
                /// Added By "ROHIT KUMAR TIWARI" on 06-SEP-2017
                ///  Method for Updating Acdamic Calendar Schedule
                /// </summary>
                /// <param name="srno"></param>
                /// <param name="batchnono"></param>
                /// <param name="degreeno"></param>
                /// <param name="activitytype"></param>
                /// <param name="activity"></param>
                /// <param name="generalschedule"></param>
                /// <param name="dateduration"></param>
                /// <param name="colcode"></param>
                /// <returns></returns>
                public int Update_AcademicCalendar(int srno, int batchnono, int degreeno, string activitytype, string activity, string generalschedule, DateTime dateduration, string colcode, DateTime todateduration)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SRNO", srno);
                        objParams[1] = new SqlParameter("@P_BATCHNO", batchnono);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_ACTIVITY_TYPE", activitytype);
                        objParams[4] = new SqlParameter("@P_ACTIVITY", activity);
                        objParams[5] = new SqlParameter("@P_GENERAL_SCHEDULE", generalschedule);
                        objParams[6] = new SqlParameter("@P_DATE_DUTRATION", dateduration);
                        objParams[7] = new SqlParameter("@P_TO_DATE_DUTRATION", todateduration);
                        objParams[8] = new SqlParameter("@P_COLLEGE_CODE", colcode);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_CALENDAR_SCHEDULE_UPDATE", objParams, true);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AcademicCalendar.Update_AcademicCalendar -> " + ex.ToString());
                    }
                    return retStatus;
                }
                /// Added By "ROHIT KUMAR TIWARI" on 06-SEP-2017
                ///  Method for Get All Details of Academic Schedule 
                /// </summary>
                /// <returns></returns>

                public DataSet GetAll_AcademicCalendarDetails(string activitytype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];

                        objParams[0] = new SqlParameter("@P_ACTIVITY_TYPE", activitytype);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_CALENDAR_SCHEDULE_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        //return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AcademicCalendar.GetAll_AcademicCalendarDetails-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                /// Added By "ROHIT KUMAR TIWARI" on 23-SEP-2017
                ///  Method for Getting Single Record from Academic Calendar
                /// 
                /// </summary>
                /// <param name="srno"></param>
                /// <returns></returns>
                public DataSet GetSingle_AcademicCalendarDetails(int srno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SRNO", srno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_ACD_CALENDAR_SCHEDULE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AcademicCalendar.GetSingle_AcademicCalendarDetails -> " + ex.ToString());
                    }
                    return ds;
                }

            }
        }
    }
}
