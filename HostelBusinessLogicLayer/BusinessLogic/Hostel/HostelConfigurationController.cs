//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : HOSTEL
// PAGE NAME     : Hostel module configuration
// CREATION DATE : 21-Sept-2022
// CREATED BY    : Sonali Bhor
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================


using System;
using System.Data;
using System.Data.SqlClient;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
  public  class HostelConfigurationController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int Add_HostelConfiguration(int HostelConfigid, int AdminAllowRoomAllot, int StudAllowRoomAllot, int FeeCollectionType, int PaymentGateway, int UANO, string IPAddress, string Mac_Address, int HostelWiseAtten, int BlockWiseAtten, int hostelDayWiseBooking, int hostelDisciplinaryAction, int CreateDemandOnRoomAllotment, int AllowDirectPayFromOnlinePaymentForm)
        {
            int status = 0;
            try
            {
              
                SQLHelper objHelp = new SQLHelper(connectionString);
                SqlParameter[] objParam = new SqlParameter[15];

                objParam[0] = new SqlParameter("@P_HostelConfigid", HostelConfigid);
                objParam[0] = new SqlParameter("@P_AdminAllowRoomAllot", AdminAllowRoomAllot);
                objParam[1] = new SqlParameter("@P_StudAllowRoomAllot", StudAllowRoomAllot);
                objParam[2] = new SqlParameter("@P_FeeCollectionType", FeeCollectionType);
                objParam[3] = new SqlParameter("@P_PaymentGateway", PaymentGateway);

                objParam[4] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                objParam[5] = new SqlParameter("@P_UANO", UANO);
                objParam[6] = new SqlParameter("@P_IP_ADDRESS", IPAddress);
                objParam[7] = new SqlParameter("@P_MAC_ADDRESS", Mac_Address);
                objParam[8] = new SqlParameter("@P_HostelWiseAtten", HostelWiseAtten);
                objParam[9] = new SqlParameter("@P_BlockWiseAtten", BlockWiseAtten);
                objParam[10] = new SqlParameter("@P_HostelDayWiseBooking", hostelDayWiseBooking);
                objParam[11] = new SqlParameter("@P_HostelDisciplinaryAction", hostelDisciplinaryAction);
                objParam[12] = new SqlParameter("@P_CreateDemandOnRoomAllotment", CreateDemandOnRoomAllotment);
                objParam[13] = new SqlParameter("@P_AllowDirectPayFromOnlinePaymentForm", AllowDirectPayFromOnlinePaymentForm);
                objParam[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParam[14].Direction = ParameterDirection.Output;

                object ret = objHelp.ExecuteNonQuerySP("PKG_HOSTEL_MODULE_CONFIGURATION_INSERT_UPDATE", objParam, true);
                status = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ModuleConfigController.SaveModuleConfiguration() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
   
      
        public DataSet GetModuleConfigData()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                ds = objSQLHelper.ExecuteDataSetSP("PKG_HOSTEL_GET_CONFIG_MODULE_DATA", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigAffilationTypeController.GetModuleConfigData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
  }
}
