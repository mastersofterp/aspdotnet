using System;
using System.Data;
using System.Data.SqlClient;
using IITMS.SQLServer.SQLDAL;

using IITMS.UAIMS.BusinessLayer.BusinessEntities;

namespace IITMS.UAIMS.BusinessLayer.BusinessLogic
{
    public class MappingController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        public int AddEntrance(int Qualino, double min_Score, int admType, int programmeType, string marks_Per, int degree, double CGPA, int orgId) //
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_QUALINO", Qualino);
                objParams[1] = new SqlParameter("@P_DEGREENO", degree);
                objParams[2] = new SqlParameter("@P_ADMTYPE", admType);
                //objParams[2] = new SqlParameter("@P_BRANCHNO", Branch);//Added By Dileep kare on 05.07.2021
                objParams[3] = new SqlParameter("@P_MIN_SCORE", min_Score); // Added By Nikhil L. on 09/07/2021
                //objParams[4] = new SqlParameter("@P_BRANCHNO", branch);
                //objParams[5] = new SqlParameter("@P_PROGRAMME_CODE", programmeCode);
                objParams[4] = new SqlParameter("@P_PROGRAMME_TYPE", programmeType);
                objParams[5] = new SqlParameter("@P_MARKS_PER", marks_Per);
                objParams[6] = new SqlParameter("@P_CGPA", CGPA);               // Added by Nikhil L. on 23/08/2021 to add CGPA for PG.
                objParams[7] = new SqlParameter("@P_ORGANIZATION_ID", orgId);               // Added by Nikhil L. on 28/03/2022 to organization Id.


                objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[8].Direction = System.Data.ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ENTRE_INSERT_ENTREMAPPING", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    retStatus = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32((CustomStatus.Error));
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MappingController.AddDocument-> " + ex.ToString());
            }
            return retStatus;
        }
        public int deleteEntrance(int Entre_degreeno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_ENTRE_DEGREE_NO", Entre_degreeno);
                objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[1].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ENTRE_DELETE_ENTREMAPPING", objParams, true);

                if (Convert.ToInt32(obj) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                if (Convert.ToInt32(obj) != 99)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.deleteattendance-> " + ex.ToString());
            }
            return retStatus;
        }
        public int AddDocument(int Docno, int Degree, int admType, int activeStatus, int orgId)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_DOCNO", Docno);
                objParams[1] = new SqlParameter("@P_DEGREENO", Degree);
                objParams[2] = new SqlParameter("@P_ADMTYPE", admType);
                objParams[3] = new SqlParameter("@P_ACTIVE_STATUS", activeStatus);
                objParams[4] = new SqlParameter("@P_ORGANIZATION_ID", orgId);
                objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[5].Direction = System.Data.ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_DOC_INSERT_DOCMAPPING", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    retStatus = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32((CustomStatus.Error));
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MappingController.AddDocument-> " + ex.ToString());
            }
            return retStatus;
        }
        public int deleteDocument(int doc_degreeno)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_DOC_DEGREE_NO", doc_degreeno);
                objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[1].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_DOC_DELETE_DEGREEMAPPING", objParams, true);

                if (Convert.ToInt32(obj) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                if (Convert.ToInt32(obj) != 99)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                }

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.deleteattendance-> " + ex.ToString());
            }
            return retStatus;
        }
        public int AddDegree(int degree,int colg)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;
                
                     objParams = new SqlParameter[3];
                            objParams[0] = new SqlParameter("@P_DEGREENO", degree);
                            objParams[1] = new SqlParameter("@P_COLLEGE_ID", colg);

                            objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                            objParams[2].Direction = System.Data.ParameterDirection.Output;

                            object obj = objSQLHelper.ExecuteNonQuerySP("PKG_COLG_INSERT_DEGREEMAPPING", objParams, true);

                            if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.Error);
              

            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32((CustomStatus.Error));
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.ChangePassword-> " + ex.ToString());
            }
            return retStatus;
        }
        public int AddDept(int dept, int colg)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_DEPTNO", dept);
                objParams[1] = new SqlParameter("@P_COLLEGE_ID", colg);

                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[2].Direction = System.Data.ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_COLG_INSERT_DEPTMAPPING", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                else
                    retStatus = Convert.ToInt32(CustomStatus.Error);


            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32((CustomStatus.Error));
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.User_AccController.ChangePassword-> " + ex.ToString());
            }
            return retStatus;
        }
        public int deleteRecord(int col_degreeno)
        {

            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_COLL_DEGREE_NO", col_degreeno);

                objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[1].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_COLG_DELETE_DEGREEMAPPING", objParams, true);


                if (Convert.ToInt32(obj) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);



                if (Convert.ToInt32(obj) != 99)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                }

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.deleteattendance-> " + ex.ToString());
            }
            return retStatus;
        }
        public int deleteDeptRecord(int col_deptno)
        {

            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_COLL_DEPT_NO", col_deptno);

                objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[1].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_COLG_DELETE_DEPTMAPPING", objParams, true);


                if (Convert.ToInt32(obj) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);



                if (Convert.ToInt32(obj) != 99)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                }

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.deleteattendance-> " + ex.ToString());
            }
            return retStatus;
        }

        public int AddBranchType(Branch objBranchType)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]

                        //Add New Branch Type
                        {
                         new SqlParameter("@P_BRANCHNO", objBranchType.BranchNo),
                         new SqlParameter("@P_INTAKE1", objBranchType.Intake1),
                         new SqlParameter("@P_INTAKE2", objBranchType.Intake2),
                         new SqlParameter("@P_INTAKE3", objBranchType.Intake3),
                         new SqlParameter("@P_INTAKE4", objBranchType.Intake4),
                         new SqlParameter("@P_INTAKE5", objBranchType.Intake5),
                         new SqlParameter("@P_DURATION", objBranchType.Duration),
                         new SqlParameter("@P_CODE", objBranchType.Code),
                         new SqlParameter("@P_BRCODE", objBranchType.BranchCode), //Added by Irfan Shaikh on 20190413
                         new SqlParameter("@P_UGPGOT", objBranchType.Ugpgot),
                         new SqlParameter("@P_DEGREENO", objBranchType.DegreeNo),
                         new SqlParameter("@P_DEPTNO", objBranchType.DeptNo),
                         new SqlParameter("@P_COLLEGE_CODE", objBranchType.CollegeCode),
                         new SqlParameter("@P_COLLEGE_ID", objBranchType.CollegeID),
                         new SqlParameter("@P_ENGSTATUS", objBranchType.EnggStatus),
                         new SqlParameter("@P_SCHOOL_COLLEGE_CODE", objBranchType.SCHOOL_COLLEGE_CODE),
                         new SqlParameter("@P_AICTE_NONAICTE", objBranchType.AICTE_NONAICTE),
                         new SqlParameter("@P_OUT", objBranchType.BranchNo)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INS_BRANCH", sqlParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
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

        public DataSet GetAllBranchType( int CollegeID)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_COLLEGE_ID", CollegeID);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_ALL_BRANCH", objParams);

            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.GetAllBranchTypes-> " + ex.ToString());
            }
            return ds;
        }

        public int deletebranchRecord(int col_Branno)
        {

            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_CDBNO", col_Branno);

                objParams[1] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[1].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DELETE_BRANCH_MAPPING", objParams, true);

                if (Convert.ToInt32(obj) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                if (Convert.ToInt32(obj) != 99)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.deleteattendance-> " + ex.ToString());
            }
            return retStatus;
        }

        public int EditbranchRecord(int col_Branno, int DURATION, string CODE, Branch objBranchType)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[16];
                objParams[0] = new SqlParameter("@P_CDBNO", col_Branno);
                objParams[1] = new SqlParameter("@P_DURATION", DURATION);
                objParams[2] = new SqlParameter("@P_CODE", CODE);
                objParams[3] = new SqlParameter("@P_BRCODE", objBranchType.BranchCode); //// Added by Irfan Shaikh on 20190413
                objParams[4] = new SqlParameter("@P_INTAKE1", objBranchType.Intake1);
                objParams[5] = new SqlParameter("@P_INTAKE2", objBranchType.Intake2);
                objParams[6] = new SqlParameter("@P_INTAKE3", objBranchType.Intake3);
                objParams[7] = new SqlParameter("@P_INTAKE4", objBranchType.Intake4);
                objParams[8] = new SqlParameter("@P_INTAKE5", objBranchType.Intake5);
                objParams[9] = new SqlParameter("@P_UGPGOT", objBranchType.Ugpgot);
                objParams[10] = new SqlParameter("@P_BRANCHNO", objBranchType.BranchNo);
                objParams[11] = new SqlParameter("@P_DEPTNO", objBranchType.DeptNo);
                objParams[12] = new SqlParameter("@P_ENGSTATUS", objBranchType.EnggStatus);
                //Added Mahesh on Dated 16-02-2021
                objParams[13] = new SqlParameter("@P_SCHOOL_COLLEGE_CODE", objBranchType.SCHOOL_COLLEGE_CODE);
                objParams[14] = new SqlParameter("@P_AICTE_NONAICTE", objBranchType.AICTE_NONAICTE);

                objParams[15] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[15].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPDATE_BRANCH_MAPPING", objParams, true);

                if (Convert.ToInt32(obj) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                if (Convert.ToInt32(obj) == 2)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.deleteattendance-> " + ex.ToString());
            }
            return retStatus;
        }

        public DataSet GetAllBranchType(int CollegeID, int degreeno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_COLLEGE_ID", CollegeID);
                objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_ALL_BRANCH", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.GetAllBranchTypes-> " + ex.ToString());
            }
            return ds;
        }

        //added by reena on 6-12-16
        public int AddExamCenter(Branch objBranchType)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]

                        //Add New Branch Type
                        {
                        
                         new SqlParameter("@P_SESSIONNO", objBranchType.SessionNo),
                         new SqlParameter("@P_CENTER_ID", objBranchType.CenterNo),
                         new SqlParameter("@P_DEGREENO",   objBranchType.Degree),
                         new SqlParameter("@P_OUT", SqlDbType.Int)
                        };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.Output;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INS_EXAN_CENTER_DEGREEWISE", sqlParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
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

        //ADDED BY REENA
        public int deleteexamcenterRecord(int sessionno, int centerno)
        {

            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_CENTER_ID", centerno);

                objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[2].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_DELETE_EXAM_CENTER_MAPPING", objParams, true);

                if (Convert.ToInt32(obj) == -99)
                    retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);

                if (Convert.ToInt32(obj) != 99)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                }
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.deleteattendance-> " + ex.ToString());
            }
            return retStatus;
        }
        /// <summary>
        /// Added by Swapnil - 12082019
        /// </summary>
        /// <returns></returns>
        public int AddDistrict(int districtno, string district, int state, string coll_code, int flag)
        {
            int retStatus = Convert.ToInt32(CustomStatus.Others);

            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                SqlParameter[] objParams = new SqlParameter[6];
                objParams[0] = new SqlParameter("@P_DISTRICTNO", districtno);
                objParams[1] = new SqlParameter("@P_DISTRICT", district);
                objParams[2] = new SqlParameter("@P_STATENO", state);
                objParams[3] = new SqlParameter("@P_COLLEGE_ID", coll_code);
                objParams[4] = new SqlParameter("@P_FLAG", flag);
                objParams[5] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[5].Direction = ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INS_DISTRICT", objParams, true);

                if (Convert.ToInt32(obj) == 1)
                {
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                }
                else if (Convert.ToInt32(obj) == 2)
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
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentAttendanceController.deleteattendance-> " + ex.ToString());
            }
            return retStatus;
        }
        /// <summary>
        /// Added by Swapnil - 12082019
        /// </summary>
        /// <returns></returns>

        public SqlDataReader GetDistrictByNo(int districtno)
        {
            SqlDataReader dr = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[] { new SqlParameter("@P_DISTRICTNO", districtno) };

                dr = objSQLHelper.ExecuteReaderSP("PKG_ACAD_DISTRICT_BY_ID", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetAllDistrict() --> " + ex.Message + " " + ex.StackTrace);
            }
            return dr;
        }

        /// <summary>
        /// Added by Swapnil - 12082019
        /// </summary>
        /// <returns></returns>

        public DataSet GetAllDistrict()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);

                ds = objSQLHelper.ExecuteDataSet("PKG_ACAD_ALL_DISTRICT");
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.GetAllDistrict-> " + ex.ToString());
            }
            return ds;
        }
        //added by reena 
        public DataSet GetAllcenterDegreewise(int sessionno, int center_id)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[2];
                objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                objParams[1] = new SqlParameter("@P_CENTER_ID", center_id);

                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_CENTERDEGREEWISE", objParams);
            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.GetAllBranchTypes-> " + ex.ToString());
            }
            return ds;
        }
        public DataSet GetDocumentMapping()
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]

                        //Add New Branch Type
                        {
                        };

                ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_DOC_MAPPING_DETAILS_OA", sqlParams);



            }
            catch (Exception ex)
            {
                //status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.AddBranchType() --> " + ex.Message + " " + ex.StackTrace);
            }

            return ds;
        }
        public int UpdateEntrance(int Entr_Degree_no, int Qualino, double min_Score, int admType, int programmeType, string marks_Per, int degree, double CGPA) //, 
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[9];
                objParams[0] = new SqlParameter("@P_ENTR_DEGREE_NO", Entr_Degree_no);
                objParams[1] = new SqlParameter("@P_QUALINO", Qualino);
                objParams[2] = new SqlParameter("@P_DEGREENO", degree);
                //objParams[3] = new SqlParameter("@P_BRANCHNO", Branch);
                objParams[3] = new SqlParameter("@P_ADMTYPE", admType);

                objParams[4] = new SqlParameter("@P_MIN_SCORE", min_Score); // Added By Nikhil L. on 09/07/2021
                //objParams[5] = new SqlParameter("@P_BRANCHNO", branch);
                //objParams[6] = new SqlParameter("@P_PROGRAMME_CODE", programmeCode);
                objParams[5] = new SqlParameter("@P_PROGRAMME_TYPE", programmeType);
                objParams[6] = new SqlParameter("@P_MARKS_PER", marks_Per);
                objParams[7] = new SqlParameter("@P_CGPA", CGPA);               // Added by Nikhil L. on 23/08/2021 to update CGPA for PG.
                objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[8].Direction = System.Data.ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ENTRE_UPDATE_ENTREMAPPING", objParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                else
                    retStatus = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32((CustomStatus.Error));
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MappingController.UpdateEntrance-> " + ex.ToString());
            }
            return retStatus;
        }
        public DataSet GetQualifyList(int admBatch, int programmeType, int degree)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_ADMTYPE", admBatch);
                objParams[1] = new SqlParameter("@P_DEGREENO", degree);
                objParams[2] = new SqlParameter("@P_PROGRAMME_TYPE", programmeType);


                ds = objSQLHelper.ExecuteDataSetSP("PKG_SP_GET_QUALIFY_LIST", objParams);

            }
            catch (Exception ex)
            {
                return ds;
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MappingController.GetQualifyList()-> " + ex.ToString());
            }
            return ds;
        }

        public int editUserDetails(int intakeno, int studylevel, int mainuser, Config objConfig, int sr_no, int Uano, Access_Link objAL, string subUser)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[8];
                objParams[0] = new SqlParameter("@P_ADMBATCH", intakeno);
                objParams[1] = new SqlParameter("@P_UGPGOT", studylevel);
                objParams[2] = new SqlParameter("@P_MAINUSER_UA_NO", mainuser);
                objParams[3] = new SqlParameter("@P_SUBUSER_UA_NO", subUser);
                objParams[4] = new SqlParameter("@P_SR_NO", sr_no);
                objParams[5] = new SqlParameter("@P_CREATEDBY", Uano);
                objParams[6] = new SqlParameter("@P_ACTIVE", objAL.chklinkstatus);
                objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[7].Direction = System.Data.ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_ACD_LEAD_ALLOTMENT", objParams, true);

                if (obj != null && obj.ToString() == "1")
                    retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32((CustomStatus.Error));
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MappingController.editUserDetails-> " + ex.ToString());
            }
            return retStatus;
        }
        public int AddUserDetails(int intakeno, int studylevel, int mainuser, Config objConfig, int Uano, Access_Link objAL, string subUser)
        {
            int retStatus = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[7];
                objParams[0] = new SqlParameter("@P_ADMBATCH", intakeno);
                objParams[1] = new SqlParameter("@P_UGPGOT", studylevel);
                objParams[2] = new SqlParameter("@P_MAINUSER_UA_NO", mainuser);
                objParams[3] = new SqlParameter("@P_SUBUSER_UA_NO", subUser);
                objParams[4] = new SqlParameter("@P_CREATEDBY", Uano);
                objParams[5] = new SqlParameter("@P_ACTIVE", objAL.chklinkstatus);
                //objParams[4] = new SqlParameter("@P_ACTIVE", chklinkstatus);


                //objParams[0] = new SqlParameter("@P_DEGREENO", objcon.DegreeNoS);
                objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                objParams[6].Direction = System.Data.ParameterDirection.Output;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ADD_ACD_LEAD_COUNSELOR_ALLOTMENT", objParams, true);

                if (obj != null && obj.ToString() == "1")
                    retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
            }
            catch (Exception ex)
            {
                retStatus = Convert.ToInt32((CustomStatus.Error));
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.MappingController.AddUserDetails-> " + ex.ToString());
            }
            return retStatus;
        }
        //-----------------------------------------------------------
        /// <summary>
        /// Added by Nikhil L. on 05-04-2022 for lead module.
        /// </summary>
        /// <param name="srno"></param>
        /// <param name="Commandtype"></param>
        /// <returns></returns>
        public DataSet GetUserDetails(int srno, int Commandtype)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter("@P_SR_NO", srno);
                sqlParam[1] = new SqlParameter("@P_COMMAND_TYPE", Commandtype);
                ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GET_LEAD_ALLOTMENT_MAPPING_WITH_LISTVIEW", sqlParam);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

    }

}
