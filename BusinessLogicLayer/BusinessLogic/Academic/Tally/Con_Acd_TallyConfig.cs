﻿//using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Data;
//using System.Data.SqlClient;
//using System.Web;
//using IITMS.SQLServer.SQLDAL;
//using IITMS.NITPRM.BusinessLayer.BusinessEntities;
//using IITMS.NITPRM.BusinessLayer.BusinessLogic;

using System;
using System.Data;
using System.Web;
using System.Linq;
using IITMS;
//using IITMS.NITPRM;
//using IITMS.NITPRM.BusinessLayer;
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
    //        namespace BusinessLogicLayer.BusinessLogic
    //        {
    public class Con_Acd_TallyConfig
    {

        // string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["NITPRM"].ConnectionString;
        string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet GetAllDetails(Ent_Acd_TallyConfig ObjTC)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@CommandType", ObjTC.CommandType);
                objParams[1] = new SqlParameter("@CollegeId", ObjTC.CollegeId);
                objParams[2] = new SqlParameter("@TallyConfigId", ObjTC.TallyConfigId);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_Tally_BindAllTallyConfigData", objParams);

            }
            catch (Exception ex)
            {
                throw;
            }
            return ds;
        }

        public long AddUpdateTallyConfig(Ent_Acd_TallyConfig ObjTC, ref string Message)
        {
            long pkid = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@TallyConfigId", ObjTC.TallyConfigId);
                objParams[1] = new SqlParameter("@ServerName", ObjTC.ServerName);
                objParams[2] = new SqlParameter("@PortNumber", ObjTC.PortNumber);
                objParams[3] = new SqlParameter("@CollegeId", ObjTC.CollegeId);
                objParams[4] = new SqlParameter("@IsActive", ObjTC.IsActive);
                objParams[5] = new SqlParameter("@CreatedBy", ObjTC.CreatedBy);
                objParams[6] = new SqlParameter("@IPAddress", ObjTC.IPAddress);
                objParams[7] = new SqlParameter("@MACAddress", ObjTC.MACAddress);
                objParams[8] = new SqlParameter("@R_Out", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;

                //  object ret = objSQLHelper.ExecuteNonQuerySP("UspInsertUpdateTallyConfig", objParams, true);
                object ret = Convert.ToInt16(objSQLHelper.ExecuteNonQuerySP("PKG_ACD_Tally_InsertUpdateTallyConfig", objParams, true));

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

        public DataSet GetAllTallyVoucherDetails(Ent_Acd_TallyConfig ObjTC)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@CommandType", ObjTC.CommandType);
                objParams[1] = new SqlParameter("@CollegeId", ObjTC.CollegeId);
                objParams[2] = new SqlParameter("@TallyVoucherTypeId", ObjTC.TallyVoucherTypeId);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_Tally_BindTallyVoucherName", objParams);

            }
            catch (Exception ex)
            {
                throw;
            }
            return ds;
        }

        public long AddUpdateTallyVoucher(Ent_Acd_TallyConfig ObjTC, ref string Message)
        {
            long pkid = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@TallyVoucherTypeId", ObjTC.TallyVoucherTypeId);
                objParams[1] = new SqlParameter("@CashTallyVoucherName", ObjTC.CashTallyVoucherName);
                objParams[2] = new SqlParameter("@BankTallyVoucherName", ObjTC.BankTallyVoucherName);
                objParams[3] = new SqlParameter("@CollegeId", ObjTC.CollegeId);
                objParams[4] = new SqlParameter("@CreatedBy", ObjTC.CreatedBy);

                objParams[5] = new SqlParameter("@IPAddress", ObjTC.IPAddress);
                objParams[6] = new SqlParameter("@MACAddress", ObjTC.MACAddress);
                objParams[7] = new SqlParameter("@R_Out", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_Tally_InsertUpdateTallyVoucher", objParams, true);

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
