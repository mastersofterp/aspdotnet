//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HOSTEL                                                               
// PAGE NAME     : HOSTEL fee head CONTROLLER                                            
// CREATION DATE : 20-Dec-2012                                                         
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
    public class MessBillController
    {
        string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        #region Methods

        public long AddUpdateMessBill(MessBillMaster objMBM, ref string Message)
        {
            long pkid = 0;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[51];

                objParams[0] = new SqlParameter("@P_MESSBILL_NO", objMBM.MESSBILLNO);
                objParams[1] = new SqlParameter("@P_ATTEND_DAYS", objMBM.ATTENDDAYS);
                objParams[2] = new SqlParameter("@P_TOTAL_EXPENCE", objMBM.TOTALEXPENCE);
                objParams[3] = new SqlParameter("@P_TOTAL_BILL", objMBM.TOTALBILL);
                objParams[4] = new SqlParameter("@P_MDATE", objMBM.MDATE);
                objParams[5] = new SqlParameter("@P_F1", objMBM.F1);
                objParams[6] = new SqlParameter("@P_F2", objMBM.F2);
                objParams[7] = new SqlParameter("@P_F3", objMBM.F3);
                objParams[8] = new SqlParameter("@P_F4", objMBM.F4);
                objParams[9] = new SqlParameter("@P_F5", objMBM.F5);
                objParams[10] = new SqlParameter("@P_F6", objMBM.F6);
                objParams[11] = new SqlParameter("@P_F7", objMBM.F7);
                objParams[12] = new SqlParameter("@P_F8", objMBM.F8);
                objParams[13] = new SqlParameter("@P_F9", objMBM.F9);
                objParams[14] = new SqlParameter("@P_F10", objMBM.F10);
                objParams[15] = new SqlParameter("@P_F11", objMBM.F11);
                objParams[16] = new SqlParameter("@P_F12", objMBM.F12);
                objParams[17] = new SqlParameter("@P_F13", objMBM.F13);
                objParams[18] = new SqlParameter("@P_F14", objMBM.F14);
                objParams[19] = new SqlParameter("@P_F15", objMBM.F15);
                objParams[20] = new SqlParameter("@P_F16", objMBM.F16);
                objParams[21] = new SqlParameter("@P_F17", objMBM.F17);
                objParams[22] = new SqlParameter("@P_F18", objMBM.F18);
                objParams[23] = new SqlParameter("@P_F19", objMBM.F19);
                objParams[24] = new SqlParameter("@P_F20", objMBM.F20);
                objParams[25] = new SqlParameter("@P_F21", objMBM.F21);
                objParams[26] = new SqlParameter("@P_F22", objMBM.F22);
                objParams[27] = new SqlParameter("@P_F23", objMBM.F23);
                objParams[28] = new SqlParameter("@P_F24", objMBM.F24);
                objParams[29] = new SqlParameter("@P_F25", objMBM.F25);
                objParams[30] = new SqlParameter("@P_F26", objMBM.F26);
                objParams[31] = new SqlParameter("@P_F27", objMBM.F27);
                objParams[32] = new SqlParameter("@P_F28", objMBM.F28);
                objParams[33] = new SqlParameter("@P_F29", objMBM.F29);
                objParams[34] = new SqlParameter("@P_F30", objMBM.F30);
                objParams[35] = new SqlParameter("@P_F31", objMBM.F31);
                objParams[36] = new SqlParameter("@P_F32", objMBM.F32);
                objParams[37] = new SqlParameter("@P_F33", objMBM.F33);
                objParams[38] = new SqlParameter("@P_F34", objMBM.F34);
                objParams[39] = new SqlParameter("@P_F35", objMBM.F35);
                objParams[40] = new SqlParameter("@P_F36", objMBM.F36);
                objParams[41] = new SqlParameter("@P_F37", objMBM.F37);
                objParams[42] = new SqlParameter("@P_F38", objMBM.F38);
                objParams[43] = new SqlParameter("@P_F39", objMBM.F39);
                objParams[44] = new SqlParameter("@P_F40", objMBM.F40);

                objParams[45] = new SqlParameter("@P_USERID", objMBM.USERID);
                objParams[46] = new SqlParameter("@P_IPADDRESS", objMBM.IPADDRESS);
                objParams[47] = new SqlParameter("@P_MACADDRESS", objMBM.MACADDRESS);
                objParams[48] = new SqlParameter("@P_AUDITDATE", objMBM.AUDITDATE);
                objParams[49] = new SqlParameter("@P_COLLEGE_CODE", objMBM.COLLEGE_CODE);
                objParams[50] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[50].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_MESSBILL_INSERT_UPDATE", objParams, true);
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
                throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.Hsl_MessBilController.AddUpdateMessBill-> " + ee.ToString());
            }
            return pkid;


        }

        /// </summary>
        /// <param name="objMBM">Type Mess</param>
        /// <param name="Message">ref string</param>
        /// <returns>long value</returns>
        //This method is used to insert in the MessBill

        public long MessBillInsert(MessBillMaster objMBM, ref string Message)
        {
            long pkid = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[3];

                objParams[0] = new SqlParameter("@P_MESSNO", objMBM.MESSNO);
                objParams[1] = new SqlParameter("@P_DATE", objMBM.BILLDATE);
                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;


                object ret2 = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_MESS_BILL_INSERT", objParams, true);

                if (ret2 != null)
                {
                    if (ret2.ToString().Equals(-99))
                        Message = ("Transaction Failed !");
                    else
                        pkid = Convert.ToInt64(ret2);
                }
                else
                    Message = "Transaction Failed !";

            }
            catch (Exception ee)
            {
                throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.Hsl_MessBilController.MessBillInsert->" + ee.ToString());
            }
            return pkid;
        }



        /// <summary>
        /// Purpose    : This method is used to get Mess bill details
        ///              
        /// </summary>
        /// <param name="MessNo">string</param>
        /// <param name="Month">string</param>
        /// <param name="Year">string</param>
        /// <returns>dataset</returns>
        public DataSet GetMessPerDayCharges(string MessNo, string Month, string Year)
        {
            try
            {

                DataSet dshead = new DataSet();
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_MESS_NO", MessNo);
                objParams[1] = new SqlParameter("@P_MONTH", Month);
                objParams[2] = new SqlParameter("@P_YEAR", Year);
                dshead = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_GET_MESS_PERDAYCHARGES", objParams);
                return dshead;
            }
            catch (Exception ee)
            {
                throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.Hsl_MessBilController.GetMessPerDayCharges-> " + ee.ToString());
            }

        }

        /// <summary>
        /// Purpose    : This method is used to get Mess bill details
        ///              
        /// </summary>
        /// <param name="messno">string</param>
        /// <param name="resident">string</param>
        /// <returns>dataset</returns>
        public DataSet GetMessBillDetails(string messno, string resident)
        {
            try
            {

                DataSet dshead = new DataSet();
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_MESS_NO", messno);
                objParams[1] = new SqlParameter("@P_RESIDENT_TYPE_NO", resident);
                dshead = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_GET_MESSBILL_DETAILS", objParams);
                return dshead;
            }
            catch (Exception ee)
            {
                throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.Hsl_MessBilController.GetMessBillDetails-> " + ee.ToString());
            }

        }

        /// <summary>
        /// Purpose    : This method is used to get Mess Fee heads from
        ///              
        /// </summary>
        /// <returns>dataset</returns>
        public DataSet GetMessFeeHeads()
        {
            try
            {

                DataSet dshead = new DataSet();
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[0];
                dshead = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_GET_MESSFEE_HEADS", objParams);
                return dshead;
            }
            catch (Exception ee)
            {
                throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.Hsl_MessBilController.GetMessFeeHeads-> " + ee.ToString());
            }
        }


        /// </summary>
        /// <param name="objMBM">Type Mess</param>
        /// <param name="Message">ref string</param>
        /// <returns>long value</returns>
        //This method is used to get the student who alloted the mess but not in messbill

        public DataSet GetNewMessStudent(string messno, string month, string year)
        {
            try
            {

                DataSet ds = new DataSet();
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_MESSNO", messno);
                objParams[1] = new SqlParameter("@P_MONTH", month);
                objParams[2] = new SqlParameter("@P_YEAR", year);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_GET_NEW_MESS_STUDENT", objParams);
                return ds;
            }
            catch (Exception ee)
            {
                throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.Hsl_MessBilController.GetNewMessStudent-> " + ee.ToString());
            }

        }

        public long UpdateMessBill(MessBillMaster objMBM, ref string Message)
        {
            long pkid = 0;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[9];

                objParams[0] = new SqlParameter("@P_IDNO", objMBM.IDNo);
                objParams[1] = new SqlParameter("@P_STUDNAME", objMBM.NAME);
                objParams[2] = new SqlParameter("@P_MESS_NO", objMBM.MESSNO);
                objParams[3] = new SqlParameter("@P_HOSTEL", objMBM.HOSTEL);
                objParams[4] = new SqlParameter("@P_ROOM", objMBM.ROOM);
                objParams[5] = new SqlParameter("@P_ATTEND_DAYS", objMBM.ATTENDDAYS);
                objParams[6] = new SqlParameter("@P_BILL_DATE", objMBM.BILLDATE);
                objParams[7] = new SqlParameter("@P_TOTAL_EXPENCE", objMBM.TOTALEXPENCE);
                //objParams[8] = new SqlParameter("@P_F1", objMBM.F1);
                //objParams[9] = new SqlParameter("@P_F2", objMBM.F2);
                //objParams[10] = new SqlParameter("@P_F3", objMBM.F3);
                //objParams[11] = new SqlParameter("@P_F4", objMBM.F4);
                //objParams[12] = new SqlParameter("@P_F5", objMBM.F5);
                //objParams[13] = new SqlParameter("@P_F6", objMBM.F6);
                //objParams[14] = new SqlParameter("@P_F7", objMBM.F7);
                //objParams[15] = new SqlParameter("@P_F8", objMBM.F8);
                //objParams[16] = new SqlParameter("@P_F9", objMBM.F9);
                //objParams[17] = new SqlParameter("@P_F10", objMBM.F10);
                //objParams[18] = new SqlParameter("@P_F11", objMBM.F11);
                //objParams[19] = new SqlParameter("@P_F12", objMBM.F12);
                //objParams[20] = new SqlParameter("@P_F13", objMBM.F13);
                //objParams[21] = new SqlParameter("@P_F14", objMBM.F14);
                //objParams[22] = new SqlParameter("@P_F15", objMBM.F15);
                //objParams[23] = new SqlParameter("@P_F16", objMBM.F16);
                //objParams[24] = new SqlParameter("@P_F17", objMBM.F17);
                //objParams[25] = new SqlParameter("@P_F18", objMBM.F18);
                //objParams[26] = new SqlParameter("@P_F19", objMBM.F19);
                //objParams[27] = new SqlParameter("@P_F20", objMBM.F20);
                //objParams[28] = new SqlParameter("@P_F21", objMBM.F21);
                //objParams[29] = new SqlParameter("@P_F22", objMBM.F22);
                //objParams[30] = new SqlParameter("@P_F23", objMBM.F23);
                //objParams[31] = new SqlParameter("@P_F24", objMBM.F24);
                //objParams[32] = new SqlParameter("@P_F25", objMBM.F25);
                //objParams[33] = new SqlParameter("@P_F26", objMBM.F26);
                //objParams[34] = new SqlParameter("@P_F27", objMBM.F27);
                //objParams[35] = new SqlParameter("@P_F28", objMBM.F28);
                //objParams[36] = new SqlParameter("@P_F29", objMBM.F29);
                //objParams[37] = new SqlParameter("@P_F30", objMBM.F30);
                //objParams[38] = new SqlParameter("@P_F31", objMBM.F31);
                //objParams[39] = new SqlParameter("@P_F32", objMBM.F32);
                //objParams[40] = new SqlParameter("@P_F33", objMBM.F33);
                //objParams[41] = new SqlParameter("@P_F34", objMBM.F34);
                //objParams[42] = new SqlParameter("@P_F35", objMBM.F35);
                //objParams[43] = new SqlParameter("@P_F36", objMBM.F36);
                //objParams[44] = new SqlParameter("@P_F37", objMBM.F37);
                //objParams[45] = new SqlParameter("@P_F38", objMBM.F38);
                //objParams[46] = new SqlParameter("@P_F39", objMBM.F39);
                //objParams[47] = new SqlParameter("@P_F40", objMBM.F40);

                objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_MESSBILL_NEWSTUD_INSERT", objParams, true);
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
                throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.Hsl_MessBilController.UpdateMessBill-> " + ee.ToString());
            }
            return pkid;


        }

        //FOR INSERTING HOSTEL STUDENT FINE AMOUNT INFORMATION
        public int HostelFineInsert(int sessionno, int idno, int hostelno, decimal fineamount, string remark, int uano, string ipaddress, int OrganizationId)
        {
            int pkid = 0;
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[9];

                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_IDNO", idno);
                objParams[2] = new SqlParameter("@P_HOSTELNO", hostelno);
                objParams[3] = new SqlParameter("@P_FINEAMOUNT", fineamount);
                objParams[4] = new SqlParameter("@P_REMARK", remark);
                objParams[5] = new SqlParameter("@P_UANO", uano);
                objParams[6] = new SqlParameter("@P_IPADDRESS", ipaddress);
                objParams[7] = new SqlParameter("@P_ORGANIZATION_ID", OrganizationId);
                objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;


                object ret2 = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_STUDENT_FINE_INSERT_ADD", objParams, true);

                if (Convert.ToInt32(ret2) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ee)
            {
                throw new IITMSException("IITMS.BusinessLogicLayer.BusinessLogic.Hsl_MessBilController.MessBillInsert->" + ee.ToString());
            }
            return pkid;
        }

        //Add hostel mess commitee member information 
        public int AddCommiteeMember(int SessionNo, int HostelNo, int MessNo, int MemberId1, int MemberId2, int MemberId3, int MemberId4, int MemberId5, DateTime date, int UaNo, string IpAddress, int Can, string CollegeCode)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_SESSIONNO", SessionNo),
                    new SqlParameter("@P_HOSTELNO", HostelNo),
                    new SqlParameter("@P_MESSNO", MessNo),
                    new SqlParameter("@P_MEMBERID1",MemberId1),
                    new SqlParameter("@P_MEMBERID2",MemberId2),
                    new SqlParameter("@P_MEMBERID3",MemberId3),
                    new SqlParameter("@P_MEMBERID4",MemberId4),
                    new SqlParameter("@P_MEMBERID5",MemberId5),
                    new SqlParameter("@P_DATE",date),
                    new SqlParameter("@P_UANO",UaNo),
                    new SqlParameter("@P_IPADDRESS",IpAddress),
                    new SqlParameter("@P_CAN",Can),
                    new SqlParameter("@P_COLLEGECODE",CollegeCode),
                    new SqlParameter("@P_OUT ",status)
                   
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_COMM_MEMBER_INSERT", sqlParams, true);

                if (obj != null && obj.ToString() != "-99")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AssetController.AddCommiteeMember() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
        //TO SHOW COMMITEE MEMBER
        public DataSet GetAllCommiteeMember(int Sessionno, int Hostelno, int Messno)
        {
            DataSet ds = null;
            try
            {

                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_SESSIONNO", Sessionno);
                objParams[1] = new SqlParameter("@P_HOSTELNO", Hostelno);
                objParams[2] = new SqlParameter("@P_MESSNO", Messno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_COMM_MEMBER_SELECT", objParams);
                return ds;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AssetController.GetAllCommiteeMember() --> " + ex.Message + " " + ex.StackTrace);
            }

        }
        #endregion 
    }
}
