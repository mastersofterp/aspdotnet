using System;
using System.Data;
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;


namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class VaccinationController
    {
        string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddVaccineNotTaken(string idno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", Convert.ToInt32(idno));
                objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADD_VACCINE_NOT_TAKEN", objParams, false);
                if (ret != null)
                    if (ret.ToString() == "1")
                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                throw new IITMSException("BusinessLayer.BusinessLogic.VaccinationController.AddVaccineNotTaken-> " + ex.ToString());
            }
            return retStatus;
        }

        public int AddUpdateFirstDoseVaccination(string idno, string vaccineName, string vaccinatedCenter, string vaccinatedDate, string filePath, string filename, string vaccinationStat)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_IDNO", Convert.ToInt32(idno));
                if (vaccinationStat == "" || vaccinationStat == "0")
                {
                    objParams[1] = new SqlParameter("@P_VACCINATION_STAT", 1);
                }
                else
                {
                    objParams[1] = new SqlParameter("@P_VACCINATION_STAT", Convert.ToInt32(vaccinationStat));
                }
                objParams[2] = new SqlParameter("@P_FIRSTDOSE_VACCINE_NAME", vaccineName);
                objParams[3] = new SqlParameter("@P_FIRSTDOSE_VACCINATION_CENTER", vaccinatedCenter);
                objParams[4] = new SqlParameter("@P_FIRSTDOSE_VACCINATED_DATE", vaccinatedDate);
                objParams[5] = new SqlParameter("@P_FIRSTDOSE_FILE_PATH", filePath);
                objParams[6] = new SqlParameter("@P_FIRSTDOSE_FILE_NAME", filename);
                objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;
                if (vaccinationStat == "")
                {
                    object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADD_FIRST_DOSE_VACCINATION", objParams, false);
                    if (ret != null)
                        if (ret.ToString() == "1")
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else
                {
                    object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_FIRST_DOSE_VACCINATION", objParams, false);
                    if (ret != null)
                        if (ret.ToString() == "1")
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("BusinessLayer.BusinessLogic.VaccinationController.AddUpdateFirstDoseVaccination-> " + ex.ToString());
            }
            return retStatus;
        }

        public int UpdateSecondDoseVaccination(string idno, string vaccineName, string vaccinatedCenter, string vaccinatedDate, string filePath, string filename)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_IDNO", Convert.ToInt32(idno));
                objParams[1] = new SqlParameter("@P_VACCINATION_STAT", 2);
                objParams[2] = new SqlParameter("@P_SECONDDOSE_VACCINE_NAME", vaccineName);
                objParams[3] = new SqlParameter("@P_SECONDDOSE_VACCINATION_CENTER", vaccinatedCenter);
                objParams[4] = new SqlParameter("@P_SECONDDOSE_VACCINATED_DATE", vaccinatedDate);
                objParams[5] = new SqlParameter("@P_SECONDDOSE_FILE_PATH", filePath);
                objParams[6] = new SqlParameter("@P_SECONDDOSE_FILE_NAME", filename);
                objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_SECOND_DOSE_VACCINATION", objParams, false);
                if (ret != null)
                    if (ret.ToString() == "1")
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                throw new IITMSException("BusinessLayer.BusinessLogic.VaccinationController.UpdateSecondDoseVaccination-> " + ex.ToString());
            }
            return retStatus;
        }


        public int UnlockFirstDoseVaccination(string idno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", Convert.ToInt32(idno));
                objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[1].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UNLOCK_FIRST_DOSE_VACCINATION", objParams, false);
                if (ret != null)
                    if (ret.ToString() == "1")
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                throw new IITMSException("BusinessLayer.BusinessLogic.VaccinationController.UnlockFirstDoseVaccination-> " + ex.ToString());
            }
            return retStatus;
        }

        public int UnlockSecondDoseVaccination(string idno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", Convert.ToInt32(idno));
                objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[1].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UNLOCK_SECOND_DOSE_VACCINATION", objParams, false);
                if (ret != null)
                    if (ret.ToString() == "1")
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                throw new IITMSException("BusinessLayer.BusinessLogic.VaccinationController.UnlockSecondDoseVaccination-> " + ex.ToString());
            }
            return retStatus;
        }

        public int UpdateSecondDoseVaccination_CRESCENT(string idno, string vaccineName, string vaccinatedCenter, string vaccinatedDate, string filePath, string filename)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_IDNO", Convert.ToInt32(idno));
                objParams[1] = new SqlParameter("@P_VACCINATION_STAT", 2);
                objParams[2] = new SqlParameter("@P_SECONDDOSE_VACCINE_NAME", vaccineName);
                objParams[3] = new SqlParameter("@P_SECONDDOSE_VACCINATION_CENTER", vaccinatedCenter);
                objParams[4] = new SqlParameter("@P_SECONDDOSE_VACCINATED_DATE", vaccinatedDate);
                objParams[5] = new SqlParameter("@P_SECONDDOSE_FILE_PATH", filePath);
                objParams[6] = new SqlParameter("@P_SECONDDOSE_FILE_NAME", filename);
                objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_SECOND_DOSE_VACCINATION", objParams, false);
                if (ret != null)
                    if (ret.ToString() == "1")
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                throw new IITMSException("BusinessLayer.BusinessLogic.VaccinationController.UpdateSecondDoseVaccination-> " + ex.ToString());
            }
            return retStatus;
        }
        // Added by Yograj on 28-07-2022
        public int UpdateVaccinationStatus(int idno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_IDNO", Convert.ToInt32(idno));
                objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[1].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_COVIDSTATUS", objParams, false);
                if (ret != null)
                    if (ret.ToString() == "1")
                        retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                throw new IITMSException("BusinessLayer.BusinessLogic.VaccinationController.UpdateSecondDoseVaccination-> " + ex.ToString());
            }
            return retStatus;
        }

    }
}
