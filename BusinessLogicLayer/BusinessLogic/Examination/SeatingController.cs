﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            /// This RoomConfigController is used to control Room Configuration detail.
            /// </summary>
            public class SeatingController
            {
                /// <summary>
                /// ConnectionString
                /// </summary>
                string _uaims_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;


                /// <summary>
                /// This controller is used to Get Exam Dates for Exam Time Table
                /// Page: NewSeatingPlan.aspx
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="degreeno"></param>
                /// <returns></returns>

                public DataSet GetStudentsExamDate(int sessionno, int degreeno)//, int semesterno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", degreeno);
                        // objParams[2] = new SqlParameter("@P_SEMESTERNO", semesterno);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_EXAM_DATE_GETALL", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SeatingController.GetStudentsExamDate-> " + ex.ToString());
                    }

                    return ds;
                }

                //---------------------------------------------------


                /// <summary>
                /// This controller is used to Retrive Shift Details by Exam Date Number
                /// Page : NewSeatingPlan.aspx
                /// </summary>
                /// <param name="exdate"></param>
                /// <param name="slotno"></param>
                /// <param name="prev_status"></param>
                /// <returns></returns>


                public DataSet GetExamDateInfoById(string exdate, int slotno, int prev_status)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]                   
                        {
                            new SqlParameter("@P_EXAMDATE", exdate),                
                            new SqlParameter("@P_SLOTNO", slotno),
                            new SqlParameter("@P_PREV_STATUS", prev_status),
                        };
                        ds = objHelper.ExecuteDataSetSP("PKG_ACAD_EXAM_DATE_INFO_GET_BYID", sqlParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentController.GetExamDateInfoById()-> " + ex.ToString());
                    }
                    return ds;
                }

                //---------------------------------------------------


                /// <summary>
                /// This controller is used to Retrive Odd Column Details by Exam Date Number
                /// Page : NewSeatingPlan.aspx
                /// </summary>
                /// <param name="exdate"></param>
                /// <param name="slotno"></param>
                /// <param name="ccode"></param>
                /// <param name="type"></param>
                /// <returns></returns>

                public DataSet GetOddColumnInfoById(string exdate, int slotno, string ccode, int type)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]                   
                        {
                            new SqlParameter("@P_EXAMDATE", exdate),              
                            new SqlParameter("@P_SLOTNO", slotno),
                            new SqlParameter("@P_CCODE",ccode),
                            new SqlParameter("@P_TYPE",type),
                        };
                        ds = objHelper.ExecuteDataSetSP("PKG_ACAD_ODD_COLUMN_INFO_BYID", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SeatingController.GetExamDateInfoById()-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// This controller is Get All Room Preference Details
                /// Page : NewSeatingController.aspx
                /// </summary>
                /// <returns></returns>

                public DataSet GetRoomPreference()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParam = new SqlParameter[0];
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_ROOM_PREFERENCE_GET_ALL", objParam);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SeatingController.GetRoomPreference()-> " + ex.ToString());
                    }
                    return ds;
                }


                /// <summary>
                /// This controller is used to Update Room Preferences.
                /// Page : NewSeatingPlan.aspx
                /// </summary>
                /// <param name="roomNos"></param>
                /// <param name="preferences"></param>
                /// <returns></returns>

                public int UpdateRoomsPreferences(string roomNos, string preferences)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ROOMNO", roomNos);
                        objParams[1] = new SqlParameter("@P_PREFERENCES", preferences);
                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_UPDATE_ROOM_PREFERENCE", objParams, true));
                        return retStatus;
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.RoomConfigController.AddRoomConfig()-> " + ex.ToString());
                    }
                }

              

                /// <summary>
                /// This controller is used to Update Shift record status.
                /// Page : NewSeatingplan.aspx
                /// </summary>
                /// <param name="exdtno"></param>
                /// <param name="status"></param>
                /// <returns></returns>

                public int UpdateShiftStatus(int exdtno, int status)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[2];
                        {
                            objParams[0] = new SqlParameter("@P_EXDTNO", exdtno);
                            objParams[1] = new SqlParameter("@P_STATUS", status);
                        }
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPD_ODD_COLUMN_ALLOT_DETAILS", objParams, false);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        return retStatus;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SeatingController.UpdateShiftStatus()-> " + ex.ToString());
                    }
                    return retStatus;
                }


                /// <summary>
                /// This controller is used to save seating plan for one branch at a time 
                /// Page : NewSeatingplan.aspx
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="examno"></param>
                /// <param name="dayno"></param>
                /// <param name="slotno"></param>
                /// <param name="courseno"></param>
                /// <param name="oddeven"></param>
                /// <param name="branchno"></param>
                /// <param name="position"></param>
                /// <returns></returns>

                public int InsertSeatingArrangement(int sessionno, int examno, int dayno, int slotno, int courseno, int oddeven, int branchno, int position)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[8];
                        {
                            objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                            objParams[1] = new SqlParameter("@P_EXAMNO", examno);
                            objParams[2] = new SqlParameter("@P_DAYNO", dayno);
                            objParams[3] = new SqlParameter("@P_SLOTNO", slotno);
                            objParams[4] = new SqlParameter("@P_COURSENO", courseno);
                            objParams[5] = new SqlParameter("@P_ODDEVEN", oddeven);
                            objParams[6] = new SqlParameter("@P_BRANCHNO", branchno);
                            objParams[7] = new SqlParameter("@P_POSITION", position);
                        }
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SEAT_ALLOTMENT", objParams, false);
                        retStatus = Convert.ToInt32(ret);
                    }
                    catch (Exception ex)
                    {
                        return retStatus;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SeatingController.UpdateShiftStatus()-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public int ConfigureSeatingArrangmentDateWise(int sessionno, string examdate, int slot, int collegeid, int seatingArr, int seatingType, string ccodes, string cccodeSequence, string roomnos, string roomSeq, int UA_NO)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_EXAM_DATE", examdate);
                        objParams[2] = new SqlParameter("@P_SLOTNO", slot);
                        objParams[3] = new SqlParameter("@P_COLLEGE_ID", collegeid);
                        objParams[4] = new SqlParameter("@P_SEATING_ARRG", seatingArr);
                        objParams[5] = new SqlParameter("@P_SEATING_TYPE", seatingType);
                        objParams[6] = new SqlParameter("@P_CCODES", ccodes);
                        objParams[7] = new SqlParameter("@P_SEATING_SEQ", cccodeSequence);
                        objParams[8] = new SqlParameter("@P_ROOMNOS", roomnos);
                        objParams[9] = new SqlParameter("@P_ROOMSEQ", roomSeq);
                        objParams[10] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        //object ret = objSQLHelper.ExecuteNonQuerySP("PR_SEATING_ARRANGMENT_ALLOTMENT", objParams, true);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PR_SEATING_ARRANGMENT_ALLOTMENT_PRAFUL_MAIN", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == -1001)
                            retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.EndSemAttenSheet-> " + ex.ToString());
                    }
                    return retStatus;
                }


                public int ConfigureSeatingArrangmentDateWise(int sessionno, string examdate, int slot, int collegeid, int seatingArr, int seatingType, string ccodes, string cccodeSequence, string roomnos, string roomSeq, int UA_NO, int mid_end, string branchnSeq, string degreenoSeq)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_EXAM_DATE", examdate);
                        objParams[2] = new SqlParameter("@P_SLOTNO", slot);
                        objParams[3] = new SqlParameter("@P_COLLEGE_ID", collegeid);
                        objParams[4] = new SqlParameter("@P_SEATING_ARRG", seatingArr);
                        objParams[5] = new SqlParameter("@P_SEATING_TYPE", seatingType);
                        objParams[6] = new SqlParameter("@P_CCODES", ccodes);
                        objParams[7] = new SqlParameter("@P_SEATING_SEQ", cccodeSequence);
                        objParams[8] = new SqlParameter("@P_ROOMNOS", roomnos);
                        objParams[9] = new SqlParameter("@P_ROOMSEQ", roomSeq);
                        objParams[10] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[11] = new SqlParameter("@P_IS_MID_END", mid_end);
                        objParams[12] = new SqlParameter("@P_BRANCHNOSEQ", branchnSeq);
                        objParams[13] = new SqlParameter("@P_DEGREENOSEQ", degreenoSeq);
                        objParams[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[14].Direction = ParameterDirection.Output;

                        //object ret = objSQLHelper.ExecuteNonQuerySP("PR_SEATING_ARRANGMENT_ALLOTMENT", objParams, true);
                        object ret = objSQLHelper.ExecuteNonQuerySP("PR_SEATING_ARRANGMENT_ALLOTMENT_PRAFUL_MAIN", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == -1001)
                            retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.EndSemAttenSheet-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //public DataSet GetAllSeatPlanByExamDate(string examdate, int slotno, int collegeId)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                //        SqlParameter[] objParams = new SqlParameter[3];
                //        objParams[0] = new SqlParameter("@P_EXAMDATE", examdate);
                //        objParams[1] = new SqlParameter("@P_SLOTNO ", slotno);
                //        objParams[2] = new SqlParameter("@P_COLLEGE_ID ", collegeId);


                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SP_ALL_ROOM_SEATING_PLAN_CONFIGURATION_EXAMDATE_WISE", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.GetAllExamName-> " + ex.ToString());
                //    }
                //    return ds;
                //}


                public int ConfigureSeatingArrangment(int sessionno, int oddeven, int position, string branchno, int roomno, string semesterno)
                {

                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[6];
                        {
                            objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                            objParams[1] = new SqlParameter("@P_ODDEVEN", oddeven);
                            objParams[2] = new SqlParameter("@P_POSITION", position);
                            objParams[3] = new SqlParameter("@P_BRANCH", branchno);
                            objParams[4] = new SqlParameter("@P_ROOM", roomno);
                            objParams[5] = new SqlParameter("@P_SEMESTERNO", semesterno);
                            //objParams[6] = new SqlParameter("@P_COLLEGE", collegeno);
                            //objParams[7] = new SqlParameter("@P_DEGREE", degreeno);
                            //objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                            //objParams[6].Direction = ParameterDirection.Output;
                        }
                        objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SEAT_ALLOTMENT", objParams, false);
                        retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        return retStatus;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SeatingController.ConfigureSeatingArrangment()-> " + ex.ToString());
                    }
                    return retStatus;
                }
                # region RoomMaster



                /// <summary>
                /// This controller is used to update room in acd_room table
                /// Page : RoomMaster.aspx
                /// </summary>
                /// <param name="objexam"></param>
                /// <returns></returns>

                //public int UpdateRoom(Exam objexam)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                //        SqlParameter[] objParams = null;

                //        //update
                //        objParams = new SqlParameter[7];
                //        objParams[0] = new SqlParameter("@P_ROOMNO", objexam.Roomno);
                //        //objParams[1] = new SqlParameter("@P_BRANCHNO", objexam.BranchNo);
                //        objParams[1] = new SqlParameter("@P_ROOMNAME", objexam.Roomname);
                //        objParams[2] = new SqlParameter("@P_ROOMCAPACITY", objexam.RoomCapacity);
                //        objParams[3] = new SqlParameter("@P_FLOORNO", objexam.FloorNo);
                //        objParams[4] = new SqlParameter("@P_REQD_INVIGILATORS", objexam.InvReq);
                //        //aayushi
                //        objParams[5] = new SqlParameter("@P_DEPARTMENT_NO", objexam.department);
                //        //aayushi
                //        objParams[6] = new SqlParameter("@P_COLLEGE_ID", objexam.collegeid);


                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_UPD_ROOM", objParams, false) != null)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.UpdateCT-> " + ex.ToString());
                //    }

                //    return retStatus;
                //}

                /// <summary>
                /// This controller is used to Add Room in Acd_room table
                /// Page : RoomMaster.aspx
                /// </summary>
                /// <param name="objexam"></param>
                /// <returns></returns>

                //public int AddRoom(Exam objexam)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                //        SqlParameter[] objParams = null;
                //        //Add
                //        objParams = new SqlParameter[7];
                //        //objParams[0] = new SqlParameter("@P_DEPTNO", objexam.BranchNo);
                //        objParams[0] = new SqlParameter("@P_ROOMNAME", objexam.Roomname);
                //        objParams[1] = new SqlParameter("@P_ROOMCAPACITY", objexam.RoomCapacity);
                //        objParams[2] = new SqlParameter("@P_FLOORNO", objexam.FloorNo);
                //        objParams[3] = new SqlParameter("@P_REQD_INVIGILATORS", objexam.InvReq);
                //        //aayushi
                //        objParams[4] = new SqlParameter("@P_DEPARTMENT_NO", objexam.department);
                //        //aayushi
                //        objParams[5] = new SqlParameter("@P_COLLEGE_ID", objexam.collegeid);
                //        objParams[6] = new SqlParameter("@P_ROOMNO", SqlDbType.Int);


                //        objParams[6].Direction = ParameterDirection.Output;

                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_INS_ROOM", objParams, true) != null)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.AddSession-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                /// <summary>
                /// This controller is used to Get All Room detail.
                /// Page : RoomMaster.aspx
                /// </summary>
                /// <returns></returns>

                public DataSet GetAllRoom(int collegeid)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", collegeid);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SP_ALL_ROOM", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetAllSession-> " + ex.ToString());
                    }
                    return ds;
                }

                /// <summary>
                /// This controller is used to get single record of room by roomno.
                /// Page : RoomMaster.apsx 
                /// </summary>
                /// <param name="Roomno"></param>
                /// <returns></returns>

                public SqlDataReader GetSingleRoom(int Roomno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("P_ROOMNO", Roomno);
                        dr = objSQLHelper.ExecuteReaderSP("PKG_SESSION_SP_RET_ROOM", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetSingleSession-> " + ex.ToString());
                    }
                    return dr;
                }

                /// <summary>
                /// This controller is used to Delete room detail from table.
                /// Page : RoomMaster.apsx
                /// </summary>
                /// <param name="Roomno"></param>
                /// <returns></returns>

                public int DeleteRoom(int Roomno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ROOMNO", Roomno);
                        //objParams[1] = new SqlParameter("@P_ROOM_OUT_NO", ParameterDirection.Output);

                        retStatus = Convert.ToInt32(objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_DELETE_ROOM", objParams, true));
                        if (retStatus != -99)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.Error);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.TeachingPlanController.DeleteAcademicSession-> " + ex.ToString());
                    }
                    return retStatus;
                }

                public DataSet GetExamCourseListByDate(int sessionno, int slotno, string examdate)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            {
                               
                               new SqlParameter("@P_SESSIONNO", sessionno),
                               new SqlParameter("@P_SLOTNO ", slotno),
                               new SqlParameter("@P_EXAMDATE", examdate)
                            };
                            ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_COURSES_BY_EXAMDATE_WISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.GetAllExamName-> " + ex.ToString());
                    }
                    return ds;
                }


                #endregion

                public DataSet GetInvigilatorAndExamdates(int sessionno, int deptno, int slotno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_DEPTNO", deptno);
                        objParams[2] = new SqlParameter("@P_SLOTNO", slotno);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_EXAM_DATES", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetAllSession-> " + ex.ToString());
                    }
                    return ds;
                }

                public SqlDataReader GetRoomConfiguration(int roomno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ROOM_NO", roomno);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_ACD_GET_ROOM_CONFIG", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.RoomConfigController.GetRoomConfiguration-> " + ex.ToString());
                    }
                    return dr;
                }

                public DataSet UpdateInvigilatorAndExamdates(int sessionno, int ua_no, string dayno, int slot)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_UA_NO", ua_no);
                        objParams[2] = new SqlParameter("@P_DAYNO", dayno);
                        objParams[3] = new SqlParameter("@P_SLOT", slot);
                        ds = objSQLHelper.ExecuteDataSetSP("ACD_ASSIGN_INVIGILATOR_DUTY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetAllSession-> " + ex.ToString());
                    }
                    return ds;
                }

               
                //FOR SEATING ARRANGEMENT COURSE FILL
                public DataSet GetCoursesForSeatingArrangement(int sessionno, int examtpe, string examdate, int slotno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_EXAM_TT_TYPE", examtpe);
                        objParams[2] = new SqlParameter("@P_EXAMDATE", examdate);
                        objParams[3] = new SqlParameter("@P_SLOTNO", slotno);


                        ds = objSH.ExecuteDataSetSP("PKG_ACAD_GET_COURSES_SEAT_ARRANGEMENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseControlle.GetCoursesForSeatingArrangement-> " + ex.ToString());
                    }
                    return ds;
                }

                //GET STUDENT FOR SEATING ARRANGEMENT
                public DataSet GetStudentsForSeatingArrangement(int sessionno, int examno, int courseno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_EXAMNO", examno);
                        objParams[2] = new SqlParameter("@P_COURSENO", courseno);

                        ds = objSH.ExecuteDataSetSP("PKG_ACAD_GET_STUDENTS_FOR_SEATING_ARRANGEMENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamControlle.GetStudentsForSeatingArrangement-> " + ex.ToString());
                    }
                    return ds;
                }

               
                //INS SEATING ARRANGEMENT
                public int SeatArrangement(int sessionno, int examno, int dayno, DateTime examdate, int slotno, int courseno, int roomno, string idnos, string regnos, string collegecode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_EXAMNO", examno);
                        objParams[2] = new SqlParameter("@P_DAYNO", dayno);
                        objParams[3] = new SqlParameter("@P_EXAMDATE", examdate);
                        objParams[4] = new SqlParameter("@P_SLOTNO", slotno);
                        objParams[5] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[6] = new SqlParameter("@P_ROOMNO", roomno);
                        objParams[7] = new SqlParameter("@P_IDNOS", idnos);
                        objParams[8] = new SqlParameter("@P_REGNO", regnos);
                        objParams[9] = new SqlParameter("@P_COLLEGE_CODE", collegecode);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INS_SEATING_ARRANGEMENT", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == -1001)
                            retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.EndSemAttenSheet-> " + ex.ToString());
                    }

                    return retStatus;
                }

                //INS SEATING ARRANGEMENT--PART 2
                public int SeatAllotment(int sessionno, int examno, int dayno, int slotno, int batch, int studperbench, int position, string collegecode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                        try
                        {
                            SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                            SqlParameter[] objParams = null;

                            objParams = new SqlParameter[9];
                            objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                            objParams[1] = new SqlParameter("@P_EXAM_TT_TYPE", examno);
                            objParams[2] = new SqlParameter("@P_DAYNO", dayno);
                            objParams[3] = new SqlParameter("@P_SLOTNO", slotno);
                            objParams[4] = new SqlParameter("@P_BATCH", batch);
                            objParams[5] = new SqlParameter("@P_STUD_PER_BENCH", studperbench);
                            objParams[6] = new SqlParameter("@P_POSITION", position);
                            objParams[7] = new SqlParameter("@P_COLLEGE_CODE", collegecode);
                            objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                            objParams[8].Direction = ParameterDirection.Output;

                            object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_SEATING_ALLOTMENT", objParams, true);

                            if (Convert.ToInt32(ret) == -99)
                                retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                            else if (Convert.ToInt32(ret) == -1001)
                                retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                            else
                                retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                        }
                        catch (Exception ex)
                        {
                            retStatus = Convert.ToInt32(CustomStatus.Error);
                            throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.EndSemAttenSheet-> " + ex.ToString());
                        }

                        return retStatus;
                   
                }


                //GET STUDENT FOR SEATING ARRANGEMENT DEALLOACTION
                public DataSet GetStudentsForSeatingArrangementDeallocation(int sessionno, int examno, string examdate, int slotno, int courseno, int roomno)//, int dayno
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_EXAMNO", examno);
                        //   objParams[2] = new SqlParameter("@P_DAYNO", dayno);
                        objParams[2] = new SqlParameter("@P_EXAMDATE", examdate);
                        objParams[3] = new SqlParameter("@P_SLOTNO", slotno);
                        objParams[4] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[5] = new SqlParameter("@P_ROOMNO", roomno);

                        ds = objSH.ExecuteDataSetSP("PKG_ACAD_GET_STUDENTS_SEATING_ARRANGEMENT_DEALLOACTION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamControlle.GetStudentsForSeatingArrangement-> " + ex.ToString());
                    }
                    return ds;
                }


                // INS SEATING ARRANGEMENT DEALLOCATION
                public int SeatArrangementDeallocation(int sessionno, int examno, int dayno, DateTime examdate, int slotno, int courseno, int roomno, string idnos, string regnos, string collegecode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_EXAMNO", examno);
                        objParams[2] = new SqlParameter("@P_DAYNO", dayno);
                        objParams[3] = new SqlParameter("@P_EXAMDATE", examdate);
                        objParams[4] = new SqlParameter("@P_SLOTNO", slotno);
                        objParams[5] = new SqlParameter("@P_COURSENO", courseno);
                        objParams[6] = new SqlParameter("@P_ROOMNO", roomno);
                        objParams[7] = new SqlParameter("@P_IDNOS", idnos);
                        objParams[8] = new SqlParameter("@P_REGNO", regnos);
                        objParams[9] = new SqlParameter("@P_COLLEGE_CODE", collegecode);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INS_SEATING_ARRANGEMENT_DEALLOCATION", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == -1001)
                            retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.EndSemAttenSheet-> " + ex.ToString());
                    }

                    return retStatus;
                }

                //Show Seating Plan in Excel Sheet
                public DataSet GetDataInExcelSheetFoSeatingAllot(int sessionNo, int examno, int dayno, int slotno,int roomno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        objParams[1] = new SqlParameter("@P_EXAMNO", examno);
                        objParams[2] = new SqlParameter("@P_DAYNO", dayno);
                        objParams[3] = new SqlParameter("@P_SLOTNO", slotno);
                        objParams[4] = new SqlParameter("@P_ROOMNO", roomno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_SEAT_ALLOT_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.StudentAttendanceController.GetDataInExcelSheetFoSeatingAllot-> " + ex.ToString());
                    }

                    return ds;
                }
               

                // GET INVIGILATOR LIST FOR INVIGILATOR ENTRY
                public DataSet GetInvigilatorList()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSqlHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParam = new SqlParameter[0];
                        ds = objSqlHelper.ExecuteDataSetSP("PKG_GET_ALL_FACULTY", objParam);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SeatingController.GetInvigilatorList()-> " + ex.ToString());
                    }
                    return ds;
                }

                //INS INVIGILATOR ENTRY
                public int InvigilatorEntry(string ua_no, string duties, string collegecode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[4];
                        objParams[0] = new SqlParameter("@P_UA_NO", ua_no);
                       // objParams[1] = new SqlParameter("@P_STATUS", status);
                        objParams[1] = new SqlParameter("@P_NO_OF_DUTIES", duties);
                        objParams[2] = new SqlParameter("@P_COLLEGE_CODE", collegecode);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INS_INVIGILATOR_ENTRY", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == -1001)
                            retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.InvigilatorEntry-> " + ex.ToString());
                    }

                    return retStatus;
                }
                public int SeatAllot(int sessionno, int examno, int roomno, string idnos, string regnos, string course, string examdates, string slot, string collegecode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[10];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_EXAMNO", examno);
                        //objParams[2] = new SqlParameter("@P_DAYNO", dayno);
                        objParams[2] = new SqlParameter("@P_EXAMDATE", examdates);
                        objParams[3] = new SqlParameter("@P_SLOTNO", slot);
                        objParams[4] = new SqlParameter("@P_COURSENO", course);
                        objParams[5] = new SqlParameter("@P_ROOMNO", roomno);
                        objParams[6] = new SqlParameter("@P_IDNOS", idnos);
                        objParams[7] = new SqlParameter("@P_REGNO", regnos);
                        objParams[8] = new SqlParameter("@P_COLLEGE_CODE", collegecode);
                        objParams[9] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[9].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INS_SEAT_ALLOT", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == -1001)
                            retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.EndSemAttenSheet-> " + ex.ToString());
                    }

                    return retStatus;
                }
                //deallocation of seats [15-09-2016] according to the requirements
                public DataSet GetCoursesFor_SeatDeallocation(int sessionno, string examdate, int slotno)//, int examtpe
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[3];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        //  objParams[1] = new SqlParameter("@P_EXAM_TT_TYPE", examtpe);
                        objParams[1] = new SqlParameter("@P_EXAMDATE", examdate);
                        objParams[2] = new SqlParameter("@P_SLOTNO", slotno);


                        ds = objSH.ExecuteDataSetSP("PKG_ACAD_GET_COURSES_SEAT_FOR_DEALLOCATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseControlle.GetCoursesForSeatingArrangement-> " + ex.ToString());
                    }
                    return ds;
                }

                //GET STUDENT FOR SEAT ALLOT
                public DataSet GetStudentsForSeatAllot(int sessionno, int examno, int degreeno, int branchno, int semno, int courseno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_EXAMNO", examno);
                        objParams[2] = new SqlParameter("@P_DEGREENO", degreeno);
                        objParams[3] = new SqlParameter("@P_BRANCHNO", branchno);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semno);
                        objParams[5] = new SqlParameter("@P_COURSENO", courseno);
                        ds = objSH.ExecuteDataSetSP("PKG_ACAD_GET_STUDENTS_FOR_SEAT_ALLOT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamControlle.GetStudentsForSeatAllot-> " + ex.ToString());
                    }
                    return ds;
                }
                // COURSE FILL FOR SEAT ALLOT
                public DataSet GetCoursesForSeatAllot(int sessionno, int examtype, int semesterno, int BranchNo, int DegreeNo, int courseno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSH = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[6];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_DEGREENO", DegreeNo);
                        objParams[2] = new SqlParameter("@P_BRANCHNO", BranchNo);
                        objParams[3] = new SqlParameter("@P_EXAM_TT_TYPE", examtype);
                        objParams[4] = new SqlParameter("@P_SEMESTERNO", semesterno);
                        objParams[5] = new SqlParameter("@P_COURSENO", courseno);

                        ds = objSH.ExecuteDataSetSP("PKG_ACAD_GET_COURSES_SEAT_ALLOT", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.CourseControlle.GetCoursesForSeatAllot-> " + ex.ToString());
                    }
                    return ds;
                }


                //aayushi gupta 
                public DataSet GetAllSeatPlan(int roomno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ROOMNO", roomno);


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SP_ALL_ROOM_SEATING_PLAN_CONFIGURATION", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.GetAllExamName-> " + ex.ToString());
                    }
                    return ds;
                }

                //INS INVIGILATOR DUTY
                public int InvigilatorDuty(int sessionno, int examno, DateTime examdate, int slotno, int extraInv, string collegecode)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_EXAM_TT_TYPE", examno);
                        // objParams[2] = new SqlParameter("@P_DAYNO", dayno);
                        objParams[2] = new SqlParameter("@P_EXAM_DATE", examdate);
                        objParams[3] = new SqlParameter("@P_SLOTNO", slotno);
                        objParams[4] = new SqlParameter("@P_EXTRA_INV_REQ", extraInv);
                        objParams[5] = new SqlParameter("@P_COLLEGE_CODE", collegecode);
                        objParams[6] = new SqlParameter("@P_MSG", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("SP_ACAD_INVIGILATOR_DUTY_ALLOTMENT", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == -1001)
                            retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.InvigilatorDuty-> " + ex.ToString());
                    }

                    return retStatus;
                }

                //---------------------------------------------------
                /// <summary>
                /// Added by S.Patil = 23052019
                /// </summary>
                /// <param name="sessionno"></param>
                /// <param name="degreeno"></param>
                /// <returns></returns>
                public DataSet GetDataForSeatingPlan(string date, int slotno, int colid, int roomno, int midEnd)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_EXAMDATE", date);
                        objParams[1] = new SqlParameter("@P_SLOT_NO", slotno);
                        objParams[2] = new SqlParameter("@P_COLLEGE_ID", colid);
                        objParams[3] = new SqlParameter("@P_ROOMNO", roomno);
                        objParams[4] = new SqlParameter("@P_IS_MID_END", midEnd);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_REPORT_SEATING_PLAN_ROOMSEATS_NEW_SEATING_FIXED", objParams);
                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SeatingController.GetDataForSeatingPlan-> " + ex.ToString());
                    }

                    return ds;
                }

                public DataSet GetAllFixedSeatPlan(int sessionNo, int slotno) // modified ON 13-03-2020 BY VAISHALI
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionNo);
                        //objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterNo);  //COMMENTED ON 13-03-2020 BY VAISHALI
                        objParams[1] = new SqlParameter("@P_SLOTNO ", slotno);
                        //objParams[3] = new SqlParameter("@P_COLLEGE_ID ", collegeId);  //COMMENTED ON 13-03-2020 BY VAISHALI


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ROOM_SEATING_PLAN_FIXED", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.GetAllExamName-> " + ex.ToString());
                    }
                    return ds;
                }

                public int ConfigureSeatingArrangmentFixed(int sessionno, int slot, int seatingArr, int seatingType, string schemeNos, string schemeNosSeq, string roomnos, string roomSeq, int UA_NO, int exam_type) //modified on 13-03-2020 by Vaishali
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[11];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        //objParams[1] = new SqlParameter("@P_SEMESTERNO", semesterno);  //COMMENTED on 13-03-2020 by Vaishali
                        objParams[1] = new SqlParameter("@P_SLOTNO", slot);
                        //objParams[3] = new SqlParameter("@P_COLLEGE_ID", collegeid);  //COMMENTED on 13-03-2020 by Vaishali
                        objParams[2] = new SqlParameter("@P_SEATING_ARRG", seatingArr);
                        objParams[3] = new SqlParameter("@P_SEATING_TYPE", seatingType);
                        objParams[4] = new SqlParameter("@P_SCHEMENOS", schemeNos);
                        objParams[5] = new SqlParameter("@P_SCHEMENO_SEQ", schemeNosSeq);
                        objParams[6] = new SqlParameter("@P_ROOMNOS", roomnos);
                        objParams[7] = new SqlParameter("@P_ROOMSEQ", roomSeq);
                        objParams[8] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[9] = new SqlParameter("@P_EXAMTYPE", exam_type);
                        objParams[10] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[10].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PR_FIXED_SEATING_ARRANGMENT_ALLOTMENT_PRAFUL_MAIN", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == -1001)
                            retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.EndSemAttenSheet-> " + ex.ToString());
                    }
                    return retStatus;
                }


                //added by akhilesh on [2019-04-09]
                public DataSet Get_ProgramsNameForSeating(int sessionNo, int examtype)  //, int semeterNo, int Institute)
                {

                    DataSet ds = null;
                    try
                    {
                        SQLHelper objHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]                   
                        {
                            new SqlParameter("@P_SESSIONNO", sessionNo),   
                            new SqlParameter("@P_EXAMTYPE", examtype),   // added ON 13-03-2020 BY VAISHALI
                            //new SqlParameter("@P_SEMESTERNO", semeterNo), //COMMENTED ON 13-03-2020 BY VAISHALI
                            //new SqlParameter("@P_COLLEGE_ID",Institute),  //COMMENTED ON 13-03-2020 BY VAISHALI                      
                        };
                        ds = objHelper.ExecuteDataSetSP("PKG_GET_PROGRAMSNAME_SEATING", sqlParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetAllSession->" + ex.ToString());
                    }
                    return ds;
                }

                //ADDED ON 29-04-2020 BY VAISHALI FOR MANUAL SEATING ARRANGEMENT
                public DataSet GetExamManualCourseListByDate(int sessionno, int slotno, string examdate, int is_mids_ends)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            {
                               
                               new SqlParameter("@P_SESSIONNO", sessionno),
                               new SqlParameter("@P_SLOTNO ", slotno),
                               new SqlParameter("@P_EXAMDATE", examdate),
                               new SqlParameter("@P_IS_MIDS_ENDS", is_mids_ends)
                            };


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_COURSES_FOR_MANUAL_BY_EXAMDATE_WISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.GetAllExamName-> " + ex.ToString());
                    }
                    return ds;
                }


 //ADDED ON 29-04-2020 BY VAISHALI FOR MANUAL SEATING ARRANGEMENT
                public DataSet GetAllManualSeatPlanByExamDate(string examdate, int slotno, int sessionno, int is_mids_ends, string roomnno, string ccodes)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[6];
                        objParams[0] = new SqlParameter("@P_EXAMDATE", examdate);
                        objParams[1] = new SqlParameter("@P_SLOTNO", slotno);
                        objParams[2] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[3] = new SqlParameter("@P_IS_MIDS_ENDS", is_mids_ends);
                        objParams[4] = new SqlParameter("@P_ROOMNO", roomnno);
                        objParams[5] = new SqlParameter("@P_CCODES", ccodes);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SP_ALL_MANUAL_ROOM_SEATING_PLAN_CONFIGURATION_EXAMDATE_WISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SeatingController.GetAllManualSeatPlanByExamDate-> " + ex.ToString());
                    }
                    return ds;
                }


