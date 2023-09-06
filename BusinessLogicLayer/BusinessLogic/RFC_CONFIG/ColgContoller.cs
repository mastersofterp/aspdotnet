//======================================================================================
// PROJECT NAME  : RFC COMMON                                                                
// MODULE NAME   : ColgContoller          
// CREATION DATE : 08-OCTOMBER-2021                                                         
// CREATED BY    : Rishabh
// ADDED BY      : 
// ADDED DATE    :                                       
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;
using System.Data;
using IITMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS;


namespace IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG
{
    public class ColgContoller
    {
        string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        /// <summary>
        /// ADDED ON 04-11-2021 BY Rishabh to Insert/Update College Master.
        /// </summary>
        /// <param name="updInfo"></param>
        /// <param name="hdf"></param>
        /// <returns></returns>
        //public int SaveCollegeInformation(College updInfo, int hdf)
        //{
        //    int retStatus = Convert.ToInt32(CustomStatus.Others);
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(connectionstring);
        //        SqlParameter[] sqlParams = null;

        //        sqlParams = new SqlParameter[18];
        //        sqlParams[0] = new SqlParameter("@COLLEGE_ID", updInfo.COLLEGE_ID);
        //        sqlParams[1] = new SqlParameter("@P_UNIVERSITY", updInfo.University);
        //        // sqlParams[2] = new SqlParameter("@P_COLLEGETYPE", updInfo.CollegeType);
        //        sqlParams[2] = new SqlParameter("@P_INSTITUTETYPE", updInfo.InstituteType);
        //        sqlParams[3] = new SqlParameter("@P_COLLEGENAME", updInfo.Name);
        //        sqlParams[4] = new SqlParameter("@P_CODE", updInfo.CollegeCode);
        //        sqlParams[5] = new SqlParameter("@P_SHORTNAME", updInfo.Short_Name);
        //        sqlParams[6] = new SqlParameter("@P_LOCATION", updInfo.Location);
        //        sqlParams[7] = new SqlParameter("@P_ADDRESS", updInfo.Address);
        //        sqlParams[8] = new SqlParameter("@P_STATE", updInfo.State);
        //        //sqlParams[9] = new SqlParameter("@P_LOGO", updInfo.UploadLogo);

        //        if (updInfo.UploadLogo == null)
        //            sqlParams[9] = new SqlParameter("@P_LOGO", DBNull.Value);
        //        else
        //            sqlParams[9] = new SqlParameter("@P_LOGO", updInfo.UploadLogo);
        //        sqlParams[9].SqlDbType = SqlDbType.Image;
        //        sqlParams[10] = new SqlParameter("@P_HDN", hdf);

        //        sqlParams[11] = new SqlParameter("@P_ACTIVESTATUS", updInfo.ActiveStatus);
        //        sqlParams[12] = new SqlParameter("@CreatedBy", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
        //        sqlParams[13] = new SqlParameter("@ModifiedBy", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
        //        sqlParams[14] = new SqlParameter("@IPAddress", System.Web.HttpContext.Current.Session["ipAddress"].ToString());
        //        sqlParams[15] = new SqlParameter("@MacAddress", System.Web.HttpContext.Current.Session["macAddress"]);
        //        sqlParams[16] = new SqlParameter("@ORGANIZATIONID", updInfo.OrgId);
        //        sqlParams[17] = new SqlParameter("@P_OUT", SqlDbType.Int);
        //        sqlParams[17].Direction = ParameterDirection.Output;

        //        //if (objSQLHelper.ExecuteNonQuerySP("sptblConfigCollegeMaster_Insert_Update", sqlParams, false) != null)
        //        //    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
        //        object ret = objSQLHelper.ExecuteNonQuerySP("sptblConfigCollegeMaster_Insert_Update", sqlParams, true);
        //        retStatus = Convert.ToInt32(ret);
        //    }
        //    catch (Exception ex)
        //    {
        //        retStatus = Convert.ToInt32(CustomStatus.Error);
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ColgContoller.SaveCollegeInformation-> " + ex.ToString());
        //    }
        //    return retStatus;
        //}
//*********************added on 22112022*************************

        public int SaveCollegeInformation(College updInfo, int hdf)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] sqlParams = null;

