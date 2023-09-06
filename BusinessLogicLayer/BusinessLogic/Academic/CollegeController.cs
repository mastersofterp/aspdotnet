using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;
using System.Data;
using IITMS;
using IITMS.UAIMS.BusinessLogicLayer.BusinessLogic;



using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;


namespace IITMS.UAIMS.BusinessLogicLayer.BusinessLogic
{
    public class CollegeController
    {
        string connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;



        //modified on[28-09-2016]
        public int AddCollege(College objCollege)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_COLLEGE_ID", objCollege.COLLEGE_ID);
                objParams[1] = new SqlParameter("@P_NAME", objCollege.Name);
                objParams[2] = new SqlParameter("@P_COLLEGE_TYPE", objCollege.Collegetype);
                objParams[3] = new SqlParameter("@P_SHORTNAME", objCollege.Short_Name);
                objParams[4] = new SqlParameter("@P_CODE", objCollege.CollegeCode);
                objParams[5] = new SqlParameter("@P_LOCATION", objCollege.Location);

                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_COLLEGEMASTER_DETAILS", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CollegeController.AddCollege --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
        //modified[28-09-2016]
        public int UpdateCollege(College objCollege)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_COLLEGE_ID", objCollege.COLLEGE_ID);
                objParams[1] = new SqlParameter("@P_NAME", objCollege.Name);
                objParams[2] = new SqlParameter("@P_COLLEGE_TYPE", objCollege.Collegetype);
                objParams[3] = new SqlParameter("@P_SHORTNAME", objCollege.Short_Name);
                objParams[4] = new SqlParameter("@P_CODE", objCollege.CollegeCode);
                objParams[5] = new SqlParameter("@P_LOCATION", objCollege.Location);

                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_ACAD_UPDATE_COLLEGEMASTER_DETAILS", objParams, true);

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

        public DataSet Getdetails()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[0];

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_SELECT_COLLEGE_MASTER_DETAILS", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CollegeController.Getdetails() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }



       
        /// <summary>
        /// Added by Abhishek 27052019
        /// </summary>
        /// <param name="objfit"></param>
        /// <returns></returns>
        public string addfitfile(College objfit)
        {
            string Status = string.Empty;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@idno", objfit.IDNO);
                objParams[1] = new SqlParameter("@documentno", objfit.DOCUMENTNO);
                objParams[2] = new SqlParameter("@doctype", objfit.DOCTYPE);
                objParams[3] = new SqlParameter("@docname", objfit.DOCNAME);
                objParams[4] = new SqlParameter("@docpath", objfit.DOCPATH);
                objParams[5] = new SqlParameter("@enroll", objfit.ENROLL);
                objParams[6] = new SqlParameter("@roll", objfit.ROLL);

                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_ADDFITDOC", objParams, true);

                Status = obj.ToString();
            }
            catch (Exception ex)
            {
                Status = "0";
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CollegeController.AddCollege --> " + ex.Message + " " + ex.StackTrace);
            }
            return Status;
        }

        /// <summary>
        /// Added by Abhishek 27052019
        /// </summary>
        /// <param name="objfit"></param>
        /// <returns></returns>
        /// <returns></returns>
        public DataSet GetAllFit(College objfit)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@idno", objfit.IDNO);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_SP_ALL_STUD", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetAllStud-> " + ex.ToString());
            }
            return ds;
        }

        /// <summary>
        /// Added by Abhishek 27052019
        /// </summary>
        /// <param name="objfit"></param>
        /// <returns></returns>

        public DataSet GetAllstudinfo(College objfit)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@idno", objfit.IDNO);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_STUD_SP_STUDINFO", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetAllStud-> " + ex.ToString());
            }
            return ds;
        }

        //Added by Nikhil Vinod Lambe on 26/03/2021 to add Affiliated College
        public int AddAffiliatedCollege(int COLLEGE_ID, string Name, string Collegetype, string Short_Name, string CollegeCode, string Location)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_COLLEGE_ID", COLLEGE_ID);
                objParams[1] = new SqlParameter("@P_NAME", Name);
                objParams[2] = new SqlParameter("@P_COLLEGE_TYPE", Collegetype);
                objParams[3] = new SqlParameter("@P_SHORTNAME", Short_Name);
                objParams[4] = new SqlParameter("@P_CODE", CollegeCode);
                objParams[5] = new SqlParameter("@P_LOCATION", Location);

                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_ACAD_INSERT_AFFILIATED_COLLEGE_DETAILS", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CollegeController.AddAffiliatedCollege --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
        //Added by Nikhil Vinod Lambe on 26/03/2021 to update Affiliated College
        public int UpdateAffiliatedCollege(int COLLEGE_ID, string Name, string Collegetype, string Short_Name, string CollegeCode, string Location)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_COLLEGE_ID", COLLEGE_ID);
                objParams[1] = new SqlParameter("@P_NAME", Name);
                objParams[2] = new SqlParameter("@P_COLLEGE_TYPE", Collegetype);
                objParams[3] = new SqlParameter("@P_SHORTNAME", Short_Name);
                objParams[4] = new SqlParameter("@P_CODE", CollegeCode);
                objParams[5] = new SqlParameter("@P_LOCATION", Location);

                object obj = objSqlHelper.ExecuteNonQuerySP("PKG_ACAD_UPDATE_AFFILIATED_COLLEGE_DETAILS", objParams, true);

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
        /// Added By Rishabh - 15062022
        /// </summary>
        /// <param name="haid"></param>
        /// <returns></returns>
        public DataSet GetHouseAllotedData(int haid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_HA_ID", haid);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_HOUSE_ALLOTMENT", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CollegeController.GetHouseAllotedData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        /// <summary>
        /// Added By Rishabh - 15062022  - SAVE/UPDATE House Allotment
        /// </summary>
        public int SaveHouseAllotment(College objCollege)
        {
            int status = 0;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionstring);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@P_HA_ID", objCollege.Ha_Id);
                objParams[1] = new SqlParameter("@P_HOUSE_ID", objCollege.House_Id);
                objParams[2] = new SqlParameter("@P_EITHER_VALUE", objCollege.Either_Val);
                objParams[3] = new SqlParameter("@P_OR_VALUE", objCollege.Or_Val);
                objParams[4] = new SqlParameter("@P_HOUSE_NAME", objCollege.HouseName);
                objParams[5] = new SqlParameter("@P_HOUSE_CODE", objCollege.HouseCode);
                objParams[6] = new SqlParameter("@P_COLOUR", objCollege.Colour);
                objParams[7] = new SqlParameter("@P_ACTIVE_STATUS", objCollege.Active_Status);
                objParams[8] = new SqlParameter("@P_ORGANIZATION_ID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[9].Direction = ParameterDirection.Output;

                object ret = objSqlHelper.ExecuteNonQuerySP("PKG_ACD_SAVE_HOUSE_ALLOTMENT", objParams, true);
                status = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.CollegeController.SaveHouseAllotment() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }


    }
}

