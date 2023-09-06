//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : SCHOLARSHIP                                                           
// PAGE NAME     : SCHOLAERSHIP CONTROLLER                                              
// CREATION DATE : 14-07-2015                                                         
// CREATED BY    : DIGEHS PATEL                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================



using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using BusinessLogicLayer.BusinessEntities.SCHOLARSHIP;



namespace BusinessLogicLayer.BusinessEntities
{

    public class ScholarshipController
    {
        private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        #region  ScholarshipEntry

      public int AddScholrshipEntry(StudentScholarship objStudentScholarship)
        {
            {
                int retStatus = 0;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                    SqlParameter[] objParams = null;

                    {
                        objParams = new SqlParameter[23];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudentScholarship.Id_No);
                        objParams[1] = new SqlParameter("@P_REGNO", objStudentScholarship.Reg_No);
                        objParams[2] = new SqlParameter("@P_DEGREENO", objStudentScholarship.Degree_No);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", objStudentScholarship.Branch_No);
                        objParams[4] = new SqlParameter("@P_ADMBATCH", objStudentScholarship.ADMBatch_No );
                        objParams[5] = new SqlParameter("@P_YEAR",objStudentScholarship.Year );
                        objParams[6] = new SqlParameter("@P_SHIFT",objStudentScholarship.Shift_No );
                        objParams[7] = new SqlParameter("@P_CATEGORYNO", objStudentScholarship.Category_No );
                        objParams[8] = new SqlParameter("@P_ADMCATEGORYNO", objStudentScholarship.Admcategory_No );
                        objParams[9] = new SqlParameter("@P_CONCESSIONNO", objStudentScholarship.Concession_No);
                        objParams[10] = new SqlParameter("@P_CONCESSION_REASON", objStudentScholarship.Concession_Reason);
                        objParams[11] = new SqlParameter("@P_AMOUNT", objStudentScholarship.Amount);
                        objParams[12] = new SqlParameter("@P_BILLNO", objStudentScholarship.Bill_No);
                        objParams[13] = new SqlParameter("@P_BILL_DT", objStudentScholarship.Bill_Date);
                        objParams[14] = new SqlParameter("@P_UA_NO", objStudentScholarship.Ua_No);
                        objParams[15] = new SqlParameter("@P_IP_ADDRESS", objStudentScholarship.IP_Address);
                        objParams[16] = new SqlParameter("@P_ENTRY_DATE", DateTime.Now);
                        objParams[17] = new SqlParameter("@P_COLLEGE_CODE",objStudentScholarship.College_Code );
                        objParams[18] = new SqlParameter("@P_REFUND_AMT",objStudentScholarship.Refund_Amount );
                        objParams[19] = new SqlParameter("@P_ADJUST_AMT",objStudentScholarship.Adjustment_Amount );
                        objParams[20] = new SqlParameter("@P_SCH_NO",objStudentScholarship.Sch_Type);
                        objParams[21] = new SqlParameter("@P_FARWARDEDTO", objStudentScholarship.Forwarede_To);
                       

                        objParams[22] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[22].Direction = ParameterDirection.Output;

                    };
                    if (objSQLHelper.ExecuteNonQuerySP("PKG_SCHOLARSHIP_STUDENT_SCHOLARSHIP_INSERT", objParams, false) != null)
                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                catch (Exception ex)
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ScholarshipController.AddScholrshipEntry-> " + ex.ToString());
                }

