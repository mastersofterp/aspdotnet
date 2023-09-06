using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class ImportDataController
            {
                private string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public int InsertToSqlDB(ImportDataMaster objIDM, int type)
                {
                    int pkid = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;
                        object ret = null;
                        if (type != 1)
                        {
                            objParams = new SqlParameter[19];
                            objParams[0] = new SqlParameter("@P_NO", objIDM.NO);
                            objParams[1] = new SqlParameter("@P_NAME", objIDM.CANDIDATENAME);
                            objParams[2] = new SqlParameter("@P_PRN", objIDM.PRN);
                            objParams[3] = new SqlParameter("@P_GATE_YEAR", objIDM.GATEYEAR);
                            objParams[4] = new SqlParameter("@P_GATE_REG", objIDM.GATEREG);
                            objParams[5] = new SqlParameter("@P_GATE_SCORE", objIDM.GATESCORE);
                            objParams[6] = new SqlParameter("@P_GATE_PAPER", objIDM.GATEPAPER);
                            objParams[7] = new SqlParameter("@P_DOB", objIDM.DOB);
                            objParams[8] = new SqlParameter("@P_GENDER", objIDM.GENDER);
                            objParams[9] = new SqlParameter("@P_MOBILE", objIDM.MOBILE);
                            objParams[10] = new SqlParameter("@P_APPLICANT_CATEGORY", objIDM.APPLICANTCATEGORY);
                            objParams[11] = new SqlParameter("@P_PROGRAMME", objIDM.PROGRAMME);
                            objParams[12] = new SqlParameter("@P_INSTITUTE", objIDM.INSTITUTE);
                            objParams[13] = new SqlParameter("@P_ALLOTTED_CATEGORY", objIDM.ALLOTTEDCATEGORY);
                            objParams[14] = new SqlParameter("@P_GROUP", objIDM.GROUP);
                            objParams[15] = new SqlParameter("@P_DEGREENO", objIDM.DEGREENO);
                            objParams[16] = new SqlParameter("@P_BRANCHNO", objIDM.BRANCHNO);
                            objParams[17] = new SqlParameter("@P_BATCHNO", objIDM.BATCHNO);

                            objParams[18] = new SqlParameter("@P_OUT", SqlDbType.Int);
                            objParams[18].Direction = ParameterDirection.Output;
                            ret = objSQLHelper.ExecuteNonQuerySP("PKG_IMPORT_DATA_INSERT_MTECH", objParams, true);
                        }
                        else
                        {
                            objParams = new SqlParameter[19];
                            objParams[0] = new SqlParameter("@P_NO", objIDM.NO);
                            objParams[1] = new SqlParameter("@P_NAME", objIDM.CANDIDATENAME);
                            objParams[2] = new SqlParameter("@P_DEGREENO", objIDM.DEGREENO);
                            objParams[3] = new SqlParameter("@P_BRANCHNO", objIDM.BRANCHNO);
                            objParams[4] = new SqlParameter("@P_ROLL_NO", objIDM.ROLLNO);
                            objParams[5] = new SqlParameter("@P_AIR_OVERALL", objIDM.AIROVERALL);
                            objParams[6] = new SqlParameter("@P_APPLICANT_CATEGORY", objIDM.APPLICANTCATEGORY);
                            objParams[7] = new SqlParameter("@P_ALLOTTED_CATEGORY", objIDM.ALLOTTEDCATEGORY);
                            objParams[8] = new SqlParameter("@P_BRANCH_NAME", objIDM.BRANCHNAME);
                            objParams[9] = new SqlParameter("@P_PH", objIDM.PH);
                            objParams[10] = new SqlParameter("@P_HOME_STATE", objIDM.HOMESTATE);
                            // objParams[22] = new SqlParameter("@P_REPORTING_DATE", objIDM.REPORTINGDATE);
                            objParams[11] = new SqlParameter("@P_QUOTA", objIDM.QUOTA);
                            objParams[12] = new SqlParameter("@P_ROUND_NO", objIDM.ROUNDNO);
                            objParams[13] = new SqlParameter("@P_BATCHNO", objIDM.BATCHNO);

                            objParams[14] = new SqlParameter("@P_MOBILE", objIDM.MOBILE);
                            objParams[15] = new SqlParameter("@P_FATHERNAME", objIDM.FATHERNAME);
                            objParams[16] = new SqlParameter("@P_MOTHERNAME", objIDM.MOTHERNAME);
                            objParams[17] = new SqlParameter("@P_GENDER", objIDM.GENDER);

                            objParams[18] = new SqlParameter("@P_OUT", SqlDbType.Int);
                            objParams[18].Direction = ParameterDirection.Output;
                            ret = objSQLHelper.ExecuteNonQuerySP("PKG_IMPORT_DATA_INSERT_BTECH", objParams, true);
                        }

                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                pkid = 99;
                            else
                                pkid = Convert.ToInt16(ret.ToString());
                        }
                        else
                            pkid = 99;

                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.ImportDataController.InsertToSqlDB-> " + ee.ToString());
                    }
                    return pkid;

                }
                /// <summary>
                /// Developed By : Mr.Manish Walde
                /// Date : 15-April-2016
                /// Used to Import course registration data of students in bulk for ug
                /// </summary>
                /// <param name="objIdm"></param>
                /// <param name="dtBulkData"></param>
                /// <returns>Total No of imported students</returns>
                /// 

                //AAYUSHI GUPTA
                public int ImportDataForStudentREgistration(ImportDataMaster objIdm, DataTable dtBulkData, int ua_no)
                {
                    int retv = 0;
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_uaims_constr);
                        SqlParameter[] objPar = new SqlParameter[]
                        {   
                            new SqlParameter("@tblStudCourseReg1",dtBulkData),
                            new SqlParameter("@P_OUTPUT",SqlDbType.Int)
                        };
                        objPar[objPar.Length - 1].Direction = ParameterDirection.Output;

                        object val = objSQL.ExecuteNonQuerySP("PKG_UPDATE_DETAIL_EXCEL", objPar, true);

                        if (val != null)
                        {
                            if (val.ToString().Equals("-99"))
                                retv = -99;
                            else
                                retv = Convert.ToInt16(val.ToString());
                        }
                        else
                            retv = -99;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.ImportDataForStudentREgistration.InsertToSqlDB-> " + ex.ToString());
                    }
                    return retv;
                }
                //public DataSet VerifyandRegisterImportedCourseRegDataUG_Bulk(ImportDataMaster objIdm, int ua_no, int prev_status)//, out int retv
                //{
                //    //int ret = 0;
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQL = new SQLHelper(_uaims_constr);
                //        SqlParameter[] objPar = new SqlParameter[]
                //        {
                //            new SqlParameter("@P_SESSIONNO",objIdm.SESSIONNO),
                //            new SqlParameter("@P_COLLEGEID",objIdm.COLLEGEID),
                //            new SqlParameter("@P_DEGREENO",objIdm.DEGREENO),
                //            new SqlParameter("@P_BRANCHNO",objIdm.BRANCHNO),
                //            new SqlParameter("@P_SEMESTERNO",objIdm.SEMESTERNO),
                //            new SqlParameter("@P_IPADDRESS",objIdm.IPADDRESS),
                //            new SqlParameter("@P_UA_NO",ua_no),
                //            new SqlParameter("@P_PREV_STATUS",prev_status)//,
                //            //new SqlParameter("@P_OUTPUT",ret)
                //        };
                //        //objPar[objPar.Length - 1].Direction = ParameterDirection.Output;

                //        //ds = objSQL.ExecuteNonQueryWithDataSetSP("PR_ACD_INS_VERIFY_IMPORTED_COURSE_REG_DATA", objPar, true, out ret);
                //        ds = objSQL.ExecuteDataSetSP("PR_ACD_INS_VERIFY_IMPORTED_COURSE_REG_DATA", objPar);
                //        //retv = Convert.ToInt16(ret.ToString());
                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.ImportCourseRegDataUG_Bulk.InsertToSqlDB-> " + ex.ToString());
                //    }
                //    return ds;
                //}
                public DataSet ImportCourseRegDataUG_Bulk(ImportDataMaster objIdm, DataTable dtBulkData, int ua_no, int prev_status)
                {
                    //int retv = 0;
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_uaims_constr);
                        SqlParameter[] objPar = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO",objIdm.SESSIONNO),
                            new SqlParameter("@P_COLLEGEID",objIdm.COLLEGEID),
                            new SqlParameter("@P_DEGREENO",objIdm.DEGREENO),
                            new SqlParameter("@P_BRANCHNO",objIdm.BRANCHNO),
                            new SqlParameter("@P_SEMESTERNO",objIdm.SEMESTERNO),
                            new SqlParameter("@P_IPADDRESS",objIdm.IPADDRESS),
                            new SqlParameter("@P_UA_NO",ua_no),
                            new SqlParameter("@P_PREV_STATUS",prev_status),
                            new SqlParameter("@tblStudCourseReg",dtBulkData),
                          //  new SqlParameter("@P_OUTPUT",SqlDbType.Int)
                        };
                       // objPar[objPar.Length - 1].Direction = ParameterDirection.Output;

                        ds = objSQL.ExecuteDataSetSP("PR_ACD_INS_EXCEL_COURSE_REG_DATA", objPar);

                        //if (val != null)
                        //{
                        //    if (val.ToString().Equals("-99"))
                        //        retv = -99;
                        //    else
                        //        retv = Convert.ToInt16(val.ToString());
                        //}
                        //else
                        //    retv = -99;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.ImportCourseRegDataUG_Bulk.InsertToSqlDB-> " + ex.ToString());
                    }
                    return ds;
                }
                /// <summary>
                /// Developed By : Mr.Manish Walde
                /// Date : 15-April-2016
                /// Used to verify Imported course registration data of students in bulk for ug and register
                /// </summary>
                /// <param name="objIdm"></param>
                /// <param name="dtBulkData"></param>
                /// <returns>Total No of imported students</returns>
                public DataSet VerifyandRegisterImportedCourseRegDataUG_Bulk(ImportDataMaster objIdm, int ua_no, int prev_status)//, out int retv
                {
                    //int ret = 0;
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_uaims_constr);
                        SqlParameter[] objPar = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO",objIdm.SESSIONNO),
                            new SqlParameter("@P_COLLEGEID",objIdm.COLLEGEID),
                            new SqlParameter("@P_DEGREENO",objIdm.DEGREENO),
                            new SqlParameter("@P_BRANCHNO",objIdm.BRANCHNO),
                            new SqlParameter("@P_SEMESTERNO",objIdm.SEMESTERNO),
                            new SqlParameter("@P_IPADDRESS",objIdm.IPADDRESS),
                            new SqlParameter("@P_UA_NO",ua_no),
                            new SqlParameter("@P_PREV_STATUS",prev_status)//,
                            //new SqlParameter("@P_OUTPUT",ret)
                        };
                        //objPar[objPar.Length - 1].Direction = ParameterDirection.Output;

                        //ds = objSQL.ExecuteNonQueryWithDataSetSP("PR_ACD_INS_VERIFY_IMPORTED_COURSE_REG_DATA", objPar, true, out ret);
                        ds = objSQL.ExecuteDataSetSP("PR_ACD_INS_VERIFY_IMPORTED_COURSE_REG_DATA", objPar);
                        //retv = Convert.ToInt16(ret.ToString());
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.ImportCourseRegDataUG_Bulk.InsertToSqlDB-> " + ex.ToString());
                    }
                    return ds;
                }
                /// <summary>
                /// Developed By : Mr.Manish Walde
                /// Date : 20-April-2016
                /// Used to change collge of students
                /// </summary>
                /// <param name="objIdm"></param>
                /// <param name="dtBulkData"></param>
                /// <returns>Success flag</returns>
                public int ChangeCollegeUG_Bulk(ImportDataMaster objIdm, int ua_no, string idnos)
                {
                    int retv = 0;
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_uaims_constr);
                        SqlParameter[] objPar = new SqlParameter[]
                        {
                            new SqlParameter("@P_IDNOS",idnos),
                            new SqlParameter("@P_SESSIONNO",objIdm.SESSIONNO),
                            new SqlParameter("@P_NEW_COLLEGEID",objIdm.COLLEGEID),
                            //new SqlParameter("@P_DEGREENO",objIdm.DEGREENO),
                            //new SqlParameter("@P_BRANCHNO",objIdm.BRANCHNO),
                            new SqlParameter("@P_SEMESTERNO",objIdm.SEMESTERNO),
                            new SqlParameter("@P_IPADDRESS",objIdm.IPADDRESS),
                            new SqlParameter("@P_UA_NO",ua_no),
                            new SqlParameter("@P_OUTPUT",SqlDbType.Int)
                        };
                        objPar[objPar.Length - 1].Direction = ParameterDirection.Output;

                        object val = objSQL.ExecuteNonQuerySP("PR_ACD_INS_COLLEGE_CHANGE", objPar, true);

                        if (val != null)
                        {
                            if (val.ToString().Equals("-99"))
                                retv = -99;
                            else
                                retv = Convert.ToInt16(val.ToString());
                        }
                        else
                            retv = -99;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.ImportCourseRegDataUG_Bulk.InsertToSqlDB-> " + ex.ToString());
                    }
                    return retv;
                }

                public DataSet ImportExcelStudentFeesData(ImportDataMaster objIdm, DataTable dtBulkData, int ua_no,string receipttype)
                {
                    //int retv = 0;
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_uaims_constr);
                        SqlParameter[] objPar = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO",objIdm.SESSIONNO),
                            new SqlParameter("@P_COLLEGEID",objIdm.COLLEGEID),
                            new SqlParameter("@P_DEGREENO",objIdm.DEGREENO),
                            new SqlParameter("@P_BRANCHNO",objIdm.BRANCHNO),
                            new SqlParameter("@P_RECEIPT_CODE",receipttype),
                            new SqlParameter("@P_SEMESTERNO",objIdm.SEMESTERNO),
                            new SqlParameter("@P_IPADDRESS",objIdm.IPADDRESS),
                            new SqlParameter("@P_UA_NO",ua_no),
                            new SqlParameter("@TBLSTUDAMTREG",dtBulkData),
                          //  new SqlParameter("@P_OUTPUT",SqlDbType.Int)
                        };
                        // objPar[objPar.Length - 1].Direction = ParameterDirection.Output;

                        ds = objSQL.ExecuteDataSetSP("PKG_ACAD_INS_EXCEL_STUD_REG_FEES_AMOUNT", objPar);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.ImportCourseRegDataUG_Bulk.InsertToSqlDB-> " + ex.ToString());
                    }
                    return ds;
                }

                public int VerifyandRegisterImportedStudentBulkFeesData(ImportDataMaster objIdm, int ua_no,string receipttype)//, out int retv
                {
                    int ret = 0;
                    //  DataSet ds = null;
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_uaims_constr);
                        SqlParameter[] objPar = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO",objIdm.SESSIONNO),
                            new SqlParameter("@P_COLLEGEID",objIdm.COLLEGEID),
                            new SqlParameter("@P_DEGREENO",objIdm.DEGREENO),
                            new SqlParameter("@P_BRANCHNO",objIdm.BRANCHNO),
                            new SqlParameter("@P_RECEIPT_CODE",receipttype),
                            new SqlParameter("@P_SEMESTERNO",objIdm.SEMESTERNO),
                            new SqlParameter("@P_IPADDRESS",objIdm.IPADDRESS),
                            new SqlParameter("@P_UA_NO",ua_no),
                            new SqlParameter("@P_OUTPUT",ret)
                        };
                        objPar[objPar.Length - 1].Direction = ParameterDirection.Output;
                        object val = objSQL.ExecuteNonQuerySP("PKG__ACAD_INS_VERIFY_IMPORTED_STUDENT_FEES_DATA", objPar, true);
                        if (val != null)
                        {
                            if (val.ToString().Equals("-99"))
                                ret = -99;
                            else
                                ret = Convert.ToInt16(val.ToString());
                        }
                        else
                            ret = -99;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.VerifyandRegisterImportedStudentBulkFeesData-> " + ex.ToString());
                    }
                    return ret;
                }

                public int InsertBulkMessageLog(string regno, int msgtype, string subject, string message, int uano, string ipaddress, string email, string mobile)
                {
                    int ret = 0;
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_uaims_constr);
                        SqlParameter[] objPar = new SqlParameter[]
                        {
                            new SqlParameter("@P_REGNO",regno),
                            new SqlParameter("@P_MESSAGE_TYPE",msgtype),
                            new SqlParameter("@P_SUBJECT",subject),
                            new SqlParameter("@P_MESSAGE",message),
                            new SqlParameter("@P_UANO",uano),
                            new SqlParameter("@P_IPADDRESS",ipaddress),
                            new SqlParameter("@P_MAIL",email),
                            new SqlParameter("@P_MOBILE",mobile),
                            new SqlParameter("@P_USER_TYPE",1),
                            new SqlParameter("@P_OUTPUT",ret)
                        };
                        objPar[objPar.Length - 1].Direction = ParameterDirection.Output;

                        object val = objSQL.ExecuteNonQuerySP("PKG_ACAD_INSERT_BULK_MESSAGE_LOG", objPar, true);

                        if (val != null)
                        {
                            ret = Convert.ToInt16(val.ToString());
                        }
                        else
                        {
                            ret = -99;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.InsertBulkMessageLog-> " + ex.ToString());
                    }
                    return ret;
                }

                public DataSet getTestPrepDataExcel(int Sessionno, int InstituteNo)
                {
                    //int retv = 0;
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_uaims_constr);
                        SqlParameter[] objPar = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO",Sessionno),
                          
                            new SqlParameter("@P_COLLEGE_ID",InstituteNo),
                         
                        };
                      
                        ds = objSQL.ExecuteDataSetSP("PKG_EXAM_DATA_FOR_TESTPREP", objPar);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.ImportCourseRegDataUG_Bulk.getTestPrepDataExcel-> " + ex.ToString());
                    }
                    return ds;
                }

                //ADDED BY SAFAL GUPTA 24032021
                public int getTestPrepExamDataUploadExcel(int Sessionno, int InstituteNo, string filename,DataTable dt,int ua_no,string ipaddress)
                {
                    int ret = 0;
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_uaims_constr);
                        SqlParameter[] objPar = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO",Sessionno),
                            new SqlParameter("@P_COLLEGE_ID",InstituteNo),
                            new SqlParameter("@P_FILENAME",filename),
                            new SqlParameter("@P_TBLBULKDATA",dt),
                            new SqlParameter("@IP_ADDRESS",ipaddress),
                            new SqlParameter("@P_UA_NO",ua_no),
                            new SqlParameter("@P_OUTPUT",DbType.Int32)
                        };
                        objPar[objPar.Length - 1].Direction = ParameterDirection.Output;

                        object val = objSQL.ExecuteNonQuerySP("PKG_EXAM_DATA_UPLOAD_TO_ERP_FROM_TESTPREP", objPar, true);
                        if (val != null)
                        {
                            ret = Convert.ToInt16(val.ToString());
                        }
                        else
                        {
                            ret = -99;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.ImportCourseRegDataUG_Bulk.getTestPrepExamDataUploadExcel-> " + ex.ToString());
                    }
                    return ret;
                }

                public DataSet getTestPrepExamDataPendingExcel(int Sessionno, int InstituteNo)
                {
                    //int retv = 0;
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_uaims_constr);
                        SqlParameter[] objPar = new SqlParameter[]
                        {
                            new SqlParameter("@P_SESSIONNO",Sessionno),
                            new SqlParameter("@P_COLLEGE_ID",InstituteNo),
                        };

                        ds = objSQL.ExecuteDataSetSP("PKG_EXAM_DATA_UPLOAD_PENDING_FROM_TESTPREP", objPar);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.ImportCourseRegDataUG_Bulk.getTestPrepExamDataPendingExcel-> " + ex.ToString());
                    }
                    return ds;
                }


                //Mahesh Malve On Dated 22-06-2021
                public object InsertTestPrepExamDataThroughAPI(TestPrepThroughtMISEntity objTPTM)
                {
                    try
                    {
                        object Result = null;
                        SQLHelper objDataAccessLayer = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[]
                        {
                           new SqlParameter("@P_TBLEXAMDATABULK",objTPTM.dt),
                           new SqlParameter("@P_SESSIONNO",objTPTM.SESSIONNO),
                           new SqlParameter("@P_CREATEDBY",objTPTM.CreatedBy),
                           new SqlParameter("@P_CREATED_IP",objTPTM.CreatedIP),
                           new SqlParameter("@P_OUTPUT",SqlDbType.Int)
                        };

                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;
                        Result = objDataAccessLayer.ExecuteNonQuerySP("PKG_INSERT_TESTPREPEXAMDATATHROUGHT_API", objParams, true);

                        return Result;
                    }
                    catch (Exception Ex)
                    {
                        throw new Exception(Ex.Message);
                    }
                }
                public int InsertToSqlDB(ImportDataMaster objIDM)
                {
                    int pkid = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;
                        object ret = null;
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_NO", objIDM.NO);
                        objParams[1] = new SqlParameter("@P_MeritNo", objIDM.Meritno);
                        objParams[2] = new SqlParameter("@P_NAME", objIDM.CANDIDATENAME);
                        objParams[3] = new SqlParameter("@P_DEGREENO", objIDM.DEGREENO);
                        objParams[4] = new SqlParameter("@P_APPLICATIONID", objIDM.ApplicationId);
                        objParams[5] = new SqlParameter("@P_SCORE", objIDM.GATESCORE);
                        objParams[6] = new SqlParameter("@P_ADMBATCH", objIDM.BATCHNO);
                        objParams[7] = new SqlParameter("@P_GENDER", objIDM.GENDER);
                        objParams[8] = new SqlParameter("@P_CATEGORY", objIDM.APPLICANTCATEGORY);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;
                        ret = objSQLHelper.ExecuteNonQuerySP("PKG_IMPORT_DATA_INSERT_BTECH", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                pkid = 99;
                            else
                                pkid = Convert.ToInt16(ret.ToString());
                        }
                        else
                            pkid = 99;

                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.ImportDataController.InsertToSqlDB-> " + ee.ToString());
                    }
                    return pkid;

                }
                

                public int InsertConvocationUser(string regno, string studname, string degree, string branch, string convo_date, string class_obtained, string convo)
                {
                    int pkid = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;
                        object ret = null;
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_REGNO", regno);
                        objParams[1] = new SqlParameter("@P_STUDNAME", studname);
                        objParams[2] = new SqlParameter("@P_DEGREE", degree);
                        objParams[3] = new SqlParameter("@P_BRANCH", branch);
                        objParams[4] = new SqlParameter("@P_CONVOCATION_DATE", convo_date);
                        objParams[5] = new SqlParameter("@P_CLASS", class_obtained);
                        objParams[6] = new SqlParameter("@P_CONV_NO", convo);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                        ret = objSQLHelper.ExecuteNonQuerySP("PKG_IMPORT_DATA_CONVOCATION_USER", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                pkid = 99;
                            else
                                pkid = Convert.ToInt16(ret.ToString());
                        }
                        else
                            pkid = 99;

                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.ImportDataController.InsertToSqlDB-> " + ee.ToString());
                    }
                    return pkid;
                }

            }
        }
    }
}
