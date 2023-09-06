//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : FILE MOVEMENT TRACKING                              
// CREATION DATE : 24-SEP-2015                                                        
// CREATED BY    : MRUNAL SINGH                                      
// MODIFIED DATE :
// MODIFIED DESC :
//====================================================================================== 
using System;
using System.Data;
using System.Web;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.FileMovement;

namespace IITMS
{
    namespace UAIMS
    {

        namespace BusinessLogicLayer.BusinessLogic  //.FileMovement    
        {
            public class FileMovementController
            {               
                /// <summary>
                /// ConnectionString
                /// </summary>
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                #region Section Name

                /// <summary>
                /// MODIFIED BY   : MRUNAL SINGH
                /// MODIFIED DATE : 25-SEP-2015
                /// DESCRIPTION   : IT IS USED TO ADD AND UPDATE SECTION NAMES.
                /// </summary>

                public int AddUpdateSectionName(FileMovement objFMov)
                {
                    int retstatus = 0;
                    try
                    {                     

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SECTIONNO", objFMov.SECTIONNO);
                        objParams[1] = new SqlParameter("@P_SECTIONNAME", objFMov.SECTIONNAME);
                        objParams[2] = new SqlParameter("@P_RECEIVER_ID", objFMov.RECEIVER_ID);
                        objParams[3] = new SqlParameter("@P_RECEIVER_HEAD_ID", objFMov.RECEIVER_HEAD_ID);
                        objParams[4] = new SqlParameter("@P_EMPDEPTNO", objFMov.EMPDEPTNO);
                        objParams[5] = new SqlParameter("@P_USER_ROLES", objFMov.USER_ROLES);
                        objParams[6] = new SqlParameter("@P_OUT", objFMov.SECTIONNO);
                        objParams[6].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_FILE_MOVEMENT_SECTION_NAME_INSERT_UPDATE", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FileMovementController.AddUpdateSectionName->" + ex.ToString());
                    }
                    return retstatus;
                }


                /// <summary>
                /// MODIFIED BY   : MRUNAL SINGH
                /// MODIFIED DATE : 17-MAR-2018
                /// DESCRIPTION   : IT IS USED TO GET SECTION USER DETAILS BY SECTION ID
                /// </summary> 

                public DataSet GetSectionUserDetails(int SECTION_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_SECTION_ID", SECTION_ID);                      
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_FILE_MOVEMENT_GET_SECTION_USER_DETAILS_BYID", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FileMovementController.GetSectionUserDetails()-> " + ex.ToString());
                    }

                    return ds;
                }


                /// <summary>
                /// MODIFIED BY   : MRUNAL SINGH
                /// MODIFIED DATE : 26-MAR-2018
                /// DESCRIPTION   : IT IS USED TO GET SECTION USER LIST
                /// </summary> 

