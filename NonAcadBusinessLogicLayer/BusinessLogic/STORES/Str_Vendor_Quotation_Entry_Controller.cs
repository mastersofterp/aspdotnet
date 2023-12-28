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

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
           public class Str_Vendor_Quotation_Entry_Controller
            {

               private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

               public DataSet GetVendorByQuotation(string Quotno)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null; ;
                       objParams = new SqlParameter[1];
                       objParams[0] = new SqlParameter("@P_QUOTNO", Quotno);

                       ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTYENTRY_GETALL_BY_QUOTNO", objParams);
                   }
                   catch (Exception ex)
                   {
                       return ds;
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetVendorByQuotation-> " + ex.ToString());
                   }
                   return ds;
               }
               public DataSet GetQuotationByDepartment(int Mdno)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null; ;
                       objParams = new SqlParameter[1];
                       objParams[0] = new SqlParameter("@P_MDNO", Mdno );

                       ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_QUOTATIONENTRY_GET_BY_MDNO", objParams);
                   }
                   catch (Exception ex)
                   {
                       return ds;
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetQuotationByDepartment()-> " + ex.ToString());
                   }
                   return ds;
               }

               public DataSet GetQuotationForApproval(int Mdno)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null; ;
                       objParams = new SqlParameter[1];
                       objParams[0] = new SqlParameter("@P_MDNO", Mdno);

                       ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_QUOTATION_FOR_APPROVAL", objParams);
                   }
                   catch (Exception ex)
                   {
                       return ds;
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetQuotationForApproval()-> " + ex.ToString());
                   }
                   return ds;
               }
               public DataSet GetSingleQuotation(string quotno)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null; ;
                       objParams = new SqlParameter[1];
                       objParams[0] = new SqlParameter("@P_QUOTNO", quotno);
                       ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_QUOTATIONENTRY_GET_BY_NO", objParams);
                   }
                   catch (Exception ex)
                   {
                       return ds;
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetVendorsForQuotation-> " + ex.ToString());
                   }
                   return ds;
               }
               public DataSet GetItemsByQuotNo(string quotno, int Pno)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null; ;
                       objParams = new SqlParameter[2];
                       objParams[0] = new SqlParameter("@P_QUOTNO", quotno);
                       objParams[1] = new SqlParameter("@P_PNO", Pno);
                       ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_ITEMS_BY_QUOTATION_NO", objParams);
                   }
                   catch (Exception ex)
                   {
                       return ds;
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetItemsByQuotNo-> " + ex.ToString());
                   }
                   return ds;
               }
            

               public int SavePartyItemsEntry(STR_PARTY_ITEM_ENTRY objPItem, string colcode)
               {
                   int retStatus = Convert.ToInt32(CustomStatus.Others);

                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null;
                       //Add New BUDGETHEAD
                       objParams = new SqlParameter[18];
                       
                       objParams[0] = new SqlParameter("@P_QUOTNO", objPItem.QUOTNO);
                       objParams[1] = new SqlParameter("@P_ITEM_NO", objPItem.ITEM_NO);                      
                       objParams[2] = new SqlParameter("@P_PRICE", objPItem.PRICE);
                       //objParams[3] = new SqlParameter("@P_UNIT", objPItem.UNIT);  
                       objParams[3] = new SqlParameter("@P_FLAG", objPItem.FLAG);
                       objParams[4] = new SqlParameter("@P_MDNO", objPItem.MDNO);
                       objParams[5] = new SqlParameter("@P_EDATE", objPItem.EDATE);                      
                       objParams[6] = new SqlParameter("@P_COLLEGE_CODE", colcode);
                       objParams[7] = new SqlParameter("@P_PNO", objPItem.PNO);
                       objParams[8] = new SqlParameter("@P_QTY", objPItem.QTY);                      
                       objParams[9] = new SqlParameter("@P_TOTAMOUNT", objPItem.TOTAMOUNT);                     
                       objParams[10] = new SqlParameter("@P_DISCOUNTPERCENT", objPItem.DISCOUNT);
                       objParams[11] = new SqlParameter("@P_DISCOUNTAMOUNT", objPItem.DISCOUNTAMOUNT);
                       objParams[12] = new SqlParameter("@P_TAXABLE_AMT", objPItem.TAXABLE_AMT);
                       objParams[13] = new SqlParameter("@P_TAX_AMT", objPItem.TAX_AMT);
                       objParams[14] = new SqlParameter("@P_ITEM_REMARK", objPItem.ITEM_REMARK);
                       objParams[15] = new SqlParameter("@P_QUALITY_QTY_SPEC", objPItem.QUALITY_QTY_SPEC);
                       if (objPItem.TECHSPECH == null)
                       {
                           objPItem.TECHSPECH = "-";
                       }
                       objParams[16] = new SqlParameter("@P_TECHSPECH", objPItem.TECHSPECH);   
                       objParams[17] = new SqlParameter("@P_PINO", SqlDbType.Int);
                       objParams[17].Direction = ParameterDirection.Output;

                       retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_PARTYITEMENTRY_INSERT", objParams, true));

                   }
                   catch (Exception ex)
                   {
                       retStatus = Convert.ToInt32(CustomStatus.Error);
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Vendor_Quotation_Entry_Controller.SavePartyItemsEntry-> " + ex.ToString());
                   }
                   return retStatus;
               }

               public int SavePartyFieldEntry(STR_PARTY_FIELD_ENTRY objPFentry ,string colcode)
               {

                   int retStatus = Convert.ToInt32(CustomStatus.Others);

                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null;
                       //Add New BUDGETHEAD
                       objParams = new SqlParameter[6];                     
                       objParams[0] = new SqlParameter("@P_QUOTNO", objPFentry.QUOTNO) ;
                       objParams[1] = new SqlParameter("@P_PNO", objPFentry.PNO );                      
                       objParams[2] = new SqlParameter("@P_COLLEGE_CODE", colcode);
                       objParams[3] = new SqlParameter("@P_VENDOR_TAX_TBL", objPFentry.VENDOR_TAX_TBL);
                       objParams[4] = new SqlParameter("@P_VENDRQUOT_UPLOAD_FILE_TBL", objPFentry.VENDRQUOT_UPLOAD_FILE_TBL);  //12/12/223
                       objParams[5] = new SqlParameter("@P_PFNO", SqlDbType.Int);
                       objParams[5].Direction = ParameterDirection.Output;

                       retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_PARTYFIELDENTRY_INSERT", objParams, true));

                   }
                   catch (Exception ex)
                   {
                       retStatus = Convert.ToInt32(CustomStatus.Error);
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Vendor_Quotation_Entry_Controller.SavePartyItemsEntry-> " + ex.ToString());
                   }
                   return retStatus;

               }


               // for item wise tax
               public int SaveItemWiseTaxEntry(int itemno,int vno,decimal taxper,decimal taxamt,int collegecode,string  quotno)
               {

                   int retStatus = Convert.ToInt32(CustomStatus.Others);

                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null;
                       //Add New BUDGETHEAD
                       objParams = new SqlParameter[7];

                       objParams[0] = new SqlParameter("@P_ITEM_NO", itemno);
                       objParams[1] = new SqlParameter("@P_PNO", vno);
                       objParams[2] = new SqlParameter("@P_TAXPER", taxper);
                       objParams[3] = new SqlParameter("@P_TOTAMOUNT", taxamt);
                       objParams[4] = new SqlParameter("@P_COLLEGE_CODE", collegecode);
                       objParams[5] = new SqlParameter("@P_QUOTNO", quotno);
                       objParams[6] = new SqlParameter("@P_PFNO", SqlDbType.Int);
                       objParams[6].Direction = ParameterDirection.Output;

                       retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_PARTYITEMENTRY_ITEMWISE_TAX", objParams, true));

                   }
                   catch (Exception ex)
                   {
                       retStatus = Convert.ToInt32(CustomStatus.Error);
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Vendor_Quotation_Entry_Controller.SavePartyItemsEntry-> " + ex.ToString());
                   }
                   return retStatus;

               }


               public int SavePartyFieldEntryForTender(STR_TENDER_FIELD_ENTRY objPFentry, string colcode)
               {

                   int retStatus = Convert.ToInt32(CustomStatus.Others);

                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null;
                       //Add New BUDGETHEAD
                       objParams = new SqlParameter[10];
                       objParams[0] = new SqlParameter("@P_TFNO", SqlDbType.Int);
                       objParams[0].Direction = ParameterDirection.Output;
                       objParams[1] = new SqlParameter("@P_TENDERNO", objPFentry.TENDERNO);
                       objParams[2] = new SqlParameter("@P_TVNO", objPFentry.TVNO);
                       objParams[3] = new SqlParameter("@P_FNO", objPFentry.FNO);
                       objParams[4] = new SqlParameter("@P_INFO", objPFentry.INFO);
                       objParams[5] = new SqlParameter("@P_AMT", objPFentry.AMT);
                       objParams[6] = new SqlParameter("@P_CDEPENDSON", objPFentry.C_CALDEPEND_ON);
                       objParams[7] = new SqlParameter("@P_PDEPENDSON", objPFentry.P_CALDEPEND_ON);
                       objParams[8] = new SqlParameter("@P_PERCENTAGE", objPFentry.PERCENTAGE);
                       objParams[9] = new SqlParameter("@P_COLLEGE_CODE", colcode);


                       retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_TENDER_FIELDENTRY_INSERT", objParams, true));

                   }
                   catch (Exception ex)
                   {
                       retStatus = Convert.ToInt32(CustomStatus.Error);
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Vendor_Quotation_Entry_Controller.SavePartyItemsEntry-> " + ex.ToString());
                   }
                   return retStatus;

               }
               public DataSet SetReportDataForCmpSingle(string quotno,int  itemno)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null; ;
                       objParams = new SqlParameter[2];
                       objParams[0] = new SqlParameter("@P_QUOTNO", quotno);
                       objParams[1] = new SqlParameter("@P_ITEM_NO", itemno);
                       ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTY_COMPARATIVESTATEMENT_REPORT", objParams);
                   }
                   catch (Exception ex)
                   {
                       return ds;
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetItemsByQuotNo-> " + ex.ToString());
                   }
                   return ds;
               }
               public DataSet GetVendorForCmpRpt(string Quotno)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null; ;
                       objParams = new SqlParameter[1];
                       objParams[0] = new SqlParameter("@P_QUOTNO", Quotno);

                       ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTYENTRY_GETALL_REPORT_FOR_TECH_COMP", objParams);
                   }
                   catch (Exception ex)
                   {
                       return ds;
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetVendorByQuotation-> " + ex.ToString());
                   }
                   return ds;
               }
               public DataSet GetItemsForVendor(string Quotno,int Pno,int itemno)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null; ;
                       objParams = new SqlParameter[3];
                       objParams[0] = new SqlParameter("@P_QUOTNO", Quotno);
                       objParams[1] = new SqlParameter("@P_PNO", Pno );
                       objParams[2] = new SqlParameter("@P_ITEM_NO", itemno );


                       ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTYITEMETRY_GET_BY_PARTY_REPORT", objParams);
                   }
                   catch (Exception ex)
                   {
                       return ds;
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetVendorByQuotation-> " + ex.ToString());
                   }
                   return ds;
               }


               //FOR GET EXTRA CHARGE [PKG_STR_PARTY_COMPARATIVESTATEMENT_REPORT_VAT]

               public DataSet GetExtraCharge(string Quotno, int Pno)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null; ;
                       objParams = new SqlParameter[2];
                       objParams[0] = new SqlParameter("@P_QUOTNO", Quotno);
                       objParams[1] = new SqlParameter("@P_PNO", Pno);
                      // objParams[2] = new SqlParameter("@P_ITEM_NO", itemno);


                       ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTY_COMPARATIVESTATEMENT_REPORT_VAT", objParams);
                   }
                   catch (Exception ex)
                   {
                       return ds;
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetVendorByQuotation-> " + ex.ToString());
                   }
                   return ds;
               }

               public DataSet GetVendorEntryItemsByParty(string quotno,int Pno)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null; ;
                       objParams = new SqlParameter[2];
                       objParams[0] = new SqlParameter("@P_QUOTNO", quotno);
                       objParams[1] = new SqlParameter("@P_PNO", Pno );
                       ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTYITEMS_BY_QUOTATION_NO_AND_PARTY", objParams);
                   }
                   catch (Exception ex)
                   {
                       return ds;
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetItemsByQuotNo-> " + ex.ToString());
                   }
                   return ds;
               }
               public DataSet GetVendorByQuotationForModification(string Quotno)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null; ;
                       objParams = new SqlParameter[1];
                       objParams[0] = new SqlParameter("@P_QUOTNO", Quotno);

                       ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTYENTRY_GETALL_BY_QUOTNO_EDIT", objParams);
                   }
                   catch (Exception ex)
                   {
                       return ds;
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetVendorByQuotation-> " + ex.ToString());
                   }
                   return ds;
               }
               public DataSet GetPartyEnrtyByNO(int pino,char istype)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null; ;
                       objParams = new SqlParameter[2];
                       objParams[0] = new SqlParameter("@P_PINO", pino );
                       objParams[1] = new SqlParameter("@P_ISTYPE", istype);

                       ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTYITEMS_BY_NO", objParams);
                   }
                   catch (Exception ex)
                   {
                       return ds;
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetVendorByQuotation-> " + ex.ToString());
                   }
                   return ds;
               }
             

               
               public DataSet GetPartyEnrtyByREQTRNO(int REQTRNO)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null; ;
                       objParams = new SqlParameter[1];
                       objParams[0] = new SqlParameter("@P_PINO", REQTRNO);

                       ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTYITEMS_BY_NO_DPO", objParams);
                   }
                   catch (Exception ex)
                   {
                       return ds;
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetVendorByQuotation-> " + ex.ToString());
                   }
                   return ds;
               }
               
               public int UpdatePartyItemsEntry(STR_PARTY_ITEM_ENTRY objPItem, string colcode)
               {
                   int retStatus = Convert.ToInt32(CustomStatus.Others);

                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null;

                       objParams = new SqlParameter[]{
                      
                       new SqlParameter("@P_PRICE", objPItem.PRICE),
                       //new SqlParameter("@P_UNIT", objPItem.UNIT),
                       new SqlParameter("@P_EDATE", objPItem.EDATE),                      
                       new SqlParameter("@P_COLLEGE_CODE", colcode),
                       new SqlParameter("@P_QTY", objPItem.QTY),
                       new SqlParameter("@P_PINO", objPItem.PINO),                      
                       new SqlParameter("@P_DISC_PERCENT", objPItem.DISCOUNT),                     
                       new SqlParameter("@P_DISCOUNTAMOUNT", objPItem.DISCOUNTAMOUNT),
                       new SqlParameter("@P_TOTAMOUNT", objPItem.TOTAMOUNT),
                       new SqlParameter("@P_QUOTNO", objPItem.QUOTNO),
                       new SqlParameter("@P_PNO", objPItem.PNO),
                       new SqlParameter("@P_TAXABLE_AMT", objPItem.TAXABLE_AMT),                       
                       new SqlParameter("@P_ITEM_REMARK", objPItem.ITEM_REMARK),
                       new SqlParameter("@P_QUALITY_QTY_SPEC", objPItem.QUALITY_QTY_SPEC),
                       new SqlParameter("@P_TECHSPECH", objPItem.TECHSPECH),
                       new SqlParameter("@P_TAX_AMT", objPItem.TAX_AMT)
                       
                   };
                       retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_PARTYITEMENTRY_UPDATE", objParams, true));

                   }
                   catch (Exception ex)
                   {
                       retStatus = Convert.ToInt32(CustomStatus.Error);
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Vendor_Quotation_Entry_Controller.SavePartyItemsEntry-> " + ex.ToString());
                   }
                   return retStatus;
               }
               public int UpdatePartyItemsEntryTender(STR_PARTY_ITEM_ENTRY objPItem, string colcode)
               {
                   int retStatus = Convert.ToInt32(CustomStatus.Others);

                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null;
                       //Add New BUDGETHEAD
                       objParams = new SqlParameter[7];
                       
                      //objParams[0] = new SqlParameter("@P_ITEMDETAIL", objPItem.ITEMDETAIL);
                      // objParams[2] = new SqlParameter("@P_MANUFACTURE", objPItem.MANUFACTURE);
                      // objParams[3] = new SqlParameter("@P_MODELNO", objPItem.MODELNO);
                      
                       objParams[0] = new SqlParameter("@P_TAX", objPItem.TAX);
                       objParams[1] = new SqlParameter("@P_PRICE", objPItem.PRICE);
                       objParams[2] = new SqlParameter("@P_TECHSPEC", objPItem.TECHSPECH);
                       objParams[3] = new SqlParameter("@P_CURRENCY", objPItem.CURRENCY);
                       objParams[4] = new SqlParameter("@P_COLLEGE_CODE", colcode);
                       objParams[5] = new SqlParameter("@P_QTY", objPItem.QTY);
                       objParams[6] = new SqlParameter("@P_PINO", objPItem.PINO);
                       retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_PARTYITEMENTRY_TENDER_UPDATE", objParams, true));

                   }
                   catch (Exception ex)
                   {
                       retStatus = Convert.ToInt32(CustomStatus.Error);
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Vendor_Quotation_Entry_Controller.SavePartyItemsEntry-> " + ex.ToString());
                   }
                   return retStatus;
               }
               public DataSet GetPartyFieldsEnrtyByParty(string quotno, int Pno)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null; ;
                       objParams = new SqlParameter[2];
                       objParams[0] = new SqlParameter("@P_QUOTNO", quotno);
                       objParams[1] = new SqlParameter("@P_PNO", Pno);
                       ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTYFIELDENTRY_BY_QUOTATION_NO_AND_PARTY", objParams);
                   }
                   catch (Exception ex)
                   {
                       return ds;
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetVendorByQuotation-> " + ex.ToString());
                   }
                   return ds;
               }
               public DataSet GetSinglePartyFieldsEnrty(string quotno, int Pno,int Fno)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null; ;
                       objParams = new SqlParameter[3];
                       objParams[0] = new SqlParameter("@P_QUOTNO", quotno);
                       objParams[1] = new SqlParameter("@P_PNO", Pno);
                       objParams[2] = new SqlParameter("@P_FNO", Fno );
                       ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTYFIELDENTRY_BY_FIELDNO", objParams);
                   }
                   catch (Exception ex)
                   {
                       return ds;
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetVendorByQuotation-> " + ex.ToString());
                   }
                   return ds;
               }




               public DataSet GetCollegeInfo()
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null; ;
                       objParams = new SqlParameter[0];
                       ds = objSQLHelper.ExecuteDataSetSP("PKG_REPORT_COLLEGE_LOGO_INFO", objParams);
                   }
                   catch (Exception ex)
                   {
                       return ds;
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetVendorByQuotation-> " + ex.ToString());
                   }
                   return ds;
               }



               public string ReadText(System.Web.UI.WebControls.FileUpload fld)
               {
                   try
                   {
                       System.IO.StreamReader sr = new System.IO.StreamReader(fld.PostedFile.FileName);

                       string text = sr.ReadToEnd();
                       sr.Close();
                       return text;
                   }
                   catch (Exception Ex)
                   {
                       return string.Empty;
                   }

               }

               public object GetPartyEnrtyByNO(int PINO, string p)
               {
                   throw new NotImplementedException();
               }


               public DataSet GetDetailForComparative(string Quotno)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null; ;
                       objParams = new SqlParameter[1];
                       objParams[0] = new SqlParameter("@P_QUOTNO", Quotno);

                       ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_COMPARATIVE_STATEMENT", objParams);
                   }
                   catch (Exception ex)
                   {
                       return ds;
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetQuotationByDepartment()-> " + ex.ToString());
                   }
                   return ds;
               }


               public DataSet GetFromDataForEmail(string quotno)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null; ;
                       objParams = new SqlParameter[1];
                       objParams[0] = new SqlParameter("@P_QUOTNO", quotno);
                       ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_DATA_TO_SEND_EMAIL", objParams);
                   }
                   catch (Exception ex)
                   {
                       return ds;
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller.GetFromDataForEmail-> " + ex.ToString());
                   }
                   return ds;
               }



               public int SendStatementForApproval(string QUOT_REFNO)
               {
                   int retstatus = 0;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null;

                       objParams = new SqlParameter[2];
                       objParams[0] = new SqlParameter("@P_QUOT_REFNO", QUOT_REFNO);                      
                       objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                       objParams[1].Direction = ParameterDirection.Output;

                       object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STR_SEND_QUOTATION_FOR_APPROVAL", objParams, true);
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
                       throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FileMovementController.AddUpdateDocumentType->" + ex.ToString());
                   }
                   return retstatus;
               }



               public int UpdateComparativeStatApproval(Str_Quotation_Tender objQT)
               {
                   int retStatus = Convert.ToInt32(CustomStatus.Others);

                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null;
                       
                       objParams = new SqlParameter[5];
                       objParams[0] = new SqlParameter("@P_QUOTNO", objQT.QUOTNO);
                       objParams[1] = new SqlParameter("@P_REFNO", objQT.REFNO);
                       objParams[2] = new SqlParameter("@P_ISAPPROVE", objQT.ISAPPROVE);
                       objParams[3] = new SqlParameter("@P_APPROVAL_REMARK", objQT.APPROVAL_REMARK);
                       objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                       objParams[4].Direction = ParameterDirection.Output;
                                          
                       retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_COMPARATIVE_STATEMENT_STATUS_UPDATE", objParams, true));

                   }
                   catch (Exception ex)
                   {
                       retStatus = Convert.ToInt32(CustomStatus.Error);
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Vendor_Quotation_Entry_Controller.UpdateComparativeStatApproval-> " + ex.ToString());
                   }
                   return retStatus;
               }

               public DataSet GetCalculativHeadsForCmpRpt(string Quotno)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null; ;
                       objParams = new SqlParameter[1];
                      objParams[0] = new SqlParameter("@P_QUOTNO", Quotno);
                      ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTYENTRY_GET_CALCULATIVE_HEADS_REPORT", objParams);
                   }
                   catch (Exception ex)
                   {
                       return ds;
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetVendorByQuotation-> " + ex.ToString());
                   }
                   return ds;
               }

               public DataSet GetCalculativeCharges(string Quotno, int Pno,int Fno)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null; ;
                       objParams = new SqlParameter[3];
                       objParams[0] = new SqlParameter("@P_QUOTNO", Quotno);
                       objParams[1] = new SqlParameter("@P_PNO", Pno);
                       objParams[2] = new SqlParameter("@P_FNO", Fno);
                       ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTYENTRY_GET_CALCULATIVE_CHARGES_REPORT", objParams);
                   }
                   catch (Exception ex)
                   {
                       return ds;
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetCalculativeCharges-> " + ex.ToString());
                   }
                   return ds;
               }

               public int DeletePartyItemEntry(string Quotno, int Pno, int Itemno)
               {
                   int retStatus = Convert.ToInt32(CustomStatus.Others);                  
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null; ;
                       objParams = new SqlParameter[4];
                       objParams[0] = new SqlParameter("@P_QUOTNO", Quotno);
                       objParams[1] = new SqlParameter("@P_PNO", Pno);
                       objParams[2] = new SqlParameter("@P_ITEM_NO", Itemno);
                       objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                       objParams[3].Direction = ParameterDirection.Output;
                       retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_DELETE_PARTYITEMENTRY_BY_ITEMNO", objParams,true));                      
                   }
                   catch (Exception ex)
                   {
                       retStatus = Convert.ToInt32(CustomStatus.Error);
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_DeleteItemEntry-> " + ex.ToString());
                   }
                   return retStatus;
               }

               public DataSet GetPartyItemEntryDetails(string Quotno, int Pno, int Itemno)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null; ;
                       objParams = new SqlParameter[3];
                       objParams[0] = new SqlParameter("@P_QUOTNO", Quotno);
                       objParams[1] = new SqlParameter("@P_PNO", Pno);
                       objParams[2] = new SqlParameter("@P_ITEM_NO", Itemno);
                       ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_PARTYITEMENTRY_TO_EDIT", objParams);
                   }
                   catch (Exception ex)
                   {
                       return ds;
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetPartyItemEntryDetails-> " + ex.ToString());
                   }
                   return ds;
               }

               //public DataSet GetTaxes(decimal Rate, int ItemNo, int IsState)
               //{
               //    DataSet ds = null;
               //    try
               //    {
               //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
               //        SqlParameter[] objParams = new SqlParameter[3];
               //        objParams[0] = new SqlParameter("@P_RATE", Rate);
               //        objParams[1] = new SqlParameter("@P_ITEM_NO", ItemNo);
               //        objParams[2] = new SqlParameter("@P_IsStateTax", IsState);
               //        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_QUOT_GET_TAXES", objParams);
               //    }
               //    catch (Exception ex)
               //    {
               //        return ds;
               //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller.GetTaxes-> " + ex.ToString());
               //    }
               //    return ds;
               //}

               public DataSet GetTaxes(string quotno, int Pno, decimal TaxableAmt,decimal BasicAmt, int ItemNo, int IsState)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null; ;
                       objParams = new SqlParameter[6];
                       objParams[0] = new SqlParameter("@P_QUOTNO", quotno);
                       objParams[1] = new SqlParameter("@P_PNO", Pno);
                       objParams[2] = new SqlParameter("@P_TAXABLE_AMT", TaxableAmt);
                       objParams[3] = new SqlParameter("@P_BASIC_AMT", BasicAmt);
                       objParams[4] = new SqlParameter("@P_ITEM_NO", ItemNo);
                       objParams[5] = new SqlParameter("@P_IsStateTax", IsState);
                       ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_TAXES_BY_QUOT_PNO", objParams);
                   }
                   catch (Exception ex)
                   {
                       return ds;
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetTaxes-> " + ex.ToString());
                   }
                   return ds;
               }
              
            }
        }
    }
}
