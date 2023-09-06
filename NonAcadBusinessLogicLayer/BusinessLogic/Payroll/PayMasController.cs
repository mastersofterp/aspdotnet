using System;
using System.Data;
using System.Web;

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
            public class PayMasController
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                /// <summary>
                /// AddEmployeePay method is used to add new Employee in PayMaster.
                /// </summary>
                /// <param name="objPM">objPM is the object of PayMaster class</param>
                /// <returns>Integer CustomStatus Record Added or Error</returns>
                public int AddEmployeePay(PayMaster objPM)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;

                        //Add New Employee
                        objParams = new SqlParameter[17];
                      
                        objParams[0] = new SqlParameter("@P_IDNO", objPM.IDNO);
                        objParams[1] = new SqlParameter("@P_PSTATUS",objPM.PSTATUS);
                        objParams[2] = new SqlParameter("@P_SUBDEPTNO", objPM.SUBDEPTNO);                        
                        objParams[3] = new SqlParameter("@P_PAYRULE", objPM.PAYRULE);
                        objParams[4] = new SqlParameter("@P_APPOINTNO", objPM.APPOINTNO);
                        objParams[5] = new SqlParameter("@P_BANKNO", objPM.BANKNO);
                        objParams[6] = new SqlParameter("@P_SCALENO", objPM.SCALENO);                        
                        objParams[7] = new SqlParameter("@P_SUBDESIGNO", objPM.SUBDESIGNO);
                        objParams[8] = new SqlParameter("@P_SEQ_NO", objPM.SEQ_NO);
                        objParams[9] = new SqlParameter("@P_DESIGNATURENO",objPM.DESIGNATURENO);                        
                        objParams[10] = new SqlParameter("@P_DOJ", objPM.DOJ);
                        objParams[11] = new SqlParameter("@P_OBASIC", objPM.OBASIC);
                        objParams[12] = new SqlParameter("@P_BASIC", objPM.BASIC);                                        
                        objParams[13] = new SqlParameter("@P_HP", objPM.HP);
                        objParams[14] = new SqlParameter("@P_TA", objPM.TA);                 
                        objParams[15] = new SqlParameter("@P_REMARK", objPM.REMARK);
                        objParams[16] = new SqlParameter("@P_COLLEGE_CODE", objPM.COLLEGE_CODE); 
                       
                       if (objSQLHelper.ExecuteNonQuerySPTrans("PKG_PAY_INS_PAYMAS", objParams, false,2) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayMasController.AddEmployeePay -> " + ex.ToString());
                    }
                    return retStatus;
                }
            }
        }
    }
}