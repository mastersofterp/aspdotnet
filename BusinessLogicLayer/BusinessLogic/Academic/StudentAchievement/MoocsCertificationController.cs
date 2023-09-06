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
using BusinessLogicLayer.BusinessEntities.Academic.StudentAchievement;

namespace BusinessLogicLayer.BusinessLogic.Academic.StudentAchievement
{
    public class MoocsCertificationController
    {

        private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


        public int InsertMoocsCertificationData(MoocsCertificationEntity objMCE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            
                              new SqlParameter("@P_NAME_OF_COURSE", objMCE.course_name),
                              new SqlParameter("@P_MOOCS_PLATFORM_ID", objMCE.moocs_platform_id),
                              new SqlParameter("@P_OFFERED_BY_INSTITUTE_UNIVERSITY", objMCE.institute_university),
                              new SqlParameter("@P_STDATE", objMCE.SDate),
                              new SqlParameter("@P_ENDDATE", objMCE.EDate),
                              new SqlParameter("@P_DURATION_ID", objMCE.duration_id),
                              new SqlParameter("@P_FA_STATUS", objMCE.fa_status),
                              new SqlParameter("@P_AMOUNT", objMCE.amount),
                              new SqlParameter("@P_FILE_NAME", objMCE.file_name),
                              new SqlParameter("@P_ACADMIC_YEAR_ID", objMCE.acadamic_year_id),
                              new SqlParameter("@P_IDNO", objMCE.idno),
                              new SqlParameter("@P_IP_ADDRESS", objMCE.IPADDRESS),
                              new SqlParameter("@P_UA_NO", objMCE.uno),
                              new SqlParameter("@P_C_DATE", objMCE.Current_Date),
                              new SqlParameter("@P_OrganizationId",objMCE.OrganizationId),
                              new SqlParameter("@P_OUTPUT", SqlDbType.Int),
                              };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_MOOCS_CERTIFICATION", sqlParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.BasicDetaisls-> " + ex.ToString());
            }
            return retStatus;
        }


        public DataSet MoocsListView()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SELECT_ACHIEVEMENT_MOOCS_CERTIFICATION", sqlParams);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BasicDetaisls-> " + ex.ToString());
            }
            return ds;
        }
        public DataSet EditMoocsplatform(int MOOCD_CERTIFICATION_ID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@P_MOOCD_CERTIFICATION_ID", MOOCD_CERTIFICATION_ID);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EDIT_ACHIEVEMENT_MOOCS_CERTIFICATION", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.EditEventNatureData-> " + ex.ToString());
            }
            return ds;
        }

        public int UpdateMoocsData(MoocsCertificationEntity objMCE)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_MOOCD_CERTIFICATION_ID",objMCE.moocs_certification_id),
                            new SqlParameter("@P_NAME_OF_COURSE", objMCE.course_name),
                            new SqlParameter("@P_MOOCS_PLATFORM_ID", objMCE.moocs_platform_id),
                            new SqlParameter("@P_OFFERED_BY_INSTITUTE_UNIVERSITY", objMCE.institute_university),
                            new SqlParameter("@P_STDATE", objMCE.SDate),
                            new SqlParameter("@P_ENDDATE", objMCE.EDate),
                            new SqlParameter("@P_DURATION_ID", objMCE.duration_id),
                            new SqlParameter("@P_FA_STATUS", objMCE.fa_status),
                            new SqlParameter("@P_AMOUNT", objMCE.amount),
                            new SqlParameter("@P_FILE_NAME", objMCE.file_name),
                            new SqlParameter("@P_ACADMIC_YEAR_ID", objMCE.acadamic_year_id),

                          };


                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_ACHIEVEMENT_MOOCS_CERTIFICATION", sqlParams, false);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);



            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.UpdateEventNatureData-> " + ex.ToString());
            }
            return retStatus;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MOOCD_CERTIFICATION_ID"></param>
        /// <returns></returns>
        public DataSet MoocsCertificationReport(int MOOCD_CERTIFICATION_ID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@P_MOOCD_CERTIFICATION_ID", MOOCD_CERTIFICATION_ID);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_SELECT_ACHIEVEMENT_MOOCS_CERTIFICATION_REPORT", sqlParams);
            }
            catch (Exception ex)
            {

                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MoocsCertificationReport-> " + ex.ToString());
            }
            return ds;
        }

    }

}
    


