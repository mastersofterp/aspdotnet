using IITMS;
using IITMS.SQLServer.SQLDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessLogic.PostAdmission
{
   public class ADMPExamMarkUploadController
    {
          string _UAIMS_constr = string.Empty;
          public ADMPExamMarkUploadController()
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

         public DataSet getStudentMarkExcel(int ADMBATCH, int ProgramType, int DegreeNo, string branchno)
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
                           new SqlParameter("@P_BRANCHNO",branchno)
                        };

                 ds = objDataAccessLayer.ExecuteDataSetSP("[dbo].[PKG_GET_STUDENT_MARKS_LIST]", objParams);
             }
             catch (Exception ex)
             {
                 throw new Exception(ex.Message);
             }

             return ds;
         }

         public int Add_Entrance_Mark_Entry_Excel_Data(int ADMBATCH, int PROGRAMTYPE, int DEGREENO, string BRANCHNO, string filename, double marks, int userno, string ipAddress,int CreatedBy,string AppicationId)
         {
             int ret = 0;
             try
             {
                 SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                 SqlParameter[] objPar = new SqlParameter[]
                        {
                           // new SqlParameter("@P_TBLBULKDATA",dt),
                            new SqlParameter("@P_ADMBATCH",ADMBATCH),    
                            new SqlParameter("@P_PROGRAMME_TYPE",PROGRAMTYPE),                           
                            new SqlParameter("@P_DEGREENO",DEGREENO),
                            new SqlParameter("@P_BRANCHNO",BRANCHNO),
                            new SqlParameter("@P_FILENAME",filename),
                            new SqlParameter("@P_ENTR_MARKS",marks),
                            new SqlParameter("@P_USERNO",userno),
                            new SqlParameter("@P_APPLICATIONID",AppicationId),
                            new SqlParameter("@P_IPADDRESS",ipAddress),
                            new SqlParameter("@P_CREATEDBY",CreatedBy),
                            new SqlParameter("@P_OUTPUT",DbType.String)
                        };
                 objPar[objPar.Length - 1].Direction = ParameterDirection.Output;

                 object val = objSQL.ExecuteNonQuerySP("PKG_SP_MARK_ENTRY_DATA_UPLOAD", objPar, true);
                 if (val != null)
                 {
                     ret = Convert.ToInt16(val.ToString());
                 }
                 else
                 {
                     ret = -99;
                 }
             }
             catch (Exception ex)
             {
                 throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.AffiliatedController.Add_Fee_Defintion_For_Affiliated_ForExcel-> " + ex.ToString());
             }
             return ret;
         }


         public DataSet ExcelMarkUpload(int ADMBATCH, int ProgramType, int DegreeNo, string branchno)
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
                        };

                 ds = objDataAccessLayer.ExecuteDataSetSP("[dbo].[PKG_SP_GET_EXECL_MARK_UPLOAD]", objParams);
             }
             catch (Exception ex)
             {
                 throw new Exception(ex.Message);
             }

             return ds;
         }


         public DataSet GetMarkUpload(int ADMBATCH, int ProgramType, int DegreeNo, string branchno)
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
                        };

                 ds = objDataAccessLayer.ExecuteDataSetSP("[dbo].[PKG_SP_GET_MARK_UPLOADED_STUDENT]", objParams);
             }
             catch (Exception ex)
             {
                 throw new Exception(ex.Message);
             }

             return ds;
         }

         public int ExamMarkLock(int ADMBATCH, string AppicationId)
         {
             int ret = 0;
             try
             {
                 SQLHelper objSQL = new SQLHelper(_UAIMS_constr);
                 SqlParameter[] objPar = new SqlParameter[]
                        {
                           // new SqlParameter("@P_TBLBULKDATA",dt),
                            new SqlParameter("@P_ADMBATCH",ADMBATCH),                           
                            new SqlParameter("@P_APPLICATIONID",AppicationId),
                            //new SqlParameter("@P_IPADDRESS",ipAddress),
                            //new SqlParameter("@P_CREATEDBY",CreatedBy),
                            new SqlParameter("@P_OUTPUT",DbType.String)
                        };
                 objPar[objPar.Length - 1].Direction = ParameterDirection.Output;

                 object val = objSQL.ExecuteNonQuerySP("PKG_SP_MARK_ENTRY_LOCK", objPar, true);
                 if (val != null)
                 {
                     ret = Convert.ToInt16(val.ToString());
                 }
                 else
                 {
                     ret = -99;
                 }
             }
             catch (Exception ex)
             {
                 throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.AffiliatedController.Add_Fee_Defintion_For_Affiliated_ForExcel-> " + ex.ToString());
             }
             return ret;
         }

    }
}
