//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAYROLL                   
// CREATION DATE : 20-MARCH-2017                                                      
// CREATED BY    : SACHIN GHAGRE                                  
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================  
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class ExcelPayHeadImportController
            {
                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                //Other Rem Upload
                public int UpdatePayHeadsByExcel(Payroll objpay)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                        //Add New File
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_IDNO", objpay.PFILENO);
                        objParams[1] = new SqlParameter("@P_PAYHEAD", objpay.PAYHEAD);
                        objParams[2] = new SqlParameter("@P_AMOUNT", objpay.TOTAMT);
                        if (objSQLHelper.ExecuteNonQuerySP("[dbo].[UPDATE_PAY_HEAD_AMOUNT_EXCEL]", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.PayController.UpdateOtherRemHeadFromExcel-> " + ex.ToString());
                    }
                    return retStatus;
                }
               
            }
        }
    }
}
