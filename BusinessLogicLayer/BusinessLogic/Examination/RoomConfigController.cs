using System;
using System.Data;
using System.Web;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
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
            /// This RoomConfigController is used to control Room Configuration detail.
            /// </summary>
            public class RoomConfigController
            {
                /// <summary>
                /// ConnectionString
                /// </summary>
                string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


                /// <summary>
                /// This controller is used to Get The Room Config. Info. By roomno
                /// Page : RoomConfig.aspx
                /// </summary>
                /// <param name="roomno"></param>
                /// <returns></returns>


                public SqlDataReader GetRoomConfiguration(int roomno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ROOM_NO", roomno);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_ACD_GET_ROOM_CONFIG", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.RoomConfigController.GetRoomConfiguration-> " + ex.ToString());
                    }
                    return dr;
                }


                //Add RoomConfig Details
                /// <summary>
                /// This controller is used to insert Room configuration detail.
                /// Page : RoomCOnfig.aspx
                /// </summary>
                /// <param name="objRoom"></param>
                /// <returns></returns>

                public int AddRoomConfig(RoomConfig objRoom, int collegeid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        //Add Block info 
                        objParams = new SqlParameter[9];

                        objParams[0] = new SqlParameter("@P_ROOM_NO", objRoom.Room_No);
                        objParams[1] = new SqlParameter("@P_ROOM_NAME", objRoom.Room_name);
                        objParams[2] = new SqlParameter("@P_ROWS", objRoom.Rows);
                        objParams[3] = new SqlParameter("@P_COLUMNS", objRoom.Columns);
                        objParams[4] = new SqlParameter("@P_ACTUAL_CAPACITY", objRoom.Actual_Capacity);
                        objParams[5] = new SqlParameter("@P_DISABLED_ID", objRoom.DisbStudId);
                        objParams[6] = new SqlParameter("@P_STATUS", objRoom.Status);
                        objParams[7] = new SqlParameter("@P_COLLEGE_ID", collegeid);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;


                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ROOMCONFIG_INSERT", objParams, false));
                        if (retStatus != 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else if (retStatus != 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.RoomConfigController.AddRoomConfig()-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// This controller is used to Get All Room Info
                /// Page : RoomConfig.aspx
                /// </summary>
                /// <returns></returns>

                public DataSet GetAllRooms()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ROOM_MASTER_GET_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.RoomConfigController.GetAllBuildingRooms-> " + ex.ToString());
                    }
                    return ds;
                }

               

            }
        }
    }
}

