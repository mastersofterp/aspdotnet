using System;
using System.Data;
using System.Web;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            /// <summary>
            /// This HelpController. is used to control Help table.
            /// </summary>
            public class HelpController
            {
                /// <summary>
                /// ConnectionString
                /// </summary>
                private string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                /// <summary>
                /// This method adds a new record in the Help Table
                /// </summary>
                /// <param name="objHelp">objHelp is the object of Help class.</param>
                /// <returns>Integer CustomStatus - RecordSaved or Error</returns>
                public int Add(Help objHelp)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add New Help
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_HAL_NO", objHelp.Hal_No);
                        objParams[1] = new SqlParameter("@P_HELPDESC", objHelp.HelpDesc);
                        objParams[2] = new SqlParameter("@P_HELPID",SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HELP_SP_INS_HELP", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HelpController.Add-> " + ex.ToString());
                    }

                    return retStatus;
                }

                /// <summary>
                /// This method updates a record in Help Table
                /// </summary>
                /// <param name="objHelp">objHelp is the object of Help class.</param>
                /// <returns>Integer CustomStatus - RecordUpdated or Error</returns>
                public int Update(Help objHelp)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        //UpdateFaculty Help
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_HELPID", objHelp.HelpId);
                        objParams[1] = new SqlParameter("@P_HAL_NO", objHelp.Hal_No);
                        objParams[2] = new SqlParameter("@P_HELPDESC", objHelp.HelpDesc);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_HELP_SP_UPD_HELP", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HelpController.UpdateFaculty-> " + ex.ToString());
                    }

                    return retStatus;
                }

                /// <summary>
                /// This method deletes a record in Help Table
                /// </summary>
                /// <param name="helpid">Deletes record as per this HelpID</param>
                /// <returns>Integer CustomStatus - RecordDeleted or Error</returns>
                public int Delete(int helpid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_HELPID", helpid);

                        objSQLHelper.ExecuteNonQuerySP("PKG_HELP_SP_DEL_HELP", objParams, false);
                        retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HelpController.GetSingleHelp-> " + ex.ToString());
                    }

                    return Convert.ToInt32(retStatus);
                }

                /// <summary>
                /// This method gets all records in Help Table
                /// </summary>
                /// <returns>Dataset</returns>
                public DataSet GetAllHelp()
                {
                    DataSet dsHelp = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        dsHelp = objSQLHelper.ExecuteDataSetSP("PKG_HELP_SP_ALL_HELP", objParams);

                    }
                    catch (Exception ex)
                    {
                        return dsHelp;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HelpController.GetAllNews-> " + ex.ToString());
                    }

                    return dsHelp;
                }

                /// <summary>
                /// This method gets a record in Help Table
                /// </summary>
                /// <param name="helpid">Gets a record as per this HelpID</param>
                /// <returns>SqlDataReader</returns>
                public SqlDataReader GetSingleHelp(int helpid)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_HELPID", helpid);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_HELP_SP_RET_HELP", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HelpController.GetSingleHelp-> " + ex.ToString());
                    }

                    return dr;
                }
                                                
            }

        }//END: BusinessLayer.BusinessLogic

    }//END: UAIMS  

}//END: IITMS