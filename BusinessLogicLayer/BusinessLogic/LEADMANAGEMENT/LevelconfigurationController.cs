using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLogicLayer.BusinessLogic
        {
           public class LevelconfigurationController
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                   /// <summary>
                  /// This Function is used to Insert or Updated Level Configuration Detail.
                 /// </summary>
                /// <param name="objLCEntity"></param>
               /// <returns></returns>
                public int InsertUpdateLevelConfiguration(DataTable ds)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_LEADLEVELCONFIGURATIONTBL", ds);
                        objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[1].Direction = ParameterDirection.Output;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_LEAD_LEVELCONFIGURATION_INSERT_UPDATE", objParams, true);
                        if (obj != null && obj.ToString() == "1")
                        {
                            status = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        else if (obj.ToString() == "-99")
                        {
                            status = Convert.ToInt32(CustomStatus.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LevelconfigurationController.InsertUpdateLevelConfiguration-> " + ex.ToString());
                    }
                    return status;
                }

                   /// <summary>
                  /// This Function is to show detail of Level Configuration by Selected Lead UANO
                 /// </summary>
                /// <param name="objLCEntity"></param>
               /// <returns></returns>
                public DataSet GetSelectedOrAllLeveConfigurationDetail(int LEAD_UA_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[0];
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_LEAD_UA_NO", LEAD_UA_NO);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_LEAD_LEVELCONFIGURATION_DETAIL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.LevelconfigurationController.GetSelectedOrAllLeveConfigurationDetail() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }
            }
        }
    }
}
