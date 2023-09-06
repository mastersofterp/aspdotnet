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

            public class PurchaseComiteeController
            {
                /// <summary>
                /// ConnectionStrings
                /// </summary>

                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                public int AddUpdatePurchaseComitee(int COMTNO, int QNO, int UNO, int COLLEGE_CODE, int UA_NO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;                       
                        objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_COMTNO", COMTNO);
                        objParams[1] = new SqlParameter("@P_QNO", QNO);
                        objParams[2] = new SqlParameter("@P_UNO", UNO);
                        objParams[3] = new SqlParameter("@P_COLLEGE_CODE", COLLEGE_CODE);
                        objParams[4] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[5].Direction = ParameterDirection.Output;

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_STR_PURCHASE_COMITEE_INSERT_UPDATE", objParams, true).ToString());
                            

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PurchaseComiteeController.AddUpdatePurchaseComitee-> " + ex.ToString());
                    }
                    return retStatus;

                }

                public int deleteComiteeMember(int COMTNO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[1];
                        string query = "delete from STORE_PURCHASE_COMITEE where COMTNO=@P_COMTNO";
                        objParams[0] = new SqlParameter("@P_COMTNO", COMTNO);  
                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP(query,objParams,false));
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PurchaseComiteeController.AddUpdatePurchaseComitee-> " + ex.ToString());
                    }
                    return retStatus;

                }

                public int InsertremarkpurchaseComitee(int qno,int uno,string remark)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];
                        string query = "UPDATE STORE_PURCHASE_COMITEE SET REMARK=@P_REMARK WHERE QNO=@P_QNO AND UNO=@P_UNO";
                        objParams[0] = new SqlParameter("@P_REMARK", remark);
                        objParams[1] = new SqlParameter("@P_QNO", qno);
                        objParams[2] = new SqlParameter("@P_UNO", uno);
                        
                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP(query, objParams, false));
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.PurchaseComiteeController.AddUpdatePurchaseComitee-> " + ex.ToString());
                    }
                    return retStatus;

                }
            }
        }
    }
}
