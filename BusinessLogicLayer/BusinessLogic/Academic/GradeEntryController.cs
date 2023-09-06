//======================================================================================
// PROJECT NAME  : UAIMS
// MODULE NAME   : ACADEMIC
// PAGE NAME     : GRADE ENTRY CONTROLLER
// CREATION DATE : 20-JUNE-2014
// CREATED BY    : VIJAY PAUNIKAR
// MODIFIED DATE :
// MODIFIED DESC :
//======================================================================================

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;
using IITMS.SQLServer.SQLDAL;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities;
using IITMS.UAIMS.BusinessLogicLayer.BusinessEntities.Academic;

namespace BusinessLogicLayer.BusinessLogic.Academic
{
    public class GradeEntryController
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

        //public int AddGradeEntry(GradeEntry objGradeEntry)
        //{
        //    int status = 0;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[]
        //         {
        //             //new SqlParameter("@P_GRADENO",objGradeEntry.GradeNo),
        //             new SqlParameter("@P_GRADE", objGradeEntry.Grade),
        //             new SqlParameter("@P_GRADE_POINT", objGradeEntry.GradePoint),
        //             new SqlParameter("@P_COLLEGE_CODE", objGradeEntry.CollegeCode),
        //             new SqlParameter("@P_SUBID", objGradeEntry.Subid),
        //             new SqlParameter("@P_MAXMARK", objGradeEntry.MaxMark),
        //             new SqlParameter("@P_MINMARK", objGradeEntry.MinMark),
        //             new SqlParameter("@P_DEGREENO", objGradeEntry.DegreeNo),
        //             new SqlParameter("@P_GRADE_TYPE", objGradeEntry.GradeType),
        //             new SqlParameter("@P_GRADE_DESC", objGradeEntry.GradeDesc),
        //             new SqlParameter("@P_GRADENO", status)
        //         };
        //        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

        //        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GRADE_INSERT", sqlParams, true);

        //        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
        //            status = Convert.ToInt32(CustomStatus.RecordSaved);
        //        else
        //            status = Convert.ToInt32(CustomStatus.Error);
        //    }
        //    catch (Exception ex)
        //    {
        //        status = Convert.ToInt32(CustomStatus.Error);
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.AddBatch() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return status;
        //}

        /// <summary>
        /// Update by Rita Munde....17012020
        /// </summary>
        /// <param name="objGradeEntry"></param>
        /// <returns></returns>
        //For Insert
        public int AddGradeEntry(GradeEntry objGradeEntry, int OrgID, bool Status, int Gradeno_New, int SubType)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_GRADENO",objGradeEntry.GradeNo),
                    new SqlParameter("@P_GRADE", objGradeEntry.Grade),
                    new SqlParameter("@P_GRADE_POINT", objGradeEntry.GradePoint),
                    new SqlParameter("@P_COLLEGE_CODE", objGradeEntry.CollegeCode),
                   // new SqlParameter("@P_SUBID", objGradeEntry.Subid),
                    new SqlParameter("@P_MAXMARK", objGradeEntry.MaxMark),
                    new SqlParameter("@P_MINMARK", objGradeEntry.MinMark),
                   // new SqlParameter("@P_DEGREENO", objGradeEntry.DegreeNo),
                    new SqlParameter("@P_GRADE_TYPE", objGradeEntry.GradeType),
                    new SqlParameter("@P_GRADE_DESC", objGradeEntry.GradeDesc),
                    new SqlParameter("@P_UGPGOT", objGradeEntry.UGPGOTNO),
                    new SqlParameter("@P_ORG_ID", OrgID),
                    new SqlParameter("@P_STATUS", Status),
                    new SqlParameter("@P_Result", objGradeEntry.Result),
                    new SqlParameter("@P_GRADENO_NEW",Gradeno_New),
                    new SqlParameter("@P_SUBTYPE",SubType),
                    new SqlParameter("@P_OUT", status)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                //object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GRADE_INSERT", sqlParams, true);
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GRADE_INSERT_DEMO", sqlParams, true);

                if (Convert.ToInt32(obj) == 2)
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
                else
                    status = Convert.ToInt32(CustomStatus.RecordSaved);

