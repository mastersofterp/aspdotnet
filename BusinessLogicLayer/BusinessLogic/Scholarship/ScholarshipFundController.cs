//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                        
// PAGE NAME     : SCHOLARSHIP FUND CONTROLLER CLASS
// CREATION DATE : 27-NOV-2013                                                        
// CREATED BY    : UMESH GANORKAR
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

namespace BusinessLogicLayer.BusinessLogic.Fees
{
    public class ScholarshipFundController
    {
        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet GetAllScholarshipFund()
        {
            DataSet ds = null;
            try
            {
                SQLHelper dataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[0];

                ds = dataAccess.ExecuteDataSetSP("PKG_ACAD_ALL_SCHOLARSHIP_FUND", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ScholarshipFundController.GetAllScholarshipFund --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        // INSERT SCHOLARSHIP FUND
        public int AddSchFund(ScolarshipFund objSF)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                //Add New Exam Configuration
                objParams = new SqlParameter[15];
                objParams[0] = new SqlParameter("@P_ADMYAER", objSF.Admyear);
                objParams[1] = new SqlParameter("@P_DEGREENO", objSF.Degree);
                objParams[2] = new SqlParameter("@P_SF_BILLNO", objSF.Sfbillno);
                objParams[3] = new SqlParameter("@P_SF_DATE", objSF.SfDate);
                objParams[4] = new SqlParameter("@P_SF_CHEQUENO", objSF.Sfchequeno);
                objParams[5] = new SqlParameter("@P_SF_CHEQUEDATE", objSF.SfchequeDate);
                objParams[6] = new SqlParameter("@P_SF_AMT", objSF.Sfamt);
                objParams[7] = new SqlParameter("@P_CONCESSIONNO", objSF.Concessionno);
                objParams[8] = new SqlParameter("@P_SCHOLARSHIPNO", objSF.ScholarshipNo);
                objParams[9] = new SqlParameter("@P_REMARK", objSF.Sfremark);
                objParams[10] = new SqlParameter("@P_COLLEGE_CODE", objSF.Collegecode);
                objParams[11] = new SqlParameter("@P_CATEGORYNO", objSF.CategoryNo);
                objParams[12] = new SqlParameter("@P_SHIFTNO", objSF.ShiiftNo);
                objParams[13] = new SqlParameter("@P_STUDCOUNT", objSF.StudCount);
                objParams[14] = new SqlParameter("@P_SF_NO", SqlDbType.Int);
                objParams[14].Direction = ParameterDirection.Output;

                if (objSQLHelper.ExecuteNonQuerySP("PKG_FEES_INS_SCOLARSHIP_FUND", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ScholarshipFundController.AddConfig -> " + ex.ToString());
            }
            return retStatus;
        }

        //STIPEND RATE MASTER DETAILS INSER & UPDATE 
        public int Sch_RateMaster_Insert_Update(ScolarshipFund objSF)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                //UpdateFaculty Existing Exam Configuration
                objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_SRNO", objSF.Srno);
                objParams[1] = new SqlParameter("@P_DEGREE", objSF.Degree);
                objParams[2] = new SqlParameter("@P_BRANCH", objSF.Branch); 
                objParams[3] = new SqlParameter("@P_STARTDATE", objSF.stDate);
                objParams[4] = new SqlParameter("@P_AMT1", objSF.Monamt1);
                objParams[5] = new SqlParameter("@P_AMT2", objSF.Monamt2);
                objParams[6] = new SqlParameter("@P_AMT3", objSF.Monamt3);
                objParams[7] = new SqlParameter("@P_NONEGATEAMT1", objSF.Nongateamt);
                objParams[8] = new SqlParameter("@P_REMARK", objSF.Remark);
                objParams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;

                if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STIPEND_RATE_MASTER", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ScholarshipFundController.UpdateSchFund -> " + ex.ToString());
            }
            return retStatus;
        }
        public SqlDataReader GetSingleSchFund(int sf_no)
        {
            SqlDataReader dr = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_SF_NO", sf_no);
                dr = objSQLHelper.ExecuteReaderSP("PKG_FEES_RET_SCHOLARSHIP_FUND", objParams);
            }
            catch (Exception ex)
            {
                return dr;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ScholarshipFundController.GetSingleSchFund -> " + ex.ToString());
            }
            return dr;
        }

