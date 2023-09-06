//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : STUDENT DETEND CONTROLLER                   
// CREATION DATE : 20-JUNE-2009                                                          
// CREATED BY    : SANJAY RATNAPARKHI                                                   
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class StudentDetend
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public DataSet GetSubjectDetails(int admbatchno, int branchno, int studid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        //objParams[0] = new SqlParameter("@P_ADMBATCHNO", admbatchno);
                        //objParams[1] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[0] = new SqlParameter("@P_STUDENTID", studid);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_STUDENT_SP_RET_COURSE_BYSTUDENT_DETEND", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudnetDetendController.GetSubjectDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by Pritish on date 31-05-2019
                /// </summary>

                public DataSet getCourseListBySection(int session, int term, int schemeno, int degreeeNo, int sectionNo, int Condition)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_TERM", term);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[3] = new SqlParameter("@P_DEGREENO", degreeeNo);
                        objParams[4] = new SqlParameter("@P_SECTIONNO", sectionNo);
                        objParams[5] = new SqlParameter("@P_CONDITION", Condition);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_COURSE_BY_SECTION", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudnetDetendController.GetSubjectDetails-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// Added by Pritish on date 31-05-2019
                /// </summary>
                /// <param name="session"></param>
                /// <param name="term"></param>
                /// <param name="schemeno"></param>
                /// <param name="degreeeNo"></param>
                /// <param name="sectionNo"></param>
                /// <param name="Condition"></param>
                /// <returns></returns>
                public DataSet getStudentListByCcode(int session, int term, int schemeno, int degreeeNo, int sectionNo, int Condition)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", session);
                        objParams[1] = new SqlParameter("@P_TERM", term);
                        objParams[2] = new SqlParameter("@P_SCHEMENO", schemeno);
                        objParams[3] = new SqlParameter("@P_DEGREENO", degreeeNo);
                        objParams[4] = new SqlParameter("@P_SECTIONNO", sectionNo);
                        objParams[5] = new SqlParameter("@P_CONDITION", Condition);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUDENT_BY_CCODE", objParams);

                    }
                    catch (Exception ex)
                    {

                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudnetDetendController.GetSubjectDetails-> " + ex.ToString());
                    }
                    return ds;
                }
            }
        }
    }
}
