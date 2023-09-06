using System;
using System.Text;
using System.Web;
using IITMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data;
using System.Data.SqlClient;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
           
            public class DepreciationController
            {
                public string _client_constr = string.Empty;
                 string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DepreciationController()
                {
                }
                public DepreciationController(string DbUserName, string DbPassword, String DataBase)
                {
                    _client_constr = "Password=" + DbPassword + ";User ID=" + DbUserName + "; SERVER=" + HttpContext.Current.Session["Server"].ToString().Trim() + ";DataBase=" + DataBase + ";";
                }


                public int DepreciationAdd(Depreciation objDepreciation,string compcode)
                {
                    int RetVal = 0;
                    DataSet dsDepreciation = new DataSet();
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        

                        objParams = new SqlParameter[6];

                        objParams[0] = new SqlParameter("@P_COMPCODE", compcode);
                        objParams[1] = new SqlParameter("@P_PARTY_NO", objDepreciation.Party_No);
                        objParams[2] = new SqlParameter("@P_RATE", objDepreciation.Rate);
                        objParams[3] = new SqlParameter("@P_FROMDATE", objDepreciation.FromDate.ToString("dd-MMM-yyyy"));
                        objParams[4] = new SqlParameter("@P_FIRSTDATE", objDepreciation.ToDate.ToString("dd-MMM-yyyy"));
                        objParams[5] = new SqlParameter("@P_SECONDDATE", objDepreciation.SecondDate.ToString("dd-MMM-yyyy"));
                        dsDepreciation = objSQLHelper.ExecuteDataSetSP("PKG_ACC_DEPRECIATION_RATE", objParams);
                        RetVal = 1;

                    }
                    catch (Exception ex)
                    {

                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddAccountDetails-> " + ex.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddAccountDetails-> " + ex.ToString());

                    }
                    return RetVal;
                }


                public int SetDepreciationBlank(string compcode)
                {
                    int RetVal = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        objSQLHelper.ExecuteNonQuery("delete from ACC_"+compcode+"_DEPRECIATION");
                        RetVal = 1;
                    }
                    catch (Exception ex)
                    {

                        //throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddAccountDetails-> " + ex.ToString());
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountTransactionController.AddAccountDetails-> " + ex.ToString());
                        
                    }
                    return RetVal;
                }
            }
        }
    }
}
