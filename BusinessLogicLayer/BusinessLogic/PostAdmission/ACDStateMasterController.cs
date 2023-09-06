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
    
    public class ACDStateMasterController
    {
        private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        
        public string InsertUpdateState(int CountryNo, int StateNo, string StateName, bool ActiveStatus, string College_id)
        {
            string retStatus = string.Empty;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;


                objParams = new SqlParameter[6];

                
                objParams[0] = new SqlParameter("@P_COUNTRYNO", CountryNo);
                objParams[1] = new SqlParameter("@P_STATENO", StateNo);
                objParams[2] = new SqlParameter("@P_STATENAME", StateName);
                objParams[3] = new SqlParameter("@P_ACTIVESTATUS", ActiveStatus);
                objParams[4] = new SqlParameter("@P_COLLEGE_CODE", College_id);

                objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_UPD_STATE_DATA", objParams, true);
                retStatus = ret.ToString();

            }
            catch (Exception ex)
            {
                retStatus = "0";
                throw new IITMSException("RAJAGIRI.BusinessLayer.BusinessLogic.ACDStateMasterController.InsertUpdateState-> " + ex.ToString());
            }
            return retStatus;
        }
        public DataSet GetStateDataList(int CountryNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_COUNTRYNO", CountryNo);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_STATE_DATA", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("RAJAGIRI.BusinessLayer.BusinessLogic.ACDStateMasterController.GetSingleExamBoardInformation->" + ex.ToString());
            }
            return ds;
        }
        public DataSet GetSingleStateInformation(int StateNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_STATENO", StateNo);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_EDIT_STATE_DATA", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("RAJAGIRI.BusinessLayer.BusinessLogic.StudentOnlineAdmissionMasterController.GetSingleExamBoardInformation->" + ex.ToString());
            }
            return ds;
        }

        public string InsertUpdateCountry(int CountryNo, string CountryName, int NationalityNo, int ActiveStatus, string College_id)
        {
            string retStatus = string.Empty;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;


                objParams = new SqlParameter[6];

                objParams[0] = new SqlParameter("@P_COUNTRYNO", CountryNo);
                objParams[1] = new SqlParameter("@P_COUNTRYNAME", CountryName);
                objParams[2] = new SqlParameter("@P_NATIONALITYNO", NationalityNo);
                objParams[3] = new SqlParameter("@P_ACTIVESTATUS", ActiveStatus);
                objParams[4] = new SqlParameter("@P_COLLEGE_CODE", College_id);
                objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_UPD_COUNTRY_DATA", objParams, true);
                retStatus = ret.ToString();

            }
            catch (Exception ex)
            {
                retStatus = "0";
                throw new IITMSException("RAJAGIRI.BusinessLayer.BusinessLogic.ACDStateMasterController.InsertUpdateCountry-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetCountryDataList(int NationalityNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_NATIONALITYNO", NationalityNo);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_COUNTRY_DATA", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("RAJAGIRI.BusinessLayer.BusinessLogic.ACDStateMasterController.GetCountryDataList->" + ex.ToString());
            }
            return ds;
        }

        public DataSet GetSingleCountryInformation(int CountryNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_COUNTRYNO", CountryNo);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_EDIT_COUNTRY_DATA", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("RAJAGIRI.BusinessLayer.BusinessLogic.ACDStateMasterController.GetSingleCountryInformation->" + ex.ToString());
            }
            return ds;
        }
    }
}
