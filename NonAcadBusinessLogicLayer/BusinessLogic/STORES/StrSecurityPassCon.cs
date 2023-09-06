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
            public class StrSecurityPassCon
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public int InsUpdateSecurityPass(StrSecurityPassEnt objSec)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[17];
                        objParams[0] = new SqlParameter("@P_SPID", objSec.SPID);
                        objParams[1] = new SqlParameter("@P_SPDATE", objSec.SPDATE);
                        objParams[2] = new SqlParameter("@P_SP_NUMBER", objSec.SP_NUMBER);
                        objParams[3] = new SqlParameter("@P_TIME", objSec.TIME);
                        objParams[4] = new SqlParameter("@P_VEHICLE_NO", objSec.VEHICLE_NO);
                        objParams[5] = new SqlParameter("@P_DMDATE  ", objSec.DMDATE);
                        objParams[6] = new SqlParameter("@P_DMNO", objSec.DMNO);
                        objParams[7] = new SqlParameter("@P_PNO ", objSec.PNO);
                        objParams[8] = new SqlParameter("@P_GATE_KEEPER ", objSec.GATE_KEEPER);
                        objParams[9] = new SqlParameter("@P_INCHARGE", objSec.INCHARGE);
                        objParams[10] = new SqlParameter("@P_PORDNO", objSec.PORDNO);
                        objParams[11] = new SqlParameter("@P_REMARK", objSec.REMARK);
                        objParams[12] = new SqlParameter("@P_SEC_PASS_ITEM_TBL", objSec.SEC_PASS_ITEM_TBL);
                        objParams[13] = new SqlParameter("@P_CREATED_BY", objSec.CREATED_BY);
                        objParams[14] = new SqlParameter("@P_MODIFIED_BY", objSec.MODIFIED_BY);
                        objParams[15] = new SqlParameter("@P_MDNO", objSec.MDNO);
                        objParams[16] = new SqlParameter("@P_OUT ", SqlDbType.Int);
                        objParams[16].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_SEC_PASS_INS_UPD", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StrSecurityPassCon.AddQuotationTender-> " + ex.ToString());
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
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_SEC_GET_ITEM_BY_PO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StrSecurityPassCon.GetItemsByPO-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetSecPassNumber()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_SEC_GENERATE_SP_NUMBER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StrSecurityPassCon.GetSecPassNumber-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet GetSecPassDetailsForEdit(int SpId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SPID", SpId);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_SEC_GET_DETAILS_FOR_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StrSecurityPassCon.GetSecPassNumber-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetPOList()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STORE_GET_PO_LIST_FOR_GATE_PASS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StrSecurityPassCon.GetPOList-> " + ex.ToString());
                    }
                    return ds;
                }

                #region Sec Pass Outward
                public DataSet GenerateOWRegSlipNo()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GENERATE_OW_REG_SLIP_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StrSecurityPassCon.GenerateOWRegSlipNo-> " + ex.ToString());
                    }
                    return ds;
                }
                public int InsUpdateSecPassOutward(StrSecurityPassEnt objSec)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[13];
                        objParams[0] = new SqlParameter("@P_SP_OW_ID", objSec.SP_OW_ID);
                        objParams[1] = new SqlParameter("@P_REG_SLIP_NO", objSec.REG_SLIP_NO);
                        objParams[2] = new SqlParameter("@P_IR_ID", objSec.GP_NUMBER);
                        objParams[3] = new SqlParameter("@P_VEHICLE_NO", objSec.VEHICLE_NO);
                        objParams[4] = new SqlParameter("@P_OUT_DATE", objSec.OUT_DATE);
                        objParams[5] = new SqlParameter("@P_OUT_TIME", objSec.OUT_TIME);
                        if (!objSec.RECEIVED_DATE.Equals(DateTime.MinValue))
                            objParams[6] = new SqlParameter("@P_RECEIVED_DATE", objSec.RECEIVED_DATE);
                        else
                            objParams[6] = new SqlParameter("@P_RECEIVED_DATE", DBNull.Value);
                        //if (!objSec.RECEIVED_TIME.Equals(DateTime.MinValue))
                        //    objParams[7] = new SqlParameter("@P_RECEIVED_TIME", objSec.RECEIVED_TIME);
                        //else
                        objParams[7] = new SqlParameter("@P_RECEIVED_TIME", objSec.RECEIVED_TIME);
                        objParams[8] = new SqlParameter("@P_IRTRANID", objSec.IRTRANID);
                        objParams[9] = new SqlParameter("@P_CREATED_BY", objSec.CREATED_BY);
                        objParams[10] = new SqlParameter("@P_MODIFIED_BY", objSec.MODIFIED_BY);
                        objParams[11] = new SqlParameter("@P_TRAN_TYPE", objSec.TRAN_TYPE);

                        objParams[12] = new SqlParameter("@P_OUT ", SqlDbType.Int);
                        objParams[12].Direction = ParameterDirection.Output;
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_SEC_PASS_OW_INS_UPD", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StrSecurityPassCon.InsUpdateSecPassOutward-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetItemRepairDetails(int SP_OW_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SP_OW_ID", SP_OW_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_SEC_GET_OW_ENTRY_DETAILS_FOR_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StrSecurityPassCon.GetItemRepairDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetItemListByGPNum(int GP_NUMBER)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_GP_NUMBER", GP_NUMBER);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_GET_ITEMS_BY_GP_NUMBER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StrSecurityPassCon.GetItemListByGPNum-> " + ex.ToString());
                    }
                    return ds;
                }


                //created by shabina
                //created date 24-09-2021
                public DataSet GetSecGPOutwardItemList()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[0];
                        //objParams[0] = new SqlParameter("@P_GP_NUMBER", GP_NUMBER);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_SEC_PASS_OW_RET_ITEMLIST", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StrSecurityPassCon.GetSecGPOutwardItemList-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion


              
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
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STORE_GET_PO_LIST_FOR_SEC_PASS_INWARD_ENTRY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StrSecurityPassCon.GetPODropdown-> " + ex.ToString());
                    }
                    return ds;
                }



                //Addedd by shabina
                public DataSet GetPODropdownForEdit(int Spid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SPID", Spid);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STORE_GET_PO_LIST_FOR_SEC_PASS_INWARD_ENTRY_EDIT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StrSecurityPassCon.GetPODropdown-> " + ex.ToString());
                    }
                    return ds;
                }





            }
        }
    }
}
