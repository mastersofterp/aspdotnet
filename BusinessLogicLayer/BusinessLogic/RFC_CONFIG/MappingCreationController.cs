
//======================================================================================
// PROJECT NAME  : RFC COMMON                                                                
// MODULE NAME   : Mapping CreationController                  
// CREATION DATE : 08-OCTOMBER-2021                                                         
// CREATED BY    : S.Patil
// ADDED BY      : 
// ADDED DATE    :                                       
// MODIFIED DATE :                                                                      
// MODIFIED DESC :                                                                      
//======================================================================================

using System;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.RFC_CONFIG;
using System.Collections.Generic;
namespace IITMS.UAIMS.BusinessLogicLayer.BusinessLogic.RFC_CONFIG
{
    public class MappingCreationController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;
        /// <summary>
        /// ADDED ON 07-10-2021 BY S.Patil to save org col mapping
        /// </summary>
        /// <param name="objOrgCol"></param>
        /// <param name="colids"></param>
        /// <param name="colnames"></param>
        /// <returns></returns>
        //public int SaveOrgColMapping(Mapping objOrgCol,string colids,string colnames)
        //{
        //    int status = 0;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
        //        SqlParameter[] objParams = null;
        //        objParams = new SqlParameter[10];
        //        objParams[0] = new SqlParameter("@OrganizationCollegeId", objOrgCol.OrganizationCollegeId);
        //        objParams[1] = new SqlParameter("@OrganizationId", objOrgCol.OrganizationId);
        //        objParams[2] = new SqlParameter("@CollegeId", colids);
        //        objParams[3] = new SqlParameter("@OrganizationCollegeName", colnames);
        //        objParams[4] = new SqlParameter("@CreatedBy", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
        //        objParams[5] = new SqlParameter("@ModifiedBy", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
        //        objParams[6] = new SqlParameter("@IPAddress", System.Web.HttpContext.Current.Session["ipAddress"].ToString());
        //        objParams[7] = new SqlParameter("@ActiveStatus", objOrgCol.ActiveStatus);
        //        objParams[8] = new SqlParameter("@MACAddress", System.Web.HttpContext.Current.Session["macAddress"]);
        //        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
        //        objParams[9].Direction = ParameterDirection.Output;

