//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : BATCH MASTER CONTROLLER                                              
// CREATION DATE : 02-SEPT-2009                                                         
// CREATED BY    : SANJAY RATNAPARKHI                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================


using System;
using System.Data;
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;

using IITMS.UAIMS.BusinessLayer.BusinessEntities;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{

    public class BatchController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddBatch(Batch objBatch, int ActiveStatus)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_BATCHNAME",objBatch.BatchName),
                    new SqlParameter("@P_SUBID", objBatch.SubId),
                    new SqlParameter("@P_COLLEGE_CODE", objBatch.CollegeCode),
                    new SqlParameter("@P_ACTIVE_STATUS", ActiveStatus),
                    new SqlParameter("@P_BATCHNO", status)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_BATCH_INSERT", sqlParams, true);

                if (Convert.ToInt32(obj) == -1001)
                {
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else if (Convert.ToInt32(obj) == 3)
                {
                    status = Convert.ToInt32(CustomStatus.RecordExist);
                }
                else
                {
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                }
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.AddBatch() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public int UpdateBatch(Batch objBatch, int ActiveStatus)
        {
            int status;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {                    
                    new SqlParameter("@P_BATCHNAME",objBatch.BatchName),
                    new SqlParameter("@P_SUBID", objBatch.SubId),
                    new SqlParameter("@P_COLLEGE_CODE", objBatch.CollegeCode),
                    new SqlParameter("@P_ACTIVE_STATUS", ActiveStatus),
                    new SqlParameter("@P_BATCHNO", objBatch.BatchNo)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_BATCH_UPDATE", sqlParams, true);

                if (Convert.ToInt32(obj) == -1001)
                {
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else if (Convert.ToInt32(obj) == 3)
                {
                    status = Convert.ToInt32(CustomStatus.RecordExist);
                }
                else
                {
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.UpdateBatch() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetAllBatch()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_BATCH_GET_ALL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetAllBatch() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public SqlDataReader GetBatchByNo(int batchNo)
        {
            SqlDataReader dr = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_BATCHNO", batchNo) };

                dr = objSQLHelper.ExecuteReaderSP("PKG_ACAD_BATCH_GET_BY_NO", objParams);
                
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetBatchByNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return dr;
        }

        public int Add_Update_HSSC_STUDNAME(int idnos, string HSSC_STUDNAME)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_IDNOS", idnos);
                if (HSSC_STUDNAME == null)
                    objParams[1] = new SqlParameter("@P_HSSC_STUDNAME", DBNull.Value);
                else
                    objParams[1] = new SqlParameter("@P_HSSC_STUDNAME", HSSC_STUDNAME);

                //objParams[1] = new SqlParameter("@P_PRNO", prNo);
                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_UPDATE_HSSC_STUDNAME", objParams, true);
                retStatus = Convert.ToInt32(ret);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.Add_Update_HSSC_STUDNAME -> " + ex.ToString());
            }
            return retStatus;
        }

        public int Add_Update_GradeCard_SerialNo(int idnos, string ORIGINAL_SERIAL_NO, string DUPLICATE_SERIAL_NO)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_IDNOS", idnos);
                if (ORIGINAL_SERIAL_NO == null)
                    objParams[1] = new SqlParameter("@P_ORIGINAL_SERIAL_NO", DBNull.Value);
                else
                    objParams[1] = new SqlParameter("@P_ORIGINAL_SERIAL_NO", ORIGINAL_SERIAL_NO);
                if (DUPLICATE_SERIAL_NO == null)
                    objParams[2] = new SqlParameter("@P_DUPLICATE_SERIAL_NO", DBNull.Value);
                else
                    objParams[2] = new SqlParameter("@P_DUPLICATE_SERIAL_NO", DUPLICATE_SERIAL_NO);

                //objParams[1] = new SqlParameter("@P_PRNO", prNo);
                objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_UPDATE_GRADECARD_SERIALNO", objParams, true);
                retStatus = Convert.ToInt32(ret);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.Add_Update_HSSC_STUDNAME -> " + ex.ToString());
            }
            return retStatus;
        }


        //-----------------------------------------------------------------add 02/03/2023 jay T.---------------------------------------------------

        public int AddPayTypeNo(string PaytypeName, string Remark, string CollegeCode, int ActiveStatus)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {

                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_PayTypeName",PaytypeName),
                    new SqlParameter("@P_REMARK", Remark),
                    new SqlParameter("@P_COLLEGE_CODE", CollegeCode),
                    new SqlParameter("@P_ACTIVE_STATUS", ActiveStatus),
                    new SqlParameter("@P_OUT", SqlDbType.Int),
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_PAYMENT_TYPE_INSERT", sqlParams, true);

                if (Convert.ToInt32(ret) == -99)
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else if (Convert.ToInt32(ret) == 1)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (Convert.ToInt32(ret) == -1001)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PhdController.InsertCommitteeData-> " + ex.ToString());
            }
            return retStatus;
        }
        //-------------------------------------------------------------------------------------------------------------
        public int UpdatePayTypeNo(string PaytypeName, string Remark, string CollegeCode, int ActiveStatus, int PaytypeNo)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {

                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_PayTypeName",PaytypeName),
                    new SqlParameter("@P_REMARK", Remark),
                    new SqlParameter("@P_COLLEGE_CODE", CollegeCode),
                    new SqlParameter("@P_ACTIVE_STATUS", ActiveStatus),
                     new SqlParameter("@P_PAYMENTTYPENO", PaytypeNo),
                    new SqlParameter("@P_OUT", SqlDbType.Int),
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_PAYMENT_TYPE_UPDATE", sqlParams, true);

                if (Convert.ToInt32(ret) == -99)
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else if (Convert.ToInt32(ret) == -1001)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PhdController.InsertCommitteeData-> " + ex.ToString());
            }
            return retStatus;

        }

        public DataSet GetAllPayType()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_PAYMENT_TYPE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetAllPayType() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }


        public SqlDataReader GetPaymentTypeByNo(int PaymentType)
        {
            SqlDataReader dr = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_PAYMENTTYPENO", PaymentType) };

                dr = objSQLHelper.ExecuteReaderSP("PKG_ACD_GET_PAYMENT_TYPE", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetBatchByNo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return dr;
        }



        
    }
}


