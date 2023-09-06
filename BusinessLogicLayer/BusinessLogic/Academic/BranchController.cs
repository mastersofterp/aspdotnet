using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using IITMS;
using IITMS.UAIMS;
using IITMS.UAIMS.BusinessLayer;
using IITMS.SQLServer.SQLDAL;
using System.Data.SqlClient;
using IITMS.UAIMS.BusinessLayer.BusinessEntities;


namespace IITMS
{
    namespace UAIMS
    {
        namespace BusinessLayer.BusinessLogic
        {
            public class BranchController
            {
                private string _UAIMS_constr = System.Configuration.ConfigurationManager.ConnectionStrings["UAIMS"].ConnectionString;

                //public int AddBranchType(Branch objBranchType)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);
                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;

                //        //Add New Branch Type
                //        objParams = new SqlParameter[15];
                //        objParams[0] = new SqlParameter("@P_SHORTNAME", objBranchType.ShortName);
                //        objParams[1] = new SqlParameter("@P_LONGNAME", objBranchType.LongName);
                //        objParams[2] = new SqlParameter("@P_BRANCHNM_HINDI", objBranchType.BranchNameInHindi);
                //        objParams[3] = new SqlParameter("@P_INTAKE1", objBranchType.Intake1);
                //        objParams[4] = new SqlParameter("@P_INTAKE2", objBranchType.Intake2);
                //        objParams[5] = new SqlParameter("@P_INTAKE3", objBranchType.Intake3);
                //        objParams[6] = new SqlParameter("@P_INTAKE4", objBranchType.Intake4);
                //        objParams[7] = new SqlParameter("@P_INTAKE5", objBranchType.Intake5);
                //        objParams[8] = new SqlParameter("@P_DURATION", objBranchType.Duration);
                //        objParams[9] = new SqlParameter("@P_CODE", objBranchType.Code);
                //        objParams[10] = new SqlParameter("@P_UGPGOT", objBranchType.Ugpgot);
                //        objParams[11] = new SqlParameter("@P_DEGREENO", objBranchType.DegreeNo);
                //        objParams[12] = new SqlParameter("@P_DEPTNO", objBranchType.DeptNo);
                //        objParams[13] = new SqlParameter("@P_COLLEGE_CODE", objBranchType.CollegeCode);
                //        objParams[14] = new SqlParameter("@P_BRANCHNO", objBranchType.BranchNo);
                //        objParams[14].Direction =  ParameterDirection.InputOutput;

                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_INS_BRANCH", objParams, false) != null)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);

                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.AddBranchType-> " + ex.ToString());
                //    }

                //    return retStatus;
                //}


                public int AddBranchType(Branch objBranchType)
                {
                    int status = 0;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]
                        

                        //Add New Branch Type
                        {
                         new SqlParameter("@P_SHORTNAME", objBranchType.ShortName),
                         new SqlParameter("@P_LONGNAME", objBranchType.LongName),
                         new SqlParameter("@P_BRANCHNM_HINDI", objBranchType.BranchNameInHindi),
                         new SqlParameter("@P_INTAKE1", objBranchType.Intake1),
                         new SqlParameter("@P_INTAKE2", objBranchType.Intake2),
                         new SqlParameter("@P_INTAKE3", objBranchType.Intake3),
                         new SqlParameter("@P_INTAKE4", objBranchType.Intake4),
                         new SqlParameter("@P_INTAKE5", objBranchType.Intake5),
                         new SqlParameter("@P_DURATION", objBranchType.Duration),
                         new SqlParameter("@P_CODE", objBranchType.Code),
                         new SqlParameter("@P_UGPGPF", objBranchType.Ugpgpf),
                         new SqlParameter("@P_DEGREENO", objBranchType.DegreeNo),
                         new SqlParameter("@P_DEPTNO", objBranchType.DeptNo),
                         new SqlParameter("@P_COLLEGE_CODE", objBranchType.CollegeCode),
                         new SqlParameter("@P_BRANCHNO", status)
                    };
                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;
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

                public SqlDataReader GetBranchType(int branchno)
                {
                    SqlDataReader dr = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_BRANCHNO", branchno);

                        dr = objSQLHelper.ExecuteReaderSP("PKG_ACAD_SINGLE_BRANCH", objParams);
                    }
                    catch (Exception ex)
                    {
                        return dr;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.GetBranchType-> " + ex.ToString());
                    }
                    return dr;
                }

                //public int UpdateBranchType(Branch objBranch)
                //{
                //    int retStatus = Convert.ToInt32(CustomStatus.Others);