        public DataSet GetstudForSchEntry(int degreeno, int branchno, int admbatchno, int year, int concessionno,int category, string BillNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_DEGREENO", degreeno),
                    new SqlParameter("@P_BRANCHNO", branchno),
                    new SqlParameter("@P_ADMBATCHNO", admbatchno),
                    //new SqlParameter("@P_SEMESTERNO", semesterno),
                    new SqlParameter("@P_YEAR", year),
                    new SqlParameter("@P_CONCESSIONNO", concessionno),
                    new SqlParameter("@P_CATEGORY", category),
                    new SqlParameter("@P_BILLNO", BillNo)

                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEES_RET_STUD_SCHOLARSHIP_ENTRY", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.ScholarshipFundController.GetstudForSchEntry() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //INSERT SCHOLARSHIP ENTRY 
        public int InsSchEntry(string idnos, string regnos, int degreeno, int branchno, int admbatchno, int year, int concessionno,string bill_no, string schamt, int uano,double schtotamt)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_IDNOS", idnos),
                            new SqlParameter("@P_REGNO",regnos),
                            new SqlParameter("@P_DEGREENO", degreeno),
                            new SqlParameter("@P_BRANCHNO", branchno),
                            new SqlParameter("@P_ADMBATCHNO",admbatchno),
                            //new SqlParameter("@P_SEMESTERNO",semesterno),
                            new SqlParameter("@P_YEAR",year),
                            new SqlParameter("@P_CONCESSIONNO",concessionno),
                            new SqlParameter("@P_BILL_NO",bill_no),
                            new SqlParameter("@P_SCH_AMT",schamt),
                            new SqlParameter("@P_UA_NO",uano),
                            new SqlParameter("@P_SCH_TOT_AMT",schtotamt),
                            //new SqlParameter("@P_SCH_EXCESS_AMT", schexcessamt),
                            new SqlParameter("@P_OUT", SqlDbType.Int) 
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objDataAccess.ExecuteNonQuerySP("PKG_FEES_INS_SCHOLARSHIP_ENTRY", sqlParams, true);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else if (Convert.ToInt32(ret) == 1)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    retStatus = Convert.ToInt32(CustomStatus.Error);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ScholarshipFundController.InsSchEntry-> " + ex.ToString());
            }

