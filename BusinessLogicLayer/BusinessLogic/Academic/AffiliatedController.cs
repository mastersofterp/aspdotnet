using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IITMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;
using System.Data;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Academic;
namespace IITMS.UAIMS.BusinessLogicLayer.BusinessLogic
{
    
   public  class AffiliatedController
    {
       
        string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        //Added by Nikhil Vinod Lambe on 01/04/2021 to add Affiliated Institute Details
        public int AddAffInstituteDetails(Affiliated aff)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[19];
                objParams[0] = new SqlParameter("@P_UA_NO",aff.UaNo);
                objParams[1] = new SqlParameter("@P_COLLEGE_CODE",aff.CollegeCode );
                objParams[2] = new SqlParameter("@P_ADDRESS",aff.Address);
                objParams[3] = new SqlParameter("@P_PINCODE",aff.PinCode);
                objParams[4] = new SqlParameter("@P_DISTRICT",aff.District);
                objParams[5] = new SqlParameter("@P_WEBSITE",aff.Website);
                objParams[6] = new SqlParameter("@P_PHONE_NO",aff.PhoneNo);
                objParams[7] = new SqlParameter("@P_ALTPHONE_NO",aff.Alt_PhoneNo);
                objParams[8] = new SqlParameter("@P_MOBILE_NO",aff.MobileNo);
                objParams[9] = new SqlParameter("@P_ALTMOBILE_NO",aff.Alt_MobileNo);
                objParams[10] = new SqlParameter("@P_FAXNO",aff.FaxNo);
                objParams[11] = new SqlParameter("@P_EMAILID",aff.EmailId);
                objParams[12] = new SqlParameter("@P_ALTEMAILID",aff.Alt_EmailId);
                objParams[13] = new SqlParameter("@P_AUTH_ID", aff.Authority);
                objParams[14] = new SqlParameter("@P_AUTH_NAME", aff.Authority_Name);
                objParams[15] = new SqlParameter("@P_CONTACT_PERSON_NAME", aff.ContactPersonName);
                objParams[16] = new SqlParameter("@P_CONTACT_PERSON_MOBILE", aff.ContactPersonMob);
                objParams[17] = new SqlParameter("@P_CONTACT_PERSON_EMAIL", aff.ContactPersonEmail);
                objParams[18] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[18].Direction = ParameterDirection.Output;

                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_AFF_INS_INSTITUTE_DETAILS", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.AddAffInstituteDetails --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
        public int UpdateAffInstituteDetails(Affiliated aff)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[19];
                objParams[0] = new SqlParameter("@P_UA_NO", aff.UaNo);
                objParams[1] = new SqlParameter("@P_COLLEGE_CODE", aff.CollegeCode);
                objParams[2] = new SqlParameter("@P_ADDRESS", aff.Address);
                objParams[3] = new SqlParameter("@P_PINCODE", aff.PinCode);
                objParams[4] = new SqlParameter("@P_DISTRICT", aff.District);
                objParams[5] = new SqlParameter("@P_WEBSITE", aff.Website);
                objParams[6] = new SqlParameter("@P_PHONE_NO", aff.PhoneNo);
                objParams[7] = new SqlParameter("@P_ALTPHONE_NO", aff.Alt_PhoneNo);
                objParams[8] = new SqlParameter("@P_MOBILE_NO", aff.MobileNo);
                objParams[9] = new SqlParameter("@P_ALTMOBILE_NO", aff.Alt_MobileNo);
                objParams[10] = new SqlParameter("@P_FAXNO", aff.FaxNo);
                objParams[11] = new SqlParameter("@P_EMAILID", aff.EmailId);
                objParams[12] = new SqlParameter("@P_ALTEMAILID", aff.Alt_EmailId);
                objParams[13] = new SqlParameter("@P_AUTH_ID", aff.Authority);
                objParams[14] = new SqlParameter("@P_AUTH_NAME", aff.Authority_Name);
                objParams[15] = new SqlParameter("@P_CONTACT_PERSON_NAME", aff.ContactPersonName);
                objParams[16] = new SqlParameter("@P_CONTACT_PERSON_MOBILE", aff.ContactPersonMob);
                objParams[17] = new SqlParameter("@P_CONTACT_PERSON_EMAIL", aff.ContactPersonEmail);
                objParams[18] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[18].Direction = ParameterDirection.Output;

                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_AFF_UPD_INSTITUTE_DETAILS", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.UpdateAffInstituteDetails --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        //Added by Nikhil Vinod Lambe on 07/04/2021 to add Affiliated Fees Category
        // Modified by Nikhil V. Lambe on 11/06/2021
        public int AddAffFeesCategory(string FeesCategory, string ShortName, string Code, int Active, int CreatedBy, int CollectionMode, string ipAddress, int collectionCategory)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_FEE_CATEGORY", FeesCategory);
                objParams[1] = new SqlParameter("@P_SHORT_NAME", ShortName);
                objParams[2] = new SqlParameter("@P_CODE", Code);
                objParams[3] = new SqlParameter("@P_ACTIVE", Active);
                objParams[4] = new SqlParameter("@P_CREATED_BY", CreatedBy);
                objParams[5] = new SqlParameter("@P_COLLECTION_MODE", CollectionMode);
                objParams[6] = new SqlParameter("@P_IPADDRESS", ipAddress);
                objParams[7] = new SqlParameter("@P_COLLECTION_CATEGORY", collectionCategory);
                objParams[8] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;

                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_INS_AFF_FEES_CATEGORY", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
                if (obj.Equals(2627))
                {
                    status = Convert.ToInt32(CustomStatus.RecordExist);
                }
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.AddAffFeesCategory --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
        //Added by Nikhil Vinod Lambe on 07/04/2021 to update Affiliated Fees Category
        public int UpdateAffFeesCategory(string FeesCategory, string ShortName, string Code, int Active, int ModifyBy, int FeeNO, int CollectionMode, string ipAddress, int collectionCategory)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_FEE_CATEGORY_NO", FeeNO);
                objParams[1] = new SqlParameter("@P_FEE_CATEGORY", FeesCategory);
                objParams[2] = new SqlParameter("@P_SHORT_NAME", ShortName);
                objParams[3] = new SqlParameter("@P_CODE", Code);
                objParams[4] = new SqlParameter("@P_ACTIVE", Active);
                objParams[5] = new SqlParameter("@P_MODIFIED_BY", ModifyBy);
                objParams[6] = new SqlParameter("@P_COLLECTION_MODE", CollectionMode);
                objParams[7] = new SqlParameter("@P_IPADDRESS", ipAddress);
                objParams[8] = new SqlParameter("@P_COLLECTION_CATEGORY", collectionCategory);
                objParams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;

                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_UPDATE_AFF_FEES_CATEGORY", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
                //if (obj.Equals(2627))
                //{
                //    status = Convert.ToInt32(CustomStatus.RecordExist);
                //}
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.UpdateAffFeesCategory --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        //Added by Nikhil Vinod Lambe on 07/04/2021 to add Affiliated Fees Category
        public int Insert_Fee_Defintion_For_Affiliated(int admyear, int sessionno, int fee_cat, string remark, DateTime deadline_date, int college_id, decimal amount, int created_by, string ipaddress)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_ADMYEAR", admyear);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[2] = new SqlParameter("@P_FEE_CATEGORY_NO", fee_cat);
                objParams[3] = new SqlParameter("@P_REMARK", remark);
                objParams[4] = new SqlParameter("@P_DEADLINE_DATE", deadline_date);
                objParams[5] = new SqlParameter("@P_COLLEGE_ID", college_id);
                objParams[6] = new SqlParameter("@P_AMOUNT", amount);
                objParams[7] = new SqlParameter("@P_CREATED_BY", created_by);
                objParams[8] = new SqlParameter("@P_IPADDRESS", ipaddress);
                objParams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;

                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_INSERT_AFF_FEES_DEFINATION", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
                if (obj.Equals(2627))
                {
                    status = Convert.ToInt32(CustomStatus.RecordExist);
                }
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.Insert_Fee_Defintion_For_Affiliated --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
        public int Update_Fee_Defintion_For_Affiliated(int AdmYear, int SessionNo, int Fee_cat, string Remark, Affiliated af, int College_id, decimal Amount, int modified_by, int created_by, string ipaddress, string Code, int programmeType = 0, int level = 0, int duration = 0, int intakeFrom = 0, int intakeTo = 0, int colCategory = 0)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[18];
                objParams[0] = new SqlParameter("@P_ADMYEAR", AdmYear);
                objParams[1] = new SqlParameter("@P_SESSIONNO", SessionNo);
                objParams[2] = new SqlParameter("@P_FEE_CATEGORY_NO", Fee_cat);
                objParams[3] = new SqlParameter("@P_REMARK", Remark);
                objParams[4] = new SqlParameter("@P_DEADLINE_DATE", af.Deadline_date);
                objParams[5] = new SqlParameter("@P_COLLEGE_ID", College_id);
                objParams[6] = new SqlParameter("@P_AMOUNT", Amount);
                objParams[7] = new SqlParameter("@P_MODIFIED_BY", modified_by);
                objParams[8] = new SqlParameter("@P_CREATED_BY", created_by);
                objParams[9] = new SqlParameter("@P_IPADDRESS", ipaddress);
                objParams[10] = new SqlParameter("@P_COLLEGE_CODE", Code);
                objParams[11] = new SqlParameter("@P_BRANCH_TYPE", programmeType);
                objParams[12] = new SqlParameter("@P_LEVEL", level);
                objParams[13] = new SqlParameter("@P_DURATION", duration);
                objParams[14] = new SqlParameter("@P_INTAKE_FROM", intakeFrom);
                objParams[15] = new SqlParameter("@P_INTAKE_TO", intakeTo);
                objParams[16] = new SqlParameter("@P_COLL_CATEGORY", colCategory);
                objParams[17] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[17].Direction = ParameterDirection.Output;

                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_UPDATE_AFF_FEE_DEFINATION", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001" || obj.ToString() == "2" || obj.ToString() == "1")
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
                //if (obj.Equals(2627))
                //{
                //    status = Convert.ToInt32(CustomStatus.RecordExist);
                //}
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.Update_Fee_Defintion_For_Affiliated --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        // Added By Nikhil V. Lambe on 07/06/2021 to update the fee defination by recievable amount creation
        public int Update_Fee_Defination_By_Recievable__Amount(int AdmYear, int SessionNo, int Fee_cat, string Remark, string deadlineDate, int College_id, decimal Amount, int modified_by, int created_by,
            string ipaddress, string Code, int programmeType, int level, string intakeFrom, string intakeTo, int duration,
            int college_category, int intake, int admittedCount, decimal overallAmount, int branch)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[22];
                objParams[0] = new SqlParameter("@P_ADMYEAR", AdmYear);
                objParams[1] = new SqlParameter("@P_SESSIONNO", SessionNo);
                objParams[2] = new SqlParameter("@P_FEE_CATEGORY_NO", Fee_cat);
                objParams[3] = new SqlParameter("@P_REMARK", Remark);
                objParams[4] = new SqlParameter("@P_DEADLINE_DATE", deadlineDate);
                objParams[5] = new SqlParameter("@P_COLLEGE_ID", College_id);
                objParams[6] = new SqlParameter("@P_AMOUNT", Amount);
                objParams[7] = new SqlParameter("@P_MODIFIED_BY", modified_by);
                objParams[8] = new SqlParameter("@P_CREATED_BY", created_by);
                objParams[9] = new SqlParameter("@P_IPADDRESS", ipaddress);
                objParams[10] = new SqlParameter("@P_COLLEGE_CODE", Code);
                objParams[11] = new SqlParameter("@P_PROGRAMME_TYPE", programmeType);
                objParams[12] = new SqlParameter("@P_LEVEL", level);
                objParams[13] = new SqlParameter("@P_INTAKE_FROM", intakeFrom);
                objParams[14] = new SqlParameter("@P_INTAKE_TO", intakeTo);
                //objParams[15] = new SqlParameter("@P_FEE_SUB_CATEGORY",feeSubCategory);
                objParams[15] = new SqlParameter("@P_DURATION", duration);
                objParams[16] = new SqlParameter("@P_COLL_CATEGORY", college_category);
                objParams[17] = new SqlParameter("@P_INTAKE", intake);
                objParams[18] = new SqlParameter("@P_STUDENT_ADMITTED_COUNT", admittedCount);
                objParams[19] = new SqlParameter("@P_OVERALL_AMOUNT", overallAmount);
                objParams[20] = new SqlParameter("@P_BRANCH", branch);
                objParams[21] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[21].Direction = ParameterDirection.Output;

                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_UPDATE_AFF_FEE_DEFINATION_BY_RECEIVABLE_AMOUNT", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001" || obj.ToString() == "2" || obj.ToString() == "1")
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
                //if (obj.Equals(2627))
                //{
                //    status = Convert.ToInt32(CustomStatus.RecordExist);
                //}
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.Update_Fee_Defintion_For_Affiliated --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        //Added by Nikhil Vinod Lambe on 11/06/2021 to get details of receivable overall amount
        public DataSet GetOverallAmountForReceivable(int admBatch, int feeCategory, int collectionCategory)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_ADMBATCH", admBatch);
                objParams[1] = new SqlParameter("@P_FEE_CATEGORY_NO", feeCategory);
                objParams[2] = new SqlParameter("@P_COLL_CATEGORY", collectionCategory);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_GET_RECEIVABLE_OVERALL_AMOUNT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetOverallAmountForReceivable()-> " + ex.ToString());
            }
            return ds;
        }

        //Added by Nikhil Vinod Lambe on 04/06/2021 to get details of receivable
        public DataSet GetDetailsOfReceivable(int admBatch, int feeCategory, int collectionCategory, int collegeid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_ADMBATCH", admBatch);
                objParams[1] = new SqlParameter("@P_FEE_CATEGORY_NO", feeCategory);
                objParams[2] = new SqlParameter("@P_COLL_CATEGORY", collectionCategory);
                objParams[3] = new SqlParameter("@P_COLLEGE_ID", collegeid);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_GET_DETAILS_OF_RECEIVABLE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetDetailsOfReceivable()-> " + ex.ToString());
            }
            return ds;
        }
        public DataSet GetCollegeList(int AdmYear,int SessionNo,int Fee_cat)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                
                    objParams = new SqlParameter[3];
                    objParams[0] = new SqlParameter("@P_ADMYEAR", AdmYear);
                    objParams[1] = new SqlParameter("@P_SESSIONNO", SessionNo);
                    objParams[2] = new SqlParameter("@P_FEE_CATEGORY_NO", Fee_cat);
                    ds = objSqlHelper.ExecuteDataSetSP("PKG_SP_GET_COLLEGE_LIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetCollegeList --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        //Added by Nikhil Vinod Lambe on 11/06/2021 to update Define Intake Entry
        public int Update_Define_Intake_Entry(Affiliated aff, int createdBy, string ipAddress, int collegeID)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[13];
                objParams[0] = new SqlParameter("@P_COLLEGE_ID", aff.CollegeId);
                objParams[1] = new SqlParameter("@P_BRANCH_TYPE", aff.BranchType);
                objParams[2] = new SqlParameter("@P_LEVEL", aff.Level);
                objParams[3] = new SqlParameter("@P_AFF_DEGREE", aff.Degree);
                objParams[4] = new SqlParameter("@P_AFF_BRANCH", aff.Branch);
                objParams[5] = new SqlParameter("@P_INTAKE", aff.Intake);
                objParams[6] = new SqlParameter("@P_DURATION", aff.Duration);
                objParams[7] = new SqlParameter("@P_CREATED_BY", createdBy);
                objParams[8] = new SqlParameter("@P_IPADDRESS", ipAddress);
                objParams[9] = new SqlParameter("@P_COLLEGE_DEL", collegeID);

                objParams[10] = new SqlParameter("@P_ACTUAL_INTAKE", aff.ActualIntake);
                objParams[11] = new SqlParameter("@P_EXM_INTAKE", aff.ExmIntake);
                objParams[12] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[12].Direction = ParameterDirection.Output;

                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_UPDATE_INTAKE", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
                if (obj.Equals(2627))
                {
                    status = Convert.ToInt32(CustomStatus.RecordExist);
                }
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.Add_Define_Intake_Entry --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet getCollegeListExcel(int AdmYear,int Fee_cat)
        {
            //int retv = 0;
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_ADMYEAR",AdmYear),                          
                            new SqlParameter("@P_FEE_CATEGORY_NO",Fee_cat),                         
                        };

                ds = objSqlHelper.ExecuteDataSetSP("PKG_SP_GET_DATA_FOR_DOWNLOAD_EXCEL", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.AffiliatedController.getCollegeListExcel-> " + ex.ToString());
            }
            return ds;
        }
        public int Add_Fee_Defintion_For_Affiliated_ForExcel(DataTable dt,int Admyear,int Sessionno,int Fee_Cat,string Remark,Affiliated af,int modified_by,int created_by,string ipaddress,string filename)
        {
            int ret = 0;
            try
            {
                SQLHelper objSQL = new SQLHelper(connectionstring);
                SqlParameter[] objPar = new SqlParameter[]
                        {
                            new SqlParameter("@P_TBLBULKDATA",dt),
                            new SqlParameter("@P_ADMYEAR",Admyear),
                            new SqlParameter("@P_SESSIONNO",Sessionno),
                            new SqlParameter("@P_FEE_CATEGORY_NO",Fee_Cat),
                            new SqlParameter("@P_REMARK",Remark),
                            new SqlParameter("@P_DEADLINE_DATE",af.Deadline_date),
                            new SqlParameter("@P_MODIFIED_BY",modified_by),
                            new SqlParameter("@P_CREATED_BY",created_by),
                            new SqlParameter("@P_IPADDRESS",ipaddress),
                            new SqlParameter("@P_FILENAME",filename),

                            new SqlParameter("@P_OUTPUT",DbType.String)
                        };
                objPar[objPar.Length - 1].Direction = ParameterDirection.Output;

                object val = objSQL.ExecuteNonQuerySP("PKG_SP_FEE_DEFINATION_DATA_UPLOAD", objPar, true);
                if (val != null)
                {
                    ret = Convert.ToInt16(val.ToString());
                }
                else
                {
                    ret = -99;
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.AffiliatedController.Add_Fee_Defintion_For_Affiliated_ForExcel-> " + ex.ToString());
            }
            return ret;
        }
        public DataSet getAffiliatedFeesDetails_Excel(int AdmYear,int Fee_category)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[]
                        {
                            new SqlParameter("@P_ADMYEAR",AdmYear),                          
                            new SqlParameter("@P_FEE_CATEGORY_NO",Fee_category),                         
                        };
                ds = objSqlHelper.ExecuteDataSetSP("PKG_SP_GET_FEE_DEFINATION_DATA_FOR_EXCEL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.AffiliatedController.getAffiliatedFeesDetails_Excel-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetAffiliatedFeeDetails_ForAffiliatedUser(int AdmYear, int Fee_category,string Code)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                
                    objParams = new SqlParameter[3];
                    objParams[0] = new SqlParameter("@P_ADMYEAR", AdmYear);
                    objParams[1] = new SqlParameter("@P_FEE_CATEGORY_NO", Fee_category);
                    objParams[2] = new SqlParameter("@P_COLLEGE_CODE",Code);
                    ds = objSqlHelper.ExecuteDataSetSP("PKG_SP_GET_FEE_DETAILS_FOR_AFFILIATED_USER", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetAffiliatedFeeDetails_ForAffiliatedUser --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetCollegeFeeDetails_University(int Admyear, string StartDate, string EndDate, int FeeCategory, int Session, string CollegeID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_ADMYEAR", Admyear);
                objParams[1] = new SqlParameter("@P_STARTDATE", StartDate);
                objParams[2] = new SqlParameter("@P_ENDDATE", EndDate);
                objParams[3] = new SqlParameter("@P_FEE_CATEGORY_NO", FeeCategory);
                objParams[4] = new SqlParameter("@P_SESSIONNO", Session);
                objParams[5] = new SqlParameter("@P_COLLEGE_ID", CollegeID);
                ds = objSqlHelper.ExecuteDataSetSP("PKG_SP_GET_AFFILIATED_FEE_DETAILS_UNIVERSITY", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetCollegeFeeDetails_University --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet GetCollegeFeeDetails_Institute(int Admyear, string StartDate, string EndDate, int FeeCategory, string CollegeCode)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_ADMYEAR", Admyear);
                objParams[1] = new SqlParameter("@P_STARTDATE", StartDate);
                objParams[2] = new SqlParameter("@P_ENDDATE", EndDate);
                objParams[3] = new SqlParameter("@P_FEE_CATEGORY_NO", FeeCategory);
                objParams[4] = new SqlParameter("@P_COLLEGE_CODE", CollegeCode);

                ds = objSqlHelper.ExecuteDataSetSP("PKG_SP_GET_AFFILIATED_FEE_DETAILS_FOR_USER", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetCollegeFeeDetails_Institute --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int InsertOnlinePaymentlog(string userno, string recipt, string PaymentMode, string Amount, string status, string OrderId, string ipAddress)
        {
            int retStatus1 = 0;
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSqlhelper = new SQLHelper(connectionstring);
                SqlParameter[] sqlparam = null;
                {
                    sqlparam = new SqlParameter[8];
                    sqlparam[0] = new SqlParameter("@P_UANO", userno);
                    sqlparam[1] = new SqlParameter("@P_RECIPT", recipt);
                    sqlparam[2] = new SqlParameter("@P_PAYMENTMODE", PaymentMode);
                    sqlparam[3] = new SqlParameter("@P_AMOUNT", Amount);
                    sqlparam[4] = new SqlParameter("@P_STATUS", status);
                    sqlparam[5] = new SqlParameter("@P_ORDER_ID", OrderId);
                    sqlparam[6] = new SqlParameter("@P_IPADDRESS", ipAddress);
                    sqlparam[7] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    sqlparam[7].Direction = ParameterDirection.Output;
                    string idcat = sqlparam[4].Direction.ToString();

                };
                object studid = objSqlhelper.ExecuteNonQuerySP("PKG_AFFILATED_ONLINE_PAYMENT_FOR_LOG_ADMIN", sqlparam, true);
                //if (Convert.ToInt32(studid) == -99)
                //{
                //    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                //}
                //else
                //    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                retStatus1 = Convert.ToInt32(studid);
                return retStatus1;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.InsertOnlinePaymentlog() --> " + ex.Message + " " + ex.StackTrace);
            }

        }

        public int InsertOnlinePayment_TempAffiliatedDCR(int UANO, int FEE_CATEGORY_NO, int COLLEGE_ID, double AMOUNT, string ORDER_ID, string STATUS, string IPADDRESS)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] param = new SqlParameter[]
                        {                         
                            new SqlParameter("@P_UANO", UANO),
                            new SqlParameter("@P_FEE_CATEGORY_NO", FEE_CATEGORY_NO),
                            new SqlParameter("@P_COLLEGE_ID", COLLEGE_ID),
                            new SqlParameter("@P_AMOUNT", AMOUNT),
                            new SqlParameter("@P_ORDER_ID", ORDER_ID),                           
                            new SqlParameter("@P_STATUS", STATUS),
                            new SqlParameter("@P_IPADDRESS", IPADDRESS),
                            new SqlParameter("@P_OUTPUT", SqlDbType.Int)          
                        };
                param[param.Length - 1].Direction = ParameterDirection.Output;
                object ret = objSqlHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_ONLINE_PAYMENT_AFFILATED_DCRTEMP", param, true);

                if (ret != null && ret.ToString() != "-99")
                    retStatus = Convert.ToInt32(ret);
                else
                    retStatus = -99;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.FeeCollectionController.InsertOnlinePayment_TempDCR-> " + ex.ToString());
            }
            return retStatus;
        }

        public int InsertOnlinePayment_AffiliatedDCR(string UserNo, string payId, string transid, double amount, string StatusF)
        {
            int retStatus1 = 0;
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSqlhelper = new SQLHelper(connectionstring);
                SqlParameter[] sqlparam = null;
                {
                    sqlparam = new SqlParameter[6];
                    sqlparam[0] = new SqlParameter("@P_UANO", UserNo);
                    sqlparam[1] = new SqlParameter("@P_ORDER_ID", payId);
                    sqlparam[2] = new SqlParameter("@P_TRANSID", transid);
                    sqlparam[3] = new SqlParameter("@P_AMOUNT", amount);
                    sqlparam[4] = new SqlParameter("@P_PAY_STATUS", StatusF);
                    sqlparam[5] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                    sqlparam[5].Direction = ParameterDirection.Output;
                    string idcat = sqlparam[4].Direction.ToString();

                };
                object studid = objSqlhelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_ONLINE_PAYMENT_AFFILATED_DCRTEMP_TO_DCR", sqlparam, true);

                retStatus1 = Convert.ToInt32(studid);
                return retStatus1;
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.FeeCollectionController.InsertOnlinePayment_DCR() --> " + ex.Message + " " + ex.StackTrace);
            }
        }

        public DataSet GetSuccessPayment_Details(int Admyear, string StartDate, string EndDate, int FeeCategory, int Session, string CollegeID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_ADMYEAR", Admyear);
                objParams[1] = new SqlParameter("@P_STARTDATE", StartDate);
                objParams[2] = new SqlParameter("@P_ENDDATE", EndDate);
                objParams[3] = new SqlParameter("@P_FEE_CATEGORY_NO", FeeCategory);
                objParams[4] = new SqlParameter("@P_SESSIONNO", Session);
                objParams[5] = new SqlParameter("@P_COLLEGE_ID", CollegeID);
                ds = objSqlHelper.ExecuteDataSetSP("PKG_SP_GET_AFFILIATED_PAYMENT_SUCCESS_UNIVERSITY_RPT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetSuccessPayment_Details --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetPendingPayment_Details(int Admyear, string StartDate, string EndDate, int FeeCategory, int Session, string CollegeID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_ADMYEAR", Admyear);
                objParams[1] = new SqlParameter("@P_STARTDATE", StartDate);
                objParams[2] = new SqlParameter("@P_ENDDATE", EndDate);
                objParams[3] = new SqlParameter("@P_FEE_CATEGORY_NO", FeeCategory);
                objParams[4] = new SqlParameter("@P_SESSIONNO", Session);
                objParams[5] = new SqlParameter("@P_COLLEGE_ID", CollegeID);
                ds = objSqlHelper.ExecuteDataSetSP("PKG_SP_GET_AFFILIATED_PAYMENT_PENDING_UNIVERSITY", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetSuccessPayment_Details --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet GetCollegePaymentSuccessDetails_Institute(int Admyear, string StartDate, string EndDate, int FeeCategory, string CollegeCode)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_ADMYEAR", Admyear);
                objParams[1] = new SqlParameter("@P_STARTDATE", StartDate);
                objParams[2] = new SqlParameter("@P_ENDDATE", EndDate);
                objParams[3] = new SqlParameter("@P_FEE_CATEGORY_NO", FeeCategory);
                objParams[4] = new SqlParameter("@P_COLLEGE_CODE", CollegeCode);

                ds = objSqlHelper.ExecuteDataSetSP("PKG_SP_GET_AFFILIATED_PAYMENT_SUCCESS_DETAILS_FOR_USER", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetCollegePaymentSuccessDetails_Institute --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet GetCollegePaymentPendingDetails_Institute(int Admyear, string StartDate, string EndDate, int FeeCategory, string CollegeCode)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_ADMYEAR", Admyear);
                objParams[1] = new SqlParameter("@P_STARTDATE", StartDate);
                objParams[2] = new SqlParameter("@P_ENDDATE", EndDate);
                objParams[3] = new SqlParameter("@P_FEE_CATEGORY_NO", FeeCategory);
                objParams[4] = new SqlParameter("@P_COLLEGE_CODE", CollegeCode);

                ds = objSqlHelper.ExecuteDataSetSP("PKG_SP_GET_AFFILIATED_PAYMENT_PENDING_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetCollegePaymentSuccessDetails_Institute --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetReceiveandDemand_Details(int Admyear, string StartDate, string EndDate, int FeeCategory, int Session, string CollegeID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_ADMYEAR", Admyear);
                objParams[1] = new SqlParameter("@P_STARTDATE", StartDate);
                objParams[2] = new SqlParameter("@P_ENDDATE", EndDate);
                objParams[3] = new SqlParameter("@P_FEE_CATEGORY_NO", FeeCategory);
                objParams[4] = new SqlParameter("@P_SESSIONNO", Session);
                objParams[5] = new SqlParameter("@P_COLLEGE_ID", CollegeID);
                ds = objSqlHelper.ExecuteDataSetSP("PKG_SP_GET_AFFILIATED_RECIEVE_AND_DEMAND", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetReceiveandDemand_Details --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //Modified by Pranita on 21/08/2021 to add programmebranch_code column
        public int Insert_Affiliated_Branch(int degreeno, string branchName, string shortName, string collegeCode, int levelType, int createdBy, string ipAddress, string programmebranch_code)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_DEGREENO", degreeno);
                objParams[1] = new SqlParameter("@P_BRANCHNAME", branchName);
                objParams[2] = new SqlParameter("@P_SHORTNAME", shortName);
                objParams[3] = new SqlParameter("@P_COLLEGE_CODE", collegeCode);
                objParams[4] = new SqlParameter("@P_LEVEL_TYPE", levelType);
                objParams[5] = new SqlParameter("@P_CREATED_BY", createdBy);
                objParams[6] = new SqlParameter("@P_IPADDRESS", ipAddress);
                objParams[7] = new SqlParameter("@P_PROGRAMMEBRANCH_CODE", programmebranch_code);

                objParams[8] = new SqlParameter("@P_BRANCHNO", status);
                objParams[8].Direction = ParameterDirection.Output;

                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_INS_AFFILIATED_BRANCH", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
                if (obj.Equals(2627))
                {
                    status = Convert.ToInt32(CustomStatus.RecordExist);
                }
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.Insert_Fee_Defintion_For_Affiliated --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
        //Added by Nikhil Vinod Lambe on 26/05/2021 to Get Affiliated Branch by Branchno
        public DataSet GetAffiliatedBranch_ByBranchno(int Branchno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_BRANCHNO", Branchno);
                ds = objSqlHelper.ExecuteDataSetSP("PKG_SP_AFF_BRANCH_GET_BY_NO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetAffiliatedBranch_ByBranchno --> " + ex.Message + " " + ex.StackTrace);

            }
            return ds;
        }

        //Modified by Pranita on 21/08/2021 to add programmebranch_code column
        public int UpdateAffiliatedBranch(int branchNo, string branchName, string shortName, string collegeCode, int LevelType, int modifiedBy, string ipAddress, int degreeNo, string programmebranch_code)
        {
            int status;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] sqlParams = new SqlParameter[]
                {                    
                    new SqlParameter("@P_BRANCHNAME",branchName),
                    new SqlParameter("@P_SHORTNAME",shortName),
                    new SqlParameter("@P_COLLEGE_CODE",collegeCode),
                    new SqlParameter("@P_LEVEL_TYPE",LevelType),
                    new SqlParameter("@P_MODIFIED_BY",modifiedBy),
                    new SqlParameter("@P_IPADDRESS",ipAddress),
                    new SqlParameter("@P_DEGREENO",degreeNo),
                    new SqlParameter("@P_PROGRAMMEBRANCH_CODE",programmebranch_code),
                    new SqlParameter("@P_BRANCHNO",branchNo)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_SP_UPDATE_AFF_BRANCH", sqlParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.UpdateAffiliatedBranch() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        //Added by Nikhil Vinod Lambe on 26/05/2021 to add Define Intake Entry
        public int Add_Define_Intake_Entry(Affiliated aff, int createdBy, string ipAddress)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[12];
                objParams[0] = new SqlParameter("@P_COLLEGE_ID", aff.CollegeId);
                objParams[1] = new SqlParameter("@P_BRANCH_TYPE", aff.BranchType);
                objParams[2] = new SqlParameter("@P_LEVEL", aff.Level);
                objParams[3] = new SqlParameter("@P_AFF_DEGREE", aff.Degree);
                objParams[4] = new SqlParameter("@P_AFF_BRANCH", aff.Branch);
                objParams[5] = new SqlParameter("@P_INTAKE", aff.Intake);
                objParams[6] = new SqlParameter("@P_DURATION", aff.Duration);
                objParams[7] = new SqlParameter("@P_CREATED_BY", createdBy);
                objParams[8] = new SqlParameter("@P_IPADDRESS", ipAddress);
                objParams[9] = new SqlParameter("@P_ACTUAL_INTAKE", aff.ActualIntake);
                objParams[10] = new SqlParameter("@P_EXM_INTAKE", aff.ExmIntake);

                objParams[11] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[11].Direction = ParameterDirection.Output;

                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_INS_DEFINE_INTAKE", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
                if (obj.Equals(2627))
                {
                    status = Convert.ToInt32(CustomStatus.RecordExist);
                }
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.Add_Define_Intake_Entry --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }


        //Added by Nikhil Vinod Lambe on 27/05/2021 to get all enteries of intake
        public DataSet GetAllIntakeEnteries()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[0];
                ds = objSqlHelper.ExecuteDataSetSP("PKG_SP_GET_ALL_INTAKE_ENTRY", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetAllIntakeEnteries --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet RetrieveAffiliatedMasterData()
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_GET_AFFILIATED_MASTER_EXCEL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.RetrieveAffiliatedMasterData-> " + ex.ToString());
            }

            return ds;
        }
        public int SaveExcelSheetIntoDataBase_AffiliatedIntake(Affiliated aff, int duration, int createdBy, string ipAddress, int modifiedBy)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);

                SqlParameter[] objParams = null;
                objParams = new SqlParameter[13];
                objParams[0] = new SqlParameter("@P_COLLEGE_ID", aff.CollIDExc);
                objParams[1] = new SqlParameter("@P_BRANCH_TYPE", aff.BranchTypeExc);
                objParams[2] = new SqlParameter("@P_LEVEL", aff.LevelExc);
                objParams[3] = new SqlParameter("@P_DEGREE", aff.DegreeExc);
                objParams[4] = new SqlParameter("@P_BRANCH", aff.BranchExc);
                objParams[5] = new SqlParameter("@P_INTAKE", aff.Intake);
                objParams[6] = new SqlParameter("@P_DURATION", duration);
                objParams[7] = new SqlParameter("@P_CREATED_BY", createdBy);
                objParams[8] = new SqlParameter("@P_IPADDRESS", ipAddress);
                objParams[9] = new SqlParameter("@P_MODIFIED_BY", modifiedBy);
                objParams[10] = new SqlParameter("@P_ACTUAL_INTAKE", aff.ActualIntake);
                objParams[11] = new SqlParameter("@P_EXM_INTAKE", aff.ExmIntake);

                objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[12].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPLOAD_DEFINE_INTAKE_DATA_EXCEL", objParams, true);
                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-999")
                {
                    if (obj.ToString() == "1")
                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    else if (obj.ToString() == "-1001")
                        retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.Error);
            }

            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AffiliatedController.SaveExcelSheetIntoDataBase_AffiliatedIntake() -> " + ex.ToString());
            }
            return retStatus;
        }
        //Added by Nikhil Vinod Lambe on 11/06/2021 to get details of intake by intake Id
        public DataSet GetIntakeDetailsByIntakeId(int collegeID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_COLLEGEID", collegeID);
                ds = objSqlHelper.ExecuteDataSetSP("PKG_SP_GET_INTAKE_ENTERIES", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetIntakeDetailsByIntakeId --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet GetIntakeEnteriesByCollegeID(int CollegeID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_COLLEGE_ID", CollegeID);
                // objParams[1] = new SqlParameter("@P_ADMBATCH", AdmBatch);
                ds = objSqlHelper.ExecuteDataSetSP("PKG_SP_GET_INAKE_ENTERIES_BY_AFFILIATED_COLLEGE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetIntakeEnteriesByCollegeID --> " + ex.Message + " " + ex.StackTrace);

            }
            return ds;
        }

        //Added by Nikhil Vinod Lambe on 28/05/2021 to add Student Admitted
        public int Add_Student_Admitted(int College, int Admbatch, int Degree, int Branch, int BranchType, int Level, int Duration, int Intake, int StudAdmitted, int createdBy, string ipAddress)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[12];
                objParams[0] = new SqlParameter("@P_COLLEGE_ID", College);
                objParams[1] = new SqlParameter("@P_ADMBATCH", Admbatch);
                objParams[2] = new SqlParameter("@P_AFF_DEGREE", Degree);
                objParams[3] = new SqlParameter("@P_AFF_BRANCH", Branch);
                objParams[4] = new SqlParameter("@P_BRANCH_TYPE", BranchType);
                objParams[5] = new SqlParameter("@P_LEVEL", Level);
                objParams[6] = new SqlParameter("@P_DURATION", Duration);
                objParams[7] = new SqlParameter("@P_INTAKE", Intake);
                objParams[8] = new SqlParameter("@P_STUDENT_ADMITTED", StudAdmitted);
                objParams[9] = new SqlParameter("@P_CREATED_BY", createdBy);
                objParams[10] = new SqlParameter("@P_IPADDRESS", ipAddress);

                objParams[11] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[11].Direction = ParameterDirection.Output;
                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_INS_STUDENT_ADMITTED", objParams, true);
                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.Add_Student_Admitted --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
        //Added by Nikhil Vinod Lambe on 28/05/2021 to retrieve master data
        public DataSet RetrieveStudentAdmittedMasterData(int collegeID, int admBatch)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_COLLEGE_ID", collegeID);
                //objParams[1] = new SqlParameter("@P_ADMBATCH", admBatch);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_GET_STUDENT_ADMITTED_MASTER_EXCEL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.RetrieveStudentAdmittedMasterData-> " + ex.ToString());
            }

            return ds;
        }
        // Added by Nikhil V.Lambe on 29/05/2021 to upload excel data into database for student admitted.
        public int SaveExcelSheetIntoDataBase_StudentAdmitted(Affiliated aff, int Duration, int Admitted, int College, int Admbatch, int createdBy, string ipAddress, int modifiedBy)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);



                SqlParameter[] objParams = null;
                objParams = new SqlParameter[13];
                objParams[0] = new SqlParameter("@P_COLLEGE_ID", College);
                objParams[1] = new SqlParameter("@P_BRANCH_TYPE", aff.BranchTypeExc);
                objParams[2] = new SqlParameter("@P_LEVEL", aff.LevelExc);
                objParams[3] = new SqlParameter("@P_DEGREE", aff.DegreeExc);
                objParams[4] = new SqlParameter("@P_BRANCH", aff.BranchExc);
                objParams[5] = new SqlParameter("@P_INTAKE", aff.Intake);
                objParams[6] = new SqlParameter("@P_DURATION", Duration);
                objParams[7] = new SqlParameter("@P_ADMITTED", Admitted);
                objParams[8] = new SqlParameter("@P_ADMBATCH", Admbatch);
                objParams[9] = new SqlParameter("@P_CREATED_BY", createdBy);
                objParams[10] = new SqlParameter("@P_IPADDRESS", ipAddress);
                objParams[11] = new SqlParameter("@P_MODIFIED_BY", modifiedBy);
                objParams[12] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[12].Direction = ParameterDirection.Output;



                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPLOAD_STUDENT_ADMITTED_DATA_EXCEL", objParams, true);
                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-999")
                {
                    if (obj.ToString() == "1")
                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    else if (obj.ToString() == "-1001")
                        retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.Error);
            }



            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.AffiliatedController.SaveExcelSheetIntoDataBase_AffiliatedIntake() -> " + ex.ToString());
            }
            return retStatus;
        }
        //Added by Nikhil Vinod Lambe on 31/05/2021 to add affiliated standard fees
        public int Add_Affiliated_Standard_Fees(int feeCategory, int branchType, int level, string standardFees, string intakeFrom, string intakeTo, int collMode, int createdBy, string ipAddress, string initial)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[11];
                objParams[0] = new SqlParameter("@P_FEE_CATEGORY_NO", feeCategory);
                objParams[1] = new SqlParameter("@P_BRANCHTYPE", branchType);
                objParams[2] = new SqlParameter("@P_LEVEL", level);
                objParams[3] = new SqlParameter("@P_STANDARD_FEES", standardFees);
                objParams[4] = new SqlParameter("@P_INTAKE_FROM", intakeFrom);
                objParams[5] = new SqlParameter("@P_INTAKE_TO", intakeTo);
                objParams[6] = new SqlParameter("@P_COLL_MODE", collMode);
                objParams[7] = new SqlParameter("@P_CREATED_BY", createdBy);
                objParams[8] = new SqlParameter("@P_IPADDRESS", ipAddress);
                objParams[9] = new SqlParameter("@P_INITIAL", initial);
                objParams[10] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[10].Direction = ParameterDirection.Output;

                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_ADD_AFFILIATED_STANDARD_FEES", objParams, true);
                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
                if (obj.Equals(2627))
                {
                    status = Convert.ToInt32(CustomStatus.RecordExist);
                }
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.Add_Define_Intake_Entry --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
        //Added by Nikhil Vinod Lambe on 01/06/2021 to get the standard fee by fee category
        //Added by Nikhil Vinod Lambe on 01/06/2021 to get the standard fee by fee category
        public DataSet GetAffiliatedStandardFees(int feeCategory)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_FEE_CATEGORY", feeCategory);
                //objParams[1] = new SqlParameter("@P_ADMBATCH", admBatch);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_GET_STANDARD_FEE_BY_FEE_CATEGORY", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetAffiliatedStandardFees-> " + ex.ToString());
            }

            return ds;
        }

        // Added by Nikhil V. Lambe on 01/06/2021 to update affiliated standard fees
        public int Update_Affiliated_StandardFees(int standardFeeNo, int feeCategory, int level, int branchType, string standardFees, string intakeFrom, string intakeTo, int coll_Mode, int modifiedBy, string ipAddress)
        {
            int status;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[11];
                objParams[0] = new SqlParameter("@P_STANDARD_FEE_NO", standardFeeNo);
                objParams[1] = new SqlParameter("@P_FEE_CATEGORY", feeCategory);
                objParams[2] = new SqlParameter("@P_BRANCHTYPE", branchType);
                objParams[3] = new SqlParameter("@P_LEVEL", level);
                objParams[4] = new SqlParameter("@P_STANDARD_FEE", standardFees);
                objParams[5] = new SqlParameter("@P_INTAKE_FROM", intakeFrom);
                objParams[6] = new SqlParameter("@P_INTAKE_TO", intakeTo);
                objParams[7] = new SqlParameter("@P_COLL_MODE", coll_Mode);
                objParams[8] = new SqlParameter("@P_MODIFIED_BY", modifiedBy);
                objParams[9] = new SqlParameter("@P_IPADDRESS", ipAddress);
                objParams[10] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[10].Direction = ParameterDirection.Output;


                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_UPDATE_STANDARD_FEE", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.Update_Affiliated_StandardFees() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
        //Added by Nikhil Vinod Lambe on 02/06/2021 to get the standard fee for excel
        public DataSet GetAffiliatedStandardFees_Excel(int feeCategory)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_FEE_CATEGORY", feeCategory);
                //objParams[1] = new SqlParameter("@P_ADMBATCH", admBatch);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_GET_STANDARD_FEE_BY_FEE_CATEGORY_EXCEL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetAffiliatedStandardFees_Excel-> " + ex.ToString());
            }

            return ds;
        }
        //Added by Nikhil Vinod Lambe on 03/06/2021 to get the details for affiliated reconcile
        public DataSet GetDetails_Affiliated_Reconcile(int college, int admBatch, int feeCategory)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_COLLEGE_ID", college);
                objParams[1] = new SqlParameter("@P_ADMBATCH", admBatch);
                objParams[2] = new SqlParameter("@P_FEE_CATEGORY", feeCategory);

                //objParams[1] = new SqlParameter("@P_ADMBATCH", admBatch);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_GET_OFFLINE_PAYMENT_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetDetails_Affiliated_Reconcile-> " + ex.ToString());
            }

            return ds;
        }
        //Added by Nikhil Vinod Lambe on 02/06/2021 to add affiliated reconcile payment offline
        public int Add_Reconcile_Payment_Offline(int collegeID, int admBatch, int feeCategoryNo, int feeDCR, int recon)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_COLLEGE_ID", collegeID);
                objParams[1] = new SqlParameter("@P_ADMYEAR", admBatch);
                objParams[2] = new SqlParameter("@P_FEE_CATEGORY_NO", feeCategoryNo);
                objParams[3] = new SqlParameter("@P_FEE_DCR_NO", feeDCR);
                objParams[4] = new SqlParameter("@P_RECON", recon);
                objParams[5] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;
                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_INSERT_AFFILIATED_OFFLINE_PAYMENT", objParams, true);
                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
                if (obj.Equals(2627))
                {
                    status = Convert.ToInt32(CustomStatus.RecordExist);
                }
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.Add_Reconcile_Payment_Offline --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        //Added by Nikhil Vinod Lambe on 02/06/2021 to add affiliated fees for payment offline
        public int Add_Affiliated_Payment_Offline(int admYear, int feeCatNo, int collegeID, string collCode, string transactionID, string bankName, DateTime transactionDate, string receievedAmount, string fileUpload, int createdBy, string ipAddress, int feeDefine)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[13];
                objParams[0] = new SqlParameter("@P_ADMYEAR", admYear);
                objParams[1] = new SqlParameter("@P_FEE_CATEGORY_NO", feeCatNo);
                objParams[2] = new SqlParameter("@P_COLLEGE_ID", collegeID);
                objParams[3] = new SqlParameter("@P_COLLEGE_CODE", collCode);
                objParams[4] = new SqlParameter("@P_TRANSACTIONID", transactionID);
                objParams[5] = new SqlParameter("@P_BANK_NAME", bankName);
                objParams[6] = new SqlParameter("@P_TRANSACTION_DATE", transactionDate);
                objParams[7] = new SqlParameter("@P_AMOUNT", receievedAmount);
                objParams[8] = new SqlParameter("@P_FILE_UPLOAD", fileUpload);
                objParams[9] = new SqlParameter("@P_CREATED_BY", createdBy);
                objParams[10] = new SqlParameter("@P_IPADDRESS", ipAddress);
                objParams[11] = new SqlParameter("@P_FEE_DEFINE_NO", feeDefine);
                objParams[12] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[12].Direction = ParameterDirection.Output;

                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_ADD_AFFILIATED_PAYMENT_OFFLINE", objParams, true);
                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
                if (obj.Equals(2627))
                {
                    status = Convert.ToInt32(CustomStatus.RecordExist);
                }
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.Add_Define_Intake_Entry --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        //Added by Nikhil Vinod Lambe on 12/06/2021 to get details of receivable for saving data
        public DataSet GetDetailsOfReceivable_ToSave(int admBatch, int feeCategory, int collectionCategory)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_ADMBATCH", admBatch);
                objParams[1] = new SqlParameter("@P_FEE_CATEGORY_NO", feeCategory);
                objParams[2] = new SqlParameter("@P_COLL_CATEGORY", collectionCategory);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_SAVE_DETAILS_OF_RECEIVABLE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.AffiliatedController.GetDetailsOfReceivable()-> " + ex.ToString());
            }
            return ds;
        }
    }
}
