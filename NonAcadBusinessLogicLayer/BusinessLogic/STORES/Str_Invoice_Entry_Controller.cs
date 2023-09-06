using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IITMS;
//using IITMS.NITPRM.BusinessLayer;
//using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System.DAC;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class Str_Invoice_Entry_Controller
            {
                StoreMasterController objMaster = new StoreMasterController();
                Str_Purchase_Order_Controller objPO = new Str_Purchase_Order_Controller();
                Common objComman = new Common();
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


                public DataSet GenrateInvoiceNo(int MDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_MDNO", MDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GENERATE_INVOICENO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Invoice_Entry_Controller_GenerateInvoiceNo-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetInvoiceNumber()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_INV_GENERATE_INVOICE_NUMBER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StrGRNCon.GetInvoiceNumber-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetItemsByGRNID(string GRNID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_GRNID", GRNID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_INVOICE_GET_ITEM_BY_GRNID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_Invoice_Entry_Controller.GetItemsByGRNID-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsUpdateInvoiceEntry(Str_Invoice objINVEnt)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[18];
                        objParams[0] = new SqlParameter("@P_INVTRNO", objINVEnt.INVTRNO);
                        objParams[1] = new SqlParameter("@P_GRNID", objINVEnt.GRN_NUM);
                        objParams[2] = new SqlParameter("@P_INVDATE", objINVEnt.INVDATE);
                        //objParams[3] = new SqlParameter("@P_GRNDATE  ", objINVEnt.GRNDATE);           //31/03/2022 junaid

                        //------Shaikh Juned (31/03/2022)-----Start----
                        if (objINVEnt.GRNDATE == DateTime.MinValue)  
                        {
                            objParams[3] = new SqlParameter("@P_GRNDATE  ", DBNull.Value);
                        }
                        else
                        {
                            objParams[3] = new SqlParameter("@P_GRNDATE  ", objINVEnt.GRNDATE);
                        }
                        //------Shaikh Juned (31/03/2022)-----end----

                        objParams[4] = new SqlParameter("@P_INVNO", objINVEnt.INVNO);
                        objParams[5] = new SqlParameter("@P_PNO ", objINVEnt.PNO);
                        objParams[6] = new SqlParameter("@P_MDNO ", objINVEnt.MDNO);
                        objParams[7] = new SqlParameter("@P_PORDNO", objINVEnt.PORDNO);
                        objParams[8] = new SqlParameter("@P_REMARK", objINVEnt.REMARK);
                        objParams[9] = new SqlParameter("@P_DMDATE", objINVEnt.DMDATE);
                        objParams[10] = new SqlParameter("@P_DMNO", objINVEnt.DMNO);
                        objParams[11] = new SqlParameter("@P_INVOICE_ITEM_TBL", objINVEnt.INVOICE_ITEM_TBL);
                        objParams[12] = new SqlParameter("@P_INVOICE_TAX_TBL", objINVEnt.INVOICE_TAX_TBL);
                        objParams[13] = new SqlParameter("@P_CREATED_BY", objINVEnt.CREATED_BY);
                        objParams[14] = new SqlParameter("@P_MODIFIED_BY", objINVEnt.MODIFIED_BY);
                        objParams[15] = new SqlParameter("@P_NETAMOUNT", objINVEnt.NETAMOUNT); // Shaikh Juned 11-11-2022
                        objParams[16] = new SqlParameter("@P_INVOICE_UPLOAD_FILE_TBL", objINVEnt.INVOICE_UPLOAD_FILE_TBL); // 29-08-2023 enhancement according to Maher ticket 4748
                        objParams[17] = new SqlParameter("@P_OUT ", SqlDbType.Int);
                        objParams[17].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_INVOICE_INS_UPD", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Invoice_Entry_Controller.InsUpdateInvoiceEntry-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetItemsByPO(string Ponumbers)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PORDNO", Ponumbers);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_INVOICE_GET_ITEM_BY_PO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_Invoice_Entry_Controller.GetItemsByPO-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetTaxes(decimal TaxableAmt, decimal BasicAmt, int ItemNo, int IsState)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_TAXABLE_AMT", TaxableAmt);
                        objParams[1] = new SqlParameter("@P_BASIC_AMT", BasicAmt);
                        objParams[2] = new SqlParameter("@P_ITEM_NO", ItemNo);
                        objParams[3] = new SqlParameter("@P_IsStateTax", IsState);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_INVOICE_GET_TAXES", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_Invoice_Entry_Controller.GetItemsByPO-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetInvoiceEntryDetailsForEdit(int INVTRNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_INVTRNO", INVTRNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_INVOICE_GET_DETAILS_FOR_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_Invoice_Entry_Controller.GetInvoiceEntryDetailsForEdit-> " + ex.ToString());
                    }
                    return ds;
                }


                // It is used to get all DSR Items to issue.
                public DataSet GetAllDSRItemsToIssue(int INVTRNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objsqlhelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_INVTRNO", INVTRNO);
                        ds = objsqlhelper.ExecuteDataSetSP("PKG_STR_GET_DSR_ITEM_TO_ISSUE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_DSR_Entry_Controller.GetAllDSRItemsToIssue-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetAllItemsByInvoiceAcc(int INVTRNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_INVTRNO", INVTRNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_INVOICE_ITEM_GET_BY_NO_ACC", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Invoice_Entry_Controller_ GetAllItemsByInvoice-> " + ex.ToString());
                    }
                    return ds;
                }


                //added for invoice itenacceptance
                public int UpdateAccItems(int invtrno, DateTime accdate, int accqyt, string accstatus, int itemno, string remark, DataTable DSRItemIssue)
                {
                    int retStatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_INVTRNO", invtrno);
                        objParams[1] = new SqlParameter("@P_ACCDATE", accdate);
                        objParams[2] = new SqlParameter("@P_ACCQTY", accqyt);
                        objParams[3] = new SqlParameter("@P_ACCSTATUS", accstatus);
                        objParams[4] = new SqlParameter("@P_ITEMNO", itemno);
                        objParams[5] = new SqlParameter("@P_REMARK", remark);
                        objParams[6] = new SqlParameter("@P_DSRItemIssueTbl", DSRItemIssue);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_INVOICE_ITEMS_ACC_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Invoice_Entry_Controller_ GetInvoiceByNo-> " + ex.ToString());
                    }

                    return retStatus;
                }


                public DataSet GetAllItemsByInvoice(int INVTRNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_INVTRNO", INVTRNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_INVOICE_ITEM_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Invoice_Entry_Controller_ GetAllItemsByInvoice-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetAllTaxeByInvoice(int INVTRNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_INVTRNO", INVTRNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_INVOICE_TAX_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Invoice_Entry_Controller_ GetInvoiceByNo-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetInvoiceByNo(int INVTRNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_INVTRNO", INVTRNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_INVOICE_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Invoice_Entry_Controller_ GetInvoiceByNo-> " + ex.ToString());
                    }
                    return ds;
                }

                //Created by Shabina  
                //Created Date 14-10-02021
                // Get PO List those PO qty Are not submitted completely.

                public DataSet GetPODropdown()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STORE_GET_PO_LIST_FOR_INVOICE_ENTRY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_Invoice_Entry_Controller_.GetPODropdown-> " + ex.ToString());
                    }
                    return ds;
                }




            }
        }
    }
}