            return retStatus;
        }

        //GET BILL INFO
        public DataSet GetBillInfo(int admbatch, int concessionno,string billno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                        {
                           
                             new SqlParameter("@P_ADMBATCH", admbatch),
                             new SqlParameter("@P_CONCESSIONNO", concessionno),
                             new SqlParameter("@P_BILLNO", billno)

                        };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_FEES_RET_BILL_INFO", sqlParams);

            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentDetentionController.GetStudInfoGroup-> " + ex.ToString());
            }
            return ds;
        }

        public SqlDataReader GetOrderDetails(int sf_no)//.
        {
            SqlDataReader dr = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_SF_NO", sf_no);
                dr = objSQLHelper.ExecuteReaderSP("PKG_FEES_RET_ORDER_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                return dr;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ScholarshipFundController.GetSingleSchFund -> " + ex.ToString());
            }
            return dr;
        }

        public int AddFeeDesc(string fee_code, string fee_desc, int fee_srno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {

                SQLHelper objSqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_FEE_CODE", fee_code);
                objParams[1] = new SqlParameter("@P_FEE_DESC", fee_desc);
                objParams[2] = new SqlParameter("@P_FEE_SRNO", fee_srno);
                objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;

                object ret = objSqlhelper.ExecuteNonQuerySP("PKG_FEE_DESC_MASTER", objParams, true);

                if (ret.ToString() == "1" && ret != null)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.AddStudConFirmInfo-> " + ex.ToString());
            }

            return retStatus;
        }

        public int AddScholarship(string sch_name,string sch_code ,int sch_srno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {

                SQLHelper objSqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_SCH_NO", sch_srno);
                objParams[1] = new SqlParameter("@P_SCH_NAME", sch_name);
                objParams[2] = new SqlParameter("@P_SCH_CODE", sch_code);
                objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;

                object ret = objSqlhelper.ExecuteNonQuerySP("PKG_SCHOLARSHIP_MASTER_INSERT_UPDATE", objParams, true);

                if (ret.ToString() == "1" && ret != null)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.AddStudConFirmInfo-> " + ex.ToString());
            }

            return retStatus;
        }
        public int InsetScholarshipAmount(int idnos, string regnos, int degreeno, int branchno, int admbatchno, int year, int concessionno, int bill_no, double schamt, int uano, string colcode, int categoryno, int shiftno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_IDNO", idnos),
                            new SqlParameter("@REGNO",regnos),
                            new SqlParameter("@P_DEGREENO", degreeno),
                            new SqlParameter("@P_BRANCHNO", branchno),
                            new SqlParameter("@P_ADMBATCH",admbatchno),
                            //new SqlParameter("@P_SEMESTERNO",semesterno),
                            new SqlParameter("@P_YEAR",year),
                            new SqlParameter("@P_CONCESSIONNO",concessionno),
                            new SqlParameter("@P_BILL_NO",bill_no),
                            new SqlParameter("@P_SCH_AMT",schamt),
                            new SqlParameter("@P_UANO",uano),
                            new SqlParameter("@P_COLLEGE_CODE",colcode),
                            new SqlParameter("@P_CATEGORYNO", categoryno),
                            new SqlParameter("@P_SHIFTNO", shiftno),
                            new SqlParameter("@P_OUT", SqlDbType.Int) 
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objDataAccess.ExecuteNonQuerySP("PKG_SCHOLARSHIP_AMOUNT_INSERT", sqlParams, true);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else if (Convert.ToInt32(ret) == 1)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    retStatus = Convert.ToInt32(CustomStatus.Error);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ScholarshipFundController.InsSchEntry-> " + ex.ToString());
            }

            return retStatus;
        }
        //UPDATE SCHOLARSHIP CONCESSION TYPE
        public DataSet GetStudForScholarshipUpdate(int sessionno, int categoryno, int Degreeno, int year, int shiftno,int branchno, int admissionmode)//.
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_connectionString);
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@P_SESSIONNO",sessionno),
                    new SqlParameter("@P_CATEGORYNO",categoryno),
                    new SqlParameter("@P_DEGREENO",Degreeno),
                    new SqlParameter("@P_YEAR",year),
                    new SqlParameter("@P_SHIFTNO",shiftno),
                    new SqlParameter("@P_BRANCHNO",branchno),
                    new SqlParameter("@P_ADMISSIONMODE",admissionmode),
                };
                ds = objSqlHelper.ExecuteDataSetSP("PKG_SCHOLARSHIP_TYPE_UPDATE", param);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.ScholarshipFundController.GetStudForScholarshipUpdate() --> " + ex.Message + " " + ex.StackTrace);

            }
            return ds;
        }

        public int updateConcessionType(int idno, int concessionno)//.
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_CONCESSIONNO", concessionno);
                if (objSQLHelper.ExecuteNonQuerySP("PKG_SCHOLARSHIP_CONCESSIONTYPE_UPDATE", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ScholarshipFundController.UpdateSchFund -> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetStudentForScholarshipAmountUpdate(int admbatch, int categoryno, int concessionno, int shiftno, int degreeno, int branchno, int year)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_connectionString);
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@P_ADMBATCH",admbatch),
                    new SqlParameter("@P_CATEGORYNO",categoryno),
                    new SqlParameter("@P_CONCESSIONNO",concessionno),
                    new SqlParameter("@P_SHIFTNO",shiftno),
                    new SqlParameter("@P_DEGREENO",degreeno),
                    new SqlParameter("@P_BRANCHNO",branchno),
                    new SqlParameter("@P_YEAR",year),
                };
                ds = objSqlHelper.ExecuteDataSetSP("PKG_SCHOLARSHIP_AMOUNT_UPDATE", param);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.ScholarshipFundController.GetStudForScholarshipUpdate() --> " + ex.Message + " " + ex.StackTrace);

            }
            return ds;
        }

        public DataSet GetScholarshipDetail(int idno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objHelper = new SQLHelper(_connectionString);
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@P_IDNO",idno),
                };
                ds = objHelper.ExecuteDataSetSP("PKG_SHOW_SCH_DETAIL", param);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.ScholarshipFundController.GetStudForScholarshipUpdate()--> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        /// <summary>
        /// TO UPDATE ADMISSION MODE OF STUDENTS
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="admmode"></param>
        /// <param name="remark"></param>
        /// <param name="uano"></param>
        /// <param name="ipaddress"></param>
        /// <returns></returns>
        public bool UpdateAdmissionMode(int studentId, int admMode, int branchNo,int admCategoryNo,int shift,DateTime admDate,string remark, int uano, string ipaddress)//.
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", studentId),
                    new SqlParameter("@P_CAP_INSTITUTE", admMode),
                    new SqlParameter("@P_BRANCHNO", branchNo),
                    new SqlParameter("@P_SHIFT", shift),
                    new SqlParameter("@P_ADM_CATEGORYNO", admCategoryNo),
                    new SqlParameter("@P_ADMDATE", admDate),
                    new SqlParameter("@P_REMARK", remark),
                    new SqlParameter("@P_UA_NO", uano),
                    new SqlParameter("@P_IPADDRESS", ipaddress),
                    
                };
                objDataAccess.ExecuteNonQuerySP("PROC_SCH_UPDATE_ADMISSIONMODE", sqlParams, false);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.FeeCollectionController.UpdateFeesPaymentType() --> " + ex.Message + " " + ex.StackTrace);
                return false;
            }
            return true;
        }

        public int UpdateScholarshipApprovalStatus(int idno, int yearno, int status, int concessionno, string reason)//.
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_YEARNO", yearno);
                objParams[2] = new SqlParameter("@P_STATUS", status);
                objParams[3] = new SqlParameter("@P_CONCESSIONNO", concessionno);
                objParams[4] = new SqlParameter("@P_REASON", reason);
                //if (objSQLHelper.ExecuteNonQuerySP("PROC_SCH_UPDATE_SCHOLARSHIP_APPROVAL", objParams, false) != null)
                if (objSQLHelper.ExecuteNonQuerySP("PROC_SCH_STUDENT_SCHOLARSHIP_APPROVAL", objParams, false) != null)
                    
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ScholarshipFundController.UpdateSchFund -> " + ex.ToString());
            }
            return retStatus;
        }
        /// <summary>
        /// STUDENT LIST FOR SCHOLARSHIP APPROVAL
        /// </summary>
        /// <param name="sessionno"></param>
        /// <param name="categoryno"></param>
        /// <param name="Degreeno"></param>
        /// <param name="year"></param>
        /// <param name="shiftno"></param>
        /// <param name="branchno"></param>
        /// <param name="admissionmode"></param>
        /// <returns></returns>
        public DataSet GetStudForScholarshipListReport(int sessionno, int categoryno, int Degreeno, int year, int shiftno, int branchno, int admissionmode)//.
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_connectionString);
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@P_SESSIONNO",sessionno),
                    new SqlParameter("@P_CATEGORYNO",categoryno),
                    new SqlParameter("@P_DEGREENO",Degreeno),
                    new SqlParameter("@P_YEAR",year),
                    new SqlParameter("@P_SHIFTNO",shiftno),
                    new SqlParameter("@P_BRANCHNO",branchno),
                    new SqlParameter("@P_ADMISSIONMODE",admissionmode),
                };
                ds = objSqlHelper.ExecuteDataSetSP("PKG_SCHOLARSHIP_STUDENT_LIST_REPORT", param);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.ScholarshipFundController.GetStudForScholarshipListReport() --> " + ex.Message + " " + ex.StackTrace);

            }
            return ds;
        }
        /// <summary>
        /// Get Students for College Concession
        /// </summary>
        /// <param name="sessionno"></param>
        /// <param name="categoryno"></param>
        /// <param name="Degreeno"></param>
        /// <param name="year"></param>
        /// <param name="shiftno"></param>
        /// <param name="branchno"></param>
        /// <param name="admissionmode"></param>
        /// <returns></returns>
        public DataSet GetStudForCollegeConcession(int sessionno, int categoryno, int Degreeno, int year, int shiftno, int branchno, int admissionmode)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_connectionString);
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@P_SESSIONNO",sessionno),
                    new SqlParameter("@P_CATEGORYNO",categoryno),
                    new SqlParameter("@P_DEGREENO",Degreeno),
                    new SqlParameter("@P_YEAR",year),
                    new SqlParameter("@P_SHIFTNO",shiftno),
                    new SqlParameter("@P_BRANCHNO",branchno),
                    new SqlParameter("@P_ADMISSIONMODE",admissionmode),
                };
                ds = objSqlHelper.ExecuteDataSetSP("PKG_SCH_GET_STUDENTS_FOR_COLLEGE_CONCESSION", param);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.ScholarshipFundController.GetStudForCollegeConcession() --> " + ex.Message + " " + ex.StackTrace);

            }
            return ds;
        }

        public int UpdateCollegeConcessionApprovalStatus(int idno, int yearno, int status, int concessionno, string reason)//.
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_YEARNO", yearno);
                objParams[2] = new SqlParameter("@P_STATUS", status);
                objParams[3] = new SqlParameter("@P_CONCESSIONNO", concessionno);
                objParams[4] = new SqlParameter("@P_REASON", reason);
                //if (objSQLHelper.ExecuteNonQuerySP("PROC_SCH_UPDATE_COLLEGE_CONCESSION_APPROVAL", objParams, false) != null)
                if (objSQLHelper.ExecuteNonQuerySP("PROC_SCH_STUDENT_CONCESSION_APPROVAL", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ScholarshipFundController.UpdateCollegeConcessionApprovalStatus -> " + ex.ToString());
            }
            return retStatus;
        }

        public int InsertCollegeConcessionAmount(int idnos, string regnos, int degreeno, int branchno, int admbatchno, int year, int concessionno, int bill_no, double schamt, int uano, string colcode, int categoryno, int shiftno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_IDNO", idnos),
                            new SqlParameter("@REGNO",regnos),
                            new SqlParameter("@P_DEGREENO", degreeno),
                            new SqlParameter("@P_BRANCHNO", branchno),
                            new SqlParameter("@P_ADMBATCH",admbatchno),
                            //new SqlParameter("@P_SEMESTERNO",semesterno),
                            new SqlParameter("@P_YEAR",year),
                            new SqlParameter("@P_CONCESSIONNO",concessionno),
                            new SqlParameter("@P_BILL_NO",bill_no),
                            new SqlParameter("@P_SCH_AMT",schamt),
                            new SqlParameter("@P_UANO",uano),
                            new SqlParameter("@P_COLLEGE_CODE",colcode),
                            new SqlParameter("@P_CATEGORYNO", categoryno),
                            new SqlParameter("@P_SHIFTNO", shiftno),
                            new SqlParameter("@P_OUT", SqlDbType.Int) 
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objDataAccess.ExecuteNonQuerySP("PKG_ACD_INSERT_COLLEGE_CONCESSION_AMOUNT", sqlParams, true);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else if (Convert.ToInt32(ret) == 1)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    retStatus = Convert.ToInt32(CustomStatus.Error);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ScholarshipFundController.InsertCollegeConcessionAmount-> " + ex.ToString());
            }

            return retStatus;
        }

        #region
        public int Sch_Con_Distribution(int idnos,int degreeno,int branchno,int admbatchno,int year,int shiftno,int categoryno,int concessionno,double schamt,int bill_no,DateTime billDate, int uano,string ipAddress, string colcode )//.
        {
            int retStatus = 0;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_IDNO", idnos),
                            new SqlParameter("@P_DEGREENO", degreeno),
                            new SqlParameter("@P_BRANCHNO", branchno),
                            new SqlParameter("@P_ADMBATCH",admbatchno),
                            new SqlParameter("@P_YEAR",year),
                            new SqlParameter("@P_SHIFTNO", shiftno),
                            new SqlParameter("@P_CATEGORYNO", categoryno),
                            new SqlParameter("@P_CONCESSIONNO",concessionno),
                            new SqlParameter("@P_SCH_AMT",schamt),
                            new SqlParameter("@P_BILL_NO",bill_no),
                            new SqlParameter("@P_BILL_DATE",billDate),
                            new SqlParameter("@P_UANO",uano),
                            new SqlParameter("@P_IPADDRESS",ipAddress),
                            new SqlParameter("@P_COLLEGE_CODE",colcode),
                            new SqlParameter("@P_OUT", SqlDbType.Int) 
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objDataAccess.ExecuteNonQuerySP("PROC_SCH_STUDENT_SCH_CON_AMOUNT_DISTRIBUTION", sqlParams, true);

                if (Convert.ToInt32(ret) == 1)
                    retStatus = 1;
                else if (Convert.ToInt32(ret) == 2)
                    retStatus = 2;
                else if (Convert.ToInt32(ret) == 3)
                    retStatus = 3;
                else
                    retStatus = 99;

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ScholarshipFundController.Sch_Con_Distribution-> " + ex.ToString());
            }

            return retStatus;
        }


        public SqlDataReader GetDistributedAmountDetails(int idno, int billNo, int concessionNo)//.
        {
            SqlDataReader dr = null;
            try
            {
                SQLHelper dataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlparams = new SqlParameter[]
                        {
                            new SqlParameter("@P_IDNO", idno),
                            new SqlParameter("@P_BILLNO", billNo),
                             new SqlParameter("@P_CONCESSIONNO", concessionNo)
                        };
                dr = dataAccess.ExecuteReaderSP("PKG_SCH_GET_DISTIBUTED_AMOUNT_DETAILS", sqlparams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ScholarshipFundController.GetDistributedAmountDetails() --> " + ex.Message + " " + ex.StackTrace);
            }
            return dr;
        }

        public int DeleteDistributedAmountEntry(int idno, int billNo, int concessionNo)//.
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;

                //Delete bos entry
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_BILLNO", billNo);
                objParams[2] = new SqlParameter("@P_CONCESSIONNO", concessionNo);
                objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SCH_DELETE_DISTRIBUTED_AMOUNT", objParams, true);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ScholarshipFundController.DeleteDistributedAmountEntry() -> " + ex.ToString());
            }

            return retStatus;
        }
        public DataSet GetStudentSch_ConcInfoById(int studentId)//.
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_IDNO", studentId)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_SCH_GET_SCH_CON_DETAILS", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ScholarshipFundController.GetStudentSch_ConcInfoById() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        #endregion

    }
}