                public DataSet GetSectionUserList()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_FILE_MOVEMENT_GET_SECTION_USER_LIST", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FileMovementController.GetSectionUserList()-> " + ex.ToString());
                    }

                    return ds;
                }

                #endregion

                #region File Master
                /// <summary>
                /// MODIFIED BY   : MRUNAL SINGH
                /// MODIFIED DATE : 26-SEP-2015
                /// DESCRIPTION   : IT IS USED TO ADD AND UPDATE FILE NAMES.
                /// </summary>

                public int AddUpdateFileName(FileMovement objFMov)
                {
                    int retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[22];
                        objParams[0] = new SqlParameter("@P_FILE_ID", objFMov.FILE_ID);
                        objParams[1] = new SqlParameter("@P_FILE_CODE", objFMov.FILE_CODE);
                        objParams[2] = new SqlParameter("@P_FILE_NAME", objFMov.FILE_NAME);
                        objParams[3] = new SqlParameter("@P_DESCRIPTION", objFMov.DESCRIPTION);
                        objParams[4] = new SqlParameter("@P_CREATION_DATE", objFMov.CREATION_DATE);
                        objParams[5] = new SqlParameter("@P_DOC_TYPE", objFMov.DOC_TYPE);
                        objParams[6] = new SqlParameter("@P_STATUS", objFMov.STATUS);
                        objParams[7] = new SqlParameter("@P_USERNO", objFMov.USERNO);
                        objParams[8] = new SqlParameter("@P_EMPDEPTNO", objFMov.EMPDEPTNO);
                        objParams[9] = new SqlParameter("@P_UPLNO", objFMov.UPLNO);
                        objParams[10] = new SqlParameter("@P_FILE_TABLE", objFMov.FILE_TABLE);
                        objParams[11] = new SqlParameter("@P_LINK_STATUS", objFMov.LINK_STATUS);
                        objParams[12] = new SqlParameter("@P_FOLDER_PATH", objFMov.FOLDER_PATH);
                        objParams[13] = new SqlParameter("@P_FILE_KEYWORDS", objFMov.FILE_KEYWORDS);
                        objParams[14] = new SqlParameter("@P_FILE_MOVID", objFMov.FILE_MOVID);
                        objParams[15] = new SqlParameter("@P_FILEPATH", objFMov.FILEPATH);
                        objParams[16] = new SqlParameter("@P_SECTIONPATH", objFMov.SECTIONPATH);
                        objParams[17] = new SqlParameter("@P_SECTIONNO", objFMov.SECTIONNO);
                        objParams[18] = new SqlParameter("@P_REMARK", objFMov.REMARK);
                        objParams[19] = new SqlParameter("@P_SELF_ROLE", objFMov.SELF_ROLE);
                        objParams[20] = new SqlParameter("@P_RECEIVER_ROLE", objFMov.RECEIVER_ROLE); 
                        objParams[21] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[21].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_FILE_MOVEMENT_FILE_NAME_INSERT_UPDATE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == -999)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else
                        {
                            objFMov.FILE_ID = Convert.ToInt32(ret); 
                             
                            //======================= For Movement ============================//                                               
                           // AddUpdateFileMovementPath(objFMov);

                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved); 
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FileMovementController.AddUpdateFileName->" + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet GetDocumentsByCategory(FileMovement objFMov)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_UA_NO", objFMov.UA_NO);
                        objParams[1] = new SqlParameter("@P_DNO", objFMov.DNO);
                        objParams[2] = new SqlParameter("@P_SOURCEPATH", objFMov.SOURCEPATH);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_FILE_MOVEMENT_GET_DOCUMENT_BY_DNO", objParams);
                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FileMovementController.GetDocumentsByCategory-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetCategoryPath(int dNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DNO", dNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_FILE_MOVEMENT_GET_PATH_BY_DNO", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FileMovementController.GetCategoryPath-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet RetrieveDocumentFiles(int FILE_ID, int UA_NO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_FILE_ID", FILE_ID);
                        objParams[1] = new SqlParameter("@P_UA_NO", UA_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_FILE_MOVEMENT_RETRIEVE_FILE_BY_UANO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FileMovementController.RetrieveDocumentFiles-> " + ex.ToString());
                    }
                    return ds;
                }

                public int DeleteDocument(int IDNO, int UANO)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_UANO", UANO);
                        object objc = objSQLHelper.ExecuteNonQuerySP("PKG_FILE_MOVEMENT_FILES_DELETE", objParams, true);

                        if (UANO != null && UANO.ToString().Equals("-99"))
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = -99;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FileMovementController.DeleteDocument-> " + ex.ToString());
                    }
                    return retstatus;

                }

                #endregion

                #region File Movement
                /// <summary>
                /// MODIFIED BY   : MRUNAL SINGH
                /// MODIFIED DATE : 28-SEP-2015
                /// DESCRIPTION   : IT IS USED TO ADD AND UPDATE FILE MOVEMENT PATH.
                /// </summary>

                public int AddUpdateFileMovementPath(FileMovement objFMov)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_FILE_MOVID", objFMov.FILE_MOVID);
                        objParams[1] = new SqlParameter("@P_FILE_ID", objFMov.FILE_ID);
                        objParams[2] = new SqlParameter("@P_FILEPATH", objFMov.FILEPATH);
                        objParams[3] = new SqlParameter("@P_SECTIONPATH", objFMov.SECTIONPATH);
                        objParams[4] = new SqlParameter("@P_USERNO", objFMov.USERNO);
                        objParams[5] = new SqlParameter("@P_SECTIONNO", objFMov.SECTIONNO);
                        objParams[6] = new SqlParameter("@P_REMARK", objFMov.REMARK);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_FILE_MOVEMENT_FILE_PATH_INSERT_UPDATE", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else if (Convert.ToInt32(ret) == 2627)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FileMovementController.AddUpdateFileMovementPath->" + ex.ToString());
                    }
                    return retstatus;
                }

                /// <summary>
                /// MODIFIED BY   : MRUNAL SINGH
                /// MODIFIED DATE : 25-NOV-2015
                /// DESCRIPTION   : IT IS USED TO GET THE MOVEMENT ENTRIES.
                /// </summary>
                public DataSet GetMovementTrans(int file_id, int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_FILE_ID", file_id);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);
                       ds = objSQLHelper.ExecuteDataSetSP("PKG_FILE_MOVEMENT_GET_FILE_MOVEMENT_TRANSACTIONS", objParams);                            
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FileMovementController.GetMovementTrans->" + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// MODIFIED BY   : MRUNAL SINGH
                /// MODIFIED DATE : 28-NOV-2015
                /// DESCRIPTION   : IT IS USED TO GET THE FILE.
                /// </summary>
                public int GetFileToReceive(int idno, int fileno)
                {
                   
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];                      
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                        objParams[1] = new SqlParameter("@P_FILEID", fileno);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                       // ds = objSQLHelper.ExecuteDataSetSP("PKG_FILE_MOVEMENT_GET_FILE_TO_RECEIVE", objParams);

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_FILE_MOVEMENT_GET_FILE_TO_RECEIVE", objParams, true);
                        if (Convert.ToInt32(ret) == 1)                           
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retstatus = Convert.ToInt32(CustomStatus.RecordNotFound);
                    }
                    catch (Exception ex)
                    {
                       retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FileMovementController.GetFileToReceive->" + ex.ToString());
                    }
                  return retstatus;    
                }





                /// <summary>
                /// MODIFIED BY   : MRUNAL SINGH
                /// MODIFIED DATE : 30-NOV-2015
                /// DESCRIPTION   : IT IS USED TO GET THE RECEIVER DETAILS.
                /// </summary>
                public DataSet GetReceiveDetails(int idno, int roleId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_IDNO", idno);
                         objParams[1] = new SqlParameter("@P_ROLE_ID", roleId);                             
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_FILE_MOVEMENT_GET_RECEIVE_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FileMovementController.GetFileToReceive->" + ex.ToString());
                    }
                    return ds;
                }




                /// <summary>
                /// MODIFIED BY   : MRUNAL SINGH
                /// MODIFIED DATE : 26-NOV-2015
                /// DESCRIPTION   : IT IS USED TO GET THE MOVEMENT DETAILS.
                /// </summary>
                public DataSet GetDetailsTran(int section_id, int file_no, int file_movid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SECTION_ID", section_id);
                        objParams[1] = new SqlParameter("@P_FILE_ID", file_no);
                        objParams[2] = new SqlParameter("@P_FILE_MOVID", file_movid);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_FILE_MOVEMENT_GET_MOVEMENT_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FileMovementController.GetMovementTrans->" + ex.ToString());
                    }
                    return ds;
                }
                #endregion


                #region File Receive
                /// <summary>
                /// MODIFIED BY   : MRUNAL SINGH
                /// MODIFIED DATE : 01-OCT-2015
                /// DESCRIPTION   : IT IS USED TO ADD & UPDATE FILE RECEIVEING DETAILS.
                /// </summary>

                public int AddUpdateFileReceiveDetails(FileMovement objFMov)
                {
                    int retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_FILE_MOVID", objFMov.FILE_MOVID);                       
                        objParams[1] = new SqlParameter("@P_FILE_ID", objFMov.FILE_ID);
                        objParams[2] = new SqlParameter("@P_SECTIONID", objFMov.SECTIONNO);
                        objParams[3] = new SqlParameter("@P_USERNO", objFMov.USERNO);
                        objParams[4] = new SqlParameter("@P_REMARK", objFMov.REMARK);
                        objParams[5] = new SqlParameter("@P_FSTATUS", objFMov.FSTATUS);
                        objParams[6] = new SqlParameter("@P_FILEPATH", objFMov.FILEPATH);
                        objParams[7] = new SqlParameter("@P_SECTIONPATH", objFMov.SECTIONPATH);
                        objParams[8] = new SqlParameter("@P_SECTION_TO_SEND", objFMov.SECTION_TO_SEND);
                        objParams[9] = new SqlParameter("@P_FILE_TABLE", objFMov.FILE_TABLE);
                        objParams[10] = new SqlParameter("@P_RECEIVER_ROLE", objFMov.RECEIVER_ROLE);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);  
                        objParams[11].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_FILE_MOVEMENT_FILE_RECEIVE_DETAILS_INSERT_UPDATE", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed); // if transaction failed
                        }
                        else if (Convert.ToInt32(ret) == 1)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);     // if receive file
                        }
                        else if (Convert.ToInt32(ret) == 2)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);     // if file is forward
                        }
                        else if (Convert.ToInt32(ret) == 3)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);  // if file is Return back  
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.FileExists); // if file is Completed 
                        }

                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FileMovementController.AddUpdateFileMovementPath->" + ex.ToString());
                    }
                    return retstatus;
                }


                public DataSet RetrieveDocumentsAtReceiver(int FILE_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_FILE_ID", FILE_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_FILE_MOVEMENT_RETRIEVE_FILE_AT_RECEIVER", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FileMovementController.RetrieveDocumentsAtReceiver-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet GetDestinationFilePath(int FILE_ID)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_FILE_ID", FILE_ID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_FILE_MOVEMENT_GET_PATH_BY_FILEID", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FileMovementController.GetDestinationFilePath-> " + ex.ToString());
                    }

                    return ds;
                }




                // This method is used to get login credentails for sending mail.
                public DataSet GetFromDataForEmail(int SectionId, int FileId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SECTIONNO", SectionId);
                        objParams[1] = new SqlParameter("@P_FILE_ID", FileId);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_FILE_MOVEMENT_GET_CREDENTIALS_FOR_EMAIL", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FileMovementController.GetFromDataForEmail-> " + ex.ToString());
                    }
                    return ds;
                }

                // This method is used to get login credentails for sending mail.
                public DataSet GetEmailDataForAll(int FileId)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];                      
                        objParams[0] = new SqlParameter("@P_FILE_ID", FileId);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_FILE_MOVEMENT_GET_DATA_FOR_ALL_USER", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FileMovementController.GetEmailDataForAll-> " + ex.ToString());
                    }
                    return ds;
                }


                public DataSet RetrieveDocuments(int FILE_ID, int UA_NO, string FILE_PATH)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_FILE_ID", FILE_ID);
                        objParams[1] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[2] = new SqlParameter("@P_FILE_PATH", FILE_PATH);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_FILE_MOVEMENT_RETRIEVE_FILE_BY_UANO_ON_MOVEMENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FileMovementController.RetrieveDocuments-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet RetrieveDocumentsAtReceiver(int FILE_ID, int UA_NO, string FILE_PATH)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_FILE_ID", FILE_ID);
                        objParams[1] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[2] = new SqlParameter("@P_FILE_PATH", FILE_PATH);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_FILE_MOVEMENT_RETRIEVE_FILE_AT_RECEIVER_ON_COMPLETE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FileMovementController.RetrieveDocumentsAtReceiver-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion

                #region Document Type


                /// <summary>
                /// CREATED BY   : MRUNAL SINGH
                /// CREATED DATE : 29-AUG-2017
                /// DESCRIPTION  : TO INSERT/UPDATE DOCUMENT TYPE
                /// </summary>
                /// <param name="objMM"></param>
                /// <returns></returns>
                /// 
                public int AddUpdateDocumentType(FileMovement objFMov)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DOCUMENT_TYPE_ID", objFMov.DOCUMENT_TYPE_ID);
                        objParams[1] = new SqlParameter("@P_DOCUMENT_TYPE", objFMov.DOCUMENT_TYPE);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_FILE_MOVEMENT_DOCUMENT_TYPE_IU", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FileMovementController.AddUpdateDocumentType->" + ex.ToString());
                    }
                    return retstatus;
                }



                public int DeleteDocumentType(int DOCUMENT_TYPE_ID)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DOCUMENT_TYPE_ID", DOCUMENT_TYPE_ID);
                        objSQLHelper.ExecuteNonQuerySP("PKG_FILE_MOVEMENT_DELETE_DOCUMENT_TYPE", objParams, false);
                        retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FileMovementController.DeleteDocumentType-> " + ex.ToString());
                    }
                    return Convert.ToInt32(retStatus);
                }

                #endregion

                #region File Details Search
                public DataSet FillFileName(string prefixText)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SEARCHTEXT", prefixText);
                        objParams[1] = new SqlParameter("@P_USERNO", HttpContext.Current.Session["userno"].ToString());
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_FILE_MOVEMENT_GET_FILES_BY_USERNO", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FileMovementController.FillFileName()-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetSelectedFileDetails(int file_id, int idno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_FILE_ID", file_id);
                        objParams[1] = new SqlParameter("@P_IDNO", idno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_FILE_MOVEMENT_GET_SELECTED_FILE_DETAILS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FileMovementController.GetSelectedFileDetails->" + ex.ToString());
                    }
                    return ds;
                }

                public DataSet FillFileNameByFileCode(string prefixText)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SEARCHTEXT", prefixText);
                        objParams[1] = new SqlParameter("@P_USERNO", HttpContext.Current.Session["userno"].ToString());
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_FILE_MOVEMENT_GET_FILES_BY_FILE_CODE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FileMovementController.FillFileNameByFileCode()-> " + ex.ToString());
                    }

                    return ds;
                }

                #endregion



                #region SMS Sending
                public int SENDMSG_PASS(string MSG, string MOBILENO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        int ret = 0;                       
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SMSBODY", MSG);
                        objParams[1] = new SqlParameter("@P_MOBILENO", MOBILENO);
                        objSQLHelper.ExecuteNonQuerySP("PKG_FILE_MOVEMENT_SEND_SMS", objParams, true);
                        return ret = 1;                      

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FileMovementController.SENDMSG_PASS-> " + ex.ToString());
                    }
                }


                public DataSet FillSectionUserNames(string prefixText)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SEARCHTEXT", prefixText);
                        objParams[1] = new SqlParameter("@P_USERTYPE", HttpContext.Current.Session["OperType"].ToString());
                        objParams[2] = new SqlParameter("@P_USERNO", HttpContext.Current.Session["userno"].ToString());
                        objParams[3] = new SqlParameter("@P_FILEID", HttpContext.Current.Session["FILE_ID"].ToString());
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_FILE_MOVEMENT_GET_SECTION_USER_BY_USERTYPE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FileMovementController.FillSectionUserNames()-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet FillSUserNamesInFileMaster(string prefixText)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SEARCHTEXT", prefixText);                       
                        objParams[1] = new SqlParameter("@P_USERNO", HttpContext.Current.Session["userno"].ToString());                      
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_FILE_MOVEMENT_SECTION_USER_IN_FILE_CREATION", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FileMovementController.FillSectionUserNames()-> " + ex.ToString());
                    }

                    return ds;
                }




                public int AddUpdateCloseFile(FileMovement objFMov)
                {
                    int retstatus = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_FILE_MOVID", objFMov.FILE_MOVID);
                        objParams[1] = new SqlParameter("@P_FILE_ID", objFMov.FILE_ID);
                        objParams[2] = new SqlParameter("@P_SECTIONID", objFMov.SECTIONNO);
                        objParams[3] = new SqlParameter("@P_USERNO", objFMov.USERNO);
                        objParams[4] = new SqlParameter("@P_REMARK", objFMov.REMARK);
                        objParams[5] = new SqlParameter("@P_FSTATUS", objFMov.FSTATUS);
                        objParams[6] = new SqlParameter("@P_FILEPATH", objFMov.FILEPATH);
                        objParams[7] = new SqlParameter("@P_SECTIONPATH", objFMov.SECTIONPATH);
                        objParams[8] = new SqlParameter("@P_SECTION_TO_SEND", objFMov.SECTION_TO_SEND);
                        objParams[9] = new SqlParameter("@P_FILE_TABLE", objFMov.FILE_TABLE);
                        objParams[10] = new SqlParameter("@P_RECEIVER_ROLE", objFMov.RECEIVER_ROLE);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_FILE_MOVEMENT_CLOSE_FILE_INSERT_UPDATE", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed); // if transaction failed
                        }                        
                        else if (Convert.ToInt32(ret) == 2)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordExist);     // if file is forward
                        }                       
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.FileExists); // if file is Completed 
                        }

                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FileMovementController.AddUpdateCloseFile->" + ex.ToString());
                    }
                    return retstatus;
                }


                /// <summary>
                /// MODIFIED BY   : MRUNAL SINGH
                /// MODIFIED DATE : 27-SEP-2017
                /// DESCRIPTION   : IT IS USED TO GET THE MOVEMENT ENTRIES.
                /// </summary>
                public DataSet GetSelectedFileDetails(int file_id)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_FILE_ID", file_id);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_FILE_MOVEMENT_GET_FILE_DETAILS_BY_ID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FileMovementController.GetSelectedFileDetails->" + ex.ToString());
                    }
                    return ds;
                }



                public DataSet GenerateFileNo(int USERNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[1];                       
                        objParams[0] = new SqlParameter("@P_USERNO", USERNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_FILE_MOVEMENT_GENERATE_FILENO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.FileMovementController.GenerateFileNo()-> " + ex.ToString());
                    }

                    return ds;
                }

                #endregion

                #region Role Name


                /// <summary>
                /// CREATED BY   : MRUNAL SINGH
                /// CREATED DATE : 14-MAR-2018
                /// DESCRIPTION  : TO INSERT/UPDATE ROLE NAME
                /// </summary>
                /// <param name="objMM"></param>
                /// <returns></returns>
                /// 
                public int AddUpdateRole(FileMovement objFMov)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ROLE_ID", objFMov.ROLE_ID);
                        objParams[1] = new SqlParameter("@P_ROLENAME", objFMov.ROLENAME);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_FILE_MOVEMENT_ROLE_NAME_IU", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FileMovementController.AddUpdateRole->" + ex.ToString());
                    }
                    return retstatus;
                }



           

                #endregion
            }
        }
    }
}
