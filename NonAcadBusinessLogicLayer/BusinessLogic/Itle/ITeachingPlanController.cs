using System;
using System.Data;
using System.Data.SqlClient;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLayer.BusinessLogic;
using IITMS.SQLServer.SQLDAL;
using IITMS.NITPRM;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    /// <summary>
    /// This ITeachingPlanController is used to control ACD_ITEACHINGPLAN table.
    /// </summary>
    public partial class ITeachingPlanController
    {
        /// <summary>ConnectionStrings</summary>
        string _nitprm_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddTeachingPlan(ITeachingPlan objPlan)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSH = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[]
                {
                    new SqlParameter("@P_SESSIONNO", objPlan.SESSIONNO),
                    new SqlParameter("@P_UA_NO", objPlan.UA_NO),
                    new SqlParameter("@P_COURSENO", objPlan.COURSENO),
                    new SqlParameter("@P_STARTDATE", objPlan.STARTDATE),
                    new SqlParameter("@P_ENDDATE", objPlan.ENDDATE),
                    new SqlParameter("@P_DESCRIPTION", objPlan.DESCRIPTION),
                    new SqlParameter("@P_MEDIA", objPlan.MEDIA),
                    new SqlParameter("@P_SUBJECT", objPlan.SUBJECT),
                    new SqlParameter("@P_SYLLABUS_NAME",objPlan.SYLLABUS_NAME),
                    new SqlParameter("@P_UNIT_NAME",objPlan.UNIT_NAME),
                    new SqlParameter("@P_TOPIC_NAME",objPlan.TOPIC_NAME),
                    new SqlParameter("@P_STATUS", objPlan.STATUS),
                    new SqlParameter("@P_COLLEGE_CODE", objPlan.COLLEGE_CODE),
                    new SqlParameter("@P_PLAN_NO", SqlDbType.Int)
                };
                objParams[objParams.Length - 1].Direction = ParameterDirection.Output;

                object ret = objSH.ExecuteNonQuerySP("PKG_ITLE_SP_INS_ITEACHINGPLAN", objParams, true);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITeachingPlanController.AddTeachingPlan -> " + ex.ToString());
            }
            return retStatus;
        }

        public int UpdateTeachingPlan(ITeachingPlan objPlan)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQL = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[]
                {
                    new SqlParameter("@P_SESSIONNO", objPlan.SESSIONNO),
                    new SqlParameter("@P_UA_NO", objPlan.UA_NO),
                    new SqlParameter("@P_COURSENO", objPlan.COURSENO),
                    new SqlParameter("@P_STARTDATE", objPlan.STARTDATE),
                    new SqlParameter("@P_ENDDATE", objPlan.ENDDATE),
                    new SqlParameter("@P_DESCRIPTION", objPlan.DESCRIPTION),
                    new SqlParameter("@P_MEDIA", objPlan.MEDIA),
                    new SqlParameter("@P_SUBJECT", objPlan.SUBJECT),
                    new SqlParameter("@P_SECTION", objPlan.SYLLABUS_NAME),
                    new SqlParameter("@P_UNIT", objPlan.UNIT_NAME),
                    new SqlParameter("@P_TOPIC", objPlan.TOPIC_NAME),
                   

                    new SqlParameter("@P_STATUS", objPlan.STATUS),
                    new SqlParameter("@P_PLAN_NO", objPlan.PLAN_NO)
                };
                objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object ret = objSQL.ExecuteNonQuerySP("PKG_ITLE_SP_UPD_ITEACHPLAN_BYPLAN_NO", objParams, true);
                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITeachingPlanController.UpdateTeachingPlan -> " + ex.ToString());
            }
            return retStatus;
        }

        public int UpdateStatusToDelete(ITeachingPlan objPlan)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSH = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[]
                {
                    new SqlParameter("@P_SESSIONNO", objPlan.SESSIONNO),
                    new SqlParameter("@P_COURSENO", objPlan.COURSENO),
                    new SqlParameter("@P_PLAN_NO", objPlan.PLAN_NO),
                    new SqlParameter("@P_OUT", SqlDbType.Int)
                };
                objParams[3].Direction = ParameterDirection.Output;

                object ret = objSH.ExecuteNonQuerySP("PKG_ITLE_SP_DEL_ITEACHPLAN_BYPLAN_NO", objParams, true);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITeachingPlanController.UpdateStatusByDelete -> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetAllTeachingPlanbyUaNo(ITeachingPlan objTPlan)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSH = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_SESSIONNO", objTPlan.SESSIONNO);
                objParams[1] = new SqlParameter("@P_COURSENO", objTPlan.COURSENO);
                objParams[2] = new SqlParameter("@P_UA_NO", objTPlan.UA_NO);

                ds = objSH.ExecuteDataSetSP("PKG_ITLE_SP_RET_ALLITEACHPLAN_BYUA_NO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITeachingPlanController.GetAllTeachingPlanbyUaNo-> " + ex.ToString());
            }
            return ds;
        }

        public DataTableReader GetSinglePlanByPlanNo(int planNo)
        {
            DataTableReader dtr = null;
            try
            {
                SQLHelper objSH = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[] 
                { 
                    new SqlParameter("@P_PLAN_NO", planNo) 
                };
                dtr = objSH.ExecuteDataSetSP("PKG_ITLE_SP_RET_ITEACHPLAN_BYPLAN_NO", objParams).Tables[0].CreateDataReader();
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITeachingPlanController.GetSinglePlanByPlanNo-> " + ex.ToString());
            }
            return dtr;
        }

        public DataSet GetAllTeachingPlanbyCourseNo(ITeachingPlan objTPlan)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSH = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_SESSIONNO", objTPlan.SESSIONNO);
                objParams[1] = new SqlParameter("@P_COURSENO", objTPlan.COURSENO);

                ds = objSH.ExecuteDataSetSP("PKG_ITLE_SP_RET_ALLITEACHPLAN_BYCOURSENO", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITeachingPlanController.GetAllTeachingPlanbyCourseNo-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetAllExecutedTPlan(int tPlanNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSH = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[] 
                { 
                   new SqlParameter("@P_TPLAN_NO", tPlanNo),
                   new SqlParameter("@P_SESSION_NO", 0 as object),
                   new SqlParameter("@P_USER_NO", 0 as object),
                   new SqlParameter("@P_COURSE_NO", 0 as object)
                };
                ds = objSH.ExecuteDataSetSP("PKG_ITLE_GET_ALL_EXECTPLAN", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITeachingPlanController.GetAllTeachingPlanbyCourseNo-> " + ex.ToString());
            }
            return ds;
        }

        public DataSet GetExecutedTPlanDetails(int execTPlanNo)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSH = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[] 
                { 
                   new SqlParameter("@P_EXEC_TPLAN_NO", execTPlanNo)
                };
                ds = objSH.ExecuteDataSetSP("PKG_ITLE_GET_EXECTPLAN_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITeachingPlanController.GetAllTeachingPlanbyCourseNo-> " + ex.ToString());
            }
            return ds;
        }

        public int AddExecutedTPlan(ExecutedTPlan execTPlan)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSH = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[]
                {
                    new SqlParameter("@P_TPLAN_NO", execTPlan.TPlanNo),
                    new SqlParameter("@P_COVERED_TOPIC", execTPlan.CoveredTopics),
                    new SqlParameter("@P_DEVIATION", execTPlan.Deviation),
                    new SqlParameter("@P_DEVIATION_REASON", execTPlan.ReasonForDeviation),
                    new SqlParameter("@P_RESOURCES", execTPlan.ResourcesUsed),
                    new SqlParameter("@P_EXEC_TPLAN_NO", execTPlan.ExecutedTPlanNo)
                };
                objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object ret = objSH.ExecuteNonQuerySP("PKG_ITLE_SP_INS_EXECUTED_TPLAN", objParams, true);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITeachingPlanController.AddTeachingPlan -> " + ex.ToString());
            }
            return retStatus;
        }

        public int UpdateExecutedTPlan(ExecutedTPlan execTPlan)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSH = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[]
                {
                    new SqlParameter("@P_TPLAN_NO", execTPlan.TPlanNo),
                    new SqlParameter("@P_COVERED_TOPIC", execTPlan.CoveredTopics),
                    new SqlParameter("@P_DEVIATION", execTPlan.Deviation),
                    new SqlParameter("@P_DEVIATION_REASON", execTPlan.ReasonForDeviation),
                    new SqlParameter("@P_RESOURCES", execTPlan.ResourcesUsed),
                    new SqlParameter("@P_EXEC_TPLAN_NO", execTPlan.ExecutedTPlanNo)
                };
                objParams[objParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object ret = objSH.ExecuteNonQuerySP("PKG_ITLE_SP_UPD_EXECUTED_TPLAN", objParams, true);

                if (Convert.ToInt32(ret) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.Error);
                else
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ITeachingPlanController.AddTeachingPlan -> " + ex.ToString());
            }
            return retStatus;
        }


        public DataTableReader GetTopicDescription(ITeachingPlan objTPlan,string topic_name)
        {
            DataTableReader dtr = null;
            try
            {
                SQLHelper objsqlhelper = new SQLHelper(_nitprm_constr);
                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_COURSENO", objTPlan.COURSENO);
                objParams[1] = new SqlParameter("@P_UA_NO", objTPlan.UA_NO);
                objParams[2] = new SqlParameter("@P_SESSIONNO", objTPlan.SESSIONNO);
                //objParams[3] = new SqlParameter("@P_SUB_NO", objTPlan.PLAN_NO);
                objParams[3] = new SqlParameter("@P_TOPIC", topic_name);

                dtr = objsqlhelper.ExecuteDataSetSP("PKG_ITLE_SP_RET_TOPIC_DESCRIPTION", objParams).Tables[0].CreateDataReader();


            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.CCMS.BusinessLogicLayer.BusinessLogic.Common.FillDropDown-> " + ex.ToString());
            }
            return dtr;
        }


        //FOR CHACKING STATUS OF TEACHING PLAN

        //public int GetStatus(DateTime current_date)
        //{
        //    int flag = 0;
        //    try
        //    {

        //        SQLHelper objHelper = new SQLHelper(_nitprm_constr);
        //        SqlParameter[] objparam = new SqlParameter[2];

        //        objparam[0] = new SqlParameter("@@P_DATE", current_date);
        //        objparam[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
        //        objparam[1].Direction = ParameterDirection.Output;
        //        Object ret = objHelper.ExecuteNonQuerySP("PKG_SP_ITLE_TEACH_PLAN_UPDATE_STATUS", objparam, true);

        //        if (Convert.ToInt32(ret) != -99)
        //            flag = 1;
        //        else
        //            flag = 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.IAnnouncementController.GetStatus -> " + ex.ToString());
        //    }
        //    return flag;
        //}

    }
}