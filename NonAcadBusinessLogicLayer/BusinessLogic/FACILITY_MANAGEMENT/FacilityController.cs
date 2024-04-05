//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS LOGIC FILE //[FacilityController]                                  
// CREATION DATE : 14-04-2018                                                 
// CREATED BY    : Swati Ghate
// MODIFIED DATE :
// MODIFIED DESC :
//====================================================================================== 
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using Newtonsoft.Json;
namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class FacilityController
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                #region Minor_Facility
                public DataSet GetMinorFacility()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_FACILITY_GET_MINOR_FACILITY", objParams);
                        
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FacilityController.GetMinorFacility-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetMinorFacilityByNo(FacilityEntity objFM)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_MinFacilityNo", objFM.MinFacilityNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_FACILITY_GET_MINOR_FACILITY_BY_NO", objParams);                        
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FacilityController.GetMinorFacilityByNo-> " + ex.ToString());
                    }
                    return ds;
                }
                public int DeleteMinorFacility(FacilityEntity objFM)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_MinFacilityNo", objFM.MinFacilityNo);
                        objParams[1] = new SqlParameter("@P_CreatedBy", objFM.CreatedBy);
                        objParams[2] = new SqlParameter("@P_IPAddress", objFM.IPAddress);
                        objParams[3] = new SqlParameter("@P_MACAddress", objFM.MacAddress);                                           

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_FACILITY_DELETE_MINOR_FACILITY", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FacilityController.DeleteMinorFacility-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int AddUpdateMinorFacility(FacilityEntity objFM)
                {
                    int retStatus = 0;// Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                      
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_MinFacilityNo", objFM.MinFacilityNo);
                        objParams[1] = new SqlParameter("@P_MinFacilityName", objFM.MinFacilityName);
                        objParams[2] = new SqlParameter("@P_MinFacilityDetail", objFM.MinFacilityDetail);
                        objParams[3] = new SqlParameter("@P_IsActive", objFM.IsActive);
                        objParams[4] = new SqlParameter("@P_CreatedBy", objFM.CreatedBy);
                        objParams[5] = new SqlParameter("@P_IPAddress", objFM.IPAddress);
                        objParams[6] = new SqlParameter("@P_MACAddress", objFM.MacAddress);
                        objParams[7] = new SqlParameter("@P_CollegeCode", objFM.CollegeCode);                     
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_FACILITY_INS_UPD_MINOR_FACILITY", objParams, false));


                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_FACILITY_INS_UPD_MINOR_FACILITY", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FacilityController.AddUpdateMinorFacility-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion
                #region Facility_Detail
                /// <summary>
                /// Centralize Facility Detail
                /// </summary>
                /// <param name="objFM"></param>
                /// <param name="dt"></param>
                /// <returns></returns>
                public int AddUpdateCentraFacilityDetail(FacilityEntity objFM,DataTable dt)
                {
                    int retStatus = 0;// Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_CentralizeDetailNo", objFM.CenFacilityNo);
                        objParams[1] = new SqlParameter("@P_CenFacilityName", objFM.CenFacilityName);
                        objParams[2] = new SqlParameter("@P_CenFacilityDetail", objFM.CenFacilityDetail);
                        objParams[3] = new SqlParameter("@P_Remark", objFM.Remark);                        
                        objParams[4] = new SqlParameter("@P_IsActive", objFM.IsActive);
                        objParams[5] = new SqlParameter("@P_CreatedBy", objFM.CreatedBy);
                        objParams[6] = new SqlParameter("@P_IPAddress", objFM.IPAddress);
                        objParams[7] = new SqlParameter("@P_MACAddress", objFM.MacAddress);
                        objParams[8] = new SqlParameter("@P_CollegeCode", objFM.CollegeCode);
                        objParams[9] = new SqlParameter("@P_FACILITY_MINOR_FAC_LIST", dt);                        
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_FACILITY_INS_UPD_CENTRALIZE_FACILITY_DETAIL", objParams, false));


                        //if (objSQLHelper.ExecuteNonQuerySP("PKG_FACILITY_INS_UPD_MINOR_FACILITY", objParams, false) != null)
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FacilityController.AddUpdateCentraFacilityDetail-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int DeleteFacilityDetail(FacilityEntity objFM)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_CentralizeDetailNo", objFM.CenFacilityNo);
                        objParams[1] = new SqlParameter("@P_CreatedBy", objFM.CreatedBy);
                        objParams[2] = new SqlParameter("@P_IPAddress", objFM.IPAddress);
                        objParams[3] = new SqlParameter("@P_MACAddress", objFM.MacAddress);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_FACILITY_DELETE_CENTRA_FACILITY_DETAIL", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FacilityController.DeleteFacilityDetail-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetCentraFacilityDetail()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_FACILITY_GET_CENTRA_FACILITY_DETAIL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FacilityController.GetCentraFacilityDetail-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetCentraFacilityDetailByNo(FacilityEntity objFM)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_CentralizeDetailNo", objFM.CenFacilityNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_FACILITY_GET_CENTRA_FACILITY_DETAIL_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FacilityController.GetCentraFacilityDetailByNo-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion
                #region Facility_Approval
                public DataSet GetPendingApplication(FacilityEntity objFM)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UANO", objFM.UANO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_FACILITY_GET_FACILITY_APPROVAL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FacilityController.GetFacilityApplication-> " + ex.ToString());
                    }
                    return ds;
                }
                public int AddUpdateFacilityApproval(FacilityEntity objFM)
                {
                    int retStatus = 0;// Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[10];

                        objParams[0] = new SqlParameter("@P_ApplicationNo", objFM.ApplicationNo);
                        objParams[1] = new SqlParameter("@P_UANO", objFM.UANO);
                        objParams[2] = new SqlParameter("@P_STATUS", objFM.STATUS);                       
                        objParams[3] = new SqlParameter("@P_ApprovalRemark", objFM.Remark);                       
                        objParams[4] = new SqlParameter("@P_IsActive", objFM.IsActive);
                        objParams[5] = new SqlParameter("@P_CreatedBy", objFM.CreatedBy);
                        objParams[6] = new SqlParameter("@P_IPAddress", objFM.IPAddress);
                        objParams[7] = new SqlParameter("@P_MACAddress", objFM.MacAddress);
                        objParams[8] = new SqlParameter("@P_CollegeCode", objFM.CollegeCode);                       
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;
                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_FACILITY_INS_UPD_CENTRALIZE_FACILITY_APPROVAL", objParams, false));
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FacilityController.AddUpdateFacilityApproval-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion
                #region Facility_Application
                public DataSet CheckApplicationExists(FacilityEntity objFM)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_FRDATE", objFM.FromDate);
                        objParams[1] = new SqlParameter("@P_TODATE", objFM.ToDate);
                        objParams[2] = new SqlParameter("@P_CentralizeDetailNo", objFM.CenFacilityNo);
                        objParams[3] = new SqlParameter("@P_ApplicationNo", objFM.ApplicationNo);                        
                        objParams[4] = new SqlParameter("@P_IDNO", objFM.IDNO);                        
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_FACILITY_CHECK_APPLICATION_EXISTS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FacilityController.CheckApplicationExists->" + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                /// <summary>
                /// to submit application
                /// </summary>
                /// <param name="objFM"></param>
                /// <param name="dt"></param>
                /// <returns></returns>
                public int AddUpdateFacilityApplication(FacilityEntity objFM, DataTable dt)
                {
                    int retStatus = 0;// Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[17];
                        
                        objParams[0] = new SqlParameter("@P_ApplicationNo", objFM.ApplicationNo);                       
                        objParams[1] = new SqlParameter("@P_CentralizeDetailNo", objFM.CenFacilityNo);                        
                        objParams[2] = new SqlParameter("@P_IDNO", objFM.IDNO);
                        objParams[3] = new SqlParameter("@P_ApplicationDate", objFM.ApplicationDate);
                        objParams[4] = new SqlParameter("@P_FromDate", objFM.FromDate);
                        objParams[5] = new SqlParameter("@P_ToDate", objFM.ToDate);
                        objParams[6] = new SqlParameter("@P_PriorityLevel", objFM.PriorityLevel);
                        objParams[7] = new SqlParameter("@P_Remark", objFM.Remark);
                        objParams[8] = new SqlParameter("@P_IsActive", objFM.IsActive);
                      
                        objParams[9]  = new SqlParameter("@P_IsCancel", objFM.IsCancel);
                        objParams[10] = new SqlParameter("@P_Reason", objFM.Reason);

                        objParams[11] = new SqlParameter("@P_CreatedBy", objFM.CreatedBy);
                        objParams[12] = new SqlParameter("@P_IPAddress", objFM.IPAddress);
                        objParams[13] = new SqlParameter("@P_MACAddress", objFM.MacAddress);
                        objParams[14] = new SqlParameter("@P_CollegeCode", objFM.CollegeCode);
                        objParams[15] = new SqlParameter("@P_FACILITY_APPLICATION_MINOR_FAC_LIST", dt);
                        objParams[16] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[16].Direction = ParameterDirection.Output;

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_FACILITY_INS_UPD_CENTRALIZE_FACILITY_APPLICATION", objParams, false));


                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FacilityController.AddUpdateCentraFacilityDetail-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int DeleteFacilityApplication(FacilityEntity objFM)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_ApplicationNo", objFM.ApplicationNo);
                        objParams[1] = new SqlParameter("@P_CreatedBy", objFM.CreatedBy);
                        objParams[2] = new SqlParameter("@P_IPAddress", objFM.IPAddress);
                        objParams[3] = new SqlParameter("@P_MACAddress", objFM.MacAddress);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_FACILITY_DELETE_FACILITY_APPLICATION", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FacilityController.DeleteFacilityDetail-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetFacilityApplication(FacilityEntity objFM)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IDNO", objFM.IDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_FACILITY_GET_FACILITY_APPLICATION", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FacilityController.GetFacilityApplication-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetFacilityApplicationByNo(FacilityEntity objFM)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ApplicationNo", objFM.ApplicationNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_FACILITY_GET_FACILITY_APPLICATION_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FacilityController.GetFacilityApplicationByNo-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion


                public int ChkApplicationDateTime(FacilityEntity objFM)
                {
                    int result = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_FROM_DATE", objFM.FromDate);
                        objParams[1] = new SqlParameter("@P_TO_DATE", objFM.ToDate);
                        objParams[2] = new SqlParameter("@P_CENTRALIZEDETAILNO", objFM.CenFacilityNo);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        result = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_FACILITY_CHECK_APPLICATION_EXISTS_DATE_TIME", objParams, true));
                    }
                    catch (Exception ex)
                    {
                        return result;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FacilityController.GetFacilityApplicationByNo-> " + ex.ToString());

                    }
                    return result;

                }

                #region Send Notification Added on 05-02-2020
               //  -- Notification Details
                public Status SubmitNotificationDetails(Notification NF)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParam = null;
                        objParam = new SqlParameter[5];
                        objParam[0] = new SqlParameter("@P_NOTIFICATIONID", NF.NotificationID);
                        //objParam[1] = new SqlParameter("@P_DEGREENO", NF.Degreeno);
                        //objParam[2] = new SqlParameter("@P_BRANCHNO", NF.Branchno);
                        //objParam[3] = new SqlParameter("@P_SEMESTERNO", NF.Semesterno);
                        //objParam[4] = new SqlParameter("@P_DEPTNO", NF.Deptno);
                        objParam[1] = new SqlParameter("@P_UA_TYPE", NF.UA_Type);
                        objParam[2] = new SqlParameter("@P_FROMUSERID", NF.UANO);
                        objParam[3] = new SqlParameter("@P_UA_NO", NF.UserNo);
                        objParam[4] = new SqlParameter("@P_STATUS", SqlDbType.Int);
                        objParam[4].Direction = ParameterDirection.Output;
                        DataSet ds = objSQLHelper.ExecuteDataSetSP("PKG_FACILITY_MGT_INS_NOTIFICATION_DETAILS", objParam);


                        BusinessLayer.BusinessEntities.SendNotice notification = new BusinessLayer.BusinessEntities.SendNotice();
                        notification.MessageTitle = NF.Title;
                        notification.Message = NF.Details;

                        bool status = SendNotification(ds, notification, 24000);
                        Status responseStatus = new Status();
                        //if (status == true)
                        //{                    
                        responseStatus.Id = objParam[4].Value.ToString();

                        int count = Convert.ToInt32(objParam[4].Value.ToString());

                        if (count > 0)
                        {
                            // responseStatus.Message = "Notification successfully sent to " + count + " users";
                            responseStatus.IsErrorInService = false;
                        }
                        else
                        {
                            // responseStatus.Message = "Notification successfully sent to users";
                            responseStatus.IsErrorInService = false;
                        }
                        //else if (count == 0)
                        //{
                        //    responseStatus.Message = "No users found for selected criteria";
                        //    responseStatus.IsErrorInService = true;
                        //}
                        //else
                        //{
                        //    responseStatus.Message = "Unable to send notice, please try again later";
                        //    responseStatus.IsErrorInService = true;
                        //}
                        //}
                        //else
                        //{
                        //    responseStatus.Message = "Unable to send notice, please try again later";
                        //    responseStatus.IsErrorInService = true;
                        //}
                        return responseStatus;
                    }
                    catch (Exception e)
                    {
                        return null;
                    }
                }
                public bool SendNotification(DataSet ds, SendNotice notification, int time)
                {
                    bool status = false;
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        List<string> ids = new List<string>();
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            string reg = "\"" + row[0].ToString() + "\"";
                            ids.Add(reg);
                        }
                        List<List<string>> idsGroup = SplitList(ids);
                        foreach (List<string> idList in idsGroup)
                        {
                            status = SendToFCMServer(idList, notification, time);
                            if (!status)
                            {
                                break;
                            }
                        }
                    }
                    return status;
                }
                private List<List<string>> SplitList(List<string> regIds)
                {
                    int size = 1000;
                    var list = new List<List<string>>();
                    for (int i = 0; i < regIds.Count; i += size)
                        list.Add(regIds.GetRange(i, Math.Min(size, regIds.Count - i)));
                    return list;
                }
                private bool SendToFCMServer(List<string> regIds, SendNotice notification, int time)
                {
                    try
                    {
                        // Time must be between 
                        // string[] credentials = GetSenderIdAndServerKey(collegeId);
                        string appId = "603792136876";
                        string serverKey = "AAAAjJTQ2qw:APA91bGgZu2gAFzC9C-bc9rjlrJyJoVWWYSJ-6D9sKGSJ3Y7kcmBCnW4LPGFbk3Nz0q8xDl_LTV4oks-gH3UEatBdongSzr5YkAgg8XcUThHR17CSTAYWorhBecDFmKSLTMxr45JlI1L";

                        WebRequest webRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                        webRequest.Method = "post";
                        webRequest.ContentType = "application/json";
                        webRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
                        webRequest.Headers.Add(string.Format("Sender: id={0}", appId));

                        string csvIds = String.Join(",", regIds.Select(x => x.ToString()).ToArray());
                        string json = JsonConvert.SerializeObject(notification);
                        string customData = "\"Message\":" + json;
                        string postData = "{\"time_to_live\":" + time + ",\"delay_while_idle\":true,\"data\": {" + customData + "},\"registration_ids\":[" + csvIds + "]}";
                        //string postData = "{\"time_to_live\":" + time + ",\"delay_while_idle\":true,\"data\": " + json + ",\"registration_ids\":[\"cTJXroKBMUI:APA91bEcNlkCk7gvUoOgMeZoGz-Zj5AeiVRAQaEM3FuNXsTvKBLNbdQjgysmBO21RbVDlgvcr9mZS9QsYhmVPE41UwyOisiao3DCqljwcoaS7BNyvZunXSHLi6fPpnNigUyPJF98qnmC\"]}";
                        //string postData = "{\"time_to_live\":" + time + ",\"delay_while_idle\":true,\"data\": " + json + ",\"registration_ids\":[\"eQya5kExd2s:APA91bEd9jteagJGuEUNbWFa9fqS1m57oP_zabzxSkBQC5tyZ9ASpCi8dnc2UOYfvgqw29fTT2vp5LiOJdEjBqeKF8ayt6ombj58fv9BaAYUH2TSl6zbh9T04YcwzJ22aLRJMNHAEmvZ\"]}";


                        Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                        webRequest.ContentLength = byteArray.Length;

                        Stream dataStream = webRequest.GetRequestStream();
                        dataStream.Write(byteArray, 0, byteArray.Length);
                        dataStream.Close();
                        Uri a = webRequest.RequestUri;
                        WebResponse tResponse = webRequest.GetResponse();

                        dataStream = tResponse.GetResponseStream();

                        StreamReader tReader = new StreamReader(dataStream);
                        String sResponseFromServer = tReader.ReadToEnd();

                        tReader.Close();
                        dataStream.Close();
                        tResponse.Close();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }

                #endregion
            }
        }
    }
}


