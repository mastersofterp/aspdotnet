using IITMS;
using IITMS.SQLServer.SQLDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessLogic.PostAdmission
{
    public class ADMPMeritListController
    {


        string _UAIMS_constr = string.Empty;

        public ADMPMeritListController()
        {
            _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        }


        public DataSet GetBranch(int Degree)
        {
            DataSet ds = null;
            SQLHelper objDataAccessLayer = new SQLHelper(_UAIMS_constr);

            try
            {
                SqlParameter[] objParams = new SqlParameter[]
                            {
                               new SqlParameter("@Degree",Degree),
                            };

                ds = objDataAccessLayer.ExecuteDataSetSP("[dbo].[PKG_ACAD_GET_GETDEGREENAME]", objParams);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return ds;
        }

        public DataSet GetSChedule(int Degree, string Branch, int ADMBATCH)
        {
            DataSet ds = null;
            SQLHelper objDataAccessLayer = new SQLHelper(_UAIMS_constr);

            try
            {
                SqlParameter[] objParams = new SqlParameter[]
                            {
                               new SqlParameter("@DEGREENO",Degree),
                               new SqlParameter("@BRACNCHNO",Branch),
                               new SqlParameter("@P_ADMBATCH",ADMBATCH)
                            };

                ds = objDataAccessLayer.ExecuteDataSetSP("[dbo].[PKG_ACAD_SP_GETSCHEDULE]", objParams);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return ds;
        }

        public DataSet GetMeritList(int ADMBATCH, int ScoreId, int DegreeNo, int branchno, int Category, int SortBy, int ApplicationType, DateTime? date)
        {
            DataSet ds = null;
            SQLHelper objDataAccessLayer = new SQLHelper(_UAIMS_constr);

            try
            {
                SqlParameter[] objParams = new SqlParameter[]
                            {
                               new SqlParameter("@P_ADMBATCH",ADMBATCH),                        
                               new SqlParameter("@P_SCOREID",ScoreId),
                               new SqlParameter("@P_DEGREENO",DegreeNo),
                               new SqlParameter("@P_BRANCHNO",branchno),
                               new SqlParameter("@P_CATEGORY",Category),
                               new SqlParameter("@P_SORTBY",SortBy),
                               new SqlParameter("@P_APPLICATIONTYPE",ApplicationType),
                               new SqlParameter("@P_CUTOFFDATE",date),
                            };

                ds = objDataAccessLayer.ExecuteDataSetSP("[dbo].[PKG_ADMP_MERIT_LIST_GENERATION_DAIICT]", objParams);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return ds;
        }

        public DataSet ExcelAttendanceSheet(int ADMBATCH, int ProgramType, int DegreeNo, string branchno, string ExamSchedule)
        {
            DataSet ds = null;
            SQLHelper objDataAccessLayer = new SQLHelper(_UAIMS_constr);

            try
            {
                SqlParameter[] objParams = new SqlParameter[]
                            {
                               new SqlParameter("@P_ADMBATCH",ADMBATCH),
                               new SqlParameter("@P_PROGRAMME_TYPE",ProgramType),
                               new SqlParameter("@P_DEGREENO",DegreeNo),
                               new SqlParameter("@P_BRANCHNO",branchno),
                               new SqlParameter("@P_Schedule",ExamSchedule)
                            };

                ds = objDataAccessLayer.ExecuteDataSetSP("[dbo].[PKG_GET_STUDENT_STUDENTATTENDANCE_EXCEL]", objParams);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return ds;
        }

        public int INSERT_UPDATE_MERITLIST(string XmlData, int UANO, int ProgramCode, int EntranceExam, string IpAddress, int DegreeNo) //int rollNo
        {
            //DataSet ds = null;
            int retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.Others);
            try
            {


                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[7];

                objParams[0] = new SqlParameter("@P_UANO", UANO);
                objParams[1] = new SqlParameter("@P_PROGRAMCODE", ProgramCode);
                objParams[2] = new SqlParameter("@P_EntranceExam", EntranceExam);
                objParams[3] = new SqlParameter("@P_IpAddress", IpAddress);
                objParams[4] = new SqlParameter("@P_DEGREENO", DegreeNo);
                objParams[5] = new SqlParameter("@P_XML", XmlData);
                objParams[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[6].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("[dbo].[PKG_ACD_ADMP_INSERT_MERITLIST_DAIICT]", objParams, true);


                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.TransactionFailed);
                else if (Convert.ToInt32(ret) == 1)
                    retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.RecordSaved);
                else if (Convert.ToInt32(ret) == 2)
                    retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamStudentAttendaceController.INSERT_Update_STUDENTATTENDANCE()-->" + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }

        public int LOCK_MERITLIST(string XmlData, int UANO, int ProgramCode, int EntranceExam, string IpAddress, int DegreeNo) //int rollNo
        {
            //DataSet ds = null;
            int retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.Others);
            try
            {


                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[7];

                objParams[0] = new SqlParameter("@P_UANO", UANO);
                objParams[1] = new SqlParameter("@P_PROGRAMCODE", ProgramCode);
                objParams[2] = new SqlParameter("@P_EntranceExam", EntranceExam);
                objParams[3] = new SqlParameter("@P_IpAddress", IpAddress);
                objParams[4] = new SqlParameter("@P_DEGREENO", DegreeNo);
                objParams[5] = new SqlParameter("@P_XML", XmlData);
                objParams[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[6].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ADMP_LOCK_MERITLIST_DAIICT", objParams, true);

                if (Convert.ToInt32(ret) == -99)
                {
                    retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.TransactionFailed);
                }
                else if (Convert.ToInt32(ret) == 1)
                {
                    retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.RecordSaved);
                }
                else if (Convert.ToInt32(ret) == 2627)
                {
                    retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.RecordExist);
                }

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamStudentAttendaceController.INSERT_Update_STUDENTATTENDANCE()-->" + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }

        public DataSet ExcelReport(int ADMBATCH, int ScoreId, int DegreeNo, int branchno, int Category, int SortBy, int ApplicationType, DateTime? date)
        {
            DataSet ds = null;
            SQLHelper objDataAccessLayer = new SQLHelper(_UAIMS_constr);

            try
            {
                SqlParameter[] objParams = new SqlParameter[]
                            {
                               new SqlParameter("@P_ADMBATCH",ADMBATCH),                        
                               new SqlParameter("@P_SCOREID",ScoreId),
                               new SqlParameter("@P_DEGREENO",DegreeNo),
                               new SqlParameter("@P_BRANCHNO",branchno),
                               new SqlParameter("@P_CATEGORY",Category),
                               new SqlParameter("@P_SORTBY",SortBy)
                            };

                ds = objDataAccessLayer.ExecuteDataSetSP("[dbo].[PKG_ADMP_MERIT_EXCEL_REPORT_DAIICT]", objParams);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return ds;
        }

        public DataSet GetBranch_CounsellinList(int AcadYear, int DegreeNo, int Entrance, int BRANCHNO, int ROUNDNO, int LETTER_TYPE_ID,DateTime GENERATEDON)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_ADMBATCH", AcadYear);
                objParams[1] = new SqlParameter("@P_DEGREENO", DegreeNo);
                objParams[2] = new SqlParameter("@P_EXAMNO", Entrance);
                objParams[3] = new SqlParameter("@P_BRANCHNO", BRANCHNO);
                objParams[4] = new SqlParameter("@P_ROUNDNO", ROUNDNO);
                objParams[5] = new SqlParameter("@P_LETTER_TYPE_ID", LETTER_TYPE_ID);
                //objParams[6] = new SqlParameter("@P_OFFER_FROM_DATE", Fromdate);
                //objParams[7] = new SqlParameter("@P_OFFER_END_DATE", Enddate);
                objParams[6] = new SqlParameter("@P_GENERATEDON", GENERATEDON);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMP_GET_BRANCH_COUNSELING", objParams);

            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetEligibleStudents-> " + ex.ToString());
            }
            return ds;
        }

    }
}

