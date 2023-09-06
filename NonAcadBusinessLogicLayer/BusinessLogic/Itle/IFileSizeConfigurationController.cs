using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

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
        namespace BusinessLayer
        {
            /// <summary>
            /// This IFileSizeConfigurationController is used to define Size of attachment files.
            /// </summary>

            namespace BusinessLogicLayer
            {
              public partial  class IFileSizeConfigurationController
                {

                    /// <summary>
                    /// ConnectionStrings
                    /// </summary>
                    string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                    /// <summary>
                    /// This method is used to add new assignment in the ASSIGNMASTER table.
                    /// </summary>
                    /// <param name="objAssign">objAssign is the object of Assignment class</param>
                    /// <returns>Integer CustomStatus - Record Added or Error</returns>


                    public int InsertUpdateFileSize(IFileSizeConfiguration objFileSize, int OrgId)
                    {
                        int retStatus=Convert.ToInt32(CustomStatus.Others);

                        try
                        {
                            SQLHelper objHelper = new SQLHelper(_nitprm_constr);
                            SqlParameter[] objParam = new SqlParameter[6];

                            objParam[0] = new SqlParameter("@P_UA_TYPE", objFileSize.UA_TYPE);
                            objParam[1] = new SqlParameter("@P_FILE_SIZE", objFileSize.FILE_SIZE);
                            objParam[2] = new SqlParameter("@P_PAGE_ID", objFileSize.PAGE_ID);
                            objParam[3] = new SqlParameter("@P_UNIT", objFileSize.UNIT);
                            objParam[4] = new SqlParameter("@P_Org_ID", OrgId);
                            objParam[5] = new SqlParameter("@P_OUT",SqlDbType.Int);
                            objParam[5].Direction = ParameterDirection.Output;

                            Object ret = objHelper.ExecuteNonQuerySP("PKG_ITLE_SP_INS_UPD_FILESIZE_BY_UATYPE", objParam,true);

                            if (Convert.ToInt32(ret) == -99)
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);


                        }
                        catch (Exception ex)
                        { 
                        
                        }

                        return retStatus;
                    }



                    public DataTableReader GetFileSizeByUserType(int page_no, int user_type, int OrgId)
                    {
                        DataTableReader dtr = null;
                        try
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                            SqlParameter[] objParams = new SqlParameter[3];
                            objParams[0] = new SqlParameter("@P_PAGE_NO", page_no);
                            objParams[1] = new SqlParameter("@P_USER_TYPE", user_type);
                            objParams[2] = new SqlParameter("@P_Org_ID", OrgId);

                            dtr = objSQLHelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_SINGLE_PAGEINFO_BY_USERTYPE", objParams).Tables[0].CreateDataReader();
                        }
                        catch (Exception ex)
                        {
                            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IAnnouncementController.GetSingleAnnouncement-> " + ex.ToString());
                        }
                        return dtr;
                    }


                  public DataSet GetAllPagesByUserNo(int ua_type, int OrgId)
                    {
                        DataSet ds = null;
                        try
                        {
                            SQLHelper objSH = new SQLHelper(_nitprm_constr);
                            SqlParameter[] objParams = null;
                            objParams = new SqlParameter[2];

                            objParams[0] = new SqlParameter("@P_UA_TYPE", ua_type);
                            objParams[1] = new SqlParameter("@P_Org_ID", OrgId);

                            ds = objSH.ExecuteDataSetSP("PKG_ITLE_GET_PAGES_BY_USERTYPE", objParams);
                        }
                        catch (Exception ex)
                        {
                        }
                        return ds;
                    }


                    public int AddPageID(int pageid, int OrgId)
                    {
                        int retStatus = Convert.ToInt32(CustomStatus.Others);
                        try
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                            SqlParameter[] objParams = null;

                            bool flag = true;
                            //string uploadPath = HttpContext.Current.Server.MapPath("~/ITLE/IAnnounce/"); //UPLOAD_FILES\announcement
                            if (flag == true)
                            {
                                //Add New Announcement



                                objParams = new SqlParameter[3];
                                objParams[0] = new SqlParameter("@P_PAGE_ID", pageid);
                                objParams[1] = new SqlParameter("@P_Org_ID", OrgId);
                                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                                objParams[2].Direction = ParameterDirection.Output;

                                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_SP_INS_PAGE_CONFIG", objParams, true);
                                if (Convert.ToInt32(ret) == -99)
                                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                                else if (Convert.ToInt32(ret) == 1)
                                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                                else
                                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                                
                                
                            }
                        }
                        catch (Exception ex)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IFileSizeConfigurationController.AddPageID -> " + ex.ToString());
                        }

                        return retStatus;
                    }
                  

                }
            }
        }
    }
}
