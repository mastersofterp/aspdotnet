using IITMS;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.BusinessLogic
{
    public class ADMPApplicationStagesConfigController
    {
        private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        #region Tab-1 - Application Phase
        public int InsertApplicationPhaseData(string PhaseName, bool ActiveStatus)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            
                            new SqlParameter("@P_PHASE", PhaseName),
                            new SqlParameter("@P_ACTIVESTATUS", ActiveStatus),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int),
                        };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_PHASE_ADMP_APPLICATION_STAGE_CONFIG", sqlParams, true);
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
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.InsertApplicationPhaseData-> " + ex.ToString());
            }
            return retStatus;
        }

        public int UpdateApplicationPhaseData(int PhaseId, string PhaseName, bool ActiveStatus)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_PHASEID",PhaseId),
                            new SqlParameter("@P_PHASE", PhaseName),
                            new SqlParameter("@P_ACTIVESTATUS", ActiveStatus),
                          };


                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPD_PHASE_ADMP_APPLICATION_STAGE_CONFIG", sqlParams, false);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.UpdateApplicationPhaseData-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetAllApplicationPhaseList()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ALL_PHASE_ADMP_APPLICATION_STAGE_CONFIG", sqlParams);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GetAllApplicationPhaseList-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetSingleApplicationPhaseData(int PhaseId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@P_PHASEID", PhaseId);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_RET_PHASE_ADMP_APPLICATION_STAGE_CONFIG", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GetSingleApplicationPhaseData-> " + ex.ToString());
            }
            return ds;
        }
        #endregion

        #region Tab-2 - Application Stages
        public int InsertApplicationStagesData(int PhaseId, string StageName, string Description, bool ActiveStatus,string IsAllowProcess)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            
                            new SqlParameter("@P_PHASEID",PhaseId),
                            new SqlParameter("@P_STAGENAME",StageName),
                            new SqlParameter("@P_DESCRIPTION",Description),
                            new SqlParameter("@P_ACTIVESTATUS", ActiveStatus),
                            new SqlParameter("@P_ISALLOWPROCESS", IsAllowProcess),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int),
                        };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_STAGES_ADMP_APPLICATION_STAGE_CONFIG", sqlParams, true);
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
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.InsertApplicationStagesData-> " + ex.ToString());
            }
            return retStatus;
        }

        public int UpdateApplicationStagesData(int StageId, int PhaseId, string StageName, string Description, bool ActiveStatus,string IsAllowProcess)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_STAGEID",StageId),
                            new SqlParameter("@P_PHASEID",PhaseId),
                            new SqlParameter("@P_STAGENAME",StageName),
                            new SqlParameter("@P_DESCRIPTION",Description),
                            new SqlParameter("@P_ACTIVESTATUS", ActiveStatus),
                            new SqlParameter("@P_ISALLOWPROCESS", IsAllowProcess),
                          };


                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPD_STAGES_ADMP_APPLICATION_STAGE_CONFIG", sqlParams, false);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.UpdateApplicationStagesData-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetAllApplicationStagesList()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ALL_STAGES_ADMP_APPLICATION_STAGE_CONFIG", sqlParams);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GetAllApplicationStagesList-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetSingleApplicationStagesData(int StageId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@P_STAGEID", StageId);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_RET_STAGES_ADMP_APPLICATION_STAGE_CONFIG", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GetSingleApplicationStagesData-> " + ex.ToString());
            }
            return ds;
        }
        #endregion

        #region Tab-3 - Stages Degree Mapping
        public int INSUPDStagesDegreeMappingData(int BatchNo, int DegreeNo, int StageId, int SequnceNo)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            
                            new SqlParameter("@P_DEGREENO",DegreeNo),
                            new SqlParameter("@P_BATCHNO",BatchNo),
                            new SqlParameter("@P_STAGEID",StageId),
                            new SqlParameter("@P_SEQUANCENO", SequnceNo),
                            new SqlParameter("@P_OUT", SqlDbType.Int),
                        };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_UPD_STAGEDEGMAP_ADMP_APPLICATION_STAGE_CONFIG", sqlParams, true);
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
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.INSUPDStagesDegreeMappingData-> " + ex.ToString());
            }
            return retStatus;
        }
        public DataSet GetAllStagesDegreeMappingList(int BatchNo, int DegreeNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[2];
                sqlParams[0] = new SqlParameter("@P_BATCHNO", BatchNo);
                sqlParams[1] = new SqlParameter("@P_DEGREENO", DegreeNo);
                

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ALL_STAGEDEGMAP_ADMP_APPLICATION_STAGE_CONFIG", sqlParams);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GetAllStagesDegreeMappingList-> " + ex.ToString());
            }
            return ds;
        }
        public int DeleteStagesDegreeMapping(int BatchNo, int DegreeNo, int StageId)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_BATCHNO",BatchNo),
                            new SqlParameter("@P_DEGREENO",DegreeNo),
                            new SqlParameter("@P_STAGEID",StageId),
                            new SqlParameter("@P_OUT",SqlDbType.Int),
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_DELETE_STAGEDEGMAP_ADMP_APPLICATION_STAGE_CONFIG", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)

                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.InsertCreateEventData-> " + ex.ToString());
            }
            return retStatus;
        }
        #endregion

        #region Tab-4 Stages Dependancies
        public int InsertStagesDependanciesData(int CurrentStageId, int NextStageId,int BatchNo,int DegreeNo)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            
                            new SqlParameter("@P_CURRENTSTAGEID", CurrentStageId),
                            new SqlParameter("@P_NEXTSTAGEID", NextStageId),
                            new SqlParameter("@P_BATCHNO", BatchNo),
                            new SqlParameter("@P_DEGREENO", DegreeNo),
                            new SqlParameter("@P_OUT", SqlDbType.Int),
                        };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INS_STAGE_DEPND_ADMP_APPLICATION_STAGE_CONFIG", sqlParams, true);
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
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.InsertStagesDependanciesData-> " + ex.ToString());
            }
            return retStatus;
        }
        public DataSet GetAllStagesDependanciesList(int StageId, int BatchNo, int DegreeNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[3];
                sqlParams[0] = new SqlParameter("@P_STAGEID", StageId);
                sqlParams[1] = new SqlParameter("@P_BATCHNO", BatchNo);
                sqlParams[2] = new SqlParameter("@P_DEGREENO", DegreeNo); 
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_ALL_STAGE_DEPND_ADMP_APPLICATION_STAGE_CONFIG", sqlParams);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GetAllStagesDependanciesList-> " + ex.ToString());
            }
            return ds;
        }
        public int DeleteStagesDependancies(int CurrentStageId, int NextStageId,int BatchNo,int DegreeNo)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_CURRENTSTAGEID",CurrentStageId),
                            new SqlParameter("@P_NEXTSTAGEID",NextStageId),
                            new SqlParameter("@P_BATCHNO",BatchNo),
                            new SqlParameter("@P_DEGREENO",DegreeNo),
                            new SqlParameter("@P_OUT",SqlDbType.Int),
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_DELETE_STAGE_DEPND_ADMP_APPLICATION_STAGE_CONFIG", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)

                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DeleteStagesDependancies-> " + ex.ToString());
            }
            return retStatus;
        }
        #endregion

        //Check Master Table ID reference and If refered then Prevent Master Data Inactive 
        public string CheckReferMasterTable(int MST_TBL_CODE, string MST_FORM_NAME, int COL_ID_VALUE)
        {
            string retStatus = string.Empty;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        {                            
                            new SqlParameter("@P_MST_TBL_CODE", MST_TBL_CODE),
                            new SqlParameter("@P_MST_FORM_NAME", MST_FORM_NAME),
                            new SqlParameter("@P_COL_ID_VALUE", COL_ID_VALUE),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int),
                        };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_VALIDATE_APPLICATION_STAGES_MASTER", sqlParams, true);

                retStatus = ret.ToString();
            }
            catch (Exception ex)
            {
                retStatus = "-1,\"Exception\"";
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CheckReferMasterTable-> " + ex.ToString());
            }
            return retStatus;
        }
    }
}
