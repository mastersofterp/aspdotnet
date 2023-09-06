using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;


namespace IITMS.UAIMS.BusinessLogicLayer.BusinessLogic
{

    public class QuarterVacantCont
    {

        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        //public int AddQuarterVacant(QuarterVacant objMat)
        //{
        //    int status = 0;

        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParam = new SqlParameter[] 
        //        {
        //            new SqlParameter("@P_EMPTYPE_ID",objMat.EmpID),
        //            new SqlParameter("@P_VNAME_ID",objMat.NameOfVacator),
        //            new SqlParameter("@P_QRTTYPE_ID ",objMat.QuarterType),
        //            new SqlParameter("@P_QRTNO",objMat.QuarterNo),
        //            new SqlParameter("@P_VORDER_NO ",objMat.VacationOrderNo),
        //            new SqlParameter("@P_DTOF_VACATION",objMat.VacationDT),

        //            new SqlParameter("@P_OFFCE_ORDER_DT ",objMat.OffceOrdetDt),
        //            new SqlParameter("@P_MATERIAL_ID",objMat.MaterialId),
        //            new SqlParameter("@P_ALLOT_QTY ",objMat.AlloterQTY),
        //            new SqlParameter("@P_SHORT_QTY",objMat.ShortQTY),
        //            new SqlParameter("@P_FINE ",objMat.Fine),
        //            new SqlParameter("@P_FINE_REMARK",objMat.Fine_Remark),
        //            new SqlParameter("@P_OUTPUT",objMat.MNO)
                    

        //        };
        //        sqlParam[sqlParam.Length - 1].Direction = ParameterDirection.InputOutput;

        //        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_EST_QRT_VACANT", sqlParam, true);

        //        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
        //            status = Convert.ToInt32(CustomStatus.RecordSaved);

        //        else
        //            status = Convert.ToInt32(CustomStatus.Error);

        //    }
        //    catch (Exception ex)
        //    {
        //        status = Convert.ToInt32(CustomStatus.Error);
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.QuarterVacantCont.AddQuarterVacant() --> " + ex.Message + " " + ex.StackTrace);

        //    }

        //    return status;
        //}

        public int AddQuarterVacant(QuarterVacant objMat)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[14];
                {
                    objParams[0] = new SqlParameter("@P_EMPTYPE_ID", objMat.EmpID);
                    objParams[1] = new SqlParameter("@P_VNAME_ID",objMat.NameOfVacator);
                    objParams[2] = new SqlParameter("@P_QRTTYPE_ID ", objMat.QuarterType);
                    objParams[3] = new SqlParameter("@P_QRTNO", objMat.QuarterNo);
                    objParams[4] = new SqlParameter("@P_VORDER_NO ", objMat.VacationOrderNo);
                    objParams[5] = new SqlParameter("@P_DTOF_VACATION", objMat.VacationDT);
                    if (objMat.OffceOrdetDt == DateTime.MinValue)
                    {
                        objParams[6] = new SqlParameter("@P_OFFCE_ORDER_DT", DBNull.Value);
                    }
                    else
                    {
                        objParams[6] = new SqlParameter("@P_OFFCE_ORDER_DT", objMat.OffceOrdetDt);
                    }
                    objParams[7] = new SqlParameter("@P_MATERIAL_ID", objMat.MaterialId);
                    objParams[8] = new SqlParameter("@P_ALLOT_QTY ", objMat.AlloterQTY);
                    objParams[9] = new SqlParameter("@P_SHORT_QTY", objMat.ShortQTY);
                    objParams[10] = new SqlParameter("@P_FINE ", objMat.Fine);
                    objParams[11] = new SqlParameter("@P_FINE_REMARK", objMat.Fine_Remark);
                    objParams[12] = new SqlParameter("@P_MNO", objMat.MNO);
                    objParams[13] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    objParams[13].Direction = ParameterDirection.Output;

                    object obj = objSQLHelper.ExecuteNonQuerySP("PKG_EST_QRT_VACANT", objParams, true);
                    if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    {
                        status = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    else
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.QuarterAllotmentController.AddQuarterAllotment() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        //public int AddShortMaterial(QuarterVacant objMat)
        //{

        //    int status = 0;
        //    try
        //    {

        //        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[] 
        //           { 
        //            new SqlParameter("@P_NAMEID",objMat.NameOfVacator),
        //            new SqlParameter("@P_MATERIAL_ID",objMat.MaterialId),
        //            new SqlParameter("@P_ALLOT_QTY",objMat.AlloterQTY),
        //            new SqlParameter("@P_SHORT_QTY",objMat.ShortQTY),
        //            new SqlParameter("@P_FINE",objMat.Fine),
        //            new SqlParameter("@P_FINE_REMARK",objMat.Fine_Remark),
        //            new SqlParameter("@P_OUTPUT",objMat.MNO)

        //           };
        //        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

        //        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_EST_QRT_ADDSHORT_MATERIAL", sqlParams, true);

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

    }

}
