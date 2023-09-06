
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
            public class Con_PayrollTallyConfig
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
                        objParams[2] = new SqlParameter("@PayrollTallyCompanyConfigId", ObjCC.TallyCompanyConfigId);
                        ds = objSQLHelper.ExecuteDataSetSP("uspBindAllPayrollTallyCompanyConfigData", objParams);

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
                        objParams[2] = new SqlParameter("@PayrollTallyCompanyConfigId", ObjCC.TallyCompanyConfigId);
                        ds = objSQLHelper.ExecuteDataSetSP("uspBindAllPayrollTallyCompanyConfigData", objParams);

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
                        objParams[0] = new SqlParameter("@PayrollTallyCompanyConfigId", ObjCCM.TallyCompanyConfigId);
                        objParams[1] = new SqlParameter("@StaffTypeId", ObjCCM.CashBookId);
                        objParams[2] = new SqlParameter("@TallyConfigId", ObjCCM.TallyConfigId);
                        objParams[3] = new SqlParameter("@TallyCompanyName", ObjCCM.TallyCompanyName);
                        objParams[4] = new SqlParameter("@CollegeId", ObjCCM.CollegeId);
                        objParams[5] = new SqlParameter("@IsActive", ObjCCM.IsActive);
                        objParams[6] = new SqlParameter("@CreatedBy", ObjCCM.CreatedBy);
                        objParams[7] = new SqlParameter("@IPAddress", ObjCCM.IPAddress);
                        objParams[8] = new SqlParameter("@MACAddress", ObjCCM.MACAddress);
                        objParams[9] = new SqlParameter("@R_Out", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("UspInsertUpdatePayrollTallyCompanyConfig", objParams, true);

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

                public DataSet GetTallyCompanyName(Ent_TransferRecordsToTally ObjTRM, int CollegeId)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@CollegeId", CollegeId);
                        ds = objSQLHelper.ExecuteDataSetSP("[dbo].[UspGetSelectedCompanyForPayrollTally]", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    return ds;
                }

                public DataSet GetTallyPayHeadDataStaffTypeWise(int CollegeNo, int stafftypeid)
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@CollegeNo", CollegeNo);
                        objParams[1] = new SqlParameter("@StaffTypeId", stafftypeid);
                        ds = objSQLHelper.ExecuteDataSetSP("[dbo].[UspGetPayrollTallyPayHeadData]", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    return ds;


                }

                public long UpdatePayHeadsTallyStaffType(Ent_TallyFeeHeads ObjTFM, ref string Message)
                {
                    long pkid = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@StaffTypeId", ObjTFM.CashBookName);
                        objParams[1] = new SqlParameter("@PayHeadTallyTbl", ObjTFM.FeeHeadTally);
                        objParams[2] = new SqlParameter("@CollegeId", ObjTFM.CollegeId);
                        objParams[3] = new SqlParameter("@ModifiedBy", ObjTFM.ModifiedBy);
                        objParams[4] = new SqlParameter("@IPAddress", ObjTFM.IPAddress);
                        objParams[5] = new SqlParameter("@MACAddress", ObjTFM.MACAddress);
                        objParams[6] = new SqlParameter("@R_Out", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("UspUpdatePayHeadForPayrollTallyIntegration", objParams, true);

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
