//======================================================================================
// PROJECT NAME  : UAIMS                                                                
// MODULE NAME   : ACADEMIC                                                             
// PAGE NAME     : Configuration page                                             
// CREATION DATE : 30-MARCH-2012                                                       
// CREATED BY    : ASHISH DHAKATE                                                 
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================
using System;
using System.Data;
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;

using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class ConfigController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public DataSet GetAllEvents()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_EVENT_GET_ALL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMS.IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.GetAllQualifyExam() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int AddConfig(Config objConfig)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_EVENTNO",objConfig.EventNo),
                    new SqlParameter("@P_STATUS", objConfig.Status),
                    new SqlParameter("@P_COLLEGE_CODE",objConfig.CollegeCode),
                    new SqlParameter("@P_CONFIGNO", status)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_CONFIG", sqlParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.AddBatch() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }


        /// <summary>
        /// Added By Dileep Kare -20-01-2020
        /// </summary>
        /// <param name="objCollege"></param>
        /// <returns></returns>
        public int AddNotice(College objCollege, string notice, int orgId)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_Notice", notice);
                objParams[1] = new SqlParameter("@P_COLLEGE_CODE", objCollege.CollegeCode);
                objParams[2] = new SqlParameter("@P_ORGANIZATION_ID", orgId);
                objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[3].Direction = ParameterDirection.Output;
                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_ADMNOTICE", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.AddNotice --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
        /// <summary>
        ///  Added By Dileep Kare -20-01-2020
        /// </summary>
        /// <param name="objCollege"></param>
        /// <returns></returns>
        public DataSet GetNoticeDetails(int NoticeID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_NoticeID", NoticeID);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_NOTICEDETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CollegeController.Getdetails() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Added By Dileep Kare -20-01-2020
        /// </summary>
        /// <param name="notice"></param>
        /// <param name="noticeid"></param>
        /// <returns></returns>
        public int UpdateNotice(string notice, int noticeid)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_Notice", notice);
                objParams[1] = new SqlParameter("@P_NoticeID", noticeid);
                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;

                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_ACAD_UPDATE_ADMNOTICE", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CollegeController.UpdateCollege --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
        /// <summary>
        /// Added By Dileep Kare -20-01-2020
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllNotices()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_ALLNOTICE", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CollegeController.Getdetails() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        // Added by Pritish on 31/12/2020
        /// <summary>
        /// Modified By Rishabh on 14/12/2021
        /// </summary>
        /// <param name="objConfig"></param>
        /// <returns></returns>
        public int AddLabelConfig(Config objConfig)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_RECNO", objConfig.RecId);
                objParams[1] = new SqlParameter("@P_LABELID", objConfig.LabelId);
                objParams[2] = new SqlParameter("@P_LABELNAME", objConfig.LabelName);
                objParams[3] = new SqlParameter("@P_COLLEGEID", objConfig.ColgId);
                objParams[4] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));

                objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_LABEL_CONFIG", objParams, true);
                status = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("RFC-CC.BusinessLogicLayer.BusinessLogic.Academic.ConfigController.AddLabelConfig() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        /// <summary>
        /// Added By Rishabh on 10/12/2021
        /// </summary>
        /// <param name="RecId"></param>
        /// <returns></returns>
        public DataSet GetLabelConfigList(int RecId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_RECNO", RecId);
                ds = objSQLHelper.ExecuteDataSetSP("SP_GET_LABELCONFIGLIST", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("RFC-CC.BusinessLogicLayer.BusinessLogic.Academic.ConfigController.GetLabelConfigList-> " + ex.ToString());
            }
            return ds;
        }
        //Added by Pritish S. on 15/12/2020
        /// <summary>
        /// Modified By Rishabh On 15/10/2021
        /// </summary>
        /// <param name="dtResult"></param>
        /// <returns></returns>
        public int AddCollegeSchemeConfig(DataTable dtResult)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@tblCollegeSchemeMap", dtResult);
                objParams[1] = new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;

                if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_COLLEGE_SCHEME_CONFIG", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.AddCollegeSchemeConfig -> " + ex.ToString());
            }
            return retStatus;
        }

        //ADDED BY PRANITA HIRADKAR ON 17/12/2021 FOR PAYMENT GATEWAY CONFIGURATION MASTER
        public int Addpayment_config(string Payment_name, int Activestatus)
        {
            int status = -99;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                             new SqlParameter("@P_PAY_GATEWAY_NAME",Payment_name),
                             new SqlParameter("@P_ACTIVE_STATUS", Activestatus),
                             new SqlParameter("@P_OUT", status)
               };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_PAYMENT_GATEWAY_MASTER", sqlParams, true);
                status = (Int32)obj;
            }
            catch (Exception ex)
            {
                status = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.Addpayment_config() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public int UpdatePayment_config(int payid, string payname, int Activestatus)
        {
            int status = -99;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                            {
                                new SqlParameter("@P_PAYID", payid),
                                new SqlParameter("@P_PAY_GATEWAY_NAME", payname),
                                new SqlParameter("@P_ACTIVE_STATUS",Activestatus), 
                                new SqlParameter("@P_OUT", status)
                            };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_PAYMENT_GATEWAY_MASTER", sqlParams, true);
                status = (Int32)obj;
            }
            catch (Exception ex)
            {
                status = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.UpdateNameOfActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetPaymentGateway_master()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_PAYMENT_GATEWAY_MASTER_DATA", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMS.IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.GetPaymentGateway_master() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public DataSet GetEditPaymentGateway_master(int payid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@P_PAYID", payid);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_EDIT_PAYMENT_GATEWAY_MASTER_DATA", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.GetEditPaymentGateway_master() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        // ENDED BY PRANITA HIRADKAR ON 17/12/2021 FOR PAYMENT GATEWAY CONFIGURATION MASTER



        //--------------------------------------------------------------------------------------------------------------------------
        // EXISTING UPDATED METHODS //  
        public int InsertPaymentgateway_config(int payid, string marchant_id, string accesscode, string checksumkey, string requesturl, string responseurl, int instance, int activestatus, int organi_id, int activity, string hashseq, string pageurl, int degreeNo, int collegeId)
        {
            int status = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[15];
                objParams[0] = new SqlParameter("@P_PAY_ID", payid);
                objParams[1] = new SqlParameter("@P_MERCHANT_ID", marchant_id);
                objParams[2] = new SqlParameter("@P_ACCESS_CODE", accesscode);
                objParams[3] = new SqlParameter("@P_CHECKSUM_KEY", checksumkey);
                objParams[4] = new SqlParameter("@P_REQUEST_URL", requesturl);
                objParams[5] = new SqlParameter("@P_RESPONSE_URL", responseurl);
                objParams[6] = new SqlParameter("@P_HASH_SEQ", hashseq);
                objParams[7] = new SqlParameter("@P_PAGE_URL", pageurl);
                objParams[8] = new SqlParameter("@P_INSTANCE", instance);
                objParams[9] = new SqlParameter("@P_ACTIVE_STATUS", activestatus);
                objParams[10] = new SqlParameter("@P_ORGANIZATION_ID", organi_id);
                objParams[11] = new SqlParameter("@P_ACTIVITY_NO", activity);
                objParams[12] = new SqlParameter("@P_DEGREENO", degreeNo);
                objParams[13] = new SqlParameter("@P_COLLEGE_ID", collegeId);
                objParams[14] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[14].Direction = System.Data.ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_PG_PAYMENT_GATEWAY_CONFIG", objParams, true);

                if (obj != null && obj.ToString() == "1")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else if (obj != null && obj.ToString() == "-99")
                    status = Convert.ToInt32(CustomStatus.RecordExist);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32((CustomStatus.Error));
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.InsertPaymentgateway_config-> " + ex.ToString());
            }
            return status;
        }

        public int UpdateadmFeeDeduction(int configid, int payid, string marchant_id, string accesscode, string checksumkey, string requesturl, string responseurl, int instance, int activestatus, int organi_id, int activity, string hashseq, string pageurl, int degreeNo, int collegeId)
        {
            int status = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[16];
                objParams[0] = new SqlParameter("@P_CONFIG_ID", configid);
                objParams[1] = new SqlParameter("@P_PAY_ID", payid);
                objParams[2] = new SqlParameter("@P_MERCHANT_ID", marchant_id);
                objParams[3] = new SqlParameter("@P_ACCESS_CODE", accesscode);
                objParams[4] = new SqlParameter("@P_CHECKSUM_KEY", checksumkey);
                objParams[5] = new SqlParameter("@P_REQUEST_URL", requesturl);
                objParams[6] = new SqlParameter("@P_RESPONSE_URL", responseurl);
                objParams[7] = new SqlParameter("@P_HASH_SEQ", hashseq);
                objParams[8] = new SqlParameter("@P_PAGE_URL", pageurl);
                objParams[9] = new SqlParameter("@P_INSTANCE", instance);
                objParams[10] = new SqlParameter("@P_ACTIVE_STATUS", activestatus);
                objParams[11] = new SqlParameter("@P_ORGANIZATION_ID", organi_id);
                objParams[12] = new SqlParameter("@P_ACTIVITY_NO", activity);
                objParams[13] = new SqlParameter("@P_DEGREENO", degreeNo);
                objParams[14] = new SqlParameter("@P_COLLEGE_ID", collegeId);
                objParams[15] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[15].Direction = System.Data.ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_PG_PAYMENT_GATEWAY_CONFIG", objParams, true);

                if (obj != null && obj.ToString() == "2")
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
                else if (obj != null && obj.ToString() == "-99")
                    status = Convert.ToInt32(CustomStatus.RecordExist);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.UpdateadmFeeDeduction() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }




        public DataSet GetData_Payment_gateway_config(int configid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper ObjSqlHelper = new SQLHelper(connectionString);
                SqlParameter[] Sqlparam = new SqlParameter[1];
                Sqlparam[0] = new SqlParameter("@P_CONFIG_ID", configid);
                ds = ObjSqlHelper.ExecuteDataSetSP("PKG_EDIT_PG_PAYMENT_GATEWAY_CONFIG", Sqlparam);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ConfigController.GetData_Payment_gateway_config() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetData_Payment_gateway_config()
        {
            DataSet ds = null;
            try
            {
                SQLHelper ObjSqlHelper = new SQLHelper(connectionString);
                SqlParameter[] Sqlparam = new SqlParameter[0];
                ds = ObjSqlHelper.ExecuteDataSetSP("PKG_GETDATA_PG_PAYMENT_GATEWAY_CONFIG", Sqlparam);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ConfigController.GetData_Payment_gateway_config() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        //ENDED BY PRANITA HIRADKAR ON 17/12/2021 FOR PAYMANET GATEWAY CONFIGURATION

        /// Added by Nikhil L. on 13-09-2022 to bind college dropdown by OrgId.
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public DataSet Bind_College_By_OrgId(int orgId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper ObjSqlHelper = new SQLHelper(connectionString);
                SqlParameter[] Sqlparam = new SqlParameter[1];
                Sqlparam[0] = new SqlParameter("@P_OrganizationId", orgId);
                ds = ObjSqlHelper.ExecuteDataSetSP("PKG_ACD_BIND_COLLEGE_BY_ORGID", Sqlparam);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ConfigController.Bind_College_By_OrgId() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        /// <summary>
        /// Added by Nikhil L. on 23/11/2022 to edit,update and insert the state and district data.
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="districtNo"></param>
        /// <param name="district"></param>
        /// <param name="stateNo"></param>
        /// <param name="college_Code"></param>
        /// <param name="Activestatus"></param>
        /// <returns></returns>
        public int State_District_Mapping(string mode, int districtNo, string district, int stateNo, string college_Code, bool Activestatus)
        {
            int status = -99;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                            {
                                new SqlParameter("@P_MODE", mode),
                                new SqlParameter("@P_DISTRICTNO", districtNo),
                                new SqlParameter("@P_DISTRICT",district), 
                                new SqlParameter("@P_STATENO",stateNo), 
                                new SqlParameter("@P_COLLEGE_CODE",college_Code), 
                                new SqlParameter("@P_ACTIVESTATUS",Activestatus), 
                                new SqlParameter("@P_OUTPUT", status)
                            };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_CRUD_STATE_DISTRICT", sqlParams, true);
                status = (Int32)obj;
            }
            catch (Exception ex)
            {
                status = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.State_District_Mapping() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        //ADDED BY AASHNA 15-11-2022
        public DataSet GetCourseConConf(int schemeno, int sessionno, string semesternos)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_SCHEMENO", schemeno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[2] = new SqlParameter("@P_SEMESTERNOS", semesternos);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_COURSE_SP_GET_CONDOLANCE_CONFIGURATION_DATA", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentController.GetCourseOffered-> " + ex.ToString());
            }
            return ds;
        }
        //added by aashna 15-11-2022
        public int AddCondolanceConfi(int cond_no, string opra_FROM, string opra_TO, int range_from, int range_to, int ched, int fees_type, int college_id, string sessiono
            , int semestreno, string fees, int organization_id, int ua_no, string ipadress, string courseno, string coursename, int COSCHNO, int col_id)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[19];
                objParams[0] = new SqlParameter("@P_CONDOLANCE_NO", cond_no);
                objParams[1] = new SqlParameter("@P_OPERATOR_FROM", opra_FROM);
                objParams[2] = new SqlParameter("@P_RANGE_FROM", range_from);
                objParams[3] = new SqlParameter("@P_OPERATOR_TO", opra_TO);
                objParams[4] = new SqlParameter("@P_RANGE_TO", range_to);
                objParams[5] = new SqlParameter("@P_CHEKED_FEES", ched);
                objParams[6] = new SqlParameter("@P_FEES_TYPE", fees_type);
                objParams[7] = new SqlParameter("@P_COLLEGE_ID", college_id);
                objParams[8] = new SqlParameter("@P_SESSIONNO", sessiono);
                objParams[9] = new SqlParameter("@P_SEMESTERNO", semestreno);
                objParams[10] = new SqlParameter("@P_FEES_DEFINE", fees);
                objParams[11] = new SqlParameter("@P_ORGANIZATION_ID", organization_id);
                objParams[12] = new SqlParameter("@P_UA_NO", ua_no);
                objParams[13] = new SqlParameter("@P_IP_ADDRESS", ipadress);
                objParams[14] = new SqlParameter("@P_COURSE_NO", courseno);
                objParams[15] = new SqlParameter("@P_COURSE_NAME", coursename);
                objParams[16] = new SqlParameter("@P_COSCHNO", COSCHNO);
                objParams[17] = new SqlParameter("@P_CONDOLANCE_ID", col_id);
                objParams[18] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[18].Direction = ParameterDirection.Output;
                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_CONDOLANCE_CONFIGURATION_DATA", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.AddNotice --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
        //added by aashna 15-11-2022
        public int updateCondolanceConfi(int cond_id, string opra_FROM, string opra_TO, int range_from, int range_to, int college_id, string sessiono
             , int semestreno, string fees, int organization_id, int COSCHNO)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[12];
                objParams[0] = new SqlParameter("@P_CONDOLANCE_ID", cond_id);
                objParams[1] = new SqlParameter("@P_OPERATOR_FROM", opra_FROM);
                objParams[2] = new SqlParameter("@P_RANGE_FROM", range_from);
                objParams[3] = new SqlParameter("@P_OPERATOR_TO", opra_TO);
                objParams[4] = new SqlParameter("@P_RANGE_TO", range_to);
                objParams[5] = new SqlParameter("@P_COLLEGE_ID", college_id);
                objParams[6] = new SqlParameter("@P_SESSIONNO", sessiono);
                objParams[7] = new SqlParameter("@P_SEMESTERNO", semestreno);
                objParams[8] = new SqlParameter("@P_FEES_DEFINE", fees);
                objParams[9] = new SqlParameter("@P_ORGANIZATION_ID", organization_id);
                objParams[10] = new SqlParameter("@P_COSCHNO", COSCHNO);
                objParams[11] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[11].Direction = ParameterDirection.Output;
                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_CONDOLANCE_CONFIGURATION_DATA", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.AddNotice --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
        //Added By AASHNA 17-11-2022
        public DataSet GetDetailsOfAttendanceByIdnoForoCondolance(int idno, int sessionno, int collegeid, int semesterno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[2] = new SqlParameter("@P_COLLEGE_ID", collegeid);
                objParams[3] = new SqlParameter("@P_SEMESTERNO", semesterno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_GET_ATTENDANCE_DETAILS_BY_IDNO_FOR_CONDOLANCE", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetDetailsOfAttendanceByIdno-> " + ex.ToString());
            }
            return ds;
        }


        //ADDED BY AASHNA  18-11-2022
        public int InsertStudentCondoApply(int idno, int sessonno, int semesterno, string courseno, string coursename, string attendance, string range
            , string shortage, string docname, string reason, int ua_no, string apaddrees, int org_id, string STUDFEES)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[15];
                objParams[0] = new SqlParameter("@P_IDNO", idno);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessonno);
                objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[3] = new SqlParameter("@P_COURSENO", courseno);
                objParams[4] = new SqlParameter("@P_COURSENAME", coursename);
                objParams[5] = new SqlParameter("@P_ATTENDANCE", attendance);
                objParams[6] = new SqlParameter("@P_RANGE", range);
                objParams[7] = new SqlParameter("@P_SHORTAGE", shortage);
                objParams[8] = new SqlParameter("@P_DOCNAME", docname);
                objParams[9] = new SqlParameter("@P_REASON", reason);
                objParams[10] = new SqlParameter("@P_UA_NO", ua_no);
                objParams[11] = new SqlParameter("@P_IPADDRESS", apaddrees);
                objParams[12] = new SqlParameter("@P_ORGANIZATION_ID", org_id);
                objParams[13] = new SqlParameter("@P_STUDFEES", STUDFEES);
                objParams[14] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[14].Direction = ParameterDirection.Output;
                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_ACD_INSERT_STUDENT_APPLY_CONDOLANCE", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.AddNotice --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        //added by aashna 15-11-2022
        public int AddedCondonation(string college, int ua_type, int ua_no, string idno, int Approved, string semesterno, string courseno, int sessionno, string reason)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_COLLEGE_NO", college);
                objParams[1] = new SqlParameter("@P_UA_TYPE", ua_type);
                objParams[2] = new SqlParameter("@P_UANO", ua_no);
                objParams[3] = new SqlParameter("@P_IDNO", idno);
                objParams[4] = new SqlParameter("@P_STATUS", Approved);
                objParams[5] = new SqlParameter("@P_COURSENO", courseno);
                objParams[6] = new SqlParameter("@P_SEMESTERNO", semesterno);
                objParams[7] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[8] = new SqlParameter("@P_REASON", reason);
                objParams[9] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;
                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_ACD_UPDATE_CONDONATION_APPROVAL_STATUS", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.AddNotice --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetIdData(int ID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_ID", ID);
                //ds =obj("PKG_Get_INTERMEDIATE_GRADE_CONFIGURATION", objParams);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_Get_INTERMEDIATE_GRADE_CONFIGURATION_New", objParams);




            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.configController.GetSingleSession-> " + ex.ToString());
            }
            return ds;
        }


        public DataSet GetAllSession()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];
                ds = objSQLHelper.ExecuteDataSetSP("PKG_SEL_INTERMEDIATE_GRADE_CONFIGURATION", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetAllSession-> " + ex.ToString());
            }
            return ds;
        }
        public int Updateinetrmediate(int id, string sessionid, int course, int collegeid, int uano, int schemano, int orgid, int mode)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_ID", id);
                objParams[1] = new SqlParameter("@P_SESSIONNO", sessionid);
                objParams[2] = new SqlParameter("@P_COURSENO", course);
                objParams[3] = new SqlParameter("@P_COLLEGEID", collegeid);
                objParams[4] = new SqlParameter("@P_UA_NO", uano);
                objParams[5] = new SqlParameter("@P_SCHEMA", schemano);
                objParams[6] = new SqlParameter("@P_ORGID ", orgid);
                objParams[7] = new SqlParameter("@mode", mode);
                objParams[8] = new SqlParameter("@P_out", SqlDbType.Int);
                objParams[8].Direction = ParameterDirection.Output;
                object ret = (objSqlHelper.ExecuteNonQuerySP("PKG_UPD_INTERMEDIATE_GRADE_CONFIGURATION", objParams, true));
                if (ret.ToString() == "1" && ret != null)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else if (ret.ToString() == "-99")
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.AddNotice --> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }

        public int Insert_Intermediate_Grade_Configuration_Details(string GradeRealse, string SubExamNo, int Sessionno, int courseno, string MarkOutOf, int UaNo, int schemeno, int OrgId, int collegeid, string gradename)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[11];
                objParams[0] = new SqlParameter("@P_UA_NO", UaNo);
                objParams[1] = new SqlParameter("@P_SESSIONNO", Sessionno);
                objParams[2] = new SqlParameter("@P_COURSENO", courseno);

                objParams[3] = new SqlParameter("@P_SCHEMANO", schemeno);
                objParams[4] = new SqlParameter("@P_GRADEREALSE", GradeRealse);
                objParams[5] = new SqlParameter("@P_SUBMENUEXAM", SubExamNo);
                objParams[6] = new SqlParameter("@P_OUTOFMARK", MarkOutOf);
                objParams[7] = new SqlParameter("@P_ORGID", OrgId);
                objParams[8] = new SqlParameter("@P_COLLEGEID", collegeid);
                objParams[9] = new SqlParameter("@P_GRADE_COLUMN_NAME", gradename);
                objParams[10] = new SqlParameter("@P_out", SqlDbType.Int);
                objParams[10].Direction = ParameterDirection.Output;
                object ret = (objSqlHelper.ExecuteNonQuerySP("PKG_INS_INTERMEDIATE_GRADE_CONFIGURATION", objParams, true));
                if (ret.ToString() == "1" && ret != null)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (ret.ToString() == "-99")
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.AddNotice --> " + ex.Message + " " + ex.StackTrace);
            }
            return retStatus;
        }

        //Added by Tejaswini Dhoble[26-12-22]

        public DataSet GetRetAll_CreateEmailTemplate(int TEMPLATEID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_CTEMP_ID", TEMPLATEID);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_RET_ALL_CREATEEMAILTEMPLATE", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("RAJAGIRI.BusinessLayer.BusinessLogic.ConfigController.GetAll_EmailTemplate-> " + ex.ToString());
            }
            return ds;
        }
        //******************************************************************************************************************************************************************************
        //Added by Tejaswini Dhoble[26-12-22]
        public int InsertCreateEmailTemplate(int id, string tempName, string subject, int UserType, int PageForTemp, int tempType, int DataField, string TempBody, int status, int mode)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@P_CREATETEMP_ID",id),
                            new SqlParameter("@P_TEMP_NAME",tempName),
                            new SqlParameter("@P_TEMP_SUBJECT",subject),
                            new SqlParameter("@P_USERTYPEID",UserType),
                            new SqlParameter("@P_AL_No",PageForTemp),
                            new SqlParameter("@P_TEMPTYPEID",tempType),
                            new SqlParameter("@P_TEMPDATAFIELDCONFIGID",DataField),
                            new SqlParameter("@P_TEMP_BODY",TempBody),
                            new SqlParameter("@P_ACTIVESTATUS",status),
                            new SqlParameter("@P_MODE",mode),
                            new SqlParameter("@P_OUT", SqlDbType.Int),
                        
                          };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("ACD_INSERTUPDATE_CREATEEMAIL_TEMP", sqlParams, true);

                if (Convert.ToInt32(ret) == -99)
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else if (Convert.ToInt32(ret) == 2627)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ConfigController.InsertReasonForLeaving-> " + ex.ToString());
            }
            return retStatus;
        }

        //************************************************************************************************************************************************************
        // ADDED BY Tejaswini Dhoble[26-12-2022] 
        public int UpdateCreateEmailTemplate(int id, string tempName, string subject, int UserType, int PageForTemp, int tempType, int DataField, string TempBody, int status, int mode)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                              new SqlParameter("@P_CREATETEMP_ID",id),
                            new SqlParameter("@P_TEMP_NAME",tempName),
                            new SqlParameter("@P_TEMP_SUBJECT",subject),
                            new SqlParameter("@P_USERTYPEID",UserType),
                            new SqlParameter("@P_AL_No",PageForTemp),
                            new SqlParameter("@P_TEMPTYPEID",tempType),
                            new SqlParameter("@P_TEMPDATAFIELDCONFIGID",DataField),
                            new SqlParameter("@P_TEMP_BODY",TempBody),
                            new SqlParameter("@P_ACTIVESTATUS",status),
                            new SqlParameter("@P_MODE",mode),
                            new SqlParameter("@P_OUT", SqlDbType.Int),
                          };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("ACD_INSERTUPDATE_CREATEEMAIL_TEMP", sqlParams, true);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ConfigController.UpdateCreateEmailTemplate-> " + ex.ToString());
            }
            return retStatus;
        }
        //Added by Nikhil L. on 24-04-2023 for email,sms, whatsapp configuration.
        public int AddServiceProvider(int Configno, String ConfigName, string ServiceProviderName, int ServiceNo, string Parameter,// int priority, 
            int active_Status)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_CONFIGNO", Configno);
                objParams[1] = new SqlParameter("@P_CONFIGNAME", ConfigName);
                objParams[2] = new SqlParameter("@P_SERVICEPROVIDERNAME", ServiceProviderName);
                objParams[3] = new SqlParameter("@P_SERVICENO", ServiceNo);
                objParams[4] = new SqlParameter("@P_PARAMETER", Parameter);
                //Priority flag committed as we don't want right now,now we are checking on basis of active status.
                //objParams[5] = new SqlParameter("@P_PRIORITY", priority);/*Added by Nikhil L. on 20/06/2023 to set priority flag.*/
                objParams[5] = new SqlParameter("@P_ACTIVE_STATUS", active_Status);/*Added by Nikhil L. on 20/06/2023 to set active/inactive flag.*/
                objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[6].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_SERVICE_PROVIDER_NAME", objParams, true);

                if (Convert.ToInt32(ret) == 1)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
            }
            return retStatus;
        }
        //Added by Nikhil L. on 24-04-2023 for email,sms, whatsapp configuration.
        public int AddEmailServiceProvider(int EMAIL_NO, int ServiceNo, string SMTP_Server, string SMTP_Server_Port, string CKey_UserId, string Email_ID, string Password, int Active, int UaNo, string sendGridAPI_Key)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[11];

                objParams[0] = new SqlParameter("@P_EMIAL_NO", EMAIL_NO);
                objParams[1] = new SqlParameter("@P_SERVICE_NO", ServiceNo);
                objParams[2] = new SqlParameter("@P_SMTP_SERVER", SMTP_Server);
                objParams[3] = new SqlParameter("@P_SMTP_PORT", SMTP_Server_Port);
                objParams[4] = new SqlParameter("@P_CKEY_USERID", CKey_UserId);
                objParams[5] = new SqlParameter("@P_EMAILID", Email_ID);
                objParams[6] = new SqlParameter("@P_PASSWORDS", Password);
                objParams[7] = new SqlParameter("@P_STATUS", Active);
                objParams[8] = new SqlParameter("@P_UANO", UaNo);
                objParams[9] = new SqlParameter("@P_SENDGRID_API_KEY", sendGridAPI_Key);
                objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[10].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_EMAIL_SERVICE_PROVIDER_CONFIGURATION", objParams, true);

                if (Convert.ToInt32(ret) == 1)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
            }
            return retStatus;

        }
        //Added by Nikhil L. on 24-04-2023 for email,sms, whatsapp configuration.
        // Update By Jay Takalkhede on 07/08/2023 
        public int AddSMSServiceProvider(int SMS_NO, int ServiceNo, string SMSAPI, string SMSUserID, string EmailSMS, string PasswordSMS, string SMSParameterI, string SMSParameterII, int Active, int UaNo
             , string SMSProvider, string SMSTempID, string SMSTemplate, string SMSTemplateName, string SMSUrl)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[16];

                objParams[0] = new SqlParameter("@P_SMS_NO", SMS_NO);
                objParams[1] = new SqlParameter("@P_SERVICE_NO", ServiceNo);
                objParams[2] = new SqlParameter("@P_SMSAPI", SMSAPI);
                objParams[3] = new SqlParameter("@P_SMSUSERID", SMSUserID);
                objParams[4] = new SqlParameter("@P_EMAILSMS", EmailSMS);
                objParams[5] = new SqlParameter("@P_PASSWORDSMS", PasswordSMS);
                objParams[6] = new SqlParameter("@P_SMSPARAMETERI", SMSParameterI);
                objParams[7] = new SqlParameter("@P_SMSPARAMETERII", SMSParameterII);
                objParams[8] = new SqlParameter("@P_STATUS", Active);
                objParams[9] = new SqlParameter("@P_UANO", UaNo);
                objParams[10] = new SqlParameter("@P_SMSPROVIDER", SMSProvider);
                objParams[11] = new SqlParameter("@P_TEMPID", SMSTempID);
                objParams[12] = new SqlParameter("@P_TEMPLATE", SMSTemplate);
                objParams[13] = new SqlParameter("@P_TEMPLATE_NAME", SMSTemplateName);
                objParams[14] = new SqlParameter("@P_SMS_URL", SMSUrl);
                objParams[15] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[15].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_SMS_SERVICE_PROVIDER_CONFIGURATION", objParams, true);

                if (Convert.ToInt32(ret) == 1)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
            }
            return retStatus;
        }
        //Added by Nikhil L. on 24-04-2023 for email,sms, whatsapp configuration.
        public int AddWhatsappServiceProvider(int WhatsAAP_NO, int ServiceNo, string API_URL, string Token, string MobileNo, string Account_SID, string UserName, int Active, int UaNo, string API_Key)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[11];
                objParams[0] = new SqlParameter("@P_WHATSAAP_NO", WhatsAAP_NO);
                objParams[1] = new SqlParameter("@P_SERVICE_NO", ServiceNo);
                objParams[2] = new SqlParameter("@P_API_URL", API_URL);
                objParams[3] = new SqlParameter("@P_TOKEN", Token);
                objParams[4] = new SqlParameter("@P_MOBILENO", MobileNo);
                objParams[5] = new SqlParameter("@P_USERNAME", UserName);
                objParams[6] = new SqlParameter("@P_ACCOUNT_SID", Account_SID);
                objParams[7] = new SqlParameter("@P_STATUS", Active);
                objParams[8] = new SqlParameter("@P_UANO", UaNo);
                objParams[9] = new SqlParameter("@P_API_KEY", API_Key);
                objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[10].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_WHATSAAP_SERVICE_PROVIDER_CONFIGURATION", objParams, true);

                if (Convert.ToInt32(ret) == 1)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (Convert.ToInt32(ret) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseController.UpdateOfferedCourse -> " + ex.ToString());
            }
            return retStatus;

        }

        //Added by Nikhil L. on 24-04-2023 for email,sms, whatsapp configuration.
        public DataSet GetEmailTamplateandUserDetails(string Emailid, string Mobileno, string username, int pageno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_EMAILID", Emailid);
                objParams[1] = new SqlParameter("@P_MOBILENO", Mobileno);
                objParams[2] = new SqlParameter("@P_USERNAME", username);
                objParams[3] = new SqlParameter("@P_PAGENO", pageno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_EMAIL_AND_USER_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic-> " + ex.ToString());
            }
            return ds;
        }

        //Added By Aniket P. On dated 15/06/2023
        public DataSet BindAndroidMenuConfig()
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                ds = objSQLHelper.ExecuteDataSet("PKG_ACAD_TBL_ANDROID_MENU_CONFIG");
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.BindAndroidMenuConfig() --> " + ex.Message + " " + ex.ToString());
            }
            return ds;
        }
        //Added By Aniket P. On dated 15/06/2023
        public int AndroidMenuConfigUpdateCheckStatus(string IsOn, string MenuId, int uano, string Ip)
        {
            int status = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper ObjSQLHElper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_MENUID", MenuId);
                objParams[1] = new SqlParameter("@P_STATUS", IsOn);
                objParams[2] = new SqlParameter("@P_UANO", uano);
                objParams[3] = new SqlParameter("@P_IPADDRESS", Ip);
                objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[4].Direction = ParameterDirection.Output;
                object obj = ObjSQLHElper.ExecuteNonQuerySP("PKG_ACAD_TBL_ANDROID_MENU_CONFIG_LOGS", objParams, true);
                if (obj.ToString() == "1")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                {
                    status = Convert.ToInt32(CustomStatus.Error);
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.AndroidMenuConfigUpdateCheckStatus() --> " + ex.Message + " " + ex.ToString());
            }
            return status;
        }


        public int UpdateDataFiledEmailTemplate(int ID, int TemplateType, string TemplatName, string DisplayDatafname, string Datafieldname, int mode)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {

                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@ID",ID),
                            new SqlParameter("@TEMPLATETYPEID",TemplateType),
                            new SqlParameter("@TEMPLATENAME",TemplatName),
                            new SqlParameter("@DISPLAYFIELDNAME",DisplayDatafname),
                            new SqlParameter("@DATAFIELDNAME",Datafieldname),  
                            new SqlParameter("@P_MODE",mode),
                            new SqlParameter("@P_OUT", SqlDbType.Int),
                          };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("ACD_INSERTUPDATE_DATAFIELDEMAIL_TEMP", sqlParams, true);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ConfigController.UpdateCreateEmailTemplate-> " + ex.ToString());
            }
            return retStatus;
        }

        public int InsertDataFiledEmailTemplate(int ID, int TemplateType, string TemplatName, string DisplayDatafname, string Datafieldname, int mode)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                        { 
                            new SqlParameter("@ID",ID),
                            new SqlParameter("@TEMPLATETYPEID",TemplateType),
                            new SqlParameter("@TEMPLATENAME",TemplatName),
                            new SqlParameter("@DISPLAYFIELDNAME",DisplayDatafname),
                            new SqlParameter("@DATAFIELDNAME",Datafieldname),  
                            new SqlParameter("@P_MODE",mode),
                            new SqlParameter("@P_OUT", SqlDbType.Int),
                        
                          };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSQLHelper.ExecuteNonQuerySP("ACD_INSERTUPDATE_DATAFIELDEMAIL_TEMP", sqlParams, true);

                if (Convert.ToInt32(ret) == -99)
                {
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                }
                else if (Convert.ToInt32(ret) == 2627)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                }
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ConfigController.InsertReasonForLeaving-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetRetAll_DatafieldsDetails(int TEMPLATEID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@TEMPDATAFIELDCONFIGID", TEMPLATEID);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_RET_ALL_CREATEDATAFIELDEMAILTEMPLATE", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("RAJAGIRI.BusinessLayer.BusinessLogic.ConfigController.GetAll_EmailTemplate-> " + ex.ToString());
            }
            return ds;
        }


        public DataSet Bind_DatafieldsDetails(int TEMPLATEID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@TEMPDATAFIELDCONFIGID", TEMPLATEID);

                ds = objSQLHelper.ExecuteDataSetSP("BIND_CREATEDATAFIELDEMAILTEMPLATE", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("RAJAGIRI.BusinessLayer.BusinessLogic.ConfigController.GetAll_EmailTemplate-> " + ex.ToString());
            }
            return ds;
        }



        // Commented BySaurabh sir on Deployment Machine As per discussion with Gopal M. Sir.
        // New Method V2 Added by -Gopal M

        //public int InsertPaymentgateway_configV2(int payid, string marchant_id, string accesscode, string checksumkey, string requesturl, string responseurl, int instance, int activestatus, int organi_id, int activity, string hashseq, string pageurl, int degreeNo, int collegeId, string submarchant_id, string feetype)
        //{
        //    int status = Convert.ToInt32(CustomStatus.Others);
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
        //        SqlParameter[] objParams = null;

        //        objParams = new SqlParameter[17];
        //        objParams[0] = new SqlParameter("@P_PAY_ID", payid);
        //        objParams[1] = new SqlParameter("@P_MERCHANT_ID", marchant_id);
        //        objParams[2] = new SqlParameter("@P_ACCESS_CODE", accesscode);
        //        objParams[3] = new SqlParameter("@P_CHECKSUM_KEY", checksumkey);
        //        objParams[4] = new SqlParameter("@P_REQUEST_URL", requesturl);
        //        objParams[5] = new SqlParameter("@P_RESPONSE_URL", responseurl);
        //        objParams[6] = new SqlParameter("@P_HASH_SEQ", hashseq);
        //        objParams[7] = new SqlParameter("@P_PAGE_URL", pageurl);
        //        objParams[8] = new SqlParameter("@P_INSTANCE", instance);
        //        objParams[9] = new SqlParameter("@P_ACTIVE_STATUS", activestatus);
        //        objParams[10] = new SqlParameter("@P_ORGANIZATION_ID", organi_id);
        //        objParams[11] = new SqlParameter("@P_ACTIVITY_NO", activity);
        //        objParams[12] = new SqlParameter("@P_DEGREENO", degreeNo);
        //        objParams[13] = new SqlParameter("@P_COLLEGE_ID", collegeId);
        //        objParams[14] = new SqlParameter("@P_SUBMERCHANT_ID", submarchant_id);
        //        objParams[15] = new SqlParameter("@P_BANKFEE_TYPE", feetype);
        //        objParams[16] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
        //        objParams[16].Direction = System.Data.ParameterDirection.Output;

        //        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_PG_PAYMENT_GATEWAY_CONFIG_V2", objParams, true);

        //        if (obj != null && obj.ToString() == "1")
        //            status = Convert.ToInt32(CustomStatus.RecordSaved);
        //        else if (obj != null && obj.ToString() == "-99")
        //            status = Convert.ToInt32(CustomStatus.RecordExist);
        //        else
        //            status = Convert.ToInt32(CustomStatus.Error);
        //    }
        //    catch (Exception ex)
        //    {
        //        status = Convert.ToInt32((CustomStatus.Error));
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.InsertPaymentgateway_configV2-> " + ex.ToString());
        //    }
        //    return status;
        //}

        //public int Addpayment_configV2(string Payment_name, int Activestatus)
        //{
        //    int status = -99;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[]
        //        {
        //                     new SqlParameter("@P_PAY_GATEWAY_NAME",Payment_name),
        //                     new SqlParameter("@P_ACTIVE_STATUS", Activestatus),
        //                     new SqlParameter("@P_OUT", status)
        //       };
        //        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

        //        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_PAYMENT_GATEWAY_MASTER_V2", sqlParams, true);
        //        status = (Int32)obj;
        //    }
        //    catch (Exception ex)
        //    {
        //        status = -99;
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.Addpayment_configV2() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return status;
        //}

        //public int UpdatePayment_configV2(int payid, string payname, int Activestatus)
        //{
        //    int status = -99;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[]
        //                    {
        //                        new SqlParameter("@P_PAYID", payid),
        //                        new SqlParameter("@P_PAY_GATEWAY_NAME", payname),
        //                        new SqlParameter("@P_ACTIVE_STATUS",Activestatus), 
        //                        new SqlParameter("@P_OUT", status)
        //                    };
        //        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

        //        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_PAYMENT_GATEWAY_MASTER_V2", sqlParams, true);
        //        status = (Int32)obj;
        //    }
        //    catch (Exception ex)
        //    {
        //        status = -99;
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.UpdateNameOfActivity() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return status;
        //}

        //public int UpdateadmFeeDeductionV2(int configid, int payid, string marchant_id, string accesscode, string checksumkey, string requesturl, string responseurl, int instance, int activestatus, int organi_id, int activity, string hashseq, string pageurl, int degreeNo, int collegeId, string submarchant_id, string feetype)
        //{
        //    int status = Convert.ToInt32(CustomStatus.Others);
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
        //        SqlParameter[] objParams = null;

        //        objParams = new SqlParameter[19];
        //        objParams[0] = new SqlParameter("@P_CONFIG_ID", configid);
        //        objParams[1] = new SqlParameter("@P_PAY_ID", payid);
        //        objParams[2] = new SqlParameter("@P_MERCHANT_ID", marchant_id);
        //        objParams[3] = new SqlParameter("@P_ACCESS_CODE", accesscode);
        //        objParams[4] = new SqlParameter("@P_CHECKSUM_KEY", checksumkey);
        //        objParams[5] = new SqlParameter("@P_REQUEST_URL", requesturl);
        //        objParams[6] = new SqlParameter("@P_RESPONSE_URL", responseurl);
        //        objParams[7] = new SqlParameter("@P_HASH_SEQ", hashseq);
        //        objParams[8] = new SqlParameter("@P_PAGE_URL", pageurl);
        //        objParams[9] = new SqlParameter("@P_INSTANCE", instance);
        //        objParams[10] = new SqlParameter("@P_ACTIVE_STATUS", activestatus);
        //        objParams[11] = new SqlParameter("@P_ORGANIZATION_ID", organi_id);
        //        objParams[12] = new SqlParameter("@P_ACTIVITY_NO", activity);
        //        objParams[13] = new SqlParameter("@P_DEGREENO", degreeNo);
        //        objParams[14] = new SqlParameter("@P_COLLEGE_ID", collegeId);
        //        objParams[15] = new SqlParameter("@P_SUBMERCHANT_ID", submarchant_id);
        //        objParams[16] = new SqlParameter("@P_BANKFEE_TYPE", feetype);
        //        objParams[17] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
        //        objParams[18].Direction = System.Data.ParameterDirection.Output;

        //        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_PG_PAYMENT_GATEWAY_CONFIG_V2", objParams, true);

        //        if (obj != null && obj.ToString() == "2")
        //            status = Convert.ToInt32(CustomStatus.RecordUpdated);
        //        else if (obj != null && obj.ToString() == "-99")
        //            status = Convert.ToInt32(CustomStatus.RecordExist);
        //        else
        //            status = Convert.ToInt32(CustomStatus.Error);
        //    }
        //    catch (Exception ex)
        //    {
        //        status = -99;
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.UpdateadmFeeDeduction() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return status;
        //}

        //public DataSet GetPaymentGateway_masterV2()
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
        //        SqlParameter[] objParams = new SqlParameter[0];
        //        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_PAYMENT_GATEWAY_MASTER_DATA_V2", objParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMS.IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.GetPaymentGateway_master() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}

        //public DataSet GetData_Payment_gateway_configV2()
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper ObjSqlHelper = new SQLHelper(connectionString);
        //        SqlParameter[] Sqlparam = new SqlParameter[0];
        //        ds = ObjSqlHelper.ExecuteDataSetSP("PKG_GETDATA_PG_PAYMENT_GATEWAY_CONFIG_V2", Sqlparam);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ConfigController.GetData_Payment_gateway_config() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}

        //public DataSet GetEditPaymentGateway_masterV2(int payid)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(connectionString);

        //        SqlParameter[] sqlParams = new SqlParameter[1];
        //        sqlParams[0] = new SqlParameter("@P_PAYID", payid);
        //        ds = objSQLHelper.ExecuteDataSetSP("PKG_EDIT_PAYMENT_GATEWAY_MASTER_DATA_V2", sqlParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.GetEditPaymentGateway_masterV2() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}

        //public DataSet GetData_Payment_gateway_configV2(int configid)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper ObjSqlHelper = new SQLHelper(connectionString);
        //        SqlParameter[] Sqlparam = new SqlParameter[1];
        //        Sqlparam[0] = new SqlParameter("@P_CONFIG_ID", configid);
        //        ds = ObjSqlHelper.ExecuteDataSetSP("PKG_EDIT_PG_PAYMENT_GATEWAY_CONFIG_V2", Sqlparam);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ConfigController.GetData_Payment_gateway_config() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}

        //public int InsertPaymentGatewayMappingV2(int payid, string reciepttype, int activity, int payment_config_Id, int collegeId, int degreeNo, int branchNo, int semesterNo, bool active_status, int organi_id, int created_by)
        //{
        //    int status = Convert.ToInt32(CustomStatus.Others);
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
        //        SqlParameter[] objParams = null;

        //        objParams = new SqlParameter[12];
        //        objParams[0] = new SqlParameter("@P_PAY_ID", payid);
        //        objParams[1] = new SqlParameter("@P_RECIEPT_TYPE", reciepttype);
        //        objParams[2] = new SqlParameter("@P_ACTIVITY_NO", activity);
        //        objParams[3] = new SqlParameter("@P_PAYMENT_CONFIG_ID", payment_config_Id);
        //        objParams[4] = new SqlParameter("@P_COLLEGE_ID", collegeId);
        //        objParams[5] = new SqlParameter("@P_DEGREENO", degreeNo);
        //        objParams[6] = new SqlParameter("@P_BRANCHNO", branchNo);
        //        objParams[7] = new SqlParameter("@P_SEMESTERNO", semesterNo);
        //        objParams[8] = new SqlParameter("@P_ACTIVE_STATUS", active_status);
        //        objParams[9] = new SqlParameter("@P_ORGANIZATION_ID", organi_id);
        //        objParams[10] = new SqlParameter("@P_CREATED_BY", created_by);
        //        objParams[11] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
        //        objParams[11].Direction = System.Data.ParameterDirection.Output;

        //        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_PG_INSERTPAYMENT_GATEWAY_MAPPING_V2", objParams, true);

        //        if (obj != null && obj.ToString() == "1")
        //            status = Convert.ToInt32(CustomStatus.RecordSaved);
        //        else if (obj != null && obj.ToString() == "-99")
        //            status = Convert.ToInt32(CustomStatus.RecordExist);
        //        else
        //            status = Convert.ToInt32(CustomStatus.Error);
        //    }
        //    catch (Exception ex)
        //    {
        //        status = Convert.ToInt32((CustomStatus.Error));
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.InsertPaymentGatewayMappingV2-> " + ex.ToString());
        //    }
        //    return status;
        //}

        //public int UpdatePaymentGatewayMappingV2(int pgmid, int payid, string reciepttype, int activity, int payment_config_Id, int collegeId, int degreeNo, int branchNo, int semesterNo, bool active_status, int organi_id, int created_by)
        //{
        //    int status = Convert.ToInt32(CustomStatus.Others);
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
        //        SqlParameter[] objParams = null;

        //        objParams = new SqlParameter[13];
        //        objParams[0] = new SqlParameter("@P_PGM_ID", pgmid);
        //        objParams[1] = new SqlParameter("@P_PAY_ID", payid);
        //        objParams[2] = new SqlParameter("@P_RECIEPT_TYPE", reciepttype);
        //        objParams[3] = new SqlParameter("@P_ACTIVITY_NO", activity);
        //        objParams[4] = new SqlParameter("@P_PAYMENT_CONFIG_ID", payment_config_Id);
        //        objParams[5] = new SqlParameter("@P_COLLEGE_ID", collegeId);
        //        objParams[6] = new SqlParameter("@P_DEGREENO", degreeNo);
        //        objParams[7] = new SqlParameter("@P_BRANCHNO", branchNo);
        //        objParams[8] = new SqlParameter("@P_SEMESTERNO", semesterNo);
        //        objParams[9] = new SqlParameter("@P_ACTIVE_STATUS", active_status);
        //        objParams[10] = new SqlParameter("@P_ORGANIZATION_ID", organi_id);
        //        objParams[11] = new SqlParameter("@P_CREATED_BY", created_by);
        //        objParams[12] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
        //        objParams[12].Direction = System.Data.ParameterDirection.Output;

        //        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_PG_UPDATEPAYMENT_GATEWAY_MAPPING_V2", objParams, true);

        //        if (obj != null && obj.ToString() == "2")
        //            status = Convert.ToInt32(CustomStatus.RecordUpdated);
        //        else if (obj != null && obj.ToString() == "-99")
        //            status = Convert.ToInt32(CustomStatus.RecordExist);
        //        else
        //            status = Convert.ToInt32(CustomStatus.Error);
        //    }
        //    catch (Exception ex)
        //    {
        //        status = Convert.ToInt32((CustomStatus.Error));
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.InsertPaymentGatewayMappingV2-> " + ex.ToString());
        //    }
        //    return status;
        //}

        //public DataSet GetEdit_PaymentGatewayMappingV2(int paygateway_mappid)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper ObjSqlHelper = new SQLHelper(connectionString);
        //        SqlParameter[] Sqlparam = new SqlParameter[1];
        //        Sqlparam[0] = new SqlParameter("@P_PGM_ID", paygateway_mappid);
        //        ds = ObjSqlHelper.ExecuteDataSetSP("PKG_EDIT_PG_PAYMENT_GATEWAY_MAPPING_V2", Sqlparam);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ConfigController.GetData_PaymentGatewayMappingV2() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}

        //public DataSet GetData_PaymentGatewayMappingV2()
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper ObjSqlHelper = new SQLHelper(connectionString);
        //        SqlParameter[] Sqlparam = new SqlParameter[0];
        //        ds = ObjSqlHelper.ExecuteDataSetSP("PKG_GETDATA_PG_PAYMENT_GATEWAY_MAPPING_V2", Sqlparam);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ConfigController.GetData_Payment_gateway_config() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}


        // // Commented BySaurabh sir on Deployment Machine As per discussion with Gopal M. Sir.



        public int AddInsertUpdateAppDetails(string APPID, string SERVER_KEY, string APP_APIURL, int id)
        {
            int status = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper ObjSQLHElper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_FIREBASE_APP_ID", APPID);
                objParams[1] = new SqlParameter("@P_FIREBASE_SERVER_KEY", SERVER_KEY);
                objParams[2] = new SqlParameter("@P_APP_API_URL", APP_APIURL);
                objParams[3] = new SqlParameter("@P_UANO", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                objParams[4] = new SqlParameter("@P_IPADDRESS", System.Web.HttpContext.Current.Session["ipAddress"].ToString());
                objParams[5] = new SqlParameter("@P_ORGID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                objParams[6] = new SqlParameter("@P_ID", id);
                objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[7].Direction = ParameterDirection.Output;
                object obj = ObjSQLHElper.ExecuteNonQuerySP("PKG_ACD_INSERT_UPDATE_APP_DETAILS", objParams, true);
                if (obj.ToString() == "1")
                {
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                if (obj.ToString() == "2")
                {
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else
                {
                    status = Convert.ToInt32(CustomStatus.Error);
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.AndroidMenuConfigUpdateCheckStatus() --> " + ex.Message + " " + ex.ToString());
            }
            return status;
        }


        public DataSet GetDataOfApiDetails()
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                ds = objSQLHelper.ExecuteDataSet("PKG_ACD_GET_DATA_APP_DETAILS");
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.GetDataOfApiDetails() --> " + ex.Message + " " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetDataOfApiDetailsEdit(int id)
        {
            DataSet ds = null;

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_ID", id);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_DATA_APP_DETAILS_EDIT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.GetDataOfApiDetailsEdit() --> " + ex.Message + " " + ex.ToString());
            }
            return ds;
        }




        //IOBPay Payment Gateway Method V2 Added by -Gopal M 30/08/2023

        public int InsertPaymentgateway_configV2(int payid, string marchant_id, string accesscode, string checksumkey, string requesturl, string responseurl, int instance, int activestatus, int organi_id, int activity, string hashseq, string pageurl, int degreeNo, int collegeId, string submarchant_id, string feetype)
        {
            int status = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[17];
                objParams[0] = new SqlParameter("@P_PAY_ID", payid);
                objParams[1] = new SqlParameter("@P_MERCHANT_ID", marchant_id);
                objParams[2] = new SqlParameter("@P_ACCESS_CODE", accesscode);
                objParams[3] = new SqlParameter("@P_CHECKSUM_KEY", checksumkey);
                objParams[4] = new SqlParameter("@P_REQUEST_URL", requesturl);
                objParams[5] = new SqlParameter("@P_RESPONSE_URL", responseurl);
                objParams[6] = new SqlParameter("@P_HASH_SEQ", hashseq);
                objParams[7] = new SqlParameter("@P_PAGE_URL", pageurl);
                objParams[8] = new SqlParameter("@P_INSTANCE", instance);
                objParams[9] = new SqlParameter("@P_ACTIVE_STATUS", activestatus);
                objParams[10] = new SqlParameter("@P_ORGANIZATION_ID", organi_id);
                objParams[11] = new SqlParameter("@P_ACTIVITY_NO", activity);
                objParams[12] = new SqlParameter("@P_DEGREENO", degreeNo);
                objParams[13] = new SqlParameter("@P_COLLEGE_ID", collegeId);
                objParams[14] = new SqlParameter("@P_SUBMERCHANT_ID", submarchant_id);
                objParams[15] = new SqlParameter("@P_BANKFEE_TYPE", feetype);
                objParams[16] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[16].Direction = System.Data.ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_PG_PAYMENT_GATEWAY_CONFIG_V2", objParams, true);

                if (obj != null && obj.ToString() == "1")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else if (obj != null && obj.ToString() == "-99")
                    status = Convert.ToInt32(CustomStatus.RecordExist);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32((CustomStatus.Error));
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.InsertPaymentgateway_configV2-> " + ex.ToString());
            }
            return status;
        }


        
        public int Addpayment_configV2(string Payment_name, int Activestatus)
        {
            int status = -99;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                             new SqlParameter("@P_PAY_GATEWAY_NAME",Payment_name),
                             new SqlParameter("@P_ACTIVE_STATUS", Activestatus),
                             new SqlParameter("@P_OUT", status)
               };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_INSERT_PAYMENT_GATEWAY_MASTER_V2", sqlParams, true);
                status = (Int32)obj;
            }
            catch (Exception ex)
            {
                status = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.Addpayment_configV2() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public int UpdatePayment_configV2(int payid, string payname, int Activestatus)
        {
            int status = -99;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                            {
                                new SqlParameter("@P_PAYID", payid),
                                new SqlParameter("@P_PAY_GATEWAY_NAME", payname),
                                new SqlParameter("@P_ACTIVE_STATUS",Activestatus), 
                                new SqlParameter("@P_OUT", status)
                            };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_PAYMENT_GATEWAY_MASTER_V2", sqlParams, true);
                status = (Int32)obj;
            }
            catch (Exception ex)
            {
                status = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.UpdateNameOfActivity() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public int UpdateadmFeeDeductionV2(int configid, int payid, string marchant_id, string accesscode, string checksumkey, string requesturl, string responseurl, int instance, int activestatus, int organi_id, int activity, string hashseq, string pageurl, int degreeNo, int collegeId, string submarchant_id, string feetype)
        {
            int status = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[18];
                objParams[0] = new SqlParameter("@P_CONFIG_ID", configid);
                objParams[1] = new SqlParameter("@P_PAY_ID", payid);
                objParams[2] = new SqlParameter("@P_MERCHANT_ID", marchant_id);
                objParams[3] = new SqlParameter("@P_ACCESS_CODE", accesscode);
                objParams[4] = new SqlParameter("@P_CHECKSUM_KEY", checksumkey);
                objParams[5] = new SqlParameter("@P_REQUEST_URL", requesturl);
                objParams[6] = new SqlParameter("@P_RESPONSE_URL", responseurl);
                objParams[7] = new SqlParameter("@P_HASH_SEQ", hashseq);
                objParams[8] = new SqlParameter("@P_PAGE_URL", pageurl);
                objParams[9] = new SqlParameter("@P_INSTANCE", instance);
                objParams[10] = new SqlParameter("@P_ACTIVE_STATUS", activestatus);
                objParams[11] = new SqlParameter("@P_ORGANIZATION_ID", organi_id);
                objParams[12] = new SqlParameter("@P_ACTIVITY_NO", activity);
                objParams[13] = new SqlParameter("@P_DEGREENO", degreeNo);
                objParams[14] = new SqlParameter("@P_COLLEGE_ID", collegeId);
                objParams[15] = new SqlParameter("@P_SUBMERCHANT_ID", submarchant_id);
                objParams[16] = new SqlParameter("@P_BANKFEE_TYPE", feetype);
                objParams[17] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[17].Direction = System.Data.ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_PG_PAYMENT_GATEWAY_CONFIG_V2", objParams, true);

                if (obj != null && obj.ToString() == "2")
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
                else if (obj != null && obj.ToString() == "-99")
                    status = Convert.ToInt32(CustomStatus.RecordExist);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = -99;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.UpdateadmFeeDeduction() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetPaymentGateway_masterV2()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_PAYMENT_GATEWAY_MASTER_DATA_V2", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMS.IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.GetPaymentGateway_master() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetData_Payment_gateway_configV2()
        {
            DataSet ds = null;
            try
            {
                SQLHelper ObjSqlHelper = new SQLHelper(connectionString);
                SqlParameter[] Sqlparam = new SqlParameter[0];
                ds = ObjSqlHelper.ExecuteDataSetSP("PKG_GETDATA_PG_PAYMENT_GATEWAY_CONFIG_V2", Sqlparam);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ConfigController.GetData_Payment_gateway_config() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetEditPaymentGateway_masterV2(int payid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = new SqlParameter("@P_PAYID", payid);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_EDIT_PAYMENT_GATEWAY_MASTER_DATA_V2", sqlParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.GetEditPaymentGateway_masterV2() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetData_Payment_gateway_configV2(int configid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper ObjSqlHelper = new SQLHelper(connectionString);
                SqlParameter[] Sqlparam = new SqlParameter[1];
                Sqlparam[0] = new SqlParameter("@P_CONFIG_ID", configid);
                ds = ObjSqlHelper.ExecuteDataSetSP("PKG_EDIT_PG_PAYMENT_GATEWAY_CONFIG_V2", Sqlparam);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ConfigController.GetData_Payment_gateway_config() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public int InsertPaymentGatewayMappingV2(int payid, string reciepttype, int activity, int payment_config_Id, int collegeId, int degreeNo, int branchNo, int semesterNo, bool active_status, int organi_id, int created_by)
        {
            int status = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[12];
                objParams[0] = new SqlParameter("@P_PAY_ID", payid);
                objParams[1] = new SqlParameter("@P_RECIEPT_TYPE", reciepttype);
                objParams[2] = new SqlParameter("@P_ACTIVITY_NO", activity);
                objParams[3] = new SqlParameter("@P_PAYMENT_CONFIG_ID", payment_config_Id);
                objParams[4] = new SqlParameter("@P_COLLEGE_ID", collegeId);
                objParams[5] = new SqlParameter("@P_DEGREENO", degreeNo);
                objParams[6] = new SqlParameter("@P_BRANCHNO", branchNo);
                objParams[7] = new SqlParameter("@P_SEMESTERNO", semesterNo);
                objParams[8] = new SqlParameter("@P_ACTIVE_STATUS", active_status);
                objParams[9] = new SqlParameter("@P_ORGANIZATION_ID", organi_id);
                objParams[10] = new SqlParameter("@P_CREATED_BY", created_by);
                objParams[11] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[11].Direction = System.Data.ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_PG_INSERTPAYMENT_GATEWAY_MAPPING_V2", objParams, true);

                if (obj != null && obj.ToString() == "1")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else if (obj != null && obj.ToString() == "-99")
                    status = Convert.ToInt32(CustomStatus.RecordExist);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32((CustomStatus.Error));
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.InsertPaymentGatewayMappingV2-> " + ex.ToString());
            }
            return status;
        }

        public int UpdatePaymentGatewayMappingV2(int pgmid, int payid, string reciepttype, int activity, int payment_config_Id, int collegeId, int degreeNo, int branchNo, int semesterNo, bool active_status, int organi_id, int created_by)
        {
            int status = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[13];
                objParams[0] = new SqlParameter("@P_PGM_ID", pgmid);
                objParams[1] = new SqlParameter("@P_PAY_ID", payid);
                objParams[2] = new SqlParameter("@P_RECIEPT_TYPE", reciepttype);
                objParams[3] = new SqlParameter("@P_ACTIVITY_NO", activity);
                objParams[4] = new SqlParameter("@P_PAYMENT_CONFIG_ID", payment_config_Id);
                objParams[5] = new SqlParameter("@P_COLLEGE_ID", collegeId);
                objParams[6] = new SqlParameter("@P_DEGREENO", degreeNo);
                objParams[7] = new SqlParameter("@P_BRANCHNO", branchNo);
                objParams[8] = new SqlParameter("@P_SEMESTERNO", semesterNo);
                objParams[9] = new SqlParameter("@P_ACTIVE_STATUS", active_status);
                objParams[10] = new SqlParameter("@P_ORGANIZATION_ID", organi_id);
                objParams[11] = new SqlParameter("@P_CREATED_BY", created_by);
                objParams[12] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[12].Direction = System.Data.ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_PG_UPDATEPAYMENT_GATEWAY_MAPPING_V2", objParams, true);

                if (obj != null && obj.ToString() == "2")
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
                else if (obj != null && obj.ToString() == "-99")
                    status = Convert.ToInt32(CustomStatus.RecordExist);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32((CustomStatus.Error));
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ConfigController.InsertPaymentGatewayMappingV2-> " + ex.ToString());
            }
            return status;
        }

        public DataSet GetEdit_PaymentGatewayMappingV2(int paygateway_mappid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper ObjSqlHelper = new SQLHelper(connectionString);
                SqlParameter[] Sqlparam = new SqlParameter[1];
                Sqlparam[0] = new SqlParameter("@P_PGM_ID", paygateway_mappid);
                ds = ObjSqlHelper.ExecuteDataSetSP("PKG_EDIT_PG_PAYMENT_GATEWAY_MAPPING_V2", Sqlparam);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ConfigController.GetData_PaymentGatewayMappingV2() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetData_PaymentGatewayMappingV2()
        {
            DataSet ds = null;
            try
            {
                SQLHelper ObjSqlHelper = new SQLHelper(connectionString);
                SqlParameter[] Sqlparam = new SqlParameter[0];
                ds = ObjSqlHelper.ExecuteDataSetSP("PKG_GETDATA_PG_PAYMENT_GATEWAY_MAPPING_V2", Sqlparam);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessEntities.ConfigController.GetData_Payment_gateway_config() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }



    }
}
