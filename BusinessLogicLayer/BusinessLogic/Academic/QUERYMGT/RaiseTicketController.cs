using IITMS.SQLServer.SQLDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class RaiseTicketController
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                //public int AddRaiseTicket(RaiseTicket objRT)
                //{
                //    int status = -99;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] sqlParams = new SqlParameter[14];

                //        sqlParams[0] = new SqlParameter("@P_QMRequestTypeID", objRT.QMRequestTypeID);
                //        sqlParams[1] = new SqlParameter("@P_QMRequestCategoryID", objRT.QMRequestCategoryID);
                //        sqlParams[2] = new SqlParameter("@P_QMRequestSubCategoryID", objRT.QMRequestSubCategoryID);
                //        sqlParams[3] = new SqlParameter("@P_IsPaidService", objRT.IsPaidService);
                //        sqlParams[4] = new SqlParameter("@P_PaidServiceAmount", objRT.PaidServiceAmount);
                //        sqlParams[5] = new SqlParameter("@P_IsEmergencyService", objRT.IsEmergencyService);
                //        sqlParams[6] = new SqlParameter("@P_EmergencyServiceAmount", objRT.EmergencyServiceAmount);
                //        sqlParams[7] = new SqlParameter("@P_TicketStatus", objRT.TicketStatus);
                //        sqlParams[8] = new SqlParameter("@P_TicketDescription", objRT.TicketDescription);
                //        sqlParams[9] = new SqlParameter("@P_Filepath",objRT.Filepath );
                //        sqlParams[10] = new SqlParameter("@P_FeedBack", objRT.FeedBack);
                //        sqlParams[11] = new SqlParameter("@P_CreatedBy", objRT.CreatedBy);
                //        sqlParams[12] = new SqlParameter("@P_Filename", objRT.Filename);
                //        sqlParams[13] = new SqlParameter("@P_Out", SqlDbType.Int);
                //        sqlParams[13].Direction = ParameterDirection.Output;

                //        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_QM_RaiseTicket_INSERT", sqlParams, true);
                //        status = (Int32)obj;
                //    }
                //    catch (Exception ex)
                //    {
                //        status = -99;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddAchievementMaster() --> " + ex.Message + " " + ex.StackTrace);
                //    }
                //    return status;

                //}


                public int AddRaiseTicket(RaiseTicket objRT, int stat)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[17];

                        sqlParams[0] = new SqlParameter("@P_QMRequestTypeID", objRT.QMRequestTypeID);
                        sqlParams[1] = new SqlParameter("@P_QMRequestCategoryID", objRT.QMRequestCategoryID);
                        sqlParams[2] = new SqlParameter("@P_QMRequestSubCategoryID", objRT.QMRequestSubCategoryID);
                        sqlParams[3] = new SqlParameter("@P_IsPaidService", objRT.IsPaidService);
                        sqlParams[4] = new SqlParameter("@P_PaidServiceAmount", objRT.PaidServiceAmount);
                        sqlParams[5] = new SqlParameter("@P_IsEmergencyService", objRT.IsEmergencyService);
                        sqlParams[6] = new SqlParameter("@P_EmergencyServiceAmount", objRT.EmergencyServiceAmount);
                        sqlParams[7] = new SqlParameter("@P_TicketStatus", objRT.TicketStatus);
                        sqlParams[8] = new SqlParameter("@P_TicketDescription", objRT.TicketDescription);
                        sqlParams[9] = new SqlParameter("@P_Filepath", objRT.Filepath);
                        sqlParams[10] = new SqlParameter("@P_FeedBack", objRT.FeedBack);
                        sqlParams[11] = new SqlParameter("@P_CreatedBy", objRT.CreatedBy);
                        sqlParams[12] = new SqlParameter("@P_Filename", objRT.Filename);
                        sqlParams[13] = new SqlParameter("@P_payment_stat", stat);
                        sqlParams[14] = new SqlParameter("@P_IsStudentRemark", objRT.IsStudentRemark); //
                        sqlParams[15] = new SqlParameter("@P_InfoReq", objRT.RequiredInformation); //
                        sqlParams[16] = new SqlParameter("@P_Out", SqlDbType.Int);
                        sqlParams[16].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_QM_RaiseTicket_Payment_INSERT", sqlParams, true);
                        status = (Int32)obj;
                    }
                    catch (Exception ex)
                    {
                        status = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddAchievementMaster() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;

                }




                public DataSet GETDATA( int UNO)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_UA_NO", UNO) };
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_QM_RaiseTicket_GETDATA", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetAllAchievement() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }


                public DataSet GetDataBYID(int ID)
                {
                    DataSet ds = null;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_OUTPUT", ID) };

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_QM_RaiseTicket_GETDATABYID", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetAchievementNo() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return ds;
                }


                public int AddRaiseTicket1(RaiseTicket objRT)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[4];

                        sqlParams[0] = new SqlParameter("@P_QMRaiseTicketID", objRT.QMRaiseTicketID);
                        sqlParams[1] = new SqlParameter("@P_FeedBack", objRT.FeedBack);
                        sqlParams[2] = new SqlParameter("@P_FeedBackPoints", objRT.FeedBackPoints);
                        sqlParams[3] = new SqlParameter("@P_Out", SqlDbType.Int);
                        sqlParams[3].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_QM_RaiseTicket_INSERT_FeedBack", sqlParams, true);
                        status = (Int32)obj;
                    }
                    catch (Exception ex)
                    {
                        status = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddAchievementMaster() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;

                }





                public int SubmitFees(RaiseTicket objRT, int PAYMENTTYPE, int GATEWAYID, string ReferenceNo)
                {
                    int retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_USERNO", objRT.UserNo);
                        objParams[1] = new SqlParameter("@P_AMOUNT", objRT.Amount);
                        objParams[2] = new SqlParameter("@P_TOTALAMOUNT", objRT.TotalAmount);
                        objParams[3] = new SqlParameter("@P_TRANSDATE", objRT.TransDate);
                        objParams[4] = new SqlParameter("@P_ORDER_ID", objRT.OrderID);
                        objParams[5] = new SqlParameter("@P_PAYMENTTYPE", PAYMENTTYPE);
                        objParams[6] = new SqlParameter("@P_GATEWAYID", GATEWAYID);
                        objParams[7] = new SqlParameter("@P_ReferenceNo", ReferenceNo);

                        objParams[8] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_QM_FEES_LOG", objParams, false);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(IITMS.UAIMS.CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.Message + " " + ex.StackTrace);
                    }
                    return retStatus;
                }


                public DataSet GetOnlineTrasactionOnlineOrderID(string userno, string orderid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_USERNO", userno);
                        objParams[1] = new SqlParameter("@P_ORDER_ID", orderid);
                        ds = objDataAccess.ExecuteDataSetSP("PKG_QM_ORDER_ID", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetSingleSession-> " + ex.ToString());
                    }
                    return ds;
                }



                public int OnlinePayment_updatePAyment(string order_id, string transaction_id, string response_code, string amount, string hash)
                {
                    int countrno = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSqlhelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlparam = null;
                        {
                            sqlparam = new SqlParameter[6];
                            sqlparam[0] = new SqlParameter("@P_ORDER_ID", order_id);
                            sqlparam[1] = new SqlParameter("@P_TRANSACTION_ID", transaction_id);
                            sqlparam[2] = new SqlParameter("@P_TRANSACTIONSTATUS", response_code);
                            sqlparam[3] = new SqlParameter("@P_AMOUNT", amount);
                            sqlparam[4] = new SqlParameter("@P_HASH", hash);
                            sqlparam[5] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                            sqlparam[5].Direction = ParameterDirection.Output;
                        };
                        //sqlparam[sqlparam.Length - 1].Direction = ParameterDirection.Output;     
                        object studid = objSqlhelper.ExecuteNonQuerySP("PKG_QM_UPD_ONLINE-PAYMENT", sqlparam, true);

                        if (Convert.ToInt32(studid) == -99)
                        {
                            countrno = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                            countrno = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.OnlinePayment_updatePAyment() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return countrno;
                }

                public int updatePAymentStatus(string refno)
                {
                    int countrno = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSqlhelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ReferenceNo", refno);

                        object studid = objSqlhelper.ExecuteNonQuerySP("PKG_QM_UpdatePaymentStatus", objParams, false);


                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.OnlinePayment_updatePAyment() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return countrno;
                }

                public int updateRaiseTicketResponse(int tktno, RaiseTicket objRT)
                {
                    int status = -99;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[5];

                        sqlParams[0] = new SqlParameter("@P_QMRaiseTicketID", tktno);
                        sqlParams[1] = new SqlParameter("@P_TicketDesc", objRT.TicketDescription);
                        sqlParams[2] = new SqlParameter("@P_Filepath", objRT.Filepath);
                        sqlParams[3] = new SqlParameter("@P_Filename", objRT.Filename);
                        sqlParams[4] = new SqlParameter("@P_Out", SqlDbType.Int);
                        sqlParams[4].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_QM_RaiseTicket_Payment_UPDATE", sqlParams, true);
                        status = (Int32)obj;
                    }
                    catch (Exception ex)
                    {
                        status = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.updateRaiseTicketResponse() --> " + ex.Message + " " + ex.StackTrace);
                    }
                    return status;
                }
            }
        }
    }
}
