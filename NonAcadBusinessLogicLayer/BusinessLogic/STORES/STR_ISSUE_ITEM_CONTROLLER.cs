using System;
using System.Data;
using System.Web;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Collections;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.DAC;
//using BusinessLogicLayer.BusinessEntities.Stores;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class STR_ISSUE_ITEM_CONTROLLER
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>

                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public int AddIssueDetails(STR_ISSUE_ITEMS objIssueItem, ListView lvItem, int reqtrno, int issueno,int SDNO)
                {
                    DAC dc = new DAC();
                    dc.StoredProcedure = "PKG_STR_ISSUE_ITEM_INSERT";
                    dc.Params.Add("@P_COLLEGE_CODE", SqlDbType.NVarChar, objIssueItem._COLLEGE_CODE);
                    dc.Params.Add("@P_REQTRNO", SqlDbType.Int, objIssueItem._REQTRNO);
                    dc.Params.Add("@P_ISSUE_SLIPNO", SqlDbType.NVarChar, objIssueItem._ISSUE_SLIPNO);
                    dc.Params.Add("@P_ISSUE_DATE", SqlDbType.DateTime, objIssueItem._ISSUE_DATE);
                    dc.Params.Add("@P_ISSUE_TYPE", SqlDbType.Int, objIssueItem._ISSUE_TYPE);
                    dc.Params.Add("@P_STATUS", SqlDbType.NVarChar, objIssueItem._STATUS);
                    dc.Params.Add("@P_REMARK", SqlDbType.NVarChar, objIssueItem._REMARK);
                    dc.Params.Add("@P_MDNO", SqlDbType.Int, objIssueItem._MDNO);
                    dc.Params.Add("@P_INVTRNO", SqlDbType.NVarChar, objIssueItem._INVOICENO);
                    dc.Params.Add("@P_SDNO", SqlDbType.Int, SDNO);
                    dc.Params.Add("@P_issue_from", SqlDbType.Char, "I");                     
                    dc.Params.Add("@P_UA_NO", SqlDbType.Int, 0);
                    DAC.Commands.Add(dc);

                    foreach (ListViewDataItem item in lvItem.Items)
                    {
                        DAC dcItem = new DAC();
                        CheckBox chk = item.FindControl("chkSelect") as CheckBox;
                        if (chk.Checked == true)
                        {
                            dcItem.StoredProcedure = "PKG_STR_ISSUE_ITEM_TRAN_INSERT";
                            dcItem.Params.Add("@P_ISSUENO", SqlDbType.Int, issueno);
                            TextBox txtIQ = item.FindControl("txtIQty") as TextBox;
                            dcItem.Params.Add("@P_ISSUED_QTY", SqlDbType.Int, Convert.ToInt32(txtIQ.Text));
                            dcItem.Params.Add("@P_ITEM_NO", SqlDbType.Int, Convert.ToInt32(chk.ToolTip));
                            TextBox txt = item.FindControl("txtQuantity") as TextBox;
                            dcItem.Params.Add("@P_QUANTITY", SqlDbType.Int, Convert.ToInt32(txt.Text));
                            dcItem.Params.Add("@P_SDNO", SqlDbType.Int,SDNO);
                            dcItem.Params.Add("@P_MDNO", SqlDbType.Int, objIssueItem._MDNO);
                            dcItem.Params.Add("@P_ISSUEDATE", SqlDbType.DateTime, objIssueItem._ISSUE_DATE);
                            DAC.Commands.Add(dcItem);


                            
                            AddIssueItemCode(Convert.ToInt32(chk.ToolTip), reqtrno, issueno, Convert.ToInt32(txtIQ.Text));
                        }
                    }

                    DAC dcReq = new DAC();
                    dcReq.StoredProcedure = "PKG_STR_UPDATE_REQ_STATUS";
                    dcReq.Params.Add("@P_REQTRNO",SqlDbType.Int, reqtrno);
                    DAC.Commands.Add(dcReq);
                    DAC.ExecuteBatch();
                    DAC.Commands.Clear();
                    return (int)CustomStatus.RecordSaved;
                }

                public int AddUpdateIssueItemWithSerialNo(STR_ISSUE_ITEMS objIssueItem,int SDNO,string Invidno)
                {
                    int retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[16];
                        objParams[0] = new SqlParameter("@P_ISSUENO", objIssueItem._ISSUENO);
                        objParams[1] = new SqlParameter("@P_ISSUE_SLIPNO", objIssueItem._ISSUE_SLIPNO);
                        objParams[2] = new SqlParameter("@P_ISSUE_DATE", objIssueItem._ISSUE_DATE);
                        objParams[3] = new SqlParameter("@P_ISSUE_TYPE", objIssueItem._ISSUE_TYPE);
                        objParams[4] = new SqlParameter("@P_REQTRNO", objIssueItem._REQTRNO);
                        objParams[5] = new SqlParameter("@P_REMARK", objIssueItem._REMARK);
                        objParams[6] = new SqlParameter("@P_COLLEGE_NO", objIssueItem._COLLEGE_NO);
                        objParams[7] = new SqlParameter("@P_ISSUE_TO_SDNO", objIssueItem._ISSUE_TO_SDNO);
                        objParams[8] = new SqlParameter("@P_ISSUE_TO_IDNO", objIssueItem._ISSUE_TO_IDNO);
                        objParams[9] = new SqlParameter("@P_COLLEGE_CODE", objIssueItem._COLLEGE_CODE);
                        objParams[10] = new SqlParameter("@P_MDNO", objIssueItem._MDNO);
                        objParams[11] = new SqlParameter("@P_SDNO", SDNO);
                        objParams[12] = new SqlParameter("@P_UA_NO", objIssueItem.UA_NO);
                        objParams[13] = new SqlParameter("@P_ItemTbl", objIssueItem.ItemTbl);
                        objParams[14] = new SqlParameter("@P_INVIDNO", Invidno); 
                        objParams[15] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[15].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STR_ISSUE_ITEM_WITH_SERIAL_NO_IU", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_ISSUE_ITEM_CONTROLLER.AddUpdateIssueItemWithSerialNo->" + ex.ToString());
                    }
                    return retstatus;
                }

                public void  AddIssueItemCode(int Item_No,int ReqtrNo,int IssueNo,int IQty)
                {
                    for (int i = 0; i < IQty;i++ )
                    {
                            DAC dcItemTag = new DAC();

                            dcItemTag.StoredProcedure = "PKG_STR_ITEM_TAGNO_GEN_INSERT";
                            dcItemTag.Params.Add("@P_ISSUE_NO", SqlDbType.Int, IssueNo);
                            dcItemTag.Params.Add("@P_REQTRNO", SqlDbType.Int, ReqtrNo);
                            dcItemTag.Params.Add("@P_ITEMNO", SqlDbType.Int, Item_No);
                          
                            DAC.Commands.Add(dcItemTag);
                    }
                 
                }


                public int UpdateIssueDetails(STR_ISSUE_ITEMS objIssueItem, ListView lvItem,int SDNO)
                {
                    DAC dc = new DAC();
                    dc.StoredProcedure = "PKG_STR_ISSUE_ITEM_UPDATE";
                    dc.Params.Add("@P_ISSUENO", SqlDbType.Int, objIssueItem._ISSUENO);
                    dc.Params.Add("@P_COLLEGE_CODE", SqlDbType.NVarChar, objIssueItem._COLLEGE_CODE);
                    dc.Params.Add("@P_ISSUE_DATE", SqlDbType.DateTime, objIssueItem._ISSUE_DATE);
                    dc.Params.Add("@P_ISSUE_TYPE", SqlDbType.Int, objIssueItem._ISSUE_TYPE);
                    dc.Params.Add("@P_REMARK", SqlDbType.NVarChar, objIssueItem._REMARK);
                    dc.Params.Add("@P_MDNO", SqlDbType.Int, objIssueItem._MDNO);
                    dc.Params.Add("@P_INVTRNO", SqlDbType.NVarChar, objIssueItem._INVOICENO);
                    dc.Params.Add("@P_SDNO", SqlDbType.Int, SDNO);
                    DAC.Commands.Add(dc);

                    foreach (ListViewDataItem item in lvItem.Items)
                    {
                        DAC dcItem = new DAC();
                        CheckBox chk = item.FindControl("chkSelect") as CheckBox;
                        if (chk.Checked == true)
                        {
                            dcItem.StoredProcedure = "PKG_STR_ISSUE_ITEM_TRAN_UPDATE";
                            dcItem.Params.Add("@P_ISSUENO", SqlDbType.Int, objIssueItem._ISSUENO);
                            TextBox txtIQ = item.FindControl("txtIQty") as TextBox;
                            dcItem.Params.Add("@P_ISSUED_QTY", SqlDbType.Int, Convert.ToInt32(txtIQ.Text));
                            dcItem.Params.Add("@P_ITEM_NO", SqlDbType.Int, Convert.ToInt32(chk.ToolTip));
                            dcItem.Params.Add("@P_ISSUEDATE", SqlDbType.DateTime, objIssueItem._ISSUE_DATE);

                            DAC.Commands.Add(dcItem);
                        }
                    }
                    DAC.ExecuteBatch();
                    DAC.Commands.Clear();
                    return (int)CustomStatus.RecordUpdated;
                }

                //public int UpdateReqStatus(int reqtrno)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;
                //        objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("@P_REQTRNO", reqtrno);
                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_UPDATE_REQ_STATUS", objParams, false) != null)
                //            retStatus = (int)CustomStatus.RecordSaved;
                //    }
                //    catch (Exception ex)
                //    {
                //        //retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.AddDeptRequest-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}


                //TO BE CANCEL
                public DataSet GetIssueInfoByissueNo(int issueno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ISSUENO", issueno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_ISSUE_INFO_BY_ISSUENO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetTranDetailsByReqNo-> " + ex.ToString());
                    }
                    return ds;
                }


                public int DeleteItem(int issueTno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ISSUE_TNO", issueTno);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_ISSUE_ITEM_TRAN_DEL", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.STR_DEPT_REQ_CONTROLLER.DeleteItem-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetTranDetailsByIssueNo(int IssueNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ISSUENO", IssueNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_ITEM_ISSUE_TRAN_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetTranDetailsByReqNo-> " + ex.ToString());
                    }
                    return ds;
                }
                //public DataSet GetItemDetailsByIssueTNO(int IssueTNo)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null; ;
                //        objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("@P_ISSUETNO", IssueTNo);
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_ITEM_ISSUE_TRAN_GET_BY_TNO", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetTranDetailsByReqNo-> " + ex.ToString());
                //    }
                //    return ds;
                //}
                public DataSet GetItemDetailsByInvoiceNo(int invoiceno, int reqno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_INVTRNO", invoiceno);
                        objParams[1] = new SqlParameter("@P_REQTRNO", reqno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_INVOICE_ITEM_IN_REQ", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetTranDetailsByReqNo-> " + ex.ToString());
                    }
                    return ds;
                }
                public int GetAvaiItemQtyBy(int ITEMNO, int MDNO, int INVTRNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ITEM_NO", ITEMNO);
                        objParams[1] = new SqlParameter("@P_MDNO", MDNO);
                        objParams[2] = new SqlParameter("@P_INVTRNO", INVTRNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_CAL_AVAI_ITEM_QTY", objParams);
                        int AQTY = Convert.ToInt32(ds.Tables[0].Rows[0]["AQTY"]);
                        return AQTY;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetTranDetailsByReqNo-> " + ex.ToString());
                    }

                }
                public DataSet GetItemAvlQty(int ITEMNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ITEM_NO", ITEMNO);
                        //objParams[1] = new SqlParameter("@P_MDNO", MDNO);                        
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_ITEM_AVL_QTY", objParams);                        
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_ISSUE_ITEM_CONTROLLER.GetItemAvlQty-> " + ex.ToString());
                    }
                    return ds;
                }

                // Commented bcz used lookup instead of this
                //public DataSet GetIsssuedReqNo(int sdno)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null; ;
                //        objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("@P_SDNO",sdno);
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_ISSUED_REQ_NO", objParams);

                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetTranDetailsByReqNo-> " + ex.ToString());
                //    }
                //    finally 
                //    {
                //        ds.Dispose();
                //    }
                //    return ds;
                //}

                //15/03/2022 by gayatri
                //public DataSet GetIssuedItemByIssueNo(int IssueNo)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null; ;
                //        objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("@P_ISSUENO", IssueNo);
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_ISSUED_ITEM_BY_ISSUENO", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetTranDetailsByReqNo-> " + ex.ToString());
                //    }
                //    finally
                //        {
                //        ds.Dispose();
                //        }
                //    return ds;
                //}

                //15/03/2022 gayatri
                public DataSet GetIssuedItemByIssueNo(int IssueNo, string ISSUE_TYPE)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ISSUENO", IssueNo);
                        objParams[1] = new SqlParameter("@P_TYPE", ISSUE_TYPE);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_ISSUED_ITEM_BY_ISSUENO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetTranDetailsByReqNo-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }


                public DataSet GetIssuedItemByReqtrno(int reqtrno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_REQTRNO", reqtrno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_ISSUED_ITEM_BY_REQTRNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetTranDetailsByReqNo-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }


                public int RejectedItemInsert(int issueno,int rejQty,string reason,int item_no,int issue_qty)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_ISSUENO", issueno);
                        objParams[1] = new SqlParameter("@P_ITEM_NO", item_no);
                        objParams[2] = new SqlParameter("@P_ISSUED_QTY", issue_qty);
                        objParams[3] = new SqlParameter("@P_REJ_QTY", rejQty);
                        objParams[4] = new SqlParameter("@P_REASON", reason);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STR_REJECTED_ITEM_INSERT", objParams, false);
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
                       
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetTranDetailsByReqNo-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public int UpdateAcceptanceStatus(int issueno, string accRemark, string accDate, string invidno, DataTable dtAcceptedItems)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_ISSUENO", issueno);
                        objParams[1] = new SqlParameter("@P_ACC_REMARK", accRemark);
                        objParams[2] = new SqlParameter("@P_ACC_DATE", Convert.ToDateTime(accDate));
                        objParams[3] = new SqlParameter("@P_INVIDNO", invidno);
                        objParams[4] = new SqlParameter("@P_STORE_ACCEPT_ITEMS", dtAcceptedItems);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKR_STR_UPDATE_ACCEPTANCE_STATUS", objParams, false);
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

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_ISSUE_ITEM_CONTROLLER.UpdateAcceptanceStatus-> " + ex.ToString());
                    }

                    return retstatus;
                }


                public int UpdateAcceptanceStatusByReq(int reqtrno, string status, string accRemark, string accDate, int accqty)
                {
                    object ret;
                    try {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_REQTRNO", reqtrno);
                       objParams[1] = new SqlParameter("@P_STATUS", status);
                       objParams[2] = new SqlParameter("@P_ACC_REMARK", accRemark);
                       objParams[3] = new SqlParameter("@P_ACC_DATE", Convert.ToDateTime(accDate));
                       objParams[4] = new SqlParameter("@P_ACC_QTY", accqty);
                       ret = objSQLHelper.ExecuteNonQuerySP("PKR_STR_UPDATE_ACCEPTANCE_STATUS_BYREQ", objParams, false);

                    }
                    catch(Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetTranDetailsByReqNo-> " + ex.ToString());

                    }
                    return (int)ret;
                }
                public int UpdateRejectedItemDetails(int rejNo,int rejQty, string reason) 
                {
                    int retstatus = 0;
                    
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_REJTRNO", rejNo);
                        objParams[1] = new SqlParameter("@P_REJ_QTY", rejQty);
                        objParams[2] = new SqlParameter("@P_REASON", reason);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STR_UPDATE_REJECTED_ITEMS", objParams, false);
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

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetTranDetailsByReqNo-> " + ex.ToString());
                    }
                    return retstatus;
                }


                public int DeleteRejectedItemDetails(int rejNo)
                {
                    object ret;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_REJTRNO", rejNo);
                        ret = objSQLHelper.ExecuteNonQuerySP("PKG_STR_DELETE_REJECTED_ITEMS", objParams, false);
                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetTranDetailsByReqNo-> " + ex.ToString());
                    }
                    return (int)ret;
                }


                #region Issue Item with serial number

                // It is used to get all DSR Items to issue.
                public DataSet GetAllDSRItemsToIssue(int INVTRNO, string ItemNos)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objsqlhelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_INVTRNO", INVTRNO);
                        objParams[1] = new SqlParameter("@P_ITEMNOS", ItemNos);
                        ds = objsqlhelper.ExecuteDataSetSP("PKG_STR_GET_ITEMS_WITH_SERIAL_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_DSR_Entry_Controller.GetAllDSRItemsToIssue-> " + ex.ToString());
                    }
                    return ds;
                }

                // Get all Items with serial no. to issue.
                public DataSet GetItemsWithSerialNoToIssue(string ItemNos, int MDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objsqlhelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ITEMNOS", ItemNos);
                        objParams[1] = new SqlParameter("@P_MDNO", MDNO);
                        ds = objsqlhelper.ExecuteDataSetSP("PKG_STR_GET_ITEMS_WITH_SERIAL_NO_BY_ITEMNOS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_DSR_Entry_Controller.GetItemsWithSerialNoToIssue-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion



            }

        }
    }
}
