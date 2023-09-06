using System;
using System.Data;
using System.Web;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class Request_Allow_Retest
            { 
               
                string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


                public int Insert_ALlow_Retest(ITLE_RequestAllowrestest objreTest)
                {
                    int status = 0;
                    try
                    {

                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_COURSENAME", objreTest.CourseName);
                        objParams[1] = new SqlParameter("@P_COURSENO",objreTest.Courseno);
                        objParams[2] = new SqlParameter("@P_SUBID",objreTest.Subid);
                        objParams[3] = new SqlParameter("@P_TESTNO",objreTest.Testno);
                        //objParams[4] = new SqlParameter("@P_LECTURE",objreTest.Lecture);
                        //objParams[5] = new SqlParameter("@P_PRACTICLE",objreTest.Practicle);
                        //objParams[6] = new SqlParameter("@P_THEORY",objreTest.Theory);
                        objParams[4] = new SqlParameter("@P_STUD_IDNO",objreTest.Stud_idno);
                        objParams[5] = new SqlParameter("@P_ROLL_NO", objreTest.Roll_no);
                        objParams[6] = new SqlParameter("@P_BATCHNAME",objreTest.BatchName);
                       // objParams[10] = new SqlParameter("@P_CREDITS",objreTest.Credits);
                        objParams[7] = new SqlParameter("@P_BRANCHNO",objreTest.Branchno);
                       
                        objParams[8] = new SqlParameter("@P_STUDNAME", objreTest.StudName);
                        objParams[9] = new SqlParameter("@P_SESSIONNO", objreTest.Sessionno);
                        objParams[10] = new SqlParameter("@P_UA_NO", objreTest.Ua_no);
                        objParams[11] = new SqlParameter("@P_OUTPUT","1");
                        objParams[11].Direction = ParameterDirection.InputOutput;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ITLE_INSERT_REQUEST_ALLOW_RETEST", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("-99"))
                                status = 0;
                            else
                                status = Convert.ToInt32(ret.ToString());
                        }
                        else
                            status = 0;



                    }
                    catch (Exception ee)
                    {
                        throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.Lib_ConfigControl.UpdateConfig-> " + ee.ToString());
                    }
                    return status;

                }

            
            
            }
        }
    }
}
