using System;
using System.Data;
using System.Web;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class Str_ConfigurationCon
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public int InsUpdConfigurationDetails(Str_ConfigurationEnt ObjEnt)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;                       
                        objParams = new SqlParameter[20];
                        objParams[0] = new SqlParameter("@P_MDNO", ObjEnt.MDNO);
                        objParams[1] = new SqlParameter("@P_DEPTUSER", ObjEnt.DEPTUSER);
                        objParams[2] = new SqlParameter("@P_SANCTION_AUTH", ObjEnt.SANCTION_AUTH);
                        objParams[3] = new SqlParameter("@P_COMP_STMNT_AUTH_UANO", ObjEnt.COMP_STMNT_AUTH_UANO);
                        objParams[4] = new SqlParameter("@P_MAIL_SEND", ObjEnt.MAIL_SEND);
                        objParams[5] = new SqlParameter("@P_DSR_CREATION", ObjEnt.DSR_CREATION);
                        objParams[6] = new SqlParameter("@P_PO_APPROVAL", ObjEnt.PO_APPROVAL);
                        objParams[7] = new SqlParameter("@P_DEPT_WISE_ITEM", ObjEnt.DEPT_WISE_ITEM);
                        objParams[8] = new SqlParameter("@P_PHONE", ObjEnt.PHONE);
                        objParams[9] = new SqlParameter("@P_EMAIL", ObjEnt.EMAIL);                       
                        objParams[10] = new SqlParameter("@P_COLLEGE_CODE", ObjEnt.COLCODE);
                        objParams[11] = new SqlParameter("@P_COLLEGE_NAME", ObjEnt.COLLEGE_NAME);
                        objParams[12] = new SqlParameter("@P_CODE_STANDARD", ObjEnt.CODE_STANDARD);
                        //objParams[10] = new SqlParameter("@P_PRE_DSR_YEAR", ObjEnt.PRE_DSR_YEAR);
                        //objParams[11] = new SqlParameter("@P_CUR_DSR_YEAR", ObjEnt.CUR_DSR_YEAR);
                        objParams[13] = new SqlParameter("@P_IS_COMP_STAT_APPROVAL", ObjEnt.IS_COMPARATIVE_STAT_APPROVAL);
                        objParams[14] = new SqlParameter("@P_STATENO", ObjEnt.STATENO);
                        objParams[15] = new SqlParameter("@P_IS_SECGP", ObjEnt.IS_SECGP);
                        objParams[16] = new SqlParameter("@P_IS_BUDGET_HEAD", ObjEnt.IS_BUDGET_HEAD);             //----Added by shabina for making budget head optional
                        objParams[17] = new SqlParameter("@P_IsAvailableQty", ObjEnt.IsAvailableQty);             //----Added by shabina for making budget head optional
                        objParams[18] = new SqlParameter("@P_IsAuthorityShowOnQuot", ObjEnt.IsAuthorityShowOnQuot);             //----Added by shabina for making Authority sign on quotation entry , optional
                        objParams[19] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[19].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STR_CONFIG_DETAILS_INS_UPD", objParams, true);
                        if (ret != null)
                        {
                            if (ret.ToString().Equals("1"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            if (ret.ToString().Equals("2"))
                                retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                        }
                        else
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                    }
                    catch (Exception ex)
                    {
                        return retStatus;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.DepartmentController.GetAllDepartMent-> " + ex.ToString());
                    }
                    return retStatus;
                }

            }
        }
    }
}
