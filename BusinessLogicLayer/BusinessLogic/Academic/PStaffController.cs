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
            public partial class PStaffController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>
                string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                //modified by reena on 21_10_16

                public PStaffController()
                { }
                //public DataSet GetStaffList(int deptno)
                public DataSet GetStaffList()   //modified by reena on 21_10_16
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[0];
                        //{ 
                        //    new SqlParameter("@P_DEPTNO", deptno)
                        //};

                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACAD_GET_STAFF", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.GetStaffList() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetStaffDetails(int staffNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_STAFF_NO", staffNo)
                };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACAD_RET_STAFF_BY_STAFFNO", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.GetStaffDetails() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
                //modified by reena on 21/10/16
                ////public int AddStaff(PStaff objPStaff)
                ////{
                ////    int retStatus = Convert.ToInt32(CustomStatus.Others);
                ////    int status;
                ////    try
                ////    {
                ////        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                ////        SqlParameter[] sqlParams = new SqlParameter[] 
                ////{
                ////    new SqlParameter("@P_STAFF_NO", objPStaff.staffno ),
                ////    new SqlParameter("@P_STAFF_NAME", objPStaff.PStaffName),
                ////    new SqlParameter("@P_STAFF_ADDRESS", objPStaff.PStaffAddress),
                ////    new SqlParameter("@P_CONTACTNO", objPStaff.Contactno),
                ////    new SqlParameter("@P_EMAIL_ID", objPStaff.Emailid),
                ////    new SqlParameter("@P_QUALIFICATION", objPStaff.Qualification),
                ////    new SqlParameter("@P_INTERNAL_EXTERNAL", objPStaff.Internal_External),
                ////    new SqlParameter("@P_TEACH_EXP", objPStaff.Teach_exp),
                ////    new SqlParameter("@P_COLLEGE_CODE", objPStaff.CollegeCode),
                ////   new SqlParameter("@P_OUT", SqlDbType.Int),
                ////};
                ////        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                ////        object ret = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACAD_INS_STAFF", sqlParams, true);
                ////        status = Convert.ToInt16(ret);

                ////        if (ret != null)
                ////        {
                ////            retStatus = status;
                ////        }
                ////        else
                ////        {
                ////            retStatus = -99;
                ////        }

                ////        return retStatus;
                ////    }
                ////    catch (Exception ex)
                ////    {
                ////        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.AddStaff() --> " + ex.Message + " " + ex.StackTrace);
                ////    }
                ////    // return ret;
                ////}

                public int AddStaff(PStaff objPStaff)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    int status;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_STAFF_NO", objPStaff.staffno ),
                    new SqlParameter("@P_STAFF_NAME", objPStaff.PStaffName),
                    new SqlParameter("@P_STAFF_ADDRESS", objPStaff.PStaffAddress),
                    new SqlParameter("@P_CONTACTNO", objPStaff.Contactno),
                    new SqlParameter("@P_EMAIL_ID", objPStaff.Emailid),
                    new SqlParameter("@P_QUALIFICATION", objPStaff.Qualification),
                    new SqlParameter("@P_UA_NO ", objPStaff.Uno),
                    new SqlParameter("@P_INTERNAL_EXTERNAL", objPStaff.Internal_External),
                    new SqlParameter("@P_TEACH_EXP", objPStaff.Teach_exp),

                    

                    new SqlParameter("@P_COLLEGE_CODE", objPStaff.CollegeCode),

                   new SqlParameter("@P_OUT", SqlDbType.Int),
                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACAD_INS_STAFF", sqlParams, true);
                        status = Convert.ToInt16(ret);

                        if (ret != null)
                        {
                            retStatus = status;
                        }
                        else
                        {
                            retStatus = -99;
                        }

                        return retStatus;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.AddStaff() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    // return ret;
                }

                public int AddNewInternalStaff(int ua_no)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                            {
                                new SqlParameter("@P_UA_NO",ua_no)
                            };
                        object ret = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACAD_INS_INTERNAL_NEW_STAFF", sqlParams, true);
                        status = (int)ret;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.AddNewInternalStaff() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }
                //added by reena on 15_10_16
                // MODIFIED BY SHUBHAM ON 28/02/2023 
                public int AddStaffPreference(int staffno, string ccode, int ps_mod, int SessioNO, int collegeid, int courseno)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_STAFFNO", staffno),
                    new SqlParameter("@P_COURSENO", courseno),
                    new SqlParameter("@P_CCODE", ccode),
                    new SqlParameter("@P_PS_MOD", ps_mod),
                    new SqlParameter("@P_SESSIONNO",SessioNO),
                    new SqlParameter("@P_COLLEGEID",collegeid)
                };

                        object ret = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACAD_INS_PAPERSET_PREFERENCE", sqlParams, true);

                        status = (int)ret;

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.AddStaffPreference() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }



                public int DeleteStaffPreference(int staffno, string ccode, int ps_mod)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_STAFFNO", staffno),
                    //new SqlParameter("@P_COURSENO", courseno),
                    new SqlParameter("@P_CCODE", ccode),
                    new SqlParameter("@P_PS_MOD", ps_mod)
                };

                        object ret = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACAD_DEL_PAPERSET_PREFERENCE", sqlParams, true);

                        status = (int)ret;

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.AddStaffPreference() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public int UpdateStaff(PStaff objPStaff)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_STAFFNO", objPStaff.PStaffNo),
                    new SqlParameter("@P_STAFF_NAME", objPStaff.PStaffName),
                    new SqlParameter("@P_STAFF_ADDRESS", objPStaff.PStaffAddress),
                    new SqlParameter("@P_CONTACTNO", objPStaff.Contactno),
                    new SqlParameter("@P_EMAIL_ID", objPStaff.Emailid),
                    new SqlParameter("@P_INTERNAL_EXTERNAL", objPStaff.Internal_External),
                    new SqlParameter("@P_COLLEGE_CODE", objPStaff.CollegeCode),
                };

                        object ret = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACAD_UPD_STAFF", sqlParams, false);

                        if (ret != null)
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.UpdateStaff() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                //INSTITUTE
                //=============
                public int AddInstitute(string code, string name, string add1, string add2, string add3, string contact_no, string col_code)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                    {
                        new SqlParameter("@P_INSTITUTE_CODE", code),
                        new SqlParameter("@P_INSTITUTE_NAME", name),
                        new SqlParameter("@P_INSTITUTE_ADDRESS1", add1),
                        new SqlParameter("@P_INSTITUTE_ADDRESS2", add2),
                        new SqlParameter("@P_INSTITUTE_ADDRESS3", add3),
                        new SqlParameter("@P_CONTACTNO", contact_no),
                        new SqlParameter("@P_COLLEGE_CODE", col_code),
                        new SqlParameter("@P_OUT", SqlDbType.Int),
                    };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACAD_INS_INSTITUTE", sqlParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            status = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.AddStaff() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public int UpdateInstitute(int inst_no, string code, string name, string add1, string add2, string add3, string contact_no)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                    {
                        new SqlParameter("@P_INSTITUTE_NO", inst_no),
                        new SqlParameter("@P_INSTITUTE_CODE", code),
                        new SqlParameter("@P_INSTITUTE_NAME", name),
                        new SqlParameter("@P_INSTITUTE_ADDRESS1", add1),
                        new SqlParameter("@P_INSTITUTE_ADDRESS2", add2),
                        new SqlParameter("@P_INSTITUTE_ADDRESS3", add3),
                        new SqlParameter("@P_CONTACTNO", contact_no),
                        new SqlParameter("@P_OUT", SqlDbType.Int),
                    };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACAD_UPD_INSTITUTE", sqlParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            status = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.AddStaff() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public int DeleteStaff(int staffNo)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_STAFFNO", staffNo)
                };
                        object ret = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACAD_DEL_STAFF", sqlParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            status = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            status = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.DeleteStaff() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }



                public int AddSupervisorName(PStaff objSupr)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_SUPERVISORNAME",objSupr.SupervisorName),
                    new SqlParameter("@P_DEPTNO", objSupr.DeptNo),
                    new SqlParameter("@P_TYPE", objSupr.Type),
                    new SqlParameter("@P_TYPENAME", objSupr.TypeName),
                    new SqlParameter("@P_COLLEGE_CODE", objSupr.CollegeCode),
                    new SqlParameter("@P_SUPERVISORNO", status)
                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SUPERVISORNAME_INSERT", sqlParams, true);

                        if (obj != null && obj.ToString() != "-99")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PStaffController.AddSupervisorName() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }


                public SqlDataReader GetSupervisorNo(int SupervisorNo)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_SUPERVISORNO", SupervisorNo) };

                        dr = objSQLHelper.ExecuteReaderSP("PKG_ACAD_SUPERVISOR_GET_BY_NO", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PStaffController.GetSupervisorNo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return dr;
                }

                public int UpdateSupervisorName(PStaff objSupr)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {                    
                    new SqlParameter("@P_SUPERVISORNAME",objSupr.SupervisorName),
                    new SqlParameter("@P_DEPTNO", objSupr.DeptNo),
                    new SqlParameter("@P_TYPE", objSupr.Type),
                    new SqlParameter("@P_TYPENAME", objSupr.TypeName),
                    new SqlParameter("@P_COLLEGE_CODE", objSupr.CollegeCode),
                    new SqlParameter("@P_SUPERVISORNO", objSupr.SupervisorNo)
                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SUPERVISORNAME_UPDATE", sqlParams, true);

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PStaffController.UpdateSupervisorName() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }


                public DataSet GetAllSupervisorName()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_SUPERVISORNAME_GET_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PStaffController.GetAllSupervisorName() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public int AddDRCDetail(PStaff objSupr)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_DRCNAME",objSupr.DRCName),
                            new SqlParameter("@P_DEPTNO", objSupr.DeptNo),
                            new SqlParameter("@P_COLLEGE_CODE", objSupr.CollegeCode),
                            new SqlParameter("@P_DRCNO", status)
                        };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DRCNAME_INSERT", sqlParams, true);

                        if (obj != null && obj.ToString() != "-99")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PStaffController.AddSupervisorName() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public int UpdateDRCName(PStaff objSupr)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                {                    
                    new SqlParameter("@P_DRCNAME",objSupr.DRCName),
                    new SqlParameter("@P_DEPTNO", objSupr.DeptNo),
                    new SqlParameter("@P_COLLEGE_CODE", objSupr.CollegeCode),
                    new SqlParameter("@P_DRCNO", objSupr.DRCNo)
                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DRCNAME_UPDATE", sqlParams, true);

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PStaffController.UpdateDRCName() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public SqlDataReader GetDRCNo(int DrcNo)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_DRCNO", DrcNo) };

                        dr = objSQLHelper.ExecuteReaderSP("PKG_ACAD_DRC_GET_BY_NO", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PStaffController.GetSupervisorNo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return dr;
                }

                public DataSet GetAllDRCName()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_DRCNAME_GET_ALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PStaffController.GetAllSupervisorName() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                //added by reena on 11_7_16
                //Modified by Shubham on 27/02/2023
                public DataSet GetPaperSetterStudCount1(int sessionId, int CollegeId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_SESSIONNO", sessionId),
                            new SqlParameter("@P_COLLEGEID", CollegeId)
                        };
                        ds = objDataAccess.ExecuteDataSetSP("ACAD_PAPER_SETTER_NAME_BOS_STUDCOUNT", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.GetPaperSetterStudCount() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                //added by reena on 11_7_16
                //Modified by Shubham on 27/02/2023
                public DataSet GetPaperSetterNotDone(int sessionId, int Schemeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_SESSIONNO", sessionId),
                            new SqlParameter("@P_SCHEMENO", Schemeno)
                        };
                        ds = objDataAccess.ExecuteDataSetSP("ACAD_PAPER_SETTER_NOT_ALLOT_BOS", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.GetPaperSetterStudCount() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetInternalStaffList()   // Added by Shubham On 27/02/2023
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[0];
                        //{
                        //    new SqlParameter("@P_DEPTNO", deptno);
                        //};

                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACAD_GET_INTERNAL_STAFF", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.GetStaffList() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetExternalStaffList()   // Added by Shubham On 27/02/2023
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[0];
                        //{
                        //    new SqlParameter("@P_DEPTNO", deptno);
                        //};

                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACAD_GET_EXTERNAL_STAFF", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.GetStaffList() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                //ADDED by Shubham on 02/03/2023
                public DataSet PaperSetFacultyListDean(int sessionId, int Schemeno, string ccode, int semester)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_SESSIONID", sessionId),
                            //new SqlParameter("@P_BOS_DEPTNO", Deptno),
                            new SqlParameter("@P_SCHEMENO", Schemeno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_SEMESTERNO", semester)
                        };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACAD_PAPERSET_REPORT_FACULTY_ACCEPTED", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.GetPaperSetterStudCount() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }


                //ADDED by Shubham on 01/03/2023
                public DataSet PaperSetReportFacultyAlloted(int sessionId, int Schemeno, string ccode, int semester)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_SESSIONID", sessionId),
                            //new SqlParameter("@P_BOS_DEPTNO", Deptno),
                             new SqlParameter("@P_SCHEMENO", Schemeno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_SEMESTERNO", semester)
                        };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACAD_PAPERSET_REPORT_FACULTY_ALLOTTED", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.GetPaperSetterStudCount() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                //ADDED by Shubham on 01/03/2023
                public DataSet FacultyNotSet(int sessionId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_SESSIONID", sessionId)
                        };
                        ds = objDataAccess.ExecuteDataSetSP("ACAD_FACULTY_NOT_ASSIGN_BOS", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.GetPaperSetterStudCount() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                //ADDED by Shubham on 02/03/2023
                public DataSet PaperSetIssueLetter(int sessionId, int Schemeno, string ccode, int semester, string Staffno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                        { 
                            new SqlParameter("@P_SESSIONID", sessionId),
                            //new SqlParameter("@P_BOS_DEPTNO", Deptno),
                            new SqlParameter("@P_SCHEMENO", Schemeno),
                            new SqlParameter("@P_CCODE", ccode),
                            new SqlParameter("@P_SEMESTERNO", semester),
                            new SqlParameter("@P_STAFFNO", Staffno)
                        };
                        ds = objDataAccess.ExecuteDataSetSP("PKG_ACAD_PAPERSET_REPORT_ISSUELETTER", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.GetPaperSetterStudCount() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public int DeleteCourseBal(int sessionNo, int bos_deptno, int semesterno, string ccode, int CollegeId)
                {
                    int retun_status = Convert.ToInt16(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
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

                // added by shubham On 20/02/23
                public int UpdateCourseBal(int sessionNo, int bos_deptno, int semesterno, string ccode, int collegeid)
                {
                    int retun_status = Convert.ToInt16(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
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

                //CHANGES DONE ON 20/02/2023 BY SHUBHAM
                public int UnlockPaperset(int SessionNo, int deptno, int sem, int CollegeId)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add New ExamName
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", SessionNo);
                        objParams[1] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[2] = new SqlParameter("@P_SEMESTERNO", sem);
                        objParams[3] = new SqlParameter("@P_COLLEGEID", CollegeId);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_UNLOCK_PAPER_SET_DETAILS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ExamNameController.Add-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //CHANGES DONE ON 01/08/2023 BY SHUBHAM
                public int AddStaff(PStaff objPStaff, int Bankno, int deptno, string accno, string Nameofinsit, string ifsc, string panno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    int status;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[] 
                {
                    new SqlParameter("@P_STAFF_NO", objPStaff.staffno ),
                    new SqlParameter("@P_STAFF_NAME", objPStaff.PStaffName),
                    new SqlParameter("@P_STAFF_ADDRESS", objPStaff.PStaffAddress),
                    new SqlParameter("@P_CONTACTNO", objPStaff.Contactno),
                    new SqlParameter("@P_EMAIL_ID", objPStaff.Emailid),
                    new SqlParameter("@P_QUALIFICATION", objPStaff.Qualification),
                    new SqlParameter("@P_UA_NO ", objPStaff.Uno),
                    new SqlParameter("@P_INTERNAL_EXTERNAL", objPStaff.Internal_External),
                    new SqlParameter("@P_TEACH_EXP", objPStaff.Teach_exp),
                    new SqlParameter("@P_COLLEGE_CODE", objPStaff.CollegeCode),
                    new SqlParameter("@P_BANKNO", Bankno),
                    new SqlParameter("@P_ACCNO", accno),
                    new SqlParameter("@P_NAME_OF_INST", Nameofinsit),
                    new SqlParameter("@P_IFSC", ifsc),
                    new SqlParameter("@P_PANNO", panno),
                    new SqlParameter("@P_DEPTNO", deptno),
                   new SqlParameter("@P_OUT", SqlDbType.Int),
                };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                        object ret = (Int32)objDataAccess.ExecuteNonQuerySP("PKG_ACAD_INS_STAFF", sqlParams, true);
                        status = Convert.ToInt16(ret);

                        if (ret != null)
                        {
                            retStatus = status;
                        }
                        else
                        {
                            retStatus = -99;
                        }

                        return retStatus;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.PStaffController.AddStaff() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    // return ret;
                }

            }

        }//END: BusinessLayer.BusinessLogic

    }//END: UAIMS  

}//END: IITMS
