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
    namespace NITPRM
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class ScheduleDefineController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>
                //private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


                public string _client_constr = string.Empty;
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public ScheduleDefineController()
                {
                }
                public ScheduleDefineController(string DbPassword, string DbUserName, String DataBase)
                {


                    _client_constr = "Password=" + DbPassword + ";User ID=" + DbUserName + "; SERVER=" + HttpContext.Current.Session["Server"].ToString().Trim() + ";DataBase=" + DataBase + ";";


                }


                public DataSet GetScheduleDefinations(object code_year,string isDetail)
                {
                    //DataTableReader dtr = null;
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = new SqlParameter[2];

                        objParams[0] = new SqlParameter("@P_CODE_YEAR", code_year);
                        objParams[1] = new SqlParameter("@P_ISDETAIL", isDetail);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACC_GET_SCHEDULE_DEFINATION", objParams).Tables[0].DataSet;

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MainGroupController.GetMainGroup-> " + ex.ToString());
                    }
                    return ds;
                }
            }

        }//END: BusinessLayer.BusinessLogic

    }//END: UAIMS  

}//END: IITMS