using System;
using System.Data;
using System.Web;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;
using System.Data.SqlTypes;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class NewsPaperController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>

                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                #region STORE_news

                public int AddNewsPaper(string newsPaperName,int cityNo,string collegeCode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New NewsPaper
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_NAME", newsPaperName);
                        objParams[1] = new SqlParameter("@P_CITYNO", cityNo);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        objParams[3] = new SqlParameter("@P_NNO", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_NEWS_INSERT", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.NewsPaperController.AddNewsPaper-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateNewsPaper(string newsPaperName, int cityNo, string collegeCode,int nNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //update  NewsPaper 
                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_NNO",nNo);
                        objParams[1] = new SqlParameter("@P_NAME",newsPaperName);
                        objParams[2] = new SqlParameter("@P_CITYNO",cityNo);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_STR_NEWS_UPDATE", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.NewsPaperController.UpdateNewsPaper-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetAllNewPaper()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[0];
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_NEWSPAPER_GET_ALL", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.NewsPaperController.GetAllNewPaper-> " + ex.ToString());
                    }
                    return ds;
                }

                public DataSet GetSingleNewPaper(int nNo)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null; ;
                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_NNO", nNo);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STR_NEWS_GET_BY_NO", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.NewsPaperController.GetSingleRecordBudget-> " + ex.ToString());
                    }
                    return ds;
                }

                #endregion
            }
        }
    }
}
