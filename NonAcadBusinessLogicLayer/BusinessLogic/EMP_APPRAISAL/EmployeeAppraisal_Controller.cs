
﻿using IITMS;
using IITMS.SQLServer.SQLDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

﻿// CREATION DATE : 10-06-2021                                                        
// CREATED BY    : TANU BALGOTE                                       
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================  

using System.Web;
using IITMS.UAIMS;
using IITMS.UAIMS.NonAcadBusinessLogicLayer.BusinessLogic;
//namespace IITMS;
//namespace NonAcadBusinessLogicLayer.BusinessLogic.EMP_APPRAISAL;
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

        namespace NonAcadBusinessLogicLayer.BusinessLogic.EMP_APPRAISAL
        {
            public class EmployeeAppraisal_Controller
            {
                string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


                #region Published Journal Details

                public DataSet GetPublishedJournalDetails(int EMPLOYEE_ID, int APPRAISAL_EMPLOYEE_ID, int SESSION_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_EMPLOYEE_ID", EMPLOYEE_ID);
                        //objParams[1] = new SqlParameter("@P_APPRAISAL_EMPLOYEE_ID", APPRAISAL_EMPLOYEE_ID);
                        objParams[1] = new SqlParameter("@P_SESSION_ID", SESSION_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_IQAC_GET_PHD_RESARCH_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.IQACController.GetPhdResarchDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

            }
        }


        namespace BusinessLayer.BusinessLogic
        {
            public class EmployeeAppraisal_Controller
            {
                string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


                #region get  Employee  detail 
                public DataSet GetEmployeePersonalInfo(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMIN_APPRAISAL_GET_EMP_INFO_BY_IDNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmployeeAppraisalFormController.GetEmployeePersonalInfo->" + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetEmployeeInfo(string appraisalEmp_Id, int SessionNo, int EmpId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_APPRAISAL_EMPLOYEE_ID", appraisalEmp_Id);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[2] = new SqlParameter("@P_Employee_Id", EmpId);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMIN_APPRAISAL_GET_EMP_INFO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpApplyController.GetEmployeeInfo-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion 

                #region submit  Emp Appraisal

                public int AddUpdatePersonalDetails(EmpApprEnt objEA)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_APPRAISAL_EMPLOYEE_ID", objEA.APPRAISAL_EMPLOYEE_ID);
                        objParams[1] = new SqlParameter("@P_EMPLOYEE_ID", objEA.EMPLOYEE_ID);
                        objParams[2] = new SqlParameter("@P_EMPLOYEE_NAME", objEA.EMPLOYEE_NAME);
                        objParams[3] = new SqlParameter("@P_DEPARTMENT_ID", objEA.DEPARTMENT_ID);
                        objParams[4] = new SqlParameter("@P_SUBDESIGNO", objEA.DESIGNATION_ID);
                        objParams[5] = new SqlParameter("@P_NAME_OF_INSTITUTION", objEA.NAME_OF_INSTITUTION);
                        objParams[6] = new SqlParameter("@P_SESSIONNO", objEA.SESSIONNO);
                        objParams[7] = new SqlParameter("@P_COLLEGE_NO", objEA.COLLEGE_NO);
                        objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objEA.COLLEGE_CODE);
                        objParams[9] = new SqlParameter("@P_PAPNO", objEA.PAPNO);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ADMIN_APPRAISAL_PERSONAL_DETAILS_IU", objParams, true);

                        if (obj != null && obj.ToString().Equals("-99"))
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpApplyController.AddUpdatePersonalDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion

                #region get employee establishment data

                public DataSet GetJournalPublication(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_APPRAISAL_GET_PUBLISHED_JOURNAL_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmployeeAppraisalFormController.GetJournalPublication->" + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetBooksPublication(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_APPRAISAL_GET_PUBLISHED_BOOKS_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmployeeAppraisalFormController.GetBooksPublication->" + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetChaptersPublicationInBook(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_APPRAISAL_GET_CHAPTERS_PUBLISHED_IN_BOOKS_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmployeeAppraisalFormController.GetEmployeePersonalInfo->" + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetConferenceProceedindsDetails(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_APPRAISAL_GET_PAPERS_IN_CONFERENCE_PROCEEDINGS_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmployeeAppraisalFormController.GetEmployeePersonalInfo->" + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAvishkarDetails(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_APPRAISAL_GET_AVISHKAR_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmployeeAppraisalFormController.GetEmployeePersonalInfo->" + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetOngoingCompletedConsultancies(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_APPRAISAL_GET_ONGOING_COMPLETED_PROJECTS_CONSULTANCIES", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmployeeAppraisalFormController.GetEmployeePersonalInfo->" + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetPatentIPRDetails(int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_APPRAISAL_GET_PATENT_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmployeeAppraisalFormController.GetPatentIPRDetails->" + ex.ToString());
                    }
                    return ds;
                }

                //public DataSet GetInnovativeDetails(int idno)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("@P_IDNO", idno);
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_APPRAISAL_GET_TEACHING_INNOVATIVE_DETAILS", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.EmployeeAppraisalFormController.GetInnovativeDetails->" + ex.ToString());
                //    }
                //    return ds;
                //}


                #endregion

                #region Insert Update Method

                public int AddUpdatePublicationDetails(EmpApprEnt objEA)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[15];

                        objParams[0] = new SqlParameter("@P_APPRAISAL_EMPLOYEE_ID", objEA.APPRAISAL_EMPLOYEE_ID);
                        objParams[1] = new SqlParameter("@P_EMPLOYEE_ID", objEA.EMPLOYEE_ID);
                        objParams[2] = new SqlParameter("@P_SESSION_ID", objEA.SESSION_ID);
                        objParams[3] = new SqlParameter("@P_PUBLISHEDJOURNAL_TBL", objEA.PUBLISHEDJOURNAL);
                        objParams[4] = new SqlParameter("@P_PUBLISHEDBOOKS_TBL", objEA.PUBLISHEDBOOKS);
                        objParams[5] = new SqlParameter("@P_CHAPTERINBOOK_TBL", objEA.CHAPTERINBOOK);
                        objParams[6] = new SqlParameter("@P_COLLEGE_NO", objEA.COLLEGE_NO);
                        objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objEA.COLLEGE_CODE);

                        objParams[8] = new SqlParameter("@P_PUBLISH_JOURNAL_API_CLAIME", objEA.PUBLISH_JOURNAL_API_CLAIME);
                        objParams[9] = new SqlParameter("@P_PUBLISH_JOURNAL_API_VERIFY", objEA.PUBLISH_JOURNAL_API_VERIFY);
                        objParams[10] = new SqlParameter("@P_BOOK_API_CLAIME", objEA.BOOK_API_CLAIME);
                        objParams[11] = new SqlParameter("@P_BOOK_API_VERIFY", objEA.BOOK_API_VERIFY);

                        objParams[12] = new SqlParameter("@P_CHAPTER_API_CLAIME", objEA.CHAPTER_API_CLAIME);
                        objParams[13] = new SqlParameter("@P_CHAPTER_API_VERIFY", objEA.CHAPTER_API_VERIFY);


                        
                        objParams[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[14].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_APPRAISAL_PUBLICATIONS_DETAILS", objParams, true);

                        if (obj != null && obj.ToString().Equals("-99"))
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (obj.ToString().Equals("1"))
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {

                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpApplyController.AddUpdatePublicationDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddUpdateConferenceProceedDetails(EmpApprEnt objEA)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[15];

                        objParams[0] = new SqlParameter("@P_APPRAISAL_EMPLOYEE_ID", objEA.APPRAISAL_EMPLOYEE_ID);
                        objParams[1] = new SqlParameter("@P_EMPLOYEE_ID", objEA.EMPLOYEE_ID);
                        objParams[2] = new SqlParameter("@P_SESSION_ID", objEA.SESSION_ID);
                        objParams[3] = new SqlParameter("@P_PAPER_IN_CONFERENCE_TBL", objEA.PAPER_IN_CONFERENCE);
                        objParams[4] = new SqlParameter("@P_AVISHKAR_DETAILS_TBL", objEA.AVISHKAR_DETAILS);
                        objParams[5] = new SqlParameter("@P_PROJECT_CONSULTANCIES_TBL", objEA.PROJECT_CONSULTANCIES);
                        objParams[6] = new SqlParameter("@P_COLLEGE_NO", objEA.COLLEGE_NO);
                        objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objEA.COLLEGE_CODE);

                        objParams[8] = new SqlParameter("@P_CONFERENCE_API_CLAIME", objEA.CONFERENCE_API_CLAIME);
                        objParams[9] = new SqlParameter("@P_CONFEREMCE_API_VERIFY", objEA.CONFEREMCE_API_VERIFY);
                        objParams[10] = new SqlParameter("@P_AVISHKAR_API_CLAIME", objEA.AVISHKAR_API_CLAIME);
                        objParams[11] = new SqlParameter("@P_AVISHKAR_API_VERIFY", objEA.AVISHKAR_API_VERIFY);
                        objParams[12] = new SqlParameter("@P_PROJECT_API_CLAIME", objEA.PROJECT_API_CLAIME);
                        objParams[13] = new SqlParameter("@P_PROJECT_API_VERIFY", objEA.PROJECT_API_VERIFY);



                        objParams[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[14].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_APPRAISAL_CONFERENCE_PROCEEDINGS_DETAILS", objParams, true);

                        if (obj != null && obj.ToString().Equals("-99"))
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (obj.ToString().Equals("1"))
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {

                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpApplyController.AddUpdateConferenceProceedDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddUpdatePatentDetails(EmpApprEnt objEA)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[11];

                        objParams[0] = new SqlParameter("@P_APPRAISAL_EMPLOYEE_ID", objEA.APPRAISAL_EMPLOYEE_ID);
                        objParams[1] = new SqlParameter("@P_EMPLOYEE_ID", objEA.EMPLOYEE_ID);
                        objParams[2] = new SqlParameter("@P_SESSION_ID", objEA.SESSION_ID);
                        objParams[3] = new SqlParameter("@P_PATENTDETAILS_TBL", objEA.PATENTDETAILS);                        
                        objParams[4] = new SqlParameter("@P_COLLEGE_NO", objEA.COLLEGE_NO);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objEA.COLLEGE_CODE);

                        objParams[6] = new SqlParameter("@P_PATENT_API_CLAIME", objEA.PATENT_API_CLAIME);
                        objParams[7] = new SqlParameter("@P_PATENT_API_VERIFY", objEA.PATENT_API_VERIFY);

                        objParams[8] = new SqlParameter("@P_CATEGORY_III", objEA.PATENT_API_CLAIME);
                        objParams[9] = new SqlParameter("@P_CATEGORYIII", objEA.PATENT_API_VERIFY);

                       
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_APPRAISAL_PATENT_IPR_DETAILS", objParams, true);

                        if (obj != null && obj.ToString().Equals("-99"))
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (obj.ToString().Equals("1"))
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {

                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpApplyController.AddUpdatePatentDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddUpdateResearchDetails(EmpApprEnt objEA)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[10];

                        objParams[0] = new SqlParameter("@P_APPRAISAL_EMPLOYEE_ID", objEA.APPRAISAL_EMPLOYEE_ID);
                        objParams[1] = new SqlParameter("@P_EMPLOYEE_ID", objEA.EMPLOYEE_ID);
                        objParams[2] = new SqlParameter("@P_SESSION_ID", objEA.SESSION_ID);
                        objParams[3] = new SqlParameter("@P_RESEARCHGUIDANCE_TBL", objEA.RESEARCHGUIDANCE);
                        objParams[4] = new SqlParameter("@P_RESEARCHQUALIFICATION_TBL", objEA.RESEARCHQUALIFICATION);                        
                        objParams[5] = new SqlParameter("@P_COLLEGE_NO", objEA.COLLEGE_NO);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objEA.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_GUIDANCE_QUALIFICATION_CLAIME_TOT", objEA.GUIDANCE_QUALIFICATION_VERIFY_TOT);
                        objParams[8] = new SqlParameter("@P_GUIDANCE_QUALIFICATION_VERIFY_TOT", objEA.GUIDANCE_QUALIFICATION_CLAIME_TOT);


                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_APPRAISAL_GET_RESEARCH_GUIDANCE_DETAILS", objParams, true);

                        if (obj != null && obj.ToString().Equals("-99"))
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (obj.ToString().Equals("1"))
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {

                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpApplyController.AddUpdatePublicationDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddUpdatePerformanceDetails(EmpApprEnt objEA)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[18];

                        objParams[0] = new SqlParameter("@P_APPRAISAL_EMPLOYEE_ID", objEA.APPRAISAL_EMPLOYEE_ID);
                        objParams[1] = new SqlParameter("@P_EMPLOYEE_ID", objEA.EMPLOYEE_ID);
                        objParams[2] = new SqlParameter("@P_SESSION_ID", objEA.SESSION_ID);
                        objParams[3] = new SqlParameter("@P_ATTENDANCEPERFORMANCE_TBL", objEA.ATTENDANCEPERFORMANCE);
                        objParams[4] = new SqlParameter("@P_PERFORMANCERESULT_TBL", objEA.PERFORMANCERESULT);                        
                        objParams[5] = new SqlParameter("@P_COLLEGE_NO", objEA.COLLEGE_NO);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objEA.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_PA_ATTENDANCE", objEA.PAavg_attendance);
                        objParams[8] = new SqlParameter("@P_PA_FACTOR", objEA.PAper_multiplying_factor);
                        objParams[9] = new SqlParameter("@P_PA_MAX_WEIGHT", objEA.PAmax_Weight);
                        objParams[10] = new SqlParameter("@P_API_SCORE", objEA.PAapi_score_claim);
                        

                        objParams[11] = new SqlParameter("@P_PR_ATTENDANCE", objEA.PRavg_attendance);
                        objParams[12] = new SqlParameter("@P_PR_FACTOR", objEA.PRper_multiplying_factor);
                        objParams[13] = new SqlParameter("@P_PR_MAX_WEIGHT", objEA.PRmax_Weight);
                        objParams[14] = new SqlParameter("@P_PR_API_SCORE", objEA.PRapi_score_claim);
                        objParams[15] = new SqlParameter("@P_API_SCORE_V", objEA.PAapi_score_Verify);
                        objParams[16] = new SqlParameter("@P_PR_API_V", objEA.PRapi_score_verify);
                       
                        objParams[17] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[17].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_APPRAISAL_STUDENTS_PERFORMANCE_DETAILS", objParams, true);

                        if (obj != null && obj.ToString().Equals("-99"))
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (obj.ToString().Equals("1"))
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {

                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpApplyController.AddUpdatePerformanceDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddUpdateInnovationDetails(EmpApprEnt objEA)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[17];

                        objParams[0] = new SqlParameter("@P_APPRAISAL_EMPLOYEE_ID", objEA.APPRAISAL_EMPLOYEE_ID);
                        objParams[1] = new SqlParameter("@P_EMPLOYEE_ID", objEA.EMPLOYEE_ID);
                        objParams[2] = new SqlParameter("@P_SESSION_ID", objEA.SESSION_ID);
                        //objParams[3] = new SqlParameter("@P_Study_Material", objEA.Study_Material);
                        //objParams[4] = new SqlParameter("@P_API_Score_Claimed", objEA.API_Score_Claimed);
                        //objParams[5] = new SqlParameter("@P_Verified_API_Score", objEA.Verified_API_Score);
                        ////objParams[3] = new SqlParameter("@P_ATTENDANCEPERFORMANCE_TBL", objEA.ATTENDANCEPERFORMANCE);
                        //objParams[6] = new SqlParameter("@P_COLLEGE_NO", objEA.COLLEGE_NO);
                        //objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objEA.COLLEGE_CODE);
                        //objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        //objParams[8].Direction = ParameterDirection.Output;

                        objParams[3] = new SqlParameter("@P_INNOVATIVETEACHING_TBL", objEA.INNOVATIVETEACHING);
                        objParams[4] = new SqlParameter("@P_STUD_FEEDBACK_TBL", objEA.STUDENT_FEEDBACK);
                        objParams[5] = new SqlParameter("@P_EXAM_WORK_TBL", objEA.EXAMINATION_WORK);

                        objParams[6] = new SqlParameter("@P_COLLEGE_NO", objEA.COLLEGE_NO);
                        objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objEA.COLLEGE_CODE);

                        objParams[8] = new SqlParameter("@P_INNOVATIVE_API_SCORE_CLAIME", objEA.INNOVATIVE_API_SCORE_CLAIME);
                        objParams[9] = new SqlParameter("@P_INNOVATIVE_API_SCORE_VERIFY", objEA.INNOVATIVE_API_SCORE_VERIFY);
                        objParams[10] = new SqlParameter("@P_STUDENT_FEEDBACK_API_SCORE_CLAIME", objEA.STUDENT_FEEDBACK_API_SCORE_CLAIME);
                        objParams[11] = new SqlParameter("@P_STUDENT_FEEDBACK_API_SCORE_VERIFY", objEA.STUDENT_FEEDBACK_API_SCORE_VERIFY);
                        objParams[12] = new SqlParameter("@P_EXAMINATION_API_SCORE_CLAIME", objEA.EXAMINATION_API_SCORE_CLAIME);
                        objParams[13] = new SqlParameter("@P_EXAMINATION_API_SCORE_VERIFY", objEA.EXAMINATION_API_SCORE_VERIFY);

                        objParams[14] = new SqlParameter("@P_CATEGORY_I", objEA.CATEGORY_I_TOTAL);
                        objParams[15] = new SqlParameter("@P_CATEGORY_II", objEA.CATEGORY_II_TOTAL);



                        objParams[16] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[16].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_APPRAISAL_LECTURES_ACADEMIC_DUTIES", objParams, true);

                        if (obj != null && obj.ToString().Equals("-99"))
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (obj.ToString().Equals("1"))
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {

                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpApplyController.AddUpdateInnovationDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddUpdateTeachingActivityDetails(EmpApprEnt objEA)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];

                        objParams[0] = new SqlParameter("@P_APPRAISAL_EMPLOYEE_ID", objEA.APPRAISAL_EMPLOYEE_ID);
                        objParams[1] = new SqlParameter("@P_EMPLOYEE_ID", objEA.EMPLOYEE_ID);
                        objParams[2] = new SqlParameter("@P_SESSION_ID", objEA.SESSION_ID);
                        objParams[3] = new SqlParameter("@P_TEACHINGACTIVITY_TBL", objEA.TEACHINGACTIVITY);
                        objParams[4] = new SqlParameter("@P_COLLEGE_NO", objEA.COLLEGE_NO);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objEA.COLLEGE_CODE);
                        objParams[6] = new SqlParameter("@P_SEM1_TOTAL", objEA.SEM1_TOTAL);
                        objParams[7] = new SqlParameter("@P_SEM2_TOTAL", objEA.SEM2_TOTAL);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_APPRAISAL_TEACHING_LEARNING_ACTIVITIES_DETAILS", objParams, true);

                        if (obj != null && obj.ToString().Equals("-99"))
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (obj.ToString().Equals("1"))
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {

                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpApplyController.AddUpdateTeachingActivityDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddUpdatePerformanceInEngagingLectures(EmpApprEnt objEA)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[12];

                        objParams[0] = new SqlParameter("@P_APPRAISAL_EMPLOYEE_ID", objEA.APPRAISAL_EMPLOYEE_ID);
                        objParams[1] = new SqlParameter("@P_EMPLOYEE_ID", objEA.EMPLOYEE_ID);
                        objParams[2] = new SqlParameter("@P_SESSION_ID", objEA.SESSION_ID);
                        objParams[3] = new SqlParameter("@P_ENGAGINGLECTURE_TBL", objEA.ENGAGINGLECTURE);
                        objParams[4] = new SqlParameter("@P_COLLEGE_NO", objEA.COLLEGE_NO);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objEA.COLLEGE_CODE);
                        objParams[6] = new SqlParameter("@P_AVERAGE", objEA.AverageL);
                        objParams[7] = new SqlParameter("@P_PERFORMANCE", objEA.MulFact);
                        objParams[8] = new SqlParameter("@P_MAXW", objEA.MaxWL);
                        objParams[9] = new SqlParameter("@P_APIC", objEA.apiCL);
                        objParams[10] = new SqlParameter("@P_APIV", objEA.apiVl);

                      



                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_APPRAISAL_PERFORMANCE_IN_ENGAGING_LECTURES", objParams, true);

                        if (obj != null && obj.ToString().Equals("-99"))
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (obj.ToString().Equals("1"))
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {

                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpApplyController.AddUpdateTeachingActivityDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddUpdateFieldBasedActivities(EmpApprEnt objEA)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[13];

                        objParams[0] = new SqlParameter("@P_APPRAISAL_EMPLOYEE_ID", objEA.APPRAISAL_EMPLOYEE_ID);
                        objParams[1] = new SqlParameter("@P_EMPLOYEE_ID", objEA.EMPLOYEE_ID);
                        objParams[2] = new SqlParameter("@P_SESSION_ID", objEA.SESSION_ID);
                        objParams[3] = new SqlParameter("@P_CU_CURRICULAR_ACTIVITY_TBL", objEA.CU_CURRICULAR_ACTIVITY);
                        objParams[4] = new SqlParameter("@P_COLLEGE_NO", objEA.COLLEGE_NO);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objEA.COLLEGE_CODE);
                        objParams[6] = new SqlParameter("@P_STUD_SCORE_CLAME", objEA.STUDENT_SCORE_CLAIME);
                        objParams[7] = new SqlParameter("@P_STUD_SCORE_VERIFY", objEA.STUDENT_SCORE_VERIFY);

                        objParams[8] = new SqlParameter("@P_API_SCORE_ALLOTTED", objEA.Community_SCORE_ALLOTED);
                        objParams[9] = new SqlParameter("@P_NAME_OF_ACTIVITY", objEA.Community_activity);
                        objParams[10] = new SqlParameter("@P_API_SCORE_CLAIME", objEA.Community_SCORE_CLAIME);
                        objParams[11] = new SqlParameter("@P_API_SCORE_VERIFY", objEA.Community_SCORE_VERIFY);



                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_APPRAISAL_STUDENT_RELETED_CO_CURRICULAR_ACTIVITY", objParams, true);

                        if (obj != null && obj.ToString().Equals("-99"))
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (obj.ToString().Equals("1"))
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {

                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpApplyController.AddUpdateFieldBasedActivities-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddUpdateAdministrativeAcademic(EmpApprEnt objEA)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[17];

                        objParams[0] = new SqlParameter("@P_APPRAISAL_EMPLOYEE_ID", objEA.APPRAISAL_EMPLOYEE_ID);
                        objParams[1] = new SqlParameter("@P_EMPLOYEE_ID", objEA.EMPLOYEE_ID);
                        objParams[2] = new SqlParameter("@P_SESSION_ID", objEA.SESSION_ID);
                        objParams[3] = new SqlParameter("@P_ADMINACADEMIC_TBL", objEA.ADMINACADEMIC);
                        objParams[4] = new SqlParameter("@P_PROFESSIONALDEVELOP_TBL", objEA.PROFESSIONALDEVELOP);
                        objParams[5] = new SqlParameter("@P_COLLEGE_NO", objEA.COLLEGE_NO);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objEA.COLLEGE_CODE);

                        objParams[7] = new SqlParameter("@P_ADMIN_SCORE_CLAIME", objEA.ADMINISTRATIVE_SCORE_CLAIME);
                        objParams[8] = new SqlParameter("@P_ADMIN_SCORE_VERIFY", objEA.ADMINISTRATIVE_SCORE_VERIFY);

                        objParams[9] = new SqlParameter("@P_DEVELOPEMENT_SCORE_CLAIME", objEA.PROFESSIONAL_SCORE_CLAIME);
                        objParams[10] = new SqlParameter("@P_DEVELOPEMENT_SCORE_VERIFY", objEA.PROFESSIONAL_SCORE_VERIFY);

                        objParams[11] = new SqlParameter("@P_CATEGORY_CLAIME", objEA.CAT_I_TOTAL);
                        objParams[12] = new SqlParameter("@P_CATEGORY_VERIFY", objEA.CAT_II_TOTAL);

                        objParams[13] = new SqlParameter("@P_TOTAL1", objEA.TOTAL1);
                        objParams[14] = new SqlParameter("@P_TOTAL2", objEA.TOTAL2);

                        objParams[15] = new SqlParameter("@P_ADMINACADOTHERACTIVIT_TBL", objEA.OtherActivity);





                        objParams[16] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[16].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_APPRAISAL_ADMINISTRATIVE_ACADEMIC_PROFESSIONAL", objParams, true);

                        if (obj != null && obj.ToString().Equals("-99"))
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (obj.ToString().Equals("1"))
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {

                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpApplyController.AddUpdateTeachingActivityDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }




                public int AddUpdateLectureAndAcademicDuties(EmpApprEnt objEA)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[12];

                        objParams[0] = new SqlParameter("@P_APPRAISAL_EMPLOYEE_ID", objEA.APPRAISAL_EMPLOYEE_ID);
                        objParams[1] = new SqlParameter("@P_EMPLOYEE_ID", objEA.EMPLOYEE_ID);
                        objParams[2] = new SqlParameter("@P_SESSION_ID", objEA.SESSION_ID);

                        objParams[3] = new SqlParameter("@P_LECTURE_AND_DUTIES", objEA.LECTURE_AND_ADUTIES);
                        objParams[4] = new SqlParameter("@P_STUDY_MATERIAL_RESOURSE", objEA.STUDY_MATERIAL_RESOURSE);
                       
                        objParams[5] = new SqlParameter("@P_COLLEGE_NO", objEA.COLLEGE_NO);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objEA.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_APISCORE", objEA.API_Score_Claimed);

                        objParams[8] = new SqlParameter("@P_API_SCORE_STUDY", objEA.API_SCORE_CLAIME_STUDY );
                        objParams[9] = new SqlParameter("@P_API_SCORE_V_STUDY", objEA.API_SCORE_VERIFIED_STUDY);

                        objParams[10] = new SqlParameter("@P_APIVER", objEA.Verified_API_Score);
                            

                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_APPRAISAL_LECTURES_DUTIES", objParams, true);

                        if (obj != null && obj.ToString().Equals("-99"))
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (obj.ToString().Equals("1"))
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                        {

                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpApplyController.AddUpdateInnovationDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }





                #endregion

                #region UPDATE FINAL SUBNIT

                public int UpdateLock(int emp_id, int SessionNo)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_EMPLOYEE_ID", emp_id);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_APPRAISAL_UPDATE_LOCK", objParams, true);

                        if (obj != null && obj.ToString().Equals("-99"))
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmployeeAppraisal_Controller.UpdateLock-> " + ex.ToString());
                    }
                    return retStatus;

                }

                #endregion

                #region Status Report
                //public DataSet GetEmpStatus(int UA_NO, int SESSIONNO, int DEPT_NO, int APPRAISAL_TYPE)
                public DataSet GetEmpStatus(int UA_NO, int SESSIONNO, int DEPT_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", SESSIONNO);
                        objParams[2] = new SqlParameter("@P_DEPT_NO", DEPT_NO);
                       // objParams[3] = new SqlParameter("@P_APPRAISAL_TYPE", APPRAISAL_TYPE);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_APPRAISAL_GET_EMP_DETAILS_FOR_REVIEW", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ClassIIIAppraisalCon.GetEmpStatus-> " + ex.ToString());
                    }
                    return ds;

                }

                public int UpdateFinalLock(int empid, int SessionNo, int SrNo)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_EMPLOYEE_ID", empid);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[2] = new SqlParameter("@P_SRNO", SrNo);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_APPRAISAL_UPDATE_FINAL_LOCK", objParams, true);

                        if (obj != null && obj.ToString().Equals("-99"))
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmployeeAppraisal_Controller.UpdateFinalLock-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AddUpdReviewingRemark(EmpApprEnt objEA)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[10];

                        objParams[0] = new SqlParameter("@P_APPRAISAL_EMPLOYEE_ID", objEA.APPRAISAL_EMPLOYEE_ID);
                        objParams[1] = new SqlParameter("@P_EMPLOYEE_ID", objEA.EMPLOYEE_ID);
                        objParams[2] = new SqlParameter("@P_SESSION_ID", objEA.SESSIONNO);
                       // objParams[3] = new SqlParameter("@P_ATID", objEA.ATID);

                       // objParams[4] = new SqlParameter("@P_DO_YOU_AGREE_ASSESSMENT", objEA.DO_YOU_AGREE_ASSESSMENT);
                        objParams[3] = new SqlParameter("@P_GIVE_REASON", objEA.GIVE_REASON);
                        objParams[4] = new SqlParameter("@P_OTHER_COMMENT", objEA.OTHER_COMMENT);
                        objParams[5] = new SqlParameter("@P_LENGTH_OF_SERVICE", objEA.LENGTH_OF_SERVICE);
                        objParams[6] = new SqlParameter("@P_NUMERICALGRADING", objEA.NUMERICALGRADING);
                        objParams[7] = new SqlParameter("@P_CREATED_BY", objEA.CREATED_BY);
                        objParams[8] = new SqlParameter("@P_MODIFIED_BY", objEA.MODIFIED_BY);

                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_APPRAISAL_INS_UPD_REVIEW_REMARK_TEACHING", objParams, true);

                        if (obj != null && obj.ToString().Equals("-99"))
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (obj != null && obj.ToString().Equals("1"))
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (obj != null && obj.ToString().Equals("2"))
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AppraisalController.AddUpdateGeneralInfo-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion


            }
        }

    }
    
}
