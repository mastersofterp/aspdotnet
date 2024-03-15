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
            public class Str_JvStockCon
            {

                /// <summary>
                /// ConnectionStrings
                /// </summary>

                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public int AddIssueDetails(STR_ISSUE_ITEMS objIssueItem, ListView lvItem, int reqtrno, int issueno, int SDNO)
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
                            dcItem.Params.Add("@P_SDNO", SqlDbType.Int, SDNO);
                            dcItem.Params.Add("@P_MDNO", SqlDbType.Int, objIssueItem._MDNO);
                            dcItem.Params.Add("@P_ISSUEDATE", SqlDbType.DateTime, objIssueItem._ISSUE_DATE);
                            DAC.Commands.Add(dcItem);



                            AddIssueItemCode(Convert.ToInt32(chk.ToolTip), reqtrno, issueno, Convert.ToInt32(txtIQ.Text));
                        }
                    }

                    DAC dcReq = new DAC();
                    dcReq.StoredProcedure = "PKG_STR_UPDATE_REQ_STATUS";
                    dcReq.Params.Add("@P_REQTRNO", SqlDbType.Int, reqtrno);
                    DAC.Commands.Add(dcReq);
                    DAC.ExecuteBatch();
                    DAC.Commands.Clear();
                    return (int)CustomStatus.RecordSaved;
                }

                public int AddUpdateJvStock(Str_JvStockEnt objJvEnt)
                {
                    int retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[22];
                        objParams[0] = new SqlParameter("@P_JVTRAN_ID", objJvEnt.JVTRAN_ID);
                        objParams[1] = new SqlParameter("@P_JVTRAN_SLIP_NO", objJvEnt.JVTRAN_SLIP_NO);
                        objParams[2] = new SqlParameter("@P_TRAN_DATE", objJvEnt.TRAN_DATE);
                        objParams[3] = new SqlParameter("@P_JVTRAN_TYPE", objJvEnt.JVTRAN_TYPE);
                        objParams[4] = new SqlParameter("@P_FROM_COLLEGE", objJvEnt.FROM_COLLEGE);
                        objParams[5] = new SqlParameter("@P_FROM_DEPT", objJvEnt.FROM_DEPT);
                        objParams[6] = new SqlParameter("@P_FROM_EMPLOYEE", objJvEnt.FROM_EMPLOYEE);
                        objParams[7] = new SqlParameter("@P_TO_COLLEGE", objJvEnt.TO_COLLEGE);
                        objParams[8] = new SqlParameter("@P_TO_DEPT", objJvEnt.TO_DEPT);
                        objParams[9] = new SqlParameter("@P_TO_EMPLOYEE", objJvEnt.TO_EMPLOYEE);
                        objParams[10] = new SqlParameter("@P_REMARK", objJvEnt.REMARK);
                        objParams[11] = new SqlParameter("@P_STORE_USER_TYPE", objJvEnt.STORE_USER_TYPE);
                        objParams[12] = new SqlParameter("@P_REQTRNO", objJvEnt.REQTRNO);
                        objParams[13] = new SqlParameter("@P_ISSUE_TYPE", objJvEnt.ISSUE_TYPE);
                        objParams[14] = new SqlParameter("@P_JV_ITEM_TBL", objJvEnt.JV_ITEM_TBL);
                        //objParams[15] = new SqlParameter("@P_INVIDNO", objJvEnt.INVIDNO);
                        objParams[15] = new SqlParameter("@P_JV_INV_ITEM_TBL", objJvEnt.INV_ITEM_TBL); //18-07-2023 for insert invoice qty and rate
                        objParams[16] = new SqlParameter("@P_CREATED_BY", objJvEnt.CREATED_BY);
                        objParams[17] = new SqlParameter("@P_MODIFIED_BY", objJvEnt.MODIFIED_BY);
                        objParams[18] = new SqlParameter("@P_COLLEGE_CODE", objJvEnt.COLLEGE_CODE);
                        objParams[19] = new SqlParameter("@P_LOCATIONNO", objJvEnt.LOCATIONNO);              //---31/10/2022
                        objParams[20] = new SqlParameter("@P_StudentIdno", objJvEnt.StudentIdno);              //---31/10/2022
                        objParams[21] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[21].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STR_JVSTCOK_INS_UPD", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_JvStockCon.AddUpdateJvStock->" + ex.ToString());
                    }
                    return retstatus;
                }

                public void AddIssueItemCode(int Item_No, int ReqtrNo, int IssueNo, int IQty)
                {
                    for (int i = 0; i < IQty; i++)
                    {
                        DAC dcItemTag = new DAC();

                        dcItemTag.StoredProcedure = "PKG_STR_ITEM_TAGNO_GEN_INSERT";
                        dcItemTag.Params.Add("@P_ISSUE_NO", SqlDbType.Int, IssueNo);
                        dcItemTag.Params.Add("@P_REQTRNO", SqlDbType.Int, ReqtrNo);
                        dcItemTag.Params.Add("@P_ITEMNO", SqlDbType.Int, Item_No);

                        DAC.Commands.Add(dcItemTag);
                    }

                }

                public int UpdateIssueDetails(STR_ISSUE_ITEMS objIssueItem, ListView lvItem, int SDNO)
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_JvStockCon.GetTranDetailsByReqNo-> " + ex.ToString());
                    }
                    return ds;
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_JvStockCon.GetTranDetailsByReqNo-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetConsumableItemDetBySubCategory(int SubCategoryNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_MISGNO", SubCategoryNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_JV_GET_CONSUMABLE_ITEM_DET_BY_MISGNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_JvStockCon.GetConsumableItemAvlQty-> " + ex.ToString());
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_JvStockCon.GetTranDetailsByReqNo-> " + ex.ToString());
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_JvStockCon.GetItemAvlQty-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetIssuedItemByIssueNo(int IssueNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ISSUENO", IssueNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_ISSUED_ITEM_BY_ISSUENO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_JvStockCon.GetTranDetailsByReqNo-> " + ex.ToString());
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_JvStockCon.GetTranDetailsByReqNo-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                public DataSet GetItemByReqNo(int Reqtrno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_REQTRNO", Reqtrno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_ITEM_BY_REQ_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_JvStockCon.GetBudget()-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GenrateJvTranSlipNo(int TRAN_TYPE_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TRAN_TYPE_NO", TRAN_TYPE_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_JV_GENERATE_TRAN_SLIP_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_JvStockCon.GenrateJvTranSlipNo-> " + ex.ToString());
                    }
                    return ds;
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

                #region Item repair
                public DataSet GetItemsForRepair(int Misgno, int ItemNo, string ItemIn, int CollegeNo, int DeptNo,string Action)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_MISGNO", Misgno);
                        objParams[1] = new SqlParameter("@P_ITEMNO", ItemNo);
                        objParams[2] = new SqlParameter("@P_ITEMIN", ItemIn);
                        objParams[3] = new SqlParameter("@P_COLLEGENO", CollegeNo);
                        objParams[4] = new SqlParameter("@P_DEPTNO", DeptNo);
                          objParams[5] = new SqlParameter("@P_ACTION", Action);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_ITEMS_FOR_REPAIR", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_JvStockCon.GetItemsForRepair()-> " + ex.ToString());
                    }
                    return ds;
                }

                //public int InsUpdateItemRepair(Str_JvStockEnt objJvEnt)
                //{
                //    int retstatus = 0;
                //    try
                //    {

                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;

                //        objParams = new SqlParameter[18];
                //        objParams[0] = new SqlParameter("@P_IR_ID", objJvEnt.IR_ID);
                //        objParams[1] = new SqlParameter("@P_GATEPASS_NO", objJvEnt.GATEPASS_NO);
                //        objParams[2] = new SqlParameter("@P_TRAN_TYPE", objJvEnt.TRAN_TYPE);
                //        objParams[3] = new SqlParameter("@P_ITEM_IN", objJvEnt.ITEM_IN);
                //        objParams[4] = new SqlParameter("@P_ITEM_NO", objJvEnt.ITEM_NO);
                //        objParams[5] = new SqlParameter("@P_COLLEGE_NO", objJvEnt.COLLEGE_NO);
                //        objParams[6] = new SqlParameter("@P_DEPT_NO", objJvEnt.DEPT_NO);
                //        objParams[7] = new SqlParameter("@P_TO_DEPTNO", objJvEnt.TO_DEPT);
                //        objParams[8] = new SqlParameter("@P_TO_EMPLOYEE", objJvEnt.TO_EMPLOYEE);
                //        objParams[9] = new SqlParameter("@P_EMP_FROM", objJvEnt.EMP_FROM);
                //        objParams[10] = new SqlParameter("@P_CARRY_EMP_NAME", objJvEnt.CARRY_EMP_NAME);
                //        objParams[11] = new SqlParameter("@P_CARRY_EMP_MBL_NO", objJvEnt.CARRY_EMP_MBL_NO);
                //        objParams[12] = new SqlParameter("@P_SENT_TO", objJvEnt.SENT_TO);
                //        if (!objJvEnt.RETURN_DATE.Equals(DateTime.MinValue))
                //            objParams[13] = new SqlParameter("@P_RETURN_DATE", objJvEnt.RETURN_DATE);
                //        else
                //            objParams[13] = new SqlParameter("@P_RETURN_DATE", DBNull.Value);
                //        objParams[14] = new SqlParameter("@P_CREATED_BY", objJvEnt.CREATED_BY);
                //        objParams[15] = new SqlParameter("@P_MODIFIED_BY", objJvEnt.MODIFIED_BY);
                //        objParams[16] = new SqlParameter("@P_ITEM_REPAIR_TBL", objJvEnt.ITEM_REPAIR_TBL);
                //        objParams[17] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //        objParams[17].Direction = ParameterDirection.Output;

                //        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STR_ITEM_REPAIR_INS_UPD", objParams, true);
                //        if (Convert.ToInt32(ret) == -99)
                //        {
                //            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //        }
                //        else
                //        {
                //            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        retstatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_JvStockCon.InsUpdateItemRepair->" + ex.ToString());
                //    }
                //    return retstatus;
                //}


                //------------------------------------21/04/2022------------------------------------

                public int InsUpdateItemRepair(Str_JvStockEnt objJvEnt)
                {
                    int retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[23];
                        objParams[0] = new SqlParameter("@P_IR_ID", objJvEnt.IR_ID);
                        objParams[1] = new SqlParameter("@P_GATEPASS_NO", objJvEnt.GATEPASS_NO);
                        objParams[2] = new SqlParameter("@P_TRAN_TYPE", objJvEnt.TRAN_TYPE);
                        objParams[3] = new SqlParameter("@P_ITEM_IN", objJvEnt.ITEM_IN);
                        objParams[4] = new SqlParameter("@P_ITEM_NO", objJvEnt.ITEM_NO);
                        objParams[5] = new SqlParameter("@P_COLLEGE_NO", objJvEnt.COLLEGE_NO);
                        objParams[6] = new SqlParameter("@P_DEPT_NO", objJvEnt.DEPT_NO);
                        objParams[7] = new SqlParameter("@P_TO_DEPTNO", objJvEnt.TO_DEPT);
                        objParams[8] = new SqlParameter("@P_TO_EMPLOYEE", objJvEnt.TO_EMPLOYEE);
                        objParams[9] = new SqlParameter("@P_EMP_FROM", objJvEnt.EMP_FROM);
                        objParams[10] = new SqlParameter("@P_CARRY_EMP_NAME", objJvEnt.CARRY_EMP_NAME);
                        objParams[11] = new SqlParameter("@P_CARRY_EMP_MBL_NO", objJvEnt.CARRY_EMP_MBL_NO);
                        objParams[12] = new SqlParameter("@P_SENT_TO", objJvEnt.SENT_TO);
                        if (!objJvEnt.RETURN_DATE.Equals(DateTime.MinValue))
                            objParams[13] = new SqlParameter("@P_RETURN_DATE", objJvEnt.RETURN_DATE);
                        else
                            objParams[13] = new SqlParameter("@P_RETURN_DATE", DBNull.Value);
                        objParams[14] = new SqlParameter("@P_CREATED_BY", objJvEnt.CREATED_BY);
                        objParams[15] = new SqlParameter("@P_MODIFIED_BY", objJvEnt.MODIFIED_BY);
                        objParams[16] = new SqlParameter("@P_ITEM_REPAIR_TBL", objJvEnt.ITEM_REPAIR_TBL);

                        objParams[17] = new SqlParameter("@P_VEHICLE_NO", objJvEnt.VEHICLE_NO);
                        if (!objJvEnt.OUT_DATE.Equals(DateTime.MinValue))
                            objParams[18] = new SqlParameter("@P_OUT_DATE", objJvEnt.OUT_DATE);
                        else
                            objParams[18] = new SqlParameter("@P_OUT_DATE", DBNull.Value);

                        if (!objJvEnt.OUT_TIME.Equals(DateTime.MinValue))
                            objParams[19] = new SqlParameter("@P_OUT_TIME", objJvEnt.OUT_TIME);
                        else
                            objParams[19] = new SqlParameter("@P_OUT_TIME", DBNull.Value);

                        if (!objJvEnt.RECEIVED_DATE.Equals(DateTime.MinValue))
                            objParams[20] = new SqlParameter("@P_RECEIVED_DATE", objJvEnt.RECEIVED_DATE);
                        else
                            objParams[20] = new SqlParameter("@P_RECEIVED_DATE", DBNull.Value);

                        if (!objJvEnt.RECEIVED_TIME.Equals(string.Empty))
                            objParams[21] = new SqlParameter("@P_RECEIVED_TIME", objJvEnt.RECEIVED_TIME);
                        else
                            objParams[21] = new SqlParameter("@P_RECEIVED_TIME", DBNull.Value);


                        objParams[22] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[22].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STR_ITEM_REPAIR_INS_UPD", objParams, true);
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_JvStockCon.InsUpdateItemRepair->" + ex.ToString());
                    }
                    return retstatus;
                }
                //------------------------------end------------------------------------------------

                public DataSet GetItemRepairDetailsForEdit(int IrId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IRID", IrId);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_ITEM_REPAIR_DETAILS_FOR_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_JvStockCon.GetItemRepairDetails-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetItemRepairDetailsOw_Ret(int IrId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_IRID", IrId);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_ITEM_REPAIR_DETAILS_FOR_OW_RET", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_JvStockCon.GetItemRepairDetailsOw_Ret-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GenrateGatePassNo()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GENERATE_GATEPASS_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_JvStockCon.GenrateGatePassNo-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                #region GetCondemnItemSale

                // Created By: Shabina 
                // Created Date: 13/08/2021
                // Method Name: GetCondemnItemSale
                // Method Parameters: string ItemNo
                // Method Description: Get the Data of Condemned Item Sale 
                public DataSet GetCondemnItemSale(string ItemNo, string txtToDate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_DSR_NUMBER", ItemNo);
                        objParams[1] = new SqlParameter("@P_TODATE", txtToDate);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_INS_DEPR_CAL_DATA", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_JvStockCon.GetCondemnItemSale-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                #region InserUpdateCondemnedSaleItem
                // Created By: Shabina 
                // Created Date: 16/08/2021
                // Method Name: InsUpdateCondemnedSaleItem
                // Method Parameters: Str_JvStockEnt objJvStockEnt
                // Method Description: Insert and Update the Data of Condemned Item Sale 
                public int InsUpdateCondemnedSaleItem(Str_JvStockEnt objJvStockEnt)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_CIS_ID", objJvStockEnt.CIS_ID);
                        objParams[1] = new SqlParameter("@P_COND_SALE_TRNO", objJvStockEnt.COND_SALE_TRNO);
                        objParams[2] = new SqlParameter("@P_ITEM_NO", objJvStockEnt.ITEMNO);
                        objParams[3] = new SqlParameter("@P_SALE_DATE", objJvStockEnt.SALE_DATE);
                        objParams[4] = new SqlParameter("@P_PNO", objJvStockEnt.PNO);
                        objParams[5] = new SqlParameter("@P_CREATED_BY", objJvStockEnt.CREATED_BY);
                        objParams[6] = new SqlParameter("@P_MODIFIED_BY", objJvStockEnt.MODIFIED_BY);
                        objParams[7] = new SqlParameter("@P_SALE_SAVE_TABLE", objJvStockEnt.SALE_SAVE_TABLE);
                        objParams[8] = new SqlParameter("@P_OUT ", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_CONDEMNED_SALE_ITEM_INS_UPD", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                        //object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STR_CONDEMNED_SALE_ITEM_INS_UPD", objParams, true);
                        //if (Convert.ToInt32(ret) == -99)
                        //{
                        //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        //}
                        //else
                        //{
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        //}

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_JvStockCon.InsUpdateCondemnedSaleItem-> " + ex.ToString());
                    }
                    return retStatus;
                }
                #endregion


                #region GetCondSaleTRNumber
                // Created By: Shabina 
                // Created Date: 17/08/2021
                // Method Name: GetCondSaleTRNumber
                // Method Parameters: Str_JvStockEnt objJvStockEnt
                // Method Description: Insert and Update the Data of Condemned Item Sale 
                public DataSet GetCondSaleTRNumber()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GENERATE_COND_SALETRNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_JvStockCon.GetCondSaleTRNumber-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                #region GetCONDEMNED_SALEDetails
                // Created By: Shabina 
                // Created Date: 17/08/2021
                // Method Name: GetCONDEMNED_SALEDetails
                // Method Parameters: Str_JvStockEnt objJvStockEnt
                // Method Description: Get the Data of Condemned Sale Detail
                public DataSet GetCONDEMNED_SALEDetails()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_CONDEMNED_SALE_DETAIL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_JvStockCon.GetCONDEMNED_SALEDetails-> " + ex.ToString());
                    }
                    return ds;

                }
                #endregion


                public DataSet GetCondSaleDetailsById(int CIS_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_CIS_ID", CIS_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GETCOND_SALE_DETAIL_ByID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_JvStockCon.GetCondSaleDetailsById-> " + ex.ToString());
                    }
                    return ds;
                }
                public int UpdateStatus(int Vpid, char Status)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_CIS_ID", Vpid);
                        objParams[1] = new SqlParameter("@P_STATUS", Status);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_COND_ITEM_SALE_APPROVE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_JvStockCon.UpdateStatus-> " + ex.ToString());
                    }
                    return retStatus;
                }


                #region GetDropDownREQ
                // Created By: Shabina 
                // Created Date: 13/09/2021
                // Method Name: GetDropDownREQ
                // Method Parameters: Str_JvStockEnt objJvStockEnt
                // Method Description: Get Requisition no of those issued quantity are balance not get here

                public DataSet GetDropDownREQ()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        //objParams[0] = new SqlParameter("@P_REQTRNO", REQNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STORE_GET_PO_LIST_FOR_JVSTOCK", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_JvStockCon.GetDropDownREQ-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                #region GetItemType
                // Created By: Shabina 
                // Created Date: 17/08/2021
                // Method Name: GetItemType
                // Method Parameters: Str_JvStockEnt objJvStockEnt
                // Method Description: Get item type sacrap non scrap item dtail 
                public DataSet GetItemType(string itemType, int MISGNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_ITEMTYPE", itemType);
                        objParams[1] = new SqlParameter("@P_MISGNO", MISGNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_SELECT_ITEM_TYPE", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_JvStockCon.GetItemType-> " + ex.ToString());
                    }
                    return ds;
                }
                #endregion

                // Created by Shabina
                //DAte 29/09/2021
                //public DataSet GetReturnableItemsList(int RDL)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = new SqlParameter[1];

                //        objParams[0] = new SqlParameter("@P_SelectType", RDL);
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_RETURNABLE_ITEM_REPAIR_DETAILS", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_JvStockCon.GetReturnableItemsList-> " + ex.ToString());
                //    }
                //    return ds;
                //}
                public DataSet GetReturnableItemsList(int RDL, int IsSecGPEntry)  //21/04/2022
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_SelectType", RDL);
                        objParams[1] = new SqlParameter("@P_IsSecGPEntry", IsSecGPEntry);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_RETURNABLE_ITEM_REPAIR_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_JvStockCon.GetReturnableItemsList-> " + ex.ToString());
                    }
                    return ds;
                }


                //====================12/05/2022=============Shabina==
                public DataSet GetItemsForSale(string Type, int Itemno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_TYPE", Type);
                        objParams[1] = new SqlParameter("@P_ITEM_NO", Itemno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STORE_GET_ITEMS_FOR_SALE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_JvStockCon.GetItemsForSale-> " + ex.ToString());
                    }
                    return ds;
                }
                //==========================end==========================


                //------------18-07-2023-----Shaikh Juned----Get Invoice Qty and Rate Details againsed item --

                public DataSet GetInvoiceDetailsByItem(int item)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ITEM_NO", item);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_INVOICE_DETAILS_BY_ITEM_ID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_JvStockCon.GetTranDetailsByReqNo-> " + ex.ToString());
                    }
                    return ds;
                }
                //---------------


            }
        }
    }
}
