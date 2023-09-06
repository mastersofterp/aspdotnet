using IITMS.SQLServer.SQLDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
namespace IITMS.UAIMS.BusinessLogicLayer.BusinessLogic
{
    //namespace IITMS
    //{
    //    namespace NITPRM
    //    {
    //        namespace BusinessLayer.BusinessLogic
    //        {
    public class Con_Acd_GetRecordsForTallyTransfer
    {
        //private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["NITPRM"].ConnectionString;
        string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        //public DataSet GetAllDetails(Ent_GetRecordsForTallyTransfer ObjTTM)
        //{
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
        //        SqlParameter[] objParams = null;

        //        objParams = new SqlParameter[4];
        //        objParams[0] = new SqlParameter("@CashBookId", ObjTTM.CashBookId);
        //        objParams[1] = new SqlParameter("@CollegeId", ObjTTM.CollegeId);
        //        objParams[2] = new SqlParameter("@FromDate", ObjTTM.FromDate);
        //        objParams[3] = new SqlParameter("@TODate", ObjTTM.ToDate);
        //        ds = objSQLHelper.ExecuteDataSetSP("[Academic].[UspGetTotalFeeForTallyIntegration]", objParams);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //    return ds;
        //}

        public DataSet GetAllDetails(Ent_Acd_GetRecordsForTallyTransfer ObjTTM, string fromdate, string todate)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@CashBookId", ObjTTM.CashBookId);
                objParams[1] = new SqlParameter("@CollegeId", ObjTTM.CollegeId);
                objParams[2] = new SqlParameter("@FromDate", fromdate);
                objParams[3] = new SqlParameter("@TODate", todate);
                ds = objSQLHelper.ExecuteDataSetSP("[PKG_ACD_Tally_GetTotalFeeForTallyIntegration]", objParams);

            }
            catch (Exception ex)
            {
                throw;
            }
            return ds;
        }










        //public long AddUpdateDCRTallyRecords(Ent_GetRecordsForTallyTransfer ObjTTM, ref string Message)
        //  {
        //      long pkid = 0;
        //      try
        //      {
        //          SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
        //          SqlParameter[] objParams = null;
        //          objParams = new SqlParameter[8];
        //          objParams[0] = new SqlParameter("@CashBookId", ObjTTM.CashBookId);
        //          objParams[1] = new SqlParameter("@FromDate", ObjTTM.FromDate);
        //          objParams[2] = new SqlParameter("@ToDate", ObjTTM.ToDate);
        //          objParams[3] = new SqlParameter("@CreatedBy", ObjTTM.CreatedBy);
        //          objParams[4] = new SqlParameter("@CollegeId", ObjTTM.CollegeId);
        //          objParams[5] = new SqlParameter("@IPAddress", ObjTTM.IPAddress);
        //          objParams[6] = new SqlParameter("@MACAddress", ObjTTM.MACAddress);
        //          objParams[7] = new SqlParameter("@R_Out", SqlDbType.Int);
        //          objParams[7].Direction = ParameterDirection.Output;


        //          object ret = objSQLHelper.ExecuteNonQuerySP("[Academic].[UspInsertDCRTallyTransfer]", objParams, true);

        //          if (ret != null)
        //          {
        //              if (ret.ToString().Equals("-99"))
        //              {
        //                  Message = "Transaction Failed!";
        //              }
        //              else
        //              {
        //                  pkid = Convert.ToInt64(ret.ToString());
        //              }
        //          }
        //          else
        //          {
        //              pkid = -99;
        //              Message = "Transaction Failed!";
        //          }
        //      }
        //      catch (Exception ex)
        //      {
        //          pkid = -99;
        //          throw;

        //      }
        //      return pkid;
        //  }


        public long AddUpdateDCRTallyRecords(Ent_Acd_GetRecordsForTallyTransfer ObjTTM, ref string Message, string fromdate, string todate)
        {
            long pkid = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@CashBookId", ObjTTM.CashBookId);
                objParams[1] = new SqlParameter("@FromDate", fromdate);
                objParams[2] = new SqlParameter("@ToDate", todate);
                objParams[3] = new SqlParameter("@CreatedBy", ObjTTM.CreatedBy);
                objParams[4] = new SqlParameter("@CollegeId", ObjTTM.CollegeId);
                objParams[5] = new SqlParameter("@IPAddress", ObjTTM.IPAddress);
                objParams[6] = new SqlParameter("@MACAddress", ObjTTM.MACAddress);
                objParams[7] = new SqlParameter("@R_Out", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;


                object ret = objSQLHelper.ExecuteNonQuerySP("[PKG_ACD_Tally_InsertDCRTallyTransfer]", objParams, true);

                if (ret != null)
                {
                    if (ret.ToString().Equals("-99"))
                    {
                        Message = "Transaction Failed!";
                    }
                    else
                    {
                        pkid = Convert.ToInt64(ret.ToString());
                    }
                }
                else
                {
                    pkid = -99;
                    Message = "Transaction Failed!";
                }
            }
            catch (Exception ex)
            {
                pkid = -99;
                throw;

            }
            return pkid;
        }



        //            }
        //        }
        //    }
        //}
    }
}
