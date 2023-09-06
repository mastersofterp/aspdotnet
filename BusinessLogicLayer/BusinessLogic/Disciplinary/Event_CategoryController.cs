
using System;
using System.Data;
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;

using IITMS.UAIMS.BusinessLayer.BusinessEntities;


namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
   public class Event_CategoryController
    {
      // string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["NITPRM"].ConnectionString;
       string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

       public int AddEventCat(Event_Category objEventCat)
       {
           int status = 0;
           try
           {
               SQLHelper objSQLHelper = new SQLHelper(connectionString);
               SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_ECATNAME",objEventCat.ECATNAME),                  
                    new SqlParameter("@P_COLLEGE_CODE", objEventCat.CCODE),
                    new SqlParameter("@P_ECATNO", DbType.Int32)
                };
               sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

               object obj = objSQLHelper.ExecuteNonQuerySP("DISCIPLINARY_EVENT_CAT_INSERT", sqlParams, true);

               if (obj != null && obj.ToString() != "-99")
                   status = Convert.ToInt32(CustomStatus.RecordSaved);
               else
                   status = Convert.ToInt32(CustomStatus.Error);
           }
           catch (Exception ex)
           {
               status = Convert.ToInt32(CustomStatus.Error);
               throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Event_CategoryController.AddEventCat() --> " + ex.Message + " " + ex.StackTrace);
           }
           return status;
       }
       public int UpdateEventCat(Event_Category objEventCat)
       {
           int status;
           try
           {
               SQLHelper objSQLHelper = new SQLHelper(connectionString);
               SqlParameter[] sqlParams = new SqlParameter[]
                {                    
                    new SqlParameter("@P_ECATNAME",objEventCat.ECATNAME),                                   
                    new SqlParameter("@P_ECATNO",objEventCat.ECATID),
                     new SqlParameter("@P_COLLEGE_CODE", objEventCat.CCODE)
                };
               sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

               object obj = objSQLHelper.ExecuteNonQuerySP("DISCIPLINARY_EVENT_CAT_UPDATE", sqlParams, true);

               if (obj != null && obj.ToString() != "-99")
                   status = Convert.ToInt32(CustomStatus.RecordUpdated);
               else
                   status = Convert.ToInt32(CustomStatus.Error);
           }
           catch (Exception ex)
           {
               status = Convert.ToInt32(CustomStatus.Error);
               throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.Event_CategoryController.UpdateEventCat() --> " + ex.Message + " " + ex.StackTrace);
           }
           return status;
       }
    }
}
