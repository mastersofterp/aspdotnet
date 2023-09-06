//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                        
// PAGE NAME     : RECIEPT TYPE CONTROLLER CLASS
// CREATION DATE : 14-MAY-2009                                                        
// CREATED BY    : AMIT YADAV
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class RecieptTypeController
    {
        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet GetRecieptTypes()
        {
            DataSet ds = null;
            try
            {
                SQLHelper dataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[0];

                ds = dataAccess.ExecuteDataSetSP("PKG_ACAD_ALL_RECIEPT_TYPE", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RecieptTypeController.GetRecieptTypes --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public int UpdatePaymenttypeofStudents(string idnos, string ptypes)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_connectionString);
                SqlParameter[] param = new SqlParameter[]
                            {                         
                            new SqlParameter("@P_IDNOS", idnos),
                            new SqlParameter("@P_PTYPE", ptypes),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)                        
                            };
                param[param.Length - 1].Direction = ParameterDirection.Output;
                object ret = objSqlHelper.ExecuteNonQuerySP("PKG_ACD_UPD_STUD_PAYMENT_TYPE", param, false);
                if (ret != null)
                    if (ret.ToString() != "-99")
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdatePaymenttypeofStudents-> " + ex.ToString());
            }
            return retStatus;
        }
        public DataSet GetStudentsForUpdatePaymentType(int Admbatchno, int Degreeno, int Branchno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] param = new SqlParameter[]
                            {
                                new SqlParameter("@P_ADMBATCHNO", Admbatchno),
                                new SqlParameter("@P_DEGREENO", Degreeno),
                                new SqlParameter("@P_BRANCHNO", Branchno),
                            };
                ds = objSQLHelper.ExecuteDataSetSP("GET_STUDENTS_FOR_UPDATE_PAYMENT_TYPE", param);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.HostelController.GetStudentsForUpdatePaymentType() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetRecieptTypeById(int recieptTypeId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper dataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@P_RCPTTYPENO", recieptTypeId);

                ds = dataAccess.ExecuteDataSetSP("PKG_ACAD_SINGLE_RECIEPT_TYPE", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RecieptTypeController.GetRecieptTypes --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }




        public int AddRecieptType(RecieptType recieptType)
        {
            int status = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper dataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {                
                    new SqlParameter("@P_RECIEPT_CODE", recieptType.RecieptCode),
                    new SqlParameter("@P_RECIEPT_TITLE", recieptType.RecieptTitle),
                    new SqlParameter("@P_BELONGS_TO", recieptType.BelongsTo),
                    new SqlParameter("@P_ACCNO", recieptType.AccountNumber),
                    new SqlParameter("@P_CNAME", recieptType.CompanyName),
                    new SqlParameter("@P_LINKED", recieptType.IsLinked),
                    new SqlParameter("@P_COLLEGE_CODE", recieptType.CollegeCode),
                    new SqlParameter("@P_ISADDMISSION", recieptType.isadmission)
                };
                object obj = dataAccess.ExecuteNonQuerySP("PKG_ACAD_INS_RECIEPT_TYPE", sqlParams, true);

                if (Convert.ToInt32(obj) == -99)
                {
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                    throw new IITMSException("Transaction Failed.");
                }
                else
                {
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RecieptTypeController.AddRecieptType --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }



        public int UpdateRecieptType(RecieptType recieptType)
        {
            int status = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper dataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_RCPTTYPENO", recieptType.RecieptTypeId),
                    new SqlParameter("@P_RECIEPT_CODE", recieptType.RecieptCode),
                    new SqlParameter("@P_RECIEPT_TITLE", recieptType.RecieptTitle),
                    new SqlParameter("@P_BELONGS_TO", recieptType.BelongsTo),
                    new SqlParameter("@P_ACCNO", recieptType.AccountNumber),
                    new SqlParameter("@P_CNAME", recieptType.CompanyName),
                    new SqlParameter("@P_LINKED", recieptType.IsLinked),
                    new SqlParameter("@P_COLLEGE_CODE", recieptType.CollegeCode),
                    new SqlParameter("@P_ISADDMISSION", recieptType.isadmission)
                };

                object obj = dataAccess.ExecuteNonQuerySP("PKG_ACAD_UPD_RECIEPT_TYPE", sqlParams, true);

                if (Convert.ToInt32(obj) == -99)
                {
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                    throw new IITMSException("Transaction Failed.");
                }
                else
                {
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RecieptTypeController.UpdateRecieptType --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

    }
}