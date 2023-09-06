using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;


namespace IITMS
{
    namespace NITPRM
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class FieldController
            {

                /// <summary>
                /// ConnectionStrings
                /// </summary>

                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                #region STORE_news

                public int AddField(FieldMaster objFieldMaster)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New Field Master
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_FIELD_NAME", objFieldMaster.FIELD_NAME);
                        objParams[1] = new SqlParameter("@P_FIELD_TYPE", objFieldMaster.FIELD_TYPE);
                        objParams[2] = new SqlParameter("@P_SERIAL_NO", objFieldMaster.SERIAL_NO);
                        objParams[3] = new SqlParameter("@P_IND_FOR", objFieldMaster.IND_FOR);
                        objParams[4] = new SqlParameter("@P_CALC_ON_BASICAMT_YN", objFieldMaster.CALC_ON_BASICAMT_YN);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objFieldMaster.COLLEGE_CODE);
                        objParams[6] = new SqlParameter("@P_FIELD_NO", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_FIELDMASTER_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FieldController.AddNewsPaper-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateFiled(FieldMaster objFieldMaster)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Update Field Master
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_FIELD_NO",objFieldMaster.FIELD_NO);
                        objParams[1] = new SqlParameter("@P_FIELD_NAME",objFieldMaster.FIELD_NAME);
                        objParams[2] = new SqlParameter("@P_FIELD_TYPE",objFieldMaster.FIELD_TYPE);
                        objParams[3] = new SqlParameter("@P_SERIAL_NO",objFieldMaster.SERIAL_NO);
                        objParams[4] = new SqlParameter("@P_IND_FOR",objFieldMaster.IND_FOR);
                        objParams[5] = new SqlParameter("@P_CALC_ON_BASICAMT_YN", objFieldMaster.CALC_ON_BASICAMT_YN);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objFieldMaster.COLLEGE_CODE);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_FIELDMASTER_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FieldController.UpdateFiled-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllFields()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_FIELDMASTER_GET_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FieldController.GetAllFields-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSingleRecordField(int fieldNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_FIELD_NO", fieldNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_FIELDMASTER_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FieldController.GetSingleRecordField-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion
            }
        }
    }
}
