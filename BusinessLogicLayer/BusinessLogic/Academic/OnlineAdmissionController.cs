//Added by Pooja SAndel on 04/02/2022
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
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
namespace IITMS.UAIMS.BusinessLogicLayer.BusinessLogic
{

    public class OnlineAdmissionController
    {
        string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddOAdm(OnlineAdmission OAdm)
        {
            int retstatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objparams = null;
                objparams = new SqlParameter[7];
                objparams[0] = new SqlParameter("@P_DEGREE_TYPE", OAdm.DEGREE_TYPE);
                objparams[1] = new SqlParameter("@P_SUBDEGREE_TYPE", OAdm.SUBDEGREE_TYPE);
                objparams[2] = new SqlParameter("@P_DEGREE", OAdm.DEGREE);
                objparams[3] = new SqlParameter("@P_DEGREE_CODE", OAdm.DEGREE_CODE);
                objparams[4] = new SqlParameter("@P_CREATED_BY", OAdm.CREATED_BY);
                //objparams[7] = new SqlParameter("CREATED_DATE", OBJAAP.CREATED_DATE);
                objparams[5] = new SqlParameter("@P_IP_ADDRESS", OAdm.IP_ADDRESS);
                objparams[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objparams[6].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_DEGREE_CODE_MAPPING_INSERT", objparams, true);

                if (ret.ToString() == "1")
                    retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else if (ret.ToString() == "0")
                    retstatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                else
                    retstatus = Convert.ToInt32(CustomStatus.Error);


            }
            catch (Exception ex)
            {
                retstatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OnlineAdmissionController.AddOAdm->" + ex.ToString());
            }
            return retstatus;
        }




        public int UpdateOAdm(OnlineAdmission OAdm)
        {
            int retstatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objparams = null;
                objparams = new SqlParameter[7];
                objparams[0] = new SqlParameter("@P_DEGREE_NO", OAdm.DEGREE_NO);
                objparams[1] = new SqlParameter("@P_DEGREE_TYPE", OAdm.DEGREE_TYPE);
                objparams[2] = new SqlParameter("@P_SUBDEGREE_TYPE", OAdm.SUBDEGREE_TYPE);
                objparams[3] = new SqlParameter("@P_DEGREE", OAdm.DEGREE);
                objparams[4] = new SqlParameter("@P_DEGREE_CODE", OAdm.DEGREE_CODE);
                objparams[5] = new SqlParameter("@P_CREATED_BY", OAdm.CREATED_BY);
                //objparams[8] = new SqlParameter("CREATED_DATE", OBJAAP.CREATED_DATE);
                objparams[6] = new SqlParameter("@P_IP_ADDRESS", OAdm.IP_ADDRESS);

                if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_DEGREE_CODE_MAPPING_UPDATE", objparams, true) != null)
                    retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retstatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OnlineAdmissionController.UpdateOAdm->" + ex.ToString());
            }
            return retstatus;
        }