                //if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                //    status = Convert.ToInt32(CustomStatus.RecordSaved);
                //else
                //    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.AddBatch() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }


        //public DataSet GetAllGradeEntry()
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
        //        SqlParameter[] objParams = new SqlParameter[0];

        //     // ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GRADE_ENTRY_GET_ALL", objParams);
        //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_GRADE_ENTRY_NEW", objParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetAllBatch() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}


        public DataSet GetAllGradeEntry(int Gradetype, int UGPG, int SubType)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[3];
                objParams[0] = new SqlParameter("@P_GRADE_TYPE", Gradetype);
                objParams[1] = new SqlParameter("@P_UGPGOT", UGPG);
                objParams[2] = new SqlParameter("@P_SUBTYPE", SubType);

                ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_GRADE_ENTRY_NEW", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GradeController.GetStudentsList_For_I_Grade --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        /// <summary>
        /// Added by Dileep Kare
        /// Date:25-01-2020
        /// </summary>
        /// <param name="objGradeEntry"></param>
        /// <returns></returns>

        public int UpdateGradeEntry(GradeEntry objGradeEntry, int OrgID, bool Status, int Gradeno_New)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_GRADENO",objGradeEntry.GradeNo),
                    new SqlParameter("@P_GRADE", objGradeEntry.Grade),
                    new SqlParameter("@P_GRADE_POINT", objGradeEntry.GradePoint),
                    new SqlParameter("@P_COLLEGE_CODE", objGradeEntry.CollegeCode),
                   // new SqlParameter("@P_SUBID", objGradeEntry.Subid),
                    new SqlParameter("@P_MAXMARK", objGradeEntry.MaxMark),
                    new SqlParameter("@P_MINMARK", objGradeEntry.MinMark),
                   // new SqlParameter("@P_DEGREENO", objGradeEntry.DegreeNo),
                    new SqlParameter("@P_GRADE_TYPE", objGradeEntry.GradeType),
                    new SqlParameter("@P_GRADE_DESC", objGradeEntry.GradeDesc),
                    new SqlParameter("@P_UGPGOT", objGradeEntry.UGPGOTNO),
                    new SqlParameter("@P_ORG_ID", OrgID),
                    new SqlParameter("@P_STATUS", Status),
                    new SqlParameter("@P_Result", objGradeEntry.Result),
                    new SqlParameter("@P_GRADENO_NEW",Gradeno_New),
                    new SqlParameter("@P_OUT", status)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GRADE_UPDATED", sqlParams, true);

                if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
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

