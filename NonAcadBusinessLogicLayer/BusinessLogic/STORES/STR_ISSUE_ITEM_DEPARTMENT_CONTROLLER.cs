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
            public class STR_ISSUE_ITEM_DEPARTMENT_CONTROLLER
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>

                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public int AddIssueDetails(STR_ISSUE_ITEMS objIssueItem, ListView lvItem, int reqtrno, int issueno, int sdno, int uano)
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
                    dc.Params.Add("@P_SDNO", SqlDbType.Int, sdno);
                    dc.Params.Add("@P_issue_from", SqlDbType.Char, "A");
                    dc.Params.Add("@P_UA_NO", SqlDbType.Int, uano);
                    DAC.Commands.Add(dc);

                    foreach (ListViewDataItem item in lvItem.Items)
                    {
                        DAC dcItem = new DAC();

                        dcItem.StoredProcedure = "PKG_STR_ISSUE_ITEM_TRAN_INSERT_ASSET";
                        dcItem.Params.Add("@P_ISSUENO", SqlDbType.Int, issueno);
                        TextBox txtIQ = item.FindControl("txtIQty") as TextBox;
                        Label lblItem = item.FindControl("lblItem") as Label;

                        dcItem.Params.Add("@P_ISSUED_QTY", SqlDbType.Int, Convert.ToInt32("1"));
                        dcItem.Params.Add("@P_ITEM_NO", SqlDbType.Int, Convert.ToInt32(lblItem.ToolTip.ToString()));
                        TextBox txt = item.FindControl("txtQuantity") as TextBox;
                        dcItem.Params.Add("@P_QUANTITY", SqlDbType.Int, Convert.ToInt32("1"));
                        TextBox txtRate = item.FindControl("txtRates") as TextBox;
                        dcItem.Params.Add("@P_rate", SqlDbType.Decimal, Convert.ToDecimal(txtRate.Text));

                        dcItem.Params.Add("@P_ISSUE_DATE", SqlDbType.DateTime, objIssueItem._ISSUE_DATE);
                        dcItem.Params.Add("@P_SDNO", SqlDbType.Int, sdno);
                        TextBox txtSrno = item.FindControl("txtSrno") as TextBox;
                        dcItem.Params.Add("@P_serial_no", SqlDbType.VarChar, txtSrno.Text);

                        TextBox txtBrnd = item.FindControl("txtBrand") as TextBox;
                        dcItem.Params.Add("@P_BRAND", SqlDbType.NVarChar, Convert.ToString(txtBrnd.Text.Trim()));

                        TextBox txtModl = item.FindControl("txtModel") as TextBox;
                        dcItem.Params.Add("@P_MODEL", SqlDbType.NVarChar, Convert.ToString(txtModl.Text.Trim()));

                        TextBox txtManufact = item.FindControl("txtManufacturer") as TextBox;
                        dcItem.Params.Add("@P_MANUFACTURER", SqlDbType.NVarChar, Convert.ToString(txtManufact.Text.Trim()));

                        TextBox txtDateofPurchs = item.FindControl("txtDateofPurchase") as TextBox;

                        if (txtDateofPurchs.Text != string.Empty)
                        //    dcItem.Params.Add("@P_DATEOF_PURCHASE", SqlDbType.DateTime, DateTime.MinValue);
                        //else
                            dcItem.Params.Add("@P_DATEOF_PURCHASE", SqlDbType.DateTime, Convert.ToDateTime(txtDateofPurchs.Text));

                        TextBox txtEndofWarrenty = item.FindControl("txtEndofWarrenty") as TextBox;

                        if (txtEndofWarrenty.Text != string.Empty)
                        //    dcItem.Params.Add("@P_ENDOF_WARRENTY_PERIOD", SqlDbType.DateTime, DateTime.MinValue);
                        //else
                            dcItem.Params.Add("@P_ENDOF_WARRENTY_PERIOD", SqlDbType.DateTime, Convert.ToDateTime(txtEndofWarrenty.Text));

                        DropDownList ddlvendr = item.FindControl("ddlVender") as DropDownList;
                        dcItem.Params.Add("@P_PNO", SqlDbType.Int, Convert.ToInt32(ddlvendr.SelectedValue));

                        TextBox txtRemrk = item.FindControl("txtRemark") as TextBox;
                        dcItem.Params.Add("@P_REMARK", SqlDbType.NVarChar, Convert.ToString(txtRemrk.Text.Trim()));

                        DropDownList ddluse = item.FindControl("ddluse") as DropDownList;
                        dcItem.Params.Add("@P_condition", SqlDbType.VarChar, ddluse.SelectedItem.Text);
                        DAC.Commands.Add(dcItem);


                    }

                    DAC dcReq = new DAC();
                    dcReq.StoredProcedure = "PKG_STR_UPDATE_REQ_STATUS";
                    dcReq.Params.Add("@P_REQTRNO", SqlDbType.Int, reqtrno);
                    DAC.Commands.Add(dcReq);
                    DAC.ExecuteBatch();
                    DAC.Commands.Clear();
                    return (int)CustomStatus.RecordSaved;
                }

                public int ReturnItem(STR_ISSUE_ITEMS objIssueItem, int itemno, string issuetno, int sdno,string Remark,string condition)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_COLLEGE_CODE", objIssueItem._COLLEGE_CODE);
                        objParams[1] = new SqlParameter("@P_ISSUE_SLIPNO", objIssueItem._ISSUE_SLIPNO);
                        objParams[2] = new SqlParameter("@P_ISSUE_DATE", objIssueItem._ISSUE_DATE);

                        objParams[3] = new SqlParameter("@P_SDNO", sdno);
                        objParams[4] = new SqlParameter("@P_ITEM_NO", itemno);
                        objParams[5] = new SqlParameter("@P_ISSUE_TNO", issuetno);
                        objParams[6] = new SqlParameter("@P_REMARK", Remark);
                        objParams[7] = new SqlParameter("@P_condition", condition);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_RETURN_ITEM_ASSET", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.STR_DEPT_REQ_CONTROLLER.DeleteItem-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateIssueDetails(STR_ISSUE_ITEMS objIssueItem, ListView lvItem, int sdno)
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
                    dc.Params.Add("@P_SDNO", SqlDbType.Int, sdno);
                    DAC.Commands.Add(dc);


                    //DAC dcItemDel = new DAC();
                    //dcItemDel.StoredProcedure = "PKG_STR_ISSUE_ITEM_DELETE";
                    //dcItemDel.Params.Add("@P_ISSUENO", SqlDbType.Int, objIssueItem._ISSUENO);
                    //DAC.Commands.Add(dcItemDel);


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
                            TextBox txtRate = item.FindControl("txtRates") as TextBox;
                            dcItem.Params.Add("@P_RATE", SqlDbType.Decimal, Convert.ToDecimal(txtRate.Text));
                            TextBox txtAmt = item.FindControl("txtAmt") as TextBox;
                            dcItem.Params.Add("@P_AMOUNT", SqlDbType.Decimal, Convert.ToDecimal(txtAmt.Text));
                            DAC.Commands.Add(dcItem);
                        }
                    }

                    foreach (ListViewDataItem item in lvItem.Items)
                    {
                        DAC dcItem = new DAC();
                        CheckBox chk = item.FindControl("chkSelect") as CheckBox;

                        if (chk.Checked == true)
                        {
                            dcItem.StoredProcedure = "PKG_STR_ISSUE_ITEM_TRAN_INSERT";
                            dcItem.Params.Add("@P_ISSUENO", SqlDbType.Int, objIssueItem._ISSUENO);
                            TextBox txtIQ = item.FindControl("txtIQty") as TextBox;
                            dcItem.Params.Add("@P_ISSUED_QTY", SqlDbType.Int, Convert.ToInt32(txtIQ.Text));
                            dcItem.Params.Add("@P_ITEM_NO", SqlDbType.Int, Convert.ToInt32(chk.ToolTip));
                            TextBox txt = item.FindControl("txtQuantity") as TextBox;
                            dcItem.Params.Add("@P_QUANTITY", SqlDbType.Int, Convert.ToInt32(txt.Text));
                            TextBox txtRate = item.FindControl("txtRates") as TextBox;
                            dcItem.Params.Add("@P_RATE", SqlDbType.Decimal, Convert.ToDecimal(txtRate.Text));
                            TextBox txtAmt = item.FindControl("txtAmt") as TextBox;
                            dcItem.Params.Add("@P_AMOUNT", SqlDbType.Decimal, Convert.ToDecimal(txtAmt.Text));
                            dcItem.Params.Add("@P_ISSUE_DATE", SqlDbType.DateTime, objIssueItem._ISSUE_DATE);
                            dcItem.Params.Add("@P_INVTRNO", SqlDbType.NVarChar, objIssueItem._INVOICENO);

                            DAC.Commands.Add(dcItem);
                        }
                    }



                    DAC.ExecuteBatch();
                    DAC.Commands.Clear();
                    return (int)CustomStatus.RecordUpdated;
                }

                public int DeptwiseUpdateIssueDetails(STR_ISSUE_ITEMS objIssueItem, ListView lvItem, int sdno)
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
                    dc.Params.Add("@P_SDNO", SqlDbType.Int, sdno);
                    DAC.Commands.Add(dc);


                    DAC dcItemDel = new DAC();
                    dcItemDel.StoredProcedure = "PKG_STR_ISSUE_ITEM_DELETE";
                    dcItemDel.Params.Add("@P_ISSUENO", SqlDbType.Int, objIssueItem._ISSUENO);
                    DAC.Commands.Add(dcItemDel);



                    foreach (ListViewDataItem item in lvItem.Items)
                    {
                        DAC dcItem = new DAC();
                        CheckBox chk = item.FindControl("chkSelect") as CheckBox;

                        if (chk.Checked == true)
                        {
                            dcItem.StoredProcedure = "PKG_STR_ISSUE_ITEM_TRAN_INSERT";
                            dcItem.Params.Add("@P_ISSUENO", SqlDbType.Int, objIssueItem._ISSUENO);
                            TextBox txtIQ = item.FindControl("txtIQty") as TextBox;
                            dcItem.Params.Add("@P_ISSUED_QTY", SqlDbType.Int, Convert.ToInt32(txtIQ.Text));
                            dcItem.Params.Add("@P_ITEM_NO", SqlDbType.Int, Convert.ToInt32(chk.ToolTip));
                            TextBox txt = item.FindControl("txtQuantity") as TextBox;
                            dcItem.Params.Add("@P_QUANTITY", SqlDbType.Int, Convert.ToInt32(txt.Text));
                            TextBox txtRate = item.FindControl("txtRates") as TextBox;
                            dcItem.Params.Add("@P_RATE", SqlDbType.Decimal, Convert.ToDecimal(txtRate.Text));
                            TextBox txtAmt = item.FindControl("txtAmt") as TextBox;
                            dcItem.Params.Add("@P_AMOUNT", SqlDbType.Decimal, Convert.ToDecimal(txtAmt.Text));
                            dcItem.Params.Add("@P_ISSUE_DATE", SqlDbType.DateTime, objIssueItem._ISSUE_DATE);
                            dcItem.Params.Add("@P_INVTRNO", SqlDbType.NVarChar, objIssueItem._INVOICENO);

                            DAC.Commands.Add(dcItem);
                        }
                    }



                    DAC.ExecuteBatch();
                    DAC.Commands.Clear();
                    return (int)CustomStatus.RecordUpdated;
                }



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

                public DataSet GetItemDetailsByInvoiceNo(int invoiceno, int reqno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                         new SqlParameter("@P_INVTRNO", invoiceno),
                         new SqlParameter("@P_REQTRNO", reqno),
                        };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_INVOICE_ITEM_IN_REQ", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetTranDetailsByReqNo-> " + ex.ToString());
                    }
                    return ds;
                }
                public int GetAvaiItemQtyBy(int issueno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                        new SqlParameter("@P_ITEM_NO", issueno)
                        };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_CAL_AVAI_ITEM_QTY", objParams);
                        int AQTY = Convert.ToInt32(ds.Tables[0].Rows[0]["AQTY"]);
                        return AQTY;
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetTranDetailsByReqNo-> " + ex.ToString());
                    }

                }

                public DataSet GetIsssuedReqNo()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_ISSUED_REQ_NO", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetTranDetailsByReqNo-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetIssuedItemByIssueNo(int IssueNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                         new SqlParameter("@P_ISSUENO", IssueNo)
                        };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_ISSUED_ITEM_BY_ISSUENO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetTranDetailsByReqNo-> " + ex.ToString());
                    }
                    return ds;
                }


                public int RejectedItemInsert(int issueno, int rejQty, string reason, int item_no, int issue_qty)
                {
                    object ret;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                       new SqlParameter("@P_ISSUENO", issueno),
                       new SqlParameter("@P_ITEM_NO", item_no),
                       new SqlParameter("@P_ISSUED_QTY", issue_qty),
                       new SqlParameter("@P_REJ_QTY", rejQty),
                       new SqlParameter("@P_REASON", reason)
                    };
                        ret = objSQLHelper.ExecuteNonQuerySP("PKG_STR_REJECTED_ITEM_INSERT", objParams, false);
                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetTranDetailsByReqNo-> " + ex.ToString());
                    }
                    return (int)ret;
                }

                public int UpdateAcceptanceStatus(int issueno, string status, string accRemark, string accDate)
                {
                    object ret;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                             new SqlParameter("@P_ISSUENO", issueno),
                             new SqlParameter("@P_STATUS", status),
                             new SqlParameter("@P_ACC_REMARK", accRemark),
                             new SqlParameter("@P_ACC_DATE", Convert.ToDateTime(accDate))
                        };
                        ret = objSQLHelper.ExecuteNonQuerySP("PKR_STR_UPDATE_ACCEPTANCE_STATUS", objParams, false);
                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetTranDetailsByReqNo-> " + ex.ToString());
                    }
                    return (int)ret;
                }


                public int UpdateRejectedItemDetails(int rejNo, int rejQty, string reason)
                {
                    object ret;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                           new SqlParameter("@P_REJTRNO", rejNo),
                           new SqlParameter("@P_REJ_QTY", rejQty),
                           new SqlParameter("@P_REASON", reason)
                        };
                        ret = objSQLHelper.ExecuteNonQuerySP("PKG_STR_UPDATE_REJECTED_ITEMS", objParams, false);
                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetTranDetailsByReqNo-> " + ex.ToString());
                    }
                    return (int)ret;
                }
                public int DeleteRejectedItemDetails(int rejNo)
                {
                    object ret;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                        {
                           new SqlParameter("@P_REJTRNO", rejNo)
                        };
                        ret = objSQLHelper.ExecuteNonQuerySP("PKG_STR_DELETE_REJECTED_ITEMS", objParams, false);
                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetTranDetailsByReqNo-> " + ex.ToString());
                    }
                    return (int)ret;
                }

                // added for issue item dept wise

                public int AddItems(int itemno, int qty, decimal rate, decimal amount, int issueno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                       {
                         new SqlParameter("@P_ITEMNO", itemno),
                         new SqlParameter("@P_QTY", qty),
                         new SqlParameter("@P_RATE",rate),
                         new SqlParameter("@P_AMOUNT",amount),
                         new SqlParameter("@P_ISSUENO",issueno)
                       };
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_ISSUE_TEMP_ITEM_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }


                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.AddDeptRequest-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetIssueItem(int issueno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ISSUENO", issueno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_ISSUE_GET_ISSUEITEM_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetTranDetailsByReqNo-> " + ex.ToString());
                    }
                    return ds;
                }


                //get Item for edit
                public DataSet GetItemByTranId(int issue_tranno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper ObjSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] ObjParam = new SqlParameter[]
                        {
                          new SqlParameter("@P_ISSUE_TRAN_NO",issue_tranno)
                        };
                        ds = ObjSqlHelper.ExecuteDataSetSP("PKG_STR_GETITEM_FOREDIT", ObjParam);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetTranDetailsByReqNo-> " + ex.ToString());
                    }
                    return ds;

                }

                //update Item details

                public int EditItemDetail(int issuetrno, int itemno, int qty, decimal rate, decimal amount)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper ObjSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] ObjParam = new SqlParameter[]
                        {
                           new SqlParameter("@P_ISSUE_TRANNO", issuetrno),
                           new SqlParameter("@P_ITEM_NO", itemno),
                           new SqlParameter("@P_QTY", qty),
                           new SqlParameter("@P_RATE",rate),
                           new SqlParameter("@P_AMOUNT",amount)
                        };
                        if (ObjSqlHelper.ExecuteNonQuerySP("PKG_STR_UPDATE_ISSUE_ITEM", ObjParam, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetTranDetailsByReqNo-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //Delete issue temp item

                public int DeleteIssueItem(int issuetrno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper ObjSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] ObjParam = null;
                        ObjParam = new SqlParameter[1];
                        ObjParam[0] = new SqlParameter("@P_ISSUETRNO", issuetrno);
                        if (ObjSqlHelper.ExecuteNonQuerySP("PKG_STR_DELETEITEM", ObjParam, true) != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                        }
                    }
                    catch (Exception ex)
                    {
                        return retStatus;
                    }

                    return retStatus;
                }


                public int DeleteIssueTempItems(int issueno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper ObjSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] ObjParam = null;
                        ObjParam = new SqlParameter[1];
                        ObjParam[0] = new SqlParameter("@P_ISSUENO", issueno);
                        if (ObjSqlHelper.ExecuteNonQuerySP("PKG_STR_DELETE_TEMPISSUE_ITEM", ObjParam, true) != null)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                        }
                    }
                    catch (Exception ex)
                    {
                        return retStatus;
                    }

                    return retStatus;
                }

                public DataSet getCurrentRate(int itemNo)
                {
                    DataSet dsRate = new DataSet();
                    try
                    {
                        SQLHelper ObjSqlHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] ObjParam = null;
                        ObjParam = new SqlParameter[1];
                        ObjParam[0] = new SqlParameter("@P_ITEM_NO", itemNo);
                        dsRate = ObjSqlHelper.ExecuteDataSetSP("PKG_STR_GET_CURRENT_RATE", ObjParam);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_ISSUE_ITEM_DEPARTMENT_CONTROLLER.cs.getCurrentRate-> " + ex.ToString());
                    }
                    return dsRate;
                }
            }

        }
    }
}
