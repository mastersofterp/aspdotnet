using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Data;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class DocumentTypeController
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                //add document type data
                public int AddDocumentType(DocumentType objDocumentType)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DOCID", objDocumentType.DOCID);
                        objParams[1] = new SqlParameter("@P_DOCUMENT_TYPE", objDocumentType.DOCUMENTTYPE);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_DOCUMENT_TYPE_INSERT", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else if (Convert.ToInt32(ret) == 1)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DocumentTypeController.AddDocumentType-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //add data 
                public int AddDocData(DocumentType objDocType)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[20];
                        objParams[0] = new SqlParameter("@P_DOC_ID", objDocType.DOCDIDDATA );
                        objParams[1] = new SqlParameter("@P_DOC_TYPE",objDocType.DOCTYPE );
                        if (objDocType.DOCDATE == DateTime.MinValue)
                        {
                            objParams[2] = new SqlParameter("@P_DOC_DATE", DBNull.Value);
                        }
                        else
                        {
                            objParams[2] = new SqlParameter("@P_DOC_DATE", objDocType.DOCDATE);
                        }
                         objParams[3] = new SqlParameter("@P_DOC_NO", objDocType.DOCNO);
                         objParams[4] = new SqlParameter("@P_DOC_PROP_ADDRESS",objDocType.PROP_ADDRESS);
                         objParams[5] = new SqlParameter("@P_DISTRICT", objDocType.District);
                         objParams[6] = new SqlParameter("@P_SURVEYNO", objDocType.SURVEYNO);
                         objParams[7] = new SqlParameter("@P_SUBDIVINO", objDocType.SUBDIVINO);
                         objParams[8] = new SqlParameter("@P_TOTAREA", objDocType.TOTAREA);
                          objParams[9] = new SqlParameter("@P_EAST_SQ_FT", objDocType.EAST);
                          objParams[10] = new SqlParameter("@P_WEST_SQ_FT", objDocType.WEST);
                          objParams[11] = new SqlParameter("@P_NORTH_SQ_FT", objDocType.NORTH);
                          objParams[12] = new SqlParameter("@P_SOUTH_SQ_FT", objDocType.SOUTH);
                          objParams[13] = new SqlParameter("@P_ECNO", objDocType.EC_NO);

                          if (objDocType.FROM_DATE == DateTime.MinValue)
                          {
                              objParams[14] = new SqlParameter("@P_FROM_DATE", DBNull.Value);
                          }
                          else
                          {
                              objParams[14] = new SqlParameter("@P_FROM_DATE", objDocType.FROM_DATE);
                          }

                          if (objDocType.TO_DATE == DateTime.MinValue)
                          {
                              objParams[15] = new SqlParameter("@P_TO_DATE", DBNull.Value);
                          }
                          else
                          {
                              objParams[15] = new SqlParameter("@P_TO_DATE", objDocType.TO_DATE);
                          }

                          objParams[16] = new SqlParameter("@P_ATTACHTABLE", objDocType.AttachTable);
                          objParams[17] = new SqlParameter("@P_ISBLOB", objDocType.ISBLOB);
                          objParams[18] = new SqlParameter("@P_UANO", objDocType.UA_NO);
                       //   objParams[18] = new SqlParameter("@P_OTHER_DOCTYPE", objDocType.OTHERDOCTYPE);
                          objParams[19] = new SqlParameter("@P_OUT", SqlDbType.Int);
                          objParams[19].Direction = ParameterDirection.Output;
                          object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_DC_ASSEST_DOCUMENT_STORAGE_INSERT", objParams, true);
                          if (Convert.ToInt32(ret) == -99)
                              retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                          else if (Convert.ToInt32(ret) == 2)
                              retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                          else if (Convert.ToInt32(ret) == 1)
                              retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                          else
                              retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DocumentTypeController.AddDocData-> " + ex.ToString());
                    }
                    return retStatus;
                }


                //GET DATA BY ID
                public DataSet RetrieveDocumentDetails(DocumentType objDocType)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DOCID", objDocType.DOCID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_RETIRV_ADMN_DC_DOCUMENT_STORAGE_DATA_BY_DOCID", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DocumentTypeController.RetrieveDocumentDetails-> " + ex.ToString());
                    }
                    return ds;
                }


                public int UpdateDocData(DocumentType objDocType)
                {
                    int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[19];
                        objParams[0] = new SqlParameter("@P_DOC_ID", objDocType.DOCDIDDATA);
                        objParams[1] = new SqlParameter("@P_DOC_TYPE", objDocType.DOCTYPE);
                        if (objDocType.DOCDATE == DateTime.MinValue)
                        {
                            objParams[2] = new SqlParameter("@P_DOC_DATE", DBNull.Value);
                        }
                        else
                        {
                            objParams[2] = new SqlParameter("@P_DOC_DATE", objDocType.DOCDATE);
                        }
                        objParams[3] = new SqlParameter("@P_DOC_NO", objDocType.DOCNO);
                        objParams[4] = new SqlParameter("@P_DOC_PROP_ADDRESS", objDocType.PROP_ADDRESS);
                        objParams[5] = new SqlParameter("@P_DISTRICT", objDocType.District);
                        objParams[6] = new SqlParameter("@P_SURVEYNO", objDocType.SURVEYNO);
                        objParams[7] = new SqlParameter("@P_SUBDIVINO", objDocType.SUBDIVINO);
                        objParams[8] = new SqlParameter("@P_TOTAREA", objDocType.TOTAREA);
                        objParams[9] = new SqlParameter("@P_EAST_SQ_FT", objDocType.EAST);
                        objParams[10] = new SqlParameter("@P_WEST_SQ_FT", objDocType.WEST);
                        objParams[11] = new SqlParameter("@P_NORTH_SQ_FT", objDocType.NORTH);
                        objParams[12] = new SqlParameter("@P_SOUTH_SQ_FT", objDocType.SOUTH);
                        objParams[13] = new SqlParameter("@P_ECNO", objDocType.EC_NO);

                        if (objDocType.FROM_DATE == DateTime.MinValue)
                        {
                            objParams[14] = new SqlParameter("@P_FROM_DATE", DBNull.Value);
                        }
                        else
                        {
                            objParams[14] = new SqlParameter("@P_FROM_DATE", objDocType.FROM_DATE);
                        }

                        if (objDocType.TO_DATE == DateTime.MinValue)
                        {
                            objParams[15] = new SqlParameter("@P_TO_DATE", DBNull.Value);
                        }
                        else
                        {
                            objParams[15] = new SqlParameter("@P_TO_DATE", objDocType.TO_DATE);
                        }

                        objParams[16] = new SqlParameter("@P_ATTACHTABLE", objDocType.AttachTable);
                        objParams[17] = new SqlParameter("@P_ISBLOB", objDocType.ISBLOB);
                        objParams[18] = new SqlParameter("@P_UANO", objDocType.UA_NO);
                   //     objParams[18] = new SqlParameter("@P_OTHER_DOCTYPE", objDocType.OTHERDOCTYPE);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_DC_ASSEST_DOCUMENT_STORAGE_UPDATE", objParams, true);
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DocumentTypeController.UpdateDocData-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetData(DocumentType objDocType)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DOCTYPE", objDocType.DOCTYPE);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_DC_ASSEST_DOCUMENT_STORAGE_GET_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DocumentTypeController.GetData-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet RetriveDocData(DocumentType objDocType)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_DOCID", objDocType.DOCID);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ADMN_DC_DOC_GET_DATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DocumentTypeController.GetData-> " + ex.ToString());
                    }
                    return ds;
                }
            }
        }
    }
}
