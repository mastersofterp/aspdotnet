//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                        
// PAGE NAME     : STANDARD FEE DEFINITION (CONTROLLER CLASS)                                                     
// CREATION DATE : 15-MAY-2009                                                        
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
using System.Web.UI;
using System.Web.UI.WebControls;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class StandardFeeController
    {
        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        //public DataSet GetFeeDefinition(string recieptCode,string CollegeId, string degreeNo, string batchNo, string paymentTypeNo)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper dataAccess = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[]
        //        {
        //            new SqlParameter("@P_RECIEPT_CODE", recieptCode),
        //            new SqlParameter("@P_COLLEGE_ID", CollegeId),
        //            new SqlParameter("@P_DEGREENO", degreeNo),
        //            new SqlParameter("@P_BATCHNO", batchNo),
        //            new SqlParameter("@P_PAYTYPENO", paymentTypeNo)
        //        };

        //        ds = dataAccess.ExecuteDataSetSP("PKG_ACAD_FEE_DEFINITION", sqlParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StandardFeeController.GetFeeDefinition --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}

        public DataSet GetFeeDefinition(string recieptCode, string CollegeId, string degreeNo, string branchno, string batchNo, string paymentTypeNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper dataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_RECIEPT_CODE", recieptCode),
                    new SqlParameter("@P_COLLEGE_ID", CollegeId),
                    new SqlParameter("@P_DEGREENO", degreeNo),
                    new SqlParameter("@P_BRANCHNO", branchno),//************
                    new SqlParameter("@P_BATCHNO", batchNo),
                    new SqlParameter("@P_PAYTYPENO", paymentTypeNo)
                };

                ds = dataAccess.ExecuteDataSetSP("PKG_ACAD_FEE_DEFINITION", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StandardFeeController.GetFeeDefinition --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetFeeDefinition(string feeCatNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper dataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] { new SqlParameter("@P_FEECATNO", feeCatNo) };
                
                ds = dataAccess.ExecuteDataSetSP("PKG_ACAD_FEE_DEFINITION_BY_CATNO", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StandardFeeController.GetFeeDefinition --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        ////public DataSet GetFeesItemsForListbox(string recieptCode, int degreeNo, int batchNo, int payTypeNo)
        ////{
        ////    DataSet ds = null;
        ////    try
        ////    {
        ////        SQLHelper dataAccess = new SQLHelper(_connectionString);
        ////        SqlParameter[] sqlParams = new SqlParameter[]
        ////        {
        ////            new SqlParameter("@P_RECIEPT_CODE", recieptCode),
        ////            new SqlParameter("@P_DEGREENO", degreeNo),
        ////            new SqlParameter("@P_BATCHNO", batchNo),
        ////            new SqlParameter("@P_PAYTYPENO", payTypeNo)
        ////        };
        ////        ds = dataAccess.ExecuteDataSetSP("PKG_ACAD_FEE_DESCRIPTION", sqlParams);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StandardFeeController.GetFeesItemsForListbox --> " + ex.Message + " " + ex.StackTrace);
        ////    }
        ////    return ds;
        ////}

        public DataSet GetFeesItemsForListbox(string recieptCode, int collegeId, int degreeNo, int branchno, int batchNo, int payTypeNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper dataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_RECIEPT_CODE", recieptCode),
                    new SqlParameter("@P_COLLEGEID", collegeId),
                    new SqlParameter("@P_DEGREENO", degreeNo),
                    new SqlParameter("@P_BRANCHNO", branchno),
                    new SqlParameter("@P_BATCHNO", batchNo),
                    new SqlParameter("@P_PAYTYPENO", payTypeNo)
                };
                ds = dataAccess.ExecuteDataSetSP("PKG_ACAD_FEE_DESCRIPTION", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StandardFeeController.GetFeesItemsForListbox --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        ////public int MaintainStandardFeeItem(ref StandardFee feeItem)
        ////{
        ////    int status = Convert.ToInt32(CustomStatus.Others);
        ////    try
        ////    {
        ////        SqlParameter[] sqlParams = new SqlParameter[]
        ////        {
        ////            new SqlParameter("@P_COLLEGE_CODE", feeItem.CollegeCode),
        ////            new SqlParameter("@P_FEE_HEAD", feeItem.FeeHead),
        ////            new SqlParameter("@P_FEE_DESCRIPTION", feeItem.FeeDesc),
        ////            new SqlParameter("@P_RECIEPT_CODE", feeItem.RecieptCode),
        ////            new SqlParameter("@P_SRNO", feeItem.SerialNo),
        ////            new SqlParameter("@P_BATCHNO", feeItem.BatchNo),
        ////            new SqlParameter("@P_DEGREENO", feeItem.DegreeNo),
        ////            new SqlParameter("@P_COLLEGE_ID",feeItem.CollegeId),
        ////            new SqlParameter("@P_PAYTYPENO", feeItem.PaymentTypeNo),
        ////            new SqlParameter("@P_SEMESTER1", feeItem.Sem1_Fee),
        ////            new SqlParameter("@P_SEMESTER2", feeItem.Sem2_Fee),
        ////            new SqlParameter("@P_SEMESTER3", feeItem.Sem3_Fee),
        ////            new SqlParameter("@P_SEMESTER4", feeItem.Sem4_Fee),
        ////            new SqlParameter("@P_SEMESTER5", feeItem.Sem5_Fee),
        ////            new SqlParameter("@P_SEMESTER6", feeItem.Sem6_Fee),
        ////            new SqlParameter("@P_SEMESTER7", feeItem.Sem7_Fee),
        ////            new SqlParameter("@P_SEMESTER8", feeItem.Sem8_Fee),
        ////            new SqlParameter("@P_SEMESTER9", feeItem.Sem9_Fee),
        ////            new SqlParameter("@P_SEMESTER10", feeItem.Sem10_Fee),
        ////            new SqlParameter("@P_SEMESTER11", feeItem.Sem11_Fee),
        ////            new SqlParameter("@P_SEMESTER12", feeItem.Sem12_Fee),
        ////            new SqlParameter("@P_FEE_CAT_NO", feeItem.FeeCatNo)
        ////        };
        ////        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

        ////        SQLHelper dataAccess = new SQLHelper(_connectionString);
        ////        object obj = dataAccess.ExecuteNonQuerySP("PKG_ACAD_INS_STDFEES", sqlParams, true);

        ////        if (obj != null) feeItem.FeeCatNo = Convert.ToInt32(obj);
                
        ////        status = Convert.ToInt32(CustomStatus.RecordSaved);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        status = Convert.ToInt32(CustomStatus.Error);
        ////        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StandardFeeController.MaintainStandardFeeItem --> " + ex.Message + " " + ex.StackTrace);
        ////    }
        ////    return status;
        ////}

        /////public int MaintainStandardFeeItem(ref StandardFee feeItem)
        //public int MaintainStandardFeeItem(StandardFee feeItem)//*****************
        //{
        //    int status = Convert.ToInt32(CustomStatus.Others);
        //    try
        //    {
        //        SqlParameter[] sqlParams = new SqlParameter[]
        //        {
        //            new SqlParameter("@P_COLLEGE_CODE", feeItem.CollegeCode),
        //            new SqlParameter("@P_FEE_HEAD", feeItem.FeeHead),
        //            new SqlParameter("@P_FEE_DESCRIPTION", feeItem.FeeDesc),
        //            new SqlParameter("@P_RECIEPT_CODE", feeItem.RecieptCode),
        //            new SqlParameter("@P_SRNO", feeItem.SerialNo),
        //            new SqlParameter("@P_BATCHNO", feeItem.BatchNo),
        //            new SqlParameter("@P_DEGREENO", feeItem.DegreeNo),
        //            new SqlParameter("@P_BRANCHNO", feeItem.Branchno),//***********
        //            new SqlParameter("@P_COLLEGE_ID",feeItem.CollegeId),
        //            new SqlParameter("@P_PAYTYPENO", feeItem.PaymentTypeNo),
        //            new SqlParameter("@P_SEMESTER1", feeItem.Sem1_Fee),
        //            new SqlParameter("@P_SEMESTER2", feeItem.Sem2_Fee),
        //            new SqlParameter("@P_SEMESTER3", feeItem.Sem3_Fee),
        //            new SqlParameter("@P_SEMESTER4", feeItem.Sem4_Fee),
        //            new SqlParameter("@P_SEMESTER5", feeItem.Sem5_Fee),
        //            new SqlParameter("@P_SEMESTER6", feeItem.Sem6_Fee),
        //            new SqlParameter("@P_SEMESTER7", feeItem.Sem7_Fee),
        //            new SqlParameter("@P_SEMESTER8", feeItem.Sem8_Fee),
        //            new SqlParameter("@P_SEMESTER9", feeItem.Sem9_Fee),
        //            new SqlParameter("@P_SEMESTER10", feeItem.Sem10_Fee),
        //            new SqlParameter("@P_SEMESTER11", feeItem.Sem11_Fee),
        //            new SqlParameter("@P_SEMESTER12", feeItem.Sem12_Fee),
        //            new SqlParameter("@P_FEE_CAT_NO", feeItem.FeeCatNo)
        //        };
        //        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

        //        SQLHelper dataAccess = new SQLHelper(_connectionString);
        //        object obj = dataAccess.ExecuteNonQuerySP("PKG_ACAD_INS_STDFEES", sqlParams, true);

        //        ////if (obj != null) feeItem.FeeCatNo = Convert.ToInt32(obj);
        //        ////status = Convert.ToInt32(CustomStatus.RecordSaved);

        //        if (obj != null)
        //        {
        //            if (obj.ToString() == "1")
        //            {
        //                status = Convert.ToInt32(CustomStatus.RecordSaved);
        //            }
        //            else if (obj.ToString() == "2")
        //            {
        //                status = Convert.ToInt32(CustomStatus.TransactionFailed);
        //            }
        //        }
        //        else
        //        {
        //            status = Convert.ToInt32(CustomStatus.TransactionFailed);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        status = Convert.ToInt32(CustomStatus.Error);
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StandardFeeController.MaintainStandardFeeItem --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return status;
        //}

        public int MaintainStandardFeeItem(ref StandardFee feeItem)//*****************
        {
            int status = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_COLLEGE_CODE", feeItem.CollegeCode),
                    new SqlParameter("@P_FEE_HEAD", feeItem.FeeHead),
                    new SqlParameter("@P_FEE_DESCRIPTION", feeItem.FeeDesc),
                    new SqlParameter("@P_RECIEPT_CODE", feeItem.RecieptCode),
                    new SqlParameter("@P_SRNO", feeItem.SerialNo),
                    new SqlParameter("@P_BATCHNO", feeItem.BatchNo),
                    new SqlParameter("@P_DEGREENO", feeItem.DegreeNo),
                    new SqlParameter("@P_BRANCHNO", feeItem.Branchno),//***********
                    new SqlParameter("@P_COLLEGE_ID",feeItem.CollegeId),
                    new SqlParameter("@P_PAYTYPENO", feeItem.PaymentTypeNo),
                    new SqlParameter("@P_SEMESTER1", feeItem.Sem1_Fee),
                    new SqlParameter("@P_SEMESTER2", feeItem.Sem2_Fee),
                    new SqlParameter("@P_SEMESTER3", feeItem.Sem3_Fee),
                    new SqlParameter("@P_SEMESTER4", feeItem.Sem4_Fee),
                    new SqlParameter("@P_SEMESTER5", feeItem.Sem5_Fee),
                    new SqlParameter("@P_SEMESTER6", feeItem.Sem6_Fee),
                    new SqlParameter("@P_SEMESTER7", feeItem.Sem7_Fee),
                    new SqlParameter("@P_SEMESTER8", feeItem.Sem8_Fee),
                    new SqlParameter("@P_SEMESTER9", feeItem.Sem9_Fee),
                    new SqlParameter("@P_SEMESTER10", feeItem.Sem10_Fee),
                    new SqlParameter("@P_SEMESTER11", feeItem.Sem11_Fee),
                    new SqlParameter("@P_SEMESTER12", feeItem.Sem12_Fee),
                    new SqlParameter("@P_UANO",   Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"])),
                    new SqlParameter("@P_IPADDRESS",  System.Web.HttpContext.Current.Session["ipAddress"].ToString()),
                    new SqlParameter("@P_FEE_CAT_NO", feeItem.FeeCatNo)
                   
                  
                   
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                SQLHelper dataAccess = new SQLHelper(_connectionString);
                object obj = dataAccess.ExecuteNonQuerySP("PKG_ACAD_INS_STDFEES", sqlParams, true);

                ////if (obj != null) feeItem.FeeCatNo = Convert.ToInt32(obj);
                ////status = Convert.ToInt32(CustomStatus.RecordSaved);

                if (obj.ToString() != "0" && obj != null)
                {
                    feeItem.FeeCatNo = Convert.ToInt32(obj);
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (obj.ToString() == "0" || obj == null)
                {
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                }

                ////if (obj != null)
                ////{
                ////    if (obj.ToString() == "1")
                ////    {
                ////        status = Convert.ToInt32(CustomStatus.RecordSaved);
                ////    }
                ////    else if (obj.ToString() == "2")
                ////    {
                ////        status = Convert.ToInt32(CustomStatus.TransactionFailed);
                ////    }
                ////}
                ////else
                ////{
                ////    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                ////}
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StandardFeeController.MaintainStandardFeeItem --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetStdFeesForListbox(string recieptCode, int degreeNo, int roomCapacity)
        {
            DataSet ds = null;
            try
            {
                SQLHelper dataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_RECIEPT_CODE", recieptCode),
                    new SqlParameter("@P_DEGREENO", degreeNo),
                    new SqlParameter("@P_ROOM_CAPACITY", roomCapacity)
                };
                ds = dataAccess.ExecuteDataSetSP("PKG_HOSTEL_GET_STANDARD_FEE_DESC", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StandardFeeController.GetFeesItemsForListbox --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        //added by Shubham Barke for Hostel
        public DataSet HostelStandardFee(string recieptCode, string degreeNo, string Session, string HostelType, string Hostel)
        {
            DataSet ds = null;
            try
            {
                SQLHelper dataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_RECIEPT_CODE", recieptCode),
                    new SqlParameter("@P_DEGREENO", degreeNo),
                    //new SqlParameter("@P_ROOM_CAPACITY", roomCapacity),
                    new SqlParameter("@P_SESSIONNO",Session),
                    new SqlParameter("@P_HOSTEL_TYPE",HostelType),
                    new SqlParameter("@P_HOSTEL_NO",Hostel)
                };

                ds = dataAccess.ExecuteDataSetSP("PKG_HOSTEL_GET_STANDARD_FEE", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StandardFeeController.GetFeeDefinition --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetHostelStandardFee(string recieptCode, string degreeNo, string roomCapacity)
        {
            DataSet ds = null;
            try
            {
                SQLHelper dataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_RECIEPT_CODE", recieptCode),
                    new SqlParameter("@P_DEGREENO", degreeNo),
                    new SqlParameter("@P_ROOM_CAPACITY", roomCapacity)
                };

                ds = dataAccess.ExecuteDataSetSP("PKG_HOSTEL_GET_STANDARD_FEE", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StandardFeeController.GetFeeDefinition --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetHostelStandardFee(string feeCatNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper dataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] { new SqlParameter("@P_FEE_CAT_NO", feeCatNo) };

                ds = dataAccess.ExecuteDataSetSP("PKG_HOSTEL_GET_STANDARD_FEE_BY_NO", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StandardFeeController.GetFeeDefinition --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int AddUpdateStandardFees(ref StandardFee feeItem)
        {
            int status = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_FEE_HEAD", feeItem.FeeHead),
                    new SqlParameter("@P_FEE_DESCRIPTION", feeItem.FeeDesc),
                    new SqlParameter("@P_SRNO", feeItem.SerialNo),
                    new SqlParameter("@P_RECIEPT_CODE", feeItem.RecieptCode),
                    new SqlParameter("@P_DEGREENO", feeItem.DegreeNo),
                    new SqlParameter("@P_ROOM_CAPACITY", feeItem.RoomCapacity),
                    new SqlParameter("@P_SEMESTER1", feeItem.Sem1_Fee),
                    new SqlParameter("@P_COLLEGE_CODE", feeItem.CollegeCode),
                    new SqlParameter("@P_FEE_CAT_NO", feeItem.FeeCatNo)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                SQLHelper dataAccess = new SQLHelper(_connectionString);
                object obj = dataAccess.ExecuteNonQuerySP("PKG_HOSTEL_ADD_UPDATE_STD_FEES", sqlParams, true);

                if (obj != null && obj.ToString() != string.Empty) 
                    feeItem.FeeCatNo = Convert.ToInt32(obj);

                status = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StandardFeeController.MaintainStandardFeeItem --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

         /// <summary>
        /// ADDED BY DILEEP
        /// ON 17042020
        /// </summary>
        /// <param name="feeItem"></param>
        /// <returns></returns>
        public int MaintainStandardFeeItemForCopy(ref StandardFee feeItem)//*****************
        {
            int status = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_COLLEGE_CODE", feeItem.CollegeCode),
                    new SqlParameter("@P_FEE_HEAD", feeItem.FeeHead),
                    new SqlParameter("@P_FEE_DESCRIPTION", feeItem.FeeDesc),
                    new SqlParameter("@P_RECIEPT_CODE", feeItem.RecieptCode),
                    new SqlParameter("@P_SRNO", feeItem.SerialNo),
                    new SqlParameter("@P_BATCHNO", feeItem.BatchNo),
                    new SqlParameter("@P_DEGREENO", feeItem.DegreeNo),
                    new SqlParameter("@P_BRANCHNO", feeItem.Branchno),//***********
                    new SqlParameter("@P_COLLEGE_ID",feeItem.CollegeId),
                    new SqlParameter("@P_PAYTYPENO", feeItem.PaymentTypeNo),
                    new SqlParameter("@P_SEMESTER1", feeItem.Sem1_Fee),
                    new SqlParameter("@P_SEMESTER2", feeItem.Sem2_Fee),
                    new SqlParameter("@P_SEMESTER3", feeItem.Sem3_Fee),
                    new SqlParameter("@P_SEMESTER4", feeItem.Sem4_Fee),
                    new SqlParameter("@P_SEMESTER5", feeItem.Sem5_Fee),
                    new SqlParameter("@P_SEMESTER6", feeItem.Sem6_Fee),
                    new SqlParameter("@P_SEMESTER7", feeItem.Sem7_Fee),
                    new SqlParameter("@P_SEMESTER8", feeItem.Sem8_Fee),
                    new SqlParameter("@P_SEMESTER9", feeItem.Sem9_Fee),
                    new SqlParameter("@P_SEMESTER10", feeItem.Sem10_Fee),
                    new SqlParameter("@P_SEMESTER11", feeItem.Sem11_Fee),
                    new SqlParameter("@P_SEMESTER12", feeItem.Sem12_Fee),
                    new SqlParameter("@P_FEE_CAT_NO", feeItem.FeeCatNo)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                SQLHelper dataAccess = new SQLHelper(_connectionString);
                object obj = dataAccess.ExecuteNonQuerySP("PKG_ACAD_INS_STDFEES_FOR_BULK", sqlParams, true);

                ////if (obj != null) feeItem.FeeCatNo = Convert.ToInt32(obj);
                ////status = Convert.ToInt32(CustomStatus.RecordSaved);

                if (obj.ToString() != "0" && obj != null)
                {
                    feeItem.FeeCatNo = Convert.ToInt32(obj);
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (obj.ToString() == "0" || obj == null)
                {
                    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                }

                ////if (obj != null)
                ////{
                ////    if (obj.ToString() == "1")
                ////    {
                ////        status = Convert.ToInt32(CustomStatus.RecordSaved);
                ////    }
                ////    else if (obj.ToString() == "2")
                ////    {
                ////        status = Convert.ToInt32(CustomStatus.TransactionFailed);
                ////    }
                ////}
                ////else
                ////{
                ////    status = Convert.ToInt32(CustomStatus.TransactionFailed);
                ////}
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StandardFeeController.MaintainStandardFeeItemForBulk --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }


        /// <summary>
        /// ADDED BY DILEEP
        /// ON 17042020
        /// </summary>
        /// <param name="feeCatNo"></param>
        /// <returns></returns>
        public DataSet GetFeeDefinitionForBulk(string recieptCode, string CollegeId, string degreeNo, string branchno, string batchNo, string paymentTypeNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper dataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_RECIEPT_CODE", recieptCode),
                    new SqlParameter("@P_COLLEGE_ID", CollegeId),
                    new SqlParameter("@P_DEGREENO", degreeNo),
                    new SqlParameter("@P_BRANCHNO", branchno),//************
                    new SqlParameter("@P_BATCHNO", batchNo),
                    new SqlParameter("@P_PAYTYPENO", paymentTypeNo)
                };

                ds = dataAccess.ExecuteDataSetSP("PKG_ACAD_FEE_DEFINITION_FORBULK", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StandardFeeController.GetFeeDefinitionForBulk --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// ADDED BY DILEEP 
        /// ON 17042020
        /// </summary>
        /// <param name="recieptCode"></param>
        /// <param name="degreeNo"></param>
        /// <param name="branchno"></param>
        /// <param name="batchNo"></param>
        /// <param name="payTypeNo"></param>
        /// <returns></returns>
        public DataSet GetFeesItemsForListboxForBulk(string recieptCode, int degreeNo, int branchno, int batchNo, int payTypeNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper dataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_RECIEPT_CODE", recieptCode),
                    new SqlParameter("@P_DEGREENO", degreeNo),
                    new SqlParameter("@P_BRANCHNO", branchno),
                    new SqlParameter("@P_BATCHNO", batchNo),
                    new SqlParameter("@P_PAYTYPENO", payTypeNo)
                };
                ds = dataAccess.ExecuteDataSetSP("PKG_ACAD_FEE_DESCRIPTION_FORBULK", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StandardFeeController.GetFeesItemsForListboxForBulk --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Added By Dileep Kare on 10.08.2021 
        /// for Copy the Standard Fees
        /// </summary>
        /// <param name="Receipt_Code"></param>
        /// <param name="AdmBatchFrom"></param>
        /// <param name="AdmBatchTo"></param>
        /// <returns></returns>
        public int CopyStandardFees(string Receipt_Code, int AdmBatchFrom, int AdmBatchTo)
        {
            int status = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[4];
                sqlParams[0] = new SqlParameter("@P_RECEIPT_TYPE", Receipt_Code);
                sqlParams[1] = new SqlParameter("@P_FROM_BATCHNO", AdmBatchFrom);
                sqlParams[2] = new SqlParameter("@P_TO_BATCHNO", AdmBatchTo);
                sqlParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                sqlParams[3].Direction = ParameterDirection.Output;
                SQLHelper dataAccess = new SQLHelper(_connectionString);
                object obj = dataAccess.ExecuteNonQuerySP("PKG_COPY_STANDARD_FEE_FROM_ADMBATCH", sqlParams, true);
                if (obj.ToString() != "0" && obj != null && obj.ToString() != "-99")
                {
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StandardFeeController.CopyStandardFees --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
        /// <summary>
        /// Added by Rishabh B. on 10.11.2021
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public DataSet GetFeeDefinition_Excel(string batchNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper dataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_BATCHNO", batchNo),
                };

                ds = dataAccess.ExecuteDataSetSP("PKG_ACAD_FEE_DEFINITION_REPORT_BULK_EXCEL", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StandardFeeController.GetFeeDefinition_Excel --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int CopyStandardFeesBranchWise(string Receipt_Code, int AdmBatchFrom, int AdmBatchTo, int CollegeidFrom, int CollegeidTo, int DegreenoFrom, int DegreenoTo, int BranchnoFrom, string BranchnoTo, int ChkOveride)
        {
            int status = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SqlParameter[] sqlParams = new SqlParameter[13];
                sqlParams[0] = new SqlParameter("@P_RECEIPT_TYPE", Receipt_Code);
                sqlParams[1] = new SqlParameter("@P_FROM_BATCHNO", AdmBatchFrom);
                sqlParams[2] = new SqlParameter("@P_TO_BATCHNO", AdmBatchTo);
                sqlParams[3] = new SqlParameter("@P_FROM_COLLEGEID", CollegeidFrom);
                sqlParams[4] = new SqlParameter("@P_TO_COLLEGEID", CollegeidTo);
                sqlParams[5] = new SqlParameter("@P_FROM_DEGREENO", DegreenoFrom);
                sqlParams[6] = new SqlParameter("@P_TO_DEGREENO", DegreenoTo);
                sqlParams[7] = new SqlParameter("@P_FROM_BRANCH", BranchnoFrom);
                sqlParams[8] = new SqlParameter("@P_TO_BRANCH", BranchnoTo);
                sqlParams[9] = new SqlParameter("@P_CHKOVERRIDE", ChkOveride);
                sqlParams[10] = new SqlParameter("@P_UANO", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                sqlParams[11] = new SqlParameter("@P_IPADDRESS", System.Web.HttpContext.Current.Session["ipAddress"].ToString());
                sqlParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                sqlParams[12].Direction = ParameterDirection.Output;
                SQLHelper dataAccess = new SQLHelper(_connectionString);
                object obj = dataAccess.ExecuteNonQuerySP("PKG_COPY_STANDARD_FEE_FROM_MULTI_BRANCHWISE", sqlParams, true);
                if (obj.ToString() != "0" && obj != null && obj.ToString() != "-99")
                {
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StandardFeeController.CopyStandardFees --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        } 
    }
}