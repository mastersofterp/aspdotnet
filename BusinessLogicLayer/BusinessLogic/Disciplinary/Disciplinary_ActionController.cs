using System;
using System.Data;
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;


using IITMS.UAIMS.BusinessLayer.BusinessEntities;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
   public class Disciplinary_ActionController
    {
        //string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["NITPRM"].ConnectionString;
       string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddEvent(Disciplinary_Action objDiscAction)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                        new SqlParameter("@P_EDATE", objDiscAction.EDATE),
                        new SqlParameter("@P_ETITLE",objDiscAction.ETITLE ),
                        new SqlParameter("@P_EDETAIL",objDiscAction.EDESC ),
                        new SqlParameter("@P_ECATNO", objDiscAction.ECAT),
                          new SqlParameter("@P_ACTIONTAKEN", objDiscAction.PUNISH),
                          new SqlParameter("@P_AUTHORITYNAME",objDiscAction.AUTHO ),
                          new SqlParameter("@P_UNO", objDiscAction.UNO),
                          new SqlParameter("@P_EENTRYDATE", objDiscAction.ENTRYDATE),
                          new SqlParameter("@P_COLLEGECODE", objDiscAction.CCODE),
                          new SqlParameter("@P_EID", SqlDbType.Int)
                  
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("DISCIPLINARY_INSERT_DISC_ACTION", sqlParams, true);

                if (obj != null && obj.ToString() != "-99")
                {
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Disciplinary_ActionController.AddEvent() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public int AddStudAction(Disciplinary_Action objDiscAction)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                        new SqlParameter("@P_IDNO", objDiscAction.STUDID),
                        new SqlParameter("@P_BRANCHNO",objDiscAction.BRANCHNO ),
                        new SqlParameter("@P_SEMNO",objDiscAction.SEMNO),       
                        new SqlParameter("@P_COLLCODE",objDiscAction.CCODE),
                        new SqlParameter("@P_EID",SqlDbType.Int) 
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("DISCIPLINARY_INSERT_STUDENT_ACTION", sqlParams, true);

                if (obj != null && obj.ToString() != "-99")
                {
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Disciplinary_ActionController.AddStudAction() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        // NEW METHOD ADDED ON 2020 MAY 29

        public int InsertDisciplinaryAction(int Sessionno, int IDNO, string Regno, DateTime FromDate, DateTime ToDate, string Remark, int UA_NO, string IPAddress, int OrgId)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                        new SqlParameter("@P_IDNO", IDNO),
                        new SqlParameter("@P_SESSIONNO",Sessionno),
                        new SqlParameter("@P_REGNO",Regno),       
                        new SqlParameter("@P_FROMDATE",FromDate),
                        new SqlParameter("@P_TODATE",ToDate),
                        new SqlParameter("@P_REMARK",Remark),
                        new SqlParameter("@P_UA_NO",UA_NO),
                        new SqlParameter("@P_IP_ADDRESS",IPAddress),  
                        new SqlParameter("@P_ORGID",OrgId),
                        new SqlParameter("@P_OUTPUT",SqlDbType.Int) 
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_DISCIPLINARY_ACTION", sqlParams, true);

                if (obj != null && obj.ToString() != "-99")
                {
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else
                {
                    status = Convert.ToInt32(CustomStatus.Error);
                }
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Disciplinary_ActionController.InsertDisciplinaryAction() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }


        public int UpdateDisciplinaryAction(int Discipline_ID, DateTime FromDate, DateTime ToDate, string Remark, int UA_NO, string IPAddress, int OrgId)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                        new SqlParameter("@P_DISCIPLINE_ID", Discipline_ID),                        
                        new SqlParameter("@P_FROMDATE",FromDate),
                        new SqlParameter("@P_TODATE",ToDate),
                        new SqlParameter("@P_REMARK",Remark),
                        new SqlParameter("@P_UA_NO",UA_NO),
                        new SqlParameter("@P_IP_ADDRESS",IPAddress),                        
                        new SqlParameter("@P_OUTPUT",SqlDbType.Int) ,
                        new SqlParameter("@P_ORGID",OrgId)// Added By Dileep Kare on 21.03.2022
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_DISCIPLINARY_ACTION", sqlParams, true);

                if (obj != null && obj.ToString() != "-99")
                {
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else
                {
                    status = Convert.ToInt32(CustomStatus.Error);
                }
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.Disciplinary_ActionController.InsertDisciplinaryAction() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
    }
}