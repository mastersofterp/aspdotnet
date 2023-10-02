using IITMS;
using IITMS.SQLServer.SQLDAL;
//using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class DefineTotalCreditController
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


                public int AddCredit(DefineCreditLimit dci, int gropuid, int gropuflag)
                {
                    int status = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add
                        objParams = new SqlParameter[26];
                        objParams[0] = new SqlParameter("@P_SCHEMENO", dci.SCHEMENO);
                        objParams[1] = new SqlParameter("@P_SCHEMENAME", dci.SCHEMENAME);
                        objParams[2] = new SqlParameter("@P_STUDENTTYPE", dci.STUDENT_TYPE);
                        objParams[3] = new SqlParameter("@P_FROMRANGE", dci.FROM_CGPA);
                        objParams[4] = new SqlParameter("@P_TORANGE", dci.TO_CGPA);
                        objParams[5] = new SqlParameter("@P_ADDITIONAL_COURSE", dci.ADDITIONAL_COURSE);
                        objParams[6] = new SqlParameter("@P_ADM_TYPE", dci.ADM_TYPE);
                        objParams[7] = new SqlParameter("@P_DEGREE_TYPE", dci.DEGREE_TYPE);
                        objParams[8] = new SqlParameter("@P_FROM_CREDIT", dci.FROM_CREDIT);
                        objParams[9] = new SqlParameter("@P_TO_CREDIT", dci.TO_CREDIT);
                        objParams[10] = new SqlParameter("@P_CORE_CREDIT", dci.Core_credit);
                        objParams[11] = new SqlParameter("@P_ELECTIVE_CREDIT", dci.Elective_credit);
                        objParams[12] = new SqlParameter("@P_GLOBAL_CREDIT", dci.Global_credit);
                        objParams[13] = new SqlParameter("@P_MAX_SCHEME_LIMIT", dci.MAX_SCHEMELIMIT);
                        objParams[14] = new SqlParameter("@P_MIN_SCHEME_LIMIT", dci.MIN_SCHEMELIMIT);
                        objParams[15] = new SqlParameter("@P_TERM", dci.TERM);
                        objParams[16] = new SqlParameter("@P_TERM_TEXT", dci.TERM_TEXT);
                        objParams[17] = new SqlParameter("@P_DEGREENO", dci.DEGREENO);
                        objParams[18] = new SqlParameter("@P_MIN_REG_CREDIT_LIMIT", dci.MIN_REG_CREDIT_LIMIT);
                        objParams[19] = new SqlParameter("@P_ELECTIVE_GROUPNO", dci.ELECTIVE_GROUPNO);
                        objParams[20] = new SqlParameter("@P_ELECTIVE_CHOISEFOR", dci.ELECTIVE_CHOISEFOR);
                        objParams[21] = new SqlParameter("@P_CREAERD_UA_NO", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                        objParams[22] = new SqlParameter("@P_IPADDRESS", System.Web.HttpContext.Current.Session["ipAddress"].ToString());
                        objParams[23] = new SqlParameter("@P_GROUPID", gropuid); //added by nehal on 16052023
                        objParams[24] = new SqlParameter("@P_GROUPFLAG", gropuflag); //added by nehal on 16052023
                        objParams[25] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[25].Direction = ParameterDirection.Output;

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_INS_SESSIONMASTER", objParams, true) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_DEFINE_TOTAL_CREDIT", objParams, true);

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DefineTotalCreditController.AddCredit-> " + ex.ToString());
                    }
                    return status;
                }


                public int UpdateCredit(DefineCreditLimit dci, int gropuid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New Registered Subject Details
                        objParams = new SqlParameter[17];

                        objParams[0] = new SqlParameter("@P_SCHEMENO", dci.SCHEMENO);
                        objParams[1] = new SqlParameter("@P_SCHEMENAME", dci.SCHEMENAME);
                        objParams[2] = new SqlParameter("@P_FROM_CREDIT", dci.FROM_CREDIT);
                        objParams[3] = new SqlParameter("@P_TO_CREDIT", dci.TO_CREDIT);
                        objParams[4] = new SqlParameter("@P_CORE_CREDIT", dci.Core_credit);
                        objParams[5] = new SqlParameter("@P_ELECTIVE_CREDIT", dci.Elective_credit);
                        objParams[6] = new SqlParameter("@P_GLOBAL_CREDIT", dci.Global_credit);
                        objParams[7] = new SqlParameter("@P_TERM", dci.TERM);
                        objParams[8] = new SqlParameter("@P_TERM_TEXT", dci.TERM_TEXT);
                        objParams[9] = new SqlParameter("@P_IDNO", dci.RECORDNO);
                        objParams[10] = new SqlParameter("@P_DEGREENO", dci.DEGREENO);
                        objParams[11] = new SqlParameter("@P_ELECTIVE_GROUPNO", dci.ELECTIVE_GROUPNO);
                        objParams[12] = new SqlParameter("@P_ELECTIVE_CHOISEFOR", dci.ELECTIVE_CHOISEFOR);
                        objParams[13] = new SqlParameter("@P_CREAERD_UA_NO", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                        objParams[14] = new SqlParameter("@P_IPADDRESS", System.Web.HttpContext.Current.Session["ipAddress"].ToString());
                        objParams[15] = new SqlParameter("@P_GROUPID", gropuid); //added by nehal
                        objParams[16] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[16].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPD_DEFINE_TOTAL_CREDIT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.UpdateCredit-> " + ex.ToString());
                    }

                    return retStatus;

                }
             
                public int LockCreditDefination()
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New Registered Subject Details
                        objParams = new SqlParameter[1];



                        objParams[0] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[0].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("LOCK_DEFINE_TOTAL_CREDIT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddRegisteredSubjects-> " + ex.ToString());
                    }

                    return retStatus;

                }


                public int AddSemPromotionDefination(DefineCreditLimit dci)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New Registered Subject Details
                        objParams = new SqlParameter[8];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", dci.SESSION);
                        objParams[1] = new SqlParameter("@P_SESSIONNAME", dci.SESSIONNAME);
                        objParams[2] = new SqlParameter("@P_DEGREENO", dci.DEGREENO);
                        objParams[3] = new SqlParameter("@P_TERM", Convert.ToInt32(dci.TERM));
                        objParams[4] = new SqlParameter("@P_MIN_CREDIT", dci.MIN_REG_CREDIT_LIMIT);
                        objParams[5] = new SqlParameter("@P_BRANCHNOS", dci.BRANCHNOS);
                        objParams[6] = new SqlParameter("@P_BRANCHNOS_TEXT", dci.BRANCHNOS_TEXT);

                        objParams[7] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SEMESTER_PROMOTION_DEFINATION", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddRegisteredSubjects-> " + ex.ToString());
                    }

                    return retStatus;

                }


                public int AddResultPublishDefination(DefineCreditLimit dci)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New Registered Subject Details
                        objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", dci.SESSION);

                        objParams[1] = new SqlParameter("@P_DEGREENO", dci.DEGREENO);
                        objParams[2] = new SqlParameter("@P_TERM", Convert.ToInt32(dci.TERM));

                        objParams[3] = new SqlParameter("@P_FLAG", dci.PUBLIISH_YES_NO);

                        objParams[4] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_RESULT_PUBLISH_DEFINATION", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddRegisteredSubjects-> " + ex.ToString());
                    }

                    return retStatus;

                }


                public int UpdateSemPromotionDefination(DefineCreditLimit dci)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New Registered Subject Details
                        objParams = new SqlParameter[9];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", dci.SESSION);
                        objParams[1] = new SqlParameter("@P_SESSIONNAME", dci.SESSIONNAME);
                        objParams[2] = new SqlParameter("@P_DEGREENO", dci.DEGREENO);
                        objParams[3] = new SqlParameter("@P_TERM", Convert.ToInt32(dci.TERM));
                        objParams[4] = new SqlParameter("@P_MIN_CREDIT", dci.MIN_REG_CREDIT_LIMIT);
                        objParams[5] = new SqlParameter("@P_BRANCHNOS", dci.BRANCHNOS);
                        objParams[6] = new SqlParameter("@P_BRANCHNOS_TEXT", dci.BRANCHNOS_TEXT);
                        objParams[7] = new SqlParameter("@P_RECORDNO", dci.RECORDNO);

                        objParams[8] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPD_SEMESTER_PROMOTION_DEFINATION", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddRegisteredSubjects-> " + ex.ToString());
                    }

                    return retStatus;

                }

                public int DeleteCredit(DefineCreditLimit dci)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_IDNO", dci.RECORDNO);
                        objParams[1] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_DEL_DEFINE_TOTAL_CREDIT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddRegisteredSubjects-> " + ex.ToString());
                    }

                    return retStatus;
                }

                public int UpdateResultPublishDefination(DefineCreditLimit dci)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[6];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", dci.SESSION);

                        objParams[1] = new SqlParameter("@P_DEGREENO", dci.DEGREENO);
                        objParams[2] = new SqlParameter("@P_TERM", Convert.ToInt32(dci.TERM));
                        objParams[3] = new SqlParameter("@P_FLAG", dci.PUBLIISH_YES_NO);
                        objParams[4] = new SqlParameter("@P_PUBNO", dci.RECORDNO);

                        objParams[5] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPD_RESULT_PUBLISH_DEFINATION", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.AddRegisteredSubjects-> " + ex.ToString());
                    }

                    return retStatus;

                }

                //added by nehal on 16052023
                public DataSet GetCreditDataDetailsEdit(int groupid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[1];
                        sqlParams[0] = new SqlParameter("@P_GROUPID", groupid);
                        ds = objDataAccess.ExecuteDataSetSP("PKG_GET_ACD_DEFINE_TOTAL_CREDIT_DETAILS_EDIT", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.StudentRegistration.GetCreditDataDetailsEdit() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                //Added by Vinay Mishra on 26/06/2023 for Credit Definition for Course Registration
                public DataSet GetCreditDefinitionDetails()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_REPORT_STU_CREDIT_DEFINITION_FOR_COURSE_REGISTRATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetCreditDefinitionDetails-> " + ex.ToString());
                    }

                    return ds;
                }



                public int AddCreditModified(DefineCreditLimit dci, int gropuid,int collegeID)
                {
                    int status = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add
                        objParams = new SqlParameter[25];
                        objParams[0] = new SqlParameter("@P_SCHEMENOS", dci.Schemenos);
                        objParams[1] = new SqlParameter("@P_STUDENTTYPE", dci.STUDENT_TYPE);
                        objParams[2] = new SqlParameter("@P_FROMRANGE", dci.FROM_CGPA);
                        objParams[3] = new SqlParameter("@P_TORANGE", dci.TO_CGPA);
                        objParams[4] = new SqlParameter("@P_ADDITIONAL_COURSE", dci.ADDITIONAL_COURSE);
                        objParams[5] = new SqlParameter("@P_ADM_TYPE", dci.ADM_TYPE);
                        objParams[6] = new SqlParameter("@P_DEGREE_TYPE", dci.DEGREE_TYPE);
                        objParams[7] = new SqlParameter("@P_FROM_CREDIT", dci.FROM_CREDIT);
                        objParams[8] = new SqlParameter("@P_TO_CREDIT", dci.TO_CREDIT);
                        objParams[9] = new SqlParameter("@P_CORE_CREDIT", dci.Core_credit);
                        objParams[10] = new SqlParameter("@P_ELECTIVE_CREDIT", dci.Elective_credit);
                        objParams[11] = new SqlParameter("@P_GLOBAL_CREDIT", dci.Global_credit);
                        objParams[12] = new SqlParameter("@P_MAX_SCHEME_LIMIT", dci.MAX_SCHEMELIMIT);
                        objParams[13] = new SqlParameter("@P_MIN_SCHEME_LIMIT", dci.MIN_SCHEMELIMIT);
                        objParams[14] = new SqlParameter("@P_TERM", dci.TERM);
                        objParams[15] = new SqlParameter("@P_DEGREENO", dci.DEGREENO);
                        objParams[16] = new SqlParameter("@P_MIN_REG_CREDIT_LIMIT", dci.MIN_REG_CREDIT_LIMIT);
                        objParams[17] = new SqlParameter("@P_ELECTIVE_GROUPNO", dci.Electivegroupnos);
                        objParams[18] = new SqlParameter("@P_ELECTIVE_CHOISEFOR", dci.ELECTIVE_CHOISEFOR);
                        objParams[19] = new SqlParameter("@P_CREAERD_UA_NO", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                        objParams[20] = new SqlParameter("@P_IPADDRESS", System.Web.HttpContext.Current.Session["ipAddress"].ToString());
                        objParams[21] = new SqlParameter("@P_OVERLOAD_CREDIT", dci.Overload_credit); //added by nehal on 16052023
                        objParams[22] = new SqlParameter("@P_GROUPID", gropuid); //added by nehal on 16052023
                        objParams[23] = new SqlParameter("@P_COLLEGE_ID", collegeID); //added by Shailendra K. on 30.09.2023
                        objParams[24] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[24].Direction = ParameterDirection.Output;

                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_INS_SESSIONMASTER", objParams, true) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_DEFINE_TOTAL_CREDIT_MODIFIED", objParams, true);

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DefineTotalCreditController.AddCredit-> " + ex.ToString());
                    }
                    return status;
                }
            }
        }
    }
}