        public DataSet GetAllAdmission()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objparams = new SqlParameter[0];
                //objparams[0] = new SqlParameter("@P_DEGREE_NO", DEGREE_NO);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_DEGREE_CODE_MAPPING_GET_BYALL", objparams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OnlineAdmissionController.GetAllAdmission->" + ex.ToString());
            }
            finally
            {
                ds.Dispose();
            }
            return ds;
        }

        public DataSet GetSingleOAdm(int DEGREE_NO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objparams = null;
                objparams = new SqlParameter[1];
                objparams[0] = new SqlParameter("@P_DEGREE_NO", DEGREE_NO);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_DEGREE_CODE_MAPPING_GET_BY_NO", objparams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OnlineAdmissionController.GetSingleOAdm->" + ex.ToString());
            }
            finally
            {
                ds.Dispose();
            }
            return ds;
        }
        //Degree_Specialization_Mapping
        public int AddSpecialization(OnlineAdmission OAdm, int activeStatus)
        {
            int retstatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objparams = null;
                objparams = new SqlParameter[6];

                objparams[0] = new SqlParameter("@P_DEGREE", OAdm.DEGREE);
                objparams[1] = new SqlParameter("@P_SPECIALIZATION", OAdm.SPECIALIZATION);
                objparams[2] = new SqlParameter("@P_CREATED_BY", OAdm.CREATED_BY);
                //objparams[7] = new SqlParameter("CREATED_DATE", OBJAAP.CREATED_DATE);
                objparams[3] = new SqlParameter("@P_IP_ADDRESS", OAdm.IP_ADDRESS);
                objparams[4] = new SqlParameter("@P_ACTIVE_STATUS", activeStatus);
                objparams[5] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objparams[5].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_DEGREE_SPECIALIZATION_MAPPING_INSERT", objparams, true);

                if (ret.ToString() == "1")
                    retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else if (ret.ToString() == "0")
                    retstatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                else
                    retstatus = Convert.ToInt32(CustomStatus.Error);


            }
            catch (Exception ex)
            {
                retstatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OnlineAdmissionController.AddSpecialization->" + ex.ToString());
            }
            return retstatus;
        }
        public int UpdateSpecialization(OnlineAdmission OAdm, int activeStatus)
        {
            int retstatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objparams = null;
                objparams = new SqlParameter[6];
                objparams[0] = new SqlParameter("@P_DEGREE_NO", OAdm.DEGREE_NO);
                objparams[1] = new SqlParameter("@P_DEGREE", OAdm.DEGREE);
                objparams[2] = new SqlParameter("@P_SPECIALIZATION", OAdm.SPECIALIZATION);
                objparams[3] = new SqlParameter("@P_CREATED_BY", OAdm.CREATED_BY);
                objparams[4] = new SqlParameter("@P_IP_ADDRESS", OAdm.IP_ADDRESS);
                objparams[5] = new SqlParameter("@P_ACTIVE_STATUS", activeStatus);

                if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_DEGREE_SPECIALIZATION_MAPPING_UPDATE", objparams, true) != null)
                    retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retstatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OnlineAdmissionController.UpdateSpecialization->" + ex.ToString());
            }
            return retstatus;
        }
        public DataSet GetAllDegree()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objparams = new SqlParameter[0];
                //objparams[0] = new SqlParameter("@P_DEGREE_NO", DEGREE_NO);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_DEGREE_SPECIALIZATION_MAPPING_GET_BYALL", objparams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OnlineAdmissionController.GetAllDegree->" + ex.ToString());
            }
            finally
            {
                ds.Dispose();
            }
            return ds;
        }
        public DataSet GetSingleSpecialization(int specNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objparams = null;
                objparams = new SqlParameter[1];
                objparams[0] = new SqlParameter("@P_SPEC_NO", specNo);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_DEGREE_SPECIALIZATION_MAPPING_GET_BY_NO", objparams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OnlineAdmissionController.GetSingleSpecialization->" + ex.ToString());
            }
            finally
            {
                ds.Dispose();
            }
            return ds;
        }
        /// <summary>
        /// Added by Nikhil V. Lambe on 28/02/2022 to bind degree document mapping list and to edit the record.
        /// </summary>
        /// <param name="docNo"></param>
        /// <returns></returns>
        public DataSet GetDegreeDoc_AndByDocNo(int docNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objparams = null;
                objparams = new SqlParameter[1];
                objparams[0] = new SqlParameter("@P_DOC_NO", docNo);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_DEGREE_DOC_AND_BY_DOCNO", objparams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OnlineAdmissionController.GetSingleSpecialization->" + ex.ToString());
            }
            finally
            {
                ds.Dispose();
            }
            return ds;
        }
        /// <summary>
        /// Added by Nikhil V. Lambe on 28/02/2022 to add degree document mapping.
        /// </summary>
        /// <param name="degreeNo"></param>
        /// <param name="docType"></param>
        /// <param name="createdBy"></param>
        /// <param name="createdIP"></param>
        /// <param name="activeStatus"></param>
        /// <returns></returns>
        public int AddDegreeDoc(int degreeNo, string docType, int createdBy, string createdIP, int activeStatus, int mandatory)
        {
            int retstatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objparams = null;
                objparams = new SqlParameter[7];

                objparams[0] = new SqlParameter("@P_DEGREENO", degreeNo);
                objparams[1] = new SqlParameter("@P_DOC_TYPE", docType);
                objparams[2] = new SqlParameter("@P_CREATED_BY", createdBy);
                objparams[3] = new SqlParameter("@P_CREATED_IP", createdIP);
                objparams[4] = new SqlParameter("@P_ACTIVE_STATUS", activeStatus);
                objparams[5] = new SqlParameter("@P_IS_MANDATORY", mandatory);      //Added by Nikhil L. on 16/03/2022
                objparams[6] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objparams[6].Direction = ParameterDirection.Output;
                object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ADD_DEGREE_DOC_MAPPING", objparams, true);

                if (ret.ToString() == "1")
                    retstatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else if (ret.ToString() == "0")
                    retstatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                else
                    retstatus = Convert.ToInt32(CustomStatus.Error);


            }
            catch (Exception ex)
            {
                retstatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OnlineAdmissionController.AddSpecialization->" + ex.ToString());
            }
            return retstatus;
        }
        /// <summary>
        /// Added by Nikhil V. Lambe on 28/02/2022 to update degree document mapping.
        /// </summary>
        /// <param name="docNo"></param>
        /// <param name="degreeNo"></param>
        /// <param name="docType"></param>
        /// <param name="modifiedBy"></param>
        /// <param name="modified_IP"></param>
        /// <param name="activeStatus"></param>
        /// <returns></returns>
        public int UpdateDegreeDoc(int docNo, int degreeNo, string docType, int modifiedBy, string modified_IP, int activeStatus, int mandatory)
        {
            int retstatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objparams = null;
                objparams = new SqlParameter[8];
                objparams[0] = new SqlParameter("@P_DOC_NO", docNo);
                objparams[1] = new SqlParameter("@P_DEGREENO", degreeNo);
                objparams[2] = new SqlParameter("@P_DOC_TYPE", docType);
                objparams[3] = new SqlParameter("@P_MODIFY_BY", modifiedBy);
                objparams[4] = new SqlParameter("@P_MODIFY_IP", modified_IP);
                objparams[5] = new SqlParameter("@P_ACTIVE_STATUS", activeStatus);
                objparams[6] = new SqlParameter("@P_IS_MANDATORY", mandatory);
                objparams[7] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objparams[7].Direction = ParameterDirection.Output;

                if (objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_DEGREE_DOC_MAPPING", objparams, true) != null)
                    retstatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retstatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OnlineAdmissionController.UpdateDegreeDoc->" + ex.ToString());
            }
            return retstatus;
        }

        public DataSet GetConfirmStatus_FromUsername(string userName)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
               {
                    new SqlParameter("@P_USERNAME",userName)
               };
                ds = objSQLHelper.ExecuteDataSetSP("PKG_CHECK_USER_FOR_UNLOCK_OA", sqlParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OnlineAdmissionController.GetSingleSpecialization->" + ex.ToString());
            }
            return ds;
        }

        /// </summary>
        /// <param name="admbatchno"></param>
        /// <param name="degreeNo"></param>
        /// <param name="complete"></param>
        /// <param name="programmeType"></param>
        /// <returns></returns>
        public DataSet GetStudentsForEmailSMS(int admbatchno, int degreeNo, int complete, int programmeType)
        {
            DataSet ds = new DataSet();
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_ADMBATCH", admbatchno);
                objParams[1] = new SqlParameter("@P_DEGREE", degreeNo);
                objParams[2] = new SqlParameter("@P_COMPLETE", complete);
                objParams[3] = new SqlParameter("@P_PROGRAMME_TYPE", programmeType);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_OA_STUDENTS_FOR_EMAIL_SMS", objParams);


            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OnlineAdmissionController.GetStudentsForEmailSMS() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Added By Nikhil to add qualifying degree.
        /// </summary>
        /// <param name="degreeNo"></param>
        /// <param name="qualDegreeNo"></param>
        /// <param name="activeStatus"></param>
        /// <param name="OrgId"></param>
        /// <param name="created_IP"></param>
        /// <param name="created_UANO"></param>
        /// <returns></returns>
        public int Add_Qualify_Degree(int degreeNo,int branchNo, int qualdegreeNo,int qualbranchNo, int activeStatus, int OrgId, string created_IP, int created_UANO)
        {
            //DataSet ds = null;
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
               {
                    new SqlParameter("@P_DEGREENO",degreeNo) ,
                    new SqlParameter("@P_BRANCHNO",branchNo),
                    new SqlParameter("@P_QUAL_DEGREENO",qualdegreeNo) ,
                    new SqlParameter("@P_QUAL_BRANCHNO",qualbranchNo),
                    new SqlParameter("@P_ACTIVE_STATUS",activeStatus) ,
                    new SqlParameter("@P_ORGANIZATION_ID",OrgId) ,
                    new SqlParameter("@P_CREATED_IP",created_IP) ,
                    new SqlParameter("@P_CREATED_UA_NO",created_UANO) ,
                    //new SqlParameter("@P_MODIFIED_IP",modify_IP) ,
                    //new SqlParameter("@P_MODIFIED_UA_NO",modify_UANO) ,
                    new SqlParameter("@P_OUTPUT",SqlDbType.Int) 
               };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ADD_UPDATE_QUAL_DEGREE_MAPPING", sqlParams, true);
                if (obj.Equals(1))
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (obj.Equals(2))
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OnlineAdmissionController.Add_Update_Qualify_Degree->" + ex.ToString());
            }
            return retStatus;
        }
        /// <summary>
        /// Added by Nikhil L. to get qualify degree details
        /// </summary>
        /// <param name="qual_Degree"></param>
        /// <returns></returns>
        public DataSet GetQualDegreeDetails(int qual_Degree)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objparams = null;
                objparams = new SqlParameter[1];
                objparams[0] = new SqlParameter("@P_QUAL_DEGREENO", qual_Degree);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_SHOW_QUALIFY_DEGREE_DETAILS_BY_QUALDEGREE", objparams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OnlineAdmissionController.GetQualDegreeDetails->" + ex.ToString());
            }
            finally
            {
                ds.Dispose();
            }
            return ds;
        }
        /// <summary>
        /// Added by Nikhil L. to update the qualify degree.
        /// </summary>
        /// <param name="qual_DegreeNo"></param>
        /// <param name="degreeNo"></param>
        /// <param name="qualDegree"></param>
        /// <param name="activeStatus"></param>
        /// <param name="modified_IP"></param>
        /// <param name="modified_UANO"></param>
        /// <returns></returns>
        public int Update_Qualify_Degree(int qual_DegreeNo, int degreeNo,int BranchNo, int qualDegree,int QualBranchno, int activeStatus, string modified_IP, int modified_UANO)
        {
            //DataSet ds = null;
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
               {
                    new SqlParameter("@P_QUAL_DEGREENO",qual_DegreeNo) ,
                    new SqlParameter("@P_DEGREENO",degreeNo) ,
                    new SqlParameter("@P_BRANCHNO",BranchNo),
                    new SqlParameter("@P_QUAL_DEGREE",qualDegree) ,
                    new SqlParameter("@P_QUALBRANCHNO",QualBranchno),
                    new SqlParameter("@P_ACTIVE_STATUS",activeStatus) ,
                    new SqlParameter("@P_MODIFIED_IP",modified_IP) ,
                    new SqlParameter("@P_MODIFIED_UANO",modified_UANO) ,
                    //new SqlParameter("@P_MODIFIED_IP",modify_IP) ,
                    //new SqlParameter("@P_MODIFIED_UA_NO",modify_UANO) ,
                    new SqlParameter("@P_OUTPUT",SqlDbType.Int) 
               };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_QUAL_DEGREE_MAPPING", sqlParams, true);
                if (obj.Equals(2))
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OnlineAdmissionController.Add_Update_Qualify_Degree->" + ex.ToString());
            }
            return retStatus;
        }

        /// <summary>
        /// Added by Nikhil L. on 08/04/2022 to get the branch study by studyNo.
        /// </summary>
        /// <param name="studyNo"></param>
        /// <returns></returns>
        public DataSet GetBranchStudy(int studyNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objparams = null;
                objparams = new SqlParameter[1];
                objparams[0] = new SqlParameter("@P_STUDY_NO", studyNo);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_SHOW_BRANCH_STUDY_BY_STUDY_NO", objparams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OnlineAdmissionController.GetQualDegreeDetails->" + ex.ToString());
            }
            finally
            {
                ds.Dispose();
            }
            return ds;
        }
        /// <summary>
        /// Added by Nikhil L. on 08/04/2022 to add the branch study.
        /// </summary>
        /// <param name="degreeNo"></param>
        /// <param name="branchStudy"></param>
        /// <param name="activeStatus"></param>
        /// <param name="OrgId"></param>
        /// <param name="created_IP"></param>
        /// <param name="created_UANO"></param>
        /// <returns></returns>
        public int Add_Branch_Study(int degreeNo, string branchStudy, int activeStatus, int OrgId, string created_IP, int created_UANO, int qualDegreeNo)
        {
            //DataSet ds = null;
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
               {
                    new SqlParameter("@P_DEGREENO",degreeNo) ,
                    new SqlParameter("@P_BRANCH_STUDY",branchStudy) ,
                    new SqlParameter("@P_ACTIVE_STATUS",activeStatus) ,
                    new SqlParameter("@P_ORGANIZATION_ID",OrgId) ,
                    new SqlParameter("@P_CREATED_IP",created_IP) ,
                    new SqlParameter("@P_CREATED_UA_NO",created_UANO) ,
                    new SqlParameter("@P_QUAL_DEGREENO",qualDegreeNo) ,             //Added by Nikhil L. on 11/04/2022 to add data by qualifying degree.
                    //new SqlParameter("@P_MODIFIED_IP",modify_IP) ,
                    //new SqlParameter("@P_MODIFIED_UA_NO",modify_UANO) ,
                    new SqlParameter("@P_OUTPUT",SqlDbType.Int) 
               };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ADD_BRANCH_STUDY_MAPPING", sqlParams, true);
                if (obj.Equals(1))
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (obj.Equals(2))
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OnlineAdmissionController.Add_Branch_Study->" + ex.ToString());
            }
            return retStatus;
        }
        /// <summary>/
        /// Added by Nikhil Lambe on 08/04/2022 to update branch study.
        /// </summary>
        /// <param name="study_No"></param>
        /// <param name="degreeNo"></param>
        /// <param name="branchStudy"></param>
        /// <param name="activeStatus"></param>
        /// <param name="modified_IP"></param>
        /// <param name="modified_UANO"></param>
        /// <returns></returns>
        public int Update_Branch_Study(int study_No, int degreeNo, string branchStudy, int activeStatus, string modified_IP, int modified_UANO, int qualDegreeNo)
        {
            //DataSet ds = null;
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
               {
                    new SqlParameter("@P_STUDY_NO",study_No) ,
                    new SqlParameter("@P_DEGREENO",degreeNo) ,
                    new SqlParameter("@P_BRANCH_STUDY",branchStudy) ,
                    new SqlParameter("@P_ACTIVE_STATUS",activeStatus) ,
                    new SqlParameter("@P_MODIFIED_IP",modified_IP) ,
                    new SqlParameter("@P_MODIFIED_UANO",modified_UANO) ,
                    new SqlParameter("@P_QUAL_DEGREENO",qualDegreeNo) ,             //Added by Nikhil L. on 11/04/2022 to update the qualifying degree.
                    //new SqlParameter("@P_MODIFIED_IP",modify_IP) ,
                    //new SqlParameter("@P_MODIFIED_UA_NO",modify_UANO) ,
                    new SqlParameter("@P_OUTPUT",SqlDbType.Int) 
               };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_BRANCH_STUDY_MAPPING", sqlParams, true);
                if (obj.Equals(2))
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OnlineAdmissionController.Add_Update_Qualify_Degree->" + ex.ToString());
            }
            return retStatus;
        }
        /// <summary>
        /// Added by Nikhil Lambe on 19/04/2022 to get state and district.
        /// </summary>
        /// <param name="districtNo"></param>
        /// <returns></returns>
        public DataSet GetState_Dist(int districtNo, int stateNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objparams = null;
                objparams = new SqlParameter[2];
                objparams[0] = new SqlParameter("@P_DISTRICTNO", districtNo);
                objparams[1] = new SqlParameter("@P_STATENO", stateNo);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STATE_DISTRICT", objparams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OnlineAdmissionController.GetState_Dist->" + ex.ToString());
            }
            finally
            {
                ds.Dispose();
            }
            return ds;
        }
        /// <summary>
        /// Added by Nikhil L. on 19/04/2022 to add mapping for state and district.
        /// </summary>
        /// <param name="stateNo"></param>
        /// <param name="district"></param>
        /// <param name="activeStatus"></param>
        /// <returns></returns>
        public int Add_State_District(int stateNo, string district, int activeStatus)
        {
            //DataSet ds = null;
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
               {
                    new SqlParameter("@P_STATENO",stateNo) ,
                    new SqlParameter("@P_DISTRICT_NAME",district) ,
                    new SqlParameter("@P_ACTIVE_STATUS",activeStatus) , 
                    //new SqlParameter("@P_MODIFIED_IP",modify_IP) ,
                    //new SqlParameter("@P_MODIFIED_UA_NO",modify_UANO) ,
                    new SqlParameter("@P_OUTPUT",SqlDbType.Int) 
               };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ADD_STATE_DISTRICT_MAPPING", sqlParams, true);
                if (obj.Equals(1))
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (obj.Equals(2627))
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OnlineAdmissionController.Add_State_District->" + ex.ToString());
            }
            return retStatus;
        }
        /// <summary>
        /// Added by Nikhil L. on 19/04/2022 to update mapping for state and district.
        /// </summary>
        /// <param name="districtNo"></param>
        /// <param name="StateNo"></param>
        /// <param name="districtName"></param>
        /// <param name="activeStatus"></param>
        /// <returns></returns>
        public int Update_State_Dsitrict(int districtNo, int StateNo, string districtName, int activeStatus)
        {
            //DataSet ds = null;
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
               {
                    new SqlParameter("@P_DISTRICT_NO",districtNo) ,
                    new SqlParameter("@P_STATE_NO",StateNo) ,
                    new SqlParameter("@P_DISTRICT_NAME",districtName) ,
                    new SqlParameter("@P_ACTIVE_STATUS",activeStatus) ,
                    //new SqlParameter("@P_MODIFIED_IP",modify_IP) ,
                    //new SqlParameter("@P_MODIFIED_UA_NO",modify_UANO) ,
                    new SqlParameter("@P_OUTPUT",SqlDbType.Int) 
               };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_STATE_DISTRICT_MAPPING", sqlParams, true);
                if (obj.Equals(2))
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                }
                else
                {
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OnlineAdmissionController.Add_Update_Qualify_Degree->" + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetStudentListForAdmApproval(int admbatch,int Collegeid,int Degreeno,int Branchno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_ADMBATCH", admbatch);
                objParams[1] = new SqlParameter("@P_COLLEGE_ID", Collegeid);
                objParams[2] = new SqlParameter("@P_DEGREENO", Degreeno);
                objParams[3] = new SqlParameter("@P_BRANCHNO", Branchno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_STUDENT_LIST_FOR_APPROVAL", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OnlineAdmissionController.GetStudentListForAdmApproval-> " + ex.ToString());
            }
            return ds;
        }
        /// <summary>
        ///     Added By Rishabh on 15062022
        /// </summary>
        /// <param name="idno"></param>
        /// <returns></returns>
        public DataSet GetStudentInfo(int idno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_IDNO", idno);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUDENT_INFO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OnlineAdmissionController.GetStudentInfo-> " + ex.ToString());
            }
            return ds;
        }

        /// <summary>
        /// Added By Rishabh - 15062022 for Provisional Admission Letter Log
        /// </summary>
        /// <param name="idno"></param>
        /// <param name="rrno"></param>
        /// <returns></returns>
        public int OfferLetterLog(int idno, string rrno)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_STUD_IDNO", idno);
                objParams[1] = new SqlParameter("@P_UA_NO", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                objParams[2] = new SqlParameter("@P_REGISTRATION_NO", rrno);
                objParams[3] = new SqlParameter("@P_IPADDRESS", System.Web.HttpContext.Current.Session["ipAddress"].ToString());
                objParams[4] = new SqlParameter("@P_MACADDRESS", System.Web.HttpContext.Current.Session["macAddress"]);
                objParams[5] = new SqlParameter("@P_ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));


                object ret = objSQLHelper.ExecuteNonQuerySP("ACD_CREATE_USER_OFFER_LETTER", objParams, true);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.OnlineAdmissionController.OfferLetterLog-> " + ex.ToString());
            }
            return retStatus;
        }

        public int UpdateStudPhotoFromNpf(Student objstud, int type)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //UpdateFaculty Reference
                objParams = new SqlParameter[3];

                objParams[0] = new SqlParameter("@P_USERNO", objstud.IdNo);
                objParams[1] = new SqlParameter("@P_STUD_PHOTO", objstud.StudPhoto);
                objParams[2] = new SqlParameter("@P_TYPE", type);

                if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_UPD_STUDENT_PHOTO_FROM_NPF", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentPhoto-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetStudentPhotoInfoByNPF(int admissionbatch, int type)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_ADMBATCHNO", admissionbatch);
                objParams[1] = new SqlParameter("@P_TYPE", type);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_SP_GET_STUDENT_PHOTO_FROM_NPF", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OnlineAdmissionController.GetStudentInfo-> " + ex.ToString());
            }
            return ds;
        }

        public int UpdateDocument(int userno, int DOCTYPENO, string filename)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_USERNO",userno),
                    new SqlParameter("@P_DOCTYPENO",DOCTYPENO), 
                    new SqlParameter("@P_PATH",null),
                    new SqlParameter("@P_FILENAME",filename)
                };
                //sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DOCUMENTENTRY_UPDATE_NPF", sqlParams, true);

                if (obj != null && obj.ToString() != "-99")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.DocumentController.AddDocument() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
        //added for online degree subject Mapping
        public int AdddegreesubjectMapping(int subid, int degree, int subjectno, string subname, int iscompulsory, int iscutoff, int isother, int isactive, int CreatedBy, string ipAddress, int modifiedby, string modifiedipaddresss, int orgid, string subjectName, int special, int branchNo)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[17];
                objParams[0] = new SqlParameter("@P_SUB_ID", subid);
                objParams[1] = new SqlParameter("@P_DEGREENO", degree);
                objParams[2] = new SqlParameter("@P_SUBJECT_NO", subjectno);
                objParams[3] = new SqlParameter("@P_SUBJECT_NAME", subname);
                objParams[4] = new SqlParameter("@P_IS_COMPULSORY", iscompulsory);
                objParams[5] = new SqlParameter("@P_IS_CUTOFF", iscutoff);
                objParams[6] = new SqlParameter("@P_IS_OTHERS", isother);
                objParams[7] = new SqlParameter("@P_ACTIVE_STATUS", isactive);
                objParams[8] = new SqlParameter("@P_CREATED_BY", CreatedBy);
                objParams[9] = new SqlParameter("@P_CREATED_IP_ADDRESS", ipAddress);
                objParams[10] = new SqlParameter("@P_MODIFIED_BY", modifiedby);
                objParams[11] = new SqlParameter("@P_MODIFIED_IP_ADDRESS", modifiedipaddresss);
                objParams[12] = new SqlParameter("@P_ORGANIZATIONID", orgid);
                objParams[13] = new SqlParameter("@P_SUBJECT", subjectName);
                objParams[14] = new SqlParameter("@P_SPECIAL", special);
                objParams[15] = new SqlParameter("@P_BRANCHNO", branchNo);
                objParams[16] = new SqlParameter("@P_OUTPUT", SqlDbType.Int);
                objParams[16].Direction = ParameterDirection.Output;

                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_SP_INS_ACD_SUBJECT_ONLINE", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                {
                    if (obj.ToString() == "1")
                        status = Convert.ToInt32(CustomStatus.RecordSaved);
                    else if (obj.ToString() == "2")
                        status = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.AddClubData --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

        public DataSet GetAllOnlineMappingdata()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);

                ds = objSQLHelper.ExecuteDataSet("PKG_GET_BY_ACD_SUBJECT_ONLINE");
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetAllOnlineMappingdata-> " + ex.ToString());
            }
            return ds;
        }
        public SqlDataReader GetAllOnlineMappingdatabyno(int subid)
        {
            SqlDataReader dr = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_SUB_ID", subid);
                dr = objSQLHelper.ExecuteReaderSP("PKG_GET_BY_NO_ACD_SUBJECT_ONLINE", objParams);
            }
            catch (Exception ex)
            {
                return dr;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetSingleSession-> " + ex.ToString());
            }
            return dr;
        }
        //ADDED BY POOJA FOR NPF
        public int UpdateStudPhotoFromNpfPhotos(Student objstud, int type)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //UpdateFaculty Reference
                objParams = new SqlParameter[3];

                objParams[0] = new SqlParameter("@P_USERNO", objstud.IdNo);
                objParams[1] = new SqlParameter("@P_STUD_PHOTO", objstud.StudPhoto);
                objParams[2] = new SqlParameter("@P_DOCNO", type);

                if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SP_UPD_STUDENT_PHOTO_FROM_NPF_PHOTOS", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.UpdateStudentPhoto-> " + ex.ToString());
            }
            return retStatus;
        }
        /// <summary>
        /// Added by Nikhil L. on 17/01/2023 to add online admission config for NRI.
        /// </summary>
        /// <param name="objConfig"></param>
        /// <param name="STime"></param>
        /// <param name="ETime"></param>
        /// <param name="UgPg"></param>
        /// <param name="orgId"></param>
        /// <param name="activeStatus"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public int AddOnlineAdm_NRI(Config objConfig, string STime, string ETime, int UgPg, int orgId, int activeStatus, string mode, int configID)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //Add New Adm
                objParams = new SqlParameter[13];
                objParams[0] = new SqlParameter("@P_ADMBATCH", objConfig.Admbatch);
                objParams[1] = new SqlParameter("@P_DEGREENO", objConfig.Degree_No);
                objParams[2] = new SqlParameter("@P_ADMSTRDATE", objConfig.Config_SDate);
                objParams[3] = new SqlParameter("@P_STARTTIME", STime);
                objParams[4] = new SqlParameter("@P_ADMENDDATE", objConfig.Config_EDate);
                objParams[5] = new SqlParameter("@P_ENDTIME", ETime);
                objParams[6] = new SqlParameter("@P_DETAILS", objConfig.Details);
                objParams[7] = new SqlParameter("@P_FEES", objConfig.Fees);
                objParams[8] = new SqlParameter("@P_UGPG", UgPg);
                objParams[9] = new SqlParameter("@P_ADMTYPE", objConfig.AdmType);
                objParams[10] = new SqlParameter("@P_ACTIVE_STATUS", activeStatus);
                objParams[11] = new SqlParameter("@P_MODE", mode);
                objParams[12] = new SqlParameter("@P_CONFIGID", configID);
                objParams[12].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_ADD_UPDATE_ADMISSION_CONFIG_FOR_NRI", objParams, true);
                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OnlineAdmissionController.AddOnlineAdm_NRI-> " + ex.ToString());
            }
            return status;
        }
        /// <summary>
        /// Added by Nikhil L. on 17/01/2023 to update online admission config for NRI.
        /// </summary>
        /// <param name="objConfig"></param>
        /// <param name="STime"></param>
        /// <param name="ETime"></param>
        /// <param name="UgPg"></param>
        /// <param name="activeStatus"></param>
        /// <returns></returns>
        public int UpdateOnlineAdm_NRI(Config objConfig, string STime, string ETime, int UgPg, int activeStatus)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                SqlParameter[] objParams = null;

                //update
                objParams = new SqlParameter[13];
                objParams[0] = new SqlParameter("@P_ADMBATCH", objConfig.Admbatch);
                objParams[1] = new SqlParameter("@P_DEGREENO", objConfig.Degree_No);
                objParams[2] = new SqlParameter("@P_ADMSTRDATE", objConfig.Config_SDate);
                objParams[3] = new SqlParameter("@P_STARTTIME", STime);
                objParams[4] = new SqlParameter("@P_ADMENDDATE", objConfig.Config_EDate);
                objParams[5] = new SqlParameter("@P_ENDTIME", ETime);
                objParams[6] = new SqlParameter("@P_DETAILS", objConfig.Details);
                objParams[7] = new SqlParameter("@P_FEES", objConfig.Fees);
                objParams[8] = new SqlParameter("@P_COLLEGEID", objConfig.College_Id);
                objParams[9] = new SqlParameter("@P_UGPG", UgPg);
                objParams[10] = new SqlParameter("@P_CONFIGID", objConfig.ConfigID);
                objParams[11] = new SqlParameter("@P_ADMTYPE", objConfig.AdmType);
                objParams[12] = new SqlParameter("@P_ACTIVE_STATUS", activeStatus);

                if (objSQLHelper.ExecuteNonQuerySP("PKG_ACD_UPD_ADMISSION_CONFIG_FOR_NRI", objParams, false) != null)
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OnlineAdmissionController.UpdateOnlineAdm_NRI-> " + ex.ToString());
            }
            return retStatus;
        }
    }
}

