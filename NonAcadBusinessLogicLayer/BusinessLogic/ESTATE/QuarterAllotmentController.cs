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
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Web;


namespace IITMS.UAIMS.BusinessLogicLayer.BusinessLogic
{
    public class QuarterAllotmentController
    {
        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        //new SqlParameter("@P_OCCUPIED_DT",objMat.occuDate),


        public int AddQuarterAllotment(QuarterAllotment objMat)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[16];
                {
                    objParams[0] = new SqlParameter("@P_NAME_ID", objMat.Name);
                    objParams[1] = new SqlParameter("@P_DESIG_ID", objMat.Designation);
                    objParams[2] = new SqlParameter("@P_DEPT_ID", objMat.Department);
                    objParams[3] = new SqlParameter("@P_ALLOTORDER_NO", objMat.AllotOrderNo);
                    objParams[4] = new SqlParameter("@P_OFFCE_ORDER_DT", objMat.OffceOrderDt);
                    objParams[5] = new SqlParameter("@P_ALLOTMENT_DT", objMat.AllotmentDate);
                    if (objMat.occuDate == DateTime.MinValue)
                    {
                        objParams[6] = new SqlParameter("@P_OCCUPIED_DT", DBNull.Value);
                    }
                    else
                    {
                        objParams[6] = new SqlParameter("@P_OCCUPIED_DT", objMat.occuDate);
                    }
                    objParams[7] = new SqlParameter("@P_EMPTYPE_ID", objMat.EmployeeType);
                    objParams[8] = new SqlParameter("@P_QRT_RENT", objMat.QuarterRent);
                    objParams[9] = new SqlParameter("@P_QUARTER_TYPE", objMat.QuarterNo);
                    objParams[10] = new SqlParameter("@P_QUARTER_NO", objMat.QID);
                    objParams[11] = new SqlParameter("@P_STATUS", objMat.MId);
                    objParams[12] = new SqlParameter("@P_TOTAL_MEMBERS", objMat.TOTAL_MEMBERS); 
                    objParams[13] = new SqlParameter("@P_QA_ID", objMat.QA_ID);

                    objParams[14] = new SqlParameter("@P_Remark", objMat.Remark);                     // Added on 21/03/2022

                    //objParams[14] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    //objParams[14].Direction = ParameterDirection.Output;

                    objParams[15] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    objParams[15].Direction = ParameterDirection.Output;

                    object obj = objSQLHelper.ExecuteNonQuerySP("PKG_EST_VACANT_QTR_ALLOTMENT", objParams, true); 
                    //object obj = objSQLHelper.ExecuteNonQuerySP("PKG_EST_VACANT_QTR_ALLOTMENT_07032022", objParams, true);
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

        //public int AddQuarterAllotment(QuarterAllotment objMat)
        //{
        //    int status = 0;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(_connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[]                      	
        //        {   

        //            new SqlParameter("@P_NAME_ID",objMat.Name),
        //            new SqlParameter("@P_DESIG_ID",objMat.Designation),
        //            new SqlParameter("@P_DEPT_ID",objMat.Department),
        //            new SqlParameter("@P_ALLOTORDER_NO",objMat.AllotOrderNo),
        //            new SqlParameter("@P_OFFCE_ORDER_DT",objMat.OffceOrderDt),
        //            new SqlParameter("@P_ALLOTMENT_DT",objMat.AllotmentDate),
        //          if(objMat.occuDate == DateTime.MinValue)
        //          {
        //              new SqlParameter("@P_OCCUPIED_DT",objMat.occuDate),
        //          }
        //          else
        //          {
        //            new SqlParameter("@P_OCCUPIED_DT",objMat.occuDate),
        //          }

        //            new SqlParameter("@P_QRT_RENT",objMat.QuarterRent),
        //            new SqlParameter("@P_QUARTER_TYPE",objMat.QuarterNo),
        //            new SqlParameter("@P_QUARTER_NO",objMat.QID),                     
        //            new SqlParameter("@P_OUTPUT",objMat.MId)        
        //        };
        //        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

        //        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_EST_QTR_ALLOTMENT", sqlParams, true);

        //        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
        //            status = Convert.ToInt32(CustomStatus.RecordSaved);
        //        else
        //            status = Convert.ToInt32(CustomStatus.Error);
        //    }
        //    catch (Exception ex)
        //    {
        //        status = Convert.ToInt32(CustomStatus.Error);
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.QuarterAllotmentController.AddQuarterAllotment() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return status;
        //}



        public DataSet getQuaterInfo(int QID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_QA_ID",QID)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ESTATE_GETQUATERALLOTINFO", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.AdmissionCancellationController.GetClearanceInfo() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet GetQuarterList(int NAME_ID, int QRT_TYP_NO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_NAME_ID", NAME_ID),
                    new SqlParameter("@P_QRT_TYP_NO", QRT_TYP_NO)
                };
                ds = objDataAccess.ExecuteDataSetSP("PKG_ESTATE_GETQUATERLIST", sqlParams);

            }
            catch (Exception ex)
            {
            }
            return ds;

        }



        public int DeleteMeterAlot(int MID)
        {

            int status = 0;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
              
                    
                    new SqlParameter("@P_MID",MID),
                    new SqlParameter("@P_OUTPUT",MID)

                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("Pkg_ESTATE_DELETE_ADDMETER", sqlParams, true);

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

        public int AddMeterAllot(QuarterAllotment objMat)
        {
            int status = 0;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[] 
                {
              
                    new SqlParameter("@P_QUARTER_TYPE",objMat.QuarterType),
                    new SqlParameter("@P_QUARTER_NO",objMat.QuarterNo),
                    new SqlParameter("@P_WATERMETERTYPE",objMat.Watermetertype), 
                    new SqlParameter("@P_WATERMETER_NO",objMat.WaterMeterNo),
                    new SqlParameter("@P_ELEMETER_TYPE",objMat.EneryMetertye),
                    new SqlParameter("@P_EMETER_NO",objMat.MeterNo),
                    new SqlParameter("@P_QID",objMat.QID),
                    new SqlParameter("@P_OUTPUT",objMat.MId)

                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ESTATE_ADDMETER", sqlParams, true);

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


        public DataSet FillEmployeeName(string prefixText)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_SEARCHTEXT", prefixText);
                objParams[1] = new SqlParameter("@P_AAPLICANT_TYPE", HttpContext.Current.Session["ApplicantType"].ToString());
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTATE_GET_EMPLOYEE_NAME", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.QuarterAllotmentController.FillEmployeeName()-> " + ex.ToString());
            }

            return ds;
        }

    }
}