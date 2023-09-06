using System;
using System.Data;
using System.Web;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

using System.Data.SqlClient;
using System.Data.SqlTypes;
using IITMS.SQLServer.SQLDAL;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class Str_SerialNumberGenController
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                #region MAIN_ITEM_GROUP
                public DataSet GetAllItemserial(int Item_no, int Type,Boolean Autoincrement)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ITEM_NO", Item_no);
                        objParams[1] = new SqlParameter("@P_TYPE", Type);
                        objParams[2] = new SqlParameter("@P_Autoincrement", Autoincrement);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STORE_AUTO_GENRATE_SERIAL_NUMER_ITEMS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_SerialNumberGenController.GetAllItemserial-> " + ex.ToString());
                    }
                    return ds;
                }

              


                public int Add_Update_SerialNumber(Str_SerialNumber objLM, int TRANNO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;


                        objParams = new SqlParameter[7];

                        //objParams[0] = new SqlParameter("@P_ESTB_LEAVE_OPENING_RECORD", dtAppRecord);

                        objParams[0] = new SqlParameter("@P_INVDINO", objLM.INVDINO);
                        objParams[1] = new SqlParameter("@P_DSR_Number", objLM.serialNumber);
                        objParams[2] = new SqlParameter("@P_TECH_SPEC", objLM.TECH_SPEC);
                        objParams[3] = new SqlParameter("@P_QUALITY_QTY_SPEC", objLM.QUALITY_QTY_SPEC);
                        objParams[4] = new SqlParameter("@P_ITEM_REMARK", objLM.ITEM_REMARK);

                        if (!objLM.DES_Date.Equals(null))
                            objParams[5] = new SqlParameter("@P_DES_Date", objLM.DES_Date);
                        else
                            objParams[5] = new SqlParameter("@P_DES_Date", DBNull.Value);

                        objParams[6] = new SqlParameter("@P_TRANNO", TRANNO);


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STORE_DSR_NUMBER_INFO_INSERT", objParams, true);

                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_SerialNumberGenController.Add_Update_SerialNumber -> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateAutoDsrNumber(int ITEM_NO)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[2];
                        objparams[0] = new SqlParameter("@P_ITEM_NO", ITEM_NO);
                        objparams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[1].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STR_GRN_UPDATE_DSR_NUMBER", objparams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_SerialNumberGenController.UpdateAutoDsrNumber->" + ex.ToString());
                    }
                    return retStatus;

                }

                #endregion


                //-----------start--Shaikh Juned 27-10-2022-Dead Stock Insert Method

                public int AddDeadStockEntry(Str_SerialNumber objLM, int orgId, int createdBy, DateTime CreatedDate, DateTime TranDate, int DSTKID, int modifyby)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[10];
                        //  objparams[0] = new SqlParameter("@P_ISSUE_DTAE", objLM.ISSUE_DATE);
                        if (objLM.ISSUE_DATE == DateTime.MinValue)
                        {
                            objparams[0] = new SqlParameter("@P_ISSUE_DTAE", DBNull.Value);
                        }
                        else
                        {
                            objparams[0] = new SqlParameter("@P_ISSUE_DTAE", objLM.ISSUE_DATE);
                        }
                        objparams[1] = new SqlParameter("@P_REMARK", objLM.REMARK);
                        objparams[2] = new SqlParameter("@P_DEAD_STOCK_ITEM_TBL", objLM.DEAD_STOCK_ITEM_TBL);
                        objparams[3] = new SqlParameter("@P_OrganizationId", orgId);
                        objparams[4] = new SqlParameter("@P_CreatedBy", createdBy);
                        objparams[5] = new SqlParameter("@P_CreatedDate", CreatedDate);
                        objparams[6] = new SqlParameter("@P_TranDate", TranDate);
                        objparams[7] = new SqlParameter("@P_DSTKID", DSTKID);
                        if (modifyby > 0)
                        {
                            objparams[8] = new SqlParameter("@P_MODIFIED_BY", modifyby);
                        }
                        else
                        {
                            objparams[8] = new SqlParameter("@P_MODIFIED_BY", DBNull.Value);
                        }
                        objparams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[9].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STORE_INS_DEAD_STOCK_ENTRY", objparams, true);

                        //if (Convert.ToInt32(ret) == -99)
                        //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        //else
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_SerialNumberGenController.UpdateAutoDsrNumber->" + ex.ToString());
                    }
                    return retStatus;

                }

                //----------end-----Shaikh Juned 27-10-2022--

                //-----------start--Shaikh Juned 27-10-2022-Dead Stock Update Method

                public int UpdDeadStockEntry(Str_SerialNumber objLM, int orgId, int createdBy, DateTime CreatedDate, int ModifyBy, DateTime TranDate, int DSTKID)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objparams = null;
                        objparams = new SqlParameter[10];
                        //  objparams[0] = new SqlParameter("@P_ISSUE_DTAE", objLM.ISSUE_DATE);
                        if (objLM.ISSUE_DATE == DateTime.MinValue)
                        {
                            objparams[0] = new SqlParameter("@P_ISSUE_DTAE", DBNull.Value);
                        }
                        else
                        {
                            objparams[0] = new SqlParameter("@P_ISSUE_DTAE", objLM.ISSUE_DATE);
                        }
                        objparams[1] = new SqlParameter("@P_REMARK", objLM.REMARK);
                        objparams[2] = new SqlParameter("@P_DEAD_STOCK_ITEM_TBL", objLM.DEAD_STOCK_ITEM_TBL);
                        objparams[3] = new SqlParameter("@P_OrganizationId", orgId);
                        if (createdBy > 0)
                        {
                            objparams[4] = new SqlParameter("@P_CreatedBy", createdBy);
                        }
                        else
                        {
                            objparams[4] = new SqlParameter("@P_CreatedBy", DBNull.Value);
                        }
                        if (CreatedDate == DateTime.MinValue)
                        {
                            objparams[5] = new SqlParameter("@P_CreatedDate", DBNull.Value);
                        }
                        else
                        {
                            objparams[5] = new SqlParameter("@P_CreatedDate", objLM.ISSUE_DATE);
                        }
                        objparams[6] = new SqlParameter("@P_TranDate", TranDate);
                        objparams[7] = new SqlParameter("@P_DSTKID", DSTKID);
                        objparams[8] = new SqlParameter("@P_MODIFIED_BY", ModifyBy);
                        objparams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objparams[9].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STORE_INS_DEAD_STOCK_ENTRY", objparams, true);

                        //if (Convert.ToInt32(ret) == -99)
                        //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        //else
                        //    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Str_SerialNumberGenController.UpdateAutoDsrNumber->" + ex.ToString());
                    }
                    return retStatus;

                }

                //----------end-----Shaikh Juned 27-10-2022--
            }
        }
    }
}
