//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HOSTEL                                                               
// PAGE NAME     : HOSTEL fee head CONTROLLER                                            
// CREATION DATE : 29-Dec-2012                                                         
// CREATED BY    : MRUNAL BANSOD                                               
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
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class MessExpenditureController
    {
        string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        /// <summary>
        /// Purpose    : This method is used to add or modify the
        ///              hostel Mess Expenditure.
        /// </summary>
        /// <param name="objMEM">Type MessExpenditureMaster </param>
        /// <param name="Message">ref string</param>
        /// <returns>long value</returns>
        /// 
        public long AddUpdateMessExpenditure(MessExpenditureMaster objMEM, int Sessionno, int Hostelno, ref string Message)
        {
            long pkid = 0;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[53];

                
                objParams[0] = new SqlParameter("@P_MESS_NO", objMEM.MESSNO);
                objParams[1] = new SqlParameter("@P_MONTH", objMEM.MONTH);
                objParams[2] = new SqlParameter("@P_DIET", objMEM.DIET);
                objParams[3] = new SqlParameter("@P_TOTAL_EXPENDITURE", objMEM.TOTALEXPENDITURE);
                objParams[4] = new SqlParameter("@P_F1", objMEM.F1);
                objParams[5] = new SqlParameter("@P_F2", objMEM.F2);
                objParams[6] = new SqlParameter("@P_F3", objMEM.F3);
                objParams[7] = new SqlParameter("@P_F4", objMEM.F4);
                objParams[8] = new SqlParameter("@P_F5", objMEM.F5);
                objParams[9] = new SqlParameter("@P_F6", objMEM.F6);
                objParams[10] = new SqlParameter("@P_F7", objMEM.F7);
                objParams[11] = new SqlParameter("@P_F8", objMEM.F8);
                objParams[12] = new SqlParameter("@P_F9", objMEM.F9);
                objParams[13] = new SqlParameter("@P_F10", objMEM.F10);
                objParams[14] = new SqlParameter("@P_F11", objMEM.F11);
                objParams[15] = new SqlParameter("@P_F12", objMEM.F12);
                objParams[16] = new SqlParameter("@P_F13", objMEM.F13);
                objParams[17] = new SqlParameter("@P_F14", objMEM.F14);
                objParams[18] = new SqlParameter("@P_F15", objMEM.F15);
                objParams[19] = new SqlParameter("@P_F16", objMEM.F16);
                objParams[20] = new SqlParameter("@P_F17", objMEM.F17);
                objParams[21] = new SqlParameter("@P_F18", objMEM.F18);
                objParams[22] = new SqlParameter("@P_F19", objMEM.F19);
                objParams[23] = new SqlParameter("@P_F20", objMEM.F20);
                objParams[24] = new SqlParameter("@P_F21", objMEM.F21);
                objParams[25] = new SqlParameter("@P_F22", objMEM.F22);
                objParams[26] = new SqlParameter("@P_F23", objMEM.F23);
                objParams[27] = new SqlParameter("@P_F24", objMEM.F24);
                objParams[28] = new SqlParameter("@P_F25", objMEM.F25);
                objParams[29] = new SqlParameter("@P_F26", objMEM.F26);
                objParams[30] = new SqlParameter("@P_F27", objMEM.F27);
                objParams[31] = new SqlParameter("@P_F28", objMEM.F28);
                objParams[32] = new SqlParameter("@P_F29", objMEM.F29);
                objParams[33] = new SqlParameter("@P_F30", objMEM.F30);
                objParams[34] = new SqlParameter("@P_F31", objMEM.F31);
                objParams[35] = new SqlParameter("@P_F32", objMEM.F32);
                objParams[36] = new SqlParameter("@P_F33", objMEM.F33);
                objParams[37] = new SqlParameter("@P_F34", objMEM.F34);
                objParams[38] = new SqlParameter("@P_F35", objMEM.F35);
                objParams[39] = new SqlParameter("@P_F36", objMEM.F36);
                objParams[40] = new SqlParameter("@P_F37", objMEM.F37);
                objParams[41] = new SqlParameter("@P_F38", objMEM.F38);
                objParams[42] = new SqlParameter("@P_F39", objMEM.F39);
                objParams[43] = new SqlParameter("@P_F40", objMEM.F40);

                objParams[44] = new SqlParameter("@P_USERID", objMEM.USERID);
                objParams[45] = new SqlParameter("@P_IPADDRESS", objMEM.IPADDRESS);
                objParams[46] = new SqlParameter("@P_MACADDRESS", objMEM.MACADDRESS);
                objParams[47] = new SqlParameter("@P_AUDITDATE", objMEM.AUDITDATE);
                objParams[48] = new SqlParameter("@P_COLLEGE_CODE", objMEM.COLLEGE_CODE);
                objParams[49] = new SqlParameter("@P_SESSION_NO", Sessionno);
                objParams[50] = new SqlParameter("@P_HOSTEL_NO", Hostelno);
                objParams[51] = new SqlParameter("@P_TRNO", objMEM.TRNO);
                objParams[52] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[52].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_MESS_EXPENDITURE_INSERT_UPDATE", objParams, true);
                if (ret != null)
                {
                    if (ret.ToString().Equals("-99"))
                        Message = "Transaction Failed!";
                    else

                        pkid = Convert.ToInt64(ret.ToString());
                }
                else
                    Message = "Transaction Failed!";


            }
            catch (Exception ee)
            {
                throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.MessExpenditureController.AddUpdateMessExpenditure-> " + ee.ToString());
            }
            return pkid;


        }

        /// <summary>
        /// Purpose    : This method is used to get head details
        ///              from Mess heads
        /// </summary>
        /// <param name="Message">ref string</param>
        /// <returns>dataset</returns>
        public DataSet GetMessHeads(string messhno)
        {
            try
            {

                DataSet dshead = new DataSet();
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_MESSHEADSRNO", messhno);
                dshead = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_GET_HOSTEL_MESSHEADS", objParams);
                return dshead;
            }
            catch (Exception ee)
            {
                throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.MessExpenditureController.GetMessHeads-> " + ee.ToString());
            }

        }

        /// <summary>
        /// Purpose    : This method is used to get mess expenditure
        ///              according to mess no & month
        /// </summary>
        /// <param name="mess_no,month">Type string </param>
        /// <param name="Message">ref string</param>
        /// <returns>dataset</returns>
        public DataSet GetMessHeadsbyMessno_Month(int sessionno,int hostel_no,int mess_no, int month)
        {
            try
            {

                DataSet dshead = new DataSet();
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_SESSION_NO", sessionno);
                objParams[1] = new SqlParameter("@P_HOSTEL_NO", hostel_no);
                objParams[2] = new SqlParameter("@P_MESS_NO", mess_no);
                objParams[3] = new SqlParameter("@P_MONTH", month);
                dshead = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_GET_MESSHEADS_BY_MESSNO_MONTH", objParams);
                return dshead;
            }
            catch (Exception ee)
            {
                throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.MessExpenditureController.GetMessHeadsbyMessno_Month-> " + ee.ToString());
            }

        }


        /// <summary>
        /// Purpose    : This method is used to get mess expenditure
        ///              for perticular mess
        /// </summary>
        /// <param name="mess_no,month">Type string </param>
        /// <param name="Message">ref string</param>
        /// <returns>dataset</returns>
        public DataSet GetMessExpenditurebyMessno_Month(int mess_no, int Sessionno, int Hostelno)//,DateTime month)
        {
            try
            {

                DataSet ds = new DataSet();
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_MESS_NO", mess_no);
                objParams[1] = new SqlParameter("@P_SESSION_NO", Sessionno);
                objParams[2] = new SqlParameter("@P_HOSTEL_NO", Hostelno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_GET_MESSEXPENDITURE_BY_MONTH", objParams);
                return ds;
            }
            catch (Exception ee)
            {
                throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.MessExpenditureController.GetMessExpenditurebyMessno_Month-> " + ee.ToString());
            }
        }

        public DataSet GetMessBillBalance(int sessionno, int messno, int month, int hostelno)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_SESSIONNO_NO", sessionno);
                objParams[1] = new SqlParameter("@P_MESS_NO", messno);
                objParams[2] = new SqlParameter("@P_MONTH", month);
                objParams[3] = new SqlParameter("@P_HOSTEL_NO", hostelno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_MESS_BILL_CALCULATION", objParams);
                return ds;
            }
            catch (Exception ee)
            {
                throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.MessExpenditureController.GetMessBillBalance-> " + ee.ToString());
            }
        }

        public int AddMessBillStudent(MessExpenditureMaster objMEM)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[27];
                objParams[0] = new SqlParameter("@P_IDNO", objMEM.Idno);
                objParams[1] = new SqlParameter("@P_MESS_NO", objMEM.Mess_no);
                objParams[2] = new SqlParameter("@P_SESSION_NO", objMEM.Session_no);
                objParams[3] = new SqlParameter("@P_HOSTEL_NO", objMEM.Hostel_no);
                objParams[4] = new SqlParameter("@P_MONTH_NO", objMEM.Month_no);
                objParams[5] = new SqlParameter("@P_ATTEND_DAYS", objMEM.Attend_days);
                objParams[6] = new SqlParameter("@P_BILL_DATE", objMEM.Bill_date);
                objParams[7] = new SqlParameter("@P_TOTAL_EXPENCE", objMEM.Total_expence);
                objParams[8] = new SqlParameter("@P_TOTAL_BALANCE", objMEM.Total_balance);
                objParams[9] = new SqlParameter("@P_TOTAL_BILL", objMEM.Total_bill);
                objParams[10] = new SqlParameter("@P_MDATE", objMEM.Mdate);
                objParams[11] = new SqlParameter("@P_USERID", objMEM.Ua_no);
                objParams[12] = new SqlParameter("@P_IPADDRESS", objMEM.IPADDRESS);
                objParams[13] = new SqlParameter("@P_MACADDRESS", objMEM.MACADDRESS);
                objParams[14] = new SqlParameter("@P_AUDITDATE", objMEM.AUDITDATE);
                objParams[15] = new SqlParameter("@P_COLLEGE_CODE", objMEM.COLLEGE_CODE);
                objParams[16] = new SqlParameter("@P_F1",objMEM.F1);
                objParams[17] = new SqlParameter("@P_F2",objMEM.F2);
                objParams[18] = new SqlParameter("@P_F3",objMEM.F3);
                objParams[19] = new SqlParameter("@P_F4",objMEM.F4);
                objParams[20] = new SqlParameter("@P_F5",objMEM.F5);
                objParams[21] = new SqlParameter("@P_F6",objMEM.F6);
                objParams[22] = new SqlParameter("@P_F7",objMEM.F7);
                objParams[23] = new SqlParameter("@P_F8",objMEM.F8);
                objParams[24] = new SqlParameter("@P_F9",objMEM.F9);
                objParams[25] = new SqlParameter("@P_F10", objMEM.F10);
                
                objParams[26] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[26].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_MESS_BILL_CALCULATION_INSERT", objParams, true);
                if (ret != null)
                {
                    retStatus = Convert.ToInt32(ret.ToString());
                }

            }
            catch (Exception ee)
            {
                throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.MessExpenditureController.AddMessBillStudent-> " + ee.ToString());
            }
            return retStatus;
        }

        public int AddMessBillTrans(MessExpenditureMaster objMEM, DateTime FromDate, DateTime Todate)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_SESSION_NO", objMEM.Session_no);
                objParams[1] = new SqlParameter("@P_MESS_NO", objMEM.Mess_no);
                objParams[2] = new SqlParameter("@P_HOSTEL_NO", objMEM.Hostel_no);
                objParams[3] = new SqlParameter("@P_MONTH_NO", objMEM.Month_no);
                objParams[4] = new SqlParameter("@P_BILL_DATE", objMEM.Bill_date);
                objParams[5] = new SqlParameter("@P_FROM_DATE", FromDate);
                objParams[6] = new SqlParameter("@P_TO_DATE", Todate);
                objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objMEM.COLLEGE_CODE);
                objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_MESS_BILL_TRANS_INSERT", objParams, true);
                if (ret != null)
                {
                    retStatus = Convert.ToInt32(ret.ToString());
                }
            }
            catch (Exception ee)
            {
                throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.MessExpenditureController.AddMessBillStudent-> " + ee.ToString());
            }
            return retStatus;
        }

    }
}
