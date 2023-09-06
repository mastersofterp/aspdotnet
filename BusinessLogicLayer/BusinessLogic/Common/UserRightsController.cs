using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            /// <summary>
            /// This UserRightsController  is used to control User_Rights table.
            /// </summary>
            public class UserRightsController
            {

                /// <summary>
                /// ConnectionString
                /// </summary>
                private string uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                /// <summary>
                /// This method is used to add user rights in User_Rights table.
                /// </summary>
                /// <param name="objUserRights">Object of UserRights class</param>
                /// <returns>Integer Custom Status - Record Added or Error</returns>
                public int Add(UserRights objUserRights)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add 
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_USERDESC",  objUserRights.UserDesc);
                        objParams[1] = new SqlParameter("@P_USERRIGHTS", objUserRights.UserRightss);
                        objParams[2] = new SqlParameter("@P_USERTYPEID",SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_USER_RIGHTS_SP_INS_RIGHTS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.UserRightsController.Add-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// This method is used to update record from User_Rights table.
                /// </summary>
                /// <param name="objUserRights">Object of UserRights class</param>
                /// <returns>Integer Custom Status - Record Updated or Error</returns>
                public int Update(UserRights objUserRights)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add 
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_USERTYPEID", objUserRights.UserTypeID);
                        objParams[1] = new SqlParameter("@P_USERDESC", objUserRights.UserDesc);
                        objParams[2] = new SqlParameter("@P_USERRIGHTS", objUserRights.UserRightss);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_USER_RIGHTS_SP_UPD_RIGHTS", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.UserRightsController.UpdateFaculty-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// This method is used to retrieve single record from User_Rights table.
                /// </summary>
                /// <param name="usertypeid">Gets record as per this usertypeid</param>
                /// <returns>SqlDataReader</returns>
                public SqlDataReader GetSingleRecord(int usertypeid)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_USERTYPEID", usertypeid);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_USER_RIGHTS_SP_RET_RIGHTS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.UserRightsController.GetSingleRecord-> " + ex.ToString());
                    }
                    return dr;
                }

                /// <summary>
                /// This method is used to get single record from User_Rights table.
                /// </summary>
                /// <param name="usertype">Gets record as per this usertype</param>
                /// <returns>SqlDataReader</returns>
                //public SqlDataReader GetSingleRecordByType(int usertype)
                //{
                //    SqlDataReader dr = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                //        SqlParameter[] objParams = new SqlParameter[2];
                //        objParams[0] = new SqlParameter("P_USERTYPE", OracleDbType.Int32, usertype);
                //        objParams[1] = new SqlParameter("COMMON_CURSOR", OracleDbType.RefCursor, System.Data.ParameterDirection.Output);

                //        dr = objSQLHelper.ExecuteReaderSP("PKG_USER_RIGHTS.SP_RET_RIGHTSBYTYPE", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return dr;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.UserRightsController.GetSingleRecordByType-> " + ex.ToString());
                //    }
                //    return dr;
                //}

                /// <summary>
                /// This method is used to get all user rights from User_Rights table.
                /// </summary>
                /// <returns>DataSet</returns>
                public DataSet GetAllUserRights()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_USER_RIGHTS_SP_ALL_RIGHTS", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.UserRightsController.GetAllUserRights-> " + ex.ToString());
                    }
                    return ds;
                }
            }

        }//END: BusinessLayer.BusinessLogic

    }//END: UAIMS  

}//END: IITMS