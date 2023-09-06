//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS LOGIC FILE [EMP APPRAISAL]                                  
// CREATION DATE : 6-03-2021                                                        
// CREATED BY    : TANU BALGOTE                                       
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================  

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Data;
using System.Web;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessEntities
        {
            public class EmpApprEnt
            {
                #region Personal Information

                private int _APPRAISAL_EMPLOYEE_ID = 0;
                private int _EMPLOYEE_ID = 0;
                private string _EMPLOYEE_NAME = string.Empty;
                private int _DEPARTMENT_ID = 0;
                private int _SUBDESIGNO = 0;
                private int _NAME_OF_INSTITUTION = 0;
                
                private int _SESSIONNO = 0;
                private int _COLLEGE_NO = 0;
                private int _COLLEGE_CODE = 0;
               
                private int _PAPNO;
                private int _USER_TYPE_NO;
                private double _SEM1_TOTAL;
                private double _SEM2_TOTAL;
                private int _GUIDANCE_QUALIFICATION_CLAIME_TOT;
                private int _GUIDANCE_QUALIFICATION_VERIFY_TOT;

                private int _PUBLISH_JOURNAL_API_CLAIME;
                private int _PUBLISH_JOURNAL_API_VERIFY;
                private int _BOOK_API_CLAIME;
                private int _BOOK_API_VERIFY;
                private int _CHAPTER_API_CLAIME;
                private int _CHAPTER_API_VERIFY;

                private int _CONFERENCE_API_CLAIME;
                private int _CONFEREMCE_API_VERIFY;
                private int _AVISHKAR_API_CLAIME;
                private int _AVISHKAR_API_VERIFY;
                private int _PROJECT_API_CLAIME;
                private int _PROJECT_API_VERIFY;
                private int _PATENT_API_CLAIME;
                private int _PATENT_API_VERIFY;
                private string _LENGTH_OF_SERVICE;
                private string _GIVE_REASON;
                private string _OTHER_COMMENT;
                private int _NUMERICALGRADING;
                private int _CREATED_BY;
                private int _MODIFIED_BY;
                private   double _AverageL;
                private string _MulFact;
                private int _MaxWL;
                private int _apiCL;
                private int _apiVl;


                public int apiVl
                {
                    get { return _apiVl; }
                    set { _apiVl = value; }
                }
                public int apiCL
                {
                    get { return _apiCL; }
                    set { _apiCL = value; }
                }
                public int MaxWL
                {
                    get { return _MaxWL; }
                    set { _MaxWL = value; }
                }
                public string MulFact
                {
                    get { return _MulFact; }
                    set { _MulFact = value; }
                }
                public double AverageL
                {
                    get { return _AverageL; }
                    set { _AverageL = value; }
                }
                public int CREATED_BY
                {
                    get { return _CREATED_BY; }
                    set { _CREATED_BY = value; }
                }
                public int MODIFIED_BY
                {
                    get { return _MODIFIED_BY; }
                    set { _MODIFIED_BY = value; }
                }
                public int NUMERICALGRADING
                {
                    get { return _NUMERICALGRADING; }
                    set { _NUMERICALGRADING = value; }
                }
                public string OTHER_COMMENT
                {
                    get { return _OTHER_COMMENT; }
                    set { _OTHER_COMMENT = value; }
                }
                public string GIVE_REASON
                {
                    get { return _GIVE_REASON; }
                    set { _GIVE_REASON = value; }
                }
                public string LENGTH_OF_SERVICE
                {
                    get { return _LENGTH_OF_SERVICE; }
                    set { _LENGTH_OF_SERVICE = value; }
                }
                public int PATENT_API_CLAIME
                {
                    get { return _PATENT_API_CLAIME; }
                    set { _PATENT_API_CLAIME = value; }
                }
                public int PATENT_API_VERIFY
                {
                    get { return _PATENT_API_VERIFY; }
                    set { _PATENT_API_VERIFY = value; }
                }
                public int CONFERENCE_API_CLAIME
                {
                    get { return _CONFERENCE_API_CLAIME; }
                    set { _CONFERENCE_API_CLAIME = value; }
                }
                public int CONFEREMCE_API_VERIFY
                {
                    get { return _CONFEREMCE_API_VERIFY; }
                    set { _CONFEREMCE_API_VERIFY = value; }
                }
                public int AVISHKAR_API_CLAIME
                {
                    get { return _AVISHKAR_API_CLAIME; }
                    set { _AVISHKAR_API_CLAIME = value; }
                }
                public int AVISHKAR_API_VERIFY
                {
                    get { return _AVISHKAR_API_VERIFY; }
                    set { _AVISHKAR_API_VERIFY = value; }
                }
                public int PROJECT_API_CLAIME
                {
                    get { return _PROJECT_API_CLAIME; }
                    set { _PROJECT_API_CLAIME = value; }
                }
                public int PROJECT_API_VERIFY
                {
                    get { return _PROJECT_API_VERIFY; }
                    set { _PROJECT_API_VERIFY = value; }
                }

                public int CHAPTER_API_VERIFY
                {
                    get { return _CHAPTER_API_VERIFY; }
                    set { _CHAPTER_API_VERIFY = value; }
                }
                public int CHAPTER_API_CLAIME
                {
                    get { return _CHAPTER_API_CLAIME; }
                    set { _CHAPTER_API_CLAIME = value; }
                }

                public int PUBLISH_JOURNAL_API_CLAIME
                {
                    get { return _PUBLISH_JOURNAL_API_CLAIME; }
                    set { _PUBLISH_JOURNAL_API_CLAIME = value; }
                }
                public int PUBLISH_JOURNAL_API_VERIFY
                {
                    get { return _PUBLISH_JOURNAL_API_VERIFY; }
                    set { _PUBLISH_JOURNAL_API_VERIFY = value; }
                }
                public int BOOK_API_CLAIME
                {
                    get { return _BOOK_API_CLAIME; }
                    set { _BOOK_API_CLAIME = value; }
                }
                public int BOOK_API_VERIFY
                {
                    get { return _BOOK_API_VERIFY; }
                    set { _BOOK_API_VERIFY = value; }
                }
                public int GUIDANCE_QUALIFICATION_VERIFY_TOT
                {
                    get { return _GUIDANCE_QUALIFICATION_VERIFY_TOT; }
                    set { _GUIDANCE_QUALIFICATION_VERIFY_TOT = value; }
                }

                public int GUIDANCE_QUALIFICATION_CLAIME_TOT
                {
                    get { return _GUIDANCE_QUALIFICATION_CLAIME_TOT; }
                    set { _GUIDANCE_QUALIFICATION_CLAIME_TOT = value; }
                }
                public int APPRAISAL_EMPLOYEE_ID
                {
                    get { return _APPRAISAL_EMPLOYEE_ID; }
                    set { _APPRAISAL_EMPLOYEE_ID = value; }
                }
                public int EMPLOYEE_ID
                {
                    get { return _EMPLOYEE_ID; }
                    set { _EMPLOYEE_ID = value; }
                }
                public string EMPLOYEE_NAME
                {
                    get { return _EMPLOYEE_NAME; }
                    set { _EMPLOYEE_NAME = value; }
                }
                public int DEPARTMENT_ID
                {
                    get { return _DEPARTMENT_ID; }
                    set { _DEPARTMENT_ID = value; }
                }
                public int DESIGNATION_ID
                {
                    get { return _SUBDESIGNO; }
                    set { _SUBDESIGNO = value; }
                }
                public int NAME_OF_INSTITUTION
                {
                    get { return _NAME_OF_INSTITUTION; }
                    set { _NAME_OF_INSTITUTION = value; }
                }
              
               
               
                public int SESSIONNO
                {
                    get { return _SESSIONNO; }
                    set { _SESSIONNO = value; }
                }
                public int COLLEGE_NO
                {
                    get { return _COLLEGE_NO; }
                    set { _COLLEGE_NO = value; }
                }
                public int COLLEGE_CODE
                {
                    get { return _COLLEGE_CODE; }
                    set { _COLLEGE_CODE = value; }
                }
               
                public int PAPNO
                {
                    get { return _PAPNO; }
                    set { _PAPNO = value; }
                }

                public double SEM1_TOTAL
                {
                    get { return _SEM1_TOTAL; }
                    set { _SEM1_TOTAL = value; }
                }

                public double SEM2_TOTAL
                {
                    get { return _SEM2_TOTAL; }
                    set { _SEM2_TOTAL = value; }
                }
                #endregion

                #region Publication Details

                private int _SESSION_ID = 0;
                private DataTable _PUBLISHEDJOURNAL = null;
                private DataTable _PUBLISHEDBOOKS = null;
                private DataTable _CHAPTERINBOOK = null;
                private DataTable _PAPER_IN_CONFERENCE = null;
                private DataTable _AVISHKAR_DETAILS = null;
                private DataTable _PROJECT_CONSULTANCIES = null;
                private DataTable _PATENTDETAILS = null;
                private DataTable _RESEARCHGUIDANCE = null;
                private DataTable _RESEARCHQUALIFICATION = null;
                private DataTable _STUDY_MATERIAL_RESOURSE = null;

                // on 11-04-2022
                private DataTable _TEACHINGACTIVITY = null;
                private DataTable _ENGAGINGLECTURE = null;
                private DataTable _CU_CURRICULAR_ACTIVITY = null;
                private DataTable _ATTENDANCEPERFORMANCE = null;
                private DataTable _PERFORMANCERESULT = null;
                private DataTable _INNOVATIVETEACHING = null;
                private DataTable _STUDENT_FEEDBACK = null;
                private DataTable _EXAMINATION_WORK = null;
                private DataTable _CoCurricular = null;
                private DataTable _ADMINACADEMIC = null;
                private DataTable _PROFESSIONALDEVELOP = null;
                private DataTable _LECTURE_AND_ADUTIES = null;
                string _Study_Material = string.Empty;
                string _API_Score_Claimed = string.Empty;
                int _Verified_API_Score = 0;
                double _PAavg_attendance ;
                string _PAper_multiplying_factor = string.Empty;
                int _PAmax_Weight = 0;
                int _PAapi_score_claim = 0;
                int _PAapi_score_Verify = 0;
                int _CATEGORY_I_TOTAL = 0;
                int _CATEGORY_II_TOTAL = 0;
                int _CAT_I_TOTAL = 0;
                int _CAT_II_TOTAL = 0;
                int _CAT_III_TOTAL1 = 0;
                int _CAT_III_TOTAL2 = 0;
               



                double _PRavg_attendance ;
                string _PRper_multiplying_factor = string.Empty;
                int _PRmax_Weight = 0;
                int _PRapi_score_claim = 0;
                int _PRapi_score_verify = 0;
                int _API_SCORE_CLAIME_STUDY = 0;
                int _API_SCORE_VERIFIED_STUDY = 0;

                int _INNOVATIVE_API_SCORE_CLAIME = 0;
                int _INNOVATIVE_API_SCORE_VERIFY = 0;

                int _STUDENT_FEEDBACK_API_SCORE_CLAIME = 0;
                int _STUDENT_FEEDBACK_API_SCORE_VERIFY = 0;

                int _EXAMINATION_API_SCORE_CLAIME = 0;
                int _EXAMINATION_API_SCORE_VERIFY = 0;

                int _ADMINISTRATIVE_SCORE_CLAIME = 0;
                int _ADMINISTRATIVE_SCORE_VERIFY = 0;

                int _PROFESSIONAL_SCORE_CLAIME = 0;
                int _PROFESSIONAL_SCORE_VERIFY = 0;


                int _STUDENT_SCORE_CLAIME = 0;
                int _STUDENT_SCORE_VERIFY = 0;
                int _TOTAL1 = 0;
                int _TOTAL2 = 0;

                string _Community_activity = string.Empty;
                int _Community_SCORE_CLAIME = 0;
                int _Community_SCORE_VERIFY = 0;
                int _Community_SCORE_ALLOTED = 0;
                private DataTable _OtherActivity = null;


                public DataTable OtherActivity
                {
                    get { return _OtherActivity; }
                    set { _OtherActivity = value; }
                }

                public int CATEGORY_II_TOTAL
                {
                    get { return _CATEGORY_II_TOTAL; }
                    set { _CATEGORY_II_TOTAL = value; }
                }

                public int CATEGORY_I_TOTAL
                {
                    get { return _CATEGORY_I_TOTAL; }
                    set { _CATEGORY_I_TOTAL = value; }

                }
               
                public int CAT_II_TOTAL
                {
                    get { return _CAT_II_TOTAL; }
                    set { _CAT_II_TOTAL = value; }

                }
                public int CAT_III_TOTAL1
                {
                    get { return _CAT_III_TOTAL1; }
                    set { _CAT_III_TOTAL1 = value; }

                }

                public int CAT_III_TOTAL2
                {
                    get { return _CAT_III_TOTAL2; }
                    set { _CAT_III_TOTAL2 = value; }
                }

                public int TOTAL1
                {
                    get { return _TOTAL1; }
                    set { _TOTAL1 = value; }

                }
                public int TOTAL2
                {
                    get { return _TOTAL2; }
                    set { _TOTAL2 = value; }

                }
                public int CAT_I_TOTAL
                {
                    get { return _CAT_I_TOTAL; }
                    set { _CAT_I_TOTAL = value; }

                }
                public int ADMINISTRATIVE_SCORE_CLAIME
                {
                    get { return _ADMINISTRATIVE_SCORE_CLAIME; }
                    set { _ADMINISTRATIVE_SCORE_CLAIME = value; }
                }
                public int ADMINISTRATIVE_SCORE_VERIFY
                {
                    get { return _ADMINISTRATIVE_SCORE_VERIFY; }
                    set { _ADMINISTRATIVE_SCORE_VERIFY = value; }
                }
                public int PROFESSIONAL_SCORE_CLAIME
                {
                    get { return _PROFESSIONAL_SCORE_CLAIME; }
                    set { _PROFESSIONAL_SCORE_CLAIME = value; }
                }
                public int PROFESSIONAL_SCORE_VERIFY
                {
                    get { return _PROFESSIONAL_SCORE_VERIFY; }
                    set { _PROFESSIONAL_SCORE_VERIFY = value; }
                }

                public int STUDENT_SCORE_CLAIME
                {
                    get { return _STUDENT_SCORE_CLAIME; }
                    set { _STUDENT_SCORE_CLAIME = value; }
                }
                public int STUDENT_SCORE_VERIFY
                {
                    get { return _STUDENT_SCORE_VERIFY; }
                    set { _STUDENT_SCORE_VERIFY = value; }
                }

                public string Community_activity
                {
                    get { return _Community_activity; }
                    set { _Community_activity = value; }
                }

                public int Community_SCORE_CLAIME
                {
                    get { return _Community_SCORE_CLAIME; }
                    set { _Community_SCORE_CLAIME = value; }
                }
               
                public int Community_SCORE_VERIFY
                {
                    get { return _Community_SCORE_VERIFY; }
                    set { _Community_SCORE_VERIFY = value; }
                }
                public int Community_SCORE_ALLOTED
                {
                    get { return _Community_SCORE_ALLOTED; }
                    set { _Community_SCORE_ALLOTED = value; }
                }












                public int INNOVATIVE_API_SCORE_CLAIME
                {
                    get { return _INNOVATIVE_API_SCORE_CLAIME; }
                    set { _INNOVATIVE_API_SCORE_CLAIME = value; }
                }

                public int INNOVATIVE_API_SCORE_VERIFY
                {
                    get { return _INNOVATIVE_API_SCORE_VERIFY; }
                    set { _INNOVATIVE_API_SCORE_VERIFY = value; }
                }

                public int STUDENT_FEEDBACK_API_SCORE_CLAIME
                {
                    get { return _STUDENT_FEEDBACK_API_SCORE_CLAIME; }
                    set { _STUDENT_FEEDBACK_API_SCORE_CLAIME = value; }
                }

                public int STUDENT_FEEDBACK_API_SCORE_VERIFY
                {
                    get { return _STUDENT_FEEDBACK_API_SCORE_VERIFY; }
                    set { _STUDENT_FEEDBACK_API_SCORE_VERIFY = value; }
                }

                public int EXAMINATION_API_SCORE_CLAIME
                {
                    get { return _EXAMINATION_API_SCORE_CLAIME; }
                    set { _EXAMINATION_API_SCORE_CLAIME = value; }
                }


                public int EXAMINATION_API_SCORE_VERIFY
                {
                    get { return _EXAMINATION_API_SCORE_VERIFY; }
                    set { _EXAMINATION_API_SCORE_VERIFY = value; }
                }


                public int API_SCORE_CLAIME_STUDY
                {
                    get { return _API_SCORE_CLAIME_STUDY; }
                    set { _API_SCORE_CLAIME_STUDY = value; }
                }
                public int API_SCORE_VERIFIED_STUDY
                {
                    get { return _API_SCORE_VERIFIED_STUDY; }
                    set { _API_SCORE_VERIFIED_STUDY = value; }
                }
                public double PAavg_attendance
                {
                    get { return _PAavg_attendance; }
                    set { _PAavg_attendance = value; }
                }
                public string PAper_multiplying_factor
                {
                    get { return _PAper_multiplying_factor; }
                    set { _PAper_multiplying_factor = value; }
                }
                public int PAmax_Weight
                {
                    get { return _PAmax_Weight; }
                    set { _PAmax_Weight = value; }
                }
                public int PAapi_score_claim
                {
                    get { return _PAapi_score_claim; }
                    set { _PAapi_score_claim = value; }
                }
                public int PAapi_score_Verify
                {
                    get { return _PAapi_score_Verify; }
                    set { _PAapi_score_Verify = value; }
                }


                public double PRavg_attendance
                {
                    get { return _PRavg_attendance; }
                    set { _PRavg_attendance = value; }
                }
                public string PRper_multiplying_factor
                {
                    get { return _PRper_multiplying_factor; }
                    set { _PRper_multiplying_factor = value; }
                }
                public int PRmax_Weight
                {
                    get { return _PRmax_Weight; }
                    set { _PRmax_Weight = value; }
                }
                public int PRapi_score_claim
                {
                    get { return _PRapi_score_claim; }
                    set { _PRapi_score_claim = value; }
                }
                public int PRapi_score_verify
                {
                    get { return _PRapi_score_verify; }
                    set { _PRapi_score_verify = value; }
                }
                public int SESSION_ID
                {
                    get { return _SESSION_ID; }
                    set { _SESSION_ID = value; }
                }

                public DataTable PUBLISHEDJOURNAL
                {
                    get { return _PUBLISHEDJOURNAL; }
                    set { _PUBLISHEDJOURNAL = value; }
                }

                public DataTable PUBLISHEDBOOKS
                {
                    get { return _PUBLISHEDBOOKS; }
                    set { _PUBLISHEDBOOKS = value; }
                }

                public DataTable CHAPTERINBOOK
                {
                    get { return _CHAPTERINBOOK; }
                    set { _CHAPTERINBOOK = value; }
                }

                public DataTable PAPER_IN_CONFERENCE
                {
                    get { return _PAPER_IN_CONFERENCE; }
                    set { _PAPER_IN_CONFERENCE = value; }
                }

                public DataTable AVISHKAR_DETAILS
                {
                    get { return _AVISHKAR_DETAILS; }
                    set { _AVISHKAR_DETAILS = value; }
                }

                public DataTable PROJECT_CONSULTANCIES
                {
                    get { return _PROJECT_CONSULTANCIES; }
                    set { _PROJECT_CONSULTANCIES = value; }
                }

                public DataTable PATENTDETAILS
                {
                    get { return _PATENTDETAILS; }
                    set { _PATENTDETAILS = value; }
                }

                public DataTable RESEARCHGUIDANCE
                {
                    get { return _RESEARCHGUIDANCE; }
                    set { _RESEARCHGUIDANCE = value; }
                }

                public DataTable RESEARCHQUALIFICATION
                {
                    get { return _RESEARCHQUALIFICATION; }
                    set { _RESEARCHQUALIFICATION = value; }
                }
                public DataTable CoCurricular
                {
                    get { return _CoCurricular; }
                    set { _CoCurricular = value; }
                }

                public DataTable ATTENDANCEPERFORMANCE
                {
                    get { return _ATTENDANCEPERFORMANCE; }
                    set { _ATTENDANCEPERFORMANCE = value; }
                }

                public DataTable PERFORMANCERESULT
                {
                    get { return _PERFORMANCERESULT; }
                    set { _PERFORMANCERESULT = value; }
                }

                public DataTable INNOVATIVETEACHING
                {
                    get { return _INNOVATIVETEACHING; }
                    set { _INNOVATIVETEACHING = value; }
                }
                public DataTable STUDENT_FEEDBACK
                {
                    get { return _STUDENT_FEEDBACK; }
                    set { _STUDENT_FEEDBACK = value; }
                }
                public DataTable EXAMINATION_WORK
                {
                    get { return _EXAMINATION_WORK; }
                    set { _EXAMINATION_WORK = value; }
                }

                        


                public string Study_Material
                {
                    get { return _Study_Material; }
                    set { _Study_Material = value; }
                }

                public string API_Score_Claimed
                {
                    get { return _API_Score_Claimed; }
                    set { _API_Score_Claimed = value; }
                }

                public int Verified_API_Score
                {
                    get { return _Verified_API_Score; }
                    set { _Verified_API_Score = value; }
                }

                public DataTable TEACHINGACTIVITY
                {
                    get { return _TEACHINGACTIVITY; }
                    set { _TEACHINGACTIVITY = value; }
                }

                public DataTable ENGAGINGLECTURE
                {
                    get { return _ENGAGINGLECTURE; }
                    set { _ENGAGINGLECTURE = value; }
                }

                public DataTable CU_CURRICULAR_ACTIVITY
                {
                    get { return _CU_CURRICULAR_ACTIVITY; }
                    set { _CU_CURRICULAR_ACTIVITY = value; }
                }

                public DataTable ADMINACADEMIC
                { 
                    get { return _ADMINACADEMIC; }
                    set { _ADMINACADEMIC = value; }
                }

                public DataTable PROFESSIONALDEVELOP
                {
                    get { return _PROFESSIONALDEVELOP; }
                    set { _PROFESSIONALDEVELOP = value; }
                }

                public DataTable LECTURE_AND_ADUTIES
                {
                    get { return _LECTURE_AND_ADUTIES; }
                    set { _LECTURE_AND_ADUTIES = value; }
                }
                public DataTable STUDY_MATERIAL_RESOURSE
                {
                    get { return _STUDY_MATERIAL_RESOURSE; }
                    set { _STUDY_MATERIAL_RESOURSE = value; }
                }
                #endregion

            }
        }
    }
    
}
