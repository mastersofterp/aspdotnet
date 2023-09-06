//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : Grievance                            
// CREATION DATE : 27-july-2019                                                        
// CREATED BY    : NANCY SHARMA                                   
// MODIFIED DATE :
// MODIFIED DESC :
//====================================================================================== 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Data;
using System.Web;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLogicLayer.BusinessLogic
        {
            public class GrievanceController
            {

                // Connection String

                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                Common objcommon = new Common();

                #region Grievance Type
                public int AddUpdateGrievanceType(GrievanceEntity objGrivE)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_GRIEVANCE_TYPE_ID", objGrivE.GRIEVANCE_TYPE_ID);
                        objParams[1] = new SqlParameter("@P_GRIEVANCE_TYPE_NAME", objGrivE.GRIEVANCE_TYPE);
                        objParams[2] = new SqlParameter("@P_UANO", objGrivE.UANO);
                        objParams[3] = new SqlParameter("@P_GRIEVANCE_TYPE_CODE", objGrivE.GRIEVANCE_TYPE_CODE);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_GRIV_GRIEVENCE_TYPE_IU", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GrievanceController.AddUpdateGrievanceType->" + ex.ToString());
                    }
                    return retstatus;
                }

                #endregion

                #region Grievance Redressal Committee

                public int AddUpdateRedressalCommitteeType(GrievanceEntity objGrivE)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_GRIEVANCE_TYPE_ID", objGrivE.COMMITTEE_TYPE_ID);
                        objParams[1] = new SqlParameter("@P_GR_COMMITTEE_TYPE", objGrivE.COMMITTEE_TYPE);
                        objParams[2] = new SqlParameter("@P_UANO", objGrivE.UANO);
                        objParams[3] = new SqlParameter("@P_DEPT_FLAG", objGrivE.DEPT_FLAG);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_GRIV_GRCOMMITTEETYPE_IU", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GRedressalCommitteeController.AddUpdateRedressalCommitteeType->" + ex.ToString());
                    }
                    return retstatus;
                }

                public int AddUpdateRedressalCell(GrievanceEntity objGrivE)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_GRC_ID", objGrivE.GRC_ID);
                        objParams[1] = new SqlParameter("@P_COMMITTEE_TYPE_ID", objGrivE.COMMITTEE_TYPE_ID);
                        objParams[2] = new SqlParameter("@P_DEPARTMENT_ID", objGrivE.DEPARTMENT_ID);
                        objParams[3] = new SqlParameter("@P_GRCELL_TABLE", objGrivE.GRCELL_TABLE);
                        objParams[4] = new SqlParameter("@P_UANO", objGrivE.UANO);
                        objParams[5] = new SqlParameter("@P_ISCOMMITEETYPE", objGrivE.ISCOMMITEETYPE);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_GRIV_GR_COMMITTEE_MEMBER_IU", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == -2627)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GRedressalCommitteeController.AddUpdateRedressalCommitteeType->" + ex.ToString());
                    }
                    return retstatus;
                }

                public int AddUpGrievanceApplication(GrievanceEntity objGrivE)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_GAID", objGrivE.GR_GAID);
                        objParams[1] = new SqlParameter("@P_MOBILE_NO", objGrivE.MOBILE_NO);
                        objParams[2] = new SqlParameter("@P_EMAIL_ID", objGrivE.EMAIL_ID);
                        objParams[3] = new SqlParameter("@P_GR_APPLICATION_DATE", objGrivE.GR_APPLICATION_DATE);
                        objParams[4] = new SqlParameter("@P_GRIEVANCE", objGrivE.GRIEVANCE);
                        objParams[5] = new SqlParameter("@P_UANO", objGrivE.UANO);
                        objParams[6] = new SqlParameter("@P_GRCOMMITTEE_TYPE", objGrivE.GRCOMMITTEE_TYPE);
                        objParams[7] = new SqlParameter("@P_DEPT_ID", objGrivE.STUDEPTID);
                        objParams[8] = new SqlParameter("@P_STATUS", objGrivE.STATUS);
                        objParams[9] = new SqlParameter("@P_GRIV_CODE", objGrivE.GRIV_CODE);
                        objParams[10] = new SqlParameter("@P_GRIV_ID", objGrivE.GRIV_ID);
                        objParams[11] = new SqlParameter("@P_GRIV_ATTACHMENT", objGrivE.GRIV_ATTACHMENT);

                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_GRIV_GRIEVANCE_APPLICATION_IU", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            objGrivE.GR_GAID = Convert.ToInt32(ret);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GRedressalCommitteeController.AddUpGrievanceApplication->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetGrievanceApplication(int GAID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_GAID", GAID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRIV_GET_GRIEVANCE_APPLICATION_DETAILS", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GrievanceController.GetCellDetailsByID-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSubGrievanceTypeApplication(int GAID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_GAID", GAID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRIV_GET_SUB_GRIEVANCE_APPLICATION_DETAILS", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GrievanceController.GetCellDetailsByID-> " + ex.ToString());
                    }
                    return ds;
                }

                public int DeleteApplicationDetail(GrievanceEntity objVM)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_GR_GAID", objVM.GR_GAID);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_GRIV_GRIEVANCE_APPLICATION_DEL", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GRedressalCommitteeController.DeleteApplicationDetail->" + ex.ToString());
                    }
                    return retstatus;
                }

                public int AddUpdateGReply(GrievanceEntity objGrivE)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_REPLY_ID", objGrivE.REPLY_ID);
                        objParams[1] = new SqlParameter("@P_REPLY", objGrivE.REPLY);
                        objParams[2] = new SqlParameter("@P_GR_GAID", objGrivE.GR_GAID);
                        objParams[3] = new SqlParameter("@P_REPLY_UANO", objGrivE.REPLY_UANO);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_GRIV_GRIEVANCE_REPLY_INSERT_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GRedressalCommitteeController.AddUpdateGReply->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetGrievanceCommitteeReply(int GAID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_GR_GAID", GAID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRIV_GET_COMMITTEE_REPLY", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GrievanceController.GetGrievanceCommitteeReply-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddUpGrReceiveReply(GrievanceEntity objGrivE)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_GR_STATUS", objGrivE.GR_STATUS);
                        objParams[1] = new SqlParameter("@P_STUD_REMARK", objGrivE.GR_REMARKS);
                        objParams[2] = new SqlParameter("@P_GAID", objGrivE.GAID);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_GRIV_GRIEVANCE_STATUS_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GRedressalCommitteeController.AddUpdateGReply->" + ex.ToString());
                    }
                    return retstatus;
                }

                public int AddUpGrEntryAuthority(GrievanceEntity objGrivE)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_GAID", objGrivE.GAID);
                        objParams[1] = new SqlParameter("@P_STUD_REMARK", objGrivE.GR_REMARKS);
                        objParams[2] = new SqlParameter("@P_GAT_ID", objGrivE.GAT_ID);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_GRIV_GRIEVANCE_AUTHORITY_ENTRY_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GRedressalCommitteeController.AddUpdateGReply->" + ex.ToString());
                    }
                    return retstatus;
                }

                public int UpdateStudentRemark(GrievanceEntity objGrivE, int GRCT_ID)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_GAID", objGrivE.GAID);
                        objParams[1] = new SqlParameter("@P_GAT_ID", objGrivE.GAT_ID);
                        objParams[2] = new SqlParameter("@P_GRCT_ID", GRCT_ID);
                        objParams[3] = new SqlParameter("@P_STUD_REMARK", objGrivE.GR_REMARKS);

                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_GRIV_UPDATE_STUDENT_REMARK", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GRedressalCommitteeController.AddUpdateGReply->" + ex.ToString());
                    }
                    return retstatus;
                }

                public int AddUpGrCentralCommittee(GrievanceEntity objGrivE)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_GAID", objGrivE.GAID);
                        objParams[1] = new SqlParameter("@P_STUD_REMARK", objGrivE.GR_REMARKS);
                        objParams[2] = new SqlParameter("@P_GAT_ID", objGrivE.GAT_ID);
                        objParams[3] = new SqlParameter("@P_GRCTID", objGrivE.GRCTID);

                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_GRIV_GRIEVANCE_AUTHORITY_CENTRAL_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GRedressalCommitteeController.AddUpdateGReply->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetGrievanceRedressalCellList(int GRCT_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_GRCT_ID", GRCT_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRIV_GET_GRIEVANCE_REDRESSAL_CELL_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GrievanceController.GetGrievanceApplicationList-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetCellDetailsByID(int GRC_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_GRC_ID", GRC_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRIV_GET_CELL_DETAILS_BYID", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GrievanceController.GetCellDetailsByID-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetCommitteeMemberDetailsByID(int GRC_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_GRC_ID", GRC_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRIV_GET_COMMITTEE_MEMBER_DETAILS_BYID", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GrievanceController.GetCellDetailsByID-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                #region Grievance Application

                public DataSet GetGrievanceApplicationList(int STUD_IDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_STUD_IDNO", STUD_IDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRIV_GET_GRIEVANCE_APPLICATION_LIST", objParams);

                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GrievanceController.GetGrievanceApplicationList-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSubGrievanceApplicationList(int STUD_IDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_STUD_IDNO", STUD_IDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRIV_GET_GRIEVANCE_APPLICATION_LIST_FOR_SUB", objParams);

                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GrievanceController.GetGrievanceApplicationList-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetGrievanceReplyList(int STUD_IDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_STUD_IDNO", STUD_IDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRIV_GET_GRIEVANCE_REPLY_LIST", objParams);

                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GrievanceController.GetGrievanceReplyList-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetReplyDetails(int GAID, int REPLY_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_GAID", GAID);
                        objParams[1] = new SqlParameter("@P_REPLY_ID", REPLY_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRIV_GET_APPLICATION_DETAILS", objParams);

                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetDriverMas-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentPersonalDetails(int STUD_IDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_STUD_IDNO", STUD_IDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRIV_GET_STUDENT_DETAILS_LIST_STUDLOGIN", objParams);

                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GrievanceController.GetStudentPersonalDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetGrievanceApplicationNo(int GRIV_ID, int STUD_IDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_GRIV_ID", GRIV_ID);
                        objParams[1] = new SqlParameter("@P_STUD_IDNO", STUD_IDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRIV_GENERATE_STUDENT_APPLICATION_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GrievanceController.GetGrievanceApplicationNo->  " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetDataForEmail(string STUDEPTID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DEPTID", STUDEPTID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRIV_GET_CREDENTIALS_FOR_EMAIL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GrievanceController.GetDataForEmail-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetDataGrAppDeptDetails(int GAID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_GAID", GAID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRIV_GET_GRIEVANCE_DETAILS_MAIL_DEPTCOMMITEE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GrievanceController.GetDataForEmail-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetAuthorityEmailID(int GRCT_ID, int GAID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_GRCT_ID", GRCT_ID);
                        objParams[1] = new SqlParameter("@P_GAID", GAID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRIV_GET_CREDENTIALS_OF_AUTHORITY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GrievanceController.GetDataForEmail-> " + ex.ToString());
                    }
                    return ds;
                }


                #endregion

                #region Grievance Reply

                public DataSet GetStudentDetailsBy(int STUD_IDNO, int GAID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_STUD_IDNO", STUD_IDNO);
                        objParams[1] = new SqlParameter("@P_GAID", GAID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRIV_GET_STUDENT_DETAILS_COMMITTEEPAGE_INFO", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GrievanceController.GetStudentDetailsBy-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetStudentGrievancesList(int userno, int level)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_userno", userno);
                        objParams[1] = new SqlParameter("@P_level", level);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRIV_GET_STUDENT_GRIEVANCES_LIST", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GrievanceController.GetStudentGrievancesList-> " + ex.ToString());
                    }
                    return ds;
                }


                // It is used to get Student EmailId
                public DataSet GetStudentEmail(int GAID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_GAID", GAID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRIV_GET_STUDENT_EMAIL_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GrievanceController.GetStudentEmail-> " + ex.ToString());
                    }
                    return ds;
                }


                // It is used to get grievance reply details.
                //public DataSet GetReplyDetailsForEmail(int GAID, int REPLY_UANO)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = new SqlParameter[2];
                //        objParams[0] = new SqlParameter("@P_GAID", GAID);
                //        objParams[1] = new SqlParameter("@P_REPLY_UANO", REPLY_UANO);
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRIV_GET_REPLY_DETAILS_FOR_EMAIL", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GrievanceController.GetReplyDetailsForEmail-> " + ex.ToString());
                //    }
                //    return ds;
                //}

                #endregion


                #region Grievance Search

                public DataSet GetGrievanceApplicationDetails(int GaidNo, string fromDate, string ToDate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_GAID", GaidNo);
                        objParams[1] = new SqlParameter("@P_FromDate", fromDate);
                        objParams[2] = new SqlParameter("@P_ToDate", ToDate);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRIV_GET_STUDENT_GRIEVANCE_DETAILS", objParams);

                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GrievanceController.GetGrievanceApplicationList-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetGrievanceApplicationReply(int GaidNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_GAID", GaidNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRIV_GET_GRIEVANCE_DETAILS_BYID", objParams);

                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GrievanceController.GetGrievanceApplicationList-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #region Upload Files in Folder

                public void upload_new_files(string folder, int idno, string primary, string table, string initials, System.Web.UI.WebControls.FileUpload fuFile)
                {
                    string uploadPath = HttpContext.Current.Server.MapPath("~/GrievanceRedressal/upload_documents/" + folder + "/" + idno + "/");
                    if (!System.IO.Directory.Exists(uploadPath))
                    {
                        System.IO.Directory.CreateDirectory(uploadPath);
                    }
                    //Upload the File
                    if (!fuFile.PostedFile.FileName.Equals(string.Empty))
                    {
                        int newfilename = Convert.ToInt32(objcommon.LookUp(table, "max(" + primary + ")", ""));
                        string uploadFile = initials + newfilename + System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
                        //fuFile.PostedFile.SaveAs(uploadPath + uploadFile);
                        fuFile.PostedFile.SaveAs(uploadPath + uploadFile);
                    }
                }

                public void update_upload(string folder, int trxno, string lastfilename, int idno, string initials, System.Web.UI.WebControls.FileUpload fuFile)
                {
                    //Upload the File
                    string uploadPath = HttpContext.Current.Server.MapPath("~/GrievanceRedressal/upload_documents/" + folder + "/" + idno + "/");
                    if (!System.IO.Directory.Exists(uploadPath))
                    {
                        System.IO.Directory.CreateDirectory(uploadPath);
                    }
                    if (!fuFile.PostedFile.FileName.Equals(string.Empty))
                    {
                        //Update Assignment
                        string oldFileName = string.Empty;

                        oldFileName = initials + trxno + System.IO.Path.GetExtension(lastfilename);

                        if (System.IO.File.Exists(uploadPath + oldFileName))
                        {
                            System.IO.File.Delete(uploadPath + oldFileName);
                        }

                        string uploadFile = initials + trxno + System.IO.Path.GetExtension(fuFile.PostedFile.FileName);
                        fuFile.PostedFile.SaveAs(uploadPath + uploadFile);
                    }

                }
                #endregion





                #region Grievance Summary

                public DataSet GetGrievanceSummaryList(GrievanceEntity objGrivE, string FromDate, string ToDate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_DEPARTMENT_ID", objGrivE.DEPARTMENT_ID);
                        objParams[1] = new SqlParameter("@P_GRIV_ID", objGrivE.GRIV_ID);
                        objParams[2] = new SqlParameter("@P_FROM_DATE", FromDate);
                        objParams[3] = new SqlParameter("@P_TO_DATE", ToDate);

                        //if (objGrivE.FROM_DATE != DateTime.MinValue)
                        //{
                        //    objParams[2] = new SqlParameter("@P_FROM_DATE", objGrivE.FROM_DATE);
                        //}
                        //else
                        //{
                        //    objParams[2] = new SqlParameter("@P_FROM_DATE", DBNull.Value);
                        //}


                        //if (objGrivE.TO_DATE != DateTime.MinValue)
                        //{
                        //    objParams[3] = new SqlParameter("@P_TO_DATE", objGrivE.TO_DATE);
                        //}
                        //else
                        //{
                        //    objParams[3] = new SqlParameter("@P_TO_DATE", DBNull.Value);
                        //}
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRIV_GET_GRIEVANCE_SUMMARY_LIST", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GrievanceController.GetGrievanceSummaryList-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion


                // IT IS US TO GET TAB DATA
                public DataSet GetTabData(int UA_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRIV_GET_TAB_DATA", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GrievanceController.GetTabData-> " + ex.ToString());
                    }
                    return ds;
                }

                #region Grievance  SUB Type
                public int AddUpdateSUBGrievanceType(GrievanceEntity objGrivE)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_GRIV_SUB_ID", objGrivE.GRIV_SUB_ID);
                        objParams[1] = new SqlParameter("@P_GRIEVANCE_TYPE_ID", objGrivE.GRIEVANCE_TYPE_ID);
                        objParams[2] = new SqlParameter("@P_GRIV_SUB_TYPE", objGrivE.GRIV_SUB_TYPE);
                        objParams[3] = new SqlParameter("@P_UANO", objGrivE.UANO);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_GRIV_SUB_GRIEVANCE_TYPE_IU", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GrievanceController.AddUpdateSUBGrievanceType->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetAllSubGriv()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRIV_GET_ALL_SUB_GRIEVANCE_TYPE", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GrievanceController.GetAllSubGriv-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSingleSubType(int SUB_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_GRIV_SUB_ID", SUB_ID);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRIV_GET_SINGLE_SUB_GRIEVANCE_TYPE", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GrievanceController.GetAllSubGriv-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                public int AddUpdateSubRedressal(GrievanceEntity objGrivE)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SUB_GR_ID", objGrivE.SUB_GR_ID);
                        objParams[1] = new SqlParameter("@P_GRIV_ID", objGrivE.GRIEVANCE_TYPE_ID);
                        objParams[2] = new SqlParameter("@P_SUB_ID", objGrivE.GRIV_SUB_ID);
                        objParams[3] = new SqlParameter("@P_DEPARTMENT_ID", objGrivE.DEPARTMENT_ID);
                        objParams[4] = new SqlParameter("@P_GRCELL_TABLE", objGrivE.GRSUB_TABLE);
                        objParams[5] = new SqlParameter("@P_UANO", objGrivE.SUBUANO);
                        objParams[6] = new SqlParameter("@P_ISCOMMITEETYPE", objGrivE.ISCOMMITEETYPE);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_GRIV_SUB_COMMITTEE_MEMBER_IU", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == -2627)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GRedressalCommitteeController.AddUpdateRedressalCommitteeType->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetSubGrievanceRedressalCellList(int GRIV_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_GRIV_ID", GRIV_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRIV_GET_SUB_GRIEVANCE_REDRESSAL_CELL_LIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GrievanceController.GetGrievanceApplicationList-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSubGrivDetailsByID(int SUB_GR_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SUB_GR_ID", SUB_GR_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRIV_GET_SUB_DETAILS_BYID", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GrievanceController.GetCellDetailsByID-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSubCommitteeByID(int SUB_GR_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SUB_GR_ID", SUB_GR_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRIV_GET_SUB_COMMITTEE_DETAILS_BYID", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GrievanceController.GetCellDetailsByID-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddUpSubGrievancTypeApplication(GrievanceEntity objGrivE)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_GAID", objGrivE.GR_GAID);
                        objParams[1] = new SqlParameter("@P_MOBILE_NO", objGrivE.MOBILE_NO);
                        objParams[2] = new SqlParameter("@P_EMAIL_ID", objGrivE.EMAIL_ID);
                        objParams[3] = new SqlParameter("@P_GR_APPLICATION_DATE", objGrivE.GR_APPLICATION_DATE);
                        objParams[4] = new SqlParameter("@P_GRIEVANCE", objGrivE.GRIEVANCE);
                        objParams[5] = new SqlParameter("@P_UANO", objGrivE.UANO);
                        objParams[6] = new SqlParameter("@P_DEPT_ID", objGrivE.STUDEPTID);
                        objParams[7] = new SqlParameter("@P_STATUS", objGrivE.STATUS);
                        objParams[8] = new SqlParameter("@P_GRIV_CODE", objGrivE.GRIV_CODE);
                        objParams[9] = new SqlParameter("@P_GRIV_ID", objGrivE.GRIV_ID);
                        objParams[10] = new SqlParameter("@P_GRIV_ATTACHMENT", objGrivE.GRIV_ATTACHMENT);
                        objParams[11] = new SqlParameter("@P_SUB_ID", objGrivE.GRIV_SUB_ID);

                        objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_GRIV_GRIEVANCE_APPLICATION_IU_FOR_SUBGRIV", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            objGrivE.GR_GAID = Convert.ToInt32(ret);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GRedressalCommitteeController.AddUpGrievanceApplication->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetSubGrivList(int userno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_userno", userno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRIV_GET_STUDENT_SUB_TYPE_GRIEVANCES_LIST", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GrievanceController.GetStudentGrievancesList-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSubGrivStudentDetailsBy(int STUD_IDNO, int GAID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_STUD_IDNO", STUD_IDNO);
                        objParams[1] = new SqlParameter("@P_GAID", GAID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRIV_GET_SUB_GRIV_STUDENT_DETAILS_COMMITTEEPAGE_LIST", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GrievanceController.GetStudentDetailsBy-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddUpdateSubGrivReply(GrievanceEntity objGrivE)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_REPLY_ID", objGrivE.REPLY_ID);
                        objParams[1] = new SqlParameter("@P_REPLY", objGrivE.REPLY);
                        objParams[2] = new SqlParameter("@P_GR_GAID", objGrivE.GR_GAID);
                        objParams[3] = new SqlParameter("@P_REPLY_UANO", objGrivE.REPLY_UANO);
                        objParams[4] = new SqlParameter("@P_GAT_ID", objGrivE.GAT_ID);
                        objParams[5] = new SqlParameter("@P_AUTH_STATUS", objGrivE.GR_STATUS);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_GRIV_SUB_GRIEVANCE_REPLY_INSERT_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GRedressalCommitteeController.AddUpdateGReply->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetSubGrievanceReplyList(int STUD_IDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_STUD_IDNO", STUD_IDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRIV_GET_SUB_GRIEVANCE_REPLY_LIST", objParams);

                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GrievanceController.GetGrievanceReplyList-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSubGrivReplyDetails(int GAID, int REPLY_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_GAID", GAID);
                        objParams[1] = new SqlParameter("@P_REPLY_ID", REPLY_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GRIV_GET_SUB_GRIV_APPLICATION_DETAILS", objParams);

                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VMController.GetDriverMas-> " + ex.ToString());
                    }
                    return ds;
                }

                public int AddUpSubGrivAuthority(GrievanceEntity objGrivE)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_GAID", objGrivE.GAID);
                        objParams[1] = new SqlParameter("@P_STUD_REMARK", objGrivE.GR_REMARKS);
                        objParams[2] = new SqlParameter("@P_GAT_ID", objGrivE.GAT_ID);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_GRIV_SUB_GRIEVANCE_AUTHORITY_ENTRY_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GRedressalCommitteeController.AddUpdateGReply->" + ex.ToString());
                    }
                    return retstatus;
                }

                public int AddUpSubGrivReceiveReply(GrievanceEntity objGrivE)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_GR_STATUS", objGrivE.GR_STATUS);
                        objParams[1] = new SqlParameter("@P_STUD_REMARK", objGrivE.GR_REMARKS);
                        objParams[2] = new SqlParameter("@P_GAID", objGrivE.GAID);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_GRIV_SUB_GRIEVANCE_STATUS_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GRedressalCommitteeController.AddUpdateGReply->" + ex.ToString());
                    }
                    return retstatus;
                }

                public int UpdateSubGrivStudentRemark(GrievanceEntity objGrivE, int SUB_GR_ID)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_GAID", objGrivE.GAID);
                        objParams[1] = new SqlParameter("@P_GAT_ID", objGrivE.GAT_ID);
                        objParams[2] = new SqlParameter("@P_SUB_GR_ID", SUB_GR_ID);
                        objParams[3] = new SqlParameter("@P_STUD_REMARK", objGrivE.GR_REMARKS);

                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_GRIV_SUB_UPDATE_STUDENT_REMARK", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GRedressalCommitteeController.AddUpdateGReply->" + ex.ToString());
                    }
                    return retstatus;
                }

            }


        }
    }
}