                return retStatus;

            }
        }
    
        public int UpdateScholrshipEntry(StudentScholarship objStudentScholarship)
        {
            {
                int retStatus = 0;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                    SqlParameter[] objParams = null;

                    {
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SCHOLIDNO",objStudentScholarship.ScholId_No );
                        objParams[1] = new SqlParameter("@P_BILL_DT", objStudentScholarship.Bill_Date);
                        objParams[2] = new SqlParameter("@P_SCH_NO", objStudentScholarship.Sch_Type);
                        objParams[3] = new SqlParameter("@P_FARWARDEDTO", objStudentScholarship.Forwarede_To);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;
                        
                    };
                    if (objSQLHelper.ExecuteNonQuerySP("PKG_SCHOLARSHIP_STUDENT_SCHOLARSHIP_UPDATE ", objParams, false) != null)
                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                catch (Exception ex)
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Phdexpertcontroller.AddPhdExpertData-> " + ex.ToString());
                }

                return retStatus;

            }
        }

        # endregion
               
        #region  ScholarshipModifyEntry

      public int AddScholrshipModifyEntry(StudentScholarshipEntry objModifyEntry)
      {
          {
              int retStatus = 0;
              try
              {
                  SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                  SqlParameter[] objParams = null;
                  {

                      objParams = new SqlParameter[24];
                      objParams[0] = new SqlParameter("@P_SCHOLIDNO", objModifyEntry.ScholId_No);
                      objParams[1] = new SqlParameter("@P_IDNO", objModifyEntry.Id_No);
                      objParams[2] = new SqlParameter("@P_REGNO", objModifyEntry.Reg_No);
                      objParams[3] = new SqlParameter("@P_DEGREENO", objModifyEntry.Degree_No);
                      objParams[4] = new SqlParameter("@P_BRANCHNO", objModifyEntry.Branch_No);
                      objParams[5] = new SqlParameter("@P_ADMBATCHNO", objModifyEntry.ADMBatch_No);
                      objParams[6] = new SqlParameter("@P_YEAR", objModifyEntry.Year);
                      objParams[7] = new SqlParameter("@P_CONCESSIONNO", objModifyEntry.Concession_No);
                      objParams[8] = new SqlParameter("@P_SCH_AMT", objModifyEntry.Sch_Amount);

                      objParams[9] = new SqlParameter("@P_BILL_NO", objModifyEntry.Bill_No);
                      objParams[10] = new SqlParameter("@P_UA_NO", objModifyEntry.Ua_No);
                      objParams[11] = new SqlParameter("@P_ENTRY_DATE", DateTime.Now);

                      objParams[12] = new SqlParameter("@P_COLLEGE_CODE",objModifyEntry.College_Code  );
                      objParams[13] = new SqlParameter("@P_REFUND_AMT", objModifyEntry.Refund_Amt);
                      objParams[14] = new SqlParameter("@P_SHIFTNO", objModifyEntry.Shift_No);

                      objParams[15] = new SqlParameter("@P_CATEGORYNO", objModifyEntry.Category_No);

                      objParams[16] = new SqlParameter("@P_BILL_DT", objModifyEntry.Bill_Date);

                      objParams[17] = new SqlParameter("@P_SCH_NO", objModifyEntry.Sch_Type);
                      objParams[18] = new SqlParameter("@P_PAYDATE", objModifyEntry.Pay_Date);
                      objParams[19] = new SqlParameter("@P_PAYAMT", objModifyEntry.PayAmt);
                      objParams[20] = new SqlParameter("@P_SANCTIONEDORDNO", objModifyEntry.SantionOrderno);
                      objParams[21] = new SqlParameter("@P_SANSACTIONYEAR", objModifyEntry.SanctionYear);
                      objParams[22] = new SqlParameter("@P_PARTICULAR", objModifyEntry.Paricular);
                      objParams[23] = new SqlParameter("@P_OUT", SqlDbType.Int);
                      objParams[23].Direction = ParameterDirection.Output;

                  };
                  if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUDENT_SCHOLARSHIP_ENTRY_INSERT", objParams, false) != null)
                      retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
              }
              catch (Exception ex)
              {
                  retStatus = Convert.ToInt32(CustomStatus.Error);
                  throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ScholarshipController.AddScholrshipEntry-> " + ex.ToString());
              }

              return retStatus;

          }
      }
      public int UpdateScholrshipModifyEntry(StudentScholarshipEntry objModifyEntry)
        {
            {
                int retStatus = 0;
                try
                {
                    SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                    SqlParameter[] objParams = null;
                    {
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SCHOLIDNO", objModifyEntry.ScholId_No);
                        objParams[1] = new SqlParameter("@P_PAYDATE", objModifyEntry.Bill_Date);
                        objParams[2] = new SqlParameter("@P_BILL_DT", objModifyEntry.Pay_Date);
                        objParams[3] = new SqlParameter("@P_PAYAMT", objModifyEntry.PayAmt);
                        objParams[4] = new SqlParameter("@P_SANCTIONEDORDNO", objModifyEntry.SantionOrderno);
                        objParams[5] = new SqlParameter("@P_SANSACTIONYEAR", objModifyEntry.SanctionYear);
                        objParams[6] = new SqlParameter("@P_PARTICULAR", objModifyEntry.Paricular);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;
                    };
                    if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_STUDENT_SCHOLARSHIP_ENTRY_UPDATE", objParams, false) != null)
                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                catch (Exception ex)
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                    throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Phdexpertcontroller.AddPhdExpertData-> " + ex.ToString());
                }

                return retStatus;

            }
        }

        # endregion

       #region  ScholarshipReport
       public int AddScholrshipNotice(StudentScholarshipEntry objModifyEntry)
       {
           {
               int retStatus = 0;
               try
               {
                   SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                   SqlParameter[] objParams = null;
                   {
                       objParams = new SqlParameter[9];
                       objParams[0] = new SqlParameter("@P_IDNO", objModifyEntry.Id_No);
                       objParams[1] = new SqlParameter("@P_DEGREENO", objModifyEntry.Degree_No);
                       objParams[2] = new SqlParameter("@P_BRANCHNO", objModifyEntry.Branch_No);
                       objParams[3] = new SqlParameter("@P_ADMBATCH", objModifyEntry.ADMBatch_No);
                       objParams[4] = new SqlParameter("@P_YEAR", objModifyEntry.Year);
                       objParams[5] = new SqlParameter("@P_SEMESTERNO", objModifyEntry.Semesterno);
                       objParams[6] = new SqlParameter("@P_CATEGORYNO", objModifyEntry.Category_No);
                       objParams[7] = new SqlParameter("@P_QUERYTYPE", 1);
                       objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                       objParams[8].Direction = ParameterDirection.Output;
                   };
                   if (objSQLHelper.ExecuteNonQuerySP("PKG_INS_DEL_SCHOLARSHIPPRINTNOTICE", objParams, false) != null)
                       retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
               }
               catch (Exception ex)
               {
                   retStatus = Convert.ToInt32(CustomStatus.Error);
                   throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ScholarshipController.AddScholrshipNotice-> " + ex.ToString());
               }
               return retStatus;
           }
       }
       public int DeleteScholrshipNotice(StudentScholarshipEntry objModifyEntry)
       {
           {
               int retStatus = 0;
               try
               {
                   SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                   SqlParameter[] objParams = null;
                   {
                       objParams = new SqlParameter[9];
                       objParams[0] = new SqlParameter("@P_IDNO", objModifyEntry.Id_No);
                       objParams[1] = new SqlParameter("@P_DEGREENO", objModifyEntry.Degree_No);
                       objParams[2] = new SqlParameter("@P_BRANCHNO", objModifyEntry.Branch_No);
                       objParams[3] = new SqlParameter("@P_ADMBATCH", objModifyEntry.ADMBatch_No);
                       objParams[4] = new SqlParameter("@P_YEAR", objModifyEntry.Year);
                       objParams[5] = new SqlParameter("@P_SEMESTERNO", objModifyEntry.Semesterno);
                       objParams[6] = new SqlParameter("@P_CATEGORYNO", objModifyEntry.Category_No);
                       objParams[7] = new SqlParameter("@P_QUERYTYPE", 2);
                       objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                       objParams[8].Direction = ParameterDirection.Output;
                   };
                   if (objSQLHelper.ExecuteNonQuerySP("PKG_INS_DEL_SCHOLARSHIPPRINTNOTICE", objParams, false) != null)
                       retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
               }
               catch (Exception ex)
               {
                   retStatus = Convert.ToInt32(CustomStatus.Error);
                   throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ScholarshipController.DleteScholrshipNotice-> " + ex.ToString());
               }
               return retStatus;
           }
       }
       public DataSet GetSCSTDetailStudent(StudentScholarshipEntry objModifyEntry)
       {
           DataSet ds = null;
           try
           {
               SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
               SqlParameter[] objParams = null;
               {
                   objParams = new SqlParameter[6];
                   objParams[0] = new SqlParameter("@P_DEGREENO", objModifyEntry.Degree_No);
                   objParams[1] = new SqlParameter("@P_ADMBATCH", objModifyEntry.ADMBatch_No);
                   objParams[2] = new SqlParameter("@P_YEAR", objModifyEntry.Year);
                   objParams[3] = new SqlParameter("@P_BRANCHNO", objModifyEntry.Branch_No);
                   objParams[4] = new SqlParameter("@P_SEMESTERNO", objModifyEntry.Semesterno);
                   objParams[5] = new SqlParameter("@P_CATEGORYNO", objModifyEntry.Category_No);
               };
               ds = objSQLHelper.ExecuteDataSetSP("PKG_SCHOLARSHIP_STUDENT_STSCLIST_REPORT", objParams);
           }
           catch (Exception ex)
           {
               throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CourseController.GetCoursesBySchemeNo-> " + ex.ToString());
           }

           return ds;
       }
       public int DeleteRow(string TableName, string Wherecondition)
       {
           int ret;
           try
           {
               SQLHelper objsqlhelper = new SQLHelper(_nitprm_constr);
               SqlParameter[] objParams = new SqlParameter[3];
               objParams[0] = new SqlParameter("@P_TABLENAME", TableName);

               if (!Wherecondition.Equals(string.Empty))
                   objParams[1] = new SqlParameter("@P_WHERECONDITION", Wherecondition);
               else
                   objParams[1] = new SqlParameter("@P_WHERECONDITION", DBNull.Value);

               objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
               objParams[2].Direction = ParameterDirection.Output;

               object obj = objsqlhelper.ExecuteNonQuerySP("PKG_UTILS_SP_SCH_DELETE", objParams, true);

               if (obj != null && obj.ToString().Equals("-99"))
               {
                   ret = Convert.ToInt32(CustomStatus.TransactionFailed);
               }
               else
               {
                   ret = Convert.ToInt32(CustomStatus.RecordDeleted);
               }
           }
           catch (Exception)
           {

               throw;
           }
           return ret;
       }
        # endregion
        public CustomStatus AddScholrshipEntry(StudentScholarshipEntry objFN)
        {
            throw new NotImplementedException();
        }

       
    }
}
