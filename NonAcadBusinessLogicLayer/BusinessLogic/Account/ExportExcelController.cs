using System;
using System.Data;
using System.Web;
using IITMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class ExportExcelController
            {
                public string _client_constr = string.Empty;
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public ExportExcelController()
                {
                }
                public ExportExcelController(string DbUserName, string DbPassword, String DataBase)
                {
                    _client_constr = "Password=" + DbPassword + ";User ID=" + DbUserName + "; SERVER=" + HttpContext.Current.Session["Server"].ToString().Trim() + ";DataBase=" + DataBase + ";";
                }

                public DataSet GetDataForExcelBankBook(string compcode,string ledger,string fromDate,string toDate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_CODE_YEAR", compcode);
                        objParams[1] = new SqlParameter("@P_LEDGER", ledger);
                        objParams[2] = new SqlParameter("@P_FROMDATE", fromDate);
                        objParams[3] = new SqlParameter("@P_TODATE", toDate);
                        objParams[4] = new SqlParameter("@P_College_Code", "0");
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_SP_LEDGER_BOOK_CONDENSED_NEW", objParams);
                    }
                    catch (Exception ex)
                    {

                    }
                    return ds;
                }

            }
        }
    }
}