        //        object ret = objSQLHelper.ExecuteNonQuerySP("sptblMapOrganizationCollege_Insert_Update", objParams, true);
        //        status = Convert.ToInt32(ret);
        //    }
        //    catch (Exception ex)
        //    {
        //        status = Convert.ToInt32(CustomStatus.Error);
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MappingCreationController.SaveOrgColMapping() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return status;
        //}
        /// <summary>
        /// ADDED ON 05-10-2021 BY Rishabh to get the details of college master.
        /// </summary>
        /// <returns></returns>
        public DataSet GetMappingData(int orgcolid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@OrgColId", orgcolid);
                ds = objSQLHelper.ExecuteDataSetSP("SP_GET_MAPORGANIZATIONCOLLEGE", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MappingCreationController.GetMappingData-> " + ex.ToString());
            }
            return ds;
        }
        /// <summary>
        /// ADDED ON 14-10-2021 BY Rishabh to get the details for Mapping Department.
        /// </summary>
        /// <returns></returns>
        public DataSet GetDepartmentList(int DepartmentId)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];
                ds = objSQLHelper.ExecuteDataSetSP("SP_GET_MAPDEPARTMENT_LIST", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("RFC-CC.BusinessLogicLayer.BusinessLogic.RFC_CONFIG.MappingController.GetDepartmentList-> " + ex.ToString());
            }
            return ds;
        }
        /// <summary>
        /// MODIFIED By Rishabh on 06/12/2021 to add new column -Organization Id
        /// </summary>
        /// <returns></returns>
        public int SaveCollegeDepartMapping(Mapping objM, string depids, string depnames)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[11];
                objParams[0] = new SqlParameter("@COL_DEPT_NO", objM.CollegeDepartmentId);
                objParams[1] = new SqlParameter("@COLLEGE_ID", objM.CollegeId);
                objParams[2] = new SqlParameter("@COLLEGE_NAME", objM.College_Name); //Added By Rishabh on 24/11/2021
                objParams[3] = new SqlParameter("@DEPTNO", depids);
                objParams[4] = new SqlParameter("@COLLEGE_DEPT_NAME", depnames);
                objParams[5] = new SqlParameter("@CREATEDBY", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                objParams[6] = new SqlParameter("@MODIFIEDBY", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                objParams[7] = new SqlParameter("@IPADDRESS", System.Web.HttpContext.Current.Session["ipAddress"].ToString());
                //objParams[7] = new SqlParameter("@ACTIVESTATUS", objM.ActiveStatus);
                objParams[8] = new SqlParameter("@MACADDRESS", System.Web.HttpContext.Current.Session["macAddress"]);
                objParams[9] = new SqlParameter("@ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])); //Added By Rishabh on 06/12/2021
                objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[10].Direction = ParameterDirection.Output;
                string a = "";
                string b = "";
                for (int i = 0; i < objParams.Length; i++)
                {
                    Type tp = objParams[i].GetType();
                    a += objParams[i] + "=" + objParams[i].Value + ",";
                }
                b = a;

                object ret = objSQLHelper.ExecuteNonQuerySP("sptblMapCollegeDepartment_Insert_Update", objParams, true);
                status = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("RFC-CC.BusinessLogicLayer.BusinessLogic.RFC_CONFIG.MappingCreationController.SaveCollegeDepartMapping() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
        /// <summary>
        /// ADDED ON 14-10-2021 BY Rishabh to get the details of Mapped Department.
        /// </summary>
        /// <returns></returns>
        public DataSet GetMappedDepartmentData(int coldepid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@ColgDepId", coldepid);
                objParams[1] = new SqlParameter("@P_ORGID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                ds = objSQLHelper.ExecuteDataSetSP("SP_GET_MAPPCOLLEGEDEPARTMENT", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("RFC-CC.BusinessLogicLayer.BusinessLogic.RFC_CONFIG.MappingController.GetMappedDepartmentData-> " + ex.ToString());
            }
            return ds;
        }
        /// <summary>
        /// Added by S.P 
        /// </summary>
        /// <param name="coldepid"></param>
        /// <returns></returns>
        public DataSet GetMappedDegreeData(int coldepid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@COLGDEPID", coldepid);
                ds = objSQLHelper.ExecuteDataSetSP("SP_GETMAPPEDDEGREEDATA", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("RFC-CC.BusinessLogicLayer.BusinessLogic.RFC_CONFIG.MappingController.GetMappedDegreeData-> " + ex.ToString());
            }
            return ds;
        }
        /// <summary>
        /// Added By S.P - 19/10/2021
        /// MODIFIED By Rishabh on 06/12/2021 to add new column -Organization Id
        /// </summary>
        /// <param name="objM"></param>
        /// <param name="degids"></param>
        /// <param name="degnames"></param>
        /// <returns></returns>
        public int SaveDegreeDeptMapping(Mapping objM, string degids, string degnames)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[11];
                objParams[0] = new SqlParameter("@COL_DEGREE_NO", objM.DepartmentId);
                objParams[1] = new SqlParameter("@COLLEGE_ID", objM.CollegeDepartmentId);
                objParams[2] = new SqlParameter("@DEGREENO", degids);
                objParams[3] = new SqlParameter("@COLLEGE_NAME", objM.College_Name); // Added By Rishabh on 24/11/2021
                //objParams[3] = new SqlParameter("@DegreeDepartmentName", degnames);
                objParams[4] = new SqlParameter("@CREATEDBY", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                objParams[5] = new SqlParameter("@MODIFIEDBY", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                objParams[6] = new SqlParameter("@IPADDRESS", System.Web.HttpContext.Current.Session["ipAddress"].ToString());
                objParams[7] = new SqlParameter("@ACTIVESTATUS", objM.ActiveStatus);
                objParams[8] = new SqlParameter("@MACADDRESS", System.Web.HttpContext.Current.Session["macAddress"]);
                objParams[9] = new SqlParameter("@ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])); //Added By Rishabh on 06/12/2021
                objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[10].Direction = ParameterDirection.Output;
                string a = "";
                string b = "";
                for (int i = 0; i < objParams.Length; i++)
                {
                    Type tp = objParams[i].GetType();
                    a += objParams[i] + "=" + objParams[i].Value + ",";
                }
                b = a;
                object ret = objSQLHelper.ExecuteNonQuerySP("SP_ACD_COLLEGE_DEGREE_INSERT_UPDATE", objParams, true);
                status = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("RFC-CC.BusinessLogicLayer.BusinessLogic.RFC_CONFIG.MappingController.SaveDegreeDeptMapping() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="colid"></param>
        /// <returns></returns>
        public DataSet GetDegMappingData(int colid)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@COLLEGE_ID", colid);
                objParams[1] = new SqlParameter("@P_ORGID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                ds = objSQLHelper.ExecuteDataSetSP("SP_GET_MAPPING_COL_DEG", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MappingController.GetDegMappingData-> " + ex.ToString());
            }
            return ds;
        }
        /// <summary>
        /// ADDED ON 07-10-2021 BY Rishabh to Get Degree master data.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetDegreeInfo()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];
                ds = objSQLHelper.ExecuteDataSetSP("[SP_GET_DEGREE_LIST]", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MappingCreationController.GetDegreeInfo-> " + ex.ToString());
            }
            return ds;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet GetColList()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];
                ds = objSQLHelper.ExecuteDataSetSP("SP_GET_COL_LIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MappingCreationController.GetColList() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        /// <summary>
        /// ADDED ON 18-10-2021 BY Rishabh to get the details for Mapping Branch.
        /// </summary>
        /// <returns></returns>
        public DataSet GetBranchList()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];
                ds = objSQLHelper.ExecuteDataSetSP("SP_GET_MAPBRANCH_LIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MappingController.GetBranchList() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        public DataSet GetDataToFillDropDownlist()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];
                ds = objSQLHelper.ExecuteDataSetSP("PKG_CONFIG_FILL_DROPDOWNLIST", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.OrganizationController.GetOrganizationById() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataSet GetData()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[0];
                ds = objSQLHelper.ExecuteDataSetSP("SP_GET_MENU", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MappingController.GetData() --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        /// <summary>
        /// MODIFIED By Rishabh on 06/12/2021 to add new column -Organization Id
        ///  MODIFIED By Rishabh on 29/11/2022 to add new column - FACULTYNO
        /// </summary>
        /// <returns></returns>
        public int SaveDegreeBranchMapping(Mapping objM, int facultyno)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]

                        //Add New Branch Type
                        {
                         new SqlParameter("@P_BRANCHNO", objM.BranchId),
                         new SqlParameter("@P_INTAKE1", objM.Intake_I),
                         new SqlParameter("@P_INTAKE2", objM.Intake_II),
                         new SqlParameter("@P_INTAKE3", objM.Intake_III),
                         new SqlParameter("@P_INTAKE4", objM.Intake_IV),
                         new SqlParameter("@P_INTAKE5", objM.Intake_V),
                         new SqlParameter("@P_DURATION", objM.Duration),
                         new SqlParameter("@P_CODE", objM.ShortName),
                         new SqlParameter("@P_BRCODE", objM.Branch_Code), //Added by Irfan Shaikh on 20190413
                         new SqlParameter("@P_UGPGOT", objM.Programme_Type),
                         new SqlParameter("@P_DEGREENO", objM.DegreeDepartmentId),
                         new SqlParameter("@P_DEPTNO", objM.DepartmentId),
                         new SqlParameter("@P_COLLEGE_CODE", objM.College_Code),
                         new SqlParameter("@P_COLLEGE_ID", objM.CollegeId),
                         new SqlParameter("@P_COLLEGE_NAME", objM.College_Name), //Added By Rishabh On 24/11/2021
                         new SqlParameter("@P_ENGSTATUS", objM.Eng_Status),
                         new SqlParameter("@P_SCHOOL_COLLEGE_CODE", objM.College_Code),
                         new SqlParameter("@P_AICTE_NONAICTE", objM.College_Type),
                         new SqlParameter("@P_CREATEDBY", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"])),
                         new SqlParameter("@P_IPADDRESS", System.Web.HttpContext.Current.Session["ipAddress"].ToString()),
                         new SqlParameter("@ORGANIZATIONID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"])), //Added By Rishabh on 06/12/2021
                         new SqlParameter("@P_ISSPECIALISATION", objM.IsSpecialisation),
                         new SqlParameter("@P_COREBRANCHNO", objM.CoreBranchNo),
                         new SqlParameter("@P_FACUILTYNO", facultyno),
                         new SqlParameter("@P_OUT",SqlDbType.Int)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                string a = "";
                string b = "";
                for (int i = 0; i < sqlParams.Length; i++)
                {
                    Type tp = sqlParams[i].GetType();
                    a += sqlParams[i] + "=" + sqlParams[i].Value + ",";
                }
                b = a;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INS_BRANCH", sqlParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001" && obj.ToString() == "1")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                else if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001" && obj.ToString() == "2")
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
                else
                    status = Convert.ToInt32(CustomStatus.Error);

            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.AddBranchType() --> " + ex.Message + " " + ex.StackTrace);
            }

            return status;
        }


        /// <summary>
        /// ADDED ON 18-10-2021 BY Rishabh to get the details of Mapped Branches.
        /// </summary>
        /// <returns></returns>
        public DataSet GetMappedBranchData(int COLLEGE_ID, int Degreeno, int CDBNO)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_COLLEGE_ID", COLLEGE_ID);
                objParams[1] = new SqlParameter("@P_DEGREENO", Degreeno);
                objParams[2] = new SqlParameter("@P_CDBNO", CDBNO);
                objParams[3] = new SqlParameter("@P_ORGID", Convert.ToInt32(System.Web.HttpContext.Current.Session["OrgId"]));
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_ALL_BRANCH", objParams);

            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("RFC-CC.BusinessLogicLayer.BusinessLogic.RFC_CONFIG.MappingController.GetMappedBranchData-> " + ex.ToString());
            }
            return ds;
        }

        //New Method Added By Amit k on 2021 Nov 13
        public Dictionary<int, string> SaveOrgColMapping(Mapping objOrgCol, string colids, string colnames)
        {
            int status = 0;
            Dictionary<int, string> RetResult = new Dictionary<int, string>();

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                objParams = new SqlParameter[10];
                objParams[0] = new SqlParameter("@OrganizationCollegeId", objOrgCol.OrganizationCollegeId);
                objParams[1] = new SqlParameter("@OrganizationId", objOrgCol.OrganizationId);
                objParams[2] = new SqlParameter("@CollegeId", colids);
                objParams[3] = new SqlParameter("@OrganizationCollegeName", colnames);
                objParams[4] = new SqlParameter("@CreatedBy", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                objParams[5] = new SqlParameter("@ModifiedBy", Convert.ToInt32(System.Web.HttpContext.Current.Session["userno"]));
                objParams[6] = new SqlParameter("@IPAddress", System.Web.HttpContext.Current.Session["ipAddress"].ToString());
                objParams[7] = new SqlParameter("@ActiveStatus", objOrgCol.ActiveStatus);
                objParams[8] = new SqlParameter("@MACAddress", System.Web.HttpContext.Current.Session["macAddress"]);
                //objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                //objParams[9].Direction = ParameterDirection.Output;
                objParams[9] = new SqlParameter("@P_OUT", SqlDbType.NVarChar, 125);
                objParams[9].Direction = ParameterDirection.Output;

                string a = "";
                string b = "";
                for (int i = 0; i < objParams.Length; i++)
                {
                    Type tp = objParams[i].GetType();
                    a += objParams[i] + "=" + objParams[i].Value + ",";
                }
                b = a;


                object ret = objSQLHelper.ExecuteNonQuerySP("sptblMapOrganizationCollege_Insert_Update", objParams, true);



                string ss = ret.ToString();
                string[] SSdata = ss.Split('|');

                RetResult.Add(Convert.ToInt32(SSdata[0]), SSdata[1]);

                //status = Convert.ToInt32(ret);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MappingCreationController.SaveOrgColMappingNew() --> " + ex.Message + " " + ex.StackTrace);
            }
            return RetResult;
        }
    }
}
