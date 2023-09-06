using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public  class WeightController
    {
        private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddWeight(weight objweight)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_POSTTYPENO", objweight.PostType);
                objParams[1] = new SqlParameter("@P_WEIGHTFROM", objweight.WeightFrom);
                objParams[2] = new SqlParameter("@P_WEIGHTTO", objweight.WeightTo);
                objParams[3] = new SqlParameter("@P_UNIT", objweight.Unit);
                objParams[4] = new SqlParameter("@P_COST", objweight.Cost);
                objParams[5] = new SqlParameter("@P_COLLEGE_CODE", objweight.College_code);
                objParams[6] = new SqlParameter("@P_CREATOR", objweight.Creator);
                objParams[7] = new SqlParameter("@P_CREATED_DATE", objweight.Created_Date);
                objParams[8] = new SqlParameter("@P_WEIGHTNO", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_IO_WEIGHT_INSERT", objParams, true);

                if (obj != null && obj.ToString().Equals("-99"))
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
            }
            catch (Exception ex)
            {
                retStatus = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.WeightController.AddWeight-> " + ex.ToString());

            }
            return retStatus;
        }
        public int UpdateWeight(weight objWeight)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_WEIGHTNO ", objWeight.WeightNo);
                objParams[1] = new SqlParameter("@P_WEIGHTFROM", objWeight.WeightFrom);
                objParams[2] = new SqlParameter("@P_WEIGHTTO", objWeight.WeightTo);
                objParams[3] = new SqlParameter("@P_UNIT", objWeight.Unit);
                objParams[4] = new SqlParameter("@P_COST", objWeight.Cost);
                objParams[5] = new SqlParameter("@P_POSTTYPENO", objWeight.PostType);
                objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objWeight.College_code);

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_IO_WEIGHT_UPDATE", objParams, true);

                if (obj != null && obj.ToString().Equals("-99"))
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
            }
            catch (Exception ex)
            {
                retStatus = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.WeightController.UpdateWeight-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetAllWeight()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[0];
                ds = objSQLHelper.ExecuteDataSetSP("PKG_IO_WEIGHT_GET_ALL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.WeightController.GetAllWeight-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetWeightByWeightNo(int WeightNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_WEIGHTNO", WeightNo);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_IO_WEIGHT_GET_BY_NO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.WeightController.GetWeightByWeightNo -> " + ex.ToString());
            }

            return ds;
        }
        // Modified By Swati ---Date:( 21-3-2014)
        public DataSet GetWeightByWeightRange(double weight1, double weight2, int unit, int posttypeno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_WEIGHTFROM", weight1);
                objParams[1] = new SqlParameter("@P_WEIGHTTO", weight2);
                objParams[2] = new SqlParameter("@P_UNIT", unit);
                objParams[3] = new SqlParameter("@P_POSTTYPENO", posttypeno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_IO_WEIGHT_GET_BY_RANGE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.WeightController.GetWeightByWeightNo -> " + ex.ToString());
            }

            return ds;
        }
        // cREATED By Swati ---Date:( 21-3-2014)
        public DataSet GetWeightByWeightNoAndRange(double weight1, double weight2, int unit, int posttypeno, int weightno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);

                SqlParameter[] objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_WEIGHTFROM", weight1);
                objParams[1] = new SqlParameter("@P_WEIGHTTO", weight2);
                objParams[2] = new SqlParameter("@P_UNIT", unit);
                objParams[3] = new SqlParameter("@P_POSTTYPENO", posttypeno);
                objParams[4] = new SqlParameter("@P_WEIGHTNO", weightno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_IO_WEIGHT_GET_BY_RANGE_WNO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.WeightController.GetWeightByWeightNo -> " + ex.ToString());
            }

            return ds;
        }

        public int DeleteWeight(int WeightNo, int IdNo)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_WEIGHTNO", WeightNo);
                objSQLHelper.ExecuteNonQuerySP("PKG_ADMN_IO_SP_DEL_WEIGHT", objParams, false);
                retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.WeightController.DeleteWeight-> " + ex.ToString());
            }
            return Convert.ToInt32(retStatus);
        }
    }
        }
    }
}