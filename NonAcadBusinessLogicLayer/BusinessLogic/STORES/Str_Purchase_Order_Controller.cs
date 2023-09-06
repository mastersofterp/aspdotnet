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
using BusinessLogicLayer.BusinessEntities.STORES;
namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
          public  class Str_Purchase_Order_Controller
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

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTY_GETALL_FOR_PO_BY_QUOTNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Purchase_Order_Controller_GetVendorByQuotation-> " + ex.ToString());
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
                        objParams[0] = new SqlParameter("@P_MDNO", Mdno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_QUOTATIONENTRY_GET_BY_MDNO_FOR_PO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Purchase_Order_Controller_GetQuotationByDepartment()-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetItemsRecommandToVendor(string quotno, int pno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_QUOTNO", quotno);
                        objParams[1] = new SqlParameter("@P_PNO", pno );

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_RECOMMAND_ITEM_GET_BY_PARTY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Purchase_Order_Controller_GetItemsRecommandToVendor()-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetFieldsRecommandToVendor(string quotno, int pno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_QUOTNO", quotno);
                        objParams[1] = new SqlParameter("@P_PNO", pno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_RECOMMAND_FIELD_GET_BY_PARTY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Purchase_Order_Controller_GetFieldsRecommandToVendor()-> " + ex.ToString());
                    }
                    return ds;
                }
                public int Add_Security_Entry_Good(Str_Entry_Goods ObjSEG,int action)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                      
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_PORDNO ", ObjSEG.PORDNO);

                        objParams[1] = new SqlParameter("@P_DCBILL_NO",ObjSEG.DCBILL_NO );
                        objParams[2] = new SqlParameter("@P_DATE_TIME",ObjSEG.DATE_TIME );
                        objParams[3] = new SqlParameter("@P_VEHICLE_NO",ObjSEG.VEHICLE_NO);
                        objParams[4] = new SqlParameter("@P_SEC_SERIAL_NO",ObjSEG.SEC_SERIAL_NO);
                        objParams[5] = new SqlParameter("@P_QTY", ObjSEG.QTY);
                        objParams[6] = new SqlParameter("@P_ENTRY_TIME", ObjSEG.E_TIME);
                        objParams[7] = new SqlParameter("@P_ACTION", action);
                        objParams[8] = new SqlParameter("@P_REMARK", ObjSEG.REMARK);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_INS_SECURITY_ENTRY_GOOD", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
      
                    }
                    catch(Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Purchase_Order_Controller.Add_Security_Entry_Good-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int SavePO(Str_Purchase_Order objPO, string colcode,string ItemNos)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[37];
                        objParams[0] = new SqlParameter("@P_PORDNO ", SqlDbType.Int);
                        objParams[0].Direction = ParameterDirection.Output;
                        objParams[1] = new SqlParameter("@P_QUOTNO", objPO.QUOTNO );
                        objParams[2] = new SqlParameter("@P_REFNO", objPO.REFNO );
                        objParams[3] = new SqlParameter("@P_MDNO", objPO.MDNO);
                        objParams[4] = new SqlParameter("@P_PNO  ", objPO.PNO);
                        objParams[5] = new SqlParameter("@P_PINO", objPO.PINO );
                        objParams[6] = new SqlParameter("@P_FOOTER1 ", objPO.FOOTER1 );
                        objParams[7] = new SqlParameter("@P_TERM1 ", objPO.TERM1 );

                        if (objPO.SDATE == DateTime.MinValue)
                        {
                            objParams[8] = new SqlParameter("@P_SDATE", DBNull.Value);
                        }
                        else
                        {
                            objParams[8] = new SqlParameter("@P_SDATE", objPO.SDATE);
                        }

                        if (objPO.TDATE == DateTime.MinValue)
                        {
                            objParams[9] = new SqlParameter("@P_TDATE", DBNull.Value);
                        }
                        else
                        {
                            objParams[9] = new SqlParameter("@P_TDATE", objPO.TDATE);
                        }

                       // objParams[8] = new SqlParameter("@P_SDATE", objPO.SDATE );
                        //objParams[9] = new SqlParameter("@P_TDATE", objPO.TDATE);
                        objParams[10] = new SqlParameter("@P_SUBJECT", objPO.SUBJECT);
                        objParams[11] = new SqlParameter("@P_FLAG", objPO.FLAG );
                        objParams[12] = new SqlParameter("@P_FLAGINV", objPO.FLAGINV );
                        objParams[13] = new SqlParameter("@P_SDPER", objPO.SDPER);
                        objParams[14] = new SqlParameter("@P_ENCL", objPO.ENCL);
                        objParams[15] = new SqlParameter("@P_COPYTO", objPO.COPYTO);
                        objParams[16] = new SqlParameter("@P_HA", objPO.HA);
                        objParams[17] = new SqlParameter("@P_SIGN", objPO.SIGN);
                        objParams[18] = new SqlParameter("@P_REMARK", objPO.REMARK);
                        objParams[19] = new SqlParameter("@P_TECHCLAR", objPO.TECHCLAR);
                        objParams[20] = new SqlParameter("@P_CIFCHARGE", objPO.CIFCHARGE);
                        objParams[21] = new SqlParameter("@P_CIFCHARGETEXT", objPO.CIFCHARGETEXT);
                        objParams[22] = new SqlParameter("@P_RELISHED", objPO.RELISHED);
                        objParams[23] = new SqlParameter("@P_AGREEMENT", objPO.AGREEMENT);
                        objParams[24] = new SqlParameter("@P_PENDING", objPO.PENDING);
                        objParams[25] = new SqlParameter("@P_DEL", objPO.DEL);
                        objParams[26] = new SqlParameter("@P_RELIES", objPO.RELIES);
                        objParams[27] = new SqlParameter("@P_CST", objPO.CST);
                        objParams[28] = new SqlParameter("@P_ED", objPO.ED);
                        objParams[29] = new SqlParameter("@P_COLLEGE_CODE",colcode);
                        objParams[30] = new SqlParameter("@P_ISTYPE", objPO.ISTYPE);
                        objParams[31] = new SqlParameter("@P_BANKGTY", objPO.BANKGTY);
                        objParams[32] = new SqlParameter("@P_AMOUNT", objPO.AMOUNT);
                        objParams[33] = new SqlParameter("@P_BANK_REMARK", objPO.BANK_REMARK);
                        objParams[34] = new SqlParameter("@P_ITEMNOS", ItemNos);
                        objParams[35] = new SqlParameter("@P_NatureOfWork", objPO.NatureOfWork);  //----16-03-2023
                        objParams[36] = new SqlParameter("@P_OurReferenceNo", objPO.OurReferenceNo); //----16-03-2023

                        if (objSQLHelper.ExecuteNonQuerySP("STR_PORDER_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.AddQuotationTender-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetAllPOByDepartment(int MDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_MDNO", MDNO) ;

                        ds = objSQLHelper.ExecuteDataSetSP("STR_PORDER_GET_BY_MDNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Purchase_Order_Controller_GetAllPOByDepartment()-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetAllPOByDepartmentforammend(int MDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_MDNO", MDNO);

                        ds = objSQLHelper.ExecuteDataSetSP("STR_PORDER_GET_BY_MDNO_FOR_AMMEND", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Purchase_Order_Controller_GetAllPOByDepartment()-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetAllUnlockPOByDepartment(int MDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_MDNO", MDNO);

                        ds = objSQLHelper.ExecuteDataSetSP("STR_PORDER_UNLOCK_GET_BY_MDNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Purchase_Order_Controller_GetAllUnlockPOByDepartment-> " + ex.ToString());
                    }
                    return ds;
                }
                public int UpdatePOLock(int pordno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PORDNO ",pordno );
                        if (objSQLHelper.ExecuteNonQuerySP("STR_PORDER_UPDATE_LOCK", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.UpdatePOLock-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetSinglePONO(int Pordno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PORDNO", Pordno );

                        ds = objSQLHelper.ExecuteDataSetSP("STR_PORDER_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Purchase_Order_Controller_GetAllUnlockPOByDepartment-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdatePO(Str_Purchase_Order objPO, string colcode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[36];
                        objParams[0] = new SqlParameter("@P_PORDNO ", objPO.PORDNO );
                        objParams[1] = new SqlParameter("@P_QUOTNO", objPO.QUOTNO);
                        objParams[2] = new SqlParameter("@P_REFNO", objPO.REFNO);
                        objParams[3] = new SqlParameter("@P_MDNO", objPO.MDNO);
                        objParams[4] = new SqlParameter("@P_PNO  ", objPO.PNO);
                        objParams[5] = new SqlParameter("@P_FOOTER1 ", objPO.FOOTER1);
                        objParams[6] = new SqlParameter("@P_TERM1 ", objPO.TERM1);
                        //objParams[7] = new SqlParameter("@P_SDATE", objPO.SDATE);
                        //objParams[8] = new SqlParameter("@P_TDATE ", objPO.TDATE);
                        if (objPO.SDATE == DateTime.MinValue)
                        {
                            objParams[7] = new SqlParameter("@P_SDATE", DBNull.Value);
                        }
                        else
                        {
                            objParams[7] = new SqlParameter("@P_SDATE", objPO.SDATE);
                        }

                        if (objPO.TDATE == DateTime.MinValue)
                        {
                            objParams[8] = new SqlParameter("@P_TDATE", DBNull.Value);
                        }
                        else
                        {
                            objParams[8] = new SqlParameter("@P_TDATE", objPO.TDATE);
                        }
                        objParams[9] = new SqlParameter("@P_SUBJECT", objPO.SUBJECT);
                        objParams[10] = new SqlParameter("@P_FLAG", objPO.FLAG);
                        objParams[11] = new SqlParameter("@P_FLAGINV", objPO.FLAGINV);
                        objParams[12] = new SqlParameter("@P_SDPER", objPO.SDPER);
                        objParams[13] = new SqlParameter("@P_ENCL", objPO.ENCL);
                        objParams[14] = new SqlParameter("@P_COPYTO", objPO.COPYTO);
                        objParams[15] = new SqlParameter("@P_HA", objPO.HA);
                        objParams[16] = new SqlParameter("@P_SIGN", objPO.SIGN);
                        objParams[17] = new SqlParameter("@P_REMARK", objPO.REMARK);
                        objParams[18] = new SqlParameter("@P_TECHCLAR", objPO.TECHCLAR);
                        objParams[19] = new SqlParameter("@P_CIFCHARGE", objPO.CIFCHARGE);
                        objParams[20] = new SqlParameter("@P_CIFCHARGETEXT", objPO.CIFCHARGETEXT);
                        objParams[21] = new SqlParameter("@P_RELISHED", objPO.RELISHED);
                        objParams[22] = new SqlParameter("@P_AGREEMENT", objPO.AGREEMENT);
                        objParams[23] = new SqlParameter("@P_PENDING", objPO.PENDING);
                        objParams[24] = new SqlParameter("@P_DEL", objPO.DEL);
                        objParams[25] = new SqlParameter("@P_RELIES", objPO.RELIES);
                        objParams[26] = new SqlParameter("@P_CST", objPO.CST);
                        objParams[27] = new SqlParameter("@P_ED", objPO.ED);
                        objParams[28] = new SqlParameter("@P_COLLEGE_CODE", colcode);
                        objParams[29] = new SqlParameter("@P_ISTYPE", objPO.ISTYPE);
                        objParams[30] = new SqlParameter("@P_AMENDNO", objPO.AMENDNO);
                        objParams[31] = new SqlParameter("@P_BANKGTY", objPO.BANKGTY);
                        objParams[32] = new SqlParameter("@P_AMOUNT", objPO.AMOUNT);
                        objParams[33] = new SqlParameter("@P_BANK_REMARK", objPO.BANK_REMARK);
                        objParams[34] = new SqlParameter("@P_NatureOfWork", objPO.NatureOfWork);  //----16-03-2023
                        objParams[35] = new SqlParameter("@P_OurReferenceNo", objPO.OurReferenceNo); //----16-03-2023

                        if (objSQLHelper.ExecuteNonQuerySP("STR_PORDER_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.AddQuotationTender-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetVendorByPO(int Pordno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PORDNO", Pordno);

                        ds = objSQLHelper.ExecuteDataSetSP("STR_PARTY_GET_BY_PORDER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Purchase_Order_Controller_GetAllUnlockPOByDepartment-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetItemsByPO(int Pordno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PORDNO", Pordno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_ITEM_FOR_POAMMENDMENT_GET_BY_PO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Purchase_Order_Controller_GetAllUnlockPOByDepartment-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetFieldsByPO(int Pordno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PORDNO", Pordno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTYFIELDENTRY_BY_PO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Purchase_Order_Controller_GetAllUnlockPOByDepartment-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetSinglePartyFieldsEnrty(int Pordno, int Fno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[2];
                       
                        objParams[0] = new SqlParameter("@P_PORDNO", Pordno);
                        objParams[1] = new SqlParameter("@P_FNO", Fno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTYFIELDENTRY_FOR_PO_BY_FIELDNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetVendorByQuotation-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GenratePno(int MDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_MDNO", MDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GENERATE_PORDERNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GenratePno-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetTenderDetails(int tenderno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TENDERNO",tenderno );

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDER_PARTYREC_GETALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Purchase_Order_Controller_GetItemsRecommandToVendor()-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetTenderItemDetails(int tenderno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TENDERNO", tenderno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDER_ITEMS_GETBYTENDERNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Purchase_Order_Controller_GetItemsRecommandToVendor()-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetTenderFields(int tenderno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TENDERNO", tenderno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDER_FIELDS_GETBY_TENDERNO_PO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Purchase_Order_Controller_GetItemsRecommandToVendor()-> " + ex.ToString());
                    }
                    return ds;
                }
                public int SaveTenderPO(Str_Purchase_Order objPO, string colcode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[36];
                        objParams[0] = new SqlParameter("@P_PORDNO ", SqlDbType.Int);
                        objParams[0].Direction = ParameterDirection.Output;
                        objParams[1] = new SqlParameter("@P_TENDERNO", objPO.TENDERNO);
                        objParams[2] = new SqlParameter("@P_REFNO", objPO.REFNO);
                        objParams[3] = new SqlParameter("@P_MDNO", objPO.MDNO);
                        objParams[4] = new SqlParameter("@P_PNO  ", objPO.PNO);
                        objParams[5] = new SqlParameter("@P_PINO", objPO.PINO);
                        objParams[6] = new SqlParameter("@P_FOOTER1 ", objPO.FOOTER1);
                        objParams[7] = new SqlParameter("@P_TERM1 ", objPO.TERM1);
                        objParams[8] = new SqlParameter("@P_SDATE", objPO.SDATE);
                        objParams[9] = new SqlParameter("@P_TDATE ", objPO.TDATE);
                        objParams[10] = new SqlParameter("@P_SUBJECT", objPO.SUBJECT);
                        objParams[11] = new SqlParameter("@P_FLAG", objPO.FLAG);
                        objParams[12] = new SqlParameter("@P_FLAGINV", objPO.FLAGINV);
                        objParams[13] = new SqlParameter("@P_SDPER", objPO.SDPER);
                        objParams[14] = new SqlParameter("@P_ENCL", objPO.ENCL);
                        objParams[15] = new SqlParameter("@P_COPYTO", objPO.COPYTO);
                        objParams[16] = new SqlParameter("@P_HA", objPO.HA);
                        objParams[17] = new SqlParameter("@P_SIGN", objPO.SIGN);
                        objParams[18] = new SqlParameter("@P_REMARK", objPO.REMARK);
                        objParams[19] = new SqlParameter("@P_TECHCLAR", objPO.TECHCLAR);
                        objParams[20] = new SqlParameter("@P_CIFCHARGE", objPO.CIFCHARGE);
                        objParams[21] = new SqlParameter("@P_CIFCHARGETEXT", objPO.CIFCHARGETEXT);
                        objParams[22] = new SqlParameter("@P_RELISHED", objPO.RELISHED);
                        objParams[23] = new SqlParameter("@P_AGREEMENT", objPO.AGREEMENT);
                        objParams[24] = new SqlParameter("@P_PENDING", objPO.PENDING);
                        objParams[25] = new SqlParameter("@P_DEL", objPO.DEL);
                        objParams[26] = new SqlParameter("@P_RELIES", objPO.RELIES);
                        objParams[27] = new SqlParameter("@P_CST", objPO.CST);
                        objParams[28] = new SqlParameter("@P_ED", objPO.ED);
                        objParams[29] = new SqlParameter("@P_COLLEGE_CODE", colcode);
                        objParams[30] = new SqlParameter("@P_ISTYPE", objPO.ISTYPE);
                        objParams[31] = new SqlParameter("@P_BANKGTY", objPO.BANKGTY);
                        objParams[32] = new SqlParameter("@P_AMOUNT", objPO.AMOUNT);
                        objParams[33] = new SqlParameter("@P_BANK_REMARK", objPO.BANK_REMARK);
                        objParams[34] = new SqlParameter("@P_NatureOfWork", objPO.NatureOfWork);  //----16-03-2023
                        objParams[35] = new SqlParameter("@P_OurReferenceNo", objPO.OurReferenceNo); //----16-03-2023

                        if (objSQLHelper.ExecuteNonQuerySP("STR_PORDER_TENDER_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.AddQuotationTender-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int UpdateTenderPO(Str_Purchase_Order objPO, string colcode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[36];
                        objParams[0] = new SqlParameter("@P_PORDNO ", objPO.PORDNO);
                        objParams[1] = new SqlParameter("@P_TENDERNO", objPO.TENDERNO);
                        objParams[2] = new SqlParameter("@P_REFNO", objPO.REFNO);
                        objParams[3] = new SqlParameter("@P_MDNO", objPO.MDNO);
                        objParams[4] = new SqlParameter("@P_PNO  ", objPO.PNO);
                        objParams[5] = new SqlParameter("@P_FOOTER1 ", objPO.FOOTER1);
                        objParams[6] = new SqlParameter("@P_TERM1 ", objPO.TERM1);
                        objParams[7] = new SqlParameter("@P_SDATE", objPO.SDATE);
                        objParams[8] = new SqlParameter("@P_TDATE ", objPO.TDATE);
                        objParams[9] = new SqlParameter("@P_SUBJECT", objPO.SUBJECT);
                        objParams[10] = new SqlParameter("@P_FLAG", objPO.FLAG);
                        objParams[11] = new SqlParameter("@P_FLAGINV", objPO.FLAGINV);
                        objParams[12] = new SqlParameter("@P_SDPER", objPO.SDPER);
                        objParams[13] = new SqlParameter("@P_ENCL", objPO.ENCL);
                        objParams[14] = new SqlParameter("@P_COPYTO", objPO.COPYTO);
                        objParams[15] = new SqlParameter("@P_HA", objPO.HA);
                        objParams[16] = new SqlParameter("@P_SIGN", objPO.SIGN);
                        objParams[17] = new SqlParameter("@P_REMARK", objPO.REMARK);
                        objParams[18] = new SqlParameter("@P_TECHCLAR", objPO.TECHCLAR);
                        objParams[19] = new SqlParameter("@P_CIFCHARGE", objPO.CIFCHARGE);
                        objParams[20] = new SqlParameter("@P_CIFCHARGETEXT", objPO.CIFCHARGETEXT);
                        objParams[21] = new SqlParameter("@P_RELISHED", objPO.RELISHED);
                        objParams[22] = new SqlParameter("@P_AGREEMENT", objPO.AGREEMENT);
                        objParams[23] = new SqlParameter("@P_PENDING", objPO.PENDING);
                        objParams[24] = new SqlParameter("@P_DEL", objPO.DEL);
                        objParams[25] = new SqlParameter("@P_RELIES", objPO.RELIES);
                        objParams[26] = new SqlParameter("@P_CST", objPO.CST);
                        objParams[27] = new SqlParameter("@P_ED", objPO.ED);
                        objParams[28] = new SqlParameter("@P_COLLEGE_CODE", colcode);
                        objParams[29] = new SqlParameter("@P_ISTYPE", objPO.ISTYPE);
                        objParams[30] = new SqlParameter("@P_AMENDNO", objPO.AMENDNO);
                        objParams[31] = new SqlParameter("@P_BANKGTY", objPO.BANKGTY);
                        objParams[32] = new SqlParameter("@P_AMOUNT", objPO.AMOUNT);
                        objParams[33] = new SqlParameter("@P_BANK_REMARK", objPO.BANK_REMARK);
                        objParams[34] = new SqlParameter("@P_NatureOfWork", objPO.NatureOfWork);  //----16-03-2023
                        objParams[35] = new SqlParameter("@P_OurReferenceNo", objPO.OurReferenceNo); //----16-03-2023

                        if (objSQLHelper.ExecuteNonQuerySP("STR_PORDER_TENDER_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.AddQuotationTender-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetVendorDetails(int pno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PNO", pno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTY_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Purchase_Order_Controller_GetItemsRecommandToVendor()-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetDPOItemDetails(int indno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_INDENTNO", indno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_ITEMS_GETBYINDENTNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Purchase_Order_Controller_GetItemsRecommandToVendor()-> " + ex.ToString());
                    }
                    return ds;
                }
                public int SaveDPOPO(Str_Purchase_Order objPO, string colcode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[38];
                        objParams[0] = new SqlParameter("@P_PORDNO ", SqlDbType.Int);
                        objParams[0].Direction = ParameterDirection.Output;
                        objParams[1] = new SqlParameter("@P_INDENTNO", objPO.INDENTNO);
                        objParams[2] = new SqlParameter("@P_REFNO", objPO.REFNO);
                        objParams[3] = new SqlParameter("@P_MDNO", objPO.MDNO);
                        objParams[4] = new SqlParameter("@P_PNO  ", objPO.PNO);
                        objParams[5] = new SqlParameter("@P_PINO", objPO.PINO);
                        objParams[6] = new SqlParameter("@P_FOOTER1 ", objPO.FOOTER1);
                        objParams[7] = new SqlParameter("@P_TERM1 ", objPO.TERM1);
                        objParams[8] = new SqlParameter("@P_SDATE", objPO.SDATE);
                        objParams[9] = new SqlParameter("@P_TDATE ", objPO.TDATE);
                        objParams[10] = new SqlParameter("@P_SUBJECT", objPO.SUBJECT);
                        objParams[11] = new SqlParameter("@P_FLAG", objPO.FLAG);
                        objParams[12] = new SqlParameter("@P_FLAGINV", objPO.FLAGINV);
                        objParams[13] = new SqlParameter("@P_SDPER", objPO.SDPER);
                        objParams[14] = new SqlParameter("@P_ENCL", objPO.ENCL);
                        objParams[15] = new SqlParameter("@P_COPYTO", objPO.COPYTO);
                        objParams[16] = new SqlParameter("@P_HA", objPO.HA);
                        objParams[17] = new SqlParameter("@P_SIGN", objPO.SIGN);
                        objParams[18] = new SqlParameter("@P_REMARK", objPO.REMARK);
                        objParams[19] = new SqlParameter("@P_TECHCLAR", objPO.TECHCLAR);
                        objParams[20] = new SqlParameter("@P_CIFCHARGE", objPO.CIFCHARGE);
                        objParams[21] = new SqlParameter("@P_CIFCHARGETEXT", objPO.CIFCHARGETEXT);
                        objParams[22] = new SqlParameter("@P_RELISHED", objPO.RELISHED);
                        objParams[23] = new SqlParameter("@P_AGREEMENT", objPO.AGREEMENT);
                        objParams[24] = new SqlParameter("@P_PENDING", objPO.PENDING);
                        objParams[25] = new SqlParameter("@P_DEL", objPO.DEL);
                        objParams[26] = new SqlParameter("@P_RELIES", objPO.RELIES);
                        objParams[27] = new SqlParameter("@P_CST", objPO.CST);
                        objParams[28] = new SqlParameter("@P_ED", objPO.ED);
                        objParams[29] = new SqlParameter("@P_ISTYPE", objPO.ISTYPE);
                        objParams[30] = new SqlParameter("@P_COLLEGE_CODE", colcode);
                        objParams[31] = new SqlParameter("@P_BANKGTY", objPO.BANKGTY);
                        objParams[32] = new SqlParameter("@P_AMOUNT", objPO.AMOUNT);
                        objParams[33] = new SqlParameter("@P_BANK_REMARK", objPO.BANK_REMARK);

                        objParams[34] = new SqlParameter("@P_DPO_ITEM_TBL", objPO.DPO_ITEM_TBL);
                        objParams[35] = new SqlParameter("@P_DPO_TAX_TBL", objPO.DPO_TAX_TBL);
                        objParams[36] = new SqlParameter("@P_NatureOfWork", objPO.NatureOfWork);  //----16-03-2023
                        objParams[37] = new SqlParameter("@P_OurReferenceNo", objPO.OurReferenceNo); //----16-03-2023
                        
                        if (objSQLHelper.ExecuteNonQuerySP("STR_PORDER_DPO_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.AddQuotationTender-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int UpdateDPOPO(Str_Purchase_Order objPO, string colcode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[38];
                        objParams[0] = new SqlParameter("@P_PORDNO ", objPO.PORDNO);
                        objParams[1] = new SqlParameter("@P_INDENTNO", objPO.INDENTNO);
                        objParams[2] = new SqlParameter("@P_REFNO", objPO.REFNO);
                        objParams[3] = new SqlParameter("@P_MDNO", objPO.MDNO);
                        objParams[4] = new SqlParameter("@P_PNO  ", objPO.PNO);
                        objParams[5] = new SqlParameter("@P_FOOTER1 ", objPO.FOOTER1);
                        objParams[6] = new SqlParameter("@P_TERM1 ", objPO.TERM1);
                        objParams[7] = new SqlParameter("@P_SDATE", objPO.SDATE);
                        objParams[8] = new SqlParameter("@P_TDATE ", objPO.TDATE);
                        objParams[9] = new SqlParameter("@P_SUBJECT", objPO.SUBJECT);
                        objParams[10] = new SqlParameter("@P_FLAG", objPO.FLAG);
                        objParams[11] = new SqlParameter("@P_FLAGINV", objPO.FLAGINV);
                        objParams[12] = new SqlParameter("@P_SDPER", objPO.SDPER);
                        objParams[13] = new SqlParameter("@P_ENCL", objPO.ENCL);
                        objParams[14] = new SqlParameter("@P_COPYTO", objPO.COPYTO);
                        objParams[15] = new SqlParameter("@P_HA", objPO.HA);
                        objParams[16] = new SqlParameter("@P_SIGN", objPO.SIGN);
                        objParams[17] = new SqlParameter("@P_REMARK", objPO.REMARK);
                        objParams[18] = new SqlParameter("@P_TECHCLAR", objPO.TECHCLAR);
                        objParams[19] = new SqlParameter("@P_CIFCHARGE", objPO.CIFCHARGE);
                        objParams[20] = new SqlParameter("@P_CIFCHARGETEXT", objPO.CIFCHARGETEXT);
                        objParams[21] = new SqlParameter("@P_RELISHED", objPO.RELISHED);
                        objParams[22] = new SqlParameter("@P_AGREEMENT", objPO.AGREEMENT);
                        objParams[23] = new SqlParameter("@P_PENDING", objPO.PENDING);
                        objParams[24] = new SqlParameter("@P_DEL", objPO.DEL);
                        objParams[25] = new SqlParameter("@P_RELIES", objPO.RELIES);
                        objParams[26] = new SqlParameter("@P_CST", objPO.CST);
                        objParams[27] = new SqlParameter("@P_ED", objPO.ED);
                        objParams[28] = new SqlParameter("@P_COLLEGE_CODE", colcode);
                        objParams[29] = new SqlParameter("@P_ISTYPE", objPO.ISTYPE);
                        objParams[30] = new SqlParameter("@P_AMENDNO", objPO.AMENDNO);
                        objParams[31] = new SqlParameter("@P_BANKGTY", objPO.BANKGTY);
                        objParams[32] = new SqlParameter("@P_AMOUNT", objPO.AMOUNT);
                        objParams[33] = new SqlParameter("@P_BANK_REMARK", objPO.BANK_REMARK);

                        objParams[34] = new SqlParameter("@P_DPO_ITEM_TBL", objPO.DPO_ITEM_TBL);
                        objParams[35] = new SqlParameter("@P_DPO_TAX_TBL", objPO.DPO_TAX_TBL);
                        objParams[36] = new SqlParameter("@P_NatureOfWork", objPO.NatureOfWork);  //----16-03-2023
                        objParams[37] = new SqlParameter("@P_OurReferenceNo", objPO.OurReferenceNo); //----16-03-2023
                        if (objSQLHelper.ExecuteNonQuerySP("STR_PORDER_DPO_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);


                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.AddQuotationTender-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetApprovalInfo(int pono)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_NO", pono);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_GET_POAPPROVAL_INFO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Purchase_Order_Controller_GetApprovalInfo()-> " + ex.ToString());
                    }
                    return ds;
                }

                public int UpdatePOApprovalInfo(int PONO, string strPOAppInfo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_NO", PONO);
                        objParams[1] = new SqlParameter("@PO_APPROVAL_INFO", strPOAppInfo);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STUD_UPDATE_POAPPROVAL_INFO", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PurchaseComiteeController.UpdatePOApprovalInfo-> " + ex.ToString());
                    }
                    return retStatus;

                }
                public int AddUpdatePOApprovalAuthority(int APP_NO, int PONO, int SR_NO, int UA_NO, int COLLEGE_CODE)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_APP_NO", APP_NO);
                        objParams[1] = new SqlParameter("@P_PNO", PONO);
                        objParams[2] = new SqlParameter("@P_SR_NO", SR_NO);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", COLLEGE_CODE);
                        objParams[4] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;
                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_PURCHASEORDER_APPROVAL_INSERT_UPDATE", objParams, true).ToString());
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PurchaseComiteeController.AddUpdatePurchaseComitee-> " + ex.ToString());
                    }
                    return retStatus;

                }
                public int DeleteExistingEntry(int pordno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PORDNO ", pordno);
                        if (objSQLHelper.ExecuteNonQuerySP("STR_PORDER_DELETE_EXISTING_ENTRY", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PurchaseComiteeController.UpdatePOLock-> " + ex.ToString());
                    }
                    return retStatus;
                }


              // It is used to get the PO approval tracking.
                public DataSet GetPOApprovalTrackDetails(int PORDNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PORDNO", PORDNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_PO_TRACK_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PurchaseComiteeController.GetPOApprovalTrackDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetTaxesForDPO(decimal TaxableAmt, decimal BasicAmt, int ItemNo, int IsState)
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
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_DPO_GET_TAXES", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StrGRNCon.GetItemsByPO-> " + ex.ToString());
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
            }
        }
    }
}