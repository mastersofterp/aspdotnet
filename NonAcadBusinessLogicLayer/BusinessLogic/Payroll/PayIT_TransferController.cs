//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : BUSINESS LOGIC FILE                              
// CREATION DATE : 07-MARCH-2017                                                        
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
            public class PayIT_TransferController
            {

                private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
                public DataSet GetEmployee(int staffNo, int CollegeNo, string ORDER_BY)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_STAFFNO", staffNo);
                        objParams[1] = new SqlParameter("@P_COLLEGE_NO", CollegeNo);
                        objParams[2] = new SqlParameter("@P_ORDER_BY", ORDER_BY);
                     
                        ds = objSQLHelper.ExecuteDataSetSP("[dbo].[PKG_PAY_IT_TRANSFER_PAYROLL]", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AttendanceController.GetAttendanceOfEmployee-> " + ex.ToString());
                    }
                    finally
                    {
                        ds.Dispose();
                    }
                    return ds;
                }


                public int UpdateITAmount(decimal Amount, int idNo, int CollegeNo)
                {
                    int retStatus = 0;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                        SqlParameter[] objParams = null;
                       
                        objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_AMOUNT", Amount);
                        objParams[1] = new SqlParameter("@P_IDNO", idNo);
                        objParams[2] = new SqlParameter("@P_COLLEGE_NO", CollegeNo);
                        if (objSQLHelper.ExecuteNonQuerySP("[dbo].[PKG_PAY_UPD_IT_AMOUNT]", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.AttendanceController.UpdateITAmount-> " + ex.ToString());
                    }
                    return retStatus;
                }


            }
        }
    }
}
