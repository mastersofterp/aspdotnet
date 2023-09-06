using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using System.Data;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
           public class ITHraCalculateController
            {
               private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

               #region ITHRA
               //HRA IT insert update
               public int AddITHRACalculation(ITHRACALCULATE objITHRA)
               {
                   int retStatus = Convert.ToInt32(CustomStatus.Others);

                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                       SqlParameter[] objParams = null;

                       //Add New Employee
                       objParams = new SqlParameter[12];
                       objParams[0] = new SqlParameter("@P_IDNO", objITHRA.IDNO);
                       objParams[1] = new SqlParameter("@P_PAY", objITHRA.PAY);
                       objParams[2] = new SqlParameter("@P_DA", objITHRA.DA);
                       objParams[3] = new SqlParameter("@P_ACTUAL_HRA", objITHRA.ACTUALHRA);
                       objParams[4] = new SqlParameter("@P_ROPHRA", objITHRA.ROPHRA);
                       objParams[5] = new SqlParameter("@P_HRARECEIVED", objITHRA.HRARECEIVED);
                       objParams[6] = new SqlParameter("@P_PAIDRENT", objITHRA.PAIDRENT);
                       objParams[7] = new SqlParameter("@P_SAL10PER", objITHRA.SAL10PER);
                       objParams[8] = new SqlParameter("@P_RENTPAIDACCESS10PERSAL", objITHRA.RENTPAIDACCESS10PER);
                       objParams[9] = new SqlParameter("@P_SALARY40PER", objITHRA.SAL40PER);
                       objParams[10] = new SqlParameter("@P_LESSHRA", objITHRA.LESSHRA);
                       objParams[11] = new SqlParameter("@P_CALCULATEDHRA", objITHRA.HRACALCULATE);
                       object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_ITHRA_INSERT", objParams, true);
                       retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                      
                   }
                   catch (Exception ex)
                   {
                       retStatus = Convert.ToInt32(CustomStatus.Error);
                       throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.EmpMasController.AddITEmplyeeInfo -> " + ex.ToString());
                   }
                   return retStatus;
               }

               public DataTableReader GetITHra(int idno)
               {
                   DataTableReader dr = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                       SqlParameter[] objParams = new SqlParameter[1];
                       objParams[0] = new SqlParameter("@P_IDNO", idno);
                       //dr = objSQLHelper.ExecuteReaderSP("PKG_PAY_FETCH_ITHRA", objParams);
                       dr = objSQLHelper.ExecuteDataSetSP("PKG_PAY_FETCH_ITHRA", objParams).Tables[0].CreateDataReader();
                   }
                   catch (Exception ex)
                   {
                       return dr;
                       throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PayController.GetAppointno-> " + ex.ToString());
                   }
                   return dr;
               }
                //End HRA IT insert update


               public int AddChapVILimit(ITHRACALCULATE objITHRA)
               {
                   int retStatus = Convert.ToInt32(CustomStatus.Others);

                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                       SqlParameter[] objParams = null;

                       //Add New Employee
                       objParams = new SqlParameter[42];
                       objParams[0] = new SqlParameter("@P_IDNO", objITHRA.IDNO);
                       objParams[1] = new SqlParameter("@P_VANO", objITHRA.VANO);
                       objParams[2] = new SqlParameter("@P_ACTUALAMT1", objITHRA.ACTUALAMT1);
                       objParams[3] = new SqlParameter("@P_ACTUALAMT2", objITHRA.ACTUALAMT2);
                       objParams[4] = new SqlParameter("@P_ACTUALAMT3", objITHRA.ACTUALAMT3);
                       objParams[5] = new SqlParameter("@P_ACTUALAMT4", objITHRA.ACTUALAMT4);
                       objParams[6] = new SqlParameter("@P_ACTUALAMT5", objITHRA.ACTUALAMT5);
                       objParams[7] = new SqlParameter("@P_ACTUALAMT6", objITHRA.ACTUALAMT6);
                       objParams[8] = new SqlParameter("@P_ACTUALAMT7", objITHRA.ACTUALAMT7);
                       objParams[9] = new SqlParameter("@P_ACTUALAMT8", objITHRA.ACTUALAMT8);
                       objParams[10] = new SqlParameter("@P_ACTUALAMT9", objITHRA.ACTUALAMT9);
                       objParams[11] = new SqlParameter("@P_ACTUALAMT10", objITHRA.ACTUALAMT10);
                       objParams[12] = new SqlParameter("@P_ACTUALAMT11", objITHRA.ACTUALAMT11);
                       objParams[13] = new SqlParameter("@P_ACTUALAMT12", objITHRA.ACTUALAMT12);
                       objParams[14] = new SqlParameter("@P_ACTUALAMT13", objITHRA.ACTUALAMT13);
                       objParams[15] = new SqlParameter("@P_LIMIT1", objITHRA.LIMIT1);
                       objParams[16] = new SqlParameter("@P_LIMIT2", objITHRA.LIMIT2);
                       objParams[17] = new SqlParameter("@P_LIMIT3", objITHRA.LIMIT3);
                       objParams[18] = new SqlParameter("@P_LIMIT4", objITHRA.LIMIT4);
                       objParams[19] = new SqlParameter("@P_LIMIT5", objITHRA.LIMIT5);
                       objParams[20] = new SqlParameter("@P_LIMIT6", objITHRA.LIMIT6);
                       objParams[21] = new SqlParameter("@P_LIMIT7", objITHRA.LIMIT7);
                       objParams[22] = new SqlParameter("@P_LIMIT8", objITHRA.LIMIT8);
                       objParams[23] = new SqlParameter("@P_LIMIT9", objITHRA.LIMIT9);
                       objParams[24] = new SqlParameter("@P_LIMIT10", objITHRA.LIMIT10);
                       objParams[25] = new SqlParameter("@P_LIMIT11", objITHRA.LIMIT11);
                       objParams[26] = new SqlParameter("@P_LIMIT12", objITHRA.LIMIT12);
                       objParams[27] = new SqlParameter("@P_LIMIT13", objITHRA.LIMIT13);
                       objParams[28] = new SqlParameter("@P_AMOUNT1", objITHRA.AMOUNT1);
                       objParams[29] = new SqlParameter("@P_AMOUNT2", objITHRA.AMOUNT2);
                       objParams[30] = new SqlParameter("@P_AMOUNT3", objITHRA.AMOUNT3);
                       objParams[31] = new SqlParameter("@P_AMOUNT4", objITHRA.AMOUNT4);
                       objParams[32] = new SqlParameter("@P_AMOUNT5", objITHRA.AMOUNT5);
                       objParams[33] = new SqlParameter("@P_AMOUNT6", objITHRA.AMOUNT6);
                       objParams[34] = new SqlParameter("@P_AMOUNT7", objITHRA.AMOUNT7);
                       objParams[35] = new SqlParameter("@P_AMOUNT8", objITHRA.AMOUNT8);
                       objParams[36] = new SqlParameter("@P_AMOUNT9", objITHRA.AMOUNT9);
                       objParams[37] = new SqlParameter("@P_AMOUNT10", objITHRA.AMOUNT10);
                       objParams[38] = new SqlParameter("@P_AMOUNT11", objITHRA.AMOUNT11);
                       objParams[39] = new SqlParameter("@P_AMOUNT12", objITHRA.AMOUNT12);
                       objParams[40] = new SqlParameter("@P_AMOUNT13", objITHRA.AMOUNT13);
                       objParams[41] = new SqlParameter("@P_FIN_YEAR", objITHRA.FINYEAR);
                       object ret = objSQLHelper.ExecuteNonQuerySP("PKG_PAY_CHAPVI_LIMIT_INSERT_UPDATE", objParams, true);
                       retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                   }
                   catch (Exception ex)
                   {
                       retStatus = Convert.ToInt32(CustomStatus.Error);
                       throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.EmpMasController.AddITEmplyeeInfo -> " + ex.ToString());
                   }
                   return retStatus;
               }

               public DataTableReader ShowEmpCHAPVIActualAmt(ITHRACALCULATE objITHra)
               {
                   DataTableReader dtr = null;
                   //SqlDataReader dr = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                       SqlParameter[] objParams = null;
                       objParams = new SqlParameter[2];
                       objParams[0] = new SqlParameter("@P_IDNO", objITHra.IDNO);
                       objParams[1] = new SqlParameter("@P_FINYEAR", objITHra.FINYEAR);
                       dtr = objSQLHelper.ExecuteDataSetSP("PKG_PAY_FETCH_CHAPVI_ACTUALAMOUNT", objParams).Tables[0].CreateDataReader();

                   }
                   catch (Exception ex)
                   {
                       throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITDeclarationController.ShowEmpDetails->" + ex.ToString());
                   }
                   return dtr;
               }

               public DataSet ShowEmpRebateHeads(int idNo, DateTime fromDate, DateTime toDate)
               {
                   DataSet ds = null;
                   //SqlDataReader dr = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                       SqlParameter[] objParams = null;
                       objParams = new SqlParameter[3];
                       objParams[0] = new SqlParameter("@P_IDNO", idNo);
                       objParams[1] = new SqlParameter("@P_FROMDATE", fromDate);
                       objParams[2] = new SqlParameter("@P_TODATE", toDate);
                       ds = objSQLHelper.ExecuteDataSetSP("PKG_PAY_FETCH_REBATE_80C_AMOUNT", objParams);
                      
                   }
                   catch (Exception ex)
                   {
                       throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.BusinessLogic.ITDeclarationController.ShowEmpRebateHeads->" + ex.ToString());
                   }
                   return ds;
               }
               #endregion
            }
        }
    }
}
