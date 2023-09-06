using System;
using System.Collections.Generic;
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
using IITMS.NITPRM;
using IITMS.UAIMS;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class Str_Vendor_Tender_Entry_Controller
            {

                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                public DataSet GetAllTendor()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDER_GET_ALL_LOCKED", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Invoice_Entry_Controller_GetAllVendor-> " + ex.ToString());
                    }
                    return ds;
                }
                public int SavePartyItemsEntry(STR_TENDER_ITEM_ENTRY objPItem, string colcode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[18];
                        objParams[0] = new SqlParameter("@P_TENDERNO", objPItem.TENDERNO);
                        objParams[1] = new SqlParameter("@P_ITEM_NO", objPItem.ITEM_NO);
                        objParams[2] = new SqlParameter("@P_RATE", objPItem.PRICE);
                        objParams[3] = new SqlParameter("@P_FLAG", objPItem.FLAG);
                        objParams[4] = new SqlParameter("@P_MDNO", objPItem.MDNO);
                        objParams[5] = new SqlParameter("@P_EDATE", objPItem.EDATE);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", colcode);
                        objParams[7] = new SqlParameter("@P_TVNO", objPItem.TVNO);
                        objParams[8] = new SqlParameter("@P_QTY", objPItem.QTY);
                        objParams[9] = new SqlParameter("@P_TOTALAMT", objPItem.TOTALAMT);
                        objParams[10] = new SqlParameter("@P_DISCOUNTPERCENT", objPItem.DISCOUNT);
                        objParams[11] = new SqlParameter("@P_DISCOUNTAMOUNT", objPItem.DISCOUNTAMOUNT);
                        objParams[12] = new SqlParameter("@P_TAXABLE_AMT", objPItem.TAXABLE_AMT);
                        objParams[13] = new SqlParameter("@P_TAX_AMT", objPItem.TAXAMT);
                        objParams[14] = new SqlParameter("@P_ITEM_REMARK", objPItem.ITEM_REMARK);
                        objParams[15] = new SqlParameter("@P_QUALITY_QTY_SPEC", objPItem.QUALITY_QTY_SPEC);
                        if (objPItem.TECHSPEC == null)
                        {
                            objPItem.TECHSPEC = "-";
                        }
                        objParams[16] = new SqlParameter("@P_TECHSPECH", objPItem.TECHSPEC);

                        objParams[17] = new SqlParameter("@P_TINO", SqlDbType.Int);
                        objParams[17].Direction = ParameterDirection.Output;

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_TENDERITEMENTRY_INSERT", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Vendor_Quotation_Entry_Controller.SavePartyItemsEntry-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int UpdatePartyItemsEntry(STR_TENDER_ITEM_ENTRY objPItem, string colcode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[]{
                      
                       new SqlParameter("@P_RATE", objPItem.PRICE),
                       //new SqlParameter("@P_UNIT", objPItem.UNIT),
                       new SqlParameter("@P_EDATE", objPItem.EDATE),                      
                       new SqlParameter("@P_COLLEGE_CODE", colcode),
                       new SqlParameter("@P_QTY", objPItem.QTY),
                       new SqlParameter("@P_TINO", objPItem.TINO),                      
                       new SqlParameter("@P_DISC_PERCENT", objPItem.DISCOUNT),                     
                       new SqlParameter("@P_DISCOUNTAMOUNT", objPItem.DISCOUNTAMOUNT),
                       new SqlParameter("@P_TOTAMOUNT", objPItem.TOTALAMT),
                       new SqlParameter("@P_TENDERNO", objPItem.TENDERNO),
                       new SqlParameter("@P_TVNO", objPItem.TVNO),
                       new SqlParameter("@P_TAXABLE_AMT", objPItem.TAXABLE_AMT),                       
                       new SqlParameter("@P_ITEM_REMARK", objPItem.ITEM_REMARK),
                       new SqlParameter("@P_QUALITY_QTY_SPEC", objPItem.QUALITY_QTY_SPEC),
                       new SqlParameter("@P_TECHSPECH", objPItem.TECHSPEC),
                       new SqlParameter("@P_TAX_AMT", objPItem.TAXAMT)
                       
                       };
                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_TENDERITEMENTRY_UPDATE", objParams, true));                        

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Vendor_Quotation_Entry_Controller.SavePartyItemsEntry-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int SavePartyItemsTech(STR_TENDER_ITEM_ENTRY objPItem)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_TECHNO", SqlDbType.Int);
                        objParams[0].Direction = ParameterDirection.Output;
                        objParams[1] = new SqlParameter("@P_TENDERNO ", objPItem.TENDERNO);
                        objParams[2] = new SqlParameter("@P_ITEM_NO", objPItem.ITEM_NO);
                        if (objPItem.TECHSPEC == null)
                        {
                            objPItem.TECHSPEC = "NO DOCUMENT ATTACHED";
                        }
                        objParams[3] = new SqlParameter("@P_TECHSPECI", objPItem.TECHSPEC);
                        objParams[4] = new SqlParameter("@P_OTHERSPEC", objPItem.OTHERSPEC);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objPItem.COLLEGE_CODE);
                        objParams[6] = new SqlParameter("@P_TVNO", objPItem.TVNO);

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_TENDERITEMTECH_INSERT", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Vendor_Quotation_Entry_Controller.SavePartyItemsTech-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int UpdatePartyItemsTech(STR_TENDER_ITEM_ENTRY objPItem)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_TECHNO", objPItem.TECHNO);
                        objParams[0].Direction = ParameterDirection.Output;
                        objParams[1] = new SqlParameter("@P_TENDERNO ", objPItem.TENDERNO);
                        objParams[2] = new SqlParameter("@P_ITEM_NO", objPItem.ITEM_NO);
                        if (objPItem.TECHSPEC == null)
                        {
                            objPItem.TECHSPEC = "NO DOCUMENT ATTACHED";
                        }
                        objParams[3] = new SqlParameter("@P_TECHSPECI", objPItem.TECHSPEC);
                        objParams[4] = new SqlParameter("@P_OTHERSPEC", objPItem.OTHERSPEC);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objPItem.COLLEGE_CODE);
                        objParams[6] = new SqlParameter("@P_TVNO", objPItem.TVNO);


                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_TENDERITEMTECH_UPDATE", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Vendor_Quotation_Entry_Controller.SavePartyItemsEntry-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int savevendor(STR_TENDER_VENDOR objPI)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_TVNO", SqlDbType.Int);
                        objParams[0].Direction = ParameterDirection.Output;
                        objParams[1] = new SqlParameter("@P_TENDERNO", objPI.TENDERNO);
                        objParams[2] = new SqlParameter("@P_VENDORNAME", objPI.NAME);
                        objParams[3] = new SqlParameter("@P_ADDRESS", objPI.ADDRESS);
                        objParams[4] = new SqlParameter("@P_REMARK", objPI.REMARK);
                        objParams[5] = new SqlParameter("@P_VENDOR_CONTACT", objPI.CONTACT);
                        objParams[6] = new SqlParameter("", objPI.OTHERTECH);
                        objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objPI.COLLEGE_CODE);
                        objParams[8] = new SqlParameter("@P_CST", Convert.ToDouble(objPI.CST));
                        objParams[9] = new SqlParameter("@P_BST", Convert.ToDouble(objPI.BST));
                        objParams[10] = new SqlParameter("@P_EMAIL", objPI.EMAIL);
                        objParams[11] = new SqlParameter("@P_VENDORCODE", objPI.VENDORCODE);
                        objParams[12] = new SqlParameter("@P_STATUS", objPI.STATUS);
                        char sr = objPI.STATUS;

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_TENDER_VENDOR_INSERT", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Vendor_Tender_Entry_Controller.savevendor-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int Updatevendor(STR_TENDER_VENDOR objPI)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_TVNO", objPI.TVNO);
                        objParams[1] = new SqlParameter("@P_TENDERNO", objPI.TENDERNO);
                        objParams[2] = new SqlParameter("@P_VENDORNAME", objPI.NAME);
                        objParams[3] = new SqlParameter("@P_ADDRESS", objPI.ADDRESS);
                        objParams[4] = new SqlParameter("@P_REMARK", objPI.REMARK);
                        objParams[5] = new SqlParameter("@P_VENDOR_CONTACT", objPI.CONTACT);
                        //objParams[6] = new SqlParameter("@P_FLAG", objPI.FLAG);
                        objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objPI.COLLEGE_CODE);
                        objParams[8] = new SqlParameter("@P_CST", objPI.CST);
                        objParams[9] = new SqlParameter("@P_BST", objPI.BST);
                        objParams[10] = new SqlParameter("@P_EMAIL", objPI.EMAIL);
                        objParams[11] = new SqlParameter("@P_VENDORCODE", objPI.VENDORCODE);
                        objParams[12] = new SqlParameter("@P_STATUS", objPI.STATUS);

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_TENDER_VENDOR_UPDATE", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Vendor_Quotation_Entry_Controller.SavePartyItemsEntry-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int SavePartyFieldEntry(STR_TENDER_FIELD_ENTRY objPFentry, string colcode)
                {

                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[5];                       
                        objParams[0] = new SqlParameter("@P_TENDERNO", objPFentry.TENDERNO);                       
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE ", colcode);
                        objParams[2] = new SqlParameter("@P_TVNO", objPFentry.TVNO);
                        objParams[3] = new SqlParameter("@P_VENDOR_TAX_TBL", objPFentry.VENDOR_TAX_TBL);
                        objParams[4] = new SqlParameter("@P_TFNO", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;                       

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_TENDER_FIELDS_INSERT", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Vendor_Quotation_Entry_Controller.SavePartyItemsEntry-> " + ex.ToString());
                    }
                    return retStatus;

                }




                public DataSet GetPartyEnrtyByNO(int TINO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TINO", TINO);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDERITEMS_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetVendorByQuotation-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetPartyTechEnrtyByNO(int TINO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TINO", TINO);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDERITEMS_TECH_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetVendorByQuotation-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetVendorEntryItemsTechByParty(string TENDERNO, int TVNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_TENDERNO", TENDERNO);
                        objParams[1] = new SqlParameter("@P_TVNO", TVNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTYITEMS_TECH_BY_TENDER_NO_AND_PARTY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetItemsByQuotNo-> " + ex.ToString());
                    }
                    return ds;
                }


                //technical bid cs
                public int AddParty(STR_TENDER_VENDOR objParty, int pno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New ITEM_MASTER
                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_TENDERNO", objParty.TENDERNO);
                        objParams[1] = new SqlParameter("@P_VENDORNAME", objParty.NAME);
                        objParams[2] = new SqlParameter("@P_ADDRESS", objParty.ADDRESS);
                        objParams[3] = new SqlParameter("@P_REMARK", objParty.REMARK);
                        objParams[4] = new SqlParameter("@P_VENDOR_CONTACT", objParty.CONTACT);
                        objParams[5] = new SqlParameter("@P_OTHERTECH", objParty.OTHERTECH);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objParty.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_CST", objParty.CST);
                        objParams[8] = new SqlParameter("@P_BST", objParty.BST);
                        objParams[9] = new SqlParameter("@P_EMAIL", objParty.EMAIL);
                        objParams[10] = new SqlParameter("@P_VENDORCODE", objParty.VENDORCODE);
                        objParams[11] = new SqlParameter("@P_STATUS", objParty.STATUS);
                        if (objParty.TECHSPEC == null)
                        {
                            objParty.TECHSPEC = "No file uploaded";
                        }
                        objParams[12] = new SqlParameter("@P_TECHSPEC", objParty.TECHSPEC);
                        objParams[13] = new SqlParameter("@P_PNO", pno);

                        objParams[14] = new SqlParameter("@P_TVNO ", SqlDbType.Int);
                        objParams[14].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_TENDER_PARTY_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.AddItemMaster-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int UpdateParty(STR_TENDER_VENDOR objParty, int pno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Update New Item_Master
                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_TENDERNO", objParty.TENDERNO);
                        objParams[1] = new SqlParameter("@P_VENDORNAME", objParty.NAME);
                        objParams[2] = new SqlParameter("@P_ADDRESS", objParty.ADDRESS);
                        objParams[3] = new SqlParameter("@P_REMARK", objParty.REMARK);
                        objParams[4] = new SqlParameter("@P_VENDOR_CONTACT", objParty.CONTACT);
                        objParams[5] = new SqlParameter("@P_OTHERTECH", objParty.OTHERTECH);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objParty.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_CST", objParty.CST);
                        objParams[8] = new SqlParameter("@P_BST", objParty.BST);
                        objParams[9] = new SqlParameter("@P_EMAIL", objParty.EMAIL);
                        objParams[10] = new SqlParameter("@P_VENDORCODE", objParty.VENDORCODE);
                        objParams[11] = new SqlParameter("@P_STATUS", objParty.STATUS);
                        if (objParty.TECHSPEC == null)
                        {
                            objParty.TECHSPEC = "No file uploaded";
                        }
                        objParams[12] = new SqlParameter("@P_TECHSPEC", objParty.TECHSPEC);
                        objParams[13] = new SqlParameter("@P_PNO", pno);
                        objParams[14] = new SqlParameter("@P_TVNO ", objParty.TVNO);


                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_TENDER_PARTY_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.UpdateItemMaster-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet getParty(int TenderNo, int TVNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_TENDERNO", TenderNo);
                        objParams[1] = new SqlParameter("@P_TVNO", TVNO);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTYDETAILS_GET_BY_TENDERNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_getParty-> " + ex.ToString());
                    }
                    return ds;
                }
                //TO GET VENDOR DETAILS FOR TENDER
                public DataSet getPartyForTender(int pno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PNO", pno);
                        //objParams[1] = new SqlParameter("@P_TVNO", TVNO);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_VENDOR_FOR_TENDER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_getParty-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet getPartyDetails(int TenderNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TENDERNO", TenderNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDER_PARTY_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_getPartyDetails-> " + ex.ToString());
                    }
                    return ds;
                }
                public int ReccomandParty(int TENDERNO, int TVNO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Update New Item_Master
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_TENDERNO", TENDERNO);
                        objParams[1] = new SqlParameter("@P_TVNO", TVNO);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_TENDER_TECHRECCOMANDATION_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.UpdateItemMaster-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int DReccomandParty(int TENDERNO, int TVNO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Update New Item_Master
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_TENDERNO", TENDERNO);
                        objParams[1] = new SqlParameter("@P_TVNO", TVNO);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_TENDER_TECHDRECCOMANDATION_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ItemMasterController.UpdateItemMaster-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //commercial bid
                public DataSet GetAllTendorCommercial()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDER_GET_ALL_TECHRECOMMENDED", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Invoice_Entry_Controller_GetAllVendor-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet getVendors(int TenderNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TENDERNO", TenderNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDER_PARTYDETAILS_GET_BY_TENDERNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_getVendor-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet getVendorsMod(int TenderNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TENDERNO", TenderNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDER_PARTYDETAILS_GET_BY_TENDERNO_MOD", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_getVendor-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet getVendor(int TenderNo, int TVNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_TENDERNO", TenderNo);
                        objParams[1] = new SqlParameter("@P_TVNO", TVNO);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_VENDORDETAILS_GET_BY_TENDERNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_getVendor-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetTenderItemByTenderNo(int TenderNo, int Tvno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_TENDERNO", TenderNo);
                        objParams[1] = new SqlParameter("@P_TVNO", Tvno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDER_GET_BIDITEM_BY_TENDERNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetVendorByQuotation-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetTenderBidItemByTenderNo(int TenderNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TENDERNO", TenderNo);
                        //objParams[1] = new SqlParameter("@P_TVNO", Tvno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDER_GET_TECH_BIDITEM_BY_TENDERNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetVendorByQuotation-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetFieldByTenderNo(int TenderNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TENDERNO", TenderNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDER_FIELDS_GETBY_TENDERNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetVendorByQuotation-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetTenderDetailsByTenderNo(int TenderNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TNO", TenderNo);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDER_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetVendorByQuotation-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetSinglePartyFieldsEnrty(int TENDERNO, int TVNO, int Fno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_TENDERNO", TENDERNO);
                        objParams[1] = new SqlParameter("@P_TVNO", TVNO);
                        objParams[2] = new SqlParameter("@P_FNO", Fno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDER_FIELDENTRY_BY_FIELDNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetSinglePartyFieldsEnrty-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetVendorEntryItemsByParty(int TENDERNO, int TVNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_TENDERNO", TENDERNO);
                        objParams[1] = new SqlParameter("@P_TVNO", TVNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTYITEMS_BY_TENDER_NO_AND_PARTY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetItemsByQuotNo-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetPartyFieldsEnrtyByParty(int TENDERNO, int TVNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_TENDERNO", TENDERNO);
                        objParams[1] = new SqlParameter("@P_TVNO", TVNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTYFIELDENTRY_BY_TENDER_NO_AND_PARTY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetVendorByQuotation-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetSingleRecordField(int fNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_FNO", fNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDER_FIELD_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FieldController.GetSingleRecordField-> " + ex.ToString());
                    }
                    return ds;
                }


                //comparitive statements
                public DataSet GetVendorForCmpRpt(int tenderno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TENDERNO", tenderno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDER_PARTYENTRY_GETALL_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetVendorByQuotation-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetItemsByTenderNo(int tenderno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TENDERNO", tenderno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDER_GETITEM_BY_TENDERNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetItemsByQuotNo-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetItemsForVendor(int Tenderno, int Pno, int itemno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_TENDERNO", Tenderno);
                        objParams[1] = new SqlParameter("@P_PNO", Pno);
                        objParams[2] = new SqlParameter("@P_ITEM_NO", itemno);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDER_PARTYITEMETRY_GET_BY_PARTY_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetVendorByQuotation-> " + ex.ToString());
                    }
                    return ds;
                }

                //Added for get status

                public DataSet GetStatus(int Tenderno, int Pno, int itemno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_TENDERNO", Tenderno);
                        objParams[1] = new SqlParameter("@P_PNO", Pno);
                        objParams[2] = new SqlParameter("@P_ITEM_NO", itemno);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDER_BIDDIBG_DTATUS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetVendorByQuotation-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetExtraCharge(int Tenderno, int Pno, int itemno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_TENDERNO", Tenderno);
                        objParams[1] = new SqlParameter("@P_PNO", Pno);
                        objParams[2] = new SqlParameter("@P_ITEM_NO", itemno);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTY_TENDER_COMPARATIVESTATEMENT_FOR_VAT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetVendorByQuotation-> " + ex.ToString());
                    }
                    return ds;
                }

                //
                public DataSet GetAllTendorComp()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDER_GET_ALL_TECHRECOMMENDED_COMP", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Invoice_Entry_Controller_GetAllVendor-> " + ex.ToString());
                    }
                    return ds;
                }

                // added for Save tender items for Technical Bidding

                public int SavePartyItemsTechBidding(STR_TENDER_ITEM_ENTRY objPItem, string colcode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_TENDERNO ", objPItem.TENDERNO);
                        objParams[1] = new SqlParameter("@P_ITEM_NO", objPItem.ITEM_NO);
                        objParams[2] = new SqlParameter("@P_PERTICULAR", objPItem.ITEMDETAIL);
                        objParams[3] = new SqlParameter("@P_TVNO ", objPItem.TVNO);
                        objParams[4] = new SqlParameter("@P_TNO", objPItem.TINO);
                        objParams[5] = new SqlParameter("@P_STATUS", objPItem.TECHSPEC);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", colcode);
                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_TENDERITEMENTRY_INSERT_TECH", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Vendor_Quotation_Entry_Controller.SavePartyItemsEntry-> " + ex.ToString());
                    }
                    return retStatus;
                }


                // added for Save tender items for Technical Bidding

                public int UpdatePartyItemsTechBidding(STR_TENDER_ITEM_ENTRY objPItem, string colcode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_TENDERNO ", objPItem.TENDERNO);
                        objParams[1] = new SqlParameter("@P_ITEM_NO", objPItem.ITEM_NO);
                        objParams[2] = new SqlParameter("@P_PERTICULAR", objPItem.ITEMDETAIL);
                        objParams[3] = new SqlParameter("@P_TVNO ", objPItem.TVNO);
                        objParams[4] = new SqlParameter("@P_TNO", objPItem.TINO);
                        objParams[5] = new SqlParameter("@P_STATUS", objPItem.TECHSPEC);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", colcode);
                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_TENDERITEMENTRY_INSERT_TECH", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Vendor_Quotation_Entry_Controller.UpdatePartyItemsTechBidding-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //Added for getting items for technical bidding 
                public DataSet GetTenderItemsByTenderNo(int tenderno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TENDERNO", tenderno);
                        //   ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDER_GET_BIDDING_ITEM_BY_TENDERNO", objParams);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDER_GET_BIDITEM_BY_TENDERNO", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetItemsByQuotNo-> " + ex.ToString());
                    }
                    return ds;
                }

                // Added for get vendor
                public DataSet GetVendorForTechBid(int tenderno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TENDERNO", tenderno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDER_PARTYENTRY_TECHBID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetVendorByQuotation-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetTaxesForTender(string Tenderno, int Tvno, decimal TaxableAmt, decimal BasicAmt, int ItemNo, int IsState)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_TENDERNO", Tenderno);
                        objParams[1] = new SqlParameter("@P_TVNO", Tvno);
                        objParams[2] = new SqlParameter("@P_TAXABLE_AMT", TaxableAmt);
                        objParams[3] = new SqlParameter("@P_BASIC_AMT", BasicAmt);
                        objParams[4] = new SqlParameter("@P_ITEM_NO", ItemNo);
                        objParams[5] = new SqlParameter("@P_IsStateTax", IsState);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_TAXES_BY_TENDER_TVNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetTaxes-> " + ex.ToString());
                    }
                    return ds;
                }

                #region Comparative Stamnt
                public DataSet GetVendorForTechCmpRpt(int tenderno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TNO", tenderno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TECH_COMP_GET_VENDOR", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetVendorForCmpRpt-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetItemsByTendernoForTechCmpRpt(int Tenderno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TNO", Tenderno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TECH_COMP_ITEMS_BY_TNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetItemsByTenderno-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetItemsForTenderVendorTechCmpRpt(int Tenderno, int Pno, int itemno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_TNO", Tenderno);
                        objParams[1] = new SqlParameter("@P_PNO", Pno);
                        objParams[2] = new SqlParameter("@P_ITEM_NO", itemno);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TECH_COMP_ITEMS_FOR_VENDOR", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetVendorByQuotation-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetVendorForFinancialCmpRpt(int Tenderno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TNO", Tenderno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_FINANCE_COMP_GET_VENDOR", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Vendor_Tender_Entry_Controller_GetVendorForFinancialCmpRpt-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetItemsByTnoForFinancialCmpRpt(int Tenderno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TNO", Tenderno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_FINANCE_COMP_GET_ITEMS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Vendor_Tender_Entry_Controller.GetItemsByTnoForFinancialCmpRp-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetItemsForTenderVendorFinancialCmpRpt(int Tenderno, int Tvno, int Itemno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_TNO", Tenderno);
                        objParams[1] = new SqlParameter("@P_TVNO", Tvno);
                        objParams[2] = new SqlParameter("@P_ITEM_NO", Itemno);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_FINANCE_COMP_GET_ITEMS_BY_TNO_TVNO_ITEM_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Vendor_Tender_Entry_Controller_GetItemsForTenderVendor-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetTaxHeadsForFinancialCmpRpt(int Quotno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TNO", Quotno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_FINANACE_COMP_GET_TAX_HEADS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Vendor_Tender_Entry_Controller_GetTaxHeadsForFinancialCmpRpt-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetTaxesForFinancialCmpRpt(int Tno, int Tvno, int Taxid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_TNO", Tno);
                        objParams[1] = new SqlParameter("@P_TVNO", Tvno);
                        objParams[2] = new SqlParameter("@P_TAXID", Taxid);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_FINANACE_COMP_GET_TAX_AMOUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetCalculativeCharges-> " + ex.ToString());
                    }
                    return ds;
                }


                #endregion
            }

        }

    }

}
