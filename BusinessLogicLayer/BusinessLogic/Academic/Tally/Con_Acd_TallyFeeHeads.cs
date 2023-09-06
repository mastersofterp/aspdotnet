using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using IITMS.SQLServer.SQLDAL;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class Con_Acd_TallyFeeHeads
            {
                //private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["NITPRM"].ConnectionString;
                string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetAllDetails(Ent_Acd_TallyFeeHeads ObjTFM)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@CommandType", ObjTFM.CommandType);
                        objParams[1] = new SqlParameter("@CollegeId", ObjTFM.CollegeId);
                        objParams[2] = new SqlParameter("@CashBookId", ObjTFM.CashBookId);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_Tally_TallyFeeHeads", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    return ds;
                }

                public long UpdateFeeHeadsTally(Ent_Acd_TallyFeeHeads ObjTFM, ref string Message)
                {
                    long pkid = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@CashBookId", ObjTFM.CashBookName);
                        objParams[1] = new SqlParameter("@FeeHeadTallyTbl", ObjTFM.FeeHeadTally);
                        objParams[2] = new SqlParameter("@CollegeId", ObjTFM.CollegeId);
                        objParams[3] = new SqlParameter("@ModifiedBy", ObjTFM.ModifiedBy);
                        objParams[4] = new SqlParameter("@IPAddress", ObjTFM.IPAddress);
                        objParams[5] = new SqlParameter("@MACAddress", ObjTFM.MACAddress);
                        objParams[6] = new SqlParameter("@R_Out", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;


                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_Tally_UpdateFeeHeadForTallyIntegration", objParams, true);

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


                public DataSet GetFeesHeads(Ent_Acd_TallyFeeHeads ObjTFM)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[2];
                       // objParams[0] = new SqlParameter("@CommandType", ObjTFM.CommandType);
                        objParams[0] = new SqlParameter("@CollegeId", ObjTFM.CollegeId);
                        objParams[1] = new SqlParameter("@CashBookId", ObjTFM.CashBookName);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_Tally_GET_FEES_HEADS_FOR_TALLY", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    return ds;
                }




            }
        }
    }
}
