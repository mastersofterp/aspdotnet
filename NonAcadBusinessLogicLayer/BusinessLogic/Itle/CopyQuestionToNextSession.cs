using System;
using System.Data;
using System.Web;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using IITMS.NITPRM;
using System.Web.UI.WebControls;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
          public  class CopyQuestionToNextSession
            {
              string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
              #region Copy Question

              public DataSet GetQuestion(CopyQuestionToNextSessionEnt objQuest)
              {
                  DataSet ds = null;
                  try
                  {
                      SQLHelper objSH = new SQLHelper(_nitprm_constr);
                      SqlParameter[] objParams = null;
                      objParams = new SqlParameter[4];
                      objParams[0] = new SqlParameter("@P_COURSENO", objQuest.COURSENO);
                      objParams[1] = new SqlParameter("@P_UA_NO", objQuest.UA_NO);
                      objParams[2] = new SqlParameter("@P_TOPIC", objQuest.TOPIC);       
                      objParams[3] = new SqlParameter("@P_OBJECTIVE_DESCRIPTIVE_TYPE", objQuest.OBJECTIVE_DESCRIPTIVE);

                      ds = objSH.ExecuteDataSetSP("PKG_ITLE_IQUESTION_GET_ALL_FOR_COPY", objParams);
                  }
                  catch (Exception ex)
                  {
                  }
                  return ds;
              }


              public int AddIQuestionToCourse(CopyQuestionToNextSessionEnt objQuest)
                {
                 int retStatus = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_COURSENO", objQuest.COURSENO);
                        objParams[1] = new SqlParameter("@P_COPYQUESTIONS_TBL", objQuest.QuestionTbl_TRAN);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_COPY_IQUESTIONBANK_INSERT", objParams, true);

                        if (obj != null && obj.ToString().Equals("-99"))
                        {
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        }
                        else
                        {
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                    }
                    catch (Exception ex)
                    {
                        retStatus = -99;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EmpApplyController.AddUpdatePersonalDetails-> " + ex.ToString());
                    }
                    return retStatus;
                }

              #endregion
            }
        }
    }
}
