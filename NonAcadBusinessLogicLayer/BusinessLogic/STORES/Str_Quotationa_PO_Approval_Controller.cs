using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLogicLayer.BusinessLogic
        {
            public class Str_Quotationa_PO_Approval_Controller
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>

                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                #region Quotation Approval

                public DataSet GetQuotationItemDatail(string CommandType, string Qtno, int UA_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@CommandType", CommandType);
                        objParams[1] = new SqlParameter("@QINO", Qtno);
                        objParams[2] = new SqlParameter("@UA_NO", UA_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_Get_Quotation_Item_Detail", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLogicLayer.BusinessLogic.Str_Quotationa_PO_Approval_Controller.GetQuotationItemDatail-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateQuotationApproval(string App_status, int QNO, int UA_NO, string Remarks)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Update QuotationApproval
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@AppStatus", App_status);
                        objParams[1] = new SqlParameter("@UANO", UA_NO);
                        objParams[2] = new SqlParameter("@QNO", QNO);
                        objParams[3] = new SqlParameter("@Remark", Remarks);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_APPROVALQUOTATION_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLogicLayer.BusinessLogic.Str_Quotationa_PO_Approval_Controller.UpdateQuotationApproval-> " + ex.ToString());
                    }
                    return retStatus;
                }

                #endregion  Quotation Approval

                #region Purchase Approval

                public DataSet GetPOItemDatail(string CommandType, int PNO, int UA_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@CommandType", CommandType);
                        objParams[1] = new SqlParameter("@PNO", PNO);
                        objParams[2] = new SqlParameter("@UA_NO", UA_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_PURCHASEORDER_ITEM_DETAIL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLogicLayer.BusinessLogic.Str_Quotationa_PO_Approval_Controller.GetPOItemDatail-> " + ex.ToString());
                    }
                    return ds;
                }


                public int UpdatePOApproval(string App_status, int PNO, int UA_NO, string Remarks)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Update QuotationApproval
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@AppStatus", App_status);
                        objParams[1] = new SqlParameter("@UANO", UA_NO);
                        objParams[2] = new SqlParameter("@PNO", PNO);
                        objParams[3] = new SqlParameter("@Remark", Remarks);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_APPROVAL_PURCHASEORDER_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLogicLayer.BusinessLogic.Str_Quotationa_PO_Approval_Controller.UpdatePOApproval-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public DataSet QUOTATIONPPATHEmailSMSINFO(int QNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_QTONO", QNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_Get_QUOTATION_SMS_EMAIL_USER_INFO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_Quotationa_PO_Approval_Controller.QUOTATIONEmailSMSINFO-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet QUOTATIONEmailSMSINFOREJECTED(int QNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_QNO", QNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_Get_QUOTATION_Email_SMS_INFO_REJECTED", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_Quotationa_PO_Approval_Controller.QUOTATIONEmailSMSINFOREJECTED-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet QUOTATIONEmailSMSINFO(int QNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_QNO", QNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_Get_QUOTATION_Email_SMS_INFO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_Quotationa_PO_Approval_Controller.QUOTATIONEmailSMSINFO-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet PPMobileMailIDUSERINFO(int TrNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TRNO", TrNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_Get_APPMobileMailID_USER_INFO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.PPMobileMailIDUSERINFO-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion  Purchase Approval

                public DataSet POPPATHEmailSMSINFO(int PONO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PONO", PONO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_Get_PO_SMS_EMAIL_USER_INFO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_Quotationa_PO_Approval_Controller.POPPATHEmailSMSINFO-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet POEmailSMSINFOREJECTED(int PONO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PONO", PONO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_Get_PO_Email_SMS_INFO_REJECTED", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_Quotationa_PO_Approval_Controller.POEmailSMSINFOREJECTED-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet POEmailSMSINFO(int PONO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PONO", PONO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_Get_PO_Email_SMS_INFO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_Quotationa_PO_Approval_Controller.POEmailSMSINFO-> " + ex.ToString());
                    }
                    return ds;
                }

            }
        }
    }
}
