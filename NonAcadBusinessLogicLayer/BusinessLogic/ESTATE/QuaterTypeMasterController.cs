
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;

namespace IITMS.UAIMS.BusinessLayer.BusinessEntities
{
    public class QuaterTypeMasterControlle 
    {
        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        //public int AddMaterialType(Masterial_Master objMat)
        //{
        //    int status = 0;
        ////    try
        ////    {
        //        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[]
        //        {
        //            new SqlParameter("@P_MATERIALNAME",objMat.MaterialName),
        //            new SqlParameter("@P_OUTPUT", objMat.MaterialNo)
        //        };
        //        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

        //        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ESTATE_ADDMATERIAL_MASTER", sqlParams, true);

        //        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
        //            status = Convert.ToInt32(CustomStatus.RecordSaved);
        //        else
        //            status = Convert.ToInt32(CustomStatus.Error);
        //    }
        //    catch (Exception ex)
        //    {
        //        status = Convert.ToInt32(CustomStatus.Error);
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MaterialMasterController.AddMaterialType() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return status;
        //}

        public int AddQuarterType(QuaterType_Master objMat)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_QTRTYPE",objMat.QrtType),
                    new SqlParameter("@P_RENT",objMat.Rent),
                    new SqlParameter("@P_QAREA",objMat.QArea),
                    new SqlParameter("@P_ESTAFF", objMat.eStaff),
                    new SqlParameter("@P_FIXED_CHARGE", objMat.fixedCharge),
                    new SqlParameter("@P_OUTPUT", objMat.QrtTypeNo)                    
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ESTATE_ADDQUARTERTYPE_MASTER", sqlParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.QuaterTypeMasterControlle.AddQuarterType() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
    }
}
