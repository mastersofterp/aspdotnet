//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Data;
//using System.Data.SqlClient;
//using System.Web;
//using IITMS.SQLServer.SQLDAL;
//using IITMS.NITPRM.BusinessLayer.BusinessEntities;

using System;
using System.Data;
using System.Web;
using System.Linq;
using IITMS;
using IITMS.NITPRM;
using IITMS.NITPRM.BusinessLayer;
using IITMS.NITPRM.BusinessLayer.BusinessEntities;
using IITMS.NITPRM.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;
using BusinessLogicLayer.BusinessEntities.Academic;
using BusinessLogicLayer.BusinessLogic.Academic;
namespace IITMS.UAIMS.BusinessLogicLayer.BusinessLogic
{

    //namespace IITMS
    //{
    //    namespace NITPRM
    //    {
    //        namespace BusinessLayer.BusinessLogic
    //        {
    public class Con_Acd_CompanyConfig
    {
        //string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["NITPRM"].ConnectionString;
        string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet GetAllDetails(Ent_Acd_CompanyConfig ObjCCM)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@CommandType", ObjCCM.CommandType);
                objParams[1] = new SqlParameter("@CollegeId", ObjCCM.CollegeId);
                objParams[2] = new SqlParameter("@TallyCompanyConfigId", ObjCCM.TallyCompanyConfigId);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_Tally_BindAllTallyCompanyConfigData", objParams);

            }
            catch (Exception ex)
            {
                throw;
            }
            return ds;
        }


        // To inser update bank data.
        public long AddUpdateTallyConfig(Ent_Acd_CompanyConfig ObjCCM, ref string Message)
        {
            long pkid = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@TallyCompanyConfigId", ObjCCM.TallyCompanyConfigId);
                objParams[1] = new SqlParameter("@CashBookId", ObjCCM.CashBookId);
                objParams[2] = new SqlParameter("@TallyConfigId", ObjCCM.TallyConfigId);
                objParams[3] = new SqlParameter("@TallyCompanyName", ObjCCM.TallyCompanyName);
                objParams[4] = new SqlParameter("@CollegeId", ObjCCM.CollegeId);
                objParams[5] = new SqlParameter("@IsActive", ObjCCM.IsActive);
                objParams[6] = new SqlParameter("@CreatedBy", ObjCCM.CreatedBy);
                objParams[7] = new SqlParameter("@IPAddress", ObjCCM.IPAddress);
                objParams[8] = new SqlParameter("@MACAddress", ObjCCM.MACAddress);
                objParams[9] = new SqlParameter("@R_Out", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;




                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_Tally_InsertUpdateTallyCompanyConfig", objParams, true);

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
