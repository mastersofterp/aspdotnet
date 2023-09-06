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
            public class SectionHeadController
            {
                public string _client_constr = string.Empty;
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public SectionHeadController()
                {
 
                }
                public SectionHeadController(string DbUserName, string DbPassword, String DataBase)
                {
                    _client_constr = "Password=" + DbPassword + ";User ID=" + DbUserName + "; SERVER=" + HttpContext.Current.Session["Server"].ToString().Trim() + "; DataBase=" + DataBase + ";";
                }

                // private string _client_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                public int AddSection(SectionHead objSectionEntity,string Comp_code, int college_code,int userid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(connectionString);
                        SqlParameter[] objParams = null;

                        //Add New Section
                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SectionNo", objSectionEntity.SectionNo);
                        objParams[1] = new SqlParameter("@P_SectionName", objSectionEntity.SectionName);
                        objParams[2] = new SqlParameter("@P_SectionPercent", objSectionEntity.SectionPer);
                        objParams[3] = new SqlParameter("@P_CODE_YEAR", Comp_code);
                        objParams[4] = new SqlParameter("@P_CreatedBy", userid);
                        objParams[5] = new SqlParameter("@P_College_Code", college_code);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACC_SECTION_INSERT", objParams, true);

                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }  
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AccountConfigurationController.AddUpdateConfiguration-> " + ex.ToString());
                    }
                    return retStatus;
                }
            }
        }
    }
}
