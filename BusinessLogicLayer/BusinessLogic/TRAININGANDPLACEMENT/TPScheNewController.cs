




//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS LOGIC FILE //[Training & Placement Schedule New ]                                  
// CREATION DATE : 29-march-2015                                                       
// CREATED BY    : Swati GHAte                     
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
            public class TPScheNewController
            {
                /// <SUMMARY>
                /// ConnectionStrings
                /// </SUMMARY>
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                public int AddComp_Schedule_New(TrainingPlacement objTP)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[23];
                        //objParams[0] = new SqlParameter("@P_SCHEDULEDATE", objTP.SCHEDULEDATE);
                        if (objTP.SCHEDULEDATE.Equals(string.Empty) || (objTP.SCHEDULEDATE == null))
                            objParams[0] = new SqlParameter("@P_SCHEDULEDATE", DBNull.Value);
                        else
                            objParams[0] = new SqlParameter("@P_SCHEDULEDATE", objTP.SCHEDULEDATE);

                        if (objTP.SCHEDULETIME.Equals(string.Empty) || (objTP.SCHEDULETIME == null))
                            objParams[1] = new SqlParameter("@P_SCHEDULETIME", DBNull.Value);
                        else
                            objParams[1] = new SqlParameter("@P_SCHEDULETIME", objTP.SCHEDULETIME);


                        //objParams[2] = new SqlParameter("@P_INTERVIEWFROM", objTP.INTERVIEWFROM);
                        if (objTP.INTERVIEWFROM.Equals(string.Empty) || (objTP.INTERVIEWFROM == null))
                            objParams[2] = new SqlParameter("@P_INTERVIEWFROM", DBNull.Value);
                        else
                            objParams[2] = new SqlParameter("@P_INTERVIEWFROM", objTP.INTERVIEWFROM);

                        //objParams[3] = new SqlParameter("@P_INTERVIEWTO", objTP.INTERVIEWTO);

                        if (objTP.INTERVIEWTO.Equals(string.Empty) || (objTP.INTERVIEWTO == null))
                            objParams[3] = new SqlParameter("@P_INTERVIEWTO", DBNull.Value);
                        else
                            objParams[3] = new SqlParameter("@P_INTERVIEWTO", objTP.INTERVIEWTO);

                        objParams[4] = new SqlParameter("@P_COMPID", objTP.COMPID);

                        objParams[5] = new SqlParameter("@P_UGPG", objTP.UGPG);
                        objParams[6] = new SqlParameter("@P_BRANCH", objTP.BRANCH);

                        objParams[7] = new SqlParameter("@P_BOND", objTP.BOND);
                        objParams[8] = new SqlParameter("@P_BONDDETAILS", objTP.BONDDETAILS);

                        objParams[9] = new SqlParameter("@P_UGSTIPEND", objTP.UGSTIPEND);
                        objParams[10] = new SqlParameter("@P_UGSALARY", objTP.UGSALARY);
                        objParams[11] = new SqlParameter("@P_PGSTIPEND", objTP.PGSTIPEND);
                        objParams[12] = new SqlParameter("@P_PGSALARY", objTP.PGSALARY);
                        objParams[13] = new SqlParameter("@P_SCHEDULESTATUS", objTP.SCHEDULESTATUS);
                        objParams[14] = new SqlParameter("@P_COLLEGE_CODE", objTP.COLLEGE_CODE);

                        if ((objTP.LASTDATE.Equals(string.Empty)) || (objTP.LASTDATE == null))
                            objParams[15] = new SqlParameter("@P_LASTDATE", DBNull.Value);
                        else
                            objParams[15] = new SqlParameter("@P_LASTDATE", objTP.LASTDATE);


                        objParams[16] = new SqlParameter("@P_VENUE", objTP.Venue);


                        if (string.IsNullOrEmpty(objTP.FileName))
                            objParams[17] = new SqlParameter("@P_FILENAME", DBNull.Value);
                        else
                            objParams[17] = new SqlParameter("@P_FILENAME", objTP.FileName);

                        objParams[18] = new SqlParameter("@P_CURRENCYSALARY", objTP.CurrencySalary);
                        objParams[19] = new SqlParameter("@P_CURRENCYSTIPEND", objTP.CurrencyStipend);

                        objParams[20] = new SqlParameter("@P_REQUIREMENT", objTP.REQUIREMENT);
                        objParams[21] = new SqlParameter("@P_SELECTNO", objTP.SELECTNO);

                        objParams[22] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[22].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_COMPSCHEDULE_INSERT_NEW", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(ret); //Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(ret);//Convert.ToInt32(CustomStatus.RecordUpdated);                        

                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddComp_Schedule-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetBranch(string BType)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TYPE", BType);
                        ds = objSQLHelper.ExecuteDataSetSP("ACAD_TP_BRANCH_GET_BY_TYPE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetBranch-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public DataSet GetCompName(int uaidno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UIDNO", uaidno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COMPANY_NAME_BY_UID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetBranch-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int UpdateComp_Schedule_New(TrainingPlacement objTP)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[24];
                        if (objTP.SCHEDULEDATE.Equals(string.Empty) || (objTP.SCHEDULEDATE == null))
                            objParams[0] = new SqlParameter("@P_SCHEDULEDATE", DBNull.Value);
                        else
                            objParams[0] = new SqlParameter("@P_SCHEDULEDATE", objTP.SCHEDULEDATE);

                        if (objTP.SCHEDULETIME.Equals(string.Empty) || (objTP.SCHEDULETIME == null))
                            objParams[1] = new SqlParameter("@P_SCHEDULETIME", DBNull.Value);
                        else
                            objParams[1] = new SqlParameter("@P_SCHEDULETIME", objTP.SCHEDULETIME);


                        //objParams[2] = new SqlParameter("@P_INTERVIEWFROM", objTP.INTERVIEWFROM);
                        if (objTP.INTERVIEWFROM.Equals(string.Empty) || (objTP.INTERVIEWFROM == null))
                            objParams[2] = new SqlParameter("@P_INTERVIEWFROM", DBNull.Value);
                        else
                            objParams[2] = new SqlParameter("@P_INTERVIEWFROM", objTP.INTERVIEWFROM);

                        //objParams[3] = new SqlParameter("@P_INTERVIEWTO", objTP.INTERVIEWTO);

                        if (objTP.INTERVIEWTO.Equals(string.Empty) || (objTP.INTERVIEWTO == null))
                            objParams[3] = new SqlParameter("@P_INTERVIEWTO", DBNull.Value);
                        else
                            objParams[3] = new SqlParameter("@P_INTERVIEWTO", objTP.INTERVIEWTO);

                        objParams[4] = new SqlParameter("@P_COMPID", objTP.COMPID);

                        objParams[5] = new SqlParameter("@P_UGPG", objTP.UGPG);
                        objParams[6] = new SqlParameter("@P_BRANCH", objTP.BRANCH);

                        objParams[7] = new SqlParameter("@P_BOND", objTP.BOND);
                        objParams[8] = new SqlParameter("@P_BONDDETAILS", objTP.BONDDETAILS);

                        objParams[9] = new SqlParameter("@P_UGSTIPEND", objTP.UGSTIPEND);
                        objParams[10] = new SqlParameter("@P_UGSALARY", objTP.UGSALARY);
                        objParams[11] = new SqlParameter("@P_PGSTIPEND", objTP.PGSTIPEND);
                        objParams[12] = new SqlParameter("@P_PGSALARY", objTP.PGSALARY);
                        objParams[13] = new SqlParameter("@P_SCHEDULESTATUS", objTP.SCHEDULESTATUS);
                        objParams[14] = new SqlParameter("@P_COLLEGE_CODE", objTP.COLLEGE_CODE);

                        if ((objTP.LASTDATE.Equals(string.Empty)) || (objTP.LASTDATE == null))
                            objParams[15] = new SqlParameter("@P_LASTDATE", DBNull.Value);
                        else
                            objParams[15] = new SqlParameter("@P_LASTDATE", objTP.LASTDATE);


                        objParams[16] = new SqlParameter("@P_VENUE", objTP.Venue);


                        if (string.IsNullOrEmpty(objTP.FileName))
                            objParams[17] = new SqlParameter("@P_FILENAME", DBNull.Value);
                        else
                            objParams[17] = new SqlParameter("@P_FILENAME", objTP.FileName);

                        objParams[18] = new SqlParameter("@P_CURRENCYSALARY", objTP.CurrencySalary);
                        objParams[19] = new SqlParameter("@P_CURRENCYSTIPEND", objTP.CurrencyStipend);
                        objParams[20] = new SqlParameter("@P_REQUIREMENT", objTP.REQUIREMENT);
                        objParams[21] = new SqlParameter("@P_SELECTNO", objTP.SELECTNO);
                        objParams[22] = new SqlParameter("@P_SCHEDULENO", objTP.SCHEDULENO);

                        objParams[23] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[23].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_COMPSCHEDULE_UPDATE_NEW", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(ret); //Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(ret);//Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddComp_Schedule-> " + ex.ToString());
                    }
                    return retStatus;

                }

                public DataSet GetCompSheduleByCompany(int COMPID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COMPID", COMPID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_COMPSCHEDULE_GET_BY_COMP", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetCompScedule-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                //Lock Unlock student profile acording to its div

                public int UpdateStudentLockUnlock(TPStudent objTPStud)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_IDNO", objTPStud.IdNo);
                        objParams[1] = new SqlParameter("@P_EXAM_LOCK_UNLOCK", objTPStud.EXAM_LOCK_UNLOCK);
                        objParams[2] = new SqlParameter("@P_WORK_EXP_LOCK_UNLOCK", objTPStud.WORK_EXP_LOCK_UNLOCK);
                        objParams[3] = new SqlParameter("@P_TECH_SKIL_LOCK_UNLOCK", objTPStud.TECH_SKIL_LOCK_UNLOCK);
                        objParams[4] = new SqlParameter("@P_PROJECT_LOCK_UNLOCK", objTPStud.PROJECT_LOCK_UNLOCK);
                        objParams[5] = new SqlParameter("@P_CERTIFICATION_LOCK_UNLOCK", objTPStud.CERTIFICATION_LOCK_UNLOCK);
                        objParams[6] = new SqlParameter("@P_LANGUAGE_LOCK_UNLOCK", objTPStud.LANGUAGE_LOCK_UNLOCK);
                        objParams[7] = new SqlParameter("@P_AWARD_LOCK_UNLOCK", objTPStud.AWARD_LOCK_UNLOCK);
                        objParams[8] = new SqlParameter("@P_COMPETITION_LOCK_UNLOCK", objTPStud.COMPETITION_LOCK_UNLOCK);
                        objParams[9] = new SqlParameter("@P_TRAINING_LOCK_UNLOCK", objTPStud.TRAINING_LOCK_UNLOCK);
                        objParams[10] = new SqlParameter("@P_TEST_SCORE_LOCK_UNLOCK", objTPStud.TEST_SCORE_LOCK_UNLOCK);
                        objParams[11] = new SqlParameter("@P_BUILD_RESUME_LOCK_UNLOCK", objTPStud.BUILD_RESUME_LOCK_UNLOCK);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_TP_STUDENT_PROFILE_LOCK_UNLOCK", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(ret); //Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(ret);//Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddComp_Schedule-> " + ex.ToString());
                    }
                    return retStatus;

                }
            }
        }
    }
}