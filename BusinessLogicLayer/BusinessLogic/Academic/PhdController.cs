using System;
using System.Data;
using System.Web;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.Academic;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            /// <summary>
            /// This PhdController is used to control Phd detail.
            /// </summary>
            public class PhdController
            {

                string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                //================= Added by dipali on 30072018 -- for  -- BULK PHD Annexure A-------//

                public DataSet GETBulkPhdAnnexureA(int degreeno, int deptno, int Admbatch)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DEGEREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", deptno);
                        objParams[2] = new SqlParameter("@P_ADMBATCH", Admbatch);

                        ds = objSQLHelper.ExecuteDataSetSP("GET_BULK_PHD_ANNEXURE_A_DETAILS", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetStudentListForIdentityCard-> " + ex.ToString());
                    }
                    return ds;
                }

                //====================== added by dipali  on 30072018 -- for  -- update bulk phd annexure A  --//

                public int UpdateBulkPhdAnnexureA(string IDNO, int admbatch)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_ADMBATCH", admbatch);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_BULK_PHD_ANNEXURE_A_UPDATE", objParams, true);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateStudRegStaus-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //================= Added by dipali on 30072018 -- for  -- BULK PHD Annexure A-------//

                public DataSet GETBulkPhdCourseRegistration(int degreeno, int deptno, int Sessionno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DEGEREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", deptno);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", Sessionno);

                        ds = objSQLHelper.ExecuteDataSetSP("GET_BULK_PHD_COURSE_REGISTRATION_DETAILS", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetStudentListForIdentityCard-> " + ex.ToString());
                    }
                    return ds;
                }

                //====================== added by dipali  on 30072018 -- for  -- update bulk phd annexure A  --//

                public int UpdateBulkPhdCourseRegistration(string IDNO, int sessionno)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_BULK_PHD_COURSE_REGISTRATION_UPDATE", objParams, true);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateStudRegStaus-> " + ex.ToString());
                    }
                    return retStatus;
                }


                //====================== PHD BUlk Semester pramotion ==================09082018 ===//

                public DataSet GETBulkPhdSemesterPramotion(int sessiono, int degreeno, int branchno, int schemeno, int semesterno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        objParams[1] = new SqlParameter("@P_DEGEREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[3] = new SqlParameter("@P_SCHEMENO  ", schemeno);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        ds = objSQLHelper.ExecuteDataSetSP("GET_BULK_PHD_SEMESTER_PRAMOTION", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetStudentListForIdentityCard-> " + ex.ToString());
                    }
                    return ds;
                }


                public int UpdateBulkPhdSemesterPramotion(string IDNO, int semester, int upsem, int Sessionno, int schemeno, int degreeno, string coursenos)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", semester);
                        objParams[2] = new SqlParameter("@P_UPSEM", upsem);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[4] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[5] = new SqlParameter("@P_COURSENO", coursenos);
                        objParams[6] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_BULK_PHD_SEMESTER_PROMOTION_UPDATE", objParams, true);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateStudRegStaus-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //-------------------------PHD ADMISSION STUDENT LIST EXCEL  --- 26072018-----------//
                public DataSet RetrievePhdStudentAdmittedlistExcel(int degreeno, int branchno, int userno, int admbatch)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_USERNO", userno);
                        objParams[2] = new SqlParameter("@P_BRANCH", branchno);
                        objParams[3] = new SqlParameter("@P_ADMBATCH", admbatch);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_PHD_ADMITTED_STUDENT_LIST_EXCEL", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentFeesDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                ///------------------Added by dipali on 27072018 -- for  -- BULK PHD STUDENT PROGRESS -------//

                public DataSet GETBulkStudProgress(int degreeno, int deptno, int Sessionno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DEGEREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", deptno);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", Sessionno);

                        ds = objSQLHelper.ExecuteDataSetSP("GET_BULK_PHD_STUDENT_PROGRESS_DETAILS", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetStudentListForIdentityCard-> " + ex.ToString());
                    }
                    return ds;
                }

                //====================== added by dipali  on 27072018 -- for  -- update bulk progress status  --//

                public int UpdateBulkPhdProgresStudent(string IDNO, int sessionno)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_BULK_PHD_STUDENT_PROGRESS_UPDATE", objParams, true);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.TPController.UpdateStudRegStaus-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //====================  Added by dipali on 02072018 for Phd degree Completed -----------//

                public DataTableReader GetPhdDegreeCompletedstudent(int idno)
                {
                    DataTableReader dtr = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PHD_GET_DEGREE_AWARD_DETAILS", objParams).Tables[0].CreateDataReader();

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return dtr;
                }
                //----------------------------------------------
                public string UpdatePhdDegreeCompletedStudentdetails(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    //int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Update Student
                        objParams = new SqlParameter[13];
                        //First Add Student Parameter
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_THESIS_TITLE_HINDI", objStudent.ThesisTitleHindi);
                        objParams[2] = new SqlParameter("@P_THESISTITLE", objStudent.ThesisTitle);
                        objParams[3] = new SqlParameter("@P_DESCIPLINE", objStudent.Descipline);
                        objParams[4] = new SqlParameter("@P_DEGREE_AMT", objStudent.PhddegreeTotalAmount);
                        objParams[5] = new SqlParameter("@P_FEES_NO", objStudent.PhdFeesRef);
                        objParams[6] = new SqlParameter("@P_PASSOUT_YEAR", objStudent.PhdPassoutyear);
                        objParams[7] = new SqlParameter("@P_CONVOCATION_YEAR", objStudent.PhdConvocationyear);
                        objParams[8] = new SqlParameter("@P_CONVOCATION_DATE", objStudent.PhdConvocationDate);
                        objParams[9] = new SqlParameter("@P_DEGREE_AWARD_REMARK", objStudent.PhdDegreeRemark);
                        objParams[10] = new SqlParameter("@P_DEGREE_APPROVAL_NO", objStudent.Uano);
                        objParams[11] = new SqlParameter("@P_STUDNAME_HINDI", objStudent.StudNameHindi);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PHD_DEGREE_COMPLETED_UPDATE", objParams, true);
                        return ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }
                //-----------------------------------

                public DataSet RetrievePhdDegreeAwardDetailsExcel(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PHD_GET_DEGREE_AWARD_EXCEL", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentFeesDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //===============Annexure changes status report -----06082018----//

                public DataSet RetrievePhdAnnexureAStatusDetails(int degreeno, int branchno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_BRANCH", branchno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_PHD_ANNEXURE_STATUS_REPORT", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentFeesDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet RetrievePhdAnnexureAStatusExcel(int degreeno, int branchno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_BRANCH", branchno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_PHD_ANNEXURE_STATUS_REPORT_EXCEL", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentFeesDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //====================== PHD BUlk Semester pramotion ==================09082018 ===//


                //=================== Phd progress for student and faculty ========================//

                public DataSet GETPhdProgressStudFaculty(int idno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("GET_PHD_PROGRESS_STUD_FACULTY_DETAILS", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetStudentListForIdentityCard-> " + ex.ToString());
                    }
                    return ds;
                }

                // ---=======================-  get data for phd Defence report -- addde by dipali on  28082018-============================- //

                public DataTableReader GetStudentPhdDefenceDetails(int idno)
                {
                    DataTableReader dtr = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PHD_STUDENT_SP_RET_STUDENT_BYID_ANNEXURE_F", objParams).Tables[0].CreateDataReader();

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return dtr;
                }

                //--===============  Phd Examiner Master -- added by dipali  on 22082018 ---==============================//

                public DataSet GETPhdExaminerDetails()
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("GET_PHD_EXAMINER_MASTER_DETAILS", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentController.GET_PHD_EXAMINER_MASTER_DETAILS-> " + ex.ToString());
                    }
                    return ds;
                }

                //  --- modify -- on 16112018
                //public string AddPhdExaminerDetails(Student objstudentinfo)
                //{
                //    string retStatus = string.Empty;
                //    try
                //    {

                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;
                //        //Add New student
                //        objParams = new SqlParameter[12];
                //        objParams[0] = new SqlParameter("@P_IDNO", objstudentinfo.IdNo);
                //        objParams[1] = new SqlParameter("@P_NAME", objstudentinfo.StudName);
                //        objParams[2] = new SqlParameter("@P_ADDRESS", objstudentinfo.PAddress);
                //        objParams[3] = new SqlParameter("@P_MOBILE", objstudentinfo.StudentMobile);
                //        objParams[4] = new SqlParameter("@P_CONTACT", objstudentinfo.PMobile);
                //        objParams[5] = new SqlParameter("@P_EMAILID", objstudentinfo.EmailID);
                //        objParams[6] = new SqlParameter("@P_EXTERNAL", objstudentinfo.NADID);

                //        objParams[7] = new SqlParameter("@P_OTHERMOBILE", objstudentinfo.FatherMobile);  //---add other mobile ,contact no --16112018
                //        objParams[8] = new SqlParameter("@P_OTHERCONTACT", objstudentinfo.MotherMobile);
                //        objParams[9] = new SqlParameter("@P_UANO", objstudentinfo.Uano);
                //        objParams[10] = new SqlParameter("@P_IPADDRESS", objstudentinfo.IPADDRESS);
                //        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int, 25);
                //        objParams[11].Direction = ParameterDirection.Output;

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_PHD_EXAMINER_MASTER", objParams, true);

                //        //if (ret.ToString().Equals("-99"))
                //        //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //        //else
                //        //    retStatus = Convert.ToInt32(ret);
                //        retStatus = ret.ToString();

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = "0";
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddStudentDetailsForProvisionalDegree-> " + ex.ToString());
                //    }

                //    return retStatus;
                //}


                public string AddPhdExaminerDetails(Student objstudentinfo, int status, string Department, string BranchName, string Specialization, string Affiliation)
                {
                    string retStatus = string.Empty;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New student
                        objParams = new SqlParameter[21];
                        objParams[0] = new SqlParameter("@P_IDNO", objstudentinfo.IdNo);
                        objParams[1] = new SqlParameter("@P_NAME", objstudentinfo.StudName);
                        objParams[2] = new SqlParameter("@P_ADDRESS", objstudentinfo.PAddress);
                        objParams[3] = new SqlParameter("@P_MOBILE", objstudentinfo.StudentMobile);
                        objParams[4] = new SqlParameter("@P_CONTACT", objstudentinfo.PMobile);
                        objParams[5] = new SqlParameter("@P_EMAILID", objstudentinfo.EmailID);
                        objParams[6] = new SqlParameter("@P_EXTERNAL", objstudentinfo.NADID);

                        objParams[7] = new SqlParameter("@P_OTHERMOBILE", objstudentinfo.FatherMobile);  //---add other mobile ,contact no --16112018
                        objParams[8] = new SqlParameter("@P_OTHERCONTACT", objstudentinfo.MotherMobile);
                        objParams[9] = new SqlParameter("@P_UANO", objstudentinfo.Uano);
                        objParams[10] = new SqlParameter("@P_IPADDRESS", objstudentinfo.IPADDRESS);
                        objParams[11] = new SqlParameter("@P_STATUS", status);

                        objParams[12] = new SqlParameter("@P_DEPARTMENT", Department);  //---add by Sneha Doble on 10/12/2020
                        objParams[13] = new SqlParameter("@P_BANKNO", objstudentinfo.BankNo);
                        objParams[14] = new SqlParameter("@P_ACCNO", objstudentinfo.AccNo);
                        objParams[15] = new SqlParameter("@P_ACC_HOLDERNAME", objstudentinfo.Accholdername);
                        objParams[16] = new SqlParameter("@P_IFSC_CODE", objstudentinfo.IFSCcode);
                        objParams[17] = new SqlParameter("@P_BRANCHNAME", BranchName);

                        objParams[18] = new SqlParameter("@P_SPECIALIZATION", Specialization);
                        objParams[19] = new SqlParameter("@P_AFFILIATION", Affiliation);

                        objParams[20] = new SqlParameter("@P_OUT", SqlDbType.Int, 25);
                        objParams[20].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_PHD_EXAMINER_MASTER", objParams, true);

                        //if (ret.ToString().Equals("-99"))
                        //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        //else
                        //    retStatus = Convert.ToInt32(ret);
                        retStatus = ret.ToString();

                    }
                    catch (Exception ex)
                    {
                        retStatus = "0";
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddStudentDetailsForProvisionalDegree-> " + ex.ToString());
                    }

                    return retStatus;
                }


                public DataSet GETPhdExaminerDetailsIDNO(int idno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("GET_PHD_EXAMINER_MASTER_DETAILS_IDNO", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentController.GET_PHD_EXAMINER_MASTER_DETAILS-> " + ex.ToString());
                    }
                    return ds;
                }

                //--------------Phd Examiner Details -------------//

                public DataTableReader GetExaminerPHDDetails(int idno)
                {
                    DataTableReader dtr = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PHD_STUDENT_SP_EXAMINER_DETAILS", objParams).Tables[0].CreateDataReader();

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return dtr;
                }

                //----- Supervisior add examiner details ------//

                public string AddPhdExaminerDetailsSupervisor(Student objstudentinfo)
                {
                    string retStatus = string.Empty;
                    try
                    {
                        //           @P_IDNO,@P_SUPERVISORNO,1,@P_DRCCHAIRMANNO,@P_EXAMINER1,@P_EXAMINER2,@P_EXAMINER3
                        //,@P_EXAMINER4,@P_EXAMINER5,@P_EXAMINER6,@P_EXAMINER7,@P_EXAMINER8,@P_EXAMINER9,@P_EXAMINER10,

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[20];
                        objParams[0] = new SqlParameter("@P_IDNO", objstudentinfo.IdNo);
                        objParams[1] = new SqlParameter("@P_SUPERVISORNO", objstudentinfo.PhdSupervisorNo);
                        objParams[2] = new SqlParameter("@P_DRCCHAIRMANNO", objstudentinfo.DrcChairNo);
                        objParams[3] = new SqlParameter("@P_EXAMINER1", objstudentinfo.PhdExaminer1);
                        objParams[4] = new SqlParameter("@P_EXAMINER2", objstudentinfo.PhdExaminer2);
                        objParams[5] = new SqlParameter("@P_EXAMINER3", objstudentinfo.PhdExaminer3);
                        objParams[6] = new SqlParameter("@P_EXAMINER4", objstudentinfo.PhdExaminer4);
                        objParams[7] = new SqlParameter("@P_EXAMINER5", objstudentinfo.PhdExaminer5);
                        objParams[8] = new SqlParameter("@P_EXAMINER6", objstudentinfo.PhdExaminer6);
                        objParams[9] = new SqlParameter("@P_EXAMINER7", objstudentinfo.PhdExaminer7);
                        objParams[10] = new SqlParameter("@P_EXAMINER8", objstudentinfo.PhdExaminer8);
                        objParams[11] = new SqlParameter("@P_EXAMINER9", objstudentinfo.PhdExaminer9);
                        objParams[12] = new SqlParameter("@P_EXAMINER10", objstudentinfo.PhdExaminer10);
                        objParams[13] = new SqlParameter("@P_SYNNAME", objstudentinfo.PhdSynName);
                        objParams[14] = new SqlParameter("@P_SYNFILE", objstudentinfo.PhdSynFile);
                        objParams[15] = new SqlParameter("@P_JOINTSUPNO", objstudentinfo.JoinsupervisorNo);
                        objParams[16] = new SqlParameter("@P_PRESYNDATE", objstudentinfo.PhdPresyndate);
                        objParams[17] = new SqlParameter("@P_PRESYNNAME", objstudentinfo.PhdPreSynName);
                        objParams[18] = new SqlParameter("@P_PRESYNFILE", objstudentinfo.PhdPreSynFile);
                        objParams[19] = new SqlParameter("@P_OUT", SqlDbType.Int, 25);
                        objParams[19].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_PHD_EXAMINER_DETAILS_ADD_BY_SUPERVISIOR", objParams, true);

                        //if (ret.ToString().Equals("-99"))
                        //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        //else
                        //    retStatus = Convert.ToInt32(ret);
                        retStatus = ret.ToString();

                    }
                    catch (Exception ex)
                    {
                        retStatus = "0";
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddStudentDetailsForProvisionalDegree-> " + ex.ToString());
                    }

                    return retStatus;
                }
                //--------- phd examiner drc & dean Confirmation -------//
                public string UpdateExaminerStatus(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Update drc 
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_STATUS", objStudent.PhdStatusValue);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PHD_EXAMINER_UPDATE", objParams, true);
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                //-------- phd dean reject supervisior -----------//
                public string RejectDeanStatus(Student objStudent, string Remark)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Update drc 
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_REMARK", Remark);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PHD_EXAMINER_DEAN_REJECT", objParams, true);

                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RejectSupervisorStatus-> " + ex.ToString());
                    }
                }
                //--------------------------------------------------
                public string UpdatePhdDeanApproval(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Update drc 
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_Examiner1Status", objStudent.PhdExaminer1Status);
                        objParams[2] = new SqlParameter("@P_Examiner2Status", objStudent.PhdExaminer2Status);
                        objParams[3] = new SqlParameter("@P_Examiner3Status", objStudent.PhdExaminer3Status);
                        objParams[4] = new SqlParameter("@P_Examiner4Status", objStudent.PhdExaminer4Status);
                        objParams[5] = new SqlParameter("@P_Examiner5Status", objStudent.PhdExaminer5Status);
                        objParams[6] = new SqlParameter("@P_Examiner6Status", objStudent.PhdExaminer6Status);
                        objParams[7] = new SqlParameter("@P_Examiner7Status", objStudent.PhdExaminer7Status);
                        objParams[8] = new SqlParameter("@P_Examiner8Status", objStudent.PhdExaminer8Status);
                        objParams[9] = new SqlParameter("@P_Examiner9Status", objStudent.PhdExaminer9Status);
                        objParams[10] = new SqlParameter("@P_Examiner10Status", objStudent.PhdExaminer10Status);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PHD_EXAMINER_DEAN_APPROVAL", objParams, true);
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }
                //-------------phd- supervisior approval  viva_voice -- //
                public string UpdatePhdSupervisiorVivaApproval(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Update drc 
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_Examiner1Status", objStudent.PhdExaminer1Status);
                        objParams[2] = new SqlParameter("@P_VIVADATE", objStudent.YearOfExam);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PHD_EXAMINER_SUPERVISIOR_VIVA_APPROVAL", objParams, true);
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }


                public DataSet RetrieveStudentDetailsPHD(string search, string category, string branchno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SEARCHSTRING", search);
                        objParams[1] = new SqlParameter("@P_SEARCH", category);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_SEARCH_PHD_STUDENT", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.RetrieveEmpDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //======================== dean external Examiner confirmation  ====================//

                public string UpdatePhdDeanVivaApproval(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Update drc 
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_Examiner1Status", objStudent.PhdExaminer1Status);
                        objParams[2] = new SqlParameter("@P_VIVADATE", objStudent.YearOfExam);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PHD_EXAMINER_DEAN_VIVA_APPROVAL", objParams, true);
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                //===============  Phd OutSide Member Master -- added by dipali  on 08012019 ---==============================//

                public DataSet GETPhdOutsideMemberDetails()
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("GET_PHD_OUTSIDE_MEMBER_MASTER_DETAILS", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentController.GET_PHD_EXAMINER_MASTER_DETAILS-> " + ex.ToString());
                    }
                    return ds;
                }

                //  --- modify -- on 16112018
                //public string AddPhdOutsideMemberDetails(Student objstudentinfo)
                //{
                //    string retStatus = string.Empty;
                //    try
                //    {

                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;
                //        //Add New student
                //        objParams = new SqlParameter[11];
                //        objParams[0] = new SqlParameter("@P_IDNO", objstudentinfo.IdNo);
                //        objParams[1] = new SqlParameter("@P_NAME", objstudentinfo.StudName);
                //        objParams[2] = new SqlParameter("@P_ADDRESS", objstudentinfo.PAddress);
                //        objParams[3] = new SqlParameter("@P_MOBILE", objstudentinfo.StudentMobile);
                //        objParams[4] = new SqlParameter("@P_CONTACT", objstudentinfo.PMobile);
                //        objParams[5] = new SqlParameter("@P_EMAILID", objstudentinfo.EmailID);
                //        objParams[6] = new SqlParameter("@P_DESIGNATION", objstudentinfo.PhdDegreeRemark);
                //        objParams[7] = new SqlParameter("@P_BRANCHNO", objstudentinfo.BranchNo);
                //        objParams[8] = new SqlParameter("@P_UANO", objstudentinfo.Uano);
                //        objParams[9] = new SqlParameter("@P_IPADDRESS", objstudentinfo.IPADDRESS);
                //        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int, 25);
                //        objParams[10].Direction = ParameterDirection.Output;

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_PHD_OUTSIDE_MEMBER_MASTER", objParams, true);

                //        retStatus = ret.ToString();

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = "0";
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddStudentDetailsForProvisionalDegree-> " + ex.ToString());
                //    }

                //    return retStatus;
                //}

                public string AddPhdOutsideMemberDetails(Student objstudentinfo, string Dept_name, string Affiliation)
                {
                    string retStatus = string.Empty;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New student
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_IDNO", objstudentinfo.IdNo);
                        objParams[1] = new SqlParameter("@P_NAME", objstudentinfo.StudName);
                        objParams[2] = new SqlParameter("@P_ADDRESS", objstudentinfo.PAddress);
                        objParams[3] = new SqlParameter("@P_MOBILE", objstudentinfo.StudentMobile);
                        objParams[4] = new SqlParameter("@P_CONTACT", objstudentinfo.PMobile);
                        objParams[5] = new SqlParameter("@P_EMAILID", objstudentinfo.EmailID);
                        objParams[6] = new SqlParameter("@P_DESIGNATION", objstudentinfo.PhdDegreeRemark);
                        objParams[7] = new SqlParameter("@P_BRANCHNO", objstudentinfo.BranchNo);
                        objParams[8] = new SqlParameter("@P_UANO", objstudentinfo.Uano);
                        objParams[9] = new SqlParameter("@P_IPADDRESS", objstudentinfo.IPADDRESS);
                        objParams[10] = new SqlParameter("@P_DEPTNAME", Dept_name);
                        objParams[11] = new SqlParameter("@P_AFFILIATION", Affiliation);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int, 25);
                        objParams[12].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_PHD_OUTSIDE_MEMBER_MASTER", objParams, true);

                        retStatus = ret.ToString();

                    }
                    catch (Exception ex)
                    {
                        retStatus = "0";
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddPhdOutsideMemberDetails-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GETPhdOutsideMemberDetailsIDNO(int idno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("GET_PHD_OUTSIDE_MEMBER_MASTER_DETAILS_IDNO", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentController.GET_PHD_EXAMINER_MASTER_DETAILS-> " + ex.ToString());
                    }
                    return ds;
                }

                // =============  Bulk Drc Chairman Update  ===================//
                public DataSet GETPhdDrcChairmanOld(int degreeno, int department)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_DEPTNO", department);
                        ds = objSQLHelper.ExecuteDataSetSP("GET_PHD_DRCCHAIRMAN_DETAILS_OLD", objParams);
                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentController.GET_PHD_EXAMINER_MASTER_DETAILS-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GETPhdDrcChairmanNew(int degreeno, int department)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_DEPTNO", department);
                        ds = objSQLHelper.ExecuteDataSetSP("GET_PHD_DRCCHAIRMAN_DETAILS_NEW", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentController.GET_PHD_EXAMINER_MASTER_DETAILS-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GETPhdDrcChairmanUpdateIdno(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UANO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("GET_PHD_DRCCHAIRMAN_DETAILS_IDNO", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentController.GET_PHD_EXAMINER_MASTER_DETAILS-> " + ex.ToString());
                    }
                    return ds;
                }

                public string UpdatePhdDrcChairmanStatus(Student objstudentinfo, string actionn)
                {
                    string retStatus = string.Empty;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New student
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_UA_NO", objstudentinfo.IdNo);
                        objParams[1] = new SqlParameter("@P_DRCSTATUS", objstudentinfo.SuperRole);
                        objParams[2] = new SqlParameter("@P_UANO", objstudentinfo.Uano);
                        objParams[3] = new SqlParameter("@P_IPADDRESS", objstudentinfo.IPADDRESS);
                        objParams[4] = new SqlParameter("@P_ACTIONN", actionn);
                        objParams[5] = new SqlParameter("@P_DEPTNO", objstudentinfo.BranchNo);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int, 25);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPD_PHD_DRC_CHAIRMAIN_DETAILS", objParams, true);

                        retStatus = ret.ToString();

                    }
                    catch (Exception ex)
                    {
                        retStatus = "0";
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddStudentDetailsForProvisionalDegree-> " + ex.ToString());
                    }

                    return retStatus;
                }

                //=========================== PHD ANNEXURE B ELIGIBLITY ================================//

                public DataTableReader GetStudentPHDDetailsAnnexureBEligibility(int idno)
                {
                    DataTableReader dtr = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PHD_STUDENT_ANNEXURE_B_ELIGIBILITY_DETAILS", objParams).Tables[0].CreateDataReader();

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return dtr;
                }

                public string UpdatePHDStudentAnnexureB_Eligibility(Student objStudent, string Action)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    //int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Update Student Details 
                        objParams = new SqlParameter[5];
                        //First Add Student Parameter
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_ACTION", Action);
                        objParams[2] = new SqlParameter("@P_UANO", objStudent.Uano);
                        objParams[3] = new SqlParameter("@P_IPADDRESS", objStudent.IPADDRESS);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PHD_STUD_SP_UPD_STUDENT_ANNEXURE_B_ELIGIBILITY", objParams, true);

                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }


                //   ============================THESIS SUBMITION FOR DEAN ==============================//

                public DataTableReader GetStudentPhdThesisDetails(int idno)
                {
                    DataTableReader dtr = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PHD_STUDENT_THESIS_DETAILS", objParams).Tables[0].CreateDataReader();

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return dtr;
                }

                public string UpdatePhdThesisSubmissionDate(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    //int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Update Student Details 
                        objParams = new SqlParameter[5];
                        //First Add Student Parameter
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_THS_SUBDATE", objStudent.ApprovedDate);
                        objParams[2] = new SqlParameter("@P_UANO", objStudent.Uano);
                        objParams[3] = new SqlParameter("@P_IPADDRESS", objStudent.IPADDRESS);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PHD_STUD_SP_UPD_STUDENT_THESIS_DETAILS", objParams, true);

                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                //---------------------------  Phd Unsatisfactory progress---------------------------------------------------
                public DataSet GETBulkStudProgressOneUnsatisfactory(int degreeno, int deptno, int Sessionno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DEGEREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", deptno);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", Sessionno);

                        ds = objSQLHelper.ExecuteDataSetSP("GET_BULK_PHD_STUDENT_PROGRESS_DETAILS_ONE_UNSATISFACTORY", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetStudentListForIdentityCard-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GETBulkStudProgressTwoUnsatisfactory(int degreeno, int deptno, int Sessionno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DEGEREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", deptno);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", Sessionno);

                        ds = objSQLHelper.ExecuteDataSetSP("GET_BULK_PHD_STUDENT_PROGRESS_DETAILS_TWO_UNSATISFACTORY", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetStudentListForIdentityCard-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GETBulkStudProgressSatisfactoryExcel(int degreeno, int deptno, int Sessionno, int action)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_DEGEREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", deptno);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[3] = new SqlParameter("@P_ACTION", action);
                        ds = objSQLHelper.ExecuteDataSetSP("GET_PHD_STUDENT_PROGRESS_DETAILS_EXCEL", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GetStudentListForIdentityCard-> " + ex.ToString());
                    }
                    return ds;
                }

                //--------------------PHD EXTEND  COMPHRENSIVE AND THESIS DATE------------------------------------// 

                public DataTableReader GetPHDExtendedDateDetails(int idno)
                {
                    DataTableReader dtr = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PHD_STUDENT_EXTEND_DATE_DETAILS", objParams).Tables[0].CreateDataReader();

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return dtr;
                }

                //public string UpdatePhdExtendedDate(Student objStudent ,string Action , string link)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);
                //    //int retStatus = Convert.ToInt32(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;
                //        //Update Student Details 
                //        objParams = new SqlParameter[7]; 

                //        //First Add Student Parameter
                //        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                //        objParams[1] = new SqlParameter("@P_THS_SUBDATE", objStudent.ApprovedDate);
                //        objParams[2] = new SqlParameter("@P_UANO", objStudent.Uano);
                //        objParams[3] = new SqlParameter("@P_IPADDRESS", objStudent.IPADDRESS);
                //        objParams[4] = new SqlParameter("@P_ACTION", Action);
                //        objParams[5] = new SqlParameter("@P_LINK", link);
                //        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //        objParams[6].Direction = ParameterDirection.Output;

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PHD_UPD_STUDENT_EXTENDED_DETAILS", objParams, true);

                //        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                //        return ret.ToString();
                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                //    }
                //}


                public int UpdatePhdExtendedDate(Student objStudent, string Action, string file, System.Web.UI.WebControls.FileUpload fuFile)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        bool flag = true;
                        string uploadPath = HttpContext.Current.Server.MapPath("~/PHD_FILES/");

                        //Upload the File
                        if (!fuFile.FileName.Equals(string.Empty))
                        {
                            if (System.IO.File.Exists(uploadPath + file))
                            {
                                //lblStatus.Text = "File already exists. Please upload another file or rename and upload.";                            
                                return Convert.ToInt32(CustomStatus.FileExists);
                            }
                            else
                            {
                                string uploadFile = System.IO.Path.GetFileName(fuFile.PostedFile.FileName);
                                fuFile.PostedFile.SaveAs(uploadPath + file);
                                flag = true;
                            }
                        }

                        if (flag == true)
                        {
                            objParams = new SqlParameter[7];

                            //First Add Student Parameter
                            objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                            objParams[1] = new SqlParameter("@P_THS_SUBDATE", objStudent.ApprovedDate);
                            objParams[2] = new SqlParameter("@P_UANO", objStudent.Uano);
                            objParams[3] = new SqlParameter("@P_IPADDRESS", objStudent.IPADDRESS);
                            objParams[4] = new SqlParameter("@P_ACTION", Action);
                            objParams[5] = new SqlParameter("@P_LINK", file);
                            objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                            objParams[6].Direction = ParameterDirection.Output;

                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PHD_UPD_STUDENT_EXTENDED_DETAILS", objParams, true);

                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            return Convert.ToInt32(ret);

                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.NewsController.Add-> " + ex.ToString());
                    }
                    return retStatus;
                }

                ///---Added for Get new ReceiptNO ----22112017
                public DataSet GetNewReceiptData(string modeOfReceipt, string receipt_code)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                {

                    new SqlParameter("@P_MODE_OF_RECEIPT", modeOfReceipt),
                    new SqlParameter("@P_RECEIPT_CODE", receipt_code),
                  
                };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_FEECOLLECT_NEW_RECEIPT_DATA_CONVOCATION", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.GetNewReceiptData() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                ///---add for INSERT-- convocation-- DCR  -- 22112017
                public int InsertPhdThesisDCR(FeeDemand objEntityClass)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[10];

                        objParams[0] = new SqlParameter("@P_SESSION_NO", objEntityClass.SessionNo);
                        objParams[1] = new SqlParameter("@P_IDNO", objEntityClass.StudentId);
                        objParams[2] = new SqlParameter("@P_ENROLLNO", objEntityClass.EnrollmentNo);
                        objParams[3] = new SqlParameter("@P_RECIEPTCODE", objEntityClass.ReceiptTypeCode);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", objEntityClass.SemesterNo);
                        objParams[5] = new SqlParameter("@P_PAYMENTTYPE", objEntityClass.PaymentTypeNo);
                        objParams[6] = new SqlParameter("@P_UA_NO", objEntityClass.UserNo);
                        objParams[7] = new SqlParameter("@P_RECIEPTNO", objEntityClass.Remark);
                        objParams[8] = new SqlParameter("@P_COUNTER_NO", objEntityClass.CounterNo);
                        objParams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        int ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_CREATE_DCR_FOR_PHD_THESIS", objParams, true));
                        if (ret == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.MarksEntryController.InsertStudentMarks() --> " + ex.Message + " " + ex.StackTrace);
                    }

                    return retStatus;

                }

                ///-------------------------------

                ///---Added for INSERT --Thesis-- DEMAND--22112017
                public int InsertPhdThesisDemand(FeeDemand objEntityClass)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_SESSION_NO", objEntityClass.SessionNo);
                        objParams[1] = new SqlParameter("@P_IDNO", objEntityClass.StudentId);
                        objParams[2] = new SqlParameter("@P_ENROLLNO", objEntityClass.EnrollmentNo);
                        objParams[3] = new SqlParameter("@P_RECIEPTCODE", objEntityClass.ReceiptTypeCode);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", objEntityClass.SemesterNo);
                        objParams[5] = new SqlParameter("@P_PAYMENTTYPE", objEntityClass.PaymentTypeNo);
                        objParams[6] = new SqlParameter("@P_UA_NO", objEntityClass.CounterNo);
                        objParams[7] = new SqlParameter("@P_COUNTER_NO", objEntityClass.FeeCatNo);
                        objParams[8] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        int ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_CREATE_DEMAND_FOR_PHD_THESIS", objParams, true));
                        if (ret == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.MarksEntryController.InsertStudentMarks() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return retStatus;
                }

                //-- Phd student details--26032018--//
                public DataSet GetPhdAnnexureBEligibilityDetailsExcel(int userno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UANO", userno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_PHD_ANNEXURE_BELIGIBILITY_DETAIL_EXCEL", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentFeesDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //-----------------------   Phd Examiner Tracker  05032019----------------------------------------------//

                public DataTableReader GetPHDTrackerDetails(int idno)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PHD_STUDENT_SP_TRACKER_DETAILS", objParams).Tables[0].CreateDataReader();

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return dtr;
                }

                // -----  Submit phd examiner details --- 
                public string SubmitPhdExaminerDetails(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[20];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_Examiner1", objStudent.PhdExaminer1);
                        objParams[2] = new SqlParameter("@P_Examiner2", objStudent.PhdExaminer2);
                        objParams[3] = new SqlParameter("@P_Examiner3", objStudent.PhdExaminer3);
                        objParams[4] = new SqlParameter("@P_Examiner4", objStudent.PhdExaminer4);
                        objParams[5] = new SqlParameter("@P_Examiner1Status", objStudent.PhdExaminer1Status);
                        objParams[6] = new SqlParameter("@P_Examiner2Status", objStudent.PhdExaminer2Status);
                        objParams[7] = new SqlParameter("@P_Examiner3Status", objStudent.PhdExaminer3Status);
                        objParams[8] = new SqlParameter("@P_Examiner4Status", objStudent.PhdExaminer4Status);

                        objParams[9] = new SqlParameter("@P_ExaminerFile1", objStudent.PhdExaminerFile1);
                        objParams[10] = new SqlParameter("@P_ExaminerFile2", objStudent.PhdExaminerFile2);
                        objParams[11] = new SqlParameter("@P_ExaminerFile3", objStudent.PhdExaminerFile3);
                        objParams[12] = new SqlParameter("@P_ExaminerFile4", objStudent.PhdExaminerFile4);

                        objParams[13] = new SqlParameter("@P_PRIORITY_EX1", objStudent.PhdPriExaminer1);
                        objParams[14] = new SqlParameter("@P_PRIORITY_EX2", objStudent.PhdPriExaminer2);
                        objParams[15] = new SqlParameter("@P_PRIORITY_EX3", objStudent.PhdPriExaminer3);
                        objParams[16] = new SqlParameter("@P_PRIORITY_EX4", objStudent.PhdPriExaminer4);

                        objParams[17] = new SqlParameter("@P_UANO", objStudent.Uano);
                        objParams[18] = new SqlParameter("@P_IPADDRESS", objStudent.IPADDRESS);
                        objParams[19] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[19].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PHD_INS_EXAMINER_DETAILS", objParams, true);
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                // -----  Reject phd examiner details --- 
                public string RejectPhdExaminerDetails(Student objStudent, string status)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_Examiner1Status", objStudent.PhdExaminer1Status);
                        objParams[2] = new SqlParameter("@P_Examiner2Status", objStudent.PhdExaminer2Status);
                        objParams[3] = new SqlParameter("@P_Examiner3Status", objStudent.PhdExaminer3Status);
                        objParams[4] = new SqlParameter("@P_Examiner4Status", objStudent.PhdExaminer4Status);
                        objParams[5] = new SqlParameter("@P_UANO", objStudent.Uano);
                        objParams[6] = new SqlParameter("@P_REMARK", objStudent.Remark);
                        objParams[7] = new SqlParameter("@P_IPADDRESS", objStudent.IPADDRESS);
                        objParams[8] = new SqlParameter("@P_STATUS", status);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PHD_REJECT_EXAMINER", objParams, true);
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                // -----  Approve  phd examiner  --- 
                public string ApprovePhdExaminerByDean(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_Examiner1Status", objStudent.PhdExaminer1Status);
                        objParams[2] = new SqlParameter("@P_Examiner2Status", objStudent.PhdExaminer2Status);
                        objParams[3] = new SqlParameter("@P_Examiner3Status", objStudent.PhdExaminer3Status);
                        objParams[4] = new SqlParameter("@P_Examiner4Status", objStudent.PhdExaminer4Status);
                        objParams[5] = new SqlParameter("@P_UANO", objStudent.Uano);
                        objParams[6] = new SqlParameter("@P_IPADDRESS", objStudent.IPADDRESS);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PHD_APPROVE_EXAMINER", objParams, true);
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                // -----  Dean Thesis Invitation for examiner  --- 
                public string PhdThesisExaminerByDean(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_Examiner1Status", objStudent.PhdExaminer1Status);
                        objParams[2] = new SqlParameter("@P_Examiner2Status", objStudent.PhdExaminer2Status);
                        objParams[3] = new SqlParameter("@P_Examiner3Status", objStudent.PhdExaminer3Status);
                        objParams[4] = new SqlParameter("@P_Examiner4Status", objStudent.PhdExaminer4Status);

                        objParams[5] = new SqlParameter("@P_ExaminerFile1", objStudent.PhdExaminerFile1);
                        objParams[6] = new SqlParameter("@P_ExaminerFile2", objStudent.PhdExaminerFile2);
                        objParams[7] = new SqlParameter("@P_ExaminerFile3", objStudent.PhdExaminerFile3);
                        objParams[8] = new SqlParameter("@P_ExaminerFile4", objStudent.PhdExaminerFile4);

                        objParams[9] = new SqlParameter("@P_UANO", objStudent.Uano);
                        objParams[10] = new SqlParameter("@P_IPADDRESS", objStudent.IPADDRESS);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PHD_INS_EXAMINER_THESIS_DETAILS", objParams, true);
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                // -----  Dean Thesis Approval for examiner  --- 
                public string PhdThesisExaminerApprovalByDean(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_Examiner1Status", objStudent.PhdExaminer1Status);
                        objParams[2] = new SqlParameter("@P_Examiner2Status", objStudent.PhdExaminer2Status);
                        objParams[3] = new SqlParameter("@P_Examiner3Status", objStudent.PhdExaminer3Status);
                        objParams[4] = new SqlParameter("@P_Examiner4Status", objStudent.PhdExaminer4Status);
                        objParams[5] = new SqlParameter("@P_UANO", objStudent.Uano);
                        objParams[6] = new SqlParameter("@P_IPADDRESS", objStudent.IPADDRESS);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PHD_EXAMINER_THESIS_APPROVAL", objParams, true);
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                // -----  Dean Thesis Approval for examiner  --- 
                public string PhdExaminerDirectorApproval(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_Examiner1Status", objStudent.PhdExaminer1Status);
                        objParams[2] = new SqlParameter("@P_Examiner2Status", objStudent.PhdExaminer2Status);
                        objParams[3] = new SqlParameter("@P_Examiner3Status", objStudent.PhdExaminer3Status);
                        objParams[4] = new SqlParameter("@P_Examiner4Status", objStudent.PhdExaminer4Status);
                        objParams[5] = new SqlParameter("@P_UANO", objStudent.Uano);
                        objParams[6] = new SqlParameter("@P_IPADDRESS", objStudent.IPADDRESS);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PHD_FINAL_EXAMINER", objParams, true);
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                //-----------------------------------------------------------------------------------------//
                ///---add for INSERT-- Progress report -- DCR  -- 22112017
                public int InsertPhdProgressDCR(FeeDemand objEntityClass)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[10];

                        objParams[0] = new SqlParameter("@P_SESSION_NO", objEntityClass.SessionNo);
                        objParams[1] = new SqlParameter("@P_IDNO", objEntityClass.StudentId);
                        objParams[2] = new SqlParameter("@P_ENROLLNO", objEntityClass.EnrollmentNo);
                        objParams[3] = new SqlParameter("@P_RECIEPTCODE", objEntityClass.ReceiptTypeCode);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", objEntityClass.SemesterNo);
                        objParams[5] = new SqlParameter("@P_PAYMENTTYPE", objEntityClass.PaymentTypeNo);
                        objParams[6] = new SqlParameter("@P_UA_NO", objEntityClass.UserNo);
                        objParams[7] = new SqlParameter("@P_RECIEPTNO", objEntityClass.Remark);
                        objParams[8] = new SqlParameter("@P_COUNTER_NO", objEntityClass.CounterNo);
                        objParams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        int ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_CREATE_DCR_FOR_PHD_PROGRESS", objParams, true));
                        if (ret == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.MarksEntryController.InsertStudentMarks() --> " + ex.Message + " " + ex.StackTrace);
                    }

                    return retStatus;

                }

                ///-------------------------------

                ///---Added for INSERT --Progress report -- DEMAND--22112017
                public int InsertPhdProgressDemand(FeeDemand objEntityClass)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_SESSION_NO", objEntityClass.SessionNo);
                        objParams[1] = new SqlParameter("@P_IDNO", objEntityClass.StudentId);
                        objParams[2] = new SqlParameter("@P_ENROLLNO", objEntityClass.EnrollmentNo);
                        objParams[3] = new SqlParameter("@P_RECIEPTCODE", objEntityClass.ReceiptTypeCode);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", objEntityClass.SemesterNo);
                        objParams[5] = new SqlParameter("@P_PAYMENTTYPE", objEntityClass.PaymentTypeNo);
                        objParams[6] = new SqlParameter("@P_UA_NO", objEntityClass.CounterNo);
                        objParams[7] = new SqlParameter("@P_COUNTER_NO", objEntityClass.FeeCatNo);
                        objParams[8] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        int ret = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_CREATE_DEMAND_FOR_PHD_PROGRESS", objParams, true));
                        if (ret == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.MarksEntryController.InsertStudentMarks() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return retStatus;
                }

                //--------phd Examiner Details Excel ----------------------29032019
                //-- Phd student details--26032018--//

                public DataSet RetrievePhdExaminerDetailsExcel(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_PHD_EXAMINER_DETAIL_EXCEL", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentFeesDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //------------------------------- DRC CHAIRMAN LIST EXCEL-------------------------//
                public DataSet RetrievePhdDrchairmanlistExcel(int degreeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_PHD_DRCCHAIRMAN_LIST_EXCEL", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentFeesDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                ///------------------Added by dipali on 06052019-- for  -- Annexure F student progress rpt details -------//

                public DataSet GETStudProgressRptDetails(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        ds = objSQLHelper.ExecuteDataSetSP("GET_PHD_STUDENT_PROGRESS_REPORT_DETAILS", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpCreateController.GETStudProgressRptDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                // =======================PHD NOC STUDENT DETAILS ============= Added by dipali on 07052019 =========//

                public DataSet GetPhdNOCDetails(string Rollno)
                {
                    DataSet dsPD = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ROLLNO", Rollno);
                        dsPD = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_GET_NOC_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dsPD;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentcontroller.GetPendingDocDetail-> " + ex.ToString());
                    }
                    return dsPD;
                }

                /// added by dipali on 07052019 -- update noc details.

                public string UpdateStudentNOCDetails(Student objStudent)
                {
                    string retun_status = string.Empty;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Update Student
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_STATUS", objStudent.Special);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_STUDENT_NOC", objParams, true);

                        retun_status = ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retun_status = "0";
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }

                    return retun_status;

                }

                //  added by dipali on 07052019  retriview noc details for excel

                public DataSet RetrievePhdNOCDetailsExcel()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        ds = objSQLHelper.ExecuteDataSet("PKG_ACD_PHD_STUDENT_NOC_DETAIL_EXCEL");
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentFeesDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                // =======================PHD WITHDRAW DETAILS ===== added by dipali on 08052019 =========================//

                public DataTableReader GetPhdWithdrawStudentDetails(int idno)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PHD_WITHDRAW_STUDENT_BYID", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return dtr;
                }

                //----- Submit Phd Withdraw details ------//

                public string AddPhdWithdrawDetails(Student objstudentinfo)
                {
                    string retStatus = string.Empty;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", objstudentinfo.IdNo);
                        objParams[1] = new SqlParameter("@P_REMARK", objstudentinfo.Remark);
                        objParams[2] = new SqlParameter("@P_ACTION", objstudentinfo.SuperRole);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int, 25);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_PHD_WITHDRAW_DETAILS", objParams, true);

                        //if (ret.ToString().Equals("-99"))
                        //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        //else
                        //    retStatus = Convert.ToInt32(ret);
                        retStatus = ret.ToString();

                    }
                    catch (Exception ex)
                    {
                        retStatus = "0";
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddStudentDetailsForProvisionalDegree-> " + ex.ToString());
                    }

                    return retStatus;
                }

                // ============================ SUPERVISOR UPLOADED THESIS =========Added by dipali on 09052019=====================//

                public DataTableReader GetPhdStudentSupervisiorThesisDetails(int idno)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PHD_STUDENT_THESIS_DETAILS_FOR_SUPERVISIOR", objParams).Tables[0].CreateDataReader();

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return dtr;
                }

                public string UpdatePhdThesisFile(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    //int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Update Student Details 
                        objParams = new SqlParameter[6];
                        //First Add Student Parameter
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_THS_NAME", objStudent.ThesisTitle);
                        objParams[2] = new SqlParameter("@P_THS_FILE", objStudent.PhdExaminerFile1);
                        objParams[3] = new SqlParameter("@P_UANO", objStudent.Uano);
                        objParams[4] = new SqlParameter("@P_IPADDRESS", objStudent.IPADDRESS);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PHD_UPD_SUPERVISIOR_THESIS_DETAILS", objParams, true);

                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                //-- Phd student details--26032018--//
                public DataSet RetrievePhdWithdrawExcel()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        ds = objSQLHelper.ExecuteDataSet("PKG_ACD_PHD_WITHDRAW_DETAIL_EXCEL");

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentFeesDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                // --------------------Mtech Thesis Extension --------  Added by dipali on 30072019  -------------------//
                public DataTableReader GetMtechThesisExtendedDetails(int idno)
                {
                    DataTableReader dtr = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_MTECH_STUDENT_EXTEND_DATE_DETAILS", objParams).Tables[0].CreateDataReader();

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return dtr;
                }

                // --------------- Save Mtech thesis Details --------------------//
                public int SubmitMtechThesisExtensionDetails(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        //First Add Student Parameter
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_THS_EXTENDDATE", objStudent.ApprovedDate);
                        objParams[2] = new SqlParameter("@P_THS_FILE_NAME", objStudent.ThesisTitle);
                        objParams[3] = new SqlParameter("@P_THS_FILE_PATH", objStudent.PhdExaminerFile1);
                        objParams[4] = new SqlParameter("@P_THS_EXTUANO", objStudent.Uano);
                        objParams[5] = new SqlParameter("@P_THS_IP", objStudent.IPADDRESS);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_MTECH_STUDENT_EXTEND_DATE_DETAILS", objParams, true);

                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return retStatus;
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }

                // ================ PhD Progress report not submit student list -- added by dipali on 20082019  ===========//
                public DataSet RetrievePhdProgressNotSubmitExcel(int degree, int session)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_DEGREENO", degree);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", session);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_PHD_PROGRESS_NOTSUBMIT_DETAIL_EXCEL", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentFeesDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                //============== PHD Tracker get synopsis  and thesis details  =============//

                public DataSet RetrievePhdTrackerDoumentsDetails(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_TRACKER_DOCUMENTS_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrievePhdTrackerDoumentsDetails-> " + ex.ToString());
                    }
                    return ds;
                }


                public string AddPhdExaminerDetails(Student objstudentinfo, int status)
                {
                    string retStatus = string.Empty;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New student
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_IDNO", objstudentinfo.IdNo);
                        objParams[1] = new SqlParameter("@P_NAME", objstudentinfo.StudName);
                        objParams[2] = new SqlParameter("@P_ADDRESS", objstudentinfo.PAddress);
                        objParams[3] = new SqlParameter("@P_MOBILE", objstudentinfo.StudentMobile);
                        objParams[4] = new SqlParameter("@P_CONTACT", objstudentinfo.PMobile);
                        objParams[5] = new SqlParameter("@P_EMAILID", objstudentinfo.EmailID);
                        objParams[6] = new SqlParameter("@P_EXTERNAL", objstudentinfo.NADID);

                        objParams[7] = new SqlParameter("@P_OTHERMOBILE", objstudentinfo.FatherMobile);  //---add other mobile ,contact no --16112018
                        objParams[8] = new SqlParameter("@P_OTHERCONTACT", objstudentinfo.MotherMobile);
                        objParams[9] = new SqlParameter("@P_UANO", objstudentinfo.Uano);
                        objParams[10] = new SqlParameter("@P_IPADDRESS", objstudentinfo.IPADDRESS);
                        objParams[11] = new SqlParameter("@P_STATUS", status);

                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int, 25);
                        objParams[12].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_PHD_EXAMINER_MASTER", objParams, true);

                        //if (ret.ToString().Equals("-99"))
                        //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        //else
                        //    retStatus = Convert.ToInt32(ret);
                        retStatus = ret.ToString();

                    }
                    catch (Exception ex)
                    {
                        retStatus = "0";
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddStudentDetailsForProvisionalDegree-> " + ex.ToString());
                    }

                    return retStatus;
                }


                public DataSet GETPhdExaminerDetails(int usertype)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_USERTYPE", usertype);
                        ds = objSQLHelper.ExecuteDataSetSP("GET_PHD_EXAMINER_MASTER_DETAILS", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentController.GET_PHD_EXAMINER_MASTER_DETAILS-> " + ex.ToString());
                    }
                    return ds;
                }



                //Added by Sneha Doble on 22/09/2020 
                public DataSet GetFacultyRemarkforphdProgress(int session, int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_FACULTY_REMARKS_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetFacultyRemarkforphdProgress-> " + ex.ToString());
                    }
                    return ds;
                }

                //End  by Sneha Doble on 22/09/2020 




                #region ExaminerMaster 22/01/2021

                public string AddMtechExaminerDetails(Student objstudentinfo, int status, string Department, string BranchName, string Designation)
                {
                    string retStatus = string.Empty;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New student
                        objParams = new SqlParameter[20];
                        objParams[0] = new SqlParameter("@P_IDNO", objstudentinfo.IdNo);
                        objParams[1] = new SqlParameter("@P_NAME", objstudentinfo.StudName);
                        objParams[2] = new SqlParameter("@P_ADDRESS", objstudentinfo.PAddress);
                        objParams[3] = new SqlParameter("@P_MOBILE", objstudentinfo.StudentMobile);
                        objParams[4] = new SqlParameter("@P_CONTACT", objstudentinfo.PMobile);
                        objParams[5] = new SqlParameter("@P_EMAILID", objstudentinfo.EmailID);
                        objParams[6] = new SqlParameter("@P_EXTERNAL", objstudentinfo.NADID);

                        objParams[7] = new SqlParameter("@P_OTHERMOBILE", objstudentinfo.FatherMobile);  //---add other mobile ,contact no --16112018
                        objParams[8] = new SqlParameter("@P_OTHERCONTACT", objstudentinfo.MotherMobile);
                        objParams[9] = new SqlParameter("@P_UANO", objstudentinfo.Uano);
                        objParams[10] = new SqlParameter("@P_IPADDRESS", objstudentinfo.IPADDRESS);
                        objParams[11] = new SqlParameter("@P_STATUS", status);

                        objParams[12] = new SqlParameter("@P_DEPARTMENT", Department);  //---add by Sneha Doble on 10/12/2020
                        objParams[13] = new SqlParameter("@P_BANKNO", objstudentinfo.BankNo);
                        objParams[14] = new SqlParameter("@P_ACCNO", objstudentinfo.AccNo);
                        objParams[15] = new SqlParameter("@P_ACC_HOLDERNAME", objstudentinfo.Accholdername);
                        objParams[16] = new SqlParameter("@P_IFSC_CODE", objstudentinfo.IFSCcode);
                        objParams[17] = new SqlParameter("@P_BRANCHNAME", BranchName);
                        objParams[18] = new SqlParameter("@P_DESIGNATION", Designation);
                        objParams[19] = new SqlParameter("@P_OUT", SqlDbType.Int, 25);
                        objParams[19].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_MTECH_EXAMINER_MASTER", objParams, true);

                        //if (ret.ToString().Equals("-99"))
                        //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        //else
                        //    retStatus = Convert.ToInt32(ret);
                        retStatus = ret.ToString();

                    }
                    catch (Exception ex)
                    {
                        retStatus = "0";
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddMtechExaminerDetails-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GETMtechExaminerDetails(int usertype)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_USERTYPE", usertype);
                        ds = objSQLHelper.ExecuteDataSetSP("GET_MTECH_EXAMINER_MASTER_DETAILS", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentController.GETMtechExaminerDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GETMtechExaminerDetailsIDNO(int idno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("GET_MTECH_EXAMINER_MASTER_DETAILS_IDNO", objParams);
                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentController.GETMtechExaminerDetailsIDNO-> " + ex.ToString());
                    }
                    return ds;
                }

                //Mtech External Examiner Entry 23/01/2021
                public DataSet GETStudentDetailsExaminerAllotment(int sessionno, int degreeno, int branchno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_MTECH_EXAMINER_ENTRY", objParams);
                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentController.GETStudentDetailsExaminerAllotment-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GETStudentDetailsExaminerAllotmentStatus(int sessionno, int degreeno, int branchno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_MTECH_EXAMINER_ENTRY_WITH_HOD_STATUS", objParams);
                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentController.GETStudentDetailsExaminerAllotmentStatus-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddMTECHExternalExaminerEntry(Phd objphd)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[15];

                        objParams[0] = new SqlParameter("@P_IDNO", objphd.IDNOS);
                        objParams[1] = new SqlParameter("@P_REGNO", objphd.Regnos);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", objphd.Sessionno);
                        objParams[3] = new SqlParameter("@P_DEGREENO", objphd.DegreeNo);
                        objParams[4] = new SqlParameter("@P_BRANCHNO", objphd.BranchNo);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objphd.College_Code);
                        objParams[6] = new SqlParameter("@P_UANO", objphd.Uano);
                        objParams[7] = new SqlParameter("@P_GUIDE1", objphd.Guide1);
                        objParams[8] = new SqlParameter("@P_GUIDE2", objphd.Guide2);
                        objParams[9] = new SqlParameter("@P_GUIDE3", objphd.Guide3);
                        objParams[10] = new SqlParameter("@P_EXAMINER1", objphd.Examiner1);
                        objParams[11] = new SqlParameter("@P_EXAMINER2", objphd.Examiner2);
                        objParams[12] = new SqlParameter("@P_EXAMINER3", objphd.Examiner3);
                        objParams[13] = new SqlParameter("@P_DISSERTATION", objphd.Dissertation);
                        objParams[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[14].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_MTECH_EXTERNAL_EXAMINER_ENRTY", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PhdController.AddMTECHExternalExaminerEntry-> " + ex.ToString());
                    }

                    return retStatus;

                }

                public DataSet GETStudentDetailsExaminerAllotmentAdmin(int sessionno, int degreeno, int branchno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", branchno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_MTECH_EXAMINER_ENTRY_DETAILS_ADMIN", objParams);
                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.studentController.GETStudentDetailsExaminerAllotmentAdmin-> " + ex.ToString());
                    }
                    return ds;
                }

                public int ExaminerAllotmentByAdmin(Phd objphd)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[9];

                        objParams[0] = new SqlParameter("@P_IDNO", objphd.IdNo);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", objphd.Sessionno);
                        objParams[2] = new SqlParameter("@P_DEGREENO", objphd.DegreeNo);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", objphd.BranchNo);
                        objParams[4] = new SqlParameter("@P_EXAMINER1STATUS", objphd.PhdExaminer1Status);
                        objParams[5] = new SqlParameter("@P_EXAMINER2STATUS", objphd.PhdExaminer2Status);
                        objParams[6] = new SqlParameter("@P_EXAMINER3STATUS", objphd.PhdExaminer3Status);
                        objParams[7] = new SqlParameter("@P_UANO", objphd.Uano);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_EXAMINER_ALLOTMENT_STATUS_BY_ADMIN", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PhdController.ExaminerAllotmentByAdmin-> " + ex.ToString());
                    }

                    return retStatus;

                }



                public DataSet GetStudentsForPhdExamination(StudentRegist srObj, int organizationid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_IDNO",srObj.IDNO),
                     new SqlParameter("@P_ORGANIZATIONID",organizationid)//added by Dileep Kare on 31.12.2021
                   
                  
                };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_GET_STUDENT_FOR_CANCEL_ADDMISSION", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.AdmissionCancellationController.SearchStudents() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
                #endregion

                //-------------------------------------------Phd--------------------------------------------------------------------
                //ADDED BY jay takalkhede 12-11-2022
                public DataSet GetStudentListForPhd(int AdmBatch, int Department, int PhDMode, int COLLEGE_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", AdmBatch);
                        objParams[1] = new SqlParameter("@P_PHD_MODE", PhDMode);
                        objParams[2] = new SqlParameter("@P_DEPARTMENT_NO", Department);
                        objParams[3] = new SqlParameter("@P_COLLEGE_ID", COLLEGE_ID);
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_GET_PHD_STUDENT_REGISTRATION_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentListForPhd-> " + ex.ToString());
                    }
                    return ds;
                }
                //ADDED BY jay takalkhede 12-11-2022
                public int InsPhDApproved(StudentRegist objSR, int AdmBatch, int Department, int userno, int PhDMode, int COLLEGE_ID)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_ADMBATCH  ", AdmBatch);
                        objParams[1] = new SqlParameter("@P_THS_IP", objSR.IPADDRESS);
                        objParams[2] = new SqlParameter("@P_THS_EXTUANO", objSR.UA_NO);
                        objParams[3] = new SqlParameter("@P_PHD_MODE ", PhDMode);
                        objParams[4] = new SqlParameter("@P_DEPARTMENT_NO", Department);
                        objParams[5] = new SqlParameter("@P_USERNO", userno);
                        objParams[6] = new SqlParameter("@P_COLLEGE_ID", COLLEGE_ID);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_APPROVE_PHD_STUDENT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(ret);
                        else
                            retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AddPhotoCopyRegisteration-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //ADDED BY jay takalkhede 12-11-2022
                //changes made by Nehal N on 18082023
                public int InsPhdSchedule(StudentRegist objSR, int AdmBatch, int PhDSch, string Venue, DateTime Config_SDate, string STime, string COLLEGE_ID, int exammode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New eXAM Registered Subject Details
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_ADMBATCH  ", AdmBatch);
                        objParams[1] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                        objParams[2] = new SqlParameter("@P_REGISTERED_BY", objSR.UA_NO);
                        objParams[3] = new SqlParameter("@P_SCHEDULEFOR ", PhDSch);
                        objParams[4] = new SqlParameter("@P_VENUE", Venue);
                        objParams[5] = new SqlParameter("@P_SCHEDULEDATE", Config_SDate);
                        objParams[6] = new SqlParameter("@P_SCHEDULETIME", STime);
                        objParams[7] = new SqlParameter("@P_COLLEGE_ID", COLLEGE_ID);
                        objParams[8] = new SqlParameter("@P_EXAM_MODE", exammode);
                        objParams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_PHD_SELECTION_SCHEDULE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(ret);
                        else
                            retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.InsPhDApproved-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //ADDED BY jay takalkhede 12-11-2022
                //changes made by Nehal N on 18082023
                public int UpdPhdSchedule(StudentRegist objSR, int AdmBatch, int PhDSch, string Venue, DateTime Config_SDate, string STime, int SCHNO, string COLLEGE_ID, int exammode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_ADMBATCH  ", AdmBatch);
                        objParams[1] = new SqlParameter("@P_SCHEDULEFOR ", PhDSch);
                        objParams[2] = new SqlParameter("@P_VENUE", Venue);
                        objParams[3] = new SqlParameter("@P_SCHEDULEDATE", Config_SDate);
                        objParams[4] = new SqlParameter("@P_SCHEDULETIME", STime);
                        objParams[5] = new SqlParameter("@P_SCHEDULENO", SCHNO);
                        objParams[6] = new SqlParameter("@P_COLLEGE_ID", COLLEGE_ID);
                        objParams[7] = new SqlParameter("@P_EXAM_MODE", exammode);
                        objParams[8] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                        objParams[9] = new SqlParameter("@P_REGISTERED_BY", objSR.UA_NO);
                        objParams[10] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPD_PHD_SELECTION_SCHEDULE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(ret);
                        else
                            retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.InsPhDApproved-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //ADDED BY jay takalkhede 12-11-2022
                public DataSet GetApprovedStudentListForPhd(int AdmBatch, int collegeid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", AdmBatch);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", collegeid);
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_GET_PHD_STUDENT_REGISTRATION_DETAILS_FOR_EMAIL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentListForPhd-> " + ex.ToString());
                    }
                    return ds;
                }
                //ADDED BY jay takalkhede 12-11-2022
                public int InsPhdScheduleemailLog(StudentRegist objSR, int userno, string username, int AdmBatch, int COLLEGE_ID)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New eXAM Registered Subject Details
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_ADMBATCH  ", AdmBatch);
                        objParams[1] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                        objParams[2] = new SqlParameter("@P_REGISTERED_BY", objSR.UA_NO);
                        objParams[3] = new SqlParameter("@P_USERNO ", userno);
                        objParams[4] = new SqlParameter("@P_USERNAME", username);
                        objParams[5] = new SqlParameter("@P_COLLEGE_ID", COLLEGE_ID);
                        objParams[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_PHD_SELECTION_SCHEDULE_EMAIL_LOG", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(ret);
                        else
                            retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.InsPhdSchedule-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //ADDED BY AASHNA 01-11-2022
                public int PhdApplicationMarkjEntry(int organizationid, string userno, int admbatch, int dept, string mode, string testmark, string inerviewmark, string total, string IPADDRESS, int UA_NO, int COLLEGE_ID)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_ORGANIZATION_ID", organizationid),
                            new SqlParameter("@P_USERNO", userno),
                            new SqlParameter("@P_ADMBATCH", admbatch),
                            new SqlParameter("@P_DEPARTMENT_NO", dept),
                            new SqlParameter("@P_MODE_OF_PURSUING",mode),
                            new SqlParameter("@P_TEST_MARK", testmark),
                            new SqlParameter("@P_INTERVIEW_MARK", inerviewmark),
                            new SqlParameter("@P_TOTAL_MARK", total),
                            new SqlParameter("@P_IP_ADDRESS", IPADDRESS),
                            new SqlParameter("@P_UA_NO", UA_NO),
                            new SqlParameter("@P_COLLEGE_ID ", COLLEGE_ID ),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)

                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_PHD_STUDENTS_FOR_ENTRY", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }
                //ADDED BY AASHNA 02-11-2022
                public int PhdMarkEntrySelectionList(string superv, int organizationid, string userno, int admbatch, int dept, string testmark, string inerviewmark, string total, string IPADDRESS, int UA_NO, int Collegeid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_SUPERVISOR_ID", superv),
                            new SqlParameter("@P_ORGANIZATION_ID", organizationid),
                            new SqlParameter("@P_USERNO", userno),
                            new SqlParameter("@P_ADMBATCH", admbatch),
                            new SqlParameter("@P_DEPARTMENT_NO", dept),
                            new SqlParameter("@P_TEST_MARK", testmark),
                            new SqlParameter("@P_INTERVIEW_MARK", inerviewmark),
                            new SqlParameter("@P_TOTAL_MARK", total),
                            new SqlParameter("@P_IP_ADDRESS", IPADDRESS),
                            new SqlParameter("@P_UA_NO", UA_NO),
                            new SqlParameter("@P_COLLEGE_ID", Collegeid),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)

                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_PHD_STUDENTS_FOR_ENTRY_SELECTION_VISIT", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }
                //added by aashna 01-11-2022
                public DataSet GetPhdMarkExcelReport(int admbatch, int PhDMode, int Department, int COLLEGE_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                        objParams[1] = new SqlParameter("@P_PHD_MODE", PhDMode);
                        objParams[2] = new SqlParameter("@P_DEPARTMENT_NO", Department);
                        objParams[3] = new SqlParameter("@P_COLLEGE_ID", COLLEGE_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_PHD_STUDENTS_FOR_ENTRY_EXCEL", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MarksEntryController.GetStudentsForMarkEntry-> " + ex.ToString());
                    }

                    return ds;
                }
                public DataSet GetApprovedScheduleListForPhd()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        ds = objSQLHelper.ExecuteDataSet("PKG_ACD_GET_PHD_SCHEDULE");

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveStudentFeesDetails-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet EditScheduleListForPhd(int SCHNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCHNO", SCHNO);
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_GET_PHD_SCHEDULE_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EditConfiguration-> " + ex.ToString());
                    }
                    return ds;
                }
                //Added bt Tejaswini Dhoble[17-12-2022]

                public DataSet GetEditDocumentMappingList(int ID, int mode)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_DOCUMENTNO", ID);
                        objParams[1] = new SqlParameter("@P_MODE", mode);
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_SELECTEDIT_DOCUMENTMAPPING", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PhdController.GetDocumentMappingList " + ex.ToString());
                    }
                    return ds;
                }
                //----------------------------------------------------------------------------------------------------------------------------------------------------------

                // ADDED BY Tejaswini Dhoble[17-12-2022]
                public int InsertDocumentMappingData(int id, string DocumentName, int status, int mandatory, int mode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_DOCUMENTNO",id),
                            new SqlParameter("@P_DOCUMENTNAME",DocumentName),
                            new SqlParameter("@P_ACTIVESTATUS",status),
                            new SqlParameter("@P_MANDATORY",mandatory),
                            new SqlParameter("@P_MODE",mode),
                            new SqlParameter("@P_OUT", SqlDbType.Int),
                        
                          };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_INSERTUPDATE_DOCUMENTMAPPING", sqlParams, true);

                        if (Convert.ToInt32(ret) == -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == 2627)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PhdController.InsertDocumentMappingData-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //------------------------------------------------------------------------------------------------------------------------------------------------------------    
                // ADDED BY Tejaswini Dhoble[17-12-2022] 
                public int UpdateDocumentMappingData(int id, string DocumentName, int status, int mandatory, int mode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_DOCUMENTNO",id),
                            new SqlParameter("@P_DOCUMENTNAME",DocumentName),
                            new SqlParameter("@P_ACTIVESTATUS",status),
                            new SqlParameter("@P_MANDATORY",mandatory),
                            new SqlParameter("@P_MODE",mode),
                            new SqlParameter("@P_OUT", SqlDbType.Int),
                        
                          };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("ACD_INSERTUPDATE_DOCUMENTMAPPING", sqlParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PhdController.UpdateDocumentMappingData-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //----------------------------------------------Added by Tejaswini 10012023 for PHD master-------------------------------------------------
                //---------------------------------------------------------------------------------------------------------

                //modified by Nehal nawghare on 16/03/2023
                public int InsertCommitteeData(int id, string committeeName, string status, int mode, int committeestatus)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_COMMITTEE_ID",id),
                            new SqlParameter("@P_COMMITTEE_NAME",committeeName),
                            new SqlParameter("@P_ACTIVESTATUS",status),
                            new SqlParameter("@P_MODE",mode),
                            new SqlParameter("@P_COMMITTEE_STATUS",committeestatus),
                            new SqlParameter("@P_OUT", SqlDbType.Int),
                        
                          };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERTUPDATE_PHDCOMMITTEE", sqlParams, true);

                        if (Convert.ToInt32(ret) == -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == 2627)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PhdController.InsertCommitteeData-> " + ex.ToString());
                    }
                    return retStatus;
                }


                //----------------------------------------------------------------------------------------------------------------------------------------------------------------------


                //modified by Nehal Nawghare  on 16/03/2023
                public int UpdateCommitteeData(int id, string committeeName, string status, int mode, int committeestatus)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                           new SqlParameter("@P_COMMITTEE_ID",id),
                            new SqlParameter("@P_COMMITTEE_NAME",committeeName),
                            new SqlParameter("@P_ACTIVESTATUS",status),
                            new SqlParameter("@P_MODE",mode),
                            new SqlParameter("@P_COMMITTEE_STATUS",committeestatus), 
                            new SqlParameter("@P_OUT", SqlDbType.Int),
                        
                          };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERTUPDATE_PHDCOMMITTEE", sqlParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PhdController.UpdateCommitteeData-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //-------------------------------------------------------------------------------------------------------------------------------------------------------------------

                public DataSet GetEditCommitteeData(int ID, int mode)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_COMMITTEE_ID", ID);
                        objParams[1] = new SqlParameter("@P_MODE", mode);
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_SELECTEDIT_PHDCOMMITTEE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PhdController.GetEditCommitteeData " + ex.ToString());
                    }
                    return ds;
                }

                //------------------------------------------------------------------------------------------------------------------------------------------------------------------

                public int InsertCommitteeDesignationData(int id, string Designation, int Externalstatus, int mode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_DESIG_ID",id),
                            new SqlParameter("@P_DESIGNATION",Designation),
                            new SqlParameter("@P_EXTERNALSTATUS",Externalstatus),
                            new SqlParameter("@P_MODE",mode),
                            new SqlParameter("@P_OUT", SqlDbType.Int),
                        
                          };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERTUPDATE_PHDCOMMITTEE_DESIGNATION", sqlParams, true);

                        if (Convert.ToInt32(ret) == -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == 2627)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PhdController.InsertCommitteeDesignationData-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //-----------------------------------------------------------------------------------------------------------------------------------------------------------------
                // Update BY Jay T. on dated (08092023)
                public int UpdateCommitteeDesignationData(int id, string Designation, int Externalstatus, int mode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                           new SqlParameter("@P_DESIG_ID",id),
                            new SqlParameter("@P_DESIGNATION",Designation),
                            new SqlParameter("@P_EXTERNALSTATUS",Externalstatus),
                            new SqlParameter("@P_MODE",mode),
                            new SqlParameter("@P_OUT", SqlDbType.Int),
                        
                          };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERTUPDATE_PHDCOMMITTEE_DESIGNATION", sqlParams, true);

                        if (Convert.ToInt32(ret) == -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == 2627)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PhdController.UpdateCommitteeDesignationData-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //--------------------------------------------------------------------------------------------------------------------------------------------------------------------

                public DataSet GetEditCommitteeDesignationData(int ID, int mode)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_DESIG_ID", ID);
                        objParams[1] = new SqlParameter("@P_MODE", mode);
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_SELECTEDIT_PHDCOMMITTEE_DESIGNATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PhdController.GetEditCommitteeDesignationData " + ex.ToString());
                    }
                    return ds;
                }

                //-----------------------------------------------------------------------------------------------------------------------------------------------------------------

                public int InsertCommitteeMappingData(int id, int committeeId, string DesigId, string status, int mode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_CMAPPING_ID",id),
                            new SqlParameter("@P_COMMITTEE_ID",committeeId),
                            new SqlParameter("@P_DESIG_ID",DesigId),
                            new SqlParameter("@P_ACTIVESTATUS",status),
                            new SqlParameter("@P_MODE",mode),
                            new SqlParameter("@P_OUT", SqlDbType.Int),
                        
                          };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERTUPDATE_COMMITTEE_MAPPING", sqlParams, true);

                        if (Convert.ToInt32(ret) == -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == 2627)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PhdController.InsertCommitteeMappingData-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //-------------------------------------------------------------------------------------------------------------------------------------------------------------

                public int UpdateCommitteeMappingData(int id, int committeeId, string DesigId, string status, int mode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                           new SqlParameter("@P_CMAPPING_ID",id),
                            new SqlParameter("@P_COMMITTEE_ID",committeeId),
                            new SqlParameter("@P_DESIG_ID",DesigId),
                            new SqlParameter("@P_ACTIVESTATUS",status),
                            new SqlParameter("@P_MODE",mode),
                            new SqlParameter("@P_OUT", SqlDbType.Int),
                          };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERTUPDATE_COMMITTEE_MAPPING", sqlParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PhdController.UpdateCommitteeMappingData-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //--------------------------------------------------------------------------------------------------------------------------------------------------------------------

                public DataSet GetEditCommitteeMappingData(int ID, int mode)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_COMMITTEE_ID", ID);
                        objParams[1] = new SqlParameter("@P_MODE", mode);
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_SELECTEDIT_PHDCOMMITTEE_MAPPING", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PhdController.GetEditCommitteeMappingData " + ex.ToString());
                    }
                    return ds;
                }
                //-----------------------------------------------------------------------------------------------------------------------
                //added by jay 07012023
                public DataSet GetInternalMemberMappingData()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_GET_PHD_INTERNAL_STUDENT_MAPPING", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EditConfiguration-> " + ex.ToString());
                    }
                    return ds;
                }
                //---------------------------------------------------------------------------------------------------------------------
                //update by jay takalkhede on dated 18082023 
                public int InsertInternalStudent(string UANO, int Designationno, int deptno, int collegeid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_UANO",UANO),
                            new SqlParameter("@P_Designationno",Designationno),
                            new SqlParameter("@P_COLLEGEID",collegeid),
                            new SqlParameter("@P_DEPARTMENTNO",deptno),
                            new SqlParameter("@P_OUT", SqlDbType.Int),
                        
                          };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INTERNAL_STUDENT_MAPPING", sqlParams, true);

                        if (Convert.ToInt32(ret) == -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == 2627)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PhdController.InsertCommitteeMappingData-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //add by jay takalkhede on dated 21082023 
                public int UpdateInternalStudent(string UANO, int Designationno, int deptno, int collegeid, int id)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_UANO",UANO),
                            new SqlParameter("@P_Designationno",Designationno),
                            new SqlParameter("@P_COLLEGEID",collegeid),
                            new SqlParameter("@P_DEPARTMENTNO",deptno),
                            new SqlParameter("@P_ID",id),
                            new SqlParameter("@P_OUT", SqlDbType.Int),
                        
                          };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_INTERNAL_STUDENT_MAPPING", sqlParams, true);

                        if (Convert.ToInt32(ret) == -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == 2627)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PhdController.InsertCommitteeMappingData-> " + ex.ToString());
                    }
                    return retStatus;
                }
                 //added by jay 19082023
                public DataSet EditInternalMemberMappingData(int ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ID", ID);
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_EDIT_PHD_INTERNAL_STUDENT_MAPPING", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EditInternalMemberMappingData-> " + ex.ToString());
                    }
                    return ds;
                }


                //-------------------------------------ADDED BY JAY T. ON DATED 19/01/2023--------------------------------------
                public DataSet GetJointlySinglyCount(int uano)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SUPERVISIORNO", uano);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_PHD_JOINTLY_SINGLY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentsForScheme-> " + ex.ToString());
                    }

                    return ds;
                }
                //-----------------------------------------------------------------------------------------------
                public string UpdateSupervisorPHDStudent(Phd ObjPhd, int NOFSP, int committe)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Update Student
                        objParams = new SqlParameter[22];
                        //First Add Student Parameter
                        objParams[0] = new SqlParameter("@P_IDNO", ObjPhd.IdNo);
                        objParams[1] = new SqlParameter("@P_NDGS", NOFSP);
                        objParams[2] = new SqlParameter("@P_SUPERROLE", ObjPhd.SuperRole);
                        objParams[3] = new SqlParameter("@P_COMMITTENO", committe);

                        objParams[4] = new SqlParameter("@P_SUPERVISOR_UANO", ObjPhd.SupervisorNo);
                        objParams[5] = new SqlParameter("@P_SUPERVISOR_DISGNNO", ObjPhd.SupervisormemberNo);
                        objParams[6] = new SqlParameter("@P_SUPERVISOR_EXT_UANO", ObjPhd.SupervisorStatus);
                        objParams[7] = new SqlParameter("@P_JOINTSUPERVISOR1_UANO", ObjPhd.JoinsupervisorNo);

                        objParams[8] = new SqlParameter("@P_JOINSUPERVISOR1_DISGNNO", ObjPhd.JoinsupervisormemberNo);
                        objParams[9] = new SqlParameter("@P_JOINTSUPERVISOR1_EXT_UANO", ObjPhd.JoinsupervisorStatus);
                        objParams[10] = new SqlParameter("@P_INSTITUTEFACULTY_UANO", ObjPhd.InstitutefacultyNo);
                        objParams[11] = new SqlParameter("@P_INSTITUTEFAC_DISGNNO", ObjPhd.InstitutefacmemberNo);
                        objParams[12] = new SqlParameter("@P_INSTITUTEFACULTY_EXT_UANO", ObjPhd.InstitutefacultyStatus);
                        objParams[13] = new SqlParameter("@P_JOINTSUPERVISOR2_UANO", ObjPhd.Secondjoinsupervisorno);
                        objParams[14] = new SqlParameter("@P_JOINSUPERVISOR2_DISGNNO", ObjPhd.Secondjoinsupervisormemberno);
                        objParams[15] = new SqlParameter("@P_JOINTSUPERVISOR2_EXT_UANO", ObjPhd.Secondjoinsupervisorstatus);
                        objParams[16] = new SqlParameter("@P_DRC_UANO", ObjPhd.DrcNo);
                        objParams[17] = new SqlParameter("@P_DRC_DISGNNO", ObjPhd.DrcmemberNo);
                        objParams[18] = new SqlParameter("@P_DRC_EXT_UANO", ObjPhd.Drcstatus);
                        objParams[19] = new SqlParameter("@P_DRCCHAIRMAN_UANO", ObjPhd.DrcChairNo);
                        objParams[20] = new SqlParameter("@P_RESEARCH", ObjPhd.Research);
                        objParams[21] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[21].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PHD_INSERT_ALLOTED_SUPERVISOR", objParams, true);
                        return ret.ToString();
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudent-> " + ex.ToString());
                    }
                }
                //----------------------------------------------------------------------------------
                public string RejectSupervisor(Phd objPhd, string Remark)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Update drc 
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_IDNO", objPhd.IdNo);
                        objParams[1] = new SqlParameter("@P_REMARK", Remark);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PHD_SUPERVISOR_REJECT", objParams, true);

                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RejectSupervisorStatus-> " + ex.ToString());
                    }
                }
                //--------------------------------------------------------------------------------------------------------------------------------

                public string ApprovedSupervisor(Phd objPhd)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Update drc 
                        objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_IDNO", objPhd.IdNo);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PHD_SUPERVISOR_APPROVED", objParams, true);

                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return ret.ToString();

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RejectSupervisorStatus-> " + ex.ToString());
                    }
                }
                //--------------------------------------------------------------------------
                public int InsertStudentRegistration(StudentRegist objSR)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_SESSIONNO", objSR.SESSIONNO),
                            new SqlParameter("@P_IDNO", objSR.IDNO),
                            new SqlParameter("@P_SEMESTERNO", objSR.SEMESTERNO),
                            new SqlParameter("@P_SCHEMENO", objSR.SCHEMENO),
                            new SqlParameter("@P_COURSENOS", objSR.COURSENOS),
                            new SqlParameter("@P_BACK_COURSENOS", objSR.Backlog_course),
                            new SqlParameter("@P_CREG_IDNO", objSR.UA_NO),
                            new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS),
                            new SqlParameter("@P_COLLEGE_CODE", objSR.COLLEGE_CODE),
                            new SqlParameter("@P_REGNO", objSR.REGNO),
                            new SqlParameter("@P_ROLLNO", objSR.ROLLNO),
                            new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PHD_PREREGIST_SP_INS_STUDENT_REGISTRATION", sqlParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistration.UdpateRegistrationByHOD-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //--------------------------------------------------JAY T. 04/02/2023------------------------------------

                //MODIFIED by nehal nawghare 17/03/2023
                public int InsertMeetingData(string IDNO, int Meetingno, string meetingtitle, DateTime meetingdate, string desc, string filename, int meetno, int uano, string ipaddress, string intuano, string extuano)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_IDNO",IDNO),
                            new SqlParameter("@P_MEETINGNO",Meetingno),
                            new SqlParameter("@P_MEETING_TITLE",meetingtitle),
                            new SqlParameter("@P_MEETINGDATE",meetingdate),
                            new SqlParameter("@P_DESCRIPTION",desc),
                            new SqlParameter("@P_FILENAME",filename),
                            new SqlParameter("@P_MEETING_NUMBER",meetno),
                            new SqlParameter("@P_UA_NO",uano),
                            new SqlParameter("@P_IP_ADDRESS",ipaddress),
                            new SqlParameter("@P_INTERNAL_SUPERVISOR_UANO",intuano),
                            new SqlParameter("@P_EXTERNAL_SUPERVISOR_UANO",extuano),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int),
                        
                          };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PHD_INS_UPD_MEETING_DETAILS", sqlParams, true);

                        if (Convert.ToInt32(ret) == -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PhdController.InsertCommitteeData-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //--------------------------------------------------21022023- added by jay takalkhede-----------------------------------
                //modified by nehal on 27032023

                //public int InsertProgressReport(int idno, int admbatch, int degree, int branch, int semester, int TNC, string ROLLNO, string RESEARCH, string work, string remark, string comment, int gread, int uano, string ipaddress, int collegeid)
                public int InsertProgressReport(int idno, int admbatch, int degree, int branch, int semester, int TNC, string ROLLNO, string RESEARCH, string work, int uano, string ipaddress, int collegeid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_IDNO",idno),
                            new SqlParameter("@P_ADMBATCH",admbatch),
                            new SqlParameter("@P_DEGREENO",degree),
                            new SqlParameter("@P_BRANCHNO",branch),
                            new SqlParameter("@P_SEMESTERNO",semester),
                             new SqlParameter("@P_TOTAL_NO_CREDITS",TNC),
                            new SqlParameter("@P_ROLL_NO",ROLLNO),
                            new SqlParameter("@P_RESEARCH_TOPIC",RESEARCH),
                            new SqlParameter("@P_WORK_DESC",work),
                            //  new SqlParameter("@P_WORK_REMARK",remark),
                            //new SqlParameter("@P_COMMENTS",comment),
                            //new SqlParameter("@P_GREAD_AWARDED",gread),
                            new SqlParameter("@P_REGISTERED_BY",uano),
                            new SqlParameter("@P_IPADDRESS",ipaddress),
                            new SqlParameter("@P_COLLEGE_ID",collegeid),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int),
                        
                          };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PHD_INSERT_PROGRESS_REPORT", sqlParams, true);

                        if (Convert.ToInt32(ret) == -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else if (Convert.ToInt32(ret) == 2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PhdController.InsertCommitteeData-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //--------------------------------------------------21022023- added by jay takalkhede-----------------------------------
                public DataSet GetPROGRESSReport(int idno, int semesterno)
                {
                    DataSet dtr = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);

                        // dtr = objSQLHelper.ExecuteDataSetSP("PKG_PHD_GET_PROGRESS_REPORT_DATA", objParams).Tables[0].CreateDataReader();
                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PHD_GET_PROGRESS_REPORT_DATA", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return dtr;
                }
                //added by nehal on 27/03/23
                //MODIFIED by nehal on 28/03/23
                public DataSet RetrieveStudentDetailsPHDforFaculty(int uano, int admbatch)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_UNO", uano);
                        objParams[1] = new SqlParameter("@P_ADMBATCH", admbatch);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_PHD_STUDENT_FACULTY", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PhdController.RetrieveStudentDetailsPHDforFaculty-> " + ex.ToString());
                    }
                    return ds;
                }
                //--------------------------------------------------JAY T. 30/03/2023------------------------------------
                public int InsertSynopsisData(int IDNO, string filename, string title)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_IDNO",IDNO),
                            new SqlParameter("@P_FILENAME",filename),
                            new SqlParameter("@P_TITLE",title),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int),
                        
                          };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PHD_INS_PRE_SYNOPSIS", sqlParams, true);

                        if (Convert.ToInt32(ret) == -99)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PhdController.InsertSynopsisData-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //------------------------------------------------------------------------------------------------------------
                public int ApprovePre_Synopsis_Report(int idno, string role)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_IDNO",idno),
                            new SqlParameter("@P_ROLE",role),
                            new SqlParameter("@P_OUT", SqlDbType.Int),
                        
                          };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PHD_APPROVE_PRE_SYNOPSIS", sqlParams, true);

                        if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PhdController.InsertCommitteeData-> " + ex.ToString());
                    }
                    return retStatus;
                }


                //--------------------------------------------------------------------------------------------------------------------

                //modified by nehal on 27/03/2023
                //public int ApproveProgressReport(int idno, int admbatch, int degree, int branch, int semester, int collegeid, string SUPERROLE, int uano, string role, int ext_status)
                public int ApproveProgressReport(int idno, int admbatch, int degree, int branch, int semester, int collegeid, string SUPERROLE, int uano, string role, int ext_status, string remark, string comment, int gread)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_IDNO",idno),
                            new SqlParameter("@P_ADMBATCH",admbatch),
                            new SqlParameter("@P_DEGREENO",degree),
                            new SqlParameter("@P_BRANCHNO",branch),
                            new SqlParameter("@P_SEMESTERNO",semester),
                            new SqlParameter("@P_COLLEGE_ID",collegeid),
                            new SqlParameter("@P_SUPERROLE",SUPERROLE),
                            new SqlParameter("@P_UANO",uano),
                            new SqlParameter("@P_ROLE",role),
                            new SqlParameter("@P_FLAG ",ext_status),
                              new SqlParameter("@P_WORK_REMARK",remark),
                            new SqlParameter("@P_COMMENTS",comment),
                            new SqlParameter("@P_GREAD_AWARDED",gread),
                            new SqlParameter("@P_OUT", SqlDbType.Int),
                        
                          };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PHD_INSERT_PROGRESS_REPORT_APPROVAL", sqlParams, true);

                        if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PhdController.InsertCommitteeData-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //--------------------------------------------------------------------Added by Nehal 24-02-2023----------------------------------------------------------------
                /// <summary>
                /// Added by Nehal 24-02-2023
                /// </summary>
                /// <returns></returns>
                /// 

                public int InsertPhdThesisEntry(string idno, string thesis_title, DateTime thesis_subdate, DateTime synopis_subdate)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_THESIS_TITLE", thesis_title);
                        objParams[2] = new SqlParameter("@P_THESIS_SUB_DATE", thesis_subdate);
                        objParams[3] = new SqlParameter("@P_SYNOPSIS_SUB_DATE", synopis_subdate);
                        objParams[4] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_PHD_THESIS_ENTRY_SP_INS_MODIFIED", objParams, true);

                        if (Convert.ToInt32(ret) == 2627)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PhdController.InsertPhdThesisEntry-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetAllStudentThesisList(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUD_THESIS_ENTRY_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PhdController.GetAllStudentThesisList-> " + ex.ToString());
                    }
                    return ds;
                }

                //added by nehal on 08082023
                public int InsertThesisApprovalEntry(int idno, Phd objPhd)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[11];

                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SUBMISSION_STATUS_NO", objPhd.SUBMISSION_STATUS_NO);
                        //objParams[2] = new SqlParameter("@P_DISPATCHEDTOREVIEWER_STATUS_NO", objPhd.DISPATCHEDTOREVIEWER_STATUS_NO);
                        //objParams[3] = new SqlParameter("@P_REVIEWERREPORTRECEIVED_STATUS_NO", objPhd.REVIEWERREPORTRECEIVED_STATUS_NO);
                        //objParams[4] = new SqlParameter("@P_OPENDEFENCEVIVASCHEDULE_STATUS_NO", objPhd.OPENDEFENCEVIVASCHEDULE_STATUS_NO);
                        //objParams[5] = new SqlParameter("@P_AWARDED_STATUS_NO", objPhd.AWARDED_STATUS_NO);
                        objParams[2] = new SqlParameter("@P_SUBMISSION_UANO", objPhd.SUBMISSION_UANO);
                        //objParams[7] = new SqlParameter("@P_DISPATCHEDTOREVIEWER_UANO", objPhd.DISPATCHEDTOREVIEWER_UANO);
                        //objParams[8] = new SqlParameter("@P_REVIEWERREPORTRECEIVED_UANO", objPhd.REVIEWERREPORTRECEIVED_UANO);
                        //objParams[9] = new SqlParameter("@P_OPENDEFENCEVIVASCHEDULE_UANO", objPhd.OPENDEFENCEVIVASCHEDULE_UANO);
                        //objParams[10] = new SqlParameter("@P_AWARDED_UANO", objPhd.AWARDED_UANO);
                        objParams[3] = new SqlParameter("@P_EXAMINER_TYPE_NO", objPhd.EXAMINER_TYPE_NO);
                        objParams[4] = new SqlParameter("@P_FLAG", objPhd.FLAG);
                        //objParams[13] = new SqlParameter("@P_SUBMISSION_STATUS", objPhd.SUBMISSION_STATUS);
                        objParams[5] = new SqlParameter("@P_SUBMISSION_IP", objPhd.SUBMISSION_IP);
                        //objParams[15] = new SqlParameter("@P_SUBMISSION_REMARK", objPhd.SUBMISSION_REMARK);
                        //objParams[16] = new SqlParameter("@P_DISPATCHEDTOREVIEWER_STATUS", objPhd.DISPATCHEDTOREVIEWER_STATUS);
                        //objParams[17] = new SqlParameter("@P_DISPATCHEDTOREVIEWER_IP", objPhd.DISPATCHEDTOREVIEWER_IP);
                        //objParams[18] = new SqlParameter("@P_DISPATCHEDTOREVIEWER_REMARK", objPhd.DISPATCHEDTOREVIEWER_REMARK);
                        //objParams[19] = new SqlParameter("@P_REVIEWERREPORTRECEIVED_STATUS", objPhd.REVIEWERREPORTRECEIVED_STATUS);
                        //objParams[20] = new SqlParameter("@P_REVIEWERREPORTRECEIVED_IP", objPhd.REVIEWERREPORTRECEIVED_IP);
                        //objParams[21] = new SqlParameter("@P_REVIEWERREPORTRECEIVED_REMARK", objPhd.REVIEWERREPORTRECEIVED_REMARK);
                        //objParams[22] = new SqlParameter("@P_OPENDEFENCEVIVASCHEDULE_STATUS", objPhd.OPENDEFENCEVIVASCHEDULE_STATUS);
                        //objParams[23] = new SqlParameter("@P_OPENDEFENCEVIVASCHEDULE_IP", objPhd.OPENDEFENCEVIVASCHEDULE_IP);
                        //objParams[24] = new SqlParameter("@P_OPENDEFENCEVIVASCHEDULE_REMARK", objPhd.OPENDEFENCEVIVASCHEDULE_REMARK);
                        //objParams[25] = new SqlParameter("@P_AWARDED_STATUS", objPhd.AWARDED_STATUS);
                        //objParams[26] = new SqlParameter("@P_AWARDED_IP", objPhd.AWARDED_IP);
                        //objParams[27] = new SqlParameter("@P_AWARDED_REMARK", objPhd.AWARDED_REMARK);
                        objParams[6] = new SqlParameter("@P_EXAMINER_TYPE", objPhd.EXAMINER_TYPE);
                        objParams[7] = new SqlParameter("@P_FLAG_NAME", objPhd.FLAG_NAME);
                        objParams[8] = new SqlParameter("@P_FLAG_REMARK", objPhd.FLAG_REMARK);
                        objParams[9] = new SqlParameter("@P_SUBMISSION_CREATE_DATE", objPhd.SUBMISSION_CREATE_DATE);
                        //objParams[32] = new SqlParameter("@P_DISPATCHEDTOREVIEWER_CREATE_DATE", objPhd.DISPATCHEDTOREVIEWER_CREATE_DATE);
                        //objParams[33] = new SqlParameter("@P_REVIEWERREPORTRECEIVED_CREATE_DATE", objPhd.REVIEWERREPORTRECEIVED_CREATE_DATE);
                        //objParams[34] = new SqlParameter("@P_OPENDEFENCEVIVASCHEDULE_CREATE_DATE", objPhd.OPENDEFENCEVIVASCHEDULE_CREATE_DATE);
                        //objParams[35] = new SqlParameter("@P_AWARDED_CREATE_DATE", objPhd.AWARDED_CREATE_DATE);

                        //objParams[36] = new SqlParameter("@P_FOREIGN_EXAMINER_REPORT_RECEIVED_STATUS_NO", objPhd.FOREIGN_EXAMINER_REPORT_RECEIVED_STATUS_NO);
                        //objParams[37] = new SqlParameter("@P_FOREIGN_EXAMINER_REPORT_RECEIVED_UANO", objPhd.FOREIGN_EXAMINER_REPORT_RECEIVED_UANO);
                        //objParams[38] = new SqlParameter("@P_FOREIGN_EXAMINER_REPORT_RECEIVED_STATUS", objPhd.FOREIGN_EXAMINER_REPORT_RECEIVED_STATUS);
                        //objParams[39] = new SqlParameter("@P_FOREIGN_EXAMINER_REPORT_RECEIVED_IP", objPhd.FOREIGN_EXAMINER_REPORT_RECEIVED_IP);
                        //objParams[40] = new SqlParameter("@P_FOREIGN_EXAMINER_REPORT_RECEIVED_REMARK", objPhd.FOREIGN_EXAMINER_REPORT_RECEIVED_REMARK);
                        //objParams[41] = new SqlParameter("@P_FOREIGN_EXAMINER_REPORT_RECEIVED_CREATE_DATE", objPhd.FOREIGN_EXAMINER_REPORT_RECEIVED_DATE);

                        objParams[10] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPD_PHD_THESIS_ENTRY", objParams, true);

                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        return retStatus;

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PhdController.InsertThesisApprovalEntry-> " + ex.ToString());
                    }
                }

                public DataSet GetAllStudStatusDetails(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUD_BIND_STATUS_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PhdController.GetAllStudStatusDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAllStudentApprovalThesisList(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUD_THESIS_APPROVAL_ENTRY_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PhdController.GetAllStudentThesisList-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentDataExcel(int idno)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUD_THESIS_APPROVAL_ENTRY_DETAILS_EXCEL", objParams);


                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PhdController.GetStudentDataExcel --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
                // -------------------------------------------Added By Nehal Nawghare on dated 23/03/2023-------------------------------------------------
                //added by nehal on 22/03/2023
                public DataSet GetAllPhdSession(int sessionid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ADM_SESSION_ID", sessionid);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_PHD_ADMISSION_SESSION", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PhdController.GetAllPhdSession-> " + ex.ToString());
                    }
                    return ds;
                }

                public int Add_Phd_Admission_Session(int sessionid, string Session_Name, DateTime Sdate, DateTime Edate, bool IsActive)
                {
                    int status = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_ADM_SESSION_ID", sessionid);
                        objParams[1] = new SqlParameter("@P_SESSION_NAME", Session_Name);
                        objParams[2] = new SqlParameter("@P_SESSION_STDATE", Sdate);
                        objParams[3] = new SqlParameter("@P_SESSION_ENDDATE", Edate);
                        objParams[4] = new SqlParameter("@P_ACTIVE_STATUS", IsActive);
                        objParams[5] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_PHD_ADMISSION_SESSION_INS", objParams, true);

                        if (obj.ToString().Equals("2"))
                        {
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else if (obj.ToString().Equals("1"))
                        {
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PhdController.Add_Phd_Admission_Session-> " + ex.ToString());
                    }
                    return status;
                }
                //--------------------------------------------------------------------------------------------------------------------------------------
                //Added by nehal on 21/03/2023
                public DataSet GetAllExaminerList(int id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ID", id);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_PHD_EXAMINER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PhdController.GetAllExaminerList-> " + ex.ToString());
                    }
                    return ds;
                }

                // updated by jay takalkhede on dated 26042023
                public int InsertExaminer(int id, string examiner_name, string institute_name, string mobileno, int countryno, int stateno, string email, int examiner_type)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_ID", id);
                        objParams[1] = new SqlParameter("@P_EXAMINER_NAME", examiner_name);
                        objParams[2] = new SqlParameter("@P_INSTITUTE_NAME", institute_name);
                        objParams[3] = new SqlParameter("@P_MOBILE_NO", mobileno);
                        objParams[4] = new SqlParameter("@P_COUNTRYNO", countryno);
                        objParams[5] = new SqlParameter("@P_EMAILID", email);
                        objParams[6] = new SqlParameter("@P_EXAMINER_TYPE_NO", examiner_type);
                        objParams[7] = new SqlParameter("@P_STATENO", stateno);
                        objParams[8] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_PHD_ADD_EXAMINER_SP_INS_UPD", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else if (Convert.ToInt32(ret) == 2)
                        {

                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PhdController.InsertExaminer-> " + ex.ToString());
                    }

                    return retStatus;
                }
                // new added by jay takalkhede on dated 26042023
                public int InsertExaminerMapping(int IDNO, int examinerID, int UANO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_EXAMINER_ID", examinerID);
                        objParams[2] = new SqlParameter("@P_UA_NO", UANO);
                        objParams[3] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_PHD_MAPPING_STUD_EXAMINER", objParams, true);

                        if (Convert.ToInt32(ret) == 1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PhdController.InsertExaminerMapping-> " + ex.ToString());
                    }

                    return retStatus;
                }
                //MODIFED BY NEHAL 28/03/2023
                public DataSet GetPROGRESSReport(int idno, int semesterno, int uano)
                {
                    DataSet dtr = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[2] = new SqlParameter("@P_UANO", uano);

                        // dtr = objSQLHelper.ExecuteDataSetSP("PKG_PHD_GET_PROGRESS_REPORT_DATA", objParams).Tables[0].CreateDataReader();
                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_PHD_GET_PROGRESS_REPORT_DATA", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetStudentDetails->" + ex.ToString());
                    }
                    return dtr;
                }
                //Added by Nehal on 17/04/23
                public int AddUpdPhdPublicationDetails(Phd objPubDel, DataTable SB_AUTHORLIST_RECORD)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[40];
                        objParams[0] = new SqlParameter("@P_IDNO", objPubDel.IdNo);
                        objParams[1] = new SqlParameter("@P_PUBLICATION_TYPE", objPubDel.PUBLICATION_TYPE);
                        objParams[2] = new SqlParameter("@P_TITLE", objPubDel.TITLE);
                        objParams[3] = new SqlParameter("@P_SUBJECT", objPubDel.SUBJECT);
                        if (!objPubDel.PUBLICATIONDATE.Equals(DateTime.MinValue))
                            objParams[4] = new SqlParameter("@P_PUBLICATIONDATE", objPubDel.PUBLICATIONDATE);
                        else
                            objParams[4] = new SqlParameter("@P_PUBLICATIONDATE", DBNull.Value);
                        objParams[5] = new SqlParameter("@P_DETAILS", objPubDel.DETAILS);
                        objParams[6] = new SqlParameter("@P_SPONSORED_AMOUNT", objPubDel.SPONSORED_AMOUNT);
                        objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objPubDel.COLLEGE_CODE);
                        objParams[8] = new SqlParameter("@P_NAME", objPubDel.CONFERENCE_NAME);
                        objParams[9] = new SqlParameter("@P_ORGANISOR", objPubDel.ORGANISOR);
                        objParams[10] = new SqlParameter("@P_PAGENO", objPubDel.PAGENO);
                        objParams[11] = new SqlParameter("@P_PUBLICATION", objPubDel.PUBLICATION);
                        objParams[12] = new SqlParameter("@P_ATTACHMENT", objPubDel.ATTACHMENTS);
                        objParams[13] = new SqlParameter("@P_ISBN", objPubDel.ISBN);
                        objParams[14] = new SqlParameter("@P_VolumeNo", objPubDel.VOLUME_NO);
                        objParams[15] = new SqlParameter("@P_IssueNo", objPubDel.ISSUE_NO);
                        objParams[16] = new SqlParameter("@P_Publication_Status", objPubDel.PUB_STATUS);
                        objParams[17] = new SqlParameter("@P_Year", objPubDel.YEAR);
                        objParams[18] = new SqlParameter("@p_Location", objPubDel.LOCATION);
                        objParams[19] = new SqlParameter("@P_Publisher", objPubDel.PUBLISHER);
                        objParams[20] = new SqlParameter("@P_IsConference", objPubDel.IS_CONFERENCE);
                        objParams[21] = new SqlParameter("@SB_AUTHORLIST_RECORD", SB_AUTHORLIST_RECORD);
                        objParams[22] = new SqlParameter("@P_PUBTRXNO", objPubDel.PUBTRXNO);
                        objParams[23] = new SqlParameter("@P_IsJournalScopus", objPubDel.IsJournalScopus);
                        objParams[24] = new SqlParameter("@P_IMPACTFACTORS", objPubDel.IMPACTFACTORS);
                        objParams[25] = new SqlParameter("@P_CITATIONINDEX", objPubDel.CITATIONINDEX);
                        objParams[26] = new SqlParameter("@P_EISSN", objPubDel.EISSN);
                        objParams[27] = new SqlParameter("@P_PUB_ADD", objPubDel.PUB_ADD);
                        objParams[28] = new SqlParameter("@P_Month", objPubDel.MONTH);
                        objParams[29] = new SqlParameter("@P_DOINO", objPubDel.DOIN);
                        objParams[30] = new SqlParameter("@P_INDEXING_TYPE", objPubDel.INDEXING_TYPE);
                        objParams[31] = new SqlParameter("@WEB_LINK", objPubDel.WEBLINK);
                        objParams[32] = new SqlParameter("@P_IndexingFactors", objPubDel.IndexingFactors);
                        objParams[33] = new SqlParameter("@P_IndexingFactorValue", objPubDel.IndexingFactorValue);
                        objParams[34] = new SqlParameter("@P_IndexingDATE", objPubDel.IndexingDATE);
                        objParams[35] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[36] = new SqlParameter("@P_DEGREENO", objPubDel.DegreeNo);
                        objParams[37] = new SqlParameter("@P_BRANCHNO", objPubDel.BranchNo);
                        objParams[38] = new SqlParameter("@P_ADMBATCH", objPubDel.AdmBatch);

                        objParams[39] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[39].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_ACD_PHD_PUBLICATION_DETAILS", objParams, true);
                        if (Convert.ToInt32(ret) == -1)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordFound);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.PhdController.AddUpdPhdPublicationDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }
                /// <summary>
                /// Added By jay Takalkhede on dated 18/05/2023 for PhD thesis Faculty 
                /// </summary>
                /// <param name="uano"></param>
                /// <param name="admbatchno"></param>
                /// <returns></returns>
                public DataSet GetAllStudentApprovalThesisList_UANO(int uano, int admbatchno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_UNO", uano);
                        objParams[1] = new SqlParameter("@P_ADMBATCH", admbatchno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUD_THESIS_APPROVAL_ENTRY_DETAILS_FACUILTY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PhdController.GetAllStudentThesisList-> " + ex.ToString());
                    }
                    return ds;
                }
                /// <summary>
                /// Added by jay takalkhede on dated 18/05/2023 for PhD thesis Faculty Excel
                /// </summary>
                /// <param name="idno"></param>
                /// <returns></returns>
                public DataSet GetStudentDataExcel_UANO(int idno, int uano, int admbatchno)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_UNO", uano);
                        objParams[2] = new SqlParameter("@P_ADMBATCH", admbatchno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUD_THESIS_APPROVAL_ENTRY_DETAILS_FACUILTY_EXCEL", objParams);


                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PhdController.GetStudentDataExcel --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
                public DataSet GetAllPublicationDetails(int idNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PHD_GETALL_PUBLICATION_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PhdController.GetAllPublicationDetails-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                //ADDED BY jay t. 02-06-2022
                public int PhdApplicationMarkjEntryCriteria(int organizationid, int admbatch, string testmark, string inerviewmark, string IPADDRESS, int UA_NO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            new SqlParameter("@P_ORGANIZATION_ID", organizationid),
                            new SqlParameter("@P_ADMBATCH", admbatch),
                            new SqlParameter("@P_TEST_MARK", testmark),
                            new SqlParameter("@P_INTERVIEW_MARK", inerviewmark),
                            new SqlParameter("@P_IP_ADDRESS", IPADDRESS),
                            new SqlParameter("@P_UA_NO", UA_NO),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_PHD_STUDENTS_FOR_MARK_ENTRY_CRITERIA", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.UpdateMarkEntry --> " + ex.ToString());
                    }
                    return retStatus;
                }

                //ADDED BY jay takalkhede 27-07-2022
                public int InsPhdOfferLetteremailLog_crescent(int org, string userno, int AdmBatch, int Deptno, string ip, int uano, int COLLEGE_ID, int status, DateTime INFORMED_DATE, string INFORMED_TIME)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New eXAM Registered Subject Details
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_USERNO", userno);
                        objParams[1] = new SqlParameter("@P_IP_ADDRESS", ip);
                        objParams[2] = new SqlParameter("@P_UA_NO", uano);
                        objParams[3] = new SqlParameter("@P_ADMBATCH", AdmBatch);
                        objParams[4] = new SqlParameter("@P_ORGANIZATION_ID", org);
                        objParams[5] = new SqlParameter("@P_COLLEGE_ID", COLLEGE_ID);
                        objParams[6] = new SqlParameter("@P_STATUS", status);
                        objParams[7] = new SqlParameter("@P_DEPARTMENT_NO", Deptno);
                        objParams[8] = new SqlParameter("@P_INFORMED_DATE", INFORMED_DATE);
                        objParams[9] = new SqlParameter("@P_INFORMED_TIME", INFORMED_TIME);
                        objParams[10] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_PHD_STUDENTS_OFFER_LETTER_LOG_CRESCENT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(ret);
                        else
                            retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.InsPhdSchedule-> " + ex.ToString());
                    }
                    return retStatus;
                }


                //ADDED BY jay takalkhede 27-07-2022
                public int InsPhdOfferLetteremailLog(int org, string userno, int AdmBatch, int Deptno, string ip, int uano, int COLLEGE_ID, int status)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New eXAM Registered Subject Details
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_USERNO", userno);
                        objParams[1] = new SqlParameter("@P_IP_ADDRESS", ip);
                        objParams[2] = new SqlParameter("@P_UA_NO", uano);
                        objParams[3] = new SqlParameter("@P_ADMBATCH", AdmBatch);
                        objParams[4] = new SqlParameter("@P_ORGANIZATION_ID", org);
                        objParams[5] = new SqlParameter("@P_COLLEGE_ID", COLLEGE_ID);
                        objParams[6] = new SqlParameter("@P_STATUS", status);
                        objParams[7] = new SqlParameter("@P_DEPARTMENT_NO", Deptno);
                        objParams[8] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_PHD_STUDENTS_OFFER_LETTER_LOG", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(ret);
                        else
                            retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.InsPhdSchedule-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //ADDED BY NEHAL ON 08082023
                public int AddStatusMaster(string StatusName, int Active_Status, int Admbatch, int sequence)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_STATUSNAME", StatusName),
                            new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),
                            new SqlParameter("@P_ACTIVESTATUS",Active_Status),
                            new SqlParameter("@P_ADMBATCH",Admbatch),
                            new SqlParameter("@P_SEQUENCE",sequence),
                            new SqlParameter("@P_OUTPUT", status)
                        };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_PHD_THESIS_MASTER_INSERT", sqlParams, true);
                        status = (Int32)obj;
                    }
                    catch (Exception ex)
                    {
                        status = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PhdController.AddStatusMaster() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                //ADDED BY NEHAL ON 08082023
                public int UpdateStatusMaster(int StatusNo, string StatusName, int Active_Status, int Admbatch, int sequence)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                            {         
                                 new SqlParameter("@P_STATUSNO", StatusNo),
                                 new SqlParameter("@P_STATUSNAME", StatusName),  
                                 new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),
                                 new SqlParameter("@P_ACTIVESTATUS",Active_Status),
                                 new SqlParameter("@P_ADMBATCH",Admbatch),
                                 new SqlParameter("@P_SEQUENCE",sequence),
                                 new SqlParameter("@P_OUTPUT",status)
                            };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_PHD_THESIS_MASTER_UPDATE", sqlParams, true);
                        status = (Int32)obj;
                    }
                    catch (Exception ex)
                    {
                        status = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PhdController.UpdateStatusMaster() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                //ADDED BY NEHAL ON 08082023
                public DataSet GetAllStatusNo(int StatusNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_OUTPUT", StatusNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_PHD_THESIS_STATUS_MASTER_GET_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PhdController.GetAllStatusNo-> " + ex.ToString());
                    }
                    return ds;
                }
                //ADDED BY Nehal 30-08-2023
                public int InsPhdScheduleemailLogmodified(StudentRegist objSR, int userno, string username, int AdmBatch, int COLLEGE_ID, int scheduleno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New eXAM Registered Subject Details
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_ADMBATCH  ", AdmBatch);
                        objParams[1] = new SqlParameter("@P_IPADDRESS", objSR.IPADDRESS);
                        objParams[2] = new SqlParameter("@P_REGISTERED_BY", objSR.UA_NO);
                        objParams[3] = new SqlParameter("@P_USERNO ", userno);
                        objParams[4] = new SqlParameter("@P_USERNAME", username);
                        objParams[5] = new SqlParameter("@P_COLLEGE_ID", COLLEGE_ID);
                        objParams[6] = new SqlParameter("@P_SCHEDULENO", scheduleno);
                        objParams[7] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_PHD_SELECTION_SCHEDULE_EMAIL_LOG_MODIFIED", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(ret);
                        else
                            retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PhdController.InsPhdScheduleemailLogmodified-> " + ex.ToString());
                    }
                    return retStatus;
                }

            }

        }//END: BusinessLayer.BusinessLogic

    }//END: UAIMS  

}//END: IITMS