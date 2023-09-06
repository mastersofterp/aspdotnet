using System;
using System.Data;
using System.Web;
using IITMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using IITMS.NITPRM;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class SponsorProjectController
            {
                public string _client_constr = string.Empty;
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                 public SponsorProjectController()
                {
                    //blank Constructor
                }

                 public SponsorProjectController(string DbUserName, string DbPassword, String DataBase)
                {
                    _client_constr = "Password=" + DbPassword + ";User ID=" + DbUserName + "; SERVER=" + HttpContext.Current.Session["Server"].ToString().Trim() + "; DataBase=" + DataBase + ";";
                }

                 // private string _client_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

               
                 public int AddUpdateProjectSubHeadName(SponserProject objSpoProj, string code_year)
                 {
                     int retStatus = Convert.ToInt32(CustomStatus.Others);

                     try
                     {
                         SQLHelper objSQLHelper = new SQLHelper(connectionString);
                         SqlParameter[] objParams = null;


                         objParams = new SqlParameter[9];
                         objParams[0] = new SqlParameter("@P_ProjectSubId", objSpoProj.ProjectSubId);
                         objParams[1] = new SqlParameter("@P_ProjectSubHeadShort", objSpoProj.ProjectSubHeadShort);
                         objParams[2] = new SqlParameter("@P_ProjectSubHeadName", objSpoProj.ProjectSubHeadName);
                         objParams[3] = new SqlParameter("@P_CODE_YEAR", code_year);
                         //objParams[4] = new SqlParameter("@P_RES_TYPE", objSpoProj.ResType);
                         objParams[4] = new SqlParameter("@P_PROJECTID", objSpoProj.ProjectId);
                         objParams[5] = new SqlParameter("@P_EXPHEADTYPE", objSpoProj.ExpHeadType);
                         objParams[6] = new SqlParameter("@P_PARTY_NO", objSpoProj.Party_No);
                         objParams[7] = new SqlParameter("@P_SRNO", objSpoProj.SRNO);

                         objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                         objParams[8].Direction = ParameterDirection.Output;

                         object ret = objSQLHelper.ExecuteNonQuerySP("ACC_PROJECT_SUBHEAD_NAME_INS_UPD", objParams, true);
                         if (ret != null)
                         {
                             if (ret.ToString().Equals("1"))
                                 retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                             else if (ret.ToString().Equals("2"))
                                 retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                         }

                     }
                     catch (Exception ee)
                     {
                         throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ee.ToString());
                     }
                     return retStatus;
                 }

                 public int AddUpdateProjectSubHeadAllocation(SponserProject objSpoProj, string code_year)
                 {
                     int retStatus = Convert.ToInt32(CustomStatus.Others);

                     try
                     {
                         SQLHelper objSQLHelper = new SQLHelper(connectionString);
                         SqlParameter[] objParams = null;


                         objParams = new SqlParameter[8];
                         objParams[0] = new SqlParameter("@P_ProjectSubHeadAllocationId", objSpoProj.ProjectSubHeadAllocationId);
                         objParams[1] = new SqlParameter("@P_ProjectId", objSpoProj.ProjectId);
                         objParams[2] = new SqlParameter("@P_ProjectSubId", objSpoProj.ProjectSubId);
                         objParams[3] = new SqlParameter("@P_TotAmtReceived", objSpoProj.TotAmtReceived);
                         objParams[4] = new SqlParameter("@P_TotAmtSpent", objSpoProj.TotAmtSpent);
                         objParams[5] = new SqlParameter("@P_TotAmtRemain", objSpoProj.TotAmtRemain);
                         objParams[6] = new SqlParameter("@P_CODE_YEAR", code_year);

                         objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                         objParams[7].Direction = ParameterDirection.Output;

                         object ret = objSQLHelper.ExecuteNonQuerySP("ACC_PROJECT_SUBHEAD_ALLOCATION_INS_UPD", objParams, true);
                         if (ret != null)
                         {
                             if (ret.ToString().Equals("1"))
                                 retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                             else if (ret.ToString().Equals("2"))
                                 retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                         }

                     }
                     catch (Exception ee)
                     {
                         throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ee.ToString());
                     }
                     return retStatus;
                 }

                 public int AddUpdateProjectName(SponserProject objSpoProj, string code_year)
                 {
                     int retStatus = Convert.ToInt32(CustomStatus.Others);

                     try
                     {
                         SQLHelper objSQLHelper = new SQLHelper(connectionString);
                         SqlParameter[] objParams = null;

                         //Add New MainGroup Group
                         objParams = new SqlParameter[13];
                         objParams[0] = new SqlParameter("@P_PROJECTID", objSpoProj.ProjectId);
                         objParams[1] = new SqlParameter("@P_PROJECTSHORTNAME", objSpoProj.ProjectShortName);
                         objParams[2] = new SqlParameter("@P_PROJECTNAME", objSpoProj.ProjectName);
                         objParams[3] = new SqlParameter("@P_CODE_YEAR", code_year);

                         objParams[4] = new SqlParameter("@P_DEPARTMENT", objSpoProj.Department);
                         objParams[5] = new SqlParameter("@P_SANCTIONBY", objSpoProj.SanctionBy);
                         objParams[6] = new SqlParameter("@P_SCHEME", objSpoProj.Scheme);
                         objParams[7] = new SqlParameter("@P_COORDINATOR", objSpoProj.Coordinator);
                         objParams[8] = new SqlParameter("@P_VALUE", objSpoProj.Value);
                         objParams[9] = new SqlParameter("@P_SANCTIONLETTER", objSpoProj.SanctionLetter);
                         objParams[10] = new SqlParameter("@P_DATE", objSpoProj.Date);
                         objParams[11] = new SqlParameter("@P_PARTYNO", objSpoProj.Party_No);
                         objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                         objParams[12].Direction = ParameterDirection.Output;

                         object ret = objSQLHelper.ExecuteNonQuerySP("ACC_PROJECT_NAME_INS_UPD", objParams, true);
                         if (ret != null)
                         {
                             if (ret.ToString().Equals("1"))
                                 retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                             else if (ret.ToString().Equals("2"))
                                 retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                         }

                     }
                     catch (Exception ee)
                     {
                         throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ee.ToString());
                     }
                     return retStatus;
                 }



                 public int UpdateProjectSubHeadAllocation(SponserProject objSpoProj, string code_year)
                 {
                     int retStatus = Convert.ToInt32(CustomStatus.Others);

                     try
                     {
                         SQLHelper objSQLHelper = new SQLHelper(connectionString);
                         SqlParameter[] objParams = null;


                         objParams = new SqlParameter[8];
                         objParams[0] = new SqlParameter("@P_ProjectSubHeadAllocationId", objSpoProj.ProjectSubHeadAllocationId);
                         objParams[1] = new SqlParameter("@P_ProjectId", objSpoProj.ProjectId);
                         objParams[2] = new SqlParameter("@P_ProjectSubId", objSpoProj.ProjectSubId);
                         objParams[3] = new SqlParameter("@P_TotAmtReceived", objSpoProj.TotAmtReceived);
                         objParams[4] = new SqlParameter("@P_TotAmtSpent", objSpoProj.TotAmtSpent);
                         objParams[5] = new SqlParameter("@P_TotAmtRemain", objSpoProj.TotAmtRemain);
                         objParams[6] = new SqlParameter("@P_CODE_YEAR", code_year);

                         objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                         objParams[7].Direction = ParameterDirection.Output;

                         object ret = objSQLHelper.ExecuteNonQuerySP("ACC_PROJECT_SUBHEAD_ALLOCATION_UPD", objParams, true);
                         if (ret != null)
                         {
                             if (ret.ToString().Equals("1"))
                                 retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                             else if (ret.ToString().Equals("2"))
                                 retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                         }

                     }
                     catch (Exception ee)
                     {
                         throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ee.ToString());
                     }
                     return retStatus;
                 }

            }
        }
    }
}
