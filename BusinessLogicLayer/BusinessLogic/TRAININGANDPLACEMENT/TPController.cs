//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS LOGIC FILE //[Training & Placement TPController ]                                  
// CREATION DATE : 19-NOV-2009                                                        
// CREATED BY    : JAYANT DHOMNE                                   
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
            public class TPController
            {
                /// <SUMMARY>
                /// ConnectionStrings
                /// </SUMMARY>
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                #region TPCompany

                public int AddCompanyReg(TrainingPlacement objTP)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[30];
                        objParams[0] = new SqlParameter("@P_COMPCATNO", objTP.COMPCATNO);
                        objParams[1] = new SqlParameter("@P_COMPCODE", objTP.COMPCODE);
                        objParams[2] = new SqlParameter("@P_COMPNAME", objTP.COMPNAME);
                        objParams[3] = new SqlParameter("@P_COMPADD", objTP.COMPADD);
                        objParams[4] = new SqlParameter("@P_COMPDIRECTOR", objTP.COMPDIRECTOR);
                        objParams[5] = new SqlParameter("@P_CITYNO", objTP.CITYNO);
                        objParams[6] = new SqlParameter("@P_CITY", objTP.CITY);
                        objParams[7] = new SqlParameter("@P_PINCODE", objTP.PINCODE);
                        objParams[8] = new SqlParameter("@P_PHONENO", objTP.PHONENO);
                        objParams[9] = new SqlParameter("@P_FAXNO", objTP.FAXNO);
                        objParams[10] = new SqlParameter("@P_EMAILID", objTP.EMAILID);
                        objParams[11] = new SqlParameter("@P_WEBSITE", objTP.WEBSITE);
                        objParams[12] = new SqlParameter("@P_SALRANGE", objTP.SALRANGE);
                        objParams[13] = new SqlParameter("@P_CONTPERSON", objTP.CONTPERSON);
                        objParams[14] = new SqlParameter("@P_CONTDESIGNATION", objTP.CONTDESIGNATION);

                        objParams[15] = new SqlParameter("@P_CONTADDRESS", objTP.CONTADDRESS);
                        objParams[16] = new SqlParameter("@P_CONTPHONE", objTP.CONTPHONE);
                        objParams[17] = new SqlParameter("@P_CONTMAILID", objTP.CONTMAILID);
                        objParams[18] = new SqlParameter("@P_OTHERINFO", objTP.OTHERINFO);
                        objParams[19] = new SqlParameter("@P_REMARK", objTP.REMARK);
                        objParams[20] = new SqlParameter("@P_COLLEGE_CODE", objTP.COLLEGE_CODE);

                        objParams[21] = new SqlParameter("@P_INPLANT", objTP.INPLANT);
                        objParams[22] = new SqlParameter("@P_IPNAME", objTP.IPCONTPERSON);
                        objParams[23] = new SqlParameter("@P_IPDESIGNATION", objTP.IPCONTDESIGNATION);
                        objParams[24] = new SqlParameter("@P_IPADDRESS", objTP.IPCONTADDRESS);
                        objParams[25] = new SqlParameter("@P_IPPHONE", objTP.IPCONTPHONE);
                        objParams[26] = new SqlParameter("@P_IPMAILID", objTP.IPCONTMAILID);
                        objParams[27] = new SqlParameter("@P_PLACEMENTCONTNO", objTP.PLACEMENTCONTNO);
                        objParams[28] = new SqlParameter("@P_BRANCHNO", objTP.BRANCHNO);
                        objParams[29] = new SqlParameter("@P_COMPID", SqlDbType.Int);
                        objParams[29].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_COMPANYREG_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        //     
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddComapny->" + ex.ToString());
                    }
                    return retStatus;
                }


                //To Add Company (Company Registration)by T & P
                public int AddCompany(TrainingPlacement objTP, string BrNo)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[33];
                        objParams[0] = new SqlParameter("@P_COMPCATNO", objTP.COMPCATNO);
                        objParams[1] = new SqlParameter("@P_COMPCODE", objTP.COMPCODE);
                        objParams[2] = new SqlParameter("@P_COMPNAME", objTP.COMPNAME);
                        objParams[3] = new SqlParameter("@P_COMPADD", objTP.COMPADD);
                        objParams[4] = new SqlParameter("@P_COMPDIRECTOR", objTP.COMPDIRECTOR);
                        objParams[5] = new SqlParameter("@P_CITYNO", objTP.CITYNO);
                        objParams[6] = new SqlParameter("@P_CITY", objTP.CITY);
                        objParams[7] = new SqlParameter("@P_PINCODE", objTP.PINCODE);
                        objParams[8] = new SqlParameter("@P_PHONENO", objTP.PHONENO);
                        objParams[9] = new SqlParameter("@P_FAXNO", objTP.FAXNO);
                        objParams[10] = new SqlParameter("@P_EMAILID", objTP.EMAILID);
                        objParams[11] = new SqlParameter("@P_WEBSITE", objTP.WEBSITE);
                        objParams[12] = new SqlParameter("@P_SALRANGE", objTP.SALRANGE);
                        objParams[13] = new SqlParameter("@P_CONTPERSON", objTP.CONTPERSON);
                        objParams[14] = new SqlParameter("@P_CONTDESIGNATION", objTP.CONTDESIGNATION);
                        objParams[15] = new SqlParameter("@P_CONTADDRESS", objTP.CONTADDRESS);
                        objParams[16] = new SqlParameter("@P_CONTPHONE", objTP.CONTPHONE);
                        objParams[17] = new SqlParameter("@P_CONTMAILID", objTP.CONTMAILID);
                        objParams[18] = new SqlParameter("@P_OTHERINFO", objTP.OTHERINFO);
                        objParams[19] = new SqlParameter("@P_REMARK", objTP.REMARK);
                        objParams[20] = new SqlParameter("@P_COLLEGE_CODE", objTP.COLLEGE_CODE);
                        objParams[21] = new SqlParameter("@P_PWD", objTP.COMPPWD);
                        objParams[22] = new SqlParameter("@P_STATUS", objTP.COMPSTATUS);

                        objParams[23] = new SqlParameter("@P_INPLANT", objTP.INPLANT);
                        objParams[24] = new SqlParameter("@P_IPNAME", objTP.IPCONTPERSON);
                        objParams[25] = new SqlParameter("@P_IPDESIGNATION", objTP.IPCONTDESIGNATION);
                        objParams[26] = new SqlParameter("@P_IPADDRESS", objTP.IPCONTADDRESS);
                        objParams[27] = new SqlParameter("@P_IPPHONE", objTP.IPCONTPHONE);
                        objParams[28] = new SqlParameter("@P_IPMAILID", objTP.IPCONTMAILID);
                        objParams[29] = new SqlParameter("@P_PLACEMENTCONTNO", objTP.PLACEMENTCONTNO);
                        objParams[30] = new SqlParameter("@P_BRANCHNO", objTP.BRANCHNO);
                        objParams[31] = new SqlParameter("@P_BRANCHESNO", BrNo);
                        objParams[32] = new SqlParameter("@P_COMPID", SqlDbType.Int);
                        objParams[32].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_COMPANY_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        //     
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddComapny->" + ex.ToString());
                    }
                    return retStatus;
                }

                // To update Existing Company
                public int UpdateCompany(TrainingPlacement objTP, string BrNo)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[34];

                        objParams[0] = new SqlParameter("@P_COMPID", objTP.COMPID);
                        objParams[1] = new SqlParameter("@P_COMPCATNO", objTP.COMPCATNO);
                        objParams[2] = new SqlParameter("@P_COMPCODE", objTP.COMPCODE);
                        objParams[3] = new SqlParameter("@P_COMPNAME", objTP.COMPNAME);
                        objParams[4] = new SqlParameter("@P_COMPADD", objTP.COMPADD);
                        objParams[5] = new SqlParameter("@P_COMPDIRECTOR", objTP.COMPDIRECTOR);
                        objParams[6] = new SqlParameter("@P_CITYNO", objTP.CITYNO);
                        objParams[7] = new SqlParameter("@P_CITY", objTP.CITY);
                        objParams[8] = new SqlParameter("@P_PINCODE", objTP.PINCODE);
                        objParams[9] = new SqlParameter("@P_PHONENO", objTP.PHONENO);
                        objParams[10] = new SqlParameter("@P_FAXNO", objTP.FAXNO);
                        objParams[11] = new SqlParameter("@P_EMAILID", objTP.EMAILID);
                        objParams[12] = new SqlParameter("@P_WEBSITE", objTP.WEBSITE);
                        objParams[13] = new SqlParameter("@P_SALRANGE", objTP.SALRANGE);
                        objParams[14] = new SqlParameter("@P_CONTPERSON", objTP.CONTPERSON);
                        objParams[15] = new SqlParameter("@p_CONTDESIGNATION", objTP.CONTDESIGNATION);
                        objParams[16] = new SqlParameter("@P_CONTADDRESS", objTP.CONTADDRESS);
                        objParams[17] = new SqlParameter("@P_CONTPHONE", objTP.CONTPHONE);
                        objParams[18] = new SqlParameter("@P_CONTMAILID", objTP.CONTMAILID);
                        objParams[19] = new SqlParameter("@P_OTHERINFO", objTP.OTHERINFO);
                        objParams[20] = new SqlParameter("@P_REMARK", objTP.REMARK);
                        objParams[21] = new SqlParameter("@P_STATUS", objTP.COMPSTATUS);
                        objParams[22] = new SqlParameter("@P_PWD", objTP.COMPPWD);
                        objParams[23] = new SqlParameter("@P_COLLEGE_CODE", objTP.COLLEGE_CODE);

                        objParams[24] = new SqlParameter("@P_INPLANT", objTP.INPLANT);
                        objParams[25] = new SqlParameter("@P_IPNAME", objTP.IPCONTPERSON);
                        objParams[26] = new SqlParameter("@P_IPDESIGNATION", objTP.IPCONTDESIGNATION);
                        objParams[27] = new SqlParameter("@P_IPADDRESS", objTP.IPCONTADDRESS);
                        objParams[28] = new SqlParameter("@P_IPPHONE", objTP.IPCONTPHONE);
                        objParams[29] = new SqlParameter("@P_IPMAILID", objTP.IPCONTMAILID);
                        objParams[30] = new SqlParameter("@P_PLACEMENTCONTNO", objTP.PLACEMENTCONTNO);
                        objParams[31] = new SqlParameter("@P_BRANCHNO", objTP.BRANCHNO);
                        objParams[32] = new SqlParameter("@P_BRANCHESNO", BrNo);
                        objParams[33] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[33].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_COMPANY_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateCompany->" + ex.ToString());

                    }
                    return retStatus;
                }

                public DataSet GetBranchIP(int Compid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COMPID", Compid);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_IP_TEMP_COMP_BRANCH", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetBranchIP-> " + ex.ToString());
                    }
                    return ds;
                }
                public int DeleteCompany(int COMPID)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COMPID", COMPID);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_COMPANY_DELETE", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.DeleteCompany-> " + ex.ToString());
                    }
                    return retstatus;
                }
                public DataSet GetAllCompany(String Status)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_STATUS", Status);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_COMPANY_GET_BY_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetAllCompany-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public DataSet GetSelectionDtForInterview(int ScheduleNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter();
                        ds = objSQLHelper.ExecuteDataSetSP("", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetSelectionDtForInterview-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public DataSet GetCompanyById(int COMPID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COMPID", COMPID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_COMPANY_GET_BY_NO", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetCompanyById-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion

                #region Company Schedule

                public DataSet GetAllSelectionType()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_SELECTIONTYPE_GET_BY_ALL", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetAllSelectionType-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
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
                //public int GenerateId()
                //{
                //    int idno = 0;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null;
                //        objParams = new SqlParameter[0];
                //        object ret = objSQLHelper.ExecuteScalarSP("PKG_ACAD_TP_RETURN_ID_COMPSCHEDULE", objParams);
                //        if (ret != null)
                //            idno = Convert.ToInt32(ret);
                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GenerateId -> " + ex.ToString());
                //    }
                //    return idno;
                //}


                //UPDATED BY SUMIT --15092019 //

                public int AddComp_Schedule(TrainingPlacement objTP)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[33];
                        objParams[0] = new SqlParameter("@P_SCHEDULEDATE", objTP.SCHEDULEDATE);
                        objParams[1] = new SqlParameter("@P_DTFIXED", objTP.IsDtFixed);
                        objParams[2] = new SqlParameter("@P_INTERVIEWFROM", objTP.INTERVIEWFROM);
                        objParams[3] = new SqlParameter("@P_INTERVIEWTO", objTP.INTERVIEWTO);

                        //if ((objTP.SCHEDULEDATE.Equals(DateTime.MinValue)) || (objTP.SCHEDULEDATE == null))
                        //    objParams[0] = new SqlParameter("@P_SCHEDULEDATE", DBNull.Value);
                        //else
                        //    objParams[0] = new SqlParameter("@P_SCHEDULEDATE", objTP.SCHEDULEDATE);

                        //if ((objTP.SCHEDULE_DT_TEXT == string.Empty) || (objTP.SCHEDULE_DT_TEXT == null))
                        //    objParams[1] = new SqlParameter("@P_SCHEDULEDT_TEXT", DBNull.Value);
                        //else
                        //    objParams[1] = new SqlParameter("@P_SCHEDULEDT_TEXT", objTP.SCHEDULE_DT_TEXT);


                        //if ((objTP.INTERVIEWFROM.Equals(DateTime.MinValue)) || (objTP.INTERVIEWFROM == null))
                        //    objParams[2] = new SqlParameter("@P_INTERVIEWFROM", DBNull.Value);
                        //else
                        //    objParams[2] = new SqlParameter("@P_INTERVIEWFROM", objTP.INTERVIEWFROM);

                        //if ((objTP.INTERVIEWTO.Equals(DateTime.MinValue)) || (objTP.INTERVIEWTO == null))
                        //    objParams[3] = new SqlParameter("@P_INTERVIEWTO", DBNull.Value);
                        //else
                        //    objParams[3] = new SqlParameter("@P_INTERVIEWTO", objTP.INTERVIEWTO);

                        objParams[4] = new SqlParameter("@P_COMPID", objTP.COMPID);
                        objParams[5] = new SqlParameter("@P_REQUIREMENT", objTP.REQUIREMENT);
                        objParams[6] = new SqlParameter("@P_DEGREE", objTP.DEGREE);
                        objParams[7] = new SqlParameter("@P_UGPG", objTP.UGPG);
                        objParams[8] = new SqlParameter("@P_BRANCH", objTP.BRANCH);
                        objParams[9] = new SqlParameter("@P_CRITERIA", objTP.CRITERIA);
                        objParams[10] = new SqlParameter("@P_BOND", objTP.BOND);
                        objParams[11] = new SqlParameter("@P_BONDDETAILS", objTP.BONDDETAILS);
                        objParams[12] = new SqlParameter("@P_SELECTNO", objTP.SELECTNO);
                        objParams[13] = new SqlParameter("@P_UGSTIPEND", objTP.UGSTIPEND);
                        objParams[14] = new SqlParameter("@P_UGSALARY", objTP.UGSALARY);
                        objParams[15] = new SqlParameter("@P_PGSTIPEND", objTP.PGSTIPEND);
                        objParams[16] = new SqlParameter("@P_PGSALARY", objTP.PGSALARY);
                        objParams[17] = new SqlParameter("@P_SCHEDULESTATUS", objTP.SCHEDULESTATUS);
                        objParams[18] = new SqlParameter("@P_COLLEGE_CODE", objTP.COLLEGE_CODE);
                        objParams[19] = new SqlParameter("@P_JOBTYPE", objTP.JOBTYPE);
                        objParams[20] = new SqlParameter("@P_JOBANNOUNCE", objTP.JOBANNOUNCEMENT);
                        //objParams[17] = new SqlParameter("@P_DREAMJOB", objTP.DreamJob);

                        //if ((objTP.LASTDATE.Equals(DateTime.MinValue)) || (objTP.LASTDATE == null))
                        //    objParams[18] = new SqlParameter("@P_LASTDATE", DBNull.Value);
                        //else
                        //    objParams[18] = new SqlParameter("@P_LASTDATE", objTP.LASTDATE);

                        //if (objTP.SCHEDULETIME.Equals(DateTime.MinValue) || (objTP.SCHEDULETIME == null))
                        //    objParams[19] = new SqlParameter("@P_SCHEDULETIME", DBNull.Value);
                        //else
                        //    objParams[19] = new SqlParameter("@P_SCHEDULETIME", objTP.SCHEDULETIME);

                        if ((objTP.LASTDATE.Equals(string.Empty)) || (objTP.LASTDATE == null))
                            objParams[21] = new SqlParameter("@P_LASTDATE", DBNull.Value);
                        else
                            objParams[21] = new SqlParameter("@P_LASTDATE", objTP.LASTDATE);

                        if (objTP.SCHEDULETIME.Equals(string.Empty) || (objTP.SCHEDULETIME == null))
                            objParams[22] = new SqlParameter("@P_SCHEDULETIME", DBNull.Value);
                        else
                            objParams[22] = new SqlParameter("@P_SCHEDULETIME", objTP.SCHEDULETIME);

                        objParams[23] = new SqlParameter("@P_VENUE", objTP.Venue);
                        objParams[24] = new SqlParameter("@P_NOTE", objTP.NOTE);

                        if (string.IsNullOrEmpty(objTP.FileName))
                            objParams[25] = new SqlParameter("@P_FILENAME", DBNull.Value);
                        else
                            objParams[25] = new SqlParameter("@P_FILENAME", objTP.FileName);
                        objParams[26] = new SqlParameter("@P_SSCPER", objTP.SSCPER);
                        objParams[27] = new SqlParameter("@P_HSCPER", objTP.HSCPER);
                        objParams[28] = new SqlParameter("@P_DIPLOMAPER", objTP.DIPLOMAPER);
                        objParams[29] = new SqlParameter("@P_UGPER", objTP.UGPER);
                        objParams[30] = new SqlParameter("@P_PGPER", objTP.PGPER);
                        objParams[31] = new SqlParameter("@P_BACKLOG", objTP.Backlog);

                        objParams[32] = new SqlParameter("@P_SCHEDULENO", SqlDbType.Int);
                        objParams[32].Direction = ParameterDirection.Output;
                        //if(objSQLHelper.ExecuteNonQuerySPTrans("PKG_ACAD_TP_COMPSCHEDULE_INSERT",objParams,false,1)!=null)
                        //   retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        //object ret = objSQLHelper.ExecuteNonQuerySPTrans("PKG_ACAD_TP_COMPSCHEDULE_INSERT", objParams, true, 1);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_COMPSCHEDULE_INSERT", objParams, true);
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


                public int AddDocumentFile(TPDocHistory objfile)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_COMPID", objfile.COMPID);
                        objParams[1] = new SqlParameter("@P_COMPANYNAME", objfile.COMPNAME);
                        objParams[2] = new SqlParameter("@P_SCHEDULENO", objfile.SCHEDULE);
                        objParams[3] = new SqlParameter("@P_FILENAME", objfile.FILENAME);
                        objParams[4] = new SqlParameter("@P_FILEPATH", objfile.FILEPATH);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objfile.COLLEGECODE);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_INS_FILE_DETAILS", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(ret); //Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(ret);//Convert.ToInt32(CustomStatus.RecordUpdated);                        

                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddDocumentFile-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int UpdDocumentFile(TPDocHistory objfile)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SCHEDULENO", objfile.SCHEDULE);
                        objParams[1] = new SqlParameter("@P_FILENAME", objfile.FILENAME);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_UPD_FILE_DETAILS", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(ret); //Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(ret);//Convert.ToInt32(CustomStatus.RecordUpdated);                        

                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddDocumentFile-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// Updated by S.P - 09/10/2021
                /// </summary>
                /// <param name="objTP"></param>
                /// <param name="JobProfile"></param>
                /// <returns></returns>
                public int UpdateComp_Schedule(TrainingPlacement objTP, int JobProfile)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[37];
                        objParams[0] = new SqlParameter("@P_SCHEDULENO", objTP.SCHEDULENO);
                        objParams[1] = new SqlParameter("@P_SCHEDULEDATE", objTP.SCHEDULEDATE);
                        objParams[2] = new SqlParameter("@P_DTFIXED", objTP.IsDtFixed);
                        objParams[3] = new SqlParameter("@P_INTERVIEWFROM", objTP.INTERVIEWFROM);
                        objParams[4] = new SqlParameter("@P_INTERVIEWTO", objTP.INTERVIEWTO);

                        //if ((objTP.SCHEDULEDATE.Equals(DateTime.MinValue)) || (objTP.SCHEDULEDATE == null))
                        //    objParams[1] = new SqlParameter("@P_SCHEDULEDATE", DBNull.Value);
                        //else
                        //    objParams[1] = new SqlParameter("@P_SCHEDULEDATE", objTP.SCHEDULEDATE);

                        //if ((objTP.SCHEDULE_DT_TEXT == string.Empty) || (objTP.SCHEDULE_DT_TEXT==null))
                        //    objParams[2] = new SqlParameter("@P_SCHEDULEDT_TEXT", DBNull.Value);
                        //else
                        //    objParams[2] = new SqlParameter("@P_SCHEDULEDT_TEXT", objTP.SCHEDULE_DT_TEXT);

                        //if ((objTP.INTERVIEWFROM.Equals(DateTime.MinValue)) || (objTP.INTERVIEWFROM == null))
                        //    objParams[3] = new SqlParameter("@P_INTERVIEWFROM", DBNull.Value);
                        //else
                        //    objParams[3] = new SqlParameter("@P_INTERVIEWFROM", objTP.INTERVIEWFROM);

                        //if ((objTP.INTERVIEWTO.Equals(DateTime.MinValue)) || (objTP.INTERVIEWTO == null))
                        //    objParams[4] = new SqlParameter("@P_INTERVIEWTO", DBNull.Value);
                        //else
                        //    objParams[4] = new SqlParameter("@P_INTERVIEWTO", objTP.INTERVIEWTO);


                        objParams[5] = new SqlParameter("@P_COMPID", objTP.COMPID);
                        objParams[6] = new SqlParameter("@P_REQUIREMENT", objTP.REQUIREMENT);
                        objParams[7] = new SqlParameter("@P_DEGREE", objTP.DEGREE);
                        objParams[8] = new SqlParameter("@P_UGPG", objTP.UGPG);
                        objParams[9] = new SqlParameter("@P_BRANCH", objTP.BRANCH);
                        objParams[10] = new SqlParameter("@P_CRITERIA", objTP.CRITERIA);
                        objParams[11] = new SqlParameter("@P_BOND", objTP.BOND);
                        objParams[12] = new SqlParameter("@P_BONDDETAILS", objTP.BONDDETAILS);
                        objParams[13] = new SqlParameter("@P_SELECTNO", objTP.SELECTNO);
                        objParams[14] = new SqlParameter("@P_UGSTIPEND", objTP.UGSTIPEND);
                        objParams[15] = new SqlParameter("@P_UGSALARY", objTP.UGSALARY);
                        objParams[16] = new SqlParameter("@P_PGSTIPEND", objTP.PGSTIPEND);
                        objParams[17] = new SqlParameter("@P_PGSALARY", objTP.PGSALARY);
                        objParams[18] = new SqlParameter("@P_SCHEDULESTATUS", objTP.SCHEDULESTATUS);
                        objParams[19] = new SqlParameter("@P_JOBTYPE", objTP.JOBTYPE);
                        objParams[20] = new SqlParameter("@P_COLLEGE_CODE", objTP.COLLEGE_CODE);
                        objParams[21] = new SqlParameter("@P_JOBANNOUNCE", objTP.JOBANNOUNCEMENT);
                        //objParams[18] = new SqlParameter("@P_DREAMJOB", objTP.DreamJob);

                        if ((objTP.LASTDATE.Equals(string.Empty)) || (objTP.LASTDATE == null))
                            objParams[22] = new SqlParameter("@P_LASTDATE", DBNull.Value);
                        else
                            objParams[22] = new SqlParameter("@P_LASTDATE", objTP.LASTDATE);

                        if (objTP.SCHEDULETIME.Equals(string.Empty) || (objTP.SCHEDULETIME == null))
                            objParams[23] = new SqlParameter("@P_SCHEDULETIME", DBNull.Value);
                        else
                            objParams[23] = new SqlParameter("@P_SCHEDULETIME", objTP.SCHEDULETIME);

                        objParams[24] = new SqlParameter("@P_VENUE", objTP.Venue);
                        objParams[25] = new SqlParameter("@P_NOTE", objTP.NOTE);

                        if (string.IsNullOrEmpty(objTP.FileName))
                            objParams[26] = new SqlParameter("@P_FILENAME", DBNull.Value);
                        else
                            objParams[26] = new SqlParameter("@P_FILENAME", objTP.FileName);

                        objParams[27] = new SqlParameter("@P_SSCPER", objTP.SSCPER);
                        objParams[28] = new SqlParameter("@P_HSCPER", objTP.HSCPER);
                        objParams[29] = new SqlParameter("@P_DIPLOMAPER", objTP.DIPLOMAPER);
                        objParams[30] = new SqlParameter("@P_UGPER", objTP.UGPER);
                        objParams[31] = new SqlParameter("@P_PGPER", objTP.PGPER);
                        objParams[32] = new SqlParameter("@P_BACKLOG", objTP.Backlog);
                        objParams[33] = new SqlParameter("@P_JOBPROFILE", JobProfile);
                        objParams[34] = new SqlParameter("@P_SALARY_CURRENCY", objTP.CurrencySalary);
                        objParams[35] = new SqlParameter("@P_STIPEND_CURRENCY", objTP.CurrencyStipend);
                        objParams[36] = new SqlParameter("@P_OUTTYPE", SqlDbType.Int);
                        objParams[36].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_COMPSCHEDULE_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(ret);
                        else
                            retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.DELETE_COMPSCHEDULEDETAILS-> " + ex.ToString());
                    }
                    return retStatus;
                }


                /// <summary>
                /// Updated by S.P - 09/10/2021
                /// </summary>
                /// <param name="objTP"></param>
                /// <param name="JobProfile"></param>
                /// <returns></returns>
                public int AddComp_Schedule(TrainingPlacement objTP, int JobProfile)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[36];
                        objParams[0] = new SqlParameter("@P_SCHEDULEDATE", objTP.SCHEDULEDATE);
                        objParams[1] = new SqlParameter("@P_DTFIXED", objTP.IsDtFixed);
                        objParams[2] = new SqlParameter("@P_INTERVIEWFROM", objTP.INTERVIEWFROM);
                        objParams[3] = new SqlParameter("@P_INTERVIEWTO", objTP.INTERVIEWTO);

                        //if ((objTP.SCHEDULEDATE.Equals(DateTime.MinValue)) || (objTP.SCHEDULEDATE == null))
                        //    objParams[0] = new SqlParameter("@P_SCHEDULEDATE", DBNull.Value);
                        //else
                        //    objParams[0] = new SqlParameter("@P_SCHEDULEDATE", objTP.SCHEDULEDATE);

                        //if ((objTP.SCHEDULE_DT_TEXT == string.Empty) || (objTP.SCHEDULE_DT_TEXT == null))
                        //    objParams[1] = new SqlParameter("@P_SCHEDULEDT_TEXT", DBNull.Value);
                        //else
                        //    objParams[1] = new SqlParameter("@P_SCHEDULEDT_TEXT", objTP.SCHEDULE_DT_TEXT);


                        //if ((objTP.INTERVIEWFROM.Equals(DateTime.MinValue)) || (objTP.INTERVIEWFROM == null))
                        //    objParams[2] = new SqlParameter("@P_INTERVIEWFROM", DBNull.Value);
                        //else
                        //    objParams[2] = new SqlParameter("@P_INTERVIEWFROM", objTP.INTERVIEWFROM);

                        //if ((objTP.INTERVIEWTO.Equals(DateTime.MinValue)) || (objTP.INTERVIEWTO == null))
                        //    objParams[3] = new SqlParameter("@P_INTERVIEWTO", DBNull.Value);
                        //else
                        //    objParams[3] = new SqlParameter("@P_INTERVIEWTO", objTP.INTERVIEWTO);

                        objParams[4] = new SqlParameter("@P_COMPID", objTP.COMPID);
                        objParams[5] = new SqlParameter("@P_REQUIREMENT", objTP.REQUIREMENT);
                        objParams[6] = new SqlParameter("@P_DEGREE", objTP.DEGREE);
                        objParams[7] = new SqlParameter("@P_UGPG", objTP.UGPG);
                        objParams[8] = new SqlParameter("@P_BRANCH", objTP.BRANCH);
                        objParams[9] = new SqlParameter("@P_CRITERIA", objTP.CRITERIA);
                        objParams[10] = new SqlParameter("@P_BOND", objTP.BOND);
                        objParams[11] = new SqlParameter("@P_BONDDETAILS", objTP.BONDDETAILS);
                        objParams[12] = new SqlParameter("@P_SELECTNO", objTP.SELECTNO);
                        objParams[13] = new SqlParameter("@P_UGSTIPEND", objTP.UGSTIPEND);
                        objParams[14] = new SqlParameter("@P_UGSALARY", objTP.UGSALARY);
                        objParams[15] = new SqlParameter("@P_PGSTIPEND", objTP.PGSTIPEND);
                        objParams[16] = new SqlParameter("@P_PGSALARY", objTP.PGSALARY);
                        objParams[17] = new SqlParameter("@P_SCHEDULESTATUS", objTP.SCHEDULESTATUS);
                        objParams[18] = new SqlParameter("@P_COLLEGE_CODE", objTP.COLLEGE_CODE);
                        objParams[19] = new SqlParameter("@P_JOBTYPE", objTP.JOBTYPE);
                        objParams[20] = new SqlParameter("@P_JOBANNOUNCE", objTP.JOBANNOUNCEMENT);
                        //objParams[17] = new SqlParameter("@P_DREAMJOB", objTP.DreamJob);

                        //if ((objTP.LASTDATE.Equals(DateTime.MinValue)) || (objTP.LASTDATE == null))
                        //    objParams[18] = new SqlParameter("@P_LASTDATE", DBNull.Value);
                        //else
                        //    objParams[18] = new SqlParameter("@P_LASTDATE", objTP.LASTDATE);

                        //if (objTP.SCHEDULETIME.Equals(DateTime.MinValue) || (objTP.SCHEDULETIME == null))
                        //    objParams[19] = new SqlParameter("@P_SCHEDULETIME", DBNull.Value);
                        //else
                        //    objParams[19] = new SqlParameter("@P_SCHEDULETIME", objTP.SCHEDULETIME);

                        if ((objTP.LASTDATE.Equals(string.Empty)) || (objTP.LASTDATE == null))
                            objParams[21] = new SqlParameter("@P_LASTDATE", DBNull.Value);
                        else
                            objParams[21] = new SqlParameter("@P_LASTDATE", objTP.LASTDATE);

                        if (objTP.SCHEDULETIME.Equals(string.Empty) || (objTP.SCHEDULETIME == null))
                            objParams[22] = new SqlParameter("@P_SCHEDULETIME", DBNull.Value);
                        else
                            objParams[22] = new SqlParameter("@P_SCHEDULETIME", objTP.SCHEDULETIME);

                        objParams[23] = new SqlParameter("@P_VENUE", objTP.Venue);
                        objParams[24] = new SqlParameter("@P_NOTE", objTP.NOTE);

                        if (string.IsNullOrEmpty(objTP.FileName))
                            objParams[25] = new SqlParameter("@P_FILENAME", DBNull.Value);
                        else
                            objParams[25] = new SqlParameter("@P_FILENAME", objTP.FileName);
                        objParams[26] = new SqlParameter("@P_SSCPER", objTP.SSCPER);
                        objParams[27] = new SqlParameter("@P_HSCPER", objTP.HSCPER);
                        objParams[28] = new SqlParameter("@P_DIPLOMAPER", objTP.DIPLOMAPER);
                        objParams[29] = new SqlParameter("@P_UGPER", objTP.UGPER);
                        objParams[30] = new SqlParameter("@P_PGPER", objTP.PGPER);
                        objParams[31] = new SqlParameter("@P_BACKLOG", objTP.Backlog);
                        objParams[32] = new SqlParameter("@P_JOBPROFILE", JobProfile);
                        objParams[33] = new SqlParameter("@P_SALARY_CURRENCY", objTP.CurrencySalary);
                        objParams[34] = new SqlParameter("@P_STIPEND_CURRENCY", objTP.CurrencyStipend);
                        objParams[35] = new SqlParameter("@P_SCHEDULENO", SqlDbType.Int);
                        objParams[35].Direction = ParameterDirection.Output;
                        //if(objSQLHelper.ExecuteNonQuerySPTrans("PKG_ACAD_TP_COMPSCHEDULE_INSERT",objParams,false,1)!=null)
                        //   retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        //object ret = objSQLHelper.ExecuteNonQuerySPTrans("PKG_ACAD_TP_COMPSCHEDULE_INSERT", objParams, true, 1);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_COMPSCHEDULE_INSERT", objParams, true);
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
                // UPDATED BY SUMIT -- 15092019 //
                public int UpdateComp_Schedule(TrainingPlacement objTP)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[34];
                        objParams[0] = new SqlParameter("@P_SCHEDULENO", objTP.SCHEDULENO);
                        objParams[1] = new SqlParameter("@P_SCHEDULEDATE", objTP.SCHEDULEDATE);
                        objParams[2] = new SqlParameter("@P_DTFIXED", objTP.IsDtFixed);
                        objParams[3] = new SqlParameter("@P_INTERVIEWFROM", objTP.INTERVIEWFROM);
                        objParams[4] = new SqlParameter("@P_INTERVIEWTO", objTP.INTERVIEWTO);

                        //if ((objTP.SCHEDULEDATE.Equals(DateTime.MinValue)) || (objTP.SCHEDULEDATE == null))
                        //    objParams[1] = new SqlParameter("@P_SCHEDULEDATE", DBNull.Value);
                        //else
                        //    objParams[1] = new SqlParameter("@P_SCHEDULEDATE", objTP.SCHEDULEDATE);

                        //if ((objTP.SCHEDULE_DT_TEXT == string.Empty) || (objTP.SCHEDULE_DT_TEXT==null))
                        //    objParams[2] = new SqlParameter("@P_SCHEDULEDT_TEXT", DBNull.Value);
                        //else
                        //    objParams[2] = new SqlParameter("@P_SCHEDULEDT_TEXT", objTP.SCHEDULE_DT_TEXT);

                        //if ((objTP.INTERVIEWFROM.Equals(DateTime.MinValue)) || (objTP.INTERVIEWFROM == null))
                        //    objParams[3] = new SqlParameter("@P_INTERVIEWFROM", DBNull.Value);
                        //else
                        //    objParams[3] = new SqlParameter("@P_INTERVIEWFROM", objTP.INTERVIEWFROM);

                        //if ((objTP.INTERVIEWTO.Equals(DateTime.MinValue)) || (objTP.INTERVIEWTO == null))
                        //    objParams[4] = new SqlParameter("@P_INTERVIEWTO", DBNull.Value);
                        //else
                        //    objParams[4] = new SqlParameter("@P_INTERVIEWTO", objTP.INTERVIEWTO);


                        objParams[5] = new SqlParameter("@P_COMPID", objTP.COMPID);
                        objParams[6] = new SqlParameter("@P_REQUIREMENT", objTP.REQUIREMENT);
                        objParams[7] = new SqlParameter("@P_DEGREE", objTP.DEGREE);
                        objParams[8] = new SqlParameter("@P_UGPG", objTP.UGPG);
                        objParams[9] = new SqlParameter("@P_BRANCH", objTP.BRANCH);
                        objParams[10] = new SqlParameter("@P_CRITERIA", objTP.CRITERIA);
                        objParams[11] = new SqlParameter("@P_BOND", objTP.BOND);
                        objParams[12] = new SqlParameter("@P_BONDDETAILS", objTP.BONDDETAILS);
                        objParams[13] = new SqlParameter("@P_SELECTNO", objTP.SELECTNO);
                        objParams[14] = new SqlParameter("@P_UGSTIPEND", objTP.UGSTIPEND);
                        objParams[15] = new SqlParameter("@P_UGSALARY", objTP.UGSALARY);
                        objParams[16] = new SqlParameter("@P_PGSTIPEND", objTP.PGSTIPEND);
                        objParams[17] = new SqlParameter("@P_PGSALARY", objTP.PGSALARY);
                        objParams[18] = new SqlParameter("@P_SCHEDULESTATUS", objTP.SCHEDULESTATUS);
                        objParams[19] = new SqlParameter("@P_JOBTYPE", objTP.JOBTYPE);
                        objParams[20] = new SqlParameter("@P_COLLEGE_CODE", objTP.COLLEGE_CODE);
                        objParams[21] = new SqlParameter("@P_JOBANNOUNCE", objTP.JOBANNOUNCEMENT);
                        //objParams[18] = new SqlParameter("@P_DREAMJOB", objTP.DreamJob);

                        if ((objTP.LASTDATE.Equals(string.Empty)) || (objTP.LASTDATE == null))
                            objParams[22] = new SqlParameter("@P_LASTDATE", DBNull.Value);
                        else
                            objParams[22] = new SqlParameter("@P_LASTDATE", objTP.LASTDATE);

                        if (objTP.SCHEDULETIME.Equals(string.Empty) || (objTP.SCHEDULETIME == null))
                            objParams[23] = new SqlParameter("@P_SCHEDULETIME", DBNull.Value);
                        else
                            objParams[23] = new SqlParameter("@P_SCHEDULETIME", objTP.SCHEDULETIME);

                        objParams[24] = new SqlParameter("@P_VENUE", objTP.Venue);
                        objParams[25] = new SqlParameter("@P_NOTE", objTP.NOTE);

                        if (string.IsNullOrEmpty(objTP.FileName))
                            objParams[26] = new SqlParameter("@P_FILENAME", DBNull.Value);
                        else
                            objParams[26] = new SqlParameter("@P_FILENAME", objTP.FileName);

                        objParams[27] = new SqlParameter("@P_SSCPER", objTP.SSCPER);
                        objParams[28] = new SqlParameter("@P_HSCPER", objTP.HSCPER);
                        objParams[29] = new SqlParameter("@P_DIPLOMAPER", objTP.DIPLOMAPER);
                        objParams[30] = new SqlParameter("@P_UGPER", objTP.UGPER);
                        objParams[31] = new SqlParameter("@P_PGPER", objTP.PGPER);
                        objParams[32] = new SqlParameter("@P_BACKLOG", objTP.Backlog);

                        objParams[33] = new SqlParameter("@P_OUTTYPE", SqlDbType.Int);
                        objParams[33].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_COMPSCHEDULE_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(ret);
                        else
                            retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.DELETE_COMPSCHEDULEDETAILS-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public int AddComp_ScheduleDetail(Int32 ScheduleNo, String Name, string Desig, string Dept, string Email, string ContNo, string Collcd)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_VISITORNAME", Name);
                        objParams[1] = new SqlParameter("@P_VISITORDESIGNATION", Desig);
                        objParams[2] = new SqlParameter("@P_VISITORDEPARTMENT", Dept);

                        objParams[3] = new SqlParameter("@P_VISITOREMAIL", Email);
                        objParams[4] = new SqlParameter("@P_VISITORCONTACTNO", ContNo);

                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", Collcd);
                        objParams[6] = new SqlParameter("@P_SCHEDULENO", ScheduleNo);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_TP_COMPSCHEDULEDETAILS_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddComp_ScheduleDeatil-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetCompSchedule()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_COMPSCHEDULE_GET_BY_ALL", objParams);
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
                public DataSet GetDFileList(int Scheduleno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCHEDULENO", Scheduleno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_GET_ALL_FILE_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetDFileList-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public DataSet GetDoclist(int scheduleNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCHEDULENO", scheduleNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_GET_FILE_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetDoclistForDelete-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                
                public DataSet GetDoclistForDelete(int scheduleno, int fileno, string filename, int companyid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SCHEDULENO", scheduleno);
                        objParams[1] = new SqlParameter("@P_FILENO", fileno);
                        objParams[2] = new SqlParameter("@P_FILENAME", filename);
                        objParams[3] = new SqlParameter("@P_COMPANYID", companyid);

                        //  objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        //  objParams[4].Direction = ParameterDirection.Output;

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_GET_FILE_DETAILS_DELETE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetDoclistForDelete-> " + ex.ToString());
                    }

                    return ds;
                }
                public int DeletFilelist(int scheduleno, int fileno, string filename, int companyid)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SCHEDULENO", scheduleno);
                        objParams[1] = new SqlParameter("@P_FILENO", fileno);
                        objParams[2] = new SqlParameter("@P_FILENAME", filename);
                        objParams[3] = new SqlParameter("@P_COMPANY_ID", companyid);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_DELETE_FILE_DETAILS", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(ret); //Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(ret);//Convert.ToInt32(CustomStatus.RecordUpdated);                        

                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.DeletFilelist-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetStudComp(int idNo, int compId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        objParams[1] = new SqlParameter("@P_COMPID", compId);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_GET_STUD_COMP", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetCompSceduleDetail-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public DataSet GetCompSceduleDetail(int ScheduleNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCHEDULENO", ScheduleNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_COMPSCHEDULE_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetCompSceduleDetail-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public int DELETE_COMPSCHEDULEDETAILS(int ScheduleNO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCHEDULENO", ScheduleNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_TP_COMPSCHEDULEDETAILS_DELETE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.DELETE_COMPSCHEDULEDETAILS-> " + ex.ToString());
                    }
                    return retStatus;

                }


                #endregion
                #region workshop
                public int AddWorkshop(TrainingPlacement objTP)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_NAME", objTP.Name);
                        objParams[1] = new SqlParameter("@P_ORGBY", objTP.ORGANISEDBY);
                        objParams[2] = new SqlParameter("@P_DATE", objTP.SCHEDULEDATE);

                        if (objTP.SCHEDULETIME.Equals(string.Empty) || (objTP.SCHEDULETIME == null))
                            objParams[3] = new SqlParameter("@P_TIME", DBNull.Value);
                        else
                            objParams[3] = new SqlParameter("@P_TIME", objTP.SCHEDULETIME);

                        objParams[4] = new SqlParameter("@P_VENUE", objTP.Venue);
                        objParams[5] = new SqlParameter("@P_UGPG", objTP.UGPG);
                        objParams[6] = new SqlParameter("@P_BRANCH", objTP.BRANCH);
                        objParams[7] = new SqlParameter("@P_ANNOUNCE", objTP.JOBANNOUNCEMENT);

                        if ((objTP.LASTDATE.Equals(string.Empty)) || (objTP.LASTDATE == null))
                            objParams[8] = new SqlParameter("@P_LASTDATE", DBNull.Value);
                        else
                            objParams[8] = new SqlParameter("@P_LASTDATE", objTP.LASTDATE);

                        objParams[9] = new SqlParameter("@P_NOTE", objTP.NOTE);
                        objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objTP.COLLEGE_CODE);

                        objParams[11] = new SqlParameter("@P_WORKNO", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_WORKSHOP_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(ret); //Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(ret);//Convert.ToInt32(CustomStatus.RecordUpdated);                        

                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddWorkshop-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int UpdateWorkshop(TrainingPlacement objTP)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_WORKNO", objTP.WORKNO);
                        objParams[1] = new SqlParameter("@P_NAME", objTP.Name);
                        objParams[2] = new SqlParameter("@P_ORGANISEDBY", objTP.ORGANISEDBY);
                        objParams[3] = new SqlParameter("@P_DATE", objTP.SCHEDULEDATE);

                        if (objTP.SCHEDULETIME.Equals(string.Empty) || (objTP.SCHEDULETIME == null))
                            objParams[4] = new SqlParameter("@P_TIME", DBNull.Value);
                        else
                            objParams[4] = new SqlParameter("@P_TIME", objTP.SCHEDULETIME);

                        objParams[5] = new SqlParameter("@P_VENUE", objTP.Venue);
                        objParams[6] = new SqlParameter("@P_UGPG", objTP.UGPG);
                        objParams[7] = new SqlParameter("@P_BRANCH", objTP.BRANCH);
                        objParams[8] = new SqlParameter("@P_ANNOUNCE", objTP.JOBANNOUNCEMENT);

                        if ((objTP.LASTDATE.Equals(string.Empty)) || (objTP.LASTDATE == null))
                            objParams[9] = new SqlParameter("@P_LASTDATE", DBNull.Value);
                        else
                            objParams[9] = new SqlParameter("@P_LASTDATE", objTP.LASTDATE);

                        objParams[10] = new SqlParameter("@P_NOTE", objTP.NOTE);

                        objParams[11] = new SqlParameter("@P_COLLEGE_CODE", objTP.COLLEGE_CODE);

                        objParams[12] = new SqlParameter("@P_OUTTYPE", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_WORKSHOP_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(ret);
                        else
                            retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateWorkshop-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetWorkshop(int type)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TYPE", type);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_WORKSHOP", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.Get_Workshop-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                #endregion
                #region Student
                public DataSet GetstudentAll(string StudID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", StudID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_STUDENT_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetstudentAll-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetstudentByIDNO(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_STUDENT_DETAILS_BY_IDNO", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetstudentByIDNO-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetstudentByIDNOIP(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TPIP_STUDENT_DETAILS_BY_IDNO", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetstudentByIDNO-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddStudTraining(int Idno, int Seqno, string org, string period, string subject, string Colcode)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_IDNO", Idno);
                        objParams[1] = new SqlParameter("@P_SEQNO", Seqno);
                        objParams[2] = new SqlParameter("@P_ORG", org);
                        objParams[3] = new SqlParameter("@P_PERIOD", period);
                        objParams[4] = new SqlParameter("@P_SUBJECT", subject);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", Colcode);
                        objParams[6] = new SqlParameter("@P_TTNO", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_TRAIN_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                        //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_TRAIN_INSERT", objParams, true);
                        //if (Convert.ToInt32(ret) == -99)
                        //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        //else
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddStudTraining-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DelStudTraining(int TTNO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TTNO", TTNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_TRAIN_DELETE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                        //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_TRAIN_DELETE", objParams, true);
                        //if (Convert.ToInt32(ret) == -99)
                        //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        //else
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.DelStudTraining-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetAllTrainByID(string StudID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", StudID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_TRAIN_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetAllTrainByID-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddStudSkill(int IdNo, int Seqno, String Heading, string Descr, string Colcode)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_IDNO", IdNo);
                        objParams[1] = new SqlParameter("@P_SEQNO", Seqno);
                        objParams[2] = new SqlParameter("@P_HEADING", Heading);
                        objParams[3] = new SqlParameter("@P_DESCR", Descr);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", Colcode);
                        objParams[5] = new SqlParameter("@P_TSNO", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_SOFT_INSERT", objParams, false) != null)
                            //if (Convert.ToInt32(ret) == -99)
                            //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            //else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddStudSkill-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DelStudSkill(int TSNO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TSNO", TSNO);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_SOFT_DELETE", objParams, false) != null)
                            //if (Convert.ToInt32(ret) == -99)
                            //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            //else
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.DelStudSkill-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllSkillByID(string StudID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", StudID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_SOFT_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetAllSkillByID-> " + ex.ToString());
                    }
                    return ds;
                }


                public int AddStudEAct(int IdNo, int Seqno, String ACTIVITY, string Colcode)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_IDNO", IdNo);
                        objParams[1] = new SqlParameter("@P_SEQNO", Seqno);
                        objParams[2] = new SqlParameter("@P_ACTIVITY", ACTIVITY);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", Colcode);
                        objParams[4] = new SqlParameter("@P_TXNO", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_XTRACT_INSERT", objParams, false) != null)
                            //if (Convert.ToInt32(ret) == -99)
                            //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            //else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddStudEAct-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DelStudEAct(int TXNO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TXNO", TXNO);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_XTRACT_DELETE", objParams, false) != null)
                            //if (Convert.ToInt32(ret) == -99)
                            //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            //else
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.DelStudSkill-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllEActByID(string StudID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", StudID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_TP_XTRACT_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetAllEActByID-> " + ex.ToString());
                    }
                    return ds;
                }
                //-----------
                public int AddStudAchieve(int IdNo, int Seqno, String Achieve, string Colcode)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_IDNO", IdNo);
                        objParams[1] = new SqlParameter("@P_SEQNO", Seqno);
                        objParams[2] = new SqlParameter("@P_ACHIEVE", Achieve);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", Colcode);
                        objParams[4] = new SqlParameter("@P_TANO", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_ACHIEVE_INSERT", objParams, false) != null)
                            //if (Convert.ToInt32(ret) == -99)
                            //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            //else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddStudAchieve-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DelStudAchieve(int TANO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TANO", TANO);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_ACHIEVE_DELETE", objParams, false) != null)
                            //if (Convert.ToInt32(ret) == -99)
                            //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            //else
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.DelStudAchieve-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllAchieveByID(string StudID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", StudID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_ACHIEVE_GET_BY_NO", objParams);


                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetAllAchieveByID-> " + ex.ToString());
                    }
                    return ds;
                }


                //////////
                public int AddStudOther(int IdNo, int Seqno, String Other, string Colcode)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_IDNO", IdNo);
                        objParams[1] = new SqlParameter("@P_SEQNO", Seqno);
                        objParams[2] = new SqlParameter("@P_INFO", Other);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", Colcode);
                        objParams[4] = new SqlParameter("@P_TONO", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_TP_OTHINFO_INSERT", objParams, false) != null)
                            //if (Convert.ToInt32(ret) == -99)
                            //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            //else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddStudOther-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int DelStudOther(int TONO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TONO", TONO);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_TP_OTHINFO_DELETE", objParams, false) != null)
                            //if (Convert.ToInt32(ret) == -99)
                            //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            //else
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.DelStudOther-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllOtherByID(string StudID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", StudID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_OTHINFO_GET_BY_NO", objParams);


                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetAllOtherByID-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddStudQual(int IdNo, int SemNo, string Yrpass, Single Mobt, Single Omarks, Single percentage, string Colcode, Int32 Atmpt)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[16];
                        objParams[0] = new SqlParameter("@P_IDNO", IdNo);
                        objParams[1] = new SqlParameter("@P_QUALIFYNO", SemNo);
                        objParams[2] = new SqlParameter("@P_YEAR_OF_EXAMHSSC", Yrpass);
                        objParams[3] = new SqlParameter("@P_SCHOOL_COLLEGE_NAME", DBNull.Value);
                        objParams[4] = new SqlParameter("@P_BOARD", DBNull.Value);
                        objParams[5] = new SqlParameter("@P_GRADE", DBNull.Value);
                        objParams[6] = new SqlParameter("@P_ATTEMPT", Atmpt);
                        objParams[7] = new SqlParameter("@P_MARKS_OBTAINED", Mobt);
                        objParams[8] = new SqlParameter("@P_OUT_OF_MRKS", Omarks);
                        objParams[9] = new SqlParameter("@P_PER", percentage);
                        objParams[10] = new SqlParameter("@P_PERCENTILE", DBNull.Value);
                        objParams[11] = new SqlParameter("@P_RES_TOPIC", DBNull.Value);
                        objParams[12] = new SqlParameter("@P_SUPERVISOR_NAME1", DBNull.Value);
                        objParams[13] = new SqlParameter("@P_SUPERVISOR_NAME2", DBNull.Value);

                        objParams[14] = new SqlParameter("@P_COLLEGECODE", Colcode);

                        objParams[15] = new SqlParameter("@P_STQEXNO", SqlDbType.Int);
                        objParams[15].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_STUDENT_UPD_LAST_QUALEXM", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddStudQual-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int DelStudQual(int AcadNo)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ACADNO", AcadNo);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_TP_LAST_QUALEXM_DELETE", objParams, false) != null)
                            //if (Convert.ToInt32(ret) == -99)
                            //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            //else
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.DelStudOther-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllQualByID(string StudID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", StudID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_STUDENT_LAST_QUALIFICATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetAllQualByID-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddStudWorkshopReg(int IDNO, int WORKNO, String COLLEGE_CODE)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_WORKNO", WORKNO);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", COLLEGE_CODE);
                        objParams[3] = new SqlParameter("@P_WORKREGNO", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_TP_WORKSHOP_REGI_INSERT", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddStudWorkshopReg-> " + ex.ToString());
                    }
                    return retStatus;

                }

                #endregion
                #region Job Application
                public DataSet GetSchedule()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_COMPSCHEDULE_GET_FOR_STUDENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetScedule-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int AddStudReg(int IDNO, int SCHEDULENO, Boolean INTVSELECT, Boolean COMPSELECT, Boolean STUDCONFIRM, String COLLEGE_CODE, int Job, Boolean appbytpo)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_SCHEDULENO", SCHEDULENO);
                        objParams[2] = new SqlParameter("@P_INTVSELECT", INTVSELECT);
                        objParams[3] = new SqlParameter("@P_COMPSELECT", COMPSELECT);
                        objParams[4] = new SqlParameter("@P_STUDCONFIRM", STUDCONFIRM);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", COLLEGE_CODE);
                        objParams[6] = new SqlParameter("@P_JOBNO", Job);
                        objParams[7] = new SqlParameter("@P_APPLIEDBYTPO", appbytpo);
                        objParams[8] = new SqlParameter("@P_TPREGNO", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_TP_REGISTER_INSERT", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddStudReg-> " + ex.ToString());
                    }
                    return retStatus;

                }
                #endregion
                //public DataSet FillSchedule(string Status, int COMPID)
                public DataSet FillSchedule(int COMPID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        // objParams[0] = new SqlParameter("@P_STATUS", Status);
                        objParams[0] = new SqlParameter("@P_COMPID", COMPID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_GET_SCHEDULENOS", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.FillSchedule-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public DataSet FillSchedule_for_Rpt(string Status, int CompId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_STATUS", Status);
                        objParams[1] = new SqlParameter("@P_COMPID", CompId);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_GET_SCHEDULENOS_FOR_REPORT", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.FillSchedule_for_Rpt-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                //Added Parameter jobtype on date 22/03/2016
                public DataSet FillSchedule_for_FinalSelectedStud(string Status, int CompId, int jobtype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_STATUS", Status);
                        objParams[1] = new SqlParameter("@P_COMPID", CompId);
                        objParams[2] = new SqlParameter("@P_JOBTYPE", jobtype);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_GET_SCHEDULENOS_FOR_FINAL_SEL_STUD", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.FillSchedule_for_Rpt-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int SetCurSession(int SessionNo, string TPOfficer, string TPOffCntno, string TPOPlacement, string TPOCertificate)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[1] = new SqlParameter("@P_TPOFFICER", TPOfficer);
                        objParams[2] = new SqlParameter("@P_TPOCNTNO", TPOffCntno);
                        objParams[3] = new SqlParameter("@P_TPOPLACE", TPOPlacement);
                        objParams[4] = new SqlParameter("@P_TPOCERTIFICATE", TPOCertificate);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_SET_CURRENT_SESSION", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateIPTraining-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetParameter()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_IP_GET_PARAMETER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetParameter-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public DataSet FillBranch(int Scheduleno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCHEDULENO", Scheduleno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_BRANCHES", objParams);


                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.FillBranch-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public DataSet GetStudentsForInterview(int Scheduleno, int INTVSELECT)
                {
                    DataSet ds = null;
                    //try
                    //{
                    SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                    SqlParameter[] objParams = null;
                    objParams = new SqlParameter[2];
                    objParams[0] = new SqlParameter("@P_SCHEDULENO", Scheduleno);
                    objParams[1] = new SqlParameter("@P_INTVSELECT", INTVSELECT);
                    //ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_STUDENT_REGISTERED_LIST", objParams);

                    SqlConnection conn = new SqlConnection(_nitprm_constr);
                    //conn.ConnectionTimeout = 1000;
                    SqlCommand cmd = new SqlCommand("PKG_ACAD_TP_STUDENT_REGISTERED_LIST", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2000;
                    int i;
                    for (i = 0; i < objParams.Length; i++)
                        cmd.Parameters.Add(objParams[i]);
                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        ds = new DataSet();
                        da.Fill(ds);
                    }

                //}
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetStudentsForInterview-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetStudentListByComp(int Scheduleno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCHEDULENO", Scheduleno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_STUDENTLIST_BYCOMPANY", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetStudentListByComp-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetStudentInfoForComp(int branchNo, char studType)
                {
                    DataSet ds = null;
                    //try
                    //{
                    SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                    SqlParameter[] objParams = null;
                    objParams = new SqlParameter[2];
                    objParams[0] = new SqlParameter("@P_BRANCHNO", branchNo);
                    objParams[1] = new SqlParameter("@P_STUDENT_TYPE", studType);
                    // ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_TP_REPORT_STUDENT_INFO_FOR_COMPANY", objParams);

                    SqlConnection conn = new SqlConnection(_nitprm_constr);
                    SqlCommand cmd = new SqlCommand("PKG_ACD_TP_REPORT_STUDENT_INFO_FOR_COMPANY", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2000;
                    int i;
                    for (i = 0; i < objParams.Length; i++)
                        cmd.Parameters.Add(objParams[i]);
                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        ds = new DataSet();
                        da.Fill(ds);
                    }

                //}
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetStudentInfoForComp-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetStudentInfoForCompNew(int sessionNo, int schemeNo, int semesterno, int tpReg)
                {
                    DataSet ds = null;
                    //try
                    //{
                    SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                    SqlParameter[] objParams = null;
                    objParams = new SqlParameter[4];
                    objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                    objParams[1] = new SqlParameter("@P_SCHEMENO", schemeNo);
                    objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                    objParams[3] = new SqlParameter("@P_ALL_OR_TPREGISTERED", tpReg);
                    //ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_STUDINFO_FOR_COMPANY", objParams);
                    //PKG_ACAD_TP_STUDACADINFO_FOR_COMPANY
                    //PKG_ACD_TP_REPORT_STUDENT_INFO_FOR_COMPANY

                    SqlConnection conn = new SqlConnection(_nitprm_constr);
                    SqlCommand cmd = new SqlCommand("PKG_ACAD_TP_STUDINFO_FOR_COMPANY", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2000;
                    int i;
                    for (i = 0; i < objParams.Length; i++)
                        cmd.Parameters.Add(objParams[i]);
                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        ds = new DataSet();
                        da.Fill(ds);
                    }

                //}
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetStudentInfoForCompNew-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetStudforSelection(int Scheduleno, int Admbatch, string Branch, string WAName, int classno, int Backlog, int SemNo, Double Per, double ten, double twelth, double allSemAgg)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_SCHEDULENO", Scheduleno);
                        objParams[1] = new SqlParameter("@P_ADMBATCH", Admbatch);

                        if (Branch.Trim().Equals(string.Empty))
                            objParams[2] = new SqlParameter("@P_BRANCHNO", DBNull.Value);
                        else
                            objParams[2] = new SqlParameter("@P_BRANCHNO", Branch);

                        if (WAName.Trim().Equals(string.Empty))
                            objParams[3] = new SqlParameter("@P_WORKAREANAME", DBNull.Value);
                        else
                            objParams[3] = new SqlParameter("@P_WORKAREANAME", WAName);

                        objParams[4] = new SqlParameter("@P_CLASSNO ", classno);
                        objParams[5] = new SqlParameter("@P_NO_OF_BACKLOG_SUBJECTS", Backlog);
                        objParams[6] = new SqlParameter("@P_SEMESTERNO", SemNo);
                        objParams[7] = new SqlParameter("@P_SEM_PER", Per);
                        //objParams[8] = new SqlParameter("@P_TEN_TWELTH", tenTwelth);
                        objParams[8] = new SqlParameter("@P_TEN", ten);
                        objParams[9] = new SqlParameter("@P_TWELTH", twelth);
                        objParams[10] = new SqlParameter("@P_ALL_SEM_AGG", allSemAgg);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_STUDENT_REGISTERED_LIST1", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetStudforSelection-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public DataSet GetScheduleHis()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_HIST_SCHEDULE_ADMIN", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetScheduleHis-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public DataSet GetComSelectionAfterIntv()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLhelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];
                        ds = objSQLhelper.ExecuteDataSetSP("PKG_ACAD_TP_STUDENT_SELECTION_LIST_AFTER_INTERVIEW", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.Getstud_forSelectionProcess-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;

                }

                public DataSet Getstud_forSelectionProcess(int ScheduleNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCHEDULENO", ScheduleNo);
                        //ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_STUDENT_SELECTION_LIST", objParams);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_STUDENT_SELECTION_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.Getstud_forSelectionProcess-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int UpdateTPReg_INTVSELECT(string IDNOS, Int32 SCHEDULENO, string Type, string Colcode)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_IDNOS", IDNOS);
                        objParams[1] = new SqlParameter("@P_SCHEDULENO", SCHEDULENO);
                        objParams[2] = new SqlParameter("@P_TYPE", Type);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", Colcode);
                        objParams[4] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_REGISTER_UPDATE_INTVSELECT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateTPReg_INTVSELECT-> " + ex.ToString());

                    }
                    return retStatus;

                }

                public int InsertSelectionProcess(string IDNOS, Int32 SCHEDULENO, int RoundNo, string RoundName, string Colcode)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];

                        objParams[0] = new SqlParameter("@P_SCHEDULENO", SCHEDULENO);
                        objParams[1] = new SqlParameter("@P_ROUNDNO", RoundNo);
                        objParams[2] = new SqlParameter("@P_ROUNDNAME", RoundName);
                        objParams[3] = new SqlParameter("@P_IDS", IDNOS);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", Colcode);
                        objParams[5] = new SqlParameter("@P_SPNO", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_STUDENT_SELECTION_PROCESS_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.InsertSelectionProcess-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetNonAppliedStu(int Scheduleno, int Branchno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SCHEDULENO", Scheduleno);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", Branchno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_GET_NON_APPLIED_STU", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetNonAppliedStu-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetSelectedCompany(int IdNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", IdNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_STUDENT_COMPANY_CONFIRM", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetSelectedCompany-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetCompIntStu(int idno, int rno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLhelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_COMPID", idno);
                        objParams[1] = new SqlParameter("@P_ROUNDNO", rno);
                        ds = objSQLhelper.ExecuteDataSetSP("PKG_ACAD_TP_STUDENTLIST_AFTER_INTV", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetCompIntStu-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateTPReg_StudConfirm(int IDNO, string SCHEDULENO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_SCHEDULENO", SCHEDULENO);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_REGISTER_UPDATE_STUDCONFIRM", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateTPReg_COMPSELECT-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddSelectedStud(int SCHEDULENO, int IDNO, string Desig, double stipend, double salary, int contract, string ColCode)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SCHEDULENO", SCHEDULENO);
                        objParams[1] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[2] = new SqlParameter("@P_DESIGNATION", Desig);
                        objParams[3] = new SqlParameter("@P_STIPEND", stipend);
                        objParams[4] = new SqlParameter("@P_SALARY", salary);
                        objParams[5] = new SqlParameter("@P_CONTRACTFORYEAR", contract);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_TRNO", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_SELECTSTUDINFO_INSERT", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddSelectedStud-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public SqlDataReader GetIdBySchedule(int Scheduleno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCHEDULENO", Scheduleno);
                        dr = objSQLHelper.ExecuteReaderSP("PKG_ACAD_TP_GET_ID_BY_SCHEDULENO", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetIdBySchedule-> " + ex.ToString());
                    }
                    return dr;
                }

                public SqlDataReader GetRegidBySchedule_Stype(int Scheduleno, Char StudType)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SCHEDULENO", Scheduleno);
                        objParams[1] = new SqlParameter("@P_SType", StudType);
                        dr = objSQLHelper.ExecuteReaderSP("PKG_ACAD_TP_GET_REGID_BY_SCHEDULENO", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetIdBySchedule-> " + ex.ToString());
                    }
                    return dr;
                }

                public int CheckkStudSelected(int IDNO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_TRNO", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_VERIFY_SELECTSTUDINFO", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.CheckkStudSelected-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int ChkStudentEligible(int IDNO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_COUNT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_VERIFY_STUDENTYEAR", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.ChkStudentEligible-> " + ex.ToString());
                    }
                    return retStatus;
                }




                

                //FOR T&P Registration
                public DataSet GetstudentDetailByRegNo(string REGNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_REGNO", REGNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_STUDENT_DETAILS_BY_REGNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetstudentDetailByRegNo-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetLastQualificationByIDNo(int IDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STU_LASTQUAL_BY_IDNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetLastQualificationByIDNo-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetTpCurrentColExam(int IDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_CURRENT_COL_EXAM_1", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetTpLastQualificationByIDNo-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetTpLastQualificationByIDNo(int IDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_STU_LASTQUAL_BY_IDNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetTpLastQualificationByIDNo-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Method Added in date 14 April 2014
                /// </summary>
                /// <param name="IdNo"></param>
                /// <param name="RegNo"></param>
                /// <param name="objStudReg"></param>
                /// <param name="objTPLastqual"></param>
                /// <param name="objTPTrain"></param>
                /// <returns></returns>
                public DataSet GetTraiandPalcementByIDNo(int IDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STU_IND_TRAIN_BY_IDNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetTraiandPalcementByIDNo-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Method Added on dated 14 April 2012
                /// </summary>
                /// <param name="IdNo"></param>
                /// <param name="RegNo"></param>
                /// <param name="objStudReg"></param>
                /// <param name="objTPLastqual"></param>
                /// <param name="objTPTrain"></param>
                /// <returns></returns>

                public DataSet GetJobLocatioByIDNo(int IDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STU_JOB_LOC_BY_IDNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLocatioByIDNo-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// MEthod added on dated 14 april 2014
                /// </summary>
                /// <param name="IdNo"></param>
                /// <param name="RegNo"></param>
                /// <param name="objStudReg"></param>
                /// <param name="objTPLastqual"></param>
                /// <param name="objTPTrain"></param>
                /// <returns></returns>
                public DataSet GetWorkAreaByIDNo(int IDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STU_WORK_AREA_BY_IDNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetWorkAreaByIDNo-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStuProjectByIDNo(int IDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_STU_WORK_AREA_BY_IDNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetWorkAreaByIDNo-> " + ex.ToString());
                    }
                    return ds;
                }

                // Update by sumit-- 25092019 //

                //public int StudentRegistrataion_TP(int IdNo, string RegNo, TPStudRegistration objStudReg, TPLASTQUAL objTPLastqual, TPTraining objTPTrain, string LPinCode)
                //{
                //    int retStatus = 0;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null;
                //        objParams = new SqlParameter[32];
                //        objParams[0] = new SqlParameter("@P_IDNO", IdNo);
                //        objParams[1] = new SqlParameter("@P_QUALIFYNO", objTPLastqual.ExamNos);
                //        objParams[2] = new SqlParameter("@P_YEAR_OF_EXAMHSSC", objTPLastqual.PassingYrs);
                //        objParams[3] = new SqlParameter("@P_MARKSOBT", objTPLastqual.MrkObt);
                //        objParams[4] = new SqlParameter("@P_MARKSOUTOF", objTPLastqual.MrkOutOf);
                //        objParams[5] = new SqlParameter("@P_PERCENTAGE", objTPLastqual.Percentages);
                //        objParams[6] = new SqlParameter("@P_BOARD", objTPLastqual.Board);
                //        objParams[7] = new SqlParameter("@P_GRADE", objTPLastqual.Grade);
                //        objParams[8] = new SqlParameter("@P_CGPA", objTPLastqual.Cgpas);
                //        objParams[9] = new SqlParameter("@P_ORG", objTPTrain.Orgs);
                //        objParams[10] = new SqlParameter("@P_PERIOD", objTPTrain.Periods);
                //        objParams[11] = new SqlParameter("@P_SUBJECT", objTPTrain.Subjects);
                //        objParams[12] = new SqlParameter("@P_SALARY", objTPTrain.Salaries);
                //        objParams[13] = new SqlParameter("@P_JOBLOCNO", objTPTrain.JLNos);
                //        objParams[14] = new SqlParameter("@P_JOBLOCATION", objTPTrain.JLNames);
                //        objParams[15] = new SqlParameter("@P_WORKAREANO", objTPTrain.WorkAreaNos);
                //        objParams[16] = new SqlParameter("@P_WORKAREANAME", objTPTrain.WorkAreaNames);
                //        objParams[17] = new SqlParameter("@P_PROJTITLE", objTPTrain.ProjTitle);
                //        objParams[18] = new SqlParameter("@P_PROJDURATION", objTPTrain.ProjDuration);
                //        objParams[19] = new SqlParameter("@P_PROJDETAIL", objTPTrain.ProjDetails);
                //        objParams[20] = new SqlParameter("@P_REGDATE", objStudReg.RegDate);
                //        objParams[21] = new SqlParameter("@P_REGSTATUS", objStudReg.RegStatus);
                //        objParams[22] = new SqlParameter("@P_STUDENT_TYPE", objStudReg.StudentType);
                //        objParams[23] = new SqlParameter("@P_OTHER_QUALIFICATION", objStudReg.OtherQualification);
                //        objParams[24] = new SqlParameter("@P_COLLEGE_CODE", objStudReg.College_Code);
                //        objParams[25] = new SqlParameter("@P_REGNO", RegNo);
                //        objParams[26] = new SqlParameter("@P_CLASSNO ", objStudReg.Gradeclass);

                //        objParams[27] = new SqlParameter("@P_MOBILE ", objStudReg.Mobile);
                //        objParams[28] = new SqlParameter("@P_EMAIL ", objStudReg.Email);
                //        objParams[29] = new SqlParameter("@P_ADDRESS ", objStudReg.Address);
                //        objParams[30] = new SqlParameter("@P_LPINCODE", LPinCode);  //Added By Nikhil on 19/11/2020
                //        //objParams[8] = new SqlParameter("@P_SEQNO_T", objTPTrain.SeqNos);
                //        //objParams[13] = new SqlParameter("@P_SEQNO_J", objTPTrain.JLSeqNos);
                //        //objParams[16] = new SqlParameter("@P_SEQNO_W", objTPTrain.WSeqNos);

                //        //objParams[27] = new SqlParameter("@P_SEMESTERNAME", objTPLastqual.Semesetername);
                //        //objParams[29] = new SqlParameter("@P_QEXMROLLNO", objTPLastqual.RollNo);
                //        //objParams[32] = new SqlParameter("@P_PERCENTILE", objTPLastqual.Percentile);
                //        //objParams[4] = new SqlParameter("@P_NO_OF_SUBJECTS_OFFERED", objTPLastqual.OfferSubjects);
                //        //objParams[5] = new SqlParameter("@P_NO_OF_SUBJECTS_PASSED", objTPLastqual.PassedSubjects);
                //        //objParams[6] = new SqlParameter("@P_NO_OF_BACKLOG_SUBJECTS", objTPLastqual.Backlogs);
                //        //objParams[7] = new SqlParameter("@P_REMARK", objTPLastqual.Remarks);

                //        objParams[31] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //        objParams[31].Direction = ParameterDirection.Output;
                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_REGISTRATION1", objParams, true);

                //        if (Convert.ToInt32(ret) == -99)
                //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //        else
                //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.StudentRegistrataion_TP-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}


                /// <summary>//
                /// Updated By Nikhil - 18/10/2021
                /// </summary>
                /// <param name="IdNo"></param>
                /// <param name="RegNo"></param>
                /// <param name="objStudReg"></param>
                /// <param name="objTPLastqual"></param>
                /// <param name="objTPTrain"></param>
                /// <param name="Lpincode"></param>
                /// <returns></returns>
                public int StudentRegistrataion_TP(int IdNo, string RegNo, TPStudRegistration objStudReg, TPLASTQUAL objTPLastqual, TPTraining objTPTrain, string Lpincode)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[34];
                        objParams[0] = new SqlParameter("@P_IDNO", IdNo);
                        objParams[1] = new SqlParameter("@P_QUALIFYNO", objTPLastqual.ExamNos);
                        objParams[2] = new SqlParameter("@P_YEAR_OF_EXAMHSSC", objTPLastqual.PassingYrs);
                        objParams[3] = new SqlParameter("@P_MARKSOBT", objTPLastqual.MrkObt);
                        objParams[4] = new SqlParameter("@P_MARKSOUTOF", objTPLastqual.MrkOutOf);
                        objParams[5] = new SqlParameter("@P_PERCENTAGE", objTPLastqual.Percentages);
                        objParams[6] = new SqlParameter("@P_BOARD", objTPLastqual.Board);
                        objParams[7] = new SqlParameter("@P_GRADE", objTPLastqual.Grade);
                        objParams[8] = new SqlParameter("@P_CGPA", objTPLastqual.Cgpas);
                        objParams[9] = new SqlParameter("@P_ORG", objTPTrain.Orgs);
                        objParams[10] = new SqlParameter("@P_PERIOD", objTPTrain.Periods);
                        objParams[11] = new SqlParameter("@P_SUBJECT", objTPTrain.Subjects);
                        objParams[12] = new SqlParameter("@P_SALARY", objTPTrain.Salaries);
                        objParams[13] = new SqlParameter("@P_JOBLOCNO", objTPTrain.JLNos);
                        objParams[14] = new SqlParameter("@P_JOBLOCATION", objTPTrain.JLNames);
                        objParams[15] = new SqlParameter("@P_WORKAREANO", objTPTrain.WorkAreaNos);
                        objParams[16] = new SqlParameter("@P_WORKAREANAME", objTPTrain.WorkAreaNames);
                        objParams[17] = new SqlParameter("@P_PROJTITLE", objTPTrain.ProjTitle);
                        objParams[18] = new SqlParameter("@P_PROJDURATION", objTPTrain.ProjDuration);
                        objParams[19] = new SqlParameter("@P_PROJDETAIL", objTPTrain.ProjDetails);
                        objParams[20] = new SqlParameter("@P_REGDATE", objStudReg.RegDate);
                        objParams[21] = new SqlParameter("@P_REGSTATUS", objStudReg.RegStatus);
                        objParams[22] = new SqlParameter("@P_STUDENT_TYPE", objStudReg.StudentType);
                        objParams[23] = new SqlParameter("@P_OTHER_QUALIFICATION", objStudReg.OtherQualification);
                        objParams[24] = new SqlParameter("@P_COLLEGE_CODE", objStudReg.College_Code);
                        objParams[25] = new SqlParameter("@P_REGNO", RegNo);
                        objParams[26] = new SqlParameter("@P_CLASSNO ", objStudReg.Gradeclass);

                        objParams[27] = new SqlParameter("@P_MOBILE", objStudReg.Mobile);
                        objParams[28] = new SqlParameter("@P_EMAIL", objStudReg.Email);
                        objParams[29] = new SqlParameter("@P_ADDRESS", objStudReg.Address);

                        //objParams[30] = new SqlParameter("@P_FILENAME ", objStudReg.FileName);
                        objParams[30] = new SqlParameter("@P_LPINCODE", Lpincode);
                        //objParams[32] = new SqlParameter("@P_CONSENTFORM ", objStudReg.ConsentFormFileName);  // added by sumit for upload consent form by student on 16-07-2020
                        objParams[31] = new SqlParameter("@P_FILENAME", objStudReg.FileName);  //Added By Swapnil on 06/10/2021
                        objParams[32] = new SqlParameter("@P_CONSENTFORM", objStudReg.ConsentFormFileName);  //Added By Swapnil on 06/10/2021
                        //objParams[8] = new SqlParameter("@P_SEQNO_T", objTPTrain.SeqNos);
                        //objParams[13] = new SqlParameter("@P_SEQNO_J", objTPTrain.JLSeqNos);
                        //objParams[16] = new SqlParameter("@P_SEQNO_W", objTPTrain.WSeqNos);

                        //objParams[27] = new SqlParameter("@P_SEMESTERNAME", objTPLastqual.Semesetername);
                        //objParams[29] = new SqlParameter("@P_QEXMROLLNO", objTPLastqual.RollNo);
                        //objParams[32] = new SqlParameter("@P_PERCENTILE", objTPLastqual.Percentile);
                        //objParams[4] = new SqlParameter("@P_NO_OF_SUBJECTS_OFFERED", objTPLastqual.OfferSubjects);
                        //objParams[5] = new SqlParameter("@P_NO_OF_SUBJECTS_PASSED", objTPLastqual.PassedSubjects);
                        //objParams[6] = new SqlParameter("@P_NO_OF_BACKLOG_SUBJECTS", objTPLastqual.Backlogs);
                        //objParams[7] = new SqlParameter("@P_REMARK", objTPLastqual.Remarks);

                        objParams[33] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[33].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_REGISTRATION1", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.StudentRegistrataion_TP-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //ADDED BY ROHIT 23-12-2021
                public DataSet CheckStudentTPRegister(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_CHECK_REGISTER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.CheckStudentTPRegister-> " + ex.ToString());
                    }
                    return ds;
                }


                public int UpdateRegistrataion_TP(int IdNo, string RegNo, TPStudRegistration objStudReg, TPLASTQUAL objTPLastqual, TPTraining objTPTrain, int isTpo, TPStudent objTPStudent)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[35];
                        objParams[0] = new SqlParameter("@P_IDNO", IdNo);
                        objParams[1] = new SqlParameter("@P_QUALIFYNO", objTPLastqual.ExamNos);
                        objParams[2] = new SqlParameter("@P_YEAR_OF_EXAMHSSC", objTPLastqual.PassingYrs);
                        objParams[3] = new SqlParameter("@P_MARKSOBT", objTPLastqual.MrkObt);
                        objParams[4] = new SqlParameter("@P_MARKSOUTOF", objTPLastqual.MrkOutOf);
                        objParams[5] = new SqlParameter("@P_PERCENTAGE", objTPLastqual.Percentages);
                        objParams[6] = new SqlParameter("@P_CGPA", objTPLastqual.Cgpas);
                        objParams[7] = new SqlParameter("@P_BOARD", objTPLastqual.Board);
                        objParams[8] = new SqlParameter("@P_GRADE", objTPLastqual.Grade);
                        //objParams[9] = new SqlParameter("@P_ATTEMPT", objTPLastqual.Attempts);

                        objParams[9] = new SqlParameter("@P_ORG", objTPTrain.Orgs);
                        objParams[10] = new SqlParameter("@P_PERIOD", objTPTrain.Periods);
                        objParams[11] = new SqlParameter("@P_SUBJECT", objTPTrain.Subjects);
                        objParams[12] = new SqlParameter("@P_SALARY", objTPTrain.Salaries);

                        objParams[13] = new SqlParameter("@P_JOBLOCNO", objTPTrain.JLNos);
                        objParams[14] = new SqlParameter("@P_JOBLOCATION", objTPTrain.JLNames);
                        objParams[15] = new SqlParameter("@P_WORKAREANO", objTPTrain.WorkAreaNos);
                        objParams[16] = new SqlParameter("@P_WORKAREANAME", objTPTrain.WorkAreaNames);
                        objParams[17] = new SqlParameter("@P_PROJTITLE", objTPTrain.ProjTitle);
                        objParams[18] = new SqlParameter("@P_PROJDURATION", objTPTrain.ProjDuration);
                        objParams[19] = new SqlParameter("@P_PROJDETAIL", objTPTrain.ProjDetails);
                        objParams[20] = new SqlParameter("@P_STUDENT_TYPE", objStudReg.StudentType);
                        objParams[21] = new SqlParameter("@P_OTHER_QUALIFICATION", objStudReg.OtherQualification);
                        objParams[22] = new SqlParameter("@P_COLLEGE_CODE", objStudReg.College_Code);
                        objParams[23] = new SqlParameter("@P_REGNO", RegNo);
                        objParams[24] = new SqlParameter("@P_CLASSNO ", objStudReg.Gradeclass);
                        objParams[25] = new SqlParameter("@P_ISTPO", isTpo);



                        objParams[26] = new SqlParameter("@P_DOB", objTPStudent.Dob);
                        objParams[27] = new SqlParameter("@P_HEIGHT", objTPStudent.Height);
                        objParams[28] = new SqlParameter("@P_WEIGHT", objTPStudent.Weight);

                        objParams[29] = new SqlParameter("@P_LADDRESS", objTPStudent.LAddress);

                        objParams[30] = new SqlParameter("@P_CONTACTNO", objTPStudent.ContactNo);
                        objParams[31] = new SqlParameter("@P_LPINCODE", objTPStudent.LPinCode);
                        objParams[32] = new SqlParameter("@P_STUD_MOBILE_NO", objTPStudent.Mobile);
                        objParams[33] = new SqlParameter("@P_STUD_EMAIL_ID", objTPStudent.EmailID);
                        //objParams[26] = new SqlParameter("@P_JOBLOCNO", objTPStudent.StudName);
                        // objParams[30] = new SqlParameter("@P_PROJTITLE", objTPStudent.BloodGroupNo);
                        // objParams[29] = new SqlParameter("@P_PROJDETAIL", objTPStudent.LCity);


                        //objParams[25] = new SqlParameter("@P_SEMESTERNAME", objTPLastqual.Semesetername);
                        //objParams[27] = new SqlParameter("@P_QEXMROLLNO", objTPLastqual.RollNo);
                        //objParams[30] = new SqlParameter("@P_PERCENTILE", objTPLastqual.Percentile);
                        //objParams[4] = new SqlParameter("@P_NO_OF_SUBJECTS_OFFERED", objTPLastqual.OfferSubjects);
                        //objParams[5] = new SqlParameter("@P_NO_OF_SUBJECTS_PASSED", objTPLastqual.PassedSubjects);
                        //objParams[6] = new SqlParameter("@P_NO_OF_BACKLOG_SUBJECTS", objTPLastqual.Backlogs);
                        //objParams[7] = new SqlParameter("@P_REMARK", objTPLastqual.Remarks);
                        //objParams[13] = new SqlParameter("@P_SEQNO_J", objTPTrain.JLSeqNos);
                        //objParams[8] = new SqlParameter("@P_SEQNO_T", objTPTrain.SeqNos);
                        //objParams[16] = new SqlParameter("@P_SEQNO_W", objTPTrain.WSeqNos);
                        //objParams[16] = new SqlParameter("@P_REGDATE", objStudReg.RegDate);
                        //objParams[17] = new SqlParameter("@P_REGSTATUS", objStudReg.RegStatus);
                        objParams[34] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[34].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_REGISTRATION_UPDATE", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateRegistrataion_TP-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdSscHscPer_TP(int IdNo, TPLASTQUAL objTPLastqual)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_IDNO", IdNo);
                        objParams[1] = new SqlParameter("@P_QUALIFYNO", objTPLastqual.Examnos_1012);
                        objParams[2] = new SqlParameter("@P_YEAR_OF_EXAMHSSC", objTPLastqual.PassingYrs_1012);
                        objParams[3] = new SqlParameter("@P_PERCENTAGE", objTPLastqual.Percentages_1012);
                        objParams[4] = new SqlParameter("@P_ATTEMPT", objTPLastqual.Attempts_1012);
                        objParams[5] = new SqlParameter("@P_BOARD", objTPLastqual.Board_1012);
                        objParams[6] = new SqlParameter("@P_MARKSOBT", objTPLastqual.mrkObt_1012);
                        objParams[7] = new SqlParameter("@P_MARKSOUTOF", objTPLastqual.mrkOutOf_1012);
                        objParams[8] = new SqlParameter("@P_GRADE", objTPLastqual.grade_1012);
                        objParams[9] = new SqlParameter("@P_CGPA", objTPLastqual.Cgpas_1012);
                        //objParams[8] = new SqlParameter("@P_PERCENTILE", objTPLastqual.percentile_1012);
                        //objParams[10] = new SqlParameter("@P_QEXMROLLNO", objTPLastqual.rollno_1012);

                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("[PKG_ACAD_TP_SSCHSC_PER_UPD]", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdSscHscPer_TP-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public void Assign_Link_Student(int IdNo, string uaname)
                {

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", IdNo);
                        objParams[1] = new SqlParameter("@P_UANAME", uaname);
                        objSQLHelper.ExecuteDataSetSP("PKG_ASSIGN_LINK_STUDENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.Assign_Link_Student-> " + ex.ToString());
                    }
                }
                public int VerifyStudRegistration(string idno)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_REGNO", idno);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_STUD_REG_VERIFY", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.VerifyStudRegistration-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetStudentListToEdit(int AdmBatch, int BranchNo, char StudType)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", AdmBatch);
                        objParams[1] = new SqlParameter("@P_BRANCH", BranchNo);
                        objParams[2] = new SqlParameter("@P_STUDENT_TYPE", StudType);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_STUDENT_SELECT_CRITERIA", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetStudentListToApprove-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateStudRegStaus(string IDNO, string RegNo, int org)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_REGNO", RegNo);
                        objParams[2] = new SqlParameter("@P_OrganizationId", org);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_UPDATE_STUD_REGSTATUS", objParams, true);
                        //retStatus = Convert.ToInt32(ret);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateStudRegStaus-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AllowStudent_BioEdit(string IDNOs)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNOs);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_ALLOW_STUD_BIOEDIT", objParams, true);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateStudRegStaus-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #region Work Area
                /// <summary>
                /// Updated By abhishek 07092019
                /// </summary>
                /// <param name="workarea"></param>
                /// <param name="ColCode"></param>
                /// <returns></returns>
                public int AddWorkArea(string workarea, string ColCode)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_WORKAREANAME", workarea);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_WORKAREA_INSERT", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddWorkArea-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// Updated By Sumit 21092019
                /// </summary>
                /// <param name="workarea"></param>
                /// <param name="ColCode"></param>
                /// <returns></returns>

                public int UpdateWorkArea(int Workareano, string workarea)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_WORKAREANO", Workareano);
                        objParams[1] = new SqlParameter("@P_WORKAREANAME", workarea);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_WORKAREA_UPDATE", objParams, true);

                        // if (objSQLHelper.ExecuteNonQuerySP("PKG_TP_WORKAREA_UPDATE", objParams, false) != null)
                        //  retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);

                        //if (Convert.ToInt32(ret) == -99)
                        //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        //else
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateWorkArea(-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet Getworkarea(int Workareano)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_WORKAREANO", Workareano);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_WORKAREA_GETDATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.Getworkarea-> " + ex.ToString());

                    }
                    return ds;
                }

                #endregion

                #region Job Locaction
                /// <summary>
                /// updated by abhishek 07092019
                /// </summary>
                /// <param name="JobLoc"></param>
                /// <param name="ColCode"></param>
                /// <returns></returns>
                public int AddJobLoc(string JobLoc, string ColCode)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_JOBLOCATION", JobLoc);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_JOBLOC_INSERT", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }
                /// <summary>
                /// Updated by Swapnil - 16092019
                /// </summary>
                /// <param name="JobLocNo"></param>
                /// <param name="JobLoc"></param>
                /// <returns></returns>
                public int UpdateJobLoc(int JobLocNo, string JobLoc)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_JOBLOCNO", JobLocNo);
                        objParams[1] = new SqlParameter("@P_JOBLOCATION", JobLoc);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_JOBLOC_UPDATE", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetJobLoc(int JobLocNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_JOBLOCNO", JobLocNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_JOBLOC_GETDATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }
                public DataSet getJobLocbyId(int IDNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_STUDENT_JOBLOC", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.getJobLocbyId-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                public DataSet getWorkareabyId(int IDNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_STUDENT_WORKRAREA", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.getWorkareabyId-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet getTrainbyId(int IDNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_STUDENT_TRAINING", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.getTrainbyId-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudByRoundNo(int ScheduleNo, int RoundNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SCHEDULENO", ScheduleNo);
                        objParams[1] = new SqlParameter("@P_ROUNDNO", RoundNo);
                        objParams[2] = new SqlParameter("@P_REPORT", "A");
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_STUDENT_SELECTION_LIST_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.getWorkareabyId-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public int Update_SelectionProcessByNo(int ScheduleNo, int RoundNo, string ids)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SCHEDULENO", ScheduleNo);
                        objParams[1] = new SqlParameter("@P_ROUNDNO", RoundNo);
                        objParams[2] = new SqlParameter("@P_IDS", ids);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_STUDENT_SELECTION_PROCESS_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.getWorkareabyId-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GeTComp_Info()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_SELECTONOFSTUDENTSFORINTERVIEW__BYTP_EDIT", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.Getstud_forSelectionProcess-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public DataSet GetAllSession()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_SESSION_GET_ALL", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetAllSession-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetSessionByNO(int SessionNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_SESSION_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetSessionByNO-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public int AddSession(string SESSIONNAME, DateTime FrmDt, DateTime ToDt, string ColCode)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNAME", SESSIONNAME);
                        objParams[1] = new SqlParameter("@P_FROMDATE", FrmDt);
                        objParams[2] = new SqlParameter("@P_TODATE", ToDt);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[4] = new SqlParameter("@P_SESSIONNO", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_SESSION_INSERT", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddSession-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int UpdateSession(int SessionNo, string SESSIONNAME, DateTime FrmDt, DateTime Todt)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[1] = new SqlParameter("@P_SESSIONNAME", SESSIONNAME);
                        objParams[2] = new SqlParameter("@P_FROMDATE", FrmDt);
                        objParams[3] = new SqlParameter("@P_TODATE", Todt);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_SESSION_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                        // object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_INPLANT_SESSION_UPDATE", objParams, true);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateSession-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet getIndustrybyId(int IDNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_STUDENT_IPINDUSTRY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.getJobLocbyId-> " + ex.ToString());
                    }
                    return ds;
                }

                public int ChkStudentEligibleIP(int IDNO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_COUNT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_VERIFY_STUDENTBRANCH", objParams, true);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.ChkStudentEligibleIP-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int UpdateRegistrataion_InPlant(int IdNo, string Regno, TPLASTQUAL objTPLastqual, string CompIds, string CompNames, string Colcode, string TAddr, string TCntNo, string Region)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[17];
                        objParams[0] = new SqlParameter("@P_IDNO", IdNo);
                        objParams[1] = new SqlParameter("@P_REGNO", Regno);
                        objParams[2] = new SqlParameter("@P_QUALIFYNO", objTPLastqual.ExamNos);
                        objParams[3] = new SqlParameter("@P_YEAR_OF_EXAMHSSC", objTPLastqual.PassingYrs);
                        objParams[4] = new SqlParameter("@P_ATTEMPT", objTPLastqual.Attempts);
                        objParams[5] = new SqlParameter("@P_NO_OF_SUBJECTS_OFFERED", objTPLastqual.OfferSubjects);
                        objParams[6] = new SqlParameter("@P_NO_OF_SUBJECTS_PASSED", objTPLastqual.PassedSubjects);
                        objParams[7] = new SqlParameter("@P_NO_OF_BACKLOG_SUBJECTS", objTPLastqual.Backlogs);
                        objParams[8] = new SqlParameter("@P_REMARK", objTPLastqual.Remarks);
                        objParams[9] = new SqlParameter("@P_PERCENTAGE", objTPLastqual.Percentages);
                        objParams[10] = new SqlParameter("@P_COMPID", CompIds);
                        objParams[11] = new SqlParameter("@P_COMPANYNAME", CompNames);
                        objParams[12] = new SqlParameter("@P_COLLEGE_CODE", Colcode);
                        objParams[13] = new SqlParameter("@P_TEMPADDR", TAddr);
                        objParams[14] = new SqlParameter("@P_TEMPCONTACTNO", TCntNo);
                        objParams[15] = new SqlParameter("@P_REGION", Region);
                        objParams[16] = new SqlParameter("@P_IPREGNO", SqlDbType.Int);
                        objParams[16].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_INPLANT_REGISTRATION", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateRegistrataion_TP-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetTrainingAll(int SessionNo, int Branch)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSION", SessionNo);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", Branch);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_INPLANT_TRAINING_GET_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetTrainingAll-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public int AddIPTraining(int Branch, int Session, DateTime FrmDt, DateTime ToDt, string ColCode, int SemNo, DateTime ExtendDt)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_BRANCHNO", Branch);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", Session);
                        objParams[2] = new SqlParameter("@P_FROMDATE", FrmDt);
                        objParams[3] = new SqlParameter("@P_TODATE", ToDt);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[5] = new SqlParameter("@P_SEMESTERNO", SemNo);
                        if (ExtendDt == DateTime.MinValue)
                            objParams[6] = new SqlParameter("@P_EXTENDDATE", DBNull.Value);
                        else
                            objParams[6] = new SqlParameter("@P_EXTENDDATE", ExtendDt);
                        objParams[7] = new SqlParameter("@P_IPTNO", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_INPLANT_TRAINING_INSERT", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddIPTraining-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateIPTraining(int IPTNO, int Branch, int Session, DateTime FrmDt, DateTime ToDt, int semNo, DateTime EXTENDDATE)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_IPTNO", IPTNO);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", Branch);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", Session);
                        objParams[3] = new SqlParameter("@P_FROMDATE", FrmDt);
                        objParams[4] = new SqlParameter("@P_TODATE", ToDt);
                        objParams[5] = new SqlParameter("@P_SEMESTERNO", semNo);
                        if (EXTENDDATE == DateTime.MinValue)
                            objParams[6] = new SqlParameter("@P_EXTENDDATE", DBNull.Value);
                        else
                            objParams[6] = new SqlParameter("@P_EXTENDDATE", EXTENDDATE);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_INPLANT_TRAINING_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);


                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateIPTraining-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetTrainingByNo(int IPTNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IPTNO", IPTNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_INPLANT_TRAINING_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetTrainingByNo-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetComBranch()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_TP_GET_LIST_IP_COM_BRANCH", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetComBranch-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;

                }
                public DataSet GetComBranchStuName(int Compid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COMPID", Compid);
                        //objParams[1] = new SqlParameter("@P_BRANCHNO", Bno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_TP_GET_LIST_IP_STUDENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetComBranch-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;

                }
                public DataSet Getstud_forSelectionIP(int SessionNo, int CompId, int BranchNo, int Backlog, int SemNo, double SemPer)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[1] = new SqlParameter("@P_COMPID", CompId);
                        if (BranchNo.ToString().Trim().Equals(string.Empty))
                            objParams[2] = new SqlParameter("@P_BRANCHNO", DBNull.Value);
                        else
                            objParams[2] = new SqlParameter("@P_BRANCHNO", BranchNo);

                        objParams[3] = new SqlParameter("@P_NO_OF_BACKLOG_SUBJECTS", Backlog);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", SemNo);
                        objParams[5] = new SqlParameter("@P_SEM_PER", SemPer);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_INPLANT_REPORT_FINALLIST", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.Getstud_forSelectionIPw-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public int InsertIPReg(string IDNOS, int CompId, string ColCode)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_COMPID", CompId);
                        objParams[1] = new SqlParameter("@P_IDNO", IDNOS);

                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[3] = new SqlParameter("@P_TREGNO", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_TP_IP_TRAINING_REGISTER_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateTPReg_INTVSELECT-> " + ex.ToString());

                    }
                    return retStatus;
                }
                public DataSet CheckScheduleDate(string IntvFrom, string IntvTo, int CompId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParameter = new SqlParameter[3];
                        objParameter[0] = new SqlParameter("@P_INTVFROM", IntvFrom);
                        objParameter[1] = new SqlParameter("@P_INTVTO", IntvTo);
                        objParameter[2] = new SqlParameter("@P_COMPID", CompId);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_CHECK_SCHEDULE_DATE_BY_COMPID", objParameter);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.PKG_ACAD_TP_CHECK_SCHEDULE_DATE_BY_COMPID->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public SqlDataReader GetStudIDsForCompany(int CompId)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COMPID", CompId);
                        dr = objSQLHelper.ExecuteReaderSP("PKG_ACAD_TP_IP_GET_STUDIDS", objParams);

                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.Getstud_forSelectionIPw-> " + ex.ToString());
                    }

                    return dr;
                }

                public int SetCurSession(int SessionNo)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_SET_CURRENT_SESSION", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateIPTraining-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int UpdateIPSelection(int IDNO, int COMPID, string COMPNAME, int FACULTY)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_COMPID", COMPID);
                        objParams[2] = new SqlParameter("@P_COMPNAME", COMPNAME);
                        objParams[3] = new SqlParameter("@P_FACULTYNO", FACULTY);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_IP_UPDATESELECTION", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateIPTraining-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetCompanyByIDNO(int IDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];

                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_IP_GET_COMPANYNAME", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.Getstud_forSelectionIPw-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public DataSet GetIndustryByBranch(int BranchNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_BRANCHNO", BranchNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_IP_INDUSTRY_REG_BY_BRANCH_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetIndustryByBranch->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;

                }
                public DataSet GetAllSelectedStud()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_GET_SELECTED_STUDENTLIST", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetAllSelectedStud-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;

                }
                public DataSet GetBranchByCompId(int Cid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COMPID", Cid);
                        ds = objSQLHelper.ExecuteDataSetSP("", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetBranchByCompId-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public DataSet GetSelectionByIDNO(int IDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];

                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_GET_SELECTED_BYIDNO", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.Getstud_forSelectionIPw-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;

                }
                public int UpdateIPLock(string IDNOs)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNOs);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_IP_UPDATELOCK", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateIPTraining-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetStudByFaculty(int SessionNo, int FacultyNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[1] = new SqlParameter("@P_FACULTYNO", FacultyNo);
                        //objParams[2] = new SqlParameter("@P_FLAG", flag);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_IP_GET_STUD_BYFACULTY", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.Getstud_forSelectionIPw-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public int InsertMoniterentry(int SessionNo, int Faculty, string IDNOs, string Progess, string Present, DateTime MDate, String ColCode)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[1] = new SqlParameter("@P_FACULTYNO", Faculty);
                        objParams[2] = new SqlParameter("@P_IDNO", IDNOs);
                        objParams[3] = new SqlParameter("@P_PROGRESS", Progess);
                        objParams[4] = new SqlParameter("@P_PRESENT", Present);
                        objParams[5] = new SqlParameter("@P_MONITORDATE", MDate);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[7] = new SqlParameter("@P_TMNO", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_TP_IP_TRAINING_MONITOR_INSERT", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.InsertMoniterentry-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetStudForEdit(int SessionNo, int FacultyNo, DateTime MDt)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[1] = new SqlParameter("@P_FACULTYNO", FacultyNo);
                        objParams[2] = new SqlParameter("@P_MDATE", MDt);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_IP_GET_STUD_BYFACULTY_FOR_EDIT", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetStudForEdit-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public int MoniterEntryExist(int SessionNo, int FacultyNo, DateTime MDt)
                {
                    int ret = 0;

                    try
                    {
                        SQLHelper objsqlhelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[1] = new SqlParameter("@P_FACULTYNO", FacultyNo);
                        objParams[2] = new SqlParameter("@P_MDATE", MDt);
                        ret = Convert.ToInt32(objsqlhelper.ExecuteScalarSP("PKG_ACAD_TP_IP_MONITERENTRY_EXIST", objParams));
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.MoniterEntryExist-> " + ex.ToString());
                    }
                    return ret;
                }


                public DataSet GetStudInfo(int BRANCHNO, int DEGREENO, int YEAR)
                {
                    DataSet ds = null;
                    //try
                    //{
                    SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                    SqlParameter[] objParams = null;
                    objParams = new SqlParameter[3];

                    objParams[0] = new SqlParameter("@P_BRANCHNO", BRANCHNO);
                    objParams[1] = new SqlParameter("@P_DEGREENO", DEGREENO);
                    objParams[2] = new SqlParameter("@P_SEMNO", YEAR);
                    //ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_REPORT_STUDLIST", objParams);

                    SqlConnection conn = new SqlConnection(_nitprm_constr);
                    //conn.ConnectionTimeout = 1000;
                    SqlCommand cmd = new SqlCommand("PKG_ACAD_TP_REPORT_STUDLIST", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2000;
                    int i;
                    for (i = 0; i < objParams.Length; i++)
                        cmd.Parameters.Add(objParams[i]);
                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        ds = new DataSet();
                        da.Fill(ds);
                    }

                //}
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetStudInfo-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetSelectedStudForIP(int SessionNo, int COMPID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[1] = new SqlParameter("@P_COMPID", COMPID);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_INPLANT_REPORT_SELECTEDLIST", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetSelectedStudForIP-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetMonitorStud(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COMPID", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_IP_GET_STU_FAC_BY_COMPID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetMonitorStud-> " + ex.ToString());
                    }
                    return ds;


                }
                public int UpdateMoniterAllotment(int Faculty, string IDNOs)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNOs);
                        objParams[1] = new SqlParameter("@P_FACULTYNO", Faculty);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_IP_UPDATEALLOTMENT", objParams, false) != null)

                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateMoniterAllotment-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetSortedStudlist(int COMPID, int SessionNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_COMPID", COMPID);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", SessionNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_IP_TRAINING_REGISTER_STUDLIST", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetSelectedStudForIP-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public DataSet GetStudentListToApprove(int BranchNo, int DegreeNo, char REGSTATUS)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_BRANCHNO", BranchNo);
                        objParams[1] = new SqlParameter("@P_REGSTATUS", REGSTATUS);
                        objParams[2] = new SqlParameter("@P_DEGREENO", DegreeNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_GET_REGSTUD_TOAPPROVE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetStudentListToApprove-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetStudentNo_Biodata(char StudType, int batchNo, int DegreeNo, int BranchNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_STUDTYPE", StudType);
                        objParams[1] = new SqlParameter("@P_BATCHNO", batchNo);
                        objParams[2] = new SqlParameter("@P_DEGREENO", DegreeNo);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", BranchNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_GET_STUD", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetStudentNo_Biodata-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateLetterGeneStud(string IDNOs)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNOS", IDNOs);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_TP_INPLANT_COMPANYS_GENE_LETTER_UPD", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateLetterGeneStud-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #region Alumni Data
                public int AddAlumniData(TPStudent objSTU)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add Block info 
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_NAME", objSTU.StudName);
                        objParams[1] = new SqlParameter("@P_COMPANYID", objSTU.CompId);
                        objParams[2] = new SqlParameter("@P_ENROLL", objSTU.EnrollmentNo);
                        objParams[3] = new SqlParameter("@P_CONTACT", objSTU.ContactNo);
                        objParams[4] = new SqlParameter("@P_COMPCONT", objSTU.CompContact);
                        objParams[5] = new SqlParameter("@P_BRANCHNO", objSTU.BranchNo);
                        objParams[6] = new SqlParameter("@P_DESIG", objSTU.Desig);
                        objParams[7] = new SqlParameter("@P_EMAIL", objSTU.EmailID);
                        objParams[8] = new SqlParameter("@P_ADDRESS", objSTU.LAddress);
                        objParams[9] = new SqlParameter("@P_PASSYEAR", objSTU.PassYear);
                        objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objSTU.CollegeCode);
                        objParams[11] = new SqlParameter("@P_ALUNO", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_ALUMNI_MASTER_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TPController.AddAlumniData-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateAlumniData(TPStudent objSTU)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //update Block info
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_STUDNAME", objSTU.StudName);
                        objParams[1] = new SqlParameter("@P_COMPANYID", objSTU.CompId);
                        objParams[2] = new SqlParameter("@P_ENROLLMENTNO", objSTU.EnrollmentNo);
                        objParams[3] = new SqlParameter("@P_CONTACTNO", objSTU.ContactNo);
                        objParams[4] = new SqlParameter("@P_COMPCONT", objSTU.CompContact);
                        objParams[5] = new SqlParameter("@P_BRANCHNO", objSTU.BranchNo);
                        objParams[6] = new SqlParameter("@P_DESIG", objSTU.Desig);
                        objParams[7] = new SqlParameter("@P_EMAILID", objSTU.EmailID);
                        objParams[8] = new SqlParameter("@P_ADDRESS", objSTU.LAddress);
                        objParams[9] = new SqlParameter("@P_PASSYEAR", objSTU.PassYear);
                        objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objSTU.CollegeCode);
                        objParams[11] = new SqlParameter("@P_ALUNO", objSTU.AluNo);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_ALUMNI_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TPController.UpdateAlumniData-> " + ex.ToString());
                    }

                    return retStatus;
                }
                #endregion


                public DataSet CompanyReportExcel(string Status)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        // objParams[0] = new SqlParameter("@P_STATUS", Status);
                        objParams[0] = new SqlParameter("@P_STATUS", Status);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_COMPANY_GET_BY_ALL_ExRpt", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.FillSchedule-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                //public DataSet GetPlacementStatuse(string fromdate, string todate)
                //{
                //    DataSet ds = null;
                //    //try
                //    //{
                //    SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //    SqlParameter[] objParams = null;
                //    objParams = new SqlParameter[2];
                //    objParams[0] = new SqlParameter("@P_SCHEDULE_FROM_DATE", fromdate);
                //    objParams[1] = new SqlParameter("@P_SCHEDULE_TO_DATE", todate);
                //    SqlConnection conn = new SqlConnection(_nitprm_constr);
                //    SqlCommand cmd = new SqlCommand("PKG_ACAD_TP_STATUS_OF_PLACEMENT", conn);
                //    cmd.CommandType = CommandType.StoredProcedure;
                //    cmd.CommandTimeout = 2000;
                //    int i;
                //    for (i = 0; i < objParams.Length; i++)
                //        cmd.Parameters.Add(objParams[i]);
                //    try
                //    {
                //        conn.Open();
                //        SqlDataAdapter da = new SqlDataAdapter();
                //        da.SelectCommand = cmd;
                //        ds = new DataSet();
                //        da.Fill(ds);
                //    }

                ////}
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetStudentDetaiReport-> " + ex.ToString());
                //    }
                //    finally
                //    {
                //        ds.Dispose();
                //    }
                //    return ds;
                //}

                //Added new Parameter degree on date 21/03/2016
                public DataSet GetPlacementStatuse(string fromdate, string todate, int degreeno)
                {
                    DataSet ds = null;
                    //try
                    //{
                    SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                    SqlParameter[] objParams = null;
                    objParams = new SqlParameter[3];
                    objParams[0] = new SqlParameter("@P_SCHEDULE_FROM_DATE", fromdate);
                    objParams[1] = new SqlParameter("@P_SCHEDULE_TO_DATE", todate);
                    objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                    SqlConnection conn = new SqlConnection(_nitprm_constr);
                    SqlCommand cmd = new SqlCommand("PKG_ACAD_TP_STATUS_OF_PLACEMENT", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2000;
                    int i;
                    for (i = 0; i < objParams.Length; i++)
                        cmd.Parameters.Add(objParams[i]);
                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        ds = new DataSet();
                        da.Fill(ds);
                    }

                //}
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetStudentDetaiReport-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                //new Added on date 03/02/2016 for final selected student

                public DataSet Getstud_FinalSelected(int ScheduleNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCHEDULENO", ScheduleNo);
                        //ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_STUDENT_SELECTION_LIST", objParams);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_STUDENT_SELECTION_LIST1", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.Getstud_forSelectionProcess-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                //Add new method on date 04/02/2016 

                public int UpdateStudentFinalSelectedStatus(string IDNOS, Int32 SCHEDULENO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNOS", IDNOS);
                        objParams[1] = new SqlParameter("@P_SCHEDULENO", SCHEDULENO);

                        objParams[2] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPDATE_SELECTED_STATUS", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateTPReg_INTVSELECT-> " + ex.ToString());

                    }
                    return retStatus;

                }

                //aDDED NEW METHOD ON DATE 14/05/2016
                public int UpdateTPDebaredStatus(string studids, string degreeno, string branchno, string semesterno, string deb_status, string ipAddress, string ua_no)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_STUDID", studids);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_DEB_STATUS", deb_status);
                        objParams[5] = new SqlParameter("@P_IPADDRESS", ipAddress);
                        objParams[6] = new SqlParameter("@P_UANO", ua_no);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_UPDATE_INS_TP_DEBARED_STATUS", objParams, false);
                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TPController.UpdateTPDebaredStatus-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public int CheckFileName(int scheduleNo, string CompanyName, string Filename, int CompanyId)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SCHEDULENO", scheduleNo);
                        objParams[1] = new SqlParameter("@P_COMPANYNAME", CompanyName);
                        objParams[2] = new SqlParameter("@P_FILENAME", Filename);
                        objParams[3] = new SqlParameter("@P_COMPANYID", CompanyId);

                        objParams[4] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        // = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_GET_FILE_DETAILS", objParams);
                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_GET_EXIST_FILENAME", objParams, true));
                    }
                    catch (Exception ex)
                    {
                        return retStatus;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetDoclistForDelete-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetTPOApprovedStudInfo(int BRANCHNO, int DEGREENO, int YEAR)
                {
                    DataSet ds = null;
                    //try
                    //{
                    SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                    SqlParameter[] objParams = null;
                    objParams = new SqlParameter[3];

                    objParams[0] = new SqlParameter("@P_BRANCHNO", BRANCHNO);
                    objParams[1] = new SqlParameter("@P_DEGREENO", DEGREENO);
                    objParams[2] = new SqlParameter("@P_SEMNO", YEAR);
                    //ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_REPORT_STUDLIST", objParams);

                    SqlConnection conn = new SqlConnection(_nitprm_constr);
                    //conn.ConnectionTimeout = 1000;
                    SqlCommand cmd = new SqlCommand("PKG_ACAD_TP_TPO_APPROVED_STUDENTLIST", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 2000;
                    int i;
                    for (i = 0; i < objParams.Length; i++)
                        cmd.Parameters.Add(objParams[i]);
                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        ds = new DataSet();
                        da.Fill(ds);
                    }

                //}
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetStudInfo-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                /// <summary>
                /// Added By Sunita on 11042018
                /// </summary>
                /// <returns></returns>
                public DataSet GetBranchByDegree(int degreeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DEGREE", degreeno);
                        ds = objSQLHelper.ExecuteDataSetSP("ACAD_TP_BRANCH_GET_DEGREE", objParams);
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

                public DataSet GetBranch()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];
                        // objParams[0] = new SqlParameter("@P_TYPE", BType);
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

                public int UpdateStudResume(TrainingPlacement objTP,int IDNO, string resumename,int Scheduleno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //UpdateFaculty Reference
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_FILENAME", resumename);
                        objParams[2] = new SqlParameter("@P_SCHEDULENO", Scheduleno);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_SP_UPD_STUDENT_RESUME", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentPhoto-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateStudConsentForm(int IDNO, string consentformname)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //UpdateFaculty Reference
                        objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_FILENAME", consentformname);


                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_UPD_STUDENT_CONSENT_FORM", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentPhoto-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int UploadOfferLetter(int IDNO, string resumename, int Sceduleno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //UpdateFaculty Reference
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_FILENAME", resumename);
                        objParams[2] = new SqlParameter("@P_SCHEDULENO", Sceduleno);


                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_UPDATE_OFFERLETTER", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentPhoto-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// Added By Dileep Kare on 13.07.2021
                /// </summary>
                /// <param name="IDNO"></param>
                /// <param name="CertificatName"></param>
                /// <param name="FilePath"></param>
                /// <returns></returns>
                public int UploadCetificate(int IDNO, string CertificatName, string FilePath)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //UpdateFaculty Reference
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_FILENAME", CertificatName);
                        objParams[2] = new SqlParameter("@P_FILEPATH", FilePath);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_TP_INSERT_OTHER_CERTIFICATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UploadCetificate-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// Added By Dileep Kare on 13.07.2021
                /// </summary>
                /// <param name="objSReg"></param>
                /// <param name="Idno"></param>
                /// <returns></returns>
                public int InsertOtherCertificationDetails(TPStudRegistration objSReg, int Idno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //UpdateFaculty Reference
                        objParams = new SqlParameter[12];

                        objParams[0] = new SqlParameter("@P_IDNO", Idno);
                        objParams[1] = new SqlParameter("@P_CERTIFICATE_NAME", objSReg.Certificate_Name);
                        objParams[2] = new SqlParameter("@P_INSTITUTE", objSReg.Institute);
                        objParams[3] = new SqlParameter("@P_DURATION", objSReg.Duration);
                        objParams[4] = new SqlParameter("@P_JOB_PROFILE", objSReg.Job_Profile);
                        objParams[5] = new SqlParameter("@P_COMPANY", objSReg.Company);
                        objParams[6] = new SqlParameter("@P_LOCATION", objSReg.Location);
                        objParams[7] = new SqlParameter("@P_WORK_DURATION", objSReg.Work_Duration);
                        objParams[8] = new SqlParameter("@P_JOB_DESCRIPTION", objSReg.Job_Description);
                        objParams[9] = new SqlParameter("@P_MAJOR", objSReg.Major);
                        objParams[10] = new SqlParameter("@P_MINOR", objSReg.Minor);
                        objParams[11] = new SqlParameter("@P_AGE", objSReg.Age);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_TP_INSERT_OTHER_CERTIFICATE_DETAILS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UploadCetificate-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #region TPCompany
                /// <summary>
                /// Added By Rita - 27062019
                /// </summary>
                /// <param name="page_link"></param>
                /// <returns></returns>
                public DataSet CheckActivityON(int page_link)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        //objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        //objParams[0] = new SqlParameter("@P_UA_TYPE", ua_type);
                        //objParams[1] = new SqlParameter("@P_UA_NO ", ua_no);
                        objParams[0] = new SqlParameter("@P_PAGE_LINK", page_link);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_CHECK_ACTIVITY_ON_OFF", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetONExams-> " + ex.ToString());
                    }
                    return ds;
                }
                //Added By Sunita ON DATE 17/07/2019
                public DataSet GetStudentDetailsForEmail(int degrreno, int branchno, int semesterno, int Registered)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_DEGREENO", degrreno);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_REGISTERED", Registered);
                        //ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_STUDENT_SELECTION_LIST", objParams);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_NOT_REG_STUDENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetStudentDetailsForEmail-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                /// <summary>
                /// Added by Swapnil - 09082019
                /// </summary>
                /// <param name="companyname"></param>
                /// <returns></returns>
                public int CheckCompanyAvalable(string companyname)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_COMPNAME", companyname);
                        objParams[1] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_CHECK_COMPANY_AVAILABILITY", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        return retStatus;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetBranch-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion

                public DataSet GetBranch(int degreeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
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
                public DataSet GetRoundFromSchedule(int scheduleno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCHEDULENO", scheduleno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ROUND_FROM_SCHEDULE_CREATED", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetRoundFromSchedule-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetStudForTPNotice(int newsid, int colid, int degreeid, int branchid, int semid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLhelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_NEWSID", newsid);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", colid);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeid);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchid);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semid);
                        ds = objSQLhelper.ExecuteDataSetSP("PKG_SHOW_STUD_LIST_FOR_TP_NOTICE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetStudForTPNotice-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by Nikhil Lambe on 10-11-2021 to get the students list as there is filldropdown method is present.
                /// </summary>
                /// <param name="status"></param>
                /// <param name="degreeno"></param>
                /// <param name="branchno"></param>
                /// <param name="semesterno"></param>
                /// <returns></returns>
                public DataSet Get_Student_TP_Status_List(string status, int degreeno, int branchno, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_STATUS", status);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUDENT_TP_STATUS_LIST_SANS_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.Get_Student_TP_Status_List-> " + ex.ToString());
                    }
                    return ds;
                }

                //public DataSet Get_Student_TP_Status_Pending_List( int degreeno, int branchno, int semesterno)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null;
                //        objParams = new SqlParameter[3];
                        
                //        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                //        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                //        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUDENT_TP_STATUS_PENDING_LIST_SANS_REPORT", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.Get_Student_TP_Status_List-> " + ex.ToString());
                //    }
                //    return ds;
                //}

                //added by rohit on 31-12-2021
                public DataSet GetStudentAllReport(string admbatch,int degreeno, int branchno,int Schemeno, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        //objParams[0] = new SqlParameter("@P_STATUS", status);
                        objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", Schemeno);
                        objParams[4] = new SqlParameter("@P_SEMNO", semesterno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_STU_REPORTS_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.Get_Student_TP_Status_List-> " + ex.ToString());
                    }
                    return ds;
                }
                //added by rohit 31-12-2021
                public DataSet StudentPlacementReportAll(string admbatch, int ScheduleNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                        objParams[1] = new SqlParameter("@P_SCHEDULENO", ScheduleNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_STUDENT_PLACEMENT_ALL_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.getWorkareabyId-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int AddJobType(string JobType, string ColCode, int status)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_JOBTYPE", JobType);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[2] = new SqlParameter("@P_STATUS", status);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_JOBTYPE_INSERT", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetJobType(int JobType)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_JOBNO", JobType);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_JOBTYPE_GETDATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                public int UpdateJobType(int JobNo, string JobType,int status)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_JOBNO", JobNo);
                        objParams[1] = new SqlParameter("@P_JOBType", JobType);
                        objParams[2] = new SqlParameter("@P_STATUS", status);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_JOBTYPE_UPDATE", objParams, true);

                        if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);                       
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //Currency Page methods
                public int AddCurrency(string Currency, string ColCode,int status)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_CURRENCY_NAME", Currency);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[2] = new SqlParameter("@P_STATUS", status);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_CURRENCY_INSERT", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetCurrency()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];
                       
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_CURRENCY_GETDATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }



                public int UpdateCurrency(int curno, string curname, int Status)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_CUR_NO", curno);
                        objParams[1] = new SqlParameter("@P_CUR_NAME", curname);
                        objParams[2] = new SqlParameter("@P_STATUS", Status);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_CURRENCY_UPDATE", objParams, true);

                        if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }



                public DataSet GetCurrency(int Curno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_CUR_NO", Curno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_CURRENCY_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                //job sector page methods
                public int AddJobSector(string JobSector, string ColCode, int status)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_JOBSector", JobSector);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[2] = new SqlParameter("@P_STATUS", status);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_JOBSECTOR_INSERT", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateJobSector(int jobsecno, string jobsector, int Status)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_JOBSEC_NO", jobsecno);
                        objParams[1] = new SqlParameter("@P_JOBSECTOR", jobsector);
                        objParams[2] = new SqlParameter("@P_STATUS", Status);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_JOBSECTOR_UPDATE", objParams, true);

                        if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public DataSet GetJobSector(int Jonsecno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_JOBSECNO", Jonsecno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_JOBSECTOR_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                public DataSet GetJobSectorLV()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_JOBSECTOR_GETDATA_LV", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                //carrer area
                public int AddCarrerArea(string carrerarea, string ColCode, int status)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_CARRERAREA", carrerarea);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[2] = new SqlParameter("@P_STATUS", status);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_CARRER_AREA_INSERT", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateCarrerArea(int carareano, string carrerarea, int Status)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_CARRER_AREA_NO", carareano);
                        objParams[1] = new SqlParameter("@P_CARRERAREA", carrerarea);
                        objParams[2] = new SqlParameter("@P_STATUS", Status);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_CARRERAREA_UPDATE", objParams, true);

                        if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public DataSet GetCarrerArea(int carareano)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_CARRER_AREA_NO", carareano);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_CARRERAREA_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                public DataSet GetCarrerAreaLV()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_CARRERAREA_GETDATA_LV", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                //Association for methods


                public int AddAssociationfor(string associationfor, string ColCode, int status)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_ASSOCIATION_FOR", associationfor);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[2] = new SqlParameter("@P_STATUS", status);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_ASSOCIATION_FOR_INSERT", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateAssociationFor(int associationno, string associationfor, int Status)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_ASSOCIATION_NO", associationno);
                        objParams[1] = new SqlParameter("@P_ASSOCIATION_FOR", associationfor);
                        objParams[2] = new SqlParameter("@P_STATUS", Status);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_ASSOCIATION_FOR_UPDATE", objParams, true);

                        if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public DataSet GetAssociationFor(int associationno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ASSOCIATION_NO", associationno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_ASSOCIATION_FOR_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                public DataSet GetAssociationforLV()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_ASSOCIATION_FOR_GETDATA_LV", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                //placement Status methods


                public int AddPlacementStatus(string Placestatus, string ColCode, int status)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_PLACED_STATUS", Placestatus);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[2] = new SqlParameter("@P_STATUS", status);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_PLACEMENT_STATUS_INSERT", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdatePlacementStatus(int statusno, string placestatus, int Status)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_STATUS_NO", statusno);
                        objParams[1] = new SqlParameter("@P_PLACED_STATUS", placestatus);
                        objParams[2] = new SqlParameter("@P_STATUS", Status);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_PLACEMENT_STATUS_UPDATE", objParams, true);

                        if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public DataSet GetPlacementStatus(int statusno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_STATUS_NO", statusno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_PLACEMENT_STATUS_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                public DataSet GetPlacementStatusLV()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_PLACEMENT_STATUS_GETDATA_LV", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                //job role methods 

                public int AddJobRole(string JobRole, string ColCode, int status, int jobno)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_JOB_ROLE", JobRole);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[2] = new SqlParameter("@P_STATUS", status);
                        objParams[3] = new SqlParameter("@P_JOBNO", jobno); 
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_JOB_ROLE_INSERT", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateJobRole(int roleno, string jobrole, int Status, int jobno)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_ROLENO", roleno);
                        objParams[1] = new SqlParameter("@P_JOB_ROLE", jobrole);
                        objParams[2] = new SqlParameter("@P_STATUS", Status);
                        objParams[3] = new SqlParameter("@P_JOBNO", jobno);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_JOB_ROLE_UPDATE", objParams, true);

                        if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public DataSet GetJobRole(int roleno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ROLENO", roleno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_JOB_ROLE_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                public DataSet GetJobRoleLV()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_JOB_ROLE_LV", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                //added by Rohit on 21-01-2022
                public int AddCompanyNew(TrainingPlacement objTP, string CarrerAreas, string Associtionfor, System.Web.UI.WebControls.FileUpload fuCollegeLogo)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //bool flag = true;
                        //string uploadPath = HttpContext.Current.Server.MapPath("~/UPLOAD_FILES/Logo/");

                        ////Upload the File
                        //if (!fuCollegeLogo.FileName.Equals(string.Empty))
                        //{
                        //    if (System.IO.File.Exists(uploadPath + objTP.Logo))
                        //    {
                        //        //lblStatus.Text = "File already exists. Please upload another file or rename and upload.";                            
                        //        return Convert.ToInt32(CustomStatus.FileExists);
                        //    }
                        //    else
                        //    {
                        //        string uploadFile = System.IO.Path.GetFileName(fuCollegeLogo.PostedFile.FileName.Replace(" ", "_"));
                        //        fuCollegeLogo.PostedFile.SaveAs(uploadPath + uploadFile);
                        //        flag = true;
                        //    }

                        //    if (flag == true)
                        //{
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_COMPNAME", objTP.COMPNAME);
                        objParams[1] = new SqlParameter("@P_COMPCODE", objTP.COMPCODE);
                        objParams[2] = new SqlParameter("@P_COMP_REG_NO", objTP.CompRegNo);
                        objParams[3] = new SqlParameter("@P_WEBSITE", objTP.WEBSITE);
                       // objParams[4] = new SqlParameter("@P_LOGO", objTP.Logo);

                        if (objTP.Logo == null)
                            objParams[4] = new SqlParameter("@P_LOGO", DBNull.Value);
                        else
                            objParams[4] = new SqlParameter("@P_LOGO", objTP.Logo);

                        objParams[4].SqlDbType = SqlDbType.Image;

                        objParams[5] = new SqlParameter("@P_JOBSECTOR", objTP.JobSector);
                        objParams[6] = new SqlParameter("@P_CAREERAREA", CarrerAreas);
                        objParams[7] = new SqlParameter("@P_ASSOCIATIONFOR", Associtionfor);  
                        objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objTP.COLLEGE_CODE);
                        //objParams[9] = new SqlParameter("@P_LocationName", objTP.LocationName); //--
                        //objParams[10] = new SqlParameter("@P_COMPADD", objTP.COMPADD); //--
                        //objParams[11] = new SqlParameter("@P_CITYNO", objTP.CITYNO); //--
                        //objParams[12] = new SqlParameter("@P_CONTPERSON", objTP.CONTPERSON); //--
                        //objParams[13] = new SqlParameter("@P_CONTDESIGNATION", objTP.CONTDESIGNATION); //--
                        //objParams[14] = new SqlParameter("@P_PHONENO", objTP.PHONENO); //--
                        //objParams[15] = new SqlParameter("@P_EMAILID", objTP.EMAILID); //--
                        objParams[9] = new SqlParameter("@P_COMPID", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_COMPANY_INSERT_NEW", objParams, true);
                        if (Convert.ToInt32(ret) == -1111)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            }
                        //     
                   // }
                       // }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddComapny->" + ex.ToString());
                    }
                    return retStatus;
                }


                public int UpdateCompanyNew(TrainingPlacement objTP, string CarrerAreas, string Associtionfor,System.Web.UI.WebControls.FileUpload fuCollegeLogo)
                {
                    int retStatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                                objParams = new SqlParameter[11];
                                objParams[0] = new SqlParameter("@P_COMPNAME", objTP.COMPNAME);
                                objParams[1] = new SqlParameter("@P_COMPCODE", objTP.COMPCODE);
                                objParams[2] = new SqlParameter("@P_COMP_REG_NO", objTP.CompRegNo);
                                objParams[3] = new SqlParameter("@P_WEBSITE", objTP.WEBSITE);
                                objParams[4] = new SqlParameter("@P_LOGO", objTP.Logo);
                                objParams[5] = new SqlParameter("@P_JOBSECTOR", objTP.JobSector);
                                objParams[6] = new SqlParameter("@P_CAREERAREA", CarrerAreas);
                                objParams[7] = new SqlParameter("@P_ASSOCIATIONFOR", Associtionfor);
                                objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objTP.COLLEGE_CODE);
                                objParams[9] = new SqlParameter("@P_COMPID", objTP.COMPID);
                                //objParams[10] = new SqlParameter("@P_LocationName", objTP.LocationName); //--
                                //objParams[11] = new SqlParameter("@P_COMPADD", objTP.COMPADD); //--
                                //objParams[12] = new SqlParameter("@P_CITYNO", objTP.CITYNO); //--
                                //objParams[13] = new SqlParameter("@P_CONTPERSON", objTP.CONTPERSON); //--
                                //objParams[14] = new SqlParameter("@P_CONTDESIGNATION", objTP.CONTDESIGNATION); //--
                                //objParams[15] = new SqlParameter("@P_PHONENO", objTP.PHONENO); //--
                                //objParams[16] = new SqlParameter("@P_EMAILID", objTP.EMAILID); //--
                                objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                                objParams[10].Direction = ParameterDirection.Output;
                            
                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_COMPANY_UPDATE_NEW", objParams, true);
                            if (Convert.ToInt32(ret) == 2)
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.Error);


                            //     
                        
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddComapny->" + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAddCompanyLV()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_ADD_COMPANY_BIND_LV", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                public DataSet GetIdAddCompany(int Compid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COMPID", Compid);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_ADD_COMPANY_NEW_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }



                public int AddCompanyLocation(TrainingPlacement objTP)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_COMPCATNO", objTP.COMPNAME);
                        objParams[1] = new SqlParameter("@P_COMPLOC", objTP.LocationName);
                        objParams[2] = new SqlParameter("@P_COMP_ADD", objTP.COMPADD);
                        objParams[3] = new SqlParameter("@P_COUNTRYNO", objTP.Country);
                        objParams[4] = new SqlParameter("@P_STATENO", objTP.State);
                        objParams[5] = new SqlParameter("@P_CITYNO", objTP.CITYNO);
                        objParams[6] = new SqlParameter("@P_CONTPER", objTP.CONTPERSON);
                        objParams[7] = new SqlParameter("@P_DESIGNATION", objTP.CONTDESIGNATION);
                        objParams[8] = new SqlParameter("@P_CONTACTNO", objTP.PHONENO);
                        objParams[9] = new SqlParameter("@P_EMAIL", objTP.EMAILID);
                        objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objTP.COLLEGE_CODE);
                        objParams[11] = new SqlParameter("@P_COMPID", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_COMPANY_LOCATION_INSERT_NEW", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        //     
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddComapny->" + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateCompanyLocation(TrainingPlacement objTP)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[12];
                       
                        objParams[0] = new SqlParameter("@P_COMPLOC", objTP.LocationName);
                        objParams[1] = new SqlParameter("@P_COMP_ADD", objTP.COMPADD);
                        objParams[2] = new SqlParameter("@P_COUNTRYNO", objTP.Country);
                        objParams[3] = new SqlParameter("@P_STATENO", objTP.State);
                        objParams[4] = new SqlParameter("@P_CITYNO", objTP.CITYNO);
                        objParams[5] = new SqlParameter("@P_CONTPER", objTP.CONTPERSON);
                        objParams[6] = new SqlParameter("@P_DESIGNATION", objTP.CONTDESIGNATION);
                        objParams[7] = new SqlParameter("@P_CONTACTNO", objTP.PHONENO);
                        objParams[8] = new SqlParameter("@P_EMAIL", objTP.EMAILID);
                        objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objTP.COLLEGE_CODE);
                        objParams[10] = new SqlParameter("@P_COMPLOID", objTP.COMPID);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_COMPANY_LOCATION_UPDATE_NEW", objParams, true);
                        if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                        //     
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddComapny->" + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAddCompanyLocationLV()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_ADD_COMPANY_LOCATION_BIND_LV", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                public DataSet GetIdAddCompanyLocation(int Compid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COMPID", Compid);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_ADD_COMPANY_LOCATION_NEW_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }


                //added by rohit on 01-02-2022 for job Announcement Page.

                public int AddJobAnnouncement(TrainingPlacement objTP, int org)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[23];
                        objParams[0] = new SqlParameter("@P_COMPID", objTP.COMPID);
                        objParams[1] = new SqlParameter("@P_JOBTYPE", objTP.JOBTYPE);
                        objParams[2] = new SqlParameter("@P_JOBROLE", objTP.JobRole);
                        objParams[3] = new SqlParameter("@P_PLACEMENTMODE", objTP.PlacementMode);
                        objParams[4] = new SqlParameter("@P_CITY", objTP.CITY);
                        objParams[5] = new SqlParameter("@P_ANYWHEREINSRILANKA", objTP.anywhereinsrilanka);
                        objParams[6] = new SqlParameter("@P_SCHFROMDATE", objTP.INTERVIEWFROM);
                        objParams[7] = new SqlParameter("@P_SCHTODATE", objTP.INTERVIEWTO);
                        objParams[8] = new SqlParameter("@P_VENUE", objTP.Venue);
                        objParams[9] = new SqlParameter("@P_SCHLASTDATE", objTP.LASTDATE);
                        objParams[10] = new SqlParameter("@P_JOBDISCRIPTION", objTP.JobDiscription);
                        objParams[11] = new SqlParameter("@P_ELIGIBILTY", objTP.CRITERIA);
                        objParams[12] = new SqlParameter("@P_AMOUNT", objTP.Amount);
                        objParams[13] = new SqlParameter("@P_MINAMOUNT", objTP.MinAmount);
                        objParams[14] = new SqlParameter("@P_MAXAMOUNT", objTP.MaxAmount);
                        objParams[15] = new SqlParameter("@P_ADD_DETAILS", objTP.SalDetails);
                        objParams[16] = new SqlParameter("@P_CURRENCY", objTP.Currency);
                        objParams[17] = new SqlParameter("@P_INTERVAL", objTP.Interval);
                        //objParams[18] = new SqlParameter("@P_ROUNDNO", objTP.SELECTNO);
                        objParams[18] = new SqlParameter("@P_ROUNDDISCRIPTON", objTP.RoundDiscription);
                       // objParams[19] = new SqlParameter("@P_FACULTY", objTP.Faculty);
                       // objParams[20] = new SqlParameter("@P_STUDYLEVEL", objTP.StudyLevel);
                       // objParams[21] = new SqlParameter("@P_PROGRAM", objTP.Program); //16-11-2022
                       // objParams[22] = new SqlParameter("@P_SEMNO", objTP.Semester);
                       // objParams[23] = new SqlParameter("@P_BRANCHNO", objTP.BRANCHNO); //16-11-2022
                       //objParams[24] = new SqlParameter("@P_DEGREENO", objTP.DEGREE);   //16-11-2022
                       objParams[19] = new SqlParameter("@P_TP_ROUND_TBL", objTP.TP_ROUND_TBL);   //07-12-2022
                       objParams[20] = new SqlParameter("@P_TP_ANNOUNCE_FOR_TBL", objTP.TP_ANNOUNCE_FOR_TBL);
                       objParams[21] = new SqlParameter("@P_OrgId", org);
                        objParams[22] = new SqlParameter("@P_SCHEDULENO", SqlDbType.Int);
                        objParams[22].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_TP_JOB_ANNOUNCEMENT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        //     
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddComapny->" + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdJobAnnouncement(TrainingPlacement objTP, int org)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[25];
                        objParams[0] = new SqlParameter("@P_COMPID", objTP.COMPID);
                        objParams[1] = new SqlParameter("@P_JOBTYPE", objTP.JOBTYPE);
                        objParams[2] = new SqlParameter("@P_JOBROLE", objTP.JobRole);
                        objParams[3] = new SqlParameter("@P_PLACEMENTMODE", objTP.PlacementMode);
                        objParams[4] = new SqlParameter("@P_CITY", objTP.CITY);
                        objParams[5] = new SqlParameter("@P_ANYWHEREINSRILANKA", objTP.anywhereinsrilanka);
                        objParams[6] = new SqlParameter("@P_SCHFROMDATE", objTP.INTERVIEWFROM);
                        objParams[7] = new SqlParameter("@P_SCHTODATE", objTP.INTERVIEWTO);
                        objParams[8] = new SqlParameter("@P_VENUE", objTP.Venue);
                        objParams[9] = new SqlParameter("@P_SCHLASTDATE", objTP.LASTDATE);
                        objParams[10] = new SqlParameter("@P_JOBDISCRIPTION", objTP.JobDiscription);
                        objParams[11] = new SqlParameter("@P_ELIGIBILTY", objTP.CRITERIA);
                        objParams[12] = new SqlParameter("@P_AMOUNT", objTP.Amount);
                        objParams[13] = new SqlParameter("@P_MINAMOUNT", objTP.MinAmount);
                        objParams[14] = new SqlParameter("@P_MAXAMOUNT", objTP.MaxAmount);
                        objParams[15] = new SqlParameter("@P_ADD_DETAILS", objTP.SalDetails);
                        objParams[16] = new SqlParameter("@P_CURRENCY", objTP.Currency);
                        objParams[17] = new SqlParameter("@P_INTERVAL", objTP.Interval);
                        //objParams[16] = new SqlParameter("@P_ROUNDNO", objTP.SELECTNO);
                        objParams[18] = new SqlParameter("@P_TP_ROUND_TBL", objTP.TP_ROUND_TBL);
                        objParams[19] = new SqlParameter("@P_ROUNDDISCRIPTON", objTP.RoundDiscription);
                        objParams[20] = new SqlParameter("@P_TP_ANNOUNCE_FOR_TBL", objTP.TP_ANNOUNCE_FOR_TBL);
                        //objParams[20] = new SqlParameter("@P_FACULTY", objTP.Faculty);
                        //objParams[21] = new SqlParameter("@P_STUDYLEVEL", objTP.StudyLevel);
                        //objParams[22] = new SqlParameter("@P_PROGRAM", objTP.Program);
                        //objParams[23] = new SqlParameter("@P_SEMNO", objTP.Semester);
                        objParams[21] = new SqlParameter("@P_OrgId", org);
                        objParams[22] = new SqlParameter("@P_SCHEDULENO", objTP.SCHEDULENO);
                        objParams[23] = new SqlParameter("@P_ACOMSCHNO", objTP.ACOMSCHNO);
                        objParams[24] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[24].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_TP_JOB_ANNOUCEMENT_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        if(Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        //     
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddComapny->" + ex.ToString());
                    }
                    return retStatus;
                }


                public DataSet JobAnnouncementBindLV()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_TP_BINDLISTVIEW_JOB_ANNOUNCEMENT_PAGE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }


                public DataSet GetIdEditJobAnnouncemnt(int ACOMSCHNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ACOMSCHNO", ACOMSCHNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_TP_EDIT_JOB_ANNOUNCEMENT_PAGE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                public DataSet JobProfileBindLV(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_BIND_LIST_VIEW_FOE_JOB_PROFILE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                public DataSet BindJobProfileCompDetails(int Compid, int Scheduleno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_COMPID", Compid);
                        objParams[1] = new SqlParameter("@P_SCHEDULENO", Scheduleno);
                        ds = objSQLHelper.ExecuteDataSetSP("ACD_TP_GET_COMPDETAILS_ON_JOB_PROFILE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }
                public DataSet InterviewSelectBindLV(int scheduleNo ,int Round)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];                        
                        objParams[0] = new SqlParameter("@P_SCHEDULENO", scheduleNo);
                        objParams[1] = new SqlParameter("@P_ROUND", Round);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_TP_SELECTIO_OFFER_LETTER_LISTVIEW_BIND", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }
                public int InsertRoundSelectionProcess(TrainingPlacement objTP, int Scheduleno, int RoundNo, string Roundname, string Ids, DateTime Date, string CollegeCode, int Status)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                          objParams = new SqlParameter[8];
                          objParams[0] = new SqlParameter("@P_SCHEDULENO", Scheduleno);
                          objParams[1] = new SqlParameter("@P_ROUNDNO", RoundNo);
                          objParams[2] = new SqlParameter("@P_ROUNDNAME", Roundname);
                          objParams[3] = new SqlParameter("@P_IDS",Ids);   
                          objParams[4] = new SqlParameter("@P_COLEGE_CODE", CollegeCode);
                          objParams[5] = new SqlParameter("@P_ROUND_DATE", Date);
                          objParams[6] = new SqlParameter("@P_STATUS", Status);
                          objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                          objParams[7].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_TP_INSERT_STUDENT_ROUND_SELECTION_PROCESS", objParams, true);
                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        if (Convert.ToInt32(ret) == -1001)
                            retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                        
                             
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddComapny->" + ex.ToString());
                    }
                    return retStatus;
                }




                public int UpdateOfferLetter(TrainingPlacement objTP, int Scheduleno, DateTime Date, string offerdescription,String Filename, int idno)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SCHEDULENO", Scheduleno);
                        objParams[1] = new SqlParameter("@P_DATE", Date);
                        objParams[2] = new SqlParameter("@P_OFFER_DESCRIPTION", offerdescription);
                        objParams[3] = new SqlParameter("@P_OFFER_LETTER", Filename);
                        objParams[4] = new SqlParameter("@P_IDNO", idno); 
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_TP_UPDATE_OFFER_LETTER", objParams, true);
                        if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        
                        //     
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddComapny->" + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateIntSelectStatus(TrainingPlacement objTP, int IDNO, int Intstatus, int SCHEDULENO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_INTSELECT", Intstatus);
                        objParams[2] = new SqlParameter("@P_SCHEDULENO", SCHEDULENO);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_TP_UPDATE_STATUS_AFTER_ROUNDS", objParams, true);
                        if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);



                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddComapny->" + ex.ToString());
                    }
                    return retStatus;
                }


                public DataSet selectionOfferLetterBindListView(int scheduleNo )
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCHEDULENO", scheduleNo);                       
                        ds = objSQLHelper.ExecuteDataSetSP("ACD_TP_SELECTION_OFFER_LETTER_BINDLISTVIEW", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                public DataSet ApplicationStatusBindLV(int scheduleNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCHEDULENO", scheduleNo);
                        ds = objSQLHelper.ExecuteDataSetSP("ACD_TP_SELECTION_OFFER_LETTER_BINDLISTVIEW", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                //public int UpdateStudResumeold(TrainingPlacement objTP, int IDNO, string Filename)
                //{
                //    int retStatus = 0;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null;
                //        objParams = new SqlParameter[3];
                //        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                //        objParams[1] = new SqlParameter("@P_RESUME", Filename);
                //        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //        objParams[2].Direction = ParameterDirection.Output;
                //        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_TP_UPDATE_STUDENT_RESUME", objParams, true);
                //        if (Convert.ToInt32(ret) == 2)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);



                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddComapny->" + ex.ToString());
                //    }
                //    return retStatus;
                //}


                public int UpdateStudConfirmStatus(TrainingPlacement objTP, int IDNO, int StudconfStatus, int scheduleno)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_STUDCONFIRM", StudconfStatus);
                        objParams[2] = new SqlParameter("@P_SCHEDULENO", scheduleno);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_TP_UPDATED_STUDENT_CONFIRMATION_SATUS", objParams, true);
                        if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);



                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddComapny->" + ex.ToString());
                    }
                    return retStatus;
                }




                public DataSet GetStudentOnApplStatusLV(int status, int scheduleno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_STUDCONFIRM", status);
                        objParams[1] = new SqlParameter("@P_SCHEDULENO", scheduleno);
                        ds = objSQLHelper.ExecuteDataSetSP("ACD_TP_BIND_LV_APPLICATION_STATUS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                public int AddRounds(string Round, string ColCode, int status)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_ROUNDNAME", Round);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[2] = new SqlParameter("@P_STATUS", status);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_ADD_ROUNDS", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateRounds(int Roundno, string Roundname, int Status)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SELECT_NO", Roundno);
                        objParams[1] = new SqlParameter("@P_ROUND_NAME", Roundname);
                        objParams[2] = new SqlParameter("@P_STATUS", Status);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_ROUNDS_UPDATE", objParams, true);

                        if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet EditRoundsByID(int Roundno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SELECT_NO", Roundno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_ROUND_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }
                public DataSet BindLVRounds()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_ROUNDS_BINDLV", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }


                //intervals

                public int AddIntervals(string Intervals, string ColCode, int status)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_INTERVALS", Intervals);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[2] = new SqlParameter("@P_STATUS", status);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_ADD_INTERVALS", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateIntervals(int Intno, string Intervals, int Status)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_INTNO", Intno);
                        objParams[1] = new SqlParameter("@P_INTERVALS", Intervals);
                        objParams[2] = new SqlParameter("@P_STATUS", Status);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_INTERVALS_UPDATE", objParams, true);

                        if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet EditIntervalsByID(int Intno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_INTNO", Intno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_INTERVALS_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }
                public DataSet BindLVIntervals()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_INTERVALS_BINDLV", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }



                public DataSet EmailTemplate()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("ACD_TP_SEND_TEMPLATE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                public DataSet reffemaildetails()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_SENDEMAIL_CREDENTIALS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }


                public DataSet SendMailByStudConfStatus(int scheduleno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCHEDULENO", scheduleno);
                        ds = objSQLHelper.ExecuteDataSetSP("ACD_TP_SEND_MAIL_AS_PER_STUDCONF_STATUS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }


                public int UpdateRemarkApplStatus(string Remark, int scheduleno, int idno)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_REMARK", Remark);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);
                        objParams[2] = new SqlParameter("@P_SCHEDULENO", scheduleno);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_INSERT_APPL_STATUS_REMARK", objParams, true);

                        if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet SendOfferLetterEmail(int Scheduleno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        
                        objParams[0] = new SqlParameter("@P_SCHEDULENO", Scheduleno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_SEND_OFFER_LETTER", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }
                //added by Ro-hit on 15-02-22 for Internship Registration Page

                public DataSet BindCompDetailsonintreg(int Compid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COMPID", Compid);
                        ds = objSQLHelper.ExecuteDataSetSP("ACD_TP_BIND_COMP_INFO_STUD_INTERNSHIP_REG", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }


                public int Addintregdetails(TrainingPlacement objTP, int compid, DateTime fromdate, DateTime todate,int days, string Sup_Email,int IDNO,int REGSTATUS)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //UpdateFaculty Reference
                        objParams = new SqlParameter[8];

                        objParams[0] = new SqlParameter("@P_COMPID", compid);
                        objParams[1] = new SqlParameter("@P_FROMDATE", fromdate);
                        objParams[2] = new SqlParameter("@P_TODATE", todate);
                        objParams[3] = new SqlParameter("@P_NOOFDAYS", days);
                        objParams[4] = new SqlParameter("@P_SUPEMAIL", Sup_Email);
                        objParams[5] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[6] = new SqlParameter("@P_INT_REG_STATUS", REGSTATUS);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                            object ret = objSQLHelper.ExecuteNonQuerySP("ACD_TP_ADD_INT_REG_DETAILS", objParams, true);
                            //retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 1234)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else {
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentPhoto-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public DataSet BindLVInternshipRegsitration()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("ACD_TP_BIND_LV_INT_REG", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                public DataSet BindLVIntRegApprovalPage(int P_UANO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UANO", P_UANO);  
                        ds = objSQLHelper.ExecuteDataSetSP("ACD_TP_BIND_LV_INT_REG_APPROVAL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddIntApprovedDetails(TrainingPlacement objTP, int IDNO, string Sup_name, string Designation, string Mobileno, string Email, int No_of_Hours, string Decription, string studLDescription,int appstatus)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //UpdateFaculty Reference
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_SUPERVISOR_NAME", Sup_name);
                        objParams[2] = new SqlParameter("@P_DESIGNATION", Designation);
                        objParams[3] = new SqlParameter("@P_CONTACT_NO", Mobileno);
                        objParams[4] = new SqlParameter("@P_OFFICIAL_EMAIL", Email);
                        objParams[5] = new SqlParameter("@P_WORK_NOOF_HOURS", No_of_Hours);
                        objParams[6] = new SqlParameter("@P_TASK_DESCRIPTION", Decription);
                        objParams[7] = new SqlParameter("@P_STUD_LEARNING_DESCRIPTION", studLDescription);
                        objParams[8] = new SqlParameter("@P_INT_APPROVAL_STATUS", appstatus);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_ADD_INT_APPROVAL_DETAILS", objParams, true);
                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_TP_ADD_INT_APPROVAL_DETAILS", objParams, false) != null)
                           // ret = Convert.ToInt32(CustomStatus.RecordSaved);
                        if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentPhoto-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int CreateUserforIntApproval(string UA_NAME, string UA_PWD, string UA_FULLNAME, string UA_EMAIL, int UA_EMPDEPTNO, int PARENT_USERTYPE, string UA_ACC)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_UA_NAME", UA_NAME);
                        objParams[1] = new SqlParameter("@P_UA_PWD", UA_PWD);                       
                        objParams[2] = new SqlParameter("@P_UA_FULLNAME", UA_FULLNAME);
                        objParams[3] = new SqlParameter("@P_UA_EMAIL", UA_EMAIL);
                        objParams[4] = new SqlParameter("@P_UA_EMPDEPTNO", UA_EMPDEPTNO);
                        objParams[5] = new SqlParameter("@P_PARENT_USERTYPE", PARENT_USERTYPE);
                        objParams[6] = new SqlParameter("@P_UA_ACC", UA_ACC);                      
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_TP_CREATE_USER_FOR_INT_SUPERVISOR", objParams, true);

                        if (Convert.ToInt32(ret) == 1)                           
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == 1234)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetUSERNOFORINTREG(string UA_NAME, string UA_PWD)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_UA_NAME", UA_NAME);
                        objParams[1] = new SqlParameter("@P_UA_PWD", UA_PWD);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_GET_UA_NO_FROM_USER_ACC", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                public int CreateUserforIntApproval(int UA_NO, int IDNO, int COMPID, DateTime fromdate, DateTime todate)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[1] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[2] = new SqlParameter("@P_COMPID", COMPID);
                        objParams[3] = new SqlParameter("@P_FROMDATE", fromdate);
                        objParams[4] = new SqlParameter("@P_TODATE", todate); 
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_UPDATE_UA_NO_INT_REG", objParams, true);

                        if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int InsertNewCompanyRequest(string CompanyName, string ShortName,string webSite, int Jobsector,int COMPREQUEST)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_COMPANYNAME", CompanyName);
                        objParams[1] = new SqlParameter("@P_SHORTNAME", ShortName);
                        objParams[2] = new SqlParameter("@P_WEBSITE", webSite);
                        objParams[3] = new SqlParameter("@P_JOBSECTOR", Jobsector);
                        objParams[4] = new SqlParameter("@P_COMPREQUEST", COMPREQUEST);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_INSERT_NEW_COMPANY_REQUEST", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet BindLVCompanyAproval(int REQSTATUS)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COMPREQUEST", REQSTATUS);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_BIND_LV_ON_APPROVED_COMPANY_PAGE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }


                public int InsertNewCompanyCompApprovalPage(TrainingPlacement objTP, string CarrerAreas, string Associtionfor, System.Web.UI.WebControls.FileUpload fuCollegeLogo, string Remark, int ReqStatus)
                {
                    int retStatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_COMP_REG_NO", objTP.CompRegNo);                     
                        objParams[1] = new SqlParameter("@P_LOGO", objTP.Logo);                       
                        objParams[2] = new SqlParameter("@P_CAREERAREA", CarrerAreas);
                        objParams[3] = new SqlParameter("@P_ASSOCIATIONFOR", Associtionfor);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", objTP.COLLEGE_CODE);
                        objParams[5] = new SqlParameter("@P_COMPID", objTP.COMPID);
                        objParams[6] = new SqlParameter("@P_REMARK", Remark);
                        objParams[7] = new SqlParameter("@P_REQSTATUS", ReqStatus);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_TP_UPDATE_ADD_COMPANY_BY_STUD", objParams, true);
                        if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddComapny->" + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet BindCompDetailsonCompApproval(int COMPID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COMPID", COMPID);

                        ds = objSQLHelper.ExecuteDataSetSP("ACD_TP_BIND_COMP_DETAILS_ON_COMP_APPROVAL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                public int UpdateCompanyRejectStatus( int COMPID, int ReqStatus)
                {
                    int retStatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_REQSTATUS", ReqStatus);
                        objParams[1] = new SqlParameter("@P_COMPID", COMPID);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_TP_REJECT_COMAPANY_APPROVAL", objParams, true);
                        if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddComapny->" + ex.ToString());
                    }
                    return retStatus;
                }
                public int AddStudentReporting(int COMPID, int IDNO, DateTime WorkDate, string WorkLocation, DateTime WORKHOURS,string ModuleName,string WorkDetails)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_COMPID", COMPID);
                        objParams[1] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[2] = new SqlParameter("@P_WORKDATE", WorkDate);
                        objParams[3] = new SqlParameter("@P_WORKING_LOCATION", WorkLocation);
                        objParams[4] = new SqlParameter("@P_WORK_HOURS", WORKHOURS);
                        objParams[5] = new SqlParameter("@P_MODULE_TASK", ModuleName);
                        objParams[6] = new SqlParameter("@P_WORK_DETAILS", WorkDetails);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_TP_ADD_STUD_REPORTING", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet BindLVReportingMonitoring(int IDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("ACD_TP_BIND_LV_DAILY_REPORTING_MONITORING", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }


                public DataSet BindWorkDetails(DateTime WORKDATE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_WORK_DATE", WORKDATE);
                        ds = objSQLHelper.ExecuteDataSetSP("ACD_TP_BIND_WORKDETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }


                public DataSet BindLVonViewReporting(DateTime FROMDATE ,DateTime TODATE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_FROMDATE", FROMDATE);
                        objParams[1] = new SqlParameter("@P_TODATE", TODATE);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_TP_VIEW_REPORTING_LV_BIND", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }


                public int UpdatedReportingStatusandRemark(DateTime WORKDATE, string REMARK,int AppStatus)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_WORKDATE", WORKDATE);
                        objParams[1] = new SqlParameter("@P_REMARK", REMARK);
                        objParams[2] = new SqlParameter("@P_APPROVED_STATUS", AppStatus);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_TP_UPDATE_REPORTING_STATUS_REMARK", objParams, true);
                        if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddComapny->" + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet BindLVforCompanyApprovedAllRequest()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_TP_SELECT_ALL_COMP_REQUEST", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }


                public DataSet finalperformancestudinfoBind(int IDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@IDNO", IDNO);                  
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_TP_FINAL_PERFORMACE_BIND_INFO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }
                public int AddStudFinalPerformanceEval(int IDNO, DateTime INTFROMDATE,DateTime INTTODATE ,int qualwork, int techability, int comm, int Leadership, string streght, string weakness, string HOW_EFFECTIVE_INT_PROG, string WAYS_TO_MAKE_INT_MEANINGFUL, string HOW_STUD_DEVELOPED_AFT_INT, string FEEDBACK_ABOUT_STUDENT, string OHTER_COMMENT_FOR_STUDENT, int OVERALL_STUD_PERFORMANCE, Decimal STUD_PERCENTAGE_RATING)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[17];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_INT_START_DATE", INTFROMDATE);
                        objParams[2] = new SqlParameter("@P_INT_END_DATE", INTTODATE);
                        objParams[3] = new SqlParameter("@P_QUALITY_OF_WORK" ,qualwork);
                        objParams[4] = new SqlParameter("@P_TECHINCAL_ABILITY" ,techability);
                        objParams[5] = new SqlParameter("@P_COMMUNICATION" ,comm);
                        objParams[6] = new SqlParameter("@P_LEADERSHIP" ,Leadership);
                        objParams[7] = new SqlParameter("@P_STUDENT_STREGHTS" ,streght);
                        objParams[8] = new SqlParameter("@P_STUDENT_WEAKNESS" ,weakness);
                        objParams[9] = new SqlParameter("@P_HOW_EFFECTIVE_INT_PROG" ,HOW_EFFECTIVE_INT_PROG);
                        objParams[10] = new SqlParameter("@P_WAYS_TO_MAKE_INT_MEANINGFUL",WAYS_TO_MAKE_INT_MEANINGFUL);
                        objParams[11] = new SqlParameter("@P_HOW_STUD_DEVELOPED_AFT_INT",HOW_STUD_DEVELOPED_AFT_INT);
                        objParams[12] = new SqlParameter("@P_FEEDBACK_ABOUT_STUDENT",FEEDBACK_ABOUT_STUDENT);		
                        objParams[13] = new SqlParameter("@P_OHTER_COMMENT_FOR_STUDENT",OHTER_COMMENT_FOR_STUDENT);
                        objParams[14] = new SqlParameter("@P_OVERALL_STUD_PERFORMANCE",OVERALL_STUD_PERFORMANCE	);
						objParams[15] = new SqlParameter("@P_STUD_PERCENTAGE_RATING",STUD_PERCENTAGE_RATING);
                        objParams[16] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[16].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_TP_INSERT_STUD_FINAL_PERFORMANCE", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet ValidateStudentforTPApply(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_BIND_LIST_VIEW_FOE_JOB_PROFILE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                //--------------start-Used In Application Status

                public DataSet GetEmailTamplateandUserDetails(string Emailid, string Mobileno, string username, int pageno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_EMAILID", Emailid);
                        objParams[1] = new SqlParameter("@P_MOBILENO", Mobileno);
                        objParams[2] = new SqlParameter("@P_USERNAME", username);
                        objParams[3] = new SqlParameter("@P_PAGENO", pageno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_EMAIL_AND_USER_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }

                //-------------end---

                //----------start-----------

                //Insert Skills
                public int AddSkillS(string Intervals, string ColCode, int status)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SKILLS", Intervals);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[2] = new SqlParameter("@P_STATUS", status);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_ADD_SKILLS", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }




                public DataSet BindLVSkills()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_SKILLS_BINDLV", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }


                public DataSet EditSkillsByID(int Intno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SKILNO", Intno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_SKILLS_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }


                public int UpdateSkills(int SKNO, string skills, int Status)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SKILNO", SKNO);
                        objParams[1] = new SqlParameter("@P_SKILLS", skills);
                        objParams[2] = new SqlParameter("@P_STATUS", Status);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_SKILLS_UPDATE", objParams, true);

                        if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }



                public int InsertProjectDetails(TPTraining objtptrn, int colcode, int idno, int orgs)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", colcode);
                        objParams[2] = new SqlParameter("@P_ORGANIZATIONID", orgs);
                        objParams[3] = new SqlParameter("@ACD_TP_PROJECT_TBL", objtptrn.TP_STUDENT_PROJECT_TBL);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_STUDENT_PROJECT_INSERT", objParams, true);

                      

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }



                public int InsertEducationDetails(TPTraining objtptrn, int colcode, int idno, int orgs)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", colcode);
                        objParams[2] = new SqlParameter("@P_ORGANIZATIONID", orgs);
                        objParams[3] = new SqlParameter("@ACD_TP_EDUCATION_TBL", objtptrn.TP_STUDENT_EDUCATION_TBL);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_STUDENT_EDUCATION_INSERT", objParams, true);



                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public int InsertCertificationDetails(TPTraining objtptrn, int colcode, int idno, int orgs)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", colcode);
                        objParams[2] = new SqlParameter("@P_ORGANIZATIONID", orgs);
                        objParams[3] = new SqlParameter("@ACD_TP_CERTIFICATION_TBL", objtptrn.TP_STUDENT_CERTIFICATION_TBL);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_STUDENT_CERTIFICATION_INSERT", objParams, true);



                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }


                //Get All Details Of Student Session wise History Added by DILEEP KARE 10/07/2021
                public DataSet GetSemesterHistoryDetails(int idno, int Semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", Semesterno);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GETALL_SEMESTERWISE_STUDDETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetSemesterHistoryDetails() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                //---------insert Career Profile Basic Information Details


                //public int InsStudentBasicDetails(int idno, string regno, string REGSTATUS, string STUDENTTYPE, int org, string confirmStatus)
                //{
                //    int retStatus = 0;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null;
                //        objParams = new SqlParameter[7];
                //        objParams[0] = new SqlParameter("@P_IDNO", idno);
                //        objParams[1] = new SqlParameter("@P_REGNO", regno);
                //        objParams[2] = new SqlParameter("@P_REGSTATUS", REGSTATUS);
                //        objParams[3] = new SqlParameter("@P_STUDENT_TYPE", STUDENTTYPE);
                //        objParams[4] = new SqlParameter("@P_OrganizationId", org);
                //        objParams[5] = new SqlParameter("@P_confirmStatus", confirmStatus);
                //        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //        objParams[6].Direction = ParameterDirection.Output;
                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_INS_STUDENT_BASIC_DETAILS", objParams, true);

                //        if (Convert.ToInt32(ret) == -99)
                //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //        else if (Convert.ToInt32(ret) == 2)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                //        else
                //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}


                //------------insert work experience Details

                //public int InsWorkExperience(int idno, int currentlyWorking, string WorkType, int SalaryType, decimal Salary, decimal Stipend, int currency, int cmpid, int JobSector, int JobType, int PositionType, string WorkSummery, string jobtitle, string location, DateTime StartDate, DateTime EndDate, double NrOfDays, string RelevantDocument, int org)
                //{
                //    int retStatus = 0;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = null;
                //        objParams = new SqlParameter[20];
                //        objParams[0] = new SqlParameter("@P_IDNO", idno);
                //        objParams[1] = new SqlParameter("@P_COMPID", cmpid);
                //        objParams[2] = new SqlParameter("@P_WorkType", WorkType);
                //        objParams[3] = new SqlParameter("@P_JOBTITLE", jobtitle);
                //        objParams[4] = new SqlParameter("@P_CMPLocation", location);
                //        objParams[5] = new SqlParameter("@P_JobSector", JobSector);
                //        objParams[6] = new SqlParameter("@P_JobType", JobType);
                //        objParams[7] = new SqlParameter("@P_PositionType", PositionType);
                //        objParams[8] = new SqlParameter("@P_WorkSummery", WorkSummery);
                //        objParams[9] = new SqlParameter("@P_StartDate", StartDate);
                //        objParams[10] = new SqlParameter("@P_EndDate", EndDate);
                //        objParams[11] = new SqlParameter("@P_Duration", NrOfDays);
                //        objParams[12] = new SqlParameter("@P_currentlyworking", currentlyWorking);
                //        objParams[13] = new SqlParameter("@P_SalaryType", SalaryType);
                //        objParams[14] = new SqlParameter("@P_Salary", Salary);
                //        objParams[15] = new SqlParameter("@P_Stipend", Stipend);
                //        objParams[16] = new SqlParameter("@P_currency", currency);
                //        objParams[17] = new SqlParameter("@P_RelevantDocument", RelevantDocument);
                //        objParams[18] = new SqlParameter("@P_OrganizationId", org);
                //        objParams[19] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //        objParams[19].Direction = ParameterDirection.Output;
                //        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_TP_CP_INSERT_STU_WORKEXPERIENCE", objParams, true);

                //        if (Convert.ToInt32(ret) == -99)
                //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //        else if (Convert.ToInt32(ret) == 2)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                //        else
                //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                //----------Job Profile

                public DataSet getjob_profile_data(string idno, int Scheduleno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SCHEDULENO", Scheduleno);
                        ds = objSQLHelper.ExecuteDataSetSP("ACD_TP_BIND_ROUND_DETAILS_ON_JOB_PROFILE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }

               

                //-----------end------

                //----start=-- level master


                public int Addlevels(string level, string ColCode, int status)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_LEVELS", level);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[2] = new SqlParameter("@P_STATUS", status);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_ADD_LEVEL", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public DataSet BindLVLEVELS()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_LEVELS_BINDLV", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                public DataSet EditlevelsByID(int LEVELNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_LEVELNO", LEVELNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_LEVELS_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                public int UpdateLevels(int LEVELNO, string LEVELS, int Status)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_LEVELNO", LEVELNO);
                        objParams[1] = new SqlParameter("@P_LEVELS", LEVELS);
                        objParams[2] = new SqlParameter("@P_STATUS", Status);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_LEVELS_UPDATE", objParams, true);

                        if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //-----end---level master

                //----start=-- Language master


                public int Addlanguage(string language, string ColCode, int status)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_LANGUAGE", language);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[2] = new SqlParameter("@P_STATUS", status);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_ADD_LANGUAGE", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public DataSet BindLanguage()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_Language_BINDLV", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                public DataSet EditlanguageByID(int LANGUAGENO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_LANGUAGENO", LANGUAGENO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_LANGUAGE_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                public int UpdateLanguas(int LANGUAGENO, string language, int Status)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_LANGUAGENO", LANGUAGENO);
                        objParams[1] = new SqlParameter("@P_LANGUAGE", language);
                        objParams[2] = new SqlParameter("@P_STATUS", Status);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_LANGUAGE_UPDATE", objParams, true);

                        if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //---------end

                //-----------STERT CAREER PROFILE-------------

                public DataSet BindStuWORKEXPERIENCE(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("ACD_TP_CP_BIND_STU_WORKEXPERIENCE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }


                public DataSet GetIdWorkExp(int WORKEXPNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_WORKEXPNO", WORKEXPNO);
                        ds = objSQLHelper.ExecuteDataSetSP("ACD_TP_CP_Get_STU_WORKEXPERIENCE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }


                public int UpdWorkExperience(int WORKEXPNO, int currentlyWorking, string WorkType, int SalaryType, decimal Salary, decimal Stipend, int currency, int cmpid, int JobSector, int JobType, int PositionType, string WorkSummery, string jobtitle, string location, DateTime StartDate, DateTime EndDate, double NrOfDays, string RelevantDocument)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[19];
                        objParams[0] = new SqlParameter("@P_WORKEXPNO", WORKEXPNO);
                        objParams[1] = new SqlParameter("@P_COMPID", cmpid);
                        objParams[2] = new SqlParameter("@P_WorkType", WorkType);
                        objParams[3] = new SqlParameter("@P_JOBTITLE", jobtitle);
                        objParams[4] = new SqlParameter("@P_CMPLocation", location);
                        objParams[5] = new SqlParameter("@P_JobSector", JobSector);
                        objParams[6] = new SqlParameter("@P_JobType", JobType);
                        objParams[7] = new SqlParameter("@P_PositionType", PositionType);
                        objParams[8] = new SqlParameter("@P_WorkSummery", WorkSummery);
                        objParams[9] = new SqlParameter("@P_StartDate", StartDate);
                        //objParams[10] = new SqlParameter("@P_EndDate", EndDate);
                        if (!EndDate.Equals(DateTime.MinValue))
                            objParams[10] = new SqlParameter("@P_EndDate", EndDate);
                        else
                            objParams[10] = new SqlParameter("@P_EndDate", DBNull.Value);
                        objParams[11] = new SqlParameter("@P_Duration", NrOfDays);
                        objParams[12] = new SqlParameter("@P_currentlyworking", currentlyWorking);
                        objParams[13] = new SqlParameter("@P_SalaryType", SalaryType);
                        objParams[14] = new SqlParameter("@P_Salary", Salary);
                        objParams[15] = new SqlParameter("@P_Stipend", Stipend);
                        objParams[16] = new SqlParameter("@P_currency", currency);
                        objParams[17] = new SqlParameter("@P_RelevantDocument", RelevantDocument);
                        objParams[18] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[18].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_TP_CP_UPDATE_STU_WORKEXPERIENCE", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //-----------END-------------

                //added by amol 

                // tab1
                public int AddUpdateTpmaster(TPTraining objSpoProj, int Org_id, int isactive)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_ID", objSpoProj.TP_ID);
                        objParams[1] = new SqlParameter("@P_ACADEMIC_YEAR_ID", objSpoProj.ACADEMIC_YEAR_ID);
                        objParams[2] = new SqlParameter("@P_DEPTNO", objSpoProj.DEPTNO);
                        objParams[3] = new SqlParameter("@P_NAME_OF_ORGNIZATION", objSpoProj.NAME_OF_ORGNIZATION);

                        objParams[4] = new SqlParameter("@P_NAME_OF_MOUS", objSpoProj.NAME_OF_MOUS);
                        objParams[5] = new SqlParameter("@P_BENEFITS_TO_STUDENTS_AND_STAFF", objSpoProj.BENEFITS_TO_STUDENTS_AND_STAFF);
                        objParams[6] = new SqlParameter("@P_COLLABORATION_YEAR", objSpoProj.COLLABORATION_YEAR);
                        objParams[7] = new SqlParameter("@P_COLLABORATION_EXPIRED_YEAR", objSpoProj.COLLABORATION_EXPIRED_YEAR);
                        objParams[8] = new SqlParameter("@P_CLAIM", objSpoProj.CLAIM);
                        objParams[9] = new SqlParameter("@P_OrganizationId", Org_id);
                        objParams[10] = new SqlParameter("@P_ISACTIVE", isactive);

                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_RCPIT_TIE_UPS_MOUS", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ee.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetallTPMasterdata()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_All_ACD_TP_RCPIT_TIE_UPS_MOUS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetTPMaster_data(int id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", id);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ACD_TP_RCPIT_TIE_UPS_MOUS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }

                //tab2 

                public int AddUpdateTpmastertabtwo(TPTraining objSpoProj, int Org_id, int isactive)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[17];
                        objParams[0] = new SqlParameter("@P_ID", objSpoProj.TAB2_ID);
                        objParams[1] = new SqlParameter("@P_ACADEMIC_YEAR_ID", objSpoProj.Acadyr_ID);
                        objParams[2] = new SqlParameter("@P_DEPTNO", objSpoProj.Tab2Dept_ID);
                        objParams[3] = new SqlParameter("@P_NAME_OF_STUDENT", objSpoProj.NAME_OF_stud);

                        objParams[4] = new SqlParameter("@P_NAME_OF_COMPANY_INSTITUTE", objSpoProj.Company);
                        objParams[5] = new SqlParameter("@P_COMPANY_ADDRESS", objSpoProj.Address);
                        objParams[6] = new SqlParameter("@P_CONTACT_PERSON", objSpoProj.ContactPerson);
                        objParams[7] = new SqlParameter("@P_EMAIL_ID", objSpoProj.tEmailID);
                        objParams[8] = new SqlParameter("@P_MOBILE_NO", objSpoProj.tMobileNo);
                        objParams[9] = new SqlParameter("@P_COMPANY_WEBSITE", objSpoProj.ttxtCompanyWebiste);
                        objParams[10] = new SqlParameter("@P_INTERNSHIP_DURATION", objSpoProj.ttxtInternshipDuration);
                        objParams[11] = new SqlParameter("@P_CLASS_NO", objSpoProj.tClass);
                        objParams[12] = new SqlParameter("@P_LEVEL_NO", objSpoProj.tLevel);
                        objParams[13] = new SqlParameter("@P_MODE_OF_INTERNSHIP", objSpoProj.tModeInternship);
                        objParams[14] = new SqlParameter("@P_ISACTIVE", isactive);
                        objParams[15] = new SqlParameter("@P_OrganizationId", Org_id);

                        objParams[16] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[16].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_RCPIT_INTERNSHIP", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ee.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetTPTab2Master_data(int id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", id);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ACD_TP_RCPIT_INTERNSHIP", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetallTab2Masterdata()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_ACD_TP_RCPIT_INTERNSHIP", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }

                //tab3


                public int AddUpdateTpmastertabthree(TPTraining objSpoProj, int Org_id, int isactive)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_ID", objSpoProj.TAB3_ID);
                        objParams[1] = new SqlParameter("@P_ACADEMIC_YEAR_ID", objSpoProj.Acadyr3_ID);
                        objParams[2] = new SqlParameter("@P_DEPTNO", objSpoProj.Tab3Dept_ID);
                        objParams[3] = new SqlParameter("@P_NAME_OF_ADVISOR", objSpoProj.NAME_OF_Advisor);
                        objParams[4] = new SqlParameter("@P_DESIGNATION_OF_ADVISOR", objSpoProj.Designation_of_Advisor);
                        objParams[5] = new SqlParameter("@P_ADVISOR_COMPANY_NAME", objSpoProj.Advisor_Company_Name);
                        objParams[6] = new SqlParameter("@P_LOCATION", objSpoProj.Location);
                        objParams[7] = new SqlParameter("@P_EMAIL_ID", objSpoProj.tEmailID3);
                        objParams[8] = new SqlParameter("@P_MOBILE_NO", objSpoProj.tMobileNo3);
                        objParams[9] = new SqlParameter("@P_EXPERT_DOMAIN", objSpoProj.Expertee_Domain);
                        objParams[10] = new SqlParameter("@P_CREDIT_CLAIM", objSpoProj.Credit_Claim);
                        objParams[11] = new SqlParameter("@P_MODE_OF_STAFF_COORDINATOR", objSpoProj.Staff_Coordinator);
                        objParams[12] = new SqlParameter("@P_ISACTIVE", isactive);
                        objParams[13] = new SqlParameter("@P_OrganizationId", Org_id);
                        // objParams[14] = new SqlParameter("@P_ISACTIVE", isactive);
                        // objParams[15] = new SqlParameter("@P_OrganizationId", Org_id);

                        objParams[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[14].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_RCPIT_INDUSTRY_ASSOCIATION", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ee.ToString());
                    }
                    return retStatus;
                }



                public DataSet GetTPTab3Master_data(int id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", id);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ACD_TP_RCPIT_INDUSTRY_ASSOCIATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetallTPMasterdatatab3()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_ACD_TP_RCPIT_INDUSTRY_ASSOCIATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }


                //tab4
                public int AddUpdateTpmastertabfour(TPTraining objSpoProj, int Org_id, int isactive)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[17];
                        objParams[0] = new SqlParameter("@P_ID", objSpoProj.TAB4_ID);
                        objParams[1] = new SqlParameter("@P_ACADEMIC_YEAR_ID", objSpoProj.Acadyr4_ID);
                        objParams[2] = new SqlParameter("@P_DEPTNO", objSpoProj.Tab4Dept_ID);
                        objParams[3] = new SqlParameter("@P_NAME_OF_EXPERT", objSpoProj.NameOfExpert);

                        objParams[4] = new SqlParameter("@P_DESIGNATION_OF_EXPERT", objSpoProj.DesignationExpert);
                        objParams[5] = new SqlParameter("@P_EXPERT_COMPANY_NAME", objSpoProj.ExpertCompanyName);
                        //objParams[6] = new SqlParameter("@P_LOCATION", "");
                        objParams[6] = new SqlParameter("@P_EMAIL_ID", objSpoProj.t4email);
                        objParams[7] = new SqlParameter("@P_MOBILE_NO", objSpoProj.t4mobile);
                        objParams[8] = new SqlParameter("@P_TOPIC_OF_INTERACTION", objSpoProj.TopicInteraction);
                        objParams[9] = new SqlParameter("@P_DATE_OF_INTERACTION", objSpoProj.DateInteraction);
                        objParams[10] = new SqlParameter("@P_MODE", objSpoProj.t4Mode);
                        objParams[11] = new SqlParameter("@P_CLASS_NO", objSpoProj.t4class);
                        objParams[12] = new SqlParameter("@P_NUMBER_OF_STUDENT_BENEFITTED", objSpoProj.StdBenefitted);
                        objParams[13] = new SqlParameter("@P_NAME_STAFF_COORDINATOR", objSpoProj.t4StaffCoordinator);
                        objParams[14] = new SqlParameter("@P_ISACTIVE", isactive);
                        objParams[15] = new SqlParameter("@P_OrganizationId", Org_id);

                        objParams[16] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[16].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_RCPIT_EXPERT_TALK", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ee.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetTPTab4Master_data(int id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", id);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ACD_TP_RCPIT_EXPERT_TALK", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetallTPMasterdatatab4()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_ACD_TP_RCPIT_EXPERT_TALK", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }

                //tab5

                public int AddUpdateTpmastertabfive(TPTraining objSpoProj, int Org_id, int isactive)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[18];
                        objParams[0] = new SqlParameter("@P_ID", objSpoProj.TAB5_ID);
                        objParams[1] = new SqlParameter("@P_ACADEMIC_YEAR_ID", objSpoProj.Acadyr5_ID);
                        objParams[2] = new SqlParameter("@P_DEPTNO", objSpoProj.Tab5Dept_ID);
                        objParams[3] = new SqlParameter("@P_NAME_OF_ALUMNI", objSpoProj.NameOfAlumni);

                        objParams[4] = new SqlParameter("@P_YEAR_OF_PASSOUT", objSpoProj.YearofPassout);
                        objParams[5] = new SqlParameter("@P_DESIGNATION_OF_ALUMNI", objSpoProj.DesignationofAlumni);
                        //objParams[6] = new SqlParameter("@P_LOCATION", "");
                        objParams[6] = new SqlParameter("@P_COMPANY_NAME", objSpoProj.CompanyName5);
                        objParams[7] = new SqlParameter("@P_EMAIL_ID", objSpoProj.t5email);
                        objParams[8] = new SqlParameter("@P_MOBILE_NO", objSpoProj.t5mobile);
                        objParams[9] = new SqlParameter("@P_DATE_OF_INTERACTION", objSpoProj.DateInteraction5);
                        objParams[10] = new SqlParameter("@P_TOPIC_OF_INTERACTION", objSpoProj.TopicInteraction5);
                        objParams[11] = new SqlParameter("@P_NUMBER_OF_STUDENT_BENEFITTED", objSpoProj.StdBenefitted5);
                        objParams[12] = new SqlParameter("@P_CLASS_NO", objSpoProj.t5class);
                        objParams[13] = new SqlParameter("@P_MODE_OF_INTERACTION", objSpoProj.t5Mode);
                        objParams[14] = new SqlParameter("@P_NAME_STAFF_COORDINATOR", objSpoProj.StaffCoordinator5);
                        objParams[15] = new SqlParameter("@P_ISACTIVE", isactive);
                        objParams[16] = new SqlParameter("@P_OrganizationId", Org_id);

                        objParams[17] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[17].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_RCPIT_ALUMNI_INTERACTION", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ee.ToString());
                    }
                    return retStatus;
                }


                public DataSet GetTPTab5Master_data(int id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", id);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ACD_TP_RCPIT_ALUMNI_INTERACTION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetallTPMasterdatatab5()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_ACD_TP_RCPIT_ALUMNI_INTERACTION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }


                //tab 6

                public int AddUpdateTpmastertabsix(TPTraining objSpoProj, int Org_id, int isactive)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_ID", objSpoProj.TAB6_ID);
                        objParams[1] = new SqlParameter("@P_ACADEMIC_YEAR_ID", objSpoProj.Acadyr6_ID);
                        objParams[2] = new SqlParameter("@P_DEPTNO", objSpoProj.Tab6Dept_ID);
                        objParams[3] = new SqlParameter("@P_NAME_OF_EVENT", objSpoProj.NameOfEvent);
                        objParams[4] = new SqlParameter("@P_NUMBER_OF_STUDENT_PARTICIPATED", objSpoProj.NumberofStudentsParticipated);
                        objParams[5] = new SqlParameter("@P_ACHIEVEMENT", objSpoProj.Achievement);
                        objParams[6] = new SqlParameter("@P_AWARD_AMOUNT", objSpoProj.AwardAmountinRs);
                        objParams[7] = new SqlParameter("@P_NUMBER_OF_STUDENT_PLACED", objSpoProj.NumberofStudentPlaced);
                        objParams[8] = new SqlParameter("@P_FINANCIAL_ASSISTANCE_FROM_INSTITUTE", objSpoProj.FinancialAssistancefromInstitute);
                        objParams[9] = new SqlParameter("@P_FINANCIAL_ASSISTANCE_FROM_INSTITUTE_AMOUNT", objSpoProj.FinancialAssistancefromInstituteinRs);
                        objParams[10] = new SqlParameter("@P_REMARK", objSpoProj.Remark);
                        objParams[11] = new SqlParameter("@P_NAME_STAFF_COORDINATOR", objSpoProj.StaffCoordinator6);
                        objParams[12] = new SqlParameter("@P_ISACTIVE", isactive);
                        objParams[13] = new SqlParameter("@P_OrganizationId", Org_id);

                        objParams[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[14].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_RCPIT_CONTEST_BASED_HIRING", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ee.ToString());
                    }
                    return retStatus;
                }


                public DataSet GetTPTab6Master_data(int id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", id);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ACD_TP_RCPIT_CONTEST_BASED_HIRING", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetallTPMasterdatatab6()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_ACD_TP_RCPIT_CONTEST_BASED_HIRING", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }




                //tab7

                public int AddUpdateTpmastertabseven(TPTraining objSpoProj, int Org_id, int isactive)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_ID", objSpoProj.TAB7_ID);
                        objParams[1] = new SqlParameter("@P_ACADEMIC_YEAR_ID", objSpoProj.Acadyr7_ID);
                        objParams[2] = new SqlParameter("@P_DEPTNO", objSpoProj.Tab7Dept_ID);
                        objParams[3] = new SqlParameter("@P_NAME_OF_STUDENT", objSpoProj.NameOfStudent);
                        objParams[4] = new SqlParameter("@P_FOREGIN_LANGUAGE", objSpoProj.ForeignLanguage);
                        objParams[5] = new SqlParameter("@P_CERTIFICATION", objSpoProj.Certification);
                        objParams[6] = new SqlParameter("@P_LEVEL_NO", objSpoProj.Level7);
                        objParams[7] = new SqlParameter("@P_LEVEL_OF_CERTIFICATION", objSpoProj.LevelofCertification);
                        objParams[8] = new SqlParameter("@P_NAME_STAFF_COORDINATOR", objSpoProj.StaffCoordinator7);
                        objParams[9] = new SqlParameter("@P_ISACTIVE", isactive);
                        objParams[10] = new SqlParameter("@P_OrganizationId", Org_id);

                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_RCPIT_FOREGIN_LANGUAGE", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ee.ToString());
                    }
                    return retStatus;
                }


                public DataSet GetTPTab7Master_data(int id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", id);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ACD_TP_RCPIT_FOREGIN_LANGUAGE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetallTPMasterdatatab7()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_ACD_TP_RCPIT_FOREGIN_LANGUAGE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }


                //tab8


                public int AddUpdateTpmastertabeight(TPTraining objSpoProj, int Org_id, int isactive)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[16];
                        objParams[0] = new SqlParameter("@P_ID", objSpoProj.TAB8_ID);
                        objParams[1] = new SqlParameter("@P_ACADEMIC_YEAR_ID", objSpoProj.Acadyr8_ID);
                        objParams[2] = new SqlParameter("@P_DEPTNO", objSpoProj.Tab8Dept_ID);
                        objParams[3] = new SqlParameter("@P_NAME_OF_COMPANY", objSpoProj.NameOfCompany8);
                        objParams[4] = new SqlParameter("@P_LOCATION", objSpoProj.Location8);
                        objParams[5] = new SqlParameter("@P_ADDRESS", objSpoProj.Address8);
                        objParams[6] = new SqlParameter("@P_EMAIL_ID", objSpoProj.EmailID8);
                        objParams[7] = new SqlParameter("@P_MOBILE_NO", objSpoProj.MobileNo8);
                        objParams[8] = new SqlParameter("@P_WEBSITE", objSpoProj.Website8);
                        objParams[9] = new SqlParameter("@P_DATE_OF_VISIT", objSpoProj.DateOfVisit8);
                        objParams[10] = new SqlParameter("@P_CLASS_NO", objSpoProj.Class8);
                        objParams[11] = new SqlParameter("@P_NUMBER_OF_STUDENT_VISITED", objSpoProj.NoofStudentVisited8);
                        objParams[12] = new SqlParameter("@P_NAME_STAFF_COORDINATOR", objSpoProj.StaffCoordinator8);
                        objParams[13] = new SqlParameter("@P_ISACTIVE", isactive);
                        objParams[14] = new SqlParameter("@P_OrganizationId", Org_id);

                        objParams[15] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[15].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_RCPIT_INDUSTRY_VISITS", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ee.ToString());
                    }
                    return retStatus;
                }


                public DataSet GetTPTab8Master_data(int id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", id);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ACD_TP_RCPIT_INDUSTRY_VISITS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetallTPMasterdatatab8()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_ACD_TP_RCPIT_INDUSTRY_VISITS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }

                //tab9

                public int AddUpdateTpmastertabnine(TPTraining objSpoProj, int Org_id, int isactive)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_ID", objSpoProj.TAB9_ID);
                        objParams[1] = new SqlParameter("@P_ACADEMIC_YEAR_ID", objSpoProj.Acadyr9_ID);
                        objParams[2] = new SqlParameter("@P_DEPTNO", objSpoProj.Tab9Dept_ID);
                        objParams[3] = new SqlParameter("@P_NAME_OF_COMPANY", objSpoProj.NameOfCompany9);
                        objParams[4] = new SqlParameter("@P_LOCATIONOF_COMPANY", objSpoProj.Location9);
                        objParams[5] = new SqlParameter("@P_DATE_OF_INTERACTION", objSpoProj.DateOfInteraction9);
                        objParams[6] = new SqlParameter("@P_MODE", objSpoProj.Mode9);
                        objParams[7] = new SqlParameter("@P_NUMBER_OF_STUDENT_BENEFITTED", objSpoProj.NoofStudentBenefitted9);
                        objParams[8] = new SqlParameter("@P_NAME_STAFF_COORDINATOR", objSpoProj.StaffCoordinator9);
                        objParams[9] = new SqlParameter("@P_CLAIM", objSpoProj.Claim9);
                        objParams[10] = new SqlParameter("@P_ISACTIVE", isactive);
                        objParams[11] = new SqlParameter("@P_OrganizationId", Org_id);

                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_RCPIT_PLACEMENT_TALK", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ee.ToString());
                    }
                    return retStatus;
                }


                public DataSet GetTPTab9Master_data(int id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", id);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ACD_TP_RCPIT_PLACEMENT_TALK", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetallTPMasterdatatab9()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_ACD_TP_RCPIT_PLACEMENT_TALK", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }

                //----start=-- Proficiency master


                public int AddProficiency(string Proficiency, string ColCode, int status)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_PROFICIENCY", Proficiency);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[2] = new SqlParameter("@P_STATUS", status);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_ADD_PROFICIENCY", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet BindProficiency()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_Proficiency_BINDLV", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                public DataSet EditProficiencyByID(int PROFNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PROFICIENCYNO", PROFNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_PROFICIENCY_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                public int UpdateProficiency(int PROFNO, string PROF, int Status)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_PROFICIENCYNO", PROFNO);
                        objParams[1] = new SqlParameter("@P_PROFICIENCY", PROF);
                        objParams[2] = new SqlParameter("@P_STATUS", Status);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_PROFICIENCY_UPDATE", objParams, true);

                        if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //-----start Work Area

                public int AddWorkArea(string Proficiency, string ColCode, int status)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_WORKAREA", Proficiency);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[2] = new SqlParameter("@P_STATUS", status);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_ADD_WORK_AREA", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public DataSet BindWorkArea()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_WORK_AREA_BINDLV", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                public DataSet EditWorkAreaByID(int WORKAREANO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_WORKAREANO", WORKAREANO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_WORKAREA_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }


                public int UpdateWorkArea(int WORKAREANO, string PROF, int Status)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_WORKAREANO", WORKAREANO);
                        objParams[1] = new SqlParameter("@P_WORKAREANAME", PROF);
                        objParams[2] = new SqlParameter("@P_STATUS", Status);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_WORKAREA_UPDATE", objParams, true);

                        if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //----end---
                //----start--Category Master

                public DataSet BindCategory()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_CATEGORY_BINDLV", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                public int AddCategory(string Category, string ColCode, int status)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_CATEGORYS", Category);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[2] = new SqlParameter("@P_STATUS", status);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_TP_ADD_CATEGORY", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet EditCategoryByID(int CATEGORYNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_CATEGORYSNO", CATEGORYNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_CATEGORY_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                public int UpdateCategory(int CATEGORYNO, string CATEGORY, int Status)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_CATEGORYSNO", CATEGORYNO);
                        objParams[1] = new SqlParameter("@P_CATEGORYS", CATEGORY);
                        objParams[2] = new SqlParameter("@P_STATUS", Status);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_CATEGORY_UPDATE", objParams, true);

                        if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //--------end
                //---- start---
                public DataSet BindExam()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_EXAM_BINDLV", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                public int AddExam(string Exam, string ColCode, int status)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_EXAM", Exam);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", ColCode);
                        objParams[2] = new SqlParameter("@P_STATUS", status);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_TP_ADD_EXAM", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet EditExamByID(int EXAM)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_EXAMNO", EXAM);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_TP_EXAM_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                public int UpdateExam(int EXAMNO, string EXAM, int Status)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_EXAMNO", EXAMNO);
                        objParams[1] = new SqlParameter("@P_EXAM", EXAM);
                        objParams[2] = new SqlParameter("@P_STATUS", Status);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_TP_EXAM_UPDATE", objParams, true);

                        if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //-----end---


                //---------insert Career Profile Basic Information Details


                public int InsStudentBasicDetails(int idno, string regno, string REGSTATUS, string STUDENTTYPE, int org, string confirmStatus)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_REGNO", regno);
                        objParams[2] = new SqlParameter("@P_REGSTATUS", REGSTATUS);
                        objParams[3] = new SqlParameter("@P_STUDENT_TYPE", STUDENTTYPE);
                        objParams[4] = new SqlParameter("@P_OrganizationId", org);
                        objParams[5] = new SqlParameter("@P_confirmStatus", confirmStatus);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_INS_STUDENT_BASIC_DETAILS", objParams, true);

                        //if (Convert.ToInt32(ret) == -99)
                        //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        //else if (Convert.ToInt32(ret) == 1)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                         if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                         else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }


                //------start------insert work experience Details

                public int InsWorkExperience(int idno, int currentlyWorking, string WorkType, int SalaryType, decimal Salary, decimal Stipend, int currency, int cmpid, int JobSector, int JobType, int PositionType, string WorkSummery, string jobtitle, string location, DateTime StartDate, DateTime EndDate, double NrOfDays, string RelevantDocument, int org)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[20];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_COMPID", cmpid);
                        objParams[2] = new SqlParameter("@P_WorkType", WorkType);
                        objParams[3] = new SqlParameter("@P_JOBTITLE", jobtitle);
                        objParams[4] = new SqlParameter("@P_CMPLocation", location);
                        objParams[5] = new SqlParameter("@P_JobSector", JobSector);
                        objParams[6] = new SqlParameter("@P_JobType", JobType);
                        objParams[7] = new SqlParameter("@P_PositionType", PositionType);
                        objParams[8] = new SqlParameter("@P_WorkSummery", WorkSummery);
                        objParams[9] = new SqlParameter("@P_StartDate", StartDate);
                        //objParams[10] = new SqlParameter("@P_EndDate", EndDate);
                        if (!EndDate.Equals(DateTime.MinValue))
                            objParams[10] = new SqlParameter("@P_EndDate", EndDate);
                        else
                            objParams[10] = new SqlParameter("@P_EndDate", DBNull.Value);
                        objParams[11] = new SqlParameter("@P_Duration", NrOfDays);
                        objParams[12] = new SqlParameter("@P_currentlyworking", currentlyWorking);
                        objParams[13] = new SqlParameter("@P_SalaryType", SalaryType);
                        objParams[14] = new SqlParameter("@P_Salary", Salary);
                        objParams[15] = new SqlParameter("@P_Stipend", Stipend);
                        objParams[16] = new SqlParameter("@P_currency", currency);
                        objParams[17] = new SqlParameter("@P_RelevantDocument", RelevantDocument);
                        objParams[18] = new SqlParameter("@P_OrganizationId", org);
                        objParams[19] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[19].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_TP_CP_INSERT_STU_WORKEXPERIENCE", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //-------end------insert work experience Details

                //-----start---Techinical Skill

                public int InsTechnicalSkill(TPTraining objTPT, int org, int id, int IDNO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_ID", id);
                        objParams[1] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[2] = new SqlParameter("@P_SKILNO", objTPT.SkillName);
                        objParams[3] = new SqlParameter("@P_PROFNO", objTPT.Proficiency);
                        objParams[4] = new SqlParameter("@P_UPLOAD_RELEVANT_DOCUMENT", objTPT.ReleventDocument);
                        objParams[5] = new SqlParameter("@P_OrganizationId", org);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TECHNICAL_SKILL", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }



                public DataSet BindTechnicalSkill(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_BIND_ACD_TP_TECHNICAL_SKILL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }


                public DataSet GetIdTechSkill(int WORKEXPNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ID", WORKEXPNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SHOW_ACD_TP_TECHNICAL_SKILL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }

                //-----end----Techinical Skill

                //---start-----------------
                #region Tab_4
                #region Insert Project Data
                public int InsUpdProject(TPTraining objTPT, int org, int id, int IDNO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_ID", id);
                        objParams[1] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[2] = new SqlParameter("@P_PROJECT_TITLE", objTPT.ProjectTitle);
                        objParams[3] = new SqlParameter("@P_PROJECT_DOMIAN", objTPT.ProjectDomian);
                        objParams[4] = new SqlParameter("@P_GUIDE_SUPERVISOR_NAME", objTPT.GuideSupervissorName);
                        objParams[5] = new SqlParameter("@P_START_DATE", objTPT.StartDate);
                        //  objParams[6] = new SqlParameter("@P_END_DATE", objTPT.EndDate);

                        if (!objTPT.EndDate.Equals(DateTime.MinValue))
                            objParams[6] = new SqlParameter("@P_END_DATE", objTPT.EndDate);
                        else
                            objParams[6] = new SqlParameter("@P_END_DATE", DBNull.Value);

                        objParams[7] = new SqlParameter("@P_CURRENTLY_WORKING", objTPT.CurrentlyWork);
                        objParams[8] = new SqlParameter("@P_DESCRIPTION", objTPT.Descripition);
                        objParams[9] = new SqlParameter("@P_UPLOAD_RELEVANT_DOCUMENT", objTPT.ReleventDocument1);
                        objParams[10] = new SqlParameter("@P_OrganizationId", org);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_PROJECTS", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion
                #region  Bind Project Details
                public DataSet BindProjects(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_BIND_ACD_TP_PROJECTS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }
                #endregion
                #region
                public DataSet GetIdProject(int Project)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ID", Project);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SHOW_ACD_TP_GET_PROJECT_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }
                #endregion
                #region Update Project Data
                public int UpdProjects(TPTraining objTPT, int org, int id, int IDNO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_ID", id);
                        objParams[1] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[2] = new SqlParameter("@P_PROJECT_TITLE", objTPT.ProjectTitle);
                        objParams[3] = new SqlParameter("@P_PROJECT_DOMIAN", objTPT.ProjectDomian);
                        objParams[4] = new SqlParameter("@P_GUIDE_SUPERVISOR_NAME", objTPT.GuideSupervissorName);
                        objParams[5] = new SqlParameter("@P_START_DATE", objTPT.StartDate);
                        // objParams[6] = new SqlParameter("@P_END_DATE", objTPT.EndDate);
                        if (!objTPT.EndDate.Equals(DateTime.MinValue))
                            objParams[6] = new SqlParameter("@P_END_DATE", objTPT.EndDate);
                        else
                            objParams[6] = new SqlParameter("@P_END_DATE", DBNull.Value);
                        objParams[7] = new SqlParameter("@P_CURRENTLY_WORKING", objTPT.CurrentlyWork);
                        objParams[8] = new SqlParameter("@P_DESCRIPTION", objTPT.Descripition);
                        objParams[9] = new SqlParameter("@P_UPLOAD_RELEVANT_DOCUMENT", objTPT.ReleventDocument1);
                        objParams[10] = new SqlParameter("@P_OrganizationId", org);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_PROJECTS", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion

                #endregion

                #region Tab_5
                #region Insert Certification Data
                public int InsUpdCertificate(TPTraining objTPT, int org, int id, int IDNO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_ID", id);
                        objParams[1] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[2] = new SqlParameter("@P_TITLE", objTPT.Title);
                        objParams[3] = new SqlParameter("@P_CERTIFIEDBY", objTPT.CertifiedBy);
                        objParams[4] = new SqlParameter("@P_GRADE", objTPT.Grade);
                        objParams[5] = new SqlParameter("@P_FROMDATE", objTPT.FromDate);
                        //  objParams[6] = new SqlParameter("@P_TODATE", objTPT.ToDate);
                        if (!objTPT.ToDate.Equals(DateTime.MinValue))
                            objParams[6] = new SqlParameter("@P_TODATE", objTPT.ToDate);
                        else
                            objParams[6] = new SqlParameter("@P_TODATE", DBNull.Value);
                        objParams[7] = new SqlParameter("@P_CURRENTLY_WORKING1", objTPT.CurrentlyWork1);
                        objParams[8] = new SqlParameter("@P_UPLOAD_RELEVANT_DOCUMENT1", objTPT.ReleventDocument2);
                        objParams[9] = new SqlParameter("@P_OrganizationId", org);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_CERTIFICATIONS", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion

                #region  Bind Certification Details
                public DataSet BindCertificat(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_BIND_ACD_TP_CERTIFICATIONS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }
                #endregion
                #region Show

                public DataSet GetIdCertification(int Certification)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ID", Certification);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SHOW_ACD_TP_GET_CERTIFICATIONS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }


                #endregion

                #region Update Certification Data
                public int UpdCertification(TPTraining objTPT, int org, int id, int IDNO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_ID", id);
                        objParams[1] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[2] = new SqlParameter("@P_TITLE", objTPT.Title);
                        objParams[3] = new SqlParameter("@P_CERTIFIEDBY", objTPT.CertifiedBy);
                        objParams[4] = new SqlParameter("@P_GRADE", objTPT.Grade);
                        objParams[5] = new SqlParameter("@P_FROMDATE", objTPT.FromDate);
                        // objParams[6] = new SqlParameter("@P_TODATE", objTPT.ToDate);
                        if (!objTPT.ToDate.Equals(DateTime.MinValue))
                            objParams[6] = new SqlParameter("@P_TODATE", objTPT.ToDate);
                        else
                            objParams[6] = new SqlParameter("@P_TODATE", DBNull.Value);
                        objParams[7] = new SqlParameter("@P_CURRENTLY_WORKING1", objTPT.CurrentlyWork1);
                        objParams[8] = new SqlParameter("@P_UPLOAD_RELEVANT_DOCUMENT1", objTPT.ReleventDocument2);
                        objParams[9] = new SqlParameter("@P_OrganizationId", org);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_CERTIFICATIONS", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion
                #endregion

                #region
                #region Insert Language Data
                public int InsLanguage(TPTraining objTPT, int org, int id, int IDNO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_ID", id);
                        objParams[1] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[2] = new SqlParameter("@P_LANGUAGE", objTPT.language);
                        objParams[3] = new SqlParameter("@P_PROFICIECNY", objTPT.Proficiency);
                        objParams[4] = new SqlParameter("@P_UPLOAD_RELEVANT_DOCUMENT1", objTPT.ReleventDocument3);
                        objParams[5] = new SqlParameter("@P_OrganizationId", org);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_LANGUAGE", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion

                #region  Bind Language Details
                public DataSet BindLAnguage(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_BIND_ACD_TP_LANGUAGE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }
                #endregion

                #region Edit Language

                public DataSet GetIdLanguage(int Language)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ID", Language);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SHOW_ACD_TP_GET_LANGUAGE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }


                #endregion

                #region Update Language Data
                public int UpdLanguage(TPTraining objTPT, int org, int id, int IDNO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_ID", id);
                        objParams[1] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[2] = new SqlParameter("@P_LANGUAGE", objTPT.language);
                        objParams[3] = new SqlParameter("@P_PROFICIECNY", objTPT.Proficiency);
                        objParams[4] = new SqlParameter("@P_UPLOAD_RELEVANT_DOCUMENT1", objTPT.ReleventDocument3);
                        objParams[5] = new SqlParameter("@P_OrganizationId", org);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_LANGUAGE", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion
                #endregion

                #region
                #region Insert Award & Recognition Data
                public int InsAWARDS_RECOGNITIONS(TPTraining objTPT, int org, int id, int IDNO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_ID", id);
                        objParams[1] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[2] = new SqlParameter("@P_AWARD_TITLE", objTPT.Award_Title);
                        objParams[3] = new SqlParameter("@P_DATE_OF_AWARD", objTPT.Date_of_Award);
                        objParams[4] = new SqlParameter("@P_GIVEN_BY", objTPT.Given_By);
                        objParams[5] = new SqlParameter("@P_LEVEL", objTPT.Level);
                        objParams[6] = new SqlParameter("@P_UPLOAD_RELEVANT_DOCUMENT1", objTPT.ReleventDocument4);
                        objParams[7] = new SqlParameter("@P_OrganizationId", org);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_AWARDS_RECOGNITIONS", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion

                #region  Bind Award Details
                public DataSet BindAWARD(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_BIND_ACD_TP_AWARD", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }
                #endregion

                #region Edit Award

                public DataSet GetIdAward(int AR_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ID", AR_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SHOW_ACD_TP_GET_AWARD", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }


                #endregion

                #region Update Award & Recognition Data
                public int UpdAWARDS_RECOGNITIONS(TPTraining objTPT, int org, int id, int IDNO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_ID", id);
                        objParams[1] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[2] = new SqlParameter("@P_AWARD_TITLE", objTPT.Award_Title);
                        objParams[3] = new SqlParameter("@P_DATE_OF_AWARD", objTPT.Date_of_Award);
                        objParams[4] = new SqlParameter("@P_GIVEN_BY", objTPT.Given_By);
                        objParams[5] = new SqlParameter("@P_LEVEL", objTPT.Level);
                        objParams[6] = new SqlParameter("@P_UPLOAD_RELEVANT_DOCUMENT1", objTPT.ReleventDocument4);
                        objParams[7] = new SqlParameter("@P_OrganizationId", org);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_AWARDS_RECOGNITIONS", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion


                #region Competitions Tab

                #region Insert Competitions Data
                public int InsCompetitions(TPTraining objTPT, int org, int id, int IDNO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_ID", id);
                        objParams[1] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[2] = new SqlParameter("@P_Competition_Title", objTPT.Competition_Title);
                        objParams[3] = new SqlParameter("@P_Organized_By", objTPT.Organized_By);
                        objParams[4] = new SqlParameter("@P_Level1", objTPT.Level1);
                        objParams[5] = new SqlParameter("@P_From_Date", objTPT.From_Date);
                        objParams[6] = new SqlParameter("@P_To_Date", objTPT.To_Date);
                        objParams[7] = new SqlParameter("@P_Project_Title", objTPT.Project_Title);
                        objParams[8] = new SqlParameter("@P_Participation_Status", objTPT.Participation_Status);
                        objParams[9] = new SqlParameter("@P_UPLOAD_RELEVANT_DOCUMENT1", objTPT.ReleventDocument4);
                        objParams[10] = new SqlParameter("@P_OrganizationId", org);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_COMPETITIONS", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion


                #region  Bind Award Details
                public DataSet BindCompetitions(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_BIND_ACD_TP_COMPETITIONS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }
                #endregion


                #region Edit Competition

                public DataSet GetIdCompetition(int CP_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ID", CP_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SHOW_ACD_TP_GET_COMPETITIONS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }


                #endregion

                #region Update COMPETITIONS Data
                public int UpdCOMPETITIONS(TPTraining objTPT, int org, int id, int IDNO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_ID", id);
                        objParams[1] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[2] = new SqlParameter("@P_Competition_Title", objTPT.Competition_Title);
                        objParams[3] = new SqlParameter("@P_Organized_By", objTPT.Organized_By);
                        objParams[4] = new SqlParameter("@P_Level1", objTPT.Level1);
                        objParams[5] = new SqlParameter("@P_From_Date", objTPT.From_Date);
                        objParams[6] = new SqlParameter("@P_To_Date", objTPT.To_Date);
                        objParams[7] = new SqlParameter("@P_Project_Title", objTPT.Project_Title);
                        objParams[8] = new SqlParameter("@P_Participation_Status", objTPT.Participation_Status);
                        objParams[9] = new SqlParameter("@P_UPLOAD_RELEVANT_DOCUMENT1", objTPT.ReleventDocument4);
                        objParams[10] = new SqlParameter("@P_OrganizationId", org);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_COMPETITIONS", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion
                #endregion


                #region Training And Workshop Tab

                #region Insert Training And Workshop Data
                public int InsTrainingAndWorkshop(TPTraining objTPT, int org, int id, int IDNO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_ID", id);
                        objParams[1] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[2] = new SqlParameter("@P_TRAINING_TITLE", objTPT.Training_Title);
                        objParams[3] = new SqlParameter("@P_ORGANIZED_BY1", objTPT.Organized_By1);
                        objParams[4] = new SqlParameter("@P_CATEGORY", objTPT.Category);
                        objParams[5] = new SqlParameter("@P_From_Date1", objTPT.From_Date1);
                        objParams[6] = new SqlParameter("@P_To_Date1", objTPT.To_Date1);
                        objParams[7] = new SqlParameter("@P_UPLOAD_RELEVANT_DOCUMENT1", objTPT.ReleventDocument6);
                        objParams[8] = new SqlParameter("@P_OrganizationId", org);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_TRAING_AND_WORKSHOP", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion

                #region  Bind Training And WorkShop Details
                public DataSet BindTrainings(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_BIND_ACD_TP_TRAINING_AND_WORKSHOP", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }
                #endregion

                #region Edit Training And Workshop

                public DataSet GetIdTraining(int TW_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ID", TW_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SHOW_ACD_TP_GET_TRAINING_AND_WORKSHOP", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }


                #endregion


                #region Update Training And Workshop Data
                public int UpdTrainingAndWorkshop(TPTraining objTPT, int org, int id, int IDNO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_ID", id);
                        objParams[1] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[2] = new SqlParameter("@P_TRAINING_TITLE", objTPT.Training_Title);
                        objParams[3] = new SqlParameter("@P_ORGANIZED_BY1", objTPT.Organized_By1);
                        objParams[4] = new SqlParameter("@P_CATEGORY", objTPT.Category);
                        objParams[5] = new SqlParameter("@P_From_Date1", objTPT.From_Date1);
                        objParams[6] = new SqlParameter("@P_To_Date1", objTPT.To_Date1);
                        objParams[7] = new SqlParameter("@P_UPLOAD_RELEVANT_DOCUMENT1", objTPT.ReleventDocument6);
                        objParams[8] = new SqlParameter("@P_OrganizationId", org);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_TRAING_AND_WORKSHOP", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion
                #endregion

                #region Test Scores Tab
                #region Insert Test Scores Data
                public int InsTestScores(TPTraining objTPT, int org, int id, int IDNO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_ID", id);
                        objParams[1] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[2] = new SqlParameter("@P_Exam", objTPT.Exam);
                        objParams[3] = new SqlParameter("@P_QualificationStatus", objTPT.QualificationStatus);
                        objParams[4] = new SqlParameter("@P_Year", objTPT.Year);
                        objParams[5] = new SqlParameter("@P_TestScore", objTPT.TestScore);
                        objParams[6] = new SqlParameter("@P_UPLOAD_RELEVANT_DOCUMENT1", objTPT.ReleventDocument7);
                        objParams[7] = new SqlParameter("@P_OrganizationId", org);
                        objParams[8] = new SqlParameter("@P_IsBlob", objTPT.IsBlob);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_TEST_SCORES", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion


                #region  Bind Test Scores Details
                public DataSet BindTestScores(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_BIND_ACD_TP_TEST_SCORES", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }
                #endregion

                #region Edit Test Scores

                public DataSet GetIdTestScores(int TS_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ID", TS_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SHOW_ACD_TP_GET_TEST_SCORES", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }


                #endregion

                #region Update Test Scores Data
                public int UpdTestScores(TPTraining objTPT, int org, int id, int IDNO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_ID", id);
                        objParams[1] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[2] = new SqlParameter("@P_Exam", objTPT.Exam);
                        objParams[3] = new SqlParameter("@P_QualificationStatus", objTPT.QualificationStatus);
                        objParams[4] = new SqlParameter("@P_Year", objTPT.Year);
                        objParams[5] = new SqlParameter("@P_TestScore", objTPT.TestScore);
                        objParams[6] = new SqlParameter("@P_UPLOAD_RELEVANT_DOCUMENT1", objTPT.ReleventDocument7);
                        objParams[7] = new SqlParameter("@P_OrganizationId", org);
                        objParams[8] = new SqlParameter("@P_IsBlob", objTPT.IsBlob);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_TEST_SCORES", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion

                #region Upload Resume
                public int UploadResume(TPTraining objTPT, int IDNO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_UPLOAD_RELEVANT_DOCUMENT1", objTPT.ReleventDocument8);
                        objParams[2] = new SqlParameter("@P_IsBlob", objTPT.IsBlob1);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_TP_UPLOAD_RESUME", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion

                #region  Bind Upload Resume Details
                public DataSet BindUploadResume(int IDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];

                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_BIND_ACD_TP_UPLOAD_RESUME", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }
                #endregion

                #endregion
                #endregion
                //--------end------


                #region TP COMPANIES DETAILS FORM

                #region Tab 0
                public int AddUpdateTpCompanies(TPTraining objSpoProj, int Org_id, int isactive)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_CMPID", objSpoProj.TP_CMPID);
                        objParams[1] = new SqlParameter("@P_COMPANY_ID", objSpoProj.COMPANY_ID);
                        objParams[2] = new SqlParameter("@P_SECTOR_ID", objSpoProj.SECTOR_ID);
                        objParams[3] = new SqlParameter("@P_INCORPORATION_STATUS", objSpoProj.INCORPORATION_STATUS);
                        objParams[4] = new SqlParameter("@P_ADDRESS", objSpoProj.ADDRESS);
                        objParams[5] = new SqlParameter("@P_WEBSITE", objSpoProj.WEBSITE);
                        objParams[6] = new SqlParameter("@P_MOBILE_NO", objSpoProj.MOBILE_NO);
                        objParams[7] = new SqlParameter("@P_MANAGER_CONTACT_PERSON_NAME", objSpoProj.MANAGER_CONTACT_PERSON_NAME);
                        objParams[8] = new SqlParameter("@P_EMAIL_ID", objSpoProj.EMAIL_ID);
                        objParams[9] = new SqlParameter("@P_OrganizationId", Org_id);
                        objParams[10] = new SqlParameter("@P_ISACTIVE", isactive);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_RCPIT_COMPAINIES", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ee.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetallTPCompanydata()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_All_ACD_TP_RCPIT_COMPANIES", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetTPCompanies_data(int TP_CMPID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", TP_CMPID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ACD_TP_RCPIT_COMPANIES", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                #region Tab 1


                public int AddUpdateTpDiscipline(TPTraining objSpoProj, int Org_id, int isactive)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_DISID", objSpoProj.DIS_ID);
                        objParams[1] = new SqlParameter("@P_DISCRIPONLINE", objSpoProj.DISCIPLINE);
                        objParams[2] = new SqlParameter("@P_LEVEL", objSpoProj.LEVEL);
                        objParams[3] = new SqlParameter("@P_YEAR_OF_INCEPTION", objSpoProj.YEAR_OF_INCEPTION);
                        objParams[4] = new SqlParameter("@P_NUMBER_OF_SUBSTREAM", objSpoProj.NUMBER_OF_SUBDTREAM);
                        objParams[5] = new SqlParameter("@P_NUMBER_OF_FACULTIES", objSpoProj.NUMBER_OF_FACULTIES);
                        objParams[6] = new SqlParameter("@P_ELIGIBLE_STUDENT_1", objSpoProj.ELIGIBLE_STUDENT_1);
                        objParams[7] = new SqlParameter("@P_ELIGIBLE_STUDENT_2", objSpoProj.ELIGIBLE_STUDENT_2);
                        objParams[8] = new SqlParameter("@P_OrganizationId", Org_id);
                        objParams[9] = new SqlParameter("@P_ISACTIVE", isactive);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_RCPIT_DISCRIPONLINE", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ee.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetallTPDisciplinedata()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_All_ACD_TP_RCPIT_DISCIPLINE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetTPDiscipline_data(int TP_DISID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", TP_DISID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ACD_TP_RCPIT_DISCIPLINE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                #region Tab 2
                public int AddUpdateTpCurriculum(TPTraining objSpoProj, int Org_id, int isactive)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[17];
                        objParams[0] = new SqlParameter("@P_CURRID", objSpoProj.TP_CUR_ID);
                        objParams[1] = new SqlParameter("@P_COMPANY_NAME", objSpoProj.COMPANY_NAME);
                        objParams[2] = new SqlParameter("@P_COMPANY_SACTOR", objSpoProj.COMPANY_SACTOR);
                        objParams[3] = new SqlParameter("@P_INCORPORATION_SACTOR", objSpoProj.INCORPORATION_SACTOR);
                        objParams[4] = new SqlParameter("@P_ADDRESS_CURR", objSpoProj.ADDRESS_CURR);
                        objParams[5] = new SqlParameter("@P_WEBSITE_CURR", objSpoProj.WEBSITE_CURR);
                        objParams[6] = new SqlParameter("@P_MOBILE_NO_CURR", objSpoProj.MOBILE_NO_CURR);
                        objParams[7] = new SqlParameter("@P_MANAGER_NAME_CURR", objSpoProj.MANAGER_NAME_CURR);
                        objParams[8] = new SqlParameter("@P_EMAIL_ID_CURR", objSpoProj.EMAIL_ID_CURR);
                        objParams[9] = new SqlParameter("@P_DISCIPLINE_CURR", objSpoProj.DISCIPLINE_CURR);
                        objParams[10] = new SqlParameter("@P_LEVEL_CURR", objSpoProj.LEVEL_CURR);
                        objParams[11] = new SqlParameter("@P_FROM_DATE_CURR", objSpoProj.FROM_DATE_CURR);
                        objParams[12] = new SqlParameter("@P_TO_DATE_CURR", objSpoProj.TO_DATE_CURR);
                        objParams[13] = new SqlParameter("@P_NO_OF_STUDENTS", objSpoProj.NO_OF_STUDENTS);
                        objParams[14] = new SqlParameter("@P_OrganizationId", Org_id);
                        objParams[15] = new SqlParameter("@P_ISACTIVE", isactive);
                        objParams[16] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[16].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_RCPIT_CURRICULUM", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ee.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetallTPCurriculumdata()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_All_ACD_TP_RCPIT_CURRICULUM_INPUT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetTPCurriculum_data(int CURR_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", CURR_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ACD_TP_RCPIT_CURRICULUM_INPUT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                #region Tab 3
                public int AddUpdateTpVisitingFaculties(TPTraining objSpoProj, int Org_id, int isactive)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[19];
                        objParams[0] = new SqlParameter("@P_VIS_ID", objSpoProj.TP_VIS_ID);
                        objParams[1] = new SqlParameter("@P_COMPANY_NO", objSpoProj.COMPANY_NAME_VIS);
                        objParams[2] = new SqlParameter("@P_SECTOR_NO", objSpoProj.COMPANY_SACTOR_VIS);
                        objParams[3] = new SqlParameter("@P_INSORPORATION_SECTOR", objSpoProj.INCORPORATION_SACTOR_VIS);
                        objParams[4] = new SqlParameter("@P_DESIGNATION", objSpoProj.DESIGNATION);
                        objParams[5] = new SqlParameter("@P_FIRST_NAME", objSpoProj.FIRST_NAME);
                        objParams[6] = new SqlParameter("@P_LAST_NAME", objSpoProj.LAST_NAME);
                        objParams[7] = new SqlParameter("@P_ADDRESS_VIS", objSpoProj.ADDRESS_VIS);
                        objParams[8] = new SqlParameter("@P_WEBSITE_VIS", objSpoProj.WEBSITE_VIS);
                        objParams[9] = new SqlParameter("@P_MOBILE_NO_VIS", objSpoProj.MOBILE_NO_VIS);
                        objParams[10] = new SqlParameter("@P_MANAGER_NAME_VIS", objSpoProj.MANAGER_NAME_VIS);
                        objParams[11] = new SqlParameter("@P_EMAIL_ID_VIS", objSpoProj.EMAIL_ID_VIS);
                        objParams[12] = new SqlParameter("@P_DISCIPLINE_VIS", objSpoProj.DISCIPLINE_VIS);
                        objParams[13] = new SqlParameter("@P_LEVEL_VIS", objSpoProj.LEVEL_VIS);
                        objParams[14] = new SqlParameter("@P_LECTURE_DATE_VIS", objSpoProj.LECTURE_DATE);
                        objParams[15] = new SqlParameter("@P_NO_OF_STUDENTS_VIS", objSpoProj.NO_OF_STUDENTS_VIS);
                        objParams[16] = new SqlParameter("@P_OrganizationId", Org_id);
                        objParams[17] = new SqlParameter("@P_ISACTIVE", isactive);
                        objParams[18] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[18].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_RCPIT_VISITING_FACULTIES", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ee.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetallTPVisitingdata()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_All_ACD_TP_RCPIT_VISITING_FACULTIES", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetTPVisiting_data(int VIS_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", VIS_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ACD_TP_RCPIT_VISITING_FACULTIES", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                #region TAB 4
                public int AddUpdateTpIndustrialVisit(TPTraining objSpoProj, int Org_id, int isactive)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[17];
                        objParams[0] = new SqlParameter("@P_IV_ID", objSpoProj.TP_IV_ID);
                        objParams[1] = new SqlParameter("@P_COMPANY_NO", objSpoProj.COMPANY_NAME_IV);
                        objParams[2] = new SqlParameter("@P_SECTOR_NO", objSpoProj.COMPANY_SACTOR_IV);
                        objParams[3] = new SqlParameter("@P_INSORPORATION_SECTOR", objSpoProj.INCORPORATION_SACTOR_IV);
                        objParams[4] = new SqlParameter("@P_ADDRESS_IV", objSpoProj.ADDRESS_IV);
                        objParams[5] = new SqlParameter("@P_WEBSITE_IV", objSpoProj.WEBSITE_IV);
                        objParams[6] = new SqlParameter("@P_MOBILE_NO_IV", objSpoProj.MOBILE_NO_IV);
                        objParams[7] = new SqlParameter("@P_MANAGER_NAME_IV", objSpoProj.MANAGER_NAME_IV);
                        objParams[8] = new SqlParameter("@P_EMAIL_ID_IV", objSpoProj.EMAIL_ID_IV);
                        objParams[9] = new SqlParameter("@P_DISCIPLINE_IV", objSpoProj.DISCIPLINE_IV);
                        objParams[10] = new SqlParameter("@P_LEVEL_IV", objSpoProj.LEVEL_IV);
                        objParams[11] = new SqlParameter("@P_FROM_DATE_IV", objSpoProj.FROM_DATE_IV);
                        objParams[12] = new SqlParameter("@P_TO_DATE_IV", objSpoProj.TO_DATE_IV);
                        objParams[13] = new SqlParameter("@P_NO_OF_STUDENTS_IV", objSpoProj.NO_OF_STUDENTS_IV);
                        objParams[14] = new SqlParameter("@P_OrganizationId", Org_id);
                        objParams[15] = new SqlParameter("@P_ISACTIVE", isactive);
                        objParams[16] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[16].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_RCPIT_INDUSTRIAL_VISITS", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ee.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetallTPIndVisit()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_All_ACD_TP_RCPIT_INDUSTRIAL_VISITS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetTPIndVisit_data(int VIS_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", VIS_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ACD_TP_RCPIT_INDUSTRIAL_VISIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                #region tab 5
                public int AddUpdateTpGuestLecture(TPTraining objSpoProj, int Org_id, int isactive)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[19];
                        objParams[0] = new SqlParameter("@P_GL_ID", objSpoProj.TP_GL_ID);
                        objParams[1] = new SqlParameter("@P_COMPANY_NO", objSpoProj.COMPANY_NAME_GUL);
                        objParams[2] = new SqlParameter("@P_SECTOR_NO", objSpoProj.COMPANY_SACTOR_GUL);
                        objParams[3] = new SqlParameter("@P_INSORPORATION_SECTOR_GUL", objSpoProj.INCORPORATION_SACTOR_GUL);
                        objParams[4] = new SqlParameter("@P_DESIGNATION_GUL", objSpoProj.DISCIPLINE_GUL);
                        objParams[5] = new SqlParameter("@P_FIRST_NAME_GUL", objSpoProj.FIRST_NAME_GUL);
                        objParams[6] = new SqlParameter("@P_LAST_NAME_GUL", objSpoProj.LAST_NAME_GUL);
                        objParams[7] = new SqlParameter("@P_ADDRESS_GUL", objSpoProj.ADDRESS_GUL);
                        objParams[8] = new SqlParameter("@P_WEBSITE_GUL", objSpoProj.WEBSITE_GUL);
                        objParams[9] = new SqlParameter("@P_MOBILE_NO_GUL", objSpoProj.MOBILE_NO_GUL);
                        objParams[10] = new SqlParameter("@P_MANAGER_NAME_GUL", objSpoProj.MANAGER_NAME_GUL);
                        objParams[11] = new SqlParameter("@P_EMAIL_ID_GUL", objSpoProj.EMAIL_ID_GUL);
                        objParams[12] = new SqlParameter("@P_DISCIPLINE_GUL", objSpoProj.DISCIPLINE_GUL);
                        objParams[13] = new SqlParameter("@P_LEVEL_GUL", objSpoProj.LEVEL_GUL);
                        objParams[14] = new SqlParameter("@P_LECTURE_DATE_GUL", objSpoProj.LECTURE_DATE_GUL);
                        objParams[15] = new SqlParameter("@P_NO_OF_STUDENTS_GUL", objSpoProj.NO_OF_STUDENTS_IV);
                        objParams[16] = new SqlParameter("@P_OrganizationId", Org_id);
                        objParams[17] = new SqlParameter("@P_ISACTIVE", isactive);
                        objParams[18] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[18].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_RCPIT_GUEST_LECTURE", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ee.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetallTPGuestLect()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_All_ACD_TP_RCPIT_GUEST_LECTURE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetTPGUESTLECTUR_data(int TP_GL_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", TP_GL_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ACD_TP_RCPIT_GUEST_LECTURE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                #region tab 6
                public int AddUpdateTpFacultyLinkIndustry(TPTraining objSpoProj, int Org_id, int isactive)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[14];
                        objParams[0] = new SqlParameter("@P_FLI_ID", objSpoProj.TP_FLI_ID);
                        objParams[1] = new SqlParameter("@P_EXTERNAL_FACULTY", objSpoProj.EXTERNAL_FACULTY_FLI);
                        objParams[2] = new SqlParameter("@P_FACULTY_ID", objSpoProj.FACULTY_ID_FLI);
                        objParams[3] = new SqlParameter("@P_COMPANY_NO_FLI", objSpoProj.COMPANY_NAME_FLI);
                        objParams[4] = new SqlParameter("@P_ADDRESS_FLI", objSpoProj.ADDRESS_FLI);
                        objParams[5] = new SqlParameter("@P_WEBSITE_FLI", objSpoProj.WEBSITE_FLI);
                        objParams[6] = new SqlParameter("@P_MOBILE_NO_FLI", objSpoProj.MOBILE_NO_FLI);
                        objParams[7] = new SqlParameter("@P_MANAGER_NAME_FLI", objSpoProj.MANAGER_NAME_FLI);
                        objParams[8] = new SqlParameter("@P_EMAIL_ID_FLI", objSpoProj.EMAIL_ID_FLI);
                        objParams[9] = new SqlParameter("@P_DISCIPLINE_FLI", objSpoProj.DISCIPLINE_FLI);
                        objParams[10] = new SqlParameter("@P_LEVEL_FLI", objSpoProj.LEVEL_FLI);
                        objParams[11] = new SqlParameter("@P_OrganizationId", Org_id);
                        objParams[12] = new SqlParameter("@P_ISACTIVE", isactive);
                        objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[13].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_RCPIT_FACULTY_LINK_INDUSTRY", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ee.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetallTPfacultylink()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_All_ACD_TP_RCPIT_FACULTY_LINK_INDUSTRY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetTPfacultylink_data(int TP_GL_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", TP_GL_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ACD_TP_RCPIT_FACULTY_LINK_INDUSTRY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #region tab 7
                public int AddUpdateTpFacultyProvTranToIndustry(TPTraining objSpoProj, int Org_id, int isactive)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[16];
                        objParams[0] = new SqlParameter("@P_FPTI_ID", objSpoProj.TP_FPTI_ID);
                        objParams[1] = new SqlParameter("@P_COMPANY_NO_FPTI", objSpoProj.COMPANY_NAME_FPTI);
                        objParams[2] = new SqlParameter("@P_SECTOR_NO_FPTI", objSpoProj.SECTOR_NAME_FPTI);
                        objParams[3] = new SqlParameter("@P_INCORPORATION_STATUS_FPTI", objSpoProj.INCORPORATION_STATUS_FPTI);
                        objParams[4] = new SqlParameter("@P_FACULTY_ID_FPTI", objSpoProj.FACULTY_ID_FPIT);
                        objParams[5] = new SqlParameter("@P_ADDRESS_FPTI", objSpoProj.ADDRESS_FPTI);
                        objParams[6] = new SqlParameter("@P_WEBSITE_FPTI", objSpoProj.WEBSITE_FPTI);
                        objParams[7] = new SqlParameter("@P_MOBILE_NO_FPTI", objSpoProj.MOBILE_NO_FPTI);
                        objParams[8] = new SqlParameter("@P_MANAGER_NAME_FPTI", objSpoProj.MANAGER_NAME_FPTI);
                        objParams[9] = new SqlParameter("@P_EMAIL_ID_FPTI", objSpoProj.EMAIL_ID_FPTI);
                        objParams[10] = new SqlParameter("@P_DISCIPLINE_FPTI", objSpoProj.DISCIPLINE_FPTI);
                        objParams[11] = new SqlParameter("@P_LEVEL_FPTI", objSpoProj.LEVEL_FPTI);
                        objParams[12] = new SqlParameter("@P_DATE_OF_LECTURE", objSpoProj.DATE_OF_LECTURE_FPTI);
                        objParams[13] = new SqlParameter("@P_OrganizationId", Org_id);
                        objParams[14] = new SqlParameter("@P_ISACTIVE", isactive);
                        objParams[15] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[15].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_RCPIT_FACULTY_PROVIDING_TRAINING_INDUSTRY", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ee.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetallTPFPTI()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_All_ACD_TP_RCPIT_FACULTY_PROVIDING_TRAINING_INDUSTRY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetTPFPTI_data(int FPTI_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", FPTI_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ACD_TP_RCPIT_FACULTY_PROVIDING_TRAINING_INDUSTRY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                #region tab 8
                public int AddUpdateTpFacultyOnboardOfIndustry(TPTraining objSpoProj, int Org_id, int isactive)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[17];
                        objParams[0] = new SqlParameter("@P_FOOI_ID", objSpoProj.TP_FOOI_ID);
                        objParams[1] = new SqlParameter("@P_COMPANY_NO_FOOI", objSpoProj.COMPANY_NAME_FOOI);
                        objParams[2] = new SqlParameter("@P_SECTOR_NO_FOOI", objSpoProj.SECTOR_NAME_FOOI);
                        objParams[3] = new SqlParameter("@P_INCORPORATION_STATUS_FOOI", objSpoProj.INCORPORATION_STATUS_FOOI);
                        objParams[4] = new SqlParameter("@P_TYPE_OF_BOARD_FOOI", objSpoProj.TYPE_OF_BOARD_FOOT);
                        objParams[5] = new SqlParameter("@P_FACULTY_ID_FOOI", objSpoProj.FACULTY_ID_FOOT);
                        objParams[6] = new SqlParameter("@P_ADDRESS_FOOI", objSpoProj.ADDRESS_FOOI);
                        objParams[7] = new SqlParameter("@P_WEBSITE_FOOI", objSpoProj.WEBSITE_FOOI);
                        objParams[8] = new SqlParameter("@P_MOBILE_NO_FOOI", objSpoProj.MOBILE_NO_FOOI);
                        objParams[9] = new SqlParameter("@P_MANAGER_NAME_FOOI", objSpoProj.MANAGER_NAME_FOOI);
                        objParams[10] = new SqlParameter("@P_EMAIL_ID_FOOI", objSpoProj.EMAIL_ID_FOOI);
                        objParams[11] = new SqlParameter("@P_DISCIPLINE_FOOI", objSpoProj.DISCIPLINE_FOOI);
                        objParams[12] = new SqlParameter("@P_LEVEL_FOOI", objSpoProj.LEVEL_FOOI);
                        objParams[13] = new SqlParameter("@P_MEMBER_FOOI", objSpoProj.MEMBER_FOOI);
                        objParams[14] = new SqlParameter("@P_OrganizationId", Org_id);
                        objParams[15] = new SqlParameter("@P_ISACTIVE", isactive);
                        objParams[16] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[16].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_RCPIT_FACULTY_ONBOARD_INDUSTRY", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ee.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetallTPFOOI()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_All_ACD_TP_RCPIT_FACULTY_ONBOARD_INDUSTRY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetTPFOOI_data(int TP_FOOI_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", TP_FOOI_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ACD_TP_RCPIT_FACULTY_ONBOARD_INDUSTRY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                #region tab 9
                public int AddUpdateTpExecutiveProgramAttendIndustry(TPTraining objSpoProj, int Org_id, int isactive)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[14];
                        objParams[0] = new SqlParameter("@P_EPAI_ID", objSpoProj.TP_EPAI_ID);
                        objParams[1] = new SqlParameter("@P_COMPANY_NO_EPAI", objSpoProj.COMPANY_NAME_EPAI);
                        objParams[2] = new SqlParameter("@P_SECTOR_NO_EPAI", objSpoProj.SECTOR_NAME_EPAI);
                        objParams[3] = new SqlParameter("@P_INCORPORATION_STATUS_EPAI", objSpoProj.INCORPORATION_STATUS_EPAI);
                        objParams[4] = new SqlParameter("@P_FACULTY_ID_EPAI", objSpoProj.FACULTY_ID_EPAI);
                        objParams[5] = new SqlParameter("@P_DISCIPLINE_EPAI", objSpoProj.DISCIPLINE_EPAI);
                        objParams[6] = new SqlParameter("@P_LEVEL_EPAI", objSpoProj.LEVEL_EPAI);
                        objParams[7] = new SqlParameter("@P_PROGRAM_NAME_EPAI", objSpoProj.PROGRAM_NAME_EPAI);
                        objParams[8] = new SqlParameter("@P_FROM_DATE_EPAI", objSpoProj.FROM_DATE_EPAI);
                        objParams[9] = new SqlParameter("@P_TO_DATE_EPAI", objSpoProj.TO_DATE_EPAI);
                        objParams[10] = new SqlParameter("@P_NO_OF_EXEC_ATTEND_COURSES_EPAI", objSpoProj.NO_OF_EXECUTIVE_ATTEND_COURSES);
                        objParams[11] = new SqlParameter("@P_OrganizationId", Org_id);
                        objParams[12] = new SqlParameter("@P_ISACTIVE", isactive);
                        objParams[13] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[13].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_RCPIT_EXECUTIVE_PROGRAM_ATTEND_INDUSTRY", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ee.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetallTPEPAI()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_All_ACD_TP_RCPIT_EXECUTIVE_PROGRAM_ATTEND_INDUSTRY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetTPEPAI_data(int TP_EPAI_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", TP_EPAI_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ACD_TP_RCPIT_EXECUTIVE_PROGRAM_ATTEND_INDUSTRY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                #region tab 10
                public int AddUpdateTpFacultyTrainedByIndustry(TPTraining objSpoProj, int Org_id, int isactive)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_FTI_ID", objSpoProj.TP_FTI_ID);
                        objParams[1] = new SqlParameter("@P_COMPANY_NO_FTI", objSpoProj.COMPANY_NAME_FTI);
                        objParams[2] = new SqlParameter("@P_SECTOR_NO_FTI", objSpoProj.SECTOR_NAME_FTI);
                        objParams[3] = new SqlParameter("@P_INCORPORATION_STATUS_FTI", objSpoProj.INCORPORATION_STATUS_FTI);
                        objParams[4] = new SqlParameter("@P_FACULTY_ID_FTI", objSpoProj.FACULTY_ID_FTI);
                        objParams[5] = new SqlParameter("@P_DISCIPLINE_FTI", objSpoProj.DISCIPLINE_FTI);
                        objParams[6] = new SqlParameter("@P_LEVEL_FTI", objSpoProj.LEVEL_FTI);
                        objParams[7] = new SqlParameter("@P_FROM_DATE_FTI", objSpoProj.FROM_DATE_FTI);
                        objParams[8] = new SqlParameter("@P_TO_DATE_FTI", objSpoProj.TO_DATE_FTI);
                        objParams[9] = new SqlParameter("@P_OrganizationId", Org_id);
                        objParams[10] = new SqlParameter("@P_ISACTIVE", isactive);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_RCPIT_FACULTY_TRAINED_BY_INDUSTRY", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ee.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetallTPFTI()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_All_ACD_TP_RCPIT_FACULTY_TRAINED_BY_INDUSTRY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetTPFTI_data(int TP_FTI_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", TP_FTI_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ACD_TP_RCPIT_FACULTY_TRAINED_BY_INDUSTRY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                #region tab 11
                public int AddUpdateTpFacultyPatentsLeadingToIndustryProducts(TPTraining objSpoProj, int Org_id, int isactive)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_FPP_ID", objSpoProj.FPP_ID);
                        objParams[1] = new SqlParameter("@P_COMPANY_NO_FPP", objSpoProj.COMPANY_NAME_FPP);
                        objParams[2] = new SqlParameter("@P_SECTOR_NO_FPP", objSpoProj.SECTOR_NAME_FPP);
                        objParams[3] = new SqlParameter("@P_INCORPORATION_STATUS_FPP", objSpoProj.INCORPORATION_STATUS_FPP);
                        objParams[4] = new SqlParameter("@P_FACULTY_ID_FPP", objSpoProj.FACULTY_ID_FPP);
                        objParams[5] = new SqlParameter("@P_DISCIPLINE_FPP", objSpoProj.DISCIPLINE_FPP);
                        objParams[6] = new SqlParameter("@P_LEVEL_FPP", objSpoProj.LEVEL_FPP);
                        objParams[7] = new SqlParameter("@P_PATENT_ADOPTION_DATE_FPP", objSpoProj.PATENT_ADOPTION_DATE_FPP);
                        objParams[8] = new SqlParameter("@P_PATENT_NO_FPP", objSpoProj.PATENT_NO_FPP);
                        objParams[9] = new SqlParameter("@P_GRANTED_FPP", objSpoProj.GRANTED_FPP);
                        objParams[10] = new SqlParameter("@P_PATENT_OWNER_FPP", objSpoProj.PATENT_OWNER_EPP);
                        objParams[11] = new SqlParameter("@P_YEAR_FPP", objSpoProj.YEAR_EPP);
                        objParams[12] = new SqlParameter("@P_OrganizationId", Org_id);
                        objParams[13] = new SqlParameter("@P_ISACTIVE", isactive);
                        objParams[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[14].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_RCPIT_FACULTY_PATENTS_INDUSTRY_PRODUCTS", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ee.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetallTPFPP()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_All_ACD_TP_RCPIT_FACULTY_PATENTS_INDUSTRY_PRODUCTS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetTPFPP_data(int FPP_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", FPP_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ACD_TP_RCPIT_FACULTY_PATENTS_INDUSTRY_PRODUCTS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                #region tab 12
                public int AddUpdateTpPapersAuthoredToIndustryByFaculty(TPTraining objSpoProj, int Org_id, int isactive)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_PAIF_ID", objSpoProj.PAIF_ID);
                        objParams[1] = new SqlParameter("@P_COMPANY_NO_PAIF", objSpoProj.COMPANY_NAME_PAIF);
                        objParams[2] = new SqlParameter("@P_SECTOR_NO_PAIF", objSpoProj.SECTOR_NAME_PAIF);
                        objParams[3] = new SqlParameter("@P_INCORPORATION_STATUS_PAIF", objSpoProj.INCORPORATION_STATUS_PAIF);
                        objParams[4] = new SqlParameter("@P_FACULTY_ID_PAIF", objSpoProj.FACULTY_ID_PAIF);
                        objParams[5] = new SqlParameter("@P_DISCIPLINE_PAIF", objSpoProj.DISCIPLINE_PAIF);
                        objParams[6] = new SqlParameter("@P_LEVEL_PAIF", objSpoProj.LEVEL_PAIF);
                        objParams[7] = new SqlParameter("@P_PRESENTED_DATE_PAIF", objSpoProj.PRESENTED_DATE_PAIF);
                        objParams[8] = new SqlParameter("@P_PAPER_TITLE_PAIF", objSpoProj.PAPER_TITLE_PAIF);
                        objParams[9] = new SqlParameter("@P_ASSIGNMENT_TYPE_PAIF", objSpoProj.ASSIGNMENT_TYPE_PAIF);
                        objParams[10] = new SqlParameter("@P_OrganizationId", Org_id);
                        objParams[11] = new SqlParameter("@P_ISACTIVE", isactive);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_RCPIT_PAPERS_AUTHORED_TO_INDUSTRY_BY_FACULTY", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ee.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetallTPPAIF()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_All_ACD_TP_RCPIT_PAPERS_AUTHORED_TO_INDUSTRY_BY_FACULTY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetTPPAIF_data(int PAIF_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", PAIF_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ACD_TP_RCPIT_PAPERS_AUTHORED_TO_INDUSTRY_BY_FACULTY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                #region tab 13
                public int AddUpdateTpServices(TPTraining objSpoProj, int Org_id, int isactive)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[16];
                        objParams[0] = new SqlParameter("@P_SEV_ID", objSpoProj.SEV_ID);
                        objParams[1] = new SqlParameter("@P_COMPANY_NO_SEV", objSpoProj.COMPANY_NAME_SEV);
                        objParams[2] = new SqlParameter("@P_SECTOR_NO_SEV", objSpoProj.SECTOR_NAME_SEV);
                        objParams[3] = new SqlParameter("@P_INCORPORATION_STATUS_SEV", objSpoProj.INCORPORATION_STATUS_SEV);
                        objParams[4] = new SqlParameter("@P_TYPE_OF_SERVICES_SEV", objSpoProj.TYPE_OF_SERVICES_SEV);
                        objParams[5] = new SqlParameter("@P_TITLE_OF_SERVICES_SEV", objSpoProj.TITLE_OF_SERVICES_SEV);
                        objParams[6] = new SqlParameter("@P_YEAR_SEV", objSpoProj.YEAR_SEV);
                        objParams[7] = new SqlParameter("@P_FACULTY_ID_SEV", objSpoProj.FACULTY_ID_SEV);
                        objParams[8] = new SqlParameter("@P_DISCIPLINE_SEV", objSpoProj.DISCIPLINE_SEV);
                        objParams[9] = new SqlParameter("@P_LEVEL_SEV", objSpoProj.LEVEL_SEV);
                        objParams[10] = new SqlParameter("@P_START_DATE_SEV", objSpoProj.START_DATE_SEV);
                        objParams[11] = new SqlParameter("@P_FINISH_DATE_SEV", objSpoProj.FINISH_DATE_SEV);
                        objParams[12] = new SqlParameter("@P_FEE_RECEIVED_FROM_INDUSTRY_SEV", objSpoProj.FEE_RECEIVED_FROM_INDUSTRY_SEV);
                        objParams[13] = new SqlParameter("@P_OrganizationId", Org_id);
                        objParams[14] = new SqlParameter("@P_ISACTIVE", isactive);
                        objParams[15] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[15].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_RCPIT_SERVICES", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ee.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetallTPSEV()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_All_ACD_TP_RCPIT_SERVICES", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetTPSEV_data(int SEV_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", SEV_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ACD_TP_RCPIT_SERVICES", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                #region tab 14
                public int AddUpdateTpSSE(TPTraining objSpoProj, int Org_id, int isactive)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New MainGroup Group
                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_SSE_ID", objSpoProj.SSE_ID);
                        objParams[1] = new SqlParameter("@P_STUDENT_FIRST_NAME_SSE", objSpoProj.STUDENT_FIRST_NAME_SSE);
                        objParams[2] = new SqlParameter("@P_STUDENT_LAST_NAME_SSE", objSpoProj.STUDENT_LAST_NAME_SSE);
                        objParams[3] = new SqlParameter("@P_TYPE_OF_SELF_EMPLOYMENT_SSE", objSpoProj.TYPE_OF_SELF_EMPLOYMENT_SSE);
                        objParams[4] = new SqlParameter("@P_DISCIPLINE_NO_SEE", objSpoProj.DISCIPLINE_SSE);
                        objParams[5] = new SqlParameter("@P_LEVEL_SEV", objSpoProj.LEVEL_SSE);
                        objParams[6] = new SqlParameter("@P_YEAR_SSE", objSpoProj.YEAR_SSE);
                        objParams[7] = new SqlParameter("@P_COMPANY_NO_SSE", objSpoProj.COMPANY_NO_SSE);
                        objParams[8] = new SqlParameter("@P_ADDRESS_SSE", objSpoProj.ADDRESS_SSE);
                        objParams[9] = new SqlParameter("@P_EMAIL_ID_SSE", objSpoProj.EMAIL_ID_SSE);
                        objParams[10] = new SqlParameter("@P_MOBILE_NO_SSE", objSpoProj.MOBILE_NO_SSE);
                        objParams[11] = new SqlParameter("@P_WEBSITE_SSE", objSpoProj.WEBSITE_SSE);
                        objParams[12] = new SqlParameter("@P_OrganizationId", Org_id);
                        objParams[13] = new SqlParameter("@P_ISACTIVE", isactive);
                        objParams[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[14].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_ACD_TP_RCPIT_STUDENT_SELF_EMPLOYMENT", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }

                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ee.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetallTPSSE()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_All_ACD_TP_RCPIT_STUDENT_SELF_EMPLOYMENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetTPSSE_data(int SSE_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", SSE_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ACD_TP_RCPIT_STUDENT_SELF_EMPLOYMENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion
                #endregion


                //--start-----Category Form Controller 17-03-2023

                public int Change_TP_Category(int IdNo)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", IdNo);
                        //objParams[1] = new SqlParameter("@P_APPLICATION_TYPE", ApplicationType);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPD_TP_TPCATEGORY_STATUS", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.TP_Category_Updation> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int TP_Category(int IdNo, int ApplicationType)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", IdNo);
                        objParams[1] = new SqlParameter("@P_APPLICATION_TYPE", ApplicationType);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_INS_CATEGORY", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.TP_Category_Updation> " + ex.ToString());
                    }
                    return retStatus;
                }

               

                //--end-----Category Form Controller 17-03-2023

                //----START ----17-03-2023--- DisciplinaryAction--


                //added by sumit karangale on 14052020 for INSERT DATA INTO TABLE OF DISCIPLINARY ACTION STUDENT
                public int AddStudentForDisciplinaryAction(string ENROLLNO, string REGNO, DateTime DISSTARTDATE, DateTime DISENDDATE, int STATUS, string REMARK)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_ENROLLNO", ENROLLNO);
                        objParams[1] = new SqlParameter("@P_REGNO", REGNO);
                        objParams[2] = new SqlParameter("@P_DISSTARTDATE", DISSTARTDATE);
                        objParams[3] = new SqlParameter("@P_DISENDDATE", DISENDDATE);
                        objParams[4] = new SqlParameter("@P_STATUS", STATUS);
                        objParams[5] = new SqlParameter("@P_REMARK", REMARK); //changes done by Nikhil
                        objParams[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_DISCIPLINARY_ACTION_STUDENT", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        //if (Convert.ToInt32(ret) == -99)
                        //    retStatus = Convert.ToInt32(ret); 
                        //else
                        //    retStatus = Convert.ToInt32(ret);                      
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.AddStudentForDisciplinaryAction-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public int UpdateDisciplinaryInfo(string REGNO, DateTime DISSTARTDATE, DateTime DISENDDATE, int STATUS, string REMARK)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];

                        //objParams[0] = new SqlParameter("@P_ENROLLNO", ENROLLNO);
                        objParams[0] = new SqlParameter("@P_REGNO", REGNO);
                        objParams[1] = new SqlParameter("@P_DISSTARTDATE", DISSTARTDATE);
                        objParams[2] = new SqlParameter("@P_DISENDDATE", DISENDDATE);
                        objParams[3] = new SqlParameter("@P_STATUS", STATUS);
                        objParams[4] = new SqlParameter("@P_REMARK", REMARK);//changes by Nikhil
                        objParams[5] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_TP_UPD_STUD_DISCIPLINARY_INFO", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateJobLoc-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public DataSet GetDisciplinaryActionStudent()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];
                        // objParams[0] = new SqlParameter("@P_DEGREENO", Degree);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_GET_DISCIPLINARY_STUDENT", objParams);


                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetDisciplinaryActionStudent-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }


                public DataSet GetDisciplinaryRecordByREGNo(string REGNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_REGNO", REGNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_TP_GET_DISCIPLINARY_RECORD_FOR_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLocatioByIDNo-> " + ex.ToString());
                    }
                    return ds;
                }

                //----END ----17-03-2023---- DisciplinaryAction-

                #region TP DataCollection For Placement Drive
                public DataSet GetDataForPlacementDrive(TPTraining objTPTrn)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_DEGREENO", objTPTrn.Degree);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", objTPTrn.Branch);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", objTPTrn.Semester);
                        objParams[3] = new SqlParameter("@P_CGPA", objTPTrn.CGPA);
                        objParams[4] = new SqlParameter("@P_BATCHNO", objTPTrn.ADMBatch);
                        objParams[5] = new SqlParameter("@P_BACKLOG", objTPTrn.NoOfAttempt);
                        objParams[6] = new SqlParameter("@P_ATTEMPT", objTPTrn.Attempt);
                        ds = objSQLHelper.ExecuteDataSetSP("ACD_TP_DATA_COLLECTION_FOR_PLACEMENT_DRIVE", objParams);


                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetDisciplinaryActionStudent-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                #endregion


                public int AddCategoryConf(TPTraining objtptraining)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_DEGREENO", objtptraining.CADegree);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", objtptraining.CASemester);
                        objParams[2] = new SqlParameter("@P_STATUS", objtptraining.CAStatus);
                        objParams[3] = new SqlParameter("@P_UA_NO", objtptraining.CAUA_NO);
                        objParams[4] = new SqlParameter("@P_CAID", objtptraining.CAID);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_TP_CATEGORY_CONFIGURATION", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == 1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else if (Convert.ToInt32(ret) == 2)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.AddUpdVehicleTransportApplication->" + ex.ToString());
                    }
                    return retstatus;
                }


                public DataSet GetCategoryconf()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("ACD_TP_GET_CATEGORY_CONFIGURATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }


                public DataSet EditCategoryConfByID(int CAID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_CACONFID", CAID);
                        ds = objSQLHelper.ExecuteDataSetSP("ACD_TP_EDIT_CATEGORY_CONFIGURATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.GetJobLoc-> " + ex.ToString());

                    }
                    return ds;
                }


            }

        }

    }
}
