//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                        
// PAGE NAME     : FEES DEMAND MODIFICATION CONTROLLER
// CREATION DATE : 06-JUL-2009                                                        
// CREATED BY    : AMIT YADAV
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class DemandModificationController
    {
        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet GetAllDemands(int studentId, int demandNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_IDNO", studentId),
                    new SqlParameter("@P_DM_NO", demandNo)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_DEMAND_MOD_GET_ALL_DEMAND", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.GetAllDemands() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetDemand(int demandId, int studentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_DMNO", demandId),
                    new SqlParameter("@P_IDNO", studentId)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_DEMAND_MOD_GET_DEMAND", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.GetDemand() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public bool UpdateDemand(FeeDemand modifiedDemand)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                    
                    new SqlParameter("@P_IDNO", modifiedDemand.StudentId),                    
                    new SqlParameter("@P_F1", modifiedDemand.FeeHeads[0]),
                    new SqlParameter("@P_F2", modifiedDemand.FeeHeads[1]),
                    new SqlParameter("@P_F3", modifiedDemand.FeeHeads[2]),
                    new SqlParameter("@P_F4", modifiedDemand.FeeHeads[3]),
                    new SqlParameter("@P_F5", modifiedDemand.FeeHeads[4]),
                    new SqlParameter("@P_F6", modifiedDemand.FeeHeads[5]),
                    new SqlParameter("@P_F7", modifiedDemand.FeeHeads[6]),
                    new SqlParameter("@P_F8", modifiedDemand.FeeHeads[7]),
                    new SqlParameter("@P_F9", modifiedDemand.FeeHeads[8]),
                    new SqlParameter("@P_F10", modifiedDemand.FeeHeads[9]),
                    new SqlParameter("@P_F11", modifiedDemand.FeeHeads[10]),
                    new SqlParameter("@P_F12", modifiedDemand.FeeHeads[11]),
                    new SqlParameter("@P_F13", modifiedDemand.FeeHeads[12]),
                    new SqlParameter("@P_F14", modifiedDemand.FeeHeads[13]),
                    new SqlParameter("@P_F15", modifiedDemand.FeeHeads[14]),
                    new SqlParameter("@P_F16", modifiedDemand.FeeHeads[15]),
                    new SqlParameter("@P_F17", modifiedDemand.FeeHeads[16]),
                    new SqlParameter("@P_F18", modifiedDemand.FeeHeads[17]),
                    new SqlParameter("@P_F19", modifiedDemand.FeeHeads[18]),
                    new SqlParameter("@P_F20", modifiedDemand.FeeHeads[19]),
                    new SqlParameter("@P_F21", modifiedDemand.FeeHeads[20]),
                    new SqlParameter("@P_F22", modifiedDemand.FeeHeads[21]),
                    new SqlParameter("@P_F23", modifiedDemand.FeeHeads[22]),
                    new SqlParameter("@P_F24", modifiedDemand.FeeHeads[23]),
                    new SqlParameter("@P_F25", modifiedDemand.FeeHeads[24]),
                    new SqlParameter("@P_F26", modifiedDemand.FeeHeads[25]),
                    new SqlParameter("@P_F27", modifiedDemand.FeeHeads[26]),
                    new SqlParameter("@P_F28", modifiedDemand.FeeHeads[27]),
                    new SqlParameter("@P_F29", modifiedDemand.FeeHeads[28]),
                    new SqlParameter("@P_F30", modifiedDemand.FeeHeads[29]),
                    new SqlParameter("@P_F31", modifiedDemand.FeeHeads[30]),
                    new SqlParameter("@P_F32", modifiedDemand.FeeHeads[31]),
                    new SqlParameter("@P_F33", modifiedDemand.FeeHeads[32]),
                    new SqlParameter("@P_F34", modifiedDemand.FeeHeads[33]),
                    new SqlParameter("@P_F35", modifiedDemand.FeeHeads[34]),
                    new SqlParameter("@P_F36", modifiedDemand.FeeHeads[35]),
                    new SqlParameter("@P_F37", modifiedDemand.FeeHeads[36]),
                    new SqlParameter("@P_F38", modifiedDemand.FeeHeads[37]),
                    new SqlParameter("@P_F39", modifiedDemand.FeeHeads[38]),
                    new SqlParameter("@P_F40", modifiedDemand.FeeHeads[39]),
                   
                    new SqlParameter("@P_TOTAL_AMT", modifiedDemand.TotalFeeAmount),
                    new SqlParameter("@P_PARTICULAR", modifiedDemand.Remark),
                    new SqlParameter("@P_IPADDRESS", modifiedDemand.IpAddress ),
                    new SqlParameter("@P_UA_NO", modifiedDemand.UANO),
                    new SqlParameter("@P_DM_NO", modifiedDemand.DemandId)

                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                modifiedDemand.DemandId = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_DEMAND_MOD_UPDT_DEMAND", sqlParams, true);

                if (modifiedDemand.DemandId == -99)
                    return false;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.UpdateDemand() --> " + ex.Message + " " + ex.StackTrace);
            }
            return true;
        }

        public string CreateDemandForStudents(FeeDemand demandCriteria, int selectSemesterNo, bool overwrite)
        {
            string strOutput = "0";
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_RECIEPTCODE", demandCriteria.ReceiptTypeCode),
                    new SqlParameter("@P_BRANCHNO", demandCriteria.BranchNo),                    
                    new SqlParameter("@P_SELECT_SEMESTERNO", selectSemesterNo),
                    new SqlParameter("@P_FOR_SEMESTERNO", demandCriteria.SemesterNo),
                    new SqlParameter("@P_PAYMENTTYPE", demandCriteria.PaymentTypeNo),
                    new SqlParameter("@P_OVERWRITE", overwrite),
                    new SqlParameter("@P_UA_NO", demandCriteria.UserNo),
                    new SqlParameter("@P_COLLEGE_CODE", demandCriteria.CollegeCode),
                    new SqlParameter("@P_OUTPUT", strOutput)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
                object output = objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_CREATEDEMAND_BULK", sqlParams, true);

                if (output != null && output.ToString() == "-99")
                    return "-99";
                else
                    strOutput = output.ToString();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.CreateDemandForStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return strOutput;
        }

        public string CreateDemandForSelectedStudents(string studentIDs, FeeDemand demandCriteria, int selectSemesterNo, bool overwrite, int College_Id)
        {
            string strOutput = "0";
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_SESSION_NO", demandCriteria.SessionNo),
                    new SqlParameter("@P_STUDENT_NOS", studentIDs),
                    new SqlParameter("@P_RECIEPTCODE", demandCriteria.ReceiptTypeCode),
                    new SqlParameter("@P_BRANCHNO", demandCriteria.BranchNo),                    
                    new SqlParameter("@P_SELECT_SEMESTERNO", selectSemesterNo),
                    new SqlParameter("@P_FOR_SEMESTERNO", demandCriteria.SemesterNo),
                    new SqlParameter("@P_PAYMENTTYPE", demandCriteria.PaymentTypeNo),
                    new SqlParameter("@P_OVERWRITE", overwrite),
                    new SqlParameter("@P_UA_NO", demandCriteria.UserNo),
                    new SqlParameter("@P_COLLEGE_CODE", demandCriteria.CollegeCode),
                    new SqlParameter("@P_COLLEGE_ID", College_Id),
                    new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),
                    new SqlParameter("@P_OUTPUT", strOutput)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
                object output = objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_CREATE_DEMAND_BULK_FOR_SELECTED_STUDENTS", sqlParams, true);

                ////if (output != null && output.ToString() == "-99")
                ////    return "-99";
                ////else
                ////    strOutput = output.ToString();
                if (output != null && output.ToString() == "-99")
                    return "-99";
                else if (output.ToString() == "5")
                    return "5";
                else
                    strOutput = output.ToString();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.CreateDemandForStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return strOutput;
        }
        public string CreateDemandForStudents(string studentIDs, FeeDemand demandCriteria, int selectSemesterNo, bool overwrite)
        {
            string strOutput = "0";
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_SESSION_NO", demandCriteria.SessionNo),
                    new SqlParameter("@P_STUDENT_NOS", studentIDs),
                    new SqlParameter("@P_RECIEPTCODE", demandCriteria.ReceiptTypeCode),
                    new SqlParameter("@P_BRANCHNO", demandCriteria.BranchNo),                    
                    new SqlParameter("@P_SELECT_SEMESTERNO", selectSemesterNo),
                    new SqlParameter("@P_FOR_SEMESTERNO", demandCriteria.SemesterNo),
                    new SqlParameter("@P_PAYMENTTYPE", demandCriteria.PaymentTypeNo),
                    new SqlParameter("@P_OVERWRITE", overwrite),
                    new SqlParameter("@P_UA_NO", demandCriteria.UserNo),
                    new SqlParameter("@P_COLLEGE_CODE", demandCriteria.CollegeCode),
                    new SqlParameter("@P_OUTPUT", strOutput)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
                object output = objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_CREATE_DEMAND_BULK_FOR_SELECTED_STUDENTS_OLD", sqlParams, true);

                if (output != null && output.ToString() == "-99")
                    return "-99";
                else
                    strOutput = output.ToString();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.CreateDemandForStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return strOutput;
        }




        public DataSet GetStudentsForDemandCreation(int DEGREENO, int branchNo, int semNo, int semesterfor, int paymentTypeNo, int College_Id)
            {
            DataSet ds = null;
            try
                {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_DEGREENO", DEGREENO),
                    new SqlParameter("@P_BRANCHNO", branchNo),
                    new SqlParameter("@P_SEMESTERNO", semNo),
                    new SqlParameter("@P_FOR_SEMESTERNO", semesterfor),
                    new SqlParameter("@P_PAYMENT_TYPE_NO", paymentTypeNo),
                    new SqlParameter("@P_COLLEGE_ID", College_Id)
         
                    
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_GET_STUDENTS_FOR_DEMAND_CREATION", sqlParams);
                }
            catch (Exception ex)
                {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.GetStudentsForDemandCreation() --> " + ex.Message + " " + ex.StackTrace);
                }
            return ds;
            }

        public DataSet GetHostelerStudents(string hostelRoomCapacity)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("@P_ROOM_CAPACITY", hostelRoomCapacity) 
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_GET_HOSTELER_STUDENT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.GetHostelerStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public string btnCreateDemandForSelStuds_Click(FeeDemand demandCriteria, string roomCapacity, bool overwrite)
        {
            string strOutput = "0";
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_RECIEPTCODE", demandCriteria.ReceiptTypeCode),
                    new SqlParameter("@P_ROOM_CAPACITY", roomCapacity),
                    new SqlParameter("@P_OVERWRITE", overwrite),
                    new SqlParameter("@P_UA_NO", demandCriteria.UserNo),
                    new SqlParameter("@P_COLLEGE_CODE", demandCriteria.CollegeCode),
                    new SqlParameter("@P_OUTPUT", strOutput)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
                object output = objDataAccess.ExecuteNonQuerySP("PKG_HOSTEL_CREATE_DEMAND_BULK", sqlParams, true);

                if (output != null && output.ToString() == "-99")
                    return "-99";
                else
                    strOutput = output.ToString();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.CreateDemandForStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return strOutput;
        }

        public string CreateHostelFeeDemand(FeeDemand demandCriteria, int sessionno, bool overwrite, string studentIDs, int ForSemester, int PayType)
        {
            string strOutput = "0";
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_STUDENT_NO", studentIDs),
                    new SqlParameter("@P_RECIEPTCODE", demandCriteria.ReceiptTypeCode),
                    new SqlParameter("@P_SESSIONNO", sessionno),
                    new SqlParameter("@P_OVERWRITE", overwrite),
                    new SqlParameter("@P_UA_NO", demandCriteria.UserNo),
                    new SqlParameter("@P_COLLEGE_CODE", demandCriteria.CollegeCode),
                    //new SqlParameter("@P_COLLEGE_ID", collegeid),
                    new SqlParameter("@P_SEMESTERNO", demandCriteria.SemesterNo),
                    new SqlParameter("@P_FORSEMESTERNO", ForSemester),
                    new SqlParameter("@P_PAY_FULL_HALF", PayType), // ADDED PATMENT TYPE WHICH NOT PASS BY SHUBHAM ON 06/06/22
                    new SqlParameter("@P_OUTPUT", strOutput)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                sqlParams[sqlParams.Length - 1].DbType = DbType.String;
                object output = objDataAccess.ExecuteNonQuerySP("PKG_HOSTEL_CREATE_DEMAND_BULK_BY_ID", sqlParams, true);

                if (output != null && output.ToString() == "-99")
                    return "-99";
                else
                    strOutput = output.ToString();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.CreateDemandForStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return strOutput;
        }

        public DataSet GetApplyHostelerStudents(int sessionno, int degreeno, int branchno, int semesterno, string reciept_code, int forSemester)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] { 
                    new SqlParameter("@P_SESSIONNO", sessionno),
                    new SqlParameter("@P_DEGREENO", degreeno),
                    new SqlParameter("@P_BRANCHNO", branchno),
                    new SqlParameter("@P_SEMESTERNO", semesterno),
                    new SqlParameter("@P_RECIEPT_CODE", reciept_code),
                    new SqlParameter("@P_FORSEMESTER", forSemester),
                    new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]))
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_GET_HOSTELER_STUDENT", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.GetHostelerStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public string CreateDcrForStudents(string studentIDs, FeeDemand dcr, int selectSemesterNo, bool overwrite, string receiptno)
        {
            string strOutput = "0";
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_SESSION_NO", dcr.SessionNo),
                    new SqlParameter("@P_STUDENT_NOS", studentIDs),
                    new SqlParameter("@P_RECIEPTCODE", dcr.ReceiptTypeCode),
                    new SqlParameter("@P_BRANCHNO", dcr.BranchNo),                    
                    new SqlParameter("@P_SELECT_SEMESTERNO", selectSemesterNo),
                    new SqlParameter("@P_FOR_SEMESTERNO", dcr.SemesterNo),
                    new SqlParameter("@P_PAYMENTTYPE", dcr.PaymentTypeNo),
                    new SqlParameter("@P_OVERWRITE", overwrite),
                    new SqlParameter("@P_UA_NO", dcr.UserNo),
                    new SqlParameter("@P_RECIEPTNO",receiptno),
                    new SqlParameter("@P_EXCESS_AMT",dcr.ExcessAmount),
                    new SqlParameter("@P_COLLEGE_CODE", dcr.CollegeCode),
                    new SqlParameter("@P_OUTPUT", strOutput)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
                object output = objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_CREATE_DCR_FOR_SELECTED_STUDENTS", sqlParams, true);

                if (output != null && output.ToString() == "-99")
                    return "-99";
                else
                    strOutput = output.ToString();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.CreateDemandForStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return strOutput;
        }

        //public string InserDDData(int dcrNo, int studentIDs, string ddno, DateTime date, int bankno, string bankname, string amount, string city, string collegecode)
        //{
        //    string strOutput = "0";
        //    try
        //    {
        //        SQLHelper objDataAccess = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[] 
        //        {
        //           new SqlParameter("@P_DCRNO",dcrNo),
        //           new SqlParameter("@P_IDNO", studentIDs),
        //           new SqlParameter("@P_DDNO", ddno),
        //           new SqlParameter("@P_DDDATE", date),
        //           new SqlParameter("@P_BANKNO", bankno),
        //           new SqlParameter("@P_BANKNAME", bankname),
        //           new SqlParameter("@P_DDAMOUNT", amount),
        //           new SqlParameter("@P_CITY", city),
        //           new SqlParameter("@P_COLLEGE_CODE", collegecode),
        //           new SqlParameter("@P_DCRTRNO", strOutput)
        //        };
        //        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
        //        sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
        //        object output = objDataAccess.ExecuteNonQuerySP("PKG_ACAD_INS_ADM_DDINFO", sqlParams, true);

        //        if (output != null && output.ToString() == "-99")
        //            return "-99";
        //        else
        //            strOutput = output.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.CreateDemandForStudents() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return strOutput;
        //}

        public int InserDDData(int dcrNo, int studentIDs, string ddno, DateTime date, int bankno, string bankname, string amount, string city, string collegecode)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[] 
                        { 
                           new SqlParameter("@P_DCRNO",dcrNo),
                           new SqlParameter("@P_IDNO", studentIDs),
                           new SqlParameter("@P_DDNO", ddno),
                           new SqlParameter("@P_DDDATE", date),
                           new SqlParameter("@P_BANKNO", bankno),
                           new SqlParameter("@P_BANKNAME", bankname),
                           new SqlParameter("@P_DDAMOUNT", amount),
                           new SqlParameter("@P_CITY", city),
                           new SqlParameter("@P_COLLEGE_CODE", collegecode),
                           new SqlParameter("@P_DCRTRNO", SqlDbType.Int)
                         };
                objParams[objParams.Length - 1].Direction = ParameterDirection.Output;
                //objSQLHelper.ExecuteNonQuerySP("PKG_SALE_PROSPECTUS_INSERT", objParams, false);

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INS_ADM_DDINFO", objParams, true);
                retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddProspectusSaleStudent --> " + ex.ToString());
            }
            return retStatus;
        }

        public string CreateDcrForBacklogStudents(int studentIDs, FeeDemand dcr, int selectSemesterNo, bool overwrite, string receiptno, double ExamAmt, double LateExamFees)
        {
            string strOutput = "0";
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_SESSION_NO", dcr.SessionNo),
                    new SqlParameter("@P_STUDENT_NOS", studentIDs),
                    new SqlParameter("@P_RECIEPTCODE", dcr.ReceiptTypeCode),
                    new SqlParameter("@P_BRANCHNO", dcr.BranchNo),                    
                    new SqlParameter("@P_SELECT_SEMESTERNO", selectSemesterNo),
                    new SqlParameter("@P_FOR_SEMESTERNO", dcr.SemesterNo),
                    new SqlParameter("@P_PAYMENTTYPE", dcr.PaymentTypeNo),
                    new SqlParameter("@P_OVERWRITE", overwrite),
                    new SqlParameter("@P_UA_NO", dcr.UserNo),
                    new SqlParameter("@P_RECIEPTNO",receiptno),
                    new SqlParameter("@P_EXAMAMOUNT",ExamAmt),
                    new SqlParameter("@P_LATEEXAMAMOUNT",LateExamFees),
                    new SqlParameter("@P_COLLEGE_CODE", dcr.CollegeCode),
                    new SqlParameter("@P_OUTPUT", strOutput)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
                object output = objDataAccess.ExecuteNonQuerySP("PKG_BACKLOG_EXAMFEECOLLECT_CREATE_DCR_FOR_SELECTED_STUDENTS", sqlParams, true);

                if (output != null && output.ToString() == "-99")
                    return "-99";
                else
                    strOutput = output.ToString();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.CreateDemandForStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return strOutput;
        }

        public int UpdateDemandLateFees(int idno, int semesterno, double AdmLateFess)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTER", semesterno);
                objParams[2] = new SqlParameter("@P_ADM_LATE_FEES", AdmLateFess);

                if (objSQLHelper.ExecuteNonQuerySP("PKG_ADM_LATE_FEES_FOR_DEMAND", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.UpdateDemandLateFees -> " + ex.ToString());
            }
            return retStatus;
        }

        public int UpdateDcrLateFees(int idno, int semesterno, double AdmLateFess)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SEMESTER", semesterno);
                objParams[2] = new SqlParameter("@P_ADM_LATE_FEES", AdmLateFess);

                if (objSQLHelper.ExecuteNonQuerySP("PKG_ADM_LATE_FEES_FOR_DCR", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.UpdateDcrLateFees -> " + ex.ToString());
            }
            return retStatus;
        }

        public int InserDDDataSemPramote(int dcrNo, int studentIDs, string ddno, DateTime date, int bankno, string bankname, string amount, string city, string collegecode, int sessionno, int semesterno, string paytype, string ddremark)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[] 
                        { 
                           new SqlParameter("@P_DCRNO",dcrNo),
                           new SqlParameter("@P_IDNO", studentIDs),
                           new SqlParameter("@P_DDNO", ddno),
                           new SqlParameter("@P_DDDATE", date),
                           new SqlParameter("@P_BANKNO", bankno),
                           new SqlParameter("@P_BANKNAME", bankname),
                           new SqlParameter("@P_DDAMOUNT", amount),
                           new SqlParameter("@P_CITY", city),
                           new SqlParameter("@P_COLLEGE_CODE", collegecode),
                           new SqlParameter("@P_SESSIONNO", sessionno),
                           new SqlParameter("@P_SEMESTERNO", semesterno),
                           new SqlParameter("@P_PAY_TYPE", paytype),
                           new SqlParameter("@P_REMARK", ddremark),
                           new SqlParameter("@P_DCRTRNO", SqlDbType.Int)
                         };
                objParams[objParams.Length - 1].Direction = ParameterDirection.Output;
                //objSQLHelper.ExecuteNonQuerySP("PKG_SALE_PROSPECTUS_INSERT", objParams, false);

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INS_ADM_DDINFO_SEMPRAMOTE", objParams, true);
                retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddProspectusSaleStudent --> " + ex.ToString());
            }
            return retStatus;
        }
        /// <summary>
        /// ADDED BY: M. REHBAR SHEIKH ON 10-07-2019
        /// </summary>
        public string CancelDemandForSelectedStudent_ByBranchChange(int studentID, FeeDemand demandCriteria, int selectSemesterNo,
                                                                    bool overwrite, int College_Id, int branch, int newbranch, int olddegereeno, int newdegreeno, decimal excessAmt)
        {
            string strOutput = "0";
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_SESSION_NO", demandCriteria.SessionNo),
                    new SqlParameter("@P_IDNO", studentID),
                    new SqlParameter("@P_RECIEPTCODE", demandCriteria.ReceiptTypeCode),
                    new SqlParameter("@P_DEGREENO", olddegereeno),  
                    new SqlParameter("@P_NEWDEGREENO",newdegreeno),
                    new SqlParameter("@P_BRANCHNO", branch),        
                    new SqlParameter("@P_NEWBRANCHNO", newbranch),        
                    new SqlParameter("@P_SEMESTERNO", selectSemesterNo),
                    new SqlParameter("@P_ADMBATCHNO", demandCriteria.AdmBatchNo),
                    new SqlParameter("@P_PAYMENTTYPE", demandCriteria.PaymentTypeNo),
                    new SqlParameter("@P_UA_NO", demandCriteria.UserNo),
                    new SqlParameter("@P_COLLEGE_CODE", demandCriteria.CollegeCode),
                    new SqlParameter("@P_EXCESSAMOUNT", excessAmt),
                    new SqlParameter("@P_OUTPUT", strOutput)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
                object output = objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_CANCEL_DEMAND_BY_BRANCHCHANGE", sqlParams, true);

                ////if (output != null && output.ToString() == "-99")
                ////    return "-99";
                ////else
                ////    strOutput = output.ToString();
                if (output != null && output.ToString() == "-99")
                    return "-99";
                else if (output.ToString() == "2")
                    return "2";
                else
                    strOutput = output.ToString();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.CancelDemandForSelectedStudent_ByBranchChange() --> " + ex.Message + " " + ex.StackTrace);
            }
            return strOutput;
        }

        /// <summary>
        /// ADDED BY: M. REHBAR SHEIKH ON 10-07-2019
        /// </summary>
        public string CreateDemandForSelectedStudents_ByBranchChange(string studentIDs, FeeDemand demandCriteria, int selectSemesterNo, bool overwrite, int College_Id, int degreeno)
        {
            string strOutput = "0";
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_SESSION_NO", demandCriteria.SessionNo),
                    new SqlParameter("@P_STUDENT_NOS", studentIDs),
                    new SqlParameter("@P_RECIEPTCODE", demandCriteria.ReceiptTypeCode),
                    new SqlParameter("@P_BRANCHNO", demandCriteria.BranchNo),  
                    new SqlParameter("@P_DEGREENO",degreeno),
                    new SqlParameter("@P_SELECT_SEMESTERNO", selectSemesterNo),
                    new SqlParameter("@P_FOR_SEMESTERNO", demandCriteria.SemesterNo),
                    new SqlParameter("@P_PAYMENTTYPE", demandCriteria.PaymentTypeNo),
                    new SqlParameter("@P_UA_NO", demandCriteria.UserNo),
                    new SqlParameter("@P_COLLEGE_CODE", demandCriteria.CollegeCode),
                    new SqlParameter("@P_OVERWRITE", overwrite),
                    new SqlParameter("@P_COLLEGE_ID", College_Id),
                    new SqlParameter("@P_OUTPUT", strOutput)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
                object output = objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_CREATE_DEMAND_BULK_FOR_SELECTED_STUDENTS_BY_BRANCHCHANGE", sqlParams, true);

                ////if (output != null && output.ToString() == "-99")
                ////    return "-99";
                ////else
                ////    strOutput = output.ToString();
                if (output != null && output.ToString() == "-99")
                    return "-99";
                else if (output.ToString() == "5")
                    return "5";
                else
                    strOutput = output.ToString();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.CreateDemandForStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return strOutput;
        }
        /// <summary>
        /// ADDED BY: M. REHBAR SHEIKH ON 12-07-2019
        /// </summary>
        public string RollBackDemandForSelectedStudent_ByBranchChange(int studentID, FeeDemand demandCriteria, int selectSemesterNo,
                                                                    bool overwrite, int College_Id, int branch, int newbranch, decimal excessAmt, int newdegreeno)
        {
            string strOutput = "0";
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_SESSION_NO", demandCriteria.SessionNo),
                    new SqlParameter("@P_IDNO", studentID),
                    new SqlParameter("@P_RECIEPTCODE", demandCriteria.ReceiptTypeCode),
                    new SqlParameter("@P_DEGREENO", demandCriteria.DegreeNo), 
                     new SqlParameter("@P_NEWDEGREENO",newdegreeno), 
                    new SqlParameter("@P_BRANCHNO", branch),        
                    new SqlParameter("@P_NEWBRANCHNO", newbranch),        
                    new SqlParameter("@P_SEMESTERNO", selectSemesterNo),
                    new SqlParameter("@P_ADMBATCHNO", demandCriteria.AdmBatchNo),
                    new SqlParameter("@P_PAYMENTTYPE", demandCriteria.PaymentTypeNo),
                    new SqlParameter("@P_UA_NO", demandCriteria.UserNo),
                    new SqlParameter("@P_COLLEGE_CODE", demandCriteria.CollegeCode),
                    new SqlParameter("@P_EXCESSAMOUNT", excessAmt),
                    new SqlParameter("@P_OUTPUT", strOutput)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
                object output = objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_ROLLBACK_DEMAND_BY_BRANCHCHANGE", sqlParams, true);

                ////if (output != null && output.ToString() == "-99")
                ////    return "-99";
                ////else
                ////    strOutput = output.ToString();
                if (output != null && output.ToString() == "-99")
                    return "-99";
                else if (output.ToString() == "2")
                    return "2";
                else
                    strOutput = output.ToString();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.RollBackDemandForSelectedStudent_ByBranchChange() --> " + ex.Message + " " + ex.StackTrace);
            }
            return strOutput;
        }

        //Added by Rita M.....

        public string CancelDemandForSelectedStudent_ByBranchChange_WithoutFees(int studentID, FeeDemand demandCriteria)
        {
            string strOutput = "0";
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_SESSION_NO", demandCriteria.SessionNo),
                    new SqlParameter("@P_IDNO", studentID),
                    new SqlParameter("@P_RECIEPTCODE", demandCriteria.ReceiptTypeCode),
                    new SqlParameter("@P_DEGREENO", demandCriteria.DegreeNo),                  
                    new SqlParameter("@P_BRANCHNO", demandCriteria.BranchNo),                             
                    new SqlParameter("@P_SEMESTERNO", demandCriteria.SemesterNo),
                    new SqlParameter("@P_ADMBATCHNO", demandCriteria.AdmBatchNo),
                    new SqlParameter("@P_PAYMENTTYPE", demandCriteria.PaymentTypeNo),
                    new SqlParameter("@P_UA_NO", demandCriteria.UserNo),
                    new SqlParameter("@P_COLLEGE_CODE", demandCriteria.CollegeCode),
                  
                    new SqlParameter("@P_OUTPUT", strOutput)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
                object output = objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_CANCEL_DEMAND_BY_BRANCHCHANGE_WITHOUTFEES", sqlParams, true);

                ////if (output != null && output.ToString() == "-99")
                ////    return "-99";
                ////else
                ////    strOutput = output.ToString();
                if (output != null && output.ToString() == "-99")
                    return "-99";
                else if (output.ToString() == "2")
                    return "2";
                else
                    strOutput = output.ToString();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.CancelDemandForSelectedStudent_ByBranchChange() --> " + ex.Message + " " + ex.StackTrace);
            }
            return strOutput;
        }

        /// <summary>
        /// ADDED BY: Rita M.....03/07/2019.....
        /// </summary>
        public string CreateDemandForSelectedStudents_ForPaymentModification(string studentIDs, FeeDemand demandCriteria, int selectSemesterNo, bool overwrite, int College_Id, int newpaytype)
        {
            string strOutput = "0";
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_SESSION_NO", demandCriteria.SessionNo),
                    new SqlParameter("@P_STUDENT_NOS", studentIDs),
                    new SqlParameter("@P_RECIEPTCODE", demandCriteria.ReceiptTypeCode),
                    new SqlParameter("@P_BRANCHNO", demandCriteria.BranchNo),  
                   // new SqlParameter("@P_DEGREENO",demandCriteria.DegreeNo),
                    new SqlParameter("@P_SELECT_SEMESTERNO", selectSemesterNo),
                    new SqlParameter("@P_FOR_SEMESTERNO", demandCriteria.SemesterNo),
                    new SqlParameter("@P_PAYMENTTYPE", demandCriteria.PaymentTypeNo),
                    new SqlParameter("@P_UA_NO", demandCriteria.UserNo),
                    new SqlParameter("@P_COLLEGE_CODE", demandCriteria.CollegeCode),
                    new SqlParameter("@P_OVERWRITE", overwrite),
                    new SqlParameter("@P_COLLEGE_ID", College_Id),
                    new SqlParameter("@P_NEWPAYMENTTYPE", newpaytype),
                    new SqlParameter("@P_OUTPUT", strOutput)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
                object output = objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_CREATE_DEMAND_BULK_FOR_SELECTED_STUDENTS_FOR_PAYTYPE_MODIFICATION", sqlParams, true);

                ////if (output != null && output.ToString() == "-99")
                ////    return "-99";
                ////else
                ////    strOutput = output.ToString();
                if (output != null && output.ToString() == "-99")
                    return "-99";
                else if (output.ToString() == "5")
                    return "5";
                else
                    strOutput = output.ToString();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.CreateDemandForStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return strOutput;
        }


        /// <summary>
        /// ADDED BY: Rita M.....06/08/2019.....
        /// </summary>
        public string CreateDemandForSelectedStudent_ForTourForm(string studentIDs, int sessionno, string ReceiptCode, int uano, string collegecode, bool overwrite)
        {
            string strOutput = "0";
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_SESSION_NO",sessionno),
                    new SqlParameter("@P_STUDENT_NOS", studentIDs),
                    new SqlParameter("@P_RECIEPTCODE",ReceiptCode),
                    //new SqlParameter("@P_BRANCHNO", demandCriteria.BranchNo),  
                   // new SqlParameter("@P_DEGREENO",demandCriteria.DegreeNo),
                    //new SqlParameter("@P_SELECT_SEMESTERNO", selectSemesterNo),
                    //new SqlParameter("@P_FOR_SEMESTERNO", demandCriteria.SemesterNo),
                    //new SqlParameter("@P_PAYMENTTYPE", demandCriteria.PaymentTypeNo),
                    new SqlParameter("@P_UA_NO",uano),
                    new SqlParameter("@P_COLLEGE_CODE", collegecode),
                    new SqlParameter("@P_OVERWRITE", overwrite),
                    //new SqlParameter("@P_COLLEGE_ID", College_Id),
                    new SqlParameter("@P_OUTPUT", strOutput)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
                object output = objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_CREATE_DEMAND_BULK_FOR_SELECTED_STUDENTS_FOR_TOUR_FORM", sqlParams, true);

                ////if (output != null && output.ToString() == "-99")
                ////    return "-99";
                ////else
                ////    strOutput = output.ToString();
                if (output != null && output.ToString() == "-99")
                    return "-99";
                else if (output.ToString() == "5")
                    return "5";
                else
                    strOutput = output.ToString();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.CreateDemandForStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return strOutput;
        }

        ///Added By S.patil - 16102019

        public string CreateDemandForExamFeesBulk(string studentIDs, FeeDemand demandCriteria, int selectSemesterNo, bool overwrite, int College_Id)
        {
            string strOutput = "0";
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_SESSION_NO", demandCriteria.SessionNo),
                    new SqlParameter("@P_STUDENT_NOS", studentIDs),
                    new SqlParameter("@P_RECIEPTCODE", demandCriteria.ReceiptTypeCode),
                    new SqlParameter("@P_BRANCHNO", demandCriteria.BranchNo),                    
                    new SqlParameter("@P_SELECT_SEMESTERNO", selectSemesterNo),
                    new SqlParameter("@P_FOR_SEMESTERNO", demandCriteria.SemesterNo),
                    //new SqlParameter("@P_PAYMENTTYPE", demandCriteria.PaymentTypeNo),
                    new SqlParameter("@P_OVERWRITE", overwrite),
                    new SqlParameter("@P_UA_NO", demandCriteria.UserNo),
                    new SqlParameter("@P_COLLEGE_CODE", demandCriteria.CollegeCode),
                    new SqlParameter("@P_COLLEGE_ID", College_Id),
                    new SqlParameter("@P_OUTPUT", strOutput)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
                object output = objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_CREATE_DEMAND_FOR_BULK_EXM_REG", sqlParams, true);

                ////if (output != null && output.ToString() == "-99")
                ////    return "-99";
                ////else
                ////    strOutput = output.ToString();
                if (output != null && output.ToString() == "-99")
                    return "-99";
                else if (output.ToString() == "5")
                    return "5";
                else
                    strOutput = output.ToString();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.CreateDemandForStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return strOutput;
        }

        /// <summary>
        /// Added By S.Patil
        /// </summary>
        /// <param name="studentIDs"></param>
        /// <param name="demandCriteria"></param>
        /// <returns></returns>
        public string CreateDemandForAttendnaceFine(int studentIDs, FeeDemand demandCriteria, string fineamt, int semno)
        {
            string strOutput = "0";
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_SESSION_NO", demandCriteria.SessionNo),
                    new SqlParameter("@P_STUDENT_NOS", studentIDs),
                    new SqlParameter("@P_RECIEPTCODE", demandCriteria.ReceiptTypeCode),                   
                    new SqlParameter("@P_FOR_SEMESTERNO", semno),
                    new SqlParameter("@P_PAYMENTTYPE", demandCriteria.PaymentTypeNo),
                    new SqlParameter("@P_UA_NO", demandCriteria.UserNo),
                    new SqlParameter("@P_COLLEGE_CODE", demandCriteria.CollegeCode),
                    new SqlParameter("@P_FINE_AMT", fineamt),
                    new SqlParameter("@P_OUTPUT", strOutput)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
                object output = objDataAccess.ExecuteNonQuerySP("PKG_CREATE_DEMAND_ATT_FINE", sqlParams, true);

                ////if (output != null && output.ToString() == "-99")
                ////    return "-99";
                ////else
                ////    strOutput = output.ToString();
                if (output != null && output.ToString() == "-99")
                    return "-99";
                else if (output.ToString() == "1")
                    return "1";
                else
                    strOutput = output.ToString();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.CreateDemandForStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return strOutput;
        }

        //Added by Deepali On 01/10/2020 For Deleting the Demand
        public bool DeleteDemand(FeeDemand deleteDemand)
        {
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                    
                    new SqlParameter("@P_IDNO", deleteDemand.StudentId),
                    new SqlParameter("@P_IPADDRESS", deleteDemand.IpAddress ),
                    new SqlParameter("@P_UA_NO", deleteDemand.UANO),
                    new SqlParameter("@P_DM_NO", deleteDemand.DemandId)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                deleteDemand.DemandId = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_DELETE_DEMAND", sqlParams, true);

                if (deleteDemand.DemandId == -99)
                    return false;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.UpdateDemand() --> " + ex.Message + " " + ex.StackTrace);
            }
            return true;
        }

        public DataSet GetDemandsforCancellation(int studentId, int demandNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_IDNO", studentId),
                    new SqlParameter("@P_DM_NO", demandNo)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_DEMAND_MOD_GET_ALL_DEMAND_CANCEL", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.GetAllDemands() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //ADDED BY SAFAL GUPTA ON 003022021
        public int CreateDemandForSelectedStudents_ByBranchChangeNew(string studentIDs, FeeDemand demandCriteria, int selectSemesterNo, bool overwrite, int College_Id, int degreeno)
        {
            int iOutput = 0;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_SESSION_NO", demandCriteria.SessionNo),
                    new SqlParameter("@P_STUDENT_NOS", studentIDs),
                    new SqlParameter("@P_RECIEPTCODE", demandCriteria.ReceiptTypeCode),
                    new SqlParameter("@P_BRANCHNO", demandCriteria.BranchNo),  
                    new SqlParameter("@P_DEGREENO",degreeno),
                    new SqlParameter("@P_SELECT_SEMESTERNO", selectSemesterNo),
                    new SqlParameter("@P_FOR_SEMESTERNO", demandCriteria.SemesterNo),
                    new SqlParameter("@P_PAYMENTTYPE", demandCriteria.PaymentTypeNo),
                    new SqlParameter("@P_UA_NO", demandCriteria.UserNo),
                    new SqlParameter("@P_COLLEGE_CODE", demandCriteria.CollegeCode),
                    new SqlParameter("@P_OVERWRITE", overwrite),                    
                    new SqlParameter("@P_COLLEGE_ID", College_Id),
                    new SqlParameter("@P_OUTPUT", iOutput)
                    
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.BigInt;
                object output = objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_CREATE_DEMAND_BULK_FOR_SELECTED_STUDENTS_BY_BRANCHCHANGE", sqlParams, true);

                if (output != null && Convert.ToInt32(output) == -99)
                    return -99;
                else if (Convert.ToInt32(output) == -5)
                    return -5;
                else
                    iOutput = Convert.ToInt32(output);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.CreateDemandForStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return iOutput;
        }

        public string CreateDemandForExcelUploadedStudents(FeeDemand demandCriteria, int status)
        {
            string strOutput = "0";
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_SESSION_NO", demandCriteria.SessionNo),
                    new SqlParameter("@P_IDNO", demandCriteria.StudentId),
                    new SqlParameter("@P_RECIEPTCODE", demandCriteria.ReceiptTypeCode), 
                    new SqlParameter("@P_STATUS", status),
                    new SqlParameter("@P_UA_NO", demandCriteria.UANO),
                    new SqlParameter("@P_COLLEGE_CODE", demandCriteria.CollegeCode),
                    new SqlParameter("@P_PTYPE", demandCriteria.PaymentTypeNo),
                    new SqlParameter("@P_OUTPUT", strOutput)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
                object output = objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_CREATE_DEMAND_FOR_EXCEL_UPLOADED_STUDENTS", sqlParams, true);

                ////if (output != null && output.ToString() == "-99")
                ////    return "-99";
                ////else
                ////    strOutput = output.ToString();
                if (output != null && output.ToString() == "-99")
                    return "-99";
                else if (output.ToString() == "5")
                    return "5";
                else
                    strOutput = output.ToString();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.CreateDemandForStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return strOutput;
        }
        /// <summary>
        /// Added by S.Patil - 05 jan 22
        /// </summary>
        /// <param name="rec"></param>
        /// <param name="brno"></param>
        /// <param name="sem"></param>
        /// <param name="ptype"></param>
        /// <param name="sess"></param>
        /// <param name="deg"></param>
        /// <param name="colid"></param>
        /// <returns></returns>
        public DataSet FeeDemandReport_DetailedExcel(FeeDemand demandParam)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_RECIEPTCODE", demandParam.ReceiptTypeCode),
                    new SqlParameter("@P_BRANCHNO", demandParam.BranchNo),
                    new SqlParameter("@P_SEMESTERNO", demandParam.SemesterNo),
                    new SqlParameter("@P_PAYMENTTYPE", demandParam.PaymentTypeNo),
                    new SqlParameter("@P_SESSIONNO", demandParam.SessionNo),
                    new SqlParameter("@P_DEGREENO", demandParam.DegreeNo),
                    new SqlParameter("@P_COLLEGE_ID", demandParam.College_ID)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_REPORT_DEMAND_CREATION_EXCEL", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.GetAllDemands() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public string CreateDemandForStudentByFinance(FeeDemand demandCriteria)
        {
            string strOutput = "0";
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_SESSION_NO", demandCriteria.SessionNo),
                    new SqlParameter("@P_IDNO", demandCriteria.StudentId),
                    new SqlParameter("@P_RECIEPTCODE", demandCriteria.ReceiptTypeCode), 
                    new SqlParameter("@P_UA_NO", demandCriteria.UANO),
                    new SqlParameter("@P_COLLEGE_CODE", demandCriteria.CollegeCode),
                    new SqlParameter("@P_PTYPE", demandCriteria.PaymentTypeNo),
                    new SqlParameter("@P_OUTPUT", strOutput)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
                object output = objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_CREATE_DEMAND_FOR_STUDENT_BY_FINANCE", sqlParams, true);

                ////if (output != null && output.ToString() == "-99")
                ////    return "-99";
                ////else
                ////    strOutput = output.ToString();
                if (output != null && output.ToString() == "-99")
                    return "-99";
                else if (output.ToString() == "5")
                    return "5";
                else
                    strOutput = output.ToString();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.CreateDemandForStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return strOutput;
        }

        public string CreateDemandForExamFeesBulk(string studentIDs, int sessionno, int sem, int type, int uano, int schemeno, string ip)
        {
            string strOutput = "0";
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                   
                    new SqlParameter("@P_IDNOS", studentIDs),
                    new SqlParameter("@P_SESSIONNO", sessionno),              
                    new SqlParameter("@P_SEMESTERNO",sem),
                  
                    new SqlParameter("@P_EXAM_TYPE", type),
                    new SqlParameter("@P_UA_NO",uano),
                    new SqlParameter("@P_SCHEMENO", schemeno),
                    new SqlParameter("@P_IPADDRESS", ip),
                    new SqlParameter("@P_OUT", strOutput)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.NVarChar;
                object output = objDataAccess.ExecuteNonQuerySP("PKG_EXAM_REGISTRATION_DETAILS_FOR_BULK_EXM_REG", sqlParams, true);

                ////if (output != null && output.ToString() == "-99")
                ////    return "-99";
                ////else
                ////    strOutput = output.ToString();
                if (output != null && output.ToString() == "-99")
                    return "-99";
                else if (output.ToString() == "5")
                    return "5";
                else
                    strOutput = output.ToString();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.CreateDemandForStudents() --> " + ex.Message + " " + ex.StackTrace);
            }
            return strOutput;
        }

        // ADDED THE NEW MENTOD TO CANCEL INSTALLNMENT FROM DEMAND MODIFICATION PAGE BY SHAILENDRA ON DATED 30.01.2023 FOR TKT NO : 38868 AND 38960// 
        public int CancelInstallnment(FeeDemand Feedmnd)
        {
            int iOutput = 0;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_STUDID", Feedmnd.StudentId),
                    new SqlParameter("@P_SEM", Feedmnd.SemesterNo),
                    new SqlParameter("@P_DEMANDID", Feedmnd.DemandId),
                    new SqlParameter("@P_UA_NO", Feedmnd.UANO),  
                   // new SqlParameter("@P_IPADDRESS", Feedmnd.IpAddress ),
                    new SqlParameter("@P_OUT", iOutput)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                sqlParams[sqlParams.Length - 1].SqlDbType = SqlDbType.Int;
                object output = objDataAccess.ExecuteNonQuerySP("PKG_ACD_INSTALLMENT_CANCEL", sqlParams, true);

                if (output != null && Convert.ToInt32(output) == -99)
                    return -99;
                else if (Convert.ToInt32(output) == -5)
                    return -5;
                else
                    iOutput = Convert.ToInt32(output);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.CancelInstallnment() --> " + ex.Message + " " + ex.StackTrace);
            }
            return iOutput;
        }

        //added by Rohit M on 04-04-2023
        public DataSet GetAllPayments(int studentId, int DCRNO)
            {
            DataSet ds = null;
            try
                {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_IDNO", studentId),
                    new SqlParameter("@P_DCRNO", DCRNO)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_GET_ALL_PAYMENTS", sqlParams);
                }
            catch (Exception ex)
                {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.GetAllDemands() --> " + ex.Message + " " + ex.StackTrace);
                }
            return ds;
            }
        //added by Rohit M on 04-04-2023
        public DataSet GetPayment(int Dcrno, int studentId)
            {
            DataSet ds = null;
            try
                {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_DCRNO", Dcrno),
                    new SqlParameter("@P_IDNO", studentId)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_GET_PAYMENT", sqlParams);
                }
            catch (Exception ex)
                {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.GetDemand() --> " + ex.Message + " " + ex.StackTrace);
                }
            return ds;
            }
        //added by Rohit M on 04-04-2023
        public bool UpdatePayment(FeeDemand modifiedDemand, double Excess_Amt)
            {
            try
                {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {                    
                    new SqlParameter("@P_IDNO", modifiedDemand.StudentId),                    
                    new SqlParameter("@P_F1", modifiedDemand.FeeHeads[0]),
                    new SqlParameter("@P_F2", modifiedDemand.FeeHeads[1]),
                    new SqlParameter("@P_F3", modifiedDemand.FeeHeads[2]),
                    new SqlParameter("@P_F4", modifiedDemand.FeeHeads[3]),
                    new SqlParameter("@P_F5", modifiedDemand.FeeHeads[4]),
                    new SqlParameter("@P_F6", modifiedDemand.FeeHeads[5]),
                    new SqlParameter("@P_F7", modifiedDemand.FeeHeads[6]),
                    new SqlParameter("@P_F8", modifiedDemand.FeeHeads[7]),
                    new SqlParameter("@P_F9", modifiedDemand.FeeHeads[8]),
                    new SqlParameter("@P_F10", modifiedDemand.FeeHeads[9]),
                    new SqlParameter("@P_F11", modifiedDemand.FeeHeads[10]),
                    new SqlParameter("@P_F12", modifiedDemand.FeeHeads[11]),
                    new SqlParameter("@P_F13", modifiedDemand.FeeHeads[12]),
                    new SqlParameter("@P_F14", modifiedDemand.FeeHeads[13]),
                    new SqlParameter("@P_F15", modifiedDemand.FeeHeads[14]),
                    new SqlParameter("@P_F16", modifiedDemand.FeeHeads[15]),
                    new SqlParameter("@P_F17", modifiedDemand.FeeHeads[16]),
                    new SqlParameter("@P_F18", modifiedDemand.FeeHeads[17]),
                    new SqlParameter("@P_F19", modifiedDemand.FeeHeads[18]),
                    new SqlParameter("@P_F20", modifiedDemand.FeeHeads[19]),
                    new SqlParameter("@P_F21", modifiedDemand.FeeHeads[20]),
                    new SqlParameter("@P_F22", modifiedDemand.FeeHeads[21]),
                    new SqlParameter("@P_F23", modifiedDemand.FeeHeads[22]),
                    new SqlParameter("@P_F24", modifiedDemand.FeeHeads[23]),
                    new SqlParameter("@P_F25", modifiedDemand.FeeHeads[24]),
                    new SqlParameter("@P_F26", modifiedDemand.FeeHeads[25]),
                    new SqlParameter("@P_F27", modifiedDemand.FeeHeads[26]),
                    new SqlParameter("@P_F28", modifiedDemand.FeeHeads[27]),
                    new SqlParameter("@P_F29", modifiedDemand.FeeHeads[28]),
                    new SqlParameter("@P_F30", modifiedDemand.FeeHeads[29]),
                    new SqlParameter("@P_F31", modifiedDemand.FeeHeads[30]),
                    new SqlParameter("@P_F32", modifiedDemand.FeeHeads[31]),
                    new SqlParameter("@P_F33", modifiedDemand.FeeHeads[32]),
                    new SqlParameter("@P_F34", modifiedDemand.FeeHeads[33]),
                    new SqlParameter("@P_F35", modifiedDemand.FeeHeads[34]),
                    new SqlParameter("@P_F36", modifiedDemand.FeeHeads[35]),
                    new SqlParameter("@P_F37", modifiedDemand.FeeHeads[36]),
                    new SqlParameter("@P_F38", modifiedDemand.FeeHeads[37]),
                    new SqlParameter("@P_F39", modifiedDemand.FeeHeads[38]),
                    new SqlParameter("@P_F40", modifiedDemand.FeeHeads[39]),
                   
                    new SqlParameter("@P_TOTAL_AMT", modifiedDemand.TotalFeeAmount),
                    new SqlParameter("@P_REMARK", modifiedDemand.Remark),
                   
                    new SqlParameter("@P_IPADDRESS", modifiedDemand.IpAddress ),
                    new SqlParameter("@P_UA_NO", modifiedDemand.UANO),
                    new SqlParameter("@P_EXCESS_AMT",Excess_Amt),
                    new SqlParameter("@P_DCRNO", modifiedDemand.DemandId),
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                modifiedDemand.DemandId = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_FEECOLLECT_PAYMENT_MOD_UPDT_PAYMENT", sqlParams, true);

                if (modifiedDemand.DemandId == -99)
                    return false;
                }
            catch (Exception ex)
                {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.DemandModificationController.UpdateDemand() --> " + ex.Message + " " + ex.StackTrace);
                }
            return true;
            }

    }
}
            
       
