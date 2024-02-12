using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class RequisitionController
            {

                /// <SUMMARY>
                /// ConnectionStrings
                /// </SUMMARY>
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                private string constr = System.Configuration.ConfigurationManager.ConnectionStrings["REC"].ConnectionString;


                public DataSet GetPosts()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];
                        //objParams[0] = new SqlParameter("@P_DEPTNO", Deptno);
                        ds = objSQLHelper.ExecuteDataSetSP("SP_POST_BROWSE_REP", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.RequisitionController.GetPosts->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int AddUpdateReqUserData(Requisition objRequisition, int p)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[11];
                        objparams[0] = new SqlParameter("@P_REQUNO", objRequisition.REQUNO);
                        objparams[1] = new SqlParameter("@P_DEPTNO", objRequisition.DEPTNO);
                        objparams[2] = new SqlParameter("@P_UA_NO", objRequisition.USERNO);
                        objparams[3] = new SqlParameter("@P_POSTCAT_NO", objRequisition.POSTCATNO);
                        objparams[4] = new SqlParameter("@P_POST_NO", objRequisition.POSTNO);
                        objparams[5] = new SqlParameter("@P_POST", objRequisition.POST);
                        objparams[6] = new SqlParameter("@P_COLLEGE_CODE", objRequisition.COLLEGE_CODE);
                        objparams[7] = new SqlParameter("@P_CREATED_BY", objRequisition.CREATEDBY);
                        objparams[8] = new SqlParameter("@P_MODIFIED_BY", objRequisition.MODIFIEDBY);
                        objparams[9] = new SqlParameter("@P_ISACTIVE", objRequisition.IsActive);
                        objparams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[10].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REC_REQUSER_INSUPD", objparams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.RequisitionController.AddUpdateReqUserData->" + ex.ToString());
                    }
                    return retStatus;

                }

                public DataSet GetAllRequisitionUser(Requisition objRequisition)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_REQUNO", objRequisition.REQUNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_REC_REQUSERDATA_GET_BYALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.RequisitionController.GetAllRequisitionUser->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetAllRequisitionApprLevel(Requisition objRequisition)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_REQ_PANO", objRequisition.REQ_PANO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_REC_REQAPPRLVLDATA_GET_BYALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.RequisitionController.GetAllRequisitionApprLevel->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int AddUpdateReqAppLevelData(Requisition objRequisition, int p)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[16];
                        objparams[0] = new SqlParameter("@P_REQ_PANO", objRequisition.REQ_PANO);
                        objparams[1] = new SqlParameter("@P_DEPTNO", objRequisition.DEPTNO);
                        objparams[2] = new SqlParameter("@P_UA_NO", objRequisition.USERNO);
                        objparams[3] = new SqlParameter("@P_POST_NO", objRequisition.POSTNO);
                        objparams[4] = new SqlParameter("@P_POST", objRequisition.POST);
                        objparams[5] = new SqlParameter("@P_PANO1", objRequisition.PANO1);
                        objparams[6] = new SqlParameter("@P_PANO2", objRequisition.PANO2);
                        objparams[7] = new SqlParameter("@P_PANO3", objRequisition.PANO3);
                        objparams[8] = new SqlParameter("@P_PANO4", objRequisition.PANO4);
                        objparams[9] = new SqlParameter("@P_PANO5", objRequisition.PANO5);
                        objparams[10] = new SqlParameter("@P_COLLEGE_CODE", objRequisition.COLLEGE_CODE);
                        objparams[11] = new SqlParameter("@P_COLLEGE_NO", objRequisition.COLLEGE_NO);
                        objparams[12] = new SqlParameter("@P_CREATED_BY", objRequisition.CREATEDBY);
                        objparams[13] = new SqlParameter("@P_MODIFIED_BY", objRequisition.MODIFIEDBY);
                        objparams[14] = new SqlParameter("@P_ISACTIVE", objRequisition.IsActive);
                        objparams[15] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[15].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REC_REQAPPRLVL_INSUPD", objparams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.RequisitionController.AddUpdateReqUserData->" + ex.ToString());
                    }
                    return retStatus;

                }

                public int AddRequisitionRequest(Requisition objReq)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[16];
                        objParams[0] = new SqlParameter("@P_REQ_ID", objReq.REQ_ID);
                        objParams[1] = new SqlParameter("@P_REQUISITION_NO", objReq.REQUISITION_NO);
                        objParams[2] = new SqlParameter("@P_DEPT_NO", objReq.DEPTNO);
                        objParams[3] = new SqlParameter("@P_POSTTYPE_NO", objReq.POSTCATNO);
                        objParams[4] = new SqlParameter("@P_POST_NO", objReq.POSTNO);
                        objParams[5] = new SqlParameter("@P_POST", objReq.POST);
                        objParams[6] = new SqlParameter("@P_REQUNO", objReq.REQUNO);
                        objParams[7] = new SqlParameter("@P_REQUEST_DATE", objReq.REQUEST_DATE);
                        objParams[8] = new SqlParameter("@P_NO_OF_POSITION", objReq.NO_OF_POSITION);
                        objParams[9] = new SqlParameter("@P_DESCRIPTION", objReq.DESCRIPTION);
                        objParams[10] = new SqlParameter("@P_APPROVAL_STATUS", objReq.APPROVAL_STATUS);
                        objParams[11] = new SqlParameter("@P_COLLEGE_CODE", objReq.COLLEGE_CODE);
                        objParams[12] = new SqlParameter("@P_COLLEGE_NO", objReq.COLLEGE_NO);
                        objParams[13] = new SqlParameter("@P_CREATED_BY", objReq.CREATEDBY);
                        objParams[14] = new SqlParameter("@P_REQ_PANO", objReq.REQ_PANO);
                        objParams[15] = new SqlParameter("@P_RET", SqlDbType.Int);
                        objParams[15].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REC_REQUISITION_REQUEST_INSERT", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else if (Convert.ToInt32(ret) == -1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.RequisitionController.AddRequisitionRequest -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateRequisitionRequest(Requisition objReq)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_REQ_ID", objReq.REQ_ID);
                        objParams[1] = new SqlParameter("@P_REQUISITION_NO", objReq.REQUISITION_NO);
                        objParams[2] = new SqlParameter("@P_DEPT_NO", objReq.DEPTNO);
                        objParams[3] = new SqlParameter("@P_POSTTYPE_NO", objReq.POSTCATNO);
                        objParams[4] = new SqlParameter("@P_POST_NO", objReq.POSTNO);
                        objParams[5] = new SqlParameter("@P_POST", objReq.POST);
                        objParams[6] = new SqlParameter("@P_REQUNO", objReq.REQUNO);
                        objParams[7] = new SqlParameter("@P_REQUEST_DATE", objReq.REQUEST_DATE);
                        objParams[8] = new SqlParameter("@P_NO_OF_POSITION", objReq.NO_OF_POSITION);
                        objParams[9] = new SqlParameter("@P_DESCRIPTION", objReq.DESCRIPTION);
                        objParams[10] = new SqlParameter("@P_APPROVAL_STATUS", objReq.APPROVAL_STATUS);
                        objParams[11] = new SqlParameter("@P_COLLEGE_CODE", objReq.COLLEGE_CODE);
                        objParams[12] = new SqlParameter("@P_COLLEGE_NO", objReq.COLLEGE_NO);
                        objParams[13] = new SqlParameter("@P_MODIFED_BY", objReq.MODIFIEDBY);
                        objParams[14] = new SqlParameter("@P_RET", SqlDbType.Int);
                        objParams[14].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REC_REQUISITION_REQUEST_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.RequisitionController.UpdateRequisitionRequest -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetRequisitionReqStatus(int userno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[1];
                        objparams[0] = new SqlParameter("@P_REQBY_UANO", userno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_REC_GET_REQUISITION_REQUEST", objparams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.RequisitionController.GetRequisitionReqStatus->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;

                }

                public int DeleteRequisitionRequest(int REQ_ID)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_REQ_ID", REQ_ID);
                        objParams[1] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        Object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REC_DELETE_REQUISITION_REQUEST", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.RequisitionController.DeleteRequisitionRequest->" + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetPendListforRequisitionApproval(int UA_No, Requisition objReq)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_No);
                        objParams[1] = new SqlParameter("@P_DEPTNO", objReq.DEPTNO);
                        objParams[2] = new SqlParameter("@P_POSTTYPE", objReq.POSTCATNO);
                        objParams[3] = new SqlParameter("@P_POSTNO", objReq.POST_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_REC_REQUISITION_GET_PENDINGLIST", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.RequisitionController.GetPendListforRequisitionApproval->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetApprListforRequisitionApproval(int UA_No, Requisition objReq)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_UA_NO", UA_No);
                        objParams[1] = new SqlParameter("@P_DEPTNO", objReq.DEPTNO);
                        objParams[2] = new SqlParameter("@P_POSTTYPE", objReq.POSTCATNO);
                        objParams[3] = new SqlParameter("@P_POSTNO", objReq.POST_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_REC_REQUISITION_GET_APPROVEDLIST", objParams);
                    }
                    catch (Exception ex)
                    {

                        return ds;
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.RequisitionController.GetApprListforRequisitionApproval->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int UpdateRequisitionApprPass(Requisition objReq)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[7];
                        objparams[0] = new SqlParameter("@P_REQ_ID", objReq.REQ_ID);
                        objparams[1] = new SqlParameter("@P_UA_NO", objReq.USERNO);
                        objparams[2] = new SqlParameter("@P_STATUS", objReq.APPROVAL_STATUS);
                        objparams[3] = new SqlParameter("@P_APRDT", objReq.APPROVED_DATE);
                        objparams[4] = new SqlParameter("@P_REMARKS", objReq.DESCRIPTION);
                        objparams[5] = new SqlParameter("@P_COLLEGE_CODE", objReq.COLLEGE_CODE);
                        objparams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[6].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_REC_REQUISITION_APPR_PASS_UPDATE", objparams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.RequisitionController.UpdateRequisitionApprPass->" + ex.ToString());
                    }
                    return retStatus;

                }

                public DataSet GetRequisitionReqStatusById(int userno, int REQ_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_REQ_ID", REQ_ID);
                        objParams[1] = new SqlParameter("@P_REQBY_UANO", userno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_REC_GET_REQUISITION_REQUEST_BY_ID", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.RequisitionController.GetRequisitionReqStatusById->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
            }
        }
    }
}
