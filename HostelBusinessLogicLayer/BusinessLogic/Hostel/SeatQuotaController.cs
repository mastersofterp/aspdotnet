//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : HOSTEL                                                               
// PAGE NAME     : HOSTEL SEAT QUOTA                                           
// CREATION DATE : 13-MAR-2013
// CREATED BY    : UMESH GANORKAR
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class SeatQuotaController
    {
        string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        /// <summary>
        /// Purpose    : This method is used to add or modify the
        ///              hostel SEAT QUOTA.
        /// </summary>
        /// 
        //INSERT / UPdate Seat Quota
        public int AddUpdateSeatQuota( string seat_quotaind, string seat_quotast, string seat_quotaind_per, string seat_quotast_per, string category1, string category1ind_per, string category1st_per, string category2, string category2ind_per, string category2st_per, string category3, string category3ind_per, string category3st_per, string category4, string category4ind_per, string category4st_per, string category5, string category5ind_per, string category5st_per, string totstud, string degreeno, int admbatchno, int semesterno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objDataAccess = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            
                            new SqlParameter("@P_SEATQUOTAIND", seat_quotaind),
                            new SqlParameter("@P_SEATQUOTAST", seat_quotast),
                            new SqlParameter("@P_SEATQUOTAIND_PER", seat_quotaind_per),
                            new SqlParameter("@P_SEATQUOTAST_PER", seat_quotast_per),
                            new SqlParameter("@P_CATEGORYNO1", category1),
                            new SqlParameter("@P_CATEGORYNO1IND_PER", category1ind_per),
                            new SqlParameter("@P_CATEGORYNO1ST_PER", category1st_per),
                            new SqlParameter("@P_CATEGORYNO2", category2),
                            new SqlParameter("@P_CATEGORYNO2IND_PER", category2ind_per),
                            new SqlParameter("@P_CATEGORYNO2ST_PER", category2st_per),
                            new SqlParameter("@P_CATEGORYNO3", category3),
                            new SqlParameter("@P_CATEGORYNO3IND_PER", category3ind_per),
                            new SqlParameter("@P_CATEGORYNO3ST_PER", category3st_per),
                            new SqlParameter("@P_CATEGORYNO4", category4),
                            new SqlParameter("@P_CATEGORYNO4IND_PER", category4ind_per),
                            new SqlParameter("@P_CATEGORYNO4ST_PER", category4st_per),
                            new SqlParameter("@P_CATEGORYNO5", category5),
                            new SqlParameter("@P_CATEGORYNO5IND_PER", category5ind_per),
                             new SqlParameter("@P_CATEGORYNO5ST_PER", category5st_per),
                             new SqlParameter("@P_TOTALSTUD", totstud),
                            new SqlParameter("@P_DEGREENO", degreeno),
                            new SqlParameter("@P_ADMBATCHNO", admbatchno),
                            new SqlParameter("@P_SEMESTERNO", semesterno),
                            new SqlParameter("@P_OUT", SqlDbType.Int) 
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objDataAccess.ExecuteNonQuerySP("PKG_ACAD_HOSTEL_INS_SEAT_QUOTA", sqlParams, true);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else if (Convert.ToInt32(ret) == 1)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    retStatus = Convert.ToInt32(CustomStatus.Error);

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.AddVerification-> " + ex.ToString());
            }

            return retStatus;
        }

        //GET STUDENT FOR HALL TICKET
        public DataSet GetDegreewise(int semesterno, int batchno )
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSH = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[1] = new SqlParameter("@P_ADMBATCH", batchno);

                ds = objSH.ExecuteDataSetSP("PKG_ACAD_HOSTEL_GET_DEGREEWISE_LIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamControlle.GetStudentsForHallTicket-> " + ex.ToString());
            }
            return ds;
        }
        public DataSet GetDegreewisePercentage(int semesterno, int batchno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSH = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[1] = new SqlParameter("@P_ADMBATCH", batchno);

                ds = objSH.ExecuteDataSetSP("PKG_ACAD_HOSTEL_GET_DEGREEWISE_LIST_PERCENTAGE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamControlle.GetStudentsForHallTicket-> " + ex.ToString());
            }
            return ds;
        }

        public int AddUpdateQuota(SeatQuota objSeatQuotaEntity)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    
                   
                    new SqlParameter("@P_BATCH_NO",objSeatQuotaEntity.BatchNo),
                    new SqlParameter("@P_ALLINDIASQNO", objSeatQuotaEntity.AllIndiaSQNo),
                    new SqlParameter("@P_STATELEVELSQNO", objSeatQuotaEntity.StateLevelSQNo),
                    new SqlParameter("@P_ALLINDIAPER", objSeatQuotaEntity.AllIndiaPer),
                    new SqlParameter("@P_STATELEVELPER", objSeatQuotaEntity.StateLevelPer),
                    new SqlParameter("@P_ALLINDIA_GENPER", objSeatQuotaEntity.AllIndia_GENPer),
                    new SqlParameter("@P_STATELEVEL_GENPER", objSeatQuotaEntity.StateLevel_GENPer),
                    new SqlParameter("@P_ALLINDIA_OBCPER", objSeatQuotaEntity.AllIndia_OBCPer),
                    new SqlParameter("@P_STATELEVEL_OBCPER", objSeatQuotaEntity.StateLevel_OBCPer),
                    new SqlParameter("@P_ALLINDIA_SCPER", objSeatQuotaEntity.AllIndia_SCPer),
                    new SqlParameter("@P_STATELEVEL_SCPER", objSeatQuotaEntity.StateLevel_SCPer),
                    new SqlParameter("@P_ALLINDIA_STPER", objSeatQuotaEntity.AllIndia_STPer),
                    new SqlParameter("@P_STATELEVEL_STPER", objSeatQuotaEntity.StateLevel_STPer),
                    new SqlParameter("@P_ALLINDIA_NTPER", objSeatQuotaEntity.AllIndia_NTPer),
                    new SqlParameter("@P_STATELEVEL_NTPER", objSeatQuotaEntity.StateLevel_NTPer),
                    new SqlParameter("@P_COLLEGE_CODE", objSeatQuotaEntity.CollegeCode),
                    new SqlParameter("@P_OUT", SqlDbType.Int) 
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_HOSTEL_QUOTA_INSERT", sqlParams, true);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else if (Convert.ToInt32(ret) == 1)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    retStatus = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SeatQuotaController.AddQuota() --> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }

      

    }
}
