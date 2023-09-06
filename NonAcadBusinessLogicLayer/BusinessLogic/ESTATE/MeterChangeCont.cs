using System;
using System.Collections.Generic;
using System.Linq;
using IITMS;
using System.Data.SqlClient;
using System.Data;
using IITMS.UAIMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;

namespace IITMS.UAIMS.BusinessLogicLayer.BusinessLogic
{
    public class MeterChangeCont
    {
       string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


       public DataSet getMeterChangeInfo(int QID)
       {
           DataSet ds = null;
           try
           {
               SQLHelper objDataAccess = new SQLHelper(_connectionString);
               SqlParameter[] sqlParams = new SqlParameter[]
                { 
                    new SqlParameter("@P_QA_ID",QID)
                };
               ds = objDataAccess.ExecuteDataSetSP("PKG_ESTATE_GETINFOMETERCHANGE", sqlParams);
           }
           catch (Exception ex)
           {
               throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.AdmissionCancellationController.GetClearanceInfo() --> " + ex.Message + " " + ex.StackTrace);
           }
           return ds;
       }


       public int MeterChangeEntry(MeterChange objConMas)
       {
           int status = 0;
           try
           {
               SQLHelper objSQLHelper = new SQLHelper(_connectionString);
               SqlParameter[] sqlParams = new SqlParameter[]
                     {
                          new SqlParameter("@p_CONSUMERTYPE_ID",objConMas.CONSUMERTYPE_ID),
                          new SqlParameter("@p_NAME_ID",objConMas.NAME_ID),
                          new SqlParameter("@p_QUATERTYPE" ,objConMas.QUATERTYPE),
                          new SqlParameter("@p_QUATERNO",objConMas.QUATERNO),
                          new SqlParameter("@p_M_CHANGE_DATE", objConMas.M_CHANGE_DATE),
                          new SqlParameter("@p_OLD_EN_MID", objConMas.OLD_EN_MID),
                          new SqlParameter("@p_OLD_EN_MTYPE", objConMas.OLD_EN_MTYPE),
                          new SqlParameter("@p_PREV_EN_MONTH_R", objConMas.PREV_EN_MONTH_R),
                          new SqlParameter("@p_CURRENT_EN_MONTH_R",objConMas.CURRENT_EN_MONTH_R),
                          new SqlParameter("@p_NEW_EN_MID",objConMas.NEW_EN_MID),
                          new SqlParameter("@p_NEW_EN_MTYPE",objConMas.NEW_EN_MTYPE),
                          new SqlParameter("@p_NEW_EN_MSTART_R",objConMas.NEW_EN_MSTART_R),
                          new SqlParameter("@p_DIFF_PREV_EN_METER_R",objConMas.DIFF_PREV_EN_METER_R),
                          new SqlParameter("@p_OLD_WA_MID",objConMas.OLD_WA_MID),
                          new SqlParameter("@p_OLD_WA_MTYPE",objConMas.OLD_WA_MTYPE),
                          new SqlParameter("@p_PREV_WA_MONTH_R",objConMas.PREV_WA_MONTH_R),
                          new SqlParameter("@p_CURRENT_WA_MONTH_R",objConMas.CURRENT_WA_MONTH_R),
                          new SqlParameter("@p_NEW_WA_MID",objConMas.NEW_WA_MID),
                          new SqlParameter("@p_NEW_WA_MTYPE",objConMas.NEW_WA_MTYPE),
                          new SqlParameter("@p_NEW_WA_MSTART_R",objConMas.NEW_WA_MSTART_R),
                          new SqlParameter("@p_DIFF_PREV_WA_METER_R",objConMas.DIFF_PREV_WA_METER_R),
                          new  SqlParameter("@P_QUATERTYPENO",objConMas.QUATERTYPENO),
                          new  SqlParameter("@P_QUATERNOID",objConMas.QUATERNOID),
                          new SqlParameter("@P_QA_ID",objConMas.QA_ID),
                          new SqlParameter("@P_OUTPUT",Convert.ToInt16("0"))
                         
                  };
               sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

               object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ESTATE_METERCHANGE", sqlParams, true);

               if (obj != null && obj.ToString() != "-99")
               {
                   status = Convert.ToInt32(CustomStatus.RecordSaved);
               }
               else
                   status = Convert.ToInt32(CustomStatus.Error);
           }
           catch (Exception ex)
           {
               status = Convert.ToInt32(CustomStatus.Error);
               throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Disciplinary_ActionController.AddEvent() --> " + ex.Message + " " + ex.StackTrace);
           }
           return status;
       }

    }
}
