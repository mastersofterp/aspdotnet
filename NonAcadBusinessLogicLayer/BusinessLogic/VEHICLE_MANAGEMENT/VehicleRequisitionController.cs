//================================================================================================
//PROJECT NAME  : UAIMS
//MODULE NAME   : BUSINESS LOGIC FILE//[VEHICLE MAINTENANCE]
//CREATION DATE : 19-NOV-2010      
//CREATED BY    : PRAKASH RADHWANI
//MODIFY  BY    : MRUNAL SINGH
//MODIFIED DATE : 09-09-2014
//MODIFIED DESC :
//================================================================================================   
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
        namespace BusinessLogicLayer.BusinessLogic
        {
           public class VehicleRequisitionController
            {

               private string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

               public DataSet GetPendingRequisitionList()
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = new SqlParameter[0];
                       ds = objSQLHelper.ExecuteDataSetSP("PKG_VEHICLE_GET_PENDING_REQUISITION_LIST", objParams);
                   }
                   catch (Exception ex)
                   {

                       return ds;
                       throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VehicleRequisitionController.GetPendingRequisitionList-> " + ex.ToString());
                   }
                   return ds;
               }

               public DataSet GetStudentDetails(int StudIdno)
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = new SqlParameter[1];
                       objParams[0] = new SqlParameter("@P_STUD_IDNO", StudIdno);
                       ds = objSQLHelper.ExecuteDataSetSP("PKG_VEHICLE_GET_STUDENT_DETAILS_TO_CHECK", objParams);
                   }
                   catch (Exception ex)
                   {

                       return ds;
                       throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VehicleRequisitionController.GetPendingRequisitionList-> " + ex.ToString());
                   }
                   return ds;
               }


               public int UpdateApprovalDetails(VM objVM)
               {
                   int retstatus = 0;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = new SqlParameter[2];
                       objParams[0] = new SqlParameter("@P_APPROVAL_TRAN_TBL", objVM.APPROVAL_TRAN);                      
                       objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                       objParams[1].Direction = ParameterDirection.Output;
                       object ret = objSQLHelper.ExecuteNonQuerySP("PKG_VEHICLE_REQUISITION_APPROVAL_UPDATE", objParams, true);
                       if (Convert.ToInt32(ret) == -99)
                       {
                           retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                       }                       
                       else
                       {
                           retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                       }
                   }
                   catch (Exception ex)
                   {
                       retstatus = Convert.ToInt32(CustomStatus.Error);
                       throw new IITMSException("IITMS.HEALTH.BusinessLayer.BusinessLogic.VehicleRequisitionController.UpdateApprovalDetails->" + ex.ToString());
                   }
                   return retstatus;
               }

               public int UpdateSecondApprovalDetails(VM objVM)
               {
                   int retstatus = 0;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = new SqlParameter[2];
                       objParams[0] = new SqlParameter("@P_APPROVAL_TRAN_TBL", objVM.APPROVAL_TRAN);
                       objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                       objParams[1].Direction = ParameterDirection.Output;
                       object ret = objSQLHelper.ExecuteNonQuerySP("PKG_VEHICLE_REQUISITION_SECOND_APPROVAL_UPDATE", objParams, true);
                       if (Convert.ToInt32(ret) == -99)
                       {
                           retstatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                       }
                       else
                       {
                           retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                       }
                   }
                   catch (Exception ex)
                   {
                       retstatus = Convert.ToInt32(CustomStatus.Error);
                       throw new IITMSException("IITMS.HEALTH.BusinessLayer.BusinessLogic.VehicleRequisitionController.UpdateApprovalDetails->" + ex.ToString());
                   }
                   return retstatus;
               }

               public DataSet GetListOfFeesPaidStudent()
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = new SqlParameter[0];
                       ds = objSQLHelper.ExecuteDataSetSP("PKG_VEHICLE_GET_FEES_PAID_STUDENT_LIST", objParams);
                   }
                   catch (Exception ex)
                   {

                       return ds;
                       throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VehicleRequisitionController.GetListOfFeesPaidStudent-> " + ex.ToString());
                   }
                   return ds;
               }

               public DataSet GetListStudentForBusPass()
               {
                   DataSet ds = null;
                   try
                   {
                       SQLHelper objSQLHelper = new SQLHelper(_nitprm_constr);
                       SqlParameter[] objParams = new SqlParameter[0];
                       ds = objSQLHelper.ExecuteDataSetSP("PKG_VEHICLE_GET_STUDENT_LIST_FOR_BUSPASS", objParams);
                   }
                   catch (Exception ex)
                   {

                       return ds;
                       throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.VehicleRequisitionController.GetListStudentForBusPass-> " + ex.ToString());
                   }
                   return ds;
               }
            }
        }
    }
}
