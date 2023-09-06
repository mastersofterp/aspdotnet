using System;
using System.Collections.Generic;
using System.Text;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class Str_VendorPaymentCon
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetVPNumber()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_VP_GENERATE_SERIAL_NUMBER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_VendorPaymentCon.GetVPNumber-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetVPDetailsByPO(string Ponumbers)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PORDNO", Ponumbers);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_VP_GET_DETAILS_BY_PO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_VendorPaymentCon.GetVPDetailsByPO-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetVPDetailsByGrnId(string GRNNumbers)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_GRNID", GRNNumbers);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_VP_GET_DETAILS_BY_GRNID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_VendorPaymentCon.GetVPDetailsByGrnId-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetVPDetailsByInvtrno(string InvoiceNumbers)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_INVTRNO", InvoiceNumbers);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_VP_GET_DETAILS_BY_INVTRNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_VendorPaymentCon.GetVPDetailsByInvtrno-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsUpdateVendorPayment(Str_VendorPaymentEnt objVPEnt)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[20];
                        objParams[0] = new SqlParameter("@P_VPID", objVPEnt.VPID);
                        objParams[1] = new SqlParameter("@P_VP_NUMBER", objVPEnt.VP_NUMBER);
                        objParams[2] = new SqlParameter("@P_VPDATE", objVPEnt.VPDATE);
                        objParams[3] = new SqlParameter("@P_PNO", objVPEnt.PNO);
                        objParams[4] = new SqlParameter("@P_PAYMENT_TYPE", objVPEnt.PAYMENT_TYPE);
                        objParams[5] = new SqlParameter("@P_PORDNO  ", objVPEnt.PORDNO);
                        objParams[6] = new SqlParameter("@P_GRNID", objVPEnt.GRNID);
                        objParams[7] = new SqlParameter("@P_INVTRNO ", objVPEnt.INVTRNO);
                        objParams[8] = new SqlParameter("@P_PAYMENT_AMOUNT ", objVPEnt.PAYMENT_AMOUNT);
                        objParams[9] = new SqlParameter("@P_MODE_OF_PAY", objVPEnt.MODE_OF_PAY);
                        objParams[10] = new SqlParameter("@P_BANK_ID", objVPEnt.BANK_ID);
                        objParams[11] = new SqlParameter("@P_BRANCH_ID", objVPEnt.BRANCH_ID);
                        objParams[12] = new SqlParameter("@P_IFSC_ID", objVPEnt.IFSC_ID);
                        objParams[13] = new SqlParameter("@P_BANKACCNO_ID", objVPEnt.BANKACCNO_ID);
                        objParams[14] = new SqlParameter("@P_PAYEE_NAME", objVPEnt.PAYEE_NAME);
                        objParams[15] = new SqlParameter("@P_REMARK", objVPEnt.REMARK);
                        objParams[16] = new SqlParameter("@P_VENDOR_PAYMENT_TABLE", objVPEnt.VENDOR_PAYMENT_TABLE);
                        objParams[17] = new SqlParameter("@P_CREATED_BY", objVPEnt.CREATED_BY);
                        objParams[18] = new SqlParameter("@P_MODIFIED_BY", objVPEnt.MODIFIED_BY);
                        objParams[19] = new SqlParameter("@P_OUT ", SqlDbType.Int);
                        objParams[19].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_VP_PAYMENT_INS_UPD", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_VendorPaymentCon.InsUpdateVendorPayment-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetVPDetailsById(int VPID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_VPID", VPID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_VP_GET_DETAILS_BY_ID_FOR_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_VendorPaymentCon.GetVPDetailsById-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetVPAllDetails()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_VP_GET_ALL_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_VendorPaymentCon.GetVPAllDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdateStatus(int Vpid,char Status)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_VPID", Vpid);
                        objParams[1] = new SqlParameter("@P_STATUS", Status);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_VP_UPDATE_STATUS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_VendorPaymentCon.UpdateStatus-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //public DataSet GetPODropdown()
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                //        SqlParameter[] objParams = new SqlParameter[0];
                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PO_DROPDOWN", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_VendorPaymentCon.GetPODropdown()-> " + ex.ToString());
                //    }
                //    return ds;
                //}

                //Created by Shabina  
                //Created Date 09-09-02021
                // get On balanc Amount PO

                public DataSet GetPODropdown()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STORE_GET_PO_LIST_FOR_VENDOR_PAYMENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_VendorPaymentCon.GetPODropdown-> " + ex.ToString());
                    }
                    return ds;
                }

                //Created by Shabina  
                //Created Date 15-09-02021
                // get On balanc Amount GRN

                public DataSet GetGRNDropdown()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_GRN_LIST_FOR_VENDOR_PAYMENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_VendorPaymentCon.GetPODropdown-> " + ex.ToString());
                    }
                    return ds;
                }

                //Created by Shabina  
                //Created Date 15-09-02021
                // get On balanc Amount Invoice

                public DataSet GetInvoiceDropdown()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STORE_GET_PO_LIST_FOR_INVOICE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_VendorPaymentCon.GetPODropdown-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetMultiSelectDropDownForEdit(int VpId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_VPID", VpId);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_DROPDOWN_LIST_FOR_VENDOR_PAYMENT_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_VendorPaymentCon.GetMultiSelectDropDownForEdit-> " + ex.ToString());
                    }
                    return ds;
                }

            }
        }

    }
}

