using System;
using System.Data;
using System.Web;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;
using System.Data.SqlTypes;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class VendorController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>

                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                #region Pary_Category

                public int AddParyCategory(string categoryName, string collegeCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New ITEM_MASTER
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_PARTY_CATEGORY_NAME", categoryName);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[2] = new SqlParameter("@P_PARTY_CATEGORY_NO", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_PARTY_CATEGORY_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.VendorController.AddParyCategory-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateParyCategory(string categoryName, string collegeCode, int categoryNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New ITEM_MASTER
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_PARTY_CATEGORY_NO", categoryNo);
                        objParams[1] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[2] = new SqlParameter("@P_PARTY_CATEGORY_NAME", categoryName);
                       
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_PARTY_CATEGORY", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.VendorController.UpdateParyCategory-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllParyCategory()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTY_CATEGORY_GET_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.VendorController.GetAllParyCategory-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSingleRecordParyCategory(int categoryNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PARTY_CATEGORY_NO", categoryNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTY_CATEGORY_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.VendorController.GetSingleRecordParyCategory-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #region Party

                public int AddParty(Vendor objVendor)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New ITEM_MASTER
                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_PARTY_CODE", objVendor.PARTY_CODE);
                        objParams[1] = new SqlParameter("@P_PARTY_NAME", objVendor.PARTY_NAME);
                        objParams[2] = new SqlParameter("@P_PARTY_CATEGORY_NO", objVendor.PARTY_CATEGORY_NO);
                        objParams[3] = new SqlParameter("@P_ADDRESS", objVendor.ADDRESS);
                        objParams[4] = new SqlParameter("@P_CITYNO", objVendor.CITYNO);
                        objParams[5] = new SqlParameter("@P_STATENO", objVendor.STATENO);
                        objParams[6] = new SqlParameter("@P_PHONE", objVendor.PHONE);
                        objParams[7] = new SqlParameter("@P_EMAIL", objVendor.EMAIL);
                        objParams[8] = new SqlParameter("@P_CST", objVendor.CST);
                        objParams[9] = new SqlParameter("@P_BST", objVendor.BST);
                        objParams[10] = new SqlParameter("@P_TNO", objVendor.TNO);
                        objParams[11] = new SqlParameter("@P_OB", objVendor.OB);
                        objParams[12] = new SqlParameter("@P_REMARK", objVendor.REMARK);                        
                        objParams[13] = new SqlParameter("@P_COLLEGE_CODE", objVendor.COLLEGE_CODE);
                        objParams[14] = new SqlParameter("@P_STR_PARTY_NO", SqlDbType.Int);
                        objParams[14].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_PARTY_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.VendorController.AddParty-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateParty(Vendor objVendor)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Update Party                       
                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_STR_PARTY_NO",objVendor.STR_PARTY_NO);
                        objParams[1] = new SqlParameter("@P_PARTY_CODE", objVendor.PARTY_CODE);
                        objParams[2] = new SqlParameter("@P_PARTY_NAME", objVendor.PARTY_NAME);
                        objParams[3] = new SqlParameter("@P_PARTY_CATEGORY_NO", objVendor.PARTY_CATEGORY_NO);
                        objParams[4] = new SqlParameter("@P_ADDRESS", objVendor.ADDRESS);
                        objParams[5] = new SqlParameter("@P_CITYNO", objVendor.CITYNO);
                        objParams[6] = new SqlParameter("@P_STATENO", objVendor.STATENO);
                        objParams[7] = new SqlParameter("@P_PHONE", objVendor.PHONE);
                        objParams[8] = new SqlParameter("@P_EMAIL", objVendor.EMAIL);
                        objParams[9] = new SqlParameter("@P_CST", objVendor.CST);
                        objParams[10] = new SqlParameter("@P_BST", objVendor.BST);
                        objParams[11] = new SqlParameter("@P_TNO", objVendor.TNO);
                        objParams[12] = new SqlParameter("@P_OB", objVendor.OB);
                        objParams[13] = new SqlParameter("@P_REMARK", objVendor.REMARK);
                        objParams[14] = new SqlParameter("@P_COLLEGE_CODE", objVendor.COLLEGE_CODE);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_PARTY_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.VendorController.UpdateParty-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllParty()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTY_GET_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.VendorController.GetAllParty-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSingleRecordParty(int partyNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_STR_PARTY_NO", partyNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_PARTY_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.VendorController.GetSingleRecordParty-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

            }

        }
    }
}
