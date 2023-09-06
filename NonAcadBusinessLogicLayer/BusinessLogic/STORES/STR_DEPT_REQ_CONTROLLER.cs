using System;
using System.Data;
using System.Web;

//using IITMS.UAIMS.BusinessLayer;
//using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.DAC;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class STR_DEPT_REQ_CONTROLLER
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>

                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                public int AddDeptRequest(Str_DeptRequest objDeptRequest)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[21];
                        objParams[0] = new SqlParameter("@P_REQ_NO", objDeptRequest.REQ_NO);
                        objParams[1] = new SqlParameter("@P_REQ_DATE", objDeptRequest.REQ_DATE);
                        objParams[2] = new SqlParameter("@P_SDNO", objDeptRequest.SDNO);
                        objParams[3] = new SqlParameter("@P_NAME", objDeptRequest.NAME);
                        objParams[4] = new SqlParameter("@P_BHALNO", objDeptRequest.BHALNO);
                        objParams[5] = new SqlParameter("@P_TEQIP", objDeptRequest.TEQIP);
                        objParams[6] = new SqlParameter("@P_STATUS", objDeptRequest.STATUS);
                        objParams[7] = new SqlParameter("@P_REMARK", objDeptRequest.REMARK);
                        objParams[8] = new SqlParameter("@P_COLLEGE_CODE", objDeptRequest.COLLEGE_CODE);
                        objParams[9] = new SqlParameter("@P_ITEMNO", objDeptRequest.ITEMNO);
                        objParams[10] = new SqlParameter("@P_REQ_QTY", objDeptRequest.REQ_QTY);
                        objParams[11] = new SqlParameter("@P_RATE", objDeptRequest.RATE);

                        if (!(objDeptRequest.TECHSPEC == null || objDeptRequest.TECHSPEC == ""))
                            objParams[12] = new SqlParameter("@P_TECHSPEC", objDeptRequest.TECHSPEC);
                        else
                            objParams[12] = new SqlParameter("@P_TECHSPEC", DBNull.Value);

                        objParams[13] = new SqlParameter("@P_REMARKTRX", objDeptRequest.REMARKTRN);
                        objParams[14] = new SqlParameter("@P_STAPPROVAL", objDeptRequest.STAPPROVAL);                        
                        objParams[15] = new SqlParameter("@P_UANO", objDeptRequest.UANO);
                        objParams[16] = new SqlParameter("@P_REQ_FOR", objDeptRequest.REQ_FOR);
                        objParams[17] = new SqlParameter("@P_ITEM_SPECIFICATION", objDeptRequest.ITEM_SPECIFICATION);
                        objParams[18] = new SqlParameter("@P_STORE_USER_TYPE", objDeptRequest.STORE_USER_TYPE);
                        objParams[19] = new SqlParameter("@P_FILENAME", objDeptRequest.FILENAME);
                        objParams[20] = new SqlParameter("@P_FILEPTH", objDeptRequest.FILEPTH);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_REQ_MAIN_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.AddDeptRequest-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetMainStoreAndDeptStoreRequisions(int MdNo, string StoreUserType)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_MDNO", MdNo);
                        objParams[1] = new SqlParameter("@P_STORE_USER_TYPE", StoreUserType);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_DEPT_REQISITIONS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetDeptRequisions-> " + ex.ToString());
                    }
                    return ds;
                }

                public int DeleteItem(int reqTno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_REQ_TNO", reqTno);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_REQ_TRAN_DEL", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.STR_DEPT_REQ_CONTROLLER.DeleteItem-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetDeptRequestByReqNo(int reqNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_REQTRNO", reqNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_REQ_MAIN_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetDeptRequestByIndentNo-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetTranDetailsByReqNo(int reqNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_REQTRNO", reqNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_REQS_TRAN_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetTranDetailsByReqNo-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetReqAcceptedItemsByReqNo(int reqNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_REQTRNO", reqNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_REQ_ITEM_ON_INDENT_FORM", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetTranDetailsByReqNo-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetItemDetails(int reqTno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_REQTRNO", reqTno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_REQ_TRAN_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetItemDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSingleTransDetailByReq_TRNO(int req_trno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@REQ_TNO", req_trno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_REQ_TRAN_GET_BY_REQTRNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetTranDetailsByReqNo-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetDeptRequisions()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_DEPT_REQISITIONS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetDeptRequisions-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetDeptRequisionsAccepted()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_DEPT_REQISITIONS_ACCEPTED", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetDeptRequisionsAccepted-> " + ex.ToString());
                    }
                    return ds;
                }

                public int SubmitRequisition(string reqNo, char status, double BUDGET_BALANCE_AMOUNT, double INPROCESS_BUDGET_AMOUNT, string REQUISITION_REMARK)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_REQ_NO", reqNo);
                        objParams[1] = new SqlParameter("@P_STATUS", status);
                        objParams[2] = new SqlParameter("@P_BUDGET_BALANCE_AMOUNT", BUDGET_BALANCE_AMOUNT);
                        objParams[3] = new SqlParameter("@P_INPROCESS_BUDGET_AMOUNT", INPROCESS_BUDGET_AMOUNT);
                        objParams[4] = new SqlParameter("@P_REQUISITION_REMARK", REQUISITION_REMARK);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_REQ_MAIN_SUBMIT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.STR_DEPT_REQ_CONTROLLER.UpdateRequisitionStatus-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int UpdateRequisitionStatus(int reqTno, string status, string tqStatus, double BUDGET_BALANCE_AMOUNT, double INPROCESS_BUDGET_AMOUNT, string REQUISITION_REMARK)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_REQTRNO", reqTno);
                        objParams[1] = new SqlParameter("@P_STATUS", status);
                        objParams[2] = new SqlParameter("@P_TQSTATUS", tqStatus);
                        objParams[3] = new SqlParameter("@P_BUDGET_BALANCE_AMOUNT", BUDGET_BALANCE_AMOUNT);
                        objParams[4] = new SqlParameter("@P_INPROCESS_BUDGET_AMOUNT", INPROCESS_BUDGET_AMOUNT);
                        objParams[5] = new SqlParameter("@P_REQUISITION_REMARK", REQUISITION_REMARK);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_UPD_REQ_STATUS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.STR_DEPT_REQ_CONTROLLER.UpdateRequisitionStatus-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int AddIndent(STR_INDENT objstrInd, string colcode, string Tqstatus, int mdno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_INDTRNO", SqlDbType.Int);
                        objParams[0].Direction = ParameterDirection.Output;
                        objParams[1] = new SqlParameter("@P_INDNO", objstrInd.INDNO);
                        objParams[2] = new SqlParameter("@P_INDSLIP_DATE", objstrInd.INDSLIP_DATE);
                        objParams[3] = new SqlParameter("@P_INDSLIP_NO", objstrInd.INDSLIP_NO);
                        objParams[4] = new SqlParameter("@P_NAME", objstrInd.NAME);
                        objParams[5] = new SqlParameter("@P_REMARK", objstrInd.REMARK);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", colcode);
                        objParams[7] = new SqlParameter("@P_TQSTATUS", Tqstatus);
                        objParams[8] = new SqlParameter("@P_MDNO", mdno);
                        objParams[9] = new SqlParameter("@P_REQTRNO", objstrInd.REQTRNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_INDENT_MAIN_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.AddIndent-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //public int UpdateIndentNoForReqTran(int reqtrno, string IndNo)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;
                //        objParams = new SqlParameter[2];
                //        objParams[0] = new SqlParameter("@P_REQ_TNO", reqtrno);
                //        objParams[1] = new SqlParameter("@P_INDENTNO", IndNo);

                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_REQ_TRAN_UPDATE_INDENT", objParams, false) != null)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.STR_DEPT_REQ_CONTROLLER.UpdateRequisitionStatus-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}


                public int UpdateIndentNoForReqTran(int reqtrno, string IndNo, Decimal indQty, int itemNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_REQ_TNO", reqtrno),
                            new SqlParameter("@P_INDENTNO", IndNo),
                            new SqlParameter("@P_INDQTY",indQty),
                            new SqlParameter("@P_ITEMNO", itemNo)
                        };

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_REQ_TRAN_UPDATE_INDENT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.STR_DEPT_REQ_CONTROLLER.UpdateRequisitionStatus-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetSingleIndent(int IndTrNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_INDTRNO", IndTrNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_INDENT_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetSingleIndent-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetItemsByIndentNo(int INDTRNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@INDTRNO", INDTRNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_INDENT_ITEMS_GET_BY_INDNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetItemsForQuotationTender-> " + ex.ToString());
                    }
                    return ds;
                }

                //public DataSet GenrateReq(int MDNO)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null; ;
                //        objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("@P_MDNO", MDNO);
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GENERATE_REQNO", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GenrateReq-> " + ex.ToString());
                //    }
                //    return ds;
                //}

                public DataSet GenrateReq(int MDNO, int OrgId,string reqtype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_MDNO", MDNO);
                        objParams[1] = new SqlParameter("@P_OrgId", OrgId);
                        objParams[2] = new SqlParameter("@P_reqtype", reqtype);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GENERATE_REQNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GenrateReq-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GenrateIssueNo(int MDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_MDNO", MDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GENERATE_ISSUENO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GenrateReq-> " + ex.ToString());
                    }
                    return ds;
                }
                //public DataSet GenrateIndNo(int MDNO)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null; ;
                //        objParams = new SqlParameter[1];
                //        objParams[0] = new SqlParameter("@P_MDNO", MDNO);
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GENERATE_INDNO", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GenrateReq-> " + ex.ToString());
                //    }
                //    return ds;
                //}
                public DataSet GenrateIndNo(int MDNO, int OrgId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_MDNO", MDNO);
                        objParams[1] = new SqlParameter("@P_OrgId", OrgId);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GENERATE_INDNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GenrateReq-> " + ex.ToString());
                    }
                    return ds;
                }
                public int UpdateIndent(STR_INDENT objstrInd, string colcode, string Tqstatus, int mdno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_INDTRNO", objstrInd.INDTRNO);
                        objParams[1] = new SqlParameter("@P_INDSLIP_DATE", objstrInd.INDSLIP_DATE);
                        objParams[2] = new SqlParameter("@P_NAME", objstrInd.NAME);
                        objParams[3] = new SqlParameter("@P_REMARK", objstrInd.REMARK);
                        objParams[4] = new SqlParameter("@P_COLLEGE_CODE", colcode);
                        objParams[5] = new SqlParameter("@P_TQSTATUS", Tqstatus);
                        objParams[6] = new SqlParameter("@P_MDNO", mdno);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_INDENT_MAIN_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.AddIndent-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetBudget(int Mdno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_MDNO", Mdno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_BUDGET_BY_DEPT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetBudget()-> " + ex.ToString());
                    }
                    return ds;
                }

                public void SaveRequisition(Str_DeptRequest _ReqMain, List<Str_DeptRequest> ListItemTran, string colcode)
                {
                    DAC.Commands.Clear();
                    DAC ReqMain = new DAC();
                    ReqMain.StoredProcedure = "PKG_STR_REQS_MAIN_INSERT";
                    ReqMain.Params.Add("@P_REQ_NO", SqlDbType.VarChar, _ReqMain.REQ_NO);
                    ReqMain.Params.Add("@P_REQ_DATE", SqlDbType.DateTime, _ReqMain.REQ_SLIP_DATE);
                    ReqMain.Params.Add("@P_SDNO", SqlDbType.Int, _ReqMain.SDNO);
                    ReqMain.Params.Add("@P_NAME", SqlDbType.VarChar, _ReqMain.NAME);
                    ReqMain.Params.Add("@P_BHALNO", SqlDbType.Int, _ReqMain.BHALNO);
                    ReqMain.Params.Add("@P_REMARK", SqlDbType.VarChar, _ReqMain.REMARK);
                    ReqMain.Params.Add("@P_COLLEGE_CODE", SqlDbType.VarChar, colcode);
                    ReqMain.Params.Add("@P_TEQIP", SqlDbType.Bit, _ReqMain.TEQIP);
                    ReqMain.Params.Add("@P_STATUS", SqlDbType.VarChar, _ReqMain.STATUS);
                    DAC.Commands.Add(ReqMain);
                    foreach (Str_DeptRequest _ItemTran in ListItemTran)
                    {
                        Common objCommon = new Common();
                        int REQTRNO = Convert.ToInt32(objCommon.LookUp("STORE_REQ_MAIN", "ISNULL(MAX(REQTRNO),0)", "")) + 1;
                        DAC ItemTran = new DAC();
                        ItemTran.StoredProcedure = "PKG_STR_REQ_TRAN_INSERT";
                        ItemTran.Params.Add("@P_REQTRNO", SqlDbType.Int, REQTRNO);
                        ItemTran.Params.Add("@P_ITEMNO", SqlDbType.Int, _ItemTran.ITEMNO);
                        ItemTran.Params.Add("@P_REQ_QTY", SqlDbType.Int, _ItemTran.REQ_QTY);
                        ItemTran.Params.Add("@P_RATE", SqlDbType.Money, _ItemTran.RATE);
                        ItemTran.Params.Add("@P_SDNO", SqlDbType.Int, _ReqMain.SDNO);
                        ItemTran.Params.Add("@P_TECHSPEC", SqlDbType.VarChar, _ItemTran.TECHSPEC);
                        ItemTran.Params.Add("@P_MDNO", SqlDbType.Int, _ItemTran.MDNO);
                        ItemTran.Params.Add("@P_REMARK", SqlDbType.VarChar, _ItemTran.REMARKTRN);
                        ItemTran.Params.Add("@P_COLLEGE_CODE", SqlDbType.VarChar, colcode);
                        DAC.Commands.Add(ItemTran);
                    }

                    DAC.ExecuteBatch();
                }
                public void UPDATERequisition(Str_DeptRequest _ReqMain, List<Str_DeptRequest> ListItemTran, string colcode)
                {
                    DAC.Commands.Clear();
                    DAC ReqMain = new DAC();
                    ReqMain.StoredProcedure = "PKG_STR_REQS_MAIN_INSERT";
                    ReqMain.Params.Add("@P_REQTRNO", SqlDbType.VarChar, _ReqMain.REQTRNO);
                    ReqMain.Params.Add("@P_REQ_NO", SqlDbType.VarChar, _ReqMain.REQ_NO);
                    ReqMain.Params.Add("@P_REQ_DATE", SqlDbType.DateTime, _ReqMain.REQ_SLIP_DATE);
                    ReqMain.Params.Add("@P_SDNO", SqlDbType.Int, _ReqMain.SDNO);
                    ReqMain.Params.Add("@P_NAME", SqlDbType.VarChar, _ReqMain.NAME);
                    ReqMain.Params.Add("@P_BHALNO", SqlDbType.Int, _ReqMain.BHALNO);
                    ReqMain.Params.Add("@P_REMARK", SqlDbType.VarChar, _ReqMain.REMARK);
                    ReqMain.Params.Add("@P_COLLEGE_CODE", SqlDbType.VarChar, colcode);
                    ReqMain.Params.Add("@P_TEQIP", SqlDbType.Bit, _ReqMain.TEQIP);
                    ReqMain.Params.Add("@P_STATUS", SqlDbType.VarChar, _ReqMain.STATUS);
                    ReqMain.Params.Add("@P_MDNO", SqlDbType.VarChar, _ReqMain.MDNO);
                    DAC.Commands.Add(ReqMain);
                    foreach (Str_DeptRequest _ItemTran in ListItemTran)
                    {
                        Common objCommon = new Common();
                        int REQTRNO = Convert.ToInt32(objCommon.LookUp("STORE_REQ_MAIN", "ISNULL(MAX(REQTRNO),0)", "")) + 1;
                        DAC ItemTran = new DAC();
                        ItemTran.StoredProcedure = "PKG_STR_REQ_TRAN_INSERT";
                        ItemTran.Params.Add("@P_REQTRNO", SqlDbType.Int, REQTRNO);
                        ItemTran.Params.Add("@P_ITEMNO", SqlDbType.Int, _ItemTran.ITEMNO);
                        ItemTran.Params.Add("@P_REQ_QTY", SqlDbType.Int, _ItemTran.REQ_QTY);
                        ItemTran.Params.Add("@P_RATE", SqlDbType.Money, _ItemTran.RATE);
                        ItemTran.Params.Add("@P_SDNO", SqlDbType.Int, _ReqMain.SDNO);
                        ItemTran.Params.Add("@P_TECHSPEC", SqlDbType.VarChar, _ItemTran.TECHSPEC);
                        ItemTran.Params.Add("@P_MDNO", SqlDbType.Int, _ItemTran.MDNO);
                        ItemTran.Params.Add("@P_REMARK", SqlDbType.VarChar, _ItemTran.REMARKTRN);
                        ItemTran.Params.Add("@P_COLLEGE_CODE", SqlDbType.VarChar, colcode);
                        DAC.Commands.Add(ItemTran);
                    }

                    DAC.ExecuteBatch();
                }
                public DataSet GetItemListByReqNo(int Reqno, string category)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_REQTRNO", Reqno);
                        objParams[1] = new SqlParameter("@P_GROUP", category);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_ITEM_GROUPBY_REQNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetBudget()-> " + ex.ToString());
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
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetBudget()-> " + ex.ToString());
                    }
                    return ds;
                }



                //code for getting requisition by subdepartment

                public DataSet GetDeptRequisions(int sdno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SDNO", sdno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_DEPT_REQISITIONS_BY_SUBDEPT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetDeptRequisions-> " + ex.ToString());
                    }
                    return ds;
                }


                //code for getting requisition by Datewise

                public DataSet GetDeptRequisionsByDatewise(DateTime StartDate, DateTime LastDate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_STARTDATE", StartDate);

                        objParams[1] = new SqlParameter("@P_LASTDATE", LastDate);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_DEPT_REQISITIONS_BY_REQDATE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetDeptRequisions-> " + ex.ToString());
                    }
                    return ds;
                }

                // get requisition detail for approval
                public DataSet GetReqItem(int reqtrno)
                //fetch  Leave Application details of Particular Leave aaplication by Passing LETRNO & its approval status
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_REQTRNO", reqtrno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_REQISITIONS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLeaveApplDetail->" + ex.ToString());

                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetItemDetailsByReqId(int REQTRNO)
                //fetch  Leave Application details of Particular Leave aaplication by Passing LETRNO & its approval status
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_REQTRNO", REQTRNO);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_ITEM_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetItemDetailsByReqId->" + ex.ToString());

                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int insertStatus(int trno, int deptNo, char sanctioning_authority, char REQ_FOR)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_TRNO", trno);
                        objParams[1] = new SqlParameter("@P_DEPTNO", deptNo);
                        objParams[2] = new SqlParameter("@P_SANCTIONING_AUTHORITY", sanctioning_authority);
                        objParams[3] = new SqlParameter("@P_REQ_FOR", REQ_FOR);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STORE_APP_PASS_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.STR_DEPT_REQ_CONTROLLER.UpdateRequisitionStatus-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int updateReqStatus(string reqNos)
                {
                    int retStatus;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        retStatus = objSQLHelper.ExecuteNonQuery("UPDATE STORE_REQ_MAIN SET CLUBREQSTATUS='CR' WHERE REQTRNO IN (" + reqNos + ")");
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.STR_DEPT_REQ_CONTROLLER.UpdateRequisitionStatus-> " + ex.ToString());
                    }

                    return retStatus;
                }

                // get fixed Assets Items
                // Added by:Suyog Thakre
                //Dated On:24/05/2017
                public DataSet GetFixedAssetItems(int item_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@ITEM_NO", item_no);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_FIXED_ASSETS_ITEM", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLeaveApplDetail->" + ex.ToString());

                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }
                // get Damaged Items
                // Added by:Suyog Thakre
                //Dated On:24/05/2017
                public DataSet GetItemWriteOff()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_ITEM_WRITE_OFF", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLeaveApplDetail->" + ex.ToString());

                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }


                public DataSet ItemWriteOff(string ItemNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@ISSUE_TNO", ItemNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_INSERT_DELETE_ITEM_WRITE_OFF", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLeaveApplDetail->" + ex.ToString());

                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }


                public DataSet SetWorkingStatus(string Working)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@ISSUE_TNO", Working);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_CHANGE_ITEM_STATUS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLeaveApplDetail->" + ex.ToString());

                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }


                public DataSet GetDamagedItemDetails()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_DAMAGED_ITEMS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLeaveApplDetail->" + ex.ToString());

                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }


                public DataSet GetPassNo(int dept)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];

                        objParams[0] = new SqlParameter("@MDNO", dept);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GATE_PASS_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLeaveApplDetail->" + ex.ToString());

                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int InsertGatePassData(STR_INDENT objstrInd, int COLLEGECODE, string issueNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[13];

                        objParams[0] = new SqlParameter("@GPNO", objstrInd.GPNO);
                        objParams[1] = new SqlParameter("@GPREFNO", objstrInd.GPREFNO);
                        objParams[2] = new SqlParameter("@MISDNO", objstrInd.MISDNO);
                        objParams[3] = new SqlParameter("@VehicleNo", objstrInd.VehicleNo);
                        objParams[4] = new SqlParameter("@STATUS", objstrInd.STATUS);
                        objParams[5] = new SqlParameter("@ITEMNO", objstrInd.ITEMNO);
                        objParams[6] = new SqlParameter("@ISREPAIR", objstrInd.ISREPAIR);
                        objParams[7] = new SqlParameter("@RECEIVERNAME", objstrInd.RECEIVERNAME);
                        objParams[8] = new SqlParameter("@RECEIVERADDRESS", objstrInd.RECEIVERADDRESS);
                        objParams[9] = new SqlParameter("@COLLEGECODE", COLLEGECODE);
                        objParams[10] = new SqlParameter("@ISSUE_TNO", issueNo);
                        objParams[11] = new SqlParameter("@GATEPASSDATE", objstrInd.GATEPASSDATE);
                        objParams[12] = new SqlParameter("@ROUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_INSERT_GATE_PASS_DATA", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.AddIndent-> " + ex.ToString());
                    }
                    return retStatus;
                }





                public int InsertGatePassWorkingData(STR_INDENT objstrInd, int COLLEGECODE)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[13];

                        objParams[0] = new SqlParameter("@GPNO", objstrInd.GPNO);
                        objParams[1] = new SqlParameter("@GPREFNO", objstrInd.GPREFNO);
                        objParams[2] = new SqlParameter("@WORKING_QTY", objstrInd.TransferQty);
                        objParams[3] = new SqlParameter("@MISDNO", objstrInd.MISDNO);
                        objParams[4] = new SqlParameter("@VehicleNo", objstrInd.VehicleNo);
                        objParams[5] = new SqlParameter("@STATUS", objstrInd.STATUS);
                        objParams[6] = new SqlParameter("@ITEMNO", objstrInd.ITEMNO);
                        objParams[7] = new SqlParameter("@ISREPAIR", objstrInd.ISREPAIR);
                        objParams[8] = new SqlParameter("@RECEIVERNAME", objstrInd.RECEIVERNAME);
                        objParams[9] = new SqlParameter("@GATEPASSDATE", objstrInd.GATEPASSDATE);
                        objParams[10] = new SqlParameter("@RECEIVERADDRESS", objstrInd.RECEIVERADDRESS);
                        objParams[11] = new SqlParameter("@COLLEGECODE", COLLEGECODE);
                        objParams[12] = new SqlParameter("@ROUT", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("[dbo].[PKG_STR_INSERT_GATE_PASS_WORKING_DATA]", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.AddIndent-> " + ex.ToString());
                    }
                    return retStatus;
                }



                public DataSet ToolkitRefNo(int dept)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];

                        objParams[0] = new SqlParameter("@MDNO", dept);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_Toolkit_RefNo", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLeaveApplDetail->" + ex.ToString());

                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }



                public DataSet GetStudentList(int yearno, int degreeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@YEARNO", yearno);
                        objParams[1] = new SqlParameter("@DEGREENO", degreeno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_DCR_STUDENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLeaveApplDetail->" + ex.ToString());

                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public DataSet GetGatePassDateWise(DateTime from, DateTime to)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@FROMDATE", from);
                        objParams[1] = new SqlParameter("@TODATE", to);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_GATEPASS_DATEWISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.LeavesController.GetLeaveApplDetail->" + ex.ToString());

                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }

                public int InsertToolKitAlllocation(int yearno, int degreeno, int toolkit, int collegeid, string str)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[6];

                        objParams[0] = new SqlParameter("@YEAR_NO", yearno);
                        objParams[1] = new SqlParameter("@DEGREE_NO", degreeno);
                        objParams[2] = new SqlParameter("@STUD_NO", str);
                        objParams[3] = new SqlParameter("@TK_NO", toolkit);
                        objParams[4] = new SqlParameter("@COLLEGE_CODE", collegeid);
                        objParams[5] = new SqlParameter("@ROUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_INSERT_TOOLKIT_ALLOCATION", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.AddIndent-> " + ex.ToString());
                    }
                    return retStatus;
                }



                // This method is used to get login credentails for sending mails to approval authority.
                public DataSet GetFromDataForSendingEmail(int TRNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TRNO", TRNO);                        
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_CREDENTIALS_FOR_EMAIL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetFromDataForEmail-> " + ex.ToString());
                    }
                    return ds;
                }

                // This method is used to get login credentails for sending mail to requisition user.
                public DataSet GetDataForEmailToRequisitionUser(int TRNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TRNO", TRNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_CREDENTIALS_FOR_EMAIL_REQUISITION_USER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetFromDataForEmail-> " + ex.ToString());
                    }
                    return ds;
                }

                // This method is used to get login credentails for sending mail.
                public DataSet GetNextAuthorityForSendingEmail(int TRNO, int UA_NO, char FLAG)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_TRNO", TRNO);
                        objParams[1] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[2] = new SqlParameter("@P_FLAG", FLAG);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_NEXT_AUTHORITY_CREDENTIALS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetNextAuthorityForSendingEmail-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetRequisitionTrackDetails(int REQTRNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_REQTRNO", REQTRNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_REQUISITION_TRACK_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetRequisitionTrackDetails-> " + ex.ToString());
                    }
                    return ds;
                }


                // This method is used to get login credentails for sending mail.
                public DataSet GetBudgetBalance(int SDNO, int BUDGETNO, DateTime REQ_DATE,int MDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SDNO", SDNO);
                        objParams[1] = new SqlParameter("@P_BUDGETNO", BUDGETNO);
                        objParams[2] = new SqlParameter("@P_REQ_DATE", REQ_DATE);
                        objParams[3] = new SqlParameter("@P_MDNO", MDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_BUDGET_BALANCE_AMOUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetBudgetBalance-> " + ex.ToString());
                    }
                    return ds;

                }


                //3/02/2022
                // This method is used to get login credentails for sending mail.
                public DataSet GetCentralStoreUserDataForSendingEmail(int TRNO, int UA_NO, char FLAG)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_TRNO", TRNO);
                        objParams[1] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[2] = new SqlParameter("@P_FLAG", FLAG);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_CENTRAL_store_CREDENTIALS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetNextAuthorityForSendingEmail-> " + ex.ToString());
                    }
                    return ds;
                }

                //---------------------------new method added for new deleopment to get secrh requisition report data
                public DataSet DisplaySearchReq(DateTime DiFrmDt, DateTime DiToDt, int dept)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];

                        if (!DiFrmDt.Equals(DateTime.MinValue))
                            objParams[0] = new SqlParameter("@P_FROMDT", DiFrmDt);
                        else
                            objParams[0] = new SqlParameter("@P_FROMDT", DBNull.Value);

                        if (!DiToDt.Equals(DateTime.MinValue))
                            objParams[1] = new SqlParameter("@P_TODT", DiToDt);
                        else
                            objParams[1] = new SqlParameter("@P_TODT", DBNull.Value);

                        objParams[2] = new SqlParameter("@P_DEPT", dept);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_SEARCH_REQUISITION_DEATILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TPController.Disp_Letter->" + ex.ToString());
                    }
                    return ds;
                }


            }
        }
    }
}