                sqlParams = new SqlParameter[19];
                sqlParams[0] = new SqlParameter("@COLLEGE_ID", updInfo.COLLEGE_ID);
                sqlParams[1] = new SqlParameter("@P_UNIVERSITY", updInfo.University);
                // sqlParams[2] = new SqlParameter("@P_COLLEGETYPE", updInfo.CollegeType);
                sqlParams[2] = new SqlParameter("@P_INSTITUTETYPE", updInfo.InstituteType);
                sqlParams[3] = new SqlParameter("@P_COLLEGENAME", updInfo.Name);
                sqlParams[4] = new SqlParameter("@P_CODE", updInfo.CollegeCode);
                sqlParams[5] = new SqlParameter("@P_SHORTNAME", updInfo.Short_Name);
                sqlParams[6] = new SqlParameter("@P_LOCATION", updInfo.Location);
                sqlParams[7] = new SqlParameter("@P_ADDRESS", updInfo.Address);
                sqlParams[8] = new SqlParameter("@P_STATE", updInfo.State);
                //sqlParams[9] = new SqlParameter("@P_LOGO", updInfo.UploadLogo);

                if (updInfo.UploadLogo == null)
                    sqlParams[9] = new SqlParameter("@P_LOGO", DBNull.Value);
                else
                    sqlParams[9] = new SqlParameter("@P_LOGO", updInfo.UploadLogo);
                sqlParams[9].SqlDbType = SqlDbType.Image;
                sqlParams[10] = new SqlParameter("@P_HDN", hdf);

                sqlParams[11] = new SqlParameter("@P_ACTIVESTATUS", updInfo.ActiveStatus);
                sqlParams[12] = new SqlParameter("@CreatedBy", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                sqlParams[13] = new SqlParameter("@ModifiedBy", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                sqlParams[14] = new SqlParameter("@IPAddress", System.Web.HttpContext.Current.Session["ipAddress"].ToString());
                sqlParams[15] = new SqlParameter("@MacAddress", System.Web.HttpContext.Current.Session["macAddress"]);
                sqlParams[16] = new SqlParameter("@ORGANIZATIONID", updInfo.OrgId);

                if (updInfo.UploadSign == null)
                    sqlParams[17] = new SqlParameter("@P_COE_Sign", DBNull.Value);
                else
                    sqlParams[17] = new SqlParameter("@P_COE_Sign", updInfo.UploadSign);
                sqlParams[17].SqlDbType = SqlDbType.Image;

                sqlParams[18] = new SqlParameter("@P_OUT", SqlDbType.Int);
                sqlParams[18].Direction = ParameterDirection.Output;

                //if (objSQLHelper.ExecuteNonQuerySP("sptblConfigCollegeMaster_Insert_Update", sqlParams, false) != null)
                //    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                object ret = objSQLHelper.ExecuteNonQuerySP("sptblConfigCollegeMaster_Insert_Update", sqlParams, true);
                retStatus = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ColgContoller.SaveCollegeInformation-> " + ex.ToString());
            }
            return retStatus;
        }




 //*************END*********************

        /// <summary>
        /// ADDED ON 05-10-2021 BY Rishabh to get the details of college master.
        /// </summary>
        /// <returns></returns>
        public DataSet GetCollegeInfo()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_ORGID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                ds = objSQLHelper.ExecuteDataSetSP("SP_CONFIG_GET_COLLEGEMASTER", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ColgContoller.GetCollegeInfo-> " + ex.ToString());
            }
            return ds;
        }

        /// <summary>
        /// ADDED ON 05-10-2021 BY Rishabh to get datails of college master by college_id.
        /// </summary>
        /// <param name="college_id"></param>
        /// <returns></returns>
        public DataSet EditCollegeInfo(int college_id)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParameter = new SqlParameter[1];
                objParameter[0] = new SqlParameter("@P_COLLEGE_ID", college_id);
                ds = objSQLHelper.ExecuteDataSetSP("SP_CONFIG_GET_COLLEGEMASTER_BY_COLLEGE_ID", objParameter);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.ColgContoller.EditCollegeInfo-> " + ex.ToString());
            }
            return ds;
        }
    }
}
