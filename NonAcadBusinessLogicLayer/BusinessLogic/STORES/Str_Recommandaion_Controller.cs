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
           public  class Str_Recommandaion_Controller
            {
               Common ObjComman = new Common();
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

                       ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTYENTRY_GETALL_REPORT", objParams);
                   }
                   catch (Exception ex)
                   {
                       return ds;
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Recommandaion_Controller_GetVendorByQuotation-> " + ex.ToString());
                   }
                   return ds;
               }
               public DataSet GetItemsByQuotNo(string quotno)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null; ;
                       objParams = new SqlParameter[1];
                       objParams[0] = new SqlParameter("@P_QUOTNO", quotno);
  
                       ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_ITEMS_FOR_RECOMMAND", objParams);
                   }
                   catch (Exception ex)
                   {
                       return ds;
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetItemsByQuotNo-> " + ex.ToString());
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

                       ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_QUOTATIONENTRY_GET_BY_MDNO", objParams);
                   }
                   catch (Exception ex)
                   {
                       return ds;
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Recommandaion_Controller__GetQuotationByDepartment()-> " + ex.ToString());
                   }
                   return ds;
               }

               public DataSet GetApprovedQuotation(int Mdno)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null; ;
                       objParams = new SqlParameter[1];
                       objParams[0] = new SqlParameter("@P_MDNO", Mdno);

                       ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_APPROVED_QUOTATION", objParams);
                   }
                   catch (Exception ex)
                   {
                       return ds;
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_vendor_Quotaton_Entry_Controller_GetQuotationForApproval()-> " + ex.ToString());
                   }
                   return ds;
               }

               public int SaveReccomanforParty(string Quotno, int Pno, int QuotItementryno, string colcode, int itemno, string Qty,string Justification)
               {
                   int retStatus = Convert.ToInt32(CustomStatus.Others);

                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null;
                       //Add New BUDGETHEAD
                       objParams = new SqlParameter[8];                      
                       objParams[0] = new SqlParameter("@P_QUOTNO", Quotno);
                       objParams[1] = new SqlParameter("@P_QINO", QuotItementryno);
                       objParams[2] = new SqlParameter("@P_PNO", Pno);
                       objParams[3] = new SqlParameter("@P_COLLEGE_CODE", colcode);
                       objParams[4] = new SqlParameter("@P_ITEM_NO", itemno);
                       objParams[5] = new SqlParameter("@P_ITEM_QTY", Qty);
                       objParams[6] = new SqlParameter("@P_JUSTIFICATION", Justification);
                       objParams[7] = new SqlParameter("@P_RECNO", SqlDbType.Int);
                       objParams[7].Direction = ParameterDirection.Output;

                       retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_ITEM_RECCOMAND_INSERT", objParams, true));

                   }
                   catch (Exception ex)
                   {
                       retStatus = Convert.ToInt32(CustomStatus.Error);
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Recommandaion_Controller.SaveRecommand-> " + ex.ToString());
                   }
                   return retStatus;
               }


               
               public int RecommendApprove(int qno, int Pno, string status,int userid)
               {
                   int retStatus = Convert.ToInt32(CustomStatus.Others);

                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null;                       
                       objParams = new SqlParameter[5];
                       objParams[0] = new SqlParameter("@P_QNO", qno);
                       objParams[1] = new SqlParameter("@P_PNO", Pno);                    
                       objParams[2] = new SqlParameter("@P_STATUS", status);
                       objParams[3] = new SqlParameter("@P_USERID", userid);
                       objParams[4] = new SqlParameter("@P_OUT", DbType.Int32);
                       objParams[4].Direction = ParameterDirection.Output;
                       retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_APPROVE_RECOMMEND", objParams, true));

                   }
                   catch (Exception ex)
                   {
                       retStatus = Convert.ToInt32(CustomStatus.Error);
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Recommandaion_Controller.SaveRecommand-> " + ex.ToString());
                   }
                   return retStatus;
               }

               public int RecommendApproveFund(int qno, int Pno, string status, int userid)
               {
                   int retStatus = Convert.ToInt32(CustomStatus.Others);

                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null;
                       objParams = new SqlParameter[5];
                       objParams[0] = new SqlParameter("@P_QNO", qno);
                       objParams[1] = new SqlParameter("@P_PNO", Pno);
                       objParams[2] = new SqlParameter("@P_STATUS", status);
                       objParams[3] = new SqlParameter("@P_USERID", userid);
                       objParams[4] = new SqlParameter("@P_OUT", DbType.Int32);
                       objParams[4].Direction = ParameterDirection.Output;
                       retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_APPROVE_RECOMMEND_FUND", objParams, true));

                   }
                   catch (Exception ex)
                   {
                       retStatus = Convert.ToInt32(CustomStatus.Error);
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Recommandaion_Controller.SaveRecommand-> " + ex.ToString());
                   }
                   return retStatus;
               }        
               public int SaveBudgetAllocation(int BANO,int BHALNO,int QNO, int Pno, int userid)
               {
                   int retStatus = Convert.ToInt32(CustomStatus.Others);

                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null;
                       objParams = new SqlParameter[6];
                       objParams[0] = new SqlParameter("@P_BANO", BANO);
                       objParams[1] = new SqlParameter("@P_QNO", QNO);
                       objParams[2] = new SqlParameter("@P_PNO", Pno);
                       objParams[3] = new SqlParameter("@P_BHALNO", BHALNO);
                       objParams[4] = new SqlParameter("@P_USERID", userid);
                       objParams[5] = new SqlParameter("@P_OUT", DbType.Int32);
                       objParams[5].Direction = ParameterDirection.Output;
                       retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_BUDGET_ALLOCATE_FINANCE", objParams, true));

                   }
                   catch (Exception ex)
                   {
                       retStatus = Convert.ToInt32(CustomStatus.Error);
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Recommandaion_Controller.SaveRecommand-> " + ex.ToString());
                   }
                   return retStatus;
               }
               public int DeleteRecommandation(int qino)
               {
                   int retStatus = Convert.ToInt32(CustomStatus.Others);

                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null;
                       //Add New BUDGETHEAD
                       objParams = new SqlParameter[1];
                       objParams[0] = new SqlParameter("@P_QINO", qino);


                       retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_ITEM_RECCOMAND_DELETE", objParams, true));

                   }
                   catch (Exception ex)
                   {
                       retStatus = Convert.ToInt32(CustomStatus.Error);
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Recommandaion_Controller.DeleteRecommandation-> " + ex.ToString());
                   }
                   return retStatus;
               }
               public DataSet GetAlreadyRecommandItemsForParty(string quotno,int pno)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null; ;
                       objParams = new SqlParameter[2];
                       objParams[0] = new SqlParameter("@P_QUOTNO", quotno);
                       objParams[1] = new SqlParameter("@P_PNO", pno );
                       ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_ITEMS_ALREADY_RECOMMAND", objParams);
                   }
                   catch (Exception ex)
                   {
                       return ds;
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetItemsByQuotNo-> " + ex.ToString());
                   }
                   return ds;
               }
             
               public bool CheckForPoLock(string Quotno, int pno)
               {
                   bool isLock = false;
                   string checkLock = ObjComman.LookUp("STORE_PORDER", "FINAL", "QUOTNO='"+Quotno +"' AND PNO="+pno );
                   if (checkLock.Equals("1"))
                   {
                       isLock= true;
                   }
                   return isLock;
               }
               public DataSet GetVendorQuotationEntryForParty(string quotno, int pno)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null; ;
                       objParams = new SqlParameter[2];
                       objParams[0] = new SqlParameter("@P_QUOTNO", quotno);
                       objParams[1] = new SqlParameter("@P_PNO", pno);
                       ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTYITEMS_BY_QUOTATION_NO_AND_PARTY", objParams);
                   }
                   catch (Exception ex)
                   {
                       return ds;
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetVendorQuotationEntryForParty-> " + ex.ToString());
                   }
                   return ds;
               }
               public DataSet GetVendorQuotationFieldEntryForParty(string quotno, int pno, int itemno)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = null; ;
                       objParams = new SqlParameter[4];
                       objParams[0] = new SqlParameter("@P_QUOTNO", quotno);
                       objParams[1] = new SqlParameter("@P_PNO", pno);
                       objParams[2] = new SqlParameter("@P_FNO", "");
                       objParams[3] = new SqlParameter("@P_ITEM_NO", itemno);
                       ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTYFIELDENTRY_TAX_AMOUNT_BY_FIELDNO", objParams);
                   }
                   catch (Exception ex)
                   {
                       return ds;
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_Quotation_Tender_Controller.GetVendorQuotationFieldEntryForParty-> " + ex.ToString());
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
                       ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_ALL_RECCOMAND", objParams);
                   }
                   catch (Exception ex)
                   {
                       return ds;
                       throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.STR_DEPT_REQ_CONTROLLER.GetDeptRequisions-> " + ex.ToString());
                   }
                   return ds;
               }
                //
            }
        }
    }
}