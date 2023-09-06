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
            public class StrGRNCon
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetTaxes(decimal TaxableAmt,decimal BasicAmt,int ItemNo,int IsState)
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
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GRN_GET_TAXES", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StrGRNCon.GetItemsByPO-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetItemsByPO(string Ponumbers)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PORDNO", Ponumbers);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GRN_GET_ITEM_BY_PO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StrGRNCon.GetItemsByPO-> " + ex.ToString());
                    }
                    return ds;
                }

                public int InsUpdateGRNEntry(StrGRNEnt objGRNEnt)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[16];
                        objParams[0] = new SqlParameter("@P_GRNID", objGRNEnt.GRNID);
                        objParams[1] = new SqlParameter("@P_SPID", objGRNEnt.SPID);
                        if (objGRNEnt.SPDATE == DateTime.MinValue)
                        {
                            objParams[2] = new SqlParameter("@P_SPDATE", DBNull.Value);
                        }
                        else
                        {
                            objParams[2] = new SqlParameter("@P_SPDATE", objGRNEnt.SPDATE);
                        }
                        objParams[3] = new SqlParameter("@P_GRNDATE  ", objGRNEnt.GRNDATE);
                        objParams[4] = new SqlParameter("@P_GRN_NUMBER", objGRNEnt.GRN_NUMBER);                       
                        objParams[5] = new SqlParameter("@P_PNO ", objGRNEnt.PNO);
                        objParams[6] = new SqlParameter("@P_MDNO ", objGRNEnt.MDNO);  
                        objParams[7] = new SqlParameter("@P_PORDNO", objGRNEnt.PORDNO);
                        objParams[8] = new SqlParameter("@P_REMARK", objGRNEnt.REMARK);
                        objParams[9] = new SqlParameter("@P_DMDATE", objGRNEnt.DMDATE);
                        objParams[10] = new SqlParameter("@P_DMNO", objGRNEnt.DMNO);
                        objParams[11] = new SqlParameter("@P_GRN_ITEM_TBL", objGRNEnt.GRN_ITEM_TBL);
                        objParams[12] = new SqlParameter("@P_GRN_TAX_TBL", objGRNEnt.GRN_TAX_TBL);
                        objParams[13] = new SqlParameter("@P_CREATED_BY", objGRNEnt.CREATED_BY);
                        objParams[14] = new SqlParameter("@P_MODIFIED_BY", objGRNEnt.MODIFIED_BY);
                        objParams[15] = new SqlParameter("@P_OUT ", SqlDbType.Int);
                        objParams[15].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_GRN_INS_UPD", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StrGRNCon.InsUpdateGRNEntry-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetGRNNumber()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GRN_GENERATE_GRN_NUMBER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StrGRNCon.GetSecPassNumber-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetItemsBySPID(int SPID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SPID", SPID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GRN_GET_ITEM_BY_SPID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StrGRNCon.GetSecPassNumber-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetGRNEntryDetailsForEdit(int GRNID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_GRNID", GRNID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GRN_GET_DETAILS_FOR_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StrGRNCon.GetGRNEntryDetailsForEdit-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetPOListOnGRN()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STORE_GET_PO_LIST_FOR_GRN", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StrGRNCon.GetPOListOnGRN-> " + ex.ToString());
                    }
                    return ds;
                }

                //Modified by Shabina 
                //Date 28-09-2021

                public DataSet GetddlSPlist()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STORE_GET_SPID_LIST_FOR_GRN", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StrGRNCon.GetddlSPlist-> " + ex.ToString());
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
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STORE_GET_PO_LIST_FOR_GRN_ENTRY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StrGRNCon.GetPODropdown-> " + ex.ToString());
                    }
                    return ds;
                }

            }
        }
    }
}