                //    try
                //    {
                //        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                //        SqlParameter[] objParams = null;

                //        //Update complaint type
                //        objParams = new SqlParameter[14];
                        
                //        objParams[0] = new SqlParameter("@P_SHORTNAME", objBranch.ShortName);
                //        objParams[1] = new SqlParameter("@P_LONGNAME ", objBranch.LongName);
                //        objParams[2] = new SqlParameter("@P_BRANCHNM_HINDI", objBranch.BranchNameInHindi);
                //        objParams[3] = new SqlParameter("@P_INTAKE1", objBranch.Intake1);
                //        objParams[4] = new SqlParameter("@P_INTAKE2", objBranch.Intake2);
                //        objParams[5] = new SqlParameter("@P_INTAKE3", objBranch.Intake3);
                //        objParams[6] = new SqlParameter("@P_INTAKE4", objBranch.Intake4);
                //        objParams[7] = new SqlParameter("@P_INTAKE5", objBranch.Intake5);
                //        objParams[8] = new SqlParameter("@P_DURATION", objBranch.Duration);
                //        objParams[9] = new SqlParameter("@P_CODE", objBranch.Code);
                //        objParams[10] = new SqlParameter("@P_UGPGOT", objBranch.Ugpgot);
                //        objParams[11] = new SqlParameter("@P_DEGREENO", objBranch.DegreeNo);
                //        objParams[12] = new SqlParameter("@P_BRANCHNO", objBranch.BranchNo);
                //        objParams[13] = new SqlParameter("@P_DEPTNO", objBranch.DeptNo);

                //        if (objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPD_BRANCH", objParams, false) != null)
                //            retStatus = Convert.ToInt32(CustomStatus.RecordUpdated);
                //    }
                //    catch (Exception ex)
                //    {
                //        retStatus = Convert.ToInt32(CustomStatus.Error);
                //        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.ComplaintController.UpdateCT-> " + ex.ToString());
                //    }

                //    return retStatus;
                //}


                public int UpdateBranchType(Branch objBranch)
                {
                    int status;

                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] sqlParams = new SqlParameter[]

                        //Update complaint type
                    
                        {
                         new SqlParameter("@P_SHORTNAME", objBranch.ShortName),
                         new SqlParameter("@P_LONGNAME ", objBranch.LongName),
                         new SqlParameter("@P_BRANCHNM_HINDI", objBranch.BranchNameInHindi),
                         new SqlParameter("@P_INTAKE1", objBranch.Intake1),
                         new SqlParameter("@P_INTAKE2", objBranch.Intake2),
                         new SqlParameter("@P_INTAKE3", objBranch.Intake3),
                         new SqlParameter("@P_INTAKE4", objBranch.Intake4),
                         new SqlParameter("@P_INTAKE5", objBranch.Intake5),
                         new SqlParameter("@P_DURATION", objBranch.Duration),
                         new SqlParameter("@P_CODE", objBranch.Code),
                         new SqlParameter("@P_UGPGPF", objBranch.Ugpgpf),
                         new SqlParameter("@P_DEGREENO", objBranch.DegreeNo),
                         new SqlParameter("@P_BRANCHNO", objBranch.BranchNo),
                         new SqlParameter("@P_DEPTNO", objBranch.DeptNo)
                    };


                        sqlParams[sqlParams.Length - 1].Direction = ParameterDirection.InputOutput;

                        object obj = objSQLHelper.ExecuteNonQuerySP("PKG_ACAD_UPD_BRANCH", sqlParams, true);

                        if (obj != null && obj.ToString() != "-99" && obj.ToString() != "-1001")
                            status = Convert.ToInt32(CustomStatus.RecordUpdated);
                        else
                            status = Convert.ToInt32(CustomStatus.Error);
                    }
                    catch (Exception ex)
                    {
                        status = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.UpdateBranch() --> " + ex.Message + " " + ex.StackTrace);
                    }

                    return status;
                }

