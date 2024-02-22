using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessLogic.PostAdmission
{
    public class ADMPAdhocReportConfigController
    {
        private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet GetAllParamsList(string ProcName)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[1];

                objParams[0] = new SqlParameter("@P_NAME", ProcName);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_PARAMS_REPORT_GENERATION_CONFIG", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("RAJAGIRI.BusinessLayer.BusinessLogic.ADMPAdhocReportConfigController.GetAllParamsList-> " + ex.ToString());
            }
            return ds;
        }
        public DataSet GetAllReportList()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[0];

                //objParams[0] = new SqlParameter("@P_NAME", ProcName);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ALL_SP_PARAMS_REPORT_GENERATION_CONFIG", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("RAJAGIRI.BusinessLayer.BusinessLogic.ADMPAdhocReportConfigController.GetAllReportList-> " + ex.ToString());
            }
            return ds;
        }
        public DataSet GetSingleReportData(int AdhocId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[1];

                objParams[0] = new SqlParameter("@P_ADHOCID", AdhocId);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_RET_EDIT_SP_PARAMS_REPORT_GENERATION_CONFIG", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("RAJAGIRI.BusinessLayer.BusinessLogic.ADMPAdhocReportConfigController.GetSingleReportData->" + ex.ToString());
            }
            return ds;
        }
        public int InsertReportParamData(string ReportName, string ProcName, string DisplayControl, string FormTab, string xml, int UserNo, int isAvtive, int displayStatus)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                //Add
                objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_REPORTNAME", ReportName);
                objParams[1] = new SqlParameter("@P_PROCEDURENAME", ProcName);
                objParams[2] = new SqlParameter("@P_DISPLAYCONTROLS", DisplayControl);
                objParams[3] = new SqlParameter("@P_FORM_TABLIST", FormTab);
                objParams[4] = new SqlParameter("@P_XMLDATA", xml);
                objParams[5] = new SqlParameter("@P_CREATEDBY", UserNo);
                objParams[6] = new SqlParameter("@P_ISACTIVE", isAvtive);
                objParams[7] = new SqlParameter("@P_ISDISPLAY", displayStatus);   // Added By Shrikant W. on 11022024
                objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INS_SP_PARAMS_REPORT_GENERATION_CONFIG", objParams, true);
                return Convert.ToInt32(ret);
                //if (ret != null && ret.ToString() != "-99" && ret.ToString() != "-1001")
                //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //else
                //    retStatus = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                return retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("RAJAGIRI.BusinessLayer.BusinessLogic.ADMPAdhocReportConfigController.InsertReportParamData-> " + ex.ToString());
            }
            //return retStatus;
        }

        public int UpdateReportParamData(string ReportName, string ProcName, string DisplayControl, string FormTab, string xml, int UserNo, int AdhocId, int isAvtive, int displayStatus)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                //Add
                objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_REPORTNAME", ReportName);
                objParams[1] = new SqlParameter("@P_PROCEDURENAME", ProcName);
                objParams[2] = new SqlParameter("@P_DISPLAYCONTROLS", DisplayControl);
                objParams[3] = new SqlParameter("@P_FORM_TABLIST", FormTab);
                objParams[4] = new SqlParameter("@P_XMLDATA", xml);
                objParams[5] = new SqlParameter("@P_MODIFIEDBY", UserNo);
                objParams[6] = new SqlParameter("@P_ADHOCID", AdhocId);
                objParams[7] = new SqlParameter("@P_ISACTIVE", isAvtive);
                objParams[8] = new SqlParameter("@P_ISDISPLAY", displayStatus);  // // Added By Shrikant W. on 11022024
                objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPD_SP_PARAMS_REPORT_GENERATION_CONFIG", objParams, true);
                return Convert.ToInt32(ret);
                //if (ret != null && ret.ToString() != "-99" && ret.ToString() != "-1001")
                //    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                //else
                //    retStatus = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                return retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("RAJAGIRI.BusinessLayer.BusinessLogic.ADMPAdhocReportConfigController.UpdateReportParamData-> " + ex.ToString());
            }
            //return retStatus;
        }
   
    }
}
