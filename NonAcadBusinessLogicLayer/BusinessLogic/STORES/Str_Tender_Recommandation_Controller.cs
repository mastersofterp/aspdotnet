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
        namespace BusinessLayer.BusinessLogic
        {
            public class Str_Tender_Recommandation_Controller
            {
                Common ObjComman = new Common();
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetVendorByTender(int tenderno )
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TENDERNO", tenderno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDER_PARTYFORREC_GETALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Recommandaion_Controller_GetVendorByQuotation-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetItemsByTendorNo(int tenderno, int tvno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_TENDERNO", tenderno);
                        objParams[1] = new SqlParameter("@P_TVNO", tvno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDER_ITEMS_FOR_RECOMMAND", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetItemsByQuotNo-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetTenderByDepartment(int Mdno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_MDNO", Mdno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDOR_GET_BY_MDNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Recommandaion_Controller__GetQuotationByDepartment()-> " + ex.ToString());
                    }
                    return ds;
                }
                public int SaveReccomanforParty(int tenderno, int tvno, int tino, string colcode,int ItemNo,int ItemQty)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[7];                       
                        objParams[0] = new SqlParameter("@P_TENDERNO", tenderno);
                        objParams[1] = new SqlParameter("@P_TINO", tino);
                        objParams[2] = new SqlParameter("@P_TVNO", tvno);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", colcode);
                        objParams[4] = new SqlParameter("@P_ITEM_NO", ItemNo);
                        objParams[5] = new SqlParameter("@P_ITEMQTY", ItemQty);
                        objParams[6] = new SqlParameter("@P_RECNO", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;
                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_TENDER_RECCOMAND_INSERT", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Recommandaion_Controller.SaveRecommand-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetAlreadyRecommandItemsForParty(int tenderno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TENDERNO", tenderno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDER_ITEMS_ALREADY_RECOMMAND", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetItemsByQuotNo-> " + ex.ToString());
                    }
                    return ds;
                }
                public int DeleteRecommandation(int Tenderno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New BUDGETHEAD
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_TENDERNO", Tenderno);


                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_TENDER_RECCOMAND_DELETE", objParams, true));

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Recommandaion_Controller.DeleteRecommandation-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public bool CheckForPoLock(string Quotno, int pno)
                {
                    bool isLock = false;
                    string checkLock = ObjComman.LookUp("STORE_PORDER", "FINAL", "QUOTNO='" + Quotno + "' AND PNO=" + pno);
                    if (checkLock.Equals("1"))
                    {
                        isLock = true;
                    }
                    return isLock;
                }
                public DataSet GetVendorTenderEntryForParty(int Tenderno, int TVNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_TENDERNO", Tenderno);
                        objParams[1] = new SqlParameter("@P_TVNO", TVNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDER_PARTYITEMS_BY_TENDERNO_AND_PARTY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetVendorQuotationEntryForParty-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetVendorTenderFieldEntry(int Tenderno, int TVNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_TENDERNO", Tenderno);
                        objParams[1] = new SqlParameter("@P_TVNO", TVNO);
                        objParams[2] = new SqlParameter("@P_FNO", "");
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDER_PARTYFIELDENTRY_BY_FIELDNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetVendorQuotationEntryForParty-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetAllReccomand()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDER_ALL_RECCOMAND", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetDeptRequisions-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetVendorTenderFieldEntryForParty(string Tenderno, int Tvno, int itemno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_TENDERNO", Tenderno);
                        objParams[1] = new SqlParameter("@P_TVNO", Tvno);                       
                        objParams[2] = new SqlParameter("@P_ITEM_NO", itemno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_TENDER_PARTYFIELDENTRY_TAX_AMOUNT_BY_FIELDNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetVendorTenderFieldEntryForParty-> " + ex.ToString());
                    }
                    return ds;
                }
                //
            }
        }
    }
}
