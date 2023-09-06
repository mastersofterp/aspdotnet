using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using BusinessLogicLayer.BusinessEntities.Academic;

namespace BusinessLogicLayer.BusinessLogic.Academic
{
  public  class PBIConfigurationController
    {
      private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


      public int InsertWorkspaceData(PBIConfigurationEntity objPCE)
      {
          int retStatus = Convert.ToInt32(CustomStatus.Others);
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            
                            new SqlParameter("@P_WORKSPACE_NAME", objPCE.workspace_name),
                            new SqlParameter("@P_STATUS", objPCE.status),
                            new SqlParameter("@P_OrganizationId",objPCE.OrganizationId),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int),
                              };

              sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

              object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_PBI_CONFIGURATION_WORKSPACE_MASTER", sqlParams, true);
              if (Convert.ToInt32(ret) == -99)
                  retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
              if (Convert.ToInt32(ret) == 2)
              {
                  retStatus = Convert.ToInt32(CustomStatus.RecordExist);
              }
              else
                  retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

          }
          catch (Exception ex)
          {
              retStatus = Convert.ToInt32(CustomStatus.Error);
              throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.InsertWorkspaceData-> " + ex.ToString());
          }
          return retStatus;
      }


      public int UpdateWorkspaceData(PBIConfigurationEntity objPCE)
      {
          int retStatus = Convert.ToInt32(CustomStatus.Others);
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_WORKSPACE_ID",objPCE.Workspace_id),
                            new SqlParameter("@P_WORKSPACE_NAME", objPCE.workspace_name),
                            new SqlParameter("@P_STATUS", objPCE.status),
                          };


              object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_CONFIGURATION_WORKSPACE_MASTER", sqlParams, false);
              if (Convert.ToInt32(ret) == -99)
                  retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
              else
                  retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);



          }
          catch (Exception ex)
          {
              retStatus = Convert.ToInt32(CustomStatus.Error);
              throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.UpdateWorkspaceData-> " + ex.ToString());
          }
          return retStatus;
      }

      public DataSet GetListview()
      {
          DataSet ds = null;
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] sqlParams = new SqlParameter[0];

              ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SELECT_CONFIGURATION_WORKSPACE_MASTER", sqlParams);
          }
          catch (Exception ex)
          {

              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BasicDetaisls-> " + ex.ToString());
          }
          return ds;
      }

      public DataSet EditWorkspaceData(int WORKSPACE_ID)
      {
          DataSet ds = null;
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] sqlParams = new SqlParameter[1];
              sqlParams[0] = new SqlParameter("@P_WORKSPACE_ID", WORKSPACE_ID);
              ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EDIT_CONFIGURATION_WORKSPACE_MASTER", sqlParams);
          }
          catch (Exception ex)
          {
              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EditWorkspaceData-> " + ex.ToString());
          }
          return ds;
      }


      public int InsertSubWorkspaceData(PBIConfigurationEntity objPCE)
      {
          int retStatus = Convert.ToInt32(CustomStatus.Others);
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            
                            new SqlParameter("@P_SUB_WORKSPACE_NAME", objPCE.sub_workspace_name),
                            new SqlParameter("@P_WORKSPACE_ID", objPCE.Workspace_id),
                            new SqlParameter("@P_STATUS",objPCE.status),
                            new SqlParameter("@P_OrganizationId",objPCE.OrganizationId),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int),
                              };

              sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

              object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_PBI_CONFIGURATION_SUB_WORKSPACE", sqlParams, true);
              if (Convert.ToInt32(ret) == -99)
                  retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
              if (Convert.ToInt32(ret) == 2)
              {
                  retStatus = Convert.ToInt32(CustomStatus.RecordExist);
              }
              else
                  retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

          }
          catch (Exception ex)
          {
              retStatus = Convert.ToInt32(CustomStatus.Error);
              throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.InsertSubWorkspaceData-> " + ex.ToString());
          }
          return retStatus;
      }

      public int UpdateSubWorkspaceData(PBIConfigurationEntity objPCE)
      {
          int retStatus = Convert.ToInt32(CustomStatus.Others);
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_SUB_WORKSPACE_ID",objPCE.sub_Workspace_id),
                            new SqlParameter("@P_SUB_WORKSPACE_NAME", objPCE.sub_workspace_name),
                            new SqlParameter("@P_WORKSPACE_ID", objPCE.Workspace_id),
                            new SqlParameter("@P_STATUS", objPCE.status),
                          };


              object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_CONFIGURATION_SUB_WORKSPACE", sqlParams, false);
              if (Convert.ToInt32(ret) == -99)
                  retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
              else
                  retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);



          }
          catch (Exception ex)
          {
              retStatus = Convert.ToInt32(CustomStatus.Error);
              throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.UpdateSubWorkspaceData-> " + ex.ToString());
          }
          return retStatus;
      }


      public DataSet EditSubWorkspaceData(int SUB_WORKSPACE_ID)
      {
          DataSet ds = null;
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] sqlParams = new SqlParameter[1];
              sqlParams[0] = new SqlParameter("@P_SUB_WORKSPACE_ID", SUB_WORKSPACE_ID);
              ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EDIT_CONFIGURATION_SUB_WORKSPACE", sqlParams);
          }
          catch (Exception ex)
          {
              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EditSubWorkspaceData-> " + ex.ToString());
          }
          return ds;
      }

      public DataSet GetSubWorkspaceListview()
      {
          DataSet ds = null;
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] sqlParams = new SqlParameter[0];

              ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SELECT_CONFIGURATION_SUB_WORKSPACE", sqlParams);
          }
          catch (Exception ex)
          {

              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GetSubWorkspaceListview-> " + ex.ToString());
          }
          return ds;
      }
      public int InsertPbiLinkData(PBIConfigurationEntity objPCE)
      {
          int retStatus = Convert.ToInt32(CustomStatus.Others);
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            
                            new SqlParameter("@P_WORKSPACE_ID", objPCE.Workspace_id),
                            new SqlParameter("@P_SUB_WORKSPACE_ID", objPCE.sub_Workspace_id),
                            new SqlParameter("@P_PBI_LINK_NAME", objPCE.pbi_link_name),
                            new SqlParameter("@P_STATUS", objPCE.status),
                            new SqlParameter("@P_OrganizationId",objPCE.OrganizationId),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int),
                              };

              sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

              object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_PBI_CONFIGURATION_PBI_LINK_CONFIGRATION", sqlParams, true);
              if (Convert.ToInt32(ret) == -99)
                  retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
              if (Convert.ToInt32(ret) == 2)
              {
                  retStatus = Convert.ToInt32(CustomStatus.RecordExist);
              }
              else
                  retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

          }
          catch (Exception ex)
          {
              retStatus = Convert.ToInt32(CustomStatus.Error);
              throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.InsertWorkspaceData-> " + ex.ToString());
          }
          return retStatus;
      }
      public int UpdatePbiData(PBIConfigurationEntity objPCE)
      {
          int retStatus = Convert.ToInt32(CustomStatus.Others);
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_PBI_LINK_CONFIGRATION_ID",objPCE.pbi_link_configuration),
                            new SqlParameter("@P_SUB_WORKSPACE_ID", objPCE.sub_Workspace_id),
                            new SqlParameter("@P_WORKSPACE_ID", objPCE.Workspace_id),
                            new  SqlParameter("@P_PBI_LINK_NAME", objPCE.pbi_link_name),
                            new SqlParameter("@P_STATUS", objPCE.status),
                          };


              object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_PBI_LINK_CONFIGRATION", sqlParams, false);
              if (Convert.ToInt32(ret) == -99)
                  retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
              else
                  retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);



          }
          catch (Exception ex)
          {
              retStatus = Convert.ToInt32(CustomStatus.Error);
              throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.UpdatePbiData-> " + ex.ToString());
          }
          return retStatus;
      }
      public DataSet EditPbiLinkData(int PBI_LINK_CONFIGRATION_ID)
      {
          DataSet ds = null;
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] sqlParams = new SqlParameter[1];
              sqlParams[0] = new SqlParameter("@P_PBI_LINK_CONFIGRATION_ID", PBI_LINK_CONFIGRATION_ID);
              ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EDIT_CONFIGURATION_PBI_LINK", sqlParams);
          }
          catch (Exception ex)
          {
              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EditSubWorkspaceData-> " + ex.ToString());
          }
          return ds;
      }
      public DataSet GetPbiLinkListview()
      {
          DataSet ds = null;
          try
          {
              SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] sqlParams = new SqlParameter[0];

              ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SELECT_CONFIGURATION_PBI_LINK_CONFIGRATION", sqlParams);
          }
          catch (Exception ex)
          {

              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GetPbiLinkListview-> " + ex.ToString());
          }
          return ds;
      }

      public DataSet GetPbiDashobardLink(int workspaceid, int dashboardid)
      {
          DataSet ds = null;
          try
          {
              SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
              SqlParameter[] objParams = new SqlParameter[2];
              objParams[0] = new SqlParameter("@P_WORKSPACEID", workspaceid);
              objParams[1] = new SqlParameter("@P_DASHBOARDID", dashboardid);
              ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_PBI_LINK_SELECTED_DASHBOARD", objParams);
          }
          catch (Exception ex)
          {
              throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PBIConfigurationController.GetPbiworkspaceList --> " + ex.Message + " " + ex.StackTrace);
          }
          return ds;
      }
    }
}
