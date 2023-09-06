using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            //namespace BusinessLogicLayer.BusinessLogic.Bundle
            //{
           public class BundleController
            {
                string _UAIMS_constr = string.Empty;
                public BundleController()
                {
                    _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                }

                public int AddBundleCreationOpenElec(int collegeno, int sessionno, DateTime date, int courseno, int roomno, string bundleNo, string idno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_College", collegeno);
                        objParams[1] = new SqlParameter("@P_Session", sessionno);
                        objParams[2] = new SqlParameter("@P_Date", date);
                        objParams[3] = new SqlParameter("@P_Course", courseno);
                        objParams[4] = new SqlParameter("@P_Room", roomno);
                        objParams[5] = new SqlParameter("@P_BUNDLENO", bundleNo);
                        objParams[6] = new SqlParameter("@P_IDNOS", idno);

                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_INSERT_BUNDLENO", objParams, true));
                        
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamNameController.AddBundleCreation-> " + ex.ToString());
                    }
                    return retStatus;
                }



                public DataSet GetFacultyForAssignBundle(int SESSIONNO, int COURSENO, string BUNDLENO, string LETTERTYPE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_COURSENO", COURSENO);
                        objParams[1] = new SqlParameter("@P_SESSIONNO", SESSIONNO);
                        objParams[2] = new SqlParameter("@P_BUNDLENAME", BUNDLENO);
                        objParams[3] = new SqlParameter("@P_LETTERTYPE", LETTERTYPE);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_FACULTY_FOR_ASSINGBUNDLE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.GetEnvelope-> " + ex.ToString());
                    }

                    return ds;
                }

                public int UpdateMarkEntry(int sessionno, int courseno, string ccode, string idnos, string marks, int lock_status, string exam, int th_pr, int ua_no, string ipaddress, string examtype, int flagReval,
                    string to_email, string from_email, string smsmobile, int flag, string sms_text, string email_text)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[] 
                        {
                            //Parameters for MARKS
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_COURSENO", courseno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_STUDIDS", idnos),
                            //Mark Fields
                            new SqlParameter("@P_MARKS", marks),
                            //Parameters for Final Lock 
                            new SqlParameter("@P_LOCK", lock_status),
                            new SqlParameter("@P_EXAM", exam),
                            //Parameters for ACD_LOCKTRAC TABLE 
                            new SqlParameter("@P_TH_PR", th_pr),
                            new SqlParameter("@P_UA_NO", ua_no),
                            new SqlParameter("@P_IPADDRESS", ipaddress),
                            new SqlParameter("@P_EXAMTYPE", examtype),
                            new SqlParameter("@P_FLAGREVAL", flagReval),

                             new SqlParameter("@P_TO_EMAIL", to_email),//below parameter added by raju bitode on dated 14.03.2019 for maintained security log..
                            new SqlParameter("@P_FROM_EMAIL", from_email),
                            new SqlParameter("@P_SMSMOB", smsmobile),
                            new SqlParameter("@P_FLAG", flag),
                            new SqlParameter("@P_SMS_TEXT", sms_text),
                            new SqlParameter("@P_EMAIL_TEXT", email_text),

                            new SqlParameter("@P_OP", SqlDbType.Int)
                        };
                        objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                        //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARKS", objParams, true);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUD_INSERT_MARKS_NEW", objParams, true);
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
                public int AssignBundle_Valuer(int sessionno, string bundleno, int valuer_ua_no, DateTime issuedate, int status)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_SESSIONNO", sessionno),
                            new SqlParameter("@P_BUNDLENO", bundleno),
                            new SqlParameter("@P_VALUER_UA_NO", valuer_ua_no),
                            new SqlParameter("@P_ISSUEDATE", issuedate),
                            new SqlParameter("@P_STATUS", status)
                            
                        };

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_EXAM_ASSIGN_BUNDLE_VALUER", objParams, false);
                        if (ret != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PStaffController.AddWork_Examiner --> " + ex.ToString());
                    }
                    return retStatus;
                }


               
                public int UPDATE_LOCKTRACK_BUNDLE(int sessionno, string ipaddress, string bundleno, DateTime lockdate, int valuer_ua_no, int unlock_ua_no)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_IPADDRESS", ipaddress);
                        objParams[2] = new SqlParameter("@P_BUNDLENO", bundleno);
                        objParams[3] = new SqlParameter("@P_LOCKDATE", lockdate);
                        objParams[4] = new SqlParameter("@P_VALUER_UA_NO", valuer_ua_no);
                        objParams[5] = new SqlParameter("@P_UNLOCK_UA_NO", unlock_ua_no);
                        //objParams[5] = new SqlParameter("@P_EXDTNO", SqlDbType.Int);
                        // objParams[6].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_EXAM_UNLOCK_BUNDLE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamNameController.Update_Locktrack_bundle-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AssignBundleToFaculty(int sessionno, int courseno, string bundleName, string ccode, int facultyNo, DateTime issuDate)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_BUNDLENO", bundleName);
                        objParams[3] = new SqlParameter("@P_CCODE", ccode);
                        objParams[4] = new SqlParameter("@P_FACULTYNO", facultyNo);
                        objParams[5] = new SqlParameter("@P_ISSUEDATE", issuDate);
                        //objParams[6] = new SqlParameter("@P_BUNDLETYPE", bundletype);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_UPDATE_BUNDLENO_VALUER", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamNameController.AddBundleCreation-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AssignBundleToModerator(int sessionno, int courseno, string bundleName, string ccode, int facultyNo, DateTime issuDate)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_BUNDLENO", bundleName);
                        objParams[3] = new SqlParameter("@P_CCODE", ccode);
                        objParams[4] = new SqlParameter("@P_FACULTYNO", facultyNo);
                        objParams[5] = new SqlParameter("@P_ISSUEDATE", issuDate);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_UPDATE_BUNDLENO_MODERATOR", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamNameController.AddBundleCreation-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int AssignBundleToScrutinizer(int sessionno, int courseno, string bundleName, string ccode, int facultyNo, DateTime issuDate)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("@P_BUNDLENO", bundleName);
                        objParams[3] = new SqlParameter("@P_CCODE", ccode);
                        objParams[4] = new SqlParameter("@P_FACULTYNO", facultyNo);
                        objParams[5] = new SqlParameter("@P_ISSUEDATE", issuDate);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_EXAM_UPDATE_BUNDLENO_SCRUTINIZER", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamNameController.AddBundleCreation-> " + ex.ToString());
                    }
                    return retStatus;
                }



                public string _uaims_constr { get; set; }

                public object Page { get; set; }
            }
        }
    }
}