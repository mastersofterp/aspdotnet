using System;
using System.Data;
using System.Web;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            /// <summary>
            /// This CourseController is used to control Course table.
            /// </summary>
            public partial class CourseController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>
                string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public CourseController()
                { }
                /// <summary>
                /// This method is used to add new course in the Course table.
                /// </summary>
                /// <param name="objCourse">objCourse is the object of Course class</param>
                /// <returns>Integer CustomStatus - Record Added or Error</returns>
                //public int AddCourse(Course objCourse)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;

                //        //Add New Course
                //        objParams = new SqlParameter[46];
                //        objParams[0] = new SqlParameter("@P_COURSE_NAME", objCourse.CourseName);
                //        objParams[1] = new SqlParameter("@P_SCHEMENO", objCourse.SchNo);
                //        objParams[2] = new SqlParameter("@P_CCODE", objCourse.CCode);
                //        objParams[3] = new SqlParameter("@P_SUBID", objCourse.SubID);

                //        objParams[4] = new SqlParameter("@P_ELECT", objCourse.Elect);
                //        objParams[5] = new SqlParameter("@P_GLOBALELE", objCourse.GlobalEle);
                //        objParams[6] = new SqlParameter("@P_CREDITS", objCourse.Credits);

                //        objParams[7] = new SqlParameter("@P_LECTURE", objCourse.Lecture);
                //        objParams[8] = new SqlParameter("@P_THEORY", objCourse.Theory);
                //        objParams[9] = new SqlParameter("@P_PRACTICAL", objCourse.Practical);

                //        objParams[10] = new SqlParameter("@P_MAXMARKS_I", objCourse.MaxMarks_I);
                //        objParams[11] = new SqlParameter("@P_MAXMARKS_E", objCourse.ExtermarkMax);
                //        objParams[12] = new SqlParameter("@P_MINMARKS", objCourse.ExtermarkMin);

                //        objParams[13] = new SqlParameter("@P_S1MAX", objCourse.S1Max);
                //        objParams[14] = new SqlParameter("@P_S2MAX", objCourse.S2Max);
                //        objParams[15] = new SqlParameter("@P_S3MAX", objCourse.S3Max);
                //        objParams[16] = new SqlParameter("@P_S4MAX", objCourse.S4Max);
                //        objParams[17] = new SqlParameter("@P_S5MAX", objCourse.S5Max);
                //        objParams[18] = new SqlParameter("@P_S6MAX", objCourse.S6Max);
                //        objParams[19] = new SqlParameter("@P_S7MAX", objCourse.S7Max);
                //        objParams[20] = new SqlParameter("@P_S8MAX", objCourse.S8Max);
                //        objParams[21] = new SqlParameter("@P_S9MAX", objCourse.S9Max);
                //        objParams[22] = new SqlParameter("@P_S10MAX", objCourse.S10Max);

                //        objParams[23] = new SqlParameter("@P_S1Min", objCourse.S1Min);
                //        objParams[24] = new SqlParameter("@P_S2Min", objCourse.S2Min);
                //        objParams[25] = new SqlParameter("@P_S3Min", objCourse.S3Min);
                //        objParams[26] = new SqlParameter("@P_S4Min", objCourse.S4Min);
                //        objParams[27] = new SqlParameter("@P_S5Min", objCourse.S5Min);
                //        objParams[28] = new SqlParameter("@P_S6Min", objCourse.S6Min);
                //        objParams[29] = new SqlParameter("@P_S7Min", objCourse.S7Min);
                //        objParams[30] = new SqlParameter("@P_S8Min", objCourse.S8Min);
                //        objParams[31] = new SqlParameter("@P_S9Min", objCourse.S9Min);
                //        objParams[32] = new SqlParameter("@P_S10Min", objCourse.S10Min);

                //        objParams[33] = new SqlParameter("@P_ASSIGNMAX", objCourse.AssignMax);
                //        objParams[34] = new SqlParameter("@P_DRAWING", objCourse.Drawing);

                //        //objParams[25] = new SqlParameter("@P_PTMAXMARKI", objCourse.Ptmaxmarki);
                //        //objParams[26] = new SqlParameter("@P_PTMAXMARKE", objCourse.Ptmaxmarke);

                //        //new fields
                //        objParams[35] = new SqlParameter("@P_LEVELNO", objCourse.Levelno);
                //        objParams[36] = new SqlParameter("@P_GROUPNO", objCourse.Groupno);
                //        objParams[37] = new SqlParameter("@P_PREREQUISITE", objCourse.Prerequisite);
                //        objParams[38] = new SqlParameter("@P_PREREQUISITE_CREDIT", objCourse.Prerequisite_cr);

                //        objParams[39] = new SqlParameter("@P_GRADE", objCourse.Grade);
                //        objParams[40] = new SqlParameter("@P_MINGRADE", objCourse.MinGrade);


                //        objParams[41] = new SqlParameter("@P_COLLEGE_CODE", objCourse.CollegeCode);
                //        objParams[42] = new SqlParameter("@P_SEMESTERNO", objCourse.SemesterNo);
                //        objParams[43] = new SqlParameter("@P_SPECIALISATIONNO", objCourse.Specialisation);
                //        objParams[44] = new SqlParameter("@P_PAPER_HRS", objCourse.Paper_hrs);

                //        objParams[45] = new SqlParameter("@P_COURSE_NO", SqlDbType.Int);
                //        objParams[45].Direction = ParameterDirection.Output;

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSE_SP_INS_COURSE", objParams, true);
                //        if (Convert.ToInt32(ret) == -99)
                //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //        else
                //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AddCourse-> " + ex.ToString());
                //    }

                //    return retStatus;
                //}
                /// <summary>
                /// This method is used to update existing course from Course table.
                /// </summary>
                /// <param name="objCourse">objCourse is the object of Course class</param>
                /// <returns>Integer CustomStatus - Record Updated or Error</returns>
                //public int UpdateCourse(Course objCourse)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;

                //        //Edit Course
                //        objParams = new SqlParameter[48];

                //        objParams[0] = new SqlParameter("@P_SCHNO", objCourse.SchNo);
                //        objParams[1] = new SqlParameter("@P_DELSCHNO", objCourse.DelSchNo);
                //        objParams[2] = new SqlParameter("@P_SCHEMENO", objCourse.SchemeNo);
                //        objParams[3] = new SqlParameter("@P_CCODE", objCourse.CCode);
                //        objParams[4] = new SqlParameter("@P_COURSE_NAME", objCourse.CourseName);

                //        objParams[5] = new SqlParameter("@P_SUBID", objCourse.SubID);
                //        objParams[6] = new SqlParameter("@P_ELECT", objCourse.Elect);
                //        objParams[7] = new SqlParameter("@P_GLOBALELE", objCourse.GlobalEle);
                //        objParams[8] = new SqlParameter("@P_CREDITS", objCourse.Credits);

                //        objParams[9] = new SqlParameter("@P_LECTURE", objCourse.Lecture);
                //        objParams[10] = new SqlParameter("@P_THEORY", objCourse.Theory);

                //        objParams[11] = new SqlParameter("@P_PRACTICAL", objCourse.Practical);
                //        objParams[12] = new SqlParameter("@P_MAXMARKS_I", objCourse.MaxMarks_I);
                //        objParams[13] = new SqlParameter("@P_MAXMARKS_E", objCourse.ExtermarkMax);
                //        objParams[14] = new SqlParameter("@P_MINMARKS", objCourse.ExtermarkMin);

                //        objParams[15] = new SqlParameter("@P_S1MAX", objCourse.S1Max);
                //        objParams[16] = new SqlParameter("@P_S2MAX", objCourse.S2Max);
                //        objParams[17] = new SqlParameter("@P_S3MAX", objCourse.S3Max);
                //        objParams[18] = new SqlParameter("@P_S4MAX", objCourse.S4Max);
                //        objParams[19] = new SqlParameter("@P_S5MAX", objCourse.S5Max);
                //        objParams[20] = new SqlParameter("@P_S6MAX", objCourse.S6Max);
                //        objParams[21] = new SqlParameter("@P_S7MAX", objCourse.S7Max);
                //        objParams[22] = new SqlParameter("@P_S8MAX", objCourse.S8Max);
                //        objParams[23] = new SqlParameter("@P_S9MAX", objCourse.S9Max);
                //        objParams[24] = new SqlParameter("@P_S10MAX", objCourse.S10Max);

                //        objParams[25] = new SqlParameter("@P_S1Min", objCourse.S1Min);
                //        objParams[26] = new SqlParameter("@P_S2Min", objCourse.S2Min);
                //        objParams[27] = new SqlParameter("@P_S3Min", objCourse.S3Min);
                //        objParams[28] = new SqlParameter("@P_S4Min", objCourse.S4Min);
                //        objParams[29] = new SqlParameter("@P_S5Min", objCourse.S5Min);
                //        objParams[30] = new SqlParameter("@P_S6Min", objCourse.S6Min);
                //        objParams[31] = new SqlParameter("@P_S7Min", objCourse.S7Min);
                //        objParams[32] = new SqlParameter("@P_S8Min", objCourse.S8Min);
                //        objParams[33] = new SqlParameter("@P_S9Min", objCourse.S9Min);
                //        objParams[34] = new SqlParameter("@P_S10Min", objCourse.S10Min);

                //        objParams[35] = new SqlParameter("@P_GRADE", objCourse.Grade);
                //        objParams[36] = new SqlParameter("@P_MINGRADE", objCourse.MinGrade);

                //        objParams[37] = new SqlParameter("@P_ASSIGNMAX", objCourse.AssignMax);
                //        //objParams[26] = new SqlParameter("@P_PTMAXMARKI", objCourse.Ptmaxmarki);
                //        //objParams[27] = new SqlParameter("@P_PTMAXMARKE", objCourse.Ptmaxmarke);

                //        //new fields
                //        objParams[38] = new SqlParameter("@P_LEVELNO", objCourse.Levelno);
                //        objParams[39] = new SqlParameter("@P_GROUPNO", objCourse.Groupno);
                //        objParams[40] = new SqlParameter("@P_PREREQUISITE", objCourse.Prerequisite);
                //        objParams[41] = new SqlParameter("@P_PREREQUISITE_CREDIT", objCourse.Prerequisite_cr);

                //        objParams[42] = new SqlParameter("@P_COLLEGE_CODE", objCourse.CollegeCode);
                //        objParams[43] = new SqlParameter("@P_SEMESTERNO", objCourse.SemesterNo);
                //        objParams[44] = new SqlParameter("@P_SPECIALISATIONNO", objCourse.Specialisation);
                //        objParams[45] = new SqlParameter("@P_BOS_DEPTNO", objCourse.Deptno);
                //        objParams[46] = new SqlParameter("@P_PAPER_HRS", objCourse.Paper_hrs);
                //        objParams[47] = new SqlParameter("@P_COURSE_NO", objCourse.CourseNo);
                //        objParams[47].Direction = ParameterDirection.InputOutput;

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSE_SP_UPD_COURSE", objParams, true);
                //        if (Convert.ToInt32(ret) == -99)
                //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //        else
                //            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.UpdateCourse -> " + ex.ToString());
                //    }

                //    return retStatus;
                //}
                /// <summary>
                /// This method is used to get courses according to scheme no.
                /// </summary>
                /// <param name="schemeno">Get courses as per this schemeno.</param>
                /// <returns>DataSet</returns>
                /// 


                public SqlDataReader GetSchemeSemesterByUser(int ua_idno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_IDNO", ua_idno);
                        dr = objSQLHelper.ExecuteReaderSP("PKG_GET_SCHEME_SEMESTER_BY_USERID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GetShemeSemester-> " + ex.ToString());
                    }
                    return dr;
                }


                public int CopyCourseToNewScheme(int fromCourseNo, int fromSchemeNo, int toSchemeNo, int SemNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                            { 
                                new SqlParameter("@P_FROMCOURSENO", fromCourseNo),                               
                                new SqlParameter("@P_FROMSCHEMENO", fromSchemeNo),
                                
                                new SqlParameter("@P_TOSCHEMENO", toSchemeNo),
                                new SqlParameter("@P_TOSEMESTERNO", SemNo),
                               
                                new SqlParameter("@P_OUT", SqlDbType.Int) 
                            };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objDataAccess.ExecuteNonQuerySP("PROC_ACAD_COPY_COURSE_AND_INSERT", sqlParams, true);

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
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.CopyCourseToNewScheme-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int InsertTransaction(int feeitem, int Sessionno, string amount, int subjecttype, int feeitemtransid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_FEEITEMID", feeitem);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[2] = new SqlParameter("@P_AMOUNT", amount);
                        objParams[3] = new SqlParameter("@P_SUBTYPE", subjecttype);
                        objParams[4] = new SqlParameter("@P_FEEITEMTRANSID", feeitemtransid);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_UPDATE_FEES_TRANSACTION", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AddCourse-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public SqlDataReader GetFeesDefinitionDetails(int FeeItemTransId)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_FeeItemTransId", FeeItemTransId);
                        dr = objSQLHelper.ExecuteReaderSP("PKG_SP_RET_FEES_DEFINITION", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetSingleSession-> " + ex.ToString());
                    }
                    return dr;
                }
                public DataSet GetFeeDetails()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        //SqlParameter[] objParams = new SqlParameter[1];
                        //objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);

                        ds = objSQLHelper.ExecuteDataSet("PKG_INSERT_FEES_TRANSACTION_REPORT");

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetFirstYearCoursesBySchemeNo-> " + ex.ToString());
                    }

                    return ds;
                }

                public int AddCourse(Course objCourse)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New Course
                        objParams = new SqlParameter[59];
                        objParams[0] = new SqlParameter("@P_COURSE_NAME", objCourse.CourseName);
                        objParams[1] = new SqlParameter("@P_SHORTNAME", objCourse.CourseShortName);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", objCourse.SchNo);
                        objParams[3] = new SqlParameter("@P_CCODE", objCourse.CCode);
                        objParams[4] = new SqlParameter("@P_SUBID", objCourse.SubID);

                        objParams[5] = new SqlParameter("@P_ELECT", objCourse.Elect);
                        objParams[6] = new SqlParameter("@P_GLOBALELE", objCourse.GlobalEle);
                        objParams[7] = new SqlParameter("@P_CREDITS", objCourse.Credits);

                        objParams[8] = new SqlParameter("@P_LECTURE", objCourse.Lecture);
                        objParams[9] = new SqlParameter("@P_THEORY", objCourse.Theory);
                        objParams[10] = new SqlParameter("@P_PRACTICAL", objCourse.Practical);

                        objParams[11] = new SqlParameter("@P_MAXMARKS_I", objCourse.MaxMarks_I);
                        objParams[12] = new SqlParameter("@P_MAXMARKS_E", objCourse.ExtermarkMax);
                        objParams[13] = new SqlParameter("@P_MINMARKS", objCourse.ExtermarkMin);

                        objParams[14] = new SqlParameter("@P_S1MAX", objCourse.S1Max);
                        objParams[15] = new SqlParameter("@P_S2MAX", objCourse.S2Max);
                        objParams[16] = new SqlParameter("@P_S3MAX", objCourse.S3Max);
                        objParams[17] = new SqlParameter("@P_S4MAX", objCourse.S4Max);
                        objParams[18] = new SqlParameter("@P_S5MAX", objCourse.S5Max);
                        objParams[19] = new SqlParameter("@P_S6MAX", objCourse.S6Max);
                        objParams[20] = new SqlParameter("@P_S7MAX", objCourse.S7Max);
                        objParams[21] = new SqlParameter("@P_S8MAX", objCourse.S8Max);
                        objParams[22] = new SqlParameter("@P_S9MAX", objCourse.S9Max);
                        objParams[23] = new SqlParameter("@P_S10MAX", objCourse.S10Max);
                        objParams[24] = new SqlParameter("@P_S1Min", objCourse.S1Min);
                        objParams[25] = new SqlParameter("@P_S2Min", objCourse.S2Min);
                        objParams[26] = new SqlParameter("@P_S3Min", objCourse.S3Min);
                        objParams[27] = new SqlParameter("@P_S4Min", objCourse.S4Min);
                        objParams[28] = new SqlParameter("@P_S5Min", objCourse.S5Min);
                        objParams[29] = new SqlParameter("@P_S6Min", objCourse.S6Min);
                        objParams[30] = new SqlParameter("@P_S7Min", objCourse.S7Min);
                        objParams[31] = new SqlParameter("@P_S8Min", objCourse.S8Min);
                        objParams[32] = new SqlParameter("@P_S9Min", objCourse.S9Min);
                        objParams[33] = new SqlParameter("@P_S10Min", objCourse.S10Min);

                        objParams[34] = new SqlParameter("@P_ASSIGNMAX", objCourse.AssignMax);
                        objParams[35] = new SqlParameter("@P_DRAWING", objCourse.Drawing);

                        //objParams[25] = new SqlParameter("@P_PTMAXMARKI", objCourse.Ptmaxmarki);
                        //objParams[26] = new SqlParameter("@P_PTMAXMARKE", objCourse.Ptmaxmarke);

                        //new fields
                        objParams[36] = new SqlParameter("@P_LEVELNO", objCourse.Levelno);
                        objParams[37] = new SqlParameter("@P_GROUPNO", objCourse.Groupno);

                        objParams[38] = new SqlParameter("@P_PREREQUISITE", objCourse.Prerequisite);
                        objParams[39] = new SqlParameter("@P_PREREQUISITE_CREDIT", objCourse.Prerequisite_cr);

                        objParams[40] = new SqlParameter("@P_GRADE", objCourse.Grade);
                        objParams[41] = new SqlParameter("@P_MINGRADE", objCourse.MinGrade);


                        objParams[42] = new SqlParameter("@P_COLLEGE_CODE", objCourse.CollegeCode);
                        objParams[43] = new SqlParameter("@P_SEMESTERNO", objCourse.SemesterNo);
                        objParams[44] = new SqlParameter("@P_SPECIALISATIONNO", objCourse.Specialisation);
                        objParams[45] = new SqlParameter("@P_PAPER_HRS", objCourse.Paper_hrs);
                        objParams[46] = new SqlParameter("@P_CGROUPNO", objCourse.CGroupno);
                        objParams[47] = new SqlParameter("@P_SCALING", objCourse.Scaling);
                        objParams[48] = new SqlParameter("@P_CATEGORYNO", objCourse.Categoryno);
                        objParams[49] = new SqlParameter("@P_BOS_DEPTNO", objCourse.Deptno);
                        objParams[50] = new SqlParameter("@P_ORGANIZATIONID", objCourse.OrgId);//Added By Dileep Kare on 11.03.2022
                        objParams[51] = new SqlParameter("@P_INTERNALMARKMIN", objCourse.InterMarkMin);//Added By Dileep Kare on 25.03.2022 as per discussed with umesh sir and ankhush sir.
                        objParams[52] = new SqlParameter("@P_TOTAL_MARKS", objCourse.Total_Marks);     //Added By Dileep Kare on 25.03.2022 as per discussed with umesh sir and ankhush sir.
                        objParams[53] = new SqlParameter("@P_MIN_TOTAL_MARKS", objCourse.MinTotalMarks);
                        objParams[54] = new SqlParameter("@P_ISVALUE_ADDED", objCourse.ValueAdded);
                        objParams[55] = new SqlParameter("@P_IS_SPECIAL", objCourse.Specialisation);
                        objParams[56] = new SqlParameter("@P_IS_FEEFBACK", objCourse.IsFeedback);
                        objParams[57] = new SqlParameter("@P_IS_AUDIT", objCourse.IsAudit);
                        objParams[58] = new SqlParameter("@P_COURSE_NO", SqlDbType.Int);
                        objParams[58].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSE_SP_INS_COURSE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AddCourse-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public DataSet GetSchemeforAllotmentCCode(string CCode, int SemesterNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_CCODE", CCode);
                        objParams[1] = new SqlParameter("@P_SemesterNO", SemesterNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SCHEME_SP_ALLOT_CCODEWISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseAllotment-> " + ex.ToString());
                    }
                    return ds;
                }


                public int UpdateCourse(Course objCourse, string ipaddress, int uano)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Edit Course
                        objParams = new SqlParameter[62];

                        objParams[0] = new SqlParameter("@P_SCHNO", objCourse.SchNo);
                        objParams[1] = new SqlParameter("@P_DELSCHNO", objCourse.DelSchNo);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", objCourse.SchemeNo);
                        objParams[3] = new SqlParameter("@P_CCODE", objCourse.CCode);
                        objParams[4] = new SqlParameter("@P_COURSE_NAME", objCourse.CourseName);
                        objParams[5] = new SqlParameter("@P_SHORTNAME", objCourse.CourseShortName);
                        objParams[6] = new SqlParameter("@P_SUBID", objCourse.SubID);
                        objParams[7] = new SqlParameter("@P_ELECT", objCourse.Elect);
                        objParams[8] = new SqlParameter("@P_GLOBALELE", objCourse.GlobalEle);
                        objParams[9] = new SqlParameter("@P_CREDITS", objCourse.Credits);

                        objParams[10] = new SqlParameter("@P_LECTURE", objCourse.Lecture);
                        objParams[11] = new SqlParameter("@P_THEORY", objCourse.Theory);

                        objParams[12] = new SqlParameter("@P_PRACTICAL", objCourse.Practical);
                        objParams[13] = new SqlParameter("@P_MAXMARKS_I", objCourse.MaxMarks_I);
                        objParams[14] = new SqlParameter("@P_MAXMARKS_E", objCourse.ExtermarkMax);
                        objParams[15] = new SqlParameter("@P_MINMARKS", objCourse.ExtermarkMin);

                        objParams[16] = new SqlParameter("@P_S1MAX", objCourse.S1Max);
                        objParams[17] = new SqlParameter("@P_S2MAX", objCourse.S2Max);
                        objParams[18] = new SqlParameter("@P_S3MAX", objCourse.S3Max);
                        objParams[19] = new SqlParameter("@P_S4MAX", objCourse.S4Max);
                        objParams[20] = new SqlParameter("@P_S5MAX", objCourse.S5Max);
                        objParams[21] = new SqlParameter("@P_S6MAX", objCourse.S6Max);
                        objParams[22] = new SqlParameter("@P_S7MAX", objCourse.S7Max);
                        objParams[23] = new SqlParameter("@P_S8MAX", objCourse.S8Max);
                        objParams[24] = new SqlParameter("@P_S9MAX", objCourse.S9Max);
                        objParams[25] = new SqlParameter("@P_S10MAX", objCourse.S10Max);

                        objParams[26] = new SqlParameter("@P_S1Min", objCourse.S1Min);
                        objParams[27] = new SqlParameter("@P_S2Min", objCourse.S2Min);
                        objParams[28] = new SqlParameter("@P_S3Min", objCourse.S3Min);
                        objParams[29] = new SqlParameter("@P_S4Min", objCourse.S4Min);
                        objParams[30] = new SqlParameter("@P_S5Min", objCourse.S5Min);
                        objParams[31] = new SqlParameter("@P_S6Min", objCourse.S6Min);
                        objParams[32] = new SqlParameter("@P_S7Min", objCourse.S7Min);
                        objParams[33] = new SqlParameter("@P_S8Min", objCourse.S8Min);
                        objParams[34] = new SqlParameter("@P_S9Min", objCourse.S9Min);
                        objParams[35] = new SqlParameter("@P_S10Min", objCourse.S10Min);

                        objParams[36] = new SqlParameter("@P_GRADE", objCourse.Grade);
                        objParams[37] = new SqlParameter("@P_MINGRADE", objCourse.MinGrade);

                        objParams[38] = new SqlParameter("@P_ASSIGNMAX", objCourse.AssignMax);
                        //objParams[26] = new SqlParameter("@P_PTMAXMARKI", objCourse.Ptmaxmarki);
                        //objParams[27] = new SqlParameter("@P_PTMAXMARKE", objCourse.Ptmaxmarke);

                        //new fields
                        objParams[39] = new SqlParameter("@P_LEVELNO", objCourse.Levelno);
                        objParams[40] = new SqlParameter("@P_GROUPNO", objCourse.Groupno);
                        objParams[41] = new SqlParameter("@P_CGROUPNO", objCourse.CGroupno);
                        objParams[42] = new SqlParameter("@P_PREREQUISITE", objCourse.Prerequisite);
                        objParams[43] = new SqlParameter("@P_PREREQUISITE_CREDIT", objCourse.Prerequisite_cr);

                        objParams[44] = new SqlParameter("@P_COLLEGE_CODE", objCourse.CollegeCode);
                        objParams[45] = new SqlParameter("@P_SEMESTERNO", objCourse.SemesterNo);
                        objParams[46] = new SqlParameter("@P_SPECIALISATIONNO", objCourse.Specialisation);
                        objParams[47] = new SqlParameter("@P_BOS_DEPTNO", objCourse.Deptno);
                        objParams[48] = new SqlParameter("@P_PAPER_HRS", objCourse.Paper_hrs);
                        objParams[49] = new SqlParameter("@P_SCALING", objCourse.Scaling);
                        objParams[50] = new SqlParameter("@P_CATEGORYNO", objCourse.Categoryno);
                        objParams[51] = new SqlParameter("@P_IPADD", ipaddress);//Added by Dileep on Date 10122020
                        objParams[52] = new SqlParameter("@P_UA_NO", uano);//Added by Dileep on Date 10122020
                        objParams[53] = new SqlParameter("@P_ORGANIZATIONID", objCourse.OrgId);//Added By Dileep Kare on 11.03.2022
                        objParams[54] = new SqlParameter("@P_INTERNALMARKMIN", objCourse.InterMarkMin);//Added by Dileep Kare on 25.03.2022 as per discussed with umesh sir and ankhush sir.
                        objParams[55] = new SqlParameter("@P_TOTAL_MARKS", objCourse.Total_Marks);     //Added by Dileep Kare on 25.03.2022 as per discussed with umesh sir and ankhush sir.
                        objParams[56] = new SqlParameter("@P_MIN_TOTAL_MARKS", objCourse.MinTotalMarks);
                        objParams[57] = new SqlParameter("@P_ISVALUE_ADDED", objCourse.ValueAdded);
                        objParams[58] = new SqlParameter("@P_IS_SPECIAL", objCourse.Specialisation);
                        objParams[59] = new SqlParameter("@P_IS_FEEFBACK", objCourse.IsFeedback);
                        objParams[60] = new SqlParameter("@P_IS_AUDIT", objCourse.IsAudit);

                        objParams[61] = new SqlParameter("@P_COURSE_NO", objCourse.CourseNo);
                        objParams[61].Direction = ParameterDirection.InputOutput;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSE_SP_UPD_COURSE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.UpdateCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }



                public DataSet GetCoursesBySchemeNo(int schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_RET_COURSE_BYSCHEMENO", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCoursesBySchemeNo-> " + ex.ToString());
                    }

                    return ds;
                }

                /// <summary>
                /// This method is used to get courses according to coursecode & schemeno.
                /// </summary>
                /// <param name="coursecode">Used to retrieve course of current coursecode</param>
                /// <param name="schemeno">Used to retrieve course of current schemeno</param>
                /// <returns>SqlDataReader</returns>
                public SqlDataReader GetCourses(int coursecode, int schemeno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_COURSENO", coursecode);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_COURSE_SP_RET_COURSE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourses-> " + ex.ToString());
                    }

                    return dr;
                }

                /// <summary>
                /// This method is used to get Scheme According to CCode,degree,semno,vtype.
                /// </summary>
                /// <param name="ccode">Used to get scheme as per this coursecode </param>
                /// <param name="degree">Used to get scheme as per this degree </param>
                /// <param name="semno">Used to get scheme as per this schemeno</param>
                /// <param name="vtype">Used to get type</param>
                /// <returns>DataSet</returns>
                public DataSet GetSchemeNoByCCode(string ccode, string degree, int semno, int batchno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_CCODE", ccode);
                        objParams[1] = new SqlParameter("@P_DEGREE", degree);
                        objParams[2] = new SqlParameter("@P_SEM_NO", semno);
                        objParams[3] = new SqlParameter("@P_BATCHNO", batchno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SCHEME_SP_RET_GELE_BYCCODE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetSchemeNoByCCode-> " + ex.ToString());
                    }

                    return ds;
                }
                /// <summary>
                /// This method is used to get Courses According to Schemeno.
                /// </summary>
                /// <param name="SchemeNo">Used to get Courses as per this schemeNo </param>
                /// <returns>DataSet</returns>

                /// <summary>
                /// This method is used to get global scheme.
                /// </summary>
                /// <param name="ccode">Used to get global scheme as per this coursecode </param>
                /// <param name="degree">Used to get global scheme as per this degree </param>
                /// <param name="schemeno">Used to get global scheme as per this </param>
                /// <param name="sem_no">Used to get global scheme as per this  scheme</param>
                /// <param name="vtype">Used to get global scheme as per this type</param>
                /// <returns>DataSet</returns>
                public DataSet GetGElecScheme(string ccode, string degree, int schemeno, int sem_no, int batchno)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_CCODE", ccode);
                        objParams[1] = new SqlParameter("@P_DEGREE", degree);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[3] = new SqlParameter("@P_SEM_NO", sem_no);
                        objParams[4] = new SqlParameter("@P_BATCHNO", batchno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SCHEME_SP_RET_GELECOURSE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetGElecScheme-> " + ex.ToString());
                    }

                    return ds;
                }
                // FOR INSERT THE COURSE DETAILS 2 FEB 2014
                public int UpdateCoursedetails(string rollno, int courseno, int status, int session, int semepro, int UANO, int IDNO, string ipaddress, int oldsem, int oldsession, string oldstatus, string oldgrade, string newgrade)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_ROLLNO", rollno),
                            new SqlParameter("@P_PREVSTATUS", status),
                            new SqlParameter("@P_SESSIONNO", session),
                            new SqlParameter("@P_SEMEPRO", semepro),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_UANO", UANO),
                            new SqlParameter("@P_IDNO", IDNO),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_OLDSEM", oldsem),
                            new SqlParameter("@P_OLDSESSION", oldsession),
                            new SqlParameter("@P_OLDSTATUS", oldstatus),
                            new SqlParameter("@P_OLDGRADE", oldgrade),
                            new SqlParameter("@P_NEWGRADE", newgrade),
                            new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_STUDENT_COURSE_DETAILS", objParams, true);
                        if (ret != null && ret.ToString() == "1")
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.MarksEntryController.UpdateCoursedetails --> " + ex.ToString());
                    }
                    return retStatus;
                }
                //GET COURSE DETAILS IN LISVIEW 01/02/2014
                public DataSet GetCourseDetails(string rollno, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ROLLNO", rollno);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);


                        ds = objSQLHelper.ExecuteDataSetSP("GET_STUDENT_COURSE_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseDetails-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetCourseAllotment(int session, int scheme, int semesterno, int Degreeno, int Branchno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", scheme);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_DEGREENO", Degreeno);
                        objParams[4] = new SqlParameter("@P_BRANCHNO", Branchno);
                        //  objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_RET_COURSE_ALLOT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseAllotment-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetCourseAllotmentSectionwise(int session, int scheme, int semesterno, int Degreeno, int Branchno, int sectionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", scheme);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_DEGREENO", Degreeno);
                        objParams[4] = new SqlParameter("@P_BRANCHNO", Branchno);
                        objParams[5] = new SqlParameter("@P_SECTIONNO", sectionno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_RET_COURSE_ALLOT_SECTIONWISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseAllotment-> " + ex.ToString());
                    }
                    return ds;
                }


                //public int AddCourseAllot(Student_Acd objStudent)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = new SqlParameter[10];
                //        objParams[0] = new SqlParameter("@P_SESSIONNO", objStudent.SessionNo);
                //        objParams[1] = new SqlParameter("@P_SCHEMENO", objStudent.SchemeNo);
                //        objParams[2] = new SqlParameter("@P_SEMESTERNO", objStudent.Sem);
                //        objParams[3] = new SqlParameter("@P_SECTIONNO", objStudent.Sectionno);
                //        objParams[4] = new SqlParameter("@P_UA_NO", objStudent.UA_No);
                //        objParams[5] = new SqlParameter("@P_ADTEACHER", objStudent.AdTeacher);
                //        objParams[6] = new SqlParameter("@P_SUBID", objStudent.Pract_Theory);
                //        objParams[7] = new SqlParameter("@P_COURSENO", objStudent.CourseNo);
                //        objParams[8] = new SqlParameter("@P_TH_PR", objStudent.Th_Pr);
                //        //objParams[9] = new SqlParameter("@P_ALLOWMARKENTRY", allowmarkentry);
                //        objParams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                //        objParams[9].Direction = ParameterDirection.Output;

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSE_SP_INS_COURSEBYALLOTMENT", objParams, true);
                //        if (ret != null)
                //            retStatus = Convert.ToInt32(ret);
                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AddCourseAllot-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                public int AddCourseAllot(Student_Acd objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objStudent.SessionNo);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", objStudent.SchemeNo);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", objStudent.Sem);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", objStudent.Sectionno);
                        objParams[4] = new SqlParameter("@P_UA_NO", objStudent.UA_No);
                        objParams[5] = new SqlParameter("@P_ADTEACHER", objStudent.AdTeacher);
                        objParams[6] = new SqlParameter("@P_SUBID", objStudent.Pract_Theory);
                        objParams[7] = new SqlParameter("@P_COURSENO", objStudent.CourseNo);
                        objParams[8] = new SqlParameter("@P_TH_PR", objStudent.Th_Pr);
                        //objParams[9] = new SqlParameter("@P_INTAKE", objStudent.Intake);
                        //objParams[9] = new SqlParameter("@P_ALLOWMARKENTRY", allowmarkentry);
                        objParams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSE_SP_INS_COURSEBYALLOTMENT", objParams, true);
                        if (ret != null)
                            retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AddCourseAllot-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //public int DeleteCourseAllot(Student_Acd objStudent)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;

                //        //Edit Course
                //        objParams = new SqlParameter[8];
                //        objParams[0] = new SqlParameter("@P_SESSIONNO", objStudent.SessionNo);
                //        objParams[1] = new SqlParameter("@P_SCHEMENO", objStudent.SchemeNo);
                //        objParams[2] = new SqlParameter("@P_UA_NO", objStudent.UA_No);
                //        objParams[3] = new SqlParameter("@P_COURSENO", objStudent.CourseNo);
                //        objParams[4] = new SqlParameter("@P_SUBID", objStudent.sub_id);
                //        objParams[5] = new SqlParameter("@P_TH_PR", objStudent.Th_Pr);
                //        objParams[6] = new SqlParameter("@P_SECTIONNO", objStudent.Sectionno);
                //        objParams[7] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                //        objParams[7].Direction = ParameterDirection.Output;
                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSE_SP_DEL_COURSEALLOT", objParams, true);

                //        if (Convert.ToInt32(ret) == -99)
                //            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //        else
                //            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.DeleteCourseAllot -> " + ex.ToString());
                //    }

                //    return retStatus;
                //}



                public int DeleteCourseAllot(Student_Acd objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Edit Course
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objStudent.SessionNo);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", objStudent.SchemeNo);
                        objParams[2] = new SqlParameter("@P_UA_NO", objStudent.UA_No);
                        objParams[3] = new SqlParameter("@P_COURSENO", objStudent.CourseNo);
                        objParams[4] = new SqlParameter("@P_SUBID", objStudent.Th_Pr);
                        objParams[5] = new SqlParameter("@P_TH_PR", objStudent.Th_Pr);
                        objParams[6] = new SqlParameter("@P_SECTIONNO", objStudent.Sectionno);
                        objParams[7] = new SqlParameter("@P_UA_NO_DELETE", objStudent.Ua_no_delete);
                        objParams[8] = new SqlParameter("@P_INTAKE", objStudent.Intake);
                        objParams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSE_SP_DEL_COURSEALLOT", objParams, true);


                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.DeleteCourseAllot -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetAllCourse(int schemeno, string coursenos, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[1] = new SqlParameter("@P_COURSENOS", coursenos);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_ALL_COURSE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllCourse-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetCourseOffered(int schemeno, int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_RET_OFFERED_COURSE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetCourseOffered-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetCourseOfferedWithSemester(int schemeno, int sessionno, string semesternos)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNOS", semesternos);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_RET_OFFERED_COURSE_WITH_SEMESTERNOS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetCourseOffered-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetViewOfferedCourseWithSemester(int schemeno, int sessionno, string semesternos)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNOS", semesternos);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_GET_OFFERED_COURSE_WITH_SEMESTERNOS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetCourseOffered-> " + ex.ToString());
                    }
                    return ds;
                }
                public int UpdateOfferedCourse(int SchemeNo, string offcourse, string sem, string sqno, int ua_no, string ipAdd, int Sessionno, int Degreeno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Edit Course
                        objParams = new SqlParameter[9];

                        objParams[0] = new SqlParameter("@P_SEMESTERNOs", sem);
                        objParams[1] = new SqlParameter("@P_COURSENOS", offcourse);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", SchemeNo);
                        objParams[3] = new SqlParameter("@P_SEQNOS", sqno);
                        objParams[4] = new SqlParameter("@P_UANO", ua_no);
                        objParams[5] = new SqlParameter("@P_IPADDDRESS", ipAdd);
                        objParams[6] = new SqlParameter("@P_SESSIONNO", Sessionno);//Added By Dileep Kare on 18/01/2021.
                        objParams[7] = new SqlParameter("@P_DEGREENO", Degreeno);//Added By Dileep Kare on 18/01/2021.
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSE_SP_UPD_OFFERED_COURSE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }
                public int UpdateOfferedCourseSemesterwise(int SchemeNo, int courseno, string sem, string sqno, int ua_no, string ipAdd, int Sessionno, int Degreeno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Edit Course
                        objParams = new SqlParameter[9];

                        objParams[0] = new SqlParameter("@P_SEMESTERNOS", sem);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", SchemeNo);
                        objParams[3] = new SqlParameter("@P_SEQNOS", sqno);
                        objParams[4] = new SqlParameter("@P_UANO", ua_no);
                        objParams[5] = new SqlParameter("@P_IPADDDRESS", ipAdd);
                        objParams[6] = new SqlParameter("@P_SESSIONNO", Sessionno);//Added By Dileep Kare on 18/01/2021.
                        objParams[7] = new SqlParameter("@P_DEGREENO", Degreeno);//Added By Dileep Kare on 18/01/2021.
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSE_SP_UPD_OFFERED_COURSE_SEMESTERWISE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }



                public int UpdateUnOfferedCourseSemesterwise(int SchemeNo, int courseno, string sem, int ua_no, string ipAdd, int Sessionno, int Degreeno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Edit Course
                        objParams = new SqlParameter[8];

                        objParams[0] = new SqlParameter("@P_SEMESTERNOS", sem);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", SchemeNo);
                        objParams[3] = new SqlParameter("@P_UANO", ua_no);
                        objParams[4] = new SqlParameter("@P_IPADDDRESS", ipAdd);
                        objParams[5] = new SqlParameter("@P_SESSIONNO", Sessionno);//Added By Dileep Kare on 18/01/2021.
                        objParams[6] = new SqlParameter("@P_DEGREENO", Degreeno);//Added By Dileep Kare on 18/01/2021.
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSE_SP_UPD_UNOFFERED_COURSE_SEMESTERWISE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }


                public int UpdateOfferedCourseTermScheme(int SessionNo, int SchemeNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Edit Course
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", SchemeNo);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSE_SP_INS_OFFERED_COURSE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourseTermScheme -> " + ex.ToString());
                    }

                    return retStatus;
                }
                public String GetCourseByCourseno(string coursenos, int schemeno)
                {
                    String mCCode = string.Empty;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_COURSENO", coursenos);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);

                        mCCode = objSQLHelper.ExecuteScalarSP("PKG_COURSE_SP_RET_COURSE_BY_COURSENO", objParams).ToString();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseByCourseno-> " + ex.ToString());
                    }
                    return mCCode;
                }

                public DataSet GetDeptWiseCourse(int deptno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DEPTNO", deptno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_RPT_OFFERED_COURSES", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetDeptWiseCourse-> " + ex.ToString());
                    }
                    return ds;
                }

                public String GetCCodeByCourseno(int courseno)
                {
                    String ccode = string.Empty;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];

                        objParams[0] = new SqlParameter("@P_COURSENO", courseno);

                        ccode = objSQLHelper.ExecuteScalarSP("PKG_COURSE_SP_RET_CCODE_BY_COURSENO", objParams).ToString();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCCodeByCourseno-> " + ex.ToString());
                    }
                    return ccode;
                }

                public int AddAuditCourse(int idno, int schemeno, string regno, int courseno, int credit, string ccode, string term, bool detained)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New Audit Course
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_REGNO", regno);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[3] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[4] = new SqlParameter("@P_CCODE", ccode);
                        objParams[5] = new SqlParameter("@P_CREDIT", credit);
                        objParams[6] = new SqlParameter("@P_TERM", term);
                        objParams[7] = new SqlParameter("@P_DETAINED", detained);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSE_SP_INS_AUDITCOURSE_REG", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AddAuditCourse-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetFirstYearCoursesBySchemeNo(int schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_PREREGIST_SP_RET_COURSEOFFERED_FY", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetFirstYearCoursesBySchemeNo-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetCourseForCourseAllotment(int schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_RET_COURSE_FOR_CA", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseForCourseAllotment-> " + ex.ToString());
                    }

                    return ds;
                }

                public SqlDataReader GetCourseAllot(int courseNo)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COURSENO", courseNo);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_COURSE_SP_RET_COURSE_TEACHER_ALLOT_BY_COURSENO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseAllot-> " + ex.ToString());
                    }
                    return dr;
                }

                //Method Added for course level master on 05/03/2010
                public DataSet GetCoursesByLevel(int levelno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_LEVELNO", levelno),
                        };

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_COURSE_BY_LEVEL", sqlParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCoursesByLevel-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataTableReader GetCourseByCourseNo(int courseno)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_COURSENO", courseno),
                        };

                        DataSet ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_RET_COURSE_BY_COURSENO", sqlParams);
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                            dtr = ds.Tables[0].CreateDataReader();

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseByCCode-> " + ex.ToString());
                    }

                    return dtr;
                }

                public DataSet GetLevelName()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];

                        ds = objSH.ExecuteDataSetSP("PKG_ACAD_GET_LEVEL_NAME", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseControlle.GetLevelName-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddLevel(string leveldsc, int admbatch, int nocourse, int cpth, int cppr, int thmarks, int prmarks, string collegeCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            
                            new SqlParameter("@P_LEVEL_DESC",leveldsc ),
                            new SqlParameter("@P_ADMBATCH", admbatch),
                            new SqlParameter("@P_NO_COURSES", nocourse),
                            new SqlParameter("@P_CP_TH",cpth),
                            new SqlParameter("@P_CP_PR", cppr),
                            new SqlParameter("@P_MARKS_TH",thmarks),
                            new SqlParameter("@P_MARKS_PR",prmarks),
                            new SqlParameter ("@P_COLLEGE_CODE",collegeCode),
                            new SqlParameter("@P_LEVELNO", SqlDbType.Int),
                        };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("[PKG_ACAD_INS_LEVEL]", sqlParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == -1001)
                            retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AddLevel-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateLevel(int levelno, string leveldsc, int admbatch, int nocourse, int cpth, int cppr, int thmarks, int prmarks)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_LEVELNO", levelno);
                        objParams[1] = new SqlParameter("@P_LEVEL_DESC", leveldsc);
                        objParams[2] = new SqlParameter("@P_ADMBATCH", admbatch);
                        objParams[3] = new SqlParameter("@P_NO_COURSES", nocourse);
                        objParams[4] = new SqlParameter("@P_CP_TH", cpth);
                        objParams[5] = new SqlParameter("@P_CP_PR", cppr);
                        objParams[6] = new SqlParameter("@P_MARKS_TH", thmarks);
                        objParams[7] = new SqlParameter("@P_MARKS_PR", prmarks);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPD_LEVEL", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.UpdateLevel-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public SqlDataReader GetLevelNo(int levelno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_LEVELNO", levelno);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_ACAD_GET_LEVEL_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetLevelNo-> " + ex.ToString());
                    }
                    return dr;
                }
                public int UpdateExamMarks(Course objCourse)
                {
                    int retun_status = Convert.ToInt16(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[13];

                        objParams[0] = new SqlParameter("@P_S1MAX", objCourse.S1Max);
                        objParams[1] = new SqlParameter("@P_S2MAX", objCourse.S2Max);
                        objParams[2] = new SqlParameter("@P_S3MAX", objCourse.S3Max);
                        objParams[3] = new SqlParameter("@P_S4MAX", objCourse.S4Max);
                        objParams[4] = new SqlParameter("@P_S5MAX", objCourse.S5Max);
                        objParams[5] = new SqlParameter("@P_EXTERMARKMAX", objCourse.ExtermarkMax);

                        //objParams[5] = new SqlParameter("@P_S6MAX", objCourse.S6Max);
                        //objParams[6] = new SqlParameter("@P_S7MAX", objCourse.S7Max);
                        //objParams[7] = new SqlParameter("@P_S8MAX", objCourse.S8Max);
                        //objParams[8] = new SqlParameter("@P_S9MAX", objCourse.S9Max);
                        //objParams[9] = new SqlParameter("@P_S10MAX", objCourse.S10Max);

                        objParams[6] = new SqlParameter("@P_S1Min", objCourse.S1Min);
                        objParams[7] = new SqlParameter("@P_S2Min", objCourse.S2Min);
                        objParams[8] = new SqlParameter("@P_S3Min", objCourse.S3Min);
                        objParams[9] = new SqlParameter("@P_S4Min", objCourse.S4Min);
                        objParams[10] = new SqlParameter("@P_S5Min", objCourse.S5Min);
                        objParams[11] = new SqlParameter("@P_EXTERMARKMIN", objCourse.ExtermarkMin);

                        //objParams[15] = new SqlParameter("@P_S6Min", objCourse.S6Min);
                        //objParams[16] = new SqlParameter("@P_S7Min", objCourse.S7Min);
                        //objParams[17] = new SqlParameter("@P_S8Min", objCourse.S8Min);
                        //objParams[18] = new SqlParameter("@P_S9Min", objCourse.S9Min);
                        //objParams[19] = new SqlParameter("@P_S10Min", objCourse.S10Min);
                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPDATE_EXAMMARKS", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retun_status = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == -1001)
                            retun_status = Convert.ToInt32(CustomStatus.DuplicateRecord);
                        else
                            retun_status = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retun_status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.UpdateExamMarks-> " + ex.ToString());
                    }

                    return retun_status;
                }

                /// <summary>
                /// This method is used to get courses marks
                /// </summary>
                /// <param name="schemeno">Get courses marks as per courseno.</param>
                /// <returns>DataSet</returns>
                public DataSet GetCoursesMarks(int courseno, int PatternNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_COURSENO", courseno);

                        objParams[1] = new SqlParameter("@P_PATTERNNO", PatternNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_EXAMNAME_COURSEMARKS", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCoursesMarks-> " + ex.ToString());
                    }

                    return ds;
                }




                //Added on 09/02/2019 for export the student in excel

                public DataSet Get_COURSE_REGISTRATION_DATA_COURSE_CODE_WISE(int sessionNo, int semesterno, string ccode)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                {                     
                    new SqlParameter("@P_SESSIONNO", sessionNo),
                    new SqlParameter("@P_SEMESTERNO", semesterno),
                   new SqlParameter("@P_CCODE", ccode),
                   
                };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_COURSECODEWISE_STUDENT_COUNT_FOR_REPORT", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.Get_STUDENT_FOR_FEE_PAYMENT() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }



                ////Bulk Lock unlock-testmark
                //public DataSet GetCoursesForLockUnlock(int CollegeID, int SessionNo, int degreeno, int branchno, int schemeno, int semesterno, int studenttype, int Int_Ext)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = new SqlParameter[8];
                //        objParams[0] = new SqlParameter("@P_COLLEGE_ID", CollegeID);
                //        objParams[1] = new SqlParameter("@P_SESSIONNO", SessionNo);
                //        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                //        objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                //        objParams[4] = new SqlParameter("@P_SCHEMENO", schemeno);
                //        objParams[5] = new SqlParameter("@P_SEMESTERNO", semesterno);
                //        objParams[6] = new SqlParameter("@P_PREV_STATUS", studenttype);
                //        objParams[7] = new SqlParameter("@P_EXAMNO", Int_Ext);
                //        //objParams[3] = new SqlParameter("@P_OP_ID", OP_ID);

                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_COURSES_BY_SCHEME_AND_OPERATOR", objParams);

                //    }
                //    catch (Exception ex)
                //    {
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GetCoursesForLockUnlock-> " + ex.ToString());
                //    }

                //    return ds;
                //}

                //Bulk Lock unlock-testmark
                public DataSet GetCoursesForLockUnlock(int CollegeID, int SessionNo, int degreeno, int branchno, int schemeno, int semesterno, int studenttype, int Int_Ext, int subid, string subexam)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", CollegeID);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[4] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[5] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[6] = new SqlParameter("@P_PREV_STATUS", studenttype);
                        objParams[7] = new SqlParameter("@P_EXAMNO", Int_Ext);
                        objParams[8] = new SqlParameter("@P_SUBID", subid);    // added on 12-02-2020 by Vaishali
                        objParams[9] = new SqlParameter("@P_SUBEXAMTYPE", subexam);    // added on 14-02-2020 by Vaishali
                        //objParams[3] = new SqlParameter("@P_OP_ID", OP_ID);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_COURSES_BY_SCHEME_AND_OPERATOR", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GetCoursesForLockUnlock-> " + ex.ToString());
                    }

                    return ds;
                }


                //ADD COURSE FOR THE PHD 

                public int AddNewPhdCourse(Course objCourse)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New Course
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_COURSE_NAME", objCourse.CourseName);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", objCourse.SchemeNo);
                        objParams[2] = new SqlParameter("@P_CCODE", objCourse.CCode);
                        objParams[3] = new SqlParameter("@P_SUBID", objCourse.SubID);
                        objParams[4] = new SqlParameter("@P_CREDITS", objCourse.Credits);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objCourse.CollegeCode);
                        //objParams[42] = new SqlParameter("@P_SEMESTERNO", objCourse.SemesterNo);
                        objParams[6] = new SqlParameter("@P_COURSE_NO", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PHD_COURSE_SP_INS_ADD_NEW_COURSE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AddCourse-> " + ex.ToString());
                    }

                    return retStatus;
                }

                #region Paper Setter

                /// <summary>
                /// This method is used to update the quantity of paper set & its reorder level
                /// </summary>
                /// <param name="ccode">ccode</param>
                /// <param name="bos_deptno">Board of studies Department</param>
                /// <param name="qty">qty of paper set</param>
                /// <param name="req">req</param>
                /// <returns>int</returns>
                public int UpdateCourseQTY(string ccode, int bos_deptno, int semesterno, int qty, int req)
                {
                    int retun_status = Convert.ToInt16(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_CCODE", ccode);
                        objParams[1] = new SqlParameter("@P_BOS_DEPTNO", bos_deptno);
                        objParams[2] = new SqlParameter("@P_SEMSTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_QTY", qty);
                        objParams[4] = new SqlParameter("@P_REORDER", req);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_COURSE_QTY_REQ", objParams, true);
                        retun_status = Convert.ToInt16(ret);

                    }
                    catch (Exception ex)
                    {
                        retun_status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.UpdateCourseQTY-> " + ex.ToString());
                    }

                    return retun_status;
                }

                /// <summary>
                /// This method is used to apply for new paper set
                /// </summary>
                /// <param name="sessionno">sessionno</param>
                /// <param name="bos_deptno">Board of studies Department</param>
                /// <param name="semesterno">semesterno</param>
                /// <param name="ccode">ccode</param>
                /// <returns>int</returns>
                //public int UpdateCourseBal(int sessionno, int bos_deptno, int semesterno, string ccode)
                //{
                //    int retun_status = Convert.ToInt16(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = new SqlParameter[4];

                //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                //        objParams[1] = new SqlParameter("@P_BOS_DEPTNO", bos_deptno);
                //        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                //        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);

                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PAPER_SET_DETAILS_INSERT", objParams, false) != null)
                //            retun_status = Convert.ToInt16(CustomStatus.RecordSaved);

                //    }
                //    catch (Exception ex)
                //    {
                //        retun_status = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.UpdateCourseBal-> " + ex.ToString());
                //    }

                //    return retun_status;
                //}

                // added by shubham On 20/02/23
                public int UpdateCourseBal(int sessionId, int bos_deptno, int semesterno, string ccode, int collegeid)
                {
                    int retun_status = Convert.ToInt16(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_SESSIONID", sessionId);
                        objParams[1] = new SqlParameter("@P_BOS_DEPTNO", bos_deptno);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_COLLEGEID", collegeid);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PAPER_SET_DETAILS_INSERT", objParams, false) != null)
                            retun_status = Convert.ToInt16(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retun_status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.UpdateCourseBal-> " + ex.ToString());
                    }

                    return retun_status;
                }


                /// <summary>
                /// This method is used to delete the request
                /// </summary>
                /// <param name="sessionno">sessionno</param>
                /// <param name="bos_deptno">Board of studies Department</param>
                /// <param name="semesterno">semesterno</param>
                /// <param name="ccode">ccode</param>
                /// <returns>int</returns>
                //public int DeleteCourseBal(int sessionno, int bos_deptno, int semesterno, string ccode)
                //{
                //    int retun_status = Convert.ToInt16(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = new SqlParameter[4];

                //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                //        objParams[1] = new SqlParameter("@P_BOS_DEPTNO", bos_deptno);
                //        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                //        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PAPER_SET_DETAILS_DELETE", objParams, true);
                //        retun_status = Convert.ToInt16(ret);

                //    }
                //    catch (Exception ex)
                //    {
                //        retun_status = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.DeleteCourseBal-> " + ex.ToString());
                //    }

                //    return retun_status;
                //}

                // added by shubham on 21/02/2023
                public int DeleteCourseBal(int SessionId, int bos_deptno, int semesterno, string ccode, int CollegeId)
                {
                    int retun_status = Convert.ToInt16(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_SESSIONID", SessionId);
                        objParams[1] = new SqlParameter("@P_BOS_DEPTNO", bos_deptno);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_COLLEGEID", CollegeId);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PAPER_SET_DETAILS_DELETE", objParams, true);
                        retun_status = Convert.ToInt16(ret);

                    }
                    catch (Exception ex)
                    {
                        retun_status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.DeleteCourseBal-> " + ex.ToString());
                    }

                    return retun_status;
                }


                /// <summary>
                /// This method is used to add the paper set faculty by BOS
                /// </summary>
                /// <param name="sessionno">sessionno</param>
                /// <param name="bos_deptno">Board of studies Department</param>
                /// <param name="semesterno">semesterno</param>
                /// <param name="ccode">ccode</param>
                /// <param name="faculty1">faculty1</param>
                /// <param name="faculty2">faculty2</param>
                /// <param name="bos_lock">Lock status set by BOS(one time)</param>
                /// <returns>int</returns>
                /// 
                //public int AddPaperSetFaculty(int sessionno, int bos_deptno, int semesterno, string ccode, int faculty1, int qt1, int moi1, int faculty2, int qt2, int moi2, int faculty3, int qt3, int moi3, bool bos_lock)
                //{
                //    int retun_status = Convert.ToInt16(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = new SqlParameter[14];

                //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                //        objParams[1] = new SqlParameter("@P_BOS_DEPTNO", bos_deptno);
                //        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                //        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                //        objParams[4] = new SqlParameter("@P_FACULTY1", faculty1);
                //        objParams[5] = new SqlParameter("@P_QT1", qt1);
                //        objParams[6] = new SqlParameter("@P_MOI1", moi1);
                //        objParams[7] = new SqlParameter("@P_FACULTY2", faculty2);
                //        objParams[8] = new SqlParameter("@P_QT2", qt2);
                //        objParams[9] = new SqlParameter("@P_MOI2", moi2);
                //        objParams[10] = new SqlParameter("@P_FACULTY3", faculty3);
                //        objParams[11] = new SqlParameter("@P_QT3", qt3);
                //        objParams[12] = new SqlParameter("@P_MOI3", moi3);
                //        objParams[13] = new SqlParameter("@P_BOS_LOCK", bos_lock);

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_ADD_PAPERSET_FACULTY", objParams, true);
                //        retun_status = Convert.ToInt16(ret);

                //    }
                //    catch (Exception ex)
                //    {
                //        retun_status = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AddPaperSetFaculty-> " + ex.ToString());
                //    }

                //    return retun_status;
                //}

                //added by shubham on 21/02/2023
                public int AddPaperSetFaculty(int sessionId, int bos_deptno, int semesterno, string ccode, int faculty1, int qt1, int moi1, int faculty2, int qt2, int moi2, int faculty3, int qt3, int moi3, bool bos_lock, int collegeId)
                {
                    int retun_status = Convert.ToInt16(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[15];

                        objParams[0] = new SqlParameter("@P_SESSIONID", sessionId);
                        objParams[1] = new SqlParameter("@P_BOS_DEPTNO", bos_deptno);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_FACULTY1", faculty1);
                        objParams[5] = new SqlParameter("@P_QT1", qt1);
                        objParams[6] = new SqlParameter("@P_MOI1", moi1);
                        objParams[7] = new SqlParameter("@P_FACULTY2", faculty2);
                        objParams[8] = new SqlParameter("@P_QT2", qt2);
                        objParams[9] = new SqlParameter("@P_MOI2", moi2);
                        objParams[10] = new SqlParameter("@P_FACULTY3", faculty3);
                        objParams[11] = new SqlParameter("@P_QT3", qt3);
                        objParams[12] = new SqlParameter("@P_MOI3", moi3);
                        objParams[13] = new SqlParameter("@P_COLLEGEID", collegeId);
                        objParams[14] = new SqlParameter("@P_BOS_LOCK", bos_lock);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_ADD_PAPERSET_FACULTY", objParams, true);
                        retun_status = Convert.ToInt16(ret);

                    }
                    catch (Exception ex)
                    {
                        retun_status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AddPaperSetFaculty-> " + ex.ToString());
                    }

                    return retun_status;
                }


                /// <summary>
                /// This method is used to accept/change the paper set faculty by DEAN
                /// </summary>
                /// <param name="sessionno">sessionno</param>
                /// <param name="bos_deptno">Board of studies Department</param>
                /// <param name="semesterno">semesterno</param>
                /// <param name="ccode">ccode</param>
                /// <param name="faculty1">faculty1</param>
                /// <param name="faculty2">faculty2</param>
                /// <param name="accept">accept set by DEAN false if not accepted</param>
                /// <param name="bos_lock">Lock status set by DEAN(one time)</param>
                /// <returns>int</returns>
                /// <summary>

                //public int AcceptPaperSetFaculty(int sessionno, int bos_deptno, int semesterno, string ccode, int fac_num, int accept, int qty, int moi, bool dean_lock)
                //{
                //    int retun_status = Convert.ToInt16(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = new SqlParameter[9];

                //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                //        objParams[1] = new SqlParameter("@P_BOS_DEPTNO", bos_deptno);
                //        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                //        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                //        objParams[4] = new SqlParameter("@P_FACULTY_NUM", fac_num);
                //        objParams[5] = new SqlParameter("@P_ACCEPT", accept);
                //        objParams[6] = new SqlParameter("@P_QTY", qty);
                //        objParams[7] = new SqlParameter("@P_MOI", moi);
                //        objParams[8] = new SqlParameter("@P_DEAN_LOCK", dean_lock);

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_ACCEPT_PAPERSET_FACULTY", objParams, true);
                //        retun_status = Convert.ToInt16(ret);

                //    }
                //    catch (Exception ex)
                //    {
                //        retun_status = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AcceptPaperSetFaculty-> " + ex.ToString());
                //    }

                //    return retun_status;
                //}

                //ADDED BY SHUBHAM ON 20/02/2023
                public int AcceptPaperSetFaculty(int sessionId, int bos_deptno, int semesterno, string ccode, int fac_num, int accept, int qty, int moi, bool dean_lock, int collegeId)
                {
                    int retun_status = Convert.ToInt16(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[10];

                        objParams[0] = new SqlParameter("@P_SESSIONID", sessionId);
                        objParams[1] = new SqlParameter("@P_BOS_DEPTNO", bos_deptno);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_FACULTY_NUM", fac_num);
                        objParams[5] = new SqlParameter("@P_ACCEPT", accept);
                        objParams[6] = new SqlParameter("@P_QTY", qty);
                        objParams[7] = new SqlParameter("@P_MOI", moi);
                        objParams[8] = new SqlParameter("@P_COLLEGEID", collegeId);
                        objParams[9] = new SqlParameter("@P_DEAN_LOCK", dean_lock);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_ACCEPT_PAPERSET_FACULTY", objParams, true);
                        retun_status = Convert.ToInt16(ret);

                    }
                    catch (Exception ex)
                    {
                        retun_status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AcceptPaperSetFaculty-> " + ex.ToString());
                    }

                    return retun_status;
                }


                /// <summary>
                /// <summary>
                /// This method is used to accept the paper set faculty receive status
                /// </summary>
                /// <param name="sessionno">sessionno</param>
                /// <param name="bos_deptno">Board of studies Department</param>
                /// <param name="semesterno">semesterno</param>
                /// <param name="ccode">ccode</param>
                /// <param name="faculty">faculty no </param>
                /// <returns>int</returns>
                /// <summary>
                /// 
                //public int AddPaperSetReceivedStatus(int sessionno, int bos_deptno, int semesterno, int qt_rcvd, int moi_rcvd, int rcvd, string ccode, int faculty)
                //{
                //    int retun_status = Convert.ToInt16(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = new SqlParameter[8];

                //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                //        objParams[1] = new SqlParameter("@P_BOS_DEPTNO", bos_deptno);
                //        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                //        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                //        objParams[4] = new SqlParameter("@P_QTYRCVD", qt_rcvd);
                //        objParams[5] = new SqlParameter("@P_MOIRCVD", moi_rcvd);
                //        objParams[6] = new SqlParameter("@P_RCVD", rcvd);
                //        objParams[7] = new SqlParameter("@P_STAFFNO", faculty);

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PAPERSET_RECEIVE_STATUS", objParams, true);
                //        retun_status = Convert.ToInt16(ret);

                //    }
                //    catch (Exception ex)
                //    {
                //        retun_status = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AcceptPaperSetFaculty-> " + ex.ToString());
                //    }

                //    return retun_status;
                //}

                //ADDED BY SHUBHAM ON 20/02/2023
                public int AddPaperSetReceivedStatus(int sessionId, int bos_deptno, int semesterno, int qt_rcvd, int moi_rcvd, int rcvd, string ccode, int faculty, int CollegeId)
                {
                    int retun_status = Convert.ToInt16(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[9];

                        objParams[0] = new SqlParameter("@P_SESSIONID", sessionId);
                        objParams[1] = new SqlParameter("@P_BOS_DEPTNO", bos_deptno);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_QTYRCVD", qt_rcvd);
                        objParams[5] = new SqlParameter("@P_MOIRCVD", moi_rcvd);
                        objParams[6] = new SqlParameter("@P_RCVD", rcvd);
                        objParams[7] = new SqlParameter("@P_STAFFNO", faculty);
                        objParams[8] = new SqlParameter("@P_COLLEGEID", CollegeId);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PAPERSET_RECEIVE_STATUS", objParams, true);
                        retun_status = Convert.ToInt16(ret);

                    }
                    catch (Exception ex)
                    {
                        retun_status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AcceptPaperSetFaculty-> " + ex.ToString());
                    }

                    return retun_status;
                }

                /// <summary>
                /// This method is used to cancel the paper set entry and insert new
                /// </summary>
                /// <param name="sessionno">sessionno</param>
                /// <param name="bos_deptno">Board of studies Department</param>
                /// <param name="semesterno">semesterno</param>
                /// <param name="ccode">ccode</param>
                /// <param name="faculty">faculty no </param>
                /// <returns>int</returns>
                /// <summary>
                //public int CancelPaperSetEntry(int sessionno, int bos_deptno, int semesterno, string ccode, int faculty)
                //{
                //    int retun_status = Convert.ToInt16(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = new SqlParameter[5];

                //        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                //        objParams[1] = new SqlParameter("@P_BOS_DEPTNO", bos_deptno);
                //        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                //        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                //        objParams[4] = new SqlParameter("@P_STAFFNO", faculty);

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PAPERSET_CANCEL_STATUS", objParams, true);
                //        retun_status = Convert.ToInt16(ret);

                //    }
                //    catch (Exception ex)
                //    {
                //        retun_status = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.CancelPaperSetEntry-> " + ex.ToString());
                //    }

                //    return retun_status;
                //}

                // ADDED BY SHUBHAM ON 21/02/2023
                public int CancelPaperSetEntry(int sessionId, int bos_deptno, int semesterno, string ccode, int faculty, int CollegeId)
                {
                    int retun_status = Convert.ToInt16(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[6];

                        objParams[0] = new SqlParameter("@P_SESSIONID", sessionId);
                        objParams[1] = new SqlParameter("@P_BOS_DEPTNO", bos_deptno);
                        objParams[2] = new SqlParameter("@P_CCODE", ccode);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_STAFFNO", faculty);
                        objParams[5] = new SqlParameter("@P_COLLEGEID", CollegeId);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_PAPERSET_CANCEL_STATUS", objParams, true);
                        retun_status = Convert.ToInt16(ret);

                    }
                    catch (Exception ex)
                    {
                        retun_status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.CancelPaperSetEntry-> " + ex.ToString());
                    }

                    return retun_status;
                }
                #endregion

                // Get Scheme, Semester, Photo by user idno
                public SqlDataReader GetShemeSemesterByUser(int ua_idno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_IDNO", ua_idno);
                        dr = objSQLHelper.ExecuteReaderSP("PKG_GET_SCHEME_SEMESTER_BY_USERID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GetShemeSemester-> " + ex.ToString());
                    }
                    return dr;
                }
                //GET COURSE DETAILS OF FACULTY

                public DataSet GetCourseOfUanoDetails(int uano, int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_UANO", uano);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);


                        ds = objSQLHelper.ExecuteDataSetSP("GET_COURSES_OF_FACULTY", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseDetails-> " + ex.ToString());
                    }
                    return ds;
                }
                public int InsertEquivalenceCourses(int idno, int oldschemeno, int newschemeno, int oldcourseno, int newcourseno, string oldccode, string newccode, int sessionno, string ip, string colcode, int uano)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                            { 
                                new SqlParameter("@P_IDNO", idno),
                                new SqlParameter("@P_OLDSCHEMENO", oldschemeno),                               
                                new SqlParameter("@P_NEWSCHEMENO", newschemeno),
                                new SqlParameter("@P_OLDCOURSENO", oldcourseno),
                                new SqlParameter("@P_NEWCOURSENO", newcourseno),
                                new SqlParameter("@P_OLDCCODE", oldccode),
                                new SqlParameter("@P_NEWCCODE", newccode),
                                new SqlParameter("@P_SESSIONNO", sessionno),
                                new SqlParameter("@P_IPADDRESS", ip),
                                new SqlParameter("@P_COLLEGE_CODE", colcode),
                                new SqlParameter("@P_UA_NO", uano),
                                new SqlParameter("@P_OP", SqlDbType.Int) 
                            };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objDataAccess.ExecuteNonQuerySP("PKG_INS_EQUIVALENCE_COURSES", sqlParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (Convert.ToInt32(ret) == -5)
                            retStatus = Convert.ToInt32(CustomStatus.RecordNotFound); //old course reg record not found
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.CopyCourseToNewScheme-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// Added by Rita M.. on date 14/07/2017
                /// </summary>
                /// <param name="degreeno"></param>
                /// <param name="subid"></param>
                /// <returns></returns>
                public DataSet GetCourseForCourseAllotment_new(int degreeno, int subid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_DEPT", degreeno);
                        objParams[1] = new SqlParameter("@P_SUBID", subid);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_RET_COURSE_FOR_AR", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseForCourseAllotment-> " + ex.ToString());
                    }

                    return ds;
                }

                /// <summary>
                /// Added by S.patil - 23052020
                /// </summary>

                public int AllotCourseAndTimeTable(Student_Acd objStudent, AllotmentMaster objAM)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[28];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objStudent.SessionNo);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", objStudent.SchemeNo);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", objStudent.Sem);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", objStudent.Sectionno);
                        objParams[4] = new SqlParameter("@P_UA_NO", objStudent.UA_No);
                        objParams[5] = new SqlParameter("@P_ADTEACHER", objStudent.AdTeacher);
                        objParams[6] = new SqlParameter("@P_SUBID", objStudent.Pract_Theory);
                        objParams[7] = new SqlParameter("@P_COURSENO", objStudent.CourseNo);
                        objParams[8] = new SqlParameter("@P_TH_PR", objStudent.Th_Pr);
                        objParams[9] = new SqlParameter("@P_LECT_COUNT1", objAM.SLOT1);
                        objParams[10] = new SqlParameter("@P_LECT_COUNT2", objAM.SLOT2);
                        objParams[11] = new SqlParameter("@P_LECT_COUNT3", objAM.SLOT3);
                        objParams[12] = new SqlParameter("@P_LECT_COUNT4", objAM.SLOT4);
                        objParams[13] = new SqlParameter("@P_LECT_COUNT5", objAM.SLOT5);
                        objParams[14] = new SqlParameter("@P_LECT_COUNT6", objAM.SLOT6);
                        objParams[15] = new SqlParameter("@P_DAYNO1", objAM.DAY1);
                        objParams[16] = new SqlParameter("@P_DAYNO2", objAM.DAY2);
                        objParams[17] = new SqlParameter("@P_DAYNO3", objAM.DAY3);
                        objParams[18] = new SqlParameter("@P_DAYNO4", objAM.DAY4);
                        objParams[19] = new SqlParameter("@P_DAYNO5", objAM.DAY5);
                        objParams[20] = new SqlParameter("@P_DAYNO6", objAM.DAY6);

                        objParams[21] = new SqlParameter("@P_ROOM1", objAM.ROOM1);
                        objParams[22] = new SqlParameter("@P_ROOM2", objAM.ROOM2);
                        objParams[23] = new SqlParameter("@P_ROOM3", objAM.ROOM3);
                        objParams[24] = new SqlParameter("@P_ROOM4", objAM.ROOM4);
                        objParams[25] = new SqlParameter("@P_ROOM5", objAM.ROOM5);
                        objParams[26] = new SqlParameter("@P_ROOM6", objAM.ROOM6);

                        objParams[27] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[27].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_COURSE_TEACHER_ALLOTMENT_DEMO", objParams, true);
                        if (ret != null)
                            retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AllotCourseAndTimeTable-> " + ex.ToString());
                    }
                    return retStatus;
                }
                /// <summary>
                /// Added by S.Patil - 25052020
                /// </summary>
                /// <param name="session"></param>
                /// <param name="scheme"></param>
                /// <param name="semesterno"></param>
                /// <param name="Degreeno"></param>
                /// <param name="Branchno"></param>
                /// <param name="sectionno"></param>
                /// <returns></returns>
                public DataSet GetCourseAllotmentSectionwiseAuto(int session, int scheme, int semesterno, int Degreeno, int Branchno, int sectionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", scheme);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_DEGREENO", Degreeno);
                        objParams[4] = new SqlParameter("@P_BRANCHNO", Branchno);
                        objParams[5] = new SqlParameter("@P_SECTIONNO", sectionno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_RET_COURSE_ALLOT_SECTIONWISE_AUTO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCourseAllotment-> " + ex.ToString());
                    }
                    return ds;
                }
                /// <summary>
                /// Added by S.Patil - 25052020
                /// </summary>
                /// <param name="objStudent"></param>
                /// <returns></returns>
                public int DeleteCourseAllot_Auto(Student_Acd objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Edit Course
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", objStudent.SessionNo);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", objStudent.SchemeNo);
                        objParams[2] = new SqlParameter("@P_UA_NO", objStudent.UA_No);
                        objParams[3] = new SqlParameter("@P_COURSENO", objStudent.CourseNo);
                        objParams[4] = new SqlParameter("@P_SUBID", objStudent.Th_Pr);
                        objParams[5] = new SqlParameter("@P_TH_PR", objStudent.Th_Pr);
                        objParams[6] = new SqlParameter("@P_SECTIONNO", objStudent.Sectionno);
                        objParams[7] = new SqlParameter("@P_UA_NO_DELETE", objStudent.Ua_no_delete);
                        objParams[8] = new SqlParameter("@P_INTAKE", objStudent.Intake);
                        objParams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSE_SP_DEL_COURSEALLOT_AUTO", objParams, true);


                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.DeleteCourseAllot -> " + ex.ToString());
                    }

                    return retStatus;
                }

                /// <summary>
                /// Added by Swapnil P - 25022021
                /// </summary>
                /// <param name="session"></param>
                /// <returns></returns>
                public DataSet GetSessionwiseRegistrationDetails(int session)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_COURSE_REGISTRATION_DETAIL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetSessionwiseRegistrationDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by Swapnil P - 25022021
                /// </summary>
                /// <param name="session"></param>
                /// <returns></returns>
                public DataSet GetSessionwiseRegistrationCount(int session)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_COURSE_REGISTRATION_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetSessionwiseRegistrationDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by Dileep Kare - 11032021
                /// </summary>
                /// <param name="session"></param>
                /// <returns></returns>
                public DataSet GetSessionwiseExamRegistrationDetails(int session)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_SESSION_WISE_EXAM_REGISTRATION_DETAIL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetSessionwiseExamRegistrationDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by Dileep Kare - 11032021
                /// </summary>
                /// <param name="session"></param>
                /// <returns></returns>
                public DataSet GetSessionwiseExamRegistrationCount(int session)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SESSION_WISE_EXAM_REGISTRATION_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetSessionwiseExamRegistrationCount-> " + ex.ToString());
                    }
                    return ds;
                }

                //Added by Nikhil Lambe on 26/04/2021 to get students list for cancel single course registration 
                public DataSet GetStudentsList_For_Cancel_Course_Reg(int SessionNo, int SchemeNo, int SemesterNo, int Subject)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", SchemeNo);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", SemesterNo);
                        objParams[3] = new SqlParameter("@P_SUBJECT", Subject);
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_SP_GET_STUDENTS_LIST_CANCEL_COURSE_REG", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetStudentsList_For_Cancel_Course_Reg --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                //Added by Nikhil Lambe on 26/04/2021 to get students list for cancel the subjects by idno
                public DataSet GetStudentsListToCancelCourse_ByIdno(int SessionNo, int SemesterNo, int Idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", SemesterNo);
                        objParams[2] = new SqlParameter("@P_IDNO", Idno);
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_SP_GET_SUBJECTS_LIST_TO_CANCEL_COURSE_BY_IDNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetStudentsListToCancelCourse_ByIdno --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
                //Added by Nikhil Lambe on 26/04/2021 to update students subject cancel
                public int UpdateStudents_Cancel_Course(int SessionNo, int SchemeNo, int SemesterNo, int Subject, int Idno, int Uano, string Ipaddress, string Remark)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", SchemeNo);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", SemesterNo);
                        objParams[3] = new SqlParameter("@P_SUBJECT", Subject);
                        objParams[4] = new SqlParameter("@P_IDNO", Idno);
                        objParams[5] = new SqlParameter("@P_UANO", Uano);
                        objParams[6] = new SqlParameter("@P_IPADDRESS", Ipaddress);
                        objParams[7] = new SqlParameter("@P_REMARK", Remark);
                        objParams[8] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;
                        object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_CANCEL_STUDENTS_COURSE", objParams, true);
                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.UpdateStudents_Cancel_Course --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                /// <summary>
                /// Added by Swapnil P - 25022021
                /// </summary>
                /// <param name="session"></param>
                /// <returns></returns>
                public DataSet GetSessionwiseActivityStatus(int session)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SESSIONWISE_ACTIVITY_STATUS_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetSessionwiseRegistrationDetails-> " + ex.ToString());
                    }
                    return ds;
                }


                //Added by Deepali on 20/05/2021 for MOOCS courses
                public DataSet GetCourseForMoocs(int schemeno, int sessionno, int OrgID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_ORGID", OrgID);//Added by Dileep Kare on 21.03.2022
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_RET_COURSE_FOR_MOOCS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetCourseForMoocs-> " + ex.ToString());
                    }
                    return ds;
                }

                public int EnterMoocsCourse(int sessionno, string offcourse, int schemeno, string semesterno, int uano, string ip_address, int OrgId)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Edit Course
                        objParams = new SqlParameter[8];

                        //objParams[0] = new SqlParameter("@P_SEMESTERNOs", sem);
                        objParams[0] = new SqlParameter("@P_COURSENO", offcourse);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[3] = new SqlParameter("@P_SEMESTERNOS", semesterno);
                        objParams[4] = new SqlParameter("@P_UANO", uano);
                        objParams[5] = new SqlParameter("@P_IP_ADDRESS", ip_address);
                        objParams[6] = new SqlParameter("@P_ORGID", OrgId);//Added by Dileep Kare on 21.03.2022
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_COURSE_SP_INS_MOOCS_COURSE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.EnterMoocsCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetMoocsCoursesData(int sessionNo, int schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_REPORT_MOOCS_COURSES", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GetMoocsCoursesData-> " + ex.ToString());
                    }

                    return ds;
                }


                /// <summary>
                /// Added by S.Prachand - 06082021
                /// </summary>
                /// <param name="session"></param>
                /// <param name="scheme"></param>
                /// <param name="semesterno"></param>
                /// <param name="Degreeno"></param>
                /// <param name="Branchno"></param>
                /// <param name="sectionno"></param>
                /// <returns></returns>
                public DataSet GetStudentFilledExamForm(int session, int scheme, int semesterno, int Degreeno, int Branchno, int examtype, int college_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", scheme);
                        objParams[2] = new SqlParameter("@P_SEMESTER_NO", semesterno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[4] = new SqlParameter("@P_EXAMTYPE", examtype);
                        objParams[5] = new SqlParameter("@P_DEGREENO", Degreeno);
                        objParams[6] = new SqlParameter("@P_BRANCHNO", Branchno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_EXAM_REGISTRATION_LIST_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetStudentFilledExamForm-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by S.Prachand - 06082021
                /// </summary>
                /// <param name="session"></param>
                /// <param name="scheme"></param>
                /// <param name="semesterno"></param>
                /// <param name="Degreeno"></param>
                /// <param name="Branchno"></param>
                /// <param name="sectionno"></param>
                /// <returns></returns>
                public DataSet GetStudentNotFilledExamForm(int session, int scheme, int semesterno, int Degreeno, int Branchno, int examtype, int college_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", scheme);
                        objParams[2] = new SqlParameter("@P_SEMESTER_NO", semesterno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[4] = new SqlParameter("@P_EXAMTYPE", examtype);
                        objParams[5] = new SqlParameter("@P_DEGREENO", Degreeno);
                        objParams[6] = new SqlParameter("@P_BRANCHNO", Branchno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EXAM_NOTREGISTRATION_LIST_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetStudentNotFilledExamForm-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by S.Prachand - 06082021
                /// </summary>
                /// <param name="session"></param>
                /// <param name="scheme"></param>
                /// <param name="semesterno"></param>
                /// <param name="Degreeno"></param>
                /// <param name="Branchno"></param>
                /// <param name="sectionno"></param>
                /// <returns></returns>
                public DataSet GetStudentPaymentConfirm(int session, int scheme, int semesterno, int Degreeno, int Branchno, int examtype, int college_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", scheme);
                        objParams[2] = new SqlParameter("@P_SEMESTER_NO", semesterno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[4] = new SqlParameter("@P_EXAMTYPE", examtype);
                        objParams[5] = new SqlParameter("@P_DEGREENO", Degreeno);
                        objParams[6] = new SqlParameter("@P_BRANCHNO", Branchno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_CONFIRMED_EXAM_REGISTRATION_LIST_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetStudentNotFilledExamForm-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by S.Prachand - 06082021
                /// </summary>
                /// <param name="session"></param>
                /// <param name="scheme"></param>
                /// <param name="semesterno"></param>
                /// <param name="Degreeno"></param>
                /// <param name="Branchno"></param>
                /// <param name="sectionno"></param>
                /// <returns></returns>
                public DataSet GetStudentPaymentNotConfirm(int session, int scheme, int semesterno, int Degreeno, int Branchno, int examtype, int college_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", scheme);
                        objParams[2] = new SqlParameter("@P_SEMESTER_NO", semesterno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[4] = new SqlParameter("@P_EXAMTYPE", examtype);
                        objParams[5] = new SqlParameter("@P_DEGREENO", Degreeno);
                        objParams[6] = new SqlParameter("@P_BRANCHNO", Branchno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_CONFIRMED_EXAM_REGISTRATION_LIST_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetStudentNotFilledExamForm-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by S.Prachand - 06082021
                /// </summary>
                /// <param name="session"></param>
                /// <param name="scheme"></param>
                /// <param name="semesterno"></param>
                /// <param name="Degreeno"></param>
                /// <param name="Branchno"></param>
                /// <param name="sectionno"></param>
                /// <returns></returns>
                public DataSet GetStudentExamRegistrationSummary(int session, int scheme, int semesterno, int Degreeno, int Branchno, int examtype, int college_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", scheme);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[4] = new SqlParameter("@P_EXAMTYPE", examtype);
                        objParams[5] = new SqlParameter("@P_DEGREENO", Degreeno);
                        objParams[6] = new SqlParameter("@P_BRANCHNO", Branchno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_REG_BACKLG_STUD_CNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetStudentNotFilledExamForm-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by S.Prachand - 06082021
                /// </summary>
                /// <param name="session"></param>
                /// <param name="scheme"></param>
                /// <param name="semesterno"></param>
                /// <param name="Degreeno"></param>
                /// <param name="Branchno"></param>
                /// <param name="sectionno"></param>
                /// <returns></returns>
                public DataSet GetStudentExamRegistrationStatus(int session, int scheme, int semesterno, int Degreeno, int Branchno, int examtype, int college_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", scheme);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[4] = new SqlParameter("@P_EXAMTYPE", examtype);
                        objParams[5] = new SqlParameter("@P_DEGREENO", Degreeno);
                        objParams[6] = new SqlParameter("@P_BRANCHNO", Branchno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_REG_BACKLG_STUD_CNT_STATUS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetStudentNotFilledExamForm-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by S.Prachand - 06082021
                /// </summary>
                /// <param name="session"></param>
                /// <param name="scheme"></param>
                /// <param name="semesterno"></param>
                /// <param name="Degreeno"></param>
                /// <param name="Branchno"></param>
                /// <param name="sectionno"></param>
                /// <returns></returns>
                public DataSet GetStudentExamFormApprovedByDepartment(int session, int scheme, int semesterno, int Degreeno, int Branchno, int examtype, int college_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", scheme);
                        objParams[2] = new SqlParameter("@P_SEMESTER_NO", semesterno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[4] = new SqlParameter("@P_EXAMTYPE", examtype);
                        objParams[5] = new SqlParameter("@P_DEGREENO", Degreeno);
                        objParams[6] = new SqlParameter("@P_BRANCHNO", Branchno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_EXAM_APPROVED_BY_DEPT_LIST_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetStudentFilledExamForm-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by S.Prachand - 06082021
                /// </summary>
                /// <param name="session"></param>
                /// <param name="scheme"></param>
                /// <param name="semesterno"></param>
                /// <param name="Degreeno"></param>
                /// <param name="Branchno"></param>
                /// <param name="sectionno"></param>
                /// <returns></returns>
                public DataSet GetStudentExamFormNotApprovedByDepartment(int session, int scheme, int semesterno, int Degreeno, int Branchno, int examtype, int college_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", scheme);
                        objParams[2] = new SqlParameter("@P_SEMESTER_NO", semesterno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[4] = new SqlParameter("@P_EXAMTYPE", examtype);
                        objParams[5] = new SqlParameter("@P_DEGREENO", Degreeno);
                        objParams[6] = new SqlParameter("@P_BRANCHNO", Branchno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_EXAM_NOT_APPROVED_BY_DEPT_LIST_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetStudentFilledExamForm-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by S.Prachand - 06082021
                /// </summary>
                /// <param name="session"></param>
                /// <param name="scheme"></param>
                /// <param name="semesterno"></param>
                /// <param name="Degreeno"></param>
                /// <param name="Branchno"></param>
                /// <param name="sectionno"></param>
                /// <returns></returns>
                public DataSet GetExamDownloadAdmitCardDetails(int session, int scheme, int semesterno, int Degreeno, int Branchno, int examtype, int college_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", scheme);
                        objParams[2] = new SqlParameter("@P_SEMESTER_NO", semesterno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[4] = new SqlParameter("@P_EXAMTYPE", examtype);
                        objParams[5] = new SqlParameter("@P_DEGREENO", Degreeno);
                        objParams[6] = new SqlParameter("@P_BRANCHNO", Branchno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_DOWNLOAD_ADMIT_CARD_LIST_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetStudentFilledExamForm-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by S.Prachand - 06082021
                /// </summary>
                /// <param name="session"></param>
                /// <param name="scheme"></param>
                /// <param name="semesterno"></param>
                /// <param name="Degreeno"></param>
                /// <param name="Branchno"></param>
                /// <param name="sectionno"></param>
                /// <returns></returns>
                public DataSet GetExamNotDownloadAdmitCardDetails(int session, int scheme, int semesterno, int Degreeno, int Branchno, int examtype, int college_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", scheme);
                        objParams[2] = new SqlParameter("@P_SEMESTER_NO", semesterno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[4] = new SqlParameter("@P_EXAMTYPE", examtype);
                        objParams[5] = new SqlParameter("@P_DEGREENO", Degreeno);
                        objParams[6] = new SqlParameter("@P_BRANCHNO", Branchno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_NOT_DOWNLOAD_ADMIT_CARD_LIST_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetStudentFilledExamForm-> " + ex.ToString());
                    }
                    return ds;
                }
                /// <summary>
                /// Added By Rishabh on 23/12/2021 to get all Course Registration data.
                /// </summary>
                /// <param name="session"></param>
                /// <returns></returns>
                public DataSet GetAllCourseRegistrationData(string session, int mode)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESIONNO", session);
                        objParams[1] = new SqlParameter("@P_MODE", mode);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_ALL_COURSE_REGISTARTION_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllCourseRegistrationData-> " + ex.ToString());
                    }
                    return ds;
                }
                /// <summary>
                /// Added by Swapnil P - 17082021
                /// </summary>
                /// <param name="session"></param>
                /// <returns></returns>
                public DataSet GetSessionwiseCourseRegisteredStudentList(int session)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SESSIONWISE_COURSE_REGISTERED_STUDENT_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetSessionwiseCourseRegisteredStudentList-> " + ex.ToString());
                    }
                    return ds;
                }
                /// <summary>
                /// Added by S.Patil - 10 jan 2022
                /// </summary>
                /// <param name="scheme"></param>
                /// <param name="semesterno"></param>
                /// <returns></returns>
                public DataSet GetSubjectCourseList(int scheme, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", scheme);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_REPORT_SCHEMEWISE_COURSEMARKS_CHECKLIST_OFFERED", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllCourseRegistrationData-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetCollegeSchemeMappingDetails(int ColSchemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COLSCHEMENO", ColSchemeno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_COLLEGE_SCHEME_MAPPING_DETAILS", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Common.GetCollegeSchemeMappingDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsertSpecialisationData(Course objCe)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[9];

                        objParams[0] = new SqlParameter("@P_DEGREENO", objCe.degreeno);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", objCe.branchno);
                        objParams[2] = new SqlParameter("@P_SPECIALISATIONNO", objCe.specialisationno);
                        objParams[3] = new SqlParameter("@P_KNOWLEDGE_PARTNER_NO", objCe.Knowledge_partner);
                        objParams[4] = new SqlParameter("@P_INTAKE", objCe.intake);
                        objParams[5] = new SqlParameter("@P_ACTIVESTATUS", objCe.status);
                        objParams[6] = new SqlParameter("@P_ORGANIZATIONID", objCe.OrgId);
                        objParams[7] = new SqlParameter("@P_SPECIAL_MAP_NO", objCe.Special_Map_No);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SPECIALISATION_MASTER_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else if (Convert.ToInt32(ret) == 2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AddCourse-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public int UpdatespecialisationData(Course objCe)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Edit Course
                        objParams = new SqlParameter[7];

                        objParams[0] = new SqlParameter("@SPECIALISATIONNO", objCe.specialisationno);
                        objParams[1] = new SqlParameter("@DEGREENO", objCe.degreeno);
                        objParams[2] = new SqlParameter("@BRANCHNO", objCe.branchno);
                        objParams[3] = new SqlParameter("@SPECIALISATION_NAME", objCe.specialisation_name);
                        objParams[4] = new SqlParameter("@KNOWLEDGE_PARTNER_NO", objCe.Knowledge_partner);

                        objParams[5] = new SqlParameter("@INTAKE", objCe.intake);
                        objParams[6] = new SqlParameter("@ACTIVESTATUS", objCe.status);
                        //objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        //objParams[7].Direction = ParameterDirection.InputOutput;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SPECIALISATION_MASTER_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.UpdateCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }


                public DataSet GetSpecialisationList(Course objCe)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SELECT_SPECIALISATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllCourseRegistrationData-> " + ex.ToString());
                    }
                    return ds;
                }




                public DataSet EditSpecialisation(int specialisationno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SPECIAL_MAP_NO", specialisationno);
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_GET_SPECIALISATION_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EditConfiguration-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAllOfferedCourseList(int sessionno, string semesterno, int coschno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[2] = new SqlParameter("@P_COSCHNO", coschno);  //(semesterno,coschno) Added By Vipul Tichakulke on dated 05-02-2024 as per T-54216

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SESSIOWISE_ALL_OFFERED_COURSES", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllOfferedCourseList-> " + ex.ToString());
                    }
                    return ds;
                }

                // Added By Sneha G On 26/05/2021.
                public DataSet GetBacklogCourseOfferedSessionwise(int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_COURSE_FOR_BACKLOG_OFFERED", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetBacklogCourseOfferedSessionwise-> " + ex.ToString());
                    }
                    return ds;
                }

                public int OfferedBacklogCourses(int Sessionno, string Courseno, string Degreeno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New Registered Subject Details
                        objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENOS", Courseno);
                        objParams[2] = new SqlParameter("@P_DEGREENO", Degreeno);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_OFFERED_BACKLOG_COURSES", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.OfferedBacklogCourses-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// Updated by Diskha N. - 23/06/2022
                /// </summary>
                /// <param name="session"></param>
                /// <returns></returns>
                public DataSet AllCourseDetailsforExcel(int degreeno, int branchno, int deptno, int schemeno, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[2] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_COURSE_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.AllCourseDetailsforExcel-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added By Pooja on 
                /// Modified by Saurabh S.
                /// </summary>25.06.2022
                /// <param name="objc"></param>
                /// <returns></returns>
                public int SaveExcelSheetCourseDataInDataBase(Course objc, int drawing)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[27];
                        objParams[0] = new SqlParameter("@P_COURSE_NAME", objc.CourseName);
                        objParams[1] = new SqlParameter("@P_SHORTNAME", objc.CourseShortName);
                        objParams[2] = new SqlParameter("@P_CCODE", objc.CCode);
                        objParams[3] = new SqlParameter("@P_CREDITS", objc.Credits);
                        objParams[4] = new SqlParameter("@P_ELECT", objc.Elect);
                        objParams[5] = new SqlParameter("@P_SUBID", objc.SubID);
                        objParams[6] = new SqlParameter("@P_SEMESTERNO", objc.SemesterNo);
                        objParams[7] = new SqlParameter("@P_SCHEMENO", objc.SchemeNo);
                        objParams[8] = new SqlParameter("@P_DEPTNO", objc.Deptno);

                        objParams[9] = new SqlParameter("@P_MINMARK_I", objc.InterMarkMin);
                        objParams[10] = new SqlParameter("@P_MAXMARK_I", objc.MaxMarks_I);
                        objParams[11] = new SqlParameter("@P_MINMARK_E", objc.ExtermarkMin);
                        objParams[12] = new SqlParameter("@P_MAXMARK_E", objc.ExtermarkMax);
                        objParams[13] = new SqlParameter("@P_TOTAL_MARKS", objc.Total_Marks);
                        objParams[14] = new SqlParameter("@P_MIN_MARK_TOT_PER", objc.MinTotalMarks);
                        objParams[15] = new SqlParameter("@P_ELECTIVE_GROUP", objc.Electivegrpno);
                        objParams[16] = new SqlParameter("@P_ISVALUE_ADDED", objc.ValueAdded);
                        objParams[17] = new SqlParameter("@P_IS_SPECIAL", objc.Specialisation);
                        //Below fields Added by Saurabh S.
                        //objParams[20] = new SqlParameter("@P_ADMBATCH", objenq.ADMBATCH);
                        objParams[18] = new SqlParameter("@P_ISFEEDBACK", objc.IsFeedback);
                        objParams[19] = new SqlParameter("@P_GLOBALELE", objc.GlobalEle);
                        objParams[20] = new SqlParameter("@P_LECTURE", objc.Lecture);
                        objParams[21] = new SqlParameter("@P_THEORY", objc.Theory);
                        objParams[22] = new SqlParameter("@P_PRACTICAL", objc.Practical);
                        objParams[23] = new SqlParameter("@P_DRAWING", drawing);
                        objParams[24] = new SqlParameter("@P_PAPER_HRS", objc.Paper_hrs);
                        objParams[25] = new SqlParameter("@P_IS_AUDIT", objc.IsAudit);
                        objParams[26] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[26].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPLOAD_STUD_NEW_COURSE_DATA_EXCEL", objParams, true);
                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                        {
                            if (obj.ToString() == "1")
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (obj.ToString() == "2")
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }

                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.SaveExcelSheetDataInDataBase() -> " + ex.ToString());
                    }
                    return retStatus;
                }

                //ADDED BY POOJA Import Course Data

                public DataSet RetrieveCourseMasterDataForExcel()
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_COURSE_DATA_EXCEL_BLANKSHEET", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetAllSubModuleDetails-> " + ex.ToString());
                    }

                    return ds;
                }

                //UPDATED BY SAKSHI M ON 07122023
                public int InsertSubjectTypeData(Course objCe, int istutorial)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            
                            new SqlParameter("@P_TH_PR",objCe.theory_Pra),
                            new SqlParameter("@P_SEC_BATCH", objCe.sec_batch),
                            new SqlParameter("@P_ACTIVESTATUS",objCe.activestatus),
                            new SqlParameter("@P_SUBNAME", objCe.subjecttype),
                            new SqlParameter("@P_ISTUTORIAL",istutorial),
                            new SqlParameter("@P_UANO", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"])),
                            new SqlParameter("@P_IPADDRESS", System.Web.HttpContext.Current.Session["ipAddress"].ToString()),
                            new SqlParameter("@P_OUT", SqlDbType.Int),
                              };

                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_SUBJECT_TYPE", sqlParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        if (Convert.ToInt32(ret) == 2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.InsertWorkspaceData-> " + ex.ToString());
                    }
                    return retStatus;
                }


                //UPDATED BY SAKSHI M ON 07122023
                public int UpdateSubjectTypeData(Course objCe, int subid, int istutorial)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_SUBID",subid),
                            new SqlParameter("@P_TH_PR",objCe.theory_Pra ),
                            new SqlParameter("@P_SEC_BATCH",objCe.sec_batch),
                             new SqlParameter("@P_ACTIVESTATUS",objCe.activestatus),
                              new SqlParameter("@P_SUBNAME", objCe.subjecttype),
                             new SqlParameter("@P_UANO", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"])),
                            new SqlParameter("@P_IPADDRESS", System.Web.HttpContext.Current.Session["ipAddress"].ToString()),
                              new SqlParameter("@P_ISTUTORIAL",istutorial),
                              new SqlParameter("@P_OUT", SqlDbType.Int),
                          };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_SUBJECTTYPE_DATA", sqlParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        if (Convert.ToInt32(ret) == 2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.UpdateWorkspaceData-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetSubjectTypeList(Course objCe)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SELECT_SUBJECTTYPE_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetSubjectTypeList-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet EditSubjectTypeData(int SUBID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SUBID", SUBID);
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_EDIT_SUBJECTTYPE_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EditConfiguration-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by Saurabh S.  -19-07-2022
                /// </summary>
                /// <param name="schemeno"></param>
                /// <param name="semesterno"></param>
                /// <param name="to_Term"></param>
                /// <param name="sessionno"></param>
                /// <returns></returns>
                public DataSet GetCourseOfferedGlobally(int schemeno, int semesterno) //, int toSemesterno, int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GLOBALLY_OFFERED_COURSE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetStudentForFaculty-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateGloballyOfferedCourse(GlobalOfferedCourse objGOC, string sessionno, string branchids, string collegeids)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Edit Course
                        objParams = new SqlParameter[17];
                        objParams[0] = new SqlParameter("@P_FROM_COLLEGE_ID", objGOC.College_id);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", objGOC.College_code);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                        //objParams[3] = new SqlParameter("@P_DEGREENO", objGOC.DegreeNo);
                        objParams[3] = new SqlParameter("@P_FROM_SCHEMENO", objGOC.Schemeno);
                        objParams[4] = new SqlParameter("@P_COURSENO", objGOC.Courseno);
                        objParams[5] = new SqlParameter("@P_CCODE", objGOC.CCODE);
                        objParams[6] = new SqlParameter("@P_SEMESTERNO", objGOC.Semesterno);
                        objParams[7] = new SqlParameter("@P_TO_SEMESTERNO", objGOC.To_semesterno);
                        objParams[8] = new SqlParameter("@P_CDBNO", branchids);
                        objParams[9] = new SqlParameter("@P_GLOBAL_OFFERED", objGOC.Global_offered);
                        objParams[10] = new SqlParameter("@P_CREDITS", objGOC.Credits);
                        objParams[11] = new SqlParameter("@P_CAPACITY", objGOC.Capacity);
                        objParams[12] = new SqlParameter("@P_UA_NO", objGOC.Ua_no);
                        objParams[13] = new SqlParameter("@P_MAIN_FACULTY_NO", objGOC.MainFacultyno);
                        objParams[14] = new SqlParameter("@P_ALTERNATE_FACULTY_NO", objGOC.AlternateFacultyno);
                        objParams[15] = new SqlParameter("@P_ORGANIZATION_ID", objGOC.Orgid);

                        objParams[16] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[16].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GLOBAL_OFFERED_COURSE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GLobalOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }
                // added by jay on 11/10/2022
                public DataSet GetredoRegistrationDetailsBySession(string sessiono)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessiono);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_REDO_COURSE_REGISTRATION_EXCEL_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetredoRegistrationDetailsBySession-> " + ex.ToString());
                    }
                    return ds;
                }
                // added by jay on date 10/10/2022
                public DataSet GetCollegeSession(int mode, string collegeID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_MODE", mode);
                        objParams[1] = new SqlParameter("@P_COLLEGE_IDNOS", collegeID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_CONCAT_COLLEGE_SESSION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetCollegeSession-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetCourseRegistrationApprvlList(int clgID, int sessionID, int degreeID, int branchID, int filter)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", clgID);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionID);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeID);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchID);
                        objParams[4] = new SqlParameter("@P_FILTER", filter);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_REG_COURSE_FOR_APPROVAL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GetCourseRegistrationApprvlList-> " + ex.ToString());
                    }
                    return ds;
                }



                public int UpdateCourseRegApproval(Student objGOC, string ipAddress)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Edit Course
                        objParams = new SqlParameter[6];

                        objParams[0] = new SqlParameter("@P_IDNO", objGOC.IdNo);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", objGOC.SessionNo);
                        objParams[2] = new SqlParameter("@P_INCH_CREG_UANO", objGOC.Uano);
                        objParams[3] = new SqlParameter("@P_INCH_CREG_IPADDR", ipAddress);
                        objParams[4] = new SqlParameter("@P_INCH_CREG_DATE", System.DateTime.Now);

                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_COURSE_REG_APPROVAL", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GLobalOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }
                public DataSet GetCourseBranchSessionForGlobalCourseDetails(int schemeNo, int SemesterNo, string courseNos)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeNo);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", SemesterNo);
                        objParams[2] = new SqlParameter("@P_COURSENOS", courseNos);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_COURSE_BRANCH_SESSION_FORGLOBALCOURSE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetPendingCourseRegDataForStudent-> " + ex.ToString());
                    }
                    return ds;
                }
                // EXISTING UPDATED METHODS //  
                public DataSet GetPendingCourseRegDataForStudent(string sessionNos)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SESSIONNOS", sessionNos);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_PENDING_COURSE_REG_STUD_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetPendingCourseRegDataForStudent-> " + ex.ToString());
                    }
                    return ds;
                }
                // added the below 3 function by shailendra on dated 14.09.2022 for global offere courses..
                public DataSet GetOffredGlobalCourses(int clgID, int schemeNo, int semesterNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", clgID);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeNo);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_OFFRED_GLOBAL_COURSES", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetOffredGlobalCourses-> " + ex.ToString());
                    }
                    return ds;
                }
                public int InActiveGlobalOfferedCourses(int GLOBAL_OFFER_ID)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_GLOBAL_OFFER_ID", GLOBAL_OFFER_ID);
                        objParams[1] = new SqlParameter("@P_MODEACTIVE", Convert.ToInt32(0));
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INACTIVE_GLOBAL_OFFERED_COURSES", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GLobalOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }
                public int ActiveGloballyOfferedCourse(int GLOBAL_OFFER_ID, int capacity)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_GLOBAL_OFFER_ID", GLOBAL_OFFER_ID);
                        objParams[1] = new SqlParameter("@P_MODEACTIVE", 1);
                        objParams[2] = new SqlParameter("@P_CAPACITY", capacity);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INACTIVE_GLOBAL_OFFERED_COURSES", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GLobalOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetCollegeSessionForAttendance(int UA_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_COLLEGE_SESSION_FOR_ATTENDANCE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GetCollegeSessionForAttendance-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetOfferedCourseListForBulkCourseRegistration(int sessionno, int schemeno, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_COURSE_OFFERED_FOR_BULK_COURSE_REGISTRATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllOfferedCourseList-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAttendanceConfigData(int sessionno, int college_id, int orgid, int userno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[2] = new SqlParameter("@P_ORGID", orgid);
                        objParams[3] = new SqlParameter("@P_UA_NO", userno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_ATTENDANCE_CONFIG_DATE_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GetCollegeSessionForAttendance-> " + ex.ToString());
                    }
                    return ds;
                }
                // ADDED BY Tejaswini Dhoble on date 12112022
                public DataSet GetFacultyDisciplineList()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SELECT_FACULTY_DISCIPLINE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetFaculty_DisciplineList-> " + ex.ToString());
                    }
                    return ds;
                }

                // ADDED BY Tejaswini Dhoble on date 12112022
                public int InsertFacultyDisciplineData(string faculty_name, string status)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            
                            new SqlParameter("@P_FACULTY_NAME",faculty_name),
                            new SqlParameter("@P_ACTIVESTATUS", status),                       
                              new SqlParameter("@P_OUT", SqlDbType.Int),
                              
                              };

                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_FACULTY_DISCIPLINE", sqlParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        if (Convert.ToInt32(ret) == 2)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.InsertFacultyDisciplineData-> " + ex.ToString());
                    }
                    return retStatus;
                }

                // ADDED BY Tejaswini Dhoble on date 12112022
                public int UpdateFacultyDisciplineData(int id, string faculty_name, string status)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_FACULTY_NO",id),
                            new SqlParameter("@P_FACULTY_NAME",faculty_name),
                            new SqlParameter("@P_ACTIVESTATUS",status),
                            new SqlParameter("@P_OUT", SqlDbType.Int),
                        
                          };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_FACULTY_DISCIPLINE", sqlParams, false);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);



                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.UpdateFacultyDisciplineData-> " + ex.ToString());
                    }
                    return retStatus;
                }

                // ADDED BY Tejaswini Dhoble on date 12112022
                public DataSet EditFacultyDiscplineData(int ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_FACULTY_NO", ID);
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_EDIT_FACULTY_DISCIPLINE_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EditFacultyDiscplineData-> " + ex.ToString());
                    }
                    return ds;
                }

                #region Global Elective
                /// <summary>
                /// Added by Swapnil for Global Elective Offered Course
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="courseno"></param>
                /// <param name="mode"></param>
                /// <returns></returns>
                /// Done
                public int SaveGloballyOfferedCourseModified(GlobalOfferedCourse objGOC, string sessionno, string branchids, string collegeids)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Edit Course
                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_FROM_COLLEGE_ID", objGOC.College_id);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", objGOC.College_code);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                        //objParams[3] = new SqlParameter("@P_DEGREENO", objGOC.DegreeNo);
                        objParams[3] = new SqlParameter("@P_FROM_SCHEMENO", objGOC.Schemeno);
                        objParams[4] = new SqlParameter("@P_COURSENO", objGOC.Courseno);
                        objParams[5] = new SqlParameter("@P_CCODE", objGOC.CCODE);
                        objParams[6] = new SqlParameter("@P_SEMESTERNO", objGOC.Semesterno);
                        objParams[7] = new SqlParameter("@P_TO_SEMESTERNO", objGOC.To_semesterno);
                        objParams[8] = new SqlParameter("@P_CDBNO", branchids);
                        objParams[9] = new SqlParameter("@P_GLOBAL_OFFERED", objGOC.Global_offered);
                        objParams[10] = new SqlParameter("@P_CREDITS", objGOC.Credits);
                        objParams[11] = new SqlParameter("@P_CAPACITY", objGOC.Capacity);
                        objParams[12] = new SqlParameter("@P_UA_NO", objGOC.Ua_no);
                        //objParams[13] = new SqlParameter("@P_MAIN_FACULTY_NO", objGOC.MainFacultyno);
                        //objParams[14] = new SqlParameter("@P_ALTERNATE_FACULTY_NO", objGOC.AlternateFacultyno);
                        objParams[13] = new SqlParameter("@P_ORGANIZATION_ID", objGOC.Orgid);
                        objParams[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[14].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GLOBAL_OFFERED_COURSE_MODIFIED", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.SaveGloballyOfferedCourseModified -> " + ex.ToString());
                    }

                    return retStatus;
                }

                /// <summary>
                /// Added by Swapnil for Global Elective Offered Course
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="courseno"></param>
                /// <param name="mode"></param>
                /// <returns></returns>
                /// Done
                public int UpdateGloballyOfferedCourseModified(GlobalOfferedCourse objGOC, string sessionno, string branchids, string collegeids)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Edit Course
                        objParams = new SqlParameter[16];
                        objParams[0] = new SqlParameter("@P_FROM_COLLEGE_ID", objGOC.College_id);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", objGOC.College_code);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                        //objParams[3] = new SqlParameter("@P_DEGREENO", objGOC.DegreeNo);
                        objParams[3] = new SqlParameter("@P_FROM_SCHEMENO", objGOC.Schemeno);
                        objParams[4] = new SqlParameter("@P_COURSENO", objGOC.Courseno);
                        objParams[5] = new SqlParameter("@P_CCODE", objGOC.CCODE);
                        objParams[6] = new SqlParameter("@P_SEMESTERNO", objGOC.Semesterno);
                        objParams[7] = new SqlParameter("@P_TO_SEMESTERNO", objGOC.To_semesterno);
                        objParams[8] = new SqlParameter("@P_CDBNO", branchids);
                        objParams[9] = new SqlParameter("@P_GLOBAL_OFFERED", objGOC.Global_offered);
                        objParams[10] = new SqlParameter("@P_CREDITS", objGOC.Credits);
                        objParams[11] = new SqlParameter("@P_CAPACITY", objGOC.Capacity);
                        objParams[12] = new SqlParameter("@P_UA_NO", objGOC.Ua_no);
                        //objParams[13] = new SqlParameter("@P_MAIN_FACULTY_NO", objGOC.MainFacultyno);
                        //objParams[14] = new SqlParameter("@P_ALTERNATE_FACULTY_NO", objGOC.AlternateFacultyno);
                        objParams[13] = new SqlParameter("@P_ORGANIZATION_ID", objGOC.Orgid);
                        objParams[14] = new SqlParameter("@P_GROUP_ID", objGOC.GroupId);
                        objParams[15] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[15].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_GLOBAL_OFFERED_COURSE_MODIFIED", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GLobalOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                /// <summary>
                /// Added by Swapnil for Global Elective Offered Course
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="courseno"></param>
                /// <param name="mode"></param>
                /// <returns></returns>
                /// Done
                public DataSet GetGlobalOfferedCourseList(int sessionno, int courseno, int ua_no, int mode)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[3] = new SqlParameter("@P_MODE", mode);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_DROPDOWN_SP_GET_OFFERED_GLOBAL_COURSE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetGlobalOfferedCourseList-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetGlobalOfferedCourseList_Section(int sessionno, int courseno, int ua_no, int mode, int sectionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[3] = new SqlParameter("@P_MODE", mode);
                        objParams[4] = new SqlParameter("@P_SECTIONNO", sectionno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_DROPDOWN_SP_GET_OFFERED_GLOBAL_COURSE_SECTION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetGlobalOfferedCourseList-> " + ex.ToString());
                    }
                    return ds;
                }


                /// <summary>
                /// Added by Swapnil for Global Elective Offered Course
                /// </summary>
                /// <param name="schemeNo"></param>
                /// <returns></returns>
                /// Done
                public DataSet GetGlobalCoursesTimeTable(int schemeNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", schemeNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_GLOBAL_COURSE_TIME_TABLE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetOffredGlobalCourses-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by Swapnil for Global Elective Offered Course
                /// </summary>
                /// <param name="facultyNo"></param>
                /// <param name="alternate"></param>
                /// <param name="schemeno"></param>
                /// <returns></returns>
                /// Done
                public DataSet GetGlobalCoursesTimeTableDetailsSection(int facultyNo, int alternate, int schemeno, string startDate, string endDate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_FACULTYNO", facultyNo);
                        objParams[1] = new SqlParameter("@P_ALTERNATEFLAG", alternate);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[3] = new SqlParameter("@P_START_DATE", startDate);
                        objParams[4] = new SqlParameter("@P_END_DATE", endDate);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_GLOBAL_COURSE_TIME_TABLE_DETAILS_SECTION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetOffredGlobalCourses-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by Swapnil for Global Elective Offered Course
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="courseno"></param>
                /// <param name="mode"></param>
                /// <returns></returns>
                /// Done
                public SqlDataReader GetGlobalCoursesTimeTableEdit(int facultyNo, int alternate, int schemeno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_FACULTYNO", facultyNo);
                        objParams[1] = new SqlParameter("@P_ALTERNATEFLAG", alternate);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_GET_GLOBAL_COURSE_TIME_TABLE_FOR_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetSingleSession-> " + ex.ToString());
                    }
                    return dr;
                }

                /// <summary>
                /// Added by Swapnil for Global Elective Offered Course
                /// </summary>
                /// <param name="UA_NO"></param>
                /// <returns></returns>
                /// Done
                public DataSet GetCollegeSessionForAttendanceGlobalElective(int UA_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_COLLEGE_SESSION_FOR_ATTENDANCE_GLOBAL_ELECTIVE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GetCollegeSessionForAttendance-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by Swapnil for Global Elective Offered Course
                /// </summary>
                /// <param name="orgid"></param>
                /// <param name="userno"></param>
                /// <returns></returns>
                /// Done
                public DataSet GetAttendanceConfigDataGlobalElective(int orgid, int userno, int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_ORGID", orgid);
                        objParams[1] = new SqlParameter("@P_UA_NO", userno);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_ATTENDANCE_CONFIG_DATE_DATA_GLOBAL_ELECTIVE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GetAttendanceConfigDataGlobalElective-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by Swapnil for Global Elective Offered Course
                /// </summary>
                /// <param name="orgid"></param>
                /// <param name="userno"></param>
                /// <returns></returns>
                /// Done
                public DataSet GetOffredGlobalCoursesModified(int clgID, int schemeNo, int semesterNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", clgID);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeNo);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_OFFRED_GLOBAL_COURSES_MODIFIED", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetOffredGlobalCourses-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by Swapnil for Global Elective Offered Course
                /// </summary>
                /// <param name="orgid"></param>
                /// <param name="userno"></param>
                /// <returns></returns>
                /// Done
                public DataSet GetOffredGlobalCoursesDetailsForEdit(int clgID, int schemeNo, int semesterNo, int groupId, int Courseno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", clgID);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeNo);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterNo);
                        objParams[3] = new SqlParameter("@P_GROUPID", groupId);
                        objParams[4] = new SqlParameter("@P_COUSERNO", Courseno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_OFFRED_GLOBAL_COURSES_FOR_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetOffredGlobalCourses-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by Swapnil for Global Elective Offered Course
                /// </summary>
                /// <param name="schemeNo"></param>
                /// <returns></returns>
                /// Done
                public DataSet GetGlobalCoursesTimeTableModified(int Sessionno, int courseno, int ua_no,int sectionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_GLOBAL_COURSE_TIME_TABLE_MODIFIED", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetOffredGlobalCourses-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by Swapnil for Global Elective Offered Course
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="courseno"></param>
                /// <param name="mode"></param>
                /// <returns></returns>
                /// Done
                public SqlDataReader GetGlobalCoursesTimeTableEditModified(int facultyNo, int alternate, int sessionno, int courseno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_FACULTYNO", facultyNo);
                        objParams[1] = new SqlParameter("@P_ALTERNATEFLAG", alternate);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[3] = new SqlParameter("@P_COURSENO", courseno);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_GET_GLOBAL_COURSE_TIME_TABLE_FOR_EDIT_MODIFIED", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetGlobalCoursesTimeTableEditModified-> " + ex.ToString());
                    }
                    return dr;
                }



                /// <summary>
                /// Added by Swapnil for Global Elective Offered Course
                /// </summary>
                /// <param name="facultyNo"></param>
                /// <param name="alternate"></param>
                /// <param name="schemeno"></param>
                /// <returns></returns>
                /// Done
                public DataSet GetGlobalCoursesTimeTableDetailsSectionModified(int facultyNo, int alternate, int Sessionno, string startDate, string endDate, int courseno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_FACULTYNO", facultyNo);
                        objParams[1] = new SqlParameter("@P_ALTERNATEFLAG", alternate);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[3] = new SqlParameter("@P_START_DATE", startDate);
                        objParams[4] = new SqlParameter("@P_END_DATE", endDate);
                        objParams[5] = new SqlParameter("@P_COURSENO", courseno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_GLOBAL_COURSE_TIME_TABLE_DETAILS_SECTION_MODIFIED", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetGlobalCoursesTimeTableDetailsSectionModified-> " + ex.ToString());
                    }
                    return ds;
                }


                #endregion

                public DataSet GetCourseForElectiveIntakeCapicity(int sessionno, string semesternos)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONID", sessionno);
                        objParams[1] = new SqlParameter("@P_SEMESTERNOS", semesternos);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_COURSE_FOR_INTAKE_CAPACITY_WITH_SEMESTER", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetCourseOffered-> " + ex.ToString());
                    }
                    return ds;
                }

                // MODIFY BELOW METHOD BY SHAILENDRA K. ON DATED 21.06.2023 AS PER T-44837 AND 44816 
                public int UpdateOfferedElectiveCourseIntakeCapacity(int Sessionno, int Semesterno, int Capacity, int SchemeNo, int Courseno, int ua_no, string ipAdd, double eligibilityCGPA)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Edit Course
                        objParams = new SqlParameter[9];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[1] = new SqlParameter("@P_SEMESTERNO", Semesterno);
                        objParams[2] = new SqlParameter("@P_CAPACITY", Capacity);
                        objParams[3] = new SqlParameter("@P_SCHEMENO", SchemeNo);
                        objParams[4] = new SqlParameter("@P_COURSENO", Courseno);
                        objParams[5] = new SqlParameter("@P_UANO", ua_no);
                        objParams[6] = new SqlParameter("@P_IPADDRESS", ipAdd);
                        objParams[7] = new SqlParameter("@P_ELIGIBILITY_CGPA", eligibilityCGPA);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_UPDATE_COURSE_FOR_INTAKE_CAPACITY_WITH_SEMESTER", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetCourseRegistrationApprvlListModified(int clgID, int sessionID, int degreeID, int branchID, int filter, int semesterNo, int studtype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", clgID);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", sessionID);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeID);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchID);
                        objParams[4] = new SqlParameter("@P_FILTER", filter);
                        objParams[5] = new SqlParameter("@P_SEMESTERNO", semesterNo); // Added by Shailendra K. On Dated 18.10.2023 as per T-46795
                        objParams[6] = new SqlParameter("@P_STUD_REGD", studtype); // Added by Shailendra K. On Dated 18.10.2023 as per T-46795
                        // ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_REG_COURSE_FOR_APPROVAL_MODIFIED", objParams); // commented by Shailendra K. On Dated 18.10.2023 as per T-46795
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_STUD_FOR_COURSE_APPROVAL", objParams); // Added by Shailendra K. On Dated 18.10.2023 as per T-46795
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GetCourseRegistrationApprvlList-> " + ex.ToString());
                    }
                    return ds;
                }


                /// <summary>
                /// Added by Swapnil for Bind college by Session id
                /// </summary>
                /// <param name="mode"></param>
                /// <param name="sessionid"></param>
                /// <returns></returns>
                public DataSet GetCollegeBySessionid(int mode, int sessionid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_MODE", mode);
                        objParams[1] = new SqlParameter("@P_SESSIONID", sessionid);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_COLLEGE_BY_SESSIONID", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetCollegeBySessionid-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added By Swapnil Prachand For Course Registration Pending Excel Report all
                /// </summary>
                /// <param name="session"></param>
                /// <returns></returns>
                public DataSet GetAllCourseRegistrationPendingReportData(int sessionid, string collegenos, int ctype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSION_ID", sessionid);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", collegenos);
                        objParams[2] = new SqlParameter("@P_COURSE_TYPE", ctype);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_COURSE_REGISTRATION_PENDING_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllCourseRegistrationData-> " + ex.ToString());
                    }
                    return ds;
                }

                // <summary>
                /// Added By Swapnil Prachand For Course Registration Report all
                /// </summary>
                /// <param name="session"></param>
                /// <returns></returns>
                public DataSet GetAllCourseRegistrationReportExcelData(int sessionid, string collegenos, int reporttype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSION_ID", sessionid);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", collegenos);
                        objParams[2] = new SqlParameter("@P_REPORT_TYPE", reporttype);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_COURSE_REGISTRATION_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllCourseRegistrationData-> " + ex.ToString());
                    }
                    return ds;
                }


                /// <summary>
                /// Added By Swapnil Prachand For Course Registration Pending Excel Report all
                /// </summary>
                /// <param name="session"></param>
                /// <returns></returns>
                public DataSet GetAllCourseRegistrationPendingReportExcelData(int sessionid, string collegenos, int ctype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSION_ID", sessionid);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", collegenos);
                        objParams[2] = new SqlParameter("@P_COURSE_TYPE", ctype);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_COURSE_REGISTRATION_PENDING_DETAILS_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllCourseRegistrationData-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetAllStudentWiseCourseRegistrationSummaryReportExcelData(int sessionid, string collegenos)
                    {
                    DataSet ds = null;
                    try
                        {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSION_ID", sessionid);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", collegenos);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_COURSE_REGISTRATION_STUDENT_SUMMARY_DETAILS_EXCEL", objParams);
                        }
                    catch (Exception ex)
                        {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllCourseRegistrationData-> " + ex.ToString());
                        }
                    return ds;
                    }

                public int InActiveGlobalOfferedCoursesTeacherAllotment(int sessionno, int courseno, int ua_no, string IpAddress, int Modifiedby)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[3] = new SqlParameter("@P_MODEACTIVE", Convert.ToInt32(0));
                        objParams[4] = new SqlParameter("@P_IPADDRESS", IpAddress);
                        objParams[5] = new SqlParameter("@P_MODIFIEDBY", Modifiedby);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INACTIVE_GLOBAL_COURSE_TEACHER_ALLOTMENT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GLobalOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int InActiveGlobalTimeTableDateWise(int slotno, string Datetime, int ua_no, int courseno, int sessionid, int cancelby, string cancelipaddress, string cancelRemark,int cancellationtype,int sectionno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_SLOTNO", slotno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[3] = new SqlParameter("@P_SESSIONID", sessionid);
                        objParams[4] = new SqlParameter("@P_CURRENTDATE", Datetime);
                        objParams[5] = new SqlParameter("@P_CANCEL_BY", cancelby);
                        objParams[6] = new SqlParameter("@P_CANCEL_IPADDRESS", cancelipaddress);
                        objParams[7] = new SqlParameter("@P_CANCEL_REMARK", cancelRemark);
                        objParams[8] = new SqlParameter("@P_CANCELLATION_TYPE", cancellationtype);
                        objParams[9] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_TIMETABLE_CANCEL_GLOBAL_ELECTIVE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GLobalOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }


                // added by shubham On 22/03/2023
                public int UpdatePaperSetCourseQTY(string ccode, int Schemeno, int semesterno, int qty, int req)
                {
                    int retun_status = Convert.ToInt16(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_CCODE", ccode);
                        //objParams[1] = new SqlParameter("@P_BOS_DEPTNO", bos_deptno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", Schemeno);
                        objParams[2] = new SqlParameter("@P_SEMSTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_QTY", qty);
                        objParams[4] = new SqlParameter("@P_REORDER", req);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_COURSE_QTY_REQ", objParams, true);
                        retun_status = Convert.ToInt16(ret);

                    }
                    catch (Exception ex)
                    {
                        retun_status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.UpdateCourseQTY-> " + ex.ToString());
                    }

                    return retun_status;
                }

                /// <summary>
                /// Added by Swapnil for Global Elective Offered Course
                /// </summary>
                /// <param name="schemeNo"></param>
                /// <returns></returns>
                /// Done
                public DataSet GetGlobalCoursesRevisedTimeTableModified(int Sessionno, int courseno, int ua_no,int sectionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_GLOBAL_COURSE_REVISED_TIME_TABLE_MODIFIED", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetGlobalCoursesRevisedTimeTableModified-> " + ex.ToString());
                    }
                    return ds;
                }
                /// <summary>
                /// Added By Nehal on 03/04/2023 to get Excel Report.
                /// </summary>
                /// <param name="session"></param>
                /// <returns></returns>
                public DataSet GetAllCourseRegistrationDataExcel(int session, string collegeid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONID", session);
                        objParams[1] = new SqlParameter("@P_COLLEGE_ID", collegeid);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_REGHISTRATION_REPORT_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllCourseRegistrationDataExcel-> " + ex.ToString());
                    }
                    return ds;
                }
                //Added By Aniket P. On dated 14/06/2023
                public DataSet RetrieveNpfMappingDataForExcel()
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_NPF_MAPPING_EXCEL_BLANKSHEET", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.RetrieveNpfMappingDataForExcel-> " + ex.ToString());
                    }

                    return ds;
                }
                //*****************************************Valu_added_course_registrtion ondated 21062023*****************************************************
                /// <summary>
                /// Added by Amit Bhumbur for Value Added Offered Course
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="courseno"></param>
                /// <param name="mode"></param>
                /// <returns></returns>
                /// Done
                public int UpdateValueAddedCourseModified(GlobalOfferedCourse objGOC, string sessionno, string branchids, string collegeids, int Duration, string DURATION_START_DATE, string DURATION_END_DATE, int ASSESSMENT_INVOLVE, int SHOW_ON_GRADE_CARD)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Edit Course
                        objParams = new SqlParameter[21];
                        objParams[0] = new SqlParameter("@P_FROM_COLLEGE_ID", objGOC.College_id);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", objGOC.College_code);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[3] = new SqlParameter("@P_FROM_SCHEMENO", objGOC.Schemeno);
                        objParams[4] = new SqlParameter("@P_COURSENO", objGOC.Courseno);
                        objParams[5] = new SqlParameter("@P_CCODE", objGOC.CCODE);
                        objParams[6] = new SqlParameter("@P_SEMESTERNO", objGOC.Semesterno);
                        objParams[7] = new SqlParameter("@P_TO_SEMESTERNO", objGOC.To_semesterno);
                        objParams[8] = new SqlParameter("@P_CDBNO", branchids);
                        objParams[9] = new SqlParameter("@P_VALUE_ADDED", objGOC.Global_offered);
                        objParams[10] = new SqlParameter("@P_CREDITS", objGOC.Credits);
                        objParams[11] = new SqlParameter("@P_CAPACITY", objGOC.Capacity);
                        objParams[12] = new SqlParameter("@P_UA_NO", objGOC.Ua_no);
                        objParams[13] = new SqlParameter("@P_ORGANIZATION_ID", objGOC.Orgid);
                        objParams[14] = new SqlParameter("@P_GROUP_ID", objGOC.GroupId);

                        objParams[15] = new SqlParameter("@P_ASSESSMENT_INVOLVE", ASSESSMENT_INVOLVE);
                        objParams[16] = new SqlParameter("@P_SHOW_ON_GRADE_CARD", SHOW_ON_GRADE_CARD);
                        objParams[17] = new SqlParameter("@P_DURATION", Duration);
                        objParams[18] = new SqlParameter("@P_DURATION_START_DATE", DURATION_START_DATE);
                        objParams[19] = new SqlParameter("@P_DURATION_END_DATE", DURATION_END_DATE);

                        objParams[20] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[20].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_VALUE_ADDED_COURSE_MODIFIED", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateValueAddedCourseModified -> " + ex.ToString());
                    }

                    return retStatus;
                }

                /// <summary>
                /// Added by Amit Bhumbur for Value Added Offered Course
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="courseno"></param>
                /// <param name="mode"></param>
                /// <returns></returns>
                /// Done
                public int SaveValueAddedCourseModified(GlobalOfferedCourse objGOC, string sessionno, string branchids, string collegeids, string DURATION_START_DATE, string DURATION_END_DATE, int ASSESSMENT_INVOLVE, int SHOW_ON_GRADE_CARD, int Duration)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Edit Course
                        objParams = new SqlParameter[20];
                        objParams[0] = new SqlParameter("@P_FROM_COLLEGE_ID", objGOC.College_id);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", objGOC.College_code);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[3] = new SqlParameter("@P_FROM_SCHEMENO", objGOC.Schemeno);
                        objParams[4] = new SqlParameter("@P_COURSENO", objGOC.Courseno);
                        objParams[5] = new SqlParameter("@P_CCODE", objGOC.CCODE);
                        objParams[6] = new SqlParameter("@P_SEMESTERNO", objGOC.Semesterno);
                        objParams[7] = new SqlParameter("@P_TO_SEMESTERNO", objGOC.To_semesterno);
                        objParams[8] = new SqlParameter("@P_CDBNO", branchids);
                        objParams[9] = new SqlParameter("@P_VALUE_ADDED", objGOC.Global_offered);
                        objParams[10] = new SqlParameter("@P_CREDITS", objGOC.Credits);
                        objParams[11] = new SqlParameter("@P_CAPACITY", objGOC.Capacity);
                        objParams[12] = new SqlParameter("@P_UA_NO", objGOC.Ua_no);
                        objParams[13] = new SqlParameter("@P_ORGANIZATION_ID", objGOC.Orgid);

                        objParams[14] = new SqlParameter("@P_ASSESSMENT_INVOLVE", ASSESSMENT_INVOLVE);
                        objParams[15] = new SqlParameter("@P_SHOW_ON_GRADE_CARD", SHOW_ON_GRADE_CARD);
                        objParams[16] = new SqlParameter("@P_DURATION_START_DATE", DURATION_START_DATE);
                        objParams[17] = new SqlParameter("@P_DURATION_END_DATE", DURATION_END_DATE);
                        objParams[18] = new SqlParameter("@P_DURATION", Duration);

                        objParams[19] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[19].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_VALUE_ADDED_COURSE_MODIFIED", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.SaveGloballyOfferedCourseModified -> " + ex.ToString());
                    }

                    return retStatus;
                }

                /// <summary>
                /// Added by Amit Bhumbur for Value Added Offered Course
                /// </summary>
                /// <param name="orgid"></param>
                /// <param name="userno"></param>
                /// <returns></returns>
                /// Done
                public DataSet GetValueAddedCoursesModified(int clgID, int schemeNo, int semesterNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", clgID);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeNo);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_VALUE_ADDED_COURSES", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetValueAddedCoursesModified-> " + ex.ToString());
                    }
                    return ds;
                }


                /// <summary>
                ///  Added by Amit Bhumbur for Value Added Offered Course
                /// </summary>
                /// <param name="VALUE_ADDED_ID"></param>
                /// <returns></returns>
                public int InActiveValueAddedCourses(int VALUE_ADDED_ID)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_VALUE_ADDED_ID", VALUE_ADDED_ID);
                        objParams[1] = new SqlParameter("@P_MODEACTIVE", Convert.ToInt32(0));
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INACTIVE_VALUE_ADDED_COURSES", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.InActiveValueAddedCourses -> " + ex.ToString());
                    }

                    return retStatus;
                }

                /// <summary>
                /// Added by Amit Bhumbur for Value Added Offered Course
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="courseno"></param>
                /// <param name="mode"></param>
                /// <returns></returns>
                /// Done
                public DataSet GetValueAddedCourseList(int sessionno, int courseno, int ua_no, int mode,int sectionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[3] = new SqlParameter("@P_MODE", mode);
                        objParams[4] = new SqlParameter("@P_SECTIONNO", sectionno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_DROPDOWN_SP_GET_OFFERED_VALUE_ADDED_COURSE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetValueAddedCourseList-> " + ex.ToString());
                    }
                    return ds;
                }


                /// <summary>
                ///  Added by Amit Bhumbur for Value Added Offered Course
                /// </summary>
                /// <param name="schemeNo"></param>
                /// <returns></returns>
                /// Done
                public DataSet GetValueAddedCoursesTimeTableModified(int Sessionno, int courseno, int ua_no,int sectionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_VALUE_ADDED_COURSE_TIME_TABLE_MODIFIED", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetValueAddedCoursesTimeTableModified-> " + ex.ToString());
                    }
                    return ds;

                }



                /// <summary>
                /// Added by Amit Bhumbur for Value Added Offered Course
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="courseno"></param>
                /// <param name="mode"></param>
                /// <returns></returns>
                /// Done
                public SqlDataReader GetValueAddedCoursesTimeTableEditModified(int facultyNo, int alternate, int sessionno, int courseno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_FACULTYNO", facultyNo);
                        objParams[1] = new SqlParameter("@P_ALTERNATEFLAG", alternate);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[3] = new SqlParameter("@P_COURSENO", courseno);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_GET_VALUE_ADDED_COURSE_TIME_TABLE_FOR_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetGlobalCoursesTimeTableEditModified-> " + ex.ToString());
                    }
                    return dr;
                }

                /// <summary>
                /// Added by Amit Bhumbur for Value Added Offered Course
                /// MODIFIED BY JAY T. FOR Value Added TT DETAILS SECTION
                /// </summary>
                /// <param name="facultyNo"></param>
                /// <param name="alternate"></param>
                /// <param name="schemeno"></param>
                /// <returns></returns>
                /// Done
                public DataSet GetValueAddedCoursesTimeTableDetailsSectionModified(int facultyNo, int alternate, int Sessionno, string startDate, string endDate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_FACULTYNO", facultyNo);
                        objParams[1] = new SqlParameter("@P_ALTERNATEFLAG", alternate);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[3] = new SqlParameter("@P_START_DATE", startDate);
                        objParams[4] = new SqlParameter("@P_END_DATE", endDate);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_VALUE_ADDED_TIME_TABLE_DETAILS_SECTION_MODIFIED", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetOffredGlobalCourses-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetValueAddedCoursesTimeTableDetailsSectionModified(int facultyNo, int alternate, int Sessionno, string startDate, string endDate, int courseno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_FACULTYNO", facultyNo);
                        objParams[1] = new SqlParameter("@P_ALTERNATEFLAG", alternate);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[3] = new SqlParameter("@P_START_DATE", startDate);
                        objParams[4] = new SqlParameter("@P_END_DATE", endDate);
                        objParams[5] = new SqlParameter("@P_COURSENO", courseno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_VALUE_ADDED_COURSE_TIME_TABLE_DETAILS_SECTION_MODIFIED", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetValueAddedCoursesTimeTableDetailsSectionModified-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by Amit Bhumbur for Value Added Offered Course
                /// </summary>
                /// <param name="orgid"></param>
                /// <param name="userno"></param>
                /// <returns></returns>
                /// Done
                public DataSet GetOffredValueAddedCoursesDetailsForEdit(int clgID, int schemeNo, int semesterNo, int groupId, int Courseno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", clgID);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeNo);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterNo);
                        objParams[3] = new SqlParameter("@P_GROUPID", groupId);
                        objParams[4] = new SqlParameter("@P_COUSERNO", Courseno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_OFFRED_VALUE_ADDED_COURSES_FOR_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetOffredGlobalCourses-> " + ex.ToString());
                    }
                    return ds;
                }


                /// <summary>
                /// Added by jay For Value added courses 
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="courseno"></param>
                /// <param name="mode"></param>
                /// <returns></returns>
                /// Done
                public DataSet GetValueAddedOfferedCourseList(int sessionno, int courseno, int ua_no, int mode,int sectionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[3] = new SqlParameter("@P_MODE", mode);
                        objParams[4] = new SqlParameter("@P_SECTIONNO", sectionno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_DROPDOWN_SP_GET_OFFERED_VALUE_ADDED_COURSE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetGlobalOfferedCourseList-> " + ex.ToString());
                    }
                    return ds;
                }


                /// <summary>
                /// Added by Amit B. for Value Added Course
                /// </summary>
                /// <param name="schemeNo"></param>
                /// <returns></returns>
                /// Done
                public DataSet GetValueAddedCoursesRevisedTimeTableModified(int Sessionno, int courseno, int ua_no,int sectionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[3] = new SqlParameter("@P_SECTIONNO", sectionno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_VALUE_ADDED_COURSE_REVISED_TIME_TABLE_MODIFIED", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetGlobalCoursesRevisedTimeTableModified-> " + ex.ToString());
                    }
                    return ds;
                }


                /// <summary>
                /// Added by Amit B. for value added Course Teacher Allotment Modified
                /// </summary>
                /// <param name="objStudent"></param>
                /// <param name="OrgId"></param>
                /// <returns></returns>
                public int UpdateCourseTeachAllotForValueAdded(Student_Acd objStudent, int OrgId, int groupid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[8];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", objStudent.SessionNo);
                        objParams[1] = new SqlParameter("@P_COURSENO", objStudent.CourseNo);
                        objParams[2] = new SqlParameter("@P_UA_NO", objStudent.UA_No);
                        objParams[3] = new SqlParameter("@P_ADDITIONAL_UANO", objStudent.AdditionalTeacher);
                        objParams[4] = new SqlParameter("@P_ADDITIONAL_FLAG", objStudent.isAdditionalFlag);
                        objParams[5] = new SqlParameter("@P_GROUP_ID", groupid);
                        objParams[6] = new SqlParameter("@P_ORGID", OrgId);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_COURSE_TEACHER_FOR_VALUE_ADDED", objParams, true);
                        retStatus = Convert.ToInt32(ret);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateCourseTeachAllotForGlobalElective-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// Added by Amit B. for Value Added Course Teacher Allotment
                /// </summary>
                /// <param name="objStudent"></param>
                /// <param name="OrgId"></param>
                /// <returns></returns>

                public DataSet GetValueAddedCourseTeacherAllotment(int sessionno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_RET_COURSE_ALLOTMENT_FOR_VALUE_ADDED", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetGlobalCourseTeacherAllotment-> " + ex.ToString());
                    }

                    return ds;
                }

                /// <summary>
                ///  Added by Amit B. for Value Added Course for Active Teacher Allotment
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="courseno"></param>
                /// <param name="ua_no"></param>
                /// <param name="IpAddress"></param>
                /// <param name="Modifiedby"></param>
                /// <returns></returns>
                public int InActiveValueAddedCoursesTeacherAllotment(int sessionno, int courseno, int ua_no, string IpAddress, int Modifiedby)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[3] = new SqlParameter("@P_MODEACTIVE", Convert.ToInt32(0));
                        objParams[4] = new SqlParameter("@P_IPADDRESS", IpAddress);
                        objParams[5] = new SqlParameter("@P_MODIFIEDBY", Modifiedby);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INACTIVE_VALUE_ADDED_COURSE_TEACHER_ALLOTMENT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GLobalOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }
                //Added by Amit B. for Value Added Course for Active Teacher Allotment
                public int InActiveValueAddedTimeTableDateWise(int slotno, string Datetime, int ua_no, int courseno, int sessionid, int cancelby, string cancelipaddress, string cancelRemark)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_SLOTNO", slotno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[3] = new SqlParameter("@P_SESSIONID", sessionid);
                        objParams[4] = new SqlParameter("@P_CURRENTDATE", Datetime);
                        objParams[5] = new SqlParameter("@P_CANCEL_BY", cancelby);
                        objParams[6] = new SqlParameter("@P_CANCEL_IPADDRESS", cancelipaddress);
                        objParams[7] = new SqlParameter("@P_CANCEL_REMARK", cancelRemark);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_TIMETABLE_CANCEL_VALUE_ADDED_COURSE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GLobalOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }


                //Added by Gopal M. for Course Lock-Unlock Details - 27/07/23 Ticket #46394
                public DataSet GetCourseLockUnlock_Details(int degreeno, int schemeno, string semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[1] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SP_RET_COURSE_LOCKUNLOCK", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetCourseLockUnlock_Details-> " + ex.ToString());
                    }

                    return ds;
                }

                public int InsertCourseLockUnlock(CoursesLockUnlock ObjCLU, string Mode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[15];

                        objParams[0] = new SqlParameter("@P_COURSENO", ObjCLU.COURSENO);
                        objParams[1] = new SqlParameter("@P_COURSE_NAME", ObjCLU.COURSE_NAME);
                        objParams[2] = new SqlParameter("@P_CCODE", ObjCLU.CCode);
                        objParams[3] = new SqlParameter("@P_COURSE_TYPE", ObjCLU.COURSE_TYPE);
                        objParams[4] = new SqlParameter("@P_DEGREENO", ObjCLU.DEGREENO);
                        objParams[5] = new SqlParameter("@P_SCHEMENO", ObjCLU.SCHEMENO);
                        objParams[6] = new SqlParameter("@P_SEMESTERNO", ObjCLU.SEMESTERNO);
                        objParams[7] = new SqlParameter("@P_STATUS", ObjCLU.STATUS);
                        objParams[8] = new SqlParameter("@P_CREATED_BY", ObjCLU.CREATED_BY);
                        objParams[9] = new SqlParameter("@P_MODIFIED_BY", 0);
                        objParams[10] = new SqlParameter("@P_COLLEGE_CODE", ObjCLU.COLLEGE_CODE);
                        objParams[11] = new SqlParameter("@P_OrganizationId", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[12] = new SqlParameter("@P_IPADDRESS", System.Web.HttpContext.Current.Session["ipAddress"].ToString());
                        objParams[13] = new SqlParameter("@P_MODE", Mode);
                        objParams[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[14].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_SP_INSERTUPDATE_COURSE_LOCKUNLOCK", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GLobalOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateCourseLockUnlock(CoursesLockUnlock ObjCLU, string Mode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[15];

                        objParams[0] = new SqlParameter("@P_COURSENO", ObjCLU.COURSENO);
                        objParams[1] = new SqlParameter("@P_COURSE_NAME", ObjCLU.COURSE_NAME);
                        objParams[2] = new SqlParameter("@P_CCODE", ObjCLU.CCode);
                        objParams[3] = new SqlParameter("@P_COURSE_TYPE", ObjCLU.COURSE_TYPE);
                        objParams[4] = new SqlParameter("@P_DEGREENO", ObjCLU.DEGREENO);
                        objParams[5] = new SqlParameter("@P_SCHEMENO", ObjCLU.SCHEMENO);
                        objParams[6] = new SqlParameter("@P_SEMESTERNO", ObjCLU.SEMESTERNO);
                        objParams[7] = new SqlParameter("@P_STATUS", ObjCLU.STATUS);
                        objParams[8] = new SqlParameter("@P_CREATED_BY", 0);
                        objParams[9] = new SqlParameter("@P_MODIFIED_BY", ObjCLU.MODIFIED_BY);
                        objParams[10] = new SqlParameter("@P_COLLEGE_CODE", ObjCLU.COLLEGE_CODE);
                        objParams[11] = new SqlParameter("@P_OrganizationId", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[12] = new SqlParameter("@P_IPADDRESS", System.Web.HttpContext.Current.Session["ipAddress"].ToString());
                        objParams[13] = new SqlParameter("@P_MODE", Mode);
                        objParams[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[14].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_SP_INSERTUPDATE_COURSE_LOCKUNLOCK", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GLobalOfferedCourse -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetDetailsOfLockUnlockStatusofCourse(int courseno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COURSENO", courseno);
                       
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SP_GET_COURSE_LOCKUNLOCK_STATUS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetDetailsOfLockUnlockStatusofCourse-> " + ex.ToString());
                    }

                    return ds;
                }

                //Added by Gopal M 23/10/2023   Ticket
                public DataSet GetBacklogRedoCourseRegistrationApprvlList(int clgID, int departNo, int sessionID, int degreeID, int branchID, int semesterNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", clgID);
                        objParams[1] = new SqlParameter("@P_DEPARTMENTID", 0);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", sessionID);
                        objParams[3] = new SqlParameter("@P_DEGREENO", degreeID);
                        //objParams[4] = new SqlParameter("@P_BRANCHNO", branchID);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterNo);
                        //objParams[4] = new SqlParameter("@P_FILTER", filter);
                        ds = objSQLHelper.ExecuteDataSetSP("ACD_BAKCLOG_REDO_COURSE_REG_APPROVE_VIEWLIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GetCourseRegistrationApprvlList-> " + ex.ToString());
                    }
                    return ds;
                }

                // Backlog Redo Course Regi Approval Update Method - Added by Gopal M 23/10/2023
                public int UpdateBacklogRedoCourseRegApproval(int userno, int SessionNo, int DegreeNo, int BranchNo, int College_ID, string StudIDNOs, string ipAddress)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Edit Course
                        objParams = new SqlParameter[7];

                        objParams[0] = new SqlParameter("@P_IDNOs", StudIDNOs);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[2] = new SqlParameter("@P_COLLEGE_ID", DegreeNo);
                        objParams[3] = new SqlParameter("@P_INCH_CREG_UANO", userno);
                        objParams[4] = new SqlParameter("@P_INCH_CREG_IPADDR", ipAddress);
                        objParams[5] = new SqlParameter("@P_OrgId", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_BACKLOG_REDO_COURSE_REG_APPROVAL", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateBacklogRedoCourseRegApproval -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetGlobalTimeSlotDropdown(int sessionno, int courseno, int slottype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_SLOTTYPE", slottype);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_DROPDOWN_SP_GET_TIME_SLOT_GLOBAL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetGlobalTimeSlotDropdown-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added By Gopal on 17/01/2024 to get student roll list and roster all Course Registration data.
                /// </summary>
                /// <param name="session"></param>
                /// <returns></returns>
                public DataSet GetStudentRollListAndRosterAllCourseRegistrationData(int sessionId, int faculty_uano, string semesterno, int coursetype, int courseno, int sectionno, int batchno, int tut_batchno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SESSIONID", sessionId);
                        objParams[1] = new SqlParameter("@P_UA_NO", faculty_uano);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_SUBID", coursetype);
                        objParams[4] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[5] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[6] = new SqlParameter("@P_BATCHNO", batchno);
                        objParams[7] = new SqlParameter("@P_TUT_BATCHNO", tut_batchno);
                        //objParams[1] = new SqlParameter("@P_MODE", mode);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_ROLLLIST_AND_ROSTER_COURSE_REGISTRATION_DETAIL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllCourseRegistrationData-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentRollListAndRosterAllCourseRegistrationDataExcel(int sessionId, int faculty_uano, string semesterno, int coursetype, int courseno, int sectionno, int batchno, int tut_batchno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SESSIONID", sessionId);
                        objParams[1] = new SqlParameter("@P_UA_NO", faculty_uano);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[3] = new SqlParameter("@P_SUBID", coursetype);
                        objParams[4] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[5] = new SqlParameter("@P_SECTIONNO", sectionno);
                        objParams[6] = new SqlParameter("@P_BATCHNO", batchno);
                        objParams[7] = new SqlParameter("@P_TUT_BATCHNO", tut_batchno);
                        //objParams[1] = new SqlParameter("@P_MODE", mode);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_STUDENT_ROLLLIST_AND_ROSTER_COURSE_REGISTRATION_DETAIL_EXCEL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetAllCourseRegistrationData-> " + ex.ToString());
                    }
                    return ds;
                }


            }//END: BusinessLayer.BusinessLogic

        }

    }//END: UAIMS  

}//END: IITMS
