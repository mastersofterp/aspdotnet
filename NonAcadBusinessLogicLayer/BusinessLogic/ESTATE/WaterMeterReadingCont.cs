using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using IITMS.UAIMS;

namespace IITMS.UAIMS.BusinessLogicLayer.BusinessLogic
{
    public class WaterMeterReadingCont
    {
        string _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet GetEnfoWaterMeterReadUpdate()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTATE_WATER_SHOWREADING_UPDATE", objParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return ds;

        }
        
        public DataSet GetEnfoWaterMeterRead( string Pdate)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_connectionString);
                SqlParameter[] objParams = new SqlParameter[]
                    {
                        new  SqlParameter("@P_date",Pdate)
                    };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ESTATE_WATER_SHOWREADING", objParams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return ds;
        }



        public int InsertMonthlyReading(WaterMeterReading objMeReEn)
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
                    new SqlParameter("@P_OUT",           objMeReEn.CheckIDNO),
                   
                };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSqlhelper.ExecuteNonQuerySP("PKG_ESTATE_MONTHLY_WMETERREADING", sqlParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    satatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    satatus = Convert.ToInt32(CustomStatus.Error);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return satatus;
        }

    }
}