        ///<summery>
        ///Added By Dileep Kare
        ///Date:25-01-2020
        ///</summery>
        public SqlDataReader GetGradebyGradeno(int Gradeno)
        {
            SqlDataReader dr = null;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = new SqlParameter[1];
                objParams[0] = new SqlParameter("@P_GRADENO", Gradeno);

                dr = objSQLHelper.ExecuteReaderSP("PKG_GET_GRADE_BY_GRADENO", objParams);

            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.GetAllBatch() --> " + ex.Message + " " + ex.StackTrace);
            }
            return dr;
        }

        //public int AddGradeEntry(GradeEntry objGradeEntry, int OrgID, bool Status, int Gradeno_New)
        //{
        //    int status = 0;
        //    try
        //    {
        //        SQLHelper objSQLHelper = new SQLHelper(connectionString);
        //        SqlParameter[] sqlParams = new SqlParameter[]
        //        {
        //            new SqlParameter("@P_GRADENO",objGradeEntry.GradeNo),
        //            new SqlParameter("@P_GRADE", objGradeEntry.Grade),
        //            new SqlParameter("@P_GRADE_POINT", objGradeEntry.GradePoint),
        //            new SqlParameter("@P_COLLEGE_CODE", objGradeEntry.CollegeCode),
        //           // new SqlParameter("@P_SUBID", objGradeEntry.Subid),
        //            new SqlParameter("@P_MAXMARK", objGradeEntry.MaxMark),
        //            new SqlParameter("@P_MINMARK", objGradeEntry.MinMark),
        //           // new SqlParameter("@P_DEGREENO", objGradeEntry.DegreeNo),
        //            new SqlParameter("@P_GRADE_TYPE", objGradeEntry.GradeType),
        //            new SqlParameter("@P_GRADE_DESC", objGradeEntry.GradeDesc),
        //            new SqlParameter("@P_UGPGOT", objGradeEntry.UGPGOTNO),
        //            new SqlParameter("@P_ORG_ID", OrgID),
        //            new SqlParameter("@P_STATUS", Status),
        //            new SqlParameter("@P_Result", objGradeEntry.Result),
        //            new SqlParameter("@P_GRADENO_NEW",Gradeno_New),
        //            new SqlParameter("@P_OUT", status)
        //        };
        //        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

        //        //object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GRADE_INSERT", sqlParams, true);
        //        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GRADE_INSERT_DEMO", sqlParams, true);

        //        if (Convert.ToInt32(obj) == 2)
        //            status = Convert.ToInt32(CustomStatus.RecordUpdated);
        //        else
        //            status = Convert.ToInt32(CustomStatus.RecordSaved);

        //        //if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
        //        //    status = Convert.ToInt32(CustomStatus.RecordSaved);
        //        //else
        //        //    status = Convert.ToInt32(CustomStatus.Error);
        //    }
        //    catch (Exception ex)
        //    {
        //        status = Convert.ToInt32(CustomStatus.Error);
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.AddBatch() --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return status;
        //}

        //public DataSet GetAllGradeEntry(int Gradetype, int UGPG)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        SQLHelper objSqlHelper = new SQLHelper(connectionString);
        //        SqlParameter[] objParams = null;

        //        objParams = new SqlParameter[2];
        //        objParams[0] = new SqlParameter("@P_GRADE_TYPE", Gradetype);
        //        objParams[1] = new SqlParameter("@P_UGPGOT", UGPG);

        //        ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_GRADE_ENTRY_NEW", objParams);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GradeController.GetStudentsList_For_I_Grade --> " + ex.Message + " " + ex.StackTrace);
        //    }
        //    return ds;
        //}

        //LockGradeEntry
        public int LockGradeEntry(GradeEntry objGradeEntry, int OrgID, int Gradeno_New, int SubType)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                              {    
                                   new SqlParameter("@P_GRADE_TYPE", objGradeEntry.GradeType),
                                   new SqlParameter("@P_UGPGOT", objGradeEntry.UGPGOTNO),
                                   new SqlParameter("@P_GRADENO_NEW",Gradeno_New),
                                   new SqlParameter("@P_ORG_ID", OrgID),
                                    new SqlParameter("@P_SUBTYPE", SubType),
                                   new SqlParameter("@P_OUT", status)
                                           
                              };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GRADE_LOCK", sqlParams, true);

                if (Convert.ToInt32(obj) == 1)
                {
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                }

                return status;

            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.LockGradeEntry() --> " + ex.Message + " " + ex.StackTrace);
            }
            //return status;
        }

        //UnLockGradeEntry
        public int UnLockGradeEntry(GradeEntry objGradeEntry, int OrgID, int Gradeno_New, int SubType)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                              {    
                                   new SqlParameter("@P_GRADE_TYPE", objGradeEntry.GradeType),
                                   new SqlParameter("@P_UGPGOT", objGradeEntry.UGPGOTNO),
                                   new SqlParameter("@P_GRADENO_NEW",Gradeno_New),
                                   new SqlParameter("@P_ORG_ID", OrgID),
                                   new SqlParameter("@P_SUBTYPE", SubType),
                                   new SqlParameter("@P_OUT", status)
                                           
                              };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GRADE_UNLOCK", sqlParams, true);

                if (Convert.ToInt32(obj) == 1)
                {
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                }

                return status;

            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.UnLockGradeEntry() --> " + ex.Message + " " + ex.StackTrace);
            }
            // return status;
        }

        public int AddGradeEntry(GradeEntry objGradeEntry, int OrgID, bool Status, int Gradeno_New, int SubType, int CollegeId)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_GRADENO",objGradeEntry.GradeNo),
                    new SqlParameter("@P_GRADE", objGradeEntry.Grade),
                    new SqlParameter("@P_GRADE_POINT", objGradeEntry.GradePoint),
                    new SqlParameter("@P_COLLEGE_CODE", objGradeEntry.CollegeCode),
                   // new SqlParameter("@P_SUBID", objGradeEntry.Subid),
                    new SqlParameter("@P_MAXMARK", objGradeEntry.MaxMark),
                    new SqlParameter("@P_MINMARK", objGradeEntry.MinMark),
                   // new SqlParameter("@P_DEGREENO", objGradeEntry.DegreeNo),
                    new SqlParameter("@P_GRADE_TYPE", objGradeEntry.GradeType),
                    new SqlParameter("@P_GRADE_DESC", objGradeEntry.GradeDesc),
                    new SqlParameter("@P_UGPGOT", objGradeEntry.UGPGOTNO),
                    new SqlParameter("@P_ORG_ID", OrgID),
                    new SqlParameter("@P_STATUS", Status),
                    new SqlParameter("@P_Result", objGradeEntry.Result),
                    new SqlParameter("@P_GRADENO_NEW",Gradeno_New),
                    new SqlParameter("@P_SUBTYPE",SubType),
                    new SqlParameter("@P_COLLEGE_ID",CollegeId),
                    new SqlParameter("@P_OUT", status)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                //object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GRADE_INSERT", sqlParams, true);
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GRADE_INSERT_DEMO", sqlParams, true);

                if (Convert.ToInt32(obj) == 2)
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
                else
                    status = Convert.ToInt32(CustomStatus.RecordSaved);

                //if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                //    status = Convert.ToInt32(CustomStatus.RecordSaved);
                //else
                //    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.AddBatch() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }


        //LockGradeEntry
        public int LockGradeEntry(GradeEntry objGradeEntry, int OrgID, int Gradeno_New, int SubType, int CollegeId)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                              {    
                                   new SqlParameter("@P_GRADE_TYPE", objGradeEntry.GradeType),
                                   new SqlParameter("@P_UGPGOT", objGradeEntry.UGPGOTNO),
                                   new SqlParameter("@P_GRADENO_NEW",Gradeno_New),
                                   new SqlParameter("@P_ORG_ID", OrgID),
                                    new SqlParameter("@P_SUBTYPE", SubType),
                                    new SqlParameter("@P_COLLEGE_ID",CollegeId),
                                   new SqlParameter("@P_OUT", status)
                                           
                              };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GRADE_LOCK", sqlParams, true);

                if (Convert.ToInt32(obj) == 1)
                {
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                }

                return status;

            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.LockGradeEntry() --> " + ex.Message + " " + ex.StackTrace);
            }
            //return status;
        }

        //UnLockGradeEntry
        public int UnLockGradeEntry(GradeEntry objGradeEntry, int OrgID, int Gradeno_New, int SubType, int CollegeId)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                              {    
                                   new SqlParameter("@P_GRADE_TYPE", objGradeEntry.GradeType),
                                   new SqlParameter("@P_UGPGOT", objGradeEntry.UGPGOTNO),
                                   new SqlParameter("@P_GRADENO_NEW",Gradeno_New),
                                   new SqlParameter("@P_ORG_ID", OrgID),
                                   new SqlParameter("@P_SUBTYPE", SubType),
                                   new SqlParameter("@P_COLLEGE_ID",CollegeId),
                                   new SqlParameter("@P_OUT", status)
                                           
                              };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GRADE_UNLOCK", sqlParams, true);

                if (Convert.ToInt32(obj) == 1)
                {
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                }

                return status;

            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.UnLockGradeEntry() --> " + ex.Message + " " + ex.StackTrace);
            }
            // return status;
        }


        public DataSet GetAllGradeEntry(int Gradetype, int UGPG, int SubType, int College_id)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[4];
                objParams[0] = new SqlParameter("@P_GRADE_TYPE", Gradetype);
                objParams[1] = new SqlParameter("@P_UGPGOT", UGPG);
                objParams[2] = new SqlParameter("@P_SUBTYPE", SubType);
                objParams[3] = new SqlParameter("@P_COLLEGE_ID", College_id);

                ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_GRADE_ENTRY_NEW", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GradeController.GetStudentsList_For_I_Grade --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }
        public int AddGradeEntry(GradeEntry objGradeEntry, int OrgID, bool Status, int Gradeno_New, int SubType, int CollegeId, string MaxMarks, string MinMarks)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_GRADENO",objGradeEntry.GradeNo),
                    new SqlParameter("@P_GRADE", objGradeEntry.Grade),
                    new SqlParameter("@P_GRADE_POINT", objGradeEntry.GradePoint),
                    new SqlParameter("@P_COLLEGE_CODE", objGradeEntry.CollegeCode),
                   // new SqlParameter("@P_SUBID", objGradeEntry.Subid),
                    new SqlParameter("@P_MAXMARK", MaxMarks),
                    new SqlParameter("@P_MINMARK", MinMarks),
                   // new SqlParameter("@P_DEGREENO", objGradeEntry.DegreeNo),
                    new SqlParameter("@P_GRADE_TYPE", objGradeEntry.GradeType),
                    new SqlParameter("@P_GRADE_DESC", objGradeEntry.GradeDesc),
                    new SqlParameter("@P_UGPGOT", objGradeEntry.UGPGOTNO),
                    new SqlParameter("@P_ORG_ID", OrgID),
                    new SqlParameter("@P_STATUS", Status),
                    new SqlParameter("@P_Result", objGradeEntry.Result),
                    new SqlParameter("@P_GRADENO_NEW",Gradeno_New),
                    new SqlParameter("@P_SUBTYPE",SubType),
                    new SqlParameter("@P_COLLEGE_ID",CollegeId),
                    new SqlParameter("@P_OUT", status)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GRADE_INSERT", sqlParams, true);
               // object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GRADE_INSERT_DEMO", sqlParams, true);

                if (Convert.ToInt32(obj) == 2)
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
                else
                    status = Convert.ToInt32(CustomStatus.RecordSaved);

                //if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                //    status = Convert.ToInt32(CustomStatus.RecordSaved);
                //else
                //    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.AddBatch() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }

       
        //Adeed by GAURAV 20_12_2022

        public DataSet GetAllGradeEntry(int Gradetype, int UGPG, int SubType, int College_id, int Schemeno)
        {
            DataSet ds = null;
            try
            {
                SQLHelper objSqlHelper = new SQLHelper(connectionString);
                SqlParameter[] objParams = null;

                objParams = new SqlParameter[5];
                objParams[0] = new SqlParameter("@P_GRADE_TYPE", Gradetype);
                objParams[1] = new SqlParameter("@P_UGPGOT", UGPG);
                objParams[2] = new SqlParameter("@P_SUBTYPE", SubType);
                objParams[3] = new SqlParameter("@P_COLLEGE_ID", College_id);
                objParams[4] = new SqlParameter("@P_SCHEMENO", Schemeno);


                ds = objSqlHelper.ExecuteDataSetSP("PKG_ACD_GRADE_ENTRY_NEW_CC", objParams);
            }
            catch (Exception ex)
            {
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.GradeController.GetStudentsList_For_I_Grade --> " + ex.Message + " " + ex.StackTrace);
            }
            return ds;
        }

        //Adeed by GAURAV 20_12_2022
        public int AddGradeEntry(GradeEntry objGradeEntry, int OrgID, bool Status, int Gradeno_New, int SubType, int CollegeId, string MaxMarks, string MinMarks, int Schemeno, int DegreeNo)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                {
                    new SqlParameter("@P_GRADENO",objGradeEntry.GradeNo),
                    new SqlParameter("@P_GRADE", objGradeEntry.Grade),
                    new SqlParameter("@P_GRADE_POINT", objGradeEntry.GradePoint),
                    new SqlParameter("@P_COLLEGE_CODE", objGradeEntry.CollegeCode),
                   // new SqlParameter("@P_SUBID", objGradeEntry.Subid),
                    new SqlParameter("@P_MAXMARK", MaxMarks),
                    new SqlParameter("@P_MINMARK", MinMarks),
                   // new SqlParameter("@P_DEGREENO", objGradeEntry.DegreeNo),
                    new SqlParameter("@P_GRADE_TYPE", objGradeEntry.GradeType),
                    new SqlParameter("@P_GRADE_DESC", objGradeEntry.GradeDesc),
                    new SqlParameter("@P_UGPGOT", objGradeEntry.UGPGOTNO),
                    new SqlParameter("@P_ORG_ID", OrgID),
                    new SqlParameter("@P_STATUS", Status),
                    new SqlParameter("@P_Result", objGradeEntry.Result),
                    new SqlParameter("@P_GRADENO_NEW",Gradeno_New),
                    new SqlParameter("@P_SUBTYPE",SubType),
                    new SqlParameter("@P_COLLEGE_ID",CollegeId),
                      new SqlParameter("@P_SCHEMENO",Schemeno),//Add Gaurav 19_12_2022
                      new SqlParameter("@P_DEGREENO", DegreeNo),//Add Gaurav 19_12_2022
                    new SqlParameter("@P_OUT", status)
                };
                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                //object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GRADE_INSERT", sqlParams, true);
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GRADE_INSERT_DEMO", sqlParams, true);

                if (Convert.ToInt32(obj) == 2)
                    status = Convert.ToInt32(CustomStatus.RecordUpdated);
                else
                    status = Convert.ToInt32(CustomStatus.RecordSaved);

                //if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                //    status = Convert.ToInt32(CustomStatus.RecordSaved);
                //else
                //    status = Convert.ToInt32(CustomStatus.Error);
            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.AddBatch() --> " + ex.Message + " " + ex.StackTrace);
            }
            return status;
        }
        //Adeed by GAURAV 20_12_2022
        public int LockGradeEntryNew(GradeEntry objGradeEntry, int OrgID, int SubType, int CollegeId, int Schemeno, int DegreeNo)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                              {    
                                   new SqlParameter("@P_GRADE_TYPE", objGradeEntry.GradeType),
                                   new SqlParameter("@P_UGPGOT", objGradeEntry.UGPGOTNO),
                                  //new SqlParameter("@P_GRADENO_NEW",Gradeno_New),
                                   new SqlParameter("@P_ORG_ID", OrgID),
                                    new SqlParameter("@P_SUBTYPE", SubType),
                                    new SqlParameter("@P_COLLEGE_ID",CollegeId),
                                       new SqlParameter("@P_SCHEMENO",Schemeno),
                                          new SqlParameter("@P_DEGREENO",DegreeNo),
                                   new SqlParameter("@P_OUT", status)
                                           
                              };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GRADE_LOCK_NEW", sqlParams, true);

                if (Convert.ToInt32(obj) == 1)
                {
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                }

                return status;

            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.LockGradeEntry() --> " + ex.Message + " " + ex.StackTrace);
            }
            //return status;
        }

        //Adeed by GAURAV 20_12_2022
        public int UnLockGradeEntryNew(GradeEntry objGradeEntry, int OrgID, int SubType, int CollegeId, int Schemeno, int DegreeNo)
        {
            int status = 0;
            try
            {
                SQLHelper objSQLHelper = new SQLHelper(connectionString);
                SqlParameter[] sqlParams = new SqlParameter[]
                              {    
                                   new SqlParameter("@P_GRADE_TYPE", objGradeEntry.GradeType),
                                   new SqlParameter("@P_UGPGOT", objGradeEntry.UGPGOTNO),
                                   //new SqlParameter("@P_GRADENO_NEW",Gradeno_New),
                                   new SqlParameter("@P_ORG_ID", OrgID),
                                   new SqlParameter("@P_SUBTYPE", SubType),
                                   new SqlParameter("@P_COLLEGE_ID",CollegeId),
                                      new SqlParameter("@P_SCHEMENO",Schemeno),
                                          new SqlParameter("@P_DEGREENO",DegreeNo),
                                   new SqlParameter("@P_OUT", status)
                                           
                              };

                sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
                object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_GRADE_UNLOCK_NEW", sqlParams, true);

                if (Convert.ToInt32(obj) == 1)
                {
                    status = Convert.ToInt32(CustomStatus.RecordSaved);
                }

                return status;

            }
            catch (Exception ex)
            {
                status = Convert.ToInt32(CustomStatus.Error);
                throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BatchController.UnLockGradeEntry() --> " + ex.Message + " " + ex.StackTrace);
            }
            // return status;
        }
    }
}
