using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;

namespace IITMS.UAIMS.BusinessLogicLayer.BusinessLogic
{
    public class QuarterOccupantController
    {
        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddQuarterOccupant(Quarter_Occupant objMat)
        {
            int status = 0;
            try
            {

                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_EMPTYPE_ID",objMat.EmpID),
                    new SqlParameter("@P_OCCUPANT_ID",objMat.OccupantName),
                    new SqlParameter("@P_ORDDT",objMat.OfficeOrder_Dt),
                    new SqlParameter("@P_ALLOT_DT",objMat.Allotment_Dt),
                    new SqlParameter("@P_QTRID",objMat.QrtType_Id),
                    new SqlParameter("@P_QNO",objMat.Qrt_No),
                    new SqlParameter("@P_Material_ID",objMat.Material),
                    new SqlParameter("@P_QTY",objMat.Quantity),
                    new SqlParameter("@P_QA_ID",objMat.QA_ID),
                    new SqlParameter("@P_OUTPUT",objMat.MNO)

                
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_EST_QTR_OCCUPANT", sqlParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MaterialMasterController.AddMaterialType() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
      

        public DataSet FillOccupantName(int EMPTYPE_ID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_EMPTYPE_ID", EMPTYPE_ID);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTATE_GET_OCCUPANT_NAME", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TPController.GetLetter->" + ex.ToString());
            }
            return ds;
        }



        // public int AddMaterial(Quarter_Occupant objMat)
        // {

        //    int status = 0;
        //    try
        //    { 
                 
        //           SQLHelper objSQLHelper = new SQLHelper(_connectionString);
        //           SqlParameter[] sqlParams = new SqlParameter[] 
        //           { 
        //            new SqlParameter("@P_NAMEID",objMat.OccupantName),
        //            new SqlParameter("@P_MNO",objMat.Material),
        //            new SqlParameter("@P_QNT",objMat.Quantity),
        //            new SqlParameter("@P_OUTPUT",objMat.MNO)

        //           };
        //        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

        //        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ESTATE_ADDMATERIAL",sqlParams, true);

        //        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
        //            status = Convert.ToInt32(CustomStatus.RecordSaved);
                
        //        else if(obj != null && obj.ToString() == "-1001")
        //            status = Convert.ToInt32(CustomStatus.DuplicateRecord);
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

    }

   
} 


