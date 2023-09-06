//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : PAYROLL
// PAGE NAME     : Pay_ModifyPaySlipController.cs                                
// CREATION DATE : 28-JAN-2016                                                        
// CREATED BY    : ZUBAIR AHMAD                                       
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
           public class Pay_ModifyPaySlipController
            {

               string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

               public DataSet GetAllPayHeads(string MonYear, int idNo, char type)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParam = new SqlParameter[3];
                       objParam[0] = new SqlParameter("@P_MON_YEAR", MonYear);
                       objParam[1] = new SqlParameter("@P_IDNO", idNo);
                       objParam[2] = new SqlParameter("@P_TYPE", type);
                       ds = objSQLHelper.ExecuteDataSetSP("PKG_PAYROLL_GET_PAYHEAD", objParam);
                   }
                   catch (Exception ex)
                   {
                       throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.Pay_ModifyPaySlipController.GetAllPayHeads() --> " + ex.Message + " " + ex.StackTrace);
                   }
                   return ds;
               }



               public int UpdatePaySlip(int idNo,string monYear,string payHeadValues)
               {
                   int retStatus = Convert.ToInt32(CustomStatus.Others);
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);                     
                       SqlParameter[] sqlParams = new SqlParameter[]
                        {  
                            new SqlParameter("@P_IDNO",idNo),                            
                            new SqlParameter("@P_MON_YEAR",monYear),
                            new SqlParameter("@P_PAYHEADVALUES",payHeadValues)                                                                                      
                        };

                       if (objSQLHelper.ExecuteNonQuerySP("PAYROLL_UPDATE_PAYSLIP", sqlParams, false) != null)
                           retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                   }
                   catch (Exception ex)
                   {
                       retStatus = Convert.ToInt32(CustomStatus.Error);
                       throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayOverRuleController.UpdatePaySlip -> " + ex.ToString());
                   }
                   return retStatus;
               }
            }
        }
    }
}