                public DataSet GetAllBranchType()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_ACAD_ALL_BRANCH", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.GetAllBranchTypes-> " + ex.ToString());
                    }
                    return ds;
                }

                // BRANCH CHANGE METHOD BELOW

                public int AddChagneBranchData(Student objStudent)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add Branch change data
                        objParams = new SqlParameter[8];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_ENROLLMENTNO", objStudent.RollNo);
                        objParams[2] = new SqlParameter("@P_OLDBRANCHNO", objStudent.BranchNo);
                        objParams[3] = new SqlParameter("@P_NEWBRANCHNO", objStudent.NewBranchNo);
                        objParams[4] = new SqlParameter("@P_NEWENROLLMENTNO", objStudent.RegNo);
                        objParams[5] = new SqlParameter("@P_REMARK", objStudent.Remark);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objStudent.CollegeCode);
                        //objParams[7] = new SqlParameter("@P_ADMROUNDNO", objStudent.AdmroundNo);
                        //objParams[8] = new SqlParameter("@P_ADMDATE", objStudent.AdmDate);
                        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[7].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_INS_BRANCH_CHANGE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.AddChagneBranchData-> " + ex.ToString());
                    }

                    return retStatus;
                }

                ////public int ChangeBranch_NewStudent(Student objStudent, string NewRegNo)
                ////{
                ////    int retStatus = Convert.ToInt32(CustomStatus.Others);
                ////    try
                ////    {
                ////        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                ////        SqlParameter[] objParams = null;

                ////        //Add Branch change data
                ////        objParams = new SqlParameter[8];
                ////        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                ////        objParams[1] = new SqlParameter("@P_ENROLLMENTNO", objStudent.RegNo);
                ////        objParams[2] = new SqlParameter("@P_OLDBRANCHNO", objStudent.BranchNo);
                ////        objParams[3] = new SqlParameter("@P_NEWBRANCHNO", objStudent.NewBranchNo);
                ////        objParams[4] = new SqlParameter("@P_NEWENROLLMENTNO", NewRegNo);
                ////        objParams[5] = new SqlParameter("@P_REMARK", objStudent.Remark);
                ////        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objStudent.CollegeCode);
                ////        objParams[7] = new SqlParameter("@P_OUT", SqlDbType.Int);
                ////        objParams[7].Direction = ParameterDirection.Output;

                ////        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_INS_BRANCH_CHANGE_NEW_ADMISSION", objParams, true);
                ////        if (Convert.ToInt32(ret) == -99)
                ////            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                ////        else
                ////            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                ////    }
                ////    catch (Exception ex)
                ////    {
                ////        retStatus = Convert.ToInt32(CustomStatus.Error);
                ////        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.BranchController.AddChagneBranchData-> " + ex.ToString());
                ////    }

                ////    return retStatus;
                ////}

                public int ChangeBranch_NewStudent(Student objStudent, string NewRegNo, int userno)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add Branch change data
                        objParams = new SqlParameter[9];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_ENROLLMENTNO", objStudent.RegNo);
                        objParams[2] = new SqlParameter("@P_OLDBRANCHNO", objStudent.BranchNo);
                        objParams[3] = new SqlParameter("@P_NEWBRANCHNO", objStudent.NewBranchNo);
                        objParams[4] = new SqlParameter("@P_NEWENROLLMENTNO", NewRegNo);
                        objParams[5] = new SqlParameter("@P_REMARK", objStudent.Remark);
                        objParams[6] = new SqlParameter("@P_COLLEGE_CODE", objStudent.CollegeCode);
                        objParams[7] = new SqlParameter("@P_USERNO", userno);
                        objParams[8] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[8].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_INS_BRANCH_CHANGE_NEW_ADMISSION", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.BranchController.AddChagneBranchData-> " + ex.ToString());
                    }

                    return retStatus;
                }

                /// <summary>
                /// UPDATED BY: M. REHBAR SHEIKH
                /// </summary>
                // EXISTING UPDATED METHODS //  
                public int ChangeBranch_NewStudent(Student objStudent, string NewRollNo, string OldRollNo, int userno, decimal paidfee, decimal excessamt, int RegnoCheckbox)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add Branch change data
                        objParams = new SqlParameter[15];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_ENROLLMENTNO", objStudent.RegNo);
                        objParams[2] = new SqlParameter("@P_OLDBRANCHNO", objStudent.BranchNo);
                        objParams[3] = new SqlParameter("@P_NEWBRANCHNO", objStudent.NewBranchNo);
                        objParams[4] = new SqlParameter("@P_NEWROLLNO", NewRollNo);
                        objParams[5] = new SqlParameter("@P_OLDROLLNO", OldRollNo);
                        objParams[6] = new SqlParameter("@P_REMARK", objStudent.Remark);
                        objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objStudent.CollegeCode);
                        objParams[8] = new SqlParameter("@P_USERNO", userno);
                        objParams[9] = new SqlParameter("@P_PAIDFEE", paidfee);
                        objParams[10] = new SqlParameter("@P_EXCESSAMOUNT", excessamt);
                        objParams[11] = new SqlParameter("@P_DEGREENO", objStudent.NewDegreeNo);
                        objParams[12] = new SqlParameter("@P_NEWCOLLEGE_ID", objStudent.NewCollege_ID);
                        objParams[13] = new SqlParameter("@P_REGNOCHECKBOX", RegnoCheckbox);
                        objParams[14] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[14].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_INS_BRANCH_CHANGE_NEW_ADMISSION", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.BranchController.AddChagneBranchData-> " + ex.ToString());
                    }
                    return retStatus;
                }
                //ADDED BY SAFAL GUPTA ON 03022021


                public int ChangeBranch_Request(Student objStudent, string NewRollNo, string OldRollNo, int userno, decimal paidfee, decimal excessamt, string file_type, string filename, string path, int new_college_id)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add Branch change data
                        objParams = new SqlParameter[18];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_ENROLLMENTNO", objStudent.RegNo);
                        objParams[2] = new SqlParameter("@P_OLDBRANCHNO", objStudent.BranchNo);
                        objParams[3] = new SqlParameter("@P_NEWBRANCHNO", objStudent.NewBranchNo);
                        objParams[4] = new SqlParameter("@P_NEWROLLNO", NewRollNo);
                        objParams[5] = new SqlParameter("@P_OLDROLLNO", OldRollNo);
                        objParams[6] = new SqlParameter("@P_REMARK", objStudent.Remark);
                        objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objStudent.CollegeCode);
                        objParams[8] = new SqlParameter("@P_USERNO", userno);
                        objParams[9] = new SqlParameter("@P_PAIDFEE", paidfee);
                        objParams[10] = new SqlParameter("@P_EXCESSAMOUNT", excessamt);
                        objParams[11] = new SqlParameter("@P_DEGREENO", objStudent.NewDegreeNo);
                        //  objParams[12] = new SqlParameter("@P_DEGREENO", objStudent.NewDegreeNo);
                        objParams[12] = new SqlParameter("@P_COLLEGE_ID", objStudent.College_ID);//added by deepali on 15/09/2020
                        objParams[13] = new SqlParameter("@P_FILE_TYPE", file_type);
                        objParams[14] = new SqlParameter("@P_FILE_NAME", filename);
                        objParams[15] = new SqlParameter("@P_PATH", path);
                        objParams[16] = new SqlParameter("@P_NEW_COLLEGE_ID", new_college_id);//added by dileep on 05.01.2021
                        objParams[17] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[17].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_INS_BRANCH_CHANGE_REQUEST", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.BranchController.ChangeBranch_Request-> " + ex.ToString());
                    }
                    return retStatus;
                }



                //*************************************************************************************************************************
                //ADDED BY SAFAL GUPTA ON 03022021

                public DataSet GetStudentsforBranchangeAcademicApproval(int college_id, int Degreeno)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[2];
                        objParams[0] = new SqlParameter("@P_COLLEGE_ID", college_id);
                        objParams[1] = new SqlParameter("@P_DEGREENO", Degreeno);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUDENTS_FOR_BRANCH_CHANGE_ACADEMIC_APPROVAL", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.GetStudentsforBranchangeApproval-> " + ex.ToString());

                    }
                    return ds;
                }


                //************************************************************************************************************




                //ADDED BY SAFAL GUPTA ON 03022021
                public int Branch_Change_ACADEMIC_Level(int IDNO, int UA_NO, string Remark)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[2] = new SqlParameter("@P_REMARK", Remark);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_BRANCH_CHANGE_ACADEMIC_LEVEL", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.Branch_Change_ACADEMIC_Level-> " + ex.ToString());
                    }
                    return retStatus;
                }




                //****************************************************************************************************************************************



                //ADDED BY SAFAL GUPTA ON 03022021
                public DataSet GetBranchChangeFirstLevel()
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[0];

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_BRANCH_CHANGE_REPORT", objParams);

                    }
                    catch (Exception ex)
                    {
                        return ds;
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.GetBranchChangeFirstLevel-> " + ex.ToString());
                    }
                    return ds;
                }



                //***************************************************************************************************************************



                //ADDED BY SAFAL GUPTA ON 03022021s
                public DataSet GetStudentsforBranchangeApproval(int AdmBatch)
                {
                    DataSet ds = null;
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = new SqlParameter[1];
                        objParams[0] = new SqlParameter("@P_ADMBATCH", AdmBatch);

                        ds = objSQLHelper.ExecuteDataSetSP("PKG_GET_STUDENTS_FOR_BRANCH_CHANGE_FIRST_APPROVAL", objParams);

                    }
                    catch (Exception ex)
                    {
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.BranchController.GetStudentsforBranchangeApproval-> " + ex.ToString());

                    }
                    return ds;
                }



                //****************************************************************************************************************************



                //ADDED BY SAFAL GUPTA ON 03022021
                public int Branch_Change_First_Level(int IDNO, int UA_NO, string Remark)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;
                        objParams = new SqlParameter[4];

                        objParams[0] = new SqlParameter("@P_IDNO", IDNO);
                        objParams[1] = new SqlParameter("@P_UA_NO", UA_NO);
                        objParams[2] = new SqlParameter("@P_REMARK", Remark);
                        objParams[3] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[3].Direction = ParameterDirection.Output;

                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_ACD_BRANCH_CHANGE_FIRST_LEVEL", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.UAIMS.BusinessLayer.BusinessLogic.StudentRegistration.Branch_Change_First_Level-> " + ex.ToString());
                    }
                    return retStatus;
                }


                //************************************************************************************************************************


                //ADDED BY SAFAL GUPTA ON 03022021

                public int ChangeBranch_FinalApprove(Student objStudent, string NewRollNo, string OldRollNo, int userno, decimal paidfee, decimal excessamt, int brch_no, int IsWithFeePaid, int SemesterNo)
                {
                    int retStatus = Convert.ToInt32(CustomStatus.Others);
                    try
                    {
                        SQLHelper objSQLHelper = new SQLHelper(_UAIMS_constr);
                        SqlParameter[] objParams = null;

                        //Add Branch change data
                        objParams = new SqlParameter[17];
                        objParams[0] = new SqlParameter("@P_IDNO", objStudent.IdNo);
                        objParams[1] = new SqlParameter("@P_ENROLLMENTNO", objStudent.RegNo);
                        objParams[2] = new SqlParameter("@P_OLDBRANCHNO", objStudent.BranchNo);
                        objParams[3] = new SqlParameter("@P_NEWBRANCHNO", objStudent.NewBranchNo);
                        objParams[4] = new SqlParameter("@P_NEWROLLNO", NewRollNo);
                        objParams[5] = new SqlParameter("@P_OLDROLLNO", OldRollNo);
                        objParams[6] = new SqlParameter("@P_REMARK", objStudent.Remark);
                        objParams[7] = new SqlParameter("@P_COLLEGE_CODE", objStudent.CollegeCode);
                        objParams[8] = new SqlParameter("@P_USERNO", userno);
                        objParams[9] = new SqlParameter("@P_PAIDFEE", paidfee);
                        objParams[10] = new SqlParameter("@P_EXCESSAMOUNT", excessamt);
                        objParams[11] = new SqlParameter("@P_DEGREENO", objStudent.NewDegreeNo);
                        //  objParams[12] = new SqlParameter("@P_DEGREENO", objStudent.NewDegreeNo);
                        objParams[12] = new SqlParameter("@P_COLLEGE_ID", objStudent.College_ID);//added by deepali on 15/09/2020
                        objParams[13] = new SqlParameter("@P_BRCH_NO", brch_no);
                        objParams[14] = new SqlParameter("@P_ISWITHFEEPAID", IsWithFeePaid);
                        objParams[15] = new SqlParameter("@P_SEMESTERNO", SemesterNo);
                        objParams[16] = new SqlParameter("@P_OUT", SqlDbType.Int);
                        objParams[16].Direction = ParameterDirection.Output;
                        object ret = objSQLHelper.ExecuteNonQuerySP("PKG_STUDENT_SP_UPD_BRANCH_CHANGE_FINAL_APPROVE", objParams, true);
                        if (Convert.ToInt32(ret) == -99)
                            retStatus = Convert.ToInt32(CustomStatus.TransactionFailed);
                        else if (Convert.ToInt32(ret) == 2627)
                            retStatus = Convert.ToInt32(CustomStatus.RecordExist);
                        else
                            retStatus = Convert.ToInt32(CustomStatus.RecordSaved);
                    }
                    catch (Exception ex)
                    {
                        retStatus = Convert.ToInt32(CustomStatus.Error);
                        throw new IITMSException("IITMS.NITPRM.BusinessLayer.BusinessLogic.BranchController.ChangeBranch_FinalApprove-> " + ex.ToString());
                    }
                    return retStatus;
                }



            }
        }
    }
}
