//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : DOCUMENT & SCANNING
// PAGE NAME     : DCMNTSCN_Category.ASPX                                                    
// CREATION DATE : 18-JAN-2011                                                        
// CREATED BY    : PRAKASH RADHWANI 
// MODIFIED DATE : 22-JAN-2015
// MODIFIED BY   : MRUNAL SINGH
// MODIFIED DESC : CHANGE THE NAME OF ALL THE PROCEDURES
//=======================================================================================
using System;
using System.Data;
using System.Web;
using IITMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {

            public class DocumentController
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


                public int AddDocument(Documentation objC)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_TITLE", objC.TITLE);
                        objParams[1] = new SqlParameter("@P_UA_NO", objC.UA_NO);
                        objParams[2] = new SqlParameter("@P_DNO", objC.DNO);
                        objParams[3] = new SqlParameter("@P_DESCRIPTION", objC.DESCRIPTION);
                        objParams[4] = new SqlParameter("@P_KEYWORD", objC.KEYWORD);
                        objParams[5] = new SqlParameter("@P_SHARE", objC.SHARED);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objC.COLLEGE_CODE);
                        objParams[7] = new SqlParameter("@P_ATTACH_TABLE", objC.AttachTable);
                        //objParams[7] = new SqlParameter("@P_ALLOW_DEPTS", objC.DEPARTMENTS );
                        objParams[8] = new SqlParameter("@P_UPLNO", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;
                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_DOCUMENT_UPLOAD_INSERT", objParams, true);

                        retStatus = Convert.ToInt32(obj);
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DocController.AddDocument-> " + ex.ToString());
                    }
                    return retStatus;
                }

                /// <summary>
                /// Created By : Mrunal Singh(15/12/2015)
                /// Description - To get list of user with assigned category.
                /// </summary>
                /// <param name="usertype"></param>
                /// <returns></returns>
                public DataSet GetEmployeeCategory(int usertype)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_TYPE", usertype);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_DOCUMENT_GET_EMPLOYEE_CATEGORY", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUsersByUserType-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetUsersWithTreeNode(int usertype, string Userlinks)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_UA_TYPE", usertype);
                        objParams[1] = new SqlParameter("@P_LINKS", Userlinks);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_DOCUMENT_CATEGORY_ASSIGNED_LIST", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.GetUsersByUserType-> " + ex.ToString());
                    }
                    return ds;
                }


                public int AddFile(Documentation obj)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_FILENAME", obj.FILENAME);
                        objParams[1] = new SqlParameter("@P_SIZE", obj.SIZE);
                        objParams[2] = new SqlParameter("@P_UA_NO", obj.UA_NO);
                        objParams[3] = new SqlParameter("@P_FILEPATH", obj.FILE_PATH);
                        objParams[4] = new SqlParameter("@P_ORIGINAL_FILENAME", obj.ORIGINAL_FILENAME);
                        objParams[5] = new SqlParameter("@P_UPLNO", obj.UPLNO);
                        objParams[6] = new SqlParameter("@P_IDNO", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;
                        object objc = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_DOCUMENT_FILE_INSERT", objParams, true);

                        if (objc != null && obj.ToString().Equals("-99"))
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
                        retstatus = -99;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DocumentController.AddFile-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public int AddCategory(Documentation obj)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_DOCUMENTNAME", obj.CAT_NAME);
                        objParams[1] = new SqlParameter("@P_SUBHEAD", obj.SUB_HEAD);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", obj.COLLEGE_CODE);
                        objParams[3] = new SqlParameter("@P_UANO", obj.UA_NO);
                        objParams[4] = new SqlParameter("@P_DNO", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        object O = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_DOCUMENT_CATEGORY_INSERT", objParams, true);

                        if (O != null && O.ToString().Equals("-99"))
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
                        retstatus = -99;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DocumentController.AddCategory-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public int UpdateCategory(Documentation obj)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_DOCUMENTNAME", obj.CAT_NAME);
                        objParams[1] = new SqlParameter("@P_SUBHEAD", obj.SUB_HEAD);
                        objParams[2] = new SqlParameter("@P_DNO", obj.DNO);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;
                        object O = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_DOCUMENT_CATEGORY_UPDATE", objParams, true);

                        if (O != null && O.ToString().Equals("-99"))
                        {
                            retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                        {
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                    }
                    catch (Exception ex)
                    {
                        retstatus = -99;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DocumentController.UpdateCategory-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet PopulateTree(int ua_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", ua_no);
                        //objParams[0] = new SqlParameter("@P_UA_NO", Convert.ToInt32(HttpContext.Current.Session["userno"]));
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_DOCUMENT_TREENODE", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DocumentController.PopulateTree-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet PopulateChild(int id, int ua_no)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_PARENTID", id);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        //objParams[1] = new SqlParameter("@P_UA_NO",Convert.ToInt32( HttpContext.Current.Session["USERNO"]));
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_DOCUMENT_TREENODE_CHILD", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DocumentController.RetrieveAllDocument-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet Retrieve_Category_ByUANO(Documentation obj)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UANO", obj.UA_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_DOCUMENT_CATEGORY_RETRIEVE_BYUNO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DocumentController.Retrieve_Category_ByUANO-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet RetrieveAllDocument(Documentation obj)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_UA_NO", obj.UA_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_DOCUMENT_RETRIEVE_ALL_BY_UANO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DocumentController.RetrieveAllDocument-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet RetrieveDocument(Documentation obj)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_UPLNO", obj.UPLNO);
                        objParams[1] = new SqlParameter("@P_UA_NO", obj.UA_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_DOCUMENT_RETRIEVE_BY_UANO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DocumentController.RetrieveDocument-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet RetrieveDocumentFiles(Documentation obj)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_UPLNO", obj.UPLNO);
                        objParams[1] = new SqlParameter("@P_UA_NO", obj.UA_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_DOCUMENT_RETRIEVE_FILE_BY_UANO", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DocumentController.RetrieveDocumentFiles-> " + ex.ToString());
                    }
                    return ds;
                }
                public int UpdateDocument(Documentation obj)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_TITLE", obj.TITLE);
                        objParams[1] = new SqlParameter("@P_UA_NO", obj.UA_NO);
                        objParams[2] = new SqlParameter("@P_DNO", obj.DNO);
                        objParams[3] = new SqlParameter("@P_DESCRIPTION", obj.DESCRIPTION);
                        objParams[4] = new SqlParameter("@P_KEYWORD", obj.KEYWORD);
                        objParams[5] = new SqlParameter("@P_SHARE", obj.SHARED);
                        objParams[6] = new SqlParameter("@P_UPLNO", obj.UPLNO);
                        //objParams[7] = new SqlParameter("@P_ALLOW_DEPTS", obj.DEPARTMENTS );

                        objParams[7] = new SqlParameter("@P_ATTACH_TABLE", obj.AttachTable);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;
                        object objc = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_DOCUMENT_UPLOAD_UPDATE", objParams, true);
                        retstatus = Convert.ToInt32(objc);
                    }
                    catch (Exception ex)
                    {
                        retstatus = -99;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DocumentController.UpdateDocument-> " + ex.ToString());
                    }
                    return retstatus;
                }

                public DataSet SearchDocumenttitlewise(Documentation obj)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_PREFIX", obj.TITLE);
                        objParams[1] = new SqlParameter("@P_UA_NO", obj.UA_NO);
                        objParams[2] = new SqlParameter("@P_TYPE", obj.TYPE);
                        objParams[3] = new SqlParameter("@P_UPLNO", obj.UPLNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_DOCUMENT_SEARCH_TITLEWISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DocumentController.SearchDocument-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet SearchDocument(Documentation obj)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_PREFIX", obj.TITLE);
                        objParams[1] = new SqlParameter("@P_UA_NO", obj.UA_NO);
                        objParams[2] = new SqlParameter("@P_TYPE", obj.TYPE);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_DOCUMENT_SEARCH", objParams);
                    }

                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DocumentController.SearchDocument-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet SearchDocumentKeyword(Documentation obj)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_PREFIX", obj.KEYWORD);
                        objParams[1] = new SqlParameter("@P_UA_NO", obj.UA_NO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_DOCUMENT_SEARCH_KEYWORD", objParams);
                    }

                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DocumentController.SearchDocument-> " + ex.ToString());
                    }
                    return ds;
                }
                public DataSet SearchDocumentTreeView(Documentation obj)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DNO", obj.DNO);
                        objParams[1] = new SqlParameter("@P_UA_NO", obj.UA_NO);
                        objParams[2] = new SqlParameter("@P_TYPE", obj.TYPE);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_DOCUMENT_SEARCH_TREEVIEW", objParams);
                    }

                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DocumentController.SearchDocument-> " + ex.ToString());
                    }
                    return ds;
                }
                public int DeleteDocument(Documentation obj)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_UA_NO", obj.UA_NO);
                        objParams[1] = new SqlParameter("@P_UPLNO", obj.UPLNO);
                        object objc = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_DOCUMENT_UPLOAD_DELETE", objParams, true);

                        if (obj != null && obj.ToString().Equals("-99"))
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
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DocumentController.DeleteDocument-> " + ex.ToString());
                    }
                    return retstatus;

                }
                public SqlDataReader AutoCompleteDocumentKeyword(string preFix, int ua_no)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        //SqlParameter[] objParams = new SqlParameter[]
                        //    { 
                        //      new SqlParameter("@P_PREFIX",preFix)  
                        //    };
                        //dr = objSQLHelper.ExecuteReaderSP("PKG_AUTOCOMPLETE_KEYWORD", objParams);

                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_PREFIX", preFix);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        dr = objSQLHelper.ExecuteReaderSP("PKG_ADMN_DOCUMENT_AUTOCOMPLETE_KEYWORD_DOC", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessEntities.DocumentController.AutoCompleteDocumentKeyword() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return dr;
                }

                public DataSet GetNestedPath(int dNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DNO", dNo);
                        //objParams[1] = new SqlParameter("@P_PATH", SqlDbType.NVarChar);
                        //objParams[1].Direction = ParameterDirection.Output;
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_DOCUMENT_GET_NESTED_PATH", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DocumentController.GetNestedPath-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet SearchDocumentByUPDNo(Documentation obj)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_PREFIX", obj.TITLE);
                        objParams[1] = new SqlParameter("@P_UA_NO", obj.UA_NO);
                        objParams[2] = new SqlParameter("@P_TYPE", obj.TYPE);
                        objParams[3] = new SqlParameter("@P_UPLNO", obj.UPLNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_DOCUMENT_SEARCH_BY_UANO_UPLOADNO", objParams);
                    }

                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DocumentController.SearchDocument-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet RetrieveDocumentByCategory(Documentation obj)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_UA_NO", obj.UA_NO);
                        objParams[1] = new SqlParameter("@P_DNO", obj.DNO);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_DOCUMENT_RETRIEVE_ALL_BY_UANO_DNO", objParams);
                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DocumentController.RetrieveDocumentByCategory-> " + ex.ToString());
                    }
                    return ds;
                }
            }
        }
    }
}