//ADDED ON 01-05-2020 BY VAISHALI FOR MANUAL SEATING ARRANGEMENT
                public DataSet CheckDataForManualSeatingArrangement(int sessionno, int slotno, int is_mids_ends, string examdate, string roomno, string ccodes)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            {
                               
                               new SqlParameter("@P_SESSIONNO", sessionno),
                               new SqlParameter("@P_SLOTNO ", slotno),
                               new SqlParameter("@P_IS_MIDS_ENDS", is_mids_ends),
                               new SqlParameter("@P_EXAMDATE", examdate),
                               new SqlParameter("@P_ROOMNO", roomno),
                               new SqlParameter("@P_CCODES", ccodes)
                            };


                        ds = objSQLHelper.ExecuteDataSetSP("PKG_CHECK_DATA_FOR_MANUAL_SEATING_ARRANGEMENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SeatingController.CheckDataForManualSeatingArrangement-> " + ex.ToString());
                    }
                    return ds;
                }

  //ADDED ON 30-04-2020 BY VAISHALI FOR MANUAL SEATING ARRANGEMENT
                public int ConfigureManualSeatingArrangmentDateWise(int sessionno, int slot, string examdate, string coursenos, int roomno, int mid_end, int UA_NO, string branchnos, string degreenos, string semesternos, string idnos)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[12];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SLOTNO", slot);
                        objParams[2] = new SqlParameter("@P_EXAM_DATE", examdate);
                        objParams[3] = new SqlParameter("@P_COURSENOS", coursenos);
                        objParams[4] = new SqlParameter("@P_ROOMNO", roomno);
                        objParams[5] = new SqlParameter("@P_IS_MID_END", mid_end);
                        objParams[6] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[7] = new SqlParameter("@P_BRANCHNOS", branchnos);
                        objParams[8] = new SqlParameter("@P_DEGREENOS", degreenos);
                        objParams[9] = new SqlParameter("@P_SEMESTERNOS", semesternos);
                        objParams[10] = new SqlParameter("@P_IDNOS", idnos);
                        objParams[11] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[11].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_MANUAL_SEATING_ARRANGEMENT_ALLOTMENT_NEW", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == -1001)
                            retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SeatingController.ConfigureManualSeatingArrangmentDateWise-> " + ex.ToString());
                    }
                    return retStatus;
                }


 //ADDED ON 01-05-2020 BY VAISHALI FOR MANUAL SEATING ARRANGEMENT
                public int DeallocateManualSeatingArrangment(int sessionno, int slot, int exam_type, string examdate, int roomno, string coursenos)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_SLOTNO", slot);
                        objParams[2] = new SqlParameter("@P_IS_MIDS_ENDS", exam_type);
                        objParams[3] = new SqlParameter("@P_EXAMDATE", examdate);
                        objParams[4] = new SqlParameter("@P_ROOMNO", roomno);
                        objParams[5] = new SqlParameter("@P_COURSENOS", coursenos);
                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_DEALLOCATE_DATA_MANUAL_SEATING_ARRANGEMENT", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordDeleted);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SeatingController.DeallocateManualSeatingArrangment-> " + ex.ToString());
                    }
                    return retStatus;
                }

                //ADDED ON 08-05-2020 BY VAISHALI FOR MANUAL SEATING ARRANGEMENT excel report
                public DataSet GetDataForManualSeatingArrangementExcelReport(string examdate, int slotno, int sessionno, int is_mids_ends)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            {
                               new SqlParameter("@P_EXAMDATE", examdate),
                               new SqlParameter("@P_SLOTNO ", slotno),
                               new SqlParameter("@P_SESSIONNO", sessionno),
                               new SqlParameter("@P_IS_MIDS_ENDS", is_mids_ends),                             
                            };

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_ALL_MANUAL_ROOM_SEATING_PLAN_CONFIGURATION_EXAMDATE_WISE_REPORT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SeatingController.GetDataForManualSeatingArrangementExcelReport-> " + ex.ToString());
                    }
                    return ds;
                }

                //ADDED ON 07-05-2020 BY VAISHALI TO GET THE STUDENT LIST FOR MANUAL SEATING ARRANGEMENT
                public DataSet GetStudentListForManualSeatingArrangement(string examdate, int sessionno, int slotno, int is_mids_ends, string coursenos, string branchnos, string semesternos, string degreenos)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[]
                            {
                               new SqlParameter("@P_EXAMDATE", examdate),
                               new SqlParameter("@P_SESSIONNO", sessionno),
                               new SqlParameter("@P_SLOTNO ", slotno),
                               new SqlParameter("@P_EXAMTYPE", is_mids_ends),
                               new SqlParameter("@P_COURSENO", coursenos),
                               new SqlParameter("@P_BRANCHNO", branchnos),
                               new SqlParameter("@P_SEMESTERNO", semesternos),
                               new SqlParameter("@P_DEGREENO", degreenos)
                            };

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUDENT_LIST_FOR_MANUAL_SEATING_ARRANGEMENT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.SeatingController.GetStudentListForManualSeatingArrangement-> " + ex.ToString());
                    }
                    return ds;
                }
                //Added on 24032022 for Sitting Arrangement page for Exam configuration

                public int ConfigureSeatingArrangmentDateWise(int sessionno, string examdate, int slot, int ExamType)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[5];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_EXAM_DATE", examdate);
                        objParams[2] = new SqlParameter("@P_SLOTNO", slot);
                        objParams[3] = new SqlParameter("@P_PREV_STATUS", ExamType);
                        objParams[4] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[4].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PR_SEATING_ARRANGMENT_ALLOTMENT", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == -1001)
                            retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.EndSemAttenSheet-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet Get_BLOCK_ARRANGEMENT_REPORT(string examdate, int slot, int previousStatus)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@EXAM_DATE", examdate);
                        objParams[1] = new SqlParameter("@SLOTNO", slot);
                        objParams[2] = new SqlParameter("@PREV_STATUS", previousStatus);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_EXAM_BLOCK_ARRANGMENT_REPORT_FOR_EXAM_DATE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetAllSession->" + ex.ToString());
                    }
                    return ds;
                }
                public DataSet Get_BuildingChartDateWise(int sessionno, string examdate, int slot, int previousStatus)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_EXAMDATE", examdate);
                        objParams[2] = new SqlParameter("@P_SLOTNO", slot);
                        objParams[3] = new SqlParameter("@P_PREV_STATUS", previousStatus);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_DATAWISE_BUILDINGCHART", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SeatingController.Get_BuildingChartDateWise-> " + ex.ToString());
                    }
                    return ds;
                }
                //Nikita Karalkar [14/11/2018]
                public DataSet Get_QUESTION_PAPER_REPORT(string examdate, int slot, int previousStatus)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_DATE", examdate);
                        objParams[1] = new SqlParameter("@P_SLOTNO", slot);
                        objParams[2] = new SqlParameter("@P_PREV_STATUS", previousStatus);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EXAM_STUDENT_COUNT", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetAllSession-> " + ex.ToString());
                    }
                    return ds;
                }
                //public DataSet Get_QUESTION_PAPER_REPORT(string examdate, int slot, int previousStatus)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                //        SqlParameter[] objParams = new SqlParameter[3];
                //        objParams[0] = new SqlParameter("@P_DATE", examdate);
                //        objParams[1] = new SqlParameter("@P_SLOTNO", slot);
                //        objParams[2] = new SqlParameter("@P_PREV_STATUS", previousStatus);

                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACD_EXAM_STUDENT_COUNT", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.GetAllSession-> " + ex.ToString());
                //    }
                //    return ds;
                //}

                //public DataSet GetAllSeatPlanByExamDate(string examdate, int slotno, int collegeId)
                //{
                //    DataSet ds = null;
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                //        SqlParameter[] objParams = new SqlParameter[3];
                //        objParams[0] = new SqlParameter("@P_EXAMDATE", examdate);
                //        objParams[1] = new SqlParameter("@P_SLOTNO ", slotno);
                //        objParams[2] = new SqlParameter("@P_COLLEGE_ID ", collegeId);


                //        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SP_ALL_ROOM_SEATING_PLAN_CONFIGURATION_EXAMDATE_WISE", objParams);
                //    }
                //    catch (Exception ex)
                //    {
                //        return ds;
                //        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.GetAllExamName-> " + ex.ToString());
                //    }
                //    return ds;
                //}

                public DataSet Get_BuildingChartDateWiseSummary(int sessionno, string examdate, int slot, int previousStatus)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_EXAMDATE", examdate);
                        objParams[2] = new SqlParameter("@P_SLOTNO", slot);
                        objParams[3] = new SqlParameter("@P_PREV_STATUS", previousStatus);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_GET_DATAWISE_BUILDINGCHARTSUMMARY", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SeatingController.Get_BuildingChartDateWise-> " + ex.ToString());
                    }
                    return ds;
                }


                //public int AddRoom(Exam objexam)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                //        SqlParameter[] objParams = null;
                //        //Add
                //        objParams = new SqlParameter[8];
                //        //objParams[0] = new SqlParameter("@P_DEPTNO", objexam.BranchNo);
                //        objParams[0] = new SqlParameter("@P_ROOMNAME", objexam.Roomname);
                //        objParams[1] = new SqlParameter("@P_ROOMCAPACITY", objexam.RoomCapacity);
                //        objParams[2] = new SqlParameter("@P_FLOORNO", objexam.FloorNo);
                //        objParams[3] = new SqlParameter("@P_REQD_INVIGILATORS", objexam.InvReq);
                //        //aayushi
                //        objParams[4] = new SqlParameter("@P_DEPARTMENT_NO", objexam.department);
                //        //aayushi
                //        objParams[5] = new SqlParameter("@P_COLLEGE_ID", objexam.collegeid);

                //        objParams[6] = new SqlParameter("@P_SequenceNo", objexam.Sequence);
                //        objParams[7] = new SqlParameter("@P_ROOMNO", SqlDbType.Int);


                //        objParams[7].Direction = ParameterDirection.Output;

                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_INS_ROOM", objParams, true) != null)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.AddSession-> " + ex.ToString());
                //    }
                //    return retStatus;
                //}

                //Added on 24032022 for Sitting Arrangement page for Exam configuration

                public int ConfigureSeatingArrangmentDateWise(int sessionno, string examdate, int slot, int ExamType, int uano, int orgno, int collegeid)
                {

                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_EXAM_DATE", examdate);
                        objParams[2] = new SqlParameter("@P_SLOTNO", slot);
                        objParams[3] = new SqlParameter("@P_PREV_STATUS", ExamType);

                        objParams[4] = new SqlParameter("@P_UA_NO", uano);
                        objParams[5] = new SqlParameter("@P_COLLEGE_ID", collegeid);
                        objParams[6] = new SqlParameter("@P_ORGANIZATIONID", orgno);

                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PR_SEATING_ARRANGMENT_ALLOTMENT", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == -1001)
                            retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.EndSemAttenSheet-> " + ex.ToString());
                    }
                    return retStatus;
                }
                /// <summary>
                /// This procedure used for to Deallocate seating arrrangement plan
                /// </summary>

                public int DeallocateSeatingArrangment(int sessionno, string examdate, int slot, int uano, int orgno, int collegeid)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[7];
                        objParams[0] = new SqlParameter("@P_SESSIONNO", sessionno);
                        objParams[1] = new SqlParameter("@P_EXAM_DATE", examdate);
                        objParams[2] = new SqlParameter("@P_SLOTNO", slot);


                        objParams[3] = new SqlParameter("@P_UA_NO", uano);
                        objParams[4] = new SqlParameter("@P_ORGANIZATIONID", orgno);
                        objParams[5] = new SqlParameter("@P_COLLEGE_ID", collegeid);

                        objParams[6] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[6].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PR_DEALLOCATE_SEATING_ARRANGMENT", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == -1001)
                            retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.EndSemAttenSheet-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public DataSet GetAllSeatPlanByExamDate(string examdate, int slotno, int collegeId)    // int ExamType
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_EXAMDATE", examdate);
                        objParams[1] = new SqlParameter("@P_SLOTNO", slotno);
                        // objParams[2] = new SqlParameter("@P_PREV_STATUS", ExamType);
                        objParams[2] = new SqlParameter("@P_COLLEGE_ID ", collegeId);
                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_SP_ALL_ROOM_SEATING_PLAN_CONFIGURATION_EXAMDATE_WISE", objParams);
                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.GetAllExamName-> " + ex.ToString());
                    }
                    return ds;
                }
                //public int UpdateRoom(Exam objexam)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                //        SqlParameter[] objParams = null;

                //        //update
                //        objParams = new SqlParameter[8];
                //        objParams[0] = new SqlParameter("@P_ROOMNO", objexam.Roomno);
                //        //objParams[1] = new SqlParameter("@P_BRANCHNO", objexam.BranchNo);
                //        objParams[1] = new SqlParameter("@P_ROOMNAME", objexam.Roomname);
                //        objParams[2] = new SqlParameter("@P_ROOMCAPACITY", objexam.RoomCapacity);
                //        objParams[3] = new SqlParameter("@P_FLOORNO", objexam.FloorNo);
                //        objParams[4] = new SqlParameter("@P_REQD_INVIGILATORS", objexam.InvReq);
                //        //aayushi
                //        objParams[5] = new SqlParameter("@P_DEPARTMENT_NO", objexam.department);
                //        //aayushi
                //        objParams[6] = new SqlParameter("@P_COLLEGE_ID", objexam.collegeid);

                //        objParams[7] = new SqlParameter("@P_SEQUENCENO", objexam.Sequence);

                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_UPD_ROOM", objParams, false) != null)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.UpdateCT-> " + ex.ToString());
                //    }

                //    return retStatus;
                //}

                public int AddRoom(Exam objexam)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;
                        //Add
                        objParams = new SqlParameter[8];
                        //objParams[0] = new SqlParameter("@P_DEPTNO", objexam.BranchNo);
                        objParams[0] = new SqlParameter("@P_ROOMNAME", objexam.Roomname);
                        objParams[1] = new SqlParameter("@P_ROOMCAPACITY", objexam.RoomCapacity);
                        objParams[2] = new SqlParameter("@P_FLOORNO", objexam.FloorNo);
                        objParams[3] = new SqlParameter("@P_REQD_INVIGILATORS", objexam.InvReq);
                        //aayushi
                        objParams[4] = new SqlParameter("@P_DEPARTMENT_NO", objexam.department);
                        //aayushi
                        objParams[5] = new SqlParameter("@P_COLLEGE_ID", objexam.collegeid);
                        // objParams[6] = new SqlParameter("@P_SequenceNo", objexam.Sequence);
                        objParams[6] = new SqlParameter("@P_BLOCKNO", objexam.Blockno);
                        objParams[7] = new SqlParameter("@P_ROOMNO", SqlDbType.Int);


                        objParams[7].Direction = ParameterDirection.Output;

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_INS_ROOM", objParams, true) != null)
                            // if (objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_INS_ROOM_TEST", objParams, true) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.AddSession-> " + ex.ToString());
                    }
                    return retStatus;
                }
                public int UpdateRoom(Exam objexam)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);
                        SqlParameter[] objParams = null;

                        //update
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_ROOMNO", objexam.Roomno);
                        //objParams[1] = new SqlParameter("@P_BRANCHNO", objexam.BranchNo);
                        objParams[1] = new SqlParameter("@P_ROOMNAME", objexam.Roomname);
                        objParams[2] = new SqlParameter("@P_ROOMCAPACITY", objexam.RoomCapacity);
                        objParams[3] = new SqlParameter("@P_FLOORNO", objexam.FloorNo);
                        objParams[4] = new SqlParameter("@P_REQD_INVIGILATORS", objexam.InvReq);
                        //aayushi
                        objParams[5] = new SqlParameter("@P_DEPARTMENT_NO", objexam.department);
                        //aayushi
                        objParams[6] = new SqlParameter("@P_COLLEGE_ID", objexam.collegeid);
                        objParams[7] = new SqlParameter("@P_BLOCKNO", objexam.Blockno);

                        //objParams[7] = new SqlParameter("@P_SEQUENCENO", objexam.Sequence);

                        if (objSQLHelper.ExecuteNonQuerySP("PKG_SESSION_SP_UPD_ROOM", objParams, false) != null)
                            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);

                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.SessionController.UpdateCT-> " + ex.ToString());
                    }

                    return retStatus;
                }
                //Added on 30042022 For Save Room Sequence

                public int ConfigureSeatingPlanSequence(int roomno, int roomSq)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_uaims_constr);

                        SqlParameter[] objParams = new SqlParameter[3];
                        objParams[0] = new SqlParameter("@P_ROOMNO", roomno);
                        objParams[1] = new SqlParameter("@P_SEQUENCENO", roomSq);

                        objParams[2] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[2].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PR_SAVE_SEATING_SEQUENCE_PLAN", objParams, true);

                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == -1001)
                            retStatus = Convert.ToInt32(CustomStatus.DuplicateRecord);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.ExamController.EndSemAttenSheet-> " + ex.ToString());
                    }
                    return retStatus;
                }


                #region Seating Arrangement Start  

                #endregion Seating Arrangement End
            }
        }
    }
}

