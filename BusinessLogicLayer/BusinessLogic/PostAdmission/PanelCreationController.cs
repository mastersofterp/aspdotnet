using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data;
using BusinessLogicLayer.BusinessEntities;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
   public  class PanelCreationController
    {
        string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
       
        public PanelCreationController()
        {
        }
        public DataSet GetAllPanel()
        {
            DataSet dspanelcreation = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] sqlParams = new SqlParameter[0];
                dspanelcreation = objSQLHelper.ExecuteDataSetSP("PKG_PANELCREATION_SP_ALL_PANELCREATIONDETAILS", sqlParams);

            }
            catch (Exception ex)
            {
                return dspanelcreation;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.PanelCreationController.GetAllPanel-> " + ex.ToString());
            }
            return dspanelcreation;
        }
        public int InsertPanelCreationData(PanelCreation objpc ,int userno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@PANELID",objpc.panelid),
                            new SqlParameter("@BATCHNO",objpc.Batchno ),
                            new SqlParameter("@DEGREENO",objpc.Degreeno ),
                            new SqlParameter("@BRANCHNO", objpc.Branchno),
                            new SqlParameter("@SCHEDULE_NO",objpc.Scheduleno ),
                            new SqlParameter("@PANEL_NAME",objpc.Panelname ),
                            new SqlParameter("@PANEL_FOR",objpc.panelfor ),
                            new SqlParameter("@STAFFNO",objpc.staff ),
                            new SqlParameter("@USERNO", userno),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int),
                         };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_UPD_PanelCreation", sqlParams, true);
                if (ret != null && ret.ToString() == "1")
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else if (ret != null && ret.ToString() == "2")
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                else if (ret != null && ret.ToString() == "-1001")
                    retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.PanelCreationController.InsertPanelCreationData-> " + ex.ToString());
            }
            return retStatus;
        }
    }
}
