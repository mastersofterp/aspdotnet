using System;
using System.Data;
using System.Web;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using IITMS.NITPRM;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            /// <summary>
            /// This ILibraryController is used to control ILibrary table.
            /// </summary>
            public partial class IPersonal_CalendarController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>
                string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                /// <summary>
                /// This method is used to add new E-Book/Link in the ILibrary table.
                /// </summary>
                /// <param name="objILib">objILib is the object of ILibrary class</param>
                /// <returns>Integer CustomStatus - Record Added or Error</returns>


                public int PersonalCalendar_SaveUpdateDelete(IPersonal_Calendar objIPC)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        bool flag = true;
                        if (flag == true)
                        {
                            //SaveUpdateDelete

                            objParams = new SqlParameter[9];
                            objParams[0] = new SqlParameter("@P_Operation",objIPC.Operation);
                            objParams[1] = new SqlParameter("@P_Id", objIPC.ID);
                            objParams[2] = new SqlParameter("@P_EventHeader", objIPC.HEADER);
                            objParams[3] = new SqlParameter("@P_EventDescription", objIPC.DESCRIPTION);
                            objParams[4] = new SqlParameter("@P_EventForeColor", objIPC.EventForeColor);
                            objParams[5] = new SqlParameter("@P_EventBackColor", objIPC.EventBackColor);
                            objParams[6] = new SqlParameter("@P_UserId", objIPC.USERID);
                            objParams[7] = new SqlParameter("@P_MainDate", objIPC.MAINDATE);
                            objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                            objParams[8].Direction = ParameterDirection.Output;

                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_PERSONAL_CALENDAR_SAVE_UPDATE_DELETE", objParams, true);
                            if (Convert.ToInt32(ret) == -99)
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (Convert.ToInt32(ret) == 1)
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (Convert.ToInt32(ret) == 2)
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            else if (Convert.ToInt32(ret) == 3)
                                retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                            }
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IPersonal_CalendarController.PersonalCalendar -> " + ex.ToString());
                    }

                    return retStatus;
                }

                public DataSet GetAllPersonalCalendar(IPersonal_Calendar objIPC)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_Operation", objIPC.Operation);
                        objParams[1] = new SqlParameter("@P_IDNO", objIPC.USERID);
                        objParams[2] = new SqlParameter("@P_DateFilter", objIPC.MAINDATE);
                        objParams[3] = new SqlParameter("@P_ID", objIPC.ID);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_GET_PERSONAL_CALENDER", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ILibraryController.GetAllEbooksByUaNo -> " + ex.ToString());
                    }
                    return ds;
                }

                //---------------------OLD--------------------------
                /// <summary>
                /// This method is used to retrieve single assignment from ASSIGNMASTER table.
                /// </summary>
                /// <param name="newsid">Retrieve single assignment as per the passed assignno,courseno,sessionno and uano.</param>
                /// <returns>DataTableReader</returns>               
                public DataTableReader GetSingleEBook(int bookno, int courseno, int sessionno, int uano)
                {
                    DataTableReader dtr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SUB_NO", bookno);
                        objParams[1] = new SqlParameter("P_COURSENO", courseno);
                        objParams[2] = new SqlParameter("P_SESSIONNO", sessionno);
                        objParams[3] = new SqlParameter("P_UA_NO", uano);

                        dtr = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_SINGLEBOOK_BY_BOOKNO", objParams).Tables[0].CreateDataReader();
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ILibraryController.GetSingleEBook -> " + ex.ToString());
                    }
                    return dtr;
                }

                public int DeleteEBookByUaNo( ILibrary objILib)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQL = new SQLHelper(_nitprm_constr);

                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", objILib.SESSIONNO);
                        objParams[1] = new SqlParameter("@P_COURSENO", objILib.COURSENO);
                        objParams[2] = new SqlParameter("@P_UA_NO", objILib.UA_NO);
                        objParams[3] = new SqlParameter("@P_BOOK_NO", objILib.BOOK_NO);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQL.ExecuteNonQuerySP("PKG_ITLE_SP_DEL_ELIBRARY", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ILibraryController.DeleteEBookByUaNo -> " + ex.ToString());
                    }

                    return retStatus;
                }



            }
        }
    }
}
