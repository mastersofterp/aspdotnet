using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessLogic.PostAdmission
{
    public class ADMPDocumentUploadController
    {

         string _UAIMS_constr = string.Empty;
         public ADMPDocumentUploadController()
                {
                    _UAIMS_constr=System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                }
         public DataSet GetBranch(int Degree)
         {
             DataSet ds = null;
             SQLHelper objDataAccessLayer = new SQLHelper(_UAIMS_constr);

             try
             {
                 SqlParameter[] objParams = new SqlParameter[]
                        {
                           new SqlParameter("@Degree",Degree),
                        };

                 ds = objDataAccessLayer.ExecuteDataSetSP("[dbo].[PKG_ACAD_GET_GETDEGREENAME]", objParams);
             }
             catch (Exception ex)
             {
                 throw new Exception(ex.Message);
             }

             return ds;
         }

         public DataSet GetStudentsForDocumentUpload(int ADMBATCH,int ProgramType,int DegreeNo,string branchno,string SearchBy, string SearchValue)
         {
             DataSet ds = null;
             SQLHelper objDataAccessLayer = new SQLHelper(_UAIMS_constr);

             try
             {
                 SqlParameter[] objParams = new SqlParameter[]
                        {
                           new SqlParameter("@P_ADMBATCH",ADMBATCH),
                           new SqlParameter("@P_PROGRAMME_TYPE",ProgramType),
                           new SqlParameter("@P_DEGREENO",DegreeNo),
                           new SqlParameter("@P_BRANCHNO",branchno),   
                           new SqlParameter("@P_SEARCH",SearchBy),  
                           new SqlParameter("@P_SEARCHSTRING",SearchValue)   
                        };

                 ds = objDataAccessLayer.ExecuteDataSetSP("[dbo].[PKG_GET_STUDENT_LIST_FOR_DOCUMENTUPLOAD]", objParams);
             }
             catch (Exception ex)
             {
                 throw new Exception(ex.Message);
             }

             return ds;
         }

        public DataSet GetDocumentList(int UA_NO, int DegreeNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_UA_NO", UA_NO);
                objParams[1] = new SqlParameter("@P_DEGREENO", DegreeNo);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ADMP_DOCUMENT_LIST", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentRegistrationController.GetDocumentList-> " + ex.ToString());
            }
            return ds;
        }

        public int AddUpdateDocumentsDetail(int idno, int hiddtudocno, string extension, string contentType, string filename, string path, string issuedate)
        {

            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_STU_DOC_NO", hiddtudocno);
                // objParams[2] = new SqlParameter("@P_CHK", chkDocuments);
                objParams[2] = new SqlParameter("@P_EXTENSION", extension);
                objParams[3] = new SqlParameter("@P_CONTENTTYPE", contentType);
                // objParams[5] = new SqlParameter("@P_FILEDATA", document);
                objParams[4] = new SqlParameter("@P_FILEPATH", path);
                objParams[5] = new SqlParameter("@P_FILENAME", filename);


                //objParams[6] = new SqlParameter("@P_CERTFICATE_NO", certificateno);
                //objParams[7] = new SqlParameter("@P_DISTRICT", district);
                objParams[6] = new SqlParameter("@P_ISSUEDATE", issuedate);
                //objParams[9] = new SqlParameter("@P_AUTHORITY", Authority);
                objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);


                objParams[7].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADMP_INSERT_UPDATE_POSTADM_UPLOAD_DOCUMENT", objParams, true);

                if (Convert.ToInt32(ret) == 1)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddUpdateStudentDocumentsDetail-> " + ex.ToString());
            }
            return retStatus;
            //throw new NotImplementedException();
        }

    }
}
