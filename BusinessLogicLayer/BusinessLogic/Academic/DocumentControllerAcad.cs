using System;
using System.Data;
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Created By  : 
Created On  : 
Purpose     :  
Version     : 
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Version     Modified On     Modified By       Purpose
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
1.0.1       27-02-2024      Anurag Baghele    [53807]-Added the parameters in AddDocument and UpdateDocument for submitting DocNO
------------------------------------------- ------------------------------------------------------------------------------------------------------------------------------
1.0.2       28-02-2024      Anurag Baghele    [53807]-Created the InsUpdDocumentName and GetAllDocumentName Method
------------------------------------------- ------------------------------------------------------------------------------------------------------------------------------
*/

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class DocumentControllerAcad
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddDocument(DocumentAcad objDocument, string category, string nationality, string admCategory)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_DOCUMENT_NAME",objDocument.Documentname),
                    new SqlParameter("@P_DOC_NO",objDocument.DocNo),//<1.0.1>
                    new SqlParameter("@P_DEGREE", objDocument.Degree),
                    new SqlParameter("@P_COLLEGE_CODE", objDocument.CollegeCode),
                    new SqlParameter("@P_IDTYPE", objDocument.Idtype),
                    new SqlParameter("@P_SRNO", objDocument.DocumentSrno),
                    new SqlParameter("@P_STATUS", objDocument.chkstatus),
                    new SqlParameter("@P_CATEGOTY", category),
                    new SqlParameter("@P_NATIONALITY", nationality),
                    new SqlParameter("@P_MANDATORY", objDocument.MandtStatus),
                    new SqlParameter("@P_ADMCATEGORYNO", admCategory),
                    new SqlParameter("@P_DOCUMENTNO", status)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DOCUMENT_INSERT", sqlParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DocumentController.AddDocument() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public int UpdateDocument(DocumentAcad objDocument, string category, string nationality, string admCategory)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {                    
                    new SqlParameter("@P_DOCUMENT_NAME",objDocument.Documentname),
                    new SqlParameter("@P_DOC_NO",objDocument.DocNo),//<1.0.1>
                    new SqlParameter("@P_DEGREE", objDocument.Degree),
                    new SqlParameter("@P_COLLEGE_CODE", objDocument.CollegeCode),
                    new SqlParameter("@P_IDTYPE", objDocument.Idtype),
                    new SqlParameter("@P_SRNO", objDocument.DocumentSrno),
                    new SqlParameter("@P_STATUS", objDocument.chkstatus),
                    new SqlParameter("@P_CATEGOTY", category),
                    new SqlParameter("@P_NATIONALITY", nationality),
                    new SqlParameter("@P_MANDATORY", objDocument.MandtStatus),
                    new SqlParameter("@P_ADMCATEGORYNO", admCategory),
                    new SqlParameter("@P_DOCUMENTNO",objDocument.Documentno)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DOCUMENT_UPDATE", sqlParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DocumentController.UpdateDocument() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetAllDocument()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_DOCUMENT_ALL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DocumentController.GetAllDocument() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public SqlDataReader GetDocumentByNo(int documentNo)
        {
            SqlDataReader dr = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_DOCUMENTNO", documentNo) };

                dr = objSQLHelper.ExecuteReaderSP("PKG_ACAD_DOCUMENT_GET_BY_NO", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DocumentController.GetDocumentByNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return dr;
        }

        //<1.0.2>
        public int InsUpdDocumentName(DocumentAcad objDocument)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];

                objParams[0] = new SqlParameter("@P_DOC_ID", objDocument.DocId);
                objParams[1] = new SqlParameter("@P_DOCUMENT_NAME", objDocument.Documentname);
                objParams[2] = new SqlParameter("@P_ACTIVE_STATUS", objDocument.chkstatus);
                objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DOCUMENT_NAME_INS_UPD", objParams, true);
                status = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
            }
            return status;
        }

        public DataSet GetAllDocumentName()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_DOCUMENT_NAME_ALL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DocumentController.GetAllDocument() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        //</1.0.2>
    }
}
