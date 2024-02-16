using System;
using System.Data;
using System.Web;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;

using System.Data.SqlClient;

namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            /// <summary>
            /// This ReferenceController is used to control Reff table.
            /// </summary>
            public class ReferenceController
            {
                /// <summary>
                /// ConnectionString
                /// </summary>
                private string uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                /// <summary>
                /// This method is used to get refference from Reff table..
                /// </summary>
                /// <returns>SqlDataReader</returns>
                public SqlDataReader GetReference()
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        dr = objSQLHelper.ExecuteReaderSP("PKG_REFF_SP_ALL_FIELDS", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ReferenceController.GetErrors-> " + ex.ToString());
                    }
                    return dr;
                }

                /// <summary>
                /// This method is used to update errors or refference from Reff table.
                /// </summary>
                /// <param name="objRef">objRef is the object of Reference class.</param>
                /// <returns>Integer CustomStatus - Record Updated or Reference</returns>
                //public int Update(Reference objRef)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                //        SqlParameter[] objParams = null;

                //        //UpdateFaculty Reference
                //        objParams = new SqlParameter[31];
                //        objParams[0] = new SqlParameter("@P_ERRORS", objRef.Errors);
                //        objParams[1] = new SqlParameter("@P_COLLEGENAME", objRef.CollegeName);
                //        objParams[2] = new SqlParameter("@P_COLL_ADDRESS", objRef.CollegeAddress);
                //        if (objRef.CollegeLogo == null)
                //            objParams[3] = new SqlParameter("@P_COLL_LOGO", DBNull.Value);
                //        else
                //            objParams[3] = new SqlParameter("@P_COLL_LOGO", objRef.CollegeLogo);

                //        objParams[3].SqlDbType = SqlDbType.Image;

                //        objParams[4] = new SqlParameter("@P_COLLEGECODE", objRef.CollegeCode);
                //        objParams[5] = new SqlParameter("@P_PHONE", objRef.Phone);
                //        objParams[6] = new SqlParameter("@P_EMAIL", objRef.EmailID);
                //        objParams[7] = new SqlParameter("@P_FAC_USERTYPE", objRef.Fac_UserType);
                //        objParams[8] = new SqlParameter("@P_ENROLLMENTNO", objRef.EnrollmentNo);
                //        objParams[9] = new SqlParameter("@P_ADM_LATE_FEE_AMT", objRef.Admlatefee);
                //        objParams[10] = new SqlParameter("@P_FEEDBACK", objRef.Feedback);
                //        objParams[11] = new SqlParameter("@P_STARTYEAR", objRef.StartYear);
                //        objParams[12] = new SqlParameter("@P_ENDYEAR", objRef.EndYear);
                //        objParams[13] = new SqlParameter("@P_RESETCOUNTER", objRef.ResetCounter);
                //        objParams[14] = new SqlParameter("@P_TIME_TABLE", objRef.Timetable);

                //        objParams[15] = new SqlParameter("@P_EMAILSVCID", objRef.Emailsvcid);
                //        objParams[16] = new SqlParameter("@P_EMAILSVCPWD", objRef.Emailsvcpwd);
                //        objParams[17] = new SqlParameter("@P_SMSSVCID", objRef.SMSsvcid);
                //        objParams[18] = new SqlParameter("@P_SMSSVCPWD", objRef.SMSsvcpwd);
                //        objParams[19] = new SqlParameter("@P_ATTEMPT", objRef.Attempt);
                //        objParams[20] = new SqlParameter("@P_ALLOWLOGPOPUP", objRef.AllowLogoutpopup);
                //        objParams[21] = new SqlParameter("@P_POPUPDURATION", objRef.Popupduration);
                //        objParams[22] = new SqlParameter("@P_FASCILITY", objRef.Fascility);
                //        objParams[23] = new SqlParameter("@P_POPUP_FLAG", objRef.POPUP_FLAG);
                //        objParams[24] = new SqlParameter("@P_POPUP_MSG", objRef.POPUP_MSG);
                //        objParams[25] = new SqlParameter("@P_USERPROFILESENDER", objRef.userProfileSender);
                //        objParams[26] = new SqlParameter("@P_USERPROFILESUBJECT", objRef.userProfileSubject);

                //        objParams[27] = new SqlParameter("@P_MARKENTRYOTP", objRef.MarkEntry_OTP);
                //        objParams[28] = new SqlParameter("@P_MARKENTRYEMAIL", objRef.MarkSaveLock_Email);
                //        objParams[29] = new SqlParameter("@P_MARKENTRYESMS", objRef.MarkSaveLock_SMS);
                //        objParams[30] = new SqlParameter("@P_Course_Reg_B_Time_Table", objRef.Course_Reg_B_Time_Table);
                     
                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_REFF_SP_UPD_REFF", objParams, false) != null)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ReferenceController.UpdateFaculty-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}


                ///// <summary>
                ///// This method is used to update errors or refference from Reff table.
                ///// </summary>
                ///// <param name="objRef">objRef is the object of Reference class.</param>
                ///// <returns>Integer CustomStatus - Record Updated or Reference</returns>
                //public int Update(Reference objRef, int IA_Marks, int PCA_Marks)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                //        SqlParameter[] objParams = null;

                //        //UpdateFaculty Reference
                //        objParams = new SqlParameter[40];
                //        objParams[0] = new SqlParameter("@P_ERRORS", objRef.Errors);
                //        objParams[1] = new SqlParameter("@P_COLLEGENAME", objRef.CollegeName);
                //        objParams[2] = new SqlParameter("@P_COLL_ADDRESS", objRef.CollegeAddress);
                //        if (objRef.CollegeLogo == null)
                //            objParams[3] = new SqlParameter("@P_COLL_LOGO", DBNull.Value);
                //        else
                //            objParams[3] = new SqlParameter("@P_COLL_LOGO", objRef.CollegeLogo);

                //        objParams[3].SqlDbType = SqlDbType.Image;

                //        objParams[4] = new SqlParameter("@P_COLLEGECODE", objRef.CollegeCode);
                //        objParams[5] = new SqlParameter("@P_PHONE", objRef.Phone);
                //        objParams[6] = new SqlParameter("@P_EMAIL", objRef.EmailID);
                //        objParams[7] = new SqlParameter("@P_FAC_USERTYPE", objRef.Fac_UserType);
                //        objParams[8] = new SqlParameter("@P_ENROLLMENTNO", objRef.EnrollmentNo);
                //        objParams[9] = new SqlParameter("@P_ADM_LATE_FEE_AMT", objRef.Admlatefee);
                //        objParams[10] = new SqlParameter("@P_FEEDBACK", objRef.Feedback);
                //        objParams[11] = new SqlParameter("@P_STARTYEAR", objRef.StartYear);
                //        objParams[12] = new SqlParameter("@P_ENDYEAR", objRef.EndYear);
                //        objParams[13] = new SqlParameter("@P_RESETCOUNTER", objRef.ResetCounter);
                //        objParams[14] = new SqlParameter("@P_TIME_TABLE", objRef.Timetable);

                //        objParams[15] = new SqlParameter("@P_EMAILSVCID", objRef.Emailsvcid);
                //        objParams[16] = new SqlParameter("@P_EMAILSVCPWD", objRef.Emailsvcpwd);
                //        objParams[17] = new SqlParameter("@P_SMSSVCID", objRef.SMSsvcid);
                //        objParams[18] = new SqlParameter("@P_SMSSVCPWD", objRef.SMSsvcpwd);
                //        objParams[19] = new SqlParameter("@P_ATTEMPT", objRef.Attempt);
                //        objParams[20] = new SqlParameter("@P_ALLOWLOGPOPUP", objRef.AllowLogoutpopup);
                //        objParams[21] = new SqlParameter("@P_POPUPDURATION", objRef.Popupduration);
                //        objParams[22] = new SqlParameter("@P_FASCILITY", objRef.Fascility);
                //        objParams[23] = new SqlParameter("@P_POPUP_FLAG", objRef.POPUP_FLAG);
                //        objParams[24] = new SqlParameter("@P_POPUP_MSG", objRef.POPUP_MSG);
                //        objParams[25] = new SqlParameter("@P_USERPROFILESENDER", objRef.userProfileSender);
                //        objParams[26] = new SqlParameter("@P_USERPROFILESUBJECT", objRef.userProfileSubject);

                //        objParams[27] = new SqlParameter("@P_MARKENTRYOTP", objRef.MarkEntry_OTP);
                //        objParams[28] = new SqlParameter("@P_MARKENTRYEMAIL", objRef.MarkSaveLock_Email);
                //        objParams[29] = new SqlParameter("@P_MARKENTRYESMS", objRef.MarkSaveLock_SMS);
                //        objParams[30] = new SqlParameter("@P_Course_Reg_B_Time_Table", objRef.Course_Reg_B_Time_Table);
                //        objParams[31] = new SqlParameter("@P_BCKATTENDANCEDAYS", objRef.AttendanceBackDays);   // ADDED BY SARANG
                //        objParams[32] = new SqlParameter("@P_ENDSEMBY_DECODE_OR_ENROLL", objRef.ENDSEMBY_DECODE_OR_ENROLL);
                //        objParams[33] = new SqlParameter("@P_IAMARKS", IA_Marks);   //Added by Nikhil on 01/04/2021 for IA Marks
                //        objParams[34] = new SqlParameter("@P_PCAMARKS", PCA_Marks); //Added by Nikhil on 01/04/2021 for PCA Marks
                //        objParams[35] = new SqlParameter("@P_ADMIN_LEVEL_MARKS_ENTRY", objRef.Admin_Level_Marks_Entry); //Added Mahesh on Dated 23/06/2021
                //        objParams[36] = new SqlParameter("@P_UPDATE_OLDEXAM_DATA_MIGRATION", objRef.Update_OldExam_Data_Migration); //Added Deepali on Dated 12/07/2021
                //        objParams[37] = new SqlParameter("@P_RECEIPT_CANCEL", objRef.Receipt_Cancel);//Added By Dileep Kare on 26.07.2021
                //        objParams[38] = new SqlParameter("@P_LATE_FINE_CANCEL_AUTHORITY_FAC_ID", objRef.Late_Fine_Cancel_Authority_Fac_ID);//Added By shailendra k on 03.01.2023
                //        if (objRef.CollegeBanner == null)
                //            objParams[39] = new SqlParameter("@P_COLL_Banner", DBNull.Value);
                //        else
                //            objParams[39] = new SqlParameter("@P_COLL_Banner", objRef.CollegeBanner);

                //            objParams[39].SqlDbType = SqlDbType.Image;


                //      //  objParams[38] = new SqlParameter("@P_COLL_Banner", objRef.CollegeBanner);//Added tanu  on 08/12/2022
                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_REFF_SP_UPD_REFF", objParams, false) != null)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ReferenceController.UpdateFaculty-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}
                public int Update(Reference objRef, int IA_Marks, int PCA_Marks, int maintenanceFlag, DateTime maintenanceDateTime, int? maintenanceEndTime, long? AlertFreq)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;

                        //UpdateFaculty Reference
                        objParams = new SqlParameter[45];
                        objParams[0] = new SqlParameter("@P_ERRORS", objRef.Errors);
                        objParams[1] = new SqlParameter("@P_COLLEGENAME", objRef.CollegeName);
                        objParams[2] = new SqlParameter("@P_COLL_ADDRESS", objRef.CollegeAddress);
                        if (objRef.CollegeLogo == null)
                            objParams[3] = new SqlParameter("@P_COLL_LOGO", DBNull.Value);
                        else
                            objParams[3] = new SqlParameter("@P_COLL_LOGO", objRef.CollegeLogo);

                        objParams[3].SqlDbType = SqlDbType.Image;

                        objParams[4] = new SqlParameter("@P_COLLEGECODE", objRef.CollegeCode);
                        objParams[5] = new SqlParameter("@P_PHONE", objRef.Phone);
                        objParams[6] = new SqlParameter("@P_EMAIL", objRef.EmailID);
                        objParams[7] = new SqlParameter("@P_FAC_USERTYPE", objRef.Fac_UserType);
                        objParams[8] = new SqlParameter("@P_ENROLLMENTNO", objRef.EnrollmentNo);
                        objParams[9] = new SqlParameter("@P_ADM_LATE_FEE_AMT", objRef.Admlatefee);
                        objParams[10] = new SqlParameter("@P_FEEDBACK", objRef.Feedback);
                        objParams[11] = new SqlParameter("@P_STARTYEAR", objRef.StartYear);
                        objParams[12] = new SqlParameter("@P_ENDYEAR", objRef.EndYear);
                        objParams[13] = new SqlParameter("@P_RESETCOUNTER", objRef.ResetCounter);
                        objParams[14] = new SqlParameter("@P_TIME_TABLE", objRef.Timetable);

                        objParams[15] = new SqlParameter("@P_EMAILSVCID", objRef.Emailsvcid);
                        objParams[16] = new SqlParameter("@P_EMAILSVCPWD", objRef.Emailsvcpwd);
                        objParams[17] = new SqlParameter("@P_SMSSVCID", objRef.SMSsvcid);
                        objParams[18] = new SqlParameter("@P_SMSSVCPWD", objRef.SMSsvcpwd);
                        objParams[19] = new SqlParameter("@P_ATTEMPT", objRef.Attempt);
                        objParams[20] = new SqlParameter("@P_ALLOWLOGPOPUP", objRef.AllowLogoutpopup);
                        objParams[21] = new SqlParameter("@P_POPUPDURATION", objRef.Popupduration);
                        objParams[22] = new SqlParameter("@P_FASCILITY", objRef.Fascility);
                        objParams[23] = new SqlParameter("@P_POPUP_FLAG", objRef.POPUP_FLAG);
                        objParams[24] = new SqlParameter("@P_POPUP_MSG", objRef.POPUP_MSG);
                        objParams[25] = new SqlParameter("@P_USERPROFILESENDER", objRef.userProfileSender);
                        objParams[26] = new SqlParameter("@P_USERPROFILESUBJECT", objRef.userProfileSubject);

                        objParams[27] = new SqlParameter("@P_MARKENTRYOTP", objRef.MarkEntry_OTP);
                        objParams[28] = new SqlParameter("@P_MARKENTRYEMAIL", objRef.MarkSaveLock_Email);
                        objParams[29] = new SqlParameter("@P_MARKENTRYESMS", objRef.MarkSaveLock_SMS);
                        objParams[30] = new SqlParameter("@P_Course_Reg_B_Time_Table", objRef.Course_Reg_B_Time_Table);
                        objParams[31] = new SqlParameter("@P_BCKATTENDANCEDAYS", objRef.AttendanceBackDays);   // ADDED BY SARANG
                        objParams[32] = new SqlParameter("@P_ENDSEMBY_DECODE_OR_ENROLL", objRef.ENDSEMBY_DECODE_OR_ENROLL);
                        objParams[33] = new SqlParameter("@P_IAMARKS", IA_Marks);   //Added by Nikhil on 01/04/2021 for IA Marks
                        objParams[34] = new SqlParameter("@P_PCAMARKS", PCA_Marks); //Added by Nikhil on 01/04/2021 for PCA Marks
                        objParams[35] = new SqlParameter("@P_ADMIN_LEVEL_MARKS_ENTRY", objRef.Admin_Level_Marks_Entry); //Added Mahesh on Dated 23/06/2021
                        objParams[36] = new SqlParameter("@P_UPDATE_OLDEXAM_DATA_MIGRATION", objRef.Update_OldExam_Data_Migration); //Added Deepali on Dated 12/07/2021
                        objParams[37] = new SqlParameter("@P_RECEIPT_CANCEL", objRef.Receipt_Cancel);//Added By Dileep Kare on 26.07.2021
                        objParams[38] = new SqlParameter("@P_LATE_FINE_CANCEL_AUTHORITY_FAC_ID", objRef.Late_Fine_Cancel_Authority_Fac_ID);//Added By shailendra k on 03.01.2023
                        //*******Added by Shahbaz Ahmad on 07/02/2023 for Maintenance******************//
                        objParams[39] = new SqlParameter("@P_MAINTENANCE_FLAG", maintenanceFlag);
                        objParams[40] = new SqlParameter("@P_MAINTENANCE_START_TIME", maintenanceDateTime);
                        objParams[41] = new SqlParameter("@P_MAINTENANCE_END_TIME", maintenanceEndTime);
                        objParams[42] = new SqlParameter("@P_ALERT_FREQUENCY", AlertFreq);
                        //******************************************************************************//
                        objParams[43] = new SqlParameter("@P_ERROR_LOG_EMAIL", objRef.Error_Log_Email); //Added by Anurag Baghele on 15-02-2024
                        if (objRef.CollegeBanner == null)
                            objParams[44] = new SqlParameter("@P_COLL_Banner", DBNull.Value);
                        else
                            objParams[44] = new SqlParameter("@P_COLL_Banner", objRef.CollegeBanner);

                        objParams[44].SqlDbType = SqlDbType.Image;


                        //  objParams[38] = new SqlParameter("@P_COLL_Banner", objRef.CollegeBanner);//Added tanu  on 08/12/2022
                        if (objSQLHelper.ExecuteNonQuerySP("PKG_REFF_SP_UPD_REFF", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ReferenceController.UpdateFaculty-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int UpdateRegistrarSign(Reference objRef)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(uaims_constr);
                        SqlParameter[] objParams = null;

                        //UpdateFaculty Reference
                        objParams = new SqlParameter[1];

                        objParams[0] = new SqlParameter("@P_REG_SIGN", objRef.CollegeLogo);



                        if (objSQLHelper.ExecuteNonQuerySP("PKG_REFF_SP_UPD_REGISTRAR_SIGN", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ReferenceController.UpdateFaculty-> " + ex.ToString());
                    }
                    return retStatus;
                }
            }

        }//END: BusinessLayer.BusinessLogic

    }//END: UAIMS  

}//END: IITMS