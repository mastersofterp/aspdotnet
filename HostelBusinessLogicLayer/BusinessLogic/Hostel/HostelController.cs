//CREATED BY - IFTAKHAR KHAN
//CREATED DATE - 24-APRIL-2014
//PURPOSE - THIS CLASS IS USED TO ADD/UPDATE/DELETE COMMAND OF HOSTEL MODULE
//APPORVE BY -PAWAN M 

using System;
using System.Data;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {

            public class HostelController
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                /// <summary>
                /// Add hostel detail according to theri hostel type
                /// </summary>
                /// <param name="hosName"></param>
                /// <param name="hosAddress"></param>
                /// <param name="hosType"></param>
                /// <param name="colCode"></param>
                /// <returns></returns>
                public int AddHostelDetail(int Degreeno, string yearno, string categoryno, string hosName, string hosAddress, int hosType, int colCode)
                {
                    int retstatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] param = new SqlParameter[]
                            {
                                new SqlParameter("@P_DEGREENO",Degreeno),
                                new SqlParameter("@P_YEARNO",yearno),
                                new SqlParameter("@P_CATEGORYNO",categoryno),
                                new SqlParameter("@P_HOS_ADDRESS",hosAddress),
                                new SqlParameter("@P_HOS_NAME",hosName),
                                new SqlParameter("@P_HOS_TYPE",hosType),
                                new SqlParameter("@P_COLLEGE_CODE",colCode),
                                new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),
                            };
                        object ret = objSqlHelper.ExecuteNonQuerySP("PKG_INS_HOSTEL_ENTRY", param, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HostelController.AddHostelDetail-> " + ex.ToString());
                    }

                    return retstatus;

                }

                /// <summary>
                /// update hostel detail through hostel ID
                /// </summary>
                /// <param name="hosName"></param>
                /// <param name="hosAddress"></param>
                /// <param name="hosType"></param>
                /// <param name="hostelno"></param>
                /// <returns></returns>
                public int UpdateHosteDetail(int Degreeno, string yearno, string categoryno, string hosName, string hosAddress, int hosType, int hostelno)
                {
                    int retstatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] param = new SqlParameter[]
                        {   
                            new SqlParameter("@P_DEGREENO",Degreeno),
                            new SqlParameter("@P_YEARNO",yearno),
                            new SqlParameter("@P_CATEGORYNO",categoryno),
                            new SqlParameter("@P_HOS_ADDRESS",hosAddress),
                            new SqlParameter("@P_HOS_NAME",hosName),
                            new SqlParameter("@P_HOS_TYPE",hosType),
                            new SqlParameter("@P_HOSTEL_NO",hostelno),
                        };
                        object ret = objSqlHelper.ExecuteNonQuerySP("PKG_UPDATE_HOSTEL_ENTRY", param, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CreateHostelController.AddBlockInfo-> " + ex.ToString());
                    }

                    return retstatus;

                }

                /// <summary>
                /// used to Delete record of hostel entry
                /// </summary>
                /// <param name="hostelno"></param>
                /// <returns></returns>
                public int DeleteHostelDetail(int hostelno)
                {
                    int restatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] param = new SqlParameter[]
                            {
                                new SqlParameter("@P_HOSTEL_NO",hostelno),
                                new SqlParameter("@P_OUT",restatus),
                            };
                        param[param.Length - 1].Direction = ParameterDirection.InputOutput;
                        object ret = objSqlHelper.ExecuteNonQuerySP("PKG_DELETE_HOSTEL_ENTRY", param, true);
                        restatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        restatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BUSSINESSLAYER.BUSINESSLOGIC.CREATEHOSTELCONTROLLER->" + ex.ToString());
                    }
                    return restatus;
                }
                public int UpdatePaymenttypeofStudents(string idnos, string ptypes)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] param = new SqlParameter[]
                            {                         
                            new SqlParameter("@P_IDNOS", idnos),
                            new SqlParameter("@P_PTYPE", ptypes),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)                        
                            };
                        param[param.Length - 1].Direction = ParameterDirection.Output;
                        object ret = objSqlHelper.ExecuteNonQuerySP("PKG_HOSTEL_UPDATE_STUDENT_PAYMENT_TYPE", param, false);
                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdatePaymenttypeofStudents-> " + ex.ToString());
                    }
                    return retStatus;
                }



                public DataSet GetStudentsForUpdatePaymentType(int Sessionno, int Degreeno, int Branchno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] param = new SqlParameter[]
                            {
                                new SqlParameter("@P_SESSIONNO", Sessionno),
                                new SqlParameter("@P_DEGREENO", Degreeno),
                                new SqlParameter("@P_BRANCHNO", Branchno),
                            };
                        ds = objSQLHelper.ExecuteDataSetSP("GET_STUDENTS_FOR_UPDATE_PAYMENT_TYPE", param);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HostelController.GetStudentsForUpdatePaymentType() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
                public DataSet GetStudentSearchForHostelIdentityCard(int hostelSessionNo, int hostelNo, int blockNo, int floorNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_HOSTEL_SESSION_NO", hostelSessionNo);
                        objParams[1] = new SqlParameter("@P_HOSTEL_NO", hostelNo);
                        objParams[2] = new SqlParameter("@P_BLOCK_NO", blockNo);
                        objParams[3] = new SqlParameter("@P_FLOOR_NO", floorNo);
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_HOSTEL_STUDENT_SEARCH_IDENTITY_CARD", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HostelController.GetSTudentSearchForHostelIdentityCard-> " + ex.ToString());
                    }
                    return ds;
                }
                public int PrepareData(int hostelsessionno, int examsessionno, int hbno, int semesterno, string degreeno, int overwrite, string hostelno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] param = new SqlParameter[]
                            {                         
                            new SqlParameter("@P_HOSTELSESSIONNO",hostelsessionno ),
                            new SqlParameter("@P_EXAMSESSIONNO",examsessionno),
                            new SqlParameter("@P_HBNO", hbno),
                            new SqlParameter("@P_SEMESTERNO",  semesterno),
                            new SqlParameter("@P_DEGREENO", degreeno),
                            new SqlParameter("@P_ISOVERWRITE ", overwrite),
                            new SqlParameter("@P_HOSTELNO ", hostelno),
                            new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)                        
                            };
                        param[param.Length - 1].Direction = ParameterDirection.Output;
                        object ret = objSqlHelper.ExecuteNonQuerySP("PKG_HOSTEL_PREPAREDATA", param, true);
                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.PrepareData-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int Insertstudentrank(int hostelsessionno, int examsessionno, int hbno, int semesterno, string degreeno, int overwrite, string hostelno, int idno, int modifyrank, int rank, decimal cgpa, decimal sgpa)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] param = new SqlParameter[]
                            {                         
                            new SqlParameter("@P_HOSTELSESSIONNO",hostelsessionno ),
                            new SqlParameter("@P_EXAMSESSIONNO",examsessionno),
                            new SqlParameter("@P_HBNO", hbno),
                            new SqlParameter("@P_SEMESTERNO",  semesterno),
                            new SqlParameter("@P_DEGREENO", degreeno),
                            new SqlParameter("@P_ISOVERWRITE ", overwrite),
                            new SqlParameter("@P_HOSTELNO ", hostelno),
                            new SqlParameter("@P_IDNO ", idno),
                            new SqlParameter("@P_MODIFYRANK ", modifyrank),
                            new SqlParameter("@P_RANK ",rank),
                            new SqlParameter("@P_CGPA ",cgpa),
                            new SqlParameter("@P_SGPA ",sgpa ),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)                        
                            };
                        param[param.Length - 1].Direction = ParameterDirection.Output;
                        object ret = objSqlHelper.ExecuteNonQuerySP("PKG_HOSTEL_PREPARE_RANKLIST", param, true);
                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.PrepareData-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet Getstudentrank(int hostelsessionno, int examsessionno, int hbno, int semesterno, string degreeno, int overwrite, string hostelno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] param = new SqlParameter[]
                            {                         
                            new SqlParameter("@P_HOSTELSESSIONNO",hostelsessionno ),
                            new SqlParameter("@P_EXAMSESSIONNO",examsessionno),
                            new SqlParameter("@P_HBNO", hbno),
                            new SqlParameter("@P_SEMESTERNO",  semesterno),
                            new SqlParameter("@P_DEGREENO", degreeno),
                            new SqlParameter("@P_HOSTELNO ", hostelno)
                            };
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_HOSTEL_ALLOTMENT_LIST", param);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.PrepareData-> " + ex.ToString());
                    }
                    return ds;
                }
                // Below function blockno added by Saurabh L on 02 March 2023
                public DataSet Show_hostelroom(int hostelsessionno, int hbno, string semesterno, string degreeno, string gender, string familer, int  blockno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[7];

                        objParams[0] = new SqlParameter("@P_HOSTEL_SESSION_NO", hostelsessionno);
                        objParams[1] = new SqlParameter("@P_HBNO", hbno);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_GENDER", gender);
                        objParams[5] = new SqlParameter("@P_FAMILER", familer);
                        objParams[6] = new SqlParameter("@P_BLOCKNO", blockno);
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_SHOW_HOSTEL_ROOMS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HostelController.GetSTudentSearchForHostelIdentityCard-> " + ex.ToString());
                    }
                    return ds;
                }
                public int Insertshow_hostelroom(int hostelsessionno, int hbno, int semesterno, int degreeno, string gender, string rooms, int familer, int OrganizationId)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] param = new SqlParameter[]
                            {                         
                            new SqlParameter("@P_HOSTELSESSIONNO",hostelsessionno ),
                            new SqlParameter("@P_HBNO", hbno),
                            new SqlParameter("@P_DEGREENO", degreeno),
                            new SqlParameter("@P_SEMESTERNO",  semesterno),
                            new SqlParameter("@P_GENDER ", gender),
                            new SqlParameter("@P_ROOMS ",rooms),
                            new SqlParameter("@P_FAMILER ",familer),
                            new SqlParameter("@P_ORGANIZATION_ID ",OrganizationId),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)                        
                            };
                        param[param.Length - 1].Direction = ParameterDirection.Output;
                        object ret = objSqlHelper.ExecuteNonQuerySP("PKG_INSERT_SHOW_HOSTELROOM_MASTER", param, true);
                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.PrepareData-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //STUDENT MESS APPLY AND ALLOTMENT 
                public DataSet Show_allotmess(int hostelsessionno, int hbno, int messno, int semesterno, int degreeno, string fromregno, string toregno, int allotflag)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[8];

                        objParams[0] = new SqlParameter("@P_HOSTEL_SESSION_NO", hostelsessionno);
                        objParams[1] = new SqlParameter("@P_HBNO", hbno);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[4] = new SqlParameter("@P_MESSNO ", messno);
                        objParams[5] = new SqlParameter("@P_FROMREGNO", fromregno);
                        objParams[6] = new SqlParameter("@P_TOREGNO", toregno);
                        objParams[7] = new SqlParameter("@P_ALLOTFLAG", allotflag);
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_SHOW_HOSTEL_MESS", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HostelController.GetSTudentSearchForHostelIdentityCard-> " + ex.ToString());
                    }
                    return ds;
                }
                public int UpdateMessofStudents(string idnos, int messno, int hostelsessiono)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] param = new SqlParameter[]
                            {                         
                            new SqlParameter("@P_IDNOS", idnos),
                            new SqlParameter("@P_MESSNO", messno),
                            new SqlParameter("@P_HOSTEL_SESSION_NO", hostelsessiono),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)                        
                            };
                        param[param.Length - 1].Direction = ParameterDirection.Output;
                        object ret = objSqlHelper.ExecuteNonQuerySP("PKG_HOSTEL_UPDATE_STUDENT_MESS", param, false);
                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdatePaymenttypeofStudents-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int InsertMessRequest(int idno, int messno, int hostelsessiono, int hostelNo, int ua_no, string remark, string updatemonth)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] param = new SqlParameter[]
                            {                         
                            new SqlParameter("@P_IDNO", idno),
                            new SqlParameter("@P_MESSNO", messno),
                            new SqlParameter("@P_HOSTEL_SESSION_NO", hostelsessiono),
                            new SqlParameter("@P_HOSTELNO", hostelNo),
                            new SqlParameter("@P_UANO", ua_no),
                            new SqlParameter("@P_REMARK", remark),
                            new SqlParameter("@P_UPDATE_MONTH", updatemonth ),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)                        
                            };
                        param[param.Length - 1].Direction = ParameterDirection.Output;
                        object ret = objSqlHelper.ExecuteNonQuerySP("PKG_HOSTEL_INSERT_STUDENT_MESS_REQUEST", param, false);
                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdatePaymenttypeofStudents-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public SqlDataReader Get_Hostel_Student_Detail(int idno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_IDNO", idno);
                        dr = objSQLHelper.ExecuteReaderSP("PKG_GET_HOSTEL_STUDENT_DETAIL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GetShemeSemester-> " + ex.ToString());
                    }
                    return dr;
                }
                public DataSet Get_Mess_Seat_Detail(int idno, int messno, int hostelsessiono, int hostelno, int semesterno, int degreeno, string updatemonth)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_MESSNO", messno);
                        objParams[2] = new SqlParameter("@P_HOSTELSESSIONO", hostelsessiono);
                        objParams[3] = new SqlParameter("@P_HOSTELNO", hostelno);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[5] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[6] = new SqlParameter("@P_UPDATE_MONTH", updatemonth);
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_GET_MESS_SEAT_DETAIL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.GetShemeSemester-> " + ex.ToString());
                    }
                    return ds;
                }
                public int Updateshow_hostelroom(int recordno, int started, int familer, int OrganizationId)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] param = new SqlParameter[]
                            {                         
                            new SqlParameter("@P_RECORDNO",recordno),
                            new SqlParameter("@P_STARTED",started),
                            new SqlParameter("@P_FAMILER",familer),
                            new SqlParameter("@P_ORGANIZATION_ID",OrganizationId),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)                        
                            };
                        param[param.Length - 1].Direction = ParameterDirection.Output;
                        object ret = objSqlHelper.ExecuteNonQuerySP("PKG_UPDATE_SHOW_HOSTELROOM_MASTER", param, true);
                        if (ret != null)
                            if (ret.ToString() != "-99")
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.PrepareData-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #region Hostel_pg_config methods
                // by Preeti on 22/09/2022
                public int UpdateadhostelPaymentgateway_config(int configid, int payid, string marchant_id, string accesscode, string checksumkey, string requesturl, string responseurl, int instance, int activestatus, int organi_id, int activity, string hashseq, string pageurl, int degreeNo, int collegeId)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[16];
                        objParams[0] = new SqlParameter("@P_CONFIG_ID", configid);
                        objParams[1] = new SqlParameter("@P_PAY_ID", payid);
                        objParams[2] = new SqlParameter("@P_MERCHANT_ID", marchant_id);
                        objParams[3] = new SqlParameter("@P_ACCESS_CODE", accesscode);
                        objParams[4] = new SqlParameter("@P_CHECKSUM_KEY", checksumkey);
                        objParams[5] = new SqlParameter("@P_REQUEST_URL", requesturl);
                        objParams[6] = new SqlParameter("@P_RESPONSE_URL", responseurl);
                        objParams[7] = new SqlParameter("@P_HASH_SEQ", hashseq);
                        objParams[8] = new SqlParameter("@P_PAGE_URL", pageurl);
                        objParams[9] = new SqlParameter("@P_INSTANCE", instance);
                        objParams[10] = new SqlParameter("@P_ACTIVE_STATUS", activestatus);
                        objParams[11] = new SqlParameter("@P_ORGANIZATION_ID", organi_id);
                        objParams[12] = new SqlParameter("@P_ACTIVITY_NO", activity);
                        objParams[13] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[14] = new SqlParameter("@P_COLLEGE_ID", collegeId);
                        objParams[15] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[15].Direction = System.Data.ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_UPDATE_PG_PAYMENT_GATEWAY_CONFIG", objParams, true);

                        if (obj != null && obj.ToString() == "2")
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else if (obj != null && obj.ToString() == "-99")
                            status = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.UpdateadmFeeDeduction() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }

                public int InserthostelPaymentgateway_config(int payid, string marchant_id, string accesscode, string checksumkey, string requesturl, string responseurl, int instance, int activestatus, int organi_id, int activity, string hashseq, string pageurl, int degreeNo, int collegeId)
                {
                    int status = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_PAY_ID", payid);
                        objParams[1] = new SqlParameter("@P_MERCHANT_ID", marchant_id);
                        objParams[2] = new SqlParameter("@P_ACCESS_CODE", accesscode);
                        objParams[3] = new SqlParameter("@P_CHECKSUM_KEY", checksumkey);
                        objParams[4] = new SqlParameter("@P_REQUEST_URL", requesturl);
                        objParams[5] = new SqlParameter("@P_RESPONSE_URL", responseurl);
                        objParams[6] = new SqlParameter("@P_HASH_SEQ", hashseq);
                        objParams[7] = new SqlParameter("@P_PAGE_URL", pageurl);
                        objParams[8] = new SqlParameter("@P_INSTANCE", instance);
                        objParams[9] = new SqlParameter("@P_ACTIVE_STATUS", activestatus);
                        objParams[10] = new SqlParameter("@P_ORGANIZATION_ID", organi_id);
                        objParams[11] = new SqlParameter("@P_ACTIVITY_NO", activity);
                        objParams[12] = new SqlParameter("@P_DEGREENO", degreeNo);
                        objParams[13] = new SqlParameter("@P_COLLEGE_ID", collegeId);
                        objParams[14] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[14].Direction = System.Data.ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_INSERT_PG_PAYMENT_GATEWAY_CONFIG", objParams, true);

                        if (obj != null && obj.ToString() == "1")
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (obj != null && obj.ToString() == "-99")
                            status = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32((CustomStatus.Error));
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.InsertPaymentgateway_config-> " + ex.ToString());
                    }
                    return status;
                }

                public DataSet GetData_HOSTEL_Payment_gateway_config1()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper ObjSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] Sqlparam = new SqlParameter[0];
                        ds = ObjSqlHelper.ExecuteDataSetSP("PKG_HOSTEL_GETDATA_PG_PAYMENT_GATEWAY_CONFIG", Sqlparam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ConfigController.GetData_Payment_gateway_config() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }

                public DataSet GetData_Hostel_Payment_gateway_config(int configid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper ObjSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] Sqlparam = new SqlParameter[1];
                        Sqlparam[0] = new SqlParameter("@P_CONFIG_ID", configid);
                        ds = ObjSqlHelper.ExecuteDataSetSP("PKG_HOSTEL_EDIT_PG_PAYMENT_GATEWAY_CONFIG", Sqlparam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ConfigController.GetData_Payment_gateway_config() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
                #endregion

            }
        }
    }
}
