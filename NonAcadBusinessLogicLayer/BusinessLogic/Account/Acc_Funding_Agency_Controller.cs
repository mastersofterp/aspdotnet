using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IITMS.SQLServer.SQLDAL;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using BusinessLayer.BusinessEntities;



namespace IITMS
{
    namespace UAIMS
    {
        namespace NonAcadBusinessLogicLayer.BusinessLogic.Account
        {

            public class Acc_Funding_Agency_Controller
            {
                public string _client_constr = string.Empty;
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public Acc_Funding_Agency_Controller()
                {
                    // Blank Controller
                }

                public Acc_Funding_Agency_Controller(string DbUserName, string DbPassword, String DataBase)
                {
                    _client_constr = "Password=" + DbPassword + ";User ID=" + DbUserName + "; SERVER=" + HttpContext.Current.Session["Server"].ToString().Trim() + "; DataBase=" + DataBase + ";";
                }

                public int AddFunding(Funding_Agency_Entity objFAE)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_Funding_Agency", objFAE.FAGENCY);
                        objParams[1] = new SqlParameter("@P_AgencyId", objFAE.AGENCYID);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                         object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_FUNDING_AGENCY_NEW", objParams, true);
                           // retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            if (Convert.ToInt32(ret) == -99)
                                retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (Convert.ToInt32(ret) == 2)
                            {
                                retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                            }
                            else
                                retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountPassingAuthorityController.AddPassAuthority->" + ex.ToString());
                    }
                    return retstatus;
               }



                public int UpdateFunding(Funding_Agency_Entity objFAE)
                {
                    int retstatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_PANAME", objFAE.FAGENCY);
                       // objParams[0] = new SqlParameter("@P_AGENCY_ID", LNO);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACC_FUNDING_AGENCY", objParams, false) != null)
                            retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retstatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountPassingAuthorityController.UpdatePassAuthority->" + ex.ToString());
                    }
                    return retstatus;
                }



            }


        }
    }
}