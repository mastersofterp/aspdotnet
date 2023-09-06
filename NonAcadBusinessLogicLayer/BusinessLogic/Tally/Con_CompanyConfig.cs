
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class Con_CompanyConfig
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetAllDetails(Ent_CompanyConfig ObjCC)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@CommandType", ObjCC.CommandType);
                        objParams[1] = new SqlParameter("@CollegeId", ObjCC.CollegeId);
                        objParams[2] = new SqlParameter("@TallyCompanyConfigId", ObjCC.TallyCompanyConfigId);
                        ds = objSQLHelper.ExecuteDataSetSP("uspBindAllTallyCompanyConfigData", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    return ds;
                }

                public DataSet GetPayCompanyDetails(Ent_CompanyConfig ObjCC)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@CommandType", ObjCC.CommandType);
                        objParams[1] = new SqlParameter("@CollegeId", ObjCC.CollegeId);
                        objParams[2] = new SqlParameter("@TallyCompanyConfigId", ObjCC.TallyCompanyConfigId);
                        ds = objSQLHelper.ExecuteDataSetSP("uspBindAllTallyCompanyConfigData", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    return ds;
                }


                // To inser update bank data.
                public long AddUpdateTallyConfig(Ent_CompanyConfig ObjCCM, ref string Message)
                {
                    long pkid = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
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

                        object ret = objSQLHelper.ExecuteNonQuerySP("UspInsertUpdateTallyCompanyConfig", objParams, true);

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
            }
        }
    }
}
