using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;


namespace IITMS.UAIMS.BusinessLogicLayer.BusinessLogic
{
    public class EnergyMeterReadingCont
    {
        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet GetEnfoEnergyMeterRead(string date, EnergyMeterReading objMeReEn)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[]
                { 
                   new SqlParameter("@P_date",date),
                   new SqlParameter("@P_BLOCKID", objMeReEn.BLOCKID),
                };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTATE_ENERGY_SHOWREADING", objParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return ds;
        }

        public DataSet GetLockUnlockStatus(string date)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[]
                { 
                   new SqlParameter("@P_DATE",date),                  
                };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTATE_GET_LOCK_UNLOCK_STATUS", objParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return ds;
        }

        public DataSet GetEnfoEnergyMeterReadUpdate()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTATE_ENERGY_SHOWREADING_Update", objParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return ds;
        }

      


        public int InsertMonthlyReading(EnergyMeterReading objMeReEn)
        {
            int satatus = 0;
            try
            {
                SQLHelper objSqlhelper = new SQLHelper(_connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    
                    new SqlParameter("@P_IDNO",          objMeReEn.IDNO),
                    new SqlParameter("@P_NAMEID",        objMeReEn.NameId),
                    new SqlParameter("@P_QTRNO",         objMeReEn.QtrNo),
                    new SqlParameter("@P_METERNO",       objMeReEn.MeterNo),
                    new SqlParameter("@P_OLDREADING",    objMeReEn.OldReading),
                    new SqlParameter("@P_CURRENTREADING",objMeReEn.CurrentReading),
                    new SqlParameter("@P_ADJUNIT",       objMeReEn.AdjUnit),
                    new SqlParameter("@P_TOTAL",         objMeReEn.Total),
                    new SqlParameter("@P_CONSTATUS",     objMeReEn.ConStatus),
                    new SqlParameter("@P_DATE",          objMeReEn.Monthreading),
                    new SqlParameter("@P_READING_DATE",  objMeReEn.ReadingDate),
                    new SqlParameter("@P_QA_ID",         objMeReEn.QA_ID), 
                    new SqlParameter("@P_EMP_CODE",         objMeReEn.EMP_CODE), 
                    new SqlParameter("@P_BLOCKID",         objMeReEn.BLOCKID), 
                    new SqlParameter("@P_OUT",           objMeReEn.CheckIDNO),
                    
                   
                };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                object obj = objSqlhelper.ExecuteNonQuerySP("PKG_ESTATE_MONTHLY_METERREADING", sqlParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    satatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    satatus = Convert.ToInt32(CustomStatus.Error);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return satatus;
        }
    
    }
